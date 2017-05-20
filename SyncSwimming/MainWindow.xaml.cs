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
        Button pressedButton;
        public MainWindow()
        {
            InitializeComponent();
            OP8.IsEnabled = false;
            pressedButton = OP8;
            DataProcessor.listOP8 = DataProcessor.GetList1("ListOP8.txt");
            DataProcessor.listOP12 = DataProcessor.GetList1("ListOP12.txt");
            DataProcessor.listOP13_15 = DataProcessor.GetList1("ListOP13_15.txt");
            DataProcessor.listSolo8 = DataProcessor.GetList1("ListSolo8.txt");
            DataProcessor.listSolo12 = DataProcessor.GetList1("ListSolo12.txt");
            DataProcessor.listSolo13_15 = DataProcessor.GetList1("ListSolo13_15.txt");
            DataProcessor.listDuo8 = DataProcessor.GetList2("ListDuo8.txt");
            DataProcessor.listDuo12 = DataProcessor.GetList2("ListDuo12.txt");
            DataProcessor.listDuo13_15 = DataProcessor.GetList2("ListDuo13_15.txt");
            //DataProcessor.listGroup = DataProcessor.GetList("ListGroup.txt");
            //DataProcessor.listCombi = DataProcessor.GetList("ListCombi.txt");
            //DataProcessor.listTrophy = DataProcessor.GetList("ListTrophy.txt");
            DataProcessor.Current = DataProcessor.listOP8;
            dataGrid.ItemsSource = DataProcessor.Current;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid.SelectedItem != null) //Выбран ли элемент?
            {
                if (DataProcessor.Current == DataProcessor.listOP8 || DataProcessor.Current == DataProcessor.listOP12 || DataProcessor.Current == DataProcessor.listOP13_15)
                {
                    ScoresOPWindow scWindow = new ScoresOPWindow((Participant)dataGrid.SelectedItem);
                    scWindow.ShowDialog();
                }
                else if (DataProcessor.Current == DataProcessor.listSolo8 || DataProcessor.Current == DataProcessor.listSolo12 || DataProcessor.Current == DataProcessor.listSolo13_15)
                {
                    ScoresPPWindow scWindow = new ScoresPPWindow((Participant)dataGrid.SelectedItem);
                    scWindow.ShowDialog();
                }
                else if (DataProcessor.Current == DataProcessor.listDuo8 || DataProcessor.Current == DataProcessor.listDuo12 || DataProcessor.Current == DataProcessor.listDuo13_15)
                {
                    ScoresDuoWindow scWindow = new ScoresDuoWindow((Duo)dataGrid.SelectedItem);
                    scWindow.ShowDialog();
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null) //Выбран ли элемент?
            {
                if (MessageBox.Show("Вы уверены? ", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (DataProcessor.Current == DataProcessor.listOP8 || DataProcessor.Current == DataProcessor.listOP12 || DataProcessor.Current == DataProcessor.listOP13_15 || DataProcessor.Current == DataProcessor.listSolo8 || DataProcessor.Current == DataProcessor.listSolo12 || DataProcessor.Current == DataProcessor.listSolo13_15)
                        ((ObservableCollection<Participant>)DataProcessor.Current).Remove((Participant)dataGrid.SelectedItem);
                    else if (DataProcessor.Current == DataProcessor.listDuo8 || DataProcessor.Current == DataProcessor.listDuo12 || DataProcessor.Current == DataProcessor.listDuo13_15)
                        ((ObservableCollection<Duo>)DataProcessor.Current).Remove((Duo)dataGrid.SelectedItem);

                }
            }
            else
            {
                MessageBox.Show("Нет выбранного элемента", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ParticipantWindow newWin = new ParticipantWindow();
            newWin.ShowDialog();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                if (DataProcessor.Current == DataProcessor.listOP8 || DataProcessor.Current == DataProcessor.listOP12 || DataProcessor.Current == DataProcessor.listOP13_15 || DataProcessor.Current == DataProcessor.listSolo8 || DataProcessor.Current == DataProcessor.listSolo12 || DataProcessor.Current == DataProcessor.listSolo13_15)
                {
                    ParticipantWindow newWin = new ParticipantWindow((Participant)dataGrid.SelectedItem, dataGrid.SelectedIndex);
                    newWin.ShowDialog();
                }
                else if (DataProcessor.Current == DataProcessor.listDuo8 || DataProcessor.Current == DataProcessor.listDuo12 || DataProcessor.Current == DataProcessor.listDuo13_15)
                {
                    DuoWindow newWin = new DuoWindow((Duo)dataGrid.SelectedItem, dataGrid.SelectedIndex);
                    newWin.ShowDialog();
                }
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
                dataGrid.ItemsSource = DataProcessor.Current;
                FoundNumber.Text = "";
            }
            else
            {
                string text = SearchTextBox.Text;
                if (DataProcessor.Current == DataProcessor.listOP8 || DataProcessor.Current == DataProcessor.listOP12 || DataProcessor.Current == DataProcessor.listOP13_15 || DataProcessor.Current == DataProcessor.listSolo8 || DataProcessor.Current == DataProcessor.listSolo12 || DataProcessor.Current == DataProcessor.listSolo13_15)
                {
                    List<Participant> searchList = new List<Participant>();
                    foreach (Participant item in DataProcessor.Current)
                    {
                        if (item.Contains(text)) searchList.Add(item);
                    }
                    dataGrid.ItemsSource = searchList;
                    FoundNumber.Text = "Найдено эл-тов: " + searchList.Count;
                }
                else if (DataProcessor.Current == DataProcessor.listDuo8 || DataProcessor.Current == DataProcessor.listDuo12 || DataProcessor.Current == DataProcessor.listDuo13_15)
                {
                    List<Duo> searchList = new List<Duo>();
                    foreach (Duo item in DataProcessor.Current)
                    {
                        if (item.Contains(text)) searchList.Add(item);
                    }
                    dataGrid.ItemsSource = searchList;
                    FoundNumber.Text = "Найдено эл-тов: " + searchList.Count;
                }
                
                
            }
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            if (DataProcessor.Current == DataProcessor.listOP8 || DataProcessor.Current == DataProcessor.listOP12 || DataProcessor.Current == DataProcessor.listOP13_15 || DataProcessor.Current == DataProcessor.listSolo8 || DataProcessor.Current == DataProcessor.listSolo12 || DataProcessor.Current == DataProcessor.listSolo13_15)
                DataProcessor.ExportToExcel(((ObservableCollection<Participant>)DataProcessor.Current));
            else if (DataProcessor.Current == DataProcessor.listDuo8 || DataProcessor.Current == DataProcessor.listDuo12 || DataProcessor.Current == DataProcessor.listDuo13_15)
                DataProcessor.ExportToExcel(((ObservableCollection<Duo>)DataProcessor.Current));
        }

        private void ChangeList_Click(object sender, RoutedEventArgs e)
        {
            pressedButton.IsEnabled = true;
            Button current = (Button)sender;
            current.IsEnabled = false;
            pressedButton = current;
            switch (current.Name)
            {
                case "OP8":
                    DataProcessor.Current = DataProcessor.listOP8;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
                case "OP12":
                    DataProcessor.Current = DataProcessor.listOP12;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
                case "OP13_15":
                    DataProcessor.Current = DataProcessor.listOP13_15;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
                case "Solo8":
                    DataProcessor.Current = DataProcessor.listSolo8;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
                case "Solo12":
                    DataProcessor.Current = DataProcessor.listSolo12;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
                case "Solo13_15":
                    DataProcessor.Current = DataProcessor.listSolo13_15;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
                case "Duo8":
                    DataProcessor.Current = DataProcessor.listDuo8;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
                case "Duo12":
                    DataProcessor.Current = DataProcessor.listDuo12;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
                case "Duo13_15":
                    DataProcessor.Current = DataProcessor.listDuo13_15;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
                case "Group":
                    DataProcessor.Current = DataProcessor.listGroup;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
                case "Combi":
                    DataProcessor.Current = DataProcessor.listCombi;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
                case "Trophy":
                    DataProcessor.Current = DataProcessor.listTrophy;
                    dataGrid.ItemsSource = DataProcessor.Current;
                    break;
            }
        }
    }
}
