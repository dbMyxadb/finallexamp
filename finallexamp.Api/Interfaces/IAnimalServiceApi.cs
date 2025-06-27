using finallexamp.Api.Models;

namespace finallexamp.Api.Interfaces
{
    public interface IAnimalServiceApi
    {
        Task<List<AnimalApi>> GetAllAnimalsByScientificNameAsync();
        Task<List<WrapperAnimal>> GetAllAnimalsByNameAsync(string name);
        Task<List<WrapperAnimal>> GetAllAnimalsByNameSortedAsync(string name);
        Task<List<WrapperAnimal>> GetAllAnimalsByCountryIdAsync(string code);
    }
}
