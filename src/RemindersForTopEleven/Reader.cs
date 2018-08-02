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
        internal List<Models.Match> ReadData()
        {
            List<Models.Match> listOfMatches = new List<Models.Match>();

            DirectoryInfo workDirectory = new DirectoryInfo($@"{Directory.GetCurrentDirectory()}\data");

            ocr = new OcrTesseract
            {
                WorkDirectory = workDirectory.FullName
            };

            DirectoryInfo directoryInfoTeam = new DirectoryInfo($@"{workDirectory.FullName}\teams");

            DirectoryInfo[] directoriesTeams = directoryInfoTeam.GetDirectories();
            foreach (DirectoryInfo directoryTeam in directoriesTeams)
            {
                string team = directoryTeam.Name;

                List<Models.Match> listOfMatchesTeam = new List<Models.Match>();

                FileInfo[] filesDate = directoryTeam.GetFiles("date-??.png", SearchOption.TopDirectoryOnly);
                foreach (FileInfo item in filesDate)
                    ReadMatchDate(listOfMatchesTeam, item.FullName, team);

                int idTime = 0;
                FileInfo[] filesTime = directoryTeam.GetFiles("time-??.png", SearchOption.TopDirectoryOnly);
                foreach (FileInfo item in filesTime)
                    ReadMatchTime(listOfMatchesTeam, item.FullName, ref idTime);

                foreach (Models.Match item in listOfMatchesTeam)
                    listOfMatches.Add(item);
            }

            return listOfMatches;
        }

        private string ReadMatchDate(List<Models.Match> listOfMatches, string fileName, string team)
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

        private string ReadMatchTime(List<Models.Match> listOfMatches, string fileName, ref int idTime)
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
