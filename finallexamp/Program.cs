
using finallexamp.DAL;
using finallexamp.Services;
using Microsoft.IdentityModel.Tokens;

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new AnimalDbContext();
        var menuService = new MenuServices(context);
        menuService.ShowMenu();
        await menuService.HandleMenuSelectionAsync();
    }
}