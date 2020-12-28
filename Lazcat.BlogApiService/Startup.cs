using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Lazcat.BlogApiService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get; }
        public const string DefaultPolicy = "DefaultPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (HostEnvironment.IsDevelopment())
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 5001;
                });
            }
            else
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }
            services.AddSwaggerGen();
            services.AddDbContext<BlogContext>(opt => opt.UseSqlServer(Configuration["BlogDbConnectString"],
                b => b.MigrationsAssembly("Lazcat.BlogApiService")));
            services.AddScoped<IRepository<int, Category>, Repository<int, Category>>();
            services.AddScoped<IRepository<int, Article>, Repository<int, Article>>();
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<IArticleAppService, ArticleAppService>();
            services.AddScoped<IArticleManager, ArticleManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped(_ => new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddCors(opt => opt.AddDefaultPolicy(builder => 
                    // builder.AllowAnyOrigin()
                    // .AllowAnyHeader()
                    // .AllowAnyMethod()
                  builder.WithOrigins(Configuration["App:CorsOrigins"].Split(",", StringSplitOptions.RemoveEmptyEntries))
                 //builder.WithOrigins("http://127.0.0.1:5567", "https://localhost:5568")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
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

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseCustomerExceptionMiddleware();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
