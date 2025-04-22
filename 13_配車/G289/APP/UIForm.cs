// $Id: UIForm.cs 57278 2015-07-30 09:42:51Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.CustomControl;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku
{
    /// <summary>
    /// 定期実績実績入力画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 定期実績入力画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 現在画面ロード中かどうかフラグ
        /// もう一回Leaveイベントが発生してしまう問題の回避策
        /// </summary>
        private bool nowLoding = false;

        ///// <summary>
        ///// 収集毎画面中データ変更時は品名毎行追加かどうかフラグ
        ///// </summary>
        //private bool mHinnmeiRowbyShushuFlg = false;

        /// <summary>
        /// 収集毎画面と品名毎画面切替フラグ
        /// </summary>
        private bool mHinnmeiShushuKirikaeFlg = true;

        /// <summary>
        /// 収集毎画面と品名毎画面切替チェックFLｇ
        /// </summary>
        private bool mKirikaeCheckFlg = true;

        /// <summary>
        /// 現在画面ロード中かどうかフラグ
        /// もう一回Leaveイベントが発生してしまう問題の回避策
        /// </summary>
        private int mNewRowGYOUSHAGENBA = 0;
        /// <summary>
        /// 変更前の定期配車実績番号
        /// </summary>
        public string bakTeikiHaishaJissekiNumber = String.Empty;
        /// <summary>
        /// 変更前の定期配車番号（配車実績番号変更後処理用）
        /// </summary>
        public string bakTeikiHaishaNumber = string.Empty;
        /// <summary>
        /// 変更前の業者CD(換算情報を取得と設定処理用)
        /// </summary>
        public string bakGyousyaCd = string.Empty;
        /// <summary>
        /// 変更前の現場CD(換算情報を取得と設定処理用)
        /// </summary>
        public string bakGenbaCd = string.Empty;
        /// <summary>
        /// 変更前の品名CD(換算情報を取得と設定処理用)
        /// </summary>
        public string bakHinmeiCd = string.Empty;
        /// <summary>
        /// 変更前の数量(換算情報を取得と設定処理用)
        /// </summary>
        public string bakSuuryou = string.Empty;
        /// <summary>
        /// 変更前の単位CD(換算情報を取得と設定処理用)
        /// </summary>
        public string bakUnitCd = string.Empty;
        /// <summary>
        /// 変更前の回数(換算情報を取得と設定処理用)
        /// </summary>
        public string bakRoundNo = string.Empty;

        //単位処理フラグ
        bool IsCdFlg = false;
        public bool mHaishaFlg = false;

        /// <summary>
        /// 変更前の数量の収集毎(換算情報を取得と設定処理用)
        /// </summary>
        public Dictionary<int, string> bakSuuryouSuusuIchiran = new Dictionary<int, string>();

        /// <summary>
        /// 元の業者CD	現場CD	品名CD　単位CDの値　伝票区分、契約区分、集計単位　設定用
        /// </summary>
        public DTOClass mOrgDedailDtoInfo { get; set; }

        /// <summary>
        /// 変更前のコース名CD (コース変更時処理用)
        /// </summary>
        private string bakCourseNameCd = string.Empty;

        /// <summary>
        /// 換算値を再設定するかを示します
        /// </summary>
        internal bool isKanzanRecalculation = false;

        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 変更前の業者CD(換算情報を取得と設定処理用)
        /// </summary>
        public string bakSSGyousyaCd = string.Empty;
        /// <summary>
        /// 変更前の現場CD(換算情報を取得と設定処理用)
        /// </summary>
        public string bakSSGenbaCd = string.Empty;
        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// Validatingイベント中か判定
        /// </summary>
        internal bool isCellValidating = false;

        /// <summary>
        /// コントロールFocus時値格納
        /// </summary>
        public Dictionary<string, string> dicControl = new Dictionary<string, string>();

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        internal Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        private string preGyoushaCd { get; set; }
        private string curGyoushaCd { get; set; }

        /// <summary>
        /// 伝票発行ポップアップがキャンセルされたかどうか判断するためのフラグ
        /// </summary>
        internal bool bCancelDenpyoPopup = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="paramIn_TeikiJisejiNumber">定期実績番号</param>
        public UIForm(WINDOW_TYPE windowType, String paramIn_TeikiJisejiNumber)
            : base(WINDOW_ID.T_TEIKIHAISHA_ZISSEKI, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, paramIn_TeikiJisejiNumber);

                this.InitializeComponent();

                // 時間コンボボックスのItemsを設定
                this.SHUKKO_HOUR.SetItems();
                this.SHUKKO_MINUTE.SetItems();
                this.KIKO_HOUR.SetItems();
                this.KIKO_MINUTE.SetItems();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);

                this.logic.teikiJisejiNumber = paramIn_TeikiJisejiNumber;
                // 変更前の定期配車実績番号を保存する
                this.bakTeikiHaishaJissekiNumber = paramIn_TeikiJisejiNumber;
                //// 変更前の配車実績番号を保存する
                //this.bakTeikiHaishaNumber = paramIn_TeikiJisejiNumber;
                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);

                this.UNTENSHA_CD.FocusOutCheckMethod.Add(new r_framework.Dto.SelectCheckDto()
                {
                    CheckMethodName = "社員マスタコードチェックandセッティング"
                });
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="paramTeikiHaishaNumber">定期実績番号</param>
        public UIForm(WINDOW_TYPE windowType, String paramTeikiHaishaNumber, bool argHaisha)
            : base(WINDOW_ID.T_TEIKIHAISHA_ZISSEKI, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, paramTeikiHaishaNumber, argHaisha);

                this.InitializeComponent();

                // 時間コンボボックスのItemsを設定
                this.SHUKKO_HOUR.SetItems();
                this.SHUKKO_MINUTE.SetItems();
                this.KIKO_HOUR.SetItems();
                this.KIKO_MINUTE.SetItems();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);

                this.logic.teikiJisejiNumber = string.Empty;
                // 変更前の配車実績番号を保存する
                this.bakTeikiHaishaNumber = paramTeikiHaishaNumber;
                this.mHaishaFlg = argHaisha;
                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                this.logic.WindowInit(WindowType);
                // すべてのコントロールを返す
                this.allControl = this.GetAllControl();

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.TabControl != null)
                {
                    int GRID_HEIGHT_MIN_VALUE = 269;
                    int h = this.Height - 190;

                    if (h < GRID_HEIGHT_MIN_VALUE)
                    {
                        this.TabControl.Height = GRID_HEIGHT_MIN_VALUE;
                    }
                    else
                    {
                        this.TabControl.Height = h;
                    }

                    if (this.TabControl.Height <= GRID_HEIGHT_MIN_VALUE)
                    {
                        this.TabControl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    }
                    else
                    {
                        this.TabControl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    }

                    if (this.Width >= 580)
                    {
                        this.TabControl.Width = this.Width - 12;
                    }
                }

                if (this.DetailIchiran != null)
                {
                    this.DetailIchiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }

                if (this.NioroshiIchiran != null)
                {
                    this.NioroshiIchiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
                }

                if (this.syuusyuuDetailIchiran != null)
                {
                    this.syuusyuuDetailIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                if (this.syuusyuuDetailIchiranSoukei != null)
                {
                    this.syuusyuuDetailIchiranSoukei.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                }
                if (this.syuusyuuDetailIchiranScroll != null)
                {
                    this.syuusyuuDetailIchiranScroll.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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
        #endregion

        #region Functionボタン 押下処理
        /// <summary>
        /// F1【切替】明細「品名毎」/「収集毎」切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetKirikaeFrom(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // ベースフォームオブジェクト取得
                var parentForm = (BusinessBaseForm)this.Parent;

                if (this.TabControl.SelectedIndex == 1)
                {
                    this.TabControl.SelectTab(0);
                }
                else
                {
                    this.TabControl.SelectTab(1);
                }
                this.Refresh();

            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKirikaeFrom", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        ///明細「品名毎」/「収集毎」切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // ベースフォームオブジェクト取得
                var parentForm = (BusinessBaseForm)this.Parent;

                //明細タブ切り替え時の入力チェックエラー発生時
                if (!this.mKirikaeCheckFlg)
                {
                    mKirikaeCheckFlg = true;
                    //エラー発生時
                    return;
                }
                bool catchErr = false;
                if (this.TabControl.SelectedIndex == 1)
                {
                    //「F6 品名追加ボタン」を使用不可にする                   
                    parentForm.bt_func6.Enabled = false;
                    //品名画面のデータ取得
                    this.logic.searchResultDetail = this.logic.DetailIchiranGridToDetailIchiranData(out catchErr);
                    if (catchErr) { return; }
                    if ((this.logic.searchResultDetail != null) && this.logic.searchResultDetail.Rows.Count > 0)
                    {
                        // 収集情報作成
                        this.logic.DetailIchiranDataToShushuIchiranData(this.logic.searchResultDetail);
                        // 収集画面のデータ設定
                        if (!this.logic.InitShushuIchiranGrid()) { return; }
                        // 明細欄（総計）を計算
                        if (!this.logic.SetSoukei()) { return; }

                        // 総計ラベル表示
                        var dgvSoukei = this.syuusyuuDetailIchiranSoukei;
                        dgvSoukei["clmSoukeiLabel", 0].Value = "総計";
                        dgvSoukei["clmSoukeiLabel", 0].Style.BackColor = Color.FromArgb(0, 105, 51);
                        dgvSoukei["clmSoukeiLabel", 0].Style.ForeColor = Color.White;
                    }
                }
                else if (this.TabControl.SelectedIndex == 0 && mHinnmeiShushuKirikaeFlg)
                {
                    if (!base.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                    {
                        //F6 品名追加ボタン」を使用可にする                  
                        parentForm.bt_func6.Enabled = true;
                    }
                    //品名データ：更新した収集画面のデータ取得
                    this.logic.ShushuIchiranGridToDetailIchiranData(out catchErr);
                    if (catchErr) { return; }
                    //収集GRID行列Clear
                    this.logic.ClearShushuIchiranGrid();
                    if (this.logic.searchResultDetail.Rows.Count > 0)
                    {
                        //品名毎情報表示時は収集備考データ更新します。
                        // 品名画面データに設定                      
                        if (!this.logic.InitDetailIchiranGrid(false)) { return; }
                    }
                }

                this.Refresh();

            }
            catch (Exception ex)
            {
                LogUtility.Error("TabControl_SelectedIndexChanged", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F2【新規】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                //品名GRID画面表示
                this.TabControl.SelectTab(0);
                // 処理モード変更
                this.SetWindowType(WINDOW_TYPE.NEW_WINDOW_FLAG);
                // 実績番号
                this.bakTeikiHaishaJissekiNumber = String.Empty;
                this.TEIKI_JISSEKI_NUMBER.Text = string.Empty;
                this.bakTeikiHaishaNumber = string.Empty;
                // 業者CD,現場CD,品名CD,数量,単位CD
                this.bakGyousyaCd = string.Empty;
                this.bakGenbaCd = string.Empty;
                this.bakHinmeiCd = string.Empty;
                this.bakSuuryou = string.Empty;
                this.bakUnitCd = string.Empty;
                this.bakRoundNo = string.Empty;
                // 画面項目初期化【新規】モード
                if (!this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent)) { return; }

                this.TEIKI_JISSEKI_NUMBER.Focus();

                // フラグ初期化
                this.logic.hasGenbaHinmeiAdded = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMode", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F3【修正】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UpdateMode(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    //品名GRID画面表示
                    this.TabControl.SelectTab(0);
                    // 処理モード変更
                    this.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                    // 画面項目初期化【修正】モード
                    if (!this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent)) { return; }
                }
                else if (r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    //品名GRID画面表示
                    this.TabControl.SelectTab(0);
                    // 処理モード変更
                    this.SetWindowType(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                    // 画面項目初期化【参照】モード
                    if (!this.logic.WindowInitReference((BusinessBaseForm)this.Parent)) { return; }
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                }

                // フラグ初期化
                this.logic.hasGenbaHinmeiAdded = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateMode", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F4【按分実行】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void AnbunnJicou(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 作業日に入力が無い場合エラー
                if (this.SAGYOU_DATE.Value == null)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E012", "作業日");
                    this.SAGYOU_DATE.Focus();
                    return;
                }

                // 一覧の単位・換算後単位で共に「kg」を指定された場合はエラー
                if (this.logic.DuplicatedUnitCdKg())
                {
                    return;
                }

                this.logic.SetAnbun();

            }
            catch (Exception ex)
            {
                LogUtility.Error("AnbunnJicou", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// F5 現場追加 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void AddGenba(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                mNewRowGYOUSHAGENBA = mNewRowGYOUSHAGENBA + 1;
                this.DetailIchiran.RowsAdded -= DetailIchiran_RowsAdded;
                if (this.TabControl.SelectedIndex == 0 && mHinnmeiShushuKirikaeFlg)
                {
                    // カーソルCell
                    DataGridViewCell currentCell = this.DetailIchiran.CurrentCell;
                    // 選択行がない場合、何もしない
                    if (currentCell == null)
                    {
                        return;
                    }

                    // カーソル行
                    int currentRowIndex = currentCell.RowIndex;

                    DataGridViewRow currentRow = this.DetailIchiran.Rows[currentRowIndex];
                    // コピー行を作成
                    this.DetailIchiran.BeginEdit(false);

                    // コピー行を追加
                    if (currentRowIndex + 1 == this.DetailIchiran.Rows.Count)
                    {
                        this.DetailIchiran.Rows.Add();

                        DataGridViewRow newRowlast = this.DetailIchiran.Rows[currentRowIndex];
                        newRowlast.Cells["GYOUSHAGENBA"].Value = mNewRowGYOUSHAGENBA.ToString();
                        newRowlast.Cells["ROUND_NO"].ReadOnly = false;
                        newRowlast.Cells["GYOUSHA_CD"].ReadOnly = false;
                        newRowlast.Cells["GENBA_CD"].ReadOnly = false;
                        newRowlast.Cells["HINMEI_CD"].ReadOnly = false;
                        newRowlast.Cells["SUURYOU"].ReadOnly = false;
                        newRowlast.Cells["UNIT_CD"].ReadOnly = false;
                        newRowlast.Cells["KANSAN_SUURYOU"].ReadOnly = false;
                        newRowlast.Cells["KANSAN_UNIT_CD"].ReadOnly = false;
                        newRowlast.Cells["ANBUN_FLG"].ReadOnly = false;
                        newRowlast.Cells["ANBUN_SUURYOU"].ReadOnly = false;
                        newRowlast.Cells["KEIYAKU_KBN"].ReadOnly = false;
                        newRowlast.Cells["TSUKIGIME_KBN"].ReadOnly = true;
                        this.DetailIchiran.CurrentCell = this.DetailIchiran.Rows[currentRowIndex].Cells[currentCell.ColumnIndex];
                        return;
                    }
                    else
                    {
                        this.DetailIchiran.Rows.InsertCopy(currentRowIndex, currentRowIndex + 1);
                    }

                    DataGridViewRow newRow = this.DetailIchiran.Rows[currentRowIndex + 1];

                    for (int colIndex = 0; colIndex < this.DetailIchiran.Columns.Count; colIndex++)
                    {
                        var columnName = this.DetailIchiran.Columns[colIndex].Name;
                        if (columnName.Equals("ROUND_NO") || columnName.Equals("GYOUSHA_CD") || columnName.Equals("GYOUSHA_NAME_RYAKU"))
                        {
                            // [回数]、[業者CD]、[業者名]そのままコピー
                            newRow.Cells[colIndex].Value = currentRow.Cells[colIndex].Value;
                        }
                        else if (columnName.Equals("KAKUTEI_FLG") || columnName.Equals("ANBUN_FLG"))
                        {
                            // [確定]、[実数]、初期値はチェック無し
                            newRow.Cells[colIndex].Value = 0;
                        }
                        else
                        {
                            newRow.Cells[colIndex].Value = string.Empty;
                        }
                    }

                    newRow.Cells["ROUND_NO"].Value = string.Empty;
                    newRow.Cells["ROUND_NO"].ReadOnly = false;
                    newRow.Cells["GYOUSHA_CD"].ReadOnly = false;
                    newRow.Cells["GENBA_CD"].ReadOnly = false;
                    newRow.Cells["HINMEI_CD"].ReadOnly = false;
                    newRow.Cells["SUURYOU"].ReadOnly = false;
                    newRow.Cells["UNIT_CD"].ReadOnly = false;
                    newRow.Cells["KANSAN_SUURYOU"].ReadOnly = false;
                    newRow.Cells["KANSAN_UNIT_CD"].ReadOnly = false;
                    newRow.Cells["ANBUN_FLG"].ReadOnly = false;
                    newRow.Cells["ANBUN_SUURYOU"].ReadOnly = false;
                    newRow.Cells["KEIYAKU_KBN"].ReadOnly = false;
                    newRow.Cells["TSUKIGIME_KBN"].ReadOnly = true;
                    newRow.Cells["GYOUSHAGENBA"].Value = mNewRowGYOUSHAGENBA.ToString();
                    newRow.Cells["INPUT_KBN"].Value = "1";
                    newRow.Cells["INPUT_KBN_NAME"].Value = ConstCls.INPUT_KBN_1;
                    // 元カーソルCellにフォーカス
                    this.DetailIchiran.CurrentCell = this.DetailIchiran.Rows[currentRowIndex + 1].Cells[currentCell.ColumnIndex];
                    this.DetailIchiran.CurrentCell.Selected = true;

                    this.DetailIchiran.EndEdit();

                    this.logic.hasGenbaHinmeiAdded = true;
                }
                else if (this.TabControl.SelectedIndex == 1)
                {
                    // カーソルCell
                    DataGridViewCell currentCell = this.syuusyuuDetailIchiran.CurrentCell;
                    // 選択行がない場合、何もしない
                    if (currentCell == null)
                    {
                        return;
                    }

                    // カーソル行
                    int currentRowIndex = currentCell.RowIndex;

                    DataGridViewRow currentRow = this.syuusyuuDetailIchiran.Rows[currentRowIndex];
                    // コピー行を作成
                    this.syuusyuuDetailIchiran.BeginEdit(false);

                    this.syuusyuuDetailIchiran.Rows.InsertCopy(currentRowIndex, currentRowIndex + 1);

                    DataGridViewRow newRow = this.syuusyuuDetailIchiran.Rows[currentRowIndex + 1];

                    for (int colIndex = 0; colIndex < this.syuusyuuDetailIchiran.Columns.Count; colIndex++)
                    {
                        var columnName = this.syuusyuuDetailIchiran.Columns[colIndex].Name;
                        
                        // 値の設定
                        if (columnName.Equals("clmGYOUSHA_CD") || columnName.Equals("clmGYOUSHA_NAME_RYAKU"))
                        {
                            // [業者CD]、[業者名]そのままコピー
                            newRow.Cells[colIndex].Value = currentRow.Cells[colIndex].Value;
                        }
                        else if (columnName.Equals("clmOk2"))
                        {
                            // [確定] 初期値はチェック無し
                            newRow.Cells[colIndex].Value = 0;
                        }
                        else
                        {
                            newRow.Cells[colIndex].Value = string.Empty;
                        }

                        // ReadOnlyの設定
                        if (!columnName.Equals("clmOk2") && !columnName.Equals("clmROUND_NO")
                            && !columnName.Equals("clmKAISHUU_BIKOU") && !columnName.Equals("clmGYOUSHA_CD")
                            && !columnName.Equals("clmGYOUSHA_NAME_RYAKU") && !columnName.Equals("clmGENBA_CD")
                            && !columnName.Equals("clmGENBA_NAME_RYAKU") && !columnName.Equals("clmSHUUSHUU_HOUR")
                            && !columnName.Equals("clmSHUUSHUU_MIN") && !columnName.Equals("clmDETAIL_SYSTEM_ID")
                            && !columnName.Equals("clmGYOUSHAGENBA"))
                        {
                            // 品名＆単位のカラム部分は読み取り専用
                            // 品名毎タブで値を設定する
                            newRow.Cells[colIndex].ReadOnly = true;
                        }
                    }

                    newRow.Cells["clmGYOUSHAGENBA"].Value = mNewRowGYOUSHAGENBA.ToString();

                    // 元カーソルCellにフォーカス
                    this.syuusyuuDetailIchiran.CurrentCell = this.syuusyuuDetailIchiran.Rows[currentRowIndex].Cells[currentCell.ColumnIndex];
                    this.syuusyuuDetailIchiran.CurrentCell.Selected = true;

                    this.syuusyuuDetailIchiran.EndEdit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("AddGenba", ex);
                throw ex;
            }
            finally
            {
                this.DetailIchiran.RowsAdded -= DetailIchiran_RowsAdded;
                this.DetailIchiran.RowsAdded += DetailIchiran_RowsAdded;
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// F6 品名追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void AddHinnmei(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.DetailIchiran.RowsAdded -= DetailIchiran_RowsAdded;
                //品名毎情報表示
                if (this.TabControl.SelectedIndex == 1)
                {
                    this.TabControl.SelectTab(0);
                    this.DetailIchiran.CurrentCell = this.DetailIchiran.Rows[this.DetailIchiran.Rows.Count - 1].Cells[ConstCls.DetailColName.GYOUSHA_CD];
                }

                // カーソルCell
                DataGridViewCell currentCell = this.DetailIchiran.CurrentCell;
                // 選択行がない場合、何もしない
                if (currentCell == null)
                {
                    return;
                }

                // カーソル行
                int currentRowIndex = currentCell.RowIndex;

                DataGridViewRow currentRow = this.DetailIchiran.Rows[currentRowIndex];
                // コピー行を作成
                this.DetailIchiran.BeginEdit(false);


                // コピー行を追加]
                if (currentRowIndex + 1 == this.DetailIchiran.Rows.Count)
                {
                    this.DetailIchiran.Rows.Add();

                    DataGridViewRow newRowlast = this.DetailIchiran.Rows[currentRowIndex];
                    mNewRowGYOUSHAGENBA = mNewRowGYOUSHAGENBA + 1;
                    newRowlast.Cells["GYOUSHAGENBA"].Value = mNewRowGYOUSHAGENBA.ToString();
                    newRowlast.Cells["ROUND_NO"].ReadOnly = false;
                    newRowlast.Cells["GYOUSHA_CD"].ReadOnly = false;
                    newRowlast.Cells["GENBA_CD"].ReadOnly = false;
                    newRowlast.Cells["HINMEI_CD"].ReadOnly = false;
                    newRowlast.Cells["SUURYOU"].ReadOnly = false;
                    newRowlast.Cells["UNIT_CD"].ReadOnly = false;
                    newRowlast.Cells["KANSAN_SUURYOU"].ReadOnly = false;
                    newRowlast.Cells["KANSAN_UNIT_CD"].ReadOnly = false;
                    newRowlast.Cells["ANBUN_FLG"].ReadOnly = false;
                    newRowlast.Cells["ANBUN_SUURYOU"].ReadOnly = false;
                    newRowlast.Cells["KEIYAKU_KBN"].ReadOnly = false;
                    newRowlast.Cells["TSUKIGIME_KBN"].ReadOnly = true;
                    this.DetailIchiran.CurrentCell = this.DetailIchiran.Rows[currentRowIndex].Cells[currentCell.ColumnIndex];
                    return;
                }
                else
                {

                    this.DetailIchiran.Rows.InsertCopy(currentRowIndex, currentRowIndex + 1);
                }

                DataGridViewRow newRow = this.DetailIchiran.Rows[currentRowIndex + 1];


                for (int colIndex = 0; colIndex < this.DetailIchiran.Columns.Count; colIndex++)
                {
                    var columnName = this.DetailIchiran.Columns[colIndex].Name;
                    if (columnName.Equals("ROUND_NO") || columnName.Equals("GYOUSHA_CD") || columnName.Equals("GYOUSHA_NAME_RYAKU")
                        || columnName.Equals("GENBA_CD") || columnName.Equals("GENBA_NAME_RYAKU"))
                    {
                        // [回数]、[業者CD]、[業者名]、[現場CD]、[現場名]、そのままコピー
                        newRow.Cells[colIndex].Value = currentRow.Cells[colIndex].Value;
                    }
                    else if (columnName.Equals("KAKUTEI_FLG") || columnName.Equals("ANBUN_FLG"))
                    {
                        // [確定]、[実数]、初期値はチェック無し
                        newRow.Cells[colIndex].Value = 0;
                    }
                    else
                    {
                        newRow.Cells[colIndex].Value = string.Empty;
                    }
                }
                mNewRowGYOUSHAGENBA = mNewRowGYOUSHAGENBA + 1;

                newRow.Cells["GYOUSHAGENBA"].Value = mNewRowGYOUSHAGENBA.ToString();
                newRow.Cells["ROUND_NO"].ReadOnly = false;
                newRow.Cells["GYOUSHA_CD"].ReadOnly = false;
                newRow.Cells["GENBA_CD"].ReadOnly = false;
                newRow.Cells["HINMEI_CD"].ReadOnly = false;
                newRow.Cells["SUURYOU"].ReadOnly = false;
                newRow.Cells["UNIT_CD"].ReadOnly = false;
                newRow.Cells["KANSAN_SUURYOU"].ReadOnly = false;
                newRow.Cells["KANSAN_UNIT_CD"].ReadOnly = false;
                newRow.Cells["ANBUN_FLG"].ReadOnly = false;
                newRow.Cells["ANBUN_SUURYOU"].ReadOnly = false;
                newRow.Cells["KEIYAKU_KBN"].ReadOnly = false;
                newRow.Cells["TSUKIGIME_KBN"].ReadOnly = true;

                newRow.Cells["INPUT_KBN"].Value = "1";
                newRow.Cells["INPUT_KBN_NAME"].Value = ConstCls.INPUT_KBN_1;
                // 元カーソルCellにフォーカス
                this.DetailIchiran.CurrentCell = this.DetailIchiran.Rows[currentRowIndex + 1].Cells[currentCell.ColumnIndex];
                this.DetailIchiran.CurrentCell.Selected = true;

                this.DetailIchiran.EndEdit();

                this.logic.hasGenbaHinmeiAdded = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("AddHinnmei", ex);
                throw ex;
            }
            finally
            {
                this.DetailIchiran.RowsAdded -= DetailIchiran_RowsAdded;
                this.DetailIchiran.RowsAdded += DetailIchiran_RowsAdded;
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F7 一覧（定期配車実績一覧画面を表示する）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                FormManager.OpenFormWithAuth("G290", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("reLoad", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F9 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                //品名GRID画面表示
                this.TabControl.SelectTab(0);

                if (!base.RegistErrorFlag)
                {
                    switch (this.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                            // 登録時の入力チェックを行う
                            if (!RegistCheck())
                            {
                                return;
                            }
                            break;

                        default:
                            break;
                    }

                    // 登録用データの作成
                    if (!this.logic.CreateEntity(false, base.WindowType)) { return; }

                    switch (base.WindowType)
                    {
                        // 新規追加
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            this.logic.Regist(base.RegistErrorFlag);
                            if (this.logic.isRegist)
                            {
                                msgLogic.MessageBoxShow("I001", "登録");
                            }
                            else
                            {
                                return;
                            }
                            break;

                        // 更新
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.logic.Update(base.RegistErrorFlag);
                            if (this.logic.isRegist)
                            {
                                msgLogic.MessageBoxShow("I001", "更新");
                            }
                            else
                            {
                                return;
                            }
                            break;

                        // 論理削除
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            this.logic.LogicalDelete();
                            if (this.logic.isRegist)
                            {
                                msgLogic.MessageBoxShow("I001", "削除");
                            }
                            else
                            {
                                return;
                            }
                            break;

                        default:
                            break;
                    }

                    // 権限チェック
                    if (r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        // DB更新後、新規モードで初期化する
                        this.SetWindowType(WINDOW_TYPE.NEW_WINDOW_FLAG);
                        // 実績番号をクリア
                        this.TEIKI_JISSEKI_NUMBER.Text = string.Empty;
                        this.logic.teikiJisejiNumber = string.Empty;
                        this.bakTeikiHaishaNumber = string.Empty;
                        this.bakTeikiHaishaJissekiNumber = string.Empty;
                        // 業者CD,現場CD,品名CD,数量,単位CD
                        this.bakGyousyaCd = string.Empty;
                        this.bakGenbaCd = string.Empty;
                        this.bakHinmeiCd = string.Empty;
                        this.bakSuuryou = string.Empty;
                        this.bakUnitCd = string.Empty;
                        this.bakRoundNo = string.Empty;
                        // 画面項目初期化
                        if (!this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent)) { return; }
                        this.logic.isRegist = false;
                        // フォーカス初期化
                        this.TEIKI_JISSEKI_NUMBER.Focus();
                    }
                    else
                    {
                        // 新規権限が無い場合は画面を閉じる
                        this.FormClose(sender, e);
                    }

                    // フラグ初期化
                    this.logic.hasGenbaHinmeiAdded = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F11 行削除処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void RemoveRow(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.TabControl.SelectedIndex == 0)
                {
                    //品名
                    if ((DataGridViewRow)this.DetailIchiran.CurrentRow != null)
                    {
                        DataGridViewRow selectedRow = (DataGridViewRow)this.DetailIchiran.CurrentRow;
                        if (!selectedRow.IsNewRow)
                        {
                            //行を削除
                            //this.DetailIchiran.Rows.Remove(selectedRow);
                            //障害 #11415 確定済みの行が行削除できてしまう。
                            if (selectedRow.Cells["KAKUTEI_FLG"].Value != null && this.logic.ConvertToBool(selectedRow.Cells["KAKUTEI_FLG"].Value))    
                            {
                                MessageBox.Show("確定済みの行は削除できません。", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                this.DetailIchiran.Rows.Remove(selectedRow);
                            }
                            //障害 #11415 確定済みの行が行削除できてしまう。
                        }
                    }
                }
                else if (this.TabControl.SelectedIndex == 1)
                {
                    //収集
                    if ((DataGridViewRow)this.syuusyuuDetailIchiran.CurrentRow != null)
                    {
                        DataGridViewRow selectedRow = (DataGridViewRow)this.syuusyuuDetailIchiran.CurrentRow;
                        if (!selectedRow.IsNewRow)
                        {
                            //行を削除
                            //this.syuusyuuDetailIchiran.Rows.Remove(selectedRow);
                            //障害 #11415 確定済みの行が行削除できてしまう。
                            if (selectedRow.Cells["clmOk2"].Value != null && this.logic.ConvertToBool(selectedRow.Cells["clmOk2"].Value))
                            {
                                MessageBox.Show("確定済みの行は削除できません。", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                this.syuusyuuDetailIchiran.Rows.Remove(selectedRow);
                            }
                            //障害 #11415 確定済みの行が行削除できてしまう。
                        }
                    }
                }

                // 明細欄(総計）再計算
                this.logic.SetSoukei();

            }
            catch (Exception ex)
            {
                LogUtility.Error("RemoveRow", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 登録時の入力チェックを行う
        /// </summary>
        /// <returns>true: エラーなし, false: エラーあり</returns>
        private bool RegistCheck()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 配車番号必須チェック
                if (string.IsNullOrEmpty(this.TEIKI_JISSEKI_NUMBER.Text) && this.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    // アラートを表示し処理を中断
                    msgLogic.MessageBoxShow("E034", "配車実績番号");
                    this.TEIKI_HAISHA_NUMBER.Focus();
                    return returnVal;
                }

                // 新規場合、配車番号チェック
                if (!string.IsNullOrEmpty(this.TEIKI_HAISHA_NUMBER.Text) && this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    if (!this.logic.checkHaishaNumber())
                    {
                        msgLogic.MessageBoxShow("E022", "入力された定期配車番号");
                        this.TEIKI_HAISHA_NUMBER.Focus();
                        return returnVal;
                    }
                }

                bool catchErr = false;

                // 出庫時間・帰庫時間の入力チェック
                if (!this.logic.CheckSagyouTime())
                {
                    return returnVal;
                }

                // 明細行が空行１行の場合
                if (!this.CheckRequiredDataForDeital(out catchErr) && !catchErr)
                {
                    // アラートを表示し処理を中断                    
                    msgLogic.MessageBoxShow("E061");
                    return returnVal;
                }
                else if (catchErr)
                {
                    return returnVal;
                }

                // 明細部の入力チェックNGの場合
                if (!this.logic.IsInputCheckOK())
                {
                    return returnVal;
                }

                // 20141015 koukouei 休動管理機能 start
                // 休動チェックNGの場合
                if (!this.logic.ChkWordClose())
                {
                    return returnVal;
                }
                // 20141015 koukouei 休動管理機能 end

                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.logic.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

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
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 荷降行削除ボタン処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void DeleteNioroshiRow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (null != this.NioroshiIchiran.CurrentRow && false == this.NioroshiIchiran.CurrentRow.IsNewRow)
            {
                //this.NioroshiIchiran.Rows.Remove(this.NioroshiIchiran.CurrentRow);
                //障害 #11416  定期配車実績入力　確定済みの行で使用している荷降行が変更できてしまう。
                bool catchErr = false;
                string nioroshiNumber = Convert.ToString(this.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value);
                if (this.logic.CheckClmOKDetailIchiran(nioroshiNumber, out catchErr)
                    || ContaninsNioroshiNumberDetaiIchiran(nioroshiNumber, out catchErr))
                {
                    if (!catchErr)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E123");
                    }
                    return;
                }
                else
                {
                    this.NioroshiIchiran.Rows.Remove(this.NioroshiIchiran.CurrentRow);
                }
                //障害 #11416  定期配車実績入力　確定済みの行で使用している荷降行が変更できてしまう。
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 指定された荷降番号が一覧で使用されているか判定
        /// </summary>
        /// <param name="nioroshiNumber">荷降番号</param>
        /// <param name="catchErr"></param>
        /// <returns>true:使用済, false:未使用</returns>
        private bool ContaninsNioroshiNumberDetaiIchiran(string nioroshiNumber, out bool catchErr)
        {
            catchErr = false;
            bool returnVal = false;
            try
            {
                if (string.IsNullOrEmpty(nioroshiNumber))
                {
                    return returnVal;
                }

                foreach (DataGridViewRow row in this.DetailIchiran.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    // 荷降Noの使用有無判定
                    if (Convert.ToString(row.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].Value).Equals(nioroshiNumber))
                    {
                        return returnVal = true;
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ContaninsNioroshiNumberDetaiIchiran", ex);
                this.logic.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
        }

        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        private Control[] GetAllControl()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);
            allControl.AddRange(controlUtil.GetAllControls(this.logic.headForm));
            return allControl.ToArray();
        }
        /// <summary>
        /// Detail-Detail-2部（回収明細部）必須チェック
        /// Datailが一行以上入力されているかチェックする
        /// </summary>
        /// <returns>true: 一件以上入力されている, false: 一件も入力されていない</returns>
        private bool CheckRequiredDataForDeital(out bool catchErr)
        {
            catchErr = false;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();
                
                foreach (DataGridViewRow row in this.DetailIchiran.Rows)
                {
                    if (row == null) continue;
                    if (row.IsNewRow) continue;

                    returnVal = true;
                    break;
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRequiredDataForDeital", ex);
                this.logic.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
        }

        #region 番号「前」
        /// <summary>
        /// 番号「前」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousNumber_Click(object sender, EventArgs e)
        {
            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) &&
                !r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E158", "修正");
                return;
            }

            String previousNumber;
            String tableName = "T_TEIKI_JISSEKI_ENTRY";
            String fieldName = "TEIKI_JISSEKI_NUMBER";
            String Number = this.TEIKI_JISSEKI_NUMBER.Text;
            String kyoten = this.logic.headForm.KYOTEN_CD.Text;
            // 前の番号を取得
            bool catchErr = false;
            previousNumber = this.logic.GetPreviousNumber(tableName, fieldName, Number, kyoten, out catchErr);
            if (catchErr) { return; }
            // 取得できなかった場合、エラー
            if (String.IsNullOrEmpty(previousNumber))
            {
                // アラート表示し、フォーカス移動しない
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E045");
                this.TEIKI_HAISHA_NUMBER.Focus();
                return;
            }

            // 番号を設定
            this.TEIKI_JISSEKI_NUMBER.Text = previousNumber;
            // 番号更新後処理
            TeikiHaishaJisekiNumberValidated(sender, e);

        }
        #endregion

        #region 番号「次」
        /// <summary>
        /// 番号「次」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextNumber_Click(object sender, EventArgs e)
        {
            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) &&
                !r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E158", "修正");
                return;
            }
            String nextNumber;
            String tableName = "T_TEIKI_JISSEKI_ENTRY";
            String fieldName = "TEIKI_JISSEKI_NUMBER";
            String Number = this.TEIKI_JISSEKI_NUMBER.Text;
            String kyoten = this.logic.headForm.KYOTEN_CD.Text;
            // 次の番号を取得
            bool catchErr = false;
            nextNumber = this.logic.GetNextNumber(tableName, fieldName, Number, kyoten, out catchErr);
            if (catchErr) { return; }
            // 取得できなかった場合、エラー
            if (String.IsNullOrEmpty(nextNumber))
            {
                // アラート表示し、フォーカス移動しない
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E045");
                this.TEIKI_HAISHA_NUMBER.Focus();
                return;
            }

            // 番号を設定
            this.TEIKI_JISSEKI_NUMBER.Text = nextNumber;
            // 番号更新後処理
            TeikiHaishaJisekiNumberValidated(sender, e);

        }
        #endregion

        #endregion

        #region 実績番号変更後処理
        /// <summary>
        /// 実績番号変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void TeikiHaishaJisekiNumberValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!nowLoding)
                {
                    nowLoding = true;
                    // ブランクの場合、処理なし
                    if (string.IsNullOrEmpty(this.TEIKI_JISSEKI_NUMBER.Text))
                    {
                        nowLoding = false;
                        return;
                    }

                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) &&
                        !r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                        this.TEIKI_JISSEKI_NUMBER.Focus();
                        return;
                    }

                    bool catchErr = false;
                    this.logic.TeikiHaishaJisekiNumberValidated(out catchErr);
                    if (catchErr) { return; }

                    //品名情報表示
                    this.TabControl.SelectTab(0);
                    if (this.ActiveControl != null)
                    {
                        this.ActiveControl.Focus();
                    }
                    nowLoding = false;

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHaishaJisekiNumberValidated", ex);
                throw ex;
            }
            finally
            {
                nowLoding = false;
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 配車番号変更後処理
        /// <summary>
        /// 配車番号変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void TeikiHaishaNumberValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.TEIKI_HAISHA_NUMBER.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.TEIKI_HAISHA_NUMBER.Text))
                {
                    this.bakTeikiHaishaNumber = "";
                    nowLoding = false;
                    return;
                }

                bool catchErr = false;
                this.logic.TeikiHaishaNumberValidated(out catchErr);
                if (catchErr) { return; }

                mHinnmeiShushuKirikaeFlg = false;
                //品名情報表示
                this.TabControl.SelectTab(0);

                mHinnmeiShushuKirikaeFlg = true;

                if (this.ActiveControl != null)
                {
                    this.ActiveControl.Focus();
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHaishaNumberValidated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region ウインドウタイプ設定処理
        /// <summary>
        /// ウインドウタイプ設定処理
        /// </summary>
        /// <param name="type"></param>
        public void SetWindowType(WINDOW_TYPE type)
        {
            try
            {
                LogUtility.DebugMethodStart(type);

                base.WindowType = type;
                base.OnLoad(new EventArgs());
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowType", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 出庫時メーターと帰庫時メーターチェック
        private void MeterFrom_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.IntegerFromToCheck(this.SHUKKO_METER, this.KIKO_METER);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHUKKO_METER_Validated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        private void MeterTo_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.IntegerFromToCheck(this.SHUKKO_METER, this.KIKO_METER);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHUKKO_METER_Validated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 定期実績荷卸
        /// <summary>
        /// RowsAddedイベント（定期実績荷卸）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NioroshiIchiran_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.NioroshiIchiran.Rows[e.RowIndex].IsNewRow && e.RowIndex > 0)
                {
                    int no = 1;
                    var max = this.NioroshiIchiran.Rows.Cast<DataGridViewRow>().Max(r => r.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value);
                    if (max != null)
                    {
                        no = Int32.Parse(max.ToString()) + 1;
                    }

                    this.NioroshiIchiran.Rows[e.RowIndex - 1].Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value = no;
                    // 単位(kg)
                    this.NioroshiIchiran.Rows[e.RowIndex - 1].Cells["UNIT"].Value = "kg";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("NioroshiIchiran_RowsAdded", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region  F5 現場追加処理
        /// <summary>
        /// 業者CDと現場CDの組み合わせ重複チェック。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void syuusyuuDetailIchiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 該当行が確定済なら、編集不可になっているのでスキップ
                var row = this.syuusyuuDetailIchiran.Rows[e.RowIndex];
                if (!this.logic.IsExecutableCellForsyuusyuuDetail(row))
                {
                    return;
                }

                // 編集列名の取得
                var colName = this.syuusyuuDetailIchiran.Columns[e.ColumnIndex].Name;

                if((colName == "clmROUND_NO") || (colName == "clmGYOUSHA_CD") || (colName == "clmGENBA_CD"))
                {
                    popupAfterExecuteSyuusyuuDetailIchiran();
                }

                // 換算数量
                if (0 <= e.RowIndex && 10 <= e.ColumnIndex)
                {
                    if (!this.logic.SetKansanInfoSyuusyuu(e)) { return; }
                }

                // 明細欄(総計）再計算
                this.logic.SetSoukei();

            }
            catch (Exception ex)
            {
                LogUtility.Error("syuusyuuDetailIchiran_CellValidated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 収集画面業者CDと現場CDの組み合わせ重複チェック
        /// </summary>
        public void popupAfterExecuteSyuusyuuDetailIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();
                int colIndex = this.syuusyuuDetailIchiran.CurrentCell.ColumnIndex;
                int rowIndex = this.syuusyuuDetailIchiran.CurrentCell.RowIndex;
                string srtRoundNo = this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmROUND_NO"].Value == null ? string.Empty : this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmROUND_NO"].Value.ToString();

                string srtGyoshaCd = this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGYOUSHA_CD"].Value == null ? string.Empty : this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGYOUSHA_CD"].Value.ToString();
                string srtGyoshaNm = this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGYOUSHA_NAME_RYAKU"].Value == null ? string.Empty : this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGYOUSHA_NAME_RYAKU"].Value.ToString();
                string srtKAISHUU_BIKOU = this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmKAISHUU_BIKOU"].Value == null ? string.Empty : this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmKAISHUU_BIKOU"].Value.ToString();

                string srtGenbaCd = this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGENBA_CD"].Value == null ? string.Empty : this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGENBA_CD"].Value.ToString();
                string srtGenbaNm = this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGENBA_NAME_RYAKU"].Value == null ? string.Empty : this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGENBA_NAME_RYAKU"].Value.ToString();

                string strGYOUSHAGENBA = this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGYOUSHAGENBA"].Value == null ? string.Empty : this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGYOUSHAGENBA"].Value.ToString();

                foreach (DataGridViewRow grdRow in this.syuusyuuDetailIchiran.Rows)
                {
                    if (!grdRow.Index.Equals(rowIndex)
                        && !string.IsNullOrEmpty(srtRoundNo)
                        && !string.IsNullOrEmpty(srtGyoshaCd)
                        && !string.IsNullOrEmpty(srtGenbaCd)
                        && this.logic.GetCellValue(grdRow.Cells["clmROUND_NO"]).ToString().Equals(srtRoundNo)
                        && this.logic.GetCellValue(grdRow.Cells["clmGYOUSHA_CD"]).ToString().Equals(srtGyoshaCd)
                        && this.logic.GetCellValue(grdRow.Cells["clmGENBA_CD"]).ToString().Equals(srtGenbaCd))
                    {
                        //回数と業者CDと現場CDの組み合わせ重複の場合。
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E031", "回数,業者CD,現場CD");
                        // エラー項目背景色は赤色に設定                   
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmROUND_NO"], true);
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGENBA_CD"], true);
                        return;
                    }
                }
                if (this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGYOUSHA_CD"].Value != null && this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGENBA_CD"].Value != null)
                {
                    this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGYOUSHAGENBA"].Value = this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGYOUSHA_CD"].Value.ToString() + this.syuusyuuDetailIchiran.Rows[rowIndex].Cells["clmGENBA_CD"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("popupAfterExecuteSyuusyuuDetailIchiran", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region コース処理

        private void COURSE_NAME_CD_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // コース名CDを保存しておく
                this.bakCourseNameCd = COURSE_NAME_CD.Text;
                // ｺｰｽ情報 ポップアップ初期化
                // 必要情報チェック
                if (!this.logic.CheckKyotenAndSagyouDate())
                {
                    return;
                }
                this.logic.PopUpDataInit();

                this.dayCD = this.DAY_CD.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Error("COURSE_NAME_CD_Enter", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        internal string dayCD;

        private void COURSE_NAME_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                var before = this.GetBeforeText(this.COURSE_NAME_CD.Name);

                LogUtility.DebugMethodStart(sender, e);

                // 未入力時は名称をクリア
                if (this.COURSE_NAME_CD.Text.Trim().Length == 0)
                {
                    COURSE_NAME_RYAKU.Clear();
                    this.DAY_CD.Text = string.Empty;
                    this.NioroshiIchiran.Rows.Clear();
                    this.DetailIchiran.Rows.Clear();
                    return;
                }

                // 必要情報チェック もしくは、前回値と同じ値だった場合  処理しない
                if (!this.logic.CheckKyotenAndSagyouDate())
                {
                    return;
                }

                var mCourseDetailDao = DaoInitUtility.GetComponent<IM_COURSE_DETAILDao>();

                // マスタ存在チェック
                var courseNameListDto = new CourseNameListDto();
                courseNameListDto.SagyouDate = Convert.ToDateTime(this.SAGYOU_DATE.Value);
                courseNameListDto.DayCd = 0;
                if (this.FURIKAE_HAISHA_KBN.Text == "2")
                {
                    switch (this.DAY_NM.Text)
                    {
                        case "月":
                            courseNameListDto.DayCd = 1;
                            break;
                        case "火":
                            courseNameListDto.DayCd = 2;
                            break;
                        case "水":
                            courseNameListDto.DayCd = 3;
                            break;
                        case "木":
                            courseNameListDto.DayCd = 4;
                            break;
                        case "金":
                            courseNameListDto.DayCd = 5;
                            break;
                        case "土":
                            courseNameListDto.DayCd = 6;
                            break;
                        case "日":
                            courseNameListDto.DayCd = 7;
                            break;
                    }
                }

                courseNameListDto.CourseNameCd = this.COURSE_NAME_CD.Text;
                courseNameListDto.KyotenCd = this.logic.headForm.KYOTEN_CD.Text;
                var courseNameList = mCourseDetailDao.GetCourseNameListForPopup(courseNameListDto);
                if (courseNameList.Rows.Count == 0)
                {
                    this.COURSE_NAME_CD.IsInputErrorOccured = true;
                    e.Cancel = true;
                    this.logic.isInputError = true;

                    MessageBox.Show("コース名称マスタに存在しないコードが入力されました。", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    this.COURSE_NAME_CD.SelectAll();
                    this.COURSE_NAME_RYAKU.Clear();
                    return;
                }

                // 入力されたCDがマスタの曜日に該当するかチェック
                if (this.FURIKAE_HAISHA_KBN.Text != "2")
                {
                    courseNameListDto.DayCd = DateUtility.GetShougunDayOfWeek(courseNameListDto.SagyouDate);
                    courseNameList = mCourseDetailDao.GetCourseNameListForPopup(courseNameListDto);
                    if (courseNameList.Rows.Count == 0)
                    {
                        this.COURSE_NAME_CD.IsInputErrorOccured = true;
                        e.Cancel = true;
                        this.logic.isInputError = true;

                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E062", "コースCDに作業日の曜日");

                        this.COURSE_NAME_CD.SelectAll();
                        this.COURSE_NAME_RYAKU.Clear();
                        return;
                    }
                }

                if (this.COURSE_NAME_CD.Text == before && !this.logic.isInputError && this.DAY_CD.Text == this.dayCD)
                {
                    return;
                }

                this.logic.isInputError = false;
                if (this.FURIKAE_HAISHA_KBN.Text == "2" && courseNameList.Rows.Count > 1)
                {
                    // 複数が該当する場合はポップアップを表示
                    this.logic.PopUpDataInit(this.COURSE_NAME_CD.Text);
                    CustomControlExtLogic.PopUp(this.COURSE_NAME_CD);
                    if (string.IsNullOrEmpty(this.DAY_NM.Text))
                    {
                        this.logic.isInputError = true;
                        e.Cancel = true;
                        return;
                    }
                    switch (this.DAY_NM.Text)
                    {
                        case "月":
                            courseNameListDto.DayCd = 1;
                            break;
                        case "火":
                            courseNameListDto.DayCd = 2;
                            break;
                        case "水":
                            courseNameListDto.DayCd = 3;
                            break;
                        case "木":
                            courseNameListDto.DayCd = 4;
                            break;
                        case "金":
                            courseNameListDto.DayCd = 5;
                            break;
                        case "土":
                            courseNameListDto.DayCd = 6;
                            break;
                        case "日":
                            courseNameListDto.DayCd = 7;
                            break;
                    }
                }

                /* コース情報取得処理 */
                if (!string.IsNullOrEmpty(COURSE_NAME_CD.Text) &&
                    !bakCourseNameCd.Equals(COURSE_NAME_CD.Text) &&
                    string.IsNullOrEmpty(TEIKI_HAISHA_NUMBER.Text))
                {
                    // 定期配車存在チェック
                    //bool isExist = this.logic.IsExistTeikiHaisha(COURSE_NAME_CD.Text);
                    //if (!isExist)
                    //{
                    //    // 存在しない場合コース情報を設定する

                    // 定期配車番号が未入力 && コースが入力された場合
                    bool catchErr = false;
                    this.logic.SetCourseInfo(COURSE_NAME_CD.Text, SAGYOU_DATE.Value.ToString(), out catchErr);
                    if (catchErr)
                    {
                        e.Cancel = true;
                        this.logic.isInputError = true;
                        return;
                    }

                    mHinnmeiShushuKirikaeFlg = false;
                    //品名情報表示
                    this.TabControl.SelectTab(0);
                    mHinnmeiShushuKirikaeFlg = true;

                    if (this.ActiveControl != null)
                    {
                        this.ActiveControl.Focus();
                    }
                    //}
                }

                this.COURSE_NAME_RYAKU.Text = courseNameList.Rows[0]["COURSE_NAME_RYAKU"].ToString();

                // コースに紐付く情報のセット
                this.logic.setCourseData(Int16.Parse(courseNameListDto.DayCd.ToString()), this.COURSE_NAME_CD.Text);

                switch (this.DAY_NM.Text)
                {
                    case "月":
                        this.DAY_CD.Text = "1";
                        break;
                    case "火":
                        this.DAY_CD.Text = "2";
                        break;
                    case "水":
                        this.DAY_CD.Text = "3";
                        break;
                    case "木":
                        this.DAY_CD.Text = "4";
                        break;
                    case "金":
                        this.DAY_CD.Text = "5";
                        break;
                    case "土":
                        this.DAY_CD.Text = "6";
                        break;
                    case "日":
                        this.DAY_CD.Text = "7";
                        break;
                }

                this.DAY_NM.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("COURSE_NAME_CD_Validating", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region DetailIchiran_CellValidated

        /// <summary>
        /// 画面中に換算情報 設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);


                if (e.RowIndex >= 0)
                {
                    DataGridViewRow detailRow = this.DetailIchiran.Rows[e.RowIndex];
                    if (!this.logic.IsExecutableCell(detailRow))
                    {
                        return;
                    }

                    #region 品名明細部取得:業者CD	現場CD	品名CD	数量	単位CD

                    if((this.DetailIchiran.Columns[ConstCls.DetailColName.GYOUSHA_CD].Index == e.ColumnIndex) ||
                       (this.DetailIchiran.Columns[ConstCls.DetailColName.GENBA_CD].Index == e.ColumnIndex) ||
                       (this.DetailIchiran.Columns[ConstCls.DetailColName.HINMEI_CD].Index == e.ColumnIndex) ||
                       (this.DetailIchiran.Columns[ConstCls.DetailColName.SUURYOU].Index == e.ColumnIndex) ||
                       (this.DetailIchiran.Columns[ConstCls.DetailColName.UNIT_CD].Index == e.ColumnIndex) ||
                       (this.DetailIchiran.Columns[ConstCls.DetailColName.ROUND_NO].Index == e.ColumnIndex))
                    {
                        // 業者CDチェック
                        if((this.DetailIchiran.Columns[ConstCls.DetailColName.GYOUSHA_CD].Index == e.ColumnIndex)
                            && (detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString().Equals(string.Empty)
                                || !beforeGyoushaCd.Equals(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString())))
                        {
                            detailRow.Cells[ConstCls.DetailColName.GENBA_CD].Value = string.Empty;
                            detailRow.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = string.Empty;
                        }

                        // 換算情報を再設定する場合
                        if (isKanzanRecalculation)
                        {
                            // 換算情報 設定
                            this.logic.setKansannInfo(detailRow);
                        }
                    }
                    #endregion


                    var row = this.DetailIchiran.Rows[e.RowIndex];
                    if(this.DetailIchiran.Columns[ConstCls.DetailColName.KEIYAKU_KBN].Index == e.ColumnIndex)
                    {
                        switch(this.logic.GetCellValue(row.Cells["KEIYAKU_KBN"]))
                        {
                            case "1":
                                row.Cells["KEIYAKU_KBN_NM"].Value = "定期";
                                //集計区分
                                row.Cells["TSUKIGIME_KBN"].Value = string.Empty;
                                row.Cells["TSUKIGIME_KBN_NM"].Value = string.Empty;
                                row.Cells["TSUKIGIME_KBN"].ReadOnly = true;
                                this.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText.Replace("※", "");

                                break;

                            case "2":
                                row.Cells["KEIYAKU_KBN_NM"].Value = "単価";
                                //集計区分
                                if (this.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG && !row.Cells["KEIYAKU_KBN"].ReadOnly)
                                {
                                    row.Cells["TSUKIGIME_KBN"].ReadOnly = false;
                                    this.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText + "※";
                                }

                                break;
                            case "3":
                                row.Cells["KEIYAKU_KBN_NM"].Value = "回収のみ";
                                //集計区分
                                row.Cells["TSUKIGIME_KBN"].Value = string.Empty;
                                row.Cells["TSUKIGIME_KBN_NM"].Value = string.Empty;
                                row.Cells["TSUKIGIME_KBN"].ReadOnly = true;
                                this.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText.Replace("※", "");

                                break;
                            default:
                                row.Cells["KEIYAKU_KBN_NM"].Value = string.Empty;
                                row.Cells["TSUKIGIME_KBN"].Value = string.Empty;
                                row.Cells["TSUKIGIME_KBN_NM"].Value = string.Empty;
                                break;
                        }
                    }
                    else if(this.DetailIchiran.Columns[ConstCls.DetailColName.TSUKIGIME_KBN].Index == e.ColumnIndex)
                    {
                        switch(this.logic.GetCellValue(row.Cells["TSUKIGIME_KBN"]))
                        {
                            case "1":
                                row.Cells["TSUKIGIME_KBN_NM"].Value = "伝票";
                                break;

                            case "2":
                                row.Cells["TSUKIGIME_KBN_NM"].Value = "合算";
                                break;

                            default:
                                row.Cells["TSUKIGIME_KBN_NM"].Value = string.Empty;
                                break;
                        }
                    }
                    else if(this.DetailIchiran.Columns[ConstCls.DetailColName.SHUUSHUU_HOUR].Index == e.ColumnIndex)
                    {
                        if(!string.IsNullOrEmpty(this.logic.GetCellValue(row.Cells[ConstCls.DetailColName.SHUUSHUU_HOUR])) &&
                            string.IsNullOrEmpty(this.logic.GetCellValue(row.Cells[ConstCls.DetailColName.SHUUSHUU_MIN])))
                        {
                            row.Cells[ConstCls.DetailColName.SHUUSHUU_MIN].Value = "0";
                        }
                    }
                    else if(this.DetailIchiran.Columns[ConstCls.DetailColName.SHUUSHUU_MIN].Index == e.ColumnIndex)
                    {
                        if(string.IsNullOrEmpty(this.logic.GetCellValue(row.Cells[ConstCls.DetailColName.SHUUSHUU_HOUR])) &&
                            !string.IsNullOrEmpty(this.logic.GetCellValue(row.Cells[ConstCls.DetailColName.SHUUSHUU_MIN])))
                        {
                            row.Cells[ConstCls.DetailColName.SHUUSHUU_HOUR].Value = "0";
                        }
                    }
                    else if (this.DetailIchiran.Columns[ConstCls.DetailColName.GENBA_CD].Index == e.ColumnIndex)
                    {
                        if (row == null)
                        {
                            return;
                        }

                        // 業者名をセット
                        this.logic.SetGyoushaName(row);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("syuusyuuDetailIchiran_CellValidated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private string beforeSagyouDate = string.Empty;

        /// <summary>
        /// 作業日 Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_DATE_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DateTime dt;
                if (DateTime.TryParse(this.SAGYOU_DATE.Text, out dt))
                {
                    beforeSagyouDate = Convert.ToString(dt);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SAGYOU_DATE_Enter", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 作業日変更時は明細リストの[換算後数量][換算後単位CD]を設定する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_DATE_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (beforeSagyouDate != null
                    && !beforeSagyouDate.Equals(Convert.ToString(this.SAGYOU_DATE.Value)))
                {
                    this.COURSE_NAME_CD.Text = string.Empty;
                    this.COURSE_NAME_RYAKU.Text = string.Empty;
                    this.NioroshiIchiran.Rows.Clear();
                    this.DetailIchiran.Rows.Clear();
                }

                // No.3362-->
                //foreach (DataGridViewRow dtRow in this.DetailIchiran.Rows)
                //{
                //    this.logic.setKansannInfo(dtRow);
                //}
                // No.3362<--
            }
            catch (Exception ex)
            {
                LogUtility.Error("SAGYOU_DATE_ValueChanged", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 明細タブ切り替え時の入力チェック
        /// <summary>
        /// 明細タブ切り替え時の入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (mKirikaeCheckFlg)
                {
                    if (!this.TabControlSelectedIndexChangedCheck())
                    {
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("TabControl_Selecting", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 明細タブ切り替え時の入力チェック
        /// </summary>
        /// <returns></returns>
        private bool TabControlSelectedIndexChangedCheck()
        {
            // 4) 明細タブ切り替え時の入力チェック（収集毎→品名毎）
            if (this.TabControl.SelectedIndex == 0)
            {
                foreach (DataGridViewRow grdRow in this.syuusyuuDetailIchiran.Rows)
                {
                    string srtRoundNo = this.logic.GetCellValue(grdRow.Cells["clmROUND_NO"]).ToString();
                    string srtGyoshaCd = this.logic.GetCellValue(grdRow.Cells["clmGYOUSHA_CD"]).ToString();
                    string srtGenbaCd = this.logic.GetCellValue(grdRow.Cells["clmGENBA_CD"]).ToString();

                    if(string.IsNullOrEmpty(srtRoundNo) || string.IsNullOrEmpty(srtGyoshaCd) || string.IsNullOrEmpty(srtGenbaCd))
                    {

                        var messageShowLogic = new MessageBoxShowLogic();
                        if(string.IsNullOrEmpty(srtRoundNo))
                        {
                            messageShowLogic.MessageBoxShow("E001", "回数");
                            // エラー項目背景色は赤色に設定                   
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["clmROUND_NO"], true);
                        }
                        else if(string.IsNullOrEmpty(srtGyoshaCd))
                        {
                            messageShowLogic.MessageBoxShow("E001", "業者CD");
                            // エラー項目背景色は赤色に設定                   
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["clmGYOUSHA_CD"], true);
                        }
                        else if(string.IsNullOrEmpty(srtGenbaCd))
                        {
                            messageShowLogic.MessageBoxShow("E001", "現場CD");
                            // エラー項目背景色は赤色に設定                   
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["clmGENBA_CD"], true);
                        }
                        //エラー発生時はFalg＊＊
                        mKirikaeCheckFlg = false;
                        this.TabControl.SelectTab(1);
                        //エラー発生時はFalg＊＊
                        mKirikaeCheckFlg = false;
                        return false;
                    }
                }
            }
            else if (this.TabControl.SelectedIndex == 1)
            {
                //3) 明細タブ切り替え時の入力チェック（品名毎→収集毎）
                foreach (DataGridViewRow grdRow in this.DetailIchiran.Rows)
                {
                    string srtGyoshaCd = this.logic.GetCellValue(grdRow.Cells["GYOUSHA_CD"]).ToString();
                    string srtGenbaCd = this.logic.GetCellValue(grdRow.Cells["GENBA_CD"]).ToString();
                    string srtRoundNo = this.logic.GetCellValue(grdRow.Cells["ROUND_NO"]).ToString();
                    if(grdRow.Index == this.DetailIchiran.Rows.Count - 1)
                    {
                        return true;
                    }
                    if(string.IsNullOrEmpty(srtGyoshaCd) || string.IsNullOrEmpty(srtGenbaCd) || string.IsNullOrEmpty(srtRoundNo))
                    {

                        var messageShowLogic = new MessageBoxShowLogic();
                        if (string.IsNullOrEmpty(srtGyoshaCd))
                        {
                            messageShowLogic.MessageBoxShow("E001", "業者CD");
                            // エラー項目背景色は赤色に設定                   
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["GYOUSHA_CD"], true);
                        }
                        else if(string.IsNullOrEmpty(srtGenbaCd))
                        {
                            messageShowLogic.MessageBoxShow("E001", "現場CD");
                            // エラー項目背景色は赤色に設定                   
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["GENBA_CD"], true);
                        }
                        else if(string.IsNullOrEmpty(srtRoundNo))
                        {
                            messageShowLogic.MessageBoxShow("E001", "回数");
                            // エラー項目背景色は赤色に設定                   
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["ROUND_NO"], true);
                        }
                        //エラー発生時はFalg＊＊
                        mKirikaeCheckFlg = false;
                        this.TabControl.SelectTab(0);
                        //エラー発生時はFalg＊＊
                        mKirikaeCheckFlg = false;
                        return false;
                    }

                    string unitCd = this.logic.GetCellValue(grdRow.Cells["UNIT_CD"]);
                    string kansanUnitCd = this.logic.GetCellValue(grdRow.Cells["KANSAN_UNIT_CD"]);

                    if (string.IsNullOrEmpty(unitCd) && string.IsNullOrEmpty(kansanUnitCd))
                    {
                        continue;
                    }
                    if (unitCd.Equals(kansanUnitCd))
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E031", "単位、換算後単位");
                        // エラー項目背景色は赤色に設定                   
                        ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["UNIT_CD"], true);
                        ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["KANSAN_UNIT_CD"], true);
                        //エラー発生時はFalg＊＊
                        mKirikaeCheckFlg = false;
                        this.TabControl.SelectTab(0);
                        //エラー発生時はFalg＊＊
                        mKirikaeCheckFlg = false;
                        return false;
                    }
                    if (DuplicatedUnit(grdRow))
                    {
                        //エラー発生時はFalg＊＊
                        mKirikaeCheckFlg = false;
                        this.TabControl.SelectTab(0);
                        //エラー発生時はFalg＊＊
                        mKirikaeCheckFlg = false;
                        return false;
                    }
                }
            }
            mKirikaeCheckFlg = true;
            return true;

        }

        /// <summary>
        /// 単位、換算後単位の重複チェック
        /// </summary>
        /// <param name="currentRow"></param>
        /// <returns>true:エラー有, false:エラー無</returns>
        private bool DuplicatedUnit(DataGridViewRow currentRow)
        {
            string currentRoundNo = this.logic.GetCellValue(currentRow.Cells["ROUND_NO"]);
            string currentGyoshaCd = this.logic.GetCellValue(currentRow.Cells["GYOUSHA_CD"]);
            string currentGenbaCd = this.logic.GetCellValue(currentRow.Cells["GENBA_CD"]);
            string currentHinmeiCd = this.logic.GetCellValue(currentRow.Cells["HINMEI_CD"]);
            string currentUnitCd = this.logic.GetCellValue(currentRow.Cells["UNIT_CD"]);
            string currentKansanUnitCd = this.logic.GetCellValue(currentRow.Cells["KANSAN_UNIT_CD"]);

            // 回数,業者,現場,品名が空の場合は未チェック
            if(string.IsNullOrEmpty(currentRoundNo)
                || string.IsNullOrEmpty(currentGyoshaCd)
                || string.IsNullOrEmpty(currentGenbaCd)
                || string.IsNullOrEmpty(currentHinmeiCd)
                || (string.IsNullOrEmpty(currentUnitCd) && string.IsNullOrEmpty(currentKansanUnitCd)))
            {
                return false;
            }

            foreach (DataGridViewRow grdRow in this.DetailIchiran.Rows)
            {
                if (grdRow.Index == this.DetailIchiran.Rows.Count - 1)
                {
                    return false;
                }

                // 同列の場合は未チェック
                if (grdRow.Index == currentRow.Index)
                {
                    continue;
                }

                string roundNo = this.logic.GetCellValue(grdRow.Cells["ROUND_NO"]);
                string gyoshaCd = this.logic.GetCellValue(grdRow.Cells["GYOUSHA_CD"]);
                string genbaCd = this.logic.GetCellValue(grdRow.Cells["GENBA_CD"]);
                string hinmeiCd = this.logic.GetCellValue(grdRow.Cells["HINMEI_CD"]);
                string unitCd = this.logic.GetCellValue(grdRow.Cells["UNIT_CD"]);
                string kansanUnitCd = this.logic.GetCellValue(grdRow.Cells["KANSAN_UNIT_CD"]);

                if(string.IsNullOrEmpty(roundNo) || string.IsNullOrEmpty(gyoshaCd) || string.IsNullOrEmpty(genbaCd) || string.IsNullOrEmpty(hinmeiCd))
                {
                    continue;
                }

                if(roundNo.Equals(currentRoundNo) && gyoshaCd.Equals(currentGyoshaCd) && genbaCd.Equals(currentGenbaCd) && hinmeiCd.Equals(currentHinmeiCd))
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    var errorStr = "同一の回数、業者、現場、品名で";
                    if (!string.IsNullOrEmpty(currentUnitCd))
                    {
                        if (currentUnitCd.Equals(unitCd))
                        {
                            messageShowLogic.MessageBoxShow("E031", errorStr + "単位");
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["UNIT_CD"], true);
                            ControlUtility.SetInputErrorOccuredForDgvCell(currentRow.Cells["UNIT_CD"], true);
                            return true;
                        }
                        else if (currentUnitCd.Equals(kansanUnitCd))
                        {
                            messageShowLogic.MessageBoxShow("E031", errorStr + "単位、換算後単位");
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["KANSAN_UNIT_CD"], true);
                            ControlUtility.SetInputErrorOccuredForDgvCell(currentRow.Cells["UNIT_CD"], true);
                            return true;
                        }
                    }

                    if (!string.IsNullOrEmpty(currentKansanUnitCd))
                    {
                        if (currentKansanUnitCd.Equals(unitCd))
                        {
                            messageShowLogic.MessageBoxShow("E031", errorStr + "単位、換算後単位");
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["UNIT_CD"], true);
                            ControlUtility.SetInputErrorOccuredForDgvCell(currentRow.Cells["KANSAN_UNIT_CD"], true);
                            return true;
                        }
                        else if (currentKansanUnitCd.Equals(kansanUnitCd))
                        {
                            messageShowLogic.MessageBoxShow("E031", errorStr + "換算後単位");
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["KANSAN_UNIT_CD"], true);
                            ControlUtility.SetInputErrorOccuredForDgvCell(currentRow.Cells["KANSAN_UNIT_CD"], true);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        #endregion

        #region 収集GRIDセール背景色設定
        /// <summary>
        /// 収集GRIDセール背景色設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void syuusyuuDetailIchiran_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (e.ColumnIndex >= 11)
                {
                    DataGridViewCell Cell = this.syuusyuuDetailIchiran.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    foreach (DataGridViewCell ariDataCell in this.logic.mCellAriList)
                    {
                        if (ariDataCell != Cell && string.IsNullOrEmpty(this.logic.GetCellValue(Cell).ToString()))
                        {
                            //存在しないデータ、値入力ありません場合
                            Cell.Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
                        }
                    }
                    bool exist = false;
                    foreach (DataGridViewCell ariDataCell in this.logic.mCellAriList)
                    {
                        //存在しないデータ、値入力ある場合
                        if (ariDataCell == Cell)
                        {
                            exist = true;

                        }
                    }
                    //存在しないデータ、値入力ある場合
                    if (!exist && !string.IsNullOrEmpty(this.logic.GetCellValue(Cell).ToString()))
                    {
                        this.logic.mCellAriList.Add(Cell);
                    }

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("syuusyuuDetailIchiran_CellValidated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region IME無効(半角英数のみ)
        private string beforeNioroshiGyoushaCd = string.Empty;

        private void NioroshiIchiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    var row = this.NioroshiIchiran.Rows[e.RowIndex];
                    beforeNioroshiGyoushaCd = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                    break;

                case 3:
                case 5:
                    //この列はIME無効(半角英数のみ)
                    NioroshiIchiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
            }

            // 前回値チェック用データをセット
            String cellName = this.NioroshiIchiran.Columns[e.ColumnIndex].Name;
            String cellValue = Convert.ToString(this.NioroshiIchiran[cellName, e.RowIndex].Value);
            if (this.beforeValuesForDetail.ContainsKey(cellName))
            {
                this.beforeValuesForDetail[cellName] = cellValue;
            }
            else
            {
                this.beforeValuesForDetail.Add(cellName, cellValue);
            }

        }

        // 業者CDの前回値
        private string beforeGyoushaCd = string.Empty;

        /// <summary>
        /// 明細欄のCellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            var row = this.DetailIchiran.Rows[e.RowIndex];

            if (!this.logic.IsExecutableCell(row))
            {
                return;
            }

            DgvCustom dgv = (DgvCustom)sender;

            if(this.DetailIchiran.Columns[ConstCls.DetailColName.GYOUSHA_CD].Index == e.ColumnIndex)
            {
                this.beforeGyoushaCd = row.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString();
            }
            else if(this.DetailIchiran.Columns[ConstCls.DetailColName.HINMEI_BIKOU].Index == e.ColumnIndex)
            {
                //この列はIME(ひらがな)
                dgv.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            }
            else
            {
                //この列はIME無効(半角英数のみ)
                dgv.ImeMode = System.Windows.Forms.ImeMode.Disable;
            }

            if (0 <= e.RowIndex)
            {
                // 初期化
                this.bakGyousyaCd = string.Empty;
                this.bakGenbaCd = string.Empty;
                this.bakHinmeiCd = string.Empty;
                this.bakSuuryou = string.Empty;
                this.bakUnitCd = string.Empty;
                this.bakRoundNo = string.Empty;

                if (row.Cells[ConstCls.DetailColName.GYOUSHA_CD] != null && row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value != null)
                {
                    this.bakGyousyaCd = row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value.ToString();
                }
                if (row.Cells[ConstCls.DetailColName.GENBA_CD] != null && row.Cells[ConstCls.DetailColName.GENBA_CD].Value != null)
                {
                    this.bakGenbaCd = row.Cells[ConstCls.DetailColName.GENBA_CD].Value.ToString();
                }
                if (row.Cells[ConstCls.DetailColName.HINMEI_CD] != null && row.Cells[ConstCls.DetailColName.HINMEI_CD].Value != null)
                {
                    this.bakHinmeiCd = row.Cells[ConstCls.DetailColName.HINMEI_CD].Value.ToString();
                }
                if (row.Cells[ConstCls.DetailColName.SUURYOU] != null && row.Cells[ConstCls.DetailColName.SUURYOU].Value != null)
                {
                    this.bakSuuryou = row.Cells[ConstCls.DetailColName.SUURYOU].Value.ToString();
                }
                if (row.Cells[ConstCls.DetailColName.UNIT_CD] != null && row.Cells[ConstCls.DetailColName.UNIT_CD].Value != null)
                {
                    this.bakUnitCd = row.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString();
                }
                if (row.Cells[ConstCls.DetailColName.ROUND_NO] != null && row.Cells[ConstCls.DetailColName.ROUND_NO].Value != null)
                {
                    this.bakRoundNo = row.Cells[ConstCls.DetailColName.ROUND_NO].Value.ToString();
                }
            }

            // 前回値チェック用データをセット
            String cellName = this.DetailIchiran.Columns[e.ColumnIndex].Name;                     
            String cellValue = Convert.ToString(this.DetailIchiran[cellName, e.RowIndex].FormattedValue);

            if (!bCancelDenpyoPopup)
            {
                if (ConstCls.DetailColName.UNIT_CD.Equals(cellName)
                    || ConstCls.DetailColName.KANSAN_UNIT_CD.Equals(cellName))
                {
                    // CDと名称のコントロールと一緒の場合、FormattedValueが使えない(名称が設定されている)ためValueで比較する
                    cellValue = Convert.ToString(this.DetailIchiran[cellName, e.RowIndex].Value);
                }

                if (this.beforeValuesForDetail.ContainsKey(cellName))
                {
                    this.beforeValuesForDetail[cellName] = cellValue;
                }
                else
                {
                    this.beforeValuesForDetail.Add(cellName, cellValue);
                }
            }
            else
            {
                bCancelDenpyoPopup = false;
            }
        }

        private void syuusyuuDetailIchiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // 編集列名の取得
            var cellName = this.syuusyuuDetailIchiran.Columns[e.ColumnIndex].Name;
            var row = this.syuusyuuDetailIchiran.Rows[e.RowIndex];

            switch(cellName)
            {
                case "clmKAISHUU_BIKOU":
                    //この列はIME(ひらがな)
                    syuusyuuDetailIchiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                    break;
                // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                case "clmGYOUSHA_CD":
                    // 前回業者
                    if (row.Cells["clmGYOUSHA_CD"] != null && row.Cells["clmGYOUSHA_CD"].Value != null)
                    {
                        this.bakSSGyousyaCd = row.Cells["clmGYOUSHA_CD"].Value.ToString();
                    }
                    break;
                case "clmGENBA_CD":
                    // 前回業者
                    if (row.Cells["clmGENBA_CD"] != null && row.Cells["clmGENBA_CD"].Value != null)
                    {
                        this.bakSSGenbaCd = row.Cells["clmGENBA_CD"].Value.ToString();
                    }
                    break;
                // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                default:
                    //この列はIME無効(半角英数のみ)
                    syuusyuuDetailIchiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
            }

            // 該当行が確定済なら、編集不可になっているのでスキップ
            if (!this.logic.IsExecutableCellForsyuusyuuDetail(row))
            {
                return;
            }

            if (0 <= e.RowIndex && 10 <= e.ColumnIndex)
            {
                // 前回値チェック用データをセット
                if (bakSuuryouSuusuIchiran.ContainsKey(e.ColumnIndex))
                {
                    bakSuuryouSuusuIchiran[e.ColumnIndex] = Convert.ToString(row.Cells[e.ColumnIndex].Value);
                }
                else
                {
                    bakSuuryouSuusuIchiran.Add(e.ColumnIndex, Convert.ToString(row.Cells[e.ColumnIndex].Value));
                }
            }

            // 前回値チェック用データをセット
            String cellValue = Convert.ToString(this.syuusyuuDetailIchiran[cellName, e.RowIndex].Value);
            if (beforeValuesForDetail.ContainsKey(cellName))
            {
                beforeValuesForDetail[cellName] = cellValue;
            }
            else
            {
                beforeValuesForDetail.Add(cellName, cellValue);
            }
        }
        #endregion

        #region tabControl横レイアウトでの並び順
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //背景色
            Color BackColor = Color.FromArgb(232, 247, 240);

            //入力可能テキストコントロール
            Color PreColor = Color.FromArgb(255, 255, 255);

            //入力中テキストコントロール
            Color InputColor = Color.FromArgb(0, 255, 255);

            TabControl tab = (TabControl)sender;
            Graphics g = e.Graphics;
            Brush b;
            TabPage page = tab.TabPages[e.Index];
            //タブページのテキストを取得
            string txt = page.Text;

            //StringFormatを作成
            StringFormat sf = new StringFormat();
            //縦書きにする
            sf.FormatFlags = StringFormatFlags.DirectionVertical;
            //ついでに、水平垂直方向の中央に、行が完全に表示されるようにする
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            sf.FormatFlags |= StringFormatFlags.LineLimit;

            //背景の描画
            if (e.Index == tab.SelectedIndex)
            {
                // イベントが選択ページに対しての場合
                b = new SolidBrush(InputColor);
            }
            else
            {
                // イベントがその他のページの場合
                b = new SolidBrush(PreColor);
            }
            g.FillRectangle(b, tab.GetTabRect(e.Index));

            //Textの描画
            Brush foreBrush = new SolidBrush(page.ForeColor);
            e.Graphics.DrawString(txt, page.Font, foreBrush, e.Bounds, sf);
            foreBrush.Dispose();

            //余白を塗る
            if (e.Index == tab.TabCount - 1)
            {
                // 最後のタブの場合、タブ領域の隣からタブコントロール全体の右端まで色を塗る
                b = new SolidBrush(BackColor);
                Rectangle r = tab.GetTabRect(e.Index);

                // タブコントロールのウィンドウハンドルからGraphicsオブジェクトを生成
                g = Graphics.FromHwnd(tab.Handle);
                g.FillRectangle(b, tab.Left, r.Top + r.Height, r.Width, tab.Height - r.Top - r.Height);
                g.Dispose();
            }
        }
        #endregion

        #region 車輌更新後処理
        /// <summary>
        /// 車輌更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var before = this.GetBeforeText(this.SHARYOU_CD.Name);
                if ((!this.logic.isInputError && this.SHARYOU_CD.Text == before) &&
                    (!this.logic.isCalledSharyouPopupFromLogic && !string.IsNullOrEmpty(this.SHARYOU_NAME_RYAKU.Text)))
                {
                    return;
                }

                this.logic.isCalledSharyouPopupFromLogic = false;
                if (!this.logic.ChechSharyouCd())
                {
                    // フォーカス設定
                    this.SHARYOU_CD.Focus();
                    return;
                }

                this.logic.isInputError = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHARYOU_CD_Validated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 明細から「単位CD」と「換算後単位CD」を非表示にする
        private void DetailIchiran_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == this.DetailIchiran.Columns[ConstCls.DetailColName.UNIT_CD].Index)
            {
                if (this.DetailIchiran[ConstCls.DetailColName.UNIT_NAME_RYAKU, e.RowIndex].Value != null)
                {
                    e.Value = this.DetailIchiran[ConstCls.DetailColName.UNIT_NAME_RYAKU, e.RowIndex].Value.ToString();
                }
            }

            if (e.ColumnIndex == this.DetailIchiran.Columns[ConstCls.DetailColName.KANSAN_UNIT_CD].Index)
            {
                if (this.DetailIchiran[ConstCls.DetailColName.UNITKANSAN_NAME, e.RowIndex].Value != null)
                {
                    e.Value = this.DetailIchiran[ConstCls.DetailColName.UNITKANSAN_NAME, e.RowIndex].Value.ToString();
                }
            }
        }

        /// <summary>
        /// EditingControlShowingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (DetailIchiran.CurrentCell.ColumnIndex == DetailIchiran.Columns[ConstCls.DetailColName.UNIT_CD].Index
                    || DetailIchiran.CurrentCell.ColumnIndex == DetailIchiran.Columns[ConstCls.DetailColName.KANSAN_UNIT_CD].Index)
                {
                    TextBox itemID = e.Control as TextBox;
                    if (itemID != null)
                    {
                        IsCdFlg = true;
                        itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                    }
                }
                else
                {
                    IsCdFlg = false;
                }

                e.Control.KeyDown -= this.DetailIchiranEditingControl_KeyDown;
                e.Control.KeyDown += this.DetailIchiranEditingControl_KeyDown;
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        /// <summary>
        /// 一覧編集ボックスキーダウン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiranEditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            this.logic.CheckPopup(e);
        }

        /// <summary>
        /// itemID_KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsCdFlg && !char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void DetailIchiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (this.isCellValidating)
                {
                    return;
                }
                
                String cellName = this.DetailIchiran.Columns[e.ColumnIndex].Name;
                String cellValue = Convert.ToString(this.DetailIchiran[cellName, e.RowIndex].FormattedValue);
                if (ConstCls.DetailColName.UNIT_CD.Equals(cellName)
                    || ConstCls.DetailColName.KANSAN_UNIT_CD.Equals(cellName))
                {
                    // CDと名称のコントロールと一緒の場合、FormattedValueが使えない(名称が設定されている)ためValueで比較する
                    cellValue = Convert.ToString(this.DetailIchiran[cellName, e.RowIndex].Value);
                }

                var row = this.DetailIchiran.Rows[e.RowIndex];

                if (!this.logic.IsExecutableCell(row) || (!this.logic.isInputError && beforeValuesForDetail[cellName] == cellValue))
                {
                    return;
                }

                // CellValidating処理
                if (!logic.DataGridViewCellValidating(e))
                {
                    e.Cancel = true;
                    return;
                }

                switch (cellName)
                {
                    // 回数
                    case (ConstCls.DetailColName.ROUND_NO):
                        var roundno = string.Empty;
                        if (row.Cells[ConstCls.DetailColName.ROUND_NO] != null && row.Cells[ConstCls.DetailColName.ROUND_NO].Value != null)
                        {
                            roundno = row.Cells[ConstCls.DetailColName.ROUND_NO].Value.ToString();
                        }

                        this.isKanzanRecalculation = false;
                        if (this.bakRoundNo != roundno)
                        {
                            this.isKanzanRecalculation = true;
                        }
                        return;

                    // 単位CD
                    case (ConstCls.DetailColName.UNIT_CD):
                        var unitValue = string.Empty;
                        if (row.Cells[ConstCls.DetailColName.UNIT_CD] != null && row.Cells[ConstCls.DetailColName.UNIT_CD].Value != null)
                        {
                            unitValue = row.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString();
                        }

                        this.isKanzanRecalculation = false;
                        if (this.bakUnitCd != unitValue)
                        {
                            this.isKanzanRecalculation = true;
                        }
                        //return;
                        break;

                    // 数量
                    case (ConstCls.DetailColName.SUURYOU):
                        var suuryou = string.Empty;
                        if (row.Cells[ConstCls.DetailColName.SUURYOU] != null && row.Cells[ConstCls.DetailColName.SUURYOU].Value != null)
                        {
                            suuryou = row.Cells[ConstCls.DetailColName.SUURYOU].Value.ToString();
                        }

                        this.isKanzanRecalculation = false;
                        if (this.bakSuuryou != suuryou)
                        {
                            this.isKanzanRecalculation = true;
                        }
                        return;

                    // 業者CD
                    case (ConstCls.DetailColName.GYOUSHA_CD):
                        // 品名CD取得
                        var gyoushaCd = string.Empty;
                        if (row.Cells[ConstCls.DetailColName.GYOUSHA_CD] != null && row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value != DBNull.Value)
                        {
                            // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                            if (!string.IsNullOrEmpty(row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value.ToString()))
                            {
                                row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value = row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                                gyoushaCd = row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value.ToString();
                            }
                            // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                        }
                        else
                        {
                            row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = string.Empty;
                        }
                        // 現場チェック
                        if (!this.logic.ChechiGyoushaCd(this.DetailIchiran.Rows[e.RowIndex]))
                        {
                            e.Cancel = true;
                            this.DetailIchiran.BeginEdit(false);
                            return;
                        }

                        this.logic.isInputError = false;

                        this.isKanzanRecalculation = false;
                        if (this.bakGyousyaCd == gyoushaCd)
                        {
                            return;
                        }
                        else
                        {
                            this.isKanzanRecalculation = true;
                        }
                        break;
                    // 現場CD
                    case (ConstCls.DetailColName.GENBA_CD):
                        // 品名CD取得
                        var genbaCd = string.Empty;
                        if (row.Cells[ConstCls.DetailColName.GENBA_CD] != null && row.Cells[ConstCls.DetailColName.GENBA_CD].Value != null)
                        {
                            // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                            if (!string.IsNullOrEmpty(row.Cells[ConstCls.DetailColName.GENBA_CD].Value.ToString()))
                            {
                                row.Cells[ConstCls.DetailColName.GENBA_CD].Value = row.Cells[ConstCls.DetailColName.GENBA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                                genbaCd = row.Cells[ConstCls.DetailColName.GENBA_CD].Value.ToString();
                            }
                            // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                        }
                        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                        // 現場チェック
                        if (!this.logic.ChechiGenbaCd(this.DetailIchiran.Rows[e.RowIndex]))
                        {
                            e.Cancel = true;
                            this.DetailIchiran.BeginEdit(false);
                            return;
                        }
                        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

                        this.logic.isInputError = false;

                        this.isKanzanRecalculation = false;
                        if (this.bakGenbaCd == genbaCd)
                        {
                            return;
                        }
                        else
                        {
                            this.isKanzanRecalculation = true;
                        }
                        break;
                    // 品名CD
                    case (ConstCls.DetailColName.HINMEI_CD):
                        // 品名CD取得
                        var hinmeiCd = string.Empty;
                        if (row.Cells[ConstCls.DetailColName.HINMEI_CD] != null && row.Cells[ConstCls.DetailColName.HINMEI_CD].Value != null)
                        {
                            hinmeiCd = row.Cells[ConstCls.DetailColName.HINMEI_CD].Value.ToString();
                        }

                        var denpyouKbn = this.DetailIchiran.CurrentRow.Cells[ConstCls.DetailColName.DENPYOU_KBN_CD_NM].Value
                            == null ? string.Empty : this.DetailIchiran.CurrentRow.Cells[ConstCls.DetailColName.DENPYOU_KBN_CD_NM].Value;

                        this.logic.isInputError = false;
                        this.isKanzanRecalculation = false;
                        if (this.bakHinmeiCd == hinmeiCd && !string.IsNullOrEmpty(denpyouKbn.ToString()))
                        {
                            return;
                        }
                        else
                        {
                            this.isKanzanRecalculation = true;
                        }
                        break;
                }

                DataGridViewRow detailRow = this.DetailIchiran.Rows[e.RowIndex];
                if((this.DetailIchiran.Columns[ConstCls.DetailColName.GYOUSHA_CD].Index == e.ColumnIndex) ||
                    (this.DetailIchiran.Columns[ConstCls.DetailColName.GENBA_CD].Index == e.ColumnIndex) ||
                    (this.DetailIchiran.Columns[ConstCls.DetailColName.HINMEI_CD].Index == e.ColumnIndex) ||
                    (this.DetailIchiran.Columns[ConstCls.DetailColName.UNIT_CD].Index == e.ColumnIndex))
                {
                    //伝票区分、契約区分、集計単位情報を取得と設定
                    this.logic.GetDataSetKbnInfo(detailRow, e);
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw ex;
            }
        }
        private void DetailIchiran_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow detailRow = this.DetailIchiran.Rows[e.RowIndex];

            if((this.DetailIchiran.Columns[ConstCls.DetailColName.GYOUSHA_CD].Index == e.ColumnIndex) ||
                (this.DetailIchiran.Columns[ConstCls.DetailColName.GENBA_CD].Index == e.ColumnIndex) ||
                (this.DetailIchiran.Columns[ConstCls.DetailColName.HINMEI_CD].Index == e.ColumnIndex) ||
                (this.DetailIchiran.Columns[ConstCls.DetailColName.UNIT_CD].Index == e.ColumnIndex))
            {
                mOrgDedailDtoInfo = new DTOClass();
                // 換算情報 
                mOrgDedailDtoInfo.GyoushaCd = this.logic.GetCellValue(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD]).ToString();
                mOrgDedailDtoInfo.GenbaCd = this.logic.GetCellValue(detailRow.Cells[ConstCls.DetailColName.GENBA_CD]).ToString();
                mOrgDedailDtoInfo.HinmeiCd = this.logic.GetCellValue(detailRow.Cells[ConstCls.DetailColName.HINMEI_CD]).ToString();
                mOrgDedailDtoInfo.UnitCd = string.IsNullOrEmpty(this.logic.GetCellValue(detailRow.Cells[ConstCls.DetailColName.UNIT_CD]).ToString()) ? -1 : int.Parse(this.logic.GetCellValue(detailRow.Cells[ConstCls.DetailColName.UNIT_CD]));
            }
        }
        #endregion

        #region 明細一覧のcellを結合する
        /// <summary>
        /// 列ヘッダーの罫線を消して結合しているように表示
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void DetailIchiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                //■■■■■■■■■■■■
                //ヘッダセルの結合処理開始
                //■■■■■■■■■■■■
                if (e.RowIndex > -1)
                {
                    // ヘッダー以外は処理なし
                    return;
                }

                //契約区分列から結合する
                int colIndex = this.DetailIchiran.Columns["KEIYAKU_KBN"].Index;

                //契約区分
                this.DetailIchiranCellPainting(sender, e, colIndex, "契約区分※");

                //集計単位
                this.DetailIchiranCellPainting(sender, e, colIndex + 2, "集計単位");

                // 結合セル以外は既定の描画を行う
                if (!(e.ColumnIndex == colIndex || e.ColumnIndex == colIndex + 1 || e.ColumnIndex == colIndex + 2 || e.ColumnIndex == colIndex + 3))
                {
                    e.Paint(e.ClipBounds, e.PaintParts);
                }

                // イベントハンドラ内で処理を行ったことを通知
                e.Handled = true;


            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw ex;
            }
        }


        private void DetailIchiranCellPainting(object sender, DataGridViewCellPaintingEventArgs e, int arCcolIndex, string colName)
        {
            //契約区分
            // 14列から結合
            int colIndex = arCcolIndex;

            // 5～6列目を結合する処理
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
                using (SolidBrush brush = new SolidBrush(this.DetailIchiran.ColumnHeadersDefaultCellStyle.BackColor))
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
                                colName,
                                e.CellStyle.Font,
                                rect,
                                e.CellStyle.ForeColor,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
            }

            //// 結合セル以外は既定の描画を行う
            //if (!(e.ColumnIndex == colIndex || e.ColumnIndex == colIndex + 1))
            //{
            //    e.Paint(e.ClipBounds, e.PaintParts);
            //}

            //// イベントハンドラ内で処理を行ったことを通知
            //e.Handled = true;
        }

        private void DetailIchiran_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.Refresh();
        }

        #endregion

        /// <summary>
        /// 運転者CD（FocusOutCheckMethodと併用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void UNTENSHA_CDValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.UNTENSHA_CD.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.UNTENSHA_CD.Text))
                {
                    // 運転者名の初期化は行う
                    this.UNTENSHA_NAME.Text = string.Empty;
                    return;
                }

                if (!this.logic.UNTENSHA_CDValidated())
                {
                    this.logic.isInputError = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運搬業者CD（FocusOutCheckMethodと併用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void UNPAN_GYOUSHA_CDValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if(!this.UNPAN_GYOUSHA_CD.Enabled)
                {
                    return;
                }

                var before = this.GetBeforeText(this.UNPAN_GYOUSHA_CD.Name);

                // ブランクの場合
                if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
                {
                    // 運搬業者名をクリアする。
                    this.UNPAN_GYOUSHA_NAME.Text = string.Empty;

                    if (this.UNPAN_GYOUSHA_CD.Text != before)
                    {
                        this.SHARYOU_CD.Text = string.Empty;
                        this.SHARYOU_NAME_RYAKU.Text = string.Empty;
                        this.ClearBeforeText(this.SHARYOU_CD.Name);
                    }
                    return;
                }

                // 前回値と同じ値だった場合 処理しない
                if (!this.logic.isInputError && this.UNPAN_GYOUSHA_CD.Text == before)
                {
                    return;
                }

                if (this.logic.UNPAN_GYOUSHA_CDValidated())
                {
                    this.logic.isInputError = false;
                }

            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 荷降明細更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NioroshiIchiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            var row = this.NioroshiIchiran.Rows[e.RowIndex];
            switch (e.ColumnIndex)
            {
                case 1:
                    if (row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value != null
                        && !string.IsNullOrEmpty(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value.ToString()))
                    {
                        row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value =
                            row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                    }

                    if (row != null
                        && (row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString().Equals(string.Empty)
                            || (!string.IsNullOrEmpty(beforeNioroshiGyoushaCd)
                                && !beforeNioroshiGyoushaCd.PadLeft(6, '0').ToUpper().Equals(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString()))))
                    {
                        row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value = string.Empty;
                        row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                    }
                    break;
                case 3:
                    if (row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value != null
                        && !string.IsNullOrEmpty(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value.ToString()))
                    {
                        row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value =
                            row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                    }
                    break;
                case 7:
                    if (!string.IsNullOrEmpty(this.logic.GetCellValue(row.Cells[ConstCls.NioroshiColName.HANNYUU_HOUR])) &&
                        string.IsNullOrEmpty(this.logic.GetCellValue(row.Cells[ConstCls.NioroshiColName.HANNYUU_MIN])))
                    {
                        row.Cells[ConstCls.NioroshiColName.HANNYUU_MIN].Value = "0";
                    }
                    break;
                case 8:
                    if (string.IsNullOrEmpty(this.logic.GetCellValue(row.Cells[ConstCls.NioroshiColName.HANNYUU_HOUR])) &&
                        !string.IsNullOrEmpty(this.logic.GetCellValue(row.Cells[ConstCls.NioroshiColName.HANNYUU_MIN])))
                    {
                        row.Cells[ConstCls.NioroshiColName.HANNYUU_HOUR].Value = "0";
                    }
                    break;
                default:
                    break;
            }
        }

        #region 時間項目の入力後チェック

        /// <summary>
        /// 出庫時間_時の入力後チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUKKO_HOUR_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!string.IsNullOrEmpty(this.SHUKKO_HOUR.Text) && string.IsNullOrEmpty(this.SHUKKO_MINUTE.Text))
                {
                    this.SHUKKO_MINUTE.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 出庫時間_分の入力後チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUKKO_MINUTE_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (string.IsNullOrEmpty(this.SHUKKO_HOUR.Text) && !string.IsNullOrEmpty(this.SHUKKO_MINUTE.Text))
                {
                    this.SHUKKO_MINUTE.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 帰庫時間_時の入力後チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KIKO_HOUR_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!string.IsNullOrEmpty(this.KIKO_HOUR.Text) && string.IsNullOrEmpty(this.KIKO_MINUTE.Text))
                {
                    this.KIKO_MINUTE.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 帰庫時間_分の入力後チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KIKO_MINUTE_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (string.IsNullOrEmpty(this.KIKO_HOUR.Text) && !string.IsNullOrEmpty(this.KIKO_MINUTE.Text))
                {
                    this.KIKO_MINUTE.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion 時間項目の自動入力

        /// <summary>
        /// 明細欄（詳細）のスクロールイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void syuusyuuDetailIchiran_Scroll(object sender, ScrollEventArgs e)
        {
            // スクロールを同期
            this.syuusyuuDetailIchiranSoukei.HorizontalScrollingOffset = this.syuusyuuDetailIchiran.HorizontalScrollingOffset;
            this.syuusyuuDetailIchiranScroll.HorizontalScrollingOffset = this.syuusyuuDetailIchiran.HorizontalScrollingOffset;
        }

        /// <summary>
        /// 明細欄（スクロール用）のスクロールイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void syuusyuuDetailIchiranScroll_Scroll(object sender, ScrollEventArgs e)
        {
            // スクロールを同期
            this.syuusyuuDetailIchiranSoukei.HorizontalScrollingOffset = this.syuusyuuDetailIchiranScroll.HorizontalScrollingOffset;
            this.syuusyuuDetailIchiran.HorizontalScrollingOffset = this.syuusyuuDetailIchiranScroll.HorizontalScrollingOffset;
        }

        #region ポップアップ後の処理
        /// <summary>
        /// 現場CDポップアップ後の処理
        /// </summary>
        public void GenbaPopupAfterMethod()
        {
            LogUtility.DebugMethodStart();

            // 現在行があるかチェック
            var row = this.DetailIchiran.CurrentRow;
            if (row == null)
            {
                return;
            }

            // 業者名をセット
            this.logic.SetGyoushaName(row);
        }

        /// <summary>
        /// 荷降業者CDポップアップ後の処理
        /// </summary>
        public void NioroshiGenbaAfterPopupMethod()
        {
            LogUtility.DebugMethodStart();

            // 現在行があるかチェック
            var row = this.NioroshiIchiran.CurrentRow;
            if (row == null)
            {
                return;
            }

            // 荷降業者名をセット
            if (!this.logic.SetNioroshiGyoushaName(row)) { return; }

            // フォーカスを現場にセット
            this.NioroshiIchiran["NIOROSHI_GENBA_CD", row.Index].Selected = true;
        }
        #endregion ポップアップ後の処理

        // 20141015 koukouei 休動管理機能 start
        #region 補助員CD検証処理
        /// <summary>
        /// 補助員CD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HOJOIN_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!this.HOJOIN_CD.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.HOJOIN_CD.Text))
                {
                    // 補助員名の初期化は行う
                    this.HOJOIN_NAME.Text = string.Empty;
                    return;
                }

                this.logic.HOJOIN_CDValidated();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 荷降明細更新処理
        /// <summary>
        /// 荷降明細更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NioroshiIchiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                String cellName = this.NioroshiIchiran.Columns[e.ColumnIndex].Name;
                String cellValue = Convert.ToString(this.NioroshiIchiran[cellName, e.RowIndex].Value);

                if (e.RowIndex < 0 || (!this.logic.isInputError && beforeValuesForDetail[cellName] == cellValue))
                {
                    return;
                }

                switch (e.ColumnIndex)
                {
                    case 1:
                        // 荷降業者チェック
                        if (!this.logic.ChechNioroshiGyoushaCd(this.NioroshiIchiran.Rows[e.RowIndex]))
                        {
                            e.Cancel = true;
                            this.NioroshiIchiran.BeginEdit(false);
                            return;
                        }
                        break;
                    case 3:
                        // 荷降現場チェック
                        if (!this.logic.ChechNioroshiGenbaCd(this.NioroshiIchiran.Rows[e.RowIndex]))
                        {
                            e.Cancel = true;
                            this.NioroshiIchiran.BeginEdit(false);
                            return;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("NioroshiIchiran_CellValidating", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        // 20141015 koukouei 休動管理機能 end

        #region 20150925 hoanghm #13100

        private void syuusyuuDetailIchiran_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.syuusyuuDetailIchiranSoukei.Columns[e.Column.Index].Width = this.syuusyuuDetailIchiran.Columns[e.Column.Index].Width;
            this.syuusyuuDetailIchiranScroll.Columns[e.Column.Index].Width = this.syuusyuuDetailIchiran.Columns[e.Column.Index].Width;
        }

        #endregion

        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 収集明細更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void syuusyuuDetailIchiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            String columnName = this.syuusyuuDetailIchiran.Columns[e.ColumnIndex].Name;
            String cellValue = Convert.ToString(this.syuusyuuDetailIchiran[columnName, e.RowIndex].Value);
            var row = this.syuusyuuDetailIchiran.Rows[e.RowIndex];

            if (!this.logic.isInputError && (beforeValuesForDetail.ContainsKey(columnName) && beforeValuesForDetail[columnName] == cellValue))
            {
                return;
            }
            var cell = row.Cells[columnName];
            if (cell.ReadOnly)
            {
                return;
            }

            switch (columnName)
            {
                // 業者CD
                case "clmGYOUSHA_CD":
                    // 業者CD取得
                    var gyoushaCd = string.Empty;
                    if (row.Cells["clmGYOUSHA_CD"] != null && row.Cells["clmGYOUSHA_CD"].Value != DBNull.Value)
                    {
                        if (!string.IsNullOrEmpty(row.Cells["clmGYOUSHA_CD"].Value.ToString()))
                        {
                            row.Cells["clmGYOUSHA_CD"].Value = row.Cells["clmGYOUSHA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                            gyoushaCd = row.Cells["clmGYOUSHA_CD"].Value.ToString();

                            // 業者チェック
                            if (!this.logic.CheckGyoushaCd(row))
                            {
                                e.Cancel = true;
                                this.syuusyuuDetailIchiran.BeginEdit(false);
                            }
                            else
                            {
                                this.logic.isInputError = false;
                            }
                        }
                    }
                    else
                    {
                        row.Cells["clmGYOUSHA_NAME_RYAKU"].Value = string.Empty;
                    }

                    if (gyoushaCd != this.bakSSGyousyaCd || string.IsNullOrEmpty(gyoushaCd))
                    {
                        row.Cells["clmGENBA_CD"].Value = string.Empty;
                        row.Cells["clmGENBA_NAME_RYAKU"].Value = string.Empty;
                    }
                    break;
                // 現場CD
                case "clmGENBA_CD":
                    // 品名CD取得
                    var genbaCd = string.Empty;
                    if (row.Cells["clmGENBA_CD"] != null && row.Cells["clmGENBA_CD"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(row.Cells["clmGENBA_CD"].Value.ToString()))
                        {
                            row.Cells["clmGENBA_CD"].Value = row.Cells["clmGENBA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                            genbaCd = row.Cells["clmGENBA_CD"].Value.ToString();
                        }
                    }
                    if (genbaCd != this.bakSSGenbaCd || string.IsNullOrEmpty(genbaCd))
                    {
                        row.Cells["clmGENBA_NAME_RYAKU"].Value = string.Empty;
                    }

                    // 現場チェック
                    if (!this.logic.ChechiSSGenbaCd(this.syuusyuuDetailIchiran.Rows[e.RowIndex]))
                    {
                        e.Cancel = true;
                        this.syuusyuuDetailIchiran.BeginEdit(false);
                        return;
                    }
                    else
                    {
                        this.logic.isInputError = false;
                    }
                    break;
            }
        }
        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

        /// <summary>
        /// 卸降業者 PopupAfterExecuteMethod
        /// </summary>
        public void NIOROSHI_GYOUSHA_PopupAfterExecuteMethod()
        {
            if (this.NioroshiIchiran.CurrentRow == null) { return; }
            if (this.NioroshiIchiran.CurrentCell.ColumnIndex == 1)
            {
                curGyoushaCd = this.NioroshiIchiran.CurrentCell.EditedFormattedValue.ToString();
                if (preGyoushaCd != curGyoushaCd)
                {
                    this.NioroshiIchiran.CurrentRow.Cells["NIOROSHI_GENBA_CD"].Value = string.Empty;
                    this.NioroshiIchiran.CurrentRow.Cells["NIOROSHI_GENBA_NAME_RYAKU"].Value = string.Empty;
                }
            }
        }

        /// <summary>
        /// 卸降業者 PopupBeforeExecuteMethod
        /// </summary>
        public void NIOROSHI_GYOUSHA_PopupBeforeExecuteMethod()
        {
            if (this.NioroshiIchiran.CurrentRow == null) { return; }
            preGyoushaCd = string.Empty;
            if (this.NioroshiIchiran.CurrentCell.ColumnIndex == 1)
            {
                preGyoushaCd = this.NioroshiIchiran.CurrentCell.EditedFormattedValue.ToString();
            }
        }

        /// <summary>
        /// 業者 PopupAfterExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            if (this.DetailIchiran.CurrentRow == null) { return; }
            if (this.DetailIchiran.CurrentCell.ColumnIndex == 2)
            {
                curGyoushaCd = this.DetailIchiran.CurrentCell.EditedFormattedValue.ToString();
                if (preGyoushaCd != curGyoushaCd)
                {
                    this.DetailIchiran.CurrentRow.Cells["GENBA_CD"].Value = string.Empty;
                    this.DetailIchiran.CurrentRow.Cells["GENBA_NAME_RYAKU"].Value = string.Empty;
                }
            }
        }

        /// <summary>
        /// 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            if (this.DetailIchiran.CurrentRow == null) { return; }
            preGyoushaCd = string.Empty;
            if (this.DetailIchiran.CurrentCell.ColumnIndex == 2)
            {
                preGyoushaCd = this.DetailIchiran.CurrentCell.EditedFormattedValue.ToString();
            }
        }

        #region フォーカス取得処理
        /// <summary>
        /// フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Control_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Type type = sender.GetType();
            if (type.Name == "CustomAlphaNumTextBox")
            {
                CustomAlphaNumTextBox ctrl = (CustomAlphaNumTextBox)sender;
                if (dicControl.ContainsKey(ctrl.Name))
                {
                    dicControl[ctrl.Name] = ctrl.Text;
                }
                else
                {
                    dicControl.Add(ctrl.Name, ctrl.Text);
                }
            }
            else if (type.Name == "CustomNumericTextBox2")
            {
                CustomNumericTextBox2 ctrl = (CustomNumericTextBox2)sender;
                if (dicControl.ContainsKey(ctrl.Name))
                {
                    dicControl[ctrl.Name] = ctrl.Text;
                }
                else
                {
                    dicControl.Add(ctrl.Name, ctrl.Text);
                }
            }
            else if (type.Name == "CustomDateTimePicker")
            {
                CustomDateTimePicker ctrl = (CustomDateTimePicker)sender;
                if (dicControl.ContainsKey(ctrl.Name))
                {
                    dicControl[ctrl.Name] = ctrl.Text;
                }
                else
                {
                    dicControl.Add(ctrl.Name, ctrl.Text);
                }
            }
        }

        /// <summary>
        /// コントロールに入力された値をクリアします
        /// </summary>
        /// <param name="key">コントロール名</param>
        internal void ClearBeforeText(string key)
        {
            if (dicControl.ContainsKey(key))
            {
                dicControl[key] = string.Empty;
            }
        }

        /// <summary>
        /// コントロールに入力されていた値を取得します
        /// </summary>
        /// <param name="key">コントロール名</param>
        /// <returns>前回値</returns>
        internal String GetBeforeText(string key)
        {
            LogUtility.DebugMethodStart(key);

            string ret = this.dicControl.Where(r => r.Key == key).Select(r => r.Value).DefaultIfEmpty(String.Empty).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        private void FURIKAE_HAISHA_KBN_TextChanged(object sender, EventArgs e)
        {
            this.COURSE_NAME_CD.Text = string.Empty;
            this.COURSE_NAME_RYAKU.Text = string.Empty;
            this.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            this.SHASHU_CD.Text = string.Empty;
            this.SHASHU_NAME_RYAKU.Text = string.Empty;
            this.SHARYOU_CD.Text = string.Empty;
            this.SHARYOU_NAME_RYAKU.Text = string.Empty;
            this.UNTENSHA_CD.Text = string.Empty;
            this.UNTENSHA_NAME.Text = string.Empty;
            this.UNTENSHA_CD.Text = string.Empty;
            this.UNTENSHA_NAME.Text = string.Empty;
            this.HOJOIN_CD.Text = string.Empty;
            this.HOJOIN_NAME.Text = string.Empty;
            this.NioroshiIchiran.Rows.Clear();
            this.DetailIchiran.Rows.Clear();
            if (string.IsNullOrEmpty(this.SAGYOU_DATE.Text))
            {
                this.SAGYOU_DATE.Value = ((BusinessBaseForm)this.ParentForm).sysDate;
            }
            this.logic.PopUpDataInit();
        }

        private void DetailIchiran_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.logic.Ichiran_RowsAdded();
        }

        public void PopupAfter()
        {
            switch (this.DAY_NM.Text)
            {
                case "月":
                    this.DAY_CD.Text = "1";
                    break;
                case "火":
                    this.DAY_CD.Text = "2";
                    break;
                case "水":
                    this.DAY_CD.Text = "3";
                    break;
                case "木":
                    this.DAY_CD.Text = "4";
                    break;
                case "金":
                    this.DAY_CD.Text = "5";
                    break;
                case "土":
                    this.DAY_CD.Text = "6";
                    break;
                case "日":
                    this.DAY_CD.Text = "7";
                    break;
            }
        }

        /// <summary>
        /// 運搬業者をクリアしても、車輌の情報もクリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                this.SHARYOU_CD.Text = string.Empty;
                this.SHARYOU_NAME_RYAKU.Text = string.Empty;
                dicControl["SHARYOU_CD"] = string.Empty;
            }
        }
    }
}
