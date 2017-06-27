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
                A_T.Text = duo.DuoScores[0].ResultPP.ToString("F4");
                I_T.Text = duo.DuoScores[1].ResultPP.ToString("F4");
                T_T.Text = duo.DuoScores[2].ResultPP.ToString("F4");
                OverAllText.Text = "Итог: " + duo.OverAllPP.ToString("F4");
            }
        }
        private void ButtonCount_Click(object sender, RoutedEventArgs e)
        {
            A_T.Text = duo.DuoScores[0].ResultPP.ToString("F4");
            I_T.Text = duo.DuoScores[1].ResultPP.ToString("F4");
            T_T.Text = duo.DuoScores[2].ResultPP.ToString("F4");
            OverAllText.Text = "Итог: " + duo.OverAllPP.ToString("F4");
            duo.IsCounted = true;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < duo.DuoScores.Count; i++)
            {
                for (int j = 0; j < duo.DuoScores[i].RefferiesPP.Length; j++)
                {
                    duo.DuoScores[i].RefferiesPP[j] = 0;
                }
            }
            scoresGrid.ItemsSource = duo.DuoScores = new ObservableCollection<Scores>(duo.DuoScores);
        }
    }
}
