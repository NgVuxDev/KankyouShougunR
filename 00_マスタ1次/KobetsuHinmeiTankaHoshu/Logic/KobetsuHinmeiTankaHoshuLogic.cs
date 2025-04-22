// $Id: KobetsuHinmeiTankaHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KobetsuHinmeiTankaHoshu.APP;
using KobetsuHinmeiTankaHoshu.MultiRowTemplate;
using KobetsuHinmeiTankaHoshu.Validator;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using System.Text;

namespace KobetsuHinmeiTankaHoshu.Logic
{
    /// <summary>
    /// 個別品名単価保守画面のビジネスロジック
    /// </summary>
    public class KobetsuHinmeiTankaHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "KobetsuHinmeiTankaHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_KOBETSU_HINMEI_DATA_SQL = "KobetsuHinmeiTankaHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_ICHIRAN_FROM_KIHON_HINMEI_DATA_SQL_FOR_URIAGE = "KobetsuHinmeiTankaHoshu.Sql.GetIchiranDataSqlFromKihonHinmeitankaForUriage.sql";//add

        private readonly string GET_ICHIRAN_FROM_KIHON_HINMEI_DATA_SQL_FOR_SHIHARAI = "KobetsuHinmeiTankaHoshu.Sql.GetIchiranDataSqlFromKihonHinmeitankaForShiharai.sql";//add

        private readonly string GET_KOBETSU_HINMEI_TANKA_DATA_SQL = "KobetsuHinmeiTankaHoshu.Sql.GetKobetsuHinmeiTankaDataSql.sql";

        private readonly string GET_TORIHIKISAKI_DATA_SQL = "KobetsuHinmeiTankaHoshu.Sql.GetTorihikisakiDataSql.sql";

        private readonly string GET_GYOUSHA_DATA_SQL = "KobetsuHinmeiTankaHoshu.Sql.GetGyoushaDataSql.sql";

        private readonly string GET_GENBA_DATA_SQL = "KobetsuHinmeiTankaHoshu.Sql.GetGenbaDataSql.sql";

        private readonly string GET_UNPAN_GYOUSHA_DATA_SQL = "KobetsuHinmeiTankaHoshu.Sql.GetUnpanGyoushaDataSql.sql";

        private readonly string GET_NIOROSHI_GYOUSHA_DATA_SQL = "KobetsuHinmeiTankaHoshu.Sql.GetNioroshiGyoushaDataSql.sql";

        private readonly string GET_NIOROSHI_GENBA_DATA_SQL = "KobetsuHinmeiTankaHoshu.Sql.GetNioroshiGenbaDataSql.sql";

        private readonly string GET_HINMEI_DATA_SQL = "KobetsuHinmeiTankaHoshu.Sql.GetHinmeiDataSql.sql";

        /// <summary>
        /// 個別品名単価保守画面Form
        /// </summary>
        private KobetsuHinmeiTankaHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        public M_KOBETSU_HINMEI_TANKA[] entitys;

        /// <summary>
        /// 品名読込み用
        /// 表示されている一覧を保持し、登録時に削除
        /// </summary>
        public M_KOBETSU_HINMEI_TANKA[] deleteEntitys;

        private bool isAllSearch;

        /// <summary>
        /// 個別品名単価のDao
        /// </summary>
        private IM_KOBETSU_HINMEI_TANKADao dao;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 品名のDao
        /// </summary>
        private IM_HINMEIDao hinmeiDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        /// <summary>
        /// 種類のDao
        /// </summary>
        private IM_SHURUIDao shuruiDao;

        /// <summary>
        /// 単位のDao
        /// </summary>
        private IM_UNITDao unitDao;

        /// <summary>
        /// 基本品名マスタ読み込み中
        /// True＝読み込み中
        /// </summary>
        internal bool isNowLoadingHinmeiMaster = false;

        // VUNGUYEN 20150525 #1294 START
        public Cell cell;
        // VUNGUYEN 20150525 #1294 END

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal MasterBaseForm parentForm;
        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// 画面のタイプ
        /// </summary>
        public WINDOW_TYPE WindowType { get; set; }

        /// <summary>
        /// 取引先CD
        /// </summary>
        public string TorihikisakiCd { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GyoushaCd { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        public string GenbaCd { get; set; }

        /// <summary>
        /// 伝票区分
        /// </summary>
        public string dennpyouKbn { get; set; }

        /// <summary>
        /// 前回値保存
        /// </summary>
        internal string previousValue { get; set; }

        #endregion フィールド

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索結果(全件)
        /// </summary>
        public DataTable SearchResultAll { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_KOBETSU_HINMEI_TANKA SearchString { get; set; }

        /// <summary>
        /// 検索結果(取引先)
        /// </summary>
        public DataTable SearchResultTorihikisaki { get; set; }

        /// <summary>
        /// 検索結果(現場)
        /// </summary>
        public DataTable SearchResultGenba { get; set; }

        /// <summary>
        /// 検索結果(業者)
        /// </summary>
        public DataTable SearchResultGyousha { get; set; }

        /// <summary>
        /// 検索結果(運搬業者)
        /// </summary>
        public DataTable SearchResultUnpan { get; set; }

        /// <summary>
        /// 検索結果(荷降先業者)
        /// </summary>
        public DataTable SearchResultNioroshiGyousha { get; set; }

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        /// <summary>
        /// 処理対象伝票区分
        /// </summary>
        public Int16 TargetDenpyouKbn { get; set; }

        #endregion プロパティ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public KobetsuHinmeiTankaHoshuLogic(KobetsuHinmeiTankaHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_KOBETSU_HINMEI_TANKADao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.shuruiDao = DaoInitUtility.GetComponent<IM_SHURUIDao>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            this.SearchResult = null;
            this.SearchResultAll = null;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (MasterBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                this.form.TORIHIKISAKI_CD.Text = Properties.Settings.Default.TorihikisakiValue_Text;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = Properties.Settings.Default.TorihikisakiName_Text;
                this.form.GYOUSHA_CD.Text = Properties.Settings.Default.GyoushaValue_Text;
                this.form.GYOUSHA_NAME_RYAKU.Text = Properties.Settings.Default.GyoushaName_Text;
                this.form.GENBA_CD.Text = Properties.Settings.Default.GenbaValue_Text;
                this.form.GENBA_NAME_RYAKU.Text = Properties.Settings.Default.GenbaName_Text;

                this.form.beforGyousaCD = this.form.GYOUSHA_CD.Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1978
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                // 20171117 wangjm 個別品名単価一覧 start
                if (this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    this.DefaultInit();
                }
                // 20171117 wangjm 個別品名単価一覧 end

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 個別品名単価入力初期値設定処理
        /// </summary>
        public void DefaultInit()
        {
            if (!string.IsNullOrEmpty(this.TorihikisakiCd))
            {
                this.form.TORIHIKISAKI_CD.Text = this.TorihikisakiCd;
                M_TORIHIKISAKI torihikisakiEntry = this.torihikisakiDao.GetDataByCd(this.TorihikisakiCd);
                if (torihikisakiEntry != null)
                {
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntry.TORIHIKISAKI_NAME_RYAKU;
                }
            }
            else
            {
                this.form.TORIHIKISAKI_CD.Text = "";
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = "";
            }

            if (!string.IsNullOrEmpty(this.GyoushaCd))
            {
                this.form.GYOUSHA_CD.Text = this.GyoushaCd;
                M_GYOUSHA gyoushaEntry = this.gyoushaDao.GetDataByCd(this.GyoushaCd);
                if (gyoushaEntry != null)
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntry.GYOUSHA_NAME_RYAKU;
                }
            }
            else
            {
                this.form.GYOUSHA_CD.Text = "";
                this.form.GYOUSHA_NAME_RYAKU.Text = "";
            }

            if (!string.IsNullOrEmpty(this.GenbaCd))
            {
                this.form.GENBA_CD.Text = this.GenbaCd;
                M_GENBA genbaDto = new M_GENBA();
                genbaDto.GYOUSHA_CD = this.GyoushaCd;
                genbaDto.GENBA_CD = this.GenbaCd;
                M_GENBA genbaEntry = this.genbaDao.GetDataByCd(genbaDto);
                if (genbaEntry != null)
                {
                    this.form.GENBA_NAME_RYAKU.Text = genbaEntry.GENBA_NAME_RYAKU;
                }
            }
            else
            {
                this.form.GENBA_CD.Text = "";
                this.form.GENBA_NAME_RYAKU.Text = "";
            }

            var parentForm = (MasterBaseForm)this.form.Parent;

            var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");

            if (this.dennpyouKbn == "1")
            {
                parentForm.bt_process1.Text = "[1]売上";
                parentForm.txb_process.Text = "2";
            }
            else if ((this.dennpyouKbn == "2"))
            {
                parentForm.bt_process1.Text = "[1]支払";
                parentForm.txb_process.Text = "1";
            }
        }

        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        public void SetHyoujiJoukenInit()
        {
            LogUtility.DebugMethodStart();
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);

            if (this.entitySysInfo != null)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Value;
            }
            else
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = true;
            }

            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.Ichiran.ReadOnly = true;
            this.form.Ichiran.AllowUserToAddRows = false;
            this.form.Ichiran.IsBrowsePurpose = true;

            // FunctionButton
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func9.Enabled = false;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //CongBinh 20201201 #136215 S
                this.isCopyData = false;
                this.ListSysId_Copy = null; 
                //CongBinh 20201201 #136215 E
                this.SearchResult = GetSearchResult();

                this.SetSearchResultAll();

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                int count = this.SearchResult.Rows == null ? 0 : 1;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// 検索してSearchResultを返す
        /// </summary>
        /// <returns></returns>
        public DataTable GetSearchResult()
        {
            SetSearchString();
            var searchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_KOBETSU_HINMEI_DATA_SQL
                                                        , this.SearchString
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked
                                                        , this.form.SYURUI_SHITEI.Checked
                                                        , this.form.SHURUI_CD.Text
                                                        );
            return searchResult;
        }

