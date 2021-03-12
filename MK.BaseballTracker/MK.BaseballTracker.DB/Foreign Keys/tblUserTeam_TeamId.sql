ALTER TABLE [dbo].[tblUserTeam]
	ADD CONSTRAINT [tblUserTeam_TeamId]
	FOREIGN KEY (TeamId)
	REFERENCES [dbo].[tblTeams] (TeamId) ON DELETE CASCADE