using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemindersForTopEleven
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Models.Match> _listOfMatches = new ObservableCollection<Models.Match>();
        private readonly Reminders _reminders;
        private readonly StartMatchEvents _startMatchEvents;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            _startMatchEvents = new StartMatchEvents();
            _startMatchEvents.StartMatch += _startMatchEvents_StartMatch;
            _reminders = new Reminders(_startMatchEvents);
        }

        public ObservableCollection<Models.Match> ListOfMatches { get => _listOfMatches; }

        private void _startMatchEvents_StartMatch()
        {
            foreach (Models.Match item in GetListOfBeginningMatches())
                SendMessageOnBeginingMatch(item);
        }

        private List<Models.Match> GetListOfBeginningMatches()
        {
            List<Models.Match> list = new List<Models.Match>();

            foreach (Models.Match item in _listOfMatches)
                if (item.CountMinutesBeforeBeginningMatch > 0)
                    list.Add(item);

            return list;
        }

        private async void ButtonReadFile_Click(object sender, RoutedEventArgs e)
        {
            _listOfMatches.Clear();

            List<Models.Match> listOfMatches = await ReadDataAsync();
            foreach (Models.Match item in listOfMatches)
                _listOfMatches.Add(item);
        }

        private async Task<List<Models.Match>> ReadDataAsync()
            => await Task.Run(() => { return new Reader().ReadData(); });

        private void ButtonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridListMatches.SelectedItem is Models.Match match)
                SendMessageOnBeginingMatch(match);
        }

        private void SendMessageOnBeginingMatch(Models.Match match)
            => new TelegramApi().SendMessageAsync(match.GetMessageOnBeginingMatch());

        private void ButtonStartTimer_Click(object sender, RoutedEventArgs e)
        {
            if (_listOfMatches.Count() > 0)
                _reminders.StartTimer();
        }

        private void ButtonStopTimer_Click(object sender, RoutedEventArgs e)
        {
            _reminders.StopTimer();
        }
    }
}
