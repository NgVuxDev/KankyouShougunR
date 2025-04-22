using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using r_framework.Const;
using r_framework.CustomControl;


namespace CustomerControls_Ex
{
    /// <summary>
    /// 共通関数で対応するのでカスタムコントロールは一旦蓋をします
    /// </summary>
    public partial class CustomAlphaNumTextBox_Ex : CustomAlphaNumTextBox
    {
        #region  Properties  
        [Category("EDISON 事業場プロパティ"), Description("GYOUSHA_CD 所属コントローラ名称")]
        public String GyoushaCd_ControlName  { get; set; }

        [Category("EDISON 事業場プロパティ"), Description("業者区分所属業者マスタテーブルのカラム名")]
        public String GyoushaTypeColumnName { get; set; }

        [Category("EDISON 事業場プロパティ"), Description("現場区分所属現場マスタテーブルのカラム名")]  
        public String GenbaTypeColumnName {get;set;}  
      
        #endregion  Properties  
  
        //===============================================================================================  
  
        // Constructor  
        public CustomAlphaNumTextBox_Ex()
            : base()  
        {  
            
        }

        ///// <summary>
        ///// 事業者コントロールのFind
        ///// </summary>
        ///// <param name="target">事業者CDコントロール名</param>
        ///// <returns></returns>
        //private Control FindControl(string target)
        //{
        //    Control ctl = null;
        //    ctl = FindControl(this.Parent, this.GyoushaCd_ControlName);
        //    if (ctl == null){
        //        if (this.Parent == null){
        //            return ctl;
        //        }
        //        else{
        //            ctl = FindControl(this.Parent.Parent, this.GyoushaCd_ControlName);
        //        }
        //        if (ctl == null)
        //        {
        //            if (this.Parent.Parent == null){
        //                return ctl;
        //            }
        //            else{
        //                ctl = FindControl(this.Parent.Parent.Parent, this.GyoushaCd_ControlName);
        //            }
        //            if (ctl==null)
        //            {
        //                if (this.Parent.Parent.Parent == null){
        //                    return ctl;
        //                }
        //                else{
        //                    ctl = FindControl(this.Parent.Parent.Parent.Parent, this.GyoushaCd_ControlName);
        //                }
        //                if (ctl==null){
        //                    if (this.Parent.Parent.Parent.Parent == null){
        //                        return ctl;
        //                    }
        //                    else{
        //                        ctl = FindControl(this.Parent.Parent.Parent.Parent.Parent, this.GyoushaCd_ControlName);
        //                    }
        //                    if (ctl==null){
        //                        return ctl;
        //                    }
        //                }
        //            }
        //        }
                
        //    }
        //    return ctl;
        //}

        ///// <summary>
        ///// Find Control
        ///// </summary>
        ///// <param name="root">root control</param>
        ///// <param name="target">controlName</param>
        ///// <returns></returns>
        //static Control FindControl(Control root, string target)
        //{
        //    if (root == null){
        //        return null;
        //    }
        //    if (root.Name.Equals(target))
        //        return root;
        //    for (var i = 0; i < root.Controls.Count; ++i)
        //    {
        //        if (root.Controls[i].Name.Equals(target))
        //            return root.Controls[i];
        //    }
        //    for (var i = 0; i < root.Controls.Count; ++i)
        //    {
        //        Control result;
        //        for (var k = 0; k < root.Controls[i].Controls.Count; ++k)
        //        {
        //            result = FindControl(root.Controls[i].Controls[k], target);
        //            if (result != null)
        //                return result;
        //        }
        //    }
        //    return null;
        //}      
        //#region  OverrideMethods  
        ///// <summary>
        ///// Enterイベントのオウバーロード
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnEnter(EventArgs e)  
        //{

        //    base.OnEnter(e);  

        //    //基本設定
        //    this.PopupWindowId = WINDOW_ID.M_GENBA;
        //    this.PopupWindowName = "複数キー用検索共通ポップアップ";

        //    //EDISONプロパーティ_チェックの設定
        //    this.FocusOutCheckMethod.Clear();
        //    r_framework.Dto.SelectCheckDto checkDto = new r_framework.Dto.SelectCheckDto();
        //    checkDto.CheckMethodName = "現場マスタコードチェックandセッティング";
        //    checkDto.SendParams = new string[] { this.GyoushaCd_ControlName };
        //    this.FocusOutCheckMethod.Add(checkDto);

