namespace CoEm.HR.Models;

public partial class Empleador
{
    public int Id { get; set; }
    public string Industria { get; set; } = null!;
    public int NumeroEmpleados { get; set; }
    public string Ubicacion { get; set; } = null!;
    public virtual Perfil Perfil { get; set; } = null!;
    public ICollection<Vacante>? Vacantes { get; set; }
}