using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tournament
{
    // Converter for disabling ComboboxItem
    public class ComboboxDisableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is LosItem)
            {
                var item = value as LosItem;
                return item.Los != null && !item.Los.IsEnabled;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Los
    {
        public int Count;
        public List<int[]> Rounds;
        public GameType Type;
        public string Name;
        public bool IsEnabled { get; set; }
        public Los(int count, GameType type, string name, bool isEnabled)
        {
            Type = type;
            Count = count;
            Name = name;
            IsEnabled = isEnabled;
            //IsEnabled = true;
        }
        public Los()
        {

        }
    }
    public class Losy
    {
        public Losy()
        {
            losy = new List<Los>();
        }
        public List<Los> losy;
        public void InitRounds()
        {
            #region Single
            Los los = new Los(4, GameType.Single, "4 hráči,2 kola", true);
            List<int[]> Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            int[] r = new int[4] { 0, 1, 2, 3 };
            Rounds.Add(r);
            r = new int[4] { 0, 2, 1, 3 };
            Rounds.Add(r);
            r = new int[4] { 0, 3, 1, 2 };
            Rounds.Add(r);
            r = new int[4] { 0, 1, 2, 3 };
            Rounds.Add(r);
            r = new int[4] { 0, 2, 1, 3 };
            Rounds.Add(r);
            r = new int[4] { 0, 3, 1, 2 };
            Rounds.Add(r);

            los = new Los(5, GameType.Single, "5 hráčů,3 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[5] { 0, 1, 2, 3, 4 };
            Rounds.Add(r);
            r = new int[5] { 0, 2, 1, 4, 3 };
            Rounds.Add(r);
            r = new int[5] { 0, 3, 2, 4, 1 };
            Rounds.Add(r);
            r = new int[5] { 0, 4, 1, 3, 2 };
            Rounds.Add(r);
            r = new int[5] { 1, 2, 3, 4, 0 };
            Rounds.Add(r);
            r = new int[5] { 0, 2, 1, 3, 4 };
            Rounds.Add(r);
            r = new int[5] { 0, 3, 2, 4, 1 };
            Rounds.Add(r);
            r = new int[5] { 1, 4, 2, 3, 0 };
            Rounds.Add(r);
            r = new int[5] { 0, 4, 1, 2, 3 };
            Rounds.Add(r);
            r = new int[5] { 0, 1, 3, 4, 2 };
            Rounds.Add(r);
            r = new int[5] { 0, 4, 2, 3, 1 };
            Rounds.Add(r);
            r = new int[5] { 0, 2, 1, 3, 4 };
            Rounds.Add(r);
            r = new int[5] { 0, 3, 1, 4, 2 };
            Rounds.Add(r);
            r = new int[5] { 0, 1, 2, 4, 3 };
            Rounds.Add(r);
            r = new int[5] { 1, 2, 3, 4, 0 };
            Rounds.Add(r);

            los = new Los(6, GameType.Single, "6 hráčů,2 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[6] { 0, 1, 2, 3, 4, 5 };
            Rounds.Add(r);
            r = new int[6] { 0, 2, 4, 3, 5, 1 };
            Rounds.Add(r);
            r = new int[6] { 0, 4, 5, 3, 2, 1 };
            Rounds.Add(r);
            r = new int[6] { 0, 5, 2, 4, 3, 1 };
            Rounds.Add(r);
            r = new int[6] { 0, 3, 2, 5, 4, 1 };
            Rounds.Add(r);
            r = new int[6] { 0, 1, 2, 3, 4, 5 };
            Rounds.Add(r);
            r = new int[6] { 0, 2, 4, 3, 5, 1 };
            Rounds.Add(r);
            r = new int[6] { 0, 4, 5, 3, 2, 1 };
            Rounds.Add(r);
            r = new int[6] { 0, 5, 2, 4, 3, 1 };
            Rounds.Add(r);
            r = new int[6] { 0, 3, 2, 5, 4, 1 };
            Rounds.Add(r);

            los = new Los(7, GameType.Single, "7 hráčů,2 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[7] { 1, 6, 2, 5, 3, 4, 0 };
            Rounds.Add(r);
            r = new int[7] { 0, 1, 2, 6, 5, 3, 4 };
            Rounds.Add(r);
            r = new int[7] { 0, 2, 3, 6, 4, 5, 1 };
            Rounds.Add(r);
            r = new int[7] { 1, 2, 0, 3, 4, 6, 5 };
            Rounds.Add(r);
            r = new int[7] { 1, 3, 0, 4, 5, 6, 2 };
            Rounds.Add(r);
            r = new int[7] { 2, 3, 1, 4, 0, 5, 6 };
            Rounds.Add(r);
            r = new int[7] { 2, 4, 1, 5, 0, 6, 3 };
            Rounds.Add(r);

            r = new int[7] { 1, 6, 2, 5, 3, 4, 0 };
            Rounds.Add(r);
            r = new int[7] { 0, 1, 2, 6, 5, 3, 4 };
            Rounds.Add(r);
            r = new int[7] { 0, 2, 3, 6, 4, 5, 1 };
            Rounds.Add(r);
            r = new int[7] { 1, 2, 0, 3, 4, 6, 5 };
            Rounds.Add(r);
            r = new int[7] { 1, 3, 0, 4, 5, 6, 2 };
            Rounds.Add(r);
            r = new int[7] { 2, 3, 1, 4, 0, 5, 6 };
            Rounds.Add(r);
            r = new int[7] { 2, 4, 1, 5, 0, 6, 3 };
            Rounds.Add(r);

            los = new Los(8, GameType.Single, "8 hráčů,2 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);


            r = new int[8] { 0, 7, 1, 6, 2, 5, 3, 4 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 6, 2, 7, 4, 5, 3 };
            Rounds.Add(r);
            r = new int[8] { 1, 7, 2, 0, 3, 6, 4, 5 };
            Rounds.Add(r);
            r = new int[8] { 1, 2, 0, 3, 7, 5, 6, 4 };
            Rounds.Add(r);
            r = new int[8] { 3, 1, 2, 7, 4, 0, 5, 6 };
            Rounds.Add(r);
            r = new int[8] { 2, 3, 1, 4, 7, 6, 0, 5 };
            Rounds.Add(r);
            r = new int[8] { 4, 2, 3, 7, 5, 1, 6, 0 };
            Rounds.Add(r);
            r = new int[8] { 0, 7, 1, 6, 2, 5, 3, 4 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 6, 2, 7, 4, 5, 3 };
            Rounds.Add(r);
            r = new int[8] { 1, 7, 2, 0, 3, 6, 4, 5 };
            Rounds.Add(r);
            r = new int[8] { 1, 2, 0, 3, 7, 5, 6, 4 };
            Rounds.Add(r);
            r = new int[8] { 3, 1, 2, 7, 4, 0, 5, 6 };
            Rounds.Add(r);
            r = new int[8] { 2, 3, 1, 4, 7, 6, 0, 5 };
            Rounds.Add(r);
            r = new int[8] { 4, 2, 3, 7, 5, 1, 6, 0 };
            Rounds.Add(r);

            los = new Los(9, GameType.Single, "9 hráčů,1 kolo", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);

            r = new int[9] { 1, 8, 2, 7, 3, 6, 4, 5, 0 };
            Rounds.Add(r);
            r = new int[9] { 0, 1, 8, 2, 7, 3, 6, 4, 5 };
            Rounds.Add(r);
            r = new int[9] { 2, 0, 3, 8, 4, 7, 5, 6, 1 };
            Rounds.Add(r);
            r = new int[9] { 1, 2, 0, 3, 8, 4, 7, 5, 6 };
            Rounds.Add(r);
            r = new int[9] { 3, 1, 4, 0, 5, 8, 6, 7, 2 };
            Rounds.Add(r);
            r = new int[9] { 2, 3, 1, 4, 0, 5, 8, 6, 7 };
            Rounds.Add(r);
            r = new int[9] { 4, 2, 5, 1, 6, 0, 7, 8, 3 };
            Rounds.Add(r);
            r = new int[9] { 3, 4, 2, 5, 1, 6, 0, 7, 8 };
            Rounds.Add(r);
            r = new int[9] { 5, 3, 6, 2, 7, 1, 8, 0, 4 };
            Rounds.Add(r);


            los = new Los(10, GameType.Single, "10 hráčů,1 kolo", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[8] { 0, 9, 1, 8, 2, 7, 3, 6 };
            Rounds.Add(r);
            r = new int[8] { 4, 5, 0, 1, 8, 2, 7, 3 };
            Rounds.Add(r);
            r = new int[8] { 6, 4, 9, 5, 2, 0, 3, 8 };
            Rounds.Add(r);
            r = new int[8] { 4, 7, 5, 6, 1, 9, 0, 3 };
            Rounds.Add(r);
            r = new int[8] { 8, 4, 1, 2, 7, 5, 9, 6 };
            Rounds.Add(r);
            r = new int[8] { 3, 1, 4, 0, 2, 9, 5, 8 };
            Rounds.Add(r);
            r = new int[8] { 8, 6, 9, 7, 4, 2, 5, 1 };
            Rounds.Add(r);
            r = new int[8] { 6, 7, 2, 3, 1, 4, 0, 5 };
            Rounds.Add(r);
            r = new int[8] { 2, 5, 1, 6, 0, 7, 9, 8 };
            Rounds.Add(r);
            r = new int[8] { 5, 3, 6, 2, 7, 1, 4, 9 };
            Rounds.Add(r);
            r = new int[8] { 6, 0, 9, 3, 7, 8, -1, -1 };
            Rounds.Add(r);
            r = new int[8] { 3, 4, 8, 0, -1, -1, -1, -1 };
            Rounds.Add(r);

            #endregion

            #region Defined
            los = new Los(4, GameType.Defined, "4 hráči,3 kola", true);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[4] { 0, 1, 2, 3 };
            Rounds.Add(r);
            r = new int[4] { 0, 2, 1, 3 };
            Rounds.Add(r);
            r = new int[4] { 0, 3, 1, 2 };
            Rounds.Add(r);
            r = new int[4] { 0, 1, 2, 3 };
            Rounds.Add(r);
            r = new int[4] { 0, 2, 1, 3 };
            Rounds.Add(r);
            r = new int[4] { 0, 3, 1, 2 };
            Rounds.Add(r);
            r = new int[4] { 0, 1, 2, 3 };
            Rounds.Add(r);
            r = new int[4] { 0, 2, 1, 3 };
            Rounds.Add(r);
            r = new int[4] { 0, 3, 1, 2 };
            Rounds.Add(r);


            los = new Los(5, GameType.Defined, "5 hráčů,3 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[5] { 0, 1, 2, 3, 4 };
            Rounds.Add(r);
            r = new int[5] { 0, 2, 1, 4, 3 };
            Rounds.Add(r);
            r = new int[5] { 0, 3, 2, 4, 1 };
            Rounds.Add(r);
            r = new int[5] { 0, 4, 1, 3, 2 };
            Rounds.Add(r);
            r = new int[5] { 1, 2, 3, 4, 0 };
            Rounds.Add(r);
            r = new int[5] { 0, 2, 1, 3, 4 };
            Rounds.Add(r);
            r = new int[5] { 0, 3, 2, 4, 1 };
            Rounds.Add(r);
            r = new int[5] { 1, 4, 2, 3, 0 };
            Rounds.Add(r);
            r = new int[5] { 0, 4, 1, 2, 3 };
            Rounds.Add(r);
            r = new int[5] { 0, 1, 3, 4, 2 };
            Rounds.Add(r);
            r = new int[5] { 0, 4, 2, 3, 1 };
            Rounds.Add(r);
            r = new int[5] { 0, 2, 1, 3, 4 };
            Rounds.Add(r);
            r = new int[5] { 0, 3, 1, 4, 2 };
            Rounds.Add(r);
            r = new int[5] { 0, 1, 2, 4, 3 };
            Rounds.Add(r);
            r = new int[5] { 1, 2, 3, 4, 0 };
            Rounds.Add(r);

            /*los = new Los(5, GameType.Defined, "5 hráčů,2 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);

            r = new int[5] { 0, 1, 2, 3, 4 };
            Rounds.Add(r);
            r = new int[5] { 0, 2, 1, 4, 3 };
            Rounds.Add(r);
            r = new int[5] { 0, 3, 2, 4, 1 };
            Rounds.Add(r);
            r = new int[5] { 0, 4, 1, 3, 2 };
            Rounds.Add(r);
            r = new int[5] { 1, 4, 2, 3, 0 };
            Rounds.Add(r);
            r = new int[5] { 0, 3, 1, 2, 4 };
            Rounds.Add(r);
            r = new int[5] { 0, 4, 1, 2, 3 };
            Rounds.Add(r);
            r = new int[5] { 0, 1, 3, 4, 2 };
            Rounds.Add(r);
            r = new int[5] { 0, 2, 3, 4, 1 };
            Rounds.Add(r);
            r = new int[5] { 1, 3, 2, 4, 0 };
            Rounds.Add(r);*/

            los = new Los(5, GameType.Defined, "5 hráčů,2 kola (New)", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);

            r = new int[5] { 0, 1, 2, 3, 4 };
            Rounds.Add(r);
            r = new int[5] { 0, 2, 1, 4, 3 };
            Rounds.Add(r);
            r = new int[5] { 0, 3, 2, 4, 1 };
            Rounds.Add(r);
            r = new int[5] { 0, 4, 1, 3, 2 };
            Rounds.Add(r);
            r = new int[5] { 1, 4, 2, 3, 0 };
            Rounds.Add(r);
            r = new int[5] { 0, 3, 1, 2, 4 };
            Rounds.Add(r);
            r = new int[5] { 0, 4, 1, 2, 3 };
            Rounds.Add(r);
            r = new int[5] { 0, 1, 3, 4, 2 };
            Rounds.Add(r);
            r = new int[5] { 0, 2, 3, 4, 1 };
            Rounds.Add(r);
            r = new int[5] { 1, 3, 2, 4, 0 };
            Rounds.Add(r);

            los = new Los(6, GameType.Defined, "6 hráčů,2 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);

            r = new int[6] { 0, 1, 2, 3, 4, 5 };
            Rounds.Add(r);
            r = new int[6] { 0, 4, 1, 5, 2, 3 };
            Rounds.Add(r);
            r = new int[6] { 2, 4, 3, 5, 0, 1 };
            Rounds.Add(r);
            r = new int[6] { 0, 5, 1, 3, 2, 4 };
            Rounds.Add(r);
            r = new int[6] { 1, 2, 3, 4, 0, 5 };
            Rounds.Add(r);
            r = new int[6] { 0, 2, 4, 5, 1, 3 };
            Rounds.Add(r);
            r = new int[6] { 0, 3, 1, 4, 2, 5 };
            Rounds.Add(r);
            r = new int[6] { 2, 5, 0, 1, 3, 4 };
            Rounds.Add(r);
            r = new int[6] { 2, 3, 4, 5, 0, 1 };
            Rounds.Add(r);
            r = new int[6] { 0, 3, 1, 5, 2, 4 };
            Rounds.Add(r);
            r = new int[6] { 0, 2, 3, 4, 1, 5 };
            Rounds.Add(r);
            r = new int[6] { 1, 4, 2, 5, 0, 3 };
            Rounds.Add(r);
            r = new int[6] { 0, 4, 1, 3, 2, 5 };
            Rounds.Add(r);
            r = new int[6] { 1, 2, 3, 5, 0, 4 };
            Rounds.Add(r);
            r = new int[6] { 2, 4, 0, 5, 1, 3 };
            Rounds.Add(r);

            los = new Los(8, GameType.Defined, "8 hráčů,2 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
            Rounds.Add(r);
            r = new int[8] { 0, 2, 4, 6, 1, 3, 5, 7 };
            Rounds.Add(r);
            r = new int[8] { 0, 3, 4, 7, 1, 2, 5, 6 };
            Rounds.Add(r);
            r = new int[8] { 0, 4, 1, 5, 2, 6, 3, 7 };
            Rounds.Add(r);
            r = new int[8] { 0, 5, 2, 7, 1, 4, 3, 6 };
            Rounds.Add(r);
            r = new int[8] { 0, 6, 1, 7, 2, 4, 3, 5 };
            Rounds.Add(r);
            r = new int[8] { 0, 7, 1, 6, 2, 5, 3, 4 };
            Rounds.Add(r);

            r = new int[8] { 2, 3, 4, 5, 6, 7, 0, 1 };
            Rounds.Add(r);
            r = new int[8] { 2, 4, 6, 0, 3, 5, 7, 1 };
            Rounds.Add(r);
            r = new int[8] { 2, 5, 6, 1, 3, 4, 7, 0 };
            Rounds.Add(r);
            r = new int[8] { 2, 6, 3, 7, 4, 0, 5, 1 };
            Rounds.Add(r);
            r = new int[8] { 2, 7, 4, 1, 3, 6, 5, 0 };
            Rounds.Add(r);
            r = new int[8] { 2, 0, 3, 1, 4, 6, 5, 7 };
            Rounds.Add(r);
            r = new int[8] { 2, 1, 3, 0, 4, 7, 5, 6 };
            Rounds.Add(r);

            los = new Los(9, GameType.Defined, "9 hráčů,1 kolo", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            Rounds.Add(r);
            r = new int[9] { 0, 2, 4, 6, 1, 3, 5, 8, 7 };
            Rounds.Add(r);
            r = new int[9] { 0, 3, 4, 7, 1, 2, 6, 8, 5 };
            Rounds.Add(r);
            r = new int[9] { 0, 4, 1, 5, 2, 6, 7, 8, 3 };
            Rounds.Add(r);
            r = new int[9] { 0, 5, 1, 6, 2, 7, 3, 8, 4 };
            Rounds.Add(r);
            r = new int[9] { 0, 6, 3, 5, 1, 7, 4, 8, 2 };
            Rounds.Add(r);
            r = new int[9] { 0, 7, 1, 8, 2, 5, 3, 4, 6 };
            Rounds.Add(r);
            r = new int[9] { 0, 8, 2, 4, 3, 6, 5, 7, 1 };
            Rounds.Add(r);
            r = new int[9] { 1, 4, 3, 7, 2, 8, 5, 6, 0 };
            Rounds.Add(r);


            los = new Los(10, GameType.Defined, "10 hráčů,1 kolo", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Rounds.Add(r);
            r = new int[10] { 6, 2, 5, 7, 8, 4, 9, 3, 0, 1 };
            Rounds.Add(r);
            r = new int[10] { 9, 7, 3, 4, 0, 8, 1, 5, 6, 2 };
            Rounds.Add(r);
            r = new int[10] { 1, 3, 5, 8, 6, 0, 2, 4, 9, 7 };
            Rounds.Add(r);
            r = new int[10] { 2, 5, 4, 0, 9, 6, 7, 8, 1, 3 };
            Rounds.Add(r);
            r = new int[10] { 7, 4, 8, 6, 1, 9, 3, 0, 2, 5 };
            Rounds.Add(r);
            r = new int[10] { 5, 0, 6, 1, 7, 2, 4, 9, 3, 8 };
            Rounds.Add(r);
            r = new int[10] { 3, 8, 0, 9, 2, 1, 5, 6, 7, 4 };
            Rounds.Add(r);
            r = new int[10] { 0, 7, 5, 9, 2, 8, 1, 4, 3, 6 };
            Rounds.Add(r);
            r = new int[10] { 4, 6, 9, 2, 3, 7, 8, 1, 5, 0 };
            Rounds.Add(r);
            r = new int[10] { 8, 9, 1, 7, 5, 3, 0, 2, 4, 6 };
            Rounds.Add(r);
            r = new int[10] { 3, 6, 1, 2, 0, 4, 5, 7, 8, 9 };
            Rounds.Add(r);




            los = new Los(12, GameType.Defined, "12 hráčů,1 kolo", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 0, 2, 4, 6, 1, 3, 8, 10, 5, 7, 9, 11 };
            Rounds.Add(r);
            r = new int[12] { 0, 3, 5, 9, 1, 10, 4, 7, 2, 6, 8, 11 };
            Rounds.Add(r);
            r = new int[12] { 0, 4, 1, 2, 3, 5, 6, 8, 7, 11, 9, 10 };
            Rounds.Add(r);
            r = new int[12] { 0, 5, 1, 4, 2, 8, 6, 9, 3, 11, 7, 10 };
            Rounds.Add(r);
            r = new int[12] { 0, 6, 3, 7, 1, 8, 2, 11, 4, 9, 5, 10 };
            Rounds.Add(r);
            r = new int[12] { 0, 7, 1, 5, 2, 9, 3, 8, 4, 10, 6, 11 };
            Rounds.Add(r);
            r = new int[12] { 0, 8, 3, 4, 1, 6, 2, 10, 5, 11, 7, 9 };
            Rounds.Add(r);
            r = new int[12] { 0, 9, 2, 7, 1, 11, 4, 8, 3, 10, 5, 6 };
            Rounds.Add(r);
            r = new int[12] { 0, 10, 1, 9, 2, 5, 3, 6, 4, 11, 7, 8 };
            Rounds.Add(r);
            r = new int[12] { 0, 11, 2, 4, 1, 7, 3, 9, 5, 8, 6, 10 };
            Rounds.Add(r);

            los = new Los(12, GameType.Defined, "12 hráčů,1 kolo, 1 dvojice", true);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);

            r = new int[12] { 0, 2, 1, 8, 3, 7, 4, 5, 6, 9, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 0, 4, 2, 5, 6, 7, 8, 9, 1, 3, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 2, 8, 3, 5, 0, 1, 7, 9, 4, 6, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 0, 7, 3, 6, 1, 9, 5, 8, 2, 4, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 1, 2, 4, 9, 3, 8, 5, 6, 0, 7, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 3, 9, 6, 8, 0, 5, 2, 7, 1, 4, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 2, 4, 7, 8, 0, 6, 1, 5, 3, 9, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 0, 9, 3, 4, 1, 7, 2, 6, 5, 8, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 0, 8, 1, 3, 4, 6, 5, 9, 2, 7, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 1, 4, 5, 7, 2, 3, 6, 9, 0, 8, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 1, 6, 4, 7, 0, 3, 2, 9, 5, 8, 10, 11 };
            Rounds.Add(r);


            los = new Los(12, GameType.Defined, "12 hráčů,2 kola, 6 mixů", true);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[12] { 1, 6, 2, 3, 0, 5, 4, 9, 7, 8, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 1, 4, 9, 10, 0, 3, 2, 7, 5, 6, 8, 11 };
            Rounds.Add(r);
            r = new int[12] { 2, 5, 6, 7, 0, 9, 3, 10, 1, 8, 4, 11 };
            Rounds.Add(r);
            r = new int[12] { 0, 7, 2, 9, 1, 10, 6, 11, 3, 8, 4, 5 };
            Rounds.Add(r);
            r = new int[12] { 0, 1, 7, 10, 2, 11, 6, 9, 3, 4, 5, 8 };
            Rounds.Add(r);
            r = new int[12] { 0, 11, 1, 2, 3, 6, 4, 7, 5, 10, 8, 9 };
            Rounds.Add(r);
            r = new int[12] { 1, 8, 4, 3, 2, 5, 6, 9, 7, 10, 0, 11 };
            Rounds.Add(r);
            r = new int[12] { 1, 6, 9, 0, 2, 3, 4, 7, 5, 8, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 4, 5, 8, 7, 2, 9, 3, 0, 1, 10, 6, 11 };
            Rounds.Add(r);
            r = new int[12] { 2, 7, 4, 9, 1, 0, 8, 11, 3, 10, 6, 5 };
            Rounds.Add(r);
            r = new int[12] { 2, 1, 7, 0, 4, 11, 8, 9, 3, 6, 5, 10 };
            Rounds.Add(r);
            r = new int[12] { 2, 11, 1, 4, 3, 8, 6, 7, 6, 0, 10, 9 };
            Rounds.Add(r);

            los = new Los(18, GameType.Defined, "18 hráčů,1 kolo, 9 mixů", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[18] { 8, 15, 11, 16, 1, 10, 4, 17, 0, 9, 13, 14, 5, 6, 7, 12, 2, 3 };
            Rounds.Add(r);
            r = new int[18] { 5, 12, 13, 16, 0, 3, 1, 8, 2, 9, 10, 17, 4, 15, 7, 14, 6, 11 };
            Rounds.Add(r);
            r = new int[18] { 0, 5, 6, 11, 2, 7, 9, 12, 3, 10, 14, 15, 4, 13, 8, 17, 1, 16 };
            Rounds.Add(r);
            r = new int[18] { 0, 13, 10, 15, 3, 4, 5, 16, 1, 14, 8, 11, 2, 17, 6, 7, 9, 12 };
            Rounds.Add(r);
            r = new int[18] { 3, 8, 7, 10, 1, 2, 5, 14, 6, 9, 15, 16, 4, 11, 12, 13, 0, 17 };
            Rounds.Add(r);
            r = new int[18] { 1, 12, 9, 10, 0, 15, 4, 5, 6, 17, 11, 14, 2, 13, 3, 16, 7, 8 };
            Rounds.Add(r);
            r = new int[18] { 0, 17, 9, 14, 1, 16, 11, 12, 2, 3, 7, 8, 5, 10, 6, 15, 4, 13 };
            Rounds.Add(r);
            r = new int[18] { 1, 6, 3, 14, 0, 7, 16, 17, 4, 9, 12, 15, 2, 5, 8, 13, 10, 11 };
            Rounds.Add(r);
            r = new int[18] { 4, 7, 10, 13, 3, 6, 9, 16, 0, 1, 5, 8, 2, 11, 12, 17, 14, 15 };
            Rounds.Add(r);
            r = new int[18] { 8, 9, 10, 11, 2, 15, 6, 13, 3, 12, 14, 17, 1, 4, 7, 16, 0, 5 };
            Rounds.Add(r);
            r = new int[18] {0, 11, 0, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1,-1, -1, -1, -1,-1 };
            Rounds.Add(r);

            los = new Los(20, GameType.Defined, "20 hráčů,1 kolo, 10 mixů", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[20] { 3, 6, 7, 8, 0, 9, 13, 18, 1, 12, 10, 17, 4, 19, 5, 14, 2, 15, 11, 16 };
            Rounds.Add(r);
            r = new int[20] { 8, 19, 9, 18, 5, 6, 10, 15, 1, 14, 3, 16, 0, 11, 7, 12, 2, 17, 4, 13 };
            Rounds.Add(r);
            r = new int[20] { 6, 7, 9, 16, 3, 8, 12, 17, 0, 1, 4, 5, 13, 14, 15, 18, 2, 11, 10, 19 };
            Rounds.Add(r);
            r = new int[20] { 8, 11, 13, 16, 0, 17, 6, 19, 1, 10, 7, 14, 2, 9, 5, 12, 3, 18, 4, 15 };
            Rounds.Add(r);
            r = new int[20] { 0, 13, 3, 10, 1, 2, 6, 15, 5, 16, 17, 18, 4, 7, 12, 19, 8, 9, 11, 14 };
            Rounds.Add(r);
            r = new int[20] { 2, 5, 3, 14, 4, 17, 9, 10, 12, 13, 16, 19, 1, 6, 11, 18, 0, 7, 8, 15 };
            Rounds.Add(r);
            r = new int[20] { 1, 16, 2, 19, 3, 12, 5, 18, 0, 15, 7, 10, 8, 13, 14, 17, 4, 9, 6, 11 };
            Rounds.Add(r);
            r = new int[20] { 0, 5, 18, 19, 2, 3, 10, 13, 7, 16, 9, 14, 1, 8, 4, 11, 6, 17, 12, 15 };
            Rounds.Add(r);
            r = new int[20] { 1, 18, 6, 13, 2, 7, 5, 8, 10, 11, 14, 15, 3, 4, 16, 17, 0, 19, 9, 12 };
            Rounds.Add(r);
            r = new int[20] { 0, 3, 6, 9, 7, 18, 14, 19, 5, 10, 8, 17, 1, 4, 15, 16, 2, 13, 11, 12 };
            Rounds.Add(r);

            los = new Los(22, GameType.Defined, "22 hráčů,1 kolo, 11 mixů", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[22] { 3, 20, 15, 16, 4, 21, 9, 18, 0, 5, 10, 11, 7, 12, 8, 13, 2, 19, 14, 17, 1, 6 };
            Rounds.Add(r);
            r = new int[22] { 1, 20, 5, 14, 6, 11, 9, 10, 3, 4, 12, 21, 0, 15, 13, 16, 2, 17, 7, 18, 8, 19 };
            Rounds.Add(r);
            r = new int[22] { 0, 11, 4, 15, 1, 12, 2, 21, 9, 16, 10, 17, 5, 6, 13, 18, 7, 8, 19, 20, 3, 14 };
            Rounds.Add(r);
            r = new int[22] { 4, 9, 8, 15, 1, 2, 10, 13, 11, 16, 12, 19, 0, 3, 6, 17, 7, 14, 18, 21, 5, 20 };
            Rounds.Add(r);
            r = new int[22] { 2, 11, 8, 21, 3, 18, 9, 20, 5, 16, 13, 14, 6, 15, 12, 17, 1, 4, 10, 19, 0, 7 };
            Rounds.Add(r);
            r = new int[22] { 1, 16, 8, 9, 0, 17, 20, 21, 5, 12, 6, 7, 10, 15, 14, 19, 2, 3, 4, 11, 13, 18 };
            Rounds.Add(r);
            r = new int[22] { 8, 11, 15, 20, 4, 17, 6, 19, 0, 21, 7, 10, 3, 14, 12, 13, 1, 18, 2, 5, 9, 16 };
            Rounds.Add(r);
            r = new int[22] { 2, 15, 3, 10, 5, 8, 14, 21, 9, 12, 16, 19, 7, 20, 11, 18, 1, 6, 4, 13, 0, 17 };
            Rounds.Add(r);
            r = new int[22] { 0, 19, 2, 9, 10, 21, 13, 20, 5, 18, 12, 15, 11, 14, 16, 17, 1, 8, 3, 6, 4, 7 };
            Rounds.Add(r);
            r = new int[22] { 5, 10, 6, 9, 8, 17, 14, 15, 2, 13, 3, 16, 0, 1, 18, 19, 4, 7, 11, 20, 12, 21 };
            Rounds.Add(r);
            r = new int[22] { 1, 10, 7, 16, 6, 21, 8, 19, 0, 13, 15, 18, 4, 5, 17, 20, 3, 12, 9, 14, 2, 11 };
            Rounds.Add(r);
            r = new int[22] { 1, 14, 17, 18, 4, 19, 5, 20, 3, 8, 11, 12, 2, 7, 6, 13, 0, 9, 16, 21, 10, 15 };
            Rounds.Add(r);
            r = new int[22] { 0, 7, 0, 7, 0, 7, 0, 7, 0, 7, 0, 7, 0, 7, 0, 7, 0, 7, 0, 7, 0, 7 };
            Rounds.Add(r);

            los = new Los(24, GameType.Defined, "24 hráčů,1 kolo, 12 mixů", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[24] { 3, 20, 10, 21, 1, 22, 16, 17, 5, 18, 12, 13, 0, 23, 2, 9, 4, 7, 6, 19, 8, 11, 14, 15 };
            Rounds.Add(r);
            r = new int[24] { 3, 14, 20, 23, 0, 21, 1, 12, 7, 18, 17, 22, 2, 5, 6, 15, 4, 9, 10, 11, 8, 13, 16, 19 };
            Rounds.Add(r);
            r = new int[24] { 1, 18, 9, 14, 2, 11, 7, 8, 0, 19, 10, 23, 3, 4, 5, 16, 6, 21, 13, 22, 12, 15, 17, 20 };
            Rounds.Add(r);
            r = new int[24] { 0, 17, 13, 20, 3, 6, 14, 19, 4, 15, 9, 12, 1, 2, 11, 22, 8, 21, 18, 23, 5, 10, 7, 16 };
            Rounds.Add(r);
            r = new int[24] { 1, 4, 13, 18, 0, 3, 15, 22, 6, 23, 10, 17, 11, 16, 19, 20, 5, 12, 8, 9, 2, 7, 14, 21 };
            Rounds.Add(r);
            r = new int[24] { 7, 22, 9, 20, 1, 14, 12, 19, 2, 13, 3, 10, 4, 21, 8, 15, 6, 11, 16, 23, 0, 5, 17, 18 };
            Rounds.Add(r);
            r = new int[24] { 10, 15, 19, 22, 0, 1, 9, 16, 4, 17, 7, 20, 5, 6, 11, 18, 2, 21, 8, 23, 3, 12, 13, 14 };
            Rounds.Add(r);
            r = new int[24] { 0, 9, 2, 17, 1, 10, 5, 14, 6, 7, 13, 16, 15, 18, 22, 23, 4, 19, 12, 21, 3, 8, 11, 20 };
            Rounds.Add(r);
            r = new int[24] { 8, 19, 12, 23, 4, 13, 5, 22, 0, 11, 1, 16, 3, 18, 6, 17, 2, 15, 9, 10, 7, 14, 20, 21 };
            Rounds.Add(r);
            r = new int[24] { 1, 6, 8, 17, 3, 22, 16, 21, 2, 19, 4, 5, 0, 7, 14, 23, 11, 12, 15, 20, 9, 18, 10, 13 };
            Rounds.Add(r);
            r = new int[24] { 1, 20, 10, 19, 2, 23, 7, 12, 0, 15, 18, 21, 9, 22, 14, 17, 3, 16, 5, 8, 4, 11, 6, 13 };
            Rounds.Add(r);
            r = new int[24] { 6, 9, 21, 22, 11, 14, 18, 19, 0, 13, 7, 10, 1, 8, 5, 20, 4, 23, 15, 16, 2, 3, 12, 17 };
            Rounds.Add(r);
            los = new Los(32, GameType.Defined, "32 hráčů, 7 kol", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[32] { 2, 22, 23, 24, 10, 16, 21, 30, 5, 9, 13, 31, 0, 26, 14, 28, 3, 11, 4, 20, 6, 17, 8, 15, 1, 7, 12, 18, 19, 27, 25, 29 };
            Rounds.Add(r);
            r = new int[32] { 4, 19, 9, 30, 5, 8, 23, 27, 11, 12, 16, 31, 1, 25, 24, 28, 10, 14, 13, 15, 2, 20, 6, 18, 7, 26, 21, 29, 0, 3, 17, 22 };
            Rounds.Add(r);
            r = new int[32] { 12, 26, 17, 30, 13, 19, 21, 22, 8, 24, 16, 20, 3, 15, 7, 23, 1, 2, 4, 5, 0, 10, 29, 31, 11, 14, 18, 25, 6, 9, 27, 28 };
            Rounds.Add(r);
            r = new int[32] { 6, 23, 10, 26, 1, 20, 15, 30, 4, 18, 17, 31, 7, 27, 14, 16, 3, 21, 12, 25, 0, 19, 5, 24, 9, 11, 22, 29, 2, 8, 13, 28 };
            Rounds.Add(r);
            r = new int[32] { 2, 9, 10, 17, 11, 26, 15, 19, 1, 21, 6, 14, 0, 20, 13, 25, 16, 23, 18, 29, 3, 31, 24, 27, 4, 22, 8, 12, 5, 7, 28, 30 };
            Rounds.Add(r);
            r = new int[32] { 2, 14, 30, 31, 5, 12, 10, 20, 3, 19, 6, 16, 7, 8, 9, 25, 18, 22, 26, 27, 0, 23, 1, 11, 4, 15, 21, 28, 13, 29, 17, 24 };
            Rounds.Add(r);
            r = new int[32] { 0, 4, 6, 7, 2, 12, 15, 29, 20, 22, 28, 31, 1, 3, 8, 10, 11, 27, 13, 30, 9, 24, 18, 21, 5, 16, 25, 26, 14, 23, 17, 19 };
            Rounds.Add(r);

            los = new Los(36, GameType.Defined, "36 hráčů, 7 kol", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[36] { 2, 15, 18, 33, 7, 29, 14, 28, 1, 35, 13, 26, 16, 34, 23, 25, 9, 27, 19, 30, 0, 5, 8, 12, 3, 32, 20, 24, 6, 10, 22, 31, 4, 17, 11, 21 };
            Rounds.Add(r);
            r = new int[36] { 18, 19, 20, 23, 11, 26, 16, 33, 3, 9, 4, 13, 1, 30, 5, 28, 6, 21, 8, 14, 0, 10, 2, 29, 17, 31, 24, 25, 7, 32, 27, 34, 12, 15, 22, 35 };
            Rounds.Add(r);
            r = new int[36] { 0, 19, 21, 34, 1, 9, 8, 11, 5, 23, 26, 31, 13, 15, 14, 32, 2, 20, 17, 22, 4, 7, 16, 24, 3, 30, 25, 35, 6, 27, 29, 33, 10, 28, 12, 18 };
            Rounds.Add(r);
            r = new int[36] { 9, 23, 14, 33, 3, 29, 15, 34, 6, 35, 17, 28, 4, 25, 10, 26, 1, 32, 18, 31, 2, 21, 7, 12, 5, 13, 11, 19, 8, 20, 16, 27, 0, 22, 24, 30 };
            Rounds.Add(r);
            r = new int[36] { 3, 7, 5, 6, 2, 32, 8, 19, 11, 12, 14, 24, 4, 33, 31, 34, 1, 25, 22, 29, 17, 18, 26, 27, 10, 23, 15, 30, 0, 16, 13, 28, 9, 20, 21, 35 };
            Rounds.Add(r);
            r = new int[36] { 5, 22, 9, 32, 11, 20, 29, 31, 3, 19, 12, 26, 0, 18, 7, 25, 8, 35, 10, 33, 2, 23, 4, 27, 1, 17, 14, 16, 15, 28, 21, 24, 6, 30, 13, 34 };
            Rounds.Add(r);
            r = new int[36] { 7, 30, 8, 17, 11, 28, 25, 27, 0, 4, 32, 35, 13, 21, 23, 29, 5, 10, 24, 34, 2, 26, 6, 9, 3, 18, 14, 22, 15, 16, 19, 31, 1, 33, 12, 20 };
            Rounds.Add(r);

            los = new Los(40, GameType.Defined, "40 hráčů, 8 kol", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[40] { 13, 25, 22, 30, 0, 29, 28, 31, 1, 8, 6, 38, 4, 34, 23, 32, 3, 21, 12, 36, 9, 11, 27, 39, 7, 20, 33, 35, 2, 17, 14, 37, 10, 15, 18, 24, 5, 26, 16, 19 };
            Rounds.Add(r);
            r = new int[40] { 1, 29, 11, 36, 9, 10, 22, 38, 6, 33, 18, 37, 2, 27, 25, 28, 4, 7, 5, 31, 8, 30, 21, 26, 3, 19, 13, 34, 12, 24, 17, 35, 0, 15, 14, 32, 16, 23, 20, 39 };
            Rounds.Add(r);
            r = new int[40] { 4, 20, 6, 29, 0, 30, 12, 37, 5, 34, 14, 28, 3, 35, 26, 39, 1, 10, 19, 21, 8, 16, 27, 32, 2, 15, 7, 22, 13, 33, 17, 31, 9, 25, 18, 36, 11, 24, 23, 38 };
            Rounds.Add(r);
            r = new int[40] { 1, 31, 12, 16, 0, 25, 3, 10, 14, 35, 21, 27, 19, 39, 29, 30, 8, 20, 9, 15, 22, 33, 23, 28, 2, 38, 34, 36, 4, 24, 13, 37, 5, 6, 11, 32, 7, 18, 17, 26 };
            Rounds.Add(r);
            r = new int[40] { 22, 27, 24, 36, 16, 34, 25, 29, 2, 33, 4, 8, 6, 15, 17, 23, 3, 20, 14, 38, 10, 26, 11, 31, 1, 35, 30, 32, 12, 13, 18, 39, 7, 37, 19, 28, 0, 21, 5, 9 };
            Rounds.Add(r);
            r = new int[40] { 25, 32, 37, 39, 6, 35, 31, 36, 12, 15, 29, 33, 14, 16, 18, 30, 3, 22, 11, 17, 9, 26, 24, 34, 1, 7, 13, 27, 5, 8, 10, 23, 0, 20, 2, 19, 4, 38, 21, 28 };
            Rounds.Add(r);
            r = new int[40] { 3, 7, 9, 29, 11, 25, 19, 33, 6, 28, 10, 12, 0, 18, 4, 22, 13, 36, 23, 26, 17, 30, 27, 34, 15, 16, 35, 38, 1, 5, 20, 37, 8, 39, 14, 31, 2, 32, 21, 24 };
            Rounds.Add(r);
            r = new int[40] { 2, 9, 23, 31, 1, 15, 25, 26, 11, 20, 12, 34, 5, 27, 18, 29, 4, 10, 14, 36, 17, 19, 32, 38, 3, 24, 30, 33, 8, 28, 13, 35, 16, 21, 22, 37, 0, 6, 7, 39 };
            Rounds.Add(r);

            los = new Los(92, GameType.Defined, "92 hráčů, 10 kol", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[92] { 8, 33, 22, 34, 14, 29, 31, 88, 7, 26, 48, 79, 12, 16, 69, 83, 1, 39, 10, 72, 15, 82, 38, 75, 3, 43, 5, 21, 20, 45, 60, 87, 17, 85, 35, 90, 0, 53, 42, 68, 28, 76, 46, 86, 19, 49, 44, 74, 27, 84, 64, 77, 18, 70, 51, 80, 6, 56, 37, 47, 23, 71, 36, 41, 55, 91, 57, 78, 9, 67, 30, 52, 11, 32, 25, 89, 40, 66, 50, 65, 54, 62, 61, 73, 4, 63, 58, 59, 2, 13, 24, 81 };
            Rounds.Add(r);
            r = new int[92] { 11, 39, 30, 47, 29, 72, 37, 49, 19, 81, 43, 65, 17, 38, 67, 87, 2, 31, 4, 69, 6, 48, 15, 66, 35, 41, 56, 58, 34, 63, 64, 71, 20, 51, 53, 77, 3, 74, 9, 59, 13, 26, 16, 78, 21, 25, 23, 60, 1, 8, 54, 89, 40, 85, 82, 86, 42, 76, 52, 57, 7, 84, 18, 90, 32, 75, 33, 44, 27, 55, 28, 80, 12, 91, 50, 61, 14, 36, 22, 70, 46, 79, 62, 68, 5, 45, 73, 83, 0, 88, 10, 24 };
            Rounds.Add(r);
            r = new int[92] { 5, 30, 10, 18, 2, 11, 45, 84, 21, 87, 31, 53, 13, 72, 58, 88, 9, 79, 19, 91, 8, 20, 35, 65, 12, 68, 22, 23, 0, 90, 62, 66, 17, 42, 55, 86, 14, 71, 32, 82, 41, 47, 52, 89, 1, 48, 44, 69, 16, 40, 34, 43, 37, 85, 50, 57, 3, 46, 36, 77, 6, 39, 24, 59, 26, 33, 28, 63, 38, 61, 49, 51, 7, 73, 56, 67, 15, 64, 54, 81, 25, 27, 70, 83, 4, 75, 74, 76, 29, 60, 78, 80 };
            Rounds.Add(r);
            r = new int[92] { 10, 49, 15, 22, 37, 91, 64, 82, 9, 27, 54, 78, 8, 23, 13, 74, 32, 84, 66, 70, 39, 69, 63, 68, 0, 86, 5, 79, 46, 53, 60, 65, 14, 28, 47, 61, 43, 83, 44, 50, 24, 45, 26, 58, 25, 34, 42, 87, 12, 89, 29, 77, 20, 73, 30, 90, 7, 38, 35, 81, 16, 71, 31, 80, 18, 75, 36, 40, 1, 11, 52, 55, 33, 88, 51, 62, 48, 59, 56, 76, 6, 19, 17, 57, 3, 4, 41, 72, 2, 21, 67, 85 };
            Rounds.Add(r);
            r = new int[92] { 18, 45, 64, 68, 42, 79, 51, 84, 0, 48, 2, 91, 19, 25, 76, 90, 37, 63, 65, 80, 36, 81, 53, 85, 5, 12, 38, 52, 3, 35, 88, 89, 9, 17, 10, 62, 7, 82, 41, 87, 6, 27, 61, 75, 28, 29, 58, 73, 1, 40, 4, 60, 23, 78, 34, 69, 50, 72, 59, 77, 30, 31, 46, 70, 13, 43, 14, 49, 20, 57, 26, 32, 33, 55, 66, 67, 11, 71, 21, 22, 8, 47, 24, 44, 54, 74, 83, 86, 15, 16, 39, 56 };
            Rounds.Add(r);
            r = new int[92] { 55, 56, 72, 89, 16, 88, 70, 91, 6, 11, 49, 63, 23, 81, 42, 75, 39, 58, 74, 80, 17, 25, 20, 48, 8, 71, 10, 61, 1, 36, 27, 30, 12, 13, 35, 86, 22, 47, 26, 69, 0, 18, 77, 85, 37, 51, 66, 73, 14, 64, 59, 83, 32, 90, 43, 60, 4, 82, 29, 79, 9, 76, 50, 53, 15, 46, 45, 67, 2, 7, 5, 33, 19, 28, 34, 68, 38, 40, 41, 62, 31, 52, 44, 65, 3, 87, 78, 84, 21, 54, 24, 57 };
            Rounds.Add(r);
            r = new int[92] { 2,62,49,75,15,41,31,59,0,46,6,81,7,63,23,24,28,36,48,89,35,80,40,83,4,78,19,73,32,51,47,68,25,84,44,82,9,42,29,43,1,71,3,33,57,70,58,60,5,72,26,90,27,79,65,69,13,66,56,61,18,37,38,86,17,22,39,52,10,74,50,67,16,76,20,85,34,91,45,77,54,87,55,88,11,53,12,64,8,14,21,30
};
            Rounds.Add(r);
            r = new int[92] { 22, 67, 76, 77, 43, 84, 48, 72, 0, 41, 9, 25, 7, 86, 53, 59, 37, 79, 71, 90, 12, 34, 18, 82, 35, 64, 55, 70, 3, 54, 44, 58, 26, 85, 52, 62, 10, 42, 31, 47, 6, 33, 20, 23, 15, 61, 60, 88, 8, 17, 46, 80, 19, 38, 27, 56, 1, 75, 63, 66, 5, 39, 50, 78, 13, 65, 51, 89, 11, 69, 14, 24, 36, 49, 87, 91, 21, 29, 32, 83, 4, 45, 28, 57, 2, 73, 16, 68, 30, 40, 74, 81 };
            Rounds.Add(r);
            r = new int[92] { 8, 16, 59, 62, 6, 78, 12, 79, 0, 72, 23, 83, 20, 71, 67, 81, 32, 56, 36, 63, 2, 14, 35, 54, 1, 24, 74, 91, 5, 42, 70, 89, 50, 82, 69, 90, 17, 53, 58, 66, 3, 10, 28, 37, 19, 29, 30, 64, 41, 55, 51, 85, 49, 84, 73, 76, 7, 34, 47, 60, 4, 21, 9, 80, 27, 44, 39, 87, 13, 38, 22, 31, 68, 86, 77, 88, 11, 48, 40, 57, 43, 61, 45, 52, 25, 46, 26, 75, 15, 65, 18, 33 };
            Rounds.Add(r);
            r = new int[92] { 21, 72, 47, 65, 6, 76, 10, 80, 25, 68, 31, 81, 19, 83, 22, 37, 32, 85, 59, 69, 44, 64, 51, 78, 27, 35, 46, 74, 34, 66, 36, 54, 7, 49, 8, 9, 14, 48, 38, 90, 18, 88, 26, 43, 2, 23, 29, 87, 20, 24, 62, 70, 4, 5, 53, 71, 52, 63, 86, 91, 11, 15, 50, 73, 12, 75, 55, 60, 28, 77, 30, 42, 39, 45, 79, 89, 3, 56, 17, 40, 16, 41, 33, 61, 0, 57, 13, 82, 1, 67, 58, 84 };
            Rounds.Add(r);
            #endregion
            #region Teams
            los = new Los(8, GameType.Teams, "4 dvojice,4 kola", true);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 4, 5, 2, 3, 6, 7 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 6, 7, 2, 3, 4, 5 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 4, 5, 2, 3, 6, 7 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 6, 7, 2, 3, 4, 5 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 4, 5, 2, 3, 6, 7 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 6, 7, 2, 3, 4, 5 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 4, 5, 2, 3, 6, 7 };
            Rounds.Add(r);
            r = new int[8] { 0, 1, 6, 7, 2, 3, 4, 5 };
            Rounds.Add(r);

            los = new Los(10, GameType.Teams, "5 dvojic,2 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Rounds.Add(r);
            r = new int[10] { 0, 1, 4, 5, 2, 3, 8, 9, 6, 7 };
            Rounds.Add(r);
            r = new int[10] { 0, 1, 6, 7, 4, 5, 8, 9, 2, 3 };
            Rounds.Add(r);
            r = new int[10] { 0, 1, 8, 9, 2, 3, 6, 7, 4, 5 };
            Rounds.Add(r);
            r = new int[10] { 2, 3, 4, 5, 6, 7, 8, 9, 0, 1 };
            Rounds.Add(r);
            r = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Rounds.Add(r);
            r = new int[10] { 0, 1, 4, 5, 2, 3, 8, 9, 6, 7 };
            Rounds.Add(r);
            r = new int[10] { 0, 1, 6, 7, 4, 5, 8, 9, 2, 3 };
            Rounds.Add(r);
            r = new int[10] { 0, 1, 8, 9, 2, 3, 6, 7, 4, 5 };
            Rounds.Add(r);
            r = new int[10] { 2, 3, 4, 5, 6, 7, 8, 9, 0, 1 };
            Rounds.Add(r);


            los = new Los(12, GameType.Teams, "6 dvojic,2 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            r = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 0, 1, 4, 5, 8, 9, 6, 7, 10, 11, 2, 3 };
            Rounds.Add(r);
            r = new int[12] { 0, 1, 8, 9, 10, 11, 6, 7, 4, 5, 2, 3 };
            Rounds.Add(r);
            r = new int[12] { 0, 1, 10, 11, 4, 5, 8, 9, 6, 7, 2, 3 };
            Rounds.Add(r);
            r = new int[12] { 0, 1, 6, 7, 4, 5, 10, 11, 8, 9, 2, 3 };
            Rounds.Add(r);
            r = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            Rounds.Add(r);
            r = new int[12] { 0, 1, 4, 5, 8, 9, 6, 7, 10, 11, 2, 3 };
            Rounds.Add(r);
            r = new int[12] { 0, 1, 8, 9, 10, 11, 6, 7, 4, 5, 2, 3 };
            Rounds.Add(r);
            r = new int[12] { 0, 1, 10, 11, 4, 5, 8, 9, 6, 7, 2, 3 };
            Rounds.Add(r);
            r = new int[12] { 0, 1, 6, 7, 4, 5, 10, 11, 8, 9, 2, 3 };
            Rounds.Add(r);

            los = new Los(14, GameType.Teams, "7 dvojic,2 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            
            r = new int[14] { 2, 3, 12, 13, 4, 5, 10, 11, 6, 7, 8, 9, 0, 1 };
            Rounds.Add(r);
            r = new int[14] { 0, 1, 2, 3, 4, 5, 12, 13, 10, 11, 6, 7, 8, 9 };
            Rounds.Add(r);
            r = new int[14] { 0, 1, 4, 5, 6, 7, 12, 13, 8, 9, 10, 11, 2, 3 };
            Rounds.Add(r);
            r = new int[14] { 2, 3, 4, 5, 0, 1, 6, 7, 8, 9, 12, 13, 10, 11 };
            Rounds.Add(r);
            r = new int[14] { 2, 3, 6, 7, 0, 1, 8, 9, 10, 11, 12, 13, 4, 5 };
            Rounds.Add(r);
            r = new int[14] { 4, 5, 6, 7, 2, 3, 8, 9, 0, 1, 10, 11, 12, 13 };
            Rounds.Add(r);
            r = new int[14] { 4, 5, 8, 9, 2, 3, 10, 11, 0, 1, 12, 13, 6, 7 };
            Rounds.Add(r);

            r = new int[14] { 2, 3, 12, 13, 4, 5, 10, 11, 6, 7, 8, 9, 0, 1 };
            Rounds.Add(r);
            r = new int[14] { 0, 1, 2, 3, 4, 5, 12, 13, 10, 11, 6, 7, 8, 9 };
            Rounds.Add(r);
            r = new int[14] { 0, 1, 4, 5, 6, 7, 12, 13, 8, 9, 10, 11, 2, 3 };
            Rounds.Add(r);
            r = new int[14] { 2, 3, 4, 5, 0, 1, 6, 7, 8, 9, 12, 13, 10, 11 };
            Rounds.Add(r);
            r = new int[14] { 2, 3, 6, 7, 0, 1, 8, 9, 10, 11, 12, 13, 4, 5 };
            Rounds.Add(r);
            r = new int[14] { 4, 5, 6, 7, 2, 3, 8, 9, 0, 1, 10, 11, 12, 13 };
            Rounds.Add(r);
            r = new int[14] { 4, 5, 8, 9, 2, 3, 10, 11, 0, 1, 12, 13, 6, 7 };
            Rounds.Add(r);

            los = new Los(16, GameType.Teams, "8 dvojic,2 kola", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
                      
        
            r = new int[16] { 0,1,14,15,2,3,12,13,4,5,10,11,6,7,8,9 };
            Rounds.Add(r);
            r = new int[16] { 0,1,2,3,12,13,4,5,14,15,8,9,10,11,6,7};
            Rounds.Add(r);
            r = new int[16] { 2,3,14,15,4,5,0,1,6,7,12,13,8,9,10,11};
            Rounds.Add(r);
            r = new int[16] { 2,3,4,5,0,1,6,7,14,15,10,11,12,13,8,9};
            Rounds.Add(r);
            r = new int[16] { 6,7,2,3,4,5,14,15,8,9,0,1,10,11,12,13};
            Rounds.Add(r);
            r = new int[16] { 4,5,6,7,2,3,8,9,14,15,12,13,0,1,10,11};
            Rounds.Add(r);
            r = new int[16] { 8,9,4,5,6,7,14,15,10,11,2,3,12,13,0,1};
            Rounds.Add(r);
            r = new int[16] { 0, 1, 14, 15, 2, 3, 12, 13, 4, 5, 10, 11, 6, 7, 8, 9 };
            Rounds.Add(r);
            r = new int[16] { 0, 1, 2, 3, 12, 13, 4, 5, 14, 15, 8, 9, 10, 11, 6, 7 };
            Rounds.Add(r);
            r = new int[16] { 2, 3, 14, 15, 4, 5, 0, 1, 6, 7, 12, 13, 8, 9, 10, 11 };
            Rounds.Add(r);
            r = new int[16] { 2, 3, 4, 5, 0, 1, 6, 7, 14, 15, 10, 11, 12, 13, 8, 9 };
            Rounds.Add(r);
            r = new int[16] { 6, 7, 2, 3, 4, 5, 14, 15, 8, 9, 0, 1, 10, 11, 12, 13 };
            Rounds.Add(r);
            r = new int[16] { 4, 5, 6, 7, 2, 3, 8, 9, 14, 15, 12, 13, 0, 1, 10, 11 };
            Rounds.Add(r);
            r = new int[16] { 8, 9, 4, 5, 6, 7, 14, 15, 10, 11, 2, 3, 12, 13, 0, 1 };
            Rounds.Add(r);

            los = new Los(16, GameType.Teams, "8 dvojic,1 kolo", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);


            r = new int[16] { 0, 1, 14, 15, 2, 3, 12, 13, 4, 5, 10, 11, 6, 7, 8, 9 };
            Rounds.Add(r);
            
            r = new int[16] { 2, 3, 14, 15, 4, 5, 0, 1, 6, 7, 12, 13, 8, 9, 10, 11 };
            Rounds.Add(r);
            r = new int[16] { 2, 3, 4, 5, 0, 1, 6, 7, 14, 15, 10, 11, 12, 13, 8, 9 };
            Rounds.Add(r);
            r = new int[16] { 6, 7, 2, 3, 4, 5, 14, 15, 8, 9, 0, 1, 10, 11, 12, 13 };
            Rounds.Add(r);
            r = new int[16] { 4, 5, 6, 7, 2, 3, 8, 9, 14, 15, 12, 13, 0, 1, 10, 11 };
            Rounds.Add(r);
            r = new int[16] { 8, 9, 4, 5, 6, 7, 14, 15, 10, 11, 2, 3, 12, 13, 0, 1 };
            Rounds.Add(r);
            r = new int[16] { 0, 1, 2, 3, 12, 13, 4, 5, 14, 15, 8, 9, 10, 11, 6, 7 };
            Rounds.Add(r);


            los = new Los(18, GameType.Teams, "9 dvojic,1 kolo", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            //2-9,3-8,4-7,5-6,1
            //1-2,9-3,8-4,7-5,6
            //3-1,4-9,5-8,6-7,2
            //2-3,1-4,9-5,8-6,7
            //4-2,5-1,6-9,7-8,3
            //3-4,2-5,1-6,9-7,8
            //5-3,6-2,7-1,8-9,4
            //4-5,3-6,2-7,1-8,9
            //6-4,7-3,8-2,9-1,5
            r = new int[18] { 2,3,16,17,4,5,14,15,6,7,12,13,8,9,10,11,0,1};
            Rounds.Add(r);
            r = new int[18] { 0,1,2,3,16,17,4,5,14,15,6,7,12,13,8,9,10,11 };
            Rounds.Add(r);
            r = new int[18] { 4,5,0,1,6,7,16,17,8,9,14,15,10,11,12,13,2,3};
            Rounds.Add(r);
            r = new int[18] { 2,3,4,5,0,1,6,7,16,17,8,9,14,15,10,11,12,13};
            Rounds.Add(r);
            r = new int[18] { 6,7,2,3,8,9,0,1,10,11,16,17,12,13,14,15,4,5};
            Rounds.Add(r);
            r = new int[18] { 4,5,6,7,2,3,8,9,0,1,10,11,16,17,12,13,14,15};
            Rounds.Add(r);
            r = new int[18] { 8,9,4,5,10,11,2,3,12,13,0,1,14,15,16,17,6,7};
            Rounds.Add(r);
            r = new int[18] {6,7,8,9,4,5,10,11,2,3,12,13,0,1,14,15,16,17 };
            Rounds.Add(r);
            r = new int[18] { 10,11,6,7,12,13,4,5,14,15,2,3,16,17,0,1,8,9};
            Rounds.Add(r);


            los = new Los(20, GameType.Teams, "10 dvojic,1 kolo", false);
            Rounds = new List<int[]>();
            los.Rounds = Rounds;
            losy.Add(los);
            //1-10,2-9,3-8,4-7,5,6
            //5-6,1-2,9-3,8-4,7,10
            //7-5,10-6,3-1,4-9,2,8
            //5-8,6-7,2-10,1-4,3,9
            //9-5,2-3,8-6,10-7,1,4
            //4-2,5-1,3-10,6-9,7,8
            //7-8,3-4,2-5,1-6,9,10
            //9-7,10-8,5-3,6-2,1,4
            //3-6,2-7,1-8,10-9,4,5
            //6-4,7-3,8-2,9-1,5,10
            //7-1,10-4,8-9,0,0,0,0
            //5-10,4-5,0,0,0,0,0,0
            r = new int[16] { 0, 1, 18, 19, 2, 3, 16, 17, 4, 5, 14, 15, 6, 7, 12, 13};
            Rounds.Add(r);
            r = new int[16] { 8, 9, 10, 11, 0, 1, 2, 3, 16, 17, 4, 5, 14, 15, 6, 7};
            Rounds.Add(r);
            r = new int[16] { 12, 13, 8, 9, 18, 19, 10, 11, 4, 5, 0, 1, 6, 7, 16, 17 };
            Rounds.Add(r);
            r = new int[16] { 8, 9, 14, 15, 10, 11, 12, 13, 2, 3, 18, 19, 0, 1, 6, 7 };
            Rounds.Add(r);
            r = new int[16] { 16, 17, 8, 9, 2, 3, 4, 5, 14, 15, 10, 11, 18, 19, 12, 13 };
            Rounds.Add(r);
            r = new int[16] { 6, 7, 2, 3, 8, 9, 0, 1, 4, 5, 18, 19, 10, 11, 16, 17 };
            Rounds.Add(r);            
            r = new int[16] { 16, 17, 12, 13, 18, 19, 14, 15, 8, 9, 4, 5, 10, 11, 2, 3 };
            Rounds.Add(r);
            r = new int[16] { 12, 13, 14, 15, 4, 5, 6, 7, 2, 3, 8, 9, 0, 1, 10, 11 };
            Rounds.Add(r);
            r = new int[16] { 4, 5, 10, 11, 2, 3, 12, 13, 0, 1, 14, 15, 18, 19, 16, 17 };
            Rounds.Add(r);
            r = new int[16] { 10, 11, 6, 7, 12, 13, 4, 5, 14, 15, 2, 3, 8, 9, 18, 19 };
            Rounds.Add(r);
            r = new int[16] { 12, 13, 0, 1, 18, 19, 6, 7, 14, 15, 16, 17, 2, 3, 2, 3 };
            Rounds.Add(r);
            r = new int[16] { 6, 7, 8, 9, 16, 17, 0, 1, 2, 3, 2, 3, 2, 3, 2, 3 };
            Rounds.Add(r);

            /*r = new int[20] {0,1,18,19,2,3,16,17,4,5,14,15,6,7,12,13,8,9,10,11};
            Rounds.Add(r);
            r = new int[20] { 8,9,10,11,0,1,2,3,16,17,4,5,14,15,6,7,12,13,18,19 };
            Rounds.Add(r);
            r = new int[20] {12,13,8,9,18,19,10,11,4,5,0,1,6,7,16,17,2,3,14,15};
            Rounds.Add(r);
            r = new int[20] { 8,9,14,15,10,11,12,13,2,3,18,19,0,1,6,7,4,5,16,17 };
            Rounds.Add(r);
            r = new int[20] { 16,17,8,9,2,3,4,5,14,15,10,11,18,19,12,13,0,1,6,7 };
            Rounds.Add(r);
            r = new int[20] { 6,7,2,3,8,9,0,1,4,5,18,19,10,11,16,17,12,13,14,15};
            Rounds.Add(r);
            r = new int[20] {12,13,14,15,4,5,6,7,2,3,8,9,0,1,10,11,16,17,18,19};
            Rounds.Add(r);
            r = new int[20] { 16,17,12,13,18,19,14,15,8,9,4,5,10,11,2,3,0,1,6,7 };
            Rounds.Add(r);
            r = new int[20] { 4,5,10,11,2,3,12,13,0,1,14,15,18,19,16,17,6,7,8,9};
            Rounds.Add(r);
            r = new int[20] { 10,11,6,7,12,13,4,5,14,15,2,3,16,17,0,1,8,9,18,19 };
            Rounds.Add(r);
            r = new int[20] { 12,13,0,1,18,19,6,7,14,15,16,17,0,0,0,0,0,0,0,0};
            Rounds.Add(r);
            r = new int[20] { 8,9,18,19,6,7,8,9,0,0,0,0,0,0,0,0,0,0,0,0};
            Rounds.Add(r);*/
            #endregion

            

        }
    }
}
