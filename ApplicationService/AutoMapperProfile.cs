using AutoMapper;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Domain.Categories;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.Dtos.Categories;

namespace ApplicationService
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleDto>().ForMember(x => x.CategoryName
                , x => x.MapFrom(y => y.Category.Name));
            CreateMap<CreateUpdateArticleInput, Article>();
            CreateMap<Category, CategoryDto>();
        }
    }
}