using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpLawyers.Models;

public class Administrador
{
    [Key]
    public int AdministradorId { get; set; }

    [Required]
    public string UsuarioId { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Nombre { get; set; } = string.Empty;

    [ForeignKey("UsuarioId")]
    public Usuario Usuario { get; set; } = null!;

    public List<Caso> Casos { get; set; } = new();
}
