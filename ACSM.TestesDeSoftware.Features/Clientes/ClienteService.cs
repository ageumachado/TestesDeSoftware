using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace ACSM.TestesDeSoftware.Features.Clientes
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IMediator mediator;

        public ClienteService(IClienteRepository clienteRepository,
                              IMediator mediator)
        {
            this.clienteRepository = clienteRepository;
            this.mediator = mediator;
        }

        public void Adicionar(Cliente cliente)
        {
            if (!cliente.EhValido()) return;

            clienteRepository.Adicionar(cliente);
            mediator.Publish(new ClienteEmailNotification("admin@teste.com", cliente.Email, "Olá", "Bem vindo!"));
        }

        public void Atualizar(Cliente cliente)
        {
            if (!cliente.EhValido())
                return;

            clienteRepository.Atualizar(cliente);
            mediator.Publish(new ClienteEmailNotification("admin@teste.com", cliente.Email, "Mudanças", "Dê uma olhada!"));
        }

        public void Inativar(Cliente cliente)
        {
            if (!cliente.EhValido())
                return;

            cliente.Inativar();
            clienteRepository.Atualizar(cliente);
            mediator.Publish(new ClienteEmailNotification("admin@teste.com", cliente.Email, "Até breve", "Até mais tarde!"));
        }

        public IEnumerable<Cliente> ObterTodosAtivos()
        {
            return clienteRepository.ObterTodos().Where(c => c.Ativo);
        }

        public void Remover(Cliente cliente)
        {
            clienteRepository.Remover(cliente.Id);
            mediator.Publish(new ClienteEmailNotification("admin@teste.com", cliente.Email, "Adeus", "Tenha uma boa jornada!"));
        }

        public void Dispose()
        {
            clienteRepository.Dispose();
        }
    }
}
