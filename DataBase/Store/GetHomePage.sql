USE DB_STUDIES
GO
IF OBJECT_ID('GetHomePage', 'P') IS NOT NULL
DROP PROC GetHomePage
GO
CREATE PROCEDURE GetHomePage
AS
BEGIN
	DECLARE @timeSpan INT = 7
	DECLARE @countNewStudies INT , @countNewTeacher INT, @countNewClass INT, @totalRevenue decimal(18,0)
	
	SET @countNewTeacher = (SELECT COUNT(1) FROM TB_USERS WHERE DATEDIFF(day,CONVERT(DATE,UserDateCreated),CONVERT(DATE,GETDATE())) >=@timeSpan AND UserType ='TEACHER' AND UserStatus ='A')
	SET @countNewStudies = (SELECT COUNT(1) FROM TB_USERS WHERE DATEDIFF(day,CONVERT(DATE,UserDateCreated),CONVERT(DATE,GETDATE())) >=@timeSpan AND UserType ='STUDIES' AND UserStatus ='A')
	SET @countNewClass = (SELECT COUNT(1) FROM TB_SCHEDULES WHERE DATEDIFF(day,CONVERT(DATE,ScheduleDateCreate),CONVERT(DATE,GETDATE())) >=@timeSpan AND ScheduleStatus ='A')
	SET @totalRevenue = (			
SELECT SUM(tbThu.Thu -tbChi.Chi) TotalRevenue FROM
(
SELECT				
				SUM(s.SchedulePrice) Thu
				   ,s.ScheduleUserId
				  , t.TrackingDate
			  FROM [DB_STUDIES].[dbo].[TB_SCHEDULES] s
			  JOIN TB_TRACKINGS t
			  ON s.ScheduleId = t.TrackingScheduleId
			  JOIN TB_USERS u ON u.UserId = s.ScheduleUserId
			  WHERE MONTH(GETDATE()) = MONTH(CONVERT(DATE,t.TrackingDate)) AND YEAR(GETDATE()) = YEAR(CONVERT(DATE,t.TrackingDate))
			GROUP BY s.ScheduleUserId,t.TrackingDate) tbThu

FULL JOIN
			(
			SELECT				
			(0.7*SUM(s.SchedulePrice)) Chi
							   ,s.ScheduleUserId
							  , t.TrackingDate
						  FROM [DB_STUDIES].[dbo].[TB_SCHEDULES] s
						  JOIN TB_TRACKINGS t
						  ON s.ScheduleId = t.TrackingScheduleId
						  JOIN TB_USERS u ON u.UserId = s.ScheduleUserId
						  WHERE MONTH(GETDATE()) = MONTH(CONVERT(DATE,t.TrackingDate)) AND YEAR(GETDATE()) = YEAR(CONVERT(DATE,t.TrackingDate))
						  GROUP BY s.ScheduleUserId,t.TrackingDate) tbChi
			ON CONVERT(DATE,tbThu.TrackingDate) = CONVERT(DATE,tbChi.TrackingDate))
		SELECT @countNewClass CountNewClass , @countNewStudies CountNewStudies , @countNewTeacher CountNewTeacher, @totalRevenue TotalRevenue
END