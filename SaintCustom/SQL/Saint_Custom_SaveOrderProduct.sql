CREATE PROCEDURE dbo.Saint_Custom_SaveOrderProduct
@pNumeroD VARCHAR(20),
@pCodProv VARCHAR(15),
@pCodUbi VARCHAR(10),
@pCodProd VARCHAR(15),
@pDescrip VARCHAR(40),
@pCosto DECIMAL(28, 4),
@pPrecio DECIMAL(28, 4),
@pCantidad DECIMAL(28, 4)
AS
BEGIN

	SET NOCOUNT ON;
	
	SET DATEFORMAT YMD;

	BEGIN TRY

		BEGIN TRANSACTION COMPRA
				
		DECLARE	@NumErrors INT = 0;		
		
		---------------------------------------------------------------------------------
		--									Order Detail
		---------------------------------------------------------------------------------

		-- Inventario
		DECLARE @UCostAct DECIMAL(28,4),
				@UCostAnt DECIMAL(28,4),
				@UCostPro DECIMAL(28,4), 
				@NCostAct DECIMAL(28,4),
				@NCostAnt DECIMAL(28,4),
				@NCostPro DECIMAL(28,4)
				
		SET @NumErrors = @NumErrors + @@ERROR;

		UPDATE SAPROD WITH (ROWLOCK)
			SET @UCostAct = CostAct,
				@UCostAnt = CostAnt,
				@UCostPro = CostPro,
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
				CostAnt = CostAct,
				CostAct = @pCosto,
				@NCostAct = CostAct,
				@NCostAnt = CostAnt,
				@NCostPro = CostPro,
				FechaUC = GETDATE()
		WHERE CodProd = @pCodProd
		
		SET @NumErrors = @NumErrors + @@ERROR;

		UPDATE SAPROD WITH (ROWLOCK)
			SET SAPROD.COSTACT = SAPROD.COSTACT - @UCostAct + @NCostAct,
				SAPROD.COSTANT = SAPROD.COSTANT - @UCostAnt + @NCostAnt,
				SAPROD.COSTPRO = SAPROD.COSTPRO - @UCostPro + @NCostPro
		FROM SAPART
		WHERE SAPART.CODPROD = SAPROD.CODPROD
		AND SAPART.CODALTE = @pCodProd
		
		SET @NumErrors = @NumErrors + @@ERROR;


		-- Estadisticas_inventario
		DECLARE @Periodo VARCHAR(6) = CONVERT(VARCHAR(6), GETDATE(), 112)

		SET @NumErrors = @NumErrors + @@ERROR;
		
		IF NOT EXISTS (	SELECT 1
						FROM SAEPRD WITH (NOLOCK)
						WHERE CodProd = @pCodProd
						AND Periodo = @Periodo)
			INSERT INTO SAEPRD (CodProd, Periodo)
			VALUES (@pCodProd, @Periodo)
		
		SET @NumErrors = @NumErrors + @@ERROR;

		UPDATE SAEPRD WITH (ROWLOCK)
			SET MtoCompra = MtoCompra + (@pCosto * @pCantidad),
				CntCompra = CntCompra + @pCantidad
		WHERE CodProd = @pCodProd
		AND Periodo = @Periodo
		
		SET @NumErrors = @NumErrors + @@ERROR;
				

		-- Proveedores Producto
		IF NOT EXISTS (	SELECT 1
						FROM SAPVPR WITH (NOLOCK)
						WHERE TipoCom = 'H'
						AND CodProd = @pCodProd
						AND CodProv = @pCodProv)
			INSERT INTO SAPVPR (TipoCom, CodProd, CodProv)
			VALUES ('H', @pCodProd, @pCodProv)
			
		SET @NumErrors = @NumErrors + @@ERROR;

		UPDATE SAPVPR WITH (ROWLOCK)
			SET Cantidad = @pCantidad,
				Costo = @pCosto,
				FechaE = GETDATE(),
				NumeroD = @pNumeroD
		WHERE TipoCom = 'H'
			AND CodProd = @pCodProd
			AND CodProv = @pCodProv

		SET @NumErrors = @NumErrors + @@ERROR;

			
		-- Existencias_inventario
		DECLARE @ExistAnt DECIMAL(28,4),
				@ExistAntUnd DECIMAL(28,4),
				@FechaActual VARCHAR(10)

		SELECT
			@ExistAnt = Existen,
			@ExistAntUnd = ExUnidad
		FROM SAEXIS WITH (NOLOCK)
		WHERE CodProd = @pCodProd
		AND CodUbic = @pCodUbi;

		SET @NumErrors = @NumErrors + @@ERROR;

		SET @FechaActual = CONVERT(VARCHAR(10), GETDATE(), 126)

		SET @NumErrors = @NumErrors + @@ERROR;

		EXEC TR_ADM_UPDATE_EXISTENCIAS	@pCodProd,
										@pCodUbi,
										@pCantidad,
										0,
										@FechaActual

		SET @NumErrors = @NumErrors + @@ERROR;
		

		-- Items_Compra
		DECLARE @CodSucu VARCHAR(15) = '00000'	
		SELECT TOP(1) 
			@CodSucu = CodSucu 
		FROM SASUCURSAL

		SET @NumErrors = @NumErrors + @@ERROR;

		DECLARE @NroLinea INT = 1
		SELECT TOP(1)
			@NroLinea = NroLinea + 1
		FROM SAITEMCOM WITH (NOLOCK)
		WHERE NumeroD = @pNumeroD
		AND CodProv = @pCodProv
		AND TipoCom = 'H'
		ORDER BY NroLinea DESC
		
		SET @NumErrors = @NumErrors + @@ERROR;

		INSERT INTO SAITEMCOM (
			CodSucu,
			Signo,
			CodProv,
			TipoCom,
			NumeroD,
			NroLinea,
			FechaE,
			CodItem,
			CodUbic,
			Descrip1,
			Cantidad,
			Costo,
			Precio1,
			Precio2,
			Precio3,
			Precio4,
			Precio5,
			Precio6,
			TotalItem,
			EsExento,
			NroUnicoL,
			FechaL,
			ExistAntU,
			ExistAnt)
		VALUES (
			@CodSucu,
			1,
			@pCodProv,
			'H',
			@pNumeroD,
			@NroLinea,
			GETDATE(),
			@pCodProd,
			@pCodUbi,
			@pDescrip,
			@pCantidad,
			@pCosto,
			@pPrecio,
			@pPrecio,
			@pPrecio,
			0,
			0,
			0,
			(@pCosto * @pCantidad),
			1,
			0,
			GETDATE(),
			ISNULL(@ExistAntUnd, 0),
			ISNULL(@ExistAnt, 0)
			)
			
		SET @NumErrors = @NumErrors + @@ERROR;


		IF @NUMERRORS > 0
		BEGIN
			ROLLBACK TRANSACTION COMPRA
		END
		ELSE
		BEGIN
			COMMIT TRANSACTION COMPRA;

			SELECT @NroLinea AS NroLinea
		END

	END TRY
	BEGIN CATCH

		ROLLBACK TRANSACTION COMPRA

	END CATCH

END