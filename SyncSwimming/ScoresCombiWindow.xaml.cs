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
    /// Логика взаимодействия для ScoresCombiWindow.xaml
    /// </summary>
    public partial class ScoresCombiWindow : Window
    {
        Combi combi;
        public ScoresCombiWindow()
        {
            InitializeComponent();
        }
        public ScoresCombiWindow(Combi com)
        {
            InitializeComponent();
            combi = com;
            Grid[] grids = new Grid[com.GroupP.Count];
            for (int i = 0; i < com.GroupP.Count; i++)
            {
                grids[i] = new Grid() { Height = 25 };
                grids[i].ColumnDefinitions.Add(new ColumnDefinition());
                grids[i].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
                grids[i].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80) });
                grids[i].ColumnDefinitions.Add(new ColumnDefinition());
                TextBlock[] txt = new TextBlock[4] { new TextBlock() { Text = "ФИО: " + com.GroupP[i].FIO, Margin = new Thickness(5) }, new TextBlock() { Text = "Год рождения: " + com.GroupP[i].Year, Margin = new Thickness(5) }, new TextBlock() { Text = "Разряд: " + com.GroupP[i].Category, Margin = new Thickness(5) }, new TextBlock() { Text = "Команда: " + com.GroupP[i].Team, Margin = new Thickness(5) } };
                Grid.SetColumn(txt[0], 0);
                Grid.SetColumn(txt[1], 1);
                Grid.SetColumn(txt[2], 2);
                Grid.SetColumn(txt[3], 3);
                grids[i].Children.Add(txt[0]);
                grids[i].Children.Add(txt[1]);
                grids[i].Children.Add(txt[2]);
                grids[i].Children.Add(txt[3]);
                stackCombi.Children.Add(grids[i]);
            }
            scoresGrid.ItemsSource = combi.GroupScores;
            if (com.IsCounted)
            {
                A_T.Text = com.GroupScores[0].ResultPP.ToString("F4");
                I_T.Text = com.GroupScores[1].ResultPP.ToString("F4");
                T_T.Text = com.GroupScores[2].ResultPP.ToString("F4");
                OverAllText.Text = "Итог: " + com.OverAllPP.ToString("F4");
            }
            double sumH = 0;
            foreach (Grid item in stackCombi.Children)
            {
                sumH += item.Height + 15;
            }
            Height = sumH + scoresGrid.Height + 10 + b1.Height + 10 + b2.Height + 10;
        }

        private void ButtonCount_Click(object sender, RoutedEventArgs e)
        {
            A_T.Text = combi.GroupScores[0].ResultPP.ToString("F4");
            I_T.Text = combi.GroupScores[1].ResultPP.ToString("F4");
            T_T.Text = combi.GroupScores[2].ResultPP.ToString("F4");
            OverAllText.Text = "Итог: " + combi.OverAllPP.ToString("F4");
            combi.IsCounted = true;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < combi.GroupScores.Count; i++)
            {
                for (int j = 0; j < combi.GroupScores[i].RefferiesPP.Length; j++)
                {
                    combi.GroupScores[i].RefferiesPP[j] = 0;
                }
            }
            scoresGrid.ItemsSource = combi.GroupScores = new ObservableCollection<Scores>(combi.GroupScores); 
        }
    }
}
