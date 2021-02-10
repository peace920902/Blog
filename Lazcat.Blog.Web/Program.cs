using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Lazcat.Blog.Models.Web;
using Lazcat.Blog.Web.Provider.Articles;
using Lazcat.Blog.Web.Provider.Categories;
using Lazcat.Blog.Web.Provider.Messages;
using Lazcat.Blog.Web.Services.Articles;
using Lazcat.Blog.Web.Services.Categories;
using Lazcat.Blog.Web.Services.Messages;
using Markdig;
using Markdig.Prism;
using Microsoft.Extensions.Configuration;

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
            builder.Services.AddSingleton<IMessageProvider, MessageProvider>();
            builder.Services.AddSingleton<IMessageService, MessageService>();
            builder.Services.AddSingleton(new MarkdownPipelineBuilder().UseAdvancedExtensions().UsePrism().Build());
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
