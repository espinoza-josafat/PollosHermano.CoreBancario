using System.Collections.Generic;

namespace PollosHermano.CoreBancario.Entities.SysCore
{
    public partial class Role
    {
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
