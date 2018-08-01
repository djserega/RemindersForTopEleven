using Telegram.Bot;
using Telegram.Bot.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RemindersForTopEleven
{
    internal class TelegramApi
    {
        private readonly ChatId _chatId = new ChatId("-274625680");

        internal async void SendMessageAsync(string message)
        {
            FileInfo[] fileInfos = new DirectoryInfo($@"{Directory.GetCurrentDirectory()}\data").GetFiles("telegrambotapi.txt");
            if (fileInfos.Count() == 0)
                return;

            FileInfo fileInfo = fileInfos[0];
            string api = new StreamReader(fileInfo.OpenRead()).ReadToEnd();

            TelegramBotClient botClient = new TelegramBotClient(api);
            
            Message result = await botClient.SendTextMessageAsync(_chatId, message);
        }
    }
}
