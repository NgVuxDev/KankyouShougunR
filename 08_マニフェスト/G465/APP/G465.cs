using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran
{
    /// <summary>
    /// G465 換算値再計算一覧
    /// </summary>
    class G465 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {

            var callForm = new Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.UIForm();
            var callHeader = new Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.UIHeader();

            //画面について引数があり、何かをさせたい場合は追加する
            if (args.Length > 0)
            {
                //callForm.hogehoge = args[0].ToString();
            }
           
            var businessForm = new BusinessBaseForm(callForm, callHeader);
            return businessForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
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
