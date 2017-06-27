using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSwimming
{
    public class ComparerParticipant : IComparer<Participant>
    {
        public int Compare(Participant x, Participant y)
        {
            if (x.Position > y.Position) return 1;
            else if (x.Position < y.Position) return -1;
            else return 0;
        }
    }
    public class ComparerDuo : IComparer<Duo>
    {
        public int Compare(Duo x, Duo y)
        {
            if (x.Position > y.Position) return 1;
            else if (x.Position < y.Position) return -1;
            else return 0;
        }
    }
    public class ComparerGroup : IComparer<Group>
    {
        public int Compare(Group x, Group y)
        {
            if (x.Position > y.Position) return 1;
            else if (x.Position < y.Position) return -1;
            else return 0;
        }
    }
    public class ComparerCombi : IComparer<Combi>
    {
        public int Compare(Combi x, Combi y)
        {
            if (x.Position > y.Position) return 1;
            else if (x.Position < y.Position) return -1;
            else return 0;
        }
    }
    public class ComparerTrophy : IComparer<Trophy>
    {
        public int Compare(Trophy x, Trophy y)
        {
            if (x.Position > y.Position) return 1;
            else if (x.Position < y.Position) return -1;
            else return 0;
        }
    }
}
