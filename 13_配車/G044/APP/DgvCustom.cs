using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.CustomControl;
using System.Windows.Forms;
using System.Data;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.Dao;

namespace Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei
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
                    case r_framework.Const.WINDOW_ID.M_SHARYOU:
                        cell.PopupGetMasterField = "SHARYOU_CD,SHARYOU_NAME_RYAKU,UNPAN_GYOUSHA_CD,UNPAN_GYOUSHA_NAME,SHASHU_CD,SHASHU_NAME_RYAKU,UNTENSHA_CD,UNTENSHA_NAME_RYAKU";
                        break;

                    case r_framework.Const.WINDOW_ID.M_COURSE_NAME:
                        cell.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU,DAY_NM";

                        // コース情報 ポップアップ取得
                        // 表示用データ取得＆加工
                        var ShainDataTable = ((UIForm)this.FindForm()).CourseNamePopUpDataInit();
                        // TableNameを設定すれば、ポップアップのタイトルになる
                        ShainDataTable.TableName = "コース名選択";

                        // 列名とデータソース設定
                        cell.PopupDataHeaderTitle = new string[] { "コース名称CD", "コース名称", "曜日" };
                        cell.PopupDataSource = ShainDataTable;
                    break;

                    default:
                        break;
                }
                // PopupGetMasterField に定義している順番で値の設定先を指定
                // 呼び出し元にGYOUSHA_CD、呼び出し元+1の列にGYOUSHA_NAME_RYAKUが設定される
                if (cell.PopupGetMasterField != null && cell.PopupGetMasterField != "")
                {
                    if (!r_framework.Const.WINDOW_ID.M_SHARYOU_CLOSED.Equals(cell.PopupWindowId))
                    {
                        cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, this.CurrentRow.Cells[this.CurrentCell.ColumnIndex + 1] as ICustomDataGridControl, this.CurrentRow.Cells["DAY_NM"] as ICustomDataGridControl };
                    }
                    else
                    {
                        cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, 
                                                        this.CurrentRow.Cells["SHARYOU_NAME_RYAKU"] as ICustomDataGridControl, 
                                                        this.CurrentRow.Cells["UNPAN_GYOUSHA_CD"] as ICustomDataGridControl, 
                                                        this.CurrentRow.Cells["UNPAN_GYOUSHA_NAME"] as ICustomDataGridControl, 
                                                        this.CurrentRow.Cells["SHASHU_CD"] as ICustomDataGridControl, 
                                                        this.CurrentRow.Cells["SHASHU_NAME_RYAKU"] as ICustomDataGridControl, 
                                                        this.CurrentRow.Cells["UNTENSHA_CD"] as ICustomDataGridControl, 
                                                        this.CurrentRow.Cells["UNTENSHA_NAME_RYAKU"] as ICustomDataGridControl,
                                                        this.CurrentRow.Cells["DAY_NM"] as ICustomDataGridControl};
                    }
                }
            }

            return base.ProcessDataGridViewKey(e);
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        public DataTable GetPopUpData(IEnumerable<string> displayCol)
        {

                M_COURSE_NAME[] CourseNameAll;
                CourseNameAll = DaoInitUtility.GetComponent<IM_COURSE_NAMEDao>().GetAllValidData(new M_COURSE_NAME());

                if (displayCol.Any(s => s.Length == 0))
                {
                    return new DataTable();
                }

                var dt = EntityUtility.EntityToDataTable(CourseNameAll);
                if (dt.Rows.Count == 0)
                {
                    return dt;
                }

                var sortedDt = new DataTable();
                foreach (var col in displayCol)
                {
                    // 表示対象の列だけを順番に追加
                    sortedDt.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
                }

                foreach (DataRow r in dt.Rows)
                {
                    sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
                }

                return sortedDt;
        }
    }
}
