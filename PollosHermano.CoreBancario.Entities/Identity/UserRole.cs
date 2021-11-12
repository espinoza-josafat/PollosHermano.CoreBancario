using Microsoft.AspNetCore.Identity;
using System;

namespace PollosHermano.CoreBancario.Entities.Identity
{
    public class UserRole : IdentityUserRole<Guid>
    {
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual User User { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
