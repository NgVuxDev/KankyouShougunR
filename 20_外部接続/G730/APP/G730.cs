using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.FormManager;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.ExternalConnection.FileUpload
{
    /// <summary>
    /// G730 ファイルアップロード
    /// </summary>
    class G730 : IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            List<long> fileIdList = null;
            WINDOW_ID windowId = WINDOW_ID.NONE;
            string[] paramList = null;
            UIForm callForm = null;

            // 1.登録済みのファイルIDリスト
            // 2.WINDOW_ID
            // 3.呼出し元画面のPK情報
            if (0 < args.Length)
            {
                fileIdList = (List<long>)args[0];
            }
            if (1 < args.Length)
            {
                windowId = (WINDOW_ID)args[1];
            }
            if (2 < args.Length)
            {
                paramList = (string[])args[2];
            }

            var headerForm = new UIHeader();
            if (2 < args.Length)
            {
                callForm = new UIForm(fileIdList, windowId, paramList);
            }
            else
            {
                callForm = new UIForm();
            }

            // モーダル表示のみのためBasePopFormで作成
            var businessForm = new BasePopForm(callForm, headerForm) { IsInFormResizable = true };
            return businessForm;
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
