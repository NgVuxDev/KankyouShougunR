using System;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.PaperManifest.Himodukeichiran
{
    class G589 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数がそのまま渡される</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var callForm = new Shougun.Core.PaperManifest.Himodukeichiran.UIForm();
            var callHeader = new Shougun.Core.PaperManifest.Himodukeichiran.UIHeader();
            // 20140618 ria EV004875 マニフェスト入力の[F7]状況ボタンを押下時に伝票紐付一覧が開く start
            if (args.Length > 0)
            {
                callForm.formManiFlag = Convert.ToInt16(args[0]);
            }
            if (args.Length > 1)
            {
                callForm.formHaikiKbn = args[1].ToString();
            }
            // 20140618 ria EV004875 マニフェスト入力の[F7]状況ボタンを押下時に伝票紐付一覧が開く end
            var businessForm = new BusinessBaseForm(callForm, callHeader);
            return businessForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="Form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 常に前面表示
            return true;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name=form>表示を更新するフォーム</param>
        /// リスト表示や他の画面で変更される内容を表示している場合は最新の情報を表示すること。
        public void UpdateForm(Form form)
        {
        }
    }
}
