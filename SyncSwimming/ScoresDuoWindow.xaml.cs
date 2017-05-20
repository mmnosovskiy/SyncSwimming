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
    /// Логика взаимодействия для ScoresDuoWindow.xaml
    /// </summary>
    public partial class ScoresDuoWindow : Window
    {
        Duo duo;
        public ScoresDuoWindow()
        {
            InitializeComponent();
        }
        public ScoresDuoWindow(Duo duoP)
        {
            InitializeComponent();
            duo = duoP;
            Fio1.Text += " " + duo.Duo1.FIO;
            BirthYear1.Text += " " + duo.Duo1.Year;
            Categ1.Text += " " + duo.Duo1.Category;
            TeamName1.Text += " " + duo.Duo1.Team;
            Fio2.Text += " " + duo.Duo2.FIO;
            BirthYear2.Text += " " + duo.Duo2.Year;
            Categ2.Text += " " + duo.Duo2.Category;
            TeamName2.Text += " " + duo.Duo2.Team;
            scoresGrid.ItemsSource = duo.DuoScores;
            if (duo.IsCounted)
            {
                A_T.Text = duo.DuoScores[0].ResultPP.ToString();
                I_T.Text = duo.DuoScores[1].ResultPP.ToString();
                T_T.Text = duo.DuoScores[2].ResultPP.ToString();
                OverAllText.Text = "Итог: " + duo.OverAllPP;
            }
        }
        private void ButtonCount_Click(object sender, RoutedEventArgs e)
        {
            A_T.Text = duo.DuoScores[0].ResultPP.ToString();
            I_T.Text = duo.DuoScores[1].ResultPP.ToString();
            T_T.Text = duo.DuoScores[2].ResultPP.ToString();
            OverAllText.Text = "Итог: " + duo.OverAllPP;
            duo.IsCounted = true;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
