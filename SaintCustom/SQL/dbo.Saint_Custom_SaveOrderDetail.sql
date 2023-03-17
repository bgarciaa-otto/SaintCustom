ALTER DATABASE EnterpriseAdminDb SET COMPATIBILITY_LEVEL = 130
GO
CREATE PROCEDURE dbo.Saint_Custom_SaveOrderDetail
@pNumeroD VARCHAR(20),
@pCodProv VARCHAR(15),
@pCodUbi VARCHAR(10),
@pProducts NVARCHAR(MAX)
AS
BEGIN

	SET NOCOUNT ON;
	
	SET DATEFORMAT YMD;
		
	DECLARE @pCodProd VARCHAR(15),
			@pDescrip VARCHAR(40),
			@pCosto DECIMAL(28, 4),
			@pPrecio DECIMAL(28, 4),
			@pCantidad DECIMAL(28, 4)
 	
	DECLARE db_cursor CURSOR READ_ONLY FOR  
		SELECT 
			CodProd,
			Descrip,
			Costo,
			Precio,
			Cantidad
		FROM OPENJSON(@pProducts, '$')
		WITH (  CodProd VARCHAR(15)  '$.Code',
				Descrip VARCHAR(40) '$.Description',
				Costo DECIMAL(28, 4) '$.Cost',
				Precio DECIMAL(28, 4) '$.Price',
				Cantidad DECIMAL(28, 4) '$.Quantity'
		) AS Products
 
	OPEN db_cursor   
	FETCH NEXT FROM db_cursor 
		INTO	@pCodProd,
				@pDescrip,
				@pCosto,
				@pPrecio,
				@pCantidad   
 
	WHILE @@FETCH_STATUS = 0   
	BEGIN   
			
		EXEC dbo.Saint_Custom_SaveOrderProduct	@pNumeroD,
												@pCodProv,
												@pCodUbi,
												@pCodProd,
												@pDescrip,
												@pCosto,
												@pPrecio,
												@pCantidad   

		FETCH NEXT FROM db_cursor 
			INTO	@pCodProd,
					@pDescrip,
					@pCosto,
					@pPrecio,
					@pCantidad   
	END   
	 
	CLOSE db_cursor   
	DEALLOCATE db_cursor	


END