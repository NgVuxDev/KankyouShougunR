// $Id: DBAccessor.cs 55905 2015-07-16 08:14:29Z wuq@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.SalesManagement.ShiharaiMotocho.Const;
using Shougun.Function.ShougunCSCommon.Utility;
using System.Text;

namespace Shougun.Core.SalesManagement.ShiharaiMotocho.Accessor
{
    /// <summary>
    /// DBAccessするためのクラス
    /// 
    /// FW側と業務側とでDaoが点在するため、
    /// 本クラスで呼び出すDaoをコントロールする
    /// </summary>
    internal class DBAccessor
    {
        #region フィールド
        /// <summary>
        /// IM_SYS_INFODao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;  // No.3688

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao TorihikisakiDao;
        /// <summary>
        /// 取引先支払情報のDao
        /// </summary>
        private IM_TORIHIKISAKI_SHIHARAIDao TorihikiShiharaiDao;
        /// <summary>
        /// 支払元帳のDao
        /// </summary>
        private Function.ShougunCSCommon.Dao.IT_SHIHARAI_MOTOCHODao MotochoDao;
        // 20150602 代納伝票対応(代納不具合一覧52) Start
        /// <summary>
        /// 売上支払Dao
        /// </summary>
        private Function.ShougunCSCommon.Dao.IT_UR_SH_ENTRYDao UrShEntryDao;
        // 20150602 代納伝票対応(代納不具合一覧52) End
        /// <summary>
        ///  取引先CDのリスト
        /// </summary>
        private M_TORIHIKISAKI[] torihikiList;
        /// <summary>
        ///  明細用テーブル
        /// </summary>
        private DataTable meisaiTable;
        /// <summary>
        ///  アプリケーション共有情報クラス
        /// </summary>
        private CommonInformation commonInfo;
        /// <summary>
        /// 前回残高と差引残高算出時でクエリの抽出条件をハンドリングするフラグです
        /// 前回残高計算時 = 1
        /// 差引残高算出時 = 0
        /// </summary>
        private int QueryFlg = 1;
        /// <summary>
        /// 全取引先データ
        /// </summary>
        private M_TORIHIKISAKI[] torihikisakiAllList;
        /// <summary>
        /// 支払月次処理・支払月次調整データ
        /// </summary>
        private DataTable monthlyTable;
        /// <summary>
        /// 支払月次処理・支払月次調整データ
        /// </summary>
        private DataTable monthlyNewTable;
        /// <summary>
        /// 支払月次処理・支払月次消費税データ
        /// </summary>
        private DataTable adjustTaxTable;

        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // フィールドの初期化
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();    // No.3688
            this.TorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.TorihikiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            // 20150602 代納伝票対応(代納不具合一覧52) Start
            this.UrShEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_UR_SH_ENTRYDao>();
            // 20150602 代納伝票対応(代納不具合一覧52) End
            this.MotochoDao = DaoInitUtility.GetComponent<Function.ShougunCSCommon.Dao.IT_SHIHARAI_MOTOCHODao>();
        }
        #endregion

        #region DBアクセッサ
        // 20150602 代納伝票対応(代納不具合一覧52) Start
        /// <summary>
        /// T_UR_SH_ENTRYを取得する
        /// </summary>
        /// <param name="DenpyouNum"></param>
        /// <returns></returns>
        public T_UR_SH_ENTRY[] GetUrShEntry(long DenpyouNum)
        {
            List<T_UR_SH_ENTRY> ret = new List<T_UR_SH_ENTRY>();
            T_UR_SH_ENTRY entry = new T_UR_SH_ENTRY();
            entry.UR_SH_NUMBER = DenpyouNum;
            entry.DELETE_FLG = false;

            // 売上データ取得
            entry.DAINOU_FLG = SqlBoolean.Null;
            ret.AddRange(UrShEntryDao.GetDataForEntityNoSql(entry));

            // 代納データ取得
            entry.DAINOU_FLG = true;
            ret.AddRange(UrShEntryDao.GetDataForEntityNoSql(entry));

            return ret.ToArray();
        }
        // 20150602 代納伝票対応(代納不具合一覧52) End

        // No.3688-->
        /// <summary>
        /// SYS_INFOを取得する
        /// </summary>
        /// <returns></returns>
        public M_SYS_INFO GetSysInfo()
        {
            M_SYS_INFO[] returnEntity = sysInfoDao.GetAllData();
            return returnEntity[0];
        }
        // No.3688<--

