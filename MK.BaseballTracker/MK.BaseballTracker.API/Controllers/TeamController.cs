using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MK.BaseballTracker.BL;
using MK.BaseballTracker.BL.Models;
//using Microsoft.AspNet.SignalR.Client;

namespace MK.BaseballTracker.API.Controllers
{
    public class TeamController : ApiController
    {
        // GET: api/Team
        public IEnumerable<Team> Get()
        {
            return TeamManager.Load();
        }

        // GET: api/Team/5
        public Team Get(Guid id)
        {
            return TeamManager.LoadbyId(id);
        }

        [HttpGet]
        [Route("api/LoadByUserId")]
        public IEnumerable<Team> LoadByUserId(Guid id)
        {
            //User user = UserManager.LoadById(id);
            //SendMessageToChannel(user.FirstName + " " + user.LastName, "Loading Your Teams...");
            return UserTeamManager.LoadByUserId(id);
        }

        // POST: api/Team
        public void Post([FromBody]Team team)
        {
            Guid id;
            TeamManager.Insert(team, out id);
        }

        // PUT: api/Team/5
        public void Put(Guid id, [FromBody]Team team)
        {
            TeamManager.Update(team, id);
        }

        // DELETE: api/Team/5
        public void Delete(Guid id)
        {
            TeamManager.Delete(id);
        }

        /*
        private void SendMessageToChannel(string name, string message)
        {
            var hubConnection = new HubConnection(Properties.Settings.Default.HubAddress);
            var myHub = hubConnection.CreateHubProxy(Properties.Settings.Default.HubName);

            hubConnection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {

                }
                else
                {
                    myHub.Invoke<string>("Send", name, message).ContinueWith(task1 =>
                    {
                        if (task1.IsFaulted)
                        {

                        }
                        else
                        {

                        }
                    });
                }
            }).Wait();
        }
        */
    }
}





