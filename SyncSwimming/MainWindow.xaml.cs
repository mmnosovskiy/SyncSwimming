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

namespace SyncSwimming
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataProcessor.GetList();
            dataGrid.ItemsSource = DataProcessor.list;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid.SelectedItem != null) //Выбран ли элемент?
            {
                //Открытие диалогового окна параметров.
                ScoresWindow scWindow = new ScoresWindow((Participant)dataGrid.SelectedItem);
                scWindow.ShowDialog();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null) //Выбран ли элемент?
            {
                if (MessageBox.Show("Вы уверены? ", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DataProcessor.list.Remove((Participant)dataGrid.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show("Нет выбранного элемента", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NewParticipantWindow newWin = new NewParticipantWindow();
            newWin.ShowDialog();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                NewParticipantWindow newWin = new NewParticipantWindow((Participant)dataGrid.SelectedItem, dataGrid.SelectedIndex);
                newWin.ShowDialog();
            }
            else
            {
                MessageBox.Show("Нет выбранного элемента", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text == string.Empty)
            {
                dataGrid.ItemsSource = DataProcessor.list;
                FoundNumber.Text = "";
            }
            else
            {
                string text = SearchTextBox.Text;
                List<Participant> searchList = new List<Participant>();
                foreach (Participant item in DataProcessor.list)
                {
                    if (item.Contains(text)) searchList.Add(item);
                }
                dataGrid.ItemsSource = searchList;
                FoundNumber.Text = "Найдено эл-тов: " + searchList.Count;
            }
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            DataProcessor.ExportToExcel(DataProcessor.list);
        }
    }
}
