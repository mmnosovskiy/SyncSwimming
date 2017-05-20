﻿using System;
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
using Excel = Microsoft.Office.Interop.Excel;
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
            DataProcessor.listGroup = DataProcessor.GetList8("ListGroup.txt");
            DataProcessor.listCombi = DataProcessor.GetList8("ListCombi.txt");
            DataProcessor.listTrophy = DataProcessor.GetList("ListTrophy.txt");
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
                else if (DataProcessor.Current == DataProcessor.listGroup || DataProcessor.Current == DataProcessor.listCombi)
                {
                    ScoresGroupWindow scWindow = new ScoresGroupWindow((Group)dataGrid.SelectedItem);
                    scWindow.ShowDialog();
                }
                else if (DataProcessor.Current == DataProcessor.listTrophy)
                {
                    ScoresTrophyWindow scWindow = new ScoresTrophyWindow((Trophy)dataGrid.SelectedItem);
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
                    else if (DataProcessor.Current == DataProcessor.listGroup || DataProcessor.Current == DataProcessor.listCombi)
                        ((ObservableCollection<Group>)DataProcessor.Current).Remove((Group)dataGrid.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show("Нет выбранного элемента", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (DataProcessor.Current == DataProcessor.listOP8 || DataProcessor.Current == DataProcessor.listOP12 || DataProcessor.Current == DataProcessor.listOP13_15 || DataProcessor.Current == DataProcessor.listSolo8 || DataProcessor.Current == DataProcessor.listSolo12 || DataProcessor.Current == DataProcessor.listSolo13_15)
            {
                ParticipantWindow newWin = new ParticipantWindow();
                newWin.ShowDialog();
            }
            else if (DataProcessor.Current == DataProcessor.listDuo8 || DataProcessor.Current == DataProcessor.listDuo12 || DataProcessor.Current == DataProcessor.listDuo13_15)
            {
                DuoWindow newWin = new DuoWindow();
                newWin.ShowDialog();
            }
            else if (DataProcessor.Current == DataProcessor.listGroup || DataProcessor.Current == DataProcessor.listCombi)
            {
                GroupWindow newWin = new GroupWindow();
                newWin.ShowDialog();
            }
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
                else if (DataProcessor.Current == DataProcessor.listGroup || DataProcessor.Current == DataProcessor.listCombi)
                {
                    GroupWindow scWindow = new GroupWindow((Group)dataGrid.SelectedItem, dataGrid.SelectedIndex);
                    scWindow.ShowDialog();
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
                else if (DataProcessor.Current == DataProcessor.listGroup || DataProcessor.Current == DataProcessor.listCombi)
                {
                    List<Group> searchList = new List<Group>();
                    foreach (Group item in DataProcessor.Current)
                    {
                        if (item.Contains(text)) searchList.Add(item);
                    }
                    dataGrid.ItemsSource = searchList;
                    FoundNumber.Text = "Найдено эл-тов: " + searchList.Count;
                }
                else if (DataProcessor.Current == DataProcessor.listTrophy)
                {
                    List<Trophy> searchList = new List<Trophy>();
                    foreach (Trophy item in DataProcessor.Current)
                    {
                        if (item.Contains(text)) searchList.Add(item);
                    }
                    dataGrid.ItemsSource = searchList;
                    FoundNumber.Text = "Найдено эл-тов: " + searchList.Count;
                }



            }
        }
        static async void ExcelAsync()
        {
            if (DataProcessor.Current == DataProcessor.listOP8) await DataProcessor.ExportToExcel((ObservableCollection<Participant>)DataProcessor.Current, "ОП 8 и м");
            else if (DataProcessor.Current == DataProcessor.listOP12) await DataProcessor.ExportToExcel((ObservableCollection<Participant>)DataProcessor.Current, "ОП 12 и м");
            else if (DataProcessor.Current == DataProcessor.listOP13_15) await DataProcessor.ExportToExcel((ObservableCollection<Participant>)DataProcessor.Current, "ОП 13-15");
            else if (DataProcessor.Current == DataProcessor.listSolo8) await DataProcessor.ExportToExcel((ObservableCollection<Participant>)DataProcessor.Current, "Соло 8 и м");
            else if (DataProcessor.Current == DataProcessor.listSolo12) await DataProcessor.ExportToExcel((ObservableCollection<Participant>)DataProcessor.Current, "Соло 12 и м");
            else if (DataProcessor.Current == DataProcessor.listSolo13_15) await DataProcessor.ExportToExcel((ObservableCollection<Participant>)DataProcessor.Current, "Соло 13-15");
            else if (DataProcessor.Current == DataProcessor.listDuo8) await DataProcessor.ExportToExcel((ObservableCollection<Duo>)DataProcessor.Current, "Дуэт 8 и м");
            else if (DataProcessor.Current == DataProcessor.listDuo12) await DataProcessor.ExportToExcel((ObservableCollection<Duo>)DataProcessor.Current, "Дуэт 12 и м");
            else if (DataProcessor.Current == DataProcessor.listDuo13_15) await DataProcessor.ExportToExcel((ObservableCollection<Duo>)DataProcessor.Current, "Дуэт 13-15");
            else if (DataProcessor.Current == DataProcessor.listGroup) await DataProcessor.ExportToExcel((ObservableCollection<Group>)DataProcessor.Current, "Группа");
            else if (DataProcessor.Current == DataProcessor.listCombi) await DataProcessor.ExportToExcel((ObservableCollection<Group>)DataProcessor.Current, "Комби");
            else if (DataProcessor.Current == DataProcessor.listTrophy) await DataProcessor.ExportToExcel((ObservableCollection<Trophy>)DataProcessor.Current, "Трофи");
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            prBar.Visibility = Visibility.Visible;
            ExcelAsync();
            prBar.Visibility = Visibility.Hidden;
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

        private void Window_Closed(object sender, EventArgs e)
        {
            DataProcessor.excelApp.Visible = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы уверены? Будет открыт Excel.", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        static Task ExcelAll()
        {
            return Task.Run(() =>
            {
                DataProcessor.ExportToExcel(DataProcessor.listOP8, "ОП 8 и м");
                DataProcessor.ExportToExcel(DataProcessor.listOP12, "ОП 12 и м");
                DataProcessor.ExportToExcel(DataProcessor.listOP13_15, "ОП 13-15");
                DataProcessor.ExportToExcel(DataProcessor.listSolo8, "Соло 8 и м");
                DataProcessor.ExportToExcel(DataProcessor.listSolo12, "Соло 12 и м");
                DataProcessor.ExportToExcel(DataProcessor.listSolo13_15, "Соло 13-15");
                DataProcessor.ExportToExcel(DataProcessor.listDuo8, "Дуэт 8 и м");
                DataProcessor.ExportToExcel(DataProcessor.listDuo12, "Дуэт 12 и м");
                DataProcessor.ExportToExcel(DataProcessor.listDuo13_15, "Дуэт 13-15");
                DataProcessor.ExportToExcel(DataProcessor.listGroup, "Группа");
                DataProcessor.ExportToExcel(DataProcessor.listCombi, "Комби");
                DataProcessor.ExportToExcel(DataProcessor.listTrophy, "Трофи");
            });
        }
        static async void ExcelAllAsync()
        {
            await ExcelAll();
        }

        private void ButtonExportAll_Click(object sender, RoutedEventArgs e)
        {
            prBar.Visibility = Visibility.Visible;
            ExcelAllAsync();
            prBar.Visibility = Visibility.Hidden;
        }
    }
}
