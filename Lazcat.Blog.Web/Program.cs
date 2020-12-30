using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using ColorCode.Styling;
using Lazcat.Blog.Models.Web;
using Lazcat.Blog.Web.Provider.Articles;
using Lazcat.Blog.Web.Provider.Categories;
using Lazcat.Blog.Web.Services.Articles;
using Lazcat.Blog.Web.Services.Categories;
using Markdig;
using Markdig.SyntaxHighlighting;
using Pek.Markdig.HighlightJs;

namespace Lazcat.Blog.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddAntDesign();
            builder.Services.AddAutoMapper(typeof(ViewProfile));
            builder.Services.AddSingleton<IArticleProvider, ArticleProviders>();
            builder.Services.AddSingleton<IArticleService, ArticleService>();
            builder.Services.AddSingleton<ICategoryProvider, CategoryProvider>();
            builder.Services.AddSingleton<ICategoryService, CategoryService>();
            //height js �|�d
            //builder.Services.AddSingleton(_ => new MarkdownPipelineBuilder().UseAdvancedExtensions().UseHighlightJs().Build());
            //builder.Services.AddSingleton(_ => new MarkdownPipelineBuilder().UseAdvancedExtensions().UseSyntaxHighlighting(StyleDictionary.DefaultDark).Build());
            builder.Services.AddSingleton(_ => new MarkdownPipelineBuilder().UseAdvancedExtensions().UseSyntaxHighlighting().Build());
            builder.Services.AddHttpClient(Setting.DefaultHttpClient, hc => hc.BaseAddress = new Uri("https://127.0.0.1:5001/api/"));
            builder.Services
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                }).AddBootstrapProviders()
                .AddFontAwesomeIcons();

            await builder.Build().RunAsync();
        }
    }
}
