using microservices.productsvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace microservices.productsvc
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> dbContextOptions) : base(dbContextOptions)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    //if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                  
                        databaseCreator.CreateTables();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public DbSet<Product> Products { get; set; }
    }
}