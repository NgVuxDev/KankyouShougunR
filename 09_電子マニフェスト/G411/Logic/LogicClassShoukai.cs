using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Dao;
using Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Dto;

namespace Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class TuuchiJouhouShoukaiLogic
    {
        #region フィールド

        /// <summary>
        /// ボタンの設定用ファイルパス
        /// </summary>
        private readonly string ButtonInfoXmlPath =
            "Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Setting.ButtonSetting.xml";

        /// <summary>
        /// 通知情報照会画面
        /// </summary>
        private TuuchiJouhouShoukai form;

        /// <summary>
        /// システム情報DAO
        /// </summary>
        private GetSysInfoDaoCls GetSysInfoDao;

        /// <summary>
        /// 重要通知情報DAO
        /// </summary>
        private GetJyuuyouTsuuchiDaoCls GetJyuuyouTsuuchiDao;

        /// <summary>
        /// お知らせ情報DAO
        /// </summary>
        private GetOshiraseTsuuchiDaoCls GetOshiraseTsuuchiDao;

        /// <summary>
        /// 画面上に表示するメッセージボックスをメッセージIDから検索し表示する処理
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TuuchiJouhouShoukaiLogic(TuuchiJouhouShoukai targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            try
            {
                this.form = targetForm;

                this.GetSysInfoDao = DaoInitUtility.GetComponent<GetSysInfoDaoCls>();
                this.GetJyuuyouTsuuchiDao = DaoInitUtility.GetComponent<GetJyuuyouTsuuchiDaoCls>();
                this.GetOshiraseTsuuchiDao = DaoInitUtility.GetComponent<GetOshiraseTsuuchiDaoCls>();

                //ロジック初期化
                this.msgLogic = new MessageBoxShowLogic();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion

        #region 初期化

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //ボタンの初期化
                this.ButtonInit();

                //コントロール初期化
                this.ControlInit();

                //イベント初期化
                this.EventInit();

                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                msgLogic.MessageBoxShow("E080", "");
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コントロール初期化処理
        /// </summary>
        private void ControlInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.form.txt_Kakunin.Text = Const.ConstCls.KAKUNIN_DEFAULT;

                this.form.cRobtn_KakuninType1.Checked = true;
                this.form.cRobtn_KakuninType2.Checked = false;

                DataTable dtTsuuti = this.GetSysInfo();

                //通知日クリア
                this.form.cDtPicker_TuuchiBiFrom.Clear();
                this.form.cDtPicker_TuuchiBiTo.Clear();

                //初期値を設定する
                if (dtTsuuti.Rows.Count > 0)
                {
                    this.form.cDtPicker_TuuchiBiFrom.Text = dtTsuuti.Rows[0][0].ToString();
                    this.form.cDtPicker_TuuchiBiTo.Text = dtTsuuti.Rows[0][1].ToString();
                }

                //編集不可を設定する
                BusinessBaseForm parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.txb_process.Enabled = false;
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
        /// 画面クリア
        /// </summary>
        public void ClearScreen()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.form.txt_Kakunin.Text = string.Empty;
                this.form.cRobtn_KakuninType1.Checked = false;
                this.form.cRobtn_KakuninType2.Checked = false;
                this.form.cDtPicker_TuuchiBiFrom.Text = string.Empty;
                this.form.cDtPicker_TuuchiBiTo.Text = string.Empty;
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

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            ButtonSetting[] loadButtonSetting = new ButtonSetting[0];

            try
            {
                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                loadButtonSetting = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            LogUtility.DebugMethodEnd();
            return loadButtonSetting;
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;

                //検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.Kensaku);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                // 20141023 Houkakou 「通知照会」のダブルクリックを追加する start
                // 「To」のイベント生成
                this.form.cDtPicker_TuuchiBiTo.MouseDoubleClick += new MouseEventHandler(cDtPicker_TuuchiBiTo_MouseDoubleClick);
                // 20141023 Houkakou 「通知照会」のダブルクリックを追加する end
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

        #region 通知情報明細画面表示

        /// <summary>
        /// 通知情報明細画面表示
        /// </summary>
        /// <param name="tuuchiBiFrom">通知日From</param>
        /// <param name="tuuchiBiTo">通知日To</param>
        /// <param name="readFlag">確認</param>
        /// <param name="tuuchiCd">通知コード</param>
        public void TsuuchiJouhouMeisaiShow(string tuuchiBiFrom, string tuuchiBiTo, string readFlag, string tuuchiCd)
        {
            LogUtility.DebugMethodStart(tuuchiBiFrom, tuuchiBiTo, readFlag, tuuchiCd);

            try
            {
                var callForm = new Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.TuuchiJouhouMeisai(
                        tuuchiBiFrom, tuuchiBiTo, readFlag, tuuchiCd);

                var callHeader = new Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.UIHeaderMeisai();

                var businessForm = new BusinessBaseForm(callForm, callHeader);

                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);

                if (!isExistForm)
                {
                    businessForm.ShowDialog();
                }

                //再検索条件を設定する
                string kensakuTchiBiFrom = "";
                if (!string.IsNullOrEmpty(this.form.cDtPicker_TuuchiBiFrom.Text.Trim()))
                {
                    kensakuTchiBiFrom = this.form.cDtPicker_TuuchiBiFrom.Text.Substring(0, 10).ToString().Replace("/", "");
                }
                string kensakuTuuchiBiTo = "";
                if (!string.IsNullOrEmpty(this.form.cDtPicker_TuuchiBiTo.Text.Trim()))
                {
                    kensakuTuuchiBiTo = this.form.cDtPicker_TuuchiBiTo.Text.Substring(0, 10).ToString().Replace("/", "");
                }
                string kensakuReadFlag = this.form.txt_Kakunin.Text;

                //再検索処理
                this.Kensaku(kensakuTchiBiFrom, kensakuTuuchiBiTo, kensakuReadFlag);
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("TsuuchiJouhouMeisaiShow", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("TsuuchiJouhouMeisaiShow", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsuuchiJouhouMeisaiShow", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(tuuchiBiFrom, tuuchiBiTo, readFlag, tuuchiCd);
            }
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="tuutiBiFrom">通知日開始</param>
        /// <param name="tuutiBiTo">通知日終了</param>
        /// <param name="kakuninKbn">確認区分</param>
        public bool Kensaku(string tuutiBiFrom, string tuutiBiTo, string kakuninKbn)
        {
            LogUtility.DebugMethodStart(tuutiBiFrom, tuutiBiTo, kakuninKbn);
            DataTable dtInfo = new DataTable();

            try
            {
                //明細クリア
                this.form.cdgv_Jyuuyou.Rows.Clear();
                this.form.cdgv_Oshirase.Rows.Clear();

                //検索条件
                TsuuchiJyouhouDTOCls serch = new TsuuchiJyouhouDTOCls();
                serch.tuuchiBiFrom = tuutiBiFrom;
                serch.tuuchiBiTo = tuutiBiTo;
                if (kakuninKbn.Equals(Const.ConstCls.KAKUNIN_DEFAULT))
                    serch.readFlag = string.Empty;
                else
                    serch.readFlag = kakuninKbn;

                //重要な通知一覧
                dtInfo = this.GetJyuuyouTsuuchiDao.GetDataForEntity(serch);

                if (dtInfo != null && dtInfo.Rows.Count > 0)
                {
                    SetTsuuchiData(dtInfo, this.form.cdgv_Jyuuyou);
                }

                //お知らせ通知一覧
                dtInfo = new DataTable();
                dtInfo = this.GetOshiraseTsuuchiDao.GetDataForEntity(serch);

                if (dtInfo != null && dtInfo.Rows.Count > 0)
                {
                    SetTsuuchiData(dtInfo, this.form.cdgv_Oshirase);
                }

                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Kensaku", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Kensaku", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Kensaku", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(tuutiBiFrom, tuutiBiTo, kakuninKbn);
            }
        }

        /// <summary>
        /// データを設定する
        /// </summary>
        /// <param name="dt">データテーブル</param>
        /// <param name="dataGridView">データグッリド</param>
        public void SetTsuuchiData(DataTable dt, CustomDataGridView dataGridView)
        {
            LogUtility.DebugMethodStart(dt, dataGridView);

            int index = dt.Rows.Count;

            try
            {
                dataGridView.IsBrowsePurpose = false;
                for (int i = 0; i < index; i++)
                {
                    dataGridView.Rows.Add();

                    //通知コード
                    dataGridView.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();

                    //通知の種類
                    dataGridView.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();

                    //件数
                    dataGridView.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                }

                dataGridView.FirstDisplayedScrollingRowIndex = 0;
                dataGridView.FirstDisplayedScrollingColumnIndex = 0;
                dataGridView.IsBrowsePurpose = true;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            LogUtility.DebugMethodEnd(dt, dataGridView);
        }

        #endregion

        // 20141021 Houkakou 「通知照会」の日付チェックを追加する start

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
                this.form.cDtPicker_TuuchiBiFrom.BackColor = Constans.NOMAL_COLOR;
                this.form.cDtPicker_TuuchiBiTo.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.cDtPicker_TuuchiBiFrom.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.cDtPicker_TuuchiBiTo.Text))
                {
                    return false;
                }

                DateTime date_from = DateTime.Parse(this.form.cDtPicker_TuuchiBiFrom.Text);
                DateTime date_to = DateTime.Parse(this.form.cDtPicker_TuuchiBiTo.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.cDtPicker_TuuchiBiFrom.IsInputErrorOccured = true;
                    this.form.cDtPicker_TuuchiBiTo.IsInputErrorOccured = true;
                    this.form.cDtPicker_TuuchiBiFrom.BackColor = Constans.ERROR_COLOR;
                    this.form.cDtPicker_TuuchiBiTo.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "通知日From", "通知日To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.cDtPicker_TuuchiBiFrom.Focus();
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

        // 20141021 Houkakou 「通知照会」の日付チェックを追加する end

        // 20141128 Houkakou 「通知照会」のダブルクリックを追加する start

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cDtPicker_TuuchiBiTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cDtPicker_TuuchiBiFrom;
            var ToTextBox = this.form.cDtPicker_TuuchiBiTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        // 20141128 Houkakou 「通知照会」のダブルクリックを追加する end
    }
}