using BAITZ_BLOG_API.Domain;
namespace BAITZ_BLOG_API.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        void Create(Client client);
        void Update(int id,Client client);
        void Delete(int id);
        Client GetByEmail(string email);

    }
}
