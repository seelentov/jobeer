﻿
using Jobeer.Exceptions;
using Jobeer.Interfaces.Services;
using Telegram.Bot.Polling;
using Telegram.Bot;
using System.Text;

namespace Jobeer.Services
{
    public class TelegramService
    {
        private readonly string _botToken;
        private readonly string _chatId;

        public TelegramService(IConfiguration configuration)
        {
            _botToken = configuration["TelegramBotToken"];
            _chatId = configuration["TelegramChatId"];
        }

        public async Task SendMessage(string message, string header = "")
        {
            using (var client = new HttpClient())
            {
                var formatMessage = message.Replace("{", "[").Replace("}", "]").Replace("\"", "");

                var headerRender = header == "" ? "" : header + "\n\n";

                var requestUri = $"https://api.telegram.org/bot{_botToken}/sendMessage";
                var content = new StringContent(
                    $"{{\"chat_id\": {_chatId}, \"text\": \"{headerRender}{formatMessage}\"}}",
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(requestUri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ошибка отправки сообщения в Telegram. Код ошибки: {response.StatusCode}");
                }
            }
        }
    }

}
