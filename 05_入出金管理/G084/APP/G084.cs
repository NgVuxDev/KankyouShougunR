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
using Shougun.Core.ReceiptPayManagement.NyukinKeshikomi;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomi
{
    class G084 : r_framework.FormManager.IShougunForm // 受入入力
    {
        public Form CreateForm(params object[] args)
        {
            // 引数args[0] 入金番号
            String strNyuukinNum = "";
            if (args.Length > 0 && args[0] != null)
            {
                strNyuukinNum = (String)args[0];
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

            var HeaderForm = new Shougun.Core.ReceiptPayManagement.NyukinKeshikomi.UIHeader();
            var callForm = new Shougun.Core.ReceiptPayManagement.NyukinKeshikomi.UIForm(HeaderForm, strNyuukinNum, strKyotenCd, strDenpyouHiduke);
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
