

using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class APIAuthTwoStepContext : DbContext
        {
            public APIAuthTwoStepContext(DbContextOptions<APIAuthTwoStepContext> options) : base(options)
            {}

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            }
        }
}