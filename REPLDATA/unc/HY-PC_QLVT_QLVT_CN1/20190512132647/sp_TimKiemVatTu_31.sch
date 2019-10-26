drop Procedure [dbo].[sp_TimKiemVatTu]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_TimKiemVatTu]
@MAVT char(4)
AS
BEGIN
	SELECT * FROM [dbo].[VATTU] 
	WHERE [MAVT] = @MAVT
END	
go
