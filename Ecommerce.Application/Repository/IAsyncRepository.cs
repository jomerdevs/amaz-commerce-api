using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Repository
{
    // interfaz que será de tipo generico <T> y que ese T será de tipo class
    public interface IAsyncRepository<T> where T : class
    {

        Task<IReadOnlyList<T>> GetAllAsync();

        // obtiene todos los registros, pero recibe una parametro de una expresión lógica
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        // obtiene todos los registros y recibe una serie de parametros, como expresión lógica, orderBy, si involucra otra entidad, etc.
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
                                       string? includeString,
                                       bool disableTracking = true);

        // obtiene todos los registros y recibe una serie de parametros, como expresión lógica, orderBy, si involucra varias entidad, etc.
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                       List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);

        // este método devuelve un solo objeto y recibe parametro de expresión lógica, lista de entidades relacionadas, etc.
        Task<T> GetEntityAsync(Expression<Func<T, bool>>? predicate,
                                         List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);

        // obtener objeto por id
        Task<T> GetByIdAsync(int id);

        // agregar objeto
        Task<T> AddAsync(T entity);


        // actualizar objeto
        Task<T> UpdateAsync(T entity);

        // eliminar
        Task DeleteAsync(T entity);


        void AddEntity(T entity);

        void UpdateEntity(T entity);

        void DeleteEntity(T entity);

        // agregar una lista de objetos en una sola línea
        void AddRange(List<T> entities);

        // eliminar una lista de objetos en una sola línea
        void DeleteRange(IReadOnlyList<T> entities);
    }
}
