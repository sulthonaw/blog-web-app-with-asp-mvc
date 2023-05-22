using System.ComponentModel.DataAnnotations;

namespace BlogWeb.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Excerpt { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public string? Author { get; set; }
        public string? FeaturedImg { get; set; }
        public int TimeRead { get; set; }
        public string? Category { get; set; }
    }
}
