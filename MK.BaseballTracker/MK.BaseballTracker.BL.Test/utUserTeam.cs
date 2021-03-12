using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MK.BaseballTracker.BL;
using MK.BaseballTracker.BL.Models;
using System.Linq;
using System.Collections.Generic;
using MK.BaseballTracker.PL;

namespace MK.BaseballTracker.BL.Test
{
    [TestClass]
    public class utUserTeam
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
            Assert.AreNotEqual(0, UserTeamManager.Load().Count);
        }

        public void InsertTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                Guid id;
                UserTeam userTeam = new UserTeam();
                userTeam.UserId = dc.tblUsers.FirstOrDefault(c => c.Email == "George.Thomas@me.com").UserId;
                userTeam.TeamId = dc.tblTeams.FirstOrDefault(mo => mo.Name == "Chicago Cubs").TeamId;
                Assert.IsTrue(UserTeamManager.Insert(userTeam, out id) > 0);
            }
        }

        public void UpdateTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                User user = new User();
                user.Id = dc.tblUsers.FirstOrDefault(c => c.Email == "George.Thomas@me.com").UserId;
                Team team = new Team();
                team.Id = dc.tblTeams.FirstOrDefault(mo => mo.Name == "Chicago Cubs").TeamId;

                UserTeam userTeam = UserTeamManager.LoadByUserTeamId(user.Id, team.Id);
                userTeam.UserId = dc.tblUsers.FirstOrDefault(c => c.Email == "Stacey.Strong@me.com").UserId;
                int actual = UserTeamManager.Update(userTeam, userTeam.Id);
                Assert.AreNotEqual(0, actual);
            }
        }

        public void DeleteTest()
        {
            using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
            {
                User user = new User();
                user.Id = dc.tblUsers.FirstOrDefault(c => c.Email == "Stacey.Strong@me.com").UserId;
                Team team = new Team();
                team.Id = dc.tblTeams.FirstOrDefault(mo => mo.Name == "Chicago Cubs").TeamId;

                UserTeam userTeam = UserTeamManager.LoadByUserTeamId(user.Id, team.Id);
                Assert.AreNotEqual(UserTeamManager.Delete(userTeam.Id), 0);
            }
        }
    }
}
