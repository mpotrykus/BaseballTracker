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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MK.BaseballTracker.WPFUI.Admin
{
    /// <summary>
    /// Interaction logic for MaintainTeams.xaml
    /// </summary>
    public partial class MaintainTeams : Window
    {

        BL.Models.Team team;
        List<BL.Models.Team> teams;

        public MaintainTeams()
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
            HttpResponseMessage response = client.GetAsync("Team").Result;
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            teams = items.ToObject<List<BL.Models.Team>>();

            cboTeams.ItemsSource = null;
            cboTeams.ItemsSource = teams;
            cboTeams.DisplayMemberPath = "Name";
            cboTeams.SelectedValuePath = "Id";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            new MainWindow().ShowDialog();
            this.Close();


        }

        private void CboTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTeams.SelectedIndex > -1)
            {
                team = teams[cboTeams.SelectedIndex];
                txtTeamName.Text = team.Name;
                txtTeamLocation.Text = team.Location;
                txtLogo.Text = team.Logo;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                HttpClient client = InitializeClient();
                Team team = new Team();
                team.Name = txtTeamName.Text;
                team.Location = txtTeamLocation.Text;
                team.Logo = txtLogo.Text;
              

                string serializedTeam = JsonConvert.SerializeObject(team);
                var content = new StringContent(serializedTeam);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("Team", content).Result;

                Reload();

                txtTeamName.Clear();
                txtTeamLocation.Clear();
                txtLogo.Clear();
       
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
                Team team = teams[cboTeams.SelectedIndex];
                team.Name = txtTeamName.Text;
                team.Location = txtTeamLocation.Text;
                team.Logo = txtLogo.Text;

                // Utilize the Web API
                string serializedTeam = JsonConvert.SerializeObject(team);
                var content = new StringContent(serializedTeam);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Team/" + team.Id, content).Result;

                Reload();

                txtTeamName.Clear();
                txtTeamLocation.Clear();
                txtLogo.Clear();

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            HttpClient client = InitializeClient();
            HttpResponseMessage response = client.DeleteAsync("Team/" + team.Id).Result;

            Reload();

            txtTeamName.Clear();
            txtTeamLocation.Clear();
            txtLogo.Clear();

        }
    }
}
