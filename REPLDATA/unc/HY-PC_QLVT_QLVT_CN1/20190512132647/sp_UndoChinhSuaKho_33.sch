drop Procedure [dbo].[sp_UndoChinhSuaKho]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_UndoChinhSuaKho]
(
@Original_MAKHO char(4),
@TENKHO nvarchar(30),
@DIACHI nvarchar(70)
)
AS
BEGIN
	UPDATE [DBO].[KHO]
	SET [DBO].[KHO].[TENKHO] = @TENKHO, [DBO].[KHO].[DIACHI] = @DIACHI
	WHERE ([DBO].[KHO].[MAKHO] = @Original_MAKHO);
END
go
