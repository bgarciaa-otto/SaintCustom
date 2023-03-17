using Newtonsoft.Json;
using SaintCustom.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SaintCustom.Tools
{
    internal class Methods
    {
        private DataTable ExecuteSQL(string query) => new Connection().ExecuteSQL(query).Tables[0];
        public List<DropDown> GetProviders()
        {
            var query = @"
            SELECT 
	             CodProv AS Code
	            ,Descrip AS Description
                ,CONCAT(CodProv, ' - ', Descrip) AS FullDescription
            FROM SAPROV WITH (NOLOCK)   
            WHERE (ACTIVO=1) 
            ORDER BY CodProv";

            return ExecuteSQL(query).ConvertToList<DropDown>();
        }

        public List<DropDown> GetWarehouses()
        {
            var query = @"
            SELECT
	             CodUbic AS Code
	            ,Descrip AS Description
                ,CONCAT(CodUbic, ' - ', Descrip) AS FullDescription
            FROM SADEPO WITH (NOLOCK)   
            WHERE (ACTIVO=1) 
            ORDER BY CodUbic";

            return ExecuteSQL(query).ConvertToList<DropDown>();
        }

        public List<Product> GetProducts(string code = null, string description = null)
        {
            var codProd = string.IsNullOrWhiteSpace(code) ? null : $"AND CodProd LIKE '%{code}%'";
            var descrip = string.IsNullOrWhiteSpace(description) ? null : $"AND Descrip LIKE '%{description}%'";

            var query = $@"
            SELECT 
	             CodProd AS Code
	            ,Descrip AS Description
	            ,Existen AS Stock
	            ,CostAct AS Cost
	            ,Precio1 AS Price
	            ,CAST(CodInst AS VARCHAR) AS ClassificationCode
            FROM SAPROD WITH (NOLOCK)
            WHERE Activo = 1
            {codProd}
            {descrip}
            ORDER BY CodProd";

            return ExecuteSQL(query).ConvertToList<Product>();
        }

        public List<DropDown> GetClassifications()
        {
            var query = @"
            SELECT
	             CAST(CodInst AS VARCHAR) AS Code
	            ,Descrip AS Description
	            ,CONCAT(CodInst, ' - ', Descrip) AS FullDescription
            FROM SAINSTA 
            WHERE TipoIns = 0 
            ORDER BY Descrip";

            return ExecuteSQL(query).ConvertToList<DropDown>();
        }

        public string SaveProduct(Product product)
        {
            var parameters = new List<Parameter>
            {
                new Parameter("pDescrip", SqlDbType.VarChar, product.Description),
                new Parameter("pCodInst", SqlDbType.Int, int.Parse(product.ClassificationCode)),
                new Parameter("pCosto", SqlDbType.Decimal, product.Cost),
                new Parameter("pPrecio", SqlDbType.Decimal, product.Price),
            };

            var code = new Connection().ExecuteScalarSP("dbo.Saint_Custom_CreateProduct", parameters);
            return $"{code}";
        }

        public string SavePurchaseOrderHeader(string providerCode, string warehouseCode)
        {
            var parameters = new List<Parameter>
            {
                new Parameter("pCodProv", SqlDbType.VarChar, providerCode),
                new Parameter("pCodUbi", SqlDbType.VarChar, warehouseCode),
                new Parameter("pEstacion", SqlDbType.VarChar, Environment.MachineName)
            };

            var numeroD = new Connection().ExecuteScalarSP("dbo.Saint_Custom_CreatePurchaseOrderHeader", parameters);
            return $"{numeroD}";
        }

        internal Document GetPurchaseOrder(string documentNumber, string providerCode)
        {
            var parameters = new List<Parameter>
            {
                new Parameter("pNumeroD", SqlDbType.VarChar, documentNumber),
                new Parameter("pCodProv", SqlDbType.VarChar, providerCode)
            };

            var ds = new Connection().ExecuteSP("dbo.Saint_Custom_GetPurchaseOrder", parameters);

            var document = ds.Tables[0].ConvertToList<Document>().FirstOrDefault();
            if (document == null) return null;
            document.Products = ds.Tables[1].ConvertToList<Product>();
            return document;
        }

        internal void SaveOrderDetail(string documentNumber, string providerCode, string warehouseCode, List<Product> products)
        {
            var parameters = new List<Parameter>
            {
                new Parameter("pNumeroD", SqlDbType.VarChar, documentNumber),
                new Parameter("pCodProv", SqlDbType.VarChar, providerCode),
                new Parameter("pCodUbi", SqlDbType.VarChar, warehouseCode),
                new Parameter("pProducts", SqlDbType.NVarChar, JsonConvert.SerializeObject(products))
            };

            new Connection().ExecuteScalarSP("dbo.Saint_Custom_SaveOrderDetail", parameters);
        }

        internal void FinishOrder(string documentNumber, string providerCode, DateTime documentDate, string dueDay)
        {
            var parameters = new List<Parameter>
            {
                new Parameter("pNumeroD", SqlDbType.VarChar, documentNumber),
                new Parameter("pCodProv", SqlDbType.VarChar, providerCode),
                new Parameter("pFechaD", SqlDbType.DateTime, documentDate),
                new Parameter("pDiasV", SqlDbType.Int, int.Parse(dueDay)),
                new Parameter("pEstacion", SqlDbType.VarChar, Environment.MachineName)
            };

            new Connection().ExecuteNonQuerySP("dbo.Saint_Custom_FinishOrder", parameters);
        }

        internal void UpdateProduct(Product product)
        {
            var parameters = new List<Parameter>
            {
                new Parameter("pCodProd", SqlDbType.VarChar, product.Code),
                new Parameter("pDescrip", SqlDbType.VarChar, product.Description),
                new Parameter("pCodInst", SqlDbType.Int, int.Parse(product.ClassificationCode)),
                new Parameter("pCosto", SqlDbType.Decimal, product.Cost),
                new Parameter("pPrecio", SqlDbType.Decimal, product.Price),
                new Parameter("pCantidad", SqlDbType.Decimal, product.Quantity),
            };

            new Connection().ExecuteScalarSP("dbo.Saint_Custom_UpdateProduct", parameters);
        }
    }
}
