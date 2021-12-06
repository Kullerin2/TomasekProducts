using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Tournament
{
    /// <summary>
    /// Interaction logic for EditPlayer.xaml
    /// </summary>
    public partial class EditPlayer : Window
    {
        private DataModel Model;
        private PlayerInfo selectedPlayer;
        private bool isNew = true;
        private byte[] actualPlayerImage;
        //private int selectedIndex = 0;
        public EditPlayer(DataModel model)
        {
            this.Model = model;
            InitializeComponent();
            if (Model.Helper.Players.Count == 0)
            {    
                NewUser();
            }
            else
            {
                isNew = false;

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblStatus.Content = "";
                if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtSurName.Text))
                {
                    lblStatus.Content = Application.Current.FindResource("EmptyName").ToString();
                    return;
                }
                selectedPlayer.FirstName = txtFirstName.Text;
                selectedPlayer.LastName = txtSurName.Text;
                selectedPlayer.Woman = chkWoman.IsChecked.HasValue && chkWoman.IsChecked.Value;
                if (actualPlayerImage != null)
                {
                    selectedPlayer.ImageBytes = actualPlayerImage;
                }
                if (isNew)
                {                    
                    Model.Helper.Players.Add(selectedPlayer);
                }                
                Model.Helper.SavePlayers();
                isNew = false;
                EnableButtons();
                LoadUsers();
                lblStatus.Content = Application.Current.FindResource("UserSaved").ToString();
            }
            catch(Exception ex)
            {
                lblStatus.Content = Application.Current.FindResource("SaveError").ToString();
            }
            
        }
        

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                actualPlayerImage = null;
                lblStatus.Content = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All files (*.*)|*.*";
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
                BitmapImage picture = new BitmapImage();
                picture.BeginInit();
                picture.UriSource = new Uri(openFileDialog.FileName);
                var stream = openFileDialog.OpenFile();
                
                if (stream != null && stream.Length > 0)
                {
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        actualPlayerImage = br.ReadBytes((Int32)stream.Length);
                    }
                }

                picture.DecodePixelHeight = Helper.PictureSize;
                picture.DecodePixelWidth = Helper.PictureSize;
                picture.EndInit();
                imgPicture.Source = picture;
            }
            catch(Exception ex)
            {
                lblStatus.Content = Application.Current.FindResource("UploadError").ToString();
            }
        }

        private void cmbPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            if (!isNew)
            {
                cmbPlayer.Items.Clear();
                foreach (PlayerInfo player in Model.Helper.Players.OrderBy(x=>x.LastName))
                {
                    cmbPlayer.Items.Add(player);
                }
                if (selectedPlayer != null)
                {
                    cmbPlayer.SelectedValue = selectedPlayer;
                }
                else
                {
                    cmbPlayer.SelectedIndex = 0;
                }
            }
        }
        private void NewUser()
        {
            isNew = true;
            EnableButtons();
            txtFirstName.Text = string.Empty;
            txtSurName.Text = string.Empty;
            chkWoman.IsChecked = false;
            actualPlayerImage = null;
            imgPicture.Source = null;
            txtFirstName.Focus();
            selectedPlayer = new PlayerInfo();

        }

        private void cmbPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isNew && cmbPlayer.SelectedValue != null)
            {
                selectedPlayer = (PlayerInfo)cmbPlayer.SelectedValue;
                txtFirstName.Text = selectedPlayer.FirstName;
                txtSurName.Text = selectedPlayer.LastName;
                chkWoman.IsChecked = selectedPlayer.Woman;
                imgPicture.Source = selectedPlayer.ByteToImage();
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            NewUser();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (isNew)
            {
                isNew = false;
                EnableButtons();
                return;
            }
            if (selectedPlayer != null)
            {
                if (Model.Helper.Players.Contains(selectedPlayer))
                {
                    Model.Helper.Players.Remove(selectedPlayer);
                }
                Model.Helper.SavePlayers();
                cmbPlayer.SelectedIndex = 0;
                LoadUsers();
            }
        }
        private void EnableButtons()
        {
            if (isNew)
            {
                btnNew.IsEnabled = false;
                cmbPlayer.IsEnabled = false;
                btnPrev.IsEnabled = false;
                btnNext.IsEnabled = false;
            }
            else
            {
                btnNew.IsEnabled = true;
                cmbPlayer.IsEnabled = true;
                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;
            }
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPlayer.SelectedIndex == 0)
            {
                cmbPlayer.SelectedIndex = cmbPlayer.Items.Count-1;
            }
            else
            {
                cmbPlayer.SelectedIndex--;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPlayer.SelectedIndex + 1 == cmbPlayer.Items.Count)
            {
                cmbPlayer.SelectedIndex = 0;
            }
            else
            {
                cmbPlayer.SelectedIndex++;
            }
        }
    }
}
