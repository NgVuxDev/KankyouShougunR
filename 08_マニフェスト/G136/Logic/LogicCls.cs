// $Id: LogicCls.cs 28443 2014-08-25 02:37:46Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.PaperManifest.JissekiHokokuSyusei.DAO;
using r_framework.Dto;
using System.Collections.ObjectModel;
using System.Linq;

namespace Shougun.Core.PaperManifest.JissekiHokokuSyusei
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class JissekiHokokuSyuseiLogic : IBuisinessLogic
    {
        /// <summary>
        /// 実績報告書データ用DAO
        /// </summary>
        private JissekiHokokuSyuseiDao dao;

        /// <summary>
        /// 実績報告書修正明細データ用DAO
        /// </summary>
        private JissekiHokokuDetailDao detailDao;

        /// <summary>
        /// M_CHIIKIBETSU_BUNRUIデータ取得用Dao
        /// </summary>
        private ChiikibetsuBunruiDao chikiDao;

        /// <summary>
        /// M_TODOFUKENデータ取得用Dao
        /// </summary>
        private IM_TODOUFUKENDao todofukenDao;

        /// <summary>
        /// M_GYOUSHAデータ取得用Dao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// M_GENBAデータ取得用Dao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// M_CHIIKIBETSU_KYOKAデータ取得用Dao
        /// </summary>
        private IM_CHIIKIBETSU_KYOKADao chikiBetsuDao;

        /// <summary>
        /// S_NUMBER_SYSTEMデータ取得用Dao
        /// </summary>
        private IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// S_NUMBER_SYSTEMデータ取得用Dao
        /// </summary>
        private ManiDetailDAO maniDetailDao;


        /// <summary>
        /// Form
        /// </summary>
        private JissekiHokokuSyuseiForm form;

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
        List<T_JISSEKI_HOUKOKU_SBN_DETAIL> detailData;

        /// <summary>
        /// 実績報告書修正明細更新データ
        /// </summary>
        List<T_JISSEKI_HOUKOKU_SBN_DETAIL> updateDetailData;

        /// <summary>
        /// 実績報告書修正マニフェスト明細更新データ
        /// </summary>
        List<T_JISSEKI_HOUKOKU_MANIFEST_DETAIL> updateManiDetailData;

        /// <summary>
        /// G134 logic
        /// </summary>
        Shougun.Core.PaperManifest.JissekiHokoku.LogicClass logic;

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.JissekiHokokuSyusei.Setting.ButtonSetting.xml";

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        HeaderForm headerForm;

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
        public JissekiHokokuSyuseiLogic(JissekiHokokuSyuseiForm targetForm)
        {
            LogUtility.DebugMethodStart();

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<JissekiHokokuSyuseiDao>();
            this.detailDao = DaoInitUtility.GetComponent<JissekiHokokuDetailDao>();
            this.chikiDao = DaoInitUtility.GetComponent<ChiikibetsuBunruiDao>();
            this.todofukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.chikiBetsuDao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_KYOKADao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();
            this.maniDetailDao = DaoInitUtility.GetComponent<ManiDetailDAO>();
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
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // コントロールを初期化
                this.ControlInit();
                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();
                // 実績報告書修正データを取得
                this.GetJissekiHokokuData(this.form.systemid);

                this.form.grdIchiran.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
            var seq = this.resultData.Rows[0]["SEQ"].ToString();
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
            // 実績報告書修正.提出書式
            if (this.resultData.Rows[0]["HOUKOKU_SHOSHIKI_NAME"] != null)
            {
                this.form.TeishutuShosiki.Text = this.resultData.Rows[0]["HOUKOKU_SHOSHIKI_NAME"].ToString();
            }
            // 実績報告書修正.報告年度
            if (this.resultData.Rows[0]["HOUKOKU_YEAR"] != null)
            {
                this.form.HokokuNendo.Text = this.resultData.Rows[0]["HOUKOKU_YEAR"].ToString();
            }
            this.form.TeishutuSakiCd.Text = Convert.ToString(this.resultData.Rows[0]["TEISHUTSU_CHIIKI_CD"]);
            // 実績報告書修正.提出先
            if (this.resultData.Rows[0]["CHIIKI_NAME"] != null)
            {
                this.form.TeishutuSaki.Text = this.resultData.Rows[0]["CHIIKI_NAME"].ToString();
            }
            // 実績報告書修正.自社業種区分
            if (this.resultData.Rows[0]["GYOUSHA_KBN_NAME"] != null)
            {
                this.form.JishaGhoushuKbn.Text = this.resultData.Rows[0]["GYOUSHA_KBN_NAME"].ToString();
            }
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
                //DataRow row = this.multiRowDataTable.NewRow();

                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                string format = mSysInfo.SYS_SUURYOU_FORMAT.ToString();

                var table = this.resultData;
                int i = 0;

                foreach (DataRow row in table.Rows)
                {
                    this.form.grdIchiran.Rows.Add();


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

                    // 実績報告書修正.産業廃棄物・特別管理産業廃棄物の種類CD
                    if (row["HOUKOKUSHO_BUNRUI_CD"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["HoukokushoBunruiCd"].Value = row["HOUKOKUSHO_BUNRUI_CD"].ToString();
                    }
                    // 実績報告書修正.産業廃棄物・特別管理産業廃棄物の種類名
                    if (row["HOUKOKUSHO_BUNRUI_NAME"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["HoukokushoBunruiName"].Value = row["HOUKOKUSHO_BUNRUI_NAME"].ToString();
                    }
                    // 実績報告書修正.受託量
                    if (row["JYUTAKU_RYOU"] != null &&
                        !string.IsNullOrWhiteSpace(row["JYUTAKU_RYOU"].ToString()))
                    {
                        this.form.grdIchiran.Rows[i]["JyutakuRyou"].Value = row["JYUTAKU_RYOU"];
                    }
                    if (row["HST_KEN_KBN"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["HST_KEN_KBN"].Value = row["HST_KEN_KBN"].ToString();
                    }
                    if (row["SBN_GENBA_CHIIKI_CD"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["SBN_GENBA_CHIIKI_CD"].Value = row["SBN_GENBA_CHIIKI_CD"].ToString();
                    }
                    if (row["ITAKU_KBN"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["ITAKU_KBN"].Value = row["ITAKU_KBN"].ToString();
                    }
                    if (row["HST_GENBA_CHIIKI_CD"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["HST_GENBA_CHIIKI_CD"].Value = row["HST_GENBA_CHIIKI_CD"].ToString();
                    }
                    // 実績報告書修正.委託量
                    if (row["ITAKU_RYOU"] != null &&
                        !string.IsNullOrWhiteSpace(row["ITAKU_RYOU"].ToString()))
                    {
                        this.form.grdIchiran.Rows[i]["ItakuRyou"].Value = row["ITAKU_RYOU"];
                    }
                    // 実績報告書修正.排出事業場(排出元の所在地)
                    if (row["HST_GENBA_ADDRESS"] != null)
                    {
                        this.form.grdIchiran.Rows[i]["HstGenbaAddress"].Value = row["HST_GENBA_ADDRESS"].ToString();
                    }
                    //if (this.headerData.HOUKOKU_SHOSHIKI.Value == 1 || this.headerData.HOUKOKU_SHOSHIKI.Value == 2)
                    //{
                        // 明細-排出事業者 1:業者データ、2:現場データ
                        switch (row["HST_GYOUSHA_NAME_DISP_KBN"].ToString())
                        {
                            case "1":
                                // 実績報告書修正.氏名又は名称
                                if (row["HST_GYOUSHA_NAME"] != null)
                                {
                                    this.form.grdIchiran.Rows[i]["HstGyoushaName"].Value = row["HST_GYOUSHA_NAME"].ToString();
                                }
                                
                                break;
                            case "2":
                                // 実績報告書修正.氏名又は名称
                                if (row["HST_GENBA_NAME"] != null)
                                {
                                    this.form.grdIchiran.Rows[i]["HstGyoushaName"].Value = row["HST_GENBA_NAME"].ToString();
                                }
                                
                                break;
                            default:
                                break;
                        }
                        // 実績報告書修正.備考
                        if (row["JYUTAKU_KBN"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["Bikou"].Value = row["JYUTAKU_KBN"].ToString();
                        }
                        // 実績報告書修正.処分方法
                        if (row["SHOBUN_HOUHOU_NAME"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["SbnHouhou"].Value = row["SHOBUN_HOUHOU_NAME"].ToString();
                        }
                        // 実績報告書修正.処分量
                        if (row["SBN_RYOU"] != null &&
                            !string.IsNullOrWhiteSpace(row["SBN_RYOU"].ToString()))
                        {
                            this.form.grdIchiran.Rows[i]["SbnRyou"].Value = row["SBN_RYOU"];
                        }
                        // 実績報告書修正.処分後量
                        if (row["SBN_AFTER_RYOU"] != null &&
                            !string.IsNullOrWhiteSpace(row["SBN_AFTER_RYOU"].ToString()))
                        {
                            this.form.grdIchiran.Rows[i]["SbnAfterRyou"].Value = row["SBN_AFTER_RYOU"];
                        }
                        // 実績報告書修正.処分の場所
                        if (row["SBN_GENBA_ADDRESS"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["SbnGenbaAddress"].Value = row["SBN_GENBA_ADDRESS"].ToString();
                        }
                        // 実績報告書修正.許可番号
                        if (row["ITAKUSAKI_KYOKA_NO"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["ItakusakiKyokaNO"].Value = row["ITAKUSAKI_KYOKA_NO"].ToString();
                        }
                        // 実績報告書修正.委託先の氏名又は名称
                        if (row["ITAKUSAKI_NAME"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["ItakusakiName"].Value = row["ITAKUSAKI_NAME"].ToString();
                        }
                        // 実績報告書修正.委託先の住所
                        if (row["ITAKUSAKI_ADDRESS"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["ItakudakiAddress"].Value = row["ITAKUSAKI_ADDRESS"].ToString();
                        }

                        // 明細-排出事業者用項目
                        if (row["HST_GYOUSHA_CD"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["HST_GYOUSHA_CD"].Value = row["HST_GYOUSHA_CD"].ToString();
                        }
                        if (row["HST_GYOUSHA_NAME"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["HST_GYOUSHA_NAME"].Value = row["HST_GYOUSHA_NAME"].ToString();
                        }
                        if (row["HST_GYOUSHA_ADDRESS"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["HST_GYOUSHA_ADDRESS"].Value = row["HST_GYOUSHA_ADDRESS"].ToString();
                        }
                        if (row["HST_GENBA_CD"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["HST_GENBA_CD"].Value = row["HST_GENBA_CD"].ToString();
                        }
                        if (row["HST_GENBA_NAME"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["HST_GENBA_NAME"].Value = row["HST_GENBA_NAME"].ToString();
                        }
                        if (row["HST_GENBA_ADDRESS"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["HST_GENBA_ADDRESS"].Value = row["HST_GENBA_ADDRESS"].ToString();
                        }
                        if (row["HST_JOU_TODOUFUKEN_CD"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["HST_JOU_TODOUFUKEN_CD"].Value = row["HST_JOU_TODOUFUKEN_CD"].ToString();
                        }
                        // 明細-処分事業者用項目
                        if (row["SBN_GYOUSHA_CD"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["SBN_GYOUSHA_CD"].Value = row["SBN_GYOUSHA_CD"].ToString();
                        }
                        if (row["SBN_GYOUSHA_NAME"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["SBN_GYOUSHA_NAME"].Value = row["SBN_GYOUSHA_NAME"].ToString();
                        }
                        if (row["SBN_GYOUSHA_ADDRESS"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["SBN_GYOUSHA_ADDRESS"].Value = row["SBN_GYOUSHA_ADDRESS"].ToString();
                        }
                        if (row["SBN_GENBA_CD"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["SBN_GENBA_CD"].Value = row["SBN_GENBA_CD"].ToString();
                        }
                        if (row["SBN_GENBA_NAME"] != null)
                        {
                            this.form.grdIchiran.Rows[i]["SBN_GENBA_NAME"].Value = row["SBN_GENBA_NAME"].ToString();
                        }
                        // 値が入っていれば必須項目に設定
                        this.SetRequired(i);
                    //}
                    i++;
                }

                // 産業廃棄物・特別管理産業廃棄物の種類検索ボタン初期化
                this.GcCustomPopupOpenButtonCellInit1("gcCustomPopupOpenButtonCell1",
                                                     this.resultData.Rows[0]["TEISHUTSU_CHIIKI_CD"].ToString(),
                                                     "M_CHIIKIBETSU_BUNRUI");

                // 処分方法検索ボタン初期化
                this.GcCustomPopupOpenButtonCellInit1("gcCustomPopupOpenButtonCell4",
                                                     this.resultData.Rows[0]["TEISHUTSU_CHIIKI_CD"].ToString(),
                                                     "M_CHIIKIBETSU_SHOBUN");

                //bool bl = this.resultData.Rows[0]["HOUKOKU_SHOSHIKI"].ToString() == "3" ||
                //          this.resultData.Rows[0]["HOUKOKU_SHOSHIKI"].ToString() == "4";
                bool bl = false;

                for (int j = 0; j < this.form.grdIchiran.Rows.Count; j++)
                {
                    // 書式 1-3号、1-4号の場合受託量、委託量以外項目は編不可
                    this.form.grdIchiran.Rows[j]["gcCustomPopupOpenButtonCell1"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["gcCustomPopupOpenButtonCell4"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["gcCustomPopupOpenButtonCell5"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["gcCustomPopupOpenButtonCell6"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["gcCustomPopupOpenButtonCell7"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["HoukokushoBunruiCd"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["HoukokushoBunruiName"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["HstGyoushaName"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["HstGenbaAddress"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["Bikou"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["SbnHouhou"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["SbnRyou"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["SbnAfterRyou"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["SbnGenbaAddress"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["ItakusakiKyokaNO"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["ItakusakiName"].ReadOnly = bl;
                    this.form.grdIchiran.Rows[j]["ItakudakiAddress"].ReadOnly = bl;

                    if (bl)
                    {
                        this.form.grdIchiran.Rows[j].Cells["JyutakuRyou"].ReadOnly = !bl;
                        this.form.grdIchiran.Rows[j].Cells["ItakuRyou"].ReadOnly = !bl;
                    }

                    /*if (this.resultData.Rows[0]["HST_GYOUSHA_NAME_DISP_KBN"].ToString() == "1")
                    {
                        // 氏名又は名称検索ボタン初期化
                        this.form.grdIchiran.Rows[j]["gcCustomPopupOpenButtonCell7"].Visible = false;

                        // 氏名又は名称検索ボタン初期化
                        this.form.grdIchiran.Rows[j]["gcCustomPopupOpenButtonCell2"].Visible = true;
                    }
                    else
                    {
                        // 氏名又は名称検索ボタン初期化
                        this.form.grdIchiran.Rows[j]["gcCustomPopupOpenButtonCell7"].Visible = true;

                        // 氏名又は名称検索ボタン初期化
                        this.form.grdIchiran.Rows[j]["gcCustomPopupOpenButtonCell2"].Visible = false;
                    }*/
                }

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
        /// 必須項目設定
        /// </summary>
        /// <param name="row">ROW</param>
        /// <param name="cellName">セル名(カラム名)</param>
        private void SetRequired(int rowNumber)
        {
            // 必須チェックのオブジェクトを生成
            SelectCheckDto existCheck = new SelectCheckDto();
            existCheck.CheckMethodName = "必須チェック";
            Collection<SelectCheckDto> existChecks = new Collection<SelectCheckDto>();
            existChecks.Add(existCheck);

            // 対象の項目のリストを作成
            var targetList = this.form.grdIchiran.Rows[rowNumber].Cells.Where(c => c.Name == "HoukokushoBunruiCd" ||
                                                                                    c.Name == "HoukokushoBunruiName" ||
                                                                                    c.Name == "HstGyoushaName" ||
                                                                                    c.Name == "HstGenbaAddress" ||
                                                                                    c.Name == "JyutakuRyou" ||
                                                                                    c.Name == "SbnHouhou" ||
                                                                                    c.Name == "SbnRyou" ||
                                                                                    c.Name == "SbnAfterRyou" ||
                                                                                    c.Name == "SbnGenbaAddress").ToList();

            // 値がある場合のみ必須チェックを設定
            foreach (var target in targetList)
            {
                var visible = target.GetType().GetProperty("Visible");
                if (visible != null && (bool)visible.GetValue(target, null)
                    && target.Value != null && !string.IsNullOrEmpty(target.Value.ToString()))
                {
                    PropertyUtility.SetValue(target, "RegistCheckMethod", existChecks);
                }
            }
        }

        /// <summary>
        /// GcCustomPopupOpenButtonCellの初期化を行う
        /// </summary>
        /// <param name="buttonNmae">ボタン名</param>
        /// <param name="value">条件値</param>
        /// <param name="tabelName">検索テーブル名</param>
        private void GcCustomPopupOpenButtonCellInit1(string buttonName, string value, string tableName)
        {
            if (this.form.grdIchiran.Rows.Count == 0)
            {
                return;
            }
            r_framework.CustomControl.GcCustomPopupOpenButtonCell_Ex cell = new r_framework.CustomControl.GcCustomPopupOpenButtonCell_Ex();
            r_framework.Dto.JoinMethodDto dto = new r_framework.Dto.JoinMethodDto();
            r_framework.Dto.SearchConditionsDto item = new r_framework.Dto.SearchConditionsDto();
            for (int i = 0; i < this.form.grdIchiran.Rows.Count; i++)
            {
                cell = new r_framework.CustomControl.GcCustomPopupOpenButtonCell_Ex();
                cell = this.form.grdIchiran.Rows[i][buttonName] as r_framework.CustomControl.GcCustomPopupOpenButtonCell_Ex;
                cell.popupWindowSetting.Clear();
                dto = new r_framework.Dto.JoinMethodDto();
                item = new r_framework.Dto.SearchConditionsDto();
                item.And_Or = CONDITION_OPERATOR.AND;
                item.Condition = JUGGMENT_CONDITION.EQUALS;
                item.LeftColumn = "CHIIKI_CD";
                item.Value = value;
                item.ValueColumnType = DB_TYPE.VARCHAR;
                dto.SearchCondition.Add(item);
                dto.Join = JOIN_METHOD.WHERE;
                dto.LeftTable = tableName;
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
            this.multiRowDataTable.Columns.Add("HoukokushoBunruiCd");
            this.multiRowDataTable.Columns.Add("HoukokushoBunruiName");
            this.multiRowDataTable.Columns.Add("HstGyoushaName");
            this.multiRowDataTable.Columns.Add("HstGenbaAddress");
            this.multiRowDataTable.Columns.Add("JyutakuRyou");
            this.multiRowDataTable.Columns.Add("Bikou");
            this.multiRowDataTable.Columns.Add("SbnHouhou");
            this.multiRowDataTable.Columns.Add("SbnRyou");
            this.multiRowDataTable.Columns.Add("SbnAfterRyou");
            this.multiRowDataTable.Columns.Add("ItakusakiKyokaNO");
            this.multiRowDataTable.Columns.Add("ItakusakiName");
            this.multiRowDataTable.Columns.Add("ItakudakiAddress");
            this.multiRowDataTable.Columns.Add("ItakuRyou");
            this.multiRowDataTable.Columns.Add("SystemId");
            this.multiRowDataTable.Columns.Add("Seq");
            this.multiRowDataTable.Columns.Add("DetailSystemId");
            //this.multiRowDataTable.Columns.Add("TeishutsuChiikiCd");
            this.multiRowDataTable.Columns.Add("HST_GYOUSHA_CD");
            this.multiRowDataTable.Columns.Add("HST_GYOUSHA_NAME");
            this.multiRowDataTable.Columns.Add("HST_GYOUSHA_ADDRESS");
            this.multiRowDataTable.Columns.Add("HST_GENBA_CD");
            this.multiRowDataTable.Columns.Add("HST_GENBA_NAME");
            this.multiRowDataTable.Columns.Add("HST_GENBA_ADDRESS");
            this.multiRowDataTable.Columns.Add("HST_GENBA_CHIIKI_CD");
            this.multiRowDataTable.Columns.Add("HST_KEN_KBN");
            this.multiRowDataTable.Columns.Add("HST_JOU_TODOUFUKEN_CD");
            this.multiRowDataTable.Columns.Add("SBN_GYOUSHA_CD");
            this.multiRowDataTable.Columns.Add("SBN_GYOUSHA_NAME");
            this.multiRowDataTable.Columns.Add("SBN_GYOUSHA_ADDRESS");
            this.multiRowDataTable.Columns.Add("SBN_GENBA_CD");
            this.multiRowDataTable.Columns.Add("SBN_GENBA_NAME");
            this.multiRowDataTable.Columns.Add("SbnGenbaAddress");
            this.multiRowDataTable.Columns.Add("SBN_GENBA_CHIIKI_CD");
            this.multiRowDataTable.Columns.Add("ITAKU_KBN");
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

                this.form.grdIchiran.CellEnter += new EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.grdIchiran_CellEnter);
                this.form.grdIchiran.CellValidated += new EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.grdIchiran_CellValidated);
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
                // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                //Control[] ctl = { this.form.grdIchiran };
                //var autoCheckLogic = new AutoRegistCheckLogic(ctl);
                //this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                //if (!this.form.RegistErrorFlag)
                {
                    this.CreateEntry();
                    M_SYS_INFO[] sysInfo = this.logic.sysInfoDao.GetAllData();
                    if (sysInfo != null)
                    {
                        this.logic.sysInfoEntity = sysInfo[0];
                    }
                    this.logic.PrintView();
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
                //FormManager.OpenFormModal("G609", this.headerData, true);
                var messageShowLogic = new MessageBoxShowLogic();
                if (this.form.grdIchiran.Rows.Count == 0)
                {
                    DialogResult result = messageShowLogic.MessageBoxShow("E044");
                    return;
                }
                using (CustomDataGridView cdgv = new CustomDataGridView())
                {
                    this.form.Controls.Add(cdgv);
                    cdgv.Visible = false;
                    DataTable dt = new DataTable();
                    //dt.Columns.Add(Convert.ToString("部署CD"));
                    dt.Columns.Add(Convert.ToString("廃棄物種類CD"));
                    dt.Columns.Add(Convert.ToString("廃棄物種類名"));
                    dt.Columns.Add(Convert.ToString("排出事業者名"));
                    dt.Columns.Add(Convert.ToString("排出事業場住所"));
                    dt.Columns.Add(Convert.ToString("受託量"));
                    dt.Columns.Add(Convert.ToString("備考"));
                    dt.Columns.Add(Convert.ToString("処分方法"));
                    dt.Columns.Add(Convert.ToString("処分前の数量"));
                    dt.Columns.Add(Convert.ToString("処分後の数量"));
                    dt.Columns.Add(Convert.ToString("処分場所"));
                    dt.Columns.Add(Convert.ToString("許可番号"));
                    dt.Columns.Add(Convert.ToString("委託先名"));
                    dt.Columns.Add(Convert.ToString("委託先住所"));
                    dt.Columns.Add(Convert.ToString("委託量"));
                    foreach (Row row in this.form.grdIchiran.Rows)
                    {
                        DataRow newRow = dt.NewRow();
                        //newRow["部署CD"] = row["BUSHO_CD"].Value;
                        newRow["廃棄物種類CD"] = row["HoukokushoBunruiCd"].Value;
                        newRow["廃棄物種類名"] = row["HoukokushoBunruiName"].Value;
                        newRow["排出事業者名"] = row["HstGyoushaName"].Value;
                        newRow["排出事業場住所"] = row["HstGenbaAddress"].Value;
                        if (row["JyutakuRyou"].Value != null)
                        {
                            newRow["受託量"] = this.ConvertSuuryo(row["JyutakuRyou"].Value.ToString());
                        }
                        newRow["備考"] = row["Bikou"].Value;
                        newRow["処分方法"] = row["SbnHouhou"].Value;
                        if (row["SbnRyou"].Value != null)
                        {
                            newRow["処分前の数量"] = this.ConvertSuuryo(row["SbnRyou"].Value.ToString());
                        }
                        if (row["SbnAfterRyou"].Value != null)
                        {
                            newRow["処分後の数量"] = this.ConvertSuuryo(row["SbnAfterRyou"].Value.ToString());
                        }
                        newRow["処分場所"] = row["SbnGenbaAddress"].Value;
                        newRow["許可番号"] = row["ItakusakiKyokaNO"].Value;
                        newRow["委託先名"] = row["ItakusakiName"].Value;
                        newRow["委託先住所"] = row["ItakudakiAddress"].Value;
                        if (row["ItakuRyou"].Value != null)
                        {
                            newRow["委託量"] = this.ConvertSuuryo(row["ItakuRyou"].Value.ToString());
                        }
                        dt.Rows.Add(newRow);
                    }
                    //----------------
                    cdgv.DataSource = dt;
                    //----------------
                    cdgv.Refresh();

                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(cdgv, true, false,
                        WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_JISSEKIHOKOKU), this.form);

                    this.form.Controls.Remove(cdgv);
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
            try
            {
                // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                //Control[] ctl = { this.form.grdIchiran };
                //var autoCheckLogic = new AutoRegistCheckLogic(ctl);
                //this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                //if (!this.form.RegistErrorFlag)
                {
                    //独自チェックの記述例を書く
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        {
                            this.SetUpdateData();
                            // 明細データを更新する
                            foreach (T_JISSEKI_HOUKOKU_SBN_DETAIL row in this.updateDetailData)
                            {
                                T_JISSEKI_HOUKOKU_SBN_DETAIL dto = new T_JISSEKI_HOUKOKU_SBN_DETAIL();
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
                                // 元EntityにDELETE_FLG更新
                                this.headerData.DELETE_FLG = true;
                                new DataBinderLogic<T_JISSEKI_HOUKOKU_ENTRY>(this.headerData).SetSystemProperty(this.headerData, true);
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
                        // 実績報告書修正データを取得する
                        this.GetJissekiHokokuData(this.form.systemid);
                        // ヘッダーデータ設定する
                        this.SetHeaderData();
                        // MultiRow初期化用DataTable
                        this.MultiRowDataTableInit();
                        // MultiRowにデータを追加する
                        this.MultiRowInit();
                    }
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
                this.updateDetailData = new List<T_JISSEKI_HOUKOKU_SBN_DETAIL>();
                // 明細マニフェストデータを設定
                this.updateManiDetailData = new List<T_JISSEKI_HOUKOKU_MANIFEST_DETAIL>();
                // updateDetailData
                T_JISSEKI_HOUKOKU_SBN_DETAIL updateDetailRow = new T_JISSEKI_HOUKOKU_SBN_DETAIL();
                // T_JISSEKI_HOUKOKU_MANIFEST_DETAILデータ
                T_JISSEKI_HOUKOKU_MANIFEST_DETAIL maniData = new T_JISSEKI_HOUKOKU_MANIFEST_DETAIL();
                // DetailSystemIdを取得
                //int detailSystemId = this.createSystemIdForJissekiHokoku();
                foreach (T_JISSEKI_HOUKOKU_SBN_DETAIL row in this.detailData)
                {
                    for (int i = 0; i < this.form.grdIchiran.RowCount; i++)
                    {
                        // 同じきーデータ設定
                        if (row.SYSTEM_ID.ToString().Equals(this.form.grdIchiran.Rows[i]["SystemId"].Value.ToString())
                            && row.SEQ.ToString().Equals(this.form.grdIchiran.Rows[i]["Seq"].Value.ToString())
                            && row.DETAIL_SYSTEM_ID.ToString().Equals(this.form.grdIchiran.Rows[i]["DetailSystemId"].Value.ToString()))
                        {
                            // 明細データを設定
                            updateDetailRow = new T_JISSEKI_HOUKOKU_SBN_DETAIL();
                            updateDetailRow = row;
                            updateDetailRow.SEQ = seq + 1;
                            updateDetailRow.DETAIL_SYSTEM_ID = Convert.ToInt64(this.form.grdIchiran.Rows[i]["DetailSystemId"].Value.ToString());
                            updateDetailRow.HOUKOKUSHO_BUNRUI_CD = Convert.ToString(this.form.grdIchiran.Rows[i]["HoukokushoBunruiCd"].Value);
                            updateDetailRow.HOUKOKUSHO_BUNRUI_NAME = Convert.ToString(this.form.grdIchiran.Rows[i]["HoukokushoBunruiName"].Value);

                            if (this.form.grdIchiran.Rows[i]["JyutakuRyou"].Value != null &&
                                !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["JyutakuRyou"].Value.ToString()))
                            {
                                updateDetailRow.JYUTAKU_RYOU = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["JyutakuRyou"].Value);
                            }
                            else
                            {
                                updateDetailRow.JYUTAKU_RYOU = SqlDecimal.Null;
                            }
                            if (this.form.grdIchiran.Rows[i]["ItakuRyou"].Value != null &&
                                !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["ItakuRyou"].Value.ToString()))
                            {
                                updateDetailRow.ITAKU_RYOU = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["ItakuRyou"].Value);
                            }
                            else
                            {
                                updateDetailRow.ITAKU_RYOU = SqlDecimal.Null;
                            }
                            //if (this.headerData.HOUKOKU_SHOSHIKI.Value == 1 || this.headerData.HOUKOKU_SHOSHIKI.Value == 2)
                            //{
                                updateDetailRow.HST_GENBA_CD = Convert.ToString(this.form.grdIchiran.Rows[i]["HST_GENBA_CD"].Value);
                                updateDetailRow.HST_GENBA_CHIIKI_CD = Convert.ToString(this.form.grdIchiran.Rows[i]["HST_GENBA_CHIIKI_CD"].Value);
                                updateDetailRow.HST_GYOUSHA_CD = Convert.ToString(this.form.grdIchiran.Rows[i]["HST_GYOUSHA_CD"].Value);
                                updateDetailRow.HST_GYOUSHA_ADDRESS = this.form.grdIchiran.Rows[i]["HST_GYOUSHA_ADDRESS"].Value.ToString();
                                updateDetailRow.HST_GENBA_ADDRESS = this.form.grdIchiran.Rows[i]["HstGenbaAddress"].Value.ToString();
                                // 実績報告書の区分によって排出事業者に表示されるデータが異なる
                                if (this.headerData.HST_GYOUSHA_NAME_DISP_KBN == 2)
                                {
                                    // 2.現場名
                                    updateDetailRow.HST_GENBA_NAME = this.form.grdIchiran.Rows[i]["HstGyoushaName"].Value.ToString();
                                }
                                else
                                {
                                    // 1.業者名
                                    updateDetailRow.HST_GYOUSHA_NAME = this.form.grdIchiran.Rows[i]["HstGyoushaName"].Value.ToString();
                                }

                                updateDetailRow.JYUTAKU_KBN = Convert.ToString(this.form.grdIchiran.Rows[i]["Bikou"].Value);
                                updateDetailRow.SHOBUN_HOUHOU_NAME = Convert.ToString(this.form.grdIchiran.Rows[i]["SbnHouhou"].Value);
                                if (this.form.grdIchiran.Rows[i]["SbnRyou"].Value != null &&
                                    !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["SbnRyou"].Value.ToString()))
                                {
                                    updateDetailRow.SBN_RYOU = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["SbnRyou"].Value);
                                }
                                else
                                {
                                    updateDetailRow.SBN_RYOU = SqlDecimal.Null;
                                }
                                if (this.form.grdIchiran.Rows[i]["SbnAfterRyou"].Value != null &&
                                    !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["SbnAfterRyou"].Value.ToString()))
                                {
                                    updateDetailRow.SBN_AFTER_RYOU = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["SbnAfterRyou"].Value);
                                }
                                else
                                {
                                    updateDetailRow.SBN_AFTER_RYOU = SqlDecimal.Null;
                                }
                                updateDetailRow.SBN_GENBA_CD = Convert.ToString(this.form.grdIchiran.Rows[i]["SBN_GENBA_CD"].Value);
                                updateDetailRow.SBN_GENBA_NAME = Convert.ToString(this.form.grdIchiran.Rows[i]["SBN_GENBA_NAME"].Value);
                                updateDetailRow.SBN_GENBA_ADDRESS = Convert.ToString(this.form.grdIchiran.Rows[i]["SbnGenbaAddress"].Value);
                                updateDetailRow.SBN_GENBA_CHIIKI_CD = Convert.ToString(this.form.grdIchiran.Rows[i]["SBN_GENBA_CHIIKI_CD"].Value);
                                updateDetailRow.SBN_GYOUSHA_CD = Convert.ToString(this.form.grdIchiran.Rows[i]["SBN_GYOUSHA_CD"].Value);
                                updateDetailRow.SBN_GYOUSHA_NAME = Convert.ToString(this.form.grdIchiran.Rows[i]["SBN_GYOUSHA_NAME"].Value);
                                updateDetailRow.SBN_GYOUSHA_ADDRESS = Convert.ToString(this.form.grdIchiran.Rows[i]["SBN_GYOUSHA_ADDRESS"].Value);

                                updateDetailRow.ITAKUSAKI_KYOKA_NO = Convert.ToString(this.form.grdIchiran.Rows[i]["ItakusakiKyokaNO"].Value);
                                updateDetailRow.ITAKUSAKI_NAME = Convert.ToString(this.form.grdIchiran.Rows[i]["ItakusakiName"].Value);
                                updateDetailRow.ITAKUSAKI_ADDRESS = Convert.ToString(this.form.grdIchiran.Rows[i]["ItakudakiAddress"].Value);

                                if (updateDetailRow.SBN_GENBA_CHIIKI_CD.Equals(updateDetailRow.HST_GENBA_CHIIKI_CD))
                                {
                                    updateDetailRow.HST_KEN_KBN = 1;
                                }
                                else
                                {
                                    updateDetailRow.HST_KEN_KBN = 2;
                                }

                            //}
                            this.updateDetailData.Add(updateDetailRow);

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
                                    maniData.SYSTEM_ID = updateDetailRow.SYSTEM_ID;
                                    maniData.SEQ = updateDetailRow.SEQ;
                                    maniData.DETAIL_SYSTEM_ID = updateDetailRow.DETAIL_SYSTEM_ID;
                                    maniData.DETAIL_ROW_NO = Convert.ToInt32(resultManiDetailData.Rows[j]["DETAIL_ROW_NO"]);
                                    if (resultManiDetailData.Rows[j]["REPORT_ID"] != null
                                        && !string.IsNullOrWhiteSpace(resultManiDetailData.Rows[j]["REPORT_ID"].ToString()))
                                    {
                                        maniData.REPORT_ID = Convert.ToInt16(resultManiDetailData.Rows[j]["REPORT_ID"]);
                                    }
                                    if (resultManiDetailData.Rows[j]["HAIKI_KBN_CD"] != null
                                        && !string.IsNullOrWhiteSpace(resultManiDetailData.Rows[j]["HAIKI_KBN_CD"].ToString()))
                                    {
                                        maniData.HAIKI_KBN_CD = Convert.ToInt16(resultManiDetailData.Rows[j]["HAIKI_KBN_CD"]);
                                    }
                                    if (resultManiDetailData.Rows[j]["MANI_SYSTEM_ID"] != null
                                        && !string.IsNullOrWhiteSpace(resultManiDetailData.Rows[j]["MANI_SYSTEM_ID"].ToString()))
                                    {
                                        maniData.MANI_SYSTEM_ID = Convert.ToInt64(resultManiDetailData.Rows[j]["MANI_SYSTEM_ID"]);
                                    }
                                    if (resultManiDetailData.Rows[j]["MANI_SEQ"] != null
                                        && !string.IsNullOrWhiteSpace(resultManiDetailData.Rows[j]["MANI_SEQ"].ToString()))
                                    {
                                        maniData.MANI_SEQ = Convert.ToInt32(resultManiDetailData.Rows[j]["MANI_SEQ"]);
                                    }
                                    if (resultManiDetailData.Rows[j]["DEN_MANI_KANRI_ID"] != null)
                                    {
                                        maniData.DEN_MANI_KANRI_ID = resultManiDetailData.Rows[j]["DEN_MANI_KANRI_ID"].ToString();
                                    }
                                    if (resultManiDetailData.Rows[j]["DEN_MANI_SEQ"] != null
                                        && !string.IsNullOrWhiteSpace(resultManiDetailData.Rows[j]["DEN_MANI_SEQ"].ToString()))
                                    {
                                        maniData.DEN_MANI_SEQ = Convert.ToInt32(resultManiDetailData.Rows[j]["DEN_MANI_SEQ"]);
                                    }
                                    if (resultManiDetailData.Rows[j]["MANIFEST_ID"] != null)
                                    {
                                        maniData.MANIFEST_ID = Convert.ToString(resultManiDetailData.Rows[j]["MANIFEST_ID"]);
                                    }
                                    this.updateManiDetailData.Add(maniData);
                                }
                            }
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
        /// 印刷用データを取得する
        /// </summary>
        private void CreateEntry()
        {
            this.logic = new Shougun.Core.PaperManifest.JissekiHokoku.LogicClass(new Shougun.Core.PaperManifest.JissekiHokoku.UIForm());

            // 帳票ヘッダーデータ
            logic.tjHoukokuEntry = new T_JISSEKI_HOUKOKU_ENTRY();
            // 帳票明細データ
            logic.tjHoukokuSbnEntryList = new List<T_JISSEKI_HOUKOKU_SBN_DETAIL>();

            logic.tjHoukokuEntry = this.headerData;
            logic.tjHoukokuEntry.TEISHUTU_DATE = Convert.ToDateTime(this.form.TeishutsuYmd.Value);

            T_JISSEKI_HOUKOKU_SBN_DETAIL row = new T_JISSEKI_HOUKOKU_SBN_DETAIL();
            for (int i = 0; i < this.form.grdIchiran.RowCount; i++)
            {
                row = new T_JISSEKI_HOUKOKU_SBN_DETAIL();
                // 明細データを設定
                row.HOUKOKUSHO_BUNRUI_CD = this.form.grdIchiran.Rows[i]["HoukokushoBunruiCd"].Value.ToString();
                row.HOUKOKUSHO_BUNRUI_NAME = this.form.grdIchiran.Rows[i]["HoukokushoBunruiName"].Value.ToString();
                
                if (this.form.grdIchiran.Rows[i]["JyutakuRyou"].Value != null &&
                    !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["JyutakuRyou"].Value.ToString()))
                {
                    row.JYUTAKU_RYOU = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["JyutakuRyou"].Value.ToString());
                }
                else
                {
                    row.JYUTAKU_RYOU = SqlDecimal.Null;
                }

                if (this.form.grdIchiran.Rows[i]["ItakuRyou"].Value != null &&
                    !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["ItakuRyou"].Value.ToString()))
                {
                    row.ITAKU_RYOU = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["ItakuRyou"].Value.ToString());
                }
                else
                {
                    row.ITAKU_RYOU = SqlDecimal.Null;
                }
                if (this.form.grdIchiran.Rows[i]["HST_KEN_KBN"].Value != null &&
                    !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["HST_KEN_KBN"].Value.ToString()))
                {
                    row.HST_KEN_KBN = Convert.ToInt16(this.form.grdIchiran.Rows[i]["HST_KEN_KBN"].Value.ToString());
                }
                if (this.form.grdIchiran.Rows[i]["ITAKU_KBN"].Value != null &&
                    !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["ITAKU_KBN"].Value.ToString()))
                {
                    row.ITAKU_KBN = Convert.ToInt16(this.form.grdIchiran.Rows[i]["ITAKU_KBN"].Value.ToString());
                }
                row.SBN_GENBA_CHIIKI_CD = this.form.grdIchiran.Rows[i]["SBN_GENBA_CHIIKI_CD"].Value.ToString();
                row.HST_GENBA_CHIIKI_CD = this.form.grdIchiran.Rows[i]["HST_GENBA_CHIIKI_CD"].Value.ToString();

                //if (this.headerData.HOUKOKU_SHOSHIKI.Value == 1 || this.headerData.HOUKOKU_SHOSHIKI.Value == 2)
                //{
                    row.HST_GYOUSHA_CD = this.form.grdIchiran.Rows[i]["HST_GYOUSHA_CD"].Value.ToString();

                    row.HST_GENBA_CD = this.form.grdIchiran.Rows[i]["HST_GENBA_CD"].Value.ToString();

                    // 画面に表示しているデータを設定
                    row.HST_GYOUSHA_NAME = this.form.grdIchiran.Rows[i]["HstGyoushaName"].Value.ToString();
                    row.HST_GENBA_ADDRESS = this.form.grdIchiran.Rows[i]["HstGenbaAddress"].Value.ToString();

                    if (this.form.grdIchiran.Rows[i]["Bikou"].Value != null &&
                        !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["Bikou"].Value.ToString()))
                    {
                        row.JYUTAKU_KBN = this.form.grdIchiran.Rows[i]["Bikou"].Value.ToString();
                    }
                    row.SHOBUN_HOUHOU_NAME = this.form.grdIchiran.Rows[i]["SbnHouhou"].Value.ToString();
                    if (this.form.grdIchiran.Rows[i]["SbnRyou"].Value != null &&
                        !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["SbnRyou"].Value.ToString()))
                    {
                        row.SBN_RYOU = Convert.ToDecimal(this.form.grdIchiran.Rows[i]["SbnRyou"].Value.ToString());
                    }
                    else
                    {
                        row.SBN_RYOU = SqlDecimal.Null;
                    }
                    if (this.form.grdIchiran.Rows[i]["SbnAfterRyou"].Value != null &&
                        !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["SbnAfterRyou"].Value.ToString()))
                    {
                        row.SBN_AFTER_RYOU = decimal.Parse(this.form.grdIchiran.Rows[i]["SbnAfterRyou"].Value.ToString());
                    }
                    else
                    {
                        row.SBN_AFTER_RYOU = SqlDecimal.Null;
                    }

                    row.SBN_GENBA_ADDRESS = this.form.grdIchiran.Rows[i]["SbnGenbaAddress"].Value.ToString();
                    row.SBN_GENBA_CD = this.form.grdIchiran.Rows[i]["SBN_GENBA_CD"].Value.ToString();
                    row.SBN_GENBA_NAME = this.form.grdIchiran.Rows[i]["SBN_GENBA_NAME"].Value.ToString();
                    row.SBN_GYOUSHA_CD = this.form.grdIchiran.Rows[i]["SBN_GYOUSHA_CD"].Value.ToString();
                    row.SBN_GYOUSHA_NAME = this.form.grdIchiran.Rows[i]["SBN_GYOUSHA_NAME"].Value.ToString();
                    row.SBN_GYOUSHA_ADDRESS = this.form.grdIchiran.Rows[i]["SBN_GYOUSHA_ADDRESS"].Value.ToString();

                    if (this.form.grdIchiran.Rows[i]["ItakusakiKyokaNO"].Value != null &&
                        !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["ItakusakiKyokaNO"].Value.ToString()))
                    {
                        row.ITAKUSAKI_KYOKA_NO = this.form.grdIchiran.Rows[i]["ItakusakiKyokaNO"].Value.ToString();
                    }
                    if (this.form.grdIchiran.Rows[i]["ItakusakiName"].Value != null &&
                        !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["ItakusakiName"].Value.ToString()))
                    {
                        row.ITAKUSAKI_NAME = this.form.grdIchiran.Rows[i]["ItakusakiName"].Value.ToString();
                    }
                    if (this.form.grdIchiran.Rows[i]["ItakudakiAddress"].Value != null &&
                        !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["ItakudakiAddress"].Value.ToString()))
                    {
                        row.ITAKUSAKI_ADDRESS = this.form.grdIchiran.Rows[i]["ItakudakiAddress"].Value.ToString();
                    }
                    if (this.form.grdIchiran.Rows[i]["HST_JOU_TODOUFUKEN_CD"].Value != null &&
                        !string.IsNullOrWhiteSpace(this.form.grdIchiran.Rows[i]["HST_JOU_TODOUFUKEN_CD"].Value.ToString()))
                    {
                        row.HST_JOU_TODOUFUKEN_CD = Convert.ToInt16(this.form.grdIchiran.Rows[i]["HST_JOU_TODOUFUKEN_CD"].Value.ToString());
                    }
                    if (row.SBN_GENBA_CHIIKI_CD.Equals(row.HST_GENBA_CHIIKI_CD))
                    {
                        row.HST_KEN_KBN = 1;
                    }
                    else
                    {
                        row.HST_KEN_KBN = 2;
                    }
                //}
                logic.tjHoukokuSbnEntryList.Add(row);
            }

        }

        /// <summary>
        /// ダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool cellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            bool ret = true;
            try
            {
                if (e.RowIndex < 0)
                {
                    return ret;
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
                ret = false;
            }
            finally
            {
            }
            return ret;
        }

        /// <summary>
        /// M_CHIIKIBETSU_BUNRUIからコントロールにデータを設定
        /// </summary>
        /// <param name="chiikiCd">CHIIKI_CD</param>
        /// <param name="shuruiCd">HOUKOKUSHO_BUNRUI_CD</param>
        /// <param name="shuruiName">HOUKOKU_BUNRUI_NAME</param>
        private void SetChikiData(string chiikiCd, GcCustomTextBoxCell shuruiCd, GcCustomTextBoxCell shuruiName)
        {
            M_CHIIKIBETSU_BUNRUI data = new M_CHIIKIBETSU_BUNRUI();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (chiikiCd != null && shuruiCd.EditedFormattedValue != null)
            {
                data.CHIIKI_CD = chiikiCd;
                //data.HOUKOKUSHO_BUNRUI_CD = shuruiCd.EditedFormattedValue.ToString().PadLeft(6, '0');
                data.HOUKOKU_BUNRUI_CD = shuruiCd.EditedFormattedValue.ToString();
                data.DELETE_FLG = false;

                DataTable result = this.chikiDao.GetChikiData(data);
                if (result != null && result.Rows.Count > 0)
                {
                    shuruiCd.Value = result.Rows[0]["HOUKOKU_BUNRUI_CD"].ToString();
                    shuruiName.Value = result.Rows[0]["HOUKOKU_BUNRUI_NAME"].ToString();
                }
            }
        }

        /// <summary>
        /// 廃棄種類パップアップ
        /// </summary>
        /// <param name="rowIndex"></param>
        //internal void HaikiShuruiCD_PopupAfter(int rowIndex)
        //{
        //    string chiikiCd = this.resultData.Rows[0]["TEISHUTSU_CHIIKI_CD"].ToString();
        //    this.SetChikiData(chiikiCd, (GcCustomTextBoxCell)this.form.grdIchiran.Rows[rowIndex]["HoukokushoBunruiCd"],
        //                                (GcCustomTextBoxCell)this.form.grdIchiran.Rows[rowIndex]["HoukokushoBunruiName"]);
        //}

        /// <summary>
        /// 現場パップアップ後
        /// </summary>
        /// <param name="index"></param>
        internal void GetGenba_PopupAfter(int index)
        {
            try
            {
                string gyoushaCd = string.Empty;
                M_GENBA dto = new M_GENBA();

                if (this.form.grdIchiran.Rows[index]["HST_GYOUSHA_CD"].Value != null
                    && this.form.grdIchiran.Rows[index]["HST_GENBA_CD"].Value != null)
                {
                    gyoushaCd = this.form.grdIchiran.Rows[index]["HST_GYOUSHA_CD"].Value.ToString();
                    dto.GYOUSHA_CD = this.form.grdIchiran.Rows[index]["HST_GYOUSHA_CD"].Value.ToString();
                    dto.GENBA_CD = this.form.grdIchiran.Rows[index]["HST_GENBA_CD"].Value.ToString();
                    dto = this.genbaDao.GetDataByCd(dto);
                }

                // 住所用の都道府県を取得
                M_TODOUFUKEN todofuken = new M_TODOUFUKEN();
                if (dto != null)
                {
                    todofuken = this.todofukenDao.GetDataByCd(dto.GENBA_TODOUFUKEN_CD.ToString());
                }
                this.form.grdIchiran.Rows[index]["HstGenbaAddress"].Value = dto != null ? todofuken.TODOUFUKEN_NAME + dto.GENBA_ADDRESS1 + dto.GENBA_ADDRESS2 : string.Empty;

                if (this.resultData.Rows[0]["HST_GYOUSHA_NAME_DISP_KBN"].ToString() == "2")
                {                    
                    // 排出事業者の氏名又は名称を表示する
                    this.form.grdIchiran.Rows[index]["HstGyoushaName"].Value = dto != null ? dto.GENBA_NAME1 + dto.GENBA_NAME2 : string.Empty;
                }
                else
                {
                    // 業者名1 + 業者名2を設定する
                    var gyousha = this.gyoushaDao.GetDataByCd(gyoushaCd);                    

                    // 排出事業者の氏名又は名称を表示する
                    this.form.grdIchiran.Rows[index]["HstGyoushaName"].Value = gyousha != null ? gyousha.GYOUSHA_NAME1 + gyousha.GYOUSHA_NAME2 : string.Empty;

                }
                if (this.form.grdIchiran.Rows[index]["HST_GYOUSHA_CD"].Value != null
                    && this.form.grdIchiran.Rows[index]["HST_GENBA_CD"].Value != null)
                {
                    dto.GYOUSHA_CD = this.form.grdIchiran.Rows[index]["HST_GYOUSHA_CD"].Value.ToString();
                    dto.GENBA_CD = this.form.grdIchiran.Rows[index]["HST_GENBA_CD"].Value.ToString();
                    dto = this.genbaDao.GetDataByCd(dto);
                }

                if (dto != null)
                {
                    this.form.grdIchiran.Rows[index]["HST_GENBA_CHIIKI_CD"].Value = dto.CHIIKI_CD;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba_PopupAfter", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }

        /// <summary>
        /// 現場パップアップ後
        /// </summary>
        /// <param name="index"></param>
        internal void SbnGensha_PopupAfter(int index)
        {
            try
            {
                M_GENBA dto = new M_GENBA();

                // 処分事業者
                dto.GYOUSHA_CD = this.form.grdIchiran.Rows[index]["SBN_GYOUSHA_CD"].Value.ToString();
                // 処分事業場
                dto.GENBA_CD = this.form.grdIchiran.Rows[index]["SBN_GENBA_CD"].Value.ToString();
                dto = this.genbaDao.GetDataByCd(dto);

                if (dto != null)
                {
                    this.form.grdIchiran.Rows[index]["SBN_GENBA_CHIIKI_CD"].Value = dto.CHIIKI_CD;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SbnGensha_PopupAfter", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }

        /// <summary>
        /// 許可番号を取得する
        /// </summary>
        /// <param name="index"></param>
        internal void Itakusaki_PopupAfter(int index)
        {
            try
            {
                M_GENBA dto = new M_GENBA();

                if (this.form.grdIchiran.Rows[index]["GyoshaCd"].Value != null
                    && this.form.grdIchiran.Rows[index]["GenbaCd"].Value != null)
                {
                    dto.GYOUSHA_CD = this.form.grdIchiran.Rows[index]["GyoshaCd"].Value.ToString();
                    dto.GENBA_CD = this.form.grdIchiran.Rows[index]["GenbaCd"].Value.ToString();
                    dto = this.genbaDao.GetDataByCd(dto);

                    // 住所用の都道府県を取得
                    M_TODOUFUKEN todofuken = new M_TODOUFUKEN();

                    if (dto != null)
                    {
                        todofuken = this.todofukenDao.GetDataByCd(dto.GENBA_TODOUFUKEN_CD.ToString());
                    }

                    this.form.grdIchiran.Rows[index]["ItakusakiName"].Value = dto != null ? dto.GENBA_NAME1 + dto.GENBA_NAME2 : string.Empty;
                    this.form.grdIchiran.Rows[index]["ItakudakiAddress"].Value = dto != null ? todofuken.TODOUFUKEN_NAME + dto.GENBA_ADDRESS1 + dto.GENBA_ADDRESS2 : string.Empty;
                }

                // 許可区分
                var kbn = 0;
                // 処分
                if (dto != null && dto.SHOBUN_NIOROSHI_GENBA_KBN)
                {
                    kbn = 2;
                }
                // 最終処分
                if (dto != null && dto.SAISHUU_SHOBUNJOU_KBN)
                {
                    kbn = 3;
                }

                // 自社区分＝Trueと（処分区分＝TrueOr最終処分区分＝True）の場合
                if (dto != null && kbn != 0 && dto.JISHA_KBN)
                {
                    M_CHIIKIBETSU_KYOKA data = new M_CHIIKIBETSU_KYOKA();
                    data.KYOKA_KBN = (short)kbn;
                    data.GYOUSHA_CD = dto.GYOUSHA_CD;
                    data.GENBA_CD = dto.GENBA_CD;
                    data.CHIIKI_CD = dto.CHIIKI_CD;

                    data = this.chikiBetsuDao.GetDataByPrimaryKey(data);

                    // 特別管理区分は「産業廃棄物」の場合
                    if (data != null && this.form.TokuKbn.Text == "産業廃棄物")
                    {
                        this.form.grdIchiran.Rows[index]["ItakusakiKyokaNO"].Value = data.FUTSUU_KYOKA_NO;
                    }
                    // 特別管理区分は「特別管理産業廃棄物」場合
                    if (data != null && this.form.TokuKbn.Text == "特別管理産業廃棄物")
                    {
                        this.form.grdIchiran.Rows[index]["ItakusakiKyokaNO"].Value = data.TOKUBETSU_KYOKA_NO;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Itakusaki_PopupAfter", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
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

        //#region マスタ検索処理
        ///// <summary>
        ///// 地域別分類検索処理
        ///// </summary>
        ///// <param name="chiikiCd">chiikiCd</param>
        ///// <param name="cd">CD</param>
        //internal M_CHIIKIBETSU_BUNRUI[] GetBunrui(string chiikiCd, string cd)
        //{
        //    LogUtility.DebugMethodStart(chiikiCd, cd);
        //    M_CHIIKIBETSU_BUNRUI dto = new M_CHIIKIBETSU_BUNRUI();
        //    dto.HOUKOKUSHO_BUNRUI_CD = cd;
        //    dto.CHIIKI_CD = chiikiCd;
        //    IM_CHIIKIBETSU_BUNRUIDao dao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_BUNRUIDao>();
        //    M_CHIIKIBETSU_BUNRUI[] results = dao.GetAllValidData(dto);
        //    LogUtility.DebugMethodEnd();
        //    return results;
        //}
        //#endregion

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