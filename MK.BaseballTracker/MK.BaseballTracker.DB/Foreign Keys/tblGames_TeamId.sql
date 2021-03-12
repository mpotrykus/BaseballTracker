ALTER TABLE [dbo].[tblGames]
	ADD CONSTRAINT [tblGames_TeamId]
	FOREIGN KEY (TeamId)
	REFERENCES [dbo].[tblTeams] (TeamId) ON DELETE CASCADE