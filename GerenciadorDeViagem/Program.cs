
using GerenciadorDeViagem.Data;
using GerenciadorDeViagem.Data.Dal.Interfaces;
using GerenciadorDeViagem.Data.Dao;
using GerenciadorDeViagem.Data.Interfaces;

namespace GerenciadorDeViagem
{
    public class Program
    {
        public static string connectionString { get; private set; } = default!;
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

           connectionString = builder.Configuration.GetConnectionString("sqlServer")!;

            builder.Services.Configure<ConfigBanco>(builder.Configuration.GetSection("SqlServer"));

            builder.Services.AddScoped<ILoginDal, LoginDal>();
            builder.Services.AddScoped<IBanco, Banco>();
            builder.Services.AddScoped<IAdministradorDal, AdministradorDal>();
            builder.Services.AddScoped<IViagemDal, ViagemDal>();
           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}