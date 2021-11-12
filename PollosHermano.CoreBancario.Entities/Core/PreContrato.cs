using PollosHermano.MicroFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollosHermano.CoreBancario.Entities.Core
{
    [Table("PreContrato", Schema = "dbo")]
    public class PreContrato : BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", Order = 1, TypeName = "int")]
        public virtual int Id { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Contrato> Contratos  { get; set; }
        
        [Required]
        [Column("NombreProspecto", Order = 2, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string NombreProspecto { get; set; }
        
        [Required]
        [Column("FechaNacimientoProspecto", Order = 3, TypeName = "Date")]
        public virtual DateTime FechaNacimientoProspecto { get; set; }
        
        [Column("ApellidoMaternoProspecto", Order = 4, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string ApellidoMaternoProspecto { get; set; }
        
        [Required]
        [Column("IdVendedor", Order = 5, TypeName = "int")]
        public virtual int IdVendedor { get; set; }
        
        [ForeignKey("IdVendedor")]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Vendedor Vendedor  { get; set; }
        
        [Required]
        [Column("ApellidoPaternoProspecto", Order = 6, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string ApellidoPaternoProspecto { get; set; }
        
        [Required]
        [Column("Numero", Order = 7, TypeName = "nvarchar")]
        [MaxLength(10)]
        public virtual string Numero { get; set; }
        
        
    }
}