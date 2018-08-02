﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemindersForTopEleven.Models
{
    public class Match : NotifyPropertyChangedClass
    {
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

        internal string GetMessageOnBeginingMatch()
            => $"Скоро гра у команди {_team}. Початок о {_dateTime.ToString("dd.MM HH:mm")}";

    }
}
                                                           