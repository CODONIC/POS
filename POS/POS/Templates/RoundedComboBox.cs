using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    [ToolboxItem(true)]
    [DefaultEvent("SelectedIndexChanged")]
    [Description("Styled ComboBox with rounded corners and a glowing border.")]
    public class CustomComboBox : Control
    {
        private ComboBox _innerComboBox;
        private bool _isFocused = false;

        // ── Backing fields ───────────────────────────────────────────────────────
        private Color _borderColor = Color.FromArgb(100, 180, 255);
        private Color _borderFocusColor = Color.FromArgb(60, 140, 255);
        private Color _innerBackColor = Color.FromArgb(18, 22, 48);
        private Color _innerForeColor = Color.WhiteSmoke;
        private Color _arrowColor = Color.FromArgb(100, 180, 255);
        private int _borderRadius = 8;
        private int _borderThickness = 2;
        private string _placeholderText = string.Empty;
        private Color _placeholderColor = Color.FromArgb(120, 150, 200);

        // ────────────────────────────────────────────────────────────────────────
        //  Constructor
        // ────────────────────────────────────────────────────────────────────────
        public CustomComboBox()
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

            InitInnerComboBox();
        }

        // ────────────────────────────────────────────────────────────────────────
        //  Inner ComboBox
        // ────────────────────────────────────────────────────────────────────────
        private void InitInnerComboBox()
        {
            _innerComboBox = new ComboBox
            {
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = _innerBackColor,
                ForeColor = _innerForeColor,
                Font = Font,
                TabStop = false,
                DrawMode = DrawMode.OwnerDrawFixed,
            };

            // Hide the native border by making it flush with the wrapper
            _innerComboBox.GotFocus += (s, e) => { _isFocused = true; Invalidate(); };
            _innerComboBox.LostFocus += (s, e) => { _isFocused = false; Invalidate(); };
            _innerComboBox.SelectedIndexChanged += (s, e) => { OnSelectedIndexChanged(e); Invalidate(); };
            _innerComboBox.DrawItem += InnerComboBox_DrawItem;

            Controls.Add(_innerComboBox);
            PositionInnerComboBox();
        }

        private void PositionInnerComboBox()
        {
            if (_innerComboBox == null) return;

            int hPad = _borderThickness + _borderRadius / 2 + 4;
            int cHeight = _innerComboBox.PreferredHeight;

            // Push the native control slightly left so our custom arrow is visible
            _innerComboBox.SetBounds(
                hPad,
                (Height - cHeight) / 2,
                Math.Max(1, Width - hPad * 2),
                cHeight);

            // ItemHeight drives row height in OwnerDraw
            _innerComboBox.ItemHeight = Math.Max(1, cHeight - 2);
        }

        // ── Draw each dropdown item ──────────────────────────────────────────────
        private void InnerComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            bool selected = (e.State & DrawItemState.Selected) != 0;

            // Row background
            Color rowBg = selected
                ? Color.FromArgb(40, 60, 100)   // highlight — same blue family
                : _innerBackColor;

            using (SolidBrush bg = new SolidBrush(rowBg))
                g.FillRectangle(bg, e.Bounds);

            // Row text
            string itemText = _innerComboBox.Items[e.Index]?.ToString() ?? string.Empty;
            Color rowFg = selected ? Color.FromArgb(160, 210, 255) : _innerForeColor;

            TextRenderer.DrawText(
                g, itemText, Font,
                new Rectangle(e.Bounds.X + 8, e.Bounds.Y, e.Bounds.Width - 8, e.Bounds.Height),
                rowFg,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }

        // ────────────────────────────────────────────────────────────────────────
        //  Painting  (mirrors CustomTextBox exactly)
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

            // 4. Placeholder (shown when nothing is selected)
            bool nothingSelected = _innerComboBox?.SelectedIndex < 0;
            if (nothingSelected && !string.IsNullOrEmpty(_placeholderText))
            {
                int px = t + _borderRadius / 2 + 4;
                int py = (Height - Font.Height) / 2;
                using (SolidBrush ph = new SolidBrush(_placeholderColor))
                    g.DrawString(_placeholderText, Font, ph, new PointF(px, py));
            }

            // 5. Custom dropdown arrow (drawn over the native one)
            DrawArrow(g, r);
        }

        private void DrawArrow(Graphics g, int radius)
        {
            // Vertical centre of the control
            int cy = Height / 2;
            // Right-aligned, inside the border + padding
            int ax = Width - _borderThickness - radius / 2 - 14;
            int size = 5;   // half-width of the triangle

            Point[] arrow =
            {
                new Point(ax - size, cy - 2),
                new Point(ax + size, cy - 2),
                new Point(ax,        cy + size - 1)
            };

            using (SolidBrush brush = new SolidBrush(_isFocused ? _borderFocusColor : _arrowColor))
                g.FillPolygon(brush, arrow);
        }

        // ────────────────────────────────────────────────────────────────────────
        //  Designer Properties  (mirrors CustomTextBox naming/categories)
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
                if (_innerComboBox != null) _innerComboBox.BackColor = value;
                Invalidate();
            }
        }

        [Category("Custom Appearance"), Description("Text / item color.")]
        public Color InnerForeColor
        {
            get => _innerForeColor;
            set
            {
                _innerForeColor = value;
                if (_innerComboBox != null) _innerComboBox.ForeColor = value;
                Invalidate();
            }
        }

        [Category("Custom Appearance"), Description("Color of the dropdown arrow icon.")]
        public Color ArrowColor
        {
            get => _arrowColor;
            set { _arrowColor = value; Invalidate(); }
        }

        [Category("Custom Appearance"), Description("Corner radius in pixels (0 = square, 8 = default).")]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = Math.Max(0, value); PositionInnerComboBox(); Invalidate(); }
        }

        [Category("Custom Appearance"), Description("Border line thickness in pixels.")]
        public int BorderThickness
        {
            get => _borderThickness;
            set { _borderThickness = Math.Max(1, value); PositionInnerComboBox(); Invalidate(); }
        }

        [Category("Custom Appearance"), Description("Hint text shown when nothing is selected.")]
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

        // ── Delegate key ComboBox members ────────────────────────────────────────
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Data"), Description("The items in the combo box.")]
        public ComboBox.ObjectCollection Items => _innerComboBox?.Items;

        [Browsable(false)]
        public int SelectedIndex
        {
            get => _innerComboBox?.SelectedIndex ?? -1;
            set { if (_innerComboBox != null) _innerComboBox.SelectedIndex = value; }
        }

        [Browsable(false)]
        public object SelectedItem
        {
            get => _innerComboBox?.SelectedItem;
            set { if (_innerComboBox != null) _innerComboBox.SelectedItem = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get => _innerComboBox?.Text ?? string.Empty;
            set { if (_innerComboBox != null) _innerComboBox.Text = value; }
        }

        // Hide BackColor — transparency managed internally
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get => Color.Transparent;
            set { /* always transparent outside the rounded rect */ }
        }

        // ── Events ───────────────────────────────────────────────────────────────
        [Category("Behavior"), Description("Fires when the selected item changes.")]
        public event EventHandler SelectedIndexChanged;

        protected virtual void OnSelectedIndexChanged(EventArgs e)
            => SelectedIndexChanged?.Invoke(this, e);

        // ────────────────────────────────────────────────────────────────────────
        //  Layout / lifecycle  (mirrors CustomTextBox)
        // ────────────────────────────────────────────────────────────────────────
        protected override void OnResize(EventArgs e) { base.OnResize(e); PositionInnerComboBox(); }
        protected override void OnClick(EventArgs e) { base.OnClick(e); _innerComboBox?.Focus(); }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (_innerComboBox != null) { _innerComboBox.Font = Font; PositionInnerComboBox(); }
        }

        // ── Helper ───────────────────────────────────────────────────────────────
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
            if (disposing) _innerComboBox?.Dispose();
            base.Dispose(disposing);
        }

        public void FocusInner() => _innerComboBox?.Focus();
    }
}