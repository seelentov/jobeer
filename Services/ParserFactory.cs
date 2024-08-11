using Jobeer.Interfaces.Services;
using Jobeer.Models;
using Jobeer.Services.Interfaces;
using OpenQA.Selenium;

namespace Jobeer.Services
{
    public class ParserFactory : IFactory<IParser, SearchModelType>
    {
        private readonly HHruService _hhruService;
        private readonly HabrService _habrService;

        public ParserFactory(HHruService hhruService, HabrService habrService)
        {
            _hhruService = hhruService;
            _habrService = habrService;
        }

        public IParser Get(SearchModelType source)
        {
            switch (source)
            {
                case SearchModelType.HHru:
                    return _hhruService;
                case SearchModelType.Habr:
                    return _habrService;
                default:
                    throw new NotFoundException("Неверный тип парсера");
            }
        }
    }
}
