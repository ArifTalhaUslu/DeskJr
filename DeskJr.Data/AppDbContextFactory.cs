using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DeskJr.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=DeskJr;trusted_connection=true;encrypt=false;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
