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
    /// Логика взаимодействия для ScoresTrophyWindow.xaml
    /// </summary>
    public partial class ScoresTrophyWindow : Window
    {
        Trophy tr;
        public ScoresTrophyWindow()
        {
            InitializeComponent();
        }
        public ScoresTrophyWindow(Trophy trophy)
        {
            InitializeComponent();
            tr = trophy;
            Grid[] grids = new Grid[trophy.Members.Count];
            for (int i = 0; i < trophy.Members.Count; i++)
            {
                grids[i] = new Grid() { Height = 25 };
                grids[i].ColumnDefinitions.Add(new ColumnDefinition());
                grids[i].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
                grids[i].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80) });
                grids[i].ColumnDefinitions.Add(new ColumnDefinition());
                TextBlock[] txt = new TextBlock[4] { new TextBlock() { Text = "ФИО: " + trophy.Members[i].FIO, Margin = new Thickness(5) }, new TextBlock() { Text = "Год рождения: " + trophy.Members[i].Year, Margin = new Thickness(5) }, new TextBlock() { Text = "Разряд: " + trophy.Members[i].Category, Margin = new Thickness(5) }, new TextBlock() { Text = "Команда: " + trophy.Members[i].Team, Margin = new Thickness(5) } };
                Grid.SetColumn(txt[0], 0);
                Grid.SetColumn(txt[1], 1);
                Grid.SetColumn(txt[2], 2);
                Grid.SetColumn(txt[3], 3);
                grids[i].Children.Add(txt[0]);
                grids[i].Children.Add(txt[1]);
                grids[i].Children.Add(txt[2]);
                grids[i].Children.Add(txt[3]);
                stackTrophy.Children.Add(grids[i]);
            }
            List<Scores> list = new List<Scores>();
            list.Add(trophy.TrophyScores);
            scoresGrid.ItemsSource = list;
            if (trophy.IsCounted)
            {
                A_T.Text = trophy.TrophyScores.ResultT.ToString("F4");
                OverAllText.Text = "Итог: " + trophy.OverAllT.ToString("F4");
            }
            double sumH = 0;
            foreach (Grid item in stackTrophy.Children)
            {
                sumH += item.Height + 15;
            }
            Height = sumH + scoresGrid.Height + 10 + b1.Height + 10 + b2.Height + 10;
        }

        private void ButtonCount_Click(object sender, RoutedEventArgs e)
        {
            A_T.Text = tr.TrophyScores.ResultT.ToString("F4");
            OverAllText.Text = "Итог: " + tr.OverAllT.ToString("F4");
            tr.IsCounted = true;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < tr.TrophyScores.RefferiesT.Length; i++)
            {
                tr.TrophyScores.RefferiesT[i] = 0;
            }
            List<Scores> list = new List<Scores>();
            list.Add(tr.TrophyScores = new Scores() { Name = "А" });
            scoresGrid.ItemsSource = list;
        }
    }
}
