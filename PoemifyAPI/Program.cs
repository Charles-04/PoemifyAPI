using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using Poemify.API.Extensions;
using Poemify.DAL.Context;
using Poemify.Models.Entities;
using PoemifyAPI.Extensions;
using System.Reflection;
using System.Text;

namespace PoemifyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = builder.Configuration.GetConnectionString("DefaultConn");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
            "/nlog.config"));
            builder.Services.AddIdentity<AppUser, AppRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(jwt =>
                 {

                     string jwtConfig = builder.Configuration.GetSection("JwtConfig:Key").Value;
                     string issuer = builder.Configuration.GetSection("JwtConfig:Issuer").Value;
                     string audience = builder.Configuration.GetSection("JwtConfig:Audience").Value;
                     var key = Encoding.ASCII.GetBytes(jwtConfig);

                     jwt.SaveToken = true;
                     jwt.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(key),
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         RequireExpirationTime = true,
                         ValidIssuer = issuer,
                         ValidAudience = audience,
                         ClockSkew = TimeSpan.Zero
                     };
                 });
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddConfigurations();
            builder.Services.AddServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true); builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseCors("CorsPolicy");
            app.ConfigureException(builder.Environment);
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}