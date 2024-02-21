using Microsoft.EntityFrameworkCore;
using quipquick_api.Interfaces;
using quipquick_api.Models;

namespace quipquick_api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly quipquickContext _context;

        public CategoryRepository(quipquickContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            try
            {
                return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las categorías", ex);
            }
        }

        public async Task<Category> GetById(int id)
        {
            try
            {
                var result = await _context.Categories.FirstAsync(c => c.Id == id);
                return result;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Debe ingresar un identificador", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("La categoría no fue encontrada", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Interno del servidor", ex);
            }
        }

        public async void Add(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo guardar la información", ex);
            }
            
        }

        public void Update(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la categoría", ex);
            }
        }

        public void Delete(Category category)
        {
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la categoría", ex);
            }
        }
    }
}
