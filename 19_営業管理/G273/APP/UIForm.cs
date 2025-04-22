using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.BusinessManagement.EigyoYosanNyuuryoku.Logic;

namespace Shougun.Core.BusinessManagement.EigyoYosanNyuuryoku
{
    /// <summary>
    /// G273：営業予算入力
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region 内部変数

        /// <summary>
        /// ヘッダクラス
        /// </summary>
        internal HeaderForm header;

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        public UIForm(HeaderForm headerForm)
            : base(WINDOW_ID.T_YOSAN_NYUURYOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            //ヘッダ
            this.header = headerForm;
            this.logic.SetHeaderInfo(this.header);
        }
        #endregion

        #region 画面コントロールイベント
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.logic.WindowInit()) { return; }        // 画面情報の初期化

            if (!isShown)
            {
                this.Height -= 7;
                this.Width -= 10;
                isShown = true;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                int GRID_HEIGHT_MIN_VALUE = 407;
                int GRID_WIDTH_MIN_VALUE = 649;
                int h = this.Height - 34;
                int w = this.Width;

                if (h < GRID_HEIGHT_MIN_VALUE)
                {
                    this.customDataGridView1.Height = GRID_HEIGHT_MIN_VALUE;
                }
                else
                {
                    this.customDataGridView1.Height = h;
                }
                if (w < GRID_WIDTH_MIN_VALUE)
                {
                    this.customDataGridView1.Width = GRID_WIDTH_MIN_VALUE;
                }
                else
                {
                    this.customDataGridView1.Width = w;
                }

                if (this.customDataGridView1.Height <= GRID_HEIGHT_MIN_VALUE
                    || this.customDataGridView1.Width <= GRID_WIDTH_MIN_VALUE)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                }
                else
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
        }
        #endregion

        #region グリッドイベント：列ヘッダーの罫線を消して結合しているように表示(セル連結はしないため削除)
        ///// <summary>
        ///// 列ヘッダーの罫線を消して結合しているように表示
        ///// </summary>
        ///// <param name="sender">イベント呼び出し元オブジェクト</param>
        ///// <param name="e">e</param>
        //private void customDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    //修正箇所[FormとLogic2箇所]
        //    //※[罫線を非表示設定の場合のみ使用の為コメントアウト]罫線を非表示に設定
        //    //■■■■■■■■■■■■■■■■■■■
        //    //ヘッダセルの罫線削除(結合風味)処理開始
        //    //■■■■■■■■■■■■■■■■■■■
        //    //if (e.ColumnIndex == 0 && e.RowIndex == -1)
        //    //{
        //    //    e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
        //    //}
        //    //else if (e.ColumnIndex == 1 && e.RowIndex == -1)
        //    //{
        //    //    e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
        //    //}
        //    //else if (e.ColumnIndex == 2 && e.RowIndex == -1)
        //    //{
        //    //    e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
        //    //}
        //    //else if (e.ColumnIndex == 3 && e.RowIndex == -1)
        //    //{
        //    //    e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
        //    //}

        //    //■■■■■■■■■■■■
        //    //ヘッダセルの結合処理開始
        //    //■■■■■■■■■■■■
        //    if (e.RowIndex > -1)
        //    {
        //        // ヘッダー以外は処理なし
        //        return;
        //    }

        //    // 1～2列目を結合する処理
        //    // 処理対象セルが、2列目の場合のみ処理を行う
        //    if (e.ColumnIndex == 0)
        //    {
        //        // セルの矩形を取得
        //        Rectangle rect = e.CellBounds;

        //        DataGridView dgv = (DataGridView)sender;

        //        // 2列目の幅を取得して、1列目の幅に足す
        //        rect.Width += dgv.Columns[1].Width;
        //        rect.Y = e.CellBounds.Y + 1;

        //        // 背景、枠線、セルの値を描画
        //        using (SolidBrush brush = new SolidBrush(this.customDataGridView1.ColumnHeadersDefaultCellStyle.BackColor))
        //        {
        //            // 背景の描画
        //            e.Graphics.FillRectangle(brush, rect);

        //            using (Pen pen = new Pen(dgv.GridColor))
        //            {
        //                // 枠線の描画
        //                e.Graphics.DrawRectangle(pen, rect);
        //            }
        //        }

        //        // セルに表示するテキストを描画
        //        TextRenderer.DrawText(e.Graphics,
        //                        "部署名",
        //                        e.CellStyle.Font,
        //                        rect,
        //                        e.CellStyle.ForeColor,
        //                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
        //    }
        //    else if (e.ColumnIndex == 2)
        //    {
        //        // セルの矩形を取得
        //        Rectangle rect_2 = e.CellBounds;

        //        DataGridView dgv = (DataGridView)sender;

        //        // 4列目の幅を取得して、3列目の幅に足す
        //        rect_2.Width += dgv.Columns[3].Width;
        //        rect_2.Y = e.CellBounds.Y + 1;

        //        // 背景、枠線、セルの値を描画
        //        using (SolidBrush brush = new SolidBrush(this.customDataGridView1.ColumnHeadersDefaultCellStyle.BackColor))
        //        {
        //            // 背景の描画
        //            e.Graphics.FillRectangle(brush, rect_2);

        //            using (Pen pen = new Pen(dgv.GridColor))
        //            {
        //                // 枠線の描画
        //                e.Graphics.DrawRectangle(pen, rect_2);
        //            }
        //        }

        //        // セルに表示するテキストを描画
        //        TextRenderer.DrawText(e.Graphics,
        //                        "営業者名",
        //                        e.CellStyle.Font,
        //                        rect_2,
        //                        e.CellStyle.ForeColor,
        //                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
        //    }

        //    // 結合セル以外は既定の描画を行う
        //    if (!(e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3))
        //    {
        //        e.Paint(e.ClipBounds, e.PaintParts);
        //    }

        //    // イベントハンドラ内で処理を行ったことを通知
        //    e.Handled = true;
            
        //}
        #endregion
    }
}
