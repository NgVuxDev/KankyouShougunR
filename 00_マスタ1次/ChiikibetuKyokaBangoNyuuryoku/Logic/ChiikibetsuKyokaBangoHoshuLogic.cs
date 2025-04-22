// $Id: ChiikibetsuKyokaBangoHoshuLogic.cs 55916 2015-07-16 09:07:23Z huangxy@oec-h.com $
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ChiikibetsuKyokaBangoHoshu.APP;
using ChiikibetsuKyokaBangoHoshu.Const;
using ChiikibetsuKyokaBangoHoshu.Dto;
using ChiikibetsuKyokaBangoHoshu.Validator;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using MasterCommon.Utility;
using Microsoft.VisualBasic;
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
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Enums;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.ExternalConnection.CommunicateLib;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;

namespace ChiikibetsuKyokaBangoHoshu.Logic
{
    /// <summary>
    /// 地域別許可番号保守画面のビジネスロジック
    /// </summary>
    public class ChiikibetsuKyokaBangoHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定パス
        /// </summary>
        private readonly string ButtonInfoXmlPath = "ChiikibetsuKyokaBangoHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// 一覧取得用SQLファイルパス
        /// </summary>
        private readonly string GET_ICHIRAN_CHIIKIBETSUKYOKA_DATA_SQL = "ChiikibetsuKyokaBangoHoshu.Sql.GetIchiranDataSql.sql";

        /// <summary>
        /// 全データ取得用SQLファイルパス
        /// </summary>
        private readonly string GET_CHIIKIBETSU_KYOKABANGO_DATA_SQL = "ChiikibetsuKyokaBangoHoshu.Sql.GetChiikibetsuKyokaBangoDataSql.sql";

        /// <summary>
        /// 地域別許可番号_銘柄取得用SQLファイルパス
        /// </summary>
        private readonly string GET_CHIIKIBETSU_KYOKA_MEIGARA_DATA_SQL = "ChiikibetsuKyokaBangoHoshu.Sql.GetChiikibetsuKyokaMeigaraDataSql.sql";

        /// <summary>
        /// 地域別許可番号入力画面Form
        /// </summary>
        private ChiikibetsuKyokaBangoHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 地域別許可番号エンティティ
        /// </summary>
        private M_CHIIKIBETSU_KYOKA[] entitys;

        /// <summary>
        /// 地域別許可番号_銘柄エンティティ
        /// </summary>
        private M_CHIIKIBETSU_KYOKA_MEIGARA[] entitys_meigara;

        /// <summary>
        /// 全検索
        /// </summary>
        private bool isAllSearch;

        /// <summary>
        /// 地域別許可のDao
        /// </summary>
        private IM_CHIIKIBETSU_KYOKADao dao;

        /// <summary>
        /// 地域別許可銘柄のDao
        /// </summary>
        private IM_CHIIKIBETSU_KYOKA_MEIGARADao daoMeigara;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoshaDao;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 地域別許可のDao
        /// </summary>
        private IM_FILE_LINK_CHIIKIBETSU_KYOKADao fileLinkChiikibetsuKyokaDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        // VUNGUYEN 20150525 #1294 START
        public Cell cell;
        // VUNGUYEN 20150525 #1294 END

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal MasterBaseForm parentForm;
        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// ファイルアップロード処理クラス
        /// </summary>
        public FileUploadLogic uploadLogic;

        /// <summary>
        /// 地域CD保持用
        /// </summary>
        public string chiikiCd;

        #endregion

        #region プロパティ

