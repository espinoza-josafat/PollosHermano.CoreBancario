using PollosHermano.MicroFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollosHermano.CoreBancario.Entities.Core
{
    [Table("Sucursal", Schema = "dbo")]
    public class Sucursal : BaseEntity
    {
        [Required]
        [Column("Nombre", Order = 1, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string Nombre { get; set; }
        
        [Column("Direccion", Order = 2, TypeName = "nvarchar")]
        [MaxLength(250)]
        public virtual string Direccion { get; set; }
        
        [Key]
        [Required]
        [Column("Id", Order = 3, TypeName = "tinyint")]
        public virtual byte Id { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Zona> Zonas  { get; set; }
        
        
    }
}