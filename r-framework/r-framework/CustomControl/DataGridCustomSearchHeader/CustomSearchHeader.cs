// $Id: CustomSearchHeader.cs 30587 2014-09-19 10:56:19Z sanbongi $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;
using System.Reflection;

namespace r_framework.CustomControl.DataGridCustomControl
{
    /// <summary>
    /// DataGridViewの検索用ヘッダー取得処理
    /// </summary>
    public partial class CustomSearchHeader : UserControl
    {
        /// <summary>
        /// 検索設定情報
        /// </summary>
        private SearchSettingInfo searchSettingInfo = null;

        /// <summary>
        /// 紐付くデータグリッドビュー
        /// </summary>
        private CustomDataGridView linkedDataGridView = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomSearchHeader()
        {
            InitializeComponent();

            if (!DesignMode)
            {
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // DataGridViewとの連携初期化
            if (LinkedDataGridViewName != null && LinkedDataGridViewName.Length > 0)
            {
                // プロパティで設定されたDataGridViewの名前からコントロールを探す
                foreach (Control control in FindForm().Controls)
                {
                    if (control.Name.Equals(LinkedDataGridViewName))
                    {
                        linkedDataGridView = control as CustomDataGridView;
                        break;
                    }
                }
            }

            // 保存しているフィルタ設定情報の読み込み
            var id = this.FindForm().Name + "." + this.Name;
            searchSettingInfo = SearchSettingHelper.LoadSearchSettingInfo(id);

            // 最初のフィルタ設定情報の表示
            this.txboxSearchSettingInfo.Text = searchSettingInfo.SearchSettingCaption;

            if (linkedDataGridView != null)
            {
                this.Width = linkedDataGridView.Width;
            }
        }

        /// <summary>
        /// ポップアップへ送信するコントロール
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        public string LinkedDataGridViewName { get; set; }
        private bool ShouldSerializeLinkedDataGridViewName()
        {
            return this.LinkedDataGridViewName != null;
        }

        /// <summary>
        /// 検索条件のユーザー変更
        /// </summary>
        public void SearchDataTable(DataTable dataTable)
        {
            if (dataTable == null)
            {
                return;
            }

            if (searchSettingInfo == null)
            {
                return;
            }

            searchSettingInfo.SetDataTableColumns(dataTable);
            this.txboxSearchSettingInfo.Text = searchSettingInfo.SearchSettingCaption;
            var sb = new StringBuilder();

            string errorMessage = string.Empty;
            var hasError = HasErrorSearchColumns(searchSettingInfo, dataTable, out errorMessage);

            if (hasError)
            {
                // 条件にエラーがある場合は、アラート表示後フィルタ無しで抽出
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("W006", errorMessage);
            }
            else
            {
                foreach (var item in searchSettingInfo.SearchColumns)
                {
                    string rowFilter = CreateRowFilterItem(dataTable, item);
                    if (!string.IsNullOrEmpty(rowFilter))
                    {
                        if (0 < sb.Length)
                        {
                            sb.Append(" AND ");
                        }

                        sb.Append(rowFilter);
                    }
                }
            }
            Debug.WriteLine(sb);

            dataTable.CaseSensitive = true;
            string sql = sb.ToString();
            dataTable.DefaultView.RowFilter = sql;
        }

        /// <summary>
        /// フィルタの条件のエラー有無を判定
        /// </summary>
        /// <param name="settingInfo"></param>
        /// <param name="dataTable"></param>
        /// <param name="errorMessage">エラー対象の列名を設定</param>
        /// <returns>true:エラー有, false:エラー無</returns>
        private bool HasErrorSearchColumns(SearchSettingInfo settingInfo, DataTable dataTable, out string errorMessage)
        {
            errorMessage = string.Empty;
            bool hasError = false;
            List<string> errList = new List<string>();

            foreach (var item in settingInfo.SearchColumns)
            {
                var t = dataTable.Columns[item.Name].DataType;

                if (t == typeof(String))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        if (string.IsNullOrWhiteSpace(inputValue))
                        {
                            errList.Add(item.HeaderText);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(DateTime))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        if (searchSettingInfo.HasErrorDateTime(inputValue))
                        {
                            errList.Add(item.HeaderText);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Int16))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Int16 result;
                        if (!Int16.TryParse(inputValue, out result))
                        {
                            errList.Add(item.HeaderText);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Int32))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Int32 result;
                        if (!Int32.TryParse(inputValue, out result))
                        {
                            errList.Add(item.HeaderText);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Int64))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Int64 result;
                        if (!Int64.TryParse(inputValue, out result))
                        {
                            errList.Add(item.HeaderText);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Double))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Double result;
                        if (!Double.TryParse(inputValue, out result))
                        {
                            errList.Add(item.HeaderText);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Decimal))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Decimal result;
                        if (!Decimal.TryParse(inputValue, out result))
                        {
                            errList.Add(item.HeaderText);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Boolean))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Boolean result;
                        if (!Boolean.TryParse(inputValue, out result))
                        {
                            errList.Add(item.HeaderText);

                            hasError = true;
                            break;
                        }
                    }
                }
            }

            var sb = new StringBuilder();
            foreach (var err in errList)
            {
                if (0 < sb.Length)
                {
                    sb.Append(", ");
                }

                sb.Append(err);
            }
            errorMessage = sb.ToString();

            return hasError;
        }

        /// <summary>
        /// 指定されたカラムの条件作成
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private string CreateRowFilterItem(DataTable dataTable, CustomSearchColumn column)
        {
            if (dataTable == null || column == null)
            {
                return null;
            }

            var sb = new StringBuilder();
            var t = dataTable.Columns[column.Name].DataType;

            if (t == typeof(String))
            {
                foreach (var str in column.Filter.Split(','))
                {
                    if (0 < sb.Length)
                    {
                        sb.Append(" OR ");
                    }
                    else
                    {
                        sb.Append("( ");
                    }

                    // LIKE検索用にエスケープ。ToEscapeStrは不可
                    StringBuilder sbLike = new StringBuilder();
                    for (int i = 0; i < str.Trim().Length; i++)
                    {
                        char c = str[i];
                        if (c == '*' || c == '%' || c == '[' || c == ']')
                        {
                            sbLike.Append("[").Append(c).Append("]");
                        }
                        else if (c == '\'')
                        {
                            sbLike.Append("''");
                        }
                        else
                        {
                            sbLike.Append(c);
                        }
                    }
                    string item = string.Format("'%{0}%'", sbLike.ToString());
                    string name = ToEscapeStr(column.Name);

                    sb.AppendFormat("{0} LIKE {1}", name, item);
                }

                if (0 < sb.Length)
                {
                    sb.AppendFormat(")");
                }
                return sb.ToString();
            }
            else if (t == typeof(DateTime))
            {
                foreach (var str in column.Filter.Split(','))
                {
                    if (0 < sb.Length)
                    {
                        sb.Append(" OR ");
                    }
                    else
                    {
                        sb.Append("( ");
                    }

                    DateTime dt = DateTime.Parse(str);
                    DateTime dtMin = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                    DateTime dtMax = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);

                    string name = ToEscapeStr(column.Name);

                    // 作成日時、最終更新日時(CREATE_DATE,UPDATE_DATE)等、
                    // 時分秒をもつ日付も考慮して、0:00:00～23:59:59の範囲検索
                    sb.AppendFormat("(#{0}# <= {1} AND {1} <= #{2}#)", dtMin, name, dtMax);
                }

                if (0 < sb.Length)
                {
                    sb.AppendFormat(")");
                }
                return sb.ToString();
            }
            else
            {
                // 数値系の条件作成を想定
                foreach (var str in column.Filter.Split(','))
                {
                    if (0 < sb.Length)
                    {
                        sb.Append(", ");
                    }

                    sb.AppendFormat("{0}", str.Trim());
                }

                string name = ToEscapeStr(column.Name);

                var filter = string.Format("{0} IN ({1})", name, sb);
                return filter;
            }
        }

        /// <summary>
        /// 特殊文字をエスケープした文字で返す
        /// </summary>
        /// <remarks>RowFilterの検索項目名用</remarks>
        /// <param name="str">変換対象文字列</param>
        /// <returns></returns>
        private string ToEscapeStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            string result = str;

            // エスケープ対象の特殊文字
            var kigos = "~()#\\=><+-*%&|^\"[]!,.\'`{}?/:;@";

            foreach (var kigo in kigos)
            {
                if (result.Contains(kigo))
                {
                    result = "[" + result + "]";
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 検索条件のユーザー変更
        /// </summary>
        public void ShowCustomSearchSettingDialog()
        {
            if (searchSettingInfo != null && linkedDataGridView != null)
            {
                //自動作成チェックボックス列を非表示
                if (linkedDataGridView.ListHeaderCheckbox != null && linkedDataGridView.ListHeaderCheckbox.Length > 0)
                {
                    foreach (var item in linkedDataGridView.ListHeaderCheckbox)
                    {
                        if (!item.SORT_FILTER_FLG)
                        {
                            linkedDataGridView.Columns[item.COLUMN_NAME].Visible = false;
                        }
                    }
                }

                bool isNotset = true;
                var dataTable = linkedDataGridView.DataSource as DataTable;
                //if (dataTable != null && dataTable.Rows.Count > 0) ゼロ件のときにダイアログ表示させたくない場合はここを有効にする
                {
                    object form = linkedDataGridView.FindForm();
                    if (form != null)
                    {
                        Type type = form.GetType();
                        PropertyInfo pinfo = type.GetProperty("NotSearchColumn");
                        if (pinfo != null)
                        {
                            var temp = pinfo.GetValue(form, null) as List<string>;
                            if (temp != null)
                            {
                                searchSettingInfo.SetDataGridViewColumns(linkedDataGridView,temp);
                                isNotset = false;
                            }
                        }
                    }

                    if (isNotset)
                    {
                        searchSettingInfo.SetDataGridViewColumns(linkedDataGridView);
                    }
                    
                    var dlg = new SearchSettingForm(searchSettingInfo);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.txboxSearchSettingInfo.Text = searchSettingInfo.SearchSettingCaption;
                        if (dataTable != null)
                        {
                            SearchDataTable(dataTable);
                        }
                    }
                    dlg.Dispose();
                }

                //自動作成チェックボックス列を再表示
                if (linkedDataGridView.ListHeaderCheckbox != null && linkedDataGridView.ListHeaderCheckbox.Length > 0)
                {
                    foreach (var item in linkedDataGridView.ListHeaderCheckbox)
                    {
                        if (!item.SORT_FILTER_FLG)
                        {
                            linkedDataGridView.Columns[item.COLUMN_NAME].Visible = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 検索条件のクリア
        /// </summary>
        public void ClearCustomSearchSetting()
        {
            if (searchSettingInfo != null)
            {
                searchSettingInfo.Clear();
                this.txboxSearchSettingInfo.Text = searchSettingInfo.SearchSettingCaption;
            }
        }
    }
}
