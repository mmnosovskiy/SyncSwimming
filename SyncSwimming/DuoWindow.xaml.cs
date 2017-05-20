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
    /// Логика взаимодействия для DuoWindow.xaml
    /// </summary>
    public partial class DuoWindow : Window
    {
        Duo newDuo;
        bool NewEl;
        int position;
        public DuoWindow()
        {
            InitializeComponent();
            NewEl = true;
        }
        public DuoWindow(Duo element, int pos)
        {
            InitializeComponent();
            position = pos;
            newDuo = element;
            DataContext = newDuo;
            NewEl = false;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            int year1, year2;
            if (NewFio1.Text == "" || NewCateg1.Text == "" || NewTeam1.Text == "" || NewYear1.Text == "" || NewFio2.Text == "" || NewCateg2.Text == "" || NewTeam2.Text == "" || NewYear2.Text == "")
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!int.TryParse(NewYear1.Text, out year1) || year1 < 1990 || year1 > 2015 || !int.TryParse(NewYear2.Text, out year2) || year2 < 1990 || year2 > 2015)
            {
                MessageBox.Show("Неверный год рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                if (NewEl)
                {
                    Participant d1 = new Participant();
                    Participant d2 = new Participant();
                    d1.FIO = NewFio1.Text;
                    d2.FIO = NewFio2.Text;
                    d1.Category = NewCateg1.Text;
                    d2.Category = NewCateg2.Text;
                    d1.Year = year1;
                    d2.Year = year2;
                    d1.Team = NewTeam1.Text;
                    d2.Team = NewTeam2.Text;
                    newDuo = new Duo(d1, d2);
                    ((ObservableCollection<Duo>)DataProcessor.Current).Add(newDuo);
                }
                else
                {
                    newDuo.Duo1.FIO = NewFio1.Text;
                    newDuo.Duo2.FIO = NewFio2.Text;
                    newDuo.Duo1.Category = NewCateg1.Text;
                    newDuo.Duo2.Category = NewCateg2.Text;
                    newDuo.Duo1.Year = year1;
                    newDuo.Duo2.Year = year2;
                    newDuo.Duo1.Team = NewTeam1.Text;
                    newDuo.Duo2.Team = NewTeam2.Text;
                    ((ObservableCollection<Duo>)DataProcessor.Current)[position] = new Duo(newDuo.Duo1, newDuo.Duo2);
                }
                Close();
            }
        }
    }
}
