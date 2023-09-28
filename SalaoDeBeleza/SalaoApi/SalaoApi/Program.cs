
namespace SalaoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var DefaultOrigins = "_defaultOrigins";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: DefaultOrigins,
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

            app.UseRouting();

            app.UseCors(DefaultOrigins);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}