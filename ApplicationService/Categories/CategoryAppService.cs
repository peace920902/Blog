using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApplicationService.Categories;
using AutoMapper;
using Lazcat.Blog.Domain.Categories;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Infrastructure;
using Lazcat.Blog.Models.Domain.Categories;
using Lazcat.Blog.Models.Dtos.Categories;
using Lazcat.Blog.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Lazcat.Blog.ApplicationService.Categories
{

    public class CategoryAppService : ICategoryAppService
    {
        private readonly IRepository<int, Category> _repository;
        private readonly ICategoryManager _categoryManager;
        private readonly IMapper _mapper;

        public CategoryAppService(IRepository<int, Category> repository, ICategoryManager categoryManager, IMapper mapper)
        {
            _repository = repository;
            _categoryManager = categoryManager;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateUpdateCategoryInput input)
        {
            var category = await _categoryManager.CreateCategory(input.Name);
            await _repository.CreateAsync(category);
        }

        public async Task UpdateCategoryAsync(int id, CreateUpdateCategoryInput input)
        {
            var category = await _repository.FindAsync(id);
            if (category == null) ExceptionBuilder.Build(HttpStatusCode.NotFound, new HttpException($"Id {id} not match category"));
            await _categoryManager.SetName(input.Name, category);
            await _repository.UpdateAsync(id, category);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoryList()
        {
            var categories = await _repository.GetAll().ToListAsync();
            return _mapper.Map<List<Category>, IEnumerable<CategoryDto>>(categories);
        }

        public async Task DeleteCategory(int id)
        {
            var res = await _repository.DeleteAsync(id);
            if(!res) ExceptionBuilder.Build(HttpStatusCode.InternalServerError, new HttpException($"delete failed, check id existed"));
        }
    }
}
