using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.CustomControl;
using System.Windows.Forms;
using r_framework.Const;
using Shougun.Core.Common.ContenaShitei.DTO;
using r_framework.Utility;
using Shougun.Core.Common.ContenaShitei.DAO;
using System.Data;
using r_framework.Logic;

namespace Shougun.Core.Common.ContenaShitei
{
   public partial class DgvCustom : r_framework.CustomControl.CustomDataGridView //　CustomDataGridView を継承
    {
        // ProcessDataGridViewKeyをoverride
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            var cell = this.CurrentCell as ICustomDataGridControl;
            int rowIndex = this.CurrentRow.Index;

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

                    case r_framework.Const.WINDOW_ID.M_CONTENA_SHURUI:
                        if (cell != null)
                        {
                            var uiForm = this.Parent as UIForm;

                            if (uiForm != null
                                && !uiForm.logic.isSuuryouKanri)
                            {
                                if ("CONTENA_SHURUI_CD_HIKIAGE".Equals(cell.GetName()))
                                {
                                    // 検索条件設定
                                    SearchConditionDto condition = new SearchConditionDto();

                                    condition.GYOUSHA_CD = uiForm.logic.GyoushaCd;
                                    condition.GENBA_CD = uiForm.logic.GenbaCd;

                                    // データ取得 + 設置データ絞込
                                    var dataList = uiForm.logic.GetPutContenaList(condition);

                                    // DataSource設定
                                    var sortedDt = new DataTable();
                                    sortedDt.Columns.Add("CONTENA_SHURUI_CD");
                                    sortedDt.Columns.Add("CONTENA_SHURUI_NAME_RYAKU");

                                    if (dataList != null && dataList.Count() > 0)
                                    {
                                        // GetPutContenaListメソッドはCONTENA_SHURUI_CDとCONTENA_SHURUI_NAME_RYAKUだけ表示すると
                                        // 重複しているように見えるので、重複を削除
                                        var distDataList = dataList.Select(s => new { s.CONTENA_SHURUI_CD, s.CONTENA_SHURUI_NAME_RYAKU }).Distinct().ToArray();

                                        foreach (var data in distDataList)
                                        {
                                            var dispData = sortedDt.NewRow();
                                            dispData["CONTENA_SHURUI_CD"] = data.CONTENA_SHURUI_CD;
                                            dispData["CONTENA_SHURUI_NAME_RYAKU"] = data.CONTENA_SHURUI_NAME_RYAKU;
                                            sortedDt.Rows.Add(dispData);
                                        }
                                    }

                                    cell.PopupDataHeaderTitle = new string[] { "コンテナ種類CD", "コンテナ種類名" };
                                    cell.PopupDataSource = sortedDt;
                                }
                            }
                        }
                        break;

