drop Procedure [dbo].[sp_KiemTraPhatSinh]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROCEDURE [dbo].[sp_KiemTraPhatSinh]
@PHIEU char(8)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [dbo].[PHATSINH] WHERE [dbo].[PHATSINH].[PHIEU] = @PHIEU)
	BEGIN
		SELECT 'Return Value' = 1
		RETURN 1
	END	
	SELECT 'Return Value' = 0
	RETURN 0
END
go
