//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MK.BaseballTracker.PL
{
    using System;
    
    public partial class spGetGames_Result
    {
        public System.Guid GameId { get; set; }
        public Nullable<int> TeamScore { get; set; }
        public Nullable<int> OpposingTeamScore { get; set; }
        public System.DateTime Date { get; set; }
        public bool Home { get; set; }
        public System.Guid TeamId { get; set; }
        public System.Guid OpposingTeamId { get; set; }
        public string TeamName { get; set; }
        public string OpposingTeamName { get; set; }
    }
}
