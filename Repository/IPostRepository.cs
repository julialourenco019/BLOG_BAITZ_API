using BAITZ_BLOG_API.Domain;
using BAITZ_BLOG_API.Domain.Models;

namespace BAITZ_BLOG_API.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
        Post GetPostById(int id);
      // Post DowloadImg(int id);
        void PostCreate(PostCreateModel postcreatemodel);
        void PostUpdate(int id, PostCreateModel postCreateModel);
        void PostDelete(int id);
    }
}
