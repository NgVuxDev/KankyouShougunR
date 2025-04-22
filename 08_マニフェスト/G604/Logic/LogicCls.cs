// $Id: LogicCls.cs 28443 2014-08-25 02:37:46Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu.DAO;
using GrapeCity.Win.MultiRow;

namespace Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class JissekiHokokuSyuseiSisetsuLogic : IBuisinessLogic
    {
        /// <summary>
        /// 実績報告書データ用DAO
        /// </summary>
        private JissekiHokokuSyuseiSisetsuDao dao;

        /// <summary>
        /// 実績報告書修正明細データ用DAO
        /// </summary>
        private JissekiHokokuShoriDetailDao detailDao;

        /// <summary>
        /// M_CHIIKIBETSU_BUNRUIデータ取得用Dao
        /// </summary>
        private ChiikibetsuBunruiDao chikiDao;

        /// <summary>
        /// M_CHIIKIBETSU_SHOBUNデータ取得用Dao
        /// </summary>
        private ChiikibetsuShobunDao chikiShobunDao;

        /// <summary>
        /// S_NUMBER_SYSTEMデータ取得用Dao
        /// </summary>
        private ManiDetailDAO maniDetailDao;

        /// <summary>
        /// S_NUMBER_SYSTEMデータ取得用Dao
        /// </summary>
        private IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// M_GENBAデータ取得用Dao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// M_CHIIKIBETSU_KYOKAデータ取得用Dao
        /// </summary>
        private IM_CHIIKIBETSU_KYOKADao chikiBetsuDao;

        private EntryDAO EntryDao;
        private BunruiDAO BunruiDAO;

        /// <summary>
        /// 検索結果(共通)
        /// </summary>
        public DataTable SearchResult { get; set; }

        public List<T_JISSEKI_HOUKOKU_SHORI_DETAIL> tjHoukokuSyoriEntryList { get; set; }

        /// <summary>
        /// Form
        /// </summary>
        private JissekiHokokuSyuseiSisetsuForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 実績報告書データ
        /// </summary>
        DataTable resultData;

        /// <summary>
        /// MultiRow初期化用DataTable
        /// </summary>
        DataTable multiRowDataTable;

        /// <summary>
        /// 実績報告書修正ヘッダーデータ
        /// </summary>
        T_JISSEKI_HOUKOKU_ENTRY headerData;

        /// <summary>
        /// 実績報告書修正ヘッダー更新データ
        /// </summary>
        T_JISSEKI_HOUKOKU_ENTRY updateheaderData;

        /// <summary>
        /// 実績報告書修正明細データ
        /// </summary>
        List<T_JISSEKI_HOUKOKU_SHORI_DETAIL> detailData;

        /// <summary>
        /// 実績報告書修正明細更新データ
        /// </summary>
        List<T_JISSEKI_HOUKOKU_SHORI_DETAIL> updateDetailData;

        /// <summary>
        /// 実績報告書修正マニフェスト明細更新データ
        /// </summary>
        List<T_JISSEKI_HOUKOKU_MANIFEST_DETAIL> updateManiDetailData;

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu.Setting.ButtonSetting.xml";

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        HeaderForm headerForm;

        /// <summary>
        /// G603 logic
        /// </summary>
        Shougun.Core.PaperManifest.JissekiHokokuSisetsu.LogicClass logic;

        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        public M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        public IM_SYS_INFODao sysInfoDao;

        private Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public JissekiHokokuSyuseiSisetsuLogic(JissekiHokokuSyuseiSisetsuForm targetForm)
        {
            LogUtility.DebugMethodStart();

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<JissekiHokokuSyuseiSisetsuDao>();
            this.detailDao = DaoInitUtility.GetComponent<JissekiHokokuShoriDetailDao>();
            this.chikiDao = DaoInitUtility.GetComponent<ChiikibetsuBunruiDao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.chikiBetsuDao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_KYOKADao>();
            this.chikiShobunDao = DaoInitUtility.GetComponent<ChiikibetsuShobunDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();
            this.maniDetailDao = DaoInitUtility.GetComponent<ManiDetailDAO>();
            this.EntryDao = DaoInitUtility.GetComponent<EntryDAO>();
            this.BunruiDAO = DaoInitUtility.GetComponent<BunruiDAO>();
            this.MsgBox = new MessageBoxShowLogic();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.grdIchiran.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                // コントロールを初期化
                this.ControlInit();
                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();
                // 実績報告書修正データを取得
                this.GetJissekiHokokuData(this.form.systemid);

                // ヘッダーデータ設定
                this.SetHeaderData();
                // MultiRow初期化用DataTable
                this.MultiRowDataTableInit();
                // MultiRowにデータを追加
                this.MultiRowInit();

                // システム設定を読み込む
                M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                }
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
        #endregion

        #region 初期表示イベント
        /// <summary>
        /// 表示イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // フォーカス設定
            this.form.header.Select();
            this.form.header.Focus();
        }
        #endregion

        #region 初期データ取得
        /// <summary>
        /// 初期データ取得
        /// </summary>
        /// <param name="systemid"></param>
        /// <param name="seq"></param>
        private void GetJissekiHokokuData(string systemid)
        {
            this.resultData = this.dao.GetJissekiHokokuData(systemid);
            // 取得したのデータはNULLの場合。
            if (this.resultData == null || this.resultData.Rows.Count == 0)
            {
                return;
            }

            string seq = this.resultData.Rows[0]["SEQ"].ToString();
            this.headerData = this.dao.GetJissekiHokokuHeadData(systemid, seq);
            this.updateheaderData = this.dao.GetJissekiHokokuHeadData(systemid, seq);
            this.detailData = this.detailDao.GetJissekiHokokuDetailData(systemid, seq);
        }
        #endregion

        #region 初期ヘッダーデータ設定
        /// <summary>
        /// 初期ヘッダーデータ設定
        /// </summary>
        private void SetHeaderData()
        {
            if (this.resultData == null || this.resultData.Rows.Count == 0)
            {
                return;
            }
            // 実績報告書修正.報告年度
            if (this.resultData.Rows[0]["HOKOKU_NENDO"] != null)
            {
                this.form.HokokuNendo.Text = this.resultData.Rows[0]["HOKOKU_NENDO"].ToString();
            }
            // 実績報告書修正.提出先
            this.form.TeishutuSakiCd.Text = Convert.ToString(this.resultData.Rows[0]["CHIIKI_CD"]);

            if (this.resultData.Rows[0]["CHIIKI_NAME"] != null)
            {
                this.form.TeishutuSaki.Text = this.resultData.Rows[0]["CHIIKI_NAME"].ToString();
            }
            // 実績報告書修正.自社業種区分
            this.form.JishaGhoushuKbn.Text = "自社業種区分なし";
            //if (this.resultData.Rows[0]["GYOUSHA_KBN_NAME"] != null)
            //{
            //    this.form.JishaGhoushuKbn.Text = this.resultData.Rows[0]["GYOUSHA_KBN_NAME"].ToString();
            //}
            // 実績報告書修正.保存年月日
            if (this.resultData.Rows[0]["UPDATE_DATE"] != null)
            {
                this.form.HozonYmd.Text = this.resultData.Rows[0]["UPDATE_DATE"].ToString();
            }
            // 実績報告書修正.特管区分
            if (this.resultData.Rows[0]["TOKUBETSU_KANRI_SYURUI"] != null)
            {
                this.form.TokuKbn.Text = this.resultData.Rows[0]["TOKUBETSU_KANRI_SYURUI"].ToString();
            }
            // 実績報告書修正.提出書式
            if (this.resultData.Rows[0]["HOUKOKU_SHOSHIKI"] != null)
            {
                this.form.TeishutuShosiki.Text = this.resultData.Rows[0]["HOUKOKU_SHOSHIKI"].ToString();
            }
            // 実績報告書修正.県区分
            if (this.resultData.Rows[0]["KEN_KBN_NAME"] != null)
            {
                this.form.KenKbn.Text = this.resultData.Rows[0]["KEN_KBN_NAME"].ToString();
            }
            // 実績報告書修正.保存名
            if (this.resultData.Rows[0]["HOZON_NAME"] != null)
            {
                this.form.HozonName.Text = this.resultData.Rows[0]["HOZON_NAME"].ToString();
            }
            // 実績報告書修正.提出先名

            if (this.resultData.Rows[0]["CHIIKI_NAME"] != null && this.resultData.Rows[0]["GOV_OR_MAY_NAME"] != null)
            {
                string chiikiName = this.resultData.Rows[0]["CHIIKI_NAME"].ToString();
                string govOrMayName = this.resultData.Rows[0]["GOV_OR_MAY_NAME"].ToString();

                if (!string.IsNullOrEmpty(chiikiName) && !string.IsNullOrEmpty(govOrMayName))
                {
                    switch (chiikiName.Substring(chiikiName.Length - 1, 1))
                    {
                        case "県":
                        case "都":
                        case "道":
                        case "府":
                            // 実績報告書修正.提出先名
                            this.form.TeishutuSakiName.Text = chiikiName + "知事　" + govOrMayName;
                            break;
                        case "市":
                            // 実績報告書修正.提出先名
                            this.form.TeishutuSakiName.Text = chiikiName + "市長　" + govOrMayName;
                            break;
                    }
                }
            }
            // 提出日
            this.form.TeishutsuYmd.Text = this.parentForm.sysDate.ToString("yyyyMMdd");
        }
        #endregion

        #region MultiRowの初期化処理
        /// <summary>
        /// MultiRowの初期化を行う
        /// </summary>
        internal void MultiRowInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (this.detailData == null || this.detailData.Count == 0)
                {
                    return;
                }

                this.form.grdIchiran.Rows.Clear();
                var table = this.resultData;
                int i = 0;

                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                string format = mSysInfo.SYS_SUURYOU_FORMAT.ToString();

                foreach (DataRow row in table.Rows)
                {
                    this.form.grdIchiran.Rows.Add();
                    // 産業廃棄物・特別管理産業廃棄物処理施設の種類
                    if (row["SHORI_SHISETSU_NAME"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["HoukokushoBunruiName"].Value = row["SHORI_SHISETSU_NAME"].ToString();
                    }
                    // 施設コード
                    if (row["SHORI_SHISETSU_CD"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["SisetsuCd"].Value = row["SHORI_SHISETSU_CD"].ToString();
                    }
                    // 種類名1
                    if (row["HAIKI_SHURUI_NAME1"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["Shurui1Name"].Value = row["HAIKI_SHURUI_NAME1"].ToString();
                    }
                    // 種類1
                    if (row["SBN_RYOU1"] != null && !string.IsNullOrWhiteSpace(row["SBN_RYOU1"].ToString()))
                    {
                        this.form.grdIchiran.Rows[i]["Shurui1"].Value = row["SBN_RYOU1"];
                    }
                    // 種類名2
                    if (row["HAIKI_SHURUI_NAME2"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["Shurui2Name"].Value = row["HAIKI_SHURUI_NAME2"].ToString();
                    }
                    // 種類2
                    if (row["SBN_RYOU2"] != null && !string.IsNullOrWhiteSpace(row["SBN_RYOU2"].ToString()))
                    {
                        this.form.grdIchiran.Rows[i]["Shurui2"].Value = row["SBN_RYOU2"];
                    }
                    // 種類名3
                    if (row["HAIKI_SHURUI_NAME3"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["Shurui3Name"].Value = row["HAIKI_SHURUI_NAME3"].ToString();
                    }
                    // 種類3
                    if (row["SBN_RYOU3"] != null && !string.IsNullOrWhiteSpace(row["SBN_RYOU3"].ToString()))
                    {
                        this.form.grdIchiran.Rows[i]["Shurui3"].Value = row["SBN_RYOU3"];
                    }
                    // 種類名4
                    if (row["HAIKI_SHURUI_NAME4"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["Shurui4Name"].Value = row["HAIKI_SHURUI_NAME4"].ToString();
                    }
                    // 種類4
                    if (row["SBN_RYOU4"] != null && !string.IsNullOrWhiteSpace(row["SBN_RYOU4"].ToString()))
                    {
                        this.form.grdIchiran.Rows[i]["Shurui4"].Value = row["SBN_RYOU4"];
                    }

                    (this.form.grdIchiran.Rows[i]["Shurui"] as GcCustomNumericTextBox2Cell).popupWindowSetting.Clear();
                    (this.form.grdIchiran.Rows[i]["SearchShurui"] as GcCustomPopupOpenButtonCell_Ex).popupWindowSetting.Clear();
                    JoinMethodDto dto = new JoinMethodDto();
                    dto.Join = JOIN_METHOD.WHERE;
                    dto.LeftTable = "M_CHIIKIBETSU_BUNRUI";
                    SearchConditionsDto conDto = new SearchConditionsDto();
                    conDto.And_Or = CONDITION_OPERATOR.AND;
                    conDto.LeftColumn = "CHIIKI_CD";
                    conDto.Condition = JUGGMENT_CONDITION.EQUALS;
                    conDto.Value = this.form.TeishutuSakiCd.Text;
                    conDto.ValueColumnType = DB_TYPE.VARCHAR;
                    dto.SearchCondition.Add(conDto);
                    (this.form.grdIchiran.Rows[i]["Shurui"] as GcCustomNumericTextBox2Cell).popupWindowSetting.Add(dto);
                    (this.form.grdIchiran.Rows[i]["SearchShurui"] as GcCustomPopupOpenButtonCell_Ex).popupWindowSetting.Add(dto);

                    // 種類名
                    if (row["SBN_AFTER_HAIKI_NAME"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["ShuruiName"].Value = row["SBN_AFTER_HAIKI_NAME"].ToString();
                    }
                    // 排出量
                    if (row["HST_RYOU"] != null && !string.IsNullOrWhiteSpace(row["HST_RYOU"].ToString()))
                    {
                        this.form.grdIchiran.Rows[i]["HaishutsuRyou"].Value = row["HST_RYOU"];
                    }
                    // 処理方法cd
                    if (row["SHOBUN_HOUHOU_CD"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["ShobunHouhouCd"].Value = row["SHOBUN_HOUHOU_CD"].ToString();
                    }

                    (this.form.grdIchiran.Rows[i]["ShobunHouhouCd"] as GcCustomNumericTextBox2Cell).popupWindowSetting.Clear();
                    (this.form.grdIchiran.Rows[i]["SearchShobunHouhou"] as GcCustomPopupOpenButtonCell_Ex).popupWindowSetting.Clear();
                    dto = new JoinMethodDto();
                    dto.Join = JOIN_METHOD.WHERE;
                    dto.LeftTable = "M_CHIIKIBETSU_SHOBUN";
                    conDto = new SearchConditionsDto();
                    conDto.And_Or = CONDITION_OPERATOR.AND;
                    conDto.LeftColumn = "CHIIKI_CD";
                    conDto.Condition = JUGGMENT_CONDITION.EQUALS;
                    conDto.Value = this.form.TeishutuSakiCd.Text;
                    conDto.ValueColumnType = DB_TYPE.VARCHAR;
                    dto.SearchCondition.Add(conDto);
                    dto.SearchCondition.Add(conDto);
                    (this.form.grdIchiran.Rows[i]["ShobunHouhouCd"] as GcCustomNumericTextBox2Cell).popupWindowSetting.Add(dto);
                    (this.form.grdIchiran.Rows[i]["SearchShobunHouhou"] as GcCustomPopupOpenButtonCell_Ex).popupWindowSetting.Add(dto);

                    // 処理方法Name
                    if (row["SHOBUN_HOUHOU_NAME"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["ShobunHouhouName"].Value = row["SHOBUN_HOUHOU_NAME"].ToString();
                    }
                    // 処分量
                    if (row["SBN_RYOU"] != null && !string.IsNullOrWhiteSpace(row["SBN_RYOU"].ToString()))
                    {
                        this.form.grdIchiran.Rows[i]["ShobunRyou"].Value = row["SBN_RYOU"];
                    }
                    // SYSTEM_ID
                    if (row["SYSTEM_ID"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["SystemId"].Value = row["SYSTEM_ID"].ToString();
                    }
                    // SEQ
                    if (row["SEQ"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["Seq"].Value = row["SEQ"].ToString();
                    }
                    // DETAIL_SYSTEM_ID
                    if (row["DETAIL_SYSTEM_ID"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["DetailSystemId"].Value = row["DETAIL_SYSTEM_ID"].ToString();
                    }
                    // 処分量1
                    if (row["HAIKI_SHURUI_CD1"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["ShobunRyou1"].Value = row["HAIKI_SHURUI_CD1"].ToString();
                    }
                    // 処分量2
                    if (row["HAIKI_SHURUI_CD2"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["ShobunRyou2"].Value = row["HAIKI_SHURUI_CD2"].ToString();
                    }
                    // 処分量3
                    if (row["HAIKI_SHURUI_CD3"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["ShobunRyou3"].Value = row["HAIKI_SHURUI_CD3"].ToString();
                    }
                    // 処分量4
                    if (row["HAIKI_SHURUI_CD4"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["ShobunRyou4"].Value = row["HAIKI_SHURUI_CD4"].ToString();
                    }
                    // HST_JOU_CHIIKI_CD
                    if (row["HST_JOU_CHIIKI_CD"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["HST_JOU_CHIIKI_CD"].Value = row["HST_JOU_CHIIKI_CD"].ToString();
                    }
                    i++;
                }

                //産業廃棄物・特別管理産業廃棄物の種類検索ボタン初期化
                this.GcCustomPopupOpenButtonCellInit("SearchShurui",
                                                     this.resultData.Rows[0]["TEISHUTSU_CHIIKI_CD"].ToString(),
                                                     "M_CHIIKIBETSU_BUNRUI");

                // 処分方法検索ボタン初期化
                this.GcCustomPopupOpenButtonCellInit("SearchShobunHouhou",
                                                     this.resultData.Rows[0]["TEISHUTSU_CHIIKI_CD"].ToString(),
                                                     "M_CHIIKIBETSU_SHOBUN");


                this.form.grdIchiran.Refresh();

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// GcCustomPopupOpenButtonCellの初期化を行う
        /// </summary>
        /// <param name="buttonNmae">ボタン名</param>
        /// <param name="value">条件値</param>
        /// <param name="tabelName">検索テーブル名</param>
        private void GcCustomPopupOpenButtonCellInit(string buttonNmae, string value, string tabelName)
        {
            if (this.multiRowDataTable.Rows.Count == 0)
            {
                return;
            }
            r_framework.CustomControl.GcCustomPopupOpenButtonCell cell = new r_framework.CustomControl.GcCustomPopupOpenButtonCell();
            for (int i = 0; i < this.multiRowDataTable.Rows.Count; i++)
            {
                cell = new r_framework.CustomControl.GcCustomPopupOpenButtonCell();
                cell = this.form.grdIchiran.Rows[i][buttonNmae] as r_framework.CustomControl.GcCustomPopupOpenButtonCell;
                cell.popupWindowSetting.Clear();
                r_framework.Dto.JoinMethodDto dto = new r_framework.Dto.JoinMethodDto();
                r_framework.Dto.SearchConditionsDto item = new r_framework.Dto.SearchConditionsDto();
                item.And_Or = CONDITION_OPERATOR.AND;
                item.Condition = JUGGMENT_CONDITION.EQUALS;
                item.LeftColumn = "CHIIKI_CD";
                item.Value = value;
                item.ValueColumnType = DB_TYPE.VARCHAR;
                dto.SearchCondition.Add(item);
                dto.Join = JOIN_METHOD.WHERE;
                dto.LeftTable = tabelName;
                cell.popupWindowSetting.Add(dto);
            }
        }

        #region MultiRow初期化用DataTableの初期化を行う
        /// <summary>
        /// MultiRow初期化用DataTableの初期化を行う
        /// </summary>
        private void MultiRowDataTableInit()
        {
            this.multiRowDataTable = new DataTable();
            this.multiRowDataTable.Columns.Add("HoukokushoBunruiName");
            this.multiRowDataTable.Columns.Add("SisetsuCd");
            this.multiRowDataTable.Columns.Add("Shurui1Name");
            this.multiRowDataTable.Columns.Add("Shurui1");
            this.multiRowDataTable.Columns.Add("Shurui2Name");
            this.multiRowDataTable.Columns.Add("Shurui2");
            this.multiRowDataTable.Columns.Add("Shurui3Name");
            this.multiRowDataTable.Columns.Add("Shurui3");
            this.multiRowDataTable.Columns.Add("Shurui4Name");
            this.multiRowDataTable.Columns.Add("Shurui4");
            this.multiRowDataTable.Columns.Add("ShuruiName");
            this.multiRowDataTable.Columns.Add("HaishutsuRyou");
            this.multiRowDataTable.Columns.Add("ShobunHouhouCd");
            this.multiRowDataTable.Columns.Add("ShobunHouhouName");
            this.multiRowDataTable.Columns.Add("ShobunRyou");
            this.multiRowDataTable.Columns.Add("SystemId");
            this.multiRowDataTable.Columns.Add("Seq");
            this.multiRowDataTable.Columns.Add("DetailSystemId");
            this.multiRowDataTable.Columns.Add("ShobunRyou1");
            this.multiRowDataTable.Columns.Add("ShobunRyou2");
            this.multiRowDataTable.Columns.Add("ShobunRyou3");
            this.multiRowDataTable.Columns.Add("ShobunRyou4");
            this.multiRowDataTable.Columns.Add("HST_JOU_CHIIKI_CD");
        }
        #endregion

        /// <summary>
        /// コントロール初期化処理
        /// </summary>
        private void ControlInit()
        {
            LogUtility.DebugMethodStart();

            this.form.HokokuNendo.Text = string.Empty;
            this.form.TeishutuSaki.Text = string.Empty;
            this.form.JishaGhoushuKbn.Text = string.Empty;
            this.form.HozonYmd.Text = string.Empty;
            this.form.TokuKbn.Text = string.Empty;
            this.form.TeishutuShosiki.Text = string.Empty;
            this.form.KenKbn.Text = string.Empty;
            this.form.HozonName.Text = string.Empty;
            this.form.TeishutuSakiName.Text = string.Empty;
            this.form.TeishutsuYmd.Text = string.Empty;

            LogUtility.DebugMethodEnd();

        }

        #region ボタンの初期化
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                // ボタンの設定情報をファイルから読み込む
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

                parentForm.bt_func3.Enabled = (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG);

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();

                LogUtility.DebugMethodEnd();

                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;
                //Functionボタンのイベント生成
                parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);            // [F3]修正イベント
                parentForm.bt_func5.Click += new EventHandler(bt_func5_Click);                  // [F5]印刷イベント
                parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);                  // [F6]CSV出力イベント
                parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);                  // [F7]一覧イベント
                parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);                  // [F9]登録イベント
                parentForm.bt_func11.Click += new EventHandler(bt_func11_Click);                // [F11]行削除イベント
                parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);                // [F12]閉じるイベント
                // 画面表示イベント
                parentForm.Shown += new EventHandler(UIForm_Shown);
                this.form.grdIchiran.CellValidated += new EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.grdIchiran_CellValidated);
                this.form.grdIchiran.CellEnter += new EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.grdIchiran_CellEnter);
                this.form.grdIchiran.Validating += new System.ComponentModel.CancelEventHandler(this.form.grdIchiran_Validating);
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [F3]修正イベント
        /// <summary>
        /// [F3]修正イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void UpdateMode(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // 実績報告書修正データを取得する
                this.GetJissekiHokokuData(this.form.systemid);
                // ヘッダーデータ設定する
                this.SetHeaderData();
                // MultiRow初期化用DataTable
                this.MultiRowDataTableInit();
                // MultiRowにデータを追加する
                this.MultiRowInit();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                // 修正バッタン使用不可
                parentForm.bt_func3.Enabled = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateMode", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region [F5]印刷イベント
        /// <summary>
        /// [F5]印刷イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.CreateEntry();
                M_SYS_INFO[] sysInfo = this.logic.sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.logic.sysInfoEntity = sysInfo[0];
                }
                this.logic.PrintView();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region [F6]CSV出力ボタンイベント
        /// <summary>
        /// [F6]CSV出力ボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (this.form.grdIchiran.Rows.Count == 0)
                {
                    DialogResult result = msgLogic.MessageBoxShow("E044");
                    return;
                }
                //if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                //{
                    this.CSVOutput();
                //}
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        internal void CSVOutput()
        {
            LogUtility.DebugMethodStart();
            // mainデータ
            //this.SearchResult = this.EntryDao.GetDataForEntity(this.headerData);

            //int result = this.setDetailEntity();

            //if (result == -1)
            //{
            //    return;
            //}
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("廃棄物処理施設種類");
            dc1.DataType = typeof(string);
            DataColumn dc2 = new DataColumn("施設コード");
            dc2.DataType = typeof(string);
            DataColumn dc3 = new DataColumn("種類名1");
            dc3.DataType = typeof(string);
            DataColumn dc4 = new DataColumn("処分量1");
            dc4.DataType = typeof(string);
            DataColumn dc5 = new DataColumn("種類名2");
            dc5.DataType = typeof(string);
            DataColumn dc6 = new DataColumn("処分量2");
            dc6.DataType = typeof(string);
            DataColumn dc7 = new DataColumn("種類名3");
            dc7.DataType = typeof(string);
            DataColumn dc8 = new DataColumn("処分量3");
            dc8.DataType = typeof(string);
            DataColumn dc9 = new DataColumn("種類名4");
            dc9.DataType = typeof(string);
            DataColumn dc10 = new DataColumn("処分量4");
            dc10.DataType = typeof(string);
            DataColumn dc11 = new DataColumn("処分後廃棄物種類");
            dc11.DataType = typeof(string);
            DataColumn dc12 = new DataColumn("排出量");
            dc12.DataType = typeof(string);
            DataColumn dc13= new DataColumn("処分方法CD");
            dc13.DataType = typeof(string);
            DataColumn dc14 = new DataColumn("処分方法名");
            dc14.DataType = typeof(string);
            DataColumn dc15 = new DataColumn("処分量");
            dc15.DataType = typeof(string);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
            dt.Columns.Add(dc9);
            dt.Columns.Add(dc10);
            dt.Columns.Add(dc11);
            dt.Columns.Add(dc12);
            dt.Columns.Add(dc13);
            dt.Columns.Add(dc14);
            dt.Columns.Add(dc15);
            //var csv = from temp in this.tjHoukokuSyoriEntryList
            //          group temp by
            //          new
            //          {
            //              temp.SHORI_SHISETSU_NAME,
            //              temp.HAIKI_SHURUI_NAME1,
            //              temp.HAIKI_SHURUI_NAME2,
            //              temp.HAIKI_SHURUI_NAME3,
            //              temp.HAIKI_SHURUI_NAME4,
            //              temp.SBN_AFTER_HAIKI_NAME,
            //              temp.SHOBUN_HOUHOU_NAME,
            //              temp.UNIT_NAME
            //          } into rst
            //          select
            //          new
            //          {
            //              SHORI_SHISETSU_NAME = rst.Key.SHORI_SHISETSU_NAME,
            //              HAIKI_SHURUI_NAME1 = rst.Key.HAIKI_SHURUI_NAME1,
            //              HAIKI_SHURUI_NAME2 = rst.Key.HAIKI_SHURUI_NAME2,
            //              HAIKI_SHURUI_NAME3 = rst.Key.HAIKI_SHURUI_NAME3,
            //              HAIKI_SHURUI_NAME4 = rst.Key.HAIKI_SHURUI_NAME4,
            //              SBN_AFTER_HAIKI_NAME = rst.Key.SBN_AFTER_HAIKI_NAME,
            //              SHOBUN_HOUHOU_NAME = rst.Key.SHOBUN_HOUHOU_NAME,
            //              UNIT_NAME = rst.Key.UNIT_NAME,
            //              DATA = rst.ToArray()
            //          };
            //foreach (var c in csv)
            //{
            //    double? sbn1 = null;
            //    double? sbn2 = null;
            //    double? sbn3 = null;
            //    double? sbn4 = null;
            //    double? hst = null;
            //    double? sbn = null;
            //    foreach (T_JISSEKI_HOUKOKU_SHORI_DETAIL groupResult in c.DATA)
            //    {
            //        this.Sum(ref sbn1, this.ToNDouble(groupResult.SBN_RYOU1));
            //        this.Sum(ref sbn2, this.ToNDouble(groupResult.SBN_RYOU2));
            //        this.Sum(ref sbn3, this.ToNDouble(groupResult.SBN_RYOU3));
            //        this.Sum(ref sbn4, this.ToNDouble(groupResult.SBN_RYOU4));
            //        this.Sum(ref hst, this.ToNDouble(groupResult.HST_RYOU));
            //        this.Sum(ref sbn, this.ToNDouble(groupResult.SBN_RYOU));
            //    }
            //    DataRow dr = dt.NewRow();
            //    dr["処理施設名"] = c.SHORI_SHISETSU_NAME;
            //    dr["種類名1"] = c.HAIKI_SHURUI_NAME1;
            //    if (!string.IsNullOrEmpty(c.HAIKI_SHURUI_NAME1))
            //    {
            //        dr["単位1"] = c.UNIT_NAME;
            //        dr["処分量1"] = Convert.ToString(sbn1) ?? "";
            //    }
            //    dr["種類名2"] = c.HAIKI_SHURUI_NAME2;
            //    if (!string.IsNullOrEmpty(c.HAIKI_SHURUI_NAME2))
            //    {
            //        dr["単位2"] = c.UNIT_NAME;
            //        dr["処分量2"] = Convert.ToString(sbn2) ?? "";
            //    }
            //    dr["種類名3"] = c.HAIKI_SHURUI_NAME3;
            //    if (!string.IsNullOrEmpty(c.HAIKI_SHURUI_NAME3))
            //    {
            //        dr["単位3"] = c.UNIT_NAME;
            //        dr["処分量3"] = Convert.ToString(sbn3) ?? "";
            //    }
            //    dr["種類名4"] = c.HAIKI_SHURUI_NAME4;
            //    if (!string.IsNullOrEmpty(c.HAIKI_SHURUI_NAME4))
            //    {
            //        dr["単位4"] = c.UNIT_NAME;
            //        dr["処分量4"] = Convert.ToString(sbn4) ?? "";
            //    }
            //    dr["処分後廃棄物名"] = c.SBN_AFTER_HAIKI_NAME;
            //    if (!string.IsNullOrEmpty(c.SBN_AFTER_HAIKI_NAME))
            //    {
            //        dr["排出単位"] = c.UNIT_NAME;
            //        dr["排出量"] = Convert.ToString(hst) ?? "";
            //        dr["処分方法名"] = c.SHOBUN_HOUHOU_NAME;
            //        dr["処分単位"] = c.UNIT_NAME;
            //        dr["処分量"] = Convert.ToString(sbn) ?? "";
            //    }
            //    dt.Rows.Add(dr);
            //}
            foreach (Row row in this.form.grdIchiran.Rows)
            {
                DataRow newRow = dt.NewRow();
                newRow["廃棄物処理施設種類"] = row["HoukokushoBunruiName"].Value;
                newRow["施設コード"] = row["SisetsuCd"].Value;
                newRow["種類名1"] = row["Shurui1Name"].Value;
                if (row["Shurui1"].Value != null)
                {
                    newRow["処分量1"] = this.ConvertSuuryo(row["Shurui1"].Value.ToString());
                }
                newRow["種類名2"] = row["Shurui2Name"].Value;
                if (row["Shurui2"].Value != null)
                {
                    newRow["処分量2"] = this.ConvertSuuryo(row["Shurui2"].Value.ToString());
                }
                newRow["種類名3"] = row["Shurui3Name"].Value;
                if (row["Shurui3"].Value != null)
                {
                    newRow["処分量3"] = this.ConvertSuuryo(row["Shurui3"].Value.ToString());
                }
                newRow["種類名4"] = row["Shurui4Name"].Value;
                if (row["Shurui4"].Value != null)
                {
                    newRow["処分量4"] = this.ConvertSuuryo(row["Shurui4"].Value.ToString());
                }
                newRow["処分後廃棄物種類"] = row["ShuruiName"].Value;
                if (row["HaishutsuRyou"].Value != null)
                {
                    newRow["排出量"] = this.ConvertSuuryo(row["HaishutsuRyou"].Value.ToString());
                }
                newRow["処分方法CD"] = row["ShobunHouhouCd"].Value;
                newRow["処分方法名"] = row["ShobunHouhouName"].Value;
                if (row["ShobunRyou"].Value != null)
                {
                    newRow["処分量"] = this.ConvertSuuryo(row["ShobunRyou"].Value.ToString());
                }
                dt.Rows.Add(newRow);
            }
            this.form.dgv_csv.DataSource = dt;
            CSVExport CSVExp = new CSVExport();
            CSVExp.ConvertCustomDataGridViewToCsv(this.form.dgv_csv, true, true, "処理施設実績報告書", this.form);
            LogUtility.DebugMethodEnd();
        }


        private int setDetailEntity()
        {
            LogUtility.DebugMethodStart();
            var messageShowLogic = new MessageBoxShowLogic();
            this.tjHoukokuSyoriEntryList = new List<T_JISSEKI_HOUKOKU_SHORI_DETAIL>();
            bool bunruiErrFlag = false;

            long DETAIL_SYSTEM_ID = 0;

            if (this.SearchResult.Rows.Count == 0)
            {
                DialogResult result = messageShowLogic.MessageBoxShow("C001");
                return -1;
            }

            List<T_JISSEKI_HOUKOKU_SHORI_DETAIL> dataList = new List<T_JISSEKI_HOUKOKU_SHORI_DETAIL>();
            List<DTOClass> dtoList = new List<DTOClass>();
            List<string> haikiSyuruiList = new List<string>();
            List<string> haikiSyuruiNameList = new List<string>();
            if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
            {
                foreach (DataRow dt in this.SearchResult.Rows)
                {
                    if (string.IsNullOrEmpty(dt["SHORI_SHISETSU_NAME"].ToString())
                        || string.IsNullOrEmpty(dt["SHORI_SHISETSU_CD"].ToString())
                        || string.IsNullOrEmpty(dt["HAIKI_SHURUI_CD"].ToString())
                        || string.IsNullOrEmpty(dt["HST_JOU_CHIIKI_CD"].ToString())
                        || string.IsNullOrEmpty(dt["HST_KEN_KBN"].ToString())
                        || string.IsNullOrEmpty(dt["HST_JOU_TODOUFUKEN_CD"].ToString())
                        || (("1".Equals(dt["NEXT_KBN"].ToString()) && (string.IsNullOrEmpty(dt["SBN_AFTER_HAIKI_NAME"].ToString())
                                                       || string.IsNullOrEmpty(dt["SHOBUN_HOUHOU_CD"].ToString()))))
                        )
                    {
                        DialogResult result = messageShowLogic.MessageBoxShow("C077", "マニフェスト伝票", "マニチェック表");
                        if (result == DialogResult.Yes)
                        {
                            FormManager.OpenForm("G124");
                        }
                        return -1;
                    }

                    if (string.IsNullOrEmpty(dt["HOUKOKUSHO_BUNRUI_CD"].ToString()))
                    {
                        if (!bunruiErrFlag)
                        {
                            DialogResult result = messageShowLogic.MessageBoxShow("C076", "マスター保守＞地域別分類", "マニチェック表", "分類コード");
                            if (result == DialogResult.No)
                            {
                                return -1;
                            }

                            bunruiErrFlag = true;
                        }
                    }
                    else
                    {
                        string chiikiCd = string.Empty;
                        string houkokuBunruiCd = string.Empty;
                        string gyoushaCd = string.Empty;
                        M_CHIIKIBETSU_BUNRUI bunruiEntity = new M_CHIIKIBETSU_BUNRUI();
                        chiikiCd = this.headerData.TEISHUTSU_CHIIKI_CD;
                        houkokuBunruiCd = dt["HOUKOKUSHO_BUNRUI_CD"].ToString();
                        bunruiEntity = this.BunruiDAO.GetBunRui(chiikiCd, houkokuBunruiCd);
                        if (bunruiEntity == null)
                        {
                            if (!bunruiErrFlag)
                            {
                                DialogResult result = messageShowLogic.MessageBoxShow("C076", "マスター保守＞地域別分類", "マニチェック表", "分類コード");
                                if (result == DialogResult.No)
                                {
                                    return -1;
                                }
                                bunruiErrFlag = true;
                            }
                        }
                    }

                    DTOClass dto = new DTOClass();

                    if (!string.IsNullOrEmpty(dt["SYSTEM_ID"].ToString()))
                    {
                        dto.SYSTEM_ID = Convert.ToInt64(dt["SYSTEM_ID"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt["SEQ"].ToString()))
                    {
                        dto.SEQ = Convert.ToInt32(dt["SEQ"].ToString());
                    }
                    dto.MANIFEST_ID = dt["MANIFEST_ID"].ToString();
                    dto.KANRI_ID = dt["KANRI_ID"].ToString();
                    if (!string.IsNullOrEmpty(dt["DEN_SEQ"].ToString()))
                    {
                        dto.SEQ = Convert.ToInt32(dt["DEN_SEQ"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt["HAIKI_KBN_CD"].ToString()))
                    {
                        dto.HAIKI_KBN_CD = Convert.ToInt16(dt["HAIKI_KBN_CD"].ToString());
                    }
                    dto.SHORI_SHISETSU_NAME = dt["SHORI_SHISETSU_NAME"].ToString();
                    if (!string.IsNullOrEmpty(dt["NEXT_KBN"].ToString()))
                    {
                        dto.NEXT_KBN = Convert.ToInt16(dt["NEXT_KBN"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dt["NEXT_SYSTEM_ID"].ToString()))
                    {
                        dto.NEXT_SYSTEM_ID = Convert.ToInt64(dt["NEXT_SYSTEM_ID"].ToString());
                    }

                    dto.HOUKOKUSHO_BUNRUI_CD = dt["HOUKOKUSHO_BUNRUI_CD"].ToString();
                    dto.SBN_AFTER_HAIKI_NAME = dt["SBN_AFTER_HAIKI_NAME"].ToString();
                    dto.SHORI_SHISETSU_CD = dt["SHORI_SHISETSU_CD"].ToString();
                    dto.HAIKI_SHURUI_CD = dt["HAIKI_SHURUI_CD"].ToString();
                    dto.HAIKI_SHURUI_NAME = dt["HAIKI_SHURUI_NAME"].ToString();

                    int count = 1;
                    string nextSystemID = dt["NEXT_SYSTEM_ID"].ToString();
                    if (!string.IsNullOrEmpty(nextSystemID))
                    {
                        count = this.EntryDao.GetDetailCount(nextSystemID);
                    }

                    if (!string.IsNullOrEmpty(dt["KANSAN_SUU"].ToString()))
                    {
                        dto.KANSAN_SUU = Convert.ToDecimal(dt["KANSAN_SUU"].ToString()) / count;
                    }

                    dto.UNIT_NAME = dt["UNIT_NAME"].ToString();

                    if (dto.NEXT_KBN.Value == 1)
                    {
                        dto.SHOBUN_HOUHOU_CD = dt["SHOBUN_HOUHOU_CD"].ToString();
                        dto.SHOBUN_HOUHOU_NAME = dt["SHOBUN_HOUHOU_NAME"].ToString();

                        if (!string.IsNullOrEmpty(dt["GENNYOU_SUU"].ToString()))
                        {
                            dto.GENNYOU_SUU = Convert.ToDecimal(dt["GENNYOU_SUU"].ToString());
                        }
                    }
                    dto.HST_JOU_CHIIKI_CD = dt["HST_JOU_CHIIKI_CD"].ToString();

                    if (!string.IsNullOrEmpty(dt["HST_KEN_KBN"].ToString()))
                    {
                        dto.HST_KEN_KBN = Convert.ToInt16(dt["HST_KEN_KBN"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dt["HST_JOU_TODOUFUKEN_CD"].ToString()))
                    {
                        dto.HST_JOU_TODOUFUKEN_CD = Convert.ToInt16(dt["HST_JOU_TODOUFUKEN_CD"].ToString());
                    }

                    bool new_haiki = true;
                    for (int i = 0; i < haikiSyuruiList.Count; i++)
                    {
                        string hiki = haikiSyuruiList[i];
                        if (dto.HAIKI_SHURUI_CD.Equals(hiki))
                        {
                            new_haiki = false;
                        }
                    }

                    if (new_haiki)
                    {
                        haikiSyuruiList.Add(dto.HAIKI_SHURUI_CD);
                        haikiSyuruiNameList.Add(dto.HAIKI_SHURUI_NAME);
                    }
                    dtoList.Add(dto);
                }
            }

            foreach (DTOClass data in dtoList)
            {
                T_JISSEKI_HOUKOKU_SHORI_DETAIL tjHoukokuSyoriEntry = new T_JISSEKI_HOUKOKU_SHORI_DETAIL();

                bool new_row = true;
                long row_detail_id = 0;
                int index = 0;
                int page_no = 0;

                for (int j = 0; j < haikiSyuruiList.Count; j++)
                {
                    if (data.HAIKI_SHURUI_CD.Equals(haikiSyuruiList[j]))
                    {
                        index = j % 4 + 1;
                        page_no = j / 4 + 1;
                    }
                }
                for (int i = 0; i < this.tjHoukokuSyoriEntryList.Count; i++)
                {
                    T_JISSEKI_HOUKOKU_SHORI_DETAIL syoriData = this.tjHoukokuSyoriEntryList[i];
                    if (syoriData.HST_JOU_CHIIKI_CD.Equals(data.HST_JOU_CHIIKI_CD)
                            && syoriData.SHORI_SHISETSU_NAME.Equals(data.SHORI_SHISETSU_NAME)
                            && syoriData.SHORI_SHISETSU_CD.Equals(data.SHORI_SHISETSU_CD)
                            && syoriData.SBN_AFTER_HAIKI_NAME == data.SBN_AFTER_HAIKI_NAME
                            && syoriData.SHOBUN_HOUHOU_CD == data.SHOBUN_HOUHOU_CD)
                    {
                        switch (index)
                        {
                            case 1:
                                if (syoriData.HAIKI_SHURUI_CD1.Equals(data.HAIKI_SHURUI_CD))
                                {
                                    if (!string.IsNullOrEmpty(data.HAIKI_SHURUI_CD))
                                    {
                                        if (!data.KANSAN_SUU.IsNull)
                                        {
                                            if (this.tjHoukokuSyoriEntryList[i].SBN_RYOU1.IsNull)
                                            {
                                                this.tjHoukokuSyoriEntryList[i].SBN_RYOU1 = 0;
                                            }

                                            this.tjHoukokuSyoriEntryList[i].SBN_RYOU1 = this.tjHoukokuSyoriEntryList[i].SBN_RYOU1.Value + data.KANSAN_SUU.Value;
                                        }
                                    }

                                    new_row = false;
                                    row_detail_id = syoriData.DETAIL_SYSTEM_ID.Value;
                                }
                                break;
                            case 2:
                                if (syoriData.HAIKI_SHURUI_CD2.Equals(data.HAIKI_SHURUI_CD))
                                {
                                    if (!string.IsNullOrEmpty(data.HAIKI_SHURUI_CD))
                                    {
                                        if (!data.KANSAN_SUU.IsNull)
                                        {
                                            if (this.tjHoukokuSyoriEntryList[i].SBN_RYOU2.IsNull)
                                            {
                                                this.tjHoukokuSyoriEntryList[i].SBN_RYOU2 = 0;
                                            }

                                            this.tjHoukokuSyoriEntryList[i].SBN_RYOU2 = this.tjHoukokuSyoriEntryList[i].SBN_RYOU2.Value + data.KANSAN_SUU.Value;
                                        }
                                    }

                                    new_row = false;
                                    row_detail_id = syoriData.DETAIL_SYSTEM_ID.Value;
                                }
                                break;
                            case 3:
                                if (syoriData.HAIKI_SHURUI_CD3.Equals(data.HAIKI_SHURUI_CD))
                                {
                                    if (!string.IsNullOrEmpty(data.HAIKI_SHURUI_CD))
                                    {
                                        if (!data.KANSAN_SUU.IsNull)
                                        {
                                            if (this.tjHoukokuSyoriEntryList[i].SBN_RYOU3.IsNull)
                                            {
                                                this.tjHoukokuSyoriEntryList[i].SBN_RYOU3 = 0;
                                            }
                                            this.tjHoukokuSyoriEntryList[i].SBN_RYOU3 = this.tjHoukokuSyoriEntryList[i].SBN_RYOU3.Value + data.KANSAN_SUU.Value;
                                        }
                                    }

                                    new_row = false;
                                    row_detail_id = syoriData.DETAIL_SYSTEM_ID.Value;
                                }
                                break;
                            case 4:
                                if (syoriData.HAIKI_SHURUI_CD4.Equals(data.HAIKI_SHURUI_CD))
                                {
                                    if (!string.IsNullOrEmpty(data.HAIKI_SHURUI_CD))
                                    {
                                        if (!data.KANSAN_SUU.IsNull)
                                        {
                                            if (this.tjHoukokuSyoriEntryList[i].SBN_RYOU4.IsNull)
                                            {
                                                this.tjHoukokuSyoriEntryList[i].SBN_RYOU4 = 0;
                                            }
                                            this.tjHoukokuSyoriEntryList[i].SBN_RYOU4 = this.tjHoukokuSyoriEntryList[i].SBN_RYOU4.Value + data.KANSAN_SUU.Value;
                                        }
                                    }

                                    new_row = false;
                                    row_detail_id = syoriData.DETAIL_SYSTEM_ID.Value;
                                }
                                break;
                        }
                    }
                }

                if (new_row)
                {
                    SqlInt64 ID = this.createSystemIdForJissekiHokoku();
                    DETAIL_SYSTEM_ID = ID.Value;
                    tjHoukokuSyoriEntry.SYSTEM_ID = this.headerData.SYSTEM_ID;

                    // 枝番
                    tjHoukokuSyoriEntry.SEQ = this.headerData.SEQ;

                    // 明細システムID
                    tjHoukokuSyoriEntry.DETAIL_SYSTEM_ID = DETAIL_SYSTEM_ID;

                    // 帳票ID
                    tjHoukokuSyoriEntry.REPORT_ID = 2;

                    // 報告書式
                    tjHoukokuSyoriEntry.HOUKOKU_SHOSHIKI_KBN = this.headerData.HOUKOKU_SHOSHIKI;

                    // 保存名
                    tjHoukokuSyoriEntry.HOZON_NAME = this.headerData.HOZON_NAME;

                    // 報告年度
                    //tjHoukokuSbnEntry.HOUKOKU_YEAR = this.tjHoukokuEntry.HOUKOKU_YEAR;
                    // 和暦でDataTimeを文字列に変換する
                    System.Globalization.CultureInfo ci =
                    new System.Globalization.CultureInfo("ja-JP", false);
                    ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();

                    tjHoukokuSyoriEntry.HOUKOKU_YEAR = Convert.ToDateTime(this.headerData.DATE_BEGIN.Value).ToString("gy年", ci);

                    // 提出先地域CD
                    tjHoukokuSyoriEntry.TEISHUTSUSAKI_CHIIKI_CD = this.headerData.TEISHUTSU_CHIIKI_CD;

                    // 処理施設CD
                    tjHoukokuSyoriEntry.SHORI_SHISETSU_CD = data.SHORI_SHISETSU_CD;

                    // 処理施設名
                    tjHoukokuSyoriEntry.SHORI_SHISETSU_NAME = data.SHORI_SHISETSU_NAME;

                    // 事業場区分
                    tjHoukokuSyoriEntry.JIGYOUJOU_KBN = 1;

                    // 県区分
                    tjHoukokuSyoriEntry.KEN_KBN = 0;

                    tjHoukokuSyoriEntry.SBN_AFTER_HAIKI_NAME = data.SBN_AFTER_HAIKI_NAME;

                    tjHoukokuSyoriEntry.PAGE_NO = page_no;

                    int haiki_no = 1;
                    int end_no = 0;
                    if (haikiSyuruiList.Count >= 4 * page_no)
                    {
                        end_no = 4 * page_no;
                    }
                    else
                    {
                        end_no = haikiSyuruiList.Count;
                    }

                    for (int k = (page_no - 1) * 4; k < end_no; k++)
                    {
                        switch (haiki_no)
                        {
                            case 1:
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_CD1 = haikiSyuruiList[k];
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_NAME1 = haikiSyuruiNameList[k];
                                //tjHoukokuSyoriEntry.SBN_RYOU1 = 0;
                                break;
                            case 2:
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_CD2 = haikiSyuruiList[k];
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_NAME2 = haikiSyuruiNameList[k];
                                //tjHoukokuSyoriEntry.SBN_RYOU2 = 0;
                                break;
                            case 3:
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_CD3 = haikiSyuruiList[k];
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_NAME3 = haikiSyuruiNameList[k];
                                //tjHoukokuSyoriEntry.SBN_RYOU3 = 0;
                                break;
                            case 4:
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_CD4 = haikiSyuruiList[k];
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_NAME4 = haikiSyuruiNameList[k];
                                //tjHoukokuSyoriEntry.SBN_RYOU4 = 0;
                                break;
                        }
                        haiki_no++;
                    }

                    //if (data.KANSAN_SUU.IsNull)
                    // {
                    //    data.KANSAN_SUU = 0;
                    // }

                    switch (index)
                    {
                        case 1:
                            if (!data.KANSAN_SUU.IsNull)
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU1 = data.KANSAN_SUU.Value;
                            }
                            break;
                        case 2:
                            if (!data.KANSAN_SUU.IsNull)
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU2 = data.KANSAN_SUU.Value;
                            }
                            break;
                        case 3:
                            if (!data.KANSAN_SUU.IsNull)
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU3 = data.KANSAN_SUU.Value;
                            }
                            break;
                        case 4:
                            if (!data.KANSAN_SUU.IsNull)
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU4 = data.KANSAN_SUU.Value;
                            }
                            break;
                    }

                    tjHoukokuSyoriEntry.UNIT_NAME = data.UNIT_NAME;
                    if (data.NEXT_KBN.Value == 1)
                    {
                        //if (data.GENNYOU_SUU.IsNull)
                        //{
                        //data.GENNYOU_SUU = 0;
                        //}
                        if (!data.GENNYOU_SUU.IsNull)
                        {
                            tjHoukokuSyoriEntry.HST_RYOU = data.GENNYOU_SUU;
                        }
                        tjHoukokuSyoriEntry.SHOBUN_HOUHOU_CD = data.SHOBUN_HOUHOU_CD;
                        tjHoukokuSyoriEntry.SHOBUN_HOUHOU_NAME = data.SHOBUN_HOUHOU_NAME;
                        tjHoukokuSyoriEntry.SBN_RYOU = data.GENNYOU_SUU;

                    }
                    tjHoukokuSyoriEntry.HST_KEN_KBN = data.HST_KEN_KBN;

                    tjHoukokuSyoriEntry.HST_JOU_TODOUFUKEN_CD = data.HST_JOU_TODOUFUKEN_CD;

                    tjHoukokuSyoriEntry.HST_JOU_CHIIKI_CD = data.HST_JOU_CHIIKI_CD;

                    tjHoukokuSyoriEntry.SYUKEI_KBN = 0;

                    this.tjHoukokuSyoriEntryList.Add(tjHoukokuSyoriEntry);
                    row_detail_id = DETAIL_SYSTEM_ID;
                }
            }

            LogUtility.DebugMethodEnd();
            return 0;
        }

        ///// <summary>
        ///// double?型に転換する
        ///// </summary>
        ///// <param name="o">o</param>
        //internal double? ToNDouble(object o)
        //{
        //    double? ret = null;
        //    double parse = 0;
        //    if (double.TryParse(Convert.ToString(o), out parse))
        //    {
        //        ret = parse;
        //    }
        //    return ret;
        //}

        /// <summary>
        /// 計算
        /// </summary>
        /// <param name="o">o</param>
        internal void Sum(ref decimal? ret, decimal? add)
        {
            if (add != null)
            {
                if (ret == null)
                {
                    ret = 0;
                }
                ret += add;
            }
        }
        #endregion

        #region [F7]一覧イベント
        /// <summary>
        /// [F7]一覧イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                r_framework.FormManager.FormManager.OpenForm("G135");
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region [F9]登録イベント
        /// <summary>
        /// [F9]登録イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            //独自チェックの記述例を書く
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                // トランザクション開始
                using (var tran = new Transaction())
                {
                    if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    {
                        this.SetUpdateData();
                        foreach (T_JISSEKI_HOUKOKU_SHORI_DETAIL row in this.updateDetailData)
                        {
                            T_JISSEKI_HOUKOKU_SHORI_DETAIL dto = new T_JISSEKI_HOUKOKU_SHORI_DETAIL();
                            dto = row;
                            this.detailDao.Insert(dto);
                        }

                        // 明細マニフェストデータを更新する
                        T_JISSEKI_HOUKOKU_MANIFEST_DETAIL data = new T_JISSEKI_HOUKOKU_MANIFEST_DETAIL();
                        foreach (T_JISSEKI_HOUKOKU_MANIFEST_DETAIL row in this.updateManiDetailData)
                        {
                            data = new T_JISSEKI_HOUKOKU_MANIFEST_DETAIL();
                            data = row;
                            this.maniDetailDao.Insert(data);
                        }

                        // 元EntityにDELETE_FLG更新
                        this.dao.Update(this.headerData);
                        // 現Entityに登録
                        this.updateheaderData.DELETE_FLG = false;
                        this.dao.Insert(this.updateheaderData);

                        // トランザクション終了
                        tran.Commit();
                        msgLogic.MessageBoxShow("I001", "修正");
                    }
                    else
                    {
                        var result = msgLogic.MessageBoxShow("C026");
                        if (result == DialogResult.Yes)
                        {
                            this.headerData.DELETE_FLG = true;
                            new DataBinderLogic<T_JISSEKI_HOUKOKU_ENTRY>(this.headerData).SetSystemProperty(this.headerData, true);
                            // 元EntityにDELETE_FLG更新
                            this.dao.Update(this.headerData);

                            // トランザクション終了
                            tran.Commit();
                            msgLogic.MessageBoxShow("I001", "削除");

                            // 画面を閉じるタイミングでログテーブルに登録しているため、
                            // 削除のトランザクション完了後（コミット後）に画面を閉じる。
                            var parentForm = (BusinessBaseForm)this.form.Parent;
                            parentForm.Close();

                            return;
                        }
                    }
                    // 実績報告書修正データを取得
                    this.GetJissekiHokokuData(this.form.systemid);
                    // ヘッダーデータ設定する
                    this.SetHeaderData();
                    // MultiRow初期化用DataTable
                    this.MultiRowDataTableInit();
                    // MultiRowにデータを追加する
                    this.MultiRowInit();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        /// <summary>
        /// 更新用リスト設定
        /// </summary>
        private void SetUpdateData()
        {
            LogUtility.DebugMethodStart();

            int seq = this.dao.GetMaxSeq(this.form.systemid);

            try
            {
                // ヘッダーデータを設定
                this.updateheaderData.SEQ = seq + 1;
                this.headerData.DELETE_FLG = true;
                new DataBinderLogic<T_JISSEKI_HOUKOKU_ENTRY>(this.headerData).SetSystemProperty(this.headerData, true);

                // 明細データを設定
                this.updateDetailData = new List<T_JISSEKI_HOUKOKU_SHORI_DETAIL>();
                // 明細マニフェストデータを設定
                this.updateManiDetailData = new List<T_JISSEKI_HOUKOKU_MANIFEST_DETAIL>();
                // updateDetailData
                T_JISSEKI_HOUKOKU_SHORI_DETAIL updateDetailRow = new T_JISSEKI_HOUKOKU_SHORI_DETAIL();
                // T_JISSEKI_HOUKOKU_MANIFEST_DETAILデータ
                T_JISSEKI_HOUKOKU_MANIFEST_DETAIL maniData = new T_JISSEKI_HOUKOKU_MANIFEST_DETAIL();
                // DetailSystemIdを取得
                int detailSystemId = this.createSystemIdForJissekiHokoku();

                foreach (T_JISSEKI_HOUKOKU_SHORI_DETAIL row in this.detailData)
                {
                    for (int i = 0; i < this.form.grdIchiran.RowCount; i++)
                    {
                        // 同じきーデータ設定
                        if (row.SYSTEM_ID.ToString().Equals(this.form.grdIchiran.Rows[i]["SystemId"].Value.ToString())
                            && row.SEQ.ToString().Equals(this.form.grdIchiran.Rows[i]["Seq"].Value.ToString())
                            && row.DETAIL_SYSTEM_ID.ToString().Equals(this.form.grdIchiran.Rows[i]["DetailSystemId"].Value.ToString()))
                        {
                            updateDetailRow = new T_JISSEKI_HOUKOKU_SHORI_DETAIL();
                            updateDetailRow = row;
                            updateDetailRow.SEQ = seq + 1;
                            updateDetailRow.DETAIL_SYSTEM_ID = detailSystemId;
                            // 産業廃棄物・特別管理産業廃棄物処理施設の種類
                            updateDetailRow.SHORI_SHISETSU_NAME = Convert.ToString(this.form.grdIchiran.Rows[i]["HoukokushoBunruiName"].Value);
                            // 施設コード
                            updateDetailRow.SHORI_SHISETSU_CD = Convert.ToString(this.form.grdIchiran.Rows[i]["SisetsuCd"].Value);
                            // 種類名1
                            updateDetailRow.HAIKI_SHURUI_NAME1 = Convert.ToString(this.form.grdIchiran.Rows[i]["Shurui1Name"].Value);
                            // 種類1
                            if (this.form.grdIchiran.Rows[i]["Shurui1"].Value != null && !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["Shurui1"].Value.ToString()))
                            {
                                updateDetailRow.SBN_RYOU1 = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["Shurui1"].Value);
                            }
                            else
                            {
                                updateDetailRow.SBN_RYOU1 = SqlDecimal.Null;
                            }
                            // 種類名2
                            updateDetailRow.HAIKI_SHURUI_NAME2 = Convert.ToString(this.form.grdIchiran.Rows[i]["Shurui2Name"].Value);
                            // 種類2
                            if (this.form.grdIchiran.Rows[i]["Shurui2"].Value != null && !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["Shurui2"].Value.ToString()))
                            {
                                updateDetailRow.SBN_RYOU2 = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["Shurui2"].Value);
                            }
                            else
                            {
                                updateDetailRow.SBN_RYOU2 = SqlDecimal.Null;
                            }
                            // 種類名3
                            updateDetailRow.HAIKI_SHURUI_NAME3 = Convert.ToString(this.form.grdIchiran.Rows[i]["Shurui3Name"].Value);
                            // 種類3
                            if (this.form.grdIchiran.Rows[i]["Shurui3"].Value != null && !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["Shurui3"].Value.ToString()))
                            {
                                updateDetailRow.SBN_RYOU3 = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["Shurui3"].Value);
                            }
                            else
                            {
                                updateDetailRow.SBN_RYOU3 = SqlDecimal.Null;
                            }
                            // 種類名4
                            updateDetailRow.HAIKI_SHURUI_NAME4 = Convert.ToString(this.form.grdIchiran.Rows[i]["Shurui4Name"].Value);
                            // 種類4
                            if (this.form.grdIchiran.Rows[i]["Shurui4"].Value != null && !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["Shurui4"].Value.ToString()))
                            {
                                updateDetailRow.SBN_RYOU4 = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["Shurui4"].Value);
                            }
                            else
                            {
                                updateDetailRow.SBN_RYOU4 = SqlDecimal.Null;
                            }
                            // 種類名
                            updateDetailRow.SBN_AFTER_HAIKI_NAME = Convert.ToString(this.form.grdIchiran.Rows[i]["ShuruiName"].Value);
                            // 排出量
                            if (this.form.grdIchiran.Rows[i]["HaishutsuRyou"].Value != null && !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["HaishutsuRyou"].Value.ToString()))
                            {
                                updateDetailRow.HST_RYOU = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["HaishutsuRyou"].Value);
                            }
                            else
                            {
                                updateDetailRow.HST_RYOU = SqlDecimal.Null;
                            }
                            // 処理方法cd
                            updateDetailRow.SHOBUN_HOUHOU_CD = Convert.ToString(this.form.grdIchiran.Rows[i]["ShobunHouhouCd"].Value);
                            // 処理方法Name
                            updateDetailRow.SHOBUN_HOUHOU_NAME = Convert.ToString(this.form.grdIchiran.Rows[i]["ShobunHouhouName"].Value);
                            // 処分量
                            if (this.form.grdIchiran.Rows[i]["ShobunRyou"].Value != null && !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["ShobunRyou"].Value.ToString()))
                            {
                                updateDetailRow.SBN_RYOU = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["ShobunRyou"].Value);
                            }
                            else
                            {
                                updateDetailRow.SBN_RYOU = SqlDecimal.Null;
                            }
                            // 処分量1
                            updateDetailRow.HAIKI_SHURUI_CD1 = Convert.ToString(this.form.grdIchiran.Rows[i]["ShobunRyou1"].Value);
                            // 処分量2
                            updateDetailRow.HAIKI_SHURUI_CD2 = Convert.ToString(this.form.grdIchiran.Rows[i]["ShobunRyou2"].Value);
                            // 処分量3
                            updateDetailRow.HAIKI_SHURUI_CD3 = Convert.ToString(this.form.grdIchiran.Rows[i]["ShobunRyou3"].Value);
                            // 処分量4
                            updateDetailRow.HAIKI_SHURUI_CD4 = Convert.ToString(this.form.grdIchiran.Rows[i]["ShobunRyou4"].Value);
                            // HST_JOU_CHIIKI_CD
                            updateDetailRow.HST_JOU_CHIIKI_CD = Convert.ToString(this.form.grdIchiran.Rows[i]["HST_JOU_CHIIKI_CD"].Value);
                            updateDetailData.Add(updateDetailRow);

                            // 明細マニフェストデータを取得する
                            DataTable resultManiDetailData = this.maniDetailDao.GetData(this.form.grdIchiran.Rows[i]["SystemId"].Value.ToString(),
                                                                                 this.form.grdIchiran.Rows[i]["Seq"].Value.ToString(),
                                                                                 this.form.grdIchiran.Rows[i]["DetailSystemId"].Value.ToString());
                            if (resultManiDetailData != null && resultManiDetailData.Rows.Count != 0)
                            {
                                // 更新用明細マニフェストデータを作成する
                                for (int j = 0; j < resultManiDetailData.Rows.Count; j++)
                                {
                                    maniData = new T_JISSEKI_HOUKOKU_MANIFEST_DETAIL();
                                    maniData.SYSTEM_ID = Convert.ToInt64(resultManiDetailData.Rows[j]["SYSTEM_ID"].ToString());
                                    maniData.SEQ = seq + 1;
                                    maniData.DETAIL_SYSTEM_ID = detailSystemId;
                                    maniData.DETAIL_ROW_NO = Convert.ToInt32(resultManiDetailData.Rows[j]["DETAIL_ROW_NO"].ToString());
                                    if (resultManiDetailData.Rows[j]["REPORT_ID"] != null
                                        && !string.IsNullOrWhiteSpace(resultManiDetailData.Rows[j]["REPORT_ID"].ToString()))
                                    {
                                        maniData.REPORT_ID = Convert.ToInt16(resultManiDetailData.Rows[j]["REPORT_ID"].ToString());
                                    }
                                    if (resultManiDetailData.Rows[j]["HAIKI_KBN_CD"] != null
                                        && !string.IsNullOrWhiteSpace(resultManiDetailData.Rows[j]["HAIKI_KBN_CD"].ToString()))
                                    {
                                        maniData.HAIKI_KBN_CD = Convert.ToInt16(resultManiDetailData.Rows[j]["HAIKI_KBN_CD"].ToString());
                                    }
                                    if (resultManiDetailData.Rows[j]["MANI_SYSTEM_ID"] != null
                                        && !string.IsNullOrWhiteSpace(resultManiDetailData.Rows[j]["MANI_SYSTEM_ID"].ToString()))
                                    {
                                        maniData.MANI_SYSTEM_ID = Convert.ToInt64(resultManiDetailData.Rows[j]["MANI_SYSTEM_ID"].ToString());
                                    }
                                    if (resultManiDetailData.Rows[j]["MANI_SEQ"] != null
                                        && !string.IsNullOrWhiteSpace(resultManiDetailData.Rows[j]["MANI_SEQ"].ToString()))
                                    {
                                        maniData.MANI_SEQ = Convert.ToInt32(resultManiDetailData.Rows[j]["MANI_SEQ"].ToString());
                                    }
                                    if (resultManiDetailData.Rows[j]["DEN_MANI_KANRI_ID"] != null)
                                    {
                                        maniData.DEN_MANI_KANRI_ID = Convert.ToString(resultManiDetailData.Rows[j]["DEN_MANI_KANRI_ID"]);
                                    }
                                    if (resultManiDetailData.Rows[j]["DEN_MANI_SEQ"] != null
                                        && !string.IsNullOrWhiteSpace(resultManiDetailData.Rows[j]["DEN_MANI_SEQ"].ToString()))
                                    {
                                        maniData.DEN_MANI_SEQ = Convert.ToInt32(resultManiDetailData.Rows[j]["DEN_MANI_SEQ"].ToString());
                                    }
                                    if (resultManiDetailData.Rows[j]["MANIFEST_ID"] != null)
                                    {
                                        maniData.MANIFEST_ID = Convert.ToString(resultManiDetailData.Rows[j]["MANIFEST_ID"]);
                                    }
                                    this.updateManiDetailData.Add(maniData);
                                }
                            }
                            detailSystemId++;
                        }
                    }
                }
                new DataBinderLogic<T_JISSEKI_HOUKOKU_ENTRY>(this.updateheaderData).SetSystemProperty(this.updateheaderData, false);
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region [F11]行削除イベント
        /// <summary>
        /// [F11]行削除イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //フォーカスがグリッドにあるかを調べる。
                if (this.form.grdIchiran.CurrentRow == null)
                {
                    return;
                }

                //該当行を削除。
                this.form.grdIchiran.Rows.RemoveAt(this.form.grdIchiran.CurrentCell.RowIndex);
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region [F12]閉じるボタンイベント
        /// <summary>
        /// [F12]閉じるボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        /// <summary>
        /// ダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                // マニ明細ポップアップを呼び出し
                var callForm = new Shougun.Core.PaperManifest.JissekiHokokuSyuseiPopup.UIForm();

                callForm.Params = new object[3];
                // SYSTEM_ID
                callForm.Params[0] = this.form.grdIchiran.Rows[e.RowIndex]["SystemId"].Value.ToString();
                // SEQ
                callForm.Params[1] = this.form.grdIchiran.Rows[e.RowIndex]["Seq"].Value.ToString();
                // DETAIL_SYSTEM_ID
                callForm.Params[2] = this.form.grdIchiran.Rows[e.RowIndex]["DetailSystemId"].Value.ToString();

                // 画面表示
                callForm.ShowDialog();
            }
            catch (Exception ex)
            {
                LogUtility.Error("cellDoubleClick", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
            }
        }

        /// <summary>
        /// 印刷用データを取得する
        /// </summary>
        private void CreateEntry()
        {
            this.logic = new Shougun.Core.PaperManifest.JissekiHokokuSisetsu.LogicClass(new Shougun.Core.PaperManifest.JissekiHokokuSisetsu.UIForm());

            // 帳票ヘッダーデータ
            logic.tjHoukokuEntry = new T_JISSEKI_HOUKOKU_ENTRY();
            // 帳票明細データ
            logic.tjHoukokuSyoriEntryList = new List<T_JISSEKI_HOUKOKU_SHORI_DETAIL>();


            T_JISSEKI_HOUKOKU_SHORI_DETAIL row = new T_JISSEKI_HOUKOKU_SHORI_DETAIL();
            // 廃棄種類コード
            DataTable haikiSyurui = new DataTable();
            haikiSyurui.Columns.Add("CD1");
            haikiSyurui.Columns.Add("CD2");
            haikiSyurui.Columns.Add("CD3");
            haikiSyurui.Columns.Add("CD4");
            DataRow haikiRow = haikiSyurui.NewRow();
            for (int i = 0; i < this.form.grdIchiran.RowCount; i++)
            {
                row = new T_JISSEKI_HOUKOKU_SHORI_DETAIL();
                // 明細データを設定
                // 和暦でDataTimeを文字列に変換する
                string nendo = this.form.HokokuNendo.Text;
                System.Globalization.CultureInfo ci =
                new System.Globalization.CultureInfo("ja-JP", false);
                ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
                row.HOUKOKU_YEAR = Convert.ToDateTime(string.Format("{0}-01-01", nendo.Substring(0, nendo.Length - 2))).ToString("gy年", ci);

                // 種類名1
                row.HAIKI_SHURUI_NAME1 = this.form.grdIchiran.Rows[i]["Shurui1Name"].Value.ToString();
                // 種類1
                if (this.form.grdIchiran.Rows[i]["Shurui1"].Value != null &&
                    !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["Shurui1"].Value.ToString()))
                {
                    row.SBN_RYOU1 = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["Shurui1"].Value.ToString());
                }
                // 種類名2
                row.HAIKI_SHURUI_NAME2 = this.form.grdIchiran.Rows[i]["Shurui2Name"].Value.ToString();
                // 種類2
                if (this.form.grdIchiran.Rows[i]["Shurui2"].Value != null &&
                   !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["Shurui2"].Value.ToString()))
                {
                    row.SBN_RYOU2 = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["Shurui2"].Value.ToString());
                }
                // 種類名3
                row.HAIKI_SHURUI_NAME3 = this.form.grdIchiran.Rows[i]["Shurui3Name"].Value.ToString();
                // 種類3
                if (this.form.grdIchiran.Rows[i]["Shurui3"].Value != null &&
                   !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["Shurui3"].Value.ToString()))
                {
                    row.SBN_RYOU3 = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["Shurui3"].Value.ToString());
                }
                // 種類名4
                row.HAIKI_SHURUI_NAME4 = this.form.grdIchiran.Rows[i]["Shurui4Name"].Value.ToString();
                // 種類4
                if (this.form.grdIchiran.Rows[i]["Shurui4"].Value != null &&
                   !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["Shurui4"].Value.ToString()))
                {
                    row.SBN_RYOU4 = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["Shurui4"].Value.ToString());
                }

                // (特別管理)産業廃棄物の種類
                row.SHORI_SHISETSU_NAME = this.form.grdIchiran.Rows[i]["HoukokushoBunruiName"].Value.ToString();
                // 施設コード
                row.SHORI_SHISETSU_CD = this.form.grdIchiran.Rows[i]["SisetsuCd"].Value.ToString();
                // 処分量1
                row.HAIKI_SHURUI_CD1 = this.form.grdIchiran.Rows[i]["ShobunRyou1"].Value.ToString();
                // 処分量2
                row.HAIKI_SHURUI_CD2 = this.form.grdIchiran.Rows[i]["ShobunRyou2"].Value.ToString();
                // 処分量3
                row.HAIKI_SHURUI_CD3 = this.form.grdIchiran.Rows[i]["ShobunRyou3"].Value.ToString();
                // 処分量4
                row.HAIKI_SHURUI_CD4 = this.form.grdIchiran.Rows[i]["ShobunRyou4"].Value.ToString();
                // 種類名
                row.SBN_AFTER_HAIKI_NAME = this.form.grdIchiran.Rows[i]["ShuruiName"].Value.ToString();
                // 排出量
                if (this.form.grdIchiran.Rows[i]["HaishutsuRyou"].Value != null &&
                   !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["HaishutsuRyou"].Value.ToString()))
                {
                    row.HST_RYOU = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["HaishutsuRyou"].Value.ToString());
                }
                // 処理方法
                row.SHOBUN_HOUHOU_NAME = this.form.grdIchiran.Rows[i]["ShobunHouhouName"].Value.ToString();
                // 処分量
                if (this.form.grdIchiran.Rows[i]["ShobunRyou"].Value != null &&
                   !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["ShobunRyou"].Value.ToString()))
                {
                    row.SBN_RYOU = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["ShobunRyou"].Value.ToString());
                }
                // HST_JOU_CHIIKI_CD
                row.HST_JOU_CHIIKI_CD = this.form.grdIchiran.Rows[i]["HST_JOU_CHIIKI_CD"].Value.ToString();
                if (i == 0)
                {
                    haikiRow = haikiSyurui.NewRow();
                    // 廃棄種類コード1
                    haikiRow["CD1"] = this.form.grdIchiran.Rows[i]["ShobunRyou1"].Value.ToString();
                    // 廃棄種類コード2
                    haikiRow["CD2"] = this.form.grdIchiran.Rows[i]["ShobunRyou2"].Value.ToString();
                    // 廃棄種類コード3
                    haikiRow["CD3"] = this.form.grdIchiran.Rows[i]["ShobunRyou3"].Value.ToString();
                    // 廃棄種類コード4
                    haikiRow["CD4"] = this.form.grdIchiran.Rows[i]["ShobunRyou4"].Value.ToString();

                    haikiSyurui.Rows.Add(haikiRow);
                }
                for (int j = 0; j < haikiSyurui.Rows.Count; j++)
                {
                    if (this.form.grdIchiran.Rows[i]["ShobunRyou1"].Value.ToString().Equals(haikiSyurui.Rows[j]["CD1"].ToString()) &&
                        this.form.grdIchiran.Rows[i]["ShobunRyou2"].Value.ToString().Equals(haikiSyurui.Rows[j]["CD2"].ToString()) &&
                        this.form.grdIchiran.Rows[i]["ShobunRyou3"].Value.ToString().Equals(haikiSyurui.Rows[j]["CD3"].ToString()) &&
                        this.form.grdIchiran.Rows[i]["ShobunRyou4"].Value.ToString().Equals(haikiSyurui.Rows[j]["CD4"].ToString()))
                    {
                        row.PAGE_NO = j + 1;
                        continue;
                    }
                    else
                    {
                        haikiRow = haikiSyurui.NewRow();
                        // 廃棄種類コード1
                        haikiRow["CD1"] = this.form.grdIchiran.Rows[i]["ShobunRyou1"].Value.ToString();
                        // 廃棄種類コード2
                        haikiRow["CD2"] = this.form.grdIchiran.Rows[i]["ShobunRyou2"].Value.ToString();
                        // 廃棄種類コード3
                        haikiRow["CD3"] = this.form.grdIchiran.Rows[i]["ShobunRyou3"].Value.ToString();
                        // 廃棄種類コード4
                        haikiRow["CD4"] = this.form.grdIchiran.Rows[i]["ShobunRyou4"].Value.ToString();

                        haikiSyurui.Rows.Add(haikiRow);
                        // PAGE_NO
                        row.PAGE_NO = haikiSyurui.Rows.Count + 1;
                    }
                }
                logic.tjHoukokuSyoriEntryList.Add(row);
            }

        }

        #region ゼロパティング
        /// <summary>
        /// のSYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        private int createSystemIdForJissekiHokoku()
        {
            int returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = 400;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = 400;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }
        #endregion

        #region HeaderForm取得
        /// <summary>
        /// HeaderForm取得
        /// </summary>
        /// <param name="hs">hs</param>
        public void SetHeaderInfo(HeaderForm hs)
        {
            LogUtility.DebugMethodStart(hs);

            this.headerForm = hs;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region デフォルトメソッド
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

        #region マスタ検索処理
        /// <summary>
        /// 処理方法検索処理
        /// </summary>
        /// <param name="chiikiCd">地域CD</param>
        /// <param name="cd">方法CD</param>
        internal M_CHIIKIBETSU_SHOBUN[] GetShobunHouhou(string chiikiCd, string cd)
        {
            LogUtility.DebugMethodStart(chiikiCd, cd);
            M_CHIIKIBETSU_SHOBUN dto = new M_CHIIKIBETSU_SHOBUN();
            dto.SHOBUN_HOUHOU_CD = cd;
            dto.CHIIKI_CD = chiikiCd;
            IM_CHIIKIBETSU_SHOBUNDao dao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_SHOBUNDao>();
            M_CHIIKIBETSU_SHOBUN[] results = dao.GetAllValidData(dto);
            LogUtility.DebugMethodEnd();
            return results;
        }

        /// <summary>
        /// 分類検索処理
        /// </summary>
        /// <param name="chiikiCd">地域CD</param>
        /// <param name="houhouCd">方法CD</param>
        internal M_CHIIKIBETSU_BUNRUI[] GetBunrui(string chiikiCd, string cd)
        {
            LogUtility.DebugMethodStart(chiikiCd, cd);
            M_CHIIKIBETSU_BUNRUI dto = new M_CHIIKIBETSU_BUNRUI();
            dto.HOUKOKUSHO_BUNRUI_CD = cd;
            dto.CHIIKI_CD = chiikiCd;
            IM_CHIIKIBETSU_BUNRUIDao dao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_BUNRUIDao>();
            M_CHIIKIBETSU_BUNRUI[] results = dao.GetAllValidData(dto);
            LogUtility.DebugMethodEnd();
            return results;
        }
        #endregion

        /// <summary>
        /// 数量フォーマット
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertSuuryo(string value)
        {
            string result = string.Empty;
            if (value != null)
            {
                decimal dec = Convert.ToDecimal(value);
                string manifestSuuryoFormatCD = this.sysInfoEntity.MANIFEST_SUURYO_FORMAT_CD.ToString();
                dec = this.mlogic.GetSuuryoRound(dec, manifestSuuryoFormatCD);
                result = dec.ToString();
            }
            return result;
        }
    }
}