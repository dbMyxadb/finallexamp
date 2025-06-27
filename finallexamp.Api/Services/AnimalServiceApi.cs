using finallexamp.Api.Interfaces;
using finallexamp.Api.Models;
using finallexamp.Core.Interfaces;
using System.Text.Json;

namespace finallexamp.Api.Services
{
    public class AnimalServiceApi : IAnimalServiceApi
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService _loggerService;

        public AnimalServiceApi(HttpClient httpClient, ILoggerService loggerService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://aes.shenlu.me/");
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }


        public async Task<List<AnimalApi>> GetAllAnimalsByScientificNameAsync()
        {
            var response = _httpClient.GetAsync($"api/v1/species");
            if (!response.Result.IsSuccessStatusCode)
            {
                var error = new HttpRequestException($"Error fetching data: {response.Result.ReasonPhrase}");
                _loggerService.LogError("Failed to fetch animals by name from API.", error);
                throw error;
            }

            try
            {
                var content = await response.Result.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(content))
                {
                    _loggerService.LogWarning("Received empty response from API.");
                    return new List<AnimalApi>();
                }

                var animalsByName = JsonSerializer.Deserialize<List<AnimalApi>>(content);
                if (animalsByName == null || !animalsByName.Any())
                {
                    _loggerService.LogWarning("No animals found in the response.");
                }

                _loggerService.LogInformation($"Found {animalsByName.Count} animals in the response.");
                return animalsByName ?? new List<AnimalApi>();
            }
            catch (JsonException ex)
            {
                _loggerService.LogError($"Error deserializing response:", ex);
                return new List<AnimalApi>();
            }
        }


        public async Task<List<WrapperAnimal>> GetAllAnimalsByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync($"api/search?{name}");

            if (!response.IsSuccessStatusCode)
            {
                var error = new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                _loggerService.LogError("Failed to fetch animals by name from API.", error);
                throw error;
            } 

            try
            {
                var content = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrWhiteSpace(content))
                {
                    _loggerService.LogWarning("Received empty response from API.");
                    return new List<WrapperAnimal>();
                }

                var animalsByName = JsonSerializer.Deserialize<List<WrapperAnimal>>(content);
                if (animalsByName == null || !animalsByName.Any())
                {
                    _loggerService.LogWarning("No animals found in the response.");
                }

                _loggerService.LogInformation($"Found {animalsByName.Count} animals in the response.");
                return animalsByName;
            }
            catch (JsonException ex)
            {
                _loggerService.LogError($"Error deserializing response:", ex);
                return new List<WrapperAnimal>();
            }
        }

        public async Task<List<WrapperAnimal>> GetAllAnimalsByNameSortedAsync(string name)
        {
            var response = await _httpClient.GetAsync($"api/search?q=Giant%2520Panda&sortType={name}");

            if (!response.IsSuccessStatusCode)
            {
                var error = new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                _loggerService.LogError("Failed to fetch sorted animals by name from API.", error);
                throw error;
            }

            try
            {
                var content = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrWhiteSpace(content))
                {
                    _loggerService.LogWarning("Received empty response from API.");
                    return new List<WrapperAnimal>();
                }

                var sortedAnimals = JsonSerializer.Deserialize<List<WrapperAnimal>>(content);
                if (sortedAnimals == null || !sortedAnimals.Any())
                {
                    _loggerService.LogWarning("No sorted animals found in the response.");
                }

                _loggerService.LogInformation($"Found {sortedAnimals.Count} sorted animals in the response.");
                return sortedAnimals;
            }
            catch (JsonException ex)
            {
                _loggerService.LogError($"Error deserializing response:", ex);
                return new List<WrapperAnimal>();
            }
        }

        public async Task<List<WrapperAnimal>> GetAllAnimalsByCountryIdAsync(string code)
        {
            var response = await _httpClient.GetAsync($"api/search?q=Giant%2520Panda&sortType={code}");

            if (!response.IsSuccessStatusCode)
            {
                var error = new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                _loggerService.LogError("Failed to fetch animals by country code from API.", error);
                throw error;
            }

            try
            {
                var content = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrWhiteSpace(content))
                {
                    _loggerService.LogWarning("Received empty response from API.");
                    return new List<WrapperAnimal>();
                }

                var animalsByCountry = JsonSerializer.Deserialize<List<WrapperAnimal>>(content);
                if (animalsByCountry == null || !animalsByCountry.Any())
                {
                    _loggerService.LogWarning("No animals found for the specified country code.");
                }
                _loggerService.LogInformation($"Found {animalsByCountry.Count} animals for country code {code}.");
                return animalsByCountry;
            }
            catch (JsonException ex)
            {
                _loggerService.LogError($"Error deserializing response:", ex);
                return new List<WrapperAnimal>();
            }
        }
    }
}
