using System;
using System.Data;
using System.Data.SqlTypes;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;
using System.Windows.Forms;

namespace Shougun.Core.BusinessManagement.DenshiShinseiIchiran
{
    /// <summary>
    /// G280 申請一覧画面クラス
    /// </summary>
    public partial class DenshiShinseiIchiranUIForm : SuperForm
    {
        /// <summary>
        /// ロジッククラス
        /// </summary>
        private DenshiShinseiIchiranLogic logic;

        /// <summary>
        /// 申請表示モードフラグ(F9ボタン制御用)
        /// True:決裁　False:申請表示
        /// </summary>
        private bool KessaiMode { get; set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">ウィンドウID</param>
        public DenshiShinseiIchiranUIForm(WINDOW_ID windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new DenshiShinseiIchiranLogic(this);

            this.DENSHI_SHINSEI_ICHIRAN.AutoGenerateColumns = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面内の項目を初期化します
        /// </summary>
        public void Initialize()
        {
            LogUtility.DebugMethodStart();

            this.SetFormData(true);
            this.ChangeShinseiKbnState();
            this.ChangeButtonFunc9Mode();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            if (!this.logic.WindowInit()) { return; }

            this.Initialize();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.DENSHI_SHINSEI_ICHIRAN != null)
            {
                int GRID_HEIGHT_MIN_VALUE = 257;
                int GRIND_WIDTH_MIN_VALUE = 576;
                int h = this.Height - 180;
                int w = this.Width;

                if (h < GRID_HEIGHT_MIN_VALUE)
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Height = GRID_HEIGHT_MIN_VALUE;
                }
                else
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Height = h;
                }
                if (w < GRIND_WIDTH_MIN_VALUE)
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Width = GRIND_WIDTH_MIN_VALUE;
                }
                else
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Width = w;
                }

