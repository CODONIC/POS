using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS.Cashier
{
    public static class CashierShortcutHelper
    {
        // Attach keyboard navigation (Up/Down arrows) to controls with custom navigation logic
        public static void AttachCustomKeyNavigation(Control control, Action<object, KeyEventArgs> keyDownHandler)
        {
            control.KeyDown += (s, e) => keyDownHandler(s, e);
        }

        // Attach function shortcuts for Cashier (Enter, Delete, Ctrl+Shift+C, Escape, F1, F2)
        public static void AttachFunctionShortcuts(Form form,
            Action<object, EventArgs> onEnter,
            Action<object, EventArgs> onDelete,
            Action<object, EventArgs> onClearCart,
            Action<object, EventArgs> onEscape,
            Action<object, EventArgs> onPayment,
            Action<object, EventArgs> onToggleCart,
            Action<object, EventArgs> onShowCheatSheet)
        {
            form.KeyPreview = true;
            form.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    onEnter?.Invoke(s, e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    onDelete?.Invoke(s, e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.C && e.Control && e.Shift)
                {
                    onClearCart?.Invoke(s, e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    onEscape?.Invoke(s, e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.F2)
                {
                    onPayment?.Invoke(s, e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.F1)
                {
                    onToggleCart?.Invoke(s, e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.H)
                {
                    onShowCheatSheet?.Invoke(s, e);
                    e.Handled = true;
                }
            };
        }

        // Setup tooltips for buttons
        public static void SetupTooltips(Control parent, params (Button button, string shortcut)[] buttonShortcuts)
        {
            var toolTip = new ToolTip { InitialDelay = 200, ShowAlways = true };
            foreach (var (button, shortcut) in buttonShortcuts)
            {
                toolTip.SetToolTip(button, shortcut);
            }
        }

        // Attach hover effect to button (with text and shortcut)
        public static void AttachHoverEffect(Button btn, string defaultText, string shortcut)
        {
            Point originalLocation = btn.Location;
            btn.MouseEnter += (s, e) =>
            {
                btn.Text = $"{defaultText}\n({shortcut})";
                btn.Location = new Point(originalLocation.X, originalLocation.Y - 3);
                btn.Padding = new Padding(0, 0, 0, 6);
            };
            btn.MouseLeave += (s, e) =>
            {
                btn.Text = defaultText;
                btn.Location = originalLocation;
                btn.Padding = new Padding(0);
            };
        }
    }
}