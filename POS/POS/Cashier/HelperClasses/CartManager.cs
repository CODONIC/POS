using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace POS.Cashier
{
    public class CartManager
    {
        private DataTable _cartTable;
        private bool _transactionStarted;

        public DataTable CartTable => _cartTable;
        public bool IsTransactionStarted => _transactionStarted;

        public CartManager()
        {
            InitializeCartTable();
        }

        private void InitializeCartTable()
        {
            _cartTable = new DataTable();
            _cartTable.Columns.Add("product_code", typeof(string));
            _cartTable.Columns.Add("product_name", typeof(string));
            _cartTable.Columns.Add("price", typeof(decimal));
            _cartTable.Columns.Add("quantity", typeof(int));
            _cartTable.Columns.Add("subtotal", typeof(decimal));
        }

        public void AddItem(string productCode, string productName, decimal price, int quantity)
        {
            var existingRow = _cartTable.AsEnumerable()
                .FirstOrDefault(r => r["product_code"].ToString() == productCode);

            if (existingRow != null)
            {
                int newQty = Convert.ToInt32(existingRow["quantity"]) + quantity;
                existingRow["quantity"] = newQty;
                existingRow["subtotal"] = newQty * price;
            }
            else
            {
                _cartTable.Rows.Add(productCode, productName, price, quantity, quantity * price);
            }

            _transactionStarted = true;
        }

        public void RemoveItem(string productCode, int removeQty, decimal price)
        {
            var cartRow = _cartTable.AsEnumerable()
                .FirstOrDefault(r => r["product_code"].ToString() == productCode);

            if (cartRow == null) return;

            int currentQty = Convert.ToInt32(cartRow["quantity"]);
            int newQty = currentQty - removeQty;

            if (newQty <= 0)
            {
                _cartTable.Rows.Remove(cartRow);
            }
            else
            {
                cartRow["quantity"] = newQty;
                cartRow["subtotal"] = newQty * price;
            }

            if (_cartTable.Rows.Count == 0)
            {
                ResetTransaction();
            }
        }

        public void ClearCart()
        {
            _cartTable.Rows.Clear();
            ResetTransaction();
        }

        public void ResetTransaction()
        {
            _cartTable.Rows.Clear();
            _transactionStarted = false;
        }

        public bool IsEmpty => _cartTable.Rows.Count == 0;
    }
}