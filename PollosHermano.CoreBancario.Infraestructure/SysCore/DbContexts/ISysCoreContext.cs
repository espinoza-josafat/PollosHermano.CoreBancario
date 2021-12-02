using PollosHermano.MicroFramework.Infraestructure.UnitOfWork;
using PollosHermano.CoreBancario.Entities.SysCore;
using Microsoft.EntityFrameworkCore;

namespace PollosHermano.CoreBancario.Infraestructure.SysCore.DbContexts
{
    public partial interface ISysCoreContext : IUnitOfWorkSqlServer
    {
        DbSet<RoleClaim> RoleClaims { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserClaim> UserClaims { get; set; }
        DbSet<UserLogin> UserLogins { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserToken> UserTokens { get; set; }
        DbSet<Menu> Menu { get; set; }
        DbSet<RoleMenu> RoleMenu { get; set; }
    }
}
