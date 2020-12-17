using System;
using System.Threading.Tasks;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Models.Domain.Categories;

namespace ApplicationService.Categories
{
    
    public class CategoryAppService
    {
        private readonly IRepository<int, Category> _repository;

        public CategoryAppService(IRepository<int,Category> repository)
        {
            _repository = repository;
        }

        public async Task CreateCategoryAsync() 
        {
            var category = new Category(){Name = "test"};
            await _repository.CreateAsync(category);
        }
    }
}
