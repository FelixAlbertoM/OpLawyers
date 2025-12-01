using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpLawyers.Models;

public class Cita
{
    [Key]
    public int CitaId { get; set; }

    [Required]
    public int ClienteId { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Required]
    [StringLength(50)]
    public string Estado { get; set; } = "Pendiente";

    [ForeignKey("ClienteId")]
    public virtual Cliente Cliente { get; set; } = null!;

    [InverseProperty("Cita")]
    public virtual ICollection<CitaDetalle> CitaDetalles { get; set; }
}
