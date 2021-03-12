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
    public class UserTeamController : ApiController
    {
        // GET: api/UserTeam
        public IEnumerable<UserTeam> Get()
        {
            return UserTeamManager.Load();
        }

        // GET: api/UserTeam/5
        public UserTeam Get(Guid id)
        {
            return UserTeamManager.LoadById(id);
        }

        [HttpGet]
        [Route("api/LoadByUserTeamId")]
        public UserTeam LoadByUserTeamId(Guid userId, Guid teamId)
        {
            return UserTeamManager.LoadByUserTeamId(userId, teamId);
        }

        // POST: api/UserTeam
        public void Post([FromBody]UserTeam user)
        {
            Guid id;
            UserTeamManager.Insert(user, out id);
        }

        // PUT: api/UserTeam/5
        public void Put(Guid id, [FromBody]UserTeam user)
        {
            UserTeamManager.Update(user, id);
        }

        // DELETE: api/UserTeam/5
        public void Delete(Guid id)
        {
            UserTeamManager.Delete(id);
        }

        [Route("api/DeleteByUserTeamId")]
        public void DeleteByUserTeamId(Guid userId, Guid teamId)
        {
            UserTeamManager.DeleteByUserTeamId(userId, teamId);
        }
    }
}
