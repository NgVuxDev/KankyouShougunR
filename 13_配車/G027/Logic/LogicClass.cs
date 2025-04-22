using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using GrapeCity.Win.MultiRow;
using System.Data.SqlTypes;
using Shougun.Core.Common.BusinessCommon;
using CommonChouhyouPopup.App;
using r_framework.Dto;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Shougun.Core.Allocation.SagyoubiHenkou
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        #region 定数

        /// <summary>
        /// 運転者CD
        /// </summary>
        internal const string CELL_NAME_UNTENSHA_CD = "cellUntenshaCd";

        /// <summary>
        /// 運転者名
        /// </summary>
        internal const string CELL_NAME_UNTENSHA_NAME = "cellUntenshaName";

        /// <summary>
        /// 車輌CD
        /// </summary>
        internal const string CELL_NAME_SHARYOU_CD = "cellSharyouCd";

        /// <summary>
        /// 車輌名
        /// </summary>
        internal const string CELL_NAME_SHARYOU_NAME = "cellSharyouName";

        /// <summary>
        /// 車種CD
        /// </summary>
        internal const string CELL_NAME_SHASHU_CD = "cellShashuCd";

        /// <summary>
        /// 車種名
        /// </summary>
        internal const string CELL_NAME_SHASHU_NAME = "cellShashuName";

        /// <summary>
        /// 運搬業者CD
        /// </summary>
        internal const string CELL_NAME_UNPAN_GYOUSHA_CD = "cellUnpanGyoushaCd";

        /// <summary>
        /// 運搬業者名
        /// </summary>
        internal const string CELL_NAME_UNPAN_GYOUSHA_NAME = "cellUnpanGyoushaName";

        #endregion

        /// <summary>
        /// 配車割当（一日）DTO
        /// </summary>
        private DTO_Haisha dtoHaisha = new DTO_Haisha();
        /// <summary>
        /// システムID枝番DTO
        /// </summary>
        private DTO_IdSeq dtoIdSeq = new DTO_IdSeq();
        /// <summary>
        /// 配車割当（一日）DAO
        /// </summary>
        private DAO_T_HAISHA_WARIATE_DAY dao_HAISHA = DaoInitUtility.GetComponent<DAO_T_HAISHA_WARIATE_DAY>();
        /// <summary>
        /// 配車メモDAO
        /// </summary>
        private DAO_T_HAISHA_MEMO daoT_HAISHA_MEMO = DaoInitUtility.GetComponent<DAO_T_HAISHA_MEMO>();
        /// <summary>
        /// 受付（収集）入力DAO
        /// </summary>
        private DAO_T_UKETSUKE_SS_ENTRY daoT_UKETSUKE_SS_ENTRY = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SS_ENTRY>();
        /// <summary>
        /// 受付（収集）明細DAO
        /// </summary>
        private DAO_T_UKETSUKE_SS_DETAIL daoT_UKETSUKE_SS_DETAIL = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SS_DETAIL>();
        /// <summary>
        /// コンテナ稼動予定DAO
        /// </summary>
        private DAO_T_CONTENA_RESERVE daoT_CONTENA_RESERVE = DaoInitUtility.GetComponent<DAO_T_CONTENA_RESERVE>();
        /// <summary>
        /// 受付（出荷）入力DAO
        /// </summary>
        private DAO_T_UKETSUKE_SK_ENTRY daoT_UKETSUKE_SK_ENTRY = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SK_ENTRY>();
        /// <summary>
        /// 受付（出荷）明細DAO
        /// </summary>
        private DAO_T_UKETSUKE_SK_DETAIL daoT_UKETSUKE_SK_DETAIL = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SK_DETAIL>();

        //ThangNguyen [Add] 20150721 Start
        /// <summary>
        /// 定期配車入力DAO
        /// </summary>
        private DAO_T_TEIKI_HAISHA_ENTRY daoT_TEIKI_HAISHA_ENTRY = DaoInitUtility.GetComponent<DAO_T_TEIKI_HAISHA_ENTRY>();
        /// <summary>
        /// 定期配車明細DAO
        /// </summary>
        private DAO_T_TEIKI_HAISHA_DETAIL daoT_TEIKI_HAISHA_DETAIL = DaoInitUtility.GetComponent<DAO_T_TEIKI_HAISHA_DETAIL>();
        /// <summary>
        /// 定期配車荷降DAO
        /// </summary>
        private DAO_T_TEIKI_HAISHA_NIOROSHI daoT_TEIKI_HAISHA_NIOROSHI = DaoInitUtility.GetComponent<DAO_T_TEIKI_HAISHA_NIOROSHI>();
        /// <summary>
        /// 定期配車詳細DAO
        /// </summary>
        private DAO_T_TEIKI_HAISHA_SHOUSAI daoT_TEIKI_HAISHA_SHOUSAI = DaoInitUtility.GetComponent<DAO_T_TEIKI_HAISHA_SHOUSAI>();

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        public M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// 配車済件数
        /// </summary>
        private int _haishaCount;
        public int HaishaCount
        {
            get { return _haishaCount; }
            set
            {
                _haishaCount = value;
            }
        }


        public Cell selectedCell = null;

        /// <summary>
        /// 前に選択した行、セルの位置
        /// </summary>
        internal bool changeHaishaFlg = false;



        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 親フォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// 検索結果DataTable
        /// </summary>
        private DataTable resultHaisha = new DataTable();

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>
        /// MultiRow
        /// </summary>
        private GcCustomMultiRow mrHaisha;

        /// <summary>
        /// 
        /// </summary>
        private int sharyouCount;

        /// <summary>
        /// 
        /// </summary>
        //private DataRow drDest;

        /// <summary>
        /// 
        /// </summary>
        //private string colDest;

        /// <summary>
        /// M_SYS_INFO.CONTENA_KANRI_HOUHOU(true:数量管理)
        /// </summary>
        internal bool isSuuryouKanri;
        internal string date;

        //指定コントロールのペイントを禁止
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

        private const int WM_SETREDRAW = 0xB;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.parentForm = (BusinessBaseForm)this.form.Parent;
            this.mrHaisha = this.form.mrHaisha;

            var sysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData();
            int contenaKanriHouhou = 1;
            if (sysInfo.Length > 0)
            {
                contenaKanriHouhou = sysInfo[0].CONTENA_KANRI_HOUHOU.IsNull ? CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU : (int)sysInfo[0].CONTENA_KANRI_HOUHOU;
            }
            this.isSuuryouKanri = (contenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU);
            
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //　ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                this.parentForm.ProcessButtonPanel.Visible = false;
                //this.parentForm.bt_func9.DialogResult = DialogResult.OK;
                this.parentForm.bt_func12.DialogResult = DialogResult.Cancel;

                if (IsShasyuSyaryou())
                {
                    // 運転者は非表示
                    this.form.lblUntensha.Visible = false;
                    this.form.UNTENSHA_CD.Visible = false;
                    this.form.UNTENSHA_NAME.Visible = false;
                }
                else
                {
                    // 車種は非表示
                    this.form.lblShashu.Visible = false;
                    this.form.SHASHU_CD.Visible = false;
                    this.form.SHASHU_NAME.Visible = false;
                }

                // 配車割当明細の項目位置設定
                this.ExecuteAlignmentForHaisha();

                this.SetColumnHeader(); //ThangNguyen [Add] 20150721

                this.SearchAndDisplay();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        //ThangNguyen [Add] 20150721 Start
        /// <summary>
        /// ScrollHaishaDataGrid
        /// </summary>
        public void ScrollHaishaDataGrid(int kijunResu)
        {
            try
            {
                kijunResu = kijunResu + 1;
                string suffix = GetSuffix(kijunResu);
                decimal x = this.mrHaisha.ColumnHeaders[0].Cells["cellHeader01"].Size.Width * (decimal)mrHaisha.ZoomFactor * (kijunResu - 1);
                int pointX = int.Parse(Math.Round(x, MidpointRounding.AwayFromZero).ToString());
                int pointY = this.mrHaisha.ColumnHeaders[0].Cells["cellHeader01"].Location.Y;
                this.form.mrHaisha.FirstDisplayedLocation = new Point(pointX, pointY);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }

        }

        /// <summary>
        /// セット列ヘッダ
        /// </summary>
        /// <returns></returns>
        private void SetColumnHeader()
        {
            int haishaWariateKaishi = 0;

            for (int i = 0; i < ConstClass.DENPYOU_COUNT; i++)
            {
                if (haishaWariateKaishi >= ConstClass.DENPYOU_COUNT)
                {
                    haishaWariateKaishi = 0;
                }
                string suffix = this.GetSuffix(i + 1);
                this.mrHaisha.ColumnHeaders[0]["cellHeader" + suffix].Value = haishaWariateKaishi;
                haishaWariateKaishi += 1;
            }
        }

        //ThangNguyen [Add] 20150721 End
        #endregion

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.BUTTON_SETTING_XML);
        }
        #endregion

        #region 配車割当明細の項目表示制御
        /// <summary>
        /// 
        /// </summary>
        internal void ExecuteAlignmentForHaisha()
        {
            LogUtility.DebugMethodStart();

            // 項目表示制御開始
            this.mrHaisha.SuspendLayout();
            var newTemplate = this.mrHaisha.Template;

            // 各項目の位置設定
            if (this.form.HaisyaKubun)
            {
                // 一列目（車輌）
                var ShashuHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellShashu"];
                ShashuHeader.Location = new Point(25, 0);
                var SharyouHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellSharyou"];
                SharyouHeader.Location = new Point(25, 21);
                var ShashuCdCell = newTemplate.Row.Cells["cellShashuCd"];
                ShashuCdCell.Location = new Point(25, 0);
                ShashuCdCell.TabIndex = 0;  //ThangNguyen [Add] 20150721
                var Unused1Cell = newTemplate.Row.Cells["cellUnused1"];
                Unused1Cell.Location = new Point(85, 0);
                var ShashuNameCell = newTemplate.Row.Cells["cellShashuName"];
                ShashuNameCell.Location = new Point(25, 20);
                ShashuNameCell.TabIndex = 1;    //ThangNguyen [Add] 20150721
                var SharyouCdCell = newTemplate.Row.Cells["cellSharyouCd"];
                SharyouCdCell.Location = new Point(25, 48);
                SharyouCdCell.TabIndex = 4; //ThangNguyen [Add] 20150721
                var Unused2Cell = newTemplate.Row.Cells["cellUnused2"];
                Unused2Cell.Location = new Point(85, 48);
                var SaidaiWakusuuCell = newTemplate.Row.Cells["cellSaidaiWakusuu"];
                SaidaiWakusuuCell.Location = new Point(25, 68);
                var WorkClosedSharyouCell = newTemplate.Row.Cells["cellWorkClosedSharyou"];
                WorkClosedSharyouCell.Location = new Point(54, 68);
                var SharyouNameCell = newTemplate.Row.Cells["cellSharyouName"];
                SharyouNameCell.Location = new Point(25, 68);
                //ThangNguyen [Add] 20150721 Start
                SharyouNameCell.TabIndex = 5;
                var cellGyoushaCd = newTemplate.Row.Cells["cellGyoushaCd"];
                cellGyoushaCd.Location = new Point(85, 68);
                var cellGyoushaName = newTemplate.Row.Cells["cellGyoushaName"];
                cellGyoushaName.Location = new Point(108, 68);
                // 二列目（運転者）
                var UntenshaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUntensha"];
                UntenshaHeader.Location = new Point(174, 0);
                var UntenshaCdCell = newTemplate.Row.Cells["cellUntenshaCd"];
                UntenshaCdCell.Location = new Point(174, 0);
                UntenshaCdCell.TabIndex = 2;
                var UntenshaNameCell = newTemplate.Row.Cells["cellUntenshaName"];
                UntenshaNameCell.Location = new Point(174, 20);
                UntenshaNameCell.TabIndex = 3;
                var WorkClosedUntenshaCell = newTemplate.Row.Cells["cellWorkClosedUntensha"];
                WorkClosedUntenshaCell.Location = new Point(174, 52);
                var BikouCell = newTemplate.Row.Cells["cellBikou"];
                BikouCell.Location = new Point(174, 48);
                var UnpanGyoushaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUnpanGyousha"];
                UnpanGyoushaHeader.Location = new Point(174, 21);
                var UnpanGyoushaCdCell = newTemplate.Row.Cells["cellUnpanGyoushaCd"];
                UnpanGyoushaCdCell.Location = new Point(174, 48);
                UnpanGyoushaCdCell.TabIndex = 6;
                var UnpanGyoushaNameCell = newTemplate.Row.Cells["cellUnpanGyoushaName"];
                UnpanGyoushaNameCell.Location = new Point(174, 68);
                UnpanGyoushaNameCell.TabIndex = 7;
                //ThangNguyen [Add] 20150721 End
                // 固定列は「運転者」
                this.mrHaisha.FreezeLeftCellName = "cellUntenshaName";
            }
            else
            {
                // 一列目（運転者）
                var UntenshaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUntensha"];
                UntenshaHeader.Location = new Point(25, 0);
                var UntenshaCdCell = newTemplate.Row.Cells["cellUntenshaCd"];
                UntenshaCdCell.Location = new Point(25, 0);
                UntenshaCdCell.TabIndex = 0;    //ThangNguyen [Add] 20150721
                var UntenshaNameCell = newTemplate.Row.Cells["cellUntenshaName"];
                UntenshaNameCell.Location = new Point(25, 20);
                UntenshaNameCell.TabIndex = 1;      //ThangNguyen [Add] 20150721
                var WorkClosedUntenshaCell = newTemplate.Row.Cells["cellWorkClosedUntensha"];
                WorkClosedUntenshaCell.Location = new Point(25, 52);
                var BikouCell = newTemplate.Row.Cells["cellBikou"];
                //ThangNguyen [Add] 20150721 Start
                //BikouCell.Location = new Point(25, 52);
                BikouCell.Location = new Point(25, 48);
                var UnpanGyoushaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUnpanGyousha"];
                UnpanGyoushaHeader.Location = new Point(25, 21);
                var UnpanGyoushaCdCell = newTemplate.Row.Cells["cellUnpanGyoushaCd"];
                UnpanGyoushaCdCell.Location = new Point(25, 48);
                UnpanGyoushaCdCell.TabIndex = 4;
                var UnpanGyoushaNameCell = newTemplate.Row.Cells["cellUnpanGyoushaName"];
                UnpanGyoushaNameCell.Location = new Point(25, 68);
                UnpanGyoushaNameCell.TabIndex = 5;
                //ThangNguyen [Add] 20150721 End
                // 二列目（車輌）
                var ShashuHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellShashu"];
                ShashuHeader.Location = new Point(95, 0);
                var SharyouHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellSharyou"];
                SharyouHeader.Location = new Point(95, 21);
                var ShashuCdCell = newTemplate.Row.Cells["cellShashuCd"];
                ShashuCdCell.Location = new Point(95, 0);
                ShashuCdCell.TabIndex = 2;  //ThangNguyen [Add] 20150721
                var Unused1Cell = newTemplate.Row.Cells["cellUnused1"];
                Unused1Cell.Location = new Point(155, 0);
                var ShashuNameCell = newTemplate.Row.Cells["cellShashuName"];
                ShashuNameCell.Location = new Point(95, 20);
                ShashuNameCell.TabIndex = 3;  //ThangNguyen [Add] 20150721
                var SharyouCdCell = newTemplate.Row.Cells["cellSharyouCd"];
                SharyouCdCell.Location = new Point(95, 48);
                SharyouCdCell.TabIndex = 6;  //ThangNguyen [Add] 20150721
                var Unused2Cell = newTemplate.Row.Cells["cellUnused2"];
                Unused2Cell.Location = new Point(155, 48);
                var SaidaiWakusuuCell = newTemplate.Row.Cells["cellSaidaiWakusuu"];
                SaidaiWakusuuCell.Location = new Point(95, 68);
                var WorkClosedSharyouCell = newTemplate.Row.Cells["cellWorkClosedSharyou"];
                WorkClosedSharyouCell.Location = new Point(124, 68);
                var SharyouNameCell = newTemplate.Row.Cells["cellSharyouName"];
                SharyouNameCell.Location = new Point(95, 68);
                //ThangNguyen [Add] 20150721 Start
                SharyouNameCell.TabIndex = 7;
                var cellGyoushaCd = newTemplate.Row.Cells["cellGyoushaCd"];
                cellGyoushaCd.Location = new Point(155, 68);
                var cellGyoushaName = newTemplate.Row.Cells["cellGyoushaName"];
                cellGyoushaName.Location = new Point(178, 68);
                //ThangNguyen [Add] 20150721 End
                // 固定列は「車輌」
                this.mrHaisha.FreezeLeftCellName = "cellShashuName";
            }

            // 配車割当明細に反映
            this.mrHaisha.Template = newTemplate;
            this.mrHaisha.ResumeLayout();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// ボタンのイベント初期化処理
        /// </summary>
        /// <returns></returns>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            this.form.C_Regist(this.parentForm.bt_func9);
            this.parentForm.Width = this.parentForm.Width + 150;

            this.parentForm.bt_func2.Click += this.bt_func2_Click;
            this.parentForm.bt_func3.Click += this.bt_func3_Click;
            this.parentForm.bt_func7.Click += this.bt_func7_Click;
            this.parentForm.bt_func9.Click += this.bt_func9_Click;
            this.parentForm.bt_func12.Click += this.bt_func12_Click;

            LogUtility.DebugMethodEnd();
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        public void SearchAndDisplay()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.Search();
                this.date = this.form.dtpSagyouDate.Text;
                this.mrHaisha.DataSource = this.resultHaisha;
                this.parentForm.bt_func9.Enabled = false;

                //画面のペイントを禁止(行ジャンプ時に、明細がぶれるため)
                SendMessage(this.form.Handle, WM_SETREDRAW, 0, IntPtr.Zero);

                //ThangNguyen [Add] 20150724 Start
                if (sysInfoEntity != null && !sysInfoEntity.HAISHA_WARIATE_KAISHI.IsNull)
                {
                    this.ScrollHaishaDataGrid(sysInfoEntity.HAISHA_WARIATE_KAISHI.Value);
                }
                //ThangNguyen [Add] 20150724 End

                //検索時も行ジャンプを有効にする。
                this.JumpGridRowByCode();
                //画面のペイントを出来るようにする
                SendMessage(this.form.Handle, WM_SETREDRAW, 1, IntPtr.Zero);

                this.form.Refresh();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchAndDisplay", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region Functionボタン 押下処理

        #region  F2 最適化
        /// <summary>
        /// F2 前日
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.form.dtpSagyouDate.Value = DateTime.Parse(this.dtoHaisha.SagyouDate) - new TimeSpan(1, 0, 0, 0);
                string timeTemp = ((DateTime)this.form.dtpSagyouDate.Value).ToShortDateString();
                if (timeTemp == this.form.StandandDateTime.ToShortDateString())
                {
                    this.form.dtpSagyouDate.Value = this.form.StandandDateTime - new TimeSpan(1, 0, 0, 0);
                }
                this.form.dtpSagyouDate.Refresh();
                this.selectedCell = null;
                this.SearchAndDisplay();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region  F3 翌日
        /// <summary>
        /// F3 翌日
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.form.dtpSagyouDate.Value = DateTime.Parse(this.dtoHaisha.SagyouDate) + new TimeSpan(1, 0, 0, 0);
                string timeTemp = ((DateTime)this.form.dtpSagyouDate.Value).ToShortDateString();
                if (timeTemp == this.form.StandandDateTime.ToShortDateString())
                {
                    this.form.dtpSagyouDate.Value = this.form.StandandDateTime + new TimeSpan(1, 0, 0, 0);
                }
                this.form.dtpSagyouDate.Refresh();
                this.selectedCell = null;
                this.SearchAndDisplay();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region  F7 検索
        /// <summary>
        /// F7 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.selectedCell = null;
                this.SearchAndDisplay();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region  F9 登録
        /// <summary>
        /// F9 登録
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.Regist(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region  F12 閉じる
        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // フォームを閉じる
                this.form.Close();
                this.parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        //-------------------------ThangNguyen [Add] 20150722 Start-------------------------
        /// <summary>
        /// 入力された車種または運転者コードの最初の表示行にジャンプする
        /// </summary>
        internal void JumpGridRowByCode()
        {
            if ((this.dtoHaisha.HaisyaKubun && !string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                || !this.dtoHaisha.HaisyaKubun && !string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
            {
                int rowCnt = 0;
                foreach (Row row in this.mrHaisha.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    if ((this.dtoHaisha.HaisyaKubun && this.form.SHASHU_CD.Text.Equals(row.Cells[CELL_NAME_SHASHU_CD].Value.ToString()))
                        || (!this.dtoHaisha.HaisyaKubun && this.form.UNTENSHA_CD.Text.Equals(row.Cells[CELL_NAME_UNTENSHA_CD].Value.ToString())))
                    {
                        // 車輌を軸とするときは入力された車輌コードと、運転者を軸とするときは入力された運転者コードと
                        // 合致する行が存在すれば、その行まで移動
                        this.mrHaisha.FirstDisplayedCellPosition = new GrapeCity.Win.MultiRow.CellPosition(rowCnt, 0);
                        row.Selected = true;
                        break;
                    }
                    rowCnt++;
                }
            }
            else
            {
                // 車種、運転者の入力がない場合は、先頭行を表示
                if (this.mrHaisha.Rows.Count > 0)
                {
                    this.mrHaisha.FirstDisplayedCellPosition = new GrapeCity.Win.MultiRow.CellPosition(0, 0);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool isRowDouble()
        {
            try
            {
                // 入力行の運転者、車輌、業者に重複がないかどうかチェック
                Dictionary<string, string> mrHaishaInputRow = new Dictionary<string, string>();
                foreach (var drHaisha in this.resultHaisha.AsEnumerable())
                {
                    string RowKey = drHaisha["SHAIN_CD"].ToString() + drHaisha["GYOUSHA_CD"].ToString() + drHaisha["SHARYOU_CD"].ToString();
                    string dummyData = string.Empty;
                    mrHaishaInputRow.Add(RowKey, dummyData);
                }

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is System.ArgumentException)
                {
                    msgLogic.MessageBoxShow("E031", "入力行の運転者と車輌");
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        /// <summary>
        /// 車輌または運転者CDの入力が無い場合のエラー制御
        /// </summary>
        /// <returns></returns>
        private bool SharyouUntenshaInputErrorCheck()
        {
            LogUtility.DebugMethodStart();

            bool retVal = false;
            bool[] errFlgSharyouCd = new bool[this.mrHaisha.Rows.Count];
            bool[] errFlgUntenshaCd = new bool[this.mrHaisha.Rows.Count];
            int rowIndex = -1;

            ClearInputError();
            this.SetAllUketsuke((dr, n, isSS, idx) =>
            {
                if (dr.Field<string>("HAISHA_SHURUI" + n) == ConstClass.HAISHA_SHURUI_KAKU)
                {
                    // 車輌CD又は運転者CDに入力が無い場合
                    if (dr.Field<string>("SHARYOU_CD") == null || dr.Field<string>("SHARYOU_CD").Equals(string.Empty))
                    {
                        // 車輌CD入力無し
                        errFlgSharyouCd[idx] = true;
                        if (rowIndex < 0) rowIndex = idx;
                    }
                    if (dr.Field<string>("SHAIN_CD") == null || dr.Field<string>("SHAIN_CD").Equals(string.Empty))
                    {
                        // 運転者CD入力無し
                        errFlgUntenshaCd[idx] = true;
                        if (rowIndex < 0) rowIndex = idx;
                    }
                }
            }
            );
            if (rowIndex >= 0)
            {
                msgLogic.MessageBoxShow("E197");
                SetNInputError(errFlgSharyouCd, errFlgUntenshaCd, rowIndex);
                retVal = true;
            }

            LogUtility.DebugMethodEnd();
            return retVal;
        }

        /// <summary>
        /// 入力エラークリア
        /// </summary>
        private void ClearInputError()
        {
            foreach (Row row in this.mrHaisha.Rows)
            {
                var cellSharyouCd = (GcCustomTextBoxCell)row[CELL_NAME_SHARYOU_CD];
                cellSharyouCd.IsInputErrorOccured = false;
                var cellUntenshaCd = (GcCustomTextBoxCell)row[CELL_NAME_UNTENSHA_CD];
                cellUntenshaCd.IsInputErrorOccured = false;
            }
        }

        /// <summary>
        /// 入力無しエラー制御
        /// </summary>
        /// <param name="errFlgSharyouCd"></param>
        /// <param name="errFlgUntenshaCd"></param>
        /// <param name="rowIndex"></param>
        private void SetNInputError(bool[] errFlgSharyouCd, bool[] errFlgUntenshaCd, int rowIndex)
        {
            for (int i = 0; i < this.mrHaisha.Rows.Count; i++)
            {
                if (errFlgSharyouCd[i])
                {
                    // 車輌CD入力エラー
                    var cellSharyouCd = (GcCustomTextBoxCell)this.mrHaisha[i, CELL_NAME_SHARYOU_CD];
                    cellSharyouCd.IsInputErrorOccured = true;
                }
                if (errFlgUntenshaCd[i])
                {
                    // 運転者CD入力エラー
                    var cellUntenshaCd = (GcCustomTextBoxCell)this.mrHaisha[i, CELL_NAME_UNTENSHA_CD];
                    cellUntenshaCd.IsInputErrorOccured = true;
                }
            }
            this.mrHaisha.FirstDisplayedCellPosition = new GrapeCity.Win.MultiRow.CellPosition(rowIndex, 0);
        }

        #region 入力軸に基づき必須入力項目を変更
        /// <summary>
        /// 入力軸に基づき必須入力項目を変更
        /// </summary>
        internal void SetRequiredSetting()
        {
            // 設定
            SelectCheckDto existCheck = new SelectCheckDto();
            existCheck.CheckMethodName = "必須チェック";
            Collection<SelectCheckDto> existChecks = new Collection<SelectCheckDto>();
            existChecks.Add(existCheck);

            // 配車明細の必須チェック項目変更
            this.mrHaisha.SuspendLayout();

            if (this.selectedCell != null && this.selectedCell.RowIndex >= 0)
            {
                // 配置した行を取得する。
                Row rowHaisha = this.mrHaisha.Rows[this.selectedCell.RowIndex];

                if (IsShasyuSyaryou())
                {
                    // 入力軸が車輌
                    var obj1 = rowHaisha.Cells[CELL_NAME_SHARYOU_CD];
                    var obj2 = rowHaisha.Cells[CELL_NAME_UNTENSHA_CD];
                    PropertyUtility.SetValue(obj1, "RegistCheckMethod", existChecks);
                    PropertyUtility.SetValue(obj2, "RegistCheckMethod", null);
                }
                else
                {
                    // 入力軸が運転者
                    var obj1 = rowHaisha.Cells[CELL_NAME_SHARYOU_CD];
                    var obj2 = rowHaisha.Cells[CELL_NAME_UNTENSHA_CD];
                    PropertyUtility.SetValue(obj1, "RegistCheckMethod", null);
                    PropertyUtility.SetValue(obj2, "RegistCheckMethod", existChecks);
                }
            }
            this.mrHaisha.ResumeLayout();
        }
        #endregion

        #region 一括解除
        /// <summary>
        /// 一括解除
        /// </summary>
        /// <param name="f"></param>
        private void SetAllUketsuke(Action<DataRow, string, bool> f)
        {
            //LogUtility.DebugMethodStart(f);

            foreach (var dr in this.resultHaisha.AsEnumerable())
            {
                for (int i = 0; i < ConstClass.DENPYOU_COUNT; ++i)
                {
                    var n = GetSuffix(i + 1);
                    var shubetsu = dr.Field<short>("SHUBETSU_KBN_" + n);
                    if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS ||
                        shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
                    {
                        f(dr, n, shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS);
                    }
                }
            }

            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 一括確定（上記一括解除メソッドをオーバーロード）
        /// </summary>
        /// <param name="f"></param>
        private void SetAllUketsuke(Action<DataRow, string, bool, int> f)
        {
            //LogUtility.DebugMethodStart(f);

            foreach (var dr in this.resultHaisha.AsEnumerable())
            {
                for (int i = 0; i < ConstClass.DENPYOU_COUNT; ++i)
                {
                    var n = GetSuffix(i + 1);
                    var shubetsu = dr.Field<short>("SHUBETSU_KBN_" + n);
                    if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS ||
                        shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
                    {
                        f(dr, n, shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS, this.resultHaisha.Rows.IndexOf(dr));
                    }
                }
            }

            //LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 休動チェック
        public bool ChkWorkClose()
        {
            string shashuCd = this.dtoHaisha.ShashuCd;
            this.dtoHaisha.ShashuCd = null;

            foreach (Row row in this.form.mrHaisha.Rows)
            {
                var cellUntenshaCd = (GcCustomTextBoxCell)row[CELL_NAME_UNTENSHA_CD];
                var cellSharyouCd = (GcCustomTextBoxCell)row[CELL_NAME_SHARYOU_CD];
                var cellGyoushaCd = (GcCustomTextBoxCell)row[CELL_NAME_UNPAN_GYOUSHA_CD];

                if (!string.IsNullOrEmpty(Convert.ToString(cellUntenshaCd.Value)))
                {
                    // 運転者
                    this.dtoHaisha.ShainCd = Convert.ToString(cellUntenshaCd.Value);
                    var dtu = this.dao_HAISHA.GetShain(this.dtoHaisha);
                    if (dtu.Rows.Count > 0)
                    {
                        var dr = dtu.Rows[0];
                        if (dr["WORK_CLOSED_UNTENSHA"].Equals(true))
                        {
                            cellUntenshaCd.IsInputErrorOccured = true;
                            cellUntenshaCd.Style.BackColor = Constans.ERROR_COLOR;
                            msgLogic.MessageBoxShow("E206", "運転者", string.Format("作業日：{0}", dtoHaisha.SagyouDate));
                            return false;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(Convert.ToString(cellSharyouCd.Value)))
                {
                    // 車輛
                    this.dtoHaisha.SharyouCd = Convert.ToString(cellSharyouCd.Value);
                    this.dtoHaisha.GyoushaCd = Convert.ToString(cellGyoushaCd.Value);
                    var dt = this.dao_HAISHA.GetSharyou(this.dtoHaisha);
                    if (dt.Rows.Count > 0)
                    {
                        var dr = dt.Rows[0];
                        if (dr["WORK_CLOSED_SHARYOU"].Equals(true))
                        {
                            cellSharyouCd.IsInputErrorOccured = true;
                            cellSharyouCd.Style.BackColor = Constans.ERROR_COLOR;
                            msgLogic.MessageBoxShow("E206", "車輛", string.Format("作業日：{0}", dtoHaisha.SagyouDate));
                            return false;
                        }
                    }
                }
            }

            this.dtoHaisha.ShashuCd = shashuCd;
            return true;
        }
        #endregion

        #region 変更先割り当て情報チェック
        /// <summary>
        /// 変更先の枠にデータが存在しているか。
        /// </summary>
        /// <param name="cellHaisha">選択中のcell</param>
        /// <returns>true:存在している。false：存在していない。</returns>
        private bool IsAllocatedDenpyou(Cell cellHaisha)
        {
            bool retVal = false;

            var suffix = this.GetColumn(cellHaisha);

            //DTO_Haisha searchInfo = new DTO_Haisha();
            //searchInfo.SagyouDate = this.dtoHaisha.SagyouDate;
            //searchInfo.GyoushaCd = ((GcCustomTextBoxCell)this.mrHaisha[cellHaisha.RowIndex, CELL_NAME_UNPAN_GYOUSHA_CD]).Value.ToString();
            //searchInfo.SharyouCd = ((GcCustomTextBoxCell)this.mrHaisha[cellHaisha.RowIndex, CELL_NAME_SHARYOU_CD]).Value.ToString();
            //searchInfo.ShainCd = ((GcCustomTextBoxCell)this.mrHaisha[cellHaisha.RowIndex, CELL_NAME_UNTENSHA_CD]).Value.ToString();

            //var haisyaWariateDays = this.dao_HAISHA.GetValidData(searchInfo);

            //if (haisyaWariateDays != null && haisyaWariateDays.Count() > 0)
            //{
            //    var tempType = haisyaWariateDays[0].GetType();
            //    SqlInt64 tempSysId = (SqlInt64)tempType.GetProperty("SYSTEM_ID_" + suffix).GetValue(haisyaWariateDays[0], null);
            //    if (!tempSysId.IsNull && !string.IsNullOrEmpty(tempSysId.ToString()))
            //    {
            //        SqlInt16 tempShubetsuKbn = (SqlInt16)tempType.GetProperty("SHUBETSU_KBN_" + suffix).GetValue(haisyaWariateDays[0], null);

            //        object tempDenpyouData = null;
            //        DTO_IdSeq searchDenpyouInfo = new DTO_IdSeq();
            //        searchDenpyouInfo.SystemId = (long)tempSysId;
            //        searchDenpyouInfo.SagyouDate = searchInfo.SagyouDate;
            //        searchDenpyouInfo.GyoushaCd = searchInfo.GyoushaCd;
            //        searchDenpyouInfo.SharyouCd = searchInfo.SharyouCd;
            //        searchDenpyouInfo.ShainCd = searchInfo.ShainCd;

            //        if (tempShubetsuKbn == 1)
            //        {
            //            // 収集受付
            //            tempDenpyouData = this.daoT_UKETSUKE_SS_ENTRY.GetValidData(searchDenpyouInfo);
            //        }
            //        else if (tempShubetsuKbn == 2)
            //        {
            //            // 出荷受付
            //            tempDenpyouData = this.daoT_UKETSUKE_SK_ENTRY.GetValidData(searchDenpyouInfo);
            //        }
            //        else if (tempShubetsuKbn == 3)
            //        {
            //            // 定期配車
            //            tempDenpyouData = this.daoT_TEIKI_HAISHA_ENTRY.GetValidData(searchDenpyouInfo);
            //        }
            //        else
            //        {
            //            // 検索しようがない場合
            //            return retVal;
            //        }

            //        if (tempDenpyouData != null)
            //        {
            //            msgLogic.MessageBoxShowError("変更後作業日の配車枠には、配車伝票が登録済みです。他の配車枠へセットしてください。");
            //            retVal = true;
            //        }
            //    }
            //}
            var drHaisha = this.resultHaisha.Rows[cellHaisha.RowIndex];
            if (drHaisha.Field<short>("SHUBETSU_KBN_" + suffix) != ConstClass.SHUBETSU_KBN_EMPTY && drHaisha["HAISHA_FLG" + suffix].ToString() != ConstClass.NO_HAISHA_FLG)
            {
                if (this.selectedCell == null || (this.selectedCell != null && (this.selectedCell.RowIndex != cellHaisha.RowIndex || this.GetColumn(this.selectedCell) != suffix)))
                {
                    msgLogic.MessageBoxShowError("変更後作業日の配車枠には、配車伝票が登録済みです。他の配車枠へセットしてください。");
                    retVal = true;
                }
            }
            return retVal;
        }
        #endregion

        //-------------------------ThangNguyen [Add] 20150722 End-------------------------

        #region 登録
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            //ThangNguyen [Add] 20150722 Start
            if (!isRowDouble()) return;

            // 必須入力チェック
            this.SetRequiredSetting();
            int iNumControl = 1;
            Control[] aryCtrl = new Control[iNumControl];
            aryCtrl[0] = this.mrHaisha;
            var autoCheckLogic = new AutoRegistCheckLogic(aryCtrl, aryCtrl);
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
            if (this.form.RegistErrorFlag) return;
            if (this.SharyouUntenshaInputErrorCheck())
            {
                this.form.RegistErrorFlag = true;
            }
            if (this.form.RegistErrorFlag) return;
            if (!ChkWorkClose())
            {
                return;
            }
            //ThangNguyen [Add] 20150722 End

            Func<string, short> GetHaishaShuruiCd = (haishaShurui) =>
                haishaShurui == ConstClass.HAISHA_SHURUI_KARI ? (short)2 :
                haishaShurui == ConstClass.HAISHA_SHURUI_KAKU ? (short)3 :
                (short)1;
            Func<string, string> GetHaishaShuruiName = (haishaShurui) =>
                haishaShurui == ConstClass.HAISHA_SHURUI_KARI ? "仮押" :
                haishaShurui == ConstClass.HAISHA_SHURUI_KAKU ? "確定" :
                "通常";
            Func<string, bool> GetHaishaSijishoFlg = (status) =>
                status == ConstClass.HAISHA_SIJISHO_TRUE;
            Func<string, bool> GetMailSendFlg = (status) =>
                status == ConstClass.MAIL_SEND_TRUE;

            Action<DataRow, string> RegistUketsukeSS = (dr, n) =>
            {
                var sse = this.daoT_UKETSUKE_SS_ENTRY.GetData(this.dtoIdSeq);
                /*SqlInt16 haisyaJoukyou = -1;
                haisyaJoukyou = sse.HAISHA_JOKYO_CD;*/

                var createDate = sse.CREATE_DATE;
                var createUser = sse.CREATE_USER;
                var createPc = sse.CREATE_PC;

                sse.DELETE_FLG = true;
                /*sse.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                sse.UPDATE_USER = SystemProperty.UserName;
                sse.UPDATE_PC = SystemInformation.ComputerName;*/
                new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(sse).SetSystemProperty(sse, true);
                this.daoT_UKETSUKE_SS_ENTRY.Update(sse);

                sse.DELETE_FLG = false;
                // DELETE_FLG立てる時にユーザー名とPC名設定しているのでUPDATE_DATEのみを更新
                /*sse.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());*/
                new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(sse).SetSystemProperty(sse, false);
                if (sse.SEQ > 0)
                {
                    sse.CREATE_DATE = createDate;
                    sse.CREATE_USER = createUser;
                    sse.CREATE_PC = createPc;
                }
                sse.SEQ = sse.SEQ + 1;
                // 車輌・運転者のどちらかが入力されていない場合は、配車状況を変更しない
                if (!String.IsNullOrEmpty(dr.Field<string>("SHARYOU_CD")) && !String.IsNullOrEmpty(dr.Field<string>("SHAIN_CD")))
                {
                    // 計上 or 回収なし の場合は配車状況を変更しない
                    if (ConstClass.HAISHA_JOKYO_CD_KEIJO != sse.HAISHA_JOKYO_CD && ConstClass.HAISHA_JOKYO_CD_NASHI != sse.HAISHA_JOKYO_CD)
                    {
                        sse.HAISHA_JOKYO_CD = 2;
                        sse.HAISHA_JOKYO_NAME = "配車";
                    }
                }
                var haishaShurui = dr.Field<string>("HAISHA_SHURUI" + n);
                sse.HAISHA_SHURUI_CD = GetHaishaShuruiCd(haishaShurui);
                sse.HAISHA_SHURUI_NAME = GetHaishaShuruiName(haishaShurui);
                sse.SAGYOU_DATE = this.dtoHaisha.SagyouDate;
                sse.SHARYOU_CD = dr.Field<string>("SHARYOU_CD");
                sse.SHARYOU_NAME = dr.Field<string>("SHARYOU_NAME_RYAKU");
                sse.SHASHU_CD = dr.Field<string>("SHASYU_CD");
                sse.SHASHU_NAME = dr.Field<string>("SHASHU_NAME_RYAKU");
                sse.UNTENSHA_CD = dr.Field<string>("SHAIN_CD");
                sse.UNTENSHA_NAME = dr.Field<string>("SHAIN_NAME_RYAKU");
                sse.HAISHA_SIJISHO_FLG = GetHaishaSijishoFlg(dr.Field<string>("HAISHA_SIJISHO_STATUS" + n));
                sse.MAIL_SEND_FLG = GetMailSendFlg(dr.Field<string>("MAIL_SEND_STATUS" + n));
                sse.UNPAN_GYOUSHA_CD = dr.Field<string>("GYOUSHA_CD");
                sse.UNPAN_GYOUSHA_NAME = dr.Field<string>("GYOUSHA_NAME_RYAKU");
                this.daoT_UKETSUKE_SS_ENTRY.Insert(sse);

                var ssdArray = this.daoT_UKETSUKE_SS_DETAIL.GetData(this.dtoIdSeq);
                foreach (var ssd in ssdArray)
                {
                    new DataBinderLogic<T_UKETSUKE_SS_DETAIL>(ssd).SetSystemProperty(ssd, false);
                    ssd.SEQ = sse.SEQ;
                    this.daoT_UKETSUKE_SS_DETAIL.Insert(ssd);
                }

                var crArray = this.daoT_CONTENA_RESERVE.GetData(this.dtoIdSeq);
                foreach (var cr in crArray)
                {
                    createDate = cr.CREATE_DATE;
                    createUser = cr.CREATE_USER;
                    createPc = cr.CREATE_PC;

                    cr.DELETE_FLG = true;
                    new DataBinderLogic<T_CONTENA_RESERVE>(cr).SetSystemProperty(cr, true);
                    this.daoT_CONTENA_RESERVE.Update(cr);

                    cr.DELETE_FLG = false;
                    new DataBinderLogic<T_CONTENA_RESERVE>(cr).SetSystemProperty(cr, false);
                    if (cr.SEQ > 0)
                    {
                        cr.CREATE_DATE = createDate;
                        cr.CREATE_USER = createUser;
                        cr.CREATE_PC = createPc;
                    }
                    cr.SEQ = sse.SEQ;
                    this.daoT_CONTENA_RESERVE.Insert(cr);
                }

                // コンテナが更新されるデータか確認
                /*if ((haisyaJoukyou == ConstClass.HAISHA_JOKYO_CD_KEIJO
                    || haisyaJoukyou == ConstClass.HAISHA_JOKYO_CD_NASHI)
                    && (crArray != null && crArray.Count() > 0))
                {
                    isExstingContenaData = true;
                }*/
            };

            Action<DataRow, string> RegistUketsukeSK = (dr, n) =>
            {
                var ske = this.daoT_UKETSUKE_SK_ENTRY.GetData(this.dtoIdSeq);

                var createDate = ske.CREATE_DATE;
                var createUser = ske.CREATE_USER;
                var createPc = ske.CREATE_PC;

                ske.DELETE_FLG = true;
                /*ske.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                ske.UPDATE_USER = SystemProperty.UserName;
                ske.UPDATE_PC = SystemInformation.ComputerName;*/
                new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(ske).SetSystemProperty(ske, true);
                this.daoT_UKETSUKE_SK_ENTRY.Update(ske);

                ske.DELETE_FLG = false;
                // DELETE_FLG立てる時にユーザー名とPC名設定しているのでUPDATE_DATEのみを更新
                //ske.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(ske).SetSystemProperty(ske, false);
                if (ske.SEQ > 0)
                {
                    ske.CREATE_DATE = createDate;
                    ske.CREATE_USER = createUser;
                    ske.CREATE_PC = createPc;
                }
                ske.SEQ = ske.SEQ + 1;

                // 車輌・運転者のどちらかが入力されていない場合は、配車状況を変更しない
                if (!String.IsNullOrEmpty(dr.Field<string>("SHARYOU_CD")) && !String.IsNullOrEmpty(dr.Field<string>("SHAIN_CD")))
                {
                    // 計上 or 回収なし の場合は配車状況を変更しない
                    if (ConstClass.HAISHA_JOKYO_CD_KEIJO != ske.HAISHA_JOKYO_CD && ConstClass.HAISHA_JOKYO_CD_NASHI != ske.HAISHA_JOKYO_CD)
                    {
                        ske.HAISHA_JOKYO_CD = 2;
                        ske.HAISHA_JOKYO_NAME = "配車";
                    }
                }
                var haishaShurui = dr.Field<string>("HAISHA_SHURUI" + n);
                ske.HAISHA_SHURUI_CD = GetHaishaShuruiCd(haishaShurui);
                ske.HAISHA_SHURUI_NAME = GetHaishaShuruiName(haishaShurui);
                ske.SAGYOU_DATE = this.dtoHaisha.SagyouDate;
                ske.SHARYOU_CD = dr.Field<string>("SHARYOU_CD");
                ske.SHARYOU_NAME = dr.Field<string>("SHARYOU_NAME_RYAKU");
                ske.SHASHU_CD = dr.Field<string>("SHASYU_CD");
                ske.SHASHU_NAME = dr.Field<string>("SHASHU_NAME_RYAKU");
                ske.UNTENSHA_CD = dr.Field<string>("SHAIN_CD");
                ske.UNTENSHA_NAME = dr.Field<string>("SHAIN_NAME_RYAKU");
                ske.HAISHA_SIJISHO_FLG = GetHaishaSijishoFlg(dr.Field<string>("HAISHA_SIJISHO_STATUS" + n));
                ske.MAIL_SEND_FLG = GetMailSendFlg(dr.Field<string>("MAIL_SEND_STATUS" + n));
                ske.UNPAN_GYOUSHA_CD = dr.Field<string>("GYOUSHA_CD");
                ske.UNPAN_GYOUSHA_NAME = dr.Field<string>("GYOUSHA_NAME_RYAKU");
                this.daoT_UKETSUKE_SK_ENTRY.Insert(ske);

                var skdArray = this.daoT_UKETSUKE_SK_DETAIL.GetData(this.dtoIdSeq);
                foreach (var skd in skdArray)
                {
                    new DataBinderLogic<T_UKETSUKE_SK_DETAIL>(skd).SetSystemProperty(skd, false);
                    skd.SEQ = ske.SEQ;
                    this.daoT_UKETSUKE_SK_DETAIL.Insert(skd);
                }
            };

            Action<DataRow, string> RegistTeikiHaisha = (dr, n) =>
            {
                var tke = this.daoT_TEIKI_HAISHA_ENTRY.GetData(this.dtoIdSeq);

                var createDate = tke.CREATE_DATE;
                var createUser = tke.CREATE_USER;
                var createPc = tke.CREATE_PC;

                tke.DELETE_FLG = true;
                new DataBinderLogic<T_TEIKI_HAISHA_ENTRY>(tke).SetSystemProperty(tke, true);
                this.daoT_TEIKI_HAISHA_ENTRY.Update(tke);

                tke.DELETE_FLG = false;
                new DataBinderLogic<T_TEIKI_HAISHA_ENTRY>(tke).SetSystemProperty(tke, false);
                if (tke.SEQ > 0)
                {
                    tke.CREATE_DATE = createDate;
                    tke.CREATE_USER = createUser;
                    tke.CREATE_PC = createPc;
                }
                tke.SEQ = tke.SEQ + 1;
                tke.SAGYOU_DATE = DateTime.Parse(this.dtoHaisha.SagyouDate);
                tke.SHARYOU_CD = dr.Field<string>("SHARYOU_CD");
                tke.SHASHU_CD = dr.Field<string>("SHASYU_CD");
                tke.UNTENSHA_CD = dr.Field<string>("SHAIN_CD");
                tke.UNPAN_GYOUSHA_CD = dr.Field<string>("GYOUSHA_CD");
                this.daoT_TEIKI_HAISHA_ENTRY.Insert(tke);

                var tkdArray = this.daoT_TEIKI_HAISHA_DETAIL.GetData(this.dtoIdSeq);
                foreach (var tkd in tkdArray)
                {
                    new DataBinderLogic<T_TEIKI_HAISHA_DETAIL>(tkd).SetSystemProperty(tkd, false);
                    tkd.SEQ = tke.SEQ;
                    this.daoT_TEIKI_HAISHA_DETAIL.Insert(tkd);
                }

                var tknArray = this.daoT_TEIKI_HAISHA_NIOROSHI.GetData(this.dtoIdSeq);
                foreach (var tkn in tknArray)
                {
                    new DataBinderLogic<T_TEIKI_HAISHA_NIOROSHI>(tkn).SetSystemProperty(tkn, false);
                    tkn.SEQ = tke.SEQ;
                    this.daoT_TEIKI_HAISHA_NIOROSHI.Insert(tkn);
                }

                var tksArray = this.daoT_TEIKI_HAISHA_SHOUSAI.GetData(this.dtoIdSeq);
                foreach (var tks in tksArray)
                {
                    new DataBinderLogic<T_TEIKI_HAISHA_SHOUSAI>(tks).SetSystemProperty(tks, false);
                    tks.SEQ = tke.SEQ;
                    this.daoT_TEIKI_HAISHA_SHOUSAI.Insert(tks);
                }
            };

            System.Action DeleteWariateDay = () =>
            {
                // 作業日で登録済の配車割当（一日）を検索
                var wdArray = this.dao_HAISHA.GetHaishaWariateDay(this.dtoHaisha);
                if (wdArray.Any())
                {
                    foreach (T_HAISHA_WARIATE_DAY wd in wdArray)
                    {
                        // 登録済の配車割当（一日）を削除
                        wd.DELETE_FLG = true;
                        //new DataBinderLogic<T_HAISHA_WARIATE_DAY>(wd).SetSystemProperty(wd, true);
                        this.dao_HAISHA.Update(wd);
                    }
                }
            };

            Action<DataRow, int> RegistWariateDay = (dr, rowNum) =>
            {
                this.dtoHaisha.ShainCd = dr.Field<string>("SHAIN_CD");
                this.dtoHaisha.ShashuCd = dr.Field<string>("SHASYU_CD");
                this.dtoHaisha.SharyouCd = dr.Field<string>("SHARYOU_CD");
                this.dtoHaisha.GyoushaCd = dr.Field<string>("GYOUSHA_CD");
                if (this.dtoHaisha.ShainCd == null)
                {
                    this.dtoHaisha.ShainCd = string.Empty;
                }
                if (this.dtoHaisha.ShashuCd == null)
                {
                    this.dtoHaisha.ShashuCd = string.Empty;
                }
                if (this.dtoHaisha.SharyouCd == null)
                {
                    this.dtoHaisha.SharyouCd = string.Empty;
                }
                if (this.dtoHaisha.GyoushaCd == null)
                {
                    this.dtoHaisha.GyoushaCd = string.Empty;
                }
                var wdArray = this.dao_HAISHA.GetData(this.dtoHaisha);
                T_HAISHA_WARIATE_DAY wd = new T_HAISHA_WARIATE_DAY();
                if (wdArray.Any())
                {
                    // 過去に登録されている組み合わせの場合は、SEQ+1
                    wd = wdArray[0];
                    wd.SEQ = wd.SEQ + 1;
                }
                else
                {
                    // 新しい組み合わせの場合は、SEQ=1
                    wd.SAGYOU_DATE = DateTime.Parse(this.dtoHaisha.SagyouDate);
                    wd.SEARCH_SAGYOU_DATE = this.dtoHaisha.SagyouDate;
                    wd.SHASHU_CD = this.dtoHaisha.ShashuCd;
                    wd.SHARYOU_CD = this.dtoHaisha.SharyouCd;
                    wd.GYOUSHA_CD = this.dtoHaisha.GyoushaCd;
                    wd.UNTENSHA_CD = this.dtoHaisha.ShainCd;
                    wd.SEQ = 1;
                }
                wd.ROW_NUM = rowNum;

                wd.DELETE_FLG = false;
                var createDate = wd.CREATE_DATE;
                var createUser = wd.CREATE_USER;
                var createPc = wd.CREATE_PC;
                new DataBinderLogic<T_HAISHA_WARIATE_DAY>(wd).SetSystemProperty(wd, false);
                if (!string.IsNullOrEmpty(createUser))
                {
                    wd.CREATE_DATE = createDate;
                    wd.CREATE_USER = createUser;
                    wd.CREATE_PC = createPc;
                }

                var type = wd.GetType();
                for (int i = 0; i < ConstClass.DENPYOU_COUNT; ++i)
                {
                    var n = GetSuffix(i + 1);
                    var shubetsuKbn = new SqlInt16((short)dr["SHUBETSU_KBN_" + n]);
                    var systemId = new SqlInt64((long)dr["SYSTEM_ID_" + n]);
                    var denpyouNum = new SqlInt64((long)dr["DENPYOU_NUM_" + n]);
                    var karaDenpyouFlg = new SqlBoolean((bool)dr["KARADENPYOU_FLG_" + n]);
                    if (shubetsuKbn == ConstClass.SHUBETSU_KBN_EMPTY)
                    {
                        shubetsuKbn = SqlInt16.Null;
                        systemId = denpyouNum = SqlInt64.Null;
                        continue;
                    }

                    // wd.SHUBETSU_KBN_01~30 = shubetsuKbn
                    type.GetProperty("SHUBETSU_KBN_" + n).SetValue(wd, shubetsuKbn, null);
                    // wd.SYSTEM_ID_01~30 = systemId
                    type.GetProperty("SYSTEM_ID_" + n).SetValue(wd, systemId, null);
                    // wd.DENPYOU_NUM_01~30 = denpyouNum
                    type.GetProperty("DENPYOU_NUM_" + n).SetValue(wd, denpyouNum, null);
                    // wd.KARADENPYOU_FLG_01~30 = karaDenpyouFlg
                    type.GetProperty("KARADENPYOU_FLG_" + n).SetValue(wd, karaDenpyouFlg, null);
                }

                this.dao_HAISHA.Insert(wd);
            };

            System.Action RegistHaishaMemo = () =>
            {
                var hm = daoT_HAISHA_MEMO.GetData(this.dtoHaisha);
                if (hm != null)
                {
                    hm.DELETE_FLG = true;
                    new DataBinderLogic<T_HAISHA_MEMO>(hm).SetSystemProperty(hm, false);
                    this.daoT_HAISHA_MEMO.Update(hm);
                    hm.SEQ = hm.SEQ + 1;
                }
                else
                {
                    hm = new T_HAISHA_MEMO();
                    hm.SYSTEM_ID = daoT_HAISHA_MEMO.GetNextSystemId();
                    hm.SEQ = 1;
                    hm.SAGYOU_DATE = DateTime.Parse(this.dtoHaisha.SagyouDate);
                    hm.HAISHA_MEMO = "";
                }

                hm.DELETE_FLG = false;
                hm.HAISHA_MEMO = this.form.tbHaishaMemo.Text;
                new DataBinderLogic<T_HAISHA_MEMO>(hm).SetSystemProperty(hm, true);
                this.daoT_HAISHA_MEMO.Insert(hm);
            };

            try
            {
                using (var tran = new Transaction())
                {
                    DeleteWariateDay();
                    for (int r = 0; r < this.resultHaisha.Rows.Count; ++r)
                    {
                        var dr = this.resultHaisha.Rows[r];
                        for (int c = 0; c < ConstClass.DENPYOU_COUNT; ++c)
                        {
                            var n = GetSuffix(c + 1);
                            this.dtoIdSeq.SystemId = dr.Field<long>("SYSTEM_ID_" + n);
                            this.dtoIdSeq.Seq = dr.Field<int>("SEQ" + n);
                            if (this.selectedCell != null && this.selectedCell.RowIndex == r && this.GetColumn(this.selectedCell) == n) //作業日変更のデータのみ更新
                            {
                                switch (dr.Field<short>("SHUBETSU_KBN_" + n))
                                {
                                    case ConstClass.SHUBETSU_KBN_UKETSUKE_SS:
                                            //委託契約チェック start
                                            if (!this.CheckItakukeiyaku(dr))
                                            {
                                                return;
                                            }
                                            //委託契約チェック end
                                            RegistUketsukeSS(dr, n);
                                        break;
                                    case ConstClass.SHUBETSU_KBN_UKETSUKE_SK:
                                            RegistUketsukeSK(dr, n);
                                        break;
                                    case ConstClass.SHUBETSU_KBN_TEIKI_HAISHA:
                                            RegistTeikiHaisha(dr, n);
                                        break;
                                }
                            }
                        }
                        RegistWariateDay(dr, r);
                    }

                    //RegistHaishaMemo();
                    tran.Commit();
                }
                this.msgLogic.MessageBoxShow("I001", "登録");
                this.SearchAndDisplay();
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Warn(ex); //排他は警告
                this.msgLogic.MessageBoxShow("E080");
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex); //その他はエラー
                this.msgLogic.MessageBoxShow("E093");
            }
            LogUtility.DebugMethodEnd();
            // フォームを閉じる
            this.form.DialogResult = DialogResult.OK;
            this.parentForm.DialogResult = DialogResult.OK;
            this.form.Close();
            this.parentForm.Close();
        }
        #endregion

        #region 検索
        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            this.dtoHaisha.KyotenCd = this.form.KyotenCd;
            this.dtoHaisha.ShashuCd = this.form.SHASHU_CD.Text;
            this.dtoHaisha.UntenshaCd = this.form.UNTENSHA_CD.Text;

            if (this.form.dtpSagyouDate.Value != null)
            {
                string timeTemp = ((DateTime)this.form.dtpSagyouDate.Value).ToShortDateString();
                if (timeTemp == this.form.StandandDateTime.ToShortDateString())
                {
                    msgLogic.MessageBoxShow("E242");
                    this.form.dtpSagyouDate.Focus();
                    this.form.beforeSagyouDate = string.Empty;
                    return 0;
                }
                this.dtoHaisha.SagyouDate = ((DateTime)this.form.dtpSagyouDate.Value).ToShortDateString();
            }
            //this.dtoHaisha.ShoriKbn = this.form.tbShoriKbn.Text;
            //this.form.dtpSagyouDate.Value = this.dtoHaisha.SagyouDate;

            this.dtoHaisha.HaisyaKubun = IsShasyuSyaryou(); // No.2544

            bool haishaFlg = true;
            var sharyouTable = this.dao_HAISHA.GetHaishaWariateData(this.dtoHaisha);
            if (sharyouTable == null || sharyouTable.Rows.Count == 0)
            {
                sharyouTable = this.dao_HAISHA.GetHaishaSharyouData(this.dtoHaisha);
                if (sharyouTable.Rows.Count > 0)
                {
                    var tmpDt = sharyouTable.Copy();
                    if (this.IsShasyuSyaryou())
                    {
                        tmpDt.DefaultView.Sort = "SHASYU_CD, SHARYOU_CD, GYOUSHA_CD, SHAIN_CD";
                    }
                    else
                    {
                        tmpDt.DefaultView.Sort = "SHAIN_CD, SHASYU_CD, SHARYOU_CD, GYOUSHA_CD";
                    }
                    sharyouTable = tmpDt.DefaultView.ToTable();
                }
                haishaFlg = false;
            }
            this.resultHaisha = CreateHaishaTable(sharyouTable, haishaFlg);

            this.sharyouCount = this.resultHaisha.Rows.Count;

            var resultDenpyo = this.dao_HAISHA.GetHaishaDenpyo(this.dtoHaisha);


            this.HaishaCount = 0;

            foreach (DataRow denpyou in resultDenpyo.Rows)
            {
                var gyoushaCd = denpyou["UNPAN_GYOUSHA_CD"].ToString();
                var shashuCd = denpyou["SHASHU_CD"].ToString();
                var sharyouCd = denpyou["SHARYOU_CD"].ToString();
                var untenshaCd = denpyou["UNTENSHA_CD"].ToString();

                // 配車先の行を検索
                var haishaRow = this.resultHaisha.AsEnumerable().Where(r => r["GYOUSHA_CD"].ToString() == gyoushaCd
                                                                         && r["SHASYU_CD"].ToString() == shashuCd
                                                                         && r["SHARYOU_CD"].ToString() == sharyouCd
                                                                         && r["SHAIN_CD"].ToString() == untenshaCd).FirstOrDefault();

                if (null == haishaRow)
                {
                    // 車種名を取得
                    var shashuDao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
                    var mShashu = shashuDao.GetAllValidData(new M_SHASHU() { SHASHU_CD = shashuCd }).DefaultIfEmpty(new M_SHASHU()).FirstOrDefault();
                    // 車輌名を取得
                    var sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
                    var mSharyou = sharyouDao.GetAllValidData(new M_SHARYOU() { SHARYOU_CD = sharyouCd, GYOUSHA_CD = gyoushaCd }).DefaultIfEmpty(new M_SHARYOU()).FirstOrDefault();
                    // 社員名を取得
                    var mShainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
                    var mShain = mShainDao.GetAllValidData(new M_SHAIN() { SHAIN_CD = untenshaCd }).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();
                    // 運搬業者名を取得
                    var mUnpanGyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                    var mUnpanGyousha = mUnpanGyoushaDao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = gyoushaCd }).DefaultIfEmpty(new M_GYOUSHA()).FirstOrDefault();
                    // 行がなければ、新規行を追加する
                    haishaRow = this.resultHaisha.NewRow();
                    haishaRow["GYOUSHA_CD"] = gyoushaCd;
                    haishaRow["GYOUSHA_NAME_RYAKU"] = mUnpanGyousha.GYOUSHA_NAME_RYAKU;
                    haishaRow["SHASYU_CD"] = shashuCd;
                    haishaRow["SHASHU_NAME_RYAKU"] = mShashu.SHASHU_NAME_RYAKU;
                    haishaRow["SHARYOU_CD"] = sharyouCd;
                    haishaRow["SHARYOU_NAME_RYAKU"] = mSharyou.SHARYOU_NAME_RYAKU;
                    haishaRow["SHAIN_CD"] = untenshaCd;
                    haishaRow["SHAIN_NAME_RYAKU"] = mShain.SHAIN_NAME_RYAKU;
                    this.resultHaisha.Rows.Add(haishaRow);

                }
                if (haishaRow != null)
                {
                    int emptyCell = 0;

                    if ((this.sysInfoEntity != null && this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU == 1) || (denpyou["HAISHA_FLG"].ToString() == ConstClass.NO_HAISHA_FLG))
                    {
                        int tempWariateKaishi = this.sysInfoEntity.HAISHA_WARIATE_KAISHI.IsNull ? 0 : (int)this.sysInfoEntity.HAISHA_WARIATE_KAISHI;

                        for (int i = (tempWariateKaishi + 1); i <= ConstClass.DENPYOU_COUNT; i++)
                        {
                            // あいている配車のセルを探す
                            if (String.IsNullOrEmpty(haishaRow["DENPYOU_CONTENT" + GetSuffix(i)].ToString()) && !(bool)haishaRow["KARADENPYOU_FLG_" + GetSuffix(i)])
                            {
                                emptyCell = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        emptyCell = int.Parse(denpyou["DENPYOU_SEQ"].ToString());
                    }
                    if (emptyCell != 0 && emptyCell <= ConstClass.DENPYOU_COUNT)
                    {
                        foreach (var di in ConstClass.DENPYOU_INFO)
                        {
                            var fn = di.FieldName;
                            if (fn != "ROW_NUM")
                            {
                                haishaRow[fn + GetSuffix(emptyCell)] = denpyou[fn];
                            }
                        }

                        // 配車件数
                        this.HaishaCount++;
                    }
                }
            }

            var hm = daoT_HAISHA_MEMO.GetData(this.dtoHaisha);
            this.form.tbHaishaMemo.Text = hm == null ? "" : hm.HAISHA_MEMO;
            this.changeHaishaFlg = false;

            LogUtility.DebugMethodEnd(this.sharyouCount);

            return this.sharyouCount;
        }

        public DataTable CreateHaishaTable(DataTable tableSharyou, bool haishaFlg)
        {
            DataTable tableHaisha = new DataTable();
            foreach (var di in ConstClass.DENPYOU_HEADER_INFO)
            {
                tableHaisha.Columns.Add(di.FieldName, di.type);
                tableHaisha.Columns[di.FieldName].DefaultValue = di.DefaultValue;
            }
            for (int i = 0; i < ConstClass.DENPYOU_COUNT; i++)
            {
                foreach (var di in ConstClass.DENPYOU_INFO)
                {
                    tableHaisha.Columns.Add(di.FieldName + GetSuffix(i + 1), di.type);
                    tableHaisha.Columns[di.FieldName + GetSuffix(i + 1)].DefaultValue = di.DefaultValue;

                }
                
            }
            foreach (DataRow row in tableSharyou.Rows)
            {
                DataRow rowHaisha = tableHaisha.NewRow();
                foreach (var di in ConstClass.DENPYOU_HEADER_INFO)
                {
                    var fn = di.FieldName;

                    rowHaisha[fn] = row[fn];
                }
                if (haishaFlg == true)
                {
                    for (int i = 0; i < ConstClass.DENPYOU_COUNT; i++)
                    {
                        string suffix = GetSuffix(i + 1);
                        if (row["KARADENPYOU_FLG_" + suffix] != DBNull.Value)
                        {
                            rowHaisha["KARADENPYOU_FLG_" + suffix] = row["KARADENPYOU_FLG_" + suffix];
                        }
                    }
                }
                tableHaisha.Rows.Add(rowHaisha);
            }
            return tableHaisha;
        }
        #endregion

        #region 入力軸が車種／車両か
        /// <summary>
        /// 入力軸が車種／車両か
        /// </summary>
        /// <returns>true:車種／車両　false:運転者</returns>
        internal bool IsShasyuSyaryou()
        {
            bool retResult = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.sysInfoEntity != null)
                {
                    // 2の場合
                    if (this.ChgDBNullToValue(sysInfoEntity.HAISHA_ONEDAY_NYUURYOKU_KBN, string.Empty).ToString().Equals("2"))
                    {
                        // trueを返す
                        retResult = true;
                    }
                }

                return retResult;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retResult);
            }
        }
        #endregion

        #region DBNull値を指定値に変換
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            object returnVal = null;
            if (obj is DBNull)
            {
                returnVal = value;
            }
            else
            {
                returnVal = obj;
            }

            return returnVal;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private string GetColumn(Cell cell)
        {
            return GetColumn(cell.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns></returns>
        private string GetColumn(string cellName)
        {
            return cellName.Substring(cellName.Length - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string GetSuffix(int i)
        {
            return i.ToString("D2");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="dr"></param>
        /// <param name="fn1"></param>
        /// <param name="fn2"></param>
        private void FormatDenpyou(CellFormattingEventArgs e, DataRow dr, string fn1, string fn2)
        {
            //LogUtility.DebugMethodStart(e, dr, fn1, fn2);

            if (e.CellName.Contains("HaishaShurui"))
            {
                var shurui = dr[fn1].ToString();
                e.CellStyle.ForeColor =
                    shurui == ConstClass.HAISHA_SHURUI_KAKU ? Color.Red :
                    shurui == ConstClass.HAISHA_SHURUI_KARI ? Color.Green :
                    e.CellStyle.ForeColor;
            }
            else if (e.CellName.Contains("GenchakuJikan"))
            {
                var bkColor = dr[fn2];
                if (!bkColor.Equals(DBNull.Value))
                {
                    //e.CellStyle.BackColor = Color.FromArgb((int)bkColor);
                    // 現着マスター画面でKnownColorの値が設定されたので、KnownColorから取得
                    e.CellStyle.BackColor = Color.FromKnownColor((KnownColor)int.Parse(bkColor.ToString()));
                }
            }
            else if (e.CellName.Contains("SagyouDateKubun"))
            {
                if (e.Value != null)
                {
                    var v = e.Value.ToString();
                    e.Value = v == "" ? "" : "期間";
                    if (v == "期間終了")
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
            }
            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void mrHaisha_CellFormatting(CellFormattingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(e);
                if (e.CellName == "cellUnused1" || e.CellName == "cellUnused2" ||
                   e.CellName == "cellUntenshaName" || e.CellName == "cellShashuName" ||
                   e.CellName == "cellUnpanGyoushaName" || e.CellName == "cellSharyouName")
                {
                    e.CellStyle.BackColor = Color.GreenYellow;
                    e.CellStyle.SelectionBackColor = Color.GreenYellow;
                    return;
                }
                if (e.CellName == "cellUntenshaCd" || e.CellName == "cellShashuCd" ||
                   e.CellName == "cellUnpanGyoushaCd" || e.CellName == "cellSharyouCd")
                {
                    e.CellStyle.BackColor = Constans.READONLY_COLOR;
                    e.CellStyle.SelectionBackColor = Constans.READONLY_COLOR;
                    return;
                }
                int n;
                if (e.RowIndex < this.resultHaisha.Rows.Count && int.TryParse(GetColumn(e.CellName), out n))
                {
                    var drHaisha = this.resultHaisha.Rows[e.RowIndex];
                    string suffix = GetColumn(e.CellName);

                    if (this.sysInfoEntity != null && !this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU.IsNull && this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU == 1)
                    {
                        if (drHaisha.Field<bool>("WORK_CLOSED_SHARYOU") || drHaisha.Field<bool>("WORK_CLOSED_UNTENSHA") || n > drHaisha.Field<int>("SAIDAI_WAKU_SUU"))
                        {
                            e.CellStyle.BackColor = Color.LightGray;
                            e.CellStyle.SelectionBackColor = Color.LightGray;
                            e.CellStyle.DisabledBackColor = Color.LightGray;
                            return;
                        }
                    }
                    
                    bool karadenpyou = drHaisha.Field<bool>("KARADENPYOU_FLG_" + suffix);
                    if (karadenpyou)
                    {
                        e.CellStyle.BackColor = Color.Gray;
                        e.CellStyle.SelectionBackColor = Color.Gray;
                        e.CellStyle.DisabledBackColor = Color.Gray;
                        return;
                    }

                    if (this.selectedCell != null && e.RowIndex == this.selectedCell.RowIndex && this.GetColumn(this.selectedCell) == suffix)
                    {
                        if (ConstClass.BlockHaishaCellName.Contains(e.CellName.Substring(0, e.CellName.Length - 2)))
                        {
                            e.CellStyle.BackColor = ConstClass.blockDetailColor;
                            e.CellStyle.SelectionBackColor = ConstClass.blockDetailColor;
                            e.CellStyle.DisabledBackColor = ConstClass.blockDetailColor;
                        }
                    }
                   
                
                    FormatDenpyou(e, drHaisha,
                        "HAISHA_SHURUI" + GetSuffix(n),
                        "GENCHAKU_BACK_COLOR" + GetSuffix(n));

                }
                
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrHaisha_CellFormatting", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void mrHaisha_CellBeginEdit(CellBeginEditEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(e);

                if (e.CellName.Contains("CheckBox"))
                {
                    if (e.RowIndex >= this.resultHaisha.Rows.Count)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        var drHaisha = this.resultHaisha.Rows[e.RowIndex];
                        var shubetsuKbn = drHaisha.Field<short>("SHUBETSU_KBN_" + GetColumn(e.CellName));
                        if (shubetsuKbn == ConstClass.SHUBETSU_KBN_EMPTY ||
                            shubetsuKbn == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
                        {
                            e.Cancel = true;
                        }
                    }
                }
                else if (e.RowIndex < this.sharyouCount)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrHaisha_CellBeginEdit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private bool isDenpyouContent(Cell cell)
        {
            return cell != null && cell.Name.Contains("DenpyouContent");
        }

        //ThangNguyen [Add] 20150721 End
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private bool isNewHaisha(Cell cell)
        {
            return cell.RowIndex == this.resultHaisha.Rows.Count;
        }


        /// <summary>
        /// 伝票取替え処理
        /// </summary>
        /// <param name="drHaishaTo"></param>
        /// <param name="suffixTo"></param>
        /// <param name="drHaishaFrom"></param>
        /// <param name="suffixFrom"></param>
        private bool SwapDenpyou(DataRow drHaishaTo, string suffixTo, DataRow drHaishaFrom, string suffixFrom)
        {
            // 前伝票
            var denpyouNumberFrom = drHaishaFrom.Field<long>("DENPYOU_NUM_" + suffixFrom).ToString();
            var shubetsuFrom = drHaishaFrom.Field<short>("SHUBETSU_KBN_" + suffixFrom);

            // 後伝票
            var denpyouNumberTo = drHaishaTo.Field<long>("DENPYOU_NUM_" + suffixTo).ToString();
            var shubetsuTo = drHaishaTo.Field<short>("SHUBETSU_KBN_" + suffixTo);

            // 確定の場合、交換禁止
            if (drHaishaFrom.Field<string>("HAISHA_SHURUI" + suffixFrom) == ConstClass.HAISHA_SHURUI_KAKU ||
                    drHaishaTo.Field<string>("HAISHA_SHURUI" + suffixTo) == ConstClass.HAISHA_SHURUI_KAKU)
            {
                msgLogic.MessageBoxShow("E112");
                return false;
            }
            if (drHaishaTo == drHaishaFrom)
            {
                // 設定された最大枠数を超える場合
                if (this.sysInfoEntity != null && !this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU.IsNull && this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU == 1)
                {
                    int intMaxWakuSuu = drHaishaTo.Field<int>("SAIDAI_WAKU_SUU");
                    if (int.Parse(suffixTo) > intMaxWakuSuu)
                    {
                        // 確認メッセージを表示
                        if (this.msgLogic.MessageBoxShow("C056") != DialogResult.Yes)
                        {
                            return false;
                        }
                    }
                }

                Object objTemp = null;
                foreach (var di in ConstClass.DENPYOU_INFO)
                {
                    var fn = di.FieldName;
                    objTemp = drHaishaTo[fn + suffixTo];
                    drHaishaTo[fn + suffixTo] = drHaishaFrom[fn + suffixFrom];
                    drHaishaFrom[fn + suffixFrom] = objTemp;
                }
                this.changeHaishaFlg = true;
                return true;
            }
            else
            {
                if (!drHaishaTo.Field<bool>("WORK_CLOSED_SHARYOU") &&
                    !drHaishaTo.Field<bool>("WORK_CLOSED_UNTENSHA") ||
                    msgLogic.MessageBoxShow("C054") == DialogResult.Yes)
                {

                    // 設定された最大枠数を超える場合
                    if (this.sysInfoEntity != null && !this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU.IsNull && this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU == 1)
                    {
                        int intMaxWakuSuu = drHaishaTo.Field<int>("SAIDAI_WAKU_SUU");
                        if (int.Parse(suffixTo) > intMaxWakuSuu)
                        {
                            // 確認メッセージを表示
                            if (this.msgLogic.MessageBoxShow("C056") != DialogResult.Yes)
                            {
                                return false;
                            }
                        }
                    }

                    Object objTemp = null;
                    foreach (var di in ConstClass.DENPYOU_INFO)
                    {
                        var fn = di.FieldName;
                        objTemp = drHaishaTo[fn + suffixTo];
                        drHaishaTo[fn + suffixTo] = drHaishaFrom[fn + suffixFrom];
                        drHaishaFrom[fn + suffixFrom] = objTemp;
                    }
                    this.changeHaishaFlg = true;
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="miHaishaRowIndex"></param>
        /// <param name="suffixTo"></param>
        /// <param name="drHaishaTo"></param>
        private bool AllocateDenpyou(string suffixTo, DataRow drHaishaTo)
        {
            var drTarget = this.form.TargetRow;
            var suffixTarget = this.form.TargetCol;

            if (drHaishaTo.Field<string>("HAISHA_SHURUI" + suffixTo) == ConstClass.HAISHA_SHURUI_KAKU)
            {
                msgLogic.MessageBoxShow("E112");
                return false;
            }
            if (!drHaishaTo.Field<bool>("WORK_CLOSED_SHARYOU") &&
                    !drHaishaTo.Field<bool>("WORK_CLOSED_UNTENSHA") ||
                    msgLogic.MessageBoxShow("C054") == DialogResult.Yes)
            {

                int i1 = int.Parse(suffixTo), i2 = i1;
                while (i2 <= ConstClass.DENPYOU_COUNT && (drHaishaTo.Field<short>("SHUBETSU_KBN_" + GetSuffix(i2)) != ConstClass.SHUBETSU_KBN_EMPTY || drHaishaTo.Field<bool>("KARADENPYOU_FLG_" + GetSuffix(i2))))
                {
                    ++i2;
                }
                // 最大枠数(30)を超える場合
                if (i2 > ConstClass.DENPYOU_COUNT)
                {
                    this.msgLogic.MessageBoxShow("E133");
                    return false;
                }
                // 設定された最大枠数を超える場合
                if (this.sysInfoEntity != null && !this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU.IsNull && this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU == 1)
                {
                    int intMaxWakuSuu = drHaishaTo.Field<int>("SAIDAI_WAKU_SUU");
                    if (int.Parse(suffixTo) > intMaxWakuSuu)
                    {
                        // 確認メッセージを表示
                        if (this.msgLogic.MessageBoxShow("C056") != DialogResult.Yes)
                        {
                            return false;
                        }
                    }
                }
                for (int i = i2; i > i1; --i)
                {
                    if (!drHaishaTo.Field<bool>("KARADENPYOU_FLG_" + GetSuffix(i)))
                    {
                        int to = -1;
                        for (int j = i - 1; j > i1 - 1; j--)
                        {
                            if (!drHaishaTo.Field<bool>("KARADENPYOU_FLG_" + GetSuffix(j)))
                            {
                                to = j;
                                break;
                            }
                        }
                        if (to > -1)
                        {
                            foreach (var di in ConstClass.DENPYOU_INFO)
                            {
                                var fn = di.FieldName;
                                drHaishaTo[fn + GetSuffix(i)] = drHaishaTo[fn + GetSuffix(to)];
                            }
                        }
                    }
                }

                //Phan bo vao luoi haisha
                 foreach (var di in ConstClass.DENPYOU_INFO)
                {
                    var fn = di.FieldName;
                    drHaishaTo[fn + suffixTo] = drTarget[fn + suffixTarget];
                }
				this.HaishaCount++;
                this.changeHaishaFlg = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void mrHaisha_CellClick(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                var cellHaisha = this.mrHaisha[e.RowIndex, e.CellName];

                if (!isNewHaisha(cellHaisha) && this.isDenpyouContent(cellHaisha))
                {
                    if (r_framework.Authority.Manager.CheckAuthority("G026", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false)
                    && !this.IsAllocatedDenpyou(cellHaisha))
                    {
                        string suffixTo = GetColumn(cellHaisha.Name);
                        if (!(bool)this.resultHaisha.Rows[e.RowIndex]["KARADENPYOU_FLG_" + suffixTo])
                        {
                            var drHaishaTo = this.resultHaisha.Rows[cellHaisha.RowIndex];
                            if (this.selectedCell!=null)
                            {
                                string suffixFrom = this.GetColumn(this.selectedCell);
                                if (this.selectedCell.RowIndex != e.RowIndex || suffixFrom != suffixTo)
                                {
                                    var drHaishaFrom = this.resultHaisha.Rows[this.selectedCell.RowIndex];
                                    if (drHaishaFrom.Field<short>("SHUBETSU_KBN_" + suffixFrom) != ConstClass.SHUBETSU_KBN_EMPTY)
                                    {
                                        if (SwapDenpyou(drHaishaTo, suffixTo, drHaishaFrom, suffixFrom))
                                        {
                                            this.selectedCell = cellHaisha;
                                            this.form.mrHaisha.Refresh();
                                            this.parentForm.bt_func9.Enabled = true;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                if (AllocateDenpyou(suffixTo, drHaishaTo))
                                {
                                    this.selectedCell = cellHaisha;
                                    this.form.mrHaisha.Refresh();
                                    this.parentForm.bt_func9.Enabled = true;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrHaisha_CellClick", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        // 20141107 Houkakou 委託契約チェック start
        #region 委託契約書チェック
        /// <summary>
        /// 委託契約書チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckItakukeiyaku(DataRow dr)
        {
            var cell = (GcCustomTextBoxCell)this.mrHaisha.CurrentCell;
            var msgLogic = new MessageBoxShowLogic();
            try
            {
                M_SYS_INFO sysInfo = new M_SYS_INFO();
                IM_SYS_INFODao sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

                M_SYS_INFO[] sysInfos = sysInfoDao.GetAllData();
                if (sysInfos != null && sysInfos.Length > 0)
                {
                    sysInfo = sysInfos[0];
                }
                else
                {
                    return true;
                }

                var uketsuke = this.daoT_UKETSUKE_SS_ENTRY.GetData(this.dtoIdSeq);
                if (uketsuke == null)
                {
                    return true;
                }
                string gyoushaCd = uketsuke.GYOUSHA_CD;
                string genbaCd = uketsuke.GENBA_CD;

                if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(this.date))
                {
                    return true;
                }

                string TITLE_GYOUSHA = "業者";
                string TITLE_GENBA = "現場";
                string TITLE_DETAIL = "品名";

                //委託契約チェックDtoを取得
                ItakuCheckDTO checkDto = new ItakuCheckDTO();
                checkDto.MANIFEST_FLG = false;
                checkDto.GYOUSHA_CD = uketsuke.GYOUSHA_CD;
                checkDto.GENBA_CD = uketsuke.GENBA_CD;
                checkDto.SAGYOU_DATE = this.date;
                checkDto.LIST_HINMEI_HAIKISHURUI = new List<DetailDTO>();

                var ssArray = this.daoT_UKETSUKE_SS_DETAIL.GetData(this.dtoIdSeq);
                foreach (var item in ssArray)
                {
                    DetailDTO detailDto = new DetailDTO();
                    detailDto.CD = item.HINMEI_CD;
                    detailDto.NAME = item.HINMEI_NAME;
                    checkDto.LIST_HINMEI_HAIKISHURUI.Add(detailDto);
                }

                ItakuKeiyakuCheckLogic itakuLogic = new ItakuKeiyakuCheckLogic();
                bool isCheck = itakuLogic.IsCheckItakuKeiyaku(sysInfo, checkDto);
                //委託契約チェックを処理しない場合
                if (isCheck == false)
                {
                    return true;
                }

                //委託契約チェック
                ItakuErrorDTO error = itakuLogic.ItakuKeiyakuCheck(checkDto);

                //エラーなし
                if (error.ERROR_KBN == (short)ITAKU_ERROR_KBN.NONE)
                {
                    return true;
                }

                var messageUtil = new MessageUtility();
                string messageContent = string.Empty;

                switch (error.ERROR_KBN)
                {
                    case (short)ITAKU_ERROR_KBN.GYOUSHA://「業者 (委託契約書が未登録)」エラー
                        if (sysInfo.ITAKU_KEIYAKU_ALERT_AUTH == 1)
                        {
                            messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_CFM_ITAKU_NOT_FOUND).MESSAGE;
                        }
                        else
                        {
                            messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_ERR_ITAKU_NOT_FOUND).MESSAGE;
                        }
                        break;
                    case (short)ITAKU_ERROR_KBN.GENBA_BLANK://「委託契約の排出事業場≠BLANK, 画面の現場＝BLANK」エラー
                        if (sysInfo.ITAKU_KEIYAKU_ALERT_AUTH == 1)
                        {
                            messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_CFM_ITAKU_GENBA_BLANK).MESSAGE;
                            messageContent = String.Format(messageContent, TITLE_GYOUSHA, TITLE_GENBA);
                        }
                        else
                        {
                            messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_ERR_ITAKU_GENBA_BLANK).MESSAGE;
                            messageContent = String.Format(messageContent, TITLE_GYOUSHA, TITLE_GENBA);
                        }
                        break;
                    case (short)ITAKU_ERROR_KBN.GENBA_NOT_FOUND://「委託契約に未登録の排出事業場」エラー
                        if (sysInfo.ITAKU_KEIYAKU_ALERT_AUTH == 1)
                        {
                            messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_CFM_ITAKU_GENBA_NOT_FOUND).MESSAGE;
                            messageContent = String.Format(messageContent, TITLE_GYOUSHA);
                        }
                        else
                        {
                            messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_ERR_ITAKU_GENBA_NOT_FOUND).MESSAGE;
                            messageContent = String.Format(messageContent, TITLE_GYOUSHA);
                        }
                        break;
                    case (short)ITAKU_ERROR_KBN.YUUKOU_KIKAN://「有効期間」エラー
                        if (sysInfo.ITAKU_KEIYAKU_ALERT_AUTH == 1)
                        {
                            messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_CFM_ITAKU_YUUKOU_KIKAN).MESSAGE;
                        }
                        else
                        {
                            messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_ERR_ITAKU_YUUKOU_KIKAN).MESSAGE;
                        }
                        break;
                    case (short)ITAKU_ERROR_KBN.HOUKOKUSHO_BUNRUI://「報告書分類」エラー
                        if (sysInfo.ITAKU_KEIYAKU_ALERT_AUTH == 1)
                        {
                            messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_CFM_ITAKU_HOUKOKUSHO_BUNRUI).MESSAGE;
                        }
                        else
                        {
                            messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_ERR_ITAKU_HOUKOKUSHO_BUNRUI).MESSAGE;
                        }

                        //エラー明細を取得
                        StringBuilder showMsg = new StringBuilder();
                        int showCount = error.DETAIL_ERROR.Count;
                        if (showCount > ItakuKeiyakuCheckLogic.MAX_ERROR_SHOW)
                        {
                            showCount = ItakuKeiyakuCheckLogic.MAX_ERROR_SHOW;
                        }

                        for (int i = 0; i < showCount; i++)
                        {
                            showMsg.AppendFormat(ItakuKeiyakuCheckLogic.ERROR_APPEND_DETAIL, TITLE_DETAIL, error.DETAIL_ERROR[i].CD, error.DETAIL_ERROR[i].NAME);
                            showMsg.AppendLine();
                        }

                        if (error.DETAIL_ERROR.Count > ItakuKeiyakuCheckLogic.MAX_ERROR_SHOW)
                        {
                            showMsg.AppendFormat(ItakuKeiyakuCheckLogic.ERROR_APPEND_SUM_RECORD, error.DETAIL_ERROR.Count);
                            showMsg.AppendLine();
                        }

                        messageContent = String.Format(messageContent, TITLE_DETAIL, showMsg.ToString());
                        break;
                }

                //エラーを表示
                if (sysInfo.ITAKU_KEIYAKU_ALERT_AUTH == 1)
                {
                    if (msgLogic.MessageBoxShowConfirm(messageContent) != DialogResult.Yes)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    cell.AutoChangeBackColorEnabled = false;
                    cell.IsInputErrorOccured = true;
                    cell.Style.BackColor = Constans.ERROR_COLOR;
                    cell.Invalidate();

                    msgLogic.MessageBoxShowError(messageContent);
                    return false;
                }
            }
            catch (Seasar.Framework.Exceptions.SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckItakukeiyaku", ex2);
                msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckItakukeiyaku", ex);
                msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion
        // 20141107 Houkakou 委託契約チェック end

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        /// <summary>
        /// 運転者チェック
        /// </summary>
        internal bool CheckUntensha()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.UNTENSHA_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    // 運転者CDがなければ何もしない。
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                // 入力された運転者CDでマスタ検索
                this.dtoHaisha.ShainCd = this.form.UNTENSHA_CD.Text;
                var dt = this.dao_HAISHA.GetShain(this.dtoHaisha);
                if (dt.Rows.Count == 0)
                {
                    // エラーメッセージ（存在しない場合）
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "社員");
                    this.form.UNTENSHA_CD.Focus();
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                else
                {
                    var dr = dt.Rows[0];
                    if (!(Boolean)dr["UNTEN_KBN"])
                    {
                        // エラーメッセージ（運転者ではない場合）
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        msgLogic.MessageBoxShow("E020", "運転者");
                        this.form.UNTENSHA_CD.Focus();
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                    // 20141015 koukouei 休動管理機能 start
                    // 休動チェック
                    else if ((Boolean)dr["WORK_CLOSED_UNTENSHA"])
                    {
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        msgLogic.MessageBoxShow("E206", "運転者", string.Format("作業日：{0}", dtoHaisha.SagyouDate));
                        this.form.UNTENSHA_CD.Focus();
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                    // 20141015 koukouei 休動管理機能 end
                    else
                    {
                        this.form.UNTENSHA_NAME.Text = dr["SHAIN_NAME_RYAKU"].ToString();
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntensha", ex);
                msgLogic.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
    }
}
