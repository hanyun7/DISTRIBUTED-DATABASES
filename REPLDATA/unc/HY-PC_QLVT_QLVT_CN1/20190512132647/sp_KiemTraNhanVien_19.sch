drop Procedure [dbo].[sp_KiemTraNhanVien]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROCEDURE [dbo].[sp_KiemTraNhanVien]
@MANV numeric
AS
BEGIN

	IF EXISTS(SELECT 1 FROM [DBO].[NHANVIEN] WHERE [DBO].[NHANVIEN].[MANV] = @MANV)
	BEGIN
		SELECT 'Return Value' = 1
		RETURN 1
	END
	IF EXISTS(SELECT 1 FROM LINK.QLVT.[DBO].[NHANVIEN] as L WHERE L.MANV = @MANV)
	BEGIN
		SELECT 'Return Value' = 1
		RETURN 1
	END
	SELECT 'Return Value' = 0
	RETURN 0
END
go
