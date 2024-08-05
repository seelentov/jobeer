using OpenQA.Selenium;
using static Jobber.Services.SeleniumFactory;

namespace Jobeer.Interfaces.Services
{
    public interface ISenderService
    {
        public Task OpenPage(string url, SeleniumOptions options);
        public Task ThrowMessages(SeleniumOptions options);
        public Task<bool> SwitchPage(SeleniumOptions options);
    }
}
