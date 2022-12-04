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
                    databaseCreator.CreateTables();
                    //var db = Database.Open("MyDatabase");
                    //string sql = $"SELECT Count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MyTable'";
                    //int count = Database.SqlQuery<int>($"SELECT Count(*) AS C FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Products'").Single();
                    //if (count == 0)
                    //{

                    //table exists
                    //}
                    //if (!databaseCreator.HasTables()) databaseCreator.CreateTables();

                    //databaseCreator.CreateTables();

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