using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MK.BaseballTracker.PL;
using System.Linq;

namespace MK.BaseballTracker.PL.Test
{
    [TestClass]
    public class utUsers
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
                    var results = dc.tblUsers.ToList();
                    int actual = results.Count;
                    Assert.AreEqual(expected, actual);
                }
            }
            [TestMethod]
            public void InsertTest()
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    tblUser newrow = new tblUser();
                    newrow.UserId = Guid.NewGuid();
                    newrow.FirstName = "Bill";
                    newrow.LastName = "Donelly";
                    newrow.Email = "Bill.Donelly@test.com";
                    newrow.DateAdded = DateTime.Now;
                    newrow.Password = "Test6";
                    dc.tblUsers.Add(newrow);
                    int results = dc.SaveChanges();
                    Assert.IsTrue(results != 0);
                }
            }
            [TestMethod]
            public void UpdateTest()
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    tblUser row = dc.tblUsers.Where(u => u.FirstName == "Bill").FirstOrDefault();
                    if (row != null)
                    {
                        row.FirstName = "Barney";
                        row.LastName = "NewLastName";
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
                    tblUser row = dc.tblUsers.Where(u => u.FirstName == "Barney").FirstOrDefault();
                    if (row != null)
                    {
                        dc.tblUsers.Remove(row);
                        int results = dc.SaveChanges();
                        Assert.IsTrue(results != 0);
                    }
                }
            }
        }
    }





