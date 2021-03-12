using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.BaseballTracker.PL;
using MK.BaseballTracker.BL.Models;

namespace MK.BaseballTracker.BL
{
    public class GameManager
    {
        public static int Insert(Game game, out Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    tblGame gameNew = new tblGame();

                    gameNew.GameId = Guid.NewGuid();
                    gameNew.TeamId = game.TeamId;
                    gameNew.TeamScore = game.TeamScore;
                    gameNew.OpposingTeamId = game.OpposingTeamId;
                    gameNew.OpposingTeamScore = game.OpposingTeamScore;
                    gameNew.Home = game.Home;
                    gameNew.Date = game.Date;

                    id = gameNew.GameId;

                    dc.tblGames.Add(gameNew);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int Update(Game game, Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    tblGame gameNew = dc.tblGames.FirstOrDefault(m => m.GameId == id);

                    if (game != null)
                    {
                        gameNew.TeamId = game.TeamId;
                        gameNew.TeamScore = game.TeamScore;
                        gameNew.OpposingTeamId = game.OpposingTeamId;
                        gameNew.OpposingTeamScore = game.OpposingTeamScore;
                        gameNew.Home = game.Home;
                        gameNew.Date = game.Date;

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
                    tblGame game = dc.tblGames.FirstOrDefault(m => m.GameId == id);

                    if (game != null)
                    {
                        dc.tblGames.Remove(game);

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
        public static Game LoadById(Guid id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {

                    tblGame game = dc.tblGames.FirstOrDefault(m => m.GameId == id);

                    if (game != null)
                    {
                        return new Game {
                            Id = game.GameId,
                            TeamId = game.TeamId,
                            TeamScore = (int)game.TeamScore,
                            OpposingTeamId = game.OpposingTeamId,
                            OpposingTeamScore = (int)game.OpposingTeamScore,
                            Home = game.Home,
                            Date = game.Date,
                            Team = TeamManager.LoadbyId(game.TeamId),
                            OpposingTeam = TeamManager.LoadbyId(game.OpposingTeamId)
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
        public static List<Game> Load()
        {
            try
            {
                List<Game> games = new List<Game>();
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {
                    dc.tblGames
                      .ToList()
                      .ForEach(m => games.Add(new Game
                      {
                          Id = m.GameId,
                          TeamId = m.TeamId,
                          TeamScore = (int)m.TeamScore,
                          OpposingTeamId = m.OpposingTeamId,
                          OpposingTeamScore = (int)m.OpposingTeamScore,
                          Home = m.Home,
                          Date = m.Date,
                          Team = TeamManager.LoadbyId(m.TeamId),
                          OpposingTeam = TeamManager.LoadbyId(m.OpposingTeamId)
                      }));
                }
                return games;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static Game LoadByTeamIds(Guid teamId, Guid opposingTeamId)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {

                    tblGame game = dc.tblGames.FirstOrDefault(m => m.TeamId == teamId && m.OpposingTeamId == opposingTeamId);

                    if (game != null)
                    {
                        return new Game
                        {
                            Id = game.GameId,
                            TeamId = game.TeamId,
                            TeamScore = (int)game.TeamScore,
                            OpposingTeamId = game.OpposingTeamId,
                            OpposingTeamScore = (int)game.OpposingTeamScore,
                            Home = game.Home,
                            Date = game.Date,
                            Team = TeamManager.LoadbyId(game.TeamId),
                            OpposingTeam = TeamManager.LoadbyId(game.OpposingTeamId)
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

        public static List<Game> LoadByTeamId(Guid Id)
        {
            try
            {
                using (BaseballTrackerEntities dc = new BaseballTrackerEntities())
                {

                    List<Game> teams = new List<Game>();
                    using (BaseballTrackerEntities sm = new BaseballTrackerEntities())
                    {

                        var results = (from ut in sm.tblGames
                                       where ut.TeamId == Id || ut.OpposingTeamId == Id
                                       orderby ut.Date ascending
                                       select new
                                       {
                                           ut.GameId,
                                           ut.TeamId,
                                           ut.OpposingTeamId,
                                           ut.TeamScore,
                                           ut.OpposingTeamScore,
                                           ut.Home,
                                           ut.Date
                                       }).ToList();

                        results.ForEach(t => teams.Add(new Game
                        {
                            Id = t.GameId,
                            TeamId = t.TeamId,
                            OpposingTeamId = t.OpposingTeamId,
                            TeamScore = (Int32)t.TeamScore,
                            OpposingTeamScore = (Int32)t.OpposingTeamScore,
                            Home = t.Home,
                            Date = t.Date,
                            Team = TeamManager.LoadbyId(t.TeamId),
                            OpposingTeam = TeamManager.LoadbyId(t.OpposingTeamId)
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
