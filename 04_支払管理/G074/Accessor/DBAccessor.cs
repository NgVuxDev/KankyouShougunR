using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Logic;

namespace Shougun.Core.PaymentManagement.KaikakekinItiranHyo.Accessor
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
        /// 取引先のDao
        /// </summary>
        internal IM_TORIHIKISAKIDao TorihikisakiDao;
        /// <summary>
        /// 買掛金一覧表のDao
        /// </summary>
        private Shougun.Core.PaymentManagement.KaikakekinItiranHyo.DAO.IT_KAKEKIN_ICHIRANDao KakekinDao;
        /// <summary>
        ///  明細用テーブル
        /// </summary>
        private DataTable meisaiTable;
        /// <summary>
        /// 自社情報
        /// </summary>
        private IM_CORP_INFODao mCorpInfoDao;
        /// <summary>
        ///  アプリケーション共有情報クラス
        /// </summary>
        private CommonInformation commonInfo;
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // フィールドの初期化
            this.TorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.KakekinDao = DaoInitUtility.GetComponent<Shougun.Core.PaymentManagement.KaikakekinItiranHyo.DAO.IT_KAKEKIN_ICHIRANDao>();
            this.mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
        }
        #endregion

        /// <summary>
        /// 明細用一覧データの取得
        /// </summary>
        /// <param name="param">範囲条件情報</param>
        /// <param name="info">アプリケーション共有情報</param>
        /// <returns name="DataTable">データテーブル</returns>
        /// <param name="form"></param>
        /// <param name="hadSearched"></param>
        internal DataTable GetIchiranData(Shougun.Core.Common.Kakepopup.Const.UIConstans.ConditionInfo param, CommonInformation info, Form form, ref bool hadSearched)
        {
            // 共有情報をセット
            this.commonInfo = info;

            ////明細用TableのClone作成
            this.meisaiTable = new DataTable();
            this.meisaiTable.Columns.Add("TORIHIKISAKI_CD");
            this.meisaiTable.Columns.Add("TORIHIKISAKI_NAME");
            this.meisaiTable.Columns.Add("KURIKOSHI_ZANDAKA", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("SHUKKIN_KINGAKU", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("SHIHARAI_KINGAKU", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("SHOHIZEI", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("ZEIKOMI_SHIHARAI", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("SASHIHIKI_ZANDAKA", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("SHIMEBI1", typeof(SqlInt16));
            this.meisaiTable.Columns.Add("SHIMEBI2", typeof(SqlInt16));
            this.meisaiTable.Columns.Add("SHIMEBI3", typeof(SqlInt16));

            // 月次締処理データ取得(this.meisaiTableと同じTableレイアウト)
            DataTable kikannaiTbl = this.KakekinDao.GetIchiranMonthlyData(param.StartTorihikisakiCD, param.EndTorihikisakiCD, param.StartDay.Year, param.StartDay.Month);

            var torihikisakiShiharaiDt = this.KakekinDao.GetTorihikisakiShiharaiList(param.StartTorihikisakiCD, param.EndTorihikisakiCD);

            if (kikannaiTbl.Rows.Count == 0)
            {
                // 該当年月の月次締処理データが存在しない場合は月次計算を行う

                // 別ユーザが月次処理中かチェック
                GetsujiShoriCheckLogicClass checkLogic = new GetsujiShoriCheckLogicClass();
                if (checkLogic.CheckGetsujiShoriChu())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E224", "実行");
                    return this.meisaiTable;
                }

                // 処理対象の取引先CDの絞込
                List<string> torihikisakiCdList = torihikisakiShiharaiDt.AsEnumerable().
                    Where(n =>
                        param.StartTorihikisakiCD.CompareTo(n["TORIHIKISAKI_CD"].ToString()) <= 0 &&
                        n["TORIHIKISAKI_CD"].ToString().CompareTo(param.EndTorihikisakiCD) <= 0
                        ).
                    Select(n => n["TORIHIKISAKI_CD"].ToString()).
                    ToList();
                // 20150715 取引先_支払をフィルターする Start
                List<M_TORIHIKISAKI_SHIHARAI> torihikisakiShiharaiList = torihikisakiShiharaiDt.AsEnumerable().
                    Where(n =>
                        param.StartTorihikisakiCD.CompareTo(n["TORIHIKISAKI_CD"].ToString()) <= 0 &&
                        n["TORIHIKISAKI_CD"].ToString().CompareTo(param.EndTorihikisakiCD) <= 0
                    ).
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

                GetsujiShoriLogic getsujiLogic = new GetsujiShoriLogic();
                // 20150715 取引先_支払キャッシュ作成 Start
                getsujiLogic.TorihikisakiShiharaiList.AddRange(torihikisakiShiharaiList);
                // 20150715 取引先_支払キャッシュ作成 End

                // 進行状況を表示させるためプログレスバーを指定
                var parentForm = (BusinessBaseForm)form.Parent;
                ToolStripProgressBar progresBar = parentForm.progresBar;
                progresBar.Minimum = 0;
                progresBar.Maximum = torihikisakiCdList.Count;
                progresBar.Value = 0;

                bool isDispAlert = true;
                int counter = 1;
                foreach (var torihikisakiCd in torihikisakiCdList)
                {
                    // 月次処理実行
                    var result = getsujiLogic.GetsujiShori(torihikisakiCd, param.StartDay.Year, param.StartDay.Month,
                        isDispAlert, 2, shoriType: GetsujiShoriLogic.GETSUJI_SHORI_TYPE.SHIHARAI);
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
                string yyyyMM = param.StartDay.ToString("yyyy/MM");
                if (getsujiLogic.MonthlyLockShDatas.ContainsKey(yyyyMM))
                {
                    var lockShEntityList = getsujiLogic.MonthlyLockShDatas[yyyyMM];

                    foreach (var lockShEntity in lockShEntityList)
                    {
                        var dt = torihikisakiShiharaiDt.AsEnumerable()
                            .Where(n => n["TORIHIKISAKI_CD"].Equals(lockShEntity.TORIHIKISAKI_CD))
                            .FirstOrDefault();
                        if (dt == null)
                        {
                            continue;
                        }

                        var row = kikannaiTbl.NewRow();
                        row["TORIHIKISAKI_CD"] = lockShEntity.TORIHIKISAKI_CD;
                        row["KURIKOSHI_ZANDAKA"] = lockShEntity.PREVIOUS_MONTH_BALANCE.Value;
                        row["SHUKKIN_KINGAKU"] = lockShEntity.SHUKKIN_KINGAKU.Value;
                        row["SHIHARAI_KINGAKU"] = lockShEntity.KINGAKU.Value;
                        // 計算直後は月次調整データは作成されないので、消費税、税込金額、調整前差引残高をそのまま表示
                        row["SHOHIZEI"] = lockShEntity.TAX.Value;
                        row["ZEIKOMI_SHIHARAI"] = lockShEntity.TOTAL_KINGAKU.Value;
                        row["SASHIHIKI_ZANDAKA"] = lockShEntity.ZANDAKA.Value;

                        // 取引先マスタ関連の設定
                        row["TORIHIKISAKI_NAME"] = dt["TORIHIKISAKI_NAME_RYAKU"];
                        row["SHIMEBI1"] = dt["SHIMEBI1"];
                        row["SHIMEBI2"] = dt["SHIMEBI2"];
                        row["SHIMEBI3"] = dt["SHIMEBI3"];

                        // 明細用Tableの追加
                        kikannaiTbl.Rows.Add(row);
                    }
                }
            }

            foreach (DataRow row in kikannaiTbl.Rows)
            {
                // 取引区分が現金かつ、全て0円のデータを除外したい場合は下記コメントアウトを解除

                //var isGenkin = torihikisakiShiharaiDt.AsEnumerable()
                //                                     .Any(n => n["TORIHIKISAKI_CD"].Equals(row["TORIHIKISAKI_CD"])
                //                                            && n["TORIHIKI_KBN_CD"].ToString().Equals("1"));
                //// 取引区分が現金かつ、全て0円データの場合は除外とする。
                //if (isGenkin && zero)

                // 全て0円データの場合は除外とする。
                var zero = AllZeroRecord(row);
                if (zero)
                {
                    continue;
                }

                this.meisaiTable.ImportRow(row);
            }

            // 検索完了用のフラグを立てる
            hadSearched = true;

            return this.meisaiTable;
        }

        /// <summary>
        /// 自社情報マスタテーブル会社名SELECT
        /// </summary>
        /// <returns></returns>
        internal String SelectCorpName()
        {
            M_CORP_INFO[] corpInfo;

            corpInfo = (M_CORP_INFO[])mCorpInfoDao.GetAllData();
            return corpInfo[0].CORP_RYAKU_NAME;
        }

        #region private
        /// <summary>
        /// 金額系カラムが全て0円か判定
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool AllZeroRecord(DataRow row)
        {
            if (row == null)
            {
                return true;
            }

            // 繰越残高～差引残高が全て0円か判定
            if (row["KURIKOSHI_ZANDAKA"] != DBNull.Value && Convert.ToDecimal(row["KURIKOSHI_ZANDAKA"]) == decimal.Zero
                && row["SHUKKIN_KINGAKU"] != DBNull.Value && Convert.ToDecimal(row["SHUKKIN_KINGAKU"]) == decimal.Zero
                && row["SHIHARAI_KINGAKU"] != DBNull.Value && Convert.ToDecimal(row["SHIHARAI_KINGAKU"]) == decimal.Zero
                && row["SHOHIZEI"] != DBNull.Value && Convert.ToDecimal(row["SHOHIZEI"]) == decimal.Zero
                && row["ZEIKOMI_SHIHARAI"] != DBNull.Value && Convert.ToDecimal(row["ZEIKOMI_SHIHARAI"]) == decimal.Zero
                && row["SASHIHIKI_ZANDAKA"] != DBNull.Value && Convert.ToDecimal(row["SASHIHIKI_ZANDAKA"]) == decimal.Zero)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
