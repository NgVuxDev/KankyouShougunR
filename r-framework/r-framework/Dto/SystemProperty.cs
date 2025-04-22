using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using r_framework.Entity;
using System.Data;

namespace r_framework
{
    /// <summary>権限フラグ</summary>
    [Flags]
    public enum AuthMethodFlag
    {
        /// <summary>無し</summary>
        None = 0,
        /// <summary>参照</summary>
        Read = 1,
        /// <summary>追加(新規)</summary>
        Add = Read << 1,
        /// <summary>更新(編集)</summary>
        Edit = Read << 2,
        /// <summary>削除</summary>
        Delete = Read << 3,
        /// <summary>全て</summary>
        All = Read | Add | Edit | Delete
    }
}

namespace r_framework.Dto
{
    /// <summary>Rシステム全体の共通情報</summary>
    static public class SystemProperty
    {
        /// <summary>
        /// ログインユーザ名
        /// </summary>
        public static string UserName { get; set; }

        public static string NameFormName { get; set; }

        /// <summary>
        /// ターミナルモードを取得する。互換性のために残しています。
        /// 代わりにAppConfigを使用してください。r_framework.Configuration.AppConfig.IsTerminalMode
        /// </summary>
        public static bool IsTerminalMode
        {
            get
            {
                return r_framework.Configuration.AppConfig.IsTerminalMode;
            }
            private set
            {
                // 使わない
            }
        }

        /// <summary>
        /// 社員情報の設定(開発用)
        /// </summary>
        /// <param name="cd">社員CD</param>
        /// <param name="name">社員名略称</param>
        public static void SetCurrentShain(string cd, string name)
        {
            SystemProperty.Shain.CD = cd;
            SystemProperty.Shain.Name = name;
        }

        /// <summary>
        /// 社員情報の設定
        /// </summary>
        /// <param name="mShain">社員情報Entity</param>
        public static void SetCurrentShain(M_SHAIN mShain)
        {
            SystemProperty.Shain.CD = mShain.SHAIN_CD;
            SystemProperty.Shain.Name = mShain.SHAIN_NAME_RYAKU;
            SystemProperty.Shain.BushoCD = mShain.BUSHO_CD;
            SystemProperty.Shain.InxsTantouFlg = mShain.INXS_TANTOU_FLG.IsNull ? false : mShain.INXS_TANTOU_FLG.Value;
        }

        /// <summary>
        /// Set InxsSubApp Setting
        /// </summary>
        /// <param name="tbDataInxs"></param>
        public static void SetAppSettingInxs(DataTable tbDataInxs)
        {
            string hyoujunTemplateCd = string.Empty;
            if (tbDataInxs != null && tbDataInxs.Rows.Count > 0)
            {
                hyoujunTemplateCd = tbDataInxs.Rows[0]["BUSINESS_TYPE"].ToString();
            }
            SystemProperty.AppSettingInxs.BusinessType = hyoujunTemplateCd;
        }

        /// <summary>
        /// 最大表示画面数情報の設定
        /// </summary>
        /// <param name="mShainMaxWindow"></param>
        public static void SetMaxWindowCount(M_SHAIN_MAX_WINDOW mShainMaxWindow)
        {
            if (mShainMaxWindow != null && !string.IsNullOrEmpty(mShainMaxWindow.SHAIN_CD))
            {
                // テーブルのデータ
                SystemProperty.Shain.MAX_WINDOW_COUNT = (short)mShainMaxWindow.MAX_WINDOW_COUNT;
            }
            else
            {
                // テーブルにデータが無い場合はデフォルト値
                SystemProperty.Shain.MAX_WINDOW_COUNT = short.Parse(r_framework.Const.Constans.MAX_WINDOW_COUNT.ToString());
            }
        }

        /// <summary>
        /// フォーマット情報の設定
        /// </summary>
        /// <param name="tanka">単価フォーマット</param>
        /// <param name="juryou">重量フォーマット</param>
        /// <param name="suuryou">数量フォーマット</param>
        public static void SetCurrentFormat(string tanka, string juryou, string suuryou)
        {
            SystemProperty.Format.Tanka = tanka;
            SystemProperty.Format.Jyuryou = juryou;
            SystemProperty.Format.Suuryou = suuryou;
        }

        /// <summary>
        /// フォーマット情報の設定
        /// </summary>
        /// <param name="mSysInfo">システム設定Entity</param>
        public static void SetCurrentFormat(M_SYS_INFO mSysInfo)
        {
            SystemProperty.Format.Jyuryou = mSysInfo.SYS_JYURYOU_FORMAT;
            SystemProperty.Format.Tanka = mSysInfo.SYS_TANKA_FORMAT;
            SystemProperty.Format.Suuryou = mSysInfo.SYS_SUURYOU_FORMAT;
            SystemProperty.Format.ManifestSuuryou = mSysInfo.MANIFEST_SUURYO_FORMAT;
            SystemProperty.Format.ItakuKeiyakuTanka = mSysInfo.ITAKU_KEIYAKU_TANKA_FORMAT;
            SystemProperty.Format.ItakuKeiyakuSuuryou = mSysInfo.ITAKU_KEIYAKU_SUURYOU_FORMAT;
        }

