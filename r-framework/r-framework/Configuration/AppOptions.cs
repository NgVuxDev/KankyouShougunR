using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shougun.UserRestrict.URXmlDocument;

namespace r_framework.Configuration
{
    /// <summary>
    /// オプションフラグを保持するクラス
    /// </summary>
    public class AppOptions
    {
        /// <summary>
        /// デフォルトコンストラクタ利用禁止
        /// </summary>
        private AppOptions()
        {
        }

        /// <summary>
        /// IDをKeyとしたDicitionary
        /// TValueStructにはcaptionとvalueを持つ
        /// </summary>
        public Dictionary<string, TValueStruct> OptionDictionary { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="URXmlDoc"></param>
        internal AppOptions(URXmlDocument URXmlDoc)
        {
            OptionDictionary = new Dictionary<string, TValueStruct>();

            // optionグループのIDを取得
            string[] idArray = URXmlDoc.EnumItems("option");

            foreach (string id in idArray)
            {
                TValueStruct tValStruct = new TValueStruct();
                tValStruct.caption = URXmlDoc.GetItem(id).caption;
                tValStruct.value = URXmlDoc.GetItemValue(id);

                OptionDictionary.Add(id, tValStruct);
            }
        }

        #region オプションの有効判定
        /// <summary>ワークフロー(電子申請)</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsWorkflow()
        {
            return this.OptionDictionary.ContainsKey("workflow") ? Convert.ToBoolean(this.OptionDictionary["workflow"].value) : false;
        }

        /// <summary>トラックスケール連動</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsTruckscale()
        {
            return this.OptionDictionary.ContainsKey("truckscale") ? Convert.ToBoolean(this.OptionDictionary["truckscale"].value) : false;
        }

        /// <summary>実績報告書</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsAchievementReport()
        {
            return this.OptionDictionary.ContainsKey("achievementReport") ? Convert.ToBoolean(this.OptionDictionary["achievementReport"].value) : false;
        }

        /// <summary>モバイル将軍連携</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsMobile()
        {
            return this.OptionDictionary.ContainsKey("mobile") ? Convert.ToBoolean(this.OptionDictionary["mobile"].value) : false;
        }

        /// <summary>オンラインバンク連携</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsOnlinebank()
        {
            return this.OptionDictionary.ContainsKey("onlinebank") ? Convert.ToBoolean(this.OptionDictionary["onlinebank"].value) : false;
        }

        //#162933 S
        /// <summary>オンラインバンク出金</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsPaymentOnlinebank()
        {
            return this.OptionDictionary.ContainsKey("paymentOnlinebank") ? Convert.ToBoolean(this.OptionDictionary["paymentOnlinebank"].value) : false;
        }
        //#162933 E

        /// <summary>財務出力</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsFinance()
        {
            return this.OptionDictionary.ContainsKey("finance") ? Convert.ToBoolean(this.OptionDictionary["finance"].value) : false;
        }

        /// <summary>汎用CSV出力</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsCsv()
        {
            return this.OptionDictionary.ContainsKey("csv") ? Convert.ToBoolean(this.OptionDictionary["csv"].value) : false;
        }

        /// <summary>電子請求</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsElectronicInvoice()
        {
            return this.OptionDictionary.ContainsKey("ElectronicInvoice") ? Convert.ToBoolean(this.OptionDictionary["ElectronicInvoice"].value) : false;
        }

        /// <summary>CTI連携</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsCti()
        {
            return this.OptionDictionary.ContainsKey("cti") ? Convert.ToBoolean(this.OptionDictionary["cti"].value) : false;
        }

        /// <summary>INXS受付連携</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsInxsUketsuke()
        {
            return this.OptionDictionary.ContainsKey("InxsUketsuke") ? Convert.ToBoolean(this.OptionDictionary["InxsUketsuke"].value) : false;
        }

        /// <summary>将軍-INXS 請求書アップロード</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsInxsSeikyuusho()
        {
            return this.OptionDictionary.ContainsKey("InxsSeikyuusho") ? Convert.ToBoolean(this.OptionDictionary["InxsSeikyuusho"].value) : false;
        }

        /// <summary>将軍-INXS 支払明細書アップロード</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsInxsShiharai()
        {
            return this.OptionDictionary.ContainsKey("InxsShiharai") ? Convert.ToBoolean(this.OptionDictionary["InxsShiharai"].value) : false;
        }

        /// <summary>将軍-INXS 契約書アップロード</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsInxsItaku()
        {
            return this.OptionDictionary.ContainsKey("InxsItaku") ? Convert.ToBoolean(this.OptionDictionary["InxsItaku"].value) : false;
        }

        /// <summary>将軍-INXS 許可証アップロード</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsInxsKyokasho()
        {
            return this.OptionDictionary.ContainsKey("InxsKyokasho") ? Convert.ToBoolean(this.OptionDictionary["InxsKyokasho"].value) : false;
        }

        /// <summary>将軍-INXS マニフェストシェア</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsInxsManifest()
        {
            return this.OptionDictionary.ContainsKey("InxsManifest") ? Convert.ToBoolean(this.OptionDictionary["InxsManifest"].value) : false;
        }

        /// <summary>将軍-INXS お知らせ・サポート</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsInxsOshirase()
        {
            return this.OptionDictionary.ContainsKey("InxsOshirase") ? Convert.ToBoolean(this.OptionDictionary["InxsOshirase"].value) : false;
        }

        /// <summary>INXS連携</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsInxs()
        {
            return this.IsInxsUketsuke() || this.IsInxsSeikyuusho() || IsInxsShiharai() || IsInxsItaku() || IsInxsKyokasho() || this.IsInxsManifest() || IsInxsOshirase();
        }

        /// <summary>紙マニCSVデータインポート</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsManiImport()
        {
            return this.OptionDictionary.ContainsKey("maniImport") ? Convert.ToBoolean(this.OptionDictionary["maniImport"].value) : false;
        }

        /// <summary>コース最適化(NAVITIME連携)</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsNAVITIME()
        {
            return this.OptionDictionary.ContainsKey("NAVITIME") ? Convert.ToBoolean(this.OptionDictionary["NAVITIME"].value) : false;
        }

        /// <summary>電子契約</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsDenshiKeiyaku()
        {
            return this.OptionDictionary.ContainsKey("denshikeiyaku") ? Convert.ToBoolean(this.OptionDictionary["denshikeiyaku"].value) : false;
        }

        /// <summary>ファイルアップロード</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsFileUpload()
        {
            return this.OptionDictionary.ContainsKey("fileupload") ? Convert.ToBoolean(this.OptionDictionary["fileupload"].value) : false;
        }

        /// <summary>ファイルアップロード</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsMAPBOX()
        {
            return this.OptionDictionary.ContainsKey("mapbox") ? Convert.ToBoolean(this.OptionDictionary["mapbox"].value) : false;
        }

        /// <summary>ファイルアップロード（現場メモ）</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsFileUploadGenbaMemo()
        {
            return this.OptionDictionary.ContainsKey("fileuploadgenbamemo") ? Convert.ToBoolean(this.OptionDictionary["fileuploadgenbamemo"].value) : false;
        }

        /// <summary>楽楽明細連携</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsRakurakuMeisai()
        {
            return OptionDictionary.ContainsKey("RakurakuMeisai") ? Convert.ToBoolean(OptionDictionary["RakurakuMeisai"].value) : false;
        }

        /// <summary>電子契約最新照会（WAN-Sign）</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsWANSign()
        {
            return this.OptionDictionary.ContainsKey("WANSign") ? Convert.ToBoolean(this.OptionDictionary["WANSign"].value) : false;
        }

        /// <summary>ショートメッセージ</summary>
        /// <returns>true:有効、false:無効</returns>
        public bool IsSMS()
        {
            return this.OptionDictionary.ContainsKey("sms") ? Convert.ToBoolean(this.OptionDictionary["sms"].value) : false;
        }
        #endregion

        /// <summary>
        /// バージョン情報に表示するオプション項目名と値
        /// </summary>
        public class TValueStruct
        {
            // 表示項目名
            public string caption { set; get; }
            // 値
            public object value { set; get; }
        }
    }
}
