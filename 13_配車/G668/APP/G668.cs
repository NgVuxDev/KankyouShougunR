using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.MobileJoukyouInfo
{

    /// <summary>
    /// G668 モバイル状況詳細
    /// </summary>
    class G668 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            WINDOW_TYPE windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // (配車)伝票番号
            string haishaDenpyouNo = string.Empty;
            if (args.Length > 1)
            {
                haishaDenpyouNo = args[1].ToString();
            }

            // (配車)配車区分
            string haishaKbn = string.Empty;
            if (args.Length > 2)
            {
                haishaKbn = args[2].ToString();
            }

            var HeaderForm = new UIHeader();
            var callForm = new UIForm(windowType, haishaDenpyouNo, haishaKbn);
            return new BusinessBaseForm(callForm, HeaderForm);
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
