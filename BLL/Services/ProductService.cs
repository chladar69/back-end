using AutoMapper;
using GameHub.Api.BLL.Interfaces;
using GameHub.Api.DAL.Interfaces;
using GameHub.Api.Models.Dtos;
using GameHub.Api.Models.Entities;

namespace GameHub.Api.BLL.Services;

public class ProductService(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IMapper mapper) : IProductService
{
    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await productRepository.GetAllAsync();
        return mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        return product is null ? null : mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var category = await categoryRepository.GetByIdAsync(dto.CategoryId);
        if (category is null)
        {
            throw new InvalidOperationException("Category does not exist.");
        }

        var entity = mapper.Map<Product>(dto);
        var created = await productRepository.CreateAsync(entity);
        return mapper.Map<ProductDto>(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var category = await categoryRepository.GetByIdAsync(dto.CategoryId);
        if (category is null)
        {
            return false;
        }

        var entity = mapper.Map<Product>(dto);
        entity.Id = id;
        return await productRepository.UpdateAsync(entity);
    }

    public Task<bool> DeleteAsync(int id) => productRepository.DeleteAsync(id);
}
