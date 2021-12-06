using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tournament
{
    [Serializable]
    public class Match
    {
        [NonSerialized]
        [XmlIgnore]
        public Player Player1;
        [NonSerialized]
        [XmlIgnore]
        public Player Player2;
        [NonSerialized]
        [XmlIgnore]
        public Player Player3;
        [NonSerialized]
        [XmlIgnore]
        public Player Player4;
        public int P1;
        public int P2;
        public int P3;
        public int P4;
        public MatchResult Result;
        public int Court;
        public int CourtIndex;
        public int RoundNumber;

        public Match()
        {
            P1 = -1;
            P2 = -1;
            P3 = -1;
            P4 = -1;
        }
        public void SetResult(int win1, int loose1, int win2, int loose2, int win3, int loose3)
        {
            Result.PointWin = 0;
            Result.PointLoose = 0;
            Result.PointWin1 = win1;
            Result.PointWin += win1;
            Result.PointLoose1 = loose1;
            Result.PointLoose += loose1;
            Result.PointWin2 = win2;
            Result.PointWin += win2;
            Result.PointLoose2 = loose2;
            Result.PointLoose += loose2;
            Result.PointWin3 = win3;
            Result.PointWin += win3;
            Result.PointLoose3 = loose3;
            Result.PointLoose += loose3;
            Result.SetLoose = 0;
            Result.SetWin = 0;

            if (win1 > loose1)
            {
                Result.SetWin++;
            }
            if (win1 < loose1)
            {
                Result.SetLoose++;
            }
            if (win2 > loose2)
            {
                Result.SetWin++;
            }
            if (win2 < loose2)
            {
                Result.SetLoose++;
            }
            if (win3 > loose3)
            {
                Result.SetWin++;
            }
            if (win3 < loose3)
            {
                Result.SetLoose++;
            }
            if (Player1 != null)
            {
                Player1.Score.CountScore();
            }
            if (Player2 != null)
            {
                Player2.Score.CountScore();
            }
            if (Player3 != null)
            {
                Player3.Score.CountScore();
            }
            if (Player4 != null)
            {
                Player4.Score.CountScore();
            }

        }
    }
}

