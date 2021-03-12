using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MK.BaseballTracker.BL;
using MK.BaseballTracker.BL.Models;
using System.Linq;
using System.Collections.Generic;

namespace MK.BaseballTracker.BL.Test
{
    [TestClass]
    public class utUser
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
            Assert.AreNotEqual(0, UserManager.Load().Count);
        }

        public void InsertTest()
        {
            Guid id;
            User user = new User();
            user.FirstName = "Michael";
            user.LastName = "Scott";
            user.Email = "mscott@dundermifflin.com";
            user.Password = "notpassword";
            Assert.IsTrue(UserManager.Insert(user, out id) > 0);
        }

        public void UpdateTest()
        {
            List<User> users = UserManager.Load();
            User user = users.FirstOrDefault(m => m.Email == "mscott@dundermifflin.com");
            user.Password = "screwtoby";
            int actual = UserManager.Update(user, user.Id);
            Assert.AreNotEqual(0, actual);
        }

        public void DeleteTest()
        {
            List<User> users = UserManager.Load();
            User user = users.FirstOrDefault(m => m.Email == "mscott@dundermifflin.com");
            Assert.AreNotEqual(UserManager.Delete(user.Id), 0);
        }
    }
}
