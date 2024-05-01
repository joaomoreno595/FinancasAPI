using Domain.Abstractions;
using Domain.Entities;
using Identity.Context;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Category>?> GetAll() => await _context.Set<Category>().ToListAsync();

    public async Task<Category?> GetCategoryById(int id) => await _context.Set<Category>().FirstOrDefaultAsync(c => c.CategoryId == id);
}
