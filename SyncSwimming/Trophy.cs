using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSwimming
{
    public class Trophy : Group
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
    }
}
