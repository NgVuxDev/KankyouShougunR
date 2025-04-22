using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.Master.KobestuHinmeiTankaIkkatsu
{
    /// <summary>
    /// M423 個別品名単価一括変更
    /// </summary>
    class M423 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name="args">SgFormManager.OpenForm()の可変引数</param>
        /// <returns>作成したフォーム。失敗時はnull</returns>
        public Form CreateForm(params object[] args)
        {
            var callForm = new UIForm();
            return new MasterBaseForm(callForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG, true);
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <returns>true：同じ false:異なる</returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 一覧画面は無条件にtrue(最前面表示)
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
