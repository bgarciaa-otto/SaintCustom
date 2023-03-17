CREATE PROCEDURE dbo.Saint_Custom_CreatePurchaseOrderHeader
@pCodProv VARCHAR(15),
@pCodUbi VARCHAR(10),
@pEstacion VARCHAR(100)
AS
BEGIN

	SET NOCOUNT ON;
	
	SET DATEFORMAT YMD;

	DECLARE @CodSucu VARCHAR(15) = '00000'	
	SELECT TOP(1) 
		@CodSucu = CodSucu 
	FROM SASUCURSAL

	DECLARE @NumeroD VARCHAR(20) = '1'
	SELECT TOP(1)
		 @NumeroD = CAST(NumeroD AS NUMERIC) + 1
	FROM SACOMP WITH (NOLOCK)  
	WHERE CodProv = @pCodProv
	AND TipoCom = 'H'
	ORDER BY CAST(NumeroD AS NUMERIC) DESC

	DECLARE @NombreProveedor VARCHAR(60)
	SELECT 
		@NombreProveedor = Descrip
	FROM SAPROV
	WHERE CodProv = @pCodProv

	INSERT INTO SACOMP (
		[CodSucu],
		[TipoCom],
		[NumeroD],
		[Signo],
		[CodUsua],
		[CodEsta],
		[FechaT],
		[CodProv],
		[CodUbic],
		[Descrip],
		[Factor],
		[ID3],
		[FechaE],
		[Monto],
		[TExento],
		[TotalPrd],
		[MtoTotal],
		[Credito])
	VALUES (
		@CodSucu,
		'H',
		@NumeroD,
		1,
		'001',
		@pEstacion,
		GETDATE(),
		@pCodProv,
		@pCodUbi,
		@NombreProveedor,
		1,
		@pCodProv,
		GETDATE(),
		0,
		0,
		0,
		0,
		0
		)

	SELECT @NumeroD AS NumeroD

END