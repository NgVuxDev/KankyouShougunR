using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoIchiran
{

    /// <summary>
    /// G511
    /// </summary>
    class G511 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[1] : 検索結果データ
            Dictionary<string, object> SearchResult = null;
            if (args.Length > 0)
            {
                SearchResult = (Dictionary<string, object>)args[0];
            }
            var callForm = new Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoIchiran.UIForm(SearchResult);
            var HeaderForm = new Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoIchiran.UIHeader();
            var orgForm = new BusinessBaseForm(callForm, HeaderForm);
            // 画面表示位置を設定（親フォーム中央）
            orgForm.StartPosition = FormStartPosition.Manual;
            // ポップアップ
            var dr = orgForm.ShowDialog();
            return null;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
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
