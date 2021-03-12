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
    public class GameController : ApiController
    {
        // GET: api/Game
        public IEnumerable<Game> Get()
        {
            return GameManager.Load();
        }

        // GET: api/Game/5
        public Game Get(Guid id)
        {
            return GameManager.LoadById(id);
        }

        [HttpGet]
        [Route("api/LoadByTeamId")]
        public IEnumerable<Game> LoadByTeamId(Guid id)
        {
            return GameManager.LoadByTeamId(id);
        }


        // POST: api/Game
        public void Post([FromBody]Game game)
        {
            Guid id;
            GameManager.Insert(game, out id);
        }

        // PUT: api/Game/5
        public void Put(Guid id, [FromBody]Game game)
        {
            GameManager.Update(game, id);
        }

        // DELETE: api/Game/5
        public void Delete(Guid id)
        {
            GameManager.Delete(id);
        }
    }
}










