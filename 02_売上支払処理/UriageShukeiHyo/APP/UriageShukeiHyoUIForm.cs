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
using Seasar.Framework.Exceptions;

namespace Shougun.Core.SalesPayment.UriageShukeiHyo
{
    /// <summary>
    /// 売上集計表画面クラス
    /// </summary>
    public partial class UriageShukeiHyoUIForm : SuperForm
    {
        /// <summary>
        /// 売上集計表ロジッククラス
        /// </summary>
        private UriageShukeiHyoLogic logic;

        /// <summary>
        /// 売上集計表DTOを取得・設定します
        /// </summary>
        internal UriageShukeiHyoDto FormDataDto { get; set; }

        /// <summary>
        /// 車輌CD Fromを入力する前の値
        /// </summary>
        private String beforeSharyouCdFrom;

        /// <summary>
        /// 車輌CD Toを入力する前の値
        /// </summary>
        private String beforeSharyouCdTo;

        /// <summary>
        ///  車輌CDのフォーカスアウトによるポップアップかのフラグ
        /// </summary>
        private bool isCallSharyouValidatingPopup = false;

        /// <summary>
        /// 車輌でエラーが起きているかのフラグ
        /// </summary>
        private bool isSharyouError = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UriageShukeiHyoUIForm()
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();

            this.WindowId = WINDOW_ID.T_URIAGE_SHUUKEIHYOU;

