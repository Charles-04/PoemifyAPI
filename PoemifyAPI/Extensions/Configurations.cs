namespace PoemifyAPI.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using Poemify.BLL.Interfaces;
    using Poemify.BLL.Services;
    using Poemify.DAL.Context;
    using Poemify.DAL.Implementation;
    using Poemify.DAL.Interfaces;
    using Poemify.Helpers.Implementations;
    using Poemify.Helpers.Interfaces;
    using Poemify.Models.Entities;

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

        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJWTAuthenticator, JwtAuthenticator>();
            services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
            
        }
        public static void AddIdentity(this IServiceCollection services)
        {
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
