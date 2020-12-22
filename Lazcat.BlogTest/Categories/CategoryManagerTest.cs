using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Lazcat.Blog.Domain.Categories;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Models.Domain.Categories;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Lazcat.Blog.Test.Categories
{
    public class CategoryManagerTest
    {
        private readonly ICategoryManager _manager;
        private readonly IRepository<int, Category> _repository;

        public CategoryManagerTest()
        {
            _repository = Substitute.For<IRepository<int, Category>>();
            _manager = new CategoryManager(_repository);
        }

        [Fact]
        public async Task Should_Create_New()
        {
            _repository.FirstOrDefaultAsync((x) => true).Returns(Task.FromResult((Category)null));
            var category = await _manager.CreateCategory("test");
            category.Name.ShouldBe("test");
        }

        [Fact]
        public void Should_Create_New_Failed()
        {
            _repository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Category,bool>>>()).Returns(Task.FromResult(new Category{Name = "test"}));
            var httpResponseException = Should.Throw<HttpResponseException>(async ()=>await _manager.CreateCategory("test"));
            httpResponseException.Response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            httpResponseException.Response.ReasonPhrase?.ShouldContain($"This category name test is existed");
        }
    }
}