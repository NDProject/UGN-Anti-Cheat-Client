#Region " Imports "

Imports System.Drawing.Drawing2D
Imports System.ComponentModel

#End Region

#Region " Button "

Class UGN_Button
    Inherits Control

#Region " Variables "

    Private MouseState As Integer
    Private Shape As GraphicsPath
    Private InactiveGB, PressedGB As LinearGradientBrush
    Private R1 As Rectangle
    Private P1, P3 As Pen
    Private _Image As Image
    Private _ImageSize As Size
    Private _TextAlignment As StringAlignment = StringAlignment.Center
    Private _TextColor As Color = Color.FromArgb(150, 150, 150)
    Private _ImageAlign As ContentAlignment = ContentAlignment.MiddleLeft

#End Region
#Region " Image Designer "

    Private Shared Function ImageLocation(ByVal SF As StringFormat, ByVal Area As SizeF, ByVal ImageArea As SizeF) As PointF
        Dim MyPoint As PointF
        Select Case SF.Alignment
            Case StringAlignment.Center
                MyPoint.X = CSng((Area.Width - ImageArea.Width) / 2)
            Case StringAlignment.Near
                MyPoint.X = 2
            Case StringAlignment.Far
                MyPoint.X = Area.Width - ImageArea.Width - 2

        End Select

        Select Case SF.LineAlignment
            Case StringAlignment.Center
                MyPoint.Y = CSng((Area.Height - ImageArea.Height) / 2)
            Case StringAlignment.Near
                MyPoint.Y = 2
            Case StringAlignment.Far
                MyPoint.Y = Area.Height - ImageArea.Height - 2
        End Select
        Return MyPoint
    End Function

    Private Function GetStringFormat(ByVal _ContentAlignment As ContentAlignment) As StringFormat
        Dim SF As StringFormat = New StringFormat()
        Select Case _ContentAlignment
            Case ContentAlignment.MiddleCenter
                SF.LineAlignment = StringAlignment.Center
                SF.Alignment = StringAlignment.Center
            Case ContentAlignment.MiddleLeft
                SF.LineAlignment = StringAlignment.Center
                SF.Alignment = StringAlignment.Near
            Case ContentAlignment.MiddleRight
                SF.LineAlignment = StringAlignment.Center
                SF.Alignment = StringAlignment.Far
            Case ContentAlignment.TopCenter
                SF.LineAlignment = StringAlignment.Near
                SF.Alignment = StringAlignment.Center
            Case ContentAlignment.TopLeft
                SF.LineAlignment = StringAlignment.Near
                SF.Alignment = StringAlignment.Near
            Case ContentAlignment.TopRight
                SF.LineAlignment = StringAlignment.Near
                SF.Alignment = StringAlignment.Far
            Case ContentAlignment.BottomCenter
                SF.LineAlignment = StringAlignment.Far
                SF.Alignment = StringAlignment.Center
            Case ContentAlignment.BottomLeft
                SF.LineAlignment = StringAlignment.Far
                SF.Alignment = StringAlignment.Near
            Case ContentAlignment.BottomRight
                SF.LineAlignment = StringAlignment.Far
                SF.Alignment = StringAlignment.Far
        End Select
        Return SF
    End Function

#End Region
#Region " Properties "

    Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            If value Is Nothing Then
                _ImageSize = Size.Empty
            Else
                _ImageSize = value.Size
            End If

            _Image = value
            Invalidate()
        End Set
    End Property

    Protected ReadOnly Property ImageSize() As Size
        Get
            Return _ImageSize
        End Get
    End Property

    Public Property ImageAlign() As ContentAlignment
        Get
            Return _ImageAlign
        End Get
        Set(ByVal Value As ContentAlignment)
            _ImageAlign = Value
            Invalidate()
        End Set
    End Property

    Public Property TextAlignment As StringAlignment
        Get
            Return Me._TextAlignment
        End Get
        Set(ByVal value As StringAlignment)
            Me._TextAlignment = value
            Me.Invalidate()
        End Set
    End Property

    Public Overrides Property ForeColor As Color
        Get
            Return Me._TextColor
        End Get
        Set(ByVal value As Color)
            Me._TextColor = value
            Me.Invalidate()
        End Set
    End Property

#End Region
#Region " EventArgs "

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MouseState = 0
        Invalidate()
        MyBase.OnMouseUp(e)
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MouseState = 1
        Focus()
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MouseState = 0
        Invalidate()
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        Invalidate()
        MyBase.OnTextChanged(e)
    End Sub

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                 ControlStyles.OptimizedDoubleBuffer Or _
                 ControlStyles.
                 Redraw Or _
                 ControlStyles.SupportsTransparentBackColor Or _
                 ControlStyles.UserPaint, True)

        BackColor = Color.Transparent
        DoubleBuffered = True
        Font = New Font("Segoe UI", 12)
        ForeColor = Color.FromArgb(255, 255, 255)
        Size = New Size(146, 41)
        _TextAlignment = StringAlignment.Center
        P1 = New Pen(Color.FromArgb(181, 41, 42)) ' Border color
        P3 = New Pen(Color.FromArgb(165, 37, 37))  ' Border color when pressed
    End Sub

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        If Width > 0 AndAlso Height > 0 Then

            Shape = New GraphicsPath
            R1 = New Rectangle(0, 0, Width, Height)

            InactiveGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(181, 41, 42), Color.FromArgb(181, 41, 42), 90.0F)
            PressedGB = New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(165, 37, 37), Color.FromArgb(165, 37, 37), 90.0F)
        End If

        With Shape
            .AddArc(0, 0, 10, 10, 180, 90)
            .AddArc(Width - 11, 0, 10, 10, -90, 90)
            .AddArc(Width - 11, Height - 11, 10, 10, 0, 90)
            .AddArc(0, Height - 11, 10, 10, 90, 90)
            .CloseAllFigures()
        End With
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        With e.Graphics
            .SmoothingMode = SmoothingMode.HighQuality
            Dim ipt As PointF = ImageLocation(GetStringFormat(ImageAlign), Size, ImageSize)

            Select Case MouseState
                Case 0 'Inactive
                    .FillPath(InactiveGB, Shape) ' Fills button body with -when inactive- color gradient
                    .DrawPath(P1, Shape) ' Draws button border -When inactive-
                    If IsNothing(Image) Then
                        .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                    Else
                        .DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height)
                        .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                    End If
                Case 1 'Pressed
                    .FillPath(PressedGB, Shape) ' Fill button body with -When pressed- color gradient
                    .DrawPath(P3, Shape) ' Draw button border -When pressed-

                    If IsNothing(Image) Then
                        .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                    Else
                        .DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height)
                        .DrawString(Text, Font, New SolidBrush(ForeColor), R1, New StringFormat() With {.Alignment = _TextAlignment, .LineAlignment = StringAlignment.Center})
                    End If
            End Select
        End With
        MyBase.OnPaint(e)
    End Sub
End Class

#End Region