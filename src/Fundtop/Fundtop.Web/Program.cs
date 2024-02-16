using Fundtop.Repositories;
using Fundtop.Repositories.Web;
using Fundtop.Repositories.Web.Interface;
using Fundtop.Web.Services;
using Fundtop.Web.Services.Interface;

namespace Fundtop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddSingleton(provider =>
            {
                // Resolve the IConfiguration from the service provider
                var config = provider.GetRequiredService<IConfiguration>();

                // Create an instance of BaseRepository using the resolved IConfiguration
                return new DataContext(config);
            });
            builder.Services.AddScoped<IFundRankingRepository, FundRankingRepository>();
            builder.Services.AddScoped<IFundRankingService, FundRankingService>();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}