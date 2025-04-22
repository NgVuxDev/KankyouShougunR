using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Logic;
using r_framework.Dto;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Stock.ZaikoTyouseiNyuuryoku
{
    /// <summary>
    /// G631 在庫移動入力ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// 在庫量フォーマット(システム参照しない)
        /// </summary>
        /// <remarks>DBはDecimal(10,3)のため、小数部最大3桁を設定する。</remarks>
        internal readonly string ZAIKO_RYOU_FORMAT = "#,##0.###";

        /// <summary>
        /// 読み込みデータDTO
        /// </summary>
        internal DTOClass dto;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 親フォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// 業者Dao
        /// </summary>
        internal IM_GYOUSHADao gyoushaDao;
        /// <summary>
        /// 現場Dao
        /// </summary>
        internal IM_GENBADao genbaDao;
        /// <summary>
        /// 在庫品名Dao
        /// </summary>
        internal IM_ZAIKO_HINMEIDao zaikoHinmeiDao;
        /// <summary>
        /// 開始在庫情報Dao
        /// </summary>
        internal IM_KAISHI_ZAIKO_INFODao kaishiZaikoInfoDao;
        /// <summary>
        /// 在庫調整Dao
        /// </summary>
        internal IT_ZAIKO_TYOUSEI_ENTRYDao entryDao;
        /// <summary>
        /// 在庫調整明細Dao
        /// </summary>
        internal IT_ZAIKO_TYOUSEI_DETAILDao detailDao;
        /// <summary>
        /// 在庫月次Dao
        /// </summary>
        internal IT_MONTHLY_LOCK_ZAIKODao monthlyZaikoDao;

        private MessageBoxShowLogic MsgBox;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォームインスタンス</param>
        /// <param name="targetForm">メインフォームインスタンス</param>
        public LogicClass(UIHeader headerForm, UIForm targetForm)
        {
            LogUtility.DebugMethodStart(headerForm, targetForm);

            this.headerForm = headerForm;
            this.form = targetForm;

            // 権限をセット
            var formId = FormManager.GetFormID(Assembly.GetExecutingAssembly());

            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.zaikoHinmeiDao = DaoInitUtility.GetComponent<IM_ZAIKO_HINMEIDao>();
            this.entryDao = DaoInitUtility.GetComponent<IT_ZAIKO_TYOUSEI_ENTRYDao>();
            this.detailDao = DaoInitUtility.GetComponent<IT_ZAIKO_TYOUSEI_DETAILDao>();
            this.monthlyZaikoDao = DaoInitUtility.GetComponent<IT_MONTHLY_LOCK_ZAIKODao>();
            this.kaishiZaikoInfoDao = DaoInitUtility.GetComponent<IM_KAISHI_ZAIKO_INFODao>();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(targetForm);
        }
        #endregion

        #region 初期化処理
        /// <summary>
        /// モードに応じて画面を初期化します
        /// </summary>
        /// <param name="isClearDenpyouDate">伝票日付をクリアするかどうかのフラグ</param>
        public bool WindowInit(bool msgFlg = true)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    /* 月次処理中チェック */
                    this.form.TYOUSEI_DATE.Value = parentForm.sysDate;
                    DateTime checkDate = DateTime.Parse(this.form.TYOUSEI_DATE.Value.ToString());
                    GetsujiShoriCheckLogicClass checkLogic = new GetsujiShoriCheckLogicClass();

                    if (checkLogic.CheckGetsujiShoriChu(checkDate))
                    {
                        if (msgFlg)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E224", "登録");
                        }
                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    }
                    // 月次処理ロックチェック
                    else if (checkLogic.CheckGetsujiShoriLock(short.Parse(checkDate.Year.ToString()), short.Parse(checkDate.Month.ToString())))
                    {
                        if (msgFlg)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E223", "登録");
                        }
                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    }
                }

                this.form.HeaderFormInit();

                this.ButtonInit();

                this.EventInit();

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("G167", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            this.NewTypeInit();
                        }
                        else
                        {
                            this.ReferenceTypeInit();
                        }
                        break;
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        this.ReferenceTypeInit();
                        break;
                }

                this.form.DETAIL_Ichiran.Refresh();
                this.SetTyouseiData();

                this.form.GYOUSHA_CD.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 参照モードで初期化
        /// </summary>
        private void ReferenceTypeInit()
        {
            if (string.IsNullOrEmpty(this.form.TYOUSEI_NUMBER.Text))
            {
                this.form.TYOUSEI_NUMBER.Enabled = true;
                this.form.PREV_BUTTON.TabStop = true;
                this.form.NEXT_BUTTON.TabStop = true;
            }
            else
            {
                this.form.TYOUSEI_NUMBER.Enabled = false;
                this.form.PREV_BUTTON.TabStop = false;
                this.form.NEXT_BUTTON.TabStop = false;
            }
            this.form.TYOUSEI_NUMBER.TabStop = false;
            this.form.GYOUSHA_CD.Enabled = false;
            this.form.GYOUSHA_POPUP.Enabled = false;
            this.form.GENBA_CD.Enabled = false;
            this.form.GENBA_POPUP.Enabled = false;
            this.form.ZAIKO_HINMEI_CD.Enabled = false;
            this.form.ZAIKO_HINMEI_POPUP.Enabled = false;
            this.form.TYOUSEI_BIKOU1.Enabled = false;
            this.form.TYOUSEI_BIKOU2.Enabled = false;
            this.form.TYOUSEI_BIKOU3.Enabled = false;

            this.form.DETAIL_Ichiran.ReadOnly = true;
            this.form.DETAIL_Ichiran.AllowUserToAddRows = false;

            this.parentForm.bt_func2.Enabled = true;
            this.parentForm.bt_func7.Enabled = true;
            this.parentForm.bt_func9.Enabled = false;
            this.parentForm.bt_func11.Enabled = false;
            this.parentForm.bt_func12.Enabled = true;
            this.parentForm.bt_process1.Enabled = false;
        }

        /// <summary>
        /// 新規モードで初期化
        /// </summary>
        private void NewTypeInit()
        {
            this.form.TYOUSEI_NUMBER.Enabled = true;
            this.form.TYOUSEI_NUMBER.TabStop = false;
            this.form.PREV_BUTTON.TabStop = false;
            this.form.NEXT_BUTTON.TabStop = false;
            this.form.GYOUSHA_CD.Enabled = true;
            this.form.GYOUSHA_POPUP.Enabled = true;
            this.form.GENBA_CD.Enabled = true;
            this.form.GENBA_POPUP.Enabled = true;
            this.form.ZAIKO_HINMEI_CD.Enabled = true;
            this.form.ZAIKO_HINMEI_POPUP.Enabled = true;
            this.form.TYOUSEI_BIKOU1.Enabled = true;
            this.form.TYOUSEI_BIKOU2.Enabled = true;
            this.form.TYOUSEI_BIKOU3.Enabled = true;

            this.form.DETAIL_Ichiran.ReadOnly = true;
            this.form.DETAIL_Ichiran.AllowUserToAddRows = true;
            
            this.parentForm.bt_func2.Enabled = true;
            this.parentForm.bt_func7.Enabled = true;
            this.parentForm.bt_func9.Enabled = true;
            this.parentForm.bt_func11.Enabled = true;
            this.parentForm.bt_func12.Enabled = true;
            this.parentForm.bt_process1.Enabled = true;
        }

        /// <summary>
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // 新規ボタン(F2)イベント
            this.parentForm.bt_func2.Click -= new EventHandler(this.form.bt_func2_Click);
            this.parentForm.bt_func2.Click += new EventHandler(this.form.bt_func2_Click);

            // 一覧ボタン(F7)イベント
            this.parentForm.bt_func7.Click -= new EventHandler(this.form.bt_func7_Click);
            this.parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

            // 登録ボタン(F9)イベント
            this.parentForm.bt_func9.Click -= new EventHandler(this.form.bt_func9_Click);
            this.parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            // 行削除ボタン(F11)イベント
            this.parentForm.bt_func11.Click -= new EventHandler(this.form.bt_func11_Click);
            this.parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

            // 閉じるボタン(F12)イベント
            this.parentForm.bt_func12.Click -= new EventHandler(this.form.bt_func12_Click);
            this.parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // 在庫品名読込イベント
            this.parentForm.bt_process1.Click -= new EventHandler(this.form.bt_process1_Click);
            this.parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            // 前ボタンイベント
            this.form.PREV_BUTTON.Click -= new EventHandler(this.form.PrevButton_Click);
            this.form.PREV_BUTTON.Click += new EventHandler(this.form.PrevButton_Click);

            // 次ボタンイベント
            this.form.NEXT_BUTTON.Click -= new EventHandler(this.form.NextButton_Click);
            this.form.NEXT_BUTTON.Click += new EventHandler(this.form.NextButton_Click);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンコントロールを初期化します
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            ButtonControlUtility.SetButtonInfo(this.CreateButtonInfo(), this.parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定情報を作成します
        /// </summary>
        /// <returns>ボタン設定情報</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            string ButtonInfoXmlPath = this.GetType().Namespace;
            ButtonInfoXmlPath = ButtonInfoXmlPath + ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath));
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }
        #endregion

        #region 型変換メソッド
        /// <summary>
        /// 指定されたオブジェクトが null または Empty文字列であるかどうかを示します
        /// </summary>
        /// <param name="value">テストする文字列</param>
        /// <returns>null または Empty文字列の場合は true それ以外の場合は false</returns>
        internal bool IsNullOrEmpty(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = false;
            if (null == value)
            {
                ret = true;
            }
            else if (string.IsNullOrEmpty(value.ToString()))
            {
                ret = true;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを Decimal に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal Decimal ConvertToDecimal(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = 0m;
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                if (value is TextBox)
                {
                    ret = Decimal.Parse(((TextBox)value).Text);
                }
                else
                {
                    ret = Decimal.Parse(value.ToString());
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlDecimal に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal SqlDecimal ConvertToSqlDecimal(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = SqlDecimal.Null;
            if (!this.IsNullOrEmpty(value))
            {
                ret = SqlDecimal.Parse(value.ToString().Replace(",", ""));
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトを SqlInt64 に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト</returns>
        internal SqlInt64 ConvertToSqlInt64(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = SqlInt64.Null;
            if (!this.IsNullOrEmpty(value))
            {
                ret = SqlInt64.Parse(value.ToString());
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 前後在庫量フォーマット適用
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        internal string FormatJyuuryou(decimal val)
        {
            // 20150527 前後在庫量書式をシステム書式に参照しないように対応 Start
            return val.ToString(ZAIKO_RYOU_FORMAT);
            // 20150527 前後在庫量書式をシステム書式に参照しないように対応 End
        }
        #endregion

        #region データ取得メソッド
        /// <summary>
        /// 基準の在庫移動番号より前で最大の在庫移動番号を取得します
        /// </summary>
        /// <param name="tyouseiNumber">基準の在庫移動番号</param>
        /// <returns>在庫移動番号</returns>
        internal SqlInt64 GetPrevTyouseiNumber(SqlInt64 tyouseiNumber)
        {
            LogUtility.DebugMethodStart(tyouseiNumber);

            var ret = SqlInt64.Null;

            var number = "";
            if (!tyouseiNumber.IsNull)
            {
                number = tyouseiNumber.Value.ToString();
            }
            var tyouseiEntry = this.entryDao.GetPrevTyouseiNumber(number);
            if (null != tyouseiEntry)
            {
                ret = tyouseiEntry.TYOUSEI_NUMBER;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 基準の在庫移動番号より後で最小の在庫移動番号を取得します
        /// </summary>
        /// <param name="nyuukinNumber">基準の在庫移動番号</param>
        /// <returns>在庫移動番号</returns>
        internal SqlInt64 GetNextTyouseiNumber(SqlInt64 tyouseiNumber)
        {
            LogUtility.DebugMethodStart(tyouseiNumber);

            var ret = SqlInt64.Null;

            var number = "";
            if (!tyouseiNumber.IsNull)
            {
                number = tyouseiNumber.Value.ToString();
            }
            var idouEntry = this.entryDao.GetNextTyouseiNumber(number);
            if (null != idouEntry)
            {
                ret = idouEntry.TYOUSEI_NUMBER;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 在庫量を取得します
        /// </summary>
        internal SqlDecimal GetZaikoryou(string gyoushaCd, string genbaCd, string zaikoHInmeiCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd, zaikoHInmeiCd);

            SqlDecimal ret = SqlDecimal.Null;
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return ret;
            }
            if (string.IsNullOrEmpty(genbaCd))
            {
                return ret;
            }
            if (string.IsNullOrEmpty(zaikoHInmeiCd))
            {
                return ret;
            }

            ret = 0;
            SqlDateTime DATE_FROM = SqlDateTime.Null;
            SqlDateTime DATE_TO = Convert.ToDateTime(this.form.TYOUSEI_DATE.Value);

            T_MONTHLY_LOCK_ZAIKO monthly = this.monthlyZaikoDao.GetGetsujiData(gyoushaCd, genbaCd, zaikoHInmeiCd);

            if (monthly == null)
            {
                M_KAISHI_ZAIKO_INFO kaishiZaikoInfo = new M_KAISHI_ZAIKO_INFO();
                kaishiZaikoInfo.GYOUSHA_CD = gyoushaCd;
                kaishiZaikoInfo.GENBA_CD = genbaCd;
                kaishiZaikoInfo.ZAIKO_HINMEI_CD = zaikoHInmeiCd;
                M_KAISHI_ZAIKO_INFO[] result = this.kaishiZaikoInfoDao.GetAllValidData(kaishiZaikoInfo);

                if (result != null && result.Length > 0)
                {
                    if (!result[0].KAISHI_ZAIKO_RYOU.IsNull)
                    {
                        ret += result[0].KAISHI_ZAIKO_RYOU.Value;
                    }
                }
            }
            else
            {
                DATE_FROM = new DateTime(monthly.YEAR.Value, monthly.MONTH.Value, 1).AddMonths(1);
                ret += monthly.GOUKEI_ZAIKO_RYOU.Value;
            }

            DataTable dt = this.entryDao.GetZaikoRyou(gyoushaCd, genbaCd, zaikoHInmeiCd, DATE_FROM, DATE_TO);
            if (dt == null || dt.Rows.Count == 0 || string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ZAIKO_RYOU"])))
            {
                ret += 0;
            }
            else
            {
                ret += this.ConvertToDecimal(dt.Rows[0]["ZAIKO_RYOU"]);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion

        #region ボタンイベント
        #region 前ボタン処理
        /// <summary>
        /// 前ボタン処理
        /// </summary>
        public void Prev()
        {
            var tyouseiNumber = SqlInt64.Null;
            if (!string.IsNullOrEmpty(this.form.TYOUSEI_NUMBER.Text))
            {
                tyouseiNumber = this.ConvertToSqlInt64(this.form.TYOUSEI_NUMBER.Text);
            }

            // 読み込む対象の移動番号を取得
            tyouseiNumber = this.GetPrevTyouseiNumber(tyouseiNumber);

            if (tyouseiNumber.IsNull)
            {
                // 読み込む対象の移動番号を取得
                tyouseiNumber = this.GetPrevTyouseiNumber(tyouseiNumber);
            }

            var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());

            if (tyouseiNumber.IsNull)
            {
                if (!Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                // 移動データがない
                this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            }
            else
            {
                if (!Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.REFERENCE_WINDOW_FLAG));
                    return;
                }

                this.form.TYOUSEI_NUMBER.Text = tyouseiNumber.Value.ToString();
                this.form.tyouseiNumber = this.form.TYOUSEI_NUMBER.Text;
                this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            }

            if (!this.WindowInit()) { return; }
        }
        #endregion

        #region 次ボタン処理
        /// <summary>
        /// 次ボタン処理
        /// </summary>
        public void Next()
        {
            var tyouseiNumber = SqlInt64.Null;
            if (!string.IsNullOrEmpty(this.form.TYOUSEI_NUMBER.Text))
            {
                tyouseiNumber = this.ConvertToSqlInt64(this.form.TYOUSEI_NUMBER.Text);
            }

            // 読み込む対象の移動番号を取得
            tyouseiNumber = this.GetNextTyouseiNumber(tyouseiNumber);

            if (tyouseiNumber.IsNull)
            {
                // 読み込む対象の移動番号を取得
                tyouseiNumber = this.GetNextTyouseiNumber(tyouseiNumber);
            }

            var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());

            if (tyouseiNumber.IsNull)
            {
                if (!Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                // 移動データがない
                this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            }
            else
            {
                if (!Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.REFERENCE_WINDOW_FLAG));
                    return;
                }

                this.form.TYOUSEI_NUMBER.Text = tyouseiNumber.Value.ToString();
                this.form.tyouseiNumber = this.form.TYOUSEI_NUMBER.Text;
                this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            }

            if (!this.WindowInit()) { return; }
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        internal void Regist()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            var autoCheckLogic = new AutoRegistCheckLogic(this.form.allControl, this.form.allControl);
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (this.form.RegistErrorFlag)
            {
                var focusControl = this.form.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
                else
                {
                    foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                    {
                        bool flg = false;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            var CheckControl = cell as ICustomAutoChangeBackColor;
                            if (CheckControl.IsInputErrorOccured)
                            {
                                this.form.DETAIL_Ichiran.CurrentCell = cell;
                                this.form.DETAIL_Ichiran.Focus();
                                flg = true;
                                break;
                            }
                        }
                        if (flg)
                        {
                            break;
                        }
                    }
                }
                return;
            }

            if (!this.form.RegistErrorFlag)
            {
                int cnt = this.form.DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).Count();
                if (cnt == 0)
                {
                    msgLogic.MessageBoxShow("E001", "明細行");
                    this.form.RegistErrorFlag = true;
                }
                //else if (!string.IsNullOrEmpty(this.form.TYOUSEI_BEFORE_GOUKEI.Text) && Convert.ToDecimal(this.form.TYOUSEI_AFTER_GOUKEI.Text.Replace(",", "")) < 0)
                //{
                //    msgLogic.MessageBoxShowError("調整量合計は調整前在庫量以下のようにしてください。");
                //    this.form.RegistErrorFlag = true;
                //}
                else
                {
                    for (int i = 0; i < this.form.DETAIL_Ichiran.Rows.Count; i++)
                    {
                        DataGridViewRow row = this.form.DETAIL_Ichiran.Rows[i];
                        decimal TYOUSEI_BEFORE_ZAIKO_RYOU = 0;
                        decimal.TryParse(Convert.ToString(row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value), out TYOUSEI_BEFORE_ZAIKO_RYOU);
                        decimal TYOUSEI_RYOU = 0;
                        decimal.TryParse(Convert.ToString(row.Cells["MEISAI_TYOUSEI_RYOU"].Value), out TYOUSEI_RYOU);
                        if (TYOUSEI_BEFORE_ZAIKO_RYOU + TYOUSEI_RYOU < 0)
                        {
                            msgLogic.MessageBoxShowError("調整量は調整前在庫量以下のようにしてください。");
                            this.form.RegistErrorFlag = true;
                            break;
                        }
                        for (int j = 0; j < i; j++)
                        {
                            DataGridViewRow row2 = this.form.DETAIL_Ichiran.Rows[j];
                            if (!string.IsNullOrEmpty(Convert.ToString(row2.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value)) && row2.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value.Equals(row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value))
                            {
                                msgLogic.MessageBoxShow("E003", "在庫品名CD", Convert.ToString(row2.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value));
                                this.form.RegistErrorFlag = true;
                                break;
                            }
                        }
                    }
                }
            }

            if (!this.form.RegistErrorFlag)
            {
                // 月次処理中チェック
                GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                if (getsujiShoriCheckLogic.CheckGetsujiShoriChu())
                {
                    // 月次処理中は登録不可
                    if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                    {
                        msgLogic.MessageBoxShow("E224", "登録");
                    }
                    this.form.RegistErrorFlag = true;
                }
                // 月次処理ロックチェック
                else
                {
                    bool isGetsujiErr = false;
                    string errMsg = "";
                    if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                    {
                        // 新規、削除は画面に表示されている伝票日付を使用
                        DateTime getsujiShoriCheckDate = DateTime.Parse(this.form.TYOUSEI_DATE.Value.ToString());
                        if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(getsujiShoriCheckDate.Year.ToString()), short.Parse(getsujiShoriCheckDate.Month.ToString())))
                        {
                            isGetsujiErr = true;
                            errMsg = "登録";
                        }
                    }

                    if (isGetsujiErr)
                    {
                        // ロック中は登録不可
                        msgLogic.MessageBoxShow("E223", errMsg);
                        this.form.RegistErrorFlag = true;
                    }
                }
            }

            if (!this.form.RegistErrorFlag)
            {
                using (Transaction tran = new Transaction())
                {
                    var registDto = this.CreateEntity();

                    // 調整入力
                    this.entryDao.Insert(registDto.Entry);

                    // 調整明細
                    foreach (var detail in registDto.DetailList)
                    {
                        this.detailDao.Insert(detail);
                    }

                    tran.Commit();
                }

                var messageLogic = new MessageBoxShowLogic();
                messageLogic.MessageBoxShow("I001", "登録");

                var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());
                if (Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                {
                    this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.form.tyouseiNumber = "";
                    if (!this.WindowInit()) { return; }
                    this.form.TYOUSEI_DATE.Focus();
                }
                else
                {
                    // 追加権限が無い場合は画面を閉じる
                    this.form.Close();
                    this.parentForm.Close();
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 登録データ作成処理
        /// <summary>
        /// 登録データ作成処理
        /// </summary>
        /// <returns></returns>
        internal DTOClass CreateEntity()
        {
            LogUtility.DebugMethodStart();

            var ret = new DTOClass();
            T_ZAIKO_TYOUSEI_ENTRY entry = new T_ZAIKO_TYOUSEI_ENTRY();
            List<T_ZAIKO_TYOUSEI_DETAIL> detailList = new List<T_ZAIKO_TYOUSEI_DETAIL>();

            entry.SYSTEM_ID = this.CreateSystemId();
            entry.SEQ = 1;
            entry.TYOUSEI_NUMBER = this.CreateIdouNumber();
            entry.TYOUSEI_DATE = Convert.ToDateTime(this.form.TYOUSEI_DATE.Value);
            entry.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            entry.GYOUSHA_NAME = this.form.GYOUSHA_NAME.Text;
            entry.GENBA_CD = this.form.GENBA_CD.Text;
            entry.GENBA_NAME = this.form.GENBA_NAME.Text;
            entry.TYOUSEI_BIKOU1 = this.form.TYOUSEI_BIKOU1.Text;
            entry.TYOUSEI_BIKOU2 = this.form.TYOUSEI_BIKOU2.Text;
            entry.TYOUSEI_BIKOU3 = this.form.TYOUSEI_BIKOU3.Text;
            entry.TYOUSEI_BEFORE_GOUKEI = ConvertToDecimal(this.form.TYOUSEI_BEFORE_GOUKEI.Text);
            entry.TYOUSEI_AFTER_GOUKEI = ConvertToDecimal(this.form.TYOUSEI_AFTER_GOUKEI.Text);
            entry.DELETE_FLG = false;

            // 在庫移動エンティティ作成
            var newNyuukinSumEntryBinderLogic = new DataBinderLogic<T_ZAIKO_TYOUSEI_ENTRY>(entry);
            newNyuukinSumEntryBinderLogic.SetSystemProperty(entry, false);

            T_ZAIKO_TYOUSEI_DETAIL detail = new T_ZAIKO_TYOUSEI_DETAIL();
            short index = 0;
            foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                detail = new T_ZAIKO_TYOUSEI_DETAIL();
                detail.SYSTEM_ID = entry.SYSTEM_ID;
                detail.SEQ = entry.SEQ;
                detail.DETAIL_SYSTEM_ID = this.CreateSystemId();
                detail.TYOUSEI_NUMBER = entry.TYOUSEI_NUMBER;
                detail.ROW_NO = index++;
                detail.ZAIKO_HINMEI_CD = Convert.ToString(row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value);
                detail.ZAIKO_HINMEI_NAME = Convert.ToString(row.Cells["MEISAI_ZAIKO_HINMEI_NAME"].Value);
                detail.TYOUSEI_BEFORE_ZAIKO_RYOU = ConvertToDecimal(row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value);
                detail.TYOUSEI_RYOU = ConvertToDecimal(row.Cells["MEISAI_TYOUSEI_RYOU"].Value);
                detail.TYOUSEI_AFTER_ZAIKO_RYOU = ConvertToDecimal(row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value);
                detailList.Add(detail);
            }
            ret.Entry = entry;
            ret.DetailList = detailList;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #region 行削除処理
        /// <summary>
        /// 行削除処理
        /// </summary>
        internal void RowRemove()
        {
            if (this.form.DETAIL_Ichiran.CurrentRow != null)
            {
                if (!this.form.DETAIL_Ichiran.CurrentRow.IsNewRow)
                {
                    this.form.DETAIL_Ichiran.Rows.RemoveAt(this.form.DETAIL_Ichiran.CurrentRow.Index);
                }
            }

            this.SetTyouseiRyouGoukei();
        }
        #endregion

        #region システムID採番
        /// <summary>
        /// 新規にシステムIDを取得します
        /// </summary>
        /// <returns>システムID</returns>
        private SqlInt64 CreateSystemId()
        {
            LogUtility.DebugMethodStart();

            var ret = SqlInt64.Null;

            var accessor = new DBAccessor();
            ret = accessor.createSystemId((int)DENSHU_KBN.ZAIKO);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #region 調整番号採番
        /// <summary>
        /// 新規に調整番号を取得します
        /// </summary>
        /// <returns></returns>
        public SqlInt64 CreateIdouNumber()
        {
            LogUtility.DebugMethodStart();

            var ret = SqlInt64.Null;

            var accessor = new DBAccessor();
            ret = accessor.createDenshuNumber((int)DENSHU_KBN.ZAIKO);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #region 在庫品名読込
        /// <summary>
        /// 在庫品名読込
        /// </summary>
        /// <returns></returns>
        public void zaikoMeiYomikomi()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                msgLogic.MessageBoxShow("E001", "業者");
                return;
            }

            if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                msgLogic.MessageBoxShow("E001", "現場");
                return;
            }

            bool errFlg = true;
            if (this.form.DETAIL_Ichiran.Rows.Count > 1)
            {
                DialogResult result = msgLogic.MessageBoxShow("C090");
                if (result == DialogResult.No)
                {
                    errFlg = false;
                }
            }
            if (errFlg)
            {
                this.form.DETAIL_Ichiran.Rows.Clear();
                M_ZAIKO_HINMEI zaikoHinmei = new M_ZAIKO_HINMEI();
                M_ZAIKO_HINMEI[] himeiArr = this.zaikoHinmeiDao.GetAllValidData(zaikoHinmei);

                if (himeiArr != null && himeiArr.Length > 0)
                {
                    this.form.label1.Focus();
                    this.form.DETAIL_Ichiran.Rows.Add(himeiArr.Length);
                    for (int i = 0; i < himeiArr.Length; i++)
                    {
                        DataGridViewRow row = this.form.DETAIL_Ichiran.Rows[i];
                        row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = himeiArr[i].ZAIKO_HINMEI_CD;
                        row.Cells["MEISAI_ZAIKO_HINMEI_NAME"].Value = himeiArr[i].ZAIKO_HINMEI_NAME_RYAKU;
                        SqlDecimal zaikoryou = this.GetZaikoryou(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, himeiArr[i].ZAIKO_HINMEI_CD);
                        if (zaikoryou.IsNull)
                        {
                            zaikoryou = 0;
                        }
                        row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value = FormatJyuuryou(zaikoryou.Value);
                        row.Cells["MEISAI_TYOUSEI_RYOU"].Value = "";
                        row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = FormatJyuuryou(zaikoryou.Value);
                    }
                    this.form.DETAIL_Ichiran.Focus();
                    this.SetTyouseiRyouGoukei();
                }
            }
        }
        #endregion
        #endregion

        #region IBuisinessLogicの実装
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

        #region 画面設定
        /// <summary>
        /// 取得した在庫調整データを画面にセットします
        /// </summary>
        internal void SetTyouseiData()
        {
            LogUtility.DebugMethodStart();

            if (string.IsNullOrEmpty(this.form.tyouseiNumber))
            {
                // 画面にデータセット
                this.headerForm.CREATE_USER.Text = "";
                this.headerForm.CREATE_DATE.Text = "";
                this.headerForm.UPDATE_USER.Text = "";
                this.headerForm.UPDATE_DATE.Text = "";

                // 明細一覧
                foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                {
                    row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = "";
                }
                this.form.DETAIL_Ichiran.Rows.Clear();

                this.form.TYOUSEI_NUMBER.Text = "";
                //this.form.TYOUSEI_DATE.Value = DateTime.Now;
                this.form.GYOUSHA_CD.Text = "";
                this.form.GYOUSHA_NAME.Text = "";
                this.form.GENBA_CD.Text = "";
                this.form.GENBA_NAME.Text = "";
                this.form.TYOUSEI_BIKOU1.Text = "";
                this.form.TYOUSEI_BIKOU2.Text = "";
                this.form.TYOUSEI_BIKOU3.Text = "";
                this.form.ZAIKO_HINMEI_CD.Text = "";
                this.form.ZAIKO_HINMEI_NAME.Text = "";
                this.form.TYOUSEI_BEFORE_GOUKEI.Text = "";
                this.form.TYOUSEI_AFTER_GOUKEI.Text = "";

            }
            else
            {
                T_ZAIKO_TYOUSEI_ENTRY entry = new T_ZAIKO_TYOUSEI_ENTRY();
                entry.TYOUSEI_NUMBER = Convert.ToInt64(this.form.tyouseiNumber);
                entry = this.entryDao.GetZaikoTyouseiEntry(entry);
                if (entry == null)
                {
                    // 該当データなし
                    var messageLogic = new MessageBoxShowLogic();
                    messageLogic.MessageBoxShow("E045");

                    this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.form.tyouseiNumber = "";
                    if (!this.WindowInit(false)) { return; }
                }
                else
                {
                    // 画面にデータセット
                    this.form.TYOUSEI_NUMBER.Text = entry.TYOUSEI_NUMBER.Value.ToString();
                    this.form.TYOUSEI_DATE.Value = entry.TYOUSEI_DATE;
                    this.form.GYOUSHA_CD.Text = entry.GYOUSHA_CD;
                    this.form.GYOUSHA_NAME.Text = entry.GYOUSHA_NAME;
                    this.form.GENBA_CD.Text = entry.GENBA_CD;
                    this.form.GENBA_NAME.Text = entry.GENBA_NAME;
                    this.form.TYOUSEI_BIKOU1.Text = entry.TYOUSEI_BIKOU1;
                    this.form.TYOUSEI_BIKOU2.Text = entry.TYOUSEI_BIKOU2;
                    this.form.TYOUSEI_BIKOU3.Text = entry.TYOUSEI_BIKOU3;
                    this.form.ZAIKO_HINMEI_CD.Text = string.Empty;
                    this.form.ZAIKO_HINMEI_NAME.Text = string.Empty;
                    this.form.TYOUSEI_BEFORE_GOUKEI.Text = FormatJyuuryou(entry.TYOUSEI_BEFORE_GOUKEI.Value);
                    this.form.TYOUSEI_AFTER_GOUKEI.Text = FormatJyuuryou(entry.TYOUSEI_AFTER_GOUKEI.Value);

                    this.headerForm.CREATE_USER.Text = entry.CREATE_USER;
                    this.headerForm.CREATE_DATE.Text = entry.CREATE_DATE.ToString();
                    this.headerForm.UPDATE_USER.Text = entry.UPDATE_USER;
                    this.headerForm.UPDATE_DATE.Text = entry.UPDATE_DATE.ToString();
                    // 移動明細
                    T_ZAIKO_TYOUSEI_DETAIL detail = new T_ZAIKO_TYOUSEI_DETAIL();
                    detail.SYSTEM_ID = entry.SYSTEM_ID;
                    detail.SEQ = entry.SEQ;
                    List<T_ZAIKO_TYOUSEI_DETAIL> list = this.detailDao.GetZaikoTyouseiDetailList(detail);

                    foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                    {
                        row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = "";
                    }
                    this.form.DETAIL_Ichiran.Rows.Clear();
                    if (list.Count > 0)
                    {
                        this.form.DETAIL_Ichiran.Rows.Add(list.Count);
                        for (int i = 0; i < list.Count; i++)
                        {
                            DataGridViewRow row = this.form.DETAIL_Ichiran.Rows[i];
                            detail = list[i];
                            row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = detail.ZAIKO_HINMEI_CD;
                            row.Cells["MEISAI_ZAIKO_HINMEI_NAME"].Value = detail.ZAIKO_HINMEI_NAME;
                            row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value = FormatJyuuryou(detail.TYOUSEI_BEFORE_ZAIKO_RYOU.Value);
                            row.Cells["MEISAI_TYOUSEI_RYOU"].Value = this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG ?
                                FormatJyuuryou(detail.TYOUSEI_RYOU.Value) : detail.TYOUSEI_RYOU.Value.ToString();
                            row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = FormatJyuuryou(detail.TYOUSEI_AFTER_ZAIKO_RYOU.Value);
                        }
                    }
                }
            }

            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 画面にデータセット
                this.form.TYOUSEI_NUMBER.Text = "";
                this.form.TYOUSEI_DATE.Value = parentForm.sysDate;

                //SqlDecimal zaikoryou = this.GetZaikoryou(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.ZAIKO_HINMEI_CD.Text);
                //if (zaikoryou.IsNull)
                //{
                //    this.form.TYOUSEI_BEFORE_GOUKEI.Text = "";
                //    this.form.TYOUSEI_AFTER_GOUKEI.Text = "";
                //}
                //else
                //{
                //    this.form.TYOUSEI_BEFORE_GOUKEI.Text = string.Format("{0:" + SysInfo.SYS_JYURYOU_FORMAT + "}", zaikoryou.Value);
                //    this.form.TYOUSEI_AFTER_GOUKEI.Text = this.form.TYOUSEI_BEFORE_GOUKEI.Text;
                //}

                this.headerForm.CREATE_USER.Text = "";
                this.headerForm.CREATE_DATE.Text = "";
                this.headerForm.UPDATE_USER.Text = "";
                this.headerForm.UPDATE_DATE.Text = "";

                // 移動明細
                foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    string zaikoHinmeiCd = Convert.ToString(row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value);

                    M_ZAIKO_HINMEI zaikoHinmei = new M_ZAIKO_HINMEI();
                    zaikoHinmei = this.zaikoHinmeiDao.GetDataByCd(zaikoHinmeiCd);

                    if (zaikoHinmei != null)
                    {
                        row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = zaikoHinmei.ZAIKO_HINMEI_CD;
                    }
                    else
                    {
                        row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = "";
                        row.Cells["MEISAI_ZAIKO_HINMEI_NAME"].Value = "";
                        row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value = "";
                        row.Cells["MEISAI_TYOUSEI_RYOU"].Value = "";
                        row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = "";
                    }

                    SqlDecimal zaikoryou = this.GetZaikoryou(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.ZAIKO_HINMEI_CD.Text);
                    if (zaikoryou.IsNull)
                    {
                        row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value = "";
                        row.Cells["MEISAI_TYOUSEI_RYOU"].Value = "";
                        row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = "";
                    }
                    else
                    {
                        row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value = FormatJyuuryou(zaikoryou.Value);
                        row.Cells["MEISAI_TYOUSEI_RYOU"].Value = "";
                        row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = FormatJyuuryou(zaikoryou.Value);
                    }
                }

                this.SetTyouseiRyouGoukei();
            }
            else
            {
                if (string.IsNullOrEmpty(this.form.TYOUSEI_NUMBER.Text))
                {
                    this.form.TYOUSEI_NUMBER.Enabled = true;
                    this.form.PREV_BUTTON.TabStop = true;
                    this.form.NEXT_BUTTON.TabStop = true;
                }
                else
                {
                    this.form.TYOUSEI_NUMBER.Enabled = false;
                    this.form.PREV_BUTTON.TabStop = false;
                    this.form.NEXT_BUTTON.TabStop = false;
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 調整量合計
        /// <summary>
        /// 調整量合計をセットします
        /// </summary>
        internal void SetTyouseiRyouGoukei()
        {
            Decimal total = 0;
            //Decimal before = this.ConvertToDecimal(this.form.TYOUSEI_BEFORE_GOUKEI.Text.Replace(",", ""));
            Decimal before = 0;
            Decimal idou = 0;
            foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
            {
                before += this.ConvertToDecimal(Convert.ToString(row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value).Replace(",", ""));
                idou = this.ConvertToDecimal(Convert.ToString(row.Cells["MEISAI_TYOUSEI_RYOU"].Value).Replace(",", ""));
                total += idou;
            }
            this.form.TYOUSEI_BEFORE_GOUKEI.Text = FormatJyuuryou(before);
            this.form.TYOUSEI_AFTER_GOUKEI.Text = FormatJyuuryou(before + total);
        }
        #endregion

        #region 画面イベント
        #region 調整番号Validated
        /// <summary>
        /// 調整番号CDValidated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void TYOUSEI_NUMBER_Validated()
        {
            try
            {
                var tyouseiNumber = SqlInt64.Null;
                if (!string.IsNullOrEmpty(this.form.TYOUSEI_NUMBER.Text) && !this.form.TYOUSEI_NUMBER.ReadOnly)
                {
                    tyouseiNumber = this.ConvertToSqlInt64(this.form.TYOUSEI_NUMBER.Text);
                }
                else
                {
                    return;
                }

                var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());

                if (!tyouseiNumber.IsNull)
                {
                    if (!Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.REFERENCE_WINDOW_FLAG));
                        return;
                    }

                    this.form.TYOUSEI_NUMBER.Text = tyouseiNumber.Value.ToString();
                    this.form.tyouseiNumber = this.form.TYOUSEI_NUMBER.Text;
                    this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }

                if (!this.WindowInit()) { return; }
                if (!string.IsNullOrEmpty(this.form.TYOUSEI_NUMBER.Text))
                {
                    this.form.PREV_BUTTON.Focus();
                }
                else
                {
                    this.form.TYOUSEI_NUMBER.Focus();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TYOUSEI_NUMBER_Validated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }
        #endregion

        #region 業者CDValidated
        /// <summary>
        /// 業者CDValidated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void GYOUSHA_CD_Validated()
        {
            var messageLogic = new MessageBoxShowLogic();
            try
            {
                int cnt = this.form.DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow).Count();
                if (cnt > 0 && this.form.beforeGyoushaCd != this.form.GYOUSHA_CD.Text)
                {
                    DialogResult result = messageLogic.MessageBoxShow("C088");
                    if (result == DialogResult.No)
                    {
                        this.form.GYOUSHA_CD.Text = this.form.beforeGyoushaCd;
                        this.form.GYOUSHA_NAME.Text = this.form.beforeGyoushaName;
                        return;
                    }
                    else
                    {
                        this.form.TYOUSEI_BEFORE_GOUKEI.Text = "";
                        this.form.TYOUSEI_AFTER_GOUKEI.Text = "";
                        foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                        {
                            row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = "";
                        }
                        this.form.DETAIL_Ichiran.Rows.Clear();
                    }
                }

                if (this.form.beforeGyoushaCd != this.form.GYOUSHA_CD.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                    {
                        this.form.GYOUSHA_NAME.Text = "";
                        this.form.GENBA_CD.Text = "";
                        this.form.GENBA_NAME.Text = "";
                        this.form.beforeGyoushaCd = "";
                        this.form.beforeGyoushaName = "";
                        this.form.beforeGenbaCd = "";
                        this.form.beforeGenbaName = "";
                        this.form.TYOUSEI_BEFORE_GOUKEI.Text = "";
                        this.form.TYOUSEI_AFTER_GOUKEI.Text = "";

                        this.form.DETAIL_Ichiran.ReadOnly = true;
                        this.form.DETAIL_Ichiran.Refresh();
                        foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                        {
                            row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = "";
                        }
                        this.form.DETAIL_Ichiran.Rows.Clear();
                        foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                        {
                            var cell = row.Cells["MEISAI_ZAIKO_HINMEI_CD"] as DgvCustomAlphaNumTextBoxCell;
                            cell.PopupSearchSendParams.Clear();
                        }
                        return;
                    }

                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    gyousha.JISHA_KBN = true;
                    gyousha = this.gyoushaDao.GetAllValidData(gyousha).FirstOrDefault();

                    if (gyousha == null)
                    {
                        this.form.isError = true;
                        this.form.GYOUSHA_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.GYOUSHA_NAME.Text = "";
                        messageLogic.MessageBoxShow("E020", "業者");
                        this.form.GYOUSHA_CD.Focus();
                        return;
                    }
                    else
                    {
                        SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                        DateTime date;
                        if (!string.IsNullOrWhiteSpace(this.form.TYOUSEI_DATE.Text) && DateTime.TryParse(this.form.TYOUSEI_DATE.Text, out date))
                        {
                            tekiyouDate = date;
                        }
                        if (gyousha.TEKIYOU_BEGIN.IsNull && gyousha.TEKIYOU_END.IsNull)
                        {
                            this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                        }
                        else if (gyousha.TEKIYOU_BEGIN.IsNull && !gyousha.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha.TEKIYOU_END) <= 0)
                        {
                            this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                        }
                        else if (!gyousha.TEKIYOU_BEGIN.IsNull && gyousha.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(gyousha.TEKIYOU_BEGIN) >= 0)
                        {
                            this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                        }
                        else if (!gyousha.TEKIYOU_BEGIN.IsNull && !gyousha.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(gyousha.TEKIYOU_BEGIN) >= 0
                                && tekiyouDate.CompareTo(gyousha.TEKIYOU_END) <= 0)
                        {
                            this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.isError = true;
                            this.form.GYOUSHA_CD.BackColor = Constans.ERROR_COLOR;
                            this.form.GYOUSHA_NAME.Text = "";
                            messageLogic.MessageBoxShow("E020", "業者");
                            this.form.GYOUSHA_CD.Focus();
                            return;
                        }
                    }

                    this.form.GENBA_CD.Text = "";
                    this.form.GENBA_NAME.Text = "";
                    this.form.DETAIL_Ichiran.ReadOnly = true;
                    this.form.DETAIL_Ichiran.Refresh();
                    foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                    {
                        row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = "";
                    }
                    this.form.DETAIL_Ichiran.Rows.Clear();
                    this.form.beforeGyoushaCd = this.form.GYOUSHA_CD.Text;
                    this.form.beforeGyoushaName = this.form.GYOUSHA_NAME.Text;
                    this.form.beforeGenbaCd = "";
                    this.form.beforeGenbaName = "";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_CD_Validated", ex);
                if (ex is SQLRuntimeException)
                {
                    messageLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    messageLogic.MessageBoxShow("E245", "");
                }
            }
        }
        #endregion

        #region 現場CDValidated
        /// <summary>
        /// 現場CDValidated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void GENBA_CD_Validated()
        {
            var messageLogic = new MessageBoxShowLogic();
            try
            {
                //if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                //{
                //    messageLogic.MessageBoxShow("E034", "業者");
                //    this.form.GENBA_CD.Focus();
                //    return;
                //}

                int cnt = this.form.DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow).Count();
                if (cnt > 0 && this.form.beforeGenbaCd != this.form.GENBA_CD.Text)
                {
                    DialogResult result = messageLogic.MessageBoxShow("C088");
                    if (result == DialogResult.No)
                    {
                        this.form.GYOUSHA_CD.Text = this.form.beforeGyoushaCd;
                        this.form.GYOUSHA_NAME.Text = this.form.beforeGyoushaName;
                        this.form.GENBA_CD.Text = this.form.beforeGenbaCd;
                        this.form.GENBA_NAME.Text = this.form.beforeGenbaName;
                        return;
                    }
                    else
                    {
                        this.form.TYOUSEI_BEFORE_GOUKEI.Text = "";
                        this.form.TYOUSEI_AFTER_GOUKEI.Text = "";
                        foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                        {
                            row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = "";
                        }
                        this.form.DETAIL_Ichiran.Rows.Clear();
                    }
                }

                if (this.form.beforeGenbaCd != this.form.GENBA_CD.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                    {
                        this.form.GENBA_NAME.Text = "";
                        this.form.beforeGenbaCd = "";
                        this.form.beforeGenbaName = "";
                        this.form.TYOUSEI_BEFORE_GOUKEI.Text = "";
                        this.form.TYOUSEI_AFTER_GOUKEI.Text = "";
                        this.form.DETAIL_Ichiran.ReadOnly = true;
                        this.form.DETAIL_Ichiran.Refresh();
                        foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                        {
                            row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = "";
                        }
                        this.form.DETAIL_Ichiran.Rows.Clear();

                        return;
                    }

                    if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                    {
                        messageLogic.MessageBoxShow("E051", "業者");
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_CD.Focus();
                        return;
                    }

                    M_GENBA genba = new M_GENBA();
                    genba.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    genba.GENBA_CD = this.form.GENBA_CD.Text;
                    genba.JISHA_KBN = true;
                    genba = this.genbaDao.GetAllValidData(genba).FirstOrDefault();

                    if (genba == null)
                    {
                        this.form.isError = true;
                        this.form.GENBA_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.GENBA_NAME.Text = "";
                        messageLogic.MessageBoxShow("E020", "現場");
                        this.form.GENBA_CD.Focus();
                        return;
                    }
                    else
                    {
                        SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                        DateTime date;
                        if (!string.IsNullOrWhiteSpace(this.form.TYOUSEI_DATE.Text) && DateTime.TryParse(this.form.TYOUSEI_DATE.Text, out date))
                        {
                            tekiyouDate = date;
                        }
                        if (genba.TEKIYOU_BEGIN.IsNull && genba.TEKIYOU_END.IsNull)
                        {
                            this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                            this.form.DETAIL_Ichiran.ReadOnly = false;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_ZAIKO_HINMEI_NAME"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["lbl1"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["lbl2"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Refresh();
                        }
                        else if (genba.TEKIYOU_BEGIN.IsNull && !genba.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba.TEKIYOU_END) <= 0)
                        {
                            this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                            this.form.DETAIL_Ichiran.ReadOnly = false;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_ZAIKO_HINMEI_NAME"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["lbl1"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["lbl2"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Refresh();
                        }
                        else if (!genba.TEKIYOU_BEGIN.IsNull && genba.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(genba.TEKIYOU_BEGIN) >= 0)
                        {
                            this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                            this.form.DETAIL_Ichiran.ReadOnly = false;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_ZAIKO_HINMEI_NAME"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["lbl1"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["lbl2"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Refresh();
                        }
                        else if (!genba.TEKIYOU_BEGIN.IsNull && !genba.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(genba.TEKIYOU_BEGIN) >= 0
                                && tekiyouDate.CompareTo(genba.TEKIYOU_END) <= 0)
                        {
                            this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                            this.form.DETAIL_Ichiran.ReadOnly = false;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_ZAIKO_HINMEI_NAME"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["lbl1"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["lbl2"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Columns["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].ReadOnly = true;
                            this.form.DETAIL_Ichiran.Refresh();
                        }
                        else
                        {
                            this.form.isError = true;
                            this.form.GENBA_CD.BackColor = Constans.ERROR_COLOR;
                            this.form.GENBA_NAME.Text = "";
                            messageLogic.MessageBoxShow("E020", "現場");
                            this.form.GENBA_CD.Focus();
                            return;
                        }
                    }

                    foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                    {
                        row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = "";
                    }
                    this.form.DETAIL_Ichiran.Rows.Clear();
                    this.form.beforeGenbaCd = this.form.GENBA_CD.Text;
                    this.form.beforeGenbaName = this.form.GENBA_NAME.Text;

                    //SqlDecimal zaikoryou = this.GetZaikoryou(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.ZAIKO_HINMEI_CD.Text);
                    //if (zaikoryou.IsNull)
                    //{
                    //    this.form.TYOUSEI_BEFORE_GOUKEI.Text = "";
                    //    this.form.TYOUSEI_AFTER_GOUKEI.Text = "";
                    //}
                    //else
                    //{
                    //    this.form.TYOUSEI_BEFORE_GOUKEI.Text = string.Format("{0:" + SysInfo.SYS_JYURYOU_FORMAT + "}", zaikoryou.Value);
                    //    this.form.TYOUSEI_AFTER_GOUKEI.Text = this.form.TYOUSEI_BEFORE_GOUKEI.Text;
                    //}
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_CD_Validated", ex);
                if (ex is SQLRuntimeException)
                {
                    messageLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    messageLogic.MessageBoxShow("E245", "");
                }
            }
        }
        #endregion

        #region 在庫品名CDValidated
        /// <summary>
        /// 在庫品名CDValidated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal int ZAIKO_HINMEI_CD_Validating()
        {
            int ret = 0;
            var messageLogic = new MessageBoxShowLogic();

            try
            {
                if (this.form.beforeZaikoHinmeiCd != this.form.ZAIKO_HINMEI_CD.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.ZAIKO_HINMEI_CD.Text))
                    {
                        this.form.ZAIKO_HINMEI_NAME.Text = "";
                        this.form.beforeZaikoHinmeiCd = "";
                        this.form.beforeZaikoHinmeiName = "";
                        return ret;
                    }

                    M_ZAIKO_HINMEI zaikoHinmei = new M_ZAIKO_HINMEI();
                    zaikoHinmei.ZAIKO_HINMEI_CD = this.form.ZAIKO_HINMEI_CD.Text;
                    zaikoHinmei = this.zaikoHinmeiDao.GetAllValidData(zaikoHinmei).FirstOrDefault();

                    if (zaikoHinmei == null || zaikoHinmei.DELETE_FLG.IsTrue)
                    {
                        this.form.isError = true;
                        this.form.ZAIKO_HINMEI_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.ZAIKO_HINMEI_NAME.Text = "";
                        messageLogic.MessageBoxShow("E020", "在庫品名");
                        this.form.ZAIKO_HINMEI_CD.Focus();
                        ret = -1;
                        return ret;
                    }
                    else
                    {
                        this.form.ZAIKO_HINMEI_NAME.Text = zaikoHinmei.ZAIKO_HINMEI_NAME_RYAKU;
                    }

                    this.form.beforeZaikoHinmeiCd = this.form.ZAIKO_HINMEI_CD.Text;
                    this.form.beforeZaikoHinmeiName = this.form.ZAIKO_HINMEI_NAME.Text;

                    //SqlDecimal zaikoryou = this.GetZaikoryou(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.ZAIKO_HINMEI_CD.Text);
                    //if (zaikoryou.IsNull)
                    //{
                    //    this.form.TYOUSEI_BEFORE_GOUKEI.Text = "";
                    //    this.form.TYOUSEI_AFTER_GOUKEI.Text = "";
                    //}
                    //else
                    //{
                    //    this.form.TYOUSEI_BEFORE_GOUKEI.Text = string.Format("{0:" + SysInfo.SYS_JYURYOU_FORMAT + "}", zaikoryou.Value);
                    //    this.form.TYOUSEI_AFTER_GOUKEI.Text = this.form.TYOUSEI_BEFORE_GOUKEI.Text;
                    //}
                    //string genbaCd = "";
                    //foreach(DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                    //{
                    //    if (row.IsNewRow)
                    //    {
                    //        continue;
                    //    }
                    //    genbaCd = this.form.GENBA_CD.Text;
                    //    zaikoryou = this.GetZaikoryou(this.form.GYOUSHA_CD.Text, genbaCd, this.form.ZAIKO_HINMEI_CD.Text);
                    //    if (zaikoryou.IsNull)
                    //    {
                    //        row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value = "";
                    //        row.Cells["MEISAI_TYOUSEI_RYOU"].Value = "";
                    //        row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = "";
                    //    }
                    //    else
                    //    {
                    //        row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value = string.Format("{0:" + SysInfo.SYS_JYURYOU_FORMAT + "}", zaikoryou.Value);
                    //        string tyousei = Convert.ToString(row.Cells["MEISAI_TYOUSEI_RYOU"].Value);
                    //        if (string.IsNullOrEmpty(tyousei))
                    //        {
                    //            row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = string.Format("{0:" + SysInfo.SYS_JYURYOU_FORMAT + "}", zaikoryou.Value);
                    //        }
                    //        else
                    //        {
                    //            decimal tyouseiRyou = Convert.ToDecimal(tyousei.Replace(",", ""));
                    //            row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = string.Format("{0:" + SysInfo.SYS_JYURYOU_FORMAT + "}", zaikoryou.Value + tyouseiRyou);
                    //        }
                    //    }
                    //}
                    //this.SetTyouseiRyouGoukei();
                }


                if (!string.IsNullOrEmpty(this.form.ZAIKO_HINMEI_CD.Text))
                {
                    foreach (DataGridViewRow row in this.form.DETAIL_Ichiran.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        if (Convert.ToString(row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value).Equals(this.form.ZAIKO_HINMEI_CD.Text))
                        {
                            this.form.DETAIL_Ichiran.Focus();
                            row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Selected = true;
                            ret = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZAIKO_HINMEI_CD_Validating", ex);
                if (ex is SQLRuntimeException)
                {
                    messageLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    messageLogic.MessageBoxShow("E245", "");
                }
                ret = -1;
            }
            finally
            {
            }
            return ret;
        }
        #endregion

        #region 明細一覧Validated
        /// <summary>
        /// 明細一覧Validated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void DETAIL_Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var messageLogic = new MessageBoxShowLogic();
            try
            {
                DataGridViewRow row = this.form.DETAIL_Ichiran.Rows[e.RowIndex];
                DataGridViewCell cell = this.form.DETAIL_Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.OwningColumn.Name == "MEISAI_TYOUSEI_RYOU")
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value)))
                    {
                        Decimal before = Convert.ToDecimal(Convert.ToString(row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value).Replace(",", ""));
                        Decimal idou = 0;
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["MEISAI_TYOUSEI_RYOU"].Value)))
                        {
                            idou = Convert.ToDecimal(Convert.ToString(row.Cells["MEISAI_TYOUSEI_RYOU"].Value).Replace(",", ""));
                        }
                        row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = FormatJyuuryou((before + idou));
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value)))
                    {
                        this.SetTyouseiRyouGoukei();
                    }
                }
                else if (cell.OwningColumn.Name == "MEISAI_ZAIKO_HINMEI_CD")
                {
                    if (this.form.beforeMesaiEditZaikoHinmeiCd != Convert.ToString(row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value) || this.form.isError)
                    {
                        this.form.isError = false;
                        string zaikoHimeiCd = Convert.ToString(row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value);
                        if (string.IsNullOrEmpty(zaikoHimeiCd))
                        {
                            row.Cells["MEISAI_ZAIKO_HINMEI_NAME"].Value = "";
                            row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value = "";
                            row.Cells["MEISAI_TYOUSEI_RYOU"].Value = "";
                            row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = "";

                            this.SetTyouseiRyouGoukei();
                            return;
                        }
                        else
                        {
                            zaikoHimeiCd = zaikoHimeiCd.ToUpper();
                            row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value = zaikoHimeiCd;

                            if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                            {
                                this.form.isError = true;
                                messageLogic.MessageBoxShow("E034", "業者");
                                this.form.GYOUSHA_CD.BackColor = Constans.ERROR_COLOR;
                                e.Cancel = true;
                                return;
                            }
                            else if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                            {
                                this.form.isError = true;
                                messageLogic.MessageBoxShow("E034", "現場");
                                this.form.GENBA_CD.BackColor = Constans.ERROR_COLOR;
                                e.Cancel = true;
                                return;
                            }
                        }

                        int cnt = this.form.DETAIL_Ichiran.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow && Convert.ToString(r.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value) == zaikoHimeiCd).Count();

                        if (cnt > 1)
                        {
                            this.form.isError = true;
                            cell.Style.BackColor = Constans.ERROR_COLOR;
                            row.Cells["MEISAI_ZAIKO_HINMEI_NAME"].Value = "";
                            row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value = "";
                            row.Cells["MEISAI_TYOUSEI_RYOU"].Value = "";
                            row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = "";
                            messageLogic.MessageBoxShow("E031", "在庫品名CD");
                            e.Cancel = true;
                            return;
                        }


                        M_ZAIKO_HINMEI zaikoHinmei = new M_ZAIKO_HINMEI();
                        zaikoHinmei.ZAIKO_HINMEI_CD = zaikoHimeiCd;
                        //zaikoHinmei = this.zaikoHinmeiDao.GetDataByCd(zaikoHimeiCd);
                        M_ZAIKO_HINMEI[] result = this.zaikoHinmeiDao.GetAllValidData(zaikoHinmei);
                        if (result == null || result.Length == 0)
                        {
                            this.form.isError = true;
                            messageLogic.MessageBoxShow("E020", "在庫品名");
                            ((TextBox)this.form.DETAIL_Ichiran.EditingControl).SelectAll();
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            row.Cells["MEISAI_ZAIKO_HINMEI_NAME"].Value = result[0].ZAIKO_HINMEI_NAME_RYAKU;
                        }
                        SqlDecimal zaikoryou = this.GetZaikoryou(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, Convert.ToString(row.Cells["MEISAI_ZAIKO_HINMEI_CD"].Value));
                        if (zaikoryou.IsNull)
                        {
                            row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value = "";
                            row.Cells["MEISAI_TYOUSEI_RYOU"].Value = "";
                            row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = "";
                        }
                        else
                        {
                            row.Cells["MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU"].Value = FormatJyuuryou(zaikoryou.Value);
                            string tyousei = Convert.ToString(row.Cells["MEISAI_TYOUSEI_RYOU"].Value);
                            if (string.IsNullOrEmpty(tyousei))
                            {
                                row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = FormatJyuuryou(zaikoryou.Value);
                            }
                            else
                            {
                                decimal tyouseiRyou = Convert.ToDecimal(tyousei.Replace(",", ""));
                                row.Cells["MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU"].Value = FormatJyuuryou(zaikoryou.Value + tyouseiRyou);
                            }
                        }
                    }

                    this.SetTyouseiRyouGoukei();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DETAIL_Ichiran_CellValidating", ex);
                if (ex is SQLRuntimeException)
                {
                    messageLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    messageLogic.MessageBoxShow("E245", "");
                }
            }
        }
        #endregion
        #endregion
    }
}
