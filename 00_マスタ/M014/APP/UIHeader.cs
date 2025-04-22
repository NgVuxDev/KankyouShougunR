using System;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Master.OboegakiIkkatuHoshu.APP
{
    public partial class UIHeader : HeaderBaseForm
    {
        /// <summary>
        /// システムID
        /// </summary>
        public long SystemId { get; set; }

        public UIHeader()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            base.OnLoad(e);
            base.lb_title.Text = "覚書一括入力";
            LogUtility.DebugMethodEnd();
        }

        private void alertNumber_Validated(object sender, EventArgs e)
        {
            // 1.「1」以上の数値のみ入力可。
            //「0」を入力された場合、フォーカス移動しない。
            if (string.IsNullOrWhiteSpace(this.alertNumber.Text) || int.Parse(this.alertNumber.Text) <= 0)
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E002", "アラート件数", "1～99999");
                this.alertNumber.Focus();
            }
        }
    }
}