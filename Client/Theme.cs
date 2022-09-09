#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;

#endregion


#region  Button


public class UGN_Button : Control
{

    #region  Variables

    private int MouseState;
    private GraphicsPath Shape;
    private LinearGradientBrush InactiveGB;
    private LinearGradientBrush PressedGB;
    private Rectangle R1;
    private Pen P1;
    private Pen P3;
    private Image _Image;
    private Size _ImageSize;
    private StringAlignment _TextAlignment = StringAlignment.Center;
    private Color _TextColor;  
    private ContentAlignment _ImageAlign = ContentAlignment.MiddleLeft;

    #endregion
    #region  Image Designer

    private static PointF ImageLocation(StringFormat SF, SizeF Area, SizeF ImageArea)
    {
        PointF MyPoint = new PointF();
        switch (SF.Alignment)
        {
            case StringAlignment.Center:
                MyPoint.X = (float)((Area.Width - ImageArea.Width) / 2);
                break;
            case StringAlignment.Near:
                MyPoint.X = 2;
                break;
            case StringAlignment.Far:
                MyPoint.X = Area.Width - ImageArea.Width - 2;
                break;

        }

        switch (SF.LineAlignment)
        {
            case StringAlignment.Center:
                MyPoint.Y = (float)((Area.Height - ImageArea.Height) / 2);
                break;
            case StringAlignment.Near:
                MyPoint.Y = 2;
                break;
            case StringAlignment.Far:
                MyPoint.Y = Area.Height - ImageArea.Height - 2;
                break;
        }
        return MyPoint;
    }

    private StringFormat GetStringFormat(ContentAlignment _ContentAlignment)
    {
        StringFormat SF = new StringFormat();
        switch (_ContentAlignment)
        {
            case ContentAlignment.MiddleCenter:
                SF.LineAlignment = StringAlignment.Center;
                SF.Alignment = StringAlignment.Center;
                break;
            case ContentAlignment.MiddleLeft:
                SF.LineAlignment = StringAlignment.Center;
                SF.Alignment = StringAlignment.Near;
                break;
            case ContentAlignment.MiddleRight:
                SF.LineAlignment = StringAlignment.Center;
                SF.Alignment = StringAlignment.Far;
                break;
            case ContentAlignment.TopCenter:
                SF.LineAlignment = StringAlignment.Near;
                SF.Alignment = StringAlignment.Center;
                break;
            case ContentAlignment.TopLeft:
                SF.LineAlignment = StringAlignment.Near;
                SF.Alignment = StringAlignment.Near;
                break;
            case ContentAlignment.TopRight:
                SF.LineAlignment = StringAlignment.Near;
                SF.Alignment = StringAlignment.Far;
                break;
            case ContentAlignment.BottomCenter:
                SF.LineAlignment = StringAlignment.Far;
                SF.Alignment = StringAlignment.Center;
                break;
            case ContentAlignment.BottomLeft:
                SF.LineAlignment = StringAlignment.Far;
                SF.Alignment = StringAlignment.Near;
                break;
            case ContentAlignment.BottomRight:
                SF.LineAlignment = StringAlignment.Far;
                SF.Alignment = StringAlignment.Far;
                break;
        }
        return SF;
    }

    #endregion
    #region  Properties

    public Image Image
    {
        get
        {
            return _Image;
        }
        set
        {
            if (value == null)
            {
                _ImageSize = Size.Empty;
            }
            else
            {
                _ImageSize = value.Size;
            }

            _Image = value;
            Invalidate();
        }
    }

    protected Size ImageSize
    {
        get
        {
            return _ImageSize;
        }
    }

    public ContentAlignment ImageAlign
    {
        get
        {
            return _ImageAlign;
        }
        set
        {
            _ImageAlign = value;
            Invalidate();
        }
    }

    public StringAlignment TextAlignment
    {
        get
        {
            return this._TextAlignment;
        }
        set
        {
            this._TextAlignment = value;
            this.Invalidate();
        }
    }