        /// <summary>
        /// 明細用一覧データの取得
        /// </summary>
        /// <param name="param">範囲条件情報</param>
        /// <param name="info">アプリケーション共有情報</param>
        /// <returns name="DataTable">データテーブル</returns>
        /// <remarks>
        /// ※データ抽出モデル
        ///		┌───────┐
        ///		│ 前月繰越残高 │
        ///		├───────┤
        ///		│　期間内支払　│┐
        ///		├───────┤│
        ///		│　期間内出金　│├─　1:発生のみの場合、｝箇所のデータがない場合は、
        ///		├───────┤│　　抽出の対象外とする(2:発生なしの場合は、前月繰越残高が存在すれば抽出する)
        ///		│ 期間内消費税 │┘
        ///		└───────┘
        /// </remarks>
        internal DataTable GetIchiranData(MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo param, CommonInformation info, Form form, ref bool hadSearched)
        {
            // 前月繰越残高用行
            DataRow zandakaRow;

            // 伝票毎税用行
            DataRow MaizeiRow;

            // 取引毎税用行
            DataRow torihikiRow;

            // 共有情報をセット
            this.commonInfo = info;

            // Dateから日付を文字列にて取得
            string sDay = param.StartDay.Date.ToString();
            string eDay = param.EndDay.Date.ToString();

            // 全取引先マスタの取得
            this.torihikisakiAllList = this.TorihikisakiDao.GetAllData();

            // 期間内支払/出金、取引毎税データテーブルの取得
            int kakuteiKBN;
            int.TryParse(this.commonInfo.SysInfo.SYS_KAKUTEI__TANNI_KBN.ToString(), out kakuteiKBN);

            // 差引残高算出用にクエリの抽出条件を変更します
            QueryFlg = 0;

            DataTable kikannaiTbl = this.MotochoDao.GetIchiranData(param.StartTorihikisakiCD, param.EndTorihikisakiCD, sDay, eDay, kakuteiKBN, param.TorihikiKBN, param.Shimebi, param.TyuusyutuKBN, QueryFlg);
            QueryFlg = 1;

            DateTime ZandakaStartDay = DateTime.MinValue;
            DateTime ZandakaEndDay = DateTime.MaxValue;
            bool continues = true;
            decimal zandaka = 0;
            Dictionary<string, List<object>> Zandaka = new Dictionary<string, List<object>>();
            bool result = true;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 明細用TableのClone作成
            this.meisaiTable = kikannaiTbl.Clone();
            this.meisaiTable.Columns["DENSHU_KBN"].AllowDBNull = true;
            this.meisaiTable.Columns["SUURYOU_UNIT"].AllowDBNull = true;
            this.meisaiTable.Columns["SHIHARAI_ZEI_KBN"].AllowDBNull = true;
            this.meisaiTable.Columns["SHIHARAI_ZEI_KEISAN_KBN"].AllowDBNull = true;
            this.meisaiTable.Columns["SHUKKIN_KINGAKU"].DataType = System.Type.GetType("System.Decimal");
            this.meisaiTable.Columns["SASHIHIKI_ZANDAKA"].DataType = System.Type.GetType("System.Decimal");

            bool isMonthlyTarget = IsMonthlyTarget(param);
            this.monthlyTable = null;
            this.monthlyNewTable = new DataTable();

            // 取引先CDリストを作成
            this.torihikiList = this.MotochoDao.GetTorihikisakiList(param.StartTorihikisakiCD, param.EndTorihikisakiCD, param.TorihikiKBN, param.Shimebi, isMonthlyTarget);

            var torihikisakiShiharaiDt = this.MotochoDao.GetTorihikisakiShiharaiList(param.StartTorihikisakiCD, param.EndTorihikisakiCD, param.Shimebi);
            // 支払月次データ取得
            if (isMonthlyTarget)
            {
                if (param.TyuusyutuKBN == 1)
                {
                    this.monthlyTable = this.MotochoDao.GetMonthlyData(param.StartTorihikisakiCD, param.EndTorihikisakiCD, param.StartDay.Year, param.StartDay.Month, param.Shimebi);
                    this.monthlyNewTable = this.monthlyTable.Clone();

                    this.adjustTaxTable = this.MotochoDao.GetMonthlyAdjustTaxData(param.StartTorihikisakiCD, param.EndTorihikisakiCD, param.StartDay.Year, param.EndDay.Year, param.StartDay.Month, param.EndDay.Month);
                    // 月次処理データの計算

                    // 別ユーザが月次処理中かのチェック
                    GetsujiShoriCheckLogicClass checkLogic = new GetsujiShoriCheckLogicClass();
                    if (checkLogic.CheckGetsujiShoriChu())
                    {
                        msgLogic.MessageBoxShow("E224", "実行");
                        return this.meisaiTable;
                    }

                    if (monthlyTable.Rows.Count == 0)
                    {
                        StringBuilder msg = new StringBuilder();
                        // 月次処理画面以外の文言(掛金一覧表、元帳)
                        msg.Append("月次処理がされていないため、");
                        msg.Append("\r\n");
                        msg.Append("表示に時間がかかる場合があります。");
                        msg.Append("\r\n");
                        msg.Append("実行してよろしいですか？");

                        DialogResult results = msgLogic.MessageBoxShowConfirm(msg.ToString());
                        if (results != DialogResult.Yes)
                        {
                            // 月次処理中断
                            return this.meisaiTable;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < monthlyTable.Rows.Count; i++)
                        {
                            List<object> torihikisakiInfo = new List<object>();
                            int year = Convert.ToInt32(monthlyTable.Rows[i]["YEAR"].ToString());
                            int month = Convert.ToInt32(monthlyTable.Rows[i]["MONTH"].ToString());
                            decimal zandakas = 0;

                            if (monthlyTable.Rows[i]["SASHIHIKI_URIAGE_ZANDAKA"] != null
                                && !string.IsNullOrEmpty(monthlyTable.Rows[i]["SASHIHIKI_URIAGE_ZANDAKA"].ToString()))
                            {
                                zandakas = Convert.ToDecimal(monthlyTable.Rows[i]["SASHIHIKI_URIAGE_ZANDAKA"].ToString());
                            }

                            #region 月次処理された月末から日付指定した[伝票日付(From)]まで

                            ZandakaStartDay = Convert.ToDateTime(year.ToString() + "/" + month.ToString() + "/" + "01").AddMonths(1);
                            ZandakaEndDay = param.StartDay.AddDays(-1);

                            if (ZandakaStartDay == param.StartDay)
                            {
                                continues = false;
                            }
                            else
                            {
                                continues = true;
                            }
                            #endregion

                            torihikisakiInfo.Add(zandakas);
                            torihikisakiInfo.Add(continues);
                            torihikisakiInfo.Add(ZandakaStartDay);
                            torihikisakiInfo.Add(ZandakaEndDay);
                            if (!Zandaka.ContainsKey(monthlyTable.Rows[i]["TORIHIKISAKI_CD"].ToString()))
                            {
                                Zandaka.Add(monthlyTable.Rows[i]["TORIHIKISAKI_CD"].ToString(), torihikisakiInfo);
                            }
                        }
                    }

                    // 処理対象の取引先CDの絞込
                    List<string> torihikisakiCdList = torihikisakiShiharaiDt.AsEnumerable().
                        Select(n => n["TORIHIKISAKI_CD"].ToString()).
                        ToList();
                    // 20150715 取引先_支払をフィルターする Start
                    List<M_TORIHIKISAKI_SHIHARAI> torihikisakiShiharaiList = torihikisakiShiharaiDt.AsEnumerable().
                        Select(n =>
                        {
                            return new M_TORIHIKISAKI_SHIHARAI()
                            {
                                TORIHIKISAKI_CD = Convert.ToString(n["TORIHIKISAKI_CD"]),
                                TORIHIKI_KBN_CD = Convert.IsDBNull(n["TORIHIKI_KBN_CD"]) ? SqlInt16.Null : Convert.ToInt16(n["TORIHIKI_KBN_CD"]),
                                KAISHI_KAIKAKE_ZANDAKA = Convert.IsDBNull(n["KAISHI_KAIKAKE_ZANDAKA"]) ? SqlDecimal.Null : Convert.ToDecimal(n["KAISHI_KAIKAKE_ZANDAKA"]),
                                SHOSHIKI_KBN = Convert.IsDBNull(n["SHOSHIKI_KBN"]) ? SqlInt16.Null : Convert.ToInt16(n["SHOSHIKI_KBN"]),
                                TAX_HASUU_CD = Convert.IsDBNull(n["TAX_HASUU_CD"]) ? SqlInt16.Null : Convert.ToInt16(n["TAX_HASUU_CD"])
                            };
                        }).
                        ToList();
                    // 20150715 取引先_支払をフィルターする End

                    var getsujiLogic = new GetsujiShoriLogic();
                    // 20150715 取引先_支払キャッシュ作成 Start
                    getsujiLogic.TorihikisakiShiharaiList.AddRange(torihikisakiShiharaiList);
                    // 20150715 取引先_支払キャッシュ作成 End

                    // 進行状況を表示させるためプログレスバーの初期化
                    var parentForm = (IchiranBaseForm)form.Parent;
                    ToolStripProgressBar progresBar = parentForm.progresBar;
                    progresBar.Minimum = 0;
                    progresBar.Maximum = torihikisakiCdList.Count;
                    progresBar.Value = 0;

                    bool isDispAlert = true;
                    int counter = 1;
                    foreach (var torihikisakiCd in torihikisakiCdList)
                    {
                        if (Zandaka.ContainsKey(torihikisakiCd))
                        {
                            if (Zandaka[torihikisakiCd][0] != null
                                && !string.IsNullOrEmpty(Zandaka[torihikisakiCd][0].ToString()))
                            {
                                zandaka = Convert.ToDecimal(Zandaka[torihikisakiCd][0]);
                            }
                            else
                            {
                                zandaka = 0;
                            }

                            continues = Convert.ToBoolean(Zandaka[torihikisakiCd][1]);
                            ZandakaStartDay = Convert.ToDateTime(Zandaka[torihikisakiCd][2]);
                            ZandakaEndDay = Convert.ToDateTime(Zandaka[torihikisakiCd][3]);
                        }
                        else
                        {
                            var dt = torihikisakiShiharaiDt.AsEnumerable()
                                    .Where(n => n["TORIHIKISAKI_CD"].Equals(torihikisakiCd))
                                    .FirstOrDefault();
                            if (dt == null)
                            {
                                continue;
                            }

                            if (dt["KAISHI_KAIKAKE_ZANDAKA"] != null
                                && !string.IsNullOrEmpty(dt["KAISHI_KAIKAKE_ZANDAKA"].ToString()))
                            {
                                zandaka = Convert.ToDecimal(dt["KAISHI_KAIKAKE_ZANDAKA"].ToString());
                            }

                            continues = true;
                            ZandakaStartDay = DateTime.MinValue;
                            ZandakaEndDay = param.StartDay.AddDays(-1);
                        }

                        // 月次処理実行
                        result = getsujiLogic.GetsujiShoriHituke(torihikisakiCd, continues, ZandakaStartDay, ZandakaEndDay, zandaka, param.StartDay, param.EndDay, 2,
                                 shoriType: GetsujiShoriLogic.GETSUJI_SHORI_TYPE.SHIHARAI);
                        if (!result)
                        {
                            // 計算処理が中断された場合は処理終了
                            return this.meisaiTable;
                        }

                        if (counter <= progresBar.Maximum)
                        {
                            progresBar.Value = counter;
                        }

                        if (isDispAlert)
                        {
                            // アラート表示は一度のみ
                            isDispAlert = false;
                        }
                        counter++;
                    }

                    // プログレスバーリセット
                    progresBar.Value = 0;

                    // 計算結果取得
                    string yyyyMM = param.EndDay.ToString("yyyy/MM");
                    if (getsujiLogic.MonthlyLockShDatas.ContainsKey(yyyyMM))
                    {
                        var lockShEntityList = getsujiLogic.MonthlyLockShDatas[yyyyMM];

                        foreach (var lockShEntity in lockShEntityList)
                        {
                            var row = this.monthlyNewTable.NewRow();
                            row["TORIHIKISAKI_CD"] = lockShEntity.TORIHIKISAKI_CD;
                            row["PREVIOUS_MONTH_BALANCE"] = lockShEntity.PREVIOUS_MONTH_BALANCE.Value;
                            row["SHIME_UTIZEI_GAKU"] = lockShEntity.SHIME_UTIZEI_GAKU.Value;
                            row["SHIME_SOTOZEI_GAKU"] = lockShEntity.SHIME_SOTOZEI_GAKU.Value;

                            // 再計算時は月次調整テーブルは存在しないので、消費税調整額は0円固定
                            row["ADJUST_TAX"] = decimal.Zero;
                            row["SASHIHIKI_URIAGE_ZANDAKA"] = lockShEntity.PREVIOUS_MONTH_BALANCE.Value;
                            row["YEAR"] = lockShEntity.YEAR.Value;
                            row["MONTH"] = lockShEntity.MONTH.Value;

                            this.monthlyNewTable.Rows.Add(row);
                        }
                    }
                }
                else
                {
                    this.monthlyTable = this.MotochoDao.GetShiharaiMonthlyData(param.StartTorihikisakiCD, param.EndTorihikisakiCD, param.StartDay.Date.ToString(), param.Shimebi);
                    this.monthlyNewTable = this.monthlyTable.Clone();
                    if (monthlyTable != null && monthlyTable.Rows.Count > 0)
                    {
                        for (int k = 0; k < monthlyTable.Rows.Count; k++)
                        {
                            List<object> ShiharaiTorihikisakiInfo = new List<object>();
                            DateTime sday = Convert.ToDateTime(monthlyTable.Rows[k]["SEISAN_DATE"].ToString());
                            decimal kurikoshis = 0;

                            if (monthlyTable.Rows[k]["SASHIHIKI_URIAGE_ZANDAKA"] != null
                                && !string.IsNullOrEmpty(monthlyTable.Rows[k]["SASHIHIKI_URIAGE_ZANDAKA"].ToString()))
                            {
                                kurikoshis = Convert.ToDecimal(monthlyTable.Rows[k]["SASHIHIKI_URIAGE_ZANDAKA"].ToString());
                            }

                            #region 支払伝票の支払日付の末日から日付指定した[伝票日付(From)]まで

                            ZandakaStartDay = sday.AddDays(+1);
                            ZandakaEndDay = param.StartDay.AddDays(-1);

                            if (ZandakaStartDay == param.StartDay)
                            {
                                continues = false;
                            }
                            else
                            {
                                continues = true;
                            }
                            #endregion

                            ShiharaiTorihikisakiInfo.Add(kurikoshis);
                            ShiharaiTorihikisakiInfo.Add(continues);
                            ShiharaiTorihikisakiInfo.Add(ZandakaStartDay);
                            ShiharaiTorihikisakiInfo.Add(ZandakaEndDay);
                            if (!Zandaka.ContainsKey(monthlyTable.Rows[k]["TORIHIKISAKI_CD"].ToString()))
                            {
                                Zandaka.Add(monthlyTable.Rows[k]["TORIHIKISAKI_CD"].ToString(), ShiharaiTorihikisakiInfo);
                            }
                        }
                    }

                    // 処理対象の取引先CDの絞込
                    List<string> torihikisakiCdList = torihikisakiShiharaiDt.AsEnumerable().
                        Select(n => n["TORIHIKISAKI_CD"].ToString()).
                        ToList();

                    List<M_TORIHIKISAKI_SHIHARAI> torihikisakiShiharaiList = torihikisakiShiharaiDt.AsEnumerable().
                        Select(n =>
                        {
                            return new M_TORIHIKISAKI_SHIHARAI()
                            {
                                TORIHIKISAKI_CD = Convert.ToString(n["TORIHIKISAKI_CD"]),
                                TORIHIKI_KBN_CD = Convert.IsDBNull(n["TORIHIKI_KBN_CD"]) ? SqlInt16.Null : Convert.ToInt16(n["TORIHIKI_KBN_CD"]),
                                KAISHI_KAIKAKE_ZANDAKA = Convert.IsDBNull(n["KAISHI_KAIKAKE_ZANDAKA"]) ? SqlDecimal.Null : Convert.ToDecimal(n["KAISHI_KAIKAKE_ZANDAKA"]),
                                SHOSHIKI_KBN = Convert.IsDBNull(n["SHOSHIKI_KBN"]) ? SqlInt16.Null : Convert.ToInt16(n["SHOSHIKI_KBN"]),
                                TAX_HASUU_CD = Convert.IsDBNull(n["TAX_HASUU_CD"]) ? SqlInt16.Null : Convert.ToInt16(n["TAX_HASUU_CD"])
                            };
                        }).
                        ToList();

                    var getsujiLogic = new GetsujiShoriLogic();
                    getsujiLogic.TorihikisakiShiharaiList.AddRange(torihikisakiShiharaiList);

                    // 進行状況を表示させるためプログレスバーの初期化
                    var parentForm = (IchiranBaseForm)form.Parent;
                    ToolStripProgressBar progresBar = parentForm.progresBar;
                    progresBar.Minimum = 0;
                    progresBar.Maximum = torihikisakiCdList.Count;
                    progresBar.Value = 0;

                    bool isDispAlert = true;
                    int counter = 1;
                    foreach (var torihikisakiCd in torihikisakiCdList)
                    {
                        if (Zandaka.ContainsKey(torihikisakiCd))
                        {
                            if (Zandaka[torihikisakiCd][0] != null
                                && !string.IsNullOrEmpty(Zandaka[torihikisakiCd][0].ToString()))
                            {
                                zandaka = Convert.ToDecimal(Zandaka[torihikisakiCd][0]);
                            }
                            else
                            {
                                zandaka = 0;
                            }

                            continues = Convert.ToBoolean(Zandaka[torihikisakiCd][1]);
                            ZandakaStartDay = Convert.ToDateTime(Zandaka[torihikisakiCd][2]);
                            ZandakaEndDay = Convert.ToDateTime(Zandaka[torihikisakiCd][3]);
                        }
                        else
                        {
                            var dt = torihikisakiShiharaiDt.AsEnumerable()
                                    .Where(n => n["TORIHIKISAKI_CD"].Equals(torihikisakiCd))
                                    .FirstOrDefault();
                            if (dt == null)
                            {
                                continue;
                            }

                            if (dt["KAISHI_KAIKAKE_ZANDAKA"] != null
                                && !string.IsNullOrEmpty(dt["KAISHI_KAIKAKE_ZANDAKA"].ToString()))
                            {
                                zandaka = Convert.ToDecimal(dt["KAISHI_KAIKAKE_ZANDAKA"].ToString());
                            }
                            continues = true;
                            ZandakaStartDay = DateTime.MinValue;
                            ZandakaEndDay = param.StartDay.AddDays(-1);
                        }

                        // 締処理実行
                        result = getsujiLogic.GetsujiShoriSeikyuu(torihikisakiCd, continues, ZandakaStartDay, ZandakaEndDay, zandaka, param.StartDay, param.EndDay, 2,
                                 shoriType: GetsujiShoriLogic.GETSUJI_SHORI_TYPE.SHIHARAI);
                        if (!result)
                        {
                            // 計算処理が中断された場合は処理終了
                            return this.meisaiTable;
                        }

                        if (counter <= progresBar.Maximum)
                        {
                            progresBar.Value = counter;
                        }

                        if (isDispAlert)
                        {
                            // アラート表示は一度のみ
                            isDispAlert = false;
                        }
                        counter++;
                    }

                    // プログレスバーリセット
                    progresBar.Value = 0;

                    // 計算結果取得
                    string yyyyMM = ZandakaEndDay.ToString("yyyy/MM");
                    if (getsujiLogic.MonthlyLockShDatas.ContainsKey(yyyyMM))
                    {
                        var lockShEntityList = getsujiLogic.MonthlyLockShDatas[yyyyMM];

                        foreach (var lockShEntity in lockShEntityList)
                        {
                            var row = this.monthlyNewTable.NewRow();
                            row["TORIHIKISAKI_CD"] = lockShEntity.TORIHIKISAKI_CD;
                            row["PREVIOUS_MONTH_BALANCE"] = lockShEntity.PREVIOUS_MONTH_BALANCE.Value;
                            row["SHIME_UTIZEI_GAKU"] = lockShEntity.SHIME_UTIZEI_GAKU.Value;
                            row["SHIME_SOTOZEI_GAKU"] = lockShEntity.SHIME_SOTOZEI_GAKU.Value;

                            // 再計算時は月次調整テーブルは存在しないので、消費税調整額は0円固定
                            row["ADJUST_TAX"] = decimal.Zero;
                            row["SASHIHIKI_URIAGE_ZANDAKA"] = lockShEntity.PREVIOUS_MONTH_BALANCE.Value;
                            row["YEAR"] = lockShEntity.YEAR.Value;
                            row["MONTH"] = lockShEntity.MONTH.Value;

                            this.monthlyNewTable.Rows.Add(row);
                        }
                    }
                }
            }

            if (kikannaiTbl.Rows.Count != 0)
            {
                int index = 0;

                // 比較値をセット
                string oldDenNum = kikannaiTbl.Rows[0]["DENPYOU_NUMBER"].ToString();
                string oldDenshu = kikannaiTbl.Rows[0]["DENSHU_KBN"].ToString();
                string oldTorihikiCD = kikannaiTbl.Rows[0]["TORIHIKISAKI_CD"].ToString();
                DateTime oldDate = param.StartDay;

                if (param.OutPutKBN == 2)
                {
                    // 出力区分が「2:発生なし」の場合、期間内データが無くとも、
                    // 前月繰越残高は表示する必要があるため、取引先範囲内の残高データを挿入する
                    while (index < this.torihikiList.Length)
                    {
                        if (0 <= string.Compare(this.torihikiList[index].TORIHIKISAKI_CD, oldTorihikiCD, true))
                        {
                            // 期間内データの取引先CDと一致した場合、もしくは
                            // 期間内データの取引先CDよりも前のCDが存在しない場合
                            // 前月繰越残高データの後に期間内データを挿入する必要があるため
                            // ここで一旦抜ける
                            if (0 == string.Compare(this.torihikiList[index].TORIHIKISAKI_CD, oldTorihikiCD, true))
                            {
                                // 次の取引先へ
                                index++;
                            }
                            break;
                        }

                        // 前月繰越残高の挿入
                        zandakaRow = this.GetCreateZandakaRow(param, this.torihikiList[index].TORIHIKISAKI_CD);
                        this.meisaiTable.Rows.Add(zandakaRow);

                        // 月次処理の消費税調整額を最後に挿入
                        var monthlyRowNotContains1 = CreateMonthlyTaxRows(this.torihikiList[index].TORIHIKISAKI_CD, param);
                        if (monthlyRowNotContains1 != null)
                        {
                            this.meisaiTable.Rows.Add(monthlyRowNotContains1);
                        }

                        // 次の取引先へ
                        index++;
                    }
                }

                // 現金の元帳の時は前月繰越残高を表示しない
                if (param.TorihikiKBN != 2)
                {
                    // 期間内データ１行目の前月繰越残高の挿入
                    zandakaRow = this.GetCreateZandakaRow(param, kikannaiTbl.Rows[0]["TORIHIKISAKI_CD"].ToString());
                    this.meisaiTable.Rows.Add(zandakaRow);
                }

                DataRow lastRow = kikannaiTbl.NewRow();
                foreach (DataRow row in kikannaiTbl.Rows)
                {
                    if (this.meisaiTable.Rows.Count != 0)
                    {
                        if (UIConstans.ZEI_KEISAN_KBN_DENPYOU == this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["ZEI_KEISAN_KBN_CD"].ToString())
                        {
                            // 同一伝票番号内の伝票毎消費税を伝票毎税としてセット
                            if ((oldDenNum != row["DENPYOU_NUMBER"].ToString()) || (oldDenshu != row["DENSHU_KBN"].ToString()))
                            {
                                // 伝票毎税の挿入
                                MaizeiRow = this.GetCreateMaizeiRow(param);
                                this.meisaiTable.Rows.Add(MaizeiRow);
                            }
                        }
                        if (param.TorihikiKBN == 2)
                        {
                            if (UIConstans.ZEI_KEISAN_KBN_MEISAI == this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["ZEI_KEISAN_KBN_CD"].ToString())
                            {
                                if ((oldDenNum != row["DENPYOU_NUMBER"].ToString()) || (oldDenshu != row["DENSHU_KBN"].ToString()))
                                {
                                    // 最後の伝票の伝票毎税の挿入
                                    MaizeiRow = this.GetCreateMaizeiRow(param);
                                    this.meisaiTable.Rows.Add(MaizeiRow);
                                }
                            }
                        }
                    }

                    // 同一取引先内かつ期間内の請内税額+請外税額を取引毎税としてセット
                    if ((oldTorihikiCD != row["TORIHIKISAKI_CD"].ToString()))
                    {
                        if (param.TorihikiKBN != 2)
                        {
                            // 取引毎税の挿入
                            var monthlyRows1 = CreateMonthlyShimeRows(oldTorihikiCD, param);
                            if (0 < monthlyRows1.Count)
                            {
                                // 月次データが存在する場合は、月次データから繰越金額を設定
                                foreach (var monthlyRow in monthlyRows1)
                                {
                                    this.meisaiTable.Rows.Add(monthlyRow);
                                }
                            }
                            else
                            {
                                // ※該当取引先最後の取引毎税の場合は、最後の明細日付以上～明細日付月の末日以下で取引毎税を検索する
                                torihikiRow = this.GetCreateSeikyuzeiRow(oldTorihikiCD, param, kikannaiTbl);
                                if (torihikiRow != null)
                                {
                                    this.meisaiTable.Rows.Add(torihikiRow);
                                }
                                // No.4212-->
                                else
                                {
                                    if (UIConstans.ZEI_KEISAN_KBN_SEIKYUU == lastRow["ZEI_KEISAN_KBN_CD"].ToString() &&
                                        UIConstans.ZEI_KBN_EXEMPTION == lastRow["ZEI_KBN_CD"].ToString())
                                    {
                                        CreateSeikyuuzeiRow(oldTorihikiCD, oldDate);
                                    }
                                }
                                // No.4212<--
                            }

                            // 月次処理の消費税調整額を最後に挿入
                            var monthlyRow1 = CreateMonthlyTaxRows(oldTorihikiCD, param);
                            if (monthlyRow1 != null)
                            {
                                this.meisaiTable.Rows.Add(monthlyRow1);
                            }

                            // 現金の元帳の時は前月繰越残高を表示しない
                            if (param.OutPutKBN == 2)
                            {
                                // 次の取引先の前月繰越残高を挿入する前に、期間内データが存在しない
                                // 取引先がいないかどうかのチェック
                                // あれば、前月繰越残高データを挿入する
                                while (index < this.torihikiList.Length)
                                {
                                    if (0 <= string.Compare(this.torihikiList[index].TORIHIKISAKI_CD, row["TORIHIKISAKI_CD"].ToString(), true))
                                    {
                                        // 期間内データの取引先CDと一致した場合、もしくは
                                        // 期間内データの取引先CDよりも前のCDが存在しない場合
                                        // 前月繰越残高データの後に期間内データを挿入する必要があるため
                                        // ここで一旦抜ける
                                        if (0 == string.Compare(this.torihikiList[index].TORIHIKISAKI_CD, row["TORIHIKISAKI_CD"].ToString(), true))
                                        {
                                            // 次の取引先へ
                                            index++;
                                        }

                                        break;
                                    }

                                    // 前月繰越残高の挿入
                                    zandakaRow = this.GetCreateZandakaRow(param, this.torihikiList[index].TORIHIKISAKI_CD);
                                    this.meisaiTable.Rows.Add(zandakaRow);

                                    // 月次処理の消費税調整額を最後に挿入
                                    var monthlyRowNotContains2 = CreateMonthlyTaxRows(this.torihikiList[index].TORIHIKISAKI_CD, param);
                                    if (monthlyRowNotContains2 != null)
                                    {
                                        this.meisaiTable.Rows.Add(monthlyRowNotContains2);
                                    }

                                    // 次の取引先へ
                                    index++;
                                }
                            }

                            /// 前月繰越残高の挿入
                            zandakaRow = this.GetCreateZandakaRow(param, row["TORIHIKISAKI_CD"].ToString());
                            this.meisaiTable.Rows.Add(zandakaRow);
                        }


                    }

                    if (param.TorihikiKBN == 2)
                    {
                        if (UIConstans.ZEI_KEISAN_KBN_MEISAI == row["ZEI_KEISAN_KBN_CD"].ToString()
                            && UIConstans.ZEI_KBN_UCHI == row["ZEI_KBN_CD"].ToString()
                            && string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            //明細毎内税品名無
                            kikannaiTbl.Columns["SHIHARAI_KINGAKU"].ReadOnly = false;
                            row["SHIHARAI_KINGAKU"] = (decimal)row["SHIHARAI_KINGAKU"] - (decimal)row["SHOUHIZEI"];
                            kikannaiTbl.Columns["SHIHARAI_KINGAKU"].ReadOnly = true;
                        }

                        else if (UIConstans.ZEI_KBN_UCHI == row["HINMEI_ZEI_KBN_CD"].ToString())
                        {
                            //品名内税
                            kikannaiTbl.Columns["SHIHARAI_KINGAKU"].ReadOnly = false;
                            row["SHIHARAI_KINGAKU"] = (decimal)row["SHIHARAI_KINGAKU"] - (decimal)row["SHOUHIZEI"];
                            kikannaiTbl.Columns["SHIHARAI_KINGAKU"].ReadOnly = true;
                        }
                    }

                    // 明細用Table、比較値の更新
                    this.meisaiTable.ImportRow(row);
                    oldDenNum = row["DENPYOU_NUMBER"].ToString();
                    oldDenshu = row["DENSHU_KBN"].ToString();
                    oldTorihikiCD = row["TORIHIKISAKI_CD"].ToString();
                    oldDate = DateTime.Parse(row["MEISAI_DATE"].ToString());
                    lastRow = row;
                }

                if (UIConstans.ZEI_KEISAN_KBN_DENPYOU == lastRow["ZEI_KEISAN_KBN_CD"].ToString())
                {
                    // 最後の伝票の伝票毎税の挿入
                    MaizeiRow = this.GetCreateMaizeiRow(param);
                    this.meisaiTable.Rows.Add(MaizeiRow);
                }
                if (param.TorihikiKBN == 2)
                {
                    if (UIConstans.ZEI_KEISAN_KBN_MEISAI == lastRow["ZEI_KEISAN_KBN_CD"].ToString())
                    {
                        // 最後の伝票の伝票毎税の挿入
                        MaizeiRow = this.GetCreateMaizeiRow(param);
                        this.meisaiTable.Rows.Add(MaizeiRow);
                    }
                }
                // 最後の伝票の取引毎税の挿入
                if (param.TorihikiKBN != 2)
                {
                    var monthlyRows2 = CreateMonthlyShimeRows(oldTorihikiCD, param);
                    if (0 < monthlyRows2.Count)
                    {
                        // 月次データが存在する場合は、月次データから繰越金額を設定
                        foreach (var monthlyRow in monthlyRows2)
                        {
                            this.meisaiTable.Rows.Add(monthlyRow);
                        }
                    }
                    else
                    {
                        // ※該当取引先最後の取引毎税の場合は、最後の明細日付以上～明細日付月の末日以下で取引毎税を検索する
                        torihikiRow = this.GetCreateSeikyuzeiRow(oldTorihikiCD, param, kikannaiTbl);
                        if (torihikiRow != null)
                        {
                            this.meisaiTable.Rows.Add(torihikiRow);
                        }
                        // No.4212-->
                        else
                        {
                            if (UIConstans.ZEI_KEISAN_KBN_SEIKYUU == lastRow["ZEI_KEISAN_KBN_CD"].ToString() &&
                                UIConstans.ZEI_KBN_EXEMPTION == lastRow["ZEI_KBN_CD"].ToString())
                            {
                                CreateSeikyuuzeiRow(oldTorihikiCD, oldDate);
                            }
                        }
                        // No.4212<--
                    }

                    // 月次処理の消費税調整額を最後に挿入
                    var monthlyRow2 = CreateMonthlyTaxRows(oldTorihikiCD, param);
                    if (monthlyRow2 != null)
                    {
                        this.meisaiTable.Rows.Add(monthlyRow2);
                    }
                }
                if (param.OutPutKBN == 2)
                {
                    // 残りの期間内データが存在しない
                    // 取引先がいないかどうかのチェック
                    // あれば、前月繰越残高データを挿入する
                    while (index < this.torihikiList.Length)
                    {
                        //20211130 INS ST NAKAYAMA #157955
                        if(param.TorihikiKBN != 2)
                        {
                            //20211130 INS ED NAKAYAMA #157955
                            // 前月繰越残高の挿入
                            zandakaRow = this.GetCreateZandakaRow(param, this.torihikiList[index].TORIHIKISAKI_CD);
                            this.meisaiTable.Rows.Add(zandakaRow);
                            //20211130 INS ST NAKAYAMA #157955
                        }
                        //20211130 INS ED NAKAYAMA #157955

                        // 月次処理の消費税調整額を最後に挿入
                        var monthlyRowNotContains3 = CreateMonthlyTaxRows(this.torihikiList[index].TORIHIKISAKI_CD, param);
                        if (monthlyRowNotContains3 != null)
                        {
                            this.meisaiTable.Rows.Add(monthlyRowNotContains3);
                        }

                        // 次の取引先へ
                        index++;
                    }
                }

                // 月次消費税調整の挿入
                this.GetCreateAdjustTxtRow(param);
            }
            else		// 期間内データが存在しない場合
            {
                //20211130 INS ST NAKAYAMA #157955
                if (param.TorihikiKBN != 2)
                {
                //20211130 INS ED NAKAYAMA #157955
                    if (param.OutPutKBN == 2)
                    {
                        // 出力区分が「2:発生なし」の場合に限り、残高データのみ格納する
                        foreach (M_TORIHIKISAKI entity in this.torihikiList)
                        {
                            /// 前月繰越残高の挿入
                            zandakaRow = this.GetCreateZandakaRow(param, entity.TORIHIKISAKI_CD);
                            this.meisaiTable.Rows.Add(zandakaRow);

                            // 月次処理の消費税調整額を最後に挿入
                            var monthlyRowNotContains4 = CreateMonthlyTaxRows(entity.TORIHIKISAKI_CD, param);
                            if (monthlyRowNotContains4 != null)
                            {
                                this.meisaiTable.Rows.Add(monthlyRowNotContains4);
                            }
                        }
                    }
                //20211130 INS ST NAKAYAMA #157955
                }
                //20211130 INS ED NAKAYAMA #157955

                // 月次消費税調整の挿入
                this.GetCreateAdjustTxtRow(param);
            }

            if (0 != this.meisaiTable.Rows.Count)
            {
                // 数量と単位を結合、格納
                this.meisaiTable = this.SetSuuryouUnit(this.meisaiTable);

                // 現金の元帳の場合、差引残高の項目はブランクとする
                if (param.TorihikiKBN != 2)
                {
                    // 差引残高を算出、格納
                    this.meisaiTable = this.SetSashihikiZandaka(this.meisaiTable);
                }

                // 金額項目に対して端数処理を実行
                this.meisaiTable = this.SetFraction(this.meisaiTable);

                // 消費税内税項目に対して（）表記を追加
                this.meisaiTable = this.SetUchizeiDisp(this.meisaiTable);
            }

            // 検索完了用のフラグを立てる
            hadSearched = true;

            return this.meisaiTable;
        }

