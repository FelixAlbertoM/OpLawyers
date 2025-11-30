using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpLawyers.Models;

public class CasoDetalle
{
    [Key]
    public int CasoDetalleId { get; set; }

    [Required]
    public int CasoId { get; set; }

    [Required]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required]
    [StringLength(2000)]
    public string Descripcion { get; set; } = string.Empty;

    [ForeignKey("CasoId")]
    public Caso Caso { get; set; } = null!;
}
