using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.OriginalException;

namespace r_framework.Utility
{
    /// <summary>
    /// ソート実行クラス
    /// </summary>
    public static class SortUtility
    {
        /// <summary>
        /// ソート条件の格納リスト
        /// </summary>
        private static List<SortConditionDto> sortSettingList;

        /// <summary>
        /// データグリッドビュー
        /// </summary>
        public static CustomDataGridView customDataGridView { get; set; }

        /// <summary>
        /// ソート条件を設定しているセル情報
        /// </summary>
        public static CellCollection Cells;

        private static SortOrder[] sortOrderList = new SortOrder[] { SortOrder.None, SortOrder.Ascending, SortOrder.Descending };

        /// <summary>
        /// ソート実行メソッド
        /// </summary>
        public static SortItem[] DoSort()
        {
            SettingExtraction();
            SortToSetting();
            return SortItemCreate();
        }

        /// <summary>
        /// ソート条件の抽出
        /// </summary>
        public static void SettingExtraction()
        {
            // ソート用情報格納領域確保
            //クラス化してListに
            sortSettingList = new List<SortConditionDto>();

            var count = 0;
            for (var i = 0; i < Cells.Count - 2; i += 2)
            {
                count++;
                //ヘッダーセクションより取得した情報を各要素へ変換
                var combBoxCell = (GcCustomSortComboBox)Cells[i];
                var textBoxCell = (TextBoxCell)Cells[i + 1];

                //項目存在チェック
                if (combBoxCell.Value == null)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(Convert.ToString(textBoxCell.Value)))
                {
                    continue;
                }

                //ソート条件の格納
                var cell = new SortConditionDto
                {
                    sortNo = Convert.ToInt32(textBoxCell.Value),
                    sortRowNo = count,
                    sortOrder = combBoxCell.GetSortOrder()
                };

                sortSettingList.Add(cell);
            }
        }

        /// <summary>
        /// ソート条件を指定順に並び替え
        /// </summary>
        public static void SortToSetting()
        {
            // ソートを行う順に並び替え
            sortSettingList.Sort(Comparison);
        }

        /// <summary>
        /// ソート実施用のアイテムを生成
        /// </summary>
        public static SortItem[] SortItemCreate()
        {
            // ソートを行うためのItemを生成
            var sortItemList = new SortItem[sortSettingList.Count];

            for (var i = 0; i < sortSettingList.Count; i++)
            {
                sortItemList[i] = new SortItem(sortSettingList[i].sortRowNo, sortSettingList[i].sortOrder);
            }

            return sortItemList;
        }

        /// <summary>
        /// 比較用メソッド
        /// </summary>
        public static int Comparison(SortConditionDto condition1, SortConditionDto condition2)
        {
            return condition1.sortNo.CompareTo(condition2.sortNo);
        }

        /// <summary>
        /// ソート条件の取得
        /// </summary>
        /// <param name="order">ソートオーダー</param>
        /// <returns>ASC or DESC</returns>
        public static string GetSortOrder(SortOrder order)
        {
            switch (order)
            {
                case SortOrder.Ascending:
                    return " ASC";
                case SortOrder.Descending:
                    return " DESC";
            }

            //throw new EdisonException();
            return "";
        }

    }
}
