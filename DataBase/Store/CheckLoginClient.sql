USE DB_STUDIES
GO
IF OBJECT_ID('CheckLoginClient', 'P') IS NOT NULL
DROP PROC CheckLoginClient
GO
CREATE PROCEDURE CheckLoginClient
(
	@userName VARCHAR(50)
	, @passWord VARCHAR(50)
	, @type varchar(50)
) AS
BEGIN
BEGIN TRY
	DECLARE @ecode VARCHAR(50), @edesc VARCHAR(50)
	DECLARE @userId INT
	IF @type ='ADMIN'
		BEGIN
			SET @userId= (SELECT UserId FROM TB_USERS WHERE UserName = @userName AND UserType IN ('ADMIN','ACCOUNTANT'))
		END
	ELSE 
		BEGIN
			SET @userId= (SELECT UserId FROM TB_USERS WHERE UserName = @userName)
		END
	 
	IF @userId ='' OR @userId IS NULL
		BEGIN
			SET @ecode ='100'
			SET @edesc ='TAI KHOAN HOAC MAT KHAU CHUA DUOC DANG KI'
		END
	ELSE
		BEGIN
			SET @userId = (SELECT UserId FROM TB_USERS WHERE UserName = @userName AND UserPassword = @passWord)
			IF @userId ='' OR @userId IS NULL
				BEGIN
					SET @ecode ='101'
					SET @edesc ='TAI KHOAN HOAC MAT KHAU KHONG DUNG'
				END
			ELSE 
				BEGIN
					SET @ecode = '200'
					SET @edesc ='DANG NHAP THANH CONG'
				END
		END
	SELECT @ecode ecode , @edesc edesc
	SELECT * FROM TB_USERS WHERE UserName = @userName AND UserPassword = @passWord 
	

END TRY
BEGIN CATCH
	SELECT '999' ECODE, ERROR_MESSAGE() EDESC
END CATCH
END