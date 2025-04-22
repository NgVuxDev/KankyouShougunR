using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;

namespace Shougun.Core.ElectronicManifest.TuusinnRirekiShoukai
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ElectronicManifest.TuusinnRirekiShoukai.Setting.ButtonSetting.xml";

        /// <summary>マニフェスト紐付画面のHeader</summary>
        public UIHeader headerform { get; set; }

        /// <summary> 親フォーム</summary>
        public BusinessBaseForm parentbaseform { get; set; }

        /// <summary>
        /// DTO
        /// </summary>
        public MeisaiInfoDTOCls dto;

        /// <summary>
        /// 紐付したい対象検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 通信履歴検索処理用Dao
        /// </summary>
        private GetMeisaiInfoDaoCls MeisaiInfoDao;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 画面上に表示するメッセージボックスをメッセージIDから検索し表示する処理
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        /// システム情報DAO
        /// </summary>
        private GetSysInfoDaoCls GetSysInfoDao;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new MeisaiInfoDTOCls();
            this.MeisaiInfoDao = DaoInitUtility.GetComponent<GetMeisaiInfoDaoCls>();
            this.GetSysInfoDao = DaoInitUtility.GetComponent<GetSysInfoDaoCls>();

            //ロジック初期化
            this.msgLogic = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        #region ボタン初期化処理

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();

            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // フォームインスタンスを取得
                this.parentbaseform = (BusinessBaseForm)this.form.Parent;
                this.headerform = (UIHeader)parentbaseform.headerForm;
                //コントロール初期化
                this.ControlInit();
                // ボタンを初期化
                this.ButtonInit();
                //footボタン処理イベントを初期化
                EventInit();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
                ret = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
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

        /// <summary>
        /// コントロール初期化処理
        /// </summary>
        private void ControlInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // 状態の初期化
                this.form.cantxt_Jyoutai.Text = "1";
                //編集不可を設定する
                BusinessBaseForm parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.txb_process.Enabled = false;

                //20151015 hoanghm #11853 start
                this.form.txt_Hidzuke.Text = "2";
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.form.txt_Hidzuke_From.Value = DateTime.Now;
                //this.form.txt_Hidzuke_To.Value = DateTime.Now;
                //this.form.txt_Hidzuke_From.Value = this.parentbaseform.sysDate;
                //this.form.txt_Hidzuke_To.Value = this.parentbaseform.sysDate;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                //20151015 hoanghm #11853 end

                DataTable dtTsuuti = this.GetSysInfo();

                //通知日クリア
                this.form.txt_Hidzuke_From.Clear();
                this.form.txt_Hidzuke_To.Clear();

                //初期値を設定する
                if (dtTsuuti.Rows.Count > 0)
                {
                    this.form.txt_Hidzuke_From.Text = dtTsuuti.Rows[0][0].ToString();
                    this.form.txt_Hidzuke_To.Text = dtTsuuti.Rows[0][1].ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;

                // ﾒｯｾｰｼﾞ詳細(F5)イベント生成
                parentform.bt_func5.Click += new EventHandler(this.form.ShowJwnetErrorMessagePopup);

                //検索ボタン(F8)イベント生成
                parentform.bt_func8.Click += new EventHandler(this.form.Search);
                //閉じるボタン(F12)イベント生成
                parentform.bt_func12.Click += new EventHandler(this.form.FormClose);

                // 20141023 Houkakou 「通信照会」のダブルクリックを追加する　start
                // 「To」のイベント生成
                this.form.cantxt_ManiFestTo.MouseDoubleClick += new MouseEventHandler(cantxt_ManiFestTo_MouseDoubleClick);
                // 20141023 Houkakou 「通信照会」のダブルクリックを追加する　end
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入力制限処理
        /// </summary>
        /// <param name="e">キープレスイベント</param>
        internal void InputLimit(ref KeyPressEventArgs e)
        {
            if (0 <= Array.IndexOf(Constans.ALLOW_KEY_CHARS_NUMERIC, e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 検索ボタンクリック後ロジック処理
        /// </summary>
        public virtual int SearchLogic()
        {
            LogUtility.DebugMethodStart();
            int ret = 0;
            try
            {
                //検索結果テーブル
                this.SearchResult = new DataTable();
                //検索実行
                this.SearchResult = MeisaiInfoDao.GetDataForEntity(dto);

                //行数の戻る
                ret = this.SearchResult.Rows.Count;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("SearchLogic", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
                ret = -1;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchLogic", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchLogic", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #region 電子マニフェスト入力画面へ遷移する

        /// <summary>
        /// 電子マニフェスト入力画面の参照モードで開く
        /// </summary>
        public void ElectronicManifestNyuryokuShow(string kanriId)
        {
            LogUtility.DebugMethodStart(kanriId);

            try
            {
                //処理モード=4「参照モード」を設定する
                //2013.12.18 naitou upd start
                //var callForm = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIForm(WINDOW_TYPE.REFERENCE_WINDOW_FLAG, kanriId, seq);
                //var callHeader = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIHeader();
                //var businessForm = new BusinessBaseForm(callForm, callHeader);
                //var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                //if (!isExistForm)
                //{
                //    businessForm.Show();
                //}

                FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, kanriId);
                //2013.12.18 naitou upd end
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ElectronicManifestNyuryokuShow", ex1);
                msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("ElectronicManifestNyuryokuShow", ex);
                msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 実現必須メソッド

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

        #endregion

        // 20141128 Houkakou 「通信照会」のダブルクリックを追加する　start

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ManiFestTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cantxt_ManiFestFrom;
            var ToTextBox = this.form.cantxt_ManiFestTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        // 20141128 Houkakou 「通信照会」のダブルクリックを追加する　end

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck(out bool catchErr)
        {
            catchErr = false;
            try
            {
                this.form.txt_Hidzuke_From.IsInputErrorOccured = false;
                this.form.txt_Hidzuke_To.IsInputErrorOccured = false;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.txt_Hidzuke_From.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.txt_Hidzuke_To.Text))
                {
                    return false;
                }

                DateTime date_from = DateTime.Parse(this.form.txt_Hidzuke_From.Text);
                DateTime date_to = DateTime.Parse(this.form.txt_Hidzuke_To.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.txt_Hidzuke_From.IsInputErrorOccured = true;
                    this.form.txt_Hidzuke_To.IsInputErrorOccured = true;
                    this.form.txt_Hidzuke_From.IsInputErrorOccured = true;
                    this.form.txt_Hidzuke_To.IsInputErrorOccured = true;
                    string[] errorMsg = { "日付From", "日付To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.txt_Hidzuke_From.Focus();
                    return true;
                }

                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        #endregion

        /// <summary>
        /// 通知日取得
        /// </summary>
        [Transaction]
        public virtual DataTable GetSysInfo()
        {
            LogUtility.DebugMethodStart();

            DataTable dt = new DataTable();

            try
            {
                M_SYS_INFO data = new M_SYS_INFO();

                dt = this.GetSysInfoDao.GetDataForEntity(data);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd();

            return dt;
        }
    }
}