    public override Color ForeColor
    {
        get
        {
            return this._TextColor;
        }
        set
        {
            this._TextColor = value;
            this.Invalidate();
        }
    }

    #endregion
    #region  EventArgs

    protected override void OnMouseUp(MouseEventArgs e)
    {
        MouseState = 0;
        Invalidate();
        base.OnMouseUp(e);
    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        MouseState = 1;
        Focus();
        Invalidate();
        base.OnMouseDown(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        MouseState = 0;
        Invalidate();
        base.OnMouseLeave(e);
    }

    protected override void OnTextChanged(System.EventArgs e)
    {
        Invalidate();
        base.OnTextChanged(e);
    }

    #endregion

    public UGN_Button()
    {
        SetStyle((System.Windows.Forms.ControlStyles)(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint), true);

        BackColor = Color.Transparent;
        DoubleBuffered = true;
        Font = new Font("Segoe UI", 12);
        ForeColor = Color.FromArgb(255, 255, 255);
        Size = new Size(146, 41);
        _TextAlignment = StringAlignment.Center;
        P1 = new Pen(Color.FromArgb(58, 64, 76)); // = Border color
        P3 = new Pen(Color.FromArgb(58, 64, 70)); // = Border color when pressed
    }

    protected override void OnResize(System.EventArgs e)
    {
        base.OnResize(e);
        if (Width > 0 && Height > 0)
        {

            Shape = new GraphicsPath();
            R1 = new Rectangle(0, 0, Width, Height);

            InactiveGB = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(86, 103, 133), Color.FromArgb(64, 77, 100), 90.0F);
            PressedGB = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(115, 138, 179), Color.FromArgb(86, 103, 133), 90.0F);
        }

        Shape.AddArc(0, 0, 10, 10, 180, 90);
        Shape.AddArc(Width - 11, 0, 10, 10, -90, 90);
        Shape.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
        Shape.AddArc(0, Height - 11, 10, 10, 90, 90);
        Shape.CloseAllFigures();
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var G = e.Graphics;
        G.SmoothingMode = SmoothingMode.HighQuality;
        PointF ipt = ImageLocation(GetStringFormat(ImageAlign), Size, ImageSize);

        switch (MouseState)
        {
            case 0:
                //When inactive
                G.FillPath(InactiveGB, Shape);
                // Fills button body when inactive with color gradient
                G.DrawPath(P1, Shape);
                // Draws button border when inactive
                if ((Image == null))
                {
                    G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat
                    {
                        Alignment = _TextAlignment,
                        LineAlignment = StringAlignment.Center
                    });
                }
                else
                {
                    G.DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height);
                    G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat
                    {
                        Alignment = _TextAlignment,
                        LineAlignment = StringAlignment.Center
                    });
                }
                break;
            case 1:
                //When pressed
                G.FillPath(PressedGB, Shape);  // Fills button body with when pressed color gradient

                G.DrawPath(P3, Shape);  // Draws button border when pressed

                if ((Image == null))
                {
                    G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat
                    {
                        Alignment = _TextAlignment,
                        LineAlignment = StringAlignment.Center
                    });
                }
                else
                {
                    G.DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height);
                    G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat
                    {
                        Alignment = _TextAlignment,
                        LineAlignment = StringAlignment.Center
                    });
                }
                break;
        }
        base.OnPaint(e);
    }
}

#endregion

