using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MK.BaseballTracker.PL;
using System.Linq;

namespace MK.BaseballTracker.PL.Test
{
    [TestClass]
    public class utUserTeams
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
                int expected = 3;
                var results = dc.tblUserTeams.ToList();
                int actual = results.Count;
                Assert.AreEqual(expected, actual);
            }
        }
        [TestMethod]
        public void InsertTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                tblUserTeam newrow = new tblUserTeam();
                newrow.UserTeamId = Guid.NewGuid();
                newrow.TeamId = dc.tblTeams.FirstOrDefault(t => t.Name == "Milwaukee Brewers").TeamId;
                newrow.UserId = dc.tblUsers.FirstOrDefault(u => u.FirstName == "Stacey").UserId;
                dc.tblUserTeams.Add(newrow);
                int results = dc.SaveChanges();
                Assert.IsTrue(results != 0);
            }
        }
        [TestMethod]
        public void UpdateTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                tblUserTeam newrow = new tblUserTeam();
                newrow.UserTeamId = Guid.NewGuid();
                newrow.TeamId = dc.tblTeams.FirstOrDefault(t => t.Name == "Milwaukee Brewers").TeamId;
                newrow.UserId = dc.tblUsers.FirstOrDefault(u => u.FirstName == "Stacey").UserId;
                tblUserTeam row = dc.tblUserTeams.Where(c => c.UserId == newrow.UserId && c.TeamId == newrow.TeamId).FirstOrDefault();

                if (row != null)
                {
                    row.UserId = dc.tblUsers.FirstOrDefault(u => u.FirstName == "George").UserId;
                    int results = dc.SaveChanges();
                    Assert.IsTrue(results != 0);
                }

            }

        }
        [TestMethod]
        public void DeleteTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                tblUserTeam newrow = new tblUserTeam();
                newrow.UserTeamId = Guid.NewGuid();
                newrow.TeamId = dc.tblTeams.FirstOrDefault(t => t.Name == "Milwaukee Brewers").TeamId;
                newrow.UserId = dc.tblUsers.FirstOrDefault(u => u.FirstName == "George").UserId;
                tblUserTeam row = dc.tblUserTeams.Where(c => c.UserId == newrow.UserId && c.TeamId == newrow.TeamId).FirstOrDefault();

                if (row != null)
                {
                    dc.tblUserTeams.Remove(row);
                    int results = dc.SaveChanges();
                    Assert.IsTrue(results != 0);
                }
            }

        }
    }
}

       
       