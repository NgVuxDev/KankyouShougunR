using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.Const;
using Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.Logic;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Dto;
using r_framework.FormManager;

namespace Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.APP
{
    /// <summary>
    /// ○○入力画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド・プロパティ

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        ///
        /// </summary>
        internal MessageBoxShowLogic MsgLogic { get { return this.msglogic; } }
        private MessageBoxShowLogic msglogic;

        /// <summary>
        /// 共通情報(SysInfoなど)
        /// </summary>
        internal CommonInformation CommInfo { get; private set; }

        /// <summary>
        /// 親フォーム
        /// </summary>
        internal BusinessBaseForm BaseForm { get; private set; }

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        internal UIHeaderForm HeaderForm { get; private set; }

        /// <summary>
        ///
        /// </summary>
        internal long DenpyouNo { get; private set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private bool errorFlag = false;

        internal bool bCancelDenpyoPopup = false;

        internal bool isHinmeiReLoad = false;

        private bool editingMultiRowFlag = false;

        private string oldDenshuKbn = "";

        private bool clickLock = false;

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        private Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>新規用</remarks>
        public UIForm()
            : base(WINDOW_ID.T_DENPYOU_DETAIL_IKKATU_UPDATE, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.InitializeComponent();

                CommonShogunData.Create(SystemProperty.Shain.CD);
                // メッセージロジック
                this.msglogic = new MessageBoxShowLogic();
                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);

                // 完全に固定
                // ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region イベント

        #region オーバーライド

        #region 画面ロード

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            try
            {
                this.BaseForm = this.Parent as BusinessBaseForm;
                this.HeaderForm = this.BaseForm.headerForm as UIHeaderForm;
                this.CommInfo = (this.BaseForm.ribbonForm as RibbonMainMenu).GlobalCommonInformation;

                // 画面情報の初期化
                this.logic.WindowInit();
                base.HeaderFormInit();

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.mrDetail != null)
                {
                    this.mrDetail.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面表示時の制御
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);

            if (!isShown)
            {
                this.Height -= 7;
                this.isShown = true;
            }
        }

        #endregion

        #endregion

        #region ファンクションボタン

        /// <summary>
        /// [F1]一括入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        public virtual void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool paramCheckFlg = true;

            if (this.mrDetail.RowCount < 1)
            {
                this.msglogic.MessageBoxShow("E051", "一括入力する伝票データ");
                return;
            }

            bool checkFlg = false;
            foreach (Row dgvRow in this.mrDetail.Rows)
            {
                if (dgvRow.Cells[ConstCls.COLUMN_CHECK].Value != null && (bool)dgvRow.Cells[ConstCls.COLUMN_CHECK].Value == true)
                {
                    checkFlg = true;
                    break;
                }
            }
            if (!checkFlg)
            {
                this.msglogic.MessageBoxShow("E051", "一括入力する伝票データ");
                return;
            }

            paramCheckFlg = this.logic.ShowPopUp();
            if (!paramCheckFlg)
            {
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F7]条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.ControlInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F8]検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_TO.IsInputErrorOccured = false;

            Control[] allControlControls = { this.HIDUKE_FROM, this.HIDUKE_TO, this.KYOTEN_CD, this.txtKakuteiKbn, this.txtDenshuKbn };
            var autoCheckLogic = new AutoRegistCheckLogic(allControlControls, allControlControls);
            this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (this.RegistErrorFlag)
            {
                //必須チェックエラーフォーカス処理
                this.logic.SetErrorFocus();
                return;
            }

            DateTime dtpFrom = DateTime.Parse(this.HIDUKE_FROM.GetResultText());
            DateTime dtpTo = DateTime.Parse(this.HIDUKE_TO.GetResultText());
            DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
            DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

            int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

            if (0 < diff)
            {
                //対象期間内でないならエラーメッセージ表示
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                this.HIDUKE_FROM.IsInputErrorOccured = true;
                this.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.HIDUKE_TO.IsInputErrorOccured = true;
                this.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                this.msglogic.MessageBoxShow("E030", "伝票日付From", "伝票日付To");
                this.HIDUKE_FROM.Select();
                this.HIDUKE_FROM.Focus();
                return;
            }

            this.logic.map.Clear();
            // 明細クリア
            this.mrDetail.Rows.Clear();

            if (this.txtDenshuKbn.Text == "1")
            {
                this.mrDetailUkeireTemplate.Row.Cells[ConstCls.COLUMN_HINMEI_CD].ReadOnly = !this.HeaderForm.cbx_Hinmei.Checked;
                this.mrDetailUkeireTemplate.Row.Cells[ConstCls.COLUMN_HINMEI_NAME].ReadOnly = !this.HeaderForm.cbx_Hinmei.Checked;
                this.mrDetailUkeireTemplate.Row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].ReadOnly = !this.HeaderForm.cbx_MeisaiBikou.Checked;
                this.mrDetailUkeireTemplate.Row.Cells[ConstCls.COLUMN_SUURYOU].ReadOnly = !this.HeaderForm.cbx_Suuryou.Checked;
                this.mrDetailUkeireTemplate.Row.Cells[ConstCls.COLUMN_UNIT_CD].ReadOnly = !this.HeaderForm.cbx_Unit.Checked;
                this.mrDetailUkeireTemplate.Row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = !this.HeaderForm.cbx_Tanka.Checked;
                this.mrDetailUkeireTemplate.Row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = !this.HeaderForm.cbx_Tanka.Checked;
                this.mrDetail.Template = this.mrDetailUkeireTemplate;
            }
            else if (this.txtDenshuKbn.Text == "2")
            {
                this.mrDetailShukkaTemplate.Row.Cells[ConstCls.COLUMN_HINMEI_CD].ReadOnly = !this.HeaderForm.cbx_Hinmei.Checked;
                this.mrDetailShukkaTemplate.Row.Cells[ConstCls.COLUMN_HINMEI_NAME].ReadOnly = !this.HeaderForm.cbx_Hinmei.Checked;
                this.mrDetailShukkaTemplate.Row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].ReadOnly = !this.HeaderForm.cbx_MeisaiBikou.Checked;
                this.mrDetailShukkaTemplate.Row.Cells[ConstCls.COLUMN_SUURYOU].ReadOnly = !this.HeaderForm.cbx_Suuryou.Checked;
                this.mrDetailShukkaTemplate.Row.Cells[ConstCls.COLUMN_UNIT_CD].ReadOnly = !this.HeaderForm.cbx_Unit.Checked;
                this.mrDetailShukkaTemplate.Row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = !this.HeaderForm.cbx_Tanka.Checked;
                this.mrDetailShukkaTemplate.Row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = !this.HeaderForm.cbx_Tanka.Checked;
                this.mrDetail.Template = this.mrDetailShukkaTemplate;
            }
            else if (this.txtDenshuKbn.Text == "3")
            {
                this.mrDetailUrshTemplate.Row.Cells[ConstCls.COLUMN_HINMEI_CD].ReadOnly = !this.HeaderForm.cbx_Hinmei.Checked;
                this.mrDetailUrshTemplate.Row.Cells[ConstCls.COLUMN_HINMEI_NAME].ReadOnly = !this.HeaderForm.cbx_Hinmei.Checked;
                this.mrDetailUrshTemplate.Row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].ReadOnly = !this.HeaderForm.cbx_MeisaiBikou.Checked;
                this.mrDetailUrshTemplate.Row.Cells[ConstCls.COLUMN_SUURYOU].ReadOnly = !this.HeaderForm.cbx_Suuryou.Checked;
                this.mrDetailUrshTemplate.Row.Cells[ConstCls.COLUMN_UNIT_CD].ReadOnly = !this.HeaderForm.cbx_Unit.Checked;
                this.mrDetailUrshTemplate.Row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = !this.HeaderForm.cbx_Tanka.Checked;
                this.mrDetailUrshTemplate.Row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = !this.HeaderForm.cbx_Tanka.Checked;
                this.mrDetail.Template = this.mrDetailUrshTemplate;
            }

            int Count = this.logic.Search();

            if (Count == 0)
            {
                this.msglogic.MessageBoxShow("C001");
                return;
            }

            this.logic.DisplayEntitesToForm();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F9]登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// 表示中のデータの新規登録、修正又は削除を行い、DBに反映する
        /// 登録完了後、データを再検索し表示する
        /// </remarks>
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var allControlAndHeaderControls = this.controlUtil.GetAllControls(this.HeaderForm).ToList();
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                if (this.RegistErrorFlag)
                {
                    //必須チェックエラーフォーカス処理
                    this.logic.SetErrorFocus();
                    return;
                }

                if (this.HeaderForm.txtCsvOutputKbn.Text == "1")
                {
                    if (string.IsNullOrEmpty(this.logic.csvPath))
                    {
                        this.msglogic.MessageBoxShow("E274");
                        return;
                    }
                    else
                    {
                        if (!Directory.Exists(this.logic.csvPath))
                        {
                            this.msglogic.MessageBoxShow("E274");
                            return;
                        }
                    }
                }

                if (this.mrDetail.RowCount < 1)
                {
                    this.msglogic.MessageBoxShow("E051", "更新する伝票データ");
                    return;
                }

                bool checkFlg = false;
                List<string> messageList = new List<string>();
                foreach (Row dgvRow in this.mrDetail.Rows)
                {
                    foreach (Cell cell in dgvRow.Cells)
                    {
                        cell.UpdateBackColor(cell.Selected);
                    }
                    if (dgvRow.Cells[ConstCls.COLUMN_CHECK].Value != null && (bool)dgvRow.Cells[ConstCls.COLUMN_CHECK].Value == true)
                    {
                        checkFlg = true;
                        if (this.HeaderForm.cbx_Hinmei.Checked)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value)))
                            {
                                dgvRow.Cells[ConstCls.COLUMN_HINMEI_CD].Style.BackColor = Constans.ERROR_COLOR;
                                var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                                var msgStr = msg.Text.Replace("{0}", "品名CD");
                                if (!messageList.Contains(msgStr))
                                {
                                    messageList.Add(msgStr);
                                }
                            }
                            if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_HINMEI_NAME].Value)))
                            {
                                dgvRow.Cells[ConstCls.COLUMN_HINMEI_CD].Style.BackColor = Constans.ERROR_COLOR;
                                var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                                var msgStr = msg.Text.Replace("{0}", "品名");
                                if (!messageList.Contains(msgStr))
                                {
                                    messageList.Add(msgStr);
                                }
                            }
                            if (this.logic.dto.denshuKbnCd == "1" || this.logic.dto.denshuKbnCd == "2")
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_URIAGE_DATE].Value)) && Convert.ToInt16(dgvRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value) == 1)
                                {
                                    messageList.Add("売上日付が入力しなかったので、伝票区分に売上を設定できません。");
                                }
                                if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value)) && Convert.ToInt16(dgvRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value) == 2)
                                {
                                    messageList.Add("支払日付が入力しなかったので、伝票区分に支払を設定できません。");
                                }
                            }
                        }
                        if (this.HeaderForm.cbx_Suuryou.Checked)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_SUURYOU].Value)))
                            {
                                dgvRow.Cells[ConstCls.COLUMN_SUURYOU].Style.BackColor = Constans.ERROR_COLOR;
                                var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                                var msgStr = msg.Text.Replace("{0}", "数量");
                                if (!messageList.Contains(msgStr))
                                {
                                    messageList.Add(msgStr);
                                }
                            }
                        }
                        if (this.HeaderForm.cbx_Unit.Checked)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_UNIT_CD].Value)))
                            {
                                dgvRow.Cells[ConstCls.COLUMN_UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
                                var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                                var msgStr = msg.Text.Replace("{0}", "単位");
                                if (!messageList.Contains(msgStr))
                                {
                                    messageList.Add(msgStr);
                                }
                            }
                        }
                        if (this.HeaderForm.cbx_Tanka.Checked)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value)))
                            {
                                dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].Style.BackColor = Constans.ERROR_COLOR;
                                var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                                var msgStr = msg.Text.Replace("{0}", "金額");
                                if (!messageList.Contains(msgStr))
                                {
                                    messageList.Add(msgStr);
                                }
                            }
                        }
                    }
                }
                if (messageList.Count > 0)
                {
                    var result = new StringBuilder(256);
                    for (int i = 0; i < messageList.Count; i++)
                    {
                        result.AppendLine(messageList[i]);
                    }
                    MessageBox.Show(result.ToString(), Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!checkFlg)
                {
                    this.msglogic.MessageBoxShow("E051", "更新する伝票データ");
                    return;
                }

                this.setFunctionEnabled(false);
                if (this.logic.CreateEntites())
                {
                    this.logic.Regist(errorFlag);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                this.setFunctionEnabled(true);
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>表示中のデータに対する修正を全て取消し、画面を閉じる</remarks>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            var parentForm = (BusinessBaseForm)this.Parent;
            parentForm.Close();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [1]伝票一括更新ボタンクリック
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            FormManager.OpenFormWithAuth("G684", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            LogUtility.DebugMethodEnd();
        }

        private void setFunctionEnabled(bool enabled)
        {
            this.BaseForm.bt_func1.Enabled = enabled;
            this.BaseForm.bt_func7.Enabled = enabled;
            this.BaseForm.bt_func8.Enabled = enabled;
            this.BaseForm.bt_func9.Enabled = enabled;
            this.BaseForm.bt_func12.Enabled = enabled;
            this.BaseForm.bt_process1.Enabled = enabled;
        }
        #endregion

        #region 取引先イベント

        /// <summary>
        /// 取引先プップでデータを選択後処理
        /// </summary>
        public void TORIHIKISAKI_CD_PopupAfter()
        {
            this.txtControl_Enter(this.TORIHIKISAKI_CD, null);
        }

        /// <summary>
        /// 取引先更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TORIHIKISAKI_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // チェック処理
                if (!this.logic.CheckTorihikisaki())
                {
                    e.Cancel = true;
                    this.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 業者イベント
        /// <summary>
        /// 業者プップ表示前処理
        /// </summary>
        public void GYOUSHA_CD_PopupBefore()
        {
            this.txtControl_Enter(this.GYOUSHA_CD, null);
        }

        /// <summary>
        /// 業者プップでデータを選択後処理
        /// </summary>
        public void GYOUSHA_CD_PopupAfter()
        {
            if (this.GYOUSHA_CD.isChanged())
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                this.txtControl_Enter(this.GYOUSHA_CD, null);
            }
        }

        /// <summary>
        /// 業者検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.GYOUSHA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckGyousha())
                {
                    e.Cancel = true;
                    this.GYOUSHA_CD.IsInputErrorOccured = true;
                    return;
                }

                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 現場イベント

        /// <summary>
        /// 現場プップ表示後処理
        /// </summary>
        public void GENBA_CD_PopupAfter()
        {
            this.txtControl_Enter(this.GENBA_CD, null);
        }

        /// <summary>
        /// 現場検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.GENBA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckGenba())
                {
                    e.Cancel = true;
                    this.GENBA_CD.IsInputErrorOccured = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 運搬業者イベント

        /// <summary>
        /// 運搬業者プップ表示後処理
        /// </summary>
        public void UPN_GYOUSHA_CD_PopupAfter()
        {
            this.txtControl_Enter(this.UPN_GYOUSHA_CD, null);
        }

        /// <summary>
        /// 運搬業者検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UPN_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.UPN_GYOUSHA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckUnpanGyousha())
                {
                    e.Cancel = true;
                    this.UPN_GYOUSHA_CD.IsInputErrorOccured = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 荷積業者イベント

        /// <summary>
        /// 荷積業者プップ表示前処理
        /// </summary>
        public void NIZUMI_GYOUSHA_CD_PopupBefore()
        {
            this.txtControl_Enter(this.NIZUMI_GYOUSHA_CD, null);
        }

        /// <summary>
        /// 荷積業者プップ表示後処理
        /// </summary>
        public void NIZUMI_GYOUSHA_CD_PopupAfter()
        {
            if (this.NIZUMI_GYOUSHA_CD.isChanged())
            {
                this.NIZUMI_GENBA_CD.Text = string.Empty;
                this.NIZUMI_GENBA_NAME_RYAKU.Text = string.Empty;
                this.txtControl_Enter(this.NIZUMI_GYOUSHA_CD, null);
            }
        }

        /// <summary>
        /// 荷積業者検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NIZUMI_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.NIZUMI_GYOUSHA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckNizumiGyousha())
                {
                    e.Cancel = true;
                    this.NIZUMI_GYOUSHA_CD.IsInputErrorOccured = true;
                    return;
                }
                this.NIZUMI_GENBA_CD.Text = string.Empty;
                this.NIZUMI_GENBA_NAME_RYAKU.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 荷積現場イベント

        /// <summary>
        /// 荷積現場プップ表示後処理
        /// </summary>
        public void NIZUMI_GENBA_CD_PopupAfter()
        {
            this.txtControl_Enter(this.NIZUMI_GENBA_CD, null);
        }

        /// <summary>
        /// 荷積現場検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NIZUMI_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.NIZUMI_GENBA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckNizumiGenba())
                {
                    e.Cancel = true;
                    this.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 荷降業者イベント

        /// <summary>
        /// 荷降業者プップ表示前処理
        /// </summary>
        public void NIOROSHI_GYOUSHA_CD_PopupBefore()
        {
            this.txtControl_Enter(this.NIOROSHI_GYOUSHA_CD, null);
        }

        /// <summary>
        /// 荷降業者プップ表示後処理
        /// </summary>
        public void NIOROSHI_GYOUSHA_CD_PopupAfter()
        {
            if (this.NIOROSHI_GYOUSHA_CD.isChanged())
            {
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME_RYAKU.Text = string.Empty;
                this.txtControl_Enter(this.NIOROSHI_GYOUSHA_CD, null);
            }
        }

        /// <summary>
        /// 荷降業者検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NIOROSHI_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.NIOROSHI_GYOUSHA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckNioroshiGyousha())
                {
                    e.Cancel = true;
                    this.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = true;
                    return;
                }
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME_RYAKU.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 荷降現場イベント

        /// <summary>
        /// 荷降現場プップ表示後処理
        /// </summary>
        public void NIOROSHI_GENBA_CD_PopupAfter()
        {
            this.txtControl_Enter(this.NIOROSHI_GENBA_CD, null);
        }

        /// <summary>
        /// 荷降現場検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NIOROSHI_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.NIOROSHI_GENBA_CD.isChanged())
                {
                    return;
                }
                if (!this.logic.CheckNioroshiGenba())
                {
                    e.Cancel = true;
                    this.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 伝種イベント
        /// <summary>
        /// 伝種TextChange後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtDenshuKbn_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.txtDenshuKbn.TextChanged -= this.txtDenshuKbn_TextChanged;
            try
            {
                if (!this.CheckAuth(this.txtDenshuKbn.Text))
                {
                    this.txtDenshuKbn.Text = this.oldDenshuKbn;
                    return;
                }
                switch (this.txtDenshuKbn.Text)
                {
                    case "1":
                        this.NIZUMI_GYOUSHA_CD.Text = string.Empty;
                        this.NIZUMI_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                        this.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.NIZUMI_GENBA_NAME_RYAKU.Text = string.Empty;

                        this.NIZUMI_GYOUSHA_CD.Enabled = false;
                        this.NIZUMI_GENBA_CD.Enabled = false;
                        this.btnNizumiGyoushaSrch.Enabled = false;
                        this.btnNizumiGenbaSrch.Enabled = false;

                        this.NIOROSHI_GYOUSHA_CD.Enabled = true;
                        this.NIOROSHI_GENBA_CD.Enabled = true;
                        this.btnNioroshiGyoushaSrch.Enabled = true;
                        this.btnNioroshiGenbaSrch.Enabled = true;
                        break;
                    case "2":
                        this.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                        this.NIOROSHI_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                        this.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.NIOROSHI_GENBA_NAME_RYAKU.Text = string.Empty;

                        this.NIZUMI_GYOUSHA_CD.Enabled = true;
                        this.NIZUMI_GENBA_CD.Enabled = true;
                        this.btnNizumiGyoushaSrch.Enabled = true;
                        this.btnNizumiGenbaSrch.Enabled = true;

                        this.NIOROSHI_GYOUSHA_CD.Enabled = false;
                        this.NIOROSHI_GENBA_CD.Enabled = false;
                        this.btnNioroshiGyoushaSrch.Enabled = false;
                        this.btnNioroshiGenbaSrch.Enabled = false;
                        break;
                    case "3":

                        this.NIZUMI_GYOUSHA_CD.Enabled = true;
                        this.NIZUMI_GENBA_CD.Enabled = true;
                        this.btnNizumiGyoushaSrch.Enabled = true;
                        this.btnNizumiGenbaSrch.Enabled = true;

                        this.NIOROSHI_GYOUSHA_CD.Enabled = true;
                        this.NIOROSHI_GENBA_CD.Enabled = true;
                        this.btnNioroshiGyoushaSrch.Enabled = true;
                        this.btnNioroshiGenbaSrch.Enabled = true;
                        break;
                }
                this.oldDenshuKbn = this.txtDenshuKbn.Text;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                this.txtDenshuKbn.TextChanged += this.txtDenshuKbn_TextChanged;
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region グリッドイベント

        #region 各CELLのフォーカス取得時処理

        /// <summary>
        /// 各CELLのフォーカス取得時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mrDetail_CellEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (this.mrDetail.CurrentCell == null)
                    return;

                if (!bCancelDenpyoPopup)
                {
                    this.txtMr_Enter(this.mrDetail.CurrentCell);
                }
                else
                {
                    bCancelDenpyoPopup = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public void Detail_PopAfter()
        {
            if (this.mrDetail.CurrentCell == null)
                return;

            string cellName = this.mrDetail.CurrentCell.Name;
            string value = Convert.ToString(this.mrDetail.CurrentCell.Value);
            Row row = this.mrDetail.CurrentRow;
            if (this.beforeValuesForDetail.ContainsKey(cellName) && this.beforeValuesForDetail[cellName] == value)
            {
                return;
            }
            string denpyouDate = Convert.ToString(row.Cells[ConstCls.COLUMN_DENPYOU_DATE].Value);
            switch (cellName)
            {
                case ConstCls.COLUMN_HINMEI_CD:

                    var gyoushaCd = (row.Cells[ConstCls.COLUMN_GYOUSHA_CD].Value == null) ? string.Empty : row.Cells[ConstCls.COLUMN_GYOUSHA_CD].Value.ToString();
                    var genbaCd = (row.Cells[ConstCls.COLUMN_GENBA_CD].Value == null) ? string.Empty : row.Cells[ConstCls.COLUMN_GENBA_CD].Value.ToString();
                    M_KOBETSU_HINMEI kobetsuHinmeis = this.logic.accessor.GetKobetsuHinmeiDataByCd(gyoushaCd, genbaCd, value, 0);
                    if (kobetsuHinmeis != null)
                    {
                        row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = kobetsuHinmeis.SEIKYUU_HINMEI_NAME;
                    }

                    break;
                default:
                    break;
            }
            this.txtMr_Enter(this.mrDetail.CurrentCell);
        }

        public void Detail_PopBefore()
        {
            if (this.mrDetail.CurrentCell == null)
                return;

            string cellName = this.mrDetail.CurrentCell.Name;
            string value = Convert.ToString(this.mrDetail.CurrentCell.Value);
            Row row = this.mrDetail.CurrentRow;
            if (this.beforeValuesForDetail.ContainsKey(cellName) && this.beforeValuesForDetail[cellName] == value)
            {
                return;
            }
            string denpyouDate = Convert.ToString(row.Cells[ConstCls.COLUMN_DENPYOU_DATE].Value);
            if (this.logic.dto.denshuKbnCd == "1")
            {
                switch (cellName)
                {
                    case ConstCls.COLUMN_HINMEI_CD:
                        break;
                    default:
                        break;
                }
            }
            this.txtMr_Enter(this.mrDetail.CurrentCell);
        }
        #endregion

        #region 各CELLの更新処理
        /// <summary>
        /// 各CELLの更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mrDetail_CellValidated(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.mrDetail.CurrentCell.ReadOnly)
            {
                return;
            }

            // 前回値と変更が無かったら処理中断
            if (beforeValuesForDetail.ContainsKey(e.CellName) &&
                beforeValuesForDetail[e.CellName].Equals(Convert.ToString(this.mrDetail.CurrentRow.Cells[e.CellName].Value)))
            {
                return;
            }
            Row row = this.mrDetail.CurrentRow;
            if (editingMultiRowFlag == false)
            {
                editingMultiRowFlag = true;

                #region 受入
                if (this.logic.dto.denshuKbnCd == "1")
                {
                    switch (e.CellName)
                    {
                        case ConstCls.COLUMN_UNIT_CD:
                            bool suuryouReadOnly = false;
                            if (Convert.ToString(row[ConstCls.COLUMN_UNIT_CD].Value) == "1" || Convert.ToString(row[ConstCls.COLUMN_UNIT_CD].Value) == "3")
                            {
                                int rowIndex = this.logic.GetListIndex(1, e.RowIndex);
                                T_UKEIRE_DETAIL detail = this.logic.tuResult[rowIndex].detailList[e.RowIndex - this.logic.tuResult[rowIndex].rowIndex];
                                if (!detail.STACK_JYUURYOU.IsNull && !detail.EMPTY_JYUURYOU.IsNull)
                                {
                                    suuryouReadOnly = true;

                                    if (Convert.ToString(row[ConstCls.COLUMN_UNIT_CD].Value) == "1")
                                    {
                                        row[ConstCls.COLUMN_SUURYOU].Value = detail.NET_JYUURYOU.IsNull ? "" : (Convert.ToDecimal(detail.NET_JYUURYOU.Value) / 1000).ToString();
                                    }
                                    else
                                    {
                                        row[ConstCls.COLUMN_SUURYOU].Value = detail.NET_JYUURYOU.IsNull ? "" : detail.NET_JYUURYOU.Value.ToString();
                                    }
                                }
                                else
                                {
                                    suuryouReadOnly = false;
                                }
                            }
                            else
                            {
                                suuryouReadOnly = false;
                            }
                            if (this.logic.headerForm.cbx_Suuryou.Checked)
                            {
                                if (suuryouReadOnly)
                                {
                                    row[ConstCls.COLUMN_SUURYOU].ReadOnly = true;
                                }
                                else
                                {
                                    row[ConstCls.COLUMN_SUURYOU].ReadOnly = false;
                                }
                                row.Cells[ConstCls.COLUMN_SUURYOU].UpdateBackColor(row.Cells[ConstCls.COLUMN_SUURYOU].Selected);
                            }
                            if (!this.logic.SearchAndCalcForUnit(false, row, 1))
                            {
                                return;
                            }
                            if (!this.logic.CalcDetaiKingaku(row, 1))
                            {
                                return;
                            }
                            break;
                        case ConstCls.COLUMN_SUURYOU:
                            if (!this.logic.CalcDetaiKingaku(row, 1))
                            {
                                return;
                            }
                            break;
                        case ConstCls.COLUMN_NEW_TANKA:
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NEW_TANKA].Value)))
                            {
                                row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = true;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                            }
                            row.Cells[ConstCls.COLUMN_NEW_KINGAKU].UpdateBackColor(row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Selected);
                            if (!this.logic.CalcDetaiKingaku(row, 1))
                            {
                                return;
                            }
                            break;
                        case ConstCls.COLUMN_NEW_KINGAKU:
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value)))
                            {
                                row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = true;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                            }
                            row.Cells[ConstCls.COLUMN_NEW_TANKA].UpdateBackColor(row.Cells[ConstCls.COLUMN_NEW_TANKA].Selected);
                            break;
                        case ConstCls.COLUMN_HINMEI_CD:
                            // 品名をセット
                            if (!this.logic.SetHinmeiName(row, 1))
                            {
                                return;
                            }
                            break;
                        default:
                            break;
                    }
                    // 高々十数件の明細行を計算するだけなので
                    editingMultiRowFlag = false;
                }
                #endregion
                #region 出荷
                else if (this.logic.dto.denshuKbnCd == "2")
                {
                    switch (e.CellName)
                    {
                        case ConstCls.COLUMN_UNIT_CD:
                            bool suuryouReadOnly = false;
                            if (Convert.ToString(row[ConstCls.COLUMN_UNIT_CD].Value) == "1" || Convert.ToString(row[ConstCls.COLUMN_UNIT_CD].Value) == "3")
                            {
                                int rowIndex = this.logic.GetListIndex(2, e.RowIndex);
                                T_SHUKKA_DETAIL detail = this.logic.tsResult[rowIndex].detailList[e.RowIndex - this.logic.tsResult[rowIndex].rowIndex];
                                if (!detail.STACK_JYUURYOU.IsNull && !detail.EMPTY_JYUURYOU.IsNull)
                                {
                                    suuryouReadOnly = true;

                                    if (Convert.ToString(row[ConstCls.COLUMN_UNIT_CD].Value) == "1")
                                    {
                                        row[ConstCls.COLUMN_SUURYOU].Value = detail.NET_JYUURYOU.IsNull ? "" : (Convert.ToDecimal(detail.NET_JYUURYOU.Value) / 1000).ToString();
                                    }
                                    else
                                    {
                                        row[ConstCls.COLUMN_SUURYOU].Value = detail.NET_JYUURYOU.IsNull ? "" : detail.NET_JYUURYOU.Value.ToString();
                                    }
                                }
                                else
                                {
                                    suuryouReadOnly = false;
                                }
                            }
                            else
                            {
                                suuryouReadOnly = false;
                            }
                            if (this.logic.headerForm.cbx_Suuryou.Checked)
                            {
                                if (suuryouReadOnly)
                                {
                                    row[ConstCls.COLUMN_SUURYOU].ReadOnly = true;
                                }
                                else
                                {
                                    row[ConstCls.COLUMN_SUURYOU].ReadOnly = false;
                                }
                                row.Cells[ConstCls.COLUMN_SUURYOU].UpdateBackColor(row.Cells[ConstCls.COLUMN_SUURYOU].Selected);
                            }
                            if (!this.logic.SearchAndCalcForUnit(false, row, 2))
                            {
                                return;
                            }
                            if (!this.logic.CalcDetaiKingaku(row, 2))
                            {
                                return;
                            }
                            break;
                        case ConstCls.COLUMN_SUURYOU:
                            if (!this.logic.CalcDetaiKingaku(row, 2))
                            {
                                return;
                            }
                            break;
                        case ConstCls.COLUMN_NEW_TANKA:
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NEW_TANKA].Value)))
                            {
                                row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = true;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                            }
                            row.Cells[ConstCls.COLUMN_NEW_KINGAKU].UpdateBackColor(row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Selected);
                            if (!this.logic.CalcDetaiKingaku(row, 2))
                            {
                                return;
                            }
                            break;
                        case ConstCls.COLUMN_NEW_KINGAKU:
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value)))
                            {
                                row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = true;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                            }
                            row.Cells[ConstCls.COLUMN_NEW_TANKA].UpdateBackColor(row.Cells[ConstCls.COLUMN_NEW_TANKA].Selected);
                            break;
                        case ConstCls.COLUMN_HINMEI_CD:
                            // 品名をセット
                            if (!this.logic.SetHinmeiName(row, 2))
                            {
                                return;
                            }
                            break;
                        default:
                            break;
                    }
                    // 高々十数件の明細行を計算するだけなので
                    editingMultiRowFlag = false;
                }
                #endregion
                #region 売上支払
                else
                {
                    switch (e.CellName)
                    {
                        case ConstCls.COLUMN_UNIT_CD:
                            if (!this.logic.SearchAndCalcForUnit(false, row, 3))
                            {
                                return;
                            }
                            if (!this.logic.CalcDetaiKingaku(row, 3))
                            {
                                return;
                            }
                            break;
                        case ConstCls.COLUMN_SUURYOU:
                            if (!this.logic.CalcDetaiKingaku(row, 3))
                            {
                                return;
                            }
                            break;
                        case ConstCls.COLUMN_NEW_TANKA:
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NEW_TANKA].Value)))
                            {
                                row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = true;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                            }
                            row.Cells[ConstCls.COLUMN_NEW_KINGAKU].UpdateBackColor(row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Selected);
                            if (!this.logic.CalcDetaiKingaku(row, 3))
                            {
                                return;
                            }
                            break;
                        case ConstCls.COLUMN_NEW_KINGAKU:
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value)))
                            {
                                row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = true;
                            }
                            else
                            {
                                row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                            }
                            row.Cells[ConstCls.COLUMN_NEW_TANKA].UpdateBackColor(row.Cells[ConstCls.COLUMN_NEW_TANKA].Selected);
                            break;
                        case ConstCls.COLUMN_HINMEI_CD:
                            // 品名をセット
                            if (!this.logic.SetHinmeiName(row, 3))
                            {
                                return;
                            }
                            break;
                        default:
                            break;
                    }
                    // 高々十数件の明細行を計算するだけなので
                    editingMultiRowFlag = false;
                }
                #endregion
            }
        }

        /// <summary>
        /// 各CELLの更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mrDetail_CellValidating(object sender, CellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool changed = !this.beforeValuesForDetail.ContainsKey(this.mrDetail.CurrentCell.Name) || this.beforeValuesForDetail[this.mrDetail.CurrentCell.Name] != Convert.ToString(this.mrDetail.CurrentCell.Value);
            try
            {
                if (this.mrDetail.CurrentCell.ReadOnly)
                {
                    return;
                }
                this.isHinmeiReLoad = false;
                string cellName = this.mrDetail.CurrentCell.Name;
                string value = Convert.ToString(this.mrDetail.CurrentCell.Value);
                Row row = this.mrDetail.CurrentRow;
                if (cellName != ConstCls.COLUMN_HINMEI_NAME && this.beforeValuesForDetail.ContainsKey(cellName) && this.beforeValuesForDetail[cellName] == value)
                {
                    return;
                }
                string denpyouDate = Convert.ToString(row.Cells[ConstCls.COLUMN_DENPYOU_DATE].Value);
                bool catchErr = true;
                bool retChousei = true;
                if (editingMultiRowFlag == false)
                {
                    editingMultiRowFlag = true;

                    #region 受入
                    if (this.logic.dto.denshuKbnCd == "1")
                    {
                        switch (e.CellName)
                        {
                            case ConstCls.COLUMN_HINMEI_CD:
                                if (string.IsNullOrEmpty(value))
                                {
                                    row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = "";
                                }
                                retChousei = this.logic.GetHinmei(row, 1, out catchErr);
                                if (!catchErr)
                                {
                                    e.Cancel = true;
                                    this.errorFlag = true;
                                    return;
                                }
                                if (retChousei)
                                {

                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E020", "品名");
                                    e.Cancel = true;
                                    this.errorFlag = true;
                                    return;
                                }
                                // 先に品名コードのエラーチェックし伝種区分が合わないものは伝票ポップアップ表示しないようにする
                                catchErr = true;
                                retChousei = this.logic.CheckHinmeiCd(row, 1, out catchErr);
                                if (!catchErr)
                                {
                                    e.Cancel = true;
                                    this.errorFlag = true;
                                    return;
                                }
                                if (!retChousei)
                                {
                                    if (this.logic.headerForm.cbx_Unit.Checked)
                                    {
                                        row.Cells[ConstCls.COLUMN_UNIT_CD].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = string.Empty;
                                    }
                                    if (this.logic.headerForm.cbx_Tanka.Checked)
                                    {
                                        row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = string.Empty;
                                    }
                                    row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = string.Empty;

                                    if (!this.logic.ZaikoHinmeiHuriwakesClear(row, 1))
                                    {
                                        return;
                                    }
                                    return;
                                }

                                // 空だったら処理中断
                                this.mrDetail.BeginEdit(false);
                                this.mrDetail.EndEdit();
                                this.mrDetail.NotifyCurrentCellDirty(false);
                                if (string.IsNullOrEmpty((string)row.Cells[e.CellName].Value))
                                {
                                    row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = string.Empty;
                                    return;
                                }

                                // No.4256-->
                                var targetRow = row;
                                if (targetRow != null)
                                {
                                    GcCustomTextBoxCell control = (GcCustomTextBoxCell)targetRow.Cells[ConstCls.COLUMN_HINMEI_CD];
                                    if (control.TextBoxChanged == true)
                                    {
                                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = string.Empty; // 伝票区分をクリア
                                        control.TextBoxChanged = false;
                                    }
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value)))
                                {
                                    return;
                                }
                                // No.4256<--

                                bool bResult = this.logic.SetDenpyouKbn();
                                if (!bResult)
                                {
                                    e.Cancel = true;
                                }
                                else
                                {
                                    // 伝票ポップアップがキャンセルされなかった場合、または伝票ポップアップが表示されない場合
                                    catchErr = true;
                                    retChousei = this.logic.CheckHinmeiCd(row, 1, out catchErr);
                                    if (!catchErr)
                                    {
                                        return;
                                    }
                                    if (retChousei)    // 品名コードの存在チェック（伝種区分が受入、または共通）
                                    {
                                        // 品名再読込フラグを立てる
                                        this.isHinmeiReLoad = true;

                                        if (!this.logic.SearchAndCalcForUnit(true, row, 1))
                                        {
                                            return;
                                        }

                                        if (!this.logic.CalcDetaiKingaku(row, 1))
                                        {
                                            return;
                                        }

                                    }
                                    else if (row.Cells[ConstCls.COLUMN_HINMEI_CD].Value == null)
                                    {
                                        // 品名CDに入力がなければ、単位コードとその略称もクリアする
                                        row.Cells[ConstCls.COLUMN_UNIT_CD].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = string.Empty;

                                        if (!this.logic.ZaikoHinmeiHuriwakesClear(row, 1))
                                        {
                                            return;
                                        }
                                    }
                                    // 前回値チェック用データをセット
                                    if (beforeValuesForDetail.ContainsKey(e.CellName))
                                    {
                                        beforeValuesForDetail[e.CellName] = Convert.ToString(row.Cells[e.CellName].Value);
                                    }
                                    else
                                    {
                                        beforeValuesForDetail.Add(e.CellName, Convert.ToString(row.Cells[e.CellName].Value));
                                    }

                                    // 品名CD変更した場合、在庫品名・比率を再検索する
                                    this.logic.ZaikoHinmeiHuriwakesSearch(row, 1);
                                }
                                break;
                            case ConstCls.COLUMN_HINMEI_NAME:
                                catchErr = true;
                                retChousei = this.logic.ValidateHinmeiName(out catchErr);
                                if (!catchErr)
                                {
                                    return;
                                }
                                if (!retChousei)
                                {
                                    e.Cancel = true;
                                }
                                break;
                            default:
                                break;
                        }

                        // 高々十数件の明細行を計算するだけなので
                        editingMultiRowFlag = false;
                    }
                    #endregion
                    #region 出荷
                    else if (this.logic.dto.denshuKbnCd == "2")
                    {
                        switch (e.CellName)
                        {
                            case ConstCls.COLUMN_HINMEI_CD:
                                if (string.IsNullOrEmpty(value))
                                {
                                    row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = "";
                                }
                                retChousei = this.logic.GetHinmei(row, 2, out catchErr);
                                if (!catchErr)
                                {
                                    e.Cancel = true;
                                    this.errorFlag = true;
                                    return;
                                }
                                if (retChousei)
                                {

                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E020", "品名");
                                    e.Cancel = true;
                                    this.errorFlag = true;
                                    return;
                                }
                                // 先に品名コードのエラーチェックし伝種区分が合わないものは伝票ポップアップ表示しないようにする
                                catchErr = true;
                                retChousei = this.logic.CheckHinmeiCd(row, 2, out catchErr);
                                if (!catchErr)
                                {
                                    e.Cancel = true;
                                    this.errorFlag = true;
                                    return;
                                }
                                if (!retChousei)
                                {
                                    if (this.logic.headerForm.cbx_Unit.Checked)
                                    {
                                        row.Cells[ConstCls.COLUMN_UNIT_CD].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = string.Empty;
                                    }
                                    if (this.logic.headerForm.cbx_Tanka.Checked)
                                    {
                                        row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = string.Empty;
                                    }
                                    row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = string.Empty;

                                    if (!this.logic.ZaikoHinmeiHuriwakesClear(row, 2))
                                    {
                                        return;
                                    }
                                    return;
                                }

                                // 空だったら処理中断
                                this.mrDetail.BeginEdit(false);
                                this.mrDetail.EndEdit();
                                this.mrDetail.NotifyCurrentCellDirty(false);
                                if (string.IsNullOrEmpty((string)row.Cells[e.CellName].Value))
                                {
                                    row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = string.Empty;
                                    return;
                                }

                                // No.4256-->
                                var targetRow = row;
                                if (targetRow != null)
                                {
                                    GcCustomTextBoxCell control = (GcCustomTextBoxCell)targetRow.Cells[ConstCls.COLUMN_HINMEI_CD];
                                    if (control.TextBoxChanged == true)
                                    {
                                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = string.Empty; // 伝票区分をクリア
                                        control.TextBoxChanged = false;
                                    }
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value)))
                                {
                                    return;
                                }
                                // No.4256<--

                                bool bResult = this.logic.SetDenpyouKbn();
                                if (!bResult)
                                {
                                    e.Cancel = true;
                                }
                                else
                                {
                                    // 伝票ポップアップがキャンセルされなかった場合、または伝票ポップアップが表示されない場合
                                    catchErr = true;
                                    retChousei = this.logic.CheckHinmeiCd(row, 2, out catchErr);
                                    if (!catchErr)
                                    {
                                        return;
                                    }
                                    if (retChousei)    // 品名コードの存在チェック（伝種区分が受入、または共通）
                                    {
                                        // 品名再読込フラグを立てる
                                        this.isHinmeiReLoad = true;

                                        if (!this.logic.SearchAndCalcForUnit(true, row, 2))
                                        {
                                            return;
                                        }

                                        if (!this.logic.CalcDetaiKingaku(row, 2))
                                        {
                                            return;
                                        }

                                    }
                                    else if (row.Cells[ConstCls.COLUMN_HINMEI_CD].Value == null)
                                    {
                                        // 品名CDに入力がなければ、単位コードとその略称もクリアする
                                        row.Cells[ConstCls.COLUMN_UNIT_CD].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = string.Empty;

                                        if (!this.logic.ZaikoHinmeiHuriwakesClear(row, 2))
                                        {
                                            return;
                                        }
                                    }
                                    // 前回値チェック用データをセット
                                    if (beforeValuesForDetail.ContainsKey(e.CellName))
                                    {
                                        beforeValuesForDetail[e.CellName] = Convert.ToString(row.Cells[e.CellName].Value);
                                    }
                                    else
                                    {
                                        beforeValuesForDetail.Add(e.CellName, Convert.ToString(row.Cells[e.CellName].Value));
                                    }

                                    // 品名CD変更した場合、在庫品名・比率を再検索する
                                    this.logic.ZaikoHinmeiHuriwakesSearch(row, 2);
                                }
                                break;
                            case ConstCls.COLUMN_HINMEI_NAME:
                                catchErr = true;
                                retChousei = this.logic.ValidateHinmeiName(out catchErr);
                                if (!catchErr)
                                {
                                    return;
                                }
                                if (!retChousei)
                                {
                                    e.Cancel = true;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    #endregion
                    #region 売上支払
                    else
                    {
                        switch (e.CellName)
                        {
                            case ConstCls.COLUMN_HINMEI_CD:
                                if (string.IsNullOrEmpty(value))
                                {
                                    row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = "";
                                }
                                retChousei = this.logic.GetHinmei(row, 3, out catchErr);
                                if (!catchErr)
                                {
                                    e.Cancel = true;
                                    this.errorFlag = true;
                                    return;
                                }
                                if (retChousei)
                                {

                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E020", "品名");
                                    e.Cancel = true;
                                    this.errorFlag = true;
                                    return;
                                }
                                // 先に品名コードのエラーチェックし伝種区分が合わないものは伝票ポップアップ表示しないようにする
                                catchErr = true;
                                retChousei = this.logic.CheckHinmeiCd(row, 3, out catchErr);
                                if (!catchErr)
                                {
                                    e.Cancel = true;
                                    this.errorFlag = true;
                                    return;
                                }
                                if (!retChousei)
                                {
                                    if (this.logic.headerForm.cbx_Unit.Checked)
                                    {
                                        row.Cells[ConstCls.COLUMN_UNIT_CD].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = string.Empty;
                                    }
                                    if (this.logic.headerForm.cbx_Tanka.Checked)
                                    {
                                        row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = string.Empty;
                                    }
                                    row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = string.Empty;

                                    if (!this.logic.ZaikoHinmeiHuriwakesClear(row, 3))
                                    {
                                        return;
                                    }
                                    return;
                                }

                                // 空だったら処理中断
                                this.mrDetail.BeginEdit(false);
                                this.mrDetail.EndEdit();
                                this.mrDetail.NotifyCurrentCellDirty(false);
                                if (string.IsNullOrEmpty((string)row.Cells[e.CellName].Value))
                                {
                                    row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = string.Empty;
                                    return;
                                }

                                // No.4356-->
                                var targetRow = row;
                                if (targetRow != null)
                                {
                                    GcCustomTextBoxCell control = (GcCustomTextBoxCell)targetRow.Cells[ConstCls.COLUMN_HINMEI_CD];
                                    if (control.TextBoxChanged == true)
                                    {
                                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = string.Empty; // 伝票区分をクリア
                                        control.TextBoxChanged = false;
                                    }
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value)))
                                {
                                    return;
                                }
                                // No.4356<--

                                bool bResult = this.logic.SetDenpyouKbn();
                                if (!bResult)
                                {
                                    e.Cancel = true;
                                }
                                else
                                {
                                    // 伝票ポップアップがキャンセルされなかった場合、または伝票ポップアップが表示されない場合
                                    catchErr = true;
                                    retChousei = this.logic.CheckHinmeiCd(row, 3, out catchErr);
                                    if (!catchErr)
                                    {
                                        return;
                                    }
                                    if (retChousei)    // 品名コードの存在チェック（伝種区分が受入、または共通）
                                    {
                                        // 品名再読込フラグを立てる
                                        this.isHinmeiReLoad = true;

                                        if (!this.logic.SearchAndCalcForUnit(true, row, 3))
                                        {
                                            return;
                                        }

                                        if (!this.logic.CalcDetaiKingaku(row, 3))
                                        {
                                            return;
                                        }

                                    }
                                    else if (row.Cells[ConstCls.COLUMN_HINMEI_CD].Value == null)
                                    {
                                        // 品名CDに入力がなければ、単位コードとその略称もクリアする
                                        row.Cells[ConstCls.COLUMN_UNIT_CD].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = string.Empty;
                                        row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = string.Empty;

                                        if (!this.logic.ZaikoHinmeiHuriwakesClear(row, 3))
                                        {
                                            return;
                                        }
                                    }
                                    // 前回値チェック用データをセット
                                    if (beforeValuesForDetail.ContainsKey(e.CellName))
                                    {
                                        beforeValuesForDetail[e.CellName] = Convert.ToString(row.Cells[e.CellName].Value);
                                    }
                                    else
                                    {
                                        beforeValuesForDetail.Add(e.CellName, Convert.ToString(row.Cells[e.CellName].Value));
                                    }

                                    // 品名CD変更した場合、在庫品名・比率を再検索する
                                    this.logic.ZaikoHinmeiHuriwakesSearch(row, 3);
                                }
                                break;
                            case ConstCls.COLUMN_HINMEI_NAME:
                                catchErr = true;
                                retChousei = this.logic.ValidateHinmeiName(out catchErr);
                                if (!catchErr)
                                {
                                    return;
                                }
                                if (!retChousei)
                                {
                                    e.Cancel = true;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                editingMultiRowFlag = false;
                if (changed)
                {
                    if (e.CellName != ConstCls.COLUMN_CHECK && !e.Cancel)
                    {
                        this.mrDetail.CurrentRow.Cells[ConstCls.COLUMN_CHECK].Value = true;
                        if (this.logic.dto.denshuKbnCd == "1")
                        {
                            int rowIndex = this.logic.GetListIndex(1, e.RowIndex);
                            this.logic.tuResult[rowIndex].check = true;
                        }
                        else if (this.logic.dto.denshuKbnCd == "2")
                        {
                            int rowIndex = this.logic.GetListIndex(2, e.RowIndex);
                            this.logic.tsResult[rowIndex].check = true;
                        }
                        else if (this.logic.dto.denshuKbnCd == "3")
                        {
                            int rowIndex = this.logic.GetListIndex(3, e.RowIndex);
                            this.logic.tusResult[rowIndex].check = true;
                        }
                    }
                }
                if (e.CellName == ConstCls.COLUMN_HINMEI_CD && !e.Cancel)
                {
                    if (this.logic.dto.denshuKbnCd == "1")
                    {
                        bool suuryouReadOnly = false;
                        if (Convert.ToString(this.mrDetail.CurrentRow[ConstCls.COLUMN_UNIT_CD].Value) == "1"
                            || Convert.ToString(this.mrDetail.CurrentRow[ConstCls.COLUMN_UNIT_CD].Value) == "3")
                        {
                            int rowIndex = this.logic.GetListIndex(1, e.RowIndex);
                            T_UKEIRE_DETAIL detail = this.logic.tuResult[rowIndex].detailList[e.RowIndex - this.logic.tuResult[rowIndex].rowIndex];
                            if (!detail.STACK_JYUURYOU.IsNull && !detail.EMPTY_JYUURYOU.IsNull)
                            {
                                suuryouReadOnly = true;
                                this.mrDetail.CurrentRow[ConstCls.COLUMN_SUURYOU].Value = detail.SUURYOU.IsNull ? "" : detail.SUURYOU.Value.ToString();
                            }
                            else
                            {
                                suuryouReadOnly = false;
                            }
                        }
                        else
                        {
                            suuryouReadOnly = false;
                        }
                        if (this.logic.headerForm.cbx_Suuryou.Checked)
                        {
                            if (suuryouReadOnly)
                            {
                                this.mrDetail.CurrentRow[ConstCls.COLUMN_SUURYOU].ReadOnly = true;
                            }
                            else
                            {
                                this.mrDetail.CurrentRow[ConstCls.COLUMN_SUURYOU].ReadOnly = false;
                            }
                            this.mrDetail.CurrentRow.Cells[ConstCls.COLUMN_SUURYOU].UpdateBackColor(this.mrDetail.CurrentRow.Cells[ConstCls.COLUMN_SUURYOU].Selected);
                        }
                    }
                    else if (this.logic.dto.denshuKbnCd == "2")
                    {
                        bool suuryouReadOnly = false;
                        if (Convert.ToString(this.mrDetail.CurrentRow[ConstCls.COLUMN_UNIT_CD].Value) == "1"
                            || Convert.ToString(this.mrDetail.CurrentRow[ConstCls.COLUMN_UNIT_CD].Value) == "3")
                        {
                            int rowIndex = this.logic.GetListIndex(2, e.RowIndex);
                            T_SHUKKA_DETAIL detail = this.logic.tsResult[rowIndex].detailList[e.RowIndex - this.logic.tsResult[rowIndex].rowIndex];
                            if (!detail.STACK_JYUURYOU.IsNull && !detail.EMPTY_JYUURYOU.IsNull)
                            {
                                suuryouReadOnly = true;
                                this.mrDetail.CurrentRow[ConstCls.COLUMN_SUURYOU].Value = detail.SUURYOU.IsNull ? "" : detail.SUURYOU.Value.ToString();
                            }
                            else
                            {
                                suuryouReadOnly = false;
                            }
                        }
                        else
                        {
                            suuryouReadOnly = false;
                        }
                        if (this.logic.headerForm.cbx_Suuryou.Checked)
                        {
                            if (suuryouReadOnly)
                            {
                                this.mrDetail.CurrentRow[ConstCls.COLUMN_SUURYOU].ReadOnly = true;
                            }
                            else
                            {
                                this.mrDetail.CurrentRow[ConstCls.COLUMN_SUURYOU].ReadOnly = false;
                            }
                            this.mrDetail.CurrentRow.Cells[ConstCls.COLUMN_SUURYOU].UpdateBackColor(this.mrDetail.CurrentRow.Cells[ConstCls.COLUMN_SUURYOU].Selected);
                        }
                    }
                }
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region へーダーチェックをクリックイベント
        public void mrDetail_CellClick(object sender, CellEventArgs e)
        {
            if (this.clickLock)
            {
                return;
            }
            try
            {
                this.clickLock = true;
                if (this.mrDetail.Rows.Count > 0)
                {
                    if (e.RowIndex == -1)
                    {
                        int currentRowIndex = this.mrDetail.CurrentCell.RowIndex;
                        int currentCellIndex = this.mrDetail.CurrentCell.CellIndex;
                        this.mrDetail.CurrentCell = null;
                        switch (e.CellIndex)
                        {
                            case 0:
                                CheckBoxCell delete = this.mrDetail.ColumnHeaders[0].Cells["lbl_CHECK"] as CheckBoxCell;
                                if (this.logic.ToNBoolean(delete.Value) == true)
                                {
                                    delete.Value = false;
                                }
                                else
                                {
                                    delete.Value = true;
                                }
                                foreach (Row row in this.mrDetail.Rows)
                                {
                                    GcCustomCheckBoxCell cell = (row.Cells["CHECK"] as GcCustomCheckBoxCell);
                                    cell.Value = delete.Value;
                                }
                                switch (this.logic.dto.denshuKbnCd)
                                {
                                    case "1":
                                        foreach (var entity in this.logic.tuResult)
                                        {
                                            entity.check = Convert.ToBoolean(delete.Value);
                                        }
                                        break;
                                    case "2":
                                        foreach (var entity in this.logic.tsResult)
                                        {
                                            entity.check = Convert.ToBoolean(delete.Value);
                                        }
                                        break;
                                    case "3":
                                        foreach (var entity in this.logic.tusResult)
                                        {
                                            entity.check = Convert.ToBoolean(delete.Value);
                                        }
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        this.mrDetail.CurrentCell = this.mrDetail.Rows[currentRowIndex].Cells[currentCellIndex];
                    }
                    else
                    {
                        int currentRowIndex = this.mrDetail.CurrentCell.RowIndex;
                        int currentCellIndex = this.mrDetail.CurrentCell.CellIndex;
                        this.mrDetail.CurrentCell = null;
                        switch (e.CellName)
                        {
                            case ConstCls.COLUMN_CHECK:
                                CheckBoxCell delete = this.mrDetail.Rows[e.RowIndex].Cells[ConstCls.COLUMN_CHECK] as CheckBoxCell;
                                bool check = this.logic.ToNBoolean(delete.Value) ?? false;
                                switch (this.logic.dto.denshuKbnCd)
                                {
                                    case "1":
                                        int rowIndex = this.logic.GetListIndex(1, e.RowIndex);
                                        if (check)
                                        {
                                            this.logic.tuResult[rowIndex].check = check;
                                        }
                                        else
                                        {
                                            bool setCheck = false;
                                            for (int i = this.logic.tuResult[rowIndex].rowIndex; i < this.logic.tuResult[rowIndex].rowIndex + this.logic.tuResult[rowIndex].detailList.Count; i++)
                                            {
                                                if (i == e.RowIndex)
                                                {
                                                    continue;
                                                }
                                                GcCustomCheckBoxCell cell = (this.mrDetail.Rows[i].Cells["CHECK"] as GcCustomCheckBoxCell);
                                                if (this.logic.ToNBoolean(cell.Value) == true)
                                                {
                                                    setCheck = true;
                                                    break;
                                                }
                                            }
                                            this.logic.tuResult[rowIndex].check = setCheck;
                                        }
                                        break;
                                    case "2":
                                        rowIndex = this.logic.GetListIndex(2, e.RowIndex);
                                        if (check)
                                        {
                                            this.logic.tsResult[rowIndex].check = check;
                                        }
                                        else
                                        {
                                            bool setCheck = false;
                                            for (int i = this.logic.tsResult[rowIndex].rowIndex; i < this.logic.tsResult[rowIndex].rowIndex + this.logic.tsResult[rowIndex].detailList.Count; i++)
                                            {
                                                if (i == e.RowIndex)
                                                {
                                                    continue;
                                                }
                                                GcCustomCheckBoxCell cell = (this.mrDetail.Rows[i].Cells["CHECK"] as GcCustomCheckBoxCell);
                                                if (this.logic.ToNBoolean(cell.Value) == true)
                                                {
                                                    setCheck = true;
                                                    break;
                                                }
                                            }
                                            this.logic.tsResult[rowIndex].check = setCheck;
                                        }
                                        break;
                                    case "3":
                                        rowIndex = this.logic.GetListIndex(3, e.RowIndex);
                                        if (check)
                                        {
                                            this.logic.tusResult[rowIndex].check = check;
                                        }
                                        else
                                        {
                                            bool setCheck = false;
                                            for (int i = this.logic.tusResult[rowIndex].rowIndex; i < this.logic.tusResult[rowIndex].rowIndex + this.logic.tusResult[rowIndex].detailList.Count; i++)
                                            {
                                                if (i == e.RowIndex)
                                                {
                                                    continue;
                                                }
                                                GcCustomCheckBoxCell cell = (this.mrDetail.Rows[i].Cells["CHECK"] as GcCustomCheckBoxCell);
                                                if (this.logic.ToNBoolean(cell.Value) == true)
                                                {
                                                    setCheck = true;
                                                    break;
                                                }
                                            }
                                            this.logic.tusResult[rowIndex].check = setCheck;
                                        }
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        this.mrDetail.CurrentCell = this.mrDetail.Rows[currentRowIndex].Cells[currentCellIndex];
                    }
                }
            }
            finally
            {
                this.clickLock = false;
            }
        }
        #endregion

        #endregion

        #region フォーカス取得イベント

        /// <summary>
        /// フォーカス取得イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtControl_Enter(object sender, EventArgs e)
        {
            // 前回内容保存
            var txtCtrl = sender as CustomTextBox;
            if (txtCtrl != null)
                txtCtrl.PrevText = txtCtrl.Text;
        }

        /// <summary>
        /// フォーカス取得イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtMr_Enter(object sender)
        {
            // 前回内容保存
            var txtCtrl = sender as GcCustomTextBoxCell;
            if (txtCtrl != null)
            {
                string cellName = txtCtrl.Name;
                if (this.errorFlag)
                {
                    this.errorFlag = false;
                }
                else
                {
                    if (this.beforeValuesForDetail.ContainsKey(cellName))
                    {
                        this.beforeValuesForDetail[cellName] = Convert.ToString(txtCtrl.Value);
                    }
                    else
                    {
                        this.beforeValuesForDetail.Add(cellName, Convert.ToString(txtCtrl.Value));
                    }
                }
            }
        }

        #endregion

        #region 日付イベント
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

        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.HIDUKE_FROM;
            var ToTextBox = this.HIDUKE_TO;

            ToTextBox.Text = FromTextBox.Text;
            LogUtility.DebugMethodEnd();
        }
        #endregion
        #endregion

        #region メソッド

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal bool CheckAuth(string kbn)
        {
            return this.logic.CheckAuth(kbn);
        }

        internal bool CheckTorihikisakiKyoten(string kyoten, M_TORIHIKISAKI tori)
        {
            if (!string.IsNullOrEmpty(kyoten))
            {
                if (Int16.Parse(kyoten) != tori.TORIHIKISAKI_KYOTEN_CD && !tori.TORIHIKISAKI_KYOTEN_CD.ToString().Equals(SalesPaymentConstans.KYOTEN_ZENSHA))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 車輌PopUpの検索条件に車輌CDを含めるかを引数によって設定します
        /// </summary>
        /// <param name="isPopupConditionsSharyouCD"></param>
        internal void PopUpConditionsSharyouSwitch(GcCustomAlphaNumTextBoxCell cell, bool isPopupConditionsSharyouCD)
        {
            PopupSearchSendParamDto sharyouParam = new PopupSearchSendParamDto();
            sharyouParam.And_Or = CONDITION_OPERATOR.AND;
            sharyouParam.Control = "mr_SHARYOU_CD";
            sharyouParam.KeyName = "key002";

            if (isPopupConditionsSharyouCD)
            {
                if (!cell.PopupSearchSendParams.Contains(sharyouParam))
                {
                    cell.PopupSearchSendParams.Add(sharyouParam);
                }
            }
            else
            {
                var paramsCount = cell.PopupSearchSendParams.Count;
                for (int i = 0; i < paramsCount; i++)
                {
                    if (cell.PopupSearchSendParams[i].Control == "SHARYOU_CD" &&
                        cell.PopupSearchSendParams[i].KeyName == "key002")
                    {
                        cell.PopupSearchSendParams.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// ゼロサプレス処理
        /// </summary>
        /// <param name="source">入力コントロール</param>
        /// <returns>ゼロサプレス後の文字列</returns>
        private string ZeroSuppress(object source)
        {
            string result = string.Empty;

            // 該当コントロールの最大桁数を取得
            object obj;
            decimal charactersNumber;
            string text = PropertyUtility.GetTextOrValue(source);
            if (!PropertyUtility.GetValue(source, Constans.CHARACTERS_NUMBER, out obj))
                // 最大桁数が取得できない場合はそのまま
                return text;

            charactersNumber = (decimal)obj;
            if (charactersNumber == 0 || source == null || string.IsNullOrEmpty(text))
                // 最大桁数が0または入力値が空の場合はそのまま
                return text;

            var strCharactersUmber = text;
            if (strCharactersUmber.Contains("."))
                // 小数点を含む場合はそのまま
                return text;

            // ゼロサプレスした値を返す
            StringBuilder sb = new StringBuilder((int)charactersNumber);
            string format = sb.Append('#', (int)charactersNumber).ToString();
            long val = 0;
            if (long.TryParse(text, out val))
                result = val == 0 ? "0" : val.ToString(format);
            else
                // 入力値が数値ではない場合はそのまま
                result = text;

            return result;
        }

        /// <summary>
        /// 伝票区分Validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DENPYOU_KBN_CD_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DENPYOU_KBN_CD.Text))
            {
                if (this.DENPYOU_KBN_CD.Text == "1")
                {
                    this.DENPYOU_KBN_NAME.Text = "売上";
                }
                else if (this.DENPYOU_KBN_CD.Text == "2")
                {
                    this.DENPYOU_KBN_NAME.Text = "支払";
                }
            }
            else
            {
                this.DENPYOU_KBN_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 取引区分Validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TORIHIKI_KBN_CD_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TORIHIKI_KBN_CD.Text))
            {
                if (this.TORIHIKI_KBN_CD.Text == "1")
                {
                    this.TORIHIKI_KBN_NAME.Text = "掛け";
                }
                else if (this.TORIHIKI_KBN_CD.Text == "2")
                {
                    this.TORIHIKI_KBN_NAME.Text = "現金";
                }
                else if (this.TORIHIKI_KBN_CD.Text == "3")
                {
                    this.TORIHIKI_KBN_NAME.Text = "全て";
                }
            }
            else
            {
                this.TORIHIKI_KBN_NAME.Text = string.Empty;
            }
        }
        #endregion


    }
}