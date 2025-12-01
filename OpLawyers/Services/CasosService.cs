using Microsoft.EntityFrameworkCore;
using OpLawyers.DAL;
using OpLawyers.Models;

namespace OpLawyers.Services
{
    public class CasosService
    {
        private readonly Contexto _contexto;

        public CasosService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CrearCasoAsync(Caso caso)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(caso.Descripcion))
                    return false;

                caso.FechaApertura = DateTime.Now;
                caso.Estado = "Abierto";

                _contexto.Casos.Add(caso);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AsignarAdministradorAsync(int casoId, int administradorId)
        {
            var caso = await _contexto.Casos.FindAsync(casoId);
            if (caso == null)
                return false;

            var adminExists = await _contexto.Administradores
                .AnyAsync(a => a.AdministradorId == administradorId);

            if (!adminExists)
                return false;

            caso.AdministradorId = administradorId;
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<List<Caso>> ConsultarCasosPorClienteAsync(int clienteId)
        {
            return await _contexto.Casos
                .Include(c => c.Administrador)
                .Include(c => c.CasoDetalles)
                .Include(c => c.Cliente)
                    .ThenInclude(cl => cl.Usuario)
                .Where(c => c.ClienteId == clienteId)
                .OrderByDescending(c => c.FechaApertura)
                .ToListAsync();
        }

        public async Task<List<Caso>> ConsultarCasosPorAdminAsync(int administradorId)
        {
            return await _contexto.Casos
                .Include(c => c.Cliente)
                    .ThenInclude(cl => cl.Usuario)
                .Include(c => c.CasoDetalles)
                .Where(c => c.AdministradorId == administradorId)
                .OrderByDescending(c => c.FechaApertura)
                .ToListAsync();
        }

        public async Task<List<Caso>> ListarTodosCasosAsync()
        {
            return await _contexto.Casos
                .Include(c => c.Cliente)
                    .ThenInclude(cl => cl.Usuario)
                .Include(c => c.Administrador)
                .Include(c => c.CasoDetalles)
                .OrderByDescending(c => c.FechaApertura)
                .ToListAsync();
        }

        public async Task<Caso?> ObtenerCasoPorIdAsync(int casoId)
        {
            return await _contexto.Casos
                .Include(c => c.Cliente)
                    .ThenInclude(cl => cl.Usuario)
                .Include(c => c.Administrador)
                .Include(c => c.CasoDetalles)
                .FirstOrDefaultAsync(c => c.CasoId == casoId);
        }

        public async Task<bool> AgregarDetalleAsync(CasoDetalle detalle)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(detalle.Descripcion))
                    return false;

                var casoExists = await _contexto.Casos.AnyAsync(c => c.CasoId == detalle.CasoId);
                if (!casoExists)
                    return false;

                detalle.Fecha = DateTime.Now;
                _contexto.CasoDetalles.Add(detalle);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CambiarEstadoAsync(int casoId, string nuevoEstado)
        {
            var caso = await _contexto.Casos.FindAsync(casoId);
            if (caso == null)
                return false;

            caso.Estado = nuevoEstado;
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarCasoAsync(Caso caso)
        {
            try
            {
                _contexto.Casos.Update(caso);
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
