USE DB_STUDIES
GO
IF OBJECT_ID('InsertPointToClass', 'P') IS NOT NULL
DROP PROC InsertPointToClass
GO
CREATE PROCEDURE InsertPointToClass
(
	@schedulesId NVARCHAR(50)
	,@xmlSchedule NVARCHAR(MAX)
	, @type VARCHAR(50)
) AS
BEGIN
BEGIN TRAN
BEGIN TRY
   DECLARE @ecode VARCHAR(50)
   -- detect xml to table 
   CREATE TABLE #TempPoint(
		UserId	int
		,PointNumber int
	)
	DECLARE @XML XML
	DECLARE @handle INT  
	DECLARE @PrepareXmlStatus INT
	SET @XML = @xmlSchedule
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @XML
	INSERT INTO #TempPoint 
	SELECT  *
	FROM    OPENXML(@handle, '/row/row', 2)  
    WITH (
			UserId	int
		,PointNumber int
    )
	INSERT INTO TB_POINTS(PointNumber,PointClassId,PointType)
	SELECT c.ClassId,tp.PointNumber,@type PointType FROM TB_CLASSES c
	JOIN #TempPoint tp ON c.ClassUserId = tp.UserId
	WHERE c.ClassScheduleId = @schedulesId
	
	
COMMIT
END TRY
BEGIN CATCH
   ROLLBACK
   SET @ecode = '999'
   SELECT '999' ecode, ERROR_MESSAGE() edesc
END CATCH
END