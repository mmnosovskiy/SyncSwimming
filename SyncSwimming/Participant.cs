using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSwimming
{
    [Serializable]
    public class Participant : IComparable
    {
        ObservableCollection<Scores> _pScoresOP = new ObservableCollection<Scores>(new Scores[4] { new Scores() { Name = "Ф1", Coef = 1.6 }, new Scores() { Name = "Ф2", Coef = 1.4 }, new Scores() { Name = "Ф3", Coef = 2.0 }, new Scores() { Name = "Ф4", Coef = 1.7 } });
        ObservableCollection<Scores> _pScoresPP = new ObservableCollection<Scores>(new Scores[3] { new Scores() { Name = "А", Coef = 0.4 }, new Scores() { Name = "И", Coef = 0.3 }, new Scores() { Name = "С", Coef = 0.3 } });
        public string FIO { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public string Team { get; set; }
        public bool IsCounted { get; set; }
        public ObservableCollection<Scores> PersonalScoresOP
        {
            get
            {
                return _pScoresOP;
            }
            set
            {
                _pScoresOP = value;
            }
        }
        public ObservableCollection<Scores> PersonalScoresPP
        {
            get
            {
                return _pScoresPP;
            }
            set
            {
                _pScoresPP = value;
            }
        }
        public double OverAllOP
        {
            get
            {
                double coef = 0;
                double sum = 0;
                foreach (Scores item in PersonalScoresOP)
                {
                    coef += item.Coef;
                    sum += item.ResultOP;
                }
                return sum / coef * 10;
            }
        }
        public double OverAllPP
        {
            get
            {
                double sum = 0;
                foreach (Scores item in PersonalScoresPP)
                {
                    sum += item.ResultPP * item.Coef;
                }
                return sum;
            }
        }

        public Participant Copy()
        {
            Participant copy = new Participant();
            copy.FIO = FIO;
            copy.Category = Category;
            copy.Year = Year;
            copy.Team = Team;
            copy.PersonalScoresOP = PersonalScoresOP;
            return copy;
        }
        public bool Contains(string text)
        {
            return FIO.Contains(text)
                || Category.Contains(text)
                || Team.Contains(text)
                || Year.ToString().Contains(text);
        }

        public int CompareTo(object obj)
        {
            if (DataProcessor.Current == DataProcessor.listOP8 || DataProcessor.Current == DataProcessor.listOP12 || DataProcessor.Current == DataProcessor.listOP13_15)
            {
                if (OverAllOP > ((Participant)obj).OverAllOP) return -1;
                else return 1;
            }
            else 
            {
                if (OverAllPP > ((Participant)obj).OverAllPP) return -1;
                else if (OverAllOP == ((Participant)obj).OverAllOP) return 0;
                else return 1;
            }
        }
    }
}
