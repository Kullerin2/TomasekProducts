using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Tournament
{
    [Serializable]
    public class PlayerInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public bool Woman { get; set; }

        public byte[] ImageBytes;

        public override string ToString()
        {
            return LastName + " " + FirstName;
        }

        public byte[] ImageToByte(BitmapImage imageSource)
        {
            Stream stream = imageSource.StreamSource;
            byte[] buffer = null;
            if (stream != null && stream.Length > 0)
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    buffer = br.ReadBytes((Int32)stream.Length);
                }
            }

            return buffer;
        }
        public BitmapImage ByteToImage()
        {
            BitmapImage myBitmapImage = new BitmapImage();
            if (ImageBytes != null)
            {
                MemoryStream strmImg = new MemoryStream(ImageBytes);                
                myBitmapImage.BeginInit();
                myBitmapImage.StreamSource = strmImg;
                myBitmapImage.DecodePixelWidth = Helper.PictureSize;
                myBitmapImage.DecodePixelHeight = Helper.PictureSize;
                myBitmapImage.EndInit();
            }
            return myBitmapImage;
        }

    }
}
