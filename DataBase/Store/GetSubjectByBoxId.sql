USE DB_STUDIES
GO
IF OBJECT_ID('GetSubjectByBoxId', 'P') IS NOT NULL
DROP PROC GetSubjectByBoxId
GO
CREATE PROCEDURE GetSubjectByBoxId
(
	@boxId nvarchar(max)
	
) AS
BEGIN
	SELECT SubjectId
      ,SubjectName
	FROM TB_SUBJECTS
	WHERE SubjectId IN (
		SELECT BoxSubjectSubjectId
			FROM TB_BOX_SUBJECTS
			WHERE BoxSubjectBoxId = @boxId
	)
END