        /// <summary>
        /// 許可区分（1:運搬 2:処分 3:最終処分）
        /// </summary>
        public SqlInt16 KyokaKbn
        {
            get
            {
                short result = (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.UNPAN;
                short.TryParse(this.form.KYOKA_KBN.Text, out result);
                return result;
            }
            set
            {
                this.form.KYOKA_KBN.Text = value.ToString();
            }
        }

        /// <summary>
        /// 特別管理区分（特管の属性であれば[1]）
        /// </summary>
        public bool TokubetsuKanriKbn { get; set; }

        /// <summary>
        /// 保存用業者CD
        /// </summary>
        public string PrevGyoushaCd { get; set; }

        /// <summary>
        /// 保存用現場CD
        /// </summary>
        public string PrevGenbaCd { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索結果(重複チェック用)
        /// </summary>
        public DataTable SearchResultCheck { get; set; }

        /// <summary>
        /// 検索結果(全件)
        /// </summary>
        public DataTable SearchResultAll { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_CHIIKIBETSU_KYOKA SearchString { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public ChiikibetsuKyokaBangoHoshuLogic(ChiikibetsuKyokaBangoHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_KYOKADao>();
            this.daoMeigara = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_KYOKA_MEIGARADao>();
            this.gyoshaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.fileLinkChiikibetsuKyokaDao = DaoInitUtility.GetComponent<IM_FILE_LINK_CHIIKIBETSU_KYOKADao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            this.uploadLogic = new FileUploadLogic();

            LogUtility.DebugMethodEnd();
        }

        #endregion

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

                // オプション非対応
                if (!AppConfig.AppOptions.IsFileUpload())
                {
                    // ファイルアップロードボタン無効化
                    this.parentForm.bt_process4.Text = string.Empty;
                    this.parentForm.bt_process4.Enabled = false;
                }

                // イベントの初期化処理
                this.EventInit();

                // 全コントロール
                this.allControl = this.form.allControl;

                // 検索条件保存領域からフォームへコピー
                // 許可区分
                this.KyokaKbn = Properties.Settings.Default.KyokaKbnValue;
                if (this.KyokaKbn == 0)
                {
                    this.KyokaKbn = (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.UNPAN;
                }

                // 業者コード
                this.form.GYOUSHA_CD.Text = Properties.Settings.Default.GyoshaCDValue_Text;
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = getGyoushaName(this.form.GYOUSHA_CD.Text);
                }

                // 現場コード
                this.form.GENBA_CD.Text = Properties.Settings.Default.GenbaCDValue_Text;
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) &&
                    !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    this.form.GENBA_NAME_RYAKU.Text = getGenbaName(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text);
                }

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                // モード変更
                bool catchErr = this.ModeChange((ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE)(short)this.KyokaKbn);
                if (catchErr)
                {
                    return true;
                }

                // タイトル幅調整
                var title = this.form.controlUtil.FindControl(ControlUtility.GetTopControl(this.form), "lb_title") as Label;
                if (title != null)
                {
                    title.Size = new System.Drawing.Size(450, 35);
                }

                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1981

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        public void SetHyoujiJoukenInit()
        {
            LogUtility.DebugMethodStart();

            if (this.entitySysInfo != null)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
            }

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

                // 検索条件の設定
                this.SetSearchString();

                //---------------------------------------------
                // 一覧データの取得（M_CHIIKIBETSU_KYOKA）
                //---------------------------------------------
                var workTable = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_CHIIKIBETSUKYOKA_DATA_SQL
                                                            , this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                var workTableCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_CHIIKIBETSUKYOKA_DATA_SQL
                                                            , this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                // M_CHIIKIBETSU_KYOKAに存在しない項目での絞り込みはデータ取得後に行う
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
                {
                    switch (this.form.CONDITION_VALUE.DBFieldsName)
                    {
                        // 地域名
                        // 全角/半角・ひらがなカタカナ・大文字小文字を区別しないであいまい検索
                        case ("CHIIKI_NAME_RYAKU"):
                            var table = (
                                from row in workTable.AsEnumerable()
                                let columnID = row.Field<string>(this.form.CONDITION_VALUE.DBFieldsName)
                                where Strings.StrConv(Strings.StrConv(Strings.StrConv(columnID, VbStrConv.Wide, 0), VbStrConv.ProperCase, 0), VbStrConv.Hiragana, 0)
                                        .Contains(Strings.StrConv(Strings.StrConv(Strings.StrConv(this.form.CONDITION_VALUE.Text, VbStrConv.Wide, 0), VbStrConv.ProperCase, 0), VbStrConv.Hiragana, 0))
                                select row
                                );
                            var tableCheck = (
                                from row in workTableCheck.AsEnumerable()
                                let columnID = row.Field<string>(this.form.CONDITION_VALUE.DBFieldsName)
                                where Strings.StrConv(Strings.StrConv(Strings.StrConv(columnID, VbStrConv.Wide, 0), VbStrConv.ProperCase, 0), VbStrConv.Hiragana, 0)
                                        .Contains(Strings.StrConv(Strings.StrConv(Strings.StrConv(this.form.CONDITION_VALUE.Text, VbStrConv.Wide, 0), VbStrConv.ProperCase, 0), VbStrConv.Hiragana, 0))
                                select row
                                );

                            if (table.Count() != 0)
                            {
                                workTable = table.CopyToDataTable();
                                workTableCheck = tableCheck.CopyToDataTable();  //同一SQL文の為、件数チェックはtable.countを使用
                            }
                            else
                            {
                                workTable.Clear();
                                workTableCheck.Clear();
                            }
                            break;
                    }
                }
                this.AddMeigaraColumns(ref workTable);
                this.SearchResult = workTable;
                this.SearchResultCheck = workTableCheck;

                // 対象外も含め、全データを抽出する
                workTable = dao.GetDataBySqlFile(this.GET_CHIIKIBETSU_KYOKABANGO_DATA_SQL, this.SearchString);
                this.AddMeigaraColumns(ref workTable);
                this.SearchResultAll = workTable;

                // 全データ抽出フラグをセットする
                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                //---------------------------------------------
                // 品目データの取得（M_CHIIKIBETSU_KYOKA_MEIGARA）
                //---------------------------------------------
                //this.SearchResult.Columns[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_KBN_CD_FUTSUU].ReadOnly = false;
                this.SearchResult.Columns[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_FUTSUU].ReadOnly = false;
                this.SearchResult.Columns[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_NAME_FUTSUU].ReadOnly = false;
                this.SearchResult.Columns[ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE_FUTSUU].ReadOnly = false;
                //this.SearchResult.Columns[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_KBN_CD_TOKUBETSU].ReadOnly = false;
                this.SearchResult.Columns[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_TOKUBETSU].ReadOnly = false;
                this.SearchResult.Columns[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_NAME_TOKUBETSU].ReadOnly = false;
                this.SearchResult.Columns[ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE_TOKUBETSU].ReadOnly = false;

                // 銘柄情報の検索情報設定
                var searchMeigara = this.GetSearchMeigaraData();
                var dtMeigara = daoMeigara.GetDataBySqlFile(this.GET_CHIIKIBETSU_KYOKA_MEIGARA_DATA_SQL, searchMeigara);

                // 行毎に銘柄情報を設定する
                foreach (DataRow row in this.SearchResult.Rows)
                {
                    string strHaikiKbnCd;
                    string strHaikiShuruiCd;
                    string strHaikiShuruiName;
                    string strTumikae;
                    var chiikiCd = (string)row[ChiikibetsuKyokaBangouNyuuryokuConstans.CHIIKI_CD];

                    // 普通
                    var rowsFutsuu = this.GetRowsByChiikiCd(dtMeigara, chiikiCd, false);
                    this.CreateLabelString(rowsFutsuu, out strHaikiKbnCd, out strHaikiShuruiCd, out strHaikiShuruiName, out strTumikae);
                    //row[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_KBN_CD_FUTSUU] = strHaikiKbnCd;
                    row[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_FUTSUU] = strHaikiShuruiCd;
                    row[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_NAME_FUTSUU] = strHaikiShuruiName;
                    row[ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE_FUTSUU] = strTumikae;

                    // 特管
                    var rowsTokubetsu = this.GetRowsByChiikiCd(dtMeigara, chiikiCd, true);
                    this.CreateLabelString(rowsTokubetsu, out strHaikiKbnCd, out strHaikiShuruiCd, out strHaikiShuruiName, out strTumikae);
                    //row[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_KBN_CD_TOKUBETSU] = strHaikiKbnCd;
                    row[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_TOKUBETSU] = strHaikiShuruiCd;
                    row[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_NAME_TOKUBETSU] = strHaikiShuruiName;
                    row[ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE_TOKUBETSU] = strTumikae;
                }

                //---------------------------------------------
                // 検索値の保存
                //---------------------------------------------
                // 許可区分
                Properties.Settings.Default.KyokaKbnValue = (short)this.KyokaKbn;
                // 業者コード
                Properties.Settings.Default.GyoshaCDValue_Text = this.form.GYOUSHA_CD.Text;
                // 現場コード
                Properties.Settings.Default.GenbaCDValue_Text = this.form.GENBA_CD.Text;

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                // 許可区分、業者コード、現場コードを保存する
                Properties.Settings.Default.Save();

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
        /// CSV
        /// </summary>
        public bool CSV()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    var multirowLocationLogic = new MultiRowIndexCreateLogic();
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

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart();

                var entityList = new M_CHIIKIBETSU_KYOKA[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_CHIIKIBETSU_KYOKA();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_CHIIKIBETSU_KYOKA>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.ChiikibetsuKyokaBangouNyuuryokuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();
                preDt = this.GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();
                var kyokaEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                var addList = new List<M_CHIIKIBETSU_KYOKA>();
                var addMeigaraList = new List<M_CHIIKIBETSU_KYOKA_MEIGARA>();
                foreach (var kyokaEntity in kyokaEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.ChiikibetsuKyokaBangouNyuuryokuConstans.CHIIKI_CD) && n.Value.ToString().Equals(kyokaEntity.CHIIKI_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.ChiikibetsuKyokaBangouNyuuryokuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            // 地域別許可番号
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), kyokaEntity);
                            addList.Add(kyokaEntity);

                            // 地域別許可番号_銘柄
                            addMeigaraList.AddRange(this.GetMeigaraEntity(kyokaEntity, row));
                            break;
                        }
                    }
                }

                this.form.Ichiran.DataSource = preDt;
                this.entitys = addList.ToArray();
                this.entitys_meigara = addMeigaraList.ToArray();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
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

                // 削除処理
                ClearCondition();

                //// 検索処理
                //SetSearchString();

                this.form.GYOUSHA_CD.Focus();
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1981

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// CD重複チェック
        /// </summary>
        /// <param name="windowID">チェック種類</param>
        /// <returns name ="bool">TRUE:重複なし, FALSE:重複あり</returns>
        internal bool DuplicationCheck(WINDOW_ID windowID)
        {
            try
            {
                LogUtility.DebugMethodStart();

                ChiikibetsuKyokaBangoHoshuValidator vali = new ChiikibetsuKyokaBangoHoshuValidator();
                var result = true;

                switch (windowID)
                {
                    case WINDOW_ID.M_CHIIKI:
                        // 地域CD重複チェック
                        result = vali.ChiikiCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);
                        break;

                    case WINDOW_ID.M_HOUKOKUSHO_BUNRUI:
                        // 分類CD重複チェック
                        result = vali.bunruiCDValidator(this.form.Ichiran);
                        break;

                    default:
                        break;
                }

                LogUtility.DebugMethodEnd();

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        public bool CancelCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //20150416 minhhoang edit #1748
                //ClearCondition();
                ClearConditionF7();
                //20150416 minhhoang end edit #1748
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
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
                        foreach (M_CHIIKIBETSU_KYOKA chiikibetsuEntity in this.entitys)
                        {
                            // キー項目設定
                            chiikibetsuEntity.KYOKA_KBN = this.KyokaKbn;
                            chiikibetsuEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                            chiikibetsuEntity.GENBA_CD = this.form.GENBA_CD.Text;
                            chiikibetsuEntity.CHIIKI_CD = chiikibetsuEntity.CHIIKI_CD;

                            // 存在確認チェック
                            var entity = this.dao.GetDataByPrimaryKey(chiikibetsuEntity);
                            if (entity == null)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (chiikibetsuEntity.DELETE_FLG)
                                {
                                    continue;
                                }

                                this.dao.Insert(chiikibetsuEntity);
                            }
                            else
                            {
                                this.dao.Update(chiikibetsuEntity);
                            }

                            // 地域別番号保守に関連する情報の更新
                            this.UpdateRelationInfo(chiikibetsuEntity, false);
                        }
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
                    //INXS check for delete INXS data start refs #158005
                    bool isUploadToInxs = false;
                    if (AppConfig.AppOptions.IsInxsKyokasho())
                    {
                        isUploadToInxs = this.CheckIsUploadToInxs();
                        if (isUploadToInxs && msgLogic.MessageBoxShow("C118") == DialogResult.No)
                        {
                            LogUtility.DebugMethodEnd();
                            return;
                        }
                    }
                    //INXS end

                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_CHIIKIBETSU_KYOKA kyokaEntity in this.entitys)
                        {
                            kyokaEntity.KYOKA_KBN = this.KyokaKbn;
                            kyokaEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                            kyokaEntity.GENBA_CD = this.form.GENBA_CD.Text;
                            kyokaEntity.CHIIKI_CD = kyokaEntity.CHIIKI_CD;
                            kyokaEntity.DELETE_FLG = false;
                            M_CHIIKIBETSU_KYOKA entity = this.dao.GetDataByPrimaryKey(kyokaEntity);
                            if (entity != null)
                            {
                                kyokaEntity.DELETE_FLG = true;
                                this.dao.Update(kyokaEntity);
                                this.UpdateRelationInfo(kyokaEntity, true);
                            }

                            // ファイルデータ削除
                            var list = this.fileLinkChiikibetsuKyokaDao.GetDataByCd(kyokaEntity.KYOKA_KBN.Value, kyokaEntity.GYOUSHA_CD, kyokaEntity.GENBA_CD, kyokaEntity.CHIIKI_CD);
                            if (list != null && 0 < list.Count)
                            {
                                // ファイルデータを物理削除する
                                var fileIdList = list.Select(n => n.FILE_ID.Value).ToList();
                                this.uploadLogic.DeleteFileData(fileIdList);

                                // 連携データ削除
                                string sql = string.Format("DELETE FROM M_FILE_LINK_CHIIKIBETSU_KYOKA WHERE KYOKA_KBN = {0} AND GYOUSHA_CD = '{1}' AND GENBA_CD = '{2}' AND CHIIKI_CD = '{3}'", kyokaEntity.KYOKA_KBN.Value, kyokaEntity.GYOUSHA_CD, kyokaEntity.GENBA_CD, kyokaEntity.CHIIKI_CD);
                                this.fileLinkChiikibetsuKyokaDao.GetDateForStringSql(sql);
                            }
                        }
                        // トランザクション終了
                        tran.Commit();
                    }
					
					//INXS delete data start refs #158005
                    if (isUploadToInxs)
                    {
                        this.DeleteInxsData();
                    }
					////INXS end

                    msgLogic.MessageBoxShow("I001", "削除");
                }

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 地域別番号保守に関連する情報の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isDelete"></param>
        private void UpdateRelationInfo(M_CHIIKIBETSU_KYOKA entity, bool isDelete)
        {
            if (entity == null || string.IsNullOrEmpty(entity.CHIIKI_CD))
            {
                return;
            }

            // 削除
            //this.daoMeigara.DeleteByChiikibetsuKyoka(entity);

            if (!isDelete)
            {
                // 更新データがある場合は許可区分、業者CD、現場CD、地域CDをキーに削除
                this.daoMeigara.DeleteByChiikibetsuKyoka(entity);

                // 登録
                foreach (var insertRec in this.entitys_meigara)
                {
                    M_CHIIKIBETSU_KYOKA_MEIGARA temp = new M_CHIIKIBETSU_KYOKA_MEIGARA();
                    // 地域CDが同一の場合のみ追加
                    if (entity.CHIIKI_CD != insertRec.CHIIKI_CD)
                    {
                        continue;
                    }

                    // キー項目設定
                    temp.KYOKA_KBN = this.KyokaKbn;
                    //主キー再設定する
                    insertRec.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    insertRec.GENBA_CD = this.form.GENBA_CD.Text;
                    temp.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    temp.GENBA_CD = this.form.GENBA_CD.Text;
                    temp.CHIIKI_CD = entity.CHIIKI_CD;
                    temp.TOKUBETSU_KANRI_KBN = insertRec.TOKUBETSU_KANRI_KBN;
                    temp.HOUKOKUSHO_BUNRUI_CD = insertRec.HOUKOKUSHO_BUNRUI_CD;

                    // 存在確認チェック
                    var meigaraEntity = this.daoMeigara.GetAllValidData(temp);
                    if (meigaraEntity.Length <= 0)
                    {
                        this.daoMeigara.Insert(insertRec);
                    }
                    else
                    {
                        this.daoMeigara.Update(insertRec);
                    }
                }
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

        #endregion

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

            ChiikibetsuKyokaBangoHoshuLogic localLogic = other as ChiikibetsuKyokaBangoHoshuLogic;
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

        #endregion

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            try
            {
                var table = this.SearchResult;

                table.AcceptChanges();
                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ichiran.DataSource = table;

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M237", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
                }
                else
                {
                    this.DispReferenceMode();
                }

                this.form.Ichiran.Select();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            // 削除ボタン(F4)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            // CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            // 条件取消ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.CancelCondition);

            // 検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);
            parentForm.bt_func8.ProcessKbn = PROCESS_KBN.NONE;

            // 登録ボタン(F9)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            // 取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // 処理No1 運搬イベント生成
            parentForm.bt_process1.Click += new EventHandler(this.form.ModeChangeUnpan);

            // 処理No2 処分イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.form.ModeChangeShobun);

            // 処理No3 最終処分イベント生成
            parentForm.bt_process3.Click += new EventHandler(this.form.ModeChangeSaishuShobun);

            // ファイルアップロードボタン(process4)イベント生成
            this.form.C_MasterRegist(parentForm.bt_process4);
            parentForm.bt_process4.Click += new EventHandler(this.bt_process4_Click);
            parentForm.bt_process4.ProcessKbn = PROCESS_KBN.NEW;

			//Receive message from INXS Subapp refs #158005
            if (AppConfig.AppOptions.IsInxsKyokasho())
            {
                parentForm.OnReceiveMessageEvent += new BaseBaseForm.OnReceiveMessage(ParentForm_OnReceiveMessageEvent);
            }            
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
            M_CHIIKIBETSU_KYOKA entity = new M_CHIIKIBETSU_KYOKA();

            // 許可区分取得
            entity.KYOKA_KBN = this.KyokaKbn;

            // 業者コード取得
            entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;

            // 現場コード取得
            entity.GENBA_CD = this.form.GENBA_CD.Text;

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    // M_CHIIKIBETSU_KYOKAに存在しない項目は検索条件に含めない
                    switch (this.form.CONDITION_VALUE.DBFieldsName)
                    {
                        case ("CHIIKI_NAME_RYAKU"):
                            break;

                        default:
                            // 検索条件の設定
                            entity.SetValue(this.form.CONDITION_VALUE);
                            break;
                    }
                }
            }

            this.SearchString = entity;
        }

        /// <summary>
        /// 銘柄情報の検索条件設定
        /// </summary>
        /// <returns></returns>
        private M_CHIIKIBETSU_KYOKA_MEIGARA GetSearchMeigaraData()
        {
            var entity = new M_CHIIKIBETSU_KYOKA_MEIGARA();

            // 許可区分取得
            entity.KYOKA_KBN = this.KyokaKbn;

            // 業者コード取得
            entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;

            // 現場コード取得
            entity.GENBA_CD = this.form.GENBA_CD.Text;

            return entity;
        }

        /// <summary>
        /// 業者CDをキーにして業者マスターから業者名を取得する。
        /// </summary>
        /// <param name="gyoushaCD"></param>
        /// <returns></returns>
        public string getGyoushaName(string gyoushaCD)
        {
            LogUtility.DebugMethodStart(gyoushaCD);

            string st = string.Empty;
            M_GYOUSHA gyousha = this.gyoshaDao.GetDataByCd(gyoushaCD);
            if (gyousha != null)
            {
                st = gyousha.GYOUSHA_NAME_RYAKU;
            }

            LogUtility.DebugMethodEnd(gyoushaCD);
            return st;
        }

        /// <summary>
        /// 現場CDをキーにして現場マスターから現場名を取得する。
        /// </summary>
        /// <param name="gyoushaCD"></param>
        /// <param name="genbaCD"></param>
        /// <returns></returns>
        public string getGenbaName(string gyoushaCD, string genbaCD)
        {
            LogUtility.DebugMethodStart(gyoushaCD, genbaCD);

            string st = string.Empty;

            M_GENBA searchParam = new M_GENBA();
            searchParam.GYOUSHA_CD = gyoushaCD;
            searchParam.GENBA_CD = genbaCD;
            M_GENBA genba = this.genbaDao.GetDataByCd(searchParam);
            if (genba != null)
            {
                st = genba.GENBA_NAME_RYAKU;
            }

            LogUtility.DebugMethodEnd(gyoushaCD, genbaCD);
            return st;
        }

        /// <summary>
        /// 許可証参照ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool FileRefClick(bool tokubetsuKanriKbn, int rowIndex)
        {
            try
            {
                LogUtility.DebugMethodStart(tokubetsuKanriKbn, rowIndex);

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M237", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    return false;
                }

                // ユーザ定義情報を取得
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                // ファイルアップロード参照先のフォルダを取得
                string serverPath = this.uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");

                //※※※※※暫定措置※※※※※
                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "参照するファイルを選択してください。";
                var initialPath = @"C:\Temp"; 
                if (!string.IsNullOrEmpty(serverPath))
                {
                    initialPath = serverPath;
                }
                var windowHandle = this.form.Handle;
                var isFileSelect = true;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    var cell = this.GetKyokaFilePath(tokubetsuKanriKbn, rowIndex);
                    cell.Value = filePath;
                }

                LogUtility.DebugMethodEnd(tokubetsuKanriKbn, rowIndex);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FileRefClick", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(tokubetsuKanriKbn, rowIndex);
                return true;
            }
        }

        /// <summary>
        /// 許可証閲覧ボタン押下処理
        /// </summary>
        public bool BrowseClick(bool tokubetsuKanriKbn, int rowIndex)
        {
            try
            {
                LogUtility.DebugMethodStart(tokubetsuKanriKbn, rowIndex);

                var cell = this.GetKyokaFilePath(tokubetsuKanriKbn, rowIndex);
                var value = cell.Value as string;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (!File.Exists(value))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E024", "許可書");
                        return false;
                    }
                    if (SystemProperty.IsTerminalMode)
                    {
                        if (string.IsNullOrEmpty(Shougun.Printing.Common.Initializer.GetXpsFilePrintingDirectoryNonMsg()))
                        {
                            MessageBox.Show("閲覧を行う前に、印刷設定の出力先フォルダを指定してください。",
                                            "アラート",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                            return false;
                        }

                        // クラウド環境でもオンプレと同じようにプロセス起動する
                        string clientFilePathInfo = System.IO.Path.Combine(Shougun.Printing.Common.Initializer.GetXpsFilePrintingDirectory(), "ClientFilePathInfo.txt");

                        // 5回ファイル作成を試す
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                using (var writer = new StreamWriter(clientFilePathInfo, false, Encoding.UTF8))
                                {
                                    writer.Write(value);
                                }
                                break;
                            }
                            catch (Exception e)
                            {
                                System.Threading.Thread.Sleep(100);
                                continue;
                            }
                        }
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(value);
                    }
                }

                LogUtility.DebugMethodEnd(tokubetsuKanriKbn, rowIndex);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BrowseClick", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(tokubetsuKanriKbn, rowIndex);
                return true;
            }
        }

        /// <summary>
        /// 許可証クリアボタン押下処理
        /// </summary>
        /// <param name="tokubetsuKanriKbn"></param>
        /// <param name="rowIndex"></param>
        public bool FileClearClick(bool tokubetsuKanriKbn, int rowIndex)
        {
            try
            {
                LogUtility.DebugMethodStart(tokubetsuKanriKbn, rowIndex);

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M237", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    return false;
                }

                var cell = this.GetKyokaFilePath(tokubetsuKanriKbn, rowIndex);
                cell.Value = string.Empty;

                LogUtility.DebugMethodEnd(tokubetsuKanriKbn, rowIndex);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FileClearClick", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 業者CDチェック処理
        /// </summary>
        public bool CheckGyoushaCD()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 変化なし
                //if (this.PrevGyoushaCd == this.form.GYOUSHA_CD.Text)
                //{
                //    return false;
                //}

                // 業者がクリアされた場合
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    // クリア処理
                    this.IchiranClear();
                    this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    return false;
                }

                // 許可区分に応じた業者の区分を参照する
                var errorFlg = true;
                var gyousha = gyoshaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                if (gyousha != null)
                {
                    // 運搬
                    if (this.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.UNPAN)
                    {
                        // 運搬受託者区分
                        // 20151022 BUNN #12040 STR
                        if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                            errorFlg = false;
                        // 20151022 BUNN #12040 END
                    }
                    // 処分
                    else if (this.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SHOBUN)
                    {
                        // 処分受託者区分
                        // 20151022 BUNN #12040 STR
                        if (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                            errorFlg = false;
                        // 20151022 BUNN #12040 END
                    }
                    // 最終処分
                    else if (this.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SAISHUSHOBUN)
                    {
                        // 処分受託者区分
                        // 20151022 BUNN #12040 STR
                        if (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                            errorFlg = false;
                        // 20151022 BUNN #12040 END
                    }
                }

                if (errorFlg)
                {
                    // レコードがない、またはエラー
                    msgLogic.MessageBoxShow("E020", "業者");

                    // フォーカスを移動させない
                    this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    return errorFlg;
                }
                else if (gyousha.DELETE_FLG.Value)
                {
                    // レコードがある場合でかつ論理削除されている場合
                    msgLogic.MessageBoxShow("E026", "業者CD");

                    // フォーカスを移動させない
                    this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    return errorFlg;
                }
                else
                {
                    // データがある場合でかつ論理削除されていない場合
                    this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    return errorFlg;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGyoushaCD", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyoushaCD", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 現場CDチェック処理
        /// </summary>
        public bool CheckGenbaCD()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 変化なし
                //if (this.PrevGenbaCd == this.form.GENBA_CD.Text)
                //{
                //    return false;
                //}

                // 現場がクリアされた場合
                if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    return false;
                }

                // 業者が選択されていない場合、エラー
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_CD.Focus();
                    // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                }
                else
                {
                    // 現場を検索
                    var genbaWhere = new M_GENBA();
                    genbaWhere.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    genbaWhere.GENBA_CD = this.form.GENBA_CD.Text;
                    var genba = genbaDao.GetDataByCd(genbaWhere);
                    if (genba == null || !this.CheckGenba_ByKyokaKbn(genba))
                    {
                        // レコードがない、もしくは現場チェックエラーの場合
                        msgLogic.MessageBoxShow("E020", "現場");

                        // フォーカスを移動させない
                        //this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        //this.form.GENBA_CD.Focus();
                        return true;
                    }
                    else if (genba.DELETE_FLG.Value)
                    {
                        // レコードがある場合でかつ論理削除されている場合
                        msgLogic.MessageBoxShow("E020", "現場");

                        // フォーカスを移動させない
                        //this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        //this.form.GENBA_CD.Focus();
                        return true;
                    }
                    else
                    {
                        // データがある場合でかつ論理削除されていない場合
                        this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenbaCD", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenbaCD", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 許可区分変更処理
        /// </summary>
        public bool ChangeKyokaKbn()
        {
            try
            {
                // ポップアップ設定(業者CD・現場CD)初期化
                this.form.GYOUSHA_CD.PopupSearchSendParams.Clear();
                this.form.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Clear();
                this.form.GENBA_CD.PopupSearchSendParams.Clear();
                this.form.GENBA_SEARCH_BUTTON.PopupSearchSendParams.Clear();

                // 現場CDに業者を紐付けておく
                var searchDtoGenba1 = new r_framework.Dto.PopupSearchSendParamDto();
                searchDtoGenba1.And_Or = CONDITION_OPERATOR.AND;
                searchDtoGenba1.Control = this.form.GYOUSHA_CD.Name;
                searchDtoGenba1.KeyName = this.form.GYOUSHA_CD.DBFieldsName;
                this.form.GENBA_CD.PopupSearchSendParams.Add(searchDtoGenba1);
                this.form.GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(searchDtoGenba1);

                // 許可区分によって設定内容を変える
                var searchDtoGenba2 = new r_framework.Dto.PopupSearchSendParamDto();
                var searchDtoGyousha1 = new r_framework.Dto.PopupSearchSendParamDto();
                if (this.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.UNPAN)
                {
                    // 業者に区分条件を紐付ける
                    // 20151022 BUNN #12040 STR
                    searchDtoGyousha1.And_Or = CONDITION_OPERATOR.AND;
                    searchDtoGyousha1.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                    searchDtoGyousha1.Value = "1";
                    this.form.GYOUSHA_CD.PopupSearchSendParams.Add(searchDtoGyousha1);
                    this.form.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(searchDtoGyousha1);
                    // 20151022 BUNN #12040 END
                }
                else if (this.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SHOBUN)
                {
                    // 20151022 BUNN #12040 STR
                    // 業者に区分条件を紐付ける
                    searchDtoGyousha1.And_Or = CONDITION_OPERATOR.AND;
                    searchDtoGyousha1.KeyName = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                    searchDtoGyousha1.Value = "1";
                    this.form.GYOUSHA_CD.PopupSearchSendParams.Add(searchDtoGyousha1);
                    this.form.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(searchDtoGyousha1);

                    // 現場に区分条件を紐付ける
                    searchDtoGenba2.And_Or = CONDITION_OPERATOR.AND;
                    searchDtoGenba2.KeyName = "SHOBUN_NIOROSHI_GENBA_KBN";
                    searchDtoGenba2.Value = "1";
                    this.form.GENBA_CD.PopupSearchSendParams.Add(searchDtoGenba2);
                    this.form.GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(searchDtoGenba2);
                    // 20151022 BUNN #12040 END
                }
                else if (this.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SAISHUSHOBUN)
                {
                    // 業者に区分条件を紐付ける
                    // 20151022 BUNN #12040 STR
                    searchDtoGyousha1.And_Or = CONDITION_OPERATOR.AND;
                    searchDtoGyousha1.KeyName = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                    searchDtoGyousha1.Value = "1";
                    this.form.GYOUSHA_CD.PopupSearchSendParams.Add(searchDtoGyousha1);
                    this.form.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(searchDtoGyousha1);
                    // 20151022 BUNN #12040 END

                    // 現場に区分条件を紐付ける
                    searchDtoGenba2.And_Or = CONDITION_OPERATOR.AND;
                    searchDtoGenba2.KeyName = "SAISHUU_SHOBUNJOU_KBN";
                    searchDtoGenba2.Value = "1";
                    this.form.GENBA_CD.PopupSearchSendParams.Add(searchDtoGenba2);
                    this.form.GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(searchDtoGenba2);
                }

                // 業者に区分条件を紐付ける
                var searchDtoDeleteFlg = new r_framework.Dto.PopupSearchSendParamDto();
                searchDtoDeleteFlg.And_Or = CONDITION_OPERATOR.AND;
                searchDtoDeleteFlg.KeyName = "TEKIYOU_FLG";
                searchDtoDeleteFlg.Value = "FALSE";
                this.form.GYOUSHA_CD.PopupSearchSendParams.Add(searchDtoDeleteFlg);
                this.form.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(searchDtoDeleteFlg);

                // 現場に区分条件を紐付ける
                var searchDtoDeleteFlg2 = new r_framework.Dto.PopupSearchSendParamDto();
                searchDtoDeleteFlg2.And_Or = CONDITION_OPERATOR.AND;
                searchDtoDeleteFlg2.KeyName = "TEKIYOU_FLG";
                searchDtoDeleteFlg2.Value = "FALSE";
                this.form.GENBA_CD.PopupSearchSendParams.Add(searchDtoDeleteFlg2);
                this.form.GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(searchDtoDeleteFlg2);

                // 一覧クリア処理
                this.IchiranClear();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ChangeKyokaKbn", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeKyokaKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 一覧クリア処理
        /// </summary>
        public void IchiranClear()
        {
            // 一覧クリア
            this.form.Ichiran.DataSource = null;
            this.SearchResultAll = null;
            this.form.Ichiran.AllowUserToAddRows = false;
            this.SearchResultAll = null;
            this.SearchString = null;
        }

        /// <summary>
        /// モード変更処理
        /// </summary>
        /// <param name="mode"></param>
        public bool ModeChange(ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE mode)
        {
            try
            {
                switch (mode)
                {
                    case ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.UNPAN:
                        // 許可区分、画面IDを変更
                        this.KyokaKbn = (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.UNPAN;
                        this.form.WindowId = WINDOW_ID.M_CHIIKIBETSU_KYOKA_UPN;
                        this.form.Ichiran.Template = this.form.chiikibetsuKyokaBangoHoshuDetail1;
                        this.form.GENBA_CD.Enabled = false;
                        this.form.GENBA_CD.ReadOnly = true;
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        this.form.GENBA_SEARCH_BUTTON.Enabled = false;
                        this.form.GENBA_CD.TabStop = false;
                        break;

                    case ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SHOBUN:
                        // 許可区分、画面IDを変更
                        this.KyokaKbn = (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SHOBUN;
                        this.form.WindowId = WINDOW_ID.M_CHIIKIBETSU_KYOKA_SBN;
                        this.form.Ichiran.Template = this.form.chiikibetsuKyokaBangoHoshuDetail2;
                        this.form.GENBA_CD.Enabled = true;
                        this.form.GENBA_CD.ReadOnly = false;
                        this.form.GENBA_SEARCH_BUTTON.Enabled = true;
                        this.form.GENBA_CD.TabStop = true;
                        break;

                    case ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SAISHUSHOBUN:
                        // 許可区分、画面IDを変更
                        this.KyokaKbn = (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SAISHUSHOBUN;
                        this.form.WindowId = WINDOW_ID.M_CHIIKIBETSU_KYOKA_LAST_SBN;
                        this.form.Ichiran.Template = this.form.chiikibetsuKyokaBangoHoshuDetail2;
                        this.form.GENBA_CD.Enabled = true;
                        this.form.GENBA_CD.ReadOnly = false;
                        this.form.GENBA_SEARCH_BUTTON.Enabled = true;
                        this.form.GENBA_CD.TabStop = true;
                        break;
                }

                // 画面タイトルの再表示
                this.IchiranClear();
                this.form.Ichiran.ReadOnly = true;
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1981
                this.form.HeaderFormInit();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ModeChange", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 登録チェックメソッド設定変更処理
        /// </summary>
        /// <param name="rowIndex">行インデックス</param>
        internal bool Ichiran_ChangeRegistCheckMethod(int rowIndex)
        {
            try
            {
                var futsuuKyokaBeginCell = this.form.Ichiran[rowIndex, Const.ChiikibetsuKyokaBangouNyuuryokuConstans.FUTSUU_KYOKA_BEGIN] as GcCustomDataTime;
                var futsuuKyokaEndCell = this.form.Ichiran[rowIndex, Const.ChiikibetsuKyokaBangouNyuuryokuConstans.FUTSUU_KYOKA_END] as GcCustomDataTime;
                var tokubetsuKyokaBeginCell = this.form.Ichiran[rowIndex, Const.ChiikibetsuKyokaBangouNyuuryokuConstans.TOKUBETSU_KYOKA_BEGIN] as GcCustomDataTime;
                var tokubetsuKyokaEndCell = this.form.Ichiran[rowIndex, Const.ChiikibetsuKyokaBangouNyuuryokuConstans.TOKUBETSU_KYOKA_END] as GcCustomDataTime;
                var futsuuKyokaBegin = futsuuKyokaBeginCell.Value as DateTime?;
                var futsuuKyokaEnd = futsuuKyokaEndCell.Value as DateTime?;
                var tokubetsuKyokaBegin = tokubetsuKyokaBeginCell.Value as DateTime?;
                var tokubetsuKyokaEnd = tokubetsuKyokaEndCell.Value as DateTime?;

                // 登録チェックメソッド初期化処理
                ChiikibetsuKyokaBangoHoshuLogic.InitRegistCheckMethod(
                    futsuuKyokaBeginCell, futsuuKyokaEndCell,
                    tokubetsuKyokaBeginCell, tokubetsuKyokaEndCell);

                // 普通許可有効期限に入力がある場合
                if (futsuuKyokaBegin.HasValue || futsuuKyokaEnd.HasValue)
                {
                    ChiikibetsuKyokaBangoHoshuLogic.SetRequiredRegistCheckMethod(
                        futsuuKyokaBeginCell, futsuuKyokaEndCell);
                }

                // 特別許可有効期限に入力がある場合
                if (tokubetsuKyokaBegin.HasValue || tokubetsuKyokaEnd.HasValue)
                {
                    ChiikibetsuKyokaBangoHoshuLogic.SetRequiredRegistCheckMethod(
                        tokubetsuKyokaBeginCell, tokubetsuKyokaEndCell);
                }

                // 普通・特別いづれにも入力がない場合
                if (!futsuuKyokaBegin.HasValue && !futsuuKyokaEnd.HasValue &&
                    !tokubetsuKyokaBegin.HasValue && !tokubetsuKyokaEnd.HasValue)
                {
                    ChiikibetsuKyokaBangoHoshuLogic.SetRequiredRegistCheckMethod(
                        Const.ChiikibetsuKyokaBangouNyuuryokuConstans.NO_KYOKA_KIGEN_MESSAGE,
                        futsuuKyokaBeginCell, futsuuKyokaEndCell,
                        tokubetsuKyokaBeginCell, tokubetsuKyokaEndCell);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_ChangeRegistCheckMethod", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #region プライベートメソッド

        /// <summary>
        /// 銘柄情報カラム追加
        /// </summary>
        /// <param name="dt"></param>
        private void AddMeigaraColumns(ref DataTable dt)
        {
            //// 廃棄物区分CD【普通】（カンマ区切り）
            //if (!dt.Columns.Contains(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_KBN_CD_FUTSUU))
            //{
            //    dt.Columns.Add(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_KBN_CD_FUTSUU, typeof(String));
            //}

            // 廃棄物種類CD【普通】（カンマ区切り）
            if (!dt.Columns.Contains(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_FUTSUU))
            {
                dt.Columns.Add(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_FUTSUU, typeof(String));
            }

            // 廃棄物種類名【普通】（カンマ区切り）
            if (!dt.Columns.Contains(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_NAME_FUTSUU))
            {
                dt.Columns.Add(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_NAME_FUTSUU, typeof(String));
            }

            // 積替【普通】（カンマ区切り）
            if (!dt.Columns.Contains(ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE_FUTSUU))
            {
                dt.Columns.Add(ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE_FUTSUU, typeof(String));
            }

            //// 廃棄物区分CD【特別】（カンマ区切り）
            //if (!dt.Columns.Contains(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_KBN_CD_TOKUBETSU))
            //{
            //    dt.Columns.Add(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_KBN_CD_TOKUBETSU, typeof(String));
            //}

            // 廃棄物種類CD【特別】（カンマ区切り）
            if (!dt.Columns.Contains(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_TOKUBETSU))
            {
                dt.Columns.Add(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_TOKUBETSU, typeof(String));
            }

            // 廃棄物種類名【特別】（カンマ区切り）
            if (!dt.Columns.Contains(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_NAME_TOKUBETSU))
            {
                dt.Columns.Add(ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_NAME_TOKUBETSU, typeof(String));
            }

            // 積替【特別】（カンマ区切り）
            if (!dt.Columns.Contains(ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE_TOKUBETSU))
            {
                dt.Columns.Add(ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE_TOKUBETSU, typeof(String));
            }
        }

        /// <summary>
        /// 地域CD、特別管理区分毎にデータを絞り込む
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="chiikiCd"></param>
        /// <param name="tokubetsuKanriKbn"></param>
        /// <returns></returns>
        private DataRow[] GetRowsByChiikiCd(DataTable dt, string chiikiCd, bool tokubetsuKanriKbn)
        {
            var sbFilter = new StringBuilder(256);
            sbFilter.AppendFormat("CHIIKI_CD = '{0}'", chiikiCd);
            sbFilter.AppendFormat(" AND TOKUBETSU_KANRI_KBN = {0}", tokubetsuKanriKbn);
            var sbSort = new StringBuilder(256);
            sbSort.Append("HOUKOKUSHO_BUNRUI_CD ASC");
            return dt.Select(sbFilter.ToString(), sbSort.ToString());
        }

        /// <summary>
        /// データテーブルよりカンマ区切り文字列を取得する
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="strHaikiKbnCd">廃棄物区分CD</param>
        /// <param name="strHaikiShuruiCd">廃棄物種類CD</param>
        /// <param name="strHaikiShuruiName">廃棄物種類略称名</param>
        /// <param name="strHaikiShuruiName">積替</param>
        private void CreateLabelString(
            DataRow[] rows,
            out string strHaikiKbnCd,
            out string strHaikiShuruiCd,
            out string strHaikiShuruiName,
            out string strTumikae)
        {
            strHaikiKbnCd = string.Empty;
            strHaikiShuruiCd = string.Empty;
            strHaikiShuruiName = string.Empty;
            strTumikae = string.Empty;

            foreach (DataRow row in rows)
            {
                //strHaikiKbnCd += row[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_KBN_CD].ToString() + ChiikibetsuKyokaBangouNyuuryokuConstans.SEPARATOR.ToString();
                strHaikiShuruiCd += (string)row[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD] + ChiikibetsuKyokaBangouNyuuryokuConstans.SEPARATOR.ToString();
                strHaikiShuruiName += (string)row[ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_NAME_RYAKU] + ChiikibetsuKyokaBangouNyuuryokuConstans.SEPARATOR.ToString();
                bool tsumika = false;
                if (!string.IsNullOrEmpty(row[ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE].ToString()))
                {
                    tsumika = (bool)row[ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE];
                }
                strTumikae += tsumika + ChiikibetsuKyokaBangouNyuuryokuConstans.SEPARATOR.ToString();
            }

            strHaikiKbnCd = RemoveLastSeparator(strHaikiKbnCd);
            strHaikiShuruiCd = RemoveLastSeparator(strHaikiShuruiCd);
            strHaikiShuruiName = RemoveLastSeparator(strHaikiShuruiName);
            strTumikae = RemoveLastSeparator(strTumikae);
        }

        /// <summary>
        /// 末尾のセパレータ文字を削除
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string RemoveLastSeparator(string text)
        {
            if (text.EndsWith(ChiikibetsuKyokaBangouNyuuryokuConstans.SEPARATOR.ToString()))
            {
                return text.Remove(text.Length - 1);
            }
            return text;
        }

        /// <summary>
        /// 地域別許可番号_銘柄エンティティ取得処理
        /// </summary>
        /// <param name="kyokaEntity"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private List<M_CHIIKIBETSU_KYOKA_MEIGARA> GetMeigaraEntity(M_CHIIKIBETSU_KYOKA kyokaEntity, Row row)
        {
            var result = new List<M_CHIIKIBETSU_KYOKA_MEIGARA>();

            // 普通
            var objFutsuu = row[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_FUTSUU].Value;
            //var haikiKbnCdFutsuu = GetSeparateString(row[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_KBN_CD_FUTSUU].Value);
            var haikiShuruiCdFutsuu = GetSeparateString(row[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_FUTSUU].Value);
            var tumikaeFutsuu = GetSeparateString(row[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE_FUTSUU].Value);
            if (DBNull.Value != objFutsuu && !string.IsNullOrWhiteSpace((string)objFutsuu))
            {
                for (int i = 0; i < haikiShuruiCdFutsuu.Length; i++)
                {
                    var entity = new M_CHIIKIBETSU_KYOKA_MEIGARA();
                    entity.GYOUSHA_CD = kyokaEntity.GYOUSHA_CD;
                    entity.GENBA_CD = kyokaEntity.GENBA_CD;
                    entity.CHIIKI_CD = kyokaEntity.CHIIKI_CD;
                    entity.TOKUBETSU_KANRI_KBN = false;
                    //entity.HAIKI_KBN_CD = Int16.Parse(haikiKbnCdFutsuu[i]);
                    entity.HOUKOKUSHO_BUNRUI_CD = haikiShuruiCdFutsuu[i];
                    entity.KYOKA_KBN = this.KyokaKbn;
                    entity.TSUMIKAE = false;
                    if (tumikaeFutsuu.Length == haikiShuruiCdFutsuu.Length)
                    {
                        if (!string.IsNullOrEmpty(tumikaeFutsuu[i]))
                        {
                            entity.TSUMIKAE = Convert.ToBoolean(tumikaeFutsuu[i]);
                        }
                    }
                    result.Add(entity);
                }
            }

            // 特別
            var objTokubetsu = row[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_TOKUBETSU].Value;
            //var haikiKbnCdTokubetsu = GetSeparateString(row[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_KBN_CD_TOKUBETSU].Value);
            var haikiShuruiCdTokubetsu = GetSeparateString(row[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.HAIKI_SHURUI_CD_TOKUBETSU].Value);
            var tumikaeTokubetsu = GetSeparateString(row[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.TSUMIKAE_TOKUBETSU].Value);
            if (DBNull.Value != objTokubetsu && !string.IsNullOrWhiteSpace((string)objTokubetsu))
            {
                for (int i = 0; i < haikiShuruiCdTokubetsu.Length; i++)
                {
                    var entity = new M_CHIIKIBETSU_KYOKA_MEIGARA();
                    entity.GYOUSHA_CD = kyokaEntity.GYOUSHA_CD;
                    entity.GENBA_CD = kyokaEntity.GENBA_CD;
                    entity.CHIIKI_CD = kyokaEntity.CHIIKI_CD;
                    entity.TOKUBETSU_KANRI_KBN = true;
                    //entity.HAIKI_KBN_CD = Int16.Parse(haikiKbnCdTokubetsu[i]);
                    entity.HOUKOKUSHO_BUNRUI_CD = haikiShuruiCdTokubetsu[i];
                    entity.KYOKA_KBN = this.KyokaKbn;
                    entity.TSUMIKAE = false;
                    if (tumikaeTokubetsu.Length == haikiShuruiCdTokubetsu.Length)
                    {
                        if (!string.IsNullOrEmpty(tumikaeTokubetsu[i]))
                        {
                            entity.TSUMIKAE = Convert.ToBoolean(tumikaeTokubetsu[i]);
                        }
                    }
                    result.Add(entity);
                }
            }

            return result;
        }

        /// <summary>
        /// 文字分割処理
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string[] GetSeparateString(object obj)
        {
            var strTemp = (obj != DBNull.Value ? (string)obj : string.Empty);
            return strTemp.Split(Const.ChiikibetsuKyokaBangouNyuuryokuConstans.SEPARATOR);
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
            // 業者コード、業者名クリア
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;

            // 現場コード、現場名クリア
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;

            // 検索条件クリア
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

            // 一覧明細クリア
            this.SearchResult = null;
            this.SearchResultAll = null;
            this.form.Ichiran.DataSource = this.SearchResult;
        }

        #region 20150416 minhhoang edit #1748

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearConditionF7()
        {
            // 検索条件クリア
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            this.SetHyoujiJoukenInit();
        }

        #endregion

        /// <summary>
        /// 許可ファイルパス取得
        /// </summary>
        /// <returns></returns>
        private Cell GetKyokaFilePath(bool tokubetsuKanriKbn, int rowIndex)
        {
            string name = string.Empty;
            if (tokubetsuKanriKbn)
            {
                name = "TOKUBETSU_KYOKA_FILE_PATH";
            }
            else
            {
                name = "FUTSUU_KYOKA_FILE_PATH";
            }

            return this.form.Ichiran[rowIndex, name];
        }

        /// <summary>
        /// 登録チェックメソッドを初期化する
        /// </summary>
        /// <param name="iControls">対象コントロール</param>
        private static void InitRegistCheckMethod(params ICustomControl[] iControls)
        {
            foreach (var iControl in iControls)
            {
                iControl.RegistCheckMethod = new Collection<r_framework.Dto.SelectCheckDto>();
            }
        }

        /// <summary>
        /// 登録チェックメソッドに必須チェックを設定する
        /// </summary>
        /// <param name="iControls">対象コントロール</param>
        private static void SetRequiredRegistCheckMethod(params ICustomControl[] iControls)
        {
            ChiikibetsuKyokaBangoHoshuLogic.SetRequiredRegistCheckMethod(string.Empty, iControls);
        }

        /// <summary>
        /// 登録チェックメソッドに必須チェックを設定する
        /// </summary>
        /// <param name="displayMessage">表示メッセージ</param>
        /// <param name="iControls">対象コントロール</param>
        private static void SetRequiredRegistCheckMethod(string displayMessage, params ICustomControl[] iControls)
        {
            foreach (var iControl in iControls)
            {
                var selectCheckDto = new r_framework.Dto.SelectCheckDto();
                selectCheckDto.CheckMethodName = ChiikibetsuKyokaBangouNyuuryokuConstans.REQUIRED_CHECK_NAME;
                selectCheckDto.DisplayMessage = displayMessage;
                iControl.RegistCheckMethod.Add(selectCheckDto);
            }
        }

        /// <summary>
        /// 許可区分による現場チェック
        /// </summary>
        /// <param name="genba">現場エンティティ</param>
        /// <returns>結果</returns>
        private bool CheckGenba_ByKyokaKbn(M_GENBA genba)
        {
            var result = true;

            // 許可区分によってチェック内容を変える
            if (this.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SHOBUN)
            {
                // 許可区分が処分の場合、現場は処分事業場区分がTRUEでなければならない
                // 20151022 BUNN #12040 STR
                if (!genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue)
                    result = false;
                // 20151022 BUNN #12040 END
            }
            else if (this.KyokaKbn == (short)ChiikibetsuKyokaBangouNyuuryokuConstans.KYOKA_MODE.SAISHUSHOBUN)
            {
                // 許可区分が最終処分の場合、現場は最終処分場区分がTRUEでなければならない
                if (!genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                    result = false;
            }

            return result;
        }

        #endregion

        // 20141217 Houkakou 「地域別許可番号入力」の日付チェックを追加する　start

        #region 普通許可日付チェック

        /// <summary>
        /// 普通許可日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool FutsuKyokaDateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            bool bFlag = false;

            foreach (Row row in this.form.Ichiran.Rows)
            {
                if ((row.Cells["DELETE_FLG"].Value != null && row.Cells["DELETE_FLG"].Value.ToString().Equals("True"))
                    && (row.Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(row.Cells["CREATE_USER"].Value.ToString())))
                {
                    continue;
                }
                row.Cells["FUTSUU_KYOKA_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                row.Cells["FUTSUU_KYOKA_END"].Style.BackColor = Constans.NOMAL_COLOR;

                string strdate_from = Convert.ToString(row.Cells["FUTSUU_KYOKA_BEGIN"].Value);
                string strdate_to = Convert.ToString(row.Cells["FUTSUU_KYOKA_END"].Value);

                //nullチェック
                if (string.IsNullOrEmpty(strdate_from))
                {
                    continue;
                }
                if (string.IsNullOrEmpty(strdate_to))
                {
                    continue;
                }

                DateTime date_from = Convert.ToDateTime(row.Cells["FUTSUU_KYOKA_BEGIN"].Value);
                DateTime date_to = Convert.ToDateTime(row.Cells["FUTSUU_KYOKA_END"].Value);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    row.Cells["FUTSUU_KYOKA_BEGIN"].Style.BackColor = Constans.ERROR_COLOR;
                    row.Cells["FUTSUU_KYOKA_END"].Style.BackColor = Constans.ERROR_COLOR;

                    bFlag = true;
                }
            }
            return bFlag;
        }

        #endregion

        #region 特別許可日付チェック

        /// <summary>
        /// 特別許可日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool TokubetsuKyokaDateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            bool bFlag = false;

            foreach (Row row in this.form.Ichiran.Rows)
            {
                if ((row.Cells["DELETE_FLG"].Value != null && row.Cells["DELETE_FLG"].Value.ToString() == "True")
                    && (row.Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(row.Cells["CREATE_USER"].Value.ToString())))
                {
                    continue;
                }
                row.Cells["TOKUBETSU_KYOKA_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                row.Cells["TOKUBETSU_KYOKA_END"].Style.BackColor = Constans.NOMAL_COLOR;

                string strdate_from = Convert.ToString(row.Cells["TOKUBETSU_KYOKA_BEGIN"].Value);
                string strdate_to = Convert.ToString(row.Cells["TOKUBETSU_KYOKA_END"].Value);

                //nullチェック
                if (string.IsNullOrEmpty(strdate_from))
                {
                    continue;
                }
                if (string.IsNullOrEmpty(strdate_to))
                {
                    continue;
                }

                DateTime date_from = Convert.ToDateTime(row.Cells["TOKUBETSU_KYOKA_BEGIN"].Value);
                DateTime date_to = Convert.ToDateTime(row.Cells["TOKUBETSU_KYOKA_END"].Value);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    row.Cells["TOKUBETSU_KYOKA_BEGIN"].Style.BackColor = Constans.ERROR_COLOR;
                    row.Cells["TOKUBETSU_KYOKA_END"].Style.BackColor = Constans.ERROR_COLOR;

                    bFlag = true;
                }
            }
            return bFlag;
        }

        #endregion

        #region 全部日付チェック

        /// <summary>
        /// 全部日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool AllDateCheck()
        {
            try
            {
                var strErrorMessage = new StringBuilder(256);

                bool bFlag = false;

                if (this.FutsuKyokaDateCheck())
                {
                    strErrorMessage.AppendLine(string.Format("{1}が{0}より前の日付になっています。\n{1}には{0}以降の日付を指定してください。", "普通許可有効期限From", "普通許可有効期限To"));
                    bFlag = true;
                }
                if (this.TokubetsuKyokaDateCheck())
                {
                    strErrorMessage.AppendLine(string.Format("{1}が{0}より前の日付になっています。\n{1}には{0}以降の日付を指定してください。", "特管許可有効期限From", "特管許可有効期限To"));
                    bFlag = true;
                }
                if (bFlag == true)
                {
                    MessageBox.Show(strErrorMessage.ToString(), "アラート", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return bFlag;
            }
            catch (Exception ex)
            {
                LogUtility.Error("AllDateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #endregion

        // 20141217 Houkakou 「地域別許可番号入力」の日付チェックを追加する　end

        // VUNGUYEN 20150525 #1294 START
        public void Ichiran_DoubleClick(object sender, EventArgs e)
        {
            if (this.cell != null && this.cell.GcMultiRow != null && this.cell.GcMultiRow.EditingControl != null && this.cell.Name.Equals("TOKUBETSU_KYOKA_END"))
            {
                PropertyUtility.SetTextOrValue(this.form.Ichiran.Rows[cell.RowIndex].Cells["TOKUBETSU_KYOKA_END"], this.form.Ichiran.Rows[cell.RowIndex].Cells["TOKUBETSU_KYOKA_BEGIN"].Value);
                if (string.IsNullOrEmpty(Convert.ToString(this.form.Ichiran.Rows[cell.RowIndex].Cells["TOKUBETSU_KYOKA_BEGIN"].Value)))
                {
                    this.cell.GcMultiRow.EditingControl.Text = "";
                }
                else
                {
                    this.cell.GcMultiRow.EditingControl.Text = Convert.ToDateTime(this.form.Ichiran.Rows[cell.RowIndex].Cells["TOKUBETSU_KYOKA_BEGIN"].Value).ToString("yyyy/MM/dd");
                }
            }
            if (this.cell != null && this.cell.GcMultiRow != null && this.cell.GcMultiRow.EditingControl != null && this.cell.Name.Equals("FUTSUU_KYOKA_END"))
            {
                PropertyUtility.SetTextOrValue(this.form.Ichiran.Rows[cell.RowIndex].Cells["FUTSUU_KYOKA_END"], this.form.Ichiran.Rows[cell.RowIndex].Cells["FUTSUU_KYOKA_BEGIN"].Value);
                if (string.IsNullOrEmpty(Convert.ToString(this.form.Ichiran.Rows[cell.RowIndex].Cells["FUTSUU_KYOKA_BEGIN"].Value)))
                {
                    this.cell.GcMultiRow.EditingControl.Text = "";
                }
                else
                {
                    this.cell.GcMultiRow.EditingControl.Text = Convert.ToDateTime(this.form.Ichiran.Rows[cell.RowIndex].Cells["FUTSUU_KYOKA_BEGIN"].Value).ToString("yyyy/MM/dd");
                }
            }
        }
        // VUNGUYEN 20150525 #1294 END

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.CHIIKI_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Select(r => r.Cells["CHIIKI_CD"]).Where(c => c.Value != null).ToList().
                                            ForEach(c =>
                                            {
                                                c.ReadOnly = allEntityList.Contains(c.Value.ToString());
                                                c.UpdateBackColor(false);
                                            });
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("EditableToPrimaryKey", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// ファイルアップロードボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process4_Click(object sender, EventArgs e)
        {
            if (this.form.RegistErrorFlag)
            {
                return;
            }
            // 日付チェック
            if (this.AllDateCheck())
            {
                return;
            }

            // 検索実行前に登録を押下された場合の対応
            if (this.form.Ichiran.DataSource == null || this.form.Ichiran.RowCount == 0)
            {
                this.form.errmessage.MessageBoxShow("E064", "登録処理");
                return;
            }

            if (this.form.Ichiran.CurrentRow == null)
            {
                this.form.errmessage.MessageBoxShowError("明細行を選択してください。");
                return;
            }

            // ユーザ定義情報を取得
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            // ファイルアップロード参照先のフォルダを取得
            string serverPath = this.uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");

            // ファイルアップロード用DB接続を確立
            if (!this.uploadLogic.CanConnectDB())
            {
                this.form.errmessage.MessageBoxShowError("ファイルアップロード用DBに接続できませんでした。\n接続情報を確認してください。");
            }
            // システム個別設定入力の初期フォルダの設定有無をチェックする。
            else if (string.IsNullOrEmpty(serverPath) || !Directory.Exists(serverPath))
            {
                this.form.errmessage.MessageBoxShowError("システム個別設定入力 - ファイルアップロード - 初期フォルダへ\r\nフォルダ情報を入力してください。");
            }
            else
            {
                // 選択行の地域CDを保持する。
                this.chiikiCd = this.form.Ichiran.CurrentRow[ChiikibetsuKyokaBangouNyuuryokuConstans.CHIIKI_CD].Value.ToString();

                // ファイルアップロード画面に渡す許可区分、業者CD、現場CD、地域CD
                string[] paramList = new string[4];

                if (this.form.errmessage.MessageBoxShowConfirm("ファイルアップロードの事前処理として登録処理を行います。よろしいですか？", MessageBoxDefaultButton.Button1)
                        != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }

                // 地域別許可番号の登録処理
                this.form.Regist(sender, e);

                // ファイルアップロード画面を起動
                paramList[0] = this.KyokaKbn.ToString();
                paramList[1] = string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) ? "" : this.form.GYOUSHA_CD.Text;
                paramList[2] = string.IsNullOrEmpty(this.form.GENBA_CD.Text) ? "" : this.form.GENBA_CD.Text;
                paramList[3] = string.IsNullOrEmpty(this.chiikiCd) ? "" : this.chiikiCd;

                List<long> fileIdList = null;
                var fileLink = this.fileLinkChiikibetsuKyokaDao.GetDataByCd(this.KyokaKbn.Value, paramList[1], paramList[2], paramList[3]);
                if (fileLink != null)
                {
                    fileIdList = fileLink.Select(n => n.FILE_ID.Value).ToList();
                }

                FormManager.OpenFormModal("G730", fileIdList, WINDOW_ID.M_CHIIKIBETSU_KYOKA, paramList);

            }
            
        }

        #region INXS処理  refs #158005

        public bool CheckIsUploadToInxs()
        {
            bool result = false;
            try
            {
                foreach (M_CHIIKIBETSU_KYOKA kyokaEntity in this.entitys)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" SELECT * FROM M_LICENSE_UPLOAD_STATUS_INXS ");
                    sql.AppendFormat(" WHERE LICENSE_TYPE = {0} ", this.KyokaKbn.Value);
                    sql.AppendFormat(" AND GYOUSHA_CD = '{0}' ", this.form.GYOUSHA_CD.Text);
                    sql.AppendFormat(" AND GENBA_CD = '{0}' ", this.form.GENBA_CD.Text);
                    sql.AppendFormat(" AND AREA_CD = '{0}' ", kyokaEntity.CHIIKI_CD);

                    DataTable dt = this.dao.GetDataForStringSql(sql.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsUploadToInxs", ex);
                result = false;
            }

            return result;
        }

        public void DeleteInxsData()
        {
            List<INXSInfoDto> commandArgs = new List<INXSInfoDto>();
            foreach (M_CHIIKIBETSU_KYOKA kyokaEntity in this.entitys)
            {
                commandArgs.Add(new INXSInfoDto()
                {
                    LicenseType = this.KyokaKbn.Value,
                    GyoushaCd = this.form.GYOUSHA_CD.Text,
                    GenbaCd = this.form.GENBA_CD.Text,
                    AreaCd= kyokaEntity.CHIIKI_CD
                });
            }

            var requestDto = new
            {
                CommandName = 11, //DeleteInxsLicenseByKeys
                ShougunParentWindowName = ((MasterBaseForm)this.form.Parent).Text,
                CommandArgs = commandArgs
            };

            RemoteAppCls remoteAppCls = new RemoteAppCls();
            var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
            {
                TransactionId = this.form.transactionId,
                ReferenceID = -1
            });
            var execCommandDto = new ExecuteCommandDto()
            {
                Token = token,
                Type = Shougun.Core.ExternalConnection.CommunicateLib.Enums.NotificationType.ExecuteCommand,
                Args = new object[] { JsonUtility.SerializeObject(requestDto) }
            };
            remoteAppCls.ExecuteCommand(Constans.StartFormText, execCommandDto);
        }

        private void ParentForm_OnReceiveMessageEvent(string message)
        {
            if (!AppConfig.AppOptions.IsInxsKyokasho())
            {
                return;
            }
            if (!string.IsNullOrEmpty(message))
            {
                var arg = JsonUtility.DeserializeObject<CommunicateAppDto>(message);
                if (arg != null)
                {
                    var msgDto = (CommunicateAppDto)arg;
                    var token = JsonUtility.DeserializeObject<CommunicateTokenDto>(msgDto.Token);
                    if (token != null)
                    {
                        if (token.TransactionId == this.form.transactionId)
                        {
                            if (msgDto.Args.Length > 0 && msgDto.Args[0] != null)
                            {
                                var responeDto = JsonUtility.DeserializeObject<InxsExecuteResponseDto>(msgDto.Args[0].ToString());
                                if (responeDto != null && responeDto.MessageType == EnumMessageType.ERROR)
                                {
                                    this.form.errmessage.MessageBoxShowError(responeDto.ResponseMessage);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}