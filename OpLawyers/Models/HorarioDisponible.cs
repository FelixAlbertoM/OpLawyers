using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpLawyers.Models;

public class HorarioDisponible
{
    [Key]
    public int HorarioId { get; set; }

    [Range(1, 7, ErrorMessage = "El día debe estar entre 1 (Lunes) y 7 (Domingo)")]
    public int DiaSemana { get; set; }

    [Required(ErrorMessage = "La hora de inicio es requerida")]
    public TimeSpan HoraInicio { get; set; }

    [Required(ErrorMessage = "La hora de fin es requerida")]
    public TimeSpan HoraFin { get; set; }

    [StringLength(50)]
    public string? Descripcion { get; set; }


    [InverseProperty("Horario")]
    public virtual ICollection<CitaDetalle> CitasDetalles { get; set; } = new List<CitaDetalle>();

    [NotMapped]
    public string NombreDia
    {
        get
        {
            return DiaSemana switch
            {
                1 => "Lunes",
                2 => "Martes",
                3 => "Miércoles",
                4 => "Jueves",
                5 => "Viernes",
                6 => "Sábado",
                7 => "Domingo",
                _ => "Desconocido"
            };
        }
    }
    [NotMapped]
    public string HorarioFormateado
    {
        get
        {
            var inicio = DateTime.Today.Add(HoraInicio);
            var fin = DateTime.Today.Add(HoraFin);
            return $"{inicio:hh:mm tt} - {fin:hh:mm tt}";
        }
    }
}
