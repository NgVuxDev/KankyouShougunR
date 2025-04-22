// $Id: DgvCustom.cs 39010 2015-01-08 08:50:58Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Windows.Forms;
using r_framework.CustomControl;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku
{
    public partial class DgvCustom : r_framework.CustomControl.CustomDataGridView //　CustomDataGridView を継承
    {
        /// <summary>
        /// マウスクリックかどうか。CellEnterで使用
        /// </summary>
        private bool isMouseClick = false;

        /// <summary>
        /// Left keyの移動かどうか。CellEnterで使用
        /// </summary>
        private bool isLeftKeyDown = false;

        // ProcessDataGridViewKeyをoverride
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            var cell = this.CurrentCell as ICustomDataGridControl;

            if (cell != null)
            {
                // 呼び出し機能ごとにPopupGetMasterFieldを設定
                switch (cell.PopupWindowId)
                {
                    case r_framework.Const.WINDOW_ID.M_GYOUSHA:
                        cell.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
                        break;

                    case r_framework.Const.WINDOW_ID.M_GENBA:
                        cell.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
                        break;

                    default:
                        break;
                }
                // PopupGetMasterField に定義している順番で値の設定先を指定
                // 呼び出し元にGYOUSHA_CD、呼び出し元+1の列にGYOUSHA_NAME_RYAKUが設定される
                if (cell.PopupGetMasterField != null && cell.PopupGetMasterField != "")
                {
                    if (cell.PopupWindowId == r_framework.Const.WINDOW_ID.M_GENBA_CLOSED
                        || cell.PopupWindowId == r_framework.Const.WINDOW_ID.M_GENBA)
                    {
                        cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, 
                                                      this.CurrentRow.Cells[this.CurrentCell.ColumnIndex + 1] as ICustomDataGridControl,
                                                      this.CurrentRow.Cells[this.CurrentCell.ColumnIndex - 2] as ICustomDataGridControl, 
                                                      this.CurrentRow.Cells[this.CurrentCell.ColumnIndex - 1] as ICustomDataGridControl
                                                    };
                    }
                    else
                    {
                        cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, this.CurrentRow.Cells[this.CurrentCell.ColumnIndex + 1] as ICustomDataGridControl };
                    }
                }
            }

            // left keyでの移動か。初期状態(false)の場合のみチェック
            if (!this.isLeftKeyDown)
            {
                this.isLeftKeyDown = e.KeyCode == Keys.Left;
            }
            
            return base.ProcessDataGridViewKey(e);
        }

        /// <summary>
        /// ReadonlyセルはSkip
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellEnter(DataGridViewCellEventArgs e)
        {
            var cell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
            DataGridViewCell nextCell = null;
            if (e.ColumnIndex != this.ColumnCount - 1)
            {
                nextCell = this.Rows[e.RowIndex].Cells[e.ColumnIndex + 1];
            }

            // マウスクリック時は飛ばない
            if (cell != null && !this.isMouseClick)
            {
                if (cell.ReadOnly && nextCell != null && !nextCell.ReadOnly)
                {
                    // left keyなら shift + tabと同じ動作
                    if (this.isLeftKeyDown)
                    {
                        SendKeys.Send("+{TAB}");
                    }
                }
                else
                {
                    // readonly cellでなくなったらリセット
                    this.isLeftKeyDown = false;
                }
            }

            base.OnCellEnter(e);

            this.isMouseClick = false;
        }

        /// <summary>
        /// マウスクリック時にフラグをON
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.isMouseClick = true;
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Gridのフォーカス時、Readonly以外のセルを選択
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            // Gridのフォーカス時の選択セルを決定
            if (this.Rows.Count > 0 && this.CurrentCell != null && this.CurrentCell.RowIndex == 0 && this.CurrentCell.ColumnIndex == 0 && this.CurrentCell.ReadOnly)
            {
                foreach (DataGridViewCell cell in this.Rows[0].Cells)
                {
                    if (!cell.ReadOnly)
                    {
                        this.CurrentCell = cell;
                        break;
                    }
                }
            }
            base.OnEnter(e);
        }


    }
}
