using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MK.BaseballTracker.BL;
using MK.BaseballTracker.BL.Models;


namespace MK.BaseballTracker.API.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public IEnumerable<User> Get()
        {
            return UserManager.Load();
        }

        [HttpGet]
        [Route("api/Login")]
        public User Login(String email, String password)
        {
            return UserManager.Login(email, password);
        }

        // GET: api/User/5
        public User Get(Guid id)
        {
            return UserManager.LoadById(id);
        }

        public User Get(string email, string password)
        {
            BL.Models.User user = UserManager.Login(email, password);
            return user;
        }

        // POST: api/User
        public void Post([FromBody]User user)
        {
            Guid id;
            UserManager.Insert(user, out id);
        }

        // PUT: api/User/5
        public void Put(Guid id, [FromBody]User user)
        {
            UserManager.Update(user, id);
        }

        // DELETE: api/User/5
        public void Delete(Guid id)
        {
            UserManager.Delete(id);
        }
    }
}


