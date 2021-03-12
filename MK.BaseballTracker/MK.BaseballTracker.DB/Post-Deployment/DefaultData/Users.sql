BEGIN
	INSERT INTO dbo.tblUsers (UserId, FirstName, LastName, Email, DateAdded, Password)
	VALUES
	(NEWID(), 'George', 'Thomas', 'George.Thomas@me.com','2016-08-09 08:04:23', 'TestPass'),
	(NEWID(), 'Todd', 'Lawrence', 'Todd.Lawrence@me.com','2017-09-11 08:04:23', 'TestPass1'),
	(NEWID(), 'Stacey', 'Strong', 'Stacey.Strong@me.com','2019-03-04 08:04:23', 'TestPass2'),
	(NEWID(), 'Kendra', 'Cooley', 'Kendra.Cooley@me.com','2015-03-09 08:04:23', 'TestPass3'),
	(NEWID(), 'Theo', 'Crouch', 'Theo.Crouch@me.com','2020-02-09 08:04:23', 'TestPass4')
END