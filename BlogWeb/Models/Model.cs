using Microsoft.EntityFrameworkCore;

namespace BlogWeb.Models
{
	public class Model
	{

	}

    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
        {
            
        }

        public DbSet<ArticleModel> Articles { get; set; }
    }
}
