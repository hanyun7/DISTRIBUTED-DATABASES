drop Procedure [dbo].[sp_UndoXoaKho]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go

CREATE PROC [dbo].[sp_UndoXoaKho]
(
@Original_MAKHO char(4),
@TENKHO nvarchar(30),
@DIACHI nvarchar(70),
@MACN char(10)
)
AS
BEGIN
	INSERT INTO [DBO].[KHO](MAKHO, TENKHO, DIACHI, MACN) VALUES (@Original_MAKHO, @TENKHO, @DIACHI, @MACN);
END
go
