// $Id: SearchSettingHelper.cs.cs 30587 2014-09-19 10:56:19Z sanbongi $
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace r_framework.CustomControl.DataGridCustomControl
{
    public class CustomSearchColumn
    {
        public string Name;
        public string HeaderText;
        public string Filter;
        public Type ItemType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CustomSearchColumn()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="HeaderText"></param>
        /// <param name="Filter"></param>
        /// <param name="ItemType"></param>
        public CustomSearchColumn(string Name, string HeaderText, string Filter, Type ItemType)
        {
            this.Name = Name;
            this.HeaderText = HeaderText;
            this.Filter = Filter;
            this.ItemType = ItemType;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="source"></param>
        public CustomSearchColumn(CustomSearchColumn source)
        {
            this.Name = source.Name;
            this.HeaderText = source.HeaderText;
            this.Filter = source.Filter;
            this.ItemType = source.ItemType;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}", Name, HeaderText, Filter);
        }
    }

    public class SearchSettingInfo
    {
        /// <summary>ID</summary>
        /// <remarks>「呼出元のForm名」.「ユーザコントロールのName」の形式</remarks>
        public string Id { get; private set; }
        /// <summary>フィルタのキャプション</summary>
        public string SearchSettingCaption { get; private set; }
        /// <summary>画面の一覧で抽出条件に指定されたリスト</summary>
        public List<CustomSearchColumn> SearchColumns { get; private set; }
        /// <summary>画面の一覧に表示されている抽出候補のリスト</summary>
        public List<CustomSearchColumn> ViewColumns { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id"></param>
        public SearchSettingInfo(string id)
        {
            this.Id = id;
            this.SearchSettingCaption = "";
            this.SearchColumns = new List<CustomSearchColumn>();
            this.ViewColumns = new List<CustomSearchColumn>();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="source"></param>
        public SearchSettingInfo(SearchSettingInfo source)
        {
            Copy(source);
        }

        /// <summary>
        /// コピー
        /// </summary>
        /// <param name="source"></param>
        public void Copy(SearchSettingInfo source)
        {
            this.Id = source.Id;
            this.SearchSettingCaption = source.SearchSettingCaption;

            this.SearchColumns = new List<CustomSearchColumn>();
            foreach (var column in source.SearchColumns)
            {
                this.SearchColumns.Add(new CustomSearchColumn(column));
            }

            this.ViewColumns = new List<CustomSearchColumn>();
            foreach (var column in source.ViewColumns)
            {
                this.ViewColumns.Add(new CustomSearchColumn(column));
            }
        }

        /// <summary>
        /// データクリア
        /// </summary>
        public void Clear()
        {
            this.SearchSettingCaption = "";
            this.SearchColumns.Clear();
        }

        /// <summary>
        /// 抽出条件リストの更新
        /// </summary>
        /// <param name="record"></param>
        /// <param name="filter"></param>
        public void UpdateSearchColumns(MySearchGridRecord record, string filter)
        {
            var viewColumn = GetViewColumn(record.列名);
            int index = GetSearchColumnIndex(viewColumn.HeaderText);

            if (string.IsNullOrWhiteSpace(filter))
            {
                if (index != -1)
                {
                    // 抽出条件リストから削除
                    this.SearchColumns.RemoveAt(index);
                }
            }
            else
            {
                if (index == -1)
                {
                    // 抽出条件リストに追加する
                    this.SearchColumns.Add(new CustomSearchColumn(viewColumn));
                }

                // 検索条件の更新
                var colmun = GetSearchColumn(viewColumn.HeaderText);
                colmun.Filter = filter;
            }
            updateSearchFilterCaption();
        }

        /// <summary>
        /// 画面の入力項目を抽出候補用リストに反映させる
        /// </summary>
        /// <param name="record"></param>
        public void UpdateViewColumn(MySearchGridRecord record)
        {
            if (record == null)
            {
                return;
            }

            foreach (var viewColumn in this.ViewColumns)
            {
                if (viewColumn.HeaderText.Equals(record.列名))
                {
                    viewColumn.Filter = record.条件;
                    break;
                }
            }
        }

        /// <summary>
        /// 画面の入力項目を抽出条件リストに設定
        /// </summary>
        public void SetSearchColumns()
        {
            this.SearchColumns.Clear();
            foreach (var viewColumn in this.ViewColumns)
            {
                var searchColumn = GetSearchColumn(viewColumn.HeaderText);
                if (searchColumn == null && !string.IsNullOrWhiteSpace(viewColumn.Filter))
                {
                    // 抽出条件リストに追加する
                    this.SearchColumns.Add(new CustomSearchColumn(viewColumn));

                    updateSearchFilterCaption();
                }
            }
        }

        /// <summary>
        /// 指定されたカラム名から抽出候補のカラム取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CustomSearchColumn GetViewColumn(string name)
        {
            foreach (var viewColumn in this.ViewColumns)
            {
                if (viewColumn.HeaderText.Equals(name))
                {
                    return viewColumn;
                }
            }
            return null;
        }

        /// <summary>
        /// 指定されたカラム名から抽出候補のカラムインデックス取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetViewColumnIndex(string name)
        {
            int index = 0;
            foreach (var viewColumn in this.ViewColumns)
            {
                if (viewColumn.HeaderText.Equals(name))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        /// <summary>
        /// 指定されたカラム名から抽出条件のカラム取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CustomSearchColumn GetSearchColumn(string name)
        {
            foreach (var searchColumn in this.SearchColumns)
            {
                if (searchColumn.HeaderText.Equals(name))
                {
                    return searchColumn;
                }
            }
            return null;
        }

        /// <summary>
        /// 指定されたカラム名から抽出条件のカラムインデックス取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int GetSearchColumnIndex(string name)
        {
            int index = 0;
            foreach (var searchColumn in this.SearchColumns)
            {
                if (searchColumn.HeaderText.Equals(name))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        /// <summary>
        /// フィルタキャプションの更新
        /// </summary>
        private void updateSearchFilterCaption()
        {
            var sb = new StringBuilder();
            foreach (var searchColumn in this.SearchColumns)
            {
                // 「項目名：条件, 条件 項目名：・・・」形式で表示
                if (0 < sb.Length)
                {
                    sb.Append(" ");
                }
                sb.Append(searchColumn.HeaderText);
                sb.AppendFormat("：{0}", searchColumn.Filter);
            }

            SearchSettingCaption = sb.ToString();
        }

        /// <summary>
        /// データテーブルに該当する抽出条件のみ条件を残す
        /// </summary>
        /// <param name="dataTable"></param>
        public void SetDataTableColumns(DataTable dataTable)
        {
            // DataTableに存在しない抽出条件項目を削除する
            var tempColumns = new List<CustomSearchColumn>();
            foreach (var searchColumn in this.SearchColumns)
            {
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    if (dataColumn.ColumnName.Equals(searchColumn.Name))
                    {
                        tempColumns.Add(searchColumn);
                        break;
                    }
                }
            }

            this.SearchColumns = tempColumns;
            updateSearchFilterCaption();
        }

        public void SetDataGridViewColumns(DataGridView grid)
        {
            // 抽出用に表示カラムのみコピー
            var gridColumns = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn gridColumn in grid.Columns)
            {
                if (gridColumn.IsDataBound && gridColumn.Visible)
                {
                    gridColumns.Add(gridColumn);
                }
            }

            // 表示インデックスでソート
            gridColumns.Sort(
                delegate(DataGridViewColumn x, DataGridViewColumn y)
                {
                    return x.DisplayIndex - y.DisplayIndex;
                }
            );

            // グリッドの表示列タイトルでリスト作成
            ViewColumns = new List<CustomSearchColumn>();
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                var viewColumn = new CustomSearchColumn(gridColumn.Name, gridColumn.HeaderText, "", gridColumn.ValueType);
                ViewColumns.Add(viewColumn);
            }

            // 存在しているものだけを抽出項目に残す
            var tempColumns = new List<CustomSearchColumn>();
            foreach (var searchColumn in this.SearchColumns)
            {
                foreach (var viewColumn in ViewColumns)
                {
                    if (viewColumn.Name.Equals(searchColumn.Name))
                    {
                        tempColumns.Add(searchColumn);
                        break;
                    }
                }
            }

            this.SearchColumns = tempColumns;
        }

        public void SetDataGridViewColumns(DataGridView grid, List<string> deleteColumn)
        {
            // 抽出用に表示カラムのみコピー
            var gridColumns = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn gridColumn in grid.Columns)
            {
                if (gridColumn.IsDataBound && gridColumn.Visible && !deleteColumn.Contains(gridColumn.HeaderText))
                {
                    gridColumns.Add(gridColumn);
                }
            }

            // 表示インデックスでソート
            gridColumns.Sort(
                delegate(DataGridViewColumn x, DataGridViewColumn y)
                {
                    return x.DisplayIndex - y.DisplayIndex;
                }
            );

            // グリッドの表示列タイトルでリスト作成
            ViewColumns = new List<CustomSearchColumn>();
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                var viewColumn = new CustomSearchColumn(gridColumn.Name, gridColumn.HeaderText, "", gridColumn.ValueType);
                ViewColumns.Add(viewColumn);
            }

            // 存在しているものだけを抽出項目に残す
            var tempColumns = new List<CustomSearchColumn>();
            foreach (var searchColumn in this.SearchColumns)
            {
                foreach (var viewColumn in ViewColumns)
                {
                    if (viewColumn.Name.Equals(searchColumn.Name))
                    {
                        tempColumns.Add(searchColumn);
                        break;
                    }
                }
            }

            this.SearchColumns = tempColumns;
        }

        /// <summary>
        /// 日付形式として正しいか判定
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns>True:エラー有, False:エラー無</returns>
        /// <remarks>
        /// 下記形式の場合もエラーとする
        /// YYYY/MM
        /// YYYY/MM/DD HH:MM:SS
        /// YYYY/MM/DD HH:MM
        /// HH:MM:SS
        /// HH:MM
        /// </remarks>
        public bool HasErrorDateTime(string inputValue)
        {
            DateTime time;
            if (!DateTime.TryParse(inputValue, out time))
            {
                return true;
            }

            // YYYY/MM チェック
            if (Regex.IsMatch(inputValue,
                @"^(\d{4}|\d{3})(\/|-|\.)([1-9]|0[1-9]|1[012])$"))
            {
                // YYYY/MMの場合、YYYY/MM/01に変換されるが月単位で検索が可能だと誤解される恐れがあるため
                return true;
            }

            // 時分秒(HH:MM:SS等) チェック
            if (inputValue.Contains(":"))
            {
                // YYYY/MM/DD HH:MM:SS
                // YYYY/MM/DD HH:MM
                // HH:MM:SS
                // HH:MM

                // 時分秒に関しては、基本設定されずにデータ登録されているので入力時はエラーとする
                return true;
            }

            return false;
        }
    }


    public static class SearchSettingHelper
    {
        public static SearchSettingInfo LoadSearchSettingInfo(string id)
        {
            var searchSettingInfo = new SearchSettingInfo(id);
            searchSettingInfo.Clear();
            //(TBD)ここでidを識別子にファイルから読み込む
            // searchInfo.Decode(readText)

            return searchSettingInfo;
        }

        public static void SaveSearchSettingInfo(SearchSettingInfo searchSettingInfo)
        {
            //(TBD)ここでsearchInfo.idを識別子にファイルに書き込む
            // saveText = searchInfo.Encode();
        }
    }
}
