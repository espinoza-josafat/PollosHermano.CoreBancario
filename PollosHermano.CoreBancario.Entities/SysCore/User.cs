using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PollosHermano.CoreBancario.Entities.SysCore
{
    public partial class User : IdentityUser<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();

        public virtual string Firstname { get; set; }

        public virtual string FatherLastname { get; set; }

        public virtual string MotherLastname { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<UserClaim> Claims { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<UserLogin> Logins { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<UserToken> Tokens { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
