using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    [Serializable]
    public class Permutace
    {
        public int Count = 1;
        int N = 0;
        public List<int[]> Matches;
        private bool OnlyMix;
        public Permutace(int n,bool onlyMix)
        {
            N = n;
            int[] permutace = new int[N];
            OnlyMix = onlyMix;
            GenerateMatches();
        }


        public void GenerateMatches()
        {
            List<Tuple<int, int>> pairs = new List<Tuple<int, int>>();
            for (int i = 0; i < N; i++)
            {
                for (int j = i; j < N; j++)
                {
                    if (i != j)
                    {
                        pairs.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            Matches = new List<int[]>();
            for (int i = 0; i < pairs.Count; i++)
            {
                for (int j = i; j < pairs.Count; j++)
                {
                    var p1 = pairs[i].Item1;
                    var p2 = pairs[i].Item2;
                    var p3 = pairs[j].Item1;
                    var p4 = pairs[j].Item2;
                    if (p1 != p2 && p1 != p3 && p1 != p4 && p2 != p3 && p2 != p4 && p3 != p4)
                    {
                        if (!OnlyMix || (p1 % 2 != p2 % 2 && p3 % 2 != p4 % 2))
                        {
                            int[] Round = new int[] { pairs[i].Item1, pairs[i].Item2, pairs[j].Item1, pairs[j].Item2 };
                            Matches.Add(Round);
                        }
                    }
                }
            }
        }

    }
}
