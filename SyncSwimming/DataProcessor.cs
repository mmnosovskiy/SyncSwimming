using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace SyncSwimming
{
    public static class DataProcessor
    {
        public static ObservableCollection<Participant> Current { get; set; }
        public static ObservableCollection<Participant> list = new ObservableCollection<Participant>();
        public static void GetList()
        {
            using (StreamReader sr = new StreamReader(new FileStream("List.txt", FileMode.Open)))
            {
                string line;
                char[] separator = { '\t' };
                try
                {
                    while (true)
                    {
                        Participant p = new Participant();
                        line = sr.ReadLine();
                        string[] words = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        p.FIO = words[0];
                        p.Year = Convert.ToInt32(words[1]);
                        p.Category = words[2];
                        p.Team = words[3];
                        list.Add(p);
                    }
                }
                catch
                {

                }
            }
        }

        public static void ExportToExcel(ObservableCollection<Participant> listP)
        {
            var excelApp = new Excel.Application();
            excelApp.Visible = true;
            //Книга.
            var WorkBookExcel = excelApp.Workbooks.Add();
            //Таблица.
            var WorkSheetExcel = (Excel.Worksheet)WorkBookExcel.Sheets[1];
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
            workSheet.Cells[1, "A"] = "ФИО";
            workSheet.Cells[1, "B"] = "Год рождения";
            workSheet.Cells[1, "C"] = "Разряд";
            workSheet.Cells[1, "D"] = "Команда";
            workSheet.Cells[1, "F"] = "С1";
            workSheet.Cells[1, "G"] = "С2";
            workSheet.Cells[1, "H"] = "С3";
            workSheet.Cells[1, "I"] = "С4";
            workSheet.Cells[1, "J"] = "С5";
            workSheet.Cells[1, "K"] = "С6";
            workSheet.Cells[1, "L"] = "С7";
            workSheet.Cells[1, "M"] = "Результат";
            workSheet.Cells[1, "N"] = "Итог";
            int row = 2;
            foreach (Participant item in list)
            {
                workSheet.Cells[row, "A"] = item.FIO;
                workSheet.Cells[row, "B"] = item.Year.ToString();
                workSheet.Cells[row, "C"] = item.Category;
                workSheet.Cells[row, "D"] = item.Team;
                workSheet.Cells[row, "N"] = item.OverAll;
                row++;
                workSheet.Cells[row, "B"] = "Ф1 =";
                workSheet.Cells[row + 1, "B"] = "Ф2 =";
                workSheet.Cells[row + 2, "B"] = "Ф3 =";
                workSheet.Cells[row + 3, "B"] = "Ф4 =";
                workSheet.Cells[row, "C"] = item.PersonalScores[0].Coef;
                workSheet.Cells[row + 1, "C"] = item.PersonalScores[1].Coef;
                workSheet.Cells[row + 2, "C"] = item.PersonalScores[2].Coef;
                workSheet.Cells[row + 3, "C"] = item.PersonalScores[3].Coef;
                workSheet.Cells[row, "E"] = "Ф1";
                workSheet.Cells[row + 1, "E"] = "Ф2";
                workSheet.Cells[row + 2, "E"] = "Ф3";
                workSheet.Cells[row + 3, "E"] = "Ф4";
                for (int i = 0; i < 4; i++)
                {
                    workSheet.Cells[row, "F"] = item.PersonalScores[i].Refferies[0];
                    workSheet.Cells[row, "G"] = item.PersonalScores[i].Refferies[1];
                    workSheet.Cells[row, "H"] = item.PersonalScores[i].Refferies[2];
                    workSheet.Cells[row, "I"] = item.PersonalScores[i].Refferies[3];
                    workSheet.Cells[row, "J"] = item.PersonalScores[i].Refferies[4];
                    workSheet.Cells[row, "K"] = item.PersonalScores[i].Refferies[5];
                    workSheet.Cells[row, "L"] = item.PersonalScores[i].Refferies[6];
                    workSheet.Cells[row, "M"] = item.PersonalScores[i].Result;
                    row++;
                }
            }
            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();
            workSheet.Columns[3].AutoFit();
            workSheet.Columns[4].AutoFit();
        }

    }
}

