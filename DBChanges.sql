--Create version table if it does not exist
IF OBJECT_ID (N'Databaseversion', N'U') IS NULL 
BEGIN
	CREATE TABLE Databaseversion
	(
		Id INT IDENTITY(1,1) PRIMARY KEY,
		Version INT NOT NULL,
		Timestamp DATETIME2 DEFAULT GETDATE()
	)
	INSERT INTO Databaseversion (Version) VALUES (0)
END

DECLARE @currentVersion INT
SELECT @currentVersion = MAX(Version) FROM DatabaseVersion

DECLARE @changeVersion INT

PRINT 'Version when starting script: ' 
PRINT @currentVersion

--TEMPLATE:
/*
-------------------------------------------------
SELECT @changeVersion = [replace with version number]
IF @currentVersion < @changeVersion 
BEGIN
	/*
	INSERT SQL HERE!
	*/
	INSERT INTO DatabaseVersion (Version) VALUES (@changeVersion)
END
--------------------------------------------------	
*/
	
-------------------------------------------------
SELECT @changeVersion = 1
IF @currentVersion < @changeVersion 
BEGIN
	CREATE TABLE Attendance (
		[intAttendanceId] [int] IDENTITY (1,1) PRIMARY KEY NOT NULL,
		[varName] varchar(100) NULL,
		[dtmAttendanceDate] datetime NOT NULL,
		[intDivisionId] [int] NULL,
		[blnConfirmed] [bit] NULL
		)

	CREATE TABLE MemberAttendance (
		[intAttendanceId] [int] NOT NULL,
		[intMemberId] [int] NOT NULL,
		PRIMARY KEY CLUSTERED 
(
	[intAttendanceId] ASC,
	[intMemberId] ASC))

	ALTER TABLE Member ADD CONSTRAINT PK_MemberId PRIMARY KEY CLUSTERED (intMemberID)

	ALTER TABLE [dbo].[MemberAttendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_MemberAttendance] FOREIGN KEY([intAttendanceId])
	REFERENCES [dbo].[Attendance] ([intAttendanceId])
	ALTER TABLE [dbo].[MemberAttendance]  WITH CHECK ADD  CONSTRAINT [FK_Member_MemberAttendance] FOREIGN KEY([intMemberId])
	REFERENCES [dbo].[Member] ([intMemberID])

	INSERT INTO DatabaseVersion (Version) VALUES (@changeVersion)
END
--------------------------------------------------
-------------------------------------------------
SELECT @changeVersion = 2
IF @currentVersion < @changeVersion 
BEGIN
	ALTER TABLE MemberAttendance ADD [blnAttend] bit NULL
		
	INSERT INTO DatabaseVersion (Version) VALUES (@changeVersion)
END
--------------------------------------------------	


SELECT @currentVersion = MAX(Version) FROM DatabaseVersion

PRINT 'Version after completing script: ' 
PRINT @currentVersion

