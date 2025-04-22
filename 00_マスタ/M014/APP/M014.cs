using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;

namespace Shougun.Core.Master.OboegakiIkkatuHoshu
{

    /// <summary>
    /// G289 定期配車実績入力
    /// </summary>
    class M014 : r_framework.FormManager.IShougunForm
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

            // 引数 arg[1] : 
            T_ITAKU_MEMO_IKKATSU_ENTRY ItakuMemoIkkatsuEntryEntity = new T_ITAKU_MEMO_IKKATSU_ENTRY();
            if (args.Length > 1)
            {
                ItakuMemoIkkatsuEntryEntity = (T_ITAKU_MEMO_IKKATSU_ENTRY)args[1];
            }
            // ベースフォーム
            var callHeader = new Shougun.Core.Master.OboegakiIkkatuHoshu.APP.UIHeader();

            // 新規モード
            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(windowType))
            {
                // 引数: 定期実績番号
                var callForm = new Shougun.Core.Master.OboegakiIkkatuHoshu.APP.UIForm();
                return new BusinessBaseForm(callForm, callHeader);
            }
            // 修正/削除/参照モード
            else
            {
                // 引数: 定期実績番号
                var callForm = new Shougun.Core.Master.OboegakiIkkatuHoshu.APP.UIForm(windowType, ItakuMemoIkkatsuEntryEntity);
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
            // 同じ受入番号のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 1)
            {
                T_ITAKU_MEMO_IKKATSU_ENTRY ItakuMemoIkkatsuEntryEntity = (T_ITAKU_MEMO_IKKATSU_ENTRY)args[1];
                var baseForm = form as BusinessBaseForm;
                var uiForm = baseForm.inForm as Shougun.Core.Master.OboegakiIkkatuHoshu.APP.UIForm;
                return (ItakuMemoIkkatsuEntryEntity.DENPYOU_NUMBER.ToString() == uiForm.txtDenpyouNumber.Text.Trim());
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
