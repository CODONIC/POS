using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace POS.Cashier
{
    public class CashierProductService
    {
        private readonly string _companyId;
        private readonly string _connectionString;

        public CashierProductService(string companyId)
        {
            _companyId = companyId;
            _connectionString = DatabaseService.ConnectionString;
        }

        public async Task<DataTable> LoadProductsAsync()
        {
            var dt = new DataTable();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                SELECT p.product_code, p.product_name, p.price, p.quantity
                FROM products p
                WHERE p.company_id = @companyId
                ORDER BY p.product_name";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));

            using var reader = await cmd.ExecuteReaderAsync();
            dt.Load(reader);
            return dt;
        }

        public DataRow FindProductByCode(DataTable products, string productCode)
        {
            return products.AsEnumerable()
                .FirstOrDefault(r => r["product_code"].ToString()
                    .Equals(productCode, StringComparison.OrdinalIgnoreCase));
        }

        public int GetAvailableStock(DataTable products, string productCode, int alreadyInCart)
        {
            var product = FindProductByCode(products, productCode);
            if (product == null) return 0;

            int availableQty = Convert.ToInt32(product["quantity"]);
            return availableQty - alreadyInCart;
        }
    }
}