using System;
using System.Linq;
using finallexamp.Api.Services;
using finallexamp.Core.Services;
using finallexamp.DAL;
using finallexamp.DAL.Repositories;

namespace finallexamp.Services
{
    public class MenuServices
    {
        private readonly LoggerService _logger;
        private readonly AnimalServiceApi _animalServiceApi;
        private readonly AnimalServices _animalService;
        private readonly AnimalDbContext _context;


        public MenuServices()
        {
            _context = new AnimalDbContext();
            _logger = new LoggerService();
            _animalServiceApi = new AnimalServiceApi(new HttpClient(), new LoggerService());
            _animalService = new AnimalServices(new AnimalRepository(_context), new LoggerService());
        }


        public void ShowMenu()
        {
            Console.WriteLine("Welcome to the Final Exam Menu! \n(api+bd)");
            Console.WriteLine("1. Show all Animal ");
            Console.WriteLine("2. Get all Animal by name");
            Console.WriteLine("3. Sort Animal by name");
            Console.WriteLine("4. Get all Animal by country code (UA,UK)");
            Console.WriteLine("5. Exit");
            Console.Write("Please select an option: ");
        }


        public async Task HandleMenuSelectionAsync()
        {
            Console.WriteLine("Entry your choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await GetAnimalByScintificNameAsync();
                    break;
                case "2":
                    await GetAllAnimalsByNameAsync();
                    break;
                case "3":
                    await SortedAnimalByNameAsync();
                    break;
                case "4":
                    await GetAllAnimalsByCountryCodeAsync();
                    break;
                case "5":
                    Console.WriteLine("Exiting the menu. Goodbye!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }


        public async Task GetAnimalByScientificNameWithApi()
        {
            try
            {
                var animals = await _animalServiceApi.GetAllAnimalsByScientificNameAsync();
                if (animals == null || !animals.Any())
                {
                    Console.WriteLine("No animals found with that scientific name.");
                    return;
                }

                foreach (var animal in animals)
                {
                    Console.WriteLine(animal);
                    Console.WriteLine("---------------------");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching animals by scientific name from API.", ex);
            }
        }


        public async Task GetAnimalByScientificNameFromLocalDB()
        {
            try
            {
                var animals = await _animalService.GetAllAnimalsByScientificNameAsync();
                if (animals == null || !animals.Any())
                {
                    Console.WriteLine("No animals found in the local database.");
                    return;
                }

                foreach (var animal in animals)
                {
                    Console.WriteLine(animal);
                    Console.WriteLine("---------------------");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching animals by scientific name from local database.", ex);
            }
        }


        public async Task GetAnimalByScintificNameAsync()
        {
            Console.WriteLine("Animals from api: ");
            await GetAnimalByScientificNameWithApi();

            Console.WriteLine("Animals from local DB: ");
            await GetAnimalByScientificNameFromLocalDB();
        }


        public async Task GetAllAnimalsByNameWithApiAsync(string name)
        {
            var animals = await _animalServiceApi.GetAllAnimalsByNameAsync(name);
            if (animals.Animals == null)
            {
                Console.WriteLine("No animals found with that name.");
                return;
            }
            foreach (var animal in animals.Animals)
            {
                Console.WriteLine(animal);
                Console.WriteLine("---------------------");
            }
        }


        public async Task GetAllAnimalsByNameFromLocalDBAsync(string name)
        {
            var animals = await _animalService.GetAllAnimalsByNameAsync(name);
            if (animals == null || !animals.Any())
            {
                Console.WriteLine("No animals found in the local database with that name.");
                return;
            }
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                Console.WriteLine("---------------------");
            }
        }

        public async Task GetAllAnimalsByNameAsync()
        {
            Console.WriteLine("Enter the name of the animal to search: ");
            string name = Console.ReadLine();

            Console.WriteLine("Animals from api: ");
            await GetAllAnimalsByNameWithApiAsync(name);

            Console.WriteLine("Animals from local DB: ");
            await GetAllAnimalsByNameFromLocalDBAsync(name);
        }


        public async Task SortedAnimalByNameWithApiAsync()
        {
            var animals = await _animalServiceApi.GetAllAnimalsByNameSortedAsync();
            if (animals == null)
            {
                Console.WriteLine("No animals found in the API.");
                return;
            }

            var sortedAnimals = animals.Animals.OrderBy(a => a.Name).ToList();

            if (!sortedAnimals.Any())
            {
                Console.WriteLine("No animals found with that name in the API.");
                return;
            }

            foreach (var animal in sortedAnimals)
            {
                Console.WriteLine(animal);
                Console.WriteLine("---------------------");
            }
        }


        public async Task SortedAnimalByNameFromLocalDBAsync()
        {
            var animals = await _animalService.GetAllAnimalsByNameSortedAsync();
            if (animals == null || !animals.Any())
            {
                Console.WriteLine("No animals found in the local database.");
                return;
            }
            var sortedAnimals = animals.OrderBy(a => a.Name).ToList();
            foreach (var animal in sortedAnimals)
            {
                Console.WriteLine(animal);
                Console.WriteLine("---------------------");
            }
        }


        public async Task SortedAnimalByNameAsync()
        {
            Console.WriteLine("Animals sorted by name from API: ");
            await SortedAnimalByNameWithApiAsync();

            Console.WriteLine("Animals sorted by name from local DB: ");
            await SortedAnimalByNameFromLocalDBAsync();
        }


        public async Task GetAllAnimalsByCountryCodeAsyncWithApi(string countryCode)
        {
            var animals = await _animalServiceApi.GetAllAnimalsByCountryCodeAsync(countryCode);
            if (animals.Animals == null)
            {
                Console.WriteLine($"No animals found for country code {countryCode} in the API.");
                return;
            }
            foreach (var animal in animals.Animals)
            {
                Console.WriteLine(animal);
                Console.WriteLine("---------------------");
            }
        }


        public async Task GetAllAnimalsByCountryCodeFromLocalDBAsync(string countryCode)
        {
            var animals = await _animalService.GetAllAnimalsByCountryCodeAsync(countryCode);
            if (animals == null || !animals.Any())
            {
                Console.WriteLine($"No animals found for country code {countryCode} in the local database.");
                return;
            }
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                Console.WriteLine("---------------------");
            }
        }


        public async Task GetAllAnimalsByCountryCodeAsync()
        {
            Console.WriteLine("Enter the country code (e.g., UA, UK): ");
            string countryCode = Console.ReadLine();

            Console.WriteLine($"Animals from API for country code {countryCode}: ");
            await GetAllAnimalsByCountryCodeAsyncWithApi(countryCode);

            Console.WriteLine($"Animals from local DB for country code {countryCode}: ");
            await GetAllAnimalsByCountryCodeFromLocalDBAsync(countryCode);
        }
    }
}
