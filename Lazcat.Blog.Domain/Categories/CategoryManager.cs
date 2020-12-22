using System;
using System.Net;
using System.Threading.Tasks;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Infrastructure;
using Lazcat.Blog.Infrastructure.Exceptions;
using Lazcat.Blog.Models.Domain.Categories;

namespace Lazcat.Blog.Domain.Categories
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IRepository<int, Category> _repository;

        public CategoryManager(IRepository<int, Category> repository)
        {
            _repository = repository;
        }

        public async Task<Category> CreateCategory(string name)
        {
            var category = new Category();
            await SetName(name, category);
            return category;
        }

        public async Task SetName(string name, Category category)
        {
            var existedCategory = await _repository.FirstOrDefaultAsync(x => x.Name.Equals(name));
            if (existedCategory != null) throw ExceptionBuilder.Build(HttpStatusCode.BadRequest, new HttpException($"This category name {name} is existed"));
            category.Name = name;
        }
    }
}