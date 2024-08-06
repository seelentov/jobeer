using OpenQA.Selenium.Chrome;
using Jobber.Services;
using Jobeer.Services;
using Jobeer.Models;
using OpenQA.Selenium.Support.UI;
using static Jobber.Services.SeleniumFactory;
using System.Text.Json;
using Jobeer.Exceptions;

namespace Jobeer.Workers
{
    public class HHruSender: IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<HHruSender> _logger;
        private readonly TelegramService _telegramService;
        private ChromeDriver _driver;
        private readonly HHruService _hhruService;
        private WebDriverWait _wait;
        private SeleniumOptions _options;
        private readonly SeleniumFactory _seleniumFactory;
        private readonly string _pageQuery = "&page=";

        private readonly bool _isLogging = true;

        public HHruSender(IServiceScopeFactory scopeFactory, ILogger<HHruSender> logger, SeleniumFactory seleniumFactory, HHruService hhruService, TelegramService telegramService)
        {
            _scopeFactory = scopeFactory;
            _telegramService = telegramService;
            _logger = logger;
            _hhruService = hhruService;
            _seleniumFactory = seleniumFactory;



            ReBuildDriver();
        }

        public void ReBuildDriver()
        {
            if(_driver != null)
            {
                _driver.Quit();
            }

            _driver = _seleniumFactory.Get();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _options = new SeleniumOptions()
            {
                wait = _wait,
                driver = _driver
            };
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {

            _ = Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {

                            var searchModelsService = scope.ServiceProvider.GetRequiredService<SearchModelsService>();

                            var searchModels = (await searchModelsService.GetRange(s => s.Type == SearchModelType.HHru)).ToList();

                            foreach (var searchModel in searchModels)
                            {
                                var links = new List<string>();

                                int page = 0;

                                while (true)
                                {
                                    await Task.Delay(TimeSpan.FromMinutes(1));

                                    List<string> pageLinks = new();

                                    bool error = false;

                                    try
                                    {
                                        pageLinks = await _hhruService.GetPageLinks(searchModel.Url + _pageQuery + page, _options);
                                        _logger.LogInformation("Page " + page);
                                        if (pageLinks.Count < 1)
                                        {
                                            break;
                                        }

                                        links.AddRange(pageLinks);
                                        page++;
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogInformation(ex, ex.Message);
                                        await _telegramService.SendMessage(ex.Message, "HHruSenderThrowError");
                                    }

                                    
                                }

                                foreach(var link in links)
                                {
                                    await Task.Delay(TimeSpan.FromMinutes(1));

                                    try
                                    {
                                        await _hhruService.ThrowMessage(link, _options);
                                        await _telegramService.SendMessage(link, "ThrowMessage");

                                    }
                                    catch (OutOfLimitException ex)
                                    {
                                        _logger.LogInformation(ex, ex.Message);
                                        await _telegramService.SendMessage(ex.Message, "HHruSenderOutOfLimitError");

                                        await Task.Delay(TimeSpan.FromHours(12));
                                    }

                                    catch (Exception ex)
                                    {
                                        _logger.LogInformation(ex, ex.Message);
                                        await _telegramService.SendMessage(ex.Message, "HHruSenderThrowError");
                                    }
                                }

                                await searchModelsService.CheckLastParse(s=>s.Url == searchModel.Url);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex, ex.Message);
                        await _telegramService.SendMessage(ex.Message, "HHruSenderError");
                       
                    }

                }
            }, cancellationToken);


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_driver != null)
            {
                _driver.Quit();
            }
            return Task.CompletedTask;
        }
    }
}
