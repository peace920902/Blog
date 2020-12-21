using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Dtos;

namespace ApplicationService.Articles
{
    public class ArticleAppService: IArticleAppService
    {
        public ArticleAppService(Repository<int,Article> articleRepository)
        {
            
        }

        public async Task CreateArticle(CreateUpdateArticleInput input)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ArticleDto> GetArticle(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ArticleDto>> GetArticleList()
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateArticle(int id, CreateUpdateArticleInput input)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteArticle(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}