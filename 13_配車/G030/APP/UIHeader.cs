// $Id: UIHeader.cs 20277 2014-05-07 04:21:26Z y-sato $
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

namespace Shougun.Core.Allocation.TeikiHaishaNyuuryoku
{
    public partial class UIHeader : HeaderBaseForm
    {
        public UIHeader()
        {
            try
            {
                LogUtility.DebugMethodStart();

                InitializeComponent();
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIHeader", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
    }
}
