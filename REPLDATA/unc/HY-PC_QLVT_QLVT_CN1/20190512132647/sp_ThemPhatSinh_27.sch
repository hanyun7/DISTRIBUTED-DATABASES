drop Procedure [dbo].[sp_ThemPhatSinh]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_ThemPhatSinh]
(
	@PHIEU char(8),
	@NGAY datetime,
	@LOAI char(1),
	@HOTENKH nvarchar(40),
	@THANHTIEN float,
	@MANV numeric,
	@MAKHO char(4)
)
AS
BEGIN
	INSERT [dbo].[PhatSinh] ([PHIEU], [NGAY], [LOAI], [HOTENKH], [THANHTIEN], [MANV], [MAKHO]) VALUES (@PHIEU,@NGAY,@LOAI,@HOTENKH,@THANHTIEN,@MANV,@MAKHO)
END	
go
