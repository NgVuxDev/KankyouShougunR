using System;
using System.Collections.Generic;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Entity;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Accessor;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DTO;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate
{
    partial class GcCustomMoveToNextContorol_Cust : GcCustomMultiRow.GcCustomMoveToNextContorol, IAction
    {
        public List<Dictionary<string, TabGoDto>> map = new List<Dictionary<string, TabGoDto>>();

        public DBAccessor accessor { get; set; }

        public new void Execute(GcMultiRow target)
        {
            if (target.CurrentCellPosition.CellIndex < 0)
            {
                SelectionActions.MoveToNextCell.Execute(target);
            }
            //空のグリッドで例外が発生する対応
            if (target.RowCount == 0 || target.CurrentCellPosition.RowIndex >= map.Count)
            {
                //データ無は次のコントロールへ
                ComponentActions.SelectNextControl.Execute(target);
                return;
            }

            Boolean isLastRow = (target.CurrentCellPosition.RowIndex == target.RowCount - 1);

            int tabMaxIndex = 0;
            int currentTabIndex = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].TabIndex;

            string cellName = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].Name;
            string value = Convert.ToString(target.CurrentCell.EditedFormattedValue);
            if (!string.IsNullOrEmpty(value))
            {
                value = value.PadLeft(6, '0');
            }

            if (!map[target.CurrentCellPosition.RowIndex].ContainsKey(cellName))
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
                bool shokuchi = false;
                TabGoDto thisControl = map[target.CurrentCellPosition.RowIndex][cellName];
                switch (cellName)
                {
                    case "mr_TORIHIKISAKI_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = value });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_TORIHIKISAKI_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_TORIHIKISAKI_NAME");
                                thisControl.NextControlName = "mr_TORIHIKISAKI_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_TORIHIKISAKI_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_GYOUSHA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = value });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_GYOUSHA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_GYOUSHA_NAME");
                                thisControl.NextControlName = "mr_GYOUSHA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_GYOUSHA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_GENBA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GENBA_CD = value, GYOUSHA_CD = Convert.ToString(target.CurrentRow.Cells["mr_GYOUSHA_CD"].Value) });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_GENBA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_GENBA_NAME");
                                thisControl.NextControlName = "mr_GENBA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_GENBA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_NIZUMI_GYOUSHA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = value });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_NIZUMI_GYOUSHA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_NIZUMI_GYOUSHA_NAME");
                                thisControl.NextControlName = "mr_NIZUMI_GYOUSHA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_NIZUMI_GYOUSHA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_NIZUMI_GENBA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GENBA_CD = value, GYOUSHA_CD = Convert.ToString(target.CurrentRow.Cells["mr_NIZUMI_GYOUSHA_CD"].Value) });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_NIZUMI_GENBA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_NIZUMI_GENBA_NAME");
                                thisControl.NextControlName = "mr_NIZUMI_GENBA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_NIZUMI_GENBA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_NIOROSHI_GYOUSHA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = value });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_NIOROSHI_GYOUSHA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_NIOROSHI_GYOUSHA_NAME");
                                thisControl.NextControlName = "mr_NIOROSHI_GYOUSHA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_NIOROSHI_GYOUSHA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_NIOROSHI_GENBA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GENBA_CD = value, GYOUSHA_CD = Convert.ToString(target.CurrentRow.Cells["mr_NIOROSHI_GYOUSHA_CD"].Value) });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_NIOROSHI_GENBA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_NIOROSHI_GENBA_NAME");
                                thisControl.NextControlName = "mr_NIOROSHI_GENBA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_NIOROSHI_GENBA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_UNPAN_GYOUSHA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = value });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_UNPAN_GYOUSHA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_UNPAN_GYOUSHA_NAME");
                                thisControl.NextControlName = "mr_UNPAN_GYOUSHA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_UNPAN_GYOUSHA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_SHARYOU_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_SHARYOU() { SHARYOU_CD = value, GYOUSHA_CD = Convert.ToString(target.CurrentRow.Cells["mr_UNPAN_GYOUSHA_CD"].Value) });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_SHARYOU_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_SHARYOU_NAME");
                                thisControl.NextControlName = "mr_SHARYOU_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_SHARYOU_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                }

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
                        // 最後のセル以外のセルでは次のセルへ移動します。 
                        CellPosition currentPos = target.CurrentCellPosition;
                        Cell currentCell = target[currentPos.RowIndex,currentPos.CellIndex];
                        while (currentCell.Name != nextControlName)
                        {
                            SelectionActions.MoveToNextCell.Execute(target);
                            if (currentPos.RowIndex == target.CurrentCellPosition.RowIndex && currentPos.CellIndex == target.CurrentCellPosition.CellIndex)
                            {
                                break;
                            }
                            currentPos = target.CurrentCellPosition;
                            currentCell = target[currentPos.RowIndex, currentPos.CellIndex];
                        }
                        //target.Rows[target.CurrentCellPosition.RowIndex].Cells[nextControlName].Selected = true;
                    }
                }
                else
                {
                    // 最後のセル以外のセルでは次のセルへ移動します。 
                    CellPosition currentPos = target.CurrentCellPosition;
                    Cell currentCell = target[currentPos.RowIndex, currentPos.CellIndex];
                    while (currentCell.Name != nextControlName)
                    {
                        SelectionActions.MoveToNextCell.Execute(target);
                        if (currentPos.RowIndex == target.CurrentCellPosition.RowIndex && currentPos.CellIndex == target.CurrentCellPosition.CellIndex)
                        {
                            break;
                        }
                        currentPos = target.CurrentCellPosition;
                        currentCell = target[currentPos.RowIndex, currentPos.CellIndex];
                    }
                }
            }
        }

        private void AddTabStop(int rowIndex, TabGoDto thisControl, string newName)
        {
            TabGoDto nextControl = map[rowIndex][thisControl.NextControlName];
            nextControl.PreviousControlName = newName;
            TabGoDto newControl = new TabGoDto();
            newControl.ControlName = newName;
            newControl.PreviousControlName = thisControl.ControlName;
            newControl.NextControlName = nextControl.ControlName;
            newControl.isLast = thisControl.isLast;
            newControl.isFirst = false;
            map[rowIndex].Add(newControl.ControlName, newControl);
        }

        private void RemoveTabStop(int rowIndex, TabGoDto thisControl, TabGoDto removeControl)
        {
            TabGoDto nextControl = map[rowIndex][removeControl.NextControlName];
            nextControl.PreviousControlName = thisControl.ControlName;
            map[rowIndex].Remove(removeControl.ControlName);
        }
    }

    partial class GcCustomMoveToPreviousContorol_Cust : GcCustomMultiRow.GcCustomMoveToPreviousContorol, IAction
    {
        public List<Dictionary<string, TabGoDto>> map = new List<Dictionary<string, TabGoDto>>();

        public DBAccessor accessor { get; set; }

        public new void Execute(GcMultiRow target)
        {
            if (target.CurrentCellPosition.CellIndex < 0)
            {
                SelectionActions.MoveToPreviousCell.Execute(target);
            }
            //空のグリッドで例外が発生する対応
            if (target.RowCount == 0 || target.CurrentCellPosition.RowIndex >= map.Count)
            {
                //データ無は次のコントロールへ
                ComponentActions.SelectPreviousControl.Execute(target);
                return;
            }

            Boolean isFirstRow = (target.CurrentCellPosition.RowIndex == 0);

            int currentTabIndex = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].TabIndex;

            int tabMinIndex = target.Template.Row.Cells[target.Template.Row.Cells.Count - 1].TabIndex;
            string cellName = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].Name;
            string value = Convert.ToString(target.CurrentCell.EditedFormattedValue);
            if (!string.IsNullOrEmpty(value))
            {
                value = value.PadLeft(6, '0');
            }

            if (!map[target.CurrentCellPosition.RowIndex].ContainsKey(cellName))
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
                bool shokuchi = false;
                TabGoDto thisControl = map[target.CurrentCellPosition.RowIndex][cellName];
                switch (cellName)
                {
                    case "mr_TORIHIKISAKI_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = value });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_TORIHIKISAKI_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_TORIHIKISAKI_NAME");
                                thisControl.NextControlName = "mr_TORIHIKISAKI_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_TORIHIKISAKI_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_GYOUSHA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = value });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_GYOUSHA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_GYOUSHA_NAME");
                                thisControl.NextControlName = "mr_GYOUSHA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_GYOUSHA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_GENBA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GENBA_CD = value, GYOUSHA_CD = Convert.ToString(target.CurrentRow.Cells["mr_GYOUSHA_CD"].Value) });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_GENBA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_GENBA_NAME");
                                thisControl.NextControlName = "mr_GENBA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_GENBA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_NIZUMI_GYOUSHA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = value });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_NIZUMI_GYOUSHA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_NIZUMI_GYOUSHA_NAME");
                                thisControl.NextControlName = "mr_NIZUMI_GYOUSHA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_NIZUMI_GYOUSHA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_NIZUMI_GENBA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GENBA_CD = value, GYOUSHA_CD = Convert.ToString(target.CurrentRow.Cells["mr_NIZUMI_GYOUSHA_CD"].Value) });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_NIZUMI_GENBA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_NIZUMI_GENBA_NAME");
                                thisControl.NextControlName = "mr_NIZUMI_GENBA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_NIZUMI_GENBA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_NIOROSHI_GYOUSHA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = value });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_NIOROSHI_GYOUSHA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_NIOROSHI_GYOUSHA_NAME");
                                thisControl.NextControlName = "mr_NIOROSHI_GYOUSHA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_NIOROSHI_GYOUSHA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_NIOROSHI_GENBA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GENBA_CD = value, GYOUSHA_CD = Convert.ToString(target.CurrentRow.Cells["mr_NIOROSHI_GYOUSHA_CD"].Value) });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_NIOROSHI_GENBA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_NIOROSHI_GENBA_NAME");
                                thisControl.NextControlName = "mr_NIOROSHI_GENBA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_NIOROSHI_GENBA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_UNPAN_GYOUSHA_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = value });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_UNPAN_GYOUSHA_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_UNPAN_GYOUSHA_NAME");
                                thisControl.NextControlName = "mr_UNPAN_GYOUSHA_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_UNPAN_GYOUSHA_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                    case "mr_SHARYOU_CD":
                        shokuchi = this.accessor.IsShokuchi(new M_SHARYOU() { SHARYOU_CD = value, GYOUSHA_CD = Convert.ToString(target.CurrentRow.Cells["mr_UNPAN_GYOUSHA_CD"].Value) });
                        if (shokuchi)
                        {
                            if (thisControl.NextControlName != "mr_SHARYOU_NAME")
                            {
                                this.AddTabStop(target.CurrentCellPosition.RowIndex, thisControl, "mr_SHARYOU_NAME");
                                thisControl.NextControlName = "mr_SHARYOU_NAME";
                                thisControl.isLast = false;
                            }
                        }
                        else if (thisControl.NextControlName == "mr_SHARYOU_NAME")
                        {
                            TabGoDto nextControl = map[target.CurrentCellPosition.RowIndex][thisControl.NextControlName];
                            this.RemoveTabStop(target.CurrentCellPosition.RowIndex, thisControl, nextControl);
                            thisControl.NextControlName = nextControl.NextControlName;
                            thisControl.isLast = nextControl.isLast;
                        }
                        break;
                }

                string preControlName = thisControl.PreviousControlName;
                if (isFirstRow)
                {
                    if (thisControl.isFirst)
                    {
                        // 最初のセルでは前のコントロールへ移動します。 
                        ComponentActions.SelectPreviousControl.Execute(target);
                    }
                    else
                    {
                        // 最初のセル以外のセルでは前のセルへ移動します。 
                        CellPosition currentPos = target.CurrentCellPosition;
                        Cell currentCell = target[currentPos.RowIndex, currentPos.CellIndex];
                        while (currentCell.Name != preControlName)
                        {
                            SelectionActions.MoveToPreviousCell.Execute(target);
                            if (currentPos.RowIndex == target.CurrentCellPosition.RowIndex && currentPos.CellIndex == target.CurrentCellPosition.CellIndex)
                            {
                                break;
                            }
                            currentPos = target.CurrentCellPosition;
                            currentCell = target[currentPos.RowIndex, currentPos.CellIndex];
                        }
                        //target.Rows[target.CurrentCellPosition.RowIndex].Cells[preControlName].Selected = true;
                    }
                }
                else
                {
                    // 最初のセル以外のセルでは前のセルへ移動します。 
                    CellPosition currentPos = target.CurrentCellPosition;
                    Cell currentCell = target[currentPos.RowIndex, currentPos.CellIndex];
                    while (currentCell.Name != preControlName)
                    {
                        SelectionActions.MoveToPreviousCell.Execute(target);
                        if (currentPos.RowIndex == target.CurrentCellPosition.RowIndex && currentPos.CellIndex == target.CurrentCellPosition.CellIndex)
                        {
                            break;
                        }
                        currentPos = target.CurrentCellPosition;
                        currentCell = target[currentPos.RowIndex, currentPos.CellIndex];
                    }
                }
            }
        }

        private void AddTabStop(int rowIndex, TabGoDto thisControl, string newName)
        {
            TabGoDto nextControl = map[rowIndex][thisControl.NextControlName];
            nextControl.PreviousControlName = newName;
            TabGoDto newControl = new TabGoDto();
            newControl.ControlName = newName;
            newControl.PreviousControlName = thisControl.ControlName;
            newControl.NextControlName = nextControl.ControlName;
            newControl.isLast = thisControl.isLast;
            newControl.isFirst = false;
            map[rowIndex].Add(newControl.ControlName, newControl);
        }

        private void RemoveTabStop(int rowIndex, TabGoDto thisControl, TabGoDto removeControl)
        {
            TabGoDto nextControl = map[rowIndex][removeControl.NextControlName];
            nextControl.PreviousControlName = thisControl.ControlName;
            map[rowIndex].Remove(removeControl.ControlName);
        }
    }
}
