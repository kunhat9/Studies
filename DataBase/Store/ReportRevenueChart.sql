USE DB_STUDIES
GO
IF OBJECT_ID('ReportRevenueChart', 'P') IS NOT NULL
DROP PROC ReportRevenueChart
GO
CREATE PROCEDURE ReportRevenueChart
(
	@startDate VARCHAR(50)
	,@endDate VARCHAR(50)
	, @type VARCHAR(50)
) AS
BEGIN
	DECLARE @to date, @from date
	IF(@startDate = '' OR @startDate IS NULL) SET @startDate = NULL
	IF(@endDate = '' OR @endDate IS NULL) SET @endDate = NULL
	IF @type ='DAY'
		BEGIN
		IF (@endDate IS NULL OR CONVERT(date, @endDate) > GETDATE()) SET @to = GETDATE()
		ELSE SET @to = (CONVERT(date, @endDate))
		IF(@startDate IS NULL OR @startDate > @to) SET @from = (select DATEADD(DAY, -29, @to))
		ELSE SET @from = (CONVERT(date, @startDate))
		;WITH CTE AS
		(
			SELECT CONVERT(date, @to) sDate
			UNION ALL
			SELECT DATEADD(DAY, -1, sDate)
			FROM CTE
			WHERE sDate > @from
		),obj AS 
		(
			SELECT COALESCE (FORMAT(CTE.sDate,'dd/MM/yyyy'),FORMAT(tbTemp.NgayLamViec,'dd/MM/yyyy')) NgayLamViec , ISNULL(tbTemp.Thu,0.00) Thu , ISNULL(tbTemp.Chi,0.00) Chi FROM 
			(
			SELECT SUM(tbChi.Thu) Chi , SUM(tbThu.Chi) Thu, tbChi.TrackingDate NgayLamViec FROM 
			(SELECT 
					(u.UserNumberSalary*SUM(s.[SchedulePrice])) Chi
				  , s.ScheduleUserId
				  , t.TrackingDate
			  FROM [DB_STUDIES].[dbo].[TB_SCHEDULES] s
			  JOIN TB_TRACKINGS t
			  ON s.ScheduleId = t.TrackingScheduleId
			  JOIN TB_USERS u ON u.UserId = s.ScheduleUserId
   
			  GROUP BY s.ScheduleUserId , u.UserNumberSalary, t.TrackingDate
			  ) tbThu
			JOIN 
			(SELECT 
					(SUM(s.[SchedulePrice])) Thu
				  , s.ScheduleUserId
				  , t.TrackingDate
			  FROM [DB_STUDIES].[dbo].[TB_SCHEDULES] s
			  JOIN TB_TRACKINGS t
			  ON s.ScheduleId = t.TrackingScheduleId
			  JOIN TB_USERS u ON u.UserId = s.ScheduleUserId
			  GROUP BY s.ScheduleUserId , u.UserNumberSalary, t.TrackingDate) tbChi
			ON tbThu.ScheduleUserId = tbChi.ScheduleUserId
			GROUP BY tbChi.TrackingDate
			) tbTemp
			RIGHT JOIN CTE 
			ON CONVERT(DATE,CTE.sDate) = CONVERT(DATE,tbTemp.NgayLamViec)
		)
		SELECT * FROM obj
		END
	ELSE IF @type ='MONTH'
		BEGIN
			IF (@endDate IS NULL OR CONVERT(date, @endDate) > GETDATE()) SET @to = GETDATE()
			ELSE SET @to = (CONVERT(date, @endDate))
			IF(@startDate IS NULL OR @startDate > @to) SET @from = (select DATEADD(MONTH, -5, @to))
			ELSE SET @from = (CONVERT(date, @startDate))
			;WITH CTE AS
		(
			SELECT CONVERT(date, @to) sDate
			UNION ALL
			SELECT DATEADD(MONTH, -1, sDate)
			FROM CTE
			WHERE sDate > @from
		)
		,obj AS 
		(
			SELECT COALESCE(tbTemp.NgayLamViec,FORMAT(CTE.sDate,'MM/yyyy')) NgayLamViec ,ISNULL(SUM(tbTemp.Thu),0.00) Thu , ISNULL(SUM(tbTemp.Chi),0.00) Chi FROM 
			(
			SELECT SUM(tbChi.Thu) Chi , SUM(tbThu.Chi) Thu, FORMAT(tbChi.TrackingDate,'MM/yyyy') NgayLamViec FROM 
			(SELECT 
					(u.UserNumberSalary*SUM(s.[SchedulePrice])) Chi
				  , s.ScheduleUserId
				  , t.TrackingDate
			  FROM [DB_STUDIES].[dbo].[TB_SCHEDULES] s
			  JOIN TB_TRACKINGS t
			  ON s.ScheduleId = t.TrackingScheduleId
			  JOIN TB_USERS u ON u.UserId = s.ScheduleUserId
   
			  GROUP BY s.ScheduleUserId , u.UserNumberSalary, t.TrackingDate
			  ) tbThu
			JOIN 
			(SELECT 
					(SUM(s.[SchedulePrice])) Thu
				  , s.ScheduleUserId
				  , t.TrackingDate
			  FROM [DB_STUDIES].[dbo].[TB_SCHEDULES] s
			  JOIN TB_TRACKINGS t
			  ON s.ScheduleId = t.TrackingScheduleId
			  JOIN TB_USERS u ON u.UserId = s.ScheduleUserId
			  GROUP BY s.ScheduleUserId , u.UserNumberSalary, t.TrackingDate) tbChi
			ON tbThu.ScheduleUserId = tbChi.ScheduleUserId
			GROUP BY FORMAT(tbChi.TrackingDate,'MM/yyyy')
			) tbTemp
			RIGHT JOIN CTE 
			ON FORMAT(CTE.sDate,'MM/yyyy') = tbTemp.NgayLamViec
			GROUP BY FORMAT(CTE.sDate,'MM/yyyy'),tbTemp.NgayLamViec
		)
		SELECT * FROM obj
		END
	ELSE IF @type ='YEAR'
		BEGIN
			IF (@endDate IS NULL OR CONVERT(date, @endDate) > GETDATE()) SET @to = GETDATE()
			ELSE SET @to = (CONVERT(date, @endDate))
			IF(@startDate IS NULL OR @startDate > @to) SET @from = (select DATEADD(YEAR, -5, @to))
			ELSE SET @from = (CONVERT(date, @startDate))
			;WITH CTE AS
			(
				SELECT CONVERT(date, @to) sDate
				UNION ALL
				SELECT DATEADD(YEAR, -1, sDate)
				FROM CTE
				WHERE sDate > @from
			),obj AS
			(
			SELECT COALESCE(tbTemp.NgayLamViec,FORMAT(CTE.sDate,'yyyy')) NgayLamViec ,ISNULL(SUM(tbTemp.Thu),0.00) Thu , ISNULL(SUM(tbTemp.Chi),0.00) Chi FROM 
			(
			SELECT SUM(tbChi.Thu) Chi , SUM(tbThu.Chi) Thu, FORMAT(tbChi.TrackingDate,'yyyy') NgayLamViec FROM 
			(SELECT 
					(u.UserNumberSalary*SUM(s.[SchedulePrice])) Chi
				  , s.ScheduleUserId
				  , t.TrackingDate
			  FROM [DB_STUDIES].[dbo].[TB_SCHEDULES] s
			  JOIN TB_TRACKINGS t
			  ON s.ScheduleId = t.TrackingScheduleId
			  JOIN TB_USERS u ON u.UserId = s.ScheduleUserId
   
			  GROUP BY s.ScheduleUserId , u.UserNumberSalary, t.TrackingDate
			  ) tbThu
			JOIN 
			(SELECT 
					(SUM(s.[SchedulePrice])) Thu
				  , s.ScheduleUserId
				  , t.TrackingDate
			  FROM [DB_STUDIES].[dbo].[TB_SCHEDULES] s
			  JOIN TB_TRACKINGS t
			  ON s.ScheduleId = t.TrackingScheduleId
			  JOIN TB_USERS u ON u.UserId = s.ScheduleUserId
			  GROUP BY s.ScheduleUserId , u.UserNumberSalary, t.TrackingDate) tbChi
			ON tbThu.ScheduleUserId = tbChi.ScheduleUserId
			GROUP BY FORMAT(tbChi.TrackingDate,'yyyy')
			) tbTemp
			RIGHT JOIN CTE 
			ON FORMAT(CTE.sDate,'yyyy') = tbTemp.NgayLamViec
			GROUP BY FORMAT(CTE.sDate,'yyyy'),tbTemp.NgayLamViec
		)
		SELECT * FROM obj
		END
END