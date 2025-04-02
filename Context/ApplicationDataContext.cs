using BAITZ_BLOG_API.Domain;
using Microsoft.EntityFrameworkCore;

namespace BAITZ_BLOG_API.Context
{
    public class ApplicationDataContext : DbContext
    {
        
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
            
        }
        public DbSet<Client> Client { get; set; }
        public DbSet<Post> Post { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.client)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.ClientId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
