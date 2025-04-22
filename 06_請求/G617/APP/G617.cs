using r_framework.APP.Base;
using r_framework.Const;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Billing.GetsujiShori
{
    class G617 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name="args">FormManager.OpenForm()の可変引数。
        /// メニューから呼び出す場合はパラメータなし。</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var HeaderForm = new UIHeader();
            var callForm = new UIForm(HeaderForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            var businessBaseForm = new BusinessBaseForm(callForm, HeaderForm);

            return businessBaseForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">FormManager.OpenForm()の可変引数</param>
        /// <return>
        /// true：同じ
        ///    表示中の画面と可変引数は同じ内容のものであることを示す。
        ///    trueを返却するとformで指定された画面が前面表示される。
        ///    一覧系や帳票出力系などの複数画面が必要ない場合は無条件でtrueを返却すること。
        ///    入力系画面で新規入力以外のモードの場合、argsが表示中の内容と同一のEntryの場合はtrueを返却すること。
        /// false:異なる。
        ///    表示中の画面とパラメータは異なる内容のものであることを示す。
        ///    argsでパラメータ指定された新規画面が作成される。
        ///    入力系画面で新規入力モードの場合はfalseを返却すること。
        ///    入力系画面で新規入力以外のモードの場合、argsが表示中の内容と異なるEntryの場合はfalseを返却すること。
        /// </return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name="form">表示を更新するフォーム</param>
        /// リスト表示や他の画面で変更される内容を表示している場合は最新の情報を表示すること。
        public void UpdateForm(Form form)
        {

        }
    }
}
