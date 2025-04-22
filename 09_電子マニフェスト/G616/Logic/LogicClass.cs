using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Reflection;
using r_framework.CustomControl;
using r_framework.Dto;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Framework.Exceptions;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;
using System.Data;
using Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku;
using System.Data.SqlTypes;
using Shougun.Core.Message;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Const;
using Seasar.Dao;

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region 定数
        /// <summary>ボタン定義ファイルパス</summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake.Setting.ButtonSetting.xml";

        /// <summary>cell names</summary>
        internal string CELL_NAME_HAIKI_SHURUI_CD = "HAIKI_SHURUI_CD";
        internal string CELL_NAME_HAIKI_SHURUI_NAME = "HAIKI_SHURUI_NAME";
        internal string CELL_NAME_SBN_HOUHOU_CD = "SBN_HOUHOU_CD";
        internal string CELL_NAME_SBN_HOUHOU_NAME = "SBN_HOUHOU_NAME";
        internal string CELL_NAME_HAIKI_SUU = "HAIKI_SUU";
        internal string CELL_NAME_HAIKI_UNIT_CD = "HAIKI_UNIT_CD";
        internal string CELL_NAME_HAIKI_UNIT_NAME = "HAIKI_UNIT_NAME";
        internal string CELL_NAME_KANSAN_SUU = "KANSAN_SUU";
        internal string CELL_NAME_GENNYOU_SUU = "GENNYOU_SUU";
        internal string CELL_NAME_HAIKI_NAME_CD = "HAIKI_NAME_CD";
        internal string CELL_NAME_HAIKI_NAME_NAME = "HAIKI_NAME_NAME";
        internal string CELL_NAME_SBN_ENDREP_KBN = "SBN_ENDREP_KBN";
        internal string CELL_NAME_SBN_ENDREP_KBN_NAME = "SBN_ENDREP_KBN_NAME";
        internal string CELL_NAME_MANI_SHURUI = "MANI_SHURUI";
        internal string CELL_NAME_NEXT_MANIFEST_ID = "NEXT_MANIFEST_ID";
        internal string CELL_NAME_LAST_SBN_END_DATE = "LAST_SBN_END_DATE";
        internal string CELL_NAME_DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID";
        internal string CELL_NAME_SEQ = "SEQ";
        internal string CELL_NAME_WARIAI = "WARIAI";

        /// <summary>区分</summary>
        internal short SBN_ENDREP_KBN_MIDDLE = 1;
        internal short SBN_ENDREP_KBN_LAST = 2;
        #endregion

        #region プロパティ
        /// <summary>DT_R18_EX.SYSTEM_ID</summary>
        internal long SystemId { get; set; }

        /// <summary>DT_R18_EX.KANRI_ID( or DT_R18.KANRI_ID)</summary>
        internal string KanriId { get; set; }

        /// <summary>最終処分終了報告済みFlg</summary>
        internal bool IsLastSbnEndrepFlg { get; set; }

        /// <summary>混廃振分済み、紐付け済みFlg</summary>
        internal bool isRelationalMixMani { get; set; }
        #endregion

        #region フィールド
        /// <summary>DTO</summary>
        private DTOClass dto;

        /// <summary>Form</summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>DAO</summary>
        private DT_R18_MIXDAOClass dt_R18_MixDao;

        private T_MANIFEST_RELATIONDAOClass t_mani_relDao;

        /// <summary>DT_MF_TOC</summary>
        private DT_MF_TOC dt_mf_toc;

        /// <summary>元データの電マニ情報</summary>
        internal DT_R18 dt_R18;

        /// <summary>元データの電マニ情報(収集運搬情報)</summary>
        private DT_R19[] dt_R19;

        /// <summary>更新前DT_R18_MIX</summary>
        private DT_R18_MIX[] beforeDtR18MixList;

        /// <summary>混合種類名検索Dao</summary>
        private GetKongouNameDaoCls GetKongouNamedao;

        /// <summary>混合廃棄物検索Dao</summary>
        private KongouHaikibutuDaoCls GetKongouHaikibutudao;

        /// <summary>加入者情報検索Dao</summary>
        private EDI_PASSWORD_DaoCls GetEDI_PASSWORDdao;

        /// <summary>電子廃棄物種類Dao</summary>
        private DenshiHaikiShuruiDaoCls denshiHaikiShuruiDao;

        /// <summary>最終処分終了報告、取消実行済みフラグ</summary>
        private bool isLastReportedFlg = false;

        /// <summary>JWNET送信中かフラグ</summary>
        private bool runningOperationFlg = false;

        /// <summary>元データの電マニ情報が手動かどうか</summary>
        private bool isNotEdiData = false;

        /// <summary>DT_R18_EXと紐付け済みかどうか</summary>
        private bool isExsRelationData = false;

        /// <summary>前回値</summary>
        private string beforeHaikiShuruiCd;
        private string beforeHaikiNameCd;
        private decimal? beforeHaikiSuu;
        private string beforeHaikiUnitCd;
        private decimal? beforeKansanSuu;
        private string beforeSbnHouhouCd;
        private int? beforeWariai;

        /// <summary>共通</summary>
        private Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        /// <summary>マニフェスト情報数量書式CD</summary>
        internal string ManifestSuuryoFormatCD = String.Empty;

        /// <summary>マニフェスト情報数量書式</summary>
        internal string ManifestSuuryoFormat = String.Empty;

        /// <summary>マニ換算値情報基本単位名称</summary>
        private string unit_name = String.Empty;

        public IM_UNITDao unitDao;
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
            this.dt_R18_MixDao = DaoInitUtility.GetComponent<DT_R18_MIXDAOClass>();
            this.t_mani_relDao = DaoInitUtility.GetComponent<T_MANIFEST_RELATIONDAOClass>();
            this.dt_mf_toc = new DT_MF_TOC();
            this.dt_R18 = new DT_R18();
            this.dt_R19 = null;
            this.beforeDtR18MixList = null;
            this.isLastReportedFlg = false;
            this.runningOperationFlg = false;
            this.isNotEdiData = false;
            this.isExsRelationData = false;
            this.beforeHaikiShuruiCd = string.Empty;
            this.beforeHaikiNameCd = string.Empty;
            this.beforeHaikiSuu = null;
            this.beforeHaikiUnitCd = string.Empty;
            this.beforeKansanSuu = null;
            this.beforeSbnHouhouCd = string.Empty;
            this.beforeWariai = null;
            this.GetKongouNamedao = DaoInitUtility.GetComponent<GetKongouNameDaoCls>();
            this.GetKongouHaikibutudao = DaoInitUtility.GetComponent<KongouHaikibutuDaoCls>();
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
            this.GetEDI_PASSWORDdao = DaoInitUtility.GetComponent<EDI_PASSWORD_DaoCls>();
            this.denshiHaikiShuruiDao = DaoInitUtility.GetComponent<DenshiHaikiShuruiDaoCls>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 初期化

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // ボタンを初期化
                this.ButtonInit();

                // footボタン処理イベントを初期化
                this.EventInit();

                this.GetManiInfo();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
        }

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
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
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentform.bt_func9);
            parentform.bt_func9.Click += new EventHandler(this.form.Regist);

            //行挿入ボタン(F10)イベント生成
            parentform.bt_func10.Click += new EventHandler(this.form.AddRow);

            //行削除ボタン(F11)イベント生成
            parentform.bt_func11.Click += new EventHandler(this.form.DeleteRow);

            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.FormClose);
        }
        #endregion

        #region 電マニ情報取得
        /// <summary>
        /// 電マニ情報を取得し、フィールドにセットする
        /// </summary>
        private void GetManiInfo()
        {
            var dtMfTocDao = DaoInitUtility.GetComponent<DT_MF_TOCDaoCls>();
            var condistionMfToc = new DT_MF_TOC();
            condistionMfToc.KANRI_ID = this.KanriId;
            var mfToc = dtMfTocDao.GetDataForEntity(condistionMfToc);

            // MF_TOCをセット
            this.dt_mf_toc = mfToc;

            if (mfToc != null)
            {
                // DT_R18を取得、セット
                var dtR18Dao = DaoInitUtility.GetComponent<DT_R18DaoCls>();
                var conditionDtR18 = new DT_R18();
                conditionDtR18.KANRI_ID = this.KanriId;
                conditionDtR18.SEQ = mfToc.LATEST_SEQ;
                var dtR18 = dtR18Dao.GetDataForEntity(conditionDtR18);
                if (dtR18 != null)
                {
                    this.dt_R18 = dtR18;
                }

                // DT_R19を取得、セット
                var dtR19Dao = DaoInitUtility.GetComponent<DT_R19DaoCls>();
                var conditionDtR19 = new DT_R19();
                conditionDtR19.KANRI_ID = this.KanriId;
                conditionDtR19.SEQ = mfToc.LATEST_SEQ;
                var dtR19 = dtR19Dao.GetAllValidData(conditionDtR19);
                if (dtR19 != null && dtR19.Length > 0)
                {
                    this.dt_R19 = dtR19;
                }

            }

            // DT_R18_MIXを取得、セット
            var conditionDtR18Mix = new DT_R18_MIX();
            conditionDtR18Mix.KANRI_ID = this.KanriId;
            conditionDtR18Mix.SYSTEM_ID = this.SystemId;

            this.beforeDtR18MixList = this.dt_R18_MixDao.GetDtR18Mix(conditionDtR18Mix);

            //数値フォーマット情報取得
            M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
            ManifestSuuryoFormatCD = mSysInfo.MANIFEST_SUURYO_FORMAT_CD.ToString();
            ManifestSuuryoFormat = mSysInfo.MANIFEST_SUURYO_FORMAT.ToString();

            //混合数量単位を取得
            SqlDecimal kakuteiSuu = 0;
            string kakuteiUnitCd = string.Empty;

            if (this.dt_R18 != null)
            {
                if (!this.dt_R18.HAIKI_SUU.IsNull)
                {
                    kakuteiSuu = this.dt_R18.HAIKI_SUU.Value;
                }
                kakuteiUnitCd = this.dt_R18.HAIKI_UNIT_CODE;
            }

            if (!kakuteiSuu.IsNull)
            {
                this.form.cntxt_JissekiSuryo.Text = kakuteiSuu.Value.ToString();
            }
            this.form.canTxt_JissekiTaniCd.Text = kakuteiUnitCd;
            if (!string.IsNullOrEmpty(kakuteiUnitCd))
            {
                M_UNIT unit = this.unitDao.GetDataByCd(Convert.ToInt32(kakuteiUnitCd));
                if (unit != null)
                {
                    this.form.canTxt_JissekiTaniCd.Text = unit.UNIT_CD.ToString();
                    this.form.ctxt_JissekiTaniName.Text = unit.UNIT_NAME_RYAKU;
                }
            }

            if (!mSysInfo.MANI_KANSAN_KIHON_UNIT_CD.IsNull)
            {
                M_UNIT unit = this.unitDao.GetDataByCd(mSysInfo.MANI_KANSAN_KIHON_UNIT_CD.Value);
                if (unit != null)
                {
                    this.unit_name = unit.UNIT_NAME;
                }
            }

            if (this.dt_R18 != null)
            {
                this.form.ctxt_EDIMemberID.Text = this.dt_R18.HST_SHA_EDI_MEMBER_ID;
                this.form.ctxt_HaisyutuJigyousha.Text = this.dt_R18.HST_SHA_NAME;
                this.form.ctxt_HaisyutuJigyouba.Text = this.dt_R18.HST_JOU_NAME;
                this.form.ctxt_HaikiName.Text = this.dt_R18.HAIKI_NAME;

                string haikiShuruiCd = string.Empty;
                if (this.dt_R18.HAIKI_DAI_CODE != null)
                {
                    haikiShuruiCd += dt_R18.HAIKI_DAI_CODE;
                }
                if (this.dt_R18.HAIKI_CHU_CODE != null)
                {
                    haikiShuruiCd += dt_R18.HAIKI_CHU_CODE;
                }
                if (this.dt_R18.HAIKI_SHO_CODE != null)
                {
                    haikiShuruiCd += dt_R18.HAIKI_SHO_CODE;
                }

                var denshiHaikiShurui = denshiHaikiShuruiDao.GetDataByCd(haikiShuruiCd);
                if (denshiHaikiShurui != null)
                {
                    this.form.ctxt_HaikiShuruiName.Text = denshiHaikiShurui.HAIKI_SHURUI_NAME;
                }
            }
        }
        #endregion

        #endregion

        #region Function Button以外のユーザー操作時処理

        #region 前回値保存
        /// <summary>
        /// 前回値保存
        /// Validatingメソッドで前回値と今回値の比較をし
        /// 変更がなかったら何もしないようにする
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellIndex"></param>
        internal void SetBeforeValue(int rowIndex, int cellIndex)
        {
            try
            {
                if (rowIndex < 0 || cellIndex < 0)
                {
                    return;
                }

                string columnName = this.form.haikiButsuDetail.Columns[cellIndex].Name;
                if (this.CELL_NAME_HAIKI_SHURUI_CD.Equals(columnName))
                {
                    this.beforeHaikiShuruiCd =
                        this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value != null
                            ? this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value.ToString()
                            : string.Empty;
                }
                else if (this.CELL_NAME_HAIKI_NAME_CD.Equals(columnName))
                {
                    this.beforeHaikiNameCd =
                        this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value != null
                            ? this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value.ToString()
                            : string.Empty;
                }
                else if (this.CELL_NAME_HAIKI_SUU.Equals(columnName))
                {
                    decimal tempHaikiSuu = 0;
                    if (this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value == null)
                    {
                        this.beforeHaikiSuu = null;
                    }
                    else if (decimal.TryParse(this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value.ToString(), out tempHaikiSuu))
                    {
                        this.beforeHaikiSuu = tempHaikiSuu;
                    }
                    else
                    {
                        this.beforeHaikiSuu = null;
                    }
                }
                else if (this.CELL_NAME_HAIKI_UNIT_CD.Equals(columnName))
                {
                    this.beforeHaikiUnitCd =
                        this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value != null
                        ? this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value.ToString()
                        : string.Empty;
                }
                else if (this.CELL_NAME_KANSAN_SUU.Equals(columnName))
                {
                    decimal tempKansanSuu = 0;
                    if (this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value == null)
                    {
                        this.beforeKansanSuu = null;
                    }
                    else if (decimal.TryParse(this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value.ToString(), out tempKansanSuu))
                    {
                        this.beforeKansanSuu = tempKansanSuu;
                    }
                    else
                    {
                        this.beforeKansanSuu = null;
                    }
                }
                else if (this.CELL_NAME_SBN_HOUHOU_CD.Equals(columnName))
                {
                    this.beforeSbnHouhouCd =
                        this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value != null
                        ? this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value.ToString()
                        : string.Empty;
                }
                else if (this.CELL_NAME_WARIAI.Equals(columnName))
                {
                    int tempWariai = 0;
                    if (this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value == null)
                    {
                        this.beforeWariai = null;
                    }
                    else if (int.TryParse(this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex].Value.ToString(), out tempWariai))
                    {
                        this.beforeWariai = tempWariai;
                    }
                    else
                    {
                        this.beforeWariai = null;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetBeforeValue", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetBeforeValue", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
        }
        #endregion

        #region Cell_Validating
        /// <summary>
        /// CellValidating
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellIndex"></param>
        /// <returns>true:正常、false:異常</returns>
        internal bool CellsValidating(int rowIndex, int cellIndex, out bool catchErr)
        {
            catchErr = false;
            try
            {
                bool returnVal = true;

                if (rowIndex < 0 || cellIndex < 0)
                {
                    return returnVal;
                }

                var DsMasterLogic = new DenshiMasterDataLogic();
                var dto = new DenshiSearchParameterDtoCls();
                var cell = this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex] as ICustomDataGridControl;
                var msgLogic = new MessageBoxShowLogic();

                if (cell != null)
                {
                    string columnName = this.form.haikiButsuDetail.Columns[cellIndex].Name;
                    if (this.CELL_NAME_HAIKI_SHURUI_CD.Equals(columnName))
                    {
                        // 廃棄物種類

                        if (string.IsNullOrEmpty(cell.GetResultText()))
                        {
                            // 廃棄物種類名をクリア
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_SHURUI_NAME].Value = string.Empty;
                            return returnVal;
                        }

                        this.form.haikiButsuDetail.Rows[rowIndex].Cells[columnName].Value =
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[columnName].Value.ToString().PadLeft(7, '0').ToUpper();

                        dto.HAIKISHURUICD = cell.GetResultText();
                        if (this.dt_R18 != null)
                        {
                            dto.EDI_MEMBER_ID = this.dt_R18.HST_SHA_EDI_MEMBER_ID;
                        }
                        var haikiShuruiDt = DsMasterLogic.GetDenshiHaikiShuruiData(dto);
                        if (haikiShuruiDt == null || haikiShuruiDt.Rows.Count < 1)
                        {
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_SHURUI_NAME].Value = string.Empty;
                            msgLogic.MessageBoxShow("E020", "廃棄種類コード");
                            returnVal = false;
                        }
                        else
                        {
                            // キーが1つなので、複数ヒットはないはず
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_SHURUI_NAME].Value
                                = haikiShuruiDt.Rows[0]["HAIKI_SHURUI_NAME"];
                        }
                    }
                    else if (this.CELL_NAME_HAIKI_NAME_CD.Equals(columnName))
                    {
                        // 廃棄物名称
                        if (string.IsNullOrEmpty(cell.GetResultText()))
                        {
                            // 廃棄物種類名をクリア
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_NAME_NAME].Value = string.Empty;
                            return returnVal;
                        }

                        this.form.haikiButsuDetail.Rows[rowIndex].Cells[columnName].Value =
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[columnName].Value.ToString().PadLeft(6, '0').ToUpper();

                        dto.HAIKI_NAME_CD = cell.GetResultText();
                        dto.KANRI_ID = this.KanriId;
                        //20151006 hoanghm #11968 start
                        if (this.dt_R18 != null)
                        {
                            dto.EDI_MEMBER_ID = this.dt_R18.HST_SHA_EDI_MEMBER_ID;
                        }
                        //20151006 hoanghm #11968 end
                        var haikiNameDt = DsMasterLogic.GetDenshiHaikiNameData(dto);
                        if (haikiNameDt == null || haikiNameDt.Rows.Count < 1)
                        {
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_NAME_NAME].Value = string.Empty;
                            msgLogic.MessageBoxShow("E020", "廃棄種類コード");
                            returnVal = false;
                        }
                        else
                        {
                            // キーが1つなので、複数ヒットはないはず
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_NAME_NAME].Value
                                = haikiNameDt.Rows[0]["HAIKI_NAME"];
                        }
                    }
                    else if (this.CELL_NAME_SBN_ENDREP_KBN.Equals(columnName))
                    {
                        // 区分
                        if (string.IsNullOrEmpty(cell.GetResultText()))
                        {
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_SBN_ENDREP_KBN_NAME].Value = string.Empty;
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_LAST_SBN_END_DATE].ReadOnly = true;
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_LAST_SBN_END_DATE].Value = null;
                        }
                        else
                        {
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_SBN_ENDREP_KBN_NAME].Value
                                = this.GetSbnEndRepKbnName(cell.GetResultText());

                            if (this.SBN_ENDREP_KBN_LAST.ToString().Equals(cell.GetResultText()))
                            {
                                this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_LAST_SBN_END_DATE].ReadOnly = false;
                            }
                            else
                            {
                                this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_LAST_SBN_END_DATE].ReadOnly = true;
                                this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_LAST_SBN_END_DATE].Value = null;
                            }
                        }
                    }
                    else if (this.CELL_NAME_SBN_HOUHOU_CD.Equals(columnName))
                    {
                        // 処分方法
                        if (!string.IsNullOrEmpty(cell.GetResultText()))
                        {
                            this.form.haikiButsuDetail.Rows[rowIndex].Cells[columnName].Value =
                                this.form.haikiButsuDetail.Rows[rowIndex].Cells[columnName].Value.ToString().PadLeft(3, '0').ToUpper();
                        }
                    }
                }

                return returnVal;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CellsValidating", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CellsValidating", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }
        #endregion

        #region Cell_Leave
        /// <summary>
        /// Cell Leaveイベント
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellIndex"></param>
        internal void CellsLeave(int rowIndex, int cellIndex)
        {
            try
            {
                if (rowIndex < 0 || cellIndex < 0)
                {
                    return;
                }

                string columnName = this.form.haikiButsuDetail.Columns[cellIndex].Name;
                var cell = this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex] as ICustomDataGridControl;
                if (cell == null)
                {
                    return;
                }

                if (this.CELL_NAME_HAIKI_SHURUI_CD.Equals(columnName))
                {
                    if (!cell.GetResultText().Equals(this.beforeHaikiShuruiCd))
                    {
                        this.CalcKansanAndGenyouSuu(rowIndex, cellIndex);
                    }
                }
                else if (this.CELL_NAME_HAIKI_NAME_CD.Equals(columnName))
                {
                    if (!cell.GetResultText().Equals(this.beforeHaikiNameCd))
                    {
                        this.CalcKansanAndGenyouSuu(rowIndex, cellIndex);
                    }
                }
                else if (this.CELL_NAME_HAIKI_SUU.Equals(columnName))
                {
                    decimal tempHaikiSuu = 0;
                    if (this.beforeHaikiSuu == null
                        && !string.IsNullOrEmpty(cell.GetResultText()))
                    {
                        this.CalcKansanAndGenyouSuu(rowIndex, cellIndex);
                    }
                    else if (this.beforeHaikiSuu != null
                        && string.IsNullOrEmpty(cell.GetResultText()))
                    {
                        this.CalcKansanAndGenyouSuu(rowIndex, cellIndex);
                    }
                    else if (decimal.TryParse(cell.GetResultText(), out tempHaikiSuu)
                        && this.beforeHaikiSuu != tempHaikiSuu)
                    {
                        this.CalcKansanAndGenyouSuu(rowIndex, cellIndex);
                    }
                }
                else if (this.CELL_NAME_HAIKI_UNIT_CD.Equals(columnName))
                {
                    if (!cell.GetResultText().Equals(this.beforeHaikiUnitCd))
                    {
                        this.CalcKansanAndGenyouSuu(rowIndex, cellIndex);
                    }
                }
                else if (this.CELL_NAME_KANSAN_SUU.Equals(columnName))
                {
                    decimal tempKansanSuu = 0;
                    if (this.beforeKansanSuu == null
                        && !string.IsNullOrEmpty(cell.GetResultText()))
                    {
                        this.CalcKansanAndGenyouSuu(rowIndex, cellIndex);
                    }
                    else if (this.beforeKansanSuu != null
                        && string.IsNullOrEmpty(cell.GetResultText()))
                    {
                        this.CalcKansanAndGenyouSuu(rowIndex, cellIndex);
                    }
                    else if (decimal.TryParse(cell.GetResultText(), out tempKansanSuu)
                        && this.beforeKansanSuu != tempKansanSuu)
                    {
                        this.CalcKansanAndGenyouSuu(rowIndex, cellIndex);
                    }
                }
                else if (this.CELL_NAME_SBN_HOUHOU_CD.Equals(columnName))
                {
                    if (!cell.GetResultText().Equals(this.beforeSbnHouhouCd))
                    {
                        this.CalcKansanAndGenyouSuu(rowIndex, cellIndex);
                    }
                }
                else if (this.CELL_NAME_WARIAI.Equals(columnName))
                {
                    if (this.form.cntxt_JissekiSuryo.Text == null || this.form.cntxt_JissekiSuryo.Text == String.Empty)
                    {
                    }
                    else
                    {
                        switch (this.ChkGridWariai(rowIndex))
                        {
                            case 0://正常
                                break;

                            case 1://空
                                return;

                            case 2://エラー
                                return;
                        }
                        this.CalcKansanAndGenyouSuu(rowIndex, cellIndex, false);
                    }
                    this.SetTotal();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CellsLeave", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CellsLeave", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
        }
        #endregion

        #endregion

        #region I/F 実装(業務処理)
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #region 登録
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 必須チェック
                var errMsg = this.CheckRegist();
                if (!string.IsNullOrEmpty(errMsg))
                {
                    MessageBoxUtility.MessageBoxShowError(errMsg);
                    this.form.RegistErrorFlag = false;
                    return;
                }

                // 入力値チェック
                var inputErrMsg = this.CheckInputData();
                if (!string.IsNullOrEmpty(inputErrMsg))
                {
                    MessageBoxUtility.MessageBoxShowError(inputErrMsg);
                    this.form.RegistErrorFlag = false;
                    return;
                }

                if (errorFlag)
                {
                    return;
                }

                #region 注意喚起メッセージ
                bool existRowDataFlg = false;
                foreach (DataGridViewRow row in this.form.haikiButsuDetail.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    existRowDataFlg = true;
                }

                // CSV取込みした電マニ(最終処分終了報告完了済み)の混廃振分を行おうとしている場合
                if (existRowDataFlg &&
                    (this.isLastReportedFlg && this.isNotEdiData))
                {
                    MessageBoxUtility.MessageBoxShowWarn("マニフェスト紐付けを必ず行ってください。マニフェスト紐付けを行わない場合は、廃棄物数量等が各出力画面で正しく出力されない可能性があります。");
                }
                #endregion

                using (Transaction tran = new Transaction())
                {
                    // 現在のデータを論理削除
                    if (beforeDtR18MixList != null)
                    {
                        foreach (var beforeData in beforeDtR18MixList)
                        {
                            beforeData.DELETE_FLG = true;
                            this.dt_R18_MixDao.Update(beforeData);
                        }
                    }

                    // 新規追加
                    var registDataList = this.CreateRegsitData();
                    foreach (var registData in registDataList)
                    {
                        // いつ誰が登録したデータかを確認できればいいので
                        // 作成日も更新日もどちらも同じ値で更新してOK
                        var dataBinder = new DataBinderLogic<DT_R18_MIX>(registData);
                        dataBinder.SetSystemProperty(registData, false);
                        this.dt_R18_MixDao.Insert(registData);
                    }

                    // 紐付け情報更新
                    var maniRel = this.t_mani_relDao.GetDataByFirstSystemId(this.SystemId);
                    if (maniRel.Count > 0)
                    {
                        Dictionary<string, SqlInt32> recSeqDic = new Dictionary<string, SqlInt32>();
                        foreach (var tempRel in maniRel)
                        {
                            if (recSeqDic.ContainsKey(this.CreateMaxRecSeqKeyName(tempRel.NEXT_SYSTEM_ID.ToString(), tempRel.NEXT_HAIKI_KBN_CD.ToString())))
                            {
                                continue;
                            }

                            var maxReqSeq = this.t_mani_relDao.GetMaxReqSeqData(tempRel.NEXT_SYSTEM_ID, tempRel.NEXT_HAIKI_KBN_CD);

                            recSeqDic.Add(this.CreateMaxRecSeqKeyName(maxReqSeq.NEXT_SYSTEM_ID.ToString(), maxReqSeq.NEXT_HAIKI_KBN_CD.ToString()), maxReqSeq.REC_SEQ);
                        }

                        // 区分CD：1(中間)のDETAIL_SYSTEM_IDを抽出
                        List<SqlInt64> mixDetailSysIds = new List<SqlInt64>();
                        foreach (var mixData in registDataList)
                        {
                            if (mixData.SBN_ENDREP_KBN == 1)
                            {
                                mixDetailSysIds.Add(mixData.DETAIL_SYSTEM_ID);
                            }
                        }

                        var registManiRel = this.CreateNewManiRelation(maniRel, this.SystemId, mixDetailSysIds, recSeqDic);

                        if (registManiRel.Count > 0)
                        {
                            // 現在のマニフェスト情報を削除(対象のDT_R18_EX.SYSTEM_IDのものだけ)
                            foreach (var tempOldManiRel in maniRel)
                            {
                                tempOldManiRel.DELETE_FLG = true;
                                this.t_mani_relDao.Update(tempOldManiRel);
                            }

                            // 新しいマニフェスト情報を追加
                            foreach (var tempRegistManiRel in registManiRel)
                            {
                                var dataBinder = new DataBinderLogic<T_MANIFEST_RELATION>(tempRegistManiRel);
                                dataBinder.SetSystemProperty(tempRegistManiRel, false);
                                this.t_mani_relDao.Insert(tempRegistManiRel);
                            }
                        }
                    }

                    tran.Commit();
                }
                msgLogic.MessageBoxShow("I001", "登録");
                this.FormClose();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Regist", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Regist", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion

        #region 検索
        /// <summary>
        /// 検索
        /// </summary>
        /// <returns>searchCount:検索件数</returns>
        public int Search()
        {
            try
            {
                int searchCount = 0;

                // 検索条件セット
                var conditionData = new DT_R18_MIX();
                conditionData.KANRI_ID = this.KanriId;
                conditionData.SYSTEM_ID = this.SystemId;

                // 検索
                var searchResult = this.dt_R18_MixDao.GetDtR18MixAndRelationInfo(conditionData, this.form.ctxt_EDIMemberID.Text);

                if (searchResult != null && searchResult.Rows.Count > 0)
                {
                    searchCount = searchResult.Rows.Count;

                    // 混合種類
                    // 全て同一コードが設定されるため、1件目を無条件に設定
                    this.form.cantxt_KongoCd.Text = Convert.ToString(searchResult.Rows[0]["KONGOU_SHURUI_CD"]);
                    this.form.ctxt_KongoName.Text = Convert.ToString(searchResult.Rows[0]["KONGOU_SHURUI_NAME_RYAKU"]);

                    // DataSourceの設定では区分名が上手く設定できなかったので、直接データを設定する
                    string[] cellNames = new string[] { this.CELL_NAME_HAIKI_SHURUI_CD, this.CELL_NAME_HAIKI_SHURUI_NAME, this.CELL_NAME_SBN_HOUHOU_CD,
                    this.CELL_NAME_SBN_HOUHOU_NAME, this.CELL_NAME_WARIAI, this.CELL_NAME_HAIKI_SUU, this.CELL_NAME_HAIKI_UNIT_CD, this.CELL_NAME_HAIKI_UNIT_NAME,
                    this.CELL_NAME_KANSAN_SUU, this.CELL_NAME_GENNYOU_SUU, this.CELL_NAME_HAIKI_NAME_CD, this.CELL_NAME_HAIKI_NAME_NAME, this.CELL_NAME_SBN_ENDREP_KBN,
                    this.CELL_NAME_MANI_SHURUI, this.CELL_NAME_NEXT_MANIFEST_ID, this.CELL_NAME_LAST_SBN_END_DATE,
                    this.CELL_NAME_DETAIL_SYSTEM_ID, this.CELL_NAME_SEQ};
                    for (int i = 0; i < searchResult.Rows.Count; i++)
                    {
                        DataRow data = searchResult.Rows[i];
                        this.form.haikiButsuDetail.Rows.Add();
                        DataGridViewRow row = this.form.haikiButsuDetail.Rows[i];

                        foreach (var colName in cellNames)
                        {
                            if ((colName.Equals(this.CELL_NAME_KANSAN_SUU) && (data[colName] == null || string.IsNullOrEmpty(data[colName].ToString())))
                                || (colName.Equals(this.CELL_NAME_GENNYOU_SUU) && (data[colName] == null || string.IsNullOrEmpty(data[colName].ToString())))
                                || (colName.Equals(this.CELL_NAME_WARIAI) && (data[colName] == null || string.IsNullOrEmpty(data[colName].ToString())))
                                )
                            {
                                row.Cells[colName].Value = 0;
                            }
                            else
                            {
                                row.Cells[colName].Value = data[colName];
                            }
                        }

                        // DBには登録されていない項目を設定
                        // 区分名
                        if (data[CELL_NAME_SBN_ENDREP_KBN] != null)
                        {
                            row.Cells[this.CELL_NAME_SBN_ENDREP_KBN_NAME].Value = this.GetSbnEndRepKbnName(data[this.CELL_NAME_SBN_ENDREP_KBN].ToString());
                            // ついでに最終処分日のReadOnlyを制御
                            if (this.SBN_ENDREP_KBN_LAST.ToString().Equals(data[this.CELL_NAME_SBN_ENDREP_KBN].ToString())
                                && (data[this.CELL_NAME_MANI_SHURUI] == null || string.IsNullOrEmpty(data[this.CELL_NAME_MANI_SHURUI].ToString()))
                                )
                            {
                                row.Cells[this.CELL_NAME_LAST_SBN_END_DATE].ReadOnly = false;
                            }
                            else
                            {
                                row.Cells[this.CELL_NAME_LAST_SBN_END_DATE].ReadOnly = true;
                            }
                        }
                    }
                }
                this.SetTotal();

                // 混合種類 活性・非活性の設定
                if (this.IsLastSbnEndrepFlg && !string.IsNullOrEmpty(this.form.cantxt_KongoCd.Text))
                {
                    this.form.cantxt_KongoCd.ReadOnly = true;
                }
                else
                {
                    this.form.cantxt_KongoCd.ReadOnly = false;
                }

                return searchCount;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
                return -1;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return -1;
            }
        }
        #endregion

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// [F12]閉じる
        /// </summary>
        internal void FormClose()
        {
            BusinessBaseForm parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            parentForm.Close();
        }
        #endregion

        #region 登録データ生成
        /// <summary>
        /// 画面の情報から登録データを生成する
        /// </summary>
        /// <returns></returns>
        private List<DT_R18_MIX> CreateRegsitData()
        {
            List<DT_R18_MIX> returnVal = new List<DT_R18_MIX>();

            if (this.form.haikiButsuDetail == null)
            {
                return returnVal;
            }

            short rowNo = 1;

            // SEQのMAXを取得
            // 見栄えがよくなるように、SEQは全て揃えるようにする。
            int seq = 0;
            foreach (DataGridViewRow row in this.form.haikiButsuDetail.Rows)
            {
                int tempSeq = -1;
                if (row.Cells[this.CELL_NAME_SEQ] != null
                    && row.Cells[this.CELL_NAME_SEQ].Value != null
                    && int.TryParse(row.Cells[this.CELL_NAME_SEQ].Value.ToString(), out tempSeq)
                    && tempSeq > seq)
                {
                    seq = tempSeq;
                }
            }

            for (int i = 0; i < this.form.haikiButsuDetail.Rows.Count; i++)
            {
                var targetRow = this.form.haikiButsuDetail.Rows[i];
                if (targetRow == null || targetRow.IsNewRow)
                {
                    continue;
                }

                DT_R18_MIX registData = new DT_R18_MIX();

                // SYSTEM_ID
                registData.SYSTEM_ID = this.SystemId;

                // DETAIL_SYSTEM_ID
                long detailSysId = -1;
                if (targetRow.Cells[this.CELL_NAME_DETAIL_SYSTEM_ID] != null
                    && targetRow.Cells[this.CELL_NAME_DETAIL_SYSTEM_ID].Value != null
                    && long.TryParse(targetRow.Cells[this.CELL_NAME_DETAIL_SYSTEM_ID].Value.ToString(), out detailSysId)
                    && detailSysId > -1)
                {
                    // 更新
                    registData.DETAIL_SYSTEM_ID = detailSysId;
                }
                else
                {
                    // 新規登録
                    Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();
                    registData.DETAIL_SYSTEM_ID = dba.createSystemId((int)DENSHU_KBN.DENSHI_MANIFEST);
                }

                // SEQ
                registData.SEQ = seq + 1;

                // KANRI_ID
                registData.KANRI_ID = this.KanriId;

                // 行番号
                registData.ROW_NO = rowNo;

                // マニフェスト番号／交付
                registData.MANIFEST_ID = this.dt_R18.MANIFEST_ID;

                // 混合種類
                if (!string.IsNullOrEmpty(this.form.cantxt_KongoCd.Text))
                {
                    registData.KONGOU_SHURUI_CD = this.form.cantxt_KongoCd.Text;
                }

                // 廃棄物種類
                if (targetRow.Cells[this.CELL_NAME_HAIKI_SHURUI_CD].Value != null
                    && !string.IsNullOrEmpty(targetRow.Cells[this.CELL_NAME_HAIKI_SHURUI_CD].Value.ToString()))
                {
                    registData.HAIKI_SHURUI_CD = targetRow.Cells[this.CELL_NAME_HAIKI_SHURUI_CD].Value.ToString();
                    // HAIKI_DAI_CODE
                    registData.HAIKI_DAI_CODE = registData.HAIKI_SHURUI_CD.Substring(0, 2);
                    registData.HAIKI_CHU_CODE = registData.HAIKI_SHURUI_CD.Substring(2, 1);
                    registData.HAIKI_SHO_CODE = registData.HAIKI_SHURUI_CD.Substring(3, 1);
                    registData.HAIKI_SAI_CODE = registData.HAIKI_SHURUI_CD.Substring(4, 3);
                    registData.HAIKI_BUNRUI_NAME = this.GetHaikiBunruiName(registData.HAIKI_DAI_CODE);
                }

                // 処分方法
                if (targetRow.Cells[this.CELL_NAME_SBN_HOUHOU_CD].Value != null
                    && !string.IsNullOrEmpty(targetRow.Cells[this.CELL_NAME_SBN_HOUHOU_CD].Value.ToString()))
                {
                    registData.SBN_HOUHOU_CD = targetRow.Cells[this.CELL_NAME_SBN_HOUHOU_CD].Value.ToString();
                }

                // 割合
                decimal wariai = 0;
                if (targetRow.Cells[this.CELL_NAME_WARIAI].Value != null
                    && !string.IsNullOrEmpty(targetRow.Cells[this.CELL_NAME_WARIAI].Value.ToString())
                    && decimal.TryParse(targetRow.Cells[this.CELL_NAME_WARIAI].Value.ToString(), out wariai))
                {
                    registData.WARIAI = wariai;
                }

                // 数量
                decimal haikiSuu = 0;
                if (targetRow.Cells[this.CELL_NAME_HAIKI_SUU].Value != null
                    && !string.IsNullOrEmpty(targetRow.Cells[this.CELL_NAME_HAIKI_SUU].Value.ToString())
                    && decimal.TryParse(targetRow.Cells[this.CELL_NAME_HAIKI_SUU].Value.ToString(), out haikiSuu))
                {
                    registData.HAIKI_SUU = haikiSuu;
                }

                // 単位
                short haikiUnit = 0;
                if (targetRow.Cells[this.CELL_NAME_HAIKI_UNIT_CD].Value != null
                    && !string.IsNullOrEmpty(targetRow.Cells[this.CELL_NAME_HAIKI_UNIT_CD].Value.ToString())
                    && short.TryParse(targetRow.Cells[this.CELL_NAME_HAIKI_UNIT_CD].Value.ToString(), out haikiUnit))
                {
                    registData.HAIKI_UNIT_CD = haikiUnit;
                }

                // 換算値
                decimal kansanSuu = 0;
                if (targetRow.Cells[this.CELL_NAME_KANSAN_SUU].Value != null
                    && !string.IsNullOrEmpty(targetRow.Cells[this.CELL_NAME_KANSAN_SUU].Value.ToString())
                    && decimal.TryParse(targetRow.Cells[this.CELL_NAME_KANSAN_SUU].Value.ToString(), out kansanSuu))
                {
                    registData.KANSAN_SUU = kansanSuu;
                }

                // 減溶後数量
                decimal genyouSuu = 0;
                if (targetRow.Cells[this.CELL_NAME_GENNYOU_SUU].Value != null
                    && !string.IsNullOrEmpty(targetRow.Cells[this.CELL_NAME_GENNYOU_SUU].Value.ToString())
                    && decimal.TryParse(targetRow.Cells[this.CELL_NAME_GENNYOU_SUU].Value.ToString(), out genyouSuu))
                {
                    registData.GENNYOU_SUU = genyouSuu;
                }

                // 廃棄物名称
                if (targetRow.Cells[this.CELL_NAME_HAIKI_NAME_CD].Value != null
                    && !string.IsNullOrEmpty(targetRow.Cells[this.CELL_NAME_HAIKI_NAME_CD].Value.ToString()))
                {
                    registData.HAIKI_NAME_CD = targetRow.Cells[this.CELL_NAME_HAIKI_NAME_CD].Value.ToString();
                }

                // 区分
                short sbnEndrepKbn = 0;
                if (targetRow.Cells[this.CELL_NAME_SBN_ENDREP_KBN].Value != null
                    && !string.IsNullOrEmpty(targetRow.Cells[this.CELL_NAME_SBN_ENDREP_KBN].Value.ToString())
                    && short.TryParse(targetRow.Cells[this.CELL_NAME_SBN_ENDREP_KBN].Value.ToString(), out sbnEndrepKbn))
                {
                    registData.SBN_ENDREP_KBN = sbnEndrepKbn;
                }

                // 最終処分終了日
                if (!registData.SBN_ENDREP_KBN.IsNull
                    && registData.SBN_ENDREP_KBN == this.SBN_ENDREP_KBN_LAST)
                {
                    DateTime lastSbnEndDate = this.parentForm.sysDate;
                    // 区分：最終の場合のみ最終処分日を設定可能
                    if (targetRow.Cells[this.CELL_NAME_LAST_SBN_END_DATE].Value != null
                        && DateTime.TryParse(targetRow.Cells[this.CELL_NAME_LAST_SBN_END_DATE].Value.ToString(), out lastSbnEndDate))
                    {
                        registData.LAST_SBN_END_DATE = lastSbnEndDate.Date;
                    }
                }

                registData.DELETE_FLG = false;

                returnVal.Add(registData);

                rowNo++;

            }

            return returnVal;
        }

        /// <summary>
        /// 廃棄物の分類名を取得
        /// (G141 LogicClassからコピー)
        /// </summary>
        /// <param name="haikiDaiCode"></param>
        /// <returns></returns>
        private string GetHaikiBunruiName(string haikiDaiCode)
        {
            string returnVal = null;
            if (string.IsNullOrEmpty(haikiDaiCode))
            {
                return returnVal;
            }

            M_DENSHI_HAIKI_SHURUI conditionData = new M_DENSHI_HAIKI_SHURUI();
            conditionData.HAIKI_SHURUI_CD = haikiDaiCode + "00";

            var im_denshi_haiki_shuruidao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUIDao>();
            var denshiHaikiDaibunruiList = im_denshi_haiki_shuruidao.GetAllValidData(conditionData);
            if (denshiHaikiDaibunruiList != null
                && denshiHaikiDaibunruiList.Count() == 1)
            {
                returnVal = denshiHaikiDaibunruiList[0].HAIKI_SHURUI_NAME;
            }

            return returnVal;
        }

        /// <summary>
        /// 新たに紐付け情報を生成する
        /// </summary>
        /// <param name="oldManiRels">現在の紐付け情報</param>
        /// <param name="exSystemId">混合振分画面を利用しているDT_R18_EX.SYSTEM_ID</param>
        /// <param name="mixDetailSystemIds">DT_R18_MIX.DETAIL_SYSTEM_ID(区分CD：1(中間)であること)</param>
        /// <param name="maxReqSeq">現在のT_MANIFEST_RELATION.REQ_SEQのMAX</param>
        /// <returns></returns>
        private List<T_MANIFEST_RELATION> CreateNewManiRelation(List<T_MANIFEST_RELATION> oldManiRels
            , long exSystemId, List<SqlInt64> mixDetailSystemIds, Dictionary<string, SqlInt32> recSeqDic)
        {
            List<T_MANIFEST_RELATION> returnVal = new List<T_MANIFEST_RELATION>();

            if (oldManiRels == null || oldManiRels.Count < 1)
            {
                return returnVal;
            }

            SqlInt32 recCount = 0;
            long tempTargetNextSysId = 0;
            short tempTargetNextKbnCd = 0;
            foreach(var oldRel in oldManiRels)
            {
                T_MANIFEST_RELATION tempNewRel = new T_MANIFEST_RELATION();
                if (!(tempTargetNextSysId == oldRel.NEXT_SYSTEM_ID
                    && tempTargetNextKbnCd == oldRel.NEXT_HAIKI_KBN_CD))
                {
                    recCount = recSeqDic[this.CreateMaxRecSeqKeyName(oldRel.NEXT_SYSTEM_ID.ToString(), oldRel.NEXT_HAIKI_KBN_CD.ToString())];
                    recCount += 1;
                }

                if (oldRel.FIRST_HAIKI_KBN_CD == 4 && oldRel.FIRST_SYSTEM_ID == exSystemId)
                {
                    // DT_R18_MIXが作成された分、紐付け情報を細分化
                    // 二次マニは一件しか紐づいていない前提。(この前提が崩れる場合は仕様の見直しが必要)
                    foreach (var mixDetailSysId in mixDetailSystemIds)
                    {
                        tempNewRel = new T_MANIFEST_RELATION();

                        tempNewRel.NEXT_SYSTEM_ID = oldRel.NEXT_SYSTEM_ID;
                        tempNewRel.SEQ = oldRel.SEQ;
                        tempNewRel.REC_SEQ = recCount;
                        tempNewRel.NEXT_HAIKI_KBN_CD = oldRel.NEXT_HAIKI_KBN_CD;
                        tempNewRel.FIRST_SYSTEM_ID = mixDetailSysId;
                        tempNewRel.FIRST_HAIKI_KBN_CD = oldRel.FIRST_HAIKI_KBN_CD;
                        tempNewRel.DELETE_FLG = SqlBoolean.False;

                        returnVal.Add(tempNewRel);
                        recCount += 1;
                    }
                }
            }

            return returnVal;
        }
        #endregion

        #region 必須チェック
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <param name="errorMsg">エラーメッセージ(正常の場合はブランク)</param>
        /// <returns></returns>
        internal string CheckRegist()
        {
            string returnVal = string.Empty;

            if (this.form.haikiButsuDetail.Rows.Count < 1)
            {
                return returnVal;
            }

            bool errFlgHaikiSuhurui = false;
            bool errFlgSbnHouhou = false;
            bool errFlgHaikiSuu = false;
            bool errFlgHaikiUnit = false;
            bool errFlgSbnEndrepKbn = false;
            bool errFlgLastSbnEndDate = false;

            foreach (DataGridViewRow row in this.form.haikiButsuDetail.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                // 廃棄物種類CD
                if (string.IsNullOrEmpty(row.Cells[this.CELL_NAME_HAIKI_SHURUI_CD].FormattedValue.ToString()))
                {
                    errFlgHaikiSuhurui = true;
                    ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[this.CELL_NAME_HAIKI_SHURUI_CD], true);
                }

                // 処分方法CD
                if (string.IsNullOrEmpty(row.Cells[this.CELL_NAME_SBN_HOUHOU_CD].FormattedValue.ToString()))
                {
                    errFlgSbnHouhou = true;
                    ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[this.CELL_NAME_SBN_HOUHOU_CD], true);
                }

                // 数量
                if (string.IsNullOrEmpty(row.Cells[this.CELL_NAME_HAIKI_SUU].FormattedValue.ToString()))
                {
                    errFlgHaikiSuu = true;
                    ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[this.CELL_NAME_HAIKI_SUU], true);
                }

                // 単位CD
                if (string.IsNullOrEmpty(row.Cells[this.CELL_NAME_HAIKI_UNIT_CD].FormattedValue.ToString()))
                {
                    errFlgHaikiUnit = true;
                    ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[this.CELL_NAME_HAIKI_UNIT_CD], true);
                }

                // 区分CD
                if (string.IsNullOrEmpty(row.Cells[this.CELL_NAME_SBN_ENDREP_KBN].FormattedValue.ToString()))
                {
                    errFlgSbnEndrepKbn = true;
                    ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[this.CELL_NAME_SBN_ENDREP_KBN], true);
                }

                // 最終処分日
                if (row.Cells[this.CELL_NAME_SBN_ENDREP_KBN].FormattedValue.ToString().Equals(SBN_ENDREP_KBN_LAST.ToString())
                    && string.IsNullOrEmpty(row.Cells[this.CELL_NAME_LAST_SBN_END_DATE].FormattedValue.ToString()))
                {
                    errFlgLastSbnEndDate = true;
                    ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[this.CELL_NAME_LAST_SBN_END_DATE], true);
                }
            }

            if (errFlgHaikiSuhurui)
            {
                returnVal += string.Format(Shougun.Core.Message.MessageUtility.GetMessage("E001").Text, "廃棄物種類CD");
            }
            if (errFlgSbnHouhou)
            {
                returnVal = string.IsNullOrEmpty(returnVal) ? returnVal : returnVal + System.Environment.NewLine;
                returnVal += string.Format(Shougun.Core.Message.MessageUtility.GetMessage("E001").Text, "処分方法CD");
            }
            if (errFlgHaikiSuu)
            {
                returnVal = string.IsNullOrEmpty(returnVal) ? returnVal : returnVal + System.Environment.NewLine;
                returnVal += string.Format(Shougun.Core.Message.MessageUtility.GetMessage("E001").Text, "数量");
            }
            if (errFlgHaikiUnit)
            {
                returnVal = string.IsNullOrEmpty(returnVal) ? returnVal : returnVal + System.Environment.NewLine;
                returnVal += string.Format(Shougun.Core.Message.MessageUtility.GetMessage("E001").Text, "単位CD");
            }
            if (errFlgSbnEndrepKbn)
            {
                returnVal = string.IsNullOrEmpty(returnVal) ? returnVal : returnVal + System.Environment.NewLine;
                returnVal += string.Format(Shougun.Core.Message.MessageUtility.GetMessage("E001").Text, "区分CD");
            }
            if (errFlgLastSbnEndDate)
            {
                returnVal = string.IsNullOrEmpty(returnVal) ? returnVal : returnVal + System.Environment.NewLine;
                returnVal += string.Format(Shougun.Core.Message.MessageUtility.GetMessage("E001").Text, "最終処分日");
            }

            return returnVal;
        }
        #endregion

        #region 入力値チェック(登録時)
        /// <summary>
        /// 入力値チェック(登録時)
        /// </summary>
        /// <returns>エラーメッセージ。値有：エラーあり、値無：エラーなし</returns>
        private string CheckInputData()
        {
            string returnStr = string.Empty;

            DateTime hikiwatashiDate = this.parentForm.sysDate;
            if (this.dt_R18 != null && !string.IsNullOrEmpty(this.dt_R18.HIKIWATASHI_DATE))
            {
                hikiwatashiDate = DateTime.ParseExact(this.dt_R18.HIKIWATASHI_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }
            DateTime tempLastSbnEndDate = this.parentForm.sysDate;
            bool isContainsMiddel = false;
            bool isLastSbnEndDateErr = false;
            bool isExistsLastSbn = false;
            foreach (DataGridViewRow row in this.form.haikiButsuDetail.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                // 区分：中間が一件でもあるかチェック(全て最終だとNG)
                // 区分CDの必須チェックは既に完了しているはず
                if (this.SBN_ENDREP_KBN_MIDDLE.ToString().Equals(row.Cells[this.CELL_NAME_SBN_ENDREP_KBN].FormattedValue.ToString()))
                {
                    isContainsMiddel = true;
                }
                else if (this.SBN_ENDREP_KBN_LAST.ToString().Equals(row.Cells[this.CELL_NAME_SBN_ENDREP_KBN].FormattedValue.ToString()))
                {
                    if (this.dt_R18 == null || string.IsNullOrEmpty(this.dt_R18.HIKIWATASHI_DATE))
                    {
                        continue;
                    }

                    isExistsLastSbn = true;

                    // 最終処分終了日には引渡日よりも前の日付を指定してはいけない
                    if (row.Cells[this.CELL_NAME_LAST_SBN_END_DATE].Value != null
                        && DateTime.TryParse(row.Cells[this.CELL_NAME_LAST_SBN_END_DATE].Value.ToString(), out tempLastSbnEndDate))
                    {
                        if (DateTime.Compare(tempLastSbnEndDate.Date, hikiwatashiDate.Date) < 0)
                        {
                            isLastSbnEndDateErr = true;
                            ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[this.CELL_NAME_LAST_SBN_END_DATE], true);
                        }
                    }
                }

            }

            var isOnlyNewRow = (this.form.haikiButsuDetail.Rows.Count == 1 && this.form.haikiButsuDetail.Rows[0].IsNewRow);

            // メッセージ作成
            if (!isContainsMiddel
                && this.form.haikiButsuDetail.Rows.Count > 0
                && !isOnlyNewRow
                && this.dt_R18.SBN_ENDREP_FLAG == 1
                && this.dt_R18.SBN_ENDREP_KBN == 1)
            {
                returnStr = string.Format(Shougun.Core.Message.MessageUtility.GetMessage("E034").Text, "必ず一行以上、区分CDに中間");
            }
            else if (isLastSbnEndDateErr)
            {
                returnStr = string.Format(Shougun.Core.Message.MessageUtility.GetMessage("E034").Text, string.Format("最終処分日には引渡日({0})以降の日付を", hikiwatashiDate.Date.ToShortDateString()));
            }
            else if (this.form.ctxt_TotalWariai.Text != string.Empty &&
                        (Convert.ToDecimal(this.form.ctxt_TotalWariai.Text) != 0 &&
                        Convert.ToDecimal(this.form.ctxt_TotalWariai.Text) != 100))
            {
                returnStr = string.Format(Shougun.Core.Message.MessageUtility.GetMessage("E040").Text);
            }
            else if (isExistsLastSbn && this.isLastReportedFlg && this.isExsRelationData)
            {
                returnStr = this.selectLastSbnKbnNGMessage;
            }

            return returnStr;
        }
        #endregion

        #region 区分の表示文字列取得
        /// <summary>
        ///  区分の表示文字列取得
        /// </summary>
        /// <param name="sbnEndrepKbn"></param>
        /// <returns></returns>
        private string GetSbnEndRepKbnName(string sbnEndrepKbn)
        {
            string returnVal = string.Empty;

            if ("1".Equals(sbnEndrepKbn))
            {
                returnVal = "中間";
            }
            else if ("2".Equals(sbnEndrepKbn))
            {
                returnVal = "最終";
            }

            return returnVal;
        }
        #endregion

        #region 換算後数量と減溶後数量を計算
        /// <summary>
        /// 換算後数量と減溶後数量の計算をする
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellIndex"></param>
        /// <param name="reCalc">true:明細合計を再計算する, false:明細合計を再計算しない</param>
        private void CalcKansanAndGenyouSuu(int rowIndex, int cellIndex, bool reCalc = true)
        {
            this.CalcKansanSuu(rowIndex, cellIndex);
            this.CalcGenyouSuu(rowIndex, cellIndex);

            if (reCalc)
            {
                this.SetTotal();
            }
        }
        #endregion

        #region 換算後数量計算
        /// <summary>
        /// 換算後数量計算
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellIndex"></param>
        private void CalcKansanSuu(int rowIndex, int cellIndex)
        {
            if (rowIndex < 0 || cellIndex < 0)
            {
                return;
            }

            var cell = this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex];
            if (cell == null)
            {
                return;
            }

            string columnName = this.form.haikiButsuDetail.Columns[cellIndex].Name;
            if (!this.CELL_NAME_HAIKI_SHURUI_CD.Equals(columnName)
                && !this.CELL_NAME_HAIKI_NAME_CD.Equals(columnName)
                && !this.CELL_NAME_HAIKI_SUU.Equals(columnName)
                && !this.CELL_NAME_HAIKI_UNIT_CD.Equals(columnName))
            {
                return;
            }

            //換算後数量の計算を行う
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
            //廃棄物種類CDが画面から取得する
            object tmpObj = this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_SHURUI_CD].Value;
            if (tmpObj != null)
            {
                //前頭4桁が廃棄物種類CD取得
                dto.HAIKI_SHURUI_CD = tmpObj.ToString().Substring(0, 4);
            }
            else
            {
                dto.HAIKI_SHURUI_CD = string.Empty;
            }

            //画面から取得の廃棄物名称CD
            tmpObj = this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_NAME_CD].Value;
            if (tmpObj != null)
            {
                dto.HAIKI_NAME_CD = tmpObj.ToString();
            }
            else
            {
                dto.HAIKI_NAME_CD = string.Empty;
            }

            //確定数量の単位CD
            tmpObj = this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_UNIT_CD].Value;
            if (tmpObj != null)
            {
                dto.UNIT_CD = tmpObj.ToString();
            }
            else
            {
                dto.UNIT_CD = string.Empty;
            }

            //荷姿CD
            dto.NISUGATA_CD = this.dt_R18.NISUGATA_CODE;

            //換算式と換算値取得
            DataTable tbl = new DenshiMasterDataLogic().GetDenmaniKansanData(dto);
            decimal haikiSuu = 0;
            if (this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_SUU].Value != null
                && decimal.TryParse(this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_SUU].Value.ToString(), out haikiSuu))
            {
                if (tbl.Rows.Count == 1)
                {   //換算式の取得
                    if (tbl.Rows[0]["KANSANCHI"] != null)
                    {
                        SqlDecimal Kansan_suu = 0;

                        string val = tbl.Rows[0]["KANSANCHI"].ToString();
                        //乗算式
                        if (tbl.Rows[0]["KANSANSHIKI"].ToString() == "0")
                        {
                            Kansan_suu = SqlDecimal.Multiply(haikiSuu, SqlDecimal.Parse(val));
                        }
                        //除算式
                        else
                        {
                            Kansan_suu = SqlDecimal.Divide(haikiSuu, SqlDecimal.Parse(val));
                        }
                        //取得成功フラグを設定
                        var maniLogic = new ManifestoLogic();
                        this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_KANSAN_SUU].Value = maniLogic.Round((decimal)Kansan_suu, SystemProperty.Format.ManifestSuuryou);
                    }
                }
            }
        }
        #endregion

        #region 減溶後数量計算
        /// <summary>
        /// 減溶後数量計算
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellIndex"></param>
        private void CalcGenyouSuu(int rowIndex, int cellIndex)
        {
            if (rowIndex < 0 || cellIndex < 0)
            {
                return;
            }

            var cell = this.form.haikiButsuDetail.Rows[rowIndex].Cells[cellIndex];
            if (cell == null)
            {
                return;
            }

            string columnName = this.form.haikiButsuDetail.Columns[cellIndex].Name;
            if (!this.CELL_NAME_HAIKI_SHURUI_CD.Equals(columnName)
                && !this.CELL_NAME_HAIKI_NAME_CD.Equals(columnName)
                && !this.CELL_NAME_HAIKI_SUU.Equals(columnName)
                && !this.CELL_NAME_HAIKI_UNIT_CD.Equals(columnName)
                && !this.CELL_NAME_KANSAN_SUU.Equals(columnName)
                && !this.CELL_NAME_SBN_HOUHOU_CD.Equals(columnName))
            {
                return;
            }

            decimal kansan_suu = 0;
            if (this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_KANSAN_SUU].Value == null
                || string.IsNullOrEmpty(this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_KANSAN_SUU].Value.ToString())
                || !decimal.TryParse(this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_KANSAN_SUU].Value.ToString(), out kansan_suu)
                )
            {
                this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_GENNYOU_SUU].Value = null;
                return;
            }

            decimal genyou_suu = kansan_suu;
            if (kansan_suu != 0)
            {
                //減容率の取得
                SqlDecimal GENNYOURITSU = 0;
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                //廃棄物種類CDが画面から取得する
                object tmpObj = this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_SHURUI_CD].Value;
                if (tmpObj != null)
                {
                    //前頭4桁が廃棄物種類CD取得
                    dto.HAIKI_SHURUI_CD = tmpObj.ToString().Substring(0, 4);
                }
                else
                {
                    return;
                }
                //画面から取得の廃棄物名称CD
                tmpObj = this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_HAIKI_NAME_CD].Value;
                if (tmpObj != null)
                {
                    dto.HAIKI_NAME_CD = tmpObj.ToString();
                }
                //画面から処分方法CD取得する
                if (!string.IsNullOrEmpty(this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_SBN_HOUHOU_CD].FormattedValue.ToString()))
                {
                    dto.SHOBUN_HOUHOU_CD = this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_SBN_HOUHOU_CD].FormattedValue.ToString();
                }

                var dao = DaoInitUtility.GetComponent<DenshiMasterDataSearchDao>();
                DataTable tbl = new DataTable();

                // 報告書分類＋廃棄物名称＋処分方法＋減容率 で検索
                if (!string.IsNullOrEmpty(dto.HAIKI_SHURUI_CD)
                    && !string.IsNullOrEmpty(dto.HAIKI_NAME_CD)
                    && !string.IsNullOrEmpty(dto.SHOBUN_HOUHOU_CD))
                {
                    tbl = dao.GetGenyouritsu(dto);
                }

                if (tbl.Rows.Count < 1)
                {
                    // 報告書分類＋処分方法＋減容率
                    if (!string.IsNullOrEmpty(dto.HAIKI_SHURUI_CD)
                        && !string.IsNullOrEmpty(dto.SHOBUN_HOUHOU_CD))
                    {
                        dto.HAIKI_NAME_CD = string.Empty;
                        tbl = dao.GetGenyouritsu(dto);
                    }
                }

                if (tbl.Rows.Count < 1)
                {
                    // 報告書分類＋減容率
                    if (!string.IsNullOrEmpty(dto.HAIKI_SHURUI_CD))
                    {
                        dto.HAIKI_NAME_CD = string.Empty;
                        dto.SHOBUN_HOUHOU_CD = string.Empty;
                        tbl = dao.GetGenyouritsu(dto);
                    }
                }

                if (tbl.Rows.Count == 1)
                {   //減容率の取得
                    if (tbl.Rows[0]["GENNYOURITSU"] != null)
                    {
                        try
                        {
                            GENNYOURITSU = SqlDecimal.Parse(tbl.Rows[0]["GENNYOURITSU"].ToString());
                            genyou_suu = (decimal)SqlDecimal.Divide(SqlDecimal.Multiply((decimal)kansan_suu, 100 - GENNYOURITSU), 100.00m);
                        }
                        catch
                        { }
                    }
                }
            }

            var maniLogic = new ManifestoLogic();
            this.form.haikiButsuDetail.Rows[rowIndex].Cells[this.CELL_NAME_GENNYOU_SUU].Value = maniLogic.Round((decimal)genyou_suu, SystemProperty.Format.ManifestSuuryou);
        }
        #endregion

        #region 入力制限
        /// <summary>
        /// 入力制限(OnLoad時にのみ呼び出し)
        /// </summary>
        internal bool ChangeReadOnlyForOnLoad()
        {
            try
            {
                string[] inputPossibleControl = new string[] { this.CELL_NAME_KANSAN_SUU };

                this.isNotEdiData = (bool)(this.dt_mf_toc != null && !this.dt_mf_toc.KIND.IsNull && this.dt_mf_toc.KIND == 5);

                // 最終処分終了報告がされている場合は、マニ紐付けされてるされてないに関わらず、換算値のみ変更可能
                // ただし「未振分＋最終処分済み＋未紐」の場合、振分登録を可能とする。
                // 行追加、削除でもチェックするため、OnLoad時に一度だけ読み込んでIsLastReportedFlgを使いまわす。
                // ただし既にDT_R18_EXで紐付け登録している場合はその限りではない。
                this.isLastReportedFlg = this.IsLastReported();
                this.runningOperationFlg = this.ExistLastReport();
                this.isExsRelationData = this.IsExsRelationData();

                if (this.beforeDtR18MixList.Length == 0 && this.isLastReportedFlg && !this.isExsRelationData)
                {
                    return true;
                }

                if ((this.isLastReportedFlg && !this.isNotEdiData && !this.isExsRelationData)
                    || this.runningOperationFlg)
                {
                    foreach (DataGridViewRow row in this.form.haikiButsuDetail.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            var colName = this.form.haikiButsuDetail.Columns[cell.ColumnIndex].Name;
                            if (!inputPossibleControl.Contains(colName))
                            {
                                cell.ReadOnly = true;
                            }
                        }
                    }

                    return true;
                }

                foreach (DataGridViewRow row in this.form.haikiButsuDetail.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    // 紐付けがされている場合は換算値のみ変更可能
                    if (row.Cells[CELL_NAME_MANI_SHURUI].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_MANI_SHURUI].Value.ToString()))
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            var colName = this.form.haikiButsuDetail.Columns[cell.ColumnIndex].Name;

                            if (!inputPossibleControl.Contains(colName))
                            {
                                cell.ReadOnly = true;
                            }
                        }
                    }
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChangeReadOnlyForOnLoad", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeReadOnlyForOnLoad", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
        }
        #endregion

        /// <summary>最終処分終了報告済みメッセージ(本クラス内でしか使用しないため、MessageData.xmlには定義しない)</summary>
        private string alreadyLastReportMessage = "既に最終処分終了報告がされています。明細行を{0}する場合は、最終処分終了取消を行ってください。";
        private string runningOperetionMessage = "JWNETへ送信中、または修正中のため、{0}は実行できません。";
        private string deleteRowMessage = "最終処分終了報告が完了しているため、行削除を行うことはできません。最終処分終了報告の取消をおこなってください。";
        private string runningOperetionKMessage = "JWNETへ送信中、または修正中のため、\n新規または、明細の挿入/削除を行うことはできません。";
        private string selectLastSbnKbnNGMessage = "全明細の区分が中間となるように設定してください。";
        private string himodukezumiManiMessage = "紐付済みのマニフェストのため、振分登録できません。\n紐付を解除してから再度振分登録を行ってください。";

        #region 行追加可能かチェック
        /// <summary>
        /// 行追加可能かチェック
        /// メッセージは本メソッド内で表示する
        /// </summary>
        /// <returns>true:可、false:不可</returns>
        internal bool IsPossibleAddNewRow(out bool catchErr)
        {
            catchErr = false;
            try
            {
                bool returnVal = true;

                // 最終処分終了報告済みかどうか
                if (this.isLastReportedFlg && !this.isNotEdiData)
                {
                    returnVal = false;
                    MessageBoxUtility.MessageBoxShowError(string.Format(alreadyLastReportMessage, "追加"));
                    return returnVal;
                }
                else if (this.runningOperationFlg)
                {
                    returnVal = false;
                    MessageBoxUtility.MessageBoxShowError(string.Format(runningOperetionMessage, "行挿入"));
                    return returnVal;
                }

                return returnVal;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("IsPossibleAddNewRow", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsPossibleAddNewRow", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }
        #endregion

        #region 行削除可能かチェック
        /// <summary>
        /// 行削除可能かチェック
        /// メッセージは本メソッド内で表示する
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns>true:可、false:不可</returns>
        internal bool IsPossibleDeleteRow(int rowIndex)
        {
            bool returnVal = true;

            if (rowIndex < 0)
            {
                return returnVal;
            }

            if (this.form.haikiButsuDetail.CurrentRow.Cells["SBN_ENDREP_KBN"].Value != null && !string.IsNullOrEmpty(this.form.haikiButsuDetail.CurrentRow.Cells["SBN_ENDREP_KBN"].Value.ToString()))
            {
                if (this.form.haikiButsuDetail.CurrentRow.Cells["SBN_ENDREP_KBN"].Value.ToString().Equals("1")
                    && this.form.haikiButsuDetail.CurrentRow.Cells["NEXT_MANIFEST_ID"].Value != null
                    && !string.IsNullOrEmpty(this.form.haikiButsuDetail.CurrentRow.Cells["NEXT_MANIFEST_ID"].Value.ToString()))
                {
                    returnVal = false;
                    MessageBoxUtility.MessageBoxShowError(deleteRowMessage);
                    return returnVal;
                }
                else if (this.form.haikiButsuDetail.CurrentRow.Cells["SBN_ENDREP_KBN"].Value.ToString().Equals("2")
                    && this.form.haikiButsuDetail.CurrentRow.Cells["LAST_SBN_END_DATE"].Value != null
                    && !string.IsNullOrEmpty(this.form.haikiButsuDetail.CurrentRow.Cells["LAST_SBN_END_DATE"].Value.ToString())
                    && this.form.haikiButsuDetail.CurrentRow.Cells["LAST_SBN_END_DATE"].ReadOnly == true)
                {
                    returnVal = false;
                    MessageBoxUtility.MessageBoxShowError(deleteRowMessage);
                    return returnVal;
                }
            }
            // 最終処分終了報告済みかどうか
            //if (this.isLastReportedFlg && !this.isNotEdiData)
            //{
            //    returnVal = false;
            //    MessageBoxUtility.MessageBoxShowError(string.Format(alreadyLastReportMessage, "削除"));
            //    return returnVal;
            //}
            //else if (this.runningOperationFlg)
            //{
            //    returnVal = false;
            //    MessageBoxUtility.MessageBoxShowError(string.Format(runningOperetionMessage, "行削除"));
            //    return returnVal;
            //}

            // 紐付け済みかどうか
            DataGridViewRow row = this.form.haikiButsuDetail.Rows[rowIndex];
            if (row != null
                && row.Cells[this.CELL_NAME_MANI_SHURUI].Value != null
                && !string.IsNullOrEmpty(row.Cells[this.CELL_NAME_MANI_SHURUI].Value.ToString()))
            {
                returnVal = false;
                MessageBoxUtility.MessageBoxShowError("既にマニフェスト紐付けがされています。明細行を削除する場合は、紐付けを解除してください。");
                return returnVal;
            }

            return returnVal;
        }
        #endregion

        #region 最終処分終了報告済み(保留含む)チェック
        /// <summary>
        /// 最終処分終了報告済み(保留含む)かどうか
        /// </summary>
        /// <returns>true:報告済み、false:未報告</returns>
        private bool IsLastReported()
        {
            bool returnVal = false;

            if (this.dt_R18.LAST_SBN_ENDREP_FLAG == 1
                || this.ExistLastSbnSuspend())
            {
                returnVal = true;
            }

            return returnVal;
        }
        #endregion

        #region 最終処分終了報告の保留チェック
        /// <summary>
        /// 最終処分終了報告の保留データがあるかどうか
        /// </summary>
        /// <returns>true:有、false:無</returns>
        private bool ExistLastSbnSuspend()
        {
            bool returnVal = false;

            long count = this.dt_R18_MixDao.GetLastSbnSuspend(this.dt_R18.KANRI_ID);

            if (count > 0)
            {
                returnVal = true;
            }

            return returnVal;
        }
        #endregion

        #region 最終処分終了報告、取消のキュー & 実行ステータスチェック
        /// <summary>
        /// 最終処分終了報告、取消のキュー、実行ステータスチェック
        /// </summary>
        /// <returns>true:実行中、false:待機状態</returns>
        private bool ExistLastReport()
        {
            bool returnVal = false;

            var maniLogic = new ManifestoLogic();
            var dummyList = new List<string>();
            var dt = new DataTable();

            dt.Columns.Add("KANRI_ID");
            dt.Columns.Add("LATEST_SEQ");
            dt.Columns.Add("STATUS_FLAG");
            dt.Columns.Add("STATUS_DETAIL");
            dt.Columns.Add("APPROVAL_SEQ");

            var row = dt.NewRow();
            row["KANRI_ID"] = this.dt_mf_toc.KANRI_ID;
            row["LATEST_SEQ"] = this.dt_mf_toc.LATEST_SEQ;
            row["STATUS_FLAG"] = this.dt_mf_toc.STATUS_FLAG;
            row["STATUS_DETAIL"] = this.dt_mf_toc.STATUS_DETAIL;
            row["APPROVAL_SEQ"] = this.dt_mf_toc.APPROVAL_SEQ.IsNull ? string.Empty : this.dt_mf_toc.APPROVAL_SEQ.ToString();
            dt.Rows.Add(row);

            // 最終処分終了報告、取消と同様のチェックロジックを使用する
            if (!maniLogic.ChkLastSbnEndrepReport(dt, true, out dummyList)
                || !maniLogic.ChkLastSbnEndrepReport(dt, false, out dummyList))
            {
                returnVal = true;
            }

            return returnVal;
        }
        #endregion

        #region 承認待ちチェック
        /// <summary>
        /// 承認待ちかどうか
        /// </summary>
        /// <returns>true:承認待ち、false:待ちなし</returns>
        internal bool IsApplying()
        {
            bool returnVal = false;

            if ( this.runningOperationFlg)
            {
                returnVal = true;
                MessageBoxUtility.MessageBoxShowInformation(string.Format(runningOperetionKMessage));

                // JWNET送信中の場合、追加・修正登録できないように制限する
                this.form.cantxt_KongoCd.ReadOnly = true;
                BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;
                parentform.bt_func9.Click -= new EventHandler(this.form.Regist);
                parentform.bt_func9.Enabled = false;
                parentform.bt_func10.Click -= new EventHandler(this.form.AddRow);
                parentform.bt_func10.Enabled = false;
                parentform.bt_func11.Click -= new EventHandler(this.form.DeleteRow);
                parentform.bt_func11.Enabled = false;
                this.form.haikiButsuDetail.AllowUserToAddRows = false;

                return returnVal;
            }
            else if (this.isRelationalMixMani && this.IsLastSbnEndrepFlg)
            {
                MessageBoxUtility.MessageBoxShowInformation(string.Format(himodukezumiManiMessage));

                // 振分済み、紐づけ済み、最終処分済の場合、追加・修正登録できないように制限する
                this.form.cantxt_KongoCd.ReadOnly = true;
                BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;
                parentform.bt_func9.Click -= new EventHandler(this.form.Regist);
                parentform.bt_func9.Enabled = false;
                parentform.bt_func10.Click -= new EventHandler(this.form.AddRow);
                parentform.bt_func10.Enabled = false;
                parentform.bt_func11.Click -= new EventHandler(this.form.DeleteRow);
                parentform.bt_func11.Enabled = false;
                this.form.haikiButsuDetail.AllowUserToAddRows = false;
            }

            return returnVal;
        }
        #endregion

        #region DT_R18_EXと紐付け済みかチェック
        /// <summary>
        /// DT_R18_EXと紐付け済みかどうか
        /// </summary>
        /// <returns>true:DT_R18_EXと紐付け済み、false:それ以外(DT_R18_MIXとは紐づいている可能性があるので注意)</returns>
        internal bool IsExsRelationData()
        {
            bool returnVal = false;

            var tempData = this.t_mani_relDao.GetDataByFirstSystemId(this.SystemId);
            returnVal = (tempData == null || tempData.Count < 1) ? false : true;

            return returnVal;
        }
        #endregion

        /// <summary>検索結果</summary>
        public DataTable SearchResult { get; set; }
        /// <summary>
        ///  混合種別名称設定処理
        /// </summary>
        public bool SetKongouName()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                DataTable dt = null;

                if (this.form.cantxt_KongoCd.Text == string.Empty)
                {
                    this.form.ctxt_KongoName.Text = string.Empty;

                    return ret;
                }

                // 初期処理
                dt = GetKongouName(this.form.cantxt_KongoCd.Text);
                if (dt.Rows.Count > 0)
                {
                    this.form.ctxt_KongoName.Text = dt.Rows[0].ItemArray[0].ToString();
                    this.form.KongoErr = false;
                }
                else
                {
                    MessageBoxUtility.MessageBoxShow("E020", "混合種類");

                    this.form.KongoErr = true;
                    this.form.cantxt_KongoCd.Focus();
                    this.form.cantxt_KongoCd.SelectAll();
                    return ret;
                }

                if (this.form.haikiButsuDetail.Rows.Count > 1)
                {
                    DialogResult result =
                        MessageBoxUtility.MessageBoxShow("C027");
                    if (result == DialogResult.No)
                    {
                        this.form.cantxt_KongoCd.Text = string.Empty;
                        this.form.ctxt_KongoName.Text = string.Empty;
                        return ret;
                    }
                }

                var SearchString = new GetKongouNameDtoCls();
                SearchString.KONGOU_SHURUI_CD = this.form.cantxt_KongoCd.Text;

                DataTable kongoudt = GetKongouHaikibutu(SearchString);

                this.form.haikiButsuDetail.Rows.Clear();

                this.form.haikiButsuDetail.RowCount = kongoudt.Rows.Count + 1;
                for (int i = 0; i < kongoudt.Rows.Count; i++)
                {
                    this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_HAIKI_SHURUI_CD].Value = kongoudt.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_WARIAI].Value = kongoudt.Rows[i]["HAIKI_HIRITSU"].ToString();
                    this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_HAIKI_SHURUI_NAME].Value = kongoudt.Rows[i]["HAIKI_SHURUI_NAME"].ToString();
                }
                this.form.haikiButsuDetail.RefreshEdit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKongouName", ex);
                if (ex is SQLRuntimeException)
                {
                    MessageBoxUtility.MessageBoxShow("E093", "");
                }
                else
                {
                    MessageBoxUtility.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 混合種別検索
        /// </summary>
        public virtual DataTable GetKongouName(string KongouName)
        {
            LogUtility.DebugMethodStart(KongouName);

            this.SearchResult = new DataTable();
            GetKongouNameDtoCls SearchString = new GetKongouNameDtoCls();
            SearchString.KONGOU_SHURUI_CD = KongouName;
            this.SearchResult = GetKongouNamedao.GetDataForEntity(SearchString);

            LogUtility.DebugMethodEnd(KongouName);

            return this.SearchResult;
        }

        /// <summary>
        /// 廃棄物種類マスタ検索
        /// </summary>
        public virtual DataTable GetKongouHaikibutu(GetKongouNameDtoCls SearchCondition)
        {
            return this.GetKongouHaikibutudao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// 数値セット
        /// </summary>
        public bool SetSuu()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                for (int i = 0; i < this.form.haikiButsuDetail.RowCount - 1; i++)
                {
                    //数量
                    switch (this.ChkGridWariai(i))
                    {
                        case 0://正常
                            break;

                        case 1://空
                            return ret;

                        case 2://エラー
                            return ret;
                    }
                    //換算後数量・減容後数量
                    this.CalcKansanAndGenyouSuu(i, 0, false);

                }
                //合計
                if (!this.SetTotal()) { return ret; }
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSuu", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 割合(％)のチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public int ChkGridWariai(int iRow)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart();

                //総量100%チェック
                if (string.IsNullOrEmpty(Convert.ToString(this.form.haikiButsuDetail.Rows[iRow].Cells[CELL_NAME_WARIAI].Value)))
                {
                    ret = 1;
                    return ret;
                }

                decimal fsuu = 0;
                fsuu = Convert.ToDecimal(this.form.haikiButsuDetail.Rows[iRow].Cells[CELL_NAME_WARIAI].Value);

                decimal fcnt = 0;
                for (int i = 0; i < this.form.haikiButsuDetail.RowCount; i++)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_WARIAI].Value)))
                    {
                        fcnt += Convert.ToDecimal(this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_WARIAI].Value);
                    }
                }

                if (fcnt > 100 || fsuu > 100)
                {
                    ret = 2;
                    MessageBoxUtility.MessageBoxShow("E039");
                    return ret;
                }

                decimal JissekiSuryo = 0;
                if (this.form.cntxt_JissekiSuryo.Text != string.Empty)
                {
                    JissekiSuryo = Convert.ToDecimal(this.form.cntxt_JissekiSuryo.Text);
                }

                this.form.haikiButsuDetail.Rows[iRow].Cells[CELL_NAME_HAIKI_SUU].Value
                    = this.mlogic.GetSuuryoRound((JissekiSuryo * fsuu / 100), this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridWariai", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 明細合計算出処理
        /// </summary>
        public bool SetTotal()
        {           
            bool ret = true;
            try
            {
                // 割合、数量、換算後数量の明細合計を算出
                decimal dWariai = 0;
                decimal dHaikiSuu = 0;
                decimal dKansanSuu = 0;
                for (int i = 0; i < this.form.haikiButsuDetail.RowCount; i++)
                {
                    if (this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_WARIAI].Value != null &&
                        this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_WARIAI].Value.ToString() != string.Empty)
                    {
                        dWariai += Convert.ToDecimal(this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_WARIAI].Value.ToString().Replace(",", ""));
                    }
                    if (this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_HAIKI_SUU].Value != null &&
                        this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_HAIKI_SUU].Value.ToString() != string.Empty)
                    {
                        dHaikiSuu += Convert.ToDecimal(this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_HAIKI_SUU].Value.ToString().Replace(",", ""));
                    }
                    if (this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_KANSAN_SUU].FormattedValue != null &&
                        this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_KANSAN_SUU].FormattedValue.ToString() != string.Empty)
                    {
                        dKansanSuu += Convert.ToDecimal(this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_KANSAN_SUU].Value.ToString().Replace(",", ""));
                    }
                }
                this.form.ctxt_TotalWariai.Text = dWariai.ToString("#,##0.0");  // 整数3桁,少数第一位まで
                this.form.ctxt_TotalSuryo.Text = dHaikiSuu.ToString(this.ManifestSuuryoFormat);
                this.form.ctxt_KansangoTotalSuryo.Text = dKansanSuu.ToString(this.ManifestSuuryoFormat) + this.unit_name;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTotal", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 実績単位CDのセット
        /// </summary>
        public void SetJissekiTani()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string strTaniCd = this.form.canTxt_JissekiTaniCd.Text;
                string strTaniName = this.GetTaniName(this.form.canTxt_JissekiTaniCd.Text);
                if (this.form.haikiButsuDetail.Rows.Count <= 1)
                {
                    return;
                }

                for (int i = 0; i < this.form.haikiButsuDetail.Rows.Count - 1; i++)
                {
                    this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_HAIKI_UNIT_CD].Value = strTaniCd;
                    this.form.haikiButsuDetail.Rows[i].Cells[CELL_NAME_HAIKI_UNIT_NAME].Value = strTaniName;

                    //換算値、減容値の順に再計算
                    this.CalcKansanSuu(i, 0);
                    this.CalcGenyouSuu(i, 0);
                }
                this.SetTotal();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetJissekiTani", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 単位名称取得
        /// </summary>
        public string GetTaniName(string strTaniCd)
        {
            LogUtility.DebugMethodStart();

            string strTaniName = string.Empty;

            if (strTaniCd != string.Empty)
            {
                var sertch = new CommonUnitDtoCls();
                sertch.UNIT_CD = strTaniCd;

                DataTable dt = mlogic.GetUnit(sertch);
                if (dt.Rows.Count > 0)
                {
                    strTaniName = dt.Rows[0]["UNIT_NAME_RYAKU"].ToString();
                }
            }

            LogUtility.DebugMethodEnd();
            return strTaniName;
        }

        /// <summary>
        /// EDI_PASSWORDを取得
        /// </summary>
        public virtual DataTable getEDI_PASSWORD(string SAI_UPN_SHA_EDI_MEMBER_ID)
        {
            return this.GetEDI_PASSWORDdao.GetDataForEntity(SAI_UPN_SHA_EDI_MEMBER_ID);
        }

        /// <summary>
        /// 紐付け情報更新用のREC_SEQの最大値を保存するためのキー名を生成
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="haikiKbnCd"></param>
        /// <returns></returns>
        private string CreateMaxRecSeqKeyName(string systemId, string haikiKbnCd)
        {
            return systemId + "-" + haikiKbnCd;
        }
    }
}
