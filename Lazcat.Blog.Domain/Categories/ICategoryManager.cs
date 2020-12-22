using System.Threading.Tasks;
using Lazcat.Blog.Models.Domain.Categories;

namespace Lazcat.Blog.Domain.Categories
{
    public interface ICategoryManager
    {
        Task<Category> CreateCategory(string name);
        Task SetName(string name, Category category);
    }
}