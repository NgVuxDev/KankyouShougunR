using System;

namespace Shougun.Core.Allocation.HaishaMeisai
{
    public class HaishaMeisaiDTOClass
    {
        //期間From
        public DateTime kikanFrom { get; set; }
        //期間To
        public DateTime kikanTo { get; set; }
        //運転者From
        public String untenshaFrom { get; set; }
        //運転者To
        public String untenshaTo { get; set; }
        // 作業日
        public String SAGYOU_DATE { get; set; }
        // 運転者CD
        public String UNTENSHA_CD { get; set; }
        // 運転者
        public String UNTENSHA { get; set; }
        // 種別CD
        public String SYUBETUCD { get; set; }
        // 種別
        public String SYUBETU { get; set; }
        // №
        public String ROW { get; set; }
        // マニ情報
        public String MANIFEST_SHURUI_NAME { get; set; }
        // マニ手配
        public String MANIFEST_TEHAI_NAME { get; set; }
        // 受付番号
        public String UKETSUKE_NUMBER { get; set; }
        // 現着時間名
        public String GENCHAKU_TIME_NAME { get; set; }
        // 現着時間
        public String GENCHAKU_TIME { get; set; }
        // 作業時間
        public String SAGYOU_TIME { get; set; }
        // 業者CD
        public String GYOUSHA_CD { get; set; }
        // 業者名
        public String GYOUSHA_NAME { get; set; }
        // 車種CD
        public String SHASHU_CD { get; set; }
        // 車種名
        public String SHASHU_NAME { get; set; }
        // 車輌CD
        public String SHARYOU_CD { get; set; }
        // 車輌名
        public String SHARYOU_NAME { get; set; }
        // 現場CD
        public String GENBA_CD { get; set; }
        // 現場名
        public String GENBA_NAME { get; set; }
        // 荷降先・荷積先CD
        public String NIOROSHI_NIZUMI_CD { get; set; }
        // 荷降先・荷積先
        public String NIOROSHI_NIZUMI_NAME { get; set; }
        // 現場住所
        public String GENBA_ADDRESS { get; set; }
        // 荷降先住所・荷積先住所
        public String NIOROSHI_NIZUMI_ADDRESS { get; set; }
        // 現場電話番号
        public String GENBA_TEL { get; set; }
        // 担当者名
        public String TANTOUSHA { get; set; }
        // 担当者携帯番号
        public String GENBA_KEITAI_TEL { get; set; }
        // 受付備考１
        public String UKETSUKE_BIKOU1 { get; set; }
        // 受付備考２
        public String UKETSUKE_BIKOU2 { get; set; }
        // 受付備考３
        public String UKETSUKE_BIKOU3 { get; set; }
        // 運転者指示事項１
        public String UNTENSHA_SIJIJIKOU1 { get; set; }
        // 運転者指示事項２
        public String UNTENSHA_SIJIJIKOU2 { get; set; }
        // 運転者指示事項３
        public String UNTENSHA_SIJIJIKOU3 { get; set; }
        // コンテナ種類CD
        public String CONTENA_SHURUI_CD { get; set; }
        // コンテナ種類
        public String CONTENA_SHURUI_NAME { get; set; }
        // 設置・引揚
        public String CONTENA_JOUKYOU_NAME { get; set; }
        // 台数
        public String DAISUU { get; set; }
        // 品名CD
        public String HINMEI_CD { get; set; }
        // 品名
        public String HINMEI_NAME { get; set; }
        // 数量
        public String SUURYOU { get; set; }
        // 単位
        public String UNIT_NAME { get; set; }
    }
}
