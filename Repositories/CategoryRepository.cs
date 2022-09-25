using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Interfaces;
using Api.Models;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly ApplicationDbContext db;
        public CategoryRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Category> AddCategory(Category category)
        {
            var c = new Category{Name = category.Name};
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
            return c;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
           return await db.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await db.Categories.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategoriesByCatelog(string Category)
        {
           var products = await db.Products.Where(x => x.Category == Category).ToListAsync();
             var SubCategories = products.Select(x => x.SupCategory).Distinct();
             var _SubCategories = new List<SubCategory>();
             foreach (var item in SubCategories)
             {
                var SubCategory = new SubCategory{Name = item};
                _SubCategories.Add(SubCategory);
             }
             return _SubCategories;
        }
    }
}