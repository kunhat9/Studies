USE DB_STUDIES
GO
IF OBJECT_ID('GetTransactionBy', 'P') IS NOT NULL
DROP PROC GetTransactionBy
GO
CREATE PROCEDURE GetTransactionBy
(
	@userId nvarchar(max)
	,@startDate nvarchar(50)
	,@endDate nvarchar(50)
	,@type varchar(50)
) AS
BEGIN
	IF @userId ='' OR @userId IS NULL SET @userId =NULL
	IF @startDate ='' OR @startDate IS NULL SET @startDate= NULL
	IF @endDate ='' OR @endDate IS NULL SET @endDate =NULL


	SELECT  [TransId]
      ,[TransNumber]
      ,[TransType]
      ,[TransUserId]
      ,[TransNote]
      ,[TransDateCreated]
      ,[TransBeginTime]
      ,[TransEndTime]
  FROM [dbo].[TB_TRANSACTION]
  WHERE TransType = @type
    AND (@userId IS NULL OR [TransUserId] = @userId )
	AND (@startDate IS NULL OR CONVERT(DATE,TransBeginTime) <= CONVERT(DATE,@startDate))
	AND (@endDate IS NULL OR CONVERT(DATE,@endDate) <= CONVERT(DATE,TransEndTime))
END