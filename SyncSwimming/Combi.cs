using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSwimming
{
    [Serializable]
    public class Combi : IComparable
    {
        public List<Participant> GroupP { get; set; }
        public Participant[] Subs { get; set; }
        public int Position { get; set; }
        public Participant this[int index]
        {
            get
            {
                return GroupP[index];
            }
            set
            {
                GroupP[index] = value;
            }
        }
        public Combi(List<Participant> group)
        {
            GroupP = group;
            foreach (Participant item in GroupP)
            {
                FIO += item.FIO + "\n";
                Year += item.Year + "\n";
                Category += item.Category + "\n";
            }
            FIO = FIO.TrimEnd('\n');
            Year = Year.TrimEnd('\n');
            Category = Category.TrimEnd('\n');
            Team = group[0].Team;
            GroupScores = new ObservableCollection<Scores>(new Scores[3] { new Scores() { Name = "А", Coef = 1 }, new Scores() { Name = "И", Coef = 1 }, new Scores() { Name = "С", Coef = 1 } });
        }
        public string FIO { get; set; }
        public string Year { get; set; }
        public string Category { get; set; }
        public string Team { get; set; }
        public bool IsCounted { get; set; }

        public ObservableCollection<Scores> GroupScores = new ObservableCollection<Scores>(new Scores[3] { new Scores() { Name = "А", Coef = 1 }, new Scores() { Name = "И", Coef = 1 }, new Scores() { Name = "С", Coef = 1 } });
        public double OverAllPP
        {
            get
            {
                double sum = 0;
                foreach (Scores item in GroupScores)
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
            if (OverAllPP > ((Combi)obj).OverAllPP) return -1;
            else if (OverAllPP == ((Combi)obj).OverAllPP) return 0;
            else return 1;
        }
    }
}

