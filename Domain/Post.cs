using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BAITZ_BLOG_API.Domain
{
    public class Post
    {
        
        public int Id { get; set; } 
        public string Title  { get; set; }
        public string Description { get; set; }
        public string ImageUrl {  get; set; }
        public DateTime Date { get; set; } 

        public int ClientId { get; set; }
        public Client? client { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }




    }
}
