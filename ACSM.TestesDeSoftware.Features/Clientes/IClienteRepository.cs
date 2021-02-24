using ACSM.TestesDeSoftware.Features.Core;

namespace ACSM.TestesDeSoftware.Features.Clientes
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente ObterPorEmail(string email);
    }
}
