using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ExternalConnection.DenshiBunshoHoshu.Const;
using Shougun.Core.ExternalConnection.DenshiBunshoHoshu.Logic;

namespace Shougun.Core.ExternalConnection.DenshiBunshoHoshu
{
    /// <summary>
    /// 電子文書詳細入力画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 電子文書詳細入力画面ロジック
        /// </summary>
        private LogicClass logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_DENSHI_BUNSHO_INFO, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyakuSystemID">委託契約システムID</param>
        /// <param name="wansignSystemID">WANSIGNシステムID</param>
        public UIForm(WINDOW_TYPE windowType, string wansignSystemID, string keiyakuSystemID)
            : base(WINDOW_ID.M_DENSHI_BUNSHO_INFO, windowType)
        {
            InitializeComponent();

            this.logic = new LogicClass(this);

            this.logic.KeiyakuSystemID = keiyakuSystemID;
            this.logic.WansignSystemID = wansignSystemID;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //※※※　何故か、タブが一度選択されないと修正時に敬称がうまくセットされないので
            //※※※　強制的に一度全タブを選択して戻すようにすることで一旦解決
            TabPage now = this.tabData.SelectedTab;
            foreach (TabPage page in this.tabData.TabPages)
            {
                this.tabData.SelectedTab = page;
            }
            this.tabData.SelectedTab = now;
            //※※※　強引な対応ここまで

            bool catchErr = this.logic.WindowInit(WindowType);
            if (catchErr)
            {
                return;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.ITAKU_KEIYAKU_ICHIRAN != null)
            {
                this.ITAKU_KEIYAKU_ICHIRAN.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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

		//PhuocLoc 2022/03/09 #161247 -Start
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // 改行可能項目にてCtrl+Enterで次の項目にフォーカスが移動しないよう判断

            var act = ControlUtility.GetActiveControl(this);
            // EnterとControlキー押下判断
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                var textBox = act as TextBox;
                // 改行できるTextBoxか判断
                if (textBox.Multiline)
                {
                    e.Handled = true;
                    return;
                }
            }

            base.OnKeyDown(e);
        }
        //PhuocLoc 2022/03/09 #161247 -End

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                var result = this.errmessage.MessageBoxShow("C131", MessageBoxDefaultButton.Button2);
                if (result != DialogResult.Yes)
                {
                    this.logic.listWansignKeiyakuInfoEntity = this.logic.wanSignKeiyakuInfoDao.GetDataByDocumentId(this.logic.headerForm.DOCUMENT_ID.Text);
                    if (this.logic.listWansignKeiyakuInfoEntity == null || this.logic.listWansignKeiyakuInfoEntity.Length == 0)
                    {
                        return;
                    }

                    foreach (M_WANSIGN_KEIYAKU_INFO wansignInfoEntity in this.logic.listWansignKeiyakuInfoEntity)
                    {
                        if (wansignInfoEntity.CONTROL_NUMBER == this.logic.wanSignKeiyakuInfoEntity.CONTROL_NUMBER
                            && ConvertStrByte.ByteToString(wansignInfoEntity.TIME_STAMP) != ConvertStrByte.ByteToString(this.logic.wanSignKeiyakuInfoEntity.TIME_STAMP))
                        {
                            this.errmessage.MessageBoxShow("E080", "");
                            return;
                        }
                    }

                    //PhuocLoc 2022/03/08 #161248 -Start
                    if (!string.IsNullOrEmpty(this.ORIGINAL_CONTROL_NUMBER.Text))
                    {
                        M_WANSIGN_KEIYAKU_INFO[] listWansignKeiyakuInfoEntityCheck = this.logic.wanSignKeiyakuInfoDao.GetDataDuplicate(this.logic.headerForm.DOCUMENT_ID.Text, this.logic.form.ORIGINAL_CONTROL_NUMBER.Text);
                        if (listWansignKeiyakuInfoEntityCheck != null && listWansignKeiyakuInfoEntityCheck.Length > 0)
                        {
                            var resultSub = this.errmessage.MessageBoxShow("C132");
                            if (resultSub != DialogResult.Yes)
                            {
                                return;
                            }
                        }
                    }
                    //PhuocLoc 2022/03/08 #161248 -End

                    bool catchErr = this.logic.CreateEntity(false);
                    if (catchErr)
                    {
                        return;
                    }
                    using (var tran = new Transaction())
                    {
                        // 更新
                        if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        {
                            this.logic.Update(base.RegistErrorFlag);
                        }
                        if (base.RegistErrorFlag)
                        {
                            return;
                        }
                        tran.Commit();
                    }

                    this.errmessage.MessageBoxShow("I029");
                }
                else
                {
                    if (this.logic.CheckRegistData())
                    {
                        return;
                    }

                    this.logic.listWansignKeiyakuInfoEntity = this.logic.wanSignKeiyakuInfoDao.GetDataByDocumentId(this.logic.headerForm.DOCUMENT_ID.Text);
                    if (this.logic.listWansignKeiyakuInfoEntity == null || this.logic.listWansignKeiyakuInfoEntity.Length == 0)
                    {
                        return;
                    }

                    foreach (M_WANSIGN_KEIYAKU_INFO wansignInfoEntity in this.logic.listWansignKeiyakuInfoEntity)
                    {
                        if (wansignInfoEntity.CONTROL_NUMBER == this.logic.wanSignKeiyakuInfoEntity.CONTROL_NUMBER
                            && ConvertStrByte.ByteToString(wansignInfoEntity.TIME_STAMP) != ConvertStrByte.ByteToString(this.logic.wanSignKeiyakuInfoEntity.TIME_STAMP))
                        {
                            this.errmessage.MessageBoxShow("E080", "");
                            return;
                        }
                    }

                    //PhuocLoc 2022/03/08 #161248 -Start
                    if (!string.IsNullOrEmpty(this.ORIGINAL_CONTROL_NUMBER.Text))
                    {
                        M_WANSIGN_KEIYAKU_INFO[] listWansignKeiyakuInfoEntityCheck = this.logic.wanSignKeiyakuInfoDao.GetDataDuplicate(this.logic.headerForm.DOCUMENT_ID.Text, this.logic.form.ORIGINAL_CONTROL_NUMBER.Text);
                        if (listWansignKeiyakuInfoEntityCheck != null && listWansignKeiyakuInfoEntityCheck.Length > 0)
                        {
                            var resultSub = this.errmessage.MessageBoxShow("C132");
                            if (resultSub != DialogResult.Yes)
                            {
                                return;
                            }
                        }
                    }
                    //PhuocLoc 2022/03/08 #161248 -End

                    bool catchErr = this.logic.CreateEntity(false);
                    if (catchErr)
                    {
                        return;
                    }
                    using (var tran = new Transaction())
                    {
                        // 更新
                        if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        {
                            if (this.logic.WanSignDocumentDetailUpdate())
                            {
                                this.logic.Update(base.RegistErrorFlag);
                            }
                            else
                            {
                                return;
                            }
                        }
                        if (base.RegistErrorFlag)
                        {
                            return;
                        }
                        tran.Commit();
                    }
                    this.errmessage.MessageBoxShow("I030");
                }

