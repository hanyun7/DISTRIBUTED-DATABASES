drop Procedure [dbo].[sp_XoaNhanVien]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_XoaNhanVien] 
@MANV numeric	
AS
BEGIN
	DELETE FROM [dbo].[NHANVIEN]
	WHERE [MANV] = @MANV
END	

go
