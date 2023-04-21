namespace PoemifyAPI.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using Poemify.DAL.Context;
    using Poemify.DAL.Entities;
    using Poemify.Helpers.Implementations;
    using Poemify.Helpers.Interfaces;

    public static class Configurations
    {
        public static void AddConfigurations(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
            services.Configure<IISOptions>(options =>
            {
            });
            services.AddSingleton<ILoggerManager, LoggerManager>();
            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
                
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),
           builder.Services);
            builder.AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();


        }
    }
}
