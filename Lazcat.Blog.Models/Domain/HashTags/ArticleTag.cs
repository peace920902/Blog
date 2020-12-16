using System;
using Lazcat.Blog.Models.Domain.Articles;

namespace Lazcat.Blog.Models.Domain.HashTags
{
    public class ArticleTag
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public Guid HashTagId { get; set; }
        public HashTag HashTag { get; set; }
    }
}