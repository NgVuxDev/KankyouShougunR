// $Id: HeaderForm.cs 15978 2014-02-14 05:45:00Z koga $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Utility;
using r_framework.APP.Base;


namespace Shougun.Core.Master.CourseNyuryoku.APP
{
    public partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        public HeaderForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = "コース入力";
            base.windowTypeLabel.Text = "新規";
        }

        /// <summary>
        /// 拠点コード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KYOTEN_CD_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                KYOTEN_CD_TextChanged();
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

        public void KYOTEN_CD_TextChanged()
        {
            LogUtility.DebugMethodStart();

            if (0 < KYOTEN_CD.Text.Trim().Length)
            {
                ((UIForm)((BusinessBaseForm)Parent).inForm).getCourseName();
                ((UIForm)((BusinessBaseForm)Parent).inForm).crearCoureseName();
                // ---20140125 oonaka delete 拠点CDによる業者、現場ﾎﾟｯﾌﾟｱｯﾌﾟの制限を外す start ---
                //((UIForm)((BusinessBaseForm)Parent).inForm).changeGyoushaCd(KYOTEN_CD.Text);
                // ---20140125 oonaka delete 拠点CDによる業者、現場ﾎﾟｯﾌﾟｱｯﾌﾟの制限を外す end ---
            }

            LogUtility.DebugMethodEnd();
        }
    }
}
