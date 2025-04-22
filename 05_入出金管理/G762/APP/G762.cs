using System;
using System.Windows.Forms;
using r_framework.Const;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;

namespace ShukkinDataShutsuryoku
{
    /// <summary>
    /// 出金データ出力
    /// </summary>
    class G762 : r_framework.FormManager.IShougunForm
    {
        public Form CreateForm(params object[] args)
        {           
            var headerForm = new UIHeader();
            var callForm = new UIForm();
            return new BusinessBaseForm(callForm, headerForm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        public void UpdateForm(Form form)
        {
        }

    }
}
