drop Procedure [dbo].[sp_ThemKho]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_ThemKho] 
(
	@MAKHO char(4),
	@TENKHO nvarchar(30),
	@DIACHI nvarchar(70),
	@MACN char(10) 
)
AS
BEGIN
	INSERT [dbo].[KHO] ([MAKHO], [TENKHO], [DIACHI], [MACN]) VALUES (@MAKHO,@TENKHO,@DIACHI,@MACN)
END	
go
