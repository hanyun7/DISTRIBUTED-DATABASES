drop Procedure [dbo].[sp_TimKiemNhanVien]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_TimKiemNhanVien]
@MANV numeric
AS
BEGIN
	SELECT * FROM [dbo].[NHANVIEN] 
	WHERE [MANV] = @MANV
END	
go
