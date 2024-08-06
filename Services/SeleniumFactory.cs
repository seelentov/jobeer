using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Jobeer.Services.Interfaces;
using OpenQA.Selenium.Support.UI;

namespace Jobber.Services
{
    public class SeleniumFactory : IFactory<ChromeDriver>
    {
        private readonly ChromeOptions _driverOptions;
        private readonly IConfiguration _configuration;
        public SeleniumFactory(IConfiguration _configuration)
        {
            ChromeOptions options = new ChromeOptions();

            options.AddUserProfilePreference("profile.default_content_settings.images", 2); // Блокирует изображения
            options.AddUserProfilePreference("profile.default_content_settings.stylesheets", 2); // Блокирует стили
            options.AddUserProfilePreference("profile.managed_default_content_settings.images", 2); // Блокирует изображения
            options.AddUserProfilePreference("profile.managed_default_content_settings.stylesheets", 2); // Блокирует стили
            options.AddUserProfilePreference("cache.disk_cache_size", 0); // Отключает кэширование на диске
            options.AddUserProfilePreference("cache.memory_cache_size", 0); // Отключает кэширование в памяти
            options.AddUserProfilePreference("cache.enable", false); // Отключает кэширование
            options.AddUserProfilePreference("extensions.enabled", false); // Отключает расширения*/
            options.AddUserProfilePreference("privacy.clear_browsing_data_on_exit", true); // Очистка данных при выходе

            if (_configuration["Hide"] == "y")
            {
                options.AddArgument("--headless=new");
            }

            options.AddArgument("--disable-gpu");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--no-sandbox");

            options.AddArgument("--user-data-dir=" + _configuration["UserProfileDir"]);

            _driverOptions = options;
            _driverOptions.PageLoadStrategy = PageLoadStrategy.Eager;
        }
        public ChromeDriver Get()
        {
            return new ChromeDriver(_driverOptions);
        }
        public struct SeleniumOptions
        {
            public WebDriverWait wait;
            public ChromeDriver driver;
        }

    }
}
