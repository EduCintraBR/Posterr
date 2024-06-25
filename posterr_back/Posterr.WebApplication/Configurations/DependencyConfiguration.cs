using Posterr.Application.Services;
using Posterr.Repository.Repositories;

namespace Posterr.WebApplication.Configurations {
    public static class DependencyConfiguration {

        public static void ConfigureService(this WebApplicationBuilder builder) {

            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<PostRepository>();
            builder.Services.AddScoped<ContentRepository>();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<PostService>();
            builder.Services.AddScoped<ContentService>();

        }


    }
}
