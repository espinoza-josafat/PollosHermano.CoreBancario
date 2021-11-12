using PollosHermano.CoreBancario.Common;
using PollosHermano.CoreBancario.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.Identity.DbContexts
{
    public partial class IdentityContext : IdentityDbContext<
        User, Role, Guid,
        UserClaim, UserRole, UserLogin,
        RoleClaim, UserToken
        >, IIdentityContext
    {
        public IdentityContext()
        : base()
        {
            //ChangeTracker.LazyLoadingEnabled = false;
        }


        public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionStrings.IdentityConnectionString).UseLazyLoadingProxies();
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("identity");

            builder.Entity<User>(b =>
            {
                b.ToTable("User");

                b.Property(t => t.Firstname).IsRequired();
                b.Property(t => t.FatherLastname).IsRequired();

                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<UserClaim>(b =>
            {
                b.ToTable("UserClaim");
            });

            builder.Entity<UserLogin>(b =>
            {
                b.ToTable("UserLogin");
            });

            builder.Entity<UserToken>(b =>
            {
                b.ToTable("UserToken");
            });

            builder.Entity<Role>(b =>
            {
                b.ToTable("Role");

                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            builder.Entity<RoleClaim>(b =>
            {
                b.ToTable("RoleClaim");
            });

            builder.Entity<UserRole>(b =>
            {
                b.ToTable("UserRole");
            });
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
