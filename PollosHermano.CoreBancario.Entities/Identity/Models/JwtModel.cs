using System;

namespace PollosHermano.CoreBancario.Entities.Identity.Models
{
    public class JwtModel
    {
        public string Token { get; set; }

        public DateTime Exp { get; set; }
    }
}
