using System;
using System.Collections.Generic;
using Lazcat.Blog.Models.Dtos.Messages;

namespace Lazcat.Blog.Models.Dtos.Articles
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CategoryName { get; set; }
        public DateTime EditTime { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishTime { get; set; }
        public string Cover { get; set; }
        public IEnumerable<MessageDto> Messages { get; set; }
    }
}