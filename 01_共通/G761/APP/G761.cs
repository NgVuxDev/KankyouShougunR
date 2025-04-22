using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.DenpyouRireki.APP;
using Shougun.Function.ShougunCSCommon.Dto;

namespace Shougun.Core.Common.DenpyouRireki
{
    class G761 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            LogUtility.DebugMethodStart(args);

            DenpyouRirekiDTOClass dto = null;

            if (args.Length > 0)
            {
                dto = (DenpyouRirekiDTOClass)args[0];
            }

            var callForm = new Shougun.Core.Common.DenpyouRireki.APP.G761Form(dto);
            var HeaderForm = new Shougun.Core.Common.DenpyouRireki.APP.G761HeaderForm();
            var businessForm = new BusinessBaseForm(callForm, HeaderForm);

            LogUtility.DebugMethodEnd(businessForm);
            return businessForm;
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
            // 引数 arg[0] : 取引先CD
            string torihikisakiCd = string.Empty;
            // 引数 arg[1] : 業者CD
            string gyoushaCd = string.Empty;
            // 引数 arg[2] : 現場CD
            string genbaCd = string.Empty;

            if (args.Length > 0)
            {
                torihikisakiCd = args[0].ToString();
            }
            if (args.Length > 1)
            {
                gyoushaCd = args[1].ToString();
            }
            if (args.Length > 2)
            {
                genbaCd = args[2].ToString();
            }

            var baseForm = form as BusinessBaseForm;
            var uiForm = baseForm.inForm as Shougun.Core.Common.DenpyouRireki.APP.G761Form;

            if ((torihikisakiCd == uiForm.TORIHIKISAKI_CD.Text && gyoushaCd == uiForm.GYOUSHA_CD.Text && genbaCd == uiForm.GENBA_CD.Text ))
            {
                return true;
            }

            return false; ;
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