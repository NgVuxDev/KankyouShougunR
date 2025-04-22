using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using r_framework.CustomControl;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;
using System.Data;

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake.APP
{
    public partial class DgvCustom : r_framework.CustomControl.CustomDataGridView
    {
        public DgvCustom()
        {
        }

        public DgvCustom(IContainer container)
            : base(container)
        {
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            var parentForm = this.Parent as UIForm;
            var DsMasterLogic = new DenshiMasterDataLogic();
            var dto = new DenshiSearchParameterDtoCls();
            var cell = this.CurrentCell as ICustomDataGridControl;

            if (cell != null)
            {
                string columnName = cell.GetName();
                if (parentForm.logic.CELL_NAME_HAIKI_SHURUI_CD.Equals(columnName))
                {
                    dto.EDI_MEMBER_ID = parentForm.logic.dt_R18.HST_SHA_EDI_MEMBER_ID;
                    cell.PopupDataHeaderTitle = new string[] { "廃棄物種類CD", "廃棄物種類名" };
                    cell.PopupGetMasterField = "HAIKISHURUICD,HAIKI_SHURUI_NAME";
                    cell.PopupSetFormField = parentForm.logic.CELL_NAME_HAIKI_SHURUI_CD + ", " + parentForm.logic.CELL_NAME_HAIKI_SHURUI_NAME;
                    cell.PopupDataSource = DsMasterLogic.GetDenshiHaikiShuruiData(dto);
                    cell.PopupDataSource.TableName = "電子廃棄物種類";
                }
                else if (parentForm.logic.CELL_NAME_HAIKI_NAME_CD.Equals(columnName))
                {
                    dto.EDI_MEMBER_ID = parentForm.logic.dt_R18.HST_SHA_EDI_MEMBER_ID;
                    cell.PopupDataHeaderTitle = new string[] { "廃棄物名称CD", "廃棄物名称" };
                    cell.PopupGetMasterField = "HAIKI_NAME_CD,HAIKI_NAME";
                    cell.PopupSetFormField = parentForm.logic.CELL_NAME_HAIKI_NAME_CD + ", " + parentForm.logic.CELL_NAME_HAIKI_NAME_NAME;
                    cell.PopupDataSource = DsMasterLogic.GetDenshiHaikiNameData(dto);
                    cell.PopupDataSource.TableName = "電子廃棄物名称";
                }
                else if (parentForm.logic.CELL_NAME_SBN_ENDREP_KBN.Equals(columnName))
                {
                    cell.PopupDataHeaderTitle = new string[] { "区分CD", "区分名" };
                    cell.PopupGetMasterField = "KBN_CD,KBN_NAME";
                    cell.PopupSetFormField = parentForm.logic.CELL_NAME_SBN_ENDREP_KBN + ", " + parentForm.logic.CELL_NAME_SBN_ENDREP_KBN_NAME;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("KBN_CD");
                    dt.Columns.Add("KBN_NAME");
                    var row1 = dt.NewRow();
                    row1[0] = "1";
                    row1[1] = "中間";
                    var row2 = dt.NewRow();
                    row2[0] = "2";
                    row2[1] = "最終";
                    dt.Rows.Add(row1);
                    dt.Rows.Add(row2);
                    cell.PopupDataSource = dt;
                    cell.PopupDataSource.TableName = "区分";
                }
            }

            return base.ProcessDataGridViewKey(e);
        }
    }
}
