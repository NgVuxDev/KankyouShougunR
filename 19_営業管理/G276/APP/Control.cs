using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        
        private void CustomDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
            //■■■■■■■■■■■■
            //ヘッダセルの結合処理開始
            //■■■■■■■■■■■■
            if (e.RowIndex > -1)
            {
                // ヘッダー以外は処理なし
                return;
            }

            // 7列から結合
            int colIndex = 6;

            // 7～8列目を結合する処理
            if (e.ColumnIndex == colIndex || e.ColumnIndex == colIndex + 1)
            {
                // セルの矩形を取得
                Rectangle rect = e.CellBounds;

                DataGridView dgv = (DataGridView)sender;

                // 1列目の場合
                if (e.ColumnIndex == colIndex)
                {
                    // 2列目の幅を取得して、1列目の幅に足す
                    rect.Width += dgv.Columns[colIndex + 1].Width;
                    rect.Y = e.CellBounds.Y + 1;
                }
                else
                {
                    // 1列目の幅を取得して、2列目の幅に足す
                    rect.Width += dgv.Columns[colIndex].Width;
                    rect.Y = e.CellBounds.Y + 1;

                    // Leftを1列目に合わせる
                    rect.X -= dgv.Columns[colIndex].Width;
                }
                // 背景、枠線、セルの値を描画
                using (SolidBrush brush = new SolidBrush(CustomDataGridView.ColumnHeadersDefaultCellStyle.BackColor))
                {
                    // 背景の描画
                    e.Graphics.FillRectangle(brush, rect);

                    using (Pen pen = new Pen(dgv.GridColor))
                    {
                        // 枠線の描画
                        e.Graphics.DrawRectangle(pen, rect);
                    }

                    using (Pen pen1 = new Pen(Color.DarkGray))
                    {
                        // 直線を描画(ヘッダ上部)
                        e.Graphics.DrawLine(pen1, rect.X, rect.Y - 1, rect.X + rect.Width, rect.Y - 1);

                        // 直線を描画(ヘッダ下部)
                        e.Graphics.DrawLine(pen1, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
                    }
                }

                // セルに表示するテキストを描画
                TextRenderer.DrawText(e.Graphics,
                                "単位",
                                e.CellStyle.Font,
                                rect,
                                e.CellStyle.ForeColor,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
            }

            // 結合セル以外は既定の描画を行う
            if (!(e.ColumnIndex == colIndex || e.ColumnIndex == colIndex + 1))
            {
                e.Paint(e.ClipBounds, e.PaintParts);
            }

            // イベントハンドラ内で処理を行ったことを通知
            e.Handled = true;
        }

    }
}
