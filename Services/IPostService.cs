using BAITZ_BLOG_API.Domain;
using BAITZ_BLOG_API.Domain.Models;
namespace BAITZ_BLOG_API.Services
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll();
        Post GetById(int id);
        void Create(PostCreateModel postcreatemodel);
        void Update(int id,PostCreateModel postcreatemodel);
        void Delete(int id);
    }
}
