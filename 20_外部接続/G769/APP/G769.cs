using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.FormManager;
using r_framework.Entity;
using Shougun.Core.ExternalConnection.SmsResult.APP;

namespace Shougun.Core.ExternalConnection.SmsResult
{
    /// <summary>
    /// G769 ｼｮｰﾄﾒｯｾｰｼﾞ着信結果
    /// </summary>
    class G769 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : T_SMS
            T_SMS entity = null;
            if (0 < args.Length && args[0] != null)
            {
                entity = args[0] as T_SMS;
            }

            // 引数 arg[1] : Window_Id
            string winId = string.Empty;
            if (1 < args.Length && args[1] != null)
            {
                winId = args[1] as string;
            }

            var callForm = new Shougun.Core.ExternalConnection.SmsResult.UIForm();
            return new BusinessBaseForm(callForm, new UIHeader());
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <returns>true：同じ false:異なる</returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
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
