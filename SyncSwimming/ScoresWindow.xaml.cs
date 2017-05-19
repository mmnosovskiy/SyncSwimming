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
    /// Логика взаимодействия для ScoresWindow.xaml
    /// </summary>
    public partial class ScoresWindow : Window
    {
        Participant participant;
        public ScoresWindow()
        {
            InitializeComponent();
        }
        public ScoresWindow(Participant par)
        {
            InitializeComponent();
            participant = par;
            Fio.Text += " " + participant.FIO;
            BirthYear.Text += " " + participant.Year;
            Categ.Text += " " + participant.Category;
            TeamName.Text += " " + participant.Team;
            scoresGrid.ItemsSource = participant.PersonalScores;
            if (participant.IsCounted)
            {
                F1_T.Text = participant.PersonalScores[0].Result.ToString();
                F2_T.Text = participant.PersonalScores[1].Result.ToString();
                F3_T.Text = participant.PersonalScores[2].Result.ToString();
                F4_T.Text = participant.PersonalScores[3].Result.ToString();
                OverAllText.Text = "Итог: " + participant.OverAll;
            }
        }

        private void ButtonCount_Click(object sender, RoutedEventArgs e)
        {
            double[] f = new double[4];
            if (double.TryParse(F1.Text, out f[0]) && double.TryParse(F2.Text, out f[1]) && double.TryParse(F3.Text, out f[2]) && double.TryParse(F4.Text, out f[3]))
            {
                for (int i = 0; i < participant.PersonalScores.Count; i++)
                {
                    participant.PersonalScores[i].Coef = f[i];
                }
                F1_T.Text = participant.PersonalScores[0].Result.ToString();
                F2_T.Text = participant.PersonalScores[1].Result.ToString();
                F3_T.Text = participant.PersonalScores[2].Result.ToString();
                F4_T.Text = participant.PersonalScores[3].Result.ToString();
                OverAllText.Text = "Итог: " + participant.OverAll;
                participant.IsCounted = true;
            }
            else
            {
                MessageBox.Show("Неверные коэффициенты", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
