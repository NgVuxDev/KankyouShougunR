// $Id: M218.cs 17036 2014-03-06 12:44:54Z y-sato $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.APP
{
    /// <summary>
    /// M218 現場一覧
    /// </summary>
    public class M757 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            DENSHU_KBN denshuKbn = DENSHU_KBN.T_RAKURAKU_MASUTA_ICHIRAN;
            if (args.Length > 0)
            {
                denshuKbn = (DENSHU_KBN)args[0];
            }

            var callForm = new RakurakuMasutaIchiranForm(denshuKbn);
            var callHeader = new UIHeader();
            return new BusinessBaseForm(callForm, callHeader);
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
