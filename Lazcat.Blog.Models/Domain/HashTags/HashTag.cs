using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lazcat.Blog.Models.Domain.Articles;

namespace Lazcat.Blog.Models.Domain.HashTags
{
    public class HashTag
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}