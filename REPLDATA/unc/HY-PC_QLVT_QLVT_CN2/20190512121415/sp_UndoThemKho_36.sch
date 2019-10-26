drop Procedure [dbo].[sp_UndoThemKho]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROCEDURE [dbo].[sp_UndoThemKho]
@MAKHO char(4)
AS
BEGIN
	DELETE FROM [DBO].[KHO] WHERE [DBO].[KHO].[MAKHO] = @MAKHO;
END
go
