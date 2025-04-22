using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;

namespace Shougun.Core.Allocation.CarTransferTeiki
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// ロジック
        /// </summary>
        private CarTransferTeiki.LogicClass IchiranLogic;

        ///// <summary>
        ///// 画面情報初期化フラグ
        ///// </summary>
        //private Boolean isLoaded;

        #endregion

        public UIForm()
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.IchiranLogic = new LogicClass(this);
        }

        public UIForm(string haishaDenpyouNo)
            : base(WINDOW_ID.T_MOBILE_TASHA_TEIKI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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

        #region 一括処理
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Lump_Click(object sender, EventArgs e)
        {
            if (this.Ichiran.Rows.Count == 0)
            {
                return;
            }

            int TaishoCount = 0;
            foreach (DataGridViewRow row in this.Ichiran.Rows)
            {
                if (bool.Parse(row.Cells["TAISHO_CHECK"].Value.ToString()))
                {
                    TaishoCount = 1;
                }
            }

            if (TaishoCount == 0)
            {
                this.IchiranLogic.MsgBox.MessageBoxShowError("対象データが選択されていません。");
                return;
            }
            if (string.IsNullOrEmpty(this.txtHaishaNo.Text))
            {
                this.IchiranLogic.MsgBox.MessageBoxShowError("配車番号が指定されていません。");
                return;
            }
            if (string.IsNullOrEmpty(this.txtCourseCd.Text))
            {
                this.IchiranLogic.MsgBox.MessageBoxShowError("コースCDが指定されていません。");
                return;
            }        
            
            DialogResult dialogResult = MessageBox.Show("対象データに対して、配車番号とコースCDを入力します。\r\nよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            foreach (DataGridViewRow row in this.Ichiran.Rows)
            {
                if (bool.Parse(row.Cells["TAISHO_CHECK"].Value.ToString()))
                {
                    row.Cells["HURI_HAISHA_NUMBER"].Value = this.txtHaishaNo.Text;
                    row.Cells["HURI_COURSE_CD"].Value = this.txtCourseCd.Text;
                    row.Cells["HURI_COURSE_NAME"].Value = this.txtCourseNm.Text;
                    row.Cells["HURI_SAGYOU_DATE"].Value = this.txtSagyouDate.Text;
                }
            }
        }
        /// <summary>
        /// 配車番号検索POP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHaishaNo_KeyDown(object sender, KeyEventArgs e)
        {
            this.IchiranLogic.CheckPopup(e, "1");
        }

        /// <summary>
        /// 配車番号の変更前情報を保持
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHaishaNo_Enter(object sender, EventArgs e)
        {
            var haishaNo = this.txtHaishaNo.Text;
            if (haishaNo != null)
            {
                this.IchiranLogic.beforeHeadHaishaNumber = haishaNo.ToString();
            }
        }

        /// <summary>
        /// 配車番号が、変更前と異なる場合は、データ取得に行く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHaishaNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                var haishaNo = this.txtHaishaNo.Text;

                if (this.IchiranLogic.isInputError || haishaNo != this.IchiranLogic.beforeHeadHaishaNumber)
                {
                    this.IchiranLogic.txtHaishaNo_Validating(e);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// コース検索POP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCourseCd_KeyDown(object sender, KeyEventArgs e)
        {
            this.IchiranLogic.CheckPopup(e, "2");
        }

        /// <summary>
        /// コースCDの変更前情報を保持
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCourseCd_Enter(object sender, EventArgs e)
        {
            var courseNo = this.txtCourseCd.Text;
            if (courseNo != null)
            {
                this.IchiranLogic.beforeHeadCd = courseNo.ToString();
            }
        }

        /// <summary>
        /// コースマスタの存在チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCourseCd_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                var courseNo = this.txtCourseCd.Text;

                if (this.IchiranLogic.isInputError || courseNo != this.IchiranLogic.beforeHeadCd)
                {
                    this.IchiranLogic.txtCourseCd_Validating(e);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

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
            if (e.ColumnIndex == 5)
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
            if (e.ColumnIndex == 5 && e.RowIndex == -1)
            {
                checkBoxAll2.Checked = !checkBoxAll2.Checked;
                this.Ichiran.Refresh();
            }

            //[モバイル連携]をクリック
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
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
                if (!(bool)this.Ichiran[5, e.RowIndex].Value)
                {
                    //配車番号が未入力→[ﾓﾊﾞｲﾙ連携]OFF
                    if ((this.Ichiran[int.Parse(this.Ichiran.Columns["HURI_HAISHA_NUMBER"].Index.ToString()), e.RowIndex].Value == null)
                        || (this.Ichiran[int.Parse(this.Ichiran.Columns["HURI_HAISHA_NUMBER"].Index.ToString()), e.RowIndex].Value.ToString() == ""))
                    {
                        //this.IchiranLogic.SpaceChk(TRUE)⇒Value(FALSE)で戻す
                        //this.IchiranLogic.SpaceChk(FALSE)⇒Value(TRUE)で戻す
                        this.Ichiran[5, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                        this.IchiranLogic.SpaceChk = false;
                        this.Ichiran.Refresh();
                        this.IchiranLogic.MsgBox.MessageBoxShowError("振替先配車番号が指定されていない為、連携できません。");
                        return;

                    }

                    //チェック条件
                    string CheckHuriTeikiNumber = this.Ichiran[int.Parse(this.Ichiran.Columns["HURI_HAISHA_NUMBER"].Index.ToString()), e.RowIndex].Value.ToString();

                    //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                    if (string.IsNullOrEmpty(this.Ichiran[int.Parse(this.Ichiran.Columns["HURI_SAGYOU_DATE"].Index.ToString()), e.RowIndex].Value.ToString()))
                    {
                        this.Ichiran[5, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                        this.IchiranLogic.SpaceChk = false;
                        this.Ichiran.Refresh();
                        this.IchiranLogic.MsgBox.MessageBoxShowError("振替先コースの作業日が当日の場合のみ、モバイル連携が可能です。");
                        return;
                    }
                    else
                    {
                        if (!(DateTime.Parse(this.Ichiran[int.Parse(this.Ichiran.Columns["HURI_SAGYOU_DATE"].Index.ToString()), e.RowIndex].Value.ToString()).ToString("yyyy/MM/dd").Equals(DateTime.Now.ToString("yyyy/MM/dd"))))
                        {
                            this.Ichiran[5, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                            this.IchiranLogic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.IchiranLogic.MsgBox.MessageBoxShowError("振替先コースの作業日が当日の場合のみ、モバイル連携が可能です。");
                            return;
                        }
                    }

                    //定期配車伝票が、モバイル状況一覧に表示される条件か
                    //UNTENSHA_CD、SHARYOU_CD、SHASHU_CD、SHASHU_NAME_RYAKUのしずれかが無し→×。
                    if (this.IchiranLogic.RenkeiCheck(6, CheckHuriTeikiNumber))
                    {
                        this.Ichiran[5, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                        this.IchiranLogic.SpaceChk = false;
                        this.Ichiran.Refresh();
                        this.IchiranLogic.MsgBox.MessageBoxShowError("モバイル将軍へ連携する条件になっていません。");
                        return;
                    }
                    //★振替先の定期配車の業者の取引先有無のが無じゃないかチェックを入れる
                    if (this.IchiranLogic.RenkeiCheck(7, CheckHuriTeikiNumber))
                    {
                        this.Ichiran[5, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                        this.IchiranLogic.SpaceChk = false;
                        this.Ichiran.Refresh();
                        this.IchiranLogic.MsgBox.MessageBoxShowError("モバイル将軍へ連携する条件になっていません。");
                        return;
                    }

                    //配車番号が、ロジコン連携されているか
                    if (this.IchiranLogic.RenkeiCheck(3, CheckHuriTeikiNumber))
                    {
                        this.Ichiran[5, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                        this.IchiranLogic.SpaceChk = false;
                        this.Ichiran.Refresh();
                        this.IchiranLogic.MsgBox.MessageBoxShowError("ロジこんぱす連携中の為、変更する事は出来ません。");
                        return;
                    }
                    //配車番号が、NAVITIME連携されているか
                    if (this.IchiranLogic.RenkeiCheck(4, CheckHuriTeikiNumber))
                    {
                        this.Ichiran[5, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                        this.IchiranLogic.SpaceChk = false;
                        this.Ichiran.Refresh();
                        this.IchiranLogic.MsgBox.MessageBoxShowError("NAVITIME連携中の為、変更する事は出来ません。");
                        return;
                    }

                    //★業者の取引先有無のが無じゃないかチェックを入れる
                    if (this.IchiranLogic.RenkeiCheck(8, this.Ichiran[int.Parse(this.Ichiran.Columns["GYOUSHA_CD"].Index.ToString()), e.RowIndex].Value.ToString()))
                    {
                        this.Ichiran[5, e.RowIndex].Value = !(bool)this.IchiranLogic.SpaceChk;
                        this.IchiranLogic.SpaceChk = false;
                        this.Ichiran.Refresh();
                        this.IchiranLogic.MsgBox.MessageBoxShowError("モバイル将軍へ連携する条件になっていません。");
                        return;
                    }
                    if (this.IchiranLogic.SpaceChk)
                    {
                        this.Ichiran[5, e.RowIndex].Value = !(bool)this.Ichiran[5, e.RowIndex].Value;
                        this.IchiranLogic.SpaceChk = false;
                    }
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
                if (curCell.ColumnIndex == 5 && curCell.RowIndex >= 0)
                {
                    //[モバイル連携]OFFにする場合は、何もしない。
                    //[モバイル連携]ONにする場合は、一度チェックボックスを反転させておく(チェック処理中に画面上ONになってしまうので)
                    this.IchiranLogic.SpaceChk = true;
                    this.IchiranLogic.SpaceON = false;
                    if (!(bool)this.Ichiran[5, curCell.RowIndex].Value)
                    {
                        this.IchiranLogic.SpaceON = true;
                        this.Ichiran[5, curCell.RowIndex].Value = !(bool)this.Ichiran[5, curCell.RowIndex].Value;
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
            this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[5];
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

                //[ﾓﾊﾞｲﾙ連携]ON
                if (checkBoxAll2.Checked)
                {
                    //配車番号が未入力→[ﾓﾊﾞｲﾙ連携]OFF
                    if ((row.Cells["HURI_HAISHA_NUMBER"].Value == null)
                        || (row.Cells["HURI_HAISHA_NUMBER"].Value == ""))
                    {
                        continue;
                    }
                    string CheckHuriTeikiNumber = row.Cells["HURI_HAISHA_NUMBER"].Value.ToString();

                    //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                    //[システム日付] != [作業日]の場合はチェックをつけない
                    if (string.IsNullOrEmpty(row.Cells["HURI_SAGYOU_DATE"].Value.ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        if (!(DateTime.Parse(row.Cells["HURI_SAGYOU_DATE"].Value.ToString()).ToString("yyyy/MM/dd")).Equals(DateTime.Now.ToString("yyyy/MM/dd")))
                        {
                            continue;
                        }
                    }

                    if (this.IchiranLogic.RenkeiCheck(6, CheckHuriTeikiNumber) || this.IchiranLogic.RenkeiCheck(7, CheckHuriTeikiNumber)
                        || this.IchiranLogic.RenkeiCheck(3, CheckHuriTeikiNumber) || this.IchiranLogic.RenkeiCheck(4, CheckHuriTeikiNumber)
                        || this.IchiranLogic.RenkeiCheck(8, row.Cells["GYOUSHA_CD"].Value.ToString()))
                    {
                        continue;
                    }
                    row.Cells[1].Value = checkBoxAll2.Checked;
                    row.Cells[5].Value = checkBoxAll2.Checked;
                }
                else
                {
                    row.Cells[5].Value = checkBoxAll2.Checked;
                }
            }
            this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[1];
            this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[5];
        }
        #endregion

        /////////////////////
        //明細keyDown
        /////////////////////
        #region 明細更新処理
        /// <summary>
        /// 明細更新前の配車番号・コースCDの取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 配車番号の前回値を取得
                var haishaNo = this.Ichiran.Rows[e.RowIndex].Cells["HURI_HAISHA_NUMBER"].Value;
                if (haishaNo != null)
                {
                    this.IchiranLogic.beforeHaishaNumber = haishaNo.ToString();
                }
                // コースCDの前回値を取得
                var courseNameCd = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["HURI_COURSE_CD"].Value);
                this.IchiranLogic.beforeCd = courseNameCd;
            }
            else
            {
                this.IchiranLogic.beforeHaishaNumber = string.Empty;
                this.IchiranLogic.beforeCd = string.Empty;
            }
        }      
  
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyDown -= this.Ichiran_KeyDown;
            e.Control.KeyDown += this.Ichiran_KeyDown;
        }

        /// <summary>
        /// 一覧編集ボックスキーダウン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_KeyDown(object sender, KeyEventArgs e)
        {
            this.IchiranLogic.CheckPopup(e);
        }

        /// <summary>
        /// 明細更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                this.IchiranLogic.Ichiran_CellValidating(e);
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}
