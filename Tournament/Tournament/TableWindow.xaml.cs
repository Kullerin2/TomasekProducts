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
using System.Windows.Shapes;

namespace Tournament
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        DataModel Model;
        private DataTable table;
        private List<List<string>> items;
        private const int pptIndex = 25;
        private int ppp = 25;
        private int pageIndex = 0;
        private Style headerStyle;
        
        public TableWindow(DataModel model)
        {
            Model = model;
            items = new List<List<string>>();
            InitializeComponent();
            headerStyle = new Style();
            headerStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.BlueViolet));

            Maximize();
        }
        private void Maximize()
        {

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowState = WindowState.Maximized;
            //window.Show();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnScreen_Click(object sender, RoutedEventArgs e)
        {
            string file = "Table_" + Model.Count + ".png";
            string fileName = Helper.FileFormat(Model, file);

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)gridResult.ActualWidth, (int)gridResult.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(gridResult);
            var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
            enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmp));

            using (var stm = System.IO.File.Create(fileName))
            {
                enc.Save(stm);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(gridResult, "My First Print Job");
            }

        }

        private void gridResult_Loaded(object sender, RoutedEventArgs e)
        {
            NewDataTable();
        }
        private void NewDataTable()
        {
            EnableButtons();
            table = new DataTable();
            gridResult.Columns.Clear();
            table.Columns.Add(Application.Current.FindResource("Name").ToString());
            for (int i = 0; i < Model.RoundsForCourt.Count; i++)
            {
                table.Columns.Add("" + (i + 1));
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
                gridResult.Columns.Add(col);
            }
            DataGridTemplateColumn col1 = (DataGridTemplateColumn)this.FindResource("dgt");
            gridResult.Columns.Add(col1);
            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = DateTime.Now.ToShortDateString();
            gridResult.Columns.Add(col2);

            gridResult.ItemsSource = table.DefaultView;
            for (int i = 0; i < Model.Count && i<ppp; i++)
            {
                var row = table.NewRow();
                table.Rows.Add(row);
            }
            Model.SortPlayers();
            gridResult.ColumnHeaderStyle = headerStyle;
            int index = 0;
            int rowIndex = 0;
            foreach (var player in Model.Players.OrderBy(p=>p.No ))
            {
                if (index >= pageIndex * ppp && index < ((pageIndex+1) * ppp)  && index <Model.Players.Count)
                {
                    DataRow row = table.Rows[rowIndex++];
                    player.PopulateRow(row, Model);
                }
                index++;
            }
            lblPage.Content = string.Format(Application.Current.FindResource("Page").ToString()+" {0}/{1}", pageIndex+1, ((Model.Count-1) / ppp)+1);

        }
        private void EnableButtons()
        {
            ppp = pptIndex;
            if (chkPlayers.IsChecked.Value)
            {
                ppp = Model.Players.Count;
            }
            btnPrev.IsEnabled = pageIndex > 0;
            btnNext.IsEnabled = (pageIndex+1)*ppp < Model.Players.Count;
        }
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            pageIndex--;
            NewDataTable();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            pageIndex++;
            NewDataTable();
        }

        private void chkPlayers_Checked(object sender, RoutedEventArgs e)
        {
            NewDataTable();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            NewDataTable();
        }
    }
}
