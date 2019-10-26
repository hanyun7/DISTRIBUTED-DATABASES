drop Procedure [dbo].[sp_UndoThemNhanVien]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROCEDURE [dbo].[sp_UndoThemNhanVien]
@Original_MANV numeric
AS
BEGIN
	UPDATE [DBO].[NHANVIEN]
	SET [DBO].[NHANVIEN].[TINHTRANG] = 'X'
	WHERE (MANV = @Original_MANV);
END

go
