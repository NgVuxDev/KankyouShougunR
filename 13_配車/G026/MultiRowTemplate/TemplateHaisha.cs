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
using r_framework.Configuration;

namespace Shougun.Core.Allocation.HaishaWariateDay
{
    public sealed partial class TemplateHaisha : Template
    {
        private GcCustomColumnHeader[] cellsHeader = new GcCustomColumnHeader[ConstClass.DENPYOU_COUNT];
        private GcCustomTextBoxCell[] cellsShurui = new GcCustomTextBoxCell[ConstClass.DENPYOU_COUNT];
        private GcCustomTextBoxCell[] cellsKubun = new GcCustomTextBoxCell[ConstClass.DENPYOU_COUNT];
        private GcCustomTextBoxCell[] cellsTime = new GcCustomTextBoxCell[ConstClass.DENPYOU_COUNT];
        private GcCustomTextBoxCell[] cellsPrintStatus = new GcCustomTextBoxCell[ConstClass.DENPYOU_COUNT];
        private GcCustomTextBoxCell[] cellsEmpty = new GcCustomTextBoxCell[ConstClass.DENPYOU_COUNT];
        private GcCustomCheckBoxCell[] cellsPrintCheckBox = new GcCustomCheckBoxCell[ConstClass.DENPYOU_COUNT];
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
            this.cellsEmpty[0] = this.cellEmpty01;
            this.cellsDenpyouContent[0] = this.cellDenpyouContent01;

            int buttonMapWidth = 0;
            // MAPBOX連携がONの場合地図表示ボタンの分幅を増やす
            if (AppConfig.AppOptions.IsMAPBOX())
            {
                buttonMapWidth = this.buttonMap.Width;
            }

            var wd = this.cellHeader01.Width;
            this.Width = buttonMapWidth + this.rowHeaderCell1.Width + this.columnHeaderCellUntensha.Width + this.columnHeaderCellShashu.Width + wd * ConstClass.DENPYOU_COUNT;//Sontt 20150707 配車機能
            for (int i = 1; i < ConstClass.DENPYOU_COUNT; ++i)
            {
                var n = i + 1;
                var suffix = string.Format("{0:D2}", n);
                Func<string, string> GetNth = s => s.Substring(0, s.Length - 2) + suffix;

                var offset = new Size(wd * i + buttonMapWidth, 0);
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
                cellHaishaShurui.TabIndex = tc.TabIndex + 7;
                cellHaishaShurui.TabStop = tc.TabStop;

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
                cellSagyouDateKubun.TabIndex = tc.TabIndex + 7;
                cellSagyouDateKubun.TabStop = tc.TabStop;

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
                cellGenchakuJikan.TabIndex = tc.TabIndex + 7;
                cellGenchakuJikan.TabStop = tc.TabStop;

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
                cellHaishaSijishoStatus.TabIndex = tc.TabIndex + 7;
                cellHaishaSijishoStatus.TabStop = tc.TabStop;

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
                cellHaishaSijishoCheckBox.TabIndex = cbc.TabIndex + 7;
                cellHaishaSijishoCheckBox.ZeroPaddengFlag = cbc.ZeroPaddengFlag;
                cellHaishaSijishoCheckBox.CheckAlign = cbc.CheckAlign;
                cellHaishaSijishoCheckBox.Tag = "配車指示書の印刷対象とします";
                cellHaishaSijishoCheckBox.TabStop = cbc.TabStop;

                tc = this.cellEmpty01;
                var cellEmpty = this.cellsEmpty[i] = new GcCustomTextBoxCell();
                this.Row.Cells.Add(cellEmpty);
                cellEmpty.DataField = GetNth(tc.DataField);
                cellEmpty.DefaultBackColor = tc.DefaultBackColor;
                cellEmpty.DisplayPopUp = tc.DisplayPopUp;
                cellEmpty.FocusOutCheckMethod = tc.FocusOutCheckMethod;
                cellEmpty.IsInputErrorOccured = tc.IsInputErrorOccured;
                cellEmpty.Location = tc.Location + offset;
                cellEmpty.Name = GetNth(tc.Name);
                cellEmpty.PopupSearchSendParams = tc.PopupSearchSendParams;
                cellEmpty.PopupWindowId = tc.PopupWindowId;
                cellEmpty.popupWindowSetting = tc.popupWindowSetting;
                cellEmpty.ReadOnly = tc.ReadOnly;
                cellEmpty.RegistCheckMethod = tc.RegistCheckMethod;
                cellEmpty.Selectable = tc.Selectable;
                cellEmpty.Size = tc.Size;
                cellEmpty.Style = tc.Style;
                cellEmpty.TabIndex = tc.TabIndex + 7;
                cellEmpty.TabStop = tc.TabStop;

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
                cellDenpyouContent.TabIndex = tc.TabIndex + 7;
                cellDenpyouContent.TabStop = tc.TabStop;

            }
        }
    }
}
