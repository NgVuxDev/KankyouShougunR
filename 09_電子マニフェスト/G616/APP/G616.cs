using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake
{
    /// <summary>
    /// G616 混合廃棄物振分
    /// </summary>
    class G616 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            //引数 arg[0] : DT_R18_EX.SYSTEM_ID
            long sysId = -1;
            if (args.Length > 0 && args[0] != null)
            {
                if (!long.TryParse(args[0].ToString(), out sysId))
                {
                    sysId = (long)args[0];
                }
            }
            //引数 arg[1] : DT_R18_EX.KANRI_ID( or DT_R18.KANRI_ID)
            String kanriId = string.Empty;
            if (args.Length > 1 && args[1] != null)
            {
                kanriId = (String)args[1];
            }

            bool isLastSbnEndrepFlg = false;
            if (args.Length > 2 && args[2] != null)
                bool.TryParse(args[2].ToString(), out isLastSbnEndrepFlg);

            bool isRelationalMixMani = false;
            if (args.Length > 3 && args[3] != null)
                bool.TryParse(args[3].ToString(), out isRelationalMixMani);

            var callHeader = new HeaderBaseForm();
            var callForm = new UIForm(sysId, kanriId, isLastSbnEndrepFlg, isRelationalMixMani);
            return new Shougun.Core.Common.BusinessCommon.Base.BaseForm.BasePopForm(callForm, callHeader);
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
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
