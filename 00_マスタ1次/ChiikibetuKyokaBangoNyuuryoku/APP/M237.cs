// $Id: M237.cs 12491 2013-12-24 07:34:26Z nagata $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace ChiikibetsuKyokaBangoHoshu.APP
{
    /// <summary>
    /// M237 地域別許可番号入力
    /// </summary>
    class M237 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// 画面作成処理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Form CreateForm(params object[] args)
        {
            //CongBinh 20210714 #152813 S
            if (args.Length > 2)
            {
                short kyokaKbn = short.Parse(args[0].ToString());
                string gyoushaCd = args[1].ToString();
                string genbaCd = args[2].ToString();
                var callForm1 = new ChiikibetsuKyokaBangoHoshuForm(kyokaKbn, gyoushaCd, genbaCd);
                return new MasterBaseForm(callForm1, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, true);
            }
            //CongBinh 20210714 #152813 E
            var callForm = new ChiikibetsuKyokaBangoHoshuForm();
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
