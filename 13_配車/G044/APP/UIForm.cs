using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using System.Drawing;

namespace Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei
{

    /// <summary>
    /// 定期配車一括作成画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {

        #region プロパティ
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }
        #endregion
        
        #region フィールド
        /// <summary>
        /// Close処理の後に実行するメソッド
        /// 制約：戻り値なし、引数なし、Publicなメソッド
        /// </summary>
        public delegate void LastRunMethod();
        
        /// <summary>
        /// UIForm
        /// </summary>
        private UIForm form;

        /// <summary>
        /// UIHeader
        /// </summary>
        UIHeader header_new;

        /// <summary>
        /// 定期配車一括作成画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 検索実行中フラグ
        /// </summary>
        private bool isLoading = false;
        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion
        
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_TEIKI_HAISHA_IKKATSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
        }
        #endregion
        
        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);
            this.logic.WindowInit();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.DetailIchiran != null)
            {
                int GRID_HEIGHT_MIN_VALUE = 347;
                int GRID_WIDTH_MIN_VALUE = 724;
                int h = this.Height - 92;
                int w = this.Width;

                if (h < GRID_HEIGHT_MIN_VALUE)
                {
                    this.DetailIchiran.Height = GRID_HEIGHT_MIN_VALUE;
                }
                else
                {
                    this.DetailIchiran.Height = h;
                }

                if (w < GRID_WIDTH_MIN_VALUE)
                {
                    this.DetailIchiran.Width = GRID_WIDTH_MIN_VALUE;
                }
                else
                {
                    this.DetailIchiran.Width = w;
                }

                if (this.DetailIchiran.Height <= GRID_HEIGHT_MIN_VALUE
                    || this.DetailIchiran.Width <= GRID_WIDTH_MIN_VALUE)
                {
                    this.DetailIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                }
                else
                {
                    this.DetailIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F8 検索処理
        /// <summary>
        /// F8 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            if (this.SAGYOU_DATE_FROM.Text.Trim().Equals(string.Empty) 
                || this.SAGYOU_DATE_TO.Text.Trim().Equals(string.Empty) 
                || string.IsNullOrEmpty(this.txt_KyotenCD.Text))
            {
                return;
            }

            isLoading = true;

            // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
            if (this.logic.CheckDate())
            {
                isLoading = false;
                return;
            }
            // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

            int count = this.logic.Search();

            // 一度でも検索ボタンを押されたら明細欄を触れるようにする
            this.DetailIchiran.Enabled = true;

            // 検索したデータが0件場合
            if (count == 0) 
            {
                //前の結果をクリア
                this.DetailIchiran.Rows.Clear();
                isLoading = false;
                return;
            }
            else if (count == -1)
            {
                isLoading = false;
                return;
            }
            this.logic.SetIchiran(count);

            isLoading = false;
        }
        #endregion
                         
        #region F9 登録処理
        /// <summary>
        /// [F9]登録イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool isErr = this.logic.setRegistCheck();
            if (isErr) { return; }

            // 必須チェックの項目を設定
            var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
            // 必須チェックを実行する
            base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (!base.RegistErrorFlag)
            {

                // 明細行が空行１行存在判断
                if (isNewRow())
                {
                    // アラートを表示し処理を中断
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E067");
                    return;
                }

                // 明細部の登録データ存在判断
                if (!isUpdateChecked())
                {
                    // アラートを表示し処理を中断
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E061");
                    return;
                }

                // コースCD存在チェック
                if (!this.logic.courseCheck())
                {
                    return;
                }

                // 適用期間外のみのコースCDかチェック
                if (!this.logic.courseKikanCheck())
                {
                    return;
                }

                // 20141015 koukouei 休動管理機能 start
                // 休動チェック
                if (!this.logic.ChkWordClose())
                {
                    return;
                }
                // 20141015 koukouei 休動管理機能 end

                // 連携チェック
                for (int i = 0; i < this.DetailIchiran.Rows.Count - 1; i++)
                {
                    DataGridViewRow detailRow = this.DetailIchiran.Rows[i];

                    //判定の前処理
                    Boolean updateFlg = false;
                    if (detailRow.Cells[ConstCls.DetailColName.TOUROKU_FLG].Value != null)
                    {
                        updateFlg = bool.Parse(detailRow.Cells[ConstCls.DetailColName.TOUROKU_FLG].Value.ToString());
                    }

                    if (updateFlg)
                    {
                        if (detailRow.Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value != null && !string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value.ToString()))
                        {
                            if (!this.logic.RenkeiCheck(detailRow.Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value.ToString(), true))
                            {
                                return;
                            }
                        }
                    }
                }


                // 登録用データの作成
                if (!this.logic.CreateEntity(this.OUTPUT_KBN_VALUE.Text))
                {
                    return;
                }

                // ※新規追加
                if (this.logic.RegistData())
                {
                    // メッセージ通知
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");
                    // [F8]検索ボタンをクリック
                    this.logic.parentForm.bt_func8.PerformClick();
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        private Control[] GetAllControl()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);
            allControl.AddRange(controlUtil.GetAllControls(this.logic.parentForm));

            return allControl.ToArray();
        }
        #endregion

        #region F12 Formクローズ処理
        /// <summary>
        /// F12 Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.Parent;

                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormClose", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 明細部の登録データ存在判断
        /// <summary>
        /// 明細部の登録データ存在判断
        /// </summary>
        /// <returns>true: 明細部の登録データ存在する, false: 明細部の登録データ存在しない</returns>
        private bool isUpdateChecked()
        {
            bool returnVal = false;

            foreach (DataGridViewRow detailRow in this.DetailIchiran.Rows)
            {
                if (detailRow == null) continue;
                if (detailRow.IsNewRow) continue;

                // 明細部の登録データ存在する場合、処理中断
                if (detailRow.Cells[ConstCls.DetailColName.TOUROKU_FLG].Value != null
                    && bool.Parse(detailRow.Cells[ConstCls.DetailColName.TOUROKU_FLG].Value.ToString()))
                {
                    returnVal = true;
                    break;
                }
            }

            return returnVal;
        }
        #endregion

        #region 明細行が空行１行存在判断
        /// <summary>
        /// 明細行が空行１行存在判断
        /// </summary>
        /// <returns>true: 明細行が空行１行存在する, false: 明細行が空行１行存在しない</returns>
        private bool isNewRow()
        {
            bool returnVal = false;
            //  明細行が空行１行存在する
            if (this.DetailIchiran.Rows[0].IsNewRow)
            {
                returnVal = true;
            }

            return returnVal;
        }
        #endregion

        #region 抽出設定 LostFocus処理
        /// <summary>
        /// 抽出設定 LostFocus処理
        /// </summary>
        private void OUTPUT_KBN_VALUE_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.OUTPUT_KBN_VALUE.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E034", ConstCls.Chushutsu);
                this.OUTPUT_KBN_VALUE.Focus();
            }
        }
        #endregion

        #region CellValidating時の処理
        /// <summary>
        /// CellValidating時の処理
        /// </summary>
        private void DetailIchiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (isLoading)
            {
                // 検索時、一覧クリアをする前にCellValidatingが実行されエラーとなるため
                return;
            }

            // 連携場合キャンセルする
            if (this.DetailIchiran.Rows[e.RowIndex].Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value != null && !string.IsNullOrEmpty(this.DetailIchiran.Rows[e.RowIndex].Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value.ToString()))
            {
                if (!this.logic.RenkeiCheck(this.DetailIchiran.Rows[e.RowIndex].Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value.ToString(), false))
                {
                    return;
                }
            }

            switch(this.DetailIchiran.Columns[e.ColumnIndex].Name)
            {
                case "SAGYOU_DATE":
                    // 作業日チェック処理
                    if (!this.logic.sagyouDateValidatedProc(e))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "COURSE_NAME_CD":
                    // コース名、拠点を取得処理
                    bool flg = this.logic.getCourseName(e.RowIndex);
                    this.DetailIchiran.Rows[e.RowIndex].Cells["DAY_NM"].Value = string.Empty;
                    if(!flg)
                    {
                        e.Cancel = true;
                    }
                    break;
                case "SHARYOU_CD":
                    // 車輌チェック処理
                    if (!this.logic.CheckSharyouCd(e))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "SHASHU_CD":
                    // 車種チェック処理
                    if (!this.logic.CheckShashuCd(e))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "UNTENSHA_CD":
                    // 運転者チェック処理
                    if (!this.logic.untenshaCDValidatedProc(e))
                    {
                        e.Cancel = true;
                    }
                    break;
                // 20141015 koukouei 休動管理機能 start
                case "HOJOIN_CD":
                    // 補助員チェック処理
                    if (!this.logic.hojoinCDValidatedProc(e))
                    {
                        e.Cancel = true;
                    }
                    break;
                // 20141015 koukouei 休動管理機能 end
                case "UNPAN_GYOUSHA_CD":
                    // 運搬業者チェック処理
                    if (!this.logic.unpanGyoushaCDValidatedProc(e))
                    {
                        e.Cancel = true;
                    }
                    break;
                default:
                    // DO NOTHING
                    break;
            }
        }
        #endregion

        #region ポップアップ画面閉じた後、コース名、拠点を取得処理
        /// <summary>
        /// コース名、拠点を取得処理
        /// </summary>
        public void getPopupAfterCd()
        {
            int selectedIndex = this.DetailIchiran.CurrentRow.Index;

            string dayNm = Convert.ToString(this.DetailIchiran.Rows[selectedIndex].Cells["DAY_NM"].Value);
            switch (dayNm)
            {
                case "月":
                    this.DetailIchiran.Rows[selectedIndex].Cells["DAY_CD"].Value = "1";
                    break;
                case "火":
                    this.DetailIchiran.Rows[selectedIndex].Cells["DAY_CD"].Value = "2";
                    break;
                case "水":
                    this.DetailIchiran.Rows[selectedIndex].Cells["DAY_CD"].Value = "3";
                    break;
                case "木":
                    this.DetailIchiran.Rows[selectedIndex].Cells["DAY_CD"].Value = "4";
                    break;
                case "金":
                    this.DetailIchiran.Rows[selectedIndex].Cells["DAY_CD"].Value = "5";
                    break;
                case "土":
                    this.DetailIchiran.Rows[selectedIndex].Cells["DAY_CD"].Value = "6";
                    break;
                case "日":
                    this.DetailIchiran.Rows[selectedIndex].Cells["DAY_CD"].Value = "7";
                    break;
            }
            // コース名、拠点を取得処理
            this.logic.getCourseName(selectedIndex);
        }
        #endregion

        #region DataGridView CellContentClickイベント
        /// <summary>
        /// CellContentClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (this.DetailIchiran.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
               
            // 「詳細」ボタン押下する時、回収品名詳細ポップアップを表示する
            if (this.DetailIchiran.Columns[ConstCls.DetailColName.SHOUSAI].Index == e.ColumnIndex)
            {
                this.logic.setRegistSyousaiCheck(this.DetailIchiran.Rows[e.RowIndex]);
                    // 必須チェックの項目を設定
                var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
                // 必須チェックを実行する
                base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                if (!base.RegistErrorFlag)
                {
                    this.logic.ShowShousai(WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.DetailIchiran.Rows[e.RowIndex]);
                }
            }
        }
        #endregion

        #region コース名称 ポップアップデータ取得処理
        /// <summary>
        /// コース名称 ポップアップデータ取得処理
        /// </summary>
        public DataTable CourseNamePopUpDataInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var cell = this.DetailIchiran.CurrentRow.Cells["COURSE_NAME_CD"] as ICustomDataGridControl;
                var sagyouDate = this.DetailIchiran.CurrentRow.Cells["SAGYOU_DATE"].Value;
                var teikiHaishaNumber = this.DetailIchiran.CurrentRow.Cells["TEIKI_HAISHA_NUMBER"].Value;

                // ｺｰｽ情報 ポップアップ取得
                // 表示用データ取得＆加工
                cell.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU,DAY_NM";
                var CourseNameDataTable = this.logic.GetPopUpData(cell.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()), sagyouDate, teikiHaishaNumber);
                return CourseNameDataTable;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CourseNamePopUpDataInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// DetailIchiranCellEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // DetailIchiranCellEnter処理
            this.logic.detailCellEnter(sender, e);
        }

        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
        private void SAGYOU_DATE_FROM_Leave(object sender, EventArgs e)
        {
            this.SAGYOU_DATE_TO.IsInputErrorOccured = false;
            this.SAGYOU_DATE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void SAGYOU_DATE_TO_Leave(object sender, EventArgs e)
        {
            this.SAGYOU_DATE_FROM.IsInputErrorOccured = false;
            this.SAGYOU_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
        }

        /// <summary>
        /// detailBeginEdit処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // detailBeginEdit処理
            // カレント行のセット
            var row = this.DetailIchiran.Rows[e.RowIndex];
            var col = this.DetailIchiran.Columns[e.ColumnIndex];

            // 「コースCD」項目が選択された場合
            if (col.Name == ConstCls.DetailColName.COURSE_NAME_CD)
            {
                if (row.Cells[ConstCls.DetailColName.SAGYOU_DATE].Value == null)
                {
                    // 「作業日」項目が空白だった場合はエラー表示を行う
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E034", "作業日");

                    // 修正処理を取り消し
                    e.Cancel = true;

                    BeginInvoke(new DelegateCustomForcus(this.MoveSagyouDateCell));
                }
            }
        }

        private delegate void DelegateCustomForcus();

        /// <summary>
        /// 現在のRowの作業日Cellにフォーカスを当てる
        /// </summary>
        private void MoveSagyouDateCell()
        {
            DataGridViewCell cell = this.DetailIchiran.CurrentCell;
            this.DetailIchiran.CurrentCell = this.DetailIchiran.Rows[cell.RowIndex].Cells[ConstCls.DetailColName.SAGYOU_DATE];
        }

        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

        /// <summary>
        /// DataGridViewCellClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                if(this.DetailIchiran.RowCount > 0)
                {
                    if(this.DetailIchiran.Columns[e.ColumnIndex].Name == "TOUROKU_FLG")
                    {
                        // 全選択チェックボックスが押下された場合、チェックボックス状態を反転する
                        this.TOUROKU_FLG_CHECK_ALL.Checked = !this.TOUROKU_FLG_CHECK_ALL.Checked;

                        // チェックボックスの全ての状態を書き換え
                        foreach(DataGridViewRow row in this.DetailIchiran.Rows)
                        {
                            if (!row.IsNewRow && !row.ReadOnly)
                            {
                                // 連携場合false
                                if (row.Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value != null && !string.IsNullOrEmpty(row.Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value.ToString()))
                                {
                                    if (!this.logic.RenkeiCheck(row.Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value.ToString(), false))
                                    {
                                        row.Cells["TOUROKU_FLG"].Value = false;
                                        continue;
                                    }
                                }

                                // 新規行以外のチェックボックスを更新
                                row.Cells["TOUROKU_FLG"].Value = this.TOUROKU_FLG_CHECK_ALL.Checked;
                            }
                        }

                        // 再描画
                        var parent = (BusinessBaseForm)this.Parent;
                        parent.txb_process.Focus();
                        this.DetailIchiran.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// セル描画イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
            if(e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                using(Bitmap bmp = new Bitmap(this.TOUROKU_FLG_CHECK_ALL.Width, this.TOUROKU_FLG_CHECK_ALL.Height))
                {
                    // チェックボックスの描画領域を確保
                    using(Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.Transparent);
                    }

                    // Bitmapに描画
                    this.TOUROKU_FLG_CHECK_ALL.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                    // 描画位置設定
                    int rightMargin = 4;
                    int x = (e.CellBounds.Width - this.TOUROKU_FLG_CHECK_ALL.Width) - rightMargin;
                    int y = ((e.CellBounds.Height - this.TOUROKU_FLG_CHECK_ALL.Height) / 2);

                    // DataGridViewの現在描画中のセルに描画
                    Point pt = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);
                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Graphics.DrawImage(bmp, pt);
                    e.Handled = true;
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


            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }
    }
}
