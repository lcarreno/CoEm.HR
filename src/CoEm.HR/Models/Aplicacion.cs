namespace CoEm.HR.Models;

public partial class Aplicacion
{
    public int Id { get; set; }
    public int VacanteId { get; set; }
    public int DemandanteId { get; set; }
    public DateOnly FechaAplicacion { get; set; }
    public string Estado { get; set; } = null!;
    public virtual Vacante Vacante { get; set; } = null!;
    public virtual Demandante Demandante { get; set; } = null!;
}