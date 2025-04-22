using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.ExternalConnection.DenshiBunshoHoshu
{
    /// <summary>
    /// M760 電子文書詳細入力
    /// </summary>
    class M760 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }
            string wansignSystemId = string.Empty;
            if (args.Length > 1)
            {
                wansignSystemId = args[1].ToString();
            }
            string keiyakuSystemId = string.Empty;
            if (args.Length > 2)
            {
                keiyakuSystemId = args[2].ToString();
            }

            // ベースフォーム
            var callHeader = new UIHeader();
            // 新規モード
            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(windowType))
            {
                var callForm = new UIForm();
                return new BusinessBaseForm(callForm, callHeader);
            }
            // 修正/削除/参照モード
            else
            {
                var callForm = new UIForm(windowType, wansignSystemId, keiyakuSystemId);
                return new BusinessBaseForm(callForm, callHeader);
            }
        }

        /// <summary>
        /// 同一情報存在問合せ処理
        /// </summary>
        /// <param name="form"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return false;
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
