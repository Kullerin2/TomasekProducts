using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private DataModel Model;
        public Register(DataModel model)
        {
            this.Model = model;
            InitializeComponent();
            lblRegisterKeyValue.Text = model.RegisterToken;
            
            if (Model.IsRegistred)
            {
                lblMessage.Content = ValidKey();
            }
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(lblRegisterKeyValue.Text);
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {            
             if (!Model.CheckRegisterKey(txtKey.Text))
            {
                lblMessage.Content = Application.Current.FindResource("InvalidKey").ToString();
            }
            else
            {
                lblMessage.Content = ValidKey();
            }
        }
        private string ValidKey()
        {
            return string.Format(Application.Current.FindResource("ValidKey").ToString(), Model.RegistrDate.ToShortDateString());
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
