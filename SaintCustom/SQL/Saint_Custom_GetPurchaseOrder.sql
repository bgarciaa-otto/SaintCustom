CREATE PROCEDURE dbo.Saint_Custom_GetPurchaseOrder
@pNumeroD VARCHAR(20),
@pCodProv VARCHAR(15)
AS
--DECLARE
--@pNumeroD VARCHAR(20) = '236',
--@pCodProv VARCHAR(15) = '03'
BEGIN 

	SET NOCOUNT ON;

	SET DATEFORMAT YMD;

	IF EXISTS (	SELECT 1
				FROM SAACXP 
				WHERE NumeroD = @pNumeroD
				AND CodProv = @pCodProv)
		THROW 50000,'La orden ya fue finalizada y no se puede cargar.', 1
		
	SELECT 
		 NumeroD AS DocumentNumber
		,CodProv AS ProviderCode
		,CodUbic AS WarehouseCode
		,FechaI AS DocumentDate
		,DATEDIFF(DAY, FechaI, FechaV) AS DueDay
	FROM SACOMP WITH (NOLOCK)  
	WHERE NumeroD = @pNumeroD
	AND CodProv = @pCodProv
	AND TipoCom = 'H'

	SELECT 
		 A.CodItem AS Code
		,A.Descrip1 AS Description
		,A.ExistAnt AS Stock
		,A.Cantidad AS Quantity
		,A.Costo AS Cost
		,A.Precio1 AS Price
		,CAST(B.CodInst AS VARCHAR) AS ClassificationCode
		,A.NroLinea AS LineNumber
	FROM SAITEMCOM A WITH (NOLOCK)
	INNER JOIN SAPROD B WITH (NOLOCK)
	ON A.CodItem = B.CodProd
	WHERE A.NumeroD = @pNumeroD
	AND A.CodProv = @pCodProv
	AND A.TipoCom = 'H'

END