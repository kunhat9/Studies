USE DB_STUDIES
GO
IF OBJECT_ID('InsertTeacherAndSchedule', 'P') IS NOT NULL
DROP PROC InsertTeacherAndSchedule
GO
CREATE PROCEDURE InsertTeacherAndSchedule
(
	@xmlUser NVARCHAR(MAX)
	,@xmlSchedule NVARCHAR(MAX)
) AS
BEGIN
BEGIN TRAN
BEGIN TRY
   DECLARE @ecode VARCHAR(50)
   -- detect xml to table 
   CREATE TABLE #TempUser(
		UserId	int
		,UserName	nvarchar(30)
		,UserFullName	nvarchar(255)
		,UserPassword	nvarchar(30)
		,UserType	nvarchar(20)
		,UserPhone	varchar(50)
		,UserEmail	varchar(50)
		,UserAddress	varchar(50)
		,UserAcademicLevel	varchar(50)
		,UserStatus	nvarchar(20)
		,UserNote	nvarchar(100)
		,UserNumberSalary	decimal(18, 2)
	
	)
	CREATE TABLE #TempSchedule(
		TeachingScheduleId	int
		,TeachingScheduleDayOfWeek	varchar(50)
		,TeachingScheduleTimeFrom	time(7)
		,TeachingScheduleTimeTo	time(7)
		,TeachingScheduleNote	nvarchar(MAX)
		,TeachingScheduleUserId	int
	
	)
	DECLARE @XML XML
	DECLARE @handle INT  
	DECLARE @PrepareXmlStatus INT
	SET @XML = @xmlUser
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @XML
	INSERT INTO #TempUser 
	SELECT  *
	FROM    OPENXML(@handle, '/row', 2)  
    WITH (
		UserId	int
		,UserName	nvarchar(30)
		,UserFullName	nvarchar(255)
		,UserPassword	nvarchar(30)
		,UserType	nvarchar(20)
		,UserPhone	varchar(50)
		,UserEmail	varchar(50)
		,UserAddress	varchar(50)
		,UserAcademicLevel	varchar(50)
		,UserStatus	nvarchar(20)
		,UserNote	nvarchar(100)
		,UserNumberSalary	decimal(18, 2)
    )
	SET @XML = @xmlSchedule
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @XML
	INSERT INTO  #TempSchedule 
	SELECT  *
	FROM    OPENXML(@handle, '/row/row', 2)  
    WITH (
		TeachingScheduleId	int
		,TeachingScheduleDayOfWeek	varchar(50)
		,TeachingScheduleTimeFrom	time(7)
		,TeachingScheduleTimeTo	time(7)
		,TeachingScheduleNote	nvarchar(MAX)
		,TeachingScheduleUserId	int
    )
	-- end detect 
	CREATE TABLE #TempUserId(
		UserId INT
	)
	-- kiem tra xem co user chua
	DECLARE @userId INT = (SELECT TOP 1 UserId FROM TB_USERS WHERE UserId IN (SELECT UserId FROM #TempUser))
	IF @userId ='' OR @userId IS NULL
		BEGIN
			-- CHUA CO THY INSERT NO VAO 
			INSERT INTO TB_USERS(
				[UserName]
			  ,[UserFullName]
			  ,[UserPassword]
			  ,[UserType]
			  ,[UserPhone]
			  ,[UserEmail]
			  ,[UserAddress]
			  ,[UserAcademicLevel]
			  ,[UserStatus]
			  ,[UserNote]
			  ,[UserNumberSalary]
			)
			OUTPUT inserted.UserId INTO #TempUserId
			SELECT [UserName]
				  ,[UserFullName]
				  ,[UserPassword]
				  ,[UserType]
				  ,[UserPhone]
				  ,[UserEmail]
				  ,[UserAddress]
				  ,[UserAcademicLevel]
				  ,[UserStatus]
				  ,[UserNote]
				  ,[UserNumberSalary]
			FROM #TempUser
			SET @userId = (SELECT TOP 1 UserId FROM #TempUserId)
		END
	ELSE
		BEGIN
			-- update thong tin user lai
			UPDATE TB_USERS
				SET [UserPassword] =(SELECT TOP 1 [UserPassword] FROM #TempUser)
				  ,[UserType] = (SELECT TOP 1 [UserType] FROM #TempUser)
				  ,[UserPhone] = (SELECT TOP 1 [UserPhone] FROM #TempUser)
				  ,[UserEmail] = (SELECT TOP 1 [UserEmail] FROM #TempUser)
				  ,[UserAddress] = (SELECT TOP 1 [UserAddress] FROM #TempUser)
				  ,[UserAcademicLevel] =(SELECT TOP 1 [UserAcademicLevel] FROM #TempUser)
				  ,[UserStatus] =(SELECT TOP 1 [UserStatus] FROM #TempUser)
				  ,[UserNote] =(SELECT TOP 1 [UserNote] FROM #TempUser)
				  ,[UserNumberSalary] =(SELECT TOP 1 [UserNumberSalary] FROM #TempUser)
			WHERE UserId = @userId
		END
	DELETE FROM TB_TEACHING_SCHEDULES WHERE TeachingScheduleUserId = @userId
	UPDATE #TempSchedule
		SET TeachingScheduleUserId = @userId
	INSERT INTO TB_TEACHING_SCHEDULES(
		[TeachingScheduleDayOfWeek]
      ,[TeachingScheduleTimeFrom]
      ,[TeachingScheduleTimeTo]
      ,[TeachingScheduleNote]
      ,[TeachingScheduleUserId]
	)
	SELECT [TeachingScheduleDayOfWeek]
      ,[TeachingScheduleTimeFrom]
      ,[TeachingScheduleTimeTo]
      ,[TeachingScheduleNote]
      ,[TeachingScheduleUserId] FROM #TempSchedule
	SET @ecode = '00'
	SELECT @ecode ecode , 'suscess' edesc
	DROP TABLE #TempUser
	DROP TABLE #TempSchedule
	DROP TABLE #TempUserId
COMMIT
END TRY
BEGIN CATCH
   ROLLBACK
   SET @ecode = '999'
   SELECT '999' ecode, ERROR_MESSAGE() edesc
END CATCH
END