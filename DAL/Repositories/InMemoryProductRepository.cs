using GameHub.Api.DAL.Interfaces;
using GameHub.Api.Models.Entities;

namespace GameHub.Api.DAL.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products =
    [
        new Product { Id = 1, Title = "Skyrim (PC)", Price = 24.99m, CategoryId = 1 },
        new Product { Id = 2, Title = "Brawl Stars Gems Pack", Price = 9.99m, CategoryId = 2 }
    ];

    public Task<List<Product>> GetAllAsync() => Task.FromResult(_products.ToList());

    public Task<Product?> GetByIdAsync(int id) =>
        Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

    public Task<Product> CreateAsync(Product product)
    {
        var nextId = _products.Count == 0 ? 1 : _products.Max(p => p.Id) + 1;
        product.Id = nextId;
        _products.Add(product);
        return Task.FromResult(product);
    }

    public Task<bool> UpdateAsync(Product product)
    {
        var existing = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existing is null)
        {
            return Task.FromResult(false);
        }

        existing.Title = product.Title;
        existing.Price = product.Price;
        existing.CategoryId = product.CategoryId;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var existing = _products.FirstOrDefault(p => p.Id == id);
        if (existing is null)
        {
            return Task.FromResult(false);
        }

        _products.Remove(existing);
        return Task.FromResult(true);
    }
}
