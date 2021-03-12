ALTER TABLE [dbo].[tblGames]
	ADD CONSTRAINT [tblGames_OpposingTeamId]
	FOREIGN KEY (TeamId)
	REFERENCES [dbo].[tblTeams] (TeamId) ON DELETE NO ACTION