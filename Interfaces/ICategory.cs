using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Dtos;
namespace Api.Interfaces
{
    public interface ICategory
    {
        Task<Category> AddCategory(Category category);
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<IEnumerable<SubCategory>> GetSubCategoriesByCatelog(string Category);
    }
}