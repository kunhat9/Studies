USE DB_STUDIES
GO
IF OBJECT_ID('GetClassBy', 'P') IS NOT NULL
DROP PROC GetClassBy
GO
CREATE PROCEDURE GetClassBy
(
	@keyText nvarchar(max)
	,@boxid varchar(50)
	,@subjectId varchar(50)
	,@timeIn varchar(50)
	,@timeEnd varchar(50)
	,@status varchar(50)
	,@pageNumber int
	,@pageSize int
) AS
BEGIN
	IF (@keyText = '' OR @keyText IS NULL) SET @keyText = ''
	IF (@boxid = '' OR @boxid IS NULL) SET @boxid = NULL
	IF (@subjectId = '' OR @subjectId IS NULL) SET @subjectId = NULL
	IF (@timeIn = '' OR @timeIn IS NULL) SET @timeIn = NULL
	IF (@timeEnd = '' OR @timeEnd IS NULL) SET @timeEnd = NULL
	IF (@status = '' OR @status IS NULL) SET @status = NULL

	DECLARE @start int, @end int
	SET @start = (((@pageNumber - 1) * @pageSize) + 1)
	SET @end = (@start + @pageSize - 1)
	CREATE TABLE #TempClassId
	(
		[ROW] int IDENTITY(1,1) PRIMARY KEY
		,ScheduleId nvarchar(50)
	)
	INSERT INTO #TempClassId(ScheduleId)
	SELECT ScheduleId
	FROM TB_SCHEDULES s
	JOIN TB_SCHEDULE_DETAILS d
	ON s.ScheduleId = d.ScheduleDetailScheduleId
	JOIN TB_USERS u
	ON u.UserId = s.ScheduleUserId
	WHERE (s.ScheduleCode LIKE N'%' + @keyText + '%'
			OR u.UserName LIKE N'%' + @keyText + '%'
			OR u.UserFullName LIKE N'%' + @keyText + '%'
			)
		AND (@status IS NULL OR s.ScheduleStatus = @status)
		AND (@timeIn IS NULL OR  CONVERT(time, @timeIn) <= d.ScheduleDetailTimeTo)
		AND (@timeEnd IS NULL OR  d.ScheduleDetailTimeFrom <= CONVERT(time, @timeEnd))
		AND s.ScheduleIdBoxSubjectId IN (
			SELECT BoxSubjectId FROM TB_BOX_SUBJECTS
				WHERE  (@boxid IS NULL OR BoxSubjectBoxId = @boxid)
				AND (@subjectId IS NULL OR BoxSubjectSubjectId = @subjectId)
		)
		
	SELECT [ScheduleId]
      ,[ScheduleCode]
      ,[ScheduleDateCreate]
      ,[ScheduleStatus]
      ,[ScheduleDateBegin]
      ,[ScheduleDateEnd]
      ,[SchedulePrice]
      ,[ScheduleIdBoxSubjectId]
      ,[ScheduleUserId]
	  ,[ScheduleFileId]
	  ,[UserName]
	  ,[UserFullName]
	  ,[ScheduleDetailId]
      ,[ScheduleDetailDayOfWeek]
      ,[ScheduleDetailTimeFrom]
      ,[ScheduleDetailTimeTo]
      ,[ScheduleDetailNote]
	FROM TB_SCHEDULES s
	JOIN TB_SCHEDULE_DETAILS d
	ON s.ScheduleId = d.ScheduleDetailScheduleId
	JOIN TB_USERS u
	ON u.UserId = s.ScheduleUserId
	WHERE ScheduleId IN
		(SELECT ScheduleId
		FROM #TempClassId
		WHERE @start <= [ROW]
			AND [ROW] <= @end)
	ORDER BY ScheduleId

	SELECT ISNULL(COUNT(1), 0) FROM #TempClassId

	DROP TABLE #TempClassId
END