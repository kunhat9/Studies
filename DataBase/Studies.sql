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
	, UserName nvarchar(30) NOT NULL        -- Tài khoản 
	, UserFullName nvarchar(255) NOT NULL   -- Họ tên
	, UserPassword nvarchar(30) NOT NULL    -- mật khẩu 
	, UserType nvarchar(20) NOT NULL		-- Phân loại: ADMIN / STAFF / STUDIES / TEACHER
	, UserPhone varchar(50) NOT NULL        -- điện thoại
	, UserEmail varchar(50)
	, UserAddress varchar(50)
	, UserDateCreated datetime 
	, UserAcademicLevel varchar(50)
	, UserStatus nvarchar(20) DEFAULT('D')	-- Trạng thái - A/D
	, UserFilesId varchar(50)                -- Files 
	, UserNote nvarchar(100)	DEFAULT ('HE THONG TU SINH')-- Ghi chú
	, UserNumberSalary decimal(18,2) DEFAULT(0.00)        -- hệ số lương nếu type = teacher
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
	,BoxSubjectPrice decimal(18,0)
	, CONSTRAINT FK_BoxSubjectBoxId FOREIGN KEY (BoxSubjectBoxId) REFERENCES TB_BOXES(BoxId)
	, CONSTRAINT FK_BoxSubjectSubjectId FOREIGN KEY (BoxSubjectSubjectId) REFERENCES TB_SUBJECTS(SubjectId)
	, CONSTRAINT Unique_BoxSubjectBoxId_BoxSubjectSubjectId UNIQUE(BoxSubjectBoxId,BoxSubjectSubjectId)
)
GO
CREATE TABLE TB_TEACHING_SCHEDULES -- ĐĂNG KÍ LỊCH DẠY CỦA GIÁO VIÊN
(
	 TeachingScheduleId INT IDENTITY PRIMARY KEY
	,TeachingScheduleDayOfWeek VARCHAR(50)         -- NGÀY TRONG TUẦN
	,TeachingScheduleTimeFrom time                 -- THỜI GIAN BẮT ĐẦU
	,TeachingScheduleTimeTo time                   -- THỜI GIAN KẾT THÚC
	, TeachingScheduleNote nvarchar(max)           -- MÔ TẢ
	, TeachingScheduleUserId INT                   -- GẮN VỚI GIÁO VIÊN NÀO 
	, CONSTRAINT FK_TeachingScheduleUserId FOREIGN KEY (TeachingScheduleUserId) REFERENCES TB_USERS(UserId) 
	, TeachingScheduleBoxSubjectId INT			   -- Gắn với môn nào , khối nào
	, CONSTRAINT FK_TeachingScheduleBoxSubjectId FOREIGN KEY (TeachingScheduleBoxSubjectId) REFERENCES TB_BOX_SUBJECTS(BoxSubjectId) 
)
GO
CREATE TABLE TB_SCHEDULES	-- Lớp dạy của giáo viên
(
	ScheduleId int IDENTITY PRIMARY KEY
	, ScheduleCode nvarchar(20) UNIQUE NOT NULL		-- Mã lớp
	, ScheduleDateCreate datetime	-- Ngày mở lớp
	, ScheduleStatus nvarchar(10)	-- Trạng thái
	, ScheduleDateBegin nvarchar(10)	-- Ngày bắt đầu
	, ScheduleDateEnd nvarchar(10)		-- Ngày kết thúc
	, SchedulePrice decimal(18,0)		-- Giá tiền
	, ScheduleIdBoxSubjectId INT
	, ScheduleFileId varchar(50)        -- FileId
	, CONSTRAINT FK_ScheduleIdBoxSubjectId FOREIGN KEY (ScheduleIdBoxSubjectId) REFERENCES TB_BOX_SUBJECTS(BoxSubjectId) 
	, ScheduleUserId int
	, CONSTRAINT FK_ScheduleUserId FOREIGN KEY (ScheduleUserId) REFERENCES TB_USERS(UserId)
)
GO
CREATE TABLE TB_SCHEDULE_DETAILS	-- Chi tiết
(
	ScheduleDetailId int IDENTITY PRIMARY KEY
	,ScheduleDetailDayOfWeek varchar(50)  -- ngày trong tuần
	,ScheduleDetailTimeFrom time  -- từ giờ 
	,ScheduleDetailTimeTo time -- đến giờ
	,ScheduleDetailNote nvarchar(500)-- mô tả
	,ScheduleDetailScheduleId INT
	, CONSTRAINT FK_ScheduleDetailScheduleId FOREIGN KEY (ScheduleDetailScheduleId) REFERENCES TB_SCHEDULES(ScheduleId)   
)
GO
CREATE TABLE TB_CLASSES  -- lớp học 
(
	ClassId INT IDENTITY PRIMARY KEY
	,ClassDateCreated datetime
	, ClassUserId     INT       -- Học sinh 
	, CONSTRAINT FK_ClassUserId FOREIGN KEY (ClassUserId) REFERENCES TB_USERS(UserId) 
	, ClassScheduleId INT
	, CONSTRAINT FK_ClassScheduleId FOREIGN KEY (ClassScheduleId) REFERENCES TB_SCHEDULES(ScheduleId)
)
GO
CREATE TABLE TB_POINTS -- BẢNG ĐIỂM
(
	PointId INT IDENTITY PRIMARY KEY
	, PointNumber decimal(18,2)  -- Số điểm
	, PointClassId INT
	, PointType VARCHAR(50) -- LAN1,LAN2
	, CONSTRAINT FK_PointClassId FOREIGN KEY (PointClassId) REFERENCES TB_CLASSES(ClassId)
)
GO
CREATE TABLE TB_REGISTERS	-- Đăng ký học
(
	RegisterId int IDENTITY PRIMARY KEY
	,RegisterUserId INT 
	, CONSTRAINT FK_RegisterUserId FOREIGN KEY (RegisterUserId) REFERENCES TB_USERS(UserId) 
	, RegisterScheduleId INT
	, CONSTRAINT FK_RegisterScheduleId FOREIGN KEY (RegisterScheduleId) REFERENCES TB_SCHEDULES(ScheduleId) 
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
	, TrackingScheduleId INT
	, CONSTRAINT FK_TrackingScheduleId FOREIGN KEY (TrackingScheduleId) REFERENCES TB_SCHEDULES(ScheduleId)
)
GO
CREATE TABLE TB_FILES	-- Hình ảnh
(
	FileId int IDENTITY PRIMARY KEY
	,FileUrl nvarchar(255)
	,FileRef nvarchar(MAX) -- id cua anh xa
	,[FileName] nvarchar(100)
	,FileType nvarchar(255)
	,FileService varchar(50) -- dinh dang xem no la cua user , lop hoc, tin tuc hay cai gi 
)
GO
CREATE SEQUENCE TB_SCHEDULESEQ START WITH 1 INCREMENT BY 1
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
-- tổng số buổi học theo lớp theo thời gian
-- cộng tổng lại theo thời gian
- Quản lý khối, môn -> Có thể hard
- Quản lý đăng ký lịch dạy ->> Cần mô tả: Từ ngày - đến ngày, giá tiền ai đưa ra? admin có sửa đc giá tiền?
- Quản lý đăng ký học ->> Cần mô tả: Cung cấp username-password ntn? / có duyệt ko?
*/