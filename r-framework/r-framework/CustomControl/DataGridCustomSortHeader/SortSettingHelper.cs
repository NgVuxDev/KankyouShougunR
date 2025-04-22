using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace r_framework.CustomControl.DataGridCustomControl
{
    public class CustomSortColumn
    {
        public string Name;
        public string HeaderText;
        public bool IsAsc;

        private CustomSortColumn()
        {
        }

        public CustomSortColumn(string Name, string HeaderText, bool IsAsc)
        {
            this.Name = Name;
            this.HeaderText = HeaderText;
            this.IsAsc = IsAsc;
        }

        public CustomSortColumn(CustomSortColumn source)
        {
            this.Name = source.Name;
            this.HeaderText = source.HeaderText;
            this.IsAsc = source.IsAsc;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}}", Name, HeaderText, IsAsc);
        }

        static public CustomSortColumn Decode(string encodedText)
        {
            var str = encodedText.Split('|');
            if (str.Length == 3)
            {
                var column = new CustomSortColumn();
                if (str[0].Length > 0)
                {
                    column.Name = str[0];
                    if (str[1].Length > 0)
                    {
                        column.HeaderText = str[1];
                        if (str[2].Length > 0 && bool.TryParse(str[2], out column.IsAsc))
                        {
                            return column;
                        }
                    }
                }
            }
            return null;
        }
    }
   
    
    public class SortSettingInfo
    {
        public string id { get; private set;}
        public string SortSettingCaption{ get; private set;}
        public List<CustomSortColumn> SortColumns { get; private set;}
        public List<CustomSortColumn> ViewColumns { get; private set; }

        public SortSettingInfo(string id)
        {
            this.id = id;
            SortSettingCaption = "";
            SortColumns = new List<CustomSortColumn>();
            ViewColumns = new List<CustomSortColumn>();
        }

        public SortSettingInfo(SortSettingInfo source)
        {
            Copy(source);
        }

        public void Copy(SortSettingInfo source)
        {
            id = source.id;
            SortSettingCaption = source.SortSettingCaption;

            SortColumns = new List<CustomSortColumn>();
            foreach (var column in source.SortColumns)
            {
                SortColumns.Add(new CustomSortColumn(column));
            }

            ViewColumns = new List<CustomSortColumn>();
            foreach (var column in source.ViewColumns)
            {
                ViewColumns.Add(new CustomSortColumn(column));
            }
        }

        public void Clear()
        {
            SortSettingCaption = "";
            SortColumns.Clear();
        }


        // indexで指定したものを次の優先キーにする（３つまで。選択済み、または４つ目以降は受け付けない）
        public bool SelectNextPriority(int index)
        {
            // 既に３つある場合はだめ
            if (SortColumns.Count < 3)
            {
                if (index >= 0 && index < ViewColumns.Count)
                {
                    var viewColumn = ViewColumns[index];
                    var sortColumn = GetSortColumn(viewColumn.Name);
                    if (sortColumn == null)
                    {
                        // 選択済みリストに追加する
                        SortColumns.Add(new CustomSortColumn(viewColumn));

                        updateSortOrderCaption();
                        return true;
                    }
                }
            }
            return false;
        }


        public bool ChangeSortOrder(int index)
        {
            if (index >= 0 && index < ViewColumns.Count)
            {
                var sortColumn = GetSortColumn(ViewColumns[index].Name);
                if (sortColumn != null)
                {
                    sortColumn.IsAsc = !sortColumn.IsAsc;
                    updateSortOrderCaption();
                    return true;
                }
            }
            return false;
        }
        
        public CustomSortColumn GetViewColumn(string Name)
        {
            foreach (var viewColumn in ViewColumns)
            {
                if (viewColumn.Name.Equals(Name))
                {
                    return viewColumn;
                }
            }
            return null;
        }

        public int GetViewColumnIndex(string Name)
        {
            int index = 0;
            foreach (var viewColumn in ViewColumns)
            {
                if (viewColumn.Name.Equals(Name))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public CustomSortColumn GetSortColumn(string Name)
        {
            foreach (var sortColumn in SortColumns)
            {
                if (sortColumn.Name.Equals(Name))
                {
                    return sortColumn;
                }
            }
            return null;
        }
        
        private void updateSortOrderCaption()
        {
            // ソート順オーダーリストをコピーしてキャプション文字列を作る
            // "列名C(昇順) > 列名A(降順) > 列名B(昇順)"
            var sb = new System.Text.StringBuilder();
            foreach (var sortColumn in SortColumns)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" > ");
                }
                sb.Append(sortColumn.HeaderText);
                sb.AppendFormat("({0})", sortColumn.IsAsc ? "昇順" : "降順"); 
            }

            SortSettingCaption = sb.ToString();
        }

        public string Encode()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendFormat("id.{0}=", id);
            foreach (var sortColumn in SortColumns)
            {
                sb.Append(sortColumn.ToString());
            }
            return sb.ToString();
        }

        public bool Decode(string encodedText)
        {
            var start = encodedText.IndexOf('=');
            if (start > 0 && start < encodedText.Length)
            {
                //TBD
            }

            return true;
        }

        public void SetDataTableColumns(DataTable dataTable)
        {
            // DataTableに存在しないソート項目を削除する
            var tempColumns = new List<CustomSortColumn>();
            foreach (var sortColumn in this.SortColumns)
            {
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    if (dataColumn.ColumnName.Equals(sortColumn.Name)) 
                    {
                        tempColumns.Add(sortColumn);
                        break;
                    }
                }
            }
            this.SortColumns = tempColumns;
            updateSortOrderCaption();
        }

        public void SetDataGridViewColumns(DataGridView grid)
        {
            // ソート用に表示カラムのみコピー
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
            ViewColumns = new List<CustomSortColumn>();
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                var viewColumn = new CustomSortColumn(gridColumn.Name, gridColumn.HeaderText, true);
                ViewColumns.Add(viewColumn);
            }
            
            // 存在しているものだけをソート項目に残す
            var tempColumns = new List<CustomSortColumn>();
            foreach (var sortColumn in this.SortColumns)
            {
                foreach (var viewColumn in ViewColumns)
                {
                    if (viewColumn.Name.Equals(sortColumn.Name))
                    {
                        tempColumns.Add(sortColumn);
                        break;
                    }
                }
            }
            this.SortColumns = tempColumns;
        }
    }

    static public class SortSettingHelper
    {
        static public SortSettingInfo LoadSortSettingInfo(string id)
        {
            var sortSettingInfo = new SortSettingInfo(id);
            sortSettingInfo.Clear();
            //(TBD)ここでidを識別子にファイルから読み込む
            // sortInfo.Decode(readText)

            return sortSettingInfo;
        }

        static public void SaveSortSettingInfo(SortSettingInfo sortSettingInfo)
        {
            //(TBD)ここでsortInfo.idを識別子にファイルに書き込む
            // saveText = sortInfo.Encode();
        }
    }
}