        /// <summary>
        /// 検索してSearchResultAllにセット
        /// </summary>
        public void SetSearchResultAll()
        {
            SetSearchString();
            M_KOBETSU_HINMEI_TANKA searchParams = new M_KOBETSU_HINMEI_TANKA();
            searchParams.TORIHIKISAKI_CD = this.SearchString.TORIHIKISAKI_CD;
            searchParams.GYOUSHA_CD = this.SearchString.GYOUSHA_CD;
            searchParams.GENBA_CD = this.SearchString.GENBA_CD;
            searchParams.DENPYOU_KBN_CD = this.SearchString.DENPYOU_KBN_CD;
            this.SearchResultAll = dao.GetDataBySqlFile(this.GET_KOBETSU_HINMEI_TANKA_DATA_SQL, searchParams);
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                var focus = (this.form.TopLevelControl as Form).ActiveControl;
                this.form.Ichiran.Focus();

                var parentForm = (MasterBaseForm)this.form.Parent;

                var entityList = new M_KOBETSU_HINMEI_TANKA[this.form.Ichiran.Rows.Count];

                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_KOBETSU_HINMEI_TANKA();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KOBETSU_HINMEI_TANKA>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }
                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var kobetsuHinmeiTankaEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_KOBETSU_HINMEI_TANKA> addList = new List<M_KOBETSU_HINMEI_TANKA>();
                foreach (var kobetsuHinmeiTankaEntity in kobetsuHinmeiTankaEntityList)
                {
                    if (kobetsuHinmeiTankaEntity.DELETE_FLG == isDelete)
                    {
                        kobetsuHinmeiTankaEntity.SetValue(this.form.GYOUSHA_CD);
                        kobetsuHinmeiTankaEntity.SetValue(this.form.GENBA_CD);
                        kobetsuHinmeiTankaEntity.SetValue(this.form.TORIHIKISAKI_CD);

                        if (parentForm.bt_process1.Text == "[1]売上")
                        {
                            kobetsuHinmeiTankaEntity.DENPYOU_KBN_CD = 2;
                        }
                        else
                        {
                            kobetsuHinmeiTankaEntity.DENPYOU_KBN_CD = 1;
                        }

                        MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), kobetsuHinmeiTankaEntity);
                        addList.Add(kobetsuHinmeiTankaEntity);
                    }
                }

                this.form.Ichiran.DataSource = preDt;

                this.entitys = addList.ToArray();

                if (focus != null)
                {
                    focus.Focus();
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SetHyoujiJoukenInit();
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1978

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 個別品名単価CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }
                //CongBinh 20201201 #136215 S
                if (isCopyData)
                {
                    foreach (DataRow row in ((DataTable)this.form.Ichiran.DataSource).Rows)
                    {
                        if (row.RowState == DataRowState.Unchanged)
                        {
                            row.SetAdded();
                        }
                    }
                }
                //CongBinh 20201201 #136215 E
                dt = ((DataTable)this.form.Ichiran.DataSource).GetChanges();
                if (dt == null || dt.Rows.Count <= 0)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                KobetsuHinmeiTankaHoshuValidator vali = new KobetsuHinmeiTankaHoshuValidator();
                if (this.isNowLoadingHinmeiMaster)
                {
                    this.SetSearchResultAll();//削除したので最新を取得
                }
                var result = vali.KobetsuHinmeiTankaCDValidator(this.form.Ichiran, this.SearchResult, this.SearchResultAll, this.isAllSearch);
                LogUtility.DebugMethodEnd(result);
                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        public bool Preview()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C011", "個別品名単価一覧表");

                MessageBox.Show("未実装");

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// CSV
        /// </summary>
        public bool CSV()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    MultiRowIndexCreateLogic multirowLocationLogic = new MultiRowIndexCreateLogic();
                    multirowLocationLogic.multiRow = this.form.Ichiran;

                    multirowLocationLogic.CreateLocations();

                    // VUNGUYEN 20150525 #1294 START
                    CSVFileLogicCustom csvLogic = new CSVFileLogicCustom();
                    // VUNGUYEN 20150525 #1294 END

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    csvLogic.CreateCSVFile(this.form);
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        public void CancelCondition()
        {
            LogUtility.DebugMethodStart();

            ClearCondition();
            bool catchErr = SetIchiran();

            LogUtility.DebugMethodEnd();
        }

        #region 登録/更新/削除

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                //独自チェックの記述例を書く
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_KOBETSU_HINMEI_TANKA kobetsuEntity in this.entitys)
                        {
                            M_KOBETSU_HINMEI_TANKA entity = null;

                            // SYS_IDの値判断
                            if (!kobetsuEntity.SYS_ID.IsNull)
                            {
                                entity = this.dao.GetDataByCd(kobetsuEntity.SYS_ID.ToString());
                            }

                            if (entity == null)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (kobetsuEntity.DELETE_FLG)
                                {
                                    continue;
                                }
                                // MAXのSYS_IDを取得する
                                Int64 sysId = Convert.ToInt64(this.dao.GetMaxPlusKey());
                                kobetsuEntity.SYS_ID = sysId;
                                this.dao.Insert(kobetsuEntity);
                            }
                            else
                            {
                                this.dao.Update(kobetsuEntity);
                            }
                        }
                        //CongBinh 20201201 #136215 S
                        if (this.ListSysId_Copy != null)
                        {
                            foreach (var item in this.ListSysId_Copy)
                            {
                                var entity = this.dao.GetDataByCd(item);
                                if (entity != null)
                                {
                                    entity.DELETE_FLG = true;
                                    this.dao.Update(entity);
                                }
                            }
                        }
                        //CongBinh 20201201 #136215 E
                        // トランザクション終了
                        tran.Commit();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");
                }
                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                var result = msgLogic.MessageBoxShow("C021");
                if (result == DialogResult.Yes)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_KOBETSU_HINMEI_TANKA kobetsuEntity in this.entitys)
                        {
                            // SYS_IDの値判断
                            if (kobetsuEntity.SYS_ID.IsNull)
                            {
                                continue;
                            }

                            M_KOBETSU_HINMEI_TANKA entity = this.dao.GetDataByCd(kobetsuEntity.SYS_ID.ToString());
                            if (entity != null)
                            {
                                this.dao.Update(kobetsuEntity);
                            }
                        }
                        // トランザクション終了
                        tran.Commit();
                    }
                    msgLogic.MessageBoxShow("I001", "削除");
                }

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除　品名読込用
        /// </summary>
        [Transaction]
        public virtual bool LogicalDeleteForHinmeiYomikomi()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // トランザクション開始
                using (var tran = new Transaction())
                {
                    //削除処理
                    foreach (M_KOBETSU_HINMEI_TANKA kobetsuEntity in this.deleteEntitys)
                    {
                        // SYS_IDの値判断
                        if (kobetsuEntity.SYS_ID.IsNull)
                        {
                            continue;
                        }

                        M_KOBETSU_HINMEI_TANKA entity = this.dao.GetDataByCd(kobetsuEntity.SYS_ID.ToString());
                        if (entity != null)
                        {
                            this.dao.Update(kobetsuEntity);
                        }
                    }

                    // トランザクション終了
                    tran.Commit();
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDeleteForHinmeiYomikomi", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion 登録/更新/削除

        #region Equals/GetHashCode/ToString

        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            KobetsuHinmeiTankaHoshuLogic localLogic = other as KobetsuHinmeiTankaHoshuLogic;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion Equals/GetHashCode/ToString

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran(bool isCopy = false) //CongBinh 20201201 #136215
        {
            try
            {
                string WhereJouken = string.Empty;
                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    WhereJouken += "TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "'";
                }
                else
                {
                    WhereJouken += "TORIHIKISAKI_CD = ''";
                }
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    if (WhereJouken != "")
                    {
                        WhereJouken += " AND ";
                    }
                    WhereJouken += "GYOUSHA_CD = '" + this.form.GYOUSHA_CD.Text + "'";
                }
                else
                {
                    if (WhereJouken != "")
                    {
                        WhereJouken += " AND ";
                    }
                    WhereJouken += "GYOUSHA_CD = ''";
                }
                if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    if (WhereJouken != "")
                    {
                        WhereJouken += " AND ";
                    }
                    WhereJouken += "GENBA_CD = '" + this.form.GENBA_CD.Text + "'";
                }
                else
                {
                    if (WhereJouken != "")
                    {
                        WhereJouken += " AND ";
                    }
                    WhereJouken += "GENBA_CD = ''";
                }

                if (this.SearchResult == null)
                {
                    return false;
                }
                //CongBinh 2021015 #146096 S
                //DataRow[] dr = isCopy ? this.SearchResult.Select("1 = 1") : this.SearchResult.Select(WhereJouken); //CongBinh 20201201 #136215
                //if (dr == null)
                //{
                //    return false;
                //}
                //DataTable table = this.SearchResult.Clone();
                //foreach (DataRow row in dr)
                //{
                //    table.ImportRow(row);
                //}
                DataTable table = this.SearchResult.Clone();
                if (isCopy)
                {
                    foreach (DataRow row in this.SearchResult.Rows)
                    {
                        table.ImportRow(row);
                    }
                }
                else
                {
                    DataRow[] dr = this.SearchResult.Select(WhereJouken);
                    if (dr == null)
                    {
                        return false;
                    }
                    foreach (DataRow row in dr)
                    {
                        table.ImportRow(row);
                    }
                }
                //CongBinh 2021015 #146096 E
                table.AcceptChanges();
                this.SearchResult = table;

                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                    table.Columns[i].AllowDBNull = true;
                }
                this.form.Ichiran.DataSource = null;//リロード
                this.form.Ichiran.DataSource = table;

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M212", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
                }
                else
                {
                    this.DispReferenceMode();
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetIchiran", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (MasterBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Title処理
        /// </summary>
        [Transaction]
        public virtual void TitleInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");

            //システム設定より画面Title取得
            if (parentForm.bt_process1.Text == "")
            {
                switch (this.entitySysInfo.KOBETSU_HINMEI_DEFAULT.Value.ToString())
                {
                    case "1":
                        parentForm.bt_process1.Text = "[1]売上";
                        break;

                    case "2":
                        parentForm.bt_process1.Text = "[1]支払";
                        break;

                    default:
                        break;
                }
            }

            if (parentForm.bt_process1.Text == "[1]売上")
            {
                titleControl.Text = "個別品名単価入力（売上）";
                parentForm.bt_process1.Text = "[1]支払";
                parentForm.txb_process.Text = "1";
                this.TargetDenpyouKbn = 1;
            }
            else
            {
                titleControl.Text = "個別品名単価入力（支払）";
                parentForm.bt_process1.Text = "[1]売上";
                parentForm.txb_process.Text = "2";
                this.TargetDenpyouKbn = 2;
            }

            //品名読込
            parentForm.bt_process2.Text = "[2]基本品名単価読込";
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            //削除ボタン(F4)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            //ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            //parentForm.bt_func5.Click += new EventHandler(this.form.Preview);//CongBinh 20201201 #136215
            parentForm.bt_func5.Click += new EventHandler(bt_func5_Click); //CongBinh 20201201 #136215
         
            //CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            //条件取消ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.CancelCondition);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //売上/支払
            parentForm.bt_process1.Click += new EventHandler(this.form.Change);

            //品名読込ボタン(2)イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.form.HinmeiLoad);

            //表示条件イベント生成
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;
            M_KOBETSU_HINMEI_TANKA entity = new M_KOBETSU_HINMEI_TANKA();

            // 取引先の検索条件の設定
            entity.SetValue(this.form.TORIHIKISAKI_CD);
            // 現場の検索条件の設定
            entity.SetValue(this.form.GENBA_CD);
            // 業者の検索条件の設定
            entity.SetValue(this.form.GYOUSHA_CD);

            var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");
            //システム設定より画面Title取得
            if (titleControl.Text == "基本品名単価入力")
            {
                switch (this.entitySysInfo.KOBETSU_HINMEI_DEFAULT.Value.ToString())
                {
                    case "1":
                        titleControl.Text = "個別品名単価入力（売上）";
                        break;

                    case "2":
                        titleControl.Text = "個別品名単価入力（支払）";
                        break;

                    default:
                        break;
                }
            }

            if (titleControl.Text == "個別品名単価入力（売上）")
            {
                entity.DENPYOU_KBN_CD = 1;
                this.TargetDenpyouKbn = 1;
            }
            else
            {
                entity.DENPYOU_KBN_CD = 2;
                this.TargetDenpyouKbn = 2;
            }

            this.SearchString = entity;
        }

        /// <summary>
        /// DataTableのクローン処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetCloneDataTable(DataTable dt)
        {
            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }

            return table;
        }

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearCondition()
        {
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.SYURUI_ALL.Checked = true;
            this.form.SHURUI_CD.Text = string.Empty;
            this.form.SHURUI_NAME_RYAKU.Text = string.Empty;

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
        }

        /// <summary>
        /// 品名検索
        /// </summary>
        /// <param name="e"></param>
        public virtual void SearchHinmei(CellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            if (e.FormattedValue == null || string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
            {
                this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_NAME_RYAKU].Value = string.Empty;
                LogUtility.DebugMethodEnd(e);
                return;
            }

            int padLen = int.Parse(((GcCustomAlphaNumTextBoxCell)((KobetsuHinmeiTankaHoshuDetail)this.form.Ichiran.Template).Row[Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_CD]).CharactersNumber.ToString());
            M_HINMEI cond = new M_HINMEI();
            cond.HINMEI_CD = e.FormattedValue.ToString().PadLeft(padLen, '0');
            if (this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value != null && !string.IsNullOrWhiteSpace(this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value.ToString()))
            {
                cond.DENSHU_KBN_CD = Int16.Parse(this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value.ToString());
            }
            cond.DENPYOU_KBN_CD = this.TargetDenpyouKbn;
            DataTable hin = this.hinmeiDao.GetDataBySqlFile(this.GET_HINMEI_DATA_SQL, cond);
            if (hin.Rows.Count <= 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "品名");
                e.Cancel = true;
                ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                return;
            }

            if (this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value == null || string.IsNullOrWhiteSpace(this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value.ToString()))
            {
                this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_CD].Value = hin.Rows[0][Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_CD].ToString();
                this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_RYAKU].Value = hin.Rows[0][Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_RYAKU].ToString();
            }
            this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_CD].Value = hin.Rows[0][Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_CD].ToString();
            this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_NAME_RYAKU].Value = hin.Rows[0][Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_NAME_RYAKU].ToString();

            LogUtility.DebugMethodEnd(e);
        }

        /// <summary>
        /// 取引先名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchTorihikisakiName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    M_TORIHIKISAKI tmp = new M_TORIHIKISAKI();
                    tmp.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    this.SearchResultTorihikisaki = torihikisakiDao.GetDataBySqlFile(this.GET_TORIHIKISAKI_DATA_SQL, tmp);

                    if (this.SearchResultTorihikisaki != null && this.SearchResultTorihikisaki.Rows.Count > 0)
                    {
                        DataRow row = this.SearchResultTorihikisaki.Rows[0];
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = row["TORIHIKISAKI_NAME_RYAKU"].ToString();
                    }
                    else
                    {
                        if (e != null)
                        {
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                            msgLogic.MessageBoxShow("E020", "取引先");
                            e.Cancel = true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchTorihikisakiName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTorihikisakiName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 業者CDから取引先CDと取引先名称を取得
        /// </summary>
        public virtual bool SearchTorihikisakiByGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text) && !string.IsNullOrWhiteSpace(this.form.GENBA_CD.Text))
                //{
                //    this.SearchGenbaName(null);
                //}
                //else if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text))
                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text))
                {
                    M_GYOUSHA tmpGyousha = new M_GYOUSHA();
                    tmpGyousha.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    this.SearchResultGyousha = gyoushaDao.GetDataBySqlFile(this.GET_GYOUSHA_DATA_SQL, tmpGyousha);

                    if (this.SearchResultGyousha != null && this.SearchResultGyousha.Rows.Count > 0)
                    {
                        DataRow rowGyousha = this.SearchResultGyousha.Rows[0];

                        //if (this.form.beforGyousaCD != this.form.GYOUSHA_CD.Text)
                        //{
                        if (rowGyousha["TORIHIKISAKI_UMU_KBN"].ToString() == "1")
                        {
                            M_TORIHIKISAKI tmpTorihikisaki = new M_TORIHIKISAKI();
                            tmpTorihikisaki.TORIHIKISAKI_CD = rowGyousha["TORIHIKISAKI_CD"].ToString();
                            this.SearchResultTorihikisaki = torihikisakiDao.GetDataBySqlFile(this.GET_TORIHIKISAKI_DATA_SQL, tmpTorihikisaki);

                            if (this.SearchResultTorihikisaki != null && this.SearchResultTorihikisaki.Rows.Count > 0)
                            {
                                DataRow rowTorihikisaki = this.SearchResultTorihikisaki.Rows[0];
                                this.form.TORIHIKISAKI_CD.Text = rowTorihikisaki["TORIHIKISAKI_CD"].ToString();
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = rowTorihikisaki["TORIHIKISAKI_NAME_RYAKU"].ToString();
                            }
                            else
                            {
                                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                            }
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_CD.Text = string.Empty;
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                        }
                        //this.form.beforGyousaCD = this.form.GYOUSHA_CD.Text;
                        //}
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchTorihikisakiByGyousha", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTorihikisakiByGyousha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 現場名称情報の取得
        /// *****現場名称取得時のエラー処理は、ErrorCheckGenba()メソッドを使用してください。*****
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchGenbaName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text) && !string.IsNullOrWhiteSpace(this.form.GENBA_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    M_GENBA tmp = new M_GENBA();
                    tmp.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    tmp.GENBA_CD = this.form.GENBA_CD.Text;
                    this.SearchResultGenba = genbaDao.GetDataBySqlFile(this.GET_GENBA_DATA_SQL, tmp);

                    if (this.SearchResultGenba != null && this.SearchResultGenba.Rows.Count > 0)
                    {
                        DataRow row = this.SearchResultGenba.Rows[0];
                        this.form.GENBA_NAME_RYAKU.Text = row["GENBA_NAME_RYAKU"].ToString();

                        if (!this.form.beforGenbaCD.Equals(this.form.GENBA_CD.Text))
                        {
                            // 取引先、業者を設定
                            this.form.GYOUSHA_CD.Text = row["GYOUSHA_CD"].ToString();
                            this.form.GYOUSHA_NAME_RYAKU.Text = row["GYOUSHA_NAME_RYAKU"].ToString();
                            this.form.TORIHIKISAKI_CD.Text = row["TORIHIKISAKI_CD"].ToString();
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = row["TORIHIKISAKI_NAME_RYAKU"].ToString();
                        }
                    }

                    this.form.beforGyousaCD = this.form.GYOUSHA_CD.Text;
                    this.form.beforGenbaCD = this.form.GENBA_CD.Text;
                }
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchGenbaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGenbaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 業者名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchGyoushaName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text) && !string.IsNullOrWhiteSpace(this.form.GENBA_CD.Text))
                {
                    if (this.form.beforGyousaCD.Equals(this.form.GYOUSHA_CD.Text))
                    {
                        return false;
                    }

                    this.SearchGenbaName(null);
                }
                else if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    M_GYOUSHA tmp = new M_GYOUSHA();
                    tmp.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    this.SearchResultGyousha = gyoushaDao.GetDataBySqlFile(this.GET_GYOUSHA_DATA_SQL, tmp);

                    if (this.SearchResultGyousha != null && this.SearchResultGyousha.Rows.Count > 0)
                    {
                        DataRow row = this.SearchResultGyousha.Rows[0];
                        this.form.GYOUSHA_NAME_RYAKU.Text = row["GYOUSHA_NAME_RYAKU"].ToString();

                        if (!this.form.beforGyousaCD.Equals(this.form.GYOUSHA_CD.Text))
                        {
                            if (row["TORIHIKISAKI_UMU_KBN"].ToString() == "1")
                            {
                                M_TORIHIKISAKI tmpTorihikisaki = new M_TORIHIKISAKI();
                                tmpTorihikisaki.TORIHIKISAKI_CD = row["TORIHIKISAKI_CD"].ToString();
                                this.SearchResultTorihikisaki = torihikisakiDao.GetDataBySqlFile(this.GET_TORIHIKISAKI_DATA_SQL, tmpTorihikisaki);

                                if (this.SearchResultTorihikisaki != null && this.SearchResultTorihikisaki.Rows.Count > 0)
                                {
                                    DataRow rowTorihikisaki = this.SearchResultTorihikisaki.Rows[0];
                                    this.form.TORIHIKISAKI_CD.Text = rowTorihikisaki["TORIHIKISAKI_CD"].ToString();
                                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = rowTorihikisaki["TORIHIKISAKI_NAME_RYAKU"].ToString();
                                }
                                else
                                {
                                    this.form.TORIHIKISAKI_CD.Text = string.Empty;
                                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                                }
                            }
                            else
                            {
                                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                            }
                        }

                        this.form.beforGyousaCD = this.form.GYOUSHA_CD.Text;
                    }
                    else
                    {
                        if (e != null)
                        {
                            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                            msgLogic.MessageBoxShow("E020", "業者");
                            e.Cancel = true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchGyoushaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyoushaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 一覧セル編集開始時イベント処理
        /// </summary>
        /// <param name="e"></param>
        public bool IchiranCellEnter(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if ((this.form.GYOUSHA_CD.TextLength <= 0 && this.form.TORIHIKISAKI_CD.TextLength <= 0) || this.SearchResultAll == null)//157931
                {
                    this.form.Ichiran.CurrentRow.Selectable = false;
                }
                else
                {
                    this.form.Ichiran.CurrentRow.Selectable = true;
                }

                // 新規行の場合には削除チェックさせない
                if (this.form.Ichiran.Rows[e.RowIndex].IsNewRow)
                {
                    this.form.Ichiran.Rows[e.RowIndex][Const.KobetsuHinmeiTankaHoshuConstans.DELETE_FLG].Selectable = false;
                }
                else
                {
                    this.form.Ichiran.Rows[e.RowIndex][Const.KobetsuHinmeiTankaHoshuConstans.DELETE_FLG].Selectable = true;
                }

                // 品名選択時
                if (e.CellName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_CD))
                {
                    GcCustomAlphaNumTextBoxCell target = (GcCustomAlphaNumTextBoxCell)this.form.Ichiran[e.RowIndex, e.CellName];
                    target.PopupSearchSendParams[1].SubCondition[1].Value = this.TargetDenpyouKbn.ToString();

                    PopupSearchSendParamDto searchParam = new PopupSearchSendParamDto();
                    PopupSearchSendParamDto searchParam9 = new PopupSearchSendParamDto();

                    if (this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value != null && !string.IsNullOrWhiteSpace(this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value.ToString()))
                    {
                        searchParam.And_Or = CONDITION_OPERATOR.AND;
                        searchParam.KeyName = "DENSHU_KBN_CD";
                        searchParam.Value = this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value.ToString();

                        searchParam9.And_Or = CONDITION_OPERATOR.OR;
                        searchParam9.KeyName = "DENSHU_KBN_CD";
                        searchParam9.Value = "9";
                    }

                    target.PopupSearchSendParams[target.PopupSearchSendParams.Count - 1].SubCondition = new Collection<PopupSearchSendParamDto>();
                    target.PopupSearchSendParams[target.PopupSearchSendParams.Count - 1].SubCondition.Add(searchParam);
                    target.PopupSearchSendParams[target.PopupSearchSendParams.Count - 1].SubCondition.Add(searchParam9);
                }
                else if (e.CellName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.NIOROSHI_GYOUSHA_CD))
                {
                    this.previousValue = string.Empty;
                    if (this.form.Ichiran[e.RowIndex, e.CellName].Value != null)
                    {
                        this.previousValue = this.form.Ichiran[e.RowIndex, e.CellName].Value.ToString();
                    }
                }

                // 1行目が新行の場合、適用開始日に本日を設定
                if (this.form.Ichiran.Rows[e.RowIndex].IsNewRow)
                {
                    // 20151102 katen #12048 「システム日付」の基準作成、適用 start
                    //this.form.Ichiran[e.RowIndex, "TEKIYOU_BEGIN"].Value = DateTime.Today;
                    this.form.Ichiran[e.RowIndex, "TEKIYOU_BEGIN"].Value = this.parentForm.sysDate.Date;
                    // 20151102 katen #12048 「システム日付」の基準作成、適用 end
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranCellEnter", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 一覧のセル編集終了イベント
        /// </summary>
        /// <param name="e"></param>
        public bool IchiranCellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                GcCustomMultiRow list = this.form.Ichiran;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                //品名
                if (e.CellName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_CD))
                {
                    this.SearchHinmei(e);
                }

                //運搬業者
                if (e.CellName.Equals("UNPAN_GYOUSHA_CD") && list.Rows.Count > 0 && list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value != null)
                {
                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString()))
                    {
                        // マスタ存在チェック
                        M_GYOUSHA gyousha = new M_GYOUSHA();
                        gyousha.GYOUSHA_CD = this.form.Ichiran[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString();
                        DataTable dt = this.gyoushaDao.GetDataBySqlFile(GET_UNPAN_GYOUSHA_DATA_SQL, gyousha);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            this.form.Ichiran[e.RowIndex, "UNPAN_GYOUSHA_RYAKU"].Value = dt.Rows[0]["UNPAN_GYOUSHA_RYAKU"].ToString();
                        }
                        else
                        {
                            this.form.Ichiran[e.RowIndex, "UNPAN_GYOUSHA_RYAKU"].Value = string.Empty;
                            msgLogic.MessageBoxShow("E020", "業者");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, "UNPAN_GYOUSHA_RYAKU"].Value = string.Empty;
                    }
                }

                //荷降業者
                if (e.CellName.Equals("NIOROSHI_GYOUSHA_CD") && list.Rows.Count > 0 && list[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value != null)
                {
                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value.ToString()))
                    {
                        if (list[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value.ToString() != this.previousValue)
                        {
                            this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.NIOROSHI_GENBA_CD].Value = string.Empty;
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_RYAKU"].Value = string.Empty;
                        }

                        // マスタ存在チェック
                        M_GYOUSHA gyousha = new M_GYOUSHA();
                        gyousha.GYOUSHA_CD = this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value.ToString();
                        DataTable dt = this.gyoushaDao.GetDataBySqlFile(GET_NIOROSHI_GYOUSHA_DATA_SQL, gyousha);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_RYAKU"].Value = dt.Rows[0]["NIOROSHI_GYOUSHA_RYAKU"].ToString();
                        }
                        else
                        {
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_RYAKU"].Value = string.Empty;
                            msgLogic.MessageBoxShow("E020", "業者");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_RYAKU"].Value = string.Empty;
                        this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.NIOROSHI_GENBA_CD].Value = string.Empty;
                        this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_RYAKU"].Value = string.Empty;
                    }
                }

                //荷降現場
                if (e.CellName.Equals("NIOROSHI_GENBA_CD") && list.Rows.Count > 0 && list[e.RowIndex, "NIOROSHI_GENBA_CD"].Value != null)
                {
                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "NIOROSHI_GENBA_CD"].Value.ToString()))
                    {
                        // 荷降先現場CDが入力されている状態で、荷降先業者CDがクリアされていた場合、エラーとする
                        if (list[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value == null || string.IsNullOrWhiteSpace(list[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value.ToString()))
                        {
                            // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                            msgLogic.MessageBoxShow("E051", "荷降業者");
                            // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
                            list[e.RowIndex, "NIOROSHI_GENBA_CD"].Value = string.Empty;
                            if (list.EditingControl != null)
                            {
                                list.EditingControl.Text = string.Empty;
                            }
                            e.Cancel = true;
                            LogUtility.DebugMethodEnd(e);
                            return false;
                        }

                        // マスタ存在チェック
                        M_GENBA genba = new M_GENBA();
                        M_GYOUSHA gyousha = new M_GYOUSHA();
                        genba.GYOUSHA_CD = this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value.ToString();
                        genba.GENBA_CD = this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_CD"].Value.ToString();
                        DataTable dt = this.genbaDao.GetDataBySqlFile(GET_NIOROSHI_GENBA_DATA_SQL, genba);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_RYAKU"].Value = dt.Rows[0]["NIOROSHI_GYOUSHA_RYAKU"].ToString();
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_RYAKU"].Value = dt.Rows[0]["NIOROSHI_GENBA_RYAKU"].ToString();
                        }
                        else
                        {
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_RYAKU"].Value = string.Empty;
                            msgLogic.MessageBoxShow("E020", "現場");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_RYAKU"].Value = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd(e);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                e.Cancel = true;
                LogUtility.Error("ErrorCheckGenba", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(e);
                return true;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                LogUtility.Error("ErrorCheckGenba", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e);
                return true;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        /// <param name="focusSwitch"></param>
        /// <returns></returns>
        public bool IchiranCellSwitchCdName(CellEventArgs e, Const.KobetsuHinmeiTankaHoshuConstans.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case Const.KobetsuHinmeiTankaHoshuConstans.FocusSwitch.IN:
                    // 名称にフォーカス時実行
                    if (e.CellName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_RYAKU))
                    {
                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_CD];
                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    else if (e.CellName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.UNIT_NAME_RYAKU))
                    {
                        this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.UNIT_CD].Visible = true;
                        this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.UNIT_CD].UpdateBackColor(false);

                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.UNIT_CD];
                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case Const.KobetsuHinmeiTankaHoshuConstans.FocusSwitch.OUT:
                    // CDに検証成功後実行
                    if (e.CellName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_CD))
                    {
                        this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_RYAKU].Visible = true;
                        this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_RYAKU].UpdateBackColor(false);
                    }
                    else if (e.CellName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.UNIT_CD))
                    {
                        this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.UNIT_NAME_RYAKU].Visible = true;
                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.UNIT_NAME_RYAKU];
                        this.form.Ichiran[e.RowIndex, Const.KobetsuHinmeiTankaHoshuConstans.UNIT_NAME_RYAKU].UpdateBackColor(false);

                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// 単位チェック
        /// </summary>
        /// <returns></returns>
        internal bool unitCheck(int index)
        {
            bool Iserr = false;
            M_UNIT unit = new M_UNIT();
            unit.UNIT_CD = Convert.ToInt16(this.form.Ichiran.Rows[index].Cells["UNIT_CD"].Value);
            M_UNIT[] result = this.unitDao.GetAllValidData(unit);

            if (result == null || result.Length == 0)
            {
                Iserr = true;
            }
            else
            {
                this.form.Ichiran.Rows[index].Cells["UNIT_CD"].Value = result[0].UNIT_CD.Value;
                this.form.Ichiran.Rows[index].Cells["UNIT_NAME_RYAKU"].Value = result[0].UNIT_NAME_RYAKU;
            }
            return Iserr;
        }

        /// <summary>
        /// 単価フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string FormatSystemTanka(Decimal num, out bool catchErr)
        {
            try
            {
                catchErr = false;
                string format = "#,##0";
                if (!string.IsNullOrWhiteSpace(this.entitySysInfo.SYS_TANKA_FORMAT))
                {
                    format = this.entitySysInfo.SYS_TANKA_FORMAT;
                    switch (this.entitySysInfo.SYS_TANKA_FORMAT_CD.Value)
                    {
                        case 1:
                        case 2:
                            num = Math.Floor(num);
                            break;

                        case 3:
                            num = Math.Floor(num * 10) / 10;
                            break;

                        case 4:
                            num = Math.Floor(num * 100) / 100;
                            break;

                        case 5:
                            num = Math.Floor(num * 1000) / 1000;
                            break;
                    }
                }
                return string.Format("{0:" + format + "}", num);
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("FormatSystemTanka", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return "";
            }
        }

        /// <summary>
        /// 種類情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchSyuruiName(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrWhiteSpace(this.form.SHURUI_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    var tmp = shuruiDao.GetDataByCd(this.form.SHURUI_CD.Text);

                    if (tmp != null)
                    {
                        this.form.SHURUI_NAME_RYAKU.Text = tmp.SHURUI_NAME_RYAKU.ToString();
                    }
                    else
                    {
                        if (e != null)
                        {
                            this.form.SHURUI_NAME_RYAKU.Text = string.Empty;
                            msgLogic.MessageBoxShow("E020", "種類");
                            e.Cancel = true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchSyuruiName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 品名読込み時の既存データ削除用
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateDeleteEntity()
        {
            try
            {
                var parentForm = (MasterBaseForm)this.form.Parent;

                var entityList = new M_KOBETSU_HINMEI_TANKA[this.form.Ichiran.Rows.Count];

                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_KOBETSU_HINMEI_TANKA();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KOBETSU_HINMEI_TANKA>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.KobetsuHinmeiTankaHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }
                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                //this.form.Ichiran.DataSource = dt.GetChanges();

                var kobetsuHinmeiTankaEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_KOBETSU_HINMEI_TANKA> addList = new List<M_KOBETSU_HINMEI_TANKA>();
                foreach (var kobetsuHinmeiTankaEntity in kobetsuHinmeiTankaEntityList)
                {
                    //if (kobetsuHinmeiTankaEntity.DELETE_FLG == isDelete)
                    //{
                    kobetsuHinmeiTankaEntity.DELETE_FLG = true;
                    kobetsuHinmeiTankaEntity.SetValue(this.form.GYOUSHA_CD);
                    kobetsuHinmeiTankaEntity.SetValue(this.form.GENBA_CD);
                    kobetsuHinmeiTankaEntity.SetValue(this.form.TORIHIKISAKI_CD);

                    if (parentForm.bt_process1.Text == "[1]売上")
                    {
                        kobetsuHinmeiTankaEntity.DENPYOU_KBN_CD = 2;
                    }
                    else
                    {
                        kobetsuHinmeiTankaEntity.DENPYOU_KBN_CD = 1;
                    }

                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), kobetsuHinmeiTankaEntity);
                    addList.Add(kobetsuHinmeiTankaEntity);
                    //}
                }

                this.form.Ichiran.DataSource = preDt;

                //登録時まで保持
                this.deleteEntitys = addList.ToArray();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateDeleteEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 基本品名マスタから一覧を取得して、一覧にセット
        /// </summary>
        /// <param name="e"></param>
        public virtual bool LoadingHinmeiListToIchiran()
        {
            try
            {
                M_HINMEI cond = new M_HINMEI();
                if (this.form.SYURUI_SHITEI.Checked)
                {
                    cond.SHURUI_CD = this.form.SHURUI_CD.Text;
                }
                else
                {
                    cond.SHURUI_CD = null;
                }

                //売上、支払で書分け
                DataTable hin;
                var parentForm = (MasterBaseForm)this.form.Parent;

                if ("[1]売上".Equals(parentForm.bt_process1.Text))
                {
                    //支払
                    SetSearchString();
                    hin = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_FROM_KIHON_HINMEI_DATA_SQL_FOR_SHIHARAI
                                                        , this.SearchString
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked
                                                        , this.form.SYURUI_SHITEI.Checked
                                                        , this.form.SHURUI_CD.Text
                                                        );
                }
                else
                {
                    //売上
                    SetSearchString();
                    hin = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_FROM_KIHON_HINMEI_DATA_SQL_FOR_URIAGE
                                                        , this.SearchString
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked
                                                        , this.form.SYURUI_SHITEI.Checked
                                                        , this.form.SHURUI_CD.Text
                                                        );
                }

                if (hin.Rows.Count <= 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "基本品名");
                    return false;
                }

                foreach (DataRow row in hin.Rows)
                {
                    DataRow newHinmeiRow = this.SearchResult.NewRow();
                    newHinmeiRow["DENPYOU_KBN_CD"] = row["DENPYOU_KBN_CD"].ToString();
                    //newHinmeiRow[""] = eRow[""].ToString();
                    newHinmeiRow["HINMEI_CD"] = row["HINMEI_CD"].ToString();
                    newHinmeiRow["HINMEI_NAME_RYAKU"] = row["HINMEI_NAME_RYAKU"].ToString();

                    newHinmeiRow["DENSHU_KBN_CD"] = row["DENSHU_KBN_CD"].ToString();
                    newHinmeiRow["DENSHU_KBN_RYAKU"] = row["DENSHU_KBN_RYAKU"].ToString();

                    if (row["UNIT_CD"].ToString() != "")
                    {
                        newHinmeiRow["UNIT_CD"] = row["UNIT_CD"].ToString();
                        newHinmeiRow["UNIT_NAME_RYAKU"] = row["UNIT_NAME_RYAKU"].ToString();
                    }

                    newHinmeiRow["UNPAN_GYOUSHA_CD"] = row["UNPAN_GYOUSHA_CD"].ToString();
                    newHinmeiRow["UNPAN_GYOUSHA_RYAKU"] = row["UNPAN_GYOUSHA_RYAKU"].ToString();
                    newHinmeiRow["NIOROSHI_GYOUSHA_CD"] = row["NIOROSHI_GYOUSHA_CD"].ToString();
                    newHinmeiRow["NIOROSHI_GYOUSHA_RYAKU"] = row["NIOROSHI_GYOUSHA_RYAKU"].ToString();
                    newHinmeiRow["NIOROSHI_GENBA_CD"] = row["NIOROSHI_GENBA_CD"].ToString();
                    newHinmeiRow["NIOROSHI_GENBA_RYAKU"] = row["NIOROSHI_GENBA_RYAKU"].ToString();
                    // 20151102 katen #12048 「システム日付」の基準作成、適用 start
                    //newHinmeiRow["TEKIYOU_BEGIN"] = DateTime.Today;//デフォルト値を入れる
                    newHinmeiRow["TEKIYOU_BEGIN"] = this.parentForm.sysDate.Date;//デフォルト値を入れる
                    // 20151102 katen #12048 「システム日付」の基準作成、適用 end
                    newHinmeiRow["TANKA"] = row["TANKA"].ToString();

                    this.SearchResult.Rows.Add(newHinmeiRow);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LoadingHinmeiListToIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 品名読込モード時
        /// 削除Flgのついている物を一覧から消す
        /// </summary>
        public bool DeleteForHinmeiLoading()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C021");
                if (result == DialogResult.Yes)
                {
                    var dt = this.SearchResult;

                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow row = dt.Rows[i];

                        if (row["DELETE_FLG"].ToString() == "True")
                        {
                            row.Delete();
                        }
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DeleteForHinmeiLoading", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 現場CDエラーチェック
        /// </summary>
        /// <returns></returns>
        internal bool ErrorCheckGenba()
        {
            try
            {
                var gyoushaCd = this.form.GYOUSHA_CD.Text;
                var genbaCd = this.form.GENBA_CD.Text;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool ren = true;

                if (!string.IsNullOrEmpty(genbaCd))
                {
                    // 業者入力されてない場合
                    if (String.IsNullOrEmpty(gyoushaCd))
                    {
                        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                        msgLogic.MessageBoxShow("E051", "業者");
                        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END

                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_CD.Focus();
                        this.form.GENBA_CD.SelectAll();
                        ren = false;
                    }
                    // 現場情報を取得
                    else if (!string.IsNullOrEmpty(genbaCd))
                    {
                        M_GENBA genbaEntitie = new M_GENBA();
                        genbaEntitie.GENBA_CD = genbaCd;
                        genbaEntitie.GYOUSHA_CD = gyoushaCd;
                        var genbaEntities = this.genbaDao.GetDataBySqlFile(this.GET_GENBA_DATA_SQL, genbaEntitie);
                        if (genbaEntities == null || genbaEntities.Rows.Count <= 0)
                        {
                            msgLogic.MessageBoxShow("E020", "現場");

                            this.form.GENBA_CD.Focus();
                            this.form.GENBA_CD.SelectAll();
                            ren = false;
                        }
                    }

                    if (ren)
                    {
                        M_GYOUSHA gyoushaEntitie = new M_GYOUSHA();
                        gyoushaEntitie.GYOUSHA_CD = gyoushaCd;
                        var gyoushaEntities = this.gyoushaDao.GetAllValidData(gyoushaEntitie).FirstOrDefault();
                        if (null == gyoushaEntities)
                        {
                            msgLogic.MessageBoxShow("E020", "業者");

                            this.form.GYOUSHA_CD.Focus();
                            this.form.GYOUSHA_CD.SelectAll();
                            ren = false;
                        }
                    }
                }
                return ren;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ErrorCheckGenba", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ErrorCheckGenba", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// 20141217 Houkakou 「個別品名単価入力」の日付チェックを追加する　start

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck(out bool catchErr)
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                catchErr = false;
                bool bFlag = false;

                foreach (Row row in this.form.Ichiran.Rows)
                {
                    if (row.Cells["DELETE_FLG"].Value != null && row.Cells["DELETE_FLG"].Value.ToString() == "True"
                        && (row.Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(row.Cells["CREATE_USER"].Value.ToString())))
                    {
                        continue;
                    }
                    row.Cells["TEKIYOU_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                    row.Cells["TEKIYOU_END"].Style.BackColor = Constans.NOMAL_COLOR;

                    string strdate_from = Convert.ToString(row.Cells["TEKIYOU_BEGIN"].Value);
                    string strdate_to = Convert.ToString(row.Cells["TEKIYOU_END"].Value);

                    //nullチェック
                    if (string.IsNullOrEmpty(strdate_from))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(strdate_to))
                    {
                        continue;
                    }

                    DateTime date_from = Convert.ToDateTime(row.Cells["TEKIYOU_BEGIN"].Value);
                    DateTime date_to = Convert.ToDateTime(row.Cells["TEKIYOU_END"].Value);

                    // 日付FROM > 日付TO 場合
                    if (date_to.CompareTo(date_from) < 0)
                    {
                        row.Cells["TEKIYOU_BEGIN"].Style.BackColor = Constans.ERROR_COLOR;
                        row.Cells["TEKIYOU_END"].Style.BackColor = Constans.ERROR_COLOR;

                        bFlag = true;
                    }
                }
                return bFlag;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("DateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion 日付チェック

        /// 20141217 Houkakou 「個別品名単価入力」の日付チェックを追加する　end

        // VUNGUYEN 20150525 #1294 START
        public void IchiranDoubleClick(object sender, EventArgs e)
        {
            if (this.cell != null && this.cell.GcMultiRow != null && this.cell.GcMultiRow.EditingControl != null && this.cell.Name.Equals("TEKIYOU_END"))
            {
                PropertyUtility.SetTextOrValue(this.form.Ichiran.Rows[cell.RowIndex].Cells["TEKIYOU_END"], this.form.Ichiran.Rows[cell.RowIndex].Cells["TEKIYOU_BEGIN"].Value);
                if (string.IsNullOrEmpty(Convert.ToString(this.form.Ichiran.Rows[cell.RowIndex].Cells["TEKIYOU_BEGIN"].Value)))
                {
                    this.cell.GcMultiRow.EditingControl.Text = "";
                }
                else
                {
                    this.cell.GcMultiRow.EditingControl.Text = Convert.ToDateTime(this.form.Ichiran.Rows[cell.RowIndex].Cells["TEKIYOU_BEGIN"].Value).ToString("yyyy/MM/dd");
                }
            }
        }

        // VUNGUYEN 20150525 #1294 END

        /// <summary>
        /// 取引先チェック
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        public bool CheckTorihikisaki(string cd)
        {
            bool ret = true;
            LogUtility.DebugMethodStart(cd);
            if (!string.IsNullOrEmpty(cd))
            {
                cd = cd.PadLeft(6, '0');
            }
            M_TORIHIKISAKI torihikisaki = torihikisakiDao.GetDataByCd(cd);
            if (torihikisaki == null || torihikisaki.DELETE_FLG.IsTrue)
            {
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        public bool CheckGyousha(string cd)
        {
            bool ret = true;
            LogUtility.DebugMethodStart(cd);
            if (!string.IsNullOrEmpty(cd))
            {
                cd = cd.PadLeft(6, '0');
            }
            M_GYOUSHA gyousha = gyoushaDao.GetDataByCd(cd);
            if (gyousha == null || gyousha.DELETE_FLG.IsTrue)
            {
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public bool CheckGenba(string gyoushaCd, string genbaCd)
        {
            bool ret = true;
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);
            M_GENBA con = new M_GENBA();
            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                con.GYOUSHA_CD = gyoushaCd.PadLeft(6, '0');
            }
            if (!string.IsNullOrEmpty(genbaCd))
            {
                con.GENBA_CD = genbaCd.PadLeft(6, '0');
            }
            M_GENBA genba = genbaDao.GetDataByCd(con);
            if (genba == null || genba.DELETE_FLG.IsTrue)
            {
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        #region CongBinh 20201201 #136215
        internal List<string> ListSysId_Copy = null;
        internal bool isCopyData = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" SELECT KON.*  ");
            sql.AppendLine(" ,ISNULL(GEN.GENBA_NAME_RYAKU,N'') AS NIOROSHI_GENBA_RYAKU ");
            sql.AppendLine(" ,ISNULL(GYOU.GYOUSHA_NAME_RYAKU,N'') AS UNPAN_GYOUSHA_RYAKU ");
            sql.AppendLine(" ,ISNULL(DENS.DENSHU_KBN_NAME_RYAKU,N'') AS DENSHU_KBN_RYAKU ");
            sql.AppendLine(" ,ISNULL(HIN.HINMEI_NAME_RYAKU,N'') AS HINMEI_NAME_RYAKU ");
            sql.AppendLine(" ,ISNULL(UNI.UNIT_NAME_RYAKU,N'') AS UNIT_NAME_RYAKU ");
            sql.AppendLine(" ,ISNULL(GYOUSHA.GYOUSHA_NAME_RYAKU,N'') AS NIOROSHI_GYOUSHA_RYAKU ");
            sql.AppendLine(" FROM M_KOBETSU_HINMEI_TANKA KON ");
            sql.AppendLine(" LEFT JOIN M_GYOUSHA GYOU ON GYOU.GYOUSHA_CD = KON.UNPAN_GYOUSHA_CD ");
            sql.AppendLine(" LEFT JOIN M_GYOUSHA GYOUSHA ON GYOUSHA.GYOUSHA_CD = KON.NIOROSHI_GYOUSHA_CD ");
            sql.AppendLine(" LEFT JOIN M_DENSHU_KBN DENS ON DENS.DENSHU_KBN_CD = KON.DENSHU_KBN_CD ");
            sql.AppendLine(" LEFT JOIN M_HINMEI HIN ON HIN.HINMEI_CD = KON.HINMEI_CD ");
            sql.AppendLine(" LEFT JOIN M_UNIT UNI ON UNI.UNIT_CD = KON.UNIT_CD ");
            sql.AppendLine(" LEFT JOIN M_GENBA GEN ON GEN.GYOUSHA_CD = KON.NIOROSHI_GYOUSHA_CD AND GEN.GENBA_CD = KON.NIOROSHI_GENBA_CD ");
            sql.AppendLine(" WHERE KON.DELETE_FLG = 0 ");
            sql.AppendLine(" AND KON.DENPYOU_KBN_CD = " + this.TargetDenpyouKbn);
            sql.AppendLine(" AND KON.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "'");
            sql.AppendLine(" AND KON.GYOUSHA_CD = '" + this.form.GYOUSHA_CD.Text + "'");
            sql.AppendLine(" AND KON.GENBA_CD = '" + this.form.GENBA_CD.Text + "'");
            sql.AppendLine(" AND ((KON.TEKIYOU_BEGIN <= '" + DateTime.Now.ToShortDateString() + "' ");
            sql.AppendLine(" AND KON.TEKIYOU_END >= '" + DateTime.Now.ToShortDateString() + "') ");
            sql.AppendLine(" OR  (KON.TEKIYOU_BEGIN <= '" + DateTime.Now.ToShortDateString() + "' ");
            sql.AppendLine(" AND KON.TEKIYOU_END IS NULL )  ");
            sql.AppendLine(" OR  KON.TEKIYOU_BEGIN > '" + DateTime.Now.ToShortDateString() + "')");
            sql.AppendLine(" ORDER BY KON.DENSHU_KBN_CD, KON.HINMEI_CD, KON.UNIT_CD, KON.UNPAN_GYOUSHA_CD, KON.NIOROSHI_GYOUSHA_CD, KON.NIOROSHI_GENBA_CD, KON.TEKIYOU_BEGIN ");//CongBinh 20210125 #146404
            var tmp = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>().GetDateForStringSql(sql.ToString());
            if (tmp == null || (tmp.Rows.Count == 0) || this.SearchResult == null || this.SearchResult.Rows.Count == 0)//CongBinh 20210125 #146405
            {
                this.form.errmessage.MessageBoxShowError("複写する明細行がありません。");
                return;
            }
            //CongBinh 20210125 #146405 S
            var list = new List<string>();
            foreach (DataRow dr in this.SearchResult.Rows)
            {
                list.Add(dr.Field<Int64>("SYS_ID").ToString());
            }
            for (var i = tmp.Rows.Count - 1; i >= 0; i--)
            {
                if (!list.Contains(tmp.Rows[i].Field<Int64>("SYS_ID").ToString()))
                {
                    tmp.Rows.RemoveAt(i);
                }
            }
            if (tmp == null || tmp.Rows.Count == 0)
            {
                this.form.errmessage.MessageBoxShowError("複写する明細行がありません。");
                return;
            }
            //CongBinh 20210125 #146405 E
            InitialPopupForm initialPopupForm = new InitialPopupForm();
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text) &&
                !string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) &&
                !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                initialPopupForm.TorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                initialPopupForm.GyoushaCd = this.form.GYOUSHA_CD.Text;
            }
            initialPopupForm.DenpyouKbnCd = this.TargetDenpyouKbn;
            var res = initialPopupForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.form.Ichiran.AllowUserToAddRows = true;
                //this.SetSearchResultAll(); //CongBinh 20210125 #146405
                foreach (DataRow row in tmp.Rows) 
                {
                    row["SYS_ID"] = 0;
                    if (!initialPopupForm.IsTekiyou)
                    {
                        row["TEKIYOU_BEGIN"] = initialPopupForm.DateFrom;
                        if (initialPopupForm.DateTo != string.Empty)
                        {
                            row["TEKIYOU_END"] = initialPopupForm.DateTo;
                        }
                        else
                        {
                            row["TEKIYOU_END"] = DBNull.Value;
                        }
                    }
                }
                this.form.TORIHIKISAKI_CD.Text = initialPopupForm.TorihikisakiCd;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = initialPopupForm.TorihikisakiName;
                this.form.GYOUSHA_CD.Text = initialPopupForm.GyoushaCd;
                this.form.GYOUSHA_NAME_RYAKU.Text = initialPopupForm.GyoushaName;
                this.form.GENBA_CD.Text = initialPopupForm.GenbaCd;
                this.form.GENBA_NAME_RYAKU.Text = initialPopupForm.GenbaName;
                this.ListSysId_Copy = initialPopupForm.OutListSysId;
                this.SearchResultAll.Rows.Clear();
                this.isCopyData = true;
                this.SearchResult = tmp;
                this.SetIchiran(true);
            }
        }
        #endregion
    }
}