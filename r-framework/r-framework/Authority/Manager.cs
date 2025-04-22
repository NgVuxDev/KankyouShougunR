using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Const;

namespace r_framework.Authority
{
    public static class Manager
    {
        /// <summary>
        /// 権限チェック
        /// </summary>
        /// <param name="formID">フォームID</param>
        /// <param name="windowType">画面区分</param>
        /// <param name="windowId">画面ID</param>
        /// <returns>権限あり:true 権限なし:false</returns>
        public static bool CheckAuthority(string formID, WINDOW_TYPE windowType, WINDOW_ID windowId = Const.WINDOW_ID.NONE)
        {
            // エラーメッセージ表示有
            return CheckAuthority(formID, windowType, true, windowId);
        }

        /// <summary>
        /// 権限チェック
        /// </summary>
        /// <param name="formID">フォームID</param>
        /// <param name="windowType">画面区分</param>
        /// <param name="isDispErrMsg">エラーメッセージ表示有無</param>
        /// <param name="windowId">画面ID</param>
        /// <returns>権限あり:true 権限なし:false</returns>
        public static bool CheckAuthority(string formID, WINDOW_TYPE windowType, bool isDispErrMsg, WINDOW_ID windowId = Const.WINDOW_ID.NONE)
        {
            if (string.IsNullOrEmpty(formID))
            {
                return false;
            }

            // モード指定があればその権限をチェック
            if (windowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG
                    && !Dto.SystemProperty.Shain.GetAuth(formID, windowId).HasFlag(AuthMethodFlag.Read))
            {
                // 参照権限なし
                if (isDispErrMsg)
                {
                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E158", "参照");
                }
                return false;
            }
            else if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG
                        && !Dto.SystemProperty.Shain.GetAuth(formID, windowId).HasFlag(AuthMethodFlag.Add))
            {
                // 新規権限なし
                if (isDispErrMsg)
                {
                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E158", "新規");
                }
                return false;
            }
            else if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG
                        && !Dto.SystemProperty.Shain.GetAuth(formID, windowId).HasFlag(AuthMethodFlag.Edit))
            {
                // 修正権限なし
                if (isDispErrMsg)
                {
                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E158", "修正");
                }
                return false;
            }
            else if (windowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                        && !Dto.SystemProperty.Shain.GetAuth(formID, windowId).HasFlag(AuthMethodFlag.Delete))
            {
                // 削除権限なし
                if (isDispErrMsg)
                {
                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E158", "削除");
                }
                return false;
            }

            // 基本は権限あり
            return true;
        }
    }
}
