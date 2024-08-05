using OpenQA.Selenium.Chrome;
using Jobber.Services;
using Jobeer.Services;
using Jobeer.Models;
using OpenQA.Selenium.Support.UI;
using static Jobber.Services.SeleniumFactory;
using System.Text.Json;

namespace Jobeer.Workers
{
    public class HHruWatcher : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<HHruSender> _logger;
        private readonly TelegramService _telegramService;
        private readonly ChromeDriver _driver;
        private readonly HHruService _hhruService;
        private readonly WebDriverWait _wait;
        private readonly SeleniumOptions _options;
        private readonly string _pageQuery = "&page=";

        public HHruWatcher(IServiceScopeFactory scopeFactory, ILogger<HHruSender> logger, SeleniumFactory seleniumFactory, HHruService hhruService, TelegramService telegramService)
        {
            _scopeFactory = scopeFactory;
            _telegramService = telegramService;
            _logger = logger;
            _hhruService = hhruService;
            _driver = seleniumFactory.Get();

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
                            var notificationsService = scope.ServiceProvider.GetRequiredService<NotificationsService>();

                            var messages = await _hhruService.GetNotifications(_options);

                            foreach(var message in messages)
                            {
                                var isHave = await notificationsService.Get(n => n.Key == message.ToString()) != null;

                                if (!isHave)
                                {
                                    await _telegramService.SendMessage(message.ToString(), "Новое приглашение");
                                    await notificationsService.UpdateOrAdd(new NotifCache() { Key = message.ToString() });
                                }
                            }

                            await Task.Delay(TimeSpan.FromMinutes(30));
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
                _driver.Close();
                _driver.Quit();
            }
            return Task.CompletedTask;
        }
    }
}
