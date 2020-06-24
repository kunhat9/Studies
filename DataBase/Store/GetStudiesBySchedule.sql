USE DB_STUDIES
GO
IF OBJECT_ID('GetStudiesBySchedule', 'P') IS NOT NULL
DROP PROC GetStudiesBySchedule
GO
CREATE PROCEDURE GetStudiesBySchedule
(
	@scheduleId nvarchar(50)
	,@pageNumber int
	, @pageSize int 
) AS
BEGIN
	DECLARE @start int, @end int
	SET @start = (((@pageNumber - 1) * @pageSize) + 1)
	SET @end = (@start + @pageSize - 1)
	CREATE TABLE #Temp
	(
		[ROW] int IDENTITY(1,1) PRIMARY KEY
		,UserId nvarchar(50)
	)
	INSERT INTO #Temp(UserId)
	SELECT UserId
	FROM TB_USERS
	WHERE UserId IN (
		SELECT ClassUserId FROM [dbo].[TB_CLASSES]
		WHERE ClassScheduleId = @scheduleId
	)

	SELECT [UserId]
      ,[UserName]
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
      ,[UserDateCreated]
	FROM [dbo].TB_USERS
	WHERE [UserId] IN
		(SELECT [UserId]
		FROM #Temp
		WHERE @start <= [ROW]
			AND [ROW] <= @end)
	ORDER BY [UserId]
	SELECT ISNULL(COUNT(1), 0) FROM #Temp


	DROP TABLE #Temp
END