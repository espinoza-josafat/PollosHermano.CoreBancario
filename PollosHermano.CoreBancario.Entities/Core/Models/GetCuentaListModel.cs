namespace PollosHermano.CoreBancario.Entities.Core.Models
{
    public class GetCuentaListModel {
	public virtual int Id { get; set; } //(int, not null)
	public virtual int IdContrato { get; set; } //(int, not null)
	public virtual string IdCliente { get; set; } //(nvarchar(25), not null)
	public virtual string IdTipoCuenta { get; set; } //(nvarchar(25), not null)
	public virtual string NumeroCuenta { get; set; } //(nvarchar(13), not null)
}

}
