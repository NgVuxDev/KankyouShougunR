// $Id: M484.cs 54199 2015-07-01 05:01:14Z minhhoang@e-mall.co.jp $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.Master.HikiaiTorihikisakiIchiran.APP
{
    /// <summary>
    /// M484 引合取引先一覧
    /// </summary>
    class M484 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            DENSHU_KBN denshuKbn = DENSHU_KBN.HIKIAI_TORIHIKISAKI;
            if (args.Length > 0)
            {
                denshuKbn = (DENSHU_KBN)args[0];
            }

            var headerForm = new IchiranHeaderForm();
            var callForm = new UIForm(denshuKbn);
            //return new BusinessBaseForm(callForm, headerForm) { IsInFormResizable = true };
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
