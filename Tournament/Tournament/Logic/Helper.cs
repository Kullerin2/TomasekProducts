using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Tournament
{
    public class Helper
    {
        [XmlIgnore]
        public Image Gold, Silver, Bronze, Cup;
        [XmlIgnore]
        public BitmapImage GoldImage, SilverImage, BronzeImage;
        [XmlIgnore]
        public static int ImageSize = 30;
        [XmlIgnore]
        public static int PictureSize = 143;
        [XmlIgnore]
        public List<PlayerInfo> Players;
        [XmlIgnore]
        public List<PlayerInfo> AvailablePlayers;
        [XmlIgnore]
        public List<PlayerInfo> NotUsedPlayers;

        public void Init()
        {
            InitPlayers();
            InitMedals();            
        }
        public void LoadAvailablePlayers(DataModel model)
        {
            NotUsedPlayers = new List<PlayerInfo>();
            AvailablePlayers = new List<PlayerInfo>();
            foreach (var player in Players)
            {
                AvailablePlayers.Add(player);
                NotUsedPlayers.Add(player);
            }
            int no = 1;
            while (AvailablePlayers.Count < model.Count)
            {
                PlayerInfo player = new PlayerInfo();
                player.LastName = Application.Current.FindResource("Player").ToString() + no++;
                AvailablePlayers.Add(player);
                NotUsedPlayers.Add(player);
            }
            for (int i = 0; i < model.Count; i++)
            {
                if (model.Players[i].Info != null)
                {
                    var player = model.Players[i];
                    var info = AvailablePlayers.FirstOrDefault(x => x.LastName == player.Info.LastName && x.FirstName == player.Info.FirstName);
                    if (info != null)
                    {
                        player.Info = info;
                        if (NotUsedPlayers.Contains(info))
                        {
                            NotUsedPlayers.Remove(info);
                        }
                    }
                }

            }
            int j = 0;
            for (int i = 0; i < model.Count; i++)
            {
                if (model.Players[i].Info == null)
                {
                    model.Players[i].Info = NotUsedPlayers[j++];
                }
            }
        }
        private void InitPlayers()
        {
            string filePath = string.Format("{0}{1}.xml", AppDomain.CurrentDomain.BaseDirectory, "Players");
            if (File.Exists(filePath))
            {
                var serializer = new XmlSerializer(typeof(List<PlayerInfo>));
                var reader = new StreamReader(filePath);
                Players = (List<PlayerInfo>)serializer.Deserialize(reader);
            }
            else
            {
                Players = new List<PlayerInfo>();
            }
        }
        public void SavePlayers()
        {
            TextWriter writer = null;
            try
            {

                string filePath = string.Format("{0}{1}.xml", AppDomain.CurrentDomain.BaseDirectory, "Players");
                var serializer = new XmlSerializer(typeof(List<PlayerInfo>));
                writer = new StreamWriter(filePath, false);
                serializer.Serialize(writer, Players);
            }
            finally
            {
                if (writer != null)
                    writer.Close();

            }
        }
        private void InitMedals()
        {
            Gold = new Image();
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/Tournament;component/Resource/Gold.png");
            image.DecodePixelHeight = ImageSize;
            image.DecodePixelWidth = ImageSize;
            image.EndInit();
            GoldImage = image;
            Gold.Source = image;
            Silver = new Image();
            image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/Tournament;component/Resource/Silver.png");
            image.DecodePixelHeight = ImageSize;
            image.DecodePixelWidth = ImageSize;
            image.EndInit();
            Silver.Source = image;
            SilverImage = image;
            Bronze = new Image();
            image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/Tournament;component/Resource/Bronze.png");
            image.DecodePixelHeight = ImageSize;
            image.DecodePixelWidth = ImageSize;
            image.EndInit();
            BronzeImage = image;
            Bronze.Source = image;

            Cup = new Image();
            image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/Tournament;component/Resource/Cup.jpg");
            image.DecodePixelHeight = ImageSize;
            image.DecodePixelWidth = ImageSize;
            image.EndInit();
            Cup.Source = image;

        }

        public Image Line
        {
            get
            {
                Image picture = new Image();
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri("pack://application:,,,/Tournament;component/Resource/Line.png");
                image.DecodePixelHeight = ImageSize;
                image.DecodePixelWidth = 1400;
                image.EndInit();
                picture.Source = image;
                return picture;
            }
        }
        public static string FileFormat(DataModel model, string fileName)
        {
            string tournamentName = string.IsNullOrEmpty(model.AppName) ? "Tournament" : model.AppName;
            return string.Format("{0}{1}_{2}_{3}", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyy_MM_dd"), tournamentName, fileName);
        }
    }

}
