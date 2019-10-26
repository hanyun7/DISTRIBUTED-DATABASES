drop Procedure [dbo].[sp_XoaPhatSinh]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_XoaPhatSinh]
(
@PHIEU char(8)
)
AS
BEGIN
	DELETE FROM [dbo].[PhatSinh]
	WHERE [dbo].[PhatSinh].[PHIEU] = @PHIEU 
END	
go
