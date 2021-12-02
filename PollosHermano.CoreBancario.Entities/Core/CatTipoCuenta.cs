using PollosHermano.MicroFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollosHermano.CoreBancario.Entities.Core
{
    [Table("CatTipoCuenta", Schema = "dbo")]
    public class CatTipoCuenta : BaseEntity
    {
        [Key]
        [Required]
        [Column("Id", Order = 1, TypeName = "tinyint")]
        public virtual byte Id { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Cuenta> Cuentas  { get; set; }
        
        [Required]
        [Column("Descripcion", Order = 2, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string Descripcion { get; set; }
        
        
    }
}