using quipquick_api.Models;

namespace quipquick_api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
