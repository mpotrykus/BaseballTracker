using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MK.BaseballTracker.BL;
using MK.BaseballTracker.BL.Models;
using System.Linq;
using System.Collections.Generic;

namespace MK.BaseballTracker.BL.Test
{
    [TestClass]
    public class utTeam
    {
        [TestMethod]
        public void RunAll()
        {
            LoadTest();
            InsertTest();
            UpdateTest();
            DeleteTest();
        }

        public void LoadTest()
        {
            Assert.AreNotEqual(0, TeamManager.Load().Count);
        }

        public void InsertTest()
        {
            Guid id;
            Team team = new Team();
            team.Location = "Phoenix, AZ";
            team.Logo = "newImg";
            team.Name = "Arizona Diamondbacks";
            Assert.IsTrue(TeamManager.Insert(team, out id) > 0);
        }

        public void UpdateTest()
        {
            List<Team> teams = TeamManager.Load();
            Team team = teams.FirstOrDefault(m => m.Name == "Arizona Diamondbacks");
            team.Logo = "updatedLogo";
            int actual = TeamManager.Update(team, team.Id);
            Assert.AreNotEqual(0, actual);
        }

        public void DeleteTest()
        {
            List<Team> teams = TeamManager.Load();
            Team team = teams.FirstOrDefault(m => m.Name == "Arizona Diamondbacks");
            Assert.AreNotEqual(TeamManager.Delete(team.Id), 0);
        }
    }
}
