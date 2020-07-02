USE DB_STUDIES
GO
IF OBJECT_ID('GetUserTracked', 'P') IS NOT NULL
DROP PROC GetUserTracked
GO
CREATE PROCEDURE GetUserTracked
(
	@scheduleId varchar(50)
	,@dateCreated varchar(50)
	,@pageNumber int
	,@pageSize int
) AS
BEGIN
	IF (@scheduleId = '' OR @scheduleId IS NULL) SET @scheduleId = NULL
	
	DECLARE @start int, @end int
	SET @start = (((@pageNumber - 1) * @pageSize) + 1)
	SET @end = (@start + @pageSize - 1)
	CREATE TABLE #TempUserId
	(
		[ROW] int IDENTITY(1,1) PRIMARY KEY
		,UserID nvarchar(50)
	)
			INSERT INTO #TempUserId(UserID)
			SELECT UserId
			FROM TB_USERS u
			JOIN TB_TRACKINGS t
			on u.UserId = t.TrackingUserId
			WHERE t.TrackingScheduleId = @scheduleId AND CONVERT(DATE,T.TrackingDate) = CONVERT(DATE,@dateCreated)
	SELECT distinct [UserId]
      ,[UserName]
      ,[UserFullName]
      ,[UserDateCreated]
	  ,c.ClassDateCreated
	FROM [dbo].[TB_USERS] u 
	JOIN TB_CLASSES  c
	on u.UserId = c.ClassUserId
	WHERE [UserId] IN
		(SELECT [UserId]
		FROM #TempUserId
		WHERE @start <= [ROW]
			AND [ROW] <= @end)
	ORDER BY [UserId]

	SELECT ISNULL(COUNT(1), 0) FROM #TempUserId

	DROP TABLE #TempUserId
END