using System.Collections.ObjectModel;
using System.Windows;

namespace SyncSwimming
{
    /// <summary>
    /// Логика взаимодействия для ScoresWindow.xaml
    /// </summary>
    public partial class ScoresOPWindow : Window
    {
        Participant participant;
        public ScoresOPWindow()
        {
            InitializeComponent();
        }
        public ScoresOPWindow(Participant par, string name)
        {
            InitializeComponent();

            switch (name)
            {
                case "OP8":
                    F1.Text = DataProcessor.F1_8.ToString();
                    F2.Text = DataProcessor.F2_8.ToString();
                    F3.Text = DataProcessor.F3_8.ToString();
                    F4.Text = DataProcessor.F4_8.ToString();
                    break;
                case "OP12":
                    F1.Text = DataProcessor.F1_12.ToString();
                    F2.Text = DataProcessor.F2_12.ToString();
                    F3.Text = DataProcessor.F3_12.ToString();
                    F4.Text = DataProcessor.F4_12.ToString();
                    break;
                case "OP13_15":
                    F1.Text = DataProcessor.F1_13_15.ToString();
                    F2.Text = DataProcessor.F2_13_15.ToString();
                    F3.Text = DataProcessor.F3_13_15.ToString();
                    F4.Text = DataProcessor.F4_13_15.ToString();
                    break;
            }
            participant = par;
            Fio.Text += " " + participant.FIO;
            BirthYear.Text += " " + participant.Year;
            Categ.Text += " " + participant.Category;
            TeamName.Text += " " + participant.Team;
            scoresGrid.ItemsSource = participant.PersonalScoresOP;
            if (participant.IsCounted)
            {
                F1_T.Text = participant.PersonalScoresOP[0].ResultOP.ToString("F4");
                F2_T.Text = participant.PersonalScoresOP[1].ResultOP.ToString("F4");
                F3_T.Text = participant.PersonalScoresOP[2].ResultOP.ToString("F4");
                F4_T.Text = participant.PersonalScoresOP[3].ResultOP.ToString("F4");
                OverAllText.Text = "Итог: " + participant.OverAllOP.ToString("F4");
            }
        }

        private void ButtonCount_Click(object sender, RoutedEventArgs e)
        {
            double[] f = new double[4];
            if (double.TryParse(F1.Text, out f[0]) && double.TryParse(F2.Text, out f[1]) && double.TryParse(F3.Text, out f[2]) && double.TryParse(F4.Text, out f[3]))
            {
                for (int i = 0; i < participant.PersonalScoresOP.Count; i++)
                {
                    participant.PersonalScoresOP[i].Coef = f[i];
                }
                F1_T.Text = participant.PersonalScoresOP[0].ResultOP.ToString("F4");
                F2_T.Text = participant.PersonalScoresOP[1].ResultOP.ToString("F4");
                F3_T.Text = participant.PersonalScoresOP[2].ResultOP.ToString("F4");
                F4_T.Text = participant.PersonalScoresOP[3].ResultOP.ToString("F4");
                OverAllText.Text = "Итог: " + participant.OverAllOP.ToString("F4");
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

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < participant.PersonalScoresOP.Count; i++)
            {
                for (int j = 0; j < participant.PersonalScoresOP[i].RefferiesOP.Length; j++)
                {
                    participant.PersonalScoresOP[i].RefferiesOP[j] = 0;
                }
            }
            scoresGrid.ItemsSource = participant.PersonalScoresOP = new ObservableCollection<Scores>(participant.PersonalScoresOP);
        }
    }
}
