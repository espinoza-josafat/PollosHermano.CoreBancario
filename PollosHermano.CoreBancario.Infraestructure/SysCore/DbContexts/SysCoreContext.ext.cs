using PollosHermano.CoreBancario.Entities.SysCore;
using Microsoft.EntityFrameworkCore;

namespace PollosHermano.CoreBancario.Infraestructure.SysCore.DbContexts
{
    public partial class SysCoreContext
    {
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<RoleMenu> RoleMenu { get; set; }
    }
}
