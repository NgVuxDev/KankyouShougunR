// $Id: M309.cs 12491 2013-12-24 07:34:26Z nagata $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;

namespace DenManiJigyoushaHoshu.APP
{
    /// <summary>
    /// M309 事業者入力
    /// </summary>
    class M309 : r_framework.FormManager.IShougunForm
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

            string ediMemberId = string.Empty;
            if (args.Length > 1)
            {
                ediMemberId = args[1].ToString();
            }

            var callForm = new DenManiJigyoushaHoshuForm(windowType, ediMemberId);
            if ((windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG
                    || windowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                    || windowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                && callForm.Search() < 1)
            {
                // 電子事業者マスタは物理削除されるデータのため、存在チェックが必要。
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E045");
                callForm.Dispose();
                return null;
            }

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
                string ediMemberId = args[1].ToString();
                var businessForm = form as BusinessBaseForm;
                var uiForm = businessForm.inForm as DenManiJigyoushaHoshuForm;
                if (uiForm.WindowType == (WINDOW_TYPE)args[0]
                    || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    return (uiForm.EDI_MEMBER_ID.Text.Equals(ediMemberId));
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
