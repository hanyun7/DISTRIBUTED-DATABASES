drop Procedure [dbo].[sp_KiemTraVatTu]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROCEDURE [dbo].[sp_KiemTraVatTu]
@MAVT char(4)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM dbo.VATTU WHERE dbo.VATTU.MAVT = @MAVT)
	BEGIN
		SELECT 'Return Value' = 1
		RETURN 1
	END
	SELECT 'Return Value' = 0
	RETURN 0
END
go
