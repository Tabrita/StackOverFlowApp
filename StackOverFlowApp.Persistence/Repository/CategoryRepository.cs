using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackOverFlowApp.Application.Persistence;
using StackOverFlowApp.Domain;

namespace StackOverFlowApp.Persistence.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StackOverFlowDbContext _context;
        public CategoryRepository(StackOverFlowDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            var category = await _context.Categories.Where(c => c.CategoryID == categoryId).FirstOrDefaultAsync();
            return category;
        }

        public async void InsertCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async void UpdateCategory(Category category)
        {
            var cat = await _context.Categories.Where(c => c.CategoryID == category.CategoryID).FirstOrDefaultAsync();
            if (cat != null)
            {
                cat.CategoryName = category.CategoryName;
                await _context.SaveChangesAsync();
            }
        }

        public async void DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.Where(c => c.CategoryID == categoryId).FirstOrDefaultAsync();
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}