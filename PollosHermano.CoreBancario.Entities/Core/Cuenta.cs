using PollosHermano.MicroFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollosHermano.CoreBancario.Entities.Core
{
    [Table("Cuenta", Schema = "dbo")]
    public class Cuenta : BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", Order = 1, TypeName = "int")]
        public virtual int Id { get; set; }
        
        [Required]
        [Column("IdContrato", Order = 2, TypeName = "int")]
        public virtual int IdContrato { get; set; }
        
        [ForeignKey("IdContrato")]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Contrato Contrato  { get; set; }
        
        [Required]
        [Column("IdCliente", Order = 3, TypeName = "int")]
        public virtual int IdCliente { get; set; }
        
        [ForeignKey("IdCliente")]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Cliente Cliente  { get; set; }
        
        [Required]
        [Column("IdTipoCuenta", Order = 4, TypeName = "tinyint")]
        public virtual byte IdTipoCuenta { get; set; }
        
        [ForeignKey("IdTipoCuenta")]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual CatTipoCuenta CatTipoCuenta  { get; set; }
        
        [Required]
        [Column("NumeroCuenta", Order = 5, TypeName = "nvarchar")]
        [MaxLength(13)]
        public virtual string NumeroCuenta { get; set; }
        
        
    }
}