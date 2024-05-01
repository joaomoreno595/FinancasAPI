using Domain.Entities;

namespace Domain.Abstractions
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>?> GetAll();
        Task<Category?> GetCategoryById(int id);
    }
}