        #region private
        /// <summary>
        /// 指定された取引先CDの前月繰越残高を取得
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <param name="startDay">開始伝票日付</param>
        /// <returns name="decimal">前月繰越残高</returns>
        private decimal GetZengetsuZandaka(string torihikisakiCD, MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo param)
        {
            decimal zandaka = 0;
            decimal torihikiZandaka = 0;
            DateTime workDate;
            DateTime execDate;

            // Dateから日付を文字列にて取得
            string sDay = param.StartDay.Date.ToString();

            // 指定された取引先CDの開始伝票日付より直近かつ精算番号が最大の精算データから取引差引残高、精算日付を抽出
            DataTable table = this.MotochoDao.GetRecentSeisanZandaka(torihikisakiCD, sDay);

            // 取引先請求情報取得
            var torihikiInfo = this.TorihikiShiharaiDao.GetDataByCd(torihikisakiCD);

            // 取引差引残高を基に繰越残高取得
            if (table.Rows.Count != 0)
            {
                // 取引差引残高取得
                // ※直近の取引データのため、該当する取引先は単一
                torihikiZandaka = decimal.Parse(table.Rows[0]["TORIHIKI_ZANDAKA"].ToString());

                // 精算日付を取得
                execDate = DateTime.Parse(table.Rows[0]["EXEC_DATE"].ToString());
            }
            else
            {
                if (!torihikiInfo.KAISHI_KAIKAKE_ZANDAKA.IsNull)
                {
                    // 差引残高が取得出来なかった場合、取引先請求情報より開始買掛残高を前月繰越残高とする
                    torihikiZandaka += decimal.Parse(torihikiInfo.KAISHI_KAIKAKE_ZANDAKA.ToString());
                }

                // 取引マスタから適用開始日を取得
                M_TORIHIKISAKI entity = this.TorihikisakiDao.GetDataByCd(torihikisakiCD);
                execDate = DateTime.Parse(entity.TEKIYOU_BEGIN.ToString());

                //適用開始日が抽出期間(From)より未来の場合
                if (execDate > param.StartDay)
                {
                    // 抽出期間(From)を抽出範囲に設定
                    execDate = param.StartDay.AddDays(-1);
                }
                else
                {
                    execDate = execDate.AddDays(-1);
                }
            }

            // 精算日付翌日～開始伝票日付間の支払/出金データテーブルの取得(単一取引先CD)
            sDay = param.StartDay.AddDays(-1).ToString();     // 開始日-1まで
            workDate = execDate.AddDays(1);
            int kakuteiKBN;
            int.TryParse(this.commonInfo.SysInfo.SYS_KAKUTEI__TANNI_KBN.ToString(), out kakuteiKBN);
            DataTable shiharaiTbl = this.MotochoDao.GetIchiranData(torihikisakiCD, torihikisakiCD, workDate.Date.ToString(), sDay, kakuteiKBN, param.TorihikiKBN, param.Shimebi, param.TyuusyutuKBN, QueryFlg);

            // 前回締日以前の未締データを計上
            DataTable shiharaiTblForMishimeData = this.MotochoDao.GetIchiranDataForMishimeData(torihikisakiCD, torihikisakiCD, workDate.Date.ToString(), kakuteiKBN, param.TorihikiKBN, param.Shimebi, param.TyuusyutuKBN);
            shiharaiTbl.Merge(shiharaiTblForMishimeData);

            string oldDenNum = string.Empty;
            string oldDenshu = string.Empty;

            // 各金額を積算
            decimal shiKin = 0;
            decimal seiZei = 0;
            decimal meiZei = 0;
            decimal denZei = 0;
            decimal shuKin = 0;
            Dictionary<decimal, decimal> seisanMaiKingaku = new Dictionary<decimal, decimal>();
            foreach (DataRow row in shiharaiTbl.Rows)
            {
                if ("20" != row["DENSHU_KBN"].ToString())
                {
                    // 支払金額を積算
                    if (false == string.IsNullOrEmpty(row["SHIHARAI_KINGAKU"].ToString()))
                    {
                        shiKin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                        // 品名税区分が無く、税計算区分が取引毎税、税区分が外税で登録されていた場合、その金額は取引毎外税の算出対象となる
                        if (true == string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            if (UIConstans.ZEI_KEISAN_KBN_SEIKYUU == row["ZEI_KEISAN_KBN_CD"].ToString())
                            {
                                if (UIConstans.ZEI_KBN_SOTO == row["SHIHARAI_ZEI_KBN_CD"].ToString())
                                {
                                    // 取引毎外税算出用に保持
                                    decimal tempShouhizeiRate = 0;
                                    decimal tempShi = 0;
                                    if (null != row["SHIHARAI_SHOUHIZEI_RATE"] && !String.IsNullOrEmpty(row["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                    {
                                        tempShouhizeiRate = Decimal.Parse(row["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                        if (!seisanMaiKingaku.ContainsKey(tempShouhizeiRate))
                                        {
                                            seisanMaiKingaku.Add(tempShouhizeiRate, tempShi);
                                        }
                                        else
                                        {
                                            tempShi = seisanMaiKingaku[tempShouhizeiRate];
                                        }
                                        tempShi += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                        seisanMaiKingaku[tempShouhizeiRate] = tempShi;
                                    }
                                }
                            }
                        }
                    }

                    // 明細毎税を積算
                    if (false == string.IsNullOrEmpty(row["SHOUHI_SOTO_ZEI"].ToString()))
                    {
                        meiZei += decimal.Parse(row["SHOUHI_SOTO_ZEI"].ToString());
                    }

                    // 伝票毎税を積算
                    if ((oldDenNum != row["DENPYOU_NUMBER"].ToString()) || (oldDenshu != row["DENSHU_KBN"].ToString()))
                    {
                        if (false == string.IsNullOrEmpty(row["DENPYOU_MAI_SOTO_ZEI"].ToString()))
                        {
                            denZei += decimal.Parse(row["DENPYOU_MAI_SOTO_ZEI"].ToString());
                        }
                    }
                }
                else
                {
                    // 出金金額を積算
                    if (false == string.IsNullOrEmpty(row["SHUKKIN_KINGAKU"].ToString()))
                    {
                        shuKin += decimal.Parse(row["SHUKKIN_KINGAKU"].ToString());
                    }
                }

                // 比較値を更新
                oldDenNum = row["DENPYOU_NUMBER"].ToString();
                oldDenshu = row["DENSHU_KBN"].ToString();
            }

            // 請求毎税を算出
            foreach (var kingakuAndShouhizeiRate in seisanMaiKingaku)
            {
                decimal shouhizeiRate = kingakuAndShouhizeiRate.Key;
                decimal kingaku = kingakuAndShouhizeiRate.Value;
                decimal tempShouhiziKingaku = (shouhizeiRate * kingaku);
                if (torihikiInfo != null && !torihikiInfo.TAX_HASUU_CD.IsNull)
                {
                    tempShouhiziKingaku = DBAccessor.FractionCalc(tempShouhiziKingaku, (short)torihikiInfo.TAX_HASUU_CD);
                }
                seiZei += tempShouhiziKingaku;
            }

            // 繰越残高を算出
            // ((取引差引残高＋支払金額＋明細毎税＋伝票毎税 + 請求毎税)‐出金額)
            zandaka = (torihikiZandaka + shiKin + meiZei + denZei + seiZei) - shuKin;

            return zandaka;
        }

        /// <summary>
        /// 差引残高のセット
        /// </summary>
        /// <param name="table">格納対象データテーブル</param>
        /// <returns name="DataTable">格納後のデータテーブル</returns>
        private DataTable SetSashihikiZandaka(DataTable table)
        {
            // 差引残高を算出、格納
            decimal zandaka = 0;
            DataTable workTable = table.Copy();
            workTable.Columns["SASHIHIKI_ZANDAKA"].ReadOnly = false;

            string kbnCD = string.Empty;
            workTable.Columns["SHIHARAI_KINGAKU"].ReadOnly = false;

            foreach (DataRow row in workTable.Rows)
            {
                if (row["HINMEI_NAME"].ToString() == "前月繰越金額")
                {
                    // 繰越金額を初期値とする
                    zandaka = decimal.Parse(row["SASHIHIKI_ZANDAKA"].ToString());
                }
                else
                {
                    if ("20" == row["DENSHU_KBN"].ToString())
                    {
                        // 出金伝票の場合は、差引残高から出金金額を減算する
                        if (false == string.IsNullOrEmpty(row["SHUKKIN_KINGAKU"].ToString()))
                        {
                            zandaka -= decimal.Parse(row["SHUKKIN_KINGAKU"].ToString());
                        }
                    }
                    else
                    {
                        // 取引区分が現金の場合は差引残高への積算を行わない
                        if ("現金" != row["TORIHIKI_KBN"].ToString())
                        {
                            // それ以外の伝票の場合は、項目に応じて加算する金額を切り替える
                            switch (row["HINMEI_NAME"].ToString())
                            {
                                case "【伝票毎消費税】":
                                    // 伝票毎消費税の場合は、伝票毎消費税項目を加算する
                                    if (false == string.IsNullOrEmpty(row["DENPYOU_MAI_SOTO_ZEI"].ToString()))
                                    {
                                        zandaka += decimal.Parse(row["DENPYOU_MAI_SOTO_ZEI"].ToString());
                                    }
                                    break;
                                case "【精算毎消費税】":
                                    // 精算毎消費税の場合は、精算毎消費税項目を加算する
                                    if (false == string.IsNullOrEmpty(row["SHOUHIZEI"].ToString()))
                                    {
                                        zandaka += decimal.Parse(row["SHOUHIZEI"].ToString());
                                    }
                                    break;
                                case "【消費税調整額】":
                                    // 消費税調整額の場合は、消費税項目を加算する
                                    if (false == string.IsNullOrEmpty(row["SHOUHIZEI"].ToString()))
                                    {
                                        zandaka += decimal.Parse(row["SHOUHIZEI"].ToString());
                                    }
                                    break;
                                case "月次消費税調整":
                                    // 月次消費税の場合は、伝票毎消費税項目を加算する
                                    if (false == string.IsNullOrEmpty(row["SHOUHIZEI"].ToString()))
                                    {
                                        zandaka += decimal.Parse(row["SHOUHIZEI"].ToString());
                                    }
                                    break;
                                default:
                                    if (false == string.IsNullOrEmpty(row["SHIHARAI_KINGAKU"].ToString()))
                                    {
                                        // 税区分CDの格納
                                        // 明細行は品名税区分CDも考慮に入れる
                                        if ("【伝票毎消費税】" == row["HINMEI_NAME"].ToString())
                                        {
                                            kbnCD = row["ZEI_KBN_CD"].ToString();
                                        }
                                        else
                                        {
                                            if (false == string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                                            {
                                                // 品名税区分CDが存在する場合は、品名税区分CDを用いる
                                                kbnCD = row["HINMEI_ZEI_KBN_CD"].ToString();
                                            }
                                            else
                                            {
                                                kbnCD = row["ZEI_KBN_CD"].ToString();
                                            }
                                        }

                                        if ((kbnCD == UIConstans.ZEI_KBN_UCHI) && (0 != decimal.Parse(row["SHIHARAI_SHOUHIZEI_RATE"].ToString())))
                                        {
                                            // 税区分が内税の場合は支払金額（税抜）とする
                                            row["SHIHARAI_KINGAKU"] = decimal.Parse(row["SHIHARAI_KINGAKU"].ToString()) - decimal.Parse(row["SHOUHIZEI"].ToString());
                                        }

                                        zandaka += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                    }
                                    if (false == string.IsNullOrEmpty(row["SHOUHI_SOTO_ZEI"].ToString()))
                                    {
                                        zandaka += decimal.Parse(row["SHOUHI_SOTO_ZEI"].ToString());
                                    }
                                    break;
                            }
                        }
                    }

                    // 差引残高をセット
                    row["SASHIHIKI_ZANDAKA"] = zandaka;
                }
            }

            return workTable;
        }

        /// <summary>
        /// 各データに対して端数処理の実行
        /// </summary>
        /// <param name="table">変換対象データテーブル</param>
        /// <returns name="DataTable">端数変換後のデータテーブル</returns>
        private DataTable SetFraction(DataTable table)
        {
            DataTable workTable = table.Copy();

            // 端数処理をかけるデータ列を書き込み可能とする
            workTable.Columns["TANKA"].ReadOnly = false;
            workTable.Columns["SHIHARAI_KINGAKU"].ReadOnly = false;
            workTable.Columns["SHUKKIN_KINGAKU"].ReadOnly = false;
            workTable.Columns["SASHIHIKI_ZANDAKA"].ReadOnly = false;
            workTable.Columns["SHOUHIZEI"].ReadOnly = false;

            // 比較値をセット
            string oldTorihikiCD = workTable.Rows[0]["TORIHIKISAKI_CD"].ToString();

            // 取引先取引情報取得
            var torihikiInfo = this.TorihikiShiharaiDao.GetDataByCd(oldTorihikiCD);

            /**********************************************************************/
            /**		TODO: 取引情報が存在しない場合の端数処理実行有無、など		**/
            /**		詳細な検討が必要										**/
            /**********************************************************************/

            // 表示している取引先取引情報から端数CDを取得
            int kingakuHasuuCD = -1;
            int taxHasuuCD = -1;
            if (torihikiInfo != null)
            {
                if (torihikiInfo.KINGAKU_HASUU_CD != SqlInt16.Null)
                {
                    kingakuHasuuCD = int.Parse(torihikiInfo.KINGAKU_HASUU_CD.ToString());
                }
                if (torihikiInfo.TAX_HASUU_CD != SqlInt16.Null)
                {
                    taxHasuuCD = int.Parse(torihikiInfo.TAX_HASUU_CD.ToString());
                }
            }

            foreach (DataRow row in workTable.Rows)
            {
                // 指定取引先の取引先取引情報を取得
                if (oldTorihikiCD != row["TORIHIKISAKI_CD"].ToString())
                {
                    // 取引先取引情報更新、端数CD初期化
                    torihikiInfo = this.TorihikiShiharaiDao.GetDataByCd(row["TORIHIKISAKI_CD"].ToString());
                    kingakuHasuuCD = -1;
                    taxHasuuCD = -1;

                    // 取引先取引情報から端数CDを取得
                    if (torihikiInfo != null)
                    {
                        if (torihikiInfo.KINGAKU_HASUU_CD != SqlInt16.Null)
                        {
                            kingakuHasuuCD = int.Parse(torihikiInfo.KINGAKU_HASUU_CD.ToString());
                        }
                        if (torihikiInfo.TAX_HASUU_CD != SqlInt16.Null)
                        {
                            taxHasuuCD = int.Parse(torihikiInfo.TAX_HASUU_CD.ToString());
                        }
                    }
                }

                // 金額端数処理結果を格納
                if (kingakuHasuuCD != -1)
                {
                    if (false == string.IsNullOrEmpty(row["TANKA"].ToString()))
                    {
                        row["TANKA"] = CommonCalc.FractionCalc(decimal.Parse(row["TANKA"].ToString()), kingakuHasuuCD);
                    }
                    if (false == string.IsNullOrEmpty(row["SHIHARAI_KINGAKU"].ToString()))
                    {
                        row["SHIHARAI_KINGAKU"] = CommonCalc.FractionCalc(decimal.Parse(row["SHIHARAI_KINGAKU"].ToString()), kingakuHasuuCD);
                    }
                    if (false == string.IsNullOrEmpty(row["SHUKKIN_KINGAKU"].ToString()))
                    {
                        row["SHUKKIN_KINGAKU"] = CommonCalc.FractionCalc(decimal.Parse(row["SHUKKIN_KINGAKU"].ToString()), kingakuHasuuCD);
                    }
                    if (false == string.IsNullOrEmpty(row["SASHIHIKI_ZANDAKA"].ToString()))
                    {
                        row["SASHIHIKI_ZANDAKA"] = CommonCalc.FractionCalc(decimal.Parse(row["SASHIHIKI_ZANDAKA"].ToString()), kingakuHasuuCD);
                    }
                }

                // 税額端数処理結果を格納
                if (taxHasuuCD != -1)
                {
                    if (false == string.IsNullOrEmpty(row["SHOUHIZEI"].ToString()))
                    {
                        row["SHOUHIZEI"] = CommonCalc.FractionCalc(decimal.Parse(row["SHOUHIZEI"].ToString()), taxHasuuCD);
                    }
                }

                // 比較値の更新
                oldTorihikiCD = row["TORIHIKISAKI_CD"].ToString();
            }

            return workTable;
        }

        /// <summary>
        /// 指定された取引先CDの前月繰越残高行を作成
        /// </summary>
        /// <param name="param">範囲条件情報</param>
        /// <param name="torihikisakiCD">取引先CD</param>
        private DataRow GetCreateZandakaRow(MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo param, string torihikisakiCD)
        {
            // 指定された取引先CDの前月繰越残高行を作成
            DataRow zandakaRow = this.meisaiTable.NewRow();
            zandakaRow["MEISAI_DATE"] = param.StartDay;
            zandakaRow["HINMEI_NAME"] = "前月繰越金額";

            if (IsMonthlyTarget(param) && ContainsTorihikisakiCdForMonthly(torihikisakiCD))
            {
                // 月次処理データが存在する場合は、月次データから繰越残を取得
                zandakaRow["SASHIHIKI_ZANDAKA"] = this.monthlyNewTable.AsEnumerable()
                                                                   .Where(n => n["TORIHIKISAKI_CD"].ToString().Equals(torihikisakiCD))
                                                                   .Select(n => n["SASHIHIKI_URIAGE_ZANDAKA"])
                                                                   .ToList()
                                                                   .FirstOrDefault();
            }
            else
            {
                zandakaRow["SASHIHIKI_ZANDAKA"] = this.GetZengetsuZandaka(torihikisakiCD, param);
            }

            // 取引マスタからCDと略名を取得
            M_TORIHIKISAKI entity = null;
            if (this.torihikisakiAllList != null)
            {
                entity = this.torihikisakiAllList.Where(n => n.TORIHIKISAKI_CD.Equals(torihikisakiCD))
                                                 .ToList()
                                                 .FirstOrDefault();
            }

            if (entity != null)
            {
                zandakaRow["TORIHIKISAKI_CD"] = entity.TORIHIKISAKI_CD;
                zandakaRow["TORIHIKISAKI_NAME"] = entity.TORIHIKISAKI_NAME_RYAKU;
            }
            else
            {
                // 該当取引先が無い場合は、略名にブランクを格納する
                zandakaRow["TORIHIKISAKI_CD"] = torihikisakiCD;
                zandakaRow["TORIHIKISAKI_NAME"] = string.Empty;
            }

            // 行を返却
            return zandakaRow;
        }

        /// <summary>
        /// 指定された取引先CDが月次締処理データに含まれているか判定
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <returns></returns>
        private bool ContainsTorihikisakiCdForMonthly(string torihikisakiCD)
        {
            if (this.monthlyNewTable == null || string.IsNullOrEmpty(torihikisakiCD))
            {
                return false;
            }

            bool contains = this.monthlyNewTable.AsEnumerable()
                                             .Where(n => !DBNull.Value.Equals(n["TORIHIKISAKI_CD"])
                                                      && torihikisakiCD.Equals(n["TORIHIKISAKI_CD"].ToString()))
                                             .ToList()
                                             .Any();

            return contains;
        }

        /// <summary>
        /// 伝票毎税行を作成
        /// </summary>
        private DataRow GetCreateMaizeiRow(MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo param)
        {
            // 伝票毎税の挿入
            DataRow maizeiRow = this.meisaiTable.NewRow();
            maizeiRow["DENSHU_KBN"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["DENSHU_KBN"];
            maizeiRow["MEISAI_DATE"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["MEISAI_DATE"];
            maizeiRow["TORIHIKI_KBN"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["TORIHIKI_KBN"];
            maizeiRow["DENPYOU_NUMBER"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["DENPYOU_NUMBER"];
            maizeiRow["GYOUSHA_CD"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["GYOUSHA_CD"];
            maizeiRow["GENBA_CD"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["GENBA_CD"];
            maizeiRow["GYOUSHA_NAME"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["GYOUSHA_NAME"];
            maizeiRow["GENBA_NAME"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["GENBA_NAME"];
            maizeiRow["HINMEI_NAME"] = "【伝票毎消費税】";
            maizeiRow["SHOUHIZEI"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["DENPYOU_MAI_ZEI"];
            maizeiRow["DENPYOU_MAI_SOTO_ZEI"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["DENPYOU_MAI_SOTO_ZEI"];
            maizeiRow["TORIHIKISAKI_CD"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["TORIHIKISAKI_CD"];
            maizeiRow["TORIHIKISAKI_NAME"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["TORIHIKISAKI_NAME"];
            maizeiRow["ZEI_KEISAN_KBN_CD"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["ZEI_KEISAN_KBN_CD"];
            maizeiRow["ZEI_KBN_CD"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["ZEI_KBN_CD"];
            maizeiRow["HINMEI_ZEI_KBN_CD"] = this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["HINMEI_ZEI_KBN_CD"];


            // 現金の元帳の場合
            if (param.TorihikiKBN == 2)
            {
                // 伝票毎の支払金額の合計を算出
                var denpyouKingaku = this.meisaiTable.Compute("Sum(SHIHARAI_KINGAKU)", "DENPYOU_NUMBER = " + this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["DENPYOU_NUMBER"].ToString());
                // 伝票毎の消費税外税の合計を算出
                var syouhiSotoSei = (decimal)this.meisaiTable.Compute("Sum(SHOUHI_SOTO_ZEI)", "DENPYOU_NUMBER = " + this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["DENPYOU_NUMBER"].ToString());
                var syouhiSei = (decimal)this.meisaiTable.Compute("Sum(SHOUHIZEI)", "DENPYOU_NUMBER = " + this.meisaiTable.Rows[this.meisaiTable.Rows.Count - 1]["DENPYOU_NUMBER"].ToString());
                // 伝票の支払金額合計＋消費税
                maizeiRow["SHUKKIN_KINGAKU"] = (decimal)denpyouKingaku + (decimal)maizeiRow["SHOUHIZEI"] + (decimal)syouhiSei;
                //【伝票毎消費税】に内税消費税を加算する
                maizeiRow["SHOUHIZEI"] = (decimal)maizeiRow["SHOUHIZEI"] + syouhiSei - syouhiSotoSei;
            }


            // 行を返却
            return maizeiRow;
        }

        // No.4212-->
        /// <summary>
        /// 明細行に取引先CDの請求毎税行を作成
        /// </summary>
        /// <param name="torihikiCD"></param>
        /// <param name="date"></param>
        private void CreateSeikyuuzeiRow(string torihikiCD, DateTime date)
        {
            DataRow newSeikyuzeiRow = this.meisaiTable.NewRow();
            if (newSeikyuzeiRow != null)
            {
                newSeikyuzeiRow["HINMEI_NAME"] = "【精算毎消費税】";
                newSeikyuzeiRow["SHOUHIZEI"] = "0";
                newSeikyuzeiRow["MEISAI_DATE"] = date;
                M_TORIHIKISAKI entity = null;
                if (this.torihikisakiAllList != null)
                {
                    entity = this.torihikisakiAllList.Where(n => n.TORIHIKISAKI_CD.Equals(torihikiCD))
                                                     .ToList()
                                                     .FirstOrDefault();
                }
                if (entity != null)
                {
                    newSeikyuzeiRow["TORIHIKISAKI_CD"] = entity.TORIHIKISAKI_CD;
                    newSeikyuzeiRow["TORIHIKISAKI_NAME"] = entity.TORIHIKISAKI_NAME_RYAKU;
                }
                else
                {
                    // 該当取引先が無い場合は、略名にブランクを格納する
                    newSeikyuzeiRow["TORIHIKISAKI_CD"] = torihikiCD;
                    newSeikyuzeiRow["TORIHIKISAKI_NAME"] = string.Empty;
                }
                this.meisaiTable.Rows.Add(newSeikyuzeiRow);
            }
        }
        // No.4212<--

        /// <summary>
        /// 指定された取引先CDの取引毎税行を作成
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <param name="seisanDate">前の行の明細日付</param>
        /// <param name="nextDate">現在行明細日付</param>
        /// <returns name="DataRow">取引毎税行(存在しない場合はnullを返却)</returns>
        private DataRow GetCreateSeikyuzeiRow(string torihikisakiCD, MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo param, DataTable kikannaiTbl)
        {
            decimal shouhizei = 0;

            // Dateから日付を文字列にて取得
            string sDay = param.StartDay.Date.ToString();
            string eDay = param.EndDay.Date.ToString();

            M_TORIHIKISAKI_SHIHARAI entity = new M_TORIHIKISAKI_SHIHARAI();
            // 取引先支払情報から締日を取得
            entity = this.TorihikiShiharaiDao.GetDataByCd(torihikisakiCD);

            // 取引先の書式区分取得
            int shosikiKbn = 0;
            if (!entity.SHOSHIKI_KBN.IsNull && !string.IsNullOrEmpty(entity.SHOSHIKI_KBN.ToString()))
            {
                shosikiKbn = entity.SHOSHIKI_KBN.Value;
            }

            // GetSeikyuuDataメソッドの戻り値のカラム名
            string COLUMN_NAME_SEISAN_NUMBER = "SEISAN_NUMBER";
            string COLUMN_NAME_SYSTEM_ID = "SYSTEM_ID";
            string COLUMN_NAME_KONKAI_SEI_UTIZEI_GAKU = "KONKAI_SEI_UTIZEI_GAKU";
            string COLUMN_NAME_KONKAI_SEI_SOTOZEI_GAKU = "KONKAI_SEI_SOTOZEI_GAKU";

            /**
             * 期間内請求伝票消費税データの取得
             */
            DataTable kikannaiSeikyuuData = this.MotochoDao.GetTorihikiMaiZeiIchiran(torihikisakiCD, sDay, eDay);

            // 取得した請求番号の一覧を作成
            List<long> allSeikyuuNumber = new List<long>();
            foreach (DataRow row in kikannaiSeikyuuData.Rows)
            {
                if (string.IsNullOrEmpty(Convert.ToString(row[COLUMN_NAME_SEISAN_NUMBER])))
                {
                    continue;
                }

                long tempSeikyuuNumber = 0;
                if (long.TryParse(Convert.ToString(row[COLUMN_NAME_SEISAN_NUMBER]), out tempSeikyuuNumber)
                    && !allSeikyuuNumber.Contains(tempSeikyuuNumber))
                {
                    allSeikyuuNumber.Add(tempSeikyuuNumber);
                }
            }

            // 請求伝票で、指定日付範囲内に全ての売上伝票が存在する場合は税額を計上する
            List<long> calcTargetSeikyuuNumber = new List<long>();
            foreach (long seikyuuNumber in allSeikyuuNumber)
            {
                var seikyuuData = kikannaiSeikyuuData.AsEnumerable().Where(s => Convert.ToInt64(s[COLUMN_NAME_SEISAN_NUMBER]) == seikyuuNumber);
                bool isExisitData = false;
                bool isCalcShouhizei = true;
                foreach (var row in seikyuuData)
                {
                    isExisitData = true;
                    // 一個でもnullがあったら、指定日付範囲内に締めた伝票が含まれていない
                    if (row[COLUMN_NAME_SYSTEM_ID] == null
                        || string.IsNullOrEmpty(row[COLUMN_NAME_SYSTEM_ID].ToString()))
                    {
                        long tempSeikyuuDenpyou = 0;
                        // 売上伝票の情報からデータを算出する対象請求番号をセット
                        if (row[COLUMN_NAME_SEISAN_NUMBER] != null
                            && long.TryParse(Convert.ToString(row[COLUMN_NAME_SEISAN_NUMBER]), out tempSeikyuuDenpyou)
                            && !calcTargetSeikyuuNumber.Contains(tempSeikyuuDenpyou))
                        {
                            calcTargetSeikyuuNumber.Add(tempSeikyuuDenpyou);
                        }

                        isCalcShouhizei = false;
                        break;
                    }
                }

                if (isExisitData && isCalcShouhizei)
                {
                    var dataRow = seikyuuData.CopyToDataTable<DataRow>();
                    // ここまでくれば、指定日付範囲内に全ての売上伝票が作成されていると判断
                    shouhizei += ((decimal)dataRow.Rows[0][COLUMN_NAME_KONKAI_SEI_UTIZEI_GAKU] + (decimal)dataRow.Rows[0][COLUMN_NAME_KONKAI_SEI_SOTOZEI_GAKU]);
                }
            }

            /**
             * 期間内未締伝票の税計算(税計算区分：2(請求毎))
             */
            var mishimeDenpyouData = kikannaiTbl.AsEnumerable().Where(s => (torihikisakiCD.Equals(Convert.ToString(s["TORIHIKISAKI_CD"])))
                                                    && (DENSHU_KBN.SHUKKIN.GetHashCode() != Convert.ToInt32(s["DENSHU_KBN"]))
                                                    );

            if (mishimeDenpyouData != null)
            {
                DataView view = mishimeDenpyouData.AsDataView();
                switch (shosikiKbn)
                {
                    // 支払明細書書式が１.「取引先毎」の場合
                    case (0):
                    case (1):
                        break;

                    // 支払明細書書式が２.「業者毎」か３.「現場毎」の場合
                    case (2):
                    case (3):
                        view.Sort = "GYOUSHA_CD, GENBA_CD";
                        break;

                    default:
                        break;
                }
                mishimeDenpyouData = view.ToTable().AsEnumerable();
            }

            // 未締伝票が無ければ消費税は足さない
            decimal seisanMaiShouhizei = 0;
            if (mishimeDenpyouData != null)
            {
                // 税率でグルーピングし、取引先の「支払明細書書式１」に準じて税率を算出
                List<decimal> shouhizeiRates = new List<decimal>();
                foreach (var row in mishimeDenpyouData)
                {
                    string shiharaiZeiKbnCd = "0";
                    string hinmeiZeiKbnCd = "0";
                    shiharaiZeiKbnCd = Convert.ToString(row["SHIHARAI_ZEI_KBN_CD"]);
                    hinmeiZeiKbnCd = Convert.ToString(row["HINMEI_ZEI_KBN_CD"]);

                    if (!string.IsNullOrEmpty(Convert.ToString(row["HINMEI_ZEI_KBN_CD"]))
                        && UIConstans.ZEI_KBN_EXEMPTION == hinmeiZeiKbnCd)
                    {
                        continue;
                    }
                    else if (string.IsNullOrEmpty(Convert.ToString(row["HINMEI_ZEI_KBN_CD"]))
                        && (UIConstans.ZEI_KBN_SOTO != shiharaiZeiKbnCd))
                    {
                        continue;
                    }
                    else if (row["SEID_SEISAN_NUMBER"] != null
                        && !string.IsNullOrEmpty(row["SEID_SEISAN_NUMBER"].ToString())
                        && !calcTargetSeikyuuNumber.Contains(Convert.ToInt64(row["SEID_SEISAN_NUMBER"])))
                    {
                        // calcTargetSeikyuuNumberに含まれていない場合は
                        // 請求伝票から税金を計算済み
                        continue;
                    }

                    decimal shouhiZeiRate = 0;
                    if (decimal.TryParse(Convert.ToString(row["SHIHARAI_SHOUHIZEI_RATE"]), out shouhiZeiRate))
                    {
                        if (!shouhizeiRates.Contains(shouhiZeiRate))
                        {
                            shouhizeiRates.Add(shouhiZeiRate);
                        }
                    }
                }
                foreach (var tempShouhizeiRate in shouhizeiRates)
                {
                    decimal tempShouhizeiSum = 0;
                    var groupByShouhizei = mishimeDenpyouData.Where(s => Convert.ToDecimal(s["SHIHARAI_SHOUHIZEI_RATE"]) == tempShouhizeiRate);
                    switch (shosikiKbn)
                    {
                        // 支払明細書書式が１.「取引先毎」の場合
                        case (0):
                        case (1):
                            {
                                foreach (var row in groupByShouhizei)
                                {
                                    if (!SeisanShouhizeiCheck(row))
                                    {
                                        continue;
                                    }
                                    tempShouhizeiSum += Convert.ToDecimal(row["SHIHARAI_KINGAKU"]) - Convert.ToDecimal(row["SHOUHIZEI"]);
                                }
                                seisanMaiShouhizei += tempShouhizeiSum * tempShouhizeiRate;
                            }
                            break;

                        // 支払明細書書式が２.「業者毎」の場合
                        case (2):
                            {
                                // 最初の行を前回値の初期値に設定
                                string beforeGyoushaCd = groupByShouhizei.AsDataView()[0].Row["GYOUSHA_CD"].ToString();
                                int rowCount = groupByShouhizei.AsDataView().Count;

                                foreach (var row in groupByShouhizei)
                                {
                                    // 業者CD毎に精算毎消税を計算
                                    rowCount--;
                                    if (beforeGyoushaCd != row["GYOUSHA_CD"].ToString() || rowCount == 0)
                                    {
                                        // 最終行では必ず計算する
                                        if (rowCount == 0)
                                        {
                                            if (SeisanShouhizeiCheck(row))
                                            {
                                                tempShouhizeiSum += Convert.ToDecimal(row["SHIHARAI_KINGAKU"]) - Convert.ToDecimal(row["SHOUHIZEI"]);
                                            }
                                        }
                                        // 業者毎の精算毎税を加算
                                        seisanMaiShouhizei += tempShouhizeiSum * tempShouhizeiRate;
                                        tempShouhizeiSum = 0;
                                    }

                                    if (!SeisanShouhizeiCheck(row))
                                    {
                                        continue;
                                    }

                                    // 売上金額を加算
                                    tempShouhizeiSum += Convert.ToDecimal(row["SHIHARAI_KINGAKU"]) - Convert.ToDecimal(row["SHOUHIZEI"]);

                                    // 前回値を保持
                                    beforeGyoushaCd = row["GYOUSHA_CD"].ToString();
                                }
                            }
                            break;

                        // 支払明細書書式が３.「現場毎」の場合
                        case (3):
                            {
                                // 最初の行を前回値の初期値に設定
                                string beforeGyoushaCd = groupByShouhizei.AsDataView()[0].Row["GYOUSHA_CD"].ToString();
                                string beforeGengaCd = groupByShouhizei.AsDataView()[0].Row["GENBA_CD"].ToString();
                                int rowCount = groupByShouhizei.AsDataView().Count;

                                foreach (var row in groupByShouhizei)
                                {
                                    // 現場CD毎に精算毎消税を計算
                                    rowCount--;
                                    if (beforeGengaCd != row["GENBA_CD"].ToString() || rowCount == 0)
                                    {
                                        if (beforeGyoushaCd != row["GYOUSHA_CD"].ToString() || rowCount == 0)
                                        {
                                            // 最終行では必ず計算する
                                            if (rowCount == 0)
                                            {
                                                if (SeisanShouhizeiCheck(row))
                                                {
                                                    tempShouhizeiSum += Convert.ToDecimal(row["SHIHARAI_KINGAKU"]) - Convert.ToDecimal(row["SHOUHIZEI"]);
                                                }
                                            }
                                            // 業者毎の精算毎税を加算
                                            seisanMaiShouhizei += tempShouhizeiSum * tempShouhizeiRate;
                                            tempShouhizeiSum = 0;
                                        }
                                    }

                                    if (!SeisanShouhizeiCheck(row))
                                    {
                                        continue;
                                    }

                                    // 売上金額を加算
                                    tempShouhizeiSum += Convert.ToDecimal(row["SHIHARAI_KINGAKU"]) - Convert.ToDecimal(row["SHOUHIZEI"]);

                                    // 前回値を保持
                                    beforeGyoushaCd = row["GYOUSHA_CD"].ToString();
                                    beforeGengaCd = row["GENBA_CD"].ToString();
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
            }

            // 請求毎外税の端数処理
            if (entity != null && !entity.TAX_HASUU_CD.IsNull)
            {
                seisanMaiShouhizei = DBAccessor.FractionCalc(seisanMaiShouhizei, (short)entity.TAX_HASUU_CD);
            }

            shouhizei += seisanMaiShouhizei;

            DataRow torihikizeiRow = this.meisaiTable.NewRow();
            DataRow[] rows = kikannaiTbl.Select(string.Format("(TORIHIKISAKI_CD LIKE '%{0}%')", torihikisakiCD));

            if (shouhizei != 0 && shouhizei != null)
            {
                if (rows.Length != 0)
                {
                    if (torihikizeiRow != null)
                    {
                        // 指定されたCDと合致する行を取得し取引毎税、日付を返却
                        torihikizeiRow["HINMEI_NAME"] = "【精算毎消費税】";
                        torihikizeiRow["SHOUHIZEI"] = shouhizei;
                        torihikizeiRow["MEISAI_DATE"] = eDay;
                        torihikizeiRow["TORIHIKISAKI_CD"] = rows[0]["TORIHIKISAKI_CD"];
                        torihikizeiRow["TORIHIKISAKI_NAME"] = rows[0]["TORIHIKISAKI_NAME"];
                    }
                }
            }
            else
            {
                // 該当取引先が無い場合は、表示を行わない
                torihikizeiRow = null;
            }
            // 行を返却
            return torihikizeiRow;
        }

        /// <summary>
        /// 精算毎消費税に該当するデータがチェックします
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <returns></returns>
        private bool SeisanShouhizeiCheck(DataRow row)
        {
            bool ren = false;

            short shiharaiZeiKbnCd = 0;
            short hinmeiZeiKbnCd = 0;
            short.TryParse(Convert.ToString(row["SHIHARAI_ZEI_KBN_CD"]), out shiharaiZeiKbnCd);
            short.TryParse(Convert.ToString(row["HINMEI_ZEI_KBN_CD"]), out hinmeiZeiKbnCd);

            if (null == row["HINMEI_ZEI_KBN_CD"] || String.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
            {
                //請求毎外税（品名税無）
                if (UIConstans.ZEI_KEISAN_KBN_SEIKYUU == row["ZEI_KEISAN_KBN_CD"].ToString()
                    && UIConstans.ZEI_KBN_SOTO == row["ZEI_KBN_CD"].ToString())
                {
                    ren = true;
                }
                //明細毎内税（品名税なし）
                if (UIConstans.ZEI_KEISAN_KBN_MEISAI == row["ZEI_KEISAN_KBN_CD"].ToString()
                    && UIConstans.ZEI_KBN_UCHI == row["ZEI_KBN_CD"].ToString())
                {
                    ren = true;
                }
            }
            else
            {
                //品名内税
                if (UIConstans.ZEI_KBN_UCHI == row["HINMEI_ZEI_KBN_CD"].ToString())
                {
                    ren = true;
                }
            }

            return ren;
        }

        /// <summary>
        /// 数量・単位の結合及び代入
        /// </summary>
        /// <param name="table">格納対象データテーブル</param>
        /// <returns name="DataTable">格納後のデータテーブル</returns>
        private DataTable SetSuuryouUnit(DataTable table)
        {
            DataTable workTable = table.Copy();
            workTable.Columns["SUURYOU_UNIT"].ReadOnly = false;
            workTable.Columns["SUURYOU_UNIT"].MaxLength = UIConstans.MAX_LENGTH;
            foreach (DataRow row in workTable.Rows)
            {
                if (row["HINMEI_NAME"].ToString() != "前月繰越金額"
                    && row["HINMEI_NAME"].ToString() != "【伝票毎消費税】"
                    && row["HINMEI_NAME"].ToString() != "【精算毎消費税】")
                {
                    // 数量端数処理
                    string formatStr = this.commonInfo.SysInfo.SYS_SUURYOU_FORMAT;
                    string Suuryou = FormatUtility.ToAmountValue(row["SUURYOU"].ToString(), formatStr);

                    // 単位の結合及び代入
                    if (false == string.IsNullOrEmpty(Suuryou))
                    {
                        row["SUURYOU_UNIT"] = Suuryou + row["UNIT_NAME_RYAKU"].ToString();
                    }
                }
            }
            return workTable;
        }

        /// <summary>
        /// 消費税内税項目に対して（）表記を追加
        /// </summary>
        /// <param name="table">格納対象データテーブル</param>
        /// <returns name="DataTable">格納後のデータテーブル</returns>
        private DataTable SetUchizeiDisp(DataTable table)
        {
            string kbnCD = string.Empty;

            DataTable workTable = table.Copy();

            // 出力行の追加
            workTable.Columns.Add("SHOUHIZEI_STR", typeof(string));

            foreach (DataRow row in workTable.Rows)
            {
                // 税区分CDの格納
                // 明細行は品名税区分CDも考慮に入れる
                if ("【伝票毎消費税】" == row["HINMEI_NAME"].ToString())
                {
                    kbnCD = row["ZEI_KBN_CD"].ToString();
                }
                else
                {
                    if (false == string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        // 品名税区分CDが存在する場合は、品名税区分CDを用いる
                        kbnCD = row["HINMEI_ZEI_KBN_CD"].ToString();
                    }
                    else
                    {
                        kbnCD = row["ZEI_KBN_CD"].ToString();
                    }
                }

                if (false == string.IsNullOrEmpty(row["SHOUHIZEI"].ToString()))
                {
                    // 税区分が内税でも税額0の場合は()表記を行わない
                    if (kbnCD == UIConstans.ZEI_KBN_UCHI)
                    {
                        if ("【伝票毎消費税】" == row["HINMEI_NAME"].ToString())
                        {
                            // 出力の正規化
                            row["SHOUHIZEI_STR"] = string.Format("{0:#,0}", row["SHOUHIZEI"]);
                        }
                        else
                        {
                            // 税区分が内税の場合は「内税抜」と表示
                            row["SHOUHIZEI_STR"] = "内税抜";
                        }
                    }
                    else
                    {
                        // 出力の正規化
                        row["SHOUHIZEI_STR"] = string.Format("{0:#,0}", row["SHOUHIZEI"]);
                    }
                }
                else
                {
                    // 出力の正規化
                    row["SHOUHIZEI_STR"] = string.Format("{0:#,0}", row["SHOUHIZEI"]);
                }
            }

            return workTable;
        }

        /// <summary>
        /// 指定された端数CDに従い、金額の端数処理を行う
        /// </summary>
        /// <param name="kingaku">端数処理対象金額</param>
        /// <param name="calcCD">端数CD</param>
        /// <returns name="decimal">端数処理後の金額</returns>
        private static decimal FractionCalc(decimal kingaku, short calcCD)
        {
            decimal returnVal = 0;      // 戻り値
            decimal sign = 1;
            if (kingaku < 0)
            {
                // 処理対象金額が負の値の場合
                sign = -1;
            }

            switch ((UIConstans.TAX_HASUU_CD)calcCD)
            {
                case UIConstans.TAX_HASUU_CD.CEILING:
                    returnVal = Math.Ceiling(Math.Abs(kingaku)) * sign;
                    break;
                case UIConstans.TAX_HASUU_CD.FLOOR:
                    returnVal = Math.Floor(Math.Abs(kingaku)) * sign;
                    break;
                case UIConstans.TAX_HASUU_CD.ROUND:
                    returnVal = Math.Round(Math.Abs(kingaku), 0, MidpointRounding.AwayFromZero) * sign;
                    break;
                default:
                    // 何もしない
                    returnVal = kingaku;
                    break;
            }

            return returnVal;
        }

        /// <summary>
        /// 月次処理対象か判定
        /// </summary>
        /// <param name="param">範囲条件情報</param>
        /// <returns></returns>
        private bool IsMonthlyTarget(MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo param)
        {
            // 掛元帳かつ伝票日付時のみ、月次処理データを取得して表示。
            if (param.TorihikiKBN == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 月次処理データから締内税額・締外税額データ作成
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="param"></param>
        /// <returns></returns>
        private List<DataRow> CreateMonthlyShimeRows(string torihikisakiCd, MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo param)
        {
            List<DataRow> rows = new List<DataRow>();

            if (!IsMonthlyTarget(param))
            {
                // 月次処理対象外ならデータは作成しない
                return rows;
            }

            // 取引先CDから月次処理データの各税額を取得
            var row = this.monthlyNewTable.AsEnumerable()
                                       .Where(n => n["TORIHIKISAKI_CD"].Equals(torihikisakiCd))
                                       .ToList();

            if (row.Count == 0)
            {
                // 該当取引先CDが存在しない場合、終了
                return rows;
            }

            decimal uchizeiGaku = 0m;
            decimal sotozeiGaku = 0m;

            if (!DBNull.Value.Equals(row[0]["SHIME_UTIZEI_GAKU"]))
            {
                uchizeiGaku = Convert.ToDecimal(row[0]["SHIME_UTIZEI_GAKU"]);
            }
            if (!DBNull.Value.Equals(row[0]["SHIME_SOTOZEI_GAKU"]))
            {
                sotozeiGaku = Convert.ToDecimal(row[0]["SHIME_SOTOZEI_GAKU"]);
            }

            M_TORIHIKISAKI entity = null;
            if (this.torihikisakiAllList != null)
            {
                entity = this.torihikisakiAllList.Where(n => n.TORIHIKISAKI_CD.Equals(torihikisakiCd))
                                                 .ToList()
                                                 .FirstOrDefault();
            }

            string torihikisakiName = string.Empty;
            if (entity != null)
            {
                torihikisakiName = entity.TORIHIKISAKI_NAME_RYAKU;
            }

            /* 現状、締内税額は使用されないので設定しない。
               使用する場合は、内税なので差引残高に計上させない処理も必要 */

            //// 締内税額
            //if (decimal.Zero != uchizeiGaku)
            //{
            //    DataRow uchizeiRow = this.meisaiTable.NewRow();
            //    uchizeiRow["HINMEI_NAME"] = "【精算毎消費税】";
            //    uchizeiRow["SHOUHIZEI"] = uchizeiGaku;
            //    uchizeiRow["MEISAI_DATE"] = day.Date.ToString();
            //    uchizeiRow["TORIHIKISAKI_CD"] = torihikisakiCd;
            //    uchizeiRow["TORIHIKISAKI_NAME"] = torihikisakiName;
            //    rows.Add(uchizeiRow);
            //}

            // 締外税額
            if (decimal.Zero != sotozeiGaku)
            {
                DataRow sotozeiRow = this.meisaiTable.NewRow();
                sotozeiRow["HINMEI_NAME"] = "【精算毎消費税】";
                sotozeiRow["SHOUHIZEI"] = sotozeiGaku;
                sotozeiRow["MEISAI_DATE"] = param.EndDay.Date.ToString();
                sotozeiRow["TORIHIKISAKI_CD"] = torihikisakiCd;
                sotozeiRow["TORIHIKISAKI_NAME"] = torihikisakiName;
                rows.Add(sotozeiRow);
            }

            return rows;
        }

        /// <summary>
        /// 月次処理データから消費税調整額データ作成
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="param"></param>
        /// <returns></returns>
        private DataRow CreateMonthlyTaxRows(string torihikisakiCd, MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo param)
        {
            if (!IsMonthlyTarget(param))
            {
                // 月次処理対象外ならデータは作成しない
                return null;
            }

            // 取引先CDから月次処理データの各税額を取得
            var monthlyRow = this.monthlyNewTable.AsEnumerable()
                                              .Where(n => n["TORIHIKISAKI_CD"].Equals(torihikisakiCd))
                                              .ToList();

            if (monthlyRow.Count == 0)
            {
                // 該当取引先CDが存在しない場合、終了
                return null;
            }

            decimal adjustTax = 0m;

            if (!DBNull.Value.Equals(monthlyRow[0]["ADJUST_TAX"]))
            {
                adjustTax = Convert.ToDecimal(monthlyRow[0]["ADJUST_TAX"]);
            }

            M_TORIHIKISAKI entity = null;
            if (this.torihikisakiAllList != null)
            {
                entity = this.torihikisakiAllList.Where(n => n.TORIHIKISAKI_CD.Equals(torihikisakiCd))
                                                 .ToList()
                                                 .FirstOrDefault();
            }

            string torihikisakiName = string.Empty;
            if (entity != null)
            {
                torihikisakiName = entity.TORIHIKISAKI_NAME_RYAKU;
            }

            // 消費税調整額
            if (decimal.Zero != adjustTax)
            {
                DataRow adjustRow = this.meisaiTable.NewRow();
                adjustRow["HINMEI_NAME"] = "【消費税調整額】";
                adjustRow["SHOUHIZEI"] = adjustTax;
                adjustRow["MEISAI_DATE"] = param.EndDay.Date.ToString();
                adjustRow["TORIHIKISAKI_CD"] = torihikisakiCd;
                adjustRow["TORIHIKISAKI_NAME"] = torihikisakiName;
                return adjustRow;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 指定された取引先CDの月次消費税調整行を作成
        /// </summary>
        /// <param name="param">範囲条件情報</param>
        /// <param name="torihikisakiCD">取引先CD</param>
        private void GetCreateAdjustTxtRow(MotochoHaniJokenPopUp.Const.UIConstans.ConditionInfo param)
        {
            if (IsMonthlyTarget(param)
                && this.adjustTaxTable != null && this.adjustTaxTable.Rows.Count > 0)
            {
                var dt = this.meisaiTable.Copy();
                int addRow = 0;

                for (int i = 0; i < this.adjustTaxTable.Rows.Count; i++)
                {
                    var row = this.adjustTaxTable.Rows[i];

                    for (int j = 0; j < this.meisaiTable.Rows.Count; j++)
                    {
                        var meisaiRow = this.meisaiTable.Rows[j];

                        if (row["TORIHIKISAKI_CD"].ToString() == meisaiRow["TORIHIKISAKI_CD"].ToString())
                        {
                            if (row["YEAR"] != null && !string.IsNullOrEmpty(row["YEAR"].ToString())
                                && row["MONTH"] != null && !string.IsNullOrEmpty(row["MONTH"].ToString()))
                            {
                                DateTime date = Convert.ToDateTime(row["YEAR"].ToString() + "/" + row["MONTH"].ToString() + "/01");
                                date = date.AddMonths(+1).AddDays(-1);

                                if (j != this.meisaiTable.Rows.Count - 1
                                    && DateTime.Compare(Convert.ToDateTime(this.meisaiTable.Rows[j + 1]["MEISAI_DATE"].ToString()), date) > 0
                                    || (j == this.meisaiTable.Rows.Count - 1
                                        && DateTime.Compare(Convert.ToDateTime(this.meisaiTable.Rows[j]["MEISAI_DATE"].ToString()), date) <= 0)
                                    || (j == this.meisaiTable.Select(string.Format("TORIHIKISAKI_CD LIKE '%{0}%'", meisaiRow["TORIHIKISAKI_CD"].ToString())).Length - 1
                                        && DateTime.Compare(Convert.ToDateTime(this.meisaiTable.Rows[j]["MEISAI_DATE"].ToString()), date) <= 0))
                                {
                                    // 指定された取引先CDの前月繰越残高行を作成
                                    DataRow zandakaRow = dt.NewRow();

                                    zandakaRow["MEISAI_DATE"] = date;
                                    zandakaRow["HINMEI_NAME"] = "月次消費税調整";

                                    if (row["ADJUST_TAX"] != null)
                                    {
                                        zandakaRow["SHOUHIZEI"] = row["ADJUST_TAX"];
                                    }
                                    // 取引マスタからCDと略名を取得
                                    M_TORIHIKISAKI entity = null;
                                    if (this.torihikisakiAllList != null)
                                    {
                                        entity = this.torihikisakiAllList.Where(n => n.TORIHIKISAKI_CD.Equals(row["TORIHIKISAKI_CD"]))
                                                                         .ToList()
                                                                         .FirstOrDefault();
                                    }

                                    if (entity != null)
                                    {
                                        zandakaRow["TORIHIKISAKI_CD"] = entity.TORIHIKISAKI_CD;
                                        zandakaRow["TORIHIKISAKI_NAME"] = entity.TORIHIKISAKI_NAME_RYAKU;
                                    }
                                    else
                                    {
                                        // 該当取引先が無い場合は、略名にブランクを格納する
                                        zandakaRow["TORIHIKISAKI_CD"] = row["TORIHIKISAKI_CD"];
                                        zandakaRow["TORIHIKISAKI_NAME"] = string.Empty;
                                    }

                                    dt.Rows.InsertAt(zandakaRow, j + 1 + addRow);
                                    addRow++;
                                    break;
                                }
                            }
                        }
                    }
                }

                this.meisaiTable = dt.Copy();
            }
            else
            {
                return;
            }
        }
        #endregion
        #endregion
    }
}
