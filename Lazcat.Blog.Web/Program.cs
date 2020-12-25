using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Lazcat.Blog.Web.Provider.Articles;
using Lazcat.Blog.Web.Services.Articles;

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
            builder.Services.AddScoped<IArticleProvider, ArticleProviders>();
            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://127.0.0.1:5000/") });
            
            await builder.Build().RunAsync();
        }
    }
}
