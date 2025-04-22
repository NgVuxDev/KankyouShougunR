// $Id:
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
using Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou.DAO;
using System.Data;
using Shougun.Core.Common.BusinessCommon.Utility;


namespace Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou
{
    #region - Class -

    /// <summary>受入/出荷明細表出力指定画面を表すクラス・コントロール</summary>
    public partial class UIForm_UkeireShukkaMeisaihyou : SuperForm
    {
        #region - Fields -

        /// <summary>画面ロジック</summary>
        private UkeireShukkaMeisaihyouLogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="UIForm_UkeireShukkaMeisaihyou" /> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public UIForm_UkeireShukkaMeisaihyou(WINDOW_ID windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new UkeireShukkaMeisaihyouLogicClass(this);

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool Initialize()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 拠点CD
                this.CustomNumericTextBox2KyotenShiteiCD.Text = "99";
                // 拠点
                this.customTextBoxKyotenShiteiMei.Text = "全社";
                // 入力担当者CD
                this.NYUURYOKU_TANTOUSHA_CD.Text = String.Empty;
                // 入力担当者
                this.NYUURYOKU_TANTOUSHA_NAME.Text = String.Empty;
                // 日付CD
                this.CustomNumericTextBox2Hiduke.Text = "1";
                // 日付範囲
                this.CustomNumericTextBox2HidukeHaniShiteiHoho.Text = "1";
                // 日付From
                this.customDateTimePickerHidukeHaniShiteiStart.Text = String.Empty;
                // 日付To
                this.customDateTimePickerHidukeHaniShiteiEnd.Text = String.Empty;
                // 入出荷物区分
                this.CustomNumericTextBox2NyuuShukkaKbn.Text = "1";
                // 並び順
                this.CustomNumericTextBox2Sort.Text = "1";

                // 伝票種類
                if (WINDOW_ID.T_UKEIRE_MEISAIHYOU_KOTEI == this.WindowId)
                {
                    this.CustomNumericTextBox2DenpyoSyuruiShitei.Text = "1";

                    // 検収注意文言非表示
                    this.CustomNumericTextBox2Hiduke.Tag = "【1、2】のいずれかで入力してください。";
                }
                else
                {
                    this.CustomNumericTextBox2DenpyoSyuruiShitei.Text = "2";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Initialize", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #region - キー処理 -
        /// <summary>
        /// 「F5 CSV出力ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.logic.mSysInfo = mSysInfoDao.GetAllData().FirstOrDefault();

                // 必須入力エラーチェック
                if (this.RegistErrorFlag)
                {
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                if (this.customRadioButtonHidukeHaniShitei3.Checked)
                {
                    if (this.logic.CheckDate())
                    {
                        return;
                    }
                }

                if (!this.SetGyoushaCdFromTo())
                {
                    return;
                }

                if (!this.ChangeGenbaCdTextBoxEnabled())
                {
                    return;
                }

                // FromToのチェックがうまくいかないので自前でチェックする
                var errMsg = String.Empty;
                bool catchErr = true;
                errMsg = CheckErr(out catchErr);
                if (!catchErr)
                {
                    return;
                }

                if (!String.IsNullOrEmpty(errMsg))
                {
                    this.logic.errmessage.MessageBoxShow("E032", errMsg + "From", errMsg + "To");
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                // CSV出力へ
                var dto = CreateUkeireShukkaMeisaihyouDto();

                var dao = DaoInitUtility.GetComponent<IUkeireShukkaMeisaihyouDao>();
                var SearchDetailResult = dao.GetUkeireShukkaMeisaiData(dto);

                this.logic.CSVPrint(SearchDetailResult, dto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func5_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>Ｆ７キー（表示）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // 必須入力エラーチェック
            if (this.RegistErrorFlag)
            {
                Cursor.Current = Cursors.Arrow;
                return;
            }

            // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
            if (this.customRadioButtonHidukeHaniShitei3.Checked)
            {
                if (this.logic.CheckDate())
                {
                    return;
                }
            }
            // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

            if (!this.SetGyoushaCdFromTo())
            {
                return;
            }

            if (!this.ChangeGenbaCdTextBoxEnabled())
            {
                return;
            }

            // FromToのチェックがうまくいかないので自前でチェックする
            var errMsg = String.Empty;
            bool catchErr = true;
            errMsg = CheckErr(out catchErr);
            if (!catchErr)
            {
                return;
            }

            if (!String.IsNullOrEmpty(errMsg))
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E032", errMsg + "From", errMsg + "To");
                Cursor.Current = Cursors.Arrow;
                return;
            }

            // 帳票出力へ
            var dto = CreateUkeireShukkaMeisaihyouDto();

            this.logic.Search(dto);

            Cursor.Current = Cursors.Arrow;
        }

        /// <summary>DTO作成</summary>
        /// <param name=""></param>
        private UkeireShukkaMeisaihyouDtoClass CreateUkeireShukkaMeisaihyouDto()
        {
            // 帳票出力とCSVへ
            var dto = new UkeireShukkaMeisaihyouDtoClass();
            dto.KyotenCd = int.Parse(this.CustomNumericTextBox2KyotenShiteiCD.Text);
            dto.KyotenName = this.customTextBoxKyotenShiteiMei.Text;
            dto.NyuuryokuTantoushaCd = this.NYUURYOKU_TANTOUSHA_CD.Text;
            dto.NyuuryokuTantoushaName = this.NYUURYOKU_TANTOUSHA_NAME.Text;
            if (this.WindowId == WINDOW_ID.T_UKEIRE_MEISAIHYOU_KOTEI)
            {
                dto.DenpyouShuruiCd = 1;
            }
            else
            {
                dto.DenpyouShuruiCd = 2;
            }
            dto.DateShuruiCd = int.Parse(this.CustomNumericTextBox2Hiduke.Text);

            // 日付範囲の選択状態で日付条件を設定
            if (this.customRadioButtonHidukeHaniShitei1.Checked)
            {
                dto.DateFrom = this.logic.parentForm.sysDate.ToString("yyyy/MM/dd");
                dto.DateTo = this.logic.parentForm.sysDate.ToString("yyyy/MM/dd");
            }
            else if (this.customRadioButtonHidukeHaniShitei2.Checked)
            {
                dto.DateFrom = new DateTime(this.logic.parentForm.sysDate.Year, this.logic.parentForm.sysDate.Month, 1).ToString("yyyy/MM/dd");
                dto.DateTo = new DateTime(this.logic.parentForm.sysDate.Year, this.logic.parentForm.sysDate.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd"); ;
            }
            else if (this.customRadioButtonHidukeHaniShitei3.Checked)
            {
                dto.DateFrom = Convert.ToDateTime(this.customDateTimePickerHidukeHaniShiteiStart.Value).ToString("yyyy/MM/dd");
                dto.DateTo = Convert.ToDateTime(this.customDateTimePickerHidukeHaniShiteiEnd.Value).ToString("yyyy/MM/dd");
            }

            dto.NyuushukkaKbn = this.CustomNumericTextBox2NyuuShukkaKbn.Text;

            dto.GyoushaCdFrom = this.customAlphaNumTextBoxGyoushaStartCD.Text;
            dto.GyoushaCdTo = this.customAlphaNumTextBoxGyoushaEndCD.Text;
            dto.GyoushaFrom = this.customTextBoxGyoushaStartMeisho.Text;
            dto.GyoushaTo = this.customTextBoxGyoushaEndMeisho.Text;

            // 現場はテキストボックスが使用不可の場合、条件なしとする
            if (this.customAlphaNumTextBoxGenbaStartCD.Enabled)
            {
                dto.GenbaCdFrom = this.customAlphaNumTextBoxGenbaStartCD.Text;
                dto.GenbaCdTo = this.customAlphaNumTextBoxGenbaEndCD.Text;
                dto.GenbaFrom = this.customTextBoxGenbaStartMeisho.Text;
                dto.GenbaTo = this.customTextBoxGenbaEndMeisho.Text;
            }

            dto.UnpanGyoushaCdFrom = this.customAlphaNumTextBoxUnpanGyoushaStartCD.Text;
            dto.UnpanGyoushaFrom = this.customTextBoxUnpanGyoushaStartMeisho.Text;
            dto.UnpanGyoushaCdTo = this.customAlphaNumTextBoxUnpanGyoushaEndCD.Text;
            dto.UnpanGyoushaTo = this.customTextBoxUnpanGyoushaEndMeisho.Text;

            if (!String.IsNullOrEmpty(this.customAlphaNumTextBoxKeitaiKbnStartCD.Text))
            {
                dto.KeitaiKbnFrom = int.Parse(this.customAlphaNumTextBoxKeitaiKbnStartCD.Text).ToString();
            }
            dto.KeitaiFrom = this.customTextBoxKeitaiKbnStartMeisho.Text;
            if (!String.IsNullOrEmpty(this.customAlphaNumTextBoxKeitaiKbnEndCD.Text))
            {
                dto.KeitaiKbnTo = int.Parse(this.customAlphaNumTextBoxKeitaiKbnEndCD.Text).ToString();
            }
            dto.KeitaiTo = this.customTextBoxKeitaiKbnEndMeisho.Text;

            dto.HinmeiCdFrom = this.customAlphaNumTextBoxHinmeiStartCD.Text;
            dto.HinmeiFrom = this.customTextBoxHinmeiStartMeisho.Text;
            dto.HinmeiCdTo = this.customAlphaNumTextBoxHinmeiEndCD.Text;
            dto.HinmeiTo = this.customTextBoxHinmeiEndMeisho.Text;

            dto.ShuruiCdFrom = this.customAlphaNumTextBoxShuruiStartCD.Text;
            dto.ShuruiFrom = this.customTextBoxShuruiStartMeisho.Text;
            dto.ShuruiCdTo = this.customAlphaNumTextBoxShuruiEndCD.Text;
            dto.ShuruiTo = this.customTextBoxShuruiEndMeisho.Text;

            dto.BunruiCdFrom = this.customAlphaNumTextBoxBunruiStartCD.Text;
            dto.BunruiFrom = this.customTextBoxBunruiStartMeisho.Text;
            dto.BunruiCdTo = this.customAlphaNumTextBoxBunruiEndCD.Text;
            dto.BunruiTo = this.customTextBoxBunruiEndMeisho.Text;

            dto.Order = int.Parse(this.CustomNumericTextBox2Sort.Text);

            dto.IsGroupGyousha = this.customCheckBoxGyousha.Checked;
            dto.IsGroupGenba = this.customCheckBoxGenba.Checked;
            dto.IsGroupDenpyouNumber = this.customCheckBoxDenpyouNumber.Checked;

            return dto;
        }

        /// <summary>
        /// 抽出範囲項目の指定チェック
        /// </summary>
        /// <returns></returns>
        private string CheckErr(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            string errMsg = string.Empty;
            catchErr = true;
            try
            {
                if (!this.CheckCodeFromTo(this.customAlphaNumTextBoxGyoushaStartCD, this.customAlphaNumTextBoxGyoushaEndCD))
                {
                    errMsg = "業者";
                    return errMsg;
                }

                if (!this.CheckCodeFromTo(this.customAlphaNumTextBoxGenbaStartCD, this.customAlphaNumTextBoxGenbaEndCD))
                {
                    errMsg = "現場";
                    return errMsg;
                }

                if (!this.CheckCodeFromTo(this.customAlphaNumTextBoxUnpanGyoushaStartCD, this.customAlphaNumTextBoxUnpanGyoushaEndCD))
                {
                    errMsg = "運搬業者";
                    return errMsg;
                }

                if (!this.CheckCodeFromTo(this.customAlphaNumTextBoxKeitaiKbnStartCD, this.customAlphaNumTextBoxKeitaiKbnEndCD))
                {
                    errMsg = "形態区分";
                    return errMsg;
                }

                if (!this.CheckCodeFromTo(this.customAlphaNumTextBoxHinmeiStartCD, this.customAlphaNumTextBoxHinmeiEndCD))
                {
                    errMsg = "品名";
                    return errMsg;
                }

                if (!this.CheckCodeFromTo(this.customAlphaNumTextBoxShuruiStartCD, this.customAlphaNumTextBoxShuruiEndCD))
                {
                    errMsg = "種類";
                    return errMsg;
                }

                if (!this.CheckCodeFromTo(this.customAlphaNumTextBoxBunruiStartCD, this.customAlphaNumTextBoxBunruiEndCD))
                {
                    errMsg = "分類";
                    return errMsg;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckErr", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(errMsg, catchErr);
            }
            return errMsg;
        }

        /// <summary>
        /// 各CDのFromToの関係をチェック
        /// </summary>
        /// <param name="TextFrom"></param>
        /// <param name="TextTo"></param>
        /// <returns></returns>
        private bool CheckCodeFromTo(CustomAlphaNumTextBox TextFrom, CustomAlphaNumTextBox TextTo)
        {
            LogUtility.DebugMethodStart(TextFrom, TextTo);

            bool ret = true;
            var cdFrom = TextFrom.Text;
            var cdTo = TextTo.Text;
            TextFrom.IsInputErrorOccured = false;
            TextTo.IsInputErrorOccured = false;

            if (!String.IsNullOrEmpty(cdFrom) && !String.IsNullOrEmpty(cdTo))
            {
                if (cdFrom.CompareTo(cdTo) > 0)
                {
                    // Fromの方がToより大きい場合エラー
                    TextFrom.IsInputErrorOccured = true;
                    TextTo.IsInputErrorOccured = true;
                    TextFrom.Focus();
                    ret = false;
                }
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 各CDのFromToの関係をチェック
        /// </summary>
        /// <param name="TextFrom"></param>
        /// <param name="TextTo"></param>
        /// <returns></returns>
        private bool CheckCodeFromTo(CustomNumericTextBox2 TextFrom, CustomNumericTextBox2 TextTo)
        {
            LogUtility.DebugMethodStart(TextFrom, TextTo);

            bool ret = true;
            var cdFrom = TextFrom.Text;
            var cdTo = TextTo.Text;
            TextFrom.IsInputErrorOccured = false;
            TextTo.IsInputErrorOccured = false;

            if (!String.IsNullOrEmpty(cdFrom) && !String.IsNullOrEmpty(cdTo))
            {
                if (cdFrom.CompareTo(cdTo) > 0)
                {
                    // Fromの方がToより大きい場合エラー
                    TextFrom.IsInputErrorOccured = true;
                    TextTo.IsInputErrorOccured = true;
                    TextFrom.Focus();
                    ret = false;
                }
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>Ｆ１２キー（閉じる）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
        }

        #endregion - キー処理 -

        /// <summary>画面Load処理</summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            if (!this.logic.WindowInit())
            {
                return;
            }

            // 初期化処理
            this.Initialize();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>日付範囲指定区分が変更された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonHidukeHaniShitei_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CustomRadioButton customRadioButton = (CustomRadioButton)sender;
                this.customPanelHidukeHaniShitei.Enabled = int.Parse(customRadioButton.Value) == 3 ? true : false;

                if (this.customPanelHidukeHaniShitei.Enabled == false)
                {   // 無効
                    this.customDateTimePickerHidukeHaniShiteiStart.Value = null;
                    this.customDateTimePickerHidukeHaniShiteiEnd.Value = null;

                    // 日付範囲指定のテキストボックスに必須チェックをはずす
                    this.customDateTimePickerHidukeHaniShiteiStart.RegistCheckMethod = new Collection<SelectCheckDto>();
                    this.customDateTimePickerHidukeHaniShiteiEnd.RegistCheckMethod = new Collection<SelectCheckDto>();
                }
                else
                {
                    // 日付範囲指定のテキストボックスに（登録時）必須チェックを追加する
                    this.customDateTimePickerHidukeHaniShiteiStart.RegistCheckMethod = new Collection<SelectCheckDto>() { new SelectCheckDto("必須チェック") };
                    this.customDateTimePickerHidukeHaniShiteiEnd.RegistCheckMethod = new Collection<SelectCheckDto>() { new SelectCheckDto("必須チェック") };
                }
            }
            catch (Exception ee)
            {
                LogUtility.Error(ee.Message, ee);
            }
        }

        /// <summary>
        /// 業者CDFromのバリデート処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customAlphaNumTextBoxGyoushaStartCD_TextChanged(object sender, EventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);

            //this.ChangeGenbaCdTextBoxEnabled();

            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDToのバリデート処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customAlphaNumTextBoxGyoushaEndCD_TextChanged(object sender, EventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);

            //this.ChangeGenbaCdTextBoxEnabled();

            //LogUtility.DebugMethodEnd();
        }
        
        /// <summary>
        /// 現場CDFromのバリデート処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customAlphaNumTextBoxGenbaStartCD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var genbaCd = this.customAlphaNumTextBoxGenbaStartCD.Text;
            var gyoushaCd = this.customAlphaNumTextBoxGyoushaStartCD.Text;
            if (String.IsNullOrEmpty(genbaCd))
            {
                this.customTextBoxGenbaStartMeisho.Text = String.Empty;
                return;
            }

            if (!this.CheckGyoushaCd())
            {
                e.Cancel = true;
            }
            else if (!this.CheckGenba(gyoushaCd, genbaCd, true))
            {
                e.Cancel = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDToのバリデート処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customAlphaNumTextBoxGenbaEndCD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var genbaCd = this.customAlphaNumTextBoxGenbaEndCD.Text;
            var gyoushaCd = this.customAlphaNumTextBoxGyoushaEndCD.Text;
            if (String.IsNullOrEmpty(genbaCd))
            {
                this.customTextBoxGenbaEndMeisho.Text = String.Empty;
                return;
            }

            if (!this.CheckGyoushaCd())
            {
                e.Cancel = true;
            }
            else if (!this.CheckGenba(gyoushaCd, genbaCd, false))
            {
                e.Cancel = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CD必須入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckGyoushaCd()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;

            try
            {
                var gyoushaCdFrom = this.customAlphaNumTextBoxGyoushaStartCD.Text;
                var gyoushaCdTo = this.customAlphaNumTextBoxGyoushaEndCD.Text;

                if (String.IsNullOrEmpty(gyoushaCdFrom) && String.IsNullOrEmpty(gyoushaCdTo))
                {
                    ret = false;

                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E012", "業者CD");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyoushaCd", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false; ;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 業者のFromとToに最小値と最大値をセットします
        /// </summary>
        private bool SetGyoushaCdFromTo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                IEnumerable<M_GYOUSHA> mGyoushaList = dao.GetAllValidData(new M_GYOUSHA());

                if (mGyoushaList.Count() > 0)
                {
                    var minGyousha = mGyoushaList.Where(g => g.GYOUSHA_CD == mGyoushaList.Min(gy => gy.GYOUSHA_CD)).FirstOrDefault();
                    var maxGyousha = mGyoushaList.OrderByDescending(m => m.GYOUSHA_CD).Where(g => g.GYOUSHA_CD == mGyoushaList.Max(gy => gy.GYOUSHA_CD)).FirstOrDefault();

                    if (String.IsNullOrEmpty(this.customAlphaNumTextBoxGyoushaStartCD.Text))
                    {
                        this.customAlphaNumTextBoxGyoushaStartCD.Text = minGyousha.GYOUSHA_CD;
                        this.customTextBoxGyoushaStartMeisho.Text = minGyousha.GYOUSHA_NAME_RYAKU;
                    }

                    if (String.IsNullOrEmpty(this.customAlphaNumTextBoxGyoushaEndCD.Text))
                    {
                        this.customAlphaNumTextBoxGyoushaEndCD.Text = maxGyousha.GYOUSHA_CD;
                        this.customTextBoxGyoushaEndMeisho.Text = maxGyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGyoushaCdFromTo", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGyoushaCdFromTo", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 業者CD入力チェック
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="isStartCd"></param>
        /// <returns></returns>
        private bool CheckGyousha(string gyoushaCd, bool isStartCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, isStartCd);

            bool ret = true;

            var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            var mGyoushaList = gyoushaDao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = gyoushaCd ,ISNOT_NEED_DELETE_FLG = true});
            if (0 == mGyoushaList.Count())
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "業者");

                if (isStartCd)
                {
                    this.customTextBoxGyoushaStartMeisho.Text = String.Empty;
                }
                else
                {
                    this.customTextBoxGyoushaEndMeisho.Text = String.Empty;
                }

                ret = false;
            }
            else
            {
                if (isStartCd)
                {
                    this.customTextBoxGyoushaStartMeisho.Text = mGyoushaList.FirstOrDefault().GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.customTextBoxGyoushaEndMeisho.Text = mGyoushaList.FirstOrDefault().GYOUSHA_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 現場CD入力チェック
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <param name="isStartCd"></param>
        /// <returns></returns>
        private bool CheckGenba(string gyoushaCd, string genbaCd, bool isStartCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd, isStartCd);

            bool ret = true;

            try
            {
                var genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                var mGenbaList = genbaDao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd ,ISNOT_NEED_DELETE_FLG = true});
                if (0 == mGenbaList.Count())
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");

                    if (isStartCd)
                    {
                        this.customTextBoxGenbaStartMeisho.Text = String.Empty;
                    }
                    else
                    {
                        this.customTextBoxGenbaEndMeisho.Text = String.Empty;
                    }

                    ret = false;
                }
                else
                {
                    if (isStartCd)
                    {
                        this.customTextBoxGenbaStartMeisho.Text = mGenbaList.FirstOrDefault().GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        this.customTextBoxGenbaEndMeisho.Text = mGenbaList.FirstOrDefault().GENBA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGenba", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 業者CDの入力状態に応じて現場CDテキストボックスの使用可、不可を制御
        /// </summary>
        private bool ChangeGenbaCdTextBoxEnabled()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var gyoushaCdFrom = this.customAlphaNumTextBoxGyoushaStartCD.Text;
                var gyoushaCdTo = this.customAlphaNumTextBoxGyoushaEndCD.Text;

                if (String.IsNullOrEmpty(gyoushaCdFrom) == false && String.IsNullOrEmpty(gyoushaCdTo) == false && this.ZeroSuppressGenbaCd(gyoushaCdFrom) == this.ZeroSuppressGenbaCd(gyoushaCdTo))
                {
                    // 現場CDテキストボックスの活性状態を初期化
                    this.customAlphaNumTextBoxGenbaStartCD.Enabled = true;
                    this.customTextBoxGenbaStartMeisho.Enabled = true;
                    this.customPopupOpenButtonGenbaStartMeishoSearch.Enabled = true;
                    this.customAlphaNumTextBoxGenbaEndCD.Enabled = true;
                    this.customTextBoxGenbaEndMeisho.Enabled = true;
                    this.customPopupOpenButtonGenbaEndMeishoSearch.Enabled = true;
                }
                else
                {
                    this.customAlphaNumTextBoxGenbaStartCD.Enabled = false;
                    this.customTextBoxGenbaStartMeisho.Enabled = false;
                    this.customPopupOpenButtonGenbaStartMeishoSearch.Enabled = false;
                    this.customAlphaNumTextBoxGenbaEndCD.Enabled = false;
                    this.customTextBoxGenbaEndMeisho.Enabled = false;
                    this.customPopupOpenButtonGenbaEndMeishoSearch.Enabled = false;

                    this.customAlphaNumTextBoxGenbaStartCD.Text = String.Empty;
                    this.customTextBoxGenbaStartMeisho.Text = String.Empty;
                    this.customAlphaNumTextBoxGenbaEndCD.Text = String.Empty;
                    this.customTextBoxGenbaEndMeisho.Text = String.Empty;
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
        /// 入出荷物区分の変更時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomNumericTextBox2NyuuShukkaKbn_TextChanged(object sender, EventArgs e)
        {
            this.ChangeNyuuShukkaKbn();
        }

        /// <summary>
        /// 入出荷物区分の変更に伴う抽出範囲使用可、不可の制御
        /// </summary>
        private bool ChangeNyuuShukkaKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string NyuuShukkaKbn = this.CustomNumericTextBox2NyuuShukkaKbn.Text;
                switch (NyuuShukkaKbn)
                {
                    case "1":
                        // 品名
                        this.customAlphaNumTextBoxHinmeiStartCD.Enabled = true;
                        this.customTextBoxHinmeiStartMeisho.Enabled = true;
                        this.customAlphaNumTextBoxHinmeiEndCD.Enabled = true;
                        this.customTextBoxHinmeiEndMeisho.Enabled = true;

                        this.customPopupOpenButtonHinmeiStartMeishoSearch.Enabled = true;
                        this.customPopupOpenButtonHinmeiEndMeishoSearch.Enabled = true;

                        this.customAlphaNumTextBoxShuruiStartCD.Enabled = false;
                        this.customTextBoxShuruiStartMeisho.Enabled = false;
                        this.customAlphaNumTextBoxShuruiEndCD.Enabled = false;
                        this.customTextBoxShuruiEndMeisho.Enabled = false;

                        this.customPopupOpenButtonShuruiStartMeishoSearch.Enabled = false;
                        this.customPopupOpenButtonShuruiEndMeishoSearch.Enabled = false;

                        this.customAlphaNumTextBoxBunruiStartCD.Enabled = false;
                        this.customTextBoxBunruiStartMeisho.Enabled = false;
                        this.customAlphaNumTextBoxBunruiEndCD.Enabled = false;
                        this.customTextBoxBunruiEndMeisho.Enabled = false;

                        this.customPopupOpenButtonBunruiStartMeishoSearch.Enabled = false;
                        this.customPopupOpenButtonBunruiEndMeishoSearch.Enabled = false;

                        this.customAlphaNumTextBoxShuruiStartCD.Text = string.Empty;
                        this.customTextBoxShuruiStartMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxShuruiEndCD.Text = string.Empty;
                        this.customTextBoxShuruiEndMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxBunruiStartCD.Text = string.Empty;
                        this.customTextBoxBunruiStartMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxBunruiEndCD.Text = string.Empty;
                        this.customTextBoxBunruiEndMeisho.Text = string.Empty;
                        break;
                    case "2":
                        // 種類
                        this.customAlphaNumTextBoxHinmeiStartCD.Enabled = false;
                        this.customTextBoxHinmeiStartMeisho.Enabled = false;
                        this.customAlphaNumTextBoxHinmeiEndCD.Enabled = false;
                        this.customTextBoxHinmeiEndMeisho.Enabled = false;

                        this.customPopupOpenButtonHinmeiStartMeishoSearch.Enabled = false;
                        this.customPopupOpenButtonHinmeiEndMeishoSearch.Enabled = false;

                        this.customAlphaNumTextBoxShuruiStartCD.Enabled = true;
                        this.customTextBoxShuruiStartMeisho.Enabled = true;
                        this.customAlphaNumTextBoxShuruiEndCD.Enabled = true;
                        this.customTextBoxShuruiEndMeisho.Enabled = true;

                        this.customPopupOpenButtonShuruiStartMeishoSearch.Enabled = true;
                        this.customPopupOpenButtonShuruiEndMeishoSearch.Enabled = true;

                        this.customAlphaNumTextBoxBunruiStartCD.Enabled = false;
                        this.customTextBoxBunruiStartMeisho.Enabled = false;
                        this.customAlphaNumTextBoxBunruiEndCD.Enabled = false;
                        this.customTextBoxBunruiEndMeisho.Enabled = false;

                        this.customPopupOpenButtonBunruiStartMeishoSearch.Enabled = false;
                        this.customPopupOpenButtonBunruiEndMeishoSearch.Enabled = false;

                        this.customAlphaNumTextBoxHinmeiStartCD.Text = string.Empty;
                        this.customTextBoxHinmeiStartMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxHinmeiEndCD.Text = string.Empty;
                        this.customTextBoxHinmeiEndMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxBunruiStartCD.Text = string.Empty;
                        this.customTextBoxBunruiStartMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxBunruiEndCD.Text = string.Empty;
                        this.customTextBoxBunruiEndMeisho.Text = string.Empty;
                        break;
                    case "3":
                        // 分類
                        this.customAlphaNumTextBoxHinmeiStartCD.Enabled = false;
                        this.customTextBoxHinmeiStartMeisho.Enabled = false;
                        this.customAlphaNumTextBoxHinmeiEndCD.Enabled = false;
                        this.customTextBoxHinmeiEndMeisho.Enabled = false;

                        this.customPopupOpenButtonHinmeiStartMeishoSearch.Enabled = false;
                        this.customPopupOpenButtonHinmeiEndMeishoSearch.Enabled = false;

                        this.customAlphaNumTextBoxShuruiStartCD.Enabled = false;
                        this.customTextBoxShuruiStartMeisho.Enabled = false;
                        this.customAlphaNumTextBoxShuruiEndCD.Enabled = false;
                        this.customTextBoxShuruiEndMeisho.Enabled = false;

                        this.customPopupOpenButtonShuruiStartMeishoSearch.Enabled = false;
                        this.customPopupOpenButtonShuruiEndMeishoSearch.Enabled = false;

                        this.customAlphaNumTextBoxBunruiStartCD.Enabled = true;
                        this.customTextBoxBunruiStartMeisho.Enabled = true;
                        this.customAlphaNumTextBoxBunruiEndCD.Enabled = true;
                        this.customTextBoxBunruiEndMeisho.Enabled = true;

                        this.customPopupOpenButtonBunruiStartMeishoSearch.Enabled = true;
                        this.customPopupOpenButtonBunruiEndMeishoSearch.Enabled = true;

                        this.customAlphaNumTextBoxHinmeiStartCD.Text = string.Empty;
                        this.customTextBoxHinmeiStartMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxHinmeiEndCD.Text = string.Empty;
                        this.customTextBoxHinmeiEndMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxShuruiStartCD.Text = string.Empty;
                        this.customTextBoxShuruiStartMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxShuruiEndCD.Text = string.Empty;
                        this.customTextBoxShuruiEndMeisho.Text = string.Empty;
                        break;
                    default:
                        this.customAlphaNumTextBoxHinmeiStartCD.Enabled = false;
                        this.customTextBoxHinmeiStartMeisho.Enabled = false;
                        this.customAlphaNumTextBoxHinmeiEndCD.Enabled = false;
                        this.customTextBoxHinmeiEndMeisho.Enabled = false;
                        this.customAlphaNumTextBoxShuruiStartCD.Enabled = false;
                        this.customTextBoxShuruiStartMeisho.Enabled = false;
                        this.customAlphaNumTextBoxShuruiEndCD.Enabled = false;
                        this.customTextBoxShuruiEndMeisho.Enabled = false;
                        this.customAlphaNumTextBoxBunruiStartCD.Enabled = false;
                        this.customTextBoxBunruiStartMeisho.Enabled = false;
                        this.customAlphaNumTextBoxBunruiEndCD.Enabled = false;
                        this.customTextBoxBunruiEndMeisho.Enabled = false;

                        this.customAlphaNumTextBoxHinmeiStartCD.Text = string.Empty;
                        this.customTextBoxHinmeiStartMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxHinmeiEndCD.Text = string.Empty;
                        this.customTextBoxHinmeiEndMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxShuruiStartCD.Text = string.Empty;
                        this.customTextBoxShuruiStartMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxShuruiEndCD.Text = string.Empty;
                        this.customTextBoxShuruiEndMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxBunruiStartCD.Text = string.Empty;
                        this.customTextBoxBunruiStartMeisho.Text = string.Empty;
                        this.customAlphaNumTextBoxBunruiEndCD.Text = string.Empty;
                        this.customTextBoxBunruiEndMeisho.Text = string.Empty;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeNyuuShukkaKbn", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 運搬業者CD（From）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customAlphaNumTextBoxUnpanGyoushaStartCD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckUnpanGyoushaCd(true);
        }

        /// <summary>
        /// 運搬業者CD（To）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customAlphaNumTextBoxUnpanGyoushaEndCD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckUnpanGyoushaCd(false);
        }

        /// <summary>
        /// 形態区分（From）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customAlphaNumTextBoxKeitaiKbnStartCD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckKeitaiKbn(true);
        }

        /// <summary>
        /// 形態区分（To）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customAlphaNumTextBoxKeitaiKbnEndCD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckKeitaiKbn(false);
        }

        /// <summary>
        /// 形態区分検索ボタン（From）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customPopupOpenButtonKeitaiKbnStartMeishoSearch_Validated(object sender, EventArgs e)
        {
            this.logic.CheckKeitaiKbn(true);
        }

        /// <summary>
        /// 形態区分検索ボタン（To）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customPopupOpenButtonKeitaiKbnEndMeishoSearch_Validated(object sender, EventArgs e)
        {
            this.logic.CheckKeitaiKbn(false);
        }

        /// <summary>
        /// 品名（From）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customAlphaNumTextBoxHinmeiStartCD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckHinmeiCd(true);
        }

        /// <summary>
        /// 品名（To）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customAlphaNumTextBoxHinmeiEndCD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckHinmeiCd(false);
        }

        /// <summary>
        /// 品名検索ボタン（From）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customPopupOpenButtonHinmeiStartMeishoSearch_Validated(object sender, EventArgs e)
        {
            this.logic.CheckHinmeiCd(true);
        }

        /// <summary>
        /// 品名検索ボタン（To）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customPopupOpenButtonHinmeiEndMeishoSearch_Validated(object sender, EventArgs e)
        {
            this.logic.CheckHinmeiCd(false);
        }

        /// <summary>
        /// 業者検索ボタン（From）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customPopupOpenButtonGyoushaStartMeishoSearch_Validated(object sender, EventArgs e)
        {
            this.ChangeGenbaCdTextBoxEnabled();
        }

        /// <summary>
        /// 業者検索ボタン（To）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customPopupOpenButtonGyoushaEndMeishoSearch_Validated(object sender, EventArgs e)
        {
            this.ChangeGenbaCdTextBoxEnabled();
        }

        /// <summary>
        /// 日付Toのテキストボックスでダブルクリックしたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customDateTimePickerHidukeHaniShiteiEnd_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (String.IsNullOrEmpty(this.customDateTimePickerHidukeHaniShiteiEnd.Text))
            //{
            this.customDateTimePickerHidukeHaniShiteiEnd.Text = this.customDateTimePickerHidukeHaniShiteiStart.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// To項目をダブルクリックしたとき、From項目の入力内容をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customAlphaNumTextBoxGyoushaEndCD_DoubleClick(object sender, EventArgs e)
        {
            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (string.IsNullOrEmpty(this.customAlphaNumTextBoxGyoushaEndCD.Text))
            //{
            this.customAlphaNumTextBoxGyoushaEndCD.Text = this.customAlphaNumTextBoxGyoushaStartCD.Text;
            this.customTextBoxGyoushaEndMeisho.Text = this.customTextBoxGyoushaStartMeisho.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
        }

        private void customAlphaNumTextBoxGenbaEndCD_DoubleClick(object sender, EventArgs e)
        {
            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (string.IsNullOrEmpty(this.customAlphaNumTextBoxGenbaEndCD.Text))
            //{
            this.customAlphaNumTextBoxGenbaEndCD.Text = this.customAlphaNumTextBoxGenbaStartCD.Text;
            this.customTextBoxGenbaEndMeisho.Text = this.customTextBoxGenbaStartMeisho.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
        }

        private void customAlphaNumTextBoxUnpanGyoushaEndCD_DoubleClick(object sender, EventArgs e)
        {
            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (string.IsNullOrEmpty(this.customAlphaNumTextBoxUnpanGyoushaEndCD.Text))
            //{
            this.customAlphaNumTextBoxUnpanGyoushaEndCD.Text = this.customAlphaNumTextBoxUnpanGyoushaStartCD.Text;
            this.customTextBoxUnpanGyoushaEndMeisho.Text = this.customTextBoxUnpanGyoushaStartMeisho.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
        }

        private void customAlphaNumTextBoxKeitaiKbnEndCD_DoubleClick(object sender, EventArgs e)
        {
            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (string.IsNullOrEmpty(this.customAlphaNumTextBoxKeitaiKbnEndCD.Text))
            //{
            this.customAlphaNumTextBoxKeitaiKbnEndCD.Text = this.customAlphaNumTextBoxKeitaiKbnStartCD.Text;
            this.customTextBoxKeitaiKbnEndMeisho.Text = this.customTextBoxKeitaiKbnStartMeisho.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
        }

        private void customAlphaNumTextBoxHinmeiEndCD_DoubleClick(object sender, EventArgs e)
        {
            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (string.IsNullOrEmpty(this.customAlphaNumTextBoxHinmeiEndCD.Text))
            //{
            this.customAlphaNumTextBoxHinmeiEndCD.Text = this.customAlphaNumTextBoxHinmeiStartCD.Text;
            this.customTextBoxHinmeiEndMeisho.Text = this.customTextBoxHinmeiStartMeisho.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
        }

        private void customAlphaNumTextBoxShuruiEndCD_DoubleClick(object sender, EventArgs e)
        {
            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (string.IsNullOrEmpty(this.customAlphaNumTextBoxShuruiEndCD.Text))
            //{
            this.customAlphaNumTextBoxShuruiEndCD.Text = this.customAlphaNumTextBoxShuruiStartCD.Text;
            this.customTextBoxShuruiEndMeisho.Text = this.customTextBoxShuruiStartMeisho.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
        }

        private void customAlphaNumTextBoxBunruiEndCD_DoubleClick(object sender, EventArgs e)
        {
            // 20141205 teikyou ダブルクリックしたときに処理されます start
            //if (string.IsNullOrEmpty(this.customAlphaNumTextBoxBunruiEndCD.Text))
            //{
            this.customAlphaNumTextBoxBunruiEndCD.Text = this.customAlphaNumTextBoxBunruiStartCD.Text;
            this.customTextBoxBunruiEndMeisho.Text = this.customTextBoxBunruiStartMeisho.Text;
            //}
            // 20141205 teikyou ダブルクリックしたときに処理されます end
        }

        #endregion - Methods -

        /// <summary>
        /// 業者CD順のラジオボタンのチェック状態が切り替わったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customRadioButtonSort1_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGroupCheckBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者フリガナ順のラジオボタンのチェック状態が切り替わったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customRadioButtonSort2_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGroupCheckBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 伝票日付順のラジオボタンのチェック状態が切り替わったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customRadioButtonSort3_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGroupCheckBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 伝票番号順のラジオボタンのチェック状態が切り替わったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customRadioButtonSort4_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeGroupCheckBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 並び順の選択によって、集計単位のチェックボックスの活性or不活性を切り替えます
        /// </summary>
        private bool ChangeGroupCheckBoxEnabled()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.customRadioButtonSort1.Checked || this.customRadioButtonSort2.Checked)
                {
                    this.customCheckBoxGyousha.Enabled = true;
                    this.customCheckBoxGenba.Enabled = true;
                    this.customCheckBoxDenpyouNumber.Enabled = true;

                    this.customCheckBoxGyousha.Checked = true;
                    this.customCheckBoxGenba.Checked = true;
                    this.customCheckBoxDenpyouNumber.Checked = true;
                }
                else if (this.customRadioButtonSort3.Checked || this.customRadioButtonSort4.Checked)
                {
                    this.customCheckBoxGyousha.Enabled = false;
                    this.customCheckBoxGenba.Enabled = false;
                    this.customCheckBoxDenpyouNumber.Enabled = true;

                    this.customCheckBoxGyousha.Checked = false;
                    this.customCheckBoxGenba.Checked = false;
                    this.customCheckBoxDenpyouNumber.Checked = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeGroupCheckBoxEnabled", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 現場CDのFromとToに最小値と最大値をセットします
        /// </summary>
        private void SetGenbaCdFromTo()
        {
            LogUtility.DebugMethodStart();

            var dao = DaoInitUtility.GetComponent<IM_GENBADao>();
            var mGenbaArray = dao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = this.customAlphaNumTextBoxGyoushaStartCD.Text });

            IEnumerable<M_GENBA> mGenbaList = new List<M_GENBA>();
            mGenbaList = mGenbaArray;

            if (mGenbaList.Count() > 0)
            {
                var minGenba = mGenbaList.Where(g => g.GENBA_CD == mGenbaList.Min(ge => ge.GENBA_CD)).FirstOrDefault();
                var maxGenba = mGenbaList.OrderByDescending(m => m.GENBA_CD).Where(g => g.GENBA_CD == mGenbaList.Max(ge => ge.GENBA_CD)).FirstOrDefault();

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxGenbaStartCD.Text))
                {
                    this.customAlphaNumTextBoxGenbaStartCD.Text = minGenba.GENBA_CD;
                    this.customTextBoxGenbaStartMeisho.Text = minGenba.GENBA_NAME_RYAKU;
                }

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxGenbaEndCD.Text))
                {
                    this.customAlphaNumTextBoxGenbaEndCD.Text = maxGenba.GENBA_CD;
                    this.customTextBoxGenbaEndMeisho.Text = maxGenba.GENBA_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬業者CDのFromとToに最小値と最大値をセットします
        /// </summary>
        private void SetUnpanGyoushaCdFromTo()
        {
            LogUtility.DebugMethodStart();

            var dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            IEnumerable<M_GYOUSHA> mGyoushaList = dao.GetAllValidData(new M_GYOUSHA());

            if (mGyoushaList.Count() > 0)
            {
                var minGyousha = mGyoushaList.Where(g => g.GYOUSHA_CD == mGyoushaList.Min(gy => gy.GYOUSHA_CD)).FirstOrDefault();
                var maxGyousha = mGyoushaList.OrderByDescending(m => m.GYOUSHA_CD).Where(g => g.GYOUSHA_CD == mGyoushaList.Max(gy => gy.GYOUSHA_CD)).FirstOrDefault();

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxUnpanGyoushaStartCD.Text))
                {
                    this.customAlphaNumTextBoxUnpanGyoushaStartCD.Text = minGyousha.GYOUSHA_CD;
                    this.customTextBoxUnpanGyoushaStartMeisho.Text = minGyousha.GYOUSHA_NAME_RYAKU;
                }

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxUnpanGyoushaEndCD.Text))
                {
                    this.customAlphaNumTextBoxUnpanGyoushaEndCD.Text = maxGyousha.GYOUSHA_CD;
                    this.customTextBoxUnpanGyoushaEndMeisho.Text = maxGyousha.GYOUSHA_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 形態区分のFromとToに最小値と最大値をセットします
        /// </summary>
        private void SetKeitaiKbnFromTo()
        {
            LogUtility.DebugMethodStart();

            var dao = DaoInitUtility.GetComponent<IM_KEITAI_KBNDao>();
            var mKeitaiKbnArray = dao.GetAllValidData(new M_KEITAI_KBN());

            IEnumerable<M_KEITAI_KBN> mKeitaiKbnList = new List<M_KEITAI_KBN>();
            if (this.WindowId == WINDOW_ID.T_UKEIRE_MEISAIHYOU_KOTEI)
            {
                mKeitaiKbnList = mKeitaiKbnArray.Where(k => k.DENSHU_KBN_CD.Value == 1 || k.DENSHU_KBN_CD.Value == 3 || k.DENSHU_KBN_CD.Value == 9);
            }
            else
            {
                mKeitaiKbnList = mKeitaiKbnArray.Where(k => k.DENSHU_KBN_CD.Value == 2 || k.DENSHU_KBN_CD.Value == 3 || k.DENSHU_KBN_CD.Value == 9);
            }

            if (mKeitaiKbnList.Count() > 0)
            {
                var minKeitaiKbn = mKeitaiKbnList.Where(k => k.KEITAI_KBN_CD.Value == mKeitaiKbnList.Min(ke => ke.KEITAI_KBN_CD).Value).FirstOrDefault();
                var maxKeitaiKbn = mKeitaiKbnList.Where(k => k.KEITAI_KBN_CD.Value == mKeitaiKbnList.Max(ke => ke.KEITAI_KBN_CD).Value).FirstOrDefault();

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxKeitaiKbnStartCD.Text))
                {
                    this.customAlphaNumTextBoxKeitaiKbnStartCD.Text = minKeitaiKbn.KEITAI_KBN_CD.Value.ToString("00");
                    this.customTextBoxKeitaiKbnStartMeisho.Text = minKeitaiKbn.KEITAI_KBN_NAME_RYAKU;
                }

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxKeitaiKbnEndCD.Text))
                {
                    this.customAlphaNumTextBoxKeitaiKbnEndCD.Text = maxKeitaiKbn.KEITAI_KBN_CD.Value.ToString("00");
                    this.customTextBoxKeitaiKbnEndMeisho.Text = maxKeitaiKbn.KEITAI_KBN_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 品名のFromとToに最小値と最大値をセットします
        /// </summary>
        private void SetHinmeiCdFromTo()
        {
            LogUtility.DebugMethodStart();

            var dao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            var mHinmeiArray = dao.GetAllValidData(new M_HINMEI());

            IEnumerable<M_HINMEI> mHinmeiList = new List<M_HINMEI>();
            if (this.WindowId == WINDOW_ID.T_UKEIRE_MEISAIHYOU_KOTEI)
            {
                mHinmeiList = mHinmeiArray.Where(h => h.DENSHU_KBN_CD.Value == 1 || h.DENSHU_KBN_CD.Value == 9);
            }
            else
            {
                mHinmeiList = mHinmeiArray.Where(h => h.DENSHU_KBN_CD.Value == 2 || h.DENSHU_KBN_CD.Value == 9);
            }

            if (mHinmeiList.Count() > 0)
            {
                var minHinmei = mHinmeiList.Where(h => h.HINMEI_CD == mHinmeiList.Min(hi => hi.HINMEI_CD)).FirstOrDefault();
                var maxHinmei = mHinmeiList.Where(h => h.HINMEI_CD == mHinmeiList.Max(hi => hi.HINMEI_CD)).FirstOrDefault();

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxHinmeiStartCD.Text))
                {
                    this.customAlphaNumTextBoxHinmeiStartCD.Text = minHinmei.HINMEI_CD;
                    this.customTextBoxHinmeiStartMeisho.Text = minHinmei.HINMEI_NAME_RYAKU;
                }

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxHinmeiEndCD.Text))
                {
                    this.customAlphaNumTextBoxHinmeiEndCD.Text = maxHinmei.HINMEI_CD;
                    this.customTextBoxHinmeiEndMeisho.Text = maxHinmei.HINMEI_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 種類のFromとToに最小値と最大値をセットします
        /// </summary>
        private void SetShuruiFromTo()
        {
            LogUtility.DebugMethodStart();

            var dao = DaoInitUtility.GetComponent<IM_SHURUIDao>();
            var mShuruiArray = dao.GetAllValidData(new M_SHURUI());

            IEnumerable<M_SHURUI> mShuruiList = new List<M_SHURUI>();
            mShuruiList = mShuruiArray;

            if (mShuruiList.Count() > 0)
            {
                var minShurui = mShuruiList.Where(s => s.SHURUI_CD == mShuruiList.Min(sh => sh.SHURUI_CD)).FirstOrDefault();
                var maxShurui = mShuruiList.Where(s => s.SHURUI_CD == mShuruiList.Max(sh => sh.SHURUI_CD)).FirstOrDefault();

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxShuruiStartCD.Text))
                {
                    this.customAlphaNumTextBoxShuruiStartCD.Text = minShurui.SHURUI_CD;
                    this.customTextBoxShuruiStartMeisho.Text = minShurui.SHURUI_NAME_RYAKU;
                }

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxShuruiEndCD.Text))
                {
                    this.customAlphaNumTextBoxShuruiEndCD.Text = maxShurui.SHURUI_CD;
                    this.customTextBoxShuruiEndMeisho.Text = maxShurui.SHURUI_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 分類のFromとToに最小値と最大値をセットします
        /// </summary>
        private void SetBunruiFromTo()
        {
            LogUtility.DebugMethodStart();

            var dao = DaoInitUtility.GetComponent<IM_BUNRUIDao>();
            var mBunruiArray = dao.GetAllValidData(new M_BUNRUI());

            IEnumerable<M_BUNRUI> mBunruiList = new List<M_BUNRUI>();
            mBunruiList = mBunruiArray;

            if (mBunruiList.Count() > 0)
            {
                var minBunrui = mBunruiList.Where(s => s.BUNRUI_CD == mBunruiList.Min(sh => sh.BUNRUI_CD)).FirstOrDefault();
                var maxBunrui = mBunruiList.Where(s => s.BUNRUI_CD == mBunruiList.Max(sh => sh.BUNRUI_CD)).FirstOrDefault();

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxBunruiStartCD.Text))
                {
                    this.customAlphaNumTextBoxBunruiStartCD.Text = minBunrui.BUNRUI_CD;
                    this.customTextBoxBunruiStartMeisho.Text = minBunrui.BUNRUI_NAME_RYAKU;
                }

                if (String.IsNullOrEmpty(this.customAlphaNumTextBoxBunruiEndCD.Text))
                {
                    this.customAlphaNumTextBoxBunruiEndCD.Text = maxBunrui.BUNRUI_CD;
                    this.customTextBoxBunruiEndMeisho.Text = maxBunrui.BUNRUI_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
        private void customDateTimePickerHidukeHaniShiteiStart_Leave(object sender, EventArgs e)
        {
            this.customDateTimePickerHidukeHaniShiteiEnd.IsInputErrorOccured = false;
            this.customDateTimePickerHidukeHaniShiteiEnd.BackColor = Constans.NOMAL_COLOR;
        }

        private void customDateTimePickerHidukeHaniShiteiEnd_Leave(object sender, EventArgs e)
        {
            this.customDateTimePickerHidukeHaniShiteiStart.IsInputErrorOccured = false;
            this.customDateTimePickerHidukeHaniShiteiStart.BackColor = Constans.NOMAL_COLOR;
        }
        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

        private void customAlphaNumTextBoxGyoushaStartCD_Validating(object sender, CancelEventArgs e)
        {
            this.ChangeGenbaCdTextBoxEnabled();
        }

        private void customAlphaNumTextBoxGyoushaEndCD_Validating(object sender, CancelEventArgs e)
        {
            this.ChangeGenbaCdTextBoxEnabled();
        }

        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            this.ChangeGenbaCdTextBoxEnabled();
        }
    }

    #endregion - Class -
}
