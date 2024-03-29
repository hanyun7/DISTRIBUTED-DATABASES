drop Procedure [dbo].[sp_ChiTietDSNX_ALL]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROCEDURE [dbo].[sp_ChiTietDSNX_ALL]
(
@FR Datetime,
@TO Datetime
)
AS
BEGIN	
	SELECT *
	FROM [dbo].[CT_PhatSinh],[dbo].[PHATSINH]
	WHERE [dbo].[CT_PhatSinh].[PHIEU] = [dbo].[PHATSINH].[PHIEU] AND [dbo].[PHATSINH].[NGAY] BETWEEN @FR AND @TO
	UNION
	SELECT *
	FROM LINK.QLVT.[dbo].[CT_PhatSinh] as L1,LINK.QLVT.[dbo].[PHATSINH] as L2
	WHERE L1.[PHIEU] = L2.[PHIEU] AND L2.[NGAY] BETWEEN @FR AND @TO	
END
go
