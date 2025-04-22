using System;

namespace Shougun.Printing.Common
{
    /// <summary>
    /// 将軍R印刷管理/帳票設定項目クラス
    /// ShougunRPrintManager.Settings.Items.xmlのDTOクラス。
    /// </summary>
    [Serializable]
    public class ReportSettingsItem
    {
        public string SettingID { set; get; }
        public string ReportIDs { set; get; }
        public string Caption { set; get; }
        public string Type { set; get; }
        public string Mode { set; get; }
    }
}
