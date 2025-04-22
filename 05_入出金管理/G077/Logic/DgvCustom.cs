using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.CustomControl;
using r_framework.Utility;
using System.Data;
using Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.DAO;
using Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku;

namespace ShougunUI.APP
{
    public class DgvCustom1 : r_framework.CustomControl.CustomDataGridView //　CustomDataGridView を継承
    {
        // Popup取引先CD
        public DAOClass_PopupNyushuukkin_Kbn Dao_PopupNyuushukkin_Kbn;

        // CustomDateGridView_KeyUpをoverride
        protected override bool ProcessDataGridViewKey(System.Windows.Forms.KeyEventArgs e)
        {
            // 継承元のイベントは削除
            //base.CustomDateGridView_KeyUp(sender, e);

            var cell = this.CurrentCell as ICustomDataGridControl;

            //cell.Leave = new 

            if (cell != null)
            {
                // 呼び出し機能ごとにPopupGetMasterFieldを設定
                switch (cell.PopupWindowId)
                {
                    case r_framework.Const.WINDOW_ID.M_NYUUSHUKKIN_KBN:
                        cell.PopupGetMasterField = "NYUUSHUKKIN_KBN_CD,NYUUSHUKKIN_KBN_NAME_RYAKU";
                        cell.PopupDataHeaderTitle = new string[] { "コード", "名称" };
                        var customDenpyou_Date = (CustomDateTimePicker)new ControlUtility().FindControl(this.Parent, "DENPYOU_DATE");
                        cell.PopupDataSource = GetPopUpNYUUSHUKKIN_KBNData(cell.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()), customDenpyou_Date.Value.ToString().Substring(0, 10));

                        break;
                    default:
                        break;
                }

                // PopupGetMasterField に定義している順番で値の設定先を指定
                // 呼び出し元にGYOUSHA_CD、呼び出し元+1の列にGYOUSHA_NAME_RYAKUが設定される
                cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, this.CurrentRow.Cells[this.CurrentCell.ColumnIndex + 1] as ICustomDataGridControl };
            }
            // 継承元を呼び出ポップアップを表示させる
            return base.ProcessDataGridViewKey(e);
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列</param>
        /// <returns></returns>
        public DataTable GetPopUpNYUUSHUKKIN_KBNData(IEnumerable<string> displayCol,String dt1)
        {

            this.Dao_PopupNyuushukkin_Kbn = DaoInitUtility.GetComponent<DAOClass_PopupNyushuukkin_Kbn>();

            DTOClass search_PopupNyuushukkin_Kbn = new DTOClass();
            search_PopupNyuushukkin_Kbn.Denpyou_Date =DateTime.Parse(dt1);

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupNyuushukkin_Kbn.GetDataForEntity(search_PopupNyuushukkin_Kbn); ;
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
            //return null;
        }

    }
}