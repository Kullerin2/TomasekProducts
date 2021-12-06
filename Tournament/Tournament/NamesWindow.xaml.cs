using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Tournament
{
    /// <summary>
    /// Interaction logic for NamesWindow.xaml
    /// </summary>
    public partial class NamesWindow : Window
    {
        public DataModel Model;
        List<ComboBox> boxes = new List<ComboBox>();
        private bool fired = false;
        public NamesWindow(DataModel model)
        {
            Model = model;
            InitializeComponent();
        }
       
        private void btnPlayer_Click(object sender, RoutedEventArgs e)
        {
            EditPlayer editPlayer = new EditPlayer(Model);
            editPlayer.ShowDialog();
            foreach (var box in boxes)
            {
                InitComboBox(box);
            }
        }
        private void btnAddSingle_Click(object sender, RoutedEventArgs e)
        {
            //Model.Game = GameType.Single;
            var player = new Player();
            player.Init(Model.Count+1, Model.Count, Model.SetCount);
            Model.Players.Add(player);
            Model.Count++;
            Load();
        }

        private void btnAddDouble_Click(object sender, RoutedEventArgs e)
        {
            //Model.Game = GameType.Teams;
            var player = new Player();
            player.Init(Model.Count+1, Model.Count, Model.SetCount);
            Model.Players.Add(player);
            Model.Count++;
            player = new Player();
            player.Init(Model.Count+1, Model.Count, Model.SetCount);
            Model.Players.Add(player);
            Model.Count++;
            Load();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Model.Players.Clear();
            if (Model.RoundsForCourt != null)
            {                
                Model.GameLoaded = false;
                Model.RoundIndex = -1;             
                Model.RoundsForCourt = new Rounds();
                Model.RoundsForCourt.Model = Model;                
            }
            Model.Count = 0;
            Load();

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveResult();
            this.Close();
        }
        private void AddButtonGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadButtons2();
        }
        private void ButtonGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadButtons();
        }
        private void PlayerGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPlayers();
        }
        private void Load()
        {
            LoadPlayers();
            LoadButtons();
            LoadButtons2();
        }
        private void LoadButtons2()
        {
            bool enabled = Model.IsRegistred || Model.Count < 4;
            AddButtonGrid.Children.Clear();
            if (enabled)
            {
                var btnAddSingle = new Button();
                btnAddSingle.SetValue(Grid.RowProperty, 0);
                btnAddSingle.SetValue(Grid.ColumnProperty, 0);
                btnAddSingle.Background = Brushes.Yellow;
                btnAddSingle.Content = Application.Current.FindResource("btn.AddSingle").ToString();
                btnAddSingle.Click += btnAddSingle_Click;
                AddButtonGrid.Children.Add(btnAddSingle);
            }
            if (enabled)
            {
                var btnAddDouble = new Button();
                btnAddDouble.SetValue(Grid.RowProperty, 0);
                btnAddDouble.SetValue(Grid.ColumnProperty, 2);
                btnAddDouble.Background = Brushes.Yellow;
                btnAddDouble.Content = Application.Current.FindResource("btn.AddDouble").ToString();
                btnAddDouble.Click += btnAddDouble_Click;
                AddButtonGrid.Children.Add(btnAddDouble);
            }
        }
        private void LoadButtons()
        {
            bool enabled = Model.IsRegistred || Model.Count < 4;
            ButtonGrid.Children.Clear();
            
            //var rowDefinition = new RowDefinition();
            //rowDefinition.Height = GridLength.Auto;
            //ButtonGrid.RowDefinitions.Add(rowDefinition);
            var btnPlayer = new Button();
            btnPlayer.SetValue(Grid.RowProperty, 0);
            btnPlayer.SetValue(Grid.ColumnProperty, 0);
            btnPlayer.Background = Brushes.Yellow;
            btnPlayer.Content = Application.Current.FindResource("btn.Player").ToString();
            btnPlayer.Click += btnPlayer_Click;
            ButtonGrid.Children.Add(btnPlayer);
            
            var btnDelete = new Button();
            btnDelete.SetValue(Grid.RowProperty, 0);
            btnDelete.SetValue(Grid.ColumnProperty, 6);
            btnDelete.Background = Brushes.Yellow;
            btnDelete.Content = Application.Current.FindResource("btn.Delete").ToString();
            btnDelete.Click += btnDelete_Click;
            ButtonGrid.Children.Add(btnDelete);

            var btnCancel = new Button();
            btnCancel.SetValue(Grid.RowProperty, 0);
            btnCancel.SetValue(Grid.ColumnProperty, 8);
            btnCancel.Background = Brushes.Yellow;
            btnCancel.Content = Application.Current.FindResource("btn.Cancel").ToString();
            btnCancel.Click += btnCancel_Click;
            btnCancel.IsCancel = true;
            ButtonGrid.Children.Add(btnCancel);
            var btnSave = new Button();
            btnSave.SetValue(Grid.RowProperty, 0);
            btnSave.SetValue(Grid.ColumnProperty, 10);
            btnSave.Background = Brushes.Yellow;
            btnSave.Content = Application.Current.FindResource("btn.Save").ToString();
            btnSave.Click += btnSave_Click;
            btnSave.IsDefault = true;
            ButtonGrid.Children.Add(btnSave);
        }
        private void LoadPlayers()
        {
            PlayerGrid.Children.Clear();
            Label lblRoundName = new Label();            
            int rowIndex = 0;
            int colIndex = 0;
            RowDefinition rowDefinition;// = new RowDefinition();
            //rowDefinition.Height = GridLength.Auto;
            //PlayerGrid.RowDefinitions.Add(rowDefinition);
            //PlayerGrid.RowDefinitions.Clear();
            for (int i = 0; i < Model.Players.Count; i ++)
            {
                rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                PlayerGrid.RowDefinitions.Add(rowDefinition);
                var p1 = Model.Players[i];
                Label l1 = new Label();
                l1.Content = string.Format(Application.Current.FindResource("Player").ToString() +" {0}",i+1);
                l1.SetValue(Grid.RowProperty, rowIndex);
                l1.SetValue(Grid.ColumnProperty, colIndex++);
                l1.HorizontalAlignment = HorizontalAlignment.Right;
                ComboBox cmb = new ComboBox();
                cmb.Tag = p1;
                cmb.SetValue(Grid.RowProperty, rowIndex);
                cmb.SetValue(Grid.ColumnProperty, colIndex++);
                InitComboBox(cmb);
                cmb.SelectionChanged += ComboBoxChanged;
                if (p1.Info != null)
                {
                    cmb.SelectedValue = p1.Info;
                }
                Button btn = new Button();
                btn.Content = "X";
                btn.SetValue(Grid.RowProperty, rowIndex);
                btn.SetValue(Grid.ColumnProperty, colIndex++);
                btn.Background = Brushes.Yellow;
                btn.Tag = cmb;
                btn.Click += BtnDeleteClick;

                /*TextBox txt1 = new TextBox();
                txt1.Text =p1.Name;
                txt1.Tag = p1;
                txt1.SetValue(Grid.RowProperty, rowIndex);
                txt1.SetValue(Grid.ColumnProperty, colIndex++);*/

                PlayerGrid.Children.Add(l1);
                PlayerGrid.Children.Add(cmb);
                PlayerGrid.Children.Add(btn);
                boxes.Add(cmb);
                if (colIndex == 12)
                {
                    rowIndex++;
                    colIndex = 0;
                }
            }
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Height = 165 + rowIndex * 25;
            Application.Current.MainWindow.MinHeight = 165 + rowIndex * 25;
        }
        private void InitComboBox(ComboBox cmb)
        {
            Model.Helper.LoadAvailablePlayers(Model);
            object selectedPlayer = null;
            if (cmb.SelectedValue != null)
            {
                selectedPlayer = cmb.SelectedValue;
            }
            cmb.Items.Clear();
            foreach (var player in Model.Helper.AvailablePlayers.OrderBy(x=>x.LastName))
            {
                cmb.Items.Add(player);
            }
            
            if (selectedPlayer != null)
            {
                //cmb.Items.Add(selectedPlayer);
                cmb.SelectedValue = selectedPlayer;
            }
        }
        private void BtnDeleteClick(object sender, EventArgs args)
        {
            Button btn = sender as Button;
            ComboBox cmb = btn.Tag as ComboBox;
            Player player = cmb.Tag as Player;
            Model.Players.Remove(player);
            Model.Count--;
            Load();

        }
        private void ComboBoxChanged(object sender, EventArgs args)
        {
            if (!fired)
            {
                fired = true;                
                ComboBox cmb = sender as ComboBox;
                if (cmb.SelectedValue != null)
                {                    
                    foreach (var box in boxes)
                    {
                        if (box!=cmb &&box.SelectedValue == cmb.SelectedValue)
                        {
                            box.SelectedValue = null;                            
                        }
                    }
                }                
                fired = false;
            }

        }
        public void SaveResult()
        {
            foreach (var box in boxes)
            {
                Player player = (Player)box.Tag;
                if (box.SelectedValue != null)
                {
                    player.Info = box.SelectedValue as PlayerInfo;
                    List<Player> allsits = new List<Player>();
                    if (Model.RoundsForCourt != null)
                    {
                        foreach (var round in Model.RoundsForCourt.AllRounds)
                        {
                            Player sit = round.Sits.FirstOrDefault(x => x.Number == player.Number);
                            if (sit != null)
                            {
                                allsits.Add(sit);
                            }
                        }
                        foreach (Player sit in allsits)
                        {
                            sit.Info = player.Info;
                        }
                    }
                }
            }            
        }
    }
}
