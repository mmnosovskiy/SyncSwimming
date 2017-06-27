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
    /// Логика взаимодействия для ScoresGroupWindow.xaml
    /// </summary>
    public partial class ScoresGroupWindow : Window
    {
        Group group;
        public ScoresGroupWindow()
        {
            InitializeComponent();
        }
        public ScoresGroupWindow(Group g)
        {
            InitializeComponent();
            group = g;
            Fio1.Text += " " + group[0].FIO;
            BirthYear1.Text += " " + group[0].Year;
            Categ1.Text += " " + group[0].Category;
            TeamName1.Text += " " + group[0].Team;
            Fio2.Text += " " + group[1].FIO;
            BirthYear2.Text += " " + group[1].Year;
            Categ2.Text += " " + group[1].Category;
            TeamName2.Text += " " + group[1].Team;
            Fio3.Text += " " + group[2].FIO;
            BirthYear3.Text += " " + group[2].Year;
            Categ3.Text += " " + group[2].Category;
            TeamName3.Text += " " + group[2].Team;
            Fio4.Text += " " + group[3].FIO;
            BirthYear4.Text += " " + group[3].Year;
            Categ4.Text += " " + group[3].Category;
            TeamName4.Text += " " + group[3].Team;
            Fio5.Text += " " + group[4].FIO;
            BirthYear5.Text += " " + group[4].Year;
            Categ5.Text += " " + group[4].Category;
            TeamName5.Text += " " + group[4].Team;
            Fio6.Text += " " + group[5].FIO;
            BirthYear6.Text += " " + group[5].Year;
            Categ6.Text += " " + group[5].Category;
            TeamName6.Text += " " + group[5].Team;
            Fio7.Text += " " + group[6].FIO;
            BirthYear7.Text += " " + group[6].Year;
            Categ7.Text += " " + group[6].Category;
            TeamName7.Text += " " + group[6].Team;
            Fio8.Text += " " + group[7].FIO;
            BirthYear8.Text += " " + group[7].Year;
            Categ8.Text += " " + group[7].Category;
            TeamName8.Text += " " + group[7].Team;

            scoresGrid.ItemsSource = group.GroupScores;
            if (group.IsCounted)
            {
                A_T.Text = group.GroupScores[0].ResultPP.ToString("F4");
                I_T.Text = group.GroupScores[1].ResultPP.ToString("F4");
                T_T.Text = group.GroupScores[2].ResultPP.ToString("F4");
                OverAllText.Text = "Итог: " + group.OverAllPP.ToString("F4");
            }
        }
        private void ButtonCount_Click(object sender, RoutedEventArgs e)
        {
            A_T.Text = group.GroupScores[0].ResultPP.ToString("F4");
            I_T.Text = group.GroupScores[1].ResultPP.ToString("F4");
            T_T.Text = group.GroupScores[2].ResultPP.ToString("F4");
            OverAllText.Text = "Итог: " + group.OverAllPP.ToString("F4");
            group.IsCounted = true;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < group.GroupScores.Count; i++)
            {
                for (int j = 0; j < group.GroupScores[i].RefferiesPP.Length; j++)
                {
                    group.GroupScores[i].RefferiesPP[j] = 0;
                }
            }
            scoresGrid.ItemsSource = group.GroupScores = new ObservableCollection<Scores>(group.GroupScores);
        }
    }
}
