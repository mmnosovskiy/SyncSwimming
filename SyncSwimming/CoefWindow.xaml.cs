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
    /// Логика взаимодействия для CoefWindow.xaml
    /// </summary>
    public partial class CoefWindow : Window
    {
        string button;
        public CoefWindow(string name)
        {
            InitializeComponent();
            button = name;
            switch (button)
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
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            double[] f = new double[4];
            if (double.TryParse(F1.Text, out f[0]) && double.TryParse(F2.Text, out f[1]) && double.TryParse(F3.Text, out f[2]) && double.TryParse(F4.Text, out f[3]))
            {
                switch (button)
                {
                    case "OP8":
                        DataProcessor.F1_8 = f[0];
                        DataProcessor.F2_8 = f[1];
                        DataProcessor.F3_8 = f[2];
                        DataProcessor.F4_8 = f[3];
                        break;
                    case "OP12":
                        DataProcessor.F1_12 = f[0];
                        DataProcessor.F2_12 = f[1];
                        DataProcessor.F3_12 = f[2];
                        DataProcessor.F4_12 = f[3];
                        break;
                    case "OP13_15":
                        DataProcessor.F1_13_15 = f[0];
                        DataProcessor.F2_13_15 = f[1];
                        DataProcessor.F3_13_15 = f[2];
                        DataProcessor.F4_13_15 = f[3];
                        break;
                }
                Close();
            }
        }
    }
}
