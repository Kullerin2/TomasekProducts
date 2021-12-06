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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tournament;

namespace RegisterTournament
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            string text = txtToken.Text;
            if (string.IsNullOrEmpty(text))
            {
                lblRegisterKey.Text = "Not valid key";
            }
            else
            {
                string token =CryptoHelper.DecryptString(text);
                string key = string.Empty;
                switch(cmbPeriod.SelectedItem.ToString())
                {
                    case "Temporary":
                        {
                            token = "Temporary";
                            key = string.Format("{0},{1}", token, DateTime.Now.AddDays(30).ToShortDateString());
                            break;
                        }
                    case "Week":
                        {
                            key = string.Format("{0},{1}", token, DateTime.Now.AddDays(7).ToShortDateString());
                            break;
                        }
                    case "Month":
                        {
                            key = string.Format("{0},{1}", token, DateTime.Now.AddMonths(1).ToShortDateString());
                            break;
                        }
                    case "Year":
                        {
                            key = string.Format("{0},{1}", token, DateTime.Now.AddYears(1).ToShortDateString());
                            break;
                        }


                }
                string cryptedKey =CryptoHelper.EncryptString(key);
                lblRegisterKey.Text = cryptedKey;

            }
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(lblRegisterKey.Text);
        }

        private void cmbPeriod_Loaded(object sender, RoutedEventArgs e)
        {
            cmbPeriod.Items.Clear();
            cmbPeriod.Items.Add("Temporary");
            cmbPeriod.Items.Add("Week");
            cmbPeriod.Items.Add("Month");
            cmbPeriod.Items.Add("Year");
            cmbPeriod.SelectedItem = "Week";
        }
    }
}
