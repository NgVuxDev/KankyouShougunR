using System;
using System.Collections.Generic;
using System.Linq;
using r_framework.Entity;

namespace Shougun.Core.SalesPayment.ShiharaiJunihyo
{
    /// <summary>
    /// パターンDTOクラス
    /// </summary>
    public class PatternDto
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public PatternDto()
        {
            this.Pattern = new M_LIST_PATTERN();
            this.PatternColumnList = new List<M_LIST_PATTERN_COLUMN>();
            this.ColumnSelectList = new List<S_LIST_COLUMN_SELECT>();
            this.ColumnSelectDetailList = new List<S_LIST_COLUMN_SELECT_DETAIL>();
        }

        /// <summary>
        /// パターンを取得・設定します
        /// </summary>
        public M_LIST_PATTERN Pattern { get; set; }

        /// <summary>
        /// パターン詳細を取得・設定します
        /// </summary>
        public List<M_LIST_PATTERN_COLUMN> PatternColumnList { get; set; }

        /// <summary>
        /// 一覧表項目選択リストを取得・設定します
        /// </summary>
        public List<S_LIST_COLUMN_SELECT> ColumnSelectList { get; set; }

        /// <summary>
        /// 一覧表項目選択詳細リストを取得・設定します
        /// </summary>
        public List<S_LIST_COLUMN_SELECT_DETAIL> ColumnSelectDetailList { get; set; }

        /// <summary>
        /// パターン名を取得します
        /// </summary>
        public String PATTERN_NAME
        {
            get { return this.Pattern.PATTERN_NAME; }
        }

        /// <summary>
        /// パターン詳細を取得します
        /// </summary>
        /// <param name="shuukeiKoumokuNo">集計項目番号</param>
        /// <returns>パターン詳細</returns>
        public M_LIST_PATTERN_COLUMN GetPatternColumn(int shuukeiKoumokuNo)
        {
            // データが無い場合のデフォルト値を設定しておく
            var ret = new M_LIST_PATTERN_COLUMN() { DETAIL_KBN = false };

            if (this.PatternColumnList.Count() >= shuukeiKoumokuNo)
            {
                ret = this.PatternColumnList[shuukeiKoumokuNo - 1];
            }

            return ret;
        }

        /// <summary>
        /// 一覧表項目選択を取得します
        /// </summary>
        /// <param name="shuukeiKoumokuNo">集計項目番号</param>
        /// <returns>一覧表項目選択</returns>
        public S_LIST_COLUMN_SELECT GetColumnSelect(int shuukeiKoumokuNo)
        {
            S_LIST_COLUMN_SELECT ret = null;

            if (this.ColumnSelectList.Count() >= shuukeiKoumokuNo)
            {
                ret = this.ColumnSelectList[shuukeiKoumokuNo - 1];
            }

            return ret;
        }

        /// <summary>
        /// 一覧表項目選択詳細を取得します
        /// </summary>
        /// <param name="shuukeiKoumokuNo">集計項目番号</param>
        /// <returns>一覧表項目選択詳細</returns>
        public S_LIST_COLUMN_SELECT_DETAIL GetColumnSelectDetail(int shuukeiKoumokuNo)
        {
            S_LIST_COLUMN_SELECT_DETAIL ret = null;

            if (this.ColumnSelectDetailList.Count() >= shuukeiKoumokuNo)
            {
                ret = this.ColumnSelectDetailList[shuukeiKoumokuNo - 1];
            }

            return ret;
        }

        /// <summary>
        /// 集計対象フラグを取得します
        /// </summary>
        /// <param name="shuukeiKoumokuNo">集計項目番号</param>
        /// <returns>集計対象フラグ</returns>
        public bool GetShuukeiFlag(int shuukeiKoumokuNo)
        {
            var ret = false;

            var patternColumn = this.GetPatternColumn(shuukeiKoumokuNo);
            if (patternColumn != null && patternColumn.DETAIL_KBN.IsNull == false)
            {
                ret = patternColumn.DETAIL_KBN.Value;
            }

            return ret;
        }
    }
}
