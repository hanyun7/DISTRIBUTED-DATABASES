drop Procedure [dbo].[sp_KiemTraKho]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROCEDURE [dbo].[sp_KiemTraKho]
@MAKHO char(4)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [DBO].[KHO] WHERE [DBO].[KHO].[MAKHO] = @MAKHO)
	BEGIN
		SELECT 'Return Value' = 1
		RETURN 1
	END
	IF EXISTS(SELECT 1 FROM LINK.QLVT.[DBO].[KHO] as L WHERE L.MAKHO = @MAKHO)
	BEGIN
		SELECT 'Return Value' = 1
		RETURN 1
	END
	SELECT 'Return Value' = 0
	RETURN 0
END
go
