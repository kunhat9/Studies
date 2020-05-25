USE DB_STUDIES
GO
IF OBJECT_ID('InsertOrUpdateSubjectToBox', 'P') IS NOT NULL
DROP PROC InsertOrUpdateSubjectToBox
GO
CREATE PROCEDURE InsertOrUpdateSubjectToBox
(
	@boxId nvarchar(50),
	@listSubjectId varchar(500)
	
) AS
BEGIN
	CREATE TABLE #ListSubjectId(
	SubjectId INT
	)
	DECLARE @ecode VARCHAR(50), @edesc VARCHAR(50)
	INSERT INTO #ListSubjectId
	SELECT * FROM FN_SplitStringToTable(@listSubjectId,',')

	SET XACT_ABORT ON
	BEGIN TRAN
	BEGIN TRY
		DECLARE @count INT = (SELECT COUNT(1) FROM TB_BOX_SUBJECTS WHERE BoxSubjectBoxId = @boxId)
		IF @count >0 
			BEGIN
			 -- NEU MA CO ROI THY XOA DI LAM LAI
			 DELETE TB_BOX_SUBJECTS WHERE BoxSubjectBoxId = @boxId
			 INSERT INTO TB_BOX_SUBJECTS(BoxSubjectBoxId,BoxSubjectSubjectId)
			 SELECT @boxId BoxSubjectBoxId, * FROM #ListSubjectId
		 
			END
		ELSE
			BEGIN
				INSERT INTO TB_BOX_SUBJECTS(BoxSubjectBoxId,BoxSubjectSubjectId)
				SELECT @boxId BoxSubjectBoxId, * FROM #ListSubjectId

			END
		SET @ecode = '00'
		SET @edesc ='Suscess'
	COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
		SET @ecode = '00'
		SET @edesc =ERROR_MESSAGE()
	END CATCH
	SELECT @ecode ECODE , @edesc EDESC 
	DROP TABLE #ListSubjectId
END