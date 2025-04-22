// $Id:
using System;
using System.Data;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou.DAO;
using Seasar.Framework.Exceptions;
using r_framework.Dao;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Windows.Forms;

namespace Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou
{
    #region - Class -

    #region - LogicClass -

    /// <summary>
    /// G569 受入明細表ビジネスロジック
    /// </summary>
    internal class UkeireShukkaMeisaihyouLogicClass : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>ボタン情報を格納しているＸＭＬファイルのパス（リソース）を保持するフィールド</summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou.Setting.UkeireShukkaMeisaihyouButtonSetting.xml";

        /// <summary>フォーム</summary>
        private UIForm_UkeireShukkaMeisaihyou form;
        private HeaderBaseForm header;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>IM_GYOUSHADao</summary>
        private r_framework.Dao.IM_GYOUSHADao gyoushaDao;

        /// <summary>IM_KEITAI_KBNDao</summary>
        private r_framework.Dao.IM_KEITAI_KBNDao keitaiKbnDao;

        /// <summary>IM_HINMEIDao</summary>
        private r_framework.Dao.IM_HINMEIDao hinmeiDao;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();
        
        /// <summary>
        /// システム設定エンティティ
        /// </summary>
        internal M_SYS_INFO mSysInfo;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="UkeireShukkaMeisaihyouLogicClass"/> class.</summary>
        /// <param name="targetForm">targetForm</param>
        public UkeireShukkaMeisaihyouLogicClass(UIForm_UkeireShukkaMeisaihyou targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.keitaiKbnDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KEITAI_KBNDao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_HINMEIDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>画面初期化処理</summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダーを初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.parentForm = (BusinessBaseForm)this.form.Parent;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            HeaderBaseForm targetHeader = (HeaderBaseForm)parentForm.headerForm;
            this.header = targetHeader;
            this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

            LogUtility.DebugMethodEnd();
        }

        #region - Function Key Proc -

        /// <summary>
        /// 帳票出力データを取得
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public bool Search(UkeireShukkaMeisaihyouDtoClass dto)
        {
            try
            {
                var dao = DaoInitUtility.GetComponent<IUkeireShukkaMeisaihyouDao>();
                var dt = dao.GetUkeireShukkaMeisaiData(dto);

                if (0 < dt.Rows.Count)
                {
                    if (1 == dto.Order || 2 == dto.Order)
                    {
                        var reportLogic = new UkeireShukkaMeisaihyouGyoushaReportClass();
                        reportLogic.CreateReport(dt, dto);
                    }
                    else if (3 == dto.Order)
                    {
                        var reportLogic = new UkeireShukkaMeisaihyouDenpyouDateReportClass();
                        reportLogic.CreateReport(dt, dto);
                    }
                    else if (4 == dto.Order)
                    {
                        var reportLogic = new UkeireShukkaMeisaihyouDenpyouNoReportClass();
                        reportLogic.CreateReport(dt, dto);
                    }
                }
                else
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        #endregion - Function Key Proc -

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>登録処理を実行する</summary>
        /// <param name="errorFlag">かどうかを表す値</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>検索処理を実行し数値を取得する</summary>
        /// <returns>？？？？？</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>更新処理を実行する</summary>
        /// <param name="errorFlag">エラーフラグかどうかを表す値</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>ボタン設定の読込</summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return null;
            }
        }

