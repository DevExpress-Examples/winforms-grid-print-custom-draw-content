Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraGrid.Views.Printing
Imports DevExpress.XtraPrinting
Imports System.Drawing
Imports DevExpress.Data
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Views.Grid

Namespace MyXtraGrid
	Public Class MyGridViewPrintInfo
		Inherits GridViewPrintInfo

		Public ReadOnly Property FooterPanelHeight() As Integer
			Get
				Return CalcStyleHeight(AppearancePrint.FooterPanel) + 4
			End Get
		End Property
		Public Sub New(ByVal args As DevExpress.XtraGrid.Views.Printing.PrintInfoArgs)
			MyBase.New(args)
		End Sub

		Public Overrides Sub PrintFooterPanel(ByVal graph As IBrickGraphics)
			MyBase.PrintFooterPanel(graph)
			CustomDrawFooterCells(graph)
		End Sub

		Private Sub CustomDrawFooterCells(ByVal graph As IBrickGraphics)
			If (Not View.OptionsPrint.PrintFooter) Then
				Return
			End If
			For Each colInfo As PrintColumnInfo In Columns
				If colInfo.Column.SummaryItem.SummaryType = SummaryItemType.None Then
					Continue For
				End If
				Dim r As Rectangle = Rectangle.Empty
				r.X = colInfo.Bounds.X + Indent
				r.Y = colInfo.RowIndex * FooterPanelHeight + 2 + Y
				r.Width = colInfo.Bounds.Width
				r.Height = FooterPanelHeight * colInfo.RowCount
				r.X -= Indent
				r.Y -= r.Height
				Dim text As String = String.Empty
				Dim ib As ImageBrick = GetImageBrick(colInfo, r, text)
				If ib IsNot Nothing Then
					graph.DrawBrick(ib, ib.Rect)
				End If
			Next colInfo
		End Sub

		Private Function GetImageBrick(ByVal colInfo As PrintColumnInfo, ByVal rect As Rectangle, <System.Runtime.InteropServices.Out()> ByRef displayText As String) As ImageBrick
			Dim bmp As New Bitmap(rect.Width, rect.Height)
			Dim cache As New GraphicsCache(Graphics.FromImage(bmp))
			Dim args As FooterCellCustomDrawEventArgs = (TryCast(View, MyGridView)).GetCustomDrawCellArgs(cache, rect, colInfo.Column)
			displayText = args.Info.DisplayText
			If (Not args.Handled) Then
				Return Nothing
			End If
			Dim border As BorderSide
			If args.Appearance.Options.UseBorderColor Then
				border = BorderSide.All
			Else
				border = BorderSide.None
			End If
			Dim ib As New ImageBrick(border, 1, args.Appearance.BorderColor, args.Appearance.BackColor)
			ib.Rect = rect
			ib.Image = bmp
			Return ib
		End Function
	End Class
End Namespace
