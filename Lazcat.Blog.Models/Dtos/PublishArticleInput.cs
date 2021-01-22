using System.ComponentModel.DataAnnotations;

namespace Lazcat.Blog.Models.Dtos
{
    public class PublishArticleInput
    {
        [Required]
        public int Id { get; set; }
        [Required] 
        public bool IsPublished { get; set; }
    }
}