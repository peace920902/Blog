using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using Lazcat.Blog.ApplicationService.Categories;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.EntityFramework;
using Lazcat.Blog.Models.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Lazcat.Blog.Test
{
    public class CategoryServiceTest:TestBase
    {
        private readonly ICategoryAppService _categoryAppService;


        protected CategoryServiceTest() : base(new DbContextOptionsBuilder<BlogContext>().UseSqlite("Filename=Test.db").Options)
        {
        }

        // [Fact]
        // public async Task Should_Get_All()
        // {
        //     using (var context = new BlogContext(ContextOptions))
        //     {
        //     }
        // }
    }
}