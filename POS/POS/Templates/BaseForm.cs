using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public class BaseForm : Form
    {
        private Point mouseOffset;
        private string _username;
        private string _companyId;

        // Call this in any child form's constructor to wire up a title bar
        protected void InitializeTitleBar(Button closeButton, params Control[] draggableControls)
        {
            foreach (var control in draggableControls)
                control.MouseDown += TitleBar_MouseDown;

            if (closeButton != null)
                closeButton.Click += CloseButton_Click;
        }

        // Method to set user context for audit logging
        protected void SetUserContext(string username, string companyId)
        {
            _username = username;
            _companyId = companyId;
        }

        public virtual async void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to exit?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                await RecordLogoutAsync();
                Application.Exit();
            }
        }

        // Override the FormClosing event to catch the X button click
        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            // Check if this is the main form being closed (not just hiding)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Check if this form is the main application window
                bool isMainForm = Application.OpenForms.Count == 1 ||
                                 (Application.OpenForms[0] == this && Application.OpenForms.Count > 0);

                if (isMainForm)
                {
                    DialogResult confirm = MessageBox.Show(
                        "Are you sure you want to exit?",
                        "Confirm Exit",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirm == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }

                    await RecordLogoutAsync();
                }
            }

            base.OnFormClosing(e);
        }

        private async Task RecordLogoutAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(_companyId) && !string.IsNullOrEmpty(_username))
                {
                    await AuditService.LogLogoutAsync(
                        username: _username,
                        companyId: _companyId,
                        deviceInfo: Environment.MachineName
                    );
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Logout audit failed: {ex.Message}");
            }
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = new Point(-e.X, -e.Y);
                var titleBar = sender as Control;
                titleBar.MouseMove += TitleBar_MouseMove;
                titleBar.MouseUp += TitleBar_MouseUp;
            }
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            var titleBar = sender as Control;
            titleBar.MouseMove -= TitleBar_MouseMove;
            titleBar.MouseUp -= TitleBar_MouseUp;
        }
    }
}