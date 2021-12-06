using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tournament
{
    public class ListBoxItem
    {
        public string Text { get; set; }
        public Image Image { get; set; }
        public BitmapImage Picture1 { get; set; }
        public BitmapImage Picture2 { get; set; }
        public int Size { get; set; }
        public Image Footer { get; set; }
        public Thickness Margin { get; set; }
        public Brush Background { get; set; }
        public ListBoxItem()
        {

        }
        public ListBoxItem(string text,Image image,BitmapImage picture1,BitmapImage picture2):this(text,image,null,picture1,picture2,-1)
        {
        
        }
        public ListBoxItem(string text,Image image,Image footer,BitmapImage picture1,BitmapImage picture2,int index)
        {
            Margin = new Thickness(0,0,0,0);
            if (footer != null)
            {
                Margin = new Thickness(200, 0, 0, 0);
            }
            Footer = footer;
            Image = image;
            Text = text;
            Size = 0;
            if (picture1 != null)
            {
                Size = 60;
                Picture1 = picture1;
            }            
            if (picture2 != null)
            {
                Size = 60;
                Picture2 = picture2;
            }
            if (index % 2 == 0)
            {
                //Background = Brushes.Bisque;
            }
        }
    }
}
