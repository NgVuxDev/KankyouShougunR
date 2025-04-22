// $Id: SearchSettingForm.cs 54453 2015-07-03 01:25:56Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using r_framework.Logic;

namespace r_framework.CustomControl.DataGridCustomControl
{
    /// <summary>
    /// CustomDatGridViewの検索条件を設定するポップアップフォーム
    /// </summary>
    public partial class SearchSettingForm : Form
    {
        /// <summary>
        /// コンストラクタ引数で指定される呼び出し元の検索情報の参照
        /// </summary>
        private SearchSettingInfo searchSettingInfoOwner;

        /// <summary>
        /// このフォームで編集するための検索情報のコピー
        /// </summary>
        private SearchSettingInfo searchSettingInfo;

        /// <summary>
        /// グリッドに表示するための検索対象列のリスト
        /// </summary>
        private List<MySearchGridRecord> myDataSource;

        private bool isLoading = false;

        // FormのClose処理中か
        private bool isCloseing = false;

        /// <summary>
        /// コンストラクタ(引数なし)
        /// </summary>
        /// <remarks>引数無しのデフォルトコンストラクタは呼び出し禁止</remarks>
        private SearchSettingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="searchSettingInfo">検索設定情報</param>
        public SearchSettingForm(SearchSettingInfo searchSettingInfo)
        {
            isLoading = true;
            isCloseing = false;
            InitializeComponent();

            this.KeyPreview = true;

            this.searchSettingInfoOwner = searchSettingInfo;
            this.searchSettingInfo = new SearchSettingInfo(searchSettingInfo);

            this.myDataSource = new List<MySearchGridRecord>();
            foreach (var viewColumn in this.searchSettingInfo.ViewColumns)
            {
                var gridRec = new MySearchGridRecord();
                gridRec.列名 = viewColumn.HeaderText;
                gridRec.条件 = "";
                gridRec.型 = viewColumn.ItemType;

                this.myDataSource.Add(gridRec);
            }
            this.grid.DataSource = this.myDataSource;

            // コンストラクタ内で指定しないとなぜかColorが反映されないため
            this.grid.Columns["列名"].ReadOnly = true;
            this.grid.ImeMode = ImeMode.On;

            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            isLoading = true;

            base.OnLoad(e);

            // 
            this.grid.Columns["列名"].FillWeight = 150;
            this.grid.Columns["条件"].Width = 425;
            this.grid.Columns["型"].Visible = false;

            int index = 0;
            if (0 < this.searchSettingInfo.SearchColumns.Count)
            {
                string topName = this.searchSettingInfo.SearchColumns[0].Name;
                int topIndex = this.searchSettingInfo.GetViewColumnIndex(topName);
                if (0 <= topIndex)
                {
                    index = topIndex;
                }

                this.grid.CurrentCell = this.grid.Rows[index].Cells[1];
            }
            else if (0 < this.grid.Rows.Count)
            {
                this.grid.CurrentCell = this.grid.Rows[0].Cells[1];
            }

            // グリッドとキャプションの最初の表示更新
            updateSearchFilterCaption();

            isLoading = false;
        }

        /// <summary>
        /// 検索条件の変更に応じてグリッドとキャプション表示の更新
        /// </summary>
        private void updateSearchFilterCaption()
        {
            foreach (var gridRec in myDataSource)
            {
                gridRec.条件 = "";
            }

            for (int i = 0; i < searchSettingInfo.SearchColumns.Count; i++)
            {
                var searchColumn = searchSettingInfo.SearchColumns[i];
                int index = this.searchSettingInfo.GetViewColumnIndex(searchColumn.HeaderText);
                var gridRec = myDataSource[index];
                gridRec.条件 = searchColumn.Filter;
            }

            this.grid.Refresh();

            // 最後にキャプションを書き換える
            this.textSearchSettingInfo.Text = searchSettingInfo.SearchSettingCaption;
        }

        /// <summary>
        /// 「実行」ファンクション
        /// </summary>
        private void OnExecute()
        {
            DialogResult = DialogResult.OK;

            var records = this.grid.DataSource as List<MySearchGridRecord>;

            foreach (var record in records)
            {
                searchSettingInfo.UpdateViewColumn(record);
            }

            searchSettingInfo.SetSearchColumns();
            searchSettingInfoOwner.Copy(searchSettingInfo);

            Close();
        }

        /// <summary>
        /// 「クリア」ファンクション
        /// </summary>
        private void OnClear()
        {
            searchSettingInfo.Clear();
            updateSearchFilterCaption();
        }

