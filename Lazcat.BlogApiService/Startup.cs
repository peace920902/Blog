using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Cors;
using ApplicationService.Categories;
using AutoMapper;
using Lazcat.Blog.ApplicationService;
using Lazcat.Blog.ApplicationService.Articles;
using Lazcat.Blog.ApplicationService.Categories;
using Lazcat.Blog.Domain.Articles;
using Lazcat.Blog.Domain.Categories;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.EntityFramework;
using Lazcat.Blog.Infrastructure;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Domain.Categories;
using Markdig;
using Microsoft.EntityFrameworkCore;

namespace Lazcat.BlogApiService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public const string DefaultPolicy = "DefaultPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddDbContext<BlogContext>(opt => opt.UseSqlServer(Configuration["BlogDbConnectString"],
                b => b.MigrationsAssembly("Lazcat.BlogApiService")));
            services.AddScoped<IRepository<int, Category>, Repository<int, Category>>();
            services.AddScoped<IRepository<int, Article>, Repository<int, Article>>();
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<IArticleAppService, ArticleAppService>();
            services.AddScoped<IArticleManager, ArticleManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<MarkdownPipeline>((s) => new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddCors(opt => opt.AddPolicy(DefaultPolicy ,builder => 
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                // builder.WithOrigins(Configuration["App:CorsOrigins"].Split(",", StringSplitOptions.RemoveEmptyEntries))
                // builder.WithOrigins("http://127.0.0.1:5567","https://127.0.0.1:5568")
                //     .AllowAnyHeader()
                //     .AllowAnyMethod()
                //     .AllowCredentials()
                //     .SetIsOriginAllowedToAllowWildcardSubdomains()
                ));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseCors(DefaultPolicy);
            app.UseAuthorization();

            app.UseCustomerExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
