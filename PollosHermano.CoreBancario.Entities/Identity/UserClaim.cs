using Microsoft.AspNetCore.Identity;
using System;

namespace PollosHermano.CoreBancario.Entities.Identity
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual User User { get; set; }
    }
}
