// $Id: M555.cs 6222 2013-11-07 15:26:04Z gai $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace NyuukinsakiIchiran.APP
{
    /// <summary>
    /// M555 入金先一覧
    /// </summary>
    class M555 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            DENSHU_KBN denshuKbn = DENSHU_KBN.NYUUKINSAKI;
            if (args.Length > 0)
            {
                denshuKbn = (DENSHU_KBN)args[0];
            }

            var callForm = new NyuukinsakiIchiranForm(denshuKbn);
            var headerForm = new HeaderForm();
            return new BusinessBaseForm(callForm, headerForm);
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
