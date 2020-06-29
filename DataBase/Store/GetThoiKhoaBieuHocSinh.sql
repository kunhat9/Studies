USE DB_STUDIES
GO
IF OBJECT_ID('GetThoiKhoaBieuHocSinh', 'P') IS NOT NULL
DROP PROC GetThoiKhoaBieuHocSinh
GO
CREATE PROCEDURE GetThoiKhoaBieuHocSinh
(
	@userId nvarchar(50)

	
) AS
BEGIN
	SELECT u.UserId, u.UserFullName, u.UserName , u.UserStatus, d.ScheduleDetailDayOfWeek, d.ScheduleDetailTimeFrom, d.ScheduleDetailTimeTo 
	FROM TB_USERS u
	JOIN TB_CLASSES c ON u.UserId = c.ClassUserId
	JOIN TB_SCHEDULES s ON s.ScheduleId = c.ClassScheduleId
	JOIN TB_SCHEDULE_DETAILS d ON d.ScheduleDetailScheduleId = s.ScheduleId
	WHERE u.UserId = @userId
END
