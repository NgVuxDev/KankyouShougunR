using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ChouhyouPatternPopup;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Carriage.UnchinShukeiHyo
{
    /// <summary>
    /// 運賃集計表画面クラス
    /// </summary>
    public partial class UnchinShukeiHyoUIForm : SuperForm
    {
        /// <summary>
        /// 運賃集計表ロジッククラス
        /// </summary>
        private UnchinShukeiHyoLogic logic;

        /// <summary>
        /// 運賃集計表DTOを取得・設定します
        /// </summary>
        internal UnchinShukeiHyoDto FormDataDto { get; set; }

        /// <summary>
        /// 車輌Fromを入力するポップアップを開いたかのフラグ
        /// </summary>
        private bool isSharyouFromPopupOpen = false;

        /// <summary>
        /// 車輌CD Fromを入力する前の値
        /// </summary>
        private string beforeSharyouCdFrom;

        /// <summary>
        /// 車輌Toを入力するポップアップを開いたかのフラグ
        /// </summary>
        private bool isSharyouToPopupOpen = false;

        /// <summary>
        /// 車輌CD Toを入力する前の値
        /// </summary>
        private string beforeSharyouCdTo;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private string preNioroshiGyoushaFrCd { get; set; }
        private string preNioroshiGyoushaToCd { get; set; }
        private string preNizumiGyoushaFrCd { get; set; }
        private string preNizumiGyoushaToCd { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnchinShukeiHyoUIForm()
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();

            this.WindowId = WINDOW_ID.T_UNCHIN_SHUUKEIHYOU;

            this.logic = new UnchinShukeiHyoLogic(this);

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

            this.logic.WindowInit();

            this.PATTERN_LIST_BOX.SetWindowId(this.WindowId);
            this.SetDefault();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面の初期値をセットします
        /// </summary>
        private void SetDefault()
        {
            this.DATE_SHURUI.Text = UnchinShukeiHyoConst.DATE_SHURUI_CD_DENPYOU;
            this.DATE_RANGE.Text = UnchinShukeiHyoConst.DATE_RANGE_CD_TOUJITSU;
            this.DENPYOU_SHURUI.Text = UnchinShukeiHyoConst.DENPYOU_SHURUI_CD_SUBETE;
            this.KYOTEN_CD.Text = UnchinShukeiHyoConst.KYOTEN_CD_ZENSHA;
            this.KYOTEN_NAME.Text = UnchinShukeiHyoConst.KYOTEN_NAME_99;
        }

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

            if (this.PATTERN_LIST_BOX.GetSelectedPatternDto() != null)
            {
                this.ShowTourokuPopup(WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.PATTERN_LIST_BOX.GetSelectedPatternDto());
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

            if (this.PATTERN_LIST_BOX.GetSelectedPatternDto() != null)
            {
                this.ShowTourokuPopup(WINDOW_TYPE.DELETE_WINDOW_FLAG, this.PATTERN_LIST_BOX.GetSelectedPatternDto());
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Ｆ5キー（表示）ボタンが押された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.PATTERN_LIST_BOX.GetSelectedPatternDto() == null)
            {
                MessageBox.Show("パターンを選択してください。", "アラート",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.PATTERN_LIST_BOX.Focus();
                this.PATTERN_LIST_BOX.PATTERN_LIST_BOX.BackColor = Constans.ERROR_COLOR;
                return;
            }

            if (this.RegistErrorFlag == false)
            {
                // 日付の必須チェックとFromToチェックを両方セットすると正常にエラーチェックされないので
                // FromToチェックは画面独自で実装
                if (this.IsErrorDateFromTo(this.DATE_FROM, this.DATE_TO))
                {
                    new MessageBoxShowLogic().MessageBoxShow("E030", this.DATE_FROM.DisplayItemName, this.DATE_TO.DisplayItemName);
                    this.RegistErrorFlag = true;
                }
                else if (this.PATTERN_LIST_BOX.GetSelectedPatternDto() != null)
                {
                    this.SetDtoData();

                    var count = this.logic.Search();
                    if (count > 0)
                    {
                        if (!this.logic.CSVPrint()) { return; }
                    }
                    else if (count == 0)
                    {
                        new MessageBoxShowLogic().MessageBoxShow("C001");
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (this.RegistErrorFlag == true)
            {
                var control = (Control)this.allControl.OfType<ICustomAutoChangeBackColor>().OrderBy(c => ((Control)c).TabIndex).Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                control.Focus();
            }

            this.RecoveryFocusOutCheckMethod();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F7]表示ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.PATTERN_LIST_BOX.GetSelectedPatternDto() == null)
            {
                MessageBox.Show("パターンを選択してください。", "アラート",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.PATTERN_LIST_BOX.Focus();
                this.PATTERN_LIST_BOX.PATTERN_LIST_BOX.BackColor = Constans.ERROR_COLOR;
                return;
            }

            if (this.RegistErrorFlag == false)
            {
                // 日付の必須チェックとFromToチェックを両方セットすると正常にエラーチェックされないので
                // FromToチェックは画面独自で実装
                if (this.IsErrorDateFromTo(this.DATE_FROM, this.DATE_TO))
                {
                    new MessageBoxShowLogic().MessageBoxShow("E030", this.DATE_FROM.DisplayItemName, this.DATE_TO.DisplayItemName);
                    this.RegistErrorFlag = true;
                }
                else if (this.PATTERN_LIST_BOX.GetSelectedPatternDto() != null)
                {
                    this.SetDtoData();

                    var count = this.logic.Search();
                    if (count > 0)
                    {
                        if (!this.logic.CreateForm()) { return; }
                    }
                    else if (count == 0)
                    {
                        new MessageBoxShowLogic().MessageBoxShow("C001");
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (this.RegistErrorFlag == true)
            {
                var control = (Control)this.allControl.OfType<ICustomAutoChangeBackColor>().OrderBy(c => ((Control)c).TabIndex).Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                control.Focus();
            }

            this.RecoveryFocusOutCheckMethod();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F12]閉じるボタンをクリックしたときに処理します
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
        /// パターンDTOに画面からデータをセットします
        /// </summary>
        private void SetDtoData()
        {
            LogUtility.DebugMethodStart();

            this.FormDataDto = new UnchinShukeiHyoDto();

            this.FormDataDto.Pattern = this.PATTERN_LIST_BOX.GetSelectedPatternDto();
            var parentForm = (BusinessBaseForm)this.Parent;

            this.FormDataDto.DateShurui = Int32.Parse(this.DATE_SHURUI.Text);
            if (this.DATE_SHURUI_1.Checked)
            {
                this.FormDataDto.DateShuruiName = UnchinShukeiHyoConst.DATE_SHURUI_1;
            }
            else
            {
                this.FormDataDto.DateShuruiName = UnchinShukeiHyoConst.DATE_SHURUI_2;
            }
            if (this.DATE_RANGE.Text == UnchinShukeiHyoConst.DATE_RANGE_CD_TOUJITSU)
            {
                this.FormDataDto.DateFrom = parentForm.sysDate.Date;
                this.FormDataDto.DateTo = parentForm.sysDate.Date;
            }
            else if (this.DATE_RANGE.Text == UnchinShukeiHyoConst.DATE_RANGE_CD_TOUGETSU)
            {
                var today = parentForm.sysDate.Date;
                this.FormDataDto.DateFrom = today.AddDays(-today.Day + 1);
                this.FormDataDto.DateTo = this.FormDataDto.DateFrom.AddMonths(1).AddDays(-1);
            }
            else
            {
                this.FormDataDto.DateFrom = DateTime.Parse(this.DATE_FROM.Value.ToString());
                this.FormDataDto.DateTo = DateTime.Parse(this.DATE_TO.Value.ToString());
            }
            this.FormDataDto.DenpyouShurui = Int32.Parse(this.DENPYOU_SHURUI.Text);
            if (this.DENSHU_SHURUI_1.Checked)
            {
                this.FormDataDto.DenpyouShuruiName = UnchinShukeiHyoConst.DENPYOU_SHURUI_1;
            }
            else if (this.DENSHU_SHURUI_2.Checked)
            {
                this.FormDataDto.DenpyouShuruiName = UnchinShukeiHyoConst.DENPYOU_SHURUI_2;
            }
            else if (this.DENSHU_SHURUI_3.Checked)
            {
                this.FormDataDto.DenpyouShuruiName = UnchinShukeiHyoConst.DENPYOU_SHURUI_3;
            }
            else if (this.DENSHU_SHURUI_4.Checked)
            {
                this.FormDataDto.DenpyouShuruiName = UnchinShukeiHyoConst.DENPYOU_SHURUI_4;
            }
            else if (this.DENSHU_SHURUI_5.Checked)
            {
                this.FormDataDto.DenpyouShuruiName = UnchinShukeiHyoConst.DENPYOU_SHURUI_5;
            }
            else
            {
                this.FormDataDto.DenpyouShuruiName = UnchinShukeiHyoConst.DENPYOU_SHURUI_6;
            }
            if (string.IsNullOrEmpty(this.KYOTEN_CD.Text) == false)
            {
                this.FormDataDto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
            }
            this.FormDataDto.KyotenName = this.KYOTEN_NAME.Text;
            this.FormDataDto.NioroshiGyoushaCdFrom = this.NIOROSHI_GYOUSHA_CD_FROM.Text;
            this.FormDataDto.NioroshiGyoushaNameFrom = this.NIOROSHI_GYOUSHA_NAME_FROM.Text;
            this.FormDataDto.NioroshiGyoushaCdTo = this.NIOROSHI_GYOUSHA_CD_TO.Text;
            this.FormDataDto.NioroshiGyoushaNameTo = this.NIOROSHI_GYOUSHA_NAME_TO.Text;
            this.FormDataDto.NioroshiGenbaCdFrom = this.NIOROSHI_GENBA_CD_FROM.Text;
            this.FormDataDto.NioroshiGenbaNameFrom = this.NIOROSHI_GENBA_NAME_FROM.Text;
            this.FormDataDto.NioroshiGenbaCdTo = this.NIOROSHI_GENBA_CD_TO.Text;
            this.FormDataDto.NioroshiGenbaNameTo = this.NIOROSHI_GENBA_NAME_TO.Text;
            this.FormDataDto.NizumiGyoushaCdFrom = this.NIZUMI_GYOUSHA_CD_FROM.Text;
            this.FormDataDto.NizumiGyoushaNameFrom = this.NIZUMI_GYOUSHA_NAME_FROM.Text;
            this.FormDataDto.NizumiGyoushaCdTo = this.NIZUMI_GYOUSHA_CD_TO.Text;
            this.FormDataDto.NizumiGyoushaNameTo = this.NIZUMI_GYOUSHA_NAME_TO.Text;
            this.FormDataDto.NizumiGenbaCdFrom = this.NIZUMI_GENBA_CD_FROM.Text;
            this.FormDataDto.NizumiGenbaNameFrom = this.NIZUMI_GENBA_NAME_FROM.Text;
            this.FormDataDto.NizumiGenbaCdTo = this.NIZUMI_GENBA_CD_TO.Text;
            this.FormDataDto.NizumiGenbaNameTo = this.NIZUMI_GENBA_NAME_TO.Text;
            this.FormDataDto.UnpanGyoushaCdFrom = this.UNPAN_GYOUSHA_CD_FROM.Text;
            this.FormDataDto.UnpanGyoushaNameFrom = this.UNPAN_GYOUSHA_NAME_FROM.Text;
            this.FormDataDto.UnpanGyoushaCdTo = this.UNPAN_GYOUSHA_CD_TO.Text;
            this.FormDataDto.UnpanGyoushaNameTo = this.UNPAN_GYOUSHA_NAME_TO.Text;
            this.FormDataDto.ShashuCdFrom = this.SHASHU_CD_FROM.Text;
            this.FormDataDto.ShashuNameFrom = this.SHASHU_NAME_FROM.Text;
            this.FormDataDto.ShashuCdTo = this.SHASHU_CD_TO.Text;
            this.FormDataDto.ShashuNameTo = this.SHASHU_NAME_TO.Text;
            this.FormDataDto.SharyouCdFrom = this.SHARYOU_CD_FROM.Text;
            this.FormDataDto.SharyouNameFrom = this.SHARYOU_NAME_FROM.Text;
            this.FormDataDto.SharyouCdTo = this.SHARYOU_CD_TO.Text;
            this.FormDataDto.SharyouNameTo = this.SHARYOU_NAME_TO.Text;
            this.FormDataDto.KeitaiKbnCdFrom = this.KEITAI_KBN_CD_FROM.Text;
            this.FormDataDto.KeitaiKbnNameFrom = this.KEITAI_KBN_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターン登録ポップアップを表示します
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <param name="dto">パターンDTOクラス</param>
        private void ShowTourokuPopup(WINDOW_TYPE windowType, PatternDto dto)
        {
            LogUtility.DebugMethodStart(windowType, dto);

            var popup = new ChouhyouPatternTourokuPopupForm(windowType, dto);
            var dialogResult = popup.ShowDialog();
            popup.Dispose();

            if (DialogResult.Cancel != dialogResult)
            {
                // ポップアップを閉じたらパターンのリストを再読込み
                this.PATTERN_LIST_BOX.SetWindowId(this.WindowId);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターンリストをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void PATTERN_LIST_BOX_PatternDoubleClicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.PATTERN_LIST_BOX.GetSelectedPatternDto() != null)
            {
                this.DATE_SHURUI.Focus();
                this.ShowTourokuPopup(WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.PATTERN_LIST_BOX.GetSelectedPatternDto());
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 日付範囲テキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DATE_RANGE_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.DATE_RANGE.Text == UnchinShukeiHyoConst.DATE_RANGE_CD_SHITEI)
            {
                this.DATE_FROM.Enabled = true;
                this.DATE_TO.Enabled = true;
            }
            else
            {
                this.DATE_FROM.Enabled = false;
                this.DATE_TO.Enabled = false;

                this.DATE_FROM.Value = null;
                this.DATE_TO.Value = null;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 日付Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!string.IsNullOrEmpty(this.DATE_FROM.Text))
            {
                this.DATE_TO.Text = this.DATE_FROM.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void TORIHIKISAKI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 業者CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// 現場CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// 品名CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HINMEI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降業者CD Fromテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIOROSHI_GYOUSHA_CD_FROM_TextChanged(object sender, EventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);

            //this.ChangeNioroshiGenbaCdTextBoxEnabled();

            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降業者CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIOROSHI_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降業者CD Toテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIOROSHI_GYOUSHA_CD_TO_TextChanged(object sender, EventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);

            //this.ChangeNioroshiGenbaCdTextBoxEnabled();

            //LogUtility.DebugMethodEnd();
        }

        public void NIOROSHI_GYOUSHA_PopupAfterExecuteMethod()
        {
            this.ChangeNioroshiGenbaCdTextBoxEnabled();
            this.preNioroshiGyoushaFrCd = this.NIOROSHI_GYOUSHA_CD_FROM.Text;
            this.preNioroshiGyoushaToCd = this.NIOROSHI_GYOUSHA_CD_TO.Text;
        }

        /// <summary>
        /// 荷降現場CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIOROSHI_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積業者CD Fromテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIZUMI_GYOUSHA_CD_FROM_TextChanged(object sender, EventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);

            //this.ChangeNizumiGenbaCdTextBoxEnabled();

            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積業者CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIZUMI_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積業者CD Toテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIZUMI_GYOUSHA_CD_TO_TextChanged(object sender, EventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);

            //this.ChangeNizumiGenbaCdTextBoxEnabled();

            //LogUtility.DebugMethodEnd();
        }

        public void NIZUMI_GYOUSHA_PopupAfterExecuteMethod()
        {
            this.ChangeNizumiGenbaCdTextBoxEnabled();
            this.preNizumiGyoushaFrCd = this.NIZUMI_GYOUSHA_CD_FROM.Text;
            this.preNizumiGyoushaToCd = this.NIZUMI_GYOUSHA_CD_TO.Text;
        }

        /// <summary>
        /// 荷積現場CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIZUMI_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 営業担当者CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void EIGYOU_TANTOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入力担当者CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUURYOKU_TANTOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬業者CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void UNPAN_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車種CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHASHU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        private bool SharyouFlag = true;
        /// <summary>
        /// 車輌CD Fromテキストボックスに入力があったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_FROM_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (SharyouFlag)
            {
                this.beforeSharyouCdFrom = this.SHARYOU_CD_FROM.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌CD Fromテキストボックスのバリデーションが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            var shashuCd = this.SHASHU_CD_FROM.Text;
            var sharyouCd = this.SHARYOU_CD_FROM.Text;
            if (string.IsNullOrEmpty(sharyouCd) == false && sharyouCd != this.beforeSharyouCdFrom)
            {
                var sharyouList = this.logic.GetSharyou(this.SHASHU_CD_FROM.Text, this.SHARYOU_CD_FROM.Text);
                if (sharyouList.Count() == 0)
                {
                    this.SHARYOU_CD_FROM.BackColor = Constans.ERROR_COLOR;
                    new MessageBoxShowLogic().MessageBoxShow("E020", "車輌");
                    e.Cancel = true;
                    SharyouFlag = false;
                }
                else if (sharyouList.Count() > 1)
                {
                    if (this.isSharyouFromPopupOpen == false || sharyouCd != this.beforeSharyouCdFrom)
                    {
                        this.isSharyouFromPopupOpen = true;
                        this.beforeSharyouCdFrom = this.SHARYOU_CD_FROM.Text;
                        CustomControlExtLogic.PopUp(this.SHARYOU_CD_FROM);
                        e.Cancel = true;
                    }
                    else
                    {
                        this.isSharyouFromPopupOpen = false;
                    }
                    SharyouFlag = true;
                }
                else
                {
                    var sharyou = sharyouList.FirstOrDefault();
                    this.SHARYOU_NAME_FROM.Text = sharyou.SHARYOU_NAME_RYAKU;
                    SharyouFlag = true;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌CD Fromテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_FROM_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (string.IsNullOrEmpty(this.SHARYOU_CD_FROM.Text))
            {
                this.SHARYOU_NAME_FROM.Text = string.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌選択ポップアップ Fromが閉じた後に処理します
        /// </summary>
        public void SHARYOU_FROM_POPUP_After()
        {
            LogUtility.DebugMethodStart();

            this.beforeSharyouCdFrom = this.SHARYOU_CD_FROM.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌CD Toテキストボックスに入力があったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_TO_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (SharyouFlag)
            {
                this.beforeSharyouCdTo = this.SHARYOU_CD_TO.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌CD Toテキストボックスのバリデーションが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var shashuCd = this.SHASHU_CD_TO.Text;
            var sharyouCd = this.SHARYOU_CD_TO.Text;
            if (string.IsNullOrEmpty(sharyouCd) == false && sharyouCd != this.beforeSharyouCdTo)
            {
                var sharyouList = this.logic.GetSharyou(this.SHASHU_CD_TO.Text, this.SHARYOU_CD_TO.Text);
                if (sharyouList.Count() == 0)
                {
                    this.SHARYOU_CD_TO.BackColor = Constans.ERROR_COLOR;
                    new MessageBoxShowLogic().MessageBoxShow("E020", "車輌");
                    e.Cancel = true;
                    SharyouFlag = false;
                }
                else if (sharyouList.Count() > 1)
                {
                    if (this.isSharyouToPopupOpen == false || sharyouCd != this.beforeSharyouCdTo)
                    {
                        this.isSharyouToPopupOpen = true;
                        this.beforeSharyouCdTo = this.SHARYOU_CD_TO.Text;
                        CustomControlExtLogic.PopUp(this.SHARYOU_CD_TO);
                        e.Cancel = true;
                    }
                    else
                    {
                        this.isSharyouToPopupOpen = false;
                    }
                    SharyouFlag = true;
                }
                else
                {
                    var sharyou = sharyouList.FirstOrDefault();
                    this.SHARYOU_NAME_TO.Text = sharyou.SHARYOU_NAME_RYAKU;
                    SharyouFlag = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌CD Toテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_TO_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (string.IsNullOrEmpty(this.SHARYOU_CD_TO.Text))
            {
                this.SHARYOU_NAME_TO.Text = string.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌選択ポップアップ Toが閉じた後に処理します
        /// </summary>
        public void SHARYOU_TO_POPUP_After()
        {
            LogUtility.DebugMethodStart();

            this.beforeSharyouCdTo = this.SHARYOU_CD_TO.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 形態区分CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void KEITAI_KBN_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 台貫CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DAIKAN_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// FromテキストボックスからToテキストボックスにCDと名称をコピーします
        /// </summary>
        /// <param name="sender">CdFromテキストボックス</param>
        private void SetToCdAndName(object sender)
        {
            LogUtility.DebugMethodStart(sender);

            var cdToTextBox = (TextBox)sender;
            var cdFromTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_TO", "_FROM")).FirstOrDefault();
            if (!string.IsNullOrEmpty(cdFromTextBox.Text))
            {
                cdToTextBox.Text = cdFromTextBox.Text;

                var nameFromTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_CD_TO", "_NAME_FROM")).FirstOrDefault();
                var nameToTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_CD_TO", "_NAME_TO")).FirstOrDefault();
                nameToTextBox.Text = nameFromTextBox.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降現場CDテキストボックスの使用可否を切り替えます
        /// </summary>
        private void ChangeNioroshiGenbaCdTextBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            var fromCd = this.NIOROSHI_GYOUSHA_CD_FROM.Text;
            var toCd = this.NIOROSHI_GYOUSHA_CD_TO.Text;
            if (string.IsNullOrEmpty(fromCd) == false && string.IsNullOrEmpty(toCd) == false && this.ZeroSuppressGenbaCd(fromCd) == this.ZeroSuppressGenbaCd(toCd))
            {
                this.NIOROSHI_GENBA_CD_FROM.Enabled = true;
                this.NIOROSHI_GENBA_CD_TO.Enabled = true;
                this.NIOROSHI_GENBA_POPUP_FROM.Enabled = true;
                this.NIOROSHI_GENBA_POPUP_TO.Enabled = true;
            }
            else
            {
                this.NIOROSHI_GENBA_CD_FROM.Text = string.Empty;
                this.NIOROSHI_GENBA_CD_TO.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME_FROM.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME_TO.Text = string.Empty;
                this.NIOROSHI_GENBA_CD_FROM.Enabled = false;
                this.NIOROSHI_GENBA_CD_TO.Enabled = false;
                this.NIOROSHI_GENBA_POPUP_FROM.Enabled = false;
                this.NIOROSHI_GENBA_POPUP_TO.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積現場CDテキストボックスの使用可否を切り替えます
        /// </summary>
        private void ChangeNizumiGenbaCdTextBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            var fromCd = this.NIZUMI_GYOUSHA_CD_FROM.Text;
            var toCd = this.NIZUMI_GYOUSHA_CD_TO.Text;
            if (string.IsNullOrEmpty(fromCd) == false && string.IsNullOrEmpty(toCd) == false && this.ZeroSuppressGenbaCd(fromCd) == this.ZeroSuppressGenbaCd(toCd))
            {
                this.NIZUMI_GENBA_CD_FROM.Enabled = true;
                this.NIZUMI_GENBA_CD_TO.Enabled = true;
                this.NIZUMI_GENBA_POPUP_FROM.Enabled = true;
                this.NIZUMI_GENBA_POPUP_TO.Enabled = true;
            }
            else
            {
                this.NIZUMI_GENBA_CD_FROM.Text = string.Empty;
                this.NIZUMI_GENBA_CD_TO.Text = string.Empty;
                this.NIZUMI_GENBA_NAME_FROM.Text = string.Empty;
                this.NIZUMI_GENBA_NAME_TO.Text = string.Empty;
                this.NIZUMI_GENBA_CD_FROM.Enabled = false;
                this.NIZUMI_GENBA_CD_TO.Enabled = false;
                this.NIZUMI_GENBA_POPUP_FROM.Enabled = false;
                this.NIZUMI_GENBA_POPUP_TO.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDのゼロ埋め処理を行います
        /// </summary>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>ゼロ埋めした現場CD</returns>
        private string ZeroSuppressGenbaCd(string genbaCd)
        {
            LogUtility.DebugMethodStart(genbaCd);

            var ret = string.Empty;
            if (string.IsNullOrEmpty(genbaCd) == false)
            {
                ret = genbaCd.ToUpper().PadLeft(6, '0');
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 現場のチェックとテキストボックスへの略称セットを行います
        /// </summary>
        /// <param name="gyoushaCdTextBox">業者CDテキストボックス</param>
        /// <param name="genbaCdTextBox">現場CDテキストボックス</param>
        /// <param name="genbaNameTextBox">現場名テキストボックス</param>
        /// <returns>エラーがなければ、True</returns>
        private bool GenbaCheckAndSetting(TextBox gyoushaCdTextBox, TextBox genbaCdTextBox, TextBox genbaNameTextBox)
        {
            LogUtility.DebugMethodStart(gyoushaCdTextBox, genbaCdTextBox, genbaNameTextBox);

            var ret = false;

            if (string.IsNullOrEmpty(genbaCdTextBox.Text))
            {
                genbaNameTextBox.Text = string.Empty;
                ret = true;
            }
            else
            {
                var mGenba = this.logic.GetGenba(gyoushaCdTextBox.Text, genbaCdTextBox.Text);
                if (mGenba == null)
                {
                    genbaCdTextBox.BackColor = Constans.ERROR_COLOR;
                    new MessageBoxShowLogic().MessageBoxShow("E020", "現場");
                    genbaNameTextBox.Text = string.Empty;
                }
                else
                {
                    genbaNameTextBox.Text = mGenba.GENBA_NAME_RYAKU;
                    ret = true;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入力担当者のチェックとテキストボックスへの略称セットを行います
        /// </summary>
        /// <param name="cdTextBox">入力担当者CDテキストボックス</param>
        /// <param name="nameTextBox">入力担当者名テキストボックス</param>
        /// <returns>エラーがなければ、True</returns>
        private bool NyuurokuTantoushaCheckAndSetting(TextBox cdTextBox, TextBox nameTextBox)
        {
            LogUtility.DebugMethodStart(cdTextBox, nameTextBox);

            var ret = false;

            if (string.IsNullOrEmpty(cdTextBox.Text))
            {
                nameTextBox.Text = string.Empty;
                ret = true;
            }
            else
            {
                var mShain = this.logic.GetNyuuryokuTantousha(cdTextBox.Text);
                if (mShain == null)
                {
                    cdTextBox.BackColor = Constans.ERROR_COLOR;
                    new MessageBoxShowLogic().MessageBoxShow("E020", "入力担当者");
                    nameTextBox.Text = string.Empty;
                }
                else
                {
                    nameTextBox.Text = mShain.SHAIN_NAME_RYAKU;
                    ret = true;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 日付範囲のFromToチェックを行います
        /// </summary>
        /// <param name="fromTextBox">日付Fromテキストボックス</param>
        /// <param name="toTextBox">日付Toテキストボックス</param>
        /// <returns>エラーがある場合は、True</returns>
        private bool IsErrorDateFromTo(CustomDateTimePicker fromTextBox, CustomDateTimePicker toTextBox)
        {
            LogUtility.DebugMethodStart(fromTextBox, toTextBox);

            var ret = false;

            var fromDate = fromTextBox.Text;
            var toDate = toTextBox.Text;
            if (string.IsNullOrEmpty(fromDate) == false && string.IsNullOrEmpty(toDate) == false)
            {
                if (fromDate.CompareTo(toDate) > 0)
                {
                    fromTextBox.IsInputErrorOccured = true;
                    toTextBox.IsInputErrorOccured = true;
                    ret = true;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 登録時のチェックでFromToのチェックが入力エラーになると、FocusOutCheckMethodが動作しなくなる対策
        /// （ValidatorでCausesValidationをfalseにしたままになる不具合）
        /// </summary>
        private void RecoveryFocusOutCheckMethod()
        {
            LogUtility.DebugMethodStart();

            this.NIOROSHI_GYOUSHA_CD_FROM.CausesValidation = true;
            this.NIOROSHI_GYOUSHA_CD_TO.CausesValidation = true;
            this.NIOROSHI_GENBA_CD_FROM.CausesValidation = true;
            this.NIOROSHI_GENBA_CD_TO.CausesValidation = true;
            this.NIZUMI_GYOUSHA_CD_FROM.CausesValidation = true;
            this.NIZUMI_GYOUSHA_CD_TO.CausesValidation = true;
            this.NIZUMI_GENBA_CD_FROM.CausesValidation = true;
            this.NIZUMI_GENBA_CD_TO.CausesValidation = true;
            this.UNPAN_GYOUSHA_CD_FROM.CausesValidation = true;
            this.UNPAN_GYOUSHA_CD_TO.CausesValidation = true;
            this.SHASHU_CD_FROM.CausesValidation = true;
            this.SHASHU_CD_TO.CausesValidation = true;
            this.SHARYOU_CD_FROM.CausesValidation = true;
            this.SHARYOU_CD_TO.CausesValidation = true;
            this.KEITAI_KBN_CD_FROM.CausesValidation = true;
            LogUtility.DebugMethodEnd();
        }

        #region 運搬業者
        /// <summary>
        /// 運搬業者FROM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.isChanged(sender)) { return; }//変更なし、戻る
            this.logic.CheckUpanGyoushaCdFrom(this.UNPAN_GYOUSHA_CD_FROM, this.UNPAN_GYOUSHA_NAME_FROM, e);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬業者TO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.isChanged(sender)) { return; }//変更なし、戻る
            this.logic.CheckUpanGyoushaCdFrom(this.UNPAN_GYOUSHA_CD_TO, this.UNPAN_GYOUSHA_NAME_TO, e);
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 荷降
        /// <summary>
        /// 荷降業者FROM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            //if (this.preNioroshiGyoushaFrCd == this.NIOROSHI_GYOUSHA_CD_FROM.Text) { return; }//変更なし、戻る
            this.logic.CheckNioroshiGyoushaCdFrom(this.NIOROSHI_GYOUSHA_CD_FROM, this.NIOROSHI_GYOUSHA_NAME_FROM);
            this.ChangeNioroshiGenbaCdTextBoxEnabled();
            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 荷降業者TO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            //if (this.preNioroshiGyoushaToCd == this.NIOROSHI_GYOUSHA_CD_TO.Text) { return; }//変更なし、戻る
            this.logic.CheckNioroshiGyoushaCdFrom(this.NIOROSHI_GYOUSHA_CD_TO, this.NIOROSHI_GYOUSHA_NAME_TO);
            this.ChangeNioroshiGenbaCdTextBoxEnabled();
            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 荷降現場FROM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NIOROSHI_GENBA_CD_FROM_Validated(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!this.logic.isChanged(sender)) { return; }//変更なし、戻る
            this.logic.CheckNioroshiGenbaCdFrom(this.NIOROSHI_GENBA_CD_FROM, this.NIOROSHI_GYOUSHA_CD_FROM, this.NIOROSHI_GENBA_NAME_FROM);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降現場TO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NIOROSHI_GENBA_CD_TO_Validated(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!this.logic.isChanged(sender)) { return; }//変更なし、戻る
            this.logic.CheckNioroshiGenbaCdFrom(this.NIOROSHI_GENBA_CD_TO, this.NIOROSHI_GYOUSHA_CD_TO, this.NIOROSHI_GENBA_NAME_TO);
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 荷積

        /// <summary>
        /// 荷積業者FROM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {

            LogUtility.DebugMethodStart(sender, e);
            //if (this.preNizumiGyoushaFrCd == this.NIZUMI_GYOUSHA_CD_FROM.Text) { return; }//変更なし、戻る
            this.logic.CheckNizumiGyoushaCdFrom(this.NIZUMI_GYOUSHA_CD_FROM, this.NIZUMI_GYOUSHA_NAME_FROM);
            this.ChangeNizumiGenbaCdTextBoxEnabled();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積業者TO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            //if (this.preNizumiGyoushaToCd == this.NIZUMI_GYOUSHA_CD_TO.Text) { return; }//変更なし、戻る
            this.logic.CheckNizumiGyoushaCdFrom(this.NIZUMI_GYOUSHA_CD_TO, this.NIZUMI_GYOUSHA_NAME_TO);
            this.ChangeNizumiGenbaCdTextBoxEnabled();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積現場FROM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_FROM_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!this.logic.isChanged(sender)) { return; }//変更なし、戻る
            this.logic.CheckNizumiGenbaCdFrom(this.NIZUMI_GENBA_CD_FROM, this.NIZUMI_GYOUSHA_CD_FROM, this.NIZUMI_GENBA_NAME_FROM);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積現場TO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_TO_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!this.logic.isChanged(sender)) { return; }//変更なし、戻る
            this.logic.CheckNizumiGenbaCdFrom(this.NIZUMI_GENBA_CD_TO, this.NIZUMI_GYOUSHA_CD_TO, this.NIZUMI_GENBA_NAME_TO);
            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 荷降業者FROM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_FROM_Enter(object sender, EventArgs e)
        {
            this.preNioroshiGyoushaFrCd = this.NIOROSHI_GYOUSHA_CD_FROM.Text;
        }

        /// <summary>
        /// 荷降業者TO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_TO_Enter(object sender, EventArgs e)
        {
            this.preNioroshiGyoushaToCd = this.NIOROSHI_GYOUSHA_CD_TO.Text;
        }

        /// <summary>
        /// 荷積業者FROM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_FROM_Enter(object sender, EventArgs e)
        {
            this.preNizumiGyoushaFrCd = this.NIZUMI_GYOUSHA_CD_FROM.Text;
        }

        /// <summary>
        /// 荷積業者TO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_TO_Enter(object sender, EventArgs e)
        {
            this.preNizumiGyoushaToCd = this.NIZUMI_GYOUSHA_CD_TO.Text;
        }

    }
}
