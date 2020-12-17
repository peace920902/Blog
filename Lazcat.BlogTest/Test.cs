using System;
using System.Threading.Tasks;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.EntityFramework;
using Lazcat.Blog.Models.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Lazcat.Blog.Test
{
    public class Test
    {
        private readonly IRepository<int, Category> _repository;
        public Test(BlogContext blogContext)
        {
            _repository = new Repository<int, Category>(blogContext);
        }

        [Fact]
        public async Task Testtt()
        {
            await _repository.CreateAsync(new Category() {Name = "123"});
            Console.Out.WriteLine("123");
        }
    }
}
