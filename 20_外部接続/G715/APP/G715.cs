using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Utility;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku
{
    /// <summary>
    /// G715 電子契約入力
    /// </summary>
    class G715 : IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (0 < args.Length)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] : 委託契約書のsystemId
            string systemId = null;

            if (1 < args.Length && args[1] != null)
            {
                systemId = args[1] as string;
            }

            // 引数 arg[2] : 電子契約のsystemId
            string denshiSystemId = null;

            if (2 < args.Length && args[2] != null)
            {
                denshiSystemId = args[2] as string;
            }

            var callHeader = new UIHeader();
            var callForm = new UIForm(windowType, systemId, denshiSystemId);
            return new BusinessBaseForm(callForm, callHeader);
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <returns>true：同じ false:異なる</returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            if (2 < args.Length)
            {
                string systemId = args[1].ToString();
                string denshiSystemId = args[2].ToString();
                T_DENSHI_KEIYAKU_KIHON entity = DaoInitUtility.GetComponent<DenshiKeiyakuKihonDAO>().GetDataByCd(systemId, denshiSystemId);
                if (entity != null)
                {
                    var businessForm = form as BusinessBaseForm;
                    var uiForm = businessForm.inForm as UIForm;
                    if (uiForm != null &&
                        (uiForm.WindowType == (WINDOW_TYPE)args[0]
                        || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.UPDATE_WINDOW_FLAG)))
                    {
                        return (uiForm.logic.systemId.Equals(systemId) && uiForm.logic.denshiSystemId.Equals(denshiSystemId));
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name="form"></param>
        /// リスト表示や他の画面で変更される内容を表示している場合は最新の情報を表示すること。
        public void UpdateForm(Form form)
        {
        }
    }
}