        /// <summary>
        /// 共通タイトル文字列を取得
        /// </summary>
        private static string commonWindowTitleText { get; set; }

        /// <summary>
        /// 共通タイトル文字列を設定
        /// </summary>
        public static void SetCommonWindowTitleText(string text)
        {
            SystemProperty.commonWindowTitleText = text;
        }

        /// <summary>
        /// 機能画面のWindowタイトルを作成する。
        /// フォームラベル名にメインメニュー画面と同じウインドウタイトルをくっつける
        /// </summary>
        public static string CreateWindowTitle(string formTitle)
        {
            var windowTitle = formTitle;
            if (!string.IsNullOrEmpty(SystemProperty.commonWindowTitleText))
            {
                // 共通タイトルが定義済みならフォームラベルタイトルの後ろにハイフンで連結する
                windowTitle = windowTitle + " - " + SystemProperty.commonWindowTitleText;
            }
            return windowTitle;
        }

        /// <summary>
        /// ログイン社員情報クラス
        /// </summary>
        public static class Shain
        {
            static Shain()
            {
                Shain.userFunctionAuth = new Dictionary<string, AuthMethodFlag>();
            }

            /// <summary>社員CD</summary>
            public static string CD { get; internal set; }

            /// <summary>社員略称</summary>
            public static string Name { get; internal set; }

            /// <summary>所属部署CD</summary>
            public static string BushoCD { get; internal set; }

            /// <summary>
            /// Inxs Permission
            /// </summary>
            public static bool InxsTantouFlg { get; internal set; }

            /// <summary>最大表示画面数</summary>
            public static short MAX_WINDOW_COUNT { get; set; }

            /// <summary>メニュー権限</summary>
            /// <remarks>[画面ID+WINDOW_ID][権限フラグ]</remarks>
            private static Dictionary<string, AuthMethodFlag> userFunctionAuth;

            // 20150610 マイメニュー選択の取得・設定 Start
            /// <summary>マイメニュー</summary>
            /// <remarks>[グループ・サブ・インデックスID+画面ID][マイメニュー表示順]</remarks>
            private static Dictionary<string, int> userBookmark;

            // 20150610 マイメニュー選択の取得・設定 End

            /// <summary>権限フラグの取得。指定機能(画面)情報を保持していない場合は全操作可能</summary>
            /// <param name="formId">画面ID</param>
            /// <param name="windowId">WINDOW_ID。デフォルトはNONE。主に汎用帳票で指定</param>
            /// <returns>権限フラグ</returns>
            public static AuthMethodFlag GetAuth(string formId, r_framework.Const.WINDOW_ID windowId = Const.WINDOW_ID.NONE)
            {
                AuthMethodFlag result = AuthMethodFlag.All;
                var keyValue = formId + (windowId == Const.WINDOW_ID.NONE ? String.Empty : Convert.ToString((int)windowId));

                if (Shain.userFunctionAuth.ContainsKey(keyValue))
                {
                    result = Shain.userFunctionAuth[keyValue];
                }

                return result;
            }

            /// <summary>権限情報設定。menuAuthを使用して自身の権限を設定</summary>
            /// <param name="menuAuth">M_MENU_AUTH</param>
            public static void SetAuth(M_MENU_AUTH[] menuAuth)
            {
                // 社員CD未設定の場合は、全画面全権限あり
                if (String.IsNullOrEmpty(Shain.CD))
                {
                    return;
                }

                Shain.userFunctionAuth = menuAuth.Where(s => s.SHAIN_CD == Shain.CD && s.BUSHO_CD == Shain.BushoCD)
                                                 .ToDictionary(
                                                    s => s.FORM_ID + (s.WINDOW_ID < 1 ? String.Empty : s.WINDOW_ID.ToString()),
                                                    s => (s.AUTH_READ ? AuthMethodFlag.Read : AuthMethodFlag.None)
                                                            | (s.AUTH_ADD ? AuthMethodFlag.Add : AuthMethodFlag.None)
                                                            | (s.AUTH_EDIT ? AuthMethodFlag.Edit : AuthMethodFlag.None)
                                                            | (s.AUTH_DELETE ? AuthMethodFlag.Delete : AuthMethodFlag.None)
                                                    );

                // 社員優先で部署分をセット
                //Dictionary<string, AuthMethodFlag> tmpDic = menuAuth.Where(s => string.IsNullOrEmpty(s.SHAIN_CD) && s.BUSHO_CD == Shain.BushoCD)
                //                                    .ToDictionary(
                //                                        s => s.FORM_ID + (s.WINDOW_ID < 1 ? String.Empty : s.WINDOW_ID.ToString()),
                //                                        s => (s.AUTH_READ ? AuthMethodFlag.Read : AuthMethodFlag.None)
                //                                            | (s.AUTH_ADD ? AuthMethodFlag.Add : AuthMethodFlag.None)
                //                                            | (s.AUTH_EDIT ? AuthMethodFlag.Edit : AuthMethodFlag.None)
                //                                            | (s.AUTH_DELETE ? AuthMethodFlag.Delete : AuthMethodFlag.None)
                //                                        );
                //foreach (string key in tmpDic.Keys)
                //{
                //    if (!Shain.userFunctionAuth.ContainsKey(key))
                //    {
                //        Shain.userFunctionAuth.Add(key, tmpDic[key]);
                //    }
                //}
            }

