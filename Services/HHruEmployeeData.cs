﻿
using Jobeer.Exceptions;
using Jobeer.Interfaces.Services;

namespace Jobeer.Services
{
    public class HHruEmployeeData : EmployeeData
    {
        private string _letter = "Full Stack разработчик.\r\nNext(React)/React Native + ASP.NET Core \r\nhttps://github.com/seelentov/\r\n\r\nЗанимаюсь изучением и практикой в сфере веб-технологий и нативных мобильных приложений.\r\nНа данный момент в IT 1,5 года, год из которых работаю над проектами под заказ, небольшие правки или разработка от реализации до публикации.\r\n\r\n    Языки Программирования\r\n- JavaScript\r\n- TypeScript\r\n- C#\r\n- Python\r\n\r\n    Frontend Технологии\r\n- React.js (Next.js)\r\n- React Native\r\n- Vue.js\r\n\r\n    Backend Технологии\r\n- ASP.NET Core\r\n- Redis\r\n- Nginx\r\n- PostgreSQL\r\n- MongoDB\r\n\r\nЛитература:\r\n\r\n- Чистый код: создание, анализ и рефакторинг. Библиотека программиста | Мартин Роберт\r\n\r\n- Чистая архитектура. Искусство разработки программного обеспечения | Мартин Роберт\r\n\r\n- UX/UI дизайн для создания идеального продукта. Полный и исчерпывающий гид | Шуваев Ярослав Александрович \r\n\r\n- Теоретический минимум по Computer Science. Все что нужно программисту и разработчику | Фило Владстон Феррейра \r\n\r\n- Грокаем алгоритмы. Иллюстрированное пособие для программистов и любопытствующих | Бхаргава Адитья \r\n\r\n- Паттерны объектно-ориентированного проектирования | Гамма Э., Хелм Р., Джонсон Р., Влиссидес Д.\r\n\r\nРаботал над проектами:\r\n\r\n- https://github.com/seelentov/cardscore-native\r\n- https://github.com/seelentov/cardscore-api\r\nМобильное приложение Cardscore для отслеживания событий в матчах 40-ка лиг, с гибкой настройкой уведомлений.\r\nFrontend: React Native\r\nBackend: ASP.NET Core, Redis, PostgreSQL\r\n\r\n- https://pixellperfect.ru/\r\nЛичный сайт. Полный цикл разработки. Первичный дизайн + frontend + backend + публикация на vps с настройкой виртуальной машины\r\nFrontend: Next.js\r\nBackend: Strapi, Redis, PostgreSQL\r\n\r\n- https://первыйзагородный.рф/\r\nСайт для риелтора ООО\"АБСОЛЮТИНВЕСТ\". Личный заказ. Разработка от верстки до публикации на домен(reg.ru)\r\nCMS: MODx\r\n\r\n- https://sanchosrest.ru/\r\nРазработка от верстки до размещении на CMS WordPress и формированию админки для удобного редактирования информации на сайте клиентом\r\nCMS: WordPress\r\n\r\n- https://seelentov.github.io/albeco.pro-home/\r\n- https://seelentov.github.io/stroiecoreshenie.ru/\r\nДва проекта, работал на версткой и посадкой на CMS MODx, на данный момент проекты неактивны, выгрузил верстку на Github Pages.\r\n\r\n";

        public HHruEmployeeData()
        {

        }


        public string GetLetter()
        {
            return _letter;
        }

    }

}
