using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.BaseballTracker.PL;
using MK.BaseballTracker.BL.Models;
using MK.Utilities.Reporting;

namespace MK.BaseballTracker.BL
{
    public class TeamManager
    {
        public static int Insert(Team team, out Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    tblTeam teamNew = new tblTeam();

                    teamNew.TeamId = Guid.NewGuid();
                    teamNew.Name = team.Name;
                    teamNew.Location = team.Location;
                    teamNew.Logo = team.Logo;

                    id = teamNew.TeamId;

                    dc.tblTeams.Add(teamNew);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int Update(Team team, Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    tblTeam teamNew = dc.tblTeams.FirstOrDefault(m => m.TeamId == id);

                    if (team != null)
                    {
                        teamNew.Name = team.Name;
                        teamNew.Location = team.Location;
                        teamNew.Logo = team.Logo;

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
                    tblTeam team = dc.tblTeams.FirstOrDefault(m => m.TeamId == id);

                    if (team != null)
                    {
                        dc.tblTeams.Remove(team);

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
        public static Team LoadbyId(Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {

                    tblTeam team = dc.tblTeams.FirstOrDefault(m => m.TeamId == id);

                    if (team != null)
                    {
                        return new Team
                        {
                            Id = team.TeamId,
                            Name = team.Name,
                            Location = team.Location,
                            Logo = team.Logo
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
        public static List<Team> Load()
        {
            try
            {
                List<Team> teams = new List<Team>();
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    dc.tblTeams
                      .ToList()
                      .ForEach(m => teams.Add(new Team
                      {
                          Id = m.TeamId,
                          Name = m.Name,
                          Location = m.Location,
                          Logo = m.Logo
                      }));
                }
                return teams;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static void Export(List<Team> teams)
        {
            try
            {
                string[,] data = new string[teams.Count + 1, 3];

                int counter = 0;
                data[counter, 0] = "Name";
                data[counter, 1] = "Location";
                data[counter, 2] = "Logo";
                counter++;

                foreach (Team t in teams)
                {
                    data[counter, 0] = t.Name;
                    data[counter, 1] = t.Location;
                    data[counter, 2] = t.Logo;
                    counter++;
                }

                Excel.Export("Teams.xlsx", data);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
