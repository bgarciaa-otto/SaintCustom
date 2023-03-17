CREATE PROCEDURE dbo.Saint_Custom_UpdateProduct
@pCodProd VARCHAR(15),
@pDescrip VARCHAR(40),
@pCodInst INT,
@pCosto DECIMAL(28, 4),
@pPrecio DECIMAL(28, 4),
@pCantidad DECIMAL(28, 4)
AS
BEGIN

	SET NOCOUNT ON;

	SET DATEFORMAT YMD;

	DECLARE @CostAnt DECIMAL(28,4),
			@CostPro DECIMAL(28,4)
				
	SELECT 
		@CostAnt = CostAct
	FROM SAPROD
	WHERE CodProd = @pCodProd
	
	UPDATE SAPROD WITH (ROWLOCK)
		SET Descrip = @pDescrip,
			CodInst = @pCodInst,
			CostAct = @pCosto,
			CostPro = ISNULL((CASE 
									WHEN CostPro = 0
										THEN @pCosto * 1
									ELSE (
										CASE 
											WHEN (Existen * 1 + ExUnidad) + @pCantidad > 0
												THEN (((CostPro / 1) * (Existen * 1 + ExUnidad)) + (@pCosto * @pCantidad)) / ((Existen * 1 + ExUnidad) + @pCantidad) * 1
											ELSE @pCosto * 1
										END
									)
								  END
							), 0),
			CostAnt = @CostAnt,
			Precio1 = @pPrecio,
			Precio2 = @pPrecio,
			Precio3 = @pPrecio
	WHERE CodProd = @pCodProd

END