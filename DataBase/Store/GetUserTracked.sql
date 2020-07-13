USE DB_STUDIES
GO
IF OBJECT_ID('GetUserTracked', 'P') IS NOT NULL
DROP PROC GetUserTracked
GO
CREATE PROCEDURE GetUserTracked
(
	@scheduleId varchar(50)
	,@numberMonth varchar(50)
	,@numberDayOfWeek varchar(50)
	,@pageNumber int
	,@pageSize int
) AS
BEGIN
	IF (@scheduleId = '' OR @scheduleId IS NULL) SET @scheduleId = NULL
	CREATE TABLE #TempDate(
		[Date] datetime,
		DayOfTheWeek int ,
		WeekOfTheMonth int
	)
	DECLARE @start int, @end int
	SET @start = (((@pageNumber - 1) * @pageSize) + 1)
	SET @end = (@start + @pageSize - 1)
	
	;WITH X AS
	(
	SELECT TOP (CASE WHEN YEAR(GETDATE()) % 4 = 0 THEN 366 ELSE 365 END)-- to handle leap year 
		  DATEADD(DAY 
				 ,ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) -1
				 , CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '0101' )      
						  DayNumber
	  From master..spt_values
	 ),DatesData AS(
	SELECT DayNumber [Date]
		  ,DATEPART(WEEKDAY,DayNumber) DayOfTheWeek
		  ,DATEDIFF(WEEK, 
				DATEADD(WEEK, 
				   DATEDIFF(WEEK, 0, DATEADD(MONTH, 
							   DATEDIFF(MONTH, 0, DayNumber), 0)), 0)
				  , DayNumber- 1) + 1 WeekOfTheMonth
	FROM X )
	INSERT INTO #TempDate
	SELECT * FROM DatesData
	WHERE DayOfTheWeek   = @numberDayOfWeek
	AND MONTH([Date]) = @numberMonth

	CREATE TABLE #TempUserId
	(
		[ROW] int IDENTITY(1,1) PRIMARY KEY
		,UserID nvarchar(50)
	)
			INSERT INTO #TempUserId(UserID)
			SELECT UserId
			FROM TB_USERS u
			JOIN TB_CLASSES  c
			on u.UserId = c.ClassUserId
			WHERE c.ClassScheduleId = @scheduleId 
			ORDER BY [UserId]
			--SELECT * FROM #TempUserId
	SELECT DISTINCT [UserId]
      ,[UserName]
      ,[UserFullName]
      ,[UserDateCreated]
	  ,c.ClassDateCreated
	  ,t.TrackingDate
	FROM [dbo].[TB_USERS] u 
	JOIN TB_CLASSES  c
	on u.UserId = c.ClassUserId
	FULL JOIN TB_TRACKINGS t
	ON t.TrackingUserId = u.UserId
	WHERE [UserId] IN
		(SELECT [UserId]
		FROM #TempUserId
		WHERE @start <= [ROW]
			AND [ROW] <= @end)
	AND (CONVERT(DATE,T.TrackingDate) IN (SELECT CONVERT(DATE,[Date]) FROM #TempDate ))
	UNION ALL
	SELECT DISTINCT [UserId]
      ,[UserName]
      ,[UserFullName]
      ,[UserDateCreated]
	  ,c.ClassDateCreated
	  ,NULL
	FROM [dbo].[TB_USERS] u 
	JOIN TB_CLASSES  c
	on u.UserId = c.ClassUserId
	WHERE [UserId] IN
		(SELECT [UserId]
		FROM #TempUserId
		WHERE @start <= [ROW]
			AND [ROW] <= @end)


	SELECT ISNULL(COUNT(1), 0) FROM #TempUserId

	DROP TABLE #TempUserId
END