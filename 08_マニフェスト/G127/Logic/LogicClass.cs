using System;
using System.Windows.Forms;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.APP.Base;
using r_framework.Setting;
using System.Reflection;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Const;

namespace Shougun.Core.PaperManifest.Manifestmeisaihyo
{
    /// <summary>
    /// マニフェスト明細表出力画面ロジッククラス
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタン設定XMLファイルパス
        /// </summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.PaperManifest.Manifestmeisaihyo.Setting.ButtonSetting.xml";

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private HeaderBaseForm header;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm form;

        /// <summary>
        /// マニフェスト明細表Dtoを取得・設定します
        /// </summary>
        public ManifestMeisaihyouDto ManifestMeisaihyouDto { get; set; }

        /// <summary>
        /// マニフェストデータテーブルを取得・設定します
        /// </summary>
        public DataTable ManifestDataTable { get; set; }

        private MessageBoxShowLogic MsgBox { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">画面クラス</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダーを初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
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
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            HeaderBaseForm targetHeader = (HeaderBaseForm)parentForm.headerForm;
            this.header = targetHeader;
            this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 印刷ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

            // CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.ButtonFunc6_Clicked);

            // 検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.ButtonFunc8_Clicked);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細表作成
        /// </summary>
        internal void CreateMeisaihyou()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.ManifestDataTable != null && 0 < this.ManifestDataTable.Rows.Count)
                {
                    var reportLogic = new ManifestMeisaihyouReportClass();
                    reportLogic.CreateReport(this.ManifestDataTable, this.ManifestMeisaihyouDto);
                }
                else
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMeisaihyou", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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

        #region IBuisinessLogic メンバー

        public int Search()
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

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion IBuisinessLogic メンバー
    }

}