                    case r_framework.Const.WINDOW_ID.M_CONTENA:
                        cell.PopupGetMasterField = "CONTENA_CD,CONTENA_NAME_RYAKU,CONTENA_SHURUI_CD, CONTENA_SHURUI_CD, CONTENA_SHURUI_NAME_RYAKU";
                        if (cell != null)
                        {
                            DgvCustomTextBoxCell cell1 = this.Rows[rowIndex].Cells[2] as DgvCustomTextBoxCell;

                            var uiForm = this.Parent as UIForm;

                            var condition = new SearchConditionDto();

                            var dataTable = new DataTable();
                            dataTable.Columns.Add("CONTENA_CD");
                            dataTable.Columns.Add("CONTENA_NAME_RYAKU");
                            dataTable.Columns.Add("CONTENA_SHURUI_CD");
                            dataTable.Columns.Add("CONTENA_SHURUI_NAME_RYAKU");

                            if (this.Rows[rowIndex].Cells[0].Value != null && !string.IsNullOrEmpty(this.Rows[rowIndex].Cells[0].Value.ToString()))
                            {
                                condition.CONTENA_SHURUI_CD = this.Rows[rowIndex].Cells[0].Value.ToString();
                            }

                            var dataList = new List<SearchResultDto>().ToArray();

                            if ("CONTENA_CD".Equals(cell.GetName()))
                            {
                                //cell1.popupWindowSetting.Clear();
                                //r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
                                //r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                                //if (this.Rows[rowIndex].Cells[0].Value != null &&
                                //    !string.IsNullOrEmpty(this.Rows[rowIndex].Cells[0].Value.ToString()))
                                //{
                                //    string contenaShurui_cd = this.Rows[rowIndex].Cells[0].Value.ToString();
                                //    searchDto.And_Or = CONDITION_OPERATOR.AND;
                                //    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                                //    searchDto.LeftColumn = "CONTENA_SHURUI_CD";
                                //    searchDto.Value = contenaShurui_cd;
                                //    searchDto.ValueColumnType = DB_TYPE.VARCHAR;
                                //    methodDto.Join = JOIN_METHOD.WHERE;
                                //    methodDto.LeftTable = "M_CONTENA";
                                //    methodDto.SearchCondition.Add(searchDto);
                                //    cell1.popupWindowSetting.Add(methodDto);
                                //}
                                //else
                                //{

                                //}

                                // 検索条件設定

                                condition.DENPYOU_DATE = uiForm.logic.denpyouDate;

                                dataList = uiForm.logic.GetContenaList(condition);
                            }
                            else if ("CONTENA_CD_HIKIAGE".Equals(cell.GetName()))
                            {
                                // 検索条件設定
                                condition.GYOUSHA_CD = uiForm.logic.GyoushaCd;
                                condition.GENBA_CD = uiForm.logic.GenbaCd;


                                // データ取得 + 設置データ絞込
                                dataList = uiForm.logic.GetPutContenaList(condition);
                            }

                            if (dataList != null && dataList.Count() > 0)
                            {
                                // 念のため重複を削除
                                var distDataList = dataList.Select(s => new { s.CONTENA_SHURUI_CD, s.CONTENA_SHURUI_NAME_RYAKU, s.CONTENA_CD, s.CONTENA_NAME_RYAKU }).Distinct().OrderBy(o => o.CONTENA_SHURUI_CD).ThenBy(t => t.CONTENA_CD);

                                foreach (var data in distDataList)
                                {
                                    var dispData = dataTable.NewRow();
                                    dispData["CONTENA_CD"] = data.CONTENA_CD;
                                    dispData["CONTENA_NAME_RYAKU"] = data.CONTENA_NAME_RYAKU;
                                    dispData["CONTENA_SHURUI_CD"] = data.CONTENA_SHURUI_CD;
                                    dispData["CONTENA_SHURUI_NAME_RYAKU"] = data.CONTENA_SHURUI_NAME_RYAKU;
                                    dataTable.Rows.Add(dispData);
                                }
                            }

                            cell1.PopupDataHeaderTitle = new string[] { "ｺﾝﾃﾅCD", "ｺﾝﾃﾅ名", "ｺﾝﾃﾅ種類CD", "ｺﾝﾃﾅ種類名" };
                            cell1.PopupDataSource = dataTable;
                        }
                        break;

                    default:
                        break;
                }

                if (this.ReadOnly == true)
                {
                    cell.PopupWindowName = "";
                    cell.PopupWindowId = WINDOW_ID.NONE;
                    cell.FocusOutCheckMethod.Clear();
                }


                // PopupGetMasterField に定義している順番で値の設定先を指定
                // 呼び出し元にGYOUSHA_CD、呼び出し元+1の列にGYOUSHA_NAME_RYAKUが設定される
                if (cell.PopupGetMasterField != null && cell.PopupGetMasterField != "")
                {
                    if (cell.PopupWindowId == r_framework.Const.WINDOW_ID.M_CONTENA)
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
    }
}

