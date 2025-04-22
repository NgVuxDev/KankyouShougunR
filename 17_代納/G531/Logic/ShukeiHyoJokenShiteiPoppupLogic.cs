using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Data;
using System.Windows.Forms;
using Shougun.Function.ShougunCSCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using System.Reflection;


namespace Shougun.Core.PayByProxy.ShukeiHyoJokenShiteiPoppup
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class ShukeiHyoJokenShiteiPoppupLogic
    {

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.PayByProxy.ShukeiHyoJokenShiteiPoppup.Setting.ButtonSetting.xml";

        /// <summary>
        /// DAO
        /// </summary>
        private ShukeiHyoJokenShiteiPoppupDAO dao;

        /// <summary>
        /// DTO
        /// </summary>
        private ShukeiHyoJokenShiteiPoppupDTO dto;

        /// <summary>
        /// Form
        /// </summary>
        private ShukeiHyoJokenShiteiPoppupForm form;

        /// <summary>
        /// 検索結果取得用
        /// </summary>
        //  public DataTable dataResults { get; set; }

        /// <summary>
        /// 現在表示中の一覧
        /// </summary>
        public DataRow[] selectedRows;

        ///<summary>
        /// alert number
        /// </summary>
        public decimal iAlertNumber;

        private WINDOW_ID windowCall;

        internal MessageBoxShowLogic errmessage;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShukeiHyoJokenShiteiPoppupLogic(WINDOW_ID windowIDCall, ShukeiHyoJokenShiteiPoppupForm targetForm, decimal alertNumber)
        {
            //LogUtility.DebugMethodStart();
            this.form = targetForm;
            this.dto = new ShukeiHyoJokenShiteiPoppupDTO();
            this.dao = DaoInitUtility.GetComponent<ShukeiHyoJokenShiteiPoppupDAO>();
            iAlertNumber = alertNumber;
            this.windowCall = windowIDCall;
            this.errmessage = new MessageBoxShowLogic();
            //LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                //　ボタンのテキストを初期化
                this.ButtonInit();

                this.EventInit();
                //remove control not use of parent form
                var parentForm = (BusinessBaseForm)this.form.Parent;
                //parentForm.Controls.Remove(parentForm.pn_foot);
                parentForm.Controls.Remove(parentForm.ProcessButtonPanel);
                parentForm.Controls.Remove(parentForm.statusStrip1);
                //parentForm.Controls.Remove(parentForm.ribbonForm);
                //ポップアップでは、最小化と最大化、リサイズはできない。
                parentForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

                // Location control
                parentForm.Size = new System.Drawing.Size(720, 460);
                parentForm.StartPosition = FormStartPosition.CenterScreen;
                //parentForm.headerForm.Location = new System.Drawing.Point(parentForm.headerForm.Location.X +10, parentForm.headerForm.Location.Y - 50);
                //foreach (Control ctrl in parentForm.Controls)
                //{
                //    ctrl.Location = new System.Drawing.Point(ctrl.Location.X, ctrl.Location.Y - 100);
                //}

                parentForm.bt_func1.Visible = false;
                parentForm.bt_func2.Visible = false;
                parentForm.bt_func3.Visible = false;
                parentForm.bt_func4.Visible = false;
                parentForm.bt_func5.Visible = false;
                parentForm.bt_func6.Visible = false;
                parentForm.bt_func7.Visible = false;
                parentForm.bt_func8.Visible = false;
                parentForm.bt_func9.Visible = true;
                parentForm.bt_func10.Visible = false;
                parentForm.bt_func11.Visible = false;
                parentForm.bt_func12.Visible = true;
                parentForm.lb_hint.Visible = false;


                var p = parentForm.bt_func9.Location;
                parentForm.bt_func9.Location = new System.Drawing.Point(479, p.Y - 20);
                parentForm.bt_func12.Location = new System.Drawing.Point(582, p.Y - 20);
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        private void EventInit()
        {
            this.form.DENPYOU_DATE_From.Text = DateTime.Now.ToShortDateString();
            this.form.DENPYOU_DATE_To.Text = DateTime.Now.ToShortDateString();
            var parentForm = (BusinessBaseForm)this.form.Parent;

            /// 20141203 Houkakou 日付チェックを追加する　start
            this.form.DENPYOU_DATE_From.Leave += new EventHandler(DENPYOU_DATE_From_Leave);
            this.form.DENPYOU_DATE_To.Leave += new EventHandler(DENPYOU_DATE_To_Leave);
            /// 20141203 Houkakou 日付チェックを追加する　end

            this.form.TORIHIKISAKI_CD_From.Leave += new EventHandler(TORIHIKISAKI_CD_From_Leave);
            this.form.TORIHIKISAKI_CD_To.Leave += new EventHandler(TORIHIKISAKI_CD_To_Leave);

            this.form.GYOUSHA_CD_From.Leave += new EventHandler(GYOUSHA_CD_From_Leave);
            this.form.GYOUSHA_CD_To.Leave += new EventHandler(GYOUSHA_CD_To_Leave);

            this.form.GENBA_CD_From.Leave += new EventHandler(GENBA_CD_From_Leave);
            this.form.GENBA_CD_To.Leave += new EventHandler(GENBA_CD_To_Leave);

            this.form.HINMEI_CD_From.Leave += new EventHandler(HINMEI_CD_From_Leave);
            this.form.HINMEI_CD_To.Leave += new EventHandler(HINMEI_CD_To_Leave);

            this.form.TORIHIKISAKI_CD_Export_From.Leave += new EventHandler(TORIHIKISAKI_CD_Export_From_Leave);
            this.form.TORIHIKISAKI_CD_Export_To.Leave += new EventHandler(TORIHIKISAKI_CD_Export_To_Leave);

            this.form.GYOUSHA_CD_Export_From.Leave += new EventHandler(GYOUSHA_CD_Export_From_Leave);
            this.form.GYOUSHA_CD_Export_To.Leave += new EventHandler(GYOUSHA_CD_Export_To_Leave);

            this.form.GENBA_CD_Export_From.Leave += new EventHandler(GENBA_CD_Export_From_Leave);
            this.form.GENBA_CD_Export_To.Leave += new EventHandler(GENBA_CD_Export_To_Leave);

            this.form.HINMEI_CD_Export_From.Leave += new EventHandler(HINMEI_CD_Export_From_Leave);
            this.form.HINMEI_CD_Export_To.Leave += new EventHandler(HINMEI_CD_Export_To_Leave);

            this.form.UPN_GYOUSHA_CD_From.Leave += new EventHandler(UPN_GYOUSHA_CD_From_Leave);
            this.form.UPN_GYOUSHA_CD_To.Leave += new EventHandler(UPN_GYOUSHA_CD_To_Leave);

            // 20141201 teikyou ダブルクリックを追加する　start
            this.form.DENPYOU_DATE_To.MouseDoubleClick += new MouseEventHandler(DENPYOU_DATE_To_MouseDoubleClick);
            this.form.TORIHIKISAKI_CD_To.MouseDoubleClick += new MouseEventHandler(TORIHIKISAKI_CD_To_MouseDoubleClick);
            this.form.GYOUSHA_CD_To.MouseDoubleClick += new MouseEventHandler(GYOUSHA_CD_To_MouseDoubleClick);
            this.form.GENBA_CD_To.MouseDoubleClick += new MouseEventHandler(GENBA_CD_To_MouseDoubleClick);
            this.form.HINMEI_CD_To.MouseDoubleClick += new MouseEventHandler(HINMEI_CD_To_MouseDoubleClick);
            this.form.TORIHIKISAKI_CD_Export_To.MouseDoubleClick += new MouseEventHandler(TORIHIKISAKI_CD_Export_To_MouseDoubleClick);
            this.form.GYOUSHA_CD_Export_To.MouseDoubleClick += new MouseEventHandler(GYOUSHA_CD_Export_To_MouseDoubleClick);
            this.form.GENBA_CD_Export_To.MouseDoubleClick += new MouseEventHandler(GENBA_CD_Export_To_MouseDoubleClick);
            this.form.HINMEI_CD_Export_To.MouseDoubleClick += new MouseEventHandler(HINMEI_CD_Export_To_MouseDoubleClick);
            this.form.UPN_GYOUSHA_CD_To.MouseDoubleClick += new MouseEventHandler(UPN_GYOUSHA_CD_To_MouseDoubleClick);
            // 20141201 teikyou ダブルクリックを追加する　end

            // 検索
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(SEARCH_BUTTON_Click);
            //閉じる
            parentForm.bt_func12.Click += new EventHandler(btnExit_Click);
        }
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BasePopForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private void DENPYOU_DATE_From_Leave(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                /// 20141203 Houkakou 日付チェックを追加する　start
                //if (this.form.DENPYOU_DATE_From.Text == "")
                //{
                //    this.form.DENPYOU_DATE_From.Focus();
                //    return;
                //}
                //DateTime dt;
                //if (DateTime.TryParse(this.form.DENPYOU_DATE_From.Text, out dt) == false)
                //{
                //    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                //    msg.MessageBoxShow("E012", "正しい日付を入力してください。");
                //    this.form.DENPYOU_DATE_From.Focus();
                //    return;
                //}
                
                //if (Convert.ToDateTime(this.form.DENPYOU_DATE_From.Value.ToString()) > Convert.ToDateTime(this.form.DENPYOU_DATE_To.Value.ToString()))
                //{
                //    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                //    msg.MessageBoxShow("E049", "期間To", "期間From");
                //    this.form.DENPYOU_DATE_From.Focus();
                //    return;
                //}
                 if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE_To.Text))
                {
                    this.form.DENPYOU_DATE_To.IsInputErrorOccured = false;
                    this.form.DENPYOU_DATE_To.BackColor = Constans.NOMAL_COLOR;
                }
                /// 20141203 Houkakou 日付チェックを追加する　end
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void DENPYOU_DATE_To_Leave(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                /// 20141203 Houkakou 日付チェックを追加する　start
                //if (this.form.DENPYOU_DATE_To.Text == null)
                //{
                //    this.form.DENPYOU_DATE_To.Focus();
                //    return;
                //}
                //DateTime dt;
                //if (DateTime.TryParse(this.form.DENPYOU_DATE_To.Text, out dt) == false)
                //{
                //    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                //    msg.MessageBoxShow("E012", "正しい日付を入力してください。");
                //    this.form.DENPYOU_DATE_To.Focus();
                //    return;
                //}
                
                //if (Convert.ToDateTime(this.form.DENPYOU_DATE_To.Value.ToString()) < Convert.ToDateTime(this.form.DENPYOU_DATE_From.Value.ToString()))
                //{
                //    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                //    msg.MessageBoxShow("E049", "期間From", "期間To");
                //    this.form.DENPYOU_DATE_To.Focus();
                //}
                if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE_From.Text))
                {
                    this.form.DENPYOU_DATE_From.IsInputErrorOccured = false;
                    this.form.DENPYOU_DATE_From.BackColor = Constans.NOMAL_COLOR;
                }
                /// 20141203 Houkakou 日付チェックを追加する　end
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 「取引先CD」ロストフォーカス時のイベン
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>`
        /// <param name="e">e</param>

        private void TORIHIKISAKI_CD_From_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_From.Text)) && !(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_To.Text)))
            {
                string strTORIHIKISAKI_CD_From = ZeroPaddeng(this.form.TORIHIKISAKI_CD_From.Text, 6);
                int i = strTORIHIKISAKI_CD_From.CompareTo(this.form.TORIHIKISAKI_CD_To.Text);
                if (i > 0)
                {
                    MessageBoxShowLogic msb = new MessageBoxShowLogic();
                    msb.MessageBoxShow("E032", "受入取引先CDfrom", "受入取引先CDto");
                    this.form.TORIHIKISAKI_CD_From.Focus();
                    return;
                }
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        private void TORIHIKISAKI_CD_To_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_From.Text)) && !(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_To.Text)))
            {
                string strTORIHIKISAKI_CD_To = ZeroPaddeng(this.form.TORIHIKISAKI_CD_To.Text, 6);
                int i = strTORIHIKISAKI_CD_To.CompareTo(this.form.TORIHIKISAKI_CD_From.Text);
                if (i < 0)
                {
                    MessageBoxShowLogic msb = new MessageBoxShowLogic();
                    msb.MessageBoxShow("E032", "受入取引先CDfrom", "受入取引先CDto");
                    this.form.TORIHIKISAKI_CD_To.Focus();
                    return;
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void GYOUSHA_CD_From_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!(string.IsNullOrEmpty(this.form.GYOUSHA_CD_From.Text)) && !string.IsNullOrEmpty(this.form.GYOUSHA_CD_To.Text))
            {
                string strGYOUSHA_CD_From = ZeroPaddeng(this.form.GYOUSHA_CD_From.Text, 6);
                int i = strGYOUSHA_CD_From.CompareTo(this.form.GYOUSHA_CD_To.Text);
                if (i > 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "受入業者CDfrom", "受入業者CDto");
                    this.form.GYOUSHA_CD_From.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }
        private void GYOUSHA_CD_To_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_From.Text) && !string.IsNullOrEmpty(this.form.GYOUSHA_CD_To.Text))
            {
                string strGYOUSHA_CD_To = ZeroPaddeng(this.form.GYOUSHA_CD_To.Text, 6);
                int i = strGYOUSHA_CD_To.CompareTo(this.form.GYOUSHA_CD_From.Text);
                if (i < 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "受入業者CDfrom", "受入業者CDto");
                    this.form.GYOUSHA_CD_To.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void GENBA_CD_From_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.GENBA_CD_From.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD_To.Text))
            {
                string strGENBA_CD_From = ZeroPaddeng(this.form.GENBA_CD_From.Text, 6);
                int i = strGENBA_CD_From.CompareTo(this.form.GENBA_CD_To.Text);
                if (i > 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "受入現場CDfrom", "受入現場CDto");
                    this.form.GENBA_CD_From.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }
        private void GENBA_CD_To_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.GENBA_CD_From.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD_To.Text))
            {
                string strGENBA_CD_To = ZeroPaddeng(this.form.GENBA_CD_To.Text, 6);
                int i = strGENBA_CD_To.CompareTo(this.form.GENBA_CD_From.Text);
                if (i < 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "受入現場CDfrom", "受入現場CDto");
                    this.form.GENBA_CD_To.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void HINMEI_CD_From_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.HINMEI_CD_From.Text) && !string.IsNullOrEmpty(this.form.HINMEI_CD_To.Text))
            {
                string strHINMEI_CD_From = ZeroPaddeng(this.form.HINMEI_CD_From.Text, 6);
                int i = strHINMEI_CD_From.CompareTo(this.form.HINMEI_CD_To.Text);
                if (i > 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "受入品名CDfrom", "受入品名CDto");
                    this.form.HINMEI_CD_From.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void HINMEI_CD_To_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.HINMEI_CD_To.Text) && !string.IsNullOrEmpty(this.form.HINMEI_CD_From.Text))
            {
                string strHINMEI_CD_To = ZeroPaddeng(this.form.HINMEI_CD_To.Text, 6);
                int i = strHINMEI_CD_To.CompareTo(this.form.HINMEI_CD_From.Text);
                if (i < 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "受入品名CDfrom", "受入品名CDto");
                    this.form.HINMEI_CD_To.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void TORIHIKISAKI_CD_Export_From_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_Export_From.Text) && !string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_Export_To.Text))
            {
                string strTORIHIKISAKI_CD_Export_From = ZeroPaddeng(this.form.TORIHIKISAKI_CD_Export_From.Text, 6);
                int i = strTORIHIKISAKI_CD_Export_From.CompareTo(this.form.TORIHIKISAKI_CD_Export_To.Text);
                if (i > 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "出荷取引先CDfrom", "出荷取引先CDto");
                    this.form.TORIHIKISAKI_CD_Export_From.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);

        }
        private void TORIHIKISAKI_CD_Export_To_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_Export_From.Text) && !string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_Export_To.Text))
            {
                string strTORIHIKISAKI_CD_Export_To = ZeroPaddeng(this.form.TORIHIKISAKI_CD_Export_To.Text, 6);
                int i = strTORIHIKISAKI_CD_Export_To.CompareTo(this.form.TORIHIKISAKI_CD_Export_From.Text);
                if (i < 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "出荷取引先CDfrom", "出荷取引先CDto");
                    this.form.TORIHIKISAKI_CD_Export_To.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void GYOUSHA_CD_Export_From_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_Export_From.Text) && !string.IsNullOrEmpty(this.form.GYOUSHA_CD_Export_To.Text))
            {
                string strGYOUSHA_CD_Export_From = ZeroPaddeng(this.form.GYOUSHA_CD_Export_From.Text, 6);
                int i = strGYOUSHA_CD_Export_From.CompareTo(this.form.GYOUSHA_CD_Export_To.Text);
                if (i > 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "出荷業者CDfrom", "出荷業者CDto");
                    this.form.GYOUSHA_CD_Export_From.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void GYOUSHA_CD_Export_To_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_Export_From.Text) && !string.IsNullOrEmpty(this.form.GYOUSHA_CD_Export_To.Text))
            {
                string strGYOUSHA_CD_Export_To = ZeroPaddeng(this.form.GYOUSHA_CD_Export_To.Text, 6);
                int i = strGYOUSHA_CD_Export_To.CompareTo(this.form.GYOUSHA_CD_Export_From.Text);
                if (i < 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "出荷業者CDfrom", "出荷業者CDto");
                    this.form.GYOUSHA_CD_Export_To.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void GENBA_CD_Export_From_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.GENBA_CD_Export_From.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD_Export_To.Text))
            {
                string strGENBA_CD_Export_From = ZeroPaddeng(this.form.GENBA_CD_Export_From.Text, 6);
                int i = strGENBA_CD_Export_From.CompareTo(this.form.GENBA_CD_Export_To.Text);
                if (i > 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "出荷現場CDfrom", "出荷現場CDto");
                    this.form.GENBA_CD_Export_From.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void GENBA_CD_Export_To_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.GENBA_CD_Export_From.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD_Export_To.Text))
            {
                string strGENBA_CD_Export_To = ZeroPaddeng(this.form.GENBA_CD_Export_To.Text, 6);
                int i = strGENBA_CD_Export_To.CompareTo(this.form.GENBA_CD_Export_From.Text);
                if (i < 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "出荷現場CDfrom", "出荷現場CDto");
                    this.form.GENBA_CD_Export_To.Focus();
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void HINMEI_CD_Export_From_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.HINMEI_CD_Export_From.Text) && !string.IsNullOrEmpty(this.form.HINMEI_CD_Export_To.Text))
            {
                string strHINMEI_CD_Export_From = ZeroPaddeng(this.form.HINMEI_CD_Export_From.Text, 6);
                int i = strHINMEI_CD_Export_From.CompareTo(this.form.HINMEI_CD_Export_To.Text);
                if (i > 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "出荷品名CDfrom", "出荷品名CDto");
                    this.form.HINMEI_CD_Export_From.Focus();
                    return;
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void HINMEI_CD_Export_To_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.HINMEI_CD_Export_From.Text) && !string.IsNullOrEmpty(this.form.HINMEI_CD_Export_To.Text))
            {
                string strHINMEI_CD_Export_To = ZeroPaddeng(this.form.HINMEI_CD_Export_To.Text, 6);
                int i = strHINMEI_CD_Export_To.CompareTo(this.form.HINMEI_CD_Export_From.Text);
                if (i < 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "出荷品名CDfrom", "出荷品名CDto");
                    this.form.HINMEI_CD_Export_To.Focus();
                    return;
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void UPN_GYOUSHA_CD_From_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD_From.Text) && !string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD_To.Text))
            {
                string UPN_GYOUSHA_CD_From = ZeroPaddeng(this.form.UPN_GYOUSHA_CD_From.Text, 6);
                int i = UPN_GYOUSHA_CD_From.CompareTo(this.form.UPN_GYOUSHA_CD_To.Text);
                if (i > 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "運搬業者CDfrom", "運搬業者CDto");
                    this.form.UPN_GYOUSHA_CD_From.Focus();
                    return;
                }
            }
            LogUtility.DebugMethodEnd(sender, e);

        }
        private void UPN_GYOUSHA_CD_To_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD_From.Text) && !string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD_To.Text))
            {
                string UPN_GYOUSHA_CD_To = ZeroPaddeng(this.form.UPN_GYOUSHA_CD_To.Text, 6);
                int i = UPN_GYOUSHA_CD_To.CompareTo(this.form.UPN_GYOUSHA_CD_From.Text);
                if (i < 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E032", "運搬業者CDfrom", "運搬業者CDto");
                    this.form.UPN_GYOUSHA_CD_To.Focus();
                    return;
                }
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
        private void CloseForm()
        {
            var fParent = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            fParent.Close();
        }
        private void SEARCH_BUTTON_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //if (string.IsNullOrEmpty(this.form.txt_KyotenCD.Text))
            //{
            //    MessageBoxShowLogic msg = new MessageBoxShowLogic();
            //    msg.MessageBoxShow("E001", "拠点");
            //    this.form.txt_KyotenCD.Focus();
            //    return;
            //}

            if (this.form.RegistErrorFlag)
            {
                return;
            }

            /// 20141203 Houkakou 日付チェックを追加する　start
            if (this.DateCheck())
            {
                return;
            }
            /// 20141203 Houkakou 日付チェックを追加する　end

            ShukeiHyoJokenDTO entity = new ShukeiHyoJokenDTO();
            entity.DAINOU_ENTRY_DENPYOU_DATE_FROM = DateTime.Parse(this.form.DENPYOU_DATE_From.Value.ToString());
            entity.DAINOU_ENTRY_DENPYOU_DATE_TO = DateTime.Parse(this.form.DENPYOU_DATE_To.Value.ToString());
            entity.M_KYOTEN_CD = this.form.txt_KyotenCD.Text;
            if (!(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_From.Text.Trim())))
            {
                entity.UKEIRE_ENTRY_TORIHIKISAKI_CD_FROM = this.form.TORIHIKISAKI_CD_From.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_To.Text.Trim())))
            {
                entity.UKEIRE_ENTRY_TORIHIKISAKI_CD_TO = this.form.TORIHIKISAKI_CD_To.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.GYOUSHA_CD_From.Text.Trim())))
            {
                entity.UKEIRE_ENTRY_GYOUSHA_CD_FROM = this.form.GYOUSHA_CD_From.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.GYOUSHA_CD_To.Text.Trim())))
            {
                entity.UKEIRE_ENTRY_GYOUSHA_CD_TO = this.form.GYOUSHA_CD_To.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.GENBA_CD_From.Text.Trim())))
            {
                entity.UKEIRE_ENTRY_GENBA_CD_FROM = this.form.GENBA_CD_From.Text;
            }
            if (!(string.IsNullOrEmpty(this.form.GENBA_CD_To.Text.Trim())))
            {
                entity.UKEIRE_ENTRY_GENBA_CD_TO = this.form.GENBA_CD_To.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.HINMEI_CD_From.Text.Trim())))
            {
                entity.UKEIRE_DETAIL_HINMEI_CD_FROM = this.form.HINMEI_CD_From.Text;
            }
            if (!(string.IsNullOrEmpty(this.form.HINMEI_CD_To.Text.Trim())))
            {
                entity.UKEIRE_DETAIL_HINMEI_CD_TO = this.form.HINMEI_CD_To.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_Export_From.Text.Trim())))
            {
                entity.SHUKKA_ENTRY_TORIHIKISAKI_CD_FROM = this.form.TORIHIKISAKI_CD_Export_From.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_Export_To.Text.Trim())))
            {
                entity.SHUKKA_ENTRY_TORIHIKISAKI_CD_TO = this.form.TORIHIKISAKI_CD_Export_To.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.GYOUSHA_CD_Export_From.Text.Trim())))
            {
                entity.SHUKKA_ENTRY_GYOUSHA_CD_FROM = this.form.GYOUSHA_CD_Export_From.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.GYOUSHA_CD_Export_To.Text.Trim())))
            {
                entity.SHUKKA_ENTRY_GYOUSHA_CD_TO = this.form.GYOUSHA_CD_Export_To.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.GENBA_CD_Export_From.Text.Trim())))
            {
                entity.SHUKKA_ENTRY_GENBA_CD_FROM = this.form.GENBA_CD_Export_From.Text;
            }
            if (!(string.IsNullOrEmpty(this.form.GENBA_CD_Export_To.Text.Trim())))
            {
                entity.SHUKKA_ENTRY_GENBA_CD_TO = this.form.GENBA_CD_Export_To.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.HINMEI_CD_Export_From.Text.Trim())))
            {
                entity.SHUKKA_DETAIL_HINMEI_CD_FROM = this.form.HINMEI_CD_Export_From.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.HINMEI_CD_Export_To.Text.Trim())))
            {
                entity.SHUKKA_DETAIL_HINMEI_CD_TO = this.form.HINMEI_CD_Export_To.Text;
            }

            if (!(string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD_From.Text.Trim())))
            {
                entity.UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_FROM = this.form.UPN_GYOUSHA_CD_From.Text;
            }
            if (!(string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD_To.Text.Trim())))
            {
                entity.UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_TO = this.form.UPN_GYOUSHA_CD_To.Text;
            }

            DataTable dtResult = new DataTable();
            if (windowCall == WINDOW_ID.T_DAINO_MEISAIHYOU)
                dtResult = this.dao.GetDataForEntity(entity);
            else if (windowCall == WINDOW_ID.T_DAINO_SYUUKEIHYOU)
                dtResult = this.dao.GetDataReport488(entity);


            //      selectedRows = dtResult.Select(GetSelectWhere());

            if (dtResult.Rows.Count <= 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E044", "出力する該当データがありません。");
                return;
            }

            if (dtResult.Rows.Count > iAlertNumber)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C035");
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            //2014.01.10 by co start
            //受入現場・受入品名・出荷現場・出荷現場・出荷品名での集計。
            if (windowCall == WINDOW_ID.T_DAINO_SYUUKEIHYOU)
            {
                //詳細情報データ在取得
                dtResult = createDetailData(dtResult);
            }
            //2014.01.10 by co end
            Dictionary<string, DataTable> dicResult = new Dictionary<string, DataTable>();

            DataTable dtHeader = DataHeader();

            DataTable dtFooter = new DataTable();

            if (windowCall == WINDOW_ID.T_DAINO_MEISAIHYOU)
                dtFooter = dataFooter(dtResult);
            else if (windowCall == WINDOW_ID.T_DAINO_SYUUKEIHYOU)
                dtFooter = dataFooter488(dtResult);

            dicResult.Add("Header", dtHeader);
            dicResult.Add("Detail", dtResult);
            dicResult.Add("Footer", dtFooter);

            this.form.resultReturnMethod(ReturnSearchCondition(), dtResult, dicResult);


            LogUtility.DebugMethodEnd(sender, e);

            CloseForm();

        }
        //2014.01.10 by co start
        /// <summary>
        /// 受入現場・受入品名・出荷現場・出荷現場・出荷品名での集計が行われていない。
        /// バグ修正対応
        /// </summary>
        /// <param name="mReportInfoDetailData"></param>
        /// <returns></returns>
        private DataTable createDetailData(DataTable mReportInfoDetailData)
        {
            // ReadOnly制約を一時的に解除
            for (int i = 0; i < mReportInfoDetailData.Columns.Count; i++)
            {
                mReportInfoDetailData.Columns[i].ReadOnly = false;
            }
            //戻り情報
            DataTable retData = mReportInfoDetailData.Clone();
            foreach (DataRow dtRow in mReportInfoDetailData.Rows)
            {
                //詳細集計
                //受入正味(kg)合計             
                dtRow["UKEIRE_SYOUMI"] = mReportInfoDetailData.AsEnumerable()
                   .Where(y => y.Field<string>("UKEIRE_TORIHIKISAKI_CD").Equals(dtRow["UKEIRE_TORIHIKISAKI_CD"].ToString()) &&
                        y.Field<string>("SHUKKA_TORIHIKISAKI_CD").Equals(dtRow["SHUKKA_TORIHIKISAKI_CD"].ToString()) &&
                         y.Field<string>("UKEIRE_GYOUSHA_CD").Equals(dtRow["UKEIRE_GYOUSHA_CD"].ToString()) &&
                           y.Field<string>("SHUKKA_GYOUSHA_CD").Equals(dtRow["SHUKKA_GYOUSHA_CD"].ToString()) &&
                             y.Field<string>("UPN_GYOUSHA_CD").Equals(dtRow["UPN_GYOUSHA_CD"].ToString()) &&
                   y.Field<string>("UKEIRE_GENBA_CD").Equals(dtRow["UKEIRE_GENBA_CD"].ToString()) &&
                   y.Field<string>("SHUKKA_GENBA_CD").Equals(dtRow["SHUKKA_GENBA_CD"].ToString()) &&
                   y.Field<string>("UKEIRE_HINMEI_CD").Equals(dtRow["UKEIRE_HINMEI_CD"].ToString()) &&
                   y.Field<string>("SHUKKA_HINMEI_CD").Equals(dtRow["SHUKKA_HINMEI_CD"].ToString()))
                   .Sum(r => r.Field<decimal>("UKEIRE_SYOUMI"));//decimal

                //受入数量合計
                dtRow["UKEIRE_SUURYOU"] = mReportInfoDetailData.AsEnumerable()
                   .Where(y => y.Field<string>("UKEIRE_TORIHIKISAKI_CD").Equals(dtRow["UKEIRE_TORIHIKISAKI_CD"].ToString()) &&
                        y.Field<string>("SHUKKA_TORIHIKISAKI_CD").Equals(dtRow["SHUKKA_TORIHIKISAKI_CD"].ToString()) &&
                         y.Field<string>("UKEIRE_GYOUSHA_CD").Equals(dtRow["UKEIRE_GYOUSHA_CD"].ToString()) &&
                           y.Field<string>("SHUKKA_GYOUSHA_CD").Equals(dtRow["SHUKKA_GYOUSHA_CD"].ToString()) &&
                             y.Field<string>("UPN_GYOUSHA_CD").Equals(dtRow["UPN_GYOUSHA_CD"].ToString()) &&
                   y.Field<string>("UKEIRE_GENBA_CD").Equals(dtRow["UKEIRE_GENBA_CD"].ToString()) &&
                   y.Field<string>("SHUKKA_GENBA_CD").Equals(dtRow["SHUKKA_GENBA_CD"].ToString()) &&
                   y.Field<string>("UKEIRE_HINMEI_CD").Equals(dtRow["UKEIRE_HINMEI_CD"].ToString()) &&
                   y.Field<string>("SHUKKA_HINMEI_CD").Equals(dtRow["SHUKKA_HINMEI_CD"].ToString()))
                   .Sum(r => r.Field<decimal>("UKEIRE_SUURYOU"));

                //受入金額合計
                dtRow["UKEIRE_KINGAKU"] = mReportInfoDetailData.AsEnumerable()
                   .Where(y => y.Field<string>("UKEIRE_TORIHIKISAKI_CD").Equals(dtRow["UKEIRE_TORIHIKISAKI_CD"].ToString()) &&
                        y.Field<string>("SHUKKA_TORIHIKISAKI_CD").Equals(dtRow["SHUKKA_TORIHIKISAKI_CD"].ToString()) &&
                         y.Field<string>("UKEIRE_GYOUSHA_CD").Equals(dtRow["UKEIRE_GYOUSHA_CD"].ToString()) &&
                           y.Field<string>("SHUKKA_GYOUSHA_CD").Equals(dtRow["SHUKKA_GYOUSHA_CD"].ToString()) &&
                             y.Field<string>("UPN_GYOUSHA_CD").Equals(dtRow["UPN_GYOUSHA_CD"].ToString()) &&
                   y.Field<string>("UKEIRE_GENBA_CD").Equals(dtRow["UKEIRE_GENBA_CD"].ToString()) &&
                   y.Field<string>("SHUKKA_GENBA_CD").Equals(dtRow["SHUKKA_GENBA_CD"].ToString()) &&
                   y.Field<string>("UKEIRE_HINMEI_CD").Equals(dtRow["UKEIRE_HINMEI_CD"].ToString()) &&
                   y.Field<string>("SHUKKA_HINMEI_CD").Equals(dtRow["SHUKKA_HINMEI_CD"].ToString()))
                   .Sum(r => r.Field<decimal>("UKEIRE_KINGAKU"));

                //出荷正味(kg)合計
                dtRow["SHUKKA_SYOUMI"] = mReportInfoDetailData.AsEnumerable()
                  .Where(y => y.Field<string>("UKEIRE_TORIHIKISAKI_CD").Equals(dtRow["UKEIRE_TORIHIKISAKI_CD"].ToString()) &&
                       y.Field<string>("SHUKKA_TORIHIKISAKI_CD").Equals(dtRow["SHUKKA_TORIHIKISAKI_CD"].ToString()) &&
                        y.Field<string>("UKEIRE_GYOUSHA_CD").Equals(dtRow["UKEIRE_GYOUSHA_CD"].ToString()) &&
                          y.Field<string>("SHUKKA_GYOUSHA_CD").Equals(dtRow["SHUKKA_GYOUSHA_CD"].ToString()) &&
                            y.Field<string>("UPN_GYOUSHA_CD").Equals(dtRow["UPN_GYOUSHA_CD"].ToString()) &&
                  y.Field<string>("UKEIRE_GENBA_CD").Equals(dtRow["UKEIRE_GENBA_CD"].ToString()) &&
                  y.Field<string>("SHUKKA_GENBA_CD").Equals(dtRow["SHUKKA_GENBA_CD"].ToString()) &&
                  y.Field<string>("UKEIRE_HINMEI_CD").Equals(dtRow["UKEIRE_HINMEI_CD"].ToString()) &&
                  y.Field<string>("SHUKKA_HINMEI_CD").Equals(dtRow["SHUKKA_HINMEI_CD"].ToString()))
                  .Sum(r => r.Field<decimal>("SHUKKA_SYOUMI"));
                //出荷数量合計
                dtRow["SHUKKA_SUURYOU"] = mReportInfoDetailData.AsEnumerable()
                  .Where(y => y.Field<string>("UKEIRE_TORIHIKISAKI_CD").Equals(dtRow["UKEIRE_TORIHIKISAKI_CD"].ToString()) &&
                       y.Field<string>("SHUKKA_TORIHIKISAKI_CD").Equals(dtRow["SHUKKA_TORIHIKISAKI_CD"].ToString()) &&
                        y.Field<string>("UKEIRE_GYOUSHA_CD").Equals(dtRow["UKEIRE_GYOUSHA_CD"].ToString()) &&
                          y.Field<string>("SHUKKA_GYOUSHA_CD").Equals(dtRow["SHUKKA_GYOUSHA_CD"].ToString()) &&
                            y.Field<string>("UPN_GYOUSHA_CD").Equals(dtRow["UPN_GYOUSHA_CD"].ToString()) &&
                  y.Field<string>("UKEIRE_GENBA_CD").Equals(dtRow["UKEIRE_GENBA_CD"].ToString()) &&
                  y.Field<string>("SHUKKA_GENBA_CD").Equals(dtRow["SHUKKA_GENBA_CD"].ToString()) &&
                  y.Field<string>("UKEIRE_HINMEI_CD").Equals(dtRow["UKEIRE_HINMEI_CD"].ToString()) &&
                  y.Field<string>("SHUKKA_HINMEI_CD").Equals(dtRow["SHUKKA_HINMEI_CD"].ToString()))
                  .Sum(r => r.Field<decimal>("SHUKKA_SUURYOU"));

                //出荷金額合計
                dtRow["SHUKKA_KINGAKU"] = mReportInfoDetailData.AsEnumerable()
                  .Where(y => y.Field<string>("UKEIRE_TORIHIKISAKI_CD").Equals(dtRow["UKEIRE_TORIHIKISAKI_CD"].ToString()) &&
                       y.Field<string>("SHUKKA_TORIHIKISAKI_CD").Equals(dtRow["SHUKKA_TORIHIKISAKI_CD"].ToString()) &&
                        y.Field<string>("UKEIRE_GYOUSHA_CD").Equals(dtRow["UKEIRE_GYOUSHA_CD"].ToString()) &&
                          y.Field<string>("SHUKKA_GYOUSHA_CD").Equals(dtRow["SHUKKA_GYOUSHA_CD"].ToString()) &&
                            y.Field<string>("UPN_GYOUSHA_CD").Equals(dtRow["UPN_GYOUSHA_CD"].ToString()) &&
                  y.Field<string>("UKEIRE_GENBA_CD").Equals(dtRow["UKEIRE_GENBA_CD"].ToString()) &&
                  y.Field<string>("SHUKKA_GENBA_CD").Equals(dtRow["SHUKKA_GENBA_CD"].ToString()) &&
                  y.Field<string>("UKEIRE_HINMEI_CD").Equals(dtRow["UKEIRE_HINMEI_CD"].ToString()) &&
                  y.Field<string>("SHUKKA_HINMEI_CD").Equals(dtRow["SHUKKA_HINMEI_CD"].ToString()))
                  .Sum(r => r.Field<decimal>("SHUKKA_KINGAKU"));

                //差益金額合計                
                dtRow["SAEKI_KINGAKU"] = mReportInfoDetailData.AsEnumerable()
                 .Where(y => y.Field<string>("UKEIRE_TORIHIKISAKI_CD").Equals(dtRow["UKEIRE_TORIHIKISAKI_CD"].ToString()) &&
                      y.Field<string>("SHUKKA_TORIHIKISAKI_CD").Equals(dtRow["SHUKKA_TORIHIKISAKI_CD"].ToString()) &&
                       y.Field<string>("UKEIRE_GYOUSHA_CD").Equals(dtRow["UKEIRE_GYOUSHA_CD"].ToString()) &&
                         y.Field<string>("SHUKKA_GYOUSHA_CD").Equals(dtRow["SHUKKA_GYOUSHA_CD"].ToString()) &&
                           y.Field<string>("UPN_GYOUSHA_CD").Equals(dtRow["UPN_GYOUSHA_CD"].ToString()) &&
                 y.Field<string>("UKEIRE_GENBA_CD").Equals(dtRow["UKEIRE_GENBA_CD"].ToString()) &&
                 y.Field<string>("SHUKKA_GENBA_CD").Equals(dtRow["SHUKKA_GENBA_CD"].ToString()) &&
                 y.Field<string>("UKEIRE_HINMEI_CD").Equals(dtRow["UKEIRE_HINMEI_CD"].ToString()) &&
                 y.Field<string>("SHUKKA_HINMEI_CD").Equals(dtRow["SHUKKA_HINMEI_CD"].ToString()))
                 .Sum(r => r.Field<decimal>("SAEKI_KINGAKU"));

                if (retData != null)
                {

                    var rowInfo = retData.AsEnumerable()
                 .Where(y => y.Field<string>("UKEIRE_TORIHIKISAKI_CD").Equals(dtRow["UKEIRE_TORIHIKISAKI_CD"].ToString()) &&
                      y.Field<string>("SHUKKA_TORIHIKISAKI_CD").Equals(dtRow["SHUKKA_TORIHIKISAKI_CD"].ToString()) &&
                       y.Field<string>("UKEIRE_GYOUSHA_CD").Equals(dtRow["UKEIRE_GYOUSHA_CD"].ToString()) &&
                         y.Field<string>("SHUKKA_GYOUSHA_CD").Equals(dtRow["SHUKKA_GYOUSHA_CD"].ToString()) &&
                           y.Field<string>("UPN_GYOUSHA_CD").Equals(dtRow["UPN_GYOUSHA_CD"].ToString()) &&
                 y.Field<string>("UKEIRE_GENBA_CD").Equals(dtRow["UKEIRE_GENBA_CD"].ToString()) &&
                 y.Field<string>("SHUKKA_GENBA_CD").Equals(dtRow["SHUKKA_GENBA_CD"].ToString()) &&
                 y.Field<string>("UKEIRE_HINMEI_CD").Equals(dtRow["UKEIRE_HINMEI_CD"].ToString()) &&
                 y.Field<string>("SHUKKA_HINMEI_CD").Equals(dtRow["SHUKKA_HINMEI_CD"].ToString())).ToList();
                    if (rowInfo.Count <= 0)
                    {
                        //データ追加
                        DataRow newRow = retData.NewRow();
                        foreach (DataColumn dtColumn in mReportInfoDetailData.Columns)
                        {
                            newRow[dtColumn.ColumnName] = dtRow[dtColumn.ColumnName];
                        }
                        retData.Rows.Add(newRow);
                    }
                }
            }
            return retData;
        }
        //2014.01.10 by co end
        /// <summary>
        /// create header
        /// </summary>
        /// <returns></returns>
        /// 
        #region Temp

        private enum TableType
        {
            Header = 1,
            Footer = 2
        }
        private DataTable DataHeader()
        {
            DataTable dtHead = this.CreateTempDataTable(TableType.Header);

            M_CORP_INFO copInfo = new M_CORP_INFO();
            IM_CORP_INFODao copDao = (IM_CORP_INFODao)DaoInitUtility.GetComponent<r_framework.Dao.IM_CORP_INFODao>();
            M_CORP_INFO[] listCop = copDao.GetAllData();
            copInfo = listCop[0];
            string CopName = copInfo.CORP_NAME;

            String DENPYOU_DATE = String.Empty;
            String TORIHIKISAKI_CD = String.Empty;
            String GYOUSHA_CD = String.Empty;
            String GENBA_CD = String.Empty;
            String HINMEI_CD = String.Empty;
            String TORIHIKISAKI_CD_Export = String.Empty;
            String GYOUSHA_CD_Export = String.Empty;
            String GENBA_CD_Export = String.Empty;
            String HINMEI_CD_Export = String.Empty;
            String UPN_GYOUSHA_CD = String.Empty;

            // 伝票日付範囲指定
            if (string.IsNullOrEmpty(this.form.DENPYOU_DATE_From.Text) && string.IsNullOrEmpty(this.form.DENPYOU_DATE_To.Text))
            {
                DENPYOU_DATE = "全て";
            }
            else
            {
                DENPYOU_DATE = DateTime.Parse(this.form.DENPYOU_DATE_From.Text).ToString("yyyy/MM/dd") + " ～ " +
                                 DateTime.Parse(this.form.DENPYOU_DATE_To.Text).ToString("yyyy/MM/dd");
            }

            // 受入取引先
            if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_From.Text) && string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_To.Text))
            {
                TORIHIKISAKI_CD = "全て";
            }
            else
            {
                TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD_From.Text + " ～ " + this.form.TORIHIKISAKI_CD_To.Text;
            }
            // 受入業者
            if (string.IsNullOrEmpty(this.form.GYOUSHA_CD_From.Text) && string.IsNullOrEmpty(this.form.GYOUSHA_CD_To.Text))
            {
                GYOUSHA_CD = "全て";
            }
            else
            {
                GYOUSHA_CD = this.form.GYOUSHA_CD_From.Text + " ～ " + this.form.GYOUSHA_CD_To.Text;
            }
            // 受入現場
            if (string.IsNullOrEmpty(this.form.GENBA_CD_From.Text) && string.IsNullOrEmpty(this.form.GENBA_CD_To.Text))
            {
                GENBA_CD = "全て";
            }
            else
            {
                GENBA_CD = this.form.GENBA_CD_From.Text + " ～ " + this.form.GENBA_CD_To.Text;
            }
            // 受入品名
            if (string.IsNullOrEmpty(this.form.HINMEI_CD_From.Text) && string.IsNullOrEmpty(this.form.HINMEI_CD_To.Text))
            {
                HINMEI_CD = "全て";
            }
            else
            {
                HINMEI_CD = this.form.HINMEI_CD_From.Text + " ～ " + this.form.HINMEI_CD_To.Text;
            }


            // 出荷取引先
            if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_Export_From.Text) && string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_Export_To.Text))
            {
                TORIHIKISAKI_CD_Export = "全て";
            }
            else
            {
                TORIHIKISAKI_CD_Export = this.form.TORIHIKISAKI_CD_Export_From.Text + " ～ " + this.form.TORIHIKISAKI_CD_Export_To.Text;
            }
            // 出荷業者
            if (string.IsNullOrEmpty(this.form.GYOUSHA_CD_Export_From.Text) && string.IsNullOrEmpty(this.form.GYOUSHA_CD_Export_To.Text))
            {
                GYOUSHA_CD_Export = "全て";
            }
            else
            {
                GYOUSHA_CD_Export = this.form.GYOUSHA_CD_Export_From.Text + " ～ " + this.form.GYOUSHA_CD_Export_To.Text;
            }
            // 出荷現場
            if (string.IsNullOrEmpty(this.form.GENBA_CD_Export_From.Text) && string.IsNullOrEmpty(this.form.GENBA_CD_Export_To.Text))
            {
                GENBA_CD_Export = "全て";
            }
            else
            {
                GENBA_CD_Export = this.form.GENBA_CD_Export_From.Text + " ～ " + this.form.GENBA_CD_Export_To.Text;
            }
            // 出荷品名
            if (string.IsNullOrEmpty(this.form.HINMEI_CD_Export_From.Text) && string.IsNullOrEmpty(this.form.HINMEI_CD_Export_To.Text))
            {
                HINMEI_CD_Export = "全て";
            }
            else
            {
                HINMEI_CD_Export = this.form.HINMEI_CD_Export_From.Text + " ～ " + this.form.HINMEI_CD_Export_To.Text;
            }
            // 運搬業者
            if (string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD_From.Text) && string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD_To.Text))
            {
                UPN_GYOUSHA_CD = "全て";
            }
            else
            {
                UPN_GYOUSHA_CD = this.form.UPN_GYOUSHA_CD_From.Text + " ～ " + this.form.UPN_GYOUSHA_CD_To.Text;
            }

            if (windowCall == WINDOW_ID.T_DAINO_MEISAIHYOU)
            {
                dtHead.Rows.Add(new object[] { CopName,DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")+ " 発行" ,
                                            this.form.txt_KyotenName.Text,
                                            DENPYOU_DATE,                                                     
                                            TORIHIKISAKI_CD,
                                            GYOUSHA_CD,
                                            GENBA_CD,
                                            HINMEI_CD,
                                            TORIHIKISAKI_CD_Export,
                                            GYOUSHA_CD_Export,
                                            GENBA_CD_Export,
                                            HINMEI_CD_Export,
                                            UPN_GYOUSHA_CD,
                                            this.form.txt_KyotenCD.Text,
                                            });
            }
            else if (windowCall == WINDOW_ID.T_DAINO_SYUUKEIHYOU)
            {
                dtHead.Rows.Add(new object[] { CopName,DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")+ " 発行" ,
                                            this.form.txt_KyotenName.Text,
                                            DENPYOU_DATE,                                                     
                                            TORIHIKISAKI_CD,
                                            GYOUSHA_CD,
                                            GENBA_CD,
                                            HINMEI_CD,
                                            TORIHIKISAKI_CD_Export,
                                            GYOUSHA_CD_Export,
                                            GENBA_CD_Export,
                                            HINMEI_CD_Export,
                                            UPN_GYOUSHA_CD,
                                            "CON_CD_10",
                                            this.form.txt_KyotenCD.Text,
                                            });
            }
            return dtHead;
        }

        private DataTable dataFooter(DataTable dataResults)
        {
            DataTable dtFooter = this.CreateTempDataTable(TableType.Footer);
            decimal UKEIRE_SYOUMI_SOUGOUKEI = 0;
            decimal UKEIRE_KINGAKU_SOUGOUKEI = 0;
            decimal SHUKKA_SYOUMI_SOUGOUKEI = 0;
            decimal SHUKKA_KINGAKU_SOUGOUKEI = 0;
            decimal SAEKI_KINGAKU_SOUGOUKEI = 0;
            decimal UNCHIN_KINGAKU_SOUGOUKEI = 0;

            if (dataResults.Rows.Count > 0)
            {
                string DENPYOU_NUMBER = Convert.ToString(dataResults.Rows[0]["DENPYOU_NUMBER"]);

                UKEIRE_SYOUMI_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["UKEIRE_SYOUMI_GOUKEI"]);
                UKEIRE_KINGAKU_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["UKEIRE_KINGAKU_GOUKEI"]);
                SHUKKA_SYOUMI_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["SHUKKA_SYOUMI_GOUKEI"]);
                SHUKKA_KINGAKU_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["SHUKKA_KINGAKU_GOUKEI"]);
                SAEKI_KINGAKU_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["SAEKI_KINGAKU_GOUKEI"]);

                if (!String.IsNullOrEmpty(Convert.ToString(dataResults.Rows[0]["UNCHIN_KINGAKU_GOUKEI"])))
                    UNCHIN_KINGAKU_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["UNCHIN_KINGAKU_GOUKEI"]);

                for (int i = 1; i < dataResults.Rows.Count; i++)
                {
                    if (!(Convert.ToString(dataResults.Rows[i]["DENPYOU_NUMBER"]).Equals(DENPYOU_NUMBER)))
                    {
                        DENPYOU_NUMBER = Convert.ToString(dataResults.Rows[i]["DENPYOU_NUMBER"]);
                        UKEIRE_SYOUMI_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["UKEIRE_SYOUMI_GOUKEI"]);
                        UKEIRE_KINGAKU_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["UKEIRE_KINGAKU_GOUKEI"]);
                        SHUKKA_SYOUMI_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["SHUKKA_SYOUMI_GOUKEI"]);
                        SHUKKA_KINGAKU_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["SHUKKA_KINGAKU_GOUKEI"]);
                        SAEKI_KINGAKU_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["SAEKI_KINGAKU_GOUKEI"]);
                        if (!String.IsNullOrEmpty(Convert.ToString(dataResults.Rows[i]["UNCHIN_KINGAKU_GOUKEI"])))
                            UNCHIN_KINGAKU_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["UNCHIN_KINGAKU_GOUKEI"]);
                    }
                }
            }

            dtFooter.Rows.Add(new object[] {
                                                CommonCalc.DecimalFormat(UKEIRE_SYOUMI_SOUGOUKEI), 
                                                CommonCalc.DecimalFormat(UKEIRE_KINGAKU_SOUGOUKEI), 
                                                CommonCalc.DecimalFormat(SHUKKA_SYOUMI_SOUGOUKEI) ,
                                                CommonCalc.DecimalFormat(SHUKKA_KINGAKU_SOUGOUKEI) ,
                                                CommonCalc.DecimalFormat(SAEKI_KINGAKU_SOUGOUKEI) ,
                                                CommonCalc.DecimalFormat(UNCHIN_KINGAKU_SOUGOUKEI) 
                                            });
            return dtFooter;


        }
        private DataTable dataFooter488(DataTable dataResults)
        {
            DataTable dtFooter = this.CreateTempDataTable(TableType.Footer);
            decimal UKEIRE_SYOUMI_SOUGOUKEI = 0;
            decimal UKEIRE_KINGAKU_SOUGOUKEI = 0;
            decimal SHUKKA_SYOUMI_SOUGOUKEI = 0;
            decimal SHUKKA_KINGAKU_SOUGOUKEI = 0;
            decimal SAEKI_KINGAKU_SOUGOUKEI = 0;
            decimal UNCHIN_KINGAKU_SOUGOUKEI = 0;

            if (dataResults.Rows.Count > 0)
            {
                string RowID = Convert.ToString(dataResults.Rows[0]["RowID"]);

                UKEIRE_SYOUMI_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["UKEIRE_SYOUMI_TOTAL"]);
                UKEIRE_KINGAKU_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["UKEIRE_KINGAKU_TOTAL"]);
                SHUKKA_SYOUMI_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["SHUKKA_SYOUMI_TOTAL"]);
                SHUKKA_KINGAKU_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["SHUKKA_KINGAKU_TOTAL"]);
                SAEKI_KINGAKU_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["SAEK_KINGAKU_TOTAL"]);

                if (!String.IsNullOrEmpty(Convert.ToString(dataResults.Rows[0]["UNCHIN_KINGAKU_TOTAL"])))
                    UNCHIN_KINGAKU_SOUGOUKEI = Convert.ToDecimal(dataResults.Rows[0]["UNCHIN_KINGAKU_TOTAL"]);

                for (int i = 1; i < dataResults.Rows.Count; i++)
                {
                    if (!(Convert.ToString(dataResults.Rows[i]["RowID"]).Equals(RowID)))
                    {
                        RowID = Convert.ToString(dataResults.Rows[i]["RowID"]);
                        UKEIRE_SYOUMI_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["UKEIRE_SYOUMI_TOTAL"]);
                        UKEIRE_KINGAKU_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["UKEIRE_KINGAKU_TOTAL"]);
                        SHUKKA_SYOUMI_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["SHUKKA_SYOUMI_TOTAL"]);
                        SHUKKA_KINGAKU_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["SHUKKA_KINGAKU_TOTAL"]);
                        SAEKI_KINGAKU_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["SAEK_KINGAKU_TOTAL"]);

                        if (!String.IsNullOrEmpty(Convert.ToString(dataResults.Rows[i]["UNCHIN_KINGAKU_TOTAL"])))
                            UNCHIN_KINGAKU_SOUGOUKEI += Convert.ToDecimal(dataResults.Rows[i]["UNCHIN_KINGAKU_TOTAL"]);
                    }
                }
            }

            dtFooter.Rows.Add(new object[] {
                                                CommonCalc.DecimalFormat(UKEIRE_SYOUMI_SOUGOUKEI), 
                                                CommonCalc.DecimalFormat(UKEIRE_KINGAKU_SOUGOUKEI), 
                                                CommonCalc.DecimalFormat(SHUKKA_SYOUMI_SOUGOUKEI) ,
                                                CommonCalc.DecimalFormat(SHUKKA_KINGAKU_SOUGOUKEI) ,
                                                CommonCalc.DecimalFormat(SAEKI_KINGAKU_SOUGOUKEI) ,
                                                CommonCalc.DecimalFormat(UNCHIN_KINGAKU_SOUGOUKEI) 
                                            });
            return dtFooter;

        }


        private DataTable CreateTempDataTable(TableType tableType)
        {
            DataTable dtResult = new DataTable();
            string sHead403 = @"CORP_RYAKU_NAME,PRINT_DATE,KYOTEN_NAME,DENPYOU_DATE,FILL_COND_CD_1,FILL_COND_CD_2,FILL_COND_CD_3,FILL_COND_CD_4,FILL_COND_CD_5,FILL_COND_CD_6,
            FILL_COND_CD_7,FILL_COND_CD_8,FILL_COND_CD_9,KYOTEN_CD";

            //    string sHead488 = @"CORP_RYAKU_NAME,PRINT_DATE,KYOTEN_NAME,DENPYOU_DATE,COND_CD_1,COND_CD_2,COND_CD_3,COND_CD_4,COND_CD_5,COND_CD_6,
            //      COND_CD_7,COND_CD_8,COND_CD_9,COND_CD_10,KYOTEN_CD"; 

            string sHead488 = @"CORP_RYAKU_NAME,PRINT_DATE,KYOTEN_NAME,DENPYOU_DATE,FILL_COND_CD_1,FILL_COND_CD_2,FILL_COND_CD_3,FILL_COND_CD_4,FILL_COND_CD_5,FILL_COND_CD_6,
             FILL_COND_CD_7,FILL_COND_CD_8,FILL_COND_CD_9,FILL_COND_CD_10,KYOTEN_CD";


            string sFooter403 = @"UKEIRE_SYOUMI_SOUGOUKEI,UKEIRE_KINGAKU_SOUGOUKEI,SHUKKA_SYOUMI_SOUGOUKEI,SHUKKA_KINGAKU_SOUGOUKEI,SAEKI_KINGAKU_SOUGOUKEI,UNCHIN_KINGAKU_SOUGOUKEI";
            string sFooter488 = @"UKEIRE_SYOUMI_TOTAL,UKEIRE_KINGAKU_TOTAL,SHUKKA_SYOUMI_TOTAL,SHUKKA_KINGAKU_TOTAL,SAEKI_KINGAKU_TOTAL,UNCHIN_KINGAKU_TOTAL";

            string sFinal = string.Empty;
            switch (tableType)
            {
                case TableType.Header:
                    if (windowCall == WINDOW_ID.T_DAINO_MEISAIHYOU)
                        sFinal = sHead403;
                    else if (windowCall == WINDOW_ID.T_DAINO_SYUUKEIHYOU)
                        sFinal = sHead488;
                    dtResult.TableName = tableType.ToString();
                    break;
                case TableType.Footer:
                    if (windowCall == WINDOW_ID.T_DAINO_MEISAIHYOU)
                        sFinal = sFooter403;
                    else if (windowCall == WINDOW_ID.T_DAINO_SYUUKEIHYOU)
                        sFinal = sFooter488;
                    dtResult.TableName = tableType.ToString();
                    break;
            }
            string[] Cols = sFinal.Split(',');
            foreach (string col in Cols)
            {
                dtResult.Columns.Add(col.Trim(), typeof(string));
            }
            return dtResult;
        }
        #endregion


        private string GetSelectWhere()
        {
            string selectWhere = " 1=1 ";

            if (!(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_From.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] >= '{1}'", "UKEIRE_TORIHIKISAKI_CD", this.form.TORIHIKISAKI_CD_From.Text) + ")";

            }
            if (!(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_To.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] <= '{1}'", "UKEIRE_TORIHIKISAKI_CD", this.form.TORIHIKISAKI_CD_To.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.GYOUSHA_CD_From.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] >= '{1}'", "UKEIRE_GYOUSHA_CD", this.form.GYOUSHA_CD_From.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.GYOUSHA_CD_To.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] <= '{1}'", "UKEIRE_GYOUSHA_CD", this.form.GYOUSHA_CD_To.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.GENBA_CD_From.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] >= '{1}'", "UKEIRE_GENBA_CD", this.form.GENBA_CD_From.Text) + ")";
            }
            if (!(string.IsNullOrEmpty(this.form.GENBA_CD_To.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] <= '{1}'", "UKEIRE_GENBA_CD", this.form.GENBA_CD_To.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.HINMEI_CD_From.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] >= '{1}'", "UKEIRE_HINMEI_CD", this.form.HINMEI_CD_From.Text) + ")";
            }
            if (!(string.IsNullOrEmpty(this.form.HINMEI_CD_To.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] <= '{1}'", "UKEIRE_HINMEI_CD", this.form.HINMEI_CD_To.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_Export_From.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] >= '{1}'", "SHUKKA_TORIHIKISAKI_CD", this.form.TORIHIKISAKI_CD_Export_From.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_Export_To.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] <= '{1}'", "SHUKKA_TORIHIKISAKI_CD", this.form.TORIHIKISAKI_CD_Export_To.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.GYOUSHA_CD_Export_From.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] >= '{1}'", "SHUKKA_GYOUSHA_CD", this.form.GYOUSHA_CD_Export_From.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.GYOUSHA_CD_Export_To.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] <= '{1}'", "SHUKKA_GYOUSHA_CD", this.form.GYOUSHA_CD_Export_To.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.GENBA_CD_Export_From.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] >= '{1}'", "SHUKKA_GENBA_CD", this.form.GENBA_CD_Export_From.Text) + ")";
            }
            if (!(string.IsNullOrEmpty(this.form.GENBA_CD_Export_To.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] <= '{1}'", "SHUKKA_GENBA_CD", this.form.GENBA_CD_Export_To.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.HINMEI_CD_Export_From.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] >= '{1}'", "SHUKKA_HINMEI_CD", this.form.HINMEI_CD_Export_From.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.HINMEI_CD_Export_To.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] <= '{1}'", "SHUKKA_HINMEI_CD", this.form.HINMEI_CD_Export_To.Text) + ")";
            }

            if (!(string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD_From.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] >= '{1}'", "UPN_GYOUSHA_CD", this.form.UPN_GYOUSHA_CD_From.Text) + ")";
            }
            if (!(string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD_To.Text.Trim())))
            {
                selectWhere += " AND (" + string.Format("[{0}] <= '{1}'", "UPN_GYOUSHA_CD", this.form.UPN_GYOUSHA_CD_To.Text) + ")";
            }
            return selectWhere;
        }

        /// <summary>
        /// return condition search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">e</param>

        public ShukeiHyoJokenDTO ReturnSearchCondition()
        {
            ShukeiHyoJokenDTO shukeiHyoJokenDTO = new ShukeiHyoJokenDTO();
            shukeiHyoJokenDTO.M_KYOTEN_CD = this.form.txt_KyotenCD.Text;
            shukeiHyoJokenDTO.M_KYOTEN_NAME = this.form.txt_KyotenName.Text;

            shukeiHyoJokenDTO.DAINOU_ENTRY_DENPYOU_DATE_FROM = Convert.ToDateTime(this.form.DENPYOU_DATE_From.Value);
            shukeiHyoJokenDTO.DAINOU_ENTRY_DENPYOU_DATE_TO = Convert.ToDateTime(this.form.DENPYOU_DATE_To.Value);

            shukeiHyoJokenDTO.UKEIRE_ENTRY_TORIHIKISAKI_CD_FROM = this.form.TORIHIKISAKI_CD_From.Text;
            shukeiHyoJokenDTO.UKEIRE_ENTRY_TORIHIKISAKI_NAME_FROM = this.form.TORIHIKISAKI_NAME_RYAKU_From.Text;
            shukeiHyoJokenDTO.UKEIRE_ENTRY_TORIHIKISAKI_CD_TO = this.form.TORIHIKISAKI_CD_To.Text;
            shukeiHyoJokenDTO.UKEIRE_ENTRY_TORIHIKISAKI_NAME_TO = this.form.TORIHIKISAKI_NAME_RYAKU_To.Text;

            shukeiHyoJokenDTO.SHUKKA_ENTRY_TORIHIKISAKI_CD_FROM = this.form.TORIHIKISAKI_CD_Export_From.Text;
            shukeiHyoJokenDTO.SHUKKA_ENTRY_TORIHIKISAKI_NAME_FROM = this.form.TORIHIKISAKI_NAME_Export_From.Text;
            shukeiHyoJokenDTO.SHUKKA_ENTRY_TORIHIKISAKI_CD_TO = this.form.TORIHIKISAKI_CD_Export_To.Text;
            shukeiHyoJokenDTO.SHUKKA_ENTRY_TORIHIKISAKI_NAME_TO = this.form.TORIHIKISAKI_NAME_Export_To.Text;

            shukeiHyoJokenDTO.UKEIRE_ENTRY_GYOUSHA_CD_FROM = this.form.GYOUSHA_CD_From.Text;
            shukeiHyoJokenDTO.UKEIRE_ENTRY_GYOUSHA_NAME_FROM = this.form.GYOUSHA_NAME_RYAKU_From.Text;
            shukeiHyoJokenDTO.UKEIRE_ENTRY_GYOUSHA_CD_TO = this.form.GYOUSHA_CD_To.Text;
            shukeiHyoJokenDTO.UKEIRE_ENTRY_GYOUSHA_NAME_TO = this.form.GYOUSHA_NAME_RYAKU_To.Text;

            shukeiHyoJokenDTO.SHUKKA_ENTRY_GYOUSHA_CD_FROM = this.form.GYOUSHA_CD_Export_From.Text;
            shukeiHyoJokenDTO.SHUKKA_ENTRY_GYOUSHA_NAME_FROM = this.form.GYOUSHA_NAME_Export_From.Text;
            shukeiHyoJokenDTO.SHUKKA_ENTRY_GYOUSHA_CD_TO = this.form.GYOUSHA_CD_Export_To.Text;
            shukeiHyoJokenDTO.SHUKKA_ENTRY_GYOUSHA_NAME_TO = this.form.GYOUSHA_NAME_Export_To.Text;

            shukeiHyoJokenDTO.UKEIRE_ENTRY_GENBA_CD_FROM = this.form.GENBA_CD_From.Text;
            shukeiHyoJokenDTO.UKEIRE_ENTRY_GENBA_NAME_FROM = this.form.GENBA_NAME_RYAKU_From.Text;
            shukeiHyoJokenDTO.UKEIRE_ENTRY_GENBA_CD_TO = this.form.GENBA_CD_To.Text;
            shukeiHyoJokenDTO.UKEIRE_ENTRY_GENBA_NAME_TO = this.form.GENBA_NAME_RYAKU_To.Text;

            shukeiHyoJokenDTO.SHUKKA_ENTRY_GENBA_CD_FROM = this.form.GENBA_CD_Export_From.Text;
            shukeiHyoJokenDTO.SHUKKA_ENTRY_GENBA_NAME_FROM = this.form.GENBA_NAME_RYAKU_Export_From.Text;
            shukeiHyoJokenDTO.SHUKKA_ENTRY_GENBA_CD_TO = this.form.GENBA_CD_Export_To.Text;
            shukeiHyoJokenDTO.SHUKKA_ENTRY_GENBA_NAME_TO = this.form.GENBA_NAME_RYAKU_Export_To.Text;

            shukeiHyoJokenDTO.UKEIRE_DETAIL_HINMEI_CD_FROM = this.form.HINMEI_CD_From.Text;
            shukeiHyoJokenDTO.UKEIRE_DETAIL_HINMEI_NAME_FROM = this.form.HINMEI_NAME_RYAKU_From.Text;
            shukeiHyoJokenDTO.UKEIRE_DETAIL_HINMEI_CD_TO = this.form.HINMEI_CD_To.Text;
            shukeiHyoJokenDTO.UKEIRE_DETAIL_HINMEI_NAME_TO = this.form.HINMEI_NAME_RYAKU_To.Text;

            shukeiHyoJokenDTO.SHUKKA_DETAIL_HINMEI_CD_FROM = this.form.HINMEI_CD_Export_From.Text;
            shukeiHyoJokenDTO.SHUKKA_DETAIL_HINMEI_NAME_FROM = this.form.HINMEI_Name_Export_From.Text;
            shukeiHyoJokenDTO.SHUKKA_DETAIL_HINMEI_CD_TO = this.form.HINMEI_CD_Export_To.Text;
            shukeiHyoJokenDTO.SHUKKA_DETAIL_HINMEI_NAME_TO = this.form.HINMEI_Name_Export_To.Text;

            shukeiHyoJokenDTO.UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_FROM = this.form.UPN_GYOUSHA_CD_From.Text;
            shukeiHyoJokenDTO.UNCHIN_ENTRY_UNPAN_GYOUSHA_NAME_FROM = this.form.UPN_GYOUSHA_NAME_From.Text;
            shukeiHyoJokenDTO.UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_TO = this.form.UPN_GYOUSHA_CD_To.Text;
            shukeiHyoJokenDTO.UNCHIN_ENTRY_UNPAN_GYOUSHA_NAME_TO = this.form.UPN_GYOUSHA_NAME_To.Text;

            return shukeiHyoJokenDTO;
        }

        ///<summary>
        ///zero paddeng
        /// </summary>
        private string ZeroPaddeng(string sValue, int iNumberCharacter)
        {
            string sResult = "";
            int i = iNumberCharacter - sValue.Length;
            if (i > 0)
            {
                sResult = InsertNumberZero(i);
            }
            return sResult + sValue;
        }

        ///<summary>
        /// insert number zero
        /// </summary>
        private string InsertNumberZero(int numberZero)
        {
            string svalue = "";
            for (int i = 0; i < numberZero; i++)
            {
                svalue += "0";
            }

            return svalue;
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
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141201 teikyou ダブルクリックを追加する　start
        // 日付のダブルクリック
        private void DENPYOU_DATE_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var denpyouDateFromTextBox = this.form.DENPYOU_DATE_From;
            var denpyouDateToTextBox = this.form.DENPYOU_DATE_To;
            denpyouDateToTextBox.Text = denpyouDateFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 取引先のダブルクリック
        private void TORIHIKISAKI_CD_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var torikisakiCDFromTextBox = this.form.TORIHIKISAKI_CD_From;
            var torikisakiCDToTextBox = this.form.TORIHIKISAKI_CD_To;
            var torikisakiNameFromTextBox = this.form.TORIHIKISAKI_NAME_RYAKU_From;
            var torikisakiNameToTextBox = this.form.TORIHIKISAKI_NAME_RYAKU_To;
            torikisakiCDToTextBox.Text = torikisakiCDFromTextBox.Text;
            torikisakiNameToTextBox.Text = torikisakiNameFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 業者のダブルクリック
        private void GYOUSHA_CD_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCDFromTextBox = this.form.GYOUSHA_CD_From;
            var gyoushaCDToTextBox = this.form.GYOUSHA_CD_To;
            var gyoushaNameFromTextBox = this.form.GYOUSHA_NAME_RYAKU_From;
            var gyoushaNameToTextBox = this.form.GYOUSHA_NAME_RYAKU_To;
            gyoushaCDToTextBox.Text = gyoushaCDFromTextBox.Text;
            gyoushaNameToTextBox.Text = gyoushaNameFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 現場のダブルクリック
        private void GENBA_CD_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var genbaCDFromTextBox = this.form.GENBA_CD_From;
            var genbaCDToTextBox = this.form.GENBA_CD_To;
            var genbaNameFromTextBox = this.form.GENBA_NAME_RYAKU_From;
            var genbaNameToTextBox = this.form.GENBA_NAME_RYAKU_To;
            genbaCDToTextBox.Text = genbaCDFromTextBox.Text;
            genbaNameToTextBox.Text = genbaNameFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 品名のダブルクリック
        private void HINMEI_CD_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hinmeiCDFromTextBox = this.form.HINMEI_CD_From;
            var hinmeiCDToTextBox = this.form.HINMEI_CD_To;
            var hinmeiNameFromTextBox = this.form.HINMEI_NAME_RYAKU_From;
            var hinmeiNameToTextBox = this.form.HINMEI_NAME_RYAKU_To;
            hinmeiCDToTextBox.Text = hinmeiCDFromTextBox.Text;
            hinmeiNameToTextBox.Text = hinmeiNameFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 取引先Exportのダブルクリック
        private void TORIHIKISAKI_CD_Export_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var torihikisakiCDExportFromTextBox = this.form.TORIHIKISAKI_CD_Export_From;
            var torihikisakiCDExportToTextBox = this.form.TORIHIKISAKI_CD_Export_To;
            var torihikisakiNameExportFromTextBox = this.form.TORIHIKISAKI_NAME_Export_From;
            var torihikisakiNameExportToTextBox = this.form.TORIHIKISAKI_NAME_Export_To;
            torihikisakiCDExportToTextBox.Text = torihikisakiCDExportFromTextBox.Text;
            torihikisakiNameExportToTextBox.Text = torihikisakiNameExportFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 業者Exportのダブルクリック
        private void GYOUSHA_CD_Export_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCDExportFromTextBox = this.form.GYOUSHA_CD_Export_From;
            var gyoushaCDExportToTextBox = this.form.GYOUSHA_CD_Export_To;
            var gyoushaNameExportFromTextBox = this.form.GYOUSHA_NAME_Export_From;
            var gyoushaNameExportToTextBox = this.form.GYOUSHA_NAME_Export_To;
            gyoushaCDExportToTextBox.Text = gyoushaCDExportFromTextBox.Text;
            gyoushaNameExportToTextBox.Text = gyoushaNameExportFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 現場Exportのダブルクリック
        private void GENBA_CD_Export_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var genbaCDExportFromTextBox = this.form.GENBA_CD_Export_From;
            var genbaCDExportToTextBox = this.form.GENBA_CD_Export_To;
            var genbaNameExportFromTextBox = this.form.GENBA_NAME_RYAKU_Export_From;
            var genbaNameExportToTextBox = this.form.GENBA_NAME_RYAKU_Export_To;
            genbaCDExportToTextBox.Text = genbaCDExportFromTextBox.Text;
            genbaNameExportToTextBox.Text = genbaNameExportFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 品名Exportのダブルクリック
        private void HINMEI_CD_Export_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hinmeiCDExportFromTextBox = this.form.HINMEI_CD_Export_From;
            var hinmeiCDExportToTextBox = this.form.HINMEI_CD_Export_To;
            var hinmeiNameExportFromTextBox = this.form.HINMEI_Name_Export_From;
            var hinmeiNameExportToTextBox = this.form.HINMEI_Name_Export_To;
            hinmeiCDExportToTextBox.Text = hinmeiCDExportFromTextBox.Text;
            hinmeiNameExportToTextBox.Text = hinmeiNameExportFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 運搬業者のダブルクリック
        private void UPN_GYOUSHA_CD_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var upnGyoushaCDFromTextBox = this.form.UPN_GYOUSHA_CD_From;
            var upnGyoushaCDToTextBox = this.form.UPN_GYOUSHA_CD_To;
            var upnGyoushaNameFromTextBox = this.form.UPN_GYOUSHA_NAME_From;
            var upnGyoushaNameToTextBox = this.form.UPN_GYOUSHA_NAME_To;
            upnGyoushaCDToTextBox.Text = upnGyoushaCDFromTextBox.Text;
            upnGyoushaNameToTextBox.Text = upnGyoushaNameFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141201 teikyou 「モバイル将軍出力」のダブルクリックを追加する　end
        #endregion

        /// 20141203 Houkakou 日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.DENPYOU_DATE_From.BackColor = Constans.NOMAL_COLOR;
            this.form.DENPYOU_DATE_To.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.DENPYOU_DATE_From.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.DENPYOU_DATE_To.Text))
            {
                return false;
            }

            DateTime date_from = Convert.ToDateTime(this.form.DENPYOU_DATE_From.Value);
            DateTime date_to = Convert.ToDateTime(this.form.DENPYOU_DATE_To.Value);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.DENPYOU_DATE_From.IsInputErrorOccured = true;
                this.form.DENPYOU_DATE_To.IsInputErrorOccured = true;
                this.form.DENPYOU_DATE_From.BackColor = Constans.ERROR_COLOR;
                this.form.DENPYOU_DATE_To.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "伝票日付範囲指定From", "伝票日付範囲指定To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.DENPYOU_DATE_From.Focus();
                return true;
            }

            return false;
        }
        #endregion
        /// 20141203 Houkakou 日付チェックを追加する　end
    }
}