using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lazcat.Blog.Models.Domain.Categories;
using Lazcat.Blog.Models.Domain.HashTags;
using Lazcat.Blog.Models.Domain.Messages;

namespace Lazcat.Blog.Models.Domain.Articles
{
    public class Article : Entity<int>
    {
        public Article()
        {
            CreateTime = DateTime.Now;
            EditTime = CreateTime;
        }

        [MaxLength(50, ErrorMessage = "Title length should less than 50")]
        public string Title { get; set; }

        public string Content { get; set; }
        public int CategoryId { get; set; }

        [MaxLength(100, ErrorMessage = "Description length should less than 100")]
        public string Description { get; set; }

        public virtual Category Category { get; set; }
        public DateTime CreateTime { get; init; }
        public DateTime EditTime { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishTime { get; set; }
        public string Cover { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}