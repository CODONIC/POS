using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Cashier
{
    public partial class QuantityDialog : Form
    {
        private TextBox txtQuantity;
        private Button btnOk;
        private Button btnCancel;
        private Label lblPrompt;

        private int _maxQuantity;
        public int Quantity { get; private set; }

        public QuantityDialog(string productName, int maxQuantity, string confirmButtonText = "Add to Cart")
        {
            Text = "Enter Quantity";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new System.Drawing.Size(300, 130);

            lblPrompt = new Label
            {
                Text = $"{productName}",
                AutoSize = false,
                Size = new System.Drawing.Size(280, 36),
                Location = new System.Drawing.Point(10, 10)
            };

            txtQuantity = new TextBox
            {
                Location = new System.Drawing.Point(10, 55),
                Size = new System.Drawing.Size(280, 24),
                MaxLength = 10
            };

            // ─── Block everything except digits and control keys ───────────────
            txtQuantity.KeyPress += (s, ev) =>
            {
                if (char.IsControl(ev.KeyChar)) return;
                if (!char.IsDigit(ev.KeyChar))
                    ev.Handled = true;
            };

            // ─── Trigger validation on Enter key ──────────────────────────────
            txtQuantity.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    btnOk.PerformClick();
                    ev.Handled = true;
                    ev.SuppressKeyPress = true;
                }
            };

            btnOk = new Button
            {
                Text = confirmButtonText,
                Location = new System.Drawing.Point(120, 90),
                Size = new System.Drawing.Size(90, 28)
            };

            btnOk.Click += (s, ev) =>
            {
                string raw = txtQuantity.Text.Trim();

                if (!int.TryParse(raw, out int typedValue) || typedValue < 1)
                {
                    MessageBox.Show("Please enter a valid quantity.",
                        "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Clear();
                    txtQuantity.Focus();
                    return;
                }

                if (typedValue > maxQuantity)
                {
                    MessageBox.Show($"Quantity exceeds available stock. Maximum available is {maxQuantity}.",
                        "Stock Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Clear();
                    txtQuantity.Focus();
                    return;
                }

                Quantity = typedValue;
                DialogResult = DialogResult.OK;
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(220, 90),
                Size = new System.Drawing.Size(70, 28)
            };

            Controls.AddRange(new Control[] { lblPrompt, txtQuantity, btnOk, btnCancel });
            CancelButton = btnCancel;
        }
    }
}