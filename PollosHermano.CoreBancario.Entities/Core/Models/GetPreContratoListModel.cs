namespace PollosHermano.CoreBancario.Entities.Core.Models
{
    public class GetPreContratoListModel {
	public virtual int Id { get; set; } //(int, not null)
	public virtual string NombreProspecto { get; set; } //(nvarchar(25), not null)
	public virtual System.DateTime FechaNacimientoProspecto { get; set; } //(date, not null)
	public virtual string ApellidoMaternoProspecto { get; set; } //(nvarchar(25), null)
	public virtual string IdVendedor { get; set; } //(nvarchar(25), not null)
	public virtual string ApellidoPaternoProspecto { get; set; } //(nvarchar(25), not null)
	public virtual string Numero { get; set; } //(nvarchar(10), not null)
}

}
