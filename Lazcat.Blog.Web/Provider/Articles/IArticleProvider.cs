﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Articles;

namespace Lazcat.Blog.Web.Provider.Articles
{
    public interface IArticleProvider
    {
        Task<IEnumerable<ArticleDto>> GetArticles();
        Task<ArticleDto> GetArticle(int id);
        Task CreateArticle(CreateUpdateArticleInput input);
        Task UpdateArticle(CreateUpdateArticleInput input);
        Task DeleteArticle(int id);
    }
}