using finallexamp;
using finallexamp.BLL.Services;
using finallexamp.DAL;
using finallexamp.DAL.Interfaces;
using finallexamp.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

class Program
{
    static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            Log.Information("Додаток запускається...");

            var services = new ServiceCollection();
            services.AddDbContext<AnimalDbContext>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<AnimalService>();

            var provider = services.BuildServiceProvider();
            var service = provider.GetRequiredService<AnimalService>();

            while (true)
            {
                Console.WriteLine("1. Всi тварини (з API + БД)");
                Console.WriteLine("2. Пошук по iменi (API)");
                Console.WriteLine("3. Сортування по iменi (API)");
                Console.WriteLine("4. По коду країни (API)");
                Console.WriteLine("5. Додати в БД");
                Console.WriteLine("6. Оновити в БД");
                Console.WriteLine("7. Видалити з БД");
                Console.WriteLine("0. Вихiд");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var all = await service.GetAllAnimalsAsync();
                        foreach (var a in all)
                            Console.WriteLine($"{a.Id}: {a.Name} - {a.Species} [{a.CountryCode}]");
                        Log.Information("Отримано всiх тварин з API та БД");
                        break;
                    case "2":
                        Console.Write("iм'я: ");
                        var name = Console.ReadLine();
                        var apiByName = await service.GetApiByNameAsync(name);
                        foreach (var a in apiByName)
                            Console.WriteLine($"{a.Name} - {a.Species} [{a.CountryCode}]");
                        Log.Information("Пошук через API: {Name}", name);
                        break;
                    case "3":
                        var sortedApi = await service.GetApiSortedAsync();
                        foreach (var a in sortedApi)
                            Console.WriteLine($"{a.Name} - {a.Species}");
                        Log.Information("Сортування через API");
                        break;
                    case "4":
                        Console.Write("Код країни: ");
                        var code = Console.ReadLine();
                        var byCountry = await service.GetApiByCountryAsync(code);
                        foreach (var a in byCountry)
                            Console.WriteLine($"{a.Name} - {a.Species}");
                        Log.Information("Пошук через API по країнi: {Code}", code);
                        break;
                    case "5":
                        Console.Write("iм'я: "); var addName = Console.ReadLine();
                        Console.Write("Вид: "); var species = Console.ReadLine();
                        Console.Write("Країна: "); var country = Console.ReadLine();
                        await service.AddAnimalAsync(new finallexamp.DAL.Models.Animal { Name = addName, Species = species, CountryCode = country });
                        Log.Information("Додано тварину в БД: {Name} - {Species} [{Country}]", addName, species, country);
                        Console.WriteLine("Додано.");
                        break;
                    case "6":
                        Console.Write("ID: "); var idU = int.Parse(Console.ReadLine());
                        Console.Write("Нове iм'я: "); var nName = Console.ReadLine();
                        Console.Write("Новий вид: "); var nSpecies = Console.ReadLine();
                        Console.Write("Нова країна: "); var nCountry = Console.ReadLine();
                        await service.UpdateAnimalAsync(new finallexamp.DAL.Models.Animal { Id = idU, Name = nName, Species = nSpecies, CountryCode = nCountry });
                        Log.Information("Оновлено тварину ID={Id}", idU);
                        Console.WriteLine("Оновлено.");
                        break;
                    case "7":
                        Console.Write("ID: "); var idD = int.Parse(Console.ReadLine());
                        await service.DeleteAnimalAsync(idD);
                        Log.Information("Видалено тварину ID={Id}", idD);
                        Console.WriteLine("Видалено.");
                        break;
                    case "0":
                        Log.Information("Завершено роботу програми");
                        return;
                    default:
                        Console.WriteLine("Невiрно");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Помилка в програмi");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}