using System;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Drawing;

namespace MyXtraGrid {
    [System.ComponentModel.DesignerCategory("")]
	public class MyGridView : GridView {
		public MyGridView() : this(null) {}
		public MyGridView(DevExpress.XtraGrid.GridControl grid) : base(grid) {
			// put your initialization code here
		}
		protected override string ViewName { get { return "MyGridView"; } }

        protected override DevExpress.XtraGrid.Views.Printing.BaseViewPrintInfo CreatePrintInfoInstance(DevExpress.XtraGrid.Views.Printing.PrintInfoArgs args)
        {
            return new MyGridViewPrintInfo(args);
        }

        public FooterCellCustomDrawEventArgs GetCustomDrawCellArgs(GraphicsCache cache, Rectangle rect, GridColumn col)
        {
            GridFooterCellInfoArgs styleArgs = new GridFooterCellInfoArgs(cache);
            styleArgs.Bounds = new Rectangle(new Point(0, 0), rect.Size);
            FooterCellCustomDrawEventArgs args = new FooterCellCustomDrawEventArgs(cache, -1, col, null, styleArgs);
            args.Handled = true;
            RaiseCustomDrawFooterCell(args);
            return args;
        }
	}
}