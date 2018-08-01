using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemindersForTopEleven
{
    internal class Reader
    {
        private OcrTesseract ocr;
        internal string ReadData(ObservableCollection<Models.Match> listOfMatches)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo($@"{Directory.GetCurrentDirectory()}\data");

            ocr = new OcrTesseract
            {
                WorkDirectory = directoryInfo.FullName
            };

            DirectoryInfo[] directoriesTeams = directoryInfo.GetDirectories();
            foreach (DirectoryInfo directoryTeam in directoriesTeams)
            {
                string team = directoryTeam.Name;

                FileInfo[] filesDate = directoryTeam.GetFiles("date-??.png", SearchOption.TopDirectoryOnly);
                foreach (FileInfo item in filesDate)
                    ReadMatchDate(listOfMatches, item.FullName, team);

                int idTime = 0;
                FileInfo[] filesTime = directoryTeam.GetFiles("time-??.png", SearchOption.TopDirectoryOnly);
                foreach (FileInfo item in filesTime)
                    ReadMatchTime(listOfMatches, item.FullName, ref idTime);
            }

            return string.Empty;
        }

        private string ReadMatchDate(ObservableCollection<Models.Match> listOfMatches, string fileName, string team)
        {
            try
            {
                string textDates = ocr.GetTextImage(fileName, "rus");
                string[] arrTextDates = textDates.Trim().Split('\n');

                string currentDay = DateTime.Now.ToString("dd.MM");
                foreach (string item in arrTextDates)
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        string itemDay = item;
                        if (itemDay == "Сегодня")
                            itemDay = currentDay;

                        listOfMatches.Add(new Models.Match(itemDay, team));
                    }
            }
            catch (Exception)
            {
                return "Не удалось обработать файл: " + fileName;
            }

            return string.Empty;
        }

        private string ReadMatchTime(ObservableCollection<Models.Match> listOfMatches, string fileName, ref int idTime)
        {

            try
            {
                string textTimes = ocr.GetTextImage(fileName, "rus");
                string[] arrTextTimes = textTimes.Trim().Split('\n');

                foreach (string item in arrTextTimes)
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        listOfMatches[idTime].Time = item;
                        listOfMatches[idTime++].SetDateTime();
                    }
            }
            catch (Exception)
            {
                return "Не удалось обработать файл: time.png";
            }

            return string.Empty;
        }

    }
}
