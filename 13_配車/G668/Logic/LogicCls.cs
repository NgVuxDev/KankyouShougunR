// $Id: LogicCls.cs 54491 2015-07-03 03:56:01Z quocthang@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using System.Data.SqlTypes;
using System.Drawing;
using Shougun.Core.Common.BusinessCommon.Logic;

namespace Shougun.Core.Allocation.MobileJoukyouInfo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.MobileJoukyouInfo.Setting.ButtonSetting.xml";

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// システム設定． 数量フォーマット
        /// </summary>
        private String systemSuuryouFormatCD;

        /// <summary>共通</summary>
        ManifestoLogic mlogic = null;

        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private UIHeader headerForm;
        /// <summary>
        /// ribbon
        /// </summary>
        private RibbonMainMenu ribbon;

        /// <summary>
        /// (配車)伝票番号
        /// </summary>
        internal SqlInt64 haishaDenpyouNo { get; set; }

        /// <summary>
        /// (配車)配車区分
        /// </summary>
        internal SqlInt32 haishaKbn { get; set; }

        /// <summary>
        /// 画面上に表示するメッセージボックス
        /// </summary>
        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// ITeikihaishaDao
        /// </summary>
        private ITeikihaishaDao haishaDao;

        /// <summary>
        /// 回収品名情報
        /// </summary>
        private DataTable hinmeiInfo { get; set; }

        /// <summary>
        /// 搬入情報
        /// </summary>
        private DataTable hannyuInfo { get; set; }

        /// <summary>
        /// コンテナシーケンス番号
        /// </summary>
        private DataTable contenaSEQ { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
                this.form = targetForm;
                this.haishaDao = DaoInitUtility.GetComponent<ITeikihaishaDao>();
                this.MsgBox = new MessageBoxShowLogic();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit(WINDOW_TYPE windowType)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォーム
                this.parentForm = this.form.Parent as BusinessBaseForm;
                this.parentForm.ProcessButtonPanel.Visible = false;

                // HeaderFormのSet
                this.headerForm = (UIHeader)this.parentForm.headerForm;

                // RibbonMenuのSet
                this.ribbon = (RibbonMainMenu)this.parentForm.ribbonForm;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // システム情報を取得
                this.GetSysInfoInit();

                // リボンメニュー非表示
                this.ribbonHide();

                // モバイル状況詳細データの取得
                this.GetMobileInfo();

                // 処理モード別画面初期化
                this.ModeInit(windowType, parentForm);
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
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="parentForm"></param>
        public void ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
            bool catchErr = false;
            switch (windowType)
            {
                // 【参照】モード
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                default:
                    catchErr = this.WindowInitReference(parentForm);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    break;
            }
        }

        /// <summary>
        /// 画面項目初期化【参照】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitReference(BusinessBaseForm parentForm)
        {
            try
            {
                // 検索結果を画面に設定
                this.SetWindowData();

                // 削除モード固有UI設定
                this.AllControlLock(false);

                // functionボタン
                parentForm.bt_func3.Enabled = false;     // 修正
                parentForm.bt_func9.Enabled = false;     // 登録
                parentForm.bt_func11.Enabled = false;    // 取消
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitReference", ex2);
                this.MsgBox.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInitReference", ex);
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // コンテナ
                parentForm.bt_func6.Click -= new System.EventHandler(this.bt_func6_Click);
                parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);

                // 閉じる
                parentForm.bt_func12.Click -= new System.EventHandler(this.bt_func12_Click);
                parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
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
                LogUtility.Error("ButtonSetting", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region システム情報を取得
        /// <summary>
        ///  システム情報を取得
        /// </summary>
        internal void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                    // 数量フォーマット
                    this.systemSuuryouFormatCD = this.ChgDBNullToValue(sysInfoEntity.SYS_SUURYOU_FORMAT_CD, string.Empty).ToString();
                }
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
        #endregion

        #region リボンメニュー非表示
        /// <summary>
        /// リボンメニュー非表示
        /// </summary>
        private void ribbonHide()
        {
            // リボン非表示
            this.ribbon.Visible = false;

            // 非表示にした分、各Formを調整
            this.headerForm.Location = new Point(this.headerForm.Location.X, this.headerForm.Location.Y - this.ribbon.Height);
            this.form.Location = new Point(this.form.Location.X, this.form.Location.Y - this.ribbon.Height);
            this.parentForm.Size = new Size(this.parentForm.Size.Width, this.parentForm.Size.Height - this.ribbon.Height);
            this.form.Size = new Size(this.form.Size.Width, this.form.Size.Height + 100);
            this.parentForm.StartPosition = FormStartPosition.CenterParent;
        }
        #endregion

        #region F6 コンテナ
        /// <summary>
        /// F6 コンテナ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var HeaderForm = new UIHeader();
                var callForm = new ContenaForm(Convert.ToString(this.contenaSEQ.Rows[0]["SEQ_NO"]));
                var form = new BusinessBaseForm(callForm, HeaderForm);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func6_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region F12 閉じる処理
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
                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region 全コントロール制御メソッド

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作可、false:操作不可</param>
        private void AllControlLock(bool isBool)
        {
            this.form.HAISHA_KBN.ReadOnly = !isBool;
            this.form.HAISHA_KBN_1.Enabled = isBool;
            this.form.HAISHA_KBN_2.Enabled = isBool;
            this.form.SAGYOU_DATE.ReadOnly = !isBool;
            this.form.COURSE_CD.ReadOnly = !isBool;
            this.form.UNPAN_GYOUSHA_CD.ReadOnly = !isBool;
            this.form.SHARYOU_CD.ReadOnly = !isBool;
            this.form.SHASHU_CD.ReadOnly = !isBool;
            this.form.SHAIN_CD.ReadOnly = !isBool;
        }

        #endregion 全コントロール制御メソッド

        #region モバイル状況詳細データの取得と画面設定

        /// <summary>
        /// モバイル状況詳細データの取得
        /// </summary>
        private void GetMobileInfo()
        {
            this.hinmeiInfo = this.haishaDao.GetHinmeiDetail(this.haishaDenpyouNo, this.haishaKbn);
            this.hannyuInfo = this.haishaDao.GetHannyuuDetail(this.haishaDenpyouNo, this.haishaKbn);
            this.contenaSEQ = this.haishaDao.GetContenaDetailSEQ(this.haishaDenpyouNo, this.haishaKbn);
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            if (this.hinmeiInfo == null || this.hinmeiInfo.Rows.Count <= 0) { return; }

            // 配車区分
            switch ((int)this.haishaKbn)
            {
                case 0:
                    this.form.HAISHA_KBN.Text = "1";
                    // コンテナ
                    parentForm.bt_func6.Enabled = false;
                    parentForm.bt_func6.Text = string.Empty;
                    break;
                case 1:
                    this.form.HAISHA_KBN.Text = "2";
                    break;
                default:
                    this.form.HAISHA_KBN.Text = "1";
                    // コンテナ
                    parentForm.bt_func6.Enabled = false;
                    parentForm.bt_func6.Text = string.Empty;
                    break;
            }
            // 作業日
            this.form.SAGYOU_DATE.Text = Convert.ToString(this.hinmeiInfo.Rows[0]["HAISHA_SAGYOU_DATE"]);
            // コース
            this.form.COURSE_CD.Text = Convert.ToString(this.hinmeiInfo.Rows[0]["HAISHA_COURSE_NAME_CD"]);
            this.form.COURSE_NAME.Text = Convert.ToString(this.hinmeiInfo.Rows[0]["HAISHA_COURSE_NAME"]);
            // 運搬業者
            this.form.UNPAN_GYOUSHA_CD.Text = Convert.ToString(this.hinmeiInfo.Rows[0]["GENBA_JISSEKI_UPNGYOSHACD"]);
            this.form.UNPAN_GYOUSHA_NAME.Text = Convert.ToString(this.hinmeiInfo.Rows[0]["GENBA_JISSEKI_UPNGYOSHA_NAME"]);
            // 車輌
            this.form.SHARYOU_CD.Text = Convert.ToString(this.hinmeiInfo.Rows[0]["SHARYOU_CD"]);
            this.form.SHARYOU_NAME.Text = Convert.ToString(this.hinmeiInfo.Rows[0]["SHARYOU_NAME"]);
            // 車種
            this.form.SHASHU_CD.Text = Convert.ToString(this.hinmeiInfo.Rows[0]["SHASHU_CD"]);
            this.form.SHASHU_NAME.Text = Convert.ToString(this.hinmeiInfo.Rows[0]["SHASHU_NAME"]);
            // 運転者
            this.form.SHAIN_CD.Text = Convert.ToString(this.hinmeiInfo.Rows[0]["UNTENSHA_CD"]);
            this.form.SHAIN_NAME.Text = Convert.ToString(this.hinmeiInfo.Rows[0]["UNTENSHA_NAME"]);

            // 回収品名情報
            this.form.Ichiran.DataSource = null;

            if (this.hinmeiInfo.Rows.Count > 0)
            {
                foreach (DataRow row in this.hinmeiInfo.Rows)
                {
                    if (!string.IsNullOrEmpty(row["GENBA_DETAIL_SUURYO2"].ToString()))
                    {
                        // 換算数量
                        row["GENBA_DETAIL_SUURYO2"] = mlogic.GetSuuryoRound(decimal.Parse(row["GENBA_DETAIL_SUURYO2"].ToString()), this.systemSuuryouFormatCD);
                    }
                }
            }


            this.form.Ichiran.DataSource = this.hinmeiInfo;


            // 搬入情報
            this.form.hannyuuDetail.DataSource = null;
            this.form.hannyuuDetail.DataSource = this.hannyuInfo;

            // 初期表示時に自動調整をし、その後は手での明細幅変更をできるようにする。
            this.form.Ichiran.HorizontalAutoSizeMode = ((GrapeCity.Win.MultiRow.HorizontalAutoSizeMode)((GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.CellsInColumnHeader | GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.DisplayedCellsInRow)));
            this.form.Ichiran.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
            this.form.hannyuuDetail.HorizontalAutoSizeMode = ((GrapeCity.Win.MultiRow.HorizontalAutoSizeMode)((GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.CellsInColumnHeader | GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.DisplayedCellsInRow)));
            this.form.hannyuuDetail.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
        }

        #endregion

        #region DBNull値を指定値に変換
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>
        /// <param name="value">変化値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else
            {
                return obj;
            }
        }
        #endregion

        #region 自動生成（実装なし）
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

        public int Search()
        {
            return 0;
        }
        #endregion
    }
}