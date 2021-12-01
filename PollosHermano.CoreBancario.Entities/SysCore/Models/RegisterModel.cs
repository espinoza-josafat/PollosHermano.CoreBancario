using System;

namespace PollosHermano.CoreBancario.Entities.SysCore.Models
{
    public class RegisterModel
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();

        public virtual string Firstname { get; set; }

        public virtual string FatherLastname { get; set; }

        public virtual string MotherLastname { get; set; }

        public virtual string Username { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string Role { get; set; }
    }
}
