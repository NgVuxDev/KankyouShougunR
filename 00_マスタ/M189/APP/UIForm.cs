// $Id: UIForm.cs 13030 2013-12-27 00:03:13Z sys_dev_23 $
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
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.KaisyaKyujitsuHoshu.Logic;
using r_framework.Utility;

namespace Shougun.Core.Master.KaisyaKyujitsuHoshu.APP
{
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        /// ヒントラベル
        /// </summary>
        private Label hintLabel;

        /// <summary>
        /// 休日データリスト
        /// </summary>
        private DataTable dtSundayList = new DataTable();

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_KAISHA_KYUJITSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                base.OnLoad(e);
                if (!this.logic.WindowInit())
                {
                    return;
                }
                this.Search(null, e);

                // 全コントロールを取得
                if (this.allControl == null)
                {
                    this.allControl = controlUtil.GetAllControls(this);
                }
                foreach (Control c in allControl)
                {
                    Control_Enter(c);

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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
        void c_GotFocus(object sender, EventArgs e)
        {
            this.hintLabel = controlUtil.FindControl(ControlUtility.GetTopControl(this), "lb_hint") as Label;
            if (this.calendarControl1.ActiveControl != null && this.calendarControl1.ActiveControl.Tag != null)
            {
                this.hintLabel.Text = this.calendarControl1.ActiveControl.Tag.ToString();
            }
            else
            {
                this.hintLabel.Text = "";
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender,e);
                //休日データ取得
                this.logic.Search();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!base.RegistErrorFlag)
                {
                    this.logic.Regist(base.RegistErrorFlag);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.Cancel();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (MasterBaseForm)this.Parent;

                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormClose", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 次の六ヶ月表示用ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendarControl1_OnAfterButton_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!this.calendarControl1.afterButtonChk())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E074", "会社休日");
                }
                else
                {
                    //日曜休日設定を取得
                    if (!this.logic.GetKyuujitsuSundayCheck())
                    {
                        return;
                    }
                    //画面に休日データを設定
                    //this.calendarControl1.CalendarDataSource = this.logic.setSundayList();
                    this.calendarControl1.viewCalendar();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("calendarControl1_OnAfterButton_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            
        }

        /// <summary>
        /// 前の六ヶ月表示用ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendarControl1_OnBeforeButton_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender,e);
                if (!this.calendarControl1.beforeButtonChk())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E074", "会社休日");
                }
                else
                {
                    //日曜休日設定を取得
                    if (!this.logic.GetKyuujitsuSundayCheck())
                    {
                        return;
                    }
                    //画面に休日データを設定
                    //this.calendarControl1.CalendarDataSource = this.logic.setSundayList();
                    this.calendarControl1.viewCalendar();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("calendarControl1_OnBeforeButton_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// フォーカス設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            //this.ActiveControl = this.calendarControl1.beforeButton;
            //this.ActiveControl.Focus();
            this.Focus();
        }
    }
}