        //    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
        //    r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();
        //    r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
        //    r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();

        //    //検索条件の設定
        //    this.popupWindowSetting.Clear();
        //    //業者CDコントロール設定された場合
        //    if (!string.IsNullOrEmpty(this.GyoushaCd_ControlName))
        //    {
        //        Control ctl = FindControl(this.GyoushaCd_ControlName);
        //        CustomAlphaNumTextBox ctlGyousha = (CustomAlphaNumTextBox)ctl;
        //        if (ctlGyousha != null)
        //        {
        //            //関連主キー業者コントロールの設定
        //            ctlGyousha.DBFieldsName = "GYOUSHA_CD";
        //            ctlGyousha.ItemDefinedTypes = "VARCHAR";
        //            //業者CD入力した場合
        //            if (!string.IsNullOrEmpty(ctlGyousha.Text))
        //            {
        //                if (!string.IsNullOrEmpty(this.GenbaTypeColumnName))
        //                {
        //                    searchDto.And_Or = CONDITION_OPERATOR.AND;
        //                    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
        //                    searchDto.LeftColumn = this.GenbaTypeColumnName;
        //                    searchDto.Value = "True";
        //                    searchDto.ValueColumnType = DB_TYPE.BIT;
        //                    methodDto.SearchCondition.Add(searchDto);
        //                }

        //                searchDto1.And_Or = CONDITION_OPERATOR.AND;
        //                searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
        //                searchDto1.LeftColumn = "GYOUSHA_CD";
        //                searchDto1.Value = ctlGyousha.Text;
        //                searchDto1.ValueColumnType = DB_TYPE.VARCHAR;
        //                methodDto.SearchCondition.Add(searchDto1);

        //                methodDto.Join = JOIN_METHOD.WHERE;
        //                methodDto.LeftTable = "M_GENBA";

        //                methodDto.SearchCondition.Add(searchDto);
        //                methodDto.SearchCondition.Add(searchDto1);

        //                this.popupWindowSetting.Clear();
        //                this.popupWindowSetting.Add(methodDto);
        //           }
        //           else//業者CD入力されない場合
        //           {
        //               SetSearchDtoForNotGYOUSHA_CD();
        //           }
        //        }
        //        else//業者CDコントロール見つかりません場合
        //        {
        //            SetSearchDtoForNotGYOUSHA_CD();
        //        }
               
        //    }
        //    else//業者CDコントロール設定され無い場合
        //    {
        //        SetSearchDtoForNotGYOUSHA_CD();
        //    }
           
        //}
        ///// <summary>
        ///// 業者CD設定ない及び業者CDControl名称が設定しない場合、現場検索DTOの設定処理
        ///// </summary>
        //private void SetSearchDtoForNotGYOUSHA_CD()
        //{
        //    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
        //    r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();
        //    r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
        //    r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();

        //    methodDto.Join = JOIN_METHOD.WHERE;
        //    methodDto.LeftTable = "M_GENBA";
        //    methodDto1.Join = JOIN_METHOD.WHERE;
        //    methodDto1.LeftTable = "M_GYOUSHA";

        //    if (!string.IsNullOrEmpty(this.GenbaTypeColumnName))
        //    {
        //        searchDto.And_Or = CONDITION_OPERATOR.AND;
        //        searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
        //        searchDto.LeftColumn = this.GenbaTypeColumnName;
        //        searchDto.Value = "True";
        //        searchDto.ValueColumnType = DB_TYPE.BIT;
        //        methodDto.SearchCondition.Add(searchDto);

        //    }

        //    if (!string.IsNullOrEmpty(this.GyoushaTypeColumnName))
        //    {
        //        searchDto1.And_Or = CONDITION_OPERATOR.AND;
        //        searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
        //        searchDto1.LeftColumn = this.GyoushaTypeColumnName;
        //        searchDto1.Value = "True";
        //        searchDto1.ValueColumnType = DB_TYPE.BIT;
        //        methodDto1.SearchCondition.Add(searchDto1);

        //    }

        //    this.popupWindowSetting.Clear();
        //    this.popupWindowSetting.Add(methodDto);
        //    this.popupWindowSetting.Add(methodDto1);
        //}
      
        //#endregion  OverrideMethods  
    }  
}  

