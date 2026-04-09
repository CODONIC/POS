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
    public partial class PaymentFrm : BaseForm
    {
        private string _username;
        private string _companyName;
        public PaymentFrm(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            lblCashierName.Text = $"{_username} | Cashier";
            titleLabel.Text = $"{_companyName} ";
        }

        public override void CloseButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        
    }
}
