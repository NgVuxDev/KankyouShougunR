// $Id:
using System;
using System.Data;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou.DAO;
using Seasar.Framework.Exceptions;
using r_framework.Entity;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou
{
    #region - Class -

    #region - LogicClass -

    /// <summary>
    /// G568 売上明細票 ビジネスロジック
    /// </summary>
    internal class UriageShiharaiMeisaihyouLogicClass : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>ボタン情報を格納しているＸＭＬファイルのパス（リソース）を保持するフィールド</summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou.Setting.UriageShiharaiMeisaihyouButtonSetting.xml";

        private HeaderBaseForm header;

        /// <summary>フォーム</summary>
        private UIForm_UriageShiharaiMeisaihyou form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>IM_TORIHIKISAKIDao</summary>
        private r_framework.Dao.IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>IM_GYOUSHADao</summary>
        private r_framework.Dao.IM_GYOUSHADao gyoushaDao;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// システム設定エンティティ
        /// </summary>
        internal M_SYS_INFO mSysInfo;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="UriageShiharaiMeisaihyouLogicClass"/> class.</summary>
        /// <param name="targetForm">targetForm</param>
        public UriageShiharaiMeisaihyouLogicClass(UIForm_UriageShiharaiMeisaihyou targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();

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
        /// 帳票出力データを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public bool Search(UriageShiharaiMeisaihyouDtoClass dto)
        {
            try
            {
                // とりあえず固定のクエリでデータ取ってくるだけ
                var dao = DaoInitUtility.GetComponent<IUriageShiharaiMeisaihyouDao>();
                var dt = dao.GetUriageShiharaiMeisaiData(dto);

                if (0 < dt.Rows.Count)
                {
                    if (1 == dto.Order || 2 == dto.Order)
                    {
                        var reportLogic = new UriageShiharaiMeisaihyouTorihikisakiReportClass();
                        reportLogic.CreateReport(dt, dto);
                    }
                    else if (3 == dto.Order)
                    {
                        var reportLogic = new UriageShiharaiMeisaihyouDenpyouDateReportClass();
                        reportLogic.CreateReport(dt, dto);
                    }
                    else if (4 == dto.Order)
                    {
                        var reportLogic = new UriageShiharaiMeisaihyouDenpyouNoReportClass();
                        reportLogic.CreateReport(dt, dto);
                    }
                }
                else
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
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
        /// 取引先CDの最大値と最小値を取得
        /// </summary>
        /// <returns></returns>
        internal DataTable GetMinMaxKeyTorihikisaki()
        {
            string sqlStr =
                "SELECT MIN(TORIHIKISAKI_CD) AS RETURN_VALUE_MIN,MAX(TORIHIKISAKI_CD) AS RETURN_VALUE_MAX FROM M_TORIHIKISAKI " +
                "WHERE M_TORIHIKISAKI.DELETE_FLG = 0";
            var dt = this.torihikisakiDao.GetDateForStringSql(sqlStr);
            if (dt == null || dt.Rows.Count < 1)
            {
                return null;
            }
            else
            {
                return dt;
            }
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
                this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
                // 入力されない場合
                if (string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                {
                    return false;
                }

                DateTime date_from = DateTime.Parse(this.form.HIDUKE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.form.HIDUKE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                    this.form.HIDUKE_TO.IsInputErrorOccured = true;
                    this.form.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    if (this.form.customRadioButtonHiduke1.Checked)
                    {
                        string[] errorMsg = { "伝票日付From", "伝票日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    else if (this.form.customRadioButtonHiduke2.Checked)
                    {
                        string[] errorMsg = { "支払日付From", "支払日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    else
                    {
                        string[] errorMsg = { "入力日付From", "入力日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    this.form.HIDUKE_FROM.Focus();
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
        internal void CSVPrint(DataTable dt, UriageShiharaiMeisaihyouDtoClass dto)
        {
            string headStr = "";

            if(dto.DenpyouKbnCd == 1)
            {
                switch (dto.Order)
                {
                    case 1:
                        headStr = "取引先CD,取引先,業者CD,業者,現場CD,現場,伝票日付,売上日付,伝票種類,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額,消費税,合計金額,伝票番号";
                        break;
                    case 2:
                        headStr = "取引先CD,取引先,業者CD,業者,現場CD,現場,伝票日付,売上日付,伝票種類,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額,消費税,合計金額,伝票番号";
                        break;
                    case 3:
                        headStr = "伝票日付,伝票種類,取引先CD,取引先,業者CD,業者,現場CD,現場,売上日付,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額,消費税,合計金額,伝票番号";
                        break;
                    case 4:
                        headStr = "伝票番号,伝票種類,取引先CD,取引先,業者CD,業者,現場CD,現場,伝票日付,売上日付,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額,消費税,合計金額";
                        break;
                }
            }
            else
            {
                switch (dto.Order)
                {
                    case 1:
                        headStr = "取引先CD,取引先,業者CD,業者,現場CD,現場,伝票日付,支払日付,伝票種類,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額,消費税,合計金額,伝票番号";
                        break;
                    case 2:
                        headStr = "取引先CD,取引先,業者CD,業者,現場CD,現場,伝票日付,支払日付,伝票種類,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額,消費税,合計金額,伝票番号";
                        break;
                    case 3:
                        headStr = "伝票日付,伝票種類,取引先CD,取引先,業者CD,業者,現場CD,現場,支払日付,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額,消費税,合計金額,伝票番号";
                        break;
                    case 4:
                        headStr = "伝票番号,伝票種類,取引先CD,取引先,業者CD,業者,現場CD,現場,伝票日付,支払日付,取引区分,締,確定,品名CD,品名,正味,数量,単位CD,単位,単価,金額,消費税,合計金額";
                        break;
                }
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

                if (dt.Rows[i]["TORIHIKISAKI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TORIHIKISAKI_CD"].ToString()))
                {
                    rowTmp["取引先CD"] = dt.Rows[i]["TORIHIKISAKI_CD"].ToString();
                }

                if (dt.Rows[i]["TORIHIKISAKI_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TORIHIKISAKI_NAME"].ToString()))
                {
                    rowTmp["取引先"] = dt.Rows[i]["TORIHIKISAKI_NAME"].ToString();
                }

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

                if (dt.Rows[i]["DENPYOU_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DENPYOU_DATE"].ToString()))
                {
                    rowTmp["伝票日付"] = Convert.ToDateTime(dt.Rows[i]["DENPYOU_DATE"]).ToString("yyyy/MM/dd");
                }

                if (dto.DenpyouKbnCd == 1)
                {
                    if (dt.Rows[i]["URIAGE_SHIHARAI_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["URIAGE_SHIHARAI_DATE"].ToString()))
                    {
                        rowTmp["売上日付"] = Convert.ToDateTime(dt.Rows[i]["URIAGE_SHIHARAI_DATE"]).ToString("yyyy/MM/dd");
                    }
                }
                else
                {
                    if (dt.Rows[i]["URIAGE_SHIHARAI_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["URIAGE_SHIHARAI_DATE"].ToString()))
                    {
                        rowTmp["支払日付"] = Convert.ToDateTime(dt.Rows[i]["URIAGE_SHIHARAI_DATE"]).ToString("yyyy/MM/dd");
                    }
                }

                if (dt.Rows[i]["DENPYOU_SHURUI"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DENPYOU_SHURUI"].ToString()))
                {
                    rowTmp["伝票種類"] = dt.Rows[i]["DENPYOU_SHURUI"].ToString();
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
                    rowTmp["単価"] = Convert.ToDecimal(dt.Rows[i]["TANKA"]).ToString(mSysInfo.SYS_TANKA_FORMAT);
                }

                if (dt.Rows[i]["ZEINUKI_KINGAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["ZEINUKI_KINGAKU"].ToString()))
                {
                    rowTmp["金額"] = Convert.ToDecimal(dt.Rows[i]["ZEINUKI_KINGAKU"]).ToString("#,##0");
                }

                if (dt.Rows[i]["SUM_KINGAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SUM_KINGAKU"].ToString()))
                {
                    rowTmp["合計金額"] = Convert.ToDecimal(ConvertNullOrEmptyToZero(dt.Rows[i]["ZEINUKI_KINGAKU"]) + ConvertNullOrEmptyToZero(dt.Rows[i]["MEISAI_TAX"])).ToString("#,##0");
                }

                if ("3" == dt.Rows[i]["ZEI_KEISAN_KBN_CD"].ToString()
                    || (dt.Rows[i]["HINMEI_ZEI_KBN_CD"] != null && !String.IsNullOrEmpty(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString())))
                {
                    if (dt.Rows[i]["MEISAI_TAX"] != null && !string.IsNullOrEmpty(dt.Rows[i]["MEISAI_TAX"].ToString()))
                    {
                        if (dt.Rows[i]["UCHIZEI"] != null && !String.IsNullOrEmpty(dt.Rows[i]["UCHIZEI"].ToString()))
                        {
                            rowTmp["消費税"] = Convert.ToDecimal(dt.Rows[i]["MEISAI_TAX"]).ToString("(#,##0)");
                        }
                        else
                        {
                            rowTmp["消費税"] = Convert.ToDecimal(dt.Rows[i]["MEISAI_TAX"]).ToString("#,##0");
                        }
                    }
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
                if (dto.DenpyouKbnCd == 1)
                {
                    csvExport.ConvertDataTableToCsv(csvDT, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_URIAGE_MEISAIHYOU), this.form);
                }
                else if (dto.DenpyouKbnCd == 2)
                {
                    csvExport.ConvertDataTableToCsv(csvDT, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_SHIHARAI_MEISAIHYOU), this.form);
                }
            }
        }
        #endregion

        /// <summary>
        /// オブジェクトをDecimal型に変換します
        /// </summary>
        /// <param name="value">対象のオブジェクト</param>
        /// <returns>NullかString.Emptyの場合、Decimal.Zeroを返します</returns>
        internal static decimal ConvertNullOrEmptyToZero(object value)
        {
            LogUtility.DebugMethodStart(value);

            decimal ret = Decimal.Zero;

            if (!String.IsNullOrEmpty(Convert.ToString(value)))
            {
                Decimal.TryParse(Convert.ToString(value), out ret);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion - Methods -
    }

    #endregion - LogicClass -


    #endregion - Class -
}
