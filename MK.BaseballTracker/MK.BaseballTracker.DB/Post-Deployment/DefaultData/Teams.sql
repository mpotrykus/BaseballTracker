BEGIN
	INSERT INTO dbo.tblTeams (TeamId, Name, Location, Logo)
	VALUES
	(NEWID(), 'Milwaukee Brewers', 'Milwaukee','img'),
	(NEWID(), 'LA Dodgers', 'Los Angeles','img'),
	(NEWID(), 'Chicago Cubs', 'Chicago','img'),
	(NEWID(), 'New York Yankees', 'New York','img'),
	(NEWID(), 'Pittsburgh Pirates', 'Pittsburgh','img')
END