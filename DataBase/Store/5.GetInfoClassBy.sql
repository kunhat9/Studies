USE DB_STUDIES
GO
IF OBJECT_ID('GetInfoClassBy', 'P') IS NOT NULL
DROP PROC GetInfoClassBy
GO
CREATE PROCEDURE GetInfoClassBy
(
	@userId nvarchar(max)
	,@userType nvarchar(50)
	,@pageNumber int
	,@pageSize int
) AS
BEGIN
	IF @userId ='' OR @userId IS NULL SET @userId =''
	DECLARE @start int, @end int
	SET @start = (((@pageNumber - 1) * @pageSize) + 1)
	SET @end = (@start + @pageSize - 1)

	CREATE TABLE #Temp
	(
		[ROW] int IDENTITY(1,1) PRIMARY KEY
		,ScheduleId nvarchar(50)
	)
	IF @userType ='TEACHER'
		BEGIN
			INSERT INTO #Temp(ScheduleId)
			SELECT ScheduleId
			FROM TB_SCHEDULES s
			JOIN [dbo].[TB_SCHEDULE_DETAILS] d
			ON s.ScheduleId = d.ScheduleDetailScheduleId
			WHERE @userId ='' OR s.ScheduleUserId = @userId
		END
	ELSE IF @userType ='STUDIES'
		BEGIN
			INSERT INTO #Temp(ScheduleId)
			SELECT ScheduleId
			FROM TB_SCHEDULES s
			JOIN [dbo].[TB_SCHEDULE_DETAILS] d
			ON s.ScheduleId = d.ScheduleDetailScheduleId
			JOIN [dbo].[TB_CLASSES] c
			ON c.ClassScheduleId = s.ScheduleId 
			WHERE ScheduleId IN (
				SELECT ClassScheduleId FROM [dbo].[TB_CLASSES]
				WHERE @userId ='' OR ClassUserId = @userId
			) AND  @userId =''  OR c.ClassUserId = @userId
		END
	SELECT [ScheduleId]
      ,[ScheduleCode]
      ,[ScheduleDateCreate]
      ,[ScheduleStatus]
      ,[ScheduleDateBegin]
      ,[ScheduleDateEnd]
      ,[SchedulePrice]
      ,[ScheduleIdBoxSubjectId]
      ,[ScheduleUserId]
	  ,[ScheduleDetailId]
      ,[ScheduleDetailDayOfWeek]
      ,[ScheduleDetailTimeFrom]
      ,[ScheduleDetailTimeTo]
      ,[ScheduleDetailNote]
      ,[ScheduleDetailScheduleId]
	FROM [dbo].[TB_SCHEDULES] s
	JOIN [dbo].[TB_SCHEDULE_DETAILS] d
	ON s.ScheduleId = d.ScheduleDetailScheduleId
	WHERE s.[ScheduleId] IN
		(SELECT ScheduleId
		FROM #Temp
		WHERE @start <= [ROW]
			AND [ROW] <= @end)
	ORDER BY ScheduleId
	SELECT ISNULL(COUNT(1), 0) FROM #Temp

	DROP TABLE #Temp
END