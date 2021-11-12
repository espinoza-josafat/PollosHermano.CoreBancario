namespace PollosHermano.CoreBancario.Entities.Core.Models
{
    public class GetSucursalListModel {
	public virtual string Nombre { get; set; } //(nvarchar(25), not null)
	public virtual string Direccion { get; set; } //(nvarchar(250), null)
	public virtual byte Id { get; set; } //(tinyint, not null)
}

}
