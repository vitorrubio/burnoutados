
using AgendaBeleza.Dados;
using Microsoft.EntityFrameworkCore;

namespace AgendaBeleza
{
    public class Program
    {
        public static void Main(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            const string PoliticaCors = "trocinho";

            var builder = WebApplication.CreateBuilder(args);

            string connStr = configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Contexto>(options => options.UseSqlServer(connStr)); /*, m => m.MigrationsAssembly("AgendaBeleza")*/

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: PoliticaCors,
                                  policy =>
                                  {
                                      policy.WithOrigins("*");
                                  });
            });

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

            app.UseRouting(); //opções de rota

            app.UseCors(PoliticaCors);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}