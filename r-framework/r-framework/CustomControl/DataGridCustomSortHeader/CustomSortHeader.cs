using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using r_framework.Dto;
using r_framework.Utility;

namespace r_framework.CustomControl.DataGridCustomControl
{
    /// <summary>
    /// DataGridViewのソート用ヘッダー取得処理
    /// </summary>
    public partial class CustomSortHeader : UserControl
   {
        /// <summary>
        /// ソート中フラグ（未使用）
        /// </summary>
        public bool SortFlag { get; set; }

        /// <summary>
        /// ソート設定情報
        /// </summary>
        private SortSettingInfo sortSettingInfo = null;
                
        /// <summary>
        /// 紐付くデータグリッドビュー
        /// </summary>
        private CustomDataGridView linkedDataGridView = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomSortHeader()
        {
            this.SortFlag = false;
            InitializeComponent();

            if (!DesignMode)
            {
            }
        }

        /// <summary>
        /// 画面ロード処理
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
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

            // 保存しているソート設定情報の読み込み
            var id = this.FindForm().Name + "." + this.Name;
            sortSettingInfo = SortSettingHelper.LoadSortSettingInfo(id);

            // 最初のソート設定情報の表示
            txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;

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
        /// ソート条件のユーザー変更
        /// </summary>
        public void SortDataTable(DataTable dataTable)
        {
            if (dataTable == null)
            {
                return;
            }

            if (sortSettingInfo == null)
            {
                return;
            }

            sortSettingInfo.SetDataTableColumns(dataTable);
            this.txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;
            var sb = new System.Text.StringBuilder();

            foreach (var item in sortSettingInfo.SortColumns)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.AppendFormat("{0} {1}", item.Name, item.IsAsc ? "ASC" : "DESC");
            }
            Debug.WriteLine(sb);

            dataTable.DefaultView.Sort = sb.ToString();
        }

        /// <summary>
        /// ソート条件のユーザー変更
        /// </summary>
        public void ShowCustomSortSettingDialog()
        {
            if (sortSettingInfo != null && linkedDataGridView != null)
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

                var dataTable = linkedDataGridView.DataSource as DataTable;
            //  if (dataTable != null && dataTable.Rows.Count > 0) ゼロ件のときにダイアログ表示させたくない場合はここを有効にする
                {
                    sortSettingInfo.SetDataGridViewColumns(linkedDataGridView);
                    var dlg = new SortSettingForm(sortSettingInfo);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;
                        if (dataTable != null)
                        {
                            SortDataTable(dataTable);
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
        /// ソート条件のクリア
        /// </summary>
        public void ClearCustomSortSetting()
        {
            if (sortSettingInfo != null)
            {
                sortSettingInfo.Clear();
                txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;
            }
        }

        private void txboxSortSettingInfo_Enter(object sender, EventArgs e)
        {
            ShowCustomSortSettingDialog();
        }
   }
}
