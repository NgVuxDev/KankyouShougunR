using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Utility;

namespace Shougun.Core.Common.KaisyuuHinmeShousai
{
    public partial class UIHeader : HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;
        public UIHeader()
            : base()
        {
            try
            {
                LogUtility.DebugMethodStart();
                InitializeComponent();

                // Load前に非表示にすれば、タイトルは左に詰まる
                base.windowTypeLabel.Visible = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIHeader", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        //画面ロード
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
    }
}
