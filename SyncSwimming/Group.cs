using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSwimming
{
    public class Group
    {
        public Participant[] GroupP { get; set; }
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
        public Group(Participant[] group)
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
        }
        public string FIO { get; set; }
        public string Year { get; set; }
        public string Category { get; set; }
        public string Team { get; set; }
        public bool IsCounted { get; set; }

        public ObservableCollection<Scores> GroupScores = new ObservableCollection<Scores>(new Scores[3] { new Scores() { Name = "А", Coef = 0.4 }, new Scores() { Name = "И", Coef = 0.3 }, new Scores() { Name = "Т", Coef = 0.3 } });
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
    }
}