                this.logic.Search();

                // 検索結果を画面に設定
                this.logic.WindowInitCtrl();
                this.logic.SetDataForWindow();
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Update(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                if (this.logic.CheckRegistData())
                {
                    return;
                }

                this.logic.listWansignKeiyakuInfoEntity = this.logic.wanSignKeiyakuInfoDao.GetDataByDocumentId(this.logic.headerForm.DOCUMENT_ID.Text);
                if (this.logic.listWansignKeiyakuInfoEntity == null || this.logic.listWansignKeiyakuInfoEntity.Length == 0)
                {
                    return;
                }

                foreach (M_WANSIGN_KEIYAKU_INFO wansignInfoEntity in this.logic.listWansignKeiyakuInfoEntity)
                {
                    if (wansignInfoEntity.CONTROL_NUMBER == this.logic.wanSignKeiyakuInfoEntity.CONTROL_NUMBER
                        && ConvertStrByte.ByteToString(wansignInfoEntity.TIME_STAMP) != ConvertStrByte.ByteToString(this.logic.wanSignKeiyakuInfoEntity.TIME_STAMP))
                    {
                        this.errmessage.MessageBoxShow("E080", "");
                        return;
                    }
                }

                //PhuocLoc 2022/03/08 #161248 -Start
                if (!string.IsNullOrEmpty(this.ORIGINAL_CONTROL_NUMBER.Text))
                {
                    M_WANSIGN_KEIYAKU_INFO[] listWansignKeiyakuInfoEntityCheck = this.logic.wanSignKeiyakuInfoDao.GetDataDuplicate(this.logic.headerForm.DOCUMENT_ID.Text, this.logic.form.ORIGINAL_CONTROL_NUMBER.Text);
                    if (listWansignKeiyakuInfoEntityCheck != null && listWansignKeiyakuInfoEntityCheck.Length > 0)
                    {
                        var resultSub = this.errmessage.MessageBoxShow("C132");
                        if (resultSub != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }
                //PhuocLoc 2022/03/08 #161248 -End

                bool catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }
                using (var tran = new Transaction())
                {
                    // 更新
                    if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    {
                        if (this.logic.WanSignDocumentDetailUpdate())
                        {
                            this.logic.Update(base.RegistErrorFlag);
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (base.RegistErrorFlag)
                    {
                        return;
                    }
                    tran.Commit();
                }
                this.errmessage.MessageBoxShow("I030");

                this.logic.Search();

                // 検索結果を画面に設定
                this.logic.WindowInitCtrl();
                this.logic.SetDataForWindow();
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            base.CloseTopForm();
        }

        /// <summary>
        /// (F5)契約イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SetKeiyakuFrom(object sender, EventArgs e)
        {
            this.logic.SetKeiyakuFrom();
        }

        /// <summary>
        /// 契約書ﾀﾞｳﾝﾛｰﾄﾞ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void KeiyakushoDownload(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //[2]契約書ダウンロード
                if (this.logic.wanSignKeiyakuInfoEntity.SIGNING_DATETIME.IsNull
                    || string.IsNullOrEmpty(this.logic.wanSignKeiyakuInfoEntity.SIGNING_DATETIME.Value.ToString()))
                {
                    this.errmessage.MessageBoxShow("E343");
                    return;
                }

                var folderName = this.logic.SetOutputFolder();
                //出力先フォルダ＝入力無
                if (string.IsNullOrEmpty(folderName))
                {
                    return;
                }

                //出力先フォルダ＝入力有
                var res = this.logic.KeiyakuDownLoad(folderName);
                if (res)
                {
                    this.errmessage.MessageBoxShow("I001", "ダウンロード");
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("KeiyakushoDownload", ex);
                this.errmessage.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 紐付SystemIdのDoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIMOZUKE_SYSTEM_ID_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.HIMOZUKE_SYSTEM_ID.Text) && !string.IsNullOrEmpty(this.SYSTEM_ID.Text))
            {
                this.HIMOZUKE_SYSTEM_ID.Text = this.SYSTEM_ID.Text;
            }
        }

        /// <summary>
        /// 紐付SystemIdのEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIMOZUKE_SYSTEM_ID_Enter(object sender, EventArgs e)
        {
            if (!this.logic.isError)
            {
                this.logic.preValue = this.HIMOZUKE_SYSTEM_ID.Text;
            }
        }

        /// <summary>
        /// 紐付SystemIdのValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIMOZUKE_SYSTEM_ID_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HIMOZUKE_SYSTEM_ID.Text))
            {
                if (this.logic.preValue != this.HIMOZUKE_SYSTEM_ID.Text)
                {
                    this.logic.SearchKeiyakuInfo();
                }
            }
            else
            {
                this.logic.ClearKeiyakuInfo();
            }
        }