        /// <summary>
        /// ファンクションボタン/キーのディスパッチ
        /// </summary>
        /// <param name="key">キーコード</param>
        private void OnFunction(Keys key)
        {
            Debug.WriteLine("OnFunction: " + key.ToString());
            switch (key)
            {
                case Keys.F1:  //実行
                    OnExecute();
                    break;

                case Keys.F11: //クリア
                    OnClear();
                    break;

                case Keys.F12: //閉じる
                    DialogResult = DialogResult.Cancel;
                    this.AutoValidate = AutoValidate.Disable;
                    // FormのClose処理中とする
                    isCloseing = true;
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// ファンクションボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFunctionButton(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var key = Keys.None;
                if (button == buttonF1) key = Keys.F1;
                if (button == buttonF11) key = Keys.F11;
                if (button == buttonF12) key = Keys.F12;
                Debug.WriteLine("OnFunctionButton: " + key.ToString());
                if (key != Keys.None)
                {
                    OnFunction(key);
                }
            }
        }

        /// <summary>
        /// キー押下（ファンクションキー）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            // Windows標準機能を外す
            if (Keys.F1 <= e.KeyCode && e.KeyCode <= Keys.F12)
            {
                if (Keys.F1 == e.KeyCode || e.KeyCode == Keys.F11 || e.KeyCode == Keys.F12)
                {
                    bool isDGV = this.ActiveControl is DataGridViewTextBoxEditingControl;

                    if (e.KeyCode != Keys.F12)
                    {
                        // 一覧編集中の場合
                        if (isDGV)
                        {
                            Point p = this.grid.CurrentCellAddress;
                            string text = this.ActiveControl.Text;

                            var hasError = HasErrorInputValue(p.X, p.Y, text);
                            if (hasError)
                            {
                                return;
                            }

                            updateSearchInfo(p.X, p.Y, text);
                        }
                    }

                    Debug.WriteLine("OnKeyUp: " + e.KeyCode.ToString());
                    OnFunction(e.KeyCode);

                    if (isDGV && e.KeyCode == Keys.F11)
                    {
                        this.ActiveControl.Text = string.Empty;
                    }
                }

                e.Handled = true;
            }
        }

