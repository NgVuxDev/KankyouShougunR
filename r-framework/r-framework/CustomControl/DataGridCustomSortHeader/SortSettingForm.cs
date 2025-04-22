using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace r_framework.CustomControl.DataGridCustomControl
{
    /// <summary>
    /// CustomDatGridViewのソート条件を設定するポップアップフォーム
    /// </summary>
    public partial class SortSettingForm : Form
    {
        /// <summary>
        /// コンストラクタ引数で指定される呼び出し元のソート情報の参照
        /// </summary>
        private SortSettingInfo sortSettingInfoOwner;

        /// <summary>
        /// このフォームで編集するためのソート情報のコピー
        /// </summary>
        private SortSettingInfo sortSettingInfo;

        /// <summary>
        /// グリッドに表示するためのソート対象列のリスト
        /// </summary>
        private List<MyGridRecord> myDataSource;

        /// <summary>
        /// コンストラクタ（引数なし）
        /// <remarks>引数なしのデフォルトコンストラクタは呼び出し禁止
        /// </summary>
        private SortSettingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// <param name="sortSettingInfo">ソート設定情報<see cref="SortSettingInfo"/>を指定します。</param>
        /// </summary>
        public SortSettingForm(SortSettingInfo sortSettingInfo)
        {
            InitializeComponent();
            
            // このフォームでキーセンスするおまじない
            this.KeyPreview = true;

            // 呼び出し元のソート情報の参照を保存し、このフォームでの編集用にコピーする
            this.sortSettingInfoOwner = sortSettingInfo;
            this.sortSettingInfo = new SortSettingInfo(sortSettingInfo);

            // ソート選択対象となる列名の一覧のデータソース作成
            myDataSource = new List<MyGridRecord>();
            foreach (var viewColumn in this.sortSettingInfo.ViewColumns)
            {
                var gridRec = new MyGridRecord();
                gridRec.優先度 = "";
                gridRec.順序 = "";
                gridRec.列名 = viewColumn.HeaderText;
                myDataSource.Add(gridRec);
            }
            grid.DataSource = myDataSource;

            // グリッドの列幅を設定
            grid.Columns[0].Width = 30;
            grid.Columns[1].Width = 20;
            grid.Columns[2].FillWeight = 100;

            // グリッドとキャプションの最初の表示更新
            updateSortOrderCaption();

            // 第1優先のソート項目を行選択状態にする
            int index = 0;
            if (this.sortSettingInfo.SortColumns.Count > 0)
            {
                string TopName = this.sortSettingInfo.SortColumns[0].Name;
                int topIndex = this.sortSettingInfo.GetViewColumnIndex(TopName);
                if (topIndex >= 0)
                {
                    index = topIndex;
                }

                grid.CurrentCell = grid.Rows[index].Cells[0];
            }

            // ファンクションボタンの最初の有効無効更新
            updateFunctionButtons();

            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// ソート条件の変更に応じてグリッドとキャプション表示の更新
        /// </summary>
        private void updateSortOrderCaption()
        {
            foreach (var gridRec in myDataSource)
            {
                gridRec.優先度 = "";
                gridRec.順序 = "";
            }

            for (int i = 0; i < sortSettingInfo.SortColumns.Count; i++)
            {
                var sortColumn = sortSettingInfo.SortColumns[i];
                int index = this.sortSettingInfo.GetViewColumnIndex(sortColumn.Name);
                var gridRec = myDataSource[index];
                gridRec.優先度 = (i + 1).ToString();
                gridRec.順序 = sortColumn.IsAsc ? "昇順" : "降順";
            }

            grid.Refresh();

            // 最後にキャプションを書き換える
            this.textSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;
        }

        /// <summary>
        /// ファンクションボタンの有効/無効更新
        /// </summary>
        private void updateFunctionButtons()
        {
            int index = -1;
            if (grid.SelectedRows.Count > 0)
            {
                index = grid.SelectedRows[0].Index;
            }

            bool f2 = false;
            bool f3 = false;
            bool f11 = false;

            if (index >= 0 && index < myDataSource.Count)
            {
                // 3つ未満でかつ選択項目がまだ優先度設定されていなければ選択可能
                if (sortSettingInfo.SortColumns.Count < 3)
                {
                    if (myDataSource[index].優先度.Equals(""))
                    {
                        f2 = true;
                    }
                }

                // 選択項目に既に順序が設定されていれば変更可能
                if (!myDataSource[index].順序.Equals(""))
                {
                    f3 = true;
                }
            }

            if (sortSettingInfo.SortColumns.Count > 0)
            {
                f11 = true;
            }

            buttonF2.Enabled = f2;
            buttonF3.Enabled = f3;
            buttonF11.Enabled = f11;
            
            if (!grid.Focused)
            {
                grid.Focus();
            }
        }

        /// <summary>
        /// グリッド選択行変更
        /// </summary>
        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            // 選択行に応じてファンクションボタンの有効/無効を切り替える
            updateFunctionButtons();
        }

        /// <summary>
        /// 「選択」ファンクション
        /// </summary>
        private void OnSelectSortColumn()
        {
            if (grid.SelectedRows.Count > 0)
            {
                var index = grid.SelectedRows[0].Index;
                if (index >= 0 && index < myDataSource.Count)
                {
                    if (sortSettingInfo.SelectNextPriority(index))
                    {
                        updateSortOrderCaption();
                    }
                }
            }
            updateFunctionButtons();
        }

        /// <summary>
        /// 「順序」ファンクション
        /// </summary>
        private void OnChangerSortOrder()
        {
            if (grid.SelectedRows.Count > 0)
            {
                var index = grid.SelectedRows[0].Index;
                if (index >= 0 && index < myDataSource.Count)
                {
                    sortSettingInfo.ChangeSortOrder(index);
                    updateSortOrderCaption();
                }
            }
            updateFunctionButtons();
        }

        /// <summary>
        /// 「クリア」ファンクション
        /// </summary>
        private void OnClear()
        {
            sortSettingInfo.Clear();
            updateSortOrderCaption();
            updateFunctionButtons();
        }


        private void OnEnter()
        {
            if (grid.Rows.Count < 1) return;

            int index = grid.SelectedRows[0].Index;
            if (index >= 0 && index < myDataSource.Count)
            {
                if (myDataSource[index].優先度.Equals(""))
                {
                    // まだソート項目に選択されていなければ選択状態にする
                    OnSelectSortColumn();
                }
                else
                {
                    // 既にソート項目選択されていれば昇順/降順の切り替えをする
                    OnChangerSortOrder();
                }
            }
        }

        /// <summary>
        /// ファンクションボタン/キーのディスパッチ
        /// </summary>
        private void OnFunction(Keys key)
        {
            Debug.WriteLine("OnFunction: " + key.ToString());
            switch (key)
            {
                case Keys.F1:  //実行
                    DialogResult = DialogResult.OK;
                    sortSettingInfoOwner.Copy(sortSettingInfo); 
                    Close();
                    break;
                
                case Keys.F2:  //選択
                    if (buttonF2.Enabled)
                    {
                        OnSelectSortColumn();
                    }
                    break;
                
                case Keys.F3:  //順序
                    if (buttonF3.Enabled)
                    {
                        OnChangerSortOrder();
                    }
                    break;
                
                case Keys.F11: //クリア
                    OnClear();
                    break;

                case Keys.F12: //閉じる
                    DialogResult = DialogResult.Cancel;
                    Close();
                    break;
            }
        }

        /// <summary>
        /// ファンクションボタン
        /// </summary>
        private void OnFunctionButton(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var key = Keys.None;
                if (button == buttonF1) key = Keys.F1;
                if (button == buttonF2) key = Keys.F2;
                if (button == buttonF3) key = Keys.F3;
                if (button == buttonF11) key = Keys.F11;
                if (button == buttonF12) key = Keys.F12;
                Debug.WriteLine("OnFunctionButton: " + key.ToString());
                if (key >= Keys.F1 && key <= Keys.F12)
                {
                    OnFunction(key);
                }
            }
        }

        /// <summary>
        /// キー押下（ファンクションキー）
        /// </summary>
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
            {
                Debug.WriteLine("OnKeyUp: " + e.KeyCode.ToString());
                OnFunction(e.KeyCode);
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                OnEnter();
                e.Handled = true;
            }
        }

        /// <summary>
        /// マウスダブルクリック
        /// </summary>
        private void OnCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OnEnter();
        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; //下に移動させない
            }
        }
    }

    /// <summary>
    /// このフォームのグリッドのデータソースの型。このフォームだけで使用する
    /// </summary>
    internal class MyGridRecord
    {
        public string 優先度 { set; get; }
        public string 順序 { set; get; }
        public string 列名 { set; get; }
    }
}
