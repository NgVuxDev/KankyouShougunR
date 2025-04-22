// $Id: G612.cs 12067 2013-12-19 11:21:15Z gai $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.BusinessManagement.TorihikisakiKakunin.APP
{
    /// <summary>
    /// G612 申請内容確認（取引先）
    /// </summary>
    class G612 : r_framework.FormManager.IShougunForm
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

            string torihikisakiCd = string.Empty;
            if (args.Length > 1)
            {
                torihikisakiCd = args[1].ToString();
            }

            string hikiaiFlg = string.Empty;
            if (args.Length > 2)
            {
                hikiaiFlg = args[2].ToString();
            }

            var callForm = new UIForm(windowType, torihikisakiCd, hikiaiFlg);
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
                string torihikisakiCd = args[1].ToString();
                string hikiaiFlg = args[2].ToString();
                var businessForm = form as BusinessBaseForm;
                var uiForm = businessForm.inForm as UIForm;
                if (uiForm.WindowType == (WINDOW_TYPE)args[0]
                    || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    return (uiForm.TORIHIKISAKI_CD.Text.Equals(torihikisakiCd));
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
