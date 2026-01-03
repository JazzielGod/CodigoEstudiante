using CodigoEstudiante.Models;
using CodigoEstudiante.Repositories;
using CodigoEstudiante.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodigoEstudiante.Services
{
    public class CategoryService(GenericRepository<Category> _categoryRepository)
    {
        public async Task<IEnumerable<CategoryVM>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoriesVM = categories.Select(item => new CategoryVM
            {
                CategoryId = item.CategoryId,
                Name = item.Name
            }).ToList();

            return categoriesVM;
        }

        public async Task AddAsync(CategoryVM viewModel)
        {
            var entity = new Category
            {
                Name = viewModel.Name,
            };
            await _categoryRepository.AddAsync(entity);
        }

    }
}
