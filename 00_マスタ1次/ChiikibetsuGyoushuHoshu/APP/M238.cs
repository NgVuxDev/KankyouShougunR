// $Id: M238.cs 12491 2013-12-24 07:34:26Z nagata $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace ChiikibetsuGyoushuHoshu.APP
{
    /// <summary>
    /// M238 地域別業種入力
    /// </summary>
    class M238 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            //引数のデータをlogicにセットしておく
            string CCd = string.Empty;
            if (args.Length > 1)
            {
                CCd = args[1].ToString();
            }

            var callForm = new ChiikibetsuGyoushuHoshuForm(CCd);
            return new MasterBaseForm(callForm, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, true);
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
