using BAITZ_BLOG_API.Context;
using BAITZ_BLOG_API.Domain;
using BAITZ_BLOG_API.Interfaces;
using BAITZ_BLOG_API.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BAITZ_BLOG_API.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDataContext _context;
        public PostRepository(ApplicationDataContext context) 
        {
            _context = context;
        }

      /*  public Post DowloadImg(int id)
        {
            var p = _context.Post.Find(id);
            var databytes = System.IO.File.ReadAllBytes(p.ImageUrl);
            return File(databytes, "image/png");
        }*/

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Post.ToList();
        }

        public Post GetPostById(int id)
        {
            return _context.Post.Find(id);
        }

        public void PostCreate([FromForm] PostCreateModel postcreatemodel)
        {
            var filePath = Path.Combine("Storage", postcreatemodel.ImageFile.FileName);
            using Stream fileStream = new FileStream(filePath , FileMode.Create);
            postcreatemodel.ImageFile.CopyTo(fileStream);
            var p = new Post()
            {
          
                Title = postcreatemodel.Title,
                Description = postcreatemodel.Description,
                ImageUrl = filePath,
                Date = postcreatemodel.Date,
                ClientId = postcreatemodel.ClientId
            };
            _context.Post.Add(p);
            _context.SaveChanges();
          

        }

        public void PostDelete(int id)
        {
            var p = _context.Post.Find(id);
            if (p != null)
            {
                _context.Post.Remove(p);
                _context.SaveChanges();
            }
        }

        public void PostUpdate(int id, PostCreateModel postcreatemodel)
        {
            var filePath = Path.Combine("Storage", postcreatemodel.ImageFile.FileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            postcreatemodel.ImageFile.CopyTo(fileStream);
            var p = _context.Post.Find(id);
            if (p != null)
            {
                 p.Title = postcreatemodel.Title;
                 p.ImageUrl = filePath;
                 p.Description = postcreatemodel.Description;
                 p.Date = postcreatemodel.Date;
                 p.ClientId = postcreatemodel.ClientId;
                _context.SaveChanges();
            }
        }
    }
}
