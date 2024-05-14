namespace CoEm.HR.Models;

public class PerfilViewModel : UsuarioViewModel
{
    public string Nombre { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Direccion { get; set; } = null!;
    public bool EsEmpleador { get; set; }
}