using System;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran
{
    class G078 : r_framework.FormManager.IShougunForm
    {
        private Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran.NyuuSyutuKinIchiranForm OyaForm;

        internal NyuuSyutuKinIchiran.LogicClass NyuuSyutuKinLogic;

        /// <summary>
        /// フォームを作成します。
        /// </summary>
        /// <param name="args">フォーム作成時に渡す引数 [0]:伝票種類</param>
        /// <returns>フォームオブジェクト</returns>
        public Form CreateForm(params object[] args)
        {
            var denpyoShurui = String.Empty;
            if (args.Length > 0)
            {
                denpyoShurui = (String)args[0];
            }

            var HeaderForm = new Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran.HeaderForm();
            var callForm = new Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran.NyuuSyutuKinIchiranForm(HeaderForm, denpyoShurui);
            this.OyaForm = callForm;
            var bbf = new BusinessBaseForm(callForm, HeaderForm) { IsInFormResizable = true };

            return bbf;
        }

        public bool IsSameContentForm(Form form, params object[] args)
        {
            //if (args.Length > 0)
            //{
            //String Shain_CD = SystemProperty.Shain.CD;
            //var footerForm = form as BusinessBaseForm;
            //var uiForm = footerForm.inForm as Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran.NyuuSyutuKinIchiranForm;
            //return (uiForm.Shain_Id == Shain_CD);
            //}
            //return false;

            // 常に前面表示
            return true;
        }

        public void UpdateForm(Form form)
        {
            NyuuSyutuKinLogic = OyaForm.NyuuSyutuKinLogic;
            this.NyuuSyutuKinLogic.Search();
        }

    }
}
