using System;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Domain.Categories;
using Lazcat.Blog.Models.Domain.HashTags;
using Lazcat.Blog.Models.Domain.Messages;
using Microsoft.EntityFrameworkCore;

namespace Lazcat.Blog.EntityFramework
{
    public class BlogContext: DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ReplyMessage> ReplyMessages { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<HashTag> HashTags { get; set; }


        public BlogContext(DbContextOptions<BlogContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().Property(x => x.Id).IsRequired();
            modelBuilder.Entity<Article>().HasOne(x => x.Category).WithMany(x => x.Articles).HasForeignKey(x=>x.CategoryId);
            modelBuilder.Entity<Article>().HasMany(x => x.Messages).WithOne(x => x.Article).HasForeignKey(x=>x.ArticleId);
            modelBuilder.Entity<Article>().HasIndex(x => x.Title);

            modelBuilder.Entity<ArticleTag>().HasKey(t => new { t.ArticleId, t.HashTagId });
            modelBuilder.Entity<ArticleTag>().HasOne(x => x.Article).WithMany(x => x.ArticleTags)
                .HasForeignKey(x => x.ArticleId);
            modelBuilder.Entity<ArticleTag>().HasOne(x => x.HashTag).WithMany(x => x.ArticleTags)
                .HasForeignKey(x => x.HashTagId);

            modelBuilder.Entity<Category>().Property(x => x.Id).IsRequired();

            modelBuilder.Entity<Message>().Property(x => x.Id).HasDefaultValueSql("newid()").IsRequired();
            modelBuilder.Entity<Message>().HasMany(x => x.ReplyMessages).WithOne(x => x.Message)
                .HasForeignKey(x => x.MessageId);

            modelBuilder.Entity<ReplyMessage>().Property(x => x.Id).HasDefaultValueSql("newid()").IsRequired();
            modelBuilder.Entity<HashTag>().Property(x => x.Id).IsRequired();
        }
    }
}