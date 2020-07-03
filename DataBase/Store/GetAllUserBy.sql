USE DB_STUDIES
GO
IF OBJECT_ID('GetAllUserBy', 'P') IS NOT NULL
DROP PROC GetAllUserBy
GO
CREATE PROCEDURE GetAllUserBy
(
	@keyText nvarchar(max)
	,@status nvarchar(50)
	,@scheduleId varchar(50)
	,@type nvarchar(50)
	,@pageNumber int
	,@pageSize int
) AS
BEGIN
	IF (@keyText = '' OR @keyText IS NULL) SET @keyText = ''
	IF (@status = '' OR @status IS NULL) SET @status = NULL
	IF (@scheduleId = '' OR @scheduleId IS NULL) SET @scheduleId = NULL
	
	DECLARE @start int, @end int
	SET @start = (((@pageNumber - 1) * @pageSize) + 1)
	SET @end = (@start + @pageSize - 1)
	CREATE TABLE #TempUserId
	(
		[ROW] int IDENTITY(1,1) PRIMARY KEY
		,UserID nvarchar(50)
	)
	IF @type ='TEACHER'
		BEGIN
			INSERT INTO #TempUserId(UserID)
			SELECT UserId
			FROM TB_USERS
			WHERE (UserFullName LIKE N'%' + @keyText + '%'
					OR UserAddress LIKE N'%' + @keyText + '%'
					OR UserPhone LIKE N'%' + @keyText + '%'
					OR UserNote LIKE N'%' + @keyText + '%'
					)
				AND (@status IS NULL OR UserStatus = @status)
				AND (@type IS NULL OR UserType = @type)
		END
	ELSE IF @type ='STUDIES'
		BEGIN
			INSERT INTO #TempUserId(UserID)
			SELECT UserId
			FROM TB_USERS
			LEFT JOIN TB_CLASSES 
			ON TB_USERS.UserId = TB_CLASSES.ClassUserId
			WHERE (UserFullName LIKE N'%' + @keyText + '%'
					OR UserAddress LIKE N'%' + @keyText + '%'
					OR UserPhone LIKE N'%' + @keyText + '%'
					OR UserNote LIKE N'%' + @keyText + '%'
					)
				AND (@status IS NULL OR UserStatus = @status)
				AND (@type IS NULL OR UserType = @type)
				AND (@scheduleId IS NULL OR TB_CLASSES.ClassScheduleId = @scheduleId)
		END
	SELECT DISTINCT [UserId]
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
	FROM [dbo].[TB_USERS]
	WHERE [UserId] IN
		(SELECT [UserId]
		FROM #TempUserId
		WHERE @start <= [ROW]
			AND [ROW] <= @end)
	ORDER BY [UserId]

	SELECT ISNULL(COUNT(1), 0) FROM #TempUserId

	DROP TABLE #TempUserId
END