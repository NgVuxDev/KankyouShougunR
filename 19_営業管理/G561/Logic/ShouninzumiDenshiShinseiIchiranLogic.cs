using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.ShouninzumiDenshiShinseiIchiran
{
    /// <summary>
    /// G561 承認済電子申請一覧ロジック
    /// </summary>
    internal class ShouninzumiDenshiShinseiIchiranLogic : IBuisinessLogic
    {
        /// <summary>
        /// ボタン設定XMLファイルパス
        /// </summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.BusinessManagement.ShouninzumiDenshiShinseiIchiran.Setting.ShouninzumiDenshiShinseiIchiranButtonSetting.xml";

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private ShouninzumiDenshiShinseiIchiranHeaderForm header;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private ShouninzumiDenshiShinseiIchiranUIForm form;

        /// <summary>
        /// 画面のデータを保持するDTOを取得・設定します
        /// </summary>
        internal ShouninzumiDenshiShinseiIchiranDto Dto { get; set; }

        /// <summary>電子申請オプションフラグ</summary>
        internal bool EnableWorkflowOption { get; set; }

        internal MessageBoxShowLogic errmessage;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">画面クラス</param>
        public ShouninzumiDenshiShinseiIchiranLogic(ShouninzumiDenshiShinseiIchiranUIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.Dto = new ShouninzumiDenshiShinseiIchiranDto();
            this.errmessage = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 電子申請オプションフラグの設定
                // TODO:システム構成の静的フラグに置き換えること
                this.EnableWorkflowOption = true;

                // ヘッダーを初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // レイアウト調整
                this.InitWindowLayout();

                //var ribbon = (RibbonMainMenu)((BusinessBaseForm)this.form.Parent).ribbonForm;
                //this.Dto.ShainCd = ribbon.GlobalCommonInformation.CurrentShain.SHAIN_CD;
                //this.Dto.ShainName = ribbon.GlobalCommonInformation.CurrentShain.SHAIN_NAME_RYAKU;

                var configProfile = new XMLAccessor().XMLReader_CurrentUserCustomConfigProfile();
                this.Dto.KyotenCd = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));

                var kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                var kyoten = kyotenDao.GetDataByCd(configProfile.ItemSetVal1);
                if (null != kyoten)
                {
                    this.Dto.KyotenName = kyoten.KYOTEN_NAME_RYAKU;
                }
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
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            var targetHeader = (ShouninzumiDenshiShinseiIchiranHeaderForm)parentForm.headerForm;
            this.header = targetHeader;
            this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

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
            ret = buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

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
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 条件クリアボタン(F7)イベント追加
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

            // 検索ボタン(F8)イベント追加
            parentForm.bt_func8.Click += new EventHandler(this.form.ButtonFunc8_Clicked);

            // 本登録ボタン(F9)イベント追加
            parentForm.bt_func9.Click += new EventHandler(this.form.ButtonFunc9_Clicked);

            // 閉じるボタン(F12)イベント追加
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            // 20141127 teikyou ダブルクリックを追加する　start
            this.form.SHINSEI_DATE_TO.MouseDoubleClick += new MouseEventHandler(SHINSEI_DATE_TO_MouseDoubleClick);
            // 20141127 teikyou ダブルクリックを追加する　end

            /// 20141203 Houkakou 「承認済申請一覧」の日付チェックを追加する　start
            this.form.SHINSEI_DATE_FROM.Leave += new System.EventHandler(SHINSEI_DATE_FROM_Leave);
            this.form.SHINSEI_DATE_TO.Leave += new System.EventHandler(SHINSEI_DATE_TO_Leave);
            /// 20141203 Houkakou 「承認済申請一覧」の日付チェックを追加する　end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// データ抽出処理を行います
        /// </summary>
        /// <returns>抽出したデータの件数</returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;

            if (false == this.form.RegistErrorFlag)
            {
                if (this.EnableWorkflowOption)
                {
                    // 電子申請OP有り
                    var dao = DaoInitUtility.GetComponent<IShouninzumiDenshiShinseiIchiranDao>();
                    var res = dao.GetShouninzumiDenshiShinseiIchiran(this.Dto);

                    this.form.SetDenshiShinseiIchiranDataSource(res);
                    ret = res.Rows.Count;
                }
                else
                {
                    // 電子申請OP無し
                    var dao = DaoInitUtility.GetComponent<IShouninzumiDenshiShinseiIchiranDao>();
                    var res = dao.GetIchiranData(this.Dto);

                    this.form.SetDenshiShinseiIchiranDataSource(res);
                    ret = res.Rows.Count;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #region 画面レイアウトの初期化
        /// <summary>
        /// 画面レイアウトの初期化
        /// 電子申請機能はオプションであるため、オプションの値によってレイアウトを調整する
        /// </summary>
        private void InitWindowLayout()
        {
            // オプションなしの場合の表示切替
            if (!this.EnableWorkflowOption)
            {
                // 検索条件
                this.form.label1.Visible = false;
                this.form.SHAIN_CD.Visible = false;
                this.form.SHAIN_NAME.Visible = false;
                this.form.label5.Visible = false;
                this.form.SHINSEI_DATE_FROM.Visible = false;
                this.form.label6.Visible = false;
                this.form.SHINSEI_DATE_TO.Visible = false;

                // 明細
                // 電子申請OP無しで表示しない項目
                string[] disableColmunNames = { "SHINSEI_NUMBER", "SHINSEI_DATE", "NAIYOU_NAME", "SHAIN_NAME_RYAKU", "SHINSEI_STATUS", "PROGRESS" };
                // 電子申請OP無しでのみ表示する項目
                string[] visibleComunNames = { "DISP_TORIHIKISAKI_CD", "DISP_GYOUSHA_CD", "DISP_GENBA_CD" };

                foreach (DataGridViewColumn col in this.form.DENSHI_SHINSEI_ICHIRAN.Columns)
                {
                    // 表示項目の幅を広げて見栄えをよくする
                    col.Width = 200;
                }

                foreach (var disableCol in disableColmunNames)
                {
                    if (this.form.DENSHI_SHINSEI_ICHIRAN.Columns.Contains(disableCol))
                    {
                        this.form.DENSHI_SHINSEI_ICHIRAN.Columns[disableCol].Visible = false;
                    }
                }

                foreach (var visibleCol in visibleComunNames)
                {
                    if (this.form.DENSHI_SHINSEI_ICHIRAN.Columns.Contains(visibleCol))
                    {
                        this.form.DENSHI_SHINSEI_ICHIRAN.Columns[visibleCol].Visible = true;
                        this.form.DENSHI_SHINSEI_ICHIRAN.Columns[visibleCol].Width = 100;
                    }
                }
            }
        }
        #endregion

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

        /// <summary>
        /// 電子申請が移行済かをチェックします
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <returns>移行済みの場合は、true</returns>
        internal bool IsRegistDenshiShinsei(object systemId, object seq, out bool catchErr)
        {
            catchErr = true;
            LogUtility.DebugMethodStart(systemId, seq);

            var ret = false;

            try
            {
                var denshiShinseiStatus = this.GetDenshiShinseiStatus(SqlInt64.Parse(systemId.ToString()), SqlInt32.Parse(seq.ToString()));
                if (null != denshiShinseiStatus)
                {
                    if (denshiShinseiStatus.SHINSEI_STATUS_CD.Value == (int)DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.COMPLETE)
                    {
                        ret = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IsRegistDenshiShinsei", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsRegistDenshiShinsei", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }


        /// <summary>
        /// 電子申請状態エンティティを取得します
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <returns>電子申請状態エンティティ</returns>
        private T_DENSHI_SHINSEI_STATUS GetDenshiShinseiStatus(SqlInt64 systemId, SqlInt32 seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);

            T_DENSHI_SHINSEI_STATUS ret = null;

            var dao = DaoInitUtility.GetComponent<IDenshiShinseiStatusDao>();
            var entityList = dao.GetDenshiShinseiStatusList(new T_DENSHI_SHINSEI_STATUS() { SYSTEM_ID = systemId, SEQ = seq, DELETE_FLG = false });
            if (0 != entityList.Count())
            {
                ret = entityList.FirstOrDefault();
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141127 teikyou ダブルクリックを追加する　start
        private void SHINSEI_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var shinseiDateFromTextBox = this.form.SHINSEI_DATE_FROM;
            var shinseiDateToTextBox = this.form.SHINSEI_DATE_TO;
            shinseiDateToTextBox.Text = shinseiDateFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141127 teikyou ダブルクリックを追加する　end
        #endregion

        /// 20141203 Houkakou 「承認済申請一覧」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.SHINSEI_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.SHINSEI_DATE_TO.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.SHINSEI_DATE_FROM.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.SHINSEI_DATE_TO.Text))
                {
                    return false;
                }

                DateTime date_from = Convert.ToDateTime(this.form.SHINSEI_DATE_FROM.Value);
                DateTime date_to = Convert.ToDateTime(this.form.SHINSEI_DATE_TO.Value);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.SHINSEI_DATE_FROM.IsInputErrorOccured = true;
                    this.form.SHINSEI_DATE_TO.IsInputErrorOccured = true;
                    this.form.SHINSEI_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.SHINSEI_DATE_TO.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "申請日付From", "申請日付To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.SHINSEI_DATE_FROM.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }

            return false;
        }
        #endregion

        #region SHINSEI_DATE_FROM_Leaveイベント
        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void SHINSEI_DATE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.SHINSEI_DATE_TO.Text))
            {
                this.form.SHINSEI_DATE_TO.IsInputErrorOccured = false;
                this.form.SHINSEI_DATE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region SHINSEI_DATE_TO_Leaveイベント
        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void SHINSEI_DATE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.SHINSEI_DATE_FROM.Text))
            {
                this.form.SHINSEI_DATE_FROM.IsInputErrorOccured = false;
                this.form.SHINSEI_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 「承認済申請一覧」の日付チェックを追加する　end
    }
}
