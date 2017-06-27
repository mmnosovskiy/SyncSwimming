using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CombiWindow.xaml
    /// </summary>
    public partial class CombiWindow : Window
    {
        Combi newCombi;
        int position;
        bool NewEl;
        int playerPos;
        public CombiWindow()
        {
            InitializeComponent();
            NewEl = true;
            newCombi = new Combi(new List<Participant>() { new Participant("") });
            DataContext = newCombi[0];
        }
        public CombiWindow(Combi element, int pos)
        {
            InitializeComponent();
            position = pos;
            newCombi = element;
            NewEl = false;
            DataContext = newCombi[0];
            playerPos = 0;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (playerPos > 0)
            {
                DataContext = newCombi[playerPos - 1];
                newCombi.GroupP.RemoveAt(playerPos--);
            }
            else
            {
                if (newCombi.GroupP.Count > 1)
                {
                    DataContext = newCombi[playerPos + 1];
                    newCombi.GroupP.RemoveAt(playerPos);
                }
            }
        }

        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            int year;
            if (playerPos > 0)
            {
                if (NewFio.Text == "" || NewCateg.Text == "" || NewTeam.Text == "" || NewYear.Text == "")
                {
                    if (playerPos == newCombi.GroupP.Count - 1)
                    {
                        newCombi.GroupP.RemoveAt(playerPos);
                        DataContext = newCombi[--playerPos];
                    }
                    else MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!int.TryParse(NewYear.Text, out year) || year < 1990 || year > 2015)
                {
                    MessageBox.Show("Неверный год рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else DataContext = newCombi[--playerPos];
            }
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            int year;
            if (playerPos < newCombi.GroupP.Count - 1)
            {
                if (NewFio.Text == "" || NewCateg.Text == "" || NewTeam.Text == "" || NewYear.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!int.TryParse(NewYear.Text, out year) || year < 1990 || year > 2015)
                {
                    MessageBox.Show("Неверный год рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else DataContext = newCombi[++playerPos];
            }
            else if (playerPos == newCombi.GroupP.Count - 1 && newCombi.GroupP.Count <= 9 && NewFio.Text != "" && NewYear.Text != "" && NewTeam.Text != "" && NewCateg.Text != "")
            {
                if (NewFio.Text == "" || NewCateg.Text == "" || NewTeam.Text == "" || NewYear.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!int.TryParse(NewYear.Text, out year) || year < 1990 || year > 2015)
                {
                    MessageBox.Show("Неверный год рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    playerPos++;
                    newCombi.GroupP.Add(new Participant(""));
                    DataContext = newCombi[playerPos];
                }
            }
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
                MessageBox.Show("Неверный год рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (NewEl)
                {
                    DataProcessor.listCombi.Add(new Combi(newCombi.GroupP));
                }
                else
                {
                    newCombi = new Combi(newCombi.GroupP);
                }
                Close();
            }
        }
    }
}
