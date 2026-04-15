using AutoMapper;
using GameHub.Api.BLL.Interfaces;
using GameHub.Api.DAL.Interfaces;
using GameHub.Api.Models.Dtos;
using GameHub.Api.Models.Entities;

namespace GameHub.Api.BLL.Services;

public class CategoryService(ICategoryRepository repository, IMapper mapper) : ICategoryService
{
    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await repository.GetAllAsync();
        return mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await repository.GetByIdAsync(id);
        return category is null ? null : mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = mapper.Map<Category>(dto);
        var created = await repository.CreateAsync(category);
        return mapper.Map<CategoryDto>(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var entity = mapper.Map<Category>(dto);
        entity.Id = id;
        return await repository.UpdateAsync(entity);
    }

    public Task<bool> DeleteAsync(int id) => repository.DeleteAsync(id);
}
