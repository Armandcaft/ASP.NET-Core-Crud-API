using System;
using CrudAPI.Models;
using CrudAPI.Repositories;

public static class MyDataSeeder
{
    public static void SeedData(IMyUserRepository userRepository, IProductRepository productRepository)
    {
        // Check if data already exists
        if (userRepository.AnyUsers() || productRepository.AnyProducts())
        {
            return; // Data already seeded
        }

        // Seed Users
        var users = new[]
        {
            new MyUser { Id = 1, Name = "John Doe" },
            new MyUser { Id = 2, Name = "Jane Smith" },
            // Add more user records as needed
        };
        foreach (var user in users)
        {
            userRepository.AddUser(user);
        }

        // Seed Products
        var products = new[]
        {
            new Product { Id = 1, Name = "Product A", Price = 10.0 },
            new Product { Id = 2, Name = "Product B", Price = 15.0 },
            // Add more product records as needed
        };
        foreach (var product in products)
        {
            productRepository.AddProduct(product);
        }
    }
}
