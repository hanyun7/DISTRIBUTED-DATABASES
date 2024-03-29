drop View [dbo].[ds_PhanManh]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE VIEW [dbo].[ds_PhanManh]
AS
SELECT TENCN = PUBS.description, TENSERVER = subscriber_server 
FROM dbo.sysmergepublications PUBS, dbo.sysmergesubscriptions SUBS
WHERE PUBS.pubid = SUBS.PUBID AND PUBS.publisher <> SUBS.subscriber_server
go
