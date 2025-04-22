// $Id: M213.cs 31591 2014-10-03 12:55:49Z nagata $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace TorihikisakiHoshu.APP
{
    /// <summary>
    /// M213 取引先入力
    /// </summary>
    class M213 : r_framework.FormManager.IShougunForm
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

            bool denshiShinseiFlg = false;
            if (args.Length > 2)
            {
                denshiShinseiFlg = bool.Parse(args[2].ToString());
            }

            // 承認済申請一覧画面から呼び出されたかのフラグ
            var isFromShouninzumiDenshiShinseiIchiran = false;
            if (args.Length > 3)
            {
                isFromShouninzumiDenshiShinseiIchiran = bool.Parse(args[3].ToString());
            }

            var shinseiTorihikisakiCd = string.Empty;
            if (args.Length > 4)
            {
                shinseiTorihikisakiCd = args[4].ToString();
            }

            var shinseiHikiaiTorihikisakiCd = string.Empty;
            if (args.Length > 5)
            {
                shinseiHikiaiTorihikisakiCd = args[5].ToString();
            }

            long denshiShinseiSysId = -1;
            if (args.Length > 6
                && args[6] != null
                && long.TryParse(args[6].ToString(), out denshiShinseiSysId))
            {
                // TryParseで格納済み
            }
            else
            {
                denshiShinseiSysId = -1;
            }

            int denshiShinseiSeq = -1;
            if (args.Length > 7
                && args[7] != null
                && int.TryParse(args[7].ToString(), out denshiShinseiSeq))
            {
                // TryParseで格納済み
            }
            else
            {
                denshiShinseiSeq = -1;
            }

            var callForm = new TorihikisakiHoshuForm(windowType, torihikisakiCd, denshiShinseiFlg, isFromShouninzumiDenshiShinseiIchiran);
            if (isFromShouninzumiDenshiShinseiIchiran)
            {
                callForm.ShinseiTorihikisakiCd = shinseiTorihikisakiCd;
                callForm.ShinseiHikiaiTorihikisakiCd = shinseiHikiaiTorihikisakiCd;
                callForm.DenshiShinseiSystemId = denshiShinseiSysId;
                callForm.DenshiShinseiSeq = denshiShinseiSeq;
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
            if (args.Length > 1)
            {
                string torihikisakiCd = args[1].ToString();
                var businessForm = form as BusinessBaseForm;
                var uiForm = businessForm.inForm as TorihikisakiHoshuForm;
                if (uiForm.WindowType == (WINDOW_TYPE)args[0]
                    || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    return (uiForm.TORIHIKISAKI_CD.Text.Equals(torihikisakiCd));
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
