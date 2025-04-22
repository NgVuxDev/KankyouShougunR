using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using System.Data;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.ReceiptPayManagement.NyuukinShuukeiChouhyou
{
    /// <summary>
    /// 入金集計表出力画面クラス
    /// </summary>
    public partial class UIForm_NyuukinShuukeihyou : SuperForm
    {
        /// <summary>
        /// ロジッククラス
        /// </summary>
        private NyuukinShuukeihyouLogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">ウィンドウID</param>
        public UIForm_NyuukinShuukeihyou(WINDOW_ID windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new NyuukinShuukeihyouLogicClass(this);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面内の項目を初期化します
        /// </summary>
        public void Initialize()
        {
            LogUtility.DebugMethodStart();

            // 抽出条件
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
            // 入金区分CDFrom
            this.NYUUKIN_KBN_FROM.Text = String.Empty;
            // 入金区分From
            this.NYUUKIN_KBN_NAME_FROM.Text = String.Empty;
            // 入金区分CDTo
            this.NYUUKIN_KBN_TO.Text = String.Empty;
            // 入金区分To
            this.NYUUKIN_KBN_NAME_TO.Text = String.Empty;
            // 抽出条件

            LogUtility.DebugMethodEnd();
        }

        #region ボタンイベント
        /// <summary>
        /// [F1]新規ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc1_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var patternDto = new PatternDto();
            patternDto.Pattern.WINDOW_ID = (int)this.WindowId;
            this.ShowTourokuPopup(WINDOW_TYPE.NEW_WINDOW_FLAG, patternDto);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F2]修正ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc2_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.patternList1.GetSelectedPatternDto() != null)
            {
                this.ShowTourokuPopup(WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.patternList1.GetSelectedPatternDto());
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F4]削除ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc4_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.patternList1.GetSelectedPatternDto() != null)
            {
                if (!this.ShowTourokuPopup(WINDOW_TYPE.DELETE_WINDOW_FLAG, this.patternList1.GetSelectedPatternDto()))
                {
                    return;
                }
            }

            if (this.patternList1.GetSelectedPatternDto() == null)
            {
                this.patternList1.ClearKoumoku();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F5]CSV
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
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

            PatternDto patternDao
                    = this.patternList1.GetSelectedPatternDto();

            if (patternDao == null)
            {
                MessageBox.Show("パターンを選択してください。", "アラート",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.patternList1.Focus();
                this.patternList1.PATTERN_LIST_BOX.BackColor = Constans.ERROR_COLOR;
                return;
            }

            // 登録時チェックロジックを実行
            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();
            if (this.RegistErrorFlag)
            {
                var control = (Control)this.allControl.OfType<ICustomAutoChangeBackColor>().OrderBy(c => ((Control)c).TabIndex).Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                control.Focus();
            }
            else
            {
                var dto = new NyuukinShuukeihyouDtoClass();
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
                dto.NyuukinKbnCdFrom = this.NYUUKIN_KBN_FROM.Text;
                dto.NyuukinKbnFrom = this.NYUUKIN_KBN_NAME_FROM.Text;
                dto.NyuukinKbnCdTo = this.NYUUKIN_KBN_TO.Text;
                dto.NyuukinKbnTo = this.NYUUKIN_KBN_NAME_TO.Text;
                dto.Jyouken1 = this.CreateConditionString(dto, 1);
                dto.Jyouken2 = this.CreateConditionString(dto, 2);

                dto.Pattern = patternDao;

                for (int i = 0; i < patternDao.PatternColumnList.Count; i++)
                {
                    if (patternDao.PatternColumnList[i].DETAIL_KBN)
                    {
                        string ronriName = patternDao.ColumnSelectList[i].KOUMOKU_RONRI_NAME;
                        string butsuriBame = patternDao.ColumnSelectDetailList[i].BUTSURI_NAME;

                        dto.ShuukeiIsChecked.Add(ronriName, butsuriBame);
                    }
                }

                this.logic.CSV(dto);
            }

            this.NYUUKINSAKI_CD_FROM.CausesValidation = true;
            this.NYUUKINSAKI_CD_TO.CausesValidation = true;
            this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
            this.TORIHIKISAKI_CD_TO.CausesValidation = true;
            this.BANK_CD_FROM.CausesValidation = true;
            this.BANK_CD_TO.CausesValidation = true;
            this.BANK_SHITEN_CD_FROM.CausesValidation = true;
            this.BANK_SHITEN_CD_TO.CausesValidation = true;

            Cursor.Current = Cursors.Arrow;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F7]表示ボタンがクリックされたときに処理します
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

            PatternDto patternDao
                    = this.patternList1.GetSelectedPatternDto();

            if (patternDao == null)
            {
                MessageBox.Show("パターンを選択してください。", "アラート",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.patternList1.Focus();
                this.patternList1.PATTERN_LIST_BOX.BackColor = Constans.ERROR_COLOR;
                return;
            }

            // 登録時チェックロジックを実行
            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();
            if (this.RegistErrorFlag)
            {
                var control = (Control)this.allControl.OfType<ICustomAutoChangeBackColor>().OrderBy(c => ((Control)c).TabIndex).Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                control.Focus();
            }
            else
            {
                var dto = new NyuukinShuukeihyouDtoClass();
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
                dto.NyuukinKbnCdFrom = this.NYUUKIN_KBN_FROM.Text;
                dto.NyuukinKbnFrom = this.NYUUKIN_KBN_NAME_FROM.Text;
                dto.NyuukinKbnCdTo = this.NYUUKIN_KBN_TO.Text;
                dto.NyuukinKbnTo = this.NYUUKIN_KBN_NAME_TO.Text;
                dto.Jyouken1 = this.CreateConditionString(dto, 1);
                dto.Jyouken2 = this.CreateConditionString(dto, 2);

                dto.Pattern = patternDao;

                for (int i = 0; i < patternDao.PatternColumnList.Count; i++)
                {
                    if (patternDao.PatternColumnList[i].DETAIL_KBN)
                    {
                        string ronriName = patternDao.ColumnSelectList[i].KOUMOKU_RONRI_NAME;
                        string butsuriBame = patternDao.ColumnSelectDetailList[i].BUTSURI_NAME;

                        dto.ShuukeiIsChecked.Add(ronriName, butsuriBame);
                    }
                }

                this.logic.Search(dto);
            }

            this.NYUUKINSAKI_CD_FROM.CausesValidation = true;
            this.NYUUKINSAKI_CD_TO.CausesValidation = true;
            this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
            this.TORIHIKISAKI_CD_TO.CausesValidation = true;
            this.BANK_CD_FROM.CausesValidation = true;
            this.BANK_CD_TO.CausesValidation = true;
            this.BANK_SHITEN_CD_FROM.CausesValidation = true;
            this.BANK_SHITEN_CD_TO.CausesValidation = true;

            Cursor.Current = Cursors.Arrow;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F12]閉じるボタンがクリックされたときに処理します
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
        #endregion

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

            this.patternList1.SetWindowId(this.WindowId);

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
        private void ChangeBankShitenState()
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
                bankShitenCdToTextBox.Enabled = true;
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
                bankShitenFromPopup.Enabled = false;
                bankShitenToPopup.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行CD Fromテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_CD_FROM_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeBankCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

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

            nyuukinsakiCdToTextBox.Text = nyuukinsakiCdFromTextBox.Text;
            nyuukinsakiNameToTextBox.Text = nyuukinsakiNameFromTextBox.Text;

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

            torihikisakiCdToTextBox.Text = torihikisakiCdFromTextBox.Text;
            torihikisakiNameToTextBox.Text = torihikisakiNameFromTextBox.Text;

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

            bankCdToTextBox.Text = bankCdFromTextBox.Text;
            bankNameToTextBox.Text = bankNameFromTextBox.Text;

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

            bankShitenCdToTextBox.Text = bankShitenCdFromTextBox.Text;
            bankShitenNameToTextBox.Text = bankShitenNameFromTextBox.Text;

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
        /// 銀行支店CDテキストボックスの使用可否を切り替えます
        /// </summary>
        private void ChangeBankCdTextBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            var fromCd = this.BANK_CD_FROM.Text;
            var toCd = this.BANK_CD_TO.Text;
            //if (!String.IsNullOrEmpty(fromCd) && !String.IsNullOrEmpty(toCd) && fromCd == toCd)
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
                var dao = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
                var mNyuukinsakiList = dao.GetAllValidData(new M_NYUUKINSAKI());
                var firstNyuukinsaki = mNyuukinsakiList.OrderBy(n => n.NYUUKINSAKI_CD).FirstOrDefault();
                var lastNyuukinsaki = mNyuukinsakiList.OrderBy(n => n.NYUUKINSAKI_CD).LastOrDefault();
                if (null != firstNyuukinsaki)
                {
                    if (String.IsNullOrEmpty(nyuukinsakiCdFrom))
                    {
                        this.NYUUKINSAKI_CD_FROM.Text = firstNyuukinsaki.NYUUKINSAKI_CD;
                        this.NYUUKINSAKI_NAME_FROM.Text = firstNyuukinsaki.NYUUKINSAKI_NAME_RYAKU;
                    }
                }
                if (null != lastNyuukinsaki)
                {
                    if (String.IsNullOrEmpty(nyuukinsakiCdTo))
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
                    var dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                    var mTorihikisakiList = dao.GetAllValidData(new M_TORIHIKISAKI());
                    var firstTorihikisaki = mTorihikisakiList.OrderBy(t => t.TORIHIKISAKI_CD).FirstOrDefault();
                    var lastTorihikisaki = mTorihikisakiList.OrderBy(t => t.TORIHIKISAKI_CD).LastOrDefault();
                    if (null != firstTorihikisaki)
                    {
                        if (String.IsNullOrEmpty(torihikisakiCdFrom))
                        {
                            this.TORIHIKISAKI_CD_FROM.Text = firstTorihikisaki.TORIHIKISAKI_CD;
                            this.TORIHIKISAKI_NAME_FROM.Text = firstTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                    }
                    if (null != lastTorihikisaki)
                    {
                        if (String.IsNullOrEmpty(torihikisakiCdTo))
                        {
                            this.TORIHIKISAKI_CD_TO.Text = lastTorihikisaki.TORIHIKISAKI_CD;
                            this.TORIHIKISAKI_NAME_TO.Text = lastTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetTorihikisakiCd", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                ret = false;
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

        /// <summary>
        /// 入金区分CDToのテキストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUUKIN_KBN_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.NYUUKIN_KBN_TO.Text = this.NYUUKIN_KBN_FROM.Text;
            this.NYUUKIN_KBN_NAME_TO.Text = this.NYUUKIN_KBN_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターン登録ポップアップを表示します
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <param name="dto">パターンDTOクラス</param>
        private bool ShowTourokuPopup(WINDOW_TYPE windowType, PatternDto dto)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(windowType, dto);

                var popup = new ChouhyouPatternTourokuPopupForm(windowType, dto);
                var dialogResult = popup.ShowDialog();
                popup.Dispose();

                if (DialogResult.Cancel != dialogResult)
                {
                    // ポップアップを閉じたらパターンのリストを再読込み
                    this.patternList1.SetWindowId(this.WindowId);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ShowTourokuPopup", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowTourokuPopup", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターンポップアップをダブルクリックしたときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void patternList1_PatternDoubleClicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            
            if (this.patternList1.GetSelectedPatternDto() != null)
            {
                this.KYOTEN_CD.Focus();
                this.ShowTourokuPopup(WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.patternList1.GetSelectedPatternDto());
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票に表示する条件の文字列を作成
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="pattern">
        ///  1 : 条件1
        ///  2 : 条件2
        /// </param>
        /// <returns></returns>
        private string CreateConditionString(NyuukinShuukeihyouDtoClass dto, int pattern)
        {
            StringBuilder sb = new StringBuilder();

            if (pattern == 1)
            {
                // 条件1
                sb.AppendLine("[抽出条件]");
                sb.AppendFormat("  [{0}] {1} ～ {2}", dto.DateShuruiCd == 1 ? "伝票日付" : "入力日付", dto.DateFrom, dto.DateTo);
                sb.AppendLine(string.Empty);
                sb.AppendFormat("  [拠点] {0}", dto.KyotenCd + " " + dto.KyotenName);
                sb.AppendLine(string.Empty);
                if (!string.IsNullOrEmpty(dto.NyuukinsakiCdFrom) || !string.IsNullOrEmpty(dto.NyuukinsakiCdTo))
                {
                    sb.AppendFormat("  [入金先]  {0} ～ {1}", dto.NyuukinsakiCdFrom + " " + dto.NyuukinsakiFrom, dto.NyuukinsakiCdTo + " " + dto.NyuukinsakiTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.TorihikisakiCdFrom) || !string.IsNullOrEmpty(dto.TorihikisakiCdTo))
                {
                    sb.AppendFormat("  [取引先]  {0} ～ {1}", dto.TorihikisakiCdFrom + " " + dto.TorihikisakiFrom, dto.TorihikisakiCdTo + " " + dto.TorihikisakiTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.BankCdFrom) || !string.IsNullOrEmpty(dto.BankCdTo))
                {
                    sb.AppendFormat("  [銀行]  {0} ～ {1}", dto.BankCdFrom + " " + dto.BankFrom, dto.BankCdTo + " " + dto.BankTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.BankShitenCdFrom) || !string.IsNullOrEmpty(dto.BankShitenCdTo))
                {
                    sb.AppendFormat("  [銀行支店]  {0} ～ {1}", dto.BankShitenCdFrom + " " + dto.BankShitenFrom, dto.BankShitenCdTo + " " + dto.BankShitenTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.NyuukinKbnCdFrom) || !string.IsNullOrEmpty(dto.NyuukinKbnCdTo))
                {
                    sb.AppendFormat("  [入金区分]  {0} ～ {1}", dto.NyuukinKbnCdFrom + " " + dto.NyuukinKbnFrom, dto.NyuukinKbnCdTo + " " + dto.NyuukinKbnTo);
                }
            }
            else
            {
                List<S_LIST_COLUMN_SELECT> denpyoList = this.patternList1.GetSelectedPatternDto().ColumnSelectList;
                if (denpyoList.Count > 0)
                {
                    sb.AppendLine("[集計項目]");
                    sb.AppendFormat("  [1]  {0}", denpyoList[0].KOUMOKU_RONRI_NAME);
                    for (int i = 1; i < denpyoList.Count; i++)
                    {
                        sb.AppendLine(string.Empty);
                        sb.AppendFormat("  [" + (i + 1) + "]  {0}", denpyoList[i].KOUMOKU_RONRI_NAME);
                    }
                    sb.AppendLine(string.Empty);
                    sb.AppendLine(string.Empty);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// CSV
        /// </summary>
        public void CsvReport(DataTable dt)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertDataTableToCsv(dt, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU), this);
            }
        }
    }
}
