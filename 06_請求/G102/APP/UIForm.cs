using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.FormManager;
using Shougun.Core.Billing.Seikyushokakunin.Const;
using r_framework.CustomControl;

namespace Shougun.Core.Billing.Seikyushokakunin
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass Logic;
        /// <summary>
        ///請求番号
        /// </summary>
        public string SeikyuNumber { get; set; }
        /// <summary>
        ///処理モード
        /// </summary>
        public WINDOW_TYPE SyoriMode { get; set; }
        /// <summary>
        ///　現在のページ番号
        /// </summary>
        public int NowPageNo { get; set; }
        /// <summary>
        ///　総ページ数
        /// </summary>
        public string PageCnt { get; set; }
        /// <summary>
        ///　全体請求データ
        /// </summary>
        public DataTable SeikyuDt { get; set; }
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
        /// <summary>
        /// Guid Id
        /// </summary>
        public string transactionId;
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

        public UIForm(string seikyuNumber, WINDOW_TYPE syoriMode)
            : base(WINDOW_ID.T_SEIKYUSHO_KAKUNIN, syoriMode)
        {
            this.InitializeComponent();

            //パラメータ設定
            //請求番号
            Shougun.Core.Billing.Seikyushokakunin.Properties.Settings.Default.SEIKYUU_NUMBER = seikyuNumber;
            this.SeikyuNumber = seikyuNumber;
            this.SyoriMode = syoriMode;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);
            //DataGridViewのシェル結合
            this.MEISEI_DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.MEISEI_DGV.CellPainting += new DataGridViewCellPaintingEventHandler(Dgv_CellPainting);
        }
        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            this.transactionId = Guid.NewGuid().ToString();
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
            this.Logic.WindowInit();
            // Anchorの設定は必ずOnLoadで行うこと  
            if (this.MEISEI_DGV != null)
            {
                this.MEISEI_DGV.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.pnlBikou != null)
            {
                this.pnlBikou.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.pnlBikou.Location = new Point(this.pnlBikou.Location.X, this.MEISEI_DGV.Location.Y + this.MEISEI_DGV.Height + 1);
            }
        }

        ///// <summary>
        ///// 初回表示イベント
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnShown(EventArgs e)
        //{
        //    // この画面を最大化したくない場合は下記のように
        //    // OnShownでWindowStateをNomalに指定する
        //    //this.ParentForm.WindowState = FormWindowState.Normal;

        //    base.OnShown(e);            
        //}

        /// <summary>
        /// DataGridViewのCellPaintingイベント・ハンドラ
        /// </summary>
        void Dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var dgv = sender as CustomDataGridView;
            // 行・列共にヘッダは処理しない
            if (e.RowIndex == -1)
            {
                Rectangle rect;
                int indexOfSuuryou = dgv.Columns[ConstClass.COL_SUURYOU].Index;
                int indexOfTani = dgv.Columns[ConstClass.COL_UNIT_NAME].Index;
                if (e.ColumnIndex == indexOfSuuryou
                    || e.ColumnIndex == indexOfTani)
                {
                    rect = e.CellBounds;
                    if (e.ColumnIndex == indexOfSuuryou)
                    {
                        rect.Width = rect.Width + dgv.Columns[indexOfTani].Width;
                    }
                    else
                    {
                        rect.Width = rect.Width + dgv.Columns[indexOfSuuryou].Width;
                        rect.X = rect.X - dgv.Columns[indexOfSuuryou].Width;
                    }
                    e.Graphics.FillRectangle(
                       new SolidBrush((dgv).ColumnHeadersDefaultCellStyle.BackColor), rect);
                    rect.Y = rect.Y + 1;
                    e.Graphics.DrawRectangle(new Pen(dgv.ColumnHeadersDefaultCellStyle.ForeColor), rect);
                    TextRenderer.DrawText(e.Graphics,
                                 "数量",
                                 e.CellStyle.Font, rect, e.CellStyle.ForeColor,
                                 TextFormatFlags.HorizontalCenter
                                 | TextFormatFlags.VerticalCenter);
                    e.Handled = true;
                }
            }
            else
            {
                // セル結合時に選択行とそれ以外の色をわける
                Color backcolor = e.CellStyle.BackColor;
                Color forecolor = e.CellStyle.ForeColor;
                if (dgv.SelectedRows.Count > 0)
                {
                    if (dgv.SelectedRows[0].Index == e.RowIndex)
                    {
                        backcolor = e.CellStyle.SelectionBackColor;
                        forecolor = e.CellStyle.SelectionForeColor;
                    }
                }

                //シェル結合フラグ（１：業者、２：現場）
                object ketugoFlg = dgv.Rows[e.RowIndex].Cells[ConstClass.COL_OUT_FLG].Value;
                //業者名行シェル結合
                if ("1".Equals(ketugoFlg))
                {
                    if (e.ColumnIndex > 0)
                    {
                        // セルの矩形を取得
                        Rectangle rect = e.CellBounds;
                        string txt = dgv.Rows[e.RowIndex].Cells[ConstClass.COL_DENPYOU_DATE].FormattedValue.ToString();

                        int beginIndex = dgv.Columns[ConstClass.COL_DENPYOU_DATE].Index;
                        int width = 0;
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            if (col.Visible)
                            {
                                if (col.Index >= beginIndex)
                                {
                                    width = width + col.Width;
                                }
                                if (col.Index < e.ColumnIndex
                                    && col.Index >= beginIndex)
                                {
                                    rect.X = rect.X - col.Width;
                                }
                            }
                        }
                        rect.Width = width;
                        rect.X -= 1;
                        rect.Y -= 1;
                        if (dgv.CurrentCell.RowIndex == e.RowIndex)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), rect);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(new SolidBrush(backcolor), rect);
                        }
                        e.Graphics.DrawRectangle(new Pen(dgv.GridColor), rect);

                        TextRenderer.DrawText(e.Graphics,
                                     txt,
                                     e.CellStyle.Font, rect, forecolor,
                                     TextFormatFlags.Left
                                     | TextFormatFlags.VerticalCenter);
                        e.Handled = true;
                    }
                }
                //現場名行シェル結合
                else if ("2".Equals(ketugoFlg))
                {
                    if (e.ColumnIndex > 1)
                    {
                        Rectangle rect = e.CellBounds;
                        string txt = dgv.Rows[e.RowIndex].Cells[ConstClass.COL_DENPYOU_DATE].FormattedValue.ToString();

                        int beginIndex = dgv.Columns[ConstClass.COL_DENPYOU_DATE].Index;
                        int width = 0;
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            if (col.Visible)
                            {
                                if (col.Index >= beginIndex)
                                {
                                    width = width + col.Width;
                                }
                                if (col.Index < e.ColumnIndex
                                    && col.Index >= beginIndex)
                                {
                                    rect.X = rect.X - col.Width;
                                }
                            }
                        }
                        rect.Width = width;
                        rect.X -= 1;
                        rect.Y -= 1;

                        if (dgv.CurrentCell.RowIndex == e.RowIndex)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), rect);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(new SolidBrush(backcolor), rect);
                        }
                        e.Graphics.DrawRectangle(new Pen(dgv.GridColor), rect);

                        TextRenderer.DrawText(e.Graphics,
                                     txt,
                                     e.CellStyle.Font, rect, forecolor,
                                     TextFormatFlags.Left
                                     | TextFormatFlags.VerticalCenter);
                        e.Handled = true;
                    }
                }
            }
            #region Old
            //// 行・列共にヘッダは処理しない
            //if (e.RowIndex == -1)
            //{
            //    Rectangle rect;
            //    if (e.ColumnIndex == 4)
            //    {
            //        rect = e.CellBounds;
            //        rect.Width = rect.Width + ((DataGridView)sender).Columns[5].Width;
            //        e.Graphics.FillRectangle(
            //           new SolidBrush(((DataGridView)sender).ColumnHeadersDefaultCellStyle.BackColor), rect);
            //        rect.Y = rect.Y + 1;
            //        e.Graphics.DrawRectangle(new Pen(this.MEISEI_DGV.ColumnHeadersDefaultCellStyle.ForeColor), rect);
            //        TextRenderer.DrawText(e.Graphics,
            //                     e.FormattedValue.ToString(),
            //                     e.CellStyle.Font, rect, e.CellStyle.ForeColor,
            //                     TextFormatFlags.HorizontalCenter
            //                     | TextFormatFlags.VerticalCenter);
            //        e.Handled = true;
            //    }
            //    else if (e.ColumnIndex == 5)
            //    {
            //        e.Handled = true;
            //    }
            //}
            //else
            //{
            //    // セル結合時に選択行とそれ以外の色をわける
            //    Color backcolor = e.CellStyle.BackColor;
            //    Color forecolor = e.CellStyle.ForeColor;
            //    if (MEISEI_DGV.SelectedRows.Count > 0)
            //    {
            //        if (MEISEI_DGV.SelectedRows[0].Index == e.RowIndex)
            //        {
            //            backcolor = e.CellStyle.SelectionBackColor;
            //            forecolor = e.CellStyle.SelectionForeColor;
            //        }
            //    }

            //    //シェル結合フラグ（１：業者、２：現場）
            //    object ketugoFlg = ((DataGridView)sender).Rows[e.RowIndex].Cells[10].Value;
            //    //業者名行シェル結合
            //    if ("1".Equals(ketugoFlg))
            //    {
            //        if (e.ColumnIndex > 0)
            //        {
            //            // セルの矩形を取得
            //            Rectangle rect = e.CellBounds;

            //            DataGridView dgv = (DataGridView)sender;
            //            int colIndex = 1;
            //            string gyoushaName = dgv.Rows[e.RowIndex].Cells[colIndex].FormattedValue.ToString();

            //            // 1列目の場合
            //            if (e.ColumnIndex == colIndex)
            //            {
            //                // 2列目の幅を取得して、1列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width
            //                             + dgv.Columns[colIndex + 3].Width
            //                             + dgv.Columns[colIndex + 4].Width
            //                             + dgv.Columns[colIndex + 5].Width
            //                             + dgv.Columns[colIndex + 6].Width
            //                             + dgv.Columns[colIndex + 7].Width
            //                             + dgv.Columns[colIndex + 8].Width;

            //                rect.X -= 1;
            //                rect.Y -= 1;
            //            }
            //            else if (e.ColumnIndex == colIndex + 1)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 2)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 3)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 4)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width
            //                             + dgv.Columns[colIndex + 3].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 5)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width
            //                             + dgv.Columns[colIndex + 3].Width
            //                             + dgv.Columns[colIndex + 4].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 6)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width
            //                             + dgv.Columns[colIndex + 3].Width
            //                             + dgv.Columns[colIndex + 4].Width
            //                             + dgv.Columns[colIndex + 5].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width + dgv.Columns[colIndex + 5].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 7)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width
            //                             + dgv.Columns[colIndex + 3].Width
            //                             + dgv.Columns[colIndex + 4].Width
            //                             + dgv.Columns[colIndex + 5].Width
            //                             + dgv.Columns[colIndex + 6].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width + dgv.Columns[colIndex + 5].Width + dgv.Columns[colIndex + 6].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 8)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width
            //                             + dgv.Columns[colIndex + 3].Width
            //                             + dgv.Columns[colIndex + 4].Width
            //                             + dgv.Columns[colIndex + 5].Width
            //                             + dgv.Columns[colIndex + 6].Width
            //                             + dgv.Columns[colIndex + 7].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width + dgv.Columns[colIndex + 5].Width + dgv.Columns[colIndex + 6].Width + dgv.Columns[colIndex + 7].Width);
            //            }

            //            if (dgv.CurrentCell.RowIndex == e.RowIndex)
            //            {
            //                e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), rect);
            //            }
            //            else
            //            {
            //                e.Graphics.FillRectangle(new SolidBrush(backcolor), rect);
            //            }
            //            e.Graphics.DrawRectangle(new Pen(this.MEISEI_DGV.GridColor), rect);

            //            TextRenderer.DrawText(e.Graphics,
            //                         gyoushaName,
            //                         e.CellStyle.Font, rect, forecolor,
            //                         TextFormatFlags.Left
            //                         | TextFormatFlags.VerticalCenter);
            //            e.Handled = true;
            //        }
            //        else if (e.ColumnIndex != 0)
            //        {
            //            e.Handled = true;
            //        }
            //    }
            //    //現場名行シェル結合
            //    else if ("2".Equals(ketugoFlg))
            //    {
            //        if (e.ColumnIndex > 1)
            //        {
            //            Rectangle rect = e.CellBounds;

            //            DataGridView dgv = (DataGridView)sender;
            //            int colIndex = 2;
            //            string genbaName = dgv.Rows[e.RowIndex].Cells[colIndex].FormattedValue.ToString();

            //            // 1列目の場合
            //            if (e.ColumnIndex == colIndex)
            //            {
            //                // 2列目の幅を取得して、1列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width
            //                             + dgv.Columns[colIndex + 3].Width
            //                             + dgv.Columns[colIndex + 4].Width
            //                             + dgv.Columns[colIndex + 5].Width
            //                             + dgv.Columns[colIndex + 6].Width
            //                             + dgv.Columns[colIndex + 7].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;
            //            }
            //            else if (e.ColumnIndex == colIndex + 1)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 2)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 3)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 4)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width
            //                             + dgv.Columns[colIndex + 3].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 5)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width
            //                             + dgv.Columns[colIndex + 3].Width
            //                             + dgv.Columns[colIndex + 4].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 6)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width
            //                             + dgv.Columns[colIndex + 3].Width
            //                             + dgv.Columns[colIndex + 4].Width
            //                             + dgv.Columns[colIndex + 5].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width + dgv.Columns[colIndex + 5].Width);
            //            }
            //            else if (e.ColumnIndex == colIndex + 7)
            //            {
            //                // 1列目の幅を取得して、2列目の幅に足す
            //                rect.Width = rect.Width
            //                             + dgv.Columns[colIndex].Width
            //                             + dgv.Columns[colIndex + 1].Width
            //                             + dgv.Columns[colIndex + 2].Width
            //                             + dgv.Columns[colIndex + 3].Width
            //                             + dgv.Columns[colIndex + 4].Width
            //                             + dgv.Columns[colIndex + 5].Width
            //                             + dgv.Columns[colIndex + 6].Width;
            //                rect.X -= 1;
            //                rect.Y -= 1;

            //                // Leftを1列目に合わせる
            //                rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width + dgv.Columns[colIndex + 5].Width + dgv.Columns[colIndex + 6].Width);
            //            }

            //            if (dgv.CurrentCell.RowIndex == e.RowIndex)
            //            {
            //                e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), rect);
            //            }
            //            else
            //            {
            //                e.Graphics.FillRectangle(new SolidBrush(backcolor), rect);
            //            }
            //            e.Graphics.DrawRectangle(new Pen(this.MEISEI_DGV.GridColor), rect);

            //            TextRenderer.DrawText(e.Graphics,
            //                         genbaName,
            //                         e.CellStyle.Font, rect, forecolor,
            //                         TextFormatFlags.Left
            //                         | TextFormatFlags.VerticalCenter);
            //            e.Handled = true;
            //        }
            //        else if (e.ColumnIndex != 0 && e.ColumnIndex != 1)
            //        {
            //            e.Handled = true;
            //        }
            //    }
            //}
            #endregion
        }

        /// <summary>
        /// 前ページイベント処理
        /// </summary>
        /// <returns></returns>
        public virtual void PrevPage(object sender, EventArgs e)
        {
            //save value bikou before when previous
            this.Logic.setSeikyuuKagamiBikouInfo();

            this.NowPageNo = this.NowPageNo - 1;
            //F1F2ボタン活性/非活性設定
            this.Logic.SetF1F2ButtonEnabled();
            //請求伝票明細データ設定
            this.Logic.SetSeikyuDenpyo();
        }
        /// <summary>
        /// 次ページイベント処理
        /// </summary>
        /// <returns></returns>
        public virtual void NextPage(object sender, EventArgs e)
        {
            //save value bikou before when previous
            this.Logic.setSeikyuuKagamiBikouInfo();

            this.NowPageNo = this.NowPageNo + 1;
            //F1F2ボタン活性/非活性設定
            this.Logic.SetF1F2ButtonEnabled();
            //請求伝票明細データ設定
            this.Logic.SetSeikyuDenpyo();
        }
        /// <summary>
        /// 一覧イベント処理
        /// </summary>
        /// <returns></returns>
        public virtual void Ichiran(object sender, EventArgs e)
        {
            //請求一覧画面表示
            UIHeader headerForm = (UIHeader)((BusinessBaseForm)this.Parent).headerForm;
            FormManager.OpenForm("G103", headerForm.KYOTEN_CD.Text, this.CD.Text, this.SEIKYU_YMD.Text);
        }
        /// <summary>
        /// 登録イベント処理
        /// </summary>
        /// <returns></returns>
        public virtual void Regist(object sender, EventArgs e)
        {
            //登録イベント処理
            if (this.Logic.Regist())
            {
                var parentForm = (BusinessBaseForm)this.Parent;
                //画面クローズ
                this.Close();
                parentForm.Close();

                FormManager.UpdateForm("G103");
            }
        }
        /// <summary>
        /// 参照イベント処理
        /// </summary>
        /// <returns></returns>
        public virtual void Sansyo(object sender, EventArgs e)
        {
            if (this.MEISEI_DGV.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.MEISEI_DGV.SelectedRows[0];
                string strFormId = "";
                bool isNyuukin = false;
                //if ("1".Equals(row.Cells[ConstClass.COL_OUT_FLG].Value))
                //{
                //    row.Cells[1].Selected = true;
                //}
                //if ("2".Equals(row.Cells[ConstClass.COL_OUT_FLG].Value))
                //{
                //    row.Cells[2].Selected = true;
                //}
                if ("3".Equals(row.Cells[ConstClass.COL_OUT_FLG].Value))
                {
                    //伝票種類が【受入】の場合は、受入入力画面へ遷移する。
                    if (Const.ConstClass.DENPYOU_SHURUI_CD_1.Equals(row.Cells[ConstClass.COL_DENPYOU_SHURUI_CD].Value.ToString()))
                    {
                        if (this.Logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                        {
                            strFormId = "G051";
                        }
                        else
                        {
                            strFormId = "G721";
                        }
                    }
                    //伝票種類が【出荷】の場合は、出荷入力画面へ遷移する。
                    else if (Const.ConstClass.DENPYOU_SHURUI_CD_2.Equals(row.Cells[ConstClass.COL_DENPYOU_SHURUI_CD].Value.ToString()))
                    {
                        if (this.Logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                        {
                            strFormId = "G053";
                        }
                        else
                        {
                            strFormId = "G722";
                        }
                    }
                    //伝票種類が【売上】の場合は、売上入力画面へ遷移する。
                    else if (Const.ConstClass.DENPYOU_SHURUI_CD_3.Equals(row.Cells[ConstClass.COL_DENPYOU_SHURUI_CD].Value.ToString()))
                    {
                        // 20150602 代納伝票対応(代納不具合一覧52) Start
                        T_UR_SH_ENTRY entry = new T_UR_SH_ENTRY();
                        entry.UR_SH_NUMBER = Convert.ToInt64(row.Cells[ConstClass.COL_DENPYOU_NUMBER].Value);
                        entry.DELETE_FLG = false;
                        entry = this.Logic.UrShEntryDao.GetDataForEntity(entry).FirstOrDefault();
                        if (entry != null)
                        {
                            if (entry.DAINOU_FLG.IsNull || entry.DAINOU_FLG.IsFalse)
                            {
                                strFormId = "G054";
                            }
                            else
                            {
                                strFormId = "G161";
                            }
                        }
                        // 20150602 代納伝票対応(代納不具合一覧52) End
                    }
                    // 伝票種類が【入金】の場合は、入金入力画面へ遷移する。
                    else if (Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(row.Cells[ConstClass.COL_DENPYOU_SHURUI_CD].Value.ToString()))
                    {
                        T_NYUUKIN_ENTRY entry = new T_NYUUKIN_ENTRY();
                        entry.NYUUKIN_NUMBER = Convert.ToInt64(row.Cells[ConstClass.COL_DENPYOU_NUMBER].Value);
                        entry = this.Logic.NyuukinEntryDao.GetDataForEntity(entry).FirstOrDefault();

                        if (entry != null && entry.TOK_INPUT_KBN.IsFalse)
                        {
                            strFormId = "G619";
                            isNyuukin = true;
                        }
                        else
                        {
                            strFormId = "G459";
                            isNyuukin = true;
                        }
                    }
                    else
                    {
                        return;
                    }

                    //参照モードで起動
                    if (isNyuukin)
                    {
                        // 入金入力画面は引数が他と異なる
                        FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, row.Cells[ConstClass.COL_DENPYOU_NUMBER].Value.ToString());
                    }
                    else
                    {
                        FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, long.Parse(row.Cells[ConstClass.COL_DENPYOU_NUMBER].Value.ToString()));
                    }
                }
            }
        }
        /// <summary>
        /// クリックイベント処理
        /// </summary>
        /// <returns></returns>
        public virtual void selectCellCange(object sender, EventArgs e)
        {
            if (this.MEISEI_DGV.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.MEISEI_DGV.SelectedRows[0];
                if ("1".Equals(row.Cells[ConstClass.COL_OUT_FLG].Value))
                {
                    row.Cells[0].Selected = true;
                }
                if ("2".Equals(row.Cells[ConstClass.COL_OUT_FLG].Value))
                {
                    row.Cells[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// CSVイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVPrint(object sender, EventArgs e)
        {
            this.Logic.CSVPrint();
        }

        /// <summary>
        /// プレビューイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PreView(object sender, EventArgs e)
        {
            this.Logic.PreView();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            //画面クローズ
            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// コンストラクタで渡された請求番号のデータ存在するかチェック
        /// </summary>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool IsExistSeikyuData()
        {
            bool catchErr = true;
            return this.Logic.IsExistSeikyuData(this.SeikyuNumber, out catchErr);
        }
    }
}
