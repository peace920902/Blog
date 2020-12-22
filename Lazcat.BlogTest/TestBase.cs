using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.EntityFramework;
using Lazcat.Blog.Models.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Lazcat.Blog.Test
{
    public class TestBase
    {
        protected TestBase(DbContextOptions<BlogContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<BlogContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new BlogContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Categories.AddRange(new List<Category>
                {
                    new Category {Name = "dotnet 5"}, 
                    new Category {Name = "DDD"},
                    new Category{Name = "Database"}}
                );

                context.AddRange();

                context.SaveChanges();
            }
        }
    }
}
