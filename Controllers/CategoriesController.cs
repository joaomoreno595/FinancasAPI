using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryRepository categoryRepository) : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    [HttpGet]
    public async Task<ActionResult<Category>> Get()
    {
        IEnumerable<Category>? categories = await _categoryRepository.GetAll();
        if (categories == null)
            return NotFound("Categories not found");

        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        Category? category = await _categoryRepository.GetCategoryById(id);
        if (category == null)
            return NotFound("Categories not found");

        return Ok(category);
    }
}
