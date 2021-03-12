using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MK.BaseballTracker.BL;
using MK.BaseballTracker.BL.Models;
using MK.BaseballTracker.PL;
using System.Linq;
using System.Collections.Generic;

namespace MK.BaseballTracker.BL.Test
{
    [TestClass]
    public class utGame
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
            Assert.AreNotEqual(0, GameManager.Load().Count);
        }

        public void InsertTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                Guid id;
                Game game = new Game();
                game.TeamId = dc.tblTeams.FirstOrDefault(c => c.Name == "Milwaukee Brewers").TeamId;
                game.OpposingTeamId = dc.tblTeams.FirstOrDefault(mo => mo.Name == "Chicago Cubs").TeamId;
                game.TeamScore = 5;
                game.OpposingTeamScore = 2;
                game.Date = DateTime.Today;
                game.Home = true;
                Assert.IsTrue(GameManager.Insert(game, out id) > 0);
            }
        }

        public void UpdateTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                Team team = new Team();
                team.Id = dc.tblTeams.FirstOrDefault(c => c.Name == "Milwaukee Brewers").TeamId;
                Team opposingTeam = new Team();
                opposingTeam.Id = dc.tblTeams.FirstOrDefault(mo => mo.Name == "Chicago Cubs").TeamId;

                Game game = GameManager.LoadByTeamIds(team.Id, opposingTeam.Id);
                game.TeamScore = 3;
                int actual = GameManager.Update(game, game.Id);
                Assert.AreNotEqual(0, actual);
            }
        }

        public void DeleteTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                Team team = new Team();
                team.Id = dc.tblTeams.FirstOrDefault(c => c.Name == "Milwaukee Brewers").TeamId;
                Team opposingTeam = new Team();
                opposingTeam.Id = dc.tblTeams.FirstOrDefault(mo => mo.Name == "Chicago Cubs").TeamId;

                Game game = GameManager.LoadByTeamIds(team.Id, opposingTeam.Id);
                Assert.AreNotEqual(GameManager.Delete(game.Id), 0);
            }
        }
    }
}
