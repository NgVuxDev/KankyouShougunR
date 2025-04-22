// $Id: G021.cs 15378 2014-01-29 00:25:09Z sys_dev_22 $using System;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.Reception.UketukeiIchiran
{
    /// <summary>
    /// G021 受付一覧
    /// </summary>
    class G021 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : KYOTEN_CD 拠点
            string kyotenCd = string.Empty;
            if (args.Length > 0)
            {
                kyotenCd = (string)args[0];
            }
            //2014/01/28 修正 仕様変更 qiao start
            // 引数 arg[1] : txtNum_DenPyouSyurui 伝票種類
            string denPyouSyurui = string.Empty;
            if (args.Length > 1)
            {
                denPyouSyurui = args[1].ToString();
            }
            var callForm = new Shougun.Core.Reception.UketukeiIchiran.UIForm(kyotenCd, denPyouSyurui);
            return new BusinessBaseForm(callForm, new UIHeaderForm());
        }
            //2014/01/28 修正 仕様変更 qiao end
        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            //2014/01/28 修正 仕様変更 qiao start
            // 同じ伝票種類のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 2)
            {
                string denPyouSyurui = args[2].ToString();
                var footerForm = form as BusinessBaseForm;
                var uiForm = footerForm.inForm as UIForm;
                if (uiForm.txtNum_DenPyouSyurui.Text != denPyouSyurui)
                {
                    switch (denPyouSyurui)
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                            uiForm.txtNum_DenPyouSyurui.Text = denPyouSyurui;
                            break;
                    }
                }
            }
            return true;
            //2014/01/28 修正 仕様変更 qiao end
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
