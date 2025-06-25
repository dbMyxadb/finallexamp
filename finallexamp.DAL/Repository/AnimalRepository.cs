using finallexamp.DAL.Interfaces;
using finallexamp.DAL.Models;
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

        public async Task<List<Animal>> GetAllAsync() => await _context.Animals.ToListAsync();

        public async Task<Animal> GetByNameAsync(string name) =>
            await _context.Animals.FirstOrDefaultAsync(a => a.Name == name);

        public async Task<List<Animal>> GetSortedAsync() =>
            await _context.Animals.OrderBy(a => a.Name).ToListAsync();

        public async Task<List<Animal>> GetByCountryCodeAsync(string code) =>
            await _context.Animals.Where(a => a.CountryCode == code).ToListAsync();

        public async Task AddAsync(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Animal animal)
        {
            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();
            }
        }
    }
}