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
    }  
}  