            /// <summary>権限情報更新。menuAuthを使用して自身の権限を更新</summary>
            /// <param name="menuAuth">M_MENU_AUTH</param>
            /// <returns>更新件数</returns>
            public static int UpdateAuth(M_MENU_AUTH[] menuAuth)
            {
                int count = 0;
                if (!String.IsNullOrEmpty(Shain.CD) && menuAuth != null)
                {
                    foreach (var auth in menuAuth.Where(s => s.SHAIN_CD == Shain.CD && s.BUSHO_CD == Shain.BushoCD))
                    {
                        var keyValue = auth.FORM_ID + (auth.WINDOW_ID < 1 ? String.Empty : auth.WINDOW_ID.ToString());
                        if (Shain.userFunctionAuth.ContainsKey(keyValue))
                        {
                            Shain.userFunctionAuth[keyValue] = (auth.AUTH_READ ? AuthMethodFlag.Read : AuthMethodFlag.None)
                                                                | (auth.AUTH_ADD ? AuthMethodFlag.Add : AuthMethodFlag.None)
                                                                | (auth.AUTH_EDIT ? AuthMethodFlag.Edit : AuthMethodFlag.None)
                                                                | (auth.AUTH_DELETE ? AuthMethodFlag.Delete : AuthMethodFlag.None);
                        }
                        else
                        {
                            Shain.userFunctionAuth.Add(keyValue, (auth.AUTH_READ ? AuthMethodFlag.Read : AuthMethodFlag.None)
                                                               | (auth.AUTH_ADD ? AuthMethodFlag.Add : AuthMethodFlag.None)
                                                               | (auth.AUTH_EDIT ? AuthMethodFlag.Edit : AuthMethodFlag.None)
                                                               | (auth.AUTH_DELETE ? AuthMethodFlag.Delete : AuthMethodFlag.None));
                        }
                        count++;
                    }
                }

                // 部署更新の場合
                //foreach (var auth in menuAuth.Where(s => string.IsNullOrEmpty(s.SHAIN_CD) && s.BUSHO_CD == Shain.BushoCD))
                //{
                //    var keyValue = auth.FORM_ID + (auth.WINDOW_ID < 1 ? String.Empty : auth.WINDOW_ID.ToString());
                //    if (Shain.userFunctionAuth.ContainsKey(keyValue))
                //    {
                //        Shain.userFunctionAuth[keyValue] = (auth.AUTH_READ ? AuthMethodFlag.Read : AuthMethodFlag.None)
                //                                            | (auth.AUTH_ADD ? AuthMethodFlag.Add : AuthMethodFlag.None)
                //                                            | (auth.AUTH_EDIT ? AuthMethodFlag.Edit : AuthMethodFlag.None)
                //                                            | (auth.AUTH_DELETE ? AuthMethodFlag.Delete : AuthMethodFlag.None);
                //    }
                //    else
                //    {
                //        Shain.userFunctionAuth.Add(keyValue, (auth.AUTH_READ ? AuthMethodFlag.Read : AuthMethodFlag.None)
                //                                           | (auth.AUTH_ADD ? AuthMethodFlag.Add : AuthMethodFlag.None)
                //                                           | (auth.AUTH_EDIT ? AuthMethodFlag.Edit : AuthMethodFlag.None)
                //                                           | (auth.AUTH_DELETE ? AuthMethodFlag.Delete : AuthMethodFlag.None));
                //    }
                //    count++;
                //}

                return count;
            }

            #region お気に入り選択