        /// <summary>
        /// 各セルのValidateが開始したときに処理されます
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string name = this.grid.Columns[e.ColumnIndex].Name;
            if ("条件".Equals(name))
            {
                if (this.grid[e.ColumnIndex, e.RowIndex].EditedFormattedValue == null
                    || this.grid.Rows[e.RowIndex].Cells["型"].EditedFormattedValue == null)
                {
                    return;
                }

                string value = this.grid[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString();
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                // AutoValidate に AutoValidate.Disable を設定してもValidatingが発生するので独自対応
                if (isCloseing)
                {
                    // F12(閉じる)ボタンが押下された場合はValidating処理しない
                    return;
                }

                // 入力値チェック
                var hasError = HasErrorInputValue(e.ColumnIndex, e.RowIndex, value);
                if (hasError)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 各セルのValidateが終了されたときに処理されます
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCellValidated(object sender, DataGridViewCellEventArgs e)
        {
            string text = string.Empty;
            if (this.grid[e.ColumnIndex, e.RowIndex].Value != null)
            {
                text = this.grid[e.ColumnIndex, e.RowIndex].Value.ToString();
            }

            updateSearchInfo(e.ColumnIndex, e.RowIndex, text);
        }

        /// <summary>
        /// 指定された一覧の入力値のエラー有無判定
        /// </summary>
        /// <param name="columnIndex">Columnインデックス</param>
        /// <param name="rowIndex">Rowインデックス</param>
        /// <param name="text">入力値</param>
        /// <returns></returns>
        private bool HasErrorInputValue(int columnIndex, int rowIndex, string text)
        {
            string name = this.grid.Columns[columnIndex].Name;
            if ("条件".Equals(name))
            {
                if (this.grid.Rows[rowIndex].Cells["型"].Value == null
                    || string.IsNullOrEmpty(text))
                {
                    return false;
                }

                string[] inputValues = text.Split(',');

                Type t = Type.GetType(this.grid.Rows[rowIndex].Cells["型"].Value.ToString());
                var messageShowLogic = new MessageBoxShowLogic();

                if (t == typeof(DateTime))
                {
                    foreach (var inputValue in inputValues)
                    {
                        if (searchSettingInfo.HasErrorDateTime(inputValue))
                        {
                            messageShowLogic.MessageBoxShow("E012", "YYYY/MM/DD形式の日付");
                            return true;
                        }
                    }
                }
                else if (t == typeof(Int16))
                {
                    foreach (var inputValue in inputValues)
                    {
                        Int16 result;
                        if (!Int16.TryParse(inputValue, out result))
                        {
                            messageShowLogic.MessageBoxShow("E012", string.Format("{0}～{1}の範囲の数値", Int16.MinValue, Int16.MaxValue));
                            return true;
                        }
                    }
                }
                else if (t == typeof(Int32))
                {
                    foreach (var inputValue in inputValues)
                    {
                        Int32 result;
                        if (!Int32.TryParse(inputValue, out result))
                        {
                            messageShowLogic.MessageBoxShow("E012", string.Format("{0}～{1}の範囲の数値", Int32.MinValue, Int32.MaxValue));
                            return true;
                        }
                    }
                }
                else if (t == typeof(Int64))
                {
                    foreach (var inputValue in inputValues)
                    {
                        Int64 result;
                        if (!Int64.TryParse(inputValue, out result))
                        {
                            messageShowLogic.MessageBoxShow("E012", string.Format("{0}～{1}の範囲の数値", Int64.MinValue, Int64.MaxValue));
                            return true;
                        }
                    }
                }
                else if (t == typeof(Double))
                {
                    foreach (var inputValue in inputValues)
                    {
                        Double result;
                        if (!Double.TryParse(inputValue, out result))
                        {
                            messageShowLogic.MessageBoxShow("E012", string.Format("308桁以下の数値"));
                            return true;
                        }
                    }
                }
                else if (t == typeof(Decimal))
                {
                    foreach (var inputValue in inputValues)
                    {
                        Decimal result;
                        if (!Decimal.TryParse(inputValue, out result))
                        {
                            messageShowLogic.MessageBoxShow("E012", string.Format("{0}～{1}の範囲の数値", Decimal.MinValue, Decimal.MaxValue));
                            return true;
                        }
                    }
                }
                else if (t == typeof(Boolean))
                {
                    foreach (var inputValue in inputValues)
                    {
                        Boolean result;
                        if (!Boolean.TryParse(inputValue, out result))
                        {
                            messageShowLogic.MessageBoxShow("E012", "「TRUE」又は「FALSE」");
                            return true;
                        }
                    }
                }
                else if (t == typeof(String))
                {
                    foreach (var inputValue in inputValues)
                    {
                        if (string.IsNullOrWhiteSpace(inputValue))
                        {
                            messageShowLogic.MessageBoxShow("E012", "空白以外の文字等を");
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// グリッドの入力値から検索情報を更新
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="text"></param>
        private void updateSearchInfo(int columnIndex, int rowIndex, string text)
        {
            string name = this.grid.Columns[columnIndex].Name;
            if ("条件".Equals(name))
            {
                var records = this.grid.DataSource as List<MySearchGridRecord>;

                searchSettingInfo.UpdateSearchColumns(records[rowIndex], text);
                updateSearchFilterCaption();
            }
        }

        /// <summary>
        /// Fomrのアクティベートイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchSettingForm_Activated(object sender, EventArgs e)
        {
            // 入力モードがひらがななら強制的に変換モードを有効にする。
            // 勝手に変換モードが無変換になってしまうことがある現象の対策。
            this.AdjustControlImeSentenceMode();
        }

        /// <summary>
        /// DagaGridViewのEditingControlShowingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private delegate void imedelgate(); 
        private void grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // 入力モードがひらがななら強制的に変換モードを有効にする。
            // 勝手に変換モードが無変換になってしまうことがある現象の対策。
            BeginInvoke(new imedelgate(this.AdjustControlImeSentenceMode));
        }

        /// <summary>
        ///  入力モードがひらがななら強制的に変換モードを有効にする。
        ///  勝手に変換モードが無変換になってしまうことがある現象の対策。
        /// </summary>
        private void AdjustControlImeSentenceMode()
        {
            // 入力モードがひらがななら強制的に変換モードを有効にする。
            // 勝手に変換モードが無変換になってしまうことがある現象の対策。
            r_framework.Utility.ImeUtility.AdjustControlImeSentenceMode(this.grid.EditingControl);
        }

    }

    /// <summary>
    /// データグリッドビューの表示用クラス
    /// </summary>
    public class MySearchGridRecord
    {
        public string 列名 { get; set; }
        public string 条件 { get; set; }
        public Type 型 { get; set; } // 条件の入力値チェック用に保持
    }
}
