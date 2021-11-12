using PollosHermano.MicroFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollosHermano.CoreBancario.Entities.Core
{
    [Table("Zona", Schema = "dbo")]
    public class Zona : BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", Order = 1, TypeName = "int")]
        public virtual int Id { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Vendedor> Vendedors  { get; set; }
        
        [Required]
        [Column("IdSucursal", Order = 2, TypeName = "tinyint")]
        public virtual byte IdSucursal { get; set; }
        
        [ForeignKey("IdSucursal")]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Sucursal Sucursal  { get; set; }
        
        [Required]
        [Column("Nombre", Order = 3, TypeName = "nvarchar")]
        [MaxLength(25)]
        public virtual string Nombre { get; set; }
        
        [Required]
        [Column("Estatus", Order = 4, TypeName = "bit")]
        public virtual bool Estatus { get; set; }
        
        
    }
}