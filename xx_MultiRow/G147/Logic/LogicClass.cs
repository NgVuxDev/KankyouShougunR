using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ElectronicManifest.MihimodukeIchiran.APP;
using Shougun.Core.ElectronicManifest.MihimodukeIchiran.Const;
using Shougun.Core.ElectronicManifest.MihimodukeIchiran.DAO;
using Shougun.Core.ElectronicManifest.MihimodukeIchiran.DTO;
using r_framework.CustomControl;
using Seasar.Framework.Exceptions;
using Microsoft.VisualBasic;

namespace Shougun.Core.ElectronicManifest.MihimodukeIchiran.Logic
{
	/// <summary>
	/// ビジネスロジック
	/// </summary>
	internal class LogicClass : IBuisinessLogic
	{
		#region フィールド
		/// <summary>
		/// 未紐付一覧画面Form
		/// </summary>
		private MihimodukeIchiran.APP.UIForm form;

		/// <summary>
		/// ParentForm
		/// </summary>
		private BusinessBaseForm parentForm;

        ///<summary>
        ///未紐付一覧のDao
        ///</summary>
        private DMTDaoCls DMTDao;

        ///<summary>
        ///業者マスタのDao
        ///</summary>
        private GYOUSHADaoCls GYOUSHADao;

        ///<summary>
        ///業場マスタのDao
        ///</summary>
        private GENBADaoCls GENBADao;

        ///<summary>
        ///処分方法マスタのDao
        ///</summary>
        private SHOBUNHDaoCls SHOBUNHDao;

        ///<summary>
        ///電子事業者マスタのDao
        ///</summary>
        private MJSDaoCls MJSDao;

        ///<summary>
        ///電子事業場マスタのDao
        ///</summary>
        private MJJDaoCls MJJDao;

        /// <summary>
        /// 電子廃棄物名称
        /// </summary>
        private MHNDaoCls MHNDao;

        /// <summary>
        /// 電子マニフェスト基本情報のDao
        /// </summary>
        private DT_R18DaoCls R18Dao;

        /// <summary>
        /// 電子マニフェスト基本拡張のDao
        /// </summary>
        private R18EXDaoCls R18EXDao;

        /// <summary>
        /// 電子マニフェスト収集運搬拡張のDao
        /// </summary>
        private R19EXDaoCls R19EXDao;

        /// <summary>
        /// 電子マニフェスト最終処分(予定)拡張のDao
        /// </summary>
        private R04EXDaoCls R04EXDao;

        /// <summary>
        /// 電子マニフェスト一次マニフェスト情報拡張のDao
        /// </summary>
        private R08EXDaoCls R08EXDao;

        /// <summary>
        /// 電子マニフェスト最終処分拡張のDao
        /// </summary>
        private R13EXDaoCls R13EXDao;

        /// <summary>
        /// フィルタ加入者番号
        /// </summary>
        private ImportMemberFilterDaoClas ImportMemberFilterDao;

        ///<summary>
        ///未紐付一覧のDao
        ///</summary>
        private DTOClass searchCondition;

        /// <summary>
        /// 電子廃棄物種類Dao
        /// </summary>
        private IM_DENSHI_HAIKI_SHURUIDao haikiShuruiDao;

        /// <summary>
        /// 電子廃棄物種類細分類Dao
        /// </summary>
        private IM_DENSHI_HAIKI_SHURUI_SAIBUNRUIDao haikiShuruiSaibunruiDao;

        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int titleMaxWidth = 500;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        internal MessageBoxShowLogic errmessage;
        #endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="targetForm">対象フォーム</param>
		public LogicClass(UIForm targetForm)
		{
			LogUtility.DebugMethodStart(targetForm);

			// フィールドの初期化
			this.form = targetForm;
            this.DMTDao = DaoInitUtility.GetComponent<DMTDaoCls>();
            this.GYOUSHADao = DaoInitUtility.GetComponent<GYOUSHADaoCls>();
            this.GENBADao = DaoInitUtility.GetComponent<GENBADaoCls>();
            this.SHOBUNHDao = DaoInitUtility.GetComponent<SHOBUNHDaoCls>();
            this.MJSDao = DaoInitUtility.GetComponent<MJSDaoCls>();
            this.MJJDao = DaoInitUtility.GetComponent<MJJDaoCls>();
            this.MHNDao = DaoInitUtility.GetComponent<MHNDaoCls>();
            this.R18Dao = DaoInitUtility.GetComponent<DT_R18DaoCls>();
            this.R18EXDao = DaoInitUtility.GetComponent<R18EXDaoCls>();
            this.R19EXDao = DaoInitUtility.GetComponent<R19EXDaoCls>();
            this.R04EXDao = DaoInitUtility.GetComponent<R04EXDaoCls>();
            this.R08EXDao = DaoInitUtility.GetComponent<R08EXDaoCls>();
            this.R13EXDao = DaoInitUtility.GetComponent<R13EXDaoCls>();
            this.ImportMemberFilterDao = DaoInitUtility.GetComponent<ImportMemberFilterDaoClas>();
            this.haikiShuruiSaibunruiDao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUI_SAIBUNRUIDao>();
            this.haikiShuruiDao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUIDao>();

            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.searchCondition = new DTOClass();
            this.errmessage = new MessageBoxShowLogic();

			LogUtility.DebugMethodEnd();
		}

		/// <summary>
		/// 画面初期化処理
		/// </summary>
        public bool WindowInit()
		{
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ParentFormのSet
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 画面表示項目初期化
                this.form.cdate_HikiwatasiBiFrom.Value = this.parentForm.sysDate;
                this.form.cdate_HikiwatasiBiTo.Value = this.parentForm.sysDate;

                // タイトルラベル範囲調整
                var header = (HeaderBaseForm)this.parentForm.headerForm;
                ControlUtility.AdjustTitleSize(header.lb_title, this.titleMaxWidth);
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
		}

		#region 検索/登録/更新/削除
		/// <summary>
		/// 検索処理
		/// </summary>
		public int Search()
		{
            LogUtility.DebugMethodStart();
            Cursor.Current = Cursors.WaitCursor;
            MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
            int count = 0;
            try
            {
                string hikiwatashiDate = null;
                // SQL文
                DataTable searchResult = new DataTable();
                this.searchCondition = new DTOClass();

                if (this.form.Ichiran.RowCount > 0)
                {
                    this.form.Ichiran.Rows.Clear();
                }

                #region 検索条件設定
                if (this.form.cdate_HikiwatasiBiFrom.Value != null)
                {
                    this.searchCondition.hikiWatashiDateFrom
                        = this.form.cdate_HikiwatasiBiFrom.Value.ToString().Substring(0, 10).Replace("/", "");
                }

                if (this.form.cdate_HikiwatasiBiTo.Value != null)
                {
                    this.searchCondition.hikiWatashiDateTo
                        = this.form.cdate_HikiwatasiBiTo.Value.ToString().Substring(0, 10).Replace("/", "");
                }

                if (!string.IsNullOrEmpty(this.form.cntxt_ManifestIdFrom.Text))
                {
                    this.searchCondition.manifestIdFrom = this.form.cntxt_ManifestIdFrom.Text;
                }

                if (!string.IsNullOrEmpty(this.form.cntxt_ManifestIdTo.Text))
                {
                    this.searchCondition.manifestIdTo = this.form.cntxt_ManifestIdTo.Text;
                }

                // マスタ設定
                int masterSettingCond = 0;
                // 初期化
                this.searchCondition.dataCondition = 3;

                if (!string.IsNullOrEmpty(this.form.MasterSettingConditionValue.Text)
                    && int.TryParse(this.form.MasterSettingConditionValue.Text.ToString(), out masterSettingCond))
                {
                    this.searchCondition.masterSettingCondition = masterSettingCond;
                }

                // データ作成
                int dataCond = 0;
                // 初期化
                this.searchCondition.dataCondition = 3;

                if (!string.IsNullOrEmpty(this.form.DataConditionValue.Text)
                    && int.TryParse(this.form.DataConditionValue.Text.ToString(), out dataCond))
                {
                    this.searchCondition.dataCondition = dataCond;
                }

                // 排出事業者
                if (!string.IsNullOrEmpty(this.form.HstJigyoushaCd.Text))
                {
                    this.searchCondition.hstEdiMemberId = this.form.HstJigyoushaCd.Text;
                }

                // 排出事業場
                if (!string.IsNullOrEmpty(this.form.HstJigyoujouCd.Text)
                    && !string.IsNullOrEmpty(this.searchCondition.hstEdiMemberId))
                {
                    // 住所をを取得しセットする
                    var jigyoujouDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                    var seachCond = new M_DENSHI_JIGYOUJOU() { EDI_MEMBER_ID = this.searchCondition.hstEdiMemberId, JIGYOUJOU_CD = this.form.HstJigyoujouCd.Text };
                    var denshiJigyoujou = jigyoujouDao.GetAllValidData(seachCond);
                    if (denshiJigyoujou != null
                        && denshiJigyoujou.Count() > 0)
                    {
                        this.searchCondition.hstJouAddress = this.CreateJigyoujouAddress(denshiJigyoujou[0]);
                    }
                }

                if (!string.IsNullOrEmpty(this.form.HstJigyoujouName.Text))
                {
                    this.searchCondition.hstJouName = this.form.HstJigyoujouName.Text;
                }

                // 収取運搬業者
                if (!string.IsNullOrEmpty(this.form.UpnJigyoushaCd.Text))
                {
                    this.searchCondition.upnEdiMemberId = this.form.UpnJigyoushaCd.Text;
                }

                // 処分事業者
                if (!string.IsNullOrEmpty(this.form.SbnJigyoushaCd.Text))
                {
                    this.searchCondition.sbnEdiMemberId = this.form.SbnJigyoushaCd.Text;
                }

                // 処分事業場
                if (!string.IsNullOrEmpty(this.form.SbnJigyoujouCd.Text)
                    && !string.IsNullOrEmpty(this.searchCondition.sbnEdiMemberId))
                {
                    // 住所をを取得しセットする
                    var jigyoujouDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                    var seachCond = new M_DENSHI_JIGYOUJOU() { EDI_MEMBER_ID = this.searchCondition.sbnEdiMemberId, JIGYOUJOU_CD = this.form.SbnJigyoujouCd.Text };
                    var denshiJigyoujou = jigyoujouDao.GetAllValidData(seachCond);
                    if (denshiJigyoujou != null
                        && denshiJigyoujou.Count() > 0)
                    {
                        this.searchCondition.sbnJouAddress = this.CreateJigyoujouAddress(denshiJigyoujou[0]);
                    }
                }

                if (!string.IsNullOrEmpty(this.form.SbnJigyoujouName.Text))
                {
                    this.searchCondition.sbnJouName = this.form.SbnJigyoujouName.Text;
                }

                // 最終処分事業者
                if (!string.IsNullOrEmpty(this.form.LastSbnJigyoushaCd.Text))
                {
                    this.searchCondition.lastSbnEdiMemberId = this.form.LastSbnJigyoushaCd.Text;
                }

                // 最終処分事業場
                if (!string.IsNullOrEmpty(this.form.LastSbnJigyoujouCd.Text)
                    && !string.IsNullOrEmpty(this.searchCondition.lastSbnEdiMemberId))
                {
                    // 住所をを取得しセットする
                    var jigyoujouDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                    var seachCond = new M_DENSHI_JIGYOUJOU() { EDI_MEMBER_ID = this.searchCondition.lastSbnEdiMemberId, JIGYOUJOU_CD = this.form.LastSbnJigyoujouCd.Text };
                    var denshiJigyoujou = jigyoujouDao.GetAllValidData(seachCond);
                    if (denshiJigyoujou != null
                        && denshiJigyoujou.Count() > 0)
                    {
                        this.searchCondition.lastSbnJouAddress = this.CreateJigyoujouAddress(denshiJigyoujou[0]);
                    }
                }

                if (!string.IsNullOrEmpty(this.form.LastSbnJigyoujouName.Text))
                {
                    this.searchCondition.lastSbnJouName = this.form.LastSbnJigyoujouName.Text;
                }
                #endregion

                // ヘッダーのチェックボックス解除
                this.form.Ichiran.ColumnHeaders["columnHeaderSection1"].Cells["CHECK_LABEL"].Value = false; 

                var tempData = this.DMTDao.GetDataForEntity(this.searchCondition);

                ///////////////////////
                tempData.Columns["MATCH_MEMBER_ID_FLG"].ReadOnly = false;
                ///////////////////////
                // 電マニ代行登録用処理
                var sysInfos = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData();
                var sysInfo = sysInfos != null && sysInfos.Count() > 0 && !sysInfos[0].COPY_MODE.IsNull ? sysInfos[0].COPY_MODE : 0;

                /*
                 *   0:フィルタ条件に関係なくデータ取得
                 *   1:フィルタ条件に該当するデータのみ取得
                 *   2:フィルタ条件に該当しないデータのみ取得
                 */
                if (sysInfo > 0)
                {
                    // フィルターデータ取得
                    DataTable filterDt = this.ImportMemberFilterDao.GetAllData();

                    // 削除 or 対象レコードのKanriID
                    List<string> targetKanriID = new List<string>();

                    // 対象データのLOOP
                    foreach (DataRow row in tempData.Rows)
                    {
                        // フィルターデータのLOOP
                        foreach (DataRow frow in filterDt.Rows)
                        {
                            string[] array = frow[0].ToString().Split(',');

                            // 空白を除いたフィルターデータの加入者番号の数
                            int cnt = array.Where(x => x != "").ToArray().Length;

                            string[] rowArray = new string[] {row["HST_SHA_EDI_MEMBER_ID"].ToString(),
                                                              row["UPN1_MEMBER_ID"].ToString(),
                                                              row["UPN2_MEMBER_ID"].ToString(),
                                                              row["UPN3_MEMBER_ID"].ToString(),
                                                              row["UPN4_MEMBER_ID"].ToString(),
                                                              row["UPN5_MEMBER_ID"].ToString(),
                                                              row["SBN_MEMBER_ID"].ToString()};
                            // 空白を除いた対象データの加入者番号の数
                            int rowCnt = rowArray.Where(x => x != "").ToArray().Length;
                            int matchCnt = 0;

                            // フィルターデータ(行)の1~5のLOOP
                            foreach (string a in array)
                            {
                                if (string.IsNullOrEmpty(a))
                                {
                                    continue;
                                }

                                if (a == row["HST_MEMBER_ID"].ToString())
                                {
                                    matchCnt++;
                                    continue;
                                }
                                if (a == row["UPN1_MEMBER_ID"].ToString())
                                {
                                    matchCnt++;
                                    continue;
                                }
                                if (a == row["UPN2_MEMBER_ID"].ToString())
                                {
                                    matchCnt++;
                                    continue;
                                }
                                if (a == row["UPN3_MEMBER_ID"].ToString())
                                {
                                    matchCnt++;
                                    continue;
                                }
                                if (a == row["UPN4_MEMBER_ID"].ToString())
                                {
                                    matchCnt++;
                                    continue;
                                }
                                if (a == row["UPN5_MEMBER_ID"].ToString())
                                {
                                    matchCnt++;
                                    continue;
                                }
                                if (a == row["SBN_MEMBER_ID"].ToString())
                                {
                                    matchCnt++;
                                    continue;
                                }
                            }
                            if (matchCnt == cnt)
                            {
                                // 除外 or 取込み対象データ
                                targetKanriID.Add(row["KANRI_ID"].ToString());
                                row["MATCH_MEMBER_ID_FLG"] = true;
                            }
                        }
                    }

                    // 条件作成
                    targetKanriID = targetKanriID.Distinct().ToList();
                    //StringBuilder sbFilter = new StringBuilder();
                    //string sign = string.Empty;
                    //string con = string.Empty;
                    //string filterStr = "KANRI_ID";
                    //if (sysInfo == 1)
                    //{
                    //    sign = " = ";
                    //    con = " OR ";
                    //    if (targetKanriID != null && targetKanriID.Count > 0)
                    //    {
                    //        foreach (string s in targetKanriID)
                    //        {
                    //            sbFilter.Append(filterStr).Append(sign)
                    //              .Append("'").Append(s).Append("'").Append(con);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        // targetKanriIDが0件の場合、有無を言わさずデータ更新しない
                    //        // 絞り込み結果が0件になるようにダミーの絞り込みを追加
                    //        sbFilter.Append("0 = 1");
                    //    }
                    //}
                    //else
                    //{
                    //    sign = " <> ";
                    //    con = " AND ";
                    //    foreach (string s in targetKanriID)
                    //    {
                    //        sbFilter.Append(filterStr).Append(sign)
                    //          .Append("'").Append(s).Append("'").Append(con);
                    //    }
                    //}
                    searchResult = tempData.Clone();

                    if (sysInfo == 1)
                    {
                        var exist = tempData.AsEnumerable().Where(r => (bool)r["MATCH_MEMBER_ID_FLG"] == true);
                        if (exist.Any())
                        {
                            searchResult = exist.CopyToDataTable();
                        }
                    }
                    else
                    {
                        var exist = tempData.AsEnumerable().Where(r => (bool)r["MATCH_MEMBER_ID_FLG"] == false);
                        if (exist.Any())
                        {
                            searchResult = exist.CopyToDataTable();
                        }
                    }

                    // DataTableから抽出
                    //searchResult = tempData.Clone();
                    //DataRow[] rows = tempData.Select(sbFilter.ToString().TrimEnd(con.ToCharArray()));
                    //foreach (DataRow row in rows)
                    //{
                    //    searchResult.ImportRow(row);
                    //}
                }
                else
                {
                    searchResult = tempData;
                }

                count = searchResult.Rows.Count;

                if (count == 0)
                {
                    messageShowLogic.MessageBoxShow("C001");
                    return count;
                }

                this.form.IchiranData = searchResult;
                
                for (int i = 0; i < count; i++)
                {
                    this.form.Ichiran.Rows.Add();

                    // 引渡し日
                    if (searchResult.Rows[i]["HIKIWATASHI_DATE"] != System.DBNull.Value
                        && !string.IsNullOrEmpty(searchResult.Rows[i]["HIKIWATASHI_DATE"].ToString()))
                    {
                        hikiwatashiDate = searchResult.Rows[i]["HIKIWATASHI_DATE"].ToString();
                        this.form.Ichiran.Rows[i].Cells["HIKIWATASHI_DATE"].Value = hikiwatashiDate.Substring(0, 4)
                            + "/" + hikiwatashiDate.Substring(4, 2) + "/" + hikiwatashiDate.Substring(6, 2)
                            + "(" + searchResult.Rows[i]["HIKIWATASHI_DAY"].ToString() + ")";
                    }

                    // 排出事業者
                    this.form.Ichiran.Rows[i].Cells["HAISHUTSU_JIGYOUSHA"].Value = searchResult.Rows[i]["HST_SHA_NAME"];
                    // 排出事業場
                    this.form.Ichiran.Rows[i].Cells["HAISHUTSU_JIGYOUJOU"].Value = searchResult.Rows[i]["HST_JOU_NAME"];

                    // 収集運搬業者1
                    this.form.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA1"].Value = searchResult.Rows[i]["SU1_UPN_SHA_NAME"];
                    if (Convert.ToInt32(searchResult.Rows[i]["UPN_ROUTE_CNT"]) > 1)
                    {
                        // 運搬先業者1
                        this.form.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA1"].Value = searchResult.Rows[i]["SU1_UPNSAKI_NAME"];
                        // 運搬先事業場1
                        this.form.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU1"].Value = searchResult.Rows[i]["SU1_UPNSAKI_JOU_NAME"];
                    }

                    // 収集運搬業者2
                    this.form.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA2"].Value = searchResult.Rows[i]["SU2_UPN_SHA_NAME"];
                    if (Convert.ToInt32(searchResult.Rows[i]["UPN_ROUTE_CNT"]) > 2)
                    {
                        // 運搬先業者2
                        this.form.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA2"].Value = searchResult.Rows[i]["SU2_UPNSAKI_NAME"];
                        // 運搬先事業場2
                        this.form.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU2"].Value = searchResult.Rows[i]["SU2_UPNSAKI_JOU_NAME"];
                    }

                    // マニフェスト番号
                    this.form.Ichiran.Rows[i].Cells["MANIFEST_ID"].Value = searchResult.Rows[i]["MANIFEST_ID"];

                    // 収集運搬業者3
                    this.form.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA3"].Value = searchResult.Rows[i]["SU3_UPN_SHA_NAME"];
                    if (Convert.ToInt32(searchResult.Rows[i]["UPN_ROUTE_CNT"]) > 3)
                    {
                        // 運搬先業者3
                        this.form.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA3"].Value = searchResult.Rows[i]["SU3_UPNSAKI_NAME"];
                        // 運搬先事業場3
                        this.form.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU3"].Value = searchResult.Rows[i]["SU3_UPNSAKI_JOU_NAME"];
                    }

                    // 収集運搬業者4
                    this.form.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA4"].Value = searchResult.Rows[i]["SU4_UPN_SHA_NAME"];
                    if (Convert.ToInt32(searchResult.Rows[i]["UPN_ROUTE_CNT"]) > 4)
                    {
                        // 運搬先業者4
                        this.form.Ichiran.Rows[i].Cells["UNPANSAKI_GYOUSHA4"].Value = searchResult.Rows[i]["SU4_UPNSAKI_NAME"];
                        // 運搬先事業場4
                        this.form.Ichiran.Rows[i].Cells["UNPANSAKI_JIGYOUJOU4"].Value = searchResult.Rows[i]["SU4_UPNSAKI_JOU_NAME"];
                    }
                        
                    // 収集運搬業者5
                    this.form.Ichiran.Rows[i].Cells["SHUSHU_UNPAN_GYOUSHA5"].Value = searchResult.Rows[i]["SU5_UPN_SHA_NAME"];

                    // 処分業者
                    this.form.Ichiran.Rows[i].Cells["SHOBUN_GYOUSHA"].Value = searchResult.Rows[i]["SB_UPNSAKI_NAME"];
                    // 処分事業場
                    this.form.Ichiran.Rows[i].Cells["SHOBUN_JIGYOUJOU"].Value = searchResult.Rows[i]["SB_UPNSAKI_JOU_NAME"];

                    // 作成
                    if (searchResult.Rows[i]["EX_KANRI_ID"] == DBNull.Value
                        || string.IsNullOrEmpty(searchResult.Rows[i]["EX_KANRI_ID"].ToString()))
                    {
                        this.form.Ichiran.Rows[i].Cells["DATA_CONDITION"].Value = "未";
                    }
                    else
                    {
                        this.form.Ichiran.Rows[i].Cells["DATA_CONDITION"].Value = "済";
                    }
                }

                //背景色設定
                this.form.SetIchiranBackColor();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                count = -1;
            }

            if (this.form.Ichiran.RowCount > 0)
            {
                this.form.Ichiran.Rows[0].Cells[ConstCls.CELL_NAME_CHECK].Selected = true;
            }
            Cursor.Current = Cursors.Default;

            LogUtility.DebugMethodEnd(count);
            return count;
		}

        /// <summary>
		/// 登録処理
		/// </summary>
        public void Regist(bool errorFlag)
		{
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

            try
            {
                if (!this.IsNullOrEmpty(this.form.cantxt_JigyoujouCd.Text))
                {
                    if (!string.IsNullOrEmpty(this.form.cantxt_GenbaCd.Text)
                        && this.form.cantxt_GenbaCd.ReadOnly == false)
                    {
                        // 電子事業場
                        M_DENSHI_JIGYOUJOU entity = new M_DENSHI_JIGYOUJOU();

                        // 加入者番号
                        entity.EDI_MEMBER_ID = this.form.cantxt_JigyoushaCd.Text;
                        // 事業場CD
                        entity.JIGYOUJOU_CD = this.form.cantxt_JigyoujouCd.Text;
                        // 業者CD
                        entity.GYOUSHA_CD = this.form.cantxt_GyoushaCd.Text;
                        // 現場CD
                        entity.GENBA_CD = this.form.cantxt_GenbaCd.Text;

                        var dataBinderEntry = new DataBinderLogic<M_DENSHI_JIGYOUJOU>(entity);
                        dataBinderEntry.SetSystemProperty(entity, false);

                        // タイムスタンプ
                        entity.TIME_STAMP = ConvertStrByte.In32ToByteArray(this.form.TimestampGenba);

                        MJJDao.Update(entity);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.form.cantxt_GyoushaCd.Text)
                        && this.form.cantxt_GyoushaCd.ReadOnly == false)
                    {
                        // 電子事業者
                        M_DENSHI_JIGYOUSHA entity = new M_DENSHI_JIGYOUSHA();

                        // 加入者番号
                        entity.EDI_MEMBER_ID = this.form.cantxt_JigyoushaCd.Text;
                        // 業者CD
                        entity.GYOUSHA_CD = this.form.cantxt_GyoushaCd.Text;

                        var dataBinderEntry = new DataBinderLogic<M_DENSHI_JIGYOUSHA>(entity);
                        dataBinderEntry.SetSystemProperty(entity, false);

                        // タイムスタンプ
                        entity.TIME_STAMP = ConvertStrByte.In32ToByteArray(this.form.TimestampGyousha);

                        MJSDao.Update(entity);
                    }
                }

                messageShowLogic.MessageBoxShow("I001", "登録");
                this.form.Ichiran.Rows[0].Cells[ConstCls.CELL_NAME_CHECK].Selected = true;
                this.form.Ichiran.Rows.Clear();
                this.Search();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.errmessage.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.errmessage.MessageBoxShow("E093", "");
                }
                else
                {
                    this.errmessage.MessageBoxShow("E245", "");
                }
            }

