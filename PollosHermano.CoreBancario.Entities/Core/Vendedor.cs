using PollosHermano.MicroFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollosHermano.CoreBancario.Entities.Core
{
    [Table("Vendedor", Schema = "dbo")]
    public class Vendedor : BaseEntity
    {
        [Required]
        [Column("ApellidoPaterno", Order = 1, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string ApellidoPaterno { get; set; }
        
        [Required]
        [Column("Nombre", Order = 2, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string Nombre { get; set; }
        
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", Order = 3, TypeName = "int")]
        public virtual int Id { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<PreContrato> PreContratos  { get; set; }
        
        [Required]
        [Column("IdZona", Order = 4, TypeName = "int")]
        public virtual int IdZona { get; set; }
        
        [ForeignKey("IdZona")]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Zona Zona  { get; set; }
        
        [Column("ApellidoMaterno", Order = 5, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string ApellidoMaterno { get; set; }
        
        
    }
}