using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChouhyouPatternPopup;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using System.Data;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.SalesPayment.ShiharaiSuiiChouhyou
{
    /// <summary>
    /// 支払推移表画面クラス
    /// </summary>
    public partial class UIForm_ShiharaiSuiihyou : SuperForm
    {
        /// <summary>
        /// 支払推移表ロジッククラス
        /// </summary>
        private ShiharaiSuiihyouLogicClass logic;

        /// <summary>
        /// 支払集計表DTOを取得・設定します
        /// </summary>
        internal ShiharaiSuiihyouDtoClass FormDataDto { get; set; }

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
        public UIForm_ShiharaiSuiihyou(WINDOW_ID windowId)
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();

            this.WindowId = windowId;

            this.logic = new ShiharaiSuiihyouLogicClass(this);

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

            this.PATTERN_LIST_BOX.SetWindowId(this.WindowId);
            // 集計チェックボックス非表示
            this.PATTERN_LIST_BOX.ChangeVisibleShuukeiFlg(false);
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
            this.DATE_SHURUI.Text = "1";
            //this.DATE_FROM.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //this.DATE_TO.Text = DateTime.Now.ToString("yyyy/MM/dd");
            // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
            this.DENPYOU_SHURUI.Text = "5";
            // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End
            this.TORIHIKI_KBN.Text = "3";
            this.KAKUTEI_KBN.Text = "3";
            this.SHIME_KBN.Text = "3";
            this.KYOTEN_CD.Text = "99";
            this.KYOTEN_NAME.Text = "全社";
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
                if (this.PATTERN_LIST_BOX.GetSelectedPatternDto() == null)
                {
                    this.PATTERN_LIST_BOX.ClearKoumoku();
                    this.PATTERN_LIST_BOX.Refresh();
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F5]表示ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.PATTERN_LIST_BOX.GetSelectedPatternDto() == null)
            {
                MessageBox.Show("パターンを選択してください。", "アラート",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.PATTERN_LIST_BOX.Focus();
                this.PATTERN_LIST_BOX.PATTERN_LIST_BOX.BackColor = Constans.ERROR_COLOR;
                this.RecoveryFocusOutCheckMethod(); //PhuocLoc 2020/12/07 #136227
                return;
            }

            if (this.RegistErrorFlag == false)
            {
                // 日付チェック
                string strSelectDate = string.Empty;
                ArrayList arrayPivot = new ArrayList();
                long monthDiff = 0;
                if (!this.logic.CheckDate(out monthDiff, out strSelectDate, out arrayPivot))
                {
                    return;
                }

                if (!this.SetDtoData())
                {
                    return;
                }
                this.FormDataDto.SelectDate = strSelectDate;
                this.FormDataDto.Pivot = arrayPivot;
                this.FormDataDto.MonthCount = (int)monthDiff + 1;

                this.logic.printFlg = false;

                this.logic.Search();
            }
            else
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
                this.RecoveryFocusOutCheckMethod(); //PhuocLoc 2020/12/07 #136227
                return;
            }

            if (this.RegistErrorFlag == false)
            {
                // 日付チェック
                string strSelectDate = string.Empty;
                ArrayList arrayPivot = new ArrayList();
                long monthDiff = 0;
                if (!this.logic.CheckDate(out monthDiff, out strSelectDate, out arrayPivot))
                {
                    return;
                }

                if (!this.SetDtoData())
                {
                    return;
                }
                this.FormDataDto.SelectDate = strSelectDate;
                this.FormDataDto.Pivot = arrayPivot;
                this.FormDataDto.MonthCount = (int)monthDiff + 1;

                this.logic.printFlg = true;

                this.logic.Search();
            }
            else
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

                this.FormDataDto = new ShiharaiSuiihyouDtoClass();

                // パターン
                this.FormDataDto.Pattern = this.PATTERN_LIST_BOX.GetSelectedPatternDto();

                // 日付種類
                this.FormDataDto.DateShuruiCd = Int32.Parse(this.DATE_SHURUI.Text);
                // 日付範囲
                this.FormDataDto.DateFrom = Convert.ToDateTime(this.DATE_FROM.Value).ToString("yyyy/MM/dd");
                this.FormDataDto.DateTo = Convert.ToDateTime(this.DATE_TO.Value).ToString("yyyy/MM/dd");
                // 伝票種類
                this.FormDataDto.DenpyouShuruiCd = Int32.Parse(this.DENPYOU_SHURUI.Text);
                // 取引区分
                this.FormDataDto.TorihikiKbnCd = Int32.Parse(this.TORIHIKI_KBN.Text);
                // 確定区分
                this.FormDataDto.KakuteiKbnCd = Int32.Parse(this.KAKUTEI_KBN.Text);
                // 締処理状況
                this.FormDataDto.ShimeJoukyouCd = Int32.Parse(this.SHIME_KBN.Text);
                // 拠点
                this.FormDataDto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
                this.FormDataDto.KyotenName = this.KYOTEN_NAME.Text;
                // 入力担当CD
                this.FormDataDto.NyuuryokuTantoushaCdFrom = this.NYUURYOKU_TANTOUSHA_CD_FROM.Text;
                this.FormDataDto.NyuuryokuTantoushaFrom = this.NYUURYOKU_TANTOUSHA_NAME_FROM.Text;
                this.FormDataDto.NyuuryokuTantoushaCdTo = this.NYUURYOKU_TANTOUSHA_CD_TO.Text;
                this.FormDataDto.NyuuryokuTantoushaTo = this.NYUURYOKU_TANTOUSHA_NAME_TO.Text;
                // 取引先CD
                this.FormDataDto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                this.FormDataDto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
                this.FormDataDto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                this.FormDataDto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
                // 業者CD
                this.FormDataDto.GyoushaCdFrom = this.GYOUSHA_CD_FROM.Text;
                this.FormDataDto.GyoushaFrom = this.GYOUSHA_NAME_FROM.Text;
                this.FormDataDto.GyoushaCdTo = this.GYOUSHA_CD_TO.Text;
                this.FormDataDto.GyoushaTo = this.GYOUSHA_NAME_TO.Text;
                // 現場CD
                this.FormDataDto.GenbaCdFrom = this.GENBA_CD_FROM.Text;
                this.FormDataDto.GenbaFrom = this.GENBA_NAME_FROM.Text;
                this.FormDataDto.GenbaCdTo = this.GENBA_CD_TO.Text;
                this.FormDataDto.GenbaTo = this.GENBA_NAME_TO.Text;
                // 品名
                this.FormDataDto.HinmeiCdFrom = this.HINMEI_CD_FROM.Text;
                this.FormDataDto.HinmeiFrom = this.HINMEI_NAME_FROM.Text;
                this.FormDataDto.HinmeiCdTo = this.HINMEI_CD_TO.Text;
                this.FormDataDto.HinmeiTo = this.HINMEI_NAME_TO.Text;
                // 種類
                this.FormDataDto.ShuruiCdFrom = this.SHURUI_CD_FROM.Text;
                this.FormDataDto.ShuruiFrom = this.SHURUI_NAME_FROM.Text;
                this.FormDataDto.ShuruiCdTo = this.SHURUI_CD_TO.Text;
                this.FormDataDto.ShuruiTo = this.SHURUI_NAME_TO.Text;
                // 分類
                this.FormDataDto.BunruiCdFrom = this.BUNRUI_CD_FROM.Text;
                this.FormDataDto.BunruiFrom = this.BUNRUI_NAME_FROM.Text;
                this.FormDataDto.BunruiCdTo = this.BUNRUI_CD_TO.Text;
                this.FormDataDto.BunruiTo = this.BUNRUI_NAME_TO.Text;
                // 荷降業者CD
                this.FormDataDto.NioroshiGyoushaCdFrom = this.NIOROSHI_GYOUSHA_CD_FROM.Text;
                this.FormDataDto.NioroshiGyoushaFrom = this.NIOROSHI_GYOUSHA_NAME_FROM.Text;
                this.FormDataDto.NioroshiGyoushaCdTo = this.NIOROSHI_GYOUSHA_CD_TO.Text;
                this.FormDataDto.NioroshiGyoushaTo = this.NIOROSHI_GYOUSHA_NAME_TO.Text;
                // 荷降現場CD
                this.FormDataDto.NioroshiGenbaCdFrom = this.NIOROSHI_GENBA_CD_FROM.Text;
                this.FormDataDto.NioroshiGenbaFrom = this.NIOROSHI_GENBA_NAME_FROM.Text;
                this.FormDataDto.NioroshiGenbaCdTo = this.NIOROSHI_GENBA_CD_TO.Text;
                this.FormDataDto.NioroshiGenbaTo = this.NIOROSHI_GENBA_NAME_TO.Text;
                // 荷積業者CD
                this.FormDataDto.NizumiGyoushaCdFrom = this.NIZUMI_GYOUSHA_CD_FROM.Text;
                this.FormDataDto.NizumiGyoushaFrom = this.NIZUMI_GYOUSHA_NAME_FROM.Text;
                this.FormDataDto.NizumiGyoushaCdTo = this.NIZUMI_GYOUSHA_CD_TO.Text;
                this.FormDataDto.NizumiGyoushaTo = this.NIZUMI_GYOUSHA_NAME_TO.Text;
                // 荷積現場CD
                this.FormDataDto.NizumiGenbaCdFrom = this.NIZUMI_GENBA_CD_FROM.Text;
                this.FormDataDto.NizumiGenbaFrom = this.NIZUMI_GENBA_NAME_FROM.Text;
                this.FormDataDto.NizumiGenbaCdTo = this.NIZUMI_GENBA_CD_TO.Text;
                this.FormDataDto.NizumiGenbaTo = this.NIZUMI_GENBA_NAME_TO.Text;
                // 営業担当CD
                this.FormDataDto.EigyouTantoushaCdForm = this.EIGYOU_TANTOUSHA_CD_FROM.Text;
                this.FormDataDto.EigyouTantoushaForm = this.EIGYOU_TANTOUSHA_NAME_FROM.Text;
                this.FormDataDto.EigyouTantoushaCdTo = this.EIGYOU_TANTOUSHA_CD_TO.Text;
                this.FormDataDto.EigyouTantoushaTo = this.EIGYOU_TANTOUSHA_NAME_TO.Text;
                // 車輛
                this.FormDataDto.SharyouCdFrom = this.SHARYOU_CD_FROM.Text;
                this.FormDataDto.SharyouFrom = this.SHARYOU_NAME_FROM.Text;
                this.FormDataDto.SharyouCdTo = this.SHARYOU_CD_TO.Text;
                this.FormDataDto.SharyouTo = this.SHARYOU_NAME_TO.Text;
                // 車種
                this.FormDataDto.ShashuCdFrom = this.SHASHU_CD_FROM.Text;
                this.FormDataDto.ShashuFrom = this.SHASHU_NAME_FROM.Text;
                this.FormDataDto.ShashuCdTo = this.SHASHU_CD_TO.Text;
                this.FormDataDto.ShashuTo = this.SHASHU_NAME_TO.Text;
                // 運搬業者CD
                this.FormDataDto.UnpanGyoushaCdFrom = this.UNPAN_GYOUSHA_CD_FROM.Text;
                this.FormDataDto.UnpanGyoushaFrom = this.UNPAN_GYOUSHA_NAME_FROM.Text;
                this.FormDataDto.UnpanGyoushaCdTo = this.UNPAN_GYOUSHA_CD_TO.Text;
                this.FormDataDto.UnpanGyoushaTo = this.UNPAN_GYOUSHA_NAME_TO.Text;
                // 形態区分
                this.FormDataDto.KeitaiKbnCdFrom = this.KEITAI_KBN_CD_FROM.Text;
                this.FormDataDto.KeitaiKbnFrom = this.KEITAI_KBN_NAME_FROM.Text;
                this.FormDataDto.KeitaiKbnCdTo = this.KEITAI_KBN_CD_TO.Text;
                this.FormDataDto.KeitaiKbnTo = this.KEITAI_KBN_NAME_TO.Text;
                // 台貫
                this.FormDataDto.DaikanCdFrom = this.DAIKAN_CD_FROM.Text;
                this.FormDataDto.DaikanFrom = this.DAIKAN_NAME_FROM.Text;
                this.FormDataDto.DaikanCdTo = this.DAIKAN_CD_TO.Text;
                this.FormDataDto.DaikanTo = this.DAIKAN_NAME_TO.Text;

                //PhuocLoc 2020/12/07 #136227 -Start
                // 集計項目
                this.FormDataDto.ShuukeiKoumokuCdFrom = this.SHUUKEI_KOUMOKU_CD_FROM.Text;
                this.FormDataDto.ShuukeiKoumokuFrom = this.SHUUKEI_KOUMOKU_NAME_FROM.Text;
                this.FormDataDto.ShuukeiKoumokuCdTo = this.SHUUKEI_KOUMOKU_CD_TO.Text;
                this.FormDataDto.ShuukeiKoumokuTo = this.SHUUKEI_KOUMOKU_NAME_TO.Text;
                //PhuocLoc 2020/12/07 #136227 -End

                // SELECT句の項目名
                List<S_LIST_COLUMN_SELECT_DETAIL> list
                    = this.PATTERN_LIST_BOX.GetSelectedPatternDto().ColumnSelectDetailList;
                foreach (S_LIST_COLUMN_SELECT_DETAIL detail in list)
                {
                    this.FormDataDto.Select.Add(detail.BUTSURI_NAME);
                }

                // 条件1
                this.FormDataDto.Jyouken1 = CreateConditionString(this.FormDataDto, 1);
                // 条件2
                this.FormDataDto.Jyouken2 = CreateConditionString(this.FormDataDto, 2);
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
        private void ShowTourokuPopup(WINDOW_TYPE windowType, PatternDto dto)
        {
            LogUtility.DebugMethodStart(windowType, dto);

            var popup = new ChouhyouPatternTourokuPopupForm(windowType, dto);
            // 集計チェックボックスを非表示
            popup.ChangeVisibleShuukeiFlg(false);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HINMEI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 種類CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHURUI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 分類CD Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BUNRUI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
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
                var sharyouList = this.logic.GetSharyou(this.SHARYOU_CD_FROM.Text, out catchErr);
                if (!catchErr)
                {
                    e.Cancel = true;
                    return;
                }
                if (sharyouList.Count() == 0)
                {
                    //new MessageBoxShowLogic().MessageBoxShow("E020", "車輌");
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
                bool catcheErr = true;
                this.SHARYOU_NAME_TO.Text = string.Empty;
                var sharyouList = this.logic.GetSharyou(this.SHARYOU_CD_TO.Text, out catcheErr);
                if (!catcheErr)
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
        /// 荷降/荷積降現場のチェックとテキストボックスへの略称セットを行います
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
        private void SetDaikanName(CustomNumericTextBox2 cdTextBox, CustomTextBox nameTextBox)
        {
            LogUtility.DebugMethodStart(cdTextBox, nameTextBox);

            var cd = cdTextBox.Text;
            var name = String.Empty;

            if (cd == "1")
            {
                name = "自社";
            }
            else if (cd == "2")
            {
                name = "他社";
            }

            nameTextBox.Text = name;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録時のチェックでFromToのチェックが入力エラーになると、FocusOutCheckMethodが動作しなくなる対策
        /// （ValidatorでCausesValidationをfalseにしたままになる不具合）
        /// </summary>
        private void RecoveryFocusOutCheckMethod()
        {
            LogUtility.DebugMethodStart();

            this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
            this.TORIHIKISAKI_CD_TO.CausesValidation = true;
            this.GYOUSHA_CD_FROM.CausesValidation = true;
            this.GYOUSHA_CD_TO.CausesValidation = true;
            this.GENBA_CD_FROM.CausesValidation = true;
            this.GENBA_CD_TO.CausesValidation = true;
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
            //PhuocLoc 2020/12/07 #136227 -Start
            this.SHUUKEI_KOUMOKU_CD_FROM.CausesValidation = true;
            this.SHUUKEI_KOUMOKU_CD_TO.CausesValidation = true;
            //PhuocLoc 2020/12/07 #136227 -End

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
        private string CreateConditionString(ShiharaiSuiihyouDtoClass dto, int pattern)
        {
            StringBuilder sb = new StringBuilder();

            if (pattern == 1)
            {
                // 条件1
                sb.AppendLine("[抽出条件]");
                // 日付
                string dateCategory = string.Empty;
                if (dto.DateShuruiCd == 1)
                {
                    dateCategory = "伝票日付";
                }
                else if (dto.DateShuruiCd == 2)
                {
                    dateCategory = "支払日付";
                }
                else
                {
                    dateCategory = "入力日付";
                }
                //sb.AppendFormat("  [日付] {0}", dateCategory);
                //sb.AppendLine(string.Empty);
                // 日付範囲
                sb.AppendFormat("  [" + dateCategory + "] {0} ～ {1}", dto.DateFrom, dto.DateTo);
                sb.AppendLine(string.Empty);
                // 伝票種類
                string denpyouCategory = string.Empty;
                if (dto.DenpyouShuruiCd == 1)
                {
                    denpyouCategory = "受入";
                }
                else if (dto.DenpyouShuruiCd == 2)
                {
                    denpyouCategory = "出荷";
                }
                else if (dto.DenpyouShuruiCd == 3)
                {
                    denpyouCategory = "売上／支払";
                }
                // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
                else if (dto.DenpyouShuruiCd == 4)
                {
                    denpyouCategory = "代納";
                }
                // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End
                else
                {
                    denpyouCategory = "全て";
                }
                sb.AppendFormat("  [伝票種類] {0}", dto.DenpyouShuruiCd + " " + denpyouCategory);
                sb.AppendLine(string.Empty);
                // 取引区分
                string torihikiKbnName = string.Empty;
                if (dto.TorihikiKbnCd == 1)
                {
                    torihikiKbnName = "現金";
                }
                else if (dto.TorihikiKbnCd == 2)
                {
                    torihikiKbnName = "掛け";
                }
                else
                {
                    torihikiKbnName = "全て";
                }
                sb.AppendFormat("  [取引区分] {0}", dto.TorihikiKbnCd + " " + torihikiKbnName);
                sb.AppendLine(string.Empty);
                // 確定区分
                string kakuteiKbnName = string.Empty;
                if (dto.KakuteiKbnCd == 1)
                {
                    kakuteiKbnName = "確定";
                }
                else if (dto.KakuteiKbnCd == 2)
                {
                    kakuteiKbnName = "未確定";
                }
                else
                {
                    kakuteiKbnName = "全て";
                }
                sb.AppendFormat("  [確定区分] {0}", dto.KakuteiKbnCd + " " + kakuteiKbnName);
                sb.AppendLine(string.Empty);
                // 締処理状況
                string shimeshoriName = string.Empty;
                if (dto.ShimeJoukyouCd == 1)
                {
                    shimeshoriName = "済";
                }
                else if (dto.ShimeJoukyouCd == 2)
                {
                    shimeshoriName = "未締";
                }
                else
                {
                    shimeshoriName = "全て";
                }
                sb.AppendFormat("  [締処理状況] {0}", dto.ShimeJoukyouCd + " " + shimeshoriName);
                sb.AppendLine(string.Empty);
                // 拠点
                sb.AppendFormat("  [拠点] {0}", dto.KyotenCd + " " + dto.KyotenName);
                sb.AppendLine(string.Empty);
                if (!string.IsNullOrEmpty(dto.NyuuryokuTantoushaCdFrom) || !string.IsNullOrEmpty(dto.NyuuryokuTantoushaCdTo))
                {
                    sb.AppendFormat("  [入力担当者]  {0} ～ {1}", dto.NyuuryokuTantoushaCdFrom + " " + dto.NyuuryokuTantoushaFrom, dto.NyuuryokuTantoushaCdTo + " " + dto.NyuuryokuTantoushaTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.TorihikisakiCdFrom) || !string.IsNullOrEmpty(dto.TorihikisakiCdTo))
                {
                    sb.AppendFormat("  [取引先]  {0} ～ {1}", dto.TorihikisakiCdFrom + " " + dto.TorihikisakiFrom, dto.TorihikisakiCdTo + " " + dto.TorihikisakiTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.GyoushaCdFrom) || !string.IsNullOrEmpty(dto.GyoushaCdTo))
                {
                    sb.AppendFormat("  [業者]  {0} ～ {1}", dto.GyoushaCdFrom + " " + dto.GyoushaFrom, dto.GyoushaCdTo + " " + dto.GyoushaTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.GenbaCdFrom) || !string.IsNullOrEmpty(dto.GenbaCdTo))
                {
                    sb.AppendFormat("  [現場]  {0} ～ {1}", dto.GenbaCdFrom + " " + dto.GenbaFrom, dto.GenbaCdTo + " " + dto.GenbaTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.HinmeiCdFrom) || !string.IsNullOrEmpty(dto.HinmeiCdTo))
                {
                    sb.AppendFormat("  [品名]  {0} ～ {1}", dto.HinmeiCdFrom + " " + dto.HinmeiFrom, dto.HinmeiCdTo + " " + dto.HinmeiTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.ShuruiCdFrom) || !string.IsNullOrEmpty(dto.ShuruiCdTo))
                {
                    sb.AppendFormat("  [種類]  {0} ～ {1}", dto.ShuruiCdFrom + " " + dto.ShuruiFrom, dto.ShuruiCdTo + " " + dto.ShuruiTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.BunruiCdFrom) || !string.IsNullOrEmpty(dto.BunruiCdTo))
                {
                    sb.AppendFormat("  [分類]  {0} ～ {1}", dto.BunruiCdFrom + " " + dto.BunruiFrom, dto.BunruiCdTo + " " + dto.BunruiTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.NioroshiGyoushaCdFrom) || !string.IsNullOrEmpty(dto.NioroshiGyoushaCdTo))
                {
                    sb.AppendFormat("  [荷降業者]  {0} ～ {1}", dto.NioroshiGyoushaCdFrom + " " + dto.NioroshiGyoushaFrom, dto.NioroshiGyoushaCdTo + " " + dto.NioroshiGyoushaTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.NioroshiGenbaCdFrom) || !string.IsNullOrEmpty(dto.NioroshiGenbaCdTo))
                {
                    sb.AppendFormat("  [荷降現場]  {0} ～ {1}", dto.NioroshiGenbaCdFrom + " " + dto.NioroshiGenbaFrom, dto.NioroshiGenbaCdTo + " " + dto.NioroshiGenbaTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.NizumiGyoushaCdFrom) || !string.IsNullOrEmpty(dto.NizumiGyoushaCdTo))
                {
                    sb.AppendFormat("  [荷積業者]  {0} ～ {1}", dto.NizumiGyoushaCdFrom + " " + dto.NizumiGyoushaFrom, dto.NizumiGyoushaCdTo + " " + dto.NizumiGyoushaTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.NizumiGenbaCdFrom) || !string.IsNullOrEmpty(dto.NizumiGenbaCdTo))
                {
                    sb.AppendFormat("  [荷積現場]  {0} ～ {1}", dto.NizumiGenbaCdFrom + " " + dto.NizumiGenbaFrom, dto.NizumiGenbaCdTo + " " + dto.NizumiGenbaTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.EigyouTantoushaCdForm) || !string.IsNullOrEmpty(dto.EigyouTantoushaCdTo))
                {
                    sb.AppendFormat("  [営業担当者]  {0} ～ {1}", dto.EigyouTantoushaCdForm + " " + dto.EigyouTantoushaForm, dto.EigyouTantoushaCdTo + " " + dto.EigyouTantoushaTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.SharyouCdFrom) || !string.IsNullOrEmpty(dto.SharyouCdTo))
                {
                    sb.AppendFormat("  [車輛]  {0} ～ {1}", dto.SharyouCdFrom + " " + dto.SharyouFrom, dto.SharyouCdTo + " " + dto.SharyouTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.ShashuCdFrom) || !string.IsNullOrEmpty(dto.ShashuCdTo))
                {
                    sb.AppendFormat("  [車種]  {0} ～ {1}", dto.ShashuCdFrom + " " + dto.ShashuFrom, dto.ShashuCdTo + " " + dto.ShashuTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.UnpanGyoushaCdFrom) || !string.IsNullOrEmpty(dto.UnpanGyoushaCdTo))
                {
                    sb.AppendFormat("  [運搬業者]  {0} ～ {1}", dto.UnpanGyoushaCdFrom + " " + dto.UnpanGyoushaFrom, dto.UnpanGyoushaCdTo + " " + dto.UnpanGyoushaTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.KeitaiKbnCdFrom) || !string.IsNullOrEmpty(dto.KeitaiKbnCdTo))
                {
                    sb.AppendFormat("  [形態区分]  {0} ～ {1}", dto.KeitaiKbnCdFrom + " " + dto.KeitaiKbnFrom, dto.KeitaiKbnCdTo + " " + dto.KeitaiKbnTo);
                    sb.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(dto.DaikanCdFrom) || !string.IsNullOrEmpty(dto.DaikanCdTo))
                {
                    sb.AppendFormat("  [台貫]  {0} ～ {1}", dto.DaikanCdFrom + " " + dto.DaikanFrom, dto.DaikanCdTo + " " + dto.DaikanTo);
                    sb.AppendLine(string.Empty);
                }
                //PhuocLoc 2020/12/07 #136227 -Start
                if (!string.IsNullOrEmpty(dto.ShuukeiKoumokuCdFrom) || !string.IsNullOrEmpty(dto.ShuukeiKoumokuCdTo))
                {
                    sb.AppendFormat("  [伝票集計項目]  {0} ～ {1}", dto.ShuukeiKoumokuCdFrom + " " + dto.ShuukeiKoumokuFrom, dto.ShuukeiKoumokuCdTo + " " + dto.ShuukeiKoumokuTo);
                    sb.AppendLine(string.Empty);
                }
                //PhuocLoc 2020/12/07 #136227 -End
            }
            else
            {
                // 条件2
                List<S_LIST_COLUMN_SELECT> denpyoList = this.PATTERN_LIST_BOX.GetSelectedPatternDto().ColumnSelectList;
                if (denpyoList.Count > 0)
                {
                    sb.AppendLine("[集計項目]");
                    sb.AppendFormat("  [1] {0}", denpyoList[0].KOUMOKU_RONRI_NAME);
                    for (int i = 1; i < denpyoList.Count; i++)
                    {
                        sb.AppendLine(string.Empty);
                        sb.AppendFormat("  [" + (i + 1) + "] {0}", denpyoList[i].KOUMOKU_RONRI_NAME);
                    }
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
                csvExport.ConvertDataTableToCsv(dt, true, true, "支払推移表", this);
            }
        }

        //PhuocLoc 2020/12/07 #136227 -Start
        private void SHUUKEI_KOUMOKU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }
        //PhuocLoc 2020/12/07 #136227 -End
    }
}