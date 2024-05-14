namespace CoEm.HR.Models;

public partial class Vacante
{
    public int Id { get; set; }
    public int EmpleadorId { get; set; }
    public string Titulo { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string TipoContrato { get; set; } = null!;
    public decimal Salario { get; set; }
    public DateOnly FechaPublicacion { get; set; }
    public virtual Empleador Empleador { get; set; } = null!;
    public ICollection<Aplicacion>? Aplicaciones { get; set; }
}