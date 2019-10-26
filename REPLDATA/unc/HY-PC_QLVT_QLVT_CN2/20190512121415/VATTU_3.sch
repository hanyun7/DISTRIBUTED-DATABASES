drop Table [dbo].[VATTU]
go
SET ANSI_PADDING ON
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VATTU](
	[MAVT] [char](4) NOT NULL,
	[TENVT] [nvarchar](50) NOT NULL,
	[DVT] [nvarchar](15) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL
)

GO
ALTER TABLE [dbo].[VATTU] ADD  CONSTRAINT [MSmerge_df_rowguid_C516FBC9F7FE456FAC4566938D36DCE0]  DEFAULT (newsequentialid()) FOR [rowguid]
GO
SET ANSI_NULLS ON

go

SET QUOTED_IDENTIFIER ON

go

SET ANSI_PADDING ON

GO
ALTER TABLE [dbo].[VATTU] ADD  CONSTRAINT [PK__VATTU__60228248AFCC00D9] PRIMARY KEY CLUSTERED 
(
	[MAVT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
