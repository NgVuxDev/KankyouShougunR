// $Id: M485.cs 18670 2014-04-08 00:13:12Z y-sato $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.Master.HikiaiGyoushaIchiran.APP
{
    /// <summary>
    /// M485 引合業者一覧
    /// </summary>
    class M485 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            DENSHU_KBN denshuKbn = DENSHU_KBN.HIKIAI_GYOUSHA;
            if (args.Length > 0)
            {
                denshuKbn = (DENSHU_KBN)args[0];
            }

            var callForm = new UIForm(denshuKbn);
            var headerForm = new IchiranHeaderForm();
            return new BusinessBaseForm(callForm, headerForm);
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
