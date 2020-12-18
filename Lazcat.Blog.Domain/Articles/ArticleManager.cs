using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Models.Domain.Articles;

namespace Lazcat.Blog.Domain.Articles
{

    public class ArticleManager
    {
        private readonly IRepository<int, Article> _repository;

        public ArticleManager(IRepository<int,Article> repository)
        {
            _repository = repository;
        }

        public async Task<Article> CreateAsync([NotNull] string title, string content, bool isPublish = false, string cover = "")
        {
            //todo
            return default;
        }
    }
}