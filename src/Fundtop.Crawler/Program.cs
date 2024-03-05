using Fundtop.Crawler.Services;
using Fundtop.Crawler.Services.Interface;
using Fundtop.Repositories;
using Fundtop.Repositories.Drawler;
using Fundtop.Repositories.Drawler.Interface;
using Serilog;

namespace Fundtop.Crawler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .UseSerilog((context, config) =>
                {
                    config.ReadFrom
                    .Configuration(context.Configuration)
                    .WriteTo.Console()
                    .WriteTo.File($"{AppContext.BaseDirectory}/logs/log.txt", rollingInterval: RollingInterval.Day);
                }) // Using Serilog as the log provider
                .ConfigureServices(services =>
                {
                    services.AddHttpClient();
                    services.AddHostedService<Worker>();

                    services.AddSingleton<ICrawlerService, CrawlerService>();

                    services.AddSingleton(provider =>
                    {
                        // Resolve the IConfiguration from the service provider
                        var config = provider.GetRequiredService<IConfiguration>();

                        // Create an instance of BaseRepository using the resolved IConfiguration
                        return new DataContext(config);
                    });
                    services.AddSingleton<IFundRankingRepository, FundRankingRepository>();
                    services.AddSingleton<IFundRankingService, FundRankingService>();
                })
                .Build();

            host.Run();
        }
    }
}