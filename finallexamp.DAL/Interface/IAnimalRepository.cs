using finallexamp.DAL.Models;
namespace finallexamp.DAL.Interfaces
{
    public interface IAnimalRepository
    {
        Task<List<Animal>> GetAllAsync();
        Task<Animal> GetByNameAsync(string name);
        Task<List<Animal>> GetSortedAsync();
        Task<List<Animal>> GetByCountryCodeAsync(string code);
        Task AddAsync(Animal animal);
        Task UpdateAsync(Animal animal);
        Task DeleteAsync(int id);
    }
}