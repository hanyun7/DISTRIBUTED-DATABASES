drop Procedure [dbo].[sp_XoaCT_PhatSinh]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_XoaCT_PhatSinh]
(
@PHIEU char(8),
@MAVT char(4)
)
AS
BEGIN
	DELETE FROM [dbo].[CT_PhatSinh]
	WHERE [dbo].[CT_PhatSinh].[PHIEU] = @PHIEU AND [dbo].[CT_PhatSinh].[MAVT] = @MAVT
END	
go
