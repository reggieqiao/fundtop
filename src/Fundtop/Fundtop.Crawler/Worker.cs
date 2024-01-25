using Fundtop.Crawler.Services;
using Fundtop.Crawler.Services.Interface;
using Quartz;
using Quartz.Impl;

namespace Fundtop.Crawler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICrawlerService _crawlerService;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ICrawlerService crawlerService)
        {
            _logger = logger;
            _configuration = configuration;
            _crawlerService = crawlerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start Execute");

            // Setup Quartz.NET scheduler
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = await schedulerFactory.GetScheduler();

            // Read crawler job configurations from appsettings.json
            var configs = _configuration.GetSection("Crawler").Get<CrawlerConfig[]>();

            // Schedule jobs
            foreach (var config in configs)
            {
                var job = JobBuilder.Create<CrawlerJob>()
                    .WithIdentity(config.Url)
                    .Build();

                // Store CrawlerConfig object in JobDataMap
                job.JobDataMap["CrawlerConfig"] = config;

                // Store CrawlerService instance in JobDataMap
                job.JobDataMap["CrawlerService"] = _crawlerService;

                var trigger = TriggerBuilder.Create()
                    .WithIdentity($"{config.Url}-trigger")
                    .WithCronSchedule(config.Cron)
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }

            // Start schedule
            await scheduler.Start();

            // Keep the service running
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }

    public class CrawlerJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // Get configuration object
            var config = (CrawlerConfig)context.MergedJobDataMap["CrawlerConfig"];

            // Get CrawlerService dependency
            var crawlerService = (CrawlerService)context.MergedJobDataMap["CrawlerService"];

            // Call fetching method
            await crawlerService.FetchDataAsync(config);
        }
    }

    public class CrawlerConfig
    {
        public string Url { get; set; }
        public string Cron { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}