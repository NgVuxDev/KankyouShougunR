using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace r_framework.CustomControl
{
    class DgvCustomComboBoxEditingControl : DataGridViewComboBoxEditingControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DgvCustomComboBoxEditingControl()
        {
        }

        /// <summary>
        /// キーダウンイベントハンドラ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // ここでイベントをハンドルしないと次のセルのドロップダウンが開いてすぐ閉じる
                e.Handled = true;

                // エンターキーでタブキーの仕事をさせる
                this.ProcessDialogKey(Keys.Tab);
            }

            base.OnKeyDown(e);
        }
    }
}
