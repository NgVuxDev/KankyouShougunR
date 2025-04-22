// $Id: M462.cs 12067 2013-12-19 11:21:15Z gai $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.BusinessManagement.GyoushaKakunin.APP
{
    /// <summary>
    /// G613 申請内容確認（業者）
    /// </summary>
    class G613 : r_framework.FormManager.IShougunForm
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

            int gyosha_kbn = 0;
            if (args.Length > 2)
            {
                gyosha_kbn = (int)args[2];
            }
            var callForm = new UIForm(windowType, gyoushaCd, gyosha_kbn);
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
                var businessForm = form as BusinessBaseForm;
                var uiForm = businessForm.inForm as UIForm;
                return (uiForm.GYOUSHA_CD.Text.Equals(gyoushaCd));
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
