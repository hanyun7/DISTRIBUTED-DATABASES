drop Procedure [dbo].[sp_XoaKho]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_XoaKho] 
@MAKHO char(4)
AS
BEGIN
	DELETE FROM [dbo].[KHO]
	WHERE [MAKHO] = @MAKHO
END	
go
