using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.PaperManifest.ManifestPattern
{
    /// <summary>
    /// G299 マニフェストパターン一覧
    /// </summary>
    class G299 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : 一括登録区分
            String ListRegistKbn = "false";

            string[] t = null;

            if (args.Length > 0)
            {
                ListRegistKbn = (String)args[0];
            }

            // 引数 arg[1] :廃棄物区分CD
            String HaikiKbnCD = "";
            if (args.Length > 1)
            {
                HaikiKbnCD = (String)args[1];
            }

            Shougun.Core.PaperManifest.ManifestPattern.UIForm callForm = null;
            switch (HaikiKbnCD)
            {
                case "1"://産廃（直行）
                case "2"://建廃
                case "3"://産廃（積替）
                default://未指定
                    callForm = new Shougun.Core.PaperManifest.ManifestPattern.UIForm(DENSHU_KBN.MANI_PATTERN_ICHIRAN, ListRegistKbn, HaikiKbnCD,"");
                    break;

                case "4"://電子
                    callForm = new Shougun.Core.PaperManifest.ManifestPattern.UIForm(DENSHU_KBN.DENSHI_MANI_PATTERN_ICHIRAN, ListRegistKbn, HaikiKbnCD,"");
                    break;
            }

            var callHeader = new Shougun.Core.PaperManifest.ManifestPattern.UIHeader("", t);
            return new BusinessBaseForm(callForm, callHeader);
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 同じ受入番号のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 1)
            {
                String HaikiKbnCD = (String)args[1];
                var footerForm = form as BusinessBaseForm;
                var uiForm = footerForm.inForm as UIForm;
                return (uiForm.HaikiKbnCD == HaikiKbnCD);
            }
            return false;
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