            this.logic = new UriageShukeiHyoLogic(this);

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
        private bool SetDefault()
        {
            try
            {
                this.DATE_SHURUI.Text = UriageShukeiHyoConst.DATE_SHURUI_CD_DENPYOU;
                this.DATE_RANGE.Text = UriageShukeiHyoConst.DATE_RANGE_CD_TOUJITSU;
                this.DENPYOU_SHURUI.Text = UriageShukeiHyoConst.DENPYOU_SHURUI_CD_SUBETE;
                this.TORIHIKI_KBN.Text = UriageShukeiHyoConst.TORIHIKI_KBN_CD_SUBETE;
                this.KAKUTEI_KBN.Text = UriageShukeiHyoConst.KAKUTEI_KBN_CD_SUBETE;
                this.SHIME_KBN.Text = UriageShukeiHyoConst.SHIME_KBN_CD_SUBETE;
                this.KYOTEN_CD.Text = UriageShukeiHyoConst.KYOTEN_CD_ZENSHA;
                this.KYOTEN_NAME.Text = UriageShukeiHyoConst.KYOTEN_NAME_99;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDefault", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
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
                this.RecoveryFocusOutCheckMethod(); //PhuocLoc 2020/12/08 #136223
                return;
            }

            if (this.RegistErrorFlag == false)
            {
                // 日付の必須チェックとFromToチェックを両方セットすると正常にエラーチェックされないので
                // FromToチェックは画面独自で実装
                bool catchErr = true;
                bool isErr = this.IsErrorDateFromTo(this.DATE_FROM, this.DATE_TO, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (isErr)
                {
                    new MessageBoxShowLogic().MessageBoxShow("E030", this.DATE_FROM.DisplayItemName, this.DATE_TO.DisplayItemName);
                    this.RegistErrorFlag = true;
                }
                else if (this.PATTERN_LIST_BOX.GetSelectedPatternDto() != null)
                {
                    if (!this.SetDtoData())
                    {
                        return;
                    }

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
                this.RecoveryFocusOutCheckMethod(); //PhuocLoc 2020/12/08 #136223
                return;
            }

            if (this.RegistErrorFlag == false)
            {
                // 日付の必須チェックとFromToチェックを両方セットすると正常にエラーチェックされないので
                // FromToチェックは画面独自で実装
                bool catchErr = true;
                bool isErr = this.IsErrorDateFromTo(this.DATE_FROM, this.DATE_TO, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (isErr)
                {
                    new MessageBoxShowLogic().MessageBoxShow("E030", this.DATE_FROM.DisplayItemName, this.DATE_TO.DisplayItemName);
                    this.RegistErrorFlag = true;
                }
                else if (this.PATTERN_LIST_BOX.GetSelectedPatternDto() != null)
                {
                    if (!this.SetDtoData())
                    {
                        return;
                    }

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
        private bool SetDtoData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.FormDataDto = new UriageShukeiHyoDto();

                this.FormDataDto.Pattern = this.PATTERN_LIST_BOX.GetSelectedPatternDto();

                this.FormDataDto.DateShurui = Int32.Parse(this.DATE_SHURUI.Text);
                if (this.DATE_SHURUI_1.Checked)
                {
                    this.FormDataDto.DateShuruiName = UriageShukeiHyoConst.DATE_SHURUI_1;
                }
                else if (this.DATE_SHURUI_2.Checked)
                {
                    this.FormDataDto.DateShuruiName = UriageShukeiHyoConst.DATE_SHURUI_2;
                }
                else
                {
                    this.FormDataDto.DateShuruiName = UriageShukeiHyoConst.DATE_SHURUI_3;
                }
                if (this.DATE_RANGE.Text == UriageShukeiHyoConst.DATE_RANGE_CD_TOUJITSU)
                {
                    this.FormDataDto.DateFrom = this.logic.parentForm.sysDate.Date;
                    this.FormDataDto.DateTo = this.logic.parentForm.sysDate.Date;
                }
                else if (this.DATE_RANGE.Text == UriageShukeiHyoConst.DATE_RANGE_CD_TOUGETSU)
                {
                    var today = this.logic.parentForm.sysDate.Date;
                    this.FormDataDto.DateFrom = today.AddDays(-today.Day + 1);
                    this.FormDataDto.DateTo = this.FormDataDto.DateFrom.AddMonths(1).AddDays(-1);
                }
                else
                {
                    this.FormDataDto.DateFrom = DateTime.Parse(this.DATE_FROM.Value.ToString());
                    this.FormDataDto.DateTo = DateTime.Parse(this.DATE_TO.Value.ToString());
                }
                this.FormDataDto.DenpyouShurui = Int32.Parse(this.DENPYOU_SHURUI.Text);
                if (this.DENPYOU_SHURUI_1.Checked)
                {
                    this.FormDataDto.DenpyouShuruiName = UriageShukeiHyoConst.DENPYOU_SHURUI_1;
                }
                else if (this.DENPYOU_SHURUI_2.Checked)
                {
                    this.FormDataDto.DenpyouShuruiName = UriageShukeiHyoConst.DENPYOU_SHURUI_2;
                }
                else if (this.DENPYOU_SHURUI_3.Checked)
                {
                    this.FormDataDto.DenpyouShuruiName = UriageShukeiHyoConst.DENPYOU_SHURUI_3;
                }
                // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) Start
                else if (this.DENPYOU_SHURUI_4.Checked)
                {
                    this.FormDataDto.DenpyouShuruiName = UriageShukeiHyoConst.DENPYOU_SHURUI_4;
                }
                else
                {
                    this.FormDataDto.DenpyouShuruiName = UriageShukeiHyoConst.DENPYOU_SHURUI_5;
                }
                // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) End
                this.FormDataDto.TorihikiKbn = Int32.Parse(this.TORIHIKI_KBN.Text);
                if (this.TORIHIKI_KBN_1.Checked)
                {
                    this.FormDataDto.TorihikiKbnName = UriageShukeiHyoConst.TORIHIKI_KBN_1;
                }
                else if (this.TORIHIKI_KBN_2.Checked)
                {
                    this.FormDataDto.TorihikiKbnName = UriageShukeiHyoConst.TORIHIKI_KBN_2;
                }
                else
                {
                    this.FormDataDto.TorihikiKbnName = UriageShukeiHyoConst.TORIHIKI_KBN_3;
                }
                this.FormDataDto.KakuteiKbn = Int32.Parse(this.KAKUTEI_KBN.Text);
                if (this.KAKUTEI_KBN_1.Checked)
                {
                    this.FormDataDto.KakuteiKbnName = UriageShukeiHyoConst.KAKUTEI_KBN_1;
                }
                else if (this.KAKUTEI_KBN_2.Checked)
                {
                    this.FormDataDto.KakuteiKbnName = UriageShukeiHyoConst.KAKUTEI_KBN_2;
                }
                else
                {
                    this.FormDataDto.KakuteiKbnName = UriageShukeiHyoConst.KAKUTEI_KBN_3;
                }
                this.FormDataDto.ShimeKbn = Int32.Parse(this.SHIME_KBN.Text);
                if (this.SHIME_KBN_1.Checked)
                {
                    this.FormDataDto.ShimeKbnName = UriageShukeiHyoConst.SHHIME_KBN_1;
                }
                else if (this.SHIME_KBN_2.Checked)
                {
                    this.FormDataDto.ShimeKbnName = UriageShukeiHyoConst.SHHIME_KBN_2;
                }
                else
                {
                    this.FormDataDto.ShimeKbnName = UriageShukeiHyoConst.SHHIME_KBN_3;
                }
                if (String.IsNullOrEmpty(this.KYOTEN_CD.Text) == false)
                {
                    this.FormDataDto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
                }
                this.FormDataDto.KyotenName = this.KYOTEN_NAME.Text;
                this.FormDataDto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                this.FormDataDto.TorihikisakiNameFrom = this.TORIHIKISAKI_NAME_FROM.Text;
                this.FormDataDto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                this.FormDataDto.TorihikisakiNameTo = this.TORIHIKISAKI_NAME_TO.Text;
                this.FormDataDto.GyoushaCdFrom = this.GYOUSHA_CD_FROM.Text;
                this.FormDataDto.GyoushaNameFrom = this.GYOUSHA_NAME_FROM.Text;
                this.FormDataDto.GyoushaCdTo = this.GYOUSHA_CD_TO.Text;
                this.FormDataDto.GyoushaNameTo = this.GYOUSHA_NAME_TO.Text;
                this.FormDataDto.GenbaCdFrom = this.GENBA_CD_FROM.Text;
                this.FormDataDto.GenbaNameFrom = this.GENBA_NAME_FROM.Text;
                this.FormDataDto.GenbaCdTo = this.GENBA_CD_TO.Text;
                this.FormDataDto.GenbaNameTo = this.GENBA_NAME_TO.Text;
                this.FormDataDto.HinmeiCdFrom = this.HINMEI_CD_FROM.Text;
                this.FormDataDto.HinmeiNameFrom = this.HINMEI_NAME_FROM.Text;
                this.FormDataDto.HinmeiCdTo = this.HINMEI_CD_TO.Text;
                this.FormDataDto.HinmeiNameTo = this.HINMEI_NAME_TO.Text;
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
                this.FormDataDto.EigyouTantoushaCdFrom = this.EIGYOU_TANTOUSHA_CD_FROM.Text;
                this.FormDataDto.EigyouTantoushaNameFrom = this.EIGYOU_TANTOUSHA_NAME_FROM.Text;
                this.FormDataDto.EigyouTantoushaCdTo = this.EIGYOU_TANTOUSHA_CD_TO.Text;
                this.FormDataDto.EigyouTantoushaNameTo = this.EIGYOU_TANTOUSHA_NAME_TO.Text;
                this.FormDataDto.NyuuryokuTantoushaCdFrom = this.NYUURYOKU_TANTOUSHA_CD_FROM.Text;
                this.FormDataDto.NyuuryokuTantoushaNameFrom = this.NYUURYOKU_TANTOUSHA_NAME_FROM.Text;
                this.FormDataDto.NyuuryokuTantoushaCdTo = this.NYUURYOKU_TANTOUSHA_CD_TO.Text;
                this.FormDataDto.NyuuryokuTantoushaNameTo = this.NYUURYOKU_TANTOUSHA_NAME_TO.Text;
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
                this.FormDataDto.KeitaiKbnCdTo = this.KEITAI_KBN_CD_TO.Text;
                this.FormDataDto.KeitaiKbnNameTo = this.KEITAI_KBN_NAME_TO.Text;
                this.FormDataDto.DaikanKbnCdFrom = this.DAIKAN_CD_FROM.Text;
                this.FormDataDto.DaikanKbnNameFrom = this.DAIKAN_NAME_FROM.Text;
                this.FormDataDto.DaikanKbnCdTo = this.DAIKAN_CD_TO.Text;
                this.FormDataDto.DaikanKbnNameTo = this.DAIKAN_NAME_TO.Text;

                this.FormDataDto.ShuruiCdFrom = this.SHURUI_CD_FROM.Text;
                this.FormDataDto.ShuruiNameFrom = this.SHURUI_NAME_FROM.Text;
                this.FormDataDto.ShuruiCdTo = this.SHURUI_CD_TO.Text;
                this.FormDataDto.ShuruiNameTo = this.SHURUI_NAME_TO.Text;
                this.FormDataDto.BunruiCdFrom = this.BUNRUI_CD_FROM.Text;
                this.FormDataDto.BunruiNameFrom = this.BUNRUI_NAME_FROM.Text;
                this.FormDataDto.BunruiCdTo = this.BUNRUI_CD_TO.Text;
                this.FormDataDto.BunruiNameTo = this.BUNRUI_NAME_TO.Text;

                //PhuocLoc 2020/12/08 #136223 -Start
                this.FormDataDto.ShuukeiKoumokuCdFrom = this.SHUUKEI_KOUMOKU_CD_FROM.Text;
                this.FormDataDto.ShuukeiKoumokuNameFrom = this.SHUUKEI_KOUMOKU_NAME_FROM.Text;
                this.FormDataDto.ShuukeiKoumokuCdTo = this.SHUUKEI_KOUMOKU_CD_TO.Text;
                this.FormDataDto.ShuukeiKoumokuNameTo = this.SHUUKEI_KOUMOKU_NAME_TO.Text;
                //PhuocLoc 2020/12/08 #136223 -End

                //VAN 20200330 #134973 S
                if (this.FormDataDto.Pattern.ColumnSelectDetailList.Where(s => s.BUTSURI_NAME == "HINMEI_CD").FirstOrDefault() != null ||
                    this.FormDataDto.Pattern.Pattern.HINMEI_DISP_KBN.IsTrue)
                {
                    this.FormDataDto.SearchHinmeiFlg = true;
                }
                if (this.FormDataDto.Pattern.Pattern.NET_JYUURYOU_DISP_KBN.IsTrue)
                {
                    this.FormDataDto.SearchNetJuuryouFlg = true;
                }
                if (this.FormDataDto.Pattern.Pattern.SUURYOU_UNIT_DISP_KBN.IsTrue)
                {
                    this.FormDataDto.SearchSuuryouTaniFlg = true;
                }
                //VAN 20200330 #134973 E
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDtoData", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// パターン登録ポップアップを表示します
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <param name="dto">パターンDTOクラス</param>
        private bool ShowTourokuPopup(WINDOW_TYPE windowType, PatternDto dto)
        {
            try
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowTourokuPopup", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
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

            this.DATE_FROM.Value = null;
            this.DATE_TO.Value = null;

            if (this.DATE_RANGE.Text == UriageShukeiHyoConst.DATE_RANGE_CD_SHITEI)
            {
                this.DATE_FROM.Enabled = true;
                this.DATE_TO.Enabled = true;
            }
            else
            {
                this.DATE_FROM.Enabled = false;
                this.DATE_TO.Enabled = false;
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

            if (!String.IsNullOrEmpty(this.DATE_FROM.Text))
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
        /// 業者CD Fromテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_FROM_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGenbaCdTextBoxEnabled();

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
        /// 業者CD Toテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_TO_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CD Toテキストボックスのバリデートが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.GenbaCheckAndSetting(this.GYOUSHA_CD_FROM, this.GENBA_CD_FROM, this.GENBA_NAME_FROM) == false)
            {
                e.Cancel = true;
                this.GENBA_CD_FROM.Focus();
            }

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
        /// 現場CD Fromテキストボックスのバリデートが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.GenbaCheckAndSetting(this.GYOUSHA_CD_TO, this.GENBA_CD_TO, this.GENBA_NAME_TO) == false)
            {
                e.Cancel = true;
                this.GENBA_CD_TO.Focus();
            }

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
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeNioroshiGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
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
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeNioroshiGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降業者CD_FROM 更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.NizumioroshiGyoushaCheckAndSetting(this.NIOROSHI_GYOUSHA_CD_FROM, this.NIOROSHI_GYOUSHA_NAME_FROM, 1))
            {
                e.Cancel = true;
                this.NIOROSHI_GYOUSHA_CD_FROM.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降業者CD_TO 更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.NizumioroshiGyoushaCheckAndSetting(this.NIOROSHI_GYOUSHA_CD_TO, this.NIOROSHI_GYOUSHA_NAME_TO, 1))
            {
                e.Cancel = true;
                this.NIOROSHI_GYOUSHA_CD_TO.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降現場CD Fromテキストボックスのバリデートが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIOROSHI_GENBA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.NizumioroshiGenbaCheckAndSetting(this.NIOROSHI_GYOUSHA_CD_FROM, this.NIOROSHI_GENBA_CD_FROM, this.NIOROSHI_GENBA_NAME_FROM, 1) == false)
            {
                e.Cancel = true;
                this.NIOROSHI_GENBA_CD_FROM.Focus();
            }

            LogUtility.DebugMethodEnd();
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
        /// 荷降現場CD Toテキストボックスのバリデートが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIOROSHI_GENBA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.NizumioroshiGenbaCheckAndSetting(this.NIOROSHI_GYOUSHA_CD_TO, this.NIOROSHI_GENBA_CD_TO, this.NIOROSHI_GENBA_NAME_TO, 1) == false)
            {
                e.Cancel = true;
                this.NIOROSHI_GENBA_CD_TO.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積業者CD Fromテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIZUMI_GYOUSHA_CD_FROM_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeNizumiGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
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
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeNizumiGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積業者CD_FROM 更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.NizumioroshiGyoushaCheckAndSetting(this.NIZUMI_GYOUSHA_CD_FROM, this.NIZUMI_GYOUSHA_NAME_FROM, 2))
            {
                e.Cancel = true;
                this.NIZUMI_GYOUSHA_CD_FROM.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積業者CD_TO 更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.NizumioroshiGyoushaCheckAndSetting(this.NIZUMI_GYOUSHA_CD_TO, this.NIZUMI_GYOUSHA_NAME_TO, 2))
            {
                e.Cancel = true;
                this.NIZUMI_GYOUSHA_CD_TO.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積現場CD Fromテキストボックスのバリデートが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIZUMI_GENBA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.NizumioroshiGenbaCheckAndSetting(this.NIZUMI_GYOUSHA_CD_FROM, this.NIZUMI_GENBA_CD_FROM, this.NIZUMI_GENBA_NAME_FROM, 2) == false)
            {
                e.Cancel = true;
                this.NIZUMI_GENBA_CD_FROM.Focus();
            }

            LogUtility.DebugMethodEnd();
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
        /// 荷積現場CD Toテキストボックスのバリデートが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NIZUMI_GENBA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.NizumioroshiGenbaCheckAndSetting(this.NIZUMI_GYOUSHA_CD_TO, this.NIZUMI_GENBA_CD_TO, this.NIZUMI_GENBA_NAME_TO, 2) == false)
            {
                e.Cancel = true;
                this.NIZUMI_GENBA_CD_TO.Focus();
            }

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
        /// 入力担当者CD Fromのバリデートが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUURYOKU_TANTOUSHA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.NyuurokuTantoushaCheckAndSetting(this.NYUURYOKU_TANTOUSHA_CD_FROM, this.NYUURYOKU_TANTOUSHA_NAME_FROM) == false)
            {
                e.Cancel = true;
                this.NYUURYOKU_TANTOUSHA_CD_FROM.Focus();
            }

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
        /// 入力担当者CD Toのバリデートが開始したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void NYUURYOKU_TANTOUSHA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.NyuurokuTantoushaCheckAndSetting(this.NYUURYOKU_TANTOUSHA_CD_TO, this.NYUURYOKU_TANTOUSHA_NAME_TO) == false)
            {
                e.Cancel = true;
                this.NYUURYOKU_TANTOUSHA_CD_TO.Focus();
            }

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
        /// 運搬業者_FROM 更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.UnpanGyoushaCheckAndSetting(this.UNPAN_GYOUSHA_CD_FROM, this.UNPAN_GYOUSHA_NAME_FROM))
            {
                e.Cancel = true;
                this.UNPAN_GYOUSHA_CD_FROM.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬業者_TO 更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.UnpanGyoushaCheckAndSetting(this.UNPAN_GYOUSHA_CD_TO, this.UNPAN_GYOUSHA_NAME_TO))
            {
                e.Cancel = true;
                this.UNPAN_GYOUSHA_CD_TO.Focus();
            }

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

        /// <summary>
        /// 車輌CD Fromテキストボックスに入力があったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_FROM_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.isSharyouError)
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

            var sharyouCd = this.SHARYOU_CD_FROM.Text;
            this.isSharyouError = false;
            if ((!string.IsNullOrEmpty(sharyouCd) && !sharyouCd.Equals(this.beforeSharyouCdFrom)) ||
                (!string.IsNullOrEmpty(sharyouCd) && string.IsNullOrEmpty(this.SHARYOU_NAME_FROM.Text) && sharyouCd.Equals(this.beforeSharyouCdFrom)))
            {
                bool catchErr = true;
                this.SHARYOU_NAME_FROM.Text = string.Empty;
                var sharyouList = this.logic.GetSharyou(this.SHARYOU_CD_FROM.Text, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (sharyouList.Count() == 0)
                {
                    new MessageBoxShowLogic().MessageBoxShow("E020", "車輌");
                    e.Cancel = true;
                }
                else if (sharyouList.Count() > 1)
                {
                    this.isCallSharyouValidatingPopup = true;
                    CustomControlExtLogic.PopUp(this.SHARYOU_CD_FROM);
                    this.isCallSharyouValidatingPopup = false;
                    if (string.IsNullOrEmpty(this.SHARYOU_NAME_FROM.Text))
                    {
                        // キャンセル時
                        this.isSharyouError = true;
                    }
                    e.Cancel = true;
                }
                else
                {
                    var sharyou = sharyouList.FirstOrDefault();
                    this.SHARYOU_NAME_FROM.Text = sharyou.SHARYOU_NAME_RYAKU;
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

            if (String.IsNullOrEmpty(this.SHARYOU_CD_FROM.Text))
            {
                this.SHARYOU_NAME_FROM.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌選択ポップアップ Fromが起動する前に処理します
        /// </summary>
        public void SHARYOU_FROM_POPUP_Before()
        {
            LogUtility.DebugMethodStart();
            this.SHARYOU_NAME_FROM.Text = string.Empty;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌選択ポップアップ Fromが閉じた後に処理します
        /// </summary>
        public void SHARYOU_FROM_POPUP_After()
        {
            LogUtility.DebugMethodStart();

            if (!this.isCallSharyouValidatingPopup && !string.IsNullOrEmpty(this.SHARYOU_NAME_FROM.Text))
            {
                this.beforeSharyouCdFrom = this.SHARYOU_CD_FROM.Text;
            }

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

            if (!this.isSharyouError)
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

            var sharyouCd = this.SHARYOU_CD_TO.Text;
            this.isSharyouError = false;
            if ((!string.IsNullOrEmpty(sharyouCd) && !sharyouCd.Equals(this.beforeSharyouCdTo)) ||
                (!string.IsNullOrEmpty(sharyouCd) && string.IsNullOrEmpty(this.SHARYOU_NAME_TO.Text) && sharyouCd.Equals(this.beforeSharyouCdTo)))
            {
                bool catchErr = true;
                this.SHARYOU_NAME_TO.Text = string.Empty;
                var sharyouList = this.logic.GetSharyou(this.SHARYOU_CD_TO.Text, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (sharyouList.Count() == 0)
                {
                    new MessageBoxShowLogic().MessageBoxShow("E020", "車輌");
                    e.Cancel = true;
                }
                else if (sharyouList.Count() > 1)
                {
                    this.isCallSharyouValidatingPopup = true;
                    CustomControlExtLogic.PopUp(this.SHARYOU_CD_TO);
                    this.isCallSharyouValidatingPopup = false;
                    if (string.IsNullOrEmpty(this.SHARYOU_NAME_TO.Text))
                    {
                        // キャンセル時
                        this.isSharyouError = true;
                    }
                    e.Cancel = true;
                }
                else
                {
                    var sharyou = sharyouList.FirstOrDefault();
                    this.SHARYOU_NAME_TO.Text = sharyou.SHARYOU_NAME_RYAKU;
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

            if (String.IsNullOrEmpty(this.SHARYOU_CD_TO.Text))
            {
                this.SHARYOU_NAME_TO.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌選択ポップアップ Toが起動する前に処理します
        /// </summary>
        public void SHARYOU_TO_POPUP_Before()
        {
            LogUtility.DebugMethodStart();
            this.SHARYOU_NAME_TO.Text = string.Empty;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌選択ポップアップ Toが閉じた後に処理します
        /// </summary>
        public void SHARYOU_TO_POPUP_After()
        {
            LogUtility.DebugMethodStart();

            if (!this.isCallSharyouValidatingPopup && !string.IsNullOrEmpty(this.SHARYOU_NAME_TO.Text))
            {
                this.beforeSharyouCdTo = this.SHARYOU_CD_TO.Text;
            }

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

        //PhuocLoc 2020/12/08 #136223 -Start
        /// <summary>
        /// 集計項目CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }
        //PhuocLoc 2020/12/08 #136223 -End

        /// <summary>
        /// 台貫CD Fromのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DAIKAN_CD_FROM_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetDaikanName(this.DAIKAN_CD_FROM, this.DAIKAN_NAME_FROM);

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
        /// 台貫CD Toのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DAIKAN_CD_TO_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetDaikanName(this.DAIKAN_CD_TO, this.DAIKAN_NAME_TO);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// FromテキストボックスからToテキストボックスにCDと名称をコピーします
        /// </summary>
        /// <param name="sender">CdFromテキストボックス</param>
        private bool SetToCdAndName(object sender)
        {
            try
            {
                LogUtility.DebugMethodStart(sender);

                var cdToTextBox = (TextBox)sender;
                var cdFromTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_TO", "_FROM")).FirstOrDefault();
                if (!String.IsNullOrEmpty(cdFromTextBox.Text))
                {
                    cdToTextBox.Text = cdFromTextBox.Text;

                    var nameFromTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_CD_TO", "_NAME_FROM")).FirstOrDefault();
                    var nameToTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_CD_TO", "_NAME_TO")).FirstOrDefault();
                    nameToTextBox.Text = nameFromTextBox.Text;

                    // 車輌CDの場合は前回値保存
                    if (cdToTextBox.Name.Equals(this.SHARYOU_CD_TO.Name))
                    {
                        this.beforeSharyouCdTo = cdToTextBox.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetToCdAndName", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 現場CDテキストボックスの使用可否を切り替えます
        /// </summary>
        private bool ChangeGenbaCdTextBoxEnabled()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var fromCd = this.GYOUSHA_CD_FROM.Text;
                var toCd = this.GYOUSHA_CD_TO.Text;
                if (String.IsNullOrEmpty(fromCd) == false && String.IsNullOrEmpty(toCd) == false && this.ZeroSuppressGenbaCd(fromCd) == this.ZeroSuppressGenbaCd(toCd))
                {
                    this.GENBA_CD_FROM.Enabled = true;
                    this.GENBA_CD_TO.Enabled = true;
                    this.GENBA_POPUP_FROM.Enabled = true;
                    this.GENBA_POPUP_TO.Enabled = true;
                }
                else
                {
                    this.GENBA_CD_FROM.Text = String.Empty;
                    this.GENBA_CD_TO.Text = String.Empty;
                    this.GENBA_NAME_FROM.Text = String.Empty;
                    this.GENBA_NAME_TO.Text = String.Empty;
                    this.GENBA_CD_FROM.Enabled = false;
                    this.GENBA_CD_TO.Enabled = false;
                    this.GENBA_POPUP_FROM.Enabled = false;
                    this.GENBA_POPUP_TO.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeGenbaCdTextBoxEnabled", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 荷降現場CDテキストボックスの使用可否を切り替えます
        /// </summary>
        private bool ChangeNioroshiGenbaCdTextBoxEnabled()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var fromCd = this.NIOROSHI_GYOUSHA_CD_FROM.Text;
                var toCd = this.NIOROSHI_GYOUSHA_CD_TO.Text;
                if (String.IsNullOrEmpty(fromCd) == false && String.IsNullOrEmpty(toCd) == false && this.ZeroSuppressGenbaCd(fromCd) == this.ZeroSuppressGenbaCd(toCd))
                {
                    this.NIOROSHI_GENBA_CD_FROM.Enabled = true;
                    this.NIOROSHI_GENBA_CD_TO.Enabled = true;
                    this.NIOROSHI_GENBA_POPUP_FROM.Enabled = true;
                    this.NIOROSHI_GENBA_POPUP_TO.Enabled = true;
                }
                else
                {
                    this.NIOROSHI_GENBA_CD_FROM.Text = String.Empty;
                    this.NIOROSHI_GENBA_CD_TO.Text = String.Empty;
                    this.NIOROSHI_GENBA_NAME_FROM.Text = String.Empty;
                    this.NIOROSHI_GENBA_NAME_TO.Text = String.Empty;
                    this.NIOROSHI_GENBA_CD_FROM.Enabled = false;
                    this.NIOROSHI_GENBA_CD_TO.Enabled = false;
                    this.NIOROSHI_GENBA_POPUP_FROM.Enabled = false;
                    this.NIOROSHI_GENBA_POPUP_TO.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeNioroshiGenbaCdTextBoxEnabled", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 荷積現場CDテキストボックスの使用可否を切り替えます
        /// </summary>
        private bool ChangeNizumiGenbaCdTextBoxEnabled()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var fromCd = this.NIZUMI_GYOUSHA_CD_FROM.Text;
                var toCd = this.NIZUMI_GYOUSHA_CD_TO.Text;
                if (String.IsNullOrEmpty(fromCd) == false && String.IsNullOrEmpty(toCd) == false && this.ZeroSuppressGenbaCd(fromCd) == this.ZeroSuppressGenbaCd(toCd))
                {
                    this.NIZUMI_GENBA_CD_FROM.Enabled = true;
                    this.NIZUMI_GENBA_CD_TO.Enabled = true;
                    this.NIZUMI_GENBA_POPUP_FROM.Enabled = true;
                    this.NIZUMI_GENBA_POPUP_TO.Enabled = true;
                }
                else
                {
                    this.NIZUMI_GENBA_CD_FROM.Text = String.Empty;
                    this.NIZUMI_GENBA_CD_TO.Text = String.Empty;
                    this.NIZUMI_GENBA_NAME_FROM.Text = String.Empty;
                    this.NIZUMI_GENBA_NAME_TO.Text = String.Empty;
                    this.NIZUMI_GENBA_CD_FROM.Enabled = false;
                    this.NIZUMI_GENBA_CD_TO.Enabled = false;
                    this.NIZUMI_GENBA_POPUP_FROM.Enabled = false;
                    this.NIZUMI_GENBA_POPUP_TO.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeNizumiGenbaCdTextBoxEnabled", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 現場CDのゼロ埋め処理を行います
        /// </summary>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>ゼロ埋めした現場CD</returns>
        private String ZeroSuppressGenbaCd(String genbaCd)
        {
            LogUtility.DebugMethodStart(genbaCd);

            var ret = String.Empty;
            if (String.IsNullOrEmpty(genbaCd) == false)
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

            try
            {
                if (String.IsNullOrEmpty(genbaCdTextBox.Text))
                {
                    genbaNameTextBox.Text = String.Empty;
                    ret = true;
                }
                else
                {
                    var mGenba = this.logic.GetGenba(gyoushaCdTextBox.Text, genbaCdTextBox.Text);
                    if (mGenba == null)
                    {
                        new MessageBoxShowLogic().MessageBoxShow("E020", "現場");
                        genbaNameTextBox.Text = String.Empty;
                    }
                    else
                    {
                        genbaNameTextBox.Text = mGenba.GENBA_NAME_RYAKU;
                        ret = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GenbaCheckAndSetting", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaCheckAndSetting", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 荷降/荷積業者のチェックとテキストボックスへの略称セットを行います
        /// </summary>
        /// <param name="gyoushaCdTextBox"></param>
        /// <param name="gyoushaNameTextBox"></param>
        /// <param name="nizumioroshiFlg">1:荷降;2:荷積</param>
        /// <returns></returns>
        private bool NizumioroshiGyoushaCheckAndSetting(TextBox gyoushaCdTextBox, TextBox gyoushaNameTextBox, int nizumioroshiFlg)
        {
            LogUtility.DebugMethodStart(gyoushaCdTextBox, gyoushaNameTextBox, nizumioroshiFlg);

            var ret = false;
            try
            {
                if (String.IsNullOrEmpty(gyoushaCdTextBox.Text))
                {
                    gyoushaNameTextBox.Text = String.Empty;
                    ret = true;
                }
                else
                {
                    var mGyousha = this.logic.GetNizumioroshiGyousha(gyoushaCdTextBox.Text);
                    if (mGyousha == null
                        || (nizumioroshiFlg == 1 && !mGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue && !mGyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                        || (nizumioroshiFlg == 2 && !mGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue && !mGyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue))
                    {
                        new MessageBoxShowLogic().MessageBoxShow("E020", "業者");
                        gyoushaNameTextBox.Text = String.Empty;
                    }
                    else
                    {
                        gyoushaNameTextBox.Text = mGyousha.GYOUSHA_NAME_RYAKU;
                        ret = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("NizumioroshiGyoushaCheckAndSetting", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("NizumioroshiGyoushaCheckAndSetting", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 荷積降現場のチェックとテキストボックスへの略称セットを行います
        /// </summary>
        /// <param name="gyoushaCdTextBox">業者CDテキストボックス</param>
        /// <param name="genbaCdTextBox">現場CDテキストボックス</param>
        /// <param name="genbaNameTextBox">現場名テキストボックス</param>
        /// <param name="nizumioroshiFlg">1:荷降;2:荷積</param>
        /// <returns>エラーがなければ、True</returns>
        private bool NizumioroshiGenbaCheckAndSetting(TextBox gyoushaCdTextBox, TextBox genbaCdTextBox, TextBox genbaNameTextBox, int nizumioroshiFlg)
        {
            LogUtility.DebugMethodStart(gyoushaCdTextBox, genbaCdTextBox, genbaNameTextBox, nizumioroshiFlg);

            var ret = false;

            try
            {
                if (String.IsNullOrEmpty(genbaCdTextBox.Text))
                {
                    genbaNameTextBox.Text = String.Empty;
                    ret = true;
                }
                else
                {
                    var mGenba = this.logic.GetNizumioroshiGenba(gyoushaCdTextBox.Text, genbaCdTextBox.Text);
                    if (mGenba == null
                        || (nizumioroshiFlg == 1
                            && !mGenba.TSUMIKAEHOKAN_KBN.IsTrue
                            && !mGenba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue
                            && !mGenba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                        || (nizumioroshiFlg == 2
                            && !mGenba.TSUMIKAEHOKAN_KBN.IsTrue
                            && !mGenba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue))
                    {
                        new MessageBoxShowLogic().MessageBoxShow("E020", "現場");
                        genbaNameTextBox.Text = String.Empty;
                    }
                    else
                    {
                        genbaNameTextBox.Text = mGenba.GENBA_NAME_RYAKU;
                        ret = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("NizumioroshiGenbaCheckAndSetting", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("NizumioroshiGenbaCheckAndSetting", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 運搬業者のチェックとテキストボックスへの略称セットを行います
        /// </summary>
        /// <param name="unpanGyoushaCdTextBox"></param>
        /// <param name="unpangyoushaNameTextBox"></param>
        /// <returns></returns>
        private bool UnpanGyoushaCheckAndSetting(TextBox unpanGyoushaCdTextBox, TextBox unpangyoushaNameTextBox)
        {
            LogUtility.DebugMethodStart(unpanGyoushaCdTextBox, unpangyoushaNameTextBox);

            var ret = false;
            try
            {
                if (String.IsNullOrEmpty(unpanGyoushaCdTextBox.Text))
                {
                    unpangyoushaNameTextBox.Text = String.Empty;
                    ret = true;
                }
                else
                {
                    var mGyousha = this.logic.GetNizumioroshiGyousha(unpanGyoushaCdTextBox.Text);
                    if (mGyousha == null || !mGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    {
                        new MessageBoxShowLogic().MessageBoxShow("E020", "業者");
                        unpangyoushaNameTextBox.Text = String.Empty;
                    }
                    else
                    {
                        unpangyoushaNameTextBox.Text = mGyousha.GYOUSHA_NAME_RYAKU;
                        ret = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UnpanGyoushaCheckAndSetting", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("UnpanGyoushaCheckAndSetting", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
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

            try
            {
                if (String.IsNullOrEmpty(cdTextBox.Text))
                {
                    nameTextBox.Text = String.Empty;
                    ret = true;
                }
                else
                {
                    var mShain = this.logic.GetNyuuryokuTantousha(cdTextBox.Text);
                    if (mShain == null)
                    {
                        new MessageBoxShowLogic().MessageBoxShow("E020", "入力担当者");
                        nameTextBox.Text = String.Empty;
                    }
                    else
                    {
                        nameTextBox.Text = mShain.SHAIN_NAME_RYAKU;
                        ret = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("NyuurokuTantoushaCheckAndSetting", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("NyuurokuTantoushaCheckAndSetting", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// テキストボックスに台貫名称をセットします
        /// </summary>
        /// <param name="cdTextBox">台貫CDテキストボックス</param>
        /// <param name="nameTextBox">台貫テキストボックス</param>
        private bool SetDaikanName(CustomNumericTextBox2 cdTextBox, CustomTextBox nameTextBox)
        {
            try
            {
                LogUtility.DebugMethodStart(cdTextBox, nameTextBox);

                var cd = cdTextBox.Text;
                var name = String.Empty;
                if (cd == UriageShukeiHyoConst.DAIKAN_CD_JISHA)
                {
                    name = UriageShukeiHyoConst.DAIKAN_NAME_1;
                }
                else if (cd == UriageShukeiHyoConst.DAIKAN_CD_TASHA)
                {
                    name = UriageShukeiHyoConst.DAIKAN_NAME_2;
                }

                nameTextBox.Text = name;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDaikanName", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 日付範囲のFromToチェックを行います
        /// </summary>
        /// <param name="fromTextBox">日付Fromテキストボックス</param>
        /// <param name="toTextBox">日付Toテキストボックス</param>
        /// <returns>エラーがある場合は、True</returns>
        private bool IsErrorDateFromTo(CustomDateTimePicker fromTextBox, CustomDateTimePicker toTextBox, out bool catchErr)
        {
            LogUtility.DebugMethodStart(fromTextBox, toTextBox);

            var ret = false;
            catchErr = true;
            try
            {
                var fromDate = fromTextBox.Text;
                var toDate = toTextBox.Text;
                if (String.IsNullOrEmpty(fromDate) == false && String.IsNullOrEmpty(toDate) == false)
                {
                    if (fromDate.CompareTo(toDate) > 0)
                    {
                        fromTextBox.IsInputErrorOccured = true;
                        toTextBox.IsInputErrorOccured = true;
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsErrorDateFromTo", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// 登録時のチェックでFromToのチェックが入力エラーになると、FocusOutCheckMethodが動作しなくなる対策
        /// （ValidatorでCausesValidationをfalseにしたままになる不具合）
        /// </summary>
        private bool RecoveryFocusOutCheckMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;
                this.GYOUSHA_CD_FROM.CausesValidation = true;
                this.GYOUSHA_CD_TO.CausesValidation = true;
                this.GENBA_CD_FROM.CausesValidation = true;
                this.GENBA_CD_TO.CausesValidation = true;
                this.HINMEI_CD_FROM.CausesValidation = true;
                this.HINMEI_CD_TO.CausesValidation = true;
                this.NIOROSHI_GYOUSHA_CD_FROM.CausesValidation = true;
                this.NIOROSHI_GYOUSHA_CD_TO.CausesValidation = true;
                this.NIOROSHI_GENBA_CD_FROM.CausesValidation = true;
                this.NIOROSHI_GENBA_CD_TO.CausesValidation = true;
                this.NIZUMI_GYOUSHA_CD_FROM.CausesValidation = true;
                this.NIZUMI_GYOUSHA_CD_TO.CausesValidation = true;
                this.NIZUMI_GENBA_CD_FROM.CausesValidation = true;
                this.NIZUMI_GENBA_CD_TO.CausesValidation = true;
                this.EIGYOU_TANTOUSHA_CD_FROM.CausesValidation = true;
                this.EIGYOU_TANTOUSHA_CD_TO.CausesValidation = true;
                this.NYUURYOKU_TANTOUSHA_CD_FROM.CausesValidation = true;
                this.NYUURYOKU_TANTOUSHA_CD_TO.CausesValidation = true;
                this.UNPAN_GYOUSHA_CD_FROM.CausesValidation = true;
                this.UNPAN_GYOUSHA_CD_TO.CausesValidation = true;
                this.SHASHU_CD_FROM.CausesValidation = true;
                this.SHASHU_CD_TO.CausesValidation = true;
                this.SHARYOU_CD_FROM.CausesValidation = true;
                this.SHARYOU_CD_TO.CausesValidation = true;
                this.KEITAI_KBN_CD_FROM.CausesValidation = true;
                this.KEITAI_KBN_CD_TO.CausesValidation = true;
                this.DAIKAN_CD_FROM.CausesValidation = true;
                this.DAIKAN_CD_TO.CausesValidation = true;

                this.SHURUI_CD_FROM.CausesValidation = true;
                this.SHURUI_CD_TO.CausesValidation = true;
                this.BUNRUI_CD_FROM.CausesValidation = true;
                this.BUNRUI_CD_TO.CausesValidation = true;

                //PhuocLoc 2020/12/08 #136223 -Start
                this.SHUUKEI_KOUMOKU_CD_FROM.CausesValidation = true;
                this.SHUUKEI_KOUMOKU_CD_TO.CausesValidation = true;
                //PhuocLoc 2020/12/08 #136223 -End
            }
            catch (Exception ex)
            {
                LogUtility.Error("RecoveryFocusOutCheckMethod", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
    }
}
