﻿CREATE TABLE [dbo].[tblUsers]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR (50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL,
	[Email] VARCHAR(50) NOT NULL,
	[DateAdded] DATETIME NOT NULL,
	[Password] VARCHAR (50) NOT NULL



)