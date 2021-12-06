using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.Xml;

namespace Tournament
{
    /// <summary>
    /// Interaction logic for ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        private DataModel Model;
        private int pageIndex = 0;
        private int maxLine = 16;
        private List<List<ListBoxItem>> pages;
        private List<ListBoxItem> page;
        private List<ListBoxItem> header;
        private int LineIndex = 0;
        
        public ListWindow(DataModel model)
        {
            Model = model;
            InitHeader();
            InitializeComponent();            
            Maximize();
        }
        private void InitHeader()
        {
            header = new List<ListBoxItem>();
            //header.Add(new ListBoxItem("", null, null, null));
            header.Add(new ListBoxItem("", Model.Helper.Cup, null, null));
            header.Add(new ListBoxItem(Application.Current.FindResource("TournamentResult").ToString(), null, null, null));
            header.Add(new ListBoxItem(string.Format("{0} - {1}", Model.AppName, Model.TournamentDate.ToShortDateString()), null, null, null));
            //header.Add(new ListBoxItem("", null, null, null));
        }
        private void RenderHeader(ListBox box)
        {
            foreach (var item in header)
            {
                box.Items.Add(item);
            }
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
        private void ShowResults()
        {
            ShowResults(lstRound1);
        }
        private void ShowResults(ListBox box)
        {
            box.Items.Clear();
            RenderHeader(box);
            lblPage.Content = string.Format(Application.Current.FindResource("Page").ToString() + " {0}/{1}", pageIndex + 1, pages.Count);
            var page = pages[pageIndex];            
            foreach (var item in page)
            {
                box.Items.Add(item);                
            }            

            EnableButtons();
        }
        
        private Image GetPlayerImage(int no)
        {            
            Image image = null;
            if (no==1)
            {
                image = Model.Helper.Gold;
            }
            else if (no ==2)
            {
                image = Model.Helper.Silver;
            }
            else if (no==3)
            {
                image = Model.Helper.Bronze;
            }
            return image;
        }
        private ListBoxItem CreatePlayerResult(int no,string line,bool addFooter,Player p1, Player p2,int index)
        {
            var picture1 = new BitmapImage();            
            var picture2 = new BitmapImage();
            
            Image image = GetPlayerImage(no);
            if (p1 != null)
            {
                picture1 = p1.Info.ByteToImage();
            }
            if (p2 != null)
            {
                picture2 = p2.Info.ByteToImage();
            }
            
            if (addFooter)
            {
                line += "\n";
                return new ListBoxItem(line, image,Model.Helper.Line, picture1, picture2,index);
            }
            else
            {
                return new ListBoxItem(line, image,null, picture1, picture2,index);
            }
        }
        
        private void LoadPages()
        {
            pages = new List<List<ListBoxItem>>();
            page = new List<ListBoxItem>();
            Model.SortPlayers();
            string line;
            string fullLine= string.Empty;
            Player evenPlayer = null;
            int playerIndex = 0;
            foreach (var player in Model.Players.OrderBy(p => p.No))
            {
                playerIndex++;
                if (Model.Game == GameType.Teams)
                {
                    if (evenPlayer == null)
                    {
                        evenPlayer = player;
                        continue;
                    }
                    line =string.Format("\t{0}.  {4} - {1}                                      {2} ({3})", player.No, player.Info.LastName.ToUpper(), player.Sets, player.Points,evenPlayer.Info.LastName.ToUpper());                    
                }
                else
                {
                   line =string.Format("\t{0}.  {1}                                      {2} ({3})", player.No, player.Info.LastName.ToUpper(), player.Sets, player.Points);
                }
                fullLine = line + "\n";                
                int matchIndex = 1;
                foreach (var match in player.Score.Matches)
                {
                    if (Model.Game == GameType.Single)
                    {
                        if (player.Score.IsFirtsPlayer(match))
                        {
                            line = string.Format("{0}. {1} ({2}:{3})", matchIndex, match.Player2.Info.LastName, match.Result.PointWin, match.Result.PointLoose);
                        }
                        else
                        {
                            line = string.Format("{0}. {1} ({2}:{3})", matchIndex, match.Player1.Info.LastName, match.Result.PointLoose, match.Result.PointWin);

                        }
                    }
                    else if (player.Score.IsFirtsPlayer(match))
                    {                        
                        line =string.Format("{0}. {1}-{2} ({5}:{6})", matchIndex,match.Player3.Info.LastName, match.Player4.Info.LastName, match.Result.SetWin,match.Result.SetLoose,match.Result.PointWin,match.Result.PointLoose);                          
                    }
                    else
                    {
                        line =string.Format("{0}. {1}-{2} ({5}:{6})", matchIndex, match.Player1.Info.LastName, match.Player2.Info.LastName, match.Result.SetLoose, match.Result.SetWin, match.Result.PointLoose, match.Result.PointWin);
                    }                    
                    if (matchIndex % 7 == 0)
                    {
                        LineIndex++;
                        fullLine += "\n   " + line;
                    }
                    else
                    {
                        fullLine += "   " + line;
                    }
                    
                    matchIndex++;
                }                
                //page.Add(CreatePlayerResult(fullLine,evenPlayer.Info, player.Info));
                
                if (LineIndex> maxLine)
                {
                    LineIndex = 0;
                    page.Add(CreatePlayerResult(player.No,fullLine,true, evenPlayer, player,playerIndex));
                    pages.Add(page);
                    
                    page = new List<ListBoxItem>();
                    
                }
                else
                {
                    LineIndex += 3;
                    page.Add(CreatePlayerResult(player.No,fullLine, true, evenPlayer, player,playerIndex));
                }
                fullLine = string.Empty;
                evenPlayer = null;
            }  
            if (LineIndex > 0)
            {
                pages.Add(page);
            }
        }
        
        private void lstRound_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPages();
            ShowResults();
        }
        private void btnScreen_Click(object sender, RoutedEventArgs e)
        {
            int tmpPageIndex = pageIndex;
            for (int i = 0; i < pages.Count; i++)
            {
                pageIndex = i;
                ShowResults();
                gridMain.UpdateLayout();
                string fileName = Helper.FileFormat(Model, "List_" + (pageIndex + 1) + ".png");

                RenderTargetBitmap bmp = new RenderTargetBitmap((int)gridMain.ActualWidth, (int)gridMain.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(gridMain);
                var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
                enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmp));
                using (var stm = System.IO.File.Create(fileName))
                {
                    enc.Save(stm);
                }
            }