        /// <summary>
        /// 紐付SystemIdのValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIMOZUKE_SYSTEM_ID_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HIMOZUKE_SYSTEM_ID.Text))
            {
                this.HIMOZUKE_SYSTEM_ID.Text = this.HIMOZUKE_SYSTEM_ID.Text.PadLeft(9, '0');
                if (this.logic.preValue != this.HIMOZUKE_SYSTEM_ID.Text && !this.logic.CheckKeiyakuInfo())
                {
                    // メッセージ表示
                    this.errmessage.MessageBoxShow("E045");
                    this.logic.isError = true;
                    e.Cancel = true;
                }
                else
                {
                    this.logic.isError = false;
                }
            }
        }

        /// <summary>
        /// 自動更新のTextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IS_AUTO_UPDATING_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeAutoUpdating();
        }

        /// <summary>
        /// ﾘﾏｲﾝﾄﾞ通知（WAN）のTextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IS_REMINDER_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeReminder();
        }

        /// <summary>
        /// 更新期間のBeforePopイベント
        /// </summary>
        public void RENEWWAL_PERIOD_UNIT_BeforePop()
        {
            this.RENEWWAL_PERIOD_UNIT.PopupDataHeaderTitle = new string[] { DenshiBunshoHoshuConstans.HEADER_RENEWWAL_PERIOD_UNIT_CD, DenshiBunshoHoshuConstans.HEADER_RENEWWAL_PERIOD_UNIT_NAME };
            this.RENEWWAL_PERIOD_UNIT.PopupDataSource = this.logic.RenewalPeriodUnitData;
        }

        /// <summary>
        /// 更新期間のValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RENEWWAL_PERIOD_UNIT_Validated(object sender, EventArgs e)
        {
            this.logic.SetRenewalPeriodUnitName();
        }

        /// <summary>
        /// 解約通知期限（WAN）のBeforePopイベント
        /// </summary>
        public void CANCEL_PERIOD_UNIT_BeforePop()
        {
            this.CANCEL_PERIOD_UNIT.PopupDataHeaderTitle = new string[] { DenshiBunshoHoshuConstans.HEADER_CANCEL_PERIOD_UNIT_CD, DenshiBunshoHoshuConstans.HEADER_CANCEL_PERIOD_UNIT_NAME };
            this.CANCEL_PERIOD_UNIT.PopupDataSource = this.logic.CancelPeriodUnitData;
        }

        /// <summary>
        /// 解約通知期限（WAN）のValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CANCEL_PERIOD_UNIT_Validated(object sender, EventArgs e)
        {
            this.logic.SetCancelPeriodUnitName();
        }

        /// <summary>
        /// ﾘﾏｲﾝﾄﾞ通知（WAN）のBeforePopイベント
        /// </summary>
        public void REMINDER_PERIOD_UNIT_BeforePop()
        {
            this.REMINDER_PERIOD_UNIT.PopupDataHeaderTitle = new string[] { DenshiBunshoHoshuConstans.HEADER_REMINDER_PERIOD_UNIT_CD, DenshiBunshoHoshuConstans.HEADER_REMINDER_PERIOD_UNIT_NAME };
            this.REMINDER_PERIOD_UNIT.PopupDataSource = this.logic.ReminderPeriodUnitData;
        }

        /// <summary>
        /// ﾘﾏｲﾝﾄﾞ通知（WAN）のValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void REMINDER_PERIOD_UNIT_Validated(object sender, EventArgs e)
        {
            this.logic.SetReminderPeriodUnitName();
        }

        /// <summary>
        /// フィールド０１のEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_1_Enter(object sender, EventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.IsNull)
            {
                switch (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.Value)
                {
                    case 1:
                        this.FIELD_STR_1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;
                    case 2:
                        this.FIELD_STR_1.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        this.FIELD_STR_1.Text = this.logic.GetResultText(this.FIELD_STR_1);
                        break;
                    case 3:
                        this.FIELD_STR_1.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                }
            }
        }

        /// <summary>
        /// フィールド０２のEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_2_Enter(object sender, EventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.IsNull)
            {
                switch (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.Value)
                {
                    case 1:
                        this.FIELD_STR_2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;
                    case 2:
                        this.FIELD_STR_2.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        this.FIELD_STR_2.Text = this.logic.GetResultText(this.FIELD_STR_2);
                        break;
                    case 3:
                        this.FIELD_STR_2.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                }
            }
        }

        /// <summary>
        /// フィールド０３のEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_3_Enter(object sender, EventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.IsNull)
            {
                switch (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.Value)
                {
                    case 1:
                        this.FIELD_STR_3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;
                    case 2:
                        this.FIELD_STR_3.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        this.FIELD_STR_3.Text = this.logic.GetResultText(this.FIELD_STR_3);
                        break;
                    case 3:
                        this.FIELD_STR_3.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                }
            }
        }

        /// <summary>
        /// フィールド０４のEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_4_Enter(object sender, EventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.IsNull)
            {
                switch (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.Value)
                {
                    case 1:
                        this.FIELD_STR_4.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;
                    case 2:
                        this.FIELD_STR_4.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        this.FIELD_STR_4.Text = this.logic.GetResultText(this.FIELD_STR_4);
                        break;
                    case 3:
                        this.FIELD_STR_4.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                }
            }
        }

        /// <summary>
        /// フィールド０５のEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_5_Enter(object sender, EventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.IsNull)
            {
                switch (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.Value)
                {
                    case 1:
                        this.FIELD_STR_5.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;
                    case 2:
                        this.FIELD_STR_5.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        this.FIELD_STR_5.Text = this.logic.GetResultText(this.FIELD_STR_5);
                        break;
                    case 3:
                        this.FIELD_STR_5.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                }
            }
        }

        /// <summary>
        /// フィールドのKeyPressイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_KeyPress(int type, CustomTextBox ctrl, object sender, KeyPressEventArgs e)
        {
            if (type == 2)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
                {
                    e.Handled = true;
                }
            }
            else if (type == 3)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '/'))
                {
                    e.Handled = true;
                }
                //全角スペースチェック KeyDownだと漢字ボタンと区別がつかないため、KeyPressで判断が必要(SHift+spaceで半角も来る）
                if (e.KeyChar == '　' || e.KeyChar == ' ')
                {
                    if (this is IDataGridViewEditingControl)
                    {
                        //エディティングコントロールの場合はGridのイベントで処理するので、ここでは何もしない
                    }
                    //ポップアップ設定がない場合は 自動設定
                    else
                    {
                        string bk = ctrl.PopupSetFormField;
                        try
                        {
                            ctrl.PopupWindowName = "カレンダーポップアップ";
                            ctrl.PopupSetFormField = ctrl.Name;
                            ctrl.PopUp();

                        }
                        finally
                        {
                            ctrl.PopupWindowName = "";
                            ctrl.PopupSetFormField = bk;
                        }
                    }

                    e.Handled = true;//入力キャンセル
                }
            }
        }

        /// <summary>
        /// フィールド０１のKeyPressイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.IsNull)
            {
                this.FIELD_STR_KeyPress(this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.Value, this.FIELD_STR_1, sender, e);
            }
        }

        /// <summary>
        /// フィールド０２のKeyPressイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.IsNull)
            {
                this.FIELD_STR_KeyPress(this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.Value, this.FIELD_STR_2, sender, e);
            }
        }

        /// <summary>
        /// フィールド０３のKeyPressイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.IsNull)
            {
                this.FIELD_STR_KeyPress(this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.Value, this.FIELD_STR_3, sender, e);
            }
        }

        /// <summary>
        /// フィールド０４のKeyPressイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.IsNull)
            {
                this.FIELD_STR_KeyPress(this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.Value, this.FIELD_STR_4, sender, e);
            }
        }

        /// <summary>
        /// フィールド０５のKeyPressイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.IsNull)
            {
                this.FIELD_STR_KeyPress(this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.Value, this.FIELD_STR_5, sender, e);
            }
        }

        /// <summary>
        /// フィールドのデータをチェックする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool CheckValidate(int type, string value, string name)
        {
            if (type == 2 && !this.logic.CheckValidNumber(value, name))
            {
                return false;
            }
            else if (type == 3 && !this.logic.CheckValidDate(value, name))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// フィールド０１のValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.FIELD_STR_1.Text) && !this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.IsNull && 
                !CheckValidate(this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.Value, this.FIELD_STR_1.Text, this.lbFIELD_1.Text))
            {
            	//PhuocLoc 2022/03/14 #161403 -Start
                // メッセージ表示
                if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.Value == 2)
                {
                    this.errmessage.MessageBoxShow("E335", this.lbFIELD_1.Text);
                }
                else if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.Value == 3)
                {
                    this.errmessage.MessageBoxShow("E336", this.lbFIELD_1.Text);
                }
                //PhuocLoc 2022/03/14 #161403 -End
                this.logic.isError = true;
                e.Cancel = true;
            }
            else
            {
                this.logic.isError = false;
            }
        }

        /// <summary>
        /// フィールド０２のValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.FIELD_STR_2.Text) && !this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.IsNull && 
                !CheckValidate(this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.Value, this.FIELD_STR_2.Text, this.lbFIELD_2.Text))
            {
            	//PhuocLoc 2022/03/14 #161403 -Start
                // メッセージ表示
                if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.Value == 2)
                {
                    this.errmessage.MessageBoxShow("E335", this.lbFIELD_2.Text);
                }
                else if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.Value == 3)
                {
                    this.errmessage.MessageBoxShow("E336", this.lbFIELD_2.Text);
                }
                //PhuocLoc 2022/03/14 #161403 -End
                this.logic.isError = true;
                e.Cancel = true;
            }
            else
            {
                this.logic.isError = false;
            }
        }

        /// <summary>
        /// フィールド０３のValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.FIELD_STR_3.Text) && !this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.IsNull && 
                !CheckValidate(this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.Value, this.FIELD_STR_3.Text, this.lbFIELD_3.Text))
            {
            	//PhuocLoc 2022/03/14 #161403 -Start
                // メッセージ表示
                if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.Value == 2)
                {
                    this.errmessage.MessageBoxShow("E335", this.lbFIELD_3.Text);
                }
                else if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.Value == 3)
                {
                    this.errmessage.MessageBoxShow("E336", this.lbFIELD_3.Text);
                }
                //PhuocLoc 2022/03/14 #161403 -End
                this.logic.isError = true;
                e.Cancel = true;
            }
            else
            {
                this.logic.isError = false;
            }
        }

        /// <summary>
        /// フィールド０４のValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_4_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.FIELD_STR_4.Text) && !this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.IsNull &&
                !CheckValidate(this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.Value, this.FIELD_STR_4.Text, this.lbFIELD_4.Text))
            {
            	//PhuocLoc 2022/03/14 #161403 -Start
                // メッセージ表示
                if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.Value == 2)
                {
                    this.errmessage.MessageBoxShow("E335", this.lbFIELD_4.Text);
                }
                else if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.Value == 3)
                {
                    this.errmessage.MessageBoxShow("E336", this.lbFIELD_4.Text);
                }
                //PhuocLoc 2022/03/14 #161403 -End
                this.logic.isError = true;
                e.Cancel = true;
            }
            else
            {
                this.logic.isError = false;
            }
        }

        /// <summary>
        /// フィールド０５のValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_5_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.FIELD_STR_5.Text) && !this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.IsNull && 
                !CheckValidate(this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.Value, this.FIELD_STR_5.Text, this.lbFIELD_5.Text))
            {
            	//PhuocLoc 2022/03/14 #161403 -Start
                // メッセージ表示
                if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.Value == 2)
                {
                    this.errmessage.MessageBoxShow("E335", this.lbFIELD_5.Text);
                }
                else if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.Value == 3)
                {
                    this.errmessage.MessageBoxShow("E336", this.lbFIELD_5.Text);
                }
                //PhuocLoc 2022/03/14 #161403 -End
                this.logic.isError = true;
                e.Cancel = true;
            }
            else
            {
                this.logic.isError = false;
            }
        }

        /// <summary>
        /// フィールド０１のLeaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_1_Leave(object sender, EventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.IsNull)
            {
                if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.Value == 2)
                {
                    this.logic.ChangeNumberFormat(this.FIELD_STR_1);
                }
                else if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.Value == 3)
                {
                    this.logic.ChangeDateTimeFormat(this.FIELD_STR_1);
                }
            }
        }

        /// <summary>
        /// フィールド０２のLeaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_2_Leave(object sender, EventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.IsNull)
            {
                if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.Value == 2)
                {
                    this.logic.ChangeNumberFormat(this.FIELD_STR_2);
                }
                else if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.Value == 3)
                {
                    this.logic.ChangeDateTimeFormat(this.FIELD_STR_2);
                }
            }
        }

        /// <summary>
        /// フィールド０３のLeaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_3_Leave(object sender, EventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.IsNull)
            {
                if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.Value == 2)
                {
                    this.logic.ChangeNumberFormat(this.FIELD_STR_3);
                }
                else if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.Value == 3)
                {
                    this.logic.ChangeDateTimeFormat(this.FIELD_STR_3);
                }
            }
        }

        /// <summary>
        /// フィールド０４のLeaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_4_Leave(object sender, EventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.IsNull)
            {
                if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.Value == 2)
                {
                    this.logic.ChangeNumberFormat(this.FIELD_STR_4);
                }
                else if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.Value == 3)
                {
                    this.logic.ChangeDateTimeFormat(this.FIELD_STR_4);
                }
            }
        }

        /// <summary>
        /// フィールド０５のLeaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FIELD_STR_5_Leave(object sender, EventArgs e)
        {
            if (!this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.IsNull)
            {
                if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.Value == 2)
                {
                    this.logic.ChangeNumberFormat(this.FIELD_STR_5);
                }
                else if (this.logic.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.Value == 3)
                {
                    this.logic.ChangeDateTimeFormat(this.FIELD_STR_5);
                }
            }
        }

        /// <summary>
        /// タブコントロールのSelectingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabData_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (this.logic.isError)
            {
                e.Cancel = true;
            }
        }

        //PhuocLoc 2022/03/08 #161249 -Start
        private void HIMOZUKE_SYSTEM_ID_BUTTON_Click(object sender, EventArgs e)
        {
            this.logic.SetKeiyakuFrom();
        }
        //PhuocLoc 2022/03/08 #161249 -End
    }
}
