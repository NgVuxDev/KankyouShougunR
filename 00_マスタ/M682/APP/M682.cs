using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.FormManager;
using NyuukinsakiIchiran.APP;

namespace Shougun.Core.Master.KobetsuHimeiTankaUpdate.APP
{
    /// <summary>
    /// M682 ○○マスタ保守
    /// </summary>
    public class M682 : IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name="args">FormManager.OpenForm()の可変引数</param>
        /// <returns>作成したフォーム</returns>
        public Form CreateForm(params object[] args)
        {
            var callForm = new UIForm();
            var headerForm = new HeaderForm();
            return new BusinessBaseForm(callForm, headerForm);
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されているフォーム</param>
        /// <param name="args">表示を要求されたFormManager.OpenForm()の可変引数</param>
        /// <returns>True: 同じ、False: 異なる</returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name="form">表示を更新するフォーム</param>
        /// <remarks>
        /// リスト表示や他の画面で変更される内容を表示している場合は最新の情報を表示すること
        /// </remarks>
        public void UpdateForm(Form form)
        {
        }
    }
}