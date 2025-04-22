SELECT
  TSD.SEIKYUU_NUMBER                                               --請求番号
  , TSDKE.KAGAMI_NUMBER                                            --鑑番号
  , TSDKE.ROW_NUMBER                                               --行番号
  , TSDKE.DENPYOU_SHURUI_CD                                        --伝票種類CD
  , TSDKE.DENPYOU_SYSTEM_ID                                        --伝票システムID
  , TSDKE.DENPYOU_SEQ                                              --伝票枝番
  , TSDKE.DETAIL_SYSTEM_ID                                         --明細システムID
  , TSDKE.DENPYOU_NUMBER                                           --伝票番号
  , TSDKE.DENPYOU_DATE                                             --伝票日付
  , TSDKE.TSDE_TORIHIKISAKI_CD                                     --取引先CD
  , TSDKE.TSDE_GYOUSHA_CD                                          --業者CD
  , TSDKE.GYOUSHA_NAME1                                            --業者名1
  , TSDKE.GYOUSHA_NAME2                                            --業者名2
  , TSDKE.TSDE_GENBA_CD                                            --現場CD
  , TSDKE.GENBA_NAME1                                              --現場名1
  , TSDKE.GENBA_NAME2                                              --現場名2
  , TSDKE.HINMEI_CD                                                --品名CD
  , TSDKE.HINMEI_NAME                                              --品名
  , TSDKE.SUURYOU                                                  --数量
  , TSDKE.UNIT_CD                                                  --単位CD
  , TSDKE.UNIT_NAME                                                --単位名
  , TSDKE.TANKA						                               --単価
  , ISNULL(TSDKE.KINGAKU,0) AS KINGAKU                             --金額
  , ISNULL(TSDKE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU                   --内税額
  , ISNULL(TSDKE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU                   --外税額
  , ISNULL(TSDKE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --伝票内税額
  , ISNULL(TSDKE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --伝票外税額
  , TSDKE.DENPYOU_ZEI_KBN_CD                                       --伝票税区分CD
  , TSDKE.MEISAI_ZEI_KBN_CD                                        --明細税区分CD
  , TSDKE.MEISAI_BIKOU                                             --明細備考
  , TSDKE.DENPYOU_ZEI_KEISAN_KBN_CD                                --伝票税計算区分
  , TSDKE.TSDK_TORIHIKISAKI_CD                                     --取引先CD
  , TSDKE.TSDK_GYOUSHA_CD                                          --業者CD
  , TSDKE.TSDK_GENBA_CD                                            --現場CD
  , TSDKE.DAIHYOU_PRINT_KBN                                        --代表者印字区分
  , TSDKE.CORP_NAME                                                --会社名
  , TSDKE.CORP_DAIHYOU                                             --代表者名
  , TSDKE.KYOTEN_NAME_PRINT_KBN                                    --拠点名印字区分
  , TSDKE.KYOTEN_CD                                                --拠点CD
  , TSDKE.KYOTEN_NAME                                              --拠点名
  , TSDKE.KYOTEN_DAIHYOU                                           --拠点代表者名
  , TSDKE.KYOTEN_POST                                              --拠点郵便番号
  , TSDKE.KYOTEN_ADDRESS1                                          --拠点住所1
  , TSDKE.KYOTEN_ADDRESS2                                          --拠点住所2
  , TSDKE.KYOTEN_TEL                                               --拠点TEL
  , TSDKE.KYOTEN_FAX                                               --拠点FAX
  , TSDKE.SEIKYUU_SOUFU_NAME1                                      --請求書送付先1
  , TSDKE.SEIKYUU_SOUFU_NAME2                                      --請求書送付先2
  , TSDKE.SEIKYUU_SOUFU_KEISHOU1                                   --請求書送付先敬称1
  , TSDKE.SEIKYUU_SOUFU_KEISHOU2                                   --請求書送付先敬称2
  , TSDKE.SEIKYUU_SOUFU_POST                                       --請求書送付先郵便番号
  , TSDKE.SEIKYUU_SOUFU_ADDRESS1                                   --請求書送付先住所1
  , TSDKE.SEIKYUU_SOUFU_ADDRESS2                                   --請求書送付先住所2
  , TSDKE.SEIKYUU_SOUFU_BUSHO                                      --請求書送付先部署
  , TSDKE.SEIKYUU_SOUFU_TANTOU                                     --請求書送付先担当者
  , TSDKE.SEIKYUU_SOUFU_TEL                                        --請求書送付先TEL
  , TSDKE.SEIKYUU_SOUFU_FAX                                        --請求書送付先FAX
  , TSDKE.SEIKYUU_TANTOU                                           --請求担当者
  , TSDKE.BIKOU_1												  --備考1
  , TSDKE.BIKOU_2												  --備考2
  , ISNULL(TSDKE.KONKAI_URIAGE_GAKU,0) AS TSDK_KONKAI_URIAGE_GAKU            --今回売上額
  , ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_SEI_UTIZEI_GAKU    --今回請内税額
  , ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_SEI_SOTOZEI_GAKU  --今回請外税額
  , ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) AS TSDK_KONKAI_DEN_UTIZEI_GAKU    --今回伝内税額
  , ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSDK_KONKAI_DEN_SOTOZEI_GAKU  --今回伝外税額
  , ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_MEI_UTIZEI_GAKU    --今回明内税額
  , ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_MEI_SOTOZEI_GAKU  --今回明外税額
  , TSD.KYOTEN_CD AS TSD_KYOTEN_CD											--拠点CD
  , TSD.SHIMEBI																--締日
  , TSD.TORIHIKISAKI_CD AS TSD_TORIHIKISAKI_CD								--取引先CD
  , TSD.SHOSHIKI_KBN														--書式区分
  , TSD.SHOSHIKI_MEISAI_KBN													--書式明細区分
  , TSD.SEIKYUU_KEITAI_KBN													--請求形態区分
  , TSD.NYUUKIN_MEISAI_KBN													--入金明細区分
  , TSD.YOUSHI_KBN															--用紙区分
  , TSD.SEIKYUU_DATE														--請求日付
  , TSD.NYUUKIN_YOTEI_BI													--入金予定日
  , ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) AS ZENKAI_KURIKOSI_GAKU              --前回繰越額
  , ISNULL(TSD.KONKAI_NYUUKIN_GAKU,0) AS KONKAI_NYUUKIN_GAKU                --今回入金額
  , ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0) AS KONKAI_CHOUSEI_GAKU                --今回調整額
  , ISNULL(TSD.KONKAI_URIAGE_GAKU,0) AS TSD_KONKAI_URIAGE_GAKU              --今回売上額
  , ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0) AS TSD_KONKAI_SEI_UTIZEI_GAKU      --今回請内税額
  , ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_SEI_SOTOZEI_GAKU    --今回請外税額
  , ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) AS TSD_KONKAI_DEN_UTIZEI_GAKU      --今回伝内税額
  , ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSD_KONKAI_DEN_SOTOZEI_GAKU    --今回伝外税額
  , ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) AS TSD_KONKAI_MEI_UTIZEI_GAKU      --今回明内税額
  , ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_MEI_SOTOZEI_GAKU    --今回明外税額
  , ISNULL(TSD.KONKAI_SEIKYU_GAKU,0) AS KONKAI_SEIKYU_GAKU                  --今回御請求額
  , TSD.FURIKOMI_BANK_CD                                          --振込銀行CD
  , TSD.FURIKOMI_BANK_NAME                                        --振込銀行名
  , TSD.FURIKOMI_BANK_SHITEN_CD                                   --振込銀行支店CD
  , TSD.FURIKOMI_BANK_SHITEN_NAME                                 --振込銀行支店名
  , TSD.KOUZA_SHURUI                                              --口座種類
  , TSD.KOUZA_NO                                                  --口座番号
  , TSD.KOUZA_NAME                                                --口座名義
  , TSD.FURIKOMI_BANK_CD_2                                        --振込銀行CD2
  , TSD.FURIKOMI_BANK_NAME_2                                      --振込銀行名2
  , TSD.FURIKOMI_BANK_SHITEN_CD_2                                 --振込銀行支店CD2
  , TSD.FURIKOMI_BANK_SHITEN_NAME_2                               --振込銀行支店名2
  , TSD.KOUZA_SHURUI_2                                            --口座種類2
  , TSD.KOUZA_NO_2                                                --口座番号2
  , TSD.KOUZA_NAME_2                                              --口座名義2
  , TSD.FURIKOMI_BANK_CD_3                                        --振込銀行CD3
  , TSD.FURIKOMI_BANK_NAME_3                                      --振込銀行名3
  , TSD.FURIKOMI_BANK_SHITEN_CD_3                                 --振込銀行支店CD3
  , TSD.FURIKOMI_BANK_SHITEN_NAME_3                               --振込銀行支店名3
  , TSD.KOUZA_SHURUI_3                                            --口座種類3
  , TSD.KOUZA_NO_3                                                --口座番号3
  , TSD.KOUZA_NAME_3                                              --口座名義3
  , TSD.HAKKOU_KBN                                                --発行区分
  , TSD.SHIME_JIKKOU_NO                                           --締実行番号
  , (ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) - ISNULL(TSD.KONKAI_NYUUKIN_GAKU,0) - ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0)) AS SASIHIKIGAKU--差引繰越額
  , (ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0)
		 + ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0)) AS SYOUHIZEIGAKU--消費税額
  , (ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) AS MEISEI_SYOHIZEI
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS RANK_DENPYO_1 --伝票ランク
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS DENPYO_KINGAKU_1 --伝票金額合計
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS RANK_GENBA_1 --現場ランク
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_UCHIZEI --現場内税消費税合計
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_SOTOZEI --現場外税消費税合計
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_KINGAKU_1 --現場金額合計
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS RANK_GYOUSHA_1 --業者ランク
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_UCHIZEI --業者内税消費税合計
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_SOTOZEI --業者外税消費税合計
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_KINGAKU_1 --業者金額合計
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER) AS RANK_SEIKYU_1 --請求ランク
  , TSD.TOUROKU_NO
  , TSD.INVOICE_KBN
  , TSDKE.KONKAI_KAZEI_KBN_1     --今回課税区分１
  , TSDKE.KONKAI_KAZEI_RATE_1    --今回課税税率１
  , TSDKE.KONKAI_KAZEI_GAKU_1    --今回課税税抜金額１
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_1 --今回課税税額１
  , TSDKE.KONKAI_KAZEI_KBN_2     --今回課税区分２
  , TSDKE.KONKAI_KAZEI_RATE_2    --今回課税税率２
  , TSDKE.KONKAI_KAZEI_GAKU_2    --今回課税税抜金額２
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_2 --今回課税税額２
  , TSDKE.KONKAI_KAZEI_KBN_3     --今回課税区分３
  , TSDKE.KONKAI_KAZEI_RATE_3    --今回課税税率３
  , TSDKE.KONKAI_KAZEI_GAKU_3    --今回課税税抜金額３
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_3 --今回課税税額３
  , TSDKE.KONKAI_KAZEI_KBN_4     --今回課税区分４
  , TSDKE.KONKAI_KAZEI_RATE_4    --今回課税税率４
  , TSDKE.KONKAI_KAZEI_GAKU_4    --今回課税税抜金額４
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_4 --今回課税税額４
  , TSDKE.KONKAI_HIKAZEI_KBN     --今回非課税区分
  , TSDKE.KONKAI_HIKAZEI_GAKU    --今回非課税額
