using Microsoft.AspNetCore.Identity;
using System;

namespace PollosHermano.CoreBancario.Entities.SysCore
{
    public partial class UserToken : IdentityUserToken<Guid>
    {
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual User User { get; set; }
    }
}
