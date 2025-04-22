using System;
using System.Collections.Generic;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Entity;
using Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.Accessor;
using Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.DTO;

namespace Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate
{
    partial class GcCustomMoveToNextContorol_Cust : GcCustomMultiRow.GcCustomMoveToNextContorol, IAction
    {
        public Dictionary<string, TabGoDto> map = new Dictionary<string, TabGoDto>();

        public DBAccessor accessor { get; set; }

        public new void Execute(GcMultiRow target)
        {
            if (target.CurrentCellPosition.CellIndex < 0)
            {
                SelectionActions.MoveToNextCell.Execute(target);
            }
            //空のグリッドで例外が発生する対応
            if (target.RowCount == 0)
            {
                //データ無は次のコントロールへ
                ComponentActions.SelectNextControl.Execute(target);
                return;
            }

            Boolean isLastRow = (target.CurrentCellPosition.RowIndex == target.RowCount - 1);

            int tabMaxIndex = 0;
            int currentTabIndex = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].TabIndex;

            string cellName = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].Name;
            string value = Convert.ToString(target.CurrentCell.EditedFormattedValue).PadLeft(6, '0');

            if (!map.ContainsKey(cellName))
            {
                if (isLastRow)
                {
                    for (int i = 0; i < target.Template.Row.Cells.Count; i++)
                    {
                        if (target.Template.Row.Cells[i].TabStop)
                        {
                            if (tabMaxIndex < target.Template.Row.Cells[i].TabIndex)
                            {
                                tabMaxIndex = target.Template.Row.Cells[i].TabIndex;
                            }
                        }
                    }

                    if (tabMaxIndex <= currentTabIndex)
                    {
                        // 最後のセルでは次のコントロールへ移動します。 
                        ComponentActions.SelectNextControl.Execute(target);
                    }
                    else
                    {
                        // 最後のセル以外のセルでは次のセルへ移動します。 
                        SelectionActions.MoveToNextCell.Execute(target);
                    }
                }
                else
                {
                    // 最後のセル以外のセルでは次のセルへ移動します。 
                    SelectionActions.MoveToNextCell.Execute(target);
                }
            }
            else
            {
                TabGoDto thisControl = map[cellName];

                string nextControlName = thisControl.NextControlName;
                if (isLastRow)
                {
                    if (thisControl.isLast)
                    {
                        // 最後のセルでは次のコントロールへ移動します。 
                        ComponentActions.SelectNextControl.Execute(target);
                    }
                    else
                    {
                        if (thisControl.ControlName == "mr_HINMEI_CD" || thisControl.ControlName == "mr_TANKA")
                        {
                            SelectionActions.MoveToNextCell.Execute(target);
                        }
                        else
                        {
                            CellPosition currentPos = target.CurrentCellPosition;
                            int nextIndex = target.Template.Row.Cells[thisControl.NextControlName].CellIndex;
                            target.Rows[currentPos.RowIndex].Cells[nextIndex - 1].Selected = true;
                            if (currentPos != target.CurrentCellPosition)
                            {
                                SelectionActions.MoveToNextCell.Execute(target);
                            }
                        }
                    }
                }
                else
                {
                    if (thisControl.isLast)
                    {
                        CellPosition currentPos = target.CurrentCellPosition;
                        target.Rows[currentPos.RowIndex + 1].Cells["CHECK"].Selected = true;
                    }
                    else
                    {
                        if (thisControl.ControlName == "mr_HINMEI_CD" || thisControl.ControlName == "mr_TANKA")
                        {
                            SelectionActions.MoveToNextCell.Execute(target);
                        }
                        else
                        {
                            CellPosition currentPos = target.CurrentCellPosition;
                            int nextIndex = target.Template.Row.Cells[thisControl.NextControlName].CellIndex;
                            target.Rows[currentPos.RowIndex].Cells[nextIndex - 1].Selected = true;
                            if (currentPos != target.CurrentCellPosition)
                            {
                                SelectionActions.MoveToNextCell.Execute(target);
                            }
                        }
                    }
                }
            }
        }
    }

    partial class GcCustomMoveToPreviousContorol_Cust : GcCustomMultiRow.GcCustomMoveToPreviousContorol, IAction
    {
        public Dictionary<string, TabGoDto> map = new Dictionary<string, TabGoDto>();

        public DBAccessor accessor { get; set; }

        public new void Execute(GcMultiRow target)
        {
            if (target.CurrentCellPosition.CellIndex < 0)
            {
                SelectionActions.MoveToPreviousCell.Execute(target);
            }
            //空のグリッドで例外が発生する対応
            if (target.RowCount == 0)
            {
                //データ無は次のコントロールへ
                ComponentActions.SelectPreviousControl.Execute(target);
                return;
            }

            Boolean isFirstRow = (target.CurrentCellPosition.RowIndex == 0);

            int currentTabIndex = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].TabIndex;

            int tabMinIndex = target.Template.Row.Cells[target.Template.Row.Cells.Count - 1].TabIndex;
            string cellName = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].Name;
            string value = Convert.ToString(target.CurrentCell.EditedFormattedValue).PadLeft(6, '0');

            if (!map.ContainsKey(cellName))
            {
                if (isFirstRow)
                {
                    for (int i = target.Template.Row.Cells.Count - 1; i >= 0; i--)
                    {
                        if (target.Template.Row.Cells[i].TabStop)
                        {
                            if (tabMinIndex > target.Template.Row.Cells[i].TabIndex)
                            {
                                tabMinIndex = target.Template.Row.Cells[i].TabIndex;
                            }
                        }
                    }

                    if (tabMinIndex >= currentTabIndex)
                    {
                        // 最後のセルでは次のコントロールへ移動します。 
                        ComponentActions.SelectPreviousControl.Execute(target);
                    }
                    else
                    {
                        // 最後のセル以外のセルでは次のセルへ移動します。 
                        SelectionActions.MoveToPreviousCell.Execute(target);
                    }
                }
                else
                {
                    // 最後のセル以外のセルでは次のセルへ移動します。 
                    SelectionActions.MoveToPreviousCell.Execute(target);
                }
            }
            else
            {
                TabGoDto thisControl = map[cellName];

                string preControlName = thisControl.PreviousControlName;
                if (isFirstRow)
                {
                    if (thisControl.isFirst)
                    {
                        // 最後のセルでは次のコントロールへ移動します。 
                        ComponentActions.SelectPreviousControl.Execute(target);
                    }
                    else
                    {
                        if (thisControl.ControlName == "mr_HINMEI_NAME" || thisControl.ControlName == "mr_KINGAKU")
                        {
                            SelectionActions.MoveToPreviousCell.Execute(target);
                        }
                        else if (preControlName == "CHECK")
                        {
                            CellPosition currentPos = target.CurrentCellPosition;
                            target.Rows[currentPos.RowIndex].Cells["CHECK"].Selected = true;
                        }
                        else
                        {
                            CellPosition currentPos = target.CurrentCellPosition;
                            int preIndex = target.Template.Row.Cells[thisControl.PreviousControlName].CellIndex;
                            target.Rows[currentPos.RowIndex].Cells[preIndex - 1].Selected = true;
                            if (currentPos != target.CurrentCellPosition)
                            {
                                SelectionActions.MoveToNextCell.Execute(target);
                            }
                        }
                    }
                }
                else
                {
                    if (thisControl.isFirst)
                    {
                        CellPosition currentPos = target.CurrentCellPosition;
                        int preIndex = target.Template.Row.Cells[thisControl.PreviousControlName].CellIndex;
                        target.Rows[currentPos.RowIndex - 1].Cells[preIndex - 1].Selected = true;
                        if (currentPos != target.CurrentCellPosition)
                        {
                            SelectionActions.MoveToNextCell.Execute(target);
                        }
                    }
                    else
                    {
                        if (thisControl.ControlName == "mr_HINMEI_NAME" || thisControl.ControlName == "mr_KINGAKU")
                        {
                            SelectionActions.MoveToPreviousCell.Execute(target);
                        }
                        else if (preControlName == "CHECK")
                        {
                            CellPosition currentPos = target.CurrentCellPosition;
                            target.Rows[currentPos.RowIndex].Cells["CHECK"].Selected = true;
                        }
                        else
                        {
                            CellPosition currentPos = target.CurrentCellPosition;
                            int preIndex = target.Template.Row.Cells[thisControl.PreviousControlName].CellIndex;
                            target.Rows[currentPos.RowIndex].Cells[preIndex - 1].Selected = true;
                            if (currentPos != target.CurrentCellPosition)
                            {
                                SelectionActions.MoveToNextCell.Execute(target);
                            }
                        }
                    }
                }
            }
        }
    }
}
