USE DB_STUDIES
GO
IF OBJECT_ID('InsertOrUpdateSubjectToBox', 'P') IS NOT NULL
DROP PROC InsertOrUpdateSubjectToBox
GO
CREATE PROCEDURE InsertOrUpdateSubjectToBox
(
	@boxId nvarchar(50),
	@listSubjectId varchar(500),
	@boxSubjectId varchar(50),
	@price varchar(50)
	
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
		IF @boxSubjectId IS NULL OR @boxSubjectId =''
		BEGIN
			
			DECLARE @count INT = (SELECT COUNT(1) FROM TB_BOX_SUBJECTS WHERE BoxSubjectBoxId = @boxId)
			IF @count >0 
				BEGIN
				 -- NEU MA CO ROI THY XOA DI LAM LAI
				 --DELETE TB_BOX_SUBJECTS WHERE BoxSubjectBoxId = @boxId
				 INSERT INTO TB_BOX_SUBJECTS(BoxSubjectBoxId,BoxSubjectPrice,BoxSubjectSubjectId)
				 SELECT @boxId BoxSubjectBoxId,@price BoxSubjectPrice, * FROM #ListSubjectId
		 
				END
			ELSE
				BEGIN
					INSERT INTO TB_BOX_SUBJECTS(BoxSubjectBoxId,BoxSubjectPrice,BoxSubjectSubjectId)
				 SELECT @boxId BoxSubjectBoxId,@price BoxSubjectPrice, * FROM #ListSubjectId

				END
		END
		ELSE 
			BEGIN
				
				UPDATE TB_BOX_SUBJECTS 
					SET BoxSubjectBoxId = @boxId,
						BoxSubjectPrice = @price,
						BoxSubjectSubjectId = (SELECT TOP 1 SubjectId FROM #ListSubjectId)
				WHERE BoxSubjectId = @boxSubjectId
			END
		
		SET @ecode = '00'
		SET @edesc ='Suscess'
	COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
		SET @ecode = '100'
		SET @edesc =ERROR_MESSAGE()
	END CATCH
	SELECT @ecode ECODE , @edesc EDESC 
	DROP TABLE #ListSubjectId
END