FROM
  T_SEIKYUU_DENPYOU TSD 
  INNER JOIN (
	SELECT
		TSDK.SEIKYUU_NUMBER                                             --請求番号
		, TSDK.KAGAMI_NUMBER                                            --鑑番号
		, TSDE.ROW_NUMBER                                               --行番号
		, TSDE.DENPYOU_SHURUI_CD                                        --伝票種類CD
		, TSDE.DENPYOU_SYSTEM_ID                                        --伝票システムID
		, TSDE.DENPYOU_SEQ                                              --伝票枝番
		, TSDE.DETAIL_SYSTEM_ID                                         --明細システムID
		, TSDE.DENPYOU_NUMBER                                           --伝票番号
		, TSDE.DENPYOU_DATE                                             --伝票日付
		, TSDE.TORIHIKISAKI_CD AS TSDE_TORIHIKISAKI_CD                  --取引先CD
		, TSDE.GYOUSHA_CD AS TSDE_GYOUSHA_CD                            --業者CD
		, TSDE.GYOUSHA_NAME1                                            --業者名1
		, TSDE.GYOUSHA_NAME2                                            --業者名2
		, TSDE.GENBA_CD AS TSDE_GENBA_CD                                --現場CD
		, TSDE.GENBA_NAME1                                              --現場名1
		, TSDE.GENBA_NAME2                                              --現場名2
		, TSDE.HINMEI_CD                                                --品名CD
		, TSDE.HINMEI_NAME                                              --品名
		, TSDE.SUURYOU                                                  --数量
		, TSDE.UNIT_CD                                                  --単位CD
		, TSDE.UNIT_NAME                                                --単位名
		, TSDE.TANKA					                                --単価
		, TSDE.KINGAKU                                                  --金額
		, ISNULL(TSDE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU                   --内税額
		, ISNULL(TSDE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU                   --外税額
		, ISNULL(TSDE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --伝票内税額
		, ISNULL(TSDE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --伝票外税額
		, TSDE.DENPYOU_ZEI_KBN_CD                                       --伝票税区分CD
		, TSDE.MEISAI_ZEI_KBN_CD                                        --明細税区分CD
		, TSDE.MEISAI_BIKOU                                             --明細備考
		, TSDE.DENPYOU_ZEI_KEISAN_KBN_CD                                --伝票税計算区分
		, TSDK.TORIHIKISAKI_CD AS TSDK_TORIHIKISAKI_CD                  --取引先CD
		, TSDK.GYOUSHA_CD AS TSDK_GYOUSHA_CD                            --業者CD
		, TSDK.GENBA_CD AS TSDK_GENBA_CD                                --現場CD
		, TSDK.DAIHYOU_PRINT_KBN                                        --代表者印字区分
		, TSDK.CORP_NAME                                                --会社名
		, TSDK.CORP_DAIHYOU                                             --代表者名
		, TSDK.KYOTEN_NAME_PRINT_KBN                                    --拠点名印字区分
		, TSDK.KYOTEN_CD                                                --拠点CD
		, TSDK.KYOTEN_NAME                                              --拠点名
		, TSDK.KYOTEN_DAIHYOU                                           --拠点代表者名
		, TSDK.KYOTEN_POST                                              --拠点郵便番号
		, TSDK.KYOTEN_ADDRESS1                                          --拠点住所1
		, TSDK.KYOTEN_ADDRESS2                                          --拠点住所2
		, TSDK.KYOTEN_TEL                                               --拠点TEL
		, TSDK.KYOTEN_FAX                                               --拠点FAX
		, TSDK.SEIKYUU_SOUFU_NAME1                                      --請求書送付先1
		, TSDK.SEIKYUU_SOUFU_NAME2                                      --請求書送付先2
		, TSDK.SEIKYUU_SOUFU_KEISHOU1                                   --請求書送付先敬称1
		, TSDK.SEIKYUU_SOUFU_KEISHOU2                                   --請求書送付先敬称2
		, TSDK.SEIKYUU_SOUFU_POST                                       --請求書送付先郵便番号
		, TSDK.SEIKYUU_SOUFU_ADDRESS1                                   --請求書送付先住所1
		, TSDK.SEIKYUU_SOUFU_ADDRESS2                                   --請求書送付先住所2
		, TSDK.SEIKYUU_SOUFU_BUSHO                                      --請求書送付先部署
		, TSDK.SEIKYUU_SOUFU_TANTOU                                     --請求書送付先担当者
		, TSDK.SEIKYUU_SOUFU_TEL                                        --請求書送付先TEL
		, TSDK.SEIKYUU_SOUFU_FAX                                        --請求書送付先FAX
		, TSDK.SEIKYUU_TANTOU                                           --請求担当者
		, ISNULL(TSDK.KONKAI_URIAGE_GAKU,0) AS KONKAI_URIAGE_GAKU            --今回売上額
		, ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU,0) AS KONKAI_SEI_UTIZEI_GAKU    --今回請内税額
		, ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU,0) AS KONKAI_SEI_SOTOZEI_GAKU  --今回請外税額
		, ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU,0) AS KONKAI_DEN_UTIZEI_GAKU    --今回伝内税額
		, ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU,0) AS KONKAI_DEN_SOTOZEI_GAKU  --今回伝外税額
		, ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU,0) AS KONKAI_MEI_UTIZEI_GAKU    --今回明内税額
		, ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU,0) AS KONKAI_MEI_SOTOZEI_GAKU  --今回明外税額
		, TSDK.BIKOU_1														 --備考1
		, TSDK.BIKOU_2														 --備考2
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_1,0) AS KONKAI_KAZEI_KBN_1            --今回課税区分１
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_1,0) AS KONKAI_KAZEI_RATE_1			 --今回課税税率１
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_1,0) AS KONKAI_KAZEI_GAKU_1			 --今回課税税抜金額１
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_1,0) AS KONKAI_KAZEI_ZEIGAKU_1	 --今回課税税額１
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_2,0) AS KONKAI_KAZEI_KBN_2            --今回課税区分２
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_2,0) AS KONKAI_KAZEI_RATE_2			 --今回課税税率２
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_2,0) AS KONKAI_KAZEI_GAKU_2			 --今回課税税抜金額２
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_2,0) AS KONKAI_KAZEI_ZEIGAKU_2    --今回課税税額２
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_3,0) AS KONKAI_KAZEI_KBN_3            --今回課税区分３
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_3,0) AS KONKAI_KAZEI_RATE_3			 --今回課税税率３
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_3,0) AS KONKAI_KAZEI_GAKU_3			 --今回課税税抜金額３
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_3,0) AS KONKAI_KAZEI_ZEIGAKU_3    --今回課税税額３
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_4,0) AS KONKAI_KAZEI_KBN_4            --今回課税区分４
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_4,0) AS KONKAI_KAZEI_RATE_4			 --今回課税税率４
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_4,0) AS KONKAI_KAZEI_GAKU_4			 --今回課税税抜金額４
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_4,0) AS KONKAI_KAZEI_ZEIGAKU_4    --今回課税税額４
		, ISNULL(TSDK.KONKAI_HIKAZEI_KBN,0) AS KONKAI_HIKAZEI_KBN			 --今回非課税区分
		, ISNULL(TSDK.KONKAI_HIKAZEI_GAKU,0) AS KONKAI_HIKAZEI_GAKU			 --今回非課税額
	FROM
		T_SEIKYUU_DENPYOU_KAGAMI TSDK
		LEFT OUTER JOIN 
        T_SEIKYUU_DETAIL TSDE 
        ON TSDK.SEIKYUU_NUMBER = TSDE.SEIKYUU_NUMBER AND TSDK.KAGAMI_NUMBER = TSDE.KAGAMI_NUMBER
  ) TSDKE 
  ON TSD.SEIKYUU_NUMBER = TSDKE.SEIKYUU_NUMBER
WHERE
  TSD.DELETE_FLG = 0
  AND TSD.SEIKYUU_NUMBER = /*seikyuNumber*/
 ORDER BY
   TSDKE.KAGAMI_NUMBER
   /*$orderBy*/
  , TSDKE.DENPYOU_DATE
  , TSDKE.DENPYOU_SHURUI_CD
  , TSDKE.DENPYOU_NUMBER
  , TSDKE.ROW_NUMBER
  