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

            new Reader().ReadData(_listOfMatches);
        }
    }
}
