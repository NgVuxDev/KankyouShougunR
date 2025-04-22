using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.ExternalConnection.GaibuRenkeiGenbaIchiran
{
    /// <summary>
    /// M694 外部連携現場一覧
    /// </summary>
    class M694 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            DENSHU_KBN denshuKbn = DENSHU_KBN.GAIBU_RENKEI_GENBA_ICHIRAN;
            if (args.Length > 0)
            {
                denshuKbn = (DENSHU_KBN)args[0];
            }
            
            var HeaderForm = new GaibuRenkeiGenbaIchiran.HeaderForm();
            var callForm = new GaibuRenkeiGenbaIchiranForm();
            var businessForm = new BusinessBaseForm(callForm, HeaderForm) { IsInFormResizable = true };

            return businessForm;
        }

        /// <summary>
        /// 同一情報存在問合せ処理
        /// </summary>
        /// <param name="form"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        /// <summary>
        /// フォーム更新処理
        /// </summary>
        /// <param name="form"></param>
        public void UpdateForm(Form form)
        {
        }
    }
}
