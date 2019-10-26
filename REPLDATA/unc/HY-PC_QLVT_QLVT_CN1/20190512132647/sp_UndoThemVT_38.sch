drop Procedure [dbo].[sp_UndoThemVT]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_UndoThemVT]
@Original_MAVT char(4)
AS
BEGIN
	DELETE FROM [DBO].[VATTU] WHERE [DBO].[VATTU].[MAVT]= @Original_MAVT
END
go
