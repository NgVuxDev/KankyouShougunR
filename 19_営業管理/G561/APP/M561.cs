using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace ShouninzumiDenshiShinseiIchiran.APP
{
    /// <summary>
    /// MXXX 承認済電子申請一覧
    /// </summary>
    class MXXX : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            //DENSHU_KBN denshuKbn = DENSHU_KBN.NYUUKINSAKI;
            //if (args.Length > 0)
            //{
            //    denshuKbn = (DENSHU_KBN)args[0];
            //}

            //var callForm = new ShouninzumiDenshiShinseiIchiran(denshuKbn);
            //var headerForm = new HeaderForm();
            //return new BusinessBaseForm(callForm, headerForm);
            return null;
        }

        /// <summary>
        /// 同一情報存在問合せ処理
        /// </summary>
        /// <param name="form"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        /// <summary>
        /// フォーム更新処理
        /// </summary>
        /// <param name="form"></param>
        public void UpdateForm(Form form)
        {
        }
    }
}
