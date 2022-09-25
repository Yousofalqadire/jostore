using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Interfaces;
using Api.Models;
using Api.Dtos;

namespace Api.Controllers
{
     [ApiController]
    [Route("api/categories")]
    public class CategoriesController:ControllerBase
    {
        private readonly ICategory category;

        public CategoriesController(ICategory _category)
        {
            category = _category;
        }

        [HttpGet("get-categories")]
        public async Task<ActionResult<IEnumerable<Category>>> getCategories()
        {
            return Ok(await category.GetCategories());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            return Ok(await category.GetCategory(id));
        }
        [HttpPost("add-category")]
        public async Task<ActionResult<Category>> AddCategory(Category model)
        {
            return Ok(await category.AddCategory(model));
        }
        [HttpGet("get-sub-categories/{name}")]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategories([FromRoute]string name)
        {
           return Ok(await category.GetSubCategoriesByCatelog(name));
        }
    }
}