// $Id: M217.cs 31808 2014-10-07 11:07:57Z nagata $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace GenbaHoshu.APP
{
    /// <summary>
    /// M217 現場入力
    /// </summary>
    class M217 : r_framework.FormManager.IShougunForm
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

            string gyoushaCd = string.Empty;
            if (args.Length > 1)
            {
                gyoushaCd = args[1].ToString();
            }

            string genbaCd = string.Empty;
            if (args.Length > 2)
            {
                genbaCd = args[2].ToString();
            }

            bool denshiShinseiFlg = false;
            if (args.Length > 3)
            {
                denshiShinseiFlg = bool.Parse(args[3].ToString());
            }

            // 承認済申請一覧画面から呼び出されたかのフラグ
            var isFromShouninzumiDenshiShinseiIchiran = false;
            if (args.Length > 4)
            {
                isFromShouninzumiDenshiShinseiIchiran = bool.Parse(args[4].ToString());
            }

            var shinseiTorihikisakiCd = string.Empty;
            if (args.Length > 5 && null != args[5])
            {
                shinseiTorihikisakiCd = args[5].ToString();
            }

            var shinseiGyoushaCd = string.Empty;
            if (args.Length > 6)
            {
                shinseiGyoushaCd = args[6].ToString();
            }

            var shinseiGenbaCd = string.Empty;
            if (args.Length > 7)
            {
                shinseiGenbaCd = args[7].ToString();
            }

            var shinseiHikiaiTorihikisakiCd = string.Empty;
            if (args.Length > 8 && null!= args[8])
            {
                shinseiHikiaiTorihikisakiCd = args[8].ToString();
            }

            var shinseiHikiaiGyoushaCd = string.Empty;
            if (args.Length > 9)
            {
                shinseiHikiaiGyoushaCd = args[9].ToString();
            }

            var shinseiHikiaiGenbaCd = string.Empty;
            if (args.Length > 10)
            {
                shinseiHikiaiGenbaCd = args[10].ToString();
            }

            long denshiShinseiSysId = -1;
            if (args.Length > 11
                && args[11] != null
                && long.TryParse(args[11].ToString(), out denshiShinseiSysId))
            {
                // TryParseで格納済み
            }
            else
            {
                denshiShinseiSysId = -1;
            }

            int denshiShinseiSeq = -1;
            if (args.Length > 12
                && args[12] != null
                && int.TryParse(args[12].ToString(), out denshiShinseiSeq))
            {
                // TryParseで格納済み
            }
            else
            {
                denshiShinseiSeq = -1;
            }

            bool ShinseiHikiaiGyoushaUseFlg = false;
            if (args.Length > 13
                && args[13] != null
                && bool.TryParse(args[13].ToString(), out ShinseiHikiaiGyoushaUseFlg))
            {
                // TryParseで格納済み
            }

            var callForm = new GenbaHoshuForm(windowType, gyoushaCd, genbaCd, denshiShinseiFlg, isFromShouninzumiDenshiShinseiIchiran);
            if (isFromShouninzumiDenshiShinseiIchiran)
            {
                callForm.ShinseiTorihikisakiCd = shinseiTorihikisakiCd;
                callForm.ShinseiGyoushaCd = shinseiGyoushaCd;
                callForm.ShinseiGenbaCd = shinseiGenbaCd;
                callForm.ShinseiHikiaiTorihikisakiCd = shinseiHikiaiTorihikisakiCd;
                callForm.ShinseiHikiaiGyoushaCd = shinseiHikiaiGyoushaCd;
                callForm.ShinseiHikiaiGenbaCd = shinseiHikiaiGenbaCd;
                callForm.DenshiShinseiSystemId = denshiShinseiSysId;
                callForm.DenshiShinseiSeq = denshiShinseiSeq;
                callForm.ShinseiHikiaiGyoushaUseFlg = ShinseiHikiaiGyoushaUseFlg;
            }
            return new BusinessBaseForm(callForm, windowType);
        }

        /// <summary>
        /// 同一情報存在問合せ処理
        /// </summary>
        /// <param name="form"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            if (args.Length > 2)
            {
                string gyoushaCd = args[1].ToString();
                string genbaCd = args[2].ToString();
                var businessForm = form as BusinessBaseForm;
                var uiForm = businessForm.inForm as GenbaHoshuForm;
                if (uiForm.WindowType == (WINDOW_TYPE)args[0]
                    || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    return (uiForm.GyoushaCode.Text.Equals(gyoushaCd) && uiForm.GenbaCode.Text.Equals(genbaCd));
                }
            }
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
