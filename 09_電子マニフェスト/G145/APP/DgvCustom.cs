using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using r_framework.CustomControl;
using r_framework.Utility;
using Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.DAO;
using r_framework.Logic;

namespace Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku
{
    public class DgvCustom1 : r_framework.CustomControl.CustomDataGridView //　CustomDataGridView を継承
    {
        // Popup運搬担当者
        public DAOClass_PopupUpnTantou Dao_PopupUpnTantou;

        // Popup報告担当者
        public DAOClass_PopupRepTantou Dao_PopupRepTantou;

        // Popup処分担当者
        public DAOClass_PopupShobunTantou Dao_PopupShobunTantou;

        // Popup運搬単位
        public DAOClass_PopupUpnTani Dao_PopupUpnTani;

        // Popup有価単位
        public DAOClass_PopupYuuTani Dao_PopupYuuTani;

        // Popup車輌
        public DAOClass_PopupSharyo Dao_PopupSharyo;

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
                    case r_framework.Const.WINDOW_ID.M_DENSHI_TANTOUSHA:
                        cell.PopupGetMasterField = "TANTOUSHA_CD,TANTOUSHA_NAME";
                        //cell.PopupDataHeaderTitle = new string[] { "コード", "名称" };
                        if (this.Columns[this.CurrentCell.ColumnIndex].Name == "運搬担当者")
                        {
                            cell.PopupDataSource = GetPopUpUpnTantoData(cell.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()),
                                this.CurrentRow.Cells["UPN_SHA_EDI_MEMBER_ID"].Value.ToString());
                            cell.PopupDataHeaderTitle = new string[] { "運搬担当者CD", "運搬担当者名" };
                            cell.PopupDataSource.TableName = "運搬担当者検索";
                            cell.ReturnControls = new[] { this.CurrentRow.Cells["運搬担当者"] as ICustomDataGridControl, this.CurrentCell as ICustomDataGridControl };
                        }
                        else if (this.Columns[this.CurrentCell.ColumnIndex].Name == "報告担当者")
                        {
                            cell.PopupDataSource = GetPopUpRepTantouData(cell.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()),
                                this.CurrentRow.Cells["SBN_SHA_MEMBER_ID"].Value.ToString());
                            cell.PopupDataHeaderTitle = new string[] { "報告担当者CD", "報告担当者名" };
                            cell.PopupDataSource.TableName = "報告担当者検索";
                            cell.ReturnControls = new[] { this.CurrentRow.Cells["報告担当者"] as ICustomDataGridControl, this.CurrentCell as ICustomDataGridControl };
                        }
                        else 
                        {
                            cell.PopupDataSource = GetPopUpShobunTantouData(cell.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()),
                                this.CurrentRow.Cells["SBN_SHA_MEMBER_ID"].Value.ToString());
                            cell.PopupDataHeaderTitle = new string[] { "処分担当者CD", "処分担当者名" };
                            cell.PopupDataSource.TableName = "処分担当者検索";
                            cell.ReturnControls = new[] { this.CurrentRow.Cells["処分担当者"] as ICustomDataGridControl, this.CurrentCell as ICustomDataGridControl };
                        }
                        break;
                    case r_framework.Const.WINDOW_ID.M_UNIT:

                        cell.PopupGetMasterField = "UNIT_CD,UNIT_NAME_RYAKU";
                        //cell.PopupDataHeaderTitle = new string[] { "コード", "名称" };
                        cell.PopupDataHeaderTitle = new string[] { "単位CD", "単位名" };
                        cell.PopupDataSource = GetPopUpUpnTaniData(cell.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
                        cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, this.CurrentRow.Cells["受入量単位名"] as ICustomDataGridControl };
                        break;
                    case r_framework.Const.WINDOW_ID.M_SHARYOU:
                        cell.PopupGetMasterField = "SHARYOU_CD,SHARYOU_NAME_RYAKU";
                        //cell.PopupDataHeaderTitle = new string[] { "コード", "名称" };
                        cell.PopupDataHeaderTitle = new string[] { "車輌CD", "車輌名" };
                        cell.PopupDataSource = GetPopUpSharyoData(cell.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()),
                            this.CurrentRow.Cells["GYOUSHA_CD"].Value.ToString());
                        cell.ReturnControls = new[] { this.CurrentRow.Cells["車輌"] as ICustomDataGridControl, this.CurrentCell as ICustomDataGridControl };
                        break;

                    default:
                        break;
                }

                // PopupGetMasterField に定義している順番で値の設定先を指定
                // 呼び出し元にGYOUSHA_CD、呼び出し元+1の列にGYOUSHA_NAME_RYAKUが設定される

            }
            // 継承元を呼び出ポップアップを表示させる
            return base.ProcessDataGridViewKey(e);
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(運搬担当者)</param>
        /// <returns></returns>
        public DataTable GetPopUpUpnTantoData(IEnumerable<string> displayCol, string cd)
        {
            LogUtility.DebugMethodStart(displayCol, cd);

            this.Dao_PopupUpnTantou = DaoInitUtility.GetComponent<DAOClass_PopupUpnTantou>();

            TMEDtoCls search_PopupUpnTantou = new TMEDtoCls();
            search_PopupUpnTantou.JIGYOUSHA_CD = cd;

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupUpnTantou.GetDataForEntity(search_PopupUpnTantou);
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

            LogUtility.DebugMethodEnd(displayCol, cd);
            return sortedDt;

            //return null;
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(報告担当者)</param>
        /// <returns></returns>
        public DataTable GetPopUpRepTantouData(IEnumerable<string> displayCol,string cd)
        {
            LogUtility.DebugMethodStart(displayCol, cd);

            this.Dao_PopupRepTantou = DaoInitUtility.GetComponent<DAOClass_PopupRepTantou>();

            TMEDtoCls search_Eikyoutantou = new TMEDtoCls();
            search_Eikyoutantou.JIGYOUSHA_CD = cd;

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupRepTantou.GetDataForEntity(search_Eikyoutantou);
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

            LogUtility.DebugMethodEnd(displayCol, cd);
            return sortedDt;
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(処分担当者)</param>
        /// <returns></returns>
        public DataTable GetPopUpShobunTantouData(IEnumerable<string> displayCol, string cd)
        {
            LogUtility.DebugMethodStart(displayCol, cd);

            this.Dao_PopupShobunTantou = DaoInitUtility.GetComponent<DAOClass_PopupShobunTantou>();

            TMEDtoCls search_Eikyoutantou = new TMEDtoCls();
            search_Eikyoutantou.JIGYOUSHA_CD = cd;
            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupShobunTantou.GetDataForEntity(search_Eikyoutantou);
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

            LogUtility.DebugMethodEnd(displayCol, cd);
            return sortedDt;
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(営業担当者)</param>
        public DataTable GetPopUpUpnTaniData(IEnumerable<string> displayCol)
        {
            LogUtility.DebugMethodStart(displayCol);

            this.Dao_PopupUpnTani = DaoInitUtility.GetComponent<DAOClass_PopupUpnTani>();

            TMEDtoCls search_Eikyoutantou = new TMEDtoCls();

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupUpnTani.GetDataForEntity(search_Eikyoutantou);
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

            LogUtility.DebugMethodEnd(displayCol);
            return sortedDt;
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(営業担当者)</param>
        public DataTable GetPopUpYuuTaniData(IEnumerable<string> displayCol)
        {
            LogUtility.DebugMethodStart(displayCol);

            this.Dao_PopupYuuTani = DaoInitUtility.GetComponent<DAOClass_PopupYuuTani>();

            TMEDtoCls search_Eikyoutantou = new TMEDtoCls();

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupYuuTani.GetDataForEntity(search_Eikyoutantou);
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

            LogUtility.DebugMethodEnd(displayCol);
            return sortedDt;
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(営業担当者)</param>
        public DataTable GetPopUpSharyoData(IEnumerable<string> displayCol, string cd)
        {
            LogUtility.DebugMethodStart(displayCol, cd);

            this.Dao_PopupSharyo = DaoInitUtility.GetComponent<DAOClass_PopupSharyo>();

            TMEDtoCls search_Eikyoutantou = new TMEDtoCls();
            search_Eikyoutantou.JIGYOUSHA_CD = cd;
            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupSharyo.GetDataForEntity(search_Eikyoutantou);
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

            LogUtility.DebugMethodEnd(displayCol, cd);
            return sortedDt;
        }

    }
}