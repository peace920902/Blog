using System;

namespace Lazcat.Blog.Models.Dtos.Articles
{
    public class ArticleDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string CategoryName { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishTime { get; set; }
        public string Cover { get; set; }
    }
}