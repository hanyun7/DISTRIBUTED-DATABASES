drop Procedure [dbo].[sp_ChiTietDSNX]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_ChiTietDSNX]
(
@FR Datetime,
@TO Datetime
)
AS
BEGIN
	SELECT *
	FROM [dbo].[CT_PhatSinh],[dbo].[PHATSINH]
	WHERE [dbo].[CT_PhatSinh].[PHIEU] = [dbo].[PHATSINH].[PHIEU] AND [dbo].[PHATSINH].[NGAY] BETWEEN @FR AND @TO
END	
go
