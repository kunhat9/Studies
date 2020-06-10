USE DB_STUDIES
GO
IF OBJECT_ID('GetSubjectBy', 'P') IS NOT NULL
DROP PROC GetSubjectBy
GO
CREATE PROCEDURE GetSubjectBy
(
	@keyText nvarchar(max)
	,@pageNumber int
	,@pageSize int
) AS
BEGIN
	IF (@keyText = '' OR @keyText IS NULL) SET @keyText = ''
	
	DECLARE @start int, @end int
	SET @start = (((@pageNumber - 1) * @pageSize) + 1)
	SET @end = (@start + @pageSize - 1)
	CREATE TABLE #TempSubjectId
	(
		[ROW] int IDENTITY(1,1) PRIMARY KEY
		,BoxSubjectId nvarchar(50)
	)
	INSERT INTO #TempSubjectId(BoxSubjectId)
	SELECT BoxSubjectId
	FROM TB_BOX_SUBJECTS bs
	JOIN TB_SUBJECTS  s
	ON bs.BoxSubjectId = s.SubjectId
	JOIN TB_BOXES b
	ON bs.BoxSubjectBoxId = b.BoxId
	WHERE (b.BoxCode LIKE N'%' + @keyText + '%'
			OR s.SubjectName LIKE N'%' + @keyText + '%'
			)
		
	SELECT bs.BoxSubjectId, b.BoxId,b.BoxCode, s.SubjectId,s.SubjectName
	FROM TB_BOX_SUBJECTS bs
	JOIN TB_SUBJECTS  s
	ON bs.BoxSubjectId = s.SubjectId
	JOIN TB_BOXES b
	ON bs.BoxSubjectBoxId = b.BoxId
	WHERE BoxSubjectId IN
		(SELECT BoxSubjectId
		FROM #TempSubjectId
		WHERE @start <= [ROW]
			AND [ROW] <= @end)
	ORDER BY BoxSubjectId

	SELECT ISNULL(COUNT(1), 0) FROM #TempSubjectId

	DROP TABLE #TempSubjectId
END