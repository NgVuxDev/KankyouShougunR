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
using Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi
{
    class G281 : r_framework.FormManager.IShougunForm // 受入入力
    {
        public Form CreateForm(params object[] args)
        {
           
            var HeaderForm = new Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi.UIHeader();
            var callForm = new Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi.UIForm(HeaderForm);
            return new BusinessBaseForm(callForm, HeaderForm);
        }

        public bool IsSameContentForm(Form form, params object[] args)
        {
            //if (args.Length > 0)
            //{
            //    String strNyuukinNum = (String)args[0];
            //    var footerForm = form as BusinessBaseForm;
            //    var uiForm = footerForm.inForm as Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi.UIForm;
            //    return (uiForm.Nyuukin_CD.Text == strNyuukinNum);
            //}
            return true;
        }

        public void UpdateForm(Form form)
        {
        }

    }
}
