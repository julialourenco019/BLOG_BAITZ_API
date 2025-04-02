using BAITZ_BLOG_API.Domain;
using BAITZ_BLOG_API.Interfaces;

namespace BAITZ_BLOG_API.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public void Create(Client client)
        {
            _clientRepository.PostClient(client);
        }

        public void Delete(int id)
        {
            _clientRepository.DeleteClient(id);
        }

        public IEnumerable<Client> GetAll()
        {
            return _clientRepository.GetAllClients();
        }

        public Client GetByEmail(string email)
        {
            return _clientRepository.GetClientByEmail(email);
        }

        public Client GetById(int id)
        {
            return _clientRepository.GetClientById(id);
        }

        public void Update(int id, Client client)
        {
            _clientRepository.UpdateClient(id, client);
        }
    }
}
