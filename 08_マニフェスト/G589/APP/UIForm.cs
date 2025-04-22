using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;
using System.ComponentModel;
using Shougun.Core.Common.BusinessCommon.Logic;
using System.Drawing;
using r_framework.Logic;

namespace Shougun.Core.PaperManifest.Himodukeichiran
{
    [Implementation]
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region フィールド

        private Himodukeichiran.LogicClass HimodukeichiranLogic;

        private string selectQuery = string.Empty;

        private string orderQuery = string.Empty;

        private string joinQuery = string.Empty;

        UIHeader header_new;

        private Boolean isLoaded;

        // 20140618 ria EV004875 マニフェスト入力の[F7]状況ボタンを押下時に伝票紐付一覧が開く start
        public int formManiFlag = 1;
        public string formHaikiKbn = "5";
        // 20140618 ria EV004875 マニフェスト入力の[F7]状況ボタンを押下時に伝票紐付一覧が開く end

        // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 start
        /// <summary>メッセージクラス</summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }
        // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 end

        /// <summary>
        /// ベースロジックで作成したSELECTクエリ
        /// </summary>
        internal string baseSelectQuery = string.Empty;

        /// <summary>
        /// ベースロジックで作成したORDER BYクエリ
        /// </summary>
        internal string baseOrderByQuery = string.Empty;

        /// <summary>
        /// ベースロジックで作成したJOINクエリ
        /// </summary>
        internal string baseJoinQuery = string.Empty;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private string befGyousha = string.Empty;
        private string befGenba = string.Empty;

        #region 出力パラメータ

        /// <summary>
        /// システムID
        /// </summary>
        public String ParamOut_SysID { get; set; }

        /// <summary>
        /// モード
        /// </summary>
        public Int32 ParamOut_WinType { get; set; }

        #endregion

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="window_type">画面区分</param>
        /// <param name="RDentatuKbn">連携伝達区分</param>
        /// <param name="SystemId">システムID</param>
        /// <param name="RMeisaiId">連携明細システムID</param>
        /// <param name="iMode">処理モード</param>
        public UIForm()
            : base(DENSHU_KBN.MANIFEST_HIMODUKE_ICHIRAN, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.HimodukeichiranLogic = new LogicClass(this);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;

            // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 start
            //メッセージクラス
            this.messageShowLogic = new MessageBoxShowLogic();
            // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 end

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
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

            // 画面情報の初期化
            if (!isLoaded)
            {
                if (!this.HimodukeichiranLogic.WindowInit()) { return; }

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(4, 161);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(4, 183);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 220);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 250);

                // キー入力設定
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;

                // ヘッダーフォームを取得
                this.header_new = (UIHeader)this.ParentBaseForm.headerForm;

                // 非表示列登録
                this.SetHiddenColumns(this.HimodukeichiranLogic.HIDDEN_HAIKI_KBN_CD_IJI,
                    this.HimodukeichiranLogic.HIDDEN_HAIKI_KBN_CD_NIJI,
                    this.HimodukeichiranLogic.HIDDEN_SYSTEM_ID_IJI,
                    this.HimodukeichiranLogic.HIDDEN_SYSTEM_ID_NIJI,
                    this.HimodukeichiranLogic.HIDDEN_SEQ_IJI,
                    this.HimodukeichiranLogic.HIDDEN_SEQ_NIJI,
                    this.HimodukeichiranLogic.HIDDEN_KANRI_ID_IJI,
                    this.HimodukeichiranLogic.HIDDEN_KANRI_ID_NIJI,
                    this.HimodukeichiranLogic.HIDDEN_LATEST_SEQ_IJI,
                    this.HimodukeichiranLogic.HIDDEN_LATEST_SEQ_NIJI);

                //表示の初期化
                this.HimodukeichiranLogic.ClearScreen("Initial");

                // 汎用検索は一旦廃止
                this.searchString.Visible = false;
            }

            isLoaded = true;
            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
            }

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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

        #region 画面の全てのTEXTBOXの内容をクリアする。
        /// <summary>
        /// 画面の全てのTEXTBOXの内容をクリアする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ControlLoopDel(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is CustomAlphaNumTextBox)
                {
                    CustomAlphaNumTextBox pb = (CustomAlphaNumTextBox)control;
                    pb.Clear();
                }
                else if (control is System.Windows.Forms.Panel)
                {
                    this.ControlLoopDel(((System.Windows.Forms.Panel)control).Controls);
                }
            }
        }
        #endregion

        #region 画面クリア処理
        /// <summary>
        /// 画面クリア処理
        /// </summary>
        /// <param name="e"></param>
        internal void ClearScreen(EventArgs e)
        {
            LogUtility.DebugMethodStart();

            //base.OnLoad(e);
            if (!this.HimodukeichiranLogic.WindowInit()) { return; }

            //テキストボックスの削除
            this.ControlLoopDel(this.Controls);

            //グリッドの削除
            this.customDataGridView1.Rows.Clear();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面コントロールイベント
        /// <summary>
        /// 抽出業者TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_ChushutuGyosha_TextChanged(object sender, EventArgs e)
        {
            //抽出業者が運搬受託者の場合は、現場をグレーアウトにします。
            this.cantxt_GenbaCd.Enabled = this.txtNum_ChushutuGyosha.Text != "2";
            this.cbtn_GenbaSan.Enabled = this.txtNum_ChushutuGyosha.Text != "2";
            if (this.txtNum_ChushutuGyosha.Text == "2")
            {
                //抽出業者が運搬受託者の場合は、現場をクリアします。
                this.cantxt_GenbaCd.Text = string.Empty;
                this.ctxt_GenbaName.Text = string.Empty;
            }

            // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 start
            //ポップアップ設定
            this.HimodukeichiranLogic.SetFilteringPopupData();
            // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 end
        }

        /// <summary>
        /// 新規(F2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func2_Click(object sender, EventArgs e)
        {
            this.HimodukeichiranLogic.FormChanges(WINDOW_TYPE.NEW_WINDOW_FLAG);
        }

        /// <summary>
        /// 修正(F3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func3_Click(object sender, EventArgs e)
        {
            this.HimodukeichiranLogic.FormChanges(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
        }

        /// <summary>
        /// 削除(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func4_Click(object sender, EventArgs e)
        {
            this.HimodukeichiranLogic.FormChanges(WINDOW_TYPE.DELETE_WINDOW_FLAG);
        }

        /// <summary>
        /// CSV出力(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
        {
            CSVExport CSVExp = new CSVExport();
            CSVExp.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, DENSHU_KBNExt.ToTitleString(DENSHU_KBN.MANIFEST_HIMODUKE_ICHIRAN), this);
        }

        /// <summary>
        /// 条件ｸﾘｱ(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {
            this.HimodukeichiranLogic.ClearScreen("ClsSearchCondition");
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            //必須チェック
            if (this.HimodukeichiranLogic.SearchCheck())
            {
                return;
            }

            //結果表示を初期化　//すぐには初期化されない
            this.HimodukeichiranLogic.IntializeResultForm();

            //switch (this.HimodukeichiranLogic.Search()) //こちらを有効にすると改善前に戻る
            switch (this.HimodukeichiranLogic.SearchVer02()) //sumi 2020/08/21 SQL速度改善版
            {
                case 0:
                    //検索結果がない場合、メッセージ「検索結果が存在しませんでした。」を表示する
                    MessageBoxUtility.MessageBoxShow("C001");
                    break;
                case -1:
                    //異常
                    break;
            }
        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
            this.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func11_Click(object sender, EventArgs e)
        {
            this.customSearchHeader1.ShowCustomSearchSettingDialog();

            if (this.customDataGridView1 != null)
            {
                this.header_new.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.header_new.ReadDataNumber.Text = "0";
            }
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.customDataGridView1.DataSource = "";

            this.Close();
            parentForm.Close();
        }

        //VAN 20210507 #148581 S
        /// <summary>
        /// [1]一次マニ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.customDataGridView1.CurrentRow == null)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxUtility.MessageBoxShow("E051", "表示するマニフェスト情報");
                    return;
                }

                this.HimodukeichiranLogic.FormChangesByManiKbn(true);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// [2]二次マニ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.customDataGridView1.CurrentRow == null)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxUtility.MessageBoxShow("E051", "表示するマニフェスト情報");
                    return;
                }

                this.HimodukeichiranLogic.FormChangesByManiKbn(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process2_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        //VAN 20210507 #148581 E

        /// <summary>
        /// ESCキー押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            if (e.KeyCode == Keys.Escape)
            {
                //処理No(ESC)へカーソル移動
                parentForm.txb_process.Focus();

            }
        }

        /// <summary>
        /// 明細データダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.HimodukeichiranLogic.FormChanges(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
        }

        /// <summary>
        /// 明細データセルフォーマット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 減容後数量（1次）
            if (e.ColumnIndex == this.customDataGridView1.Columns[this.HimodukeichiranLogic.GENNYOU_SU_IJI].Index)
            {
                var v = this.customDataGridView1[this.HimodukeichiranLogic.GENNYOU_SU_IJI, e.RowIndex].Value;
                decimal gennyouSuu;
                if (v != null && !string.IsNullOrEmpty(v.ToString()) && Decimal.TryParse(v.ToString(), out gennyouSuu))
                {
                    e.Value = gennyouSuu.ToString(this.HimodukeichiranLogic.sysInfo.MANIFEST_SUURYO_FORMAT);
                }
            }

            // 換算後数量（2次）
            if (e.ColumnIndex == this.customDataGridView1.Columns[this.HimodukeichiranLogic.KANSAN_SU_NIJI].Index)
            {
                var v = this.customDataGridView1[this.HimodukeichiranLogic.KANSAN_SU_NIJI, e.RowIndex].Value;
                decimal kansanSuu;
                if (v != null && !string.IsNullOrEmpty(v.ToString()) && Decimal.TryParse(v.ToString(), out kansanSuu))
                {
                    e.Value = kansanSuu.ToString(this.HimodukeichiranLogic.sysInfo.MANIFEST_SUURYO_FORMAT);
                }
            }
        }

        /// <summary>
        /// 業者CD Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_GyousyaCd_Validating()
        {
            this.cantxt_GyousyaCd_PopupAfterExecuteMethod();

        }

        /// <summary>
        /// 業者CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_GyousyaCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 start
            ////業者CDが空の場合、業者名をクリアする
            //if (string.IsNullOrEmpty(this.cantxt_GyousyaCd.Text))
            //{
            //    this.ctxt_GyousyaName.Text = string.Empty;
            //}
            ////業者CDが変更した場合は現場CDと現場名をクリアする
            //this.cantxt_GenbaCd.Text = string.Empty;
            //this.ctxt_GenbaName.Text = string.Empty;

            string returnFlg = this.HimodukeichiranLogic.SetCheckGyoushaItem();

            //事業者チェック
            switch (this.HimodukeichiranLogic.ChkGyosya(this.cantxt_GyousyaCd, returnFlg))
            {
                case 0://正常
                    break;

                case 1://空
                    this.ctxt_GyousyaName.Text = string.Empty;

                    //業者CDが変更した場合は現場CDと現場名をクリアする
                    if (this.befGyousha != this.cantxt_GyousyaCd.Text)
                    {
                        this.cantxt_GenbaCd.Text = string.Empty;
                        this.ctxt_GenbaName.Text = string.Empty;
                    }

                    return;

                case 2://エラー
                    return;
            }

            switch (this.txtNum_ChushutuGyosha.Text)
            {
                case "1":
                    //排出
                    ManifestoLogic.SetAddrGyousha("All", cantxt_GyousyaCd, ctxt_GyousyaName,
                        null, null, null,
                        null, null, null,
                        true, false, false, false, true);
                    break;

                case "2":
                    //運搬
                    ManifestoLogic.SetAddrGyousha("All", cantxt_GyousyaCd, ctxt_GyousyaName,
                        null, null, null,
                        null, null, null,
                        false, false, true, false, true);
                    break;

                case "3":
                //処分
                case "4":
                    //最終処分
                    ManifestoLogic.SetAddrGyousha("All", cantxt_GyousyaCd, ctxt_GyousyaName,
                        null, null, null,
                        null, null, null,
                        false, true, false, false, true);
                    break;
            }
            // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 end
        }

        /// <summary>
        /// 現場CD Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_GenbaName_Validating()
        {
            this.cantxt_GenbaName_PopupAfterExecuteMethod();
        }

        /// <summary>
        /// 現場CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_GenbaCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 start

            ////現場CDが空の場合、現場名をクリアする
            //if (string.IsNullOrEmpty(this.cantxt_GenbaCd.Text))
            //{
            //    this.ctxt_GenbaName.Text = string.Empty;
            //}

            string returnFlg = this.HimodukeichiranLogic.SetCheckGenbanItem();

            switch (this.HimodukeichiranLogic.ChkJigyouba(this.cantxt_GenbaCd, this.cantxt_GyousyaCd, returnFlg))
            {
                case 0://正常

                    break;

                case 1://空
                    //現場CDが空の場合、現場名をクリアする
                    this.ctxt_GenbaName.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }


            //業者　設定
            switch (this.txtNum_ChushutuGyosha.Text)
            {
                case "1":
                    //排出
                    ManifestoLogic.SetAddrGyousha("All", cantxt_GyousyaCd, ctxt_GyousyaName,
                        null, null, null,
                        null, null, null,
                        true, false, false, false, true);

                    //事業場　設定
                    this.HimodukeichiranLogic.SetAddressJigyouba("All", cantxt_GyousyaCd, cantxt_GenbaCd, ctxt_GenbaName, true, false, false, false, true);

                    break;

                case "2":
                    //運搬
                    ManifestoLogic.SetAddrGyousha("All", cantxt_GyousyaCd, ctxt_GyousyaName,
                        null, null, null,
                        null, null, null,
                        false, false, true, false, true);
                    break;

                case "3":
                    //処分
                    ManifestoLogic.SetAddrGyousha("All", cantxt_GyousyaCd, ctxt_GyousyaName,
                        null, null, null,
                        null, null, null,
                        false, true, false, false, true);

                    //事業場　設定
                    this.HimodukeichiranLogic.SetAddressJigyouba("All", cantxt_GyousyaCd, cantxt_GenbaCd, ctxt_GenbaName, false, false, true, false, true);

                    break;
                case "4":
                    //最終処分
                    ManifestoLogic.SetAddrGyousha("All", cantxt_GyousyaCd, ctxt_GyousyaName,
                        null, null, null,
                        null, null, null,
                        false, true, false, false, true);

                    //事業場　設定
                    this.HimodukeichiranLogic.SetAddressJigyouba("All", cantxt_GyousyaCd, cantxt_GenbaCd, ctxt_GenbaName, false, true, false, false, true);

                    break;
            }
            // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 end

        }

        /// <summary>
        /// 抽出対象区分 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_ChushutuTaishouKbn_Validated(object sender, EventArgs e)
        {
            CustomNumericTextBox2 text = sender as CustomNumericTextBox2;
            if (string.IsNullOrEmpty(text.Text))
            {
                //抽出対象区分が空の場合、メッセージ「抽出対象区分は必須項目です。入力してください。」を表示する
                MessageBoxUtility.MessageBoxShow("E001", text.DisplayItemName);
                //フォーカスを抽出対象区分へ移動
                text.Select();
            }
        }

        /// <summary>
        /// 紐付状況 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_HimodukeJyoukyou_Validated(object sender, EventArgs e)
        {
            CustomNumericTextBox2 text = sender as CustomNumericTextBox2;
            if (string.IsNullOrEmpty(text.Text))
            {
                //紐付状況が空の場合、メッセージ「紐付状況は必須項目です。入力してください。」を表示する
                MessageBoxUtility.MessageBoxShow("E001", text.DisplayItemName);
                //フォーカスを紐付状況へ移動
                text.Select();
            }
        }

        /// <summary>
        /// マニフェスト種類 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_ManifestShurui_Validated(object sender, EventArgs e)
        {
            CustomNumericTextBox2 text = sender as CustomNumericTextBox2;
            if (string.IsNullOrEmpty(text.Text))
            {
                //マニフェスト種類が空の場合、メッセージ「マニフェスト種類は必須項目です。入力してください。」を表示する
                MessageBoxUtility.MessageBoxShow("E001", text.DisplayItemName);
                //フォーカスをマニフェスト種類へ移動
                text.Select();
            }
        }

        /// <summary>
        /// 抽出日付 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_ChushutuHiduke_Validated(object sender, EventArgs e)
        {
            CustomNumericTextBox2 text = sender as CustomNumericTextBox2;
            if (string.IsNullOrEmpty(text.Text))
            {
                //抽出日付が空の場合、メッセージ「抽出日付は必須項目です。入力してください。」を表示する
                MessageBoxUtility.MessageBoxShow("E001", text.DisplayItemName);
                //フォーカスを抽出日付へ移動
                text.Select();
            }
        }

        /// <summary>
        /// 抽出業者 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_ChushutuGyosha_Validated(object sender, EventArgs e)
        {
            CustomNumericTextBox2 text = sender as CustomNumericTextBox2;
            if (string.IsNullOrEmpty(text.Text))
            {
                //抽出業者が空の場合、メッセージ「抽出業者は必須項目です。入力してください。」を表示する
                MessageBoxUtility.MessageBoxShow("E001", text.DisplayItemName);
                //フォーカスを抽出業者へ移動
                text.Select();
            }
        }

        /// <summary>
        /// 抽出対象区分 TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_ChushutuTaishouKbn_TextChanged(object sender, EventArgs e)
        {
            switch (this.txtNum_ChushutuTaishouKbn.Text)
            {
                case "1":
                    Color color_iji = Color.FromArgb(0, 105, 51);
                    this.HimodukeichiranLogic.SetColor(color_iji);
                    break;
                case "2":
                    Color color_niji = Color.FromArgb(0, 51, 160);
                    this.HimodukeichiranLogic.SetColor(color_niji);
                    break;
            }
        }
        #endregion

        // 20140605 katen 不具合No.4690 start‏
        #region 交付番号
        /// <summary>
        /// 交付番号 Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KohuNo_Validating(object sender, CancelEventArgs e)
        {
            //string isNotExist = string.Empty;

            if (this.HimodukeichiranLogic.ChkKohuNo())
            {
                e.Cancel = true;
                return;
            }

            //交付番号存在チェック
            string SystemId = string.Empty;
            string Seq = string.Empty;
            string SeqRD = string.Empty;

            if (this.txtNum_ChushutuTaishouKbn.Text != "5")
            {
                if (this.HimodukeichiranLogic.ExistKohuNo(this.txtNum_ChushutuTaishouKbn.Text, this.cantxt_KohuNo.Text, ref SystemId, ref Seq, ref SeqRD))
                {
                    return;
                }
            }
        }

        #endregion
        // 20140605 katen 不具合No.4690 end‏

        #region 業者CD
        /// <summary>
        /// 業者CD PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_GyousyaCd_PopupAfterExecuteMethod()
        {
            if (this.cantxt_GyousyaCd.Text != this.befGyousha)
            {
                this.cantxt_GenbaCd.Text = string.Empty;
                this.ctxt_GenbaName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 業者CD PopupBeforeExecuteMethod
        /// </summary>
        public void cantxt_GyousyaCd_PopupBeforeExecuteMethod()
        {
            this.befGyousha = this.cantxt_GyousyaCd.Text;
        }
        #endregion

        /// <summary>
        /// 現場CD PopupAfterExecuteMethod
        /// </summary>
        public void cantxt_GenbaName_PopupAfterExecuteMethod()
        {

        }

        #region 検索結果表示
        /// <summary>
        /// 画面表示
        /// </summary>
        public virtual void ShowData()
        {
            this.Table = this.HimodukeichiranLogic.searchResult;

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
            }
        }
        #endregion

        #region 値保持
        /// <summary>
        /// Enter時の値保持
        /// </summary>
        private Dictionary<Control, string> _EnterValue = new Dictionary<Control, string>();

        private object lastObject = null;

        internal void EnterEventInit()
        {
            foreach (var c in controlUtil.GetAllControls(this.Parent))
            {
                c.Enter += new EventHandler(this.SaveTextOnEnter);
            }
        }

        /// <summary>
        /// Enter時　入力値保存
        /// </summary>
        /// <param name="value"></param>
        private void SaveTextOnEnter(object sender, EventArgs e)
        {
            var value = sender as Control;

            if (value == null)
            {
                return;
            }

            //エラー等でフォーカス移動しなかった場合は、値クリアして強制チェックするようにする。 
            // ※1（正常）→0（エラー）→1と入れた場合 チェックする。
            // ※※この処理がない場合、0（エラー）→0（ノーチェック）となってしまう。
            if (lastObject == sender)
            {
                if (_EnterValue.ContainsKey(value))
                {
                    _EnterValue[value] = null;
                }
                else
                {
                    _EnterValue.Add(value, null);
                }

                return;

            }

            this.lastObject = sender;

            if (_EnterValue.ContainsKey(value))
            {
                _EnterValue[value] = value.Text;
            }
            else
            {
                _EnterValue.Add(value, value.Text);
            }
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, value.Text); //一致する場合変更なし
        }
        /// <summary>
        /// 値比較時
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal string get_EnterValue(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return null;
            }
            return _EnterValue[value];
        }
        #endregion

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        }
        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end
    }
}
