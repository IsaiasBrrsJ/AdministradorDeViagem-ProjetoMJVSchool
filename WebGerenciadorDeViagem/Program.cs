using GerenciadorDeViagem.WEB.Models.Api;
using GerenciadorDeViagem.WEB.Models.Api.Interfaces;
using GerenciadorDeViagem.WEB.Models.EndPoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GerenciadorDeViagem.WEB.Data;
using System.Globalization;

namespace WebGerenciadorDeViagem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<GerenciadorDeViagemWEBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GerenciadoViagemWEBContext") ?? throw new InvalidOperationException("Connection string 'GerenciadorDeViagemWEBContext' not found.")));

            builder.Services.AddHttpClient("ApiAdministradorDeViagens", cliente =>
            {
                cliente.BaseAddress = new Uri(String.Empty);
            });


            builder.Services.Configure<LoginEndPoint>(builder.Configuration.GetSection("EndPointsAPILogin"));
            builder.Services.Configure<ViagemEndPoint>(builder.Configuration.GetSection("EndPointsAPIViagem"));
            builder.Services.Configure<AdministradorEndPoint>(builder.Configuration.GetSection("EndPointsAPIAdministrador"));

            builder.Services.AddScoped<ILoginApi, LoginApi>();
            builder.Services.AddScoped<IApiMetodos, ApiCliente>();
            builder.Services.AddScoped<IUsuario, UsuarioApi>();
            builder.Services.AddScoped<IAdministrador, AdministradorApi>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.Run();
        }
    }
}