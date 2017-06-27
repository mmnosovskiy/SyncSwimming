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
    /// Логика взаимодействия для TrophyWindow.xaml
    /// </summary>
    public partial class TrophyWindow : Window
    {
        Trophy newTrophy;
        int position;
        bool NewEl;
        int playerPos;
        public TrophyWindow()
        {
            NewEl = true;
            InitializeComponent();
            newTrophy = new Trophy(new List<Participant>() { new Participant("") });
            DataContext = newTrophy[0];
        }
        public TrophyWindow(Trophy element, int pos)
        {
            InitializeComponent();
            NewEl = false;
            position = pos;
            newTrophy = element;
            DataContext = newTrophy[0];
            playerPos = 0;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (playerPos > 0)
            {
                DataContext = newTrophy[playerPos - 1];
                newTrophy.Members.RemoveAt(playerPos--);
            }
            else
            {
                if (newTrophy.Members.Count > 1)
                {
                    DataContext = newTrophy[playerPos + 1];
                    newTrophy.Members.RemoveAt(playerPos);
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
                    if (playerPos == newTrophy.Members.Count - 1)
                    {
                        newTrophy.Members.RemoveAt(playerPos);
                        DataContext = newTrophy[--playerPos];
                    }
                    else MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!int.TryParse(NewYear.Text, out year) || year < 1990 || year > 2015)
                {
                    MessageBox.Show("Неверный год рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else DataContext = newTrophy[--playerPos];
            }
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            int year;
            if (playerPos < newTrophy.Members.Count - 1)
            {
                if (NewFio.Text == "" || NewCateg.Text == "" || NewTeam.Text == "" || NewYear.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!int.TryParse(NewYear.Text, out year) || year < 1990 || year > 2015)
                {
                    MessageBox.Show("Неверный год рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else DataContext = newTrophy[++playerPos];
            }
            else if (playerPos == newTrophy.Members.Count - 1 && newTrophy.Members.Count <= 9 && NewFio.Text != "" && NewYear.Text != "" && NewTeam.Text != "" && NewCateg.Text != "")
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
                    newTrophy.Members.Add(new Participant(""));
                    DataContext = newTrophy[playerPos];
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
                    DataProcessor.listTrophy.Add(new Trophy(newTrophy.Members));
                }
                else
                {
                    newTrophy = new Trophy(newTrophy.Members);
                }
                Close();
            }
        }
    }
}
