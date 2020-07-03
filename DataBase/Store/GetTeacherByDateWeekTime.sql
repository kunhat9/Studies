USE DB_STUDIES
GO
IF OBJECT_ID('GetTeacherByDateWeekTime', 'P') IS NOT NULL
DROP PROC GetTeacherByDateWeekTime
GO
CREATE PROCEDURE GetTeacherByDateWeekTime
(
	@dayOfWeek nvarchar(50),
	@timeFrom varchar(50),
	@timeTo varchar(50)
	
	
) AS
BEGIN
	SELECT [UserId]
      ,[UserName]
      ,[UserFullName]
      ,[UserPassword]
      ,[UserType]
      ,[UserPhone]
      ,[UserEmail]
      ,[UserAddress]
      ,[UserAcademicLevel]
      ,[UserStatus]
      ,[UserNote]
      ,[UserNumberSalary]
      ,[UserDateCreated]
	 FROM TB_USERS u
	JOIN TB_TEACHING_SCHEDULES t
	ON u.UserId = t.TeachingScheduleUserId
	WHERE u.UserType='TEACHER'
	AND t.TeachingScheduleDayOfWeek = @dayOfWeek
	AND t.TeachingScheduleTimeFrom < CONVERT(time,@timeTo)
	AND t.TeachingScheduleTimeTo > CONVERT(time,@timeFrom)
END