using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeaselApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        public void SetIP(object sender, EventArgs e)
        {
            
        }

        public async void SetCredentials(object sender, EventArgs e)
        {
            PublicVariables._IP = IPAddress.Parse(IP_TextBox.Text);
            PublicVariables._CurrentUsername = Username_TextBox.Text;
            PublicVariables._UserHash = ConvertStringToSHA256String(Username_TextBox.Text + Password_TextBox.Text);

            //Move to another main interface
            await Navigation.PushAsync(new FlyoutPageMain());
        }

        public string ConvertStringToSHA256String(string value)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
    }
}