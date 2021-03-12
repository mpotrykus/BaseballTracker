CREATE PROCEDURE [dbo].[spGetGames]
AS
	SELECT G.GameId, G.TeamScore, G.OpposingTeamScore, G.Date, G.Home, G.TeamId, G.OpposingTeamId,
	t.Name TeamName,
	te.Name OpposingTeamName
	FROM tblGames G
	INNER JOIN tblTeams t on G.TeamId = t.TeamId
	INNER JOIN tblTeams te on G.OpposingTeamId = t.TeamId
RETURN 0
