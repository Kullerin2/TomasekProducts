using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tournament
{
    [Serializable]
    public class Round
    {
        public List<Match> Matches;
        public List<Player> Sits;
        public int RoundNumber;
        public Round()
        {
            Matches = new List<Match>();
            Sits = new List<Player>();
        }
    }
    [Serializable]
    public class Rounds
    {
        [NonSerialized]
        [XmlIgnore]
        public DataModel Model;        

        public List<Round> AllRounds = new List<Round>();
        private int CourtIndex = 0;
        public List<int> InitAvailableCourts()
        {
            List<int> availableCourts = new List<int>();
            for (int i = 0; i < Model.CourtCount; i++)
            {
                availableCourts.Add(i);
            }
            return availableCourts;
        }
        public void RemoveRound()
        {
            AllRounds.Remove(AllRounds.Last());
        }
        public void AddRound(int[] round)
        {            
            Round newRound = new Round();
            newRound.RoundNumber = AllRounds.Count;
            List<Player> usedPlayers = new List<Player>();
            CourtIndex = 0;
            List<int> availableCourts = InitAvailableCourts();
            
            if (Model.Game == GameType.Single)
            {
                for (int i = 0; i + 1 < round.Length; i += 2)
                {
                    Match match = new Match();
                    match.P1 = round[i];
                    match.P2 = round[i + 1];                    
                    if (match.P1 >= 0 && match.P2 >= 0)
                    {
                        match.Player1 = Model.Players[match.P1];
                        match.Player2 = Model.Players[match.P2];                        
                        usedPlayers.Add(match.Player1);
                        usedPlayers.Add(match.Player2);                        
                        match.RoundNumber = newRound.RoundNumber;
                        ChooseCourtNumber(match, ref availableCourts);
                        
                        AddResult(match);
                        newRound.Matches.Add(match);
                    }
                }
            }
            else
            {
                for (int i = 0; i + 3 < round.Length; i += 4)
                {
                    Match match = new Match();
                    match.P1 = round[i];
                    match.P2 = round[i + 1];
                    match.P3 = round[i + 2];
                    match.P4 = round[i + 3];
                    //if (match.P1 >= 0 && match.P2 >= 0 && match.P3 >= 0 && match.P4 >= 0 && !DataModel.SamePlayer(match.P1, match.P2, match.P3, match.P4))
                    if (match.P1 >= 0 && match.P2 >= 0 && match.P3 >= 0 && match.P4 >= 0 )
                        {
                        match.Player1 = Model.Players[match.P1];
                        match.Player2 = Model.Players[match.P2];
                        match.Player3 = Model.Players[match.P3];
                        match.Player4 = Model.Players[match.P4];
                        usedPlayers.Add(match.Player1);
                        usedPlayers.Add(match.Player2);
                        usedPlayers.Add(match.Player3);
                        usedPlayers.Add(match.Player4);
                        match.RoundNumber = newRound.RoundNumber;
                        ChooseCourtNumber(match, ref availableCourts);
                        AddResult(match);
                        newRound.Matches.Add(match);
                    }
                }
            }
            foreach (var player in Model.Players)
            {
                if (!usedPlayers.Contains(player))
                {
                    newRound.Sits.Add(player);
                }
            }
            AllRounds.Add(newRound);
        }
        
        public void AddResult(Match match)
        {
            //Single
            if (match.P3 == -1)
            {
                int pi1 = match.P1;
                int pi2 = match.P2;
                if (pi1 >= 0 && pi2 >= 0)
                {
                    match.Result = new MatchResult();
                    match.Player1.Score.AddMatch(match);
                    match.Player2.Score.AddMatch(match);
                }
            }
            else
            {
                int pi1 = match.P1;
                int pi2 = match.P2;
                int pi3 = match.P3;
                int pi4 = match.P4;
                if (pi1 >= 0 && pi2 >= 0 && pi3 >= 0 && pi4 >= 0)
                {
                    match.Result = new MatchResult();
                    match.Player1.Score.AddMatch(match);
                    match.Player2.Score.AddMatch(match);
                    match.Player3.Score.AddMatch(match);
                    match.Player4.Score.AddMatch(match);
                }
            }
        }
        public int Count { get { return AllRounds.Count; } }
        public Round LastRound
        {
            get { return AllRounds[Model.RoundIndex]; }
        }
        public void Clear()
        {
            AllRounds.Clear();
            foreach (Player player in Model.Players)
            {
                player.Score.Clear();
                player.InitCourts(Model.CourtCount);
            }
        }
        public void ChooseCourtNumber(Match match,ref List<int> courts)
        {          
            if (courts.Count == 0)
            {
                courts = InitAvailableCourts();
                CourtIndex += Model.CourtCount;
            }
            int sum = -1;
            int minCourt = courts[0];
            if (Model.Game == GameType.Single)
            {
                foreach (int court in courts)
                {
                    int sum1 = match.Player1.Courts[court]
                        + match.Player2.Courts[court];
                        
                    if (sum1 < sum || sum == -1)
                    {
                        minCourt = court;
                        sum = sum1;
                    }
                }
                courts.Remove(minCourt);
                match.Player1.Courts[minCourt]++;
                match.Player2.Courts[minCourt]++;                
                match.Court = minCourt + 1;
                match.CourtIndex = match.Court + CourtIndex;
            }
            else
            {
                foreach (int court in courts)
                {
                    int sum1 = match.Player1.Courts[court]
                        + match.Player2.Courts[court]
                        + match.Player3.Courts[court]
                        + match.Player4.Courts[court];
                    if (Model.Game == GameType.Random)
                    {
                        minCourt = court;
                        break;
                    }
                    if (sum1 < sum || sum == -1)
                    {
                        minCourt = court;
                        sum = sum1;
                    }
                }
                courts.Remove(minCourt);
                match.Player1.Courts[minCourt]++;
                match.Player2.Courts[minCourt]++;
                match.Player3.Courts[minCourt]++;
                match.Player4.Courts[minCourt]++;
                match.Court = minCourt + 1;
                match.CourtIndex = match.Court + CourtIndex;
            }
        }
    }
}
