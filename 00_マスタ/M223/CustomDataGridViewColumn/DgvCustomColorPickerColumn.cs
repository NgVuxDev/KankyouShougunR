// $Id:
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Collections.ObjectModel;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Editor;
using r_framework.Logic;
using r_framework.OriginalException;
using r_framework.Utility;
using r_framework.CustomControl;

namespace Shougun.Core.Master.GenchakuJikanHoshu.CustomDataGridViewColumn
{

    public class DataGridViewColourPickerColumn : DgvCustomTextBoxColumn
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataGridViewColourPickerColumn()
        {
            this.CellTemplate = new DataGridViewColourPickerCell();
        }

        ///// <summary>
        ///// DBのフィールド名を指定するプロパティ
        ///// </summary>
        //public string LabelText { get; set; }
        //private bool ShouldSerializeLabelText()
        //{
        //    return this.LabelText != null;
        //}

        ///// <summary>
        ///// コピー処理
        ///// </summary>
        ///// <returns></returns>
        //public override object Clone()
        //{
        //    DataGridViewColourPickerColumn myTextBoxCell = base.Clone() as DataGridViewColourPickerColumn;
        //    myTextBoxCell.LabelText = this.LabelText;
        //    return myTextBoxCell;
        //}

    }
}