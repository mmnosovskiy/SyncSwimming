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
using System.Windows.Shapes;

namespace SyncSwimming
{
    /// <summary>
    /// Логика взаимодействия для NewParticipant.xaml
    /// </summary>
    public partial class ParticipantWindow : Window
    {
        Participant newParticipant;
        bool NewEl;
        int position;
        public ParticipantWindow()
        {
            InitializeComponent();
            newParticipant = new Participant();
            NewEl = true;
        }
        public ParticipantWindow(Participant element, int pos)
        {
            InitializeComponent();
            position = pos;
            newParticipant = element;
            DataContext = newParticipant;
            NewEl = false;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            int year;
            if (NewFio.Text == "" || NewCateg.Text == "" || NewTeam.Text == "" || NewYear.Text == "")
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!int.TryParse(NewYear.Text, out year) || year < 1990 || year > 2015)
            {
                MessageBox.Show("Невыерный год рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                newParticipant.FIO = NewFio.Text;
                newParticipant.Category = NewCateg.Text;
                newParticipant.Team = NewTeam.Text;
                newParticipant.Year = year;
                if (NewEl) ((ObservableCollection<Participant>)DataProcessor.Current).Add(newParticipant);
                else ((ObservableCollection<Participant>)DataProcessor.Current)[position] = newParticipant;
                Close();
            }
        }
    }
}
