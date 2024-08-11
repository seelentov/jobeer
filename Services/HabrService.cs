using Jobber.Services;
using Jobeer.Exceptions;
using Jobeer.Interfaces.Services;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Jobeer.Services
{
    public class HabrService: IParser
    {
        private readonly string _domain;
        private readonly HabrEmployeeData _employeeData;
        private readonly ILogger<HabrService> _logger;
        public HabrService(HabrEmployeeData employeeData, ILogger<HabrService> logger)
        {
            _employeeData = employeeData;
            _logger = logger;

            _domain = "https://career.habr.com";
        }

        private async Task Log(string message)
        {
            _logger.LogInformation(message, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        public async Task<List<string>> GetPageLinks(string url, SeleniumFactory.SeleniumOptions options)
        {
            options.driver.Navigate().GoToUrl(url);

            var vacancies = options.driver.FindElements(By.CssSelector(".vacancy-card"));

            var links = new List<string>();

            foreach ( var v in vacancies)
            {
                var link = v.FindElement(By.CssSelector("a")).GetAttribute("href");
                links.Add(link);
                await Log("Get " + link);
            }

            return links;
        }

        public async Task ThrowMessage(string url, SeleniumFactory.SeleniumOptions options)
        {
            options.driver.Navigate().GoToUrl(url);

            var form = options.driver.FindElement(By.CssSelector(".basic-form"));

            var textarea = form.FindElement(By.CssSelector("textarea"));
            var letter = _employeeData.GetLetter();
            textarea.SendKeys(letter);

            var button = form.FindElement(By.CssSelector("button[type='submit']"));
            button.Click();

            await Task.Delay(1000);

            var isCaptcha = options.driver.PageSource.Contains("Капча");

            if (isCaptcha)
            {
                throw new BotExcention("Captcha!");
            }
        }
    }
}
