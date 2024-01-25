using Fundtop.Crawler.Services;
using Fundtop.Crawler.Services.Interface;
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
                    services.AddSingleton<IFundRankingService, FundRankingService>();
                })
                .Build();

            host.Run();
        }
    }
}