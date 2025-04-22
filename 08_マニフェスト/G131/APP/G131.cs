using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup
{

    /// <summary>
    /// G131
    /// </summary>
    class G131 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
           
            var callForm = new Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup.UIForm();
            var callHeader = new Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup.UIHeader();
            var popForm = new BasePopForm(callForm, callHeader);
            // 画面表示位置を設定（親フォーム中央）
            popForm.StartPosition = FormStartPosition.Manual;
            //// ポップアップ
            //var dr = popForm.ShowDialog();

            return popForm;
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
