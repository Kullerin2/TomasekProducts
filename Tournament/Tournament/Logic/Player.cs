using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tournament
{
    [Serializable]
    public class Player
    {
        public PlayerInfo Info { get; set; }
        public int Count;
        public Score Score;
        public double MiniSet { get; set; }
        public int[] Courts;
        public string Points
        {
            get
            {                
                return string.Format("{0} : {1}", Score.PointWin, Score.PointLoose);

            }
        }
        public string Sets
        {
            get
            {
                return string.Format("{0} : {1}", Score.SetWin, Score.SetLoose);
            }
        }
        public string Games
        {
            get
            {
                return string.Format("{0} : {1}", Score.GameWin, Score.GameLoose);
            }
        }

        public int No;
        public int Number;
        public void Init(int number, int count,int setCount)
        {
            Count = count;            
            Score = new Score();
            Score.Init(number-1);
            //Score.SetCount = setCount;
            this.Number = number;
            MiniSet = 0;            
        }

        public void PopulateRow(DataRow row, DataModel model)
        {
            bool swissGame = model.ScoreType == ScoreType.Games;
            int count = 0;
            if (model.RoundsForCourt != null)
            {
                count = model.RoundsForCourt.Count;
            }
            //if (showScore)
            {
                //  row[0] = Number + ". " + Name;
            }
            //else
            {
                row[0] = Info.LastName;
            }
            for (int i = 0; i < count; i++)
            {
                row[i + 1] = Score.ShowPoint(i, model.ShowScore, swissGame);
            }
            if (swissGame)
            {
                row[count + 1] = Games;
                row[count + 2] = Sets;
                row[count + 3] = Points;
                row[count + 4] = No;
                ShowMedal(row, model, count + 5);
            }
            else
            {
                row[count + 1] = Sets;
                row[count + 2] = Points;
                row[count + 3] = No;
                ShowMedal(row, model, count + 4);
            }

        }
        private void ShowMedal(DataRow row, DataModel model, int index)
        {
            if (No == 1)
            {
                row[index] = model.Helper.GoldImage;
            }
            else if (No == 2)
            {
                row[index] = model.Helper.SilverImage;
            }
            else if (No == 3)
            {
                row[index] = model.Helper.BronzeImage;
            }




        }
    public void InitCourts(int courtCount)
        {
            Courts = new int[courtCount];
        }


    }
}
