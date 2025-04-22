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
using Shougun.Core.ReceiptPayManagement.ShukkinKeshikomi;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.ReceiptPayManagement.ShukkinKeshikomi
{
    /// <summary>
    /// G752 出金消込一覧
    /// </summary>
    class G752 : r_framework.FormManager.IShougunForm
    {
        public Form CreateForm(params object[] args)
        {
            // 引数args[0] 出金番号
            String strShukkinNum = "";
            if (args.Length > 0 && args[0] != null)
            {
                strShukkinNum = (String)args[0];
            }

            // 引数args[1] 拠点CD
            String strKyotenCd = "";
            if (args.Length > 1 && args[1] != null)
            {
                strKyotenCd = (String)args[1];
            }

            // 引数args[3] 伝票日付
            String strDenpyouHiduke = "";
            if (args.Length > 2 && args[2] != null)
            {
                strDenpyouHiduke = (String)args[2];
            }

            var HeaderForm = new Shougun.Core.ReceiptPayManagement.ShukkinKeshikomi.UIHeader();
            var callForm = new Shougun.Core.ReceiptPayManagement.ShukkinKeshikomi.UIForm(HeaderForm, strShukkinNum, strKyotenCd, strDenpyouHiduke);
            //HeaderForm.form = callForm;
            return new BusinessBaseForm(callForm, HeaderForm);
        }

        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        public void UpdateForm(Form form)
        {
        }

    }
}
