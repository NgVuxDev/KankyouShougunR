using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using Microsoft.VisualBasic;
using r_framework.CustomControl;

namespace Shougun.Core.Allocation.SagyoubiHenkou
{
    public sealed partial class TemplateHaisha : Template
    {
        private GcCustomColumnHeader[] cellsHeader = new GcCustomColumnHeader[ConstClass.DENPYOU_COUNT];
        private GcCustomTextBoxCell[] cellsShurui = new GcCustomTextBoxCell[ConstClass.DENPYOU_COUNT];
        private GcCustomTextBoxCell[] cellsKubun = new GcCustomTextBoxCell[ConstClass.DENPYOU_COUNT];
        private GcCustomTextBoxCell[] cellsTime = new GcCustomTextBoxCell[ConstClass.DENPYOU_COUNT];
        private GcCustomTextBoxCell[] cellsPrintStatus = new GcCustomTextBoxCell[ConstClass.DENPYOU_COUNT];
        private GcCustomTextBoxCell[] cellsMailStatus = new GcCustomTextBoxCell[ConstClass.DENPYOU_COUNT];
        private GcCustomCheckBoxCell[] cellsPrintCheckBox = new GcCustomCheckBoxCell[ConstClass.DENPYOU_COUNT];
        //private GcCustomCheckBoxCell[] cellsMailCheckBox = new GcCustomCheckBoxCell[ConstClass.DENPYOU_COUNT];
        private GcCustomTextBoxCell[] cellsDenpyouContent = new GcCustomTextBoxCell[ConstClass.DENPYOU_COUNT];

