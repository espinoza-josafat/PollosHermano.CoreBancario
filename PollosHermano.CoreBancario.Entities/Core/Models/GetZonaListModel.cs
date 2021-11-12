namespace PollosHermano.CoreBancario.Entities.Core.Models
{
    public class GetZonaListModel {
	public virtual int Id { get; set; } //(int, not null)
	public virtual string IdSucursal { get; set; } //(nvarchar(25), not null)
	public virtual string Nombre { get; set; } //(nvarchar(25), not null)
	public virtual bool Estatus { get; set; } //(bit, not null)
}

}
