drop Procedure [dbo].[sp_UndoXoaNhanVien]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_UndoXoaNhanVien]
@Original_MANV numeric
AS
BEGIN	
	UPDATE [DBO].[NHANVIEN]
	SET [DBO].[NHANVIEN].[TINHTRANG] = 'D'
	WHERE ([DBO].[NHANVIEN].[MANV] = @Original_MANV);
END
go
