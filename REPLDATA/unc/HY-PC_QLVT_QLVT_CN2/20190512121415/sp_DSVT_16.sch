drop Procedure [dbo].[sp_DSVT]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_DSVT]
AS
BEGIN
	SELECT * FROM [dbo].[VATTU] ORDER BY TENVT ASC
END
go
