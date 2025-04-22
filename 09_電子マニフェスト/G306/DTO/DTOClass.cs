using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.ElectronicManifest.UnpanShuryouHoukokuIkkatuNyuuryoku
{
    /// <summary>
    /// 入力内容
    /// </summary>
    public class InputInfoDTOCls
    {
        ///// <summary>
        ///// グリッドの行番号
        ///// </summary>
        //public int ROW_INDEX { get; set; }

        /// <summary>
        /// 加入者番号
        /// </summary>
        public String EDI_MEMBER_ID { get; set; }
        /// <summary>
        /// 運搬業者の業者CD
        /// </summary>
        public String GYOUSHA_CD { get; set; }
        /// <summary>
        /// 管理番号
        /// </summary>
        public String KANRI_ID { get; set; }
        /// <summary>
        /// 枝番
        /// </summary>
        public String SEQ { get; set; }
        /// <summary>
        /// 区間番号
        /// </summary>
        public String UPN_ROUTE_NO { get; set; }
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
        /// 運搬終了日
        /// </summary>
        public String UNNPANN_SYURYOUHI { get; set; }
        /// <summary>
        /// 運搬担当者コード
        /// </summary>
        public String UNNPANN_TANNTOUSYA_CD { get; set; }
        /// <summary>
        /// 運搬担当者名
        /// </summary>
        public String UNNPANN_TANNTOUSYA_NAME { get; set; }
        /// <summary>
        /// 報告担当者CD
        /// </summary>
        public String HOUKOKU_TANNTOUSYA_CD { get; set; }
        /// <summary>
        /// 報告担当者名
        /// </summary>
        public String HOUKOKU_TANNTOUSYA_NAME { get; set; }
        /// <summary>
        /// 運搬量
        /// </summary>
        public String UNNPANN_RYOU { get; set; }
        /// <summary>
        /// 運搬量単位コード
        /// </summary>
        public String UNNPANNRYOU_TANNI_CD { get; set; }
        /// <summary>
        /// 運搬量単位名称
        /// </summary>
        public String UNNPANNRYOU_TANNI_NAME { get; set; }
        /// <summary>
        /// 有価物拾集量
        /// </summary>
        public String YUKABUTU_JYUUSYUU_RYOU { get; set; }
        /// <summary>
        /// 有価物拾集量単位コード
        /// </summary>
        public String YUKABUTU_JYUUSYUURYOU_TANNI_CD { get; set; }
        /// <summary>
        /// 有価物拾集量単位名称
        /// </summary>
        public String YUKABUTU_JYUUSYUURYOU_TANNI_NAME { get; set; }
        /// <summary>
        /// 車輌番号
        /// </summary>
        public String SYARYOU_CD { get; set; }
        /// <summary>
        /// 車輌名称
        /// </summary>
        public String SYARYOU_NAME { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public String BIKO { get; set; }

        /// <summary>
        /// 区間番号
        /// </summary>
        public String UPN_ROUTE_NO { get; set; }
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
        /// 検索条件 :区間番号
        /// </summary>
        public String UPN_ROUTE_NO { get; set; }
        /// <summary>
        /// 運搬業者の業者CD
        /// </summary>
        public String GYOUSHA_CD { get; set; }

    }
}
