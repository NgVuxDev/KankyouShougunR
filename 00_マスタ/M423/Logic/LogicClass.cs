// $Id: LogicClass.cs 29309 2014-09-03 07:53:33Z miya@e-mall.co.jp $
using System;
using System.Collections.Generic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.DAO;
using Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.DTO;
using System.Reflection;
using System.Data;
using System.Data.SqlTypes;
using System.Windows.Forms;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using CommonChouhyouPopup.App;
using Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Report;
using Shougun.Core.Common.BusinessCommon.Utility;
using Seasar.Framework.Exceptions;


namespace Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region Field
        /// <summary>
        /// Button設定用XMLファイルパス
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Setting.ButtonSetting.xml";

        /// Report設定用XMLファイルパス
        /// </summary>
        private readonly string ReportFormXmlPath = "Template\\R424-Form.xml";

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 会社情報のDao
        /// </summary>
        private IM_CORP_INFODao corpInfoDao;

        /// <summary>
        /// 取引先情報のDao
        /// </summary>
        private TorihikisakiDao torihikisakiDao;

        /// <summary>
        /// 伝種区分情報のDao
        /// </summary>
        private IM_DENSHU_KBNDao denshuKbnDao;

        /// <summary>
        /// 伝票区分情報のDao
        /// </summary>
        private IM_DENPYOU_KBNDao denpyouKbnDao;

        /// <summary>
        /// 業者情報のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 現場情報のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 取引先情報のDao
        /// </summary>
        private IM_TORIHIKISAKIDao toriDao;

        /// <summary>
        /// 個別品名単価一覧のDao
        /// </summary>
        private DAOClass dao;

        /// <summary>
        /// アップデート用のDAO
        /// </summary>
        private UpdateDAOCls updateDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// 会社情報のエンティティ
        /// </summary>
        private M_CORP_INFO[] corpInfoEntity;

        /// <summary>
        /// 伝種区分情報のエンティティ
        /// </summary>
        private M_DENSHU_KBN denshuKbnEntity;

        /// <summary>
        /// 伝票区分情報のエンティティ
        /// </summary>
        private M_DENPYOU_KBN denpyouKbnEntity;

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_KOBETSU_HINMEI_TANKA SearchString { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索用種類コード
        /// </summary>
        private string shuruiCd;

        /// <summary>
        /// 検索用分類コード
        /// </summary>
        private string BunruiCd;


        /// <summary>
        /// 一覧エラーフラグ
        /// </summary>
        private bool errorFlag = false;

        /// <summary>
        /// 内部保持用単価増減値
        /// </summary>
        private string tankaZokgen;


        /// <summary>
        /// 内部保持用適用開始日
        /// </summary>
        private string tekiyouBegin;

        /// <summary>
        /// CSVファイルを出力するクラス
        /// </summary>
        //CSVFileLogic outputLogic;

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

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.corpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<TorihikisakiDao>();
            this.denshuKbnDao = DaoInitUtility.GetComponent<IM_DENSHU_KBNDao>();
            this.denpyouKbnDao = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>();
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.updateDao = DaoInitUtility.GetComponent<UpdateDAOCls>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.toriDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();

            this.SearchResult = new DataTable();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // ヘッダーの初期化処理
                this.HeaderInit();

                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();

                // 画面のコントロールを初期化
                this.ClearWindowInit();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
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
            var parentForm = (MasterBaseForm)this.form.Parent;

            //var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();

        }

        #endregion

        #region ボタン設定の読込

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var parentForm = (MasterBaseForm)this.form.Parent;
            //var parentForm = (BusinessBaseForm)this.form.Parent;
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();

            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

        }

        #endregion

        #region イベント初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (MasterBaseForm)this.form.Parent;
            //var parentForm = (BusinessBaseForm)this.form.Parent;

            // 印刷ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Print);

            // CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);


            // 検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            // 登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            // 取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
            this.form.GYOUSHA_CD.Validated += new EventHandler(this.form.GYOUSHA_CD_Validated);
            this.form.GENBA_CD.Validated += new EventHandler(this.form.GENBA_CD_Validated);
            // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ヘッダーの初期化処理

        /// <summary>
        /// ヘッダーの初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (MasterBaseForm)this.form.Parent;

            ListHeaderForm header = (ListHeaderForm)parentForm.headerForm;
            header.lbl_読込日時.Visible = false;
            header.HEADER_KYOTEN_CD.Visible = false;
            header.HEADER_KYOTEN_NAME.Visible = false;
            header.ReadDataNumber.Enabled = true;
            header.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            header.alertNumber.MaxLength = 5;
            header.alertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region システム情報を取得し、初期値をセットする

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            LogUtility.DebugMethodStart();

            // システム情報を取得し、初期値をセットする
            M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];

                var parentForm = (MasterBaseForm)this.form.Parent;

                ListHeaderForm header = (ListHeaderForm)parentForm.headerForm;

                header.alertNumber.Text = String.Format("{0:#,0}", this.sysInfoEntity.ICHIRAN_ALERT_KENSUU.Value);

            }

            LogUtility.DebugMethodEnd();

        }

        #endregion

        #region 画面コントロール初期化

        /// <summary>
        /// 画面のコントロールを初期化
        /// </summary>
        public void ClearWindowInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (MasterBaseForm)this.form.Parent;

            // 
            this.form.denshuKbn.Text = "4";
            this.form.denpyouKbn.Text = "1";
            this.form.TEKIYOU_BEGIN.Value = DateTime.Now;
            this.form.tankaZougen.Text = string.Empty;

            // 取引先CD～荷降現場まで初期化
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;

            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;

            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;

            this.form.UNPAN_KAISHA_CD.Text = string.Empty;
            this.form.UNPAN_KAISHA_NAME_RYAKU.Text = string.Empty;

            this.form.NIOROSHI_KAISHA_CD.Text = string.Empty;
            this.form.NIOROSHI_KAISHA_NAME_RYAKU.Text = string.Empty;

            this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
            this.form.NIOROSHI_GENBA_NAME_RYAKU.Text = string.Empty;

            this.form.SHURUI_CD.Text = string.Empty;
            this.form.SHURUI_NAME_RYAKU.Text = string.Empty;

            this.form.BUNRUI_CD.Text = string.Empty;
            this.form.BUNRUI_NAME_RYAKU.Text = string.Empty;

            this.form.HINMEI_CD.Text = string.Empty;
            this.form.HINMEI_NAME_RYAKU.Text = string.Empty;

            this.form.UNIT_CD.Text = string.Empty;
            this.form.UNIT_NAME_RYAKU.Text = string.Empty;

            // 一覧をクリア
            this.form.KobetsuHinmeiTankaDetail.Rows.Clear();
            //this.form.KobetsuHinmeiTankaDetail.Rows.Add();

            // 登録ボタンを非活性
            parentForm.bt_func9.Enabled = false;
            ListHeaderForm header = (ListHeaderForm)parentForm.headerForm;
            header.ReadDataNumber.Text = "0";
            this.SearchResult = new DataTable();

            // サブファンクション非表示
            parentForm.ProcessButtonPanel.Visible = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region LogicalDelete

        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();
            LogUtility.DebugMethodEnd();
            throw new NotImplementedException();
        }

        #endregion

        #region PhysicalDelete

        public void PhysicalDelete()
        {
            LogUtility.DebugMethodStart();
            LogUtility.DebugMethodEnd();
            throw new NotImplementedException();
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            //独自チェックの記述例を書く
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        for (int i = 0; i < this.form.KobetsuHinmeiTankaDetail.Rows.Count; i++)
                        {
                            M_KOBETSU_HINMEI_TANKA entity = new M_KOBETSU_HINMEI_TANKA();
                            var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KOBETSU_HINMEI_TANKA>(entity);

                            // 適用終了日の有無
                            if (DBNull.Value.Equals(this.SearchResult.Rows[i]["TEKIYOU_END"]))
                            //if (DBNull.Value.Equals(this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["TEKIYOU_END"].Value))
                            {
                                // 適用開始日
                                DateTime dtTekiyouBegin = DateTime.Parse(this.SearchResult.Rows[i]["TEKIYOU_BEGIN"].ToString());
                                //DateTime tekiyouBegin = DateTime.Parse(this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_BEGIN"].Value.ToString());
                                //int tekiyouBeginCompare = ((DateTime.Parse(this.form.TEKIYOU_BEGIN.Value.ToString())).ToShortDateString()).CompareTo(tekiyouBegin.ToString("yyyy/MM/dd"));
                                int tekiyouBeginCompare = ((DateTime.Parse(this.tekiyouBegin)).ToShortDateString()).CompareTo(dtTekiyouBegin.ToString("yyyy/MM/dd"));
                                if (tekiyouBeginCompare > 0)
                                {
                                    // パターン2
                                    M_KOBETSU_HINMEI_TANKA m_Entity = new M_KOBETSU_HINMEI_TANKA();
                                    m_Entity = CreateUpdateTekiyouEndEntity(this.form.KobetsuHinmeiTankaDetail.Rows[i]);
                                    // システム自動プロパティ設定
                                    dataBinderLogic.SetSystemProperty(m_Entity, false);

                                    // SYS_ID
                                    m_Entity.SYS_ID = Int64.Parse(this.SearchResult.Rows[i]["SYS_ID"].ToString());

                                    // 適用開始日
                                    m_Entity.TEKIYOU_BEGIN = DateTime.Parse(this.SearchResult.Rows[i]["TEKIYOU_BEGIN"].ToString());

                                    // TIME_STAMP
                                    if (!DBNull.Value.Equals(this.SearchResult.Rows[i]["TIME_STAMP"]))
                                    {
                                        m_Entity.TIME_STAMP = (byte[])this.SearchResult.Rows[i]["TIME_STAMP"];
                                    }

                                    // 適用終了日更新
                                    this.updateDao.Update(m_Entity);

                                    entity = this.CreateInsertEntity(this.form.KobetsuHinmeiTankaDetail.Rows[i]);
                                    // 伝票区分設定
                                    entity.DENPYOU_KBN_CD = Int16.Parse(this.SearchResult.Rows[i]["DENPYOU_KBN_CD"].ToString());

                                    // 伝種区分設定
                                    entity.DENSHU_KBN_CD = Int16.Parse(this.SearchResult.Rows[i]["DENSHU_KBN_CD"].ToString());

                                    // システム自動プロパティ設定
                                    dataBinderLogic.SetSystemProperty(entity, false);

                                    this.dao.Insert(entity);
                                }
                                else if (tekiyouBeginCompare <= 0)
                                {
                                    // パターン4/6
                                    entity = this.CreateUpdateEntity(this.form.KobetsuHinmeiTankaDetail.Rows[i]);

                                    // SYS_ID
                                    entity.SYS_ID = Int64.Parse(this.SearchResult.Rows[i]["SYS_ID"].ToString());

                                    // TIME_STAMP
                                    if (!DBNull.Value.Equals(this.SearchResult.Rows[i]["TIME_STAMP"]))
                                    {
                                        entity.TIME_STAMP = (byte[])this.SearchResult.Rows[i]["TIME_STAMP"];
                                    }

                                    // システム自動プロパティ設定
                                    dataBinderLogic.SetSystemProperty(entity, false);

                                    this.dao.Update(entity);
                                }

                            }
                            else
                            {
                                // 適用開始日
                                //DateTime tekiyouBegin = DateTime.Parse(this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_BEGIN"].Value.ToString());
                                DateTime dtTekiyouBegin = DateTime.Parse(this.SearchResult.Rows[i]["TEKIYOU_BEGIN"].ToString());
                                // 適用終了日
                                //DateTime tekiyouEnd = DateTime.Parse(this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_END"].Value.ToString());
                                DateTime tekiyouEnd = DateTime.Parse(this.SearchResult.Rows[i]["TEKIYOU_END"].ToString());

                                //DateTime tekiyouBegin = DateTime.Parse(this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_BEGIN"].Value.ToString());
                                int tekiyouBeginCompare = ((DateTime.Parse(this.tekiyouBegin)).ToShortDateString()).CompareTo(dtTekiyouBegin.ToString("yyyy/MM/dd"));

                                //int tekiyouEndCompare = ((DateTime.Parse(this.form.TEKIYOU_BEGIN.Value.ToString())).ToShortDateString()).CompareTo(tekiyouEnd.ToString("yyyy/MM/dd"));
                                int tekiyouEndCompare = ((DateTime.Parse(this.tekiyouBegin)).ToShortDateString()).CompareTo(tekiyouEnd.ToString("yyyy/MM/dd"));


                                // パターン3
                                if ((tekiyouBeginCompare < 0) && (tekiyouEndCompare < 0))
                                {
                                    entity = this.CreateUpdateEntity(this.form.KobetsuHinmeiTankaDetail.Rows[i]);
                                    // SYS_ID
                                    entity.SYS_ID = Int64.Parse(this.SearchResult.Rows[i]["SYS_ID"].ToString());

                                    // TEKIYOU_END
                                    entity.TEKIYOU_END = DateTime.Parse(this.SearchResult.Rows[i]["TEKIYOU_END"].ToString());

                                    // TIME_STAMP
                                    if (!DBNull.Value.Equals(this.SearchResult.Rows[i]["TIME_STAMP"]))
                                    {
                                        entity.TIME_STAMP = (byte[])this.SearchResult.Rows[i]["TIME_STAMP"];
                                    }

                                    // システム自動プロパティ設定
                                    dataBinderLogic.SetSystemProperty(entity, false);

                                    this.dao.Update(entity);
                                }
                                // パターン5
                                else if ((tekiyouBeginCompare == 0) && (tekiyouEndCompare < 0))
                                {
                                    entity = this.CreateUpdateEntity(this.form.KobetsuHinmeiTankaDetail.Rows[i]);
                                    // SYS_ID
                                    entity.SYS_ID = Int64.Parse(this.SearchResult.Rows[i]["SYS_ID"].ToString());

                                    // TEKIYOU_END
                                    entity.TEKIYOU_END = DateTime.Parse(this.SearchResult.Rows[i]["TEKIYOU_END"].ToString());

                                    // TIME_STAMP
                                    if (!DBNull.Value.Equals(this.SearchResult.Rows[i]["TIME_STAMP"]))
                                    {
                                        entity.TIME_STAMP = (byte[])this.SearchResult.Rows[i]["TIME_STAMP"];
                                    }

                                    // システム自動プロパティ設定
                                    dataBinderLogic.SetSystemProperty(entity, false);

                                    this.dao.Update(entity);
                                }
                                // パターン7
                                else if ((tekiyouBeginCompare > 0) && (tekiyouEndCompare > 0))
                                {
                                    // パターン7
                                    entity = this.CreateInsertEntity(this.form.KobetsuHinmeiTankaDetail.Rows[i]);
                                    // 伝票区分設定
                                    entity.DENPYOU_KBN_CD = Int16.Parse(this.SearchResult.Rows[i]["DENPYOU_KBN_CD"].ToString());

                                    // 伝種区分設定
                                    entity.DENSHU_KBN_CD = Int16.Parse(this.SearchResult.Rows[i]["DENSHU_KBN_CD"].ToString());

                                    // システム自動プロパティ設定
                                    dataBinderLogic.SetSystemProperty(entity, false);

                                    this.dao.Insert(entity);
                                }
                            }
                        }

                        tran.Commit();
                    }

                    msgLogic.MessageBoxShow("I001", "個別品名単価の一括変更");
                    // 登録ボタン非活性
                    var parentForm = (MasterBaseForm)this.form.Parent;

                    parentForm.bt_func9.Enabled = false;
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
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Search

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {

            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (MasterBaseForm)this.form.Parent;

                var messageShowLogic = new MessageBoxShowLogic();


                if (false == this.CheckSearchCondition())
                {
                    LogUtility.DebugMethodEnd(-1);
                    return -1;
                }

                if (false == SetSearchString())
                {
                    LogUtility.DebugMethodEnd(-1);
                    return -1;
                }

                // テーブル作成
                this.SearchResult = new DataTable();


                this.SearchResult = dao.GetIchiranDataSql(this.SearchString
                                    , this.form.SHURUI_CD.Text
                                    , this.form.BUNRUI_CD.Text);

                ListHeaderForm header = (ListHeaderForm)parentForm.headerForm;


                if (this.SearchResult.Rows != null && this.SearchResult.Rows.Count > 0)
                {

                    count = this.SearchResult.Rows.Count;

                    DialogResult result = DialogResult.Yes;

                    // アラート表示
                    if (int.Parse(header.alertNumber.Text.Replace(",", "")) < count)
                    {
                        result = messageShowLogic.MessageBoxShow("C025");
                    }
                    if (result == DialogResult.Yes)
                    {
                        // 登録ボタンを活性
                        parentForm.bt_func9.Enabled = true;
                        // 一覧に検索データを設定
                        this.SetResultDataForDataGrid();

                        header.ReadDataNumber.Text = String.Format("{0:#,0}", this.SearchResult.Rows.Count);
                        // 内部保持データを保存
                        this.tankaZokgen = this.form.tankaZougen.Text;

                    }
                    else
                    {
                        this.form.KobetsuHinmeiTankaDetail.Rows.Clear();
                        //this.form.KobetsuHinmeiTankaDetail.Rows.Add();
                        // 登録ボタンを非活性
                        parentForm.bt_func9.Enabled = false;
                        // 読み込み件数初期化
                        header.ReadDataNumber.Text = "0";
                        LogUtility.DebugMethodEnd(count);
                        return count;

                    }
                }
                else
                {
                    this.form.KobetsuHinmeiTankaDetail.Rows.Clear();
                    //this.form.KobetsuHinmeiTankaDetail.Rows.Add();
                    parentForm.bt_func9.Enabled = false;
                    header.ReadDataNumber.Text = "0";
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }


                // 一覧データのチェック
                bool catchErr = true;
                this.errorFlag = this.CheckIchiranData(out catchErr);
                if (!catchErr)
                {
                    LogUtility.DebugMethodEnd(-1);
                    return -1;
                }

                if (this.errorFlag)
                {
                    messageShowLogic.MessageBoxShow("E056", "検索した明細にエラーがあるため、");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                count = -1;
            }
            LogUtility.DebugMethodEnd(count);

            return count;


        }

        #endregion

        #region Update

        public void Update(bool errorFlag)
        {

            throw new NotImplementedException();

        }

        #endregion

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

        #region 印刷処理

        /// <summary>
        /// 印刷処理
        /// </summary>
        internal bool Print()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if ((this.SearchResult.Rows == null) || (this.SearchResult.Rows.Count == 0))
                {
                    msgLogic.MessageBoxShow("E044");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // ヘッダー情報テーブル作成
                DataTable HeaderTable = this.CreateHeaderTable();

                // 見出し情報テーブル作成
                //DataTable PageHeader = this.CreatePageHeaderTable();

                // 明細情報テーブル作成
                DataTable DetailData = this.CreateReportData();

                ReportInfoR424 report_r424 = new ReportInfoR424(this.form.WindowId);

                //report_r424.DataTableList.Add("HeaderTable", HeaderTable);
                //report_r424.DataTableList.Add("PageHeader", PageHeader);
                //report_r424.DataTableList.Add("DetailData", DetailData);
                report_r424.DataTableList.Add("Header", HeaderTable);
                report_r424.DataTableList.Add("Detail", DetailData);


                //report_r424.Create(this.ReportFormXmlPath, "LAYOUT1", new DataTable());
                report_r424.Create(@".\Template\R424-Form.xml", "LAYOUT1", new DataTable());

                using (FormReportPrintPopup report = new FormReportPrintPopup(report_r424))
                {
                    report.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

                    report.ShowDialog();
                    report.Dispose();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Print", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Print", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region CSVファイル出力処理

        /// <summary>
        /// CSVファイル出力処理
        /// </summary>
        internal void OutputCsvFile()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if ((this.SearchResult.Rows == null) || (this.SearchResult.Rows.Count == 0))
            {
                msgLogic.MessageBoxShow("E044");
                return;
            }

            if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
            {

                // 2014/01/22 oonaka delete CSV出力はCustomGridViewに変換して行う start
                //// CSVファイル出力用クラスを初期化
                //if (outputLogic == null)
                //{
                //    outputLogic = new CSVFileLogic();
                //}

                //MultiRowIndexCreateLogic multirowLocationLogic = new MultiRowIndexCreateLogic();
                //multirowLocationLogic.multiRow = this.form.KobetsuHinmeiTankaDetail;

                //multirowLocationLogic.CreateLocations();


                //outputLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                //outputLogic.Detail = this.form.KobetsuHinmeiTankaDetail;

                //WINDOW_ID id = this.form.WindowId;

                //outputLogic.FileName = id.ToTitleString();
                //outputLogic.headerOutputFlag = true;

                //outputLogic.CreateCSVFile();
                // 2014/01/22 oonaka delete CSV出力はCustomGridViewに変換して行う end

                // 2014/01/22 oonaka add CSV出力はCustomGridViewに変換して行う start

                // CSV用グリッドにマルチローの表示データを移動
                this.form.gridCSV.DataSource = this.GetDataTableForMultRow();
                this.form.gridCSV.Refresh();

                // CSV出力実行
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertCustomDataGridViewToCsv(this.form.gridCSV, true, true, WINDOW_TITLEExt.ToTitleString(this.form.WindowId), this.form);

                // 2014/01/22 oonaka add CSV出力はCustomGridViewに変換して行う end
            }

            LogUtility.DebugMethodEnd();

        }

        // 2014/01/22 oonaka add CSV出力はCustomGridViewに変換して行う start

        /// <summary>
        /// マルチローの表示データをDataTable化
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTableForMultRow()
        {
            LogUtility.DebugMethodStart();

            // CSV用グリッドのバインド名からテーブル定義作成
            DataTable tbl = new DataTable();
            foreach (DataGridViewColumn col in this.form.gridCSV.Columns)
            {
                if (!string.IsNullOrWhiteSpace(col.DataPropertyName))
                {
                    tbl.Columns.Add(col.DataPropertyName, typeof(string));
                }
            }

            // マルチローから値を取得
            foreach (GrapeCity.Win.MultiRow.Row multiRow in this.form.KobetsuHinmeiTankaDetail.Rows)
            {
                DataRow row = tbl.NewRow();
                foreach (DataColumn col in tbl.Columns)
                {
                    row[col.ColumnName] = multiRow[col.ColumnName].FormattedValue;
                }
                tbl.Rows.Add(row);
            }

            // 返却
            LogUtility.DebugMethodEnd(tbl);
            return tbl;
        }

        // 2014/01/22 oonaka add CSV出力はCustomGridViewに変換して行う end


        #endregion

        #region 取消処理

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.GetSysInfoInit();

                this.ClearWindowInit();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Cancel", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region 検索項目チェック

        /// <summary>
        /// 検索項目チェック処理
        /// </summary>
        /// <returns></returns>
        public Boolean CheckSearchCondition()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if ((string.IsNullOrEmpty(this.form.SHURUI_CD.Text)) && (string.IsNullOrEmpty(this.form.BUNRUI_CD.Text))
                && (string.IsNullOrEmpty(this.form.HINMEI_CD.Text)))
            {

                msgLogic.MessageBoxShow("E012", "検索を実行するには、種類、分類、品名のいずれか１項目");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            if (string.IsNullOrEmpty(this.form.UNIT_CD.Text))
            {
                msgLogic.MessageBoxShow("E012", "検索を実行するには、単位");
                LogUtility.DebugMethodEnd(false);
                return false;
            }



            LogUtility.DebugMethodEnd(true);

            return true;
        }

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        /// <returns></returns>
        public Boolean SetSearchString()
        {
            LogUtility.DebugMethodStart();

            M_KOBETSU_HINMEI_TANKA entity = new M_KOBETSU_HINMEI_TANKA();

            this.tankaZokgen = string.Empty;
            this.tekiyouBegin = string.Empty;
            this.BunruiCd = string.Empty;
            this.shuruiCd = string.Empty;


            // 伝種区分
            if (this.form.denshuKbn.Text != "4")
            {
                entity.DENSHU_KBN_CD = SqlInt16.Parse(this.form.denshuKbn.Text);
            }

            // 伝票区分
            entity.DENPYOU_KBN_CD = SqlInt16.Parse(this.form.denpyouKbn.Text);

            // 取引先CD
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                entity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            }

            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                // 業者CD
                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            }

            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                // 現場CD
                entity.GENBA_CD = this.form.GENBA_CD.Text;
            }

            if (!string.IsNullOrEmpty(this.form.UNPAN_KAISHA_CD.Text))
            {
                // 運搬業者CD
                entity.UNPAN_GYOUSHA_CD = this.form.UNPAN_KAISHA_CD.Text;
            }


            if (!string.IsNullOrEmpty(this.form.NIOROSHI_KAISHA_CD.Text))
            {
                // 荷降業者CD
                entity.NIOROSHI_GYOUSHA_CD = this.form.NIOROSHI_KAISHA_CD.Text;
            }

            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
            {
                // 荷降現場CD
                entity.NIOROSHI_GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;
            }

            if (!string.IsNullOrEmpty(this.form.SHURUI_CD.Text))
            {
                // 種類CD
                this.shuruiCd = this.form.SHURUI_CD.Text;
            }

            if (!string.IsNullOrEmpty(this.form.BUNRUI_CD.Text))
            {
                // 分類CD
                this.BunruiCd = this.form.BUNRUI_CD.Text;
            }

            // 品名CD
            if (!string.IsNullOrEmpty(this.form.HINMEI_CD.Text))
            {

                entity.HINMEI_CD = this.form.HINMEI_CD.Text;
            }


            // 単位CD
            if (!string.IsNullOrEmpty(this.form.UNIT_CD.Text))
            {
                entity.UNIT_CD = SqlInt16.Parse(this.form.UNIT_CD.Text);
            }

            // 適用開始日
            if (this.form.TEKIYOU_BEGIN.Value != null)
            {
                this.tekiyouBegin = (DateTime.Parse(this.form.TEKIYOU_BEGIN.Value.ToString())).ToShortDateString();
            }

            this.SearchString = entity;

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        #endregion


        #region 検索結果を一覧に設定

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        public void SetResultDataForDataGrid()
        {
            LogUtility.DebugMethodStart();

            this.form.KobetsuHinmeiTankaDetail.Rows.Clear();
            for (int i = 0; i < this.SearchResult.Rows.Count; i++)
            {
                this.form.KobetsuHinmeiTankaDetail.Rows.Add();

                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["TORIHIKISAKI_CD"].Value = this.SearchResult.Rows[i]["TORIHIKISAKI_CD"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["TORIHIKISAKI_NAME_RYAKU"].Value = this.SearchResult.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString();

                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["GENBA_CD"].Value = this.SearchResult.Rows[i]["GENBA_CD"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["GENBA_NAME_RYAKU"].Value = this.SearchResult.Rows[i]["GENBA_NAME_RYAKU"].ToString();

                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["NIOROSHI_GYOUSHA_CD"].Value = this.SearchResult.Rows[i]["NIOROSHI_GYOUSHA_CD"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["NIOROSHI_GYOUSHA_NAME_RYAKU"].Value = this.SearchResult.Rows[i]["NIOROSHI_GYOUSHA_NAME_RYAKU"].ToString();

                int tanka = int.Parse(this.SearchResult.Rows[i]["TANKA"].ToString());
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["TANKA"].Value = String.Format("{0:#,0}", tanka);

                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["GYOUSHA_CD"].Value = this.SearchResult.Rows[i]["GYOUSHA_CD"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["GYOUSHA_NAME_RYAKU"].Value = this.SearchResult.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();

                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["UNPAN_GYOUSHA_CD"].Value = this.SearchResult.Rows[i]["UNPAN_GYOUSHA_CD"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["UNPAN_GYOUSHA_NAME_RYAKU"].Value = this.SearchResult.Rows[i]["UNPAN_GYOUSHA_NAME_RYAKU"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["NIOROSHI_GENBA_CD"].Value = this.SearchResult.Rows[i]["NIOROSHI_GENBA_CD"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["NIOROSHI_GENBA_NAME_RYAKU"].Value = this.SearchResult.Rows[i]["NIOROSHI_GENBA_NAME_RYAKU"].ToString();
                if (!string.IsNullOrEmpty(this.form.tankaZougen.Text))
                {
                    decimal tankaZougen = Convert.ToDecimal(this.form.tankaZougen.Text.Replace(",", ""));
                    this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_TANKA"].Value = String.Format("{0:#,0}", (tanka + tankaZougen));

                }
                else if (string.IsNullOrEmpty(this.form.tankaZougen.Text))
                {
                    this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["TEKIYOU_TANKA"].Value = DBNull.Value;

                }

                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["SHURUI_CD"].Value = this.SearchResult.Rows[i]["SHURUI_CD"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["SHURUI_NAME_RYAKU"].Value = this.SearchResult.Rows[i]["SHURUI_NAME_RYAKU"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["BUNRUI_CD"].Value = this.SearchResult.Rows[i]["BUNRUI_CD"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["BUNRUI_NAME_RYAKU"].Value = this.SearchResult.Rows[i]["BUNRUI_NAME_RYAKU"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["HINMEI_CD"].Value = this.SearchResult.Rows[i]["HINMEI_CD"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["HINMEI_NAME_RYAKU"].Value = this.SearchResult.Rows[i]["HINMEI_NAME_RYAKU"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["UNIT_CD"].Value = this.SearchResult.Rows[i]["UNIT_CD"].ToString();
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["UNIT_NAME_RYAKU"].Value = this.SearchResult.Rows[i]["UNIT_NAME_RYAKU"].ToString();

                // 2014/01/22 oonaka add BIKOUはスライド start
                this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["BIKOU"].Value = this.SearchResult.Rows[i]["BIKOU"].ToString();
                // 2014/01/22 oonaka add BIKOUはスライド end
            }

            LogUtility.DebugMethodEnd();

        }

        #endregion

        #region 帳票ヘッダーテーブル作成

        /// <summary>
        /// 帳票ヘッダーテーブル作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateHeaderTable()
        {
            LogUtility.DebugMethodStart();

            //DataTable FormHeader = new DataTable();
            DataTable dataTableTmp;
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Header";

            //FormHeader.Columns.Add("FH_CORP_RYAKU_NAME_VLB");
            //FormHeader.Columns.Add("FH_PRINT_DATE_VLB");
            //FormHeader.Columns.Add("FH_TITLE_FLB");
            //FormHeader.Columns.Add("FH_KYOTEN_NAME_VLB");
            dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
            dataTableTmp.Columns.Add("FH_PRINT_DATE_VLB");
            dataTableTmp.Columns.Add("FH_TITLE_FLB");
            dataTableTmp.Columns.Add("KYOTEN_NAME");
            dataTableTmp.Columns.Add("DENSHU_KBN_NAME");
            dataTableTmp.Columns.Add("DENPYOU_KBN_NAME");
            dataTableTmp.Columns.Add("TORIHIKISAKI_CD");
            dataTableTmp.Columns.Add("GYOUSHA_CD");
            dataTableTmp.Columns.Add("GENBA_CD");
            dataTableTmp.Columns.Add("UNPAN_GYOUSHA_CD");
            dataTableTmp.Columns.Add("NIOROSHI_GYOUSHA_CD");
            dataTableTmp.Columns.Add("NIOROSHI_GENBA_CD");
            dataTableTmp.Columns.Add("SHURUI_CD");
            dataTableTmp.Columns.Add("BUNRUI_CD");
            dataTableTmp.Columns.Add("HINMEI_CD");
            dataTableTmp.Columns.Add("UNIT_CD");


            DataRow row = dataTableTmp.NewRow();

            this.corpInfoEntity = corpInfoDao.GetAllData();

            if (corpInfoEntity != null)
            {
                row["CORP_RYAKU_NAME"] = corpInfoEntity[0].CORP_RYAKU_NAME;
            }

            row["FH_PRINT_DATE_VLB"] = DateTime.Now.ToString() + "発行";


            row["FH_TITLE_FLB"] = "単価変更対象一覧表";
            if (!string.IsNullOrEmpty(this.SearchString.TORIHIKISAKI_CD))
            {//拠点名を取得
                M_TORIHIKISAKI entity = new M_TORIHIKISAKI();
                DataTable table = new DataTable();
                entity.TORIHIKISAKI_CD = this.SearchString.TORIHIKISAKI_CD;

                table = this.torihikisakiDao.GetKyotenData(entity);

                if (table.Rows.Count > 0)
                {
                    row["KYOTEN_NAME"] = table.Rows[0]["KYOTEN_NAME_RYAKU"];
                }
            }
            else
            {
                //row["FH_KYOTEN_NAME_VLB"] = "全社";
                row["KYOTEN_NAME"] = "全社";
            }
            // 伝種区分名取得
            this.denshuKbnEntity = new M_DENSHU_KBN();
            if (!this.SearchString.DENSHU_KBN_CD.IsNull)
            {
                this.denshuKbnEntity = this.denshuKbnDao.GetDataByCd(this.SearchString.DENSHU_KBN_CD.ToString());

                if (this.denshuKbnEntity != null)
                {
                    //row["FH_DENSHU_KBN_NAME_CTL"] = denshuKbnEntity.DENSHU_KBN_NAME_RYAKU;
                    row["DENSHU_KBN_NAME"] = denshuKbnEntity.DENSHU_KBN_NAME_RYAKU;
                }

            }
            else
            {
                //row["FH_DENSHU_KBN_NAME_CTL"] = "全て";
                row["DENSHU_KBN_NAME"] = "全て";
            }
            // 伝票区分名取得
            this.denpyouKbnEntity = new M_DENPYOU_KBN();
            if (!this.SearchString.DENPYOU_KBN_CD.IsNull)
            {
                this.denpyouKbnEntity = this.denpyouKbnDao.GetDataByCd(this.SearchString.DENPYOU_KBN_CD.ToString());

                if (this.denpyouKbnEntity != null)
                {
                    //row["FH_DENPYOU_KBN_NAME_CTL"] = denpyouKbnEntity.DENPYOU_KBN_NAME_RYAKU;
                    row["DENPYOU_KBN_NAME"] = denpyouKbnEntity.DENPYOU_KBN_NAME_RYAKU;
                }
            }
            row["TORIHIKISAKI_CD"] = this.SearchString.TORIHIKISAKI_CD;
            //row["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
            //row["GENBA_CD"] = this.form.GENBA_CD.Text;
            //row["UNPAN_GYOUSHA_CD"] = this.form.UNPAN_KAISHA_CD.Text;
            //row["NIOROSHI_GYOUSHA_CD"] = this.form.NIOROSHI_KAISHA_CD.Text;
            //row["NIOROSHI_GENBA_CD"] = this.form.NIOROSHI_GENBA_CD.Text;
            //row["SHURUI_CD"] = this.form.SHURUI_CD.Text;
            //row["BUNRUI_CD"] = this.form.BUNRUI_CD.Text;
            //row["HINMEI_CD"] = this.form.HINMEI_CD.Text;
            //row["UNIT_CD"] = this.form.UNIT_CD.Text;
            row["GYOUSHA_CD"] = this.SearchString.GYOUSHA_CD;
            row["GENBA_CD"] = this.SearchString.GENBA_CD;
            row["UNPAN_GYOUSHA_CD"] = this.SearchString.UNPAN_GYOUSHA_CD;
            row["NIOROSHI_GYOUSHA_CD"] = this.SearchString.NIOROSHI_GYOUSHA_CD;
            row["NIOROSHI_GENBA_CD"] = this.SearchString.NIOROSHI_GENBA_CD;
            row["SHURUI_CD"] = this.shuruiCd;
            row["BUNRUI_CD"] = this.BunruiCd;
            row["HINMEI_CD"] = this.SearchString.HINMEI_CD;
            row["UNIT_CD"] = this.SearchString.UNIT_CD;

            //FormHeader.Rows.Add(row);
            dataTableTmp.Rows.Add(row);

            LogUtility.DebugMethodEnd(dataTableTmp);

            return dataTableTmp;

        }

        #endregion

        #region 帳票見出しテーブル作成

        /// <summary>
        /// 帳票見出しテーブル作成
        /// </summary>
        /// <returns></returns>
        //private DataTable CreatePageHeaderTable()
        //{
        //    LogUtility.DebugMethodStart();

        //    DataTable table = new DataTable();

        //table.Columns.Add("DENSHU_KBN_NAME");
        //table.Columns.Add("DENPYOU_KBN_NAME");
        //table.Columns.Add("FH_TORIHIKISAKI_CD_CTL");
        //table.Columns.Add("FH_GYOUSHA_CD_CTL");
        //table.Columns.Add("FH_GENBA_CD_CTL");
        //table.Columns.Add("FH_UNPAN_GYOUSHA_CD_CTL");
        //table.Columns.Add("FH_NIOROSHI_GYOUSHA_CD_CTL");
        //table.Columns.Add("FH_NIOROSHI_GENBA_CD_CTL");
        //table.Columns.Add("FH_SHURUI_CD_CTL");
        //table.Columns.Add("FH_BUNRUI_CD_CTL");
        //table.Columns.Add("FH_HINMEI_CD_CTL");
        //table.Columns.Add("FH_UNIT_CD_CTL");


        //DataRow row = table.NewRow();

        //// 伝種区分名取得
        //this.denshuKbnEntity = new M_DENSHU_KBN();
        //if (!this.SearchString.DENSHU_KBN_CD.IsNull)
        //{
        //    this.denshuKbnEntity = this.denshuKbnDao.GetDataByCd(this.SearchString.DENSHU_KBN_CD.ToString());

        //    if (this.denshuKbnEntity != null)
        //    {
        //        //row["FH_DENSHU_KBN_NAME_CTL"] = denshuKbnEntity.DENSHU_KBN_NAME_RYAKU;
        //        row["DENSHU_KBN_NAME"] = denshuKbnEntity.DENSHU_KBN_NAME_RYAKU;
        //    }

        //}
        //else
        //{
        //    //row["FH_DENSHU_KBN_NAME_CTL"] = "全て";
        //    row["DENSHU_KBN_NAMEL"] = "全て";
        //}
        //// 伝票区分名取得
        //this.denpyouKbnEntity = new M_DENPYOU_KBN();
        //if (!this.SearchString.DENPYOU_KBN_CD.IsNull)
        //{
        //    this.denpyouKbnEntity = this.denpyouKbnDao.GetDataByCd(this.SearchString.DENPYOU_KBN_CD.ToString());

        //    if (this.denpyouKbnEntity != null)
        //    {
        //        //row["FH_DENPYOU_KBN_NAME_CTL"] = denpyouKbnEntity.DENPYOU_KBN_NAME_RYAKU;
        //        row["DENPYOU_KBN_NAME"] = denpyouKbnEntity.DENPYOU_KBN_NAME_RYAKU;
        //    }
        //}

        //row["FH_TORIHIKISAKI_CD_CTL"] = this.SearchString.TORIHIKISAKI_CD;
        //row["FH_GYOUSHA_CD_CTL"] = this.SearchString.GYOUSHA_CD;
        //row["FH_GENBA_CD_CTL"] = this.SearchString.GENBA_CD;
        //row["FH_UNPAN_GYOUSHA_CD_CTL"] = this.SearchString.UNPAN_GYOUSHA_CD;
        //row["FH_NIOROSHI_GYOUSHA_CD_CTL"] = this.SearchString.NIOROSHI_GYOUSHA_CD;
        //row["FH_NIOROSHI_GENBA_CD_CTL"] = this.SearchString.NIOROSHI_GENBA_CD;
        //row["FH_SHURUI_CD_CTL"] = this.BunruiCd;
        //row["FH_BUNRUI_CD_CTL"] = this.shuruiCd;
        //row["FH_HINMEI_CD_CTL"] = this.SearchString.HINMEI_CD;
        //row["FH_UNIT_CD_CTL"] = this.SearchString.UNIT_CD;
        //    row["TORIHIKISAKI_CD"] = this.SearchString.TORIHIKISAKI_CD;
        //    row["GYOUSHA_CD"] = this.SearchString.GYOUSHA_CD;
        //    row["GENBA_CD"] = this.SearchString.GENBA_CD;
        //    row["UNPAN_GYOUSHA_CD"] = this.SearchString.UNPAN_GYOUSHA_CD;
        //    row["NIOROSHI_GYOUSHA_CD"] = this.SearchString.NIOROSHI_GYOUSHA_CD;
        //    row["NIOROSHI_GENBA_CD"] = this.SearchString.NIOROSHI_GENBA_CD;
        //    row["SHURUI_CD"] = this.BunruiCd;
        //    row["BUNRUI_CD"] = this.shuruiCd;
        //    row["HINMEI_CD"] = this.SearchString.HINMEI_CD;
        //    row["UNIT_CD"] = this.SearchString.UNIT_CD;

        //    table.Rows.Add(row);

        //    LogUtility.DebugMethodEnd(table);

        //    return table;
        //}

        #endregion

        #region 帳票明細データ作成

        /// <summary>
        /// 帳票明細データ作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateReportData()
        {
            LogUtility.DebugMethodStart();

            // 帳票用テーブル
            //DataTable reportTable = new DataTable();
            DataTable dataTableTmp;
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";

            // グループヘッダ部
            //reportTable.Columns.Add("DTL_TORIHIKISAKI_CD_CTL");
            //reportTable.Columns.Add("DTL_TORIHIKISAKI_NAME_CTL");
            //reportTable.Columns.Add("DTL_GENBA_CD_CTL");
            //reportTable.Columns.Add("DTL_GENBA_NAME_CTL");
            //reportTable.Columns.Add("DTL_NIOROSHI_GYOUSHA_CD_CTL");
            //reportTable.Columns.Add("DTL_NIOROSHI_GYOUSHA_NAME_CTL");
            //reportTable.Columns.Add("DTL_SHURUI_CD_CTL");
            //reportTable.Columns.Add("DTL_SHURUI_NAME_CTL");
            //reportTable.Columns.Add("DTL_HINMEI_CD_CTL");
            //reportTable.Columns.Add("DTL_HINMEI_NAME_CTL");
            //reportTable.Columns.Add("DTL_UNIT_NAME_CTL");
            //reportTable.Columns.Add("DTL_TEKIYOU_BEGIN_CTL");
            //reportTable.Columns.Add("DTL_GYOUSHA_CD_CTL");
            //reportTable.Columns.Add("DTL_GYOUSHA_NAME_CTL");
            //reportTable.Columns.Add("DTL_UNPAN_GYOUSHA_CD_CTL");
            //reportTable.Columns.Add("DTL_UNPAN_GYOUSHA_NAME_CTL");
            //reportTable.Columns.Add("DTL_NIOROSHI_GENBA_CD_CTL");
            //reportTable.Columns.Add("DTL_NIOROSHI_GENBA_NAME_CTL");
            //reportTable.Columns.Add("DTL_BUNRUI_CD_CTL");
            //reportTable.Columns.Add("DTL_BUNRUI_NAME_CTL");
            //reportTable.Columns.Add("DTL_TANKA_CTL");
            //reportTable.Columns.Add("DTL_ZOUGEN_TANKA_CTL");
            //reportTable.Columns.Add("DTL_TEKIYOU_TANKA_CTL");

            dataTableTmp.Columns.Add("TORIHIKISAKI_CD");
            dataTableTmp.Columns.Add("TORIHIKISAKI");
            dataTableTmp.Columns.Add("GENBA_CD");
            dataTableTmp.Columns.Add("GENBA");
            dataTableTmp.Columns.Add("NIOROSHI_GYOUSHA_CD");
            dataTableTmp.Columns.Add("NIOROSHI_GYOUSHA");
            dataTableTmp.Columns.Add("SHURUI_CD");
            dataTableTmp.Columns.Add("SHURUI");
            dataTableTmp.Columns.Add("HINMEI_CD");
            dataTableTmp.Columns.Add("HINMEI");
            dataTableTmp.Columns.Add("UNIT");
            dataTableTmp.Columns.Add("TEKIYOU_BEGIN");
            dataTableTmp.Columns.Add("GYOUSHA_CD");
            dataTableTmp.Columns.Add("GYOUSHA");
            dataTableTmp.Columns.Add("UNPAN_GYOUSHA_CD");
            dataTableTmp.Columns.Add("UNPAN_GYOUSHA");
            dataTableTmp.Columns.Add("NIOROSHI_GENBA_CD");
            dataTableTmp.Columns.Add("NIOROSHI_GENBA");
            dataTableTmp.Columns.Add("BUNRUI_CD");
            dataTableTmp.Columns.Add("BUNRUI");
            dataTableTmp.Columns.Add("TANKA");
            dataTableTmp.Columns.Add("ZOUGEN_TANKA");
            dataTableTmp.Columns.Add("TEKIYOU_TANKA");

            // 一覧データ設定
            for (int i = 0; i < this.form.KobetsuHinmeiTankaDetail.Rows.Count; i++)
            {

                DataRow row = dataTableTmp.NewRow();

                // 取引先CD
                //row["DTL_TORIHIKISAKI_CD_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["TORIHIKISAKI_CD"].Value.ToString();
                //row["DTL_TORIHIKISAKI_NAME_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].Value.ToString();
                row["TORIHIKISAKI_CD"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["TORIHIKISAKI_CD"].Value.ToString();
                row["TORIHIKISAKI"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].Value.ToString();

                // 現場
                //row["DTL_GENBA_CD_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["GENBA_CD"].Value.ToString();
                //row["DTL_GENBA_NAME_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["GENBA_NAME_RYAKU"].Value.ToString();
                row["GENBA_CD"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["GENBA_CD"].Value.ToString();
                row["GENBA"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["GENBA_NAME_RYAKU"].Value.ToString();

                // 荷降業者
                //row["DTL_NIOROSHI_GYOUSHA_CD_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["NIOROSHI_GYOUSHA_CD"].Value.ToString();
                //row["DTL_NIOROSHI_GYOUSHA_NAME_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["NIOROSHI_GYOUSHA_NAME_RYAKU"].Value.ToString();
                row["NIOROSHI_GYOUSHA_CD"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["NIOROSHI_GYOUSHA_CD"].Value.ToString();
                row["NIOROSHI_GYOUSHA"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["NIOROSHI_GYOUSHA_NAME_RYAKU"].Value.ToString();

                // 種類
                //row["DTL_SHURUI_CD_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["SHURUI_CD"].Value.ToString();
                //row["DTL_SHURUI_NAME_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["SHURUI_NAME_RYAKU"].Value.ToString();
                row["SHURUI_CD"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["SHURUI_CD"].Value.ToString();
                row["SHURUI"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["SHURUI_NAME_RYAKU"].Value.ToString();

                //// 品名
                //row["DTL_HINMEI_CD_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["HINMEI_CD"].Value.ToString();
                //row["DTL_HINMEI_NAME_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["HINMEI_NAME_RYAKU"].Value.ToString();
                row["HINMEI_CD"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["HINMEI_CD"].Value.ToString();
                row["HINMEI"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["HINMEI_NAME_RYAKU"].Value.ToString();

                // 単位
                //row["DTL_UNIT_NAME_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["UNIT_NAME_RYAKU"].Value.ToString();
                row["UNIT"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["UNIT_NAME_RYAKU"].Value.ToString();

                // 適用開始日
                //row["DTL_TEKIYOU_BEGIN_CTL"] = this.tekiyouBegin;
                row["TEKIYOU_BEGIN"] = this.tekiyouBegin;

                // 業者
                //row["DTL_GYOUSHA_CD_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["GYOUSHA_CD"].Value.ToString();
                //row["DTL_GYOUSHA_NAME_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["GYOUSHA_NAME_RYAKU"].Value.ToString();
                row["GYOUSHA_CD"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["GYOUSHA_CD"].Value.ToString();
                row["GYOUSHA"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["GYOUSHA_NAME_RYAKU"].Value.ToString();

                // 運搬業者
                //row["DTL_UNPAN_GYOUSHA_CD_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["UNPAN_GYOUSHA_CD"];
                //row["DTL_UNPAN_GYOUSHA_NAME_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["UNPAN_GYOUSHA_NAME_RYAKU"].Value.ToString();
                row["UNPAN_GYOUSHA_CD"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["UNPAN_GYOUSHA_CD"].Value.ToString();
                row["UNPAN_GYOUSHA"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["UNPAN_GYOUSHA_NAME_RYAKU"].Value.ToString();

                // 荷降現場
                //row["DTL_NIOROSHI_GENBA_CD_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["NIOROSHI_GENBA_CD"].Value.ToString();
                //row["DTL_NIOROSHI_GENBA_NAME_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["NIOROSHI_GENBA_NAME_RYAKU"].Value.ToString();
                row["NIOROSHI_GENBA_CD"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["NIOROSHI_GENBA_CD"].Value.ToString();
                row["NIOROSHI_GENBA"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["NIOROSHI_GENBA_NAME_RYAKU"].Value.ToString();

                // 分類
                //row["DTL_BUNRUI_CD_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["BUNRUI_CD"].Value.ToString();
                //row["DTL_BUNRUI_NAME_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["BUNRUI_NAME_RYAKU"].Value.ToString();
                row["BUNRUI_CD"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["BUNRUI_CD"].Value.ToString();
                row["BUNRUI"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["BUNRUI_NAME_RYAKU"].Value.ToString();

                // 単価
                //row["DTL_TANKA_CTL"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["TANKA"].Value.ToString();
                row["TANKA"] = this.form.KobetsuHinmeiTankaDetail.Rows[i]["TANKA"].Value.ToString();

                // 増減単価
                //row["DTL_ZOUGEN_TANKA_CTL"] = this.tankaZokgen;
                row["ZOUGEN_TANKA"] = this.tankaZokgen;

                // 適用単価
                //row["DTL_TEKIYOU_TANKA_CTL"] = string.Format("{0:#,0}", this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_TANKA"].Value.ToString());
                row["TEKIYOU_TANKA"] = string.Format("{0:#,0}", this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_TANKA"].Value.ToString());

                dataTableTmp.Rows.Add(row);
            }

            LogUtility.DebugMethodEnd(dataTableTmp);

            return dataTableTmp;

        }

        #endregion

        #region 件数チェック

        /// <summary>
        /// イベント前件数チェック
        /// </summary>
        /// <returns></returns>
        public bool ActionBeforeCheck()
        {
            LogUtility.DebugMethodStart();

            if ((this.SearchResult.Rows == null) || (this.SearchResult.Rows.Count == 0))
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        #endregion

        #region 登録前チェック

        /// <summary>
        /// 登録前チェック
        /// </summary>
        /// <returns></returns>
        public Boolean CheckBeforeUpdate()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();
                //if (string.IsNullOrEmpty(this.form.TEKIYOU_BEGIN.Text))
                if (this.form.TEKIYOU_BEGIN.Value == null)
                {

                    msgLogic.MessageBoxShow("E001", "適用開始日");

                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                if (string.IsNullOrEmpty(this.tekiyouBegin))
                {
                    msgLogic.MessageBoxShow("E057", "検索時に適用開始日が設定", "登録");

                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                if (DBNull.Value.Equals(this.form.KobetsuHinmeiTankaDetail.Rows[0].Cells["TEKIYOU_TANKA"].Value) ||
                    "".Equals(this.form.KobetsuHinmeiTankaDetail.Rows[0].Cells["TEKIYOU_TANKA"].Value.ToString().Trim()))
                {
                    msgLogic.MessageBoxShow("E057", "検索時に単価増減値が設定", "登録");

                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckBeforeUpdate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);

            return true;

        }

        #endregion

        #region Insertデータ作成

        /// <summary>
        /// 登録用データ作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal M_KOBETSU_HINMEI_TANKA CreateInsertEntity(GrapeCity.Win.MultiRow.Row row)
        {
            LogUtility.DebugMethodStart(row);

            M_KOBETSU_HINMEI_TANKA entity = new M_KOBETSU_HINMEI_TANKA();

            DataTable table = this.dao.GetMaxSysIDDataSql(entity);

            // システムID
            entity.SYS_ID = (Int64.Parse(table.Rows[0]["SYS_ID"].ToString())) + 1;

            // 伝票区分
            //entity.DENPYOU_KBN_CD = Int16.Parse(this.form.denpyouKbn.Text);

            // 取引先CD
            entity.TORIHIKISAKI_CD = row["TORIHIKISAKI_CD"].Value.ToString();
            //entity.TORIHIKISAKI_CD = this.SearchString.TORIHIKISAKI_CD;

            // 業者CD
            entity.GYOUSHA_CD = row["GYOUSHA_CD"].Value.ToString();
            //entity.GYOUSHA_CD = this.SearchString.GYOUSHA_CD;

            // 現場CD
            entity.GENBA_CD = row["GENBA_CD"].Value.ToString();
            //entity.GENBA_CD = this.SearchString.GENBA_CD;

            // 品名CD
            entity.HINMEI_CD = row["HINMEI_CD"].Value.ToString();
            //entity.HINMEI_CD = this.SearchString.HINMEI_CD;

            // 伝種区分CD
            //entity.DENSHU_KBN_CD = Int16.Parse(this.form.denshuKbn.Text);
            //entity.DENSHU_KBN_CD = this.SearchString.DENSHU_KBN_CD;

            // 単位CD
            entity.UNIT_CD = Int16.Parse(row["UNIT_CD"].Value.ToString());
            //entity.UNIT_CD = this.SearchString.UNIT_CD;

            // 運搬業者CD
            entity.UNPAN_GYOUSHA_CD = row["UNPAN_GYOUSHA_CD"].Value.ToString();
            //entity.UNPAN_GYOUSHA_CD = this.SearchString.UNPAN_GYOUSHA_CD;

            // 荷降業者CD
            entity.NIOROSHI_GYOUSHA_CD = row["NIOROSHI_GYOUSHA_CD"].Value.ToString();
            //entity.NIOROSHI_GYOUSHA_CD = this.SearchString.NIOROSHI_GYOUSHA_CD;

            // 荷降現場CD
            entity.NIOROSHI_GENBA_CD = row["NIOROSHI_GENBA_CD"].Value.ToString();
            //entity.NIOROSHI_GENBA_CD = this.SearchString.NIOROSHI_GENBA_CD;

            // 単価
            entity.TANKA = decimal.Parse(row["TEKIYOU_TANKA"].Value.ToString());
            //entity.TANKA = Double.Parse(row["TEKIYOU_TANKA"].Value.ToString());

            // 適用開始日
            entity.TEKIYOU_BEGIN = DateTime.Parse(this.tekiyouBegin);
            //entity.TEKIYOU_BEGIN = DateTime.Parse(this.form.TEKIYOU_BEGIN.Value.ToString());

            // 備考
            // 2014/01/22 oonaka delete BIKOUはスライド start
            //entity.BIKOU = null;
            // 2014/01/22 oonaka delete BIKOUはスライド end

            // 2014/01/22 oonaka add BIKOUはスライド start
            entity.BIKOU = row["BIKOU"].Value as string;
            // 2014/01/22 oonaka add BIKOUはスライド end

            // DELETE_Flg
            entity.DELETE_FLG = false;


            LogUtility.DebugMethodEnd(entity);

            return entity;

        }

        #endregion

        #region 更新データ作成

        /// <summary>
        /// 更新データ作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal M_KOBETSU_HINMEI_TANKA CreateUpdateEntity(GrapeCity.Win.MultiRow.Row row)
        {
            LogUtility.DebugMethodStart(row);

            M_KOBETSU_HINMEI_TANKA entity = new M_KOBETSU_HINMEI_TANKA();


            // システムID
            //entity.SYS_ID = Int16.Parse(row["SYS_ID"].Value.ToString());

            // 伝票区分
            //entity.DENPYOU_KBN_CD = Int16.Parse(this.form.denpyouKbn.Text);

            // 取引先CD
            entity.TORIHIKISAKI_CD = row["TORIHIKISAKI_CD"].Value.ToString();

            // 業者CD
            entity.GYOUSHA_CD = row["GYOUSHA_CD"].Value.ToString();

            // 現場CD
            entity.GENBA_CD = row["GENBA_CD"].Value.ToString();

            // 品名CD
            entity.HINMEI_CD = row["HINMEI_CD"].Value.ToString();

            // 伝種区分CD
            //entity.DENSHU_KBN_CD = Int16.Parse(this.form.denshuKbn.Text);

            // 単位CD
            entity.UNIT_CD = Int16.Parse(row["UNIT_CD"].Value.ToString());

            // 運搬業者CD
            entity.UNPAN_GYOUSHA_CD = row["UNPAN_GYOUSHA_CD"].Value.ToString();

            // 荷降業者CD
            entity.NIOROSHI_GYOUSHA_CD = row["NIOROSHI_GYOUSHA_CD"].Value.ToString();

            // 荷降現場CD
            entity.NIOROSHI_GENBA_CD = row["NIOROSHI_GENBA_CD"].Value.ToString();

            // 単価
            entity.TANKA = decimal.Parse(row["TEKIYOU_TANKA"].Value.ToString());

            // 適用開始日
            //entity.TEKIYOU_BEGIN = DateTime.Parse(this.form.TEKIYOU_BEGIN.Value.ToString());
            entity.TEKIYOU_BEGIN = DateTime.Parse(this.tekiyouBegin);

            //// 適用終了日
            //if (!DBNull.Value.Equals(row["TEKIYOU_END"]))
            //{
            //    entity.TEKIYOU_END = DateTime.Parse(row["TEKIYOU_END"].Value.ToString());
            //}

            // 削除フラグ
            entity.DELETE_FLG = false;

            LogUtility.DebugMethodEnd(entity);

            return entity;
        }

        #endregion

        #region 適用終了日更新Entity作成

        /// <summary>
        /// 適用終了日更新用Entity作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal M_KOBETSU_HINMEI_TANKA CreateUpdateTekiyouEndEntity(GrapeCity.Win.MultiRow.Row row)
        {
            LogUtility.DebugMethodStart(row);

            M_KOBETSU_HINMEI_TANKA entity = new M_KOBETSU_HINMEI_TANKA();

            // 伝票区分
            //entity.DENPYOU_KBN_CD = Int16.Parse(this.form.denpyouKbn.Text);

            // 取引先CD
            entity.TORIHIKISAKI_CD = row["TORIHIKISAKI_CD"].Value.ToString();

            // 業者CD
            entity.GYOUSHA_CD = row["GYOUSHA_CD"].Value.ToString();

            // 現場CD
            entity.GENBA_CD = row["GENBA_CD"].Value.ToString();

            // 品名CD
            entity.HINMEI_CD = row["HINMEI_CD"].Value.ToString();

            // 伝種区分CD
            //entity.DENSHU_KBN_CD = Int16.Parse(this.form.denshuKbn.Text);

            // 単位CD
            entity.UNIT_CD = Int16.Parse(row["UNIT_CD"].Value.ToString());

            // 運搬業者CD
            entity.UNPAN_GYOUSHA_CD = row["UNPAN_GYOUSHA_CD"].Value.ToString();

            // 荷降業者CD
            entity.NIOROSHI_GYOUSHA_CD = row["NIOROSHI_GYOUSHA_CD"].Value.ToString();

            // 荷降現場CD
            entity.NIOROSHI_GENBA_CD = row["NIOROSHI_GENBA_CD"].Value.ToString();

            // 単価
            entity.TANKA = decimal.Parse(row["TANKA"].Value.ToString());

            // 適用開始日
            //entity.TEKIYOU_BEGIN = DateTime.Parse(this.form.TEKIYOU_BEGIN.Value.ToString());
            entity.TEKIYOU_BEGIN = DateTime.Parse(this.tekiyouBegin);

            // 適用終了日
            //entity.TEKIYOU_END = DateTime.Parse(this.form.TEKIYOU_BEGIN.Value.ToString()).AddDays(-1);
            entity.TEKIYOU_END = DateTime.Parse(this.tekiyouBegin).AddDays(-1);

            // DELETE_Flg
            entity.DELETE_FLG = false;


            LogUtility.DebugMethodEnd(entity);

            return entity;
        }

        #endregion

        #region 明細データチェック

        /// <summary>
        /// 明細データのチェック
        /// </summary>
        /// <returns></returns>
        public Boolean CheckIchiranData(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            this.errorFlag = false;
            catchErr = true;

            try
            {
                for (int i = 0; i < this.form.KobetsuHinmeiTankaDetail.Rows.Count; i++)
                {
                    /* 不具合管理表No4529により、単価のマイナスチェックは削除
                    if (!DBNull.Value.Equals(this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_TANKA"].Value))
                    {
                        if (int.Parse(this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_TANKA"].Value.ToString().Replace(",", "")) < 0)
                        {
                            this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_TANKA"].Style.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                            this.errorFlag = true;
                        }
                        else
                        {
                            //this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_TANKA"].Style.BackColor = r_framework.Const.Constans.NOMAL_COLOR;
                            this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_TANKA"].Style.BackColor = r_framework.Const.Constans.READONLY_COLOR;
                        }
                    }
                    */

                    if (!string.IsNullOrEmpty(this.tekiyouBegin))
                    {
                        // パターン1の確認
                        //DateTime tekiyouBegin =DateTime.Parse(this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_BEGIN"].Value.ToString());
                        DateTime dtTekiyouBegin = DateTime.Parse(this.SearchResult.Rows[i]["TEKIYOU_BEGIN"].ToString());
                        DateTime tekiyouEnd;
                        int tekiyouEndCompare = 0;
                        //if (!DBNull.Value.Equals(this.form.KobetsuHinmeiTankaDetail.Rows[i].Cells["TEKIYOU_END"].Value))
                        if (!DBNull.Value.Equals(this.SearchResult.Rows[i]["TEKIYOU_END"]))
                        {
                            //tekiyouEnd = DateTime.Parse((this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_END"].Value.ToString()));
                            tekiyouEnd = DateTime.Parse((this.SearchResult.Rows[i]["TEKIYOU_END"].ToString()));

                            //tekiyouEndCompare = ((DateTime.Parse(this.form.TEKIYOU_BEGIN.Value.ToString())).ToShortDateString()).CompareTo(tekiyouEnd.ToString("yyyy/MM/dd"));
                            tekiyouEndCompare = ((DateTime.Parse(this.tekiyouBegin)).ToShortDateString()).CompareTo(tekiyouEnd.ToString("yyyy/MM/dd"));

                        }

                        //int tekiyouBeginCompare = ((DateTime.Parse(this.form.TEKIYOU_BEGIN.Value.ToString())).ToShortDateString()).CompareTo(tekiyouBegin.ToString("yyyy/MM/dd"));
                        int tekiyouBeginCompare = ((DateTime.Parse(this.tekiyouBegin)).ToShortDateString()).CompareTo(dtTekiyouBegin.ToString("yyyy/MM/dd"));
                        if ((tekiyouBeginCompare > 0) && (tekiyouEndCompare < 0))
                        {
                            // パターン1の場合は、1明細を赤くする
                            this.form.KobetsuHinmeiTankaDetail.Rows[i].BackColor = r_framework.Const.Constans.ERROR_COLOR;
                            // 単価がOKの場合、なぜか赤くならないので赤くする
                            this.form.KobetsuHinmeiTankaDetail.Rows[i]["TEKIYOU_TANKA"].Style.BackColor = r_framework.Const.Constans.ERROR_COLOR;

                            this.errorFlag = true;
                        }
                        else
                        {
                            this.form.KobetsuHinmeiTankaDetail.Rows[i].BackColor = r_framework.Const.Constans.NOMAL_COLOR;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckIchiranData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(errorFlag, catchErr);

            return errorFlag;

        }

        #endregion

        //#region 現場ロストフォーカスイベント処理

        ///// <summary>
        ///// 現場ロストフォーカスイベント処理
        ///// </summary>
        //internal void GenbaLostFocus()
        //{
        //    LogUtility.DebugMethodStart();

        //    var msgLogic = new MessageBoxShowLogic();

        //    if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
        //    {
        //        if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
        //        {
        //            msgLogic.MessageBoxShow("E012", "先に業者CD");
        //            this.form.GENBA_CD.Focus();

        //        }
        //        else
        //        {
        //            M_GENBA entity = new M_GENBA();
        //            entity.GENBA_CD = this.form.GENBA_CD.Text.PadLeft(6, '0');
        //            entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text.PadLeft(6, '0');

        //            entity = this.genbaDao.GetGenbaData(entity);

        //            if (entity != null)
        //            {
        //                this.form.GENBA_NAME_RYAKU.Text = entity.GENBA_NAME_RYAKU;

        //            }
        //            else
        //            {
        //                msgLogic.MessageBoxShow("E020", "現場");
        //                this.form.GENBA_CD.Focus();
        //                this.form.GENBA_CD.IsInputErrorOccured = true;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        this.form.GENBA_NAME_RYAKU.Text = string.Empty;

        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        //#endregion

        //#region 運搬会社ロストフォーカスイベント

        ///// <summary>
        ///// 運搬会社ロストフォーカスイベント
        ///// </summary>
        //internal void UnpanKaishaLostFocus()
        //{

        //    LogUtility.DebugMethodStart();
        //    var msgLogic = new MessageBoxShowLogic();

        //    if (!string.IsNullOrEmpty(this.form.UNPAN_KAISHA_CD.Text))
        //    {
        //        M_GYOUSHA entity = new M_GYOUSHA();

        //        entity.GYOUSHA_CD = this.form.UNPAN_KAISHA_CD.Text;

        //        entity = this.gyoushaDao.GetUnpanGyoushaData(entity.GYOUSHA_CD.PadLeft(6, '0'));

        //        if (entity != null)
        //        {
        //            this.form.UNPAN_KAISHA_NAME_RYAKU.Text = entity.GYOUSHA_NAME_RYAKU;
        //        }
        //        else
        //        {
        //            msgLogic.MessageBoxShow("E020", "業者");
        //            this.form.UNPAN_KAISHA_CD.Focus();
        //            this.form.UNPAN_KAISHA_CD.IsInputErrorOccured = true;
        //        }

        //    }
        //    else
        //    {
        //        this.form.UNPAN_KAISHA_NAME_RYAKU.Text = string.Empty;
        //    }

        //    LogUtility.DebugMethodEnd();

        //}

        //#endregion

        //#region 荷降業者CDロストフォーカスイベント処理

        ///// <summary>
        ///// 荷降業者CDロストフォーカスイベント処理
        ///// </summary>
        //internal void NioroshiKaishaLostFocus()
        //{
        //    LogUtility.DebugMethodStart();

        //    var msgLogic = new MessageBoxShowLogic();

        //    if (!string.IsNullOrEmpty(this.form.NIOROSHI_KAISHA_CD.Text))
        //    {
        //        M_GYOUSHA entity = new M_GYOUSHA();

        //        entity.GYOUSHA_CD = this.form.NIOROSHI_KAISHA_CD.Text;

        //        entity = this.gyoushaDao.GetNioroshiGyoushaData(entity.GYOUSHA_CD.PadLeft(6, '0'));

        //        if (entity != null)
        //        {
        //            this.form.NIOROSHI_KAISHA_NAME_RYAKU.Text = entity.GYOUSHA_NAME_RYAKU;
        //        }
        //        else
        //        {
        //            msgLogic.MessageBoxShow("E020", "業者");
        //            this.form.NIOROSHI_KAISHA_CD.Focus();
        //            this.form.NIOROSHI_KAISHA_CD.IsInputErrorOccured = true;
        //        }

        //    }
        //    else
        //    {
        //        this.form.NIOROSHI_KAISHA_NAME_RYAKU.Text = string.Empty;
        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        //#endregion

        //#region 荷降現場CDロストフォーカスイベント処理

        ///// <summary>
        ///// 荷降現場CDロストフォーカスイベント処理
        ///// </summary>
        //internal void NioroshiGenbaLostFocus()
        //{

        //    LogUtility.DebugMethodStart();

        //    var msgLogic = new MessageBoxShowLogic();

        //    if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
        //    {
        //        if (string.IsNullOrEmpty(this.form.NIOROSHI_KAISHA_CD.Text))
        //        {
        //            msgLogic.MessageBoxShow("E012", "先に荷降業者CD");
        //            this.form.NIOROSHI_GENBA_CD.Focus();
        //        }
        //        else
        //        {
        //            M_GENBA entity = new M_GENBA();
        //            entity.GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text.PadLeft(6, '0');
        //            entity.GYOUSHA_CD = this.form.NIOROSHI_KAISHA_CD.Text.PadLeft(6, '0');

        //            entity = this.genbaDao.GetUnpanGenbaData(entity);

        //            if (entity != null)
        //            {
        //                this.form.NIOROSHI_GENBA_NAME_RYAKU.Text = entity.GENBA_NAME_RYAKU;

        //            }
        //            else
        //            {
        //                msgLogic.MessageBoxShow("E020", "現場");
        //                this.form.NIOROSHI_GENBA_CD.Focus();
        //                this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;

        //            }

        //        }
        //    }
        //    else
        //    {
        //        this.form.NIOROSHI_GENBA_NAME_RYAKU.Text = string.Empty;

        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        //#endregion

        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
        /// <summary>
        /// 業者情報設定
        /// </summary>
        internal bool SetGyoushaInfo()
        {
            try
            {
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text)) { return true; }

                M_GYOUSHA entity = new M_GYOUSHA();
                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                var entityList = this.gyoushaDao.GetAllValidData(entity);
                if (entityList != null && entityList.Length > 0)
                {
                    // 取引先を再設定
                    // 取引先を取得
                    M_TORIHIKISAKI toriEntity = new M_TORIHIKISAKI();
                    toriEntity.TORIHIKISAKI_CD = entityList[0].TORIHIKISAKI_CD;
                    var torihikisaki = this.toriDao.GetAllValidData(toriEntity);
                    if (torihikisaki != null && torihikisaki.Length > 0)
                    {
                        this.form.TORIHIKISAKI_CD.Text = torihikisaki[0].TORIHIKISAKI_CD;
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki[0].TORIHIKISAKI_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGyoushaInfo", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGyoushaInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 現場情報設定
        /// </summary>
        internal bool SetGenbaInfo()
        {
            try
            {
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) || string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    return true;
                }

                M_GENBA entity = new M_GENBA();
                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                entity.GENBA_CD = this.form.GENBA_CD.Text;
                var entityList = this.genbaDao.GetAllValidData(entity);
                if (entityList != null && entityList.Length > 0)
                {
                    // 取引先を再設定
                    // 取引先を取得
                    M_TORIHIKISAKI toriEntity = new M_TORIHIKISAKI();
                    toriEntity.TORIHIKISAKI_CD = entityList[0].TORIHIKISAKI_CD;
                    var torihikisaki = this.toriDao.GetAllValidData(toriEntity);
                    if (torihikisaki != null && torihikisaki.Length > 0)
                    {
                        this.form.TORIHIKISAKI_CD.Text = torihikisaki[0].TORIHIKISAKI_CD;
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki[0].TORIHIKISAKI_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGenbaInfo", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGenbaInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
    }
}
