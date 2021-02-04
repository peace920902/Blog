using AutoMapper;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Domain.Categories;
using Lazcat.Blog.Models.Domain.Messages;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.Dtos.Categories;
using Lazcat.Blog.Models.Dtos.Messages;

namespace Lazcat.Blog.ApplicationService
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleDto>().ForMember(x => x.CategoryName, x => x.MapFrom(y => y.Category.Name));
            CreateMap<CreateUpdateArticleInput, Article>().ForMember(x=>x.Title, x=>x.Ignore()).ForMember(x=>x.Id, x=>x.Ignore()).ForMember(x => x.IsPublished, x => x.Ignore());
            CreateMap<Message, MessageDto>();
            CreateMap<CreateUpdateMessageInput, Message>().ForMember(x => x.Id, x => x.Ignore());
            CreateMap<Category, CategoryDto>();
        }
    }
}