using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.FormManager;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku
{
    /// <summary>
    /// G695 配送計画入力
    /// </summary>
    class G695 : IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (0 < args.Length)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            string systemId = null;
            if (1 < args.Length)
            {
                systemId = args[1].ToString();
            }

            bool f4BtnUnLook = false;
            if (2 < args.Length)
            {
                f4BtnUnLook = bool.Parse(args[2].ToString());
            }

            var callForm = new UIForm(windowType, systemId, f4BtnUnLook);
            var callHeader = new UIHeader();
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
