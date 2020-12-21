using AutoMapper;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Dtos;

namespace ApplicationService
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleDto>().ForMember(x => x.CategoryName
                , x => x.MapFrom(y => y.Category.Name));
            CreateMap<ArticleDto, Article>();
            CreateMap<CreateUpdateArticleInput, Article>();
        }
    }
}