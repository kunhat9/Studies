USE master
GO
DROP DATABASE DB_STUDIES
GO
CREATE DATABASE DB_STUDIES
GO
USE DB_STUDIES
GO
CREATE TABLE TB_USERS		-- Người dùng
(
	UserId int IDENTITY PRIMARY KEY
	, UserName nvarchar(30) NOT NULL
	, UserPassword nvarchar(30) NOT NULL
	, UserType nvarchar(20) NOT NULL		-- Phân loại: ADMIN / STAFF / STUDIES
	, UserPhone varchar(50) NOT NULL
	, UserEmail varchar(50)
	, UserAddress varchar(50)
	, UserAcademicLevel varchar(50)
	, UserStatus nvarchar(20) DEFAULT('D')	-- Trạng thái - A/D
	, UserNote nvarchar(100)	DEFAULT ('HE THONG TU SINH')-- Ghi chú
)
GO
CREATE TABLE TB_HEADQUARTERS -- cơ sở 
(
	HeadQuarterId INT IDENTITY PRIMARY KEY
	,HeadQuarterName nvarchar(500)
	,HeadQuarterAddress nvarchar(max)
	,HeadQuarterPhone varchar(10)
	,HeadQuarterEmail nvarchar(200)
	,HeadQuarterNote nvarchar(max)
	,HeadQuarterLat varchar(50)
	,HeadQuarterLong varchar(50)

)
GO
CREATE TABLE TB_BOXES		-- Khối
(
	BoxId int IDENTITY PRIMARY KEY
	, BoxCode nvarchar(2) NOT NULL		-- Mã khối: 5, 6, 7, 12
)
GO
CREATE TABLE TB_SUBJECTS	-- Môn học
(
	SubjectId int IDENTITY PRIMARY KEY
	, SubjectName nvarchar(20) NOT NULL		-- Tên môn học
)
GO
CREATE TABLE TB_BOX_SUBJECTS -- Môn học của khối
(
	BoxSubjectId INT IDENTITY PRIMARY KEY
	,BoxSubjectBoxId INT NOT NULL
	,BoxSubjectSubjectId INT NOT NULL
	, CONSTRAINT FK_BoxSubjectBoxId FOREIGN KEY (BoxSubjectBoxId) REFERENCES TB_BOXES(BoxId)
	, CONSTRAINT FK_BoxSubjectSubjectId FOREIGN KEY (BoxSubjectSubjectId) REFERENCES TB_SUBJECTS(SubjectId)
	, CONSTRAINT Unique_BoxSubjectBoxId_BoxSubjectSubjectId UNIQUE(BoxSubjectBoxId,BoxSubjectSubjectId)
)
GO
CREATE TABLE TB_SCHEDULES	-- Lớp dạy của giáo viên
(
	ScheduleId int IDENTITY PRIMARY KEY
	, ScheduleCode nvarchar(20)		-- Mã lớp
	, ScheduleDateCreate datetime	-- Ngày đăng ký
	, ScheduleStatus nvarchar(10)	-- Trạng thái
	, ScheduleDateBegin nvarchar(10)	-- Ngày bắt đầu
	, ScheduleDateEnd nvarchar(10)		-- Ngày kết thúc
	, SchedulePrice decimal(18,0)		-- Giá tiền
	, ScheduleIdBoxSubjectId INT
	, CONSTRAINT FK_ScheduleIdBoxSubjectId FOREIGN KEY (ScheduleIdBoxSubjectId) REFERENCES TB_BOX_SUBJECTS(BoxSubjectId) 
	, ScheduleUserId int
	, CONSTRAINT FK_ScheduleUserId FOREIGN KEY (ScheduleUserId) REFERENCES TB_USERS(UserId)
)
GO
CREATE TABLE TB_SCHEDULE_DETAILS	-- Chi tiết
(
	ScheduleDetailId int IDENTITY PRIMARY KEY
	,ScheduleDetailDate date  -- ngày dạy học 
	,ScheduleDetailTimeFrom time  -- từ giờ 
	,ScheduleDetailTimeTo time -- đến giờ
	,ScheduleDetailNote varchar(50)-- mô tả
	,ScheduleDetailScheduleId INT
	, CONSTRAINT FK_ScheduleDetailScheduleId FOREIGN KEY (ScheduleDetailScheduleId) REFERENCES TB_SCHEDULES(ScheduleId)   
)
GO
CREATE TABLE TB_CLASS  -- lớp học 
(
	ClassId INT IDENTITY PRIMARY KEY
	, ClassName nvarchar(max) -- Tên lớp 
	, ClassCode nvarchar(50) -- mã lớp
	, ClassDateBegin  datetime       -- ngày bắt đầu
	, ClassDateEnd    datetime  -- ngày kết thúc
	, ClassTimeStart  time      -- giờ bắt đầu
	, ClassTimeEnd    time      -- giờ kết thúc
	, ClassUserId     INT       -- Học sinh 
	, CONSTRAINT FK_ClassUserId FOREIGN KEY (ClassUserId) REFERENCES TB_USERS(UserId) 
	, ClassScheduleId INT
	, CONSTRAINT FK_ClassScheduleId FOREIGN KEY (ClassScheduleId) REFERENCES TB_SCHEDULES(ScheduleId)
	, ClassBoxSubjectId INT -- Môn học theo khối
	, CONSTRAINT FK_ClassBoxSubjectId FOREIGN KEY (ClassBoxSubjectId) REFERENCES TB_BOX_SUBJECTS(BoxSubjectId) 
)
GO
CREATE TABLE TB_POINT -- BẢNG ĐIỂM
(
	PointId INT IDENTITY PRIMARY KEY
	, PointNumber decimal(18,2)  -- Số điểm
	, PointClassId INT
	, CONSTRAINT FK_PointClassId FOREIGN KEY (PointClassId) REFERENCES TB_CLASS(ClassId)
	, PointUserId INT
	, CONSTRAINT FK_PointUserId FOREIGN KEY (PointUserId) REFERENCES TB_USERS(UserId) 
)
GO
CREATE TABLE TB_REGISTERS	-- Đăng ký học
(
	RegisterId int IDENTITY PRIMARY KEY
	, RegisterName nvarchar(50)		-- Tên đăng ký*
	, RegisterPlace nvarchar(MAX)	-- Nơi đang học
	, RegisterPhone nvarchar(20)	-- Sđt phụ huynh*
	, RegisterBoxSubjectId INT
	, CONSTRAINT FK_RegisterBoxSubjectId FOREIGN KEY (RegisterBoxSubjectId) REFERENCES TB_BOX_SUBJECTS(BoxSubjectId) 
	, RegisterDateCreate datetime	-- Ngày đk
)
GO
CREATE TABLE TB_TRACKINGS	-- Điểm danh
(
	TrackingId int IDENTITY PRIMARY KEY
	,TrackingDate datetime -- ngày điểm danh , giờ điểm danh
	,TrackingNote nvarchar(500) -- mô tả
	,TrackingUserId INT -- mã học viên 
	, CONSTRAINT FK_TrackingUserId  FOREIGN KEY (TrackingUserId) REFERENCES TB_USERS(UserId)
)
GO
/*
GENERAL
	Trang chủ
	Giới thiệu
	Giáo viên
	Lớp học
	Đăng ký học
	Đăng nhập

TEACHER
	Đăng ký lịch dạy
	Điểm danh

STUDENT
	Thông tin học sinh
		Thời khóa biểu
		Thông tin điểm danh
		Học phí
		Điểm
*/
/*
- Quản lý khối, môn -> Có thể hard
- Quản lý đăng ký lịch dạy ->> Cần mô tả: Từ ngày - đến ngày, giá tiền ai đưa ra? admin có sửa đc giá tiền?
- Quản lý đăng ký học ->> Cần mô tả: Cung cấp username-password ntn? / có duyệt ko?
*/