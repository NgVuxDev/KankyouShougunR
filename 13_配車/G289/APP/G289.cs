using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku
{

    /// <summary>
    /// G289 定期配車実績入力
    /// </summary>
    class G289 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] : 定期実績番号
           
            String jisekiNumber = String.Empty;
            if (args.Length > 1)
            {
                jisekiNumber = (string)args[1];
            }
            // ベースフォーム
            var callHeader = new UIHeader();

            // 参照モード
            if (WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(windowType))
            {
                // 引数: 定期実績番号
                var callForm = new UIForm(windowType, jisekiNumber);
                return new BusinessBaseForm(callForm, callHeader);
            }
            // 新規/修正/削除モード 
            else
            {
                bool haishaFlg = false;
                if (args.Length > 2)
                {
                    haishaFlg = (bool)args[2];
                    // 引数: 定期実績番号
                    var callForm = new UIForm(windowType, jisekiNumber, haishaFlg);
                    return new BusinessBaseForm(callForm, callHeader);
                }
                else
                {
                    // 引数: 定期実績番号
                    var callForm = new UIForm(windowType, jisekiNumber);
                    return new BusinessBaseForm(callForm, callHeader);
                }
            }
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
                string uketsukeNumber = (string)args[1];
                var baseForm = form as BusinessBaseForm;
                var uiForm = baseForm.inForm as UIForm;
                return (uiForm.TEIKI_JISSEKI_NUMBER.Text.Trim() == uketsukeNumber);
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
