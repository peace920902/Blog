﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Lazcat.Blog.Domain.Articles;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Models.Domain.Articles;
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
        public async Task Should_Create_New(string title, string content, int categoryId, bool isPublished = false, string cover = null)
        {
            _repository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Article, bool>>>()).Returns(Task.FromResult<Article>(null));
            var article = await _manager.CreateAsync(title, content, categoryId, isPublished, cover);
            article.Title.ShouldBe(title);
        }

        [Theory]
        [ClassData(typeof(CreateNewData))]
        public void Should_Not_Create_New(string title, string content, int categoryId, bool isPublished = false, string cover = null)
        {
            _repository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Article, bool>>>()).Returns(Task.FromResult(new Article{Title = title}));
            var exception = Should.Throw<HttpResponseException>(async ()=>await _manager.CreateAsync(title, content, categoryId, isPublished, cover));
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
                yield return new object[] { "test", "It's a test article", 1, true };
                yield return new object[] { "test2", "It's a test article", 6 };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}