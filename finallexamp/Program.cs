﻿using finallexamp.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var menuService = new MenuServices();
        menuService.ShowMenu();
        await menuService.HandleMenuSelectionAsync();
    }
}