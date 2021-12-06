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
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public DataModel Model;
        Dictionary <Match,List<TextBox>> boxes;
        public bool AllSaved;
        public ResultWindow(DataModel model)
        {
            Model = model;
            InitializeComponent();
            boxes = new Dictionary<Match, List<TextBox>>();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveResult();
            this.DialogResult = true;
            this.Close();
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Label lblRoundName = new Label();
            lblRoundName.Content = Application.Current.FindResource("Round").ToString()+" " + (Model.RoundIndex + 1);
            int rowIndex = 0;
            int colIndex = 0;
            lblRoundName.SetValue(Grid.RowProperty, rowIndex++);
            lblRoundName.SetValue(Grid.ColumnProperty,0);
            var rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;
            
            ResultGrid.RowDefinitions.Add(rowDefinition);
            ResultGrid.Children.Add(lblRoundName);
            TextBox txtFocus = null;
            var round = Model.RoundsForCourt.LastRound;
            if (Model.Game == GameType.Single)
            {
                AddMatchesForSingle(round, ref rowIndex, colIndex,ref txtFocus);
            }
            else
            {
                AddMatchesForDebl(round,ref rowIndex, colIndex,ref txtFocus);
            }
            rowIndex++;             
            
            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Height = 130+rowIndex*25;
            Application.Current.MainWindow.MinHeight = 130 + rowIndex * 25;
            Application.Current.MainWindow.MaxHeight = 130 + rowIndex * 25;

            Keyboard.Focus(txtFocus);
            txtFocus.Focus();
            //this.MinHeight = Height;
        }
        private void AddMatchesForSingle(Round round, ref int rowIndex, int colIndex,ref TextBox txtFocus)
        {
            int i = 0;
            RowDefinition rowDefinition;
            foreach (var match in round.Matches.OrderBy(m => m.CourtIndex))
            {
                rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                ResultGrid.RowDefinitions.Add(rowDefinition);
                int pi1 = match.P1;
                int pi2 = match.P2;                

                if (pi1 >= 0 && pi2 >= 0 )
                {
                    var p1 = match.Player1;
                    var p2 = match.Player2;
                    boxes.Add(match, new List<TextBox>());
                    AddLabel(rowIndex, colIndex, string.Format("{0}  :  {1}", p1.Info.LastName, p2.Info.LastName));
                    TextBox txt12 = new TextBox(), txt13 = new TextBox(), txt22 = new TextBox(), txt23 = new TextBox();
                    TextBox txt11 = AddTextBox(p1.Score.Point(Model.RoundIndex, 1) + "", rowIndex, colIndex + 1);
                    ResultGrid.Children.Add(txt11);
                    AddLabel(rowIndex, colIndex + 2, " : ");
                    TextBox txt21 = AddTextBox(p2.Score.Point(Model.RoundIndex, 1) + "", rowIndex, colIndex + 3);
                    ResultGrid.Children.Add(txt21);
                    if (i == 0)
                    {
                        i++;
                        txtFocus = txt11;
                    }
                    if (Model.SetCount > 1)
                    {
                        txt12 = AddTextBox(p1.Score.Point(Model.RoundIndex, 2) + "", rowIndex, colIndex + 5);
                        ResultGrid.Children.Add(txt12);
                        AddLabel(rowIndex, colIndex + 6, " : ");
                        txt22 = AddTextBox(p2.Score.Point(Model.RoundIndex, 2) + "", rowIndex, colIndex + 7);
                        ResultGrid.Children.Add(txt22);
                    }
                    if (Model.SetCount > 2)
                    {
                        txt13 = AddTextBox(p1.Score.Point(Model.RoundIndex, 3) + "", rowIndex, colIndex + 9);
                        ResultGrid.Children.Add(txt13);
                        AddLabel(rowIndex, colIndex + 10, " : ");
                        txt23 = AddTextBox(p2.Score.Point(Model.RoundIndex, 3) + "", rowIndex, colIndex + 11);
                        ResultGrid.Children.Add(txt23);
                    }

                    boxes[match].Add(txt11);
                    boxes[match].Add(txt21);
                    if (Model.SetCount > 1)
                    {
                        boxes[match].Add(txt12);
                        boxes[match].Add(txt22);
                    }
                    if (Model.SetCount > 2)
                    {
                        boxes[match].Add(txt13);
                        boxes[match].Add(txt23);
                    }

                    colIndex += 13;
                    if (colIndex >= 18)
                    {
                        rowIndex++;
                        colIndex = 0;
                    }

                }
            }
        }
        private void AddMatchesForDebl(Round round,ref int rowIndex,int colIndex,ref TextBox txtFocus)
        {
            int i = 0;
            RowDefinition rowDefinition;
            foreach (var match in round.Matches.OrderBy(m =>m.CourtIndex))
            {
                rowDefinition = new RowDefinition();                
                rowDefinition.Height = GridLength.Auto;
                ResultGrid.RowDefinitions.Add(rowDefinition);
                int pi1 = match.P1;
                int pi2 = match.P2;
                int pi3 = match.P3;
                int pi4 = match.P4;

                //if ((pi1 >= 0 && pi2 >= 0 && pi3 >= 0 && pi4 >= 0) && !DataModel.SamePlayer(pi1, pi2, pi3, pi4))
                if ((pi1 >= 0 && pi2 >= 0 && pi3 >= 0 && pi4 >= 0))
                    {
                    var p1 = match.Player1;
                    var p2 = match.Player2;
                    var p3 = match.Player3;
                    var p4 = match.Player4;
                    boxes.Add(match, new List<TextBox>());
                    AddLabel(rowIndex, colIndex, string.Format("{0} + {1}  :  {2} + {3}", p1.Info.LastName, p2.Info.LastName, p3.Info.LastName, p4.Info.LastName));
                    TextBox txt12 = new TextBox(), txt13 = new TextBox(), txt22 = new TextBox(), txt23 = new TextBox();
                    TextBox txt11 = AddTextBox(p1.Score.Point(Model.RoundIndex, 1) + "", rowIndex, colIndex + 1);
                    ResultGrid.Children.Add(txt11);
                    AddLabel(rowIndex, colIndex + 2, " : ");
                    TextBox txt21 = AddTextBox(p3.Score.Point(Model.RoundIndex, 1) + "", rowIndex, colIndex + 3);
                    ResultGrid.Children.Add(txt21);
                    if (i == 0)
                    {
                        i++;
                        txtFocus = txt11;
                    }
                    if (Model.SetCount > 1)
                    {
                        txt12 = AddTextBox(p1.Score.Point(Model.RoundIndex, 2) + "", rowIndex, colIndex + 5);
                        ResultGrid.Children.Add(txt12);
                        AddLabel(rowIndex, colIndex + 6, " : ");
                        txt22 = AddTextBox(p3.Score.Point(Model.RoundIndex, 2) + "", rowIndex, colIndex + 7);
                        ResultGrid.Children.Add(txt22);
                    }
                    if (Model.SetCount > 2)
                    {
                        txt13 = AddTextBox(p1.Score.Point(Model.RoundIndex, 3) + "", rowIndex, colIndex + 9);
                        ResultGrid.Children.Add(txt13);
                        AddLabel(rowIndex, colIndex + 10, " : ");
                        txt23 = AddTextBox(p3.Score.Point(Model.RoundIndex, 3) + "", rowIndex, colIndex + 11);
                        ResultGrid.Children.Add(txt23);
                    }

                    boxes[match].Add(txt11);
                    boxes[match].Add(txt21);
                    if (Model.SetCount > 1)
                    {
                        boxes[match].Add(txt12);
                        boxes[match].Add(txt22);
                    }
                    if (Model.SetCount > 2)
                    {
                        boxes[match].Add(txt13);
                        boxes[match].Add(txt23);
                    }

                    colIndex += 13;
                    if (colIndex >= 18)
                    {
                        rowIndex++;
                        colIndex = 0;
                    }

                }
            }
        }
        private void AddLabel(int rowIndex,int colIndex,string value)
        {
            Label l = new Label();
            l.Content = value;
            l.SetValue(Grid.RowProperty, rowIndex);
            l.SetValue(Grid.ColumnProperty, colIndex);
            l.HorizontalAlignment = HorizontalAlignment.Right;
            if (rowIndex % 2 ==0)
            {
                //l.Background = Brushes.Bisque;
            }
            ResultGrid.Children.Add(l);
        }
        private TextBox AddTextBox(string value,int rowIndex,int colIndex)
        {
            TextBox result = new TextBox();
            result.Text = value;            
            result.SetValue(Grid.RowProperty, rowIndex);
            result.SetValue(Grid.ColumnProperty, colIndex);
            result.Width = 25;
            return result;
        }
        private void SaveResult()
        {
            AllSaved = true;
            foreach (var match in boxes.Keys)
            {                
                var box = boxes[match];
                int val1 = 0;
                int val2 = 0;
                int val3 = 0;
                int val4 = 0;
                int val5 = 0;
                int val6 = 0;

                Int32.TryParse(box[0].Text, out val1);
                Int32.TryParse(box[1].Text, out val2);
                if (val1==0 && val2 == 0)
                {
                    AllSaved = false;
                }
                if (box.Count > 2)
                {
                    Int32.TryParse(box[2].Text, out val3);
                    Int32.TryParse(box[3].Text, out val4);
                    if (val3 == 0 && val4 == 0)
                    {
                        AllSaved = false;
                    }
                }
                if (box.Count > 4)
                {
                    Int32.TryParse(box[4].Text, out val5);
                    Int32.TryParse(box[5].Text, out val6);
                    if (val1 == 5 && val6 == 0)
                    {
                        AllSaved = false;
                    }
                }
                match.SetResult(val1, val2, val3, val4, val5, val6);                
            }
        }

        private void ButtonGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var btnCancel = new Button();
            btnCancel.SetValue(Grid.ColumnProperty, 1);
            btnCancel.Content = Application.Current.FindResource("btn.Cancel").ToString();
            btnCancel.Click += btnCancel_Click;
            btnCancel.IsCancel = true;
            btnCancel.Background = Brushes.Yellow;
            ButtonGrid.Children.Add(btnCancel);
            var btnSave = new Button();
            btnSave.SetValue(Grid.ColumnProperty, 2);
            btnSave.Content = Application.Current.FindResource("btn.Save").ToString();
            btnSave.Click += btnSave_Click;
            btnSave.IsDefault = true;
            btnSave.Background = Brushes.Yellow;
            ButtonGrid.Children.Add(btnSave);
        }
    }
}
