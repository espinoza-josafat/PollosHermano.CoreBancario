using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollosHermano.CoreBancario.Entities.SysCore
{
    [Table("RoleMenu", Schema = "syscore")]
	public partial class RoleMenu
	{
		[Key]
		[Column("RoleId", Order = 1, TypeName = "uniqueidentifier")]
		[Required]
		public virtual Guid RoleId { get; set; } //(uniqueidentifier, not null)

		[ForeignKey("RoleId")]
		[Newtonsoft.Json.JsonIgnore]
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual Role Role { get; set; }

		[Key]
		[Column("MenuId", Order = 2, TypeName = "uniqueidentifier")]
		[Required]
		public virtual Guid MenuId { get; set; } //(uniqueidentifier, not null)

		[ForeignKey("MenuId")]
		[Newtonsoft.Json.JsonIgnore]
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual Menu Menu { get; set; }
	}
}
