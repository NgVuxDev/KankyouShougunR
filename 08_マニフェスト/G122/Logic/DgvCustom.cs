using System.Windows.Forms;
using r_framework.Const;
using r_framework.CustomControl;
using Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.Const;

namespace Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu
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

        public DgvCustom()
        {
            //base.InitializeComponent();
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            var cell = this.CurrentCell as ICustomDataGridControl;
            int rowIndex = this.CurrentRow.Index;
            if (cell != null)
            {

                // 呼び出し機能ごとにPopupGetMasterFieldを設定
                switch (cell.PopupWindowId)
                {
                    case r_framework.Const.WINDOW_ID.M_GYOUSHA:
                        // 排出事業者CD
                        if (ConstCls.HST_GYOUSHA_CD.Equals(cell.GetName()))
                        {
                            cell.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";

                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_NAME] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell3 = this.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_POST] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell4 = this.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_TEL] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell5 = this.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_ADDRESS] as DgvCustomTextBoxCell;
                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell3.Name)) cell3.Name = ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell4.Name)) cell4.Name = ((System.Windows.Forms.DataGridViewCell)(cell4)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell5.Name)) cell5.Name = ((System.Windows.Forms.DataGridViewCell)(cell5)).OwningColumn.Name;

                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2, cell3, cell4, cell5 };
                        }
                        // 最終処分の場所（予定）業者CD or 最終処分業者CD
                        else if (ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD.Equals(cell.GetName()))
                        {
                            cell.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
                            // PopupGetMasterField に定義している順番で値の設定先を指定
                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl
                                                        , this.Rows[rowIndex].Cells[this.CurrentCell.ColumnIndex + 1] as ICustomDataGridControl
                                                    };
                        }
                        // 処分受託者CD
                        else if (ConstCls.SBN_GYOUSHA_CD.Equals(cell.GetName()))
                        {
                            cell.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";

                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_NAME] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell3 = this.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_POST] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell4 = this.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_TEL] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell5 = this.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_ADDRESS] as DgvCustomTextBoxCell;
                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell3.Name)) cell3.Name = ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell4.Name)) cell4.Name = ((System.Windows.Forms.DataGridViewCell)(cell4)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell5.Name)) cell5.Name = ((System.Windows.Forms.DataGridViewCell)(cell5)).OwningColumn.Name;

                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2, cell3, cell4, cell5 };
                        }
                        // 処分の受領者CD
                        else if (ConstCls.SBN_JYURYOUSHA_CD.Equals(cell.GetName()))
                        {
                            cell.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
                            // PopupGetMasterField に定義している順番で値の設定先を指定
                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_NAME] as DgvCustomTextBoxCell;
                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                        }
                        // 処分の受託者CD
                        else if (ConstCls.SBN_JYUTAKUSHA_CD.Equals(cell.GetName()))
                        {
                            cell.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";

                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells[ConstCls.SBN_JYUTAKUSHA_NAME] as DgvCustomTextBoxCell;
                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                        }
                        // 区間1：運搬受託者CD
                        else if (ConstCls.UPN_GYOUSHA_CD_1.Equals(cell.GetName()))
                        {
                            cell.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";

                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_1] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell3 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_1] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell4 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_1] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell5 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_1] as DgvCustomTextBoxCell;
                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell3.Name)) cell3.Name = ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell4.Name)) cell4.Name = ((System.Windows.Forms.DataGridViewCell)(cell4)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell5.Name)) cell5.Name = ((System.Windows.Forms.DataGridViewCell)(cell5)).OwningColumn.Name;

                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2, cell3, cell4, cell5 };
                        }
                        // 区間2：運搬受託者CD
                        else if (ConstCls.UPN_GYOUSHA_CD_2.Equals(cell.GetName()))
                        {
                            cell.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";

                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_2] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell3 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_2] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell4 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_2] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell5 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_2] as DgvCustomTextBoxCell;
                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell3.Name)) cell3.Name = ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell4.Name)) cell4.Name = ((System.Windows.Forms.DataGridViewCell)(cell4)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell5.Name)) cell5.Name = ((System.Windows.Forms.DataGridViewCell)(cell5)).OwningColumn.Name;

                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2, cell3, cell4, cell5 };
                        }
                        // 区間1：運搬受託者CD
                        else if (ConstCls.UPN_GYOUSHA_CD_3.Equals(cell.GetName()))
                        {
                            cell.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";

                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_3] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell3 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_3] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell4 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_3] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell5 = this.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_3] as DgvCustomTextBoxCell;
                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell3.Name)) cell3.Name = ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell4.Name)) cell4.Name = ((System.Windows.Forms.DataGridViewCell)(cell4)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell5.Name)) cell5.Name = ((System.Windows.Forms.DataGridViewCell)(cell5)).OwningColumn.Name;

                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2, cell3, cell4, cell5 };
                        }
                        break;
                    case r_framework.Const.WINDOW_ID.M_GENBA:
                        // 排出事業場CD
                        if (ConstCls.HST_GENBA_CD.Equals(cell.GetName()))
                        {
                            string hstGyoushaCd = null;
                            r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                            r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                            r_framework.Dto.SearchConditionsDto searchDto2 = new r_framework.Dto.SearchConditionsDto();
                            r_framework.Dto.PopupSearchSendParamDto sendPar = new r_framework.Dto.PopupSearchSendParamDto();
                            r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
                            r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();

                            for (int i = 0; i < this.ColumnCount; i++)
                            {
                                // 排出事業者CD
                                if (ConstCls.HST_GYOUSHA_CD.Equals(this.Columns[i].Name))
                                {
                                    if (this.Rows[this.CurrentRow.Index].Cells[i].Value != null)
                                    {
                                        hstGyoushaCd = this.Rows[this.CurrentRow.Index].Cells[i].Value.ToString();
                                        break;
                                    }
                                }
                            }

                            if (hstGyoushaCd != null)
                            {
                                searchDto.And_Or = CONDITION_OPERATOR.AND;
                                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                                searchDto.Value = "True";
                                searchDto.ValueColumnType = DB_TYPE.BIT;

                                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                                searchDto1.LeftColumn = "GYOUSHA_CD";
                                searchDto1.Value = hstGyoushaCd;
                                searchDto1.ValueColumnType = DB_TYPE.VARCHAR;

                                methodDto.Join = JOIN_METHOD.WHERE;
                                methodDto.LeftTable = "M_GENBA";
                                methodDto.SearchCondition.Add(searchDto);
                                methodDto.SearchCondition.Add(searchDto1);

                                cell.popupWindowSetting.Clear();
                                cell.popupWindowSetting.Add(methodDto);

                                sendPar.Control = ConstCls.HST_GYOUSHA_CD;
                                sendPar.KeyName = "GYOUSHA_CD";
                                cell.PopupSearchSendParams.Clear();
                                cell.PopupSearchSendParams.Add(sendPar);

                                r_framework.Dto.PopupSearchSendParamDto paramDto = new r_framework.Dto.PopupSearchSendParamDto();
                                paramDto.And_Or = CONDITION_OPERATOR.AND;
                                paramDto.KeyName = "TEKIYOU_BEGIN";
                                paramDto.Control = ConstCls.KOUFU_DATE;
                                cell.PopupSearchSendParams.Add(paramDto);

                            }
                            else
                            {
                                searchDto.And_Or = CONDITION_OPERATOR.AND;
                                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                                searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                                searchDto.Value = "True";
                                searchDto.ValueColumnType = DB_TYPE.BIT;

                                methodDto.Join = JOIN_METHOD.WHERE;
                                methodDto.LeftTable = "M_GENBA";
                                methodDto.SearchCondition.Add(searchDto);

                                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                                searchDto1.LeftColumn = "GYOUSHAKBN_MANI";
                                searchDto1.Value = "True";
                                searchDto1.ValueColumnType = DB_TYPE.BIT;

                                searchDto2.And_Or = CONDITION_OPERATOR.AND;
                                searchDto2.Condition = JUGGMENT_CONDITION.EQUALS;
                                searchDto2.LeftColumn = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                                searchDto2.Value = "True";
                                searchDto2.ValueColumnType = DB_TYPE.BIT;

                                methodDto1.Join = JOIN_METHOD.WHERE;
                                methodDto1.LeftTable = "M_GYOUSHA";
                                methodDto1.SearchCondition.Add(searchDto1);
                                methodDto1.SearchCondition.Add(searchDto2);

                                cell.popupWindowSetting.Clear();
                                cell.popupWindowSetting.Add(methodDto);
                                cell.popupWindowSetting.Add(methodDto1);
                                cell.PopupSearchSendParams.Clear();
                                r_framework.Dto.PopupSearchSendParamDto paramDto = new r_framework.Dto.PopupSearchSendParamDto();
                                paramDto.And_Or = CONDITION_OPERATOR.AND;
                                paramDto.KeyName = "TEKIYOU_BEGIN";
                                paramDto.Control = ConstCls.KOUFU_DATE;
                                cell.PopupSearchSendParams.Add(paramDto);
                            }
                            cell.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU,GENBA_POST,GENBA_TEL,GENBA_ADDRESS1";

                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_CD] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell3 = this.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_NAME] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell4 = this.Rows[rowIndex].Cells[ConstCls.HST_GENBA_NAME] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell5 = this.Rows[rowIndex].Cells[ConstCls.HST_GENBA_POST] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell6 = this.Rows[rowIndex].Cells[ConstCls.HST_GENBA_TEL] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell7 = this.Rows[rowIndex].Cells[ConstCls.HST_GENBA_ADDRESS] as DgvCustomTextBoxCell;
                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell3.Name)) cell3.Name = ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell4.Name)) cell4.Name = ((System.Windows.Forms.DataGridViewCell)(cell4)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell5.Name)) cell5.Name = ((System.Windows.Forms.DataGridViewCell)(cell5)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell6.Name)) cell6.Name = ((System.Windows.Forms.DataGridViewCell)(cell6)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell7.Name)) cell7.Name = ((System.Windows.Forms.DataGridViewCell)(cell7)).OwningColumn.Name;

                            cell.ReturnControls = new[] { cell2, cell3, this.CurrentCell as ICustomDataGridControl, cell4, cell5, cell6, cell7 };
                        }
                        // 最終処分の場所（予定）現場CD
                        else if (ConstCls.LAST_SBN_YOTEI_GENBA_CD.Equals(cell.GetName()))
                        {
                            // 最終処分の場所（予定）業者CD
                            string lastSbnYoteiGyoushaCd = null;
                            r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                            r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                            r_framework.Dto.SearchConditionsDto searchDto2 = new r_framework.Dto.SearchConditionsDto();
                            r_framework.Dto.PopupSearchSendParamDto sendPar = new r_framework.Dto.PopupSearchSendParamDto();
                            r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
                            r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();

                            for (int i = 0; i < this.ColumnCount; i++)
                            {
                                // 最終処分の場所（予定）業者CD
                                if (ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD.Equals(this.Columns[i].Name))
                                {
                                    if (this.Rows[this.CurrentRow.Index].Cells[i].Value != null)
                                    {
                                        lastSbnYoteiGyoushaCd = this.Rows[this.CurrentRow.Index].Cells[i].Value.ToString();
                                        break;
                                    }
                                }
                            }

                            if (lastSbnYoteiGyoushaCd != null)
                            {
                                searchDto.And_Or = CONDITION_OPERATOR.AND;
                                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                                searchDto.LeftColumn = "SAISHUU_SHOBUNJOU_KBN";
                                searchDto.Value = "True";
                                searchDto.ValueColumnType = DB_TYPE.BIT;

                                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                                searchDto1.LeftColumn = "GYOUSHA_CD";
                                searchDto1.Value = lastSbnYoteiGyoushaCd;
                                searchDto1.ValueColumnType = DB_TYPE.VARCHAR;

                                methodDto.Join = JOIN_METHOD.WHERE;
                                methodDto.LeftTable = "M_GENBA";
                                methodDto.SearchCondition.Add(searchDto);
                                methodDto.SearchCondition.Add(searchDto1);

                                cell.popupWindowSetting.Clear();
                                cell.popupWindowSetting.Add(methodDto);

                                sendPar.Control = ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD;
                                sendPar.KeyName = "GYOUSHA_CD";
                                cell.PopupSearchSendParams.Clear();
                                cell.PopupSearchSendParams.Add(sendPar);
                            }
                            else
                            {
                                searchDto.And_Or = CONDITION_OPERATOR.AND;
                                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                                searchDto.LeftColumn = "SAISHUU_SHOBUNJOU_KBN";
                                searchDto.Value = "True";
                                searchDto.ValueColumnType = DB_TYPE.BIT;

                                methodDto.Join = JOIN_METHOD.WHERE;
                                methodDto.LeftTable = "M_GENBA";
                                methodDto.SearchCondition.Add(searchDto);

                                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                                searchDto1.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                                searchDto1.Value = "True";
                                searchDto1.ValueColumnType = DB_TYPE.BIT;

                                searchDto2.And_Or = CONDITION_OPERATOR.AND;
                                searchDto2.Condition = JUGGMENT_CONDITION.EQUALS;
                                searchDto2.LeftColumn = "GYOUSHAKBN_MANI";
                                searchDto2.Value = "True";
                                searchDto2.ValueColumnType = DB_TYPE.BIT;

                                methodDto1.Join = JOIN_METHOD.WHERE;
                                methodDto1.LeftTable = "M_GYOUSHA";
                                methodDto1.SearchCondition.Add(searchDto1);
                                methodDto1.SearchCondition.Add(searchDto2);

                                cell.popupWindowSetting.Clear();
                                cell.popupWindowSetting.Add(methodDto);
                                cell.popupWindowSetting.Add(methodDto1);
                            }

                            cell.PopupGetMasterField = "GYOUSHA_CD,GENBA_CD,GENBA_NAME_RYAKU,GENBA_POST,GENBA_TEL,GENBA_ADDRESS1";

                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell3 = this.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_CD] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell4 = this.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_NAME] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell5 = this.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_POST] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell6 = this.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_TEL] as DgvCustomTextBoxCell;
                            DgvCustomTextBoxCell cell7 = this.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS] as DgvCustomTextBoxCell;
                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell3.Name)) cell3.Name = ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell4.Name)) cell4.Name = ((System.Windows.Forms.DataGridViewCell)(cell4)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell5.Name)) cell5.Name = ((System.Windows.Forms.DataGridViewCell)(cell5)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell6.Name)) cell6.Name = ((System.Windows.Forms.DataGridViewCell)(cell6)).OwningColumn.Name;
                            if (string.IsNullOrEmpty(cell7.Name)) cell7.Name = ((System.Windows.Forms.DataGridViewCell)(cell7)).OwningColumn.Name;

                            cell.ReturnControls = new[] { cell2, cell3, cell4, cell5, cell6, cell7 };
                        }
                        // 最終処分現場CD
                        else if (ConstCls.LAST_SBN_GENBA_CD == cell.GetName())
                        {
                            //// 最終処分現場CD
                            //string lastSbnGyoushaCd = null;
                            //r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                            //r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                            //r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
                            //r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();

                            //for (int i = 0; i < this.ColumnCount; i++)
                            //{
                            //    // 最終処分業者CD
                            //    if (ConstCls.LAST_SBN_GYOUSHA_CD == this.Columns[i].Name)
                            //    {
                            //        if (this.Rows[this.CurrentRow.Index].Cells[i].Value != null)
                            //        {
                            //            lastSbnGyoushaCd = this.Rows[this.CurrentRow.Index].Cells[i].Value.ToString();
                            //            break;
                            //        }
                            //    }
                            //}

                            //if (lastSbnGyoushaCd != null)
                            //{
                            //    searchDto.And_Or = CONDITION_OPERATOR.AND;
                            //    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                            //    searchDto.LeftColumn = "SAISHUU_SHOBUNJOU_KBN";
                            //    searchDto.Value = "True";
                            //    searchDto.ValueColumnType = DB_TYPE.BIT;

                            //    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                            //    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                            //    searchDto1.LeftColumn = "GYOUSHA_CD";
                            //    searchDto1.Value = lastSbnGyoushaCd;
                            //    searchDto1.ValueColumnType = DB_TYPE.VARCHAR;

                            //    methodDto.Join = JOIN_METHOD.WHERE;
                            //    methodDto.LeftTable = "M_GENBA";
                            //    methodDto.SearchCondition.Add(searchDto);
                            //    methodDto.SearchCondition.Add(searchDto1);

                            //    cell.popupWindowSetting.Clear();
                            //    cell.popupWindowSetting.Add(methodDto);
                            //}
                            //else
                            //{
                            //    searchDto.And_Or = CONDITION_OPERATOR.AND;
                            //    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                            //    searchDto.LeftColumn = "SAISHUU_SHOBUNJOU_KBN";
                            //    searchDto.Value = "True";
                            //    searchDto.ValueColumnType = DB_TYPE.BIT;

                            //    methodDto.Join = JOIN_METHOD.WHERE;
                            //    methodDto.LeftTable = "M_GENBA";
                            //    methodDto.SearchCondition.Add(searchDto);

                            //    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                            //    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                            //    searchDto1.LeftColumn = "SHOBUN_JUTAKUSHA_KBN";
                            //    searchDto1.Value = "True";
                            //    searchDto1.ValueColumnType = DB_TYPE.BIT;

                            //    methodDto1.Join = JOIN_METHOD.WHERE;
                            //    methodDto1.LeftTable = "M_GYOUSHA";
                            //    methodDto1.SearchCondition.Add(searchDto1);

                            //    cell.popupWindowSetting.Clear();
                            //    cell.popupWindowSetting.Add(methodDto);
                            //    cell.popupWindowSetting.Add(methodDto1);
                            //}

                            //cell.PopupGetMasterField = "GYOUSHA_CD,GENBA_CD,GENBA_NAME_RYAKU";

                            //DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GYOUSHA_CD] as DgvCustomTextBoxCell;
                            //DgvCustomTextBoxCell cell3 = this.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GENBA_CD] as DgvCustomTextBoxCell;
                            //DgvCustomTextBoxCell cell4 = this.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GENBA_NAME] as DgvCustomTextBoxCell;
                            //if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;
                            //if (string.IsNullOrEmpty(cell3.Name)) cell3.Name = ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;
                            //if (string.IsNullOrEmpty(cell4.Name)) cell4.Name = ((System.Windows.Forms.DataGridViewCell)(cell4)).OwningColumn.Name;

                            //cell.ReturnControls = new[] { cell2, cell3, cell4 };
                        } break;
                    case r_framework.Const.WINDOW_ID.M_SHARYOU:
                        #region 区間1：車輌CD
                        if (ConstCls.SHARYOU_CD_1.Equals(cell.GetName()))
                        {
                            // 区間1：運搬受託者CD
                            string upnGyoushaCd1 = null;
                            // 区間1：車種CD
                            string shashuCd1 = null;
                            r_framework.Dto.PopupSearchSendParamDto searchDto = new r_framework.Dto.PopupSearchSendParamDto();
                            r_framework.Dto.PopupSearchSendParamDto searchDto1 = new r_framework.Dto.PopupSearchSendParamDto();

                            for (int i = 0; i < this.ColumnCount; i++)
                            {
                                // 区間1：運搬受託者CD
                                if (ConstCls.UPN_GYOUSHA_CD_1.Equals(this.Columns[i].Name))
                                {
                                    if (this.Rows[this.CurrentRow.Index].Cells[i].Value != null)
                                    {
                                        upnGyoushaCd1 = this.Rows[this.CurrentRow.Index].Cells[i].Value.ToString();
                                    }
                                }
                                // 区間1：車種CD
                                if (ConstCls.SHASHU_CD_1.Equals(this.Columns[i].Name))
                                {
                                    if (this.Rows[this.CurrentRow.Index].Cells[i].Value != null)
                                    {
                                        shashuCd1 = this.Rows[this.CurrentRow.Index].Cells[i].Value.ToString();
                                    }
                                }
                            }

                            cell.PopupSearchSendParams.Clear();

                            if (upnGyoushaCd1 != null)
                            {
                                searchDto.Control = this.Columns[ConstCls.UPN_GYOUSHA_CD_1].Name;
                                searchDto.KeyName = "key001";

                                cell.PopupSearchSendParams.Add(searchDto);
                            }

                            if (shashuCd1 != null)
                            {
                                searchDto1.Control = this.Columns[ConstCls.SHASHU_CD_1].Name;
                                searchDto1.KeyName = "key003";

                                cell.PopupSearchSendParams.Add(searchDto1);
                            }
                        }
                        // 区間2：車輌CD
                        else if (ConstCls.SHARYOU_CD_2.Equals(cell.GetName()))
                        {
                            // 区間2：運搬受託者CD
                            string upnGyoushaCd2 = null;
                            // 区間2：車種CD
                            string shashuCd2 = null;
                            r_framework.Dto.PopupSearchSendParamDto searchDto = new r_framework.Dto.PopupSearchSendParamDto();
                            r_framework.Dto.PopupSearchSendParamDto searchDto1 = new r_framework.Dto.PopupSearchSendParamDto();

                            for (int i = 0; i < this.ColumnCount; i++)
                            {
                                // 区間2：運搬受託者CD
                                if (ConstCls.UPN_GYOUSHA_CD_2.Equals(this.Columns[i].Name))
                                {
                                    if (this.Rows[this.CurrentRow.Index].Cells[i].Value != null)
                                    {
                                        upnGyoushaCd2 = this.Rows[this.CurrentRow.Index].Cells[i].Value.ToString();
                                    }
                                }
                                // 区間2：車種CD
                                if (ConstCls.SHASHU_CD_2.Equals(this.Columns[i].Name))
                                {
                                    if (this.Rows[this.CurrentRow.Index].Cells[i].Value != null)
                                    {
                                        shashuCd2 = this.Rows[this.CurrentRow.Index].Cells[i].Value.ToString();
                                    }
                                }
                            }

                            cell.PopupSearchSendParams.Clear();
                            if (upnGyoushaCd2 != null)
                            {
                                searchDto.Control = this.Columns[ConstCls.UPN_GYOUSHA_CD_2].Name;
                                searchDto.KeyName = "key001";

                                cell.PopupSearchSendParams.Add(searchDto);
                            }

                            if (shashuCd2 != null)
                            {
                                searchDto1.Control = this.Columns[ConstCls.SHASHU_CD_2].Name;
                                searchDto1.KeyName = "key003";

                                cell.PopupSearchSendParams.Add(searchDto1);
                            }
                        }
                        // 区間3：車輌CD
                        else if (ConstCls.SHARYOU_CD_3.Equals(cell.GetName()))
                        {
                            // 区間3：運搬受託者CD
                            string upnGyoushaCd3 = null;
                            // 区間3：車種CD
                            string shashuCd3 = null;
                            r_framework.Dto.PopupSearchSendParamDto searchDto = new r_framework.Dto.PopupSearchSendParamDto();
                            r_framework.Dto.PopupSearchSendParamDto searchDto1 = new r_framework.Dto.PopupSearchSendParamDto();

                            for (int i = 0; i < this.ColumnCount; i++)
                            {
                                // 区間3：運搬受託者CD
                                if (ConstCls.UPN_GYOUSHA_CD_3.Equals(this.Columns[i].Name))
                                {
                                    if (this.Rows[this.CurrentRow.Index].Cells[i].Value != null)
                                    {
                                        upnGyoushaCd3 = this.Rows[this.CurrentRow.Index].Cells[i].Value.ToString();
                                    }
                                }
                                // 区間3：車種CD
                                if (ConstCls.SHASHU_CD_3.Equals(this.Columns[i].Name))
                                {
                                    if (this.Rows[this.CurrentRow.Index].Cells[i].Value != null)
                                    {
                                        shashuCd3 = this.Rows[this.CurrentRow.Index].Cells[i].Value.ToString();
                                    }
                                }
                            }

                            cell.PopupSearchSendParams.Clear();

                            if (upnGyoushaCd3 != null)
                            {
                                searchDto.Control = this.Columns[ConstCls.UPN_GYOUSHA_CD_3].Name;
                                searchDto.KeyName = "key001";

                                cell.PopupSearchSendParams.Add(searchDto);
                            }

                            if (shashuCd3 != null)
                            {
                                searchDto1.Control = this.Columns[ConstCls.SHASHU_CD_3].Name;
                                searchDto1.KeyName = "key003";

                                cell.PopupSearchSendParams.Add(searchDto1);
                            }
                        }
                        break;
                        #endregion
                    case r_framework.Const.WINDOW_ID.M_HAIKI_SHURUI:

                        // 廃棄物種類CD
                        if (ConstCls.HAIKI_SHURUI_CD_RYAKU.Equals(cell.GetName()))
                        {
                            // マニフェスト種類CD
                            string manifestShuruiCd = null;
                            r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                            r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();

                            for (int i = 0; i < this.ColumnCount; i++)
                            {
                                // マニフェスト種類CD
                                if (ConstCls.MANIFEST_SHURUI_CD.Equals(this.Columns[i].Name))
                                {
                                    if (this.Rows[this.CurrentRow.Index].Cells[i].Value != null)
                                    {
                                        manifestShuruiCd = this.Rows[this.CurrentRow.Index].Cells[i].Value.ToString();
                                        break;
                                    }
                                }
                            }

                            cell.popupWindowSetting.Clear();

                            if (manifestShuruiCd != null)
                            {
                                searchDto.And_Or = CONDITION_OPERATOR.AND;
                                searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                                searchDto.LeftColumn = "HAIKI_KBN_CD";
                                searchDto.Value = manifestShuruiCd;
                                searchDto.ValueColumnType = DB_TYPE.SMALLINT;

                                methodDto.Join = JOIN_METHOD.WHERE;
                                methodDto.LeftTable = "M_HAIKI_SHURUI";
                                methodDto.SearchCondition.Add(searchDto);
                                r_framework.Dto.SearchConditionsDto subDto = new r_framework.Dto.SearchConditionsDto();
                                subDto.And_Or = CONDITION_OPERATOR.AND;
                                subDto.Condition = JUGGMENT_CONDITION.EQUALS;
                                subDto.LeftColumn = "TEKIYOU_FLG";
                                subDto.Value = "FALSE";
                                methodDto.SearchCondition.Add(subDto);

                                cell.popupWindowSetting.Add(methodDto);
                            }
                        }
                        break;
                    //default:
                    //    return base.ProcessDataGridViewKey(e);
                }
            }

            // left keyでの移動か。初期状態(false)の場合のみチェック
            if (!this.isLeftKeyDown)
            {
                this.isLeftKeyDown = e.KeyCode == Keys.Left;
            }

            // 継承元を呼び出ポップアップを表示させる
            return base.ProcessDataGridViewKey(e);
        }

        /// <summary>
        /// セル検証時処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellValidating(DataGridViewCellValidatingEventArgs e)
        {
            //this.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            base.OnCellValidating(e);
            if (e.Cancel) return;

            if (this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                //ロストフォーカスチェック(該当セルが空白時)
                ((UIForm)this.Parent).LostFocusInit(e);
                return;
            }
            //ロストフォーカスチェック(該当セルに値がある場合)
            bool checkFlg = ((UIForm)this.Parent).LostFocusCheck(e);

            if (!checkFlg)
            {
                this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                e.Cancel = true;
                this.EditMode = DataGridViewEditMode.EditOnEnter;
                return;
            }
        }

        /// <summary>
        /// CreateControlイベントハンドラ
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //this.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            this.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        /// <summary>ReadonlyセルはSkip</summary>
        /// <param name="e"></param>
        protected override void OnCellEnter(DataGridViewCellEventArgs e)
        {
            var cell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];

            // マウスクリック時は飛ばない
            if (cell != null && !this.isMouseClick)
            {
                if (cell.ReadOnly)
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
    }
}