            pageIndex = tmpPageIndex;
            ShowResults();


        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {                
                int tmpPageIndex = pageIndex;
                FixedDocument document = CreateFixedDocument();
                printDialog.PrintDocument(document.DocumentPaginator, "Result Printing");
                pageIndex = tmpPageIndex;
                ShowResults();
            }

        }
        private FixedDocument CreateFixedDocument()
        {
            FixedDocument fixedDocument = new FixedDocument();
            for (int i = 0; i < pages.Count; i++)
            {
                pageIndex = i;                
                PrintOnePage(fixedDocument);
            }

            return fixedDocument;
        }
       
        private void PrintOnePage(FixedDocument fixedDocument)
        {
            ShowResults();
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

            btnNext.IsEnabled = pageIndex < pages.Count - 1;
            btnPrev.IsEnabled = pageIndex > 0;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            pageIndex++;
            ShowResults();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            pageIndex--;
            ShowResults();
        }
    }
    public static class ExtensionMethods
    {
        /*
        public static T XamlClone<T>(this T original)
            where T : class
        {
            if (original == null)
                return null;

            object clone;
            using (var stream = new MemoryStream())
            {
                XamlWriter.Save(original, stream);
                stream.Seek(0, SeekOrigin.Begin);
                clone = XamlReader.Load(stream);
            }

            if (clone is T)
                return (T)clone;
            else
                return null;
        }*/
        public static T DeepClone<T>(this T from)
        {
            using (MemoryStream s = new MemoryStream())
            {
                BinaryFormatter f = new BinaryFormatter();
                f.Serialize(s, from);
                s.Position = 0;
                object clone = f.Deserialize(s);

                return (T)clone;
            }
        }

        public static BindingBase CloneViaXamlSerialization(this BindingBase binding)
        {
            var sb = new StringBuilder();
            var writer = XmlWriter.Create(sb, new XmlWriterSettings
            {
                Indent = true,
                ConformanceLevel = ConformanceLevel.Fragment,
                OmitXmlDeclaration = true,
                NamespaceHandling = NamespaceHandling.OmitDuplicates,
            });
            var mgr = new XamlDesignerSerializationManager(writer);

            // HERE BE MAGIC!!!
            mgr.XamlWriterMode = XamlWriterMode.Expression;
            // THERE WERE MAGIC!!!

            System.Windows.Markup.XamlWriter.Save(binding, mgr);
            StringReader stringReader = new StringReader(sb.ToString());
            XmlReader xmlReader = XmlReader.Create(stringReader);
            object newBinding = (object)XamlReader.Load(xmlReader);
            if (newBinding == null)
            {
                throw new ArgumentNullException("Binding could not be cloned via Xaml Serialization Stack.");
            }

            if (newBinding is Binding)
            {
                return (Binding)newBinding;
            }
            else if (newBinding is MultiBinding)
            {
                return (MultiBinding)newBinding;
            }
            else if (newBinding is PriorityBinding)
            {
                return (PriorityBinding)newBinding;
            }
            else
            {
                throw new InvalidOperationException("Binding could not be cast.");
            }
        }
    }
}
