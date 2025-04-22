using System.Collections.Generic;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.Accessor
{
    /// <summary>
    /// パターン関連DBアクセス
    /// </summary>
    internal class PatternDbAccessor
    {
        #region フィールド

        /// <summary>
        /// 汎用CSV出力パターンDAO
        /// </summary>
        private IM_OUTPUT_CSV_PATTERNDao csvPatternDao;

        /// <summary>
        /// 汎用CSV出力パターン詳細DAO
        /// </summary>
        private IM_OUTPUT_CSV_PATTERN_COLUMNDao csvPatternColumnDao;

        /// <summary>
        /// 汎用CSV出力パターン販売管理DAO
        /// </summary>
        private IM_OUTPUT_CSV_PATTERN_HANBAIKANRIDao csvPatternHanbaikanriDao;

        /// <summary>
        /// 汎用CSV出力パターン入出金DAO
        /// </summary>
        private IM_OUTPUT_CSV_PATTERN_NYUUSHUKKINDao csvPatternNyuushukkinDao;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PatternDbAccessor()
        {
            // 各DAO初期化
            this.csvPatternDao = DaoInitUtility.GetComponent<IM_OUTPUT_CSV_PATTERNDao>();
            this.csvPatternColumnDao = DaoInitUtility.GetComponent<IM_OUTPUT_CSV_PATTERN_COLUMNDao>();
            this.csvPatternHanbaikanriDao = DaoInitUtility.GetComponent<IM_OUTPUT_CSV_PATTERN_HANBAIKANRIDao>();
            this.csvPatternNyuushukkinDao = DaoInitUtility.GetComponent<IM_OUTPUT_CSV_PATTERN_NYUUSHUKKINDao>();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// パターン検索
        /// </summary>
        /// <param name="haniKbn">出力範囲区分</param>
        /// <param name="mCsvPattern">条件パターンデータ</param>
        /// <returns>検索結果(パターンデータ1件)</returns>
        /// <remarks>
        /// 範囲区分とSYSTEM_ID、SEQを指定し、単一パターンデータを取得する。
        /// </remarks>
        internal M_OUTPUT_CSV_PATTERN GetCsvPattern(int haniKbn, M_OUTPUT_CSV_PATTERN mCsvPattern)
        {
            LogUtility.DebugMethodStart(haniKbn, mCsvPattern);

            M_OUTPUT_CSV_PATTERN ret = null;
            M_OUTPUT_CSV_PATTERN cond = new M_OUTPUT_CSV_PATTERN()
            {
                SYSTEM_ID = mCsvPattern.SYSTEM_ID,
                SEQ = mCsvPattern.SEQ,
                KBN = string.Format("{0:000}", haniKbn)
            };
            ret = this.csvPatternDao.GetDataForEntity(cond);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターン検索
        /// </summary>
        /// <param name="mCsvPattern">条件パターンデータ</param>
        /// <returns>検索結果(パターンデータ1件)</returns>
        /// <remarks>
        /// SYSTEM_IDとSEQを指定し、単一パターンデータを取得する。
        /// </remarks>
        internal M_OUTPUT_CSV_PATTERN GetCsvPattern(M_OUTPUT_CSV_PATTERN mCsvPattern)
        {
            LogUtility.DebugMethodStart(mCsvPattern);

            M_OUTPUT_CSV_PATTERN ret = null;
            M_OUTPUT_CSV_PATTERN cond = new M_OUTPUT_CSV_PATTERN()
            {
                SYSTEM_ID = mCsvPattern.SYSTEM_ID,
                SEQ = mCsvPattern.SEQ
            };
            ret = this.csvPatternDao.GetDataForEntity(cond);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターン検索
        /// </summary>
        /// <param name="haniKbn">出力範囲区分</param>
        /// <returns>検索結果(パターンデータ配列)</returns>
        /// <remarks>
        /// 範囲区分を指定し、全パターンデータを取得する。
        /// </remarks>
        internal List<M_OUTPUT_CSV_PATTERN> GetCsvPatterns(int haniKbn)
        {
            LogUtility.DebugMethodStart(haniKbn);

            List<M_OUTPUT_CSV_PATTERN> ret = null;
            M_OUTPUT_CSV_PATTERN cond = new M_OUTPUT_CSV_PATTERN()
            {
                KBN = string.Format("{0:000}", haniKbn),
                DELETE_FLG = false
            };
            ret = this.csvPatternDao.GetDataForEntities(cond);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターン明細検索
        /// </summary>
        /// <param name="mCsvPattern">条件パターンデータ</param>
        /// <returns>検索結果(パターン明細データ配列)</returns>
        /// <remarks>
        /// SYSTEM_IDとSEQを指定し、パターン明細データを取得する。
        /// </remarks>
        internal List<M_OUTPUT_CSV_PATTERN_COLUMN> GetCsvPatternColumns(M_OUTPUT_CSV_PATTERN mCsvPattern)
        {
            LogUtility.DebugMethodStart(mCsvPattern);

            List<M_OUTPUT_CSV_PATTERN_COLUMN> ret = null;
            M_OUTPUT_CSV_PATTERN_COLUMN cond = new M_OUTPUT_CSV_PATTERN_COLUMN()
            {
                SYSTEM_ID = mCsvPattern.SYSTEM_ID,
                SEQ = mCsvPattern.SEQ
            };
            ret = this.csvPatternColumnDao.GetDataForEntities(cond);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターン販売管理検索
        /// </summary>
        /// <param name="mCsvPattern">条件パターンデータ</param>
        /// <returns>検索結果(パターン販売管理付加データ1件)</returns>
        /// <remarks>
        /// SYSTEM_IDとSEQを指定し、単一パターンデータ対するパターン販売管理付加データを取得する。
        /// </remarks>
        internal M_OUTPUT_CSV_PATTERN_HANBAIKANRI GetCsvPatternHanbaikanri(M_OUTPUT_CSV_PATTERN mCsvPattern)
        {
            LogUtility.DebugMethodStart(mCsvPattern);

            M_OUTPUT_CSV_PATTERN_HANBAIKANRI ret = null;
            M_OUTPUT_CSV_PATTERN_HANBAIKANRI cond = new M_OUTPUT_CSV_PATTERN_HANBAIKANRI()
            {
                SYSTEM_ID = mCsvPattern.SYSTEM_ID,
                SEQ = mCsvPattern.SEQ
            };
            ret = this.csvPatternHanbaikanriDao.GetDataForEntity(cond);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターン入出金検索
        /// </summary>
        /// <param name="mCsvPattern">条件パターンデータ</param>
        /// <returns>検索結果(パターン入出金付加データ1件)</returns>
        /// <remarks>
        /// SYSTEM_IDとSEQを指定し、単一パターンデータ対するパターン入出金付加データを取得する。
        /// </remarks>
        internal M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN GetCsvPatternNyuushukkin(M_OUTPUT_CSV_PATTERN mCsvPattern)
        {
            LogUtility.DebugMethodStart(mCsvPattern);

            M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN ret = null;
            M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN cond = new M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN()
            {
                SYSTEM_ID = mCsvPattern.SYSTEM_ID,
                SEQ = mCsvPattern.SEQ
            };
            ret = this.csvPatternNyuushukkinDao.GetDataForEntity(cond);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターン登録
        /// </summary>
        /// <param name="mCsvPattern">対象パターンデータ</param>
        /// <returns>登録件数</returns>
        internal int InsertCsvPattern(M_OUTPUT_CSV_PATTERN mCsvPattern)
        {
            LogUtility.DebugMethodStart(mCsvPattern);
            int ret = 0;

            ret = this.csvPatternDao.Insert(mCsvPattern);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターン明細登録
        /// </summary>
        /// <param name="mCsvPatternColumns">対象パターン明細データ</param>
        /// <returns>登録総件数</returns>
        internal int InsertCsvPatternColumns(List<M_OUTPUT_CSV_PATTERN_COLUMN> mCsvPatternColumns)
        {
            LogUtility.DebugMethodStart(mCsvPatternColumns);
            int ret = 0;

            foreach (var mCsvPatternColumn in mCsvPatternColumns)
                ret += this.csvPatternColumnDao.Insert(mCsvPatternColumn);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターン販売管理登録
        /// </summary>
        /// <param name="mCsvPatternHanbaikanri">対象パターン販売管理データ</param>
        /// <returns>登録件数</returns>
        internal int InsertCsvPatternHanbaikanri(M_OUTPUT_CSV_PATTERN_HANBAIKANRI mCsvPatternHanbaikanri)
        {
            LogUtility.DebugMethodStart(mCsvPatternHanbaikanri);
            int ret = 0;

            ret = this.csvPatternHanbaikanriDao.Insert(mCsvPatternHanbaikanri);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターン入出金登録
        /// </summary>
        /// <param name="mCsvPatternNyuushukkin">対象パターン入出金データ</param>
        /// <returns>登録件数</returns>
        internal int InsertCsvPatternNyuushukkin(M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN mCsvPatternNyuushukkin)
        {
            LogUtility.DebugMethodStart(mCsvPatternNyuushukkin);
            int ret = 0;

            ret = this.csvPatternNyuushukkinDao.Insert(mCsvPatternNyuushukkin);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターン更新
        /// </summary>
        /// <param name="mCsvPattern">対象パターンデータ</param>
        /// <returns>更新件数</returns>
        internal int UpdateCsvPattern(M_OUTPUT_CSV_PATTERN mCsvPattern)
        {
            LogUtility.DebugMethodStart(mCsvPattern);
            int ret = 0;

            ret = this.csvPatternDao.Update(mCsvPattern);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion
    }
}