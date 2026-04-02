using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace POS.Admin
{
    public partial class ManageStocks : BaseForm
    {
        private string _username;
        private string _companyName;
        public ManageStocks(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName} ";
            this.KeyPreview = true;
            this.KeyDown += ManageStocksFrm_KeyDown;
            ShortcutKeyHints();
        }
        private void btnAddStock_Click(object sender, EventArgs e)
        {
            //Code here
        }
        private void btnRemoveStock_Click(object sender, EventArgs e)
        {
            //Code Here
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Code here
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Code here
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard(_username, _companyName);
            adminDashboard.Show();
            this.Close();
        }
        // ─── Shortcut Keys ────────────────────────────────────────────────────────────

        private void ManageStocksFrm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    btnBack_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F1:
                    btnAddStock_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F2:
                    btnRemoveStock_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F3:
                    btnSave_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F4:
                    btnCancel_Click(sender, e);
                    e.Handled = true;
                    break;
            }
        }

        private void ShortcutKeyHints()
        {
            //Shortcut keys:

            ToolTip toolTip = new ToolTip();
            toolTip.InitialDelay = 200; // ms before tooltip appears
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(btnBack, "ESC");
            toolTip.SetToolTip(btnAddStock, "F1");
            toolTip.SetToolTip(btnRemoveStock, "F2");
            toolTip.SetToolTip(btnSave, "F3");
            toolTip.SetToolTip(btnCancel, "F4");
            AttachHoverEffect(btnBack, "BACK", "ESC");
            AttachHoverEffect(btnAddStock, "ADD STOCK", "F1");
            AttachHoverEffect(btnRemoveStock, "REMOVE STOCK", "F2");
            AttachHoverEffect(btnSave, "SAVE", "F3");
            AttachHoverEffect(btnCancel, "CANCEL", "F4");
        }
        private void AttachHoverEffect(Button btn, string defaultText, string shortcut)
        {
            Point originalLocation = btn.Location;

            btn.MouseEnter += (s, e) =>
            {
                btn.Text = $"{defaultText}\n({shortcut})";
                btn.Location = new Point(originalLocation.X, originalLocation.Y - 3);
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.Text = defaultText;
                btn.Location = originalLocation;
            };
        }

        
    }
}
