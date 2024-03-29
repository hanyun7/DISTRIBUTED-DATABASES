drop Procedure [dbo].[sp_ThemCT_PhatSinh]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_ThemCT_PhatSinh]
(
	@PHIEU char(8),
	@MAVT char(4),
	@SOLUONG int,
	@DONGIA float
)
AS
BEGIN
	INSERT [dbo].[CT_PhatSinh] ([PHIEU], [MAVT], [SOLUONG], [DONGIA]) VALUES (@PHIEU,@MAVT,@SOLUONG,@DONGIA)
	UPDATE [dbo].[CT_PHATSINH]
	SET [dbo].[CT_PHATSINH].[TRIGIA] = CAST([dbo].[CT_PHATSINH].[SOLUONG] AS float)*[dbo].[CT_PHATSINH].[DONGIA]
	UPDATE [dbo].[PhatSinh]
	SET [dbo].[PhatSinh].[THANHTIEN] = [dbo].[PhatSinh].[THANHTIEN] + CAST(@SOLUONG AS float) * @DONGIA
	WHERE [dbo].[PhatSinh].[PHIEU] = @PHIEU
END	
go
