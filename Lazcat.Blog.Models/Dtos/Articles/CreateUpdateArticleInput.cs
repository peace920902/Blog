using System;
using System.ComponentModel.DataAnnotations;

namespace Lazcat.Blog.Models.Dtos.Articles
{
    public class CreateUpdateArticleInput
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Title length should less than 50")]
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public bool IsPublished { get; set; }
        public string Cover { get; set; }
    }
}