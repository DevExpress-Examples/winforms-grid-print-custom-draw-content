using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;

namespace MyXtraGrid {
    public partial class Form1 : Form {

        private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Number", typeof(int));
            tbl.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i % 3), i, 3 - i, DateTime.Now.AddDays(i) });
            return tbl;
        }
        

        public Form1() {
            InitializeComponent();
            myGridControl1.DataSource = CreateTable(20);
        }

        private void myGridView1_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            bool isPrinting = e.Handled;
            e.Handled = true;
            e.Appearance.BackColor = Color.Yellow;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            Rectangle rect = e.Bounds;
            rect.Width = rect.Height;
            e.Appearance.FillRectangle(e.Cache, rect);
            e.Graphics.DrawLine(new Pen(Brushes.Red, 3), new Point(rect.X, rect.Y), new Point(rect.Right, rect.Bottom));
            e.Graphics.DrawEllipse(new Pen(Brushes.Red, 3), rect);
            e.Graphics.DrawString("Custom Draw", AppearanceObject.DefaultFont, Brushes.Black, e.Bounds);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            myGridControl1.ShowPrintPreview();
        }

    }
}