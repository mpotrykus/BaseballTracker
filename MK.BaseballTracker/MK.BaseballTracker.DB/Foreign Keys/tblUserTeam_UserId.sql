ALTER TABLE [dbo].[tblUserTeam]
	ADD CONSTRAINT [tblUserTeam_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [dbo].[tblUsers] (UserId) ON DELETE CASCADE