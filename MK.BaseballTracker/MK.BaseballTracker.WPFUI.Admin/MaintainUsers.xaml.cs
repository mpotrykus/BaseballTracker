using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using MK.BaseballTracker.BL;
using MK.BaseballTracker.BL.Models;

namespace MK.BaseballTracker.WPFUI.Admin
{
    /// <summary>
    /// Interaction logic for MaintainUsers.xaml
    /// </summary>
    public partial class MaintainUsers : Window
    {
        BL.Models.User user;
        List<BL.Models.User> users;


        public MaintainUsers()
        {
            InitializeComponent();

            Reload();
        }

        private static HttpClient InitializeClient()
        {

            HttpClient client = new HttpClient();
            // Address of the Web API
            client.BaseAddress = new Uri("https://localhost:44376/api/");
            // Add security code

            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            return client;
        }

        private void Reload()
        {
            

            HttpClient client = InitializeClient();
            HttpResponseMessage response = client.GetAsync("User").Result;
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            users = items.ToObject<List<BL.Models.User>>();

            cboUsers.ItemsSource = null;
            cboUsers.ItemsSource = users;
            cboUsers.DisplayMemberPath = "DisplayName";
            cboUsers.SelectedValuePath = "Id";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            new MainWindow().ShowDialog();
            this.Close();


        }

        private void CboUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboUsers.SelectedIndex > -1)
            {
                user = users[cboUsers.SelectedIndex];
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtEmail.Text = user.Email;
                txtPassword.Text = user.Password;
                
            }

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient client = InitializeClient();
                User user = new User();
                user.FirstName = txtFirstName.Text;
                user.LastName = txtLastName.Text;
                user.Email = txtEmail.Text;
                user.Password = txtPassword.Text;

                string serializedUser = JsonConvert.SerializeObject(user);
                var content = new StringContent(serializedUser);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("User", content).Result;

                Reload();

                txtFirstName.Clear();
                txtLastName.Clear();
                txtEmail.Clear();
                txtPassword.Clear();

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                HttpClient client = InitializeClient();
                User user = users[cboUsers.SelectedIndex];
                user.FirstName = txtFirstName.Text;
                user.LastName = txtLastName.Text;
                user.Email = txtEmail.Text;
                user.Password = txtPassword.Text;

                // Utilize the Web API
                string serializedUser = JsonConvert.SerializeObject(user);
                var content = new StringContent(serializedUser);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("User/" + user.Id, content).Result;

                Reload();

                txtFirstName.Clear();
                txtLastName.Clear();
                txtEmail.Clear();
                txtPassword.Clear();

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            HttpClient client = InitializeClient();
            HttpResponseMessage response = client.DeleteAsync("User/" + user.Id).Result;

            Reload();

            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();




        }
    }
}