        public TemplateHaisha()
        {
            InitializeComponent();

            this.cellsHeader[0] = this.cellHeader01;
            this.cellsShurui[0] = this.cellHaishaShurui01;
            this.cellsKubun[0] = this.cellSagyouDateKubun01;
            this.cellsTime[0] = this.cellGenchakuJikan01;
            this.cellsPrintStatus[0] = this.cellHaishaSijishoStatus01;
            this.cellsPrintCheckBox[0] = this.cellHaishaSijishoCheckBox01;
            this.cellsMailStatus[0] = this.cellEmpty01;
            //this.cellsMailCheckBox[0] = this.cellMailCheckBox01;
            this.cellsDenpyouContent[0] = this.cellDenpyouContent01;

            var wd = this.cellHeader01.Width;
            this.Width = this.rowHeaderCell1.Width + this.columnHeaderCellUntensha.Width + this.columnHeaderCellShashu.Width + wd * ConstClass.DENPYOU_COUNT;   //ThangNguyen [Add] 20150721
            for (int i = 1; i < ConstClass.DENPYOU_COUNT; ++i)
            {
                var n = i + 1;
                var suffix = string.Format("{0:D2}", n);
                Func<string, string> GetNth = s => s.Substring(0, s.Length - 2) + suffix;
                var offset = new Size(wd * i, 0);

                var chc = this.cellHeader01;
                var cellHeader = this.cellsHeader[i] = new GcCustomColumnHeader();
                this.columnHeaderSection1.Cells.Add(cellHeader);
                cellHeader.FlatStyle = chc.FlatStyle;
                cellHeader.Location = chc.Location + offset;
                cellHeader.Name = GetNth(chc.Name);
                cellHeader.Size = chc.Size;
                cellHeader.Style = chc.Style;
                cellHeader.TabIndex = chc.TabIndex + i;
                cellHeader.Value = Strings.StrConv((n).ToString(), VbStrConv.Wide, 0);
                cellHeader.ViewSearchItem = chc.ViewSearchItem;

                var tc = this.cellHaishaShurui01;
                var cellHaishaShurui = this.cellsShurui[i] = new GcCustomTextBoxCell();
                this.Row.Cells.Add(cellHaishaShurui);
                cellHaishaShurui.DataField = GetNth(tc.DataField);
                cellHaishaShurui.DefaultBackColor = tc.DefaultBackColor;
                cellHaishaShurui.DisplayPopUp = tc.DisplayPopUp;
                cellHaishaShurui.FocusOutCheckMethod = tc.FocusOutCheckMethod;
                cellHaishaShurui.IsInputErrorOccured = tc.IsInputErrorOccured;
                cellHaishaShurui.Location = tc.Location + offset;
                cellHaishaShurui.Name = GetNth(tc.Name);
                cellHaishaShurui.PopupSearchSendParams = tc.PopupSearchSendParams;
                cellHaishaShurui.PopupWindowId = tc.PopupWindowId;
                cellHaishaShurui.popupWindowSetting = tc.popupWindowSetting;
                cellHaishaShurui.ReadOnly = tc.ReadOnly;
                cellHaishaShurui.RegistCheckMethod = tc.RegistCheckMethod;
                cellHaishaShurui.Selectable = tc.Selectable;
                cellHaishaShurui.Size = tc.Size;
                cellHaishaShurui.Style = tc.Style;
                cellHaishaShurui.TabIndex = tc.TabIndex + 8;

                tc = this.cellSagyouDateKubun01;
                var cellSagyouDateKubun = this.cellsKubun[i] = new GcCustomTextBoxCell();
                this.Row.Cells.Add(cellSagyouDateKubun);
                cellSagyouDateKubun.DataField = GetNth(tc.DataField);
                cellSagyouDateKubun.DefaultBackColor = tc.DefaultBackColor;
                cellSagyouDateKubun.DisplayPopUp = tc.DisplayPopUp;
                cellSagyouDateKubun.FocusOutCheckMethod = tc.FocusOutCheckMethod;
                cellSagyouDateKubun.IsInputErrorOccured = tc.IsInputErrorOccured;
                cellSagyouDateKubun.Location = tc.Location + offset;
                cellSagyouDateKubun.Name = GetNth(tc.Name);
                cellSagyouDateKubun.PopupSearchSendParams = tc.PopupSearchSendParams;
                cellSagyouDateKubun.PopupWindowId = tc.PopupWindowId;
                cellSagyouDateKubun.popupWindowSetting = tc.popupWindowSetting;
                cellSagyouDateKubun.ReadOnly = tc.ReadOnly;
                cellSagyouDateKubun.RegistCheckMethod = tc.RegistCheckMethod;
                cellSagyouDateKubun.Selectable = tc.Selectable;
                cellSagyouDateKubun.Size = tc.Size;
                cellSagyouDateKubun.Style = tc.Style;
                cellSagyouDateKubun.TabIndex = tc.TabIndex + 8;

                tc = this.cellGenchakuJikan01;
                var cellGenchakuJikan = this.cellsTime[i] = new GcCustomTextBoxCell();
                this.Row.Cells.Add(cellGenchakuJikan);
                cellGenchakuJikan.DataField = GetNth(tc.DataField);
                cellGenchakuJikan.DefaultBackColor = tc.DefaultBackColor;
                cellGenchakuJikan.DisplayPopUp = tc.DisplayPopUp;
                cellGenchakuJikan.FocusOutCheckMethod = tc.FocusOutCheckMethod;
                cellGenchakuJikan.IsInputErrorOccured = tc.IsInputErrorOccured;
                cellGenchakuJikan.Location = tc.Location + offset;
                cellGenchakuJikan.Name = GetNth(tc.Name);
                cellGenchakuJikan.PopupSearchSendParams = tc.PopupSearchSendParams;
                cellGenchakuJikan.PopupWindowId = tc.PopupWindowId;
                cellGenchakuJikan.popupWindowSetting = tc.popupWindowSetting;
                cellGenchakuJikan.ReadOnly = tc.ReadOnly;
                cellGenchakuJikan.RegistCheckMethod = tc.RegistCheckMethod;
                cellGenchakuJikan.Selectable = tc.Selectable;
                cellGenchakuJikan.Size = tc.Size;
                cellGenchakuJikan.Style = tc.Style;
                cellGenchakuJikan.TabIndex = tc.TabIndex + 8;

                tc = this.cellHaishaSijishoStatus01;
                var cellHaishaSijishoStatus = this.cellsPrintStatus[i] = new GcCustomTextBoxCell();
                this.Row.Cells.Add(cellHaishaSijishoStatus);
                cellHaishaSijishoStatus.DataField = GetNth(tc.DataField);
                cellHaishaSijishoStatus.DefaultBackColor = tc.DefaultBackColor;
                cellHaishaSijishoStatus.DisplayPopUp = tc.DisplayPopUp;
                cellHaishaSijishoStatus.FocusOutCheckMethod = tc.FocusOutCheckMethod;
                cellHaishaSijishoStatus.IsInputErrorOccured = tc.IsInputErrorOccured;
                cellHaishaSijishoStatus.Location = tc.Location + offset;
                cellHaishaSijishoStatus.Name = GetNth(tc.Name);
                cellHaishaSijishoStatus.PopupSearchSendParams = tc.PopupSearchSendParams;
                cellHaishaSijishoStatus.PopupWindowId = tc.PopupWindowId;
                cellHaishaSijishoStatus.popupWindowSetting = tc.popupWindowSetting;
                cellHaishaSijishoStatus.ReadOnly = tc.ReadOnly;
                cellHaishaSijishoStatus.RegistCheckMethod = tc.RegistCheckMethod;
                cellHaishaSijishoStatus.Selectable = tc.Selectable;
                cellHaishaSijishoStatus.Size = tc.Size;
                cellHaishaSijishoStatus.Style = tc.Style;
                cellHaishaSijishoStatus.TabIndex = tc.TabIndex + 8;

                var cbc = this.cellHaishaSijishoCheckBox01;
                var cellHaishaSijishoCheckBox = this.cellsPrintCheckBox[i] = new GcCustomCheckBoxCell();
                this.Row.Cells.Add(cellHaishaSijishoCheckBox);
                cellHaishaSijishoCheckBox.CharactersNumber = cbc.CharactersNumber;
                cellHaishaSijishoCheckBox.DefaultBackColor = cbc.DefaultBackColor;
                cellHaishaSijishoCheckBox.DataField = GetNth(cbc.DataField);
                cellHaishaSijishoCheckBox.DisplayItemName = cbc.DisplayItemName;
                cellHaishaSijishoCheckBox.ErrorMessage = cbc.ErrorMessage;
                cellHaishaSijishoCheckBox.FocusOutCheckMethod = cbc.FocusOutCheckMethod;
                cellHaishaSijishoCheckBox.GetCodeMasterField = cbc.GetCodeMasterField;
                cellHaishaSijishoCheckBox.ItemDefinedTypes = cbc.ItemDefinedTypes;
                cellHaishaSijishoCheckBox.Location = cbc.Location + offset;
                cellHaishaSijishoCheckBox.Name = GetNth(cbc.Name);
                cellHaishaSijishoCheckBox.PopupSearchSendParams = cbc.PopupSearchSendParams;
                cellHaishaSijishoCheckBox.PopupWindowId = cbc.PopupWindowId;
                cellHaishaSijishoCheckBox.PopupWindowName = cbc.PopupWindowName;
                cellHaishaSijishoCheckBox.popupWindowSetting = cbc.popupWindowSetting;
                cellHaishaSijishoCheckBox.ReadOnly = cbc.ReadOnly;
                cellHaishaSijishoCheckBox.RegistCheckMethod = cbc.RegistCheckMethod;
                cellHaishaSijishoCheckBox.SearchDisplayFlag = cbc.SearchDisplayFlag;
                cellHaishaSijishoCheckBox.SetFormField = cbc.SetFormField;
                cellHaishaSijishoCheckBox.ShortItemName = cbc.ShortItemName;
                cellHaishaSijishoCheckBox.Size = cbc.Size;
                cellHaishaSijishoCheckBox.Style = cbc.Style;
                cellHaishaSijishoCheckBox.TabIndex = cbc.TabIndex + 8;
                cellHaishaSijishoCheckBox.ZeroPaddengFlag = cbc.ZeroPaddengFlag;
                cellHaishaSijishoCheckBox.CheckAlign = cbc.CheckAlign;

                tc = this.cellEmpty01;
                var cellMailStatus = this.cellsMailStatus[i] = new GcCustomTextBoxCell();
                this.Row.Cells.Add(cellMailStatus);
                cellMailStatus.DataField = GetNth(tc.DataField);
                cellMailStatus.DefaultBackColor = tc.DefaultBackColor;
                cellMailStatus.DisplayPopUp = tc.DisplayPopUp;
                cellMailStatus.FocusOutCheckMethod = tc.FocusOutCheckMethod;
                cellMailStatus.IsInputErrorOccured = tc.IsInputErrorOccured;
                cellMailStatus.Location = tc.Location + offset;
                cellMailStatus.Name = GetNth(tc.Name);
                cellMailStatus.PopupSearchSendParams = tc.PopupSearchSendParams;
                cellMailStatus.PopupWindowId = tc.PopupWindowId;
                cellMailStatus.popupWindowSetting = tc.popupWindowSetting;
                cellMailStatus.ReadOnly = tc.ReadOnly;
                cellMailStatus.RegistCheckMethod = tc.RegistCheckMethod;
                cellMailStatus.Selectable = tc.Selectable;
                cellMailStatus.Size = tc.Size;
                cellMailStatus.Style = tc.Style;
                cellMailStatus.TabIndex = tc.TabIndex + 8;

                /*cbc = this.cellMailCheckBox01;
                var cellMailCheckBox = this.cellsMailCheckBox[i] = new GcCustomCheckBoxCell();
                this.Row.Cells.Add(cellMailCheckBox);
                cellMailCheckBox.CharactersNumber = cbc.CharactersNumber;
                cellMailCheckBox.DefaultBackColor = cbc.DefaultBackColor;
                cellMailCheckBox.DataField = GetNth(cbc.DataField);
                cellMailCheckBox.DisplayItemName = cbc.DisplayItemName;
                cellMailCheckBox.ErrorMessage = cbc.ErrorMessage;
                cellMailCheckBox.FocusOutCheckMethod = cbc.FocusOutCheckMethod;
                cellMailCheckBox.GetCodeMasterField = cbc.GetCodeMasterField;
                cellMailCheckBox.ItemDefinedTypes = cbc.ItemDefinedTypes;
                cellMailCheckBox.Location = cbc.Location + offset;
                cellMailCheckBox.Name = GetNth(cbc.Name);
                cellMailCheckBox.PopupSearchSendParams = cbc.PopupSearchSendParams;
                cellMailCheckBox.PopupWindowId = cbc.PopupWindowId;
                cellMailCheckBox.PopupWindowName = cbc.PopupWindowName;
                cellMailCheckBox.popupWindowSetting = cbc.popupWindowSetting;
                cellMailCheckBox.ReadOnly = cbc.ReadOnly;
                cellMailCheckBox.RegistCheckMethod = cbc.RegistCheckMethod;
                cellMailCheckBox.SearchDisplayFlag = cbc.SearchDisplayFlag;
                cellMailCheckBox.SetFormField = cbc.SetFormField;
                cellMailCheckBox.ShortItemName = cbc.ShortItemName;
                cellMailCheckBox.Size = cbc.Size;
                cellMailCheckBox.Style = cbc.Style;
                cellMailCheckBox.TabIndex = cbc.TabIndex + 8;
                cellMailCheckBox.ZeroPaddengFlag = cbc.ZeroPaddengFlag;
                cellMailCheckBox.Enabled = false;*/

                tc = this.cellDenpyouContent01;
                var cellDenpyouContent = this.cellsDenpyouContent[i] = new GcCustomTextBoxCell();
                this.Row.Cells.Add(cellDenpyouContent);
                cellDenpyouContent.DataField = GetNth(tc.DataField);
                cellDenpyouContent.DefaultBackColor = tc.DefaultBackColor;
                cellDenpyouContent.DisplayPopUp = tc.DisplayPopUp;
                cellDenpyouContent.FocusOutCheckMethod = tc.FocusOutCheckMethod;
                cellDenpyouContent.IsInputErrorOccured = tc.IsInputErrorOccured;
                cellDenpyouContent.Location = tc.Location + offset;
                cellDenpyouContent.Name = GetNth(tc.Name);
                cellDenpyouContent.PopupSearchSendParams = tc.PopupSearchSendParams;
                cellDenpyouContent.PopupWindowId = tc.PopupWindowId;
                cellDenpyouContent.popupWindowSetting = tc.popupWindowSetting;
                cellDenpyouContent.ReadOnly = tc.ReadOnly;
                cellDenpyouContent.RegistCheckMethod = tc.RegistCheckMethod;
                cellDenpyouContent.Selectable = tc.Selectable;
                cellDenpyouContent.Size = tc.Size;
                cellDenpyouContent.Style = tc.Style;
                cellDenpyouContent.TabIndex = tc.TabIndex + 8;
            }
        }
    }
}
