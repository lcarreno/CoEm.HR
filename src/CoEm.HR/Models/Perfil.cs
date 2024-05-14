namespace CoEm.HR.Models;

public partial class Perfil
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Direccion { get; set; } = null!;
    public bool EsEmpleador { get; set; }
    public virtual Usuario Usuario { get; set; } = null!;
    public virtual Empleador? Empleador { get; set; }
    public virtual Demandante? Demandante { get; set; }
}