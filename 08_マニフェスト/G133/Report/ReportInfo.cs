using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using System.Data;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon;
using r_framework.Entity;

namespace Report
{
    /// <summary> 廃棄物帳簿を表すクラス・コントロール </summary>
    public class ReportInfo : ReportInfoBase
    {
        // ヘッダー部
        private string fh_corpRyakuName = string.Empty;                // 会社略称
        private string fh_hidukeFrom = string.Empty;                   // 日付開始
        private string fh_hidukeTo = string.Empty;                     // 日付終了

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportInfo"/> class.
        /// </summary>
        public ReportInfo()
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = "Template/R395_R517_R518_R519_R520_R521-Form.xml";
    
            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "R395";
        }

        /// <summary>
        /// 帳票出力フルパスフォームを保持するプロパティ
        /// </summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>
        /// 帳票出力フォームレイアウトを保持するプロパティ
        /// </summary>
        public string OutputFormLayout { get; set; }

        /// <summary>
        /// C1Reportの帳票データの作成ならびに明細部分の列定義を実行する
        /// </summary>
        public void Haikibutsu_Report(String layoutno, String corpRyakuName, String hidukeFrom, String hidukeTo, DataTable chouhyouData)
        {
            // ヘッダー項目の編集
            fh_corpRyakuName = corpRyakuName;
            fh_hidukeFrom = hidukeFrom;
            fh_hidukeTo = hidukeTo;

            // 帳票出力フォームレイアウトの決定
            switch (layoutno)
            {
                case "1":
                    // 収集運搬帳簿
                    this.OutputFormLayout = "R395";
                    break;
                case "2":
                    // 中間処理帳簿
                    this.OutputFormLayout = "R518";
                    break;
                case "3":
                    // 運搬帳簿
                    this.OutputFormLayout = "R519";
                    break;
                case "4":
                    // 運搬委託帳簿
                    this.OutputFormLayout = "R517";
                    break;
                case "5":
                    // 最終処分委託帳簿
                    this.OutputFormLayout = "R520";
                    break;
                case "6":
                    // 最終処分帳簿
                    this.OutputFormLayout = "R521";
                    break;
                default:
                    break;
            }

            // 抽出条件をセット

            // 明細データTABLEをセット
            this.SetRecord(chouhyouData);

            // レポートの作成
            this.Create(this.OutputFormFullPathName, this.OutputFormLayout, chouhyouData);
        }
        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {
            // ヘッダ部
            this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", fh_corpRyakuName);　　　　　　// 会社略称
            this.SetFieldName("FH_HIDUKE_HANI_FROM_VLB", fh_hidukeFrom);              // 日付開始
            this.SetFieldName("FH_HIDUKE_HANI_TO_VLB", fh_hidukeTo);                  // 日付終了
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.SetFieldName("FH_PRINT_DATE_VLB", this.getDBDateTime().ToString("yyyy/MM/dd HH:mm:ss"));
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            //数値フォーマット情報取得
            M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
            string ManifestSuuryoFormat = mSysInfo.MANIFEST_SUURYO_FORMAT.ToString();

            //R395:収集運搬帳簿
            //受入量
            this.SetFieldFormat("DTL_UKEIRERYO_CTL", ManifestSuuryoFormat);

            //運搬量
            this.SetFieldFormat("DTL_UNPANRYO_CTL", ManifestSuuryoFormat);

            //搬出量
            this.SetFieldFormat("DTL_HANSHUTURYO_CTL", ManifestSuuryoFormat);

            //受入先合計
            this.SetFieldFormat("G2F_UKEIRESAKI_GOUKEI_CTL1", ManifestSuuryoFormat);

            //運搬先合計
            this.SetFieldFormat("G2F_UNPANSAKI_GOUKEI_CTL1", ManifestSuuryoFormat);

            //運搬方法合計
            this.SetFieldFormat("G2F_UNPANHOUHOU_GOUKEI_CTL1", ManifestSuuryoFormat);

            //廃棄物種類毎合計
            this.SetFieldFormat("G2F_HAIKIBUTU_SHURUIGOTO_GOUKEI_CTL", ManifestSuuryoFormat);

            //総合計
            this.SetFieldFormat("G1F_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　start
            //受入量総合計
            this.SetFieldFormat("G1F_UKEIRERYO_SOUGOUKEI_CTL", ManifestSuuryoFormat);
            //運搬量総合計
            this.SetFieldFormat("G1F_UNPANRYO_SOUGOUKEI_FLB", ManifestSuuryoFormat);
            //搬出量総合計
            this.SetFieldFormat("G1F_HANSHUTURYO_SOUGOUKEI_CTL", ManifestSuuryoFormat);
            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　end

            //R517:運搬委託帳簿
            //委託量
            this.SetFieldFormat("DTL_ITAKURYO_CTL", ManifestSuuryoFormat);

            //運搬先合計
            this.SetFieldFormat("G2F_UNPANSAKI_GOUKEI_CTL1", ManifestSuuryoFormat);

            //廃棄物種類毎合計
            this.SetFieldFormat("G2F_HAIKIBUTU_SHURUIGOTO_GOUKEI_CTL", ManifestSuuryoFormat);

            //運搬先総合計
            this.SetFieldFormat("G1F_UNPANSAKI_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            //廃棄物種類毎総合計
            this.SetFieldFormat("G1F_HAIKIBUTU_SHURUIGOTO_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　start
            //委託量総合計
            this.SetFieldFormat("G1F_ITAKURYO_SOUGOUKEI_CTL", ManifestSuuryoFormat);
            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　end

            //R518:中間処理帳簿
            //受入量
            this.SetFieldFormat("DTL_UKEIRERYO_CTL", ManifestSuuryoFormat);

            //処分量
            this.SetFieldFormat("DTL_SHOBUNRYO_CTL", ManifestSuuryoFormat);

            //持出量
            this.SetFieldFormat("DTL_MOTIDASHIRYO_CTL", ManifestSuuryoFormat);

            //受入先合計
            this.SetFieldFormat("G2F_UKEIRESAKI_GOUKEI_CTL1", ManifestSuuryoFormat);

            //処分方法合計
            this.SetFieldFormat("G2F_SHOBUNHOUHOU_GOUKEI_CTL1", ManifestSuuryoFormat);

            //持出先合計
            this.SetFieldFormat("G2F_MOTIDASHISAKI_GOUKEI_CTL1", ManifestSuuryoFormat);

            //廃棄物種類毎合計
            this.SetFieldFormat("G2F_HAIKIBUTU_SHURUIGOTO_GOUKEI_CTL", ManifestSuuryoFormat);

            //受入先総合計
            this.SetFieldFormat("G1F_UKEIRESAKI_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            //処分方法総合計
            this.SetFieldFormat("G1F_SHOBUNHOUHOU_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            //持出先総合計
            this.SetFieldFormat("G1F_MOTIDASHISAKI_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            //廃棄物種類毎合計
            this.SetFieldFormat("G1F_HAIKIBUTU_SHURUIGOTO_SOUGOUKEI_CTL", ManifestSuuryoFormat);


            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　start
            //受入量総合計
            this.SetFieldFormat("G1F_UKEIRESAKI_SOUGOUKEI_CTL", ManifestSuuryoFormat);
            //処分量総合計
            this.SetFieldFormat("G1F_SHOBUNHOUHOU_SOUGOUKEI_CTL", ManifestSuuryoFormat);
            //持出量総合計
            this.SetFieldFormat("G1F_MOTIDASHISAKI_SOUGOUKEI_CTL", ManifestSuuryoFormat);
            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　end

            //R519:運搬帳簿
            //受入量
            this.SetFieldFormat("DTL_UKEIRERYO_CTL", ManifestSuuryoFormat);

            //運搬量
            this.SetFieldFormat("DTL_UNPANRYO_CTL", ManifestSuuryoFormat);

            //搬出量
            this.SetFieldFormat("DTL_HANSHUTURYO_CTL", ManifestSuuryoFormat);


            //受入先合計
            this.SetFieldFormat("G2F_UKEIRESAKI_GOUKEI_CTL1", ManifestSuuryoFormat);

            //運搬先合計
            this.SetFieldFormat("G2F_UNPANSAKI_GOUKEI_CTL1", ManifestSuuryoFormat);

            //運搬方法合計
            this.SetFieldFormat("G2F_UNPANHOUHOU_GOUKEI_CTL1", ManifestSuuryoFormat);

            //廃棄物種類毎合計
            this.SetFieldFormat("G2F_HAIKIBUTU_SHURUIGOTO_GOUKEI_CTL", ManifestSuuryoFormat);

            //総合計
            this.SetFieldFormat("G1F_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　start


            //受入先合計
            this.SetFieldFormat("G2F_UKEIRESAKI_GOUKEI_CTL", ManifestSuuryoFormat);

            //運搬先合計
            this.SetFieldFormat("G2F_UNPANSAKI_GOUKEI_CTL", ManifestSuuryoFormat);

            //運搬方法合計
            this.SetFieldFormat("G2F_UNPANHOUHOU_GOUKEI_CTL", ManifestSuuryoFormat);

            //廃棄物種類毎合計
            this.SetFieldFormat("G2F_HAIKIBUTU_SHURUIGOTO_GOUKEI_CTL", ManifestSuuryoFormat);

            //受入量総合計
            this.SetFieldFormat("G1F_UKEIRERYO_SOUGOUKEI_CTL", ManifestSuuryoFormat);
            //運搬量総合計
            this.SetFieldFormat("G1F_UNPANRYO_SOUGOUKEI_CTL", ManifestSuuryoFormat);
            //搬出量総合計
            this.SetFieldFormat("G1F_HANSHUTURYO_SOUGOUKEI_CTL", ManifestSuuryoFormat);
            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　end

            //R520:最終処分委託帳簿
            //委託量
            this.SetFieldFormat("DTL_ITAKU_RYO_CTL", ManifestSuuryoFormat);

            //受託者合計
            this.SetFieldFormat("G2F_JUTAKUSHA_GOUKEI_CTL1", ManifestSuuryoFormat);

            //廃棄物合計
            this.SetFieldFormat("G2F_HAIKIBUTU_GOUKEI_CTL", ManifestSuuryoFormat);

            //受託者総合計
            this.SetFieldFormat("G1F_JUTAKUSHA_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            //廃棄物総合計
            this.SetFieldFormat("G1F_HAIKIBUTU_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　start
            //委託量総合計
            this.SetFieldFormat("G1F_SOUGOUKEI_ITAKURYOU_CTL", ManifestSuuryoFormat);
            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　end

            //R521:最終処分帳簿
            //受入量
            this.SetFieldFormat("DTL_UKEIRERYO_CTL", ManifestSuuryoFormat);

            //処分量
            this.SetFieldFormat("DTL_SHOBUNRYO_CTL", ManifestSuuryoFormat);

            //受入先合計
            this.SetFieldFormat("G2F_UKEIRESAKI_GOUKEI_CTL1", ManifestSuuryoFormat);

            //処分方法合計
            this.SetFieldFormat("G2F_SHOBUNHOUHOU_GOUKEI_CTL1", ManifestSuuryoFormat);

            //廃棄物種類毎合計
            this.SetFieldFormat("G2F_HAIKIBUTUSHURUIGOTO_GOUKEI_CTL", ManifestSuuryoFormat);

            //受入先総合計
            this.SetFieldFormat("G1F_UKEIRESAKI_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            //処分方法総合計
            this.SetFieldFormat("G1F_SHOBUNHOUHOU_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            //廃棄物種類毎総合計
            this.SetFieldFormat("G1F_HAIKIBUTUSHURUIGOTO_SOUGOUKEI_CTL", ManifestSuuryoFormat);

            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　start
            //受入量総合計
            this.SetFieldFormat("G1F_UKEIRERYOU_SOUGOUKEI_CTL", ManifestSuuryoFormat);
            //処分量総合計
            this.SetFieldFormat("G1F_SHOBUNRYOU_SOUGOUKEI_CTL", ManifestSuuryoFormat);
            // 20140627 syunrei EV005044_収集運搬帳簿にて受入量総合計・運搬量総合計・搬出量総合計が小数点第一位固定となっている　end

        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            r_framework.Dao.GET_SYSDATEDao dao = r_framework.Utility.DaoInitUtility.GetComponent<r_framework.Dao.GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}
