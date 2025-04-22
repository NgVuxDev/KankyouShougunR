using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Utility;

namespace Shougun.Core.ExternalConnection.GenbamemoNyuryoku
{
    /// <summary>
    /// G741 現場メモ入力
    /// </summary>
    class G741 : IShougunForm
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

            // 引数 arg[1] : systemId
            string systemId = null;

            if (1 < args.Length && args[1] != null)
            {
                systemId = args[1] as string;
            }

            // 引数 arg[2] : SEQ
            string SEQ = null;
            if (2 < args.Length && args[2] != null)
            {
                SEQ = args[2] as string;
            }

            // 引数 arg[3] : T_GENBAMEMO_ENTRY
            T_GENBAMEMO_ENTRY entry = null;
            if (3 < args.Length && args[3] != null)
            {
                entry = args[3] as T_GENBAMEMO_ENTRY;
            }

            // 引数 arg[4] : Window_Id
            string winId = string.Empty;
            if (4 < args.Length && args[4] != null)
            {
                winId = args[4] as string;
            }

            // 引数 arg[5] : 現場メモ一覧から複写で開かれたかを連携するフラグ
            string hukushaFlg = string.Empty;
            if (5 < args.Length && args[5] != null)
            {
                hukushaFlg = args[5] as string;
            }

            var callHeader = new UIHeader();
            var callForm = new UIForm(windowType, systemId, SEQ, entry, winId, hukushaFlg);
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
            // 同じ受入番号のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 1)
            {
                string systemId = (string)args[1];
                var baseForm = form as BusinessBaseForm;
                var uiForm = baseForm.inForm as UIForm;
                return (uiForm.SystemId == systemId);
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
