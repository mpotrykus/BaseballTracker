CREATE TABLE [dbo].[tblLog]
(
	[ErrDate] DATETIME NOT NULL, 
    [ErrThread] VARCHAR(200) NOT NULL, 
    [ErrLevel] VARCHAR(200) NOT NULL, 
    [ErrLogger] VARCHAR(200) NOT NULL, 
    [ErrMessage] VARCHAR(2000) NOT NULL, 
    [ErrUser] VARCHAR(50) NOT NULL
)
