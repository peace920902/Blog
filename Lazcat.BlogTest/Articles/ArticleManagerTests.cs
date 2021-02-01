using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AntDesign;
using Lazcat.Blog.Domain.Articles;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Dtos.Articles;
using Markdig;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Lazcat.Blog.Test.Articles
{
    public class ArticleManagerTests
    {
        private readonly IArticleManager _manager;
        private readonly IRepository<int, Article> _repository;

        public ArticleManagerTests()
        {
            _repository = Substitute.For<IRepository<int, Article>>();
            _manager = new ArticleManager(_repository, new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
        }

        [Theory]
        [ClassData(typeof(CreateNewData))]
        public async Task Should_Create_New(CreateUpdateArticleInput input)
        {
            _repository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Article, bool>>>()).Returns(Task.FromResult<Article>(null));
            var article = await _manager.CreateAsync(input);
            article.Title.ShouldBe(input.Title);
            article.CategoryId.ShouldBe(input.CategoryId);
            article.Content.ShouldBe(input.Content);
            article.Cover.ShouldBe(input.Cover);
            article.Description.ShouldBe(input.Description);
        }

        [Theory]
        [ClassData(typeof(CreateNewData))]
        public void SameTitle_Should_Not_Create_New(CreateUpdateArticleInput input)
        {
            _repository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Article, bool>>>()).Returns(Task.FromResult(new Article { Title = input.Title }));
            var exception = Should.Throw<HttpResponseException>(async () => await _manager.CreateAsync(input));
            exception.Response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            exception.Response.ReasonPhrase?.ShouldContain("title is already Existed");
        }

        [Fact()]
        public void RenderMarkdownTest()
        {
            var input = "This is a text with some *emphasis*";
            var markdown = _manager.RenderMarkdown(input);
            markdown.ShouldContain("<p>This is a text with some <em>emphasis</em></p>");
        }

        private class CreateNewData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new CreateUpdateArticleInput() { Title = "test", Content = "It's a test article", CategoryId = 1, Description = "it's a suck test",IsPublished = true } };
                yield return new object[] { new CreateUpdateArticleInput { Title = "test2", Content = "It's a test article", Description = "I've tried to make it better", CategoryId = 6 } };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}