drop Procedure [dbo].[sp_SelectPhatSinh]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_SelectPhatSinh]
@PHIEU char(8)
AS
BEGIN
	SELECT * FROM [dbo].[PhatSinh] WHERE [dbo].[PhatSinh].[PHIEU] = @PHIEU
END	
go
