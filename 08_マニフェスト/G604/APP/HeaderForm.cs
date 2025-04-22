// $Id: HeaderForm.cs 7176 2013-11-15 09:23:47Z sys_dev_27 $
using System;
using r_framework.APP.Base;
using r_framework.Utility;

namespace Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu
{
    public partial class HeaderForm : HeaderBaseForm
    {
        #region HeaderForm
        public HeaderForm()
        {
            LogUtility.DebugMethodStart();
            try
            {
                InitializeComponent();
                // Load前に非表示にすれば、タイトルは左に詰まる
                //base.windowTypeLabel.Visible = false;
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

        //#region 拠点コード
        ///// <summary>
        ///// 拠点コード
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void KYOTEN_CD_Leave(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);
        //    try
        //    {
        //        int i;
        //        if (!int.TryParse(this.KYOTEN_CD.Text, out i))
        //        {
        //            this.KYOTEN_CD.Text = string.Empty;
        //        }
        //    }
        //    catch
        //    {
        //        throw new r_framework.OriginalException.EdisonException();
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd(sender, e);
        //    }
        //}
        //#endregion
    }
}
