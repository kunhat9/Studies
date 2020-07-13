USE DB_STUDIES
GO
IF OBJECT_ID('InsertStudiesInClass', 'P') IS NOT NULL
DROP PROC InsertStudiesInClass
GO
CREATE PROCEDURE InsertStudiesInClass
(
	@scheduleId nvarchar(50),
	@listUserId varchar(500),
	@type VARCHAR(50)
	
) AS
BEGIN
	CREATE TABLE #ListUserId(
	UserId INT
	)
	DECLARE @ecode VARCHAR(50), @edesc VARCHAR(50)
	INSERT INTO #ListUserId
	SELECT * FROM FN_SplitStringToTable(@listUserId,',')
	SET XACT_ABORT ON
	BEGIN TRAN
	BEGIN TRY
		DECLARE @count INT = (SELECT COUNT(1) FROM TB_CLASSES WHERE ClassScheduleId = @scheduleId)
		IF @type = 'INSERT' 
			BEGIN
			 -- NEU MA CO ROI THY XOA DI LAM LAI
			 IF @count >= 20
				BEGIN
					SET @ecode = '150'
					SET @edesc ='Lop hoc da du so luong'
				END
			ELSE
				BEGIN
					 INSERT INTO TB_CLASSES(ClassScheduleId,ClassUserId)
						SELECT @scheduleId ClassScheduleId, * FROM #ListUserId
					SET @ecode = '00'
					SET @edesc ='Suscess'
				END
			
		 
			END
		ELSE IF @type ='DELETE'
			BEGIN
				 DELETE TB_CLASSES WHERE ClassUserId IN (SELECT UserId FROM #ListUserId) AND ClassScheduleId = @scheduleId
				 SET @ecode = '00'
				SET @edesc ='Suscess'
			END
		ELSE
			BEGIN
				SET @ecode = '200'
				SET @edesc ='Ma truyen vào k dung'
			END
		
	COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
		SET @ecode = '100'
		SET @edesc =ERROR_MESSAGE()
	END CATCH
	SELECT @ecode ECODE , @edesc EDESC 
	DROP TABLE #ListUserId
END