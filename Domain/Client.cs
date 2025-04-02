
using System.ComponentModel.DataAnnotations;

namespace BAITZ_BLOG_API.Domain
{
    public class Client
    {
        
        public int Id { get; set; } = new int();
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Post>? Posts { get; set; }

     
    }
}
