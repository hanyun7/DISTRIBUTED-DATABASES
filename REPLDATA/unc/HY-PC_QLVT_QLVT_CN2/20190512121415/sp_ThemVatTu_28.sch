drop Procedure [dbo].[sp_ThemVatTu]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_ThemVatTu]
(
	@MAVT char(4),
	@TENVT nvarchar(50),
	@DVT nvarchar(15)
)
AS
BEGIN
	INSERT [dbo].[VATTU] ([MAVT], [TENVT], [DVT]) VALUES (@MAVT,@TENVT,@DVT)
END
go
