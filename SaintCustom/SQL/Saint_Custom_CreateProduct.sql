CREATE PROCEDURE dbo.Saint_Custom_CreateProduct
@pDescrip VARCHAR(40),
@pCodInst INT,
@pCosto DECIMAL(28, 4),
@pPrecio DECIMAL(28, 4)
AS
BEGIN

	SET NOCOUNT ON;

	SET DATEFORMAT YMD;

	DECLARE @CodProd VARCHAR(15) = '001000'

	SELECT TOP(1) 
		@CodProd = RIGHT(CONCAT('000000', CAST(CodProd AS NUMERIC) + 1), 6)
	FROM SAPROD 
	WITH (NOLOCK)
	ORDER BY CAST(CodProd AS NUMERIC) DESC
	
	INSERT INTO SAPROD (
		[CodProd],
		[Descrip],
		[CodInst],
		[CostAct],
		[CostPro],
		[CostAnt],
		[Precio1],
		[Precio2],
		[Precio3],
		[EsExento],
		[Activo]
		)
	VALUES (
		@CodProd,
		@pDescrip,
		@pCodInst,
		@pCosto,
		@pCosto,
		@pCosto,
		@pPrecio,
		@pPrecio,
		@pPrecio,
		1,
		1
		)

	INSERT INTO SAIPRD (CodProd, Imagen)
	VALUES (@CodProd, NULL)

	INSERT INTO SACODBAR (CodProd, CodAlte)
	VALUES (@CodProd, @CodProd)	
	
	SELECT @CodProd AS CodProd

END