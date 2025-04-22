using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.CustomControl;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Reception.UketsukeMeisaihyo
{
    /// <summary>
    /// 受付明細表 出力画面ロジッククラス
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region 定数

        /// <summary>ボタン設定XMLファイルパス</summary>
        private const string buttonInfoXmlPath = "Shougun.Core.Reception.UketsukeMeisaihyo.Setting.ButtonSetting.xml";
        #endregion

        #region フィールド
        
        /// <summary>受付明細表 メイン画面</summary>
        UIForm form;

        #region Dao

        /// <summary>業者マスタDao</summary>
        IM_GYOUSHADao dao_M_GYOUSHA;

        /// <summary>現場マスタDao</summary>
        IM_GENBADao dao_M_GENBA;

        /// <summary>車輌マスタDao</summary>
        IM_SHARYOUDao dao_M_SHARYOU;

        /// <summary>社員マスタDao</summary>
        IM_SHAINDao dao_M_SHAIN;

        /// <summary>自社情報マスタDao</summary>
        IM_CORP_INFODao dao_M_CORP_INFO;

        /// <summary>受入明細表用Dao</summary>
        DAOClass dao_UketsukeMeisaihyo;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        #endregion

        #endregion

        #region メソッド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form">受付明細表 メイン画面</param>
        public LogicClass(UIForm form)
        {
            LogUtility.DebugMethodStart();
            
            this.form = form;
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化

        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダ初期化
                this.HeaderInit();

                // ボタン初期化
                this.ButtonInit();

                // Dao初期化
                this.DaoInit();

                // イベント初期化
                this.EventInit();

                // 画面項目初期設定
                this.SetDisplayInit();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #region ヘッダ初期化

        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            // タイトル設定
            var parentForm = (BusinessBaseForm)this.form.Parent;
            HeaderBaseForm headerBaseForm = (HeaderBaseForm)parentForm.headerForm;
            headerBaseForm.lb_title.Text = WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン初期化

        /// <summary>
        /// ボタンを初期化します
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定を作成します
        /// </summary>
        /// <returns>ボタン設定</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            ButtonSetting[] ret;

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            ret = buttonSetting.LoadButtonSetting(thisAssembly, buttonInfoXmlPath);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion

        #region DAO初期化

        private void DaoInit()
        {
            this.dao_M_GYOUSHA = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.dao_M_GENBA = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.dao_M_SHARYOU = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
            this.dao_M_SHAIN = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.dao_M_CORP_INFO = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.dao_UketsukeMeisaihyo = DaoInitUtility.GetComponent<DAOClass>();
        }

        #endregion

        #region イベント初期化

        /// <summary>
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 表示ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

            // 表示ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面項目初期値設定

        /// <summary>
        /// 画面項目初期値設定
        /// </summary>
        private void SetDisplayInit()
        {
            // 日付(1:受付日)
            this.form.HIDUKE_TEXT.Text = "1";
            // 日付範囲(1：当日)
            this.form.HIDUKE_RANGE_TEXT.Text = "1";
            // 日付FromTo(実行年月日)
            this.form.HIDUKE_RANGE_FROM.Value = null;
            this.form.HIDUKE_RANGE_TO.Value = null;
            // 並び順(1:取引先CD順)
            this.form.ORDER.Text = "1";
            // 現場非活性
            this.SetGenbaEnabled(false);
            // 車輌非活性
            this.SetSharyouEnabled(false);

            // 拠点CD初期値 99:全社
            IM_KYOTENDao dao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            M_KYOTEN kyotenData = dao.GetDataByCd("99");
            this.form.KYOTEN_CD.Text = "99";
            this.form.KYOTEN_NAME.Text = kyotenData.KYOTEN_NAME_RYAKU;
        }

        #endregion

        #endregion

        #region DBAccess

        #region 業者マスタ

        /// <summary>
        /// 指定したキーで運搬業者に該当するデータを業者マスタから検索します
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        private M_GYOUSHA GetUnpanGyoushaData(string gyoushaCd)
        {
            M_GYOUSHA data = this.dao_M_GYOUSHA.GetDataByCd(gyoushaCd);

            if (data != null && data.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 現場マスタ

        /// <summary>
        /// 指定したキーで現場マスタを検索します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns></returns>
        private M_GENBA[] GetGenbaData(string gyoushaCd, string genbaCd)
        {
            M_GENBA entity = new M_GENBA();
            entity.GYOUSHA_CD = gyoushaCd;
            entity.GENBA_CD = genbaCd;
            entity.ISNOT_NEED_DELETE_FLG = true;

            return this.dao_M_GENBA.GetAllValidData(entity);
        }

        #endregion

        #region 車輌マスタ

        /// <summary>
        /// 指定したキーで車輌マスタを検索します
        /// </summary>
        /// <param name="unpanGyoushaCd">業者CD</param>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns></returns>
        private M_SHARYOU[] GetSharyouData(string gyoushaCd, string sharyouCd)
        {
            M_SHARYOU entity = new M_SHARYOU();
            entity.GYOUSHA_CD = gyoushaCd;
            entity.SHARYOU_CD = sharyouCd;
            entity.ISNOT_NEED_DELETE_FLG = true;

            return this.dao_M_SHARYOU.GetAllValidData(entity);
        }

        #endregion

        #region 社員マスタ

        private M_SHAIN GetUntenshaData(string shainCd)
        {
            M_SHAIN entity = new M_SHAIN();
            entity.SHAIN_CD = shainCd;
            entity.UNTEN_KBN = true;
            entity.ISNOT_NEED_DELETE_FLG = true;

            M_SHAIN[] datas = this.dao_M_SHAIN.GetAllValidData(entity);

            if (datas != null && datas.Length > 0)
            {
                return datas[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #endregion

        #region 必須チェック

        internal bool RegistCheck()
        {
            bool check = true;
            StringBuilder errMsg = new StringBuilder();
            bool isTextBoxControlError = false;
            bool isDenpyouShuruiError = false;
            bool isHaishaJoukyouError = false;

            // 拠点CD
            if (string.IsNullOrEmpty(this.form.KYOTEN_CD.Text))
            {
                this.form.KYOTEN_CD.IsInputErrorOccured = true;
                this.form.KYOTEN_CD.UpdateBackColor();
                errMsg.Append("拠点は必須項目です。入力してください。");
                check = false;
                isTextBoxControlError = true;
            }

            // 伝票種類
            if (!this.form.DENPYOU_SHURUI_SHUSHU.Checked && !this.form.DENPYOU_SHURUI_SHUKKA.Checked && !this.form.DENPYOU_SHURUI_MOCHIKOMI.Checked)
            {
                if (errMsg.Length > 0)  errMsg.Append("\r\n");
                errMsg.Append("伝票種類は何れか1つを選択してください。");
                check = false;
                isDenpyouShuruiError = true;
            }

            // 日付
            if (string.IsNullOrEmpty(this.form.HIDUKE_TEXT.Text))
            {
                this.form.HIDUKE_TEXT.IsInputErrorOccured = true;
                this.form.HIDUKE_TEXT.UpdateBackColor();
                if (errMsg.Length > 0) errMsg.Append("\r\n");
                errMsg.Append("日付は必須項目です。入力してください。");
                check = false;
                if(!isDenpyouShuruiError) isTextBoxControlError = true;
            }

            // 日付範囲
            if (string.IsNullOrEmpty(this.form.HIDUKE_RANGE_TEXT.Text))
            {
                this.form.HIDUKE_RANGE_TEXT.IsInputErrorOccured = true;
                this.form.HIDUKE_RANGE_TEXT.UpdateBackColor();
                if (errMsg.Length > 0) errMsg.Append("\r\n");
                errMsg.Append("日付範囲は必須項目です。入力してください。");
                check = false;
                if (!isDenpyouShuruiError) isTextBoxControlError = true;
            }

            // 日付FromTo ※日付範囲が「3」の場合
            if (this.form.HIDUKE_RANGE_TEXT.Text.Equals("3"))
            {
                if (string.IsNullOrEmpty(this.form.HIDUKE_RANGE_FROM.Text) || string.IsNullOrEmpty(this.form.HIDUKE_RANGE_TO.Text))
                {
                    if (string.IsNullOrEmpty(this.form.HIDUKE_RANGE_FROM.Text))
                    {
                        this.form.HIDUKE_RANGE_FROM.IsInputErrorOccured = true;
                        this.form.HIDUKE_RANGE_FROM.UpdateBackColor();
                        if (errMsg.Length > 0) errMsg.Append("\r\n");
                        errMsg.Append("日付範囲Fromは必須項目です。入力してください。");
                    }

                    if (string.IsNullOrEmpty(this.form.HIDUKE_RANGE_TO.Text))
                    {
                        this.form.HIDUKE_RANGE_TO.IsInputErrorOccured = true;
                        this.form.HIDUKE_RANGE_TO.UpdateBackColor();
                        if (errMsg.Length > 0) errMsg.Append("\r\n");
                        errMsg.Append("日付範囲Toは必須項目です。入力してください。");
                    }

                    check = false;
                    if (!isDenpyouShuruiError) isTextBoxControlError = true;
                }
                else
                {
                    DateTime from = (DateTime)this.form.HIDUKE_RANGE_FROM.Value;
                    DateTime to = (DateTime)this.form.HIDUKE_RANGE_TO.Value;
                    if (to.CompareTo(from) < 0)
                    {
                        this.form.HIDUKE_RANGE_FROM.IsInputErrorOccured = true;
                        this.form.HIDUKE_RANGE_FROM.UpdateBackColor();
                        this.form.HIDUKE_RANGE_TO.IsInputErrorOccured = true;
                        this.form.HIDUKE_RANGE_TO.UpdateBackColor();
                        if (errMsg.Length > 0) errMsg.Append("\r\n");
                        errMsg.Append("日付範囲Toが日付範囲Fromより前の日付になっています。\r\n");
                        errMsg.Append("日付範囲Toには日付範囲From以降の日付を指定してください。");
                        check = false;
                        if (!isDenpyouShuruiError) isTextBoxControlError = true;
                    }
                }
            }

            // 配車状況
            if (!this.form.HAISHA_JOUKYOU_JUCHU.Checked && !this.form.HAISHA_JOUKYOU_HAISHA.Checked &&
                !this.form.HAISHA_JOUKYOU_KEIJO.Checked && !this.form.HAISHA_JOUKYOU_CANCEL.Checked && !this.form.HAISHA_JOUKYOU_KAISHUNASHI.Checked)
            {
                if (errMsg.Length > 0) errMsg.Append("\r\n");
                errMsg.Append("配車状況は何れか1つを選択してください。");
                check = false;
                if (!isDenpyouShuruiError && !isTextBoxControlError) isHaishaJoukyouError = true;
            }

            // 取引先FromTo
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_FROM.Text) && !string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_TO.Text) &&
                this.form.TORIHIKISAKI_CD_TO.Text.CompareTo(this.form.TORIHIKISAKI_CD_FROM.Text) < 0)
            {
                this.form.TORIHIKISAKI_CD_FROM.IsInputErrorOccured = true;
                this.form.TORIHIKISAKI_CD_FROM.UpdateBackColor();
                this.form.TORIHIKISAKI_CD_TO.IsInputErrorOccured = true;
                this.form.TORIHIKISAKI_CD_TO.UpdateBackColor();
                if (errMsg.Length > 0) errMsg.Append("\r\n");
                errMsg.Append("取引先Toが取引先Fromより前の値になっています。\r\n");
                errMsg.Append("取引先Toには取引先From以降の値を指定してください。");
                check = false;
                if (!isDenpyouShuruiError && !isHaishaJoukyouError) isTextBoxControlError = true;
            }

            // 業者FromTo
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_FROM.Text) && !string.IsNullOrEmpty(this.form.GYOUSHA_CD_TO.Text) &&
                this.form.GYOUSHA_CD_TO.Text.CompareTo(this.form.GYOUSHA_CD_FROM.Text) < 0)
            {
                this.form.GYOUSHA_CD_FROM.IsInputErrorOccured = true;
                this.form.GYOUSHA_CD_FROM.UpdateBackColor();
                this.form.GYOUSHA_CD_TO.IsInputErrorOccured = true;
                this.form.GYOUSHA_CD_TO.UpdateBackColor();
                if (errMsg.Length > 0) errMsg.Append("\r\n");
                errMsg.Append("業者Toが業者Fromより前の値になっています。\r\n");
                errMsg.Append("業者Toには業者From以降の値を指定してください。");
                check = false;
                if (!isDenpyouShuruiError && !isHaishaJoukyouError) isTextBoxControlError = true;
            }

            // 現場FromTo
            if (this.form.GENBA_CD_FROM.Enabled && this.form.GENBA_CD_TO.Enabled &&
                !string.IsNullOrEmpty(this.form.GENBA_CD_FROM.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD_TO.Text) &&
                this.form.GENBA_CD_TO.Text.CompareTo(this.form.GENBA_CD_FROM.Text) < 0)
            {
                this.form.GENBA_CD_FROM.IsInputErrorOccured = true;
                this.form.GENBA_CD_FROM.UpdateBackColor();
                this.form.GENBA_CD_TO.IsInputErrorOccured = true;
                this.form.GENBA_CD_TO.UpdateBackColor();
                if (errMsg.Length > 0) errMsg.Append("\r\n");
                errMsg.Append("現場Toが現場Fromより前の値になっています。\r\n");
                errMsg.Append("現場Toには現場From以降の値を指定してください。");
                check = false;
                if (!isDenpyouShuruiError && !isHaishaJoukyouError) isTextBoxControlError = true;
            }

            // 運搬業者FromTo
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_FROM.Text) && !string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_TO.Text) &&
                this.form.UNPAN_GYOUSHA_CD_TO.Text.CompareTo(this.form.UNPAN_GYOUSHA_CD_FROM.Text) < 0)
            {
                this.form.UNPAN_GYOUSHA_CD_FROM.IsInputErrorOccured = true;
                this.form.UNPAN_GYOUSHA_CD_FROM.UpdateBackColor();
                this.form.UNPAN_GYOUSHA_CD_TO.IsInputErrorOccured = true;
                this.form.UNPAN_GYOUSHA_CD_TO.UpdateBackColor();
                if (errMsg.Length > 0) errMsg.Append("\r\n");
                errMsg.Append("運搬業者Toが運搬業者Fromより前の値になっています。\r\n");
                errMsg.Append("運搬業者Toには運搬業者From以降の値を指定してください。");
                check = false;
                if (!isDenpyouShuruiError && !isHaishaJoukyouError) isTextBoxControlError = true;
            }

            // 車種FromTo
            if (!string.IsNullOrEmpty(this.form.SHASHU_CD_FROM.Text) && !string.IsNullOrEmpty(this.form.SHASHU_CD_TO.Text) &&
                this.form.SHASHU_CD_TO.Text.CompareTo(this.form.SHASHU_CD_FROM.Text) < 0)
            {
                this.form.SHASHU_CD_FROM.IsInputErrorOccured = true;
                this.form.SHASHU_CD_FROM.UpdateBackColor();
                this.form.SHASHU_CD_TO.IsInputErrorOccured = true;
                this.form.SHASHU_CD_TO.UpdateBackColor();
                if (errMsg.Length > 0) errMsg.Append("\r\n");
                errMsg.Append("車種Toが車種Fromより前の値になっています。\r\n");
                errMsg.Append("車種Toには車種From以降の値を指定してください。");
                check = false;
                if (!isDenpyouShuruiError && !isHaishaJoukyouError) isTextBoxControlError = true;
            }

            // 車輌FromTo
            if (this.form.SHARYOU_CD_FROM.Enabled && this.form.SHARYOU_CD_TO.Enabled &&
                !string.IsNullOrEmpty(this.form.SHARYOU_CD_FROM.Text) && !string.IsNullOrEmpty(this.form.SHARYOU_CD_TO.Text) &&
                this.form.SHARYOU_CD_TO.Text.CompareTo(this.form.SHARYOU_CD_FROM.Text) < 0)
            {
                this.form.SHARYOU_CD_FROM.IsInputErrorOccured = true;
                this.form.SHARYOU_CD_FROM.UpdateBackColor();
                this.form.SHARYOU_CD_TO.IsInputErrorOccured = true;
                this.form.SHARYOU_CD_TO.UpdateBackColor();
                if (errMsg.Length > 0) errMsg.Append("\r\n");
                errMsg.Append("車輌Toが車輌Fromより前の値になっています。\r\n");
                errMsg.Append("車輌Toには車輌From以降の値を指定してください。");
                check = false;
                if (!isDenpyouShuruiError && !isHaishaJoukyouError) isTextBoxControlError = true;
            }

            // 運転者FromTo
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD_FROM.Text) && !string.IsNullOrEmpty(this.form.UNTENSHA_CD_TO.Text) &&
                this.form.UNTENSHA_CD_TO.Text.CompareTo(this.form.UNTENSHA_CD_FROM.Text) < 0)
            {
                this.form.UNTENSHA_CD_FROM.IsInputErrorOccured = true;
                this.form.UNTENSHA_CD_FROM.UpdateBackColor();
                this.form.UNTENSHA_CD_TO.IsInputErrorOccured = true;
                this.form.UNTENSHA_CD_TO.UpdateBackColor();
                if (errMsg.Length > 0) errMsg.Append("\r\n");
                errMsg.Append("運転者Toが運転者Fromより前の値になっています。\r\n");
                errMsg.Append("運転者Toには運転者From以降の値を指定してください。");
                check = false;
                if (!isDenpyouShuruiError && !isHaishaJoukyouError) isTextBoxControlError = true;
            }

            // 並び順
            if (string.IsNullOrEmpty(this.form.ORDER.Text))
            {
                this.form.ORDER.IsInputErrorOccured = true;
                this.form.ORDER.UpdateBackColor();
                if (errMsg.Length > 0) errMsg.Append("\r\n");
                errMsg.Append("並び順は必須項目です。入力してください。");
                check = false;
                if (!isDenpyouShuruiError && !isHaishaJoukyouError) isTextBoxControlError = true;
            }

            if (!check)
            {
                // エラーメッセージ
                new MessageBoxShowLogic().MessageBoxShowError(errMsg.ToString());

                this.form.HIDUKE_RANGE_FROM.CausesValidation = true;
                this.form.HIDUKE_RANGE_TO.CausesValidation = true;
                this.form.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.form.TORIHIKISAKI_CD_TO.CausesValidation = true;
                this.form.GYOUSHA_CD_FROM.CausesValidation = true;
                this.form.GYOUSHA_CD_TO.CausesValidation = true;
                this.form.GENBA_CD_FROM.CausesValidation = true;
                this.form.GENBA_CD_TO.CausesValidation = true;
                this.form.UNPAN_GYOUSHA_CD_FROM.CausesValidation = true;
                this.form.UNPAN_GYOUSHA_CD_TO.CausesValidation = true;
                this.form.SHASHU_CD_FROM.CausesValidation = true;
                this.form.SHASHU_CD_TO.CausesValidation = true;
                this.form.SHARYOU_CD_FROM.CausesValidation = true;
                this.form.SHARYOU_CD_TO.CausesValidation = true;
                this.form.UNTENSHA_CD_FROM.CausesValidation = true;
                this.form.UNTENSHA_CD_TO.CausesValidation = true;

                // エラーコントロールにフォーカスを当てる
                if (!isTextBoxControlError && isDenpyouShuruiError)
                {
                    // 伝票種類 - 収集
                    this.form.DENPYOU_SHURUI_SHUSHU.Focus();
                }
                else if (!isTextBoxControlError && isHaishaJoukyouError)
                {
                    // 配車状況 - 受注
                    this.form.HAISHA_JOUKYOU_JUCHU.Focus();
                }
                else
                {
                    // テキストボックス系をインデックスでソート
                    var focusControl = this.form.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                    if (null != focusControl)
                    {
                        ((Control)focusControl).Focus();
                    }
                }
            }

            return check;
        }

        #endregion

        #region [F5] CSVボタン ClickEvent

        /// <summary>
        /// [F5] CSVボタン ClickEvent
        /// </summary>
        internal bool CSVPrint()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
               
                // CSV出力DTO作成
                PrintDtoClass printDto = this.CreatePrintDto();

                /* データ存在チェック */
                if (printDto.DETAIL_DATA_TABLE == null || printDto.DETAIL_DATA_TABLE.Rows.Count == 0)
                {
                    // エラー
                    this.errmessage.MessageBoxShow("C001");
                    return ret;
                }

                string headStr = "";

                switch (Convert.ToInt16(printDto.ORDER))
                {
                    case 1:
                        headStr = "取引先CD,取引先,業者CD,業者,現場CD,現場,作業日,受付日,伝票種類,受付番号,車種CD,車種,車輌,運搬業者CD,運搬業者,運転者,品名CD,品名,伝票区分,数量,単位CD,単位,単価,金額";
                        break;
                    case 2:
                        headStr = "業者CD,業者,現場CD,現場,取引先CD,取引先,作業日,受付日,伝票種類,受付番号,車種CD,車種,車輌,運搬業者CD,運搬業者,運転者,品名CD,品名,伝票区分,数量,単位CD,単位,単価,金額";
                        break;
                    case 3:
                        headStr = "運搬業者CD,運搬業者,車輌,車種CD,車種,運転者,作業日,受付日,伝票種類,受付番号,取引先CD,取引先,業者CD,業者,現場CD,現場,品名CD,品名,伝票区分,数量,単位CD,単位,単価,金額";
                        break;
                    case 4:
                        headStr = "運転者,運搬業者CD,運搬業者,車輌,車種CD,車種,受付日,作業日,伝票種類,受付番号,取引先CD,取引先,業者CD,業者,現場CD,現場,品名CD,品名,伝票区分,数量,単位CD,単位,単価,金額";
                        break;
                    case 5:
                        headStr = "作業日,受付日,取引先CD,取引先,業者CD,業者,現場CD,現場,伝票種類,受付番号,車種CD,車種,車輌,運搬業者CD,運搬業者,運転者,品名CD,品名,伝票区分,数量,単位CD,単位,単価,金額";
                        break;
                    case 6:
                        headStr = "受付番号,伝票種類,取引先CD,取引先,業者CD,業者,現場CD,現場,受付日,作業日,車種CD,車種,車輌,運搬業者CD,運搬業者,運転者,品名CD,品名,伝票区分,数量,単位CD,単位,単価,金額";
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
                for (int i = 0; i < printDto.DETAIL_DATA_TABLE.Rows.Count; i++)
                {
                    rowTmp = csvDT.NewRow();

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["TORIHIKISAKI_CD"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["TORIHIKISAKI_CD"].ToString()))
                    {
                        rowTmp["取引先CD"] = printDto.DETAIL_DATA_TABLE.Rows[i]["TORIHIKISAKI_CD"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["TORIHIKISAKI_NAME_RYAKU"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString()))
                    {
                        rowTmp["取引先"] = printDto.DETAIL_DATA_TABLE.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["GYOUSHA_CD"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["GYOUSHA_CD"].ToString()))
                    {
                        rowTmp["業者CD"] = printDto.DETAIL_DATA_TABLE.Rows[i]["GYOUSHA_CD"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["GYOUSHA_NAME_RYAKU"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString()))
                    {
                        rowTmp["業者"] = printDto.DETAIL_DATA_TABLE.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["GENBA_CD"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["GENBA_CD"].ToString()))
                    {
                        rowTmp["現場CD"] = printDto.DETAIL_DATA_TABLE.Rows[i]["GENBA_CD"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["GENBA_NAME_RYAKU"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["GENBA_NAME_RYAKU"].ToString()))
                    {
                        rowTmp["現場"] = printDto.DETAIL_DATA_TABLE.Rows[i]["GENBA_NAME_RYAKU"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["SAGYOU_DATE"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["SAGYOU_DATE"].ToString()))
                    {
                        rowTmp["作業日"] = Convert.ToDateTime(printDto.DETAIL_DATA_TABLE.Rows[i]["SAGYOU_DATE"]).ToString("yyyy/MM/dd");
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["UKETSUKE_DATE"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["UKETSUKE_DATE"].ToString()))
                    {
                        rowTmp["受付日"] = Convert.ToDateTime(printDto.DETAIL_DATA_TABLE.Rows[i]["UKETSUKE_DATE"]).ToString("yyyy/MM/dd");
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["DENPYOU_SHURUI"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["DENPYOU_SHURUI"].ToString()))
                    {
                        rowTmp["伝票種類"] = printDto.DETAIL_DATA_TABLE.Rows[i]["DENPYOU_SHURUI"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["UKETSUKE_NUMBER"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["UKETSUKE_NUMBER"].ToString()))
                    {
                        rowTmp["受付番号"] = printDto.DETAIL_DATA_TABLE.Rows[i]["UKETSUKE_NUMBER"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["SHASHU_CD"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["SHASHU_CD"].ToString()))
                    {
                        rowTmp["車種CD"] = printDto.DETAIL_DATA_TABLE.Rows[i]["SHASHU_CD"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["SHASHU_NAME"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["SHASHU_NAME"].ToString()))
                    {
                        rowTmp["車種"] = printDto.DETAIL_DATA_TABLE.Rows[i]["SHASHU_NAME"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["SHARYOU_NAME"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["SHARYOU_NAME"].ToString()))
                    {
                        rowTmp["車輌"] = printDto.DETAIL_DATA_TABLE.Rows[i]["SHARYOU_NAME"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["UNPAN_GYOUSHA_CD"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["UNPAN_GYOUSHA_CD"].ToString()))
                    {
                        rowTmp["運搬業者CD"] = printDto.DETAIL_DATA_TABLE.Rows[i]["UNPAN_GYOUSHA_CD"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["UNPAN_GYOUSHA_NAME"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["UNPAN_GYOUSHA_NAME"].ToString()))
                    {
                        rowTmp["運搬業者"] = printDto.DETAIL_DATA_TABLE.Rows[i]["UNPAN_GYOUSHA_NAME"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["UNTENSHA_NAME"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["UNTENSHA_NAME"].ToString()))
                    {
                        rowTmp["運転者"] = printDto.DETAIL_DATA_TABLE.Rows[i]["UNTENSHA_NAME"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["HINMEI_CD"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["HINMEI_CD"].ToString()))
                    {
                        rowTmp["品名CD"] = printDto.DETAIL_DATA_TABLE.Rows[i]["HINMEI_CD"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["HINMEI_NAME"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["HINMEI_NAME"].ToString()))
                    {
                        rowTmp["品名"] = printDto.DETAIL_DATA_TABLE.Rows[i]["HINMEI_NAME"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["DENPYOU_KBN"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["DENPYOU_KBN"].ToString()))
                    {
                        rowTmp["伝票区分"] = printDto.DETAIL_DATA_TABLE.Rows[i]["DENPYOU_KBN"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["TMP_SUURYOU"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["TMP_SUURYOU"].ToString()))
                    {
                        rowTmp["数量"] = Convert.ToDecimal(printDto.DETAIL_DATA_TABLE.Rows[i]["TMP_SUURYOU"]).ToString(r_framework.Dto.SystemProperty.Format.Suuryou);
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["UNIT_CD"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["UNIT_CD"].ToString()))
                    {
                        rowTmp["単位CD"] = printDto.DETAIL_DATA_TABLE.Rows[i]["UNIT_CD"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["UNIT_NAME"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["UNIT_NAME"].ToString()))
                    {
                        rowTmp["単位"] = printDto.DETAIL_DATA_TABLE.Rows[i]["UNIT_NAME"].ToString();
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["TMP_TANKA"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["TMP_TANKA"].ToString()))
                    {
                        rowTmp["単価"] = Convert.ToDecimal(printDto.DETAIL_DATA_TABLE.Rows[i]["TMP_TANKA"]).ToString(r_framework.Dto.SystemProperty.Format.Tanka);
                    }

                    if (printDto.DETAIL_DATA_TABLE.Rows[i]["KINGAKU"] != null && !string.IsNullOrEmpty(printDto.DETAIL_DATA_TABLE.Rows[i]["KINGAKU"].ToString()))
                    {
                        rowTmp["金額"] = printDto.DETAIL_DATA_TABLE.Rows[i]["KINGAKU"].ToString();
                    }

                    csvDT.Rows.Add(rowTmp);
                }

                // 一覧に明細行がない場合、アラートを表示し、CSV出力処理はしない
                if (csvDT.Rows.Count == 0)
                {
                    this.errmessage.MessageBoxShow("E044");
                    ret = false;
                }
                // 出力先指定のポップアップを表示させる。
                if (this.errmessage.MessageBoxShow("C013") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertDataTableToCsv(csvDT, true, true, "受付明細表", this.form);

                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonFunc5_Clicked", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region [F7] 表示ボタン ClickEvent

        /// <summary>
        /// [F7] 表示ボタン ClickEvent
        /// </summary>
        internal bool ButtonFunc7_Clicked()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                /*** 必須チェックはフォーム側で行っている ***/

                // 帳票出力DTO作成
                PrintDtoClass printDto = this.CreatePrintDto();

                /* データ存在チェック */
                if (printDto.DETAIL_DATA_TABLE == null || printDto.DETAIL_DATA_TABLE.Rows.Count == 0)
                {
                    // エラー
                    new MessageBoxShowLogic().MessageBoxShow("C001");
                    return ret;
                }

                // 出力データ作成
                ReportInfoR659 report = new ReportInfoR659(printDto);
                report.ReportID = "R659";
                report.Title = "受付明細表";
                report.R659_Reprt();

                // XPS出力(初期アクション：プレビュー)
                FormReport formReport = new FormReport(report, "R659", this.form.WindowId);
                formReport.PrintInitAction = 2;
                formReport.PrintXPS();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonFunc7_Clicked", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 帳票出力用DTO作成

        /// <summary>
        /// 帳票とCSV出力用DTO作成
        /// </summary>
        /// <returns>帳票出力用DTO</returns>
        private PrintDtoClass CreatePrintDto()
        {
            LogUtility.DebugMethodStart();

            PrintDtoClass printDto = new PrintDtoClass();

            /* 会社名 */
            // 基本1件だが念のため条件指定
            M_CORP_INFO corpInfo = new M_CORP_INFO();
            corpInfo.SYS_ID = 0;
            corpInfo.ISNOT_NEED_DELETE_FLG = true;
            M_CORP_INFO[] corpDatas = this.dao_M_CORP_INFO.GetAllValidData(corpInfo);
            printDto.CORP_NAME_RYAKU = corpDatas[0].CORP_RYAKU_NAME;

            /* 拠点名 */
            printDto.KYOTEN_NAME_RYAKU = this.form.KYOTEN_NAME.Text;

            /* 発行年月日 */
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //printDto.PRINT_DATE = DateTime.Now.ToString("yyyy/MM/dd hh:mm") + "発行";
            var parentForm = (BusinessBaseForm)this.form.Parent;
            printDto.PRINT_DATE = parentForm.sysDate.ToString("yyyy/MM/dd HH:mm") + "発行";
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            /* 抽出条件 伝票種類 */
            bool isShushu = this.form.DENPYOU_SHURUI_SHUSHU.Checked;
            bool isShukka = this.form.DENPYOU_SHURUI_SHUKKA.Checked;
            bool isMochikomi = this.form.DENPYOU_SHURUI_MOCHIKOMI.Checked;
            if (isShushu && isShukka && isMochikomi)
            {
                printDto.SEARCH_DENPYOU_KBN = "全て";
            }
            else
            {
                string tmp = string.Empty;
                tmp += isShushu ? "、収集" : "";
                tmp += isShukka ? "、出荷" : "";
                tmp += isMochikomi ? "、持込" : "";
                printDto.SEARCH_DENPYOU_KBN = (tmp.Length > 0) ? tmp.Remove(0, 1) : string.Empty;
            }
            
            /* 抽出条件 日付 */
            switch (this.form.HIDUKE_TEXT.Text)
            {
                case "1":
                    printDto.SEARCH_DATE_LABEL = "[受付日]";
                    break;
                case "2":
                    printDto.SEARCH_DATE_LABEL = "[作業日]";
                    break;
                case "3":
                    printDto.SEARCH_DATE_LABEL = "[入力日付]";
                    break;
            }

            /* 抽出条件 日付範囲*/
            DateTime date = parentForm.sysDate.Date;
            switch (this.form.HIDUKE_RANGE_TEXT.Text)
            {
                case "1":
                    // 当日
                    printDto.SEARCH_DATE = date.ToString("yyyy/MM/dd") + " ～ " + date.ToString("yyyy/MM/dd");
                    break;
                case "2":
                    // 当月
                    printDto.SEARCH_DATE = (new DateTime(date.Year, date.Month, 1)).ToString("yyyy/MM/dd") + " ～ "
                                                + (new DateTime(date.Year, date.Month, 1)).AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd");
                    break;
                case "3":
                    // 期間指定
                    if (this.form.HIDUKE_RANGE_FROM.Value != null) printDto.SEARCH_DATE = ((DateTime)this.form.HIDUKE_RANGE_FROM.Value).ToString("yyyy/MM/dd");
                    if (this.form.HIDUKE_RANGE_FROM.Value != null || this.form.HIDUKE_RANGE_TO.Value != null) printDto.SEARCH_DATE += " ～ ";
                    if (this.form.HIDUKE_RANGE_TO.Value != null) printDto.SEARCH_DATE += ((DateTime)this.form.HIDUKE_RANGE_TO.Value).ToString("yyyy/MM/dd");
                    break;
            }

            /* 配車状況 */
            bool isJuchu = this.form.HAISHA_JOUKYOU_JUCHU.Checked;
            bool isHaisha = this.form.HAISHA_JOUKYOU_HAISHA.Checked;
            bool isKeijo = this.form.HAISHA_JOUKYOU_KEIJO.Checked;
            bool isCancel = this.form.HAISHA_JOUKYOU_CANCEL.Checked;
            bool isKaishunashi = this.form.HAISHA_JOUKYOU_KAISHUNASHI.Checked;
            if (isJuchu && isHaisha && isKeijo && isCancel && isKaishunashi)
            {
                printDto.SEARCH_HAISHA_JOUKYOU = "全て";
            }
            else
            {
                string tmp = string.Empty;
                tmp += isJuchu ? "、受注" : "";
                tmp += isHaisha ? "、配車" : "";
                tmp += isKeijo ? "、計上" : "";
                tmp += isCancel ? "、キャンセル" : "";
                tmp += isKaishunashi ? "、回収なし" : "";
                printDto.SEARCH_HAISHA_JOUKYOU = (tmp.Length > 0) ? tmp.Remove(0, 1) : string.Empty;
            }

            /* 取引先 */
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_FROM.Text))
            {
                printDto.SEARCH_TORIHIKISAKI = this.form.TORIHIKISAKI_CD_FROM.Text + " " + this.form.TORIHIKISAKI_NAME_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_FROM.Text) || !string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_TO.Text))
            {
                printDto.SEARCH_TORIHIKISAKI += " ～ ";
            }
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_TO.Text))
            {
                printDto.SEARCH_TORIHIKISAKI += this.form.TORIHIKISAKI_CD_TO.Text + " " + this.form.TORIHIKISAKI_NAME_TO.Text;
            }

            /* 業者 */
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_FROM.Text))
            {
                printDto.SEARCH_GYOUSHA = this.form.GYOUSHA_CD_FROM.Text + " " + this.form.GYOUSHA_NAME_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_FROM.Text) || !string.IsNullOrEmpty(this.form.GYOUSHA_CD_TO.Text))
            {
                printDto.SEARCH_GYOUSHA += " ～ ";
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_TO.Text))
            {
                printDto.SEARCH_GYOUSHA += this.form.GYOUSHA_CD_TO.Text + " " + this.form.GYOUSHA_NAME_TO.Text;
            }

            /* 現場 */
            if (!string.IsNullOrEmpty(this.form.GENBA_CD_FROM.Text) && this.form.GENBA_CD_FROM.Enabled)
            {
                printDto.SEARCH_GENBA = this.form.GENBA_CD_FROM.Text + " " + this.form.GENBA_NAME_FROM.Text;
            }
            if ((!string.IsNullOrEmpty(this.form.GENBA_CD_FROM.Text) && this.form.GENBA_CD_FROM.Enabled) ||
                (!string.IsNullOrEmpty(this.form.GENBA_CD_TO.Text) && this.form.GENBA_CD_TO.Enabled))
            {
                printDto.SEARCH_GENBA += " ～ ";
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_CD_TO.Text) && this.form.GENBA_CD_TO.Enabled)
            {
                printDto.SEARCH_GENBA += this.form.GENBA_CD_TO.Text + " " + this.form.GENBA_NAME_TO.Text;
            }

            /* 運搬業者 */
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_FROM.Text))
            {
                printDto.SEARCH_UNPAN_GYOUSHA = this.form.UNPAN_GYOUSHA_CD_FROM.Text + " " + this.form.UNPAN_GYOUSHA_NAME_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_FROM.Text) || !string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_TO.Text))
            {
                printDto.SEARCH_UNPAN_GYOUSHA += " ～ ";
            }
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_TO.Text))
            {
                printDto.SEARCH_UNPAN_GYOUSHA += this.form.UNPAN_GYOUSHA_CD_TO.Text + " " + this.form.UNPAN_GYOUSHA_NAME_TO.Text;
            }

            /* 車種 */
            if (!string.IsNullOrEmpty(this.form.SHASHU_CD_FROM.Text))
            {
                printDto.SEARCH_SHASHU = this.form.SHASHU_CD_FROM.Text + " " + this.form.SHASHU_NAME_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHASHU_CD_FROM.Text) || !string.IsNullOrEmpty(this.form.SHASHU_CD_TO.Text))
            {
                printDto.SEARCH_SHASHU += " ～ ";
            }
            if (!string.IsNullOrEmpty(this.form.SHASHU_CD_TO.Text))
            {
                printDto.SEARCH_SHASHU += this.form.SHASHU_CD_TO.Text + " " + this.form.SHASHU_NAME_TO.Text;
            }

            /* 車輌 */
            if (!string.IsNullOrEmpty(this.form.SHARYOU_CD_FROM.Text) && this.form.SHARYOU_CD_FROM.Enabled)
            {
                printDto.SEARCH_SHARYO = this.form.SHARYOU_CD_FROM.Text + " " + this.form.SHARYOU_NAME_FROM.Text;
            }
            if ((!string.IsNullOrEmpty(this.form.SHARYOU_CD_FROM.Text) && this.form.SHARYOU_CD_FROM.Enabled) ||
                (!string.IsNullOrEmpty(this.form.SHARYOU_CD_TO.Text) && this.form.SHARYOU_CD_TO.Enabled))
            {
                printDto.SEARCH_SHARYO += " ～ ";
            }
            if (!string.IsNullOrEmpty(this.form.SHARYOU_CD_TO.Text) && this.form.SHARYOU_CD_TO.Enabled)
            {
                printDto.SEARCH_SHARYO += this.form.SHARYOU_CD_TO.Text + " " + this.form.SHARYOU_NAME_TO.Text;
            }

            /* 運転者 */
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD_FROM.Text))
            {
                printDto.SEARCH_UNTENSHA = this.form.UNTENSHA_CD_FROM.Text + " " + this.form.UNTENSHA_NAME_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD_FROM.Text) || !string.IsNullOrEmpty(this.form.UNTENSHA_CD_TO.Text))
            {
                printDto.SEARCH_UNTENSHA += " ～ ";
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD_TO.Text))
            {
                printDto.SEARCH_UNTENSHA += this.form.UNTENSHA_CD_TO.Text + " " + this.form.UNTENSHA_NAME_TO.Text;
            }

            /* Detail出力用DataTable */
            printDto.DETAIL_DATA_TABLE = this.GetPrintDetailDataTable();

            /* 売上金額合計、支払金額合計の算出 */
            if (printDto.DETAIL_DATA_TABLE != null && printDto.DETAIL_DATA_TABLE.Rows.Count > 0)
            {
                // 売上
                printDto.URIAGE_TOTAL_KINGAKU = this.CreateTotalKingaku(printDto.DETAIL_DATA_TABLE, 1).ToString("#,#");
                // 支払
                printDto.SHIHARAI_TOTAL_KINGAKU = this.CreateTotalKingaku(printDto.DETAIL_DATA_TABLE, 2).ToString("#,#");
            }

            /* 並び順 */
            printDto.ORDER = this.form.ORDER.Text;

            LogUtility.DebugMethodEnd(printDto);
            return printDto;
        }

        #endregion

        #region 受付明細表 Detail部用DataTable作成

        /// <summary>
        /// 受付明細表 Detail部用データを取得します
        /// </summary>
        /// <returns></returns>
        private DataTable GetPrintDetailDataTable()
        {
            // データ取得
            SearchDtoClass dto = this.CreateSearchDto();
            DataTable dt = this.dao_UketsukeMeisaihyo.GetPrintData(dto);

            // 表示用項目の追加
            dt.Columns.Add("SUURYOU");  // 数量
            dt.Columns.Add("TANKA");    // 単価

            foreach (DataRow row in dt.Rows)
            {
                // 数量の書式設定
                decimal suryou = 0;
                if (decimal.TryParse(row["TMP_SUURYOU"].ToString(), out suryou))
                {
                    row["SUURYOU"] = Convert.ToDecimal(suryou).ToString(r_framework.Dto.SystemProperty.Format.Suuryou);
                }

                // 単価の書式設定
                decimal tanka = 0;
                if (decimal.TryParse(row["TMP_TANKA"].ToString(), out tanka))
                {
                    row["TANKA"] = Convert.ToDecimal(tanka).ToString(r_framework.Dto.SystemProperty.Format.Tanka);
                }
            }

            return dt;
        }

        #endregion

        #region 帳票出力用「総合計」算出
        /// <summary>
        /// 帳票出力Detail部用DataTableを元に帳票出力用売上または支払の「合計金額」を算出します
        /// </summary>
        /// <param name="dt">帳票出力Detail部用DataTable</param>
        /// <param name="kbn">伝票区分 … 1:売上合計、2:支払合計</param>
        /// /// <returns>総合計</returns>
        private decimal CreateTotalKingaku(DataTable dt, int kbn)
        {
            decimal val = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (string.IsNullOrEmpty(row["DENPYOU_KBN"].ToString())) continue;
                if (kbn == 2 && row["DENPYOU_KBN"].ToString().Equals("売上")) continue;
                if (kbn == 1 && row["DENPYOU_KBN"].ToString().Equals("支払")) continue;

                decimal tmp = 0;
                if (decimal.TryParse(row["KINGAKU"].ToString(), out tmp))
                {
                    val += tmp;
                }
            }

            return val;
        }
        #endregion

        #region 受付明細表 Detail部データ検索用のDTO作成

        /// <summary>
        /// 受付明細表 Detail部データ検索用のDTOを作成します
        /// </summary>
        /// <returns></returns>
        private SearchDtoClass CreateSearchDto()
        {
            LogUtility.DebugMethodStart();

            SearchDtoClass dto = new SearchDtoClass();

            /*伝票種類 収集*/
            dto.DENPYOU_SHURUI_SHUSHU = this.form.DENPYOU_SHURUI_SHUSHU.Checked;

            /*伝票種類 出荷*/
            dto.DENPYOU_SHURUI_SHUKKA = this.form.DENPYOU_SHURUI_SHUKKA.Checked;

            /*伝票種類 持込*/
            dto.DENPYOU_SHURUI_MOCHIKOMI = this.form.DENPYOU_SHURUI_MOCHIKOMI.Checked;

            /*拠点CD*/
            dto.KYOTEN_CD = int.Parse(this.form.KYOTEN_CD.Text);

            /*日付(1.受付日 2.作業日 3.入力日付)*/
            dto.HIDUKE = int.Parse(this.form.HIDUKE_TEXT.Text);

            /* 日付範囲 */
            var parentForm = (BusinessBaseForm)this.form.Parent;
            DateTime date = parentForm.sysDate.Date;
            switch (this.form.HIDUKE_RANGE_TEXT.Text)
            {
                case "1":
                    // 当日
                    dto.HIDUKE_RANGE_FROM = date.ToString("yyyy/MM/dd");
                    dto.HIDUKE_RANGE_TO = date.AddDays(1).ToString("yyyy/MM/dd");
                    break;
                case "2":
                    // 当月
                    dto.HIDUKE_RANGE_FROM = (new DateTime(date.Year, date.Month, 1)).ToString("yyyy/MM/dd");
                    dto.HIDUKE_RANGE_TO = (new DateTime(date.Year, date.Month, 1)).AddMonths(1).ToString("yyyy/MM/dd");
                    break;
                case "3":
                    // 期間指定
                    dto.HIDUKE_RANGE_FROM = ((DateTime)this.form.HIDUKE_RANGE_FROM.Value).ToString("yyyy/MM/dd");
                    dto.HIDUKE_RANGE_TO = ((DateTime)this.form.HIDUKE_RANGE_TO.Value).AddDays(1).ToString("yyyy/MM/dd");
                    break;
            }

            /* 配車状況 */
            dto.HAISHA_JOUKYOU_JUCHU = this.form.HAISHA_JOUKYOU_JUCHU.Checked;
            dto.HAISHA_JOUKYOU_HAISHA = this.form.HAISHA_JOUKYOU_HAISHA.Checked;
            dto.HAISHA_JOUKYOU_KEIJO = this.form.HAISHA_JOUKYOU_KEIJO.Checked;
            dto.HAISHA_JOUKYOU_CANCEL = this.form.HAISHA_JOUKYOU_CANCEL.Checked;
            dto.HAISHA_JOUKYOU_KAISHUNASHI = this.form.HAISHA_JOUKYOU_KAISHUNASHI.Checked;

            /*取引先CDFrom*/
            dto.TORIHIKISAKI_CD_FROM = this.form.TORIHIKISAKI_CD_FROM.Text;

            /*取引先CDTo*/
            dto.TORIHIKISAKI_CD_TO = this.form.TORIHIKISAKI_CD_TO.Text;

            /*業者CDFrom*/
            dto.GYOUSHA_CD_FROM = this.form.GYOUSHA_CD_FROM.Text;

            /*業者CDTo*/
            dto.GYOUSHA_CD_TO = this.form.GYOUSHA_CD_TO.Text;

            /*現場CDFrom*/
            dto.GENBA_CD_FROM = this.form.GENBA_CD_FROM.Text;

            /*現場CDTo*/
            dto.GENBA_CD_TO = this.form.GENBA_CD_TO.Text;

            /*運搬業者CDFrom*/
            dto.UNPAN_GYOUSHA_CD_FROM = this.form.UNPAN_GYOUSHA_CD_FROM.Text;

            /*運搬業者CDTo*/
            dto.UNPAN_GYOUSHA_CD_TO = this.form.UNPAN_GYOUSHA_CD_TO.Text;

            /*車種CDFrom*/
            dto.SHASHU_CD_FROM = this.form.SHASHU_CD_FROM.Text;

            /*車種CDTo*/
            dto.SHASHU_CD_TO = this.form.SHASHU_CD_TO.Text;

            /*車輌CDFrom*/
            dto.SHARYOU_CD_FROM = this.form.SHARYOU_CD_FROM.Text;

            /*車輌CDTo*/
            dto.SHARYOU_CD_TO = this.form.SHARYOU_CD_TO.Text;

            /*運転者CDFrom*/
            dto.UNTENSHA_CD_FROM = this.form.UNTENSHA_CD_FROM.Text;

            /*運転者CDTo*/
            dto.UNTENSHA_CD_TO = this.form.UNTENSHA_CD_TO.Text;

            /*OrderBy句(並び順)*/
            dto.ORDER = int.Parse(this.form.ORDER.Text);

            LogUtility.DebugMethodEnd(dto);
            return dto;
        }

        #endregion

        #region 業者CD入力済＆同値チェック

        /// <summary>
        /// 業者CDFrom、業者CDToが空でないか、同値であるかをチェックします
        /// </summary>
        /// <returns>True : 業者CDFrom、業者CDToが空でないかつ同値</returns>
        internal bool CheckGgyoushaIsNotNullAndSameValue()
        {
            LogUtility.DebugMethodStart();

            string gyoushaCdFrom = this.form.GYOUSHA_CD_FROM.Text;
            string gyoushaCdTo = this.form.GYOUSHA_CD_TO.Text;
            LogUtility.Debug("GYOUSHA_CD_FROM : " + gyoushaCdFrom + " , GYOUSHA_CD_TO : " + gyoushaCdTo);

            // 大文字変換＆0埋め
            string gyoushaCdFrom2 = gyoushaCdFrom.ToUpper().PadLeft(6, '0');
            string gyoushaCdTo2 = gyoushaCdTo.ToUpper().PadLeft(6, '0');
            LogUtility.Debug("GYOUSHA_CD_FROM : " + gyoushaCdFrom2 + " , GYOUSHA_CD_TO : " + gyoushaCdTo2);

            bool val = !string.IsNullOrEmpty(gyoushaCdFrom) && !string.IsNullOrEmpty(gyoushaCdTo) && gyoushaCdFrom2.Equals(gyoushaCdTo2);
            LogUtility.DebugMethodEnd(val);

            return val;
        }

        #endregion

        #region 現場CDFrom、現場CDToのEnabled設定

        /// <summary>
        /// 現場CDFrom、現場CDToのEnabledを引数の値で設定します
        /// </summary>
        /// <param name="enabled"></param>
        internal void SetGenbaEnabled(bool enabled)
        {
            LogUtility.DebugMethodStart(enabled);

            this.form.GENBA_CD_FROM.Enabled = enabled;
            this.form.GENBA_NAME_FROM.Enabled = enabled;
            this.form.GENBA_FROM_BUTTON.Enabled = enabled;

            this.form.GENBA_CD_TO.Enabled = enabled;
            this.form.GENBA_NAME_TO.Enabled = enabled;
            this.form.GENBA_TO_BUTTON.Enabled = enabled;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 現場CDFrom、現場CDTo ValidatingEvent : 存在チェック＆名称設定

        /// <summary>
        /// 現場CDFrom、現場CDTo ValidatingEvent
        /// 存在チェック＆名称設定を行います
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // CDコントロール
                var cdControl = new r_framework.CustomControl.CustomTextBox();
                // NAMEコントロール
                var nameControl = new r_framework.CustomControl.CustomTextBox();

                // From、Toどちらのコントロールか判定
                var control = (r_framework.CustomControl.CustomTextBox)sender;
                if (control.Name.Equals(this.form.GENBA_CD_FROM.Name) && this.form.GENBA_CD_FROM.Enabled)
                {
                    cdControl = this.form.GENBA_CD_FROM;
                    nameControl = this.form.GENBA_NAME_FROM;
                }
                else if (control.Name.Equals(this.form.GENBA_CD_TO.Name) && this.form.GENBA_CD_TO.Enabled)
                {
                    cdControl = this.form.GENBA_CD_TO;
                    nameControl = this.form.GENBA_NAME_TO;
                }
                else
                {
                    return ret;
                }

                // 現場CD
                string genbaCd = cdControl.Text;

                // Nullチェック
                if (string.IsNullOrEmpty(genbaCd))
                {
                    // 名称クリア
                    nameControl.Text = string.Empty;
                    return ret;
                }

                // 業者CD
                // 活性化されるのは業者CDが一意の場合のみのためFrom/Toどちらの値でも可
                string gyoushaCd = this.form.GYOUSHA_CD_FROM.Text;

                // 現場検索
                M_GENBA[] genbaDatas = this.GetGenbaData(gyoushaCd, genbaCd);

                // 存在チェック
                if (genbaDatas == null || genbaDatas.Length == 0)
                {
                    // エラー
                    new MessageBoxShowLogic().MessageBoxShow("E020", "現場");
                    nameControl.Text = string.Empty;
                    cdControl.SelectAll();
                    e.Cancel = true;
                    return ret;
                }

                // 名称設定
                nameControl.Text = genbaDatas[0].GENBA_NAME_RYAKU;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GENBA_CD_Validating", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_CD_Validating", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 運搬業者CD入力済＆同値チェック

        /// <summary>
        /// 運搬業者CDFrom、運搬業者CDToが空でないか、同値であるかをチェックします
        /// </summary>
        /// <returns>True : 運搬業者CDFrom、業者CDToが空でないかつ同値</returns>
        internal bool CheckUnpanGyoushaIsNotNullAndSameValue()
        {
            LogUtility.DebugMethodStart();

            string unpanGyoushaCdFrom = this.form.UNPAN_GYOUSHA_CD_FROM.Text;
            string unpanGyoushaCdTo = this.form.UNPAN_GYOUSHA_CD_TO.Text;
            LogUtility.Debug("UNPAN_GYOUSHA_CD_FROM : " + unpanGyoushaCdFrom + " , UNPAN_GYOUSHA_CD_TO : " + unpanGyoushaCdTo);

            // 大文字変換＆0埋め
            string unpanGyoushaCdFrom2 = unpanGyoushaCdFrom.ToUpper().PadLeft(6, '0');
            string unpanGyoushaCdTo2 = unpanGyoushaCdTo.ToUpper().PadLeft(6, '0');
            LogUtility.Debug("UNPAN_GYOUSHA_CD_FROM : " + unpanGyoushaCdFrom2 + " , UNPAN_GYOUSHA_CD_TO : " + unpanGyoushaCdTo2);

            bool val = !string.IsNullOrEmpty(unpanGyoushaCdFrom) && !string.IsNullOrEmpty(unpanGyoushaCdTo) && unpanGyoushaCdFrom2.Equals(unpanGyoushaCdTo2);
            LogUtility.DebugMethodEnd(val);

            return val;
        }

        #endregion

        #region 運搬業者CDFrom、運搬業者CDTo ValidatingEvent : 存在チェック＆名称設定

        /// <summary>
        /// 運搬業者CDFrom、運搬業者CDTo ValidatingEvent
        /// 存在チェック＆名称設定を行います
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool UNPAN_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // CDコントロール
                var cdControl = new r_framework.CustomControl.CustomTextBox();
                // NAMEコントロール
                var nameControl = new r_framework.CustomControl.CustomTextBox();

                // From、Toどちらのコントロールか判定
                var control = (r_framework.CustomControl.CustomTextBox)sender;
                if (control.Name.Equals(this.form.UNPAN_GYOUSHA_CD_FROM.Name))
                {
                    cdControl = this.form.UNPAN_GYOUSHA_CD_FROM;
                    nameControl = this.form.UNPAN_GYOUSHA_NAME_FROM;
                }
                else if (control.Name.Equals(this.form.UNPAN_GYOUSHA_CD_TO.Name))
                {
                    cdControl = this.form.UNPAN_GYOUSHA_CD_TO;
                    nameControl = this.form.UNPAN_GYOUSHA_NAME_TO;
                }
                else
                {
                    return ret;
                }

                // 運搬業者CD
                string unpanGyoushaCd = cdControl.Text;

                // Nullチェック
                if (string.IsNullOrEmpty(unpanGyoushaCd))
                {
                    // 名称クリア
                    nameControl.Text = string.Empty;
                    return ret;
                }

                // 業者検索
                M_GYOUSHA gyoushaDatas = this.GetUnpanGyoushaData(unpanGyoushaCd);

                // 存在チェック
                if (gyoushaDatas == null)
                {
                    // エラー
                    new MessageBoxShowLogic().MessageBoxShow("E020", "業者");
                    nameControl.Text = string.Empty;
                    cdControl.SelectAll();
                    e.Cancel = true;
                    return ret;
                }

                // 名称設定
                nameControl.Text = gyoushaDatas.GYOUSHA_NAME_RYAKU;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CD_Validating", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CD_Validating", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 車輌CDFrom、車輌CDToのEnabled設定

        internal void SetSharyouEnabled(bool enabled)
        {
            LogUtility.DebugMethodStart(enabled);

            this.form.SHARYOU_CD_FROM.Enabled = enabled;
            this.form.SHARYOU_NAME_FROM.Enabled = enabled;
            this.form.SHARYOU_FROM_BUTTON.Enabled = enabled;

            this.form.SHARYOU_CD_TO.Enabled = enabled;
            this.form.SHARYOU_NAME_TO.Enabled = enabled;
            this.form.SHARYOU_TO_BUTTON.Enabled = enabled;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 車輌CDFrom、車輌CDTo ValidatingEvent : 存在チェック＆名称設定

        /// <summary>
        /// 車輌CDFrom、車輌CDTo ValidatingEvent
        /// 存在チェック＆名称設定を行います
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool SHARYOU_CD_Validating(object sender, CancelEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // CDコントロール
                var cdControl = new r_framework.CustomControl.CustomTextBox();
                // NAMEコントロール
                var nameControl = new r_framework.CustomControl.CustomTextBox();

                // From、Toどちらのコントロールか判定
                var control = (r_framework.CustomControl.CustomTextBox)sender;
                if (control.Name.Equals(this.form.SHARYOU_CD_FROM.Name) && this.form.SHARYOU_CD_FROM.Enabled)
                {
                    cdControl = this.form.SHARYOU_CD_FROM;
                    nameControl = this.form.SHARYOU_NAME_FROM;
                }
                else if (control.Name.Equals(this.form.SHARYOU_CD_TO.Name) && this.form.SHARYOU_CD_TO.Enabled)
                {
                    cdControl = this.form.SHARYOU_CD_TO;
                    nameControl = this.form.SHARYOU_NAME_TO;
                }
                else
                {
                    return ret;
                }

                // 車輌CD
                string sharyouCd = cdControl.Text;

                // Nullチェック
                if (string.IsNullOrEmpty(sharyouCd))
                {
                    // 名称クリア
                    nameControl.Text = string.Empty;
                    return ret;
                }

                // 運搬業者CD
                // 活性化されるのは運搬業者CDが一意の場合のみのためFrom/Toどちらの値でも可
                string unpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD_FROM.Text;

                // 車輌検索
                M_SHARYOU[] sharyouDatas = this.GetSharyouData(unpanGyoushaCd, sharyouCd);

                // 存在チェック
                if (sharyouDatas == null || sharyouDatas.Length == 0)
                {
                    // エラー
                    new MessageBoxShowLogic().MessageBoxShow("E020", "車輌");
                    nameControl.Text = string.Empty;
                    cdControl.SelectAll();
                    e.Cancel = true;
                    return ret;
                }

                // 名称設定
                nameControl.Text = sharyouDatas[0].SHARYOU_NAME_RYAKU;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SHARYOU_CD_Validating", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHARYOU_CD_Validating", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 運転者CDFrom、運転者CDTo ValidatingEvent：存在チェック&名称設定

        internal bool UNTENSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // CDコントロール
                var cdControl = new r_framework.CustomControl.CustomTextBox();
                // NAMEコントロール
                var nameControl = new r_framework.CustomControl.CustomTextBox();

                // From、Toどちらのコントロールか判定
                var control = (r_framework.CustomControl.CustomTextBox)sender;
                if (control.Name.Equals(this.form.UNTENSHA_CD_FROM.Name))
                {
                    cdControl = this.form.UNTENSHA_CD_FROM;
                    nameControl = this.form.UNTENSHA_NAME_FROM;
                }
                else if (control.Name.Equals(this.form.UNTENSHA_CD_TO.Name))
                {
                    cdControl = this.form.UNTENSHA_CD_TO;
                    nameControl = this.form.UNTENSHA_NAME_TO;
                }
                else
                {
                    return ret;
                }

                // 運転者CD
                string untenshaCd = cdControl.Text;

                // Nullチェック
                if (string.IsNullOrEmpty(untenshaCd))
                {
                    // 名称クリア
                    nameControl.Text = string.Empty;
                    return ret;
                }

                // 社員検索
                M_SHAIN shainData = this.GetUntenshaData(untenshaCd);

                // 存在チェック
                if (shainData == null)
                {
                    // エラー
                    new MessageBoxShowLogic().MessageBoxShow("E020", "社員");
                    nameControl.Text = string.Empty;
                    cdControl.SelectAll();
                    e.Cancel = true;
                    return ret;
                }

                // 名称設定
                nameControl.Text = shainData.SHAIN_NAME_RYAKU;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UNTENSHA_CD_Validating", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNTENSHA_CD_Validating", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 未使用

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion
        #endregion
    }
}