            LogUtility.DebugMethodEnd();
		}

        /// <summary>
        /// 事業者登録
        /// </summary>
        [Transaction]
        public virtual void JigyoushaToroku()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (this.form.IchiranData == null || this.form.IchiranData.Rows.Count == 0)
            {
                msgLogic.MessageBoxShow("E061");
                return;
            }

            // キー:加入者番号
            var dic = new Dictionary<string, M_DENSHI_JIGYOUSHA>();
            var updateData = new Dictionary<string, M_DENSHI_JIGYOUSHA>();

            try
            {
                DataTable jigyoushaDt = MJSDao.GetDataForEntity("SELECT DISTINCT EDI_MEMBER_ID, HST_KBN, UPN_KBN, SBN_KBN FROM M_DENSHI_JIGYOUSHA ORDER BY EDI_MEMBER_ID");
                var fmDenshiJigyoushaDao = DaoInitUtility.GetComponent<DenshiJigyoujouDaoCls>();

                // 排出事業場
                {
                    // 画面の一覧から排出事業場を取得
                    var groupRow = this.form.IchiranData.AsEnumerable().Where(n => !this.IsNullOrEmpty(n["KANRI_ID"]) && this.IsNullOrEmpty(n["EDI_MEMBER_ID"]))
                                                                       .ToLookup(n => n["HST_SHA_EDI_MEMBER_ID"])
                                                                       .ToList();

                    foreach (var row in groupRow)
                    {
                        var jigyousha = row.FirstOrDefault();
                        if (jigyousha == null 
                            || this.IsNullOrEmpty(jigyousha["HST_SHA_EDI_MEMBER_ID"])
                            || jigyoushaDt.AsEnumerable().Any(n => n["EDI_MEMBER_ID"].ToString().Equals(jigyousha["HST_SHA_EDI_MEMBER_ID"])))
                        {
                            continue;
                        }

                        if (dic.ContainsKey(jigyousha["HST_SHA_EDI_MEMBER_ID"].ToString()))
                        {
                            continue;
                        }

                        M_DENSHI_JIGYOUSHA entity = new M_DENSHI_JIGYOUSHA();
                        entity.EDI_MEMBER_ID = jigyousha["HST_SHA_EDI_MEMBER_ID"].ToString();
                        entity.EDI_PASSWORD = string.Empty;
                        entity.JIGYOUSHA_NAME = jigyousha["HST_SHA_NAME"].ToString();
                        entity.JIGYOUSHA_POST = jigyousha["HST_SHA_POST"].ToString();
                        entity.JIGYOUSHA_ADDRESS1 = jigyousha["HST_SHA_ADDRESS1"].ToString();
                        entity.JIGYOUSHA_ADDRESS2 = jigyousha["HST_SHA_ADDRESS2"].ToString();
                        entity.JIGYOUSHA_ADDRESS3 = jigyousha["HST_SHA_ADDRESS3"].ToString();
                        entity.JIGYOUSHA_ADDRESS4 = jigyousha["HST_SHA_ADDRESS4"].ToString();
                        entity.JIGYOUSHA_TEL = jigyousha["HST_SHA_TEL"].ToString();
                        entity.JIGYOUSHA_FAX = jigyousha["HST_SHA_FAX"].ToString();
                        entity.UPN_KBN = false;
                        if (!string.IsNullOrEmpty(entity.EDI_MEMBER_ID)
                            && (entity.EDI_MEMBER_ID.StartsWith(ConstCls.EDI_MEMBER_ID_SHOBUN_3)
                            || entity.EDI_MEMBER_ID.StartsWith(ConstCls.EDI_MEMBER_ID_SHOBUN_D3)))
                        {
                            entity.HST_KBN = false;
                            entity.SBN_KBN = true;
                        }
                        else
                        {
                            entity.HST_KBN = true;
                            entity.SBN_KBN = false;
                        }
                        entity.HOUKOKU_HUYOU_KBN = false;
                        entity.GYOUSHA_CD = string.Empty;

                        dic.Add(jigyousha["HST_SHA_EDI_MEMBER_ID"].ToString(), entity);
                    }
                }

                // 収集運搬業者1～5
                for (int i = 1; i < 6; i++)
                {
                    // カラム名
                    string ediMemberId = "SU" + i + "_UPN_SHA_EDI_MEMBER_ID";
                    string jigyoushaName = "SU" + i + "_UPN_SHA_NAME";
                    string jigyoushaPost = "SU" + i + "_UPN_SHA_POST";
                    string jigyoushaAddress1 = "SU" + i + "_UPN_SHA_ADDRESS1";
                    string jigyoushaAddress2 = "SU" + i + "_UPN_SHA_ADDRESS2";
                    string jigyoushaAddress3 = "SU" + i + "_UPN_SHA_ADDRESS3";
                    string jigyoushaAddress4 = "SU" + i + "_UPN_SHA_ADDRESS4";
                    string jigyoushaTel = "SU" + i + "_UPN_SHA_TEL";
                    string jigyoushaFax = "SU" + i + "_UPN_SHA_FAX";

                    // 画面の一覧から収集運搬業者を取得
                    var groupRow = this.form.IchiranData.AsEnumerable().Where(n => !this.IsNullOrEmpty(n["SU" + i + "_KANRI_ID"]) && this.IsNullOrEmpty(n["SU" + i + "_EDI_MEMBER_ID"]))
                                                                       .ToLookup(n => n[ediMemberId])
                                                                       .ToList();

                    foreach (var row in groupRow)
                    {
                        var jigyousha = row.FirstOrDefault();
                        if (jigyousha == null 
                            || this.IsNullOrEmpty(jigyousha[ediMemberId])
                            || jigyoushaDt.AsEnumerable().Any(n => n["EDI_MEMBER_ID"].ToString().Equals(jigyousha[ediMemberId]) && (bool)n["UPN_KBN"]))
                        {
                            continue;
                        }

                        if (dic.ContainsKey(jigyousha[ediMemberId].ToString()))
                        {
                            // 運搬業者区分のみ更新
                            dic[jigyousha[ediMemberId].ToString()].UPN_KBN = true;
                            continue;
                        }

                        if (!jigyoushaDt.AsEnumerable().Any(n => n["EDI_MEMBER_ID"].ToString().Equals(jigyousha[ediMemberId])))
                        {
                            // 新規登録
                            M_DENSHI_JIGYOUSHA entity = new M_DENSHI_JIGYOUSHA();
                            entity.EDI_MEMBER_ID = jigyousha[ediMemberId].ToString();
                            entity.EDI_PASSWORD = string.Empty;
                            entity.JIGYOUSHA_NAME = jigyousha[jigyoushaName].ToString();
                            entity.JIGYOUSHA_POST = jigyousha[jigyoushaPost].ToString();
                            entity.JIGYOUSHA_ADDRESS1 = jigyousha[jigyoushaAddress1].ToString();
                            entity.JIGYOUSHA_ADDRESS2 = jigyousha[jigyoushaAddress2].ToString();
                            entity.JIGYOUSHA_ADDRESS3 = jigyousha[jigyoushaAddress3].ToString();
                            entity.JIGYOUSHA_ADDRESS4 = jigyousha[jigyoushaAddress4].ToString();
                            entity.JIGYOUSHA_TEL = jigyousha[jigyoushaTel].ToString();
                            entity.JIGYOUSHA_FAX = jigyousha[jigyoushaFax].ToString();
                            entity.HST_KBN = false;
                            entity.UPN_KBN = true;
                            entity.SBN_KBN = false;
                            entity.HOUKOKU_HUYOU_KBN = false;
                            entity.GYOUSHA_CD = string.Empty;

                            dic.Add(jigyousha[ediMemberId].ToString(), entity);
                        }
                        else if (jigyoushaDt.AsEnumerable().Any(n => n["EDI_MEMBER_ID"].ToString().Equals(jigyousha[ediMemberId]) && !(bool)n["UPN_KBN"])
                            && !updateData.ContainsKey(jigyousha[ediMemberId].ToString()))
                        {
                            // 既に電子事業者マスタに存在する場合は、運搬業者区分のみ更新
                            var latestJigyousha = fmDenshiJigyoushaDao.GetDataByCd(jigyousha[ediMemberId].ToString());
                            latestJigyousha.UPN_KBN = true;
                            updateData.Add(latestJigyousha.EDI_MEMBER_ID, latestJigyousha);
                        }
                    }
                }

                // 運搬先業者1～4(5は処分業者)
                for (int i = 1; i < 5; i++)
                {
                    // 自己積替の対策
                    // カラム名
                    string ediMemberId = "SU" + i + "_UPNSAKI_EDI_MEMBER_ID";
                    var groupRow = this.form.IchiranData.AsEnumerable().Where(n => !this.IsNullOrEmpty(n["SU" + i + "_KANRI_ID"]) && this.IsNullOrEmpty(n["US" + i + "_EDI_MEMBER_ID"]))
                                                                       .ToLookup(n => n[ediMemberId])
                                                                       .ToList();

                    foreach (var row in groupRow)
                    {
                        var jigyousha = row.FirstOrDefault();
                        if (jigyousha == null
                            || this.IsNullOrEmpty(jigyousha[ediMemberId])
                            || jigyoushaDt.AsEnumerable().Any(n => n["EDI_MEMBER_ID"].ToString().Equals(jigyousha[ediMemberId]) && (bool)n["UPN_KBN"]))
                        {
                            continue;
                        }

                        if (jigyousha[ediMemberId].ToString() != jigyousha["HST_SHA_EDI_MEMBER_ID"].ToString())
                        {
                            continue;
                        }

                        if (dic.ContainsKey(jigyousha[ediMemberId].ToString()))
                        {
                            // 運搬業者区分のみ更新
                            dic[jigyousha[ediMemberId].ToString()].UPN_KBN = true;
                            continue;
                        }

                        if (!jigyoushaDt.AsEnumerable().Any(n => n["EDI_MEMBER_ID"].ToString().Equals(jigyousha[ediMemberId])))
                        {
                        }
                        else if (jigyoushaDt.AsEnumerable().Any(n => n["EDI_MEMBER_ID"].ToString().Equals(jigyousha[ediMemberId]) && !(bool)n["UPN_KBN"])
                            && !updateData.ContainsKey(jigyousha[ediMemberId].ToString()))
                        {
                            // 既に電子事業者マスタに存在する場合は、運搬業者区分のみ更新
                            var latestJigyousha = fmDenshiJigyoushaDao.GetDataByCd(jigyousha[ediMemberId].ToString());
                            latestJigyousha.UPN_KBN = true;
                            updateData.Add(latestJigyousha.EDI_MEMBER_ID, latestJigyousha);
                        }
                    }
                }


                // 処分業者
                {
                    // 画面の一覧から処分業者を取得
                    var groupRow = this.form.IchiranData.AsEnumerable().Where(n => !this.IsNullOrEmpty(n["SB_KANRI_ID"]) && this.IsNullOrEmpty(n["SB_EDI_MEMBER_ID"]))
                                                                       .ToLookup(n => n["SB_SBN_SHA_MEMBER_ID"])
                                                                       .ToList();

                    foreach (var row in groupRow)
                    {
                        var jigyousha = row.FirstOrDefault();
                        if (jigyousha == null 
                            || this.IsNullOrEmpty(jigyousha["SB_SBN_SHA_MEMBER_ID"])
                            || jigyoushaDt.AsEnumerable().Any(n => n["EDI_MEMBER_ID"].ToString().Equals(jigyousha["SB_SBN_SHA_MEMBER_ID"])))
                        {
                            continue;
                        }

                        if (dic.ContainsKey(jigyousha["SB_SBN_SHA_MEMBER_ID"].ToString()))
                        {
                            // 運搬業者区分のみ更新
                            dic[jigyousha["SB_SBN_SHA_MEMBER_ID"].ToString()].SBN_KBN = true;
                            continue;
                        }

                        M_DENSHI_JIGYOUSHA entity = new M_DENSHI_JIGYOUSHA();
                        entity.EDI_MEMBER_ID = jigyousha["SB_SBN_SHA_MEMBER_ID"].ToString();
                        entity.EDI_PASSWORD = string.Empty;
                        entity.JIGYOUSHA_NAME = jigyousha["SB_SBN_SHA_NAME"].ToString();
                        entity.JIGYOUSHA_POST = jigyousha["SB_SBN_SHA_POST"].ToString();
                        entity.JIGYOUSHA_ADDRESS1 = jigyousha["SB_SBN_SHA_ADDRESS1"].ToString();
                        entity.JIGYOUSHA_ADDRESS2 = jigyousha["SB_SBN_SHA_ADDRESS2"].ToString();
                        entity.JIGYOUSHA_ADDRESS3 = jigyousha["SB_SBN_SHA_ADDRESS3"].ToString();
                        entity.JIGYOUSHA_ADDRESS4 = jigyousha["SB_SBN_SHA_ADDRESS4"].ToString();
                        entity.JIGYOUSHA_TEL = jigyousha["SB_SBN_SHA_TEL"].ToString();
                        entity.JIGYOUSHA_FAX = jigyousha["SB_SBN_SHA_FAX"].ToString();
                        entity.HST_KBN = false;
                        entity.UPN_KBN = false;
                        entity.SBN_KBN = true;
                        entity.HOUKOKU_HUYOU_KBN = false;
                        entity.GYOUSHA_CD = string.Empty;

                        dic.Add(jigyousha["SB_SBN_SHA_MEMBER_ID"].ToString(), entity);
                    }
                }

                if (dic.Values.Count == 0 && updateData.Values.Count == 0)
                {
                    msgLogic.MessageBoxShow("E061");
                    return;
                }

                foreach (var entity in dic.Values)
                {
                    var dataBinderEntry = new DataBinderLogic<M_DENSHI_JIGYOUSHA>(entity);
                    dataBinderEntry.SetSystemProperty(entity, false);
                }

                // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                using (Transaction tran = new Transaction())
                {
                    foreach (var entity in dic.Values)
                    {
                        MJSDao.Insert(entity);
                    }

                    foreach (var updateEntity in updateData.Values)
                    {
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //updateEntity.UPDATE_DATE = DateTime.Now;
                        updateEntity.UPDATE_DATE = this.getDBDateTime();
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        updateEntity.UPDATE_PC = SystemInformation.ComputerName;
                        updateEntity.UPDATE_USER = SystemProperty.UserName;
                        fmDenshiJigyoushaDao.Update(updateEntity);
                    }

                    tran.Commit();
                }

                msgLogic.MessageBoxShow("I001", "登録");
                this.Search();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラー
                    //メッセージ出力して継続
                    msgLogic.MessageBoxShow("E080");

                }
                else if (ex is Seasar.Framework.Exceptions.SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 事業場登録
        /// </summary>
        [Transaction]
        public virtual void JigyoujouToroku()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (this.form.IchiranData == null || this.form.IchiranData.Rows.Count == 0)
            {
                msgLogic.MessageBoxShow("E061");
                return;
            }

            // キー:加入者番号・事業場名称・事業場所在地1～5
            var hstDic = new Dictionary<string, M_DENSHI_JIGYOUJOU>();
            var upnDic = new Dictionary<string, M_DENSHI_JIGYOUJOU>();
            var sbnDic = new Dictionary<string, M_DENSHI_JIGYOUJOU>();

            // 重複排除用リスト
            List<string> registSbnJigyoujouList = new List<string>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT DISTINCT");
                sb.AppendLine(" (EDI_MEMBER_ID + '/' + ");
                sb.AppendLine("  JIGYOUJOU_NAME + '/' + ");
                sb.AppendLine("  ISNULL(JIGYOUJOU_ADDRESS1,'') + ");
                sb.AppendLine("  ISNULL(JIGYOUJOU_ADDRESS2,'') + ");
                sb.AppendLine("  ISNULL(JIGYOUJOU_ADDRESS3,'') + ");
                sb.AppendLine("  ISNULL(JIGYOUJOU_ADDRESS4,'')");
                sb.AppendLine(" ) AS INFO FROM M_DENSHI_JIGYOUJOU");

                DataTable jigyoujouDt = MJJDao.GetDataForEntity(sb.ToString());

                // 排出事業者
                {
                    // 画面の一覧から排出事業場を取得
                    var groupRow = this.form.IchiranData.AsEnumerable().Where(n => !this.IsNullOrEmpty(n["KANRI_ID"]) 
                                                                                 && this.IsNullOrEmpty(n["JIGYOUJOU_CD"]))
                                                                       .ToLookup(n => n["HST_JOU_INFO"])
                                                                       .ToList();

                    foreach (var row in groupRow)
                    {
                        var jigyoujou = row.FirstOrDefault();
                        if (jigyoujou == null
                            || this.IsNullOrEmpty(jigyoujou["HST_SHA_EDI_MEMBER_ID"])
                            || this.IsNullOrEmpty(jigyoujou["HST_JOU_NAME"])
                            || jigyoujouDt.AsEnumerable().Any(n => this.ConvertTwoByteToOneByte(n["INFO"].ToString()).Equals(this.ConvertTwoByteToOneByte(jigyoujou["HST_JOU_INFO"].ToString()))))
                        {
                            continue;
                        }

                        M_DENSHI_JIGYOUJOU entity = new M_DENSHI_JIGYOUJOU();
                        entity.EDI_MEMBER_ID = jigyoujou["HST_SHA_EDI_MEMBER_ID"].ToString();
                        entity.JIGYOUJOU_CD = string.Empty;

                        // 「3」又は「D3」から始まる加入者番号は処分事業場
                        if (entity.EDI_MEMBER_ID.StartsWith(ConstCls.EDI_MEMBER_ID_SHOBUN_3)
                            || entity.EDI_MEMBER_ID.StartsWith(ConstCls.EDI_MEMBER_ID_SHOBUN_D3))
                        {
                            entity.JIGYOUSHA_KBN = 3;
                            entity.JIGYOUJOU_KBN = 3;
                        }
                        else
                        {
                            entity.JIGYOUSHA_KBN = 1;
                            entity.JIGYOUJOU_KBN = 1;
                        }
                        entity.JIGYOUJOU_NAME = jigyoujou["HST_JOU_NAME"].ToString();
                        entity.JIGYOUJOU_POST = jigyoujou["HST_JOU_POST_NO"].ToString();
                        entity.JIGYOUJOU_ADDRESS1 = jigyoujou["HST_JOU_ADDRESS1"].ToString();
                        entity.JIGYOUJOU_ADDRESS2 = jigyoujou["HST_JOU_ADDRESS2"].ToString();
                        entity.JIGYOUJOU_ADDRESS3 = jigyoujou["HST_JOU_ADDRESS3"].ToString();
                        entity.JIGYOUJOU_ADDRESS4 = jigyoujou["HST_JOU_ADDRESS4"].ToString();
                        entity.JIGYOUJOU_TEL = jigyoujou["HST_JOU_TEL"].ToString();
                        entity.GYOUSHA_CD = string.Empty;
                        entity.GENBA_CD = string.Empty;

                        if (entity.JIGYOUSHA_KBN == 3
                            && entity.JIGYOUJOU_KBN == 3)
                        {
                            var jigyoujouInfo = this.CreatePrimaryJigyoujouInfo(entity);

                            if (!string.IsNullOrEmpty(jigyoujouInfo)
                                && !registSbnJigyoujouList.Contains(jigyoujouInfo))
                            {
                                // 処分事業場が重複して作成されるのを防ぐ
                                registSbnJigyoujouList.Add(jigyoujouInfo);
                                hstDic.Add(jigyoujou["HST_JOU_INFO"].ToString(), entity);
                            }
                        }
                        else
                        {
                            var jigyoujouInfo = this.CreatePrimaryJigyoujouInfo(entity);

                            if (!string.IsNullOrEmpty(jigyoujouInfo)
                                && !registSbnJigyoujouList.Contains(jigyoujouInfo))
                            {
                                registSbnJigyoujouList.Add(jigyoujouInfo);
                                hstDic.Add(jigyoujou["HST_JOU_INFO"].ToString(), entity);
                            }
                        }
                    }
                }

                // 収集運搬業者1～4(5は処分事業場)
                for (int i = 1; i < 5; i++)
                {
                    // カラム名
                    string jigyoujouInfo = "SU" + i + "_UPNSAKI_JOU_INFO";
                    string ediMemberId = "SU" + i + "_UPNSAKI_EDI_MEMBER_ID";
                    string jigyoujouName = "SU" + i + "_UPNSAKI_JOU_NAME";
                    string jigyoujouPost = "SU" + i + "_UPNSAKI_JOU_POST";
                    string jigyoujouAddress1 = "SU" + i + "_UPNSAKI_JOU_ADDRESS1";
                    string jigyoujouAddress2 = "SU" + i + "_UPNSAKI_JOU_ADDRESS2";
                    string jigyoujouAddress3 = "SU" + i + "_UPNSAKI_JOU_ADDRESS3";
                    string jigyoujouAddress4 = "SU" + i + "_UPNSAKI_JOU_ADDRESS4";
                    string jigyoujouTel = "SU" + i + "_UPNSAKI_JOU_TEL";

                    // 画面の一覧から収集運搬業者を取得
                    var groupRow = this.form.IchiranData.AsEnumerable().Where(n => !this.IsNullOrEmpty(n["SU" + i + "_KANRI_ID"]) 
                                                                                 && this.IsNullOrEmpty(n["US" + i + "_JIGYOUJOU_CD"])
                                                                                 && Convert.ToInt32(n["UPN_ROUTE_CNT"]) > i)
                                                                       .ToLookup(n => n[jigyoujouInfo])
                                                                       .ToList();

                    foreach (var row in groupRow)
                    {
                        var jigyoujou = row.FirstOrDefault();
                        if (jigyoujou == null
                            || this.IsNullOrEmpty(jigyoujou[jigyoujouInfo])
                            || jigyoujouDt.AsEnumerable().Any(n => this.ConvertTwoByteToOneByte(n["INFO"].ToString()).Equals(this.ConvertTwoByteToOneByte(jigyoujou[jigyoujouInfo].ToString()))))
                        {
                            continue;
                        }

                        if (upnDic.ContainsKey(jigyoujou[jigyoujouInfo].ToString()))
                        {
                            continue;
                        }

                        M_DENSHI_JIGYOUJOU entity = new M_DENSHI_JIGYOUJOU();
                        entity.EDI_MEMBER_ID = jigyoujou[ediMemberId].ToString();
                        entity.JIGYOUJOU_CD = string.Empty;
                        entity.JIGYOUSHA_KBN = 2;
                        entity.JIGYOUJOU_KBN = 2;
                        entity.JIGYOUJOU_NAME = jigyoujou[jigyoujouName].ToString();
                        entity.JIGYOUJOU_POST = jigyoujou[jigyoujouPost].ToString();
                        entity.JIGYOUJOU_ADDRESS1 = jigyoujou[jigyoujouAddress1].ToString();
                        entity.JIGYOUJOU_ADDRESS2 = jigyoujou[jigyoujouAddress2].ToString();
                        entity.JIGYOUJOU_ADDRESS3 = jigyoujou[jigyoujouAddress3].ToString();
                        entity.JIGYOUJOU_ADDRESS4 = jigyoujou[jigyoujouAddress4].ToString();
                        entity.JIGYOUJOU_TEL = jigyoujou[jigyoujouTel].ToString();
                        entity.GYOUSHA_CD = string.Empty;
                        entity.GENBA_CD = string.Empty;

                        upnDic.Add(jigyoujou[jigyoujouInfo].ToString(), entity);
                    }
                }

                // 処分業者
                {
                    // 画面の一覧から処分業者を取得
                    var groupRow = this.form.IchiranData.AsEnumerable().Where(n => !this.IsNullOrEmpty(n["KANRI_ID"]) && this.IsNullOrEmpty(n["SB_JIGYOUJOU_CD"]))
                                                                       .ToLookup(n => n["SB_UPNSAKI_JOU_INFO"])
                                                                       .ToList();

                    foreach (var row in groupRow)
                    {
                        var jigyoujou = row.FirstOrDefault();
                        if (jigyoujou == null
                            || this.IsNullOrEmpty(jigyoujou["SB_UPNSAKI_EDI_MEMBER_ID"])
                            || this.IsNullOrEmpty(jigyoujou["HST_JOU_NAME"])
                            || jigyoujouDt.AsEnumerable().Any(n => this.ConvertTwoByteToOneByte(n["INFO"].ToString()).Equals(this.ConvertTwoByteToOneByte(jigyoujou["SB_UPNSAKI_JOU_INFO"].ToString()))))
                        {
                            continue;
                        }

                        M_DENSHI_JIGYOUJOU entity = new M_DENSHI_JIGYOUJOU();
                        entity.EDI_MEMBER_ID = jigyoujou["SB_UPNSAKI_EDI_MEMBER_ID"].ToString();
                        entity.JIGYOUJOU_CD = string.Empty;
                        entity.JIGYOUSHA_KBN = 3;
                        entity.JIGYOUJOU_KBN = 3;
                        entity.JIGYOUJOU_NAME = jigyoujou["SB_UPNSAKI_JOU_NAME"].ToString();
                        entity.JIGYOUJOU_POST = jigyoujou["SB_UPNSAKI_JOU_POST"].ToString();
                        entity.JIGYOUJOU_ADDRESS1 = jigyoujou["SB_UPNSAKI_JOU_ADDRESS1"].ToString();
                        entity.JIGYOUJOU_ADDRESS2 = jigyoujou["SB_UPNSAKI_JOU_ADDRESS2"].ToString();
                        entity.JIGYOUJOU_ADDRESS3 = jigyoujou["SB_UPNSAKI_JOU_ADDRESS3"].ToString();
                        entity.JIGYOUJOU_ADDRESS4 = jigyoujou["SB_UPNSAKI_JOU_ADDRESS4"].ToString();
                        entity.JIGYOUJOU_TEL = jigyoujou["SB_UPNSAKI_JOU_TEL"].ToString();
                        entity.GYOUSHA_CD = string.Empty;
                        entity.GENBA_CD = string.Empty;

                        var jigyoujouInfo = this.CreatePrimaryJigyoujouInfo(entity);

                        if (!string.IsNullOrEmpty(jigyoujouInfo)
                            && !registSbnJigyoujouList.Contains(jigyoujouInfo))
                        {
                            // 処分事業場が重複して作成されるのを防ぐ
                            registSbnJigyoujouList.Add(jigyoujouInfo);
                            sbnDic.Add(jigyoujou["SB_UPNSAKI_JOU_INFO"].ToString(), entity);
                        }
                    }
                }

                int total = hstDic.Values.Count + upnDic.Values.Count + sbnDic.Values.Count;

                if (total == 0)
                {
                    msgLogic.MessageBoxShow("E061");
                    return;
                }

                foreach (var entity in hstDic.Values)
                {
                    var dataBinderEntry = new DataBinderLogic<M_DENSHI_JIGYOUJOU>(entity);
                    dataBinderEntry.SetSystemProperty(entity, false);
                }

                foreach (var entity in upnDic.Values)
                {
                    var dataBinderEntry = new DataBinderLogic<M_DENSHI_JIGYOUJOU>(entity);
                    dataBinderEntry.SetSystemProperty(entity, false);
                }

                foreach (var entity in sbnDic.Values)
                {
                    var dataBinderEntry = new DataBinderLogic<M_DENSHI_JIGYOUJOU>(entity);
                    dataBinderEntry.SetSystemProperty(entity, false);
                }

                // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                using (Transaction tran = new Transaction())
                {
                    foreach (var entity in hstDic.Values)
                    {
                        // 事業場CDの設定
                        entity.JIGYOUJOU_CD = MJJDao.GetNewJigyoujouCD(entity.EDI_MEMBER_ID);
                        
                        MJJDao.Insert(entity);
                    }
                    foreach (var entity in upnDic.Values)
                    {
                        // 事業場CDの設定
                        entity.JIGYOUJOU_CD = MJJDao.GetNewJigyoujouCD(entity.EDI_MEMBER_ID);

                        MJJDao.Insert(entity);
                    }
                    foreach (var entity in sbnDic.Values)
                    {
                        // 事業場CDの設定
                        entity.JIGYOUJOU_CD = MJJDao.GetNewJigyoujouCD(entity.EDI_MEMBER_ID);

                        MJJDao.Insert(entity);
                    }

                    tran.Commit();
                }

                msgLogic.MessageBoxShow("I001", "登録");
                this.Search();
            }
            catch (Exception ex)
            {
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラー
                    //メッセージ出力して継続
                    msgLogic.MessageBoxShow("E080");

                }
                else if (ex is Seasar.Framework.Exceptions.SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 拡張データ作成
        /// </summary>
        public virtual void ExDataSakusei()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {

                if (this.form.IchiranData == null || this.form.IchiranData.Rows.Count == 0)
                {
                    msgLogic.MessageBoxShow("E061");
                    return;
                }

                DataTable result = this.form.IchiranData.Clone();

                foreach (Row row in this.form.Ichiran.Rows)
                {
                    bool checkedCell = (bool)row.Cells[ConstCls.CELL_NAME_CHECK].Value;

                    if (checkedCell)
                    {
                        string maniId = row.Cells["MANIFEST_ID"].Value.ToString();
                        string where = string.Format("MANIFEST_ID = '{0}'",maniId);

                        // チェック済みのデータを取得
                        var selectRow = this.form.IchiranData.Select(where).FirstOrDefault();

                        if(selectRow != null)
                        {
                            result.ImportRow(selectRow);
                        }
                    }
                }

                if (result.Rows.Count == 0)
                {
                    msgLogic.MessageBoxShow("E061");
                    return;
                }

                List<DTODTExClass> dtoList = new List<DTODTExClass>();
                List<M_DENSHI_HAIKI_SHURUI_SAIBUNRUI> haikiSaibunruiDtoList = new List<M_DENSHI_HAIKI_SHURUI_SAIBUNRUI>();

                foreach (DataRow dataRow in result.Rows)
                {
                    var dto = CreateDtEx(dataRow);
                    var haikiShruiSaibunruiDto = this.CreateDenSaibunruiDto(dataRow);

                    if (dto != null)
                    {
                        dtoList.Add(dto);
                    }

                    if (haikiShruiSaibunruiDto != null)
                    {
                        if (haikiSaibunruiDtoList == null || haikiSaibunruiDtoList.Count < 1)
                        {
                            haikiSaibunruiDtoList.Add(haikiShruiSaibunruiDto);
                        }
                        else
                        {
                            var containsList =
                                haikiSaibunruiDtoList.Where(w =>
                                    w.EDI_MEMBER_ID.Equals(haikiShruiSaibunruiDto.EDI_MEMBER_ID)
                                    && w.HAIKI_SHURUI_CD.Equals(haikiShruiSaibunruiDto.HAIKI_SHURUI_CD)
                                    && w.HAIKI_SHURUI_SAIBUNRUI_CD.Equals(haikiShruiSaibunruiDto.HAIKI_SHURUI_SAIBUNRUI_CD)
                                );

                            if (containsList == null || containsList.Count() < 1)
                            {
                                haikiSaibunruiDtoList.Add(haikiShruiSaibunruiDto);
                            }
                        }
                    }
                }

                // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                using (Transaction tran = new Transaction())
                {
                    foreach (var dto in dtoList)
                    {
                        string kanriId = dto.dtR18ExEntity.KANRI_ID;

                        //DT_R18_EX
                        SqlInt64 systemId = InsertDT_R18_EX(dto, kanriId);

                        //DT_R19_EX
                        InsertDT_R19_EX(dto, kanriId, systemId);

                        //DT_R04_EX
                        InsertDT_R04_EX(dto, kanriId, systemId);

                        //DT_R08_EX
                        InsertDT_R08_EX(dto, kanriId, systemId);

                        //DT_R13_EX
                        InsertDT_R13_EX(dto, kanriId, systemId);
                    }

                    foreach (var saibunruiDto in haikiSaibunruiDtoList)
                    {
                        InsertDenHaikiShuruiSaibunrui(saibunruiDto);
                    }

                    tran.Commit();
                }

                // 検索処理
                if (this.Search() == -1)
                {
                    return;
                }

                this.form.ClearDetailFooter();
                // 業者コードの設定
                this.form.GyoushaCdSet(false);
                // 現場コードの設定
                this.form.GenbaCdSet();

                msgLogic.MessageBoxShow("I001", "登録");
            }
            catch (Exception ex)
            {
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラー
                    //メッセージ出力して継続
                    msgLogic.MessageBoxShow("E080");
                }
                else if (ex is Seasar.Framework.Exceptions.SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDロストフォーカスチェック
        /// </summary>
        public bool GyoushaCdLeaveChk(string cellName, out bool catchErr)
        {
            LogUtility.DebugMethodStart(cellName);
            catchErr = true;
            try
            {
                DataTable result = new DataTable();

                //SQL文格納StringBuilder
                StringBuilder sql = new StringBuilder();

                sql.Append(" SELECT M_GYOUSHA.GYOUSHA_CD, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_NAME_RYAKU, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_FURIGANA, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_POST, ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_ADDRESS1, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_TEL ");
                sql.Append(" FROM M_GYOUSHA LEFT JOIN M_TODOUFUKEN ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_CD ");
                sql.Append(" AND M_TODOUFUKEN.DELETE_FLG = 0 ");
                // 「運搬先事業場N」の場合、M_GENBA.積替保管区分=1
                if (ConstCls.UNPANSAKI_JIGYOUJOUN.Equals(cellName))
                {
                    sql.Append(" INNER JOIN M_GENBA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD");
                }
                sql.Append(" WHERE ((M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), ");
                sql.Append(" 111), 120) ");
                sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= ");
                sql.Append(" M_GYOUSHA.TEKIYOU_END) OR (M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, ");
                sql.Append(" CONVERT(nvarchar, GETDATE(), 111), 120) ");
                sql.Append(" AND M_GYOUSHA.TEKIYOU_END IS NULL) ");
                sql.Append(" OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= ");
                sql.Append(" M_GYOUSHA.TEKIYOU_END) ");
                sql.Append(" OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" AND M_GYOUSHA.TEKIYOU_END IS NULL)) ");
                sql.Append(" AND M_GYOUSHA.DELETE_FLG = 0 ");

                // 20151022 BUNN #12040 STR
                // 「排出事業者」の場合、排出事業者区分=1
                if (ConstCls.HAISHUTSU_JIGYOUSHA.Equals(cellName))
                {
                    sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                }
                // 「収集運搬業者N」の場合、運搬受託者区分=1
                else if (ConstCls.SHUSHU_UNPAN_GYOUSHAN.Equals(cellName))
                {
                    sql.Append(" AND M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = CONVERT(bit, 'True') ");
                }
                // 「運搬先業者N」の場合、運搬受託者区分=1
                else if (ConstCls.UNPANSAKI_GYOUSHAN.Equals(cellName))
                {
                    sql.Append(" AND M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = CONVERT(bit, 'True') ");
                }
                // 「処分業者」の場合、処分受託者区分=1
                else if (ConstCls.SHOBUN_GYOUSHA.Equals(cellName))
                {
                    sql.Append(" AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                }
                // 「排出事業場」の場合、M_GYOUSHA.排出事業者区分=1
                else if (ConstCls.HAISHUTSU_JIGYOUJOU.Equals(cellName))
                {
                    sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                }
                // 「運搬先事業場N」の場合、M_GENBA.積替保管区分=1
                else if (ConstCls.UNPANSAKI_JIGYOUJOUN.Equals(cellName))
                {
                    sql.Append(" AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                }
                // 「処分事業場」の場合、M_GYOUSHA.処分受託者区分=1
                else if (ConstCls.SHOBUN_JIGYOUJOU.Equals(cellName))
                {
                    sql.Append(" AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                }

                sql.Append(" AND M_GYOUSHA.GYOUSHAKBN_MANI = CONVERT(bit, 'True') ");
                // 20151022 BUNN #12040 END

                sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '" + this.form.cantxt_GyoushaCd.Text + "'");

                result = this.GYOUSHADao.GetDataForEntity(sql.ToString());

                if (result.Rows.Count > 0)
                {
                    // 業者名
                    this.form.ctxt_GyoushaName.Text = result.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                }
                else
                {
                    // 業者名
                    this.form.ctxt_GyoushaName.Text = string.Empty;
                    this.form.cantxt_GenbaCd.Text = string.Empty;
                    this.form.ctxt_GenbaName.Text = string.Empty;
                    LogUtility.DebugMethodEnd(false, catchErr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is SQLRuntimeException)
                {
                    this.errmessage.MessageBoxShow("E093", "");
                }
                else
                {
                    this.errmessage.MessageBoxShow("E245", "");
                }
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(true, catchErr);
            return true;
        }

        /// <summary>
        /// 現場CDロストフォーカスチェック
        /// </summary>
        public bool GenbaCdLeaveChk(string cellName, out bool catchErr)
        {
            LogUtility.DebugMethodStart(cellName);
            catchErr = true;
            try
            {
                DataTable result = new DataTable();

                //SQL文格納StringBuilder
                StringBuilder sql = new StringBuilder();

                sql.Append(" SELECT M_GENBA.GYOUSHA_CD, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_CD, ");
                sql.Append(" M_GENBA.GENBA_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_FURIGANA, ");
                sql.Append(" M_GENBA.GENBA_POST, ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_ADDRESS1, ");
                sql.Append(" M_GENBA.GENBA_TEL ");
                sql.Append(" FROM M_GENBA LEFT JOIN M_GYOUSHA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                sql.Append(" AND ((M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), ");
                sql.Append(" 111), 120) ");
                sql.Append(" and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= ");
                sql.Append(" M_GYOUSHA.TEKIYOU_END) or (M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, ");
                sql.Append(" CONVERT(nvarchar, GETDATE(), 111), 120) ");
                sql.Append(" and M_GYOUSHA.TEKIYOU_END IS NULL) ");
                sql.Append(" or (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= ");
                sql.Append(" M_GYOUSHA.TEKIYOU_END) ");
                sql.Append(" or (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" and M_GYOUSHA.TEKIYOU_END IS NULL)) ");
                sql.Append(" AND M_GYOUSHA.DELETE_FLG = 0 LEFT JOIN M_TODOUFUKEN ON M_GENBA.GENBA_TODOUFUKEN_CD ");
                sql.Append("  = M_TODOUFUKEN.TODOUFUKEN_CD ");
                sql.Append(" AND M_TODOUFUKEN.DELETE_FLG = 0 ");
                sql.Append(" WHERE ((M_GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), ");
                sql.Append(" 111), 120) ");
                sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= ");
                sql.Append(" M_GENBA.TEKIYOU_END) OR (M_GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, ");
                sql.Append(" CONVERT(nvarchar, GETDATE(), 111), 120) ");
                sql.Append(" AND M_GENBA.TEKIYOU_END IS NULL) ");
                sql.Append(" OR (M_GENBA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= ");
                sql.Append(" M_GENBA.TEKIYOU_END) ");
                sql.Append(" OR (M_GENBA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" AND M_GENBA.TEKIYOU_END IS NULL)) ");
                sql.Append(" AND M_GENBA.DELETE_FLG = 0 ");

                // 20151022 BUNN #12040 STR
                // 「排出事業場」の場合、排出事業場区分=1
                if (ConstCls.HAISHUTSU_JIGYOUJOU.Equals(cellName))
                {
                    sql.Append(" AND M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN = CONVERT(bit, 'True')");
                }
                // 「運搬先事業場N」の場合、積替保管区分=1
                else if (ConstCls.UNPANSAKI_JIGYOUJOUN.Equals(cellName))
                {
                    sql.Append(" AND M_GENBA.TSUMIKAEHOKAN_KBN = CONVERT(bit, 'True')");
                }
                // 「処分事業場」の場合、処分事業場区分=1 OR 最終処分場区分=1
                else if (ConstCls.SHOBUN_JIGYOUJOU.Equals(cellName))
                {
                    sql.Append(" AND (M_GENBA.SHOBUN_NIOROSHI_GENBA_KBN = CONVERT(bit, 'True')");
                    sql.Append("  OR M_GENBA.SAISHUU_SHOBUNJOU_KBN = CONVERT(bit, 'True'))");
                }

                // 業者CDが入力される場合
                if (!string.IsNullOrEmpty(this.form.cantxt_GyoushaCd.Text))
                {
                    sql.Append(" AND M_GENBA.GYOUSHA_CD = '" + this.form.cantxt_GyoushaCd.Text + "'");
                }
                else
                {
                    // 「排出事業場」の場合
                    if (ConstCls.HAISHUTSU_JIGYOUJOU.Equals(cellName))
                    {
                        sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = CONVERT(bit, 'True')");
                    }
                    // 「運搬先事業場N」の場合
                    else if (ConstCls.UNPANSAKI_JIGYOUJOUN.Equals(cellName))
                    {
                        sql.Append(" AND M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = CONVERT(bit, 'True')");
                    }
                    // 「処分事業場」の場合
                    else if (ConstCls.SHOBUN_JIGYOUJOU.Equals(cellName))
                    {
                        sql.Append(" AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = CONVERT(bit, 'True')");
                    }

                    sql.Append(" AND M_GYOUSHA.GYOUSHAKBN_MANI = CONVERT(bit, 'True')");
                }
                // 20151022 BUNN #12040 END

                sql.Append(" AND M_GENBA.GENBA_CD = '" + this.form.cantxt_GenbaCd.Text + "'");

                result = this.GENBADao.GetDataForEntity(sql.ToString());

                if (result.Rows.Count > 0)
                {
                    // 業者CD
                    this.form.cantxt_GyoushaCd.Text = result.Rows[0]["GYOUSHA_CD"].ToString();
                    // 業者名
                    this.form.ctxt_GyoushaName.Text = result.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                    // 現場名
                    this.form.ctxt_GenbaName.Text = result.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                }
                else
                {
                    //if (this.form.cantxt_GyoushaCd.Enabled == true)
                    //{
                    //    // 業者CD
                    //    this.form.cantxt_GyoushaCd.Text = string.Empty;
                    //    // 業者名
                    //    this.form.ctxt_GyoushaName.Text = string.Empty;
                    //}

                    // 現場名
                    this.form.ctxt_GenbaName.Text = string.Empty;
                    LogUtility.DebugMethodEnd(false, catchErr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is SQLRuntimeException)
                {
                    this.errmessage.MessageBoxShow("E093", "");
                }
                else
                {
                    this.errmessage.MessageBoxShow("E245", "");
                }
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(true, catchErr);
            return true;
        }

        /// <summary>
        /// チェックボックス押下時のアラート表示
        /// </summary>
        /// <param name="row"></param>
        public void CheckRenkeiMaster(Row row)
        {
            if (row == null)
            {
                return;
            }

            // 
            if (row.Cells[ConstCls.CELL_NAME_CHECK].EditedFormattedValue != null 
                && !string.IsNullOrEmpty(row.Cells[ConstCls.CELL_NAME_CHECK].EditedFormattedValue.ToString()))
            {
                bool checkValue = false;
                if (bool.TryParse(row.Cells[ConstCls.CELL_NAME_CHECK].EditedFormattedValue.ToString(), out checkValue)
                    && checkValue)
                {
                    if (HasErrorRenkeiMaster(this.form.IchiranData.Rows[this.form.Ichiran.CurrentRow.Index]))
                    {
                        // チェック済みかつ設定されていない連携マスタ存在時
                        MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E172", "将軍連携マスタの設定");

                        this.form.Ichiran.CancelEdit();
                    }
                }
            }
        }

        /// <summary>
        /// ヘッダーチェックボックスの変更処理
        /// </summary>
        public bool ChangeHeaderCheckBox()
        {
            bool ret = true;
            try
            {
                if (this.form.Ichiran == null || this.form.Ichiran.RowCount == 0 ||
                    this.form.IchiranData == null || this.form.IchiranData.Rows.Count == 0)
                {
                    return ret;
                }

                // ヘッダーチェックボックスの値取得
                bool check = (bool)this.form.Ichiran.CurrentCell.EditedFormattedValue;

                for (int i = 0; i < this.form.IchiranData.Rows.Count; i++)
                {
                    if (!HasErrorRenkeiMaster(this.form.IchiranData.Rows[i]))
                    {
                        this.form.Ichiran.Rows[i].Cells[ConstCls.CELL_NAME_CHECK].Value = check;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeHeaderCheckBox", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

		/// <summary>
		/// 更新処理
		/// </summary>
		public void Update(bool errorFlag)
		{
		}

		/// <summary>
		/// 論理削除処理
		/// </summary>
		public void LogicalDelete()
		{
		}

		/// <summary>
		/// 物理削除処理
		/// </summary>
		public void PhysicalDelete()
		{
		}
		#endregion

		#region private
		/// <summary>
		/// ボタン初期化処理
		/// </summary>
		private void ButtonInit()
		{
			LogUtility.DebugMethodStart();

			// ボタン設定の読込
			var buttonSetting = this.CreateButtonInfo();

			// ボタンの名称とヒントテキストを設定
			ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

			LogUtility.DebugMethodEnd();
		}

		/// <summary>
		/// ボタン設定の読込
		/// </summary>
		/// <returns name="ButtonSetting[]">XMLに記載されたButtonのリスト</returns>
		private ButtonSetting[] CreateButtonInfo()
		{
			LogUtility.DebugMethodStart();

			var buttonSetting = new ButtonSetting();
			var thisAssembly = Assembly.GetExecutingAssembly();

			// XML読み込み
            var tmp = buttonSetting.LoadButtonSetting(thisAssembly, Const.ConstCls.ButtonInfoXmlPath);

			LogUtility.DebugMethodEnd();

            return tmp;
		}

		/// <summary>
		/// イベントの初期化処理
		/// </summary>
		private void EventInit()
		{
			LogUtility.DebugMethodStart();

			// 検索ボタン(F8)イベント生成
			parentForm.bt_func8.Click += new EventHandler(this.form.Search);

			// 登録ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);

			// 閉じるボタン(F12)イベント生成
			parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //【1】業者登録イベント生成
            parentForm.bt_process1.Click += new EventHandler(this.form.GyoushaTouroku);

            //【2】現場登録イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.form.GenbaTouroku);

            //【3】事業者登録イベント生成
            parentForm.bt_process3.Click += new EventHandler(this.form.JigyoushaToroku);

            //【4】事業場登録イベント生成
            parentForm.bt_process4.Click += new EventHandler(this.form.JigyoujouToroku);

            //【5】データ作成イベント生成
            parentForm.bt_process5.Click += new EventHandler(this.form.ExDataSakusei);

            /// 20141023 Houkakou 「補助データ」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.cdate_HikiwatasiBiTo.MouseDoubleClick += new MouseEventHandler(cdate_HikiwatasiBiTo_MouseDoubleClick);
            this.form.cntxt_ManifestIdTo.MouseDoubleClick += new MouseEventHandler(cntxt_ManifestIdTo_MouseDoubleClick);
            /// 20141023 Houkakou 「補助データ」のダブルクリックを追加する　end

			LogUtility.DebugMethodEnd();
		}

        #region 画面レイアウト変更
        /// <summary>
        /// 画面レイアウトを変更
        /// Ver 1.18で、画面レイアウトが変更になったため、このメソッドに処理をまとめる。
        /// </summary>
        public void ChangeLayout()
        {
            #region 不要なプロパティを初期化
            this.form.cntxt_ManifestIdFrom.FocusOutCheckMethod = null;
            this.form.cntxt_ManifestIdTo.FocusOutCheckMethod = null;
            #endregion

            #region コントロールの初期化
            this.form.MasterSettingConditionLabel = new Label();
            this.form.MasterSettingConditionValue = new CustomNumericTextBox2();
            this.form.MasterSettingConditionKbn1 = new CustomRadioButton();
            this.form.MasterSettingConditionKbn2 = new CustomRadioButton();
            this.form.MasterSettingConditionKbn3 = new CustomRadioButton();
            this.form.MasterSettingCondPanel = new CustomPanel();
            this.form.DataConditionLabel = new Label();
            this.form.DataConditionValue = new CustomNumericTextBox2();
            this.form.DataConditionKbn1 = new CustomRadioButton();
            this.form.DataConditionKbn2 = new CustomRadioButton();
            this.form.DataConditionKbn3 = new CustomRadioButton();
            this.form.DataCondPanel = new CustomPanel();
            this.form.HstJigyoushaLabel = new Label();
            this.form.HstJigyoushaCd = new CustomAlphaNumTextBox();
            this.form.HstJigyoushaName = new CustomTextBox();
            this.form.HstJigyoushaSearch = new CustomPopupOpenButton();
            this.form.HstJigyoujouLabel = new Label();
            this.form.HstJigyoujouCd = new CustomAlphaNumTextBox();
            this.form.HstJigyoujouName = new CustomTextBox();
            this.form.HstJigyoujouSearch = new CustomPopupOpenButton();
            this.form.UpnJigyoushaLabel = new Label();
            this.form.UpnJigyoushaCd = new CustomAlphaNumTextBox();
            this.form.UpnJigyoushaName = new CustomTextBox();
            this.form.UpnJigyoushaSearch = new CustomPopupOpenButton();
            this.form.SbnJigyoushaLabel = new Label();
            this.form.SbnJigyoushaCd = new CustomAlphaNumTextBox();
            this.form.SbnJigyoushaName = new CustomTextBox();
            this.form.SbnJigyoushaSearch = new CustomPopupOpenButton();
            this.form.SbnJigyoujouLabel = new Label();
            this.form.SbnJigyoujouCd = new CustomAlphaNumTextBox();
            this.form.SbnJigyoujouName = new CustomTextBox();
            this.form.SbnJigyoujouSearch = new CustomPopupOpenButton();
            this.form.LastSbnJigyoushaLabel = new Label();
            this.form.LastSbnJigyoushaCd = new CustomAlphaNumTextBox();
            this.form.LastSbnJigyoushaName = new CustomTextBox();
            this.form.LastSbnJigyoushaSearch = new CustomPopupOpenButton();
            this.form.LastSbnJigyoujouLabel = new Label();
            this.form.LastSbnJigyoujouCd = new CustomAlphaNumTextBox();
            this.form.LastSbnJigyoujouName = new CustomTextBox();
            this.form.LastSbnJigyoujouSearch = new CustomPopupOpenButton();
            #endregion

            // Location用
            int tempLeft = 0;
            int tempWith = 0;
            int under = this.form.Height - 2;

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));

            #region 引渡し日
            this.form.cdate_HikiwatasiBiFrom.Width -= 30;
            tempLeft = this.form.lbl_HikiwatasiBi.Width + this.form.lbl_HikiwatasiBi.Left + 2;
            this.form.cdate_HikiwatasiBiFrom.Left = tempLeft;
            tempLeft = this.form.cdate_HikiwatasiBiFrom.Width + this.form.cdate_HikiwatasiBiFrom.Left + 4;
            this.form.label1.Left = tempLeft;
            tempLeft = this.form.label1.Width + this.form.label1.Left + 4;
            this.form.cdate_HikiwatasiBiTo.Left = tempLeft;
            this.form.cdate_HikiwatasiBiTo.Width -= 30;
            #endregion

            #region マニフェスト番号
            tempLeft = this.form.cdate_HikiwatasiBiTo.Width + this.form.cdate_HikiwatasiBiTo.Left + 10;
            this.form.label2.Location = new Point(tempLeft, 0);
            tempLeft = this.form.label2.Width + this.form.label2.Left + 2;
            this.form.cntxt_ManifestIdFrom.Location = new Point(tempLeft, 0);
            this.form.cntxt_ManifestIdFrom.Width = this.form.cdate_HikiwatasiBiFrom.Width;
            tempLeft = this.form.cntxt_ManifestIdFrom.Width + this.form.cntxt_ManifestIdFrom.Left + 4;
            this.form.label3.Location = new Point(tempLeft, 4);
            tempLeft = this.form.label3.Width + this.form.label3.Left + 4;
            this.form.cntxt_ManifestIdTo.Location = new Point(tempLeft, 0);
            this.form.cntxt_ManifestIdTo.Width = this.form.cdate_HikiwatasiBiTo.Width;
            #endregion

            #region マスタ設定

            // add Form
            this.form.Controls.Add(this.form.MasterSettingConditionLabel);
            this.form.Controls.Add(this.form.MasterSettingCondPanel);

            // Label
            this.form.MasterSettingConditionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.MasterSettingConditionLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.MasterSettingConditionLabel.ForeColor = System.Drawing.Color.White;
            tempLeft = this.form.lbl_HikiwatasiBi.Left;
            this.form.MasterSettingConditionLabel.Location = new Point(tempLeft, this.form.lbl_HikiwatasiBi.Height + 2);
            this.form.MasterSettingConditionLabel.Name = "MasterSettingConditionLabel";
            this.form.MasterSettingConditionLabel.Size = new Size(this.form.lbl_HikiwatasiBi.Width, 20);
            this.form.MasterSettingConditionLabel.TabIndex = this.form.cntxt_ManifestIdTo.TabIndex + 1;
            this.form.MasterSettingConditionLabel.Text = "マスタ設定※";
            this.form.MasterSettingConditionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // input box
            this.form.MasterSettingConditionValue.BackColor = SystemColors.Window;
            this.form.MasterSettingConditionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.MasterSettingConditionValue.BackColor = System.Drawing.SystemColors.Window;
            this.form.MasterSettingConditionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.MasterSettingConditionValue.CharacterLimitList = new char[] { '1', '2', '3' };
            this.form.MasterSettingConditionValue.CharactersNumber = new decimal(new int[] { 1, 0, 0, 0 });
            this.form.MasterSettingConditionValue.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.MasterSettingConditionValue.DisplayItemName = "マスタ設定";
            this.form.MasterSettingConditionValue.DisplayPopUp = null;
            this.form.MasterSettingConditionValue.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.MasterSettingConditionValue.ForeColor = System.Drawing.Color.Black;
            this.form.MasterSettingConditionValue.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.MasterSettingConditionValue.IsInputErrorOccured = false;
            this.form.MasterSettingConditionValue.LinkedRadioButtonArray = new string[] { "MasterSettingConditionKbn1", "MasterSettingConditionKbn2", "MasterSettingConditionKbn3" };
            this.form.MasterSettingConditionValue.Location = new System.Drawing.Point(-1, -1);
            this.form.MasterSettingConditionValue.MaxLength = 1;
            this.form.MasterSettingConditionValue.Name = "MasterSettingConditionValue";
            this.form.MasterSettingConditionValue.PopupAfterExecute = null;
            this.form.MasterSettingConditionValue.PopupBeforeExecute = null;
            RangeSettingDto rangeSettingDto2 = new RangeSettingDto();
            rangeSettingDto2.Max = new decimal(new int[] { 3, 0, 0, 0 });
            rangeSettingDto2.Min = new decimal(new int[] { 1, 0, 0, 0 });
            this.form.MasterSettingConditionValue.RangeSetting = rangeSettingDto2;
            this.form.MasterSettingConditionValue.ShortItemName = "マスタ設定";
            this.form.MasterSettingConditionValue.Size = new System.Drawing.Size(20, 20);
            this.form.MasterSettingConditionValue.TabIndex = 2;
            this.form.MasterSettingConditionValue.Tag = "【1～3】のいずれかで入力してください";
            this.form.MasterSettingConditionValue.TextAlign = HorizontalAlignment.Center;

            // radio button 1
            this.form.MasterSettingConditionKbn1.AutoSize = true;
            this.form.MasterSettingConditionKbn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.MasterSettingConditionKbn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.MasterSettingConditionKbn1.LinkedTextBox = "MasterSettingConditionValue";
            this.form.MasterSettingConditionKbn1.Location = new System.Drawing.Point(25, 0);
            this.form.MasterSettingConditionKbn1.Name = "MasterSettingConditionKbn1";
            this.form.MasterSettingConditionKbn1.PopupAfterExecute = null;
            this.form.MasterSettingConditionKbn1.PopupBeforeExecute = null;
            this.form.MasterSettingConditionKbn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.MasterSettingConditionKbn1.Size = new System.Drawing.Size(55, 17);
            this.form.MasterSettingConditionKbn1.TabIndex = 4;
            this.form.MasterSettingConditionKbn1.Tag = "";
            this.form.MasterSettingConditionKbn1.Text = "1.済";
            this.form.MasterSettingConditionKbn1.UseVisualStyleBackColor = true;
            this.form.MasterSettingConditionKbn1.Value = "1";

            // radio button 2
            this.form.MasterSettingConditionKbn2.AutoSize = true;
            this.form.MasterSettingConditionKbn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.MasterSettingConditionKbn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.MasterSettingConditionKbn2.LinkedTextBox = "MasterSettingConditionValue";
            this.form.MasterSettingConditionKbn2.Location = new System.Drawing.Point(85, 0);
            this.form.MasterSettingConditionKbn2.Name = "MasterSettingConditionKbn2";
            this.form.MasterSettingConditionKbn2.PopupAfterExecute = null;
            this.form.MasterSettingConditionKbn2.PopupBeforeExecute = null;
            this.form.MasterSettingConditionKbn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.MasterSettingConditionKbn2.Size = new System.Drawing.Size(55, 17);
            this.form.MasterSettingConditionKbn2.TabIndex = 6;
            this.form.MasterSettingConditionKbn2.Tag = "";
            this.form.MasterSettingConditionKbn2.Text = "2.未";
            this.form.MasterSettingConditionKbn2.UseVisualStyleBackColor = true;
            this.form.MasterSettingConditionKbn2.Value = "2";

            // radio button 3
            this.form.MasterSettingConditionKbn3.AutoSize = true;
            this.form.MasterSettingConditionKbn3.Checked = true;
            this.form.MasterSettingConditionKbn3.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.MasterSettingConditionKbn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.MasterSettingConditionKbn3.LinkedTextBox = "MasterSettingConditionValue";
            this.form.MasterSettingConditionKbn3.Location = new System.Drawing.Point(145, 0);
            this.form.MasterSettingConditionKbn3.Name = "MasterSettingConditionKbn3";
            this.form.MasterSettingConditionKbn3.PopupAfterExecute = null;
            this.form.MasterSettingConditionKbn3.PopupBeforeExecute = null;
            this.form.MasterSettingConditionKbn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.MasterSettingConditionKbn3.Size = new System.Drawing.Size(67, 17);
            this.form.MasterSettingConditionKbn3.TabIndex = 8;
            this.form.MasterSettingConditionKbn3.Tag = "";
            this.form.MasterSettingConditionKbn3.Text = "3.全て";
            this.form.MasterSettingConditionKbn3.UseVisualStyleBackColor = true;
            this.form.MasterSettingConditionKbn3.Value = "3";

            // Panel
            this.form.MasterSettingCondPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.MasterSettingCondPanel.Controls.Add(this.form.MasterSettingConditionKbn1);
            this.form.MasterSettingCondPanel.Controls.Add(this.form.MasterSettingConditionKbn2);
            this.form.MasterSettingCondPanel.Controls.Add(this.form.MasterSettingConditionKbn3);
            this.form.MasterSettingCondPanel.Controls.Add(this.form.MasterSettingConditionValue);
            tempLeft = this.form.MasterSettingConditionLabel.Width + this.form.MasterSettingConditionLabel.Left + 2;
            this.form.MasterSettingCondPanel.Location = new System.Drawing.Point(tempLeft, this.form.MasterSettingConditionLabel.Top);
            this.form.MasterSettingCondPanel.Name = "MasterSettingCondPanel";
            tempWith = this.form.cdate_HikiwatasiBiTo.Width + this.form.cdate_HikiwatasiBiTo.Left - this.form.MasterSettingCondPanel.Left;
            this.form.MasterSettingCondPanel.Size = new System.Drawing.Size(tempWith, 20);
            this.form.MasterSettingCondPanel.TabIndex = this.form.MasterSettingConditionLabel.TabIndex + 1;

            this.form.MasterSettingConditionValue.SetResultText("3");
            #endregion

            #region データ作成

            // add Form
            this.form.Controls.Add(this.form.DataConditionLabel);
            this.form.Controls.Add(this.form.DataCondPanel);

            // Label
            this.form.DataConditionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.DataConditionLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.DataConditionLabel.ForeColor = System.Drawing.Color.White;
            tempLeft = this.form.MasterSettingCondPanel.Width + this.form.MasterSettingCondPanel.Left + 10;
            this.form.DataConditionLabel.Location = new Point(tempLeft, this.form.MasterSettingCondPanel.Top);
            this.form.DataConditionLabel.Name = "DataConditionLabel";
            this.form.DataConditionLabel.Size = new Size(this.form.label2.Width, 20);
            this.form.DataConditionLabel.TabIndex = this.form.MasterSettingCondPanel.TabIndex + 1;
            this.form.DataConditionLabel.Text = "データ作成※";
            this.form.DataConditionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // input box
            this.form.DataConditionValue.BackColor = SystemColors.Window;
            this.form.DataConditionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.DataConditionValue.BackColor = System.Drawing.SystemColors.Window;
            this.form.DataConditionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.DataConditionValue.CharactersNumber = new decimal(new int[] { 1, 0, 0, 0 });
            this.form.DataConditionValue.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.DataConditionValue.DisplayItemName = "データ作成";
            this.form.DataConditionValue.DisplayPopUp = null;
            this.form.DataConditionValue.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.DataConditionValue.ForeColor = System.Drawing.Color.Black;
            this.form.DataConditionValue.IsInputErrorOccured = false;
            this.form.DataConditionValue.LinkedRadioButtonArray = new string[] { "DataConditionKbn1", "DataConditionKbn2", "DataConditionKbn3" };
            this.form.DataConditionValue.Location = new System.Drawing.Point(-1, -1);
            this.form.DataConditionValue.MaxLength = 1;
            this.form.DataConditionValue.Name = "DataConditionValue";
            this.form.DataConditionValue.PopupAfterExecute = null;
            this.form.DataConditionValue.PopupBeforeExecute = null;
            RangeSettingDto rangeSettingDto1 = new RangeSettingDto();
            rangeSettingDto1.Max = new decimal(new int[] {3, 0, 0, 0});
            rangeSettingDto1.Min = new decimal(new int[] {1, 0, 0, 0});
            this.form.DataConditionValue.RangeSetting = rangeSettingDto1;
            this.form.DataConditionValue.ShortItemName = "データ作成";
            this.form.DataConditionValue.Size = new System.Drawing.Size(20, 20);
            this.form.DataConditionValue.TabIndex = 2;
            this.form.DataConditionValue.Tag = "【1～3】のいずれかで入力してください";
            this.form.DataConditionValue.TextAlign = HorizontalAlignment.Center;

            // radio button 1
            this.form.DataConditionKbn1.AutoSize = true;
            this.form.DataConditionKbn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.DataConditionKbn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.DataConditionKbn1.LinkedTextBox = "DataConditionValue";
            this.form.DataConditionKbn1.Location = new System.Drawing.Point(25, 0);
            this.form.DataConditionKbn1.Name = "DataConditionKbn1";
            this.form.DataConditionKbn1.PopupAfterExecute = null;
            this.form.DataConditionKbn1.PopupBeforeExecute = null;
            this.form.DataConditionKbn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.DataConditionKbn1.Size = new System.Drawing.Size(55, 17);
            this.form.DataConditionKbn1.TabIndex = 4;
            this.form.DataConditionKbn1.Tag = "";
            this.form.DataConditionKbn1.Text = "1.済";
            this.form.DataConditionKbn1.UseVisualStyleBackColor = true;
            this.form.DataConditionKbn1.Value = "1";

            // radio button 2
            this.form.DataConditionKbn2.AutoSize = true;
            this.form.DataConditionKbn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.DataConditionKbn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.DataConditionKbn2.LinkedTextBox = "DataConditionValue";
            this.form.DataConditionKbn2.Location = new System.Drawing.Point(85, 0);
            this.form.DataConditionKbn2.Name = "DataConditionKbn2";
            this.form.DataConditionKbn2.PopupAfterExecute = null;
            this.form.DataConditionKbn2.PopupBeforeExecute = null;
            this.form.DataConditionKbn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.DataConditionKbn2.Size = new System.Drawing.Size(55, 17);
            this.form.DataConditionKbn2.TabIndex = 6;
            this.form.DataConditionKbn2.Tag = "";
            this.form.DataConditionKbn2.Text = "2.未";
            this.form.DataConditionKbn2.UseVisualStyleBackColor = true;
            this.form.DataConditionKbn2.Value = "2";

            // radio button 3
            this.form.DataConditionKbn3.AutoSize = true;
            this.form.DataConditionKbn3.Checked = true;
            this.form.DataConditionKbn3.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.DataConditionKbn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.DataConditionKbn3.LinkedTextBox = "DataConditionValue";
            this.form.DataConditionKbn3.Location = new System.Drawing.Point(145, 0);
            this.form.DataConditionKbn3.Name = "DataConditionKbn3";
            this.form.DataConditionKbn3.PopupAfterExecute = null;
            this.form.DataConditionKbn3.PopupBeforeExecute = null;
            this.form.DataConditionKbn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.DataConditionKbn3.Size = new System.Drawing.Size(67, 17);
            this.form.DataConditionKbn3.TabIndex = 8;
            this.form.DataConditionKbn3.Tag = "";
            this.form.DataConditionKbn3.Text = "3.全て";
            this.form.DataConditionKbn3.UseVisualStyleBackColor = true;
            this.form.DataConditionKbn3.Value = "3";

            // Panel
            this.form.DataCondPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.DataCondPanel.Controls.Add(this.form.DataConditionKbn1);
            this.form.DataCondPanel.Controls.Add(this.form.DataConditionKbn2);
            this.form.DataCondPanel.Controls.Add(this.form.DataConditionKbn3);
            this.form.DataCondPanel.Controls.Add(this.form.DataConditionValue);
            tempLeft = this.form.DataConditionLabel.Width + this.form.DataConditionLabel.Left + 2;
            this.form.DataCondPanel.Location = new System.Drawing.Point(tempLeft, this.form.DataConditionLabel.Top);
            this.form.DataCondPanel.Name = "DataCondPanel";
            tempWith = this.form.cntxt_ManifestIdTo.Width + this.form.cntxt_ManifestIdTo.Left - this.form.DataCondPanel.Left;
            this.form.DataCondPanel.Size = new System.Drawing.Size(tempWith, 20);
            this.form.DataCondPanel.TabIndex = this.form.DataConditionLabel.TabIndex + 1;

            this.form.DataConditionValue.SetResultText("3");
            #endregion

            #region 排出事業者
            // add Form
            form.Controls.Add(this.form.HstJigyoushaLabel);
            form.Controls.Add(this.form.HstJigyoushaCd);
            form.Controls.Add(this.form.HstJigyoushaName);
            form.Controls.Add(this.form.HstJigyoushaSearch);

            // Label
            this.form.HstJigyoushaLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.HstJigyoushaLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.HstJigyoushaLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.HstJigyoushaLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.HstJigyoushaLabel.ForeColor = System.Drawing.Color.White;
            this.form.HstJigyoushaLabel.Location = new System.Drawing.Point(this.form.MasterSettingConditionLabel.Left, this.form.MasterSettingConditionLabel.Top + this.form.MasterSettingConditionLabel.Height + 2);
            this.form.HstJigyoushaLabel.Name = "HstJigyoushaLabel";
            this.form.HstJigyoushaLabel.Size = new System.Drawing.Size(this.form.lbl_HikiwatasiBi.Width, 20);
            this.form.HstJigyoushaLabel.TabIndex = this.form.DataCondPanel.TabIndex + 1;
            this.form.HstJigyoushaLabel.Text = "排出事業者";
            this.form.HstJigyoushaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // CD
            this.form.HstJigyoushaCd.BackColor = System.Drawing.SystemColors.Window;
            this.form.HstJigyoushaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.HstJigyoushaCd.ChangeUpperCase = true;
            this.form.HstJigyoushaCd.CharacterLimitList = null;
            this.form.HstJigyoushaCd.CharactersNumber = new decimal(new int[] { 7, 0, 0, 0 });
            this.form.HstJigyoushaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.HstJigyoushaCd.DisplayItemName = "排出事業者";
            this.form.HstJigyoushaCd.DisplayPopUp = null;
            this.form.HstJigyoushaCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.HstJigyoushaCd.ForeColor = System.Drawing.Color.Black;
            this.form.HstJigyoushaCd.GetCodeMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.HstJigyoushaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.HstJigyoushaCd.IsInputErrorOccured = false;
            tempLeft = this.form.HstJigyoushaLabel.Width + this.form.HstJigyoushaLabel.Left + 2;
            this.form.HstJigyoushaCd.Location = new System.Drawing.Point(tempLeft, this.form.HstJigyoushaLabel.Top);
            this.form.HstJigyoushaCd.MaxLength = 7;
            this.form.HstJigyoushaCd.Name = "HstJigyoushaCd";
            this.form.HstJigyoushaCd.PopupAfterExecuteMethod = "HstJigyoushaCd_PopupAfterExecuteMethod";
            this.form.HstJigyoushaCd.PopupAfterExecute = null;
            this.form.HstJigyoushaCd.PopupBeforeExecuteMethod = "HstJigyoushaCd_PopupBeforeExecuteMethod";
            this.form.HstJigyoushaCd.PopupBeforeExecute = null;
            this.form.HstJigyoushaCd.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.HstJigyoushaCd.PopupSetFormField = "HstJigyoushaCd, HstJigyoushaName";
            this.form.HstJigyoushaCd.prevText = null;
            this.form.HstJigyoushaCd.SetFormField = "HstJigyoushaCd, HstJigyoushaName";
            this.form.HstJigyoushaCd.Size = new System.Drawing.Size(55, 20);
            this.form.HstJigyoushaCd.TabIndex = this.form.HstJigyoushaLabel.TabIndex + 1;
            this.form.HstJigyoushaCd.Tag = "排出事業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.form.HstJigyoushaCd.ZeroPaddengFlag = true;

            // name
            this.form.HstJigyoushaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.HstJigyoushaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.HstJigyoushaName.CharactersNumber = new decimal(new int[] { 32767, 0, 0, 0 });
            this.form.HstJigyoushaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.HstJigyoushaName.DisplayPopUp = null;
            this.form.HstJigyoushaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.HstJigyoushaName.ForeColor = System.Drawing.Color.Black;
            this.form.HstJigyoushaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.form.HstJigyoushaName.IsInputErrorOccured = false;
            tempLeft = this.form.HstJigyoushaCd.Width + this.form.HstJigyoushaCd.Left - 1;
            this.form.HstJigyoushaName.Location = new System.Drawing.Point(tempLeft, this.form.HstJigyoushaCd.Top);
            this.form.HstJigyoushaName.MaxLength = 0;
            this.form.HstJigyoushaName.Name = "HstJigyoushaName";
            this.form.HstJigyoushaName.PopupAfterExecute = null;
            this.form.HstJigyoushaName.PopupBeforeExecute = null;
            this.form.HstJigyoushaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.HstJigyoushaName.prevText = null;
            this.form.HstJigyoushaName.ReadOnly = true;
            this.form.HstJigyoushaName.Size = new System.Drawing.Size(160, 20);
            this.form.HstJigyoushaName.TabIndex = this.form.HstJigyoushaCd.TabIndex + 1;
            this.form.HstJigyoushaName.TabStop = false;

            // Search button
            this.form.HstJigyoushaSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.form.HstJigyoushaSearch.CharactersNumber = new decimal(new int[] { 0, 0, 0, 0 });
            this.form.HstJigyoushaSearch.DBFieldsName = null;
            this.form.HstJigyoushaSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.HstJigyoushaSearch.DisplayItemName = null;
            this.form.HstJigyoushaSearch.DisplayPopUp = null;
            this.form.HstJigyoushaSearch.ErrorMessage = null;
            this.form.HstJigyoushaSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.form.HstJigyoushaSearch.GetCodeMasterField = null;
            this.form.HstJigyoushaSearch.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_BUTTON.Image")));
            this.form.HstJigyoushaSearch.ItemDefinedTypes = null;
            this.form.HstJigyoushaSearch.LinkedTextBoxs = null;
            tempLeft = this.form.HstJigyoushaName.Width + this.form.HstJigyoushaName.Left + 5;
            this.form.HstJigyoushaSearch.Location = new System.Drawing.Point(tempLeft, this.form.HstJigyoushaName.Top - 1);
            this.form.HstJigyoushaSearch.Name = "HstJigyoushaSearch";
            this.form.HstJigyoushaSearch.PopupAfterExecuteMethod = "HstJigyoushaCd_PopupAfterExecuteMethod";
            this.form.HstJigyoushaSearch.PopupAfterExecute = null;
            this.form.HstJigyoushaSearch.PopupBeforeExecuteMethod = "HstJigyoushaCd_PopupBeforeExecuteMethod";
            this.form.HstJigyoushaSearch.PopupBeforeExecute = null;
            this.form.HstJigyoushaSearch.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.HstJigyoushaSearch.PopupSetFormField = "HstJigyoushaCd, HstJigyoushaName";
            this.form.HstJigyoushaSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.form.HstJigyoushaSearch.PopupWindowName = "検索共通ポップアップ";
            this.form.HstJigyoushaSearch.SearchDisplayFlag = 0;
            this.form.HstJigyoushaSearch.SetFormField = "HstJigyoushaCd, HstJigyoushaName";
            this.form.HstJigyoushaSearch.ShortItemName = null;
            this.form.HstJigyoushaSearch.Size = new System.Drawing.Size(22, 22);
            this.form.HstJigyoushaSearch.TabIndex = this.form.HstJigyoushaName.TabIndex + 1;
            this.form.HstJigyoushaSearch.TabStop = false;
            this.form.HstJigyoushaSearch.Tag = "排出事業者の検索を行います";
            this.form.HstJigyoushaSearch.Text = " ";
            this.form.HstJigyoushaSearch.UseVisualStyleBackColor = false;
            this.form.HstJigyoushaSearch.ZeroPaddengFlag = false;
            #endregion

            #region 排出事業場
            // add Form
            form.Controls.Add(this.form.HstJigyoujouLabel);
            form.Controls.Add(this.form.HstJigyoujouCd);
            form.Controls.Add(this.form.HstJigyoujouName);
            form.Controls.Add(this.form.HstJigyoujouSearch);

            // Label
            this.form.HstJigyoujouLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.HstJigyoujouLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.HstJigyoujouLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.HstJigyoujouLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.HstJigyoujouLabel.ForeColor = System.Drawing.Color.White;
            this.form.HstJigyoujouLabel.Location = new System.Drawing.Point(this.form.DataConditionLabel.Left, this.form.HstJigyoushaLabel.Top);
            this.form.HstJigyoujouLabel.Name = "HstJigyoujouLabel";
            this.form.HstJigyoujouLabel.Size = new System.Drawing.Size(this.form.label2.Width, 20);
            this.form.HstJigyoujouLabel.TabIndex = this.form.HstJigyoushaSearch.TabIndex + 1;
            this.form.HstJigyoujouLabel.Text = "排出事業場";
            this.form.HstJigyoujouLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // CD
            this.form.HstJigyoujouCd.BackColor = System.Drawing.SystemColors.Window;
            this.form.HstJigyoujouCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.HstJigyoujouCd.ChangeUpperCase = true;
            this.form.HstJigyoujouCd.CharacterLimitList = null;
            this.form.HstJigyoujouCd.CharactersNumber = new decimal(new int[] { 9, 0, 0, 0 });
            this.form.HstJigyoujouCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.HstJigyoujouCd.DisplayItemName = "排出事業場";
            this.form.HstJigyoujouCd.DisplayPopUp = null;
            this.form.HstJigyoujouCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.HstJigyoujouCd.ForeColor = System.Drawing.Color.Black;
            this.form.HstJigyoujouCd.GetCodeMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.form.HstJigyoujouCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.HstJigyoujouCd.IsInputErrorOccured = false;
            tempLeft = this.form.HstJigyoujouLabel.Width + this.form.HstJigyoujouLabel.Left + 2;
            this.form.HstJigyoujouCd.Location = new System.Drawing.Point(tempLeft, this.form.HstJigyoujouLabel.Top);
            this.form.HstJigyoujouCd.MaxLength = 9;
            this.form.HstJigyoujouCd.Name = "HstJigyoujouCd";
            this.form.HstJigyoujouCd.PopupAfterExecute = null;
            this.form.HstJigyoujouCd.PopupBeforeExecute = null;
            this.form.HstJigyoujouCd.PopupGetMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.form.HstJigyoujouCd.PopupSetFormField = "HstJigyoujouCd, HstJigyoujouName";
            this.form.HstJigyoujouCd.prevText = null;
            this.form.HstJigyoujouCd.SetFormField = "HstJigyoujouCd, HstJigyoujouName";
            this.form.HstJigyoujouCd.Size = new System.Drawing.Size(80, 20);
            this.form.HstJigyoujouCd.TabIndex = this.form.HstJigyoujouLabel.TabIndex + 1;
            this.form.HstJigyoujouCd.Tag = "排出事業場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.form.HstJigyoujouCd.ZeroPaddengFlag = true;

            // name
            this.form.HstJigyoujouName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.HstJigyoujouName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.HstJigyoujouName.CharactersNumber = new decimal(new int[] { 32767, 0, 0, 0 });
            this.form.HstJigyoujouName.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.HstJigyoujouName.DisplayPopUp = null;
            this.form.HstJigyoujouName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.HstJigyoujouName.ForeColor = System.Drawing.Color.Black;
            this.form.HstJigyoujouName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.form.HstJigyoujouName.IsInputErrorOccured = false;
            tempLeft = this.form.HstJigyoujouCd.Width + this.form.HstJigyoujouCd.Left - 1;
            this.form.HstJigyoujouName.Location = new System.Drawing.Point(tempLeft, this.form.HstJigyoujouCd.Top);
            this.form.HstJigyoujouName.MaxLength = 0;
            this.form.HstJigyoujouName.Name = "HstJigyoujouName";
            this.form.HstJigyoujouName.PopupAfterExecute = null;
            this.form.HstJigyoujouName.PopupBeforeExecute = null;
            this.form.HstJigyoujouName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.HstJigyoujouName.prevText = null;
            this.form.HstJigyoujouName.ReadOnly = true;
            this.form.HstJigyoujouName.Size = new System.Drawing.Size(160, 20);
            this.form.HstJigyoujouName.TabIndex = this.form.HstJigyoujouCd.TabIndex + 1;
            this.form.HstJigyoujouName.TabStop = false;

            // Search button
            this.form.HstJigyoujouSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.form.HstJigyoujouSearch.CharactersNumber = new decimal(new int[] { 0, 0, 0, 0 });
            this.form.HstJigyoujouSearch.DBFieldsName = null;
            this.form.HstJigyoujouSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.HstJigyoujouSearch.DisplayItemName = null;
            this.form.HstJigyoujouSearch.DisplayPopUp = null;
            this.form.HstJigyoujouSearch.ErrorMessage = null;
            this.form.HstJigyoujouSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.form.HstJigyoujouSearch.GetCodeMasterField = null;
            this.form.HstJigyoujouSearch.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_BUTTON.Image")));
            this.form.HstJigyoujouSearch.ItemDefinedTypes = null;
            this.form.HstJigyoujouSearch.LinkedTextBoxs = null;
            tempLeft = this.form.HstJigyoujouName.Width + this.form.HstJigyoujouName.Left + 5;
            this.form.HstJigyoujouSearch.Location = new System.Drawing.Point(tempLeft, this.form.HstJigyoujouName.Top - 1);
            this.form.HstJigyoujouSearch.Name = "HstJigyoujouSearch";
            this.form.HstJigyoujouSearch.PopupAfterExecute = null;
            this.form.HstJigyoujouSearch.PopupGetMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.form.HstJigyoujouSearch.PopupSetFormField = "HstJigyoujouCd, HstJigyoujouName";
            this.form.HstJigyoujouSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.form.HstJigyoujouSearch.PopupWindowName = "検索共通ポップアップ";
            this.form.HstJigyoujouSearch.SearchDisplayFlag = 0;
            this.form.HstJigyoujouSearch.SetFormField = "HstJigyoujouCd, HstJigyoujouName";
            this.form.HstJigyoujouSearch.ShortItemName = null;
            this.form.HstJigyoujouSearch.Size = new System.Drawing.Size(22, 22);
            this.form.HstJigyoujouSearch.TabIndex = this.form.HstJigyoujouName.TabIndex + 1;
            this.form.HstJigyoujouSearch.TabStop = false;
            this.form.HstJigyoujouSearch.Tag = "排出事業場の検索を行います";
            this.form.HstJigyoujouSearch.Text = " ";
            this.form.HstJigyoujouSearch.UseVisualStyleBackColor = false;
            this.form.HstJigyoujouSearch.ZeroPaddengFlag = false;
            #endregion

            #region 収集運搬業者
            // add Form
            form.Controls.Add(this.form.UpnJigyoushaLabel);
            form.Controls.Add(this.form.UpnJigyoushaCd);
            form.Controls.Add(this.form.UpnJigyoushaName);
            form.Controls.Add(this.form.UpnJigyoushaSearch);

            // Label
            this.form.UpnJigyoushaLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.UpnJigyoushaLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.UpnJigyoushaLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.UpnJigyoushaLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.UpnJigyoushaLabel.ForeColor = System.Drawing.Color.White;
            this.form.UpnJigyoushaLabel.Location = new System.Drawing.Point(this.form.HstJigyoushaLabel.Left, this.form.HstJigyoushaLabel.Top + this.form.HstJigyoushaLabel.Height + 2);
            this.form.UpnJigyoushaLabel.Name = "UpnJigyoushaLabel";
            this.form.UpnJigyoushaLabel.Size = new System.Drawing.Size(this.form.lbl_HikiwatasiBi.Width, 20);
            this.form.UpnJigyoushaLabel.TabIndex = this.form.HstJigyoujouSearch.TabIndex + 1;
            this.form.UpnJigyoushaLabel.Text = "収集運搬業者";
            this.form.UpnJigyoushaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // CD
            this.form.UpnJigyoushaCd.BackColor = System.Drawing.SystemColors.Window;
            this.form.UpnJigyoushaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.UpnJigyoushaCd.ChangeUpperCase = true;
            this.form.UpnJigyoushaCd.CharacterLimitList = null;
            this.form.UpnJigyoushaCd.CharactersNumber = new decimal(new int[] { 7, 0, 0, 0 });
            this.form.UpnJigyoushaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.UpnJigyoushaCd.DisplayItemName = "収集運搬業者";
            this.form.UpnJigyoushaCd.DisplayPopUp = null;
            this.form.UpnJigyoushaCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.UpnJigyoushaCd.ForeColor = System.Drawing.Color.Black;
            this.form.UpnJigyoushaCd.GetCodeMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.UpnJigyoushaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.UpnJigyoushaCd.IsInputErrorOccured = false;
            tempLeft = this.form.UpnJigyoushaLabel.Width + this.form.UpnJigyoushaLabel.Left + 2;
            this.form.UpnJigyoushaCd.Location = new System.Drawing.Point(tempLeft, this.form.UpnJigyoushaLabel.Top);
            this.form.UpnJigyoushaCd.MaxLength = 7;
            this.form.UpnJigyoushaCd.Name = "UpnJigyoushaCd";
            this.form.UpnJigyoushaCd.PopupAfterExecute = null;
            this.form.UpnJigyoushaCd.PopupBeforeExecute = null;
            this.form.UpnJigyoushaCd.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.UpnJigyoushaCd.PopupSetFormField = "UpnJigyoushaCd, UpnJigyoushaName";
            this.form.UpnJigyoushaCd.prevText = null;
            this.form.UpnJigyoushaCd.SetFormField = "UpnJigyoushaCd, UpnJigyoushaName";
            this.form.UpnJigyoushaCd.Size = new System.Drawing.Size(55, 20);
            this.form.UpnJigyoushaCd.TabIndex = this.form.UpnJigyoushaLabel.TabIndex + 1;
            this.form.UpnJigyoushaCd.Tag = "収集運搬業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.form.UpnJigyoushaCd.ZeroPaddengFlag = true;

            // name
            this.form.UpnJigyoushaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.UpnJigyoushaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.UpnJigyoushaName.CharactersNumber = new decimal(new int[] { 32767, 0, 0, 0 });
            this.form.UpnJigyoushaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.UpnJigyoushaName.DisplayPopUp = null;
            this.form.UpnJigyoushaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.UpnJigyoushaName.ForeColor = System.Drawing.Color.Black;
            this.form.UpnJigyoushaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.form.UpnJigyoushaName.IsInputErrorOccured = false;
            tempLeft = this.form.UpnJigyoushaCd.Width + this.form.UpnJigyoushaCd.Left - 1;
            this.form.UpnJigyoushaName.Location = new System.Drawing.Point(tempLeft, this.form.UpnJigyoushaCd.Top);
            this.form.UpnJigyoushaName.MaxLength = 0;
            this.form.UpnJigyoushaName.Name = "UpnJigyoushaName";
            this.form.UpnJigyoushaName.PopupAfterExecute = null;
            this.form.UpnJigyoushaName.PopupBeforeExecute = null;
            this.form.UpnJigyoushaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.UpnJigyoushaName.prevText = null;
            this.form.UpnJigyoushaName.ReadOnly = true;
            this.form.UpnJigyoushaName.Size = new System.Drawing.Size(160, 20);
            this.form.UpnJigyoushaName.TabIndex = this.form.UpnJigyoushaCd.TabIndex + 1;
            this.form.UpnJigyoushaName.TabStop = false;

            // Search button
            this.form.UpnJigyoushaSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.form.UpnJigyoushaSearch.CharactersNumber = new decimal(new int[] { 0, 0, 0, 0 });
            this.form.UpnJigyoushaSearch.DBFieldsName = null;
            this.form.UpnJigyoushaSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.UpnJigyoushaSearch.DisplayItemName = null;
            this.form.UpnJigyoushaSearch.DisplayPopUp = null;
            this.form.UpnJigyoushaSearch.ErrorMessage = null;
            this.form.UpnJigyoushaSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.form.UpnJigyoushaSearch.GetCodeMasterField = null;
            this.form.UpnJigyoushaSearch.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_BUTTON.Image")));
            this.form.UpnJigyoushaSearch.ItemDefinedTypes = null;
            this.form.UpnJigyoushaSearch.LinkedTextBoxs = null;
            tempLeft = this.form.UpnJigyoushaName.Width + this.form.UpnJigyoushaName.Left + 5;
            this.form.UpnJigyoushaSearch.Location = new System.Drawing.Point(tempLeft, this.form.UpnJigyoushaName.Top - 1);
            this.form.UpnJigyoushaSearch.Name = "UpnJigyoushaSearch";
            this.form.UpnJigyoushaSearch.PopupAfterExecute = null;
            this.form.UpnJigyoushaSearch.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.UpnJigyoushaSearch.PopupSetFormField = "UpnJigyoushaCd, UpnJigyoushaName";
            this.form.UpnJigyoushaSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.form.UpnJigyoushaSearch.PopupWindowName = "検索共通ポップアップ";
            this.form.UpnJigyoushaSearch.SearchDisplayFlag = 0;
            this.form.UpnJigyoushaSearch.SetFormField = "UpnJigyoushaCd, UpnJigyoushaName";
            this.form.UpnJigyoushaSearch.ShortItemName = null;
            this.form.UpnJigyoushaSearch.Size = new System.Drawing.Size(22, 22);
            this.form.UpnJigyoushaSearch.TabIndex = this.form.UpnJigyoushaName.TabIndex + 1;
            this.form.UpnJigyoushaSearch.TabStop = false;
            this.form.UpnJigyoushaSearch.Tag = "収集運搬業者の検索を行います";
            this.form.UpnJigyoushaSearch.Text = " ";
            this.form.UpnJigyoushaSearch.UseVisualStyleBackColor = false;
            this.form.UpnJigyoushaSearch.ZeroPaddengFlag = false;
            #endregion

            #region 処分業者
            // add Form
            form.Controls.Add(this.form.SbnJigyoushaLabel);
            form.Controls.Add(this.form.SbnJigyoushaCd);
            form.Controls.Add(this.form.SbnJigyoushaName);
            form.Controls.Add(this.form.SbnJigyoushaSearch);

            // Label
            this.form.SbnJigyoushaLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.SbnJigyoushaLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.SbnJigyoushaLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.SbnJigyoushaLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.SbnJigyoushaLabel.ForeColor = System.Drawing.Color.White;
            this.form.SbnJigyoushaLabel.Location = new System.Drawing.Point(this.form.UpnJigyoushaLabel.Left, this.form.UpnJigyoushaLabel.Top + this.form.UpnJigyoushaLabel.Height + 2);
            this.form.SbnJigyoushaLabel.Name = "SbnJigyoushaLabel";
            this.form.SbnJigyoushaLabel.Size = new System.Drawing.Size(this.form.lbl_HikiwatasiBi.Width, 20);
            this.form.SbnJigyoushaLabel.TabIndex = this.form.UpnJigyoushaSearch.TabIndex + 1;
            this.form.SbnJigyoushaLabel.Text = "処分業者";
            this.form.SbnJigyoushaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // CD
            this.form.SbnJigyoushaCd.BackColor = System.Drawing.SystemColors.Window;
            this.form.SbnJigyoushaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.SbnJigyoushaCd.ChangeUpperCase = true;
            this.form.SbnJigyoushaCd.CharacterLimitList = null;
            this.form.SbnJigyoushaCd.CharactersNumber = new decimal(new int[] { 7, 0, 0, 0 });
            this.form.SbnJigyoushaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.SbnJigyoushaCd.DisplayItemName = "処分業者";
            this.form.SbnJigyoushaCd.DisplayPopUp = null;
            this.form.SbnJigyoushaCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.SbnJigyoushaCd.ForeColor = System.Drawing.Color.Black;
            this.form.SbnJigyoushaCd.GetCodeMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.SbnJigyoushaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.SbnJigyoushaCd.IsInputErrorOccured = false;
            tempLeft = this.form.SbnJigyoushaLabel.Width + this.form.SbnJigyoushaLabel.Left + 2;
            this.form.SbnJigyoushaCd.Location = new System.Drawing.Point(tempLeft, this.form.SbnJigyoushaLabel.Top);
            this.form.SbnJigyoushaCd.MaxLength = 7;
            this.form.SbnJigyoushaCd.Name = "SbnJigyoushaCd";
            this.form.SbnJigyoushaCd.PopupAfterExecuteMethod = "SbnJigyoushaCd_PopupAfterExecuteMethod";
            this.form.SbnJigyoushaCd.PopupAfterExecute = null;
            this.form.SbnJigyoushaCd.PopupBeforeExecuteMethod = "SbnJigyoushaCd_PopupBeforeExecuteMethod";
            this.form.SbnJigyoushaCd.PopupBeforeExecute = null;
            this.form.SbnJigyoushaCd.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.SbnJigyoushaCd.PopupSetFormField = "SbnJigyoushaCd, SbnJigyoushaName";
            this.form.SbnJigyoushaCd.prevText = null;
            this.form.SbnJigyoushaCd.SetFormField = "SbnJigyoushaCd, SbnJigyoushaName";
            this.form.SbnJigyoushaCd.Size = new System.Drawing.Size(55, 20);
            this.form.SbnJigyoushaCd.TabIndex = this.form.SbnJigyoushaLabel.TabIndex + 1;
            this.form.SbnJigyoushaCd.Tag = "処分業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.form.SbnJigyoushaCd.ZeroPaddengFlag = true;

            // name
            this.form.SbnJigyoushaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.SbnJigyoushaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.SbnJigyoushaName.CharactersNumber = new decimal(new int[] { 32767, 0, 0, 0 });
            this.form.SbnJigyoushaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.SbnJigyoushaName.DisplayPopUp = null;
            this.form.SbnJigyoushaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.SbnJigyoushaName.ForeColor = System.Drawing.Color.Black;
            this.form.SbnJigyoushaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.form.SbnJigyoushaName.IsInputErrorOccured = false;
            tempLeft = this.form.SbnJigyoushaCd.Width + this.form.SbnJigyoushaCd.Left - 1;
            this.form.SbnJigyoushaName.Location = new System.Drawing.Point(tempLeft, this.form.SbnJigyoushaCd.Top);
            this.form.SbnJigyoushaName.MaxLength = 0;
            this.form.SbnJigyoushaName.Name = "SbnJigyoushaName";
            this.form.SbnJigyoushaName.PopupAfterExecute = null;
            this.form.SbnJigyoushaName.PopupBeforeExecute = null;
            this.form.SbnJigyoushaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.SbnJigyoushaName.prevText = null;
            this.form.SbnJigyoushaName.ReadOnly = true;
            this.form.SbnJigyoushaName.Size = new System.Drawing.Size(160, 20);
            this.form.SbnJigyoushaName.TabIndex = this.form.SbnJigyoushaCd.TabIndex + 1;
            this.form.SbnJigyoushaName.TabStop = false;

            // Search button
            this.form.SbnJigyoushaSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.form.SbnJigyoushaSearch.CharactersNumber = new decimal(new int[] { 0, 0, 0, 0 });
            this.form.SbnJigyoushaSearch.DBFieldsName = null;
            this.form.SbnJigyoushaSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.SbnJigyoushaSearch.DisplayItemName = null;
            this.form.SbnJigyoushaSearch.DisplayPopUp = null;
            this.form.SbnJigyoushaSearch.ErrorMessage = null;
            this.form.SbnJigyoushaSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.form.SbnJigyoushaSearch.GetCodeMasterField = null;
            this.form.SbnJigyoushaSearch.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_BUTTON.Image")));
            this.form.SbnJigyoushaSearch.ItemDefinedTypes = null;
            this.form.SbnJigyoushaSearch.LinkedTextBoxs = null;
            tempLeft = this.form.SbnJigyoushaName.Width + this.form.SbnJigyoushaName.Left + 5;
            this.form.SbnJigyoushaSearch.Location = new System.Drawing.Point(tempLeft, this.form.SbnJigyoushaName.Top - 1);
            this.form.SbnJigyoushaSearch.Name = "SbnJigyoushaSearch";
            this.form.SbnJigyoushaSearch.PopupAfterExecuteMethod = "SbnJigyoushaCd_PopupAfterExecuteMethod";
            this.form.SbnJigyoushaSearch.PopupAfterExecute = null;
            this.form.SbnJigyoushaSearch.PopupBeforeExecuteMethod = "SbnJigyoushaCd_PopupBeforeExecuteMethod";
            this.form.SbnJigyoushaSearch.PopupBeforeExecute = null;
            this.form.SbnJigyoushaSearch.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.SbnJigyoushaSearch.PopupSetFormField = "SbnJigyoushaCd, SbnJigyoushaName";
            this.form.SbnJigyoushaSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.form.SbnJigyoushaSearch.PopupWindowName = "検索共通ポップアップ";
            this.form.SbnJigyoushaSearch.SearchDisplayFlag = 0;
            this.form.SbnJigyoushaSearch.SetFormField = "SbnJigyoushaCd, SbnJigyoushaName";
            this.form.SbnJigyoushaSearch.ShortItemName = null;
            this.form.SbnJigyoushaSearch.Size = new System.Drawing.Size(22, 22);
            this.form.SbnJigyoushaSearch.TabIndex = this.form.SbnJigyoushaName.TabIndex + 1;
            this.form.SbnJigyoushaSearch.TabStop = false;
            this.form.SbnJigyoushaSearch.Tag = "処分事業者の検索を行います";
            this.form.SbnJigyoushaSearch.Text = " ";
            this.form.SbnJigyoushaSearch.UseVisualStyleBackColor = false;
            this.form.SbnJigyoushaSearch.ZeroPaddengFlag = false;
            #endregion

            #region 処分事業場
            // add Form
            form.Controls.Add(this.form.SbnJigyoujouLabel);
            form.Controls.Add(this.form.SbnJigyoujouCd);
            form.Controls.Add(this.form.SbnJigyoujouName);
            form.Controls.Add(this.form.SbnJigyoujouSearch);

            // Label
            this.form.SbnJigyoujouLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.SbnJigyoujouLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.SbnJigyoujouLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.SbnJigyoujouLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.SbnJigyoujouLabel.ForeColor = System.Drawing.Color.White;
            this.form.SbnJigyoujouLabel.Location = new System.Drawing.Point(this.form.HstJigyoujouLabel.Left, this.form.SbnJigyoushaLabel.Top);
            this.form.SbnJigyoujouLabel.Name = "SbnJigyoujouLabel";
            this.form.SbnJigyoujouLabel.Size = new System.Drawing.Size(this.form.label2.Width, 20);
            this.form.SbnJigyoujouLabel.TabIndex = this.form.SbnJigyoushaSearch.TabIndex + 1;
            this.form.SbnJigyoujouLabel.Text = "処分事業場";
            this.form.SbnJigyoujouLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // CD
            this.form.SbnJigyoujouCd.BackColor = System.Drawing.SystemColors.Window;
            this.form.SbnJigyoujouCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.SbnJigyoujouCd.ChangeUpperCase = true;
            this.form.SbnJigyoujouCd.CharacterLimitList = null;
            this.form.SbnJigyoujouCd.CharactersNumber = new decimal(new int[] { 9, 0, 0, 0 });
            this.form.SbnJigyoujouCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.SbnJigyoujouCd.DisplayItemName = "処分事業場";
            this.form.SbnJigyoujouCd.DisplayPopUp = null;
            this.form.SbnJigyoujouCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.SbnJigyoujouCd.ForeColor = System.Drawing.Color.Black;
            this.form.SbnJigyoujouCd.GetCodeMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.form.SbnJigyoujouCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.SbnJigyoujouCd.IsInputErrorOccured = false;
            tempLeft = this.form.SbnJigyoujouLabel.Width + this.form.SbnJigyoujouLabel.Left + 2;
            this.form.SbnJigyoujouCd.Location = new System.Drawing.Point(tempLeft, this.form.SbnJigyoujouLabel.Top);
            this.form.SbnJigyoujouCd.MaxLength = 9;
            this.form.SbnJigyoujouCd.Name = "SbnJigyoujouCd";
            this.form.SbnJigyoujouCd.PopupAfterExecute = null;
            this.form.SbnJigyoujouCd.PopupBeforeExecute = null;
            this.form.SbnJigyoujouCd.PopupGetMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.form.SbnJigyoujouCd.PopupSetFormField = "SbnJigyoujouCd, SbnJigyoujouName";
            this.form.SbnJigyoujouCd.prevText = null;
            this.form.SbnJigyoujouCd.SetFormField = "SbnJigyoujouCd, SbnJigyoujouName";
            this.form.SbnJigyoujouCd.Size = new System.Drawing.Size(80, 20);
            this.form.SbnJigyoujouCd.TabIndex = this.form.SbnJigyoujouLabel.TabIndex + 1;
            this.form.SbnJigyoujouCd.Tag = "処分事業場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.form.SbnJigyoujouCd.ZeroPaddengFlag = true;

            // name
            this.form.SbnJigyoujouName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.SbnJigyoujouName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.SbnJigyoujouName.CharactersNumber = new decimal(new int[] { 32767, 0, 0, 0 });
            this.form.SbnJigyoujouName.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.SbnJigyoujouName.DisplayPopUp = null;
            this.form.SbnJigyoujouName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.SbnJigyoujouName.ForeColor = System.Drawing.Color.Black;
            this.form.SbnJigyoujouName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.form.SbnJigyoujouName.IsInputErrorOccured = false;
            tempLeft = this.form.SbnJigyoujouCd.Width + this.form.SbnJigyoujouCd.Left - 1;
            this.form.SbnJigyoujouName.Location = new System.Drawing.Point(tempLeft, this.form.SbnJigyoujouCd.Top);
            this.form.SbnJigyoujouName.MaxLength = 0;
            this.form.SbnJigyoujouName.Name = "SbnJigyoujouName";
            this.form.SbnJigyoujouName.PopupAfterExecute = null;
            this.form.SbnJigyoujouName.PopupBeforeExecute = null;
            this.form.SbnJigyoujouName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.SbnJigyoujouName.prevText = null;
            this.form.SbnJigyoujouName.ReadOnly = true;
            this.form.SbnJigyoujouName.Size = new System.Drawing.Size(160, 20);
            this.form.SbnJigyoujouName.TabIndex = this.form.SbnJigyoujouCd.TabIndex + 1;
            this.form.SbnJigyoujouName.TabStop = false;

            // Search button
            this.form.SbnJigyoujouSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.form.SbnJigyoujouSearch.CharactersNumber = new decimal(new int[] { 0, 0, 0, 0 });
            this.form.SbnJigyoujouSearch.DBFieldsName = null;
            this.form.SbnJigyoujouSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.SbnJigyoujouSearch.DisplayItemName = null;
            this.form.SbnJigyoujouSearch.DisplayPopUp = null;
            this.form.SbnJigyoujouSearch.ErrorMessage = null;
            this.form.SbnJigyoujouSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.form.SbnJigyoujouSearch.GetCodeMasterField = null;
            this.form.SbnJigyoujouSearch.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_BUTTON.Image")));
            this.form.SbnJigyoujouSearch.ItemDefinedTypes = null;
            this.form.SbnJigyoujouSearch.LinkedTextBoxs = null;
            tempLeft = this.form.SbnJigyoujouName.Width + this.form.SbnJigyoujouName.Left + 5;
            this.form.SbnJigyoujouSearch.Location = new System.Drawing.Point(tempLeft, this.form.SbnJigyoujouName.Top - 1);
            this.form.SbnJigyoujouSearch.Name = "SbnJigyoujouSearch";
            this.form.SbnJigyoujouSearch.PopupAfterExecute = null;
            this.form.SbnJigyoujouSearch.PopupGetMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.form.SbnJigyoujouSearch.PopupSetFormField = "SbnJigyoujouCd, SbnJigyoujouName";
            this.form.SbnJigyoujouSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.form.SbnJigyoujouSearch.PopupWindowName = "検索共通ポップアップ";
            this.form.SbnJigyoujouSearch.SearchDisplayFlag = 0;
            this.form.SbnJigyoujouSearch.SetFormField = "SbnJigyoujouCd, SbnJigyoujouName";
            this.form.SbnJigyoujouSearch.ShortItemName = null;
            this.form.SbnJigyoujouSearch.Size = new System.Drawing.Size(22, 22);
            this.form.SbnJigyoujouSearch.TabIndex = this.form.SbnJigyoujouName.TabIndex + 1;
            this.form.SbnJigyoujouSearch.TabStop = false;
            this.form.SbnJigyoujouSearch.Tag = "処分事業場の検索を行います";
            this.form.SbnJigyoujouSearch.Text = " ";
            this.form.SbnJigyoujouSearch.UseVisualStyleBackColor = false;
            this.form.SbnJigyoujouSearch.ZeroPaddengFlag = false;
            #endregion

            #region 最終処分業者
            // add Form
            form.Controls.Add(this.form.LastSbnJigyoushaLabel);
            form.Controls.Add(this.form.LastSbnJigyoushaCd);
            form.Controls.Add(this.form.LastSbnJigyoushaName);
            form.Controls.Add(this.form.LastSbnJigyoushaSearch);

            // TODO: 最終処分情報は一旦保留。最終処分情報の補助データ作成をサポートするタイミングで復活させる。
            this.form.LastSbnJigyoushaLabel.Visible = false;
            this.form.LastSbnJigyoushaCd.Visible = false;
            this.form.LastSbnJigyoushaName.Visible = false;
            this.form.LastSbnJigyoushaSearch.Visible = false;

            // Label
            this.form.LastSbnJigyoushaLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.LastSbnJigyoushaLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LastSbnJigyoushaLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.LastSbnJigyoushaLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LastSbnJigyoushaLabel.ForeColor = System.Drawing.Color.White;
            this.form.LastSbnJigyoushaLabel.Location = new System.Drawing.Point(this.form.SbnJigyoushaLabel.Left, this.form.SbnJigyoushaLabel.Height + this.form.SbnJigyoushaLabel.Top + 2);
            this.form.LastSbnJigyoushaLabel.Name = "LastSbnJigyoushaLabel";
            this.form.LastSbnJigyoushaLabel.Size = new System.Drawing.Size(110, 20);
            this.form.LastSbnJigyoushaLabel.TabIndex = this.form.SbnJigyoujouSearch.TabIndex + 1;
            this.form.LastSbnJigyoushaLabel.Text = "最終処分事業者";
            this.form.LastSbnJigyoushaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // CD
            this.form.LastSbnJigyoushaCd.BackColor = System.Drawing.SystemColors.Window;
            this.form.LastSbnJigyoushaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LastSbnJigyoushaCd.ChangeUpperCase = true;
            this.form.LastSbnJigyoushaCd.CharacterLimitList = null;
            this.form.LastSbnJigyoushaCd.CharactersNumber = new decimal(new int[] { 7, 0, 0, 0 });
            this.form.LastSbnJigyoushaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.LastSbnJigyoushaCd.DisplayItemName = "最終処分事業者";
            this.form.LastSbnJigyoushaCd.DisplayPopUp = null;
            this.form.LastSbnJigyoushaCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LastSbnJigyoushaCd.ForeColor = System.Drawing.Color.Black;
            this.form.LastSbnJigyoushaCd.GetCodeMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.LastSbnJigyoushaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.LastSbnJigyoushaCd.IsInputErrorOccured = false;
            tempLeft = this.form.LastSbnJigyoushaLabel.Width + this.form.LastSbnJigyoushaLabel.Left + 2;
            this.form.LastSbnJigyoushaCd.Location = new System.Drawing.Point(tempLeft, this.form.LastSbnJigyoushaLabel.Top);
            this.form.LastSbnJigyoushaCd.MaxLength = 7;
            this.form.LastSbnJigyoushaCd.Name = "LastSbnJigyoushaCd";
            this.form.LastSbnJigyoushaCd.PopupAfterExecuteMethod = "LastSbnJigyoushaCd_PopupAfterExecuteMethod";
            this.form.LastSbnJigyoushaCd.PopupAfterExecute = null;
            this.form.LastSbnJigyoushaCd.PopupBeforeExecuteMethod = "LastSbnJigyoushaCd_PopupBeforeExecuteMethod";
            this.form.LastSbnJigyoushaCd.PopupBeforeExecute = null;
            this.form.LastSbnJigyoushaCd.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.LastSbnJigyoushaCd.PopupSetFormField = "LastSbnJigyoushaCd, LastSbnJigyoushaName";
            this.form.LastSbnJigyoushaCd.prevText = null;
            this.form.LastSbnJigyoushaCd.SetFormField = "LastSbnJigyoushaCd, LastSbnJigyoushaName";
            this.form.LastSbnJigyoushaCd.Size = new System.Drawing.Size(55, 20);
            this.form.LastSbnJigyoushaCd.TabIndex = this.form.LastSbnJigyoushaLabel.TabIndex + 1;
            this.form.LastSbnJigyoushaCd.Tag = "最終処分事業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.form.LastSbnJigyoushaCd.ZeroPaddengFlag = true;

            // name
            this.form.LastSbnJigyoushaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.LastSbnJigyoushaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LastSbnJigyoushaName.CharactersNumber = new decimal(new int[] { 32767, 0, 0, 0 });
            this.form.LastSbnJigyoushaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.LastSbnJigyoushaName.DisplayPopUp = null;
            this.form.LastSbnJigyoushaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LastSbnJigyoushaName.ForeColor = System.Drawing.Color.Black;
            this.form.LastSbnJigyoushaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.form.LastSbnJigyoushaName.IsInputErrorOccured = false;
            tempLeft = this.form.LastSbnJigyoushaCd.Width + this.form.LastSbnJigyoushaCd.Left - 1;
            this.form.LastSbnJigyoushaName.Location = new System.Drawing.Point(tempLeft, this.form.LastSbnJigyoushaCd.Top);
            this.form.LastSbnJigyoushaName.MaxLength = 0;
            this.form.LastSbnJigyoushaName.Name = "LastSbnJigyoushaName";
            this.form.LastSbnJigyoushaName.PopupAfterExecute = null;
            this.form.LastSbnJigyoushaName.PopupBeforeExecute = null;
            this.form.LastSbnJigyoushaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.LastSbnJigyoushaName.prevText = null;
            this.form.LastSbnJigyoushaName.ReadOnly = true;
            this.form.LastSbnJigyoushaName.Size = new System.Drawing.Size(160, 20);
            this.form.LastSbnJigyoushaName.TabIndex = this.form.LastSbnJigyoushaCd.TabIndex + 1;
            this.form.LastSbnJigyoushaName.TabStop = false;

            // Search button
            this.form.LastSbnJigyoushaSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.form.LastSbnJigyoushaSearch.CharactersNumber = new decimal(new int[] { 0, 0, 0, 0 });
            this.form.LastSbnJigyoushaSearch.DBFieldsName = null;
            this.form.LastSbnJigyoushaSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.LastSbnJigyoushaSearch.DisplayItemName = null;
            this.form.LastSbnJigyoushaSearch.DisplayPopUp = null;
            this.form.LastSbnJigyoushaSearch.ErrorMessage = null;
            this.form.LastSbnJigyoushaSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.form.LastSbnJigyoushaSearch.GetCodeMasterField = null;
            this.form.LastSbnJigyoushaSearch.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_BUTTON.Image")));
            this.form.LastSbnJigyoushaSearch.ItemDefinedTypes = null;
            this.form.LastSbnJigyoushaSearch.LinkedTextBoxs = null;
            tempLeft = this.form.LastSbnJigyoushaName.Width + this.form.LastSbnJigyoushaName.Left + 5;
            this.form.LastSbnJigyoushaSearch.Location = new System.Drawing.Point(tempLeft, this.form.LastSbnJigyoushaName.Top - 1);
            this.form.LastSbnJigyoushaSearch.Name = "LastSbnJigyoushaSearch";
            this.form.LastSbnJigyoushaSearch.PopupAfterExecuteMethod = "LastSbnJigyoushaCd_PopupAfterExecuteMethod";
            this.form.LastSbnJigyoushaSearch.PopupAfterExecute = null;
            this.form.LastSbnJigyoushaSearch.PopupBeforeExecuteMethod = "LastSbnJigyoushaCd_PopupBeforeExecuteMethod";
            this.form.LastSbnJigyoushaSearch.PopupBeforeExecute = null;
            this.form.LastSbnJigyoushaSearch.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.LastSbnJigyoushaSearch.PopupSetFormField = "LastSbnJigyoushaCd, LastSbnJigyoushaName";
            this.form.LastSbnJigyoushaSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.form.LastSbnJigyoushaSearch.PopupWindowName = "検索共通ポップアップ";
            this.form.LastSbnJigyoushaSearch.SearchDisplayFlag = 0;
            this.form.LastSbnJigyoushaSearch.SetFormField = "LastSbnJigyoushaCd, LastSbnJigyoushaName";
            this.form.LastSbnJigyoushaSearch.ShortItemName = null;
            this.form.LastSbnJigyoushaSearch.Size = new System.Drawing.Size(22, 22);
            this.form.LastSbnJigyoushaSearch.TabIndex = this.form.LastSbnJigyoushaName.TabIndex + 1;
            this.form.LastSbnJigyoushaSearch.TabStop = false;
            this.form.LastSbnJigyoushaSearch.Tag = "最終処分事業者の検索を行います";
            this.form.LastSbnJigyoushaSearch.Text = " ";
            this.form.LastSbnJigyoushaSearch.UseVisualStyleBackColor = false;
            this.form.LastSbnJigyoushaSearch.ZeroPaddengFlag = false;
            #endregion

            #region 最終処分事業場
            form.Controls.Add(this.form.LastSbnJigyoujouLabel);
            form.Controls.Add(this.form.LastSbnJigyoujouCd);
            form.Controls.Add(this.form.LastSbnJigyoujouName);
            form.Controls.Add(this.form.LastSbnJigyoujouSearch);

            // TODO: 最終処分情報は一旦保留。最終処分情報の補助データ作成をサポートするタイミングで復活させる。
            this.form.LastSbnJigyoujouLabel.Visible = false;
            this.form.LastSbnJigyoujouCd.Visible = false;
            this.form.LastSbnJigyoujouName.Visible = false;
            this.form.LastSbnJigyoujouSearch.Visible = false;

            // Label
            this.form.LastSbnJigyoujouLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.LastSbnJigyoujouLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LastSbnJigyoujouLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.LastSbnJigyoujouLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LastSbnJigyoujouLabel.ForeColor = System.Drawing.Color.White;
            tempLeft = this.form.LastSbnJigyoushaSearch.Left + this.form.LastSbnJigyoushaSearch.Width + 5;
            this.form.LastSbnJigyoujouLabel.Location = new System.Drawing.Point(tempLeft, this.form.LastSbnJigyoushaLabel.Top);
            this.form.LastSbnJigyoujouLabel.Name = "LastSbnJigyoujouLabel";
            this.form.LastSbnJigyoujouLabel.Size = new System.Drawing.Size(110, 20);
            this.form.LastSbnJigyoujouLabel.TabIndex = this.form.LastSbnJigyoushaSearch.TabIndex + 1;
            this.form.LastSbnJigyoujouLabel.Text = "最終処分事業場";
            this.form.LastSbnJigyoujouLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // CD
            this.form.LastSbnJigyoujouCd.BackColor = System.Drawing.SystemColors.Window;
            this.form.LastSbnJigyoujouCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LastSbnJigyoujouCd.ChangeUpperCase = true;
            this.form.LastSbnJigyoujouCd.CharacterLimitList = null;
            this.form.LastSbnJigyoujouCd.CharactersNumber = new decimal(new int[] { 9, 0, 0, 0 });
            this.form.LastSbnJigyoujouCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.LastSbnJigyoujouCd.DisplayItemName = "最終処分事業場";
            this.form.LastSbnJigyoujouCd.DisplayPopUp = null;
            this.form.LastSbnJigyoujouCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LastSbnJigyoujouCd.ForeColor = System.Drawing.Color.Black;
            this.form.LastSbnJigyoujouCd.GetCodeMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.form.LastSbnJigyoujouCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.LastSbnJigyoujouCd.IsInputErrorOccured = false;
            tempLeft = this.form.LastSbnJigyoujouLabel.Width + this.form.LastSbnJigyoujouLabel.Left + 2;
            this.form.LastSbnJigyoujouCd.Location = new System.Drawing.Point(tempLeft, this.form.LastSbnJigyoujouLabel.Top);
            this.form.LastSbnJigyoujouCd.MaxLength = 9;
            this.form.LastSbnJigyoujouCd.Name = "LastSbnJigyoujouCd";
            this.form.LastSbnJigyoujouCd.PopupAfterExecute = null;
            this.form.LastSbnJigyoujouCd.PopupBeforeExecute = null;
            this.form.LastSbnJigyoujouCd.PopupGetMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.form.LastSbnJigyoujouCd.PopupSetFormField = "LastSbnJigyoujouCd, LastSbnJigyoujouName";
            this.form.LastSbnJigyoujouCd.prevText = null;
            this.form.LastSbnJigyoujouCd.SetFormField = "LastSbnJigyoujouCd, LastSbnJigyoujouName";
            this.form.LastSbnJigyoujouCd.Size = new System.Drawing.Size(80, 20);
            this.form.LastSbnJigyoujouCd.TabIndex = this.form.LastSbnJigyoujouLabel.TabIndex + 1;
            this.form.LastSbnJigyoujouCd.Tag = "最終処分事業場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.form.LastSbnJigyoujouCd.ZeroPaddengFlag = true;

            // name
            this.form.LastSbnJigyoujouName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.LastSbnJigyoujouName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LastSbnJigyoujouName.CharactersNumber = new decimal(new int[] { 32767, 0, 0, 0 });
            this.form.LastSbnJigyoujouName.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.LastSbnJigyoujouName.DisplayPopUp = null;
            this.form.LastSbnJigyoujouName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LastSbnJigyoujouName.ForeColor = System.Drawing.Color.Black;
            this.form.LastSbnJigyoujouName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.form.LastSbnJigyoujouName.IsInputErrorOccured = false;
            tempLeft = this.form.LastSbnJigyoujouCd.Width + this.form.LastSbnJigyoujouCd.Left - 1;
            this.form.LastSbnJigyoujouName.Location = new System.Drawing.Point(tempLeft, this.form.LastSbnJigyoujouCd.Top);
            this.form.LastSbnJigyoujouName.MaxLength = 0;
            this.form.LastSbnJigyoujouName.Name = "LastSbnJigyoujouName";
            this.form.LastSbnJigyoujouName.PopupAfterExecute = null;
            this.form.LastSbnJigyoujouName.PopupBeforeExecute = null;
            this.form.LastSbnJigyoujouName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.LastSbnJigyoujouName.prevText = null;
            this.form.LastSbnJigyoujouName.ReadOnly = true;
            this.form.LastSbnJigyoujouName.Size = new System.Drawing.Size(160, 20);
            this.form.LastSbnJigyoujouName.TabIndex = this.form.LastSbnJigyoujouCd.TabIndex + 1;
            this.form.LastSbnJigyoujouName.TabStop = false;

            // Search button
            this.form.LastSbnJigyoujouSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.form.LastSbnJigyoujouSearch.CharactersNumber = new decimal(new int[] { 0, 0, 0, 0 });
            this.form.LastSbnJigyoujouSearch.DBFieldsName = null;
            this.form.LastSbnJigyoujouSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.LastSbnJigyoujouSearch.DisplayItemName = null;
            this.form.LastSbnJigyoujouSearch.DisplayPopUp = null;
            this.form.LastSbnJigyoujouSearch.ErrorMessage = null;
            this.form.LastSbnJigyoujouSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.form.LastSbnJigyoujouSearch.GetCodeMasterField = null;
            this.form.LastSbnJigyoujouSearch.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_BUTTON.Image")));
            this.form.LastSbnJigyoujouSearch.ItemDefinedTypes = null;
            this.form.LastSbnJigyoujouSearch.LinkedTextBoxs = null;
            tempLeft = this.form.LastSbnJigyoujouName.Width + this.form.LastSbnJigyoujouName.Left + 5;
            this.form.LastSbnJigyoujouSearch.Location = new System.Drawing.Point(tempLeft, this.form.LastSbnJigyoujouName.Top - 1);
            this.form.LastSbnJigyoujouSearch.Name = "LastSbnJigyoujouSearch";
            this.form.LastSbnJigyoujouSearch.PopupAfterExecute = null;
            this.form.LastSbnJigyoujouSearch.PopupGetMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.form.LastSbnJigyoujouSearch.PopupSetFormField = "LastSbnJigyoujouCd, LastSbnJigyoujouName";
            this.form.LastSbnJigyoujouSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.form.LastSbnJigyoujouSearch.PopupWindowName = "検索共通ポップアップ";
            this.form.LastSbnJigyoujouSearch.SearchDisplayFlag = 0;
            this.form.LastSbnJigyoujouSearch.SetFormField = "LastSbnJigyoujouCd, LastSbnJigyoujouName";
            this.form.LastSbnJigyoujouSearch.ShortItemName = null;
            this.form.LastSbnJigyoujouSearch.Size = new System.Drawing.Size(22, 22);
            this.form.LastSbnJigyoujouSearch.TabIndex = this.form.LastSbnJigyoujouName.TabIndex + 1;
            this.form.LastSbnJigyoujouSearch.TabStop = false;
            this.form.LastSbnJigyoujouSearch.Tag = "最終処分事業場の検索を行います";
            this.form.LastSbnJigyoujouSearch.Text = " ";
            this.form.LastSbnJigyoujouSearch.UseVisualStyleBackColor = false;
            this.form.LastSbnJigyoujouSearch.ZeroPaddengFlag = false;
            #endregion

            #region setting for popupSetting and validatingEvent
            // setting for popupSetting and validatingEvent

            // 排出
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.HstJigyoushaCd, this.form.HstJigyoushaName, this.form.HstJigyoushaSearch,
                this.form.HstJigyoujouCd, this.form.HstJigyoujouName, this.form.HstJigyoujouSearch,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.DENSHI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.HAISHUTSU_NIZUMI_GYOUSHA | Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA, false, false,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.HAISHUTSU_NIZUMI_GENBA,
                true, true, true);

            // ヒントテキストのみ再設定
            this.form.HstJigyoushaCd.Tag = "半角7文字以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.form.HstJigyoujouCd.Tag = "半角10文字以内で入力してください（スペースキー押下にて、検索画面を表示します）";

            // 収集運搬
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.UpnJigyoushaCd, this.form.UpnJigyoushaName, this.form.UpnJigyoushaSearch,
                null, null, null,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.DENSHI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.UNPAN_JUTAKUSHA_KAISHA, false, false,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.NONE,
                false, true, false);

            // ヒントテキストのみ再設定
            this.form.UpnJigyoushaCd.Tag = "半角7文字以内で入力してください（スペースキー押下にて、検索画面を表示します）";

            // 処分
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.SbnJigyoushaCd, this.form.SbnJigyoushaName, this.form.SbnJigyoushaSearch,
                this.form.SbnJigyoujouCd, this.form.SbnJigyoujouName, this.form.SbnJigyoujouSearch,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.DENSHI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA, false, false,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.SHOBUN_NIOROSHI_GENBA,
                true, true, true);

            // ヒントテキストのみ再設定
            this.form.SbnJigyoushaCd.Tag = "半角7文字以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.form.SbnJigyoujouCd.Tag = "半角10文字以内で入力してください（スペースキー押下にて、検索画面を表示します）";

            // 最終処分
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.LastSbnJigyoushaCd, this.form.LastSbnJigyoushaName, this.form.LastSbnJigyoushaSearch,
                this.form.LastSbnJigyoujouCd, this.form.LastSbnJigyoujouName, this.form.LastSbnJigyoujouSearch,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.DENSHI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA, false, false,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.SAISHUU_SHOBUNJOU,
                true, true, true);
            #endregion

            #region マスタ情報
            // Formの下から計算
            this.form.panel2.Height = 60;
            this.form.panel2.Top += 20;
            this.form.lbl_Jigyoujou.Top = this.form.panel2.Height - (this.form.lbl_Jigyoujou.Height + 6);
            this.form.cantxt_JigyoujouCd.Top = this.form.lbl_Jigyoujou.Top;
            this.form.ctxt_JigyoujouName.Top = this.form.lbl_Jigyoujou.Top;
            this.form.lbl_Jigyousha.Top = (this.form.lbl_Jigyoujou.Top - 2) - this.form.lbl_Jigyousha.Height;
            this.form.cantxt_JigyoushaCd.Top = this.form.lbl_Jigyousha.Top;
            this.form.ctxt_JigyoushaName.Top = this.form.lbl_Jigyousha.Top;
            this.form.lbl_MasterInfo.Top = this.form.panel2.Top - 9;
            #endregion

            #region 将軍連携マスタ
            this.form.lbl_ShogunMaster.Top = this.form.lbl_MasterInfo.Top;
            this.form.lbl_Gyousha.Top = this.form.lbl_Jigyousha.Top;
            this.form.cantxt_GyoushaCd.Top = this.form.lbl_Gyousha.Top;
            this.form.ctxt_GyoushaName.Top = this.form.lbl_Gyousha.Top;
            this.form.lbl_Genba.Top = this.form.lbl_Jigyoujou.Top;
            this.form.cantxt_GenbaCd.Top = this.form.lbl_Genba.Top;
            this.form.ctxt_GenbaName.Top = this.form.lbl_Genba.Top;
            this.form.panel3.Top = this.form.panel2.Top;
            this.form.panel3.Height = this.form.panel2.Height;
            #endregion

            #region 選択先
            this.form.lbl_SentakuSaki.Top += 25;
            this.form.ctxt_Sentakusaki.Top = this.form.lbl_SentakuSaki.Top;
            #endregion

            #region Ichiran
            var top = this.form.SbnJigyoujouLabel.Top + this.form.SbnJigyoujouLabel.Height + 5;
            var heigth = this.form.Height - top - (this.form.Height - this.form.ctxt_Sentakusaki.Top) - 5;
            this.form.Ichiran.Location = new Point(this.form.Ichiran.Left, top);
            this.form.Ichiran.Size = new Size(this.form.Ichiran.Width, heigth);
            this.form.Ichiran.TabIndex = this.form.LastSbnJigyoujouSearch.TabIndex + 1;
            #endregion
        }
        #endregion

        /// <summary>
        /// 紐付有無判定
        /// </summary>
        private bool IsNullOrEmpty(object obj)
        {
            if (obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// カレント行で将軍連携マスタが設定されていないか判定
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns></returns>
        private bool HasErrorRenkeiMaster(DataRow searchResult)
        {
            bool result = false;
            
            if (!this.IsNullOrEmpty(searchResult["KANRI_ID"]))
            {
                if (this.IsNullOrEmpty(searchResult["EDI_MEMBER_ID"])
                    || this.IsNullOrEmpty(searchResult["GYOUSHA_CD"]))
                {
                    // 排出事業者
                    result = true;
                }

                if (this.IsNullOrEmpty(searchResult["JIGYOUJOU_CD"])
                    || this.IsNullOrEmpty(searchResult["GENBA_CD"])
                    || this.IsNullOrEmpty(searchResult["GYOUSHA_CD"]))
                {
                    // 排出事業場
                    result = true;
                }
            }
            if (!this.IsNullOrEmpty(searchResult["SU1_KANRI_ID"]))
            {
                if (this.IsNullOrEmpty(searchResult["SU1_EDI_MEMBER_ID"])
                    || this.IsNullOrEmpty(searchResult["SU1_GYOUSHA_CD"]))
                {
                    // 収集運搬業者1
                    result = true;
                }

                if (Convert.ToInt32(searchResult["UPN_ROUTE_CNT"]) > 1)
                {
                    if (this.IsNullOrEmpty(searchResult["US1_EDI_MEMBER_ID"])
                        || this.IsNullOrEmpty(searchResult["US1_GYOUSHA_CD"]))
                    {
                        // 運搬先業者1
                        result = true;
                    }

                    if (this.IsNullOrEmpty(searchResult["US1_JIGYOUJOU_CD"])
                        || this.IsNullOrEmpty(searchResult["US1_GENBA_CD"])
                        || this.IsNullOrEmpty(searchResult["US1_GYOUSHA_CD"]))
                    {
                        // 運搬先事業場1
                        result = true;
                    }
                }
            }

            if (!this.IsNullOrEmpty(searchResult["SU2_KANRI_ID"]))
            {
                if (this.IsNullOrEmpty(searchResult["SU2_EDI_MEMBER_ID"])
                    || this.IsNullOrEmpty(searchResult["SU2_GYOUSHA_CD"]))
                {
                    // 収集運搬業者2
                    result = true;
                }

                if (Convert.ToInt32(searchResult["UPN_ROUTE_CNT"]) > 2)
                {
                    if (this.IsNullOrEmpty(searchResult["US2_EDI_MEMBER_ID"])
                        || this.IsNullOrEmpty(searchResult["US2_GYOUSHA_CD"]))
                    {
                        // 運搬先業者2
                        result = true;
                    }

                    if (this.IsNullOrEmpty(searchResult["US2_JIGYOUJOU_CD"])
                        || this.IsNullOrEmpty(searchResult["US2_GENBA_CD"])
                        || this.IsNullOrEmpty(searchResult["US2_GYOUSHA_CD"]))
                    {
                        // 運搬先事業場2
                        result = true;
                    }
                }
            }

            if (!this.IsNullOrEmpty(searchResult["SU3_KANRI_ID"]))
            {
                if (this.IsNullOrEmpty(searchResult["SU3_EDI_MEMBER_ID"])
                    || this.IsNullOrEmpty(searchResult["SU3_GYOUSHA_CD"]))
                {
                    // 収集運搬業者3
                    result = true;
                }

                if (Convert.ToInt32(searchResult["UPN_ROUTE_CNT"]) > 3)
                {
                    if (this.IsNullOrEmpty(searchResult["US3_EDI_MEMBER_ID"])
                        || this.IsNullOrEmpty(searchResult["US3_GYOUSHA_CD"]))
                    {
                        // 運搬先業者3
                        result = true;
                    }

                    if (this.IsNullOrEmpty(searchResult["US3_JIGYOUJOU_CD"])
                        || this.IsNullOrEmpty(searchResult["US3_GENBA_CD"])
                        || this.IsNullOrEmpty(searchResult["US3_GYOUSHA_CD"]))
                    {
                        // 運搬先事業場3
                        result = true;
                    }
                }
            }

            if (!this.IsNullOrEmpty(searchResult["SU4_KANRI_ID"]))
            {
                if (this.IsNullOrEmpty(searchResult["SU4_EDI_MEMBER_ID"])
                    || this.IsNullOrEmpty(searchResult["SU4_GYOUSHA_CD"]))
                {
                    // 収集運搬業者4
                    result = true;
                }

                if (Convert.ToInt32(searchResult["UPN_ROUTE_CNT"]) > 4)
                {
                    if (this.IsNullOrEmpty(searchResult["US4_EDI_MEMBER_ID"])
                        || this.IsNullOrEmpty(searchResult["US4_GYOUSHA_CD"]))
                    {
                        // 運搬先業者4
                        result = true;
                    }

                    if (this.IsNullOrEmpty(searchResult["US4_JIGYOUJOU_CD"])
                        || this.IsNullOrEmpty(searchResult["US4_GENBA_CD"])
                        || this.IsNullOrEmpty(searchResult["US4_GYOUSHA_CD"]))
                    {
                        // 運搬先事業場4
                        result = true;
                    }
                }
            }

            if (!this.IsNullOrEmpty(searchResult["SU5_KANRI_ID"]))
            {
                if (this.IsNullOrEmpty(searchResult["SU5_EDI_MEMBER_ID"])
                    || this.IsNullOrEmpty(searchResult["SU5_GYOUSHA_CD"]))
                {
                    // 収集運搬業者5
                    result = true;
                }
            }

            if (!this.IsNullOrEmpty(searchResult["SB_KANRI_ID"]))
            {
                if (this.IsNullOrEmpty(searchResult["SB_EDI_MEMBER_ID"])
                    || this.IsNullOrEmpty(searchResult["SB_GYOUSHA_CD"]))
                {
                    // 処分業者
                    result = true;
                }

                if (this.IsNullOrEmpty(searchResult["SB_JIGYOUJOU_CD"])
                    || this.IsNullOrEmpty(searchResult["SB_GENBA_CD"])
                    || this.IsNullOrEmpty(searchResult["SB_GYOUSHA_CD"]))
                {
                    // 処分事業場
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 拡張データDTO作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private DTODTExClass CreateDtEx(DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            DTODTExClass dto = new DTODTExClass();
            dto.dtR18ExEntity = CreateDtR18Ex(row);
            dto.dtR19ExEntityList = CreateDtR19Ex(row);
            dto.dtR04ExEntityList = CreateDtR04Ex(row);
            dto.dtR08ExEntityList = CreateDtR08Ex(row);
            dto.dtR13ExEntityList = CreateDtR13Ex(row);

            return dto;
        }

        /// <summary>
        /// 拡張データ(DT_R18_EX)作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private DT_R18_EX CreateDtR18Ex(DataRow row)
        {
            // ''→報告担当者CD
            // ''→処分担当者CD
            // ''→運搬担当者CD
            // ''→車輌CD
            DT_R18_EX entity = R18EXDao.GetDataByKanriId(row["KANRI_ID"].ToString());
            if (entity == null)
            {
                entity = new DT_R18_EX();
                entity.KANRI_ID = row["KANRI_ID"].ToString();
            }
            entity.MANIFEST_ID = row["MANIFEST_ID"].ToString();

            // 排出事業者の加入者番号＋排出事業場名称＋排出事業場所在地1＋2＋3＋4：電子事業場：→排出事業場CDと排出事業者CD
            {
                string hstShaEdiMemberId = row["HST_SHA_EDI_MEMBER_ID"].ToString();
                // Where句の文字列に対してエスケープシーケンス対策を行う
                string hstJouName = SqlCreateUtility.CounterplanEscapeSequence2(row["HST_JOU_NAME"].ToString());
                string hstJouAddress = SqlCreateUtility.CounterplanEscapeSequence2(row["HST_JOU_ADDRESS1"].ToString() +
                                       row["HST_JOU_ADDRESS2"].ToString() +
                                       row["HST_JOU_ADDRESS3"].ToString() +
                                       row["HST_JOU_ADDRESS4"].ToString());

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM M_DENSHI_JIGYOUJOU ");
                sb.Append("WHERE ");
                sb.AppendFormat(" EDI_MEMBER_ID = '{0}' ", hstShaEdiMemberId);
                sb.AppendFormat(" AND JIGYOUJOU_NAME = '{0}' ", hstJouName);
                sb.AppendFormat(" AND (ISNULL(JIGYOUJOU_ADDRESS1,'')+ISNULL(JIGYOUJOU_ADDRESS2,'')+ISNULL(JIGYOUJOU_ADDRESS3,'')+ISNULL(JIGYOUJOU_ADDRESS4,'')) = '{0}' ", hstJouAddress);

                DataTable dt = MJJDao.GetDataForEntity(sb.ToString());
                if (dt != null && 0 < dt.Rows.Count)
                {
                    entity.HST_GENBA_CD = dt.Rows[0]["GENBA_CD"].ToString();
                    entity.HST_GYOUSHA_CD = dt.Rows[0]["GYOUSHA_CD"].ToString();
                }
                else
                {
                    entity.HST_GENBA_CD = null;
                    entity.HST_GYOUSHA_CD = null;
                }
            }

            // 廃棄物の名称：電子廃棄物名称：→廃棄物名称CD
            {
                string hstShaEdiMemberId = row["HST_SHA_EDI_MEMBER_ID"].ToString();
                // Where句の文字列に対してエスケープシーケンス対策を行う
                string haikiName = SqlCreateUtility.CounterplanEscapeSequence2(row["HAIKI_NAME"].ToString());

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM M_DENSHI_HAIKI_NAME ");
                sb.Append("WHERE ");
                sb.AppendFormat("EDI_MEMBER_ID = '{0}' ", hstShaEdiMemberId);
                sb.AppendFormat("AND HAIKI_NAME = '{0}' ", haikiName);

                DataTable dt = MHNDao.GetDataForEntity(sb.ToString());
                if (dt != null && 0 < dt.Rows.Count)
                {
                    entity.HAIKI_NAME_CD = dt.Rows[0]["HAIKI_NAME_CD"].ToString();
                }
                else
                {
                    entity.HAIKI_NAME_CD = null;
                }
            }

            // 処分方法コード：処分方法：→処分方法CD
            {
                string exSbnHouHouCd = row["EX_SBN_HOUHOU_CD"].ToString();

                if (string.IsNullOrEmpty(exSbnHouHouCd))
                {
                    string sbnWayCode = row["SBN_WAY_CODE"].ToString();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM M_SHOBUN_HOUHOU ");
                    sb.Append("WHERE ");
                    sb.AppendFormat("SHOBUN_HOUHOU_CD = '{0}'", sbnWayCode);

                    DataTable dt = SHOBUNHDao.GetDataForEntity(sb.ToString());
                    if (dt != null && 0 < dt.Rows.Count)
                    {
                        entity.SBN_HOUHOU_CD = dt.Rows[0]["SHOBUN_HOUHOU_CD"].ToString();
                    }
                    else
                    {
                        entity.SBN_HOUHOU_CD = null;
                    }
                }
                else
                {
                    // （将軍）処分方法が設定されている場合、ユーザが設定した値の可能性があるため引き継ぐ
                    entity.SBN_HOUHOU_CD = exSbnHouHouCd;
                }
            }

            // 運搬先加入者番号＋運搬先事業場名＋運搬先事業場所在地1＋2＋3＋4：電子事業場：→処分事業場CDと処分受託者CD
            {
                string upnsakiEdiMemberId = row["SB_UPNSAKI_EDI_MEMBER_ID"].ToString();
                // Where句の文字列に対してエスケープシーケンス対策を行う
                string upnsakiJouName = SqlCreateUtility.CounterplanEscapeSequence2(row["SB_UPNSAKI_JOU_NAME"].ToString());
                string upnsakiJouAddress = SqlCreateUtility.CounterplanEscapeSequence2(row["SB_UPNSAKI_JOU_ADDRESS1"].ToString() +
                                           row["SB_UPNSAKI_JOU_ADDRESS2"].ToString() +
                                           row["SB_UPNSAKI_JOU_ADDRESS3"].ToString() +
                                           row["SB_UPNSAKI_JOU_ADDRESS4"].ToString());

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM M_DENSHI_JIGYOUJOU ");
                sb.Append("WHERE ");
                sb.AppendFormat(" EDI_MEMBER_ID = '{0}' ", upnsakiEdiMemberId);
                sb.AppendFormat(" AND JIGYOUJOU_NAME = '{0}' ", upnsakiJouName);
                sb.AppendFormat(" AND (ISNULL(JIGYOUJOU_ADDRESS1,'')+ISNULL(JIGYOUJOU_ADDRESS2,'')+ISNULL(JIGYOUJOU_ADDRESS3,'')+ISNULL(JIGYOUJOU_ADDRESS4,'')) = '{0}' ", upnsakiJouAddress);

                DataTable dt = MJJDao.GetDataForEntity(sb.ToString());
                if (dt != null && 0 < dt.Rows.Count)
                {
                    entity.SBN_GENBA_CD = dt.Rows[0]["GENBA_CD"].ToString();
                    entity.SBN_GYOUSHA_CD = dt.Rows[0]["GYOUSHA_CD"].ToString();
                }
                else
                {
                    entity.SBN_GENBA_CD = null;
                    entity.SBN_GYOUSHA_CD = null;
                }
            }

            ManifestoLogic maniLogic = new ManifestoLogic();

            // 換算後数量　※電子マニフェスト入力画面の機能より引用
            {
                #region 確定数量取得
                var sysInfos = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData();
                M_SYS_INFO sysInfo = sysInfos != null && sysInfos.Count() > 0 ? sysInfos[0] : new M_SYS_INFO();

                SqlDecimal kakuteiSuu = 0;
                string kakuteiUnitCd = string.Empty;
                bool isEmptySuuryou = true;

                if (sysInfo != null && !sysInfo.MANIFEST_REPORT_SUU_KBN.IsNull)
                {
                    switch (sysInfo.MANIFEST_REPORT_SUU_KBN.ToString())
                    {
                        case "1":
                            // 1.確定数量
                            // 数量確定者CDにより、どの数量を使うかを判定し換算値計算
                            //数量確定者コード
                            string SUU_KAKUTEI_CODE = string.Empty;
                            var tmpObjHaiki = row["SUU_KAKUTEI_CODE"];
                            if (!this.IsNullOrEmpty(tmpObjHaiki))
                            {
                                SUU_KAKUTEI_CODE = tmpObjHaiki.ToString();
                            }
                            else
                            {
                                SUU_KAKUTEI_CODE = "01";
                            }

                            // 数量確定者が排出事業者の場合
                            if (SUU_KAKUTEI_CODE == "01")
                            {
                                var tmpHaikiSuu = row["HAIKI_SUU"];
                                var tmpUnpUnit = row["HAIKI_UNIT_CODE"];
                                if (!this.IsNullOrEmpty(tmpHaikiSuu))
                                {
                                    kakuteiSuu = SqlDecimal.Parse(tmpHaikiSuu.ToString());
                                    isEmptySuuryou = false;
                                }
                                if (!this.IsNullOrEmpty(tmpUnpUnit))
                                {
                                    kakuteiUnitCd = tmpUnpUnit.ToString();
                                }
                            }
                            //数量確定者が処分事業者の場合
                            else if (SUU_KAKUTEI_CODE == "02")
                            {
                                var tmpHaikiSuu = row["RECEPT_SUU"];
                                var tmpUnpUnit = row["RECEPT_UNIT_CODE"];
                                if (!this.IsNullOrEmpty(tmpHaikiSuu))
                                {
                                    //確定数量
                                    kakuteiSuu = SqlDecimal.Parse(tmpHaikiSuu.ToString());
                                    isEmptySuuryou = false;
                                }
                                if (!this.IsNullOrEmpty(tmpUnpUnit))
                                {
                                    //確定数量の単位
                                    kakuteiUnitCd = tmpUnpUnit.ToString();
                                }
                            }
                            //確定者が運搬業者の場合
                            else
                            {
                                //確定者CD(3～7で区間を判断する)
                                int nConfirmCd = int.Parse(SUU_KAKUTEI_CODE);
                                int no = nConfirmCd - 2;

                                // 廃棄物の確定数量
                                var upnSuu = row["SU" + no + "_UPN_SUU"];
                                if (!this.IsNullOrEmpty(upnSuu) && SqlDecimal.Parse(upnSuu.ToString()) != 0)
                                {
                                    kakuteiSuu = SqlDecimal.Parse(upnSuu.ToString());
                                    isEmptySuuryou = false;
                                }
                                else
                                {
                                    var tmpHaikiSuu = row["HAIKI_SUU"];
                                    if (!this.IsNullOrEmpty(tmpHaikiSuu))
                                    {
                                        kakuteiSuu = SqlDecimal.Parse(tmpHaikiSuu.ToString());
                                    }
                                }

                                var upnUnitCode = row["SU" + no + "_UPN_UNIT_CODE"];
                                if (!this.IsNullOrEmpty(upnUnitCode))
                                {
                                    //廃棄物の確定数量の単位コード
                                    kakuteiUnitCd = upnUnitCode.ToString();
                                }
                                else
                                {
                                    var tmpUnpUnit = row["HAIKI_UNIT_CODE"];
                                    if (!this.IsNullOrEmpty(tmpUnpUnit))
                                    {
                                        kakuteiUnitCd = tmpUnpUnit.ToString();
                                    }
                                }
                            }
                            break;

                        case "2":
                            // 2.排出事業者
                            kakuteiSuu = SqlDecimal.Parse(row["HAIKI_SUU"].ToString());
                            kakuteiUnitCd = row["HAIKI_UNIT_CODE"].ToString();
                            isEmptySuuryou = false;
                            break;

                        case "3":
                            // 3.収集運搬業者
                            // 区間5からチェックし、EDI_PASSWORDが存在した区間の運搬量を使用する
                            for (int i = 4; i >= 0; i-- )
                            {
                                int no = i + 1;
                                string upnShaEdiPass = row["SU" + no + "_UPN_SHA_EDI_PASSWORD"].ToString();
                                if (!string.IsNullOrEmpty(upnShaEdiPass))
                                {
                                    if (!string.IsNullOrEmpty(row["SU" + no + "_UPN_SUU"].ToString()))
                                    {
                                        kakuteiSuu = SqlDecimal.Parse(row["SU" + no + "_UPN_SUU"].ToString());
                                        kakuteiUnitCd = row["SU" + no + "_UPN_UNIT_CODE"].ToString();
                                        isEmptySuuryou = false;
                                    }
                                    break;
                                }
                            }
                            break;

                        case "4":
                            // 4.処分事業者
                            if (!string.IsNullOrEmpty(row["RECEPT_SUU"].ToString()))
                            {
                                kakuteiSuu = SqlDecimal.Parse(row["RECEPT_SUU"].ToString());
                                kakuteiUnitCd = row["RECEPT_UNIT_CODE"].ToString();
                                isEmptySuuryou = false;
                            }
                            break;

                        default:
                            break;
                    }
                }

                if (isEmptySuuryou)
                {
                    kakuteiSuu = SqlDecimal.Parse(row["HAIKI_SUU"].ToString());
                    kakuteiUnitCd = row["HAIKI_UNIT_CODE"].ToString();
                }
                #endregion

                var dto = new DenshiSearchParameterDtoCls();

                string haikisyuruyiCd = string.Empty;
                {
                    var sb = new StringBuilder();
                    if (!this.IsNullOrEmpty(row["HAIKI_DAI_CODE"]))
                    {
                        sb.Append(row["HAIKI_DAI_CODE"].ToString());
                    }
                    if (!this.IsNullOrEmpty(row["HAIKI_CHU_CODE"]))
                    {
                        sb.Append(row["HAIKI_CHU_CODE"].ToString());
                    }
                    if (!this.IsNullOrEmpty(row["HAIKI_SHO_CODE"]))
                    {
                        sb.Append(row["HAIKI_SHO_CODE"].ToString());
                    }

                    haikisyuruyiCd = sb.ToString();
                }

                if (haikisyuruyiCd != null)
                {
                    dto.HAIKI_SHURUI_CD = haikisyuruyiCd;
                }
                else
                {
                    dto.HAIKI_SHURUI_CD = string.Empty;
                }

                if (entity.HAIKI_NAME_CD != null)
                {
                    dto.HAIKI_NAME_CD = entity.HAIKI_NAME_CD;
                }
                else
                {
                    dto.HAIKI_NAME_CD = string.Empty;
                }

                if (kakuteiUnitCd != null)
                {
                    dto.UNIT_CD = kakuteiUnitCd;
                }
                else
                {
                    dto.UNIT_CD = string.Empty;
                }

                if (row["NISUGATA_CODE"] != null)
                {
                    dto.NISUGATA_CD = row["NISUGATA_CODE"].ToString();
                }
                else
                {
                    dto.NISUGATA_CD = string.Empty;
                }

                var dataLogic = new DenshiMasterDataLogic();
                DataTable tbl = dataLogic.GetDenmaniKansanData(dto);
                if (tbl.Rows.Count == 1)
                {   //換算式の取得
                    if (tbl.Rows[0]["KANSANCHI"] != null)
                    {
                        string val = tbl.Rows[0]["KANSANCHI"].ToString();

                        //乗算式
                        if (tbl.Rows[0]["KANSANSHIKI"].ToString() == "0")
                        {
                            entity.KANSAN_SUU = maniLogic.Round((decimal)SqlDecimal.Multiply(kakuteiSuu, SqlDecimal.Parse(val)), SystemProperty.Format.ManifestSuuryou);
                        }
                        //除算式
                        else
                        {
                            entity.KANSAN_SUU = maniLogic.Round((decimal)SqlDecimal.Divide(kakuteiSuu, SqlDecimal.Parse(val)), SystemProperty.Format.ManifestSuuryou);
                        }
                    }
                }
                else
                {
                    entity.KANSAN_SUU = SqlDecimal.Null;
                }
            }

            // 減算後数量 電マニ入力画面から引用
            // ※Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.LogicClass#GetGenYougou_suuメソッドより引用
            {
                SqlDecimal genyou_suu = entity.KANSAN_SUU;
                if (!genyou_suu.IsNull && genyou_suu != 0)
                {
                    //減容率の取得
                    SqlDecimal GENNYOURITSU = 0;
                    SearchDTOForDTExClass dto = new SearchDTOForDTExClass();
                    //廃棄物種類CDが画面から取得する
                    string haikisyuruyiCd = string.Empty;
                    {
                        var sb = new StringBuilder();
                        if (!this.IsNullOrEmpty(row["HAIKI_DAI_CODE"]))
                        {
                            sb.Append(row["HAIKI_DAI_CODE"].ToString());
                        }
                        if (!this.IsNullOrEmpty(row["HAIKI_CHU_CODE"]))
                        {
                            sb.Append(row["HAIKI_CHU_CODE"].ToString());
                        }
                        if (!this.IsNullOrEmpty(row["HAIKI_SHO_CODE"]))
                        {
                            sb.Append(row["HAIKI_SHO_CODE"].ToString());
                        }

                        haikisyuruyiCd = sb.ToString();
                    }

                    DataTable tbl = new DataTable();

                    //報告書分類＋廃棄物名称＋処分方法＋減容率 で検索
                    if (!string.IsNullOrEmpty(haikisyuruyiCd)
                        && !string.IsNullOrEmpty(entity.HAIKI_NAME_CD)
                        && !string.IsNullOrEmpty(entity.SBN_HOUHOU_CD))
                    {
                        dto.HAIKI_SHURUI_CD = haikisyuruyiCd.ToString().Substring(0, 4);
                        dto.HAIKI_NAME_CD = entity.HAIKI_NAME_CD;
                        dto.SHOBUN_HOUHOU_CD = entity.SBN_HOUHOU_CD;

                        tbl = this.R18Dao.GetGenYourituData(dto);
                    }

                    // 報告書分類＋処分方法＋減容率
                    if (tbl.Rows.Count < 1)
                    {
                        if (!string.IsNullOrEmpty(haikisyuruyiCd)
                            && !string.IsNullOrEmpty(entity.SBN_HOUHOU_CD))
                        {
                            dto.HAIKI_SHURUI_CD = haikisyuruyiCd.ToString().Substring(0, 4);
                            dto.HAIKI_NAME_CD = string.Empty;
                            dto.SHOBUN_HOUHOU_CD = entity.SBN_HOUHOU_CD;

                            tbl = this.R18Dao.GetGenYourituData(dto);
                        }
                    }

                    // 報告書分類＋減容率
                    if (tbl.Rows.Count < 1)
                    {
                        if (!string.IsNullOrEmpty(haikisyuruyiCd))
                        {
                            dto.HAIKI_SHURUI_CD = haikisyuruyiCd.ToString().Substring(0, 4);
                            dto.HAIKI_NAME_CD = string.Empty;
                            dto.SHOBUN_HOUHOU_CD = string.Empty;
                            tbl = this.R18Dao.GetGenYourituData(dto);
                        }
                    }

                    if (tbl.Rows.Count == 1
                        && tbl.Rows[0]["GENNYOURITSU"] != null)
                    {   //減容率の取得
                        GENNYOURITSU = SqlDecimal.Parse(tbl.Rows[0]["GENNYOURITSU"].ToString());
                        genyou_suu = (decimal)(SqlDecimal.Divide(SqlDecimal.Multiply(entity.KANSAN_SUU, 100 - GENNYOURITSU), 100.00m));
                        genyou_suu = maniLogic.Round((decimal)genyou_suu, SystemProperty.Format.ManifestSuuryou);
                    }
                }

                entity.GENNYOU_SUU = genyou_suu;
            }

            return entity;
        }

        /// <summary>
        /// 拡張データ(DT_R19_EX)作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private DT_R19_EX[] CreateDtR19Ex(DataRow row)
        {
            // ''→運搬担当者CD
            // ''→車輌CD
            // ''→運搬報告記載の運搬担当者CD
            // ''→運搬報告記載の車輌CD
            // ''→報告担当者CD
            DT_R19_EX[] entityList = R19EXDao.GetDataByKanriId(row["KANRI_ID"].ToString());

            bool isNew = (entityList.Length == 0);

            if (isNew)
            {
                // 新規登録時は「DT_R18:収集運搬情報件数」分の作成
                int cnt = int.Parse(row["UPN_ROUTE_CNT"].ToString());
                entityList = new DT_R19_EX[cnt];
            }

            for (int i = 0; i < entityList.Length; i++)
            {
                int no = i + 1;

                if (isNew)
                {
                    entityList[i] = new DT_R19_EX();

                    // 区間番号
                    entityList[i].UPN_ROUTE_NO = no;
                    entityList[i].KANRI_ID = row["KANRI_ID"].ToString();
                }

                // 収集運搬業者加入者番号：電子事業者：→収集運搬業者CD
                string upnShaEdiMemnerId = row["SU" + no + "_UPN_SHA_EDI_MEMBER_ID"].ToString();
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM M_DENSHI_JIGYOUSHA ");
                    sb.AppendFormat("WHERE EDI_MEMBER_ID = '{0}'", upnShaEdiMemnerId);

                    DataTable dt = MJSDao.GetDataForEntity(sb.ToString());
                    if (dt != null && 0 < dt.Rows.Count)
                    {
                        entityList[i].UPN_GYOUSHA_CD = dt.Rows[0]["GYOUSHA_CD"].ToString();
                    }
                    else
                    {
                        entityList[i].UPN_GYOUSHA_CD = null;
                    }
                }

                string upnsakiEdiMemberId = string.Empty;

                // 運搬先加入者番号：電子事業者：→運搬先業者CD
                if (no == 5)
                {
                    // 5レコード目は処分業者カラムから取得
                    upnsakiEdiMemberId = row["SB_UPNSAKI_EDI_MEMBER_ID"].ToString();
                }
                else
                {
                    upnsakiEdiMemberId = row["SU" + no + "_UPNSAKI_EDI_MEMBER_ID"].ToString();
                }

                // 運搬先加入者番号＋運搬先事業場名＋運搬先事業場所在地1＋2＋3＋4：電子事業場：→運搬先事業場CDと運搬先業者CD
                string upnsakiJouName = string.Empty;
                string upnsakiJouAddress = string.Empty;
                if (no == 5)
                {
                    // 5レコード目は処分業者カラムから取得
                    upnsakiJouName = SqlCreateUtility.CounterplanEscapeSequence2(row["SB_UPNSAKI_JOU_NAME"].ToString());
                    upnsakiJouAddress = SqlCreateUtility.CounterplanEscapeSequence2(row["SB_UPNSAKI_JOU_ADDRESS1"].ToString() +
                                        row["SB_UPNSAKI_JOU_ADDRESS2"].ToString() +
                                        row["SB_UPNSAKI_JOU_ADDRESS3"].ToString() +
                                        row["SB_UPNSAKI_JOU_ADDRESS4"].ToString());
                }
                else
                {
                    upnsakiJouName = SqlCreateUtility.CounterplanEscapeSequence2(row["SU" + no + "_UPNSAKI_JOU_NAME"].ToString());
                    upnsakiJouAddress = SqlCreateUtility.CounterplanEscapeSequence2(row["SU" + no + "_UPNSAKI_JOU_ADDRESS1"].ToString() +
                                               row["SU" + no + "_UPNSAKI_JOU_ADDRESS2"].ToString() +
                                               row["SU" + no + "_UPNSAKI_JOU_ADDRESS3"].ToString() +
                                               row["SU" + no + "_UPNSAKI_JOU_ADDRESS4"].ToString());
                }

                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM M_DENSHI_JIGYOUJOU ");
                    sb.Append("WHERE ");
                    sb.AppendFormat(" EDI_MEMBER_ID = '{0}' ", upnsakiEdiMemberId);
                    sb.AppendFormat(" AND JIGYOUJOU_NAME = '{0}' ", upnsakiJouName);
                    sb.AppendFormat(" AND (ISNULL(JIGYOUJOU_ADDRESS1,'')+ISNULL(JIGYOUJOU_ADDRESS2,'')+ISNULL(JIGYOUJOU_ADDRESS3,'')+ISNULL(JIGYOUJOU_ADDRESS4,'')) = '{0}' ", upnsakiJouAddress);

                    DataTable dt = MJJDao.GetDataForEntity(sb.ToString());
                    if (dt != null && 0 < dt.Rows.Count)
                    {
                        entityList[i].UPNSAKI_GENBA_CD = dt.Rows[0]["GENBA_CD"].ToString();
                        entityList[i].UPNSAKI_GYOUSHA_CD = dt.Rows[0]["GYOUSHA_CD"].ToString();
                    }
                    else
                    {
                        entityList[i].UPNSAKI_GENBA_CD = null;
                        entityList[i].UPNSAKI_GYOUSHA_CD = null;
                    }
                }
            }
            return entityList;
        }

        /// <summary>
        /// 拡張データ(DT_R04_EX)作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private DT_R04_EX[] CreateDtR04Ex(DataRow row)
        {
            // ''→最終処分業者CD
            // ''→最終処分事業場CD
            DT_R04_EX[] entityList = R04EXDao.GetDataByKanriId(row["KANRI_ID"].ToString());

            bool isNew = (entityList.Length == 0);

            int maxSeq = R04EXDao.GetMaxSeqByKanriId(row["KANRI_ID"].ToString());

            if (isNew)
            {
                // 新規登録時は「DT_R04:レコード連番」分の作成
                int cnt = 0;
                if (!this.IsNullOrEmpty(row["R04_REC_SEQ_COUNT"]))
                {
                    cnt = int.Parse(row["R04_REC_SEQ_COUNT"].ToString());
                }
                entityList = new DT_R04_EX[cnt];
            }

            for (int i = 0; i < entityList.Length; i++)
            {
                int no = i + 1;

                if (isNew)
                {
                    entityList[i] = new DT_R04_EX();

                    // 区間番号
                    entityList[i].REC_SEQ = no;
                    entityList[i].KANRI_ID = row["KANRI_ID"].ToString();

                    if (0 < maxSeq)
                    {
                        entityList[i].MANIFEST_ID = row["MANIFEST_ID"].ToString();
                    }
                }
            }
            return entityList;
        }

        /// <summary>
        /// 拡張データ(DT_R08_EX)作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private DT_R08_EX[] CreateDtR08Ex(DataRow row)
        {
            // ''→排出事業者CD
            // ''→排出事業場CD
            // ''→廃棄物種類CD
            DT_R08_EX[] entityList = R08EXDao.GetDataByKanriId(row["KANRI_ID"].ToString());

            bool isNew = (entityList.Length == 0);

            if (isNew)
            {
                // 新規登録時は「DT_R08:レコード連番」分の作成
                int cnt = 0;
                if (!this.IsNullOrEmpty(row["R08_REC_SEQ_COUNT"]))
                {
                    cnt = int.Parse(row["R08_REC_SEQ_COUNT"].ToString());
                }
                entityList = new DT_R08_EX[cnt];
            }

            for (int i = 0; i < entityList.Length; i++)
            {
                int no = i + 1;

                if (isNew)
                {
                    entityList[i] = new DT_R08_EX();

                    // 区間番号
                    entityList[i].REC_SEQ = no;
                    entityList[i].KANRI_ID = row["KANRI_ID"].ToString();
                }
            }
            return entityList;
        }

        /// <summary>
        /// 拡張データ(DT_R13_EX)作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private DT_R13_EX[] CreateDtR13Ex(DataRow row)
        {
            // ''→最終処分業者CD
            // ''→最終処分事業場CD
            DT_R13_EX[] entityList = R13EXDao.GetDataByKanriId(row["KANRI_ID"].ToString());

            bool isNew = (entityList.Length == 0);

            if (isNew)
            {
                // 新規登録時は「DT_R13:レコード連番」分の作成
                int cnt = 0;
                if (!this.IsNullOrEmpty(row["R13_REC_SEQ_COUNT"]))
                {
                    cnt = int.Parse(row["R13_REC_SEQ_COUNT"].ToString());
                }
                entityList = new DT_R13_EX[cnt];
            }

            for (int i = 0; i < entityList.Length; i++)
            {
                int no = i + 1;

                if (isNew)
                {
                    entityList[i] = new DT_R13_EX();

                    // 区間番号
                    entityList[i].REC_SEQ = no;
                    entityList[i].KANRI_ID = row["KANRI_ID"].ToString();
                }
            }
            return entityList;
        }

        /// <summary>
        /// 拡張データ(DT_R18_EX)登録
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="kanriId"></param>
        /// <returns>システムID</returns>
        private SqlInt64 InsertDT_R18_EX(DTODTExClass dto, string kanriId)
        {
            DT_R18_EX r18Entity = R18EXDao.GetDataByKanriId(kanriId);

            SqlInt64 systemId = SqlInt64.Null;
            SqlInt32 seq = SqlInt32.Null;

            if (r18Entity != null)
            {
                systemId = r18Entity.SYSTEM_ID;
                seq = r18Entity.SEQ + 1;

                r18Entity.DELETE_FLG = SqlBoolean.True;

                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //r18Entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                r18Entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                r18Entity.UPDATE_USER = SystemProperty.UserName;
                r18Entity.UPDATE_PC = SystemInformation.ComputerName;

                R18EXDao.Update(r18Entity);
            }
            else
            {
                DBAccessor dbAcc = new DBAccessor();
                systemId = dbAcc.createSystemId(SqlInt16.Parse(DENSHU_KBN.DENSHI_MANIFEST.GetHashCode().ToString()));
                seq = 1;
            }

            dto.dtR18ExEntity.SYSTEM_ID = systemId;
            dto.dtR18ExEntity.SEQ = seq;

            var dataBinderEntry = new DataBinderLogic<DT_R18_EX>(dto.dtR18ExEntity);
            dataBinderEntry.SetSystemProperty(dto.dtR18ExEntity, false);

            dto.dtR18ExEntity.DELETE_FLG = SqlBoolean.False;

            R18EXDao.Insert(dto.dtR18ExEntity);

            return systemId;
        }

        /// <summary>
        /// 拡張データ(DT_R19_EX)登録
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="kanriId"></param>
        /// <param name="systemId"></param>
        private void InsertDT_R19_EX(DTODTExClass dto, string kanriId, SqlInt64 systemId)
        {
            DT_R19_EX[] r19EntityList = R19EXDao.GetDataByKanriId(kanriId);
            SqlInt32 seq = SqlInt32.Null;

            if (0 < r19EntityList.Length)
            {
                seq = r19EntityList[0].SEQ + 1;

                foreach (var r19Entity in r19EntityList)
                {
                    r19Entity.DELETE_FLG = SqlBoolean.True;

                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //r19Entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    r19Entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    r19Entity.UPDATE_USER = SystemProperty.UserName;
                    r19Entity.UPDATE_PC = SystemInformation.ComputerName;

                    R19EXDao.Update(r19Entity);
                }
            }
            else
            {
                seq = 1;
            }

            foreach (var dtR19ExEntity in dto.dtR19ExEntityList)
            {
                dtR19ExEntity.SYSTEM_ID = systemId;
                dtR19ExEntity.SEQ = seq;

                var dataBinderEntry = new DataBinderLogic<DT_R19_EX>(dtR19ExEntity);
                dataBinderEntry.SetSystemProperty(dtR19ExEntity, false);

                dtR19ExEntity.DELETE_FLG = SqlBoolean.False;

                R19EXDao.Insert(dtR19ExEntity);
            }
        }

        /// <summary>
        /// 拡張データ(DT_R04_EX)登録
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="kanriId"></param>
        /// <param name="systemId"></param>
        private void InsertDT_R04_EX(DTODTExClass dto, string kanriId, SqlInt64 systemId)
        {
            DT_R04_EX[] r04EntityList = R04EXDao.GetDataByKanriId(kanriId);
            SqlInt32 seq = SqlInt32.Null;

            if (0 < r04EntityList.Length)
            {
                seq = r04EntityList[0].SEQ + 1;

                foreach (var r04Entity in r04EntityList)
                {
                    r04Entity.DELETE_FLG = SqlBoolean.True;

                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //r04Entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    r04Entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    r04Entity.UPDATE_USER = SystemProperty.UserName;
                    r04Entity.UPDATE_PC = SystemInformation.ComputerName;

                    R04EXDao.Update(r04Entity);
                }
            }
            else
            {
                var resultSeq = R04EXDao.GetMaxSeqByKanriId(kanriId);
                seq = (0 < resultSeq) ? dto.dtR18ExEntity.SEQ : 1;
            }

            foreach (var dtR04ExEntity in dto.dtR04ExEntityList)
            {
                dtR04ExEntity.SYSTEM_ID = systemId;
                dtR04ExEntity.SEQ = seq;

                var dataBinderEntry = new DataBinderLogic<DT_R04_EX>(dtR04ExEntity);
                dataBinderEntry.SetSystemProperty(dtR04ExEntity, false);

                dtR04ExEntity.DELETE_FLG = SqlBoolean.False;

                R04EXDao.Insert(dtR04ExEntity);
            }
        }

        /// <summary>
        /// 拡張データ(DT_R08_EX)登録
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="kanriId"></param>
        /// <param name="systemId"></param>
        private void InsertDT_R08_EX(DTODTExClass dto, string kanriId, SqlInt64 systemId)
        {
            DT_R08_EX[] r08EntityList = R08EXDao.GetDataByKanriId(kanriId);

            if (0 < r08EntityList.Length)
            {

                foreach (var r08Entity in r08EntityList)
                {
                    r08Entity.DELETE_FLG = SqlBoolean.True;

                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //r08Entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    r08Entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    r08Entity.UPDATE_USER = SystemProperty.UserName;
                    r08Entity.UPDATE_PC = SystemInformation.ComputerName;

                    R08EXDao.Update(r08Entity);
                }
            }

            foreach (var dtR08ExEntity in dto.dtR08ExEntityList)
            {
                dtR08ExEntity.SYSTEM_ID = systemId;
                dtR08ExEntity.SEQ = dto.dtR18ExEntity.SEQ;

                var dataBinderEntry = new DataBinderLogic<DT_R08_EX>(dtR08ExEntity);
                dataBinderEntry.SetSystemProperty(dtR08ExEntity, false);

                dtR08ExEntity.DELETE_FLG = SqlBoolean.False;

                R08EXDao.Insert(dtR08ExEntity);
            }
        }

        /// <summary>
        /// 拡張データ(DT_R13_EX)登録
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="kanriId"></param>
        /// <param name="systemId"></param>
        private void InsertDT_R13_EX(DTODTExClass dto, string kanriId, SqlInt64 systemId)
        {
            DT_R13_EX[] r13EntityList = R13EXDao.GetDataByKanriId(kanriId);

            if (0 < r13EntityList.Length)
            {
                foreach (var r13Entity in r13EntityList)
                {
                    r13Entity.DELETE_FLG = SqlBoolean.True;

                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //r13Entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    r13Entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    r13Entity.UPDATE_USER = SystemProperty.UserName;
                    r13Entity.UPDATE_PC = SystemInformation.ComputerName;

                    R13EXDao.Update(r13Entity);
                }
            }

            foreach (var dtR13ExEntity in dto.dtR13ExEntityList)
            {
                dtR13ExEntity.SYSTEM_ID = systemId;
                dtR13ExEntity.SEQ = dto.dtR18ExEntity.SEQ;

                var dataBinderEntry = new DataBinderLogic<DT_R13_EX>(dtR13ExEntity);
                dataBinderEntry.SetSystemProperty(dtR13ExEntity, false);

                dtR13ExEntity.DELETE_FLG = SqlBoolean.False;

                R13EXDao.Insert(dtR13ExEntity);
            }
        }
        #endregion

        #region 電子廃棄物種類細分類マスタ関連
        /// <summary>
        /// 電子廃棄物種類細分類マスタ登録用のデータを作成する。
        /// 既にDBに登録されているデータの場合はInsertしない。
        /// </summary>
        /// <param name="row"></param>
        /// <returns>既にDBに登録されている場合nullを返す。また、DB検索に必要なデータが揃っていない場合もnullを返す。</returns>
        private M_DENSHI_HAIKI_SHURUI_SAIBUNRUI CreateDenSaibunruiDto(DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            // 必要なデータをチェック
            /* 
             * 排出事業者の加入者番号
             * 廃棄物種類CD(大分類CD + 中分類CD + 小分類CD )
             * 廃棄物種類細分類CD
             * 廃棄物の種類
             */
            if ((row["HST_SHA_EDI_MEMBER_ID"] == null || string.IsNullOrEmpty(row["HST_SHA_EDI_MEMBER_ID"].ToString()))
                || (row["HAIKI_DAI_CODE"] == null || string.IsNullOrEmpty(row["HAIKI_DAI_CODE"].ToString()))
                || (row["HAIKI_CHU_CODE"] == null || string.IsNullOrEmpty(row["HAIKI_CHU_CODE"].ToString()))
                || (row["HAIKI_SHO_CODE"] == null || string.IsNullOrEmpty(row["HAIKI_SHO_CODE"].ToString()))
                || (row["HAIKI_SAI_CODE"] == null || string.IsNullOrEmpty(row["HAIKI_SAI_CODE"].ToString()))
                || (row["HAIKI_SHURUI"] == null || string.IsNullOrEmpty(row["HAIKI_SHURUI"].ToString())))
            {
                return null;
            }

            // 細分類CD=000は登録しない
            if ("000".Equals(row["HAIKI_SAI_CODE"].ToString()))
            {
                return null;
            }

            var searchCondition = new M_DENSHI_HAIKI_SHURUI_SAIBUNRUI();
            searchCondition.EDI_MEMBER_ID = row["HST_SHA_EDI_MEMBER_ID"].ToString();
            searchCondition.HAIKI_SHURUI_CD = row["HAIKI_DAI_CODE"].ToString() + row["HAIKI_CHU_CODE"].ToString() + row["HAIKI_SHO_CODE"].ToString();
            searchCondition.HAIKI_SHURUI_SAIBUNRUI_CD = row["HAIKI_SAI_CODE"].ToString();

            /* 登録用の電子廃棄物種類細分類マスタデータを作成 */
            // 電子廃棄物種類を取得
            var searchShurui = new M_DENSHI_HAIKI_SHURUI();
            searchShurui.HAIKI_SHURUI_CD = searchCondition.HAIKI_SHURUI_CD;
            var resultHaikiShurui = this.haikiShuruiDao.GetAllValidData(searchShurui);

            // 電子廃棄物種類が存在しない場合、作りようがない。
            if (resultHaikiShurui == null || resultHaikiShurui.Length < 1)
            {
                return null;
            }

            var haikiShrui = resultHaikiShurui[0];
            searchCondition.ISNOT_NEED_DELETE_FLG = true;
            M_DENSHI_HAIKI_SHURUI_SAIBUNRUI dto = null;
            var searchResult = this.haikiShuruiSaibunruiDao.GetAllValidData(searchCondition);
            if (searchResult != null & searchResult.Length > 0)
            {
                dto = searchResult.First();
            }
            else
            {
                dto = new M_DENSHI_HAIKI_SHURUI_SAIBUNRUI();
                dto.HAIKI_KBN = haikiShrui.HAIKI_KBN;
                dto.KONGOU_KBN = haikiShrui.KONGOU_KBN;
                dto.HOUKOKUSHO_BUNRUI_CD = haikiShrui.HOUKOKUSHO_BUNRUI_CD;
                dto.DELETE_FLG = false;
            }

            dto.EDI_MEMBER_ID = searchCondition.EDI_MEMBER_ID;
            dto.HAIKI_SHURUI_CD = searchCondition.HAIKI_SHURUI_CD;
            dto.HAIKI_SHURUI_SAIBUNRUI_CD = row["HAIKI_SAI_CODE"].ToString();
            dto.HAIKI_SHURUI_NAME = row["HAIKI_SHURUI"].ToString();
            dto.HAIKI_BUNRUI = 4;

            return dto;
        }

        /// <summary>
        /// 電子廃棄物種類細分類マスタの登録
        /// </summary>
        /// <param name="dto"></param>
        private void InsertDenHaikiShuruiSaibunrui(M_DENSHI_HAIKI_SHURUI_SAIBUNRUI dto)
        {
            if (dto != null)
            {
                if (dto.TIME_STAMP != null)
                {
                    if (dto.DELETE_FLG.Value)
                    {
                        string createUser = dto.CREATE_USER;
                        string createPC = dto.CREATE_PC;
                        DateTime createDate = dto.CREATE_DATE.Value;
                        var dataBinderEntry = new DataBinderLogic<M_DENSHI_HAIKI_SHURUI_SAIBUNRUI>(dto);
                        dataBinderEntry.SetSystemProperty(dto, false);
                        dto.CREATE_USER = createUser;
                        dto.CREATE_PC = createPC;
                        dto.CREATE_DATE = createDate;
                        dto.DELETE_FLG = false;
                        this.haikiShuruiSaibunruiDao.Update(dto);
                    }
                }
                else
                {
                    var dataBinderEntry = new DataBinderLogic<M_DENSHI_HAIKI_SHURUI_SAIBUNRUI>(dto);
                    dataBinderEntry.SetSystemProperty(dto, false);

                    this.haikiShuruiSaibunruiDao.Insert(dto);
                }
            }
        }
        #endregion

        /// 20141021 Houkakou 「補助データ」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.cdate_HikiwatasiBiFrom.BackColor = Constans.NOMAL_COLOR;
                this.form.cdate_HikiwatasiBiTo.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.cdate_HikiwatasiBiFrom.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.cdate_HikiwatasiBiTo.Text))
                {
                    return false;
                }

                DateTime date_from = DateTime.Parse(this.form.cdate_HikiwatasiBiFrom.Text);
                DateTime date_to = DateTime.Parse(this.form.cdate_HikiwatasiBiTo.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.cdate_HikiwatasiBiFrom.IsInputErrorOccured = true;
                    this.form.cdate_HikiwatasiBiTo.IsInputErrorOccured = true;
                    this.form.cdate_HikiwatasiBiFrom.BackColor = Constans.ERROR_COLOR;
                    this.form.cdate_HikiwatasiBiTo.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "引渡し日From", "引渡し日To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.cdate_HikiwatasiBiFrom.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }

            return false;
        }
        #endregion
        /// 20141021 Houkakou 「補助データ」の日付チェックを追加する　end

        /// 20141128 Houkakou 「補助データ」のダブルクリックを追加する　start
        #region cdate_HikiwatasiBiToダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdate_HikiwatasiBiTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cdate_HikiwatasiBiFrom;
            var ToTextBox = this.form.cdate_HikiwatasiBiTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region cntxt_ManifestIdToダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntxt_ManifestIdTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cntxt_ManifestIdFrom;
            var ToTextBox = this.form.cntxt_ManifestIdTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou 「補助データ」のダブルクリックを追加する　end

        #region マニフェスト番号チェック
        /// <summary>
        /// マニフェスト番号不正チェック
        /// </summary>
        /// <returns name="bool">TRUE:入力OK, FALSE:エラー</returns>
        internal bool maniIdIntegrityCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            var bRet = true;
            try
            {
                var fromIdStr = this.form.cntxt_ManifestIdFrom.GetResultText();
                var toIdStr = this.form.cntxt_ManifestIdTo.GetResultText();

                // エラー状態初期化
                this.form.cntxt_ManifestIdFrom.IsInputErrorOccured = false;
                this.form.cntxt_ManifestIdTo.IsInputErrorOccured = false;

                if ((false == string.IsNullOrEmpty(fromIdStr)) && (false == string.IsNullOrEmpty(toIdStr)))
                {
                    // 数値変換後、比較を行う
                    var fromId = Int64.Parse(fromIdStr);
                    var toId = Int64.Parse(toIdStr);
                    if (0 < fromId.CompareTo(toId))
                    {
                        // マニフェスト番号入力不正のため、エラー表示
                        this.form.cntxt_ManifestIdFrom.IsInputErrorOccured = true;
                        this.form.cntxt_ManifestIdTo.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E032", this.form.cntxt_ManifestIdFrom.DisplayItemName, this.form.cntxt_ManifestIdTo.DisplayItemName);
                        this.form.cntxt_ManifestIdFrom.Focus();
                        bRet = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("maniIdIntegrityCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                bRet = false;
            }

            return bRet;
        }
        #endregion

        #region データ作成(検索条件)チェック
        /// <summary>
        /// 
        /// </summary>
        /// <returns name="bool">TRUE:入力OK, FALSE:エラー</returns>
        internal bool DataConditionCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (string.IsNullOrEmpty(this.form.DataConditionValue.GetResultText()))
            {
                this.form.DataConditionValue.IsInputErrorOccured = true;
                msgLogic.MessageBoxShow("E001", this.form.DataConditionValue.DisplayItemName);
                this.form.DataConditionValue.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region マスタ設定(検索条件)チェック
        /// <summary>
        /// 
        /// </summary>
        /// <returns name="bool">TRUE:入力OK, FALSE:エラー</returns>
        internal bool MasterSettingCondCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (string.IsNullOrEmpty(this.form.MasterSettingConditionValue.GetResultText()))
            {
                this.form.MasterSettingConditionValue.IsInputErrorOccured = true;
                msgLogic.MessageBoxShow("E001", this.form.MasterSettingConditionValue.DisplayItemName);
                this.form.MasterSettingConditionValue.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region CreatePrimaryJigyoujouInfo
        /// <summary>
        /// 一意に識別できる事業場情報を作成
        /// </summary>
        /// <param name="jigyoiujou">作成対象</param>
        /// <returns>EDI_MEMBER_ID + JIGYOUJOU_NAME + JIGYOUJOU_ADDRESS1 + JIGYOUJOU_ADDRESS2 + JIGYOUJOU_ADDRESS3 + JIGYOUJOU_ADDRESS4</returns>
        private string CreatePrimaryJigyoujouInfo(M_DENSHI_JIGYOUJOU jigyoiujou)
        {
            if (jigyoiujou == null) { return string.Empty; }

            string ediMemberId = string.IsNullOrEmpty(jigyoiujou.EDI_MEMBER_ID) ? string.Empty : jigyoiujou.EDI_MEMBER_ID;
            string name = string.IsNullOrEmpty(jigyoiujou.JIGYOUJOU_NAME) ? string.Empty : jigyoiujou.JIGYOUJOU_NAME;
            string address1 = string.IsNullOrEmpty(jigyoiujou.JIGYOUJOU_ADDRESS1) ? string.Empty : jigyoiujou.JIGYOUJOU_ADDRESS1;
            string address2 = string.IsNullOrEmpty(jigyoiujou.JIGYOUJOU_ADDRESS2) ? string.Empty : jigyoiujou.JIGYOUJOU_ADDRESS2;
            string address3 = string.IsNullOrEmpty(jigyoiujou.JIGYOUJOU_ADDRESS3) ? string.Empty : jigyoiujou.JIGYOUJOU_ADDRESS3;
            string address4 = string.IsNullOrEmpty(jigyoiujou.JIGYOUJOU_ADDRESS4) ? string.Empty : jigyoiujou.JIGYOUJOU_ADDRESS4;

            return ConvertTwoByteToOneByte(ediMemberId + name + address1 + address2 + address3 + address4);
        }
        #endregion

        public string ConvertTwoByteToOneByte(String prmValue)
        {
            LogUtility.DebugMethodStart(prmValue);

            string output = Strings.StrConv(prmValue, VbStrConv.Narrow, 0);

            LogUtility.DebugMethodEnd(output);

            return output;
        }

        #region CreateJigyoujouAddress
        /// <summary>
        /// 事業場の住所1～4を結合する
        /// </summary>
        /// <param name="jigyoiujou">作成対象</param>
        /// <returns>JIGYOUJOU_ADDRESS1 + JIGYOUJOU_ADDRESS2 + JIGYOUJOU_ADDRESS3 + JIGYOUJOU_ADDRESS4</returns>
        private string CreateJigyoujouAddress(M_DENSHI_JIGYOUJOU jigyoiujou)
        {
            if (jigyoiujou == null) { return string.Empty; }

            string address1 = string.IsNullOrEmpty(jigyoiujou.JIGYOUJOU_ADDRESS1) ? string.Empty : jigyoiujou.JIGYOUJOU_ADDRESS1;
            string address2 = string.IsNullOrEmpty(jigyoiujou.JIGYOUJOU_ADDRESS2) ? string.Empty : jigyoiujou.JIGYOUJOU_ADDRESS2;
            string address3 = string.IsNullOrEmpty(jigyoiujou.JIGYOUJOU_ADDRESS3) ? string.Empty : jigyoiujou.JIGYOUJOU_ADDRESS3;
            string address4 = string.IsNullOrEmpty(jigyoiujou.JIGYOUJOU_ADDRESS4) ? string.Empty : jigyoiujou.JIGYOUJOU_ADDRESS4;

            return address1 + address2 + address3 + address4;
        }
        #endregion

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
    }
}
