using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace KyokaShouIchiran.APP
{
    internal class M748 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            DENSHU_KBN denshuKbn = DENSHU_KBN.KYOKASHOU_ICHIRAN;
            if (args.Length > 1)
            {
                denshuKbn = (DENSHU_KBN)args[0];
            }
            var headerForm = new HeaderForm();
            var callForm = new KyokaShouIchiranForm(denshuKbn, headerForm);
            var businessForm = new BusinessBaseForm(callForm, headerForm);
            return businessForm;
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