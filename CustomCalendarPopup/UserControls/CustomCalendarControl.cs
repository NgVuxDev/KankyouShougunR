using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using r_framework.Logic;
using r_framework.Utility;

namespace WindowsFormsApplication1.UserControls
{
    /// <summary>
    /// ６か月分のカレンダー情報を表示するユーザコントロール
    /// </summary>
    public partial class CustomCalendarControl : UserControl
    {
        #region イベント定義
        public delegate void BeforeHandler(object sender, System.EventArgs e);
        public event BeforeHandler OnFromPrevButton_Click;
        public delegate void AfterHandler(object sender, System.EventArgs e);
        public event AfterHandler OnFromNextButton_Click;
        public event AfterHandler OnDayDouble_Click;
        #endregion
        
        #region プロパティ
        /// <summary>
        /// 開始日フィールド
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        ///  Min開始日
        /// </summary>
        public DateTime MinDateTime { get; set; }

        /// <summary>
        ///  Max開始日
        /// </summary>
        public DateTime MaxDateTime { get; set; }

        /// <summary>
        ///  Min修正日
        /// </summary>
        public DateTime MinDate { get; set; }

        /// <summary>
        ///  Max修正日
        /// </summary>
        public DateTime MaxDate { get; set; }
        /// <summary>
        /// 日付
        /// </summary>
        public Panel calendarPanel { get; set; }

        /// <summary>
        /// 休日データ
        /// </summary>
        public DataTable CalendarDataSource { get; set; }

        /// <summary>
        /// 日曜休日設定
        /// </summary>
        public string sysSunday { get; set; }

        /// <summary>
        /// 日フィールド
        /// </summary>
        public String DayText { get; set; }

        /// <summary>
        /// チェックを行う日付の格納フィールド
        /// </summary>
        private List<string> CheckDate { get; set; }
  
        /// <summary>
        /// コントロールユーティリティ
        /// </summary>
        private ControlUtility controlUtil;
        
        /// <summary>
        /// チェッククラス
        /// </summary>
        private Validator validator;


        private DoubleClickButton button1;
        private FormBorderStyle initialStyle;


        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomCalendarControl()
        {
            InitializeComponent();

            CheckDate = new List<string>();

            controlUtil = new ControlUtility();
            validator = new Validator();

            this.DateReset();
        }
        #endregion

        #region 日付コントロール初期化

        /// <summary>
        /// コントロールの初期表示設定
        /// </summary>
        private void CalendarControl_Load(object sender, EventArgs e)
        {
            this.DateReset();
            this.viewCalendar();
        }

        /// <summary>
        /// 日付の初期化を行う
        /// </summary>
        public void DateReset()
        {
            StartDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
            StartDateTime = StartDateTime.AddMonths(-1);           
        }

        /// <summary>
        /// カレンダー情報を動的に生成する
        /// </summary>
        public void viewCalendar()
        {
            //this.CreateButtonText();
            
            for (int i = 1; i <= 1; i++)
            {
                DateTime calendarTime = StartDateTime.AddMonths(i);
                Panel calendarPanel = (Panel)controlUtil.FindControl(this, "calendarPanel" + i);

                //曜日ラベル生成
                this.createLabel(i, calendarPanel);

                //日付タイトル設定
                Label calendarTitle = (Label)controlUtil.FindControl(this, "calendarTitle" + i);
                calendarTitle.Text = calendarTime.Year + "/" + calendarTime.Month;
                
                //日付チェックボックス生成
                this.createCalender(calendarTime.Year, calendarTime.Month, calendarTime.Day, calendarPanel);
            }
        }

        /// <summary>
        /// 曜日ラベルの生成を行う
        /// </summary>
        public void createLabel(int calendarNo, Panel calendarPanel)
        {
            calendarPanel.Controls.Clear();

            Label SundayLabel = new Label();
            Label MondayLabel = new Label();
            Label TuesdayLabel = new Label();
            Label WednesdayLabel = new Label();
            Label ThursdayLabel = new Label();
            Label FridayLabel = new Label();
            Label SaturdayLabel = new Label();
            // 
            // SaturdayLabel
            // 
            SaturdayLabel.BackColor = System.Drawing.Color.Blue;
            SaturdayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            SaturdayLabel.Location = new System.Drawing.Point(257, 26);
            SaturdayLabel.Name = "SaturdayLabel" + calendarNo;
            SaturdayLabel.Size = new System.Drawing.Size(40, 22);
            SaturdayLabel.TabIndex = 111;
            SaturdayLabel.Text = "土";
            SaturdayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(SaturdayLabel);
            // 
            // FridayLabel
            // 
            FridayLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            FridayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            FridayLabel.Location = new System.Drawing.Point(215, 26);
            FridayLabel.Name = "FridayLabel" + calendarNo;
            FridayLabel.Size = new System.Drawing.Size(40, 22);
            FridayLabel.TabIndex = 110;
            FridayLabel.Text = "金";
            FridayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(FridayLabel);
            // 
            // ThursdayLabel
            // 
            ThursdayLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            ThursdayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            ThursdayLabel.Location = new System.Drawing.Point(173, 26);
            ThursdayLabel.Name = "ThursdayLabel" + calendarNo;
            ThursdayLabel.Size = new System.Drawing.Size(40, 22);
            ThursdayLabel.TabIndex = 109;
            ThursdayLabel.Text = "木";
            ThursdayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(ThursdayLabel);
            // 
            // WednesdayLabel
            // 
            WednesdayLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            WednesdayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            WednesdayLabel.Location = new System.Drawing.Point(131, 26);
            WednesdayLabel.Name = "WednesdayLabel" + calendarNo;
            WednesdayLabel.Size = new System.Drawing.Size(40, 22);
            WednesdayLabel.TabIndex = 108;
            WednesdayLabel.Text = "水";
            WednesdayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(WednesdayLabel);
            // 
            // TuesdayLabel
            // 
            TuesdayLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            TuesdayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            TuesdayLabel.Location = new System.Drawing.Point(89, 26);
            TuesdayLabel.Name = "TuesdayLabel" + calendarNo;
            TuesdayLabel.Size = new System.Drawing.Size(40, 22);
            TuesdayLabel.TabIndex = 107;
            TuesdayLabel.Text = "火";
            TuesdayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(TuesdayLabel);
            // 
            // MondayLabel
            // 
            MondayLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            MondayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            MondayLabel.Location = new System.Drawing.Point(47, 26);
            MondayLabel.Name = "MondayLabel" + calendarNo;
            MondayLabel.Size = new System.Drawing.Size(40, 22);
            MondayLabel.TabIndex = 106;
            MondayLabel.Text = "月";
            MondayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(MondayLabel);
            // 
            // SundayLabel
            // 
            SundayLabel.BackColor = System.Drawing.Color.Red;
            SundayLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            SundayLabel.Location = new System.Drawing.Point(5, 26);
            SundayLabel.Name = "SundayLabel" + calendarNo;
            SundayLabel.Size = new System.Drawing.Size(40, 22);
            SundayLabel.TabIndex = 105;
            SundayLabel.Text = "日";
            SundayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            calendarPanel.Controls.Add(SundayLabel);

            Label calendarTitle = new Label();
            // 
            // calendarTitle2
            // 
            calendarTitle.AutoSize = true;
            calendarTitle.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            calendarTitle.ForeColor = System.Drawing.Color.White;
            calendarTitle.Location = new System.Drawing.Point(106 + 20, 6);
            calendarTitle.Name = "calendarTitle" + calendarNo;
            calendarTitle.Size = new System.Drawing.Size(64, 16);
            //calendarTitle.TabIndex = 0;
            calendarPanel.Controls.Add(calendarTitle);
        }

        /// <summary>
        /// カレンダーに表示する日付チェックボックスの生成を行う
        /// </summary>
        public void createCalender(int year, int Month, int day, Panel addPanel)
        {
            DateTime thisDate = new DateTime(year, Month, day);
            var location = this.CreateNewLocation(year, Month, day);
            int xline = Convert.ToInt32(thisDate.DayOfWeek);
            int yline = 1;
            var day1 = 1;
            // 今日の日付
            string strNowDateTIme = DateTime.Now.ToString("yyyyMMdd");

            while (true)
            {
                location.Y = location.Y + 24;

                while ((xline + 1) % 8 != 0)
                {
                    if (!validator.IsDate(year, Month, day1))
                    {
                        return;
                    }           
                    //var DayTextBox = new System.Windows.Forms.TextBox();
                   // var DayTextBox = new System.Windows.Forms.Button();
                   // initialStyle = this.FormBorderStyle;                    
                    var DayTextBox = new DoubleClickButton();

                    var thisDays = new DateTime(year, Month, day1);
                    var holidayInfo = HolidayCheckLogic.Holiday(thisDays);                 

                    if (holidayInfo.name != null && holidayInfo.name.Equals("日曜日"))//日曜
                    {
                        DayTextBox.ForeColor = System.Drawing.Color.Red;
                    }

                    // 当日チェック
                    bool todayFlag = false;
                    DateTime dateTimeVale = new DateTime(year, Month, day1, 0, 0, 0);
                    if (String.Compare(dateTimeVale.ToString("yyyyMMdd"), strNowDateTIme) == 0)
                    {
                        todayFlag = true;
                    }

                    //当日の場合
                    if (todayFlag)
                    {
                        var todayLocation = new Point(2, 2);
                        DayTextBox.Location = todayLocation;
                        DayTextBox.Size = new System.Drawing.Size(36, 20);
                    }
                    else
                    {
                        DayTextBox.Location = location;
                        DayTextBox.Size = new System.Drawing.Size(40, 22);
                    }

                    DayTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
                    //DayTextBox.Location = location;
                    DayTextBox.Name = year + "/" + Month.ToString("00") + "/" + day1.ToString("00");
                    //DayTextBox.Size = new System.Drawing.Size(40, 22);
                    DayTextBox.TabIndex = day1 + 111;
                    DayTextBox.Text = Convert.ToString(day1);
                    //DayTextBox.ReadOnly = true;                                                    
                    //DayTextBox.TextAlign =  HorizontalAlignment.Center;
               
                    DayTextBox.PreviewKeyDown += new PreviewKeyDownEventHandler(DayTextBox_PreviewKeyDown);
                    DayTextBox.Enter += new EventHandler(DayTextBox_Enter);
                    DayTextBox.Leave += new EventHandler(DayTextBox_Leave);
                    DayTextBox.Click += new EventHandler(DayTextBox_Click);
                    DayTextBox.Tag = "日付を選択してください";
                    
                    //addPanel.Controls.Add(DayTextBox);
                    if (todayFlag)
                    {
                        Panel pl = new Panel();
                        pl.Location = location;
                        pl.BackColor = Color.Red;
                        pl.Size = new System.Drawing.Size(40, 24);
                        pl.Controls.Add(DayTextBox);
                        addPanel.Controls.Add(pl);
                    }
                    else
                    {
                        addPanel.Controls.Add(DayTextBox);
                    }
                                        
                    location.X = location.X + 42;
                    xline++;
                    day1++;
                }
                location.X = 5;

                xline++;
                yline++;
            }
        }

        /// <summary>
        /// 日付チェックボックスの開始ロケーションを設定する
        /// </summary>
        public Point CreateNewLocation(int iYear, int iMonth, int iDay)
        {
            DateTime thisDate = new DateTime(iYear, iMonth, iDay);

            var week = thisDate.DayOfWeek;
            if (DayOfWeek.Sunday == week)
            {
                return new Point(5, 26);
            }
            else if (DayOfWeek.Monday == week)
            {
                return new Point(47, 26);
            }
            else if (DayOfWeek.Tuesday == week)
            {
                return new Point(89, 26);
            }
            else if (DayOfWeek.Wednesday == week)
            {
                return new Point(131, 26);
            }
            else if (DayOfWeek.Thursday == week)
            {
                return new Point(173, 26);
            }
            else if (DayOfWeek.Friday == week)
            {
                return new Point(215, 26);
            }
            else if (DayOfWeek.Saturday == week)
            {
                return new Point(257, 26);
            }
            throw new Exception();

        }

        /// <summary>
        /// 前の1ヶ月ボタン
        /// </summary>
        /// <returns></returns>
        public bool prevButtonChk()
        {
            StartDateTime = StartDateTime.AddMonths(-1);
            return true;
        }

        /// <summary>
        /// 次の1ヶ月ボタン
        /// </summary>
        /// <returns></returns>
        public bool nextButtonChk()
        {
            StartDateTime = StartDateTime.AddMonths(1);
            return true;
        }

        /// <summary>
        /// パラメータの日付設定
        /// </summary>
        /// <returns></returns>
        public bool SetParamDate(DateTime dt)
        {
            if ((StartDateTime.Year == 9999 && (StartDateTime.Month + 1) < 12) || StartDateTime.Year < 9999)
            {
                StartDateTime = dt.AddMonths(-1);
            }
            return true;
        }

        /// <summary>
        /// 初期表示時のフォーカス
        /// </summary>
        public void SetParamDateFocus(DateTime dt)
        {
            string targetName = dt.ToString("yyyy/MM/dd");
            foreach (Control target in this.Controls.Find(targetName, true))
            {
                target.Focus();
                target.Select();
            }
        }

        /// <summary>
        /// 現在の年月日の取得
        /// </summary>
        /// <returns></returns>
        public bool GetYearMonthDay(ref string val)
        {
            DateTime dt = StartDateTime.AddMonths(1);
            val = dt.ToString("yyyyMM") + this.DayText;
            return true;
        }

        /// <summary>
        /// 初期表示時のフォーカス
        /// </summary>
        public void InitializeFocus()
        {            
            string targetName = DateTime.Today.ToString("yyyy/MM/dd");
            foreach (Control target in this.Controls.Find(targetName, true))
            {
                target.Focus();
                target.Select();
            }
        }
        #endregion

        #region コントロールイベント
        /// <summary>
        /// 初期表示に戻るボタン（未使用）
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            this.DateReset();
            this.viewCalendar();
        }

        /// <summary>
        /// フォーカス入時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayTextBox_Enter(object sender, EventArgs e)
        {
            Button activeControl = (Button)sender;
            activeControl.BackColor = System.Drawing.SystemColors.Highlight;
        }

        /// <summary>
        /// フォーカス離時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayTextBox_Leave(object sender, EventArgs e)
        {
            Button activeControl = (Button)sender;
            activeControl.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            string day = activeControl.Text;
            DayText = day.PadLeft(2, '0');
        }

        /// <summary>
        ///  一ヶ月前のボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FromPrevBtn_Click(object sender, EventArgs e)
        {
            OnFromPrevButton_Click(sender, e);
        }

        /// <summary>
        /// 一ヶ月後のボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FromNextBtn_Click(object sender, EventArgs e)
        {
            OnFromNextButton_Click(sender, e);
        }

        /// <summary>
        /// クリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayTextBox_Click(object sender, EventArgs e)
        {
            OnDayDouble_Click(sender, e);
        }               

        /// <summary>
        /// キー押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Button activeControl = (Button)sender;     
            DateTime activeDate = DateTime.ParseExact(activeControl.Name, "yyyy/MM/dd", null);
            if (activeDate == null)
            {
                throw new Exception();
            }

            DateTime targetDate = activeDate;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    targetDate = activeDate.AddDays(-7);
                    break;
                case Keys.Down:
                    targetDate = activeDate.AddDays(7);
                    break;
                case Keys.Left:
                    targetDate = activeDate.AddDays(-1);
                    break;
                case Keys.Right:
                    targetDate = activeDate.AddDays(1);
                    break;
                default:
                    return;
            }
            
            string targetName = targetDate.ToString("yyyy/MM/dd", null);
            Control[] targets = this.Controls.Find(targetName, true);
            if (targets.Length == 0)
            {
                if (activeControl.Name.CompareTo(targetName) < 0)
                {
                    // 次1ヵ月へ
                    FromNextBtn_Click(null, new EventArgs());
                }
                else
                {
                    // 前1ヵ月へ
                    FromPrevBtn_Click(null, new EventArgs());
                }
            }
            foreach (Control target in this.Controls.Find(targetName, true))
            {
                target.Focus();
            }
            e.IsInputKey = true;
        }

        #endregion

        #region 日付計算
        /// <summary>
        /// 前後ボタンの表示テキストの設定を行う
        /// </summary>
        private void CreateButtonText()
        {
            var beforeTextStart = StartDateTime.AddMonths(-5);
            var beforeTextEnd = StartDateTime;

            var afterTextStart = StartDateTime.AddMonths(7);
            var afterTextEnd = StartDateTime.AddMonths(12);
        }

        #endregion
    }

    #region Doubleクリックボタン"
    /// <summary>
    /// Doubleクリック
    /// </summary>
    public class DoubleClickButton : Button
    {
        public DoubleClickButton()
            : base()
        {
            // Set the style so a double click event occurs.
            SetStyle(ControlStyles.StandardClick |
                ControlStyles.StandardDoubleClick, true);
        }
    }
    #endregion
}
