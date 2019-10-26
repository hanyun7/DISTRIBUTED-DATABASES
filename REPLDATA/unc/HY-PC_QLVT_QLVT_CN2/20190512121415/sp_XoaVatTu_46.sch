drop Procedure [dbo].[sp_XoaVatTu]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_XoaVatTu] 
@MAVT char(4)
AS
BEGIN
	DELETE FROM [dbo].[VATTU]
	WHERE [MAVT] = @MAVT
END	
go
