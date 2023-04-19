namespace PoemifyAPI.Extensions
{
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

        }
    }
}
