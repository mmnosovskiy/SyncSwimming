using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSwimming
{
    [Serializable]
    public class Trophy : Group, IComparable
    {
        public Scores TrophyScores = new Scores() { Name = "А" };

        public double OverAllT
        {
            get
            {
                return TrophyScores.ResultT;
            }
        }

        public Trophy(Participant[] group) : base(group)
        {
        }

        public new int CompareTo(object obj)
        {
            if (OverAllT > ((Trophy)obj).OverAllT) return -1;
            else if (OverAllT == ((Trophy)obj).OverAllT) return 0;
            else return 1;
        }
    }
}
