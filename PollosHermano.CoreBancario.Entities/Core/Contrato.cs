using PollosHermano.MicroFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollosHermano.CoreBancario.Entities.Core
{
    [Table("Contrato", Schema = "dbo")]
    public class Contrato : BaseEntity
    {
        [Required]
        [Column("IdPreContrato", Order = 1, TypeName = "int")]
        public virtual int IdPreContrato { get; set; }
        
        [ForeignKey("IdPreContrato")]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual PreContrato PreContrato  { get; set; }
        
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", Order = 2, TypeName = "int")]
        public virtual int Id { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Cuenta> Cuentas  { get; set; }
        
        
    }
}