using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

namespace SyncSwimming
{
    public static class DataProcessor
    {
        public static bool IsFirst;
        public static bool IsExport;
        public static bool IsWord;
        public static double F1_8 { get; set; }
        public static double F2_8 { get; set; }
        public static double F3_8 { get; set; }
        public static double F4_8 { get; set; }
        public static double F1_12 { get; set; }
        public static double F2_12 { get; set; }
        public static double F3_12 { get; set; }
        public static double F4_12 { get; set; }
        public static double F1_13_15 { get; set; }
        public static double F2_13_15 { get; set; }
        public static double F3_13_15 { get; set; }
        public static double F4_13_15 { get; set; }
        public static bool IsSortedListOP8 { get; set; }
        public static bool IsSortedListOP12 { get; set; }
        public static bool IsSortedListOP13_15 { get; set; }
        public static bool IsSortedListSolo8 { get; set; }
        public static bool IsSortedListSolo12 { get; set; }
        public static bool IsSortedListSolo13_15 { get; set; }
        public static bool IsSortedListDuo8 { get; set; }
        public static bool IsSortedListDuo12 { get; set; }
        public static bool IsSortedListDuo13_15 { get; set; }
        public static bool IsSortedListGroup { get; set; }
        public static bool IsSortedListCombi { get; set; }
        public static bool IsSortedListTrophy { get; set; }
        public static ICollection Current { get; set; }
        static DataProcessor()
        {
            IsSortedListOP8 = false;
            IsSortedListOP12 = false;
            IsSortedListOP13_15 = false;
            IsSortedListSolo8 = false;
            IsSortedListSolo12 = false;
            IsSortedListSolo13_15 = false;
            IsSortedListDuo8 = false;
            IsSortedListDuo12 = false;
            IsSortedListDuo13_15 = false;
            IsSortedListGroup = false;
            IsSortedListCombi = false;
            IsSortedListTrophy = false;
        }
        public static ObservableCollection<Participant> listOP8 = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listOP12 = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listOP13_15 = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listSolo8 = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listSolo12 = new ObservableCollection<Participant>();
        public static ObservableCollection<Participant> listSolo13_15 = new ObservableCollection<Participant>();
        public static ObservableCollection<Duo> listDuo8 = new ObservableCollection<Duo>();
        public static ObservableCollection<Duo> listDuo12 = new ObservableCollection<Duo>();
        public static ObservableCollection<Duo> listDuo13_15 = new ObservableCollection<Duo>();
        public static ObservableCollection<Group> listGroup = new ObservableCollection<Group>();
        public static ObservableCollection<Combi> listCombi = new ObservableCollection<Combi>();
        public static ObservableCollection<Trophy> listTrophy = new ObservableCollection<Trophy>();
        public static Excel.Application excelApp;
        public static Excel.Workbook WorkBookExcel;
        public static Word.Application wordApp;
        public static Word.Document wordDoc;
        public static Word.Paragraph wordPara;
        

        public static void MakeMixList(ICollection<Participant> list)
        {
            Random r = new Random();

            SortedList<int, Participant> mixedList = new SortedList<int, Participant>();
            foreach (Participant item in list)
                mixedList.Add(r.Next(), item);

            list.Clear();
            for (int i = 0; i < mixedList.Count; i++)
            {
                list.Add(mixedList.Values[i]);
            }
            //mixedList.Clear();  
        }

        static void CreateExcel()
        {
            excelApp = new Excel.Application();
            //Книга.
            WorkBookExcel = excelApp.Application.Workbooks.Add();
            Excel.Worksheet wsTrophy = WorkBookExcel.ActiveSheet;
            wsTrophy.Name = "Трофи";
            Excel.Worksheet wsCombi = WorkBookExcel.Sheets.Add();
            wsCombi.Name = "Комби";
            Excel.Worksheet wsGroup = WorkBookExcel.Sheets.Add();
            wsGroup.Name = "Группа";
            Excel.Worksheet wsDuo13_15 = WorkBookExcel.Sheets.Add();
            wsDuo13_15.Name = "Дуэт 13-15";
            Excel.Worksheet wsDuo12 = WorkBookExcel.Sheets.Add();
            wsDuo12.Name = "Дуэт 12 и м";
            Excel.Worksheet wsDuo8 = WorkBookExcel.Sheets.Add();
            wsDuo8.Name = "Дуэт 8 и м";
            Excel.Worksheet wsSolo13_15 = WorkBookExcel.Sheets.Add();
            wsSolo13_15.Name = "Соло 13-15";
            Excel.Worksheet wsSolo12 = WorkBookExcel.Sheets.Add();
            wsSolo12.Name = "Соло 12 и м";
            Excel.Worksheet wsSolo8 = WorkBookExcel.Sheets.Add();
            wsSolo8.Name = "Соло 8 и м";
            Excel.Worksheet wsOP13_15 = WorkBookExcel.Sheets.Add();
            wsOP13_15.Name = "ОП 13-15";
            Excel.Worksheet wsOP12 = WorkBookExcel.Sheets.Add();
            wsOP12.Name = "ОП 12 и м";
            Excel.Worksheet wsOP8 = WorkBookExcel.Sheets.Add();
            wsOP8.Name = "ОП 8 и м";
        }

        public static ObservableCollection<Participant> GetList1(string path)
        {
            ObservableCollection<Participant> list = new ObservableCollection<Participant>();
            using (StreamReader sr = new StreamReader(new FileStream("Resource/" + path, FileMode.Open)))
            {
                string line;
                char[] separator = { '\t' };
                try
                {
                    string cat = "";
                    switch (path)
                    {
                        case "OP8.txt":
                            cat = "OP8";
                            break;
                        case "OP12.txt":
                            cat = "OP12";
                            break;
                        case "OP13_15.txt":
                            cat = "OP13_15";
                            break;
                        case "Solo8.txt":
                            cat = "Solo8";
                            break;
                        case "Solo12.txt":
                            cat = "Solo12";
                            break;
                        case "Solo13_15.txt":
                            cat = "Solo13_15";
                            break;
                    }
                    while (true)
                    {
                        Participant p = new Participant(cat);
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
            using (StreamReader sr = new StreamReader(new FileStream("Resource/" + path, FileMode.Open)))
            {
                string line;
                char[] separator = { '\t' };
                try
                {
                    while (true)
                    {
                        line = sr.ReadLine();
                        string[] words = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        Participant d1 = new Participant("");
                        Participant d2 = new Participant("");
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

        public static ObservableCollection<Group> GetList8(string path)
        {
            ObservableCollection<Group> list = new ObservableCollection<Group>();
            using (StreamReader sr = new StreamReader(new FileStream("Resource/" + path, FileMode.Open)))
            {
                string line;
                char[] separator = { '\t' };
                try
                {
                    while (true)
                    {
                        Participant[] group = new Participant[8] { new Participant(""), new Participant(""), new Participant(""), new Participant(""), new Participant(""), new Participant(""), new Participant(""), new Participant("") };
                        line = sr.ReadLine();
                        string[] words = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < 8; i++)
                        {
                            group[i].FIO = words[i];
                            group[i].Year = Convert.ToInt32(words[i + 8]);
                            group[i].Category = words[i + 16];
                            group[i].Team = words[words.Length - 1];
                        }
                        
                        list.Add(new Group(group));
                    }
                }
                catch
                {

                }
                return list;
            }
        }

        public static ObservableCollection<Combi> GetList10(string path)
        {
            ObservableCollection<Combi> list = new ObservableCollection<Combi>();
            using (StreamReader sr = new StreamReader(new FileStream("Resource/" + path, FileMode.Open)))
            {
                string line;
                char[] separator = { '\t' };
                try
                {
                    while (true)
                    {
                        List<Participant> group = new List<Participant>();
                        line = sr.ReadLine();
                        string[] words = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < (words.Length - 1) / 3; i++)
                        {
                            group.Add(new Participant(""));
                            group[i].FIO = words[i];
                            group[i].Year = Convert.ToInt32(words[i + (words.Length - 1) / 3]);
                            group[i].Category = words[i + (words.Length - 1) / 3 * 2];
                            group[i].Team = words[words.Length - 1];
                        }

                        list.Add(new Combi(group));
                    }
                }
                catch
                {

                }
                return list;
            }
        }
        public static ObservableCollection<Trophy> GetList(string path)
        {
            ObservableCollection<Trophy> list = new ObservableCollection<Trophy>();
            string line = File.ReadAllText("Resource/" + path);
            string[] sep = { "\r\n$\r\n" };
            string[] teams = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            char[] separator = { '\t' };
            try
            {
                for (int j = 0; j < teams.Length; j++)
                {
                    List<Participant> group = new List<Participant>();
                    
                    string[] words = teams[j].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < (words.Length - 1) / 3; i++)
                    {
                        group.Add(new Participant(""));
                        group[i].FIO = words[i];
                        group[i].Year = Convert.ToInt32(words[i + (words.Length - 1) / 3]);
                        group[i].Category = words[i + (words.Length - 1) / 3 * 2];
                        group[i].Team = words[words.Length - 1];
                    }

                    list.Add(new Trophy(group));
                }
            }
            catch
            {

            }
            return list;
            
        }


        public static Task ExportToExcelA(ObservableCollection<Participant> listP, string wsName)
        {
            return Task.Run(() => { 
                if (!IsExport)
                {
                    CreateExcel();
                    IsExport = true;
                }
                Excel._Worksheet workSheet = (Excel.Worksheet)WorkBookExcel.Sheets[wsName];
                if (wsName == "ОП 8 и м" || wsName == "ОП 12 и м" || wsName == "ОП 13-15")
                {
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
                    foreach (Participant item in listP)
                    {
                        workSheet.Cells[row, "A"] = item.FIO;
                        workSheet.Cells[row, "B"] = item.Year.ToString();
                        workSheet.Cells[row, "C"] = item.Category;
                        workSheet.Cells[row, "D"] = item.Team;
                        workSheet.Cells[row, "N"] = item.OverAllOP.ToString("F4");
                        row++;
                        workSheet.Cells[row, "B"] = "Ф1";
                        workSheet.Cells[row + 1, "B"] = "Ф2";
                        workSheet.Cells[row + 2, "B"] = "Ф3";
                        workSheet.Cells[row + 3, "B"] = "Ф4";
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
                            workSheet.Cells[row, "M"] = item.PersonalScoresOP[i].ResultOP.ToString("F4");
                            row++;
                        }
                    }
                    workSheet.Columns[1].AutoFit();
                    workSheet.Columns[2].AutoFit();
                    workSheet.Columns[3].AutoFit();
                    workSheet.Columns[4].AutoFit();
                }
                else
                {
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
                    foreach (Participant item in listP)
                    {
                        workSheet.Cells[row, "A"] = item.FIO;
                        workSheet.Cells[row, "B"] = item.Year;
                        workSheet.Cells[row, "C"] = item.Category;
                        workSheet.Cells[row, "D"] = item.Team;
                        workSheet.Cells[row, "L"] = item.OverAllPP.ToString("F4");
                        row++;
                        workSheet.Cells[row, "E"] = "А";
                        workSheet.Cells[row + 1, "E"] = "И";
                        workSheet.Cells[row + 2, "E"] = "С";
                        for (int i = 0; i < 3; i++)
                        {
                            workSheet.Cells[row, "F"] = item.PersonalScoresPP[i].RefferiesPP[0];
                            workSheet.Cells[row, "G"] = item.PersonalScoresPP[i].RefferiesPP[1];
                            workSheet.Cells[row, "H"] = item.PersonalScoresPP[i].RefferiesPP[2];
                            workSheet.Cells[row, "I"] = item.PersonalScoresPP[i].RefferiesPP[3];
                            workSheet.Cells[row, "J"] = item.PersonalScoresPP[i].RefferiesPP[4];
                            workSheet.Cells[row, "K"] = item.PersonalScoresPP[i].ResultPP.ToString("F4");
                            row++;
                        }
                    }
                    workSheet.Columns[1].AutoFit();
                    workSheet.Columns[2].AutoFit();
                    workSheet.Columns[3].AutoFit();
                    workSheet.Columns[4].AutoFit();
                }
        });
        }

        public static void ExportToExcel(ObservableCollection<Participant> listP, string wsName)
        {
            
                if (!IsExport)
                {
                    CreateExcel();
                    IsExport = true;
                }
                Excel._Worksheet workSheet = (Excel.Worksheet)WorkBookExcel.Sheets[wsName];
                if (wsName == "ОП 8 и м" || wsName == "ОП 12 и м" || wsName == "ОП 13-15")
                {
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
                    foreach (Participant item in listP)
                    {
                        workSheet.Cells[row, "A"] = item.FIO;
                        workSheet.Cells[row, "B"] = item.Year.ToString();
                        workSheet.Cells[row, "C"] = item.Category;
                        workSheet.Cells[row, "D"] = item.Team;
                        workSheet.Cells[row, "N"] = item.OverAllOP.ToString("F4");
                        row++;
                        workSheet.Cells[row, "B"] = "Ф1";
                        workSheet.Cells[row + 1, "B"] = "Ф2";
                        workSheet.Cells[row + 2, "B"] = "Ф3";
                        workSheet.Cells[row + 3, "B"] = "Ф4";
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
                            workSheet.Cells[row, "M"] = item.PersonalScoresOP[i].ResultOP.ToString("F4");
                            row++;
                        }
                    }
                    workSheet.Columns[1].AutoFit();
                    workSheet.Columns[2].AutoFit();
                    workSheet.Columns[3].AutoFit();
                    workSheet.Columns[4].AutoFit();
                }
                else
                {
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
                    foreach (Participant item in listP)
                    {
                        workSheet.Cells[row, "A"] = item.FIO;
                        workSheet.Cells[row, "B"] = item.Year;
                        workSheet.Cells[row, "C"] = item.Category;
                        workSheet.Cells[row, "D"] = item.Team;
                        workSheet.Cells[row, "L"] = item.OverAllPP.ToString("F4");
                        row++;
                        workSheet.Cells[row, "E"] = "А";
                        workSheet.Cells[row + 1, "E"] = "И";
                        workSheet.Cells[row + 2, "E"] = "С";
                        for (int i = 0; i < 3; i++)
                        {
                            workSheet.Cells[row, "F"] = item.PersonalScoresPP[i].RefferiesPP[0];
                            workSheet.Cells[row, "G"] = item.PersonalScoresPP[i].RefferiesPP[1];
                            workSheet.Cells[row, "H"] = item.PersonalScoresPP[i].RefferiesPP[2];
                            workSheet.Cells[row, "I"] = item.PersonalScoresPP[i].RefferiesPP[3];
                            workSheet.Cells[row, "J"] = item.PersonalScoresPP[i].RefferiesPP[4];
                            workSheet.Cells[row, "K"] = item.PersonalScoresPP[i].ResultPP.ToString("F4");
                            row++;
                        }
                    }
                    workSheet.Columns[1].AutoFit();
                    workSheet.Columns[2].AutoFit();
                    workSheet.Columns[3].AutoFit();
                    workSheet.Columns[4].AutoFit();
                }
            
        }

        public static Task ExportToExcelA(ObservableCollection<Duo> listP, string wsName)
        {
            return Task.Run(() =>
            {
                if (!IsExport)
                {
                    CreateExcel();
                    IsExport = true;
                }
                Excel._Worksheet workSheet = (Excel.Worksheet)WorkBookExcel.Sheets[wsName];
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
                foreach (Duo item in listP)
                {
                    workSheet.Cells[row, "A"] = item.Duo1.FIO + "\n" + item.Duo2.FIO;
                    workSheet.Cells[row, "B"] = item.Duo1.Year + "\n" + item.Duo2.Year;
                    workSheet.Cells[row, "C"] = item.Duo1.Category + "\n" + item.Duo2.Category;
                    workSheet.Cells[row, "D"] = item.Duo1.Team;
                    workSheet.Cells[row, "L"] = item.OverAllPP.ToString("F4");
                    row++;
                    workSheet.Cells[row, "E"] = "А";
                    workSheet.Cells[row + 1, "E"] = "И";
                    workSheet.Cells[row + 2, "E"] = "С";
                    for (int i = 0; i < 3; i++)
                    {
                        workSheet.Cells[row, "F"] = item.DuoScores[i].RefferiesPP[0];
                        workSheet.Cells[row, "G"] = item.DuoScores[i].RefferiesPP[1];
                        workSheet.Cells[row, "H"] = item.DuoScores[i].RefferiesPP[2];
                        workSheet.Cells[row, "I"] = item.DuoScores[i].RefferiesPP[3];
                        workSheet.Cells[row, "J"] = item.DuoScores[i].RefferiesPP[4];
                        workSheet.Cells[row, "K"] = item.DuoScores[i].ResultPP.ToString("F4");
                        row++;
                    }
                }
                workSheet.Columns[1].AutoFit();
                workSheet.Columns[2].AutoFit();
                workSheet.Columns[3].AutoFit();
                workSheet.Columns[4].AutoFit();
            });
        }

        public static void ExportToExcel(ObservableCollection<Duo> listP, string wsName)
        {
            
                if (!IsExport)
                {
                    CreateExcel();
                    IsExport = true;
                }
                Excel._Worksheet workSheet = (Excel.Worksheet)WorkBookExcel.Sheets[wsName];
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
                foreach (Duo item in listP)
                {
                    workSheet.Cells[row, "A"] = item.Duo1.FIO + "\n" + item.Duo2.FIO;
                    workSheet.Cells[row, "B"] = item.Duo1.Year + "\n" + item.Duo2.Year;
                    workSheet.Cells[row, "C"] = item.Duo1.Category + "\n" + item.Duo2.Category;
                    workSheet.Cells[row, "D"] = item.Duo1.Team;
                    workSheet.Cells[row, "L"] = item.OverAllPP.ToString("F4");
                    row++;
                    workSheet.Cells[row, "E"] = "А";
                    workSheet.Cells[row + 1, "E"] = "И";
                    workSheet.Cells[row + 2, "E"] = "С";
                    for (int i = 0; i < 3; i++)
                    {
                        workSheet.Cells[row, "F"] = item.DuoScores[i].RefferiesPP[0];
                        workSheet.Cells[row, "G"] = item.DuoScores[i].RefferiesPP[1];
                        workSheet.Cells[row, "H"] = item.DuoScores[i].RefferiesPP[2];
                        workSheet.Cells[row, "I"] = item.DuoScores[i].RefferiesPP[3];
                        workSheet.Cells[row, "J"] = item.DuoScores[i].RefferiesPP[4];
                        workSheet.Cells[row, "K"] = item.DuoScores[i].ResultPP.ToString("F4");
                        row++;
                    }
                }
                workSheet.Columns[1].AutoFit();
                workSheet.Columns[2].AutoFit();
                workSheet.Columns[3].AutoFit();
                workSheet.Columns[4].AutoFit();
            
        }

        public static Task ExportToExcelA(ObservableCollection<Group> listP, string wsName)
        {
            return Task.Run(() =>
            {
                if (!IsExport)
                {
                    CreateExcel();
                    IsExport = true;
                }
                Excel._Worksheet workSheet = (Excel.Worksheet)WorkBookExcel.Sheets[wsName];
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
            foreach (Group item in listP)
            {
                workSheet.Cells[row, "A"] = item.FIO;
                workSheet.Cells[row, "B"] = item.Year;
                workSheet.Cells[row, "C"] = item.Category;
                workSheet.Cells[row, "D"] = item.Team;
                workSheet.Cells[row, "L"] = item.OverAllPP.ToString("F4");
                row++;
                workSheet.Cells[row, "E"] = "А";
                workSheet.Cells[row + 1, "E"] = "И";
                workSheet.Cells[row + 2, "E"] = "С";
                for (int i = 0; i < 3; i++)
                {
                    workSheet.Cells[row, "F"] = item.GroupScores[i].RefferiesPP[0];
                    workSheet.Cells[row, "G"] = item.GroupScores[i].RefferiesPP[1];
                    workSheet.Cells[row, "H"] = item.GroupScores[i].RefferiesPP[2];
                    workSheet.Cells[row, "I"] = item.GroupScores[i].RefferiesPP[3];
                    workSheet.Cells[row, "J"] = item.GroupScores[i].RefferiesPP[4];
                    workSheet.Cells[row, "K"] = item.GroupScores[i].ResultPP.ToString("F4");
                    row++;
                }
            }
            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();
            workSheet.Columns[3].AutoFit();
            workSheet.Columns[4].AutoFit();
            });
        }

        public static void ExportToExcel(ObservableCollection<Group> listP, string wsName)
        {
            
                if (!IsExport)
                {
                    CreateExcel();
                    IsExport = true;
                }
                Excel._Worksheet workSheet = (Excel.Worksheet)WorkBookExcel.Sheets[wsName];
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
                foreach (Group item in listP)
                {
                    workSheet.Cells[row, "A"] = item.FIO;
                    workSheet.Cells[row, "B"] = item.Year;
                    workSheet.Cells[row, "C"] = item.Category;
                    workSheet.Cells[row, "D"] = item.Team;
                    workSheet.Cells[row, "L"] = item.OverAllPP.ToString("F4");
                    row++;
                    workSheet.Cells[row, "E"] = "А";
                    workSheet.Cells[row + 1, "E"] = "И";
                    workSheet.Cells[row + 2, "E"] = "С";
                    for (int i = 0; i < 3; i++)
                    {
                        workSheet.Cells[row, "F"] = item.GroupScores[i].RefferiesPP[0];
                        workSheet.Cells[row, "G"] = item.GroupScores[i].RefferiesPP[1];
                        workSheet.Cells[row, "H"] = item.GroupScores[i].RefferiesPP[2];
                        workSheet.Cells[row, "I"] = item.GroupScores[i].RefferiesPP[3];
                        workSheet.Cells[row, "J"] = item.GroupScores[i].RefferiesPP[4];
                        workSheet.Cells[row, "K"] = item.GroupScores[i].ResultPP.ToString("F4");
                        row++;
                    }
                }
                workSheet.Columns[1].AutoFit();
                workSheet.Columns[2].AutoFit();
                workSheet.Columns[3].AutoFit();
                workSheet.Columns[4].AutoFit();
           
        }

        public static Task ExportToExcelA(ObservableCollection<Combi> listP, string wsName)
        {
            return Task.Run(() =>
            {
                if (!IsExport)
                {
                    CreateExcel();
                    IsExport = true;
                }
                Excel._Worksheet workSheet = (Excel.Worksheet)WorkBookExcel.Sheets[wsName];
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
                foreach (Combi item in listP)
                {
                    workSheet.Cells[row, "A"] = item.FIO;
                    workSheet.Cells[row, "B"] = item.Year;
                    workSheet.Cells[row, "C"] = item.Category;
                    workSheet.Cells[row, "D"] = item.Team;
                    workSheet.Cells[row, "L"] = item.OverAllPP.ToString("F4");
                    row++;
                    workSheet.Cells[row, "E"] = "А";
                    workSheet.Cells[row + 1, "E"] = "И";
                    workSheet.Cells[row + 2, "E"] = "С";
                    for (int i = 0; i < 3; i++)
                    {
                        workSheet.Cells[row, "F"] = item.GroupScores[i].RefferiesPP[0];
                        workSheet.Cells[row, "G"] = item.GroupScores[i].RefferiesPP[1];
                        workSheet.Cells[row, "H"] = item.GroupScores[i].RefferiesPP[2];
                        workSheet.Cells[row, "I"] = item.GroupScores[i].RefferiesPP[3];
                        workSheet.Cells[row, "J"] = item.GroupScores[i].RefferiesPP[4];
                        workSheet.Cells[row, "K"] = item.GroupScores[i].ResultPP.ToString("F4");
                        row++;
                    }
                }
                workSheet.Columns[1].AutoFit();
                workSheet.Columns[2].AutoFit();
                workSheet.Columns[3].AutoFit();
                workSheet.Columns[4].AutoFit();
            });
        }

        public static void ExportToExcel(ObservableCollection<Combi> listP, string wsName)
        {

            if (!IsExport)
            {
                CreateExcel();
                IsExport = true;
            }
            Excel._Worksheet workSheet = (Excel.Worksheet)WorkBookExcel.Sheets[wsName];
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
            foreach (Combi item in listP)
            {
                workSheet.Cells[row, "A"] = item.FIO;
                workSheet.Cells[row, "B"] = item.Year;
                workSheet.Cells[row, "C"] = item.Category;
                workSheet.Cells[row, "D"] = item.Team;
                workSheet.Cells[row, "L"] = item.OverAllPP.ToString("F4");
                row++;
                workSheet.Cells[row, "E"] = "А";
                workSheet.Cells[row + 1, "E"] = "И";
                workSheet.Cells[row + 2, "E"] = "С";
                for (int i = 0; i < 3; i++)
                {
                    workSheet.Cells[row, "F"] = item.GroupScores[i].RefferiesPP[0];
                    workSheet.Cells[row, "G"] = item.GroupScores[i].RefferiesPP[1];
                    workSheet.Cells[row, "H"] = item.GroupScores[i].RefferiesPP[2];
                    workSheet.Cells[row, "I"] = item.GroupScores[i].RefferiesPP[3];
                    workSheet.Cells[row, "J"] = item.GroupScores[i].RefferiesPP[4];
                    workSheet.Cells[row, "K"] = item.GroupScores[i].ResultPP.ToString("F4");
                    row++;
                }
            }
            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();
            workSheet.Columns[3].AutoFit();
            workSheet.Columns[4].AutoFit();

        }

        public static Task ExportToExcelA(ObservableCollection<Trophy> listP, string wsName)
        {
            return Task.Run(() =>
            {
                if (!IsExport)
                {
                    CreateExcel();
                    IsExport = true;
                }
                Excel._Worksheet workSheet = (Excel.Worksheet)WorkBookExcel.Sheets[wsName];
                workSheet.Cells[1, "A"] = "ФИО";
                workSheet.Cells[1, "B"] = "Год рождения";
                workSheet.Cells[1, "C"] = "Разряд";
                workSheet.Cells[1, "D"] = "Команда";
                workSheet.Cells[1, "F"] = "С1";
                workSheet.Cells[1, "G"] = "С2";
                workSheet.Cells[1, "H"] = "С3";
                workSheet.Cells[1, "I"] = "С4";
                workSheet.Cells[1, "J"] = "С5";
                workSheet.Cells[1, "K"] = "C6";
                workSheet.Cells[1, "L"] = "C7";
                workSheet.Cells[1, "M"] = "C8";
                workSheet.Cells[1, "N"] = "C9";
                workSheet.Cells[1, "O"] = "Результат";
                workSheet.Cells[1, "P"] = "Итог";

                int row = 2;
                foreach (Trophy item in listP)
                {
                    workSheet.Cells[row, "A"] = item.FIO;
                    workSheet.Cells[row, "B"] = item.Year;
                    workSheet.Cells[row, "C"] = item.Category;
                    workSheet.Cells[row, "D"] = item.Team;
                    workSheet.Cells[row, "P"] = item.OverAllT.ToString("F4");
                    row++;
                    workSheet.Cells[row, "E"] = "А";

                    workSheet.Cells[row, "F"] = item.TrophyScores.RefferiesT[0];
                    workSheet.Cells[row, "G"] = item.TrophyScores.RefferiesT[1];
                    workSheet.Cells[row, "H"] = item.TrophyScores.RefferiesT[2];
                    workSheet.Cells[row, "I"] = item.TrophyScores.RefferiesT[3];
                    workSheet.Cells[row, "J"] = item.TrophyScores.RefferiesT[4];
                    workSheet.Cells[row, "K"] = item.TrophyScores.RefferiesT[5];
                    workSheet.Cells[row, "L"] = item.TrophyScores.RefferiesT[6];
                    workSheet.Cells[row, "M"] = item.TrophyScores.RefferiesT[7];
                    workSheet.Cells[row, "N"] = item.TrophyScores.RefferiesT[8];
                    workSheet.Cells[row, "O"] = item.TrophyScores.ResultT.ToString("F4");
                    row++;
                }
                workSheet.Columns[1].AutoFit();
                workSheet.Columns[2].AutoFit();
                workSheet.Columns[3].AutoFit();
                workSheet.Columns[4].AutoFit();
            });
        }

        public static void ExportToExcel(ObservableCollection<Trophy> listP, string wsName)
        {
            
                if (!IsExport)
                {
                    CreateExcel();
                    IsExport = true;
                }
                Excel._Worksheet workSheet = (Excel.Worksheet)WorkBookExcel.Sheets[wsName];
                workSheet.Cells[1, "A"] = "ФИО";
                workSheet.Cells[1, "B"] = "Год рождения";
                workSheet.Cells[1, "C"] = "Разряд";
                workSheet.Cells[1, "D"] = "Команда";
                workSheet.Cells[1, "F"] = "С1";
                workSheet.Cells[1, "G"] = "С2";
                workSheet.Cells[1, "H"] = "С3";
                workSheet.Cells[1, "I"] = "С4";
                workSheet.Cells[1, "J"] = "С5";
                workSheet.Cells[1, "K"] = "C6";
                workSheet.Cells[1, "L"] = "C7";
                workSheet.Cells[1, "M"] = "C8";
                workSheet.Cells[1, "N"] = "C9";
                workSheet.Cells[1, "O"] = "Результат";
                workSheet.Cells[1, "P"] = "Итог";

                int row = 2;
                foreach (Trophy item in listP)
                {
                    workSheet.Cells[row, "A"] = item.FIO;
                    workSheet.Cells[row, "B"] = item.Year;
                    workSheet.Cells[row, "C"] = item.Category;
                    workSheet.Cells[row, "D"] = item.Team;
                    workSheet.Cells[row, "P"] = item.OverAllT.ToString("F4");
                    row++;
                    workSheet.Cells[row, "E"] = "А";

                    workSheet.Cells[row, "F"] = item.TrophyScores.RefferiesT[0];
                    workSheet.Cells[row, "G"] = item.TrophyScores.RefferiesT[1];
                    workSheet.Cells[row, "H"] = item.TrophyScores.RefferiesT[2];
                    workSheet.Cells[row, "I"] = item.TrophyScores.RefferiesT[3];
                    workSheet.Cells[row, "J"] = item.TrophyScores.RefferiesT[4];
                    workSheet.Cells[row, "K"] = item.TrophyScores.RefferiesT[5];
                    workSheet.Cells[row, "L"] = item.TrophyScores.RefferiesT[6];
                    workSheet.Cells[row, "M"] = item.TrophyScores.RefferiesT[7];
                    workSheet.Cells[row, "N"] = item.TrophyScores.RefferiesT[8];
                    workSheet.Cells[row, "O"] = item.TrophyScores.ResultT.ToString("F4");
                    row++;
                }
                workSheet.Columns[1].AutoFit();
                workSheet.Columns[2].AutoFit();
                workSheet.Columns[3].AutoFit();
                workSheet.Columns[4].AutoFit();
            
        }

        public static void Deserialize()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream("Resource/Data.txt", FileMode.Open))
            {
                F1_8 = (double)bf.Deserialize(fs);
                F2_8 = (double)bf.Deserialize(fs);
                F3_8 = (double)bf.Deserialize(fs);
                F4_8 = (double)bf.Deserialize(fs);
                F1_12 = (double)bf.Deserialize(fs);
                F2_12 = (double)bf.Deserialize(fs);
                F3_12 = (double)bf.Deserialize(fs);
                F4_12 = (double)bf.Deserialize(fs);
                F1_13_15 = (double)bf.Deserialize(fs);
                F2_13_15 = (double)bf.Deserialize(fs);
                F3_13_15 = (double)bf.Deserialize(fs);
                F4_13_15 = (double)bf.Deserialize(fs);
                listOP8 = (ObservableCollection<Participant>)bf.Deserialize(fs);
                listOP12 = (ObservableCollection<Participant>)bf.Deserialize(fs);
                listOP13_15 = (ObservableCollection<Participant>)bf.Deserialize(fs);
                listSolo8 = (ObservableCollection<Participant>)bf.Deserialize(fs);
                listSolo12 = (ObservableCollection<Participant>)bf.Deserialize(fs);
                listSolo13_15 = (ObservableCollection<Participant>)bf.Deserialize(fs);
                listDuo8 = (ObservableCollection<Duo>)bf.Deserialize(fs);
                listDuo12 = (ObservableCollection<Duo>)bf.Deserialize(fs);
                listDuo13_15 = (ObservableCollection<Duo>)bf.Deserialize(fs);
                listGroup = (ObservableCollection<Group>)bf.Deserialize(fs);
                listCombi = (ObservableCollection<Combi>)bf.Deserialize(fs);
                listTrophy = (ObservableCollection<Trophy>)bf.Deserialize(fs);
            }
            //F1_8 = 1.6;
            //F2_8 = 1.4;
            //F3_8 = 2;
            //F4_8 = 1.7;
            //DataProcessor.listOP8 = DataProcessor.GetList1("ListOP8.txt");
            //DataProcessor.listOP12 = DataProcessor.GetList1("ListOP12.txt");
            //DataProcessor.listOP13_15 = DataProcessor.GetList1("ListOP13_15.txt");
            //DataProcessor.listSolo8 = DataProcessor.GetList1("ListSolo8.txt");
            //DataProcessor.listSolo12 = DataProcessor.GetList1("ListSolo12.txt");
            //DataProcessor.listSolo13_15 = DataProcessor.GetList1("ListSolo13_15.txt");
            //DataProcessor.listDuo8 = DataProcessor.GetList2("ListDuo8.txt");
            //DataProcessor.listDuo12 = DataProcessor.GetList2("ListDuo12.txt");
            //DataProcessor.listDuo13_15 = DataProcessor.GetList2("ListDuo13_15.txt");
            //DataProcessor.listGroup = DataProcessor.GetList8("ListGroup.txt");
            //DataProcessor.listCombi = DataProcessor.GetList10("ListCombi.txt");
            //DataProcessor.listTrophy = DataProcessor.GetList("ListTrophy.txt");

        }
        
        

        public static void Serialize()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream("Resource/Data.txt", FileMode.Create))
            {
                bf.Serialize(fs, F1_8);
                bf.Serialize(fs, F2_8);
                bf.Serialize(fs, F3_8);
                bf.Serialize(fs, F4_8);
                bf.Serialize(fs, F1_12);
                bf.Serialize(fs, F2_12);
                bf.Serialize(fs, F3_12);
                bf.Serialize(fs, F4_12);
                bf.Serialize(fs, F1_13_15);
                bf.Serialize(fs, F2_13_15);
                bf.Serialize(fs, F3_13_15);
                bf.Serialize(fs, F4_13_15);
                bf.Serialize(fs, listOP8);
                bf.Serialize(fs, listOP12);
                bf.Serialize(fs, listOP13_15);
                bf.Serialize(fs, listSolo8);
                bf.Serialize(fs, listSolo12);
                bf.Serialize(fs, listSolo13_15);
                bf.Serialize(fs, listDuo8);
                bf.Serialize(fs, listDuo12);
                bf.Serialize(fs, listDuo13_15);
                bf.Serialize(fs, listGroup);
                bf.Serialize(fs, listCombi);
                bf.Serialize(fs, listTrophy);
            }
        } 

        public static Task WordExport(ObservableCollection<Participant> list)
        {
            return Task.Run(() => { 

            if (!IsWord) wordApp = new Word.Application();
            wordDoc = wordApp.Documents.Add();
            int i = 1; 
            foreach (Participant member in list)
            {
                wordDoc.Paragraphs.Add();
                wordDoc.Paragraphs[i++].Range.Text = member.FIO + " " + member.Year + " " + member.Category + " " + member.Team;
            }
            wordApp.Visible = true;

            });
        }
        public static Task WordExport(ObservableCollection<Duo> list)
        {
            return Task.Run(() => {

                if (!IsWord) wordApp = new Word.Application();
                wordDoc = wordApp.Documents.Add();
                int i = 1;
                foreach (Duo member in list)
                {
                    wordDoc.Paragraphs.Add();
                    wordDoc.Paragraphs[i++].Range.Text = member.Duo1.FIO + " " + member.Duo1.Year + " " + member.Duo1.Category + " " + member.Duo1.Team;
                    wordDoc.Paragraphs.Add();
                    wordDoc.Paragraphs[i++].Range.Text = member.Duo2.FIO + " " + member.Duo2.Year + " " + member.Duo2.Category + " " + member.Duo2.Team;
                    wordDoc.Paragraphs.Add();
                    i++;
                }
                wordApp.Visible = true;

            });
        }
        public static Task WordExport(ObservableCollection<Group> list)
        {
            return Task.Run(() => {

                if (!IsWord) wordApp = new Word.Application();
                wordDoc = wordApp.Documents.Add();
                int i = 1;
                foreach (Group member in list)
                {
                    foreach (Participant memb in member.GroupP)
                    {
                        wordDoc.Paragraphs.Add();
                        wordDoc.Paragraphs[i++].Range.Text = memb.FIO + " " + memb.Year + " " + memb.Category + " " + memb.Team;
                    }
                    wordDoc.Paragraphs.Add();
                    i++;
                }
                wordApp.Visible = true;

            });
        }
        public static Task WordExport(ObservableCollection<Combi> list)
        {
            return Task.Run(() => {

                if (!IsWord) wordApp = new Word.Application();
                wordDoc = wordApp.Documents.Add();
                int i = 1;
                foreach (Combi member in list)
                {
                    foreach (Participant memb in member.GroupP)
                    {
                        wordDoc.Paragraphs.Add();
                        wordDoc.Paragraphs[i++].Range.Text = memb.FIO + " " + memb.Year + " " + memb.Category + " " + memb.Team;
                    }
                    wordDoc.Paragraphs.Add();
                    i++;
                }
                wordApp.Visible = true;

            });
        }
        public static Task WordExport(ObservableCollection<Trophy> list)
        {
            return Task.Run(() => {

                if (!IsWord) wordApp = new Word.Application();
                wordDoc = wordApp.Documents.Add();
                int i = 1;
                foreach (Trophy member in list)
                {
                    foreach (Participant memb in member.Members)
                    {
                        wordDoc.Paragraphs.Add();
                        wordDoc.Paragraphs[i++].Range.Text = memb.FIO + " " + memb.Year + " " + memb.Category + " " + memb.Team;
                    }
                    wordDoc.Paragraphs.Add();
                    i++;
                }
                wordApp.Visible = true;

            });
        }
    }
}

