using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quipquick_api.APIResponse;
using quipquick_api.DTOs;
using quipquick_api.Interfaces;
using quipquick_api.Models;
using quipquick_api.Vatidations;

namespace quipquick_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var response = new GeneralResponse<IEnumerable<CategoryDTO>>();

            try
            {
                var categories = await _categoryRepository.GetAll();
                var categoriesDTOs = categories.Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name
                });
                response.Data = categoriesDTOs;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var response = new GeneralResponse<CategoryDTO>();

            try
            {
                var category = await _categoryRepository.GetById(id);

                var categoryDTO = new CategoryDTO { Id = category.Id, Name = category.Name };
                response.Data = categoryDTO;
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return StatusCode(404, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public ActionResult<GeneralResponse<string>> SaveCategory(Category category)
        {
            var response = new GeneralResponse<string>();

            try
            {
                var validator = new CategoryValidation();
                var resultValidate = validator.Validate(category);

                if (resultValidate.IsValid)
                {
                    _categoryRepository.Add(category);
                    response.Data = "La categoría se guardó correctamente";
                    return StatusCode(200, response);
                }
                else
                {
                    response.Success = false;
                    response.Data = resultValidate.Errors[0].ErrorMessage;
                    return StatusCode(400, response);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<string>>> Update(int id, [FromBody] Category category)
        {
            var response = new GeneralResponse<string>();

            try
            {
                var existingCategory = await _categoryRepository.GetById(id);
                existingCategory.Name = category.Name;

                _categoryRepository.Update(existingCategory);

                response.Data = "La categoría se actualizó correctamente";
                return StatusCode(200, response);
            }
            catch (InvalidOperationException ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return StatusCode(404, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<string>>> Delete(int id)
        {
            var response = new GeneralResponse<string>();

            try
            {
                var existingCategory = await _categoryRepository.GetById(id);

                _categoryRepository.Delete(existingCategory);

                response.Data = "La categoría se eliminó correctamente";
                return StatusCode(200, response);
            }
            catch (InvalidOperationException ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return StatusCode(404, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return StatusCode(500, response);
            }
        }

    }
}
