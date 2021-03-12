BEGIN
	DECLARE @UserId uniqueidentifier;
	SELECT @UserId = UserId from tblUsers where FirstName = 'Stacey'

	DECLARE @TeamId1 uniqueidentifier;
	SELECT @TeamId1 = TeamId from tblTeams where Name = 'Milwaukee Brewers'

	INSERT INTO dbo.tblUserTeam (UserTeamId, UserId, TeamId)
	VALUES
	(NEWID(), @UserId, @TeamId1)

	SELECT @UserId = UserId from tblUsers where LastName = 'Thomas'

	SELECT @TeamId1 = TeamId from tblTeams where Location = 'Los Angeles'

	INSERT INTO dbo.tblUserTeam (UserTeamId, UserId, TeamId)
	VALUES
	(NEWID(), @UserId, @TeamId1)

	SELECT @UserId = UserId from tblUsers where LastName = 'Crouch'

	SELECT @TeamId1 = TeamId from tblTeams where Location = 'New York'

	INSERT INTO dbo.tblUserTeam (UserTeamId, UserId, TeamId)
	VALUES
	(NEWID(), @UserId, @TeamId1)
	
END