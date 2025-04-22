using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;
using r_framework.Utility;
using OboegakiIkkatuHoshu.Logic;

namespace OboegakiIkkatuHoshu.APP
{
    public partial class OboegakiIkkatuHoshuHeader : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// システムID
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// 覚書一括入力画面ロジック
        /// </summary>
        private OboegakiIkkatuHoshuLogic logic;


        public OboegakiIkkatuHoshuHeader()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart();
            base.OnLoad(e);       
            base.lb_title.Text = "覚書一括入力";         
            LogUtility.DebugMethodEnd();
        }

        private void alertNumber_Validated(object sender, EventArgs e)
        {
         // 1.「1」以上の数値のみ入力可。
         //「0」を入力された場合、フォーカス移動しない。

            if (!string.IsNullOrEmpty(this.alertNumber.Text.ToString())&& int.Parse(this.alertNumber.Text.ToString()) <= 0)
            {
                this.alertNumber.Focus();
            }
        }



    }
}
