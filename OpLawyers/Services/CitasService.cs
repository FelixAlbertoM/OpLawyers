using Microsoft.EntityFrameworkCore;
using OpLawyers.DAL;
using OpLawyers.Models;

namespace OpLawyers.Services
{
    public class CitasService
    {
        private readonly Contexto _contexto;

        public CitasService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CrearCitaAsync(Cita cita, int horarioId)
        {
            using var transaction = await _contexto.Database.BeginTransactionAsync();
            try
            {

                var clienteExists = await _contexto.Clientes
                    .AnyAsync(c => c.ClienteId == cita.ClienteId);

                if (!clienteExists)
                    return false;

                var horarioDisponible = await _contexto.CitaDetalles
                    .AnyAsync(cd => cd.HorarioId == horarioId
                                 && cd.Fecha.Date == cita.Fecha.Date
                                 && cd.Bloqueado);

                if (horarioDisponible)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                var horario = await _contexto.HorariosDisponibles
                    .FindAsync(horarioId);

                if (horario == null)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                cita.Estado = "Pendiente";
                _contexto.Citas.Add(cita);
                await _contexto.SaveChangesAsync();

                var detalle = new CitaDetalle
                {
                    CitaId = cita.CitaId,
                    HorarioId = horarioId,
                    HoraInicio = horario.HoraInicio,
                    HoraFin = horario.HoraFin,
                    Fecha = cita.Fecha.Date,
                    Bloqueado = true
                };
                _contexto.CitaDetalles.Add(detalle);

                await _contexto.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> CancelarCitaAsync(int citaId)
        {
            using var transaction = await _contexto.Database.BeginTransactionAsync();
            try
            {
                var cita = await _contexto.Citas
                    .Include(c => c.CitaDetalles)
                    .FirstOrDefaultAsync(c => c.CitaId == citaId);

                if (cita == null)
                    return false;

                cita.Estado = "Cancelada";

                var detalle = cita.CitaDetalles.FirstOrDefault();
                if (detalle != null)
                {
                    detalle.Bloqueado = false;
                }

                await _contexto.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> CompletarCitaAsync(int citaId)
        {
            try
            {
                var cita = await _contexto.Citas.FindAsync(citaId);
                if (cita == null)
                    return false;

                cita.Estado = "Completada";
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Cita>> ListarPorClienteIdAsync(int clienteId)
        {
            return await _contexto.Citas
                .Include(c => c.CitaDetalles)
                    .ThenInclude(cd => cd.Horario)
                .Where(c => c.ClienteId == clienteId)
                .OrderByDescending(c => c.Fecha)
                .ToListAsync();
        }

        public async Task<List<Cita>> ListarTodasCitasAsync()
        {
            return await _contexto.Citas
                .Include(c => c.Cliente)
                .Include(c => c.CitaDetalles)
                    .ThenInclude(cd => cd.Horario)
                .OrderByDescending(c => c.Fecha)
                .ToListAsync();
        }

        public async Task<Cita?> ObtenerCitaPorIdAsync(int citaId)
        {
            return await _contexto.Citas
                .Include(c => c.Cliente)
                .Include(c => c.CitaDetalles)
                    .ThenInclude(cd => cd.Horario)
                .FirstOrDefaultAsync(c => c.CitaId == citaId);
        }

        public async Task<bool> ActualizarCitaAsync(Cita cita, int nuevoHorarioId)
        {
            using var transaction = await _contexto.Database.BeginTransactionAsync();
            try
            {
                var citaExistente = await _contexto.Citas
                    .Include(c => c.CitaDetalles)
                    .FirstOrDefaultAsync(c => c.CitaId == cita.CitaId);

                if (citaExistente == null)
                    return false;

                var detalleAnterior = citaExistente.CitaDetalles.FirstOrDefault();
                if (detalleAnterior != null)
                {
                    detalleAnterior.Bloqueado = false;
                }

                var nuevoHorarioDisponible = await _contexto.CitaDetalles
                    .AnyAsync(cd => cd.HorarioId == nuevoHorarioId
                                 && cd.Fecha.Date == cita.Fecha.Date
                                 && cd.Bloqueado);

                if (nuevoHorarioDisponible)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                var nuevoHorario = await _contexto.HorariosDisponibles
                    .FindAsync(nuevoHorarioId);

                if (nuevoHorario == null)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                citaExistente.Fecha = cita.Fecha;
                citaExistente.Estado = cita.Estado;

                var nuevoDetalle = new CitaDetalle
                {
                    CitaId = cita.CitaId,
                    HorarioId = nuevoHorarioId,
                    HoraInicio = nuevoHorario.HoraInicio,
                    HoraFin = nuevoHorario.HoraFin,
                    Fecha = cita.Fecha.Date,
                    Bloqueado = true
                };
                _contexto.CitaDetalles.Add(nuevoDetalle);

                await _contexto.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        public async Task<bool> EliminarCitaAsync(int citaId)
        {
            using var transaction = await _contexto.Database.BeginTransactionAsync();
            try
            {
                var cita = await _contexto.Citas
                    .Include(c => c.CitaDetalles)
                    .FirstOrDefaultAsync(c => c.CitaId == citaId);

                if (cita == null)
                    return false;

                _contexto.CitaDetalles.RemoveRange(cita.CitaDetalles);

                _contexto.Citas.Remove(cita);

                await _contexto.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
