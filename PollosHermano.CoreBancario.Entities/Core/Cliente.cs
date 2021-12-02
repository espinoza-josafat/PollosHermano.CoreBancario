using PollosHermano.MicroFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollosHermano.CoreBancario.Entities.Core
{
    [Table("Cliente", Schema = "dbo")]
    public class Cliente : BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", Order = 1, TypeName = "int")]
        public virtual int Id { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Cuenta> Cuentas  { get; set; }
        
        [Required]
        [Column("Nombre", Order = 2, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string Nombre { get; set; }
        
        [Required]
        [Column("ApellidoPaterno", Order = 3, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string ApellidoPaterno { get; set; }
        
        [Column("ApellidoMaterno", Order = 4, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string ApellidoMaterno { get; set; }
        
        [Required]
        [Column("FechaNacimiento", Order = 5, TypeName = "Date")]
        public virtual DateTime FechaNacimiento { get; set; }
        
        
    }
}