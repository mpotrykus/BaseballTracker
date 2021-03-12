using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MK.BaseballTracker.PL;
using System.Linq;

namespace MK.BaseballTracker.PL.Test
{
    [TestClass]
    public class utGames
    {
        [TestInitialize]
        public void init()
        {
        }
        [TestMethod]
        public void LoadTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                int expected = 2;
                var results = dc.tblGames.ToList();
                int actual = results.Count;
                Assert.AreEqual(expected, actual);
            }
        }
        [TestMethod]
        public void InsertTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                tblGame newrow = new tblGame();
                newrow.GameId = Guid.NewGuid();
                newrow.TeamId = dc.tblTeams.FirstOrDefault(t => t.Name == "Milwaukee Brewers").TeamId;
                newrow.OpposingTeamId = dc.tblTeams.FirstOrDefault(te => te.Name == "New York Yankees").TeamId;
                newrow.TeamScore = 5;
                newrow.OpposingTeamScore = 0;
                newrow.Home = true;
                newrow.Date = DateTime.Today;
                dc.tblGames.Add(newrow);
                int results = dc.SaveChanges();
                Assert.IsTrue(results != 0);
            }
        }

        [TestMethod]
        public void UpdateTest() 
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {

                var results = (from g in dc.tblGames
                               select new
                               {
                                   g.GameId,
                                   g.TeamId,
                                   g.OpposingTeamId,                             
                                   g.TeamScore,
                                   g.OpposingTeamScore,
                                   g.Home,
                                   g.Date,
                                   TeamName = g.tblTeam.Name,                                  
                               }).ToList()
                                  .Where(t => t.TeamName == "New York Yankees")
                                  .FirstOrDefault();

                tblGame game = dc.tblGames.FirstOrDefault(g => g.GameId == results.GameId);

                game.OpposingTeamScore = 1;
                int actual = dc.SaveChanges();
                Assert.AreEqual(1, actual);

            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                tblGame row = dc.tblGames.Where(g => g.OpposingTeamScore == 1).FirstOrDefault();
                if (row != null)
                {
                    dc.tblGames.Remove(row);
                    int results = dc.SaveChanges();
                    Assert.IsTrue(results != 0);
                }
            }
        }
    }
}

