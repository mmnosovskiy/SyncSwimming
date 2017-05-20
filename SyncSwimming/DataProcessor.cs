using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;

namespace SyncSwimming
{
    public static class DataProcessor
    {
        public static ICollection Current { get; set; }
        
        public static ObservableCollection<Participant> listOP8 = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listOP12 = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listOP13_15 = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listSolo8 = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listSolo12 = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listSolo13_15 = new ObservableCollection<Participant>();
        public static ObservableCollection<Duo> listDuo8 = new ObservableCollection<Duo>();
        public static ObservableCollection<Duo> listDuo12 = new ObservableCollection<Duo>();
        public static ObservableCollection<Duo> listDuo13_15 = new ObservableCollection<Duo>();
        public static ObservableCollection<Participant> listGroup = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listCombi = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listTrophy = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> GetList1(string path)
        {
            ObservableCollection<Participant> list = new ObservableCollection<Participant>();
            using (StreamReader sr = new StreamReader(new FileStream("../../Resources/" + path, FileMode.Open)))
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
                return list;
            }
        }
        public static ObservableCollection<Duo> GetList2(string path)
        {
            ObservableCollection<Duo> list = new ObservableCollection<Duo>();
            using (StreamReader sr = new StreamReader(new FileStream("../../Resources/" + path, FileMode.Open)))
            {
                string line;
                char[] separator = { '\t' };
                try
                {
                    while (true)
                    {
                        line = sr.ReadLine();
                        string[] words = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        Participant d1 = new Participant();
                        Participant d2 = new Participant();
                        d1.FIO = words[0];
                        d2.FIO = words[1];
                        d1.Year = Convert.ToInt32(words[2]);
                        d2.Year = Convert.ToInt32(words[3]);
                        d1.Category = words[4];
                        d2.Category = words[5];
                        d1.Team = d2.Team = words[6];
                        Duo d = new Duo(d1, d2);
                        list.Add(d);
                    }
                }
                catch
                {

                }
                return list;
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
            foreach (Participant item in Current)
            {
                workSheet.Cells[row, "A"] = item.FIO;
                workSheet.Cells[row, "B"] = item.Year.ToString();
                workSheet.Cells[row, "C"] = item.Category;
                workSheet.Cells[row, "D"] = item.Team;
                workSheet.Cells[row, "N"] = item.OverAllOP;
                row++;
                workSheet.Cells[row, "B"] = "Ф1 =";
                workSheet.Cells[row + 1, "B"] = "Ф2 =";
                workSheet.Cells[row + 2, "B"] = "Ф3 =";
                workSheet.Cells[row + 3, "B"] = "Ф4 =";
                workSheet.Cells[row, "C"] = item.PersonalScoresOP[0].Coef;
                workSheet.Cells[row + 1, "C"] = item.PersonalScoresOP[1].Coef;
                workSheet.Cells[row + 2, "C"] = item.PersonalScoresOP[2].Coef;
                workSheet.Cells[row + 3, "C"] = item.PersonalScoresOP[3].Coef;
                workSheet.Cells[row, "E"] = "Ф1";
                workSheet.Cells[row + 1, "E"] = "Ф2";
                workSheet.Cells[row + 2, "E"] = "Ф3";
                workSheet.Cells[row + 3, "E"] = "Ф4";
                for (int i = 0; i < 4; i++)
                {
                    workSheet.Cells[row, "F"] = item.PersonalScoresOP[i].RefferiesOP[0];
                    workSheet.Cells[row, "G"] = item.PersonalScoresOP[i].RefferiesOP[1];
                    workSheet.Cells[row, "H"] = item.PersonalScoresOP[i].RefferiesOP[2];
                    workSheet.Cells[row, "I"] = item.PersonalScoresOP[i].RefferiesOP[3];
                    workSheet.Cells[row, "J"] = item.PersonalScoresOP[i].RefferiesOP[4];
                    workSheet.Cells[row, "K"] = item.PersonalScoresOP[i].RefferiesOP[5];
                    workSheet.Cells[row, "L"] = item.PersonalScoresOP[i].RefferiesOP[6];
                    workSheet.Cells[row, "M"] = item.PersonalScoresOP[i].ResultOP;
                    row++;
                }
            }
            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();
            workSheet.Columns[3].AutoFit();
            workSheet.Columns[4].AutoFit();
        }
        public static void ExportToExcel(ObservableCollection<Duo> listP)
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
            workSheet.Cells[1, "K"] = "Результат";
            workSheet.Cells[1, "L"] = "Итог";
            int row = 2;
            foreach (Duo item in (ObservableCollection<Duo>)Current)
            {
                workSheet.Cells[row, "A"] = item.Duo1.FIO + " " + item.Duo2.FIO;
                workSheet.Cells[row, "B"] = item.Duo1.Year + " " + item.Duo2.Year;
                workSheet.Cells[row, "C"] = item.Duo1.Category + " " + item.Duo2.Category;
                workSheet.Cells[row, "D"] = item.Duo1.Team;
                workSheet.Cells[row, "N"] = item.OverAllPP;
                row++;
                workSheet.Cells[row, "E"] = "А";
                workSheet.Cells[row + 1, "E"] = "И";
                workSheet.Cells[row + 2, "E"] = "Т";
                for (int i = 0; i < 3; i++)
                {
                    workSheet.Cells[row, "F"] = item.DuoScores[i].RefferiesPP[0];
                    workSheet.Cells[row, "G"] = item.DuoScores[i].RefferiesPP[1];
                    workSheet.Cells[row, "H"] = item.DuoScores[i].RefferiesPP[2];
                    workSheet.Cells[row, "I"] = item.DuoScores[i].RefferiesPP[3];
                    workSheet.Cells[row, "J"] = item.DuoScores[i].RefferiesPP[4];
                    workSheet.Cells[row, "M"] = item.DuoScores[i].ResultPP;
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

