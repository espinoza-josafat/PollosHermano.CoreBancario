using Microsoft.AspNetCore.Identity;
using System;

namespace PollosHermano.CoreBancario.Entities.Identity
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
