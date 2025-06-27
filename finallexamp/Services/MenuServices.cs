
using System;
using finallexamp.Api.Interfaces;
using finallexamp.Api.Services;
using finallexamp.Core.Interfaces;
using finallexamp.Core.Services;
using finallexamp.DAL;
using finallexamp.DAL.Repositories;
using Microsoft.Extensions.Logging;

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
            _logger = new LoggerService();
            _animalServiceApi = new AnimalServiceApi(new HttpClient(), new LoggerService());
            _animalService = new AnimalServices(new AnimalRepository(_context), new LoggerService());
        }

        public void ShowMenu()
        {
            Console.WriteLine("Welcome to the Final Exam Menu! \n(api+bd)");
            Console.WriteLine("1. Show all Animal (api+bd)");
            Console.WriteLine("2. Get all Animal by name");
            Console.WriteLine("3. Sort Animal by name");
            Console.WriteLine("4. Get all Animal by country code (UA,UK)");
            Console.WriteLine("===== Local bd =====");
            Console.WriteLine("5. Add Animal");
            Console.WriteLine("6. Update Animal");
            Console.WriteLine("7. Delete Animal");

            Console.Write("Please select an option: ");
        }



        public async Task HandleMenuSelectionAsync()
        {
            Console.WriteLine("Entry your choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await GetAnimalByScintificName();

                    break;
                case "2":
                    await GetAllAnimalsByName();
                    break;
                case "3":

                    break;
                case "4":

                    break;
                case "5":


                    break;
                case "6":
                    break;
                case "7":
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
                Console.Write(animals);
                foreach(var animal in animals)
                {
                    Console.WriteLine(animal);
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
                Console.Write(animals);
                if (animals == null || !animals.Any())
                {
                    Console.WriteLine("No animals found in the local database.");
                    return;
                }
                foreach (var animal in animals)
                {
                    Console.WriteLine(animal);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching animals by scientific name from local database.", ex);
            }
        }

        public async Task GetAnimalByScintificName()
        {
            Console.WriteLine("Animals from api: ");
            await GetAnimalByScientificNameWithApi();
            
            Console.WriteLine("Animals from local DB: ");
            await GetAnimalByScientificNameFromLocalDB();
        }

        public async Task GetAllAnimalsByName()
        {
            Console.WriteLine("Entry Animal name: "); string AnimalName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(AnimalName))
            {
                Console.WriteLine("Animal name cannot be empty. Please try again.");
                return;
            }

            Console.WriteLine("Animals from api: ");

            try
            {
                var animals = await _animalServiceApi.GetAllAnimalsByNameAsync(AnimalName);
                if (animals == null || !animals.Any())
                {
                    Console.WriteLine("No animals found with that name.");
                }
                else
                {
                    foreach (var animal in animals)
                    {
                        Console.WriteLine(animal);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching animals by name from API.", ex);
            }


            Console.WriteLine("Animals from local DB: ");
            var animalsFromDb = await _animalService.GetAllAnimalsByNameAsync(AnimalName);
            if (animalsFromDb == null || !animalsFromDb.Any())
            {
                Console.WriteLine("No animals found with that name in the local database.");
            }
            else
            {
                foreach (var animal in animalsFromDb)
                {
                    Console.WriteLine(animal);
                }
            }

        }
    }
}
