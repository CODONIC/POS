using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public class BaseForm : Form
    {
        private Point mouseOffset;

        // Call this in any child form's constructor to wire up a title bar
        protected void InitializeTitleBar(Button closeButton, params Control[] draggableControls)
        {
            foreach (var control in draggableControls)
                control.MouseDown += TitleBar_MouseDown;

            if (closeButton != null)
                closeButton.Click += CloseButton_Click;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
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