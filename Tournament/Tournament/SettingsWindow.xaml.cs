using System;
using System.Collections.Generic;
using System.Data;
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

namespace Tournament
{
    public enum GameType
    {
        Single,
        Teams,
        Defined,
        Random,
        RandomSwiss,
        Swiss
    }
    public enum RandomType
    {
        UniqueCoPlayers,
        UniqueOpo,
        FullRandomRoundRobin,
        Mix,
        UniqueOpoWoman
    }
    public enum ScoreType
    {
        Sets,
        Points,
        Games,
        SetsScore
    }    
    public enum Language
    {
        Czech,
        English
    }
    public class LanguageItem
    {
        public LanguageItem(Language language,string langKey)
        {
            Language = language;
            LanguageName =(string)Application.Current.FindResource(langKey);
        }
        public Language Language;
        public string LanguageName;
        public override string ToString()
        {
            return LanguageName;
        }
    }
    public class LosItem
    {
        public LosItem(int count,Los los)
        {
            Count = count;
            Los = los;
        }
        public int Count=0;
        public Los Los;        
        public override string ToString()
        {
            if (Los==null ||string.IsNullOrEmpty(Los.Name))
            {
                return string.Format("{0} players", Count);
            }
            else
            {
                return Los.Name;
            }
        }
    }
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class SettingsWindow : Window
	{
        public DataModel Model;
        public bool NewGame = false;
        private bool noChange = false;
        public SettingsWindow(DataModel model,bool newGame)
		{
            Model = model;
            NewGame = newGame;
            InitializeComponent();            
        }
        //public int[] PlayersCount = { 8, 9, 10, 11, 12,13,14, 16,18, 20,22, 24,26, 28, 32, 36, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 84, 88, 92,96,100 };

        private void PopulateCount()
        {
            int count = Model.Count;
            Model.InitLosy();
            if (cmbPlayers != null)
            {
                cmbPlayers.Items.Clear();
                if (Model.Game == GameType.Random || Model.Game == GameType.Swiss || Model.Game == GameType.RandomSwiss)
                {
                    
                    
                    if (Model.Count %2 == 0)
                    {
                        var los = new Los(count, GameType.Random, count + "", true);
                        cmbPlayers.Items.Add(new LosItem(count, los));
                    }
                }
                else
                {
                    foreach (Los los in Model.Losy)
                    {
                        if (los.Type == Model.Game && los.Count == Model.Count)
                        {                         
                            cmbPlayers.Items.Add(new LosItem(los.Count,los));                            
                        }
                    }
                }
                cmbPlayers.SelectedIndex = 0;
                /*
                for ( int i = 0; i < cmbPlayers.Items.Count; i++){
                    var item = cmbPlayers.Items[i] as LosItem;
                    if (item.Count == Model.Count)
                    {
                        cmbPlayers.SelectedIndex = i;
                        break;
                    }
                }*/
                
            }
            EnableButtons();
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPlayers.SelectedItem == null || cmbPlayers.SelectedItem == null)
            {
                DialogResult = false;
                this.Close();
            }
            if (cmbPlayers.IsEnabled)
            {
                var losItem = ((LosItem)cmbPlayers.SelectedItem);
                Model.Count = losItem.Count;
                Model.Los = losItem.Los;
            }
            if (NewGame && cmbCourt !=null && cmbCourt.SelectedItem !=null)
            {
                var court = (int)cmbCourt.SelectedItem;
                Model.CourtCount = court;
            }
            Model.ShowScore = chkScore.IsChecked.Value;            
                        
            if (radPoints.IsChecked.Value)
            {
                Model.ScoreType = ScoreType.Points;
            }
            else if (radSet.IsChecked.Value)
            {
                Model.ScoreType = ScoreType.Sets;
            }
            else if (radSetScore.IsChecked.Value)
            {
                Model.ScoreType = ScoreType.SetsScore;
            }
            else
            {
                Model.ScoreType = ScoreType.Games;
            }
            if (radSet1.IsChecked.Value)
            {
                Model.SetCount = 1;
            }
            else if (radSet2.IsChecked.Value)
            {
                Model.SetCount = 2;
            }
            else
            {
                Model.SetCount = 3;
            }
            Model.SwissGame = (int)cmbPoints.SelectedItem;
            Model.GenerateCourts = chkGen.IsChecked.Value;
            if (!string.IsNullOrEmpty(txtAppName.Text))
            {
                Model.AppName = txtAppName.Text;
            }
            Model.TournamentDate = DateTime.Now;
            DialogResult = true;
            this.Close();
        }
        private void radSingle_Checked(object sender, RoutedEventArgs e)
        {
            Model.Game = GameType.Single;
            PopulateCount();
        }
        private void radTeams_Checked(object sender, RoutedEventArgs e)
        {
            Model.Game = GameType.Teams;
            PopulateCount();
        }

        private void radDef_Checked(object sender, RoutedEventArgs e)
        {
            Model.Game = GameType.Defined;
            PopulateCount();
        }

        private void cmbPlayers_Loaded(object sender, RoutedEventArgs e)
        {
            cmbPlayers.IsEnabled = NewGame;
            PopulateCount();
        }

        private void chkScore_Loaded(object sender, RoutedEventArgs e)
        {
            chkScore.IsChecked = Model.ShowScore;
        }
        private void radSingle_Loaded(object sender, RoutedEventArgs e)
        {
            radSingle.IsEnabled = NewGame;
            radSingle.IsChecked = Model.Game == GameType.Single;
            //EnableButtons();
        }
        private void radTeams_Loaded(object sender, RoutedEventArgs e)
        {
            radTeams.IsEnabled = NewGame;
            radTeams.IsChecked = Model.Game == GameType.Teams;
            //EnableButtons();
        }

        private void radDef_Loaded(object sender, RoutedEventArgs e)
        {
            radDef.IsEnabled = NewGame;
            radDef.IsChecked = Model.Game == GameType.Defined;
            //EnableButtons();
        }

        private void radRandom_Loaded(object sender, RoutedEventArgs e)
        {
            radRandom.IsEnabled = NewGame;
            radRandom.IsChecked = Model.Game == GameType.Random;
            //EnableButtons();
        }

        private void radRandom_Checked(object sender, RoutedEventArgs e)
        {
            Model.Game = GameType.Random;
            PopulateCount();
        }

        private void radRandomSwiss_Loaded(object sender, RoutedEventArgs e)
        {
            radRandomSwiss.IsEnabled = NewGame;
            radRandomSwiss.IsChecked = Model.Game == GameType.RandomSwiss;
            //EnableButtons();
        }
        private void radSwiss_Loaded(object sender, RoutedEventArgs e)
        {
            radSwiss.IsEnabled = NewGame;
            radSwiss.IsChecked = Model.Game == GameType.Swiss;
            //EnableButtons();
        }

        private void radRandomSwiss_Checked(object sender, RoutedEventArgs e)
        {
            Model.Game = GameType.RandomSwiss;
            PopulateCount();
        }
        private void radSwiss_Checked(object sender, RoutedEventArgs e)
        {
            Model.Game = GameType.Swiss;
            PopulateCount();
        }

        private void radSet_Loaded(object sender, RoutedEventArgs e)
        {            
            radSet.IsChecked = Model.ScoreType == ScoreType.Sets;
        }
        private void radSetScore_Loaded(object sender, RoutedEventArgs e)
        {
            radSetScore.IsChecked = Model.ScoreType == ScoreType.SetsScore;
        }
        private void radGames_Loaded(object sender, RoutedEventArgs e)
        {
            radGames.IsChecked = Model.ScoreType == ScoreType.Games;
            
        }
        private void radPoints_Loaded(object sender, RoutedEventArgs e)
        {
            radPoints.IsChecked = Model.ScoreType == ScoreType.Points;
        }    

        

        private void cmbPoints_Loaded(object sender, RoutedEventArgs e)
        {            
            if (cmbPoints != null)
            {
                cmbPoints.Items.Clear();
                for (int point = 10; point < 21; point++)
                {
                    cmbPoints.Items.Add(point);
                }
                cmbPoints.IsEnabled = radGames.IsChecked.Value;
                cmbPoints.SelectedIndex = 1;
            }
        }

        private void radPoints_Click(object sender, RoutedEventArgs e)
        {
            cmbPoints.IsEnabled = radGames.IsChecked.Value;
            
        }

        private void radSet1_Loaded(object sender, RoutedEventArgs e)
        {
            radSet1.IsEnabled = NewGame;
            radSet1.IsChecked = Model.SetCount==1;
        }       

        private void radSet2_Loaded(object sender, RoutedEventArgs e)
        {
            radSet2.IsEnabled = NewGame;
            radSet2.IsChecked = Model.SetCount == 2;
        }

        private void radSet3_Loaded(object sender, RoutedEventArgs e)
        {
            radSet3.IsEnabled = NewGame;
            radSet3.IsChecked = Model.SetCount == 3;
        }
        private void radSet1_Click(object sender, RoutedEventArgs e)
        {

        }      

        private void radSet2_Click(object sender, RoutedEventArgs e)
        {

        }
        private void radSet3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void radUniqueCo_Checked(object sender, RoutedEventArgs e)
        {
            Model.RandomType = RandomType.UniqueCoPlayers;
        }

        private void radUniqueOpo_Checked(object sender, RoutedEventArgs e)
        {
            Model.RandomType = RandomType.UniqueOpo;
        }
        private void radUniqueOpoWoman_Checked(object sender, RoutedEventArgs e)
        {
            Model.RandomType = RandomType.UniqueOpoWoman;
        }

        private void radFull_Checked(object sender, RoutedEventArgs e)
        {
            Model.RandomType = RandomType.FullRandomRoundRobin;
        }

        private void radMix_Checked(object sender, RoutedEventArgs e)
        {
            Model.RandomType = RandomType.Mix;
        }

        private void radUniqueCo_Loaded(object sender, RoutedEventArgs e)
        {
            radUniqueCo.IsEnabled = NewGame && radRandom.IsChecked.Value;
            radUniqueCo.IsChecked = Model.RandomType == RandomType.UniqueCoPlayers;
        }

        private void radUniqueOpo_Loaded(object sender, RoutedEventArgs e)
        {
            radUniqueOpo.IsEnabled = NewGame;
            radUniqueOpo.IsEnabled = NewGame && radRandom.IsChecked.Value;
            radUniqueOpo.IsChecked = Model.RandomType == RandomType.UniqueOpo;
        }

        private void radUniqueOpoWoman_Loaded(object sender, RoutedEventArgs e)
        {
            radUniqueOpoWoman.IsEnabled = NewGame;
            radUniqueOpoWoman.IsEnabled = NewGame && radRandom.IsChecked.Value;
            radUniqueOpoWoman.IsChecked = Model.RandomType == RandomType.UniqueOpoWoman;
        }

        private void radFull_Loaded(object sender, RoutedEventArgs e)
        {
            radFull.IsEnabled = NewGame && radRandom.IsChecked.Value;
            radFull.IsChecked = Model.RandomType == RandomType.FullRandomRoundRobin;
        }

        private void radMix_Loaded(object sender, RoutedEventArgs e)
        {
            radMix.IsEnabled = NewGame && radRandom.IsChecked.Value;
            radMix.IsChecked = Model.RandomType == RandomType.Mix;
        }

        private void cmbCourt_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCourt.IsEnabled = NewGame;
            cmbCourt.Items.Clear();

            for (int i = 1; i < 11; i++)
            {
                cmbCourt.Items.Add(i);
            }
            cmbCourt.SelectedIndex = Model.CourtCount-1;
        }
        private void EnableButtons()
        {
            radUniqueCo.IsEnabled = radRandom.IsChecked.Value;
            radUniqueOpo.IsEnabled = radRandom.IsChecked.Value;
            radUniqueOpoWoman.IsEnabled = radRandom.IsChecked.Value;
            radFull.IsEnabled = radRandom.IsChecked.Value;
            radMix.IsEnabled = radRandom.IsChecked.Value;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         
        }

        private void chkGen_Loaded(object sender, RoutedEventArgs e)
        {
            chkGen.IsChecked = Model.GenerateCourts;
        }
        private void cmbLanguage_Loaded(object sender, RoutedEventArgs e)
        {            
                cmbLanguage.Items.Clear();
                cmbLanguage.Items.Add(new LanguageItem(Tournament.Language.Czech, "Czech"));
                cmbLanguage.Items.Add(new LanguageItem(Tournament.Language.English, "English"));
                if (Model.Language == Tournament.Language.Czech)
                {
                    cmbLanguage.SelectedIndex = 0;
                }
                else
                {
                    cmbLanguage.SelectedIndex = 1;
                }
           
        }
        public static void ChangeLanguage(Tournament.Language Language)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            ResourceDictionary dict = new ResourceDictionary();
            if (Language == Tournament.Language.Czech)
            {
                dict.Source = new Uri("Resource\\ResourceCZ.xaml", UriKind.Relative);
            }
            else
            {
                dict.Source = new Uri("Resource\\Resource.xaml", UriKind.Relative);
            }
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
        private void cmbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var languageItem = ((LanguageItem)cmbLanguage.SelectedItem);
            if (languageItem != null && !noChange)
            {
                ChangeLanguage(languageItem.Language);
                this.UpdateLayout();
                Model.Language = languageItem.Language;
                noChange = true;
                cmbLanguage_Loaded(null, null);
                noChange = false;
            }
            
        }

        private void txtAppName_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Model.AppName))
            {
                txtAppName.Text = Model.AppName;
            }
        }
    }
}
