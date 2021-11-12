using PollosHermano.MicroFramework.Domain.Repositories;
using PollosHermano.MicroFramework.Entities;
using PollosHermano.MicroFramework.Infraestructure.Factories;
using PollosHermano.MicroFramework.Infraestructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PollosHermano.MicroFramework.Infraestructure.Repositories.Common
{
    /// <summary>
    /// Implementacion de un repositorio generico
    /// </summary>
    public class RepositorySqlServer<TEntity> : RepositoryGenericSqlServer<TEntity>, IRepositorySqlServer<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dbFactory">DataBase factory</param>
        protected RepositorySqlServer(IUnitOfWorkSqlServerFactory dbFactory)
            : base(dbFactory)
        {
        }
    }

    /// <summary>
    /// Implementacion de un repositorio generico
    /// </summary>
    public class RepositoryGenericSqlServer<TEntity> : IRepositorySqlServer<TEntity> where TEntity : class
    {
        protected readonly IUnitOfWorkSqlServer _unitOfWork;
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dbFactory">DataBase factory</param>
        protected RepositoryGenericSqlServer(IUnitOfWorkSqlServerFactory dbFactory)
        {
            if (dbFactory == null)
                throw new ArgumentNullException(nameof(dbFactory));

            _unitOfWork = dbFactory.Init();
            _dbSet = _unitOfWork.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet;
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            await Task.FromResult(0);

            return _dbSet;
        }

        /// <summary>
        /// Método genérico para recuperar una coleccion de entidades
        /// </summary>
        /// <param name="filter">Expresion para filtrar las entidades</param>
        /// <param name="orderBy">Orden en el que se quiere recuperar las entidades</param>
        /// <param name="includeProperties">Propiedades de Navegacion a incluir</param>
        /// <returns>Un listado de objetos de la entidad genérica</returns>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Método genérico para recuperar una coleccion de entidades
        /// </summary>
        /// <param name="filter">Expresion para filtrar las entidades</param>
        /// <param name="orderBy">Orden en el que se quiere recuperar las entidades</param>
        /// <param name="includeProperties">Propiedades de Navegacion a incluir</param>
        /// <returns>Un listado de objetos de la entidad genérica</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Metodo generico para recuperar una entidad a partir de su identidad
        /// </summary>
        /// <param name="id">La identidad de la entidad</param>
        /// <returns>La entidad</returns>
        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Metodo generico para recuperar una entidad a partir de su identidad
        /// </summary>
        /// <param name="id">La identidad de la entidad</param>
        /// <returns>La entidad</returns>
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Metodo generico para añadir una entidad al contexto de trabajo
        /// </summary>
        /// <param name="entity">La entidad para añadir</param>
        public virtual TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);

            return entity;
        }

        /// <summary>
        /// Metodo generico para añadir una entidad al contexto de trabajo
        /// </summary>
        /// <param name="entity">La entidad para añadir</param>
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            return entity;
        }

        /// <summary>
        /// Metodo generico para eliminar una entidad del contexto de trabajo
        /// </summary>
        /// <param name="id">La identidad de la entidad</param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Metodo generico para eliminar una entidad del contexto de trabajo pasandole la entidad
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        public virtual void Delete(TEntity entity)
        {
            _unitOfWork.Attach(entity);
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Metodo generico para eliminar una entidad del contexto de trabajo pasandole la entidad
        /// </summary>
        /// <param name="entityToDelete">Entidad a eliminar</param>
        public virtual void DeleteRange(params TEntity[] entitiesToDelete)
        {
            _dbSet.RemoveRange(entitiesToDelete);
        }

        /// <summary>
        /// Metodo generico para eliminar una entidad del contexto de trabajo pasandole la entidad
        /// </summary>
        /// <param name="entityToDelete">Entidad a eliminar</param>
        public virtual void DeleteRange(IEnumerable<TEntity> entitiesToDelete)
        {
            _dbSet.RemoveRange(entitiesToDelete);
        }

        /// <summary>
        /// Metodo generico para eliminar una entidad del contexto de trabajo
        /// </summary>
        /// <param name="id">La identidad de la entidad</param>
        public virtual async Task DeleteAsync(object id)
        {
            TEntity entityToDelete = await _dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Metodo generico para eliminar una entidad del contexto de trabajo pasandole la entidad
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            _unitOfWork.Attach(entity);
            _dbSet.Remove(entity);
            await Task.FromResult(0);
        }

        /// <summary>
        /// Metodo generico para modificar una entidad en el contexto de trabajo
        /// </summary>
        /// <param name="entityToUpdate">La entidad a modificar</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            _unitOfWork.SetModified(entityToUpdate);
        }

        /// <summary>
        /// Metodo generico para modificar una entidad en el contexto de trabajo
        /// </summary>
        /// <param name="entityToUpdate">La entidad a modificar</param>
        public virtual async Task UpdateAsync(TEntity entityToUpdate)
        {
            await Task.FromResult(0);
            _unitOfWork.SetModified(entityToUpdate);
        }

        /// <summary>
        /// Implementacion generica de un metodo para paginar
        /// </summary>
        /// <typeparam name="TKey">Clave para el orden</typeparam>
        /// <param name="pageIndex">Indice de la pagina a recuperar</param>/// 
        /// <param name="pageCount">Numero de entidades a recuperar</param>
        /// <param name="orderByExpression">La expresion para establecer el orden</param>
        /// <param name="ascending">Si el orden es ascendente o descendente</param>
        /// <param name="includeProperties">Includes</param>
        /// <returns>Listado con todas las entidades que cumplan los criterios</returns>        
        public IEnumerable<TEntity> GetPagedElements<TKey>(int pageIndex, int pageCount, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending = true, string includeProperties = "")
        {
            if (orderByExpression == null)
                throw new ArgumentNullException("orderByExpression");

            IQueryable<TEntity> query = _dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (pageIndex < 1) { pageIndex = 1; }

            try
            {
                return ascending
                                ?
                            query.OrderBy(orderByExpression)
                                .Skip((pageIndex - 1) * pageCount)
                                .Take(pageCount)
                                .ToList()
                                :
                            query.OrderByDescending(orderByExpression)
                                .Skip((pageIndex - 1) * pageCount)
                                .Take(pageCount)
                                .ToList();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return Enumerable.Empty<TEntity>();
        }

        /// <summary>
        /// Implementacion generica de un metodo para paginar
        /// </summary>
        /// <typeparam name="TKey">Clave para el orden</typeparam>
        /// <param name="pageIndex">Indice de la pagina a recuperar</param>/// 
        /// <param name="pageCount">Numero de entidades a recuperar</param>
        /// <param name="orderByExpression">La expresion para establecer el orden</param>
        /// <param name="ascending">Si el orden es ascendente o descendente</param>
        /// <param name="includeProperties">Includes</param>
        /// <returns>Listado con todas las entidades que cumplan los criterios</returns>        
        public async Task<IEnumerable<TEntity>> GetPagedElementsAsync<TKey>(int pageIndex, int pageCount, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending = true, string includeProperties = "")
        {
            ValidateOrderByExpression(orderByExpression);

            IQueryable<TEntity> query = _dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (pageIndex < 1) { pageIndex = 1; }

            try
            {
                return ascending
                                ?
                            await query.OrderBy(orderByExpression)
                                .Skip((pageIndex - 1) * pageCount)
                                .Take(pageCount)
                                .ToListAsync()
                                :
                            await query.OrderByDescending(orderByExpression)
                                .Skip((pageIndex - 1) * pageCount)
                                .Take(pageCount)
                                .ToListAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return Enumerable.Empty<TEntity>();
        }

        void ValidateOrderByExpression<TKey>(Expression<Func<TEntity, TKey>> orderByExpression)
        {
            if (orderByExpression == null)
                throw new ArgumentNullException("orderByExpression");
        }

        /// <summary>
        /// Ejecutar una query en la base de datos
        /// </summary>
        /// <param name="sqlQuery">La Query</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>Listado de entidades que recupera la query</returns>
        public IEnumerable<TEntity> GetFromDatabaseWithQuery(string sqlQuery, params object[] parameters)
        {
            return _unitOfWork.ExecuteQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// Ejecutar una query en la base de datos
        /// </summary>
        /// <param name="sqlQuery">La Query</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>Listado de entidades que recupera la query</returns>
        public async Task<IEnumerable<TEntity>> GetFromDatabaseWithQueryAsync(string sqlQuery, params object[] parameters)
        {
            await Task.FromResult(0);
            return _unitOfWork.ExecuteQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// Ejecutar un command en la base de datos 
        /// </summary>
        /// <param name="sqlCommand">La query</param>
        /// <param name="parameters">Los parametros</param>
        /// <returns>El sql code que devuelve la query</returns>
        public int ExecuteInDatabaseByQuery(string sqlCommand, params object[] parameters)
        {
            return _unitOfWork.ExecuteCommand(sqlCommand, parameters);
        }

        /// <summary>
        /// Ejecutar un command en la base de datos 
        /// </summary>
        /// <param name="sqlCommand">La query</param>
        /// <param name="parameters">Los parametros</param>
        /// <returns>El sql code que devuelve la query</returns>
        public async Task<int> ExecuteInDatabaseByQueryAsync(string sqlCommand, params object[] parameters)
        {
            await Task.FromResult(0);

            return _unitOfWork.ExecuteCommand(sqlCommand, parameters);
        }
    }
}
