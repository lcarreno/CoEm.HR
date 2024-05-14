namespace CoEm.HR.Models;

public class EmpleadorViewModel : PerfilViewModel
{
    public string Industria { get; set; } = null!;
    public int NumeroEmpleados { get; set; }
    public string Ubicacion { get; set; } = null!;
}
