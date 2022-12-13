using ForecastWebApi2.Middlewares;

namespace ForecastWebApi2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                    policy =>
                    {
                        policy.WithOrigins()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithMethods("PUT", "DELETE", "GET", "POST");
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            
            
            app.UseMiddleware<CorsMiddleware>();
            app.UseCors("MyPolicy");
            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}