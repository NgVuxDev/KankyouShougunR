SELECT
    TSD.SEISAN_NUMBER                              --精算番号
  , TSDKE.KAGAMI_NUMBER                            --鑑番号
  , TSDKE.ROW_NUMBER                               --行番号
  , TSDKE.DENPYOU_SHURUI_CD                        --伝票種類CD
  , TSDKE.DENPYOU_SYSTEM_ID                        --伝票システムID
  , TSDKE.DENPYOU_SEQ                              --伝票枝番
  , TSDKE.DETAIL_SYSTEM_ID                         --明細システムID
  , TSDKE.DENPYOU_NUMBER                           --伝票番号
  , TSDKE.DENPYOU_DATE                             --伝票日付
  , TSDKE.TORIHIKISAKI_CD                          --取引先CD
  , TSDKE.GYOUSHA_CD                               --業者CD
  , TSDKE.GYOUSHA_NAME1                            --業者名1
  , TSDKE.GYOUSHA_NAME2                            --業者名2
  , TSDKE.GENBA_CD                                 --現場CD
  , TSDKE.GENBA_NAME1                              --現場名1
  , TSDKE.GENBA_NAME2                              --現場名2
  , TSDKE.HINMEI_CD                                --品名CD
  , TSDKE.HINMEI_NAME                              --品名
  , TSDKE.SUURYOU                                  --数量
  , TSDKE.UNIT_CD                                  --単位CD
  , TSDKE.UNIT_NAME                                --単位名
  , TSDKE.TANKA						               --単価
  , ISNULL(TSDKE.KINGAKU,0) AS KINGAKU             --金額
  , ISNULL(TSDKE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU   --内税額
  , ISNULL(TSDKE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU   --外税額
  , ISNULL(TSDKE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --伝票内税額
  , ISNULL(TSDKE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --伝票外税額
  , TSDKE.DENPYOU_ZEI_KBN_CD                       --伝票税区分CD
  , TSDKE.MEISAI_ZEI_KBN_CD                        --明細税区分CD
  , TSDKE.MEISAI_BIKOU                             --明細備考
  , TSDKE.DENPYOU_ZEI_KEISAN_KBN_CD                --伝票税計算区分
  , TSDKE.TORIHIKISAKI_CD                          --取引先CD
  , TSDKE.GYOUSHA_CD                               --業者CD
  , TSDKE.GENBA_CD                                 --現場CD
  , TSDKE.DAIHYOU_PRINT_KBN                        --代表者印字区分
  , TSDKE.CORP_NAME                                --会社名
  , TSDKE.CORP_DAIHYOU                             --代表者名
  , TSDKE.KYOTEN_NAME_PRINT_KBN                    --拠点名印字区分
  , TSDKE.TSDK_KYOTEN_CD AS TSDK_KYOTEN_CD         --拠点CD
  , TSDKE.KYOTEN_NAME                              --拠点名
  , TSDKE.KYOTEN_DAIHYOU                           --拠点代表者名
  , TSDKE.KYOTEN_POST                              --拠点郵便番号
  , TSDKE.KYOTEN_ADDRESS1                          --拠点住所1
  , TSDKE.KYOTEN_ADDRESS2                          --拠点住所2
  , TSDKE.KYOTEN_TEL                               --拠点TEL
  , TSDKE.KYOTEN_FAX                               --拠点FAX
  , TSDKE.SHIHARAI_SOUFU_NAME1                     --支払明細書送付先1
  , TSDKE.SHIHARAI_SOUFU_NAME2                     --支払明細書送付先2
  , TSDKE.SHIHARAI_SOUFU_KEISHOU1                  --支払明細書送付先敬称1
  , TSDKE.SHIHARAI_SOUFU_KEISHOU2                  --支払明細書送付先敬称2
  , TSDKE.SHIHARAI_SOUFU_POST                      --支払明細書送付先郵便番号
  , TSDKE.SHIHARAI_SOUFU_ADDRESS1                  --支払明細書送付先住所1
  , TSDKE.SHIHARAI_SOUFU_ADDRESS2                  --支払明細書送付先住所2
  , TSDKE.SHIHARAI_SOUFU_BUSHO                     --支払明細書送付先部署
  , TSDKE.SHIHARAI_SOUFU_TANTOU                    --支払明細書送付先担当者
  , TSDKE.SHIHARAI_SOUFU_TEL                       --支払明細書送付先TEL
  , TSDKE.SHIHARAI_SOUFU_FAX                       --支払明細書送付先FAX
  , ISNULL(TSDKE.KONKAI_SHIHARAI_GAKU,0) AS TSDK_KONKAI_SHIHARAI_GAKU        --今回支払額
  , ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_SEI_UTIZEI_GAKU    --今回請内税額
  , ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_SEI_SOTOZEI_GAKU  --今回請外税額
  , ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) AS TSDK_KONKAI_DEN_UTIZEI_GAKU    --今回伝内税額
  , ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSDK_KONKAI_DEN_SOTOZEI_GAKU  --今回伝外税額
  , ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_MEI_UTIZEI_GAKU    --今回明内税額
  , ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_MEI_SOTOZEI_GAKU  --今回明外税額
  , TSD.KYOTEN_CD                                 --拠点CD
  , TSD.SHIMEBI                                   --締日
  , TSD.TORIHIKISAKI_CD AS TSD_TORIHIKISAKI_CD    --取引先CD
  , TSD.SHOSHIKI_KBN                              --書式区分
  , TSD.SHOSHIKI_MEISAI_KBN                       --書式明細区分
  , TSD.SHOSHIKI_GENBA_KBN						  --支払明細書書式3
  , TSD.SHIHARAI_KEITAI_KBN                       --支払形態区分
  , TSD.SHUKKIN_MEISAI_KBN                        --入金明細区分
  , TSD.YOUSHI_KBN                                --用紙区分
  , TSD.SEISAN_DATE                               --精算日付
  , TSD.SHUKKIN_YOTEI_BI                          --出金予定日
  , TSDKE.BIKOU_1								  --備考1
  , TSDKE.BIKOU_2								  --備考2
  , ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) AS ZENKAI_KURIKOSI_GAKU              --前回繰越額
  , ISNULL(TSD.KONKAI_SHUKKIN_GAKU,0) AS KONKAI_SHUKKIN_GAKU                --今回出金額
  , ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0) AS KONKAI_CHOUSEI_GAKU                --今回調整額
  , ISNULL(TSD.KONKAI_SHIHARAI_GAKU,0) AS TSD_KONKAI_SHIHARAI_GAKU          --今回支払額
  , ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0) AS TSD_KONKAI_SEI_UTIZEI_GAKU      --今回請内税額
  , ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_SEI_SOTOZEI_GAKU    --今回請外税額
  , ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) AS TSD_KONKAI_DEN_UTIZEI_GAKU      --今回伝内税額
  , ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSD_KONKAI_DEN_SOTOZEI_GAKU    --今回伝外税額
  , ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) AS TSD_KONKAI_MEI_UTIZEI_GAKU      --今回明内税額
  , ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_MEI_SOTOZEI_GAKU    --今回明外税額
  , ISNULL(TSD.KONKAI_SEISAN_GAKU,0) AS KONKAI_SEISAN_GAKU                  --今回御精算額
  , TSD.HAKKOU_KBN                                --発行区分
  , TSD.SHIME_JIKKOU_NO                           --締実行番号
  , (ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) - ISNULL(TSD.KONKAI_SHUKKIN_GAKU,0) - ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0)) AS SASIHIKIGAKU --差引繰越額
  , (ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) 
        + ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0)) AS SYOUHIZEIGAKU--消費税額
  , (ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) AS MEISEI_SYOHIZEI
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS RANK_DENPYO_1 --伝票ランク
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS DENPYO_SYOHIZEI_1 --伝票消費税合計
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS DENPYO_KINGAKU_1 --伝票金額合計
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD) AS RANK_GENBA_1 --現場ランク
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD) AS GENBA_UCHIZEI --現場内税消費税合計
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD) AS GENBA_SOTOZEI --現場外税消費税合計
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD) AS GENBA_SYOHIZEI_1 --現場消費税合計
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD,TSDKE.GENBA_CD) AS GENBA_KINGAKU_1 --現場金額合計
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD) AS RANK_GYOUSHA_1 --業者ランク
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD) AS GYOUSHA_UCHIZEI --業者内税消費税合計
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD) AS GYOUSHA_SOTOZEI --業者外税消費税合計
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD) AS GYOUSHA_SYOHIZEI_1 --業者消費税合計
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.GYOUSHA_CD) AS GYOUSHA_KINGAKU_1 --業者金額合計
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER) AS RANK_SEISAN_1 --精算ランク
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER) AS SEISAN_UCHIZEI_1 --精算消費税合計(内)
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER) AS SEISAN_SOTOZEI_1 --精算消費税合計(外)
  , TSDKE.TSDK_GYOUSHA_CD                         -- 業者CD
  , TSD.TOUROKU_NO
  , TSD.INVOICE_KBN
  , ISNULL(TSDKE.KONKAI_KAZEI_KBN_1,0) AS KONKAI_KAZEI_KBN_1            --今回課税区分１
  , ISNULL(TSDKE.KONKAI_KAZEI_RATE_1,0) AS KONKAI_KAZEI_RATE_1          --今回課税率１
  , ISNULL(TSDKE.KONKAI_KAZEI_GAKU_1,0) AS KONKAI_KAZEI_GAKU_1          --今回課税税抜金額１
  , ISNULL(TSDKE.KONKAI_KAZEI_ZEIGAKU_1,0) AS KONKAI_KAZEI_ZEIGAKU_1    --今回課税税額１
  , ISNULL(TSDKE.KONKAI_KAZEI_KBN_2,0) AS KONKAI_KAZEI_KBN_2            --今回課税区分２
  , ISNULL(TSDKE.KONKAI_KAZEI_RATE_2,0) AS KONKAI_KAZEI_RATE_2          --今回課税率２
  , ISNULL(TSDKE.KONKAI_KAZEI_GAKU_2,0) AS KONKAI_KAZEI_GAKU_2          --今回課税税抜金額２
  , ISNULL(TSDKE.KONKAI_KAZEI_ZEIGAKU_2,0) AS KONKAI_KAZEI_ZEIGAKU_2    --今回課税税額２
  , ISNULL(TSDKE.KONKAI_KAZEI_KBN_3,0) AS KONKAI_KAZEI_KBN_3            --今回課税区分３
  , ISNULL(TSDKE.KONKAI_KAZEI_RATE_3,0) AS KONKAI_KAZEI_RATE_3          --今回課税率３
  , ISNULL(TSDKE.KONKAI_KAZEI_GAKU_3,0) AS KONKAI_KAZEI_GAKU_3          --今回課税税抜金額３
  , ISNULL(TSDKE.KONKAI_KAZEI_ZEIGAKU_3,0) AS KONKAI_KAZEI_ZEIGAKU_3    --今回課税税額３
  , ISNULL(TSDKE.KONKAI_KAZEI_KBN_4,0) AS KONKAI_KAZEI_KBN_4            --今回課税区分４
  , ISNULL(TSDKE.KONKAI_KAZEI_RATE_4,0) AS KONKAI_KAZEI_RATE_4          --今回課税率４
  , ISNULL(TSDKE.KONKAI_KAZEI_GAKU_4,0) AS KONKAI_KAZEI_GAKU_4          --今回課税税抜金額４
  , ISNULL(TSDKE.KONKAI_KAZEI_ZEIGAKU_4,0) AS KONKAI_KAZEI_ZEIGAKU_4    --今回課税税額４
  , ISNULL(TSDKE.KONKAI_HIKAZEI_KBN,0) AS KONKAI_HIKAZEI_KBN            --今回非課税区分
  , ISNULL(TSDKE.KONKAI_HIKAZEI_GAKU,0) AS KONKAI_HIKAZEI_GAKU          --今回非課税額
  , ISNULL(TSDKE.SHOUHIZEI_RATE,0) AS SHOUHIZEI_RATE                    --消費税率
FROM
  T_SEISAN_DENPYOU TSD 
  INNER JOIN (
    SELECT
      TSDK.SEISAN_NUMBER                              --精算番号
      , TSDK.KAGAMI_NUMBER                            --鑑番号
      , TSDE.ROW_NUMBER                               --行番号
      , TSDE.DENPYOU_SHURUI_CD                        --伝票種類CD
      , TSDE.DENPYOU_SYSTEM_ID                        --伝票システムID
      , TSDE.DENPYOU_SEQ                              --伝票枝番
      , TSDE.DETAIL_SYSTEM_ID                         --明細システムID
      , TSDE.DENPYOU_NUMBER                           --伝票番号
      , TSDE.DENPYOU_DATE                             --伝票日付
      , TSDE.GYOUSHA_CD                               --業者CD
      , TSDE.GYOUSHA_NAME1                            --業者名1
      , TSDE.GYOUSHA_NAME2                            --業者名2
      , TSDE.GENBA_CD                                 --現場CD
      , TSDE.GENBA_NAME1                              --現場名1
      , TSDE.GENBA_NAME2                              --現場名2
      , TSDE.HINMEI_CD                                --品名CD
      , TSDE.HINMEI_NAME                              --品名
      , TSDE.SUURYOU                                  --数量
      , TSDE.UNIT_CD                                  --単位CD
      , TSDE.UNIT_NAME                                --単位名
      , TSDE.TANKA						              --単価
      , TSDE.KINGAKU                                  --金額
      , ISNULL(TSDE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU   --内税額
      , ISNULL(TSDE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU   --外税額
      , ISNULL(TSDE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --伝票内税額
      , ISNULL(TSDE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --伝票外税額
      , TSDE.DENPYOU_ZEI_KBN_CD                       --伝票税区分CD
      , TSDE.MEISAI_ZEI_KBN_CD                        --明細税区分CD
      , TSDE.MEISAI_BIKOU                             --明細備考
      , TSDE.DENPYOU_ZEI_KEISAN_KBN_CD                --伝票税計算区分
      , TSDK.TORIHIKISAKI_CD                          --取引先CD
      , TSDK.DAIHYOU_PRINT_KBN                        --代表者印字区分
      , TSDK.CORP_NAME                                --会社名
      , TSDK.CORP_DAIHYOU                             --代表者名
      , TSDK.KYOTEN_NAME_PRINT_KBN                    --拠点名印字区分
      , TSDK.KYOTEN_CD AS TSDK_KYOTEN_CD              --拠点CD
      , TSDK.KYOTEN_NAME                              --拠点名
      , TSDK.KYOTEN_DAIHYOU                           --拠点代表者名
      , TSDK.KYOTEN_POST                              --拠点郵便番号
      , TSDK.KYOTEN_ADDRESS1                          --拠点住所1
      , TSDK.KYOTEN_ADDRESS2                          --拠点住所2
      , TSDK.KYOTEN_TEL                               --拠点TEL
      , TSDK.KYOTEN_FAX                               --拠点FAX
      , TSDK.SHIHARAI_SOUFU_NAME1                     --支払明細書送付先1
      , TSDK.SHIHARAI_SOUFU_NAME2                     --支払明細書送付先2
      , TSDK.SHIHARAI_SOUFU_KEISHOU1                  --支払明細書送付先敬称1
      , TSDK.SHIHARAI_SOUFU_KEISHOU2                  --支払明細書送付先敬称2
      , TSDK.SHIHARAI_SOUFU_POST                      --支払明細書送付先郵便番号
      , TSDK.SHIHARAI_SOUFU_ADDRESS1                  --支払明細書送付先住所1
      , TSDK.SHIHARAI_SOUFU_ADDRESS2                  --支払明細書送付先住所2
      , TSDK.SHIHARAI_SOUFU_BUSHO                     --支払明細書送付先部署
      , TSDK.SHIHARAI_SOUFU_TANTOU                    --支払明細書送付先担当者
      , TSDK.SHIHARAI_SOUFU_TEL                       --支払明細書送付先TEL
      , TSDK.SHIHARAI_SOUFU_FAX                       --支払明細書送付先FAX
      , ISNULL(TSDK.KONKAI_SHIHARAI_GAKU,0) AS KONKAI_SHIHARAI_GAKU        --今回支払額
      , ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU,0) AS KONKAI_SEI_UTIZEI_GAKU    --今回請内税額
      , ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU,0) AS KONKAI_SEI_SOTOZEI_GAKU  --今回請外税額
      , ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU,0) AS KONKAI_DEN_UTIZEI_GAKU    --今回伝内税額
      , ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU,0) AS KONKAI_DEN_SOTOZEI_GAKU  --今回伝外税額
      , ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU,0) AS KONKAI_MEI_UTIZEI_GAKU    --今回明内税額
      , ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU,0) AS KONKAI_MEI_SOTOZEI_GAKU  --今回明外税額
      , TSDK.GYOUSHA_CD AS TSDK_GYOUSHA_CD            --業者CD
	  , TSDK.BIKOU_1								  --備考1
	  , TSDK.BIKOU_2								  --備考2
      , ISNULL(TSDK.KONKAI_KAZEI_KBN_1,0) AS KONKAI_KAZEI_KBN_1            --今回課税区分１
	  , ISNULL(TSDK.KONKAI_KAZEI_RATE_1,0) AS KONKAI_KAZEI_RATE_1          --今回課税率１
	  , ISNULL(TSDK.KONKAI_KAZEI_GAKU_1,0) AS KONKAI_KAZEI_GAKU_1          --今回課税税抜金額１
	  , ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_1,0) AS KONKAI_KAZEI_ZEIGAKU_1    --今回課税税額１
      , ISNULL(TSDK.KONKAI_KAZEI_KBN_2,0) AS KONKAI_KAZEI_KBN_2            --今回課税区分２
	  , ISNULL(TSDK.KONKAI_KAZEI_RATE_2,0) AS KONKAI_KAZEI_RATE_2          --今回課税率２
	  , ISNULL(TSDK.KONKAI_KAZEI_GAKU_2,0) AS KONKAI_KAZEI_GAKU_2          --今回課税税抜金額２
	  , ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_2,0) AS KONKAI_KAZEI_ZEIGAKU_2    --今回課税税額２
      , ISNULL(TSDK.KONKAI_KAZEI_KBN_3,0) AS KONKAI_KAZEI_KBN_3            --今回課税区分３
	  , ISNULL(TSDK.KONKAI_KAZEI_RATE_3,0) AS KONKAI_KAZEI_RATE_3          --今回課税率３
	  , ISNULL(TSDK.KONKAI_KAZEI_GAKU_3,0) AS KONKAI_KAZEI_GAKU_3          --今回課税税抜金額３
	  , ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_3,0) AS KONKAI_KAZEI_ZEIGAKU_3    --今回課税税額３
      , ISNULL(TSDK.KONKAI_KAZEI_KBN_4,0) AS KONKAI_KAZEI_KBN_4            --今回課税区分４
	  , ISNULL(TSDK.KONKAI_KAZEI_RATE_4,0) AS KONKAI_KAZEI_RATE_4          --今回課税率４
	  , ISNULL(TSDK.KONKAI_KAZEI_GAKU_4,0) AS KONKAI_KAZEI_GAKU_4          --今回課税税抜金額４
	  , ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_4,0) AS KONKAI_KAZEI_ZEIGAKU_4    --今回課税税額４
	  , ISNULL(TSDK.KONKAI_HIKAZEI_KBN,0) AS KONKAI_HIKAZEI_KBN            --今回非課税区分
	  , ISNULL(TSDK.KONKAI_HIKAZEI_GAKU,0) AS KONKAI_HIKAZEI_GAKU          --今回非課税額
	  , ISNULL(TSDE.SHOUHIZEI_RATE,0) AS SHOUHIZEI_RATE					 --消費税率
    FROM
        T_SEISAN_DENPYOU_KAGAMI TSDK 
        LEFT OUTER JOIN (
            /*IF shukkinMeisaiKbn != '2'*/
            SELECT
                SEISAN_NUMBER
                ,KAGAMI_NUMBER
                ,MAX(ROW_NUMBER) AS ROW_NUMBER
                ,DENPYOU_SHURUI_CD
                ,MAX(DENPYOU_SYSTEM_ID) AS DENPYOU_SYSTEM_ID
                ,MAX(DENPYOU_SEQ) AS DENPYOU_SEQ
                ,MAX(DETAIL_SYSTEM_ID) AS DETAIL_SYSTEM_ID
                ,DENPYOU_NUMBER
                ,DENPYOU_DATE
                ,TORIHIKISAKI_CD
                ,NULL AS GYOUSHA_CD
                ,NULL AS GYOUSHA_NAME1
                ,NULL AS GYOUSHA_NAME2
                ,NULL AS GENBA_CD
                ,NULL AS GENBA_NAME1
                ,NULL AS GENBA_NAME2
                ,HINMEI_CD
                ,HINMEI_NAME
                ,0 AS SUURYOU
                ,NULL AS UNIT_CD
                ,'' AS UNIT_NAME
                ,0 AS TANKA
                ,SUM(KINGAKU) AS KINGAKU
                ,0.00 AS UCHIZEI_GAKU
                ,0.00 AS SOTOZEI_GAKU
                ,0.00 AS DENPYOU_UCHIZEI_GAKU
                ,0.00 AS DENPYOU_SOTOZEI_GAKU
                ,NULL AS DENPYOU_ZEI_KBN_CD
                ,NULL AS MEISAI_ZEI_KBN_CD
                ,'' AS MEISAI_BIKOU
                ,DELETE_FLG
                ,NULL AS DENPYOU_ZEI_KEISAN_KBN_CD
				,NULL AS SHOUHIZEI_RATE
            FROM
                T_SEISAN_DETAIL TSD_SHUKKIN
            WHERE
                TSD_SHUKKIN.DENPYOU_SHURUI_CD = 20
            GROUP BY TSD_SHUKKIN.SEISAN_NUMBER,TSD_SHUKKIN.KAGAMI_NUMBER,TSD_SHUKKIN.DENPYOU_SHURUI_CD,TSD_SHUKKIN.DENPYOU_NUMBER,TSD_SHUKKIN.DENPYOU_DATE,TSD_SHUKKIN.TORIHIKISAKI_CD,TSD_SHUKKIN.HINMEI_CD,TSD_SHUKKIN.HINMEI_NAME,TSD_SHUKKIN.DELETE_FLG
            UNION
            /*END*/ 
            SELECT
                SEISAN_NUMBER
                ,KAGAMI_NUMBER
                ,ROW_NUMBER
                ,DENPYOU_SHURUI_CD
                ,DENPYOU_SYSTEM_ID
                ,DENPYOU_SEQ
                ,DETAIL_SYSTEM_ID
                ,DENPYOU_NUMBER
                ,DENPYOU_DATE
                ,TORIHIKISAKI_CD
                ,GYOUSHA_CD
                ,GYOUSHA_NAME1
                ,GYOUSHA_NAME2
                ,GENBA_CD
                ,GENBA_NAME1
                ,GENBA_NAME2
                ,HINMEI_CD
                ,HINMEI_NAME
                ,SUURYOU
                ,UNIT_CD
                ,UNIT_NAME
                ,TANKA
                ,KINGAKU
                ,UCHIZEI_GAKU
                ,SOTOZEI_GAKU
                ,DENPYOU_UCHIZEI_GAKU
                ,DENPYOU_SOTOZEI_GAKU
                ,DENPYOU_ZEI_KBN_CD
                ,MEISAI_ZEI_KBN_CD
                ,MEISAI_BIKOU
                ,DELETE_FLG
                ,DENPYOU_ZEI_KEISAN_KBN_CD
				,SHOUHIZEI_RATE
            FROM
                T_SEISAN_DETAIL TSDE_A
            WHERE
                (TSDE_A.DENPYOU_SHURUI_CD <> 20 OR TSDE_A.DENPYOU_SHURUI_CD IS NULL)
        ) TSDE ON TSDE.SEISAN_NUMBER = TSDK.SEISAN_NUMBER AND TSDE.KAGAMI_NUMBER = TSDK.KAGAMI_NUMBER
  ) TSDKE ON TSD.SEISAN_NUMBER = TSDKE.SEISAN_NUMBER
WHERE
  TSD.SEISAN_NUMBER = /*seisanNumber*/
  AND TSD.DELETE_FLG = 0
  /*IF IsZeroKingakuTaishogai*/
  AND (
		 (TSD.SHOSHIKI_KBN != 1 
		 AND (ISNULL(TSDKE.KONKAI_SHIHARAI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) + 
			  ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0) <> 0))
		OR
		(TSD.SHOSHIKI_KBN = 1
		 AND (CASE TSD.SHIHARAI_KEITAI_KBN 
				WHEN 2 THEN ISNULL(TSD.KONKAI_SEISAN_GAKU, 0)
				ELSE (ISNULL(TSD.KONKAI_SHIHARAI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0)+ 
					  ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0))
				END) <> 0))
 /*END*/
 ORDER BY
   TSDKE.KAGAMI_NUMBER
   /*$orderBy*/
  , TSDKE.DENPYOU_DATE
  , TSDKE.DENPYOU_SHURUI_CD
  , TSDKE.DENPYOU_NUMBER
  , TSDKE.ROW_NUMBER
  