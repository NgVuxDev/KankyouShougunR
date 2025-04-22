using r_framework.Entity;

namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Entity
{
    /// <summary>
    /// 年度情報データクラス
    /// </summary>
    public class NendoInfo : SuperEntity
    {
        // SQLより取得する項目
        public string BUSHO_CD { get; set; }    //部署コード
        public string BUSHO_NAME { get; set; }  //部署名
        public string SHAIN_CD { get; set; }    //社員コード
        public string SHAIN_NAME { get; set; }  //社員名
        public decimal YOSAN_1 { get; set; }    //年1の予算
        public decimal YOSAN_2 { get; set; }    //年2の予算
        public decimal YOSAN_3 { get; set; }    //年3の予算
        public decimal YOSAN_4 { get; set; }    //年4の予算
        public decimal YOSAN_5 { get; set; }    //年5の予算
        public decimal YOSAN_6 { get; set; }    //年6の予算
        public decimal YOSAN_7 { get; set; }    //年7の予算
        public decimal YOSAN_8 { get; set; }    //年8の予算
        public decimal YOSAN_9 { get; set; }    //年9の予算
        public decimal YOSAN_GOUKEI { get; set; }     //予算合計
        public decimal JISSEKI_1 { get; set; }  //年1の実績
        public decimal JISSEKI_2 { get; set; }  //年2の実績
        public decimal JISSEKI_3 { get; set; }  //年3の実績
        public decimal JISSEKI_4 { get; set; }  //年4の実績
        public decimal JISSEKI_5 { get; set; }  //年5の実績
        public decimal JISSEKI_6 { get; set; }  //年6の実績
        public decimal JISSEKI_7 { get; set; }  //年7の実績
        public decimal JISSEKI_8 { get; set; }  //年8の実績
        public decimal JISSEKI_9 { get; set; }  //年9の実績
        public decimal JISSEKI_GOUKEI { get; set; }   //実績合計

        // 編集結果項目
        public decimal TASSEI_RITSU_1 { get; set; }  //年1の達成率
        public decimal TASSEI_RITSU_2 { get; set; }  //年2の達成率
        public decimal TASSEI_RITSU_3 { get; set; }  //年3の達成率
        public decimal TASSEI_RITSU_4 { get; set; }  //年4の達成率
        public decimal TASSEI_RITSU_5 { get; set; }  //年5の達成率
        public decimal TASSEI_RITSU_6 { get; set; }  //年6の達成率
        public decimal TASSEI_RITSU_7 { get; set; }  //年7の達成率
        public decimal TASSEI_RITSU_8 { get; set; }  //年8の達成率
        public decimal TASSEI_RITSU_9 { get; set; }  //年9の達成率
        public decimal TASSEI_GOKEI { get; set; }   //達成率合計
    }
}