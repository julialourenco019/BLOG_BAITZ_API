using BAITZ_BLOG_API.Domain;
using BAITZ_BLOG_API.Interfaces;
using BAITZ_BLOG_API.Domain.Models;

namespace BAITZ_BLOG_API.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository) 
        {
            _postRepository = postRepository;
        }

        public void Create(PostCreateModel postcreatemodel)
        {
           _postRepository.PostCreate(postcreatemodel);
        }

    

        public void Delete(int id)
        {
            _postRepository.PostDelete(id);
        }

        public IEnumerable<Post> GetAll()
        {
           return _postRepository.GetAllPosts();
        }

        public Post GetById(int id)
        {
           return _postRepository.GetPostById(id);
        }

        public void Update(int id, PostCreateModel postcreatemodel)
        {
           _postRepository.PostUpdate(id, postcreatemodel);
        }
    }
}
