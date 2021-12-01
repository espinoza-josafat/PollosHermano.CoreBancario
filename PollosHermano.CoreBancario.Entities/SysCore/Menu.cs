using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollosHermano.CoreBancario.Entities.SysCore
{
    [Table("Menu", Schema = "syscore")]
	public partial class Menu
	{
		[Key]
		[Required]
		[Column("Id", Order = 1, TypeName = "uniqueidentifier")]
		public virtual Guid Id { get; set; } //(uniqueidentifier, not null)

		[Newtonsoft.Json.JsonIgnore]
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual ICollection<RoleMenu> RoleMenus { get; set; }

		[Column("MenuIdParent", Order = 2, TypeName = "uniqueidentifier")]
		public virtual Guid? MenuIdParent { get; set; } = null; //(uniqueidentifier, null)

		[Required]
		[Column("Name", Order = 3, TypeName = "nvarchar")]
		[MaxLength(50)]
		public virtual string Name { get; set; } //(nvarchar(50), not null)

		[Column("Description", Order = 4, TypeName = "nvarchar")]
		[MaxLength(255)]
		public virtual string Description { get; set; } //(nvarchar(255), null)

		[Column("Icon", Order = 5, TypeName = "nvarchar")]
		[MaxLength(255)]
		public virtual string Icon { get; set; } //(nvarchar(255), null)

		[Required]
		[Column("Path", Order = 6, TypeName = "nvarchar")]
		[MaxLength(255)]
		public virtual string Path { get; set; } //(nvarchar(255), not null)

		[Required]
		[Column("IsExternalLink", Order = 7, TypeName = "bit")]
		public virtual bool IsExternalLink { get; set; } = false; //(bit, not null)

		[Required]
		[Column("DateCreation", Order = 8, TypeName = "datetime")]
		public virtual DateTime DateCreation { get; set; } //(datetime, not null)

		[Column("DateModification", Order = 9, TypeName = "datetime")]
		public virtual DateTime? DateModification { get; set; } = null; //(datetime, null)

		[Required]
		[Column("Order", Order = 10, TypeName = "int")]
		public virtual int Order { get; set; } //(int, not null)

		[Required]
		[Column("Status", Order = 11, TypeName = "tinyint")]
		public virtual byte Status { get; set; } //(tinyint, not null)
	}
}
