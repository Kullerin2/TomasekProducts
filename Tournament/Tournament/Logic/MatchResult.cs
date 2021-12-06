using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    [Serializable]
    public class MatchResult
    {
        public int PointWin = 0;
        public int PointLoose = 0;
        public int PointWin1 = 0;
        public int PointLoose1 = 0;
        public int PointWin2 = 0;
        public int PointLoose2 = 0;
        public int PointWin3 = 0;
        public int PointLoose3 = 0;
        public int SetWin = 0;
        public int SetLoose = 0;        
        //public int SetCount = 1;
        public int GameWin = 0;
        public int GameLoose = 0;
        public MatchResult()
        {

        }
    }
}
