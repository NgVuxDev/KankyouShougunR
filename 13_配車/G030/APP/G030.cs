using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;

namespace Shougun.Core.Allocation.TeikiHaishaNyuuryoku
{
    /// <summary>
    /// G030 定期配車入力
    /// </summary>
    class G030 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name="args">SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] : String 定期配車番号　
            String teikiHaishaNumber = string.Empty;
            if (args.Length > 1)
            {
                teikiHaishaNumber = args[1].ToString();
            }

            // 引数 arg[2] : String 作業日
            DateTime sagyoubi = new DateTime();
            if (args.Length > 2 && args[2] != null)
            {
                DateTime.TryParse(args[2].ToString(), out sagyoubi);
            }

            // 引数 arg[3] : コース名称CD
            String courseNameCd = string.Empty;
            if (args.Length > 3)
            {
                courseNameCd = args[3].ToString();
            }

            // No.2840-->
            // 引数 arg[4] :車両情報
            string[] slist = new string[8];
            if (args.Length > 4)
            {
                for (var i = 0; i < 8; i++)
                {
                    slist[i] = ((string[])args[4])[i];
                }
            }
            else
            {
                slist = null;
            }
            // No.2840<--

            // 引数 arg[5] : 振替配車区分
            string fukikae_kbn = string.Empty;
            if (args.Length > 5)
            {
                fukikae_kbn = args[5].ToString();
            }

            // 引数 arg[6] : 曜日
            string dayCd = string.Empty;
            if (args.Length > 6)
            {
                dayCd = args[6].ToString();
            }

            // ベースフォーム
            var callHeader = new UIHeader();

            // 参照モード
            if (WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(windowType))
            {
                // 引数:処理モード、曜日CD、コース名称CD、車両情報
                var callForm = new UIForm(windowType, callHeader, sagyoubi, courseNameCd, teikiHaishaNumber, slist, fukikae_kbn, dayCd);
                return new BusinessBaseForm(callForm, callHeader);
            }
            // 新規/修正/削除モード
            else
            {
                // 引数:処理モード、定期配車番号
                var callForm = new UIForm(windowType, callHeader, teikiHaishaNumber, slist);
                return new BusinessBaseForm(callForm, callHeader);
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
            // 同じ定期配車番号のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 1)
            {
                string teikiHaishaNumber = (string)args[1];
                var baseForm = form as BusinessBaseForm;
                var uiForm = baseForm.inForm as UIForm;
                return (uiForm.TEIKI_HAISHA_NUMBER.Text.Trim() == teikiHaishaNumber);
            }
            return false;
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
