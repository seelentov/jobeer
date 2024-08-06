using Jobeer.Exceptions;
using Jobeer.Interfaces.Services;
using Jobeer.Models;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using static Jobber.Services.SeleniumFactory;

namespace Jobeer.Services
{
    public class HHruService
    {
        private readonly string _domain;
        private readonly string _notificationsUrl;
        private readonly HHruEmployeeData _hhruEmployeeData;
        private readonly ILogger<HHruService> _logger;
        public HHruService(HHruEmployeeData hhruEmployeeData, ILogger<HHruService> logger)
        {
            _hhruEmployeeData = hhruEmployeeData;
            _domain = "https://hh.ru";
            _logger = logger;
            _notificationsUrl = "https://chelyabinsk.hh.ru/applicant/negotiations?state=INVITATION&filter=all&hhtmFrom=negotiation_list";
        }

        public void Log(string message)
        {
            _logger.LogInformation(message, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        private async Task CheckBot(SeleniumOptions options)
        {
            var isBotCheck = options.driver.PageSource.Contains("не робот");

            if (isBotCheck)
            {
                throw new BotExcention("Stuck on bot checker");
            }
        }

        public async Task<List<string>> GetPageLinks(string url, SeleniumOptions options)
        {
            options.driver.Navigate().GoToUrl(url);

            await CheckBot(options);

            var vacancies = options.driver.FindElements(By.CssSelector("[data-qa=\"bloko-header-2\"]"));

            var urls = new List<string>();

            foreach(var vacancy in vacancies)
            {
                var linkElem = vacancy.FindElement(By.CssSelector("a"));
                var link = linkElem.GetAttribute("href");
                urls.Add(link);
                Log("Get " + link);

            }

            return urls;
        }

        public async Task ThrowMessage(string url, SeleniumOptions options)
        {

            options.driver.Navigate().GoToUrl(url);

            await CheckBot(options);

            var isNotClicked = options.driver.FindElements(By.CssSelector("a[data-qa='vacancy-response-link-top']")).Count > 1;

            if (isNotClicked)
            {
                var apply = options.driver.FindElement(By.CssSelector("a[data-qa='vacancy-response-link-top']"));
                apply.Click();

                await Task.Delay(2000);

                var isHaveTest = options.driver.PageSource.Contains("Для отклика на&nbsp;эту вакансию необходимо ответить на&nbsp;несколько вопросов работодателя.");

                if (!isHaveTest)
                {
                    var letterBtnCheck = options.driver.FindElements(By.CssSelector("button[data-qa=\"vacancy-response-letter-toggle\"]")).Count > 0;

                    if (letterBtnCheck)
                    {
                        var letterBtn = options.driver.FindElement(By.CssSelector("button[data-qa=\"vacancy-response-letter-toggle\"]"));
                        letterBtn.Click();

                        var letterBlock = options.driver.FindElement(By.CssSelector("div[data-qa=\"vacancy-response-letter-informer\"]"));

                        var letterTextArea = letterBlock.FindElement(By.CssSelector("textarea"));
                        await Task.Delay(2000);

                        var letter = _hhruEmployeeData.GetLetter();

                        letterTextArea.SendKeys(letter);
                        await Task.Delay(2000);

                        var letterBtnApply = letterBlock.FindElement(By.CssSelector("button"));

                        letterBtnApply.Click();

                        Log("Throw message " + options.driver.Title);
                    }
                }
                else
                {
                    Log("Test on " + url);
                }
            }
        }
    }
}
