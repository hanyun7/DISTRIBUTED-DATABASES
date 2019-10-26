drop Procedure [dbo].[sp_UndoXoaVatTu]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROCEDURE [dbo].[sp_UndoXoaVatTu]
(
	@MAVT char(4),
	@TENVT nvarchar(50),
	@DVT nvarchar(15)
)
AS
BEGIN
	INSERT INTO [DBO].[VATTU](MAVT,TENVT,DVT) VALUES (@MAVT,@TENVT,@DVT);
END

go
