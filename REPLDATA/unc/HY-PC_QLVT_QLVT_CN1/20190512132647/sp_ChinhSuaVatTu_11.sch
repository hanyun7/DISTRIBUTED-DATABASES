drop Procedure [dbo].[sp_ChinhSuaVatTu]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_ChinhSuaVatTu] 
(
	@MAVT char(4),
	@TENVT nvarchar(50),
	@DVT nvarchar(15)
)
AS
BEGIN
	UPDATE [dbo].[VATTU]
	SET [TENVT] = @TENVT, [DVT] = @DVT	
	WHERE [MAVT] = @MAVT
END	
go
