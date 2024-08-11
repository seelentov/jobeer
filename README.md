## Jobeer - приложения для поиска работы

Jobeer предоставляет возможность запуска автоматизированного бота для поиска работы на площадках HH.ru и Хабр.Карьера.
Легкое для запуска и работы приложение, которым я сам пользуюсь уже на протяжении нескольких месяцев.

# Инструкция к запуску

- Скопировать репозиторий
- Изменить данные в db.db в таблице SearchModels, указав ссылки на поиски с фильтрацией и типы парсера к ним. Ccылки необходимо генерировать через соответствующий сайт (пример: [https://career.habr.com/vacancies?remote=true&skills[]=706&type=all&sort=date](https://chelyabinsk.hh.ru/search/vacancy?text=c%23&salary=100000&schedule=remote&experience=between1And3&ored_clusters=true&excluded_text=ai%2C+python%2C+php%2C+laravel&area=113&hhtmFrom=vacancy_search_list&hhtmFromLabel=vacancy_search_line&order_by=publication_time))
  - Тип данных:
    -  Id (PRIMARY)
    -  LastParse (Дата последнего парсинга по этому поиску, обновляется автоматически, нужна для отслеживания даты последнего поиска вакансий по этому фильтру)
    -  Name (Имя для Вас, не участвует в работе приложения)
    -  Type (Тип парсера, на данный момент: 0 - HH.ru, 1 - Хабр.Карьера)
    -  Url (Ссылка на поиск с фильтрацией, пример: [https://career.habr.com/vacancies?remote=true&skills[]=706&type=all&sort=date](https://chelyabinsk.hh.ru/search/vacancy?text=c%23&salary=100000&schedule=remote&experience=between1And3&ored_clusters=true&excluded_text=ai%2C+python%2C+php%2C+laravel&area=113&hhtmFrom=vacancy_search_list&hhtmFromLabel=vacancy_search_line&order_by=publication_time))
- Изменить данные в appsettings.json:
  - Hide: "{n\y}" (Показывать работу браузера в момент поиска вакансий или скрыть, нужно для проверки работоспособности)
  - TelegramBotToken и TelegramChatId: {string} (Опционально, нужно для получения уведомлений о работе приложения через телеграм-бота, по настройке бота и получению этих данных [подробнее](https://core.telegram.org/bots/api))
  - HHLetter и HabrLetter: {string} (Тут, в формате string, указываете сопроводительное письмо, которое будет отправлено вместе с откликом на вакансию)
  - UserProfileDir: {string} (Тут необходимо указать ссылку на профиль Google Chrome, в котором уже выполнены входы на соответствующих сайтах (hh, хабр))
- Запустить бота

# Заключение

На данный момент некоторые сложности с работой на Хабр.Карьера в связи с капчей, но HH, в связи с прописанными задержками в 30 секунд, работает отлично. Функционал еще на доработке и в скором времени будет решена проблема с капчей-защитой :)
Буду рад получить фидбек от использования моего приложения. Баги, благодарности, вопросы буду ждать по почте <b>komkov222111@gmail.com</b>. Спасибо и удачи в поиске работы :p.
