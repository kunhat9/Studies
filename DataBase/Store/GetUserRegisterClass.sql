USE DB_STUDIES
GO
IF OBJECT_ID('GetUserRegisterClass', 'P') IS NOT NULL
DROP PROC GetUserRegisterClass
GO
CREATE PROCEDURE GetUserRegisterClass
(
	@scheduledId varchar(50)
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
      ,[UserFilesId]
	FROM [dbo].[TB_USERS] u
	JOIN TB_REGISTERS r
	ON u.UserId = r.RegisterUserId
	WHERE r.RegisterScheduleId = @scheduledId
	ORDER BY [UserId]

END