using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Stock.ZaikoKanriHyo.Accessor;
using Shougun.Core.Stock.ZaikoKanriHyo.Const;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Stock.ZaikoKanriHyo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class PopupLogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// Form
        /// </summary>
        private PopupForm form;
        /// <summary>
        /// 画面に表示しているすべてのコントロールを格納するフィールド
        /// </summary>
        private Control[] allControl;
        /// <summary>
        /// チェック対象コントロール
        /// </summary>
        public ICustomControl CheckControl { get; set; }
        /// <summary>
        /// DBAccessor
        /// </summary>
        public DBAccessor dba;
        private IM_GYOUSHADao gyoushaDao;
        private IM_GENBADao genbaDao;
        private IM_ZAIKO_HINMEIDao zaikoHinmeiDao;
        private MessageBoxShowLogic MsgBox;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">対象フォーム</param>
        internal PopupLogicClass(PopupForm targetForm)
        {
            // フィールドの初期化
            this.form = targetForm;
            this.dba = new DBAccessor();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.zaikoHinmeiDao = DaoInitUtility.GetComponent<IM_ZAIKO_HINMEIDao>();
            this.MsgBox = new MessageBoxShowLogic();
        }

 		/// <summary>
		/// 画面初期化
		/// </summary>
		/// <param name="Joken">範囲条件情報</param>
        internal bool WindowInit(UIConstans.ConditionInfo Joken)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(Joken);

                // イベントの初期化処理
                this.EventInit();

                // 画面表示項目初期化
                this.form.DATE_FROM.Value = Joken.DateFrom;
                this.form.DATE_TO.Value = Joken.DateTo;
                this.form.OUTPUT_KBN.Text = Joken.OutPutKBN.ToString();

                // 業者CDから名前をSet
                this.form.GYOUSHA_CD_FROM.Text = Joken.GyoushaCdFrom;
                this.form.GYOUSHA_CD_TO.Text = Joken.GyoushaCdTo;
                if (!string.IsNullOrEmpty(Joken.GyoushaCdFrom))
                {
                    // 業者CDが設定されている場合、略称名のセット
                    this.form.GYOUSHA_NAME_FROM.Text = dba.GetGyoushaName(Joken.GyoushaCdFrom);
                }
                if (!string.IsNullOrEmpty(Joken.GyoushaCdTo))
                {
                    // 業者CDが設定されている場合、略称名のセット
                    this.form.GYOUSHA_NAME_TO.Text = dba.GetGyoushaName(Joken.GyoushaCdTo);
                }

                // 現場CDから名前をSet
                if (Joken.GyoushaCdFrom == Joken.GyoushaCdTo && Joken.GyoushaCdFrom != "")
                {
                    this.form.GENBA_CD_FROM.Enabled = true;
                    this.form.GENBA_POPUP_FROM.Enabled = true;
                    this.form.GENBA_CD_TO.Enabled = true;
                    this.form.GENBA_POPUP_TO.Enabled = true;
                    this.form.GENBA_CD_FROM.Text = Joken.GenbaCdFrom;
                    this.form.GENBA_CD_TO.Text = Joken.GenbaCdTo;
                    if (!string.IsNullOrEmpty(Joken.GenbaCdFrom))
                    {
                        // 現場CDが設定されている場合、略称名のセット
                        this.form.GENBA_NAME_FROM.Text = dba.GetGenbaName(Joken.GyoushaCdFrom, Joken.GenbaCdFrom);
                    }
                    if (!string.IsNullOrEmpty(Joken.GenbaCdTo))
                    {
                        // 現場CDが設定されている場合、略称名のセット
                        this.form.GENBA_NAME_TO.Text = dba.GetGenbaName(Joken.GyoushaCdFrom, Joken.GenbaCdTo);
                    }
                }
                else
                {
                    this.form.GENBA_CD_FROM.Enabled = false;
                    this.form.GENBA_POPUP_FROM.Enabled = false;
                    this.form.GENBA_CD_TO.Enabled = false;
                    this.form.GENBA_POPUP_TO.Enabled = false;
                }

                // 在庫品名CDから名前をSet
                this.form.ZAIKO_HINMEI_CD_FROM.Text = Joken.ZaikoHinmeiCdFrom;
                this.form.ZAIKO_HINMEI_CD_TO.Text = Joken.ZaikoHinmeiCdTo;
                if (!string.IsNullOrEmpty(Joken.ZaikoHinmeiCdFrom))
                {
                    // 在庫品名CDが設定されている場合、略称名のセット
                    this.form.ZAIKO_HINMEI_NAME_FROM.Text = dba.GetZaikoHinmeiName(Joken.ZaikoHinmeiCdFrom);
                }
                if (!string.IsNullOrEmpty(Joken.ZaikoHinmeiCdTo))
                {
                    // 在庫品名CDが設定されている場合、略称名のセット
                    this.form.ZAIKO_HINMEI_NAME_TO.Text = dba.GetZaikoHinmeiName(Joken.ZaikoHinmeiCdTo);
                }
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // UIFormキーイベント生成
            this.form.KeyUp += new KeyEventHandler(this.form.ItemKeyUp);

            // 前月ボタン(F1)イベント生成
            this.form.btn_zengetsu.Click += new EventHandler(this.form.Function1Click);

            // 次月ボタン(F2)イベント生成
            this.form.btn_jigetsu.Click += new EventHandler(this.form.Function2Click);

            // 検索実行ボタン(F9)イベント生成
            this.form.btn_kensakujikkou.DialogResult = DialogResult.OK;
            this.form.btn_kensakujikkou.Click += new EventHandler(this.form.SearchExec);

            // キャンセルボタン(F12)イベント生成
            this.form.btn_cancel.DialogResult = DialogResult.Cancel;
            this.form.btn_cancel.Click += new EventHandler(this.form.FormClose);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 必須チェック実行
        /// </summary>
        /// <returns name="bool">エラー発生</returns>
        internal bool RegistCheck()
        {
            /*************************************************/
            /**		元帳PopUpはヘッダやフッタを使用しない	**/
            /**		単一フォームであるため、必須チェックの	**/
            /**		トリガを自前で発生させる				**/
            /*************************************************/
            bool Error = false;

            try
            {
                var CtrlUtil = new ControlUtility();
                var MsgList = new StringBuilder();
                Validator ValidLogic;
                string result;
                if (this.allControl == null)
                {
                    // 画面に表示しているすべてのControlを取得
                    List<Control> allCtl = new List<Control>();
                    allCtl.AddRange(CtrlUtil.GetAllControls(this.form));
                    // コントロールが下からセットされるため反転させる
                    allCtl.Reverse();
                    this.allControl = allCtl.ToArray();
                }
                foreach (var c in allControl)
                {
                    // 必須チェックが登録されているControlのみを抽出
                    this.CheckControl = c as ICustomControl;
                    if (this.CheckControl != null)
                    {
                        var mthodList = this.CheckControl.RegistCheckMethod;
                        if (mthodList != null && mthodList.Count != 0)
                        {
                            var check = new CheckMethodSetting();
                            foreach (var checkMethodName in mthodList)
                            {
                                var methodSetting = check[checkMethodName.CheckMethodName];
                                if (methodSetting.MethodName == "MandatoryCheck")
                                {
                                    // 必須チェック実行
                                    ValidLogic = new Validator(this.CheckControl, null);
                                    result = ValidLogic.MandatoryCheck();
                                    if (result.Length != 0)
                                    {
                                        // 入力されていないものがある場合はMessageListに登録
                                        MsgList.AppendLine(result);
                                    }
                                }
                            }
                        }
                    }
                }

                if (MsgList.Length != 0)
                {
                    // エラー表示
                    MessageBox.Show(MsgList.ToString(), Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Error = true;
                }
                else
                {
                    //業者CDの大小チェック
                    if (0 < string.Compare(this.form.GYOUSHA_CD_FROM.Text, this.form.GYOUSHA_CD_TO.Text))
                    {
                        this.form.GYOUSHA_CD_FROM.BackColor = Constans.ERROR_COLOR;
                        this.form.GYOUSHA_CD_TO.BackColor = Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E032", "業者From", "業者To");
                        Error = true;
                        this.form.GYOUSHA_CD_FROM.Focus();
                        return Error;
                    }
                    //現場CDの大小チェック
                    if (0 < string.Compare(this.form.GENBA_CD_FROM.Text, this.form.GENBA_CD_TO.Text))
                    {
                        this.form.GENBA_CD_FROM.BackColor = Constans.ERROR_COLOR;
                        this.form.GENBA_CD_TO.BackColor = Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E032", "現場From", "現場To");
                        Error = true;
                        this.form.GENBA_CD_FROM.Focus();
                        return Error;
                    }
                    //在庫品名CDの大小チェック
                    if (0 < string.Compare(this.form.ZAIKO_HINMEI_CD_FROM.Text, this.form.ZAIKO_HINMEI_CD_TO.Text))
                    {
                        this.form.ZAIKO_HINMEI_CD_FROM.BackColor = Constans.ERROR_COLOR;
                        this.form.ZAIKO_HINMEI_CD_TO.BackColor = Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E032", "在庫品名From", "在庫品名To");
                        Error = true;
                        this.form.ZAIKO_HINMEI_CD_FROM.Focus();
                        return Error;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                Error = true;
            }
            return Error;
        }

        #region "[F1]前月、[F2]次月押下時"
        /// <summary>
        /// 前月日付設定
        /// </summary>
        public void SetDatePreviousMonth(out object valDateFrom, out object valDateTo, bool isNextMonth, out bool catchErr)
        {
            valDateFrom = null;
            valDateTo = null;
            catchErr = false;

            try
            {
                LogUtility.DebugMethodStart(valDateFrom, valDateTo, isNextMonth);

                int monthFlg = 0;

                if (isNextMonth)
                {
                    monthFlg = 1;
                }
                else
                {
                    monthFlg = -1;
                }

                if (this.form.DATE_FROM.Value != null)
                {
                    DateTime dateFrom = (DateTime)this.form.DATE_FROM.Value;
                    valDateFrom = (object)dateFrom.AddMonths(monthFlg);
                }

                if (this.form.DATE_TO.Value != null)
                {
                    DateTime dateTo = (DateTime)this.form.DATE_TO.Value;
                    dateTo = dateTo.AddMonths(monthFlg);

                    valDateTo = new DateTime(dateTo.Year, dateTo.Month, DateTime.DaysInMonth(dateTo.Year, dateTo.Month));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDatePreviousMonth", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(valDateFrom, valDateTo, isNextMonth, catchErr);
            }
        }
        #endregion "[F1]前月、[F2]次月押下時"

        /// <summary>
        /// 設定値保存
        /// </summary>
        internal void SaveParams()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 設定条件を保存
                var info = new UIConstans.ConditionInfo();

                DateTime dateFrom = DateTime.Parse(this.form.DATE_FROM.Text + "00:00:00");
                DateTime dateTo = DateTime.Parse(this.form.DATE_TO.Text + "23:59:59");
                info.OutPutKBN = int.Parse(this.form.OUTPUT_KBN.Text);		// 出力区分
                info.DateFrom = dateFrom;	                                            // 開始日付
                info.DateTo = dateTo;			                                        // 終了日付
                info.GyoushaCdFrom = this.form.GYOUSHA_CD_FROM.Text;		            // 開始業者CD
                info.GyoushaCdTo = this.form.GYOUSHA_CD_TO.Text;            			// 終了業者CD
                info.GenbaCdFrom = this.form.GENBA_CD_FROM.Text;		                // 開始現場CD
                info.GenbaCdTo = this.form.GENBA_CD_TO.Text;			                // 終了現場CD
                info.ZaikoHinmeiCdFrom = this.form.ZAIKO_HINMEI_CD_FROM.Text;		    // 開始在庫品名CD
                info.ZaikoHinmeiCdTo = this.form.ZAIKO_HINMEI_CD_TO.Text;		    	// 終了在庫品名CD
                info.DataSetFlag = true;												// 値格納フラグ
                this.form.Joken = info;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaveParams", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 業者CD MINを取得
        /// </summary>
        /// <returns></returns>
        public string GetGyoushaCdMin()
        {
            string cd = "";
            var dt = gyoushaDao.GetDateForStringSql("SELECT MIN(GYOUSHA_CD) as GYOUSHA_CD FROM M_GYOUSHA " +
            " LEFT JOIN M_TORIHIKISAKI ON M_GYOUSHA.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD " +
            " LEFT JOIN M_TODOUFUKEN ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = M_TODOUFUKEN.TODOUFUKEN_CD " +
            " AND M_TODOUFUKEN.DELETE_FLG = 0  WHERE M_GYOUSHA.JISHA_KBN = '1'");
            if (dt != null && dt.Rows.Count > 0)
            {
                cd = Convert.ToString(dt.Rows[0][0]);
            }
            return cd;
        }

        /// <summary>
        /// 業者CD MAXを取得
        /// </summary>
        /// <returns></returns>
        public string GetGyoushaCdMax()
        {
            string cd = "";
            var dt = gyoushaDao.GetDateForStringSql("SELECT MAX(GYOUSHA_CD) as GYOUSHA_CD FROM M_GYOUSHA " +
            " LEFT JOIN M_TORIHIKISAKI ON M_GYOUSHA.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD " +
            " LEFT JOIN M_TODOUFUKEN ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = M_TODOUFUKEN.TODOUFUKEN_CD " +
            " AND M_TODOUFUKEN.DELETE_FLG = 0  WHERE M_GYOUSHA.JISHA_KBN = '1'");
            if (dt != null && dt.Rows.Count > 0)
            {
                cd = Convert.ToString(dt.Rows[0][0]);
            }
            return cd;
        }

        /// <summary>
        /// 現場CD MINを取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public string GetGenbaCdMin(string gyoushaCd)
        {
            string cd = "";
            string sql = "SELECT MIN(GENBA_CD) as GENBA_CD FROM M_GENBA " +
                " LEFT JOIN M_GYOUSHA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD " +
                " LEFT JOIN M_TODOUFUKEN ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = M_TODOUFUKEN.TODOUFUKEN_CD AND M_TODOUFUKEN.DELETE_FLG = 0" +
                " LEFT JOIN M_TODOUFUKEN M_TODOUFUKEN2 ON M_GENBA.GENBA_TODOUFUKEN_CD = M_TODOUFUKEN2.TODOUFUKEN_CD AND M_TODOUFUKEN2.DELETE_FLG = 0 " +
                " LEFT JOIN M_TORIHIKISAKI ON M_TORIHIKISAKI.TORIHIKISAKI_CD = M_GYOUSHA.TORIHIKISAKI_CD " +
                " WHERE M_GENBA.JISHA_KBN = '1' AND M_GYOUSHA.JISHA_KBN = '1' AND M_GENBA.GYOUSHA_CD = '{0}'";
            var dt = genbaDao.GetDateForStringSql(string.Format(sql, gyoushaCd));
            if (dt != null && dt.Rows.Count > 0)
            {
                cd = Convert.ToString(dt.Rows[0][0]);
            }
            return cd;
        }
        /// <summary>
        /// 現場CD MAXを取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public string GetGenbaCdMax(string gyoushaCd)
        {
            string cd = "";
            string sql = "SELECT MAX(GENBA_CD) as GENBA_CD FROM M_GENBA " +
                " LEFT JOIN M_GYOUSHA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD " +
                " LEFT JOIN M_TODOUFUKEN ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = M_TODOUFUKEN.TODOUFUKEN_CD AND M_TODOUFUKEN.DELETE_FLG = 0" +
                " LEFT JOIN M_TODOUFUKEN M_TODOUFUKEN2 ON M_GENBA.GENBA_TODOUFUKEN_CD = M_TODOUFUKEN2.TODOUFUKEN_CD AND M_TODOUFUKEN2.DELETE_FLG = 0 " +
                " LEFT JOIN M_TORIHIKISAKI ON M_TORIHIKISAKI.TORIHIKISAKI_CD = M_GYOUSHA.TORIHIKISAKI_CD " +
                " WHERE M_GENBA.JISHA_KBN = '1' AND M_GYOUSHA.JISHA_KBN = '1' AND M_GENBA.GYOUSHA_CD = '{0}'";
            var dt = genbaDao.GetDateForStringSql(string.Format(sql, gyoushaCd));
            if (dt != null && dt.Rows.Count > 0)
            {
                cd = Convert.ToString(dt.Rows[0][0]);
            }
            return cd;
        }

        /// <summary>
        /// 在庫品名CD MINを取得
        /// </summary>
        /// <returns></returns>
        public string GetZaikoHinmeiCdMin()
        {
            string cd = "";
            string sql = "SELECT MIN(ZAIKO_HINMEI_CD) as ZAIKO_HINMEI_CD FROM M_ZAIKO_HINMEI";
            var dt = genbaDao.GetDateForStringSql(string.Format(sql));
            if (dt != null && dt.Rows.Count > 0)
            {
                cd = Convert.ToString(dt.Rows[0][0]);
            }
            return cd;
        }
        /// <summary>
        /// 在庫品名CD MAXを取得
        /// </summary>
        /// <returns></returns>
        public string GetZaikoHinmeiCdMax()
        {
            string cd = "";
            string sql = "SELECT MAX(ZAIKO_HINMEI_CD) as ZAIKO_HINMEI_CD FROM M_ZAIKO_HINMEI";
            var dt = genbaDao.GetDateForStringSql(string.Format(sql));
            if (dt != null && dt.Rows.Count > 0)
            {
                cd = Convert.ToString(dt.Rows[0][0]);
            }
            return cd;
        }

        public void GYOUSHA_CD_FROM_Validated()
        {
            try
            {
                var messageLogic = new MessageBoxShowLogic();

                if (this.form.preGyoushaCd != this.form.GYOUSHA_CD_FROM.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.GYOUSHA_CD_FROM.Text))
                    {
                        this.form.GYOUSHA_NAME_FROM.Text = "";
                        this.form.GENBA_CD_FROM.Text = "";
                        this.form.GENBA_NAME_FROM.Text = "";
                        this.form.GENBA_CD_TO.Text = "";
                        this.form.GENBA_NAME_TO.Text = "";
                        this.form.GENBA_CD_FROM.Enabled = false;
                        this.form.GENBA_POPUP_FROM.Enabled = false;
                        this.form.GENBA_CD_TO.Enabled = false;
                        this.form.GENBA_POPUP_TO.Enabled = false;
                        this.form.preGyoushaCd = "";
                        return;
                    }

                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha.GYOUSHA_CD = this.form.GYOUSHA_CD_FROM.Text;
                    gyousha.ISNOT_NEED_DELETE_FLG = true;
                    gyousha.JISHA_KBN = true;
                    gyousha = this.gyoushaDao.GetAllValidData(gyousha).FirstOrDefault();

                    if (gyousha == null)
                    {
                        this.form.isError = true;
                        this.form.GYOUSHA_CD_FROM.BackColor = Constans.ERROR_COLOR;
                        this.form.GYOUSHA_NAME_FROM.Text = "";
                        messageLogic.MessageBoxShow("E020", "業者");
                        this.form.GYOUSHA_CD_FROM.Focus();
                        return;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME_FROM.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    this.form.GENBA_CD_FROM.Text = "";
                    this.form.GENBA_NAME_FROM.Text = "";
                    this.form.GENBA_CD_TO.Text = "";
                    this.form.GENBA_NAME_TO.Text = "";

                    if (this.form.GYOUSHA_CD_FROM.Text != this.form.GYOUSHA_CD_TO.Text)
                    {
                        this.form.GENBA_CD_FROM.Enabled = false;
                        this.form.GENBA_POPUP_FROM.Enabled = false;
                        this.form.GENBA_CD_TO.Enabled = false;
                        this.form.GENBA_POPUP_TO.Enabled = false;
                    }
                    else
                    {
                        this.form.GENBA_CD_FROM.Enabled = true;
                        this.form.GENBA_POPUP_FROM.Enabled = true;
                        this.form.GENBA_CD_TO.Enabled = true;
                        this.form.GENBA_POPUP_TO.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_CD_FROM_Validated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }

        public void GYOUSHA_CD_TO_Validated()
        {
            try
            {
                var messageLogic = new MessageBoxShowLogic();

                if (this.form.preGyoushaCd != this.form.GYOUSHA_CD_TO.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.GYOUSHA_CD_TO.Text))
                    {
                        this.form.GYOUSHA_NAME_TO.Text = "";
                        this.form.GENBA_CD_FROM.Text = "";
                        this.form.GENBA_NAME_FROM.Text = "";
                        this.form.GENBA_CD_TO.Text = "";
                        this.form.GENBA_NAME_TO.Text = "";
                        this.form.GENBA_CD_FROM.Enabled = false;
                        this.form.GENBA_POPUP_FROM.Enabled = false;
                        this.form.GENBA_CD_TO.Enabled = false;
                        this.form.GENBA_POPUP_TO.Enabled = false;
                        this.form.preGyoushaCd = "";
                        if (this.form.GYOUSHA_CD_FROM.Text == this.form.GYOUSHA_CD_TO.Text)
                        {
                            this.form.GENBA_CD_FROM.Focus();
                        }
                        else
                        {
                            this.form.ZAIKO_HINMEI_CD_FROM.Focus();
                        }
                        return;
                    }

                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha.GYOUSHA_CD = this.form.GYOUSHA_CD_TO.Text;
                    gyousha.ISNOT_NEED_DELETE_FLG = true;
                    gyousha.JISHA_KBN = true;
                    gyousha = this.gyoushaDao.GetAllValidData(gyousha).FirstOrDefault();

                    if (gyousha == null)
                    {
                        this.form.isError = true;
                        this.form.GYOUSHA_CD_TO.BackColor = Constans.ERROR_COLOR;
                        this.form.GYOUSHA_NAME_TO.Text = "";
                        messageLogic.MessageBoxShow("E020", "業者");
                        this.form.GYOUSHA_CD_TO.Focus();
                        return;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME_TO.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    this.form.GENBA_CD_FROM.Text = "";
                    this.form.GENBA_NAME_FROM.Text = "";
                    this.form.GENBA_CD_TO.Text = "";
                    this.form.GENBA_NAME_TO.Text = "";

                    if (this.form.GYOUSHA_CD_FROM.Text != this.form.GYOUSHA_CD_TO.Text)
                    {
                        this.form.GENBA_CD_FROM.Enabled = false;
                        this.form.GENBA_POPUP_FROM.Enabled = false;
                        this.form.GENBA_CD_TO.Enabled = false;
                        this.form.GENBA_POPUP_TO.Enabled = false;
                    }
                    else
                    {
                        this.form.GENBA_CD_FROM.Enabled = true;
                        this.form.GENBA_POPUP_FROM.Enabled = true;
                        this.form.GENBA_CD_TO.Enabled = true;
                        this.form.GENBA_POPUP_TO.Enabled = true;
                    }

                    if (this.form.GYOUSHA_CD_FROM.Text == this.form.GYOUSHA_CD_TO.Text)
                    {
                        this.form.GENBA_CD_FROM.Focus();
                    }
                    else
                    {
                        this.form.ZAIKO_HINMEI_CD_FROM.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_CD_TO_Validated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }

        public void GENBA_CD_FROM_Validated()
        {
            try
            {
                var messageLogic = new MessageBoxShowLogic();

                if (this.form.preGenbaCd != this.form.GENBA_CD_FROM.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.GENBA_CD_FROM.Text))
                    {
                        this.form.GENBA_NAME_FROM.Text = "";
                        this.form.preGenbaCd = "";
                        return;
                    }

                    M_GENBA genba = new M_GENBA();
                    genba.GYOUSHA_CD = this.form.GYOUSHA_CD_FROM.Text;
                    genba.GENBA_CD = this.form.GENBA_CD_FROM.Text;
                    genba.ISNOT_NEED_DELETE_FLG = true;
                    genba.JISHA_KBN = true;
                    genba = this.genbaDao.GetAllValidData(genba).FirstOrDefault();

                    if (genba == null)
                    {
                        this.form.isError = true;
                        this.form.GENBA_CD_FROM.BackColor = Constans.ERROR_COLOR;
                        this.form.GENBA_NAME_FROM.Text = "";
                        messageLogic.MessageBoxShow("E020", "現場");
                        this.form.GENBA_CD_FROM.Focus();
                        return;
                    }
                    else
                    {
                        this.form.GENBA_NAME_FROM.Text = genba.GENBA_NAME_RYAKU;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_CD_FROM_Validated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }

        public void GENBA_CD_TO_Validated()
        {
            try
            {
                var messageLogic = new MessageBoxShowLogic();

                if (this.form.preGenbaCd != this.form.GENBA_CD_TO.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.GENBA_CD_TO.Text))
                    {
                        this.form.GENBA_NAME_TO.Text = "";
                        this.form.preGenbaCd = "";
                        return;
                    }

                    M_GENBA genba = new M_GENBA();
                    genba.GYOUSHA_CD = this.form.GYOUSHA_CD_FROM.Text;
                    genba.GENBA_CD = this.form.GENBA_CD_TO.Text;
                    genba.ISNOT_NEED_DELETE_FLG = true;
                    genba.JISHA_KBN = true;
                    genba = this.genbaDao.GetAllValidData(genba).FirstOrDefault();

                    if (genba == null)
                    {
                        this.form.isError = true;
                        this.form.GENBA_CD_TO.BackColor = Constans.ERROR_COLOR;
                        this.form.GENBA_NAME_TO.Text = "";
                        messageLogic.MessageBoxShow("E020", "現場");
                        this.form.GENBA_CD_TO.Focus();
                        return;
                    }
                    else
                    {
                        this.form.GENBA_NAME_TO.Text = genba.GENBA_NAME_RYAKU;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_CD_TO_Validated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }

        public void ZAIKO_HINMEI_CD_FROM_Validated()
        {
            try
            {
                var messageLogic = new MessageBoxShowLogic();

                if (this.form.preZaikoHinmeiCd != this.form.ZAIKO_HINMEI_CD_FROM.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.ZAIKO_HINMEI_CD_FROM.Text))
                    {
                        this.form.ZAIKO_HINMEI_NAME_FROM.Text = "";
                        this.form.preZaikoHinmeiCd = "";
                        return;
                    }

                    M_ZAIKO_HINMEI zaikoHinmei = new M_ZAIKO_HINMEI();
                    zaikoHinmei.ZAIKO_HINMEI_CD = this.form.ZAIKO_HINMEI_CD_FROM.Text;
                    zaikoHinmei.ISNOT_NEED_DELETE_FLG = true;
                    zaikoHinmei = this.zaikoHinmeiDao.GetAllValidData(zaikoHinmei).FirstOrDefault();

                    if (zaikoHinmei == null)
                    {
                        this.form.isError = true;
                        this.form.ZAIKO_HINMEI_CD_FROM.BackColor = Constans.ERROR_COLOR;
                        this.form.ZAIKO_HINMEI_NAME_FROM.Text = "";
                        messageLogic.MessageBoxShow("E020", "在庫品名");
                        this.form.ZAIKO_HINMEI_CD_FROM.Focus();
                        return;
                    }
                    else
                    {
                        this.form.ZAIKO_HINMEI_NAME_FROM.Text = zaikoHinmei.ZAIKO_HINMEI_NAME_RYAKU;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZAIKO_HINMEI_CD_FROM_Validated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }

        public void ZAIKO_HINMEI_CD_TO_Validated()
        {
            try
            {
                var messageLogic = new MessageBoxShowLogic();

                if (this.form.preZaikoHinmeiCd != this.form.ZAIKO_HINMEI_CD_TO.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.ZAIKO_HINMEI_CD_TO.Text))
                    {
                        this.form.ZAIKO_HINMEI_NAME_TO.Text = "";
                        this.form.preZaikoHinmeiCd = "";
                        return;
                    }

                    M_ZAIKO_HINMEI zaikoHinmei = new M_ZAIKO_HINMEI();
                    zaikoHinmei.ZAIKO_HINMEI_CD = this.form.ZAIKO_HINMEI_CD_TO.Text;
                    zaikoHinmei.ISNOT_NEED_DELETE_FLG = true;
                    zaikoHinmei = this.zaikoHinmeiDao.GetAllValidData(zaikoHinmei).FirstOrDefault();

                    if (zaikoHinmei == null)
                    {
                        this.form.isError = true;
                        this.form.ZAIKO_HINMEI_CD_TO.BackColor = Constans.ERROR_COLOR;
                        this.form.ZAIKO_HINMEI_NAME_TO.Text = "";
                        messageLogic.MessageBoxShow("E020", "在庫品名");
                        this.form.ZAIKO_HINMEI_CD_TO.Focus();
                        return;
                    }
                    else
                    {
                        this.form.ZAIKO_HINMEI_NAME_TO.Text = zaikoHinmei.ZAIKO_HINMEI_NAME_RYAKU;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZAIKO_HINMEI_CD_TO_Validated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }
    }
}
