Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils

Namespace MyXtraGrid
	Partial Public Class Form1
		Inherits Form

		Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("ID", GetType(Integer))
			tbl.Columns.Add("Number", GetType(Integer))
			tbl.Columns.Add("Date", GetType(DateTime))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i Mod 3), i, 3 - i, DateTime.Now.AddDays(i) })
			Next i
			Return tbl
		End Function


		Public Sub New()
			InitializeComponent()
			myGridControl1.DataSource = CreateTable(20)
		End Sub

		Private Sub myGridView1_CustomDrawFooterCell(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs) Handles myGridView1.CustomDrawFooterCell
			Dim isPrinting As Boolean = e.Handled
			e.Handled = True
			e.Appearance.BackColor = Color.Yellow
			e.Appearance.FillRectangle(e.Cache, e.Bounds)
			Dim rect As Rectangle = e.Bounds
			rect.Width = rect.Height
			e.Appearance.FillRectangle(e.Cache, rect)
			e.Graphics.DrawLine(New Pen(Brushes.Red, 3), New Point(rect.X, rect.Y), New Point(rect.Right, rect.Bottom))
			e.Graphics.DrawEllipse(New Pen(Brushes.Red, 3), rect)
			e.Graphics.DrawString("Custom Draw", AppearanceObject.DefaultFont, Brushes.Black, e.Bounds)
		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			myGridControl1.ShowPrintPreview()
		End Sub

	End Class
End Namespace