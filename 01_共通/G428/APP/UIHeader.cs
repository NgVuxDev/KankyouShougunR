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
using Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Logic;
using r_framework.Const;

namespace Shougun.Core.Common.TenpyouTankaIkatsuHenkou.APP
{
    public partial class UIHeader : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// システムID
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;


        public UIHeader()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart();
            base.OnLoad(e);
            base.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_DENPYOU_TANKA_IKKATSU);       
            LogUtility.DebugMethodEnd();
        }

        private void alertNumber_Validated(object sender, EventArgs e)
        {
         // 1.「1」以上の数値のみ入力可。
         //「0」を入力された場合、フォーカス移動しない。

            if (string.IsNullOrEmpty(this.alertNumber.Text.ToString()) || !string.IsNullOrEmpty(this.alertNumber.Text.ToString()) && int.Parse(this.alertNumber.Text.Replace(",", "").ToString()) <= 0)
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E002", "アラート件数", "1～99999");
                this.alertNumber.Focus();
            }

           
        }

        


    }
}
