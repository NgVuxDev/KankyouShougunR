using System;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Message;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi.Dao;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data.SqlTypes;
using System.Drawing;
using Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2;
using r_framework.BrowseForFolder;
using r_framework.Dto;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;

namespace Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi.Setting.ButtonSetting.xml";

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        private UIHeader headForm;
        private BusinessBaseForm footer;

        private IM_SYS_INFODao sysInfoDao;
        private M_SYS_INFO sysInfo;
        /// <summary>	
        /// 拠点マスタ	
        /// </summary>	
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// 部門マスタ
        /// </summary>
        //private IM_BUMONDao mbumonDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;
        internal r_framework.Dao.IM_TORIHIKISAKI_SEIKYUUDao torihikisakiSeikyuuDao;

        /// <summary>
        /// 銀行Dao
        /// </summary>
        private IM_BANKDao bankDao;

        /// <summary>
        /// 銀行支店Dao
        /// </summary>
        private IM_BANK_SHITENDao bankShitenDao;

        /// <summary>
        /// 入金データ取込
        /// </summary>
        private IT_NYUUKIN_DATA_TORIKOMIDao nyuukinDataTorikomiDao;

        /// <summary>
        /// 入金一括入力Dao(G459_入金入力参照、取得)
        /// </summary>
        private IT_NYUUKIN_SUM_ENTRYDao NyuukinSumEntryDao;

        /// <summary>
        /// 入金一括入力明細Dao(G459_入金入力参照、取得)
        /// </summary>
        private IT_NYUUKIN_SUM_DETAILDao NyuukinSumDetailDao;

        /// <summary>
        /// 入金入力Dao(G459_入金入力参照、取得)
        /// </summary>
        private IT_NYUUKIN_ENTRYDao NyuukinEntryDao;

        /// <summary>
        /// 入金入力明細Dao(G459_入金入力参照、取得)
        /// </summary>
        private IT_NYUUKIN_DETAILDao NyuukinDetailDao;

        /// <summary>
        /// 入金消込Dao(G459_入金入力参照、取得)
        /// </summary>
        private IT_NYUUKIN_KESHIKOMIDao NyuukinKeshikomiDao;

        ///// <summary>
        ///// 仮受金調整Dao(G459_入金入力参照、取得)
        ///// </summary>
        //private IT_KARIUKE_CHOUSEIDao KariukeChouseiDao;

        ///// <summary>
        ///// 仮受金管理(G459_入金入力参照、取得)
        ///// </summary>
        //private IT_KARIUKE_CONTROLDao KariukeContorlDao;

        /// <summary>
        /// DBAccessor
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.DBAccessor commonAccesser;

        internal bool initFlag = true;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;

        /// <summary>
        /// 
        /// </summary>
        private Control[] allControl;


        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.dto = new DTOClass();
            this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            //this.mbumonDao = DaoInitUtility.GetComponent<IM_BUMONDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKI_SEIKYUUDao>();
            this.bankDao = DaoInitUtility.GetComponent<IM_BANKDao>();
            this.bankShitenDao = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
            this.nyuukinDataTorikomiDao = DaoInitUtility.GetComponent<IT_NYUUKIN_DATA_TORIKOMIDao>();
            this.commonAccesser = new DBAccessor();

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.NyuukinSumEntryDao = DaoInitUtility.GetComponent<IT_NYUUKIN_SUM_ENTRYDao>();
            this.NyuukinSumDetailDao = DaoInitUtility.GetComponent<IT_NYUUKIN_SUM_DETAILDao>();
            this.NyuukinEntryDao = DaoInitUtility.GetComponent<IT_NYUUKIN_ENTRYDao>();
            this.NyuukinDetailDao = DaoInitUtility.GetComponent<IT_NYUUKIN_DETAILDao>();
            this.NyuukinKeshikomiDao = DaoInitUtility.GetComponent<IT_NYUUKIN_KESHIKOMIDao>();
            //this.KariukeChouseiDao = DaoInitUtility.GetComponent<IT_KARIUKE_CHOUSEIDao>();
            //this.KariukeContorlDao = DaoInitUtility.GetComponent<IT_KARIUKE_CONTROLDao>();

            this.sysInfo = sysInfoDao.GetAllDataForCode(CommonConst.SYS_ID);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            //2013.12.23 naitou upd start
            // ヘッダー（フッター）を初期化
            this.HeaderInit();

            //　ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            this.allControl = this.form.allControl;
            this.form.customDataGridView1.IsBrowsePurpose = false;
            this.form.customDataGridView1.TabStop = false;
            this.form.customDataGridView1.AllowUserToAddRows = false;
            this.form.customDataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(customDataGridView1_CellFormatting);
            this.form.customDataGridView1.DataError += new DataGridViewDataErrorEventHandler(customDataGridView1_DataError);
            this.form.customDataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(customDataGridView1_CellValidating);
            this.form.customDataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(customDataGridView1_CellPainting);
            this.form.customDataGridView1.CellEnter += new DataGridViewCellEventHandler(customDataGridView1_CellEnter);
            this.form.customDataGridView1.CellContentClick += new DataGridViewCellEventHandler(customDataGridView1_CellContentClick);
            this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(customDataGridView1_CellContentClick);
            this.form.customDataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(customDataGridView1_EditingControlShowing);
            this.form.TORIKOMI.Text = Properties.Settings.Default.TORIKOMI_PATH;
            this.form.SAKUSEI_KBN.Text = "3";
            this.form.SAKUSEI_BI_FROM.Text = string.Empty;
            this.form.SAKUSEI_BI_TO.Text = string.Empty;
            this.form.customDataGridView1.Location = new System.Drawing.Point(this.form.customDataGridView1.Location.X, this.form.KOUZA_SHURUI.Location.Y + 30);
            this.form.customDataGridView1.Size = new Size(this.form.customDataGridView1.Size.Width, 230);
            this.form.customDataGridView1.TabIndex = 70;
            this.form.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            this.form.SEL_LB_SENTAKU_SAKI.Location = new Point(this.form.SEL_LB_SENTAKU_SAKI.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 5);
            this.form.SEL_SENTAKU_SAKI.Location = new Point(this.form.SEL_SENTAKU_SAKI.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 5);
            this.form.SEL_LB_TORIHIKISAKI.Location = new Point(this.form.SEL_LB_TORIHIKISAKI.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
            this.form.SEL_TORIHIKISAKI_CD.Location = new Point(this.form.SEL_TORIHIKISAKI_CD.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
            this.form.SEL_TORIHIKISAKI_NAME_RYAKU.Location = new Point(this.form.SEL_TORIHIKISAKI_NAME_RYAKU.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
            this.form.SEL_TORIHIKISAKI_BTN.Location = new Point(this.form.SEL_TORIHIKISAKI_BTN.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
            this.form.SEL_LB_FURIKOMI_JINMEI_1.Location = new Point(this.form.SEL_LB_FURIKOMI_JINMEI_1.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 5);
            this.form.SEL_FURIKOMI_JINMEI_1.Location = new Point(this.form.SEL_FURIKOMI_JINMEI_1.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 5);
            this.form.SEL_SETTEI_BTN_1.Location = new Point(this.form.SEL_SETTEI_BTN_1.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 5);
            this.form.SEL_LB_FURIKOMI_JINMEI_2.Location = new Point(this.form.SEL_LB_FURIKOMI_JINMEI_2.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
            this.form.SEL_FURIKOMI_JINMEI_2.Location = new Point(this.form.SEL_FURIKOMI_JINMEI_2.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
            this.form.SEL_SETTEI_BTN_2.Location = new Point(this.form.SEL_SETTEI_BTN_2.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
            this.form.SEL_LB_SENTAKU_SAKI.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.SEL_SENTAKU_SAKI.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.SEL_LB_TORIHIKISAKI.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.SEL_TORIHIKISAKI_CD.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.SEL_TORIHIKISAKI_NAME_RYAKU.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.SEL_TORIHIKISAKI_BTN.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.SEL_LB_FURIKOMI_JINMEI_1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.SEL_FURIKOMI_JINMEI_1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.SEL_SETTEI_BTN_1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.SEL_LB_FURIKOMI_JINMEI_2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.SEL_FURIKOMI_JINMEI_2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.SEL_SETTEI_BTN_2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

            this.form.bt_ptn1.Location = new Point(this.form.bt_ptn1.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 55);
            this.form.bt_ptn2.Location = new Point(this.form.bt_ptn2.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 55);
            this.form.bt_ptn3.Location = new Point(this.form.bt_ptn3.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 55);
            this.form.bt_ptn4.Location = new Point(this.form.bt_ptn4.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 55);
            this.form.bt_ptn5.Location = new Point(this.form.bt_ptn5.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 55);
            this.form.bt_ptn1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.bt_ptn2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.bt_ptn3.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.bt_ptn4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.bt_ptn5.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.form.bt_ptn1.TabStop = false;
            this.form.bt_ptn2.TabStop = false;
            this.form.bt_ptn3.TabStop = false;
            this.form.bt_ptn4.TabStop = false;
            this.form.bt_ptn5.TabStop = false;

            LogUtility.DebugMethodEnd();
        }
     

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.headForm = targetHeader;

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// HeaderForm.cs設定
        /// </summary>
        /// /// <returns>hs</returns>
        public void SetHeader(UIHeader hs)
        {
            LogUtility.DebugMethodStart(hs);
            this.headForm = hs;
            LogUtility.DebugMethodEnd(hs);
        }
        #endregion

        
        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        public void EventInit()
        {
            LogUtility.DebugMethodStart();

            //Functionボタンのイベント生成
            footer.bt_func1.Click += new System.EventHandler(this.bt_func1_Click);          //汎用検索
            //this.form.C_Regist(footer.bt_func8);
            footer.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);          //検索
            footer.bt_func9.Click += new System.EventHandler(this.bt_func9_Click);          //検索
            footer.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);        //閉じる
            footer.bt_process1.Click += new EventHandler(bt_process1_Click);                //パターン一覧画面へ遷移
            footer.bt_process2.Click += new EventHandler(bt_process2_Click);
            this.form.customDataGridView1.KeyDown += new KeyEventHandler(customDataGridView1_KeyDown);
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region プロセスボタン押下処理
        private void customDataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                this.DislaySelectRowTori(this.form.customDataGridView1.CurrentRow.Index);
            }
        }

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var sysID = this.form.OpenPatternIchiran();

            if (!string.IsNullOrEmpty(sysID))
            {
                this.form.SetPatternBySysId(sysID);
                this.SearchResult = this.form.Table;
                this.form.ShowData();
            }
            LogUtility.DebugMethodEnd();

        }

        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.form.SEL_TORIHIKISAKI_CD.Text))
            {
                this.form.SEL_TORIHIKISAKI_CD.BackColor = Constans.ERROR_COLOR;
                msgLogic.MessageBoxShow("E331", "");
                this.form.SEL_TORIHIKISAKI_CD.Focus();
                return;
            }
            var toriSeikyuu = this.torihikisakiSeikyuuDao.GetDataByCd(this.form.SEL_TORIHIKISAKI_CD.Text);
            if (toriSeikyuu != null)
            {
                using (Transaction tran = new Transaction())
                {
                    toriSeikyuu.FURIKOMI_NAME1 = this.form.SEL_FURIKOMI_JINMEI_1.Text;
                    toriSeikyuu.FURIKOMI_NAME2 = this.form.SEL_FURIKOMI_JINMEI_2.Text;
                    this.torihikisakiSeikyuuDao.Update(toriSeikyuu);
                    tran.Commit();
                }
                msgLogic.MessageBoxShow("I001", "登録");
                bt_func8_Click(null, null);
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            LogUtility.DebugMethodEnd();

        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        public void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Functionデータ取込

        /// <summary>
        /// [F1]データ取込
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                this.Torikomi();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        
        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // パターンチェック
            if (this.form.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }
            var autoCheckLogic = new AutoRegistCheckLogic(this.allControl, this.allControl);
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
            if (this.form.RegistErrorFlag)
            {
                this.headForm.ReadDataNumber.Text = "0";

                this.form.ShowHeader();

                this.form.ResizeColumns();

                this.form.FormatColumns();
                var focusControl = this.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
  
            }
            else
            {
                this.Search();
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// [F9]登録
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                RegistNyuukin();

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region DB Accessor
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// 入金入力用のSYSTEM_ID採番処理
        /// </summary>
        /// <returns></returns>
        public SqlInt64 CreateSystemIdForNyuukin()
        {
            DBAccessor dBAccessor = new DBAccessor();

            int densyuKubun = (int)DENSHU_KBN.NYUUKIN;
            return dBAccessor.createSystemId((SqlInt16)densyuKubun);
        }

        /// <summary>
        /// 入金番号を取得する
        /// </summary>
        /// <returns></returns>
        public SqlInt64 GetDenshuNumberForNyuukin()
        {
            DBAccessor dBAccessor = new DBAccessor();

            int densyuKubun = (int)DENSHU_KBN.NYUUKIN;
            return dBAccessor.createDenshuNumber((SqlInt16)densyuKubun);
        }

        #endregion

        #region customDataGridView1

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly == false)
                {
                    if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name == ConstClass.COL_SAKUSEI)
                    {
                        //if ((bool)this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].EditedFormattedValue == true)
                        {
                            //this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Value = true;
                            this.form.customDataGridView1[ConstClass.COL_SAKUJO, e.RowIndex].Value = false;
                        }
                    }
                    else if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name == ConstClass.COL_SAKUJO)
                    {
                        //if ((bool)this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].EditedFormattedValue == true)
                        {
                            //this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Value = true;
                            this.form.customDataGridView1[ConstClass.COL_SAKUSEI, e.RowIndex].Value = false;
                        }
                    }
                }

                this.form._indexRow = e.RowIndex;
            }

            this.DislaySelectRowTori(this.form._indexRow);
        }

        private void DislaySelectRowTori(int RowIndex)
        {
            var row = this.form.customDataGridView1.CurrentRow;
            if (row != null && RowIndex >= 0)
            {
                var sentakuSaki = Convert.ToString(this.form.customDataGridView1[ConstClass.COL_FURIKOMI_JINMEI, RowIndex].Value);
                var toriCd = Convert.ToString(this.form.customDataGridView1[ConstClass.COL_TORIHIKISAKI_CD, RowIndex].Value);
                var toriName = Convert.ToString(this.form.customDataGridView1[ConstClass.COL_TORIHIKISAKI_NAME, RowIndex].Value);
                string furikomiJinmei_1 = string.Empty, furikomiJinmei_2 = string.Empty;
                var toriSeikyuu = this.torihikisakiSeikyuuDao.GetDataByCd(toriCd);
                if (toriSeikyuu != null)
                {
                    furikomiJinmei_1 = toriSeikyuu.FURIKOMI_NAME1;
                    furikomiJinmei_2 = toriSeikyuu.FURIKOMI_NAME2;
                }
                this.form.SEL_SENTAKU_SAKI.Text = sentakuSaki;
                this.form.SEL_TORIHIKISAKI_CD.Text = toriCd;
                this.form.SEL_TORIHIKISAKI_NAME_RYAKU.Text = toriName;
                this.form.SEL_FURIKOMI_JINMEI_1.Text = furikomiJinmei_1;
                this.form.SEL_FURIKOMI_JINMEI_2.Text = furikomiJinmei_2;
            }
            else
            {
                this.ClearSelTorikomi();
            }
        }

        internal void SetFurikomiBtn(TextBox txtSentakuSaki, TextBox txtFurikomiJinmei, string msg)
        {
            if (string.IsNullOrEmpty(txtFurikomiJinmei.Text))
            {
                txtFurikomiJinmei.Text = txtSentakuSaki.Text;
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C126", MessageBoxDefaultButton.Button1, msg) == DialogResult.Yes)
                {
                    txtFurikomiJinmei.Text = txtSentakuSaki.Text;
                }
            }
        }

        private void ClearSelTorikomi()
        {
            this.form.SEL_SENTAKU_SAKI.Text = string.Empty;
            this.form.SEL_TORIHIKISAKI_CD.Text = string.Empty;
            this.form.SEL_TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.SEL_FURIKOMI_JINMEI_1.Text = string.Empty;
            this.form.SEL_FURIKOMI_JINMEI_2.Text = string.Empty;
        }

        internal void SetDataTorihikisaki(string toriCd)
        {
            var toriSeikyuu = this.torihikisakiSeikyuuDao.GetDataByCd(toriCd);
            if (toriSeikyuu != null)
            {
                this.form.SEL_FURIKOMI_JINMEI_1.Text = toriSeikyuu.FURIKOMI_NAME1;
                this.form.SEL_FURIKOMI_JINMEI_2.Text = toriSeikyuu.FURIKOMI_NAME2;
            }
            else
            {
                this.form.SEL_FURIKOMI_JINMEI_1.Text = string.Empty;
                this.form.SEL_FURIKOMI_JINMEI_2.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.SEL_TORIHIKISAKI_CD.Text)) this.form.SEL_FURIKOMI_JINMEI_1.BackColor = Constans.NOMAL_COLOR;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customDataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name == ConstClass.COL_TORIHIKISAKI_CD)
                {
                    this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
                }
                else if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name == ConstClass.COL_SAKUSEI)
                {
                    this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Tag = "伝票を作成する明細をチェックしてください";
                }
                else if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name == ConstClass.COL_SAKUJO)
                {
                    this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Tag = "削除する場合にチェックを付けてください";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if ((e.ColumnIndex >= 0) && (e.RowIndex >= 0))
            {

                if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name == ConstClass.COL_SAKUSEI)
                {
                    string errorCd = this.form.customDataGridView1[ConstClass.COL_ERROR_CD, e.RowIndex].Value == null ? string.Empty : this.form.customDataGridView1[ConstClass.COL_ERROR_CD, e.RowIndex].Value.ToString();
                    if (!String.IsNullOrEmpty(errorCd))
                    {
                        e.PaintBackground(e.CellBounds, true);
                        Rectangle r = e.CellBounds;
                        r.Width = 14;
                        r.Height = 14;
                        r.X += e.CellBounds.Width / 2 - 7;
                        r.Y += e.CellBounds.Height / 2 - 7;
                        ControlPaint.DrawCheckBox(e.Graphics, r, ButtonState.Flat);
                        e.Handled = true;
                    }

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customDataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name == ConstClass.COL_TORIHIKISAKI_CD)
                {
                    if (!TorihikisakiValidating(e.ColumnIndex, e.RowIndex))
                    {
                        var message = new MessageBoxShowLogic();
                        message.MessageBoxShow("E012", "取引先");
                        e.Cancel = true;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customDataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {

                    string errorCd = this.form.customDataGridView1[ConstClass.COL_ERROR_CD, e.RowIndex].Value == null ? string.Empty : this.form.customDataGridView1[ConstClass.COL_ERROR_CD, e.RowIndex].Value.ToString();
                    string errorCdOrig = this.form.customDataGridView1[ConstClass.COL_ERROR_CD_ORIG, e.RowIndex].Value == null ? string.Empty : this.form.customDataGridView1[ConstClass.COL_ERROR_CD_ORIG, e.RowIndex].Value.ToString();
                    if (String.IsNullOrEmpty(errorCd))
                    {
                        this.form.customDataGridView1[ConstClass.COL_SAKUSEI, e.RowIndex].ReadOnly = false;
                        if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name == ConstClass.COL_TORIHIKISAKI_CD && (errorCdOrig == "3" || errorCdOrig == "4"))
                        {
                            //this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Constans.NOMAL_COLOR;
                            //this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Constans.FOCUSED_COLOR;
                        }
                        else
                        {
                            this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Constans.READONLY_COLOR;
                            this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Constans.READONLY_FOCUSED_COLOR;
                        }
                    }
                    else if (errorCd == "3" || errorCd == "4")
                    {
                        //this.form.customDataGridView1[ConstClass.COL_TORIHIKISAKI_CD, e.RowIndex].ReadOnly = false;
                        if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name == ConstClass.COL_TORIHIKISAKI_CD)
                        {
                            //this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Constans.NOMAL_COLOR;
                            //this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Constans.FOCUSED_COLOR;
                        }
                        else
                        {
                            this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = ConstClass.YELLOW_COLOR;
                            this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = ConstClass.YELLOW_COLOR;
                        }
                    }
                    else if (errorCd == "1" || errorCd == "2")
                    {
                        this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor =ConstClass.RED_COLOR;
                        this.form.customDataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = ConstClass.RED_COLOR;
                    }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customDataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                var dgv = (CustomDataGridView)sender;
                var control = (DataGridViewTextBoxEditingControl)e.Control;
                if (dgv.CurrentCell != null && dgv.CurrentCell.OwningColumn.Name == ConstClass.COL_TORIHIKISAKI_CD)
                {
                    control.ImeMode = ImeMode.Disable;
                }
            }

            LogUtility.DebugMethodEnd();
        }
		
        #endregion

        #region Base
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

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Validating
        /// <summary>
        /// 銀行支店リストを取得します
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <param name="bankShitenCd">銀行支店CD</param>
        /// <returns>銀行支店リスト</returns>
        internal List<M_BANK_SHITEN> GetBankShiten(string bankCd, string bankShitenCd)
        {
            LogUtility.DebugMethodStart(bankCd, bankShitenCd);

            var bankShitenList = this.bankShitenDao.GetAllValidData(new M_BANK_SHITEN() { BANK_CD = bankCd, BANK_SHITEN_CD = bankShitenCd, ISNOT_NEED_DELETE_FLG = true }).ToList();

            LogUtility.DebugMethodEnd(bankShitenList);

            return bankShitenList;
        }

        /// <summary>
        /// 銀行支店リストを取得します
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <param name="bankShitenCd">銀行支店CD</param>
        /// <returns>銀行支店リスト</returns>
        internal List<M_BANK_SHITEN> GetBankShiten(string bankCd, string bankShitenCd, string kouzaShurui, string kouzaNo)
        {
            LogUtility.DebugMethodStart(bankCd, bankShitenCd, kouzaShurui, kouzaNo);

            var bankShitenList = this.bankShitenDao.GetAllValidData(new M_BANK_SHITEN() { BANK_CD = bankCd, BANK_SHITEN_CD = bankShitenCd, KOUZA_SHURUI = kouzaShurui, KOUZA_NO = kouzaNo, ISNOT_NEED_DELETE_FLG = true }).ToList();

            LogUtility.DebugMethodEnd(bankShitenList);

            return bankShitenList;
        }

        /// <summary>
        /// 取引先リストを取得します
        /// </summary>
        /// <param name="colIdx"></param>
        /// <param name="rowIdx"></param>
        /// <returns></returns>
        public bool TorihikisakiValidating(int colIdx, int rowIdx)
        {

            string torihikisakiCd = (this.form.customDataGridView1[ConstClass.COL_TORIHIKISAKI_CD, rowIdx].EditedFormattedValue == null ? string.Empty : this.form.customDataGridView1[ConstClass.COL_TORIHIKISAKI_CD, rowIdx].EditedFormattedValue.ToString());
            if (!string.IsNullOrWhiteSpace(torihikisakiCd))
            {
                DataTable torihikiData = daoIchiran.GetTorihikisakiData(torihikisakiCd, null);
                if (torihikiData != null && torihikiData.Rows.Count == 1)
                {
                    this.form.customDataGridView1[ConstClass.COL_TORIHIKISAKI_CD, rowIdx].Value = torihikiData.Rows[0]["TORIHIKISAKI_CD"].ToString();
                    this.form.customDataGridView1[ConstClass.COL_TORIHIKISAKI_NAME, rowIdx].Value = torihikiData.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                    this.form.customDataGridView1[ConstClass.COL_NYUUKINSAKI_CD, rowIdx].Value = torihikiData.Rows[0]["NYUUKINSAKI_CD"].ToString();
                    this.form.customDataGridView1[ConstClass.COL_NYUUKINSAKI_NAME, rowIdx].Value = torihikiData.Rows[0]["NYUUKINSAKI_NAME_RYAKU"].ToString();
                    this.form.customDataGridView1[ConstClass.COL_ERROR_CD, rowIdx].Value = string.Empty;
                    this.form.customDataGridView1[ConstClass.COL_ERROR_NAME, rowIdx].Value = string.Empty;
                    this.form.customDataGridView1[ConstClass.COL_DENPYOU_SAKUSEI, rowIdx].Value = "可";
                }
                else
                {

                    return false;
                }
            }
            else
            {
                this.form.customDataGridView1[ConstClass.COL_TORIHIKISAKI_CD, rowIdx].Value = string.Empty;
                this.form.customDataGridView1[ConstClass.COL_TORIHIKISAKI_NAME, rowIdx].Value = string.Empty;
                this.form.customDataGridView1[ConstClass.COL_NYUUKINSAKI_CD, rowIdx].Value = string.Empty;
                this.form.customDataGridView1[ConstClass.COL_NYUUKINSAKI_NAME, rowIdx].Value = string.Empty;
                this.form.customDataGridView1[ConstClass.COL_ERROR_CD, rowIdx].Value = this.form.customDataGridView1[ConstClass.COL_ERROR_CD_ORIG, rowIdx].Value;
                this.form.customDataGridView1[ConstClass.COL_ERROR_NAME, rowIdx].Value = this.form.customDataGridView1[ConstClass.COL_ERROR_NAME_ORIG, rowIdx].Value;
                this.form.customDataGridView1[ConstClass.COL_DENPYOU_SAKUSEI, rowIdx].Value = "不可";
            }


            return true;
        }
        #endregion

        #region Functions
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal string ForderSearch()
        {
            LogUtility.DebugMethodStart();
            string forderPath = string.Empty;
            if (!string.IsNullOrEmpty(this.form.TORIKOMI.Text))
            {

                forderPath = this.form.TORIKOMI.Text;

            }
            if (string.IsNullOrEmpty(forderPath))
            {
                forderPath = @"C:\";
            }
            var browserForFolder = new BrowseForFolder();
            var title = "ファイルまたはフォルダの参照";
            var initialPath = forderPath;
            var windowHandle = this.form.Handle;
            var isFileSelect = false;
            var isTerminalMode = SystemProperty.IsTerminalMode;

            try
            {
                forderPath = browserForFolder.getFolderPath(title, initialPath, windowHandle, isFileSelect);
            }
            catch (Exception ex)
            {
                forderPath = "";
            }

            LogUtility.DebugMethodEnd();

            return forderPath.ToString();
        }

        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();


            //検索実行
            this.SearchResult = new DataTable();

            dto = new DTOClass();
            if (this.form.SAKUSEI_BI_FROM.Value != null)
            {
                dto.YonyuuDateFrom = (DateTime)this.form.SAKUSEI_BI_FROM.Value;
            }
            if (this.form.SAKUSEI_BI_TO.Value != null)
            {
                dto.YonyuuDateTo = (DateTime)this.form.SAKUSEI_BI_TO.Value;
            }

            DataTable dataTorikomi = daoIchiran.GetNyuukinDataTorikomi(dto);
            DataTable dataResult = this.form.Table.Clone();
            dataResult.Columns[ConstClass.COL_SAKUSEI].DataType = typeof(bool);
            dataResult.Columns[ConstClass.COL_SAKUJO].DataType = typeof(bool);
            dataResult.Columns.Add(ConstClass.COL_KOUZA_NAME);
            dataResult.Columns.Add(ConstClass.COL_TEKIYOU_NAIYOU);
            dataResult.Columns.Add(ConstClass.COL_TORIKOMI_NUMBER, typeof(long));
            dataResult.Columns.Add(ConstClass.COL_ROW_NUMBER,typeof(int));
            dataResult.Columns.Add(ConstClass.COL_TIME_STAMP);
            dataResult.Columns.Add(ConstClass.COL_ERROR_CD);
            dataResult.Columns.Add(ConstClass.COL_ERROR_CD_ORIG);
            dataResult.Columns.Add(ConstClass.COL_ERROR_NAME_ORIG);
            dataResult.Columns[ConstClass.COL_KINGAKU].DataType = typeof(decimal);
            foreach (DataRow rowTorikomi in dataTorikomi.Rows)
            {
                DataRow newRow = dataResult.NewRow();
                newRow[ConstClass.COL_SAKUSEI] = false;
                newRow[ConstClass.COL_SAKUJO] = false;
                newRow[ConstClass.COL_TORIKOMI_NUMBER] = rowTorikomi["TORIKOMI_NUMBER"];
                newRow[ConstClass.COL_ROW_NUMBER] = rowTorikomi["ROW_NUMBER"];
                newRow[ConstClass.COL_TIME_STAMP] = ConvertStrByte.ByteToString((byte[])rowTorikomi["TIME_STAMP"]);
                newRow[ConstClass.COL_FURIKOMI_JINMEI] = rowTorikomi["FURIKOMI_JINMEI"];
                newRow[ConstClass.COL_TEKIYOU_NAIYOU] = rowTorikomi["TEKIYOU_NAIYOU"];
                newRow[ConstClass.COL_YONYUU_DATE] = ((DateTime)rowTorikomi["YONYUU_DATE"]).ToString("yyyy/MM/dd(ddd)");
                newRow[ConstClass.COL_KINGAKU] = Decimal.Parse(rowTorikomi["KINGAKU"].ToString());
                var bankList = bankDao.GetAllValidData(new M_BANK() { RENKEI_CD = rowTorikomi["BANK_RENKEI_CD"].ToString() });
                if (bankList != null && bankList.Length == 1)
                {
                    newRow[ConstClass.COL_BANK_CD] = bankList[0].BANK_CD;
                    newRow[ConstClass.COL_BANK_NAME] = bankList[0].BANK_NAME_RYAKU;

                    var bankShitenList = bankShitenDao.GetAllValidData(new M_BANK_SHITEN() { BANK_CD = bankList[0].BANK_CD, KOUZA_NO = rowTorikomi["KOUZA_NO"].ToString(), RENKEI_CD = rowTorikomi["BANK_SHITEN_RENKEI_CD"].ToString() });
                    if (bankShitenList != null && bankShitenList.Length == 1)
                    {
                        newRow[ConstClass.COL_BANK_SHITEN_CD] = bankShitenList[0].BANK_SHITEN_CD;
                        newRow[ConstClass.COL_BANK_SHITEN_NAME] = bankShitenList[0].BANK_SHIETN_NAME_RYAKU;
                        newRow[ConstClass.COL_KOUZA_SHURUI] = bankShitenList[0].KOUZA_SHURUI;
                        newRow[ConstClass.COL_KOUZA_NO] = bankShitenList[0].KOUZA_NO;
                        newRow[ConstClass.COL_KOUZA_NAME] = bankShitenList[0].KOUZA_NAME;
                    }
                    else
                    {
                        newRow[ConstClass.COL_ERROR_CD] = 2;
                        newRow[ConstClass.COL_ERROR_NAME] = "銀行支店が登録されていません。";
                    }
                }
                else
                {
                    newRow[ConstClass.COL_ERROR_CD] = 1;
                    newRow[ConstClass.COL_ERROR_NAME] = "銀行が登録されていません。";
                }

                DataTable torihikiData = daoIchiran.GetTorihikisakiData(null, rowTorikomi["FURIKOMI_JINMEI"].ToString());
                if (torihikiData != null && torihikiData.Rows.Count == 1)
                {
                    newRow[ConstClass.COL_TORIHIKISAKI_CD] = torihikiData.Rows[0]["TORIHIKISAKI_CD"].ToString();
                    newRow[ConstClass.COL_TORIHIKISAKI_NAME] = torihikiData.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                    newRow[ConstClass.COL_NYUUKINSAKI_CD] = torihikiData.Rows[0]["NYUUKINSAKI_CD"].ToString();
                    newRow[ConstClass.COL_NYUUKINSAKI_NAME] = torihikiData.Rows[0]["NYUUKINSAKI_NAME_RYAKU"].ToString();
                }
                else if (torihikiData != null && torihikiData.Rows.Count > 1)
                {
                    if (String.IsNullOrEmpty(newRow[ConstClass.COL_ERROR_CD].ToString()))
                    {
                        newRow[ConstClass.COL_ERROR_CD] = 4;
                        newRow[ConstClass.COL_ERROR_NAME] = "振込人名が重複して登録されています。";
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(newRow[ConstClass.COL_ERROR_CD].ToString()))
                    {
                        newRow[ConstClass.COL_ERROR_CD] = 3;
                        newRow[ConstClass.COL_ERROR_NAME] = "振込人名が登録されていません。";
                    }
                }
                if (!String.IsNullOrEmpty(newRow[ConstClass.COL_ERROR_CD].ToString()))
                {
                    newRow[ConstClass.COL_DENPYOU_SAKUSEI] = "不可";
                }
                else
                {
                    newRow[ConstClass.COL_DENPYOU_SAKUSEI] = "可";
                }
                newRow[ConstClass.COL_ERROR_CD_ORIG] = newRow[ConstClass.COL_ERROR_CD];
                newRow[ConstClass.COL_ERROR_NAME_ORIG] = newRow[ConstClass.COL_ERROR_NAME];
                dataResult.Rows.Add(newRow);
            }
            string filter = string.Empty;
            if (this.form.SAKUSEI_KBN.Text == "1")
            {
                filter = "伝票作成 = '可' ";
            }
            else if (this.form.SAKUSEI_KBN.Text == "2")
            {
                filter = "伝票作成 = '不可' ";
            }
            if (!String.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                if (!String.IsNullOrWhiteSpace(filter))
                    filter += " AND ";
                filter += " 取引先CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ";
            }
            if (!String.IsNullOrEmpty(this.form.BANK_CD.Text))
            {
                if (!String.IsNullOrWhiteSpace(filter))
                    filter += " AND ";
                filter += " 銀行CD = '" + this.form.BANK_CD.Text + "' ";
            }
            if (!String.IsNullOrEmpty(this.form.BANK_SHITEN_CD.Text))
            {
                if (!String.IsNullOrWhiteSpace(filter))
                    filter += " AND ";
                filter += " 銀行支店CD = '" + this.form.BANK_SHITEN_CD.Text + "' ";
            }
            if (!String.IsNullOrEmpty(this.form.KOUZA_SHURUI.Text))
            {
                if (!String.IsNullOrWhiteSpace(filter))
                    filter += " AND ";
                filter += " 口座種類 = '" + this.form.KOUZA_SHURUI.Text + "' ";
            }
            if (!String.IsNullOrEmpty(this.form.KOUZA_NO.Text))
            {
                if (!String.IsNullOrWhiteSpace(filter))
                    filter += " AND ";
                filter += " 口座番号 = '" + this.form.KOUZA_NO.Text + "' ";
            }
            string order =  this.form.OrderByQuery.Replace('"',' ');
            DataRow[] rowResult = dataResult.Select(filter, order);
            if (rowResult.Length > 0)
            {
                this.SearchResult = rowResult.CopyToDataTable();
            }
            else
            {
                this.SearchResult = dataResult.Clone();
            }
            int count = SearchResult.Rows.Count;

            //2013.12.15 naitou upd start
            //読込データ件数を取得
            this.headForm.ReadDataNumber.Text = count.ToString();
            this.form.ShowData();
            this.form.customDataGridView1.TabStop = true;
            if (count == 0)
            {
                MessageBoxUtility.MessageBoxShow("C001");
                this.form.customDataGridView1.TabStop = false;
            }
            else
            {
                if (this.form.customDataGridView1[ConstClass.COL_SAKUSEI, 0].ReadOnly == false)
                {
                    this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[ConstClass.COL_SAKUSEI, 0];
                }
                else
                {
                    this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[ConstClass.COL_SAKUJO, 0];
                }
                this.DislaySelectRowTori(this.form.customDataGridView1.CurrentRow.Index);
            }
            LogUtility.DebugMethodEnd();
            return SearchResult.Rows.Count;
        }
        /// <summary>
        /// データ取込
        /// </summary>
        private void Torikomi()
        {
            LogUtility.DebugMethodStart();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.form.TORIKOMI.Text))
            {
                msgLogic.MessageBoxShow("E001", "取込先");
                return;
            }
            DirectoryInfo folder = new DirectoryInfo(this.form.TORIKOMI.Text);
            if (!folder.Exists)
            {
                msgLogic.MessageBoxShow("E264");
                return;
            }
            FileInfo[] fileInfoAllList = folder.GetFiles();
            if (fileInfoAllList.Length == 0)
            {
                msgLogic.MessageBoxShow("E270");
                return;
            }
            //foreach (FileInfo fileInfo in fileInfoList)
            //{
            //    if (fileInfo.Extension.ToUpper() != ".TXT")
            //    {
            //        msgLogic.MessageBoxShow("E265");
            //        return;
            //    }
            //}
            FileInfo[] fileInfoList = folder.GetFiles("*.txt");
            if (fileInfoList.Length == 0)
            {
                msgLogic.MessageBoxShow("E265");
                return;
            }
            long fileNumber = 0;
            bool errorFlg = false;
            bool errorDataFlg = false;
            bool errorFormatFlg = false;
            bool errorDataDetailFlg = false;
            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            List<T_NYUUKIN_DATA_TORIKOMI> nyuukinDataTorikomiList = new List<T_NYUUKIN_DATA_TORIKOMI>();
            foreach (FileInfo fileInfoTxt in fileInfoList)
            {
                bool dataKbn_1_Flg = false;
                int dataKbn_1_Count = 0;
                bool dataKbn_8_Flg = false;
                int dataKbn_8_Count = 0;
                bool dataKbn_9_Flg = false;
                bool dataKbn_2_Flg = false;
                bool dataValid = false;
                using (StreamReader reader = new StreamReader(fileInfoTxt.FullName, encoding))
                {
                    string bankRenkeiCd = string.Empty;
                    string bankShitenRenkeiCd = string.Empty;
                    string kouzaNo = string.Empty;
                    string fileContent = reader.ReadLine();
                    while (!String.IsNullOrEmpty(fileContent))
                    {

                        var byteArray = encoding.GetBytes(fileContent);
                        if (byteArray.Length != ConstClass.TORIKOMI_DATA_LENGTH)
                        {
                            errorFlg = true;
                            break;
                        }
                        string dataKbn = GetTorikomiDataValue(byteArray, ConstClass.TORIKOMI_DATA_KBN, encoding).Trim();
                        if (string.IsNullOrEmpty(dataKbn))
                        {
                            errorFlg = true;
                            break;
                        }
                        try
                        {
                            switch (dataKbn)
                            {
                                case "1":
                                    {

                                        //if (dataKbn_1_Flg)
                                        //{
                                        //    errorFlg = true;
                                        //    break;
                                        //}
                                        //if (!errorFlg)
                                        //{
                                        string subetsuCd = GetTorikomiDataValue(byteArray, ConstClass.TORIKOMI_SUBETSU_CD, encoding);
                                        if (subetsuCd != "03")
                                        {
                                            errorFlg = true;
                                            break;
                                        }
                                        if (!errorFlg)
                                        {
                                            bankRenkeiCd = GetTorikomiDataValue(byteArray, ConstClass.TORIKOMI_BANK_RENKEI_CD, encoding).Trim();
                                            bankShitenRenkeiCd = GetTorikomiDataValue(byteArray, ConstClass.TORIKOMI_BANK_SHITEN_RENKEI_CD, encoding).Trim();
                                            kouzaNo = GetTorikomiDataValue(byteArray, ConstClass.TORIKOMI_KOUZA_NO, encoding).Trim();
                                            if (String.IsNullOrEmpty(bankRenkeiCd) || String.IsNullOrEmpty(bankShitenRenkeiCd) || String.IsNullOrEmpty(kouzaNo))
                                            {
                                                errorFlg = true;
                                                break;
                                            }
                                            dataKbn_1_Flg = true;
                                            dataKbn_1_Count += 1;

                                        }
                                        //}
                                        break;
                                    }
                                case "8":
                                    {
                                        //if (dataKbn_8_Flg)
                                        //{
                                        //    errorFlg = true;
                                        //    break;
                                        //}
                                        //if (!errorFlg)
                                        //{
                                        dataKbn_8_Flg = true;
                                        dataKbn_8_Count += 1;
                                        //}
                                        break;
                                    }
                                case "9":
                                    {
                                        if (dataKbn_9_Flg)
                                        {
                                            errorFlg = true;
                                            break;
                                        }
                                        if (!errorFlg)
                                        {
                                            dataKbn_9_Flg = true;
                                        }
                                        break;
                                    }
                                case "2":
                                    {
                                        dataKbn_2_Flg = true;
                                        string haraisashiKbn = GetTorikomiDataValue(byteArray, ConstClass.TORIKOMI_HARAIDASHI_KBN, encoding).Trim();
                                        string torihikiKbn = GetTorikomiDataValue(byteArray, ConstClass.TORIKOMI_TORIHIKI_KBN, encoding).Trim();
                                        string yonyuuDate = GetTorikomiDataValue(byteArray, ConstClass.TORIKOMI_YONYUU_DATE, encoding).Trim();
                                        string kingaku = GetTorikomiDataValue(byteArray, ConstClass.TORIKOMI_KINGAKU, encoding).Trim();
                                        string furikomiJinmei = GetTorikomiDataValue(byteArray, ConstClass.TORIKOMI_FURIKOMI_JINMEI, encoding).Trim();
                                        string tekiyouNaiyou = GetTorikomiDataValue(byteArray, ConstClass.TORIKOMI_TEKIYOU_NAIYOU, encoding).Trim();
                                        //if (String.IsNullOrEmpty(haraisashiKbn) || String.IsNullOrEmpty(torihikiKbn) || String.IsNullOrEmpty(yonyuuDate) || String.IsNullOrEmpty(kingaku) || String.IsNullOrEmpty(furikomiJinmei))
                                        //{
                                        //    errorFlg = true;
                                        //    break;
                                        //}
                                        //if (haraisashiKbn == "1" && torihikiKbn == "11")
                                        if (haraisashiKbn == "1" && torihikiKbn == "11" && !String.IsNullOrEmpty(haraisashiKbn) && !String.IsNullOrEmpty(torihikiKbn) && !String.IsNullOrEmpty(yonyuuDate) && !String.IsNullOrEmpty(kingaku) && !String.IsNullOrEmpty(furikomiJinmei))
                                        {
                                            T_NYUUKIN_DATA_TORIKOMI nyuukinDataTorikomi = new T_NYUUKIN_DATA_TORIKOMI();
                                            nyuukinDataTorikomi.TORIKOMI_NUMBER = fileNumber;
                                            nyuukinDataTorikomi.BANK_RENKEI_CD = bankRenkeiCd;
                                            nyuukinDataTorikomi.BANK_SHITEN_RENKEI_CD = bankShitenRenkeiCd;
                                            nyuukinDataTorikomi.KOUZA_NO = kouzaNo.Substring(3);
                                            DateTime parseDate;
                                            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ja-JP", true);
                                            culture.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();

                                            string gengou = string.Empty;
                                            string warekiNow = DateTime.Now.ToString("yy/M/d", culture).Substring(0, 2);

                                            if (int.Parse(yonyuuDate.Substring(0, 2)) > int.Parse(warekiNow))
                                            {
                                                // 一つ前の元号を設定
                                                var tempEra = culture.DateTimeFormat.Calendar.GetEra(DateTime.Now) - 1;
                                                gengou = culture.DateTimeFormat.GetAbbreviatedEraName(tempEra);
                                            }
                                            else
                                            {
                                                // Nowの元号
                                                gengou = DateTime.Now.ToString("ggyy/M/d", culture).Substring(0, 2);
                                            }

                                            string tempYonyuuDate = yonyuuDate.Insert(2, "/").Insert(5, "/");
                                            tempYonyuuDate = gengou + tempYonyuuDate;

                                            if (DateTime.TryParse(tempYonyuuDate, culture.DateTimeFormat, System.Globalization.DateTimeStyles.None, out parseDate))
                                            {
                                                nyuukinDataTorikomi.YONYUU_DATE = parseDate;
                                            }
                                            else
                                            {
                                                errorFormatFlg = true;
                                                break;
                                            }
                                            Decimal parseKingaku;
                                            if (Decimal.TryParse(kingaku, out parseKingaku))
                                            {
                                                nyuukinDataTorikomi.KINGAKU = parseKingaku;
                                            }
                                            else
                                            {
                                                errorFormatFlg = true;
                                                break;
                                            }
                                            nyuukinDataTorikomi.FURIKOMI_JINMEI = furikomiJinmei;
                                            nyuukinDataTorikomi.TEKIYOU_NAIYOU = tekiyouNaiyou;
                                            nyuukinDataTorikomi.DELETE_FLG = false;
                                            nyuukinDataTorikomiList.Add(nyuukinDataTorikomi);
                                            dataValid = true;
                                        }
                                        else
                                        {
                                            errorDataDetailFlg = true;
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                        catch
                        {
                            errorFormatFlg = true;
                        }
                        if (errorFlg)
                        {
                            break;
                        }
                        fileContent = reader.ReadLine();
                    }
                }
                if (errorFlg)
                {
                    break;
                }
                if (!dataKbn_1_Flg || !dataKbn_2_Flg || !dataKbn_8_Flg || !dataKbn_9_Flg)
                {
                    errorFlg = true;
                    break;
                }
                if (dataKbn_1_Count != dataKbn_8_Count)
                {
                    errorFlg = true;
                    break;
                }
                if (!dataValid)
                {
                    errorDataFlg = true;
                }
                fileNumber = fileNumber + 1;


            }
            if (errorFlg)
            {
                msgLogic.MessageBoxShow("E266");
                return;
            }
            if (errorFormatFlg)
            {
                msgLogic.MessageBoxShow("E267");
                return;
            }
            if (errorDataFlg)
            {
                msgLogic.MessageBoxShow("E268");
                return;
            }
            using (Transaction tran = new Transaction())
            {
                //Start Sontt #18209 20160518
                string folderBakup = this.form.TORIKOMI.Text + "/Backup";
                if (!Directory.Exists(folderBakup))
                {
                    Directory.CreateDirectory(folderBakup);
                }
                DirectoryInfo directoryBackup = new DirectoryInfo(folderBakup);
                foreach (FileInfo fileOld in directoryBackup.GetFiles())
                {
                    fileOld.Delete();
                }
                foreach (FileInfo fileInfoTxt in fileInfoList)
                {
                    string fileBackup = folderBakup + "/" + fileInfoTxt.Name;
                    int i = 1;
                    while (File.Exists(fileBackup))
                    {
                        fileBackup = folderBakup + "/" + Path.GetFileNameWithoutExtension(fileInfoTxt.FullName) + "(" + i.ToString() + ")" + fileInfoTxt.Extension;
                        i++;
                    }
                    File.Move(fileInfoTxt.FullName, fileBackup);
                }
                //End Sontt #18209 20160518

                Int64 prevFileNumber = nyuukinDataTorikomiList[0].TORIKOMI_NUMBER.Value;
                SqlInt64 prevTorikomiNumber = commonAccesser.createDenshuNumber(((Int16)DENSHU_KBN.NYUUKIN_DATA_TORIKOMI_ICHIRAN));
                Int32 rowNumber = 1;
                foreach (var nyuukinDataTorikomi in nyuukinDataTorikomiList)
                {
                    if (nyuukinDataTorikomi.TORIKOMI_NUMBER.Value == prevFileNumber)
                    {
                        nyuukinDataTorikomi.TORIKOMI_NUMBER = prevTorikomiNumber;
                        nyuukinDataTorikomi.ROW_NUMBER = rowNumber;
                        rowNumber = rowNumber + 1;
                    }
                    else
                    {
                        prevFileNumber = nyuukinDataTorikomi.TORIKOMI_NUMBER.Value;
                        prevTorikomiNumber = commonAccesser.createDenshuNumber(((Int16)DENSHU_KBN.NYUUKIN_DATA_TORIKOMI_ICHIRAN));
                        rowNumber = 1;
                        nyuukinDataTorikomi.TORIKOMI_NUMBER = prevTorikomiNumber;
                        nyuukinDataTorikomi.ROW_NUMBER = rowNumber;
                        rowNumber = rowNumber + 1;
                    }
                    nyuukinDataTorikomi.DELETE_FLG = false;
                    var dataBindert = new DataBinderLogic<T_NYUUKIN_DATA_TORIKOMI>(nyuukinDataTorikomi);
                    dataBindert.SetSystemProperty(nyuukinDataTorikomi, false);
                    nyuukinDataTorikomiDao.Insert(nyuukinDataTorikomi);
                }
                tran.Commit();
            }
            
            if (errorDataDetailFlg)
            {
                msgLogic.MessageBoxShow("I024");
            }
            else
            {
                msgLogic.MessageBoxShow("I023");
            }
            Properties.Settings.Default.TORIKOMI_PATH = this.form.TORIKOMI.Text;

            Properties.Settings.Default.Save();

            bt_func8_Click(null, null);

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteArrayData"></param>
        /// <param name="torikomiDataStruct"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private string GetTorikomiDataValue(byte[] byteArrayData, TorikomiDataStruct torikomiDataStruct, Encoding encoding)
        {
            return encoding.GetString(byteArrayData, torikomiDataStruct.Position - 1, torikomiDataStruct.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="yonyuuDate"></param>
        /// <returns></returns>
        private DataTable GetMinKeshikomiData(string torihikisakiCd, DateTime yonyuuDate)
        {
            DataTable keshikomiData = this.daoIchiran.GetKeshikomiData(torihikisakiCd, yonyuuDate);
            DataTable minKeshikomiData = keshikomiData.Clone();
            if (keshikomiData.Rows.Count > 0)
            {
                string seikyuuNumber = keshikomiData.Rows[0]["SEIKYUU_NUMBER"].ToString();
                string seikyuuDate = keshikomiData.Rows[0]["SEIKYUU_DATE"].ToString();
                foreach (DataRow row in keshikomiData.Rows)
                {
                    //if (row["SEIKYUU_NUMBER"].ToString() == seikyuuNumber || row["SEIKYUU_DATE"].ToString() == seikyuuDate)
                    if (row["SEIKYUU_NUMBER"].ToString() == seikyuuNumber)
                    {
                        minKeshikomiData.Rows.Add(row.ItemArray);
                    }
                }
            }
            return minKeshikomiData;
        }
        /// <summary>
        /// 登録
        /// </summary>
        private void RegistNyuukin()
        {
            LogUtility.DebugMethodStart();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            const string CONST_KYOTEN_CD = "拠点CD";
            const string CONST_BUMON_CD = "部門CD";
            Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile userProfile = Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile.Load();
            string kyotenCd = this.GetUserProfileValue(userProfile, CONST_KYOTEN_CD);
            String BUMON_CD = this.GetUserProfileValue(userProfile, CONST_BUMON_CD);
            SqlInt16 KYOTEN_CD = SqlInt16.Null;
            if (!String.IsNullOrEmpty(kyotenCd))
            {
                KYOTEN_CD = Int16.Parse(kyotenCd);
            }

            // 月次処理中チェック
            if (this.CheckGetsujiShoriChu())
            {
                msgLogic.MessageBoxShow("E224", "実行");
                return;
            }

            // 月次処理ロックチェック
            if (this.CheckGetsujiShoriLock())
            {
                msgLogic.MessageBoxShow("E223", "実行");
                return;
            }

            // 締済期間チェック
            if (!this.ShimeiDateCheck(kyotenCd))
            {
                return;
            }
          

            List<T_NYUUKIN_SUM_ENTRY> createNyuukinSumEntryEntityList = new List<T_NYUUKIN_SUM_ENTRY>();
            List<T_NYUUKIN_SUM_DETAIL> createNyuukinSumDetailEntityList = new List<T_NYUUKIN_SUM_DETAIL>();
            List<T_NYUUKIN_ENTRY> createNyuukinEntryEntityList = new List<T_NYUUKIN_ENTRY>();
            List<T_NYUUKIN_DETAIL> createNyuukinDetailEntityList = new List<T_NYUUKIN_DETAIL>();
            List<T_NYUUKIN_KESHIKOMI> createNyuukinKeshikomiEntityList = new List<T_NYUUKIN_KESHIKOMI>();
            //List<T_KARIUKE_CHOUSEI> createKariukeChouseiEntityList = new List<T_KARIUKE_CHOUSEI>();
            //List<T_KARIUKE_CONTROL> createKariukeControlEntityList = new List<T_KARIUKE_CONTROL>();
            List<T_NYUUKIN_DATA_TORIKOMI> torikomiDeleteList = new List<T_NYUUKIN_DATA_TORIKOMI>();
            List<DuplicateNyuukinDTOClass> DuplicateNyuukinList = new List<DuplicateNyuukinDTOClass>();

            for (int i = 0; i < this.form.customDataGridView1.RowCount; i++)
            {
                if (this.form.customDataGridView1[ConstClass.COL_SAKUSEI, i].Value != null && (bool)this.form.customDataGridView1[ConstClass.COL_SAKUSEI, i].Value)
                {

                    T_NYUUKIN_DATA_TORIKOMI torikomi = new T_NYUUKIN_DATA_TORIKOMI();
                    torikomi.TORIKOMI_NUMBER = Int64.Parse(this.form.customDataGridView1[ConstClass.COL_TORIKOMI_NUMBER, i].Value.ToString());
                    torikomi.ROW_NUMBER = Int32.Parse(this.form.customDataGridView1[ConstClass.COL_ROW_NUMBER, i].Value.ToString());
                    torikomi.TIME_STAMP = ConvertStrByte.StringToByte(this.form.customDataGridView1[ConstClass.COL_TIME_STAMP, i].Value.ToString());
                    torikomiDeleteList.Add(torikomi);
                    //--------------------------------------------------------------------------------------------
                    DateTime yonyuuDate = DateTime.Parse(this.form.customDataGridView1[ConstClass.COL_YONYUU_DATE, i].Value.ToString());
                    string torihikisakiCd = this.form.customDataGridView1[ConstClass.COL_TORIHIKISAKI_CD, i].Value.ToString();
                    string nyuukinsakiCd = this.form.customDataGridView1[ConstClass.COL_NYUUKINSAKI_CD, i].Value.ToString();
                    decimal kingaku = decimal.Parse(this.form.customDataGridView1[ConstClass.COL_KINGAKU, i].Value.ToString());
                    string bankCd = this.form.customDataGridView1[ConstClass.COL_BANK_CD, i].Value.ToString();
                    string bankShitenCd = this.form.customDataGridView1[ConstClass.COL_BANK_SHITEN_CD, i].Value.ToString();
                    string kouzaShurui = this.form.customDataGridView1[ConstClass.COL_KOUZA_SHURUI, i].Value.ToString();
                    string kouzaNo = this.form.customDataGridView1[ConstClass.COL_KOUZA_NO, i].Value.ToString();
                    string kouzaName = this.form.customDataGridView1[ConstClass.COL_KOUZA_NO, i].Value.ToString();
                    string tekiyouNaiyou = this.form.customDataGridView1[ConstClass.COL_TEKIYOU_NAIYOU, i].Value.ToString();
                    bool keshikomiFlg = false;
                    decimal chouseiKingaku = 0;
                    //-----------------------------------------------------------------------------------------------------
                    //DataTable keshikomiData = this.daoIchiran.GetKeshikomiData(torihikisakiCd, yonyuuDate);
                    DataTable keshikomiData = GetMinKeshikomiData(torihikisakiCd, yonyuuDate); 
                    decimal mikeshikomiKingakuTotal = 0;
                    foreach (DataRow row in keshikomiData.Rows)
                    {
                        if (!String.IsNullOrEmpty(row["MIKESHIKOMI_KINGAKU"].ToString()) && decimal.Parse(row["MIKESHIKOMI_KINGAKU"].ToString()) != 0)
                        {
                            decimal miKeshikomiKingaku = decimal.Parse(row["MIKESHIKOMI_KINGAKU"].ToString());
                            mikeshikomiKingakuTotal += miKeshikomiKingaku;
                        }
                    }
                    if (mikeshikomiKingakuTotal >= kingaku)
                    {
                        chouseiKingaku = mikeshikomiKingakuTotal - kingaku;
                        if (chouseiKingaku > 0)
                        {
                            keshikomiFlg = true;
                            if (sysInfo != null && !sysInfo.NYUUKIN_HANDAN_BEGIN.IsNull)
                            {
                                if (chouseiKingaku < decimal.Parse(sysInfo.NYUUKIN_HANDAN_BEGIN.Value.ToString()))
                                {
                                    chouseiKingaku = 0;
                                    keshikomiFlg = false;
                                }
                            }
                            if (sysInfo != null && !sysInfo.NYUUKIN_HANDAN_END.IsNull)
                            {
                                if (chouseiKingaku > decimal.Parse(sysInfo.NYUUKIN_HANDAN_END.Value.ToString()))
                                {
                                    chouseiKingaku = 0;
                                    keshikomiFlg = false;
                                }
                            }
                        }
                        else
                        {
                            keshikomiFlg = true;
                        }
                    }
                    //-----------------------------------------------------------------------------------------------------

                    //20220420 Thanh 162575 s
                    var item = DuplicateNyuukinList.Where(t => t.DENPYOU_DATE.Value == yonyuuDate
                                                            && t.TORIHIKISAKI_CD == torihikisakiCd
                                                             && t.KINGAKU.Value == kingaku).FirstOrDefault();
                    if (item == null)
                    {
                        DuplicateNyuukinDTOClass item1 = new DuplicateNyuukinDTOClass();
                        item1.DENPYOU_DATE = yonyuuDate;
                        item1.TORIHIKISAKI_CD = torihikisakiCd;
                        item1.KINGAKU = kingaku;
                        DuplicateNyuukinList.Add(item1);
                    }
                    else
                    {
                        chouseiKingaku = 0;
                        keshikomiFlg = false;
                    }
                    //20220420 Thanh 162575 e

                    // 入金一括入力
                    var nyuukinSumEntry = this.CreateNyuukinSumEntry(KYOTEN_CD, BUMON_CD, yonyuuDate, nyuukinsakiCd, bankCd, bankShitenCd, kouzaShurui, kouzaNo, kouzaName, kingaku, chouseiKingaku, keshikomiFlg);

                    // 入金一括明細
                    var nyuukinSumDetailList = this.CreateNyuukinSumDetail(nyuukinSumEntry, kingaku, chouseiKingaku, tekiyouNaiyou);

                    // 入金入力
                    var nyuukinEntry = this.CreateNyuukinEntry(nyuukinSumEntry, torihikisakiCd);

                    // 入金明細
                    var nyukinDetailList = this.CreateNyuukinDetail(nyuukinEntry, nyuukinSumDetailList);

                    //// 仮受金調整
                    //var kariukeChousei = this.CreateKariukeChousei(nyuukinSumEntry);

                    //// 仮受金管理
                    //var kariukeControl = this.CreateKariukeControl(nyuukinSumEntry);

                    // 登録リストに追加
                    createNyuukinSumEntryEntityList.Add(nyuukinSumEntry);
                    createNyuukinSumDetailEntityList.AddRange(nyuukinSumDetailList);
                    createNyuukinEntryEntityList.Add(nyuukinEntry);
                    createNyuukinDetailEntityList.AddRange(nyukinDetailList);
                    //createKariukeChouseiEntityList.Add(kariukeChousei);
                    //createKariukeControlEntityList.Add(kariukeControl);
                    if (keshikomiFlg)
                    {
                        var nyuukinKeshikomiList = this.CreateNyuukinKeshikomi(nyuukinEntry, keshikomiData);
                        createNyuukinKeshikomiEntityList.AddRange(nyuukinKeshikomiList);
                    }



                }
                else if (this.form.customDataGridView1[ConstClass.COL_SAKUJO, i].Value != null && (bool)this.form.customDataGridView1[ConstClass.COL_SAKUJO, i].Value)
                {
                    T_NYUUKIN_DATA_TORIKOMI torikomi = new T_NYUUKIN_DATA_TORIKOMI();
                    torikomi.TORIKOMI_NUMBER = Int64.Parse(this.form.customDataGridView1[ConstClass.COL_TORIKOMI_NUMBER, i].Value.ToString());
                    torikomi.ROW_NUMBER = Int32.Parse(this.form.customDataGridView1[ConstClass.COL_ROW_NUMBER, i].Value.ToString());
                    torikomi.TIME_STAMP = ConvertStrByte.StringToByte(this.form.customDataGridView1[ConstClass.COL_TIME_STAMP, i].Value.ToString());
                    torikomiDeleteList.Add(torikomi);
                }
            }
            if (torikomiDeleteList.Count == 0)
            {
                msgLogic.MessageBoxShow("E269");
                LogUtility.DebugMethodEnd();
                return;
            }
            using (Transaction tran = new Transaction())
            {
                // 入金一括入力
                foreach (T_NYUUKIN_SUM_ENTRY entity in createNyuukinSumEntryEntityList)
                {
                    // 自動設定
                    var dataBinderContenaResult = new DataBinderLogic<T_NYUUKIN_SUM_ENTRY>(entity);
                    dataBinderContenaResult.SetSystemProperty(entity, false);

                    this.NyuukinSumEntryDao.Insert(entity);
                }

                // 入金一括入力明細
                foreach (T_NYUUKIN_SUM_DETAIL entity in createNyuukinSumDetailEntityList)
                {
                    // 自動設定
                    var dataBinderContenaResult = new DataBinderLogic<T_NYUUKIN_SUM_DETAIL>(entity);
                    dataBinderContenaResult.SetSystemProperty(entity, false);

                    this.NyuukinSumDetailDao.Insert(entity);
                }

                // 入金入力
                foreach (T_NYUUKIN_ENTRY entity in createNyuukinEntryEntityList)
                {
                    // 自動設定
                    var dataBinderContenaResult = new DataBinderLogic<T_NYUUKIN_ENTRY>(entity);
                    dataBinderContenaResult.SetSystemProperty(entity, false);

                    this.NyuukinEntryDao.Insert(entity);
                }

                // 入金入力明細
                foreach (T_NYUUKIN_DETAIL entity in createNyuukinDetailEntityList)
                {
                    // 自動設定
                    var dataBinderContenaResult = new DataBinderLogic<T_NYUUKIN_DETAIL>(entity);
                    dataBinderContenaResult.SetSystemProperty(entity, false);

                    this.NyuukinDetailDao.Insert(entity);
                }

                //// 仮受金調整
                //foreach (T_KARIUKE_CHOUSEI entity in createKariukeChouseiEntityList)
                //{
                //    // 自動設定
                //    var dataBinderContenaResult = new DataBinderLogic<T_KARIUKE_CHOUSEI>(entity);
                //    dataBinderContenaResult.SetSystemProperty(entity, false);

                //    this.KariukeChouseiDao.Insert(entity);
                //}

                //// 仮受金管理
                //foreach (T_KARIUKE_CONTROL entity in createKariukeControlEntityList)
                //{
                //    // 自動設定
                //    var dataBinderContenaResult = new DataBinderLogic<T_KARIUKE_CONTROL>(entity);
                //    dataBinderContenaResult.SetSystemProperty(entity, false);

                //    T_KARIUKE_CONTROL returnEntity = this.KariukeContorlDao.GetKariukekinByNyukinSakiCd(entity.NYUUKINSAKI_CD);
                //    if (returnEntity == null)
                //    {
                //        this.KariukeContorlDao.Insert(entity);
                //    }
                //    else
                //    {
                //        entity.TIME_STAMP = returnEntity.TIME_STAMP;
                //        entity.KARIUKE_TOTAL_KINGAKU = (returnEntity.KARIUKE_TOTAL_KINGAKU.IsNull ? 0 : returnEntity.KARIUKE_TOTAL_KINGAKU.Value) + entity.KARIUKE_TOTAL_KINGAKU.Value;
                //        this.KariukeContorlDao.Update(entity);
                //    }
                //}

                // 入金消込
                foreach (T_NYUUKIN_KESHIKOMI entity in createNyuukinKeshikomiEntityList)
                {
                    // 自動設定
                    var dataBinderContenaResult = new DataBinderLogic<T_NYUUKIN_KESHIKOMI>(entity);
                    dataBinderContenaResult.SetSystemProperty(entity, false);

                    this.NyuukinKeshikomiDao.Insert(entity);
                }

                foreach (T_NYUUKIN_DATA_TORIKOMI entity in torikomiDeleteList)
                {
                    nyuukinDataTorikomiDao.Delete(entity);
                }
                tran.Commit();
            }
            msgLogic.MessageBoxShow("I001", "登録");
            bt_func8_Click(null, null);
            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 入金一括入力のEntityを作成します
        /// </summary>
        /// <param name="kyotenCd"></param>
        /// <param name="bumonCd"></param>
        /// <param name="yonyuuDate"></param>
        /// <param name="nyuukinsakiCd"></param>
        /// <param name="bankCd"></param>
        /// <param name="bankShitenCd"></param>
        /// <param name="kouzaShurui"></param>
        /// <param name="kouzaNo"></param>
        /// <param name="kouzaName"></param>
        /// <param name="kingaku"></param>
        /// <param name="chouseiKingaku"></param>
        /// <param name="keshikomiFlg"></param>
        /// <returns></returns>
        private T_NYUUKIN_SUM_ENTRY CreateNyuukinSumEntry(SqlInt16 kyotenCd, string bumonCd, SqlDateTime yonyuuDate, string nyuukinsakiCd, string bankCd, string bankShitenCd, string kouzaShurui, string kouzaNo, string kouzaName, decimal kingaku, decimal chouseiKingaku, bool keshikomiFlg)
        {
            LogUtility.DebugMethodStart(kyotenCd, bumonCd, yonyuuDate, nyuukinsakiCd, bankCd, bankShitenCd, kouzaShurui, kouzaNo, kouzaName, kingaku, chouseiKingaku, keshikomiFlg);


            T_NYUUKIN_SUM_ENTRY nyuukinSumEntry = new T_NYUUKIN_SUM_ENTRY();

            nyuukinSumEntry.SYSTEM_ID = this.CreateSystemIdForNyuukin();
            nyuukinSumEntry.SEQ = 1;
            nyuukinSumEntry.KYOTEN_CD = kyotenCd;
            //nyuukinSumEntry.BUMON_CD = bumonCd;
            nyuukinSumEntry.NYUUKIN_NUMBER = this.GetDenshuNumberForNyuukin();
            nyuukinSumEntry.DENPYOU_DATE = yonyuuDate;
            nyuukinSumEntry.NYUUKINSAKI_CD = nyuukinsakiCd;
            nyuukinSumEntry.BANK_CD = bankCd;
            nyuukinSumEntry.BANK_SHITEN_CD = bankShitenCd;
            nyuukinSumEntry.KOUZA_SHURUI = kouzaShurui;
            nyuukinSumEntry.KOUZA_NO = kouzaNo;
            nyuukinSumEntry.KOUZA_NAME = kouzaName;
            nyuukinSumEntry.EIGYOU_TANTOUSHA_CD = null;
            nyuukinSumEntry.DENPYOU_BIKOU = null;
            nyuukinSumEntry.NYUUKIN_AMOUNT_TOTAL = kingaku;
            nyuukinSumEntry.CHOUSEI_AMOUNT_TOTAL = chouseiKingaku;
            nyuukinSumEntry.KARIUKEKIN_WARIATE_TOTAL = SqlDecimal.Null;
            nyuukinSumEntry.SEISAN_SOUSAI_CREATE_KBN = false;
            nyuukinSumEntry.DELETE_FLG = false;
            nyuukinSumEntry.TORI_KOMI_KBN = true;
            nyuukinSumEntry.JIDO_KESHIKOMI_KBN = keshikomiFlg;
            LogUtility.DebugMethodEnd(nyuukinSumEntry);
            return nyuukinSumEntry;
        }

        /// 入金一括入力明細のEntityを作成します
        /// </summary>
        /// <param name="nyuukinSumEntry"></param>
        /// <param name="kingaku"></param>
        /// <param name="chouseiKingaku"></param>
        /// <param name="tekiyouNaiyou"></param>
        /// <returns></returns>
        private List<T_NYUUKIN_SUM_DETAIL> CreateNyuukinSumDetail(T_NYUUKIN_SUM_ENTRY nyuukinSumEntry, decimal kingaku, decimal chouseiKingaku, string tekiyouNaiyou)
        {
            LogUtility.DebugMethodStart(nyuukinSumEntry, kingaku, chouseiKingaku, tekiyouNaiyou);
            List<T_NYUUKIN_SUM_DETAIL> nyuukinSumDetailList = new List<T_NYUUKIN_SUM_DETAIL>();
            // 入金額明細
            T_NYUUKIN_SUM_DETAIL nyuukinSumDetail = new T_NYUUKIN_SUM_DETAIL();

            nyuukinSumDetail.SYSTEM_ID = nyuukinSumEntry.SYSTEM_ID;
            nyuukinSumDetail.SEQ = nyuukinSumEntry.SEQ;
            nyuukinSumDetail.DETAIL_SYSTEM_ID = this.CreateSystemIdForNyuukin();
            nyuukinSumDetail.ROW_NUMBER = 1;

            nyuukinSumDetail.NYUUSHUKKIN_KBN_CD = 2;

            nyuukinSumDetail.KINGAKU = kingaku;

            nyuukinSumDetail.MEISAI_BIKOU = tekiyouNaiyou;

            nyuukinSumDetailList.Add(nyuukinSumDetail);

            if (chouseiKingaku > 0)
            {
                T_NYUUKIN_SUM_DETAIL nyuukinSumDetailChousei = new T_NYUUKIN_SUM_DETAIL();

                nyuukinSumDetailChousei.SYSTEM_ID = nyuukinSumEntry.SYSTEM_ID;
                nyuukinSumDetailChousei.SEQ = nyuukinSumEntry.SEQ;
                nyuukinSumDetailChousei.DETAIL_SYSTEM_ID = this.CreateSystemIdForNyuukin();
                nyuukinSumDetailChousei.ROW_NUMBER = 2;

                nyuukinSumDetailChousei.NYUUSHUKKIN_KBN_CD = 21;

                nyuukinSumDetailChousei.KINGAKU = chouseiKingaku;

                nyuukinSumDetailChousei.MEISAI_BIKOU = tekiyouNaiyou;

                nyuukinSumDetailList.Add(nyuukinSumDetailChousei);
            }

            LogUtility.DebugMethodEnd(nyuukinSumDetailList);
            return nyuukinSumDetailList;
        }

        /// <summary>
        /// 入金入力のEntityを作成します
        /// </summary>
        /// <param name="nyuukinSumEntry"></param>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        private T_NYUUKIN_ENTRY CreateNyuukinEntry(T_NYUUKIN_SUM_ENTRY nyuukinSumEntry, string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(nyuukinSumEntry, torihikisakiCd);


            T_NYUUKIN_ENTRY nyuukinEntry = new T_NYUUKIN_ENTRY();

            nyuukinEntry.SYSTEM_ID = this.CreateSystemIdForNyuukin();
            nyuukinEntry.SEQ = 1;
            nyuukinEntry.KYOTEN_CD = nyuukinSumEntry.KYOTEN_CD;
            //nyuukinEntry.BUMON_CD = nyuukinSumEntry.BUMON_CD;
            nyuukinEntry.NYUUKIN_SUM_SYSTEM_ID = nyuukinSumEntry.SYSTEM_ID;
            nyuukinEntry.NYUUKIN_NUMBER = nyuukinSumEntry.NYUUKIN_NUMBER;
            nyuukinEntry.DENPYOU_DATE = nyuukinSumEntry.DENPYOU_DATE;
            nyuukinEntry.NYUUKINSAKI_CD = nyuukinSumEntry.NYUUKINSAKI_CD;
            nyuukinEntry.TORIHIKISAKI_CD = torihikisakiCd;
            nyuukinEntry.BANK_CD = nyuukinSumEntry.BANK_CD;
            nyuukinEntry.BANK_SHITEN_CD = nyuukinSumEntry.BANK_SHITEN_CD;
            nyuukinEntry.KOUZA_SHURUI = nyuukinSumEntry.KOUZA_SHURUI;
            nyuukinEntry.KOUZA_NO = nyuukinSumEntry.KOUZA_NO;

            nyuukinEntry.EIGYOU_TANTOUSHA_CD = null;

            nyuukinEntry.KARIUKEKIN = SqlDecimal.Null;
            nyuukinEntry.NYUUKIN_AMOUNT_TOTAL = nyuukinSumEntry.NYUUKIN_AMOUNT_TOTAL;
            nyuukinEntry.CHOUSEI_AMOUNT_TOTAL = nyuukinSumEntry.CHOUSEI_AMOUNT_TOTAL;
            nyuukinEntry.KARIUKEKIN_WARIATE_TOTAL = SqlDecimal.Null;
            nyuukinEntry.CHOUSEI_DENPYOU_KBN = false;
            nyuukinEntry.TOK_INPUT_KBN = false;
            nyuukinEntry.DELETE_FLG = false;

            LogUtility.DebugMethodEnd(nyuukinEntry);
            return nyuukinEntry;
        }

        /// <summary>
        /// 入金入力明細のEntityを作成します
        /// </summary>
        /// <param name="nyuukinEntry"></param>
        /// <param name="listNyuukinSumDetail"></param>
        /// <returns></returns>
        private List<T_NYUUKIN_DETAIL> CreateNyuukinDetail(T_NYUUKIN_ENTRY nyuukinEntry, List<T_NYUUKIN_SUM_DETAIL> listNyuukinSumDetail)
        {
            LogUtility.DebugMethodStart(nyuukinEntry, listNyuukinSumDetail);
            List<T_NYUUKIN_DETAIL> listNyuukinDetail = new List<T_NYUUKIN_DETAIL>();

            foreach (var nyuukinSumDetail in listNyuukinSumDetail)
            {
                // 入金額明細
                T_NYUUKIN_DETAIL nyuukinDetail = new T_NYUUKIN_DETAIL();

                nyuukinDetail.SYSTEM_ID = nyuukinEntry.SYSTEM_ID;
                nyuukinDetail.SEQ = nyuukinEntry.SEQ;
                nyuukinDetail.DETAIL_SYSTEM_ID = this.CreateSystemIdForNyuukin();

                nyuukinDetail.ROW_NUMBER = nyuukinSumDetail.ROW_NUMBER;
                nyuukinDetail.NYUUSHUKKIN_KBN_CD = nyuukinSumDetail.NYUUSHUKKIN_KBN_CD;

                nyuukinDetail.KINGAKU = nyuukinSumDetail.KINGAKU;

                nyuukinDetail.MEISAI_BIKOU = nyuukinSumDetail.MEISAI_BIKOU;

                listNyuukinDetail.Add(nyuukinDetail);

            }
            LogUtility.DebugMethodEnd(listNyuukinDetail);
            return listNyuukinDetail;
        }


       ///// <summary>
       // /// 仮受金調整のEntityを作成します
       ///// </summary>
       ///// <param name="nyuukinSumEntry"></param>
       ///// <returns></returns>
       // private T_KARIUKE_CHOUSEI CreateKariukeChousei(T_NYUUKIN_SUM_ENTRY nyuukinSumEntry)
            //{

       //     T_KARIUKE_CHOUSEI nyuukinChousei = new T_KARIUKE_CHOUSEI();

       //     nyuukinChousei.SYSTEM_ID = nyuukinSumEntry.SYSTEM_ID;
       //     nyuukinChousei.SEQ = nyuukinSumEntry.SEQ;
       //     nyuukinChousei.NYUUKIN_NUMBER = nyuukinSumEntry.NYUUKIN_NUMBER;
       //     nyuukinChousei.NYUUKINSAKI_CD = nyuukinSumEntry.NYUUKINSAKI_CD;
       //     nyuukinChousei.KINGAKU = 0;

       //     return nyuukinChousei;
            //}

       // /// <summary>
       // /// 仮受金管理のEntityを作成します
       // /// </summary>
       // /// <param name="nyuukinSumEntry"></param>
       // /// <returns></returns>
       // private T_KARIUKE_CONTROL CreateKariukeControl(T_NYUUKIN_SUM_ENTRY nyuukinSumEntry)
       // {
       //     T_KARIUKE_CONTROL kariukeControl = new T_KARIUKE_CONTROL();

       //     kariukeControl.NYUUKINSAKI_CD = nyuukinSumEntry.NYUUKINSAKI_CD;

       //     //decimal kariukeKingaku = 0;
       //     //T_KARIUKE_CONTROL returnEntity = this.KariukeContorlDao.GetKariukekinByNyukinSakiCd(nyuukinSumEntry.NYUUKINSAKI_CD);
       //     //if (returnEntity != null)
       //     //{
       //     //    kariukeKingaku = (decimal)returnEntity.KARIUKE_TOTAL_KINGAKU;
       //     //}
       //     kariukeControl.KARIUKE_TOTAL_KINGAKU = nyuukinSumEntry.NYUUKIN_AMOUNT_TOTAL + nyuukinSumEntry.CHOUSEI_AMOUNT_TOTAL;

       //     return kariukeControl;
       // }

        /// <summary>
        /// Keshikomi
        /// </summary>
        /// <param name="nyuukinEntryList"></param>
        /// <returns></returns>
        internal List<T_NYUUKIN_KESHIKOMI> CreateNyuukinKeshikomi(T_NYUUKIN_ENTRY nyuukinEntry, DataTable keshikomiData)
        {
            LogUtility.DebugMethodStart(nyuukinEntry, keshikomiData);


            List<T_NYUUKIN_KESHIKOMI> nyuukinKeshikomiList = new List<T_NYUUKIN_KESHIKOMI>();

            decimal keshikomiKingakuTotal = nyuukinEntry.NYUUKIN_AMOUNT_TOTAL.Value + nyuukinEntry.CHOUSEI_AMOUNT_TOTAL.Value;

            for (int idxKeshikomi = 0; idxKeshikomi < keshikomiData.Rows.Count; idxKeshikomi++)
            {
                if (keshikomiKingakuTotal > 0)
                {
                    DataRow row = keshikomiData.Rows[idxKeshikomi];
                    var keshikomi = new T_NYUUKIN_KESHIKOMI();
                    keshikomi.SYSTEM_ID = nyuukinEntry.SYSTEM_ID;
                    keshikomi.SEIKYUU_NUMBER = Int64.Parse(row["SEIKYUU_NUMBER"].ToString());
                    keshikomi.KAGAMI_NUMBER = Int32.Parse(row["KAGAMI_NUMBER"].ToString());

                    keshikomi.KESHIKOMI_SEQ = 1;

                    keshikomi.NYUUKIN_NUMBER = nyuukinEntry.NYUUKIN_NUMBER;
                    keshikomi.TORIHIKISAKI_CD = nyuukinEntry.TORIHIKISAKI_CD;
                    keshikomi.KESHIKOMI_BIKOU = string.Empty;
                    keshikomi.NYUUKIN_SEQ = 0;
                    keshikomi.DELETE_FLG = false;
                    if (!String.IsNullOrEmpty(row["MIKESHIKOMI_KINGAKU"].ToString()) && decimal.Parse(row["MIKESHIKOMI_KINGAKU"].ToString()) != 0)
                    {
                        decimal miKeshikomiKingaku = decimal.Parse(row["MIKESHIKOMI_KINGAKU"].ToString());
                        if (keshikomiKingakuTotal > miKeshikomiKingaku)
                        {
                            keshikomi.KESHIKOMI_GAKU = miKeshikomiKingaku;
                            keshikomiKingakuTotal = keshikomiKingakuTotal - miKeshikomiKingaku;
                        }
                        else
                        {
                            keshikomi.KESHIKOMI_GAKU = keshikomiKingakuTotal;
                            keshikomiKingakuTotal = 0;
                        }
                    }
                    if (!keshikomi.KESHIKOMI_GAKU.IsNull&&keshikomi.KESHIKOMI_GAKU.Value>0)
                    {
                        nyuukinKeshikomiList.Add(keshikomi);
                    }

                }
                else
                {
                    break;
                }

            }
            LogUtility.DebugMethodEnd(nyuukinKeshikomiList);
            return nyuukinKeshikomiList;
        }

        #endregion

        #region 月次処理チェック

        /// <summary>
        /// 月次処理中かのチェックを行います
        /// </summary>
        /// <returns>True：月次処理中</returns>
        internal bool CheckGetsujiShoriChu()
        {
            bool val = false;

            // 最新月次処理中年月取得
            GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
            string strDate = getsujiShoriCheckLogic.GetLatestGetsjiShoriChuDateTime();

            if (string.IsNullOrEmpty(strDate))
            {
                // 月次処理は実行されていない
                return val;
            }

            DateTime getsujiShoriChuDate = DateTime.Parse(strDate);

            for (int i = 0; i < this.form.customDataGridView1.RowCount; i++)
            {
                if (this.form.customDataGridView1[ConstClass.COL_SAKUSEI, i].Value != null && (bool)this.form.customDataGridView1[ConstClass.COL_SAKUSEI, i].Value)
                {
                    DateTime yonyuuDate = DateTime.Parse(this.form.customDataGridView1[ConstClass.COL_YONYUU_DATE, i].Value.ToString());

                    if (yonyuuDate.CompareTo(getsujiShoriChuDate) <= 0)
                    {
                        // 登録する伝票日付が月次処理中の日付より前の場合は伝票登録不可
                        val = true;
                        break;
                    }
                }
            }

            return val;
        }

        /// <summary>
        /// 月次処理によってロックされているかのチェックを行います
        /// </summary>
        /// <returns>True：ロックされている　False：ロックされていない</returns>
        internal bool CheckGetsujiShoriLock()
        {
            bool val = false;

            // 最新月次年月取得
            int year = 0;
            int month = 0;
            GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
            getsujiShoriCheckLogic.GetLatestGetsujiDate(ref year, ref month);

            if (year == 0 || month == 0)
            {
                // 月次処理データ無し
                return val;
            }

            // 月次年月日を最新月次年月末日にする
            DateTime getsujiShoriDate = new DateTime(year, month, 1);
            getsujiShoriDate = getsujiShoriDate.AddMonths(1).AddDays(-1);

            for (int i = 0; i < this.form.customDataGridView1.RowCount; i++)
            {
                if (this.form.customDataGridView1[ConstClass.COL_SAKUSEI, i].Value != null && (bool)this.form.customDataGridView1[ConstClass.COL_SAKUSEI, i].Value)
                {

                    DateTime yonyuuDate = DateTime.Parse(this.form.customDataGridView1[ConstClass.COL_YONYUU_DATE, i].Value.ToString());

                    if (yonyuuDate.CompareTo(getsujiShoriDate) <= 0)
                    {
                        // 登録する伝票日付が最新月次年月の日付より前の場合は伝票登録不可
                        val = true;
                        break;
                    }
                }
            }
            return val;
        }

        #endregion

        #region 締済期間チェック
        /// <summary>
        /// 締済期間チェック
        /// </summary>
        /// <returns></returns>
        internal bool ShimeiDateCheck(string kyotenCd)
        {
            if (string.IsNullOrEmpty(kyotenCd))
            {
                return true;
            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            ShimeCheckLogic CheckShimeDate = new ShimeCheckLogic();
            List<ReturnDate> returnDate = new List<ReturnDate>();
            List<CheckDate> checkDateList = new List<CheckDate>();

            CheckDate checkDate = new CheckDate();
            // 更新ループ
            for (int i = 0; i < this.form.customDataGridView1.RowCount; i++)
            {
                if (this.form.customDataGridView1[ConstClass.COL_SAKUSEI, i].Value != null && (bool)this.form.customDataGridView1[ConstClass.COL_SAKUSEI, i].Value)
                {
                    DateTime yonyuuDate = DateTime.Parse(this.form.customDataGridView1[ConstClass.COL_YONYUU_DATE, i].Value.ToString());
                    string torihikisakiCd = this.form.customDataGridView1[ConstClass.COL_TORIHIKISAKI_CD, i].Value.ToString();


                    checkDate = new CheckDate();
                    // 取引先CD
                    checkDate.TORIHIKISAKI_CD = torihikisakiCd;
                    // 拠点CD
                    checkDate.KYOTEN_CD = kyotenCd;
                    // 日付
                    checkDate.CHECK_DATE = yonyuuDate;

                    checkDateList.Add(checkDate);

                }
            }

            // 売上チェック
            returnDate = CheckShimeDate.GetNearShimeDate(checkDateList, 1);
            // 支払チェック

            // 売上
            if (returnDate.Count != 0)
            {
                //例外日付が含まれる
                foreach (ReturnDate rdDate in returnDate)
                {
                    if (rdDate.dtDATE == SqlDateTime.MinValue.Value)
                    {
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                }
                if (msgLogic.MessageBoxShow("C085", "") == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
