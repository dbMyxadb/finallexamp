using finallexamp.Core.Models;

namespace finallexamp.Core.Interfaces
{
    public interface IAnimalRepository
    {
        Task<List<Animal>> GetAllAsync();
        Task AddAsync(Animal animal);
        Task UpdateAsync(Animal animal);
        Task DeleteAsync(int id);
    }
}