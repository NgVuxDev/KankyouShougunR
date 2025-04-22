using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.CustomControl;
using System.Windows.Forms;
using System.ComponentModel;

namespace Shougun.Core.Master.CourseNyuryoku.APP
{
   public partial class DgvCustom : r_framework.CustomControl.CustomDataGridView //　CustomDataGridView を継承
    {
        // ProcessDataGridViewKeyをoverride
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            var cell = this.CurrentCell as ICustomDataGridControl;

            if (cell != null)
            {
                // 呼び出し機能ごとにPopupGetMasterFieldを設定
                switch (cell.PopupWindowId)
                {
                    case r_framework.Const.WINDOW_ID.M_NYUUSHUKKIN_KBN:
                        cell.PopupGetMasterField = "NYUUSHUKKIN_KBN_CD,NYUUSHUKKIN_KBN_NAME_RYAKU";
                        break;

                    case r_framework.Const.WINDOW_ID.M_HAIKI_SHURUI:
                        cell.PopupGetMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME_RYAKU";                        
                        break;

                    case r_framework.Const.WINDOW_ID.M_HAIKI_NAME:
                        cell.PopupGetMasterField = "HAIKI_NAME_CD,HAIKI_NAME_RYAKU";
                        break;

                    case r_framework.Const.WINDOW_ID.M_NISUGATA:
                        cell.PopupGetMasterField = "NISUGATA_CD,NISUGATA_NAME_RYAKU";
                        break;

                    case r_framework.Const.WINDOW_ID.M_UNIT:
                        cell.PopupGetMasterField = "UNIT_CD,UNIT_NAME_RYAKU";
                        break;

                    case r_framework.Const.WINDOW_ID.M_SHOBUN_HOUHOU:
                        cell.PopupGetMasterField = "SHOBUN_HOUHOU_CD,SHOBUN_HOUHOU_NAME_RYAKU";
                        break;

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
                    if (cell.PopupWindowId == r_framework.Const.WINDOW_ID.M_GENBA)
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

            return base.ProcessDataGridViewKey(e);
        }



        // ---20140114 oonaka add [グリッド機能追加] セルの変更前、変更後の値を保持 start ---
        [Browsable(false)]
        public object CellValidatingOldValue { get; set; }
        [Browsable(false)]
        public object CellValidatingNewValue { get; set; }

        protected override void OnCellValidating(DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                CellValidatingOldValue = this.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue;
                CellValidatingNewValue = e.FormattedValue;
            }
            base.OnCellValidating(e);
        }
        // ---20140114 oonaka add [グリッド機能追加] セルの変更前、変更後の値を保持 end ---
    }
}

