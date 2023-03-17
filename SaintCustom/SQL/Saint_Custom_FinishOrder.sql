CREATE PROCEDURE dbo.Saint_Custom_FinishOrder
@pNumeroD VARCHAR(20),
@pCodProv VARCHAR(15),
@pFechaD DATETIME,
@pDiasV INT,
@pEstacion VARCHAR(100)
AS
BEGIN

	SET NOCOUNT ON;
	
	SET DATEFORMAT YMD;

	BEGIN TRY

		BEGIN TRANSACTION COMPRA
				
		DECLARE	@NumErrors INT = 0;		
				
		---------------------------------------------------------------------------------
		--								Order Header
		---------------------------------------------------------------------------------
		
		-- Compras
		DECLARE @Monto DECIMAL(28,4)

		SELECT 
			@Monto = SUM(Cantidad * Costo)
		FROM SAITEMCOM WITH (NOLOCK)
		WHERE NumeroD = @pNumeroD
		AND CodProv = @pCodProv
		AND TipoCom = 'H'
		
		SET @NumErrors = @NumErrors + @@ERROR;
		
		UPDATE SACOMP 
		SET 
			FechaI = @pFechaD,
			FechaV = DATEADD(DAY, @pDiasV, @pFechaD),
			Monto = @Monto,
			TExento = @Monto,
			TotalPrd = @Monto,
			MtoTotal = @Monto,
			Credito = @Monto
		WHERE NumeroD = @pNumeroD
		AND CodProv = @pCodProv
		AND TipoCom = 'H'
	
		SET @NumErrors = @NumErrors + @@ERROR;


		-- Estadistica_compras
		DECLARE @PeriodoFull VARCHAR(8)

		SET @PeriodoFull = CONVERT(VARCHAR(8), GETDATE(), 112)

		SET @NumErrors = @NumErrors + @@ERROR;
		
		DECLARE @CodSucu VARCHAR(15) = '00000'	
		SELECT TOP(1) 
			@CodSucu = CodSucu 
		FROM SASUCURSAL

		SET @NumErrors = @NumErrors + @@ERROR;

		IF NOT EXISTS (	SELECT 1
						FROM SAECOM WITH (NOLOCK)
						WHERE CodSucu = @CodSucu
						AND Periodo = @PeriodoFull)
			INSERT INTO SAECOM (CodSucu, Periodo)
			VALUES (@CodSucu, @PeriodoFull)
			
		SET @NumErrors = @NumErrors + @@ERROR;

		UPDATE SAECOM WITH (ROWLOCK)
			SET MtoCompra = MtoCompra + @Monto,
				MtoTax = MtoTax + 0,
				Contado = Contado + 0,
				Credito = Credito + @Monto,
				Descto = Descto + 0,
				Fletes = Fletes + 0,
				NroComps = NroComps + 1,
				NroDevol = NroDevol + 0
		WHERE CodSucu = @CodSucu
		AND Periodo = @PeriodoFull
		
		SET @NumErrors = @NumErrors + @@ERROR;


		-- Cuentas_pagar
		DECLARE @NombreProveedor VARCHAR(60)
		SELECT 
			@NombreProveedor = Descrip
		FROM SAPROV
		WHERE CodProv = @pCodProv

		SET @NumErrors = @NumErrors + @@ERROR;

		INSERT INTO SAACXP (
			CodSucu,
			CodProv,
			NumeroD,
			CodUsua,
			CodEsta,
			TipoCxP,
			Descrip,
			ID3,
			FechaT,
			Document,
			FechaI,
			FechaE,
			FechaV,
			MontoNeto,
			TExento,
			Factor,
			Monto,
			Saldo,
			SaldoOrg,
			EsLibroI
			)
		VALUES (
			@CodSucu,
			@pCodProv,
			@pNumeroD,
			'001',
			@pEstacion,
			'10',
			@NombreProveedor,
			@pCodProv,
			GETDATE(),
			CONCAT(@pNumeroD, ' '),
			@pFechaD,
			GETDATE(),
			DATEADD(DAY, @pDiasV, @pFechaD),
			@Monto,
			@Monto,
			1,
			@Monto,
			@Monto,
			@Monto,
			1
			)
			
		SET @NumErrors = @NumErrors + @@ERROR;

		DECLARE @NroUnicoCXP NUMERIC(18,0)
		SET @NroUnicoCXP = IDENT_CURRENT('SAACXP')

		SET @NumErrors = @NumErrors + @@ERROR;
		
		
		-- Proveedores
		UPDATE SAPROV WITH (ROWLOCK)
			SET Saldo = Saldo + @Monto,
				PagosA = PagosA + 0,
				FechaUC = @pFechaD,
				MontoUC = @Monto,
				NumeroUC = @pNumeroD
		WHERE CodProv = @pCodProv
		
		SET @NumErrors = @NumErrors + @@ERROR;


		-- Estadisticas_proveedores
		DECLARE @Periodo VARCHAR(6) = CONVERT(VARCHAR(6), GETDATE(), 112)

		SET @NumErrors = @NumErrors + @@ERROR;

		IF NOT EXISTS (	SELECT 1
						FROM SAEPRV WITH (NOLOCK)
						WHERE CodProv = @pCodProv
						AND Periodo = @Periodo)
			INSERT INTO SAEPRV (CodProv, Periodo)
			VALUES (@pCodProv, @Periodo)
				
		SET @NumErrors = @NumErrors + @@ERROR;

		UPDATE SAEPRV WITH (ROWLOCK)
			SET NroComps = NroComps + 1,
				NroDevol = NroDevol + 0,
				Credito = Credito + @Monto,
				Contado = Contado + 0,
				MtoDevol = MtoDevol + 0,
				MtoRetenImp = MtoRetenImp + 0
		WHERE CodProv = @pCodProv
		AND Periodo = @Periodo

		SET @NumErrors = @NumErrors + @@ERROR;


		IF @NUMERRORS > 0
		BEGIN
			ROLLBACK TRANSACTION COMPRA
		END
		ELSE
		BEGIN
			COMMIT TRANSACTION COMPRA;
		END

	END TRY
	BEGIN CATCH

		ROLLBACK TRANSACTION COMPRA

	END CATCH

END