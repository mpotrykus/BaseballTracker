using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.BaseballTracker.PL;
using MK.BaseballTracker.BL;

namespace MK.BaseballTracker.BL.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid OpposingTeamId { get; set; }
        public int TeamScore { get; set; }
        public int OpposingTeamScore { get; set; }
        public bool Home { get; set; }
        public DateTime Date { get; set; }
        public Team Team { get; set; }
        public Team OpposingTeam { get; set; }
        private string gameName = "gameName";
        public string GameName { get { return gameName; }  set { gameName = (Team.Name + " vs " + OpposingTeam.Name); } }
        private string location = "location";
        public string Location { get { return location; } set { if (Home) { location = Team.Location; } else { location = OpposingTeam.Location; }; } }
        private string gameScore = "gameScore";
        public string GameScore { get { return gameScore; } set { gameScore = (TeamScore + " - " + OpposingTeamScore); } }
        
    }
}
