using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lazcat.Blog.Models.Domain.Articles;

namespace Lazcat.Blog.Models.Domain.Categories
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public ICollection<Article> Articles { get; set; }
    }
}