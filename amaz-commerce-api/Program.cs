using Ecommerce.Application;
using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Application.Interfaces.Infrastructure;
using Ecommerce.Domain;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.AppContext;
using Ecommerce.Infrastructure.ImageCloudinary;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// inyectando el InfrastructureService que creamos en la clase InfrastructureServiceRegistration de infrastructure
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"),
    // Para imprimir el log por consola de todas las tareas que se realizen en la DB
    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName) 
    )
);

builder.Services.AddMediatR(typeof(GetProductListQueryHandler).Assembly);

builder.Services.AddScoped<IManageImageService, ManageImageService>();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    // Agregar protección para que el acceso a todos los endpoints requiera que el usuario este autenticado
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<Usuario>();

// sobreescribimos el identityBuilder para tener acceso a todos los servicios como agregar nuevos roles y eso
identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);

// soporte a roles
identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders();

// claims por defecto
identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>();

identityBuilder.AddEntityFrameworkStores<AppDbContext>(); // store DB
identityBuilder.AddSignInManager<SignInManager<Usuario>>(); // soporte login

// soporte para creación de los datetime cuando genere un nuevo registro
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

// configuración del token
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

// configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = service.GetRequiredService<AppDbContext>();
        var usuarioManager = service.GetRequiredService<UserManager<Usuario>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        await context.Database.MigrateAsync();
        await AppDbContextData.LoadDataAsync(context, usuarioManager, roleManager, loggerFactory);

    }catch(Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Error en migración");
    }
}
app.Run();
