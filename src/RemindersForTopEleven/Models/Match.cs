using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemindersForTopEleven.Models
{
    public class Match : NotifyPropertyChangedClass
    {
        private readonly TimeSpan _timeSpanMax = new TimeSpan(0, 3, 0); //TimeSpan timeSpanMax = new TimeSpan(0, 15, 0);
        private readonly TimeSpan _timeSpanMin = new TimeSpan(0, 0, 0);

        private string _date;
        private string _time;
        private string _team;
        private DateTime _dateTime;
        private bool _currentDay;

        public Match(string date)
        {
            _date = date;
        }

        public Match(string date, string team)
        {
            _date = date;
            _team = team;
        }

        public string Date { get => _date; set { _date = value; NotifyPropertyChanged(); } }
        public string Time { get => _time; set { _time = value; NotifyPropertyChanged(); } }
        public string Team { get => _team; set { _team = value; NotifyPropertyChanged(); } }
        public DateTime DateTime { get => _dateTime; set { _dateTime = value; NotifyPropertyChanged(); SetCurrentDay(); } }
        public bool CurrentDay { get => _currentDay; set { _currentDay = value; NotifyPropertyChanged(); } }

        internal int CountMinutesBeforeBeginningMatch { get => GetCountMinutesBeforeBeginningMatch(); }

        internal void SetDateTime()
        {
            DateTime = new DateTime(
                DateTime.Now.Year,
                GetInt(_date.Substring(3)),
                GetInt(_date.Substring(0, 2)),
                GetInt(_time.Substring(0, 2)),
                GetInt(_time.Substring(3)),
                0);
        }

        private int GetInt(string text)
            => int.Parse(text);

        private void SetCurrentDay()
            => CurrentDay = DateTime.Now.Date == _dateTime.Date;

        internal int GetCountMinutesBeforeBeginningMatch()
        {
            TimeSpan currentTimeSpan = _dateTime.Subtract(DateTime.Now);

            if (_timeSpanMax > currentTimeSpan && currentTimeSpan > _timeSpanMin)
            {
                int minutes = currentTimeSpan.Minutes;

                if (
                    //minutes == 15 ||
                    //minutes == 10 ||
                    //minutes == 5 ||
                    minutes == 3 || 
                    minutes == 2 ||
                    minutes == 1 ||
                    minutes == 0)
                    return minutes;
            }
            return 0;
        }

        internal string GetMessageOnBeginingMatch()
        {
            string message = $"Гра у команди {_team}. ";

            switch (CountMinutesBeforeBeginningMatch)
            {
                //case 15:
                //    message += $"Початок о {_dateTime.ToString("HH:mm")}";
                //    break;
                //case 10:
                //    message += $"Початок за 10 хв";
                //    break;
                //case 5:
                //    message += $"Початок за 5 хв";
                //    break;
                case 3:
                    message += $"Початок за 3 хв";
                    break;
                case 2:
                    message += $"Початок за 2 хв";
                    break;
                case 1:
                    message = $"Гравці {_team} вже виходять на поле";
                    break;
            }
            return message;
        }

    }
}
                                                           