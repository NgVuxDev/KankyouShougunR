// $Id: CalendarPopupForm.cs 7745 2013-11-21 01:26:31Z sys_dev_53 $
using System;
using System.Windows.Forms;
using CalendarPopup.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;

namespace CalendarPopup.APP
{
    /// <summary>
    /// カレンダーポップアップ画面
    /// </summary>
    public partial class CalendarPopupForm : SuperPopupForm
    {
        #region フィールド
        /// <summary>
        /// 共通ロジック
        /// </summary>
        public CalendarPopupLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        /// <summary>
        /// カレンダ変更フラグ
        /// </summary>
        private bool calendarFlg = true;

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private CustomRadioButton[] shiinRadioList;

        private string conditionRengeForMessage = string.Empty;

        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CalendarPopupForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 画面コントロールイベント
        /// <summary>
        /// 画面起動時処理
        /// </summary>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            logic = new CalendarPopupLogic(this);

            logic.ReceiveParamFlag = false;
            bool catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }

            var allControl = controlUtil.GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }
            //  日付の初期設定(当日)
            if (!logic.ReceiveParamFlag)
            {
                this.customCalendarControl1.InitializeFocus();
            }
        }

        /// <summary>
        /// フォーカスイン時に実行されるメソッドの追加を行う
        /// </summary>
        /// <param name="c">追加を行う対象のコントロール</param>
        /// <returns></returns>
        private void Control_Enter(Control c)
        {
            c.Enter -= c_GotFocus;
            c.Enter += c_GotFocus;
        }

        /// <summary>
        /// フォーカスが移ったときにヒントテキストを表示する
        /// </summary>
        protected void c_GotFocus(object sender, EventArgs e)
        {
            var activ = ActiveControl as SuperPopupForm;

            if (activ == null)
            {
                if (ActiveControl != null)
                {
                    //   this.lb_hint.Text = (string)ActiveControl.Tag;                   
                }
            }
        }
        #endregion

        #region ファンクションイベント

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetSelectedDateTime(sender, e, false);

                e.Handled = true;
            }
        }

        /// <summary>
        /// [F10]今日
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SeletetToday(object sender, EventArgs e)
        {
            //this.customCalendarControl1.DateReset();
            //this.customCalendarControl1.viewCalendar();
            ////  日付の初期設定(当日)
            //this.customCalendarControl1.InitializeFocus();

            this.SetSelectedDateTime(this.sysDate);
        }

        /// <summary>
        /// [F9]確定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Selected(object sender, EventArgs e)
        {
            SetSelectedDateTime(sender, e, false);
        }

        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Close(object sender, EventArgs e)
        {
            base.ReturnParams = null;
            this.Close();
        }

        /// <summary>
        /// 前の１ヶ月表示用ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendarControl1_OnFromPrevButton_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!this.customCalendarControl1.prevButtonChk())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //msgLogic.MessageBoxShow("E074", "会社休日");
                }
                else
                {
                    this.customCalendarControl1.viewCalendar();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("calendarControl1_OnFromPreButton_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 次の１ヶ月表示用ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendarControl1_OnFromNextButton_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!this.customCalendarControl1.nextButtonChk())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //msgLogic.MessageBoxShow("E074", "会社休日");
                }
                else
                {
                    this.customCalendarControl1.viewCalendar();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("calendarControl1_OnFromNextButton_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 日をダブルクリックした場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendarControl1_OnDayDouble_Click(object sender, EventArgs e)
        {
            SetSelectedDateTime(sender, e, true);
        }

        /// <summary>
        /// 日付選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool SetSelectedDateTime(object sender, EventArgs e, bool dayFlag)
        {
            string val = String.Empty;
            string day = String.Empty;
            try
            {
                this.customCalendarControl1.GetYearMonthDay(ref val);  //F9だと年月日、ダブルクリックだと 年月の取得になる
                if (dayFlag)
                {
                    Button activeControl = (Button)sender;
                    val = val.Length > 6 ? val.Substring(0, 6) : val;
                    day = activeControl.Text;
                    val = val + day.PadLeft(2, '0');
                }
                
                //DateTime dt = DateTime.ParseExact(val, "yyyyMMdd", null);
                DateTime dt = new DateTime();
                if (!DateTime.TryParseExact(val, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dt))
                {
                    string year = val.Substring(0, 4);
                    string month = val.Substring(4, 2);
                    if (!string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(month))
                    {
                        int dayOfMonth = DateTime.DaysInMonth(Convert.ToInt16(year), Convert.ToInt16(month));
                        string newVal = year + month + dayOfMonth.ToString();
                        DateTime.TryParseExact(newVal, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dt);
                    }
                }
                bool catchErr = this.SetSelectedDateTime(dt);
                return catchErr;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSelectedDateTime", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 日付選択(引数の日付をそのまま返却)
        /// </summary>
        /// <param name="dt">日付</param>
        public bool SetSelectedDateTime(DateTime dt)
        {
            try
            {
                //確定
                //MultiRow等の日付型にセットする際は yyyyMMddだと例外発生する。
                this.logic.ElementDecision(dt.ToString("yyyy/MM/dd"));
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSelectedDateTime", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion
    }
}
