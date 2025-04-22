
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CalendarPopup.APP;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.OriginalException;
using r_framework.Setting;
using r_framework.Utility;
using System.Globalization;

namespace CalendarPopup.Logic
{
    /// <summary>
    /// カレンダーポップアップロジック
    /// </summary>
    public class CalendarPopupLogic
    {
        #region フィールド

        /// <summary>
        /// バインドするカラム名一覧
        /// </summary>
        internal string[] bindColumnNames = new string[] { "" };

        /// <summary>
        /// 表示カラム名
        /// </summary>
        internal string[] displayColumnNames = new string[] { };

        /// <summary>
        /// 表示Tag
        /// </summary>
        internal string[] displayTags = new string[] { };

        /// <summary>
        /// 共通一覧画面のForm
        /// </summary>
        private CalendarPopupForm form;
     
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "CalendarPopup.Setting.ButtonSetting.xml";

        /// <summary>
        /// 年度のみ表示フラグ
        /// </summary>
        public bool displayOnlyYear;
        
        /// <summary>
        /// 自社情報マスタデータ取得テーブル
        /// </summary>
        DataTable corpResult { get; set; }

        /// <summary>
        /// 自社情報から取得した期首月
        /// </summary>
        int kishuMonth { get; set; }
        
        /// <summary>
        /// 起動元への戻り値(カラム名)
        /// </summary>
        public string[] returnParamNames = new string[] { };

        /// <summary>
        /// 受信パラメータの日付
        /// </summary>
        string ReceiveParamDate { get; set; }

        /// <summary>
        /// 受信日付があるかどうかのフラグ
        /// </summary>
        public bool ReceiveParamFlag { get; set; }
                
        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal CalendarPopupLogic(CalendarPopupForm targetForm)
        {
            this.form = targetForm;           
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            //確定ボタン(F9)イベント生成
            this.form.bt_func9.Click += new EventHandler(this.form.Selected);

            //今日ボタン(F10)イベント生成
            this.form.bt_func10.Click += new EventHandler(this.form.SeletetToday);

            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.Close);
        }

        /// <summary>
        /// アンカーを設定して、フォームサイズの変更に自動対応
        /// </summary>
        private void CopeResize()
        {
            //リサイズ対応       
            this.form.bt_func9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func12.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            //this.form.lb_hint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                //ファンクションキー対応
                this.form.KeyPreview = true;

                this.form.customCalendarControl1.sysDate = this.form.sysDate;

                //リサイズ対応
                this.CopeResize();

                //ボタンの初期化
                this.ButtonInit();

                // 画面タイトルやDaoを初期化
                this.DisplyInit();

                //パラメータに日付があった場合
                this.ReViewCalendar();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 画面のタイトルなどを初期化を行う
        /// </summary>
        private void DisplyInit()
        {
            // PopupGetMasterFieldプロパティから返却値を設定
            string[] popupGetMasterField = new string[] { };
            if (!string.IsNullOrEmpty(this.form.PopupGetMasterField))
            {
                string str = this.form.PopupGetMasterField.Replace(" ", "");
                str = str.Replace("　", "");
                if (!string.IsNullOrEmpty(str))
                {
                    popupGetMasterField = str.Split(',');
                }
            }

             this.displayOnlyYear = false;
            object[] receiveParamArray = null;
            if (this.form.Params != null)
            {
                receiveParamArray = new object[this.form.Params.Length];
                for (int i = 0; i < this.form.Params.Length; i++)
                {
                    var sendParam = this.form.Params[i];
                    try
                    {
                        if (sendParam != null && sendParam.Equals("DISPLAY_ONLY_YEAR"))
                        {
                            this.displayOnlyYear = true;
                        }
                        else
                        {
                            if (sendParam != null)
                            {
                                string val = "";
                                if (sendParam is Control)
                                {
                                    Control control = sendParam as Control;
                                    val = control.Text;
                                }
                                else
                                {
                                    val = sendParam.ToString();
                                }
                                if ( val.LastIndexOf("DATE_TIME_VALUE")> -1) {
                                    string[] txt = val.Split('=');
                                    this.ReceiveParamDate = txt[1].ToString();
                                }
                            }
                        }
                    }
                    catch
                    {
                        this.displayOnlyYear = false;
                    }
                }
            }

            string parentLabel = string.Empty;
            string childLabel = string.Empty;
            string hintTextConditon = string.Empty;

            switch (this.form.WindowId)
            {
                // 画面IDごとに生成を行うDaoを変更する
                case WINDOW_ID.MAIN_MENU:
                    this.returnParamNames = popupGetMasterField;
                    break;
                default:
                    break;
            }

            /// 画面のラベル表示名を更新
            //this.form.lb_title.Text = childLabel + "日付選択";
            //// Formタイトルの初期化
            //this.form.Text = this.form.lb_title.Text;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            //// ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CcreateButtonInfo();
            var parentForm = (SuperPopupForm)this.form;
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                //設定対象のコントロールを探して名称の設定を行う
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                cont.Text = button.IchiranButtonName;
                cont.Tag = button.IchiranButtonHintText;
            }

            EventInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CcreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            //生成したアセンブリの情報を送って
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// カレンダーの再表示
        /// </summary>
        private void ReViewCalendar()
        {
            if (!this.displayOnlyYear)
            {
                try
                {
                    string value = this.ReceiveParamDate;
                    DateTime tempDate = DateTime.Now.Date;
                    if (string.IsNullOrEmpty(value) || !DateTime.TryParse(value, out tempDate))
                    {
                        value = this.form.sysDate.ToString("yyyy/MM/dd");
                    }
                    if (value != null)
                    {
                        if (value.Length == 8)
                        {
                            value = value.Substring(0, 4) + "/" + value.Substring(4, 2) + "/" + value.Substring(6, 2);
                        }
                        else if (value.Length >= 10)
                        {
                            value = value.Substring(0, 10);
                        }

                        DateTime dt;
                        if (DateTime.TryParse(value, out dt))
                        {
                            DateTime startDateTime = new DateTime(dt.Year, dt.Month, 01);
                            if (this.form.customCalendarControl1.SetParamDate(startDateTime))
                            {
                                this.form.customCalendarControl1.viewCalendar();
                                // 日付の初期設定
                                this.form.customCalendarControl1.SetParamDateFocus(dt);
                                this.ReceiveParamFlag = true;
                            }
                        }
                        else
                        {
                            // 2016/1/1等入力された場合、こちらに入ってくる。
                            DateTime startDateTime = new DateTime(this.form.sysDate.Year, this.form.sysDate.Month, 01);
                            if (this.form.customCalendarControl1.SetParamDate(startDateTime))
                            {
                                this.form.customCalendarControl1.viewCalendar();
                                // 日付の初期設定
                                this.form.customCalendarControl1.SetParamDateFocus(dt);
                                this.ReceiveParamFlag = false;
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            else
            {
                // 年度モードの場合、日付が不正となる対応
                // システム日付が設定された後に日付の初期化をして、カレンダーを再生成する
                this.form.customCalendarControl1.DateReset();
                this.form.customCalendarControl1.viewCalendar();
            }
        }
        #endregion

        #region カレンダ日付選択処理
        /// <summary>
        /// 選択データ決定処理
        /// </summary>
        internal void ElementDecision(string selecterValue)
        {
            Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
            List<PopupReturnParam> setParam = new List<PopupReturnParam>();
            if (!this.form.PopupMultiSelect)
            {
                PopupReturnParam popupParam = new PopupReturnParam();

                popupParam.Key = "Value";

                if (this.displayOnlyYear)
                {
                     decimal dNullable;
                     if (decimal.TryParse(selecterValue, System.Globalization.NumberStyles.Any, null, out dNullable))
                     {
                         DateTime dt = DateTime.ParseExact(selecterValue, "yyyyMMdd", null);
                         selecterValue = dt.ToString("yyyy/MM/dd");
                     }                   

                    CorpInfoUtility corpInfoUtil = new CorpInfoUtility();
                    int year = corpInfoUtil.GetCurrentYear(DateTime.Parse(selecterValue));
                    popupParam.Value = year.ToString();
                }
                else
                {
                    popupParam.Value = selecterValue;
                }

                if (setParamList.ContainsKey(0))
                {
                    setParam = setParamList[0];
                }
                else
                {
                    setParam = new List<PopupReturnParam>();
                }

                setParam.Add(popupParam);
                setParamList.Add(0, setParam);
            }
            else
            {
                for (int i = 0; i < this.returnParamNames.Length; i++)
                {
                    List<string> list = new List<string>();

                    PopupReturnParam popupParam = new PopupReturnParam();
                    popupParam.Key = "Value";
                    popupParam.Value = string.Join(",", list.ToArray());
                    if (setParamList.ContainsKey(i))
                    {
                        setParam = setParamList[i];
                    }
                    else
                    {
                        setParam = new List<PopupReturnParam>();
                    }
                    setParam.Add(popupParam);
                    setParamList.Add(i, setParam);
                }
            }
            this.form.ReturnParams = setParamList;
            this.form.Close();
        }
        #endregion      
    }
}
