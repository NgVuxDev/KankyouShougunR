using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Data;

namespace Shougun.Core.ReceiptPayManagement.NyuukinKoteiChouhyou
{
    /// <summary>
    /// 入金明細表出力画面クラス
    /// </summary>
    public partial class UIForm_NyuukinMeisaihyou : SuperForm
    {
        /// <summary>
        /// ロジッククラス
        /// </summary>
        private NyuukinMeisaihyouLogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">ウィンドウID</param>
        public UIForm_NyuukinMeisaihyou(WINDOW_ID windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new NyuukinMeisaihyouLogicClass(this);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面内の項目を初期化します
        /// </summary>
        public void Initialize()
        {
            LogUtility.DebugMethodStart();

            // 拠点CD
            this.KYOTEN_CD.Text = "99";
            // 拠点
            this.KYOTEN_NAME.Text = "全社";
            // 日付CD
            this.HIDUKE_SHURUI.Text = "1";
            // 日付範囲
            this.HIDUKE.Text = "1";
            // 日付From
            this.HIDUKE_FROM.Value = this.logic.parentForm.sysDate;
            // 日付To
            this.HIDUKE_TO.Value = this.logic.parentForm.sysDate;
            // 入金先CDFrom
            this.NYUUKINSAKI_CD_FROM.Text = String.Empty;
            // 入金先From
            this.NYUUKINSAKI_NAME_FROM.Text = String.Empty;
            // 入金先CDTo
            this.NYUUKINSAKI_CD_TO.Text = String.Empty;
            // 入金先To
            this.NYUUKINSAKI_NAME_TO.Text = String.Empty;
            // 取引先CDFrom
            this.TORIHIKISAKI_CD_FROM.Text = String.Empty;
            // 取引先From
            this.TORIHIKISAKI_NAME_FROM.Text = String.Empty;
            // 取引先CDTo
            this.TORIHIKISAKI_CD_TO.Text = String.Empty;
            // 取引先To
            this.TORIHIKISAKI_NAME_TO.Text = String.Empty;
            // 銀行CDFrom
            this.BANK_CD_FROM.Text = String.Empty;
            // 銀行From
            this.BANK_NAME_FROM.Text = String.Empty;
            // 銀行CDTo
            this.BANK_CD_TO.Text = String.Empty;
            // 銀行To
            this.BANK_NAME_TO.Text = String.Empty;
            // 銀行支店CDFrom
            this.BANK_SHITEN_CD_FROM.Text = String.Empty;
            // 銀行支店From
            this.BANK_SHITEN_NAME_FROM.Text = String.Empty;
            // 銀行支店CDTo
            this.BANK_SHITEN_CD_TO.Text = String.Empty;
            // 銀行支店To
            this.BANK_SHITEN_NAME_TO.Text = String.Empty;
            // 並び順１
            this.SORT_1_KOUMOKU.Text = "1";
            // 並び順２
            this.SORT_2_KOUMOKU.Text = "1";
            // 集計単位（伝票）
            this.GROUP_DENPYOU.Checked = true;
            // 集計単位（取引先）
            this.GROUP_TORIHIKISAKI.Checked = false;
            // 集計単位（入金先）
            this.GROUP_NYUUKINSAKI.Checked = true;
            // 集計単位（入金区分）
            this.GROUP_NYUUSHUKKIN_KBN.Checked = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV
        /// </summary>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Cursor.Current = Cursors.WaitCursor;
            if (!this.SetNyuukinsakiCd())
            {
                return;
            }
            if (!this.SetTorihikisakiCd())
            {
                return;
            }

            // 登録時チェックロジックを実行
            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();
            if (this.logic.CheckDate())
            {
                return;
            }

            // 入金先CD、取引先CDの背景色の変更がうまくいかないので自前で変更する
            if (String.IsNullOrEmpty(this.NYUUKINSAKI_CD_FROM.Text))
            {
                this.NYUUKINSAKI_CD_FROM.IsInputErrorOccured = true;
                this.NYUUKINSAKI_CD_FROM.UpdateBackColor();
            }
            if (String.IsNullOrEmpty(this.NYUUKINSAKI_CD_TO.Text))
            {
                this.NYUUKINSAKI_CD_TO.IsInputErrorOccured = true;
                this.NYUUKINSAKI_CD_TO.UpdateBackColor();
            }

            if (this.TORIHIKISAKI_CD_FROM.Enabled && String.IsNullOrEmpty(this.TORIHIKISAKI_CD_FROM.Text))
            {
                this.TORIHIKISAKI_CD_FROM.IsInputErrorOccured = true;
                this.TORIHIKISAKI_CD_FROM.UpdateBackColor();
            }
            if (this.TORIHIKISAKI_CD_TO.Enabled && String.IsNullOrEmpty(this.TORIHIKISAKI_CD_TO.Text))
            {
                this.TORIHIKISAKI_CD_TO.IsInputErrorOccured = true;
                this.TORIHIKISAKI_CD_TO.UpdateBackColor();
            }

            if (this.RegistErrorFlag)
            {
                // FromToの整合性チェックで、CausesValidation=falseのままになってしまうのでtrueに戻す
                this.NYUUKINSAKI_CD_FROM.CausesValidation = true;
                this.NYUUKINSAKI_CD_TO.CausesValidation = true;
                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;
                this.BANK_CD_FROM.CausesValidation = true;
                this.BANK_CD_TO.CausesValidation = true;
                this.BANK_SHITEN_CD_FROM.CausesValidation = true;
                this.BANK_SHITEN_CD_TO.CausesValidation = true;
            }
            else
            {
                var dto = new NyuukinMeisaihyouDtoClass();
                dto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
                dto.KyotenName = this.KYOTEN_NAME.Text;
                dto.DateShuruiCd = Int32.Parse(this.HIDUKE_SHURUI.Text);
                dto.DateFrom = ((DateTime)this.HIDUKE_FROM.Value).ToString("yyyy/MM/dd");
                dto.DateTo = ((DateTime)this.HIDUKE_TO.Value).ToString("yyyy/MM/dd");
                dto.NyuukinsakiCdFrom = this.NYUUKINSAKI_CD_FROM.Text;
                dto.NyuukinsakiFrom = this.NYUUKINSAKI_NAME_FROM.Text;
                dto.NyuukinsakiCdTo = this.NYUUKINSAKI_CD_TO.Text;
                dto.NyuukinsakiTo = this.NYUUKINSAKI_NAME_TO.Text;
                dto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                dto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
                dto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                dto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
                dto.BankCdFrom = this.BANK_CD_FROM.Text;
                dto.BankFrom = this.BANK_NAME_FROM.Text;
                dto.BankCdTo = this.BANK_CD_TO.Text;
                dto.BankTo = this.BANK_NAME_TO.Text;
                dto.BankShitenCdFrom = this.BANK_SHITEN_CD_FROM.Text;
                dto.BankShitenFrom = this.BANK_SHITEN_NAME_FROM.Text;
                dto.BankShitenCdTo = this.BANK_SHITEN_CD_TO.Text;
                dto.BankShitenTo = this.BANK_SHITEN_NAME_TO.Text;
                dto.Sort1 = Int32.Parse(this.SORT_1_KOUMOKU.Text);
                dto.Sort2 = Int32.Parse(this.SORT_2_KOUMOKU.Text);
                dto.IsGroupDenpyouNumber = this.GROUP_DENPYOU.Checked;
                dto.IsGroupTorihikisaki = this.GROUP_TORIHIKISAKI.Checked;
                dto.IsGroupNyuukinsaki = this.GROUP_NYUUKINSAKI.Checked;
                dto.IsGroupNyuushukkinKbn = this.GROUP_NYUUSHUKKIN_KBN.Checked;

                this.logic.CSV(dto);
            }

            Cursor.Current = Cursors.Arrow;

            LogUtility.DebugMethodEnd();
        }

        public void CsvReport(DataTable dt)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertDataTableToCsv(dt, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_NYUUKIN_MEISAIHYOU), this);
            }
        }

        /// <summary>
        /// 表示ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Cursor.Current = Cursors.WaitCursor;
            if (!this.SetNyuukinsakiCd())
            {
                return;
            }
            if (!this.SetTorihikisakiCd())
            {
                return;
            }

            // 登録時チェックロジックを実行
            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();
            // luning 20141023 「From　>　To」のアラート表示タイミング変更 start
            if (this.logic.CheckDate())
            {
                return;
            }
            // luning 20141023 「From　>　To」のアラート表示タイミング変更 end

            // 入金先CD、取引先CDの背景色の変更がうまくいかないので自前で変更する
            if (String.IsNullOrEmpty(this.NYUUKINSAKI_CD_FROM.Text))
            {
                this.NYUUKINSAKI_CD_FROM.IsInputErrorOccured = true;
                this.NYUUKINSAKI_CD_FROM.UpdateBackColor();
            }
            if (String.IsNullOrEmpty(this.NYUUKINSAKI_CD_TO.Text))
            {
                this.NYUUKINSAKI_CD_TO.IsInputErrorOccured = true;
                this.NYUUKINSAKI_CD_TO.UpdateBackColor();
            }

            if (this.TORIHIKISAKI_CD_FROM.Enabled && String.IsNullOrEmpty(this.TORIHIKISAKI_CD_FROM.Text))
            {
                this.TORIHIKISAKI_CD_FROM.IsInputErrorOccured = true;
                this.TORIHIKISAKI_CD_FROM.UpdateBackColor();
            }
            if (this.TORIHIKISAKI_CD_TO.Enabled && String.IsNullOrEmpty(this.TORIHIKISAKI_CD_TO.Text))
            {
                this.TORIHIKISAKI_CD_TO.IsInputErrorOccured = true;
                this.TORIHIKISAKI_CD_TO.UpdateBackColor();
            }

            if (this.RegistErrorFlag)
            {
                // FromToの整合性チェックで、CausesValidation=falseのままになってしまうのでtrueに戻す
                this.NYUUKINSAKI_CD_FROM.CausesValidation = true;
                this.NYUUKINSAKI_CD_TO.CausesValidation = true;
                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;
                this.BANK_CD_FROM.CausesValidation = true;
                this.BANK_CD_TO.CausesValidation = true;
                this.BANK_SHITEN_CD_FROM.CausesValidation = true;
                this.BANK_SHITEN_CD_TO.CausesValidation = true;
            }
            else
            {
                var dto = new NyuukinMeisaihyouDtoClass();
                dto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
                dto.KyotenName = this.KYOTEN_NAME.Text;
                dto.DateShuruiCd = Int32.Parse(this.HIDUKE_SHURUI.Text);
                dto.DateFrom = ((DateTime)this.HIDUKE_FROM.Value).ToString("yyyy/MM/dd");
                dto.DateTo = ((DateTime)this.HIDUKE_TO.Value).ToString("yyyy/MM/dd");
                dto.NyuukinsakiCdFrom = this.NYUUKINSAKI_CD_FROM.Text;
                dto.NyuukinsakiFrom = this.NYUUKINSAKI_NAME_FROM.Text;
                dto.NyuukinsakiCdTo = this.NYUUKINSAKI_CD_TO.Text;
                dto.NyuukinsakiTo = this.NYUUKINSAKI_NAME_TO.Text;
                dto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                dto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
                dto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                dto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
                dto.BankCdFrom = this.BANK_CD_FROM.Text;
                dto.BankFrom = this.BANK_NAME_FROM.Text;
                dto.BankCdTo = this.BANK_CD_TO.Text;
                dto.BankTo = this.BANK_NAME_TO.Text;
                dto.BankShitenCdFrom = this.BANK_SHITEN_CD_FROM.Text;
                dto.BankShitenFrom = this.BANK_SHITEN_NAME_FROM.Text;
                dto.BankShitenCdTo = this.BANK_SHITEN_CD_TO.Text;
                dto.BankShitenTo = this.BANK_SHITEN_NAME_TO.Text;
                dto.Sort1 = Int32.Parse(this.SORT_1_KOUMOKU.Text);
                dto.Sort2 = Int32.Parse(this.SORT_2_KOUMOKU.Text);
                dto.IsGroupDenpyouNumber = this.GROUP_DENPYOU.Checked;
                dto.IsGroupTorihikisaki = this.GROUP_TORIHIKISAKI.Checked;
                dto.IsGroupNyuukinsaki = this.GROUP_NYUUKINSAKI.Checked;
                dto.IsGroupNyuushukkinKbn = this.GROUP_NYUUSHUKKIN_KBN.Checked;

                this.logic.Search(dto);
            }

            Cursor.Current = Cursors.Arrow;

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
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            if (!this.logic.WindowInit())
            {
                return;
            }

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            this.Initialize();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 日付範囲テキストボックスのテキストが変更された時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HIDUKE_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hiduke = this.HIDUKE.Text;
            if (!String.IsNullOrEmpty(hiduke))
            {
                var fromTextBox = this.HIDUKE_FROM;
                var toTextBox = this.HIDUKE_TO;

                // 期間指定の時だけ入力可なので、入力不可に初期化しておく
                fromTextBox.Enabled = false;
                toTextBox.Enabled = false;

                switch (hiduke)
                {
                    case "1":
                        // 当日
                        fromTextBox.Value = this.logic.parentForm.sysDate;
                        toTextBox.Value = this.logic.parentForm.sysDate;
                        break;
                    case "2":
                        // 当月
                        fromTextBox.Value = new DateTime(this.logic.parentForm.sysDate.Year, this.logic.parentForm.sysDate.Month, 1);
                        toTextBox.Value = new DateTime(this.logic.parentForm.sysDate.Year, this.logic.parentForm.sysDate.Month, 1).AddMonths(1).AddDays(-1);
                        break;
                    case "3":
                        // 期間指定
                        fromTextBox.Text = String.Empty;
                        toTextBox.Text = String.Empty;
                        fromTextBox.Enabled = true;
                        toTextBox.Enabled = true;
                        break;
                    default:
                        fromTextBox.Text = String.Empty;
                        toTextBox.Text = String.Empty;
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 日付Toテキストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hidukeFromTextBox = this.HIDUKE_FROM;
            var hidukeToTextBox = this.HIDUKE_TO;
            //if (!String.IsNullOrEmpty(hidukeFromTextBox.Text) && String.IsNullOrEmpty(hidukeToTextBox.Text))
            //{
                hidukeToTextBox.Text = hidukeFromTextBox.Text;
            //}

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行コードFromテキストボックスのバリデーションが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void BANK_CD_FROM_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeBankShitenState();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行コードToテキストボックスのバリデーションが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void BANK_CD_TO_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeBankShitenState();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行コードテキストボックスの入力状態に応じて銀行支店項目の状態を変更します
        /// </summary>
        private bool ChangeBankShitenState()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var bankCdFrom = this.BANK_CD_FROM.Text;
                var bankCdTo = this.BANK_CD_TO.Text;

                var bankShitenCdFromTextBox = this.BANK_SHITEN_CD_FROM;
                var bankShitenCdToTextBox = this.BANK_SHITEN_CD_TO;
                var bankShitenNameFromTextBox = this.BANK_SHITEN_NAME_FROM;
                var bankShitenNameToTextBox = this.BANK_SHITEN_NAME_TO;
                var bankShitenFromPopup = this.BANK_SHITEN_FROM_POPUP;
                var bankShitenToPopup = this.BANK_SHITEN_TO_POPUP;

                if (!String.IsNullOrEmpty(bankCdFrom) && !String.IsNullOrEmpty(bankCdTo) && bankCdFrom == bankCdTo)
                {
                    bankShitenCdFromTextBox.Enabled = true;
                    //
                    //bankShitenCdFromTextBox.Focus();
                    //
                    bankShitenCdToTextBox.Enabled = true;
                    bankShitenNameFromTextBox.Enabled = true;
                    bankShitenNameToTextBox.Enabled = true;
                    bankShitenFromPopup.Enabled = true;
                    bankShitenToPopup.Enabled = true;
                }
                else
                {
                    bankShitenCdFromTextBox.Text = String.Empty;
                    bankShitenCdToTextBox.Text = String.Empty;
                    bankShitenNameFromTextBox.Text = String.Empty;
                    bankShitenNameToTextBox.Text = String.Empty;
                    bankShitenCdFromTextBox.Enabled = false;
                    bankShitenCdToTextBox.Enabled = false;
                    bankShitenNameFromTextBox.Enabled = false;
                    bankShitenNameToTextBox.Enabled = false;
                    bankShitenFromPopup.Enabled = false;
                    bankShitenToPopup.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeBankShitenState", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 並び順１のテキストボックスのテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SORT_1_KOUMOKU_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var sortTorihikisakiRadioButton = this.SORT_1_KOUMOKU_2;
            var groupTorihikisakiCheckBox = this.GROUP_TORIHIKISAKI;

            var torihikisakiCdFromTextBox = this.TORIHIKISAKI_CD_FROM;
            var torihikisakiNameFromTextBox = this.TORIHIKISAKI_NAME_FROM;
            var torihikisakiFromPopup = this.TORIHIKISAKI_FROM_POPUP;
            var torihikisakiCdToTextBox = this.TORIHIKISAKI_CD_TO;
            var torihikisakiNameToTextBox = this.TORIHIKISAKI_NAME_TO;
            var torihikisakiToPopup = this.TORIHIKISAKI_TO_POPUP;

            if (this.SORT_1_KOUMOKU.Text == "2")
            {
                groupTorihikisakiCheckBox.Checked = true;
                groupTorihikisakiCheckBox.Enabled = true;

                torihikisakiCdFromTextBox.Enabled = true;
                torihikisakiNameFromTextBox.Enabled = true;
                torihikisakiFromPopup.Enabled = true;
                torihikisakiCdToTextBox.Enabled = true;
                torihikisakiNameToTextBox.Enabled = true;
                torihikisakiToPopup.Enabled = true;
                // 201508 katen #11995 並び順の範囲指定を手入力変更しても使用可能とならない start‏
                this.NYUUKINSAKI_CD_FROM.Enabled = false;
                this.NYUUKINSAKI_NAME_FROM.Enabled = false;
                this.NYUUKINSAKI_FROM_POPUP.Enabled = false;
                this.NYUUKINSAKI_CD_TO.Enabled = false;
                this.NYUUKINSAKI_NAME_TO.Enabled = false;
                this.NYUUKINSAKI_TO_POPUP.Enabled = false;

                this.NYUUKINSAKI_CD_FROM.Text = String.Empty;
                this.NYUUKINSAKI_NAME_FROM.Text = String.Empty;
                this.NYUUKINSAKI_CD_TO.Text = String.Empty;
                this.NYUUKINSAKI_NAME_TO.Text = String.Empty;
                // 201508 katen #11995 並び順の範囲指定を手入力変更しても使用可能とならない end‏
            }
            else
            {
                groupTorihikisakiCheckBox.Checked = false;
                groupTorihikisakiCheckBox.Enabled = false;

                torihikisakiCdFromTextBox.Enabled = false;
                torihikisakiNameFromTextBox.Enabled = false;
                torihikisakiFromPopup.Enabled = false;
                torihikisakiCdToTextBox.Enabled = false;
                torihikisakiNameToTextBox.Enabled = false;
                torihikisakiToPopup.Enabled = false;

                torihikisakiCdFromTextBox.Text = String.Empty;
                torihikisakiNameFromTextBox.Text = String.Empty;
                torihikisakiCdToTextBox.Text = String.Empty;
                torihikisakiNameToTextBox.Text = String.Empty;
                // 201508 katen #11995 並び順の範囲指定を手入力変更しても使用可能とならない start‏
                this.NYUUKINSAKI_CD_FROM.Enabled = true;
                this.NYUUKINSAKI_NAME_FROM.Enabled = true;
                this.NYUUKINSAKI_FROM_POPUP.Enabled = true;
                this.NYUUKINSAKI_CD_TO.Enabled = true;
                this.NYUUKINSAKI_NAME_TO.Enabled = true;
                this.NYUUKINSAKI_TO_POPUP.Enabled = true;
                // 201508 katen #11995 並び順の範囲指定を手入力変更しても使用可能とならない end‏
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 並び順２のテキストボックスのテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SORT_2_KOUMOKU_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var sort2Koumoku = this.SORT_2_KOUMOKU.Text;
            if (this.SORT_2_KOUMOKU_5.Checked)
            {
                this.GROUP_NYUUSHUKKIN_KBN.Enabled = true;
                this.GROUP_NYUUSHUKKIN_KBN.Checked = true;
            }
            else
            {
                this.GROUP_NYUUSHUKKIN_KBN.Enabled = false;
                this.GROUP_NYUUSHUKKIN_KBN.Checked = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金先CDToのテキストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKINSAKI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var nyuukinsakiCdFromTextBox = this.NYUUKINSAKI_CD_FROM;
            var nyuukinsakiNameFromTextBox = this.NYUUKINSAKI_NAME_FROM;
            var nyuukinsakiCdToTextBox = this.NYUUKINSAKI_CD_TO;
            var nyuukinsakiNameToTextBox = this.NYUUKINSAKI_NAME_TO;

            //if (!String.IsNullOrEmpty(nyuukinsakiCdFromTextBox.Text) && String.IsNullOrEmpty(nyuukinsakiCdToTextBox.Text))
            //{
                nyuukinsakiCdToTextBox.Text = nyuukinsakiCdFromTextBox.Text;
                nyuukinsakiNameToTextBox.Text = nyuukinsakiNameFromTextBox.Text;
            //}

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDToのテキストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void TORIHIKISAKI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var torihikisakiCdFromTextBox = this.TORIHIKISAKI_CD_FROM;
            var torihikisakiNameFromTextBox = this.TORIHIKISAKI_NAME_FROM;
            var torihikisakiCdToTextBox = this.TORIHIKISAKI_CD_TO;
            var torihikisakiNameToTextBox = this.TORIHIKISAKI_NAME_TO;

            //if (!String.IsNullOrEmpty(torihikisakiCdFromTextBox.Text) && String.IsNullOrEmpty(torihikisakiCdToTextBox.Text))
            //{
                torihikisakiCdToTextBox.Text = torihikisakiCdFromTextBox.Text;
                torihikisakiNameToTextBox.Text = torihikisakiNameFromTextBox.Text;
            //}

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行CDToのテキストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void BANK_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCdFromTextBox = this.BANK_CD_FROM;
            var bankNameFromTextBox = this.BANK_NAME_FROM;
            var bankCdToTextBox = this.BANK_CD_TO;
            var bankNameToTextBox = this.BANK_NAME_TO;

            //if (!String.IsNullOrEmpty(bankCdFromTextBox.Text) && String.IsNullOrEmpty(bankCdToTextBox.Text))
            //{
                bankCdToTextBox.Text = bankCdFromTextBox.Text;
                bankNameToTextBox.Text = bankNameFromTextBox.Text;
            //}

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行支店CDToのテキストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void BANK_SHITEN_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankShitenCdFromTextBox = this.BANK_SHITEN_CD_FROM;
            var bankShitenNameFromTextBox = this.BANK_SHITEN_NAME_FROM;
            var bankShitenCdToTextBox = this.BANK_SHITEN_CD_TO;
            var bankShitenNameToTextBox = this.BANK_SHITEN_NAME_TO;

            //if (!String.IsNullOrEmpty(bankShitenCdFromTextBox.Text) && String.IsNullOrEmpty(bankShitenCdToTextBox.Text))
            //{
                bankShitenCdToTextBox.Text = bankShitenCdFromTextBox.Text;
                bankShitenNameToTextBox.Text = bankShitenNameFromTextBox.Text;
            //}

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行支店CDFromテキストボックスのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void BANK_SHITEN_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.BANK_CD_FROM.Text;
            var bankShitenCd = this.BANK_SHITEN_CD_FROM.Text;

            if (String.IsNullOrEmpty(bankShitenCd))
            {
                this.BANK_SHITEN_NAME_FROM.Text = String.Empty;
            }
            else
            {
                bool catchErr = true;
                var mBankShiten = this.logic.GetBankShiten(bankCd, bankShitenCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (null == mBankShiten)
                {
                    var messageBoxShowLogic = new MessageBoxShowLogic();
                    messageBoxShowLogic.MessageBoxShow("E011", "銀行支店マスタ");

                    // 該当なし
                    this.BANK_SHITEN_NAME_FROM.Text = String.Empty;

                    e.Cancel = true;
                }
                else
                {
                    this.BANK_SHITEN_NAME_FROM.Text = mBankShiten.BANK_SHIETN_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行支店CDToテキストボックスのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void BANK_SHITEN_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.BANK_CD_TO.Text;
            var bankShitenCd = this.BANK_SHITEN_CD_TO.Text;

            if (String.IsNullOrEmpty(bankShitenCd))
            {
                this.BANK_SHITEN_NAME_TO.Text = String.Empty;
            }
            else
            {
                bool catchErr = true;
                var mBankShiten = this.logic.GetBankShiten(bankCd, bankShitenCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (null == mBankShiten)
                {
                    var messageBoxShowLogic = new MessageBoxShowLogic();
                    messageBoxShowLogic.MessageBoxShow("E011", "銀行支店マスタ");

                    // 該当なし
                    this.BANK_SHITEN_NAME_TO.Text = String.Empty;

                    e.Cancel = true;
                }
                else
                {
                    this.BANK_SHITEN_NAME_TO.Text = mBankShiten.BANK_SHIETN_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金先CDをセットします（入金先CDが入力されていないとき）
        /// </summary>
        private bool SetNyuukinsakiCd()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 入金先が入力されていなければ自動入力する
                var nyuukinsakiCdFrom = this.NYUUKINSAKI_CD_FROM.Text;
                var nyuukinsakiCdTo = this.NYUUKINSAKI_CD_TO.Text;
                if (String.IsNullOrEmpty(nyuukinsakiCdFrom) && String.IsNullOrEmpty(nyuukinsakiCdTo))
                {
                    var dao = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
                    var mNyuukinsakiList = dao.GetAllValidData(new M_NYUUKINSAKI());
                    var firstNyuukinsaki = mNyuukinsakiList.OrderBy(n => n.NYUUKINSAKI_CD).FirstOrDefault();
                    var lastNyuukinsaki = mNyuukinsakiList.OrderBy(n => n.NYUUKINSAKI_CD).LastOrDefault();
                    if (null != firstNyuukinsaki)
                    {
                        this.NYUUKINSAKI_CD_FROM.Text = firstNyuukinsaki.NYUUKINSAKI_CD;
                        this.NYUUKINSAKI_NAME_FROM.Text = firstNyuukinsaki.NYUUKINSAKI_NAME_RYAKU;
                    }
                    if (null != lastNyuukinsaki)
                    {
                        this.NYUUKINSAKI_CD_TO.Text = lastNyuukinsaki.NYUUKINSAKI_CD;
                        this.NYUUKINSAKI_NAME_TO.Text = lastNyuukinsaki.NYUUKINSAKI_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetNyuukinsakiCd", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetNyuukinsakiCd", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 取引先CDをセットします（取引先CDが入力されていないとき）
        /// </summary>
        private bool SetTorihikisakiCd()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 取引先が入力可で、入力されていなければ自動入力する
                if (this.TORIHIKISAKI_CD_FROM.Enabled && this.TORIHIKISAKI_CD_TO.Enabled)
                {
                    var torihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                    var torihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                    if (String.IsNullOrEmpty(torihikisakiCdFrom) && String.IsNullOrEmpty(torihikisakiCdTo))
                    {
                        var dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                        var mTorihikisakiList = dao.GetAllValidData(new M_TORIHIKISAKI() { ISNOT_NEED_DELETE_FLG = true });
                        var firstTorihikisaki = mTorihikisakiList.OrderBy(t => t.TORIHIKISAKI_CD).FirstOrDefault();
                        var lastTorihikisaki = mTorihikisakiList.OrderBy(t => t.TORIHIKISAKI_CD).LastOrDefault();
                        if (null != firstTorihikisaki)
                        {
                            this.TORIHIKISAKI_CD_FROM.Text = firstTorihikisaki.TORIHIKISAKI_CD;
                            this.TORIHIKISAKI_NAME_FROM.Text = firstTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                        if (null != lastTorihikisaki)
                        {
                            this.TORIHIKISAKI_CD_TO.Text = lastTorihikisaki.TORIHIKISAKI_CD;
                            this.TORIHIKISAKI_NAME_TO.Text = lastTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihikisakiCd", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 銀行CDをセットします（銀行CDが入力されていないとき）
        /// </summary>
        private void SetBankCd()
        {
            LogUtility.DebugMethodStart();

            // 銀行が入力されていなければ自動入力する
            var bankCdFrom = this.BANK_CD_FROM.Text;
            var bankCdTo = this.BANK_CD_TO.Text;
            if (String.IsNullOrEmpty(bankCdFrom) && String.IsNullOrEmpty(bankCdTo))
            {
                var dao = DaoInitUtility.GetComponent<IM_BANKDao>();
                var mBankList = dao.GetAllValidData(new M_BANK());
                var firstBank = mBankList.OrderBy(b => b.BANK_CD).FirstOrDefault();
                var lastBank = mBankList.OrderBy(b => b.BANK_CD).LastOrDefault();
                if (null != firstBank)
                {
                    this.BANK_CD_FROM.Text = firstBank.BANK_CD;
                    this.BANK_NAME_FROM.Text = firstBank.BANK_NAME_RYAKU;
                }
                if (null != lastBank)
                {
                    this.BANK_CD_TO.Text = lastBank.BANK_CD;
                    this.BANK_NAME_TO.Text = lastBank.BANK_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #region 20150618 #10107 hoanghm
        /// <summary>
        /// 銀行CD Toテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_CD_TO_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeBankCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行支店CDテキストボックスの使用可否を切り替えます
        /// </summary>
        private bool ChangeBankCdTextBoxEnabled()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var fromCd = this.BANK_CD_FROM.Text;
                var toCd = this.BANK_CD_TO.Text;
                if (!String.IsNullOrEmpty(fromCd) && !String.IsNullOrEmpty(toCd) && fromCd.PadLeft(4, '0') == toCd.PadLeft(4, '0'))
                {
                    this.BANK_SHITEN_CD_FROM.Enabled = true;
                    this.BANK_SHITEN_CD_TO.Enabled = true;
                    this.BANK_SHITEN_FROM_POPUP.Enabled = true;
                    this.BANK_SHITEN_TO_POPUP.Enabled = true;
                    this.BANK_SHITEN_NAME_FROM.Enabled = true;
                    this.BANK_SHITEN_NAME_TO.Enabled = true;
                }
                else
                {
                    this.BANK_SHITEN_CD_FROM.Text = String.Empty;
                    this.BANK_SHITEN_CD_TO.Text = String.Empty;
                    this.BANK_SHITEN_NAME_FROM.Text = String.Empty;
                    this.BANK_SHITEN_NAME_TO.Text = String.Empty;
                    this.BANK_SHITEN_CD_FROM.Enabled = false;
                    this.BANK_SHITEN_CD_TO.Enabled = false;
                    this.BANK_SHITEN_FROM_POPUP.Enabled = false;
                    this.BANK_SHITEN_TO_POPUP.Enabled = false;
                    this.BANK_SHITEN_NAME_FROM.Enabled = false;
                    this.BANK_SHITEN_NAME_TO.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeBankCdTextBoxEnabled", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        #endregion
    }
}
