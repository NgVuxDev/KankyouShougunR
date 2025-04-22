using System;
using r_framework.APP.Base;

namespace Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.APP
{
    /// <summary>
    ///
    /// </summary>
    public partial class UIHeaderForm : HeaderBaseForm
    {
        /// <summary>
        ///
        /// </summary>
        public UIHeaderForm()
        {
            this.InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //読込データ件数の初期値設定
            this.ReadDataNumber.Text = "0";
        }
    }
}