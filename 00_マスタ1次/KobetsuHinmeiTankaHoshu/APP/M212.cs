// $Id: M212.cs 12491 2013-12-24 07:34:26Z nagata $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace KobetsuHinmeiTankaHoshu.APP
{
    /// <summary>
    /// M212 個別品名単価入力
    /// </summary>
    class M212 : r_framework.FormManager.IShougunForm
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

            string torihikisakiCd = string.Empty;
            if (args.Length > 1)
            {
                torihikisakiCd = args[1].ToString();
            }

            string gyoushaCd = string.Empty;
            if (args.Length > 2)
            {
                gyoushaCd = args[2].ToString();
            }

            string genbaCd = string.Empty;
            if (args.Length > 3)
            {
                genbaCd = args[3].ToString();
            }

            string dennpyouKbn = string.Empty;
            if (args.Length > 4)
            {
                dennpyouKbn = args[4].ToString();
            }

            var callForm = new KobetsuHinmeiTankaHoshuForm(windowType, torihikisakiCd, gyoushaCd, genbaCd, dennpyouKbn);
            return new MasterBaseForm(callForm, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, true);
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
