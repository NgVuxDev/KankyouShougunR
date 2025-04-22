using System;
using System.Diagnostics;
using System.Linq;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using PgInfo = Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile.SettingsCls.ProgramInfos;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    /// <summary>
    /// 外部プログラム
    /// </summary>
    /// <remarks>
    /// CurrentUserCustomConfigProfile.xmlのProgramInfos.Nameと同じメンバー名を定義すること
    /// </remarks>
    public enum ProgramsInfoEnum : int
    {
        /// <summary>該当なし</summary>
        NONE = 0,

        /// <summary>年次報告CSV</summary>
        NENJI_HOKOKU = 1,
    }


    /// <summary>
    /// 外部プログラムをキックするクラス
    /// </summary>
    public static class ProgramKickerUtility
    {
        /// <summary>
        /// 指定された外部プログラムを開始します。
        /// </summary>
        /// <param name="pgInfoEnum">起動したいプログラム名</param>
        /// <param name="isWait">
        ///     true  = プロセスが終了するのを待ちます
        ///     false = プロセスの終了を待ちません
        /// </param>
        public static void Start(ProgramsInfoEnum pgInfoEnum, bool isWait = false)
        {
            // プロセス起動情報のインスタンス生成
            ProcessStartInfo info = new ProcessStartInfo();

            try
            {
                // プロセス起動情報を編集
                if (!SetProcessInfo(pgInfoEnum, ref info))
                {
                    return;
                }

                // プロセススタート
                var prosess = Process.Start(info);

                if (isWait)
                {
                    // プロセスが終了するまで待機
                    prosess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ProgramKickerUtility.Start", ex);
                new MessageBoxShowLogic().MessageBoxShowError(string.Format("外部プログラム '{0}' が見つかりませんでした。", pgInfoEnum.ToString()));
            }
        }

        /// <summary>
        /// プロセス起動情報を編集します
        /// </summary>
        /// <param name="pgInfoEnum">起動したいプログラム名</param>
        /// <param name="info">プロセス起動情報</param>
        /// <returns>true=成功、false=失敗</returns>
        private static bool SetProcessInfo(ProgramsInfoEnum pgInfoEnum, ref ProcessStartInfo info)
        {
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            if (userProfile.Settings.ExternalProgram == null)
            {
                new MessageBoxShowLogic().MessageBoxShow(string.Format("外部プログラム '{0}' が見つかりませんでした。", pgInfoEnum.ToString()));
                return false;
            }

            PgInfo cls = userProfile.Settings.ExternalProgram
                                             .Where(n => n.Name.Equals(Enum.GetName(typeof(ProgramsInfoEnum), pgInfoEnum)))
                                             .FirstOrDefault();

            if (cls == null)
            {
                new MessageBoxShowLogic().MessageBoxShow(string.Format("外部プログラム '{0}' が見つかりませんでした。", pgInfoEnum.ToString()));
                return false;
            }

            // プロセス起動情報の設定
            info.FileName = cls.FilePath;

            if (!string.IsNullOrEmpty(cls.Argument))
            {
                info.Arguments = cls.Argument;
            }

            return true;
        }
    }
}