            /// <summary>
            /// お気に入り選択フラグの取得
            /// </summary>
            /// <param name="groupIndexNo"></param>
            /// <param name="subIndexNo"></param>
            /// <param name="assemblyIndexNo"></param>
            /// <param name="formId"></param>
            /// <returns></returns>
            public static int GetBookmark(int groupIndexNo, int subIndexNo, int assemblyIndexNo, string formId)
            {
                int result = 0;
                var key = string.Format("{0}_{1}_{2}_{3}", groupIndexNo, subIndexNo, assemblyIndexNo, formId);

                if (Shain.userBookmark.ContainsKey(key))
                {
                    result = Shain.userBookmark[key];
                }

                return result;
            }

            /// <summary>
            /// お気に入り選択の設定
            /// </summary>
            /// <param name="bookmarkList"></param>
            public static void SetBookmark(M_MY_FAVORITE[] bookmarkList)
            {
                Shain.userBookmark = bookmarkList.Where(s => s.SHAIN_CD == Shain.CD && s.BUSHO_CD == Shain.BushoCD).
                    ToDictionary(s => string.Format("{0}_{1}", s.INDEX_NO, s.FORM_ID), s => (s.MY_FAVORITE.IsNull ? 0 : s.MY_FAVORITE.Value));
            }

            #endregion お気に入り選択
        }

        /// <summary>
        /// フォーマット情報クラス
        /// </summary>
        public static class Format
        {
            /// <summary>
            /// 単価フォーマット
            /// </summary>
            public static string Tanka { get; internal set; }

            /// <summary>
            /// 重量フォーマット
            /// </summary>
            public static string Jyuryou { get; internal set; }

            /// <summary>
            /// 数量フォーマット
            /// </summary>
            public static string Suuryou { get; internal set; }

            /// <summary>
            /// 委託契約書数量フォーマット
            /// </summary>
            public static string ItakuKeiyakuSuuryou { get; internal set; }

            /// <summary>
            /// 委託契約書単価フォーマット
            /// </summary>
            public static string ItakuKeiyakuTanka { get; internal set; }

            /// <summary>
            /// マニフェスト数量フォーマット
            /// </summary>
            public static string ManifestSuuryou { get; internal set; }
        }

        /// <summary>
        /// 印刷設定
        /// </summary>
        public static class PrintSettings
        {
            /// <summary>
            /// 帳票リスト
            /// </summary>
            public static ReadOnlyCollection<ReportInfo> ReportList { get; private set; }

            /// <summary>
            /// 帳票印刷設定情報の設定
            /// </summary>
            /// <param name="settings"></param>
            public static void SetReportList(S_REPORTLISTPRINTERSETTINGS[] settings)
            {
                var reportListPrinterSettings = new List<ReportInfo>();

                for (int i = 0; i < settings.Length; i++)
                {
                    reportListPrinterSettings.Add(new ReportInfo(settings[i]));
                }

                ReportList = new ReadOnlyCollection<ReportInfo>(reportListPrinterSettings);
            }

            /// <summary>
            /// 帳票情報クラス
            /// </summary>
            public class ReportInfo
            {
                /// <summary>
                /// コンストラクタ
                /// </summary>
                internal ReportInfo()
                {
                }

                /// <summary>
                /// コンストラクタ
                /// </summary>
                /// <param name="entity"></param>
                internal ReportInfo(S_REPORTLISTPRINTERSETTINGS entity)
                {
                    this.ID = (int)entity.ID;
                    this.DispName = entity.REPORT_DISP_NAME;
                    this.ReportButsuriName = entity.REPORT_BUTSURI_NAME;
                    this.ReportLayoutName = entity.REPORT_LAYOUT_NAME;
                }

                /// <summary>
                /// ID
                /// </summary>
                public int ID { get; private set; }

                /// <summary>
                /// 帳票表示名
                /// </summary>
                public string DispName { get; private set; }

                /// <summary>
                /// 帳票物理名(テンプレートファイルパス)
                /// </summary>
                public string ReportButsuriName { get; set; }

                /// <summary>
                /// 帳票レイアウト名
                /// </summary>
                public string ReportLayoutName { get; set; }
            }
        }

        /// <summary>
        /// XPS出力情報クラス
        /// </summary>
        public static class XPSPrintInfo
        {
            /// <summary>
            /// 印刷連番
            /// </summary>
            public static int PrintNo { get; set; }
        }

        /// <summary>
        /// InxsSubApp setting
        /// </summary>
        public static class AppSettingInxs
        {
            public static string BusinessType { get; internal set; }
        }

        /// <summary>
        /// INXS Setting
        /// </summary>
        public static class InxsSettings
        {
            public static string FilePath { get; internal set; }
        }

        /// <summary>
        /// Set INXS Setting
        /// </summary>
        /// <param name="filePath"></param>
        public static void SetInxsSettings(string filePath)
        {
            SystemProperty.InxsSettings.FilePath = filePath;
        }
    }
}