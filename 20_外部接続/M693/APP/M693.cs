using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.ExternalConnection.GaibuRenkeiGenbaHoshu.APP
{
    /// <summary>
    /// M693 外部現場入力
    /// </summary>
    class M693 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            string gyoushaCd = string.Empty;
            if (args.Length > 1)
            {
                gyoushaCd = args[1].ToString();
            }

            string genbaCd = string.Empty;
            if (args.Length > 2)
            {
                genbaCd = args[2].ToString();
            }

            var callForm = new UIForm(windowType, gyoushaCd, genbaCd);
            var headerFrom = new HeaderForm();

            return new BusinessBaseForm(callForm, headerFrom);
        }

        /// <summary>
        /// 同一情報存在問合せ処理
        /// </summary>
        /// <param name="form"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            if (args.Length > 2)
            {
                string gyoushaCd = args[1].ToString();
                string genbaCd = args[2].ToString();
                var businessForm = form as BusinessBaseForm;
                var uiForm = businessForm.inForm as UIForm;
                if (uiForm.WindowType == (WINDOW_TYPE)args[0]
                    || (uiForm.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG && (WINDOW_TYPE)args[0] != WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return (uiForm.GYOUSHA_CD.Text.Equals(gyoushaCd) && uiForm.GENBA_CD.Text.Equals(genbaCd));
                }
            }
            return false;
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
