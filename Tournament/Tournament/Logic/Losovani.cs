using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    [Serializable]
    public class Losovani
    {
        public List<int[]> Matches;
        Permutace permutace;
        public double OcIndex = 4;
        public int[,] opo;
        public List<int> sit;
        public DataModel Model;
        public int[] Base = {2,3,5,7,11,13,17,19,23,29,31 ,  37,  41,  43,  47,  53,  59 , 61 , 67,  71,73 , 79,  83,  89,  97,  101,     103,     107 ,       113,
127 ,  131 ,  137 ,  139 ,  149  ,   151 ,   157 ,   163 ,   167  ,  173  ,
179 ,  181 ,  191 ,  193 ,  197  ,   199 ,   211 ,   223 ,   227  ,  229  ,
233 ,  239 ,  241 ,  251 ,  257  ,   263 ,   269 ,   271 ,   277  ,  281  ,
283 ,  293 ,  307 ,  311 ,  313  ,   317 ,   331 ,   337 ,   347  ,  349  ,
353 ,  359 ,  367 ,  373 ,  379  ,   383 ,   389 ,   397 ,   401  ,  409  ,
419 ,  421 ,  431 ,  433 ,  439  ,   443 ,   449 ,   457 ,   461  ,  463  ,
467 ,  479 ,  487 ,  491 ,  499  ,   503 ,   509 ,   521 ,   523  ,  541  ,
547 ,  557 ,  563 ,  569 ,  571  ,   577 ,   587 ,   593 ,   599  ,  601  ,
607 ,  613 ,  617 ,  619 ,  631  ,   641 ,   643 ,   647 ,   653  ,  659  ,
661 ,  673 ,  677 ,  683 ,  691  ,   701 ,   709 ,   719 ,   727  ,  733  ,
739 ,  743 ,  751 ,  757 ,  761  ,   769 ,   773 ,   787 ,   797  ,  809  ,
811 ,  821 ,  823 ,  827 ,  829  ,   839 ,   853 ,   857 ,   859  ,  863  ,
877 ,  881 ,  883 ,  887 ,  907  ,   911 ,   919 ,   929 ,   937  ,  941  ,
947 ,  953 ,  967 ,  971 ,  977  ,   983 ,   991 ,   997 ,   1009 ,  1013 };
        public Losovani()
        {
            
        }
        public Losovani(DataModel model, bool isLoad)
        {
            Model = model;
            if (!Model.GenerateCourts)
            {
                if (Model.Game == GameType.Single)
                {
                    Model.CourtCount = (Model.Players.Count - 1) / 2 + 1;
                }
                else
                {
                    Model.CourtCount = (Model.Players.Count - 1) / 4 + 1;
                }

            }
            if (Model.Game == GameType.Random && Model.OneRandomRound)
            {
                NewPermutace(isLoad);
                if (Model.RoundsWithAllPlayers == null || !isLoad)
                {
                    Model.RoundsWithAllPlayers = new List<int[]>();
                }

            }
            else if (Model.Game == GameType.RandomSwiss)
            {
                NewPermutace(isLoad);
                if (Model.RoundsWithAllPlayers == null || !isLoad)
                {
                    Model.RoundsWithAllPlayers = new List<int[]>();
                }

            }

            else if (Model.RoundsWithAllPlayers == null || !isLoad)
            {
                if (!Model.OneRandomRound && Model.Game == GameType.Random)
                {
                    while (true) {
                        GenerateRounds();
                        if (Model.RandomType != RandomType.Mix || CheckLosovaniAllMix())
                        {
                            break;
                        }
                    }                    
                }
                else
                {
                    Model.RoundsWithAllPlayers = Model.Los.Rounds;
                }

            }
            InitPlayerCourts();
            if (Model.RoundsForCourt != null &&Model.RoundsForCourt.Count == 0)
            {
                while (true)
                {
                    GenerateRoundsForCourts();
                    if (CheckRepeatSits())
                    {
                        break;
                    }
                }
            }

        }
        public bool CheckRepeatSits()
        {
            bool result = true;
            if (Model.Game == GameType.Random)
            {
                return result;
            }
            if (Model.GenerateCourts && Model.Count / 4 > Model.CourtCount)
            {
                List<int> sits = new List<int>();
                for(int i = 0; i < Model.RoundsForCourt.Count-1; i++)
                {
                    var round = Model.RoundsForCourt.AllRounds[i];
                    foreach (int sit in sits)
                    {
                        if (round.Sits.Any(p => p.Number== sit))
                        {
                            return false;
                        }
                    }
                    sits = new List<int>();
                    foreach (Player p in round.Sits)
                    {
                        sits.Add(p.Number);
                    }
                }
            }
            return result;
        }
        public void InitPlayerCourts()
        {
            foreach (Player player in Model.Players)
            {
                player.InitCourts(Model.CourtCount);
            }
        }
        public Round GetNextRound()
        {
            Model.RoundIndex++;
            if (Model.Game == GameType.Random && Model.OneRandomRound)
            {
                if (Model.RoundIndex >= Model.RoundsWithAllPlayers.Count)
                {
                    var newRound = GetNextRandomRound();
                    if (newRound != null)
                    {
                        Model.RoundsWithAllPlayers.Add(newRound);
                        AddNewRound(newRound, false);
                        CheckLosovani();
                        //Model.AddResult();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            if (Model.Game == GameType.RandomSwiss)
            {
                if (Model.RoundIndex >= Model.RoundsWithAllPlayers.Count)
                {
                    int[] newRound = null;
                    if (Model.RoundIndex == 0)
                    {
                        newRound = GetNextRandomRound();
                    }
                    else
                    {
                        newRound = GetNextRandomSwissRound();
                    }
                    if (newRound != null)
                    {
                        Model.RoundsWithAllPlayers.Add(newRound);
                        AddNewRound(newRound, false);                        
                        
                    }
                    else
                    {
                        return null;
                    }
                }
            }


            return Model.RoundsForCourt.LastRound;

        }
        private void NewPermutace(bool isLoad)
        {
            permutace = new Permutace(Model.Players.Count, Model.RandomType == RandomType.Mix);
            Matches = permutace.Matches;
            if (!isLoad)
            {
                Model.RoundsWithAllPlayers = new List<int[]>();
                opo = new int[Model.Players.Count, Model.Players.Count];
                sit = new List<int>();
                if (Model.OneRandomRound)
                {

                }
            }
        }
        public void ClearRound()
        {
            int index = Model.RoundsForCourt.Count-1;
            while  (index > Model.RoundIndex) {
                Model.RoundsForCourt.RemoveRound();
                Model.RoundsWithAllPlayers.RemoveAt(index);
                index = Model.RoundsForCourt.Count - 1;

            }
        }

        private void GenerateRounds()
        {
            int index = 0;
            NewPermutace(false);
            int maxAttempt = 0;
            int count = Model.Count - 1 + (Model.Count % 4);
            if (Model.RandomType == RandomType.Mix)
            {
                OcIndex = Model.Count/4;
                count = count / 2 + count%2;
            }
            //while (index != count)
            while (Matches.Count >0)
            {
                bool isLastRound = Model.RoundsWithAllPlayers.Count >= count-1;
                var newRound = GetNextRandomRound(isLastRound);
                if (newRound == null)
                {
                    index = 0;
                    maxAttempt++;
                    NewPermutace(false);
                }
                else
                {
                    Model.RoundsWithAllPlayers.Add(newRound);
                    index++;
                }
                if (maxAttempt > 200)
                {
                    OcIndex -= 0.2;
                    maxAttempt = 0;
                }
                if (OcIndex < 0)
                {
                    break;
                }
            }
            if (Model.RandomType == RandomType.Mix)
            {
                var newRound = AddSpecialRowInMix();
                if (newRound != null)
                {
                    Model.RoundsWithAllPlayers.Add(newRound);
                }
            }
        }
        public bool CheckLosovaniAllMix()
        {
            Dictionary<int, List<int>> all = new Dictionary<int, List<int>>();
            for (int i = 0; i < Model.Players.Count; i++)
            {
                all.Add(i, new List<int>());
            }
            foreach (var match in Model.RoundsWithAllPlayers)
            {
                for (int i = 0; i + 3 < match.Length; i += 4)
                {
                    var p1 = match[i];
                    var p2 = match[i + 1];
                    var p3 = match[i + 2];
                    var p4 = match[i + 3];
                    if (p1 >= 0 && p2 >= 0 && p3 >= 0 && p4 >= 0)
                    {
                        AddPlayerToList(all[p1], p2);
                        AddPlayerToList(all[p2], p1);
                        AddPlayerToList(all[p3], p4);
                        AddPlayerToList(all[p4], p3);
                    }
                }
            }
            bool result = true;
            for(int i = 0; i < Model.Players.Count;i++)
            {
                var list = all[i];
                if (list.Count < Model.Players.Count / 2)
                {
                    return false;
                }
            }
            return result;
        }
        private int[] AddSpecialRowInMix()
        {
            List<int> newRound = new List<int>();
            Dictionary<int, List<int>> all = new Dictionary<int, List<int>>();
            for (int i = 0; i < Model.Players.Count; i++)
            {
                all.Add(i, new List<int>());
            }
            foreach (var match in Model.RoundsWithAllPlayers)
            {
                for (int i = 0; i+3 < match.Length; i += 4)
                {
                    var p1 = match[i];
                    var p2 = match[i + 1];
                    var p3 = match[i + 2];
                    var p4 = match[i + 3];
                    AddPlayerToList(all[p1], p2);
                    AddPlayerToList(all[p2], p1);
                    AddPlayerToList(all[p3], p4);
                    AddPlayerToList(all[p4], p3);
                }
            }
            for (int i = 0; i < Model.Players.Count; i += 2)
            {
                var co = all[i];
                for (int j = 1; j < Model.Players.Count; j += 2)
                {
                    if (!co.Contains(j))
                    {
                        newRound.Add(i);
                        newRound.Add(j);
                        newRound.Add(i);
                        newRound.Add(j);
                    }

                }
            }
            if (newRound.Count > 0)
            {
                for (int i = newRound.Count; i < Model.Players.Count; i++)
                {
                    newRound.Add(-1);
                }
                return newRound.ToArray();
            }

            return null;

        }
        private void CheckLosovani()
        {
            Dictionary<int, List<int>> co = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> op = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> all = new Dictionary<int, List<int>>();
            for (int i = 0; i < Model.Players.Count; i++)
            {
                co.Add(i, new List<int>());
                op.Add(i, new List<int>());
                all.Add(i, new List<int>());
            }

            foreach (var match in Model.RoundsWithAllPlayers)
            {
                for (int i = 0; i < match.Length; i += 4)
                {
                    var p1 = match[i];
                    var p2 = match[i + 1];
                    var p3 = match[i + 2];
                    var p4 = match[i + 3];

                    AddPlayerToList(all[p1], p2);
                    AddPlayerToList(all[p1], p3);
                    AddPlayerToList(all[p1], p4);
                    AddPlayerToList(all[p2], p1);
                    AddPlayerToList(all[p2], p3);
                    AddPlayerToList(all[p2], p4);
                    AddPlayerToList(all[p3], p1);
                    AddPlayerToList(all[p3], p2);
                    AddPlayerToList(all[p3], p4);
                    AddPlayerToList(all[p4], p1);
                    AddPlayerToList(all[p4], p2);
                    AddPlayerToList(all[p4], p3);

                }
            }
        }
        private bool AddPlayerToList(List<int> list,int p2)
        {
            if (list.Contains(p2))
            {
                return false;
            }
            else
            {
                list.Add(p2);
                return true;
            }
        }
        private void CheckLosovani2()
        {
            Dictionary<int, List<int>> co = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> op = new Dictionary<int, List<int>>();
            for (int i = 0; i < Model.Players.Count; i++)
            {
                co.Add(i, new List<int>());
                op.Add(i, new List<int>());
            }
            foreach (var match in Model.RoundsWithAllPlayers)
            {
                for (int i = 0; i < match.Length; i += 4)
                {
                    var p1 = match[i];
                    var p2 = match[i + 1];
                    var p3 = match[i + 2];
                    var p4 = match[i + 3];
                    co[p1].Add(p2);
                    co[p2].Add(p1);
                    co[p3].Add(p4);
                    co[p4].Add(p3);
                    op[p1].Add(p3);
                    op[p1].Add(p4);
                    op[p2].Add(p3);
                    op[p2].Add(p4);
                    op[p3].Add(p1);
                    op[p3].Add(p2);
                    op[p4].Add(p1);
                    op[p4].Add(p2);
                }

            }
            foreach (var match in Model.RoundsWithAllPlayers)
            {
                for (int i = 0; i < match.Length; i += 4)
                {
                    var p1 = match[i];
                    var p2 = match[i + 1];
                    var p3 = match[i + 2];
                    var p4 = match[i + 3];
                    co[p1].Add(p2);
                    co[p2].Add(p1);
                    co[p3].Add(p4);
                    co[p4].Add(p3);
                    op[p1].Add(p3);
                    op[p1].Add(p4);
                    op[p2].Add(p3);
                    op[p2].Add(p4);
                    op[p3].Add(p1);
                    op[p3].Add(p2);
                    op[p4].Add(p1);
                    op[p4].Add(p2);
                }

            }
            foreach (var key in co.Keys)
            {
                int n = co.Count() / 2;
                var sum = co[key].Sum();
                if (key % 2 == 0)
                {
                    if (sum != (n) * (n))
                    {
                        // throw new Exception();
                    }
                }
                else
                {
                    if (sum != (n) * (n - 1))
                    {
                        // throw new Exception();
                    }
                }
            }
            foreach (var key in op.Keys)
            {
                //GetMaxOppacity(key, op[key]);
            }
        }
        private void GetMaxOppacity(int key, List<int> list)
        {
            int[] opp = new int[(list.Count)];
            int max = 0;
            int maxNo = 0;
            foreach (int p in list)
            {
                opp[p]++;
                if (max < opp[p])
                {
                    max = opp[p];
                    maxNo = p;
                }
            }
            int mix = opp.Min();

        }
        public int[] GetNextRandomRound()
        {
            return GetNextRandomRound(false);
        }
        public int[] GetNextRandomRound(bool lastRound)
        {
            var usedPlayers = InitPlayerList();
            int index = 0;
            int count = 0;
            int reset = 0;
            Stack<int[]> result = new Stack<int[]>();
            Random random = new Random();
            while (usedPlayers.Count > 3 && reset < Matches.Count)
            {
                if (count > Matches.Count * 4)
                {
                    reset++;
                    if (reset < Matches.Count)
                    {
                        count = 0;
                        usedPlayers = InitPlayerList();
                        result = new Stack<int[]>();
                    }
                }
                int[] match;
                index = random.Next(Matches.Count);
                match = Matches[index];
                if (CheckMatch(match, usedPlayers))
                {
                    PushMatch(match, result, usedPlayers);
                }
                else
                {
                    count++;
                }
                /*if (usedPlayers.Count < 4) {
                    foreach (var notUsedPlayer in usedPlayers) {
                        if (sit.Contains(notUsedPlayer)){
                            reset++;
                            count = 0;
                            usedPlayers = InitPlayerList();
                            result = new Stack<int[]>();
                        }
                    }                    
                }*/

                //index++;
            }
            if (result.Count == Model.Players.Count / 4 || lastRound)
            {
                var list = new List<int>();
                foreach (var match in result)
                {
                    list.Add(match[0]);
                    list.Add(match[1]);
                    list.Add(match[2]);
                    list.Add(match[3]);
                }
                foreach (var notUsedPlayer in usedPlayers)
                {
                    if (!lastRound)
                    {
                        list.Add(notUsedPlayer);
                    }
                    sit.Add(notUsedPlayer);
                }
                var array = list.ToArray();
                if (!Model.OneRandomRound)
                {
                    UseMatch(array);
                }
                RemoveMatches(array);
                //RemoveMatches(array);

                return array;
            }

            return null;
        }

        public int[] GetNextRandomSwissRound()
        {
            return GetNextRandomSwissRound(false);
        }
        public int[] GetNextRandomSwissRound(bool lastRound)
        {
            var usedPlayers = InitPlayerByRank();
            int index = 0;
            int count = 0;
            int reset = 0;
            Stack<int[]> result = new Stack<int[]>();
            while (usedPlayers.Count > 3)
            {
                int[] match = new int[4];
                match[0] = usedPlayers[index];
                match[1] = usedPlayers[index+3];
                match[2] = usedPlayers[index + 1];
                match[3] = usedPlayers[index + 2];
                PushMatch(match, result, usedPlayers);                
            }
            if (result.Count >= (Model.Players.Count+3) / 4)
            {
                var list = new List<int>();
                foreach (var match in result)
                {
                    list.Add(match[0]);
                    list.Add(match[1]);
                    list.Add(match[2]);
                    list.Add(match[3]);
                }
                foreach (var notUsedPlayer in usedPlayers)
                {                    
                    sit.Add(notUsedPlayer);
                }
                var array = list.ToArray();              

                return array;
            }
            return null;
        }
        public void UseMatch(int[] match)
        {
            for (int i = 0; i + 3 < match.Length; i = i + 4)
            {
                int player1 = match[i];
                int player2 = match[i + 1];
                int player3 = match[i + 2];
                int player4 = match[i + 3];

                opo[player1, player3]++;
                opo[player1, player4]++;
                opo[player2, player3]++;
                opo[player2, player4]++;
                opo[player3, player1]++;
                opo[player3, player2]++;
                opo[player4, player1]++;
                opo[player4, player2]++;

            }
        }
        private int[] PopMatch(Stack<int[]> result, List<int> usedPlayers)
        {
            int[] match = result.Pop();
            usedPlayers.Add(match[0]);
            usedPlayers.Add(match[1]);
            usedPlayers.Add(match[2]);
            usedPlayers.Add(match[3]);
            return match;
        }
        private void PushMatch(int[] match, Stack<int[]> result, List<int> usedPlayers)
        {
            usedPlayers.Remove(match[0]);
            usedPlayers.Remove(match[1]);
            usedPlayers.Remove(match[2]);
            usedPlayers.Remove(match[3]);
            result.Push(match);
        }
        public void WriteMatch(StreamWriter sw, int[] match)
        {
            sw.WriteLine("{0},{1},{2},{3}", match[0], match[1], match[2], match[3]);
        }
        public void RemoveMatches(Round round)
        {
            List<int> newRound = new List<int>();
            foreach (var match in round.Matches)
            {
                newRound.Add(match.P1);
                newRound.Add(match.P2);
                newRound.Add(match.P3);
                newRound.Add(match.P4);
            }
            RemoveMatches(newRound.ToArray());
        }
        public bool CheckCoPlayer(int p1, int p2, int p3, int p4, int q1, int q2, int q3, int q4)
        {
            bool result = false;
            result |= p1 == q1 && p2 == q2;
            result |= p1 == q2 && p2 == q1;
            result |= p3 == q3 && p4 == q4;
            result |= p3 == q4 && p4 == q3;

            result |= p1 == q1 && p3 == q3;
            result |= p1 == q1 && p3 == q4;
            result |= p1 == q1 && p4 == q3;
            result |= p1 == q1 && p4 == q4;
            result |= p1 == q2 && p3 == q3;
            result |= p1 == q2 && p3 == q4;
            result |= p1 == q2 && p4 == q3;
            result |= p1 == q2 && p4 == q4;

            result |= p2 == q1 && p3 == q3;
            result |= p2 == q1 && p3 == q4;
            result |= p2 == q1 && p4 == q3;
            result |= p2 == q1 && p4 == q4;
            result |= p2 == q2 && p3 == q3;
            result |= p2 == q2 && p3 == q4;
            result |= p2 == q2 && p4 == q3;
            result |= p2 == q2 && p4 == q4;

            result |= p3 == q3 && p1 == q1;
            result |= p3 == q3 && p1 == q2;
            result |= p3 == q3 && p2 == q1;
            result |= p3 == q3 && p2 == q2;
            result |= p3 == q4 && p1 == q1;
            result |= p3 == q4 && p1 == q2;
            result |= p3 == q4 && p2 == q1;
            result |= p3 == q4 && p2 == q2;

            result |= p4 == q3 && p1 == q1;
            result |= p4 == q3 && p1 == q2;
            result |= p4 == q3 && p2 == q1;
            result |= p4 == q3 && p2 == q2;
            result |= p4 == q4 && p1 == q1;
            result |= p4 == q4 && p1 == q2;
            result |= p4 == q4 && p2 == q1;
            result |= p4 == q4 && p2 == q2;
            return result;
        }
        public bool CheckOpoWomanPlayer(int p1, int p2, int p3, int p4, int q1, int q2, int q3, int q4)
        {
            if ((q1 + q2) % 2 == 0) return true;
            if ((q3 + q4) % 2 == 0) return true;
            //return CheckOpoPlayer(p1, p2, p3, p4, q1, q2, q3, q4);
            return CheckCoPlayer(p1, p2, p3, p4, q1, q2, q3, q4);
        }
        public bool CheckOpoPlayer(int p1, int p2, int p3, int p4, int q1, int q2, int q3, int q4)
        {
            long s= ((long)Base[p1]) * ((long)Base[p2]) * ((long)Base[p3]) * ((long)Base[p4]);
            int ok = 0;
            if (s % Base[q1] == 0)
            {
                ok++;
            }
            if (s % Base[q2] == 0)
            {
                ok++;
            }
            if (s % Base[q3] == 0)
            {
                ok++;
            }
            if (s % Base[q4] == 0)
            {
                ok++;
            }
            bool result = ok > 1;
            return result;
        }
        public bool CheckOpoPlayer2(int p1, int p2, int p3, int p4, int q1, int q2, int q3, int q4)
        {
            bool result = false;
            result |= p1 == q1 && p2 == q2;
            result |= p1 == q1 && p2 == q3;
            result |= p1 == q1 && p2 == q4;
            result |= p1 == q1 && p3 == q2;
            result |= p1 == q1 && p3 == q3;
            result |= p1 == q1 && p3 == q4;
            result |= p1 == q1 && p4 == q2;
            result |= p1 == q1 && p4 == q3;
            result |= p1 == q1 && p4 == q4;

            result |= p1 == q2 && p2 == q1;
            result |= p1 == q2 && p2 == q3;
            result |= p1 == q2 && p2 == q4;
            result |= p1 == q2 && p3 == q1;
            result |= p1 == q2 && p3 == q3;
            result |= p1 == q2 && p3 == q4;
            result |= p1 == q2 && p4 == q1;
            result |= p1 == q2 && p4 == q3;
            result |= p1 == q2 && p4 == q4;

            result |= p1 == q3 && p2 == q1;
            result |= p1 == q3 && p2 == q2;
            result |= p1 == q3 && p2 == q4;
            result |= p1 == q3 && p3 == q1;
            result |= p1 == q3 && p3 == q2;
            result |= p1 == q3 && p3 == q4;
            result |= p1 == q3 && p4 == q1;
            result |= p1 == q3 && p4 == q2;
            result |= p1 == q3 && p4 == q4;

            result |= p1 == q4 && p2 == q2;
            result |= p1 == q4 && p2 == q3;
            result |= p1 == q4 && p2 == q4;
            result |= p1 == q4 && p3 == q2;
            result |= p1 == q4 && p3 == q3;
            result |= p1 == q4 && p3 == q4;
            result |= p1 == q4 && p4 == q2;
            result |= p1 == q4 && p4 == q3;
            result |= p1 == q4 && p4 == q4;
            //2
            result |= p2 == q1 && p1 == q2;
            result |= p2 == q1 && p1 == q3;
            result |= p2 == q1 && p1 == q4;
            result |= p2 == q1 && p3 == q2;
            result |= p2 == q1 && p3 == q3;
            result |= p2 == q1 && p3 == q4;
            result |= p2 == q1 && p4 == q2;
            result |= p2 == q1 && p4 == q3;
            result |= p2 == q1 && p4 == q4;

            result |= p2 == q2 && p1 == q1;
            result |= p2 == q2 && p1 == q3;
            result |= p2 == q2 && p1 == q4;
            result |= p2 == q2 && p3 == q1;
            result |= p2 == q2 && p3 == q3;
            result |= p2 == q2 && p3 == q4;
            result |= p2 == q2 && p4 == q1;
            result |= p2 == q2 && p4 == q3;
            result |= p2 == q2 && p4 == q4;

            result |= p2 == q3 && p1 == q1;
            result |= p2 == q3 && p1 == q2;
            result |= p2 == q3 && p1 == q4;
            result |= p2 == q3 && p3 == q1;
            result |= p2 == q3 && p3 == q2;
            result |= p2 == q3 && p3 == q4;
            result |= p2 == q3 && p4 == q1;
            result |= p2 == q3 && p4 == q2;
            result |= p2 == q3 && p4 == q4;

            result |= p2 == q4 && p1 == q2;
            result |= p2 == q4 && p1 == q3;
            result |= p2 == q4 && p1 == q4;
            result |= p2 == q4 && p3 == q2;
            result |= p2 == q4 && p3 == q3;
            result |= p2 == q4 && p3 == q4;
            result |= p2 == q4 && p4 == q2;
            result |= p2 == q4 && p4 == q3;
            result |= p2 == q4 && p4 == q4;
            //3
            result |= p3 == q1 && p1 == q2;
            result |= p3 == q1 && p1 == q3;
            result |= p3 == q1 && p1 == q4;
            result |= p3 == q1 && p2 == q2;
            result |= p3 == q1 && p2 == q3;
            result |= p3 == q1 && p2 == q4;
            result |= p3 == q1 && p4 == q2;
            result |= p3 == q1 && p4 == q3;
            result |= p3 == q1 && p4 == q4;

            result |= p3 == q2 && p1 == q1;
            result |= p3 == q2 && p1 == q3;
            result |= p3 == q2 && p1 == q4;
            result |= p3 == q2 && p2 == q1;
            result |= p3 == q2 && p2 == q3;
            result |= p3 == q2 && p2 == q4;
            result |= p3 == q2 && p4 == q1;
            result |= p3 == q2 && p4 == q3;
            result |= p3 == q2 && p4 == q4;

            result |= p3 == q3 && p1 == q1;
            result |= p3 == q3 && p1 == q2;
            result |= p3 == q3 && p1 == q4;
            result |= p3 == q3 && p2 == q1;
            result |= p3 == q3 && p2 == q2;
            result |= p3 == q3 && p2 == q4;
            result |= p3 == q3 && p4 == q1;
            result |= p3 == q3 && p4 == q2;
            result |= p3 == q3 && p4 == q4;

            result |= p3 == q4 && p1 == q2;
            result |= p3 == q4 && p1 == q3;
            result |= p3 == q4 && p1 == q4;
            result |= p3 == q4 && p2 == q2;
            result |= p3 == q4 && p2 == q3;
            result |= p3 == q4 && p2 == q4;
            result |= p3 == q4 && p4 == q2;
            result |= p3 == q4 && p4 == q3;
            result |= p3 == q4 && p4 == q4;
            //4
            result |= p4 == q1 && p1 == q2;
            result |= p4 == q1 && p1 == q3;
            result |= p4 == q1 && p1 == q4;
            result |= p4 == q1 && p2 == q2;
            result |= p4 == q1 && p2 == q3;
            result |= p4 == q1 && p2 == q4;
            result |= p4 == q1 && p3 == q2;
            result |= p4 == q1 && p3 == q3;
            result |= p4 == q1 && p3 == q4;

            result |= p4 == q2 && p1 == q1;
            result |= p4 == q2 && p1 == q3;
            result |= p4 == q2 && p1 == q4;
            result |= p4 == q2 && p2 == q1;
            result |= p4 == q2 && p2 == q3;
            result |= p4 == q2 && p2 == q4;
            result |= p4 == q2 && p3 == q1;
            result |= p4 == q2 && p3 == q3;
            result |= p4 == q2 && p3 == q4;

            result |= p4 == q3 && p1 == q1;
            result |= p4 == q3 && p1 == q2;
            result |= p4 == q3 && p1 == q4;
            result |= p4 == q3 && p2 == q1;
            result |= p4 == q3 && p2 == q2;
            result |= p4 == q3 && p2 == q4;
            result |= p4 == q3 && p3 == q1;
            result |= p4 == q3 && p3 == q2;
            result |= p4 == q3 && p3 == q4;

            result |= p4 == q4 && p1 == q2;
            result |= p4 == q4 && p1 == q3;
            result |= p4 == q4 && p1 == q4;
            result |= p4 == q4 && p2 == q2;
            result |= p4 == q4 && p2 == q3;
            result |= p4 == q4 && p2 == q4;
            result |= p4 == q4 && p3 == q2;
            result |= p4 == q4 && p3 == q3;
            result |= p4 == q4 && p3 == q4;
            return result;
        }
        public void RemoveMatches(int[] round)
        {
            List<int[]> newMatches = new List<int[]>();
            List<int[]> matchesInRound = new List<int[]>();
            for (int i = 0; i + 3 < round.Length; i += 4)
            {
                int p1 = round[i];
                int p2 = round[i + 1];
                int p3 = round[i + 2];
                int p4 = round[i + 3];
                int[] newMatch = new int[] { p1, p2, p3, p4 };
                matchesInRound.Add(newMatch);
              
                //WriteMatch(sw, new int[] { p1, p2, p3, p4 });
            }
            StreamWriter sw = new StreamWriter("Matches.txt", true);
            sw.WriteLine(Matches.Count);
            foreach (var match in Matches)
            {
                bool result = false;
                int q1 = match[0];
                int q2 = match[1];
                int q3 = match[2];
                int q4 = match[3];
                foreach (var newMatch in matchesInRound)
                {
                    int p1 = newMatch[0];
                    int p2 = newMatch[1];
                    int p3 = newMatch[2];
                    int p4 = newMatch[3];
                    if (Model.RandomType == RandomType.UniqueCoPlayers)
                    {
                        result |= CheckCoPlayer(p1, p2, p3, p4, q1, q2, q3, q4);
                    }
                    else if (Model.RandomType == RandomType.UniqueOpo)
                    {
                        result |= CheckOpoPlayer(p1, p2, p3, p4, q1, q2, q3, q4);
                    }
                    else if (Model.RandomType == RandomType.UniqueOpoWoman)
                    {
                        result |= CheckOpoWomanPlayer(p1, p2, p3, p4, q1, q2, q3, q4);
                    }
                    else if (!Model.OneRandomRound)
                    {
                        result |= p1 == q1 && p2 == q2;
                        result |= p1 == q2 && p2 == q1;
                        result |= p1 == q3 && p2 == q4;
                        result |= p1 == q4 && p2 == q3;

                        result |= p3 == q3 && p4 == q4;
                        result |= p3 == q4 && p4 == q3;
                        result |= p3 == q1 && p4 == q2;
                        result |= p3 == q2 && p4 == q1;
                    }
                    if (result)
                    {
                        break;
                    }
                }

                if (!result)
                {
                    AddMatches(newMatches, match);
                    WriteMatch(sw, match);
                }
                else
                {
                    
                    //remMathces.Add(match);
                }
            }
            Matches = newMatches;
            sw.Flush();
            sw.Close();
        }
        private void AddMatches(List<int[]> matches, int[] match)
        {
            matches.Add(match);
        }
        private bool CheckMatch(int[] match, List<int> usedPlayers)
        {
            bool result = usedPlayers.Contains(match[0]) &&
                usedPlayers.Contains(match[1]) &&
                usedPlayers.Contains(match[2]) &&
                usedPlayers.Contains(match[3]);
            if (Model.RandomType == RandomType.UniqueOpoWoman)
            {
                int chk = match[0] + match[1];
                int chk2 = match[2] + match[3];
                result &= chk % 2 == 1 && chk2 % 2 == 1;
            }
            if (!Model.OneRandomRound)
            {
                bool opo = false;
                int player1 = match[0];
                int player2 = match[1];
                int player3 = match[2];
                int player4 = match[3];
                opo |= CheckOpo(player1, player3);
                opo |= CheckOpo(player1, player4);
                opo |= CheckOpo(player2, player3);
                opo |= CheckOpo(player2, player4);
                opo |= CheckOpo(player3, player1);
                opo |= CheckOpo(player3, player2);
                opo |= CheckOpo(player4, player1);
                opo |= CheckOpo(player4, player2);

                result &= !opo;
            }

            return result;
        }
        private List<int> InitPlayerList()
        {
            var result = new List<int>();
            for (int i = 0; i < Model.Players.Count; i++)
            {
                result.Add(i);
            }
            return result;
        }
        public List<int> InitPlayerByRank()
        {
            var players = Model.Players.OrderBy(x => x.No);
            var result = new List<int>();
            foreach(var player in players)
            {
                result.Add(player.Number-1);
            }
            return result;
        }

        public bool CheckOpo(int player1, int player2)
        {
            if (Model.RoundsWithAllPlayers.Count == 0)
            {
                return false;
            }
            return opo[player1, player2] > (Model.RoundsWithAllPlayers.Count) / OcIndex;
        }
        private Queue<int[]> GetNewQueue(Queue<int[]> queue, int[] usedPlayers)
        {
            var matches = queue.ToList().OrderBy(match => usedPlayers[match[0]] + usedPlayers[match[1]] + usedPlayers[match[2]] + usedPlayers[match[3]]);

            queue = new Queue<int[]>();
            foreach (var match in matches)
            {
                queue.Enqueue(match);
            }
            return queue;
        }
        private Queue<int[]> GetRandomQueue(Queue<int[]> queue)
        {
            var matches = queue.ToList();
            Random random = new Random();
            queue = new Queue<int[]>();
            while (matches.Count > 0)
            {
                int index = random.Next(matches.Count);
                var match = matches[index];
                queue.Enqueue(match);
                matches.RemoveAt(index);
            }
            return queue;
        }
        private Queue<int[]> InitQueue(out int[] specialRound)
        {
            Queue<int[]> queue = new Queue<int[]>();
            specialRound = null;
            foreach (var round in Model.RoundsWithAllPlayers)
            {
                for (int i = 0; i + 3 < round.Length; i += 4)
                {
                    int[] match = new int[] { round[i], round[i + 1], round[i + 2], round[i + 3] };
                    int p1 = match[0];
                    int p2 = match[1];
                    int p3 = match[2];
                    int p4 = match[3];
                    if (p1 == p3 && p2 == p4)
                    {
                        specialRound = round;
                        break;
                    }
                    else
                    {
                        queue.Enqueue(match);
                    }
                }
            }
            return queue;
        }
        private int[] Dequeue(ref Queue<int[]> queue )
        {
            var list = queue.ToList();
            int maxSitCount =0;
            int[] maxMatch = null;
            foreach (int[] match in list)
            {
                int sitCount = 0;
                for(int i = 0; i < 4; i++)
                {
                    sitCount += Model.Sits[match[i]];
                }
                if (sitCount > maxSitCount)
                {
                    maxMatch = match;
                    //break;
                    maxSitCount = sitCount;                    
                    
                }
            }

            if (maxMatch == null)
            {
                maxMatch = list[0];
            }
            list.Remove(maxMatch);
            queue = new Queue<int[]>(list);

            return maxMatch;
        }
        private void GenerateRoundsForCourts()
        {
            Model.RoundsForCourt = new Rounds();
            Model.RoundsForCourt.Model = Model;
            var newRound = NewRound();
            if (!Model.GenerateCourts)
            {
                foreach (var round in Model.RoundsWithAllPlayers)
                {
                    newRound = NewRound();
                    AddNewRound(round, false);
                }
                return;
            }
            if (Model.GenerateCourts && Model.Game == GameType.Teams && Model.Count /4  == Model.CourtCount)
            {
                foreach (var round in Model.RoundsWithAllPlayers)
                {
                    List<int> usedPlayer = new List<int>();
                    newRound = NewRound();
                    int j = 0;
                    for (int i = 0; i < round.Length && i < Model.CourtCount * 4; i++)
                    {
                        newRound[i] = round[i];
                        usedPlayer.Add(newRound[i]);
                        j++;
                    }
                    AddNewRound(newRound, false);
                }
                return;
            }

            int[] specialRound = null;
            var queue = InitQueue(out specialRound);

            int[] usedPlayers = new int[Model.Count];
            //queue = GetNewQueue(queue,usedPlayers);
            queue = GetRandomQueue(queue);
            newRound = NewRound();

            List<int> usedPlayersInRound = new List<int>(Model.Players.Count);
            int index = 0;
            int maxAttempt = 0;
            int maxReset = 0;
            int maxNew = 0;
            bool lastRound = false;
            int[] match;
            Model.Sits = new int[Model.Players.Count];
            int maxAttemptCount =queue.Count + 10;
            long cycle = 0;
            int minCount = queue.Count;
            while (queue.Count > 0 && cycle <100000000)
            {
                cycle++;
                if (minCount > queue.Count)
                {
                    minCount=queue.Count;
                }
                if (maxAttempt > queue.Count+10)
                {
                    PushRoundBack(newRound, index, usedPlayers, usedPlayersInRound, queue);
                    //Model.Sits = new int[Model.Players.Count];
                    queue = GetRandomQueue(queue);
                    newRound = NewRound();
                    index = 0;
                    maxAttempt = 0;
                    maxReset++;
                }
                if (maxReset > 100)
                {
                    //GenerateRounds();
                    Model.RoundsForCourt.Clear();
                    usedPlayersInRound = new List<int>(Model.Players.Count);
                    Model.Sits = new int[Model.Players.Count];
                    newRound = NewRound();
                    index = 0;
                    usedPlayers = new int[Model.Count];
                    maxAttempt = 0;
                    maxReset = 0;
                    queue = InitQueue(out specialRound);
                    queue = GetRandomQueue(queue);
                    maxNew++;
                }
                lastRound = queue.Count < Model.CourtCount;
                //match = Dequeue(ref queue);
                match = queue.Dequeue();
                
                if (CheckCourtMatch(match, usedPlayers, usedPlayersInRound, lastRound))
                {
                    //queue = GetNewQueue(queue,usedPlayers);                   
                   
                    if (index < Model.CourtCount * 4 && index + 3 < Model.Count)
                    {
                        newRound[index++] = match[0];
                        newRound[index++] = match[1];
                        newRound[index++] = match[2];
                        newRound[index++] = match[3];
                    }
                    if (index >= Model.CourtCount * 4 || index + 3 >= Model.Count)
                    {
                        if (queue.Count > Model.CourtCount && !CheckSits(newRound))
                        {
                            for (int i = 0; i < Model.Count && newRound[i] != -1; i += 4)
                            {
                                match = new int[4];
                                match[0] = newRound[i];
                                match[1] = newRound[i + 1];
                                match[2] = newRound[i + 2];
                                match[3] = newRound[i + 3];
                                queue.Enqueue(match);
                                PushMatchBack(match[0],match[1],match[2],match[3],  usedPlayers, usedPlayersInRound);
                                maxAttempt++;
                            }
                            //queue = GetRandomQueue(queue);
                        }
                        else
                        {
                            AddNewRound(newRound, true);
                            maxAttempt = 0;
                        }
                        newRound = NewRound();
                        index = 0;
                        usedPlayersInRound = new List<int>();                        
                        
                    }
                    else if (queue.Count == 0)
                    {
                        AddNewRound(newRound, true);
                        maxAttempt = 0;
                    }
                    
                    //queue = GetRandomQueue(queue);

                }
                else
                {
                    queue.Enqueue(match);
                    //queue = GetRandomQueue(queue);
                    maxAttempt++;
                }
            }
            index = 0;
            /*bool emptyQueue = true;
            while (queue.Count > 0)
            {
                
                emptyQueue = false;
                newRound = NewRound();
                match = queue.Dequeue();
                newRound[index++] = match[0];
                newRound[index++] = match[1];
                newRound[index++] = match[2];
                newRound[index++] = match[3];                
            }
            if (!emptyQueue)
            {
                Model.RoundsForCourt.Add(newRound);
            }*/

            if (specialRound != null)
            {
                newRound = NewRound();
                for (int i = 0; i < specialRound.Length; i++)
                {
                    newRound[i] = specialRound[i];
                }
                AddNewRound(newRound, true);
            }
        }
        private bool CheckSits(int[] newRound)
        {
            int maxSits = ((Model.Count / 4) / Model.CourtCount) + 1;
            var list = newRound.ToList();
            for (int i = 0; i < newRound.Length; i++)
            {
                if (!list.Contains(i))
                {
                    if (Model.Sits[i] == maxSits)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void AddNewRound(int[] newRound, bool sit)
        {
            Model.RoundsForCourt.AddRound(newRound);
            if (sit)
            {
                var list = newRound.ToList();
                for (int i = 0; i < newRound.Length; i++)
                {
                    if (!list.Contains(i))
                    {
                        Model.Sits[i]++;
                    }
                    else
                    {
                        Model.Sits[i] = 0;
                    }
                }
            }
        }
        private int[] NewRound()
        {
            int[] round = new int[Model.Count];
            for (int i = 0; i < round.Length; i++)
            {
                round[i] = -1;
            }
            return round;
        }
        private void PushRoundBack(int[] newRound, int index, int[] usedPlayers, List<int> usedPlayersInRound, Queue<int[]> queue)
        {
            for (int i = 0; i < index; i += 4)
            {
                int p1 = newRound[i];
                int p2 = newRound[i + 1];
                int p3 = newRound[i + 2];
                int p4 = newRound[i + 3];
                PushMatchBack(p1, p2, p3, p4, usedPlayers, usedPlayersInRound);
                queue.Enqueue(new int[] { p1, p2, p3, p4 });
            }
        }
        private void PushMatchBack(int p1,int p2, int p3, int p4,int[] usedPlayers, List<int>usedPlayersInRound)
        {
            usedPlayersInRound.Remove(p1);
            usedPlayersInRound.Remove(p2);
            usedPlayersInRound.Remove(p3);
            usedPlayersInRound.Remove(p4);
            usedPlayers[p1]--;
            usedPlayers[p2]--;
            usedPlayers[p3]--;
            usedPlayers[p4]--;
           
        }        
        private bool CheckCourtMatch(int[] match, int[] usedPlayers, List<int> usedPlayersInRound, bool lastRound)
        {
            int max = usedPlayers.Max();
            int dif = max - usedPlayers.Min();
            bool result = true;
            foreach (int i in match)
            {
                result &= !usedPlayersInRound.Contains(i);
            }
            //result |= lastRound;
            int p1 = match[0];
            int p2 = match[1];
            int p3 = match[2];
            int p4 = match[3];
            int usedExp = (max + 3 - dif)*(Model.Count/Model.CourtCount);

            result &= usedPlayers[p1] < usedExp;
            result &= usedPlayers[p2] < usedExp;
            result &= usedPlayers[p3] < usedExp;
            result &= usedPlayers[p4] < usedExp;

            //result |= maxAttempt > 1000;
            if (result)
            {

                usedPlayersInRound.Add(p1);
                usedPlayersInRound.Add(p2);
                usedPlayersInRound.Add(p3);
                usedPlayersInRound.Add(p4);
                usedPlayers[p1]++;
                usedPlayers[p2]++;
                usedPlayers[p3]++;
                usedPlayers[p4]++;
            }
            else
            {

            }
            return result;
        }

    }
}
