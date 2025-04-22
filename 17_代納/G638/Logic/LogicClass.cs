using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using System.Data.SqlTypes;
using Shougun.Core.Common.BusinessCommon.Dao;
using System.Collections.ObjectModel;
using System.Text;
using CommonChouhyouPopup.Logic;
using CommonChouhyouPopup.App;
using System.Collections;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.PayByProxy.DainoMeisaihyoOutput
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>ボタンの設定用ファイルパス</summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.PayByProxy.DainoMeisaihyoOutput.Setting.ButtonSetting.xml";

        /// <summary>
        /// 建廃マニフェスト入力のForm
        /// </summary>
        public UIForm form { get; set; }

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 建廃マニフェスト入力のHeader
        /// </summary>
        public UIHeader headerform { get; set; }

        /// <summary> 親フォーム</summary>
        public BusinessBaseForm parentbaseform { get; set; }

        /// <summary>画面初期表示Flag</summary>
        private bool firstLoadFlg = true;

        /// <summary>Entity</summary>
        public SuperEntity Entity { get; set; }

        /// <summary>検索結果</summary>
        public DataTable SearchResult { get; set; }

        /// <summary>検索条件</summary>
        public SearchParameterDto SearchString { get; set; }

        /// <summary>取引先Dao</summary>
        public IM_TORIHIKISAKIDao toriDao { get; set; }

        /// <summary>業者Dao</summary>
        public IM_GYOUSHADao gyoushaDao { get; set; }

        /// <summary>現場Dao</summary>
        public IM_GENBADao genbaDao { get; set; }

        /// <summary>自社情報マスタDao</summary>
        public IM_CORP_INFODao corpInfoDao;

        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allControl;

        private M_SYS_INFO mSysInfo;

        internal MessageBoxShowLogic errmessage;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            this.form = targetForm;
            this.toriDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.corpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.mSysInfo = new DBAccessor().GetSysInfo();
            this.errmessage = new MessageBoxShowLogic();
            LogUtility.DebugMethodEnd(targetForm);
        }

        /// <summary>
        /// 
        /// </summary>
        public void LogicalDelete()
        {
            this.LogicalDelete2();
        }

        #region 登録/更新/削除
        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public bool LogicalDelete2()
        {
            return true;
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Registメソッドが正常の場合True
        /// </summary>
        private bool RegistResult = false;
        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            RegistResult = false;
            try
            {
                RegistResult = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// update成功時True
        /// </summary>
        private bool UpdateResult = false;
        /// <summary>
        /// 修正処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            UpdateResult = false;

            try
            {
                UpdateResult = true;

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;

            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        #endregion

        #region 初期化

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.firstLoadFlg)
                {
                    // フォームインスタンスを取得
                    this.parentbaseform = (BusinessBaseForm)this.form.Parent;
                    this.headerform = (UIHeader)parentbaseform.headerForm;

                    // ボタンのテキストを初期化
                    this.ButtonInit();

                    // イベントの初期化処理
                    this.EventInit();

                    this.parentForm = (BusinessBaseForm)this.form.Parent;

                    this.allControl = this.form.allControl;
                }

                // コントロールを初期化
                this.ControlInit();

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG://更新モード
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                        break;
                }
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

        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        private void SetErrorFocus()
        {
            Control target = null;
            foreach (Control control in this.form.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        if (target != null)
                        {
                            if (target.TabIndex > control.TabIndex)
                            {
                                target = control;
                            }
                        }
                        else
                        {
                            target = control;
                        }
                    }
                }
            }
            //ヘッダーチェック
            foreach (Control control in this.headerform.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        target = control;
                    }
                }
            }
            if (target != null)
            {
                target.Focus();
            }
        }

        /// <summary>
        /// コントロール初期化処理
        /// </summary>
        public void ControlInit()
        {
            LogUtility.DebugMethodStart();
            (new ManifestoLogic()).SetKyoten(this.form.KYOTEN_CD, this.form.KYOTEN_NAME);
            DateTime now = this.parentForm.sysDate;
            this.form.DATE_FROM.Value = now.Date;
            this.form.DATE_TO.Value = now.Date;
            this.form.UKEIRE_TORI_CD_FROM.Text = "";
            this.form.UKEIRE_TORI_CD_TO.Text = "";
            this.form.UKEIRE_GYOSHA_CD_FROM.Text = "";
            this.form.UKEIRE_GYOSHA_CD_TO.Text = "";
            this.form.UKEIRE_GENBA_CD_FROM.Text = "";
            this.form.UKEIRE_GENBA_CD_TO.Text = "";
            this.form.SHUKKA_TORI_CD_FROM.Text = "";
            this.form.SHUKKA_TORI_CD_TO.Text = "";
            this.form.SHUKKA_GYOSHA_CD_FROM.Text = "";
            this.form.SHUKKA_GYOSHA_CD_TO.Text = "";
            this.form.SHUKKA_GENBA_CD_FROM.Text = "";
            this.form.SHUKKA_GENBA_CD_TO.Text = "";
            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// ボタン初期化処理
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;
            
            //CSVボタン(F5)イベント生成
            parentform.bt_func5.Click += new EventHandler(this.form.bt_func05_Click);

            //表示ボタン(F7)イベント生成
            parentform.bt_func7.Click += new EventHandler(this.form.bt_func07_Click);

            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            this.form.DATE_TO.DoubleClick += new EventHandler(this.form.DATE_TO_DoubleClick);
            this.form.UKEIRE_TORI_CD_TO.DoubleClick += new EventHandler(this.form.UKEIRE_TORI_CD_TO_DoubleClick);
            this.form.UKEIRE_GYOSHA_CD_TO.DoubleClick += new EventHandler(this.form.UKEIRE_GYOSHA_CD_TO_DoubleClick);
            this.form.UKEIRE_GENBA_CD_TO.DoubleClick += new EventHandler(this.form.UKEIRE_GENBA_CD_TO_DoubleClick);
            this.form.SHUKKA_TORI_CD_TO.DoubleClick += new EventHandler(this.form.SHUKKA_TORI_CD_TO_DoubleClick);
            this.form.SHUKKA_GYOSHA_CD_TO.DoubleClick += new EventHandler(this.form.SHUKKA_GYOSHA_CD_TO_DoubleClick);
            this.form.SHUKKA_GENBA_CD_TO.DoubleClick += new EventHandler(this.form.SHUKKA_GENBA_CD_TO_DoubleClick);
            this.form.UKEIRE_TORI_CD_FROM.Validating += new CancelEventHandler(this.form.UKEIRE_TORI_CD_FROM_Validating);
            this.form.UKEIRE_TORI_CD_TO.Validating += new CancelEventHandler(this.form.UKEIRE_TORI_CD_TO_Validating);
            this.form.UKEIRE_GYOSHA_CD_FROM.Validating += new CancelEventHandler(this.form.UKEIRE_GYOSHA_CD_FROM_Validating);
            this.form.UKEIRE_GYOSHA_CD_TO.Validating += new CancelEventHandler(this.form.UKEIRE_GYOSHA_CD_TO_Validating);
            this.form.UKEIRE_GENBA_CD_FROM.Validating += new CancelEventHandler(this.form.UKEIRE_GENBA_CD_FROM_Validating);
            this.form.UKEIRE_GENBA_CD_TO.Validating += new CancelEventHandler(this.form.UKEIRE_GENBA_CD_TO_Validating);
            this.form.SHUKKA_TORI_CD_FROM.Validating += new CancelEventHandler(this.form.SHUKKA_TORI_CD_FROM_Validating);
            this.form.SHUKKA_TORI_CD_TO.Validating += new CancelEventHandler(this.form.SHUKKA_TORI_CD_TO_Validating);
            this.form.SHUKKA_GYOSHA_CD_FROM.Validating += new CancelEventHandler(this.form.SHUKKA_GYOSHA_CD_FROM_Validating);
            this.form.SHUKKA_GYOSHA_CD_TO.Validating += new CancelEventHandler(this.form.SHUKKA_GYOSHA_CD_TO_Validating);
            this.form.SHUKKA_GENBA_CD_FROM.Validating += new CancelEventHandler(this.form.SHUKKA_GENBA_CD_FROM_Validating);
            this.form.SHUKKA_GENBA_CD_TO.Validating += new CancelEventHandler(this.form.SHUKKA_GENBA_CD_TO_Validating);

            //前回値保存の仕組み初期化
            this.form.EnterEventInit();
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 補助ファンクション
        /// <summary>
        /// Int16?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal Int16? ToNInt16(object o)
        {
            Int16? ret = null;
            Int16 parse = 0;
            if (Int16.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// Int32?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal int? ToNInt32(object o)
        {
            int? ret = null;
            int parse = 0;
            if (int.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// Int64?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal Int64? ToNInt64(object o)
        {
            Int64? ret = null;
            Int64 parse = 0;
            if (Int64.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        ///// <summary>
        ///// double?型に転換する
        ///// </summary>
        ///// <param name="o">o</param>
        //internal double? ToNDouble(object o)
        //{
        //    double? ret = null;
        //    double parse = 0;
        //    if (double.TryParse(Convert.ToString(o), out parse))
        //    {
        //        ret = parse;
        //    }
        //    return ret;
        //}

        /// <summary>
        /// decimal?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal decimal? ToNDecimal(object o)
        {
            decimal? ret = null;
            decimal parse = 0;
            if (decimal.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// bool?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal bool? ToNBoolean(object o)
        {
            bool? ret = null;
            bool parse = false;
            if (Boolean.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// DateTime?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal DateTime? ToNDateTime(object o)
        {
            DateTime? ret = null;
            DateTime parse = this.parentForm.sysDate;
            if (DateTime.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }
        #endregion

        #region マスタ検索処理
        /// <summary>
        /// 取引先データ取得処理
        /// </summary>
        /// <returns></returns>
        public M_TORIHIKISAKI GetTorihikisaki(string cd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(cd);
                M_TORIHIKISAKI condition = new M_TORIHIKISAKI();
                condition.TORIHIKISAKI_CD = cd;
                condition.ISNOT_NEED_DELETE_FLG = true;
                M_TORIHIKISAKI[] ret = this.toriDao.GetAllValidData(condition);
                LogUtility.DebugMethodEnd(ret, catchErr);
                return ret.Length == 0 ? null : ret[0];
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisaki", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(null, catchErr);
            return null;
        }

        /// <summary>
        /// 業者データ取得処理
        /// </summary>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string cd, int denshuKbn, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(cd, denshuKbn);
                M_GYOUSHA condition = new M_GYOUSHA();
                condition.GYOUSHA_CD = cd;
                condition.ISNOT_NEED_DELETE_FLG = true;
                if (denshuKbn == 1)
                {
                    condition.GYOUSHAKBN_UKEIRE = true;
                }
                else
                {
                    condition.GYOUSHAKBN_SHUKKA = true;
                }
                M_GYOUSHA[] ret = this.gyoushaDao.GetAllValidData(condition);
                LogUtility.DebugMethodEnd(ret, catchErr);
                return ret.Length == 0 ? null : ret[0];
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(null, catchErr);
            return null;
        }

        /// <summary>
        /// 現場データ取得処理
        /// </summary>
        /// <returns></returns>
        public DataTable GetGenba(string gyoushaCd, string genbaCd, int denshuKbn, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd, denshuKbn);
                var sql = new StringBuilder();
                sql.Append("    SELECT M_GENBA.GYOUSHA_CD, ");
                sql.Append("           M_GYOUSHA.GYOUSHA_NAME_RYAKU, ");
                sql.Append("           M_GENBA.GENBA_CD, ");
                sql.Append("           M_GENBA.GENBA_NAME_RYAKU, ");
                sql.Append("           M_GYOUSHA.TORIHIKISAKI_CD, ");
                sql.Append("           M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU ");
                sql.Append("      FROM M_GENBA ");
                sql.Append(" LEFT JOIN M_GYOUSHA ");
                sql.Append("        ON M_GYOUSHA.GYOUSHA_CD = M_GENBA.GYOUSHA_CD ");
                sql.Append(" LEFT JOIN M_TORIHIKISAKI ");
                sql.Append("        ON M_GYOUSHA.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD ");
                sql.Append("     WHERE 1 = 1 ");
                if (!string.IsNullOrEmpty(gyoushaCd))
                {
                    sql.AppendFormat("       AND M_GENBA.GYOUSHA_CD = '{0}' ", gyoushaCd);
                }
                sql.AppendFormat("       AND M_GENBA.GENBA_CD = '{0}' ", genbaCd);
                if (denshuKbn == 1)
                {
                    sql.Append("       AND M_GYOUSHA.GYOUSHAKBN_UKEIRE = 1 ");
                }
                else
                {
                    sql.Append("       AND M_GYOUSHA.GYOUSHAKBN_SHUKKA = 1 ");
                }
                DataTable ret = this.genbaDao.GetDateForStringSql(sql.ToString());
                LogUtility.DebugMethodEnd(ret, catchErr);
                return ret;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenba", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 自社情報検索処理
        /// </summary>
        /// <param name="cd">CD</param>
        internal M_CORP_INFO[] GetJishaJouhou()
        {
            LogUtility.DebugMethodStart();
            M_CORP_INFO entity = new M_CORP_INFO();
            entity.SYS_ID = 0;
            M_CORP_INFO[] results = this.corpInfoDao.GetAllValidData(entity);
            LogUtility.DebugMethodEnd();
            return results;
        }
        #endregion

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <returns></returns>
        public void CsvPrint()
        {
            var dao = DaoInitUtility.GetComponent<DainoMeisaiDao>();
            SearchParameterDto condition = new SearchParameterDto();
            condition.KYOTEN_CD = Convert.ToInt16(this.form.KYOTEN_CD.Text);
            condition.DATE_FROM = Convert.ToDateTime(this.form.DATE_FROM.Value);
            condition.DATE_TO = Convert.ToDateTime(this.form.DATE_TO.Value);
            condition.UKEIRE_TORI_CD_FROM = this.form.UKEIRE_TORI_CD_FROM.Text;
            condition.UKEIRE_TORI_CD_TO = this.form.UKEIRE_TORI_CD_TO.Text;
            if (!string.IsNullOrEmpty(this.form.UKEIRE_GYOSHA_CD_FROM.Text))
            {
                condition.UKEIRE_GYOUSHA_CD_FROM = this.form.UKEIRE_GYOSHA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UKEIRE_GYOSHA_CD_TO.Text))
            {
                condition.UKEIRE_GYOUSHA_CD_TO = this.form.UKEIRE_GYOSHA_CD_TO.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UKEIRE_GENBA_CD_FROM.Text))
            {
                condition.UKEIRE_GENBA_CD_FROM = this.form.UKEIRE_GENBA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UKEIRE_GENBA_CD_TO.Text))
            {
                condition.UKEIRE_GENBA_CD_TO = this.form.UKEIRE_GENBA_CD_TO.Text;
            }
            condition.SHUKKA_TORI_CD_FROM = this.form.SHUKKA_TORI_CD_FROM.Text;
            condition.SHUKKA_TORI_CD_TO = this.form.SHUKKA_TORI_CD_TO.Text;
            if (!string.IsNullOrEmpty(this.form.SHUKKA_GYOSHA_CD_FROM.Text))
            {
                condition.SHUKKA_GYOUSHA_CD_FROM = this.form.SHUKKA_GYOSHA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHUKKA_GYOSHA_CD_TO.Text))
            {
                condition.SHUKKA_GYOUSHA_CD_TO = this.form.SHUKKA_GYOSHA_CD_TO.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHUKKA_GENBA_CD_FROM.Text))
            {
                condition.SHUKKA_GENBA_CD_FROM = this.form.SHUKKA_GENBA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHUKKA_GENBA_CD_TO.Text))
            {
                condition.SHUKKA_GENBA_CD_TO = this.form.SHUKKA_GENBA_CD_TO.Text;
            }
            DataTable dt = dao.GetData(condition);

            string[] csvHead = { "伝票番号", "伝票日付", "支払日付", "売上日付", "受入取引先CD", "受入取引先", "受入業者CD",
                                       "受入業者", "受入現場CD", "受入現場", "出荷取引先CD", "出荷取引先",
                                       "出荷業者CD", "出荷業者", "出荷現場CD", "出荷現場", "運搬業者CD",
                                       "運搬業者", "車輌CD", "車輌", "受入品名CD", "受入品名", "受入正味",
                                       "受入調整", "受入実正味", "受入数量", "受入単位CD", "受入単位", "受入単価",
                                       "支払金額", "受入備考", "出荷品名CD", "出荷品名", "出荷正味", "出荷調整",
                                       "出荷実正味", "出荷数量", "出荷単位CD", "出荷単位", "出荷単価", "売上金額", "出荷備考"};

            DataTable csvDT = new DataTable();
            DataRow rowTmp;
            for (int i = 0; i < csvHead.Length; i++)
            {
                csvDT.Columns.Add(csvHead[i]);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rowTmp = csvDT.NewRow();

                if (dt.Rows[i]["UR_SH_NUMBER"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UR_SH_NUMBER"].ToString()))
                {
                    rowTmp["伝票番号"] = dt.Rows[i]["UR_SH_NUMBER"].ToString();
                }

                if (dt.Rows[i]["DENPYOU_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DENPYOU_DATE"].ToString()))
                {
                    rowTmp["伝票日付"] = Convert.ToDateTime(dt.Rows[i]["DENPYOU_DATE"]).ToString("yyyy/MM/dd");
                }

                if (dt.Rows[i]["SHIHARAI_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHIHARAI_DATE"].ToString()))
                {
                    rowTmp["支払日付"] = Convert.ToDateTime(dt.Rows[i]["SHIHARAI_DATE"]).ToString("yyyy/MM/dd");
                }

                if (dt.Rows[i]["URIAGE_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["URIAGE_DATE"].ToString()))
                {
                    rowTmp["売上日付"] = Convert.ToDateTime(dt.Rows[i]["URIAGE_DATE"]).ToString("yyyy/MM/dd");
                }

                if (dt.Rows[i]["U_TORI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_TORI_CD"].ToString()))
                {
                    rowTmp["受入取引先CD"] = dt.Rows[i]["U_TORI_CD"].ToString();
                }

                if (dt.Rows[i]["U_TORI_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_TORI_NAME"].ToString()))
                {
                    rowTmp["受入取引先"] = dt.Rows[i]["U_TORI_NAME"].ToString();
                }

                if (dt.Rows[i]["U_GYOUSHA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_GYOUSHA_CD"].ToString()))
                {
                    rowTmp["受入業者CD"] = dt.Rows[i]["U_GYOUSHA_CD"].ToString();
                }

                if (dt.Rows[i]["U_GYOUSHA_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_GYOUSHA_NAME"].ToString()))
                {
                    rowTmp["受入業者"] = dt.Rows[i]["U_GYOUSHA_NAME"].ToString();
                }

                if (dt.Rows[i]["U_GENBA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_GENBA_CD"].ToString()))
                {
                    rowTmp["受入現場CD"] = dt.Rows[i]["U_GENBA_CD"].ToString();
                }

                if (dt.Rows[i]["U_GENBA_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_GENBA_NAME"].ToString()))
                {
                    rowTmp["受入現場"] = dt.Rows[i]["U_GENBA_NAME"].ToString();
                }

                if (dt.Rows[i]["S_TORI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_TORI_CD"].ToString()))
                {
                    rowTmp["出荷取引先CD"] = dt.Rows[i]["S_TORI_CD"].ToString();
                }

                if (dt.Rows[i]["S_TORI_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_TORI_NAME"].ToString()))
                {
                    rowTmp["出荷取引先"] = dt.Rows[i]["S_TORI_NAME"].ToString();
                }

                if (dt.Rows[i]["S_GYOUSHA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_GYOUSHA_CD"].ToString()))
                {
                    rowTmp["出荷業者CD"] = dt.Rows[i]["S_GYOUSHA_CD"].ToString();
                }

                if (dt.Rows[i]["S_GYOUSHA_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_GYOUSHA_NAME"].ToString()))
                {
                    rowTmp["出荷業者"] = dt.Rows[i]["S_GYOUSHA_NAME"].ToString();
                }

                if (dt.Rows[i]["S_GENBA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_GENBA_CD"].ToString()))
                {
                    rowTmp["出荷現場CD"] = dt.Rows[i]["S_GENBA_CD"].ToString();
                }

                if (dt.Rows[i]["S_GENBA_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_GENBA_NAME"].ToString()))
                {
                    rowTmp["出荷現場"] = dt.Rows[i]["S_GENBA_NAME"].ToString();
                }
                
                if (dt.Rows[i]["UPN_GYOUSHA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UPN_GYOUSHA_CD"].ToString()))
                {
                    rowTmp["運搬業者CD"] = dt.Rows[i]["UPN_GYOUSHA_CD"].ToString();
                }

                if (dt.Rows[i]["UPN_GYOUSHA_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UPN_GYOUSHA_NAME"].ToString()))
                {
                    rowTmp["運搬業者"] = dt.Rows[i]["UPN_GYOUSHA_NAME"].ToString();
                }

                if (dt.Rows[i]["SHARYOU_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHARYOU_CD"].ToString()))
                {
                    rowTmp["車輌CD"] = dt.Rows[i]["SHARYOU_CD"].ToString();
                }

                if (dt.Rows[i]["SHARYOU_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHARYOU_NAME"].ToString()))
                {
                    rowTmp["車輌"] = dt.Rows[i]["SHARYOU_NAME"].ToString();
                }

                if (dt.Rows[i]["U_HINMEI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_HINMEI_CD"].ToString()))
                {
                    rowTmp["受入品名CD"] = dt.Rows[i]["U_HINMEI_CD"].ToString();
                }

                if (dt.Rows[i]["U_HINMEI_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_HINMEI_NAME"].ToString()))
                {
                    rowTmp["受入品名"] = dt.Rows[i]["U_HINMEI_NAME"].ToString();
                }

                if (dt.Rows[i]["U_SHOMI"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_SHOMI"].ToString()))
                {
                    rowTmp["受入正味"] = this.SetFormat(Convert.ToString(dt.Rows[i]["U_SHOMI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                }

                if (dt.Rows[i]["U_CHOUSEI"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_CHOUSEI"].ToString()))
                {
                    rowTmp["受入調整"] = this.SetFormat(Convert.ToString(dt.Rows[i]["U_CHOUSEI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                }

                if (dt.Rows[i]["U_JITUSHOMI"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_JITUSHOMI"].ToString()))
                {
                    rowTmp["受入実正味"] = this.SetFormat(Convert.ToString(dt.Rows[i]["U_JITUSHOMI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                }

                if (dt.Rows[i]["U_NUMBER"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_NUMBER"].ToString()))
                {
                    rowTmp["受入数量"] = this.SetFormat(Convert.ToString(dt.Rows[i]["U_NUMBER"]), this.mSysInfo.SYS_SUURYOU_FORMAT);
                }

                if (dt.Rows[i]["U_UNIT_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_UNIT_CD"].ToString()))
                {
                    rowTmp["受入単位CD"] = dt.Rows[i]["U_UNIT_CD"].ToString();
                }

                if (dt.Rows[i]["U_UNIT"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_UNIT"].ToString()))
                {
                    rowTmp["受入単位"] = dt.Rows[i]["U_UNIT"].ToString();
                }

                if (dt.Rows[i]["U_TANKA"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_TANKA"].ToString()))
                {
                    rowTmp["受入単価"] = this.SetFormat(Convert.ToString(dt.Rows[i]["U_TANKA"]), this.mSysInfo.SYS_TANKA_FORMAT);
                }

                if (dt.Rows[i]["U_KINGAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_KINGAKU"].ToString()))
                {
                    rowTmp["支払金額"] = this.SetFormat(Convert.ToString(dt.Rows[i]["U_KINGAKU"]), "#,##0");
                }

                if (dt.Rows[i]["U_BIKOU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["U_BIKOU"].ToString()))
                {
                    rowTmp["受入備考"] = dt.Rows[i]["U_BIKOU"].ToString();
                }

                if (dt.Rows[i]["S_HINMEI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_HINMEI_CD"].ToString()))
                {
                    rowTmp["出荷品名CD"] = dt.Rows[i]["S_HINMEI_CD"].ToString();
                }

                if (dt.Rows[i]["S_HINMEI_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_HINMEI_NAME"].ToString()))
                {
                    rowTmp["出荷品名"] = dt.Rows[i]["S_HINMEI_NAME"].ToString();
                }

                if (dt.Rows[i]["S_SHOMI"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_SHOMI"].ToString()))
                {
                    rowTmp["出荷正味"] = this.SetFormat(Convert.ToString(dt.Rows[i]["S_SHOMI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                }

                if (dt.Rows[i]["S_CHOUSEI"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_CHOUSEI"].ToString()))
                {
                    rowTmp["出荷調整"] = this.SetFormat(Convert.ToString(dt.Rows[i]["S_CHOUSEI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                }

                if (dt.Rows[i]["S_JITUSHOMI"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_JITUSHOMI"].ToString()))
                {
                    rowTmp["出荷実正味"] = this.SetFormat(Convert.ToString(dt.Rows[i]["S_JITUSHOMI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                }

                if (dt.Rows[i]["S_NUMBER"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_NUMBER"].ToString()))
                {
                    rowTmp["出荷数量"] = this.SetFormat(Convert.ToString(dt.Rows[i]["S_NUMBER"]), this.mSysInfo.SYS_SUURYOU_FORMAT);
                }

                if (dt.Rows[i]["S_UNIT_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_UNIT_CD"].ToString()))
                {
                    rowTmp["出荷単位CD"] = dt.Rows[i]["S_UNIT_CD"].ToString();
                }

                if (dt.Rows[i]["S_UNIT"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_UNIT"].ToString()))
                {
                    rowTmp["出荷単位"] = dt.Rows[i]["S_UNIT"].ToString();
                }

                if (dt.Rows[i]["S_TANKA"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_TANKA"].ToString()))
                {
                    rowTmp["出荷単価"] = this.SetFormat(Convert.ToString(dt.Rows[i]["S_TANKA"]), this.mSysInfo.SYS_TANKA_FORMAT);
                }

                if (dt.Rows[i]["S_KINGAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_KINGAKU"].ToString()))
                {
                    rowTmp["売上金額"] = this.SetFormat(Convert.ToString(dt.Rows[i]["S_KINGAKU"]), "#,##0");
                }

                if (dt.Rows[i]["S_BIKOU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["S_BIKOU"].ToString()))
                {
                    rowTmp["出荷備考"] = dt.Rows[i]["S_BIKOU"].ToString();
                }

                csvDT.Rows.Add(rowTmp);
            }

            // 一覧に明細行がない場合、アラートを表示し、CSV出力処理はしない
            if (csvDT.Rows.Count == 0)
            {
                this.form.messageShowLogic.MessageBoxShow("E044");
                return;
            }
            // 出力先指定のポップアップを表示させる。
            if (this.form.messageShowLogic.MessageBoxShow("C013") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertDataTableToCsv(csvDT, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_DAINOU_ICHIRANHYOU), this.form);
            }

        }

        #region [F7]表示ボタン押下
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public void Print()
        {
            var dao = DaoInitUtility.GetComponent<DainoMeisaiDao>();
            SearchParameterDto condition = new SearchParameterDto();
            condition.KYOTEN_CD = Convert.ToInt16(this.form.KYOTEN_CD.Text);
            condition.DATE_FROM = Convert.ToDateTime(this.form.DATE_FROM.Value);
            condition.DATE_TO = Convert.ToDateTime(this.form.DATE_TO.Value);
            condition.UKEIRE_TORI_CD_FROM = this.form.UKEIRE_TORI_CD_FROM.Text;
            condition.UKEIRE_TORI_CD_TO = this.form.UKEIRE_TORI_CD_TO.Text;
            if (!string.IsNullOrEmpty(this.form.UKEIRE_GYOSHA_CD_FROM.Text))
            {
                condition.UKEIRE_GYOUSHA_CD_FROM = this.form.UKEIRE_GYOSHA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UKEIRE_GYOSHA_CD_TO.Text))
            {
                condition.UKEIRE_GYOUSHA_CD_TO = this.form.UKEIRE_GYOSHA_CD_TO.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UKEIRE_GENBA_CD_FROM.Text))
            {
                condition.UKEIRE_GENBA_CD_FROM = this.form.UKEIRE_GENBA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UKEIRE_GENBA_CD_TO.Text))
            {
                condition.UKEIRE_GENBA_CD_TO = this.form.UKEIRE_GENBA_CD_TO.Text;
            }
            condition.SHUKKA_TORI_CD_FROM = this.form.SHUKKA_TORI_CD_FROM.Text;
            condition.SHUKKA_TORI_CD_TO = this.form.SHUKKA_TORI_CD_TO.Text;
            if (!string.IsNullOrEmpty(this.form.SHUKKA_GYOSHA_CD_FROM.Text))
            {
                condition.SHUKKA_GYOUSHA_CD_FROM = this.form.SHUKKA_GYOSHA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHUKKA_GYOSHA_CD_TO.Text))
            {
                condition.SHUKKA_GYOUSHA_CD_TO = this.form.SHUKKA_GYOSHA_CD_TO.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHUKKA_GENBA_CD_FROM.Text))
            {
                condition.SHUKKA_GENBA_CD_FROM = this.form.SHUKKA_GENBA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHUKKA_GENBA_CD_TO.Text))
            {
                condition.SHUKKA_GENBA_CD_TO = this.form.SHUKKA_GENBA_CD_TO.Text;
            }
            DataTable dt = dao.GetData(condition);
            DataTable result = new DataTable();
            result.Columns.Add("UR_SH_NUMBER");
            result.Columns.Add("DENPYOU_DATE");
            result.Columns.Add("SHIHARAI_DATE");
            result.Columns.Add("URIAGE_DATE");
            result.Columns.Add("UPN_GYOUSHA_CD");
            result.Columns.Add("UPN_GYOUSHA_NAME");
            result.Columns.Add("SHARYOU_CD");
            result.Columns.Add("SHARYOU_NAME");
            result.Columns.Add("ROW_NO");
            result.Columns.Add("U_TORI_CD");
            result.Columns.Add("U_TORI_NAME");
            result.Columns.Add("U_GYOUSHA_CD");
            result.Columns.Add("U_GYOUSHA_NAME");
            result.Columns.Add("U_GENBA_CD");
            result.Columns.Add("U_GENBA_NAME");
            result.Columns.Add("U_HINMEI_CD");
            result.Columns.Add("U_HINMEI_NAME");
            result.Columns.Add("U_SHOMI");
            result.Columns.Add("U_CHOUSEI");
            result.Columns.Add("U_JITUSHOMI");
            result.Columns.Add("U_NUMBER");
            result.Columns.Add("U_UNIT");
            result.Columns.Add("U_TANKA");
            result.Columns.Add("U_KINGAKU");
            result.Columns.Add("U_BIKOU");
            result.Columns.Add("S_TORI_CD");
            result.Columns.Add("S_TORI_NAME");
            result.Columns.Add("S_GYOUSHA_CD");
            result.Columns.Add("S_GYOUSHA_NAME");
            result.Columns.Add("S_GENBA_CD");
            result.Columns.Add("S_GENBA_NAME");
            result.Columns.Add("S_HINMEI_CD");
            result.Columns.Add("S_HINMEI_NAME");
            result.Columns.Add("S_SHOMI");
            result.Columns.Add("S_CHOUSEI");
            result.Columns.Add("S_JITUSHOMI");
            result.Columns.Add("S_NUMBER");
            result.Columns.Add("S_UNIT");
            result.Columns.Add("S_TANKA");
            result.Columns.Add("S_KINGAKU");
            result.Columns.Add("S_BIKOU");
            result.Columns.Add("U_JITUSHOMI_SUM");
            result.Columns.Add("S_JITUSHOMI_SUM");
            result.Columns.Add("DIFF_JITUSHOMI");
            result.Columns.Add("U_KINGAKU_SUM");
            result.Columns.Add("S_KINGAKU_SUM");
            result.Columns.Add("DIFF_KINGAKU");
            result.Columns.Add("U_JITUSHOMI_TOTAL");
            result.Columns.Add("S_JITUSHOMI_TOTAL");
            result.Columns.Add("DIFF_JITUSHOMI_TOTAL");
            result.Columns.Add("U_KINGAKU_TOTAL");
            result.Columns.Add("S_KINGAKU_TOTAL");
            result.Columns.Add("DIFF_KINGAKU_TOTAL");
            if (dt == null || dt.Rows.Count == 0)
            {
                this.form.messageShowLogic.MessageBoxShow("C001");
                return;
            }
            else
            {
                decimal ujt = 0;
                decimal sjt = 0;
                decimal djt = 0;
                decimal ukt = 0;
                decimal skt = 0;
                decimal dkt = 0;
                long ur_sh_nm = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow resultDr = result.NewRow();
                    resultDr["UR_SH_NUMBER"] = dr["UR_SH_NUMBER"];
                    resultDr["DENPYOU_DATE"] = dr["DENPYOU_DATE"];
                    resultDr["SHIHARAI_DATE"] = dr["SHIHARAI_DATE"];
                    resultDr["URIAGE_DATE"] = dr["URIAGE_DATE"];
                    resultDr["UPN_GYOUSHA_CD"] = dr["UPN_GYOUSHA_CD"];
                    resultDr["UPN_GYOUSHA_NAME"] = dr["UPN_GYOUSHA_NAME"];
                    resultDr["SHARYOU_CD"] = dr["SHARYOU_CD"];
                    resultDr["SHARYOU_NAME"] = dr["SHARYOU_NAME"];
                    resultDr["ROW_NO"] = dr["ROW_NO"];
                    resultDr["U_TORI_CD"] = dr["U_TORI_CD"];
                    resultDr["U_TORI_NAME"] = dr["U_TORI_NAME"];
                    resultDr["U_GYOUSHA_CD"] = dr["U_GYOUSHA_CD"];
                    resultDr["U_GYOUSHA_NAME"] = dr["U_GYOUSHA_NAME"];
                    resultDr["U_GENBA_CD"] = dr["U_GENBA_CD"];
                    resultDr["U_GENBA_NAME"] = dr["U_GENBA_NAME"];
                    resultDr["U_HINMEI_CD"] = dr["U_HINMEI_CD"];
                    resultDr["U_HINMEI_NAME"] = dr["U_HINMEI_NAME"];
                    resultDr["U_SHOMI"] = this.SetFormat(Convert.ToString(dr["U_SHOMI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                    resultDr["U_CHOUSEI"] = this.SetFormat(Convert.ToString(dr["U_CHOUSEI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                    resultDr["U_JITUSHOMI"] = this.SetFormat(Convert.ToString(dr["U_JITUSHOMI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                    resultDr["U_NUMBER"] = this.SetFormat(Convert.ToString(dr["U_NUMBER"]), this.mSysInfo.SYS_SUURYOU_FORMAT);
                    resultDr["U_UNIT"] = dr["U_UNIT"];
                    resultDr["U_TANKA"] = this.SetFormat(Convert.ToString(dr["U_TANKA"]), this.mSysInfo.SYS_TANKA_FORMAT);
                    resultDr["U_KINGAKU"] = this.SetFormat(Convert.ToString(dr["U_KINGAKU"]), "#,##0");
                    resultDr["U_BIKOU"] = dr["U_BIKOU"];
                    resultDr["S_TORI_CD"] = dr["S_TORI_CD"];
                    resultDr["S_TORI_NAME"] = dr["S_TORI_NAME"];
                    resultDr["S_GYOUSHA_CD"] = dr["S_GYOUSHA_CD"];
                    resultDr["S_GYOUSHA_NAME"] = dr["S_GYOUSHA_NAME"];
                    resultDr["S_GENBA_CD"] = dr["S_GENBA_CD"];
                    resultDr["S_GENBA_NAME"] = dr["S_GENBA_NAME"];
                    resultDr["S_HINMEI_CD"] = dr["S_HINMEI_CD"];
                    resultDr["S_HINMEI_NAME"] = dr["S_HINMEI_NAME"];
                    resultDr["S_SHOMI"] = this.SetFormat(Convert.ToString(dr["S_SHOMI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                    resultDr["S_CHOUSEI"] = this.SetFormat(Convert.ToString(dr["S_CHOUSEI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                    resultDr["S_JITUSHOMI"] = this.SetFormat(Convert.ToString(dr["S_JITUSHOMI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                    resultDr["S_NUMBER"] = this.SetFormat(Convert.ToString(dr["S_NUMBER"]), this.mSysInfo.SYS_SUURYOU_FORMAT);
                    resultDr["S_UNIT"] = dr["S_UNIT"];
                    resultDr["S_TANKA"] = this.SetFormat(Convert.ToString(dr["S_TANKA"]), this.mSysInfo.SYS_TANKA_FORMAT);
                    resultDr["S_KINGAKU"] = this.SetFormat(Convert.ToString(dr["S_KINGAKU"]), "#,##0");
                    resultDr["S_BIKOU"] = dr["S_BIKOU"];
                    resultDr["U_JITUSHOMI_SUM"] = this.SetFormat(Convert.ToString(dr["U_JITUSHOMI_SUM"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                    resultDr["S_JITUSHOMI_SUM"] = this.SetFormat(Convert.ToString(dr["S_JITUSHOMI_SUM"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                    resultDr["DIFF_JITUSHOMI"] = this.SetFormat(Convert.ToString(dr["DIFF_JITUSHOMI"]), this.mSysInfo.SYS_JYURYOU_FORMAT);
                    resultDr["U_KINGAKU_SUM"] = this.SetFormat(Convert.ToString(dr["U_KINGAKU_SUM"]), "#,##0");
                    resultDr["S_KINGAKU_SUM"] = this.SetFormat(Convert.ToString(dr["S_KINGAKU_SUM"]), "#,##0");
                    resultDr["DIFF_KINGAKU"] = this.SetFormat(Convert.ToString(dr["DIFF_KINGAKU"]), "#,##0");
                    if (ur_sh_nm != Convert.ToInt64(dr["UR_SH_NUMBER"]))
                    {
                        ujt += this.ToNDecimal(dr["U_JITUSHOMI_SUM"]) ?? 0;
                        sjt += this.ToNDecimal(dr["S_JITUSHOMI_SUM"]) ?? 0;
                        djt += this.ToNDecimal(dr["DIFF_JITUSHOMI"]) ?? 0;
                        ukt += this.ToNDecimal(dr["U_KINGAKU_SUM"]) ?? 0;
                        skt += this.ToNDecimal(dr["S_KINGAKU_SUM"]) ?? 0;
                        dkt += this.ToNDecimal(dr["DIFF_KINGAKU"]) ?? 0;
                        ur_sh_nm = Convert.ToInt64(dr["UR_SH_NUMBER"]);
                    }
                    result.Rows.Add(resultDr);
                }
                result.Rows[result.Rows.Count - 1]["U_JITUSHOMI_TOTAL"] = this.SetFormat(Convert.ToString(ujt), this.mSysInfo.SYS_JYURYOU_FORMAT);
                result.Rows[result.Rows.Count - 1]["S_JITUSHOMI_TOTAL"] = this.SetFormat(Convert.ToString(sjt), this.mSysInfo.SYS_JYURYOU_FORMAT);
                result.Rows[result.Rows.Count - 1]["DIFF_JITUSHOMI_TOTAL"] = this.SetFormat(Convert.ToString(djt), this.mSysInfo.SYS_JYURYOU_FORMAT);
                result.Rows[result.Rows.Count - 1]["U_KINGAKU_TOTAL"] = this.SetFormat(Convert.ToString(ukt), "#,##0");
                result.Rows[result.Rows.Count - 1]["S_KINGAKU_TOTAL"] = this.SetFormat(Convert.ToString(skt), "#,##0");
                result.Rows[result.Rows.Count - 1]["DIFF_KINGAKU_TOTAL"] = this.SetFormat(Convert.ToString(dkt), "#,##0");
            }
            DataTable head = new DataTable();
            head.Columns.Add("FH_JISHA_NAME");
            head.Columns.Add("FH_KYOTEN_NAME");
            head.Columns.Add("FH_DATE_FR");
            head.Columns.Add("FH_DATE_TO");
            head.Columns.Add("FH_U_TORI_CD_FR");
            head.Columns.Add("FH_U_TORI_NM_FR");
            head.Columns.Add("FH_U_TORI_CD_TO");
            head.Columns.Add("FH_U_TORI_NM_TO");
            head.Columns.Add("FH_U_GYOUSHA_CD_FR");
            head.Columns.Add("FH_U_GYOUSHA_NM_FR");
            head.Columns.Add("FH_U_GYOUSHA_CD_TO");
            head.Columns.Add("FH_U_GYOUSHA_NM_TO");
            head.Columns.Add("FH_U_GENBA_CD_FR");
            head.Columns.Add("FH_U_GENBA_NM_FR");
            head.Columns.Add("FH_U_GENBA_CD_TO");
            head.Columns.Add("FH_U_GENBA_NM_TO");
            head.Columns.Add("FH_S_TORI_CD_FR");
            head.Columns.Add("FH_S_TORI_NM_FR");
            head.Columns.Add("FH_S_TORI_CD_TO");
            head.Columns.Add("FH_S_TORI_NM_TO");
            head.Columns.Add("FH_S_GYOUSHA_CD_FR");
            head.Columns.Add("FH_S_GYOUSHA_NM_FR");
            head.Columns.Add("FH_S_GYOUSHA_CD_TO");
            head.Columns.Add("FH_S_GYOUSHA_NM_TO");
            head.Columns.Add("FH_S_GENBA_CD_FR");
            head.Columns.Add("FH_S_GENBA_NM_FR");
            head.Columns.Add("FH_S_GENBA_CD_TO");
            head.Columns.Add("FH_S_GENBA_NM_TO");
            DataRow headDr = head.NewRow();
            M_CORP_INFO[] results = this.GetJishaJouhou();
            if (results != null && results.Length > 0)
            {
                headDr["FH_JISHA_NAME"] = results[0].CORP_RYAKU_NAME;
            }
            else
            {
                headDr["FH_JISHA_NAME"] = "";
            }
            headDr["FH_KYOTEN_NAME"] = this.form.KYOTEN_NAME.Text;
            headDr["FH_DATE_FR"] = this.ToNDateTime(this.form.DATE_FROM.Value) == null ? "" : this.ToNDateTime(this.form.DATE_FROM.Value).Value.ToString("yyyy/MM/dd");
            headDr["FH_DATE_TO"] = this.ToNDateTime(this.form.DATE_TO.Value) == null ? "" : this.ToNDateTime(this.form.DATE_TO.Value).Value.ToString("yyyy/MM/dd");
            headDr["FH_U_TORI_CD_FR"] = this.form.UKEIRE_TORI_CD_FROM.Text;
            headDr["FH_U_TORI_NM_FR"] = this.form.UKEIRE_TORI_NAME_FROM.Text;
            headDr["FH_U_TORI_CD_TO"] = this.form.UKEIRE_TORI_CD_TO.Text;
            headDr["FH_U_TORI_NM_TO"] = this.form.UKEIRE_TORI_NAME_TO.Text;
            headDr["FH_U_GYOUSHA_CD_FR"] = this.form.UKEIRE_GYOSHA_CD_FROM.Text;
            headDr["FH_U_GYOUSHA_NM_FR"] = this.form.UKEIRE_GYOSHA_NAME_FROM.Text;
            headDr["FH_U_GYOUSHA_CD_TO"] = this.form.UKEIRE_GYOSHA_CD_TO.Text;
            headDr["FH_U_GYOUSHA_NM_TO"] = this.form.UKEIRE_GYOSHA_NAME_TO.Text;
            headDr["FH_U_GENBA_CD_FR"] = this.form.UKEIRE_GENBA_CD_FROM.Text;
            headDr["FH_U_GENBA_NM_FR"] = this.form.UKEIRE_GENBA_NAME_FROM.Text;
            headDr["FH_U_GENBA_CD_TO"] = this.form.UKEIRE_GENBA_CD_TO.Text;
            headDr["FH_U_GENBA_NM_TO"] = this.form.UKEIRE_GENBA_NAME_TO.Text;
            headDr["FH_S_TORI_CD_FR"] = this.form.SHUKKA_TORI_CD_FROM.Text;
            headDr["FH_S_TORI_NM_FR"] = this.form.SHUKKA_TORI_NAME_FROM.Text;
            headDr["FH_S_TORI_CD_TO"] = this.form.SHUKKA_TORI_CD_TO.Text;
            headDr["FH_S_TORI_NM_TO"] = this.form.SHUKKA_TORI_NAME_TO.Text;
            headDr["FH_S_GYOUSHA_CD_FR"] = this.form.SHUKKA_GYOSHA_CD_FROM.Text;
            headDr["FH_S_GYOUSHA_NM_FR"] = this.form.SHUKKA_GYOSHA_NAME_FROM.Text;
            headDr["FH_S_GYOUSHA_CD_TO"] = this.form.SHUKKA_GYOSHA_CD_TO.Text;
            headDr["FH_S_GYOUSHA_NM_TO"] = this.form.SHUKKA_GYOSHA_NAME_TO.Text;
            headDr["FH_S_GENBA_CD_FR"] = this.form.SHUKKA_GENBA_CD_FROM.Text;
            headDr["FH_S_GENBA_NM_FR"] = this.form.SHUKKA_GENBA_NAME_FROM.Text;
            headDr["FH_S_GENBA_CD_TO"] = this.form.SHUKKA_GENBA_CD_TO.Text;
            headDr["FH_S_GENBA_NM_TO"] = this.form.SHUKKA_GENBA_NAME_TO.Text;
            head.Rows.Add(headDr);
            // 印刷シーケンスの開始
            if (!ContinuousPrinting.Begin())
            {
                return;
            }
            bool isAbortRequired = false;
            ReportInfoR403 report = new ReportInfoR403();
            report.R403_Report(result, head);
            // XPSプロパティ - タイトル(取引先も表示させる)
            report.Title = "代納明細表";
            FormReport formReport = new FormReport(report, "R403");
            formReport.Caption = "代納明細表";
            // 印刷アプリ初期動作(プレビュー)
            formReport.PrintInitAction = 2;
            formReport.PrintXPS();
            // 印刷画面から中止要求があれば中断
            if (ContinuousPrinting.IsAbortRequired)
            {
                isAbortRequired = true;
                return;
            }
            // 印刷シーケンスの終了
            ContinuousPrinting.End(isAbortRequired);
        }

        /// <summary> 指定フォーマットに変換した文字列を取得する </summary>
        /// <param name="value">変換対象を表す文字列</param>
        /// <param name="format">指定フォーマットを表す文字列</param>
        /// <returns>指定フォーマットに変換後文字列</returns>
        private string SetFormat(string value, string format)
        {
            // フォーマット変換後文字列
            string ret = string.Empty;

            decimal temp = 0;

            // 引数の文字列が数値変換可能か
            if (decimal.TryParse(value, out temp))
            {
                // 数値変換できた場合は指定フォーマットの文字列に変換する
                ret = string.Format(temp.ToString(format));
            }
            return ret;
        }
        #endregion
    }
}
