using GameHub.Api.DAL.Interfaces;
using GameHub.Api.Models.Entities;

namespace GameHub.Api.DAL.Repositories;

public class InMemoryCategoryRepository : ICategoryRepository
{
    private readonly List<Category> _categories =
    [
        new Category { Id = 1, Name = "PC Games" },
        new Category { Id = 2, Name = "Mobile Donate" }
    ];

    public Task<List<Category>> GetAllAsync() => Task.FromResult(_categories.ToList());

    public Task<Category?> GetByIdAsync(int id) =>
        Task.FromResult(_categories.FirstOrDefault(c => c.Id == id));

    public Task<Category> CreateAsync(Category category)
    {
        var nextId = _categories.Count == 0 ? 1 : _categories.Max(c => c.Id) + 1;
        category.Id = nextId;
        _categories.Add(category);
        return Task.FromResult(category);
    }

    public Task<bool> UpdateAsync(Category category)
    {
        var existing = _categories.FirstOrDefault(c => c.Id == category.Id);
        if (existing is null)
        {
            return Task.FromResult(false);
        }

        existing.Name = category.Name;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var existing = _categories.FirstOrDefault(c => c.Id == id);
        if (existing is null)
        {
            return Task.FromResult(false);
        }

        _categories.Remove(existing);
        return Task.FromResult(true);
    }
}
