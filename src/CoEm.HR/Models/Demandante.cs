namespace CoEm.HR.Models;

public partial class Demandante
{
    public int Id { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public string NivelEducacion { get; set; } = null!;
    public string? ExperienciaLaboral { get; set; }
    public virtual Perfil Perfil { get; set; } = null!;
    public ICollection<Aplicacion>? Aplicaciones { get; set; }
}