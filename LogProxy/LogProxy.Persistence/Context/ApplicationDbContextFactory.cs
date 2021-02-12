using LogProxy.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LogProxy.Persistence.Context
{
    public class ApplicationDbContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
    {
        public ApplicationDbContextFactory() { }

        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
    }
}