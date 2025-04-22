// $Id: LogicCls.cs 44428 2015-03-12 08:08:05Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Seasar.Dao;
using r_framework.Configuration;

namespace Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// コース配車依頼入力Dao
        /// </summary>
        private DAOCls dao;

        private PopupDAOCls popupDao;

        /// <summary>
        /// コース名称Dao
        /// </summary>
        private IM_COURSE_NAMEDao courseNameDao;

        /// <summary>
        /// システム情報Dao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 拠点情報Dao
        /// </summary>
        private IM_KYOTENDao kyoutenDao;

        /// <summary>
        /// 定期配車情報Dao
        /// </summary>
        private IT_TEIKI_HAISHA_ENTRYDao teikiHaishaDao;

        /// <summary>
        /// 定期配車荷降情報Dao
        /// </summary>
        private IT_TEIKI_HAISHA_NIOROSHIDao teikiHaishaNioroshiDao;

        /// <summary>
        /// 定期配車明細情報Dao
        /// </summary>
        private IT_TEIKI_HAISHA_DETAILDao teikiHaishaDetailDao;

        /// <summary>
        /// 定期配車詳細情報Dao
        /// </summary>
        private IT_TEIKI_HAISHA_SHOUSAIDao teikiHaishaShousaiDao;

        /// <summary>
        /// 受付（収集）入力のDao
        /// </summary>
        private T_UKETSUKE_SS_ENTRYDao daoUketsukeSSEntry;

        /// <summary>
        /// 受付（収集）明細のDao
        /// </summary>
        private T_UKETSUKE_SS_DETAILDao daoUketsukeSSDetail;

        /// <summary>
        /// 受付（出荷）入力のDao
        /// </summary>
        private T_UKETSUKE_SK_ENTRYDao daoUketsukeSKEntry;

        /// <summary>
        /// 受付（出荷）明細のDao
        /// </summary>
        private T_UKETSUKE_SK_DETAILDao daoUketsukeSKDetail;

        /// <summary>
        /// コンテナ稼働予定のDao
        /// </summary>
        private T_CONTENA_RESERVEDao daoContenaReserver;

        /// <summary>
        /// 現場定期品名のDao
        /// </summary>
        private IM_GENBA_TEIKI_HINMEIDao daoGenbaTeikiHinmei;

        /// <summary>
        /// システム情報エンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// IM_KYOTENDao(拠点Dao)
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// 20140930 Houkakou 「コース配車依頼入力」の休動を追加する　start
        /// <summary>
        /// 車輌休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_SHARYOUDao workclosedsharyouDao;

        /// <summary>
        /// 運転者休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_UNTENSHADao workcloseduntenshaDao;
        /// 20140930 Houkakou 「コース配車依頼入力」の休動を追加する　end

        /// <summary>
        /// DTO
        /// </summary>
        private DTOCls dto;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 配車番号に変更のあった行を格納します
        /// </summary>
        internal List<DataGridViewRow> haishaNumberChangedRowList = new List<DataGridViewRow>();

        /// <summary>
        /// モバイル連携用データテーブル
        /// </summary>
        private DataTable ResultTable;

        private int MobileTryTime;

        /// <summary>
        /// モバイル連携用の伝票番号
        /// </summary>
        internal string Renkei_TeikiDetailSystemId;

        /// <summary>
        /// モバイル将軍業務TBLのentity
        /// </summary>
        private T_MOBISYO_RT entitysMobisyoRt { get; set; }

        /// <summary>
        /// モバイル将軍業務詳細TBLのentity
        /// </summary>
        private T_MOBISYO_RT_DTL entitysMobisyoRtDTL { get; set; }

        /// <summary>
        /// モバイル将軍業務搬入TBLのentity
        /// </summary>
        private T_MOBISYO_RT_HANNYUU entitysMobisyoRtHN { get; set; }

        /// <summary>
        /// モバイル将軍業務TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT> entitysMobisyoRtList { get; set; }

        /// <summary>
        /// モバイル将軍業務詳細TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT_DTL> entitysMobisyoRtDTLList { get; set; }

        /// <summary>
        /// モバイル将軍業務搬入TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT_HANNYUU> entitysMobisyoRtHNList { get; set; }

        /// <summary>
        /// モバイル将軍業務TBLのDao
        /// </summary>
        private IT_MOBISYO_RTDao TmobisyoRtDao;

        /// <summary>
        /// モバイル将軍業務詳細TBLのDao
        /// </summary>
        private IT_MOBISYO_RT_DTLDao TmobisyoRtDTLDao;

        /// <summary>
        /// モバイル将軍業務搬入TBLのDao
        /// </summary>
        private IT_MOBISYO_RT_HANNYUUDao TmobisyoRtHNDao;

        /// <summary>
        /// 配車番号の前回値を保持します
        /// </summary>
        internal string beforeHaishaNumber = string.Empty;
        internal string beforeCd = string.Empty;
        internal bool isInputError = false;

        /// <summary>
        /// チェックボックスのスペースキー対応用
        /// </summary>
        internal bool SpaceChk = false;
        internal bool SpaceON = false;

        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        internal bool is_mobile = false;

        #endregion

        #region プロパティ
        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOCls SearchString { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOCls();
            this.dao = DaoInitUtility.GetComponent<DAOCls>();
            this.popupDao = DaoInitUtility.GetComponent<PopupDAOCls>();
            this.courseNameDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_COURSE_NAMEDao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.kyoutenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.teikiHaishaDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_ENTRYDao>();
            this.teikiHaishaNioroshiDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_NIOROSHIDao>();
            this.teikiHaishaDetailDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_DETAILDao>();
            this.teikiHaishaShousaiDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_SHOUSAIDao>();
            this.daoUketsukeSSEntry = DaoInitUtility.GetComponent<T_UKETSUKE_SS_ENTRYDao>();
            this.daoUketsukeSSDetail = DaoInitUtility.GetComponent<T_UKETSUKE_SS_DETAILDao>();
            this.daoUketsukeSKEntry = DaoInitUtility.GetComponent<T_UKETSUKE_SK_ENTRYDao>();
            this.daoUketsukeSKDetail = DaoInitUtility.GetComponent<T_UKETSUKE_SK_DETAILDao>();
            this.daoContenaReserver = DaoInitUtility.GetComponent<T_CONTENA_RESERVEDao>();
            this.daoGenbaTeikiHinmei = DaoInitUtility.GetComponent<IM_GENBA_TEIKI_HINMEIDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            /// 20140930 Houkakou 「コース配車依頼入力」の休動を追加する　start
            this.workclosedsharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();
            this.workcloseduntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();
            /// 20140930 Houkakou 「コース配車依頼入力」の休動を追加する　end
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            //モバイル連携
            this.TmobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();
            this.TmobisyoRtDTLDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_DTLDao>();
            this.TmobisyoRtHNDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_HANNYUUDao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 拠点初期値設定
                this.SetInitKyoten();

                this.allControl = this.form.allControl;

                // システム情報を取得し、初期値をセットする
                this.GetSysInfoInit();

                // 拠点情報初期値セット
                //this.SetKyoutenInfoInit();

                // 作業日初期値設定
                this.form.SAGYOU_DATE_BEGIN.Value = null;
                this.form.SAGYOU_DATE_END.Value = null;

                // 組込状態初期値設定
                this.form.radbtnMikumikomi.Checked = true;
                this.form.Ichiran.AutoGenerateColumns = false;

                //ﾓﾊﾞｲﾙ連携設定
                this.form.checkBoxAll.Visible = false;
                is_mobile = r_framework.Configuration.AppConfig.AppOptions.IsMobile();
                if (is_mobile)
                {
                    this.form.Ichiran.Columns[0].Visible = true;
                }
                else
                {
                    this.form.Ichiran.Columns[0].Visible = false;
                }
                    
                // サブファンクション非表示
                this.parentForm.txb_process.Enabled = false;

                // オプション非対応
                if (!AppConfig.AppOptions.IsMAPBOX())
                {
                    // mapbox用ボタン無効化
                    parentForm.bt_process1.Text = string.Empty;
                    parentForm.bt_process1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }
        #endregion

        #region 拠点設定
        /// <summary>
        /// 拠点初期値設定
        /// </summary>
        private void SetInitKyoten()
        {
            LogUtility.DebugMethodStart();

            // 拠点
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            this.form.HEADER_KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, "拠点CD");
            if (!string.IsNullOrEmpty(this.form.HEADER_KYOTEN_CD.Text.ToString()))
            {
                this.form.HEADER_KYOTEN_CD.Text = this.form.HEADER_KYOTEN_CD.Text.ToString().PadLeft(this.form.HEADER_KYOTEN_CD.MaxLength, '0');
                this.CheckKyotenCd();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ユーザー定義情報取得処理
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile">ＸＭＬファイルにアクセスするためのクラス</param>
        /// <param name="key">キー</param>
        /// <returns>キーに紐づく値</returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }
        #endregion

        #region ヘッダーの拠点CDの存在チェック
        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.HEADER_KYOTEN_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.HEADER_KYOTEN_CD.Text))
                {
                    this.form.HEADER_KYOTEN_NAME.Text = string.Empty;
                    return;
                }

                short kyoteCd = -1;
                if (!short.TryParse(this.form.HEADER_KYOTEN_CD.Text, out kyoteCd))
                {
                    return;
                }

                M_KYOTEN keyEntity = new M_KYOTEN();
                keyEntity.KYOTEN_CD = kyoteCd;
                var kyotens = this.kyotenDao.GetAllValidData(keyEntity);

                // 存在チェック
                if (kyotens == null || kyotens.Length < 1)
                {
                    //MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //msgLogic.MessageBoxShow("E020", "拠点");
                    //this.headerForm.KYOTEN_CD.Focus();
                    return;
                }
                else
                {
                    // キーが１つなので複数はヒットしないはず
                    M_KYOTEN kyoten = kyotens[0];
                    this.form.HEADER_KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
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



        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            //詳細ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(bt_func1_Click);

            //検索ボタン(F8)イベント生成
            this.form.C_Regist(parentForm.bt_func8);
            parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);

            //登録ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

            //地図表示ボタン(subF1)イベント生成
            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);

            // 20141128 teikyou ダブルクリックを追加する　start
            this.form.SAGYOU_DATE_END.MouseDoubleClick += new MouseEventHandler(SAGYOU_DATE_END_MouseDoubleClick);
            // 20141128 teikyou ダブルクリックを追加する　end
        }
        #endregion

        #region システム情報を取得し、初期値をセットする
        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }
        #endregion

        #region 拠点情報初期値セット
        /// <summary>
        ///  拠点情報初期値セット
        /// </summary>
        public void SetKyoutenInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報から拠点CD取得
                SqlInt16 kyotenCD = SqlInt16.Parse(this.sysInfoEntity.SHIHARAI_KYOTEN_CD.ToString());

                M_KYOTEN mKyoten = new M_KYOTEN();
                // 拠点ID
                mKyoten.KYOTEN_CD = kyotenCD;

                M_KYOTEN[] kyotenInfo = kyoutenDao.GetAllValidData(mKyoten);

                if (kyotenInfo != null && kyotenInfo.Length > 0 && !kyotenCD.IsNull)
                {
                    this.form.HEADER_KYOTEN_CD.Text = kyotenCD.ToString();
                    this.form.HEADER_KYOTEN_NAME.Text = kyotenInfo[0].KYOTEN_NAME.ToString();
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKyoutenInfoInit", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region ファンクション

        #region [F1] 詳細
        /// <summary>
        /// 「F1 詳細ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        public void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 選択されたレコードを取得する
                DataGridViewCell datagridviewcell = this.form.Ichiran.CurrentCell;

                if (datagridviewcell != null)
                {
                    //入力画面へ遷移する（新規モード）
                    forwardNyuuryoku(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [F8] 検索
        void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.bt_func8_Click(true);
        }

        /// <summary>
        /// 「F8 検索ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func8_Click(bool showMsgFlg)
        {
            try
            {
                if (this.form.RegistErrorFlag)
                {
                    this.form.txtNum_HidukeSentaku.Focus();
                    return;
                }
                // 初期化
                this.haishaNumberChangedRowList.Clear();

                // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    return;
                }
                // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

                // 検索条件を設定する
                SetSearchString();

                // 検索結果を取得する
                this.SearchResult = this.dao.GetIchiranDataSql(this.SearchString);

                // ReadOnly制約を一時的に解除
                for (int i = 0; i < this.SearchResult.Columns.Count; i++)
                {
                    this.SearchResult.Columns[i].ReadOnly = false;
                }
                this.SearchResult.Columns["KUMIAI_SUMI"].DefaultValue = false;
                this.SearchResult.Columns["MOBILE_RENKEI"].DefaultValue = false;
                this.form.checkBoxAll.Checked = false;
                foreach (DataColumn column in this.SearchResult.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;
                }

                // 検索結果を画面に設定する
                int count = this.SearchResult.Rows.Count;
                if (count == 0 && showMsgFlg)
                {
                    msgLogic.MessageBoxShow("C001");

                    //明細をクリア
                    this.form.Ichiran.DataSource = null;
                    this.form.SAGYOU_DATE_BEGIN.Focus();
                    return;
                }
                else
                {
                    // 検索結果を表示する
                    this.form.Ichiran.DataSource = this.SearchResult;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [F9] 登録
        /// <summary>
        /// 「F9 登録ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        public void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 作業日の差異をチェック
                bool numError = false;
                bool cdError = false;

                bool MobieRegistCheck = false;
                this.Renkei_TeikiDetailSystemId = string.Empty;

                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                {
                    numError = false;
                    cdError = false;
                    string number = Convert.ToString(row.Cells["TEIKI_HAISHA_NUMBER"].Value);
                    if (string.IsNullOrEmpty(number))
                    {
                        numError = true;
                    }
                    string courseNameCd = Convert.ToString(row.Cells["COURSE_NAME_CD"].Value);
                    if (string.IsNullOrEmpty(courseNameCd))
                    {
                        cdError = true;
                    }
                    if (numError && !cdError)
                    {
                        row.Cells["TEIKI_HAISHA_NUMBER"].Style.BackColor = Constans.ERROR_COLOR;
                        msgLogic.MessageBoxShow("E012", "配車番号");
                        return;
                    }
                    else if (!numError && cdError)
                    {
                        row.Cells["COURSE_NAME_CD"].Style.BackColor = Constans.ERROR_COLOR;
                        msgLogic.MessageBoxShow("E012", "コースCD");
                        return;
                    }

                    if (is_mobile)
                    {
                        //データチェック
                        if ((bool)row.Cells["MOBILE_RENKEI"].Value)
                        {                        
                            //配車番号無し
                            if (numError)
                            {
                                row.Cells["TEIKI_HAISHA_NUMBER"].Style.BackColor = Constans.ERROR_COLOR;
                                msgLogic.MessageBoxShow("E012", "配車番号");
                                return;
                            }

                            if ((bool)row.Cells["KUMIAI_SUMI"].Value)
                            {
                                row.Cells["KUMIAI_SUMI"].Style.BackColor = Constans.ERROR_COLOR;
                                msgLogic.MessageBoxShowError("ﾓﾊﾞｲﾙ連携と臨時は同時にチェックできません。");
                                return;
                            }
                            //受付伝票が、ﾓﾊﾞｲﾙ連携されているか →[ﾓﾊﾞｲﾙ連携]OFF
                            if (this.RenkeiCheck(2, Convert.ToString(row.Cells["UKETSUKE_NUMBER"].Value)))
                            {
                                row.Cells["MOBILE_RENKEI"].Style.BackColor = Constans.ERROR_COLOR;
                                msgLogic.MessageBoxShowError("既にモバイル将軍へ連携されている為、連携出来ません。");
                                return;
                            }
                            //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                            //[システム日付] != [作業日]の場合はチェックをつけない
                            if (string.IsNullOrEmpty(row.Cells["SAGYOU_DATE"].Value.ToString()))
                            {
                                row.Cells["MOBILE_RENKEI"].Style.BackColor = Constans.ERROR_COLOR;
                                this.msgLogic.MessageBoxShowError("作業日が当日の場合のみ連携が可能です。");
                                return;
                            }
                            else
                            {
                                if (!(DateTime.Parse(row.Cells["SAGYOU_DATE"].Value.ToString()).ToString("yyyy/MM/dd")).Equals(DateTime.Now.ToString("yyyy/MM/dd")))
                                {
                                    row.Cells["MOBILE_RENKEI"].Style.BackColor = Constans.ERROR_COLOR;
                                    this.msgLogic.MessageBoxShowError("作業日が当日の場合のみ連携が可能です。");
                                    return;
                                }
                            }

                            //定期配車伝票が、モバイル状況一覧に表示される条件か
                            //UNTENSHA_CD、SHARYOU_CD、SHASHU_CD、SHASHU_NAME_RYAKUのデータがあり。
                            //ここではじく。
                            if (this.RenkeiCheck(6, number))
                            {
                                row.Cells["MOBILE_RENKEI"].Style.BackColor = Constans.ERROR_COLOR;
                                this.msgLogic.MessageBoxShowError("モバイル将軍へ連携する条件になっていません。");
                                return;
                            }

                            //★振替先の定期配車伝票の業者が、業者マスタの取引先有無区分が無の時
                            if (this.RenkeiCheck(7, number))
                            {
                                row.Cells["MOBILE_RENKEI"].Style.BackColor = Constans.ERROR_COLOR;
                                this.msgLogic.MessageBoxShowError("モバイル将軍へ連携する条件になっていません。");
                                return;
                            }
                            //★選択した業者の、業者マスタの取引先有無区分が無の時
                            if (this.RenkeiCheck(8, Convert.ToString(row.Cells["GYOUSHA_CD"].Value)))
                            {
                                row.Cells["MOBILE_RENKEI"].Style.BackColor = Constans.ERROR_COLOR;
                                this.msgLogic.MessageBoxShowError("モバイル将軍へ連携する条件になっていません。");
                                return;
                            }

                            //配車番号が、ﾓﾊﾞｲﾙ連携されているか
                            if (this.RenkeiCheck(5, Convert.ToString(row.Cells["UKETSUKE_NUMBER"].Value)))
                            {
                                row.Cells["MOBILE_RENKEI"].Style.BackColor = Constans.ERROR_COLOR;
                                this.msgLogic.MessageBoxShowError("既にモバイル将軍へ連携されている為、変更する事は出来ません。");
                                return;
                            }

                            //配車番号が、ロジコン連携されているか
                            if (this.RenkeiCheck(3, number))
                            {
                                row.Cells["MOBILE_RENKEI"].Style.BackColor = Constans.ERROR_COLOR;
                                this.msgLogic.MessageBoxShowError("ロジこんぱす連携中の為、変更する事は出来ません。");
                                return;
                            }

                            //配車番号が、NAVITIME連携されているか
                            if (this.RenkeiCheck(4, number))
                            {
                                row.Cells["MOBILE_RENKEI"].Style.BackColor = Constans.ERROR_COLOR;
                                this.msgLogic.MessageBoxShowError("NAVITIME連携中の為、変更する事は出来ません。");
                                return;
                            }
                        }
                    }
                }
                // [受付伝票]と[定期配車伝票]の作業日に差異のある行番号のListを取得します
                List<int> differenceRowIndexList = new List<int>();
                this.GetSagyouDateDeffResult(out differenceRowIndexList);

                // 取得したListが空じゃなかったらダイアログを表示
                DialogResult result = DialogResult.Yes;
                if (differenceRowIndexList.Count > 0)
                {
                    result = msgLogic.MessageBoxShow("C046", "収集受付伝票と定期配車伝票の作業日が異なっている行があります。登録");
                }

                // 「はい」を選択した場合はそのまま登録する
                if (result == DialogResult.Yes)
                {
                    // 配車番号（変更後）
                    string haishaNumber = string.Empty;
                    // 配車番号（変更前）
                    string haishaNumberBefore = string.Empty;
                    // 選択フラグ
                    bool isChecked = false;
                    // エンティティ区分
                    string entityKbn = string.Empty;
                    // コースCD
                    string courseNameCD = string.Empty;
                    // システムID
                    string systemID = string.Empty;
                    // 枝番
                    string seq = string.Empty;
                    // 業者CD
                    string gyoushaCD = string.Empty;
                    // 現場CD
                    string genbaCD = string.Empty;
                    // 受付番号
                    string uketsukeNumber = string.Empty;
                    // モバイル連携フラグ
                    bool isMobikeChecked = false;

                    DataTable dt = this.form.Ichiran.DataSource as DataTable;

                    // 明細行が空行１行の場合
                    if (dt == null || dt.Rows.Count == 0 || dt.GetChanges() == null)
                    {
                        msgLogic.MessageBoxShow("E061");
                        return;
                    }

                    foreach (DataColumn column in dt.Columns)
                    {
                        // NOT NULL制約を一時的に解除(新規追加行対策)
                        column.AllowDBNull = true;
                    }

                    dt.BeginLoadData();

                    // 変更したデータ取得
                    DataTable updateData = dt.GetChanges();

                    /// 20140930 Houkakou 「コース配車依頼入力」の休動を追加する　start
                    for (int i = 0; i < updateData.Rows.Count; i++)
                    {
                        DataRow dr = updateData.Rows[i];
                        if (this.checkClosedDate(dr) == false)
                        {
                            return;
                        }
                    }
                    /// 20140930 Houkakou 「コース配車依頼入力」の休動を追加する　end

                    int noModifyCount = 0;
                    int MobileRCount = 0;
                    using (Transaction tran = new Transaction())
                    {
                        // トランザクション開始
                        for (int i = 0; i < updateData.Rows.Count; i++)
                        {
                            DataRow dr = updateData.Rows[i];
                            // 配車番号（変更前）
                            haishaNumberBefore = updateData.Rows[i]["TEIKI_HAISHA_NUMBER_BEFORE"].ToString();
                            // 配車番号（変更後）
                            haishaNumber = updateData.Rows[i]["TEIKI_HAISHA_NUMBER"].ToString();
                            // 臨時フラグ
                            isChecked = bool.Parse(updateData.Rows[i]["KUMIAI_SUMI"].ToString());
                            // モバイル連携フラグ
                            isMobikeChecked = bool.Parse(updateData.Rows[i]["MOBILE_RENKEI"].ToString());
                            //受付番号
                            uketsukeNumber = updateData.Rows[i]["UKETSUKE_NUMBER"].ToString();

                            // 明細データの内、配車番号に新規に入力が有った場合
                            if (string.IsNullOrEmpty(haishaNumberBefore) && !string.IsNullOrEmpty(haishaNumber))
                            {
                                // １．受付情報登録処理
                                registUketsukeInfo(dr);

                                // ２．定期配車情報登録処理
                                registTeikihaishaInfo(dr);
                            }
                            // 明細データの内、配車番号に変更が有った場合（初期表示時点と登録ボタン押下時で変わった配車番号）
                            else if (!string.IsNullOrEmpty(haishaNumberBefore) &&
                                !string.IsNullOrEmpty(haishaNumber) &&
                                !haishaNumberBefore.Equals(haishaNumber))
                            {
                                // １．受付情報登録処理
                                registUketsukeInfo(dr);

                                // ２．定期配車情報登録処理
                                registTeikihaishaInfo(dr);

                                // ４．定期配車情報取り消し処理
                                cancelTeikihaishaInfo(dr);
                            }
                            // 明細データの内、配車番号が取り消し（クリア）された場合、または臨時チェックボックスがONの場合
                            else if ((!string.IsNullOrEmpty(haishaNumberBefore) && string.IsNullOrEmpty(haishaNumber)) || isChecked)
                            {
                                // ３．受付情報取り消し処理
                                cancelUketsukeInfo(dr);

                                // ４．定期配車情報取り消し処理
                                cancelTeikihaishaInfo(dr);
                            }
                            else
                            {
                                noModifyCount++;

                                //モバイル連携だけ行う場合、DETAIL_SYSTEM_IDを取得しておく
                                if (isMobikeChecked)
                                {
                                    string selectStr = "SELECT THD.DETAIL_SYSTEM_ID FROM T_TEIKI_HAISHA_ENTRY THE "
                                            + " INNER JOIN T_TEIKI_HAISHA_DETAIL THD ON THE.SYSTEM_ID = THD.SYSTEM_ID AND THE.SEQ = THD.SEQ "
                                            + " WHERE THE.DELETE_FLG = 0 AND THD.UKETSUKE_NUMBER = " + uketsukeNumber;
                                    dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                                    if (dt.Rows.Count > 0)
                                    {
                                        if (string.IsNullOrEmpty(this.Renkei_TeikiDetailSystemId))
                                        {
                                            this.Renkei_TeikiDetailSystemId = dt.Rows[0][0].ToString();
                                        }
                                        else
                                        {
                                            this.Renkei_TeikiDetailSystemId = this.Renkei_TeikiDetailSystemId + ", " + dt.Rows[0][0].ToString(); 
                                        }
                                    }
                                }
                            }
                            // モバイル連携
                            if (isMobikeChecked)
                            {
                                MobileRCount++;
                            }

                        }

                        if (updateData.Rows.Count == noModifyCount)
                        {
                            //更新データなし/モバイル連携なし→アラート→抜ける
                            if (MobileRCount == 0)
                            {
                                msgLogic.MessageBoxShow("E061");
                                return;
                            }
                        }
                        else
                        {
                            //更新データあり→コミット
                            tran.Commit();
                        }
                    }

                    //更新データあり→登録完了MSG
                    if (updateData.Rows.Count != noModifyCount)
                    {
                        // 登録完了メッセージ表示
                        msgLogic.MessageBoxShow("I001", "登録");
                    }

                    if (this.MobileRegistCheck())
                    {
                        //モバイルデータ登録チェック→モバイルデータ登録
                        MobieRegistCheck = this.MobileRegist();
                        if (MobieRegistCheck)
                        {
                            msgLogic.MessageBoxShow("I001", "データの連携");
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("I007", "データの連携");
                        }
                    }

                    // 画面再表示
                    bt_func8_Click(false);
                }
                else
                {
                    // 「いいえ」を選択した場合は作業日が異なる行の配車番号の背景色を赤くする
                    foreach (int rowIndex in differenceRowIndexList)
                    {
                        var dgvTextCell = this.form.Ichiran.Rows[rowIndex].Cells["TEIKI_HAISHA_NUMBER"] as DgvCustomTextBoxCell;
                        if (dgvTextCell != null)
                        {
                            dgvTextCell.IsInputErrorOccured = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                return;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [F12] 閉じる
        /// <summary>
        /// 「F12 閉じるボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [subF1] 地図表示
        /// <summary>
        /// 「subF1 地図表示ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_process1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.form.Ichiran.Rows.Count == 0)
                {
                    this.msgLogic.MessageBoxShowError("地図表示対象の明細がありません。");
                    return;
                }

                if (this.msgLogic.MessageBoxShowConfirm("地図を表示しますか？" +
    Environment.NewLine + "※緯度/経度が登録されていない現場は表示されません。") == DialogResult.No)
                {
                    return;
                }

                MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

                // 地図に渡すDTO作成
                List<mapDtoList> dtos = new List<mapDtoList>();
                dtos = this.createMapboxDto();
                if (dtos.Count == 0)
                {
                    this.msgLogic.MessageBoxShowError("表示する対象がありません。");
                    return;
                }

                // 地図表示
                gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.T_COURSE_HAISHA_IRAI);

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process1_Click", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion

        #region 入力画面へ遷移する
        /// <summary>
        /// 入力画面へ遷移する
        /// <param name="windowType"></param>
        /// </summary>
        private void forwardNyuuryoku(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                // 受付番号を初期化
                String uketsukeNumber = String.Empty;
                // 画面区分を初期化
                String gamenKbn = String.Empty;

                // 選択されたレコードを取得する
                DataGridViewCell datagridviewcell = this.form.Ichiran.CurrentCell;
                // 受付番号に設定
                uketsukeNumber = this.form.Ichiran.Rows[datagridviewcell.RowIndex].Cells["UKETSUKE_NUMBER"].Value.ToString();

                // 画面区分
                gamenKbn = this.form.Ichiran.Rows[datagridviewcell.RowIndex].Cells["ENTITY_KBN"].Value.ToString();

                //画面遷移
                switch (gamenKbn)
                {
                    case "1":
                        //受付（収集）入力
                        FormManager.OpenFormWithAuth("G015", windowType, windowType, uketsukeNumber);
                        break;
                    case "2":
                        //受付（出荷）入力
                        // No.4234対応により受付（出荷）は対象外となる。ただし処理は残す。
                        FormManager.OpenFormWithAuth("G016", windowType, windowType, uketsukeNumber);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("forwardNyuuryoku", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 検索条件の設定
        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public void SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DTOCls searchCondition = new DTOCls();

                // 組込状態
                searchCondition.KUMIKOMI_STATUS = this.form.txtNum_HidukeSentaku.Text;

                // 作業日FROM
                if (!string.IsNullOrEmpty(this.form.SAGYOU_DATE_BEGIN.Text.Trim()))
                {
                    searchCondition.SAGYOU_DATE_BEGIN = this.form.SAGYOU_DATE_BEGIN.Text.ToString().Substring(0, 10);
                }

                // 作業日TO
                if (!string.IsNullOrEmpty(this.form.SAGYOU_DATE_END.Text.Trim()))
                {
                    searchCondition.SAGYOU_DATE_END = this.form.SAGYOU_DATE_END.Text.ToString().Substring(0, 10);
                }

                // 拠点
                if (!string.IsNullOrEmpty(this.form.HEADER_KYOTEN_CD.Text) && "99" != this.form.HEADER_KYOTEN_CD.Text)
                {
                    searchCondition.KYOTEN_CD = this.form.HEADER_KYOTEN_CD.Text;
                }

                this.SearchString = searchCondition;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSearchString", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 配車番号検索
        /// <summary>
        /// 配車番号検索
        /// </summary>
        /// <param name="haishaNmuber"></param>
        public T_TEIKI_HAISHA_ENTRY getHaishaNumberInfo(string haishaNmuber, string sagyouDate, out bool catchErr)
        {
            T_TEIKI_HAISHA_ENTRY returnValue = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(haishaNmuber, sagyouDate);

                T_TEIKI_HAISHA_ENTRY keyEntity = new T_TEIKI_HAISHA_ENTRY();

                keyEntity.TEIKI_HAISHA_NUMBER = int.Parse(haishaNmuber);
                DateTime date = DateTime.Now;
                if (DateTime.TryParse(sagyouDate, out date))
                {
                    keyEntity.SAGYOU_DATE = DateTime.Parse(sagyouDate);
                }

                var courseNameCD = this.teikiHaishaDao.GetAllValidDataByHaishaNumber(keyEntity);

                if (courseNameCD != null && courseNameCD.Length > 0)
                {
                    returnValue = courseNameCD[0];
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("getHaishaNumberInfo", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue, catchErr);
            }
            return returnValue;
        }
        #endregion

        #region コース名検索
        /// <summary>
        /// コース名検索
        /// </summary>
        /// <param name="haishaNmuber"></param>
        public M_COURSE_NAME getCourseNameInfo(string haishaNmuber, out bool catchErr)
        {
            M_COURSE_NAME returnValue = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(haishaNmuber);

                M_COURSE_NAME keyEntity = new M_COURSE_NAME();

                keyEntity.COURSE_NAME_CD = haishaNmuber;

                var courseName = this.courseNameDao.GetAllValidData(keyEntity);

                if (courseName != null)
                {
                    returnValue = courseName[0];
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("getCourseNameInfo", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue, catchErr);
            }
            return returnValue;
        }
        #endregion

        #region 受付情報登録処理
        /// <summary>
        /// 受付情報登録処理
        /// </summary>
        /// <param name="dr"></param>
        private bool registUketsukeInfo(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);
            bool result = false;
            try
            {
                // エンティティ区分
                string entityKbn = dr["ENTITY_KBN"].ToString();
                // システムID
                int systemID = int.Parse(dr["SYSTEM_ID"].ToString());
                // 枝番
                int seq = int.Parse(dr["SEQ"].ToString());
                // コースCD
                string courseNameCD = dr["COURSE_NAME_CD"].ToString();

                var teikiHaishaNumber = Int64.Parse(dr["TEIKI_HAISHA_NUMBER"].ToString());
                var teikiHaishaEntryDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_ENTRYDao>();
                var teikiHaishaEntry = teikiHaishaEntryDao.GetAllValidData(new T_TEIKI_HAISHA_ENTRY() { TEIKI_HAISHA_NUMBER = teikiHaishaNumber }).DefaultIfEmpty(new T_TEIKI_HAISHA_ENTRY()).FirstOrDefault();


                var unpanDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                var unpan = unpanDao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = teikiHaishaEntry.UNPAN_GYOUSHA_CD }).DefaultIfEmpty(new M_GYOUSHA()).FirstOrDefault();
                var shashuDao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
                var shashu = shashuDao.GetAllValidData(new M_SHASHU() { SHASHU_CD = teikiHaishaEntry.SHASHU_CD }).DefaultIfEmpty(new M_SHASHU()).FirstOrDefault();
                var sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
                var sharyou = sharyouDao.GetAllValidData(new M_SHARYOU() { SHARYOU_CD = teikiHaishaEntry.SHARYOU_CD }).DefaultIfEmpty(new M_SHARYOU()).FirstOrDefault();
                var shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
                var untensha = shainDao.GetAllValidData(new M_SHAIN() { SHAIN_CD = teikiHaishaEntry.UNTENSHA_CD }).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();
                var hojoin = shainDao.GetAllValidData(new M_SHAIN() { SHAIN_CD = teikiHaishaEntry.HOJOIN_CD }).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();

                switch (entityKbn)
                {
                    // 受付（収集）の場合
                    case "1":
                        // システムIDと枝番より、受付（収集）入力テーブルからデータ取得
                        T_UKETSUKE_SS_ENTRY uketsukeSSEntry = getUketsukeSSEntryData(systemID);

                        if (uketsukeSSEntry != null)
                        {
                            // 1-1-1.受付（収集）入力テーブルの更新（論理削除）
                            var WHOSSEntry = new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(uketsukeSSEntry);
                            // システム自動設定のプロパティを設定する
                            //WHOSSEntry.SetSystemProperty(uketsukeSSEntry, true);
                            // 削除フラグを設定
                            uketsukeSSEntry.DELETE_FLG = true;
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            uketsukeSSEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSSEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // データ更新
                            this.daoUketsukeSSEntry.Update(uketsukeSSEntry);

                            // 1-1-2.受付（収集）入力テーブルの追加
                            // 枝番+1
                            uketsukeSSEntry.SEQ = seq + 1;
                            // 配車状況CD
                            uketsukeSSEntry.HAISHA_JOKYO_CD = 2;
                            // 配車状況名
                            uketsukeSSEntry.HAISHA_JOKYO_NAME = "配車";
                            // 配車種類CD
                            uketsukeSSEntry.HAISHA_SHURUI_CD = 3;
                            // 配車種類名
                            uketsukeSSEntry.HAISHA_SHURUI_NAME = "確定";
                            // コース名CD
                            uketsukeSSEntry.COURSE_NAME_CD = courseNameCD;
                            //運搬業者CD
                            uketsukeSSEntry.UNPAN_GYOUSHA_CD = teikiHaishaEntry.UNPAN_GYOUSHA_CD;
                            //運搬業者名
                            uketsukeSSEntry.UNPAN_GYOUSHA_NAME = unpan.GYOUSHA_NAME1;
                            // 車種CD
                            uketsukeSSEntry.SHASHU_CD = teikiHaishaEntry.SHASHU_CD;
                            // 車種名
                            uketsukeSSEntry.SHASHU_NAME = shashu.SHASHU_NAME_RYAKU;
                            //車輌CD
                            uketsukeSSEntry.SHARYOU_CD = teikiHaishaEntry.SHARYOU_CD;
                            // 車輌名
                            uketsukeSSEntry.SHARYOU_NAME = sharyou.SHARYOU_NAME_RYAKU;
                            // 運転者CD
                            uketsukeSSEntry.UNTENSHA_CD = teikiHaishaEntry.UNTENSHA_CD;
                            // 運転者名
                            uketsukeSSEntry.UNTENSHA_NAME = untensha.SHAIN_NAME_RYAKU;
                            // 補助員CD
                            uketsukeSSEntry.HOJOIN_CD = teikiHaishaEntry.HOJOIN_CD;
                            // 補助員名
                            uketsukeSSEntry.HOJOIN_NAME = hojoin.SHAIN_NAME_RYAKU;
                            // 削除フラグ
                            uketsukeSSEntry.DELETE_FLG = false;
                            //作業日
                            uketsukeSSEntry.SAGYOU_DATE = teikiHaishaEntry.SAGYOU_DATE.ToString();
                            // システム自動設定のプロパティを設定する
                            //WHOSSEntry.SetSystemProperty(uketsukeSSEntry,false);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            uketsukeSSEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSSEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 受付（収集）入力テーブルに追加
                            this.daoUketsukeSSEntry.Insert(uketsukeSSEntry);

                            // 1-1-3.受付（収集）明細テーブルの追加（コピーイメージ）
                            T_UKETSUKE_SS_DETAIL uketsukeSSDetailParam = new T_UKETSUKE_SS_DETAIL();
                            // システムID
                            uketsukeSSDetailParam.SYSTEM_ID = systemID;
                            // 枝番
                            uketsukeSSDetailParam.SEQ = seq;
                            // 取得したキーで読み込み
                            T_UKETSUKE_SS_DETAIL[] uketsukeSSDetailList = this.daoUketsukeSSDetail.GetDataForEntity(uketsukeSSDetailParam);

                            if (uketsukeSSDetailList != null && uketsukeSSDetailList.Length > 0)
                            {
                                foreach (var ssDetail in uketsukeSSDetailList)
                                {
                                    // システムID
                                    ssDetail.SYSTEM_ID = uketsukeSSEntry.SYSTEM_ID;
                                    // 枝番
                                    ssDetail.SEQ = uketsukeSSEntry.SEQ;
                                    // システム自動設定のプロパティを設定する
                                    //var WHOSSDetail = new DataBinderLogic<T_UKETSUKE_SS_DETAIL>(ssDetail);
                                    //WHOSSDetail.SetSystemProperty(ssDetail, false);
                                    // 更新日
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //ssDetail.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                    ssDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    // 更新者
                                    ssDetail.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    ssDetail.UPDATE_PC = SystemInformation.ComputerName;
                                    // 受付（収集）明細テーブルに追加
                                    this.daoUketsukeSSDetail.Insert(ssDetail);
                                }
                            }

                            // 1-1-4.コンテナ稼動予定テーブルの追加（コピーイメージ）
                            T_CONTENA_RESERVE contenaReserveParam = new T_CONTENA_RESERVE();
                            // システムID
                            contenaReserveParam.SYSTEM_ID = systemID;
                            // 枝番
                            contenaReserveParam.SEQ = seq;
                            // 取得したキーで読み込み
                            T_CONTENA_RESERVE[] contenaReserveList = this.daoContenaReserver.GetDataForEntity(contenaReserveParam);

                            if (contenaReserveList != null && contenaReserveList.Length > 0)
                            {
                                //削除フラグを立てる
                                foreach (var contenaReserve in contenaReserveList)
                                {
                                    // 削除フラグ
                                    contenaReserve.DELETE_FLG = true;
                                    // 更新日
                                    contenaReserve.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 更新者
                                    contenaReserve.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    contenaReserve.UPDATE_PC = SystemInformation.ComputerName;
                                    // コンテナ稼動予定テーブルに追加
                                    this.daoContenaReserver.Update(contenaReserve);
                                }


                                foreach (var contenaReserve in contenaReserveList)
                                {
                                    // システムID
                                    contenaReserve.SYSTEM_ID = uketsukeSSEntry.SYSTEM_ID;
                                    // 枝番
                                    contenaReserve.SEQ = uketsukeSSEntry.SEQ;
                                    // DELETE_FLG
                                    contenaReserve.DELETE_FLG = false;
                                    // システム自動設定のプロパティを設定する
                                    //var WHOSSDetail = new DataBinderLogic<T_CONTENA_RESERVE>(contenaReserve);
                                    //WHOSSDetail.SetSystemProperty(contenaReserve, false);
                                    // 更新日
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //contenaReserve.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                    contenaReserve.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    // 更新者
                                    contenaReserve.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    contenaReserve.UPDATE_PC = SystemInformation.ComputerName;
                                    // コンテナ稼動予定テーブルに追加
                                    this.daoContenaReserver.Insert(contenaReserve);
                                }
                            }
                        }

                        break;

                    // 受付（出荷）の場合
                    // No.4234対応により受付（出荷）は対象外となる。ただし処理は残す。
                    case "2":
                        // システムIDと枝番より、データ取得
                        T_UKETSUKE_SK_ENTRY uketsukeSKEntry = getUketsukeSKEntryData(systemID);

                        if (uketsukeSKEntry != null)
                        {
                            // 1-1-1.受付（出荷）入力テーブルの更新（論理削除）
                            var WHOSKEntry = new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(uketsukeSKEntry);
                            // システム自動設定のプロパティを設定する
                            //WHOSKEntry.SetSystemProperty(uketsukeSKEntry, true);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            uketsukeSKEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSKEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 削除フラグを設定
                            uketsukeSKEntry.DELETE_FLG = true;
                            this.daoUketsukeSKEntry.Update(uketsukeSKEntry);

                            // 1-1-2.受付（出荷）入力テーブルの追加
                            // 枝番+1
                            uketsukeSKEntry.SEQ = seq + 1;
                            // 配車状況CD
                            uketsukeSKEntry.HAISHA_JOKYO_CD = 2;
                            // 配車状況名
                            uketsukeSKEntry.HAISHA_JOKYO_NAME = "配車";
                            // 配車種類CD
                            uketsukeSKEntry.HAISHA_SHURUI_CD = 3;
                            // 配車種類名
                            uketsukeSKEntry.HAISHA_SHURUI_NAME = "確定";
                            // コース名CD
                            uketsukeSKEntry.COURSE_NAME_CD = courseNameCD;
                            //運搬業者CD
                            uketsukeSKEntry.UNPAN_GYOUSHA_CD = teikiHaishaEntry.UNPAN_GYOUSHA_CD;
                            //運搬業者名
                            uketsukeSKEntry.UNPAN_GYOUSHA_NAME = unpan.GYOUSHA_NAME1;
                            // 車種CD
                            uketsukeSKEntry.SHASHU_CD = teikiHaishaEntry.SHASHU_CD;
                            // 車種名
                            uketsukeSKEntry.SHASHU_NAME = shashu.SHASHU_NAME_RYAKU;
                            //車輌CD
                            uketsukeSKEntry.SHARYOU_CD = teikiHaishaEntry.SHARYOU_CD;
                            // 車輌名
                            uketsukeSKEntry.SHARYOU_NAME = sharyou.SHARYOU_NAME_RYAKU;
                            // 運転者CD
                            uketsukeSKEntry.UNTENSHA_CD = teikiHaishaEntry.UNTENSHA_CD;
                            // 運転者名
                            uketsukeSKEntry.UNTENSHA_NAME = untensha.SHAIN_NAME_RYAKU;
                            // 補助員CD
                            uketsukeSKEntry.HOJOIN_CD = teikiHaishaEntry.HOJOIN_CD;
                            // 補助員名
                            uketsukeSKEntry.HOJOIN_NAME = hojoin.SHAIN_NAME_RYAKU;
                            // 削除フラグ
                            uketsukeSKEntry.DELETE_FLG = false;
                            // システム自動設定のプロパティを設定する
                            //WHOSKEntry.SetSystemProperty(uketsukeSKEntry, false);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            uketsukeSKEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSKEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 受付（出荷）入力テーブルに追加
                            this.daoUketsukeSKEntry.Insert(uketsukeSKEntry);

                            // 1-1-3.受付（出荷）明細テーブルの追加（コピーイメージ）
                            T_UKETSUKE_SK_DETAIL uketsukeSKDetailParam = new T_UKETSUKE_SK_DETAIL();
                            // システムID
                            uketsukeSKDetailParam.SYSTEM_ID = systemID;
                            // 枝番
                            uketsukeSKDetailParam.SEQ = seq;
                            // 取得したキーで読み込み
                            T_UKETSUKE_SK_DETAIL[] uketsukeSKDetailList = this.daoUketsukeSKDetail.GetDataForEntity(uketsukeSKDetailParam);

                            if (uketsukeSKDetailList != null && uketsukeSKDetailList.Length > 0)
                            {
                                foreach (var skDetail in uketsukeSKDetailList)
                                {
                                    // システムID
                                    skDetail.SYSTEM_ID = uketsukeSKEntry.SYSTEM_ID;
                                    // 枝番
                                    skDetail.SEQ = uketsukeSKEntry.SEQ;
                                    // 更新者、更新日など設定
                                    //var WHOSSDetail = new DataBinderLogic<T_UKETSUKE_SK_DETAIL>(skDetail);
                                    //WHOSSDetail.SetSystemProperty(skDetail, false);
                                    // 更新日
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //skDetail.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                    skDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    // 更新者
                                    skDetail.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    skDetail.UPDATE_PC = SystemInformation.ComputerName;
                                    // 受付（出荷）明細テーブルに追加
                                    this.daoUketsukeSKDetail.Insert(skDetail);
                                }
                            }
                        }

                        break;

                    default:
                        break;
                }

                result = true;

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }
        #endregion

        #region 定期配車情報登録処理
        /// <summary>
        /// 定期配車情報登録処理
        /// </summary>
        /// <param name="dr"></param>
        private bool registTeikihaishaInfo(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);
            bool result = false;
            try
            {
                // エンティティ区分
                string entityKbn = dr["ENTITY_KBN"].ToString();
                // システムID
                int systemID = int.Parse(dr["SYSTEM_ID"].ToString());
                // 枝番
                int seq = int.Parse(dr["SEQ"].ToString());
                // コースCD
                string courseNameCD = dr["COURSE_NAME_CD"].ToString();
                // 業者CD
                string gyoushaCD = dr["GYOUSHA_CD"].ToString();
                // 現場CD
                string genbaCD = dr["GENBA_CD"].ToString();
                // 受付番号
                string uketsukeNumber = dr["UKETSUKE_NUMBER"].ToString();
                // 配車番号
                int haishaNumber = int.Parse(dr["TEIKI_HAISHA_NUMBER"].ToString());
                // 回数
                Int32 roundNo = 1;

                // 配車番号より、データ取得
                T_TEIKI_HAISHA_ENTRY teikihaishaEntry = getTeikihaishaEntryData(haishaNumber);

                if (teikihaishaEntry != null)
                {
                    // 枝番
                    seq = (int)teikihaishaEntry.SEQ;

                    // 2-1.定期配車入力テーブルの更新（論理削除）
                    var WHOTeikihaishaEntry = new DataBinderLogic<T_TEIKI_HAISHA_ENTRY>(teikihaishaEntry);
                    // システム自動設定のプロパティを設定する
                    //WHOTeikihaishaEntry.SetSystemProperty(teikihaishaEntry, true);
                    // 更新日
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //teikihaishaEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    teikihaishaEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    // 更新者
                    teikihaishaEntry.UPDATE_USER = SystemProperty.UserName;
                    // 更新PC
                    teikihaishaEntry.UPDATE_PC = SystemInformation.ComputerName;
                    // 削除フラグを設定
                    teikihaishaEntry.DELETE_FLG = true;
                    // データ更新
                    this.teikiHaishaDao.Update(teikihaishaEntry);

                    // 2-2.定期配車入力テーブルの追加
                    // 枝番+1
                    teikihaishaEntry.SEQ = seq + 1;
                    // 削除フラグ
                    teikihaishaEntry.DELETE_FLG = false;
                    // システム自動設定のプロパティを設定する
                    //WHOTeikihaishaEntry.SetSystemProperty(teikihaishaEntry, false);
                    // 更新日
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //teikihaishaEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    teikihaishaEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    // 更新者
                    teikihaishaEntry.UPDATE_USER = SystemProperty.UserName;
                    // 更新PC
                    teikihaishaEntry.UPDATE_PC = SystemInformation.ComputerName;
                    // 定期配車入力テーブルに追加
                    this.teikiHaishaDao.Insert(teikihaishaEntry);

                    // 2-3.定期配車荷降テーブルの追加（コピーイメージ）
                    T_TEIKI_HAISHA_NIOROSHI teikihaishaNioroshiParam = new T_TEIKI_HAISHA_NIOROSHI();
                    // 定期配車番号
                    teikihaishaNioroshiParam.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                    // システムID
                    teikihaishaNioroshiParam.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                    // 枝番
                    teikihaishaNioroshiParam.SEQ = seq;

                    T_TEIKI_HAISHA_NIOROSHI[] teikihaishaNioroshiList = teikiHaishaNioroshiDao.GetDataForEntity(teikihaishaNioroshiParam);

                    if (teikihaishaNioroshiList != null && teikihaishaNioroshiList.Length > 0)
                    {
                        foreach (var teikihaishaNioroshiDetail in teikihaishaNioroshiList)
                        {
                            // システムID
                            teikihaishaNioroshiDetail.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                            // 枝番
                            teikihaishaNioroshiDetail.SEQ = teikihaishaEntry.SEQ;
                            // システム自動設定のプロパティを設定する
                            //var WHOTHNDetail = new DataBinderLogic<T_TEIKI_HAISHA_NIOROSHI>(teikihaishaNioroshiDetail);
                            //WHOTHNDetail.SetSystemProperty(teikihaishaNioroshiDetail, false);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //teikihaishaNioroshiDetail.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            teikihaishaNioroshiDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            teikihaishaNioroshiDetail.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            teikihaishaNioroshiDetail.UPDATE_PC = SystemInformation.ComputerName;
                            // 定期配車荷降テーブルに追加
                            this.teikiHaishaNioroshiDao.Insert(teikihaishaNioroshiDetail);
                        }
                    }

                    // 2-4.定期配車明細テーブルの追加（コピーイメージ）
                    T_TEIKI_HAISHA_DETAIL teikihaishaDetailParam = new T_TEIKI_HAISHA_DETAIL();
                    // 定期配車番号
                    teikihaishaDetailParam.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                    // システムID
                    teikihaishaDetailParam.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                    // 枝番
                    teikihaishaDetailParam.SEQ = seq;

                    T_TEIKI_HAISHA_DETAIL[] teikihaishaDetailList = teikiHaishaDetailDao.GetDataForEntity(teikihaishaDetailParam);

                    SqlInt16 rowNumber = 0;
                    if (teikihaishaDetailList != null && teikihaishaDetailList.Length > 0)
                    {
                        foreach (var teikihaishaDetailItem in teikihaishaDetailList)
                        {
                            // rowNumber
                            rowNumber = rowNumber > teikihaishaDetailItem.ROW_NUMBER ? rowNumber : teikihaishaDetailItem.ROW_NUMBER;
                            // システムID
                            teikihaishaDetailItem.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                            // 枝番
                            teikihaishaDetailItem.SEQ = teikihaishaEntry.SEQ;
                            // システム自動設定のプロパティを設定する
                            //var WHOTeikihaishaDetail = new DataBinderLogic<T_TEIKI_HAISHA_DETAIL>(teikihaishaDetailItem);
                            //WHOTeikihaishaDetail.SetSystemProperty(teikihaishaDetailItem, false);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //teikihaishaDetailItem.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            teikihaishaDetailItem.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            teikihaishaDetailItem.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            teikihaishaDetailItem.UPDATE_PC = SystemInformation.ComputerName;
                            // 定期配車荷降テーブルに追加
                            this.teikiHaishaDetailDao.Insert(teikihaishaDetailItem);
                        }
                    }

                    // 2-5.定期配車明細テーブルの追加（画面より新規作成）
                    T_TEIKI_HAISHA_DETAIL teikihaishaDetail = new T_TEIKI_HAISHA_DETAIL();
                    // システムID
                    teikihaishaDetail.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                    // 枝番
                    teikihaishaDetail.SEQ = teikihaishaEntry.SEQ;
                    // 明細システムID
                    teikihaishaDetail.DETAIL_SYSTEM_ID = createSystemIdForUketsuke();

                    //ﾓﾊﾞｲﾙ連携用に、DETAIL_SYSTEM_IDを集める
                    if ((bool)dr["MOBILE_RENKEI"])
                    {
                        if (string.IsNullOrEmpty(this.Renkei_TeikiDetailSystemId))
                        {
                            this.Renkei_TeikiDetailSystemId = teikihaishaDetail.DETAIL_SYSTEM_ID.ToString();
                        }
                        else
                        {
                            this.Renkei_TeikiDetailSystemId = this.Renkei_TeikiDetailSystemId + ", " + teikihaishaDetail.DETAIL_SYSTEM_ID.ToString();
                        }
                    }

                    // 定期配車番号
                    teikihaishaDetail.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                    // 行番号
                    teikihaishaDetail.ROW_NUMBER = rowNumber + 1;
                    // 業者CD
                    teikihaishaDetail.GYOUSHA_CD = gyoushaCD;
                    // 現場CD
                    teikihaishaDetail.GENBA_CD = genbaCD;
                    // 明細備考
                    teikihaishaDetail.MEISAI_BIKOU = null;
                    // 受付番号
                    teikihaishaDetail.UKETSUKE_NUMBER = int.Parse(uketsukeNumber);
                    // システム自動設定のプロパティを設定する
                    //var WHOTeikihaishaDetailNew = new DataBinderLogic<T_TEIKI_HAISHA_DETAIL>(teikihaishaDetail);
                    //WHOTeikihaishaDetailNew.SetSystemProperty(teikihaishaDetail, false);
                    // 更新日
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //teikihaishaDetail.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    teikihaishaDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    // 更新者
                    teikihaishaDetail.UPDATE_USER = SystemProperty.UserName;
                    // 更新PC
                    teikihaishaDetail.UPDATE_PC = SystemInformation.ComputerName;

                    // 回数を算出し、回数をセット
                    if (teikihaishaDetailList != null && teikihaishaDetailList.Length > 0)
                    {
                        // Insert対象の業者CD・現場CDと一致するものを検索
                        var list = teikihaishaDetailList.Where(r => (r.GYOUSHA_CD == gyoushaCD && r.GENBA_CD == genbaCD)).ToArray();

                        if (list != null && list.Length > 0)
                        {
                            // 該当情報が存在すれば、回数の最大値+1をInsert対象の回数とする
                            roundNo = (list.Select(r => r.ROUND_NO).Max() + 1).Value;
                        }
                    }
                    teikihaishaDetail.ROUND_NO = roundNo;

                    // 定期配車明細テーブルに追加
                    this.teikiHaishaDetailDao.Insert(teikihaishaDetail);

                    // 2-6.定期配車詳細テーブルの追加（コピーイメージ）
                    T_TEIKI_HAISHA_SHOUSAI teikihaishaShousaiParam = new T_TEIKI_HAISHA_SHOUSAI();
                    // 定期配車番号
                    teikihaishaShousaiParam.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                    // システムID
                    teikihaishaShousaiParam.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                    // 枝番
                    teikihaishaShousaiParam.SEQ = seq;

                    T_TEIKI_HAISHA_SHOUSAI[] teikiHaishaShousaiList = teikiHaishaShousaiDao.GetDataForEntity(teikihaishaShousaiParam);

                    if (teikiHaishaShousaiList != null && teikiHaishaShousaiList.Length > 0)
                    {
                        foreach (var teikihaishaShousaiItem in teikiHaishaShousaiList)
                        {
                            // システムID
                            teikihaishaShousaiItem.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                            // 枝番
                            teikihaishaShousaiItem.SEQ = teikihaishaEntry.SEQ;
                            // システム自動設定のプロパティを設定する
                            //var WHOTeikihaishaShousai = new DataBinderLogic<T_TEIKI_HAISHA_SHOUSAI>(teikihaishaShousaiItem);
                            //WHOTeikihaishaShousai.SetSystemProperty(teikihaishaShousaiItem, false);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //teikihaishaShousaiItem.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            teikihaishaShousaiItem.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            teikihaishaShousaiItem.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            teikihaishaShousaiItem.UPDATE_PC = SystemInformation.ComputerName;
                            // 定期配車詳細テーブルに追加
                            this.teikiHaishaShousaiDao.Insert(teikihaishaShousaiItem);
                        }
                    }

                    // 2-7.定期配車詳細テーブルの追加（画面→ＤＢより新規作成）
                    DataTable uketsukeDetailList = new DataTable();
                    if (entityKbn.Equals("1"))
                    {
                        // 受付（収集）入力テーブル検索
                        DTOCls uketsukeSSDetail = new DTOCls();
                        // システムID
                        uketsukeSSDetail.SYSTEM_ID = systemID;
                        // 受付（収集）情報取得
                        uketsukeDetailList = daoUketsukeSSDetail.GetDataToDataTable(uketsukeSSDetail);
                    }
                    // No.4234対応により受付（出荷）は対象外となる。ただし処理は残す。
                    else if (entityKbn.Equals("2"))
                    {
                        // 受付（出荷）入力テーブル検索
                        DTOCls uketsukeSKDetail = new DTOCls();
                        // システムID
                        uketsukeSKDetail.SYSTEM_ID = systemID;
                        // 受付（出荷）情報取得
                        uketsukeDetailList = daoUketsukeSKDetail.GetDataToDataTable(uketsukeSKDetail);
                    }

                    for (int i = 0; i < uketsukeDetailList.Rows.Count; i++)
                    {
                        M_GENBA_TEIKI_HINMEI genbaTeikiHinmeiEntity = new M_GENBA_TEIKI_HINMEI();
                        genbaTeikiHinmeiEntity.GYOUSHA_CD = gyoushaCD;
                        genbaTeikiHinmeiEntity.GENBA_CD = genbaCD;
                        genbaTeikiHinmeiEntity.HINMEI_CD = uketsukeDetailList.Rows[i]["HINMEI_CD"].ToString();
                        genbaTeikiHinmeiEntity.UNIT_CD = SqlInt16.Parse(uketsukeDetailList.Rows[i]["UNIT_CD"].ToString());

                        // 現場_定期品名検索
                        M_GENBA_TEIKI_HINMEI genbaTeikiHinmei = daoGenbaTeikiHinmei.GetDataForEntity(genbaTeikiHinmeiEntity).FirstOrDefault();

                        T_TEIKI_HAISHA_SHOUSAI teikihaishaShousai = new T_TEIKI_HAISHA_SHOUSAI();
                        // SYSTEM_ID
                        teikihaishaShousai.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                        // SEQ
                        teikihaishaShousai.SEQ = teikihaishaEntry.SEQ;
                        // DETAIL_SYSTEM_ID
                        teikihaishaShousai.DETAIL_SYSTEM_ID = teikihaishaDetail.DETAIL_SYSTEM_ID;
                        // ROW_NUMBER
                        teikihaishaShousai.ROW_NUMBER = i + 1;
                        // TEIKI_HAISHA_NUMBER
                        teikihaishaShousai.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                        // INPUT_KBN
                        teikihaishaShousai.INPUT_KBN = (genbaTeikiHinmei != null) ? SqlInt16.Parse(CourseHaishaConstans.INPUT_KBN_KUMIKOMI) : SqlInt16.Parse(CourseHaishaConstans.INPUT_KBN_CHOKUSETU);
                        // NIOROSHI_NUMBER
                        // HINMEI_CD
                        teikihaishaShousai.HINMEI_CD = uketsukeDetailList.Rows[i]["HINMEI_CD"].ToString();
                        // UNIT_CD
                        teikihaishaShousai.UNIT_CD = SqlInt16.Parse(uketsukeDetailList.Rows[i]["UNIT_CD"].ToString());
                        // KANSANCHI
                        if (genbaTeikiHinmei != null)
                        {
                            teikihaishaShousai.KANSANCHI = genbaTeikiHinmei.KANSANCHI;
                        }
                        // KANSAN_UNIT_CD
                        if (genbaTeikiHinmei != null)
                        {
                            teikihaishaShousai.KANSAN_UNIT_CD = genbaTeikiHinmei.KANSAN_UNIT_CD;
                        }

                        SqlInt16 unitKg = SqlInt16.Parse(CourseHaishaConstans.UNIT_CD_KG);
                        if (!(unitKg.Equals(teikihaishaShousai.UNIT_CD) || unitKg.Equals(teikihaishaShousai.KANSAN_UNIT_CD)))
                        {
                            // 換算後単位にkg設定。回収品名詳細画面で単位or換算後単位に「kg」必須のため
                            teikihaishaShousai.KANSAN_UNIT_CD = unitKg;
                        }

                        // KEIYAKU_KBN
                        teikihaishaShousai.KEIYAKU_KBN = (genbaTeikiHinmei != null) ? genbaTeikiHinmei.KEIYAKU_KBN : SqlInt16.Parse(CourseHaishaConstans.KEIYAKU_KBN_TANKA);
                        // KEIJYOU_KBN
                        teikihaishaShousai.KEIJYOU_KBN = (genbaTeikiHinmei != null) ? genbaTeikiHinmei.KEIJYOU_KBN : SqlInt16.Parse(CourseHaishaConstans.KEIJYOU_KBN_DENPYOU);
                        // ADD_FLG
                        teikihaishaShousai.ADD_FLG = true;
                        // DENPYOU_KBN_CD
                        teikihaishaShousai.DENPYOU_KBN_CD = SqlInt16.Parse(uketsukeDetailList.Rows[i]["DENPYOU_KBN_CD"].ToString());
                        // KANSAN_UNIT_MOBILE_OUTPUT_FLG
                        teikihaishaShousai.KANSAN_UNIT_MOBILE_OUTPUT_FLG = (genbaTeikiHinmei != null) ? genbaTeikiHinmei.KANSAN_UNIT_MOBILE_OUTPUT_FLG : false;
                        // ANBUN_FLG
                        teikihaishaShousai.ANBUN_FLG = (genbaTeikiHinmei != null) ? genbaTeikiHinmei.ANBUN_FLG : false;

                        teikiHaishaShousaiDao.Insert(teikihaishaShousai);
                    }
                }

                result = true;

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }
        #endregion

        #region 受付情報取り消し処理
        /// <summary>
        /// 受付情報取り消し処理
        /// </summary>
        /// <param name="dr"></param>
        private bool cancelUketsukeInfo(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);
            bool result = false;
            try
            {
                // エンティティ区分
                string entityKbn = dr["ENTITY_KBN"].ToString();
                // システムID
                int systemID = int.Parse(dr["SYSTEM_ID"].ToString());
                // 枝番
                int seq = int.Parse(dr["SEQ"].ToString());
                // コースCD
                string courseNameCD = dr["COURSE_NAME_CD"].ToString();
                // 選択フラグ
                bool isChecked = (bool)(dr["KUMIAI_SUMI"]);
                // 業者CD
                string gyoushaCD = dr["GYOUSHA_CD"].ToString();
                // 現場CD
                string genbaCD = dr["GENBA_CD"].ToString();
                // 受付番号
                string uketsukeNumber = dr["UKETSUKE_NUMBER"].ToString();

                switch (entityKbn)
                {
                    // 受付（収集）の場合
                    case "1":
                        // システムIDと枝番より、データ取得
                        T_UKETSUKE_SS_ENTRY uketsukeSSEntry = getUketsukeSSEntryData(systemID);

                        if (uketsukeSSEntry != null)
                        {
                            seq = (int)uketsukeSSEntry.SEQ;
                            // 3-1-1.受付（収集）入力テーブルの更新（論理削除）
                            var WHOSSEntry = new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(uketsukeSSEntry);
                            // システム自動設定のプロパティを設定する
                            //WHOSSEntry.SetSystemProperty(uketsukeSSEntry, true);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            uketsukeSSEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSSEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 削除フラグを設定
                            uketsukeSSEntry.DELETE_FLG = true;
                            // データ更新
                            this.daoUketsukeSSEntry.Update(uketsukeSSEntry);

                            // 3-1-2.受付（収集）入力テーブルの追加
                            // 枝番+1
                            uketsukeSSEntry.SEQ = seq + 1;
                            // 配車状況CD
                            uketsukeSSEntry.HAISHA_JOKYO_CD = 1;
                            // 配車状況名
                            uketsukeSSEntry.HAISHA_JOKYO_NAME = "受注";
                            // 配車種類CD
                            uketsukeSSEntry.HAISHA_SHURUI_CD = 1;
                            // 配車種類名
                            uketsukeSSEntry.HAISHA_SHURUI_NAME = "通常";
                            // コース組込CD
                            if (isChecked)
                            {
                                uketsukeSSEntry.COURSE_KUMIKOMI_CD = 1;
                            }
                            // コース名CD
                            uketsukeSSEntry.COURSE_NAME_CD = null;
                            //運搬業者CD
                            uketsukeSSEntry.UNPAN_GYOUSHA_CD = String.Empty;
                            //運搬業者名
                            uketsukeSSEntry.UNPAN_GYOUSHA_NAME = String.Empty;
                            // 車種CD
                            uketsukeSSEntry.SHASHU_CD = String.Empty;
                            // 車種名
                            uketsukeSSEntry.SHASHU_NAME = String.Empty;
                            //車輌CD
                            uketsukeSSEntry.SHARYOU_CD = String.Empty;
                            // 車輌名
                            uketsukeSSEntry.SHARYOU_NAME = String.Empty;
                            // 運転者CD
                            uketsukeSSEntry.UNTENSHA_CD = String.Empty;
                            // 運転者名
                            uketsukeSSEntry.UNTENSHA_NAME = String.Empty;
                            // 補助員CD
                            uketsukeSSEntry.HOJOIN_CD = String.Empty;
                            // 補助員名
                            uketsukeSSEntry.HOJOIN_NAME = String.Empty;
                            // 削除フラグ
                            uketsukeSSEntry.DELETE_FLG = false;
                            // システム自動設定のプロパティを設定する
                            //WHOSSEntry.SetSystemProperty(uketsukeSSEntry, false);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            uketsukeSSEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSSEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 受付（収集）入力テーブルに追加
                            this.daoUketsukeSSEntry.Insert(uketsukeSSEntry);

                            // 3-1-3.受付（収集）明細テーブルの追加（コピーイメージ）
                            T_UKETSUKE_SS_DETAIL uketsukeSSDetailParam = new T_UKETSUKE_SS_DETAIL();
                            // システムID
                            uketsukeSSDetailParam.SYSTEM_ID = systemID;
                            // 枝番
                            uketsukeSSDetailParam.SEQ = seq;

                            // 受付（収集）明細データ取得
                            T_UKETSUKE_SS_DETAIL[] uketsukeSSDetailList = daoUketsukeSSDetail.GetDataForEntity(uketsukeSSDetailParam);

                            if (uketsukeSSDetailList != null && uketsukeSSDetailList.Length > 0)
                            {
                                foreach (var ssDetail in uketsukeSSDetailList)
                                {
                                    // システムID
                                    ssDetail.SYSTEM_ID = uketsukeSSEntry.SYSTEM_ID;
                                    // 枝番
                                    ssDetail.SEQ = uketsukeSSEntry.SEQ;
                                    // システム自動設定のプロパティを設定する
                                    //var WHOSSDetail = new DataBinderLogic<T_UKETSUKE_SS_DETAIL>(ssDetail);
                                    //WHOSSDetail.SetSystemProperty(ssDetail, false);
                                    // 更新日
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //ssDetail.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                    ssDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    // 更新者
                                    ssDetail.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    ssDetail.UPDATE_PC = SystemInformation.ComputerName;
                                    // 受付（収集）明細テーブルに追加
                                    this.daoUketsukeSSDetail.Insert(ssDetail);
                                }
                            }

                            // 3-1-4.コンテナ稼動予定テーブルの追加（コピーイメージ）
                            T_CONTENA_RESERVE contenaReserveParam = new T_CONTENA_RESERVE();
                            // システムID
                            contenaReserveParam.SYSTEM_ID = systemID;
                            // 枝番
                            contenaReserveParam.SEQ = seq;

                            T_CONTENA_RESERVE[] contenaReserveList = daoContenaReserver.GetDataForEntity(contenaReserveParam);

                            if (contenaReserveList != null && contenaReserveList.Length > 0)
                            {
                                foreach (var contenareserveDetail in contenaReserveList)
                                {
                                    // 削除フラグ
                                    contenareserveDetail.DELETE_FLG = true;
                                    // 更新日
                                    contenareserveDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 更新者
                                    contenareserveDetail.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    contenareserveDetail.UPDATE_PC = SystemInformation.ComputerName;
                                    // コンテナ稼動予定テーブルに追加
                                    this.daoContenaReserver.Update(contenareserveDetail);
                                }

                                foreach (var contenareserveDetail in contenaReserveList)
                                {
                                    // システムID
                                    contenareserveDetail.SYSTEM_ID = uketsukeSSEntry.SYSTEM_ID;
                                    // 枝番
                                    contenareserveDetail.SEQ = uketsukeSSEntry.SEQ;
                                    // 削除フラグ
                                    contenareserveDetail.DELETE_FLG = false;
                                    // システム自動設定のプロパティを設定する
                                    //var WHOSSDetail = new DataBinderLogic<T_CONTENA_RESERVE>(contenareserveDetail);
                                    //WHOSSDetail.SetSystemProperty(contenareserveDetail, false);
                                    // 更新日
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //contenareserveDetail.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                    contenareserveDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    // 更新者
                                    contenareserveDetail.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    contenareserveDetail.UPDATE_PC = SystemInformation.ComputerName;
                                    // コンテナ稼動予定テーブルに追加
                                    this.daoContenaReserver.Insert(contenareserveDetail);
                                }
                            }
                        }

                        break;
                    // 受付（出荷）の場合
                    // No.4234対応により受付（出荷）は対象外となる。ただし処理は残す。
                    case "2":
                        // システムIDと枝番より、データ取得
                        T_UKETSUKE_SK_ENTRY uketsukeSKEntry = getUketsukeSKEntryData(systemID);

                        if (uketsukeSKEntry != null)
                        {
                            seq = (int)uketsukeSKEntry.SEQ;
                            // 3-2-1.受付（出荷）入力テーブルの更新（論理削除）
                            var WHOSKEntry = new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(uketsukeSKEntry);
                            // システム自動設定のプロパティを設定する
                            //WHOSKEntry.SetSystemProperty(uketsukeSKEntry, true);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            uketsukeSKEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSKEntry.UPDATE_PC = SystemInformation.ComputerName;
                            // 削除フラグを設定
                            uketsukeSKEntry.DELETE_FLG = true;
                            // データ更新
                            this.daoUketsukeSKEntry.Update(uketsukeSKEntry);

                            // 3-2-2.受付（出荷）入力テーブルの追加
                            // 枝番+1
                            uketsukeSKEntry.SEQ = seq + 1;
                            // 配車状況CD
                            uketsukeSKEntry.HAISHA_JOKYO_CD = 1;
                            // 配車状況名
                            uketsukeSKEntry.HAISHA_JOKYO_NAME = "受注";
                            // 配車種類CD
                            uketsukeSKEntry.HAISHA_SHURUI_CD = 1;
                            // 配車種類名
                            uketsukeSKEntry.HAISHA_SHURUI_NAME = "通常";
                            // コース組込CD
                            if (isChecked)
                            {
                                uketsukeSKEntry.COURSE_KUMIKOMI_CD = 1;
                            }
                            // コース名CD
                            uketsukeSKEntry.COURSE_NAME_CD = null;
                            //運搬業者CD
                            uketsukeSKEntry.UNPAN_GYOUSHA_CD = String.Empty;
                            //運搬業者名
                            uketsukeSKEntry.UNPAN_GYOUSHA_NAME = String.Empty;
                            // 車種CD
                            uketsukeSKEntry.SHASHU_CD = String.Empty;
                            // 車種名
                            uketsukeSKEntry.SHASHU_NAME = String.Empty;
                            //車輌CD
                            uketsukeSKEntry.SHARYOU_CD = String.Empty;
                            // 車輌名
                            uketsukeSKEntry.SHARYOU_NAME = String.Empty;
                            // 運転者CD
                            uketsukeSKEntry.UNTENSHA_CD = String.Empty;
                            // 運転者名
                            uketsukeSKEntry.UNTENSHA_NAME = String.Empty;
                            // 補助員CD
                            uketsukeSKEntry.HOJOIN_CD = String.Empty;
                            // 補助員名
                            uketsukeSKEntry.HOJOIN_NAME = String.Empty;
                            // 削除フラグ
                            uketsukeSKEntry.DELETE_FLG = false;
                            // システム自動設定のプロパティを設定する
                            //WHOSKEntry.SetSystemProperty(uketsukeSKEntry, false);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            uketsukeSKEntry.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            uketsukeSKEntry.UPDATE_PC = SystemInformation.ComputerName;

                            this.daoUketsukeSKEntry.Insert(uketsukeSKEntry);

                            // 3-2-3.受付（出荷）明細テーブルの追加（コピーイメージ）
                            T_UKETSUKE_SK_DETAIL uketsukeSKDetailParam = new T_UKETSUKE_SK_DETAIL();
                            // システムID
                            uketsukeSKDetailParam.SYSTEM_ID = systemID;
                            // 枝番
                            uketsukeSKDetailParam.SEQ = seq;

                            // 受付（出荷）明細データ取得
                            T_UKETSUKE_SK_DETAIL[] uketsukeSKDetailList = daoUketsukeSKDetail.GetDataForEntity(uketsukeSKDetailParam);

                            if (uketsukeSKDetailList != null && uketsukeSKDetailList.Length > 0)
                            {
                                foreach (var skDetail in uketsukeSKDetailList)
                                {
                                    // システムID
                                    skDetail.SYSTEM_ID = uketsukeSKEntry.SYSTEM_ID;
                                    // 枝番
                                    skDetail.SEQ = uketsukeSKEntry.SEQ;
                                    // システム自動設定のプロパティを設定する
                                    //var WHOSKDetail = new DataBinderLogic<T_UKETSUKE_SK_DETAIL>(skDetail);
                                    //WHOSKDetail.SetSystemProperty(skDetail, false);
                                    // 更新日
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //skDetail.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                    skDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    // 更新者
                                    skDetail.UPDATE_USER = SystemProperty.UserName;
                                    // 更新PC
                                    skDetail.UPDATE_PC = SystemInformation.ComputerName;
                                    // 受付（出荷）明細テーブルに追加
                                    this.daoUketsukeSKDetail.Insert(skDetail);
                                }
                            }
                        }

                        break;

                    default:
                        break;
                }

                result = true;

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }
        #endregion

        #region 定期配車情報取り消し処理
        /// <summary>
        /// 定期配車情報取り消し処理
        /// </summary>
        /// <param name="dr"></param>
        private bool cancelTeikihaishaInfo(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);
            bool result = false;
            try
            {
                // エンティティ区分
                string entityKbn = dr["ENTITY_KBN"].ToString();
                // システムID
                int systemID = int.Parse(dr["SYSTEM_ID"].ToString());
                // 枝番
                int seq = int.Parse(dr["SEQ"].ToString());
                // コースCD
                string courseNameCD = dr["COURSE_NAME_CD"].ToString();
                // 業者CD
                string gyoushaCD = dr["GYOUSHA_CD"].ToString();
                // 現場CD
                string genbaCD = dr["GENBA_CD"].ToString();
                // 受付番号
                string uketsukeNumber = dr["UKETSUKE_NUMBER"].ToString();
                // 配車番号
                int haishaNumber = 0;
                if (DBNull.Value.Equals(dr["TEIKI_HAISHA_NUMBER"]))
                {
                    if (DBNull.Value.Equals(dr["TEIKI_HAISHA_NUMBER_BEFORE"]))
                    {
                        return false;
                    }
                    haishaNumber = int.Parse(dr["TEIKI_HAISHA_NUMBER_BEFORE"].ToString());
                }
                else
                {
                    haishaNumber = int.Parse(dr["TEIKI_HAISHA_NUMBER_BEFORE"].ToString());
                }

                // システムIDと枝番より、データ取得
                T_TEIKI_HAISHA_ENTRY teikihaishaEntry = getTeikihaishaEntryData(haishaNumber);

                if (teikihaishaEntry != null)
                {
                    seq = (int)teikihaishaEntry.SEQ;
                    // 4-1.定期配車入力テーブルの更新（論理削除）
                    var WHOTeikihaishaEntry = new DataBinderLogic<T_TEIKI_HAISHA_ENTRY>(teikihaishaEntry);
                    // システム自動設定のプロパティを設定する
                    //WHOTeikihaishaEntry.SetSystemProperty(teikihaishaEntry, true);
                    // 更新日
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //teikihaishaEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    teikihaishaEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    // 更新者
                    teikihaishaEntry.UPDATE_USER = SystemProperty.UserName;
                    // 更新PC
                    teikihaishaEntry.UPDATE_PC = SystemInformation.ComputerName;
                    // 削除フラグを設定
                    teikihaishaEntry.DELETE_FLG = true;
                    // データ更新
                    this.teikiHaishaDao.Update(teikihaishaEntry);

                    // 4-2.定期配車入力テーブルの追加
                    // 枝番+1
                    teikihaishaEntry.SEQ = seq + 1;
                    // 削除フラグ
                    teikihaishaEntry.DELETE_FLG = false;
                    // システム自動設定のプロパティを設定する
                    //WHOTeikihaishaEntry.SetSystemProperty(teikihaishaEntry, false);
                    // 更新日
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //teikihaishaEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    teikihaishaEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    // 更新者
                    teikihaishaEntry.UPDATE_USER = SystemProperty.UserName;
                    // 更新PC
                    teikihaishaEntry.UPDATE_PC = SystemInformation.ComputerName;
                    // 定期配車入力テーブルに追加
                    this.teikiHaishaDao.Insert(teikihaishaEntry);

                    // 4-3.定期配車荷降テーブルの追加（コピーイメージ）
                    T_TEIKI_HAISHA_NIOROSHI teikihaishaNioroshiParam = new T_TEIKI_HAISHA_NIOROSHI();
                    // 定期配車番号
                    teikihaishaNioroshiParam.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                    // システムID
                    teikihaishaNioroshiParam.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                    // 枝番
                    teikihaishaNioroshiParam.SEQ = seq;

                    T_TEIKI_HAISHA_NIOROSHI[] teikihaishaNioroshiList = teikiHaishaNioroshiDao.GetDataForEntity(teikihaishaNioroshiParam);

                    if (teikihaishaNioroshiList != null && teikihaishaNioroshiList.Length > 0)
                    {
                        foreach (var teikihaishaNioroshiDetail in teikihaishaNioroshiList)
                        {
                            // システムID
                            teikihaishaNioroshiDetail.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                            // 枝番
                            teikihaishaNioroshiDetail.SEQ = teikihaishaEntry.SEQ;
                            // システム自動設定のプロパティを設定する
                            //var WHOTHNDetail = new DataBinderLogic<T_TEIKI_HAISHA_NIOROSHI>(teikihaishaNioroshiDetail);
                            //WHOTHNDetail.SetSystemProperty(teikihaishaNioroshiDetail, false);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //teikihaishaNioroshiDetail.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            teikihaishaNioroshiDetail.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            teikihaishaNioroshiDetail.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            teikihaishaNioroshiDetail.UPDATE_PC = SystemInformation.ComputerName;
                            // 定期配車荷降テーブルに追加
                            this.teikiHaishaNioroshiDao.Insert(teikihaishaNioroshiDetail);
                        }
                    }

                    // 4-4.定期配車明細テーブルの追加（コピーイメージ）
                    T_TEIKI_HAISHA_DETAIL teikihaishaDetailParam = new T_TEIKI_HAISHA_DETAIL();
                    // 定期配車番号
                    teikihaishaDetailParam.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                    // システムID
                    teikihaishaDetailParam.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                    // 枝番
                    teikihaishaDetailParam.SEQ = seq;

                    T_TEIKI_HAISHA_DETAIL[] teikihaishaDetailList = teikiHaishaDetailDao.GetDataForEntity(teikihaishaDetailParam);

                    if (teikihaishaDetailList != null && teikihaishaDetailList.Length > 0)
                    {
                        foreach (var teikihaishaDetailItem in teikihaishaDetailList)
                        {
                            // 受付番号＝画面.受付番号は対象外とする。
                            if (!teikihaishaDetailItem.UKETSUKE_NUMBER.IsNull &&
                                int.Parse(uketsukeNumber) == teikihaishaDetailItem.UKETSUKE_NUMBER)
                            {
                                continue;
                            }
                            // システムID
                            teikihaishaDetailItem.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                            // 枝番
                            teikihaishaDetailItem.SEQ = teikihaishaEntry.SEQ;
                            // システム自動設定のプロパティを設定する
                            //var WHOTeikihaishaDetail = new DataBinderLogic<T_TEIKI_HAISHA_DETAIL>(teikihaishaDetailItem);
                            //WHOTeikihaishaDetail.SetSystemProperty(teikihaishaDetailItem, false);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //teikihaishaDetailItem.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            teikihaishaDetailItem.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            teikihaishaDetailItem.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            teikihaishaDetailItem.UPDATE_PC = SystemInformation.ComputerName;
                            // 定期配車明細テーブルに追加
                            this.teikiHaishaDetailDao.Insert(teikihaishaDetailItem);
                        }
                    }

                    // 4-5.定期配車詳細テーブルの追加（コピーイメージ）
                    T_TEIKI_HAISHA_SHOUSAI teikihaishaShousaiParam = new T_TEIKI_HAISHA_SHOUSAI();
                    // 定期配車番号
                    teikihaishaShousaiParam.TEIKI_HAISHA_NUMBER = teikihaishaEntry.TEIKI_HAISHA_NUMBER;
                    // システムID
                    teikihaishaShousaiParam.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                    // 枝番
                    teikihaishaShousaiParam.SEQ = seq;

                    var skipList = teikihaishaDetailList.Where(r => r.UKETSUKE_NUMBER.CompareTo(int.Parse(uketsukeNumber)) == 0).ToList();

                    T_TEIKI_HAISHA_SHOUSAI[] teikiHaishaShousaiList = teikiHaishaShousaiDao.GetDataForEntity(teikihaishaShousaiParam);
                    if (teikiHaishaShousaiList != null && teikiHaishaShousaiList.Length > 0)
                    {
                        foreach (var teikihaishaShousaiItem in teikiHaishaShousaiList)
                        {
                            // 受付番号＝画面.受付番号は対象外とする。
                            foreach (var skipItem in skipList)
                            {
                                if (skipItem.SYSTEM_ID.Equals(teikihaishaShousaiItem.SYSTEM_ID) &&
                                    skipItem.SEQ.Equals(teikihaishaShousaiItem.SEQ) &&
                                    skipItem.DETAIL_SYSTEM_ID.Equals(teikihaishaShousaiItem.DETAIL_SYSTEM_ID))
                                {
                                    continue;
                                }
                            }

                            var contains = skipList.Any(n => n.SYSTEM_ID.Equals(teikihaishaShousaiItem.SYSTEM_ID) &&
                                                             n.SEQ.Equals(teikihaishaShousaiItem.SEQ) &&
                                                             n.DETAIL_SYSTEM_ID.Equals(teikihaishaShousaiItem.DETAIL_SYSTEM_ID));
                            if (contains)
                            {
                                continue;
                            }

                            // システムID
                            teikihaishaShousaiItem.SYSTEM_ID = teikihaishaEntry.SYSTEM_ID;
                            // 枝番
                            teikihaishaShousaiItem.SEQ = teikihaishaEntry.SEQ;
                            // システム自動設定のプロパティを設定する
                            //var WHOTeikihaishaShousai = new DataBinderLogic<T_TEIKI_HAISHA_SHOUSAI>(teikihaishaShousaiItem);
                            //WHOTeikihaishaShousai.SetSystemProperty(teikihaishaShousaiItem, false);
                            // 更新日
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //teikihaishaShousaiItem.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            teikihaishaShousaiItem.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            // 更新者
                            teikihaishaShousaiItem.UPDATE_USER = SystemProperty.UserName;
                            // 更新PC
                            teikihaishaShousaiItem.UPDATE_PC = SystemInformation.ComputerName;
                            // 定期配車詳細テーブルに追加
                            this.teikiHaishaShousaiDao.Insert(teikihaishaShousaiItem);
                        }
                    }
                }

                result = true;

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }
        #endregion

        #region 受付収集検索
        /// <summary>
        /// 受付収集検索
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="seq"></param>
        private T_UKETSUKE_SS_ENTRY getUketsukeSSEntryData(int systemID)
        {
            T_UKETSUKE_SS_ENTRY returnValue = null;
            try
            {
                LogUtility.DebugMethodStart(systemID);

                T_UKETSUKE_SS_ENTRY uketsukeSSEntryParm = new T_UKETSUKE_SS_ENTRY();
                uketsukeSSEntryParm.SYSTEM_ID = systemID;

                T_UKETSUKE_SS_ENTRY[] uketsukeSSEntry = daoUketsukeSSEntry.GetDataForEntity(uketsukeSSEntryParm);

                if (uketsukeSSEntry != null)
                {
                    returnValue = uketsukeSSEntry[0];
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue);
            }

        }
        #endregion

        #region 受付出荷検索
        /// <summary>
        /// 受付出荷検索
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="seq"></param>
        private T_UKETSUKE_SK_ENTRY getUketsukeSKEntryData(int systemID)
        {
            T_UKETSUKE_SK_ENTRY returnValue = null;
            try
            {
                LogUtility.DebugMethodStart(systemID);

                T_UKETSUKE_SK_ENTRY uketsukeSKEntryParm = new T_UKETSUKE_SK_ENTRY();
                uketsukeSKEntryParm.SYSTEM_ID = systemID;

                T_UKETSUKE_SK_ENTRY[] uketsukeSKEntry = daoUketsukeSKEntry.GetDataForEntity(uketsukeSKEntryParm);

                if (uketsukeSKEntry != null)
                {
                    returnValue = uketsukeSKEntry[0];
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue);
            }
        }
        #endregion

        #region 定期配車検索
        /// <summary>
        /// 定期配車検索
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="seq"></param>
        private T_TEIKI_HAISHA_ENTRY getTeikihaishaEntryData(int haishaNumber)
        {
            T_TEIKI_HAISHA_ENTRY returnValue = null;
            try
            {
                LogUtility.DebugMethodStart(haishaNumber);

                T_TEIKI_HAISHA_ENTRY teikihaishaEntryParm = new T_TEIKI_HAISHA_ENTRY();

                teikihaishaEntryParm.TEIKI_HAISHA_NUMBER = haishaNumber;

                T_TEIKI_HAISHA_ENTRY[] teikihaishaEntry = teikiHaishaDao.GetAllValidData(teikihaishaEntryParm);

                if (teikihaishaEntry != null)
                {
                    returnValue = teikihaishaEntry[0];
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue);
            }
        }
        #endregion

        #region SYSTEM_IDを採番
        /// <summary>
        /// SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        private SqlInt64 createSystemIdForUketsuke()
        {
            SqlInt64 returnVal = 1;

            try
            {
                LogUtility.DebugMethodStart();

                var entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.TEIKI_HAISHA.GetHashCode();

                // IS_NUMBER_SYSTEMDao(共通)
                IS_NUMBER_SYSTEMDao numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

                var updateEntity = numberSystemDao.GetNumberSystemData(entity);
                returnVal = numberSystemDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_SYSTEM();
                    updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UKETSUKE.GetHashCode();
                    updateEntity.CURRENT_NUMBER = returnVal;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    numberSystemDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnVal;
                    numberSystemDao.Update(updateEntity);
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        /// 20140930 Houkakou 「コース配車依頼入力」の休動を追加する　start
        #region 車輌と運転者の休動チェック
        private bool checkClosedDate(DataRow dr)
        {
            // エンティティ区分
            string entityKbn = dr["ENTITY_KBN"].ToString();
            // システムID
            int systemID = Convert.ToInt32(dr["SYSTEM_ID"].ToString());

            // 配車番号
            int haishaNumber = 0;
            if (!string.IsNullOrEmpty(dr["TEIKI_HAISHA_NUMBER"].ToString()))
            {
                haishaNumber = Convert.ToInt32(dr["TEIKI_HAISHA_NUMBER"].ToString());
            }
            else
            {
                return true;
            }

            // 配車番号と枝番より、データ取得
            T_TEIKI_HAISHA_ENTRY teikihaishaEntry = getTeikihaishaEntryData(haishaNumber);

            // システムIDと枝番より、データ取得
            T_UKETSUKE_SS_ENTRY uketsukeSSEntry = new T_UKETSUKE_SS_ENTRY();

            // システムIDと枝番より、データ取得
            T_UKETSUKE_SK_ENTRY uketsukeSKEntry = new T_UKETSUKE_SK_ENTRY();

            M_WORK_CLOSED_SHARYOU workclosedsharyouEntry = new M_WORK_CLOSED_SHARYOU();
            M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();

            //業者CD取得
            workclosedsharyouEntry.GYOUSHA_CD = teikihaishaEntry.UNPAN_GYOUSHA_CD;
            //車輌CD取得
            workclosedsharyouEntry.SHARYOU_CD = teikihaishaEntry.SHARYOU_CD;
            //社員CD取得
            workcloseduntenshaEntry.SHAIN_CD = teikihaishaEntry.UNTENSHA_CD;

            //受付収集
            if (entityKbn == "1")
            {
                //休動日取得
                uketsukeSSEntry = getUketsukeSSEntryData(systemID);
                if (string.IsNullOrEmpty(uketsukeSSEntry.SAGYOU_DATE))
                {
                    workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(dr["SAGYOU_DATE"]);
                    workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(dr["SAGYOU_DATE"]);
                }
                else
                {
                    workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(uketsukeSSEntry.SAGYOU_DATE);
                    workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(uketsukeSSEntry.SAGYOU_DATE);
                }
            }
            //受付出荷
            else if (entityKbn == "2")
            {
                ////休動日取得
                uketsukeSKEntry = getUketsukeSKEntryData(systemID);
                workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(uketsukeSKEntry.SAGYOU_DATE);
                workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(uketsukeSKEntry.SAGYOU_DATE);
            }

            M_WORK_CLOSED_SHARYOU[] workclosedsharyouList = workclosedsharyouDao.GetAllValidData(workclosedsharyouEntry);

            //取得テータ
            if (workclosedsharyouList.Count() >= 1)
            {
                msgLogic.MessageBoxShow("E207", new string[] { dr["TEIKI_HAISHA_NUMBER"].ToString(), "車輌", "作業日：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd") });

                return false;
            }


            M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);

            //取得テータ
            if (workcloseduntenshaList.Count() >= 1)
            {
                msgLogic.MessageBoxShow("E207", new string[] { dr["TEIKI_HAISHA_NUMBER"].ToString(), "運転者", "作業日：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd") });
                return false;
            }

            return true;
        }

        #endregion
        /// 20140930 Houkakou 「コース配車依頼入力」の休動を追加する　end

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

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.SAGYOU_DATE_BEGIN.BackColor = Constans.NOMAL_COLOR;
            this.form.SAGYOU_DATE_END.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE_BEGIN.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE_END.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.SAGYOU_DATE_BEGIN.Text);
            DateTime date_to = DateTime.Parse(this.form.SAGYOU_DATE_END.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.SAGYOU_DATE_BEGIN.IsInputErrorOccured = true;
                this.form.SAGYOU_DATE_END.IsInputErrorOccured = true;
                this.form.SAGYOU_DATE_BEGIN.BackColor = Constans.ERROR_COLOR;
                this.form.SAGYOU_DATE_END.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "作業日From", "作業日To" };
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                msglogic.MessageBoxShow("E030", errorMsg);
                this.form.SAGYOU_DATE_BEGIN.Focus();
                return true;
            }
            return false;
        }
        #endregion
        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141128 teikyou ダブルクリックを追加する　start
        private void SAGYOU_DATE_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var sagyouDateBeginTextBox = this.form.SAGYOU_DATE_BEGIN;
            var sagyouDateEndTextBox = this.form.SAGYOU_DATE_END;
            sagyouDateEndTextBox.Text = sagyouDateBeginTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141128 teikyou ブルクリックを追加する　end
        #endregion

        /// <summary>
        /// [受付伝票]と[定期配車伝票]の作業日に差異のある行番号のListを取得します
        /// </summary>
        /// <param name="differenceRowIndexList">
        /// [受付伝票]と[定期配車伝票]の作業日に差異のある行番号のList
        /// </param>
        internal void GetSagyouDateDeffResult(out List<int> differenceRowIndexList)
        {
            // 初期化
            differenceRowIndexList = new List<int>();

            // 変更された行がある場合
            if (this.haishaNumberChangedRowList.Count > 0)
            {
                // 作業日の差異をチェック
                foreach (DataGridViewRow row in this.haishaNumberChangedRowList)
                {
                    // なぜかインデックスがマイナスのデータができてしまうときがある為
                    if (row.Index < 0) continue;

                    // 新規行と作業日が無い行はとばす
                    if (row.IsNewRow || !(row.Cells["SAGYOU_DATE"].Value is DateTime)) continue;

                    // [受付伝票]と[定期配車伝票]の作業日に差異のある行番号のListを生成
                    if (row.Cells["TEIKI_HAISHA_NUMBER"].Value != null
                        && !string.IsNullOrEmpty(row.Cells["TEIKI_HAISHA_NUMBER"].Value.ToString()))
                    {
                        bool catchErr = false;
                        string haishaNmuber = Convert.ToString(row.Cells["TEIKI_HAISHA_NUMBER"].Value);
                        string date = Convert.ToString(row.Cells["SAGYOU_DATE"].Value);
                        var mTHaisha = this.getHaishaNumberInfo(haishaNmuber, date, out catchErr);
                        if (catchErr) { return; }

                        var sagyouDate = Convert.ToDateTime(row.Cells["SAGYOU_DATE"].Value);
                        if (mTHaisha != null && mTHaisha.SAGYOU_DATE.Value != sagyouDate)
                        {

                            differenceRowIndexList.Add(row.Index);
                        }
                    }
                }
            }
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        public void CheckPopup(KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            if (e.KeyCode == Keys.Space)
            {
                if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("TEIKI_HAISHA_NUMBER"))
                {
                    DataGridViewRow row = this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex];

                    string Ukenumber = row.Cells["UKETSUKE_NUMBER"].Value.ToString();
                    string Teikinumber = row.Cells["TEIKI_HAISHA_NUMBER"].Value.ToString();
                    string TeikiBefnumber = row.Cells["TEIKI_HAISHA_NUMBER_BEFORE"].Value.ToString();
                    string GenbaCd = row.Cells["GENBA_CD"].Value.ToString();

                    //組込済の定期伝票の状態を確かめる
                    if (!string.IsNullOrEmpty(TeikiBefnumber))
                    {
                        if (RenkeiCheck(5, Ukenumber))
                        {
                            //組込済の定期データがモバイル連携済み
                            row.Cells["TEIKI_HAISHA_NUMBER"].Value = TeikiBefnumber;
                            this.form.msgLogic.MessageBoxShowError("既にモバイル将軍へ連携されている為、変更出来ません。");
                            return;
                        }
                        if (RenkeiCheck(3, TeikiBefnumber))
                        {
                            //変更前データがロジコン連携済み
                            row.Cells["TEIKI_HAISHA_NUMBER"].Value = TeikiBefnumber;
                            this.form.msgLogic.MessageBoxShowError("ロジこんぱす連携中の為、変更する事は出来ません。");
                            return;
                        }
                        if (RenkeiCheck(4, TeikiBefnumber))
                        {
                            //変更前データがNAVITIME連携済み
                            row.Cells["TEIKI_HAISHA_NUMBER"].Value = TeikiBefnumber;
                            this.form.msgLogic.MessageBoxShowError("NAVITIME連携中の為、変更する事は出来ません。");
                            return;
                        }
                    }

                    // 明細行の現場CDが未入力の場合、エラーとする。
                    if (string.IsNullOrEmpty(GenbaCd))
                    {
                        row.Cells["TEIKI_HAISHA_NUMBER"].Value = string.Empty;
                        this.form.msgLogic.MessageBoxShowError("現場が未入力のデータの為、組み込み出来ません。\n\n確認してください。");
                        return;
                    }

                    PopupDTOCls dto = new PopupDTOCls();
                    dto.KYOTEN_CD = this.form.HEADER_KYOTEN_CD.Text;
                    dto.SAGYOU_DATE = Convert.ToString(row.Cells["SAGYOU_DATE"].Value);
                    DateTime date = DateTime.Now;
                    if (DateTime.TryParse(dto.SAGYOU_DATE, out date))
                    {
                        dto.DAY_CD = DateUtility.GetShougunDayOfWeek(date).ToString();
                    }
                    PopupForm form = new PopupForm(dto);
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {

                        if (RenkeiCheck(3, form.ReturnParams[0][0].Value.ToString()))
                        {
                            //変更後のロジコン連携済み
                            row.Cells["TEIKI_HAISHA_NUMBER"].Value = TeikiBefnumber;
                            this.form.msgLogic.MessageBoxShowError("ロジこんぱす連携されているの為、指定する事は出来ません。");
                            return;
                        }
                        if (RenkeiCheck(4, form.ReturnParams[0][0].Value.ToString()))
                        {
                            //変更後のNAVITIME連携済み
                            row.Cells["TEIKI_HAISHA_NUMBER"].Value = TeikiBefnumber;
                            this.form.msgLogic.MessageBoxShowError("NAVITIME連携されているの為、指定する事は出来ません。");
                            return;
                        }

                        if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("TEIKI_HAISHA_NUMBER"))
                        {
                            this.form.Ichiran.EditingControl.Text = Convert.ToString(form.ReturnParams[0][0].Value);
                            this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["TEIKI_HAISHA_NUMBER"].Value = form.ReturnParams[0][0].Value;
                            if (string.IsNullOrEmpty(Convert.ToString(form.ReturnParams[1][0].Value)))
                            {
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["COURSE_NAME_CD"].Value = string.Empty;
                            }
                            else
                            {
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["COURSE_NAME_CD"].Value = Convert.ToString(form.ReturnParams[1][0].Value).PadLeft(6, '0');
                            }
                        }
                        else
                        {
                            this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["TEIKI_HAISHA_NUMBER"].Value = Convert.ToString(form.ReturnParams[0][0].Value);
                            if (string.IsNullOrEmpty(Convert.ToString(form.ReturnParams[1][0].Value)))
                            {
                                this.form.Ichiran.EditingControl.Text = string.Empty;
                            }
                            else
                            {
                                this.form.Ichiran.EditingControl.Text = Convert.ToString(form.ReturnParams[1][0].Value).PadLeft(6, '0');
                            }
                        }
                        this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["COURSE_NAME"].Value = Convert.ToString(form.ReturnParams[2][0].Value);

                        if (string.IsNullOrEmpty(Convert.ToString(row.Cells["SAGYOU_DATE_OLD"].Value)))
                        {
                            string selectStr = "SELECT SAGYOU_DATE FROM T_TEIKI_HAISHA_ENTRY WHERE TEIKI_HAISHA_NUMBER = " + row.Cells["TEIKI_HAISHA_NUMBER"].Value + " AND DELETE_FLG = 0";
                            DataTable dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                            if (dt.Rows.Count > 0)
                            {
                                row.Cells["SAGYOU_DATE"].Value = dt.Rows[0][0].ToString();
                            }
                        }

                    }
                }
                else if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("COURSE_NAME_CD"))
                {
                    DataGridViewRow row = this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex];

                    string GenbaCd = row.Cells["GENBA_CD"].Value.ToString();
                    // 明細行の現場CDが未入力の場合、エラーとする。
                    if (string.IsNullOrEmpty(GenbaCd))
                    {
                        row.Cells["TEIKI_HAISHA_NUMBER"].Value = string.Empty;
                        this.form.msgLogic.MessageBoxShowError("現場が未入力のデータの為、組み込み出来ません。\n\n確認してください。");
                        return;
                    }

                    PopupDTOCls dto = new PopupDTOCls();
                    dto.KYOTEN_CD = this.form.HEADER_KYOTEN_CD.Text;
                    dto.SAGYOU_DATE = Convert.ToString(row.Cells["SAGYOU_DATE"].Value);
                    dto.TEIKI_HAISHA_NUMBER = Convert.ToString(row.Cells["TEIKI_HAISHA_NUMBER"].Value);
                    dto.courseOnly = true;
                    DateTime date = DateTime.Now;
                    if (DateTime.TryParse(dto.SAGYOU_DATE, out date))
                    {
                        dto.DAY_CD = DateUtility.GetShougunDayOfWeek(date).ToString();
                    }
                    PopupForm form = new PopupForm(dto);
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(form.ReturnParams[0][0].Value)))
                        {
                            this.form.Ichiran.EditingControl.Text = string.Empty;
                        }
                        else
                        {
                            this.form.Ichiran.EditingControl.Text = Convert.ToString(form.ReturnParams[0][0].Value).PadLeft(6, '0');
                            this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["COURSE_NAME_CD"].Value = Convert.ToString(form.ReturnParams[0][0].Value).PadLeft(6, '0');
                        }
                        this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["COURSE_NAME"].Value = Convert.ToString(form.ReturnParams[1][0].Value);
                    }
                }
            }
        }

        /// <summary>
        /// KeyDown時のチェック
        /// </summary>
        /// <param name="e"></param>
        public void CheckKeyDown(KeyEventArgs e)
        {
            // 配車番号
            if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("TEIKI_HAISHA_NUMBER"))
            {
                // 数字のキーのみチェックする。
                if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                    (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                    e.KeyCode == Keys.Decimal)
                {
                    DataGridViewRow row = this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex];
                    string GenbaCd = row.Cells["GENBA_CD"].Value.ToString();

                    // 明細行の現場CDが未入力の場合、エラーとする。
                    if (string.IsNullOrEmpty(GenbaCd))
                    {
                        row.Cells["COURSE_NAME_CD"].Selected = true;
                        row.Cells["TEIKI_HAISHA_NUMBER"].Value = string.Empty;
                        row.Cells["TEIKI_HAISHA_NUMBER"].Selected = true;
                        this.form.msgLogic.MessageBoxShowError("現場が未入力のデータの為、組み込み出来ません。\n\n確認してください。");
                        return;
                    }
                }
            }
            // コースCD
            else if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("COURSE_NAME_CD"))
            {
                // 英数字のキーのみチェックする。
                if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                    (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                    e.KeyCode == Keys.Decimal ||
                    (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z))
                {
                    DataGridViewRow row = this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex];
                    string GenbaCd = row.Cells["GENBA_CD"].Value.ToString();

                    // 明細行の現場CDが未入力の場合、エラーとする。
                    if (string.IsNullOrEmpty(GenbaCd))
                    {
                        row.Cells["TEIKI_HAISHA_NUMBER"].Selected = true;
                        row.Cells["COURSE_NAME_CD"].Value = string.Empty;
                        row.Cells["COURSE_NAME_CD"].Selected = true;
                        this.form.msgLogic.MessageBoxShowError("現場が未入力のデータの為、組み込み出来ません。\n\n確認してください。");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// TextChanged時のチェック
        /// </summary>
        public void CheckTextChanged(object sender)
        {
            // 一覧が０件の場合は処理中断
            if (this.form.Ichiran.RowCount == 0)
            {
                return;
            }

            DataGridViewRow row = this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex];
            string GenbaCd = row.Cells["GENBA_CD"].Value.ToString();

            if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("TEIKI_HAISHA_NUMBER"))
            {
                var enteredText = (sender as TextBox).Text;

                if (string.IsNullOrEmpty(GenbaCd) && !string.IsNullOrEmpty(enteredText))
                {
                    row.Cells["TEIKI_HAISHA_NUMBER"].Value = string.Empty;
                    this.form.msgLogic.MessageBoxShowError("現場が未入力のデータの為、組み込み出来ません。\n\n確認してください。");
                    return;
                }
            }
            else if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.ColumnIndex].Name.Equals("COURSE_NAME_CD"))
            {
                var enteredText = (sender as TextBox).Text;

                if (string.IsNullOrEmpty(GenbaCd) && !string.IsNullOrEmpty(enteredText))
                {
                    row.Cells["TEIKI_HAISHA_NUMBER"].Value = string.Empty;
                    row.Cells["COURSE_NAME_CD"].Value = string.Empty;
                    this.form.msgLogic.MessageBoxShowError("現場が未入力のデータの為、組み込み出来ません。\n\n確認してください。");
                    return;
                }
            }
        }

        /// <summary>
        /// アルファベットを判定する。
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsAlphabet(char c)
        {
            return (c >= 'A' && c <= 'z') ? true : false;
        }

        public void Ichiran_CellValidating(DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewRow row = this.form.Ichiran.Rows[e.RowIndex];
            PopupDTOCls dto = new PopupDTOCls();
            DateTime date = DateTime.Now;
            DataTable dt = new DataTable();
            bool catchErr = false;
            switch (this.form.Ichiran.Columns[e.ColumnIndex].Name)
            {
                case ("TEIKI_HAISHA_NUMBER"):

                    string haishaNo = Convert.ToString(row.Cells["TEIKI_HAISHA_NUMBER"].Value);
                    string courseCd = Convert.ToString(row.Cells["COURSE_NAME_CD"].Value);
                    string sagyouDate = Convert.ToString(row.Cells["SAGYOU_DATE"].Value);
                    string haishaNoBef = Convert.ToString(row.Cells["TEIKI_HAISHA_NUMBER_BEFORE"].Value);
                    string uketsukeNo = Convert.ToString(row.Cells["UKETSUKE_NUMBER"].Value);

                    //組込済の定期伝票の状態を確かめる
                    if (haishaNo != this.beforeHaishaNumber)
                    {
                        row.Cells["SAGYOU_DATE"].Value = row.Cells["SAGYOU_DATE_OLD"].Value;
                        row.Cells["MOBILE_RENKEI"].Value = false;
                        if (!string.IsNullOrEmpty(haishaNoBef))
                        {
                            if (RenkeiCheck(5, uketsukeNo))
                            {
                                //組込済の定期データがモバイル連携済み
                                row.Cells["TEIKI_HAISHA_NUMBER"].Value = haishaNoBef;
                                this.form.msgLogic.MessageBoxShowError("既にモバイル将軍へ連携されている為、変更出来ません。");
                                return;
                            }
                            if (RenkeiCheck(3, haishaNoBef))
                            {
                                //変更前データがロジコン連携済み
                                row.Cells["TEIKI_HAISHA_NUMBER"].Value = haishaNoBef;
                                this.form.msgLogic.MessageBoxShowError("ロジこんぱす連携中の為、変更する事は出来ません。");
                                return;
                            }
                            if (RenkeiCheck(4, haishaNoBef))
                            {
                                //変更前データがNAVITIME連携済み
                                row.Cells["TEIKI_HAISHA_NUMBER"].Value = haishaNoBef;
                                this.form.msgLogic.MessageBoxShowError("NAVITIME連携中の為、変更する事は出来ません。");
                                return;
                            }
                        }
                    }
                    
                    if (!string.IsNullOrEmpty(haishaNo))
                    {
                        if (haishaNo != this.beforeHaishaNumber)
                        {
                            if (RenkeiCheck(3, haishaNo))
                            {
                                //変更後のロジコン連携済み
                                row.Cells["TEIKI_HAISHA_NUMBER"].Value = haishaNoBef;
                                this.form.msgLogic.MessageBoxShowError("ロジこんぱす連携されているの為、指定する事は出来ません。");
                                e.Cancel = true;
                                this.isInputError = true;
                                return;
                            }
                            if (RenkeiCheck(4, haishaNo))
                            {
                                //変更後のNAVITIME連携済み
                                row.Cells["TEIKI_HAISHA_NUMBER"].Value = haishaNoBef;
                                this.form.msgLogic.MessageBoxShowError("NAVITIME連携されているの為、指定する事は出来ません。");
                                e.Cancel = true;
                                this.isInputError = true;
                                return;
                            }
                        }
                        var haishaNumberEntity = this.getHaishaNumberInfo(haishaNo, sagyouDate, out catchErr);
                        if (catchErr) { return; }

                        if (haishaNumberEntity == null)
                        {
                            var iText = row.Cells[e.ColumnIndex] as ICustomTextBox;
                            iText.IsInputErrorOccured = true;
                            this.msgLogic.MessageBoxShow("E076");
                            e.Cancel = true;
                            this.form.Ichiran.BeginEdit(false);
                            return;
                        }

                        if (!string.IsNullOrEmpty(courseCd))
                        {
                            dto = new PopupDTOCls();
                            dto.KYOTEN_CD = this.form.HEADER_KYOTEN_CD.Text;
                            dto.SAGYOU_DATE = Convert.ToString(row.Cells["SAGYOU_DATE_OLD"].Value);
                            date = DateTime.Now;
                            if (DateTime.TryParse(dto.SAGYOU_DATE, out date))
                            {
                                dto.DAY_CD = DateUtility.GetShougunDayOfWeek(date).ToString();
                            }
                            dto.TEIKI_HAISHA_NUMBER = haishaNo;
                            dto.COURSE_NAME_CD = courseCd;
                            dt = this.popupDao.GetPopupData(dto);
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                row.Cells["TEIKI_HAISHA_NUMBER"].Style.BackColor = Constans.ERROR_COLOR;
                                this.form.msgLogic.MessageBoxShowError("選択されたコースの定期配車が存在しませんでした。");
                                e.Cancel = true;
                                this.isInputError = true;
                            }
                            else
                            {
                                row.Cells["TEIKI_HAISHA_NUMBER"].Value = dt.Rows[0][0].ToString();
                                row.Cells["COURSE_NAME_CD"].Value = dt.Rows[0][1].ToString();
                                row.Cells["COURSE_NAME"].Value = dt.Rows[0][2].ToString();
                            }
                        }
                        else
                        {
                            var courseNameEntity = this.getCourseNameInfo(haishaNumberEntity.COURSE_NAME_CD, out catchErr);
                            if (catchErr) { return; }
                            if (courseNameEntity != null)
                            {
                                // コースCD
                                this.form.Ichiran.Rows[e.RowIndex].Cells["COURSE_NAME_CD"].Value = courseNameEntity.COURSE_NAME_CD;
                                // コース名
                                this.form.Ichiran.Rows[e.RowIndex].Cells["COURSE_NAME"].Value = courseNameEntity.COURSE_NAME;
                            }
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(row.Cells["SAGYOU_DATE_OLD"].Value)))
                        {
                            string selectStr = "SELECT SAGYOU_DATE FROM T_TEIKI_HAISHA_ENTRY WHERE TEIKI_HAISHA_NUMBER = " + row.Cells["TEIKI_HAISHA_NUMBER"].Value + " AND DELETE_FLG = 0";
                            dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                            if (dt.Rows.Count > 0)
                            {
                                row.Cells["SAGYOU_DATE"].Value = dt.Rows[0][0].ToString();
                            }
                        }
                        ///////////////////
                        if (this.is_mobile)
                        {
                            if ((bool)row.Cells["MOBILE_RENKEI"].Value)
                            {
                                //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                                if (!string.IsNullOrEmpty(row.Cells["SAGYOU_DATE"].Value.ToString()))
                                {
                                    if (!(DateTime.Parse(row.Cells["SAGYOU_DATE"].Value.ToString()).ToString("yyyy/MM/dd")).Equals(DateTime.Now.ToString("yyyy/MM/dd")))
                                    {
                                        this.msgLogic.MessageBoxShowInformation("作業日が当日の場合のみモバイル連携が可能です。\r\n明細のモバイル連携のチェックはクリアされます。");
                                        row.Cells["MOBILE_RENKEI"].Value = false;
                                    }
                                }
                            }
                        }
                        ////////////////
                    }
                    else
                    {
                        // コースCD
                        this.form.Ichiran.Rows[e.RowIndex].Cells["COURSE_NAME_CD"].Value = string.Empty;
                        // コース名
                        this.form.Ichiran.Rows[e.RowIndex].Cells["COURSE_NAME"].Value = string.Empty;
                        return;
                    }

                    // 前回値と値が異なる場合
                    if (haishaNo != this.beforeHaishaNumber)
                    {
                        // Listに既に存在するかチェック
                        // あれば消してから追加（上書き）
                        var sysId = row.Cells["SYSTEM_ID"].Value;
                        var sysColumnIndex = row.Cells["SYSTEM_ID"].ColumnIndex;
                        if (!sysId.Equals(DBNull.Value) && sysId != null)
                        {
                            for (int i = 0; this.haishaNumberChangedRowList.Count > i; i++)
                            {
                                DataGridViewCell sysRow = this.haishaNumberChangedRowList[i].Cells[sysColumnIndex];
                                if (sysRow != null && sysRow.Value != null && sysRow.Value.Equals(sysId))
                                {
                                    this.haishaNumberChangedRowList.RemoveAt(i);
                                    break;
                                }
                            }
                        }

                        if (this.form.Ichiran.Rows[e.RowIndex].Index >= 0)
                        {
                            // 配車番号に変更があった行をListに追加
                            this.haishaNumberChangedRowList.Add(this.form.Ichiran.Rows[e.RowIndex]);
                        }
                    }
                    break;
                case ("COURSE_NAME_CD"):
                    string uketsukeCNo = Convert.ToString(row.Cells["UKETSUKE_NUMBER"].Value);
                    string haishaNoCBef = Convert.ToString(row.Cells["TEIKI_HAISHA_NUMBER_BEFORE"].Value);
                    string courseNameCd = Convert.ToString(row.Cells["COURSE_NAME_CD"].Value);
                    string courseCdBef = Convert.ToString(row.Cells["COURSE_NAME_CD_BEFORE"].Value);

                    if (courseNameCd == this.beforeCd && !this.isInputError)
                    {
                        return;
                    }
                    row.Cells["SAGYOU_DATE"].Value = row.Cells["SAGYOU_DATE_OLD"].Value;
                    row.Cells["MOBILE_RENKEI"].Value = false;

                    this.isInputError = false;
                    row.Cells["COURSE_NAME_CD"].Value = courseNameCd.ToUpper();

                    //コースCD変更前確認(配車番号削除と同等)
                    //組込済の定期伝票の状態を確かめる
                    if (!string.IsNullOrEmpty(haishaNoCBef))
                    {
                        if (RenkeiCheck(5, uketsukeCNo))
                        {
                            //組込済の定期データがモバイル連携済み
                            row.Cells["COURSE_NAME_CD"].Value = courseCdBef;
                            this.form.msgLogic.MessageBoxShowError("既にモバイル将軍へ連携されている為、変更出来ません。");
                            return;
                        }
                        if (RenkeiCheck(3, haishaNoCBef))
                        {
                            //変更前データがロジコン連携済み
                            row.Cells["COURSE_NAME_CD"].Value = courseCdBef;
                            this.form.msgLogic.MessageBoxShowError("ロジこんぱす連携中の為、変更する事は出来ません。");
                            return;
                        }
                        if (RenkeiCheck(4, haishaNoCBef))
                        {
                            //変更前データがNAVITIME連携済み
                            row.Cells["COURSE_NAME_CD"].Value = courseCdBef;
                            this.form.msgLogic.MessageBoxShowError("NAVITIME連携中の為、変更する事は出来ません。");
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(courseNameCd))
                    {
                        row.Cells["TEIKI_HAISHA_NUMBER"].Value = DBNull.Value;
                        row.Cells["COURSE_NAME"].Value = string.Empty;
                        return;
                    }

                    dto = new PopupDTOCls();
                    dto.KYOTEN_CD = this.form.HEADER_KYOTEN_CD.Text;
                    dto.SAGYOU_DATE = Convert.ToString(row.Cells["SAGYOU_DATE_OLD"].Value);
                    date = DateTime.Now;
                    if (DateTime.TryParse(dto.SAGYOU_DATE, out date))
                    {
                        dto.DAY_CD = DateUtility.GetShougunDayOfWeek(date).ToString();
                    }
                    dto.TEIKI_HAISHA_NUMBER = Convert.ToString(row.Cells["TEIKI_HAISHA_NUMBER"].Value);
                    dto.COURSE_NAME_CD = Convert.ToString(row.Cells["COURSE_NAME_CD"].Value);
                    dt = this.popupDao.GetPopupData(dto);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        row.Cells["COURSE_NAME_CD"].Style.BackColor = Constans.ERROR_COLOR;
                        row.Cells["COURSE_NAME"].Value = string.Empty;
                        this.form.msgLogic.MessageBoxShowError("選択されたコースの定期配車が存在しませんでした。");
                        e.Cancel = true;
                        this.isInputError = true;
                    }
                    else if (dt.Rows.Count == 1)
                    {
                        if (RenkeiCheck(3, dt.Rows[0][0].ToString()))
                        {
                            //変更後のロジコン連携済み
                            row.Cells["TEIKI_HAISHA_NUMBER"].Value = haishaNoCBef;
                            this.form.msgLogic.MessageBoxShowError("ロジこんぱす連携されているの為、指定する事は出来ません。");
                            e.Cancel = true;
                            this.isInputError = true;
                            return;
                        }
                        if (RenkeiCheck(4, dt.Rows[0][0].ToString()))
                        {
                            //変更後のNAVITIME連携済み
                            row.Cells["TEIKI_HAISHA_NUMBER"].Value = haishaNoCBef;
                            this.form.msgLogic.MessageBoxShowError("NAVITIME連携されているの為、指定する事は出来ません。");
                            e.Cancel = true;
                            this.isInputError = true;
                            return;
                        }
                        row.Cells["TEIKI_HAISHA_NUMBER"].Value = dt.Rows[0][0].ToString();
                        row.Cells["COURSE_NAME_CD"].Value = dt.Rows[0][1].ToString();
                        row.Cells["COURSE_NAME"].Value = dt.Rows[0][2].ToString();

                        if (string.IsNullOrEmpty(Convert.ToString(row.Cells["SAGYOU_DATE_OLD"].Value)))
                        {
                            string selectStr = "SELECT SAGYOU_DATE FROM T_TEIKI_HAISHA_ENTRY WHERE TEIKI_HAISHA_NUMBER = " + row.Cells["TEIKI_HAISHA_NUMBER"].Value + " AND DELETE_FLG = 0";
                            dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                            if (dt.Rows.Count > 0)
                            {
                                row.Cells["SAGYOU_DATE"].Value = dt.Rows[0][0].ToString();
                            }
                        }
                    }
                    else
                    {
                        row.Cells["COURSE_NAME_CD"].Value = dt.Rows[0][1].ToString();
                        row.Cells["COURSE_NAME"].Value = dt.Rows[0][2].ToString();
                        PopupForm form = new PopupForm(dto);
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.ShowDialog();
                        if (form.ReturnParams != null)
                        {
                            if (RenkeiCheck(3, dt.Rows[0][0].ToString()))
                            {
                                //変更後のロジコン連携済み
                                row.Cells["TEIKI_HAISHA_NUMBER"].Value = haishaNoCBef;
                                this.form.msgLogic.MessageBoxShowError("ロジこんぱす連携されているの為、指定する事は出来ません。");
                                e.Cancel = true;
                                this.isInputError = true;
                                return;
                            }
                            if (RenkeiCheck(4, dt.Rows[0][0].ToString()))
                            {
                                //変更後のNAVITIME連携済み
                                row.Cells["TEIKI_HAISHA_NUMBER"].Value = haishaNoCBef;
                                this.form.msgLogic.MessageBoxShowError("NAVITIME連携されているの為、指定する事は出来ません。");
                                e.Cancel = true;
                                this.isInputError = true;
                                return;
                            }
                            this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["TEIKI_HAISHA_NUMBER"].Value = form.ReturnParams[0][0].Value.ToString();
                            if (string.IsNullOrEmpty(Convert.ToString(form.ReturnParams[1][0].Value)))
                            {
                                this.form.Ichiran.EditingControl.Text = string.Empty;
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["COURSE_NAME"].Value = string.Empty;
                            }
                            else
                            {
                                this.form.Ichiran.EditingControl.Text = form.ReturnParams[1][0].Value.ToString().PadLeft(6, '0');
                                this.form.Ichiran.Rows[this.form.Ichiran.CurrentCell.RowIndex].Cells["COURSE_NAME"].Value = form.ReturnParams[2][0].Value.ToString();
                            }


                            if (string.IsNullOrEmpty(Convert.ToString(row.Cells["SAGYOU_DATE_OLD"].Value)))
                            {
                                string selectStr = "SELECT SAGYOU_DATE FROM T_TEIKI_HAISHA_ENTRY WHERE TEIKI_HAISHA_NUMBER = " + row.Cells["TEIKI_HAISHA_NUMBER"].Value + " AND DELETE_FLG = 0";
                                dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                                if (dt.Rows.Count > 0)
                                {
                                    row.Cells["SAGYOU_DATE"].Value = dt.Rows[0][0].ToString();
                                }
                            }

                        }
                        else
                        {
                            e.Cancel = true;
                            this.isInputError = true;
                        }
                    }
                    break;
            }
        }

        #region 連携処理

        /// <summary>
        /// mapbox表示用Dto作成
        /// </summary>
        /// <returns></returns>
        private List<mapDtoList> createMapboxDto()
        {
            try
            {
                int layerId = 0;

                List<mapDtoList> dtoLists = new List<mapDtoList>();

                //layerId++;

                // レイヤー追加
                mapDtoList dtoList = new mapDtoList();
                dtoList.layerId = layerId;

                List<mapDto> dtos = new List<mapDto>();

                StringBuilder sb = new StringBuilder();
                DataTable dt = null;

                // 地図出力に必要な情報を収集
                #region 明細1件のコースの内容を取得する

                #region 受付伝票の情報

                bool kumikomiChk = true;
                if (this.form.Ichiran.CurrentRow.Cells["COURSE_KUMIKOMI_NAME"].Value.ToString() == "済" &&
                    this.form.Ichiran.CurrentRow.Cells["TEIKI_HAISHA_NUMBER"].Value.ToString() != string.Empty)
                {
                    // 組込みが済になっていても、配車番号を打ち換えている可能性があるので
                    // この行の受付番号が定期配車に登録済みでないかDBをチェックする
                    sb.AppendFormat(" SELECT * FROM T_TEIKI_HAISHA_ENTRY ENT ");
                    sb.AppendFormat(" LEFT JOIN T_TEIKI_HAISHA_DETAIL DET ON ENT.SYSTEM_ID=DET.SYSTEM_ID AND ENT.SEQ=DET.SEQ ");
                    sb.AppendFormat(" WHERE ENT.DELETE_FLG=0 AND DET.UKETSUKE_NUMBER={0}", this.form.Ichiran.CurrentRow.Cells["UKETSUKE_NUMBER"].Value.ToString());
                    dt = this.sysInfoDao.GetDateForStringSql(sb.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        // 定期配車に登録済みの受付番号なら飛ばす
                        kumikomiChk = false;
                    }
                }

                if (kumikomiChk)
                {
                    // 画面区分
                    string gamenKbn = this.form.Ichiran.CurrentRow.Cells["ENTITY_KBN"].Value.ToString();

                    // 画面遷移
                    if (gamenKbn == "1")
                    {
                        // 受付（収集）入力

                        sb.Clear();
                        sb.Append(" SELECT ");
                        sb.AppendFormat(" GYO.GYOUSHA_CD AS {0} ", CourseHaishaConstans.GYOUSHA_CD);
                        sb.AppendFormat(",ENT.GYOUSHA_NAME AS {0} ", CourseHaishaConstans.GYOUSHA_NAME_RYAKU);
                        sb.AppendFormat(",GEN.GENBA_CD AS {0} ", CourseHaishaConstans.GENBA_CD);
                        sb.AppendFormat(",ENT.GENBA_NAME AS {0} ", CourseHaishaConstans.GENBA_NAME_RYAKU);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN TDF.TODOUFUKEN_NAME ELSE TDF2.TODOUFUKEN_NAME  END AS {0} ", CourseHaishaConstans.TODOUFUKEN_NAME);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_ADDRESS1  ELSE GYO.GYOUSHA_ADDRESS1  END AS {0} ", CourseHaishaConstans.ADDRESS1);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_ADDRESS2  ELSE GYO.GYOUSHA_ADDRESS2  END AS {0} ", CourseHaishaConstans.ADDRESS2);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_LATITUDE  ELSE GYO.GYOUSHA_LATITUDE  END AS {0} ", CourseHaishaConstans.LATITUDE);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_LONGITUDE ELSE GYO.GYOUSHA_LONGITUDE END AS {0} ", CourseHaishaConstans.LONGITUDE);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_POST      ELSE GYO.GYOUSHA_POST      END AS {0} ", CourseHaishaConstans.POST);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_TEL       ELSE GYO.GYOUSHA_TEL       END AS {0} ", CourseHaishaConstans.TEL);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.BIKOU1          ELSE GYO.BIKOU1            END AS {0} ", CourseHaishaConstans.BIKOU1);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.BIKOU2          ELSE GYO.BIKOU2            END AS {0} ", CourseHaishaConstans.BIKOU2);
                        sb.AppendFormat(",ENT.GENCHAKU_TIME_NAME");
                        sb.AppendFormat(",ENT.GENCHAKU_TIME");
                        sb.AppendFormat(",ENT.SYSTEM_ID ");
                        sb.AppendFormat(",ENT.SEQ ");
                        // 収集受付の場合
                        sb.AppendFormat(",'収集' AS DENSHU_KBN");
                        sb.AppendFormat(" FROM T_UKETSUKE_SS_ENTRY AS ENT ");
                        sb.AppendFormat(" LEFT JOIN M_GYOUSHA    GYO  ON ENT.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                        sb.AppendFormat(" LEFT JOIN M_GENBA      GEN  ON ENT.GYOUSHA_CD = GEN.GYOUSHA_CD AND ENT.GENBA_CD = GEN.GENBA_CD ");
                        sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF  ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                        sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF2 ON GYO.GYOUSHA_TODOUFUKEN_CD = TDF2.TODOUFUKEN_CD ");
                        sb.AppendFormat(" WHERE ENT.DELETE_FLG = 0 ");
                        sb.AppendFormat(" AND ENT.UKETSUKE_NUMBER = {0}", this.form.Ichiran.CurrentRow.Cells["UKETSUKE_NUMBER"].Value.ToString());
                    }
                    else
                    {
                        // 受付（出荷）入力
                        sb.Append(" SELECT ");
                        sb.AppendFormat(" GYO.GYOUSHA_CD AS {0} ", CourseHaishaConstans.GYOUSHA_CD);
                        sb.AppendFormat(",ENT.GYOUSHA_NAME AS {0} ", CourseHaishaConstans.GYOUSHA_NAME_RYAKU);
                        sb.AppendFormat(",GEN.GENBA_CD AS {0} ", CourseHaishaConstans.GENBA_CD);
                        sb.AppendFormat(",ENT.GENBA_NAME AS {0} ", CourseHaishaConstans.GENBA_NAME_RYAKU);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN TDF.TODOUFUKEN_NAME ELSE TDF2.TODOUFUKEN_NAME  END AS {0} ", CourseHaishaConstans.TODOUFUKEN_NAME);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_ADDRESS1  ELSE GYO.GYOUSHA_ADDRESS1  END AS {0} ", CourseHaishaConstans.ADDRESS1);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_ADDRESS2  ELSE GYO.GYOUSHA_ADDRESS2  END AS {0} ", CourseHaishaConstans.ADDRESS2);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_LATITUDE  ELSE GYO.GYOUSHA_LATITUDE  END AS {0} ", CourseHaishaConstans.LATITUDE);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_LONGITUDE ELSE GYO.GYOUSHA_LONGITUDE END AS {0} ", CourseHaishaConstans.LONGITUDE);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_POST      ELSE GYO.GYOUSHA_POST      END AS {0} ", CourseHaishaConstans.POST);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.GENBA_TEL       ELSE GYO.GYOUSHA_TEL       END AS {0} ", CourseHaishaConstans.TEL);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.BIKOU1          ELSE GYO.BIKOU1            END AS {0} ", CourseHaishaConstans.BIKOU1);
                        sb.AppendFormat(",CASE WHEN GEN.GENBA_CD IS NOT NULL AND GEN.GENBA_CD!='' THEN GEN.BIKOU2          ELSE GYO.BIKOU2            END AS {0} ", CourseHaishaConstans.BIKOU2);
                        sb.AppendFormat(",ENT.GENCHAKU_TIME_NAME");
                        sb.AppendFormat(",ENT.GENCHAKU_TIME");
                        sb.AppendFormat(",ENT.SYSTEM_ID ");
                        sb.AppendFormat(",ENT.SEQ ");
                        // 出荷受付の場合
                        sb.AppendFormat(",'出荷' AS DENSHU_KBN");
                        sb.AppendFormat(" FROM T_UKETSUKE_SK_ENTRY AS ENT ");
                        sb.AppendFormat(" LEFT JOIN M_GYOUSHA    GYO  ON ENT.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                        sb.AppendFormat(" LEFT JOIN M_GENBA      GEN  ON ENT.GYOUSHA_CD = GEN.GYOUSHA_CD AND ENT.GENBA_CD = GEN.GENBA_CD ");
                        sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF  ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                        sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF2 ON GYO.GYOUSHA_TODOUFUKEN_CD = TDF2.TODOUFUKEN_CD ");
                        sb.AppendFormat(" WHERE ENT.DELETE_FLG = 0 ");
                        sb.AppendFormat(" AND ENT.UKETSUKE_NUMBER = {0}", this.form.Ichiran.CurrentRow.Cells["UKETSUKE_NUMBER"].Value.ToString());
                    }
                    dt = this.sysInfoDao.GetDateForStringSql(sb.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            mapDto dto = new mapDto();
                            dto.id = layerId;
                            dto.layerNo = layerId;
                            dto.courseName = string.Empty;
                            dto.dayName = string.Empty;
                            dto.teikiHaishaNo = string.Empty;
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiName = string.Empty;
                            dto.gyoushaCd = string.Empty;
                            dto.gyoushaName = Convert.ToString(dt.Rows[j][CourseHaishaConstans.GYOUSHA_NAME_RYAKU]);
                            dto.genbaCd = string.Empty;
                            dto.genbaName = Convert.ToString(dt.Rows[j][CourseHaishaConstans.GENBA_NAME_RYAKU]);
                            dto.post = Convert.ToString(dt.Rows[j][CourseHaishaConstans.POST]);
                            dto.address = Convert.ToString(dt.Rows[j][CourseHaishaConstans.TODOUFUKEN_NAME])
                                        + Convert.ToString(dt.Rows[j][CourseHaishaConstans.ADDRESS1])
                                        + Convert.ToString(dt.Rows[j][CourseHaishaConstans.ADDRESS2]);
                            dto.tel = Convert.ToString(dt.Rows[j][CourseHaishaConstans.TEL]);
                            dto.bikou1 = Convert.ToString(dt.Rows[j][CourseHaishaConstans.BIKOU1]);
                            dto.bikou2 = Convert.ToString(dt.Rows[j][CourseHaishaConstans.BIKOU2]);
                            string time = Convert.ToString(dt.Rows[j]["GENCHAKU_TIME_NAME"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j]["GENCHAKU_TIME"])))
                                time += Convert.ToDateTime(Convert.ToString(dt.Rows[j]["GENCHAKU_TIME"])).ToString("HH:mm");
                            dto.genbaChaku = time;

                            string sql = string.Empty;
                            if (Convert.ToString(dt.Rows[j]["DENSHU_KBN"]) == "収集")
                            {
                                sql = " SELECT H.HINMEI_NAME_RYAKU, SS.SUURYOU, U.UNIT_NAME_RYAKU FROM T_UKETSUKE_SS_DETAIL SS "
                                    + " LEFT JOIN M_HINMEI H ON SS.HINMEI_CD = H.HINMEI_CD "
                                    + " LEFT JOIN M_UNIT U ON SS.UNIT_CD = U.UNIT_CD"
                                    + " WHERE SYSTEM_ID = " + Convert.ToInt64(dt.Rows[j]["SYSTEM_ID"])
                                    + "   AND SEQ = " + Convert.ToInt32(dt.Rows[j]["SEQ"]);
                            }
                            else
                            {
                                sql = " SELECT H.HINMEI_NAME_RYAKU, SK.SUURYOU, U.UNIT_NAME_RYAKU FROM T_UKETSUKE_SK_DETAIL SK "
                                    + " LEFT JOIN M_HINMEI H ON SK.HINMEI_CD = H.HINMEI_CD "
                                    + " LEFT JOIN M_UNIT U ON SK.UNIT_CD = U.UNIT_CD"
                                    + " WHERE SYSTEM_ID = " + Convert.ToInt64(dt.Rows[j]["SYSTEM_ID"])
                                    + "   AND SEQ = " + Convert.ToInt32(dt.Rows[j]["SEQ"]);
                            }
                            DataTable hinmeiDt = this.sysInfoDao.GetDateForStringSql(sql);
                            string hinmei = string.Empty;
                            foreach (DataRow dr in hinmeiDt.Rows)
                            {
                                string suuryou = string.Empty;
                                if (dr["SUURYOU"] != DBNull.Value)
                                {
                                    suuryou = Convert.ToDecimal(dr["SUURYOU"]).ToString(this.sysInfoEntity.SYS_SUURYOU_FORMAT);
                                }

                                if (string.IsNullOrEmpty(hinmei))
                                {
                                    hinmei += dr["HINMEI_NAME_RYAKU"] + " " + suuryou + dr["UNIT_NAME_RYAKU"];
                                }
                                else
                                {
                                    hinmei += "/" + dr["HINMEI_NAME_RYAKU"] + " " + suuryou + dr["UNIT_NAME_RYAKU"];
                                }
                            }
                            dto.hinmei = hinmei;
                            dto.latitude = Convert.ToString(dt.Rows[j][CourseHaishaConstans.LATITUDE]);
                            dto.longitude = Convert.ToString(dt.Rows[j][CourseHaishaConstans.LONGITUDE]);
                            dto.NoCount = true;
                            dtos.Add(dto);
                        }
                        // 1コース終わったらリストにセット
                        dtoList.dtos = dtos;
                    }
                    if (dtoList.dtos != null)
                    {
                        if (dtoList.dtos.Count != 0)
                        {
                            dtoLists.Add(dtoList);
                        }
                    }
                }

                #endregion

                #region 定期配車の情報

                if (Convert.ToString(this.form.Ichiran.CurrentRow.Cells["TEIKI_HAISHA_NUMBER"].Value) != string.Empty)
                {
                    layerId++;
                    dtoList = new mapDtoList();
                    dtoList.layerId = layerId;
                    dtos = new List<mapDto>();

                    sb.Clear();
                    sb.AppendFormat(" SELECT ");
                    sb.AppendFormat(" DET.ROW_NUMBER AS {0} ", CourseHaishaConstans.ROW_NO);
                    sb.AppendFormat(",DET.ROUND_NO AS {0} ", CourseHaishaConstans.ROUND_NO);
                    sb.AppendFormat(",CON.COURSE_NAME AS {0} ", CourseHaishaConstans.COURSE_NAME);
                    sb.AppendFormat(",DET.GYOUSHA_CD AS {0} ", CourseHaishaConstans.GYOUSHA_CD);
                    sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", CourseHaishaConstans.GYOUSHA_NAME_RYAKU);
                    sb.AppendFormat(",DET.GENBA_CD AS {0} ", CourseHaishaConstans.GENBA_CD);
                    sb.AppendFormat(",GEN.GENBA_NAME_RYAKU AS {0} ", CourseHaishaConstans.GENBA_NAME_RYAKU);
                    sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", CourseHaishaConstans.TODOUFUKEN_NAME);
                    sb.AppendFormat(",GEN.GENBA_ADDRESS1 AS {0} ", CourseHaishaConstans.ADDRESS1);
                    sb.AppendFormat(",GEN.GENBA_ADDRESS2 AS {0} ", CourseHaishaConstans.ADDRESS2);
                    sb.AppendFormat(",GEN.GENBA_LATITUDE AS {0} ", CourseHaishaConstans.LATITUDE);
                    sb.AppendFormat(",GEN.GENBA_LONGITUDE AS {0} ", CourseHaishaConstans.LONGITUDE);
                    sb.AppendFormat(",GEN.GENBA_POST AS {0} ", CourseHaishaConstans.POST);
                    sb.AppendFormat(",GEN.GENBA_TEL AS {0} ", CourseHaishaConstans.TEL);
                    sb.AppendFormat(",GEN.BIKOU1 AS {0} ", CourseHaishaConstans.BIKOU1);
                    sb.AppendFormat(",GEN.BIKOU2 AS {0} ", CourseHaishaConstans.BIKOU2);
                    sb.AppendFormat(",ENT.TEIKI_HAISHA_NUMBER");
                    sb.AppendFormat(",ENT.SAGYOU_DATE");
                    sb.AppendFormat(",ENT.SHUPPATSU_GYOUSHA_CD ");
                    sb.AppendFormat(",ENT.SHUPPATSU_GENBA_CD ");
                    sb.AppendFormat(",DET.KIBOU_TIME ");
                    sb.AppendFormat(",DET.SYSTEM_ID ");
                    sb.AppendFormat(",DET.SEQ ");
                    sb.AppendFormat(",DET.DETAIL_SYSTEM_ID ");
                    sb.AppendFormat(" FROM T_TEIKI_HAISHA_ENTRY AS ENT ");
                    sb.AppendFormat(" LEFT JOIN M_COURSE_NAME CON ON ENT.COURSE_NAME_CD = CON.COURSE_NAME_CD ");
                    sb.AppendFormat(" LEFT JOIN T_TEIKI_HAISHA_DETAIL DET ON ENT.SYSTEM_ID = DET.SYSTEM_ID AND ENT.SEQ = DET.SEQ ");
                    sb.AppendFormat(" LEFT JOIN M_GYOUSHA GYO ON DET.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                    sb.AppendFormat(" LEFT JOIN M_GENBA GEN ON DET.GYOUSHA_CD = GEN.GYOUSHA_CD AND DET.GENBA_CD = GEN.GENBA_CD ");
                    sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                    sb.AppendFormat(" WHERE ENT.DELETE_FLG = 0 ");
                    sb.AppendFormat(" AND ENT.TEIKI_HAISHA_NUMBER = {0}", this.form.Ichiran.CurrentRow.Cells["TEIKI_HAISHA_NUMBER"].Value);
                    sb.AppendFormat(" ORDER BY DET.ROW_NUMBER ");

                    dt = this.sysInfoDao.GetDateForStringSql(sb.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        // 出発業者のみ、または出発業者と出発現場が設定されている場合、コースの先頭とする。
                        string gyoushaCd = dt.Rows[0]["SHUPPATSU_GYOUSHA_CD"].ToString();
                        string genbaCd = dt.Rows[0]["SHUPPATSU_GENBA_CD"].ToString();

                        if (!string.IsNullOrEmpty(gyoushaCd) && string.IsNullOrEmpty(genbaCd))
                        {
                            string sql = string.Empty;
                            sb = new StringBuilder();

                            sb.AppendFormat(" SELECT ");
                            sb.AppendFormat(" GYO.GYOUSHA_CD AS {0} ", CourseHaishaConstans.GYOUSHA_CD);
                            sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", CourseHaishaConstans.GYOUSHA_NAME_RYAKU);
                            sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", CourseHaishaConstans.TODOUFUKEN_NAME);
                            sb.AppendFormat(",GYO.GYOUSHA_ADDRESS1 AS {0} ", CourseHaishaConstans.GYOUSHA_ADDRESS1);
                            sb.AppendFormat(",GYO.GYOUSHA_ADDRESS2 AS {0} ", CourseHaishaConstans.GYOUSHA_ADDRESS2);
                            sb.AppendFormat(",GYO.GYOUSHA_LATITUDE AS {0} ", CourseHaishaConstans.GYOUSHA_LATITUDE);
                            sb.AppendFormat(",GYO.GYOUSHA_LONGITUDE AS {0} ", CourseHaishaConstans.GYOUSHA_LONGITUDE);
                            sb.AppendFormat(",GYO.GYOUSHA_POST AS {0} ", CourseHaishaConstans.GYOUSHA_POST);
                            sb.AppendFormat(",GYO.GYOUSHA_TEL AS {0} ", CourseHaishaConstans.GYOUSHA_TEL);
                            sb.AppendFormat(",GYO.BIKOU1 AS {0} ", CourseHaishaConstans.BIKOU1);
                            sb.AppendFormat(",GYO.BIKOU2 AS {0} ", CourseHaishaConstans.BIKOU2);
                            sb.AppendFormat(" FROM M_GYOUSHA AS GYO ");
                            sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF ON GYO.GYOUSHA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                            sb.AppendFormat(" WHERE GYO.DELETE_FLG = 0 ");
                            sb.AppendFormat(" AND GYO.GYOUSHA_CD = '{0}'", gyoushaCd);

                            DataTable dtShuppatsu = this.sysInfoDao.GetDateForStringSql(sb.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                mapDto dto = new mapDto();
                                dto.id = layerId;
                                dto.layerNo = layerId;
                                dto.courseName = Convert.ToString(dt.Rows[0][CourseHaishaConstans.COURSE_NAME]);
                                dto.dayName = mapLogic.SetDayNameByDate(Convert.ToString(dt.Rows[0]["SAGYOU_DATE"]));
                                dto.teikiHaishaNo = Convert.ToString(dt.Rows[0]["TEIKI_HAISHA_NUMBER"]);
                                dto.torihikisakiCd = string.Empty;
                                dto.torihikisakiName = string.Empty;
                                dto.gyoushaCd = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GYOUSHA_CD]);
                                dto.gyoushaName = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GYOUSHA_NAME_RYAKU]);
                                dto.genbaCd = string.Empty;
                                dto.genbaName = string.Empty;
                                dto.post = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GYOUSHA_POST]);
                                dto.address = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.TODOUFUKEN_NAME])
                                            + Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GYOUSHA_ADDRESS1])
                                            + Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GYOUSHA_ADDRESS2]);
                                dto.tel = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GYOUSHA_TEL]);
                                dto.bikou1 = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.BIKOU1]);
                                dto.bikou2 = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.BIKOU2]);
                                dto.latitude = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GYOUSHA_LATITUDE]);
                                dto.longitude = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GYOUSHA_LONGITUDE]);
                                dto.rowNo = 0;
                                dto.roundNo = 0;
                                dto.genbaChaku = string.Empty;
                                dto.hinmei = string.Empty;
                                dto.shuppatsuFlag = true;
                                dtos.Add(dto);
                            }
                        }
                        else if (!string.IsNullOrEmpty(gyoushaCd) && !string.IsNullOrEmpty(genbaCd))
                        {
                            string sql = string.Empty;
                            sb = new StringBuilder();

                            sb.AppendFormat(" SELECT ");
                            sb.AppendFormat(" GEN.GYOUSHA_CD AS {0} ", CourseHaishaConstans.GYOUSHA_CD);
                            sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", CourseHaishaConstans.GYOUSHA_NAME_RYAKU);
                            sb.AppendFormat(",GEN.GENBA_CD AS {0} ", CourseHaishaConstans.GENBA_CD);
                            sb.AppendFormat(",GEN.GENBA_NAME_RYAKU AS {0} ", CourseHaishaConstans.GENBA_NAME_RYAKU);
                            sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", CourseHaishaConstans.TODOUFUKEN_NAME);
                            sb.AppendFormat(",GEN.GENBA_ADDRESS1 AS {0} ", CourseHaishaConstans.ADDRESS1);
                            sb.AppendFormat(",GEN.GENBA_ADDRESS2 AS {0} ", CourseHaishaConstans.ADDRESS2);
                            sb.AppendFormat(",GEN.GENBA_LATITUDE AS {0} ", CourseHaishaConstans.LATITUDE);
                            sb.AppendFormat(",GEN.GENBA_LONGITUDE AS {0} ", CourseHaishaConstans.LONGITUDE);
                            sb.AppendFormat(",GEN.GENBA_POST AS {0} ", CourseHaishaConstans.POST);
                            sb.AppendFormat(",GEN.GENBA_TEL AS {0} ", CourseHaishaConstans.TEL);
                            sb.AppendFormat(",GEN.BIKOU1 AS {0} ", CourseHaishaConstans.BIKOU1);
                            sb.AppendFormat(",GEN.BIKOU2 AS {0} ", CourseHaishaConstans.BIKOU2);
                            sb.AppendFormat(" FROM M_GENBA AS GEN ");
                            sb.AppendFormat(" LEFT JOIN M_GYOUSHA GYO ON GEN.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                            sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                            sb.AppendFormat(" WHERE GEN.DELETE_FLG = 0 ");
                            sb.AppendFormat(" AND GEN.GYOUSHA_CD = '{0}'", gyoushaCd);
                            sb.AppendFormat(" AND GEN.GENBA_CD = '{0}'", genbaCd);

                            DataTable dtShuppatsu = this.sysInfoDao.GetDateForStringSql(sb.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                mapDto dto = new mapDto();
                                dto.id = layerId;
                                dto.layerNo = layerId;
                                dto.courseName = Convert.ToString(dt.Rows[0][CourseHaishaConstans.COURSE_NAME]);
                                dto.dayName = mapLogic.SetDayNameByDate(Convert.ToString(dt.Rows[0]["SAGYOU_DATE"]));
                                dto.teikiHaishaNo = Convert.ToString(dt.Rows[0]["TEIKI_HAISHA_NUMBER"]);
                                dto.torihikisakiCd = string.Empty;
                                dto.torihikisakiName = string.Empty;
                                dto.gyoushaCd = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GYOUSHA_CD]);
                                dto.gyoushaName = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GYOUSHA_NAME_RYAKU]);
                                dto.genbaCd = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GENBA_CD]);
                                dto.genbaName = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.GENBA_NAME_RYAKU]);
                                dto.post = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.POST]);
                                dto.address = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.TODOUFUKEN_NAME])
                                            + Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.ADDRESS1])
                                            + Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.ADDRESS2]);
                                dto.tel = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.TEL]);
                                dto.bikou1 = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.BIKOU1]);
                                dto.bikou2 = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.BIKOU2]);
                                dto.latitude = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.LATITUDE]);
                                dto.longitude = Convert.ToString(dtShuppatsu.Rows[0][CourseHaishaConstans.LONGITUDE]);
                                dto.rowNo = 0;
                                dto.roundNo = 0;
                                dto.genbaChaku = string.Empty;
                                dto.hinmei = string.Empty;
                                dto.shuppatsuFlag = true;
                                dtos.Add(dto);
                            }
                        }

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                            mapDto dto = new mapDto();
                            dto.id = layerId;
                            dto.layerNo = layerId;
                            dto.courseName = Convert.ToString(dt.Rows[j][CourseHaishaConstans.COURSE_NAME]);
                            dto.dayName = mapLogic.SetDayNameByDate(Convert.ToString(dt.Rows[j]["SAGYOU_DATE"]));
                            dto.teikiHaishaNo = Convert.ToString(dt.Rows[j]["TEIKI_HAISHA_NUMBER"]);
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiName = string.Empty;
                            dto.gyoushaCd = Convert.ToString(dt.Rows[j][CourseHaishaConstans.GYOUSHA_CD]);
                            dto.gyoushaName = Convert.ToString(dt.Rows[j][CourseHaishaConstans.GYOUSHA_NAME_RYAKU]);
                            dto.genbaCd = Convert.ToString(dt.Rows[j][CourseHaishaConstans.GENBA_CD]);
                            dto.genbaName = Convert.ToString(dt.Rows[j][CourseHaishaConstans.GENBA_NAME_RYAKU]);
                            dto.post = Convert.ToString(dt.Rows[j][CourseHaishaConstans.POST]);
                            dto.address = Convert.ToString(dt.Rows[j][CourseHaishaConstans.TODOUFUKEN_NAME]) + Convert.ToString(dt.Rows[j][CourseHaishaConstans.ADDRESS1]) + Convert.ToString(dt.Rows[j][CourseHaishaConstans.ADDRESS2]);
                            dto.tel = Convert.ToString(dt.Rows[j][CourseHaishaConstans.TEL]);
                            dto.bikou1 = Convert.ToString(dt.Rows[j][CourseHaishaConstans.BIKOU1]);
                            dto.bikou2 = Convert.ToString(dt.Rows[j][CourseHaishaConstans.BIKOU2]);
                            dto.rowNo = Convert.ToInt32(dt.Rows[j][CourseHaishaConstans.ROW_NO]);
                            dto.roundNo = Convert.ToInt32(dt.Rows[j][CourseHaishaConstans.ROUND_NO]);
                            string time = string.Empty;
                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j]["KIBOU_TIME"])))
                                time = Convert.ToDateTime(Convert.ToString(dt.Rows[j]["KIBOU_TIME"])).ToString("HH:mm");
                            dto.genbaChaku = time;

                            string sql = " SELECT H.HINMEI_NAME_RYAKU, U.UNIT_NAME_RYAKU FROM T_TEIKI_HAISHA_SHOUSAI HS "
                                       + " LEFT JOIN M_HINMEI H ON HS.HINMEI_CD = H.HINMEI_CD "
                                       + " LEFT JOIN M_UNIT U ON HS.UNIT_CD = U.UNIT_CD "
                                       + " WHERE SYSTEM_ID = " + Convert.ToInt64(dt.Rows[j]["SYSTEM_ID"])
                                       + "   AND SEQ = " + Convert.ToInt32(dt.Rows[j]["SEQ"])
                                       + "   AND DETAIL_SYSTEM_ID = " + Convert.ToInt64(dt.Rows[j]["DETAIL_SYSTEM_ID"]);
                            DataTable hinmeiDt = this.sysInfoDao.GetDateForStringSql(sql);
                            string hinmei = string.Empty;
                            foreach (DataRow dr in hinmeiDt.Rows)
                            {
                                if (string.IsNullOrEmpty(hinmei))
                                {
                                    hinmei += dr["HINMEI_NAME_RYAKU"] + " " + dr["UNIT_NAME_RYAKU"];
                                }
                                else
                                {
                                    hinmei += "/" + dr["HINMEI_NAME_RYAKU"] + " " + dr["UNIT_NAME_RYAKU"];
                                }
                            }
                            dto.hinmei = hinmei;
                            dto.latitude = Convert.ToString(dt.Rows[j][CourseHaishaConstans.LATITUDE]);
                            dto.longitude = Convert.ToString(dt.Rows[j][CourseHaishaConstans.LONGITUDE]);
                            dtos.Add(dto);
                        }
                        // 1コース終わったらリストにセット
                        dtoList.dtos = dtos;
                    }

                    if (dtoList.dtos != null)
                    {
                        if (dtoList.dtos.Count != 0)
                        {
                            dtoLists.Add(dtoList);
                        }
                    }
                }

                #endregion

                #endregion

                return dtoLists;
            }
            catch (Exception ex)
            {
                LogUtility.Error("createMapboxDto", ex);
                this.msgLogic.MessageBoxShowError(ex.Message);
                return null;
            }
        }

        #endregion

        /////////////////////////////////////
        //モバイル連携
        /////////////////////////////////////
        #region モバイル登録
        /// <summary>
        /// モバイル登録
        /// </summary>
        /// <returns></returns>

        //[Transaction]
        public bool MobileRegist()
        {
            bool MobileRegistC = false;

            //リトライは5回まで
            for (MobileTryTime = 1; MobileTryTime <= 5; MobileTryTime++)
            {
                try
                {
                    if (!this.CreateMobileEntity())
                    {
                        //エラーならリトライ
                        continue;
                    }
                    using (Transaction tran = new Transaction())
                    {
                        // モバイル将軍業務TBLテーブル登録
                        foreach (T_MOBISYO_RT detail in this.entitysMobisyoRtList)
                        {
                            this.TmobisyoRtDao.Insert(detail);
                        }
                        //定期配車は、コンテナ無し
                        // モバイル将軍業務詳細TBLテーブル登録           
                        foreach (T_MOBISYO_RT_DTL detail in this.entitysMobisyoRtDTLList)
                        {
                            this.TmobisyoRtDTLDao.Insert(detail);
                        }
                        // モバイル将軍業務搬入TBL テーブル登録           
                        foreach (T_MOBISYO_RT_HANNYUU detail in this.entitysMobisyoRtHNList)
                        {
                            this.TmobisyoRtHNDao.Insert(detail);
                        }
                        // トランザクション終了
                        tran.Commit();
                    }
                    return true;
                }
                catch (NotSingleRowUpdatedRuntimeException ex1)
                {
                    //該当データは他ユーザーにより更新されています
                    LogUtility.Error("MobileRegist", ex1);
                }
                catch (SQLRuntimeException ex2)
                {
                    //データの登録または検索に失敗しました
                    LogUtility.Error("MobileRegist", ex2);
                }
                catch (Exception ex)
                {
                    //予期しないエラーが発生しました。
                    LogUtility.Error("MobileRegist", ex);
                }
            }
            return MobileRegistC;
        }
        #endregion モバイル登録
        #region モバイル登録チェック
        /// <summary>
        /// モバイル登録チェック
        /// ※モバイルオプションかつ、作業日＝当日が前提条件
        /// 　かつ、モバイル連携可能条件を満たしている事
        /// </summary>
        /// <returns></returns>
        public bool MobileRegistCheck()
        {
            bool mobileRegist = false;

            bool is_mobile = r_framework.Configuration.AppConfig.AppOptions.IsMobile();
            if (is_mobile)
            {
                if (string.IsNullOrEmpty(this.Renkei_TeikiDetailSystemId))
                {
                    //どの明細にもチェックが付いてなかったら対象外
                    return false;
                }
                this.dto = new DTOCls();
                this.dto.DETAIL_SYSTEM_ID = this.Renkei_TeikiDetailSystemId;
                this.dto.SAGYOU_DATE_FROM = DateTime.Now.ToString("yyyy/MM/dd");
                this.dto.SAGYOU_DATE_TO = DateTime.Now.ToString("yyyy/MM/dd");
                this.ResultTable = new DataTable();
                this.ResultTable = this.teikiHaishaDao.GetDataToMRDataTable(this.dto);

                if (0 < this.ResultTable.Rows.Count)
                {
                    DialogResult dr = new DialogResult();
                    dr = this.msgLogic.MessageBoxShow("C110");
                    if (dr == DialogResult.OK || dr == DialogResult.Yes)
                    {
                        mobileRegist = true;
                    }
                }
            }

            return mobileRegist;
        }
        #endregion モバイル登録チェック

        #region モバイル採番
        /// <summary>
        /// シーケンシャルナンバー採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 CreateMobileSeqNo()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.MOBILE_RENKEI.GetHashCode(); ;

            // IS_NUMBER_SYSTEMDao(共通)
            IS_NUMBER_SYSTEMDao numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

            var updateEntity = numberSystemDao.GetNumberSystemData(entity);
            returnInt = numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.MOBILE_RENKEI.GetHashCode(); ;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }
        #endregion モバイル採番

        #region モバイルentity作成
        /// <summary>
        /// データチェックした時に取得した情報からEntityを作成する
        /// </summary>
        /// <param name="isDelete">True削除:False登録</param>
        /// <returns></returns>
        public bool CreateMobileEntity()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.entitysMobisyoRtList = new List<T_MOBISYO_RT>();
                this.entitysMobisyoRtDTLList = new List<T_MOBISYO_RT_DTL>();
                this.entitysMobisyoRtHNList = new List<T_MOBISYO_RT_HANNYUU>();
                int ZenHaishaNo = -1;
                int HaishaNo;
                int ZenHaishaRowNo = -1;
                int HaishaRowNo;

                foreach (DataRow tableRow in this.ResultTable.Rows)
                {
                    //モバイル連携にチェックが付いてた明細だけ処理
                    // 定期配車番号
                    HaishaNo = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                    // 定期配車行番号
                    HaishaRowNo = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());

                    #region T_MOBISYO_RT
                    // 定期配車番号行番号をカウント作成
                    if (ZenHaishaNo != HaishaNo || ZenHaishaRowNo != HaishaRowNo)
                    {
                        // entitys作成
                        this.entitysMobisyoRt = new T_MOBISYO_RT();
                        // シーケンシャルナンバー
                        this.entitysMobisyoRt.SEQ_NO = this.CreateMobileSeqNo();

                        // 車種CD
                        if (!string.IsNullOrEmpty(tableRow["SHASHU_CD"].ToString()))
                        {
                            this.entitysMobisyoRt.SHASHU_CD = tableRow["SHASHU_CD"].ToString();
                        }
                        // 車種名
                        if (!string.IsNullOrEmpty(tableRow["SHASHU_NAME"].ToString()))
                        {
                            this.entitysMobisyoRt.SHASHU_NAME = tableRow["SHASHU_NAME"].ToString();
                        }
                        // 車輌CD
                        if (!string.IsNullOrEmpty(tableRow["SHARYOU_CD"].ToString()))
                        {
                            this.entitysMobisyoRt.SHARYOU_CD = tableRow["SHARYOU_CD"].ToString();
                        }
                        // 車輌名
                        if (!string.IsNullOrEmpty(tableRow["SHARYOU_NAME"].ToString()))
                        {
                            this.entitysMobisyoRt.SHARYOU_NAME = tableRow["SHARYOU_NAME"].ToString();
                        }
                        // 運転者名
                        if (!string.IsNullOrEmpty(tableRow["UNTENSHA_NAME"].ToString()))
                        {
                            this.entitysMobisyoRt.UNTENSHA_NAME = tableRow["UNTENSHA_NAME"].ToString();
                        }
                        // 運転者名CD
                        if (!string.IsNullOrEmpty(tableRow["UNTENSHA_CD"].ToString()))
                        {
                            this.entitysMobisyoRt.UNTENSHA_CD = tableRow["UNTENSHA_CD"].ToString();
                        }
                        // (配車)作業日
                        if (!SqlDateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString()).IsNull)
                        {
                            this.entitysMobisyoRt.HAISHA_SAGYOU_DATE = SqlDateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString());
                        }
                        // (配車)伝票番号
                        this.entitysMobisyoRt.HAISHA_DENPYOU_NO = SqlInt64.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                        // (配車)コース名称CD
                        if (!string.IsNullOrEmpty(tableRow["HAISHA_COURSE_NAME_CD"].ToString()))
                        {
                            this.entitysMobisyoRt.HAISHA_COURSE_NAME_CD = tableRow["HAISHA_COURSE_NAME_CD"].ToString();
                        }
                        // (配車)コース名称
                        if (!string.IsNullOrEmpty(tableRow["HAISHA_COURSE_NAME"].ToString()))
                        {
                            this.entitysMobisyoRt.HAISHA_COURSE_NAME = tableRow["HAISHA_COURSE_NAME"].ToString();
                        }
                        // (配車)配車区分 0
                        this.entitysMobisyoRt.HAISHA_KBN = 0;
                        // 登録日時 Insertした日次
                        this.entitysMobisyoRt.GENBA_JISSEKI_CREATEDATE = parentForm.sysDate;
                        // 修正日時 Insertした日次
                        this.entitysMobisyoRt.GENBA_JISSEKI_UPDATEDATE = parentForm.sysDate;
                        // 業者CD
                        if (!string.IsNullOrEmpty(tableRow["GENBA_JISSEKI_GYOUSHACD"].ToString()))
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_GYOUSHACD = tableRow["GENBA_JISSEKI_GYOUSHACD"].ToString();
                        }
                        // 現場CD
                        if (!string.IsNullOrEmpty(tableRow["GENBA_JISSEKI_GENBACD"].ToString()))
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_GENBACD = tableRow["GENBA_JISSEKI_GENBACD"].ToString();
                        }
                        // 追加現場フラグ 基本的には0。データを登録するとき、作業日＝当日の場合、1
                        if (!SqlDateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString()).IsNull &&
                            (DateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString()).ToString("yyyy/MM/dd") == (parentForm.sysDate).ToString("yyyy/MM/dd")))
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_ADDGENBAFLG = true;
                        }
                        else
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_ADDGENBAFLG = false;
                        }
                        // 指示確認フラグ 0
                        this.entitysMobisyoRt.SHIJI_FLG = false;
                        // 除外フラグ 0
                        this.entitysMobisyoRt.GENBA_JISSEKI_JYOGAIFLG = false;
                        // マニフェスト区分 0
                        this.entitysMobisyoRt.GENBA_DETAIL_MANIKBN = 0;
                        // ステータス
                        this.entitysMobisyoRt.GENBA_STTS = "0";
                        // 実績登録フラグ
                        this.entitysMobisyoRt.JISSEKI_REGIST_FLG = false;
                        // 運搬業者CD
                        if (!string.IsNullOrEmpty(tableRow["GENBA_JISSEKI_UPNGYOSHACD"].ToString()))
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_UPNGYOSHACD = tableRow["GENBA_JISSEKI_UPNGYOSHACD"].ToString();
                        }
                        else
                        {
                            this.entitysMobisyoRt.GENBA_JISSEKI_UPNGYOSHACD = string.Empty;
                        }
                        // (配車)行番号
                        this.entitysMobisyoRt.HAISHA_ROW_NUMBER = SqlInt32.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());

                        // 削除フラグ
                        this.entitysMobisyoRt.DELETE_FLG = false;

                        // 20170601 wangjm モバイル将軍#105481 start
                        this.entitysMobisyoRt.KAISHU_NO = SqlInt32.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());
                        this.entitysMobisyoRt.KAISHU_BIKOU = tableRow["GENBA_MEISAI_BIKOU"].ToString();
                        // 20170601 wangjm モバイル将軍#105481 end

                        // 自動設定
                        var dataBinderContenaResult = new DataBinderLogic<T_MOBISYO_RT>(this.entitysMobisyoRt);
                        dataBinderContenaResult.SetSystemProperty(this.entitysMobisyoRt, false);

                        // Listに追加
                        this.entitysMobisyoRtList.Add(this.entitysMobisyoRt);
                    }
                    #endregion

                    // 前回定期配車番号
                    ZenHaishaNo = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                    // 前回定期配車行番号
                    ZenHaishaRowNo = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());
                }

                int ZenHaishaNo2 = -1;
                int HaishaNo2;
                int ZenHaishaRowNo2 = -1;
                int HaishaRowNo2;
                string NiorosiNo2;
                int Edaban2 = 0;
                List<NiorosiClass> niorosiList = new List<NiorosiClass>();
                foreach (DataRow tableRow in this.ResultTable.Rows)
                {
                    // 定期配車番号
                    HaishaNo2 = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                    // 定期配車行番号
                    HaishaRowNo2 = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());
                    // 荷降番号
                    if (string.IsNullOrEmpty(tableRow["NIOROSHI_NUMBER"].ToString()))
                    {
                        NiorosiNo2 = null;
                    }
                    else
                    {
                        NiorosiNo2 = tableRow["NIOROSHI_NUMBER"].ToString();
                    }

                    if (ZenHaishaNo2 != HaishaNo2 || ZenHaishaRowNo2 != HaishaRowNo2)
                    {
                        // 枝番
                        Edaban2 = 0;
                    }

                    if (ZenHaishaNo2 != HaishaNo2 && NiorosiNo2 != null)
                    {
                        niorosiList = new List<NiorosiClass>();
                        DataTable niorosiTable = this.teikiHaishaDao.GetMobilNioroshiData(HaishaNo2, int.Parse(NiorosiNo2));
                        if (niorosiTable != null && niorosiTable.Rows.Count > 0)
                        {
                            foreach (DataRow niorosiRow in niorosiTable.Rows)
                            {
                                NiorosiClass niorosi = new NiorosiClass();
                                niorosi.TEIKI_HAISHA_NUMBER = niorosiRow["HAISHA_DENPYOU_NO"].ToString();
                                niorosi.NIOROSHI_NUMBER = niorosiRow["NIOROSHI_NUMBER"].ToString();
                                niorosi.HANYU_SEQ_NO = SqlInt64.Parse(niorosiRow["HANYU_SEQ_NO"].ToString());
                                niorosiList.Add(niorosi);
                            }
                        }

                    }
                    // 前回定期配車番号
                    ZenHaishaNo2 = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                    // 前回定期配車行番号
                    ZenHaishaRowNo2 = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());

                    // 品名なしの場合、T_MOBISYO_RT_DTLデータを作成しない。
                    if (string.IsNullOrEmpty(tableRow["GENBA_DETAIL_HINMEICD"].ToString()))
                    {
                        continue;
                    }

                    #region T_MOBISYO_RT_DTL
                    // 枝番
                    Edaban2++;

                    // entitys作成
                    this.entitysMobisyoRtDTL = new T_MOBISYO_RT_DTL();
                    // シーケンシャルナンバー

                    List<T_MOBISYO_RT> data = (from temp in entitysMobisyoRtList
                                                where temp.HAISHA_DENPYOU_NO.ToString().Equals(HaishaNo2.ToString()) &&
                                                        temp.HAISHA_ROW_NUMBER.ToString().Equals(HaishaRowNo2.ToString())
                                                select temp).ToList();
                    this.entitysMobisyoRtDTL.SEQ_NO = data[0].SEQ_NO;
                    List<NiorosiClass> niorosiData = null;
                    if (NiorosiNo2 != null)
                    {
                        niorosiData = (from temp in niorosiList
                                        where temp.TEIKI_HAISHA_NUMBER.ToString().Equals(HaishaNo2.ToString()) &&
                                        temp.NIOROSHI_NUMBER.ToString().Equals(NiorosiNo2.ToString())
                                        select temp).ToList();
                    }
                    // 搬入シーケンシャルナンバー
                    if (niorosiData != null && niorosiData.Count > 0 && NiorosiNo2 != null)
                    {
                        this.entitysMobisyoRtDTL.HANYU_SEQ_NO = niorosiData[0].HANYU_SEQ_NO;
                    }
                    else
                    {
                        if (NiorosiNo2 != null)
                        {
                            this.entitysMobisyoRtDTL.HANYU_SEQ_NO = this.CreateMobileSeqNo();
                            NiorosiClass niorosi = new NiorosiClass();
                            niorosi.TEIKI_HAISHA_NUMBER = HaishaNo2.ToString();
                            niorosi.NIOROSHI_NUMBER = NiorosiNo2;
                            niorosi.HANYU_SEQ_NO = this.entitysMobisyoRtDTL.HANYU_SEQ_NO;
                            niorosiList.Add(niorosi);

                            #region T_MOBISYO_RT_HANNYUU
                            // entitys作成
                            this.entitysMobisyoRtHN = new T_MOBISYO_RT_HANNYUU();

                            // 搬入シーケンシャルナンバー
                            this.entitysMobisyoRtHN.HANYU_SEQ_NO = this.entitysMobisyoRtDTL.HANYU_SEQ_NO;
                            // 枝番1を設定する
                            this.entitysMobisyoRtHN.EDABAN = 1;

                            SqlInt64 SYSTEM_ID = SqlInt64.Parse(tableRow["SYSTEM_ID"].ToString());
                            SqlInt32 SEQ = SqlInt32.Parse(tableRow["SEQ"].ToString());
                            SqlInt32 NIOROSHI_NUMBER = SqlInt32.Parse(tableRow["NIOROSHI_NUMBER"].ToString());

                            DataTable NioroshiData = this.teikiHaishaDao.GetTeikiHaishaNioroshiData(SYSTEM_ID, SEQ, NIOROSHI_NUMBER);

                            if (NioroshiData.Rows.Count > 0)
                            {
                                // (搬入)業者CD
                                if (!string.IsNullOrEmpty(NioroshiData.Rows[0]["HANNYUU_GYOUSHACD"].ToString()))
                                {
                                    this.entitysMobisyoRtHN.HANNYUU_GYOUSHACD = NioroshiData.Rows[0]["HANNYUU_GYOUSHACD"].ToString();
                                }

                                // (搬入)現場CD
                                if (!string.IsNullOrEmpty(NioroshiData.Rows[0]["HANNYUU_GENBACD"].ToString()))
                                {
                                    this.entitysMobisyoRtHN.HANNYUU_GENBACD = NioroshiData.Rows[0]["HANNYUU_GENBACD"].ToString();
                                }
                            }
                            // (搬入)搬入量
                            this.entitysMobisyoRtHN.HANNYUU_RYO = SqlDouble.Null;
                            this.entitysMobisyoRtHN.HANNYUU_JISSEKI_RYO = SqlDouble.Null;
                            // 搬入フラグ
                            this.entitysMobisyoRtHN.JISSEKI_REGIST_FLG = false;
                            // 削除フラグ
                            this.entitysMobisyoRtHN.DELETE_FLG = false;

                            // 自動設定
                            var dataBinderContenaResultHN = new DataBinderLogic<T_MOBISYO_RT_HANNYUU>(this.entitysMobisyoRtHN);
                            dataBinderContenaResultHN.SetSystemProperty(this.entitysMobisyoRtHN, false);

                            // Listに追加
                            this.entitysMobisyoRtHNList.Add(this.entitysMobisyoRtHN);

                            #endregion
                        }
                    }

                    // 枝番
                    this.entitysMobisyoRtDTL.EDABAN = Edaban2;
                    // (現場明細)品名CD
                    if (!string.IsNullOrEmpty(tableRow["GENBA_DETAIL_HINMEICD"].ToString()))
                    {
                        this.entitysMobisyoRtDTL.GENBA_DETAIL_HINMEICD = tableRow["GENBA_DETAIL_HINMEICD"].ToString();
                    }
                    // (現場明細)単位１
                    if (!string.IsNullOrEmpty(tableRow["GENBA_DETAIL_UNIT_CD1"].ToString()))
                    {
                        this.entitysMobisyoRtDTL.GENBA_DETAIL_UNIT_CD1 = SqlInt16.Parse(tableRow["GENBA_DETAIL_UNIT_CD1"].ToString());
                    }
                    // (現場明細)単位2
                    if (!string.IsNullOrEmpty(tableRow["GENBA_DETAIL_UNIT_CD2"].ToString()))
                    {
                        if (SqlBoolean.Parse(tableRow["KANSAN_UNIT_MOBILE_OUTPUT_FLG"].ToString()).IsTrue)
                        {
                            this.entitysMobisyoRtDTL.GENBA_DETAIL_UNIT_CD2 = SqlInt16.Parse(tableRow["GENBA_DETAIL_UNIT_CD2"].ToString());
                        }
                    }
                    // (現場明細)数量１
                    this.entitysMobisyoRtDTL.GENBA_DETAIL_SUURYO1 = SqlDouble.Null;
                    // (現場明細)数量２
                    this.entitysMobisyoRtDTL.GENBA_DETAIL_SUURYO2 = SqlDouble.Null;
                    // (現場明細)追加品名フラグ
                    this.entitysMobisyoRtDTL.GENBA_DETAIL_ADDHINMEIFLG = false;
                    // 回収実績フラグ
                    this.entitysMobisyoRtDTL.JISSEKI_REGIST_FLG = false;
                    // 削除フラグ
                    this.entitysMobisyoRtDTL.DELETE_FLG = false;

                    // 自動設定
                    var dataBinderContenaResultDTL = new DataBinderLogic<T_MOBISYO_RT_DTL>(this.entitysMobisyoRtDTL);
                    dataBinderContenaResultDTL.SetSystemProperty(this.entitysMobisyoRtDTL, false);

                    // Listに追加
                    this.entitysMobisyoRtDTLList.Add(this.entitysMobisyoRtDTL);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMobileEntity", ex);

                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion


        /// <summary>
        /// 受付伝票・定期配車伝票のモバイル/ロジコン/NAVITIME連携状況チェック
        /// </summary>
        /// <param name="KB"></param>
        /// <param name="NUMBER"></param>
        /// <returns></returns>
        public bool RenkeiCheck(int KB, string NUMBER = "")
        {
            bool RenkeiCheck = false;
            string selectStr;
            DataTable dt = new DataTable();

            if (NUMBER == "")
            {
                return RenkeiCheck;
            }

            //KB:2　受付伝票が、ﾓﾊﾞｲﾙ連携されているか
            if (KB == 2)
            {
                Int32 UketsukeNumberM = Int32.Parse(NUMBER);
                selectStr = "SELECT RT.* FROM T_MOBISYO_RT RT WHERE RT.HAISHA_DENPYOU_NO = " + UketsukeNumberM
                    + " AND RT.HAISHA_KBN = 1 AND RT.DELETE_FLG = 0";
                dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                if (dt.Rows.Count > 0)
                {
                    RenkeiCheck = true;
                }
            }

            //KB:1受付/ KB:3定期データロジコン連携チェック
            if (KB == 1 || KB == 3)
            {
                // ロジこんぱす連携済みであるかをチェックする。
                Int32 NumberM = Int32.Parse(NUMBER);
                selectStr = "SELECT DISTINCT LLS.* FROM T_LOGI_LINK_STATUS LLS "
                    + "LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD on LDD.SYSTEM_ID = LLS.SYSTEM_ID and LDD.DELETE_FLG = 0";
                selectStr += " WHERE LDD.DENPYOU_ATTR = " + KB  // 1：収集受付 3:定期
                    + " and LDD.REF_DENPYOU_NO = " + NumberM
                    + " and LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                    + " and LLS.DELETE_FLG = 0";

                // データ取得
                dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    RenkeiCheck = true;
                }
            }

            //KB:4　定期データNAVITIME連携チェック
            if (KB == 4)
            {
                if (AppConfig.AppOptions.IsNAVITIME())
                {
                    Int32 NumberM = Int32.Parse(NUMBER);
                    selectStr = " SELECT * FROM T_TEIKI_HAISHA_ENTRY T "
                               + " INNER JOIN T_NAVI_DELIVERY D ON T.SYSTEM_ID = D.TEIKI_SYSTEM_ID AND D.DELETE_FLG = 0 "
                               + " INNER JOIN T_NAVI_LINK_STATUS L ON D.SYSTEM_ID = L.SYSTEM_ID AND L.LINK_STATUS != 3 "
                               + " WHERE T.DELETE_FLG = 0 "
                               + " AND T.TEIKI_HAISHA_NUMBER = " + NumberM;
                    dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                    if (dt.Rows.Count > 0)
                    {
                        RenkeiCheck = true;
                    }
                }
            }

            //KB:5　定期配車に組み込んだ受付の情報が、ﾓﾊﾞｲﾙ連携されているか
            if (KB == 5)
            {
                Int32 UketsukeNumberM = Int32.Parse(NUMBER);
                selectStr = "SELECT * FROM T_MOBISYO_RT RT "
                        + " INNER JOIN T_TEIKI_HAISHA_DETAIL THD ON RT.HAISHA_DENPYOU_NO = THD.TEIKI_HAISHA_NUMBER AND RT.HAISHA_ROW_NUMBER = THD.ROW_NUMBER "
                        + " INNER JOIN T_TEIKI_HAISHA_ENTRY THE ON THE.SYSTEM_ID = THD.SYSTEM_ID AND THE.SEQ = THD.SEQ "
                        + " WHERE RT.DELETE_FLG = 0 AND THE.DELETE_FLG = 0 AND THD.UKETSUKE_NUMBER = " + UketsukeNumberM;
                dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                if (dt.Rows.Count > 0)
                {
                    RenkeiCheck = true;
                }
            }


            //KB:6  定期配車伝票が、モバイル状況一覧に表示される条件か(対象外の場合、RenkeiCheck = true)
            //UNTENSHA_CD、SHARYOU_CD、SHASHU_CD、SHASHU_NAME_RYAKUのしずれかが無し→×。
            if (KB == 6)
            {
                Int32 NumberM = Int32.Parse(NUMBER);
                selectStr = "SELECT * FROM T_TEIKI_HAISHA_ENTRY THE "
                        + " LEFT JOIN M_SHASHU ON M_SHASHU.SHASHU_CD = THE.SHASHU_CD"
                        + " WHERE THE.TEIKI_HAISHA_NUMBER = " + NumberM
                        + " AND THE.DELETE_FLG = 0"
                        + " AND (THE.UNTENSHA_CD IS NOT NULL AND THE.UNTENSHA_CD != '') "
                        + " AND (THE.SHARYOU_CD IS NOT NULL AND THE.SHARYOU_CD != '')"
                        + "	AND (THE.SHASHU_CD IS NOT NULL AND THE.SHASHU_CD != '')"
                        + " AND (M_SHASHU.SHASHU_NAME_RYAKU IS NOT NULL AND M_SHASHU.SHASHU_NAME_RYAKU != '')";
                dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                if (dt.Rows.Count <= 0)
                {
                    RenkeiCheck = true;
                }
            }

            //KB:7  定期配車伝票無いに、業者の取引有無が、無のデータがあるか
            if (KB == 7)
            {
                 Int32 NumberM = Int32.Parse(NUMBER);
                 selectStr = "SELECT ENT.TEIKI_HAISHA_NUMBER FROM "
                        + " T_TEIKI_HAISHA_ENTRY ENT"
                        + " LEFT JOIN T_TEIKI_HAISHA_DETAIL DET ON ENT.SYSTEM_ID = DET.SYSTEM_ID AND ENT.SEQ = DET.SEQ"
                        + " LEFT JOIN M_GYOUSHA ON DET.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD"
                        + " WHERE"
                        + " ENT.TEIKI_HAISHA_NUMBER = " + NumberM
                        + " AND ENT.DELETE_FLG = 0 AND M_GYOUSHA.TORIHIKISAKI_UMU_KBN = 2";
                dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                if (dt.Rows.Count > 0)
                {
                    RenkeiCheck = true;
                }
            }

            //KB:8  定期配車伝票無いに、業者の取引有無が、無のデータがあるか
            if (KB == 8)
            {
                //Int32 NumberM = Int32.Parse(NUMBER);
                selectStr = "SELECT M_GYOUSHA.GYOUSHA_CD FROM M_GYOUSHA"
                       + " WHERE"
                       + " M_GYOUSHA.GYOUSHA_CD = '" + NUMBER + "'"
                       + " AND M_GYOUSHA.DELETE_FLG = 0 AND M_GYOUSHA.TORIHIKISAKI_UMU_KBN = 2";
                dt = this.daoUketsukeSSEntry.GetDateForStringSql(selectStr);
                if (dt.Rows.Count > 0)
                {
                    RenkeiCheck = true;
                }
            }
            return RenkeiCheck;
        }

    }
}