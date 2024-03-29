drop Procedure [dbo].[sp_UndoChinhSuaVatTu]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_UndoChinhSuaVatTu]
(
	@MAVT char(4),
	@TENVT nvarchar(50),
	@DVT nvarchar(15)
)
AS
BEGIN
	UPDATE [DBO].[VATTU]
	SET [DBO].[VATTU].[TENVT] = @TENVT,[DBO].[VATTU].[DVT] = @DVT
	WHERE ([DBO].[VATTU].[MAVT] = @MAVT);
END
go
