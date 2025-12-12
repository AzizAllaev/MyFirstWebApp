using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JwtTokenSample.Models;
using JwtTokenSample.Services;

namespace JwtTokenSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<NorthwindContext>(options =>
	            options.UseSqlServer(builder.Configuration.GetConnectionString(@"Server=.\SQLEXPRESS;Database=Northwind;Trusted_Connection=True")));
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	            .AddJwtBearer(options =>
	            {
	            	options.TokenValidationParameters = new TokenValidationParameters
	            	{
		            	ValidateIssuer = true,
		            	ValidateAudience = true,
			            ValidateLifetime = true,
			            ValidateIssuerSigningKey = true,
		            	ValidIssuer = "yourdomain.com",
		            	ValidAudience = "yourdomain.com",
		            	IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkeysecretkeysecretkeysecretkeysecretkey"))
		            };
	            });

            builder.Services.AddAuthorization();
            builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
            builder.Services.AddScoped<ITokenGenerator, ComplexTokenGenerator>();
            // Add services to the container.
			builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
