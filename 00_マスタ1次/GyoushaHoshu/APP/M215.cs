// $Id: M215.cs 31591 2014-10-03 12:55:49Z nagata $
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using System;

namespace GyoushaHoshu.APP
{
    /// <summary>
    /// M215 業者入力
    /// </summary>
    class M215 : r_framework.FormManager.IShougunForm
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

            var shinseiGyoushaCd = string.Empty;
            if (args.Length > 4)
            {
                shinseiGyoushaCd = args[4].ToString();
            }

            var shinseiHikiaiGyoushaCd = string.Empty;
            if (args.Length > 5)
            {
                shinseiHikiaiGyoushaCd = args[5].ToString();
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

            string torihikisakiCd = "";
            if (args.Length > 8)
            {
                torihikisakiCd = Convert.ToString(args[8]);
            }

            var callForm = new GyoushaHoshuForm(windowType, gyoushaCd, denshiShinseiFlg, isFromShouninzumiDenshiShinseiIchiran, torihikisakiCd);
            if (isFromShouninzumiDenshiShinseiIchiran)
            {
                callForm.ShinseiGyoushaCd = shinseiGyoushaCd;
                callForm.ShinseiHikiaiGyoushaCd = shinseiHikiaiGyoushaCd;
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
                string gyoushaCd = args[1].ToString();
                var businessForm = form as BusinessBaseForm;
                var uiForm = businessForm.inForm as GyoushaHoshuForm;
                if (uiForm.WindowType == (WINDOW_TYPE)args[0]
                    || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && (WINDOW_TYPE)args[0] == WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    if (args.Length >= 9)
                    {
                        string torihikisakiCd = args[8].ToString();
                        if (torihikisakiCd != "" && uiForm.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                        {
                            //取引先CDが引数で渡されていて新規画面を起動しようとしているとき、取引先→業者の連携で使用
                            //この時は、業者入力が新規で立ち上がっていても、許容する。
                            return false;
                        }
                    }
                    else
                    {
                        return (uiForm.GYOUSHA_CD.Text.Equals(gyoushaCd));
                    }
                }
                else
                {
                    return (uiForm.GYOUSHA_CD.Text.Equals(gyoushaCd));
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
