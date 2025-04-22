// $Id: HeaderForm.cs 7176 2013-11-15 09:23:47Z sys_dev_27 $
using System;
using r_framework.APP.Base;
using r_framework.Utility;

namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.APP
{
    public partial class F18_G275UIHeaderForm : HeaderBaseForm
    {
        #region HeaderForm
        public F18_G275UIHeaderForm()
        {
            LogUtility.DebugMethodStart();
            try
            {
                InitializeComponent();
                // Load前に非表示にすれば、タイトルは左に詰まる
                base.windowTypeLabel.Visible = false;
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            try
            {
                base.OnLoad(e);
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(e);
            }
        }
        #endregion

        private void F18_G275UIHeaderForm_Load(object sender, EventArgs e)
        {

        }

    }
}
