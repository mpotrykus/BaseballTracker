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
    public class utGame
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
            HttpResponseMessage response = client.GetAsync("Game").Result;
            string result = response.Content.ReadAsStringAsync().Result;

            dynamic items = (JArray)JsonConvert.DeserializeObject(result);

            List<Game> games = items.ToObject<List<Game>>();

            Assert.AreEqual(2, games.Count);
        }
    }
}

