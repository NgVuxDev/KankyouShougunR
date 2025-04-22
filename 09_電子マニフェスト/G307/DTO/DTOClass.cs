using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.ElectronicManifest.SyobunnShuryouHoukokuIkkatuNyuuryoku
{


    /// <summary>
    /// 入力内容
    /// </summary>
    public class InputInfoDTOCls
    {
        /// <summary>
        /// 管理番号を取得・設定します
        /// </summary>
        public String KANRI_ID { get; set; }

        /// <summary>
        /// 枝番を取得・設定します
        /// </summary>
        public String SEQ { get; set; }

        /// <summary>
        /// 運搬業者の加入者番号
        /// </summary>
        public String GYOUSYA_EDI_MEMBER_ID { get; set; }
        /// <summary>
        /// 運搬先業者の加入者番号
        /// </summary>
        public String SAKI_GYOUSYA_EDI_MEMBER_ID { get; set; }
        /// <summary>
        /// 運搬業者の業者CD
        /// </summary>
        public String GYOUSHA_CD { get; set; }
    }

    /// <summary>
    /// 出力内容
    /// </summary>
    public class OutputInfoDTOCls
    {
        /// <summary>
        /// 管理番号を取得・設定します
        /// </summary>
        public String KANRI_ID { get; set; }

        /// <summary>
        /// 枝番を取得・設定します
        /// </summary>
        public String SEQ { get; set; }

        /// <summary>
        /// 報告区分
        /// </summary>
        public String HOUKOKU_KUBUNN { get; set; }
        /// <summary>
        /// 処分終了日
        /// </summary>
        public String SYOBUNN_SYUURYOUHI { get; set; }
        /// <summary>
        /// 処分担当者コード
        /// </summary>
        public String SYOBUNN_TANNTOUSYA_CD { get; set; }
        /// <summary>
        /// 処分担当者名称
        /// </summary>
        public String SYOBUNN_TANNTOUSYA_NAME { get; set; }
        /// <summary>
        /// 報告担当者コード
        /// </summary>
        public String HOUKOKU_TANNTOUSYA_CD { get; set; }
        /// <summary>
        /// 報告担当者名称
        /// </summary>
        public String HOUKOKU_TANNTOUSYA_NAME { get; set; }
        /// <summary>
        /// 廃棄物受領日
        /// </summary>
        public String HAIKIBUTU_JYURYOUHI { get; set; }
        /// <summary>
        /// 運搬担当者コード
        /// </summary>
        public String UNNPANN_TANNTOUSYA_CD { get; set; }
        /// <summary>
        /// 運搬担当者名称
        /// </summary>
        public String UNNPANN_TANNTOUSYA_NAME { get; set; }
        /// <summary>
        /// 車輌番号コード
        /// </summary>
        public String SYARYOU_CD { get; set; }
        /// <summary>
        /// 車輌名称
        /// </summary>
        public String SYARYOU_NAME { get; set; }
        /// <summary>
        /// 受入量
        /// </summary>
        public String UKEIRERYOU { get; set; }
        /// <summary>
        /// 受入量単位コード
        /// </summary>
        public String UKEIRERYOU_TANNI_CD { get; set; }
        /// <summary>
        /// 受入量単位名称
        /// </summary>
        public String UKEIRERYOU_TANNI_NAME { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public String BIKO { get; set; }
        /// <summary>
        /// 種類（入力区分）
        /// </summary>
        public Decimal KIND { get; set; }

    }


    /// <summary>
    /// 一括入力内容取得検索条件DTO
    /// </summary>
    public class GetInputInfoDTOCls
    {
        /// <summary>
        /// 検索条件 :管理番号
        /// </summary>
        public String KANRI_ID { get; set; }
        /// <summary>
        /// 検索条件 :枝番
        /// </summary>
        public String SEQ { get; set; }
        /// <summary>
        /// 運搬業者の業者CD
        /// </summary>
        public String GYOUSHA_CD { get; set; }

    }
}
