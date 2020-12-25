using AutoMapper;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.ViewModel;

namespace Lazcat.Blog.Web
{
    public class ViewProfile: Profile
    {
        public ViewProfile()
        {
            CreateMap<ArticleDto, SimpleArticle>().ForMember(x => x.PublishTime, x => x.MapFrom(y => y.PublishTime));
            
        }
    }
}