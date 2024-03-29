drop Procedure [dbo].[sp_TongHop_NX]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROCEDURE [dbo].[sp_TongHop_NX]
(
@ROLE NVARCHAR(15),
@STARTTIME DATETIME,
@ENDTIME DATETIME
)
AS
BEGIN
	DECLARE @TONGNHAP FLOAT
	DECLARE @TONGXUAT FLOAT
	DECLARE @CONGTYLEN FLOAT = 100
	DECLARE @CONGTYLEX FLOAT = 100
	DECLARE @TONGNHAPALL TABLE(TONGNHAP FLOAT)
	DECLARE @TONGXUATALL TABLE(TONGXUAT FLOAT)
	DECLARE @NHAP TABLE(NGAY DATETIME, TONGN FLOAT, TYLE1 FLOAT, TONGX FLOAT, TYLE2 FLOAT)
	DECLARE @XUAT TABLE(NGAY DATETIME, TONGN FLOAT, TYLE1 FLOAT, TONGX FLOAT, TYLE2 FLOAT)
	DECLARE @NX TABLE(NGAY DATETIME, TONGN FLOAT, TYLE1 FLOAT, TONGX FLOAT, TYLE2 FLOAT)
	DECLARE @RT TABLE(NGAY DATETIME, TONGN FLOAT, TYLE1 FLOAT, TONGX FLOAT, TYLE2 FLOAT)


	--QUYỀN CHI NHÁNH
	IF(@ROLE='CHINHANH')
	BEGIN
	
		--TONG NHAP TONG XUAT TRONG KHOANG THOI GIAN
		SELECT @TONGNHAP = SUM(THANHTIEN) 
		FROM PHATSINH
		WHERE LOAI='N' AND NGAY BETWEEN @STARTTIME AND @ENDTIME
		
		SELECT @TONGXUAT = SUM(THANHTIEN) 
		FROM PHATSINH
		WHERE LOAI='X' AND NGAY BETWEEN @STARTTIME AND @ENDTIME

		--CHỌN NHỮNG NGÀY CHỈ CÓ XUẤT, K CÓ NHẬP
		INSERT INTO @XUAT(NGAY, TONGX,TYLE2) 
			SELECT NGAY, SUM(THANHTIEN),ROUND((SUM(THANHTIEN) / @TONGXUAT)*100,2) 
			FROM PHATSINH
			WHERE LOAI='X' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY NOT IN(SELECT NGAY FROM PHATSINH WHERE LOAI='N')
			GROUP BY NGAY
		--CHỌN NHỮNG NGÀY CHỈ CÓ NHẬP, K CÓ XUẤT
		INSERT INTO @NHAP(NGAY, TONGN, TYLE1) 
			SELECT NGAY, SUM(THANHTIEN),ROUND((SUM(THANHTIEN) / @TONGNHAP)*100,2) 
			FROM PHATSINH
			WHERE LOAI='N' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY NOT IN(SELECT NGAY FROM PHATSINH WHERE LOAI='X')
			GROUP BY NGAY
		--CHỌN NHỮNG NGÀY CÓ NHẬP LẪN XUẤT
		INSERT INTO @NX(NGAY, TONGN, TONGX, TYLE1, TYLE2) 
			SELECT X.NGAY, N.TN, X.TX, N.TL, X.TL FROM
				(SELECT NGAY, SUM(THANHTIEN) AS TX, ROUND((SUM(THANHTIEN) / @TONGXUAT)*100,2) AS TL FROM PHATSINH
												 WHERE LOAI='X' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY IN(SELECT NGAY FROM PHATSINH WHERE LOAI='N')
												 GROUP BY NGAY) X,
				(SELECT NGAY, SUM(THANHTIEN) AS TN,ROUND((SUM(THANHTIEN) / @TONGNHAP)*100,2) AS TL FROM PHATSINH
												 WHERE LOAI='N' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY IN(SELECT NGAY FROM PHATSINH WHERE LOAI='X')
												 GROUP BY NGAY) N
				WHERE X.NGAY = N.NGAY		
	END

	--QUYỀN CÔNG TY
	ELSE IF(@ROLE='CONGTY')
	BEGIN
			
		--TONG NHAP TONG XUAT TRONG KHOANG THOI GIAN
		
		INSERT INTO @TONGNHAPALL(TONGNHAP)
		SELECT SUM(THANHTIEN) 
		FROM LINK.QLVT.DBO.PHATSINH
		WHERE LOAI='N' AND NGAY BETWEEN @STARTTIME AND @ENDTIME
		UNION
		SELECT SUM(THANHTIEN) 
		FROM DBO.PHATSINH
		WHERE LOAI='N' AND NGAY BETWEEN @STARTTIME AND @ENDTIME	

		SELECT @TONGNHAP = SUM(TONGNHAP)
		FROM @TONGNHAPALL
		--WHERE LOAI='N' AND NGAY BETWEEN @STARTTIME AND @ENDTIME

		INSERT INTO @TONGXUATALL(TONGXUAT)
		SELECT SUM(THANHTIEN) 
		FROM LINK.QLVT.DBO.PHATSINH
		WHERE LOAI='X' AND NGAY BETWEEN @STARTTIME AND @ENDTIME
		UNION
		SELECT SUM(THANHTIEN) 
		FROM DBO.PHATSINH
		WHERE LOAI='X' AND NGAY BETWEEN @STARTTIME AND @ENDTIME	

		SELECT @TONGXUAT = SUM(TONGXUAT)
		FROM @TONGXUATALL
		--SELECT @TONGXUAT = SUM(THANHTIEN) 
		--FROM LINK.QLVT.DBO.PHATSINH
		--WHERE LOAI='X' AND NGAY BETWEEN @STARTTIME AND @ENDTIME

		--CHỌN NHỮNG NGÀY CHỈ CÓ XUẤT, K CÓ NHẬP

		INSERT INTO @XUAT(NGAY, TONGX,TYLE2) ( 
			SELECT NGAY, SUM(THANHTIEN),ROUND((SUM(THANHTIEN) / @TONGXUAT)*100,2)
			FROM LINK.QLVT.DBO.PHATSINH
			WHERE LOAI='X' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY NOT IN(SELECT NGAY FROM LINK.QLVT.DBO.PHATSINH WHERE LOAI='N')
			GROUP BY NGAY )
			UNION
			(
			SELECT NGAY, SUM(THANHTIEN),ROUND((SUM(THANHTIEN) / @TONGXUAT)*100,2)
			FROM DBO.PHATSINH
			WHERE LOAI='X' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY NOT IN(SELECT NGAY FROM LINK.QLVT.DBO.PHATSINH WHERE LOAI='N')
			GROUP BY NGAY )
		--CHỌN NHỮNG NGÀY CHỈ CÓ NHẬP, K CÓ XUẤT

		INSERT INTO @NHAP(NGAY, TONGN, TYLE1) (
			SELECT NGAY, SUM(THANHTIEN),ROUND((SUM(THANHTIEN) / @TONGNHAP)*100,2)
			FROM LINK.QLVT.DBO.PHATSINH
			WHERE LOAI='N' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY NOT IN(SELECT NGAY FROM LINK.QLVT.DBO.PHATSINH WHERE LOAI='X')
			GROUP BY NGAY )
			UNION
			(
			SELECT NGAY, SUM(THANHTIEN),ROUND((SUM(THANHTIEN) / @TONGNHAP)*100,2)
			FROM DBO.PHATSINH
			WHERE LOAI='N' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY NOT IN(SELECT NGAY FROM LINK.QLVT.DBO.PHATSINH WHERE LOAI='X')
			GROUP BY NGAY )
		--CHỌN NHỮNG NGÀY CÓ NHẬP LẪN XUẤT

		INSERT INTO @NX(NGAY, TONGN, TONGX, TYLE1, TYLE2) (
			SELECT X.NGAY, N.TN, X.TX, N.TL, X.TL FROM
				(SELECT NGAY, SUM(THANHTIEN) AS TX, ROUND((SUM(THANHTIEN) / @TONGXUAT)*100,2) AS TL 
												FROM LINK.QLVT.DBO.PHATSINH
												WHERE LOAI='X' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY IN(SELECT NGAY FROM LINK.QLVT.DBO.PHATSINH WHERE LOAI='N')
												GROUP BY NGAY) X,
				(SELECT NGAY, SUM(THANHTIEN) AS TN,ROUND((SUM(THANHTIEN) / @TONGNHAP)*100,2) AS TL 
												FROM LINK.QLVT.DBO.PHATSINH
												WHERE LOAI='N' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY IN(SELECT NGAY FROM LINK.QLVT.DBO.PHATSINH WHERE LOAI='X')
												GROUP BY NGAY) N
				WHERE X.NGAY = N.NGAY )
			UNION
			(
			SELECT X.NGAY, N.TN, X.TX, N.TL, X.TL FROM
				(SELECT NGAY, SUM(THANHTIEN) AS TX, ROUND((SUM(THANHTIEN) / @TONGXUAT)*100,2) AS TL 
												FROM DBO.PHATSINH
												WHERE LOAI='X' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY IN(SELECT NGAY FROM LINK.QLVT.DBO.PHATSINH WHERE LOAI='N')
												GROUP BY NGAY) X,
				(SELECT NGAY, SUM(THANHTIEN) AS TN,ROUND((SUM(THANHTIEN) / @TONGNHAP)*100,2) AS TL 
												FROM DBO.PHATSINH
												WHERE LOAI='N' AND NGAY BETWEEN @STARTTIME AND @ENDTIME AND NGAY IN(SELECT NGAY FROM LINK.QLVT.DBO.PHATSINH WHERE LOAI='X')
												GROUP BY NGAY) N
				WHERE X.NGAY = N.NGAY )	 

	END
-------------------------------------------------------------------------------------------------------------------------------

	SELECT NGAY ,ISNULL(TONGN,0) AS TONGN, ISNULL(TYLE1,0) AS TYLE1, ISNULL(TONGX,0) AS TONGX, ISNULL(TYLE2,0) AS TYLE2 FROM @NHAP
	UNION SELECT  NGAY ,ISNULL(TONGN,0), ISNULL(TYLE1,0), ISNULL(TONGX,0), ISNULL(TYLE2,0)  FROM @NX
	UNION SELECT  NGAY ,ISNULL(TONGN,0), ISNULL(TYLE1,0), ISNULL(TONGX,0), ISNULL(TYLE2,0)  FROM @XUAT

END
go
