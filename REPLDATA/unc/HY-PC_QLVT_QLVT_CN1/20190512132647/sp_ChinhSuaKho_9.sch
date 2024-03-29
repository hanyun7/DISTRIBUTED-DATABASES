drop Procedure [dbo].[sp_ChinhSuaKho]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_ChinhSuaKho] 
(
	@MAKHO char(4),
	@TENKHO nvarchar(30),
	@DIACHI nvarchar(70),
	@MACN char(10) 
)
AS
BEGIN
	UPDATE [dbo].[KHO]
	SET [TENKHO] = @TENKHO, [DIACHI] = @DIACHI, [MACN] = @MACN
	WHERE [MAKHO] = @MAKHO
END	
go
