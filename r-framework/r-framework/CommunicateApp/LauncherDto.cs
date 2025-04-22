using r_framework.Dto;

namespace r_framework.CommunicateApp
{
    public class LauncherDto
    {
        public string ShainCD { get; set; }
        public string ShougunConfigPath { get; set; }
        public string SubAppDBConnectName { get; set; }
        public bool IsInxsUketsuke { get; set; }
        public bool IsInxsSeikyuusho { get; set; } // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338
        public bool IsInxsPayment { get; set; } // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339
        public bool IsInxsContract { get; set; }  // 20210225 【マージ】INXS委託契約書アップロード機能を環境将軍R V2.22にマージ依頼 #147341
        public bool IsInxsLicense { get; set; } // 20210225 【マージ】INXS許可証アップロード機能を環境将軍R V2.22にマージ依頼
        public bool IsInxsManifest { get; set; } // 20210226 【マージ】INXSマニフェストシェア機能を環境将軍R V2.22にマージ依頼
        public bool IsInxsNoticeSupport { get; set; }
        public CommunicateAppDto Args { get; set; }
    }
}
