using System;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;

namespace Shougun.Core.Allocation.CarTransferSpot
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private CarTransferSpot.LogicClass IchiranLogic;

        private string preCellCd { get; set; }
        private string curCellCd { get; set; }


        public UIForm()
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.IchiranLogic = new LogicClass(this);
        }
        public UIForm(string haishaDenpyouNo)
            : base(WINDOW_ID.T_MOBILE_TASHA_SUPOTTO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.IchiranLogic = new LogicClass(this);

            // (配車)伝票番号設定
            this.IchiranLogic.haishaDenpyouNo = haishaDenpyouNo;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);


                //画面情報の初期化
                if (!this.IchiranLogic.WindowInit()) { return; }
                //Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        //////////////////////
        //明細チェックボックス
        //////////////////////
        #region 列ヘッダーにチェックボックスを表示
        /// <summary>
        /// 列ヘッダーにチェックボックスを表示
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(25, 25))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // 描画領域の中央に配置
                        Point pt1 = new Point((bmp.Width - checkBoxAll.Width) / 2, (bmp.Height - checkBoxAll.Height) / 2);
                        if (pt1.X < 0) pt1.X = 0;
                        if (pt1.Y < 0) pt1.Y = 0;

                        // Bitmapに描画
                        checkBoxAll.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                        // DataGridViewの現在描画中のセルの中央に描画
                        int x = (e.CellBounds.Width - bmp.Width);
                        int y = (e.CellBounds.Height - bmp.Height) / 2;

                        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp, pt2);
                        e.Handled = true;
                    }
                }
            }
            if (e.ColumnIndex == 2)
            {
                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(25, 25))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // 描画領域の中央に配置
                        Point pt1 = new Point((bmp.Width - checkBoxAll2.Width) / 2, (bmp.Height - checkBoxAll2.Height) / 2);
                        if (pt1.X < 0) pt1.X = 0;
                        if (pt1.Y < 0) pt1.Y = 0;

                        // Bitmapに描画
                        checkBoxAll2.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                        // DataGridViewの現在描画中のセルの中央に描画
                        int x = (e.CellBounds.Width - bmp.Width);
                        int y = (e.CellBounds.Height - bmp.Height) / 2;

                        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp, pt2);
                        e.Handled = true;
                    }
                }
            }
        }
        #endregion
        #region 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void Ichiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //[対象]のヘッダをクリック
            if (e.ColumnIndex == 1 && e.RowIndex == -1)
            {
                checkBoxAll.Checked = !checkBoxAll.Checked;
                this.Ichiran.Refresh();
            }
            //[モバイル連携]のヘッダをクリック
            if (e.ColumnIndex == 2 && e.RowIndex == -1)
            {
                checkBoxAll2.Checked = !checkBoxAll2.Checked;
                this.Ichiran.Refresh();
            }

            //[モバイル連携]のヘッダをクリック
            if (e.ColumnIndex == 2 && e.RowIndex >=0)
            {
                //スペースで、OFFの場合は抜ける
                if (((bool)this.IchiranLogic.SpaceChk) && (!(bool)this.IchiranLogic.SpaceON))
                {
                    return;
                }
                this.IchiranLogic.SpaceON = false;

                //マウスクリック⇒チェック反転前に処理が走る
                //スペースキー⇒チェック反転後に処理が走る
                //ので、チェックに引っかかった時に、[モバイル連携]のON/OFF設定は逆になる。
                if (!(bool)this.Ichiran[2, e.RowIndex].Value)
                {
                    //チェック条件
                    string CheckUketsukeNumber = this.Ichiran[int.Parse(this.Ichiran.Columns["UKETSUKE_NUMBER"].Index.ToString()), e.RowIndex].Value.ToString();

                    //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                    if (this.Ichiran[int.Parse(this.Ichiran.Columns["SAGYOU_DATE"].Index.ToString()), e.RowIndex].Value == null)
                    {
                        this.Ichiran[2, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                        this.IchiranLogic.SpaceChk = false;
                        this.Ichiran.Refresh();
                        this.IchiranLogic.MsgBox.MessageBoxShowError("作業日が当日の場合のみ連携が可能です。");
                        return;
                    }
                    else
                    {
                        if (!(DateTime.Parse(this.Ichiran[int.Parse(this.Ichiran.Columns["SAGYOU_DATE"].Index.ToString()), e.RowIndex].Value.ToString()).ToString("yyyy/MM/dd").Equals(DateTime.Now.ToString("yyyy/MM/dd"))))
                        {
                            this.Ichiran[2, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                            this.IchiranLogic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.IchiranLogic.MsgBox.MessageBoxShowError("作業日が当日の場合のみ連携が可能です。");
                            return;
                        }
                    }

                    //変更するデータが入力されているか
                    //UNTENSHA_CD、SHARYOU_CD、SHASHU_CD、SHASHU_NAME、TORIHIKISAKI_CDのいずれかが無し→×。
                    if ((this.Ichiran[int.Parse(this.Ichiran.Columns["UNTENSHA_CD"].Index.ToString()), e.RowIndex].Value == null)
                        || (this.Ichiran[int.Parse(this.Ichiran.Columns["SHARYOU_CD"].Index.ToString()), e.RowIndex].Value == null)
                        || (this.Ichiran[int.Parse(this.Ichiran.Columns["SHASHU_CD"].Index.ToString()), e.RowIndex].Value == null)
                        || (this.Ichiran[int.Parse(this.Ichiran.Columns["SHASHU_NAME"].Index.ToString()), e.RowIndex].Value == null)
                        || (this.Ichiran[int.Parse(this.Ichiran.Columns["TORIHIKISAKI_CD"].Index.ToString()), e.RowIndex].Value == null))
                    {
                        this.Ichiran[2, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                        this.IchiranLogic.SpaceChk = false;
                        this.Ichiran.Refresh();
                        this.IchiranLogic.MsgBox.MessageBoxShowError("モバイル将軍へ連携する条件になっていません。");
                        return;
                    }

                    if ((string.IsNullOrEmpty(this.Ichiran[int.Parse(this.Ichiran.Columns["UNTENSHA_CD"].Index.ToString()), e.RowIndex].Value.ToString()))
                        || (string.IsNullOrEmpty(this.Ichiran[int.Parse(this.Ichiran.Columns["SHARYOU_CD"].Index.ToString()), e.RowIndex].Value.ToString()))
                        || (string.IsNullOrEmpty(this.Ichiran[int.Parse(this.Ichiran.Columns["SHASHU_CD"].Index.ToString()), e.RowIndex].Value.ToString()))
                        || (string.IsNullOrEmpty(this.Ichiran[int.Parse(this.Ichiran.Columns["SHASHU_NAME"].Index.ToString()), e.RowIndex].Value.ToString()))
                        || (string.IsNullOrEmpty(this.Ichiran[int.Parse(this.Ichiran.Columns["TORIHIKISAKI_CD"].Index.ToString()), e.RowIndex].Value.ToString())))
                    {
                        this.Ichiran[2, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                        this.IchiranLogic.SpaceChk = false;
                        this.Ichiran.Refresh();
                        this.IchiranLogic.MsgBox.MessageBoxShowError("モバイル将軍へ連携する条件になっていません。");
                        return;
                    }

                    //配車番号が、ロジコン連携されているか
                    if (this.IchiranLogic.RenkeiCheck(1, CheckUketsukeNumber))
                    {
                        this.Ichiran[2, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                        this.IchiranLogic.SpaceChk = false;
                        this.Ichiran.Refresh();
                        this.IchiranLogic.MsgBox.MessageBoxShowError("ロジこんぱす連携中の為、変更する事は出来ません。");
                        return;
                    }
                    if (this.IchiranLogic.SpaceChk)
                    {
                        this.Ichiran[2, e.RowIndex].Value = !(bool)this.Ichiran[2, e.RowIndex].Value;
                        this.IchiranLogic.SpaceChk = false;
                    }
                    //[ﾓﾊﾞｲﾙ連携]にチェックが付けれたら、[対象]チェックをONにする
                    this.Ichiran[1, e.RowIndex].Value = true;
                }
                else
                {
                    this.IchiranLogic.SpaceChk = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// [対象][モバイル連携]で、スペースキーでチェック処理が走るように下準備
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                DataGridViewCell curCell = this.Ichiran.CurrentCell;
                //[モバイル連携]
                if (curCell.ColumnIndex == 2 && curCell.RowIndex >= 0)
                {
                    //[モバイル連携]OFFにする場合は、何もしない。
                    //[モバイル連携]ONにする場合は、一度チェックボックスを反転させておく(チェック処理中に画面上ONになってしまうので)
                    this.IchiranLogic.SpaceChk = true;
                    this.IchiranLogic.SpaceON = false;
                    if (!(bool)this.Ichiran[2, curCell.RowIndex].Value)
                    {
                        this.IchiranLogic.SpaceON = true;
                        this.Ichiran[2, curCell.RowIndex].Value = !(bool)this.Ichiran[2, curCell.RowIndex].Value;
                        this.Ichiran.Refresh();
                    }
                }
            }
        }

        #region 明細全チェック
        /// <summary>
        /// [対象]全チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Ichiran.Rows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in this.Ichiran.Rows)
            {
                row.Cells[1].Value = checkBoxAll.Checked;
            }
            this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[1];
            this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[2];
            this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[1];
        }
        /// <summary>
        /// [モバイル連携]全チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAll2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Ichiran.Rows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in this.Ichiran.Rows)
            {

                string CheckUketsukeNumber = row.Cells["UKETSUKE_NUMBER"].Value.ToString();

                //[ﾓﾊﾞｲﾙ連携]ON
                if (checkBoxAll2.Checked)
                {
                    //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                    //[システム日付] != [作業日]の場合はチェックをつけない
                    if ((row.Cells["SAGYOU_DATE"].Value == null)
                        || (string.IsNullOrEmpty(row.Cells["SAGYOU_DATE"].Value.ToString())))
                    {
                        continue;
                    }
                    else
                    {
                        if (!(DateTime.Parse(row.Cells["SAGYOU_DATE"].Value.ToString()).ToString("yyyy/MM/dd")).Equals(DateTime.Now.ToString("yyyy/MM/dd")))
                        {
                            continue;
                        }
                    }

                    //UNTENSHA_CD、SHARYOU_CD、SHASHU_CD、SHASHU_NAME、TORIHIKISAKI_CDのいずれかが無し→×。
                    if ((row.Cells["UNTENSHA_CD"].Value == null)
                        || (row.Cells["SHARYOU_CD"].Value == null)
                        || (row.Cells["SHASHU_CD"].Value == null)
                        || (row.Cells["SHARYOU_NAME"].Value == null)
                        || (row.Cells["TORIHIKISAKI_CD"].Value == null))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(row.Cells["UNTENSHA_CD"].Value.ToString())
                        || string.IsNullOrEmpty(row.Cells["SHARYOU_CD"].Value.ToString())
                        || string.IsNullOrEmpty(row.Cells["SHASHU_CD"].Value.ToString())
                        || string.IsNullOrEmpty(row.Cells["SHARYOU_NAME"].Value.ToString())
                        || string.IsNullOrEmpty(row.Cells["TORIHIKISAKI_CD"].Value.ToString()))
                    {
                        continue;
                    }

                    //ロジコンチェック
                    if (this.IchiranLogic.RenkeiCheck(1, CheckUketsukeNumber))
                    {
                        continue;
                    }

                    row.Cells[2].Value = checkBoxAll2.Checked;
                    //[ﾓﾊﾞｲﾙ連携]にチェックが付けれたら、[対象]チェックをONにする
                    row.Cells[1].Value = true;
                }
                else
                {
                    row.Cells[2].Value = checkBoxAll2.Checked;
                }

            }
            this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[2];
            this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[1];
            this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[2];
        }
        #endregion

        /////////////////////
        //明細keyDown
        /////////////////////
        #region 拠点POP
        /// <summary>
        /// 拠点 PopupAfterExecuteMethod
        /// </summary>
        public void KYOTEN_PopupAfterExecuteMethod()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            if (this.Ichiran.CurrentCell.ColumnIndex == 5)
            {
                curCellCd = this.Ichiran.CurrentCell.EditedFormattedValue.ToString();
                if (preCellCd != curCellCd)
                {
                    this.IchiranLogic.ChechiKyotenCd(this.Ichiran.Rows[this.Ichiran.CurrentCell.RowIndex]);
                }
            }
        }
        /// <summary>
        /// 拠点 PopupBeforeExecuteMethod
        /// </summary>
        public void KYOTEN_PopupBeforeExecuteMethod()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            preCellCd = string.Empty;
            if (this.Ichiran.CurrentCell.ColumnIndex == 5)
            {
                preCellCd = this.Ichiran.CurrentCell.EditedFormattedValue.ToString();
            }
        }
        #endregion 

        #region 運搬POP
        /// <summary>
        /// 運搬 PopupAfterExecuteMethod
        /// </summary>
        public void UNPAN_PopupAfterExecuteMethod()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            if (this.Ichiran.CurrentCell.ColumnIndex == 12)
            {
                curCellCd = this.Ichiran.CurrentCell.EditedFormattedValue.ToString();
                if (preCellCd != curCellCd)
                {
                    this.Ichiran.CurrentRow.Cells["SHARYOU_CD"].Value = string.Empty;
                    this.Ichiran.CurrentRow.Cells["SHARYOU_NAME"].Value = string.Empty;
                    this.IchiranLogic.CheckunpanCd(this.Ichiran.Rows[this.Ichiran.CurrentCell.RowIndex]);
                }
            }
        }
        /// <summary>
        /// 運搬 PopupBeforeExecuteMethod
        /// </summary>
        public void UNPAN_PopupBeforeExecuteMethod()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            preCellCd = string.Empty;
            if (this.Ichiran.CurrentCell.ColumnIndex == 12)
            {
                preCellCd = this.Ichiran.CurrentCell.EditedFormattedValue.ToString();
            }
        }
        #endregion

        #region 車輌POP
        /// <summary>
        /// 車輌 PopupAfterExecuteMethod
        /// </summary>
        public void SHARYOU_PopupAfterExecuteMethod()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            if (this.Ichiran.CurrentCell.ColumnIndex == 14)
            {
                curCellCd = this.Ichiran.CurrentCell.EditedFormattedValue.ToString();
                if (preCellCd != curCellCd)
                {
                    this.IchiranLogic.ChecksharyouCd(this.Ichiran.Rows[this.Ichiran.CurrentCell.RowIndex]);
                }
            }
        }
        /// <summary>
        /// 車輌 PopupBeforeExecuteMethod
        /// </summary>
        public void SHARYOU_PopupBeforeExecuteMethod()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            preCellCd = string.Empty;
            if (this.Ichiran.CurrentCell.ColumnIndex == 14)
            {
                preCellCd = this.Ichiran.CurrentCell.EditedFormattedValue.ToString();
            }
        }
        #endregion

        #region 車種POP
        /// <summary>
        /// 車種 PopupAfterExecuteMethod
        /// </summary>
        public void SHASHU_PopupAfterExecuteMethod()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            if (this.Ichiran.CurrentCell.ColumnIndex == 16)
            {
                curCellCd = this.Ichiran.CurrentCell.EditedFormattedValue.ToString();
                if (preCellCd != curCellCd)
                {
                    this.Ichiran.CurrentRow.Cells["SHARYOU_CD"].Value = string.Empty;
                    this.Ichiran.CurrentRow.Cells["SHARYOU_NAME"].Value = string.Empty;
                    this.IchiranLogic.CheckshashuCd(this.Ichiran.Rows[this.Ichiran.CurrentCell.RowIndex]);
                }
            }
        }
        /// <summary>
        /// 車種 PopupBeforeExecuteMethod
        /// </summary>
        public void SHASHU_PopupBeforeExecuteMethod()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            preCellCd = string.Empty;
            if (this.Ichiran.CurrentCell.ColumnIndex == 16)
            {
                preCellCd = this.Ichiran.CurrentCell.EditedFormattedValue.ToString();
            }
        }
        #endregion

        #region 運転者POP
        /// <summary>
        /// 運転者 PopupAfterExecuteMethod
        /// </summary>
        public void UNTENSHA_PopupAfterExecuteMethod()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            if (this.Ichiran.CurrentCell.ColumnIndex == 18)
            {
                curCellCd = this.Ichiran.CurrentCell.EditedFormattedValue.ToString();
                if (preCellCd != curCellCd)
                {
                    this.IchiranLogic.CheckUntenCd(this.Ichiran.Rows[this.Ichiran.CurrentCell.RowIndex]);
                }
            }
        }
        /// <summary>
        /// 運転者 PopupBeforeExecuteMethod
        /// </summary>
        public void UNTENSHA_PopupBeforeExecuteMethod()
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            preCellCd = string.Empty;
            if (this.Ichiran.CurrentCell.ColumnIndex == 18)
            {
                preCellCd = this.Ichiran.CurrentCell.EditedFormattedValue.ToString();
            }
        }
        #endregion

        #region 明細更新前情報保持
        /// <summary>
        /// 更新前の明細項目の保持
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.Ichiran.CurrentRow == null) { return; }
            if (this.Ichiran.RowCount == 0) { return; }

            var row = this.Ichiran.Rows[e.RowIndex];

            //拠点
            this.IchiranLogic.oldKyotenCD = string.Empty;
            if (row.Cells["KYOTEN_CD"] != null && row.Cells["KYOTEN_CD"].Value != DBNull.Value && row.Cells["KYOTEN_CD"].Value != null)
            {
                if (!string.IsNullOrEmpty(row.Cells["KYOTEN_CD"].Value.ToString()))
                {
                    this.IchiranLogic.oldKyotenCD = row.Cells["KYOTEN_CD"].Value.ToString().PadLeft(2, '0').ToUpper();
                }
            }

            //作業日
            this.IchiranLogic.oldSagyouDate = string.Empty;
            if (row.Cells["SAGYOU_DATE"].Value != null)
            {
                this.IchiranLogic.oldSagyouDate = DateTime.Parse(row.Cells["SAGYOU_DATE"].Value.ToString()).ToString("yyyy/MM/dd");
            }

            //運搬業者
            this.IchiranLogic.oldUnpanGyoushaCD = string.Empty;
            if (row.Cells["UNPAN_GYOUSHA_CD"] != null && row.Cells["UNPAN_GYOUSHA_CD"].Value != DBNull.Value && row.Cells["UNPAN_GYOUSHA_CD"].Value != null)
            {
                if (!string.IsNullOrEmpty(row.Cells["UNPAN_GYOUSHA_CD"].Value.ToString()))
                {
                    this.IchiranLogic.oldUnpanGyoushaCD = row.Cells["UNPAN_GYOUSHA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                }
            }

            this.IchiranLogic.oldUnpanGyoushaName = string.Empty;
            if (row.Cells["UNPAN_GYOUSHA_NAME"] != null && row.Cells["UNPAN_GYOUSHA_NAME"].Value != DBNull.Value && row.Cells["UNPAN_GYOUSHA_NAME"].Value != null)
            {
                if (!string.IsNullOrEmpty(row.Cells["UNPAN_GYOUSHA_NAME"].Value.ToString()))
                {
                    this.IchiranLogic.oldUnpanGyoushaName = row.Cells["UNPAN_GYOUSHA_NAME"].Value.ToString();
                }
            }
            
            //車種
            this.IchiranLogic.oldShasyuCD = string.Empty;
            if (row.Cells["SHASHU_CD"] != null && row.Cells["SHASHU_CD"].Value != DBNull.Value && row.Cells["SHASHU_CD"].Value != null)
            {
                if (!string.IsNullOrEmpty(row.Cells["SHASHU_CD"].Value.ToString()))
                {
                    this.IchiranLogic.oldShasyuCD = row.Cells["SHASHU_CD"].Value.ToString().PadLeft(3, '0').ToUpper();
                }
            }

            //車輌
            this.IchiranLogic.oldSharyouCD = string.Empty;
            if (row.Cells["SHARYOU_CD"] != null && row.Cells["SHARYOU_CD"].Value != DBNull.Value && row.Cells["SHARYOU_CD"].Value != null)
            {
                if (!string.IsNullOrEmpty(row.Cells["SHARYOU_CD"].Value.ToString()))
                {
                    this.IchiranLogic.oldSharyouCD = row.Cells["SHARYOU_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                }
            }

            //運転者
            this.IchiranLogic.oldUntenshaCD = string.Empty;
            if (row.Cells["UNTENSHA_CD"] != null && row.Cells["UNTENSHA_CD"].Value != DBNull.Value && row.Cells["UNTENSHA_CD"].Value != null)
            {
                if (!string.IsNullOrEmpty(row.Cells["UNTENSHA_CD"].Value.ToString()))
                {
                    this.IchiranLogic.oldUntenshaCD = row.Cells["UNTENSHA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                }
            }
        }
        #endregion 明細更新前情報保持

        #region 明細更新中処理
        /// <summary>
        /// 明細更新中チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var columnName = this.Ichiran.Columns[e.ColumnIndex].Name;
            var cellValue = Convert.ToString(this.Ichiran[columnName, e.RowIndex].Value);
            var row = this.Ichiran.Rows[e.RowIndex];
            var cell = this.Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex];
            

            switch (columnName)
            {
                //拠点CD
                case "KYOTEN_CD":
                    // 拠点CD取得
                    if (row.Cells["KYOTEN_CD"] != null && row.Cells["KYOTEN_CD"].Value != DBNull.Value && row.Cells["KYOTEN_CD"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(row.Cells["KYOTEN_CD"].Value.ToString()))
                        {
                            row.Cells["KYOTEN_CD"].Value = row.Cells["KYOTEN_CD"].Value.ToString().PadLeft(2, '0').ToUpper();
                        }
                    }
                    else
                    {
                        row.Cells["KYOTEN_NAME"].Value = string.Empty;
                    }
                    // 拠点チェック
                    if (!this.IchiranLogic.ChechiKyotenCd(this.Ichiran.Rows[e.RowIndex]))
                    {
                        e.Cancel = true;
                        this.Ichiran.BeginEdit(false);
                        return;
                    }

                    this.IchiranLogic.isInputError = false;
                    //項目を更新したら[対象]チェックをONにする
                    if (!this.IchiranLogic.oldKyotenCD.Equals(row.Cells["KYOTEN_CD"].Value))
                    {
                        row.Cells["TAISHO_CHECK"].Value = true;
                    }
                    break;
                //作業日
                case "SAGYOU_DATE":
                    //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                    if ((bool)row.Cells["MOBILE_RENKEI"].Value)
                    {
                        if ((row.Cells["SAGYOU_DATE"].Value != null) && (!string.IsNullOrEmpty(row.Cells["SAGYOU_DATE"].Value.ToString())))
                        {
                            if (!(DateTime.Parse(row.Cells["SAGYOU_DATE"].Value.ToString()).ToString("yyyy/MM/dd")).Equals(DateTime.Now.ToString("yyyy/MM/dd")))
                            {
                                this.IchiranLogic.MsgBox.MessageBoxShowInformation("作業日が当日の場合のみモバイル連携が可能です。\r\n明細のモバイル連携のチェックはクリアされます。");
                                row.Cells["MOBILE_RENKEI"].Value = false;
                            }
                        }
                    }

                    if (row.Cells["SAGYOU_DATE"].Value != null)
                    {
                        if (!this.IchiranLogic.oldSagyouDate.Equals(DateTime.Parse(row.Cells["SAGYOU_DATE"].Value.ToString()).ToString("yyyy/MM/dd")))
                        {
                            row.Cells["TAISHO_CHECK"].Value = true;
                        }
                    }
                    else
                    {
                        if (!this.IchiranLogic.oldSagyouDate.Equals(""))
                        {
                            row.Cells["TAISHO_CHECK"].Value = true;
                        }
                    }
                    break;
                //運搬受託者CD
                case "UNPAN_GYOUSHA_CD":
                    // 運搬受託者CD取得
                    if (row.Cells["UNPAN_GYOUSHA_CD"] != null && row.Cells["UNPAN_GYOUSHA_CD"].Value != DBNull.Value && row.Cells["UNPAN_GYOUSHA_CD"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(row.Cells["UNPAN_GYOUSHA_CD"].Value.ToString()))
                        {
                            row.Cells["UNPAN_GYOUSHA_CD"].Value = row.Cells["UNPAN_GYOUSHA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                        }
                    }
                    else
                    {
                        row.Cells["UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                    }

                    if ((this.IchiranLogic.isInputError) || (!this.IchiranLogic.oldUnpanGyoushaCD.Equals(row.Cells["UNPAN_GYOUSHA_CD"].Value)))
                    {
                        // 運搬受託者チェック
                        if (!this.IchiranLogic.CheckunpanCd(this.Ichiran.Rows[e.RowIndex]))
                        {
                            e.Cancel = true;
                            this.Ichiran.BeginEdit(false);
                            return;
                        }
                        else
                        {
                            this.IchiranLogic.isInputError = false;

                            //項目を更新したら[対象]チェックをONにする
                            row.Cells["TAISHO_CHECK"].Value = true;
                        }
                    }
                    break;
                    //運搬業者名
                case "UNPAN_GYOUSHA_NAME":
                    if  (!this.IchiranLogic.oldUnpanGyoushaName.Equals(row.Cells["UNPAN_GYOUSHA_NAME"].Value))
                    {
                        //項目を更新したら[対象]チェックをONにする
                        row.Cells["TAISHO_CHECK"].Value = true;
                    }
                    break;
                //車種CD
                case "SHASHU_CD":
                    // 車種CD取得
                    if (row.Cells["SHASHU_CD"] != null && row.Cells["SHASHU_CD"].Value != DBNull.Value && row.Cells["SHASHU_CD"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(row.Cells["SHASHU_CD"].Value.ToString()))
                        {
                            row.Cells["SHASHU_CD"].Value = row.Cells["SHASHU_CD"].Value.ToString().PadLeft(3, '0').ToUpper();
                        }
                    }
                    else
                    {
                        row.Cells["SHASHU_NAME"].Value = string.Empty;
                    }

                    if ((this.IchiranLogic.isInputError) || (!this.IchiranLogic.oldShasyuCD.Equals(row.Cells["SHASHU_CD"].Value)))
                    {
                        // 車種チェック
                        if (!this.IchiranLogic.CheckshashuCd(this.Ichiran.Rows[e.RowIndex]))
                        {
                            e.Cancel = true;
                            this.Ichiran.BeginEdit(false);
                            return;
                        }
                        else
                        {
                            this.IchiranLogic.isInputError = false;

                            //項目を更新したら[対象]チェックをONにする
                            row.Cells["TAISHO_CHECK"].Value = true;
                        }
                    }
                    break;
                //車輌CD
                case "SHARYOU_CD":
                    // 車輌CD取得
                    if (row.Cells["SHARYOU_CD"] != null && row.Cells["SHARYOU_CD"].Value != DBNull.Value && row.Cells["SHARYOU_CD"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(row.Cells["SHARYOU_CD"].Value.ToString()))
                        {
                            row.Cells["SHARYOU_CD"].Value = row.Cells["SHARYOU_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                        }
                    }
                    else
                    {
                        row.Cells["SHARYOU_NAME"].Value = string.Empty;
                    }

                    if ((this.IchiranLogic.isInputError) || (!this.IchiranLogic.oldSharyouCD.Equals(row.Cells["SHARYOU_CD"].Value)))
                    {
                        // 車輌チェック
                        if (!this.IchiranLogic.ChecksharyouCd(this.Ichiran.Rows[e.RowIndex]))
                        {
                            e.Cancel = true;
                            this.Ichiran.BeginEdit(false);
                            return;
                        }
                        else
                        {
                            this.IchiranLogic.isInputError = false;
                            //項目を更新したら[対象]チェックをONにする
                            row.Cells["TAISHO_CHECK"].Value = true;
                        }
                    }
                    break;
                //運転者CD
                case "UNTENSHA_CD":
                    // 運転者CD取得
                    if (row.Cells["UNTENSHA_CD"] != null && row.Cells["UNTENSHA_CD"].Value != DBNull.Value && row.Cells["UNTENSHA_CD"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(row.Cells["UNTENSHA_CD"].Value.ToString()))
                        {
                            row.Cells["UNTENSHA_CD"].Value = row.Cells["UNTENSHA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                        }
                    }
                    else
                    {
                        row.Cells["UNTENSHA_NAME"].Value = string.Empty;
                    }
                    if ((this.IchiranLogic.isInputError) || (!this.IchiranLogic.oldUntenshaCD.Equals(row.Cells["UNTENSHA_CD"].Value)))
                    {
                        // 運転者チェック
                        if (!this.IchiranLogic.CheckUntenCd(this.Ichiran.Rows[e.RowIndex]))
                        {
                            e.Cancel = true;
                            this.Ichiran.BeginEdit(false);
                            return;
                        }
                        else
                        {
                            this.IchiranLogic.isInputError = false;
                            //項目を更新したら[対象]チェックをONにする
                            row.Cells["TAISHO_CHECK"].Value = true;
                        }
                    }
                    break;
            }
        }
        #endregion 明細更新中処理

        /// <summary>
        /// 明細のIME設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string HeaderText =
             this.Ichiran.Columns[this.Ichiran.CurrentCell.ColumnIndex].HeaderText.ToString();
            if ("運搬業者名".Equals(HeaderText))
            {
                if (this.Ichiran.ImeMode != ImeMode.Hiragana || this.Ichiran.ImeMode != ImeMode.Katakana)
                {
                    this.Ichiran.ImeMode = ImeMode.Hiragana;
                }
            }
            else
            {
                this.Ichiran.ImeMode = ImeMode.Alpha;
                this.Ichiran.ImeMode = ImeMode.Disable;
            }
        }

 
    }
}
