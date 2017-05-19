using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSwimming
{
    public class Participant
    {
        ObservableCollection<Scores> _pScores = new ObservableCollection<Scores>(new Scores[4] { new Scores() { Name = "Ф1" }, new Scores() { Name = "Ф2" }, new Scores() { Name = "Ф3" }, new Scores() { Name = "Ф4" } });
        public string FIO { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public string Team { get; set; }
        public bool IsCounted { get; set; }
        public ObservableCollection<Scores> PersonalScores
        {
            get
            {
                return _pScores;
            }
            set
            {
                _pScores = value;
            }
        }
        public double OverAll
        {
            get
            {
                double coef = 0;
                double sum = 0;
                foreach (Scores item in PersonalScores)
                {
                    coef += item.Coef;
                    sum += item.Result;
                }
                return sum * coef;
            }
        }

        public Participant Copy()
        {
            Participant copy = new Participant();
            copy.FIO = FIO;
            copy.Category = Category;
            copy.Year = Year;
            copy.Team = Team;
            copy.PersonalScores = PersonalScores;
            return copy;
        }
        public bool Contains(string text)
        {
            return FIO.Contains(text)
                || Category.Contains(text)
                || Team.Contains(text)
                || Year.ToString().Contains(text);
        }
    }
}
