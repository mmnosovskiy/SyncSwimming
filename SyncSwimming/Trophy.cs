using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSwimming
{
    [Serializable]
    public class Trophy : IComparable
    {
        public Scores TrophyScores = new Scores() { Name = "А" };
        public List<Participant> Members { get; set; }

        public string FIO { get; set; }
        public string Year { get; set; }
        public string Category { get; set; }
        public string Team { get; set; }
        public bool IsCounted { get; set; }
        public int Position { get; set; }

        public double OverAllT
        {
            get
            {
                return TrophyScores.ResultT;
            }
        }

        public Participant this[int index]
        {
            get
            {
                return Members[index];
            }
            set
            {
                Members[index] = value;
            }
        }

        public Trophy(List<Participant> group) 
        {
            Members = group;
            foreach (Participant item in group )
            {
                FIO += item.FIO + "\n";
                Year += item.Year + "\n";
                Category += item.Category + "\n";
            }
            FIO = FIO.TrimEnd('\n');
            Year = Year.TrimEnd('\n');
            Category = Category.TrimEnd('\n');
            Team = group[0].Team;
        }

        public int CompareTo(object obj)
        {
            if (OverAllT > ((Trophy)obj).OverAllT) return -1;
            else if (OverAllT == ((Trophy)obj).OverAllT) return 0;
            else return 1;
        }
        public bool Contains(string text)
        {
            return FIO.Contains(text)
                || Category.Contains(text)
                || Team.Contains(text)
                || Year.Contains(text);
        }
    }
}
