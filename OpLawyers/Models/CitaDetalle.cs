using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpLawyers.Models;

public class CitaDetalle
{
    [Key]
    public int CitaDetalleId { get; set; }

    [Required]
    public int CitaId { get; set; }

    public int? HorarioId { get; set; }

    [Required]
    public TimeSpan HoraInicio { get; set; }

    [Required]
    public TimeSpan HoraFin { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    public bool Bloqueado { get; set; } = true;

    
    [ForeignKey("CitaId")]
    public Cita Cita { get; set; } = null!;

    [ForeignKey("HorarioId")]
    public HorarioDisponible? Horario { get; set; }
}
