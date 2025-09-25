using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parking.FindingSlotManagement.Infrastructure.Persistences;

namespace Parking.FindingSlotManagement.Infrastructure.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ParkZDbContext>();
            
            try
            {
                // Ensure database is created
                await context.Database.EnsureCreatedAsync();
                
                // Seed data
                await context.SeedDataAsync();
                
                Console.WriteLine("Database seeded successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding database: {ex.Message}");
                throw;
            }
        }

        public static async Task SeedDatabaseAsync(this ParkZDbContext context)
        {
            try
            {
                Console.WriteLine("Starting database seeding...");
                
                // Ensure database is created
                await context.Database.EnsureCreatedAsync();
                
                // Seed data
                await context.SeedDataAsync();
                
                Console.WriteLine("Database seeded successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding database: {ex.Message}");
                throw;
            }
        }
    }
}
