using System;
using r_framework.Utility;

namespace KyokaShouIchiran
{
    public partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// 
        /// </summary>
        public HeaderForm()
        {
            try
            {
                InitializeComponent();
                base.windowTypeLabel.Visible = false;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("HeaderForm", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                this.ReadDataNumber.Text = "0";
                this.alertNumber.Text = "0";
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("OnLoad", ex);
                throw ex;
            }
        }
    }
}