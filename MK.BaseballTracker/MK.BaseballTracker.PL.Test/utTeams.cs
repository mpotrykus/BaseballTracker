using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MK.BaseballTracker.PL;
using System.Linq;

namespace MK.BaseballTracker.PL.Test
{
    [TestClass]
    public class utTeams
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
                int expected = 5;
                var results = dc.tblTeams.ToList();
                int actual = results.Count;
                Assert.AreEqual(expected, actual);
            }
        }
        [TestMethod]
        public void InsertTest()
        {
           
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                tblTeam newrow = new tblTeam();
                newrow.TeamId = Guid.NewGuid();
                newrow.Name = "Boston Red Sox";
                newrow.Location = "Boston";
                newrow.Logo = null;
                dc.tblTeams.Add(newrow);
                int results = dc.SaveChanges();
                Assert.IsTrue(results != 0);
            }
        }
        [TestMethod]
        public void UpdateTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                tblTeam row = dc.tblTeams.Where(t => t.Name == "Boston Red Sox").FirstOrDefault();
                if (row != null)
                {
                    row.Name = "Cleveland Indians";
                    row.Location = "Cleveland";
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
                tblTeam row = dc.tblTeams.Where(t => t.Name == "Cleveland Indians").FirstOrDefault();
                if (row != null)
                {
                    dc.tblTeams.Remove(row);
                    int results = dc.SaveChanges();
                    Assert.IsTrue(results != 0);
                }
            }
        }
    }
}




