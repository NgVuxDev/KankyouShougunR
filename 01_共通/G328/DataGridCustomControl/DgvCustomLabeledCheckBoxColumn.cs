using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Collections.ObjectModel;
using System.ComponentModel;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Editor;
using r_framework.Logic;
using r_framework.OriginalException;
using r_framework.Utility;

namespace Shougun.Core.Common.KaisyuuHinmeShousai
{
    public class DgvCustomLabeledCheckBoxColumn : DgvCustomCheckBoxColumn
	{
        /// <summary>
        /// DBのフィールド名を指定するプロパティ
        /// </summary>
        public string LabelText { get; set; }
        private bool ShouldSerializeLabelText()
        {
            return this.LabelText != null;
        }

        /// <summary>
        /// コピー処理
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            DgvCustomLabeledCheckBoxColumn myTextBoxCell = base.Clone() as DgvCustomLabeledCheckBoxColumn;
            myTextBoxCell.LabelText = this.LabelText;
            return myTextBoxCell;
        }

        public DgvCustomLabeledCheckBoxColumn()
		{
            this.CellTemplate = new DgvCustomLabeledCheckBoxCell();
		}
	}
}
