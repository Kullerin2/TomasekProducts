using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    [Serializable]
    public class Score
    {
        public int PointWin;
        public int PointLoose;        
        public int SetWin;
        public int SetLoose;
        public int GameWin;
        public int GameLoose;
        public List<Match> Matches;
        public int Number;
        //public int Count;
        public Score()
        {

        }

        public void Init(int number)
        {
            PointWin = 0;
            PointLoose = 0;            
            SetWin = 0;
            SetLoose = 0;
            Matches = new List<Match>();
            Number = number;
        }
        
        public void AddMatch(Match match)
        {
            Matches.Add(match);
        }
        public void Clear()
        {
            Matches.Clear();
        }
        public bool IsFirtsPlayer(Match match)
        {
            return (match.P3 != -1 && (Number == match.P1 || Number == match.P2))
                    || (match.P3 == -1 && Number == match.P1);
        } 
       
        public void CountScore()
        {
            PointWin = 0;
            PointLoose = 0;
            SetWin = 0;
            SetLoose = 0;
            foreach (var match in Matches)
            {
                if (IsFirtsPlayer(match)) 
                {
                    PointWin += match.Result.PointWin;
                    PointLoose += match.Result.PointLoose;
                    SetWin += match.Result.SetWin;
                    SetLoose += match.Result.SetLoose;
                }
                else
                {
                    PointWin += match.Result.PointLoose;
                    PointLoose += match.Result.PointWin;
                    SetWin += match.Result.SetLoose;
                    SetLoose += match.Result.SetWin;
                }
            }
        }
        
        public int Point(int round,int setIndex)
        {
            var matches = Matches.Where(r => r.RoundNumber == round);
            int point = 0;
            foreach (var match in matches)
            {
                var result = match.Result;
                if (setIndex == 1)
                {
                    if (IsFirtsPlayer(match))
                    {
                        point += result.PointWin1;
                    }
                    else
                    {
                        point += result.PointLoose1;
                    }
                }
                else if (setIndex == 2)
                {
                    if (IsFirtsPlayer(match))
                    {
                        point += result.PointWin2;
                    }
                    else
                    {
                        point += result.PointLoose2;
                    }
                }
                else
                {
                    if (IsFirtsPlayer(match))
                    {
                        point += result.PointWin3;
                    }
                    else
                    {
                        point += result.PointLoose3;
                    }
                }
            }
            return point;
        }
        public string ShowPoint(int round,bool showAll,bool swissGame)
        {
            var matches = Matches.Where(r => r.RoundNumber == round);
            int gameWin = 0;
            int gameLoose = 0;
            int pointWin = 0;
            int pointLoose = 0;
            if (matches.Count() ==0 )
            {
                return "---";
            }
            foreach (var match in matches)
            {
                var result = match.Result;
                if (IsFirtsPlayer(match))
                {
                    gameWin += result.GameWin;
                    gameLoose += result.GameLoose;
                    pointWin += result.PointWin;
                    pointLoose += result.PointLoose;
                }
                else
                {
                    gameWin += result.GameLoose;
                    gameLoose += result.GameWin;
                    pointWin += result.PointLoose;
                    pointLoose += result.PointWin;
                }                
            }
            if (showAll)
            {

                //string exScore = string.Format("{0} ({1}+{2})", Results[round].PointWin, Results[round].Opo1, Results[round].Opo2);
                if (swissGame)
                {
                    return string.Format("{0}:{1}({2}:{3})", gameWin, gameLoose, pointWin, pointLoose);

                }
                return string.Format("{0}:{1}", pointWin, pointLoose);
            }
            if (swissGame)
            {
                return string.Format("{0}:{1}", gameWin, gameLoose);
            }
            return pointWin+"";
        }

    }
}
