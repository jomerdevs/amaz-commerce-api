using Ecommerce.Application.Interfaces.Identity;
using Ecommerce.Application.Interfaces.Infrastructure;
using Ecommerce.Application.Models.Email;
using Ecommerce.Application.Models.ImageManagement;
using Ecommerce.Application.Models.Token;
using Ecommerce.Application.Repository;
using Ecommerce.Infrastructure.EmailImplementation;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Infrastructure.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthService, AuthService>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            return services;
        }
    }
}
