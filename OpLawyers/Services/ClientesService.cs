using Microsoft.EntityFrameworkCore;
using OpLawyers.DAL;
using OpLawyers.Models;

namespace OpLawyers.Services
{
    public class ClientesService
    {
        private readonly Contexto _contexto;

        public ClientesService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CrearClienteAsync(Cliente cliente)
        {
            try
            {
                if (await ExisteCedulaAsync(cliente.Cedula))
                    return false;

                _contexto.Clientes.Add(cliente);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Cliente?> ObtenerClientePorUsuarioIdAsync(string usuarioId)
        {
            return await _contexto.Clientes
                .Include(c => c.Casos)
                .Include(c => c.Citas)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
        }

        public async Task<Cliente?> ObtenerClientePorIdAsync(int clienteId)
        {
            return await _contexto.Clientes
                .Include(c => c.Casos)
                .Include(c => c.Citas)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
        }

        public async Task<List<Cliente>> ListarClientesAsync()
        {
            return await _contexto.Clientes
                .Include(c => c.Usuario)
                .Include(c => c.Casos)
                .Include(c => c.Citas)
                .ToListAsync();
        }

        public async Task<bool> BloquearClienteAsync(int clienteId)
        {
            var cliente = await _contexto.Clientes.FindAsync(clienteId);
            if (cliente == null)
                return false;

            cliente.Estado = "bloqueado";
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DesbloquearClienteAsync(int clienteId)
        {
            var cliente = await _contexto.Clientes.FindAsync(clienteId);
            if (cliente == null)
                return false;

            cliente.Estado = "activo";
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarClienteAsync(Cliente cliente)
        {
            try
            {
                _contexto.Clientes.Update(cliente);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EstaClienteBloqueadoAsync(string usuarioId)
        {
            var cliente = await _contexto.Clientes
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            return cliente != null && cliente.Estado == "bloqueado";
        }

        private async Task<bool> ExisteCedulaAsync(string cedula)
        {
            return await _contexto.Clientes.AnyAsync(c => c.Cedula == cedula);
        }
    }
}
