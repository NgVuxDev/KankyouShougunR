using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.PaperManifest.ManifestImport.Const;
using Shougun.Core.PaperManifest.ManifestImport.DAO;

namespace Shougun.Core.PaperManifest.ManifestImport
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// 画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ParentForm
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// マニフェストエントリDao
        /// </summary>
        private ManifestEntryDaoClass manifestEntryDao;

        /// <summary>
        /// マニフェスト運搬Dao
        /// </summary>
        private ManifestUpnDaoClass manifestUpnDao;

        /// <summary>
        /// T_MANIFEST_RET_DATEDao
        /// </summary>
        private T_MANIFEST_RELATIONDaoCls manifestRetDateDao;

        /// <summary>マニ印字_建廃_形状更新Dao</summary>
        private KeijyouDaoCls KeijyouDao;

        /// <summary>マニ印字_建廃_荷姿更新Dao</summary>
        private NisugataDaoCls NisugataDao;

        /// <summary>マニ印字_建廃_処分方法更新Dao</summary>
        private SbnHouhouDaoCls SbnHouhouDao;

        /// <summary>
        /// マニフェスト明細Dao
        /// </summary>
        private ManifestDetailDaoClass manifestDetailDao;

        /// <summary>
        /// 電マニフェストエントリDao
        /// </summary>
        private DenManiDaoClass denManiDao;

        /// <summary>
        /// 電マニフェストエントリDao
        /// </summary>
        private DenManiRelationDaoClass denManiRelationDao;

        /// <summary>
        /// 全てマニ検索処理用Dao
        /// </summary>
        private PaperAndElecDaoCls PaperAndElecManiDao;

        /// <summary>タイムスタンプ</summary>
        private byte[] timeStampEntry = null;

        /// <summary>マニ返却日</summary>
        private byte[] timeStampRetDate = null;

        // 数量の合計
        private SqlDecimal totalSuu = 0;

        // 換算後数量の合計
        private SqlDecimal totalKansanSuu = 0;

        // 減容後数量の合計
        private SqlDecimal totalGennyouSuu = 0;

        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allControl;

        /// <summary>
        /// 取得した業者エンティティを保持する
        /// </summary>
        private List<M_GYOUSHA> gyoushaList = new List<M_GYOUSHA>();

        /// <summary>
        /// 取得した業者データを保持する
        /// </summary>
        private DataTable gyoushaIchiran = new DataTable();

        /// <summary>
        /// 取得した現場エンティティを保持する
        /// </summary>
        private List<M_GENBA> genbaList = new List<M_GENBA>();

        /// <summary>
        /// 取得した現場データを保持する
        /// </summary>
        private DataTable genbaIchiran = new DataTable();

        /// <summary>
        /// 取得した社員エンティティを保持する
        /// </summary>
        private List<M_SHAIN> shainList = new List<M_SHAIN>();

        /// <summary>
        /// 取得した車種エンティティを保持する
        /// </summary>
        private List<M_SHASHU> shashuList = new List<M_SHASHU>();

        /// <summary>
        /// 取得した種類エンティティを保持する
        /// </summary>
        private List<M_SHURUI> shuruiList = new List<M_SHURUI>();

        /// <summary>
        /// 取得した廃棄物種類エンティティを保持する
        /// </summary>
        private List<M_HAIKI_SHURUI> haikiShuruiList = new List<M_HAIKI_SHURUI>();

        /// <summary>
        /// 取得した廃棄物の名称エンティティを保持する
        /// </summary>
        private List<M_HAIKI_NAME> haikiNameList = new List<M_HAIKI_NAME>();

        /// <summary>
        /// 取得した荷姿エンティティを保持する
        /// </summary>
        private List<M_NISUGATA> nisugataList = new List<M_NISUGATA>();

        /// 取得した運搬方法エンティティを保持する
        /// </summary>
        private List<M_UNPAN_HOUHOU> unpanHouhouList = new List<M_UNPAN_HOUHOU>();

        /// 取得した運搬方法エンティティを保持する
        /// </summary>
        private List<M_SHOBUN_HOUHOU> shobunHouhouList = new List<M_SHOBUN_HOUHOU>();

        /// <summary>
        /// 取得した有害物質エンティティを保持する
        /// </summary>
        private List<M_YUUGAI_BUSSHITSU> yuugaiBusshitsuList = new List<M_YUUGAI_BUSSHITSU>();

        /// <summary>
        /// 取得した車輌エンティティを保持する
        /// </summary>
        private List<M_SHARYOU> sharyouList = new List<M_SHARYOU>();

        /// <summary>
        /// 取得した拠点エンティティを保持する
        /// </summary>
        private List<M_KYOTEN> kyotenList = new List<M_KYOTEN>();

        /// <summary>
        /// 取得した単位エンティティを保持する
        /// </summary>
        private List<M_UNIT> unitList = new List<M_UNIT>();

        /// <summary>
        /// 取得した都道府県エンティティを保持する
        /// </summary>
        private List<M_TODOUFUKEN> todoufukenList = new List<M_TODOUFUKEN>();

        /// <summary>
        /// 取得した形状データを保持する
        /// </summary>
        private DataTable keijouIchiran = new DataTable();

        /// <summary>
        /// 取得した荷姿データを保持する
        /// </summary>
        private DataTable nisugataIchiran = new DataTable();

        /// <summary>共通メッセージ</summary>
        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>システム設定</summary>
        M_SYS_INFO mSysInfo = null;

        /// <summary>共通</summary>
        private Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        /// <summary>個別メッセージ</summary>
        private MessageLogic localMsgLogic = null;

        /// <summary>
        /// マニフェスト伝票リスト
        /// </summary>
        List<string[]> listDenpyouManifest = new List<string[]>();

        /// <summary>
        /// マニフェスト紐付リスト
        /// </summary>
        List<RelationInfo_DTOCls> listRelationInfo = new List<RelationInfo_DTOCls>();

        /// <summary>
        /// マニフェスト伝票重複リスト
        /// </summary>
        Dictionary<int, string> dicValidator = new Dictionary<int, string>();

        /// <summary>
        /// マニフェスト伝票重複リスト
        /// </summary>
        Dictionary<int, string> CheckMainData = new Dictionary<int, string>();

        /// <summary>
        /// INSERTすべき紐付情報
        /// ※システムIDとかwhoカラムは空なので登録前にセット必要
        /// </summary>
        public T_MANIFEST_RELATION[] regist_relations;

        /// <summary>
        /// 紐づけられた明細に対応するEntry
        /// ※タイムスタンプ付　更新日未セット。空更新して楽観排他を行う。
        /// </summary>
        public T_MANIFEST_ENTRY[] paperEntries;

        /// <summary>
        /// 紐づけられた明細に対応するEntry（電子）
        /// ※タイムスタンプ付　更新日未セット。EXが無い場合は作成する。
        /// </summary>
        public DT_R18_EX[] elecEntriesIns;

        /// <summary>
        /// 紐づけられた明細に対応するEntry（電子）
        /// ※タイムスタンプ付　更新日未セット。空更新して楽観排他を行う。
        /// </summary>
        public DT_R18_EX[] elecEntriesUpd;

        /// <summary>
        /// 論理削除すべき紐付情報
        /// ※タイムスタンプ付 削除フラグセット済み。更新日未セット。 楽観排他行う。</summary>
        public T_MANIFEST_RELATION[] delete_relations;

        /// <summary>
        /// 現在の紐付テーブル情報（論理削除用）
        /// 検索するたびに取得しなおすこと。論理削除のためにとっておきます。
        /// </summary>
        private T_MANIFEST_RELATION[] currentRelation;

        /// <summary>
        /// 紐付1次Table
        /// </summary>
        private DataTable firstDt;

        /// <summary>
        /// 紐付2次Table
        /// </summary>
        private DataTable secondDt;

        /// <summary>
        /// マニフェスト紐付情報
        /// </summary>
        private RelationInfo_DTOCls relationInfoDto;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">対象フォーム</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.manifestEntryDao = DaoInitUtility.GetComponent<ManifestEntryDaoClass>();
            this.manifestUpnDao = DaoInitUtility.GetComponent<ManifestUpnDaoClass>();
            this.manifestRetDateDao = DaoInitUtility.GetComponent<T_MANIFEST_RELATIONDaoCls>();
            this.KeijyouDao = DaoInitUtility.GetComponent<KeijyouDaoCls>();
            this.NisugataDao = DaoInitUtility.GetComponent<NisugataDaoCls>();
            this.SbnHouhouDao = DaoInitUtility.GetComponent<SbnHouhouDaoCls>();
            this.manifestDetailDao = DaoInitUtility.GetComponent<ManifestDetailDaoClass>();
            this.denManiDao = DaoInitUtility.GetComponent<DenManiDaoClass>();
            this.denManiRelationDao = DaoInitUtility.GetComponent<DenManiRelationDaoClass>();
            this.PaperAndElecManiDao = DaoInitUtility.GetComponent<PaperAndElecDaoCls>();
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
            this.localMsgLogic = new MessageLogic();

            this.getDefaultList();
            mSysInfo = this.GetSysInfo();

            LogUtility.DebugMethodEnd(targetForm);
        }

        /// <summary>
        /// SYS_INFOを取得する
        /// </summary>
        private M_SYS_INFO GetSysInfo()
        {
            IM_SYS_INFODao sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();
            M_SYS_INFO[] returnEntity = sysInfoDao.GetAllData();
            return returnEntity[0];
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            // ParentFormのSet
            parentForm = (BusinessBaseForm)this.form.Parent;

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            this.allControl = this.form.allControl;

            // 画面表示初期化
            this.SetInitDisp();

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
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            var tmp = buttonSetting.LoadButtonSetting(thisAssembly, Const.UIConstans.ButtonInfoXmlPath);
            return tmp;
        }

        #endregion

        #region イベントの初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // 対象マニフェスト種別イベント
            this.form.txtHaikiKbn.TextChanged += new EventHandler(txtHaikiKbn_TextChanged);

            // ファンクションキーイベント
            this.parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);
            this.form.C_Regist(parentForm.bt_func9);
            this.parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);
            this.parentForm.bt_func11.Click += new EventHandler(bt_func11_Click);
            this.parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

            this.form.btnBrowseChokko.Click += new EventHandler(btnBrowseChokko_Click);
            this.form.btnBrowseTsumikae.Click += new EventHandler(btnBrowseTsumikae_Click);
            this.form.btnBrowseKenpai.Click += new EventHandler(btnBrowseKenpai_Click);
            this.form.btnBrowseManiHimoduke.Click += new EventHandler(btnBrowseManiHimoduke_Click);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 参照ボタンのイベント
        /// <summary>
        /// 産廃(直行)の参照ボタンのイベント
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseChokko_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // CSVファイルを選択する。
            var filePath = fileSelectHandler();

            if (false == String.IsNullOrEmpty(filePath))
            {
                this.form.txtFilePathChokko.Text = filePath;
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 産廃(積替)の参照ボタンのイベント
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseTsumikae_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // CSVファイルを選択する。
            var filePath = fileSelectHandler();

            if (false == String.IsNullOrEmpty(filePath))
            {
                this.form.txtFilePathTsumikae.Text = filePath;
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 建廃の参照ボタンのイベント
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseKenpai_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // CSVファイルを選択する。
            var filePath = fileSelectHandler();

            if (false == String.IsNullOrEmpty(filePath))
            {
                this.form.txtFilePathKenpai.Text = filePath;
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// マニ紐付の参照ボタンのイベント
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseManiHimoduke_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // CSVファイルを選択する。
            var filePath = fileSelectHandler();

            if (false == String.IsNullOrEmpty(filePath))
            {
                this.form.txtFilePathManiHimoduke.Text = filePath;
            }

            LogUtility.DebugMethodEnd(sender, e);
        }
        #endregion

        #region CSVファイル選択処理
        /// <summary>
        /// CSVファイル選択処理
        /// </summary>
        private String fileSelectHandler()
        {
            var title = "取り込むCSVファイルを選択してください";
            var initialPath = @"C:\Temp";

            if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
            {
                initialPath = this.form.txtFilePathChokko.Text;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
            {
                initialPath = this.form.txtFilePathTsumikae.Text;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
            {
                initialPath = this.form.txtFilePathKenpai.Text;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
            {
                initialPath = this.form.txtFilePathManiHimoduke.Text;
            }

            var windowHandle = this.form.Handle;
            var isFileSelect = true;
            var isTerminalMode = SystemProperty.IsTerminalMode;
            var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
            var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

            return filePath;
        }
        #endregion

        #region 対象マニフェスト種別（テキストボックス）イベント
        /// <summary>
        /// 対象マニフェスト種別（テキストボックス）イベント
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void txtHaikiKbn_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
            {
                this.form.txtFilePathChokko.Enabled = true;
                this.form.btnBrowseChokko.Enabled = true;

                this.form.txtFilePathTsumikae.Enabled = false;
                this.form.btnBrowseTsumikae.Enabled = false;

                this.form.txtFilePathKenpai.Enabled = false;
                this.form.btnBrowseKenpai.Enabled = false;

                this.form.txtFilePathManiHimoduke.Enabled = false;
                this.form.btnBrowseManiHimoduke.Enabled = false;

                this.form.txtError.Enabled = true;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
            {
                this.form.txtFilePathChokko.Enabled = false;
                this.form.btnBrowseChokko.Enabled = false;

                this.form.txtFilePathTsumikae.Enabled = true;
                this.form.btnBrowseTsumikae.Enabled = true;

                this.form.txtFilePathKenpai.Enabled = false;
                this.form.btnBrowseKenpai.Enabled = false;

                this.form.txtFilePathManiHimoduke.Enabled = false;
                this.form.btnBrowseManiHimoduke.Enabled = false;

                this.form.txtError.Enabled = true;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
            {
                this.form.txtFilePathChokko.Enabled = false;
                this.form.btnBrowseChokko.Enabled = false;

                this.form.txtFilePathTsumikae.Enabled = false;
                this.form.btnBrowseTsumikae.Enabled = false;

                this.form.txtFilePathKenpai.Enabled = true;
                this.form.btnBrowseKenpai.Enabled = true;

                this.form.txtFilePathManiHimoduke.Enabled = false;
                this.form.btnBrowseManiHimoduke.Enabled = false;

                this.form.txtError.Enabled = true;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
            {
                this.form.txtFilePathChokko.Enabled = false;
                this.form.btnBrowseChokko.Enabled = false;

                this.form.txtFilePathTsumikae.Enabled = false;
                this.form.btnBrowseTsumikae.Enabled = false;

                this.form.txtFilePathKenpai.Enabled = false;
                this.form.btnBrowseKenpai.Enabled = false;

                this.form.txtFilePathManiHimoduke.Enabled = true;
                this.form.btnBrowseManiHimoduke.Enabled = true;

                this.form.txtError.Enabled = false;
            }
            else
            {
                this.form.txtFilePathChokko.Enabled = false;
                this.form.btnBrowseChokko.Enabled = false;

                this.form.txtFilePathTsumikae.Enabled = false;
                this.form.btnBrowseTsumikae.Enabled = false;

                this.form.txtFilePathKenpai.Enabled = false;
                this.form.btnBrowseKenpai.Enabled = false;

                this.form.txtFilePathManiHimoduke.Enabled = false;
                this.form.btnBrowseManiHimoduke.Enabled = false;

                this.form.txtError.Enabled = true;
            }

            LogUtility.DebugMethodEnd(sender, e);
        }
        #endregion

        #region [F7]作成件数集計ボタンのイベント
        /// <summary>
        /// [F7]作成件数集計ボタンのイベント
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            List<string> GetNums = new List<string>();
            GetNumForMani(GetNums);
            string showText = string.Empty;
            foreach (var GetNum in GetNums)
            {
                showText = showText + GetNum + "\r\n";
            }
            this.form.txtImportStatus.Text = showText;
        }
        #endregion

        #region [F9]登録ボタンのイベント
        /// <summary>
        /// [F9]登録ボタンのイベント
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            if (!this.form.RegistErrorFlag)
            {
                Torikomi();
            }
        }
        #endregion

        #region [F11]取消ボタンイベント

        /// <summary>
        /// [F11]取消ボタンイベント
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SetInitDisp();

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 閉じるボタンイベント

        /// <summary>
        /// 閉じるボタンイベント
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Properties.Settings.Default.FilePathChokko = this.form.txtFilePathChokko.Text;
            Properties.Settings.Default.FilePathTsumikae = this.form.txtFilePathTsumikae.Text;
            Properties.Settings.Default.FilePathKenpai = this.form.txtFilePathKenpai.Text;
            Properties.Settings.Default.FilePathHimoduke = this.form.txtFilePathManiHimoduke.Text;
            Properties.Settings.Default.Save();

            //閉じる
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 画面表示初期化

        /// <summary>
        /// 画面表示初期化
        /// </summary>
        private void SetInitDisp()
        {
            this.form.txtHaikiKbn.Text = UIConstans.MANI_SBT_CHOKKOU;
            this.form.txtFilePathChokko.Text = Properties.Settings.Default.FilePathChokko;
            this.form.txtFilePathTsumikae.Text = Properties.Settings.Default.FilePathTsumikae;
            this.form.txtFilePathKenpai.Text = Properties.Settings.Default.FilePathKenpai;
            this.form.txtFilePathManiHimoduke.Text = Properties.Settings.Default.FilePathHimoduke;
            this.form.txtError.Text = "100";
            this.form.txtImportStatus.Text = "";
            this.form.dateTimeCreateDateFrom.Text = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1).ToShortDateString();
            this.form.dateTimeCreateDateTo.Text = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
        }

        #endregion

        #region 継承メソッド

        public void setHeaderForm(UIHeader hs)
        {
            this.headerForm = hs;
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

        #endregion

        #region データ取込
        /// <summary>
        /// データ取込
        /// </summary>
        private void Torikomi()
        {

            String fileDirPath = "";
            DataTable errorLogTable = new DataTable();

            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                errorLogTable = new DataTable();
                errorLogTable.Columns.Add("ERROR", typeof(string));
                this.form.txtImportStatus.Text = "";

                if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
                {
                    fileDirPath = this.form.txtFilePathChokko.Text;
                }
                else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
                {
                    fileDirPath = this.form.txtFilePathTsumikae.Text;
                }
                else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
                {
                    fileDirPath = this.form.txtFilePathKenpai.Text;
                }
                else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                {
                    fileDirPath = this.form.txtFilePathManiHimoduke.Text;
                }

                if (string.IsNullOrEmpty(fileDirPath))
                {
                    msgLogic.MessageBoxShow("E001", "取込先");
                    return;
                }

                FileInfo fileInfo = new FileInfo(fileDirPath);
                //ファイルの存在チェック
                if (fileInfo.Exists == false)
                {
                    msgLogic.MessageBoxShow("E270");
                    return;
                }

                Encoding encoding = Encoding.GetEncoding("Shift_JIS");
                int length = 0;

                if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
                {
                    length = UIConstans.ListManiChokkoHeader.Length;
                }
                else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
                {
                    length = UIConstans.ListManiTsumikaeHeader.Length;
                }
                else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
                {
                    length = UIConstans.ListManiKenpaiHeader.Length;
                }

                int count = 0;
                int errorCount = 0;
                string manifestBangou = "";
                string koufuDate = "";
                int numberOfDenpyou = 0;
                this.listDenpyouManifest.Clear();
                this.listRelationInfo.Clear();
                this.dicValidator.Clear();
                this.CheckMainData.Clear();
                this.regist_relations = null;
                this.paperEntries = null;
                this.elecEntriesIns = null;
                this.elecEntriesUpd = null;
                this.delete_relations = null;
                this.currentRelation = null;
                // マニ紐付時、チェック
                if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                {
                    bool ValidatorFlg = false;
                    // マニ紐付時、データ重複チェック優先
                    this.CheckTorikomiHimoduke(fileInfo, encoding, ref errorLogTable, ref errorCount, ref ValidatorFlg);
                    if (ValidatorFlg)
                    {
                        return;
                    }
                }
                else
                {
                    using (StreamReader reader = new StreamReader(fileInfo.FullName, encoding))
                    {
                        string bankRenkeiCd = string.Empty;
                        string bankShitenRenkeiCd = string.Empty;
                        string kouzaNo = string.Empty;
                        string fileContent = reader.ReadLine();

                        while (!String.IsNullOrEmpty(fileContent))
                        {
                            #region Step1.ファイルヘッダチェック
                            //■Step1.ファイルヘッダチェック
                            if (count == 0)
                            {
                                // 見出し行の項目確認
                                string[] listHeaderCheck = fileContent.Split(',');
                                if (this.CheckFileLayout(listHeaderCheck, fileInfo, ref errorLogTable, ref errorCount))
                                {
                                    if (errorCount < int.Parse(this.form.txtError.Text))
                                    {
                                        DataRow row1 = errorLogTable.NewRow();
                                        row1[0] = localMsgLogic.GetMessageString("9999");
                                        errorLogTable.Rows.Add(row1);

                                        CreateErrorTextFile(errorLogTable, fileInfo.DirectoryName);
                                        this.form.txtImportStatus.Text += "Error... レポートを出力します。" + Environment.NewLine;

                                        this.msgLogic.MessageBoxShow("E169", "取込エラー", "登録処理");

                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }

                            }
                            else
                            {
                                string[] listColumnCheck = fileContent.Split(',');

                                //レコード数チェック
                                if (count == 1)
                                {
                                    int s = GetNumberOfSecondToCheckDataImport(listColumnCheck, fileInfo.FullName, encoding, length);
                                    this.dicValidator.Clear();

                                    if (s > UIConstans.LIMIT_NUMBER)
                                    {
                                        String time = Convert.ToString(UIConstans.LIMIT_NUMBER / 60);

                                        DialogResult re = msgLogic.MessageBoxShowConfirm(string.Format("処理時間が{0}分を超えます。処理を継続しますか？", time));

                                        if (re == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                this.form.txtImportStatus.Text = "Phase.1 ... インポートレイアウトチェック　･･･　完了" + Environment.NewLine;
                                #region Step2.データチェック
                                //■Step2.データチェック
                                if (listColumnCheck.Length != length)
                                {
                                    DataRow row = errorLogTable.NewRow();
                                    row[0] = (count + 1).ToString() + "行目：" + localMsgLogic.GetMessageString("0102");
                                    errorLogTable.Rows.Add(row);

                                    errorCount = errorCount + 1;

                                    //エラーの件数が画面の「エラー上限」で指定された件数を超えた場合、以降のエラーチェックは行わずに、エラーログを出力する
                                    if (this.CheckErrorJyougen(errorCount, errorLogTable, fileInfo))
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    if ((IsDenpyouRow(listColumnCheck) || !string.IsNullOrEmpty(listColumnCheck[4]))
                                        && !this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                                    {
                                        manifestBangou = listColumnCheck[4];
                                        koufuDate = listColumnCheck[6];
                                        numberOfDenpyou++;
                                    }

                                    for (int i = 0; i < length; i++)
                                    {
                                        #region 必須ﾁｪｯｸ
                                        if (IsDenpyouRow(listColumnCheck))
                                        {
                                            if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
                                            {
                                                if (UIConstans.ListHissuColumnIndexChokko.Contains(i))
                                                {
                                                    if (string.IsNullOrEmpty(listColumnCheck[i]))
                                                    {
                                                        DataRow row = errorLogTable.NewRow();
                                                        row[0] = (count + 1).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[i] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0201"));
                                                        errorLogTable.Rows.Add(row);

                                                        errorCount = errorCount + 1;

                                                        //エラーの件数が画面の「エラー上限」で指定された件数を超えた場合、以降のエラーチェックは行わずに、エラーログを出力する
                                                        if (this.CheckErrorJyougen(errorCount, errorLogTable, fileInfo))
                                                        {
                                                            return;
                                                        }
                                                        continue;
                                                    }
                                                }
                                            }
                                            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
                                            {
                                                if (UIConstans.ListHissuColumnIndexTsumikae.Contains(i))
                                                {
                                                    if (string.IsNullOrEmpty(listColumnCheck[i]))
                                                    {
                                                        DataRow row = errorLogTable.NewRow();
                                                        row[0] = (count + 1).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[i] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0201"));
                                                        errorLogTable.Rows.Add(row);

                                                        errorCount = errorCount + 1;

                                                        //エラーの件数が画面の「エラー上限」で指定された件数を超えた場合、以降のエラーチェックは行わずに、エラーログを出力する
                                                        if (this.CheckErrorJyougen(errorCount, errorLogTable, fileInfo))
                                                        {
                                                            return;
                                                        }
                                                        continue;
                                                    }
                                                }
                                            }
                                            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
                                            {
                                                if (UIConstans.ListHissuColumnIndexKenpai.Contains(i))
                                                {
                                                    if (string.IsNullOrEmpty(listColumnCheck[i]))
                                                    {
                                                        DataRow row = errorLogTable.NewRow();
                                                        row[0] = (count + 1).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[i] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0201"));
                                                        errorLogTable.Rows.Add(row);

                                                        errorCount = errorCount + 1;

                                                        //エラーの件数が画面の「エラー上限」で指定された件数を超えた場合、以降のエラーチェックは行わずに、エラーログを出力する
                                                        if (this.CheckErrorJyougen(errorCount, errorLogTable, fileInfo))
                                                        {
                                                            return;
                                                        }
                                                        continue;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                                            {
                                                if (UIConstans.ListHissuColumnIndexHimoduke.Contains(i))
                                                {
                                                    if (string.IsNullOrEmpty(listColumnCheck[i]))
                                                    {
                                                        DataRow row = errorLogTable.NewRow();
                                                        row[0] = (count + 1).ToString() + "行目：" + UIConstans.ListManiHimodukeHeader[i] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0201"));
                                                        errorLogTable.Rows.Add(row);

                                                        errorCount = errorCount + 1;

                                                        //エラーの件数が画面の「エラー上限」で指定された件数を超えた場合、以降のエラーチェックは行わずに、エラーログを出力する
                                                        if (this.CheckErrorJyougen(errorCount, errorLogTable, fileInfo))
                                                        {
                                                            return;
                                                        }
                                                        continue;
                                                    }
                                                }
                                            }
                                        }
                                        #endregion

                                        #region データタイプチェック
                                        string errorMsg = "";
                                        string type = "";
                                        int dataLength = 0;

                                        if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
                                        {
                                            type = UIConstans.ListColumnDataTypeChokko[i].Type;
                                            dataLength = UIConstans.ListColumnDataTypeChokko[i].Lenght;
                                        }
                                        else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
                                        {
                                            type = UIConstans.ListColumnDataTypeTsumikae[i].Type;
                                            dataLength = UIConstans.ListColumnDataTypeTsumikae[i].Lenght;
                                        }
                                        else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
                                        {
                                            type = UIConstans.ListColumnDataTypeKenpai[i].Type;
                                            dataLength = UIConstans.ListColumnDataTypeKenpai[i].Lenght;
                                        }
                                        else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                                        {
                                            type = UIConstans.ListColumnDataTypeHimoduke[i].Type;
                                            dataLength = UIConstans.ListColumnDataTypeHimoduke[i].Lenght;
                                        }

                                        if (this.CheckDataType(listColumnCheck[i], type, dataLength, ref errorMsg))
                                        {
                                            if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
                                            {
                                                errorMsg = UIConstans.ListManiChokkoHeader[i] + UIConstans.ZENKAKU_SPACE + errorMsg;
                                            }
                                            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
                                            {
                                                errorMsg = UIConstans.ListManiTsumikaeHeader[i] + UIConstans.ZENKAKU_SPACE + errorMsg;
                                            }
                                            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
                                            {
                                                errorMsg = UIConstans.ListManiKenpaiHeader[i] + UIConstans.ZENKAKU_SPACE + errorMsg;
                                            }
                                            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                                            {
                                                errorMsg = UIConstans.ListManiHimodukeHeader[i] + UIConstans.ZENKAKU_SPACE + errorMsg;
                                            }

                                            DataRow row = errorLogTable.NewRow();
                                            row[0] = (count + 1).ToString() + "行目：" + errorMsg;
                                            errorLogTable.Rows.Add(row);

                                            errorCount = errorCount + 1;

                                            //エラーの件数が画面の「エラー上限」で指定された件数を超えた場合、以降のエラーチェックは行わずに、エラーログを出力する
                                            if (this.CheckErrorJyougen(errorCount, errorLogTable, fileInfo))
                                            {
                                                return;
                                            }

                                            continue;
                                        }
                                        #endregion

                                        #region マスタチェック、その他チェック等
                                        CheckListColumn(listColumnCheck, listColumnCheck[i], i, count + 1, manifestBangou, koufuDate, fileInfo.FullName, encoding, length, ref errorLogTable, ref errorCount);
                                        #endregion

                                        //エラーの件数が画面の「エラー上限」で指定された件数を超えた場合、以降のエラーチェックは行わずに、エラーログを出力する
                                        if (this.CheckErrorJyougen(errorCount, errorLogTable, fileInfo))
                                        {
                                            return;
                                        }
                                    }
                                }
                                #endregion
                                this.listDenpyouManifest.Add(listColumnCheck);
                            }
                            #endregion

                            //エラーの件数が画面の「エラー上限」で指定された件数を超えた場合、以降のエラーチェックは行わずに、エラーログを出力する
                            if (this.CheckErrorJyougen(errorCount, errorLogTable, fileInfo))
                            {
                                return;
                            }

                            fileContent = reader.ReadLine();
                            count++;
                        }
                    }
                }

                // エラーが発生時、エラーのログのファイルを作成。
                if (errorLogTable.Rows.Count > 0)
                {
                    DataRow row1 = errorLogTable.NewRow();
                    row1[0] = localMsgLogic.GetMessageString("9999");
                    errorLogTable.Rows.Add(row1);

                    CreateErrorTextFile(errorLogTable, fileInfo.DirectoryName);
                    this.form.txtImportStatus.Text += "Error... レポートを出力します。" + Environment.NewLine;

                    this.msgLogic.MessageBoxShow("E169", "取込エラー", "登録処理");

                    return;
                }
                else
                {
                    this.form.txtImportStatus.Text += "Phase.2 ... インポートデータチェック　　　･･･　完了" + Environment.NewLine;
                }

                //■Step3.マニフェストデータ更新 
                if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                {
                    this.RegistRelationInfo(this.listRelationInfo);
                    numberOfDenpyou = this.listRelationInfo.Count;
                }
                else
                {
                    if (this.SetRegist())
                    {
                        return;
                    }
                }
                this.form.txtImportStatus.Text += "Phase.3 ... マニフェストデータ更新　　　　･･･　完了" + Environment.NewLine;
                this.form.txtImportStatus.Text += "Finish  ... 取込　･･･　" + numberOfDenpyou.ToString() + "件" + Environment.NewLine;

                // 登録完了時、取込対象のファイルを完了済みフォルダ(フォルダ名：Success)に移動。
                CreateFileAfterRegist(fileInfo);

                this.msgLogic.MessageBoxShow("I001", "登録");

            }
            catch (Exception ex)
            {
                FileInfo fileInfo = new FileInfo(fileDirPath);

                // Exceptionの内容をエラーログに追加する。
                DataRow row = errorLogTable.NewRow();
                row[0] = ex.Message;
                errorLogTable.Rows.Add(row);

                row = errorLogTable.NewRow();
                row[0] = ex.StackTrace;
                errorLogTable.Rows.Add(row);

                // 処理中止のメッセージをエラーログに追加する。
                row = errorLogTable.NewRow();
                row[0] = localMsgLogic.GetMessageString("9999");
                errorLogTable.Rows.Add(row);

                CreateErrorTextFile(errorLogTable, fileInfo.DirectoryName);
                this.form.txtImportStatus.Text += "Error... レポートを出力します。" + Environment.NewLine;

                this.msgLogic.MessageBoxShow("E169", "取込エラー", "登録処理");

                LogUtility.Error(ex.Message);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region 完了済みフォルダ(フォルダ名：Success)に移動
        /// <summary>
        /// 登録完了時、取込対象のファイルを完了済みフォルダ(フォルダ名：Success)に移動。
        /// </summary>
        /// <param name="sourceFile">インポートファイルの情報</param>
        private void CreateFileAfterRegist(FileInfo sourceFile)
        {
            string folderBakup = sourceFile.DirectoryName + "/Success";
            if (!Directory.Exists(folderBakup))
            {
                Directory.CreateDirectory(folderBakup);
            }
            DirectoryInfo directoryBackup = new DirectoryInfo(folderBakup);

            string fileBackup = folderBakup + "/" + getDBDateTime().ToString("yyyyMMdd_HHmmss") + "_" + sourceFile.Name;

            File.Move(sourceFile.FullName, fileBackup);
        }
        #endregion

        #region エラーが発生時、エラーのログのファイルを作成
        /// <summary>
        /// エラーが発生時、エラーのログのファイルを作成。
        /// </summary>
        /// <param name="dt">エラー内容</param>
        /// <param name="path">ファイルを置く場所</param>
        private void CreateErrorTextFile(DataTable dt, string path)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                string fileName = "ManiImportError_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
                string exportPath = Path.Combine(path, fileName);

                if (!File.Exists(exportPath))
                {
                    StreamWriter sw = null;

                    using (sw = new StreamWriter(exportPath, false, Encoding.GetEncoding("Shift_JIS")))
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sw.WriteLine(dt.Rows[i][0].ToString());
                        }

                        sw.Close();
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// ファイルレイアウトチェック
        /// </summary>
        /// <param name="listHeader">見出し行の項目</param>
        /// <param name="fileInfo">インポートファイル情報</param>
        /// <param name="errorLogTable">エラー内容</param>
        /// <param name="errorCount">エラー件数</param>
        private bool CheckFileLayout(string[] listHeader, FileInfo fileInfo, ref DataTable errorLogTable, ref int errorCount)
        {
            bool error = false;
            int length = 0;

            if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
            {
                length = UIConstans.ListManiChokkoHeader.Length;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
            {
                length = UIConstans.ListManiTsumikaeHeader.Length;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
            {
                length = UIConstans.ListManiKenpaiHeader.Length;
            }

            if (listHeader == null || listHeader.Length != length)
            {
                DataRow row = errorLogTable.NewRow();
                row[0] = "1行目：" + localMsgLogic.GetMessageString("0102");
                errorLogTable.Rows.Add(row);

                error = true;
                errorCount = errorCount + 1;

                //エラーの件数が画面の「エラー上限」で指定された件数を超えた場合、以降のエラーチェックは行わずに、エラーログを出力する
                if (this.CheckErrorJyougen(errorCount, errorLogTable, fileInfo))
                {
                    return true;
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    DataRow row = errorLogTable.NewRow();

                    if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
                    {
                        if (!listHeader[i].Equals(UIConstans.ListManiChokkoHeader[i]))
                        {
                            row[0] = "1行目：" + UIConstans.ListManiChokkoHeader[i] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0101"));
                            errorLogTable.Rows.Add(row);

                            error = true;
                            errorCount = errorCount + 1;
                        }
                    }
                    else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
                    {
                        if (!listHeader[i].Equals(UIConstans.ListManiTsumikaeHeader[i]))
                        {
                            row[0] = "1行目：" + UIConstans.ListManiTsumikaeHeader[i] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0101"));
                            errorLogTable.Rows.Add(row);

                            error = true;
                            errorCount = errorCount + 1;
                        }
                    }
                    else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
                    {
                        if (!listHeader[i].Equals(UIConstans.ListManiKenpaiHeader[i]))
                        {
                            row[0] = "1行目：" + UIConstans.ListManiKenpaiHeader[i] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0101"));
                            errorLogTable.Rows.Add(row);

                            error = true;
                            errorCount = errorCount + 1;
                        }
                    }
                    else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                    {
                        if (!listHeader[i].Equals(UIConstans.ListManiHimodukeHeader[i]))
                        {
                            row[0] = "1行目：" + UIConstans.ListManiHimodukeHeader[i] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0101"));
                            errorLogTable.Rows.Add(row);

                            error = true;
                            errorCount = errorCount + 1;
                        }
                    }

                    //エラーの件数が画面の「エラー上限」で指定された件数を超えた場合、以降のエラーチェックは行わずに、エラーログを出力する
                    if (this.CheckErrorJyougen(errorCount, errorLogTable, fileInfo))
                    {
                        return true;
                    }
                }
            }

            return error;
        }

        /// <summary>
        /// エラー上限チェック
        /// </summary>
        /// <param name="errorCount">エラー件数</param>
        /// <param name="errorLogTable">エラー内容</param>
        /// <param name="fileInfo">インポートファイル情報</param>
        private bool CheckErrorJyougen(int errorCount, DataTable errorLogTable, FileInfo fileInfo)
        {
            //エラーの件数が画面の「エラー上限」で指定された件数を超えた場合、以降のエラーチェックは行わずに、エラーログを出力する
            if (errorCount >= int.Parse(this.form.txtError.Text, System.Globalization.NumberStyles.AllowThousands))
            {
                DataRow row = errorLogTable.NewRow();
                row[0] = string.Format(localMsgLogic.GetMessageString("9998"), this.form.txtError.Text);
                errorLogTable.Rows.Add(row);

                CreateErrorTextFile(errorLogTable, fileInfo.DirectoryName);
                this.form.txtImportStatus.Text += "Error... レポートを出力します。" + Environment.NewLine;

                this.msgLogic.MessageBoxShow("E169", "取込エラー", "登録処理");

                return true;
            }
            return false;
        }

        /// <summary>
        /// データのタイプチェック
        /// </summary>
        /// <param name="value">チェック項目</param>
        /// <param name="type">データタイプ</param>
        /// <param name="length">データサイズ</param>
        /// <param name="errorMsg">エラー内容</param>
        private bool CheckDataType(string value, string type, int length, ref string errorMsg)
        {
            bool error = false;
            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            var byteArray = encoding.GetBytes(value);
            decimal maxValue = 999999999.999m;
            string typeName = "";

            switch (type)
            {
                case "String":
                    typeName = "文字列(" + length.ToString() + ")";
                    if (byteArray.Length > length * 2)
                    {
                        errorMsg = string.Format(localMsgLogic.GetMessageString("0202"), typeName);
                        error = true;
                    }
                    break;
                case "Number":
                    if (length != -1)
                    {
                        typeName = "数字(" + length.ToString() + ")";
                        if (byteArray.Length > length)
                        {
                            errorMsg = string.Format(localMsgLogic.GetMessageString("0202"), typeName);
                            error = true;
                            break;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                int outValue = 0;

                                if (!int.TryParse(value, out outValue))
                                {
                                    errorMsg = string.Format(localMsgLogic.GetMessageString("0202"), typeName);
                                    error = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        decimal outValue = 0m;

                        if (!string.IsNullOrEmpty(value))
                        {
                            if (!decimal.TryParse(value, out outValue))
                            {
                                typeName = "数字";
                                errorMsg = string.Format(localMsgLogic.GetMessageString("0202"), typeName);
                                error = true;
                                break;
                            }

                            if (outValue < 0 || outValue > maxValue)
                            {
                                errorMsg = string.Format(localMsgLogic.GetMessageString("0304"), "0～999999999.999");
                                error = true;
                                break;
                            }

                            // 少数点桁数チェック
                            int outValueBit = GetDecimalPointNumber(outValue);
                            int sysValueBit = GetDecimalPointNumber(mSysInfo.MANIFEST_SUURYO_FORMAT); // マニフェスト情報数量書式
                            if (sysValueBit < outValueBit)
                            {
                                string msg = string.Format("「{0}」形式の少数点", mSysInfo.MANIFEST_SUURYO_FORMAT);
                                errorMsg = string.Format(localMsgLogic.GetMessageString("0305"), msg);
                                error = true;
                                break;
                            }
                        }
                    }
                    break;
                case "DateTime":
                    DateTime outValue1;

                    if (!string.IsNullOrEmpty(value))
                    {
                        if (!DateTime.TryParse(value, out outValue1))
                        {
                            errorMsg = string.Format(localMsgLogic.GetMessageString("0202"), "yyyy/MM/dd");
                            error = true;
                            break;
                        }
                    }
                    break;
                case "Boolean":
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (!value.Equals("0") && !value.Equals("1"))
                        {
                            errorMsg = string.Format(localMsgLogic.GetMessageString("0202"), "bool");
                            error = true;
                            break;
                        }
                    }
                    break;
                case "StringNumber":
                    if (!string.IsNullOrEmpty(value))
                    {
                        typeName = "英数字(" + length.ToString() + ")";
                        if (byteArray.Length > length)
                        {
                            errorMsg = string.Format(localMsgLogic.GetMessageString("0202"), typeName);
                            error = true;
                        }
                        else
                        {
                            int errorCounter = Regex.Matches(value.PadLeft(length, '0'), @"[a-zA-Z]").Count;
                            errorCounter += Regex.Matches(value.PadLeft(length, '0'), @"[0-9]").Count;

                            if (errorCounter != length)
                            {
                                errorMsg = string.Format(localMsgLogic.GetMessageString("0202"), typeName);
                                error = true;
                            }
                        }
                    }
                    break;
            }
            return error;
        }

        /// <summary>
        /// 小数点以下の桁数を取得
        /// </summary>
        /// <param name="price">取得対象の値</param>
        private int GetDecimalPointNumber(decimal price)
        {
            string priceString = price.ToString().TrimEnd('0');

            return GetDecimalPointNumber(priceString);
        }

        /// <summary>
        /// 小数点以下の桁数を取得
        /// </summary>
        /// <param name="priceString">取得対象の値</param>
        private int GetDecimalPointNumber(string priceString)
        {
            if (string.IsNullOrEmpty(priceString))
            {
                return 0;
            }

            int index = priceString.IndexOf('.');
            if (index == -1)
                return 0;

            return priceString.Substring(index + 1).Length;
        }

        /// <summary>
        /// 交付番号区分が1の場合、桁数は11桁固定数字かつ、最後の1桁はチェックデジットかチェック
        /// </summary>
        /// <param name="manifestBangou">交付番号</param>
        /// <param name="koufuKbn">交付番号区分</param>
        private bool CheckManifestBangou(string manifestBangou, string koufuKbn)
        {
            string ret = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(manifestBangou, koufuKbn);

                Encoding encoding = Encoding.GetEncoding("Shift_JIS");
                var byteArray = encoding.GetBytes(manifestBangou);

                if (koufuKbn.Equals("1") && byteArray.Length != 11)
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(koufuKbn) && koufuKbn.Equals("1"))
                {
                    ret = ManifestoLogic.ChkKoufuNo(manifestBangou, koufuKbn.Equals("1"));
                }

                if (!string.IsNullOrEmpty(ret))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckManifestBangou", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(!string.IsNullOrEmpty(ret));
            }

            return false;
        }

        /// <summary>
        /// DBサーバ日付を取得する
        /// </summary>
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            r_framework.Dao.GET_SYSDATEDao dateDao = DaoInitUtility.GetComponent<r_framework.Dao.GET_SYSDATEDao>();
            System.Data.DataTable dt = dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }

        /// <summary>
        /// マニ未紐付けデータかチェック
        /// </summary>
        /// <param name="manifestBangou">交付番号</param>
        /// <param name="manifestKbn">一次マニフェスト区分</param>
        /// <param name="haikiKbnCd">廃棄物区分</param>
        private bool CheckExistManifestHimoduke(string manifestBangou, string manifestKbn, string haikiKbnCd = "", string detailSystemId = "")
        {
            bool error = false;

            T_MANIFEST_ENTRY data = new T_MANIFEST_ENTRY();
            data.MANIFEST_ID = manifestBangou;

            if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
            {
                data.HAIKI_KBN_CD = UIConstans.HAIKI_KBN_CHOKKOU;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
            {
                data.HAIKI_KBN_CD = UIConstans.HAIKI_KBN_TUMIKAE;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
            {
                data.HAIKI_KBN_CD = UIConstans.HAIKI_KBN_KENPAI;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
            {
                data.HAIKI_KBN_CD = Convert.ToInt16(haikiKbnCd);
            }

            if ("4".Equals(haikiKbnCd))
            {
                //一次マニフェスト区分は「0：一次マニフェスト」の場合
                if (manifestKbn.Equals("0"))
                {
                    var result = this.denManiRelationDao.GetManiRelationDataForDenshiByFirst(manifestBangou, detailSystemId);

                    if (result != null && result.Length > 0)
                    {
                        error = true;
                    }
                }
                //一次マニフェスト区分は「1:一次マニフェスト以外（2次）」の場合
                else if (manifestKbn.Equals("1"))
                {
                    var result = this.denManiRelationDao.GetManiRelationDataForDenshiByNext(manifestBangou);

                    if (result != null && result.Length > 0)
                    {
                        error = true;
                    }
                }
            }
            else
            {
                //一次マニフェスト区分は「0：一次マニフェスト」の場合
                if (manifestKbn.Equals("0"))
                {
                    var result = this.manifestEntryDao.GetManifestRelationDataByFirstSystemId(data, detailSystemId);

                    if (result != null && result.Length > 0)
                    {
                        error = true;
                    }
                }
                //一次マニフェスト区分は「1:一次マニフェスト以外（2次）」の場合
                else if (manifestKbn.Equals("1"))
                {
                    var result = this.manifestEntryDao.GetManifestRelationDataByNextSystemId(data);

                    if (result != null && result.Length > 0)
                    {
                        error = true;
                    }
                }
            }

            return error;
        }

        #region 紐付け可能チェック
        /// <summary>
        /// 紐付け可能かどうか判定
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns>true:紐付けOK, false:紐付けNG</returns>
        internal bool IsHimodukeOk(string[] listColumnCheck, string firSystemId, string firKanriId, string systemId, string secKanriId)
        {
            bool isHimodukeOkFlg = false;
            // 紐付条件チェック
            // 紙マニの場合
            if (!"4".Equals(listColumnCheck[5]))
            {
                var result = this.PaperAndElecManiDao.CheckHimodukeForPaper(firSystemId);

                if (result == null || result.Rows.Count < 1)
                {
                    return isHimodukeOkFlg;
                }
            }
            // 電マニの場合
            else
            {
                var result = this.PaperAndElecManiDao.CheckHimodukeForElec(firKanriId);

                if (result == null || result.Rows.Count < 1)
                {
                    return isHimodukeOkFlg;
                }
            }

            // 選択されたのが電マニの場合だけチェックする
            if (!"4".Equals(listColumnCheck[5]))
            {
                return true;
            }
            // 一次マニの最終処分場情報
            DataTable LastSbnJyouDataForFirstMani = new DataTable();
            LastSbnJyouDataForFirstMani = this.PaperAndElecManiDao.GetLastSbnJyouInfoForElec(firKanriId);
            if (LastSbnJyouDataForFirstMani == null
                || LastSbnJyouDataForFirstMani.Rows.Count < 1
                || string.IsNullOrEmpty(LastSbnJyouDataForFirstMani.Rows[0]["LAST_SBN_JIGYOUJOU_NAME_AND_ADDRESS"].ToString()))
            {
                // 一次マニの最終処分場所がない場合はOK
                return true;
            }
            // 二次マニの最終処分場情報
            DataTable LastSbnJyouDataForSecondMani = new DataTable();
            // エラーチェック用に二次マニ最終処分場情報を取得
            if ("4".Equals(listColumnCheck[1]))
            {
                // 電マニ
                LastSbnJyouDataForSecondMani = this.PaperAndElecManiDao.GetLastSbnJyouInfoForElec(secKanriId);
            }
            else
            {
                // 紙マニ
                LastSbnJyouDataForSecondMani = this.PaperAndElecManiDao.GetLastSbnJyouInfoForPaper(systemId);
            }

            // 一次マニの最終処分場所があるのに
            // 二次マニの最終処分場所がない場合はNG
            if (LastSbnJyouDataForSecondMani == null
                || LastSbnJyouDataForSecondMani.Rows.Count < 1)
            {
                return false;
            }
            else if (LastSbnJyouDataForFirstMani.Rows[0]["LAST_SBN_JIGYOUJOU_NAME_AND_ADDRESS"].ToString()
                .Equals(LastSbnJyouDataForSecondMani.Rows[0]["LAST_SBN_JIGYOUJOU_NAME_AND_ADDRESS"].ToString()))
            {
                isHimodukeOkFlg = true;
            }
            return isHimodukeOkFlg;
        }
        #endregion

        /// <summary>
        /// その交付番号に該当する最大の行番号をカウント
        /// </summary>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="manifestBangou">交付番号</param>
        /// <param name="fileFullName">ファイル名</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="length">項目数</param>
        private int CountNumberOfDetailInFileByManifestNumber(string haikiKbn, string manifestBangou, string fileFullName, Encoding encoding, int length)
        {
            int count = 0;
            bool startCount = false;
            int rowNo = 0;

            using (StreamReader reader = new StreamReader(fileFullName, encoding))
            {
                string fileContent = reader.ReadLine();
                while (!String.IsNullOrEmpty(fileContent))
                {
                    string[] listColumnCheck = fileContent.Split(',');

                    if (listColumnCheck.Length != length)
                    {
                        count = 0;
                        break;
                    }

                    if (manifestBangou.Equals(listColumnCheck[4]) && haikiKbn.Equals(listColumnCheck[3]))
                    {
                        startCount = true;
                        rowNo = count;
                    }

                    if (startCount)
                    {
                        if (!string.IsNullOrEmpty(listColumnCheck[4]) && rowNo != count)
                        {
                            break;
                        }

                        count++;
                    }

                    fileContent = reader.ReadLine();
                }
            }

            return count;
        }

        /// <summary>
        /// その交付番号に該当する行番号を連番かチェック
        /// </summary>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="manifestBangou">交付番号</param>
        /// <param name="fileFullName">ファイル名</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="length">項目数</param>
        private List<string> CheckDetailRowNo(string haikiKbn, string manifestBangou, string fileFullName, Encoding encoding, int length)
        {
            List<string> lstErrorIndex = new List<string>();
            int count = 0;
            int index = 1;
            bool startCheck = false;

            using (StreamReader reader = new StreamReader(fileFullName, encoding))
            {
                string fileContent = reader.ReadLine();
                while (!String.IsNullOrEmpty(fileContent))
                {
                    string[] listColumnCheck = fileContent.Split(',');

                    if (manifestBangou.Equals(listColumnCheck[4]) && haikiKbn.Equals(listColumnCheck[3]))
                    {
                        startCheck = true;
                    }

                    if (startCheck)
                    {
                        if (count != 0 && !string.IsNullOrEmpty(listColumnCheck[4]))
                        {
                            break;
                        }

                        count++;

                        if (listColumnCheck.Length != length)
                        {
                            lstErrorIndex.Add((index).ToString());
                        }
                        else
                        {
                            if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
                            {
                                if (!listColumnCheck[94].Equals(count.ToString()))
                                {
                                    lstErrorIndex.Add((index).ToString());
                                }
                            }
                            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
                            {
                                if (!listColumnCheck[159].Equals(count.ToString()))
                                {
                                    lstErrorIndex.Add((index).ToString());
                                }
                            }
                            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
                            {
                                if (!listColumnCheck[155].Equals(count.ToString()))
                                {
                                    lstErrorIndex.Add((index).ToString());
                                }
                            }
                            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                            {
                                if (!listColumnCheck[7].Equals(count.ToString()))
                                {
                                    lstErrorIndex.Add((index).ToString());
                                }
                            }
                        }
                    }

                    index++;
                    fileContent = reader.ReadLine();
                }
            }

            return lstErrorIndex;
        }

        /// <summary>
        /// その行にENTRYの情報があるかチェック
        /// 必須項目を検索し、１つでも必須項目が設定されている場合、ENTRY情報が存在する。
        /// ただし、行番号のみ設定されている場合、ENTRY情報が存在しない。（明細行である）
        /// </summary>
        /// <param name="listColumnCheck">チェック項目</param>
        private bool IsDenpyouRow(string[] listColumnCheck)
        {
            int gyouNoCount = 0;
            bool otherFlg = false;

            // 直行
            if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
            {
                for (int i = 0; i < listColumnCheck.Length; i++)
                {
                    if (UIConstans.ListHissuColumnIndexChokko.Contains(i) && !string.IsNullOrEmpty(listColumnCheck[i]))
                    {
                        if (UIConstans.ListHissuColumnIndexChokko[UIConstans.ListHissuColumnIndexChokko.Count - 1].Equals(i))
                        {
                            gyouNoCount++;
                        }
                        else
                        {
                            otherFlg = true;
                        }
                    }
                }
            }
            // 積替
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
            {
                for (int i = 0; i < listColumnCheck.Length; i++)
                {
                    if (UIConstans.ListHissuColumnIndexTsumikae.Contains(i) && !string.IsNullOrEmpty(listColumnCheck[i]))
                    {
                        if (UIConstans.ListHissuColumnIndexTsumikae[UIConstans.ListHissuColumnIndexTsumikae.Count - 1].Equals(i))
                        {
                            gyouNoCount++;
                        }
                        else
                        {
                            otherFlg = true;
                        }
                    }
                }
            }
            // 建廃
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
            {
                for (int i = 0; i < listColumnCheck.Length; i++)
                {
                    if (UIConstans.ListHissuColumnIndexKenpai.Contains(i) && !string.IsNullOrEmpty(listColumnCheck[i]))
                    {
                        if (UIConstans.ListHissuColumnIndexKenpai[UIConstans.ListHissuColumnIndexKenpai.Count - 1].Equals(i))
                        {
                            gyouNoCount++;
                        }
                        else
                        {
                            otherFlg = true;
                        }
                    }
                }
            }
            // 紐付
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
            {
                //for (int i = 0; i < listColumnCheck.Length; i++)
                //{
                //    if (UIConstans.ListHissuColumnIndexHimoduke.Contains(i) && !string.IsNullOrEmpty(listColumnCheck[i]))
                //    {
                //        if (UIConstans.ListHissuColumnIndexHimoduke[UIConstans.ListHissuColumnIndexHimoduke.Count - 1].Equals(i))
                //        {
                //            gyouNoCount++;
                //        }
                //        else
                //        {
                //            otherFlg = true;
                //        }
                //    }
                //}
                return false;
            }

            // 行番号のみが設定されている場合、明細行と判定する。
            if (!otherFlg && gyouNoCount > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// マスタチェック、その他チェック等
        /// </summary>
        /// <param name="listColumnCheck">Listチェック項目</param>
        /// <param name="value">チェック項目</param>
        /// <param name="index">チェック項目の位置</param>
        /// <param name="rowNumber">行数</param>
        /// <param name="manifestBangou">交付番号</param>
        /// <param name="koufuDate">交付年月日</param>
        /// <param name="fileFullName">ファイル名</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="length">項目数</param>
        /// <param name="errorLogTable">エラー内容</param>
        /// <param name="errorCount">エラーの件数</param>
        private bool CheckListColumn(string[] listColumnCheck, string value, int index, int rowNumber, string manifestBangou, string koufuDate, string fileFullName, Encoding encoding, int length, ref DataTable errorLogTable, ref int errorCount)
        {
            bool error = false;
            bool error1 = false;

            if (IsDenpyouRow(listColumnCheck))
            {
                switch (index)
                {
                    #region 拠点CD
                    case 0:
                        if (string.IsNullOrEmpty(CheckKyotenCd(value)))
                        {
                            DataRow row = errorLogTable.NewRow();
                            row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                            errorLogTable.Rows.Add(row);
                            errorCount++;
                            return true;
                        }
                        break;
                    #endregion

                    #region 一次マニフェスト区分
                    case 2:
                        // 0：一次マニフェスト　1：一次マニフェスト以外（2次）
                        if (!value.Equals("0") && !value.Equals("1"))
                        {
                            DataRow row = errorLogTable.NewRow();
                            row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "0～1");
                            errorLogTable.Rows.Add(row);
                            errorCount++;
                            return true;
                        }

                        break;
                    #endregion

                    #region 交付番号区分
                    case 3:
                        // 1：通常　2：特殊
                        if (!value.Equals("1") && !value.Equals("2"))
                        {
                            DataRow row = errorLogTable.NewRow();
                            row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "1～2");
                            errorLogTable.Rows.Add(row);
                            errorCount++;
                            return true;
                        }

                        break;
                    #endregion

                    #region 交付番号
                    case 4:
                        // 1：通常の場合、チェックします
                        if (listColumnCheck[3].Equals("1"))
                        {
                            if (CheckManifestBangou(value, "1"))
                            {
                                DataRow row = errorLogTable.NewRow();
                                row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0303"));
                                errorLogTable.Rows.Add(row);
                                errorCount++;
                                return true;
                            }
                        }

                        if (CheckExistManifestHimoduke(value, listColumnCheck[2]))
                        {
                            DataRow row = errorLogTable.NewRow();
                            row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0301"));
                            errorLogTable.Rows.Add(row);
                            errorCount++;
                            return true;
                        }

                        break;
                    #endregion

                    #region 行数
                    case 5:
                        if (IsDenpyouRow(listColumnCheck))
                        {
                            int count = CountNumberOfDetailInFileByManifestNumber(listColumnCheck[3], listColumnCheck[4], fileFullName, encoding, length);
                            if (!value.Equals(count.ToString()))
                            {
                                DataRow row = errorLogTable.NewRow();
                                row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0307"));
                                errorLogTable.Rows.Add(row);
                                errorCount++;
                                return true;
                            }
                        }

                        break;
                    #endregion
                }
            }

            if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
            {
                if (IsDenpyouRow(listColumnCheck))
                {
                    error = CheckExistsInMasterChokkoForDenyou(value, index, listColumnCheck, rowNumber, ref errorLogTable, ref errorCount);
                }
                error1 = CheckExistsInMasterChokkoForMeisai(manifestBangou, koufuDate, fileFullName, encoding, length, value, index, listColumnCheck, rowNumber, ref errorLogTable, ref errorCount);
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
            {
                if (IsDenpyouRow(listColumnCheck))
                {
                    error = CheckExistsInMasterTsumikaeForDenyou(value, index, listColumnCheck, rowNumber, ref errorLogTable, ref errorCount);
                }
                error1 = CheckExistsInMasterTsumikaeForMeisai(manifestBangou, koufuDate, fileFullName, encoding, length, value, index, listColumnCheck, rowNumber, ref errorLogTable, ref errorCount);
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
            {
                if (IsDenpyouRow(listColumnCheck))
                {
                    error = CheckExistsInMasterKenpaiForDenyou(value, index, listColumnCheck, rowNumber, ref errorLogTable, ref errorCount);
                }
                error1 = CheckExistsInMasterKenpaiForMeisai(manifestBangou, koufuDate, fileFullName, encoding, length, value, index, listColumnCheck, rowNumber, ref errorLogTable, ref errorCount);
            }

            if (!error && !error1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// マスタチェック、その他チェック等(直行のENTRY用)
        /// </summary>
        /// <param name="value">チェック項目</param>
        /// <param name="index">チェック項目の位置</param>
        /// <param name="listColumnCheck">Listチェック項目</param>
        /// <param name="rowNumber">行数</param>
        /// <param name="errorLogTable">エラー内容</param>
        /// <param name="errorCount">エラー件数</param>
        private bool CheckExistsInMasterChokkoForDenyou(string value, int index, string[] listColumnCheck, int rowNumber, ref DataTable errorLogTable, ref int errorCount)
        {
            bool error = false;

            #region 排出事業者CD
            if (index == 9)
            {
                if (ChkGyosya(value, "HAISHUTSU_NIZUMI_GYOUSHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 排出事業場CD
            if (index == 14)
            {
                if (CheckGenba(listColumnCheck[9], value, "HAISHUTSU_NIZUMI_GENBA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 中間処理産業廃棄物フラグ
            if (index == 19)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 0：設定なし 1：帳簿記載のとおり　2：当欄記載のとおり
                    if (!value.Equals("0") && !value.Equals("1") && !value.Equals("2"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "0～2");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分の場所(予定)フラグ
            if (index == 21)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 0：設定なし 1：委託契約書記載のとおり　2：当欄記載のとおり
                    if (!value.Equals("0") && !value.Equals("1") && !value.Equals("2"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "0～2");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分の場所(予定)業者CD
            if (index == 22)
            {
                //最終処分の場所(予定)フラグが2：当欄記載のとおり、の場合、最終処分の場所(予定)業者CDは必須です
                if (listColumnCheck[21].Equals("2"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }

                if (ChkGyosya(value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 最終処分の場所(予定)現場CD
            if (index == 23)
            {
                //最終処分の場所(予定)フラグが2：当欄記載のとおり、の場合、最終処分の場所(予定)現場CDは必須です
                if (listColumnCheck[21].Equals("2"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index - 2] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }

                if (CheckGenba(listColumnCheck[22], value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 運搬受託者CD
            if (index == 28)
            {
                if (ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 運搬方法CD
            if (index == 33)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnpanHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 車輌CD
            if (index == 35)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckSharyouCd(value, listColumnCheck[28], listColumnCheck[37])))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 車種CD
            if (index == 37)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShahuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 運搬先の事業場CD
            if (index == 40)
            {
                bool errorFlg = false;

                // 処分受託者CDをチェックし、エラーか判定する。
                if (ChkGyosya(listColumnCheck[45], "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6]))
                {
                    errorFlg = true;
                }
                // 処分受託者CDがエラーの場合、運搬先の事業場CDはチェックしない。
                if (CheckGenba(listColumnCheck[45], value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6]) && !errorFlg)
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 処分受託者CD
            if (index == 45)
            {
                if (ChkGyosya(value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 積替保管業者CD
            if (index == 50)
            {
                if (ChkGyosya(value, "TSUMIKAEHOKAN_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 積替保管現場CD
            if (index == 52)
            {
                if (CheckGenba(listColumnCheck[50], value, "TSUMIKAEHOKAN_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 有害物質CD
            if (index == 57)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckYuugaiBusshitsuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 運搬の受託者CD
            if (index == 60)
            {
                // チェック、マスタ存在チェック、フォーマットチェック
                if ((!string.IsNullOrEmpty(value) && ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                    || (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(listColumnCheck[28])
                        && !(value.PadLeft(6, '0').ToUpper()).Equals(listColumnCheck[28].PadLeft(6, '0').ToUpper())))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 運転者CD
            if (index == 62)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUntenShaCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 有価物拾集量単位CD
            if (index == 66)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnitCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                    if (string.IsNullOrEmpty(listColumnCheck[65]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiChokkoHeader[index - 1] + "、" + UIConstans.ListManiChokkoHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(listColumnCheck[65]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiChokkoHeader[index - 1] + "、" + UIConstans.ListManiChokkoHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分の受託者CD
            if (index == 68)
            {
                // 空チェック、マスタ存在チェック、フォーマットチェック
                if ((!string.IsNullOrEmpty(value) && ChkGyosya(value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6])))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
                // 処分受託者CDと処分の受託者CDが一致していない場合、エラーとする。
                if ((!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(listColumnCheck[45])
                        && !(value.PadLeft(6, '0').ToUpper()).Equals(listColumnCheck[45].PadLeft(6, '0').ToUpper())))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 処分担当者CD
            if (index == 70)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunTantouCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分業者CD
            if (index == 72)
            {
                if (ChkGyosya(value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 最終処分現場CD
            if (index == 73)
            {
                if (CheckGenba(listColumnCheck[72], value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            return error;
        }

        /// <summary>
        /// マスタチェック、その他チェック等(積替のENTRY用)
        /// </summary>
        /// <param name="value">チェック項目</param>
        /// <param name="index">チェック項目の位置</param>
        /// <param name="listColumnCheck">Listチェック項目</param>
        /// <param name="rowNumber">行数</param>
        /// <param name="errorLogTable">エラー内容</param>
        /// <param name="errorCount">エラー件数</param>
        private bool CheckExistsInMasterTsumikaeForDenyou(string value, int index, string[] listColumnCheck, int rowNumber, ref DataTable errorLogTable, ref int errorCount)
        {
            bool error = false;

            #region 排出事業者CD
            if (index == 9)
            {
                if (ChkGyosya(value, "HAISHUTSU_NIZUMI_GYOUSHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 排出事業場CD
            if (index == 14)
            {
                if (CheckGenba(listColumnCheck[9], value, "HAISHUTSU_NIZUMI_GENBA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 中間処理産業廃棄物フラグ
            if (index == 19)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 0：設定なし 1：帳簿記載のとおり　2：当欄記載のとおり
                    if (!value.Equals("0") && !value.Equals("1") && !value.Equals("2"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "0～2");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分の場所(予定)フラグ
            if (index == 21)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 0：設定なし 1：委託契約書記載のとおり　2：当欄記載のとおり
                    if (!value.Equals("0") && !value.Equals("1") && !value.Equals("2"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "0～2");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分の場所(予定)業者CD
            if (index == 22)
            {
                if (listColumnCheck[21].Equals("2"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }

                if (ChkGyosya(value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 最終処分の場所(予定)現場CD
            if (index == 23)
            {
                if (listColumnCheck[21].Equals("2"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index - 2] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }

                if (CheckGenba(listColumnCheck[22], value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間1)運搬受託者CD
            if (index == 28)
            {
                if (ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間1)車輌CD
            if (index == 33)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckSharyouCd(value, listColumnCheck[28], listColumnCheck[35])))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)車種CD
            if (index == 35)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShahuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)運搬方法CD
            if (index == 37)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnpanHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)運搬先区分
            if (index == 39)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 1：処分施設　2：積替保管
                    if (!value.Equals("1") && !value.Equals("2"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "1～2");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)運搬先の事業者CD
            if (index == 40)
            {
                //運搬先区分=1 かつ 運搬先の事業者CD = 処分受託者CD
                if (listColumnCheck[39].Equals("1") && !value.Equals(listColumnCheck[82]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }

                //(区間1)運搬先の事業者CD
                if (listColumnCheck[39].Equals("2"))
                {
                    if (ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)運搬先の事業場CD
            if (index == 41)
            {
                //処分受託者CD
                if (listColumnCheck[39].Equals("1"))
                {
                    if (CheckGenba(listColumnCheck[82], value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
                //(区間1)運搬先の事業者CD
                if (listColumnCheck[39].Equals("2"))
                {
                    if (CheckGenba(listColumnCheck[40], value, "TSUMIKAEHOKAN_KBN", listColumnCheck[6]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)運搬受託者CD
            if (index == 46)
            {
                if (ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間2)車輌CD
            if (index == 51)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckSharyouCd(value, listColumnCheck[46], listColumnCheck[53])))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)車種CD
            if (index == 53)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShahuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)運搬方法CD
            if (index == 55)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnpanHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)運搬先区分
            if (index == 57)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 1：処分施設　2：積替保管
                    if (!value.Equals("1") && !value.Equals("2"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "1～2");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)運搬先の事業者CD
            if (index == 58)
            {
                //運搬先区分=1 かつ 運搬先の事業者CD = 処分受託者CD
                if (listColumnCheck[57].Equals("1") && !value.Equals(listColumnCheck[82]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }

                //(区間2)運搬先の事業者CD
                if (listColumnCheck[57].Equals("2"))
                {
                    if (ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)運搬先の事業場CD
            if (index == 59)
            {
                //処分受託者CD
                if (listColumnCheck[57].Equals("1"))
                {
                    if (CheckGenba(listColumnCheck[82], value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
                //(区間2)運搬先の事業者CD
                if (listColumnCheck[57].Equals("2"))
                {
                    if (CheckGenba(listColumnCheck[58], value, "TSUMIKAEHOKAN_KBN", listColumnCheck[6]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間3)運搬受託者CD
            if (index == 64)
            {
                if (ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間3)車輌CD
            if (index == 69)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckSharyouCd(value, listColumnCheck[64], listColumnCheck[71])))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間3)車種CD
            if (index == 71)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShahuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間3)運搬方法CD
            if (index == 73)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnpanHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間3)運搬先区分
            if (index == 75)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 1：処分施設
                    if (!value.Equals("1"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0305"), "1");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間3)運搬先の事業者CD
            if (index == 76)
            {
                //運搬先区分=1 かつ 運搬先の事業者CD = 処分受託者CD
                if (listColumnCheck[75].Equals("1") && !value.Equals(listColumnCheck[82]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間3)運搬先の事業場CD
            if (index == 77)
            {
                //処分受託者CD
                if (listColumnCheck[75].Equals("1"))
                {
                    if (CheckGenba(listColumnCheck[82], value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
                //(区間2)運搬先の事業者CD
                if (listColumnCheck[75].Equals("2"))
                {
                    if (CheckGenba(listColumnCheck[76], value, "TSUMIKAEHOKAN_KBN", listColumnCheck[6]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分受託者CD
            if (index == 82)
            {
                if (ChkGyosya(value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 積替保管業者CD
            if (index == 87)
            {
                if (ChkGyosya(value, "TSUMIKAEHOKAN_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 積替保管現場CD
            if (index == 89)
            {
                if (CheckGenba(listColumnCheck[87], value, "TSUMIKAEHOKAN_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 有害物質CD
            if (index == 94)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckYuugaiBusshitsuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)運搬の受託者CD
            if (index == 96)
            {
                // 空チェック、マスタ存在チェック、フォーマットチェック
                if ((!string.IsNullOrEmpty(value) && ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                    || (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(listColumnCheck[28])
                        && !(value.PadLeft(6, '0').ToUpper()).Equals(listColumnCheck[28].PadLeft(6, '0').ToUpper())))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間1)運転者CD
            if (index == 98)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUntenShaCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)有価物拾集量単位CD
            if (index == 102)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnitCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                    if (string.IsNullOrEmpty(listColumnCheck[101]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiTsumikaeHeader[index - 1] + "、" + UIConstans.ListManiTsumikaeHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(listColumnCheck[101]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiTsumikaeHeader[index - 1] + "、" + UIConstans.ListManiTsumikaeHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)運搬の受託者CD
            if (index == 104)
            {
                // 空チェック、マスタ存在チェック、フォーマットチェック
                if ((!string.IsNullOrEmpty(value) && ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                    || (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(listColumnCheck[46])
                        && !(value.PadLeft(6, '0').ToUpper()).Equals(listColumnCheck[46].PadLeft(6, '0').ToUpper())))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間2)運転者CD
            if (index == 106)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUntenShaCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)有価物拾集量単位CD
            if (index == 110)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnitCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                    if (string.IsNullOrEmpty(listColumnCheck[109]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiTsumikaeHeader[index - 1] + "、" + UIConstans.ListManiTsumikaeHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(listColumnCheck[109]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiTsumikaeHeader[index - 1] + "、" + UIConstans.ListManiTsumikaeHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間3)運搬の受託者CD
            if (index == 112)
            {
                // 空チェック、マスタ存在チェック、フォーマットチェック
                if ((!string.IsNullOrEmpty(value) && ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                    || (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(listColumnCheck[64])
                        && !(value.PadLeft(6, '0').ToUpper()).Equals(listColumnCheck[64].PadLeft(6, '0').ToUpper())))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間3)運転者CD
            if (index == 114)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUntenShaCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間3)有価物拾集量単位CD
            if (index == 118)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnitCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                    if (string.IsNullOrEmpty(listColumnCheck[117]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiTsumikaeHeader[index - 1] + "、" + UIConstans.ListManiTsumikaeHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(listColumnCheck[117]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiTsumikaeHeader[index - 1] + "、" + UIConstans.ListManiTsumikaeHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分の受託者CD
            if (index == 120)
            {
                // 空チェック、マスタ存在チェック、フォーマットチェック
                if ((!string.IsNullOrEmpty(value) && ChkGyosya(value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6])))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
                // 処分受託者CDと処分の受託者CDが一致していない場合、エラーとする。
                if ((!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(listColumnCheck[82])
                        && !(value.PadLeft(6, '0').ToUpper()).Equals(listColumnCheck[82].PadLeft(6, '0').ToUpper())))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 処分担当者CD
            if (index == 122)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunTantouCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分業者CD
            if (index == 124)
            {
                if (ChkGyosya(value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 最終処分現場CD
            if (index == 125)
            {
                if (CheckGenba(listColumnCheck[124], value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            return error;
        }

        /// <summary>
        /// マスタチェック、その他チェック等(建廃のENTRY用)
        /// </summary>
        /// <param name="value">チェック項目</param>
        /// <param name="index">チェック項目の位置</param>
        /// <param name="listColumnCheck">Listチェック項目</param>
        /// <param name="rowNumber">行数</param>
        /// <param name="errorLogTable">エラー内容</param>
        /// <param name="errorCount">エラー件数</param>
        private bool CheckExistsInMasterKenpaiForDenyou(string value, int index, string[] listColumnCheck, int rowNumber, ref DataTable errorLogTable, ref int errorCount)
        {
            bool error = false;

            #region 排出事業者CD
            if (index == 12)
            {
                if (ChkGyosya(value, "HAISHUTSU_NIZUMI_GYOUSHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 排出事業場CD
            if (index == 17)
            {
                if (CheckGenba(listColumnCheck[12], value, "HAISHUTSU_NIZUMI_GENBA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 中間処理産業廃棄物フラグ
            if (index == 22)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 0：設定なし 1：帳簿記載のとおり　2：当欄記載のとおり
                    if (!value.Equals("0") && !value.Equals("1") && !value.Equals("2"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "0～2");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分の場所(予定)フラグ
            if (index == 24)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 0：設定なし 1：委託契約書記載のとおり　2：当欄記載のとおり
                    if (!value.Equals("0") && !value.Equals("1") && !value.Equals("2"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "0～2");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分の場所(予定)業者CD
            if (index == 25)
            {
                if (listColumnCheck[24].Equals("2"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }

                if (ChkGyosya(value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 最終処分の場所(予定)現場CD
            if (index == 26)
            {
                if (listColumnCheck[24].Equals("2"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index - 2] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }

                if (CheckGenba(listColumnCheck[25], value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 形状4CD
            if (index == 33)
            {
                // 1：形状のチェック有
                if (listColumnCheck[32].Equals("1"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }

                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(ChkKeijou(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 形状5CD
            if (index == 35)
            {
                // 1：形状のチェック有
                if (listColumnCheck[34].Equals("1"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }

                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(ChkKeijou(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 形状6CD
            if (index == 37)
            {
                // 1：形状のチェック有
                if (listColumnCheck[36].Equals("1"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }

                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(ChkKeijou(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 形状7CD
            if (index == 39)
            {
                // 1：形状のチェック有
                if (listColumnCheck[38].Equals("1"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }

                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(ChkKeijou(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)運搬受託者CD
            if (index == 50)
            {
                if (ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間1)車輌CD
            if (index == 55)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckSharyouCd(value, listColumnCheck[50], listColumnCheck[57])))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)車種CD
            if (index == 57)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShahuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)積替・保管有無
            if (index == 59)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 1：有　0：無し
                    if (!value.Equals("0") && !value.Equals("1"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "0～1");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)運搬方法CD
            if (index == 60)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnpanHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)運搬受託者CD
            if (index == 61)
            {
                if (ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間2)車輌CD
            if (index == 66)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckSharyouCd(value, listColumnCheck[61], listColumnCheck[68])))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)車種CD
            if (index == 68)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShahuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)積替・保管有無
            if (index == 70)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 1：有　0：無し
                    if (!value.Equals("0") && !value.Equals("1"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "0～1");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)運搬方法CD
            if (index == 71)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnpanHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分受託者CD
            if (index == 72)
            {
                if (ChkGyosya(value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 運搬先の事業場CD
            if (index == 77)
            {
                if (CheckGenba(listColumnCheck[72], value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 積替保管業者CD
            if (index == 82)
            {
                if (ChkGyosya(value, "TSUMIKAEHOKAN_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 積替保管現場CD
            if (index == 83)
            {
                if (CheckGenba(listColumnCheck[82], value, "TSUMIKAEHOKAN_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 有価物拾集
            if (index == 87)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 1：有　2：無し
                    if (!value.Equals("1") && !value.Equals("2"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "1～2");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 有価物拾集量単位CD
            if (index == 89)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // 1：t　2：m3
                    if (!value.Equals("1") && !value.Equals("2"))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0304"), "1～2");
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnitCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }

                }

                if ((!string.IsNullOrEmpty(listColumnCheck[87]) && string.IsNullOrEmpty(listColumnCheck[88]) && string.IsNullOrEmpty(value))
                    || (string.IsNullOrEmpty(listColumnCheck[87]) && !string.IsNullOrEmpty(listColumnCheck[88]) && string.IsNullOrEmpty(value))
                    || (string.IsNullOrEmpty(listColumnCheck[87]) && string.IsNullOrEmpty(listColumnCheck[88]) && !string.IsNullOrEmpty(value))
                    || (!string.IsNullOrEmpty(listColumnCheck[87]) && !string.IsNullOrEmpty(listColumnCheck[88]) && string.IsNullOrEmpty(value))
                    || (string.IsNullOrEmpty(listColumnCheck[87]) && !string.IsNullOrEmpty(listColumnCheck[88]) && !string.IsNullOrEmpty(value))
                    || (!string.IsNullOrEmpty(listColumnCheck[87]) && string.IsNullOrEmpty(listColumnCheck[88]) && !string.IsNullOrEmpty(value)))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiKenpaiHeader[index - 2] + "、" + UIConstans.ListManiKenpaiHeader[index - 1] + "、" + UIConstans.ListManiKenpaiHeader[index]);
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 処分方法中間処理4CD
            if (index == 94)
            {
                // 1：処分方法中間処理のチェック有
                if (listColumnCheck[93].Equals("1"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }

                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分方法中間処理5CD
            if (index == 96)
            {
                // 1：処分方法中間処理のチェック有
                if (listColumnCheck[95].Equals("1"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }

                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分方法中間処理6CD
            if (index == 98)
            {
                // 1：処分方法中間処理のチェック有
                if (listColumnCheck[97].Equals("1"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }

                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分方法中間処理7CD
            if (index == 100)
            {
                // 1：処分方法中間処理のチェック有
                if (listColumnCheck[99].Equals("1"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }

                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分方法中間処理8CD
            if (index == 102)
            {
                // 1：処分方法中間処理のチェック有
                if (listColumnCheck[101].Equals("1"))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index - 1] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0309"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }

                }

                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間1)運搬の受託者CD
            if (index == 107)
            {
                // 空チェック、マスタ存在チェック、フォーマットチェック
                if ((!string.IsNullOrEmpty(value) && ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                    || (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(listColumnCheck[50])
                        && !(value.PadLeft(6, '0').ToUpper()).Equals(listColumnCheck[50].PadLeft(6, '0').ToUpper())))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間1)運転者CD
            if (index == 109)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUntenShaCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region (区間2)運搬の受託者CD
            if (index == 112)
            {
                // 空チェック、マスタ存在チェック、フォーマットチェック
                if ((!string.IsNullOrEmpty(value) && ChkGyosya(value, "UNPAN_JUTAKUSHA_KAISHA_KBN", listColumnCheck[6]))
                    || (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(listColumnCheck[61])
                        && !(value.PadLeft(6, '0').ToUpper()).Equals(listColumnCheck[61].PadLeft(6, '0').ToUpper())))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region (区間2)運転者CD
            if (index == 114)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUntenShaCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分(1)の受託者CD
            if (index == 117)
            {
                // 空チェック、マスタ存在チェック、フォーマットチェック
                if ((!string.IsNullOrEmpty(value) && ChkGyosya(value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6])))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
                // 処分受託者CDと処分(1)の受託者CDが一致していない場合、エラーとする。
                if ((!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(listColumnCheck[72])
                        && !(value.PadLeft(6, '0').ToUpper()).Equals(listColumnCheck[72].PadLeft(6, '0').ToUpper())))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 処分(1)担当者CD
            if (index == 119)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunTantouCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分(2)の受託者CD
            if (index == 122)
            {
                // 空チェック、マスタ存在チェック、フォーマットチェック
                if ((!string.IsNullOrEmpty(value) && ChkGyosya(value, "SHOBUN_NIOROSHI_GYOUSHA_KBN", listColumnCheck[6])))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
                // 処分受託者CDと処分(2)の受託者CDが一致していない場合、エラーとする。
                if ((!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(listColumnCheck[72])
                        && !(value.PadLeft(6, '0').ToUpper()).Equals(listColumnCheck[72].PadLeft(6, '0').ToUpper())))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 処分(2)担当者CD
            if (index == 124)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunTantouCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分業者CD
            if (index == 127)
            {
                if (ChkGyosya(value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 最終処分現場CD
            if (index == 128)
            {
                if (CheckGenba(listColumnCheck[127], value, "SAISHUU_SHOBUNJOU_KBN", listColumnCheck[6]))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            return error;
        }

        /// <summary>
        /// マスタチェック、その他チェック等(直行のDETAIL用)
        /// </summary>
        /// <param name="manifestBangou">交付番号</param>
        /// <param name="koufuDate">交付年月日</param>
        /// <param name="fileFullName">ファイル名</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="length">項目数</param>
        /// <param name="value">チェック項目</param>
        /// <param name="index">チェック項目の位置</param>
        /// <param name="listColumnCheck">Listチェック項目</param>
        /// <param name="rowNumber">行数</param>
        /// <param name="errorLogTable">エラー内容</param>
        /// <param name="errorCount">エラー件数</param>
        private bool CheckExistsInMasterChokkoForMeisai(string manifestBangou, string koufuDate, string fileFullName, Encoding encoding, int length, string value, int index, string[] listColumnCheck, int rowNumber, ref DataTable errorLogTable, ref int errorCount)
        {
            bool error = false;

            #region 行番号
            if (index == 94)
            {
                List<string> listErrorRownumber = CheckDetailRowNo(listColumnCheck[3], manifestBangou, fileFullName, encoding, length);

                if (listErrorRownumber.Contains((rowNumber).ToString()))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0308"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 廃棄物種類CD
            if (index == 95)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckHaikiShuruiCd(value, 1)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 廃棄物の名称CD
            if (index == 97)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckHaikiNameCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 荷姿CD
            if (index == 99)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckNisugataCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 単位CD
            if (index == 102)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnitCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                    if (string.IsNullOrEmpty(listColumnCheck[101]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiChokkoHeader[index - 1] + "、" + UIConstans.ListManiChokkoHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(listColumnCheck[101]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiChokkoHeader[index - 1] + "、" + UIConstans.ListManiChokkoHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分方法CD
            if (index == 105)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分業者CD
            if (index == 108)
            {
                if (ChkGyosya(value, "SAISHUU_SHOBUNJOU_KBN", koufuDate))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 最終処分場所CD
            if (index == 110)
            {
                if (CheckGenba(listColumnCheck[108], value, "SAISHUU_SHOBUNJOU_KBN", koufuDate))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiChokkoHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            return error;
        }

        /// <summary>
        /// マスタチェック、その他チェック等(積替のDETAIL用)
        /// </summary>
        /// <param name="manifestBangou">交付番号</param>
        /// <param name="koufuDate">交付年月日</param>
        /// <param name="fileFullName">ファイル名</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="length">項目数</param>
        /// <param name="value">チェック項目</param>
        /// <param name="index">チェック項目の位置</param>
        /// <param name="listColumnCheck">Listチェック項目</param>
        /// <param name="rowNumber">行数</param>
        /// <param name="errorLogTable">エラー内容</param>
        /// <param name="errorCount">エラー件数</param>
        private bool CheckExistsInMasterTsumikaeForMeisai(string manifestBangou, string koufuDate, string fileFullName, Encoding encoding, int length, string value, int index, string[] listColumnCheck, int rowNumber, ref DataTable errorLogTable, ref int errorCount)
        {
            bool error = false;

            #region 行番号
            if (index == 159)
            {
                List<string> listErrorRownumber = CheckDetailRowNo(listColumnCheck[3], manifestBangou, fileFullName, encoding, length);

                if (listErrorRownumber.Contains((rowNumber).ToString()))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0308")); ;
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 廃棄物種類CD
            if (index == 160)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckHaikiShuruiCd(value, 3)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 廃棄物の名称CD
            if (index == 162)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckHaikiNameCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 荷姿CD
            if (index == 164)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckNisugataCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 単位CD
            if (index == 167)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnitCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                    if (string.IsNullOrEmpty(listColumnCheck[166]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiTsumikaeHeader[index - 1] + "、" + UIConstans.ListManiTsumikaeHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(listColumnCheck[166]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiTsumikaeHeader[index - 1] + "、" + UIConstans.ListManiTsumikaeHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分方法CD
            if (index == 170)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分業者CD
            if (index == 173)
            {
                if (ChkGyosya(value, "SAISHUU_SHOBUNJOU_KBN", koufuDate))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 最終処分場所CD
            if (index == 175)
            {
                if (CheckGenba(listColumnCheck[173], value, "SAISHUU_SHOBUNJOU_KBN", koufuDate))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiTsumikaeHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            return error;
        }

        /// <summary>
        /// マスタチェック、その他チェック等(建廃のDETAIL用)
        /// </summary>
        /// <param name="manifestBangou">交付番号</param>
        /// <param name="koufuDate">交付年月日</param>
        /// <param name="fileFullName">ファイル名</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="length">項目数</param>
        /// <param name="value">チェック項目</param>
        /// <param name="index">チェック項目の位置</param>
        /// <param name="listColumnCheck">Listチェック項目</param>
        /// <param name="rowNumber">行数</param>
        /// <param name="errorLogTable">エラー内容</param>
        /// <param name="errorCount">エラー件数</param>
        private bool CheckExistsInMasterKenpaiForMeisai(string manifestBangou, string koufuDate, string fileFullName, Encoding encoding, int length, string value, int index, string[] listColumnCheck, int rowNumber, ref DataTable errorLogTable, ref int errorCount)
        {
            bool error = false;

            #region 行番号
            if (index == 155)
            {
                List<string> listErrorRownumber = CheckDetailRowNo(listColumnCheck[3], manifestBangou, fileFullName, encoding, length);

                if (listErrorRownumber.Contains((rowNumber).ToString()))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0308"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 廃棄物種類CD
            if (index == 156)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckHaikiShuruiCd(value, 2)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 廃棄物の名称CD
            if (index == 158)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckHaikiNameCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 荷姿CD
            if (index == 160)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckNisugataCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 単位CD
            if (index == 163)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckUnitCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                    if (string.IsNullOrEmpty(listColumnCheck[162]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiKenpaiHeader[index - 1] + "、" + UIConstans.ListManiKenpaiHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(listColumnCheck[162]))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + string.Format(localMsgLogic.GetMessageString("0306"), UIConstans.ListManiKenpaiHeader[index - 1] + "、" + UIConstans.ListManiKenpaiHeader[index]);
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 処分方法CD
            if (index == 166)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(CheckShobunHouhouuCd(value)))
                    {
                        error = true;
                        DataRow row = errorLogTable.NewRow();
                        row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                        errorLogTable.Rows.Add(row);
                        errorCount++;
                        return true;
                    }
                }
            }
            #endregion

            #region 最終処分業者CD
            if (index == 170)
            {
                if (ChkGyosya(value, "SAISHUU_SHOBUNJOU_KBN", koufuDate))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            #region 最終処分場所CD
            if (index == 172)
            {
                if (CheckGenba(listColumnCheck[170], value, "SAISHUU_SHOBUNJOU_KBN", koufuDate))
                {
                    error = true;
                    DataRow row = errorLogTable.NewRow();
                    row[0] = (rowNumber).ToString() + "行目：" + UIConstans.ListManiKenpaiHeader[index] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0302"));
                    errorLogTable.Rows.Add(row);
                    errorCount++;
                    return true;
                }
            }
            #endregion

            return error;
        }

        /// <summary>
        /// マスタチェック、その他チェック等(紐付用)
        /// </summary>
        /// <param name="listColumnCheck">Listチェック項目</param>
        /// <param name="errorLogTable">エラー内容</param>
        /// <param name="errorCount">エラー件数</param>
        private bool CheckExistsInMasterHimoduke(string[] listColumnCheck, ref DataTable errorLogTableCheck, ref int errorCount)
        {
            bool error = false;

            #region 紐付データ取得
            string firSystemId = string.Empty;
            string firKanriId = string.Empty;
            string systemId = string.Empty;
            string secKanriId = string.Empty;
            this.firstDt = new DataTable();
            this.secondDt = new DataTable();
            this.relationInfoDto = new RelationInfo_DTOCls();
            if ("4".Equals(listColumnCheck[1]))
            {
                DT_R18 entity = new DT_R18();
                entity.MANIFEST_ID = listColumnCheck[0];
                string haikiShuruiCd = string.Empty;
                if (listColumnCheck[2].Length.Equals(7))
                {
                    haikiShuruiCd = listColumnCheck[2];
                }
                secondDt = this.denManiDao.GetDenshiManifestData(entity, haikiShuruiCd, listColumnCheck[3], false);
                if (secondDt.Rows.Count > 0)
                {
                    secKanriId = secondDt.Rows[0]["KANRI_ID"].ToString();
                    relationInfoDto.SECOND_MANI_KBN = listColumnCheck[1];
                    relationInfoDto.SECOND_KANRI_ID = secondDt.Rows[0]["KANRI_ID"].ToString();
                    if (secondDt.Rows[0]["SYSTEM_ID"] != null && !string.IsNullOrEmpty(secondDt.Rows[0]["SYSTEM_ID"].ToString()))
                    {
                        relationInfoDto.SECOND_SYSTEM_ID = Convert.ToInt64(secondDt.Rows[0]["SYSTEM_ID"].ToString());
                    }
                }
            }
            else if (!string.IsNullOrEmpty(listColumnCheck[1]))
            {
                T_MANIFEST_ENTRY entity = new T_MANIFEST_ENTRY();
                entity.HAIKI_KBN_CD = Convert.ToInt16(listColumnCheck[1]);
                entity.MANIFEST_ID = listColumnCheck[0];
                entity.FIRST_MANIFEST_KBN = true;
                secondDt = this.manifestDetailDao.GetKamiManifestData(entity, listColumnCheck[2], listColumnCheck[3]);
                if (secondDt.Rows.Count > 0)
                {
                    systemId = secondDt.Rows[0]["SYSTEM_ID"].ToString();
                    relationInfoDto.HimodukeErrorFlg = false;
                    relationInfoDto.SECOND_MANI_KBN = listColumnCheck[1];
                    relationInfoDto.SECOND_SYSTEM_ID = Convert.ToInt64(secondDt.Rows[0]["SYSTEM_ID"].ToString());
                    relationInfoDto.SECOND_DETAIL_SYSTEM_ID = Convert.ToInt64(secondDt.Rows[0]["DETAIL_SYSTEM_ID"].ToString());
                }
            }

            if ("4".Equals(listColumnCheck[5]))
            {
                DT_R18 entity = new DT_R18();
                entity.MANIFEST_ID = listColumnCheck[4];
                string haikiShuruiCd = string.Empty;
                if (listColumnCheck[6].Length.Equals(7))
                {
                    haikiShuruiCd = listColumnCheck[6];
                }
                firstDt = this.denManiDao.GetDenshiManifestData(entity, haikiShuruiCd, listColumnCheck[7], true);
                if (firstDt.Rows.Count > 0)
                {
                    firKanriId = firstDt.Rows[0]["KANRI_ID"].ToString();
                    relationInfoDto.KANRI_ID = firstDt.Rows[0]["KANRI_ID"].ToString();
                    relationInfoDto.MANIFEST_ID = listColumnCheck[4];
                    relationInfoDto.MANIFEST_TYPE = listColumnCheck[5];
                    relationInfoDto.HAIKI_NAME_CD = listColumnCheck[7];
                    if (firstDt.Rows[0]["SYSTEM_ID"] != null && !string.IsNullOrEmpty(firstDt.Rows[0]["SYSTEM_ID"].ToString()))
                    {
                        relationInfoDto.DT_R18_EX_SYSTEM_ID = Convert.ToInt64(firstDt.Rows[0]["SYSTEM_ID"].ToString());
                    }
                    if (firstDt.Rows[0]["DT_R18_EX_SEQ"] != null && !string.IsNullOrEmpty(firstDt.Rows[0]["DT_R18_EX_SEQ"].ToString()))
                    {
                        relationInfoDto.DT_R18_EX_SEQ = Convert.ToInt32(firstDt.Rows[0]["DT_R18_EX_SEQ"].ToString());
                    }
                    if (firstDt.Rows[0]["KANSAN_SUU"] != null && !string.IsNullOrEmpty(firstDt.Rows[0]["KANSAN_SUU"].ToString()))
                    {
                        relationInfoDto.KANSAN_SUU = firstDt.Rows[0]["KANSAN_SUU"].ToString();
                    }
                    if (firstDt.Rows[0]["DT_R18_EX_TIME_STAMP"] != null && !string.IsNullOrEmpty(firstDt.Rows[0]["DT_R18_EX_TIME_STAMP"].ToString()))
                    {
                        relationInfoDto.DT_R18_EX_TIME_STAMP = ConvertStrByte.In32ToByteArray(Convert.ToInt32(firstDt.Rows[0]["DT_R18_EX_TIME_STAMP"].ToString()));
                    }
                    relationInfoDto.FIRST_SYSTEM_ID = firstDt.Rows[0]["SYSTEM_ID"].ToString();
                }
            }
            else if (!string.IsNullOrEmpty(listColumnCheck[5]))
            {
                T_MANIFEST_ENTRY entity = new T_MANIFEST_ENTRY();
                entity.HAIKI_KBN_CD = Convert.ToInt16(listColumnCheck[5]);
                entity.MANIFEST_ID = listColumnCheck[4];
                entity.FIRST_MANIFEST_KBN = false;
                firstDt = this.manifestDetailDao.GetKamiManifestData(entity, listColumnCheck[6], listColumnCheck[7]);
                if (firstDt.Rows.Count > 0)
                {
                    firSystemId = firstDt.Rows[0]["SYSTEM_ID"].ToString();
                    relationInfoDto.FIRST_SYSTEM_ID = firstDt.Rows[0]["DETAIL_SYSTEM_ID"].ToString();
                    relationInfoDto.MANIFEST_ID = listColumnCheck[4];
                    relationInfoDto.MANIFEST_TYPE = listColumnCheck[5];
                    relationInfoDto.TME_SYSTEM_ID = Convert.ToInt64(firstDt.Rows[0]["SYSTEM_ID"].ToString());
                    relationInfoDto.TME_SEQ = Convert.ToInt32(firstDt.Rows[0]["SEQ"].ToString());
                    relationInfoDto.TME_TIME_STAMP = ConvertStrByte.In32ToByteArray(Convert.ToInt32(firstDt.Rows[0]["TME_TIME_STAMP"].ToString()));
                }
            }
            #endregion

            #region 二次マニが存在チェック
            bool errorFlg = false;
            if (secondDt == null || secondDt.Rows.Count < 1)
            {
                relationInfoDto.HimodukeErrorFlg = true;
                error = true;
                DataRow row = errorLogTableCheck.NewRow();
                row[0] = "紐付を行う二次マニフェストが存在しません。ＣＳＶファイルの確認を行ってください。"
                    + "二次交付番号＝" + listColumnCheck[0]
                    + "、二次廃棄物区分ＣＤ＝" + listColumnCheck[1]
                    + "、二次廃棄物種類ＣＤ＝" + listColumnCheck[2]
                    + "、二次廃棄物名称ＣＤ＝" + listColumnCheck[3];
                errorLogTableCheck.Rows.Add(row);
                errorCount++;
            }
            #endregion

            #region 二次マニが排出事業場の自社区分チェック
            // 紐付条件が合っているか確認する
            if (secondDt != null && secondDt.Rows.Count > 0)
            {
                var keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = secondDt.Rows[0]["HST_GYOUSHA_CD"].ToString();
                keyEntity.GENBA_CD = secondDt.Rows[0]["HST_GENBA_CD"].ToString();
                var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
                keyEntity = dao.GetDataByCd(keyEntity);
                if (keyEntity != null && keyEntity.JISHA_KBN.IsFalse)
                {
                    relationInfoDto.HimodukeErrorFlg = true;
                    error = true;
                    DataRow row = errorLogTableCheck.NewRow();
                    row[0] = "自社以外の排出事業場の場合、紐付処理が行えません。ＣＳＶファイルの確認を行ってください。"
                        + "二次交付番号＝" + listColumnCheck[0]
                        + "、二次廃棄物区分ＣＤ＝" + listColumnCheck[1]
                        + "、二次廃棄物種類ＣＤ＝" + listColumnCheck[2]
                        + "、二次廃棄物名称ＣＤ＝" + listColumnCheck[3];
                    errorLogTableCheck.Rows.Add(row);
                    errorCount++;
                }
            }
            #endregion

            #region 二次抽出した明細数が　＞　１　の場合　　（複数行存在した場合）
            if (secondDt.Rows.Count > 1)
            {
                relationInfoDto.HimodukeErrorFlg = true;
                error = true;
                DataRow row = errorLogTableCheck.NewRow();
                row[0] = "二次マニフェストが複数存在しました。対象の一次マニフェストを手入力で紐付してください。"
                    + "（二次交付番号＝" + listColumnCheck[0]
                    + "、二次廃棄物区分ＣＤ＝" + listColumnCheck[1]
                    + "、二次廃棄物種類ＣＤ＝" + listColumnCheck[2]
                    + "、二次廃棄物名称ＣＤ＝" + listColumnCheck[3];
                errorLogTableCheck.Rows.Add(row);
                errorCount++;
            }
            #endregion

            #region エラーリスト件数　＞　０処理中断
            if (errorLogTableCheck.Rows.Count > 0)
            {
                return error;
            }
            #endregion

            #region 一次マニが存在チェック
            if (firstDt == null || firstDt.Rows.Count < 1)
            {
                relationInfoDto.HimodukeErrorFlg = true;
                error = true;
                DataRow row = errorLogTableCheck.NewRow();
                row[0] = "紐付を行う一次マニフェストが存在しません。ＣＳＶファイルの確認を行ってください。"
                    + "一次交付番号＝" + listColumnCheck[4]
                    + "、一次廃棄物区分ＣＤ＝" + listColumnCheck[5]
                    + "、一次廃棄物種類ＣＤ＝" + listColumnCheck[6]
                    + "、一次廃棄物名称ＣＤ＝" + listColumnCheck[7];
                errorLogTableCheck.Rows.Add(row);
                errorCount++;
            }
            #endregion

            #region 一次抽出した明細数が　＞　１　の場合　　（複数行存在した場合）
            if (firstDt.Rows.Count > 1)
            {
                relationInfoDto.HimodukeErrorFlg = true;
                error = true;
                DataRow row = errorLogTableCheck.NewRow();
                row[0] = "紐付対象の一次マニフェストが複数存在しました。対象の一次マニフェストを手入力で紐付してください。"
                    + "（一次交付番号＝" + listColumnCheck[4]
                    + "、一次廃棄物区分ＣＤ＝" + listColumnCheck[5]
                    + "、一次廃棄物種類ＣＤ＝" + listColumnCheck[6]
                    + "、一次廃棄物名称ＣＤ＝" + listColumnCheck[7];
                errorLogTableCheck.Rows.Add(row);
                errorCount++;
            }
            #endregion

            #region 処分状況のチェック（処分が終わっている　かつ　最終処分が未実施のマニフェスト）
            string strError = string.Empty;
            foreach (DataRow item in firstDt.Rows)
            {
                if (!string.IsNullOrEmpty(item["SBN_END_DATE"].ToString())
                    && string.IsNullOrEmpty(item["LAST_SBN_END_DATE"].ToString()))
                {
                    errorFlg = false;
                }
                else if (string.IsNullOrEmpty(item["SBN_END_DATE"].ToString()))
                {
                    strError = "処分終了日が未登録の場合、紐付を行うことができません。";
                    errorFlg = true;
                }
                else if (!string.IsNullOrEmpty(item["SBN_END_DATE"].ToString())
                    && !string.IsNullOrEmpty(item["LAST_SBN_END_DATE"].ToString()))
                {
                    strError = "最終処分終了日が登録済みの場合、紐付を行うことができません。";
                    errorFlg = true;
                }
            }
            if (errorFlg)
            {
                relationInfoDto.HimodukeErrorFlg = true;
                error = true;
                DataRow row = errorLogTableCheck.NewRow();
                row[0] = strError
                    + "ＣＳＶファイルの確認を行ってください。"
                    + "一次交付番号＝" + listColumnCheck[4]
                    + "、一次廃棄物区分ＣＤ＝" + listColumnCheck[5]
                    + "、一次廃棄物種類ＣＤ＝" + listColumnCheck[6]
                    + "、一次廃棄物名称ＣＤ＝" + listColumnCheck[7];
                errorLogTableCheck.Rows.Add(row);
                errorCount++;
            }
            #endregion

            #region CSVの１次マニフェストが未紐付なこと
            // 紐付け解除されているか確認する
            if (CheckExistManifestHimoduke(listColumnCheck[4], "0", listColumnCheck[5], relationInfoDto.FIRST_SYSTEM_ID))
            {
                relationInfoDto.HimodukeErrorFlg = true;
                error = true;
                DataRow row = errorLogTableCheck.NewRow();
                row[0] = "紐付済の1次マニフェストは、紐付できません。"
                    + "ＣＳＶファイルの確認を行ってください。"
                    + "一次交付番号＝" + listColumnCheck[4]
                    + "、一次廃棄物区分ＣＤ＝" + listColumnCheck[5]
                    + "、一次廃棄物種類ＣＤ＝" + listColumnCheck[6]
                    + "、一次廃棄物名称ＣＤ＝" + listColumnCheck[7];
                errorLogTableCheck.Rows.Add(row);
                errorCount++;
            }
            #endregion

            #region エラーリスト件数　＞　０処理中断
            if (errorLogTableCheck.Rows.Count > 0)
            {
                return error;
            }
            #endregion

            #region １次マニの最終処分場　と　２次マニの最終処分場が異なる場合
            if (!IsHimodukeOk(listColumnCheck, firSystemId, firKanriId, systemId, secKanriId))
            {
                relationInfoDto.HimodukeErrorFlg = true;
                error = true;
                DataRow row = errorLogTableCheck.NewRow();
                row[0] = "紐付条件が合っているか確認する。"
                    + "一次交付番号＝" + listColumnCheck[4]
                    + "、一次廃棄物区分ＣＤ＝" + listColumnCheck[5]
                    + "、一次廃棄物種類ＣＤ＝" + listColumnCheck[6]
                    + "、一次廃棄物名称ＣＤ＝" + listColumnCheck[7];
                errorLogTableCheck.Rows.Add(row);
                errorCount++;
            }
            #endregion

            return error;
        }

        /// <summary>
        /// レコード数チェック 
        /// </summary>
        /// <param name="listColumnCheck">Listチェック項目</param>
        /// <param name="fileFullName">ファイル名</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="length">項目数</param>
        private int GetNumberOfSecondToCheckDataImport(string[] listColumnCheck, string fileFullName, Encoding encoding, int length)
        {
            int s = 0;
            DateTime start = DateTime.Now;
            string manifestBangou = "";
            string koufuDate = "";
            DataTable errorLogTable = new DataTable();
            errorLogTable.Columns.Add();
            int errorCount = 0;

            //■Step2.データチェック

            if (listColumnCheck.Length == length)
            {
                if (IsDenpyouRow(listColumnCheck))
                {
                    manifestBangou = listColumnCheck[4];
                    koufuDate = listColumnCheck[6];
                }

                for (int i = 0; i < length; i++)
                {
                    #region 必須ﾁｪｯｸ
                    if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
                    {
                        if (UIConstans.ListHissuColumnIndexChokko.Contains(i))
                        {
                            if (string.IsNullOrEmpty(listColumnCheck[i]))
                            {
                                continue;
                            }
                        }
                    }
                    else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
                    {
                        if (UIConstans.ListHissuColumnIndexTsumikae.Contains(i))
                        {
                            if (string.IsNullOrEmpty(listColumnCheck[i]))
                            {
                                continue;
                            }
                        }
                    }
                    else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
                    {
                        if (UIConstans.ListHissuColumnIndexKenpai.Contains(i))
                        {
                            if (string.IsNullOrEmpty(listColumnCheck[i]))
                            {
                                continue;
                            }
                        }
                    }
                    else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                    {
                        if (UIConstans.ListHissuColumnIndexHimoduke.Contains(i))
                        {
                            if (string.IsNullOrEmpty(listColumnCheck[i]))
                            {
                                continue;
                            }
                        }
                    }
                    #endregion

                    #region データタイプチェック
                    string errorMsg = "";
                    string type = "";
                    int dataLength = 0;

                    if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_CHOKKOU))
                    {
                        type = UIConstans.ListColumnDataTypeChokko[i].Type;
                        dataLength = UIConstans.ListColumnDataTypeChokko[i].Lenght;
                    }
                    else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
                    {
                        type = UIConstans.ListColumnDataTypeTsumikae[i].Type;
                        dataLength = UIConstans.ListColumnDataTypeTsumikae[i].Lenght;
                    }
                    else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
                    {
                        type = UIConstans.ListColumnDataTypeKenpai[i].Type;
                        dataLength = UIConstans.ListColumnDataTypeKenpai[i].Lenght;
                    }
                    else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                    {
                        type = UIConstans.ListColumnDataTypeHimoduke[i].Type;
                        dataLength = UIConstans.ListColumnDataTypeHimoduke[i].Lenght;
                    }

                    if (this.CheckDataType(listColumnCheck[i], type, dataLength, ref errorMsg))
                    {
                        continue;
                    }
                    #endregion

                    #region マスタチェック、その他チェック等
                    CheckListColumn(listColumnCheck, listColumnCheck[i], i, 1, manifestBangou, koufuDate, fileFullName, encoding, length, ref errorLogTable, ref errorCount);
                    #endregion
                }
            }

            DateTime end = DateTime.Now;
            int count = 0;

            using (StreamReader reader = new StreamReader(fileFullName, encoding))
            {
                string fileContent = reader.ReadLine();
                while (!String.IsNullOrEmpty(fileContent))
                {
                    count++;
                    fileContent = reader.ReadLine();
                }
            }

            s = (int)(end - start).TotalMilliseconds;
            return (s * (count - 1)) / 1000;
        }

        #region 業者
        /// <summary>
        /// 業者をまとめたリスト
        /// </summary>
        private void getListGyoushaDefault()
        {
            var keyEntity = new M_GYOUSHA();
            r_framework.Dao.IM_GYOUSHADao gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.gyoushaList = gyoushaDao.GetAllData().ToList();
        }

        /// <summary>
        /// 業者をまとめたデータテーブル
        /// </summary>
        private void getDatatableGyoushaDefault()
        {
            var keyEntity = new M_GYOUSHA();
            r_framework.Dao.IM_GYOUSHADao gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            string sql = "SELECT M_GYOUSHA.GYOUSHA_CD, M_GYOUSHA.GYOUSHAKBN_MANI, M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN, M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN, M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN, M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN, M_GENBA.SAISHUU_SHOBUNJOU_KBN, M_GENBA.TSUMIKAEHOKAN_KBN, M_GYOUSHA.TEKIYOU_BEGIN, M_GYOUSHA.TEKIYOU_END FROM M_GYOUSHA LEFT JOIN M_GENBA ON M_GYOUSHA.GYOUSHA_CD = M_GENBA.GYOUSHA_CD WHERE M_GYOUSHA.GYOUSHAKBN_MANI = 1 AND M_GYOUSHA.DELETE_FLG = 0";
            this.gyoushaIchiran = gyoushaDao.GetDateForStringSql(sql);
        }

        /// <summary>
        /// 業者チェック(排出事業者、運搬受託者、処分受託者、運搬先の事業者(処分業者)、運搬の受託者、処分の受託者)
        /// </summary>
        /// <param name="gyoushacd">業者CD</param>
        /// <param name="colname">カラム名</param>
        /// <param name="kohuDate">交付年月日</param>
        /// <returns>False：正常　True：エラー</returns>
        private bool ChkGyosya(string gyoushacd, string colname, string kohuDate)
        {
            bool retvalue = false;
            try
            {
                LogUtility.DebugMethodStart(gyoushacd, colname, kohuDate);

                if (string.IsNullOrEmpty(gyoushacd))
                {
                    return false;
                }

                string cd = gyoushacd.PadLeft(6, '0').ToUpper();

                DataTable dt = new DataTable();

                IEnumerable<DataRow> query = from myRow in this.gyoushaIchiran.AsEnumerable()
                                             where myRow.Field<string>("GYOUSHA_CD") == cd
                                             select myRow;
                if (query.Count() > 0)
                    dt = query.CopyToDataTable<DataRow>();

                if (dt.Rows.Count > 0)
                {
                    SqlDateTime tekiyou = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(kohuDate) && (DateTime.TryParse(kohuDate, out date)))
                    {
                        tekiyou = date;
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][colname].ToString() == "True")
                        {
                            //最終処分の場所(予定)業者 & 最終処分業者
                            if (colname.Equals("SAISHUU_SHOBUNJOU_KBN"))
                            {
                                if (dt.Rows[i]["SHOBUN_NIOROSHI_GYOUSHA_KBN"].ToString() == "True")
                                {
                                    SqlDateTime from1 = SqlDateTime.Null;
                                    SqlDateTime to1 = SqlDateTime.Null;
                                    string begin1 = Convert.ToString(dt.Rows[i]["TEKIYOU_BEGIN"]);
                                    string end1 = Convert.ToString(dt.Rows[i]["TEKIYOU_END"]);
                                    if (!string.IsNullOrWhiteSpace(begin1) && (DateTime.TryParse(begin1, out date)))
                                    {
                                        from1 = date;
                                    }
                                    if (!string.IsNullOrWhiteSpace(end1) && (DateTime.TryParse(end1, out date)))
                                    {
                                        to1 = date;
                                    }

                                    if (from1.IsNull && to1.IsNull)
                                    {
                                        return retvalue;
                                    }
                                    else if (from1.IsNull && !to1.IsNull && tekiyou.CompareTo(to1) <= 0)
                                    {
                                        return retvalue;
                                    }
                                    else if (!from1.IsNull && to1.IsNull && tekiyou.CompareTo(from1) >= 0)
                                    {
                                        return retvalue;
                                    }
                                    else if (!from1.IsNull && !to1.IsNull && tekiyou.CompareTo(from1) >= 0
                                            && tekiyou.CompareTo(to1) <= 0)
                                    {
                                        return retvalue;
                                    }
                                }
                                return true;
                            }

                            SqlDateTime from = SqlDateTime.Null;
                            SqlDateTime to = SqlDateTime.Null;
                            string begin = Convert.ToString(dt.Rows[i]["TEKIYOU_BEGIN"]);
                            string end = Convert.ToString(dt.Rows[i]["TEKIYOU_END"]);
                            if (!string.IsNullOrWhiteSpace(begin) && (DateTime.TryParse(begin, out date)))
                            {
                                from = date;
                            }
                            if (!string.IsNullOrWhiteSpace(end) && (DateTime.TryParse(end, out date)))
                            {
                                to = date;
                            }

                            if (from.IsNull && to.IsNull)
                            {
                                return retvalue;
                            }
                            else if (from.IsNull && !to.IsNull && tekiyou.CompareTo(to) <= 0)
                            {
                                return retvalue;
                            }
                            else if (!from.IsNull && to.IsNull && tekiyou.CompareTo(from) >= 0)
                            {
                                return retvalue;
                            }
                            else if (!from.IsNull && !to.IsNull && tekiyou.CompareTo(from) >= 0
                                    && tekiyou.CompareTo(to) <= 0)
                            {
                                return retvalue;
                            }
                        }
                    }
                }
                retvalue = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGyosya", ex);
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(gyoushacd, colname, kohuDate);
            }
            return retvalue;
        }

        /// <summary>
        /// 業者エンティティを取得する
        /// </summary>
        /// <param name="gyoushacd">業者CD</param>
        private M_GYOUSHA GetGyousha(string gyoushacd)
        {
            M_GYOUSHA gyousha = this.gyoushaList.Where(h => h.GYOUSHA_CD == gyoushacd).FirstOrDefault();
            return gyousha;
        }
        #endregion

        #region 都道府県取得

        /// <summary>
        /// 都道府県取得をまとめたリスト
        /// </summary>
        private void getListTodoufukenDefault()
        {
            var keyEntity = new M_TODOUFUKEN();
            r_framework.Dao.IM_TODOUFUKENDao todoufukenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TODOUFUKENDao>();
            this.todoufukenList = todoufukenDao.GetAllData().ToList();
        }

        /// <summary>
        /// 都道府県取得
        /// </summary>
        /// <param name="todofukenCd">都道府県CD</param>
        private string GetTodofukenName(string todofukenCd)
        {
            if (string.IsNullOrEmpty(todofukenCd))
            {
                return null;
            }

            M_TODOUFUKEN todoufukenEntity = this.todoufukenList.Where(h => h.TODOUFUKEN_CD.Value == Convert.ToInt16(todofukenCd)).FirstOrDefault();
            if (todoufukenEntity == null)
            {
                return null;
            }
            else
            {
                return todoufukenEntity.TODOUFUKEN_NAME;
            }
        }
        #endregion

        #region 現場
        /// <summary>
        /// 現場をまとめたリスト
        /// </summary>
        private void getListGenbaDefault()
        {
            var keyEntity = new M_GENBA();
            r_framework.Dao.IM_GENBADao genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.genbaList = genbaDao.GetAllData().ToList();
        }

        /// <summary>
        /// 現場をまとめたデータテーブル
        /// </summary>
        private void getDatatableGenbaDefault()
        {
            var keyEntity = new M_GENBA();
            r_framework.Dao.IM_GENBADao genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            string sql = "SELECT M_GENBA.GYOUSHA_CD, M_GENBA.GENBA_CD, M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN, M_GENBA.SHOBUN_NIOROSHI_GENBA_KBN, M_GENBA.SAISHUU_SHOBUNJOU_KBN, M_GENBA.TSUMIKAEHOKAN_KBN, M_GENBA.TEKIYOU_BEGIN, M_GENBA.TEKIYOU_END, M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN, M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN FROM M_GENBA INNER JOIN M_GYOUSHA ON M_GYOUSHA.GYOUSHA_CD = M_GENBA.GYOUSHA_CD WHERE M_GYOUSHA.GYOUSHAKBN_MANI = 1 AND M_GENBA.DELETE_FLG = 0";
            this.genbaIchiran = genbaDao.GetDateForStringSql(sql);
        }


        /// <summary>
        /// 現場CD
        /// </summary>
        /// <param name="gyoushacd">業者CD</param>
        /// <param name="genbacd">現場CD</param>
        /// <param name="colname">カラム名</param>
        /// <param name="kohuDate">交付年月日</param>
        private bool CheckGenba(string gyoushacd, string genbacd, string colname, string kohuDate)
        {
            bool retvalue = false;
            try
            {
                LogUtility.DebugMethodStart(gyoushacd, genbacd, colname, kohuDate);

                if (string.IsNullOrEmpty(genbacd))
                {
                    return false;
                }
                else
                {
                    if (string.IsNullOrEmpty(gyoushacd))
                    {
                        return true;
                    }
                }

                DataTable dt = new DataTable();

                IEnumerable<DataRow> query = from myRow in this.genbaIchiran.AsEnumerable()
                                             where myRow.Field<string>("GYOUSHA_CD") == gyoushacd.PadLeft(6, '0').ToUpper()
                                             && myRow.Field<string>("GENBA_CD") == genbacd.PadLeft(6, '0').ToUpper()
                                             select myRow;
                if (query.Count() > 0)
                {
                    dt = query.CopyToDataTable<DataRow>();
                }

                if (dt.Rows.Count > 0)
                {
                    SqlDateTime tekiyou = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(kohuDate) && (DateTime.TryParse(kohuDate, out date)))
                    {
                        tekiyou = date;
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][colname].ToString() == "True")
                        {
                            // 排出事業場CD
                            if (colname.Equals("HAISHUTSU_NIZUMI_GENBA_KBN"))
                            {
                                if (dt.Rows[i]["HAISHUTSU_NIZUMI_GENBA_KBN"].ToString() == "True")
                                {
                                    SqlDateTime from = SqlDateTime.Null;
                                    SqlDateTime to = SqlDateTime.Null;
                                    string begin = Convert.ToString(dt.Rows[i]["TEKIYOU_BEGIN"]);
                                    string end = Convert.ToString(dt.Rows[i]["TEKIYOU_END"]);
                                    if (!string.IsNullOrWhiteSpace(begin) && (DateTime.TryParse(begin, out date)))
                                    {
                                        from = date;
                                    }
                                    if (!string.IsNullOrWhiteSpace(end) && (DateTime.TryParse(end, out date)))
                                    {
                                        to = date;
                                    }

                                    if (from.IsNull && to.IsNull)
                                    {
                                        return retvalue;
                                    }
                                    else if (from.IsNull && !to.IsNull && tekiyou.CompareTo(to) <= 0)
                                    {
                                        return retvalue;
                                    }
                                    else if (!from.IsNull && to.IsNull && tekiyou.CompareTo(from) >= 0)
                                    {
                                        return retvalue;
                                    }
                                    else if (!from.IsNull && !to.IsNull && tekiyou.CompareTo(from) >= 0
                                            && tekiyou.CompareTo(to) <= 0)
                                    {
                                        return retvalue;
                                    }
                                }
                                return true;
                            }

                            // 運搬先の事業場CD
                            if (colname.Equals("SHOBUN_NIOROSHI_GYOUSHA_KBN"))
                            {
                                if (dt.Rows[i]["SHOBUN_NIOROSHI_GENBA_KBN"].ToString() == "True"
                                    || dt.Rows[i]["SAISHUU_SHOBUNJOU_KBN"].ToString() == "True")
                                {
                                    SqlDateTime from = SqlDateTime.Null;
                                    SqlDateTime to = SqlDateTime.Null;
                                    string begin = Convert.ToString(dt.Rows[i]["TEKIYOU_BEGIN"]);
                                    string end = Convert.ToString(dt.Rows[i]["TEKIYOU_END"]);
                                    if (!string.IsNullOrWhiteSpace(begin) && (DateTime.TryParse(begin, out date)))
                                    {
                                        from = date;
                                    }
                                    if (!string.IsNullOrWhiteSpace(end) && (DateTime.TryParse(end, out date)))
                                    {
                                        to = date;
                                    }

                                    if (from.IsNull && to.IsNull)
                                    {
                                        return retvalue;
                                    }
                                    else if (from.IsNull && !to.IsNull && tekiyou.CompareTo(to) <= 0)
                                    {
                                        return retvalue;
                                    }
                                    else if (!from.IsNull && to.IsNull && tekiyou.CompareTo(from) >= 0)
                                    {
                                        return retvalue;
                                    }
                                    else if (!from.IsNull && !to.IsNull && tekiyou.CompareTo(from) >= 0
                                            && tekiyou.CompareTo(to) <= 0)
                                    {
                                        return retvalue;
                                    }
                                }
                                return true;
                            }

                            // 最終処分の場所(予定)現場 & 最終処分現場
                            if (colname.Equals("SAISHUU_SHOBUNJOU_KBN"))
                            {
                                if (dt.Rows[i]["SHOBUN_NIOROSHI_GYOUSHA_KBN"].ToString() == "True")
                                {
                                    SqlDateTime from = SqlDateTime.Null;
                                    SqlDateTime to = SqlDateTime.Null;
                                    string begin = Convert.ToString(dt.Rows[i]["TEKIYOU_BEGIN"]);
                                    string end = Convert.ToString(dt.Rows[i]["TEKIYOU_END"]);
                                    if (!string.IsNullOrWhiteSpace(begin) && (DateTime.TryParse(begin, out date)))
                                    {
                                        from = date;
                                    }
                                    if (!string.IsNullOrWhiteSpace(end) && (DateTime.TryParse(end, out date)))
                                    {
                                        to = date;
                                    }

                                    if (from.IsNull && to.IsNull)
                                    {
                                        return retvalue;
                                    }
                                    else if (from.IsNull && !to.IsNull && tekiyou.CompareTo(to) <= 0)
                                    {
                                        return retvalue;
                                    }
                                    else if (!from.IsNull && to.IsNull && tekiyou.CompareTo(from) >= 0)
                                    {
                                        return retvalue;
                                    }
                                    else if (!from.IsNull && !to.IsNull && tekiyou.CompareTo(from) >= 0
                                            && tekiyou.CompareTo(to) <= 0)
                                    {
                                        return retvalue;
                                    }
                                }
                                return true;
                            }

                            SqlDateTime from1 = SqlDateTime.Null;
                            SqlDateTime to1 = SqlDateTime.Null;
                            string begin1 = Convert.ToString(dt.Rows[i]["TEKIYOU_BEGIN"]);
                            string end1 = Convert.ToString(dt.Rows[i]["TEKIYOU_END"]);
                            if (!string.IsNullOrWhiteSpace(begin1) && (DateTime.TryParse(begin1, out date)))
                            {
                                from1 = date;
                            }
                            if (!string.IsNullOrWhiteSpace(end1) && (DateTime.TryParse(end1, out date)))
                            {
                                to1 = date;
                            }

                            if (from1.IsNull && to1.IsNull)
                            {
                                return retvalue;
                            }
                            else if (from1.IsNull && !to1.IsNull && tekiyou.CompareTo(to1) <= 0)
                            {
                                return retvalue;
                            }
                            else if (!from1.IsNull && to1.IsNull && tekiyou.CompareTo(from1) >= 0)
                            {
                                return retvalue;
                            }
                            else if (!from1.IsNull && !to1.IsNull && tekiyou.CompareTo(from1) >= 0
                                    && tekiyou.CompareTo(to1) <= 0)
                            {
                                return retvalue;
                            }
                        }
                    }
                }
                retvalue = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(gyoushacd, genbacd, colname, kohuDate);
            }
            return retvalue;
        }


        /// <summary>
        /// 現場エンティティを取得する
        /// </summary>
        /// <param name="gyoushacd">業者CD</param>
        /// <param name="genbacd">現場CD</param>
        private M_GENBA GetGenba(string gyoushacd, string genbacd)
        {
            gyoushacd = gyoushacd.PadLeft(6, '0');
            genbacd = genbacd.PadLeft(6, '0');
            M_GENBA genba = this.genbaList.Where(h => h.GYOUSHA_CD == gyoushacd && h.GENBA_CD == genbacd).FirstOrDefault();
            return genba;
        }
        #endregion

        #region 拠点
        /// <summary>
        /// 拠点をまとめたリスト
        /// </summary>
        private void getListKyotenDefault()
        {
            var keyEntity = new M_KYOTEN();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            this.kyotenList = dao.GetAllValidData(keyEntity).ToList();
        }

        /// <summary>
        /// 拠点CDの存在チェック
        /// </summary>
        /// <param name="kyotencd">拠点CD</param>
        private string CheckKyotenCd(string kyotencd)
        {
            string retvalue = string.Empty;

            short cd = -1;
            if (!short.TryParse(kyotencd, out cd))
            {
                return retvalue;
            }

            M_KYOTEN kyotens = this.kyotenList.Where(h => h.KYOTEN_CD.Value == cd).FirstOrDefault();

            // 存在チェック
            // 拠点CD「99:全社」の場合はエラーとする
            if (kyotens == null || cd == 99)
            {
                return retvalue;
            }
            else
            {
                retvalue = kyotens.KYOTEN_NAME.ToString();
            }
            return retvalue;
        }


        #endregion

        #region 運搬方法
        /// <summary>
        /// 運搬方法をまとめたリスト
        /// </summary>
        private void getListUnpanHouhouDefault()
        {
            var keyEntity = new M_UNPAN_HOUHOU();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_UNPAN_HOUHOUDao>();
            this.unpanHouhouList = dao.GetAllValidData(keyEntity).ToList();
        }

        /// <summary>
        /// 運搬方法CDの存在チェック
        /// </summary>
        /// <param name="unpanHouhouucd">運搬方法CD</param>
        private string CheckUnpanHouhouuCd(string unpanHouhouucd)
        {
            string retvalue = string.Empty;

            M_UNPAN_HOUHOU unpanHouhouus = this.unpanHouhouList.Where(h => h.UNPAN_HOUHOU_CD == unpanHouhouucd.ToUpper() && h.KAMI_USE_KBN.IsTrue).FirstOrDefault();

            // 存在チェック
            if (unpanHouhouus == null)
            {
                return retvalue;
            }
            else
            {
                retvalue = unpanHouhouus.UNPAN_HOUHOU_NAME.ToString();
            }
            return retvalue;
        }
        #endregion

        #region 車輌
        /// <summary>
        /// 車輌をまとめたリスト
        /// </summary>
        private void getListSharyouDefault()
        {
            var keyEntity = new M_SHARYOU();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();
            this.sharyouList = dao.GetAllValidData(keyEntity).ToList();
        }

        /// <summary>
        /// 車輌CDの存在チェック
        /// </summary>
        /// <param name="sharyoucd">車輌CD</param>
        /// <param name="gyoushacd">業者CD</param>
        /// <param name="shashuCd">車種CD</param>
        private string CheckSharyouCd(string sharyoucd, string gyoushacd, string shashuCd)
        {
            string retvalue = string.Empty;

            string cd = sharyoucd.PadLeft(6, '0').ToUpper();

            if (string.IsNullOrEmpty(gyoushacd))
            {
                return retvalue;
            }

            string gyoCd = gyoushacd.PadLeft(6, '0').ToUpper();
            string shashucd = "";

            if (!string.IsNullOrEmpty(shashuCd))
            {
                shashucd = shashuCd.PadLeft(3, '0').ToUpper();
            }

            M_SHARYOU sharyous = this.sharyouList.Where(h => h.SHARYOU_CD == cd && h.GYOUSHA_CD == gyoCd && h.SHASYU_CD == shashucd).FirstOrDefault();

            // 存在チェック
            if (sharyous == null)
            {
                return retvalue;
            }
            else
            {
                retvalue = sharyous.SHARYOU_NAME.ToString();
            }
            return retvalue;
        }
        #endregion

        #region 車種
        /// <summary>
        /// 車種をまとめたリスト
        /// </summary>
        private void getListShashuDefault()
        {
            var keyEntity = new M_SHASHU();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();
            this.shashuList = dao.GetAllValidData(keyEntity).ToList();
        }

        /// <summary>
        /// 種類CDの存在チェック
        /// </summary>
        /// <param name="shashucd">車種CD</param>
        private string CheckShahuCd(string shashucd)
        {
            string retvalue = string.Empty;

            string cd = shashucd.PadLeft(3, '0').ToUpper();

            M_SHASHU shahus = this.shashuList.Where(h => h.SHASHU_CD == cd).FirstOrDefault();

            // 存在チェック
            if (shahus == null)
            {
                return retvalue;
            }
            else
            {
                retvalue = shahus.SHASHU_NAME.ToString();
            }
            return retvalue;
        }
        #endregion

        #region 有害物質
        /// <summary>
        /// 有害物質をまとめたリスト
        /// </summary>
        private void getListYuugaiBusshitsuDefault()
        {
            var keyEntity = new M_YUUGAI_BUSSHITSU();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_YUUGAI_BUSSHITSUDao>();
            this.yuugaiBusshitsuList = dao.GetAllValidData(keyEntity).ToList();
        }

        /// <summary>
        /// 有害物質CDの存在チェック
        /// </summary>
        /// <param name="yuugaiBusshitsucd">有害物質CD</param>
        private string CheckYuugaiBusshitsuCd(string yuugaiBusshitsucd)
        {
            string retvalue = string.Empty;

            string cd = yuugaiBusshitsucd.PadLeft(2, '0').ToUpper();

            M_YUUGAI_BUSSHITSU yuugaiBusshitsus = this.yuugaiBusshitsuList.Where(h => h.YUUGAI_BUSSHITSU_CD == cd).FirstOrDefault();

            // 存在チェック
            if (yuugaiBusshitsus == null)
            {
                return retvalue;
            }
            else
            {
                retvalue = yuugaiBusshitsus.YUUGAI_BUSSHITSU_NAME.ToString();
            }
            return retvalue;
        }
        #endregion

        #region 社員
        /// <summary>
        /// 社員をまとめたリスト
        /// </summary>
        private void getListShainDefault()
        {
            var keyEntity = new M_SHAIN();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();
            this.shainList = dao.GetAllData().Where(h => h.DELETE_FLG.IsFalse).ToList();
        }

        /// <summary>
        /// 運転者CDの存在チェック
        /// </summary>
        /// <param name="shaincd">社員CD</param>
        private string CheckUntenShaCd(string shaincd)
        {
            string retvalue = string.Empty;

            string cd = shaincd.PadLeft(6, '0').ToUpper();

            M_SHAIN shains = this.shainList.Where(h => h.SHAIN_CD == cd && h.UNTEN_KBN.IsTrue).FirstOrDefault();

            // 存在チェック
            if (shains == null)
            {
                return retvalue;
            }
            else
            {
                retvalue = shains.SHAIN_NAME.ToString();
            }
            return retvalue;
        }

        /// <summary>
        /// 処分担当者CDの存在チェック
        /// </summary>
        /// <param name="shaincd">社員CD</param>
        private string CheckShobunTantouCd(string shaincd)
        {
            string retvalue = string.Empty;

            string cd = shaincd.PadLeft(6, '0').ToUpper();

            M_SHAIN shains = this.shainList.Where(h => h.SHAIN_CD == cd && h.SHOBUN_TANTOU_KBN.IsTrue).FirstOrDefault();

            // 存在チェック
            if (shains == null)
            {
                return retvalue;
            }
            else
            {
                retvalue = shains.SHAIN_NAME.ToString();
            }
            return retvalue;
        }
        #endregion

        #region 単位
        /// <summary>
        /// 単位をまとめたリスト
        /// </summary>
        private void getListUnitDefault()
        {
            var keyEntity = new M_UNIT();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_UNITDao>();
            this.unitList = dao.GetAllValidData(keyEntity).ToList();
        }

        /// <summary>
        /// 単位CDの存在チェック
        /// </summary>
        /// <param name="shaincd">単位CD</param>
        private string CheckUnitCd(string unitcd)
        {
            string retvalue = string.Empty;

            short cd = -1;
            if (!short.TryParse(unitcd, out cd))
            {
                return retvalue;
            }

            M_UNIT units = this.unitList.Where(h => h.UNIT_CD.Value == cd && h.KAMI_USE_KBN.IsTrue).FirstOrDefault();

            // 存在チェック
            if (units == null)
            {
                return retvalue;
            }
            else
            {
                retvalue = units.UNIT_NAME.ToString();
            }
            return retvalue;
        }
        #endregion

        #region 廃棄物種類
        /// <summary>
        /// 廃棄物種類をまとめたリスト
        /// </summary>
        private void getListHaikiShuruiDefault()
        {
            var keyEntity = new M_HAIKI_SHURUI();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_HAIKI_SHURUIDao>();
            this.haikiShuruiList = dao.GetAllValidData(keyEntity).ToList();
        }

        /// <summary>
        /// 廃棄物種類CDの存在チェック
        /// </summary>
        /// <param name="haikiShuruicd">廃棄種類CD</param>
        /// <param name="haikiKbn">廃棄区分</param>
        private string CheckHaikiShuruiCd(string haikiShuruicd, int haikiKbn)
        {
            string retvalue = string.Empty;

            string cd = haikiShuruicd.PadLeft(4, '0').ToUpper();

            M_HAIKI_SHURUI haikiShuruis = this.haikiShuruiList.Where(h => h.HAIKI_SHURUI_CD == cd && h.HAIKI_KBN_CD.Value == haikiKbn).FirstOrDefault();

            // 存在チェック
            if (haikiShuruis == null)
            {
                return retvalue;
            }
            else
            {
                retvalue = haikiShuruis.HAIKI_SHURUI_NAME.ToString();
            }
            return retvalue;
        }
        #endregion

        #region 廃棄物名称
        /// <summary>
        /// 廃棄物名称をまとめたリスト
        /// </summary>
        private void getListHaikiNameDefault()
        {
            var keyEntity = new M_HAIKI_NAME();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_HAIKI_NAMEDao>();
            this.haikiNameList = dao.GetAllValidData(keyEntity).ToList();
        }

        /// <summary>
        /// 廃棄物名称CDの存在チェック
        /// </summary>
        /// <param name="haikiNamecd">廃棄物名称CD</param>
        private string CheckHaikiNameCd(string haikiNamecd)
        {
            string retvalue = string.Empty;

            string cd = haikiNamecd.PadLeft(6, '0').ToUpper();

            M_HAIKI_NAME haikiNames = this.haikiNameList.Where(h => h.HAIKI_NAME_CD == cd).FirstOrDefault();

            // 存在チェック
            if (haikiNames == null)
            {
                return retvalue;
            }
            else
            {
                retvalue = haikiNames.HAIKI_NAME.ToString();
            }
            return retvalue;
        }
        #endregion

        #region 荷姿
        /// <summary>
        /// 荷姿をまとめたリスト
        /// </summary>
        private void getListNisugataDefault()
        {
            var keyEntity = new M_NISUGATA();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_NISUGATADao>();
            this.nisugataList = dao.GetAllValidData(keyEntity).ToList();
        }

        /// <summary>
        /// 荷姿CDの存在チェック
        /// </summary>
        /// <param name="nisugatacd">荷姿CD</param>
        private string CheckNisugataCd(string nisugatacd)
        {
            string retvalue = string.Empty;

            string cd = nisugatacd.PadLeft(2, '0').ToUpper();

            M_NISUGATA nisugatas = this.nisugataList.Where(h => h.NISUGATA_CD == cd && h.KAMI_USE_KBN.IsTrue).FirstOrDefault();

            // 存在チェック
            if (nisugatas == null)
            {
                return retvalue;
            }
            else
            {
                retvalue = nisugatas.NISUGATA_NAME.ToString();
            }
            return retvalue;
        }
        /// <summary>
        /// 荷姿をまとめたデータテーブル
        /// </summary>
        private void getDatatableNisugataDefault()
        {
            r_framework.Dao.IM_GYOUSHADao gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            string sql = "SELECT NISUGATA_CD, NISUGATA_NAME, NISUGATA_NAME_RYAKU, NISUGATA_BIKOU, DENSHI_USE_KBN, KAMI_USE_KBN FROM M_NISUGATA WHERE NISUGATA_CD >= '05' AND NISUGATA_CD <= '99' AND DELETE_FLG = 0 AND KAMI_USE_KBN = 1 ";
            this.nisugataIchiran = gyoushaDao.GetDateForStringSql(sql);
        }

        /// <summary>
        /// 荷姿チェック
        /// </summary>
        /// <param name="nisugatacd">荷姿CD</param>
        private bool ChkNisugata(string nisugatacd)
        {
            bool retvalue = false;
            try
            {
                LogUtility.DebugMethodStart(nisugatacd);

                if (string.IsNullOrEmpty(nisugatacd))
                {
                    return true;
                }

                string cd = nisugatacd.PadLeft(2, '0').ToUpper();

                DataTable dt = new DataTable();

                IEnumerable<DataRow> query = from myRow in this.nisugataIchiran.AsEnumerable()
                                             where myRow.Field<string>("NISUGATA_CD") == cd
                                             select myRow;
                if (query.Count() > 0)
                {
                    dt = query.CopyToDataTable<DataRow>();
                }

                if (dt.Rows.Count == 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkNisugata", ex);
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(nisugatacd);
            }
            return retvalue;
        }

        /// <summary>
        /// 荷姿を取得
        /// </summary>
        /// <param name="table">T_MANIFEST_KP_NISUGATAのデータテーブル</param>
        /// <param name="recNo">印字番号</param>
        private DataTable GetNisugata(DataTable table, int recNo)
        {
            DataTable dt = null;
            if (table.Rows.Count > 0)
            {
                IEnumerable<DataRow> query = from myRow in table.AsEnumerable()
                                             where myRow.Field<Int16>("REC_NO") == recNo
                                             select myRow;
                if (query.Count() > 0)
                {
                    dt = query.CopyToDataTable<DataRow>();
                }
            }
            return dt;
        }
        #endregion

        #region 処分方法
        /// <summary>
        /// 処分方法をまとめたリスト
        /// </summary>
        private void getListShobunHouhouDefault()
        {
            var keyEntity = new M_SHOBUN_HOUHOU();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHOBUN_HOUHOUDao>();
            this.shobunHouhouList = dao.GetAllValidData(keyEntity).ToList();
        }

        /// <summary>
        /// 処分方法CDの存在チェック
        /// </summary>
        /// <param name="shobunHouhouucd">処分方法CD</param>
        private string CheckShobunHouhouuCd(string shobunHouhouucd)
        {
            string retvalue = string.Empty;

            string cd = shobunHouhouucd.PadLeft(3, '0').ToUpper();

            M_SHOBUN_HOUHOU shobunHouhouus = this.shobunHouhouList.Where(h => h.SHOBUN_HOUHOU_CD == cd && h.KAMI_USE_KBN.IsTrue).FirstOrDefault();

            // 存在チェック
            if (shobunHouhouus == null)
            {
                return retvalue;
            }
            else
            {
                retvalue = shobunHouhouus.SHOBUN_HOUHOU_NAME.ToString();
            }
            return retvalue;
        }


        /// <summary>
        /// 処分方法を取得
        /// </summary>
        /// <param name="table">T_MANIFEST_KP_SBN_HOUHOUのデータテーブル</param>
        /// <param name="recNo">印字番号</param>
        private DataTable GetSbnHouhou(DataTable table, int recNo)
        {
            DataTable dt = null;
            if (table.Rows.Count > 0)
            {
                IEnumerable<DataRow> query = from myRow in table.AsEnumerable()
                                             where myRow.Field<Int16>("REC_NO") == recNo
                                             select myRow;
                if (query.Count() > 0)
                {
                    dt = query.CopyToDataTable<DataRow>();
                }
            }
            return dt;
        }
        #endregion

        #region 形状
        /// <summary>
        /// 形状をまとめたデータテーブル
        /// </summary>
        private void getDatatableKeijouDefault()
        {
            r_framework.Dao.IM_GYOUSHADao gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            string sql = "SELECT KEIJOU_CD, KEIJOU_NAME, KEIJOU_NAME_RYAKU FROM M_KEIJOU WHERE KEIJOU_CD >= '04' AND KEIJOU_CD <= '99' AND DELETE_FLG = 0 ";
            this.keijouIchiran = gyoushaDao.GetDateForStringSql(sql);
        }

        /// <summary>
        /// 形状チェック
        /// </summary>
        /// <param name="keijoucd">形状CD</param>
        private string ChkKeijou(string keijoucd)
        {
            try
            {
                LogUtility.DebugMethodStart(keijoucd);

                if (string.IsNullOrEmpty(keijoucd))
                {
                    return string.Empty;
                }

                string cd = keijoucd.PadLeft(2, '0').ToUpper();

                DataTable dt = new DataTable();

                IEnumerable<DataRow> query = from myRow in this.keijouIchiran.AsEnumerable()
                                             where myRow.Field<string>("KEIJOU_CD") == cd
                                             select myRow;
                if (query.Count() > 0)
                {
                    dt = query.CopyToDataTable<DataRow>();
                    return Convert.ToString(dt.Rows[0]["KEIJOU_NAME_RYAKU"]);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkKeijou", ex);
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(keijoucd);
            }
            return string.Empty;
        }

        /// <summary>
        /// 形状を取得
        /// </summary>
        /// <param name="table">T_MANIFEST_PT_KP_KEIJYOUのデータテーブル</param>
        /// <param name="recNo">印字番号</param>
        private DataTable GetKeijou(DataTable table, int recNo)
        {
            DataTable dt = null;
            if (table.Rows.Count > 0)
            {
                IEnumerable<DataRow> query = from myRow in table.AsEnumerable()
                                             where myRow.Field<Int16>("REC_NO") == recNo
                                             select myRow;
                if (query.Count() > 0)
                {
                    dt = query.CopyToDataTable<DataRow>();
                }
            }
            return dt;
        }
        #endregion

        #region マスタデータを取得
        /// <summary>
        /// マスタデータを取得
        /// </summary>
        private void getDefaultList()
        {
            getListGyoushaDefault();
            getDatatableGyoushaDefault();
            getListKyotenDefault();
            getListUnpanHouhouDefault();
            getListSharyouDefault();
            getListShashuDefault();
            getListYuugaiBusshitsuDefault();
            getListShainDefault();
            getListUnitDefault();
            getListHaikiShuruiDefault();
            getListHaikiNameDefault();
            getListNisugataDefault();
            getListShobunHouhouDefault();
            getListTodoufukenDefault();
            getDatatableKeijouDefault();
            getListGenbaDefault();
            getDatatableGenbaDefault();
            getDatatableNisugataDefault();
        }
        #endregion

        /// <summary>
        /// 登録処理
        /// </summary>
        private bool SetRegist()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<string[]> listdenpyou = new List<string[]>();
                Int16 haikiKbn = Convert.ToInt16(this.form.txtHaikiKbn.Text);
                if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
                {
                    haikiKbn = UIConstans.HAIKI_KBN_TUMIKAE;
                }
                else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
                {
                    haikiKbn = UIConstans.HAIKI_KBN_KENPAI;
                }
                foreach (string[] listdetail in this.listDenpyouManifest)
                {
                    if (IsDenpyouRow(listdetail))
                    {
                        if (listdenpyou.Count != 0)
                        {
                            CreateEntityAndUpdateTables(haikiKbn, listdenpyou);
                            listdenpyou.Clear();
                        }
                        listdenpyou.Add(listdetail);
                    }
                    else
                    {
                        listdenpyou.Add(listdetail);
                    }
                    if (listdenpyou.Count != 0 && this.listDenpyouManifest.IndexOf(listdetail) == this.listDenpyouManifest.Count - 1)
                    {
                        CreateEntityAndUpdateTables(haikiKbn, listdenpyou);
                        listdenpyou.Clear();
                    }
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("SetRegist", sqlEx);
                this.msgLogic.MessageBoxShow("E093", "");
                throw;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetRegist", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(false);
            }
            return false;
        }

        /// <summary>
        /// Entity作成と登録処理
        /// </summary>
        /// <param name="haikiKbn">廃棄物区分</param>
        /// <param name="listdenpyou">マニフェストリスト</param>
        private void CreateEntityAndUpdateTables(Int16 haikiKbn, List<string[]> listdenpyou)
        {
            //登録データ作成
            //システムID(全般･マニ返却日)
            String systemId = "";

            //枝番(全般)
            String seq = "";

            //枝番(マニ返却日)
            String seqRD = "";

            if (!this.mlogic.ExistKohuNo(haikiKbn.ToString(), listdenpyou[0][4].ToString(), ref systemId, ref seq, ref seqRD))
            {
                var search = new CommonSerchParameterDtoCls();
                search.SYSTEM_ID = systemId;
                search.HAIKI_KBN_CD = haikiKbn.ToString();
                DataTable dt = this.mlogic.SearchData(search);
                if (dt.Rows.Count != 0)
                {
                    DataRow dr = dt.Rows[0];
                    //タイムスタンプ
                    if (dr["TME_TIME_STAMP"].ToString() != string.Empty)
                    {
                        int data2 = (int)dr["TME_TIME_STAMP"];
                        byte[] d = Shougun.Core.Common.BusinessCommon.Utility.ConvertStrByte.In32ToByteArray(data2);
                        this.timeStampEntry = d;
                    }
                    //タイムスタンプ
                    if (string.IsNullOrEmpty(dr["TMRD_TIME_STAMP"].ToString()) == false)
                    {
                        int data2 = (int)dr["TMRD_TIME_STAMP"];
                        byte[] d = Shougun.Core.Common.BusinessCommon.Utility.ConvertStrByte.In32ToByteArray(data2);
                        this.timeStampRetDate = d;
                    }
                    // マニフェスト区分 
                    bool manifestKbn = false;
                    if (dr["FIRST_MANIFEST_KBN"].ToString() == "True")
                    {
                        manifestKbn = true;
                    }
                    // 交付番号区分
                    Int16 kofuKbn = 0;
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["KOUFU_KBN"])))
                    {
                        kofuKbn = Convert.ToInt16(dr["KOUFU_KBN"]);
                    }
                    this.Update(listdenpyou, systemId, seq, seqRD, manifestKbn, kofuKbn);
                }
            }
            else
            {
                this.Regist(listdenpyou);
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="listmanifest">マニフェストリスト</param>
        [Transaction]
        private void Regist(List<string[]> listmanifest)
        {
            try
            {
                LogUtility.DebugMethodStart(listmanifest);
                List<T_MANIFEST_ENTRY> entrylist = new List<T_MANIFEST_ENTRY>();
                List<T_MANIFEST_UPN> upnlist = new List<T_MANIFEST_UPN>();
                List<T_MANIFEST_PRT> prtlist = new List<T_MANIFEST_PRT>();
                List<T_MANIFEST_DETAIL_PRT> detailprtlist = new List<T_MANIFEST_DETAIL_PRT>();
                List<T_MANIFEST_KP_KEIJYOU> keijyoulist = new List<T_MANIFEST_KP_KEIJYOU>();
                List<T_MANIFEST_KP_NISUGATA> niugatalist = new List<T_MANIFEST_KP_NISUGATA>();
                List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist = new List<T_MANIFEST_KP_SBN_HOUHOU>();
                List<T_MANIFEST_DETAIL> detaillist = new List<T_MANIFEST_DETAIL>();
                List<T_MANIFEST_RET_DATE> retdatelist = new List<T_MANIFEST_RET_DATE>();

                //登録データ作成
                //システムID(全般･マニ返却日)
                String systemID = "";

                //枝番(全般)
                String seq = "";

                //枝番(マニ返却日)
                String seqRD = "";

                bool manifestKbn = ToBoolean(listmanifest[0][2]);

                this.MakeData(ref entrylist, ref upnlist, ref prtlist, ref detailprtlist, ref keijyoulist, ref niugatalist, ref houhoulist, ref detaillist, ref retdatelist, false, systemID, seq, seqRD, manifestKbn, 0, listmanifest);

                using (Transaction tran = new Transaction())
                {
                    mlogic.Insert(entrylist, upnlist, prtlist, detailprtlist, keijyoulist, niugatalist, houhoulist, detaillist, retdatelist);

                    tran.Commit();
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

        /// <summary>
        /// 修正処理
        /// </summary>
        /// <param name="listmanifest">マニフェストリスト</param>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">シーケンス番号</param>
        /// <param name="seqRD">シーケンス番号（T_MANIFEST_RET_DATE）</param>
        /// <param name="manifestKbn">マニフェスト区分</param>
        /// <param name="kofuKbn">交付番号区分</param>
        [Transaction]
        private void Update(List<string[]> listmanifest, String systemId, String seq, String seqRD, bool manifestKbn, Int16 kofuKbn)
        {
            try
            {
                LogUtility.DebugMethodStart(listmanifest, systemId, seq, seqRD, manifestKbn, kofuKbn);
                List<T_MANIFEST_ENTRY> entrylist = new List<T_MANIFEST_ENTRY>();
                List<T_MANIFEST_UPN> upnlist = new List<T_MANIFEST_UPN>();
                List<T_MANIFEST_PRT> prtlist = new List<T_MANIFEST_PRT>();
                List<T_MANIFEST_DETAIL_PRT> detailprtlist = new List<T_MANIFEST_DETAIL_PRT>();
                List<T_MANIFEST_KP_KEIJYOU> keijyoulist = new List<T_MANIFEST_KP_KEIJYOU>();
                List<T_MANIFEST_KP_NISUGATA> niugatalist = new List<T_MANIFEST_KP_NISUGATA>();
                List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist = new List<T_MANIFEST_KP_SBN_HOUHOU>();
                List<T_MANIFEST_DETAIL> detaillist = new List<T_MANIFEST_DETAIL>();
                List<T_MANIFEST_RET_DATE> retdatelist = new List<T_MANIFEST_RET_DATE>();


                //登録データ作成
                this.MakeData(ref entrylist, ref upnlist, ref prtlist, ref detailprtlist, ref keijyoulist, ref niugatalist, ref houhoulist, ref detaillist, ref retdatelist, false, systemId, seq, seqRD, manifestKbn, kofuKbn, listmanifest);

                using (Transaction tran = new Transaction())
                {
                    mlogic.LogicalEntityDel(systemId, seq, timeStampEntry);

                    if (String.IsNullOrEmpty(seqRD) == false)
                    {
                        mlogic.LogicalRetDateDel(systemId, seqRD, timeStampRetDate);
                    }

                    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                    this.UpdateCreateInfo(ref entrylist, ref retdatelist, systemId, seq);
                    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end

                    mlogic.Insert(entrylist, upnlist, prtlist, detailprtlist, keijyoulist, niugatalist, houhoulist, detaillist, retdatelist);

                    tran.Commit();
                }
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Warn(ex);
                throw;
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
        /// 登録者情報の設定
        /// </summary>
        /// <param name="entrylist">T_MANIFEST_ENTRYの登録データ</param>
        /// <param name="retdatelist">T_MANIFEST_RET_DATEの登録データ</param>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">シーケンス番号</param>
        private void UpdateCreateInfo(
            ref List<T_MANIFEST_ENTRY> entrylist,
            ref List<T_MANIFEST_RET_DATE> retdatelist,
            String systemId,
            String seq
            )
        {
            foreach (T_MANIFEST_ENTRY data in entrylist)
            {
                T_MANIFEST_ENTRY entryDto = new T_MANIFEST_ENTRY();
                entryDto.SYSTEM_ID = Convert.ToInt64(systemId);
                entryDto.SEQ = Convert.ToInt32(seq);
                T_MANIFEST_ENTRY retDto = this.manifestEntryDao.GetDataByPrimaryKey(entryDto);
                if (retDto != null)
                {
                    data.CREATE_DATE = retDto.CREATE_DATE;
                    data.CREATE_USER = retDto.CREATE_USER;
                    data.CREATE_PC = retDto.CREATE_PC;
                }
            }

            foreach (T_MANIFEST_RET_DATE data in retdatelist)
            {
                T_MANIFEST_RET_DATE entryDto = new T_MANIFEST_RET_DATE();
                entryDto.SYSTEM_ID = Convert.ToInt64(systemId);
                entryDto.SEQ = Convert.ToInt32(seq);
                T_MANIFEST_RET_DATE retDto = this.manifestRetDateDao.GetDataByPrimaryKey(entryDto);
                if (retDto != null)
                {
                    data.CREATE_DATE = retDto.CREATE_DATE;
                    data.CREATE_USER = retDto.CREATE_USER;
                    data.CREATE_PC = retDto.CREATE_PC;
                }
            }
        }

        #region データ作成処理(マニ)

        /// <summary>
        /// データ作成
        /// </summary>
        /// <param name="entrylist">T_MANIFEST_ENTRYの登録データ</param>
        /// <param name="upnlist">T_MANIFEST_UPNの登録データ</param>
        /// <param name="prtlist">T_MANIFEST_PRTの登録データ</param>
        /// <param name="detailprtlist">T_MANIFEST_DETAIL_PRTの登録データ</param>
        /// <param name="keijyoulist">T_MANIFEST_KP_KEIJYOUの登録データ</param>
        /// <param name="niugatalist">T_MANIFEST_KP_NISUGATAの登録データ</param>
        /// <param name="houhoulist">T_MANIFEST_KP_SBN_HOUHOUの登録データ</param>
        /// <param name="detaillist">T_MANIFEST_DETAILの登録データ</param>
        /// <param name="retdatelist">T_MANIFEST_RET_DATEの登録データ</param>
        /// <param name="delflg">削除フラグ</param>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">シーケンス番号</param>
        /// <param name="seqRD">シーケンス番号（T_MANIFEST_RET_DATE）</param>
        /// <param name="manifestKbn">マニフェスト区分</param>
        /// <param name="kofuKbn">交付番号区分</param>
        /// <param name="listdenpyou">CSVデータ</param>
        private Boolean MakeData(
            ref List<T_MANIFEST_ENTRY> entrylist,
            ref List<T_MANIFEST_UPN> upnlist,
            ref List<T_MANIFEST_PRT> prtlist,
            ref List<T_MANIFEST_DETAIL_PRT> detailprtlist,
            ref List<T_MANIFEST_KP_KEIJYOU> keijyoulist,
            ref List<T_MANIFEST_KP_NISUGATA> niugatalist,
            ref List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist,
            ref List<T_MANIFEST_DETAIL> detaillist,
            ref List<T_MANIFEST_RET_DATE> retdatelist,
            bool delflg,
            String systemId,
            String seq,
            String seqRD,
             bool manifestKbn,
            Int16 kofuKbn,
            List<string[]> listdenpyou
            )
        {
            LogUtility.DebugMethodStart(entrylist, upnlist, prtlist, detailprtlist, keijyoulist, niugatalist, houhoulist, detaillist, retdatelist, delflg, systemId, seq, seqRD, manifestKbn, kofuKbn, listdenpyou);

            //システムID(全般･マニ返却日)
            long lSysId = 0;

            //枝番(全般)
            int iSeq = 0;

            //枝番(マニ返却日)
            int iSeqRD = 0;

            //枝番(マニ返却日)
            long iDsysID = 0;

            string haikiKbn = this.form.txtHaikiKbn.Text;

            if (!string.IsNullOrEmpty(systemId))
            {
                lSysId = long.Parse(systemId);
                iSeq = int.Parse(seq) + 1;
                if (!string.IsNullOrWhiteSpace(seqRD))
                {
                    iSeqRD = int.Parse(seqRD) + 1;
                }
                // 廃棄物区分
                switch (haikiKbn)
                {
                    case UIConstans.MANI_SBT_CHOKKOU:
                        delflg = ToBoolean(listdenpyou[0][93]);
                        break;
                    case UIConstans.MANI_SBT_TUMIKAE:
                        delflg = ToBoolean(listdenpyou[0][158]);
                        break;
                    case UIConstans.MANI_SBT_KENPAI:
                        delflg = ToBoolean(listdenpyou[0][154]);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Common.BusinessCommon.DBAccessor dba = null;
                dba = new Common.BusinessCommon.DBAccessor();
                lSysId = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);
                iSeq = 1;
                iSeqRD = 1;
            }

            totalSuu = 0;
            totalKansanSuu = 0;
            totalGennyouSuu = 0;
            //マニ明細(T_MANIFEST_DETAIL)データ作成
            detaillist = new List<T_MANIFEST_DETAIL>();
            MakeManifestDetailList(lSysId, iSeq, iDsysID, manifestKbn, haikiKbn, ref detaillist, listdenpyou);

            //マニフェスト(T_MANIFEST_ENTRY)データ作成
            entrylist = new List<T_MANIFEST_ENTRY>();
            var maniEntry = new T_MANIFEST_ENTRY();
            MakeManifestEntry(lSysId, iSeq, manifestKbn, haikiKbn, kofuKbn, ref maniEntry, listdenpyou[0], delflg);
            entrylist.Add(maniEntry);

            //マニ収集運搬(T_MANIFEST_UPN)データ作成
            upnlist = new List<T_MANIFEST_UPN>();
            T_MANIFEST_UPN maniUpn = null;
            maniUpn = new T_MANIFEST_UPN();
            MakeManifestUpn(lSysId, iSeq, 1, haikiKbn, ref maniUpn, listdenpyou[0]);
            upnlist.Add(maniUpn);

            if (haikiKbn == UIConstans.MANI_SBT_TUMIKAE || haikiKbn == UIConstans.MANI_SBT_KENPAI)
            {
                maniUpn = new T_MANIFEST_UPN();
                MakeManifestUpn(lSysId, iSeq, 2, haikiKbn, ref maniUpn, listdenpyou[0]);
                upnlist.Add(maniUpn);
                if (haikiKbn == UIConstans.MANI_SBT_TUMIKAE)
                {
                    maniUpn = new T_MANIFEST_UPN();
                    MakeManifestUpn(lSysId, iSeq, 3, haikiKbn, ref maniUpn, listdenpyou[0]);
                    upnlist.Add(maniUpn);
                }
            }

            //マニ印字(T_MANIFEST_PRT)データ作成
            prtlist = new List<T_MANIFEST_PRT>();
            T_MANIFEST_PRT maniPrt = new T_MANIFEST_PRT();
            MakeManifestPrt(lSysId, iSeq, haikiKbn, ref maniPrt, listdenpyou[0]);
            prtlist.Add(maniPrt);

            if (!string.IsNullOrEmpty(systemId))
            {
                //マニ印字明細(T_MANIFEST_DETAIL_PRT)データ作成
                detailprtlist = new List<T_MANIFEST_DETAIL_PRT>();
                MakeManifestDetailPrtList(lSysId, iSeq, haikiKbn, ref detailprtlist);
            }

            //マニ返却日(T_MANIFEST_RET_DATE)データ作成
            retdatelist = new List<T_MANIFEST_RET_DATE>();
            MakeManifestRetDateList(lSysId, iSeqRD, haikiKbn, ref retdatelist, listdenpyou[0], manifestKbn, delflg);

            if (haikiKbn == UIConstans.MANI_SBT_KENPAI)
            {
                //マニ印字_建廃_形状(T_MANIFEST_KP_KEIJYOU)データ作成
                keijyoulist = new List<T_MANIFEST_KP_KEIJYOU>();
                MakeManifestKeijyouList(lSysId, iSeq, ref keijyoulist, listdenpyou[0]);

                //マニ印字_建廃_荷姿(T_MANIFEST_KP_NISUGATA)データ作成
                niugatalist = new List<T_MANIFEST_KP_NISUGATA>();
                MakeManifestNisugataList(lSysId, iSeq, ref niugatalist, listdenpyou[0]);

                //マニ印字_建廃_処分方法(T_MANIFEST_KP_SBN_HOUHOU)データ作成
                houhoulist = new List<T_MANIFEST_KP_SBN_HOUHOU>();
                MakeManifestHouhouList(lSysId, iSeq, ref houhoulist, listdenpyou[0]);
            }

            LogUtility.DebugMethodEnd(entrylist, upnlist, prtlist, prtlist, detaillist, retdatelist, delflg, systemId, seq, seqRD, haikiKbn, listdenpyou);
            return true;
        }

        /// <summary>
        /// マニフェスト(T_MANIFEST_ENTRY)データ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="manifestKbn">マニフェスト区分</param>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="kofuKbn">交付番号区分</param>
        /// <param name="tmp">T_MANIFEST_ENTRYのDTO</param>
        /// <param name="listdenpyou">CSVデータ</param>
        /// <param name="delflg">削除フラグ</param>
        private void MakeManifestEntry(long lSysId, int iSeq, bool manifestKbn, string haikiKbn, Int16 kofuKbn, ref T_MANIFEST_ENTRY tmp, string[] listdenpyou, bool delflg)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, manifestKbn, haikiKbn, kofuKbn, tmp, listdenpyou, delflg);

            // 前回値取得
            T_MANIFEST_ENTRY preEntry = null;
            if (lSysId != 0 && 2 <= iSeq)
            {
                preEntry = GetPreManifestEntry(lSysId, iSeq);
                if (preEntry != null)
                {
                    tmp = preEntry;
                }
            }

            // システムID
            tmp.SYSTEM_ID = lSysId;

            // 枝番
            tmp.SEQ = iSeq;

            // 廃棄物区分CD
            tmp.HAIKI_KBN_CD = Convert.ToInt16(this.form.txtHaikiKbn.Text);
            if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_TUMIKAE))
            {
                tmp.HAIKI_KBN_CD = UIConstans.HAIKI_KBN_TUMIKAE;
            }
            else if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_KENPAI))
            {
                tmp.HAIKI_KBN_CD = UIConstans.HAIKI_KBN_KENPAI;
            }

            if (tmp.HAIKI_KBN_CD == UIConstans.HAIKI_KBN_CHOKKOU && tmp.MANIFEST_MERCURY_CHECK.IsNull)
            {
                // システム設定産廃マニ水銀区分を取得する
                tmp.MANIFEST_MERCURY_CHECK = this.mSysInfo.SANPAI_MANIFEST_MERCURY_CHECK;
            }
            else if (tmp.HAIKI_KBN_CD == UIConstans.HAIKI_KBN_KENPAI && tmp.MANIFEST_MERCURY_CHECK.IsNull)
            {
                // システム設定建廃マニ水銀区分を取得する
                tmp.MANIFEST_MERCURY_CHECK = this.mSysInfo.KENPAI_MANIFEST_MERCURY_CHECK;
            }

            //一次マニフェスト区分
            tmp.FIRST_MANIFEST_KBN = manifestKbn;

            //拠点
            tmp.KYOTEN_CD = Convert.ToInt16(listdenpyou[0]);

            // 取引先CD
            if (preEntry == null)
            {
                tmp.TORIHIKISAKI_CD = null;
            }

            //建廃
            if (haikiKbn == UIConstans.MANI_SBT_KENPAI)
            {
                // 事前協議番号 
                if (!string.IsNullOrEmpty(listdenpyou[10]))
                {
                    tmp.JIZEN_NUMBER = Convert.ToString(listdenpyou[10]);
                }

                // 事前協議年月日
                if (!string.IsNullOrEmpty(listdenpyou[11]))
                {
                    tmp.JIZEN_DATE = Convert.ToDateTime(listdenpyou[11]);
                }
            }

            // 交付番号区分
            tmp.KOUFU_KBN = kofuKbn;
            if (kofuKbn == 0)
            {
                tmp.KOUFU_KBN = Convert.ToInt16(listdenpyou[3]);
            }

            // 交付番号
            tmp.MANIFEST_ID = Convert.ToString(listdenpyou[4]).ToUpper();

            // 交付年月日
            if (!string.IsNullOrEmpty(listdenpyou[6]))
            {
                tmp.KOUFU_DATE = Convert.ToDateTime(listdenpyou[6]);
            }

            // 整理番号
            if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[7])))
            {
                tmp.SEIRI_ID = Convert.ToString(listdenpyou[7]);
            }

            // 直行、積替
            if (haikiKbn == UIConstans.MANI_SBT_CHOKKOU || haikiKbn == UIConstans.MANI_SBT_TUMIKAE)
            {
                // 交付担当者
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[8])))
                {
                    tmp.KOUFU_TANTOUSHA = Convert.ToString(listdenpyou[8]);
                }

                // 排出事業者CD
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[9])))
                {
                    tmp.HST_GYOUSHA_CD = Convert.ToString(listdenpyou[9]).PadLeft(6, '0').ToUpper();
                }
                else
                {
                    if (preEntry == null)
                    {
                        tmp.HST_GYOUSHA_CD = String.Empty;
                    }
                }

                // 排出事業場CD
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[14])))
                {
                    tmp.HST_GENBA_CD = Convert.ToString(listdenpyou[14]).PadLeft(6, '0').ToUpper();
                }
                else
                {
                    if (preEntry == null)
                    {
                        tmp.HST_GENBA_CD = String.Empty;
                    }
                }
            }
            //建廃
            else if (haikiKbn == UIConstans.MANI_SBT_KENPAI)
            {
                //交付担当者所属
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[8])))
                {
                    tmp.KOUFU_TANTOUSHA_SHOZOKU = Convert.ToString(listdenpyou[8]);
                }
                // 交付担当者
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[9])))
                {
                    tmp.KOUFU_TANTOUSHA = Convert.ToString(listdenpyou[9]);
                }
                // 排出事業者CD
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[12])))
                {
                    tmp.HST_GYOUSHA_CD = Convert.ToString(listdenpyou[12]).PadLeft(6, '0').ToUpper();
                }
                else
                {
                    if (preEntry == null)
                    {
                        tmp.HST_GYOUSHA_CD = String.Empty;
                    }
                }
                // 排出事業場CD
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[17])))
                {
                    tmp.HST_GENBA_CD = Convert.ToString(listdenpyou[17]).PadLeft(6, '0').ToUpper();
                }
                else
                {
                    if (preEntry == null)
                    {
                        tmp.HST_GENBA_CD = String.Empty;
                    }
                }
            }

            // 排出事業者名称
            M_GYOUSHA gyoushaEntity = null;
            if (!string.IsNullOrEmpty(tmp.HST_GYOUSHA_CD))
            {
                gyoushaEntity = this.GetGyousha(tmp.HST_GYOUSHA_CD);
            }
            tmp.HST_GYOUSHA_NAME = string.Empty;
            if (gyoushaEntity != null)
            {
                if (!string.IsNullOrEmpty(gyoushaEntity.GYOUSHA_NAME2))
                {
                    tmp.HST_GYOUSHA_NAME = gyoushaEntity.GYOUSHA_NAME1.PadRight(40, ' ') + gyoushaEntity.GYOUSHA_NAME2;
                }
                else
                {
                    tmp.HST_GYOUSHA_NAME = gyoushaEntity.GYOUSHA_NAME1;
                }
            }
            // 排出事業者郵便番号
            if (gyoushaEntity != null && !string.IsNullOrEmpty(gyoushaEntity.GYOUSHA_POST))
            {
                tmp.HST_GYOUSHA_POST = gyoushaEntity.GYOUSHA_POST;
            }
            else
            {
                tmp.HST_GYOUSHA_POST = String.Empty;
            }

            // 排出事業者電話番号
            if (gyoushaEntity != null && !string.IsNullOrEmpty(gyoushaEntity.GYOUSHA_TEL))
            {
                tmp.HST_GYOUSHA_TEL = gyoushaEntity.GYOUSHA_TEL;
            }
            else
            {
                tmp.HST_GYOUSHA_TEL = String.Empty;
            }

            // 排出事業者住所
            if (gyoushaEntity != null && (!string.IsNullOrEmpty(gyoushaEntity.GYOUSHA_ADDRESS1) ||
                                            !string.IsNullOrEmpty(gyoushaEntity.GYOUSHA_ADDRESS2)))
            {
                string todofukenname = string.Empty;
                if (!gyoushaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    // 都道府県CDが取得できる場合
                    todofukenname = this.GetTodofukenName(gyoushaEntity.GYOUSHA_TODOUFUKEN_CD.ToString());
                }
                if (!string.IsNullOrEmpty(gyoushaEntity.GYOUSHA_ADDRESS2))
                {
                    tmp.HST_GYOUSHA_ADDRESS = (todofukenname + gyoushaEntity.GYOUSHA_ADDRESS1).PadRight(48, ' ') + gyoushaEntity.GYOUSHA_ADDRESS2;
                }
                else
                {
                    tmp.HST_GYOUSHA_ADDRESS = todofukenname + gyoushaEntity.GYOUSHA_ADDRESS1;
                }
            }
            else
            {
                tmp.HST_GYOUSHA_ADDRESS = String.Empty;
            }

            // 排出事業場名称
            M_GENBA genbaEntity = null;
            if (!string.IsNullOrEmpty(tmp.HST_GYOUSHA_CD) && !string.IsNullOrEmpty(tmp.HST_GENBA_CD))
            {
                genbaEntity = this.GetGenba(tmp.HST_GYOUSHA_CD, tmp.HST_GENBA_CD);
            }
            tmp.HST_GENBA_NAME = string.Empty;
            if (genbaEntity != null)
            {
                if (!string.IsNullOrEmpty(genbaEntity.GENBA_NAME2))
                {
                    tmp.HST_GENBA_NAME = genbaEntity.GENBA_NAME1.PadRight(40, ' ') + genbaEntity.GENBA_NAME2;
                }
                else
                {
                    tmp.HST_GENBA_NAME = genbaEntity.GENBA_NAME1;
                }
            }

            // 排出事業場郵便番号
            if (genbaEntity != null && !string.IsNullOrEmpty(genbaEntity.GENBA_POST))
            {
                tmp.HST_GENBA_POST = genbaEntity.GENBA_POST;
            }
            else
            {
                tmp.HST_GENBA_POST = String.Empty;
            }

            // 排出事業場電話番号
            if (genbaEntity != null && !string.IsNullOrEmpty(genbaEntity.GENBA_TEL))
            {
                tmp.HST_GENBA_TEL = genbaEntity.GENBA_TEL;
            }
            else
            {
                tmp.HST_GENBA_TEL = String.Empty;
            }

            // 排出事業場住所
            if (genbaEntity != null && (!string.IsNullOrEmpty(genbaEntity.GENBA_ADDRESS1) ||
                                            !string.IsNullOrEmpty(genbaEntity.GENBA_ADDRESS2)))
            {
                string todofukenname = string.Empty;
                if (!genbaEntity.GENBA_TODOUFUKEN_CD.IsNull)
                {
                    // 都道府県CDが取得できる場合
                    todofukenname = this.GetTodofukenName(genbaEntity.GENBA_TODOUFUKEN_CD.ToString());
                }
                if (!string.IsNullOrEmpty(genbaEntity.GENBA_ADDRESS2))
                {
                    tmp.HST_GENBA_ADDRESS = (todofukenname + genbaEntity.GENBA_ADDRESS1).PadRight(48, ' ') + genbaEntity.GENBA_ADDRESS2;
                }
                else
                {
                    tmp.HST_GENBA_ADDRESS = todofukenname + genbaEntity.GENBA_ADDRESS1;
                }
            }
            else
            {
                tmp.HST_GENBA_ADDRESS = String.Empty;
            }

            // 備考
            switch (haikiKbn)
            {
                case UIConstans.MANI_SBT_CHOKKOU:
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[59])))
                    {
                        tmp.BIKOU = Convert.ToString(listdenpyou[59]);
                    }
                    break;
                case UIConstans.MANI_SBT_TUMIKAE:
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[131])))
                    {
                        tmp.BIKOU = Convert.ToString(listdenpyou[131]);
                    }
                    break;
                case UIConstans.MANI_SBT_KENPAI:
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[106])))
                    {
                        tmp.BIKOU = Convert.ToString(listdenpyou[106]);
                    }
                    break;
                default:
                    break;
            }

            // 混合種類
            tmp.KONGOU_SHURUI_CD = string.Empty;

            // 実績数量
            tmp.HAIKI_SUU = SqlDecimal.Null;

            // 実績単位CD
            tmp.HAIKI_UNIT_CD = SqlInt16.Null;

            // 数量の合計
            tmp.TOTAL_SUU = totalSuu;

            // 換算後数量の合計
            tmp.TOTAL_KANSAN_SUU = totalKansanSuu;

            // 減容後数量の合計
            tmp.TOTAL_GENNYOU_SUU = totalGennyouSuu;

            int lastSbnYoteiKbn = 0;
            // 直行、積替
            if (haikiKbn == UIConstans.MANI_SBT_CHOKKOU || haikiKbn == UIConstans.MANI_SBT_TUMIKAE)
            {
                // 中間処理産業廃棄物区分
                int chuukanHaikiKbn = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[19])))
                {
                    if (int.TryParse(Convert.ToString(listdenpyou[19]), out chuukanHaikiKbn))
                    {
                        tmp.CHUUKAN_HAIKI_KBN = Convert.ToInt16(listdenpyou[19]);
                    }
                }

                // 中間処理産業廃棄物
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[20])))
                {
                    tmp.CHUUKAN_HAIKI = Convert.ToString(listdenpyou[20]);
                }

                // 最終処分の場所（予定）区分
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[21])))
                {
                    if (int.TryParse(Convert.ToString(listdenpyou[21]), out lastSbnYoteiKbn))
                    {
                        tmp.LAST_SBN_YOTEI_KBN = Convert.ToInt16(listdenpyou[21]);
                    }
                }
                else
                {
                    if (preEntry == null)
                    {
                        tmp.LAST_SBN_YOTEI_KBN = Convert.ToInt16(lastSbnYoteiKbn);
                    }
                }

                // 最終処分の場所（予定）業者CD
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[22])))
                {
                    tmp.LAST_SBN_YOTEI_GYOUSHA_CD = Convert.ToString(listdenpyou[22]).PadLeft(6, '0').ToUpper();
                }
                else
                {
                    if (preEntry == null)
                    {
                        tmp.LAST_SBN_YOTEI_GYOUSHA_CD = String.Empty;
                    }
                }

                // 最終処分の場所（予定）現場CD
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[23])))
                {
                    tmp.LAST_SBN_YOTEI_GENBA_CD = Convert.ToString(listdenpyou[23]).PadLeft(6, '0').ToUpper();
                }
                else
                {
                    if (preEntry == null)
                    {
                        tmp.LAST_SBN_YOTEI_GENBA_CD = String.Empty;
                    }
                }
            }
            //建廃
            else if (haikiKbn == UIConstans.MANI_SBT_KENPAI)
            {
                // 中間処理産業廃棄物区分
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[22])))
                {
                    tmp.CHUUKAN_HAIKI_KBN = Convert.ToInt16(listdenpyou[22]);
                }

                // 中間処理産業廃棄物
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[23])))
                {
                    tmp.CHUUKAN_HAIKI = Convert.ToString(listdenpyou[23]);
                }

                // 最終処分の場所（予定）区分
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[24])))
                {
                    tmp.LAST_SBN_YOTEI_KBN = Convert.ToInt16(listdenpyou[24]);
                }
                else
                {
                    if (preEntry == null)
                    {
                        tmp.LAST_SBN_YOTEI_KBN = Convert.ToInt16(lastSbnYoteiKbn);
                    }
                }

                // 最終処分の場所（予定）業者CD
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[25])))
                {
                    tmp.LAST_SBN_YOTEI_GYOUSHA_CD = Convert.ToString(listdenpyou[25]).PadLeft(6, '0').ToUpper();
                }
                else
                {
                    if (preEntry == null)
                    {
                        tmp.LAST_SBN_YOTEI_GYOUSHA_CD = String.Empty;
                    }
                }

                // 最終処分の場所（予定）現場CD
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[26])))
                {
                    tmp.LAST_SBN_YOTEI_GENBA_CD = Convert.ToString(listdenpyou[26]).PadLeft(6, '0').ToUpper();
                }
                else
                {
                    if (preEntry == null)
                    {
                        tmp.LAST_SBN_YOTEI_GENBA_CD = String.Empty;
                    }
                }
            }

            M_GENBA lastgenbaEntity = null;
            tmp.LAST_SBN_YOTEI_GENBA_NAME = string.Empty;
            if (!string.IsNullOrEmpty(tmp.LAST_SBN_YOTEI_GYOUSHA_CD) && !string.IsNullOrEmpty(tmp.LAST_SBN_YOTEI_GENBA_CD))
            {
                lastgenbaEntity = this.GetGenba(tmp.LAST_SBN_YOTEI_GYOUSHA_CD, tmp.LAST_SBN_YOTEI_GENBA_CD);
            }
            // 最終処分の場所（予定）現場名称
            if (lastgenbaEntity != null)
            {
                if (!string.IsNullOrEmpty(lastgenbaEntity.GENBA_NAME2))
                {
                    tmp.LAST_SBN_YOTEI_GENBA_NAME = lastgenbaEntity.GENBA_NAME1 + lastgenbaEntity.GENBA_NAME2;
                }
                else
                {
                    tmp.LAST_SBN_YOTEI_GENBA_NAME = lastgenbaEntity.GENBA_NAME1;
                }
            }
            // 最終処分の場所（予定）郵便番号
            if (lastgenbaEntity != null && !string.IsNullOrEmpty(lastgenbaEntity.GENBA_POST) && haikiKbn != UIConstans.MANI_SBT_KENPAI)
            {
                tmp.LAST_SBN_YOTEI_GENBA_POST = lastgenbaEntity.GENBA_POST;
            }
            else
            {
                if (haikiKbn != UIConstans.MANI_SBT_KENPAI)
                {
                    tmp.LAST_SBN_YOTEI_GENBA_POST = String.Empty;
                }
            }

            // 最終処分の場所（予定）電話番号
            if (lastgenbaEntity != null && !string.IsNullOrEmpty(lastgenbaEntity.GENBA_TEL) && haikiKbn != UIConstans.MANI_SBT_KENPAI)
            {
                tmp.LAST_SBN_YOTEI_GENBA_TEL = lastgenbaEntity.GENBA_TEL;
            }
            else
            {
                if (haikiKbn != UIConstans.MANI_SBT_KENPAI)
                {
                    tmp.LAST_SBN_YOTEI_GENBA_TEL = String.Empty;
                }
            }

            // 最終処分の場所（予定）住所 
            if (lastgenbaEntity != null && (!string.IsNullOrEmpty(lastgenbaEntity.GENBA_ADDRESS1) ||
                                            !string.IsNullOrEmpty(lastgenbaEntity.GENBA_ADDRESS2)))
            {
                string todofukenname = string.Empty;
                if (!lastgenbaEntity.GENBA_TODOUFUKEN_CD.IsNull)
                {
                    // 都道府県CDが取得できる場合
                    todofukenname = this.GetTodofukenName(lastgenbaEntity.GENBA_TODOUFUKEN_CD.ToString());
                }
                if (!string.IsNullOrEmpty(lastgenbaEntity.GENBA_ADDRESS2))
                {
                    tmp.LAST_SBN_YOTEI_GENBA_ADDRESS = (todofukenname + lastgenbaEntity.GENBA_ADDRESS1) + lastgenbaEntity.GENBA_ADDRESS2;
                }
                else
                {
                    tmp.LAST_SBN_YOTEI_GENBA_ADDRESS = todofukenname + lastgenbaEntity.GENBA_ADDRESS1;
                }
            }
            else
            {
                tmp.LAST_SBN_YOTEI_GENBA_ADDRESS = String.Empty;
            }

            switch (haikiKbn)
            {
                case UIConstans.MANI_SBT_CHOKKOU:
                    // 処分受託者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[45])))
                    {
                        tmp.SBN_GYOUSHA_CD = Convert.ToString(listdenpyou[45]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SBN_GYOUSHA_CD = String.Empty;
                        }
                    }
                    // 積替保管業者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[50])))
                    {
                        tmp.TMH_GYOUSHA_CD = Convert.ToString(listdenpyou[50]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.TMH_GYOUSHA_CD = String.Empty;
                        }
                    }
                    // 積替保管場CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[52])))
                    {
                        tmp.TMH_GENBA_CD = Convert.ToString(listdenpyou[52]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.TMH_GENBA_CD = String.Empty;
                        }
                    }
                    break;
                case UIConstans.MANI_SBT_TUMIKAE:
                    // 処分受託者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[82])))
                    {
                        tmp.SBN_GYOUSHA_CD = Convert.ToString(listdenpyou[82]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SBN_GYOUSHA_CD = String.Empty;
                        }
                    }
                    // 積替保管業者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[87])))
                    {
                        tmp.TMH_GYOUSHA_CD = Convert.ToString(listdenpyou[87]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.TMH_GYOUSHA_CD = String.Empty;
                        }
                    }
                    // 積替保管場CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[89])))
                    {
                        tmp.TMH_GENBA_CD = Convert.ToString(listdenpyou[89]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.TMH_GENBA_CD = String.Empty;
                        }
                    }
                    break;
                case UIConstans.MANI_SBT_KENPAI:
                    // 処分受託者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[72])))
                    {
                        tmp.SBN_GYOUSHA_CD = Convert.ToString(listdenpyou[72]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SBN_GYOUSHA_CD = String.Empty;
                        }
                    }
                    // 積替保管業者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[82])))
                    {
                        tmp.TMH_GYOUSHA_CD = Convert.ToString(listdenpyou[82]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.TMH_GYOUSHA_CD = String.Empty;
                        }
                    }
                    // 積替保管場CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[83])))
                    {
                        tmp.TMH_GENBA_CD = Convert.ToString(listdenpyou[83]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.TMH_GENBA_CD = String.Empty;
                        }
                    }
                    break;
                default:
                    break;
            }

            // 処分受託者名称
            M_GYOUSHA shobungyoushaEntity = null;
            if (!string.IsNullOrEmpty(tmp.SBN_GYOUSHA_CD))
            {
                shobungyoushaEntity = this.GetGyousha(tmp.SBN_GYOUSHA_CD);
            }
            tmp.SBN_GYOUSHA_NAME = string.Empty;
            if (shobungyoushaEntity != null)
            {
                if (!string.IsNullOrEmpty(shobungyoushaEntity.GYOUSHA_NAME2))
                {
                    tmp.SBN_GYOUSHA_NAME = shobungyoushaEntity.GYOUSHA_NAME1 + shobungyoushaEntity.GYOUSHA_NAME2;
                }
                else
                {
                    tmp.SBN_GYOUSHA_NAME = shobungyoushaEntity.GYOUSHA_NAME1;
                }
            }
            // 処分受託者郵便番号
            if (shobungyoushaEntity != null && !string.IsNullOrEmpty(shobungyoushaEntity.GYOUSHA_POST))
            {
                tmp.SBN_GYOUSHA_POST = shobungyoushaEntity.GYOUSHA_POST;
            }
            else
            {
                tmp.SBN_GYOUSHA_POST = String.Empty;
            }

            // 処分受託者電話番号
            if (shobungyoushaEntity != null && !string.IsNullOrEmpty(shobungyoushaEntity.GYOUSHA_TEL))
            {
                tmp.SBN_GYOUSHA_TEL = shobungyoushaEntity.GYOUSHA_TEL;
            }
            else
            {
                tmp.SBN_GYOUSHA_TEL = String.Empty;
            }

            // 処分受託者住所
            if (shobungyoushaEntity != null && (!string.IsNullOrEmpty(shobungyoushaEntity.GYOUSHA_ADDRESS1) ||
                                            !string.IsNullOrEmpty(shobungyoushaEntity.GYOUSHA_ADDRESS2)))
            {
                string todofukenname = string.Empty;
                if (!shobungyoushaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    // 都道府県CDが取得できる場合
                    todofukenname = this.GetTodofukenName(shobungyoushaEntity.GYOUSHA_TODOUFUKEN_CD.ToString());
                }
                if (!string.IsNullOrEmpty(shobungyoushaEntity.GYOUSHA_ADDRESS2))
                {
                    tmp.SBN_GYOUSHA_ADDRESS = (todofukenname + shobungyoushaEntity.GYOUSHA_ADDRESS1) + shobungyoushaEntity.GYOUSHA_ADDRESS2;
                }
                else
                {
                    tmp.SBN_GYOUSHA_ADDRESS = todofukenname + shobungyoushaEntity.GYOUSHA_ADDRESS1;
                }
            }
            else
            {
                tmp.SBN_GYOUSHA_ADDRESS = String.Empty;
            }

            // 積替保管業者名称
            M_GYOUSHA tumikaegyoushaEntity = null;
            if (!string.IsNullOrEmpty(tmp.TMH_GYOUSHA_CD))
            {
                tumikaegyoushaEntity = this.GetGyousha(tmp.TMH_GYOUSHA_CD);
            }
            tmp.TMH_GYOUSHA_NAME = string.Empty;
            if (tumikaegyoushaEntity != null)
            {
                if (!string.IsNullOrEmpty(tumikaegyoushaEntity.GYOUSHA_NAME2))
                {
                    tmp.TMH_GYOUSHA_NAME = tumikaegyoushaEntity.GYOUSHA_NAME1 + tumikaegyoushaEntity.GYOUSHA_NAME2;
                }
                else
                {
                    tmp.TMH_GYOUSHA_NAME = tumikaegyoushaEntity.GYOUSHA_NAME1;
                }
            }

            // 積替保管場名称
            M_GENBA tumikaegenbaEntity = null;
            if (!string.IsNullOrEmpty(tmp.TMH_GYOUSHA_CD) && !string.IsNullOrEmpty(tmp.TMH_GENBA_CD))
            {
                tumikaegenbaEntity = this.GetGenba(tmp.TMH_GYOUSHA_CD, tmp.TMH_GENBA_CD);
            }
            tmp.TMH_GENBA_NAME = string.Empty;
            if (tumikaegenbaEntity != null)
            {
                if (!string.IsNullOrEmpty(tumikaegenbaEntity.GENBA_NAME2))
                {
                    tmp.TMH_GENBA_NAME = tumikaegenbaEntity.GENBA_NAME1 + tumikaegenbaEntity.GENBA_NAME2;
                }
                else
                {
                    tmp.TMH_GENBA_NAME = tumikaegenbaEntity.GENBA_NAME1;
                }
            }

            // 積替保管場郵便番号
            if (tumikaegenbaEntity != null && !string.IsNullOrEmpty(tumikaegenbaEntity.GENBA_POST))
            {
                tmp.TMH_GENBA_POST = tumikaegenbaEntity.GENBA_POST;
            }
            else
            {
                tmp.TMH_GENBA_POST = String.Empty;
            }

            // 積替保管場電話番号
            if (tumikaegenbaEntity != null && !string.IsNullOrEmpty(tumikaegenbaEntity.GENBA_TEL))
            {
                tmp.TMH_GENBA_TEL = tumikaegenbaEntity.GENBA_TEL;
            }
            else
            {
                tmp.TMH_GENBA_TEL = String.Empty;
            }

            // 積替保管場住所
            if (tumikaegenbaEntity != null && (!string.IsNullOrEmpty(tumikaegenbaEntity.GENBA_ADDRESS1) ||
                                            !string.IsNullOrEmpty(tumikaegenbaEntity.GENBA_ADDRESS2)))
            {
                string todofukenname = string.Empty;
                if (!tumikaegenbaEntity.GENBA_TODOUFUKEN_CD.IsNull)
                {
                    // 都道府県CDが取得できる場合
                    todofukenname = this.GetTodofukenName(tumikaegenbaEntity.GENBA_TODOUFUKEN_CD.ToString());
                }
                if (!string.IsNullOrEmpty(tumikaegenbaEntity.GENBA_ADDRESS2))
                {
                    tmp.TMH_GENBA_ADDRESS = (todofukenname + tumikaegenbaEntity.GENBA_ADDRESS1) + tumikaegenbaEntity.GENBA_ADDRESS2;
                }
                else
                {
                    tmp.TMH_GENBA_ADDRESS = todofukenname + tumikaegenbaEntity.GENBA_ADDRESS1;
                }
            }
            else
            {
                tmp.TMH_GENBA_ADDRESS = String.Empty;
            }

            // 廃棄物区分
            switch (haikiKbn)
            {
                case UIConstans.MANI_SBT_CHOKKOU:
                    // 有価物拾集量
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[65])))
                    {
                        tmp.YUUKA_SUU = Convert.ToDecimal(listdenpyou[65]);
                    }
                    //有価物拾集量単位CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[66])))
                    {
                        tmp.YUUKA_UNIT_CD = Convert.ToInt16(listdenpyou[66]);
                    }
                    // 処分の受託者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[68])))
                    {
                        tmp.SBN_JYUTAKUSHA_CD = Convert.ToString(listdenpyou[68]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SBN_JYUTAKUSHA_CD = String.Empty;
                        }
                    }
                    // 処分担当者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[70])))
                    {
                        tmp.SBN_TANTOU_CD = Convert.ToString(listdenpyou[70]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SBN_TANTOU_CD = String.Empty;
                        }
                    }
                    // 処分担当者名
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[71])))
                    {
                        tmp.SBN_TANTOU_NAME = Convert.ToString(listdenpyou[71]);
                    }
                    // 最終処分業者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[72])))
                    {
                        tmp.LAST_SBN_GYOUSHA_CD = Convert.ToString(listdenpyou[72]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.LAST_SBN_GYOUSHA_CD = String.Empty;
                        }
                    }
                    // 最終処分場CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[73])))
                    {
                        tmp.LAST_SBN_GENBA_CD = Convert.ToString(listdenpyou[73]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.LAST_SBN_GENBA_CD = String.Empty;
                        }
                    }
                    break;
                case UIConstans.MANI_SBT_TUMIKAE:
                    // 処分の受託者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[120])))
                    {
                        tmp.SBN_JYUTAKUSHA_CD = Convert.ToString(listdenpyou[120]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SBN_JYUTAKUSHA_CD = String.Empty;
                        }
                    }
                    // 処分担当者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[122])))
                    {
                        tmp.SBN_TANTOU_CD = Convert.ToString(listdenpyou[122]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SBN_TANTOU_CD = String.Empty;
                        }
                    }
                    // 処分担当者名
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[123])))
                    {
                        tmp.SBN_TANTOU_NAME = Convert.ToString(listdenpyou[123]);
                    }
                    // 最終処分業者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[124])))
                    {
                        tmp.LAST_SBN_GYOUSHA_CD = Convert.ToString(listdenpyou[124]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.LAST_SBN_GYOUSHA_CD = String.Empty;
                        }
                    }
                    // 最終処分場CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[125])))
                    {
                        tmp.LAST_SBN_GENBA_CD = Convert.ToString(listdenpyou[125]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.LAST_SBN_GENBA_CD = String.Empty;
                        }
                    }
                    break;
                case UIConstans.MANI_SBT_KENPAI:
                    // 有価物拾集有無
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[87])))
                    {
                        tmp.YUUKA_KBN = Convert.ToInt16(listdenpyou[87]);
                    }

                    // 有価物拾集量
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[88])))
                    {
                        tmp.YUUKA_SUU = Convert.ToDecimal(listdenpyou[88]);
                    }

                    //有価物拾集量単位CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[89])))
                    {
                        tmp.YUUKA_UNIT_CD = Convert.ToInt16(listdenpyou[89]);
                    }

                    // 処分の受領者CD 
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[117])))
                    {
                        tmp.SBN_JYURYOUSHA_CD = Convert.ToString(listdenpyou[117]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SBN_JYURYOUSHA_CD = String.Empty;
                        }
                    }

                    // 処分の受領者名称 
                    M_GYOUSHA sbngyoushaEntity = null;
                    if (!string.IsNullOrEmpty(tmp.SBN_JYURYOUSHA_CD))
                    {
                        sbngyoushaEntity = this.GetGyousha(tmp.SBN_JYURYOUSHA_CD);
                    }
                    tmp.SBN_JYURYOUSHA_NAME = string.Empty;
                    if (sbngyoushaEntity != null)
                    {
                        if (!string.IsNullOrEmpty(sbngyoushaEntity.GYOUSHA_NAME2))
                        {
                            tmp.SBN_JYURYOUSHA_NAME = sbngyoushaEntity.GYOUSHA_NAME1 + sbngyoushaEntity.GYOUSHA_NAME2;
                        }
                        else
                        {
                            tmp.SBN_JYURYOUSHA_NAME = sbngyoushaEntity.GYOUSHA_NAME1;
                        }
                    }

                    // 処分の受領担当者CD 
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[119])))
                    {
                        tmp.SBN_JYURYOU_TANTOU_CD = Convert.ToString(listdenpyou[119]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SBN_JYURYOU_TANTOU_CD = String.Empty;
                        }
                    }

                    // 処分の受領担当者名
                    if (!string.IsNullOrEmpty(tmp.SBN_JYURYOU_TANTOU_CD))
                    {
                        tmp.SBN_JYURYOU_TANTOU_NAME = this.CheckShobunTantouCd(tmp.SBN_JYURYOU_TANTOU_CD);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[120])))
                        {
                            tmp.SBN_JYURYOU_TANTOU_NAME = Convert.ToString(listdenpyou[120]);
                        }
                    }

                    // 処分受領日  
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[121])))
                    {
                        tmp.SBN_JYURYOU_DATE = Convert.ToDateTime(listdenpyou[121]);
                    }
                    // 処分の受託者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[122])))
                    {
                        tmp.SBN_JYUTAKUSHA_CD = Convert.ToString(listdenpyou[122]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SBN_JYUTAKUSHA_CD = String.Empty;
                        }
                    }
                    // 処分担当者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[124])))
                    {
                        tmp.SBN_TANTOU_CD = Convert.ToString(listdenpyou[124]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SBN_TANTOU_CD = String.Empty;
                        }
                    }
                    // 処分担当者名
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[125])))
                    {
                        tmp.SBN_TANTOU_NAME = Convert.ToString(listdenpyou[125]);
                    }
                    // 最終処分業者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[127])))
                    {
                        tmp.LAST_SBN_GYOUSHA_CD = Convert.ToString(listdenpyou[127]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.LAST_SBN_GYOUSHA_CD = String.Empty;
                        }
                    }
                    // 最終処分場CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[128])))
                    {
                        tmp.LAST_SBN_GENBA_CD = Convert.ToString(listdenpyou[128]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.LAST_SBN_GENBA_CD = String.Empty;
                        }
                    }
                    break;
                default:
                    break;
            }

            // 処分の受託者名称
            M_GYOUSHA jyutakushagyoushaEntity = null;
            if (!string.IsNullOrEmpty(tmp.SBN_JYUTAKUSHA_CD))
            {
                jyutakushagyoushaEntity = this.GetGyousha(tmp.SBN_JYUTAKUSHA_CD);
            }
            tmp.SBN_JYUTAKUSHA_NAME = string.Empty;
            if (jyutakushagyoushaEntity != null)
            {
                if (!string.IsNullOrEmpty(jyutakushagyoushaEntity.GYOUSHA_NAME2))
                {
                    tmp.SBN_JYUTAKUSHA_NAME = jyutakushagyoushaEntity.GYOUSHA_NAME1 + jyutakushagyoushaEntity.GYOUSHA_NAME2;
                }
                else
                {
                    tmp.SBN_JYUTAKUSHA_NAME = jyutakushagyoushaEntity.GYOUSHA_NAME1;
                }
            }
            // 処分担当者名
            if (!string.IsNullOrEmpty(tmp.SBN_TANTOU_CD))
            {
                tmp.SBN_TANTOU_NAME = this.CheckShobunTantouCd(tmp.SBN_TANTOU_CD);
            }

            //  最終処分場名称
            M_GENBA lastsbgenbaEntity = null;
            if (!string.IsNullOrEmpty(tmp.LAST_SBN_GYOUSHA_CD) && !string.IsNullOrEmpty(tmp.LAST_SBN_GENBA_CD))
            {
                lastsbgenbaEntity = this.GetGenba(tmp.LAST_SBN_GYOUSHA_CD, tmp.LAST_SBN_GENBA_CD);
            }
            tmp.LAST_SBN_GENBA_NAME = string.Empty;
            if (lastsbgenbaEntity != null)
            {
                if (!string.IsNullOrEmpty(lastsbgenbaEntity.GENBA_NAME2))
                {
                    tmp.LAST_SBN_GENBA_NAME = lastsbgenbaEntity.GENBA_NAME1 + lastsbgenbaEntity.GENBA_NAME2;
                }
                else
                {
                    tmp.LAST_SBN_GENBA_NAME = lastsbgenbaEntity.GENBA_NAME1;
                }
            }

            //  最終処分場郵便番号
            if (lastsbgenbaEntity != null && !string.IsNullOrEmpty(lastsbgenbaEntity.GENBA_POST))
            {
                tmp.LAST_SBN_GENBA_POST = lastsbgenbaEntity.GENBA_POST;
            }
            else
            {
                tmp.LAST_SBN_GENBA_POST = String.Empty;
            }

            //  最終処分場電話番号
            if (lastsbgenbaEntity != null && !string.IsNullOrEmpty(lastsbgenbaEntity.GENBA_TEL) && haikiKbn != UIConstans.MANI_SBT_KENPAI)
            {
                tmp.LAST_SBN_GENBA_TEL = lastsbgenbaEntity.GENBA_TEL;
            }
            else
            {
                if (haikiKbn != UIConstans.MANI_SBT_KENPAI)
                {
                    tmp.LAST_SBN_GENBA_TEL = String.Empty;
                }
            }

            //  最終処分場住所
            if (lastsbgenbaEntity != null && (!string.IsNullOrEmpty(lastsbgenbaEntity.GENBA_ADDRESS1) ||
                                            !string.IsNullOrEmpty(lastsbgenbaEntity.GENBA_ADDRESS2)))
            {
                string todofukenname = string.Empty;
                if (!lastsbgenbaEntity.GENBA_TODOUFUKEN_CD.IsNull)
                {
                    // 都道府県CDが取得できる場合
                    todofukenname = this.GetTodofukenName(lastsbgenbaEntity.GENBA_TODOUFUKEN_CD.ToString());
                }
                if (!string.IsNullOrEmpty(lastsbgenbaEntity.GENBA_ADDRESS2))
                {
                    tmp.LAST_SBN_GENBA_ADDRESS = (todofukenname + lastsbgenbaEntity.GENBA_ADDRESS1) + lastsbgenbaEntity.GENBA_ADDRESS2;
                }
                else
                {
                    tmp.LAST_SBN_GENBA_ADDRESS = todofukenname + lastsbgenbaEntity.GENBA_ADDRESS1;
                }
            }
            else
            {
                tmp.LAST_SBN_GENBA_ADDRESS = String.Empty;
            }

            // 廃棄物区分
            switch (haikiKbn)
            {
                case UIConstans.MANI_SBT_CHOKKOU:
                    // 最終処分場処分先番号
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[78])))
                    {
                        tmp.LAST_SBN_GENBA_NUMBER = Convert.ToString(listdenpyou[78]);
                    }
                    // 照合確認B2票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[79])))
                    {
                        tmp.CHECK_B2 = Convert.ToDateTime(listdenpyou[79]);
                    }
                    // 照合確認D票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[80])))
                    {
                        tmp.CHECK_D = Convert.ToDateTime(listdenpyou[80]);
                    }
                    // 照合確認E票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[81])))
                    {
                        tmp.CHECK_E = Convert.ToDateTime(listdenpyou[81]);
                    }
                    break;
                case UIConstans.MANI_SBT_TUMIKAE:
                    // 最終処分場処分先番号
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[130])))
                    {
                        tmp.LAST_SBN_GENBA_NUMBER = Convert.ToString(listdenpyou[130]);
                    }
                    // 照合確認B2票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[132])))
                    {
                        tmp.CHECK_B2 = Convert.ToDateTime(listdenpyou[132]);
                    }
                    // 照合確認B4票 
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[133])))
                    {
                        tmp.CHECK_B4 = Convert.ToDateTime(listdenpyou[133]);
                    }
                    // 照合確認B6票 
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[134])))
                    {
                        tmp.CHECK_B6 = Convert.ToDateTime(listdenpyou[134]);
                    }
                    // 照合確認D票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[135])))
                    {
                        tmp.CHECK_D = Convert.ToDateTime(listdenpyou[135]);
                    }
                    // 照合確認E票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[136])))
                    {
                        tmp.CHECK_E = Convert.ToDateTime(listdenpyou[136]);
                    }
                    break;
                case UIConstans.MANI_SBT_KENPAI:
                    // 最終処分場処分先番号
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[132])))
                    {
                        tmp.LAST_SBN_GENBA_NUMBER = Convert.ToString(listdenpyou[132]);
                    }
                    // 最終処分 確認者 
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[126])))
                    {
                        tmp.LAST_SBN_CHECK_NAME = Convert.ToString(listdenpyou[126]);
                    }
                    // 照合確認 B1票  
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[133])))
                    {
                        tmp.CHECK_B1 = Convert.ToDateTime(listdenpyou[133]);
                    }
                    // 照合確認B2票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[134])))
                    {
                        tmp.CHECK_B2 = Convert.ToDateTime(listdenpyou[134]);
                    }
                    // 照合確認D票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[135])))
                    {
                        tmp.CHECK_D = Convert.ToDateTime(listdenpyou[135]);
                    }
                    // 照合確認E票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[136])))
                    {
                        tmp.CHECK_E = Convert.ToDateTime(listdenpyou[136]);
                    }
                    break;
                default:
                    break;
            }

            if (preEntry == null)
            {
                // 連携伝種区分CD
                tmp.RENKEI_DENSHU_KBN_CD = SqlInt16.Null;
                // 連携システムID
                tmp.RENKEI_SYSTEM_ID = SqlInt64.Null;
                // 連携明細システムID
                tmp.RENKEI_MEISAI_SYSTEM_ID = SqlInt64.Null;
                if (haikiKbn != UIConstans.MANI_SBT_CHOKKOU)
                {
                    // 連携システムID
                    tmp.RENKEI_SYSTEM_ID = 0;

                    // 連携明細システムID
                    tmp.RENKEI_MEISAI_SYSTEM_ID = 0;
                }
            }
            var who = new DataBinderLogic<T_MANIFEST_ENTRY>(tmp);
            who.SetSystemProperty(tmp, false);

            // インポート処理の作成者変更
            tmp.CREATE_USER = "MANIINPORT";
            // インポート処理の更新者変更
            tmp.UPDATE_USER = "MANIINPORT";
            tmp.DELETE_FLG = delflg;

            LogUtility.DebugMethodEnd(lSysId, iSeq, manifestKbn, haikiKbn, kofuKbn, tmp, listdenpyou);
        }

        /// <summary>
        /// 更新用前回値のT_MANIFEST_ENTRYを取得
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <returns></returns>
        private T_MANIFEST_ENTRY GetPreManifestEntry(long lSysId, int iSeq)
        {
            if (lSysId == 0 || iSeq <= 1)
            {
                return null;
            }

            T_MANIFEST_ENTRY searchDto = new T_MANIFEST_ENTRY();
            searchDto.SYSTEM_ID = Convert.ToInt64(lSysId);
            searchDto.SEQ = Convert.ToInt32(iSeq - 1);
            // 更新対象の前回値を取得
            T_MANIFEST_ENTRY retDto = this.manifestEntryDao.GetDataByPrimaryKey(searchDto);

            // 指定された項目のみ前回値を設定
            T_MANIFEST_ENTRY entryDto = new T_MANIFEST_ENTRY();
            // 整理番号
            entryDto.SEIRI_ID = retDto.SEIRI_ID;
            // 交付担当者所属
            entryDto.KOUFU_TANTOUSHA_SHOZOKU = retDto.KOUFU_TANTOUSHA_SHOZOKU;
            // 交付担当者
            entryDto.KOUFU_TANTOUSHA = retDto.KOUFU_TANTOUSHA;
            // 事前協議番号
            entryDto.JIZEN_NUMBER = retDto.JIZEN_NUMBER;
            // 事前協議年月日
            entryDto.JIZEN_DATE = retDto.JIZEN_DATE;
            // 排出事業者CD
            entryDto.HST_GYOUSHA_CD = retDto.HST_GYOUSHA_CD;
            // 排出事業場CD
            entryDto.HST_GENBA_CD = retDto.HST_GENBA_CD;
            // 中間処理産業廃棄物フラグ
            entryDto.CHUUKAN_HAIKI_KBN = retDto.CHUUKAN_HAIKI_KBN;
            // 中間処理産業廃棄物
            entryDto.CHUUKAN_HAIKI = retDto.CHUUKAN_HAIKI;
            // 最終処分の場所（予定）フラグ
            entryDto.LAST_SBN_YOTEI_KBN = retDto.LAST_SBN_YOTEI_KBN;
            // 最終処分の場所（予定）業者CD
            entryDto.LAST_SBN_YOTEI_GYOUSHA_CD = retDto.LAST_SBN_YOTEI_GYOUSHA_CD;
            // 最終処分の場所（予定）現場CD
            entryDto.LAST_SBN_YOTEI_GENBA_CD = retDto.LAST_SBN_YOTEI_GENBA_CD;
            // 処分受託者CD
            entryDto.SBN_GYOUSHA_CD = retDto.SBN_GYOUSHA_CD;
            // 積替保管業者CD
            entryDto.TMH_GYOUSHA_CD = retDto.TMH_GYOUSHA_CD;
            // 積替保管現場
            entryDto.TMH_GENBA_CD = retDto.TMH_GENBA_CD;
            // 備考
            entryDto.BIKOU = retDto.BIKOU;
            // 処分(1)の受託者CD
            entryDto.SBN_JYURYOUSHA_CD = retDto.SBN_JYURYOUSHA_CD;
            // 処分(1)担当者CD
            entryDto.SBN_JYURYOU_TANTOU_CD = retDto.SBN_JYURYOU_TANTOU_CD;
            // 処分(1)担当者名称
            entryDto.SBN_JYURYOU_TANTOU_NAME = retDto.SBN_JYURYOU_TANTOU_NAME;
            // 処分の受託者CD
            entryDto.SBN_JYUTAKUSHA_CD = retDto.SBN_JYUTAKUSHA_CD;
            // 処分担当者CD
            entryDto.SBN_TANTOU_CD = retDto.SBN_TANTOU_CD;
            // 処分担当者名
            entryDto.SBN_TANTOU_NAME = retDto.SBN_TANTOU_NAME;
            // 最終処分確認者
            entryDto.LAST_SBN_CHECK_NAME = retDto.LAST_SBN_CHECK_NAME;
            // 最終処分業者CD
            entryDto.LAST_SBN_GYOUSHA_CD = retDto.LAST_SBN_GYOUSHA_CD;
            // 最終処分現場CD
            entryDto.LAST_SBN_GENBA_CD = retDto.LAST_SBN_GENBA_CD;
            // 処分先No
            entryDto.LAST_SBN_GENBA_NUMBER = retDto.LAST_SBN_GENBA_NUMBER;
            // 照合確認B1票
            entryDto.CHECK_B1 = retDto.CHECK_B1;
            // 照合確認B2票
            entryDto.CHECK_B2 = retDto.CHECK_B2;
            // 照合確認B4票
            entryDto.CHECK_B4 = retDto.CHECK_B4;
            // 照合確認B6票
            entryDto.CHECK_B6 = retDto.CHECK_B6;
            // 照合確認D票
            entryDto.CHECK_D = retDto.CHECK_D;
            // 照合確認E票
            entryDto.CHECK_E = retDto.CHECK_E;

            // CSV取込項目外だが、前回値を引継ぐ項目
            // 取引先CD
            entryDto.TORIHIKISAKI_CD = retDto.TORIHIKISAKI_CD;
            // 連携伝種区分CD
            entryDto.RENKEI_DENSHU_KBN_CD = retDto.RENKEI_DENSHU_KBN_CD;
            // 連携システムID
            entryDto.RENKEI_SYSTEM_ID = retDto.RENKEI_SYSTEM_ID;
            // 連携明細システムID
            entryDto.RENKEI_MEISAI_SYSTEM_ID = retDto.RENKEI_MEISAI_SYSTEM_ID;

            // マニフェスト水銀有無
            entryDto.MANIFEST_MERCURY_CHECK = retDto.MANIFEST_MERCURY_CHECK;
            // 水銀使用製品産業廃棄物
            entryDto.MERCURY_USED_SEIHIN_HAIKIBUTU_CHECK = retDto.MERCURY_USED_SEIHIN_HAIKIBUTU_CHECK;
            // 水銀含有ばいじん等
            entryDto.MERCURY_BAIJINNADO_HAIKIBUTU_CHECK = retDto.MERCURY_BAIJINNADO_HAIKIBUTU_CHECK;
            // 石綿含有産業廃棄物
            entryDto.ISIWAKANADO_HAIKIBUTU_CHECK = retDto.ISIWAKANADO_HAIKIBUTU_CHECK;
            // 特定産業廃棄物
            entryDto.TOKUTEI_SANGYOU_HAIKIBUTU_CHECK = retDto.TOKUTEI_SANGYOU_HAIKIBUTU_CHECK;



            return entryDto;
        }

        /// <summary>
        /// マニ収集運搬(T_MANIFEST_UPN)データ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="No">運搬区間</param>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="tmp">T_MANIFEST_UPNのDTO</param>
        /// <param name="listdenpyou">CSVデータ</param>
        private void MakeManifestUpn(long lSysId, int iSeq, int No, string haikiKbn, ref T_MANIFEST_UPN tmp, string[] listdenpyou)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, No, haikiKbn, tmp, listdenpyou);

            // 前回値取得
            T_MANIFEST_UPN preEntry = null;
            if (lSysId != 0 && 2 <= iSeq)
            {
                preEntry = GetPreManifestUpn(lSysId, iSeq, No);
                if (preEntry != null)
                {
                    tmp = preEntry;
                }
            }

            //システムID
            tmp.SYSTEM_ID = lSysId;

            //枝番
            tmp.SEQ = iSeq;

            //運搬区間
            tmp.UPN_ROUTE_NO = Convert.ToInt16(No);

            // 運搬先区分のチェック有無
            bool upnSakiCheckFlg = true;

            // 廃棄物区分
            switch (haikiKbn)
            {
                case UIConstans.MANI_SBT_CHOKKOU:
                    //運搬受託者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[28])))
                    {
                        tmp.UPN_GYOUSHA_CD = Convert.ToString(listdenpyou[28]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.UPN_GYOUSHA_CD = String.Empty;
                        }
                    }

                    //運搬方法CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[33])))
                    {
                        tmp.UPN_HOUHOU_CD = Convert.ToString(listdenpyou[33]).ToUpper();
                    }

                    //車種CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[37])))
                    {
                        tmp.SHASHU_CD = Convert.ToString(listdenpyou[37]).PadLeft(3, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SHASHU_CD = String.Empty;
                        }
                    }

                    //車輌CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[35])))
                    {
                        tmp.SHARYOU_CD = Convert.ToString(listdenpyou[35]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.SHARYOU_CD = String.Empty;
                        }
                    }

                    //車輌名
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[36])))
                    {
                        tmp.SHARYOU_NAME = Convert.ToString(listdenpyou[36]);
                    }

                    //運搬の受託者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[60])))
                    {
                        tmp.UPN_JYUTAKUSHA_CD = Convert.ToString(listdenpyou[60]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.UPN_JYUTAKUSHA_CD = String.Empty;
                        }
                    }

                    //運搬先の事業者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[45])))
                    {
                        tmp.UPN_SAKI_GYOUSHA_CD = Convert.ToString(listdenpyou[45]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.UPN_SAKI_GYOUSHA_CD = String.Empty;
                        }
                    }

                    //運搬先の事業場CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[40])))
                    {
                        tmp.UPN_SAKI_GENBA_CD = Convert.ToString(listdenpyou[40]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.UPN_SAKI_GENBA_CD = String.Empty;
                        }
                    }

                    //運搬終了年月日
                    DateTime datetime = DateTime.Now;
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[64])))
                    {
                        if (DateTime.TryParse(Convert.ToString(listdenpyou[64]), out datetime))
                        {
                            tmp.UPN_END_DATE = Convert.ToDateTime(Convert.ToString(listdenpyou[64]));
                        }
                    }
                    //運転者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[62])))
                    {
                        tmp.UNTENSHA_CD = Convert.ToString(listdenpyou[62]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.UNTENSHA_CD = String.Empty;
                        }
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[63])))
                    {
                        tmp.UNTENSHA_NAME = Convert.ToString(listdenpyou[63]);
                    }
                    break;
                case UIConstans.MANI_SBT_TUMIKAE:
                    if (No == 1)
                    {
                        //運搬受託者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[28])))
                        {
                            tmp.UPN_GYOUSHA_CD = Convert.ToString(listdenpyou[28]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UPN_GYOUSHA_CD = String.Empty;
                            }
                        }

                        //運搬方法CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[37])))
                        {
                            tmp.UPN_HOUHOU_CD = Convert.ToString(listdenpyou[37]).ToUpper();
                        }

                        //車種CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[35])))
                        {
                            tmp.SHASHU_CD = Convert.ToString(listdenpyou[35]).PadLeft(3, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.SHASHU_CD = String.Empty;
                            }
                        }

                        //車輌CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[33])))
                        {
                            tmp.SHARYOU_CD = Convert.ToString(listdenpyou[33]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.SHARYOU_CD = String.Empty;
                            }
                        }

                        //車輌名
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[34])))
                        {
                            tmp.SHARYOU_NAME = Convert.ToString(listdenpyou[34]);
                        }

                        //運搬先区分
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[39])))
                        {
                            tmp.UPN_SAKI_KBN = Convert.ToInt16(listdenpyou[39]);

                            //運搬先の事業者CD
                            if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[40])) && tmp.UPN_SAKI_KBN == 2)
                            {
                                //※(区間2)運搬先区分 = 2(積替保管) の場合運搬先の事業者CD
                                tmp.UPN_SAKI_GYOUSHA_CD = Convert.ToString(listdenpyou[40]).PadLeft(6, '0').ToUpper();
                            }
                            else if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[82])) && tmp.UPN_SAKI_KBN == 1)
                            {
                                //※(区間2)運搬先区分 = 1(処分施設) の場合処分受託者CD.
                                tmp.UPN_SAKI_GYOUSHA_CD = Convert.ToString(listdenpyou[82]).PadLeft(6, '0').ToUpper();
                            }
                            else
                            {
                                if (preEntry == null)
                                {
                                    tmp.UPN_SAKI_GYOUSHA_CD = String.Empty;
                                }
                            }

                            //運搬先の事業場CD
                            if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[41])))
                            {
                                tmp.UPN_SAKI_GENBA_CD = Convert.ToString(listdenpyou[41]).PadLeft(6, '0').ToUpper();
                            }
                            else
                            {
                                if (preEntry == null)
                                {
                                    tmp.UPN_SAKI_GENBA_CD = String.Empty;
                                }
                            }
                        }
                        else
                        {
                            if (preEntry == null || preEntry.UPN_SAKI_KBN.IsNull)
                            {
                                upnSakiCheckFlg = false;
                            }
                        }

                        //運搬の受託者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[96])))
                        {
                            tmp.UPN_JYUTAKUSHA_CD = Convert.ToString(listdenpyou[96]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UPN_JYUTAKUSHA_CD = String.Empty;
                            }
                        }

                        //運転者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[98])))
                        {
                            tmp.UNTENSHA_CD = Convert.ToString(listdenpyou[98]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UNTENSHA_CD = String.Empty;
                            }
                        }

                        //運転者名
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[99])))
                        {
                            tmp.UNTENSHA_NAME = Convert.ToString(listdenpyou[99]);
                        }

                        //運搬終了年月日
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[100])))
                        {
                            tmp.UPN_END_DATE = Convert.ToDateTime(Convert.ToString(listdenpyou[100]));
                        }

                        //有価物拾得量数量
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[101])))
                        {
                            tmp.YUUKA_SUU = Convert.ToDecimal(Convert.ToString(listdenpyou[101]));
                        }

                        //有価物拾得量単位CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[102])))
                        {
                            tmp.YUUKA_UNIT_CD = Convert.ToInt16(Convert.ToString(listdenpyou[102]));
                        }
                    }
                    else if (No == 2)
                    {
                        //運搬受託者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[46])))
                        {
                            tmp.UPN_GYOUSHA_CD = Convert.ToString(listdenpyou[46]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UPN_GYOUSHA_CD = String.Empty;
                            }
                        }

                        //運搬方法CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[55])))
                        {
                            tmp.UPN_HOUHOU_CD = Convert.ToString(listdenpyou[55]).ToUpper();
                        }

                        //車種CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[53])))
                        {
                            tmp.SHASHU_CD = Convert.ToString(listdenpyou[53]).PadLeft(3, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.SHASHU_CD = String.Empty;
                            }
                        }

                        //車輌CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[51])))
                        {
                            tmp.SHARYOU_CD = Convert.ToString(listdenpyou[51]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.SHARYOU_CD = String.Empty;
                            }
                        }

                        //車輌名
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[52])))
                        {
                            tmp.SHARYOU_NAME = Convert.ToString(listdenpyou[52]);
                        }

                        //運搬先区分
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[57])))
                        {
                            tmp.UPN_SAKI_KBN = Convert.ToInt16(listdenpyou[57]);

                            //運搬先の事業者CD
                            if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[58])) && tmp.UPN_SAKI_KBN == 2)
                            {
                                //※(区間2)運搬先区分 = 2(積替保管) の場合運搬先の事業者CD
                                tmp.UPN_SAKI_GYOUSHA_CD = Convert.ToString(listdenpyou[58]).PadLeft(6, '0').ToUpper();
                            }
                            else if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[82])) && tmp.UPN_SAKI_KBN == 1)
                            {
                                //※(区間2)運搬先区分 = 1(処分施設) の場合処分受託者CD.
                                tmp.UPN_SAKI_GYOUSHA_CD = Convert.ToString(listdenpyou[82]).PadLeft(6, '0').ToUpper();
                            }
                            else
                            {
                                if (preEntry == null)
                                {
                                    tmp.UPN_SAKI_GYOUSHA_CD = String.Empty;
                                }
                            }

                            //運搬先の事業場CD
                            if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[59])))
                            {
                                tmp.UPN_SAKI_GENBA_CD = Convert.ToString(listdenpyou[59]).PadLeft(6, '0').ToUpper();
                            }
                            else
                            {
                                if (preEntry == null)
                                {
                                    tmp.UPN_SAKI_GENBA_CD = String.Empty;
                                }
                            }
                        }
                        else
                        {
                            if (preEntry == null || preEntry.UPN_SAKI_KBN.IsNull)
                            {
                                upnSakiCheckFlg = false;
                            }
                        }

                        //運搬の受託者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[104])))
                        {
                            tmp.UPN_JYUTAKUSHA_CD = Convert.ToString(listdenpyou[104]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UPN_JYUTAKUSHA_CD = String.Empty;
                            }
                        }

                        //運転者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[106])))
                        {
                            tmp.UNTENSHA_CD = Convert.ToString(listdenpyou[106]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UNTENSHA_CD = String.Empty;
                            }
                        }

                        //運転者名
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[107])))
                        {
                            tmp.UNTENSHA_NAME = Convert.ToString(listdenpyou[107]);
                        }

                        //運搬終了年月日
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[108])))
                        {
                            tmp.UPN_END_DATE = Convert.ToDateTime(Convert.ToString(listdenpyou[108]));
                        }

                        //有価物拾得量数量
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[109])))
                        {
                            tmp.YUUKA_SUU = Convert.ToDecimal(Convert.ToString(listdenpyou[109]));
                        }

                        //有価物拾得量単位CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[110])))
                        {
                            tmp.YUUKA_UNIT_CD = Convert.ToInt16(Convert.ToString(listdenpyou[110]));
                        }
                    }
                    else if (No == 3)
                    {
                        //運搬受託者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[64])))
                        {
                            tmp.UPN_GYOUSHA_CD = Convert.ToString(listdenpyou[64]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UPN_GYOUSHA_CD = String.Empty;
                            }
                        }

                        //運搬方法CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[73])))
                        {
                            tmp.UPN_HOUHOU_CD = Convert.ToString(listdenpyou[73]).ToUpper();
                        }

                        //車種CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[71])))
                        {
                            tmp.SHASHU_CD = Convert.ToString(listdenpyou[71]).PadLeft(3, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.SHASHU_CD = String.Empty;
                            }
                        }

                        //車輌CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[69])))
                        {
                            tmp.SHARYOU_CD = Convert.ToString(listdenpyou[69]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.SHARYOU_CD = String.Empty;
                            }
                        }

                        //車輌名
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[70])))
                        {
                            tmp.SHARYOU_NAME = Convert.ToString(listdenpyou[70]);
                        }

                        //運搬先区分
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[75])))
                        {
                            tmp.UPN_SAKI_KBN = Convert.ToInt16(listdenpyou[75]);

                            //運搬先の事業者CD
                            if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[76])) && tmp.UPN_SAKI_KBN == 2)
                            {
                                //※(区間2)運搬先区分 = 2(積替保管) の場合運搬先の事業者CD
                                tmp.UPN_SAKI_GYOUSHA_CD = Convert.ToString(listdenpyou[76]).PadLeft(6, '0').ToUpper();
                            }
                            else if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[82])) && tmp.UPN_SAKI_KBN == 1)
                            {
                                //※(区間2)運搬先区分 = 1(処分施設) の場合処分受託者CD.
                                tmp.UPN_SAKI_GYOUSHA_CD = Convert.ToString(listdenpyou[82]).PadLeft(6, '0').ToUpper();
                            }
                            else
                            {
                                if (preEntry == null)
                                {
                                    tmp.UPN_SAKI_GYOUSHA_CD = String.Empty;
                                }
                            }

                            //運搬先の事業場CD
                            if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[77])))
                            {
                                tmp.UPN_SAKI_GENBA_CD = Convert.ToString(listdenpyou[77]).PadLeft(6, '0').ToUpper();
                            }
                            else
                            {
                                if (preEntry == null)
                                {
                                    tmp.UPN_SAKI_GENBA_CD = String.Empty;
                                }
                            }
                        }
                        else
                        {
                            if (preEntry == null || preEntry.UPN_SAKI_KBN.IsNull)
                            {
                                upnSakiCheckFlg = false;
                            }
                        }

                        //運搬の受託者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[112])))
                        {
                            tmp.UPN_JYUTAKUSHA_CD = Convert.ToString(listdenpyou[112]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UPN_JYUTAKUSHA_CD = String.Empty;
                            }
                        }

                        //運転者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[114])))
                        {
                            tmp.UNTENSHA_CD = Convert.ToString(listdenpyou[114]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UNTENSHA_CD = String.Empty;
                            }
                        }

                        //運転者名
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[115])))
                        {
                            tmp.UNTENSHA_NAME = Convert.ToString(listdenpyou[115]);
                        }

                        //運搬終了年月日
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[116])))
                        {
                            tmp.UPN_END_DATE = Convert.ToDateTime(Convert.ToString(listdenpyou[116]));
                        }

                        //有価物拾得量数量
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[117])))
                        {
                            tmp.YUUKA_SUU = Convert.ToDecimal(Convert.ToString(listdenpyou[117]));
                        }

                        //有価物拾得量単位CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[118])))
                        {
                            tmp.YUUKA_UNIT_CD = Convert.ToInt16(Convert.ToString(listdenpyou[118]));
                        }
                    }
                    break;
                case UIConstans.MANI_SBT_KENPAI:
                    if (No == 1)
                    {
                        //運搬受託者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[50])))
                        {
                            tmp.UPN_GYOUSHA_CD = Convert.ToString(listdenpyou[50]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UPN_GYOUSHA_CD = String.Empty;
                            }
                        }

                        //運搬方法CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[60])))
                        {
                            tmp.UPN_HOUHOU_CD = Convert.ToString(listdenpyou[60]).ToUpper();
                        }

                        //車種CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[57])))
                        {
                            tmp.SHASHU_CD = Convert.ToString(listdenpyou[57]).PadLeft(3, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.SHASHU_CD = String.Empty;
                            }
                        }

                        //車種名
                        if (string.IsNullOrEmpty(tmp.SHASHU_CD) && preEntry == null)
                        {
                            tmp.SHASHU_NAME = Convert.ToString(listdenpyou[58]);
                        }

                        //車輌CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[55])))
                        {
                            tmp.SHARYOU_CD = Convert.ToString(listdenpyou[55]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.SHARYOU_CD = String.Empty;
                            }
                        }

                        //車輌名
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[56])))
                        {
                            tmp.SHARYOU_NAME = Convert.ToString(listdenpyou[56]);
                        }

                        //積替・保管有無
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[59])))
                        {
                            tmp.TMH_KBN = Convert.ToInt16(listdenpyou[59]);
                        }

                        //運搬の受託者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[107])))
                        {
                            tmp.UPN_JYUTAKUSHA_CD = Convert.ToString(listdenpyou[107]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UPN_JYUTAKUSHA_CD = String.Empty;
                            }
                        }

                        //運転者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[109])))
                        {
                            tmp.UNTENSHA_CD = Convert.ToString(listdenpyou[109]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UNTENSHA_CD = String.Empty;
                            }
                        }

                        //運転者名
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[110])))
                        {
                            tmp.UNTENSHA_NAME = Convert.ToString(listdenpyou[110]);
                        }

                        //運搬終了年月日
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[111])))
                        {
                            tmp.UPN_END_DATE = Convert.ToDateTime(Convert.ToString(listdenpyou[111]));
                        }

                    }
                    else if (No == 2)
                    {
                        //運搬受託者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[61])))
                        {
                            tmp.UPN_GYOUSHA_CD = Convert.ToString(listdenpyou[61]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UPN_GYOUSHA_CD = String.Empty;
                            }
                        }

                        //運搬方法CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[71])))
                        {
                            tmp.UPN_HOUHOU_CD = Convert.ToString(listdenpyou[71]).ToUpper();
                        }

                        //車種CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[68])))
                        {
                            tmp.SHASHU_CD = Convert.ToString(listdenpyou[68]).PadLeft(3, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.SHASHU_CD = String.Empty;
                            }
                        }

                        //車種名
                        if (string.IsNullOrEmpty(tmp.SHASHU_CD) && preEntry == null)
                        {
                            tmp.SHASHU_NAME = Convert.ToString(listdenpyou[69]);
                        }

                        //車輌CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[66])))
                        {
                            tmp.SHARYOU_CD = Convert.ToString(listdenpyou[66]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.SHARYOU_CD = String.Empty;
                            }
                        }

                        //車輌名
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[67])))
                        {
                            tmp.SHARYOU_NAME = Convert.ToString(listdenpyou[67]);
                        }

                        //積替・保管有無
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[70])))
                        {
                            tmp.TMH_KBN = Convert.ToInt16(listdenpyou[70]);
                        }

                        //運搬の受託者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[112])))
                        {
                            tmp.UPN_JYUTAKUSHA_CD = Convert.ToString(listdenpyou[112]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UPN_JYUTAKUSHA_CD = String.Empty;
                            }
                        }

                        //運転者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[114])))
                        {
                            tmp.UNTENSHA_CD = Convert.ToString(listdenpyou[114]).PadLeft(6, '0').ToUpper();
                        }
                        else
                        {
                            if (preEntry == null)
                            {
                                tmp.UNTENSHA_CD = String.Empty;
                            }
                        }

                        //運転者名
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[115])))
                        {
                            tmp.UNTENSHA_NAME = Convert.ToString(listdenpyou[115]);
                        }

                        //運搬終了年月日
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[116])))
                        {
                            tmp.UPN_END_DATE = Convert.ToDateTime(Convert.ToString(listdenpyou[116]));
                        }
                    }
                    //車種名
                    if (!string.IsNullOrEmpty(tmp.SHASHU_CD))
                    {
                        tmp.SHASHU_NAME = this.CheckShahuCd(tmp.SHASHU_CD);
                    }

                    //運搬先区分
                    tmp.UPN_SAKI_KBN = SqlInt16.Null;

                    //運搬先の事業者CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[72])))
                    {
                        tmp.UPN_SAKI_GYOUSHA_CD = Convert.ToString(listdenpyou[72]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.UPN_SAKI_GYOUSHA_CD = String.Empty;
                        }
                    }

                    //運搬先の事業場CD
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[77])))
                    {
                        tmp.UPN_SAKI_GENBA_CD = Convert.ToString(listdenpyou[77]).PadLeft(6, '0').ToUpper();
                    }
                    else
                    {
                        if (preEntry == null)
                        {
                            tmp.UPN_SAKI_GENBA_CD = String.Empty;
                        }
                    }

                    break;
                default:
                    break;
            }

            //運搬受託者名称
            M_GYOUSHA unpangyoushaEntity = null;
            if (!string.IsNullOrEmpty(tmp.UPN_GYOUSHA_CD))
            {
                unpangyoushaEntity = this.GetGyousha(tmp.UPN_GYOUSHA_CD);
            }
            tmp.UPN_GYOUSHA_NAME = string.Empty;
            if (unpangyoushaEntity != null)
            {
                if (!string.IsNullOrEmpty(unpangyoushaEntity.GYOUSHA_NAME2))
                {
                    tmp.UPN_GYOUSHA_NAME = unpangyoushaEntity.GYOUSHA_NAME1 + unpangyoushaEntity.GYOUSHA_NAME2;
                }
                else
                {
                    tmp.UPN_GYOUSHA_NAME = unpangyoushaEntity.GYOUSHA_NAME1;
                }
            }
            // 運搬受託者郵便番号
            if (unpangyoushaEntity != null && !string.IsNullOrEmpty(unpangyoushaEntity.GYOUSHA_POST))
            {
                tmp.UPN_GYOUSHA_POST = unpangyoushaEntity.GYOUSHA_POST;
            }
            else
            {
                tmp.UPN_GYOUSHA_POST = String.Empty;
            }

            // 運搬受託者電話番号
            if (unpangyoushaEntity != null && !string.IsNullOrEmpty(unpangyoushaEntity.GYOUSHA_TEL))
            {
                tmp.UPN_GYOUSHA_TEL = unpangyoushaEntity.GYOUSHA_TEL;
            }
            else
            {
                tmp.UPN_GYOUSHA_TEL = String.Empty;
            }

            // 運搬受託者住所
            if (unpangyoushaEntity != null && (!string.IsNullOrEmpty(unpangyoushaEntity.GYOUSHA_ADDRESS1) ||
                                            !string.IsNullOrEmpty(unpangyoushaEntity.GYOUSHA_ADDRESS2)))
            {
                string todofukenname = string.Empty;
                if (!unpangyoushaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    // 都道府県CDが取得できる場合
                    todofukenname = this.GetTodofukenName(unpangyoushaEntity.GYOUSHA_TODOUFUKEN_CD.ToString());
                }
                if (!string.IsNullOrEmpty(unpangyoushaEntity.GYOUSHA_ADDRESS2))
                {
                    tmp.UPN_GYOUSHA_ADDRESS = (todofukenname + unpangyoushaEntity.GYOUSHA_ADDRESS1) + unpangyoushaEntity.GYOUSHA_ADDRESS2;
                }
                else
                {
                    tmp.UPN_GYOUSHA_ADDRESS = todofukenname + unpangyoushaEntity.GYOUSHA_ADDRESS1;
                }
            }
            else
            {
                tmp.UPN_GYOUSHA_ADDRESS = String.Empty;
            }

            // 車輌名
            if (!string.IsNullOrEmpty(tmp.UPN_GYOUSHA_CD) && !string.IsNullOrEmpty(tmp.SHARYOU_CD))
            {
                tmp.SHARYOU_NAME = this.CheckSharyouCd(tmp.SHARYOU_CD, tmp.UPN_GYOUSHA_CD, tmp.SHASHU_CD);
            }

            // 運搬先区分にチェックが入っている場合、運搬先の事業場名称、運搬先の事業場郵便番号、運搬先の事業場電話番号、運搬先の事業場住所を設定する。
            if (upnSakiCheckFlg)
            {
                // 運搬先の事業場名称
                M_GENBA upnsakigenbaEntity = null;
                if (!string.IsNullOrEmpty(tmp.UPN_SAKI_GYOUSHA_CD) && !string.IsNullOrEmpty(tmp.UPN_SAKI_GENBA_CD))
                {
                    upnsakigenbaEntity = this.GetGenba(tmp.UPN_SAKI_GYOUSHA_CD, tmp.UPN_SAKI_GENBA_CD);
                }
                tmp.UPN_SAKI_GENBA_NAME = string.Empty;
                if (upnsakigenbaEntity != null)
                {
                    if (!string.IsNullOrEmpty(upnsakigenbaEntity.GENBA_NAME2))
                    {
                        tmp.UPN_SAKI_GENBA_NAME = upnsakigenbaEntity.GENBA_NAME1 + upnsakigenbaEntity.GENBA_NAME2;
                    }
                    else
                    {
                        tmp.UPN_SAKI_GENBA_NAME = upnsakigenbaEntity.GENBA_NAME1;
                    }
                }

                // 運搬先の事業場郵便番号
                if (upnsakigenbaEntity != null && !string.IsNullOrEmpty(upnsakigenbaEntity.GENBA_POST))
                {
                    tmp.UPN_SAKI_GENBA_POST = upnsakigenbaEntity.GENBA_POST;
                }
                else
                {
                    tmp.UPN_SAKI_GENBA_POST = String.Empty;
                }

                // 運搬先の事業場電話番号
                if (upnsakigenbaEntity != null && !string.IsNullOrEmpty(upnsakigenbaEntity.GENBA_TEL))
                {
                    tmp.UPN_SAKI_GENBA_TEL = upnsakigenbaEntity.GENBA_TEL;
                }
                else
                {
                    tmp.UPN_SAKI_GENBA_TEL = String.Empty;
                }

                // 運搬先の事業場住所
                if (upnsakigenbaEntity != null && (!string.IsNullOrEmpty(upnsakigenbaEntity.GENBA_ADDRESS1) ||
                                                !string.IsNullOrEmpty(upnsakigenbaEntity.GENBA_ADDRESS2)))
                {
                    string todofukenname = string.Empty;
                    if (!upnsakigenbaEntity.GENBA_TODOUFUKEN_CD.IsNull)
                    {
                        // 都道府県CDが取得できる場合
                        todofukenname = this.GetTodofukenName(upnsakigenbaEntity.GENBA_TODOUFUKEN_CD.ToString());
                    }
                    if (!string.IsNullOrEmpty(upnsakigenbaEntity.GENBA_ADDRESS2))
                    {
                        tmp.UPN_SAKI_GENBA_ADDRESS = (todofukenname + upnsakigenbaEntity.GENBA_ADDRESS1) + upnsakigenbaEntity.GENBA_ADDRESS2;
                    }
                    else
                    {
                        tmp.UPN_SAKI_GENBA_ADDRESS = todofukenname + upnsakigenbaEntity.GENBA_ADDRESS1;
                    }
                }
                else
                {
                    tmp.UPN_SAKI_GENBA_ADDRESS = String.Empty;
                }
            }

            //運搬の受託者名称
            M_GYOUSHA unpanJyu = null;
            if (!string.IsNullOrEmpty(tmp.UPN_JYUTAKUSHA_CD))
            {
                unpanJyu = this.GetGyousha(tmp.UPN_JYUTAKUSHA_CD);
            }
            tmp.UPN_JYUTAKUSHA_NAME = string.Empty;
            if (unpanJyu != null)
            {
                if (!string.IsNullOrEmpty(unpanJyu.GYOUSHA_NAME2))
                {
                    tmp.UPN_JYUTAKUSHA_NAME = unpanJyu.GYOUSHA_NAME1 + unpanJyu.GYOUSHA_NAME2;
                }
                else
                {
                    tmp.UPN_JYUTAKUSHA_NAME = unpanJyu.GYOUSHA_NAME1;
                }
            }

            //運転者名
            if (!string.IsNullOrEmpty(tmp.UNTENSHA_CD))
            {
                tmp.UNTENSHA_NAME = this.CheckUntenShaCd(tmp.UNTENSHA_CD);
            }

            if (haikiKbn == UIConstans.MANI_SBT_CHOKKOU)
            {
                //運搬先区分
                tmp.UPN_SAKI_KBN = 1;

                //有価物拾得量数量
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[65])))
                {
                    tmp.YUUKA_SUU = Convert.ToDecimal(Convert.ToString(listdenpyou[65]));
                }

                //有価物拾得量単位CD
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[66])))
                {
                    tmp.YUUKA_UNIT_CD = Convert.ToInt16(Convert.ToString(listdenpyou[66]));
                }
            }

            if (haikiKbn == UIConstans.MANI_SBT_TUMIKAE)
            {
                //運搬先区分
                if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[39])) && listdenpyou[39].ToString() == "1" && tmp.UPN_ROUTE_NO == 2
                    || !string.IsNullOrEmpty(Convert.ToString(listdenpyou[39])) && listdenpyou[39].ToString() == "1" && tmp.UPN_ROUTE_NO == 3
                    || !string.IsNullOrEmpty(Convert.ToString(listdenpyou[57])) && listdenpyou[57].ToString() == "1" && tmp.UPN_ROUTE_NO == 3)
                {
                    //運搬受託者CD
                    tmp.UPN_GYOUSHA_CD = string.Empty;

                    //運搬受託者名称
                    tmp.UPN_GYOUSHA_NAME = string.Empty;

                    //運搬受託者郵便番号
                    tmp.UPN_GYOUSHA_POST = string.Empty;

                    //運搬受託者電話番号
                    tmp.UPN_GYOUSHA_TEL = string.Empty;

                    //運搬受託者住所
                    tmp.UPN_GYOUSHA_ADDRESS = string.Empty;

                    //運搬方法CD
                    tmp.UPN_HOUHOU_CD = string.Empty;

                    //車種CD
                    tmp.SHASHU_CD = string.Empty;

                    //車種名
                    tmp.SHASHU_NAME = null;

                    //車輌CD
                    tmp.SHARYOU_CD = string.Empty;

                    // 車輌名
                    tmp.SHARYOU_NAME = string.Empty;

                    //運搬先区分
                    tmp.UPN_SAKI_KBN = SqlInt16.Null;

                    //運搬先の事業者CD
                    tmp.UPN_SAKI_GYOUSHA_CD = string.Empty;

                    //運搬先の事業場CD
                    tmp.UPN_SAKI_GENBA_CD = string.Empty;

                    //運搬先の事業場名称
                    tmp.UPN_SAKI_GENBA_NAME = string.Empty;

                    //運搬先の事業場郵便番号
                    tmp.UPN_SAKI_GENBA_POST = string.Empty;

                    //運搬先の事業場電話番号
                    tmp.UPN_SAKI_GENBA_TEL = string.Empty;

                    //運搬先の事業場住所
                    tmp.UPN_SAKI_GENBA_ADDRESS = string.Empty;

                    //運搬の受託者CD
                    tmp.UPN_JYUTAKUSHA_CD = string.Empty;

                    //運搬の受託者名称
                    tmp.UPN_JYUTAKUSHA_NAME = string.Empty;

                    //運転者CD
                    tmp.UNTENSHA_CD = string.Empty;

                    //運転者名
                    tmp.UNTENSHA_NAME = string.Empty;

                    //運搬終了年月日
                    tmp.UPN_END_DATE = SqlDateTime.Null;

                    //有価物拾得量数量
                    tmp.YUUKA_SUU = SqlDecimal.Null;

                    //有価物拾得量単位CD
                    tmp.YUUKA_UNIT_CD = SqlInt16.Null;
                }
                else if (tmp.UPN_SAKI_KBN.IsNull)
                {
                    //運搬先の事業者CD
                    tmp.UPN_SAKI_GYOUSHA_CD = string.Empty;

                    //運搬先の事業場CD
                    tmp.UPN_SAKI_GENBA_CD = string.Empty;

                    //運搬先の事業場名称
                    tmp.UPN_SAKI_GENBA_NAME = string.Empty;

                    //運搬先の事業場郵便番号
                    tmp.UPN_SAKI_GENBA_POST = string.Empty;

                    //運搬先の事業場電話番号
                    tmp.UPN_SAKI_GENBA_TEL = string.Empty;

                    //運搬先の事業場住所
                    tmp.UPN_SAKI_GENBA_ADDRESS = string.Empty;
                }

            }

            var who = new DataBinderLogic<T_MANIFEST_UPN>(tmp);
            who.SetSystemProperty(tmp, false);

            LogUtility.DebugMethodEnd(lSysId, iSeq, No, tmp);
        }

        /// <summary>
        /// 更新用前回値のT_MANIFEST_UPNを取得
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="No">運搬区間</param>
        /// <returns></returns>
        private T_MANIFEST_UPN GetPreManifestUpn(long lSysId, int iSeq, int No)
        {
            if (lSysId == 0 || iSeq <= 1)
            {
                return null;
            }

            T_MANIFEST_UPN searchDto = new T_MANIFEST_UPN();
            searchDto.SYSTEM_ID = Convert.ToInt64(lSysId);
            searchDto.SEQ = Convert.ToInt32(iSeq - 1);
            searchDto.UPN_ROUTE_NO = Convert.ToInt16(No);
            // 更新対象の前回値を取得
            T_MANIFEST_UPN retDto = this.manifestUpnDao.GetDataByPrimaryKey(searchDto);

            // 指定された項目のみ前回値を設定
            T_MANIFEST_UPN entryDto = new T_MANIFEST_UPN();

            // 運搬受託者CD
            entryDto.UPN_GYOUSHA_CD = retDto.UPN_GYOUSHA_CD;
            // 運搬方法CD
            entryDto.UPN_HOUHOU_CD = retDto.UPN_HOUHOU_CD;
            // 運搬先区分
            entryDto.UPN_SAKI_KBN = retDto.UPN_SAKI_KBN;
            // 車輌CD
            entryDto.SHARYOU_CD = retDto.SHARYOU_CD;
            // 車輌名
            entryDto.SHARYOU_NAME = retDto.SHARYOU_NAME;
            // 車種CD
            entryDto.SHASHU_CD = retDto.SHASHU_CD;
            // 車種名称
            entryDto.SHASHU_NAME = retDto.SHASHU_NAME;
            // 積替・保管有無
            entryDto.TMH_KBN = retDto.TMH_KBN;
            // 運搬先の事業者CD
            entryDto.UPN_SAKI_GYOUSHA_CD = retDto.UPN_SAKI_GYOUSHA_CD;
            // 運搬先の事業場CD
            entryDto.UPN_SAKI_GENBA_CD = retDto.UPN_SAKI_GENBA_CD;
            // 運搬の受託者CD
            entryDto.UPN_JYUTAKUSHA_CD = retDto.UPN_JYUTAKUSHA_CD;
            // 運転者CD
            entryDto.UNTENSHA_CD = retDto.UNTENSHA_CD;
            // 運転者名
            entryDto.UNTENSHA_NAME = retDto.UNTENSHA_NAME;

            return entryDto;
        }

        /// <summary>
        /// マニ印字(T_MANIFEST_PRT)データ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="tmp">T_MANIFEST_PRTのDTO</param>
        /// <param name="listdenpyou">CSVデータ</param>
        private void MakeManifestPrt(long lSysId, int iSeq, string haikiKbn, ref T_MANIFEST_PRT tmp, string[] listdenpyou)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, haikiKbn, tmp, listdenpyou);

            // システムID
            tmp.SYSTEM_ID = lSysId;

            // 枝番
            tmp.SEQ = iSeq;

            // 印字種類（普通）
            tmp.PRT_FUTSUU_HAIKIBUTSU = false;

            // 印字種類（特管）
            tmp.PRT_TOKUBETSU_HAIKIBUTSU = false;

            // 印字廃棄物種類CD
            tmp.PRT_HAIKI_SHURUI_CD = null;

            // 印字廃棄物種類名
            tmp.PRT_HAIKI_SHURUI_NAME = null;

            // 印字数量
            tmp.PRT_SUU = SqlDecimal.Null;

            // 印字単位CD
            tmp.PRT_UNIT_CD = SqlInt16.Null;

            // 印字廃棄物名称CD
            tmp.PRT_HAIKI_NAME_CD = string.Empty;

            // 印字廃棄物名称
            tmp.PRT_HAIKI_NAME = string.Empty;

            //マニフェスト一式（マニ明細、マニ印字明細除く）データ読み込み
            var search = new CommonSerchParameterDtoCls();
            search.SYSTEM_ID = lSysId.ToString();
            search.SEQ = (iSeq - 1).ToString();
            if (haikiKbn.Equals(UIConstans.MANI_SBT_CHOKKOU))
            {
                search.HAIKI_KBN_CD = UIConstans.HAIKI_KBN_CHOKKOU.ToString();
            }
            else if (haikiKbn.Equals(UIConstans.MANI_SBT_TUMIKAE))
            {
                search.HAIKI_KBN_CD = UIConstans.HAIKI_KBN_TUMIKAE.ToString();
            }
            else if (haikiKbn.Equals(UIConstans.MANI_SBT_KENPAI))
            {
                search.HAIKI_KBN_CD = UIConstans.HAIKI_KBN_KENPAI.ToString();
            }

            DataTable dt = this.mlogic.SearchData(search);
            if (dt.Rows.Count != 0)
            {
                // 印字単位CD
                if (!dt.Rows[0]["PRT_UNIT_CD"].Equals(DBNull.Value))
                {
                    tmp.PRT_UNIT_CD = Convert.ToInt16(dt.Rows[0]["PRT_UNIT_CD"]);
                }

                // 斜線水銀
                if (!dt.Rows[0]["SLASH_MERCURY_FLG"].Equals(DBNull.Value))
                {
                    tmp.SLASH_MERCURY_FLG = Convert.ToBoolean(dt.Rows[0]["SLASH_MERCURY_FLG"]);
                }
            }

            if (haikiKbn != UIConstans.MANI_SBT_KENPAI)
            {
                if (dt.Rows.Count != 0)
                {
                    // 印字有害物質CD
                    tmp.PRT_YUUGAI_CD = Convert.ToString(dt.Rows[0]["PRT_YUUGAI_CD"]);
                    // 印字有害物質名称
                    tmp.PRT_YUUGAI_NAME = Convert.ToString(dt.Rows[0]["PRT_YUUGAI_NAME"]);
                    // 印字種類（普通）
                    tmp.PRT_FUTSUU_HAIKIBUTSU = Convert.ToBoolean(dt.Rows[0]["PRT_FUTSUU_HAIKIBUTSU"]);
                    // 印字種類（特管）
                    tmp.PRT_TOKUBETSU_HAIKIBUTSU = Convert.ToBoolean(dt.Rows[0]["PRT_TOKUBETSU_HAIKIBUTSU"]);
                    // 印字廃棄物種類CD
                    if (!String.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["PRT_HAIKI_SHURUI_CD"])))
                    {
                        tmp.PRT_HAIKI_SHURUI_CD = Convert.ToString(dt.Rows[0]["PRT_HAIKI_SHURUI_CD"]);
                    }
                    // 印字廃棄物種類名
                    if (!String.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["PRT_HAIKI_SHURUI_NAME"])))
                    {
                        tmp.PRT_HAIKI_SHURUI_NAME = Convert.ToString(dt.Rows[0]["PRT_HAIKI_SHURUI_NAME"]);
                    }
                    // 印字数量
                    if (!dt.Rows[0]["PRT_SUU"].Equals(DBNull.Value))
                    {
                        tmp.PRT_SUU = Convert.ToDecimal(dt.Rows[0]["PRT_SUU"]);
                    }
                    // 印字荷姿CD
                    tmp.PRT_NISUGATA_CD = Convert.ToString(dt.Rows[0]["PRT_NISUGATA_CD"]);
                    // 印字荷姿名称
                    tmp.PRT_NISUGATA_NAME = Convert.ToString(dt.Rows[0]["PRT_NISUGATA_NAME"]);
                    // 印字廃棄物名称CD
                    tmp.PRT_HAIKI_NAME_CD = Convert.ToString(dt.Rows[0]["PRT_HAIKI_NAME_CD"]);
                    // 印字廃棄物名称
                    tmp.PRT_HAIKI_NAME = Convert.ToString(dt.Rows[0]["PRT_HAIKI_NAME"]);
                    // 印字処分方法CD
                    tmp.PRT_SBN_HOUHOU_CD = Convert.ToString(dt.Rows[0]["PRT_SBN_HOUHOU_CD"]);
                    // 印字処分方法名
                    tmp.PRT_SBN_HOUHOU_NAME = Convert.ToString(dt.Rows[0]["PRT_SBN_HOUHOU_NAME"]);
                }
                else
                {
                    // 印字荷姿CD
                    tmp.PRT_NISUGATA_CD = string.Empty;
                    // 印字荷姿名称
                    tmp.PRT_NISUGATA_NAME = string.Empty;
                    // 印字処分方法CD
                    tmp.PRT_SBN_HOUHOU_CD = string.Empty;
                    // 印字処分方法名
                    tmp.PRT_SBN_HOUHOU_NAME = string.Empty;
                }
            }

            // 斜線項目有害物質
            tmp.SLASH_YUUGAI_FLG = false;

            // 斜線項目備考
            tmp.SLASH_BIKOU_FLG = false;

            // 斜線項目中間処理産業廃棄物
            tmp.SLASH_CHUUKAN_FLG = false;

            // 斜線項目積替保管
            tmp.SLASH_TSUMIHO_FLG = false;

            // 斜線項目備考
            tmp.SLASH_BIKOU_FLG = false;

            // 斜線項目事前協議
            tmp.SLASH_JIZENKYOUGI_FLG = false;

            // 斜線項目運搬受託者2
            tmp.SLASH_UPN_GYOUSHA2_FLG = false;

            // 斜線項目運搬受託者3
            tmp.SLASH_UPN_GYOUSHA3_FLG = false;

            // 斜線項目運搬の受託者2
            tmp.SLASH_UPN_JYUTAKUSHA2_FLG = false;

            // 斜線項目運搬の受託者3
            tmp.SLASH_UPN_JYUTAKUSHA3_FLG = false;

            // 斜線項目運搬先事業場2
            tmp.SLASH_UPN_SAKI_GENBA2_FLG = false;

            // 斜線項目運搬先事業場3
            tmp.SLASH_UPN_SAKI_GENBA3_FLG = false;

            // 斜線項目B1票
            tmp.SLASH_B1_FLG = false;

            // 斜線項目B2票
            tmp.SLASH_B2_FLG = false;

            // 斜線項目B4票
            tmp.SLASH_B4_FLG = false;

            // 斜線項目B6票
            tmp.SLASH_B6_FLG = false;

            // 斜線項目D票
            tmp.SLASH_D_FLG = false;

            // 斜線項目E票
            tmp.SLASH_E_FLG = false;

            // 廃棄物区分
            switch (haikiKbn)
            {
                case UIConstans.MANI_SBT_CHOKKOU:
                    // 印字有害物質CD
                    if (string.IsNullOrEmpty(tmp.PRT_YUUGAI_CD) && string.IsNullOrEmpty(tmp.PRT_YUUGAI_NAME))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[57])))
                        {
                            tmp.PRT_YUUGAI_CD = Convert.ToString(listdenpyou[57]).PadLeft(2, '0').ToUpper();
                        }
                        else
                        {
                            tmp.PRT_YUUGAI_CD = String.Empty;
                        }
                        // 印字有害物質名
                        tmp.PRT_YUUGAI_NAME = Convert.ToString(listdenpyou[58]);
                    }
                    // 斜線項目有害物質
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[89])))
                    {
                        tmp.SLASH_YUUGAI_FLG = ToBoolean(listdenpyou[89]);
                    }
                    // 斜線項目備考
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[90])))
                    {
                        tmp.SLASH_BIKOU_FLG = ToBoolean(listdenpyou[90]);
                    }
                    // 斜線項目中間処理産業廃棄物
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[91])))
                    {
                        tmp.SLASH_CHUUKAN_FLG = ToBoolean(listdenpyou[91]);
                    }
                    // 斜線項目積替保管
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[92])))
                    {
                        tmp.SLASH_TSUMIHO_FLG = ToBoolean(listdenpyou[92]);
                    }
                    break;
                case UIConstans.MANI_SBT_TUMIKAE:
                    // 印字有害物質CD
                    if (string.IsNullOrEmpty(tmp.PRT_YUUGAI_CD) && string.IsNullOrEmpty(tmp.PRT_YUUGAI_NAME))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[94])))
                        {
                            tmp.PRT_YUUGAI_CD = Convert.ToString(listdenpyou[94]).PadLeft(2, '0').ToUpper();
                        }
                        else
                        {
                            tmp.PRT_YUUGAI_CD = String.Empty;
                        }
                        // 印字有害物質名
                        tmp.PRT_YUUGAI_NAME = Convert.ToString(listdenpyou[95]);
                    }
                    // 斜線項目有害物質
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[146])))
                    {
                        tmp.SLASH_YUUGAI_FLG = ToBoolean(listdenpyou[146]);
                    }
                    // 斜線項目備考
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[155])))
                    {
                        tmp.SLASH_BIKOU_FLG = ToBoolean(listdenpyou[155]);
                    }
                    // 斜線項目中間処理産業廃棄物
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[147])))
                    {
                        tmp.SLASH_CHUUKAN_FLG = ToBoolean(listdenpyou[147]);
                    }
                    // 斜線項目積替保管
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[152])))
                    {
                        tmp.SLASH_TSUMIHO_FLG = ToBoolean(listdenpyou[152]);
                    }
                    // 斜線項目運搬受託者2
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[148])))
                    {
                        tmp.SLASH_UPN_GYOUSHA2_FLG = ToBoolean(listdenpyou[148]);
                    }
                    // 斜線項目運搬受託者3
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[150])))
                    {
                        tmp.SLASH_UPN_GYOUSHA3_FLG = ToBoolean(listdenpyou[150]);
                    }
                    // 斜線項目運搬の受託者2
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[149])))
                    {
                        tmp.SLASH_UPN_JYUTAKUSHA2_FLG = ToBoolean(listdenpyou[149]);
                    }
                    // 斜線項目運搬の受託者3
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[151])))
                    {
                        tmp.SLASH_UPN_JYUTAKUSHA3_FLG = ToBoolean(listdenpyou[151]);
                    }
                    // 斜線項目運搬先事業場2
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[153])))
                    {
                        tmp.SLASH_UPN_SAKI_GENBA2_FLG = ToBoolean(listdenpyou[153]);
                    }
                    // 斜線項目運搬先事業場3
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[154])))
                    {
                        tmp.SLASH_UPN_SAKI_GENBA3_FLG = ToBoolean(listdenpyou[154]);
                    }
                    // 斜線項目B4票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[156])))
                    {
                        tmp.SLASH_B4_FLG = ToBoolean(listdenpyou[156]);
                    }
                    // 斜線項目B6票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[157])))
                    {
                        tmp.SLASH_B6_FLG = ToBoolean(listdenpyou[157]);
                    }
                    break;
                case UIConstans.MANI_SBT_KENPAI:
                    // 斜線項目備考
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[153])))
                    {
                        tmp.SLASH_BIKOU_FLG = ToBoolean(listdenpyou[153]);
                    }
                    // 斜線項目中間処理産業廃棄物
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[149])))
                    {
                        tmp.SLASH_CHUUKAN_FLG = ToBoolean(listdenpyou[149]);
                    }
                    // 斜線項目積替保管
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[151])))
                    {
                        tmp.SLASH_TSUMIHO_FLG = ToBoolean(listdenpyou[151]);
                    }
                    // 斜線項目事前協議
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[144])))
                    {
                        tmp.SLASH_JIZENKYOUGI_FLG = ToBoolean(listdenpyou[144]);
                    }
                    // 斜線項目運搬受託者2
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[150])))
                    {
                        tmp.SLASH_UPN_GYOUSHA2_FLG = ToBoolean(listdenpyou[150]);
                    }
                    // 斜線項目運搬の受託者2
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[152])))
                    {
                        tmp.SLASH_UPN_JYUTAKUSHA2_FLG = ToBoolean(listdenpyou[152]);
                    }
                    // 斜線項目B1票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[145])))
                    {
                        tmp.SLASH_B1_FLG = ToBoolean(listdenpyou[145]);
                    }
                    // 斜線項目B2票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[146])))
                    {
                        tmp.SLASH_B2_FLG = ToBoolean(listdenpyou[146]);
                    }
                    // 斜線項目D票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[147])))
                    {
                        tmp.SLASH_D_FLG = ToBoolean(listdenpyou[147]);
                    }
                    // 斜線項目E票
                    if (!string.IsNullOrEmpty(Convert.ToString(listdenpyou[148])))
                    {
                        tmp.SLASH_E_FLG = ToBoolean(listdenpyou[148]);
                    }
                    break;
                default:
                    break;
            }

            if (haikiKbn != UIConstans.MANI_SBT_KENPAI)
            {
                if (!string.IsNullOrEmpty(tmp.PRT_YUUGAI_CD))
                {
                    // 印字有害物質名
                    tmp.PRT_YUUGAI_NAME = this.CheckYuugaiBusshitsuCd(tmp.PRT_YUUGAI_CD);
                }
            }

            var who = new DataBinderLogic<T_MANIFEST_PRT>(tmp);
            who.SetSystemProperty(tmp, false);

            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp);
        }

        /// <summary>
        /// マニ明細(T_MANIFEST_DETAIL)リストデータ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="iDsysID">明細システムID</param>
        /// <param name="manifestKbn">マニフェスト区分</param>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="list">T_MANIFEST_DETAILのDTO</param>
        /// <param name="listdenpyou">CSVデータ</param>
        private void MakeManifestDetailList(long lSysId, int iSeq, long iDsysID, bool manifestKbn, string haikiKbn, ref List<T_MANIFEST_DETAIL> list, List<string[]> listdenpyou)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, iDsysID, manifestKbn, haikiKbn, list, listdenpyou);

            T_MANIFEST_DETAIL tmp = null;

            var search = new CommonSerchParameterDtoCls();
            search.SYSTEM_ID = Convert.ToString(lSysId);
            search.SEQ = Convert.ToString(iSeq - 1);
            DataTable dtDetail = this.mlogic.SearchDetailData(search);
            int count = 0;

            foreach (var item in listdenpyou)
            {
                // 明細行にデータが存在するかチェックする。
                bool detailFlg = false;
                switch (haikiKbn)
                {
                    case UIConstans.MANI_SBT_CHOKKOU:
                        for (int i = 95; i < item.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(item[i]))
                            {
                                detailFlg = true;
                                break;
                            }
                        }
                        break;
                    case UIConstans.MANI_SBT_TUMIKAE:
                        for (int i = 160; i < item.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(item[i]))
                            {
                                detailFlg = true;
                            }
                        }
                        break;
                    case UIConstans.MANI_SBT_KENPAI:
                        for (int i = 156; i < item.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(item[i]))
                            {
                                detailFlg = true;
                            }
                        }
                        break;
                    default:
                        break;
                }


                // 明細行にデータが存在しない場合、T_MANIFEST_DETAILにレコードを作成しない。
                if (!detailFlg)
                {
                    continue;
                }

                tmp = new T_MANIFEST_DETAIL();

                //システムID
                tmp.SYSTEM_ID = lSysId;

                //枝番
                tmp.SEQ = iSeq;

                //明細システムID
                iDsysID = 0;
                if (dtDetail.Rows.Count != 0 && count < dtDetail.Rows.Count)
                {
                    DataRow detaildr = dtDetail.Rows[count];

                    if (detaildr["DETAIL_SYSTEM_ID"].ToString() != string.Empty)
                    {
                        iDsysID = Convert.ToInt64(detaildr["DETAIL_SYSTEM_ID"]);
                    }
                    count++;
                }

                //明細システムID
                if (iDsysID == 0)
                {
                    // 印刷時はSYSTEM_IDの採番不要(印刷部数ポップアップ側で採番するため)
                    Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();
                    tmp.DETAIL_SYSTEM_ID = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);
                }
                else
                {
                    tmp.DETAIL_SYSTEM_ID = iDsysID;
                }

                //数量
                tmp.HAIKI_SUU = 0;

                //換算後数量
                tmp.KANSAN_SUU = 0;

                // 備考
                switch (haikiKbn)
                {
                    case UIConstans.MANI_SBT_CHOKKOU:
                        //廃棄物種類CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[95])))
                        {
                            tmp.HAIKI_SHURUI_CD = Convert.ToString(item[95]).PadLeft(4, '0').ToUpper();
                        }
                        //廃棄物名称CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[97])))
                        {
                            tmp.HAIKI_NAME_CD = Convert.ToString(item[97]).PadLeft(6, '0').ToUpper();
                        }
                        //荷姿CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[99])))
                        {
                            tmp.NISUGATA_CD = Convert.ToString(item[99]).PadLeft(2, '0').ToUpper();
                        }
                        //数量
                        if (!string.IsNullOrEmpty(Convert.ToString(item[101])))
                        {
                            tmp.HAIKI_SUU = Convert.ToDecimal(item[101]);
                        }
                        //単位CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[102])))
                        {
                            tmp.HAIKI_UNIT_CD = Convert.ToInt16(item[102]);
                        }

                        //処分方法CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[105])))
                        {
                            tmp.SBN_HOUHOU_CD = Convert.ToString(item[105]).PadLeft(3, '0').ToUpper();
                        }

                        //処分終了日
                        if (!string.IsNullOrEmpty(Convert.ToString(item[107])))
                        {
                            tmp.SBN_END_DATE = Convert.ToDateTime(item[107]);
                        }

                        //最終処分終了日
                        if (!string.IsNullOrEmpty(Convert.ToString(item[112])))
                        {
                            tmp.LAST_SBN_END_DATE = Convert.ToDateTime(item[112]);
                        }

                        //最終処分業者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[108])))
                        {
                            tmp.LAST_SBN_GYOUSHA_CD = Convert.ToString(item[108]).PadLeft(6, '0').ToUpper();
                        }

                        //最終処分現場CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[110])))
                        {
                            tmp.LAST_SBN_GENBA_CD = Convert.ToString(item[110]).PadLeft(6, '0').ToUpper();
                        }
                        break;
                    case UIConstans.MANI_SBT_TUMIKAE:
                        //廃棄物種類CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[160])))
                        {
                            tmp.HAIKI_SHURUI_CD = Convert.ToString(item[160]).PadLeft(4, '0').ToUpper();
                        }
                        //廃棄物名称CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[162])))
                        {
                            tmp.HAIKI_NAME_CD = Convert.ToString(item[162]).PadLeft(6, '0').ToUpper();
                        }
                        //荷姿CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[164])))
                        {
                            tmp.NISUGATA_CD = Convert.ToString(item[164]).PadLeft(2, '0').ToUpper();
                        }
                        //数量
                        if (!string.IsNullOrEmpty(Convert.ToString(item[166])))
                        {
                            tmp.HAIKI_SUU = Convert.ToDecimal(item[166]);
                        }
                        //単位CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[167])))
                        {
                            tmp.HAIKI_UNIT_CD = Convert.ToInt16(item[167]);
                        }

                        //処分方法CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[170])))
                        {
                            tmp.SBN_HOUHOU_CD = Convert.ToString(item[170]).PadLeft(3, '0').ToUpper();
                        }

                        //処分終了日
                        if (!string.IsNullOrEmpty(Convert.ToString(item[172])))
                        {
                            tmp.SBN_END_DATE = Convert.ToDateTime(item[172]);
                        }

                        //最終処分終了日
                        if (!string.IsNullOrEmpty(Convert.ToString(item[177])))
                        {
                            tmp.LAST_SBN_END_DATE = Convert.ToDateTime(item[177]);
                        }

                        //最終処分業者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[173])))
                        {
                            tmp.LAST_SBN_GYOUSHA_CD = Convert.ToString(item[173]).PadLeft(6, '0').ToUpper();
                        }

                        //最終処分現場CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[175])))
                        {
                            tmp.LAST_SBN_GENBA_CD = Convert.ToString(item[175]).PadLeft(6, '0').ToUpper();
                        }
                        break;
                    case UIConstans.MANI_SBT_KENPAI:
                        //廃棄物種類CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[156])))
                        {
                            tmp.HAIKI_SHURUI_CD = Convert.ToString(item[156]).PadLeft(4, '0').ToUpper();
                        }
                        //廃棄物名称CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[158])))
                        {
                            tmp.HAIKI_NAME_CD = Convert.ToString(item[158]).PadLeft(6, '0').ToUpper();
                        }
                        //荷姿CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[160])))
                        {
                            tmp.NISUGATA_CD = Convert.ToString(item[160]).PadLeft(2, '0').ToUpper();
                        }
                        //数量
                        if (!string.IsNullOrEmpty(Convert.ToString(item[162])))
                        {
                            tmp.HAIKI_SUU = Convert.ToDecimal(item[162]);
                        }
                        //単位CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[163])))
                        {
                            tmp.HAIKI_UNIT_CD = Convert.ToInt16(item[163]);
                        }

                        //処分方法CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[166])))
                        {
                            tmp.SBN_HOUHOU_CD = Convert.ToString(item[166]).PadLeft(3, '0').ToUpper();
                        }

                        //処分終了日
                        if (!string.IsNullOrEmpty(Convert.ToString(item[168])))
                        {
                            tmp.SBN_END_DATE = Convert.ToDateTime(item[168]);
                        }

                        //最終処分終了日
                        if (!string.IsNullOrEmpty(Convert.ToString(item[169])))
                        {
                            tmp.LAST_SBN_END_DATE = Convert.ToDateTime(item[169]);
                        }

                        //最終処分業者CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[170])))
                        {
                            tmp.LAST_SBN_GYOUSHA_CD = Convert.ToString(item[170]).PadLeft(6, '0').ToUpper();
                        }

                        //最終処分現場CD
                        if (!string.IsNullOrEmpty(Convert.ToString(item[172])))
                        {
                            tmp.LAST_SBN_GENBA_CD = Convert.ToString(item[172]).PadLeft(6, '0').ToUpper();
                        }
                        break;
                    default:
                        break;
                }

                // 換算後数量、減容後数量の算出に渡す廃棄物区分を設定する。
                String haikibutsuKbn = "";
                if (haikiKbn.Equals(UIConstans.MANI_SBT_CHOKKOU))
                {
                    // 直行
                    haikibutsuKbn = UIConstans.HAIKI_KBN_CHOKKOU.ToString();
                }
                else if (haikiKbn.Equals(UIConstans.MANI_SBT_TUMIKAE))
                {
                    // 積替
                    haikibutsuKbn = UIConstans.HAIKI_KBN_TUMIKAE.ToString();
                }
                else if (haikiKbn.Equals(UIConstans.MANI_SBT_KENPAI))
                {
                    // 建廃
                    haikibutsuKbn = UIConstans.HAIKI_KBN_KENPAI.ToString();
                }

                // 換算後数量
                tmp.KANSAN_SUU = this.GetKansanSuu(haikibutsuKbn, tmp.HAIKI_SHURUI_CD, tmp.HAIKI_NAME_CD, tmp.NISUGATA_CD, Convert.ToInt16(tmp.HAIKI_UNIT_CD.Value), Convert.ToDecimal(tmp.HAIKI_SUU.Value));

                // 1次マニの場合のみ設定
                if (!manifestKbn)
                {
                    //減容後数量
                    tmp.GENNYOU_SUU = this.GetGenyouSuu(haikibutsuKbn, tmp.HAIKI_SHURUI_CD, tmp.HAIKI_NAME_CD, tmp.SBN_HOUHOU_CD, Convert.ToDecimal(tmp.KANSAN_SUU.Value));
                }

                this.totalSuu += tmp.HAIKI_SUU;
                this.totalKansanSuu += tmp.KANSAN_SUU;
                this.totalGennyouSuu += tmp.GENNYOU_SUU;

                var who = new DataBinderLogic<T_MANIFEST_DETAIL>(tmp);
                who.SetSystemProperty(tmp, false);

                list.Add(tmp);

            }

            LogUtility.DebugMethodEnd(lSysId, iSeq, haikiKbn, list, listdenpyou);
        }

        /// <summary>
        /// マニ印字明細(T_MANIFEST_DETAIL_PRT)データ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="list">T_MANIFEST_DETAIL_PRTのDTO</param>
        private void MakeManifestDetailPrtList(long lSysId, int iSeq, string haikiKbn, ref  List<T_MANIFEST_DETAIL_PRT> list)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, haikiKbn, list);

            T_MANIFEST_DETAIL_PRT tmp = null;
            //マニフェスト一式（マニ明細、マニ印字明細除く）データ読み込み
            var search = new CommonSerchParameterDtoCls();
            search.SYSTEM_ID = lSysId.ToString();
            search.SEQ = (iSeq - 1).ToString();
            //マニ印字明細
            DataTable dtDetailPrt = this.mlogic.SearchDetailPrtData(search);
            if (dtDetailPrt.Rows.Count > 0)
            {
                for (int i = 0; i < dtDetailPrt.Rows.Count; i++)
                {
                    tmp = new T_MANIFEST_DETAIL_PRT();
                    MakeManifestDetailPrt(lSysId, iSeq, haikiKbn, dtDetailPrt.Rows[i], ref tmp);
                    list.Add(tmp);
                }
            }
        }

        /// <summary>
        /// マニ印字明細(T_MANIFEST_DETAIL_PRT)データ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="dr">T_MANIFEST_DETAIL_PRTのDBデータ</param>
        /// <param name="tmp">T_MANIFEST_DETAIL_PRTのDTO</param>
        private void MakeManifestDetailPrt(long lSysId, int iSeq, string haikiKbn, DataRow dr, ref T_MANIFEST_DETAIL_PRT tmp)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, haikiKbn, dr, tmp);

            // システムID
            tmp.SYSTEM_ID = lSysId;

            // 枝番
            tmp.SEQ = iSeq;

            // 印字番号
            if (!string.IsNullOrEmpty(dr["REC_NO"].ToString()))
            {
                tmp.REC_NO = Convert.ToInt16(dr["REC_NO"].ToString());
            }

            if (haikiKbn != UIConstans.MANI_SBT_TUMIKAE)
            {
                // 廃棄物種類CD
                tmp.HAIKI_SHURUI_CD = dr["HAIKI_SHURUI_CD"].ToString();

                // 廃棄物種類名
                tmp.HAIKI_SHURUI_NAME = dr["HAIKI_SHURUI_NAME"].ToString();

                // checkBox
                if ((dr["PRT_FLG"] != null && !string.IsNullOrEmpty(dr["PRT_FLG"].ToString())))
                {
                    tmp.PRT_FLG = dr.Field<bool>("PRT_FLG");
                }

                // 数量
                if (String.IsNullOrEmpty(dr["HAIKI_SUURYOU"].ToString()) == false)
                {
                    tmp.HAIKI_SUURYOU = Convert.ToDecimal(dr["HAIKI_SUURYOU"].ToString());
                }
            }

            var who = new DataBinderLogic<T_MANIFEST_DETAIL_PRT>(tmp);
            who.SetSystemProperty(tmp, false);

            LogUtility.DebugMethodEnd(lSysId, iSeq, haikiKbn, dr, tmp);
        }

        /// <summary>
        /// マニ返却日(T_MANIFEST_RET_DATE)データ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="list">T_MANIFEST_RET_DATEのDTO</param>
        /// <param name="listdenpyou">CSVデータ</param>
        /// <param name="manifestKbn">マニフェスト区分</param>
        /// <param name="delflg">削除フラグ</param>
        private void MakeManifestRetDateList(long lSysId, int iSeq, string haikiKbn, ref  List<T_MANIFEST_RET_DATE> list, string[] listdenpyou, bool manifestKbn, bool delflg)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, haikiKbn, list, listdenpyou, manifestKbn, delflg);
            T_MANIFEST_RET_DATE tmp = null;

            tmp = new T_MANIFEST_RET_DATE();

            // 前回値取得
            T_MANIFEST_RET_DATE preEntry = null;
            if (lSysId != 0 && 2 <= iSeq)
            {
                preEntry = GetPreManifestRetDate(lSysId, iSeq);
                if (preEntry != null)
                {
                    tmp = preEntry;
                }
            }

            //システムID
            tmp.SYSTEM_ID = lSysId;

            //枝番
            tmp.SEQ = iSeq;

            DateTime datetime = DateTime.Now;

            if (!manifestKbn)
            {
                // 廃棄物区分
                switch (haikiKbn)
                {
                    case UIConstans.MANI_SBT_CHOKKOU:
                        //A票
                        if (this.mSysInfo.MANIFEST_USE_A == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[82])))
                        {
                            tmp.SEND_A = Convert.ToDateTime(listdenpyou[82]);
                        }
                        //B1票
                        if (this.mSysInfo.MANIFEST_USE_B1 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[83])))
                        {
                            tmp.SEND_B1 = Convert.ToDateTime(listdenpyou[83]);
                        }
                        //B2票
                        if (this.mSysInfo.MANIFEST_USE_B2 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[84])))
                        {
                            tmp.SEND_B2 = Convert.ToDateTime(listdenpyou[84]);
                        }
                        //C1票
                        if (this.mSysInfo.MANIFEST_USE_C1 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[85])))
                        {
                            tmp.SEND_C1 = Convert.ToDateTime(listdenpyou[85]);
                        }
                        //C2票
                        if (this.mSysInfo.MANIFEST_USE_C2 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[86])))
                        {
                            if (DateTime.TryParse(listdenpyou[86].ToString(), out datetime))
                            {
                                tmp.SEND_C2 = Convert.ToDateTime(listdenpyou[86]);
                            }
                        }
                        //D票
                        if (this.mSysInfo.MANIFEST_USE_D == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[87])))
                        {
                            if (DateTime.TryParse(listdenpyou[87].ToString(), out datetime))
                            {
                                tmp.SEND_D = Convert.ToDateTime(listdenpyou[87]);
                            }
                        }
                        //E票
                        if (this.mSysInfo.MANIFEST_USE_E == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[88])))
                        {
                            if (DateTime.TryParse(listdenpyou[88].ToString(), out datetime))
                            {
                                tmp.SEND_E = Convert.ToDateTime(listdenpyou[88]);
                            }
                        }
                        break;
                    case UIConstans.MANI_SBT_TUMIKAE:
                        //B4票
                        if (this.mSysInfo.MANIFEST_USE_B4 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[140])))
                        {
                            tmp.SEND_B4 = Convert.ToDateTime(listdenpyou[140]);
                        }
                        //B6票
                        if (this.mSysInfo.MANIFEST_USE_B6 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[141])))
                        {
                            tmp.SEND_B6 = Convert.ToDateTime(listdenpyou[141]);
                        }
                        //C1票
                        if (this.mSysInfo.MANIFEST_USE_C1 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[142])))
                        {
                            tmp.SEND_C1 = Convert.ToDateTime(listdenpyou[142]);
                        }
                        //C2票
                        if (this.mSysInfo.MANIFEST_USE_C2 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[143])))
                        {
                            tmp.SEND_C2 = Convert.ToDateTime(listdenpyou[143]);
                        }
                        //D票
                        if (this.mSysInfo.MANIFEST_USE_D == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[144])))
                        {
                            tmp.SEND_D = Convert.ToDateTime(listdenpyou[144]);
                        }
                        //E票
                        if (this.mSysInfo.MANIFEST_USE_E == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[145])))
                        {
                            tmp.SEND_E = Convert.ToDateTime(listdenpyou[145]);
                        }
                        break;
                    case UIConstans.MANI_SBT_KENPAI:
                        //B1票
                        if (this.mSysInfo.MANIFEST_USE_B1 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[138])))
                        {
                            tmp.SEND_B1 = Convert.ToDateTime(listdenpyou[138]);
                        }
                        //C1票
                        if (this.mSysInfo.MANIFEST_USE_C1 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[140])))
                        {
                            tmp.SEND_C1 = Convert.ToDateTime(listdenpyou[140]);
                        }
                        //C2票
                        if (this.mSysInfo.MANIFEST_USE_C2 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[141])))
                        {
                            tmp.SEND_C2 = Convert.ToDateTime(listdenpyou[141]);
                        }
                        //D票
                        if (this.mSysInfo.MANIFEST_USE_D == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[142])))
                        {
                            tmp.SEND_D = Convert.ToDateTime(listdenpyou[142]);
                        }
                        //E票
                        if (this.mSysInfo.MANIFEST_USE_E == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[143])))
                        {
                            tmp.SEND_E = Convert.ToDateTime(listdenpyou[143]);
                        }
                        break;
                    default:
                        break;
                }
                if (haikiKbn != UIConstans.MANI_SBT_CHOKKOU)
                {
                    //A票
                    if (this.mSysInfo.MANIFEST_USE_A == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[137])))
                    {
                        tmp.SEND_A = Convert.ToDateTime(listdenpyou[137]);
                    }
                    //B2票
                    if (this.mSysInfo.MANIFEST_USE_B2 == 1 && !string.IsNullOrEmpty(Convert.ToString(listdenpyou[139])))
                    {
                        tmp.SEND_B2 = Convert.ToDateTime(listdenpyou[139]);
                    }
                }
            }
            //削除フラグ
            tmp.DELETE_FLG = delflg;

            var who = new DataBinderLogic<T_MANIFEST_RET_DATE>(tmp);
            who.SetSystemProperty(tmp, false);

            // インポート処理の作成者変更
            tmp.CREATE_USER = "MANIINPORT";
            // インポート処理の更新者変更
            tmp.UPDATE_USER = "MANIINPORT";

            list.Add(tmp);
            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        /// <summary>
        /// 更新用前回値のT_MANIFEST_RET_DATEを取得
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <returns></returns>
        private T_MANIFEST_RET_DATE GetPreManifestRetDate(long lSysId, int iSeq)
        {
            if (lSysId == 0 || iSeq <= 1)
            {
                return null;
            }

            T_MANIFEST_RET_DATE searchDto = new T_MANIFEST_RET_DATE();
            searchDto.SYSTEM_ID = Convert.ToInt64(lSysId);
            searchDto.SEQ = Convert.ToInt32(iSeq - 1);
            // 更新対象の前回値を取得
            T_MANIFEST_RET_DATE retDto = this.manifestRetDateDao.GetDataByPrimaryKey(searchDto);

            // 指定された項目のみ前回値を設定
            T_MANIFEST_RET_DATE entryDto = new T_MANIFEST_RET_DATE();

            // A票
            entryDto.SEND_A = retDto.SEND_A;
            // B1票
            entryDto.SEND_B1 = retDto.SEND_B1;
            // B2票
            entryDto.SEND_B2 = retDto.SEND_B2;
            // B4票
            entryDto.SEND_B4 = retDto.SEND_B4;
            // B6票
            entryDto.SEND_B6 = retDto.SEND_B6;
            // C1票
            entryDto.SEND_C1 = retDto.SEND_C1;
            // C2票
            entryDto.SEND_C2 = retDto.SEND_C2;
            // D票
            entryDto.SEND_D = retDto.SEND_D;
            // E票
            entryDto.SEND_E = retDto.SEND_E;

            return entryDto;
        }

        #endregion データ作成処理(マニパターン)

        #region データ作成処理(マニパターン建廃)

        /// <summary>
        /// マニ印字_建廃_形状(T_MANIFEST_KP_KEIJYOU)データ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="tmp">T_MANIFEST_KP_KEIJYOUのDTO</param>
        /// <param name="ino">印字番号</param>
        /// <param name="cd">廃棄物種類CD</param>
        /// <param name="name">廃棄物種類名称</param>
        /// <param name="check">印字指定</param>
        private void MakeManifestKeijyou(long lSysId, int iSeq, ref T_MANIFEST_KP_KEIJYOU tmp, int ino, string cd, string name, string check)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp, ino, cd, name, check);

            // システムID
            tmp.SYSTEM_ID = lSysId;
            // 枝番	
            tmp.SEQ = iSeq;
            // 印字番号
            tmp.REC_NO = Convert.ToInt16(ino);
            // 廃棄物種類CD
            if (!string.IsNullOrEmpty(cd))
            {
                tmp.KEIJOU_CD = cd.PadLeft(2, '0').ToUpper();
            }
            // 廃棄物種類名
            if (!string.IsNullOrEmpty(name))
            {
                tmp.KEIJOU_NAME = name;
            }
            else if (!string.IsNullOrEmpty(cd))
            {
                tmp.KEIJOU_NAME = this.ChkKeijou(cd);
            }
            //印刷指定
            tmp.PRT_FLG = ToBoolean(check);

            var who = new DataBinderLogic<T_MANIFEST_KP_KEIJYOU>(tmp);
            who.SetSystemProperty(tmp, false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニ印字_建廃_形状(T_MANIFEST_KP_KEIJYOU)リストデータ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="list">T_MANIFEST_KP_KEIJYOUのDTO</param>
        /// <param name="listdenpyou">CSVデータ</param>
        private void MakeManifestKeijyouList(long lSysId, int iSeq, ref  List<T_MANIFEST_KP_KEIJYOU> list, string[] listdenpyou)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list, listdenpyou);

            T_MANIFEST_KP_KEIJYOU tmp = null;
            int icnt = 1;

            //マニ印字_建廃_形状(T_MANIFEST_KP_KEIJYOU)
            var search = new CommonSerchParameterDtoCls();
            search.SYSTEM_ID = lSysId.ToString();
            search.SEQ = (iSeq - 1).ToString();
            DataTable dtKpKeijyou = this.KeijyouDao.GetDataForEntity(search);

            // 初回登録か判定
            bool isFirst = false;
            if (iSeq == 1 || dtKpKeijyou.Rows.Count == 0)
            {
                isFirst = true;
            }

            string check = string.Empty;
            // CSVに項目設定 or 仮登録(前回SEQ)値があれば登録用データ作成
            DataTable keujou = this.GetKeijou(dtKpKeijyou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[29]) && ToBoolean(Convert.ToString(listdenpyou[29])) && isFirst)
                || (keujou != null && keujou.Rows.Count > 0))
            {
                check = Convert.ToString(listdenpyou[29]);
                if (keujou != null && keujou.Rows.Count > 0)
                {
                    check = Convert.ToString(keujou.Rows[0]["PRT_FLG"]);
                }
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, icnt, "1", "固形状", check);
                list.Add(tmp);
            }
            icnt++;

            keujou = this.GetKeijou(dtKpKeijyou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[30]) && ToBoolean(Convert.ToString(listdenpyou[30])) && isFirst)
                || (keujou != null && keujou.Rows.Count > 0))
            {
                check = Convert.ToString(listdenpyou[30]);
                if (keujou != null && keujou.Rows.Count > 0)
                {
                    check = Convert.ToString(keujou.Rows[0]["PRT_FLG"]);
                }
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, icnt, "2", "泥状", check);
                list.Add(tmp);
            }
            icnt++;

            keujou = this.GetKeijou(dtKpKeijyou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[31]) && ToBoolean(Convert.ToString(listdenpyou[31])) && isFirst)
                || (keujou != null && keujou.Rows.Count > 0))
            {
                check = Convert.ToString(listdenpyou[31]);
                if (keujou != null && keujou.Rows.Count > 0)
                {
                    check = Convert.ToString(keujou.Rows[0]["PRT_FLG"]);
                }
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, icnt, "3", "液体状", check);
                list.Add(tmp);
            }
            icnt++;

            string name = string.Empty;
            string cd = string.Empty;
            keujou = this.GetKeijou(dtKpKeijyou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[32]) && ToBoolean(Convert.ToString(listdenpyou[32])) && isFirst)
                || (!string.IsNullOrEmpty(listdenpyou[33]) && isFirst)
                || (keujou != null && keujou.Rows.Count > 0))
            {
                check = Convert.ToString(listdenpyou[32]);
                cd = Convert.ToString(listdenpyou[33]);
                if (keujou != null && keujou.Rows.Count > 0)
                {
                    cd = Convert.ToString(keujou.Rows[0]["KEIJOU_CD"]);
                    name = Convert.ToString(keujou.Rows[0]["KEIJOU_NAME"]);
                    check = Convert.ToString(keujou.Rows[0]["PRT_FLG"]);
                }
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, icnt, cd, name, check);
                list.Add(tmp);
            }
            icnt++;

            keujou = this.GetKeijou(dtKpKeijyou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[34]) && ToBoolean(Convert.ToString(listdenpyou[34])) && isFirst)
                || (!string.IsNullOrEmpty(listdenpyou[35]) && isFirst)
                || (keujou != null && keujou.Rows.Count > 0))
            {
                check = Convert.ToString(listdenpyou[34]);
                cd = Convert.ToString(listdenpyou[35]);
                name = string.Empty;
                if (keujou != null && keujou.Rows.Count > 0)
                {
                    cd = Convert.ToString(keujou.Rows[0]["KEIJOU_CD"]);
                    name = Convert.ToString(keujou.Rows[0]["KEIJOU_NAME"]);
                    check = Convert.ToString(keujou.Rows[0]["PRT_FLG"]);
                }
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, icnt, cd, name, check);
                list.Add(tmp);
            }
            icnt++;

            keujou = this.GetKeijou(dtKpKeijyou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[36]) && ToBoolean(Convert.ToString(listdenpyou[36])) && isFirst)
                || (!string.IsNullOrEmpty(listdenpyou[37]) && isFirst)
                || (keujou != null && keujou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_KEIJYOU();
                check = Convert.ToString(listdenpyou[36]);
                cd = Convert.ToString(listdenpyou[37]);
                name = string.Empty;
                if (keujou != null && keujou.Rows.Count > 0)
                {
                    cd = Convert.ToString(keujou.Rows[0]["KEIJOU_CD"]);
                    name = Convert.ToString(keujou.Rows[0]["KEIJOU_NAME"]);
                    check = Convert.ToString(keujou.Rows[0]["PRT_FLG"]);
                }
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, icnt, cd, name, check);
                list.Add(tmp);
            }
            icnt++;

            keujou = this.GetKeijou(dtKpKeijyou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[38]) && ToBoolean(Convert.ToString(listdenpyou[38])) && isFirst)
                || (!string.IsNullOrEmpty(listdenpyou[39]) && isFirst)
                || (keujou != null && keujou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_KEIJYOU();
                check = Convert.ToString(listdenpyou[38]);
                cd = Convert.ToString(listdenpyou[39]);
                name = string.Empty;
                if (keujou != null && keujou.Rows.Count > 0)
                {
                    cd = Convert.ToString(keujou.Rows[0]["KEIJOU_CD"]);
                    name = Convert.ToString(keujou.Rows[0]["KEIJOU_NAME"]);
                    check = Convert.ToString(keujou.Rows[0]["PRT_FLG"]);
                }
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, icnt, cd, name, check);
                list.Add(tmp);
            }
            icnt++;

            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        /// <summary>
        /// マニ印字_建廃_荷姿(T_MANIFEST_KP_NISUGATA)データ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="tmp">T_MANIFEST_KP_NISUGATAのDTO</param>
        /// <param name="ino">印字番号</param>
        /// <param name="cd">荷姿CD</param>
        /// <param name="name">荷姿名称</param>
        /// <param name="check">印字指定</param>
        private void MakeManifestNisugata(long lSysId, int iSeq, ref T_MANIFEST_KP_NISUGATA tmp, ref int ino, string cd, string name, string check)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp, ino, cd, name, check);

            // システムID
            tmp.SYSTEM_ID = lSysId;
            // 枝番	
            tmp.SEQ = iSeq;
            // 印字番号
            tmp.REC_NO = Convert.ToInt16(ino);
            // 荷姿CD
            if (!string.IsNullOrEmpty(cd))
            {
                tmp.NISUGATA_CD = cd.PadLeft(2, '0').ToUpper();
            }
            // 荷姿名
            if (!string.IsNullOrEmpty(name))
            {
                tmp.NISUGATA_NAME = name;
            }
            else if (!string.IsNullOrEmpty(cd))
            {
                tmp.NISUGATA_NAME = this.CheckNisugataCd(tmp.NISUGATA_CD);
            }
            //印刷指定
            tmp.PRT_FLG = ToBoolean(check);

            var who = new DataBinderLogic<T_MANIFEST_KP_NISUGATA>(tmp);
            who.SetSystemProperty(tmp, false);

            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp, ino, cd, name, check);
        }

        /// <summary>
        /// マニ印字_建廃_荷姿(T_MANIFEST_KP_NISUGATA)リストデータ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="list">T_MANIFEST_KP_NISUGATAのDTO</param>
        /// <param name="listdenpyou">CSVデータ</param>
        private void MakeManifestNisugataList(long lSysId, int iSeq, ref  List<T_MANIFEST_KP_NISUGATA> list, string[] listdenpyou)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list, listdenpyou);

            //マニ印字_建廃_荷姿(T_MANIFEST_KP_NISUGATA)データ作成
            T_MANIFEST_KP_NISUGATA tmp = null;
            int icnt = 1;

            //マニ印字_建廃_荷姿(T_MANIFEST_KP_NISUGATA)
            var search = new CommonSerchParameterDtoCls();
            search.SYSTEM_ID = lSysId.ToString();
            search.SEQ = (iSeq - 1).ToString();
            DataTable dtKpNisugata = this.NisugataDao.GetDataForEntity(search);

            string check = string.Empty;
            // 仮登録(前回SEQ)値があれば登録用データ作成
            DataTable nisugata = this.GetNisugata(dtKpNisugata, icnt);
            if (nisugata != null && nisugata.Rows.Count > 0)
            {
                check = Convert.ToString(nisugata.Rows[0]["PRT_FLG"]);
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, ref icnt, "1", "バラ", check);
                list.Add(tmp);
            }
            icnt++;

            nisugata = this.GetNisugata(dtKpNisugata, icnt);
            if (nisugata != null && nisugata.Rows.Count > 0)
            {
                check = Convert.ToString(nisugata.Rows[0]["PRT_FLG"]);
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, ref icnt, "2", "コンテナ", check);
                list.Add(tmp);
            }
            icnt++;

            nisugata = this.GetNisugata(dtKpNisugata, icnt);
            if (nisugata != null && nisugata.Rows.Count > 0)
            {
                check = Convert.ToString(nisugata.Rows[0]["PRT_FLG"]);
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, ref icnt, "3", "ドラム缶", check);
                list.Add(tmp);
            }
            icnt++;

            nisugata = this.GetNisugata(dtKpNisugata, icnt);
            if (nisugata != null && nisugata.Rows.Count > 0)
            {
                check = Convert.ToString(nisugata.Rows[0]["PRT_FLG"]);
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, ref icnt, "4", "袋", check);
                list.Add(tmp);
            }
            icnt++;

            string name = string.Empty;
            string cd = string.Empty;
            nisugata = this.GetNisugata(dtKpNisugata, icnt);
            if (nisugata != null && nisugata.Rows.Count > 0)
            {
                cd = Convert.ToString(nisugata.Rows[0]["NISUGATA_CD"]);
                name = Convert.ToString(nisugata.Rows[0]["NISUGATA_NAME"]);
                check = Convert.ToString(nisugata.Rows[0]["PRT_FLG"]);
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, ref icnt, cd, name, check);
                list.Add(tmp);
            }
            icnt++;

            nisugata = this.GetNisugata(dtKpNisugata, icnt);
            if (nisugata != null && nisugata.Rows.Count > 0)
            {
                cd = Convert.ToString(nisugata.Rows[0]["NISUGATA_CD"]);
                name = Convert.ToString(nisugata.Rows[0]["NISUGATA_NAME"]);
                check = Convert.ToString(nisugata.Rows[0]["PRT_FLG"]);
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, ref icnt, cd, name, check);
                list.Add(tmp);
            }
            icnt++;

            nisugata = this.GetNisugata(dtKpNisugata, icnt);
            if (nisugata != null && nisugata.Rows.Count > 0)
            {
                cd = Convert.ToString(nisugata.Rows[0]["NISUGATA_CD"]);
                name = Convert.ToString(nisugata.Rows[0]["NISUGATA_NAME"]);
                check = Convert.ToString(nisugata.Rows[0]["PRT_FLG"]);
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, ref icnt, cd, name, check);
                list.Add(tmp);
            }
            icnt++;

            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        /// <summary>
        /// マニ印字_建廃_処分方法(T_MANIFEST_KP_SBN_HOUHOU)データ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="tmp">T_MANIFEST_KP_SBN_HOUHOUのDTO</param>
        /// <param name="ino">件数</param>
        /// <param name="cd">処分方法CD</param>
        /// <param name="name">処分方法名称</param>
        /// <param name="check">印字指定</param>
        private void MakeManifestHouhou(long lSysId, int iSeq, ref T_MANIFEST_KP_SBN_HOUHOU tmp, int ino, string cd, string name, string check)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp, ino, cd, name, check);

            // システムID
            tmp.SYSTEM_ID = lSysId;

            // 枝番	
            tmp.SEQ = iSeq;

            // 件数
            tmp.REC_NO = Convert.ToInt16(ino);

            // 処分方法CD
            if (!string.IsNullOrEmpty(cd))
            {
                tmp.SHOBUN_HOUHOU_CD = cd.PadLeft(3, '0').ToUpper();
            }

            // 処分方法名
            if (!string.IsNullOrEmpty(name))
            {
                tmp.SHOBUN_HOUHOU_NAME = name;
            }
            else if (!string.IsNullOrEmpty(cd))
            {
                tmp.SHOBUN_HOUHOU_NAME = this.CheckShobunHouhouuCd(tmp.SHOBUN_HOUHOU_CD);
            }

            //印刷指定
            tmp.PRT_FLG = ToBoolean(check);

            var who = new DataBinderLogic<T_MANIFEST_KP_SBN_HOUHOU>(tmp);
            who.SetSystemProperty(tmp, false);
            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp, ino, cd, name, check);
        }

        /// <summary>
        /// マニ印字_建廃_処分方法(T_MANIFEST_KP_SBN_HOUHOU)リストデータ作成
        /// </summary>
        /// <param name="lSysId">システムID</param>
        /// <param name="iSeq">シーケンス番号</param>
        /// <param name="list">T_MANIFEST_KP_SBN_HOUHOUのDTO</param>
        /// <param name="listdenpyou">CSVデータ</param>
        private void MakeManifestHouhouList(long lSysId, int iSeq, ref  List<T_MANIFEST_KP_SBN_HOUHOU> list, string[] listdenpyou)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list, listdenpyou);

            //マニ印字_建廃_処分方法(T_MANIFEST_KP_SBN_HOUHOU)データ作成
            T_MANIFEST_KP_SBN_HOUHOU tmp = null;

            //中間処理
            //1項目：1.脱水
            int icnt = 1;

            //マニ印字_建廃_処分方法(T_MANIFEST_KP_SBN_HOUHOU)
            var search = new CommonSerchParameterDtoCls();
            search.SYSTEM_ID = lSysId.ToString();
            search.SEQ = (iSeq - 1).ToString();
            DataTable dtKpSbnHouhou = this.SbnHouhouDao.GetDataForEntity(search);

            string check = string.Empty;
            // 仮登録(前回SEQ)値があれば登録用データ作成
            DataTable sbnHouhou = this.GetSbnHouhou(dtKpSbnHouhou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[90]) && ToBoolean(Convert.ToString(listdenpyou[90])))
                || (sbnHouhou != null && sbnHouhou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                if (sbnHouhou != null && sbnHouhou.Rows.Count > 0)
                {
                    check = Convert.ToString(sbnHouhou.Rows[0]["PRT_FLG"]);
                }
                if ((!string.IsNullOrEmpty(listdenpyou[90]) && ToBoolean(Convert.ToString(listdenpyou[90]))))
                {
                    check = Convert.ToString(listdenpyou[90]);
                }
                MakeManifestHouhou(lSysId, iSeq, ref tmp, icnt, "1", "脱水", check);
                list.Add(tmp);
            }
            icnt++;

            sbnHouhou = this.GetSbnHouhou(dtKpSbnHouhou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[91]) && ToBoolean(Convert.ToString(listdenpyou[91])))
                || (sbnHouhou != null && sbnHouhou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                if (sbnHouhou != null && sbnHouhou.Rows.Count > 0)
                {
                    check = Convert.ToString(sbnHouhou.Rows[0]["PRT_FLG"]);
                }
                if ((!string.IsNullOrEmpty(listdenpyou[91]) && ToBoolean(Convert.ToString(listdenpyou[91]))))
                {
                    check = Convert.ToString(listdenpyou[91]);
                }
                MakeManifestHouhou(lSysId, iSeq, ref tmp, icnt, "2", "焼却", check);
                list.Add(tmp);
            }
            icnt++;

            sbnHouhou = this.GetSbnHouhou(dtKpSbnHouhou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[92]) && ToBoolean(Convert.ToString(listdenpyou[92])))
                || (sbnHouhou != null && sbnHouhou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                if (sbnHouhou != null && sbnHouhou.Rows.Count > 0)
                {
                    check = Convert.ToString(sbnHouhou.Rows[0]["PRT_FLG"]);
                }
                if ((!string.IsNullOrEmpty(listdenpyou[92]) && ToBoolean(Convert.ToString(listdenpyou[92]))))
                {
                    check = Convert.ToString(listdenpyou[92]);
                }
                MakeManifestHouhou(lSysId, iSeq, ref tmp, icnt, "3", "破砕", check);
                list.Add(tmp);
            }
            icnt++;

            string cd = string.Empty;
            sbnHouhou = this.GetSbnHouhou(dtKpSbnHouhou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[93]) && ToBoolean(Convert.ToString(listdenpyou[93])))
                || !string.IsNullOrEmpty(listdenpyou[94])
                || (sbnHouhou != null && sbnHouhou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                if (sbnHouhou != null && sbnHouhou.Rows.Count > 0)
                {
                    cd = Convert.ToString(sbnHouhou.Rows[0]["SHOBUN_HOUHOU_CD"]);
                    check = Convert.ToString(sbnHouhou.Rows[0]["PRT_FLG"]);
                }
                if (!string.IsNullOrEmpty(listdenpyou[94]))
                {
                    cd = Convert.ToString(listdenpyou[94]);
                }
                if ((!string.IsNullOrEmpty(listdenpyou[93]) && ToBoolean(Convert.ToString(listdenpyou[93]))))
                {
                    check = Convert.ToString(listdenpyou[93]);
                }
                MakeManifestHouhou(lSysId, iSeq, ref tmp, icnt, cd, string.Empty, check);
                list.Add(tmp);
            }
            icnt++;

            sbnHouhou = this.GetSbnHouhou(dtKpSbnHouhou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[95]) && ToBoolean(Convert.ToString(listdenpyou[95])))
                || !string.IsNullOrEmpty(listdenpyou[96])
                || (sbnHouhou != null && sbnHouhou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                if (sbnHouhou != null && sbnHouhou.Rows.Count > 0)
                {
                    cd = Convert.ToString(sbnHouhou.Rows[0]["SHOBUN_HOUHOU_CD"]);
                    check = Convert.ToString(sbnHouhou.Rows[0]["PRT_FLG"]);
                }
                if (!string.IsNullOrEmpty(listdenpyou[96]))
                {
                    cd = Convert.ToString(listdenpyou[96]);
                }
                if ((!string.IsNullOrEmpty(listdenpyou[95]) && ToBoolean(Convert.ToString(listdenpyou[95]))))
                {
                    check = Convert.ToString(listdenpyou[95]);
                }
                MakeManifestHouhou(lSysId, iSeq, ref tmp, icnt, cd, string.Empty, check);
                list.Add(tmp);
            }
            icnt++;

            sbnHouhou = this.GetSbnHouhou(dtKpSbnHouhou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[97]) && ToBoolean(Convert.ToString(listdenpyou[97])))
                || !string.IsNullOrEmpty(listdenpyou[98])
                || (sbnHouhou != null && sbnHouhou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                if (sbnHouhou != null && sbnHouhou.Rows.Count > 0)
                {
                    cd = Convert.ToString(sbnHouhou.Rows[0]["SHOBUN_HOUHOU_CD"]);
                    check = Convert.ToString(sbnHouhou.Rows[0]["PRT_FLG"]);
                }
                if (!string.IsNullOrEmpty(listdenpyou[98]))
                {
                    cd = Convert.ToString(listdenpyou[98]);
                }
                if ((!string.IsNullOrEmpty(listdenpyou[97]) && ToBoolean(Convert.ToString(listdenpyou[97]))))
                {
                    check = Convert.ToString(listdenpyou[97]);
                }
                MakeManifestHouhou(lSysId, iSeq, ref tmp, icnt, cd, string.Empty, check);
                list.Add(tmp);
            }
            icnt++;

            sbnHouhou = this.GetSbnHouhou(dtKpSbnHouhou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[99]) && ToBoolean(Convert.ToString(listdenpyou[99])))
                || !string.IsNullOrEmpty(listdenpyou[100])
                || (sbnHouhou != null && sbnHouhou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                if (sbnHouhou != null && sbnHouhou.Rows.Count > 0)
                {
                    cd = Convert.ToString(sbnHouhou.Rows[0]["SHOBUN_HOUHOU_CD"]);
                    check = Convert.ToString(sbnHouhou.Rows[0]["PRT_FLG"]);
                }
                if (!string.IsNullOrEmpty(listdenpyou[100]))
                {
                    cd = Convert.ToString(listdenpyou[100]);
                }
                if ((!string.IsNullOrEmpty(listdenpyou[99]) && ToBoolean(Convert.ToString(listdenpyou[99]))))
                {
                    check = Convert.ToString(listdenpyou[99]);
                }
                MakeManifestHouhou(lSysId, iSeq, ref tmp, icnt, cd, string.Empty, check);
                list.Add(tmp);
            }
            icnt++;

            sbnHouhou = this.GetSbnHouhou(dtKpSbnHouhou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[101]) && ToBoolean(Convert.ToString(listdenpyou[101])))
                || !string.IsNullOrEmpty(listdenpyou[102])
                || (sbnHouhou != null && sbnHouhou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                if (sbnHouhou != null && sbnHouhou.Rows.Count > 0)
                {
                    cd = Convert.ToString(sbnHouhou.Rows[0]["SHOBUN_HOUHOU_CD"]);
                    check = Convert.ToString(sbnHouhou.Rows[0]["PRT_FLG"]);
                }
                if (!string.IsNullOrEmpty(listdenpyou[102]))
                {
                    cd = Convert.ToString(listdenpyou[102]);
                }
                if ((!string.IsNullOrEmpty(listdenpyou[101]) && ToBoolean(Convert.ToString(listdenpyou[101]))))
                {
                    check = Convert.ToString(listdenpyou[101]);
                }
                MakeManifestHouhou(lSysId, iSeq, ref tmp, icnt, cd, string.Empty, check);
                list.Add(tmp);
            }
            icnt++;

            //最終処分
            //9項目：1.安定型
            icnt = 11;
            sbnHouhou = this.GetSbnHouhou(dtKpSbnHouhou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[103]) && ToBoolean(Convert.ToString(listdenpyou[103])))
                || (sbnHouhou != null && sbnHouhou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                if (sbnHouhou != null && sbnHouhou.Rows.Count > 0)
                {
                    check = Convert.ToString(sbnHouhou.Rows[0]["PRT_FLG"]);
                }
                if ((!string.IsNullOrEmpty(listdenpyou[103]) && ToBoolean(Convert.ToString(listdenpyou[103]))))
                {
                    check = Convert.ToString(listdenpyou[103]);
                }
                MakeManifestHouhou(lSysId, iSeq, ref tmp, icnt, "1", "安定型", check);
                list.Add(tmp);
            }


            //10項目：2.管理型
            icnt = 12;
            sbnHouhou = this.GetSbnHouhou(dtKpSbnHouhou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[104]) && ToBoolean(Convert.ToString(listdenpyou[104])))
                || (sbnHouhou != null && sbnHouhou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                if (sbnHouhou != null && sbnHouhou.Rows.Count > 0)
                {
                    check = Convert.ToString(sbnHouhou.Rows[0]["PRT_FLG"]);
                }
                if ((!string.IsNullOrEmpty(listdenpyou[104]) && ToBoolean(Convert.ToString(listdenpyou[104]))))
                {
                    check = Convert.ToString(listdenpyou[104]);
                }
                MakeManifestHouhou(lSysId, iSeq, ref tmp, icnt, "2", "管理型", check);
                list.Add(tmp);
            }


            //11項目：3.遮断型
            icnt = 13;
            sbnHouhou = this.GetSbnHouhou(dtKpSbnHouhou, icnt);
            if ((!string.IsNullOrEmpty(listdenpyou[105]) && ToBoolean(Convert.ToString(listdenpyou[105])))
                || (sbnHouhou != null && sbnHouhou.Rows.Count > 0))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                if (sbnHouhou != null && sbnHouhou.Rows.Count > 0)
                {
                    check = Convert.ToString(sbnHouhou.Rows[0]["PRT_FLG"]);
                }
                if ((!string.IsNullOrEmpty(listdenpyou[105]) && ToBoolean(Convert.ToString(listdenpyou[105]))))
                {
                    check = Convert.ToString(listdenpyou[105]);
                }
                MakeManifestHouhou(lSysId, iSeq, ref tmp, icnt, "3", "遮断型", check);
                list.Add(tmp);
            }

            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }
        #endregion　データ作成処理(マニパターン建廃)

        #region 紐付情報のインサート処理
        /// <summary>
        /// 紐付情報のインサート処理
        /// </summary>
        /// <param name="lstRelation"></param>
        /// <returns></returns>
        [Transaction]
        public virtual int RegistRelationInfo(List<RelationInfo_DTOCls> lstRelationInfo)
        {
            LogUtility.DebugMethodStart(lstRelationInfo);

            try
            {
                //新紐付作成
                var newRelation = new List<T_MANIFEST_RELATION>();
                var tme = new Dictionary<SqlInt64, T_MANIFEST_ENTRY>(); //複数明細の場合1つで良いので重複排除が必要
                var r18exIns = new List<DT_R18_EX>();  //電子は1明細なので重複考慮不要だが　更新情報があるのでInsとDelの二種必要
                var r18exUpd = new List<DT_R18_EX>();

                var listFirstDetailForUpdate = new List<T_MANIFEST_DETAIL>();

                bool currentFlg = false;
                int cnt = 0;
                //whoカラム更新用
                var bind = new r_framework.Logic.DataBinderLogic<SuperEntity>(null as SuperEntity);

                foreach (var r in lstRelationInfo)
                {
                    if (this.currentRelation != null && !currentFlg)
                    {
                        foreach (T_MANIFEST_RELATION entity in this.currentRelation)
                        {
                            // 引用伝達を回避
                            T_MANIFEST_RELATION item = relationInfoDto.ManifestRelationClone(entity);
                            if (UIConstans.HAIKI_KBN_DENSHI.Equals(Convert.ToInt16(r.SECOND_MANI_KBN)))
                            {
                                if (r.SECOND_SYSTEM_ID.Equals(entity.NEXT_SYSTEM_ID))
                                {
                                    cnt++;
                                    item.SEQ = r.RELATION_SEQ.IsNull ? 1 : r.RELATION_SEQ;
                                    item.REC_SEQ = cnt;
                                    bind.SetSystemProperty(item, false);
                                    item.UPDATE_USER = "MANIRELATION";
                                    newRelation.Add(item);
                                }
                            }
                            else
                            {
                                if (r.SECOND_DETAIL_SYSTEM_ID.Equals(entity.NEXT_SYSTEM_ID))
                                {
                                    cnt++;
                                    item.SEQ = r.RELATION_SEQ.IsNull ? 1 : r.RELATION_SEQ;
                                    item.REC_SEQ = cnt;
                                    bind.SetSystemProperty(item, false);
                                    item.UPDATE_USER = "MANIRELATION";
                                    newRelation.Add(item);
                                }
                            }
                        }
                        currentFlg = true;
                    }
                    cnt++;
                    DataTable SecondPaperInfo = new DataTable();
                    if (UIConstans.HAIKI_KBN_DENSHI.Equals(Convert.ToInt16(r.SECOND_MANI_KBN)))
                    {
                        SecondPaperInfo = PaperAndElecManiDao.GetDataForEntitySecondForElecMani(r.SECOND_KANRI_ID);
                    }
                    else
                    {
                        SecondPaperInfo = PaperAndElecManiDao.GetDataForEntitySecond(r.SECOND_DETAIL_SYSTEM_ID);
                    }

                    T_MANIFEST_RELATION rel = new T_MANIFEST_RELATION();
                    if (UIConstans.HAIKI_KBN_DENSHI.Equals(Convert.ToInt16(r.SECOND_MANI_KBN)))
                    {
                        rel.NEXT_SYSTEM_ID = r.SECOND_SYSTEM_ID;
                    }
                    else
                    {
                        rel.NEXT_SYSTEM_ID = r.SECOND_DETAIL_SYSTEM_ID;
                    }
                    rel.SEQ = r.RELATION_SEQ.IsNull ? 1 : r.RELATION_SEQ;
                    rel.REC_SEQ = cnt;
                    rel.NEXT_HAIKI_KBN_CD = SqlInt16.Parse(r.SECOND_MANI_KBN);
                    rel.FIRST_HAIKI_KBN_CD = SqlInt16.Parse(r.MANIFEST_TYPE);
                    bind.SetSystemProperty(rel, false);
                    rel.CREATE_USER = "MANIRELATION";
                    rel.UPDATE_USER = "MANIRELATION";

                    // 電マニの場合は最終処分終了日等は別機能で登録させるため紐付機能では更新しない。
                    if (!UIConstans.HAIKI_KBN_DENSHI.Equals(Convert.ToInt16(r.MANIFEST_TYPE)))
                    {
                        var dtl = manifestDetailDao.GetDataForEntity(r.TME_SYSTEM_ID, r.TME_SEQ, SqlInt64.Parse(r.FIRST_SYSTEM_ID));
                        // 最終処分終了日
                        if (!string.IsNullOrEmpty(SecondPaperInfo.Rows[0]["LAST_SBN_END_DATE"].ToString()))
                        {
                            DateTime date;
                            if (DateTime.TryParseExact(SecondPaperInfo.Rows[0]["LAST_SBN_END_DATE"].ToString(),
                                                "yyyyMMdd",
                                                System.Globalization.CultureInfo.InvariantCulture,
                                                System.Globalization.DateTimeStyles.None,
                                                out date))
                            {
                                // 電マニの最終処分日付の形式にあわせる
                                dtl.LAST_SBN_END_DATE = date;
                            }
                            else
                            {
                                dtl.LAST_SBN_END_DATE = Convert.ToDateTime(SecondPaperInfo.Rows[0]["LAST_SBN_END_DATE"].ToString());
                            }
                        }
                        else
                        {
                            dtl.LAST_SBN_END_DATE = SqlDateTime.Null;
                        }

                        if (int.Parse(SecondPaperInfo.Rows[0]["LINE_COUNT"].ToString()) <= 1)
                        {
                            // 最終処分業者
                            if (!string.IsNullOrEmpty(SecondPaperInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString()))
                            {
                                dtl.LAST_SBN_GYOUSHA_CD = SecondPaperInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
                            }
                            else
                            {
                                dtl.LAST_SBN_GYOUSHA_CD = null;
                            }

                            // 最終処分現場
                            if (!string.IsNullOrEmpty(SecondPaperInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString()))
                            {
                                dtl.LAST_SBN_GENBA_CD = SecondPaperInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString();
                            }
                            else
                            {
                                dtl.LAST_SBN_GENBA_CD = null;
                            }
                        }
                        else
                        {
                            // 二次マニフェストの実績が2行以上ある場合
                            dtl.LAST_SBN_GYOUSHA_CD = null;
                            dtl.LAST_SBN_GENBA_CD = null;
                            SqlInt64 SystemID = SqlInt64.Parse(SecondPaperInfo.Rows[0]["SYSTEM_ID"].ToString());

                            // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                            SqlInt64 DetailSystemID = SqlInt64.Parse(SecondPaperInfo.Rows[0]["DETAIL_SYSTEM_ID"].ToString());
                            // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end

                            SqlInt32 SEQ = SqlInt32.Parse(SecondPaperInfo.Rows[0]["SEQ"].ToString());
                            DataTable SecondPaperLastsbnInfo = new DataTable();
                            if (UIConstans.HAIKI_KBN_DENSHI.Equals(Convert.ToInt16(r.MANIFEST_TYPE)))
                            {
                                // 電マニ
                                SecondPaperLastsbnInfo = PaperAndElecManiDao.GetDataForEntitySecondLastSbnForElecMani(SystemID, SEQ);
                            }
                            else
                            {
                                // 紙マニ
                                SecondPaperLastsbnInfo = PaperAndElecManiDao.GetDataForEntitySecondLastSbn(DetailSystemID, SEQ);
                            }
                            if (SecondPaperLastsbnInfo.Rows.Count <= 1)
                            {
                                // 全ての行で最終処分業者、最終処分場所が一致している場合
                                if (SecondPaperLastsbnInfo.Rows.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString())
                                        && !string.IsNullOrEmpty(SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString()))
                                    {
                                        dtl.LAST_SBN_GYOUSHA_CD = SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
                                        dtl.LAST_SBN_GENBA_CD = SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                // 最終処分業者、最終処分場所が異なる行がある場合
                                DataView SecondInfoDataView = new DataView(SecondPaperLastsbnInfo);
                                DataTable tblLastSbnGyousha = SecondInfoDataView.ToTable("DistinctTable", true, new string[] { "LAST_SBN_GYOUSHA_CD" });
                                if (tblLastSbnGyousha.Rows.Count <= 1)
                                {
                                    // 最終処分業者が全て同じ場合
                                    dtl.LAST_SBN_GYOUSHA_CD = tblLastSbnGyousha.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
                                }
                            }
                        }
                        //二次マニフェストは原本もしくは一件実績しかない場合
                        listFirstDetailForUpdate.Add(dtl);

                        //紙はシステムIDをそのまま利用
                        rel.FIRST_SYSTEM_ID = SqlInt64.Parse(r.FIRST_SYSTEM_ID);
                        //ENTRY作成
                        if (!tme.ContainsKey(r.TME_SYSTEM_ID))
                        {
                            tme.Add(r.TME_SYSTEM_ID, new T_MANIFEST_ENTRY()
                            {
                                SYSTEM_ID = r.TME_SYSTEM_ID,
                                SEQ = r.TME_SEQ,
                                TIME_STAMP = r.TME_TIME_STAMP,
                                MANIFEST_ID = r.MANIFEST_ID
                            });

                        }
                    }
                    else
                    {

                        //電子はR18EXを作り直すのでSYSTEM_IDを取り直す

                        //旧18
                        if (!r.DT_R18_EX_SYSTEM_ID.IsNull)
                        {
                            #region /*NHU MOD 20160812 #20631 */ merge
                            //r18exUpd.Add(new DT_R18_EX()
                            //{
                            //    SYSTEM_ID = r.DT_R18_EX_SYSTEM_ID,
                            //    SEQ = r.DT_R18_EX_SEQ,
                            //    KANRI_ID = r.KANRI_ID,
                            //    TIME_STAMP = r.DT_R18_EX_TIME_STAMP,
                            //});
                            #endregion
                            rel.FIRST_SYSTEM_ID = r.DT_R18_EX_SYSTEM_ID;
                        }
                        else
                        {
                            //新18
                            var newR18EX = new DT_R18_EX();
                            //システムIDの採番
                            Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();
                            newR18EX.SYSTEM_ID = dba.createSystemId((int)DENSHU_KBN.DENSHI_MANIFEST);
                            newR18EX.SEQ = 1;
                            newR18EX.KANRI_ID = r.KANRI_ID; ;
                            newR18EX.MANIFEST_ID = r.MANIFEST_ID;
                            newR18EX.HAIKI_NAME_CD = r.HAIKI_NAME_CD;  //廃棄物名称CD
                            newR18EX.KANSAN_SUU = string.IsNullOrEmpty(r.KANSAN_SUU) ? SqlDecimal.Null : Convert.ToDecimal(r.KANSAN_SUU);  //換算後数量

                            r18exIns.Add(newR18EX);

                            rel.FIRST_SYSTEM_ID = newR18EX.SYSTEM_ID; //採番した値

                        }
                    }

                    newRelation.Add(rel);
                }

                this.regist_relations = newRelation.ToArray();
                this.paperEntries = tme.Values.ToArray();
                this.elecEntriesIns = r18exIns.ToArray();
                this.elecEntriesUpd = r18exUpd.ToArray();
                this.delete_relations = this.currentRelation;

                //登録
                using (Transaction tran = new Transaction())
                {
                    this.HimodukeRegist(tran);

                    // 一次マニフェスト情報明細更新（最終処分場所、最終処分終了日更新）
                    if (listFirstDetailForUpdate != null)
                    {
                        foreach (var r in listFirstDetailForUpdate)
                        {
                            manifestDetailDao.Update(r);
                        }
                    }
                    tran.Commit();
                }

                // 一次電マニの最終処分情報更新
                this.UpdateLastSbnInfoForFirstElecMani(this.currentRelation, lstRelationInfo);

                LogUtility.DebugMethodEnd(0);
                return 0;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd(lstRelationInfo.Count);
            return lstRelationInfo.Count;
        }
        #endregion

        #region 一次電マニの最終処分情報更新
        /// <summary>
        /// 一次電マニの最終処分情報更新
        /// 既に紐付いているマニ、画面で新たに紐付けたマニの両方を更新する
        /// </summary>
        /// <param name="currentRelation">既に紐付いているマニ</param>
        /// <param name="lstRelationInfo">画面で選択したマニ</param>
        internal void UpdateLastSbnInfoForFirstElecMani(T_MANIFEST_RELATION[] currentRelation, List<RelationInfo_DTOCls> lstRelationInfo)
        {
            #region 一次電マニ更新用

            // 更新用変数
            var mfTocList = new List<DT_MF_TOC>();
            var r18List = new List<DT_R18>();
            var r19List = new List<DT_R19[]>();
            var r02List = new List<DT_R02[]>();
            var r04List = new List<DT_R04[]>();
            var r05List = new List<DT_R05[]>();
            var r08List = new List<DT_R08[]>();
            var R13List = new List<DT_R13[]>();
            var r18ExList = new List<DT_R18_EX>();
            var r19ExList = new List<DT_R19_EX[]>();
            var r04ExList = new List<DT_R04_EX[]>();
            var r08ExList = new List<DT_R08_EX[]>();
            var oldR13ExList = new List<DT_R13_EX[]>();

            var newR13ExList = new List<DT_R13_EX[]>();

            // 一次電マニの重複更新防止用変数
            var executedKanriIds = new List<string>();
            #region 電マニ用Dao生成
            var mfTocDao = DaoInitUtility.GetComponent<CommonDT_MF_TOCDaoCls>();
            var r18Dao = DaoInitUtility.GetComponent<CommonDT_R18DaoCls>();
            var r19Dao = DaoInitUtility.GetComponent<CommonDT_R19DaoCls>();
            var r02Dao = DaoInitUtility.GetComponent<CommonDT_R02DaoCls>();
            var r04Dao = DaoInitUtility.GetComponent<CommonDT_R04DaoCls>();
            var r05Dao = DaoInitUtility.GetComponent<CommonDT_R05DaoCls>();
            var r06Dao = DaoInitUtility.GetComponent<CommonDT_R06DaoCls>();
            var r08Dao = DaoInitUtility.GetComponent<CommonDT_R08DaoCls>();
            var r13Dao = DaoInitUtility.GetComponent<CommonDT_R13DaoCls>();
            var r18ExDao = DaoInitUtility.GetComponent<CommonDT_R18_EXDaoCls>();
            var r19ExDao = DaoInitUtility.GetComponent<CommonDT_R19_EXDaoCls>();
            var r04ExDao = DaoInitUtility.GetComponent<CommonDT_R04_EXDaoCls>();
            var r08ExDao = DaoInitUtility.GetComponent<CommonDT_R08_EXDaoCls>();
            var r13ExDao = DaoInitUtility.GetComponent<CommonDT_R13_EXDaoCls>();
            #endregion

            #endregion

            foreach (var r in lstRelationInfo)
            {
                // 電マニの場合は最終処分終了日等は別機能で登録させるため紐付機能では更新しない。
                if (UIConstans.HAIKI_KBN_DENSHI.Equals(Convert.ToInt16(r.MANIFEST_TYPE)))
                {
                    #region 一次電マニ更新用
                    // 一次電マニの更新情報をセット
                    if (!r.DT_R18_EX_SYSTEM_ID.IsNull
                        && !r.DT_R18_EX_SEQ.IsNull
                        && !string.IsNullOrEmpty(r.KANRI_ID)
                        && !executedKanriIds.Contains(r.KANRI_ID))
                    {
                        executedKanriIds.Add(r.KANRI_ID);
                        DT_R18_EX oldR18Ex = r18ExDao.GetDataForSystemId(r.DT_R18_EX_SYSTEM_ID);

                        // 一次電マニの場合は紙マニと違い、一括で紐付いている二次マニを参照してDT_R13, DT_R13_EXを作成しないとならい
                        #region 現在のデータを取得
                        DT_MF_TOC mfToc = mfTocDao.GetDataForEntity(new DT_MF_TOC() { KANRI_ID = r.KANRI_ID });
                        SqlDecimal latestSeq = mfToc.LATEST_SEQ;
                        DT_R18 r18 = r18Dao.GetDataForEntity(new DT_R18() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R19[] r19 = r19Dao.GetDataForEntity(new DT_R19() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R02[] r02 = r02Dao.GetDataForEntity(new DT_R02() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R04[] r04 = r04Dao.GetDataForEntity(new DT_R04() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R05[] r05 = r05Dao.GetDataForEntity(new DT_R05() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R08[] r08 = r08Dao.GetDataForEntity(new DT_R08() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R13[] r13 = r13Dao.GetDataForEntity(new DT_R13() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R19_EX[] oldR19Ex = r19ExDao.GetDataForEntity(new DT_R19_EX() { SYSTEM_ID = oldR18Ex.SYSTEM_ID, SEQ = oldR18Ex.SEQ });
                        DT_R04_EX[] oldR04Ex = r04ExDao.GetDataForEntity(new DT_R04_EX() { SYSTEM_ID = oldR18Ex.SYSTEM_ID, SEQ = oldR18Ex.SEQ });
                        DT_R08_EX[] oldR08Ex = r08ExDao.GetDataForEntity(new DT_R08_EX() { SYSTEM_ID = oldR18Ex.SYSTEM_ID, SEQ = oldR18Ex.SEQ });
                        DT_R13_EX[] oldR13Ex = r13ExDao.GetDataForEntity(new DT_R13_EX() { SYSTEM_ID = oldR18Ex.SYSTEM_ID, SEQ = oldR18Ex.SEQ });
                        #endregion

                        if (mfToc.KIND.IsNull
                            || mfToc.KIND != CommonConst.MF_TOC_KIND_NOT_EDI)//Sontt #5135 20150511 Merge 48547
                        {
                            // 一次電マニがNot EDI(手動)以外の場合、最終処分情報は更新しない。
                            // Not EDI(手動)以外の場合は[3] 最終処分終了報告ボタンから最終処分情報を更新する。
                            continue;
                        }

                        SqlDecimal updateLatestSeq = mfToc.LATEST_SEQ + 1;
                        SqlInt32 updateExSeq = oldR18Ex.SEQ + 1;

                        // DT_R13, DT_R13_EXの更新データセット
                        this.CreateUpdateElecMani(r.KANRI_ID, oldR18Ex.SYSTEM_ID, r.DT_R18_EX_SYSTEM_ID, updateLatestSeq, updateExSeq, r18, oldR13Ex,
                                                ref r18List, ref R13List, ref oldR13ExList, ref newR13ExList);

                        #region DT_R13, DT_R13_EX以外の更新データをセット
                        mfToc.LATEST_SEQ = updateLatestSeq;
                        mfTocList.Add(mfToc);

                        foreach (var tempR19 in r19)
                        {
                            tempR19.SEQ = updateLatestSeq;
                        }
                        r19List.Add(r19);

                        foreach (var tempR02 in r02)
                        {
                            tempR02.SEQ = updateLatestSeq;
                        }
                        r02List.Add(r02);

                        foreach (var tempR04 in r04)
                        {
                            tempR04.SEQ = updateLatestSeq;
                        }
                        r04List.Add(r04);

                        foreach (var tempR05 in r05)
                        {
                            tempR05.SEQ = updateLatestSeq;
                        }
                        r05List.Add(r05);

                        foreach (var tempR08 in r08)
                        {
                            tempR08.SEQ = updateLatestSeq;
                        }
                        r08List.Add(r08);

                        r18ExList.Add(oldR18Ex);
                        r19ExList.Add(oldR19Ex);
                        r04ExList.Add(oldR04Ex);
                        r08ExList.Add(oldR08Ex);
                        #endregion
                    }
                    #endregion
                }
            }

            #region 紐付け削除時用データ作成
            if (this.currentRelation != null)
            {
                foreach (T_MANIFEST_RELATION r in currentRelation)
                {
                    if (r.FIRST_HAIKI_KBN_CD != 4) continue;

                    #region 現在のデータを取得
                    DT_R18_EX oldR18Ex = r18ExDao.GetDataForSystemId(r.FIRST_SYSTEM_ID);
                    //Start Sontt #5135 20150511 Merge 42476
                    if (oldR18Ex == null)
                    {
                        // DT_R18_EX.SYSTEM_IDと紐付け後、DT_R18_MIXを作成している場合、上記メソッドでは取得できないので
                        // DT_R18_EXを再取得
                        oldR18Ex = r18ExDao.GetDataForExSystemId(r.FIRST_SYSTEM_ID);
                    }

                    if (oldR18Ex == null)
                    {
                        // 取得できない場合は削除されている可能性があるため、スキップ
                        continue;
                    }
                    //End Sontt #5135 20150511 Merge 42476
                    string kanriId = oldR18Ex.KANRI_ID;

                    // 更新用の方で一度処理している場合は、重複して更新しまう
                    if (executedKanriIds.Contains(kanriId)) continue;

                    executedKanriIds.Add(kanriId);

                    SqlInt64 exSystemId = oldR18Ex.SYSTEM_ID;
                    SqlInt32 exSeq = oldR18Ex.SEQ;
                    DT_MF_TOC mfToc = mfTocDao.GetDataForEntity(new DT_MF_TOC() { KANRI_ID = kanriId });
                    SqlDecimal latestSeq = mfToc.LATEST_SEQ;
                    DT_R18 r18 = r18Dao.GetDataForEntity(new DT_R18() { KANRI_ID = kanriId, SEQ = latestSeq });
                    DT_R19[] r19 = r19Dao.GetDataForEntity(new DT_R19() { KANRI_ID = kanriId, SEQ = latestSeq });
                    DT_R02[] r02 = r02Dao.GetDataForEntity(new DT_R02() { KANRI_ID = kanriId, SEQ = latestSeq });
                    DT_R04[] r04 = r04Dao.GetDataForEntity(new DT_R04() { KANRI_ID = kanriId, SEQ = latestSeq });
                    DT_R05[] r05 = r05Dao.GetDataForEntity(new DT_R05() { KANRI_ID = kanriId, SEQ = latestSeq });
                    DT_R08[] r08 = r08Dao.GetDataForEntity(new DT_R08() { KANRI_ID = kanriId, SEQ = latestSeq });
                    DT_R13[] r13 = r13Dao.GetDataForEntity(new DT_R13() { KANRI_ID = kanriId, SEQ = latestSeq });
                    DT_R19_EX[] oldR19Ex = r19ExDao.GetDataForEntity(new DT_R19_EX() { SYSTEM_ID = exSystemId, SEQ = exSeq });
                    DT_R04_EX[] oldR04Ex = r04ExDao.GetDataForEntity(new DT_R04_EX() { SYSTEM_ID = exSystemId, SEQ = exSeq });
                    DT_R08_EX[] oldR08Ex = r08ExDao.GetDataForEntity(new DT_R08_EX() { SYSTEM_ID = exSystemId, SEQ = exSeq });
                    DT_R13_EX[] oldR13Ex = r13ExDao.GetDataForEntity(new DT_R13_EX() { SYSTEM_ID = exSystemId, SEQ = exSeq });
                    #endregion

                    //if (mfToc.KIND != CommonConst.MF_TOC_KIND_NOT_EDI)
                    if (mfToc.KIND.IsNull
                         || mfToc.KIND != CommonConst.MF_TOC_KIND_NOT_EDI)//Sontt #5135 20150511 Merge 48556
                    {
                        // 一次電マニがNot EDI(手動)以外の場合、最終処分情報は更新しない。
                        // Not EDI(手動)以外の場合は[3] 最終処分終了報告ボタンから最終処分情報を更新する。
                        continue;
                    }

                    SqlDecimal updateLatestSeq = mfToc.LATEST_SEQ + 1;
                    SqlInt32 updateExSeq = exSeq + 1;

                    // DT_R13, DT_R13_EXの更新データセット
                    this.CreateUpdateElecMani(kanriId, exSystemId, r.FIRST_SYSTEM_ID, updateLatestSeq, updateExSeq, r18, oldR13Ex,
                                            ref r18List, ref R13List, ref oldR13ExList, ref newR13ExList);

                    #region DT_R13, DT_R13_EX以外の更新データをセット
                    mfToc.LATEST_SEQ = updateLatestSeq;
                    mfTocList.Add(mfToc);

                    foreach (var tempR19 in r19)
                    {
                        tempR19.SEQ = updateLatestSeq;
                    }
                    r19List.Add(r19);

                    foreach (var tempR02 in r02)
                    {
                        tempR02.SEQ = updateLatestSeq;
                    }
                    r02List.Add(r02);

                    foreach (var tempR04 in r04)
                    {
                        tempR04.SEQ = updateLatestSeq;
                    }
                    r04List.Add(r04);

                    foreach (var tempR05 in r05)
                    {
                        tempR05.SEQ = updateLatestSeq;
                    }
                    r05List.Add(r05);

                    foreach (var tempR08 in r08)
                    {
                        tempR08.SEQ = updateLatestSeq;
                    }
                    r08List.Add(r08);

                    r18ExList.Add(oldR18Ex);
                    r19ExList.Add(oldR19Ex);
                    r04ExList.Add(oldR04Ex);
                    r08ExList.Add(oldR08Ex);
                    #endregion

                }
            }
            #endregion

            using (Transaction tran = new Transaction())
            {
                // 一次電マニ情報更新
                this.UpdateFirstElecMani(mfTocList, r18List, r19List, r02List, r04List, r05List, r08List, R13List
                    , r18ExList, r19ExList, r04ExList, r08ExList, oldR13ExList, newR13ExList);
                tran.Commit();
            }
        }
        #endregion

        #region 電マニ更新用データ作成
        /// <summary>
        /// 電マニ更新用データ作成
        /// DT_R18, DT_R13, DT_R13_EXのデータを作成する
        /// </summary>
        /// <param name="kanriId">管理ID</param>
        /// <param name="exSystemId">DT_R18_EX.SYSTEM_ID</param>
        /// <param name="exOrMixSystemId">DT_R18_EX.SYSTEM_IDまたはDT_R18_MIX.DETAIL_SYSTEM_IDの有効なほう</param>
        /// <param name="updateLatestSeq">DT_MF_TOC.LATEST_SEQ + 1</param>
        /// <param name="updateExSeq">DT_R18_EX.SYSTEM_ID + 1</param>
        /// <param name="r18"></param>
        /// <param name="oldR13Ex"></param>
        /// <param name="r18List"></param>
        /// <param name="R13List"></param>
        /// <param name="oldR13ExList"></param>
        /// <param name="newR13ExList"></param>
        internal void CreateUpdateElecMani(string kanriId, SqlInt64 exSystemId, SqlInt64 exOrMixSystemId, SqlDecimal updateLatestSeq, SqlInt32 updateExSeq, DT_R18 r18, DT_R13_EX[] oldR13Ex,
                            ref List<DT_R18> r18List, ref List<DT_R13[]> R13List, ref List<DT_R13_EX[]> oldR13ExList, ref List<DT_R13_EX[]> newR13ExList)
        {
            var getManiRelDao = DaoInitUtility.GetComponent<GetManifestRelationDaoCls>();
            decimal lastSbnEndRepFlg = 0;

            // 二次マニ全件取得
            DataTable nextManis = new DataTable();
            nextManis = getManiRelDao.GetLastSbnInfoForNexttMani(exOrMixSystemId);

            // 業者、現場の一覧を生成
            var gyoushaAndGenbaList = nextManis.AsEnumerable().Select(result => new
            {
                SECOND_HAIKI_KBN_CD = result.Field<int>("SECOND_HAIKI_KBN_CD"),
                SECOND_SYS_ID = result.Field<long>("SECOND_SYSTEM_ID"),
                SECOND_DETAIL_SYS_ID = result.Field<decimal>("SECOND_DETAIL_SYSTEM_ID"),
                LAST_SBN_JOU_NAME = result.Field<string>("LAST_SBN_JOU_NAME"),
                LAST_SBN_JOU_ADDRESS = result.Field<string>("LAST_SBN_JOU_ADDRESS")
            }).
            GroupBy(gryoup => new { gryoup.SECOND_HAIKI_KBN_CD, gryoup.SECOND_SYS_ID, gryoup.SECOND_DETAIL_SYS_ID, gryoup.LAST_SBN_JOU_NAME, gryoup.LAST_SBN_JOU_ADDRESS });

            int recSeq = 1;
            // DT_R13_EX用のCreate情報をセット
            string createUser = oldR13Ex == null || oldR13Ex.Count() < 1 ? string.Empty : oldR13Ex[0].CREATE_USER;
            SqlDateTime createDate = oldR13Ex == null || oldR13Ex.Count() < 1 ? this.getDBDateTime() : oldR13Ex[0].CREATE_DATE;
            string createPc = oldR13Ex == null || oldR13Ex.Count() < 1 ? string.Empty : oldR13Ex[0].CREATE_PC;
            DateTime lastSbnEndDate = DateTime.MinValue;

            var commonManiLogic = new ManifestoLogic();
            var tempR13 = new List<DT_R13>();
            var tempR13Ex = new List<DT_R13_EX>();

            // 業者、現場毎に最終処分終了報告情報を生成
            foreach (var gyoushaAndGenbaRow in gyoushaAndGenbaList)
            {
                if (string.IsNullOrEmpty(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_NAME)
                    || string.IsNullOrEmpty(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_ADDRESS))
                {
                    // 最終処分場がない場合、データの作りようがないので、除外
                    continue;
                }

                // SQL Injectionが発生する可能性があるので、予約文字をエスケープ
                string lastSbnJouName = Regex.Replace(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_NAME.Replace("'", "''"), @"([\[\]*%])", "[$1]");
                string lastSbnJouAddress = Regex.Replace(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_ADDRESS.Replace("'", "''"), @"([\[\]*%])", "[$1]");

                DateTime tempLastSbnEndDate = DateTime.MinValue;
                var groupData = nextManis.Select(string.Format(
                            "SECOND_HAIKI_KBN_CD = {0} AND SECOND_SYSTEM_ID = {1} AND SECOND_DETAIL_SYSTEM_ID = {2} AND LAST_SBN_JOU_NAME = '{3}' AND LAST_SBN_JOU_ADDRESS = '{4}'"
                            , gyoushaAndGenbaRow.Key.SECOND_HAIKI_KBN_CD, gyoushaAndGenbaRow.Key.SECOND_SYS_ID, gyoushaAndGenbaRow.Key.SECOND_DETAIL_SYS_ID
                            , lastSbnJouName, lastSbnJouAddress)
                        );
                foreach (var tempRow in groupData)
                {
                    if (tempRow["LAST_SBN_END_DATE"] == null
                        || string.IsNullOrEmpty(tempRow["LAST_SBN_END_DATE"].ToString()))
                    {
                        // 最終処分終了日が設定されていないものがあれば最終処分未完了
                        tempLastSbnEndDate = DateTime.MinValue;
                        break;
                    }

                    // 一番新しい日付をセット
                    if (DateTime.Compare(tempLastSbnEndDate, DateTime.Parse(tempRow["LAST_SBN_END_DATE"].ToString())) < 0)
                    {
                        tempLastSbnEndDate = DateTime.Parse(tempRow["LAST_SBN_END_DATE"].ToString());
                    }
                }

                // 
                var firstManiR13 = new DT_R13();
                var firstManiR13EX = new DT_R13_EX();

                // 住所分割
                string tempAddress1;
                string tempAddress2;
                string tempAddress3;
                string tempAddress4;
                commonManiLogic.SetAddress1ToAddress4(groupData[0].Field<string>("LAST_SBN_JOU_ADDRESS"),
                    out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                // set keys
                firstManiR13.KANRI_ID = kanriId;
                firstManiR13.SEQ = updateLatestSeq;
                firstManiR13.REC_SEQ = recSeq;
                firstManiR13EX.KANRI_ID = kanriId;
                firstManiR13EX.SYSTEM_ID = exSystemId;
                firstManiR13EX.SEQ = updateExSeq;
                firstManiR13EX.REC_SEQ = recSeq;

                // DT_R18.LAST_SBN_END_DATE用の日付
                lastSbnEndDate = DateTime.Compare(lastSbnEndDate, tempLastSbnEndDate) < 0 ? tempLastSbnEndDate : lastSbnEndDate;

                // DT_R13
                firstManiR13.LAST_SBN_END_DATE = tempLastSbnEndDate.Equals(DateTime.MinValue) ? null : tempLastSbnEndDate.ToString("yyyyMMdd");
                firstManiR13.MANIFEST_ID = r18.MANIFEST_ID;
                firstManiR13.LAST_SBN_JOU_NAME = groupData[0].Field<string>("LAST_SBN_JOU_NAME");
                firstManiR13.LAST_SBN_JOU_POST = groupData[0].Field<string>("LAST_SBN_JOU_POST");
                firstManiR13.LAST_SBN_JOU_ADDRESS1 = tempAddress1;
                firstManiR13.LAST_SBN_JOU_ADDRESS2 = tempAddress2;
                firstManiR13.LAST_SBN_JOU_ADDRESS3 = tempAddress3;
                firstManiR13.LAST_SBN_JOU_ADDRESS4 = tempAddress4;
                firstManiR13.LAST_SBN_JOU_TEL = groupData[0].Field<string>("LAST_SBN_JOU_TEL");
                DateTime now = this.getDBDateTime();
                firstManiR13.CREATE_DATE = now;
                firstManiR13.UPDATE_TS = now;

                // DT_R13_EX
                firstManiR13EX.MANIFEST_ID = r18.MANIFEST_ID;
                firstManiR13EX.LAST_SBN_GYOUSHA_CD = groupData[0].Field<string>("LAST_SBN_GYOUSHA_CD");
                firstManiR13EX.LAST_SBN_GENBA_CD = groupData[0].Field<string>("LAST_SBN_GENBA_CD");
                firstManiR13EX.CREATE_USER = createUser;
                firstManiR13EX.CREATE_DATE = createDate;
                firstManiR13EX.CREATE_PC = createPc;
                firstManiR13EX.UPDATE_USER = SystemProperty.UserName;
                firstManiR13EX.UPDATE_DATE = now;
                firstManiR13EX.UPDATE_PC = SystemInformation.ComputerName;
                firstManiR13EX.DELETE_FLG = false;

                // DT_R13, DT_R13_EX追加
                tempR13.Add(firstManiR13);
                tempR13Ex.Add(firstManiR13EX);

                recSeq++;
            }

            var blankLastSbnJou = nextManis.Select("ISNULL(LAST_SBN_JOU_NAME, '') = '' OR ISNULL(LAST_SBN_JOU_ADDRESS, '') = ''");
            var blankLstSbnEndDate = nextManis.Select("LAST_SBN_END_DATE IS NULL");
            lastSbnEndRepFlg = (nextManis.Rows.Count > 0 && blankLastSbnJou.Count() < 1 && blankLstSbnEndDate.Count() < 1) ? 1 : 0;

            bool blankLastSbnEndDateFlg = false;
            for (int i = 0; i < tempR13.Count; i++)
            {
                var tmpLastSbnEndDate = tempR13[i].LAST_SBN_END_DATE;
                DateTime tempDate = DateTime.MinValue;

                if (tmpLastSbnEndDate != null
                    && !string.IsNullOrEmpty(tmpLastSbnEndDate.ToString()))
                {
                    if (DateTime.Compare(lastSbnEndDate, tempDate.Date) < 0)
                    {
                        lastSbnEndDate = tempDate.Date;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(tempR13Ex[i].LAST_SBN_GENBA_CD))
                    {
                        blankLastSbnEndDateFlg = true;
                        break;
                    }
                }
            }

            R13List.Add(tempR13.ToArray());
            newR13ExList.Add(tempR13Ex.ToArray());
            oldR13ExList.Add(oldR13Ex);

            r18.SEQ = updateLatestSeq;
            r18.LAST_SBN_ENDREP_FLAG = lastSbnEndRepFlg;
            r18.LAST_SBN_END_DATE = null;
            r18.LAST_SBN_END_REP_DATE = null;
            if (lastSbnEndRepFlg == 1)
            {
                r18.LAST_SBN_END_REP_DATE = this.parentForm.sysDate.ToString("yyyyMMdd");
            }
            if (!blankLastSbnEndDateFlg)
            {
                r18.LAST_SBN_END_DATE = lastSbnEndDate.Equals(DateTime.MinValue) ? null : lastSbnEndDate.ToString("yyyyMMdd");
            }
            else
            {
                r18.LAST_SBN_END_DATE = null;
            }
            r18List.Add(r18);
        }
        #endregion

        /// <summary>
        /// Boolean変換処理
        /// </summary>
        /// <param name="str">対象文字列</param>
        private static Boolean ToBoolean(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            String cleanValue = (str ?? "").Trim();
            if (String.Equals(cleanValue, "False", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                return
                    (String.Equals(cleanValue, "True", StringComparison.OrdinalIgnoreCase)) ||
                    (cleanValue != "0");
            }
        }

        /// <summary>
        /// 減容値計算　※必ず計算するので、呼び出し元はセルの値変更のない場合は呼ばないようにすること
        /// </summary>
        /// <param name="haikiKbnCd">直行、積替、建廃(必須)</param>
        /// <param name="haikiShuruiCd">廃棄物種類CD(必須)</param>
        /// <param name="haikiNameCd">廃棄物名称CD(空文字OK)</param>
        /// <param name="shobunCd">処分方法CD(空文字OK)</param>
        /// <param name="kansanSu">換算数(必須)</param>
        private decimal GetGenyouSuu(string haikiKbnCd, string haikiShuruiCd, string haikiNameCd, string shobunCd, decimal kansanSu)
        {
            LogUtility.DebugMethodStart(haikiKbnCd, haikiShuruiCd, haikiNameCd, shobunCd, kansanSu);

            //絶対必須項目

            //一括入力だと空の場合がある
            if (string.IsNullOrEmpty(haikiKbnCd)) return 0;

            //廃棄物種類
            if (string.IsNullOrEmpty(haikiShuruiCd)) return 0;
            //数量
            if (string.IsNullOrEmpty(kansanSu.ToString())) return 0;

            //個別単価のように優先順位を考慮して検索
            bool find = false;
            var searchString = new CommonKanSanDtoCls();
            DataTable dt = null;

            //廃棄物区分CD
            searchString.HAIKI_KBN_CD = haikiKbnCd;
            //廃棄物種類CD
            searchString.HAIKI_SHURUI_CD = haikiShuruiCd;
            //数量
            searchString.KANSAN_SUU = kansanSu.ToString().Replace(",", "");

            //１．廃棄物名称　あり 、処分方法　あり
            if (!string.IsNullOrEmpty(haikiNameCd) && !string.IsNullOrEmpty(shobunCd))
            {
                searchString.HAIKI_NAME_CD = haikiNameCd;
                searchString.SHOBUN_HOUHOU_CD = shobunCd;

                dt = this.mlogic.GetGenyouti(searchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //２．廃棄物名称　あり 、処分方法　 なし
            if (!find && !string.IsNullOrEmpty(haikiNameCd))
            {
                searchString.HAIKI_NAME_CD = haikiNameCd;
                searchString.SHOBUN_HOUHOU_CD = string.Empty;

                dt = this.mlogic.GetGenyouti(searchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //３．廃棄物名称　なし 、処分方法　 あり
            if (!find && !string.IsNullOrEmpty(shobunCd))
            {
                searchString.HAIKI_NAME_CD = string.Empty;
                searchString.SHOBUN_HOUHOU_CD = shobunCd;

                dt = this.mlogic.GetGenyouti(searchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //４．廃棄物名称　なし 、処分方法　 なし
            if (!find)
            {
                searchString.HAIKI_NAME_CD = string.Empty;
                searchString.SHOBUN_HOUHOU_CD = string.Empty;

                dt = this.mlogic.GetGenyouti(searchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //完全無
            if (!find)
            {
                LogUtility.DebugMethodEnd();
                // 20140624 kayo EV005028 減容率が登録されていない報告書分類を実績タブに入力すると減容後数量が0になってしまう start
                return kansanSu;
                // 20140624 kayo EV005028 減容率が登録されていない報告書分類を実績タブに入力すると減容後数量が0になってしまう end
            }

            //あり

            //数値の丸め
            decimal dKansanti = 0;
            dKansanti = this.mlogic.GetSuuryoRound(Convert.ToDecimal(dt.Rows[0]["GENYOU_CHI"]), this.mSysInfo.MANIFEST_SUURYO_FORMAT_CD.ToString());

            LogUtility.DebugMethodEnd();

            //換算後数量
            return dKansanti;
        }

        /// <summary>
        /// 換算値計算
        /// </summary>
        /// <param name="haikiKbnCd">直行、積替、建廃</param>
        /// <param name="haikiShuruiCd">廃棄物種類CD</param>
        /// <param name="haikiNameCd">廃棄物名称CD</param>
        /// <param name="nisugataCd">荷姿CD</param>
        /// <param name="unitCd">単位CD</param>
        /// <param name="kansanSu">換算数(必須)</param>
        private decimal GetKansanSuu(string haikiKbnCd, string haikiShuruiCd, string haikiNameCd, string nisugataCd, Int16 unitCd, decimal kansanSu)
        {

            //廃棄区分
            if (string.IsNullOrEmpty(haikiKbnCd)) return 0;
            //廃棄物種類
            if (string.IsNullOrEmpty(haikiShuruiCd)) return 0;
            //単位
            if (string.IsNullOrEmpty(unitCd.ToString())) return 0;
            //数量
            if (string.IsNullOrEmpty(kansanSu.ToString())) return 0;

            //個別単価のように優先順位を考慮して検索
            bool find = false;
            var SearchString = new CommonKanSanDtoCls();
            DataTable dt = null;

            //廃棄物区分CD
            SearchString.HAIKI_KBN_CD = haikiKbnCd;
            //廃棄物種類CD
            SearchString.HAIKI_SHURUI_CD = haikiShuruiCd;
            //数量
            SearchString.HAIKI_SUU = kansanSu.ToString().Replace(",", "");
            //単位CD
            SearchString.UNIT_CD = unitCd.ToString().Replace(",", "");

            //１．廃棄物名称　あり 、荷姿CD あり
            if (!string.IsNullOrEmpty(haikiNameCd) && !string.IsNullOrEmpty(nisugataCd))
            {
                SearchString.HAIKI_NAME_CD = haikiNameCd;
                SearchString.NISUGATA_CD = nisugataCd;

                dt = this.mlogic.GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //２．廃棄物名称　あり 、荷姿CD なし
            if (!find && !string.IsNullOrEmpty(haikiNameCd))
            {
                SearchString.HAIKI_NAME_CD = haikiNameCd;
                SearchString.NISUGATA_CD = string.Empty;

                dt = this.mlogic.GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //３．廃棄物名称　なし 、荷姿CD あり
            if (!find && !string.IsNullOrEmpty(nisugataCd))
            {
                SearchString.HAIKI_NAME_CD = string.Empty;
                SearchString.NISUGATA_CD = nisugataCd;

                dt = this.mlogic.GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //４．廃棄物名称　なし 、荷姿CD なし
            if (!find)
            {
                SearchString.HAIKI_NAME_CD = string.Empty;
                SearchString.NISUGATA_CD = string.Empty;

                dt = this.mlogic.GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //完全無
            if (!find)
            {
                LogUtility.DebugMethodEnd();
                return 0;
            }

            //あり
            //数値の丸め
            decimal dKansanti = 0;
            dKansanti = this.mlogic.GetSuuryoRound(Convert.ToDecimal(dt.Rows[0]["KANSAN_CHI"]), this.mSysInfo.MANIFEST_SUURYO_FORMAT_CD.ToString());

            return dKansanti;
        }

        public void GetNumForMani(List<string> getNum)
        {
            string getCensuu = string.Empty;
            string CreateFrom = string.Empty;
            string CreateTo = string.Empty;
            if (!string.IsNullOrEmpty(this.form.dateTimeCreateDateFrom.Text))
            {
                CreateFrom = Convert.ToDateTime(this.form.dateTimeCreateDateFrom.Text).ToString("yyyy/MM/dd");
            }
            if (!string.IsNullOrEmpty(this.form.dateTimeCreateDateTo.Text))
            {
                CreateTo = Convert.ToDateTime(this.form.dateTimeCreateDateTo.Text).ToString("yyyy/MM/dd");
            }
            int count = 0;
            T_MANIFEST_ENTRY[] entitys = this.manifestEntryDao.GetManifestCountByData(CreateFrom, CreateTo);
            count = entitys.Where(s => s.HAIKI_KBN_CD.Value.Equals(1)).Count();
            getCensuu = "産廃（直行）" + "       " + count + "件";
            getNum.Add(getCensuu);
            count = entitys.Where(s => s.HAIKI_KBN_CD.Value.Equals(3)).Count();
            getCensuu = "産廃（積替）" + "       " + count + "件";
            getNum.Add(getCensuu);
            count = entitys.Where(s => s.HAIKI_KBN_CD.Value.Equals(2)).Count();
            getCensuu = "建廃" + "               " + count + "件";
            getNum.Add(getCensuu);
            count = entitys.Count();
            getCensuu = "インポートマニフェスト累計" + "   " + count + "件";
            getNum.Add(getCensuu);
            var himoduke1 = this.PaperAndElecManiDao.GetManiHimodukeConutByData(true, CreateFrom, CreateTo);
            count = himoduke1 == null || himoduke1.Rows.Count <= 0 ? 0 : himoduke1.Rows.Count;
            getCensuu = "紐付（一次）" + "       " + count + "件";
            getNum.Add(getCensuu);
            var himoduke2 = this.PaperAndElecManiDao.GetManiHimodukeConutByData(false, CreateFrom, CreateTo);
            count = himoduke2 == null || himoduke2.Rows.Count <= 0 ? 0 : himoduke2.Rows.Count;
            getCensuu = "紐付（二次）" + "       " + count + "件";
            getNum.Add(getCensuu);
        }

        /// <summary>
        /// 紐付関係テーブルの登録処理を行う。
        /// トランザクション中に呼ぶこと
        /// </summary>
        public void HimodukeRegist(Transaction tran)
        {
            r_framework.Utility.LogUtility.DebugMethodStart(tran);

            //リレーション更新用
            var mrlDao = r_framework.Utility.DaoInitUtility.GetComponent<CommonManifestRelationDaoCls>();

            //Entry更新用(紙)
            var tmeDao = r_framework.Utility.DaoInitUtility.GetComponent<CommonEntryDaoCls>();

            //Entry更新用(電子)
            var r18exDao = r_framework.Utility.DaoInitUtility.GetComponent<CommonDT_R18_EXDaoCls>();

            //whoカラム更新用
            var bind = new r_framework.Logic.DataBinderLogic<SuperEntity>(null as SuperEntity);

            //紙
            if (this.paperEntries != null)
            {
                foreach (var e in this.paperEntries)
                {
                    bind.SetSystemProperty(e, false);
                    tmeDao.UpdateForRelation(e);
                }
            }
            //電子
            if (this.elecEntriesUpd != null)
            {
                foreach (var e in this.elecEntriesUpd)
                {
                    e.DELETE_FLG = true;
                    bind.SetSystemProperty(e, false);
                    r18exDao.UpdateForRelation(e);
                }
            }

            //電子
            if (this.elecEntriesIns != null)
            {
                foreach (var e in this.elecEntriesIns)
                {
                    bind.SetSystemProperty(e, false);
                    r18exDao.Insert(e);
                }
            }

            //紐付
            if (this.delete_relations != null)
            {
                foreach (var e in this.delete_relations)
                {
                    e.DELETE_FLG = true;
                    bind.SetSystemProperty(e, false);
                    mrlDao.Update(e);
                }
            }
            if (this.regist_relations != null)
            {
                foreach (var e in this.regist_relations)
                {
                    e.DELETE_FLG = false;
                    mrlDao.Insert(e);
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 一次電マニ更新処理
        /// </summary>
        /// <param name="mfTocList"></param>
        /// <param name="dtR18List"></param>
        /// <param name="dtR19List"></param>
        /// <param name="dtR02List"></param>
        /// <param name="dtR04List"></param>
        /// <param name="dtR05List"></param>
        /// <param name="dtR08List"></param>
        /// <param name="dtR13List"></param>
        /// <param name="dtR18ExList"></param>
        /// <param name="dtR19ExList"></param>
        /// <param name="dtR04ExList"></param>
        /// <param name="dtR08ExList"></param>
        /// <param name="oldDtR13ExList"></param>
        /// <param name="newDtR13ExList"></param>
        internal void UpdateFirstElecMani(List<DT_MF_TOC> mfTocList, List<DT_R18> dtR18List, List<DT_R19[]> dtR19List,
                                            List<DT_R02[]> dtR02List, List<DT_R04[]> dtR04List, List<DT_R05[]> dtR05List,
                                            List<DT_R08[]> dtR08List, List<DT_R13[]> dtR13List, List<DT_R18_EX> dtR18ExList,
                                            List<DT_R19_EX[]> dtR19ExList, List<DT_R04_EX[]> dtR04ExList, List<DT_R08_EX[]> dtR08ExList,
                                            List<DT_R13_EX[]> oldDtR13ExList, List<DT_R13_EX[]> newDtR13ExList)
        {
            #region Dao生成
            var mfTocDao = DaoInitUtility.GetComponent<CommonDT_MF_TOCDaoCls>();
            var r18Dao = DaoInitUtility.GetComponent<CommonDT_R18DaoCls>();
            var r19Dao = DaoInitUtility.GetComponent<CommonDT_R19DaoCls>();
            var r02Dao = DaoInitUtility.GetComponent<CommonDT_R02DaoCls>();
            var r04Dao = DaoInitUtility.GetComponent<CommonDT_R04DaoCls>();
            var r05Dao = DaoInitUtility.GetComponent<CommonDT_R05DaoCls>();
            var r06Dao = DaoInitUtility.GetComponent<CommonDT_R06DaoCls>();
            var r08Dao = DaoInitUtility.GetComponent<CommonDT_R08DaoCls>();
            var r13Dao = DaoInitUtility.GetComponent<CommonDT_R13DaoCls>();
            var r18ExDao = DaoInitUtility.GetComponent<CommonDT_R18_EXDaoCls>();
            var r19ExDao = DaoInitUtility.GetComponent<CommonDT_R19_EXDaoCls>();
            var r04ExDao = DaoInitUtility.GetComponent<CommonDT_R04_EXDaoCls>();
            var r08ExDao = DaoInitUtility.GetComponent<CommonDT_R08_EXDaoCls>();
            var r13ExDao = DaoInitUtility.GetComponent<CommonDT_R13_EXDaoCls>();
            #endregion

            // データ追加、更新
            #region XX_EX以外の更新
            foreach (var mfToc in mfTocList)
            {
                mfTocDao.Update(mfToc);
            }

            foreach (var tempR18 in dtR18List)
            {
                r18Dao.Insert(tempR18);
            }

            foreach (var tempR19s in dtR19List)
            {
                foreach (var tempR19 in tempR19s)
                {
                    r19Dao.Insert(tempR19);
                }
            }

            foreach (var tempR02s in dtR02List)
            {
                foreach (var tempR02 in tempR02s)
                {
                    r02Dao.Insert(tempR02);
                }
            }

            foreach (var tempR04s in dtR04List)
            {
                foreach (var tempR04 in tempR04s)
                {
                    r04Dao.Insert(tempR04);
                }
            }

            foreach (var tempR05s in dtR05List)
            {
                foreach (var tempR05 in tempR05s)
                {
                    r05Dao.Insert(tempR05);
                }
            }

            foreach (var tempR08s in dtR08List)
            {
                foreach (var tempR08 in tempR08s)
                {
                    r08Dao.Insert(tempR08);
                }
            }

            foreach (var tempR13s in dtR13List)
            {
                foreach (var tempR13 in tempR13s)
                {
                    r13Dao.Insert(tempR13);
                }
            }
            #endregion

            #region XX_EXの更新
            foreach (var tempR18Ex in dtR18ExList)
            {
                if (tempR18Ex != null)
                {
                    tempR18Ex.DELETE_FLG = true;
                    r18ExDao.Update(tempR18Ex);
                    tempR18Ex.SEQ = tempR18Ex.SEQ + 1;
                    tempR18Ex.UPDATE_USER = SystemProperty.UserName;
                    tempR18Ex.UPDATE_DATE = DateTime.Now;
                    tempR18Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR18Ex.DELETE_FLG = false;
                    r18ExDao.Insert(tempR18Ex);
                }
            }

            foreach (var tempR19Exs in dtR19ExList)
            {
                foreach (var tempR19Ex in tempR19Exs)
                {
                    tempR19Ex.DELETE_FLG = true;
                    r19ExDao.Update(tempR19Ex);
                    tempR19Ex.SEQ = tempR19Ex.SEQ + 1;
                    tempR19Ex.UPDATE_USER = SystemProperty.UserName;
                    tempR19Ex.UPDATE_DATE = DateTime.Now;
                    tempR19Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR19Ex.DELETE_FLG = false;
                    r19ExDao.Insert(tempR19Ex);
                }
            }

            foreach (var tempR04Exs in dtR04ExList)
            {
                foreach (var tempR04Ex in tempR04Exs)
                {
                    tempR04Ex.DELETE_FLG = true;
                    r04ExDao.Update(tempR04Ex);
                    tempR04Ex.SEQ = tempR04Ex.SEQ + 1;
                    tempR04Ex.UPDATE_USER = SystemProperty.UserName;
                    tempR04Ex.UPDATE_DATE = DateTime.Now;
                    tempR04Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR04Ex.DELETE_FLG = false;
                    r04ExDao.Insert(tempR04Ex);
                }
            }

            foreach (var tempR08Exs in dtR08ExList)
            {
                foreach (var tempR08Ex in tempR08Exs)
                {
                    tempR08Ex.DELETE_FLG = true;
                    r08ExDao.Update(tempR08Ex);
                    tempR08Ex.SEQ = tempR08Ex.SEQ + 1;
                    tempR08Ex.UPDATE_USER = SystemProperty.UserName;
                    tempR08Ex.UPDATE_DATE = DateTime.Now;
                    tempR08Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR08Ex.DELETE_FLG = false;
                    r08ExDao.Insert(tempR08Ex);
                }
            }

            foreach (var oldTempR13Exs in oldDtR13ExList)
            {
                foreach (var oldTempR13Ex in oldTempR13Exs)
                {
                    oldTempR13Ex.DELETE_FLG = true;
                    r13ExDao.Update(oldTempR13Ex);
                }
            }

            foreach (var newTempR13Exs in newDtR13ExList)
            {
                foreach (var newTempR13Ex in newTempR13Exs)
                {
                    r13ExDao.Insert(newTempR13Ex);
                }
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo">インポートファイル情報</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="errorLogTable">エラー内容</param>
        /// <param name="errorCount">エラー件数</param>
        /// <returns></returns>
        private void CheckTorikomiHimoduke(FileInfo fileInfo, Encoding encoding, ref DataTable errorLogTable, ref int errorCount, ref bool ValidatorFlg)
        {
            int count = 0;
            int himodukeLength = UIConstans.ListManiHimodukeHeader.Length;
            using (StreamReader reader = new StreamReader(fileInfo.FullName, encoding))
            {
                string fileContent = reader.ReadLine();
                while (!String.IsNullOrEmpty(fileContent))
                {
                    //■Step1.ファイルヘッダチェック
                    if (count == 0)
                    {
                        // 見出し行の項目確認
                        string[] listHeaderCheck = fileContent.Split(',');
                        if (listHeaderCheck == null || listHeaderCheck.Length != himodukeLength)
                        {
                            DataRow row = errorLogTable.NewRow();
                            row[0] = "1行目：" + localMsgLogic.GetMessageString("0102");
                            errorLogTable.Rows.Add(row);
                            errorCount = errorCount + 1;
                        }
                        else
                        {
                            for (int i = 0; i < himodukeLength; i++)
                            {
                                DataRow row = errorLogTable.NewRow();

                                if (!listHeaderCheck[i].Equals(UIConstans.ListManiHimodukeHeader[i]))
                                {
                                    row[0] = "1行目：" + UIConstans.ListManiHimodukeHeader[i] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0101"));
                                    errorLogTable.Rows.Add(row);

                                    errorCount = errorCount + 1;
                                }
                            }
                        }
                        if (errorLogTable.Rows.Count > 0)
                        {
                            return;
                        }
                    }
                    else
                    {
                        string[] listValidatorCheck = fileContent.Split(',');
                        string onefileContent = string.Format("{0}_{1}_{2}_{3}", listValidatorCheck[4], listValidatorCheck[5], listValidatorCheck[6], listValidatorCheck[7]);
                        #region マニフェスト明細の重複チェック
                        if (dicValidator.ContainsValue(fileContent))
                        {
                            string strError = "重複データがあります。" + Environment.NewLine;
                            strError += "重複データを確認し、削除してください。" + Environment.NewLine;
                            strError += "二次交付番号＝" + listValidatorCheck[0];
                            strError += "、廃棄物種類ＣＤ＝" + listValidatorCheck[2] + Environment.NewLine; ;
                            strError += "と一次交付番号＝" + listValidatorCheck[4];
                            strError += "、廃棄物種類ＣＤ＝" + listValidatorCheck[6];
                            this.msgLogic.MessageBoxShowError(strError);
                            ValidatorFlg = true;
                            return;
                        }
                        else
                        {
                            dicValidator.Add(dicValidator.Count + 1, fileContent);
                        }

                        if (CheckMainData.ContainsValue(onefileContent))
                        {
                            string strError = "重複データがあります。" + Environment.NewLine;
                            strError += "重複データを確認し、削除してください。" + Environment.NewLine;
                            strError += "一次交付番号＝" + listValidatorCheck[4];
                            strError += "、一次廃棄物区分ＣＤ＝" + listValidatorCheck[5] + Environment.NewLine;
                            strError += "、一次廃棄物種類ＣＤ＝" + listValidatorCheck[6];
                            strError += "、一次廃棄物名称ＣＤ＝" + listValidatorCheck[7];
                            this.msgLogic.MessageBoxShowError(strError);
                            ValidatorFlg = true;
                            return;
                        }
                        else
                        {
                            CheckMainData.Add(CheckMainData.Count + 1, onefileContent);
                        }
                    }
                        #endregion

                    fileContent = reader.ReadLine();
                    count++;
                }
            }

            count = 0;
            using (StreamReader reader = new StreamReader(fileInfo.FullName, encoding))
            {
                string fileContent = reader.ReadLine();
                while (!String.IsNullOrEmpty(fileContent))
                {
                    if (count == 0)
                    {
                        fileContent = reader.ReadLine();
                        count++;
                        continue;
                    }
                    else
                    {
                        string[] listColumnCheck = fileContent.Split(',');

                        //レコード数チェック
                        if (count == 1)
                        {
                            int s = GetNumberOfSecondToCheckDataImport(listColumnCheck, fileInfo.FullName, encoding, himodukeLength);
                            this.dicValidator.Clear();

                            if (s > UIConstans.LIMIT_NUMBER)
                            {
                                String time = Convert.ToString(UIConstans.LIMIT_NUMBER / 60);

                                DialogResult re = msgLogic.MessageBoxShowConfirm(string.Format("処理時間が{0}分を超えます。処理を継続しますか？", time));

                                if (re == DialogResult.No)
                                {
                                    return;
                                }
                            }
                        }

                        this.form.txtImportStatus.Text = "Phase.1 ... インポートレイアウトチェック　･･･　完了" + Environment.NewLine;
                        #region Step2.データチェック
                        //■Step2.データチェック
                        if (listColumnCheck.Length != himodukeLength)
                        {
                            DataRow row = errorLogTable.NewRow();
                            row[0] = (count + 1).ToString() + "行目：" + localMsgLogic.GetMessageString("0102");
                            errorLogTable.Rows.Add(row);

                            errorCount = errorCount + 1;

                        }
                        else
                        {

                            for (int i = 0; i < himodukeLength; i++)
                            {
                                #region 必須ﾁｪｯｸ
                                if (this.form.txtHaikiKbn.Text.Equals(UIConstans.MANI_SBT_HIMODUKE))
                                {
                                    if (UIConstans.ListHissuColumnIndexHimoduke.Contains(i))
                                    {
                                        if (string.IsNullOrEmpty(listColumnCheck[i]))
                                        {
                                            DataRow row = errorLogTable.NewRow();
                                            row[0] = (count + 1).ToString() + "行目：" + UIConstans.ListManiHimodukeHeader[i] + UIConstans.ZENKAKU_SPACE + string.Format(localMsgLogic.GetMessageString("0201"));
                                            errorLogTable.Rows.Add(row);

                                            errorCount = errorCount + 1;

                                            continue;
                                        }
                                    }
                                }
                                #endregion

                                #region データタイプチェック
                                string errorMsg = "";
                                string type = UIConstans.ListColumnDataTypeHimoduke[i].Type; ;
                                int dataLength = UIConstans.ListColumnDataTypeHimoduke[i].Lenght;

                                if (this.CheckDataType(listColumnCheck[i], type, dataLength, ref errorMsg))
                                {
                                    errorMsg = UIConstans.ListManiHimodukeHeader[i] + UIConstans.ZENKAKU_SPACE + errorMsg;
                                    DataRow row = errorLogTable.NewRow();
                                    row[0] = (count + 1).ToString() + "行目：" + errorMsg;
                                    errorLogTable.Rows.Add(row);

                                    errorCount = errorCount + 1;

                                    continue;
                                }
                                #endregion
                            }

                            #region マスタチェック、その他チェック等
                            DataTable errorLogTableCheck = new DataTable();
                            errorLogTableCheck.Columns.Add("ERROR", typeof(string));
                            CheckExistsInMasterHimoduke(listColumnCheck, ref errorLogTableCheck, ref errorCount);

                            if (errorLogTableCheck != null && errorLogTableCheck.Rows.Count > 0)
                            {
                                for (int i = 0; i < errorLogTableCheck.Rows.Count; i++)
                                {
                                    DataRow row = errorLogTable.NewRow();

                                    row[0] = errorLogTableCheck.Rows[i][0];
                                    errorLogTable.Rows.Add(row);
                                }
                            }
                            #endregion
                        }
                        #endregion
                        if (!relationInfoDto.HimodukeErrorFlg)
                        {
                            SqlInt64 nextSystemId = -1;
                            if (UIConstans.HAIKI_KBN_DENSHI.Equals(Convert.ToInt16(relationInfoDto.SECOND_MANI_KBN)))
                            {
                                nextSystemId = relationInfoDto.SECOND_SYSTEM_ID;
                            }
                            else
                            {
                                nextSystemId = relationInfoDto.SECOND_DETAIL_SYSTEM_ID;
                            }
                            if (this.currentRelation != null)
                            {
                                T_MANIFEST_RELATION[] copyList = PaperAndElecManiDao.SelectCurrent(nextSystemId, Convert.ToInt16(relationInfoDto.SECOND_MANI_KBN), false);
                                this.currentRelation.CopyTo(copyList, 0);
                                if (copyList.Length > 0)
                                {
                                    relationInfoDto.RELATION_SEQ = copyList.FirstOrDefault().SEQ + 1;
                                }
                                else
                                {
                                    copyList = PaperAndElecManiDao.SelectCurrent(nextSystemId, Convert.ToInt16(relationInfoDto.SECOND_MANI_KBN), true);
                                    if (copyList.Length > 0)
                                    {
                                        relationInfoDto.RELATION_SEQ = copyList.FirstOrDefault().SEQ + 1;
                                    }
                                }
                            }
                            else
                            {
                                this.currentRelation = PaperAndElecManiDao.SelectCurrent(nextSystemId, Convert.ToInt16(relationInfoDto.SECOND_MANI_KBN), false);
                                if (this.currentRelation.Length > 0)
                                {
                                    relationInfoDto.RELATION_SEQ = this.currentRelation.FirstOrDefault().SEQ + 1;
                                }
                                else
                                {
                                    this.currentRelation = PaperAndElecManiDao.SelectCurrent(nextSystemId, Convert.ToInt16(relationInfoDto.SECOND_MANI_KBN), true);
                                    if (this.currentRelation.Length > 0)
                                    {
                                        relationInfoDto.RELATION_SEQ = this.currentRelation[this.currentRelation.Length - 1].SEQ + 1;
                                        this.currentRelation = null;
                                    }
                                }
                            }
                            this.listRelationInfo.Add(relationInfoDto);
                        }
                    }

                    fileContent = reader.ReadLine();
                    count++;
                }
            }
        }
    }
}