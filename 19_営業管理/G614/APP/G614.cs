// $Id: M463.cs 12259 2013-12-20 10:51:32Z gai $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.BusinessManagement.GenbaKakunin.APP
{
    /// <summary>
    /// G614 申請内容確認（現場）
    /// </summary>
    class G614 : r_framework.FormManager.IShougunForm
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

            bool hikiaiGyoushaUseFlg = false;
            if (args.Length > 1)
            {
                hikiaiGyoushaUseFlg = bool.Parse(args[1].ToString());
            }

            string gyoushaCd = string.Empty;
            if (args.Length > 2)
            {
                gyoushaCd = args[2].ToString();
            }

            string genbaCd = string.Empty;
            if (args.Length > 3)
            {
                genbaCd = args[3].ToString();
            }

            bool useKariData = false;
            if (args.Length > 4)
            {
                useKariData = bool.Parse(args[4].ToString());
            }

            var callForm = new UIForm(windowType, hikiaiGyoushaUseFlg, gyoushaCd, genbaCd, useKariData);
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
            if (args.Length > 2)
            {
                string gyoushaCd = args[1].ToString();
                string genbaCd = args[2].ToString();
                var businessForm = form as BusinessBaseForm;
                var uiForm = businessForm.inForm as UIForm;
                if (uiForm.WindowType == (WINDOW_TYPE)args[0]
                    || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    return (uiForm.GyoushaCode.Text.Equals(gyoushaCd) && uiForm.GenbaCode.Text.Equals(genbaCd));
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
