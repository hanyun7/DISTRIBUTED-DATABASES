drop Procedure [dbo].[sp_HienThiCT_PhatSinh]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_HienThiCT_PhatSinh]
@PHIEU char(8)
AS
BEGIN
	SELECT * FROM [dbo].[CT_PhatSinh] WHERE [dbo].[CT_PhatSinh].[PHIEU] = @PHIEU
END	
go
