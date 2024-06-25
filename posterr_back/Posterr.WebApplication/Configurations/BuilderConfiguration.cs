using Microsoft.EntityFrameworkCore;
using Posterr.Repository;

namespace Posterr.WebApplication.Configurations {
    public static class BuilderConfiguration {

        public static void ConfigureRepository(this WebApplicationBuilder builder) {

            var cnnString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (cnnString == null)
                throw new Exception("ConnectionString 'DefaultConnection' not informed on appSettings.");

            builder.Services.AddDbContext<AppDataContext>(options => options.UseSqlServer(cnnString, b => b.MigrationsAssembly("Posterr.WebApplication")));

        }

    }
}
