drop Procedure [dbo].[sp_TimKiemKho]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_TimKiemKho] 
@MAKHO char(4)
AS
BEGIN
	SELECT * FROM [dbo].[KHO] 
	WHERE [MAKHO] = @MAKHO
END	
go
