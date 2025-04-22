using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.CustomControl;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;

namespace Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku
{
    public partial class DgvCustom : r_framework.CustomControl.CustomDataGridView //　CustomDataGridView を継承
    {
         public DgvCustom()
        {
            //base.InitializeComponent();
        }

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
                        //if (cell != null)
                        //{
                        //    if ("SaisyuBasyoCd".Equals(cell.GetName()))
                        //    {

                        //        DgvCustomTextBoxCell cell1 = this.Rows[rowIndex].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuBasyoCd] as DgvCustomTextBoxCell;
                        //        cell1.popupWindowSetting.Clear();
                        //        if (this.Rows[rowIndex].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value != null)
                        //        {
                        //            string gyousha_cd = this.Rows[rowIndex].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value.ToString();
                        //            r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
                        //            r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                        //            searchDto.And_Or = CONDITION_OPERATOR.AND;
                        //            searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        //            searchDto.LeftColumn = "GYOUSHA_CD";
                        //            searchDto.Value = gyousha_cd;
                        //            searchDto.ValueColumnType = DB_TYPE.VARCHAR;
                        //            methodDto.Join = JOIN_METHOD.WHERE;
                        //            methodDto.LeftTable = "M_GENBA";
                        //            methodDto.SearchCondition.Add(searchDto);
                        //            cell1.popupWindowSetting.Add(methodDto);

                        //            r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();
                        //            r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                        //            searchDto1.And_Or = CONDITION_OPERATOR.AND;
                        //            searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                        //            searchDto1.LeftColumn = "SHOBUN_JIGYOUJOU_KBN";
                        //            searchDto1.Value = "True";
                        //            searchDto1.ValueColumnType = DB_TYPE.BIT;
                        //            methodDto1.Join = JOIN_METHOD.WHERE;
                        //            methodDto1.LeftTable = "M_GENBA";
                        //            methodDto1.SearchCondition.Add(searchDto1);
                        //            cell1.popupWindowSetting.Add(methodDto1);
                        //        }
                        //        else
                        //        {
                        //            r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();
                        //            r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                        //            searchDto1.And_Or = CONDITION_OPERATOR.AND;
                        //            searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                        //            searchDto1.LeftColumn = "SHOBUN_JIGYOUJOU_KBN";
                        //            searchDto1.Value = "True";
                        //            searchDto1.ValueColumnType = DB_TYPE.BIT;
                        //            methodDto1.Join = JOIN_METHOD.WHERE;
                        //            methodDto1.LeftTable = "M_GENBA";
                        //            methodDto1.SearchCondition.Add(searchDto1);
                        //            cell1.popupWindowSetting.Add(methodDto1);

                        //        }
                        //    }
                        //}
                        break;

                    default:
                        return base.ProcessDataGridViewKey(e);
                }
                // PopupGetMasterField に定義している順番で値の設定先を指定
                // 呼び出し元にGYOUSHA_CD、呼び出し元+1の列にGYOUSHA_NAME_RYAKUが設定される
                if (cell.PopupGetMasterField != null && cell.PopupGetMasterField != "")
                {
                    if (cell.PopupWindowId == r_framework.Const.WINDOW_ID.M_GENBA)
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

            // PopupGetMasterField に定義している順番で値の設定先を指定
            // 呼び出し元にGYOUSHA_CD、呼び出し元+1の列にGYOUSHA_NAME_RYAKUが設定される
            //if (cell.PopupGetMasterField != null && cell.PopupGetMasterField != "")
            //{
            //    cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, this.CurrentRow.Cells[this.CurrentCell.ColumnIndex + 1] as ICustomDataGridControl };
            //}

            var ret = base.ProcessDataGridViewKey(e);

            //終了
            return ret;

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            var ret = base.ProcessDialogKey(keyData);

            //終了
            return ret;
        }
        
        private bool _ForceCheck = false;//エラーの場合、治るまでValidating処理すること。

        /// <summary>
        /// セル検証時処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellValidating(DataGridViewCellValidatingEventArgs e)
        {
            base.OnCellValidating(e);
            if (e.Cancel) return;

            if (!this._ForceCheck && 
                string.Equals(this[e.ColumnIndex, e.RowIndex].EditedFormattedValue == null ? string.Empty : this[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString(),
                this.ValueAtEnter == null ? string.Empty : this.ValueAtEnter.ToString()))
            {
                return; //変更なし
            }

            //禁則文字チェック
            DgvCustomTextBoxCell c = this.Rows[e.RowIndex].Cells[e.ColumnIndex] as DgvCustomTextBoxCell;
            if (c != null && c.ReadOnly == false)
            {
                //フォマット未設定の場合、禁則文字チェックを行う
                if (string.IsNullOrEmpty(c.CustomFormatSetting))
                {
                    object tmpobj = c.EditedFormattedValue;
                    if (tmpobj != null)
                    {
                        if (this.KinsokuMoziCheck(tmpobj.ToString()) == false)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E071", "該当箇所");
                            e.Cancel = true;
                        }
                    }
                }
            }

            bool checkFlg = ((KenpaiManifestoNyuryoku)this.FindForm()).cdgrid_Jisseki_LostFocusCheck(e);

            if (!checkFlg)
            {
                this._ForceCheck = true;//強制チェック必要
                this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                e.Cancel = true;
                return;
            }
            this._ForceCheck = false;//強制チェック不要
        }

        /// <summary>
        /// セルにフォーカスインした際の値（ロスとフォーカス時の比較用）
        /// </summary>
        protected object ValueAtEnter { get; private set; }

        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            //保存
            this.ValueAtEnter = this[e.ColumnIndex, e.RowIndex].FormattedValue;

            base.OnCellBeginEdit(e);
        }
        protected override void OnCellEnter(DataGridViewCellEventArgs e)
        {
            //保存
            this.ValueAtEnter = this[e.ColumnIndex, e.RowIndex].FormattedValue;

            base.OnCellEnter(e);
        }


        /// <summary>
        /// 禁則文字チェック
        /// </summary>
        /// <param name="insertVal">登録項目</param>
        public bool KinsokuMoziCheck(string insertVal)
        {
            Validator v = new Validator();

            if (!v.isJWNetValidShiftJisCharForSign(insertVal))
            {
                return false;
            }
            return true;
        }

    }
}

