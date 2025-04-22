using System;
using System.Text;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;

namespace Shougun.Printing.Common
{
    /// <summary>
    /// 将軍R印刷管理/帳票設定内容クラス
    /// 帳票種類ごとのユーザーの設定内容を保持する。
    /// 保存ファイルからの設定内容の読込/保存も行う。
    /// </summary>
    public class ReportSettingsManager
    {
        public static ReportSettingsManager Instance
        {
            get
            {
                return ReportSettingsManager.singleton;
            }
        }
        private static ReportSettingsManager singleton = new ReportSettingsManager();
    
        private ReportSettingsManager()
        {
        }

        /// <summary>
        /// 帳票設定内容の配列を取得
        /// </summary>
        public ReportSettings[] Items { get; private set; }

        /// <summary>
        /// 帳票設定コンテナの初期化
        /// 帳票設定項目定義(Items.xml)を読み込み、設定内容を準備する。
        /// </summary>
        public bool Initialize()
        {
            LastError.Clear();
            
            // 既に初期化済みなら何もしない
            if (this.Items != null && this.Items.Length > 0)
            {
                return true;
            }

            // 埋め込みXMLから定義項目（帳票名）の一覧を作成
            string xmlPath = "Shougun.Printing.Common.ReportSettingsItems.xml";
            try
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(ReportSettingsItem[]));
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream(xmlPath))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        if (reader != null)
                        {
                            // XMLから帳票設定項目を読み込み
                            var itemsArray = (ReportSettingsItem[])serializer.Deserialize(reader);

                            // 帳票設定項目毎に帳票設定内容を初期化して配列化
                            var list = new List<ReportSettings>();
                            foreach (var item in itemsArray)
                            {
                                list.Add(new ReportSettings(item));
                            }
                            this.Items = list.ToArray();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LastError.Exception = ex;
            }

            this.Items = new ReportSettings[0];
            LastError.Message = xmlPath + "の読み込みに失敗しました。\r\n" + LastError.Message;
            return false;
        }

        /// <summary>
        /// 帳票設定の取得。設定ID指定("Settings/Items.xmlの<SettingID>参照")
        /// </summary>
        /// <param name="settingsID"></param>
        /// <returns>ReportSettings</returns>
        public ReportSettings FindBySettingsID(string settingsID)
        {
            foreach (var settings in this.Items)
            {
                if (settings.Item.SettingID.Equals(settingsID))
                {
                    return settings;
                }
            }
            return null;
        }

        /// <summary>
        /// 帳票設定の取得。帳票ID指定("Settings/Items.xmlの<SettingID>参照")
        /// </summary>
        /// <param name="reportID"></param>
        /// <param name="isMulti"></param>
        /// <returns>ReportSettings</returns>
        public ReportSettings FindByReportId(string reportID, bool isMulti)
        {
            if (!string.IsNullOrEmpty(reportID) && ReportSettingsManager.Instance.Items != null)
            {
                // 帳票IDから設定情報を探す
                foreach (var settings in ReportSettingsManager.Instance.Items)
                {
                    if (settings.Item.ReportIDs.Contains(reportID))
                    {
                        if ((isMulti && settings.Item.Mode.Equals("Multi"))
                         || (!isMulti && !settings.Item.Mode.Equals("Multi")))
                        {
                            return settings;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 変更のあった帳票設定をすべて保存。
        /// 変更フラグが立っているものを保存する。
        /// </summary>
        public bool SaveAll()
        {
            LastError.Clear();
            try
            {
                foreach (var settings in this.Items)
                {
                    if (settings.HasChanged)
                    {
                        settings.Save();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LastError.Message = "印刷設定の保存に失敗しました。\r\n" + ex.Message;
            }
            return false;
        }

        /// <summary>
        /// 帳票設定変更の有無を確認
        /// ひとつでも変更があればtrue、すべて変更なしならfalse
        /// </summary>
        public bool HasChanges
        {
            get
            {
                if (this.Items != null)
                {
                    foreach (var settings in this.Items)
                    {
                        if (settings.HasChanged)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>レイアウトの余白調整値を取得する</summary>
        /// <remarks>
        /// 将軍本体でXPSファイル作成前にロードしたテンプレートに対し余白調整するための差分値。
        /// 縦方向と横方向の2種類。単位はtwip。データ型はSystem.Double。負数あり。
        /// 取得した調整値でC1.C1Report.LayoutクラスのMarginプロパティを変更すること。
        /// 変更した結果が負数にならないように注意すること。
        /// 帳票IDが不明な場合は利用しないこと。
        /// </remarks>
        /// <param name="ReportId">帳票ID。"R493"など</param>
        /// <param name="isMulti">連票フラグ。連票の場合はTrue、そうでない場合はfalse</param>
        /// <value>上下左右の調整値が格納されたMargin構造体インスタンス</value>
        /// <example>
        /// C1.C1Report.Layout layout = ...;
        /// var delta = Shougun.Printing.Common.ReportSettingsManager.GetMarginDelta("R493", true);
        /// layout.MarginLeft = Math.Max(0, layout.MarginLeft + delta.Left);
        /// layout.MarginRight = Math.Max(0, layout.MarginRight + delta.Right); 
        /// layout.MarginTop = Math.Max(0, layout.MarginTop + delta.Top);
        /// layout.MarginBottm = Math.Max(0, layout.MarginBottom + delta.Bottom); 
        /// </example>
        public static Margins GetMarginDelta(string reportID, bool isMulti)
        {
            if (ReportSettingsManager.chacheEnable)
            {
                if (ReportSettingsManager.cachedReportID != null)
                {
                    if (ReportSettingsManager.cachedReportID.Equals(reportID)
                        && ReportSettingsManager.cachedIsMulti == isMulti)
                    {
                        return ReportSettingsManager.cachedMargins;
                    }
                    else
                    {
                        ReportSettingsManager.EnableMarginDeltaCache(false);
                    }
                }
            }

            Margins margins = new Margins();
            var settings = ReportSettingsManager.Instance.FindByReportId(reportID, isMulti);
            if (settings != null)
            { 
                settings.Load();
                margins = settings.Margins;
                margins.Factor(57.6d); // 元はmm単位なのでtwip単位に変換
            }

            if (ReportSettingsManager.chacheEnable)
            {
                ReportSettingsManager.cachedMargins = margins;
                ReportSettingsManager.cachedReportID = reportID;
                ReportSettingsManager.cachedIsMulti = isMulti;
            }
            
            return margins;
        }

        static bool chacheEnable = false;
        static Margins cachedMargins;
        static string cachedReportID = null;
        static bool cachedIsMulti = false;

        public static void EnableMarginDeltaCache(bool enable)
        {
            ReportSettingsManager.chacheEnable = enable;
            ReportSettingsManager.cachedReportID = null;
        }
    }
}
