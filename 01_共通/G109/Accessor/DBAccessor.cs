using System;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill.Attrs;

namespace Shougun.Core.SalesManagement.UrikakekinItiranHyo.Accessor
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
        private IM_TORIHIKISAKIDao TorihikisakiDao;
        /// <summary>
        /// 取引区分のDao
        /// </summary>
        private IM_TORIHIKI_KBNDao TorihikiKBNDao;
        /// <summary>
        /// 取引先請求情報のDao
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao TorihikiSeikyuDao;
        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao GyoushaDao;
        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao GenbaDao;
        /// <summary>
        /// 品名のDao
        /// </summary>
        private IM_HINMEIDao HinmeiDao;
        /// <summary>
        /// 消費税のDao
        /// </summary>
        private IM_SHOUHIZEIDao ShouhizeiDao;
        /// <summary>
        /// 売掛金一覧表のDao
        /// </summary>
        private Shougun.Core.SalesManagement.UrikakekinItiranHyo.DAO.IT_KAKEKIN_ICHIRANDao KakekinDao;
        /// <summary>
        ///  取引先毎の請求毎税一覧
        /// </summary>
        private DataTable MaiZeiIchiran;
        /// <summary>
        ///  取引先CDのリスト
        /// </summary>
        private M_TORIHIKISAKI[] torihikiList;
        /// <summary>
        ///  明細用テーブル
        /// </summary>
        private DataTable meisaiTable;
        /// <summary>
        /// 自社情報
        /// </summary>
        private IM_CORP_INFODao mCorpInfoDao;

        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // フィールドの初期化
            this.TorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.TorihikiKBNDao = DaoInitUtility.GetComponent<IM_TORIHIKI_KBNDao>();
            this.GyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.GenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.HinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.KakekinDao = DaoInitUtility.GetComponent<Shougun.Core.SalesManagement.UrikakekinItiranHyo.DAO.IT_KAKEKIN_ICHIRANDao>();
            this.TorihikiSeikyuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.ShouhizeiDao = DaoInitUtility.GetComponent<IM_SHOUHIZEIDao>();
            this.mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
        }
        #endregion

        /// <summary>
        /// 明細用一覧データの取得
        /// </summary>
        /// <param name="param">範囲条件情報</param>
        /// <returns name="DataTable">データテーブル</returns>
        /// <remarks>
        /// ※データ抽出モデル
        ///		┌───────┐
        ///		│ 前月繰越残高 │
        ///		├───────┤
        ///		│　期間内売上　│
        ///		├───────┤
        ///		│　期間内入金　│
        ///		├───────┤
        ///		│ 期間内消費税 │
        ///		└───────┘
        /// </remarks>
        internal DataTable GetIchiranData(Shougun.Core.Common.Kakepopup.Const.UIConstans.ConditionInfo param)
        {
            // Dateから日付を文字列にて取得
            string sDay = param.StartDay.Date.ToString();
            string eDay = param.EndDay.Date.ToString();

            // 期間内売上/入金データテーブルの取得
            DataTable kikannaiTbl = this.KakekinDao.GetIchiranData(param.StartTorihikisakiCD, param.EndTorihikisakiCD, sDay, eDay);

             ////明細用TableのClone作成
            this.meisaiTable = new DataTable();
            this.meisaiTable.Columns.Add("TORIHIKISAKI_CD");
            this.meisaiTable.Columns.Add("TORIHIKISAKI_NAME");
            this.meisaiTable.Columns.Add("KURIKOSHI_ZANDAKA", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("NYUKINGAKU", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("ZEINUKI_URIAGE", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("SHOHIZEI", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("ZEIKOMI_URIAGE", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("SASHIHIKI_URIAGE_ZANDAKA", Type.GetType("System.Decimal"));
            this.meisaiTable.Columns.Add("SHIMEBI1", typeof(SqlInt16));
            this.meisaiTable.Columns.Add("SHIMEBI2", typeof(SqlInt16));
            this.meisaiTable.Columns.Add("SHIMEBI3", typeof(SqlInt16));

            // 取引先CDリストを作成
            this.torihikiList = this.KakekinDao.GetTorihikisakiList(param.StartTorihikisakiCD, param.EndTorihikisakiCD);

            if (kikannaiTbl.Rows.Count != 0)
            {
                int index = 0;
                int ichiranflag = 0;
                int shimebiflag = 0;

                string torihikicd = string.Empty;
                string torihikiname = string.Empty;
                SqlInt16 shimebi1 = 0;
                SqlInt16 shimebi2 = 0;
                SqlInt16 shimebi3 = 0;
                decimal nyukinkingaku = 0;
                decimal zeinukiuriage = 0;
                decimal shouhizei = 0;
                decimal kurikoshizandaka = 0;

                string denshu = string.Empty;
                string systemid = string.Empty;
                string seq = string.Empty;

				while(index < this.torihikiList.Length)
				{
                    foreach (DataRow row in kikannaiTbl.Rows)
                    {
                        if ((this.torihikiList[index].TORIHIKISAKI_CD == row["TORIHIKISAKI_CD"].ToString()))
                        {
                            if (ichiranflag == 0)
                            {
                                torihikicd = row["TORIHIKISAKI_CD"].ToString();
                                torihikiname = row["TORIHIKISAKI_NAME"].ToString();
                                kurikoshizandaka = this.GetZengetsuZandaka(this.torihikiList[index].TORIHIKISAKI_CD, param.StartDay);

                                // 取引先請求情報から締日を取得
                                M_TORIHIKISAKI_SEIKYUU entity = this.TorihikiSeikyuDao.GetDataByCd(this.torihikiList[index].TORIHIKISAKI_CD);
                                if (entity != null)
                                {
                                    shimebi1 = entity.SHIMEBI1;
                                    shimebi2 = entity.SHIMEBI2;
                                    shimebi3 = entity.SHIMEBI3;
                                }
                                else
                                {
                                    shimebiflag = 1;
                                }
                                ichiranflag = 1;
                            }
                            if (row["NYUUKIN_KINGAKU"].ToString() != "")
                            {
                                nyukinkingaku += (decimal)(row["NYUUKIN_KINGAKU"]);
                            }
                            if (row["URIAGE_KINGAKU"].ToString() != "")
                            {
                                zeinukiuriage += (decimal)(row["URIAGE_KINGAKU"]);
                            }
                            if (row["SHOUHIZEI"].ToString() != "")
                            {
                                shouhizei += (decimal)(row["SHOUHIZEI"]);
                            }
                            // 伝種区分、伝票毎税　伝票が変わったときだけセットする
                            if (!denshu.Equals(row["DENSHU_KBN"].ToString()) || !systemid.Equals(row["SYSTEM_ID"].ToString()) || !seq.Equals(row["SEQ"].ToString()))
                            {
                                if (row["DENPYOU_MAI_ZEI"].ToString() != "")
                                {
                                    shouhizei += (decimal)(row["DENPYOU_MAI_ZEI"]);
                                }
                            }
                        }
                        else
                        {

                        }

                        denshu = row["DENSHU_KBN"].ToString();
                        systemid = row["SYSTEM_ID"].ToString();
                        seq = row["SEQ"].ToString();

                    }
                    if (ichiranflag == 1)
                    {
                        // 期間内請求伝票消費税データの取得
                        DataTable kikannaiSeikyuuTax = this.KakekinDao.GetSeikyuugotoTax(this.torihikiList[index].TORIHIKISAKI_CD, sDay, eDay);
                        shouhizei += (decimal)kikannaiSeikyuuTax.Rows[0][0];

                        DataRow kakekinRow = this.meisaiTable.NewRow();
                        kakekinRow["TORIHIKISAKI_CD"] = torihikicd;
                        kakekinRow["TORIHIKISAKI_NAME"] = torihikiname;
                        kakekinRow["KURIKOSHI_ZANDAKA"] = kurikoshizandaka;
                        kakekinRow["NYUKINGAKU"] = nyukinkingaku;
                        kakekinRow["ZEINUKI_URIAGE"] = zeinukiuriage;
                        kakekinRow["SHOHIZEI"] = shouhizei;
                        kakekinRow["ZEIKOMI_URIAGE"] = zeinukiuriage + shouhizei;
                        kakekinRow["SASHIHIKI_URIAGE_ZANDAKA"] = kurikoshizandaka + zeinukiuriage + shouhizei - nyukinkingaku;

                        // 該当取引先請求情報が無い場合は、締日にブランクを格納する
                        if (shimebiflag == 0)
                        {
                            kakekinRow["SHIMEBI1"] = shimebi1;
                            kakekinRow["SHIMEBI2"] = shimebi2;
                            kakekinRow["SHIMEBI3"] = shimebi3;
                        }
                        // 明細用Tableの追加
                        this.meisaiTable.Rows.Add(kakekinRow);

                        nyukinkingaku = 0;
                        zeinukiuriage = 0;
                        shouhizei = 0;
                        kurikoshizandaka = 0;
                        ichiranflag = 0;
                        shimebiflag = 0;
                        torihikicd = string.Empty;
                        torihikiname = string.Empty;
                        shimebi1 = 0;
                        shimebi2 = 0;
                        shimebi3 = 0;
                    }
                    // 次の取引先へ
                    index++;
                }
            }
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
            return corpInfo[0].CORP_NAME;
        }

        #region private
        /// <summary>
        /// 指定された取引先CDの前月繰越残高を取得
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <param name="startDay">開始伝票日付</param>
        /// <returns name="decimal">前月繰越残高</returns>
        private decimal GetZengetsuZandaka(string torihikisakiCD, DateTime startDay)
        {
            decimal zandaka = 0;
            DateTime workDate;

            // Dateから日付を文字列にて取得
            string sDay = startDay.Date.ToString();

            // 指定された取引先CDの開始伝票日付より直近の請求データから請求差引残高、締実行日を抽出
            DataTable table = this.KakekinDao.GetRecentSeikyuuZandaka(torihikisakiCD, sDay);

            // 請求差引残高を基に繰越残高取得
            if (table.Rows.Count != 0)
            {
                // 請求差引残高取得
                // ※直近の請求データのため、該当する取引先は単一
                decimal seikyuuZandaka = decimal.Parse(table.Rows[0]["SEIKYUU_ZANDAKA"].ToString());

                // 締実行日を取得
                DateTime execDate = DateTime.Parse(table.Rows[0]["SHIME_EXEC_DATE"].ToString());

                // 締実行日翌日～開始伝票日付間の売上/入金データテーブルの取得(単一取引先CD)
                sDay = startDay.AddDays(-1).ToString();     // 開始日-1まで
                workDate = execDate.AddDays(1);
                DataTable uriageTbl = this.KakekinDao.GetIchiranData(torihikisakiCD, torihikisakiCD, workDate.Date.ToString(), sDay);

                //string oldDenNum = string.Empty;
                //if (0 != uriageTbl.Rows.Count)
                //{
                //    // 比較値をセット
                //    oldDenNum = uriageTbl.Rows[0]["DENPYOU_NUMBER"].ToString();
                //}

                // 比較値
                string denshu = string.Empty;
                string systemid = string.Empty;
                string seq = string.Empty;

                // 各金額を積算
                decimal uriKin = 0;
                decimal meiZei = 0;
                decimal denZei = 0;
                decimal nyuKin = 0;
                foreach (DataRow row in uriageTbl.Rows)
                {
                    if ("10" != row["DENSHU_KBN"].ToString())
                    {
                        // 売上金額を積算
                        uriKin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());

                        // 明細毎税を積算
                        meiZei += decimal.Parse(row["SHOUHIZEI"].ToString());

                        // 伝種区分、伝票毎税　伝票が変わったときだけセットする
                        if (!denshu.Equals(row["DENSHU_KBN"].ToString()) || !systemid.Equals(row["SYSTEM_ID"].ToString()) || !seq.Equals(row["SEQ"].ToString()))
                        {
                            denZei += decimal.Parse(row["DENPYOU_MAI_ZEI"].ToString());
                        }
                    }
                    else
                    {
                        if (!(row["NYUUKIN_KINGAKU"] is DBNull))
                        {
                            nyuKin += decimal.Parse(row["NYUUKIN_KINGAKU"].ToString());
                        }
                    }
                    denshu = row["DENSHU_KBN"].ToString();
                    systemid = row["SYSTEM_ID"].ToString();
                    seq = row["SEQ"].ToString();

                }

                // 開始伝票日付前日の消費税率を取得
                decimal rate = 0;
                workDate = startDay.AddDays(-1);
                M_SHOUHIZEI[] entityList = this.ShouhizeiDao.GetAllData();
                foreach (M_SHOUHIZEI entity in entityList)
                {
                    if ((entity.TEKIYOU_BEGIN.Value.Date <= workDate.Date) && (entity.TEKIYOU_END.Value.Date >= workDate.Date))
                    {
                        // 消費税率を取得
                        rate = entity.SHOUHIZEI_RATE.Value;
                    }
                }

                // 請求毎税を算出
                // (金額の合計＊開始日付の前日の税率)
                decimal seiKin = 0;
                seiKin = uriKin * (rate / 100);
                //seiKin = uriKin * ((rate / 100) + 1);

                // 繰越残高を算出
                // ((請求差引残高＋売上金額＋明細毎税＋伝票毎税＋請求毎税)‐入金額)
                zandaka = (seikyuuZandaka + uriKin + meiZei + denZei + seiKin) - nyuKin;
            }
            return zandaka;
        }
        #endregion
    }
}
