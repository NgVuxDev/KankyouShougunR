// $Id: KaisyaKyujitsuHoshuForm.cs 3868 2013-10-17 01:38:09Z sys_dev_22 $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Intercepter;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using KaisyaKyujitsuHoshu.Logic;

namespace KaisyaKyujitsuHoshu.APP
{
    [Implementation]
    [Aspect(typeof(TraceLogIntercepter))]
    public partial class KaisyaKyujitsuHoshuForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private KaisyaKyujitsuHoshuLogic logic;

        /// <summary>
        /// 休日データリスト
        /// </summary>
        private DataTable dtSundayList = new DataTable();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KaisyaKyujitsuHoshuForm()
            : base(WINDOW_ID.M_KAISHA_KYUJITSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new KaisyaKyujitsuHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();
            this.Search(null, e);
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            //休日データ取得
            this.logic.Search();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                this.logic.Regist(base.RegistErrorFlag);
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.logic.Cancel();
        }

        /// <summary>
        /// 閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

        }

        /// <summary>
        /// 次の六ヶ月表示用ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendarControl1_OnAfterButton_Click(object sender, EventArgs e)
        {
            if (!this.calendarControl1.afterButtonChk())
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E074");
            }
            else
            {
                //日曜休日設定を取得
                this.logic.GetKyuujitsuSundayCheck();
                //画面に休日データを設定
                this.calendarControl1.CalendarDataSource = this.logic.setSundayList();
                this.calendarControl1.viewCalendar();
            }
            
        }

        /// <summary>
        /// 前の六ヶ月表示用ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendarControl1_OnBeforeButton_Click(object sender, EventArgs e)
        {
            if (!this.calendarControl1.beforeButtonChk())
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E074");
            }
            else
            {
                //日曜休日設定を取得
                this.logic.GetKyuujitsuSundayCheck();
                //画面に休日データを設定
                this.calendarControl1.CalendarDataSource = this.logic.setSundayList();
                this.calendarControl1.viewCalendar();
            }
        }
    }
}
