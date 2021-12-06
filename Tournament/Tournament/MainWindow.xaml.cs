using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Tournament
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private DataTable table;
        public DataModel Model;
        public Losovani Losovani;
        private Style headerStyle;
        public MainWindow()
		{
            
			InitializeComponent();
            table = new DataTable();
            Model = new DataModel();
            Model.InitLosy();
            Model.InitRegister();
            Model.InitHelper();
            Model.Players = new List<Player>();
            Model.Game = GameType.Defined;
            Model.CourtCount = 4;
            Model.RandomType = RandomType.UniqueCoPlayers;
            Model.ScoreType = ScoreType.Sets;
            Model.ShowScore = true;
            headerStyle = new Style();
            headerStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.BlueViolet));

            Maximize();
            
            if (!Model.IsRegistred)
            {
                btnRegister_Click(null, null);
            }
        }
        
        private void Maximize()
        {
            
            this.WindowStartupLocation=WindowStartupLocation.CenterScreen;
            this.WindowState = WindowState.Maximized;
            //window.Show();
        }

        private void btnVysledek_Click(object sender, RoutedEventArgs e)
        {
            LoadResult();
            Save("tmp.xml");
            PopulateRows();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            NextRound();          
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            Model.RoundIndex--;
            Model.Round = Model.RoundsForCourt.LastRound;
            SetupActualRound();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            LoadSettings(false);
            PopulateRows();
            this.UpdateLayout();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            
            bool isNewGame =LoadSettings(true);            
            if (isNewGame && Model.Count > 0)
            {
                NewGame();
            }

        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            TextReader reader = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePathPlayer = "";
            if (openFileDialog.ShowDialog() == true)
            {
                filePathPlayer = openFileDialog.FileName;
            }
            else
            {
                return;
            }

            //string filePathPlayer = string.Format("{0}{1}.xml", AppDomain.CurrentDomain.BaseDirectory, Model.FilePathResultFormat);

            try
            {
                var serializer = new XmlSerializer(typeof(DataModel));
                reader = new StreamReader(filePathPlayer);
                Model = (DataModel)serializer.Deserialize(reader);
                //string filePathLosovani = string.Format("{0}{1}{2}.csv", AppDomain.CurrentDomain.BaseDirectory, Model.FilePathRoundsFormat, Model.Count);
                //StreamReader sr = new StreamReader(filePathLosovani);
                //string line = sr.ReadLine();
                //Model.Rounds = new List<int[]>();
                /*while (!string.IsNullOrEmpty(line))
                {
                    string[] roundStr = line.Split(',');
                    Model.Round = new int[Model.Count];
                    int index = 0;
                    foreach (string p in roundStr)
                    {
                        int n = 0;
                        Int32.TryParse(p, out n);
                        Model.Round[index++] = n;
                    }
                    Model.Rounds.Add(Model.Round);
                    line = sr.ReadLine();
                }*/
                Losovani = new Losovani(Model, true);
                Model.GameLoaded = true;
                if (Model.Game == GameType.Random && Model.OneRandomRound)
                {
                    foreach (var round in Model.RoundsForCourt.AllRounds)
                    {
                        //Losovani.RemoveMatches(round);
                    }
                }                
                SettingsWindow.ChangeLanguage(Model.Language);
                this.UpdateLayout();
                Model.RoundIndex = 0;
                Model.LoadModel();
                if (Model.RoundsForCourt != null)
                {
                    Model.Round = Model.RoundsForCourt.LastRound;
                }
                Model.InitHelper();
                Model.Helper.LoadAvailablePlayers(Model);
                PopulateRows();
                EnableButtons();
                
                
            }
            catch { }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        private void Save(string filePath)
        {
            TextWriter writer = null;
            StreamWriter writer2 = null;
            try
            {
                string filePathLosovani = string.Format("{0}{1}{2}.csv", AppDomain.CurrentDomain.BaseDirectory, Model.FilePathRoundsFormat, Model.Count);
                var serializer = new XmlSerializer(typeof(DataModel));
                writer = new StreamWriter(filePath, false);
                serializer.Serialize(writer, Model);
                writer2 = new StreamWriter(filePathLosovani, false);
                if (Model.RoundsForCourt != null)
                {
                    for (int i = 0; i < Model.RoundsForCourt.Count; i++)
                    {
                        var round = Model.RoundsForCourt.AllRounds[i];
                        int j = 0;
                        foreach (var match in round.Matches)
                        {
                            if (j == 0)
                            {
                                writer2.Write("{0}", match);
                            }
                            else
                            {
                                writer2.Write(",{0}", match);
                            }
                            j++;
                        }
                        writer2.WriteLine();
                    }
                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                if (writer2 != null)
                    writer2.Close();
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            saveFileDialog.DefaultExt = ".xml";
            string filePathPlayer = string.Format("{0}.xml", Model.FilePathResultFormat);
            saveFileDialog.FileName = filePathPlayer;
            if (saveFileDialog.ShowDialog() == true)
            {
                filePathPlayer = saveFileDialog.FileName;
            }
            else
            {
                return;
            }
            Save(filePathPlayer);

        }
        private void btnNames_Click(object sender, RoutedEventArgs e)
        {
            /*Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
            foreach (var round in Model.Rounds)
            {
                for(int i = 0; i + 3 < round.Length; i+=4)
                {
                    int p1 = round[i];
                    int p2 = round[i+1];
                    int p3 = round[i+2];
                    int p4 = round[i+3];
                    AddCo(dic,p1,p2);
                    AddCo(dic, p2, p1);
                    AddCo(dic, p3, p4);
                    AddCo(dic, p4, p3);
                }
            }*/
            NamesWindow names = new NamesWindow(Model);
            names.ShowDialog();
            PopulateRows();
        }
        private void AddCo(Dictionary<int, List<int>> dic, int p1, int p2)
        {
            if (!dic.ContainsKey(p1))
            {
                dic.Add(p1, new List<int>());
            }
            dic[p1].Add(p2);
        }

        private void btnVysledek_Loaded(object sender, RoutedEventArgs e)
        {
            btnVysledek.IsEnabled = false;
        }

        private void btnNext_Loaded(object sender, RoutedEventArgs e)
        {
            btnNext.IsEnabled = false;
        }
        private void btnPrev_Loaded(object sender, RoutedEventArgs e)
        {
            btnPrev.IsEnabled = false;
        }
        private void btnNames_Loaded(object sender, RoutedEventArgs e)
        {
            btnNames.IsEnabled = true;
        }
        private void btnSave_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = false;
        }
        private void LoadResult()
        {
            ResultWindow result = new ResultWindow(Model);
            var isNewResult = result.ShowDialog();
            if (isNewResult.HasValue&&isNewResult.Value && result.AllSaved && Model.RoundIndex < Model.RoundsForCourt.Count-1  )
            {
                NextRound();
            }
           
        }
        private bool LoadSettings(bool newGame)
        {
            SettingsWindow settings = new SettingsWindow(Model,newGame);
            bool? isNewGame = settings.ShowDialog();
            ChangeTitle();
            return isNewGame.HasValue && isNewGame.Value;
        }

        

        private void EnableButtons()
        {
            if (Model.RoundsForCourt != null && Model.RoundsForCourt.Count > 0)
            {
                btnVysledek.IsEnabled = true;
                //btnNames.IsEnabled = true;
                btnSave.IsEnabled = true;
                btnPrintResult.IsEnabled = true;
                btnPrintRound.IsEnabled = true;
                btnList.IsEnabled = true;
                btnSettings.IsEnabled = true;
                hdrSettings.IsEnabled = true;
                
                bool lastRound = Model.RoundIndex == Model.RoundsForCourt.Count-1;
                bool lastRandomRound = Model.Game == GameType.Random && Model.OneRandomRound && lastRound && Model.GameLoaded;
                btnNext.IsEnabled = (Model.Game == GameType.Random&& Model.OneRandomRound) || !lastRound ;
                btnNext.IsEnabled &= !lastRandomRound;
                btnPrev.IsEnabled = Model.RoundIndex > 0;
                btnClear.IsEnabled = Model.RoundIndex > 0 && (Model.Game == GameType.Random || Model.Game == GameType.RandomSwiss);
                if (Model.Game == GameType.RandomSwiss)
                {
                    if (lastRound)
                    {
                        btnNext.IsEnabled = true;
                    }
                }
            }
            hdrSave.IsEnabled = Model.Count>0;
            btnSave.IsEnabled = Model.Count > 0;
        }

        private void NextRound()
        {            
            Model.Round = Losovani.GetNextRound();
            if (Model.Round == null)
            {
                lstRound.Items.Clear();
                lstRound.Items.Add("Next round not exists");
                Model.RoundIndex--;
            }
            if (Model.Round != null)
            {
                PopulateRows();
            }

        }
        private void NewGame()
        {
            Model.GameLoaded = false;
            Model.RoundIndex = -1;
            /*Model.Players.Clear();            
            for (int i = 0; i < Model.Count; i++)
            {
                var player = new Player();
                player.Init(i + 1, Model.Count,Model.SetCount);                
                Model.Players.Add(player);
            }*/
            Model.RoundsForCourt = new Rounds();
            Model.RoundsForCourt.Model = Model;
            Model.Helper.LoadAvailablePlayers(Model);
            Losovani = new Losovani(Model,false);
            Model.LoadModel();
            //Model.AddAllResult();
            /*if (Model.Names.Count > 0)
            {
                int index = 0;
                foreach (var player in Model.Players)
                {
                    player.Name = Model.Names[index];
                    index++;
                    if (index > Model.Names.Count-1)
                    {
                        break;
                    }
                }
            }*/
            NextRound();
            

        }
        private void NewDataTable()
        {
            gridVysledky.Columns.Clear();
            gridVysledky.ItemsSource = null;
            if (Model.Players== null ||  Model.Players.Count == 0)
            {
                return;
            }
            
            table = new DataTable();
            table.Columns.Add(Application.Current.FindResource("Name").ToString());
            if (Model.RoundsForCourt != null)
            {
                for (int i = 0; i < Model.RoundsForCourt.Count; i++)
                {
                    table.Columns.Add("" + (i + 1));
                }
            }
            if (Model.ScoreType == ScoreType.Games)
            {
                table.Columns.Add(Application.Current.FindResource("Games").ToString());
            }
            table.Columns.Add(Application.Current.FindResource("Sets").ToString());
            table.Columns.Add(Application.Current.FindResource("Points").ToString());
            table.Columns.Add(Application.Current.FindResource("No").ToString(), typeof(int));
            table.Columns.Add("Medal", typeof(BitmapImage));
            for (int i = 0; i < table.Columns.Count - 1; i++)
            {
                var column = table.Columns[i];
                DataGridTextColumn col = new DataGridTextColumn();
                col.Header = column.ColumnName;
                col.Binding = new Binding(column.ColumnName);                
                gridVysledky.Columns.Add(col);
            }
            DataGridTemplateColumn col1 = (DataGridTemplateColumn)this.FindResource("dgt");
            gridVysledky.Columns.Add(col1);
            //DataGridTextColumn col2 = new DataGridTextColumn();
            //col2.Header = DateTime.Now.ToShortDateString();
            //gridVysledky.Columns.Add(col2);
            gridVysledky.ItemsSource = table.DefaultView;
            for (int i = 0; i < Model.Count; i++)
            {
                var row = table.NewRow();
                table.Rows.Add(row);
            }
            Model.SortPlayers();
            gridVysledky.ColumnHeaderStyle = headerStyle;

        }
        public void PopulateRows()
        {
            SetupActualRound();
            NewDataTable();
            for (int i = 0; i < Model.Count; i++)
            {
                var player = Model.Players[i];
                DataRow row = table.Rows[i];

                player.PopulateRow(row, Model);
            }

        }
        public static void PrintRound(Round round, ListBox lst, DataModel model)
        {
            List<string> list = new List<string>();
            PrepareRound(round, list, model);
            foreach (string line in list)
            {
                lst.Items.Add(line);
                
            }
        }
        public static void PrepareRound(Round round, List<string> list,DataModel model)
        {
            int index = 0;
            if (model.Game == GameType.Single)
            {
                foreach (var match in round.Matches.OrderBy(m => m.CourtIndex))
                {
                    string roundStr = string.Empty;
                    if (match.P1 >= 0 && match.P2 >= 0)
                    {
                        string left = string.Format("{0}.   {1}", match.Court, match.Player1.Info.LastName);
                        string right = string.Format("{0}", match.Player2.Info.LastName);
                        roundStr = GetRoundString(left, right);
                        list.Add(roundStr);
                    }
                    index++;
                }
            }
            else
            {
                foreach (var match in round.Matches.OrderBy(m => m.CourtIndex))
                    {
                    string roundStr = string.Empty;
                    /*if (DataModel.SamePlayer(match.P1, match.P2, match.P3, match.P4))
                    {
                        break;
                    }*/
                    if (match.P1 >= 0 && match.P2 >= 0 && match.P3 >= 0 && match.P4 >= 0)
                    {
                        string left = string.Format("{0}.   {1} + {2}", match.Court, match.Player1.Info.LastName, match.Player2.Info.LastName);
                        string right = string.Format("{0} + {1}", match.Player3.Info.LastName, match.Player4.Info.LastName);
                        roundStr = GetRoundString(left, right);
                        list.Add(roundStr);
                    }
                    index++;
                }
            }
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (Player sit in round.Sits)
            {                
                if (i==0)
                {
                    sb.AppendFormat(Application.Current.FindResource("RoundSit").ToString()+" :{0}", sit.Info.LastName);
                }
                else
                {
                    
                    if (i % 4 == 0)
                    {
                        sb.AppendLine();
                        sb.AppendFormat("{0}", sit.Info.LastName);
                    }
                    else
                    {
                        sb.AppendFormat(" , {0}", sit.Info.LastName);
                    }
                }        
                
                i++;
            }
            list.Add(sb.ToString());
        }
        private static string GetRoundString(string left, string right)
        {

            FormattedText formattedTextLeft = new FormattedText(left, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 16, Brushes.Black);
            var widthLeft = formattedTextLeft.Width;
            FormattedText formattedTextRight = new FormattedText(right, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 16, Brushes.Black);
            var widthRight = formattedTextRight.Width;
            FormattedText formattedTextSpace = new FormattedText(right + " " + right, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 16, Brushes.Black);
            var widthSpace = formattedTextSpace.Width - 2 * widthRight;
            int countSpace = (int)((widthLeft - widthRight) / widthSpace);
            if (widthLeft > widthRight)
            {
                right = right.PadRight(right.Length + countSpace);
            }
            else
            {
                left = left.PadLeft(left.Length - countSpace);
            }

            return string.Format("{0}      :      {1}", left, right);
        }
        private void SetupActualRound()
        {

            lstRound.Items.Clear();
            lstRound.Items.Add(DateTime.Now.ToShortDateString());
            lstRound.Items.Add(Application.Current.FindResource("Round").ToString() + " " + (Model.RoundIndex + 1));
            if (Model.RoundsForCourt != null && Model.RoundsForCourt.Count > 0)
            {
                var round = Model.RoundsForCourt.LastRound;
                PrintRound(round, lstRound, Model);
                if (Model.RoundIndex + 1 < Model.RoundsForCourt.Count)
                {
                    lstRound.Items.Add(Application.Current.FindResource("NextRound").ToString() + " " + (Model.RoundIndex + 2));
                    round = Model.RoundsForCourt.AllRounds[Model.RoundIndex + 1];
                    PrintRound(round, lstRound, Model);
                }
            }
            EnableButtons();
        }
        
               

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            ListWindow listWindow = new ListWindow(Model);
            listWindow.ShowDialog();

        }

        private void btnPrintResult_Click(object sender, RoutedEventArgs e)
        {
            TableWindow roundWindow = new TableWindow(Model);
            roundWindow.ShowDialog();
        }

        private void btnPrintRound_Click(object sender, RoutedEventArgs e)
        {
            RoundsWindow roundWindow = new RoundsWindow(Model);
            roundWindow.ShowDialog();

        }

        private void btnList_Loaded(object sender, RoutedEventArgs e)
        {
            btnList.IsEnabled = false;
        }

        private void btnPrintResult_Loaded(object sender, RoutedEventArgs e)
        {
            btnPrintResult.IsEnabled = false;
        }

        private void btnPrintRound_Loaded(object sender, RoutedEventArgs e)
        {
            btnPrintRound.IsEnabled = false;
        }

        private void btnSettings_Loaded(object sender, RoutedEventArgs e)
        {
            btnSettings.IsEnabled = false;
        }
        private void ChangeTitle()
        {
            if (!string.IsNullOrEmpty(Model.AppName))
            {
                this.Title = Model.AppName;
            }
        }
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register(Model);
            reg.ShowDialog();
        }

        private void btnPlayer_Click(object sender, RoutedEventArgs e)
        {
            EditPlayer player = new EditPlayer(Model);
            player.ShowDialog();
            Model.Helper.LoadAvailablePlayers(Model);
            if (Model.Players.Count > 0)
            {
                PopulateRows();
            }
        }

        private void btnPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            btnPlayer.IsEnabled = true;
        }
        private void btnClear_Loaded(object sender, RoutedEventArgs e)
        {
            btnClear.IsEnabled = false;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Losovani.ClearRound();
            if (Model.Round != null)
            {
                PopulateRows();
            }
        }
    }
}

