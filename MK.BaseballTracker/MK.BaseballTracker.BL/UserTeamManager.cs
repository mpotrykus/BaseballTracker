using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.BaseballTracker.PL;
using MK.BaseballTracker.BL.Models;

namespace MK.BaseballTracker.BL
{
    public class UserTeamManager
    {
        public static int Insert(UserTeam userTeam, out Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    tblUserTeam userTeamNew = new tblUserTeam();

                    userTeamNew.UserTeamId = Guid.NewGuid();
                    userTeamNew.UserId = userTeam.UserId;
                    userTeamNew.TeamId = userTeam.TeamId;

                    id = userTeamNew.UserTeamId;

                    dc.tblUserTeams.Add(userTeamNew);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int Update(UserTeam userTeam, Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    tblUserTeam userTeamNew = dc.tblUserTeams.FirstOrDefault(m => m.UserTeamId == id);

                    if (userTeam != null)
                    {
                        userTeamNew.UserId = userTeam.UserId;
                        userTeamNew.TeamId = userTeam.TeamId;

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
                    tblUserTeam userTeam = dc.tblUserTeams.FirstOrDefault(m => m.UserTeamId == id);

                    if (userTeam != null)
                    {
                        dc.tblUserTeams.Remove(userTeam);

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
        public static UserTeam LoadById(Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {

                    tblUserTeam userTeam = dc.tblUserTeams.FirstOrDefault(m => m.UserTeamId == id);

                    if (userTeam != null)
                    {
                        return new UserTeam
                        {
                            Id = userTeam.UserTeamId,
                            UserId = userTeam.UserId,
                            TeamId = userTeam.TeamId
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
        public static int DeleteByUserTeamId(Guid userId, Guid teamId)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    List<tblUserTeam> userTeams = dc.tblUserTeams.Where(m => m.UserId == userId && m.TeamId == teamId).ToList();

                    if (userTeams != null)
                    {
                        foreach (tblUserTeam qa in userTeams)
                        {
                            dc.tblUserTeams.Remove(qa);
                        }

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
        public static List<UserTeam> Load()
        {
            try
            {
                List<UserTeam> teams = new List<UserTeam>();
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    dc.tblUserTeams
                      .ToList()
                      .ForEach(m => teams.Add(new UserTeam
                      {
                          Id = m.UserTeamId,
                          UserId = m.UserId,
                          TeamId = m.TeamId
                      }));
                }
                return teams;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static UserTeam LoadByUserTeamId(Guid userId, Guid teamId)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {

                    tblUserTeam userTeam = dc.tblUserTeams.FirstOrDefault(m => m.TeamId == teamId && m.UserId == userId);

                    if (userTeam != null)
                    {
                        return new UserTeam
                        {
                            Id = userTeam.UserTeamId,
                            TeamId = userTeam.TeamId,
                            UserId = userTeam.UserId
                        };
                    }
                    else
                    {
                        return null;
                        //throw new Exception("Cannot find the row.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Team> LoadByUserId(Guid Id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {

                    List<Team> teams = new List<Team>();
                    using (BaseballTrackerEntities sm = new BaseballTrackerEntities())
                    {

                        var results = (from ut in sm.tblUserTeams
                                       join u in sm.tblUsers on ut.UserId equals u.UserId
                                       join t in sm.tblTeams on ut.TeamId equals t.TeamId
                                       where ut.UserId == Id
                                       orderby t.Name ascending
                                       select new
                                       {
                                           t.TeamId,
                                           t.Name,
                                           t.Location,
                                           t.Logo
                                       }).ToList();

                        results.ForEach(t => teams.Add(new Team
                        {
                            Id = t.TeamId,
                            Name = t.Name,
                            Location = t.Location,
                            Logo = t.Logo
                        }));
                    }

                    return teams;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