        /// <summary>ボタン初期化処理</summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>イベントの初期化処理</summary>
        private void EventInit()
        {
            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;

                // 明細項目CSVボタン(F5)イベント生成
                this.form.C_Regist(parentForm.bt_func5);
                parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);
                // 明細項目表示ボタン(F7)イベント生成
                this.form.C_Regist(parentForm.bt_func7);
                parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>
        /// 運搬業者CDチェック
        /// </summary>
        /// <param name="IsFrom"></param>
        /// <returns></returns>
        internal bool CheckUnpanGyoushaCd(bool IsFrom)
        {
            try
            {
                LogUtility.DebugMethodStart(IsFrom);

                var msgLogic = new MessageBoxShowLogic();
                var inputUnpanGyoushaCd = string.Empty;

                if (IsFrom)
                {
                    inputUnpanGyoushaCd = this.form.customAlphaNumTextBoxUnpanGyoushaStartCD.Text;
                }
                else
                {
                    inputUnpanGyoushaCd = this.form.customAlphaNumTextBoxUnpanGyoushaEndCD.Text;
                }

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if (!String.IsNullOrEmpty(inputUnpanGyoushaCd))
                {
                    var gyousha = this.GetGyousha(inputUnpanGyoushaCd);
                    if (gyousha != null)
                    {
                        // 20151026 BUNN #12040 STR
                        if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                        // 20151026 BUNN #12040 END
                        {
                            if (IsFrom)
                            {
                                this.form.customTextBoxUnpanGyoushaStartMeisho.Text = gyousha.GYOUSHA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.customTextBoxUnpanGyoushaEndMeisho.Text = gyousha.GYOUSHA_NAME_RYAKU;
                            }
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "業者");
                            if (IsFrom)
                            {
                                this.form.customTextBoxUnpanGyoushaStartMeisho.Text = string.Empty;
                                this.form.customAlphaNumTextBoxUnpanGyoushaStartCD.Focus();
                            }
                            else
                            {
                                this.form.customTextBoxUnpanGyoushaEndMeisho.Text = string.Empty;
                                this.form.customAlphaNumTextBoxUnpanGyoushaEndCD.Focus();
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        private M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return null;
            }

            M_GYOUSHA keyEntity = new M_GYOUSHA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

            if (gyousha == null || gyousha.Length < 1)
            {
                return null;
            }
            else
            {
                return gyousha[0];
            }
        }

        /// <summary>
        /// 形態区分チェック
        /// </summary>
        /// <param name="IsFrom"></param>
        internal bool CheckKeitaiKbn(bool IsFrom)
        {
            try
            {
                LogUtility.DebugMethodStart(IsFrom);

                var KeitaiKbnCtrl = new r_framework.CustomControl.CustomNumericTextBox2();
                var KeitaiKbnName = new r_framework.CustomControl.CustomTextBox();
                if (IsFrom)
                {
                    KeitaiKbnCtrl = this.form.customAlphaNumTextBoxKeitaiKbnStartCD;
                    KeitaiKbnName = this.form.customTextBoxKeitaiKbnStartMeisho;
                }
                else
                {
                    KeitaiKbnCtrl = this.form.customAlphaNumTextBoxKeitaiKbnEndCD;
                    KeitaiKbnName = this.form.customTextBoxKeitaiKbnEndMeisho;
                }

                if (string.IsNullOrEmpty(KeitaiKbnCtrl.Text))
                {
                    KeitaiKbnName.Text = string.Empty;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                short keitaiKbnCd;
                if (!short.TryParse(KeitaiKbnCtrl.Text, out keitaiKbnCd))
                {
                    KeitaiKbnName.Text = string.Empty;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                M_KEITAI_KBN keitaiKbn = this.GetkeitaiKbn(keitaiKbnCd);
                if (keitaiKbn == null)
                {
                    // エラーメッセージ（マスタに存在しない）
                    KeitaiKbnCtrl.IsInputErrorOccured = true;
                    KeitaiKbnCtrl.BackColor = Constans.ERROR_COLOR;
                    KeitaiKbnCtrl.ForeColor = Constans.ERROR_COLOR_FORE;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "形態区分");
                    KeitaiKbnName.Text = string.Empty;
                    KeitaiKbnCtrl.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                var denshuKbnCd = (DENSHU_KBN)Enum.ToObject(typeof(DENSHU_KBN), (int)keitaiKbn.DENSHU_KBN_CD);
                bool errFlg = false;
                switch (this.form.WindowId)
                {
                    case WINDOW_ID.T_UKEIRE_MEISAIHYOU_KOTEI:
                        if (denshuKbnCd != DENSHU_KBN.UKEIRE && denshuKbnCd != DENSHU_KBN.KYOUTSUU)
                        {
                            errFlg = true;
                        }
                        break;
                    case WINDOW_ID.T_SHUKKA_MEISAIHYOU_KOTEI:
                        if (denshuKbnCd != DENSHU_KBN.SHUKKA && denshuKbnCd != DENSHU_KBN.KYOUTSUU)
                        {
                            errFlg = true;
                        }
                        break;
                }
                if (errFlg)
                {
                    // エラーメッセージ（伝種区分と異なる）
                    KeitaiKbnCtrl.IsInputErrorOccured = true;
                    KeitaiKbnCtrl.BackColor = Constans.ERROR_COLOR;
                    KeitaiKbnCtrl.ForeColor = Constans.ERROR_COLOR_FORE;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "形態区分");
                    KeitaiKbnName.Text = string.Empty;
                    KeitaiKbnCtrl.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                else
                {
                    KeitaiKbnName.Text = keitaiKbn.KEITAI_KBN_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckKeitaiKbn", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKeitaiKbn", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 形態区分取得
        /// </summary>
        /// <param name="keitaiCbnCd"></param>
        /// <returns></returns>
        private M_KEITAI_KBN GetkeitaiKbn(short keitaiCbnCd)
        {
            if (keitaiCbnCd < 0)
            {
                return null;
            }

            M_KEITAI_KBN keyEntity = new M_KEITAI_KBN();
            keyEntity.KEITAI_KBN_CD = keitaiCbnCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var result = this.keitaiKbnDao.GetAllValidData(keyEntity);
            if (result == null || result.Length < 1)
            {
                return null;
            }

            return result[0];
        }

        /// <summary>
        /// 品名チェック
        /// </summary>
        /// <param name="IsFrom"></param>
        internal bool CheckHinmeiCd(bool IsFrom)
        {
            try
            {
                LogUtility.DebugMethodStart(IsFrom);

                var HinmeiCtrl = new r_framework.CustomControl.CustomAlphaNumTextBox();
                var HinmeiName = new r_framework.CustomControl.CustomTextBox();
                if (IsFrom)
                {
                    HinmeiCtrl = this.form.customAlphaNumTextBoxHinmeiStartCD;
                    HinmeiName = this.form.customTextBoxHinmeiStartMeisho;
                }
                else
                {
                    HinmeiCtrl = this.form.customAlphaNumTextBoxHinmeiEndCD;
                    HinmeiName = this.form.customTextBoxHinmeiEndMeisho;
                }

                if (string.IsNullOrEmpty(HinmeiCtrl.Text))
                {
                    HinmeiName.Text = string.Empty;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                M_HINMEI[] hinmeis = this.GetHinmeiData(HinmeiCtrl.Text);
                if (hinmeis == null || hinmeis.Length < 1)
                {
                    // エラーメッセージ（マスタに存在しない）
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "品名");
                    HinmeiName.Text = string.Empty;
                    HinmeiCtrl.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                else
                {
                    M_HINMEI hinmei = hinmeis[0];
                    var denshuKbnCd = (DENSHU_KBN)Enum.ToObject(typeof(DENSHU_KBN), (int)hinmei.DENSHU_KBN_CD);
                    bool errFlg = false;
                    switch (this.form.WindowId)
                    {
                        case WINDOW_ID.T_UKEIRE_MEISAIHYOU_KOTEI:
                            if (denshuKbnCd != DENSHU_KBN.UKEIRE && denshuKbnCd != DENSHU_KBN.KYOUTSUU)
                            {
                                errFlg = true;
                            }
                            break;
                        case WINDOW_ID.T_SHUKKA_MEISAIHYOU_KOTEI:
                            if (denshuKbnCd != DENSHU_KBN.SHUKKA && denshuKbnCd != DENSHU_KBN.KYOUTSUU)
                            {
                                errFlg = true;
                            }
                            break;
                    }
                    if (errFlg)
                    {
                        // エラーメッセージ（伝種区分と異なる）
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E058", "");
                        HinmeiName.Text = string.Empty;
                        HinmeiCtrl.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                    else
                    {
                        HinmeiName.Text = hinmei.HINMEI_NAME;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckHinmeiCd", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 品名マスタ取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private M_HINMEI[] GetHinmeiData(string key = null)
        {
            M_HINMEI keyEntity = new M_HINMEI();
            if (!string.IsNullOrEmpty(key))
            {
                keyEntity.HINMEI_CD = key;
            }
            keyEntity.ISNOT_NEED_DELETE_FLG = true;

            return this.hinmeiDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 業者CDの最大値と最小値を取得
        /// </summary>
        /// <param name="key"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        internal DataTable GetMinMaxKeyGyousha()
        {
            string sqlStr =
                "SELECT MIN(GYOUSHA_CD) AS RETURN_VALUE_MIN,MAX(GYOUSHA_CD) AS RETURN_VALUE_MAX FROM M_GYOUSHA " +
                "WHERE M_GYOUSHA.DELETE_FLG = 0";
            var dt = this.gyoushaDao.GetDateForStringSql(sqlStr);
            if (dt == null || dt.Rows.Count < 1)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }

        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            try
            {
                this.form.customDateTimePickerHidukeHaniShiteiStart.BackColor = Constans.NOMAL_COLOR;
                this.form.customDateTimePickerHidukeHaniShiteiEnd.BackColor = Constans.NOMAL_COLOR;
                // 入力されない場合
                if (string.IsNullOrEmpty(this.form.customDateTimePickerHidukeHaniShiteiStart.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.customDateTimePickerHidukeHaniShiteiEnd.Text))
                {
                    return false;
                }

                DateTime date_from = DateTime.Parse(this.form.customDateTimePickerHidukeHaniShiteiStart.Text);
                DateTime date_to = DateTime.Parse(this.form.customDateTimePickerHidukeHaniShiteiEnd.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.customDateTimePickerHidukeHaniShiteiStart.IsInputErrorOccured = true;
                    this.form.customDateTimePickerHidukeHaniShiteiEnd.IsInputErrorOccured = true;
                    this.form.customDateTimePickerHidukeHaniShiteiStart.BackColor = Constans.ERROR_COLOR;
                    this.form.customDateTimePickerHidukeHaniShiteiEnd.BackColor = Constans.ERROR_COLOR;
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    if (this.form.customRadioButtonHiduke1.Checked)
                    {
                        string[] errorMsg = { "伝票日付From", "伝票日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    else
                    {
                        string[] errorMsg = { "入力日付From", "入力日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    this.form.customDateTimePickerHidukeHaniShiteiStart.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDate", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
            return false;
        }
        #endregion
        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns></returns>
        internal void CSVPrint(DataTable dt, UkeireShukkaMeisaihyouDtoClass dto)
        {
            string headStr = "";

            switch (dto.Order)
            {
                case 1:
                    headStr = "業者CD,業者,現場CD,現場,運搬業者CD,運搬業者,伝票日付,売/支日付,伝票区分,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額,伝票番号";
                    break;
                case 2:
                    headStr = "業者CD,業者,現場CD,現場,運搬業者CD,運搬業者,伝票日付,売/支日付,伝票区分,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額,伝票番号";
                    break;
                case 3:
                    headStr = "伝票日付,業者CD,業者,現場CD,現場,運搬業者CD,運搬業者,売/支日付,伝票区分,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額,伝票番号";
                    break;
                case 4:
                    headStr = "伝票番号,伝票日付,業者CD,業者,現場CD,現場,運搬業者CD,運搬業者,売/支日付,伝票区分,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額";
                    break;
            }

            string[] csvHead;
            csvHead = headStr.Split(',');

            DataTable csvDT = new DataTable();
            DataRow rowTmp;
            for (int i = 0; i < csvHead.Length; i++)
            {
                csvDT.Columns.Add(csvHead[i]);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rowTmp = csvDT.NewRow();

                if (dt.Rows[i]["GYOUSHA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GYOUSHA_CD"].ToString()))
                {
                    rowTmp["業者CD"] = dt.Rows[i]["GYOUSHA_CD"].ToString();
                }

                if (dt.Rows[i]["GYOUSHA_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GYOUSHA_NAME"].ToString()))
                {
                    rowTmp["業者"] = dt.Rows[i]["GYOUSHA_NAME"].ToString();
                }

                if (dt.Rows[i]["GENBA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GENBA_CD"].ToString()))
                {
                    rowTmp["現場CD"] = dt.Rows[i]["GENBA_CD"].ToString();
                }

                if (dt.Rows[i]["GENBA_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GENBA_NAME"].ToString()))
                {
                    rowTmp["現場"] = dt.Rows[i]["GENBA_NAME"].ToString();
                }

                if (dt.Rows[i]["UNPAN_GYOUSHA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UNPAN_GYOUSHA_CD"].ToString()))
                {
                    rowTmp["運搬業者CD"] = dt.Rows[i]["UNPAN_GYOUSHA_CD"].ToString();
                }

                if (dt.Rows[i]["UNPAN_GYOUSHA_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UNPAN_GYOUSHA_NAME"].ToString()))
                {
                    rowTmp["運搬業者"] = dt.Rows[i]["UNPAN_GYOUSHA_NAME"].ToString();
                }

                if (dt.Rows[i]["DENPYOU_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DENPYOU_DATE"].ToString()))
                {
                    rowTmp["伝票日付"] = Convert.ToDateTime(dt.Rows[i]["DENPYOU_DATE"]).ToString("yyyy/MM/dd");
                }

                if (dt.Rows[i]["URIAGE_SHIHARAI_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["URIAGE_SHIHARAI_DATE"].ToString()))
                {
                    rowTmp["売/支日付"] = Convert.ToDateTime(dt.Rows[i]["URIAGE_SHIHARAI_DATE"]).ToString("yyyy/MM/dd");
                }

                if (dt.Rows[i]["DENPYOU_KBN_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DENPYOU_KBN_CD"].ToString()))
                {
                    if (dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1"))
                    {
                        rowTmp["伝票区分"] = "売上";
                    }
                    else
                    {
                        rowTmp["伝票区分"] = "支払";
                    }

                }

                if (dt.Rows[i]["TORIHIKI_KBN"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TORIHIKI_KBN"].ToString()))
                {
                    rowTmp["取引区分"] = dt.Rows[i]["TORIHIKI_KBN"].ToString();
                }

                if (dt.Rows[i]["SHIME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHIME"].ToString()))
                {
                    rowTmp["締"] = dt.Rows[i]["SHIME"].ToString();
                }

                if (dt.Rows[i]["DETAIL_KAKUTEI_KBN"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DETAIL_KAKUTEI_KBN"].ToString()))
                {
                    if (dt.Rows[i]["DETAIL_KAKUTEI_KBN"].ToString().Equals("1"))
                    {
                        rowTmp["確定"] = "確";
                    }
                    else
                    {
                        rowTmp["確定"] = "未";
                    }
                }

                if (dt.Rows[i]["HINMEI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["HINMEI_CD"].ToString()))
                {
                    rowTmp["品名CD"] = dt.Rows[i]["HINMEI_CD"].ToString();
                }

                if (dt.Rows[i]["HINMEI_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["HINMEI_NAME"].ToString()))
                {
                    rowTmp["品名"] = dt.Rows[i]["HINMEI_NAME"].ToString();
                }

                if (dt.Rows[i]["NET_JYUURYOU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NET_JYUURYOU"].ToString()))
                {
                    rowTmp["正味"] = Convert.ToDecimal(dt.Rows[i]["NET_JYUURYOU"]).ToString(this.mSysInfo.SYS_JYURYOU_FORMAT);
                }

                if (dt.Rows[i]["SUURYOU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SUURYOU"].ToString()))
                {
                    rowTmp["数量"] = Convert.ToDecimal(dt.Rows[i]["SUURYOU"]).ToString(this.mSysInfo.SYS_SUURYOU_FORMAT);
                }

                if (dt.Rows[i]["UNIT_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UNIT_CD"].ToString()))
                {
                    rowTmp["単位CD"] = dt.Rows[i]["UNIT_CD"].ToString();
                }

                if (dt.Rows[i]["UNIT_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UNIT_NAME_RYAKU"].ToString()))
                {
                    rowTmp["単位"] = dt.Rows[i]["UNIT_NAME_RYAKU"].ToString();
                }

                if (dt.Rows[i]["TANKA"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TANKA"].ToString()))
                {
                    rowTmp["単価"] = Convert.ToDecimal(dt.Rows[i]["TANKA"]).ToString(this.mSysInfo.SYS_TANKA_FORMAT);
                }

                if (dt.Rows[i]["SUM_KINGAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SUM_KINGAKU"].ToString()))
                {
                    rowTmp["金額"] = Convert.ToDecimal(dt.Rows[i]["SUM_KINGAKU"]).ToString("#,##0");
                }

                if (dt.Rows[i]["DENPYOU_NUMBER"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DENPYOU_NUMBER"].ToString()))
                {
                    rowTmp["伝票番号"] = dt.Rows[i]["DENPYOU_NUMBER"].ToString();
                }

                csvDT.Rows.Add(rowTmp);
            }

            // 一覧に明細行がない場合、アラートを表示し、CSV出力処理はしない
            if (csvDT.Rows.Count == 0)
            {
                this.errmessage.MessageBoxShow("E044");
                return;
            }
            // 出力先指定のポップアップを表示させる。
            if (this.errmessage.MessageBoxShow("C013") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();

                if (dto.DenpyouShuruiCd == 1)
                {
                    csvExport.ConvertDataTableToCsv(csvDT, true, true, "受入明細表", this.form);
                }
                else if (dto.DenpyouShuruiCd == 2)
                {
                    csvExport.ConvertDataTableToCsv(csvDT, true, true, "出荷明細表", this.form);
                }
            }
        }
        #endregion

        #endregion - Methods -
    }

    #endregion - LogicClass -

    #endregion - Class -
}
