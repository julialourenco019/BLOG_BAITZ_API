namespace BAITZ_BLOG_API.Domain.Models
{
    public class PostCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }  // Para upload de arquivo
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
    }
}
