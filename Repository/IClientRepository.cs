using BAITZ_BLOG_API.Domain;
namespace BAITZ_BLOG_API.Interfaces
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientById(int id);
        Client GetClientByEmail(string email);
        void PostClient(Client client);
        void UpdateClient(int id , Client client);
        void DeleteClient(int id);
    }
}
