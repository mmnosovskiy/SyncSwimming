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
    /// Логика взаимодействия для ScoresSoloWindow.xaml
    /// </summary>
    public partial class ScoresPPWindow : Window
    {
        Participant participant;
        public ScoresPPWindow()
        {
            InitializeComponent();
        }
        public ScoresPPWindow(Participant par)
        {
            InitializeComponent();
            participant = par;
            Fio.Text += " " + participant.FIO;
            BirthYear.Text += " " + participant.Year;
            Categ.Text += " " + participant.Category;
            TeamName.Text += " " + participant.Team;
            scoresGrid.ItemsSource = participant.PersonalScoresPP;
            if (participant.IsCounted)
            {
                A_T.Text = participant.PersonalScoresPP[0].ResultPP.ToString();
                I_T.Text = participant.PersonalScoresPP[1].ResultPP.ToString();
                T_T.Text = participant.PersonalScoresPP[2].ResultPP.ToString();
                OverAllText.Text = "Итог: " + participant.OverAllPP;
            }
        }

        private void ButtonCount_Click(object sender, RoutedEventArgs e)
        {
            A_T.Text = participant.PersonalScoresPP[0].ResultPP.ToString();
            I_T.Text = participant.PersonalScoresPP[1].ResultPP.ToString();
            T_T.Text = participant.PersonalScoresPP[2].ResultPP.ToString();
            OverAllText.Text = "Итог: " + participant.OverAllPP;
            participant.IsCounted = true;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
