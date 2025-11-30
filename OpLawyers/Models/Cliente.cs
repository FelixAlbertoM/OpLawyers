using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpLawyers.Models;

public class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    [Required]
    public string UsuarioId { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Cedula { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Apellido { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Telefono { get; set; }

    [Required]
    [StringLength(20)]
    public string Estado { get; set; } = "activo";

    [ForeignKey("UsuarioId")]
    public Usuario Usuario { get; set; } = null!;

    public List<Caso> Casos { get; set; } = new();
    public List<Cita> Citas { get; set; } = new();
}
