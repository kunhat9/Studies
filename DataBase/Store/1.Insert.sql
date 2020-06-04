USE DB_STUDIES
GO
-- INSERT KHỐI
INSERT INTO TB_BOXES(BoxCode)
VALUES(1)
INSERT INTO TB_BOXES(BoxCode)
VALUES(2)
INSERT INTO TB_BOXES(BoxCode)
VALUES(3)
INSERT INTO TB_BOXES(BoxCode)
VALUES(4)
INSERT INTO TB_BOXES(BoxCode)
VALUES(5)
INSERT INTO TB_BOXES(BoxCode)
VALUES(6)
INSERT INTO TB_BOXES(BoxCode)
VALUES(7)
INSERT INTO TB_BOXES(BoxCode)
VALUES(8)
INSERT INTO TB_BOXES(BoxCode)
VALUES(9)
INSERT INTO TB_BOXES(BoxCode)
VALUES(10)
INSERT INTO TB_BOXES(BoxCode)
VALUES(11)
INSERT INTO TB_BOXES(BoxCode)
VALUES(12)
-- INSERT MÔN HỌC
GO
INSERT INTO TB_SUBJECTS(SubjectName)
VALUES(N'Toán')
INSERT INTO TB_SUBJECTS(SubjectName)
VALUES(N'Vật lý')
INSERT INTO TB_SUBJECTS(SubjectName)
VALUES(N'Hóa học')
INSERT INTO TB_SUBJECTS(SubjectName)
VALUES(N'Sinh học')
INSERT INTO TB_SUBJECTS(SubjectName)
VALUES(N'Văn Học')
INSERT INTO TB_SUBJECTS(SubjectName)
VALUES(N'Lịch Sử')
INSERT INTO TB_SUBJECTS(SubjectName)
VALUES(N'Địa lý')
INSERT INTO TB_SUBJECTS(SubjectName)
VALUES(N'Tiếng Anh')
GO
-- INSERT USER ADMIN
INSERT INTO TB_USERS(
       [UserName]
	  ,[UserFullName]
      ,[UserPassword]
      ,[UserType]
      ,[UserPhone]
      ,[UserEmail]
      ,[UserAddress]
      ,[UserAcademicLevel]
      ,[UserStatus]
      ,[UserNote])
VALUES(
	'ADMIN'
	,N'Lê Văn Nhật'
	, '1'
	, 'ADMIN'
	, ''
	, ''
	, ''
	, ''
	, 'A'
	, 'ADMINSTRATOR'
)
GO
 -- INSERT DATA HARD 
 INSERT INTO TB_USERS(
       [UserName]
	  ,[UserFullName]
      ,[UserPassword]
      ,[UserType]
      ,[UserPhone]
      ,[UserEmail]
      ,[UserAddress]
      ,[UserAcademicLevel]
      ,[UserStatus]
      ,[UserNote]
	  ,[UserNumberSalary])
VALUES(
	'nhatlv1'
	,N'Lê Văn Một'
	, '1'
	, 'TEACHER'
	, ''
	, ''
	, ''
	, ''
	, 'A'
	, N'GIÁO VIÊN '
	,0.7
)
GO
INSERT INTO TB_USERS(
       [UserName]
	  ,[UserFullName]
      ,[UserPassword]
      ,[UserType]
      ,[UserPhone]
      ,[UserEmail]
      ,[UserAddress]
      ,[UserAcademicLevel]
      ,[UserStatus]
      ,[UserNote]
	  ,[UserNumberSalary])
VALUES(
	'nhatlv2'
	,N'Lê Văn Hai'
	, '1'
	, 'TEACHER'
	, ''
	, ''
	, ''
	, ''
	, 'A'
	, N'GIÁO VIÊN'
	,0.5
)
GO
INSERT INTO TB_USERS(
       [UserName]
	  ,[UserFullName]
      ,[UserPassword]
      ,[UserType]
      ,[UserPhone]
      ,[UserEmail]
      ,[UserAddress]
      ,[UserAcademicLevel]
      ,[UserStatus]
      ,[UserNote])
VALUES(
	'nhatlv3'
	,N'Lê Văn Ba'
	, '1'
	, 'STUDIES'
	, ''
	, ''
	, ''
	, ''
	, 'A'
	, N'HỌC SINH '
)
GO
INSERT INTO TB_USERS(
       [UserName]
	  ,[UserFullName]
      ,[UserPassword]
      ,[UserType]
      ,[UserPhone]
      ,[UserEmail]
      ,[UserAddress]
      ,[UserAcademicLevel]
      ,[UserStatus]
      ,[UserNote])
VALUES(
	'nhatlv4'
	,N'Lê Văn Bốn'
	, '1'
	, 'STUDIES'
	, ''
	, ''
	, ''
	, ''
	, 'A'
	, N'HỌC SINH'
)
GO
INSERT INTO TB_USERS(
       [UserName]
	  ,[UserFullName]
      ,[UserPassword]
      ,[UserType]
      ,[UserPhone]
      ,[UserEmail]
      ,[UserAddress]
      ,[UserAcademicLevel]
      ,[UserStatus]
      ,[UserNote])
VALUES(
	'nhatlv5'
	,N'Lê Văn Năm'
	, '1'
	, 'STUDIES'
	, ''
	, ''
	, ''
	, ''
	, 'A'
	, N'HỌC SINH'
)
GO
