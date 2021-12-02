namespace PollosHermano.CoreBancario.Entities.Core.Models
{
    public class GetClienteListModel {
	public virtual int Id { get; set; } //(int, not null)
	public virtual string Nombre { get; set; } //(nvarchar(25), not null)
	public virtual string ApellidoPaterno { get; set; } //(nvarchar(25), not null)
	public virtual string ApellidoMaterno { get; set; } //(nvarchar(25), null)
	public virtual System.DateTime FechaNacimiento { get; set; } //(date, not null)
}

}
