using System;
using DevExpress.XtraGrid.Views.Printing;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.Data;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid;

namespace MyXtraGrid
{
    public class MyGridViewPrintInfo : GridViewPrintInfo
    {

        public int FooterPanelHeight
        {
            get
            {
                return CalcStyleHeight(AppearancePrint.FooterPanel) + 4;
            }
        }
        public MyGridViewPrintInfo(DevExpress.XtraGrid.Views.Printing.PrintInfoArgs args) : base(args) { }

        public override void PrintFooterPanel(IBrickGraphics graph)
        {
            base.PrintFooterPanel(graph);
            CustomDrawFooterCells(graph);
        }

        private void CustomDrawFooterCells(IBrickGraphics graph)
        {
            if (!View.OptionsPrint.PrintFooter) return;
            foreach (PrintColumnInfo colInfo in Columns)
            {
                if (colInfo.Column.SummaryItem.SummaryType == SummaryItemType.None) continue;
                Rectangle r = Rectangle.Empty;
                r.X = colInfo.Bounds.X + Indent;
                r.Y = colInfo.RowIndex * FooterPanelHeight + 2 + Y;
                r.Width = colInfo.Bounds.Width;
                r.Height = FooterPanelHeight * colInfo.RowCount;
                r.X -= Indent;
                r.Y -= r.Height;
                string text = string.Empty;
                ImageBrick ib = GetImageBrick(colInfo, r, out text);
                if (ib != null)
                    graph.DrawBrick(ib, ib.Rect);
            }
        }

        private ImageBrick GetImageBrick(PrintColumnInfo colInfo, Rectangle rect, out string displayText)
        {
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            GraphicsCache cache = new GraphicsCache(Graphics.FromImage(bmp));
            FooterCellCustomDrawEventArgs args = (View as MyGridView).GetCustomDrawCellArgs(cache, rect, colInfo.Column);
            displayText = args.Info.DisplayText;
            if (!args.Handled)
                return null;
            BorderSide border = args.Appearance.Options.UseBorderColor? BorderSide.All: BorderSide.None;
            ImageBrick ib = new ImageBrick(border, 1, args.Appearance.BorderColor, args.Appearance.BackColor);
            ib.Rect = rect;
            ib.Image = bmp;
            return ib;
        }
    }
}
