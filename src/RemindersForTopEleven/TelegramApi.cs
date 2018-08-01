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
        private string _api;
        private ChatId _chatId;

        internal TelegramApi()
        {
            SetApi();
            SetChatID();
        }

        internal async void SendMessageAsync(string message)
        {
            if (_api == null || _chatId == null)
                return;

            TelegramBotClient botClient = new TelegramBotClient(_api);

            Message result = await botClient.SendTextMessageAsync(_chatId, message);
        }

        private void SetApi()
        {
            try
            {
                FileInfo[] fileInfos = new DirectoryInfo($@"{Directory.GetCurrentDirectory()}\data").GetFiles("telegrambotapi.txt");
                if (fileInfos.Count() == 0)
                    return;

                _api = new StreamReader(fileInfos[0].OpenRead()).ReadToEnd();
            }
            catch (Exception)
            {
            }
        }

        private void SetChatID()
        {
            try
            {
                FileInfo[] fileInfos = new DirectoryInfo($@"{Directory.GetCurrentDirectory()}\data").GetFiles("chatid.txt");
                if (fileInfos.Count() == 0)
                    return;

                FileInfo fileInfo = fileInfos[0];
                _chatId = new ChatId(new StreamReader(fileInfo.OpenRead()).ReadToEnd());
            }
            catch (Exception)
            {
            }
        }
    }
}
