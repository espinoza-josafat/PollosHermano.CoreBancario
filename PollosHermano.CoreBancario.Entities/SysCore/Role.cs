using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PollosHermano.CoreBancario.Entities.SysCore
{
    public partial class Role : IdentityRole<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}
