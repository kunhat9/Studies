USE DB_STUDIES
GO
IF OBJECT_ID('GetCountStudiesInClass', 'P') IS NOT NULL
DROP PROC GetCountStudiesInClass
GO
CREATE PROCEDURE GetCountStudiesInClass
(
@scheduleId varchar(50)
)
AS
BEGIN
IF @scheduleId ='' OR @scheduleId IS NULL SET @scheduleId = NULL
SELECT s.ScheduleId, COUNT(ClassUserId) CountStudie FROM TB_CLASSES c
RIGHT JOIN TB_SCHEDULES s ON C.ClassScheduleId = s.ScheduleId 
WHERE (@scheduleId IS NULL OR S.ScheduleId = @scheduleId)
GROUP BY s.ScheduleId

END