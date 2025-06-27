using finallexamp.Core.Interfaces;
using finallexamp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace finallexamp.DAL.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalDbContext _context;

        public AnimalRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Animal>> GetAllAsync() 
        {
            try
            {
                return await _context.Animals.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching animals from the database.", ex);
            }
        }


        public async Task AddAsync(Animal animal)
        {
            _context.Add(animal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Animal animal)
        {
            _context.Update(animal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Remove(animal);
                await _context.SaveChangesAsync();
            }
        }
    }
}