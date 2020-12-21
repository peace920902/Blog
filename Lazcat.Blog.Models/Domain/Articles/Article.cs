using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using Lazcat.Blog.Models.Domain.Categories;
using Lazcat.Blog.Models.Domain.HashTags;
using Lazcat.Blog.Models.Domain.Messages;

namespace Lazcat.Blog.Models.Domain.Articles
{
    public class Article : Entity<int>
    {
        [MaxLength(50, ErrorMessage = "Title length should less than 50")]
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishTime { get; set; }
        public string Cover { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
        public ICollection<Message> Messages { get; set; }

        public Article()
        {
            CreateTime = DateTime.Now;
        }
    }
}