using OpenQA.Selenium.Chrome;
using Jobber.Services;
using Jobeer.Services;
using Jobeer.Models;
using OpenQA.Selenium.Support.UI;
using static Jobber.Services.SeleniumFactory;
using System.Text.Json;
using Jobeer.Exceptions;
using Jobeer.Interfaces.Services;
using OpenQA.Selenium;

namespace Jobeer.Workers
{
    public class SenderHosted : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SenderHosted> _logger;
        private readonly TelegramService _telegramService;
        private readonly ParserFactory _parserFactory;
        private readonly SeleniumFactory _seleniumFactory;
        private readonly string _pageQuery = "&page=";

        private WebDriverWait _wait;
        private SeleniumOptions _options;
        private WebDriver _driver;

        public SenderHosted(IServiceScopeFactory scopeFactory, ILogger<SenderHosted> logger, SeleniumFactory seleniumFactory, TelegramService telegramService, ParserFactory parserFactory)
        {
            _scopeFactory = scopeFactory;
            _telegramService = telegramService;
            _logger = logger;
            _seleniumFactory = seleniumFactory;
            _parserFactory = parserFactory;

            ReBuildDriver();
        }

        public void ReBuildDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
            }

            _driver = _seleniumFactory.Get(DriverType.Chrome);
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

                            var searchModels = (await searchModelsService.GetAll()).ToList();

                            foreach (var searchModel in searchModels)
                            {
                                if (searchModel == null)
                                {
                                    continue;
                                }

                                var links = new List<string>();

                                var parser = _parserFactory.Get(searchModel.Type);

                                int page = searchModel.Type == SearchModelType.HHru ? 0 : 1;

                                while (true)
                                {
                                    List<string> pageLinks = new();

                                    try
                                    {
                                        pageLinks = await parser.GetPageLinks(searchModel.Url + _pageQuery + page, _options);
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
                                        await _telegramService.SendMessage(ex.Message, "SenderThrowError");
                                    }

                                    await Task.Delay(TimeSpan.FromSeconds(30));

                                }

                                int count = 0;

                                foreach (var link in links)
                                {
                                    try
                                    {
                                        await parser.ThrowMessage(link, _options);
                                        await _telegramService.SendMessage(link, "ThrowMessage");
                                    }
                                    catch(OutOfLimitException ex)
                                    {
                                        _logger.LogInformation(ex, ex.Message);
                                        await _telegramService.SendMessage(ex.Message, "SenderOutOfLimit");

                                        var smsToDel = searchModels.Where(s=>s.Type == searchModel.Type).ToList();

                                        foreach(var sm in smsToDel)
                                        {
                                            searchModels.Remove(sm);
                                        }

                                        continue;
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogInformation(ex, ex.Message);
                                        await _telegramService.SendMessage(ex.Message, "SenderThrowError");
                                    }
                                    finally
                                    {
                                        await _telegramService.SendMessage("Count " + count + " / " + links.Count);
                                        count++;
                                    }

                                    await Task.Delay(TimeSpan.FromSeconds(30));
                                }

                                await searchModelsService.CheckLastParse(s => s.Url == searchModel.Url);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex, ex.Message);
                        await _telegramService.SendMessage(ex.Message, "SenderError");

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
