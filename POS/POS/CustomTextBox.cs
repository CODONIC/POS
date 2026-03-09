using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    [ToolboxItem(true)]
    [DefaultEvent("TextChanged")]
    [Description("Styled TextBox with rounded corners and a glowing border.")]
    public class CustomTextBox : Control
    {
        private TextBox _innerTextBox;
        private bool _isFocused = false;

        // ── Backing fields ───────────────────────────────────────────────────────
        private Color _borderColor = Color.FromArgb(100, 180, 255);
        private Color _borderFocusColor = Color.FromArgb(60, 140, 255);
        private Color _innerBackColor = Color.FromArgb(18, 22, 48);
        private Color _innerForeColor = Color.WhiteSmoke;
        private int _borderRadius = 8;
        private int _borderThickness = 2;
        private string _placeholderText = string.Empty;
        private Color _placeholderColor = Color.FromArgb(120, 150, 200);
        private bool _isPasswordField = false;
        private char _passwordChar = '●';   // ← default mask character

        // ────────────────────────────────────────────────────────────────────────
        //  Constructor
        // ────────────────────────────────────────────────────────────────────────
        public CustomTextBox()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(220, 36);
            base.BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 10f, FontStyle.Regular, GraphicsUnit.Point);

            InitInnerTextBox();
        }

        // ────────────────────────────────────────────────────────────────────────
        //  Inner TextBox
        // ────────────────────────────────────────────────────────────────────────
        private void InitInnerTextBox()
        {
            _innerTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = _innerBackColor,
                ForeColor = _innerForeColor,
                Font = Font,
                TabStop = false,
            };

            // Apply password masking based on current settings
            ApplyPasswordMode();

            _innerTextBox.GotFocus += (s, e) => { _isFocused = true; Invalidate(); };
            _innerTextBox.LostFocus += (s, e) => { _isFocused = false; Invalidate(); };
            _innerTextBox.TextChanged += (s, e) => { OnTextChanged(e); Invalidate(); };
            _innerTextBox.KeyDown += (s, e) => OnKeyDown(e);
            _innerTextBox.KeyPress += (s, e) => OnKeyPress(e);

            Controls.Add(_innerTextBox);
            PositionInnerTextBox();
        }

        /// <summary>
        /// Applies the current password mode to the inner TextBox.
        /// Prefers an explicit PasswordChar when set; falls back to
        /// UseSystemPasswordChar only when PasswordChar is '\0'.
        /// </summary>
        private void ApplyPasswordMode()
        {
            if (_innerTextBox == null) return;

            if (!_isPasswordField)
            {
                // Clear all masking
                _innerTextBox.UseSystemPasswordChar = false;
                _innerTextBox.PasswordChar = '\0';
            }
            else if (_passwordChar != '\0')
            {
                // Custom character takes priority — UseSystemPasswordChar must be
                // false, otherwise Windows ignores PasswordChar entirely.
                _innerTextBox.UseSystemPasswordChar = false;
                _innerTextBox.PasswordChar = _passwordChar;
            }
            else
            {
                // Caller explicitly set PasswordChar to '\0': use the OS default
                _innerTextBox.PasswordChar = '\0';
                _innerTextBox.UseSystemPasswordChar = true;
            }
        }

        private void PositionInnerTextBox()
        {
            if (_innerTextBox == null) return;
            int hPad = _borderThickness + _borderRadius / 2 + 4;
            int tHeight = _innerTextBox.PreferredHeight;
            _innerTextBox.SetBounds(
                hPad,
                (Height - tHeight) / 2,
                Math.Max(1, Width - hPad * 2),
                tHeight);
        }

        // ────────────────────────────────────────────────────────────────────────
        //  Designer Properties
        // ────────────────────────────────────────────────────────────────────────
        [Category("Custom Appearance"), Description("Border color when idle.")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Custom Appearance"), Description("Border / glow color when focused.")]
        public Color BorderFocusColor
        {
            get => _borderFocusColor;
            set { _borderFocusColor = value; Invalidate(); }
        }

        [Category("Custom Appearance"), Description("Background fill color inside the box.")]
        public Color InnerBackColor
        {
            get => _innerBackColor;
            set
            {
                _innerBackColor = value;
                if (_innerTextBox != null) _innerTextBox.BackColor = value;
                Invalidate();
            }
        }

        [Category("Custom Appearance"), Description("Text color.")]
        public Color InnerForeColor
        {
            get => _innerForeColor;
            set
            {
                _innerForeColor = value;
                if (_innerTextBox != null) _innerTextBox.ForeColor = value;
                Invalidate();
            }
        }

        [Category("Custom Appearance"), Description("Corner radius in pixels (0 = fully square, 8 = default subtle rounding).")]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = Math.Max(0, value); PositionInnerTextBox(); Invalidate(); }
        }

        [Category("Custom Appearance"), Description("Border line thickness in pixels.")]
        public int BorderThickness
        {
            get => _borderThickness;
            set { _borderThickness = Math.Max(1, value); PositionInnerTextBox(); Invalidate(); }
        }

        [Category("Custom Appearance"), Description("Hint text shown when the field is empty.")]
        public string PlaceholderText
        {
            get => _placeholderText;
            set { _placeholderText = value; Invalidate(); }
        }

        [Category("Custom Appearance"), Description("Color of the placeholder / hint text.")]
        public Color PlaceholderColor
        {
            get => _placeholderColor;
            set { _placeholderColor = value; Invalidate(); }
        }

        [Category("Behavior")]
        [Description("Mask input as a password field. Uses PasswordChar (default ●) to hide typed characters.")]
        public bool IsPasswordField
        {
            get => _isPasswordField;
            set
            {
                _isPasswordField = value;
                ApplyPasswordMode();
            }
        }

        [Category("Behavior")]
        [Description(
            "Character used to mask input when IsPasswordField is true. " +
            "Default is ● (U+25CF). Set to '\\0' to fall back to the OS system password character.")]
        [DefaultValue('●')]
        public char PasswordChar
        {
            get => _passwordChar;
            set
            {
                _passwordChar = value;
                ApplyPasswordMode();   // re-apply so the change takes effect immediately
            }
        }

        // Hide BackColor from designer — transparency is managed internally.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get => Color.Transparent;
            set { /* always transparent outside the rounded rect */ }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => _innerTextBox?.Text ?? string.Empty;
            set { if (_innerTextBox != null) _innerTextBox.Text = value; }
        }

        // ────────────────────────────────────────────────────────────────────────
        //  Painting
        // ────────────────────────────────────────────────────────────────────────
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent != null)
            {
                var clip = e.ClipRectangle;
                var pt = PointToScreen(clip.Location);
                pt = Parent.PointToClient(pt);

                e.Graphics.TranslateTransform(clip.X, clip.Y);
                using (var pea = new PaintEventArgs(e.Graphics,
                    new Rectangle(pt, clip.Size)))
                {
                    pea.Graphics.TranslateTransform(-pt.X, -pt.Y);
                    InvokePaintBackground(Parent, pea);
                    InvokePaint(Parent, pea);
                }
                e.Graphics.ResetTransform();
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int t = _borderThickness;
            var rect = new Rectangle(t, t, Width - t * 2 - 1, Height - t * 2 - 1);
            int r = Math.Min(_borderRadius, Math.Min(rect.Width, rect.Height) / 2);

            // 1. Fill rounded background
            using (GraphicsPath clip = RoundedRect(rect, r))
            {
                g.SetClip(clip);
                using (SolidBrush fill = new SolidBrush(_innerBackColor))
                    g.FillPath(fill, clip);
                g.ResetClip();
            }

            // 2. Focus glow
            if (_isFocused)
            {
                var gRect = new Rectangle(t + 1, t + 1, Width - t * 2 - 3, Height - t * 2 - 3);
                using (GraphicsPath gp = RoundedRect(gRect, Math.Max(0, r - 1)))
                using (Pen glow = new Pen(Color.FromArgb(60, _borderFocusColor), 4))
                    g.DrawPath(glow, gp);
            }

            // 3. Border
            Color active = _isFocused ? _borderFocusColor : _borderColor;
            using (GraphicsPath bp = RoundedRect(rect, r))
            using (Pen pen = new Pen(active, t))
                g.DrawPath(pen, bp);

            // 4. Placeholder
            if (!_isFocused && string.IsNullOrEmpty(_innerTextBox?.Text) && !string.IsNullOrEmpty(_placeholderText))
            {
                int px = t + _borderRadius / 2 + 4;
                int py = (Height - Font.Height) / 2;
                using (SolidBrush ph = new SolidBrush(_placeholderColor))
                    g.DrawString(_placeholderText, Font, ph, new PointF(px, py));
            }
        }

        // ────────────────────────────────────────────────────────────────────────
        //  Public Methods
        // ────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Clears all text from the textbox, even while the user is actively typing.
        /// </summary>
        public void Clear()
        {
            if (_innerTextBox != null)
            {
                _innerTextBox.Text = string.Empty;   // forcefully wipe the buffer
                _innerTextBox.SelectionStart = 0;    // reset caret to beginning
                _innerTextBox.SelectionLength = 0;
            }

            Invalidate(); // repaint so placeholder reappears immediately
        }

        // ────────────────────────────────────────────────────────────────────────
        //  Events / Layout
        // ────────────────────────────────────────────────────────────────────────
        protected override void OnResize(EventArgs e) { base.OnResize(e); PositionInnerTextBox(); }
        protected override void OnClick(EventArgs e) { base.OnClick(e); _innerTextBox?.Focus(); }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (_innerTextBox != null) { _innerTextBox.Font = Font; PositionInnerTextBox(); }
        }

        // ────────────────────────────────────────────────────────────────────────
        //  Helper
        // ────────────────────────────────────────────────────────────────────────
        private static GraphicsPath RoundedRect(Rectangle b, int r)
        {
            var p = new GraphicsPath();
            if (r <= 0) { p.AddRectangle(b); return p; }
            int d = r * 2;
            p.AddArc(b.X, b.Y, d, d, 180, 90);
            p.AddArc(b.Right - d, b.Y, d, d, 270, 90);
            p.AddArc(b.Right - d, b.Bottom - d, d, d, 0, 90);
            p.AddArc(b.X, b.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _innerTextBox?.Dispose();
            base.Dispose(disposing);
        }
    }
}