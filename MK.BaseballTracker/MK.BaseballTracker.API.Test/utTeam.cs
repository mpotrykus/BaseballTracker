using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MK.BaseballTracker.BL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MK.BaseballTracker.API.Test
{
    [TestClass]
    public class utTeam
    {
        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            // Address of the Web API
            client.BaseAddress = new Uri("https://localhost:44376/api/");
            return client;
        }

        [TestMethod]
        public void LoadTest()
        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response = client.GetAsync("Team").Result;
            string result = response.Content.ReadAsStringAsync().Result;

            dynamic items = (JArray)JsonConvert.DeserializeObject(result);

            List<Team> teams = items.ToObject<List<Team>>();

            Assert.AreEqual(5, teams.Count);
        }
    }
}

