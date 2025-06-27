using finallexamp.Core.Models;

namespace finallexamp.Core.Interfaces
{
    public interface IAnimalService
    {
        Task<List<Animal>> GetAllAnimalsByScientificNameAsync();
        Task<List<Animal>> GetAllAnimalsByNameAsync(string name);
        Task<List<Animal>> GetAllAnimalsByNameSortedAsync(string name);
        Task<List<Animal>> GetAllAnimalsByCountryIdAsync(string code);
    }
}
