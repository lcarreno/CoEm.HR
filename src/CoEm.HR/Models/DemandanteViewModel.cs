namespace CoEm.HR.Models;

public class DemandanteViewModel : PerfilViewModel
{
    public DateOnly FechaNacimiento { get; set; }
    public string NivelEducacion { get; set; } = null!;
    public string ExperienciaLaboral { get; set; } = null!;
}