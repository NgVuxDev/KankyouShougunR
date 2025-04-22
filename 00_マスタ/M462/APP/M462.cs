// $Id: M462.cs 31591 2014-10-03 12:55:49Z nagata $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.Master.HikiaiGyousha.APP
{
    /// <summary>
    /// M462 引合業者入力
    /// </summary>
    class M462 : r_framework.FormManager.IShougunForm
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

            bool denshiShinseiFlg = false;
            if (args.Length > 2)
            {
                denshiShinseiFlg = bool.Parse(args[2].ToString());
            }

            var callForm = new UIForm(windowType, gyoushaCd, denshiShinseiFlg);
            return new BusinessBaseForm(callForm, windowType);
        }

        /// <summary>
        /// 同一情報存在問合せ処理
        /// </summary>
        /// <param name="form"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            if (args.Length > 1)
            {
                string gyoushaCd = args[1].ToString();
                var businessForm = form as BusinessBaseForm;
                var uiForm = businessForm.inForm as UIForm;
                if (uiForm.WindowType == (WINDOW_TYPE)args[0]
                    || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    return (uiForm.GYOUSHA_CD.Text.Equals(gyoushaCd));
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
