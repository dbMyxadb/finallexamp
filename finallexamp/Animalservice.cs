using finallexamp.DAL.Interfaces;
using finallexamp.DAL.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;

namespace finallexamp.BLL.Services
{
    public class AnimalService
    {
        private readonly IAnimalRepository _repo;
        private readonly HttpClient _http;

        public AnimalService(IAnimalRepository repo)
        {
            _repo = repo;
            _http = new HttpClient();
        }

        public async Task<List<Animal>> GetAllAnimalsAsync()
        {
            var local = await _repo.GetAllAsync();
            var api = await _http.GetFromJsonAsync<List<Animal>>("https://aes.shenlu.me/api/v1/species");
            return local.Concat(api ?? new List<Animal>()).ToList();
        }

        public Task<Animal> GetByNameAsync(string name) => _repo.GetByNameAsync(name);
        public Task<List<Animal>> GetSortedAsync() => _repo.GetSortedAsync();
        public Task<List<Animal>> GetByCountryCodeAsync(string code) => _repo.GetByCountryCodeAsync(code);

        public Task AddAnimalAsync(Animal animal) => _repo.AddAsync(animal);
        public Task UpdateAnimalAsync(Animal animal) => _repo.UpdateAsync(animal);
        public Task DeleteAnimalAsync(int id) => _repo.DeleteAsync(id);

        public async Task<List<Animal>> GetApiByNameAsync(string name)
        {
            return await _http.GetFromJsonAsync<List<Animal>>($"https://aes.shenlu.me/api/v1/species/animals?name={Uri.EscapeDataString(name)}")
                   ?? new List<Animal>();
        }

        public async Task<List<Animal>> GetApiSortedAsync()
        {
            return await _http.GetFromJsonAsync<List<Animal>>("https://aes.shenlu.me/api/v1/species/animals?sort=name")
                   ?? new List<Animal>();
        }

        public async Task<List<Animal>> GetApiByCountryAsync(string code)
        {
            return await _http.GetFromJsonAsync<List<Animal>>($"https://aes.shenlu.me/api/v1/species/animals?country={Uri.EscapeDataString(code)}")
                   ?? new List<Animal>();
        }
    }
}

