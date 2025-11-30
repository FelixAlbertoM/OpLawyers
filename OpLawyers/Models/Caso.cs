using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpLawyers.Models;

public class Caso
{
    [Key]
    public int CasoId { get; set; }

    [Required]
    public int ClienteId { get; set; }

    public int? AdministradorId { get; set; }

    [Required]
    public DateTime FechaApertura { get; set; } = DateTime.Now;

    [Required]
    [StringLength(50)]
    public string Estado { get; set; } = "Abierto";

    [Required]
    [StringLength(1000)]
    public string Descripcion { get; set; } = string.Empty;

    [ForeignKey("ClienteId")]
    public Cliente Cliente { get; set; } = null!;

    [ForeignKey("AdministradorId")]
    public Administrador? Administrador { get; set; }

    public List<CasoDetalle> CasoDetalles { get; set; } = new();
}
