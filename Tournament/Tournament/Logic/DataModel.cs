using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Tournament
{
    [Serializable]
    public class DataModel
    {
        public int Count = 0;
        //public int RoundCount = 0;        
        public List<Player> Players = new List<Player>();
        public int RoundIndex = -1;
        public Round Round;
        public List<int[]> RoundsWithAllPlayers;
        public Rounds RoundsForCourt;
        private string filePathResultFormat = "Tournament";
        
        public string FilePathRoundsFormat = "TournamentRounds";
        //public Losovani Losovani;
        private Losy losy = new Losy();
        public GameType Game;
        public ScoreType ScoreType;
        public int SwissGame=11;
        public int SetCount=1;
        public bool ShowScore;
        public RandomType RandomType;
        public Los Los;
        public List<Los> Losy { get { return losy.losy; } }
        //public List<string> Names = new List<string>();
        public int CourtCount = 1;
        public bool GenerateCourts = false;
        public Language Language;
        public string AppName = string.Empty;
        public string RegisterToken{get;set;}
        public string RegisterKey { get; set;}
        public bool IsRegistred = false;
        public DateTime RegistrDate = DateTime.Now;
        public DateTime TournamentDate = DateTime.Now;
        [XmlIgnore]
        public Helper Helper;
        public bool GameLoaded = false;
        [XmlIgnore]
        public int[] Sits;
        
             

        public bool OneRandomRound
        {
            get
            {
                return this.RandomType != RandomType.FullRandomRoundRobin && this.RandomType != RandomType.Mix;
            }
        }
        public string FilePathResultFormat
        {
            get
            {
                string name = filePathResultFormat;
                if (!string.IsNullOrEmpty(AppName))
                {
                    name = AppName;
                }
                string result = string.Format("{0}_{1}_{2}", DateTime.Now.ToString("yyyy_MM_dd"),name, Count);
                return result;
            }
        }
        public void InitLosy()
        {
            losy = new Losy();
            losy.InitRounds();
            foreach (Los los in losy.losy)
            {
                los.IsEnabled = los.IsEnabled || IsRegistred;
            }
        }
        public void InitRegister()
        {
            LoadRegisterKey();
            GetRegisterToken();
        }
        public void InitHelper()
        {
            Helper = new Helper();
            Helper.Init();
        }
        /*
        public void AddAllResult()
        {
            int roundIndex = 0;
            foreach (var round in RoundsForCourt.AllRounds)
            {
                foreach (var player in Players)
                {
                    player.Score.AddResult(roundIndex);
                }
                foreach(Match match in round.Matches)
                {
                    int pi1 = match.P1;
                    int pi2 = match.P2;
                    int pi3 = match.P3;
                    int pi4 = match.P4;
                    if (pi1 >= 0 && pi2 >= 0 && pi3 >= 0 && pi4 >= 0)
                    {
                        
                        match.Player1.Score.Results[roundIndex].Opo1 = match.Player3.Number;
                        match.Player1.Score.Results[roundIndex].Opo2 = match.Player4.Number;
                        match.Player2.Score.Results[roundIndex].Opo1 = match.Player3.Number;
                        match.Player2.Score.Results[roundIndex].Opo2 = match.Player4.Number;
                        match.Player3.Score.Results[roundIndex].Opo1 = match.Player1.Number;
                        match.Player3.Score.Results[roundIndex].Opo2 = match.Player2.Number;
                        match.Player4.Score.Results[roundIndex].Opo1 = match.Player1.Number;
                        match.Player4.Score.Results[roundIndex].Opo2 = match.Player2.Number;
                    }

                }
                roundIndex++;
            }
        }*/
        /*
        public void AddResult()
        {
            foreach (var player in Players)
            {
                player.Score.AddResult(RoundIndex);
            }
            var round = RoundsForCourt.AllRounds[RoundIndex];
            foreach (Match match in round.Matches)
            {
                var p1 = match.Player1;
                var p2 = match.Player2;
                var p3 = match.Player3;
                var p4 = match.Player4;
                p1.Score.Results[RoundIndex].Opo1 = p3.Number;
                p1.Score.Results[RoundIndex].Opo2 = p4.Number;
                p2.Score.Results[RoundIndex].Opo1 = p3.Number;
                p2.Score.Results[RoundIndex].Opo2 = p4.Number;
                p3.Score.Results[RoundIndex].Opo1 = p1.Number;
                p3.Score.Results[RoundIndex].Opo2 = p2.Number;
                p4.Score.Results[RoundIndex].Opo1 = p1.Number;
                p4.Score.Results[RoundIndex].Opo2 = p2.Number;

            }            
        }*/
        private void CountOneSwissSet(MatchResult match, int win, int loose)
        {
            if (win > 0 || loose > 0)
            {

                if (win > loose)
                {
                    match.GameWin++;
                }
                else
                {
                    match.GameLoose++;
                }

                if (win >= SwissGame)
                {
                    match.GameWin++;
                }
                else
                {
                    match.GameLoose++;
                }
                if (loose >= SwissGame)
                {
                    match.GameLoose++;
                }
                else
                {
                    match.GameWin++;
                }
            }
        }
        public void CountSwissGame()
        {
            if (ScoreType == ScoreType.Games)
            {
                foreach (var player in Players)
                {
                    player.Score.GameWin = 0;
                    player.Score.GameLoose = 0;
                    foreach (Match match in player.Score.Matches)
                    {
                        var result = match.Result;
                        result.GameWin = 0;
                        result.GameLoose = 0;
                        CountOneSwissSet(result, result.PointWin1, result.PointLoose1);
                        CountOneSwissSet(result, result.PointWin2, result.PointLoose2);
                        CountOneSwissSet(result, result.PointWin3, result.PointLoose3);
                        player.Score.GameWin += result.GameWin;
                        player.Score.GameLoose += result.GameLoose;
                    }
                }
            }
        }

        public double Rank(Player p)
        {
            if (ScoreType == ScoreType.Points)
            {
                return p.Score.PointWin*1000 - p.Score.PointLoose;
            }
            else if (ScoreType == ScoreType.Sets )
            {
                var result = p.Score.SetWin * 1000000 + p.MiniSet*100 + p.Score.PointWin - p.Score.PointLoose+(double)p.Score.PointWin / (double)100;
                return result;
            }
            else if (ScoreType == ScoreType.SetsScore)
            {
                var result = p.Score.SetWin * 1000000 +  p.Score.PointWin - p.Score.PointLoose + (double)p.Score.PointWin / (double)100;
                return result;
            }
            else
            {
                return p.Score.GameWin * 1000000 + p.Score.SetWin * 100 + p.Score.PointWin - p.Score.PointLoose + (double)p.Score.PointWin/(double)100;
            }
        }
        public void CreateMiniTables()
        {
            var sortedPlayers = Players.OrderByDescending(o =>o.Score.SetWin).ToList();
            int actualMaxSet = 0;
            List<List<Player>> miniTables = new List<List<Player>>();
            List<Player> actualMiniTable = new List<Player>();
            foreach(Player player in sortedPlayers)
            {                
                if (actualMaxSet != player.Score.SetWin)
                {
                    actualMiniTable = new List<Player>();
                    actualMiniTable.Add(player);
                    miniTables.Add(actualMiniTable);                    
                    actualMaxSet = player.Score.SetWin;
                }
                else
                {
                    actualMiniTable.Add(player);
                }
            }
            foreach (var miniTable in miniTables)
            {
                ResolveMiniTable(miniTable);
            }
        }

        public void ResolveMiniTable(List<Player> miniTable)
        {
            try
            {
                foreach (Player player in miniTable)
                {
                    player.MiniSet = 0;
                    foreach (var match in player.Score.Matches)
                    {
                        var result = match.Result;
                        Player opo1 = match.Player3;
                        Player opo2 = match.Player4;
                        if (match.Player3 == player || match.Player4 == player)
                        {
                            opo1 = match.Player1;
                            opo2 = match.Player2;
                        }

                        if (opo1 != null && opo2 != null)
                        {                            
                            if (miniTable.Contains(opo1) || miniTable.Contains(opo2))
                            {                                
                                if (player == match.Player1 || player == match.Player2)
                                {
                                    player.MiniSet += result.SetWin * 100 + result.PointWin - result.PointLoose;
                                }
                                else
                                {
                                    player.MiniSet += result.SetLoose * 100 + result.PointLoose - result.PointWin;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }


        }
        public void SortPlayers()
        {
            CountSwissGame();
            if (ScoreType == ScoreType.Sets)
            {
                CreateMiniTables();
            }
            var sortedPlayers = Players.OrderByDescending(o => Rank(o)).ToList();
            int index = 1;
            double actualSum = -1;
            int actualNo = 1;
            foreach (var player in sortedPlayers)
            {
                if (Rank(player) == actualSum)
                {
                    player.No = actualNo;
                }
                else
                {
                    player.No = index;
                }
                actualSum = Rank(player);
                actualNo = player.No;
                if (Game == GameType.Teams)
                {
                    player.No = (player.No + 1) / 2;
                }
                index++;
            }
        }
        public static bool SamePlayer (int p1,int p2, int p3,int p4)
        {            
            return ((p1 == p3 && p2 == p4) || (p1 == p4 && p2 == p3));
        }
        public void InitPlayerCours()
        {
            foreach (Player player in Players)
            {

            }
        }
        public void ClearScore()
        {
            foreach (Player player in Players)
            {
                player.Score.Clear();
            }
            foreach (Player player in Players)
            {
                player.Score.CountScore();
            }
        }
        public void CountScore()
        {
            foreach (Player player in Players)
            {
                player.Score.CountScore();
            }
        }
        public void LoadModel()
        {
            ClearScore();
            if (RoundsForCourt != null)
            {
                RoundsForCourt.Model = this;
                foreach (Round round in RoundsForCourt.AllRounds)
                {
                    foreach (Match match in round.Matches)
                    {
                        match.Player1 = Players[match.P1];
                        match.Player2 = Players[match.P2];
                        match.Player3 = Players[match.P3];
                        match.Player4 = Players[match.P4];
                        match.Player1.Score.AddMatch(match);
                        match.Player2.Score.AddMatch(match);
                        if (match.Player1 != match.Player3 && match.Player2 != match.Player4)
                        {
                            match.Player3.Score.AddMatch(match);
                            match.Player4.Score.AddMatch(match);
                        }
                    }
                }
            }
            CountScore();
            
        }
        private string GetMacAddress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                
                if (nic.NetworkInterfaceType ==  NetworkInterfaceType.Ethernet)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return macAddresses;
        }
        public void GetRegisterToken()
        {
            //string key = string.Format("{0},{1}", GetMacAddress(), DateTime.Now.ToShortDateString());
            string key = string.Format("{0}", GetMacAddress());
            
            RegisterToken = CryptoHelper.EncryptString(key);
            
        }
        public bool CheckRegisterKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }
            else
            {
                try
                {
                    if (CheckKey(key))
                    {
                        SaveRegisterKey();
                        return true;
                    }
                    return false;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
            
        }
        private bool CheckKey(string key)
        {
            if (DateTime.Now.Year < 2023)
                return true;            
            string clearKey = CryptoHelper.DecryptString(key);
            string[] tmp = clearKey.Split(',');
            string mac1 = string.Empty;
            string mac2 = string.Empty;
            if (tmp[0] == "Temporary")
            {

            }
            else
            {
                mac1 = GetMacAddress();
                if (string.IsNullOrEmpty(mac1))
                {
                    return true;
                }
                mac2 = tmp[0];
            }

            DateTime date = DateTime.Parse(tmp[1]);
            if (mac1 == mac2 && date > DateTime.Now)
            {
                IsRegistred = true;
                RegistrDate = date;
                RegisterKey = key;                
                return true;
            }
            return false;
        }
        public void SaveRegisterKey()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory+"RegisterKey.ini";
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(RegisterKey);
            sw.Flush();
            sw.Close();
        }
        public void LoadRegisterKey()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "RegisterKey.ini";
            try
            {
                StreamReader sr = new StreamReader(path);
                string key =sr.ReadLine();                
                sr.Close();
                IsRegistred = CheckKey(key);
            }
            catch(Exception ex)
            {

            }
        }
    }
}

