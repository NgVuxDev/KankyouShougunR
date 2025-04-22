using System;
using r_framework.APP.Base;
using r_framework.Utility;
using Shougun.Core.Common.DenpyouhimozukePatternIchiran.Const;

namespace Shougun.Core.Common.DenpyouhimozukePatternIchiran.APP
{
    public partial class UIHeader : HeaderBaseForm
    {
        #region UiHeader
        public UIHeader()
        {
            LogUtility.DebugMethodStart();
            try
            {
                InitializeComponent();

                base.windowTypeLabel.Visible = false;
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
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

                base.lb_title.Text = ConstCls.HeaderTitle;
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
