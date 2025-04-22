using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Shougun.Core.Master.SystemKobetsuSetteiHoshu.APP;

namespace Shougun.Core.Master.SystemKobetsuSetteiHoshu
{
    class M688 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            var callForm = new UIForm();
            return new MasterBaseForm(callForm, WINDOW_TYPE.NONE, false);
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
