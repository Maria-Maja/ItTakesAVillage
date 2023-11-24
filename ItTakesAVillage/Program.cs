using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
namespace ItTakesAVillage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ItTakesAVillageContextConnection") ?? throw new InvalidOperationException("Connection string 'ItTakesAVillageContextConnection' not found.");

            builder.Services.AddDbContext<ItTakesAVillageContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ItTakesAVillageUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ItTakesAVillageContext>();

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
