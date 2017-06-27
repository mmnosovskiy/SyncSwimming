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
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        Group newGroup;
        bool NewEl;
        int position;
        public GroupWindow()
        {
            InitializeComponent();
            NewEl = true;
        }
        public GroupWindow(Group element, int pos)
        {
            InitializeComponent();
            position = pos;
            newGroup = element;
            DataContext = newGroup;
            NewEl = false;
        }
        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            int year1, year2, year3, year4, year5, year6, year7, year8;
            if (NewFio1.Text == "" || NewCateg1.Text == "" || NewTeam1.Text == "" || NewYear1.Text == ""
                || NewFio2.Text == "" || NewCateg2.Text == "" || NewTeam2.Text == "" || NewYear2.Text == ""
                || NewFio3.Text == "" || NewCateg3.Text == "" || NewTeam3.Text == "" || NewYear3.Text == ""
                || NewFio4.Text == "" || NewCateg4.Text == "" || NewTeam4.Text == "" || NewYear4.Text == ""
                || NewFio5.Text == "" || NewCateg5.Text == "" || NewTeam5.Text == "" || NewYear5.Text == ""
                || NewFio6.Text == "" || NewCateg6.Text == "" || NewTeam6.Text == "" || NewYear6.Text == ""
                || NewFio7.Text == "" || NewCateg7.Text == "" || NewTeam7.Text == "" || NewYear7.Text == ""
                || NewFio8.Text == "" || NewCateg8.Text == "" || NewTeam8.Text == "" || NewYear8.Text == "")
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!int.TryParse(NewYear1.Text, out year1) || year1 < 1990 || year1 > 2015
                    || !int.TryParse(NewYear2.Text, out year2) || year2 < 1990 || year2 > 2015
                    || !int.TryParse(NewYear3.Text, out year3) || year3 < 1990 || year3 > 2015
                    || !int.TryParse(NewYear4.Text, out year4) || year4 < 1990 || year4 > 2015
                    || !int.TryParse(NewYear5.Text, out year5) || year5 < 1990 || year5 > 2015
                    || !int.TryParse(NewYear6.Text, out year6) || year6 < 1990 || year6 > 2015
                    || !int.TryParse(NewYear7.Text, out year7) || year7 < 1990 || year7 > 2015
                    || !int.TryParse(NewYear8.Text, out year8) || year8 < 1990 || year8 > 2015)
            {
                MessageBox.Show("Неверный год рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                if (NewEl)
                {
                    Participant p1 = new Participant("");
                    Participant p2 = new Participant("");
                    Participant p3 = new Participant("");
                    Participant p4 = new Participant("");
                    Participant p5 = new Participant("");
                    Participant p6 = new Participant("");
                    Participant p7 = new Participant("");
                    Participant p8 = new Participant("");
                    p1.FIO = NewFio1.Text;
                    p2.FIO = NewFio2.Text;
                    p3.FIO = NewFio3.Text;
                    p4.FIO = NewFio4.Text;
                    p5.FIO = NewFio5.Text;
                    p6.FIO = NewFio6.Text;
                    p7.FIO = NewFio7.Text;
                    p8.FIO = NewFio8.Text;
                    p1.Category = NewCateg1.Text;
                    p2.Category = NewCateg2.Text;
                    p3.Category = NewCateg3.Text;
                    p4.Category = NewCateg4.Text;
                    p5.Category = NewCateg5.Text;
                    p6.Category = NewCateg6.Text;
                    p7.Category = NewCateg7.Text;
                    p8.Category = NewCateg8.Text;
                    p1.Year = year1;
                    p2.Year = year2;
                    p3.Year = year3;
                    p4.Year = year4;
                    p5.Year = year5;
                    p6.Year = year6;
                    p7.Year = year7;
                    p8.Year = year8;
                    p1.Team = NewTeam1.Text;
                    p2.Team = NewTeam2.Text;
                    p3.Team = NewTeam3.Text;
                    p4.Team = NewTeam4.Text;
                    p5.Team = NewTeam5.Text;
                    p6.Team = NewTeam6.Text;
                    p7.Team = NewTeam7.Text;
                    p8.Team = NewTeam8.Text;
                    newGroup = new Group(new Participant[8] { p1, p2, p3, p4, p5, p6, p7, p8 });
                    ((ObservableCollection<Group>)DataProcessor.Current).Add(newGroup);
                }
                else
                {
                    Participant p1 = new Participant("");
                    Participant p2 = new Participant("");
                    Participant p3 = new Participant("");
                    Participant p4 = new Participant("");
                    Participant p5 = new Participant("");
                    Participant p6 = new Participant("");
                    Participant p7 = new Participant("");
                    Participant p8 = new Participant("");
                    p1.FIO = NewFio1.Text;
                    p2.FIO = NewFio2.Text;
                    p3.FIO = NewFio3.Text;
                    p4.FIO = NewFio4.Text;
                    p5.FIO = NewFio5.Text;
                    p6.FIO = NewFio6.Text;
                    p7.FIO = NewFio7.Text;
                    p8.FIO = NewFio8.Text;
                    p1.Category = NewCateg1.Text;
                    p2.Category = NewCateg2.Text;
                    p3.Category = NewCateg3.Text;
                    p4.Category = NewCateg4.Text;
                    p5.Category = NewCateg5.Text;
                    p6.Category = NewCateg6.Text;
                    p7.Category = NewCateg7.Text;
                    p8.Category = NewCateg8.Text;
                    p1.Year = year1;
                    p2.Year = year2;
                    p3.Year = year3;
                    p4.Year = year4;
                    p5.Year = year5;
                    p6.Year = year6;
                    p7.Year = year7;
                    p8.Year = year8;
                    p1.Team = NewTeam1.Text;
                    p2.Team = NewTeam2.Text;
                    p3.Team = NewTeam3.Text;
                    p4.Team = NewTeam4.Text;
                    p5.Team = NewTeam5.Text;
                    p6.Team = NewTeam6.Text;
                    p7.Team = NewTeam7.Text;
                    p8.Team = NewTeam8.Text;
                    ((ObservableCollection<Group>)DataProcessor.Current)[position] = new Group(new Participant[8] { p1, p2, p3, p4, p5, p6, p7, p8 });
                }
                Close();
            }
        }
    }
}
