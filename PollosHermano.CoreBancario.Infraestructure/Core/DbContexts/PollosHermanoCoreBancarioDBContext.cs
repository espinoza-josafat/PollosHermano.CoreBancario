using PollosHermano.CoreBancario.Common;
using PollosHermano.CoreBancario.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.Core.DbContexts
{
    public partial class PollosHermanoCoreBancarioDBContext : DbContext, IPollosHermanoCoreBancarioDBContext
    {
        public PollosHermanoCoreBancarioDBContext()
        : base()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionStrings.PollosHermanoCoreBancarioDBConnectionString);
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }

            base.OnConfiguring(optionsBuilder);
        }

        // Sobreescribimos el método OnModelCreating de la clase DbContext
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override EntityEntry Attach(object entity)
        {
            if (Entry(entity).State == EntityState.Detached)
            {
                return base.Attach(entity);
            }

            return null;
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return ExecuteQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            try
            {
                return base.Database.ExecuteSqlRaw(sqlCommand, parameters);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return -1;
        }

        // Implementación de IUnitOfWork
        public void SetModified<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await base.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException exception)
                {
                    saveFailed = true;

                    exception.Entries.ToList()
                                     .ForEach(entry =>
                                     {
                                         entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                                     });
                }
            } while (saveFailed);
        }

        public void Rollback()
        {
            ChangeTracker.Entries()
                         .ToList()
                         .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public new void Dispose()
        {
            base.Dispose();
        }
    }
}