[DefaultEvent("CheckedChanged")]
    public class UGNCheckBox : Control
    {

        #region " Control Help - MouseState & Flicker Control"
        private Point mouse = new Point(0, 0);

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mouse = e.Location;
        }
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        private bool _Checked;
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                Invalidate();
            }
        }
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Height = 14;
        }
        protected override void OnClick(System.EventArgs e)
        {
            if (mouse.X <= Height - 1)
            {
                _Checked = !_Checked;
                Invalidate();
                if (CheckedChanged != null)
                {
                    CheckedChanged(this);
                }
            }

            base.OnClick(e);
        }
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);
        #endregion

        public UGNCheckBox() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            Size = new Size(145, 16);
            DoubleBuffered = true;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);

            Rectangle checkBoxRectangle = new Rectangle(0, 0, Height - 1, Height - 1);
            G.SmoothingMode = SmoothingMode.HighQuality;

            G.Clear(Parent.FindForm().BackColor);

            LinearGradientBrush gradientBody = new LinearGradientBrush(checkBoxRectangle, Color.FromArgb(245, 245, 245), Color.FromArgb(231, 231, 231), 90);
            G.FillRectangle(gradientBody, gradientBody.Rectangle);
            G.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189)), new Rectangle(0, 0, Height - 1, Height - 2));
            G.DrawRectangle(new Pen(Color.FromArgb(252, 252, 252)), new Rectangle(1, 1, Height - 3, Height - 4));
            G.DrawLine(new Pen(Color.FromArgb(168, 168, 168)), new Point(1, Height - 1), new Point(Height - 2, Height - 1));

            if (Checked)
            {
                Rectangle CheckPoly = new Rectangle(checkBoxRectangle.X + checkBoxRectangle.Width / 4, checkBoxRectangle.Y + checkBoxRectangle.Height / 4, checkBoxRectangle.Width / 2, checkBoxRectangle.Height / 2);
                Point[] Poly = {
				new Point(CheckPoly.X, CheckPoly.Y + CheckPoly.Height / 2),
				new Point(CheckPoly.X + CheckPoly.Width / 2, CheckPoly.Y + CheckPoly.Height),
				new Point(CheckPoly.X + CheckPoly.Width, CheckPoly.Y)
			};
                G.SmoothingMode = SmoothingMode.HighQuality;
                Pen P1 = new Pen(Color.FromArgb(27, 94, 137), 2);
                LinearGradientBrush chkGrad = new LinearGradientBrush(CheckPoly, Color.FromArgb(200, 200, 200), Color.FromArgb(255, 255, 255), 0f);

                G.DrawString("a", new Font("Marlett", 10.75f), new SolidBrush(Color.FromArgb(220, ForeColor)), new Rectangle(-2, -1, Width - 1, Height - 1), new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                });
            }

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(16, 0), new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            });


            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();

        }

    }

public class ugnStatusBar : Control
{

    protected override void CreateHandle()
    {
        base.CreateHandle();
        Dock = DockStyle.None;
    }
    protected override void OnResize(System.EventArgs e)
    {
        base.OnResize(e);
        Invalidate();
    }
    protected override void OnTextChanged(System.EventArgs e)
    {
        base.OnTextChanged(e);
        Invalidate();
    }

    public ugnStatusBar()
        : base()
    {
        Font = new Font("Verdana", 6.75f, FontStyle.Bold);
        ForeColor = Color.FromArgb(119, 119, 119);
        Size = new Size(Width, 20);
        DoubleBuffered = true;
    }

    protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
    {
        Bitmap B = new Bitmap(Width, Height);
        Graphics G = Graphics.FromImage(B);
        base.OnPaint(e);

        LinearGradientBrush gradientBodyNone = new LinearGradientBrush(new Rectangle(0, 1, Width, Height - 1), Color.FromArgb(245, 245, 245), Color.FromArgb(230, 230, 230), 90);
        G.FillRectangle(gradientBodyNone, gradientBodyNone.Rectangle);
        LinearGradientBrush bodyInBorderNone = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 3), Color.FromArgb(58, 64, 76), Color.FromArgb(58, 64, 76), 90);
        G.DrawRectangle(new Pen(bodyInBorderNone), new Rectangle(1, 1, Width - 3, Height - 3));
        G.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189)), new Rectangle(0, 0, Width - 1, Height - 1));

        G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(4, 4), StringFormat.GenericDefault);

        e.Graphics.DrawImage(B, 0, 0);
        G.Dispose();
        B.Dispose();
    }
}



  