using BAITZ_BLOG_API.Context;
using BAITZ_BLOG_API.Domain;
using BAITZ_BLOG_API.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BAITZ_BLOG_API.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDataContext _context;
        public ClientRepository( ApplicationDataContext context)
        {
            _context = context;
        }
        public void DeleteClient(int id)
        {
            var c = _context.Client.Find(id);
            if (c != null)
            {
                _context.Client.Remove(c);
                _context.SaveChanges();
            }
        }
        public IEnumerable<Client> GetAllClients()
        {
          return _context.Client.ToList();
        }

        public Client GetClientByEmail(string email)
        {
            return _context.Client.FirstOrDefault(c => c.Email == email);

        }

        public Client GetClientById(int id)
        {
            return _context.Client.Find(id);
        }

        public void PostClient([FromForm] Client client)
        {
            var c = new Client()
            {
                Email = client.Email,
                Password = client.Password
            };
            _context.Client.Add(c);
            _context.SaveChanges();
        }

        public void UpdateClient(int id, Client client)
        {
           var c = _context.Client.Find(id);
            if (c != null)
            {
                c.Email = client.Email;
                c.Password = client.Password;
                _context.SaveChanges();
            }
           
        }
    }
}
