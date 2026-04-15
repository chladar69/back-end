using GameHub.Api.BLL.Interfaces;
using GameHub.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll()
    {
        var result = await service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
    {
        try
        {
            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
