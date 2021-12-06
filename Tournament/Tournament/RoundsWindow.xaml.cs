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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tournament
{
    /// <summary>
    /// Interaction logic for RoundsWindow.xaml
    /// </summary>
    public partial class RoundsWindow : Window
    {
        int maxRound = 22;
        DataModel Model;
        private List<List<string>> items;
        private bool showAllRound = false;
        private int pageIndex = 0;
        private int roundIndex = 0;
        private List<List<List<string>>> pages;
        public RoundsWindow(DataModel model)
        {
            Model = model;
            roundIndex = Model.RoundIndex;
            InitializeComponent();
            Maximize();

        }
        private void LoadPages()
        {
            
            pages = new List<List<List<string>>>();
            items = new List<List<string>>();
            for (int r = 0; r < Model.RoundsForCourt.Count; r++)
            {
                items.Add(AddRound(r, true));
            }
            var oneRoundText = items[0];
            int roundCount = maxRound / oneRoundText.Count;
            //if (roundCount == 0) { roundCount = 1; }
            int index = 0;
            int i = 0;
            List<string> lines;
            List<string> pageCol;
            while (index < items.Count)
            {
                if (roundCount == 0)
                {
                    var item =items[index++];
                    var page = new List<List<string>>();
                    pages.Add(page);
                    pageCol = new List<string>();
                    i = 0;
                    while (i< item.Count/2)
                    {
                        var line = item[i++];
                        pageCol.Add(line);
                    }
                    page.Add(pageCol);                    
                    pageCol = new List<string>();
                    while (i < item.Count)
                    {
                        var line = item[i++];
                        pageCol.Add(line);
                    }
                    
                    page.Add(pageCol);


                }
                else
                {
                    var page = new List<List<string>>();
                    pages.Add(page);
                    pageCol = new List<string>();
                    while (i < roundCount && index < items.Count)
                    {
                        lines = items[index++];
                        pageCol.AddRange(lines);
                        i++;
                    }
                    page.Add(pageCol);
                    pageCol = new List<string>();
                    i = 0;
                    while (i < roundCount && index < items.Count)
                    {
                        lines = items[index++];
                        pageCol.AddRange(lines);
                        i++;
                    }
                    page.Add(pageCol);
                    i = 0;
                }
            }
        }
        private void Maximize()
        {

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowState = WindowState.Maximized;
            //window.Show();
        }
        private List<string> AddRound(int roundIndex,bool allRound)
        {
            var list = new List<string>();            
            list.Add(Application.Current.FindResource("Round").ToString() + " " + (roundIndex + 1));
            var round = Model.RoundsForCourt.AllRounds[roundIndex];
            MainWindow.PrepareRound(round, list, Model);
            return list;
        }
        private void lstRound_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPages();
            ShowRounds();
        }
        private void ShowRounds()
        {
            ShowRounds(lstRound1, lstRound2);
        }
        private void ShowRounds(ListBox box1, ListBox box2)
        {
            items = new List<List<string>>();
            box1.Items.Clear();
            box2.Items.Clear();
            ListBox box;
            if (showAllRound)
            {
                lblPage.Content = string.Format(Application.Current.FindResource("Page").ToString() + " {0}/{1}",pageIndex+1,pages.Count);
                var page = pages[pageIndex];
                for(int i = 0; i < 2; i++)
                {
                    if (page.Count > i)
                    {
                        var lines = page[i];
                        if (i == 0)
                        {
                            box = box1;
                        }else
                        {
                            box = box2;
                        }                       
                        foreach (string line in lines)
                        {
                            box.Items.Add(line);
                        }
                    }
                }
            }
            else
            {
                lblPage.Content = string.Format(Application.Current.FindResource("Round").ToString() + " {0}/{1}", roundIndex + 1, Model.RoundsForCourt.Count);
                ShowOneRound(box1,box2);
            }
            EnableButtons();
        }
        private void ShowOneRound(ListBox box1,ListBox box2)
        {
            List<string> lines =AddRound(roundIndex,false);
            int i = 0;
            while (i< lines.Count && i<maxRound)
            {
                var line = lines[i++];
                box1.Items.Add(line);
            }
            while (i < lines.Count)
            {
                var line = lines[i++];
                box2.Items.Add(line);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ScreenOneRound()
        {
            int index = showAllRound ? pageIndex + 1 : roundIndex + 1;
            ShowRounds();
            gridMain.UpdateLayout();
            string file = "Rounds" + index + ".png";
            string fileName = Helper.FileFormat(Model, file);

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)gridMain.ActualWidth, (int)gridMain.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(gridMain);
            var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
            enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmp));

            using (var stm = System.IO.File.Create(fileName))
            {
                enc.Save(stm);
            }
        }
        private void btnScreen_Click(object sender, RoutedEventArgs e)
        {
            int tmpPageIndex = pageIndex;
            if (showAllRound)
            {
                for (int i = 0; i < pages.Count; i++)
                {
                    pageIndex = i;
                    ScreenOneRound();
                }
            }
            else
            {
                ScreenOneRound();
            }

            pageIndex = tmpPageIndex;
            ShowRounds();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                int tmpPageIndex = pageIndex;

                FixedDocument document = CreateFixedDocument();
                printDialog.PrintDocument(document.DocumentPaginator, "Rounds Printing");          
                

                pageIndex = tmpPageIndex;
                ShowRounds();
            }

        }
        private void CreateLayout(ref Grid grid, ref ListBox box1,ref ListBox box2)
        {
            grid = new Grid();
            box1 = new ListBox();
            box2 = new ListBox();
            foreach (var colDef in gridMain.ColumnDefinitions)
            {
                ColumnDefinition newCol = new ColumnDefinition();
                newCol.Width = colDef.Width;
                grid.ColumnDefinitions.Add(newCol);
            }
            grid.Children.Add(box1);
            grid.Children.Add(box2);
            box1.Margin = lstRound1.Margin;
            box2.Margin = lstRound2.Margin;
            box1.VerticalAlignment = lstRound1.VerticalAlignment;
            box2.VerticalAlignment = lstRound2.VerticalAlignment;
            box1.HorizontalAlignment = lstRound1.HorizontalAlignment;
            box2.HorizontalAlignment = lstRound2.HorizontalAlignment;
            box1.FontSize = lstRound1.FontSize;
            box2.FontSize = lstRound2.FontSize;
            box1.SetValue(Grid.ColumnProperty, 0);
            box2.SetValue(Grid.ColumnProperty, 1);
        }
        private FixedDocument CreateFixedDocument()
        {
            FixedDocument fixedDocument = new FixedDocument();
            if (showAllRound)
            {
                for (int i = 0; i < pages.Count; i++)
                {
                    pageIndex = i;
                    PrintOnePage(fixedDocument);
                }
                
            }
            else
            {
                PrintOnePage(fixedDocument);
            }
            return fixedDocument;
        }
        private void PrintOnePage2(FixedDocument fixedDocument)
        {
            Grid grid = null;
            ListBox box1 = null;
            ListBox box2 = null;
            CreateLayout(ref grid, ref box1, ref box2);
            ShowRounds(box1, box2);
            grid.UpdateLayout();

            PageContent page = new PageContent();
            FixedPage fixedPage = new FixedPage();
            fixedPage.Children.Add(grid);
            ((IAddChild)page).AddChild(fixedPage);
            fixedDocument.Pages.Add(page);
        }
        private void PrintOnePage(FixedDocument fixedDocument)
        {
            ShowRounds();
            gridMain.UpdateLayout();
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)gridMain.ActualWidth, (int)gridMain.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(gridMain);
            BitmapSource bitmap = bmp;
            Image image = new Image();
            image.Height = gridMain.ActualHeight;
            image.Width = gridMain.ActualWidth;
            image.Source = bitmap;
            PageContent page = new PageContent();
            FixedPage fixedPage = new FixedPage();
            fixedPage.Height = gridMain.ActualHeight;
            fixedPage.Width = gridMain.ActualWidth;
            fixedPage.Children.Add(image);
            ((IAddChild)page).AddChild(fixedPage);
            fixedDocument.Pages.Add(page);
        }

        private void EnableButtons()
        {
            showAllRound = chkAllRound.IsChecked.Value;
            if (showAllRound) {
                btnNext.IsEnabled = pageIndex < pages.Count-1;
                btnPrev.IsEnabled = pageIndex>0;
            }
            else
            {
                btnNext.IsEnabled = roundIndex < Model.RoundsForCourt.Count-1;
                btnPrev.IsEnabled = roundIndex > 0;
            }

        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            showAllRound = chkAllRound.IsChecked.Value;
            ShowRounds();
        }


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (showAllRound)
            {
                pageIndex++;
            }
            else
            {
                roundIndex++;
            }
            ShowRounds();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (showAllRound)
            {
                pageIndex--;
            }
            else
            {
                roundIndex--;
            }
            ShowRounds();
        }

        private void chkAllRound_Click(object sender, RoutedEventArgs e)
        {
            showAllRound = chkAllRound.IsChecked.Value;
            ShowRounds();
        }
    }
}
