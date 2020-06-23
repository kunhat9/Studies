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
	SET @totalRevenue = (SELECT SUM(( tbChi.Thu - tbThu.Chi)) TotalRevenue FROM 
							(SELECT 
									(u.UserNumberSalary*SUM(s.[SchedulePrice])) Chi
								  , s.ScheduleUserId
							  FROM [DB_STUDIES].[dbo].[TB_SCHEDULES] s
							  JOIN TB_TRACKINGS t
							  ON s.ScheduleId = t.TrackingScheduleId
							  JOIN TB_USERS u ON u.UserId = s.ScheduleUserId
							   WHERE MONTH(GETDATE()) = MONTH(CONVERT(DATE,t.TrackingDate)) AND YEAR(GETDATE()) = YEAR(CONVERT(DATE,t.TrackingDate))
							  GROUP BY s.ScheduleUserId , u.UserNumberSalary
							  ) tbThu
							JOIN 
							(SELECT 
									(SUM(s.[SchedulePrice])) Thu
								  , s.ScheduleUserId
							  FROM [DB_STUDIES].[dbo].[TB_SCHEDULES] s
							  JOIN TB_TRACKINGS t
							  ON s.ScheduleId = t.TrackingScheduleId
							  JOIN TB_USERS u ON u.UserId = s.ScheduleUserId
							  WHERE MONTH(GETDATE()) = MONTH(CONVERT(DATE,t.TrackingDate)) AND YEAR(GETDATE()) = YEAR(CONVERT(DATE,t.TrackingDate))
							  GROUP BY s.ScheduleUserId , u.UserNumberSalary) tbChi
							ON tbThu.ScheduleUserId = tbChi.ScheduleUserId)
		SELECT @countNewClass CountNewClass , @countNewStudies CountNewStudies , @countNewTeacher CountNewTeacher, @totalRevenue TotalRevenue
END