                if (this.DENSHI_SHINSEI_ICHIRAN.Height <= GRID_HEIGHT_MIN_VALUE
                    || this.DENSHI_SHINSEI_ICHIRAN.Width <= GRIND_WIDTH_MIN_VALUE)
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                }
                else
                {
                    this.DENSHI_SHINSEI_ICHIRAN.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }

            LogUtility.DebugMethodEnd();
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

        /// <summary>
        /// 内容確認ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc3_Clicked(object sender, EventArgs e)
        {
            var row = this.DENSHI_SHINSEI_ICHIRAN.CurrentRow;
            if (row == null)
            {
                return;
            }

            var name = string.Empty;

            if (row.Cells["SHINSEI_STATUS"].Value != null)
            {
                name = row.Cells["SHINSEI_STATUS"].Value.ToString();
                var utility = new DenshiShinseiUtility();
                if (utility.ToString(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPLYING).Equals(name)
                    || utility.ToString(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPROVAL).Equals(name)
                    || utility.ToString(DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.DENIAL_CONF).Equals(name))
                {
                    this.logic.ShowReferenceDisplay();
                    return;
                }
            }

            // 否認、移行済みの場合は完了済み申請のため内容確認はできないようにする。
            MessageBoxUtility.MessageBoxShow("E210", name);
        }

        /// <summary>
        /// 条件取消ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.Dto = new DenshiShinseiIchiranDto();

            var ribbon = (RibbonMainMenu)((BusinessBaseForm)this.Parent).ribbonForm;
            this.logic.Dto.ShainCd = ribbon.GlobalCommonInformation.CurrentShain.SHAIN_CD;
            this.logic.Dto.ShainName = ribbon.GlobalCommonInformation.CurrentShain.SHAIN_NAME_RYAKU;

            this.customSortHeader1.ClearCustomSortSetting();

            if (!this.logic.SetHeaderInitData()) { return; }

            this.SetFormData(false);
            this.ChangeShinseiKbnState();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc8_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var autoCheckLogic = new AutoRegistCheckLogic(this.allControl, this.allControl);
            this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (false == this.RegistErrorFlag)
            {
                // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    return;
                }
                // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

                int count = Search();
                if (count == -1) { return; }
                if (count < 1)
                {
                    MessageBoxUtility.MessageBoxShow("C001");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索処理を行います
        /// </summary>
        /// <returns></returns>
        internal int Search()
        {
            LogUtility.DebugMethodStart();

            this.GetFormData();
            var count = this.logic.Search();
            if (count < 0) { return count; }

            var header = (DenshiShinseiIchiranUIHeaderForm)((BusinessBaseForm)this.Parent).headerForm;
            header.readDataNumber.Text = count.ToString();

            LogUtility.DebugMethodEnd();

            return count;
        }

        /// <summary>
        /// 決裁モードを設定します。
        /// 決裁が出来る場合は「決裁」、出来ない場合は「申請表示」に変更
        /// </summary>
        private void ChangeButtonFunc9Mode()
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            if (this.SHINSEI_KBN_1.Text.Equals(DenshiShinseiIchiranConst.SHINSEI_KBN_1_KAKUNINIRAI) &&
                this.SHINSEI_KBN_2.Text.Equals(DenshiShinseiIchiranConst.SHINSEI_KBN_2_MIKAKUNIN))
            {
                // 決裁
                this.KessaiMode = true;
            }
            else
            {
                // 申請表示
                this.KessaiMode = false;
            }
        }

        /// <summary>
        /// 申請表示ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc9_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var row = this.DENSHI_SHINSEI_ICHIRAN.CurrentRow;
            if (null != row)
            {
                var systemId = row.Cells["SYSTEM_ID"].Value;
                var seq = row.Cells["SEQ"].Value;

                if (this.KessaiMode)
                {
                    // 決裁
                    FormManager.OpenFormModal("G279", WINDOW_TYPE.UPDATE_WINDOW_FLAG, systemId, seq);
                }
                else
                {
                    // 申請表示
                    FormManager.OpenFormModal("G279", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, systemId, seq);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 並び替えボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc10_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customSortHeader1.ShowCustomSortSettingDialog();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 閉じるボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 申請区分１のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHINSEI_KBN_1_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeShinseiKbnState();

            // F9ボタンのモード変更
            this.ChangeButtonFunc9Mode();

            // 値変更時は変更前の検索結果が表示されている可能性があるためクリア。
            r_framework.CustomControl.CustomTextBox ctr = sender as r_framework.CustomControl.CustomTextBox;
            if (ctr != null)
            {
                // ※ラジオボタン変更時はNullになってしまうが、そもそも同じ値だとTextChangeが発生しない
                string prevText = ctr.prevText == null ? "" : ctr.prevText;

                string text = ctr.Text == null ? "" : ctr.Text;
                if (!prevText.Equals(text))
                {
                    DataTable dt = this.DENSHI_SHINSEI_ICHIRAN.DataSource as DataTable;
                    if (dt != null)
                    {
                        dt.Clear();
                    }
                    this.DENSHI_SHINSEI_ICHIRAN.DataSource = dt;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 申請区分2のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHINSEI_KBN_2_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // F9ボタンのモード変更
            this.ChangeButtonFunc9Mode();

            // 値変更時は変更前の検索結果が表示されている可能性があるためクリア。
            r_framework.CustomControl.CustomTextBox ctr = sender as r_framework.CustomControl.CustomTextBox;
            if (ctr != null)
            {
                // ※ラジオボタン変更時はNullになってしまうが、そもそも同じ値だとTextChangeが発生しない
                string prevText = ctr.prevText == null ? "" : ctr.prevText;

                string text = ctr.Text == null ? "" : ctr.Text;
                if (!prevText.Equals(text))
                {
                    DataTable dt = this.DENSHI_SHINSEI_ICHIRAN.DataSource as DataTable;
                    if (dt != null)
                    {
                        dt.Clear();
                    }
                    this.DENSHI_SHINSEI_ICHIRAN.DataSource = dt;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 申請区分3のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHINSEI_KBN_3_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // F9ボタンのモード変更
            this.ChangeButtonFunc9Mode();

            // 値変更時は変更前の検索結果が表示されている可能性があるためクリア。
            r_framework.CustomControl.CustomTextBox ctr = sender as r_framework.CustomControl.CustomTextBox;
            if (ctr != null)
            {
                // ※ラジオボタン変更時はNullになってしまうが、そもそも同じ値だとTextChangeが発生しない
                string prevText = ctr.prevText == null ? "" : ctr.prevText;

                string text = ctr.Text == null ? "" : ctr.Text;
                if (!prevText.Equals(text))
                {
                    DataTable dt = this.DENSHI_SHINSEI_ICHIRAN.DataSource as DataTable;
                    if (dt != null)
                    {
                        dt.Clear();
                    }
                    this.DENSHI_SHINSEI_ICHIRAN.DataSource = dt;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// DTOに画面からデータをセットします
        /// </summary>
        private void GetFormData()
        {
            LogUtility.DebugMethodStart();

            var header = (DenshiShinseiIchiranUIHeaderForm)((BusinessBaseForm)this.Parent).headerForm;
            this.logic.Dto.KyotenCd = header.KYOTEN_CD.Text;
            this.logic.Dto.KyotenName = header.KYOTEN_NAME_RYAKU.Text;

            this.logic.Dto.ShainCd = this.SHAIN_CD.Text;
            this.logic.Dto.ShainName = this.SHAIN_NAME.Text;
            this.logic.Dto.ShinseiKbn1Cd = Int16.Parse(this.SHINSEI_KBN_1.Text);
            if (!String.IsNullOrEmpty(this.SHINSEI_KBN_2.Text))
            {
                this.logic.Dto.ShinseiKbn2Cd = Int16.Parse(this.SHINSEI_KBN_2.Text);
            }
            else
            {
                this.logic.Dto.ShinseiKbn2Cd = 0;
            }
            if (!String.IsNullOrEmpty(this.SHINSEI_KBN_3.Text))
            {
                this.logic.Dto.ShinseiKbn3Cd = Int16.Parse(this.SHINSEI_KBN_3.Text);
            }
            else
            {
                this.logic.Dto.ShinseiKbn3Cd = 0;
            }

            if (this.SHINSEI_DATE_FROM.Value != null)
            {
                this.logic.Dto.ShinseiDateFrom = SqlDateTime.Parse(this.SHINSEI_DATE_FROM.Value.ToString());
            }
            else
            {
                this.logic.Dto.ShinseiDateFrom = SqlDateTime.Null;
            }

            if (this.SHINSEI_DATE_TO.Value != null)
            {
                this.logic.Dto.ShinseiDateTo = SqlDateTime.Parse(this.SHINSEI_DATE_TO.Value.ToString());
            }
            else
            {
                this.logic.Dto.ShinseiDateTo = SqlDateTime.Null;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// DTOのデータを画面にセットします
        /// </summary>
        /// <param name="isSetKyoten">拠点を変更するかのフラグ</param>
        private void SetFormData(bool isSetKyoten)
        {
            LogUtility.DebugMethodStart();

            var header = (DenshiShinseiIchiranUIHeaderForm)((BusinessBaseForm)this.Parent).headerForm;
            if (isSetKyoten)
            {
                // F7条件ｸﾘｱでは初期化しないため
                header.KYOTEN_CD.Text = this.logic.Dto.KyotenCd;
                header.KYOTEN_NAME_RYAKU.Text = this.logic.Dto.KyotenName;
            }
            header.readDataNumber.Text = "0";

            this.SHAIN_CD.Text = this.logic.Dto.ShainCd;
            this.SHAIN_NAME.Text = this.logic.Dto.ShainName;
            this.SHINSEI_KBN_1.Text = this.logic.Dto.ShinseiKbn1Cd.ToString();
            if (0 != this.logic.Dto.ShinseiKbn2Cd)
            {
                this.SHINSEI_KBN_2.Text = this.logic.Dto.ShinseiKbn2Cd.ToString();
            }
            else
            {
                this.SHINSEI_KBN_2.Text = String.Empty;
            }
            if (0 != this.logic.Dto.ShinseiKbn3Cd)
            {
                this.SHINSEI_KBN_3.Text = this.logic.Dto.ShinseiKbn3Cd.ToString();
            }
            else
            {
                this.SHINSEI_KBN_3.Text = String.Empty;
            }

            if (!this.logic.Dto.ShinseiDateFrom.IsNull)
            {
                this.SHINSEI_DATE_FROM.Text = this.logic.Dto.ShinseiDateFrom.ToString();
            }
            else
            {
                this.SHINSEI_DATE_FROM.Text = String.Empty;
            }
            if (!this.logic.Dto.ShinseiDateTo.IsNull)
            {
                this.SHINSEI_DATE_TO.Text = this.logic.Dto.ShinseiDateTo.ToString();
            }
            else
            {
                this.SHINSEI_DATE_TO.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 申請区分１の状態に応じて、申請区分２・申請区分３の状態を変更します
        /// </summary>
        private void ChangeShinseiKbnState()
        {
            LogUtility.DebugMethodStart();

            var shinseiKbn1 = this.SHINSEI_KBN_1.Text;
            switch (shinseiKbn1)
            {
                case "1":
                    this.SHINSEI_KBN_2.Enabled = true;
                    this.SHINSEI_KBN_2_1.Enabled = true;
                    this.SHINSEI_KBN_2_2.Enabled = true;
                    this.SHINSEI_KBN_2_3.Enabled = true;
                    this.SHINSEI_KBN_3.Enabled = false;
                    this.SHINSEI_KBN_3_1.Enabled = false;
                    this.SHINSEI_KBN_3_2.Enabled = false;
                    this.SHINSEI_KBN_3_3.Enabled = false;
                    this.SHINSEI_KBN_3_4.Enabled = false;

                    this.SHINSEI_KBN_2.Text = "1";
                    this.SHINSEI_KBN_3.Text = String.Empty;

                    this.SHINSEI_KBN_2.RegistCheckMethod.Add(new SelectCheckDto("必須チェック"));
                    this.SHINSEI_KBN_3.RegistCheckMethod.Clear();

                    this.label3.Text = "申請区分2※";
                    this.label4.Text = "申請区分3";
                    break;
                case "2":
                    this.SHINSEI_KBN_2.Enabled = false;
                    this.SHINSEI_KBN_2_1.Enabled = false;
                    this.SHINSEI_KBN_2_2.Enabled = false;
                    this.SHINSEI_KBN_2_3.Enabled = false;
                    this.SHINSEI_KBN_3.Enabled = true;
                    this.SHINSEI_KBN_3_1.Enabled = true;
                    this.SHINSEI_KBN_3_2.Enabled = true;
                    this.SHINSEI_KBN_3_3.Enabled = true;
                    this.SHINSEI_KBN_3_4.Enabled = true;

                    this.SHINSEI_KBN_2.Text = String.Empty;
                    this.SHINSEI_KBN_3.Text = "1";

                    this.SHINSEI_KBN_2.RegistCheckMethod.Clear();
                    this.SHINSEI_KBN_3.RegistCheckMethod.Add(new SelectCheckDto("必須チェック"));

                    this.label3.Text = "申請区分2";
                    this.label4.Text = "申請区分3※";
                    break;
                default:
                    this.SHINSEI_KBN_2.Enabled = false;
                    this.SHINSEI_KBN_2_1.Enabled = false;
                    this.SHINSEI_KBN_2_2.Enabled = false;
                    this.SHINSEI_KBN_2_3.Enabled = false;
                    this.SHINSEI_KBN_3.Enabled = false;
                    this.SHINSEI_KBN_3_1.Enabled = false;
                    this.SHINSEI_KBN_3_2.Enabled = false;
                    this.SHINSEI_KBN_3_3.Enabled = false;
                    this.SHINSEI_KBN_3_4.Enabled = false;

                    this.SHINSEI_KBN_2.Text = String.Empty;
                    this.SHINSEI_KBN_3.Text = String.Empty;

                    this.SHINSEI_KBN_2.RegistCheckMethod.Clear();
                    this.SHINSEI_KBN_3.RegistCheckMethod.Clear();

                    this.label3.Text = "申請区分2";
                    this.label4.Text = "申請区分3";
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 申請一覧のDataSourceにデータをセットします
        /// </summary>
        /// <param name="dataTable"></param>
        internal void SetDenshiShinseiIchiranDataSource(DataTable dataTable)
        {
            LogUtility.DebugMethodStart();

            if (dataTable != null)
            {
                this.customSortHeader1.SortDataTable(dataTable);
            }
            this.DENSHI_SHINSEI_ICHIRAN.IsBrowsePurpose = false;
            this.DENSHI_SHINSEI_ICHIRAN.DataSource = dataTable;
            this.DENSHI_SHINSEI_ICHIRAN.IsBrowsePurpose = true;

            LogUtility.DebugMethodEnd();
        }

        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.SHINSEI_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.SHINSEI_DATE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.SHINSEI_DATE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.SHINSEI_DATE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.SHINSEI_DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.SHINSEI_DATE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.SHINSEI_DATE_FROM.IsInputErrorOccured = true;
                this.SHINSEI_DATE_TO.IsInputErrorOccured = true;
                this.SHINSEI_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.SHINSEI_DATE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "申請日付From", "申請日付To" };
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                msglogic.MessageBoxShow("E030", errorMsg);
                this.SHINSEI_DATE_FROM.Focus();
                return true;
            }
            return false;
        }
        #endregion

        private void SHINSEI_DATE_FROM_Leave(object sender, EventArgs e)
        {
            this.SHINSEI_DATE_TO.IsInputErrorOccured = false;
            this.SHINSEI_DATE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void SHINSEI_DATE_TO_Leave(object sender, EventArgs e)
        {
            this.SHINSEI_DATE_FROM.IsInputErrorOccured = false;
            this.SHINSEI_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
        }
        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141127 teikyou ダブルクリックを追加する　start
        private void SHINSEI_DATE_TO_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var shinseiDateFromTextBox = this.SHINSEI_DATE_FROM;
            var shinseiDateToTextBox = this.SHINSEI_DATE_TO;
            shinseiDateToTextBox.Text = shinseiDateFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141127 teikyou ダブルクリックを追加する　end
        #endregion
    }
}
