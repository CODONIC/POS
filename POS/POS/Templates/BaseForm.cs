using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public class BaseForm : Form
    {
        protected string CurrentUsername { get; private set; }
        protected string CurrentUserId { get; private set; }
        protected string CurrentSessionToken { get; private set; }
        private System.Windows.Forms.Timer _sessionTimer;
        private LoginService _loginService = new LoginService();
        private Point mouseOffset;
        private string _username;
        private string _companyId;
        private bool _isLoggingOut = false;
        private bool _sessionExpiredShown = false;
        private static bool _isAppExiting = false;

        protected void SetUserContext(string username, string userId, string sessionToken)
        {
            CurrentUsername = username;
            CurrentUserId = userId;
            CurrentSessionToken = sessionToken;
            _isLoggingOut = false;
            _sessionExpiredShown = false;

            // Start session validation timer
            if (_sessionTimer == null && !_isLoggingOut && !_isAppExiting)
            {
                _sessionTimer = new System.Windows.Forms.Timer();
                _sessionTimer.Interval = 30000; // Check every 30 seconds
                _sessionTimer.Tick += OnTimerTick;
                _sessionTimer.Start();
            }
        }

        protected void SetUserContext(string username, string companyId)
        {
            _username = username;
            _companyId = companyId;
        }

        public void StopSessionMonitoring()
        {
            _isLoggingOut = true;
            _isAppExiting = true;

            if (_sessionTimer != null)
            {
                _sessionTimer.Enabled = false;
                _sessionTimer.Stop();
                _sessionTimer.Tick -= OnTimerTick;
                _sessionTimer.Dispose();
                _sessionTimer = null;
            }
        }

        private async void OnTimerTick(object sender, EventArgs e)
        {
            // Immediately disable the timer to prevent multiple calls
            if (_sessionTimer != null)
            {
                _sessionTimer.Enabled = false;
            }

            await ValidateCurrentSession();

            // Re-enable if still active
            if (_sessionTimer != null && !_isLoggingOut && !_isAppExiting && !_sessionExpiredShown)
            {
                _sessionTimer.Enabled = true;
            }
        }

        private async Task ValidateCurrentSession()
        {
            // Don't validate if we're logging out or session already expired
            if (_isLoggingOut || _sessionExpiredShown || _isAppExiting) return;

            if (string.IsNullOrEmpty(CurrentUserId) || string.IsNullOrEmpty(CurrentSessionToken))
                return;

            bool isValid = await _loginService.ValidateSessionAsync(CurrentUserId, CurrentSessionToken);

            if (!isValid)
            {
                _isLoggingOut = true;
                _sessionExpiredShown = true;
                _isAppExiting = true;

                if (_sessionTimer != null)
                {
                    _sessionTimer.Enabled = false;
                    _sessionTimer.Stop();
                    _sessionTimer.Tick -= OnTimerTick;
                    _sessionTimer.Dispose();
                    _sessionTimer = null;
                }

                if (InvokeRequired)
                {
                    Invoke(new Action(() => ShowSessionExpiredMessage()));
                }
                else
                {
                    ShowSessionExpiredMessage();
                }
            }
        }

        private void ShowSessionExpiredMessage()
        {
            MessageBox.Show(
                "Your session has expired because you logged in from another device.\n\n" +
                "The application will now close.",
                "Session Expired",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopSessionMonitoring();
            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            StopSessionMonitoring();
            base.OnFormClosed(e);
        }

        // Title bar functionality
        protected void InitializeTitleBar(Button closeButton, params Control[] draggableControls)
        {
            foreach (var control in draggableControls)
                control.MouseDown += TitleBar_MouseDown;

            if (closeButton != null)
                closeButton.Click += CloseButton_Click;
        }

        public virtual async void CloseButton_Click(object sender, EventArgs e)
        {
            StopSessionMonitoring();

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to exit?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                await RecordLogoutAsync();
                Application.Exit();
            }
            else
            {
                _isLoggingOut = false;
                _isAppExiting = false;
            }
        }

        private async Task RecordLogoutAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(_companyId) && !string.IsNullOrEmpty(_username))
                {
                    await AuditService.LogLogoutAsync(_username, _companyId, Environment.MachineName);
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
        public static void SetAppExiting(bool exiting)
        {
            _isAppExiting = exiting;
        }

        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            var titleBar = sender as Control;
            titleBar.MouseMove -= TitleBar_MouseMove;
            titleBar.MouseUp -= TitleBar_MouseUp;
        }
    }
}