using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MaintainGames.xaml
    /// </summary>
    public partial class MaintainGames : Window
    {
        BL.Models.Game game;
        List<BL.Models.Game>games;
        List<Team> teams;

        public MaintainGames()
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
            games = new List<Game>();
            teams = new List<Team>();
            game = new Game();

            Debug.WriteLine("0");

            HttpClient client = InitializeClient();
            HttpResponseMessage response = client.GetAsync("Game").Result;
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            games = items.ToObject<List<Game>>();

            cboGames.ItemsSource = null;
            cboGames.ItemsSource = games;
            cboGames.DisplayMemberPath = "GameName";
            cboGames.SelectedValuePath = "Id";

            HttpResponseMessage response1 = client.GetAsync("Team").Result;
            string result1 = response1.Content.ReadAsStringAsync().Result;
            dynamic items1 = (JArray)JsonConvert.DeserializeObject(result1);
            teams = items1.ToObject<List<Team>>();

            Debug.WriteLine("2");

            Debug.WriteLine("cboGames: " + cboGames.SelectedIndex);
            Debug.WriteLine("cboTeams: " + cboTeams.SelectedIndex);
            Debug.WriteLine("cboOpposingTeams: " + cboOpposingTeams.SelectedIndex);

            Debug.WriteLine("2-1");

            cboTeams.SelectedIndex = -1;
            cboOpposingTeams.SelectedIndex = -1;

            Debug.WriteLine("3-1");
            
            cboTeams.ItemsSource = null;
            cboTeams.ItemsSource = teams;
            cboTeams.DisplayMemberPath = "Name";
            cboTeams.SelectedValuePath = "Id";
            
            Debug.WriteLine("3-2");

            cboOpposingTeams.ItemsSource = null;
            cboOpposingTeams.ItemsSource = teams;
            cboOpposingTeams.DisplayMemberPath = "Name";
            cboOpposingTeams.SelectedValuePath = "Id";

            Debug.WriteLine("4");

            txtTeamScore.Text = "";
            txtOpposingTeamScore.Text = "";
            cbHome.IsChecked = false;
            dpDate.SelectedDate = Convert.ToDateTime("2020-01-01");
            
            Debug.WriteLine("5");
            
        }



        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new MainWindow().ShowDialog();
            this.Close();

        }

        private void CboGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboGames.SelectedIndex > -1)
            {
                if (games.Count > 0)
                {
                    var selectedGame = games[cboGames.SelectedIndex];
                    //selectedGame.TeamId = teams[cboTeams.SelectedIndex].Id;
                    //selectedGame.OpposingTeamId = teams[cboTeams.SelectedIndex].Id;

                    //cboTeams.SelectedIndex = cboTeams.FindStringExact(selectedGame.Team.Name);

                    cboTeams.Text = selectedGame.Team.Name;
                    cboOpposingTeams.Text = selectedGame.OpposingTeam.Name;
                    txtTeamScore.Text = selectedGame.TeamScore.ToString();
                    txtOpposingTeamScore.Text = selectedGame.OpposingTeamScore.ToString();
                    dpDate.SelectedDate = selectedGame.Date;

                    if (selectedGame.Home)
                    {
                        cbHome.IsChecked = true;
                    }

                    Debug.WriteLine("cboTeams: " + cboTeams.SelectedIndex);
                    Debug.WriteLine("cboOpposingTeams: " + cboOpposingTeams.SelectedIndex);


                    HttpClient client = InitializeClient();
                    HttpResponseMessage response = client.GetAsync("Games/" + games[cboGames.SelectedIndex].Id).Result;
                    string result = response.Content.ReadAsStringAsync().Result;
                    dynamic items = (JObject)JsonConvert.DeserializeObject(result);
                    game = items.ToObject<Game>();
                }
            }

        }
               

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                HttpClient client = InitializeClient();

                Game game = new Game();

                game.TeamId = teams[cboTeams.SelectedIndex].Id;
                game.OpposingTeamId = teams[cboOpposingTeams.SelectedIndex].Id;
                game.TeamScore = Convert.ToInt32(txtTeamScore.Text);
                game.OpposingTeamScore = Convert.ToInt32(txtOpposingTeamScore.Text);
                game.Home = cbHome.IsChecked ?? false;
                game.Date = Convert.ToDateTime(dpDate.SelectedDate);

                string serializedGame = JsonConvert.SerializeObject(game);
                var content = new StringContent(serializedGame);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("Game", content).Result;

                Reload();
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

                game.Id = games[cboGames.SelectedIndex].Id;
                game.TeamId = teams[cboTeams.SelectedIndex].Id;
                game.OpposingTeamId = teams[cboOpposingTeams.SelectedIndex].Id;
                game.TeamScore = Convert.ToInt32(txtTeamScore.Text);
                game.OpposingTeamScore = Convert.ToInt32(txtOpposingTeamScore.Text);
                game.Home = cbHome.IsChecked ?? false;
                game.Date = Convert.ToDateTime(dpDate.SelectedDate);
                game.Team = teams[cboTeams.SelectedIndex];
                game.OpposingTeam = teams[cboOpposingTeams.SelectedIndex];

                string serializedGame = JsonConvert.SerializeObject(game);
                var content = new StringContent(serializedGame);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Game/" + game.Id, content).Result;

                Reload();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response = client.DeleteAsync("Game/" + games[cboGames.SelectedIndex].Id).Result;

            Reload();
        }
    }
}
