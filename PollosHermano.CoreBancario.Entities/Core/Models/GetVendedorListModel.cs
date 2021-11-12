namespace PollosHermano.CoreBancario.Entities.Core.Models
{
    public class GetVendedorListModel {
	public virtual string ApellidoPaterno { get; set; } //(nvarchar(25), not null)
	public virtual string Nombre { get; set; } //(nvarchar(25), not null)
	public virtual int Id { get; set; } //(int, not null)
	public virtual string IdZona { get; set; } //(nvarchar(25), not null)
	public virtual string ApellidoMaterno { get; set; } //(nvarchar(25), null)
}

}
