#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularProject.Data;
using AngularProject.Models;
using AngularAPI.Services;
using AngularAPI.Models;
using System.Text.Json;
using AngularAPI.Dtos;
using DotNetWebAPI.DTOs;
using AutoMapper;

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories([FromQuery] CategorySearchModel searchModel)
        {
           var categories = await _repo.GetAllCategorysAsync(searchModel);

            var categoryDto = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDto>>
               (categories);

/*            int count = _repo.CountAsync(searchModel).Result;
*/
          /*  PaginationMetaData<CategoryDto> paginationMetaData =
                new PaginationMetaData<CategoryDto>
                (count, searchModel.PageIndex,
                searchModel.PageSize,
                categoryDto);*/

            return categoryDto.ToList();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _repo.GetCategoryByIdAsync(id);
            var categoryDto = _mapper.Map<Category, CategoryDto>(category);

            if (category == null)
            {
                return NotFound();
            }

            return categoryDto;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }


            try
            {
                await _repo.UpdateCategoryAsync(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
           
            await _repo.AddCategoryAsync(category);

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _repo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _repo.DeleteCategoryAsync(category);

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _repo.IsCategoryExixtsAsync(id);
        }
    }
}
