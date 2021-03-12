using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.BaseballTracker.PL;
using MK.BaseballTracker.BL.Models;

namespace MK.BaseballTracker.BL
{
    public class UserManager
    {
        public static int Insert(User user, out Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    tblUser userNew = new tblUser();

                    userNew.UserId = Guid.NewGuid();
                    userNew.FirstName = user.FirstName;
                    userNew.LastName = user.LastName;
                    userNew.Email = user.Email;
                    userNew.Password = user.Password;
                    userNew.DateAdded = DateTime.Today;

                    id = userNew.UserId;

                    dc.tblUsers.Add(userNew);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int Update(User user, Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    tblUser userNew = dc.tblUsers.FirstOrDefault(m => m.UserId == id);

                    if (user != null)
                    {
                       
                        userNew.FirstName = user.FirstName;
                        userNew.LastName = user.LastName;
                        userNew.Email = user.Email;
                        userNew.Password = user.Password;
                        userNew.DateAdded = user.DateAdded;

                        return dc.SaveChanges();
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int Delete(Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    tblUser user = dc.tblUsers.FirstOrDefault(m => m.UserId == id);

                    if (user != null)
                    {
                        dc.tblUsers.Remove(user);

                        return dc.SaveChanges();
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static User LoadById(Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {

                    tblUser user = dc.tblUsers.FirstOrDefault(m => m.UserId == id);

                    if (user != null)
                    {
                        return new User
                        {
                            Id = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Password = user.Password,
                            DateAdded = user.DateAdded
                        };
                    }
                    else
                    {
                        throw new Exception("Cannot find the row.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<User> Load()
        {
            try
            {
                List<User> teams = new List<User>();
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    dc.tblUsers
                      .ToList()
                      .ForEach(m => teams.Add(new User
                      {
                          Id = m.UserId,
                          FirstName = m.FirstName,
                          LastName = m.LastName,
                          Email = m.Email,
                          Password = m.Password,
                          DateAdded = m.DateAdded
                      }));
                }
                return teams;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private string GetHash(string password)
        {
            using (var hash = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hash.ComputeHash(hashbytes));
            }
        }
        public static User Login(string email, string password)
        {
            using (BaseballTrackerEntities db = new BaseballTrackerEntities())
            {
                if (!string.IsNullOrEmpty(email))
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        tblUser user = db.tblUsers.FirstOrDefault(u => u.Email == email);
                        if (user != null)
                        {
                            //if (user.Password == this.GetHash(password))
                            if (user.Password == password)
                            {
                                return new User
                                {
                                    Id = user.UserId,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    Email = user.Email,
                                    Password = user.Password,
                                    DateAdded = user.DateAdded
                                };
                            }
                            else { return null; }
                        }
                        else { return null; }
                    }
                    else { return null; }
                }
                else { return null; }
            }
        }
    }
}
