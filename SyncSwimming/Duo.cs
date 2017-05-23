using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSwimming
{
    [Serializable]
    public class Duo : IComparable
    {
        public Participant Duo1 { get; set; }
        public Participant Duo2 { get; set; }
        public Duo(Participant p1, Participant p2)
        {
            Duo1 = p1;
            Duo2 = p2;
            FIO = Duo1.FIO + "\n" + Duo2.FIO;
            Year = Duo1.Year + "\n" + Duo2.Year;
            Category = Duo1.Category + "\n" + Duo2.Category;
            Team = Duo1.Team;
        }
        public string FIO { get; set; }
        public string Year { get; set; }
        public string Category { get; set; }
        public string Team { get; set; }
        public bool IsCounted { get; set; }

        public ObservableCollection<Scores> DuoScores = new ObservableCollection<Scores>(new Scores[3] { new Scores() { Name = "А", Coef = 0.4 }, new Scores() { Name = "И", Coef = 0.3 }, new Scores() { Name = "Т", Coef = 0.3 } });
        public double OverAllPP
        {
            get
            {
                double sum = 0;
                foreach (Scores item in DuoScores)
                {
                    sum += item.ResultPP;
                }
                return sum;
            }
        }
        public bool Contains(string text)
        {
            return FIO.Contains(text)
                || Category.Contains(text)
                || Team.Contains(text)
                || Year.Contains(text);
        }

        public int CompareTo(object obj)
        {
            if (OverAllPP > ((Duo)obj).OverAllPP) return -1;
            else if (OverAllPP == ((Duo)obj).OverAllPP) return 0;
            else return 1;
        }
    }
}
