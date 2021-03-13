using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lazcat.Blog.Models.Domain.HashTags
{
    public class HashTag : Entity<Guid>
    {
        [Required] public string Name { get; set; }

        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}