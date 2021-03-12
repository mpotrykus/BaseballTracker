BEGIN
	DECLARE @TeamId uniqueidentifier;
	SELECT @TeamId = TeamId from tblTeams where Name = 'Milwaukee Brewers'

	DECLARE @OpposingTeamId uniqueidentifier;
	SELECT @OpposingTeamId = TeamId from tblTeams where Name = 'LA Dodgers'

	INSERT INTO dbo.tblGames (GameId, TeamId, OpposingTeamId, TeamScore, OpposingTeamScore, Home, Date)
	VALUES
	(NEWID(), @TeamId, @OpposingTeamId, 6, 1, 1, '2019-06-07 07:25:00')

	
	SELECT @TeamId = TeamId from tblTeams where Name = 'New York Yankees'

	SELECT @OpposingTeamId = TeamId from tblTeams where Name = 'Pittsburgh Pirates'

	INSERT INTO dbo.tblGames (GameId, TeamId, OpposingTeamId, TeamScore, OpposingTeamScore, Home, Date)
	VALUES
	(NEWID(), @TeamId, @OpposingTeamId, 1, 5, 0, '2019-07-09 01:05:00')


	SELECT @TeamId = TeamId from tblTeams where Name = 'Chicago Cubs'

	SELECT @OpposingTeamId = TeamId from tblTeams where Name = 'Milwaukee Brewers'

	INSERT INTO dbo.tblGames (GameId, TeamId, OpposingTeamId, TeamScore, OpposingTeamScore, Home, Date)
	VALUES
	(NEWID(), @TeamId, @OpposingTeamId, 3, 8, 1, '2019-09-09 03:05:00')

END