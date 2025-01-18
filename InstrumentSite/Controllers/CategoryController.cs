using InstrumentSite.Dtos.Category;
using InstrumentSite.Services.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null) return NotFound($"Category with ID {id} not found.");
        return Ok(category);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> AddCategory(CreateCategoryDTO categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoryId = await _categoryService.AddCategoryAsync(categoryDto);
        return CreatedAtAction(nameof(GetCategoryById), new { id = categoryId }, categoryDto);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDTO categoryDto)
    {
        if (id != categoryDto.Id)
        {
            return BadRequest("Category ID in the URL does not match the ID in the body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updated = await _categoryService.UpdateCategoryAsync(categoryDto);
        if (!updated) return NotFound($"Category with ID {id} not found.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var deleted = await _categoryService.DeleteCategoryAsync(id);
        if (!deleted) return NotFound($"Category with ID {id} not found.");

        return NoContent();
    }
}
