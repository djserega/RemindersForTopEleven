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

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        public ObservableCollection<Models.Match> ListOfMatches { get => _listOfMatches; }

        private void ButtonReadFile_Click(object sender, RoutedEventArgs e)
        {
            _listOfMatches.Clear();

            string errorText = new Reader().ReadData(_listOfMatches);


            #region game friends
            //int countRow = arrText_times.Count();

            //int countRowMatch;
            //if (countRow % 2 == 1)
            //    countRowMatch = (countRow - 1) / 2;
            //else
            //    countRowMatch = countRow / 2;

            //List<string> listOfMatches = new List<string>();

            //int idMatch = 0;

            //for (int i = 0; i < arrText_times.Length; i++)
            //{
            //    string item = arrText_times[i];

            //    if (!string.IsNullOrWhiteSpace(item))
            //    {
            //        if (i < countRowMatch)
            //            listOfMatches.Add(item);
            //        else
            //            listOfMatches[idMatch] += " - " + item;

            //        idMatch++;
            //    }

            //    if (i == countRowMatch)
            //        idMatch = 0;
            //} 
            #endregion

            #region teams
            //string fileNameTeamsHome = "02 - teams home";
            //string[] text_teams_home = new string[3];
            //text_teams_home[0] = new OcrTesseract().GetTextImage(fileNameTeamsHome);
            //text_teams_home[1] = new OcrTesseract().GetTextImage(fileNameTeamsHome, "rus");
            //text_teams_home[2] = new OcrTesseract().GetTextImage(fileNameTeamsHome, "ukr");

            //string fileNameTeamsAway = "02 - teams away";
            //string[] text_teams_away = new string[3];
            //text_teams_away[0] = new OcrTesseract().GetTextImage(fileNameTeamsAway);
            //text_teams_away[1] = new OcrTesseract().GetTextImage(fileNameTeamsAway, "rus");
            //text_teams_away[2] = new OcrTesseract().GetTextImage(fileNameTeamsAway, "ukr"); 
            #endregion

        }
    }
}
