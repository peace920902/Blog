using System.ComponentModel.DataAnnotations;

namespace Lazcat.Blog.Models.Dtos.Categories
{
    public class CreateUpdateCategoryInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}