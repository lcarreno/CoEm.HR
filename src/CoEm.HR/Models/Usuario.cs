namespace CoEm.HR.Models;

public partial class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public byte[] Clave { get; set; } = null!;
    public virtual Perfil? Perfil { get; set; }
}