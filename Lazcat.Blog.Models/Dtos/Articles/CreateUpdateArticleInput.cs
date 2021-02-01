using System.ComponentModel.DataAnnotations;

namespace Lazcat.Blog.Models.Dtos.Articles
{
    public class CreateUpdateArticleInput
    {
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Title length should less than 50")]
        public string Title { get; set; }
        [MaxLength(100, ErrorMessage = "Description length should less than 100")]
        public string Description { get; set; }
        public string Content { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
        public bool IsPublished { get; set; }
        public string Cover { get; set; }
    }
}