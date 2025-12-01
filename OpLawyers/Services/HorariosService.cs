using Microsoft.EntityFrameworkCore;
using OpLawyers.DAL;
using OpLawyers.Models;

namespace OpLawyers.Services
{
    public class HorariosService
    {
        private readonly Contexto _contexto;

        public HorariosService(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<List<HorarioDisponible>> ListarHorariosAsync()
        {
            return await _contexto.HorariosDisponibles
                .OrderBy(h => h.DiaSemana)
                .ThenBy(h => h.HoraInicio)
                .ToListAsync();
        }
        public async Task<List<HorarioDisponible>> ListarHorariosPorDiaAsync(int diaSemana)
        {
            return await _contexto.HorariosDisponibles
                .Where(h => h.DiaSemana == diaSemana && h.Activo)
                .OrderBy(h => h.HoraInicio)
                .ToListAsync();
        }

        public async Task<List<HorarioDisponible>> ObtenerHorariosDisponiblesParaFechaAsync(DateTime fecha)
        {
            int diaSemana = fecha.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)fecha.DayOfWeek;
            var fechaSolo = fecha.Date;

            var horariosBloquedos = await _contexto.CitaDetalles
                .Where(cd => cd.Fecha.Date == fechaSolo && cd.Bloqueado)
                .Select(cd => cd.HorarioId)
                .ToListAsync();

            return await _contexto.HorariosDisponibles
                .Where(h => h.DiaSemana == diaSemana
                         && h.Activo
                         && !horariosBloquedos.Contains(h.HorarioId))
                .OrderBy(h => h.HoraInicio)
                .ToListAsync();
        }
        public async Task<bool> HorarioDisponibleAsync(int horarioId, DateTime fecha)
        {
            var fechaSolo = fecha.Date;

            return !await _contexto.CitaDetalles
                .AnyAsync(cd => cd.HorarioId == horarioId
                             && cd.Fecha.Date == fechaSolo
                             && cd.Bloqueado);
        }
        public async Task<bool> CrearHorarioAsync(HorarioDisponible horario)
        {
            try
            {
                var existe = await _contexto.HorariosDisponibles
                    .AnyAsync(h => h.DiaSemana == horario.DiaSemana
                                && h.HoraInicio == horario.HoraInicio
                                && h.HoraFin == horario.HoraFin);

                if (existe)
                    return false;

                _contexto.HorariosDisponibles.Add(horario);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> ActualizarHorarioAsync(HorarioDisponible horario)
        {
            try
            {
                _contexto.HorariosDisponibles.Update(horario);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> EliminarHorarioAsync(int horarioId)
        {
            try
            {
                var horario = await _contexto.HorariosDisponibles
                    .Include(h => h.CitasDetalles)
                    .FirstOrDefaultAsync(h => h.HorarioId == horarioId);

                if (horario == null)
                    return false;

                if (horario.CitasDetalles.Any())
                {
                    horario.Activo = false;
                    await _contexto.SaveChangesAsync();
                    return true;
                }
                _contexto.HorariosDisponibles.Remove(horario);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<HorarioDisponible?> ObtenerHorarioPorIdAsync(int horarioId)
        {
            return await _contexto.HorariosDisponibles
                .FirstOrDefaultAsync(h => h.HorarioId == horarioId);
        }

        public async Task<bool> CrearHorariosPredeterminadosAsync()
        {
            try
            {
                if (await _contexto.HorariosDisponibles.AnyAsync())
                    return false;

                var horarios = new List<HorarioDisponible>();

                for (int dia = 1; dia <= 6; dia++)
                {
                    for (int hora = 8; hora < 17; hora++)
                    {
                        horarios.Add(new HorarioDisponible
                        {
                            DiaSemana = dia,
                            HoraInicio = new TimeSpan(hora, 0, 0),
                            HoraFin = new TimeSpan(hora + 1, 0, 0),
                            Activo = true,
                            Descripcion = hora < 12 ? "Mañana" : "Tarde"
                        });
                    }
                }
                _contexto.HorariosDisponibles.AddRange(horarios);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
