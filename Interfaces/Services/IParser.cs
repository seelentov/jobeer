using static Jobber.Services.SeleniumFactory;

namespace Jobeer.Interfaces.Services
{
    public interface IParser
    {
        public Task<List<string>> GetPageLinks(string url, SeleniumOptions options);
        public Task ThrowMessage(string url, SeleniumOptions options);
    }
}
