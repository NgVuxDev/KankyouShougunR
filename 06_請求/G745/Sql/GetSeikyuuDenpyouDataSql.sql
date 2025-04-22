SELECT DISTINCT
    CASE
        WHEN PUBLISHED_USER_SETTING1.PUBLISHED_USER_SETTING IS NULL
             AND PREVIOUS_TSD.SHOSHIKI_KBN IS NOT NULL 
             AND TSD.SHOSHIKI_KBN != PREVIOUS_TSD.SHOSHIKI_KBN THEN Cast(1 AS BIT)
        ELSE NEED_SETTING_USER1.NEED_SETTING_USER
    END                                            NEED_USER_CONFIRMATION,
    PUBLISHED_USER_SETTING1.PUBLISHED_USER_SETTING AS PUBLISHED_USER_SETTING,
    TSD_INXS.UPLOAD_STATUS AS UPLOAD_STATUS,
    TSD_INXS.DOWNLOAD_STATUS AS DOWNLOAD_STATUS,
    TSD.SEIKYUU_NUMBER,
    TSD.TORIHIKISAKI_CD,
    MT.TORIHIKISAKI_NAME_RYAKU,
    TSD.SHIMEBI,
	(CASE TSD.SEIKYUU_KEITAI_KBN 
		WHEN 2 THEN TSD.ZENKAI_KURIKOSI_GAKU 
        WHEN 1 THEN NULL
		ELSE 0 
		END) AS ZENKAI_KURIKOSI_GAKU,
    CASE TSD.SEIKYUU_KEITAI_KBN 
        WHEN 1 THEN NULL
        ELSE TSD.KONKAI_NYUUKIN_GAKU
    END AS KONKAI_NYUUKIN_GAKU,
    CASE TSD.SEIKYUU_KEITAI_KBN 
        WHEN 1 THEN NULL
        ELSE TSD.KONKAI_CHOUSEI_GAKU
    END AS KONKAI_CHOUSEI_GAKU,
    TSD.KONKAI_URIAGE_GAKU,
    TSD.KONKAI_SEI_UTIZEI_GAKU
        + TSD.KONKAI_SEI_SOTOZEI_GAKU
        + TSD.KONKAI_DEN_UTIZEI_GAKU
        + TSD.KONKAI_DEN_SOTOZEI_GAKU
        + TSD.KONKAI_MEI_UTIZEI_GAKU
        + TSD.KONKAI_MEI_SOTOZEI_GAKU SHOHIZEI_GAKU,
	(CASE TSD.SEIKYUU_KEITAI_KBN 
		WHEN 2 THEN TSD.KONKAI_SEIKYU_GAKU
		ELSE (TSD.KONKAI_URIAGE_GAKU + TSD.KONKAI_SEI_UTIZEI_GAKU + TSD.KONKAI_SEI_SOTOZEI_GAKU + TSD.KONKAI_DEN_UTIZEI_GAKU + TSD.KONKAI_DEN_SOTOZEI_GAKU + TSD.KONKAI_MEI_UTIZEI_GAKU + TSD.KONKAI_MEI_SOTOZEI_GAKU)
	    END) AS KONKAI_SEIKYU_GAKU,
    TSD.NYUUKIN_YOTEI_BI,
    TSD.TIME_STAMP,
    TSD.SEIKYUU_DATE
    /*IF data.PrintOrder == 1*/
    ,MT.TORIHIKISAKI_FURIGANA
    /*END*/
    /*IF data.PrintOrder == 2*/
    ,TSD.TORIHIKISAKI_CD
    /*END*/
	/*IF !data.FilteringData.IsNull && data.FilteringData == 2*/
	,TSDE.SEIKYUU_NUMBER
	/*END*/
    ,CAST(ISNULL(TSD.HIKAE_INSATSU_KBN, 0) AS BIT) AS HIKAE_INSATSU_KBN
FROM
    T_SEIKYUU_DENPYOU TSD
    OUTER APPLY(SELECT TOP 1 Cast(1 AS INT) IS_USE
                FROM   T_SEIKYUU_DETAIL TSDE
                WHERE  TSDE.SEIKYUU_NUMBER = TSD.SEIKYUU_NUMBER
                        AND ( TSDE.DENPYOU_SHURUI_CD <> 10
                                OR TSDE.DENPYOU_SHURUI_CD IS NULL )) NYUUKIN
    OUTER APPLY (SELECT TOP 1 Cast(1 AS BIT) AS NEED_SETTING_USER
                FROM   T_SEIKYUU_DENPYOU_KAGAMI TSDK
                        OUTER APPLY (SELECT TOP 1 Cast(1 AS INT) IS_DENPYOU
                                    FROM   T_SEIKYUU_DETAIL TSDE
                                    WHERE  TSDE.SEIKYUU_NUMBER = TSDK.SEIKYUU_NUMBER
                                            AND TSDE.KAGAMI_NUMBER = TSDK.KAGAMI_NUMBER
                                            AND ( TSDE.DENPYOU_SHURUI_CD <> 10
                                                    OR TSDE.DENPYOU_SHURUI_CD IS NULL )) DENPYOU
                WHERE  TSDK.SEIKYUU_NUMBER = TSD.SEIKYUU_NUMBER
                        AND ( NYUUKIN.IS_USE IS NULL
                                OR ( DENPYOU.IS_DENPYOU IS NOT NULL ) )
                        AND NOT EXISTS (SELECT *
                                        FROM   M_USER_ACCOUNT_INFO_INXS
                                                LEFT JOIN T_SEIKYUU_USER_SETTING_INXS AS USER_SETTING
                                                        ON USER_SETTING.USER_SYS_ID = M_USER_ACCOUNT_INFO_INXS.SYS_ID
                                                        AND USER_SETTING.SEIKYUU_NUMBER = TSDK.SEIKYUU_NUMBER
                                                        AND USER_SETTING.KAGAMI_NUMBER = TSDK.KAGAMI_NUMBER
                                        WHERE  ( ( TSD.SHOSHIKI_KBN = 1
                                                    AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_TORIHIKISAKI_CD = TSDK.TORIHIKISAKI_CD )
                                                    OR ( TSD.SHOSHIKI_KBN = 2
                                                        AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_TORIHIKISAKI_CD = TSDK.TORIHIKISAKI_CD
                                                                OR ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GYOUSHA_CD = TSDK.GYOUSHA_CD
                                                                    AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD IS NULL
                                                                            OR M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD = '' ) ) ) )
                                                    OR ( TSD.SHOSHIKI_KBN = 3
                                                        AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_TORIHIKISAKI_CD = TSDK.TORIHIKISAKI_CD
                                                                OR ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GYOUSHA_CD = TSDK.GYOUSHA_CD
                                                                    AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD IS NULL
                                                                            OR M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD = '' ) )
                                                                OR ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GYOUSHA_CD = TSDK.GYOUSHA_CD
                                                                    AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD = TSDK.GENBA_CD
                                                                    AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD IS NOT NULL
                                                                    AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD != '' ) ) ) )
                                                AND ( ( M_USER_ACCOUNT_INFO_INXS.TOUKETSU_FLG IS NULL
                                                        OR M_USER_ACCOUNT_INFO_INXS.TOUKETSU_FLG = 0 )
                                                        OR ( M_USER_ACCOUNT_INFO_INXS.TOUKETSU_FLG = 1
                                                            AND CONVERT(DATE, M_USER_ACCOUNT_INFO_INXS.TOUKETSU_DATE) > CONVERT(DATE, Getdate()) ) )
                                                AND M_USER_ACCOUNT_INFO_INXS.DELETE_FLG = 0
                                                AND M_USER_ACCOUNT_INFO_INXS.REGISTER_STATUS <> 2
                                                AND ( ( USER_SETTING.SEIKYUU_NUMBER IS NOT NULL
                                                        AND USER_SETTING.IS_SEND = Cast(1 AS BIT) )
                                                        OR ( USER_SETTING.SEIKYUU_NUMBER IS NULL
                                                            AND M_USER_ACCOUNT_INFO_INXS.IS_SEND_BILLING = Cast(1 AS BIT) ) ))) NEED_SETTING_USER1
    OUTER APPLY (SELECT Stuff((SELECT 
								 ',' + Cast(T_SEIKYUU_DENPYOU_KAGAMI.KAGAMI_NUMBER AS VARCHAR)
                                 + '-' + Cast(T_SEIKYUU_USER_SETTING_INXS.USER_SYS_ID AS VARCHAR)
                                 + '-' + Cast(M_USER_ACCOUNT_INFO_INXS.USER_ID AS VARCHAR)
                                 + '-' + Cast(Cast(T_SEIKYUU_USER_SETTING_INXS.IS_SEND AS SMALLINT) AS VARCHAR)
                                  FROM   T_SEIKYUU_DENPYOU_KAGAMI
                                         INNER JOIN T_SEIKYUU_USER_SETTING_INXS
                                                 ON T_SEIKYUU_USER_SETTING_INXS.SEIKYUU_NUMBER = T_SEIKYUU_DENPYOU_KAGAMI.SEIKYUU_NUMBER
                                                    AND T_SEIKYUU_USER_SETTING_INXS.KAGAMI_NUMBER = T_SEIKYUU_DENPYOU_KAGAMI.KAGAMI_NUMBER
                                         INNER JOIN M_USER_ACCOUNT_INFO_INXS
                                                 ON T_SEIKYUU_USER_SETTING_INXS.USER_SYS_ID = M_USER_ACCOUNT_INFO_INXS.SYS_ID
                                  WHERE  T_SEIKYUU_DENPYOU_KAGAMI.SEIKYUU_NUMBER = TSD.SEIKYUU_NUMBER
                                         AND ( ( TSD.SHOSHIKI_KBN = 1
                                                 AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_TORIHIKISAKI_CD = T_SEIKYUU_DENPYOU_KAGAMI.TORIHIKISAKI_CD )
                                                OR ( TSD.SHOSHIKI_KBN = 2
                                                     AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_TORIHIKISAKI_CD = T_SEIKYUU_DENPYOU_KAGAMI.TORIHIKISAKI_CD
                                                            OR ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GYOUSHA_CD = T_SEIKYUU_DENPYOU_KAGAMI.GYOUSHA_CD
                                                                 AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD IS NULL
                                                                        OR M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD = '' ) ) ) )
                                                OR ( TSD.SHOSHIKI_KBN = 3
                                                     AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_TORIHIKISAKI_CD = T_SEIKYUU_DENPYOU_KAGAMI.TORIHIKISAKI_CD
                                                            OR ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GYOUSHA_CD = T_SEIKYUU_DENPYOU_KAGAMI.GYOUSHA_CD
                                                                 AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD IS NULL
                                                                        OR M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD = '' ) )
                                                            OR ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GYOUSHA_CD = T_SEIKYUU_DENPYOU_KAGAMI.GYOUSHA_CD
                                                                 AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD = T_SEIKYUU_DENPYOU_KAGAMI.GENBA_CD
                                                                 AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD IS NOT NULL
                                                                 AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD != '' ) ) ) )
                                         AND ( ( M_USER_ACCOUNT_INFO_INXS.TOUKETSU_FLG IS NULL
                                                  OR M_USER_ACCOUNT_INFO_INXS.TOUKETSU_FLG = 0 )
                                                OR ( M_USER_ACCOUNT_INFO_INXS.TOUKETSU_FLG = 1
                                                     AND CONVERT(DATE, M_USER_ACCOUNT_INFO_INXS.TOUKETSU_DATE) > CONVERT(DATE, Getdate()) ) )
                                         AND M_USER_ACCOUNT_INFO_INXS.DELETE_FLG = 0
                                         AND M_USER_ACCOUNT_INFO_INXS.REGISTER_STATUS <> 2
                                  FOR XML PATH('')), 1, 1, '') AS PUBLISHED_USER_SETTING) PUBLISHED_USER_SETTING1
    LEFT JOIN T_SEIKYUU_DENPYOU_INXS TSD_INXS
            ON TSD_INXS.SEIKYUU_NUMBER = TSD.SEIKYUU_NUMBER
    OUTER APPLY (SELECT TOP 1 TMP.SHOSHIKI_KBN
                    FROM   T_SEIKYUU_DENPYOU TMP
                    WHERE  TMP.TORIHIKISAKI_CD = TSD.TORIHIKISAKI_CD
                           AND TMP.SEIKYUU_NUMBER < TSD.SEIKYUU_NUMBER
                           AND TMP.DELETE_FLG = 0
                    ORDER  BY TMP.SEIKYUU_NUMBER DESC) AS PREVIOUS_TSD
    LEFT OUTER JOIN M_TORIHIKISAKI MT ON
        TSD.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD
	LEFT OUTER JOIN M_TORIHIKISAKI_SEIKYUU MTS ON
	    TSD.TORIHIKISAKI_CD = MTS.TORIHIKISAKI_CD
	/*IF !data.FilteringData.IsNull && data.FilteringData == 1*/
	INNER JOIN T_SEIKYUU_DETAIL AS TSDE ON 
	TSD.SEIKYUU_NUMBER = TSDE.SEIKYUU_NUMBER
	AND TSDE.DELETE_FLG = 0
	AND TSDE.DENPYOU_SHURUI_CD != 10
	/*END*/
	/*IF !data.FilteringData.IsNull && data.FilteringData == 2*/
	LEFT JOIN T_SEIKYUU_DETAIL AS TSDE ON 
	TSD.SEIKYUU_NUMBER = TSDE.SEIKYUU_NUMBER
	AND TSDE.DELETE_FLG = 0
	/*END*/
/*BEGIN*/
WHERE
    /*IF !deletechuFlg*/
    TSD.DELETE_FLG = 0
    /*END*/
    /*IF data.DenpyoHizukeFrom != null && data.DenpyoHizukeFrom != ''*/
    AND TSD.SEIKYUU_DATE >= /*data.DenpyoHizukeFrom*/'2013/01/07'
    /*END*/
    /*IF data.DenpyoHizukeTo != null && data.DenpyoHizukeTo != ''*/
    AND TSD.SEIKYUU_DATE <= /*data.DenpyoHizukeTo*/'2014/01/07'
    /*END*/
    /*IF data.HakkouKyotenCD != null && data.HakkouKyotenCD != ''*/
    AND TSD.KYOTEN_CD = /*data.HakkouKyotenCD*/''
    /*END*/
    /*IF !deletechuFlg*/
    AND TSD.SHIMEBI = /*data.Simebi*/31
    /*END*/
    /*IF data.SeikyuPaper < 3*/
    AND TSD.YOUSHI_KBN = /*data.SeikyuPaper*/2
	--ELSE
	/*IF data.SeikyuPaper == 3*/
	AND MTS.YOUSHI_KBN = 1
	/*END*/
	/*IF data.SeikyuPaper == 4*/
	AND MTS.YOUSHI_KBN = 2
	/*END*/
    /*END*/
	/*IF data.TorihikisakiCD != null && data.TorihikisakiCD != ''*/
	AND TSD.TORIHIKISAKI_CD = /*data.TorihikisakiCD*/''
	/*END*/
    /*IF !data.HikaeInsatsuKbn.IsNull*/
	AND ISNULL(TSD.HIKAE_INSATSU_KBN, 0) = /*data.HikaeInsatsuKbn*/1
	/*END*/
	/*IF !data.FilteringData.IsNull && data.FilteringData == 2*/
	AND 
	(
		TSD.SEIKYUU_NUMBER IS NOT NULL
		OR NOT (
			CASE TSD.SEIKYUU_KEITAI_KBN 
			WHEN 2 THEN
			ISNULL(TSD.ZENKAI_KURIKOSI_GAKU, 0)
			ELSE 0
			END = 0
			AND
			CASE TSD.SEIKYUU_KEITAI_KBN 
			WHEN 2 THEN ISNULL(TSD.KONKAI_SEIKYU_GAKU, 0)
			ELSE (TSD.KONKAI_URIAGE_GAKU + TSD.KONKAI_SEI_UTIZEI_GAKU + TSD.KONKAI_SEI_SOTOZEI_GAKU + TSD.KONKAI_DEN_UTIZEI_GAKU + TSD.KONKAI_DEN_SOTOZEI_GAKU + TSD.KONKAI_MEI_UTIZEI_GAKU + TSD.KONKAI_MEI_SOTOZEI_GAKU)
			END = 0
		)
	)
	/*END*/

	AND MTS.INXS_SEIKYUU_KBN = 1

    /*IF data.UploadStatus != null*/
    AND ISNULL(TSD_INXS.UPLOAD_STATUS,1) = /*data.UploadStatus*/
    /*END*/
    /*IF data.ZeroKingakuTaishogai*/
	--今回御請求額
	AND (
		 (TSD.SHOSHIKI_KBN != 1 
		 AND EXISTS (SELECT 1 
					   FROM T_SEIKYUU_DENPYOU_KAGAMI TSDK
					   WHERE
					   TSDK.SEIKYUU_NUMBER = TSD.SEIKYUU_NUMBER
					   AND (ISNULL(TSDK.KONKAI_URIAGE_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU,0) + 
							 ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU,0) <> 0)))
		OR
		(TSD.SHOSHIKI_KBN = 1
		 AND (CASE TSD.SEIKYUU_KEITAI_KBN 
				WHEN 2 THEN ISNULL(TSD.KONKAI_SEIKYU_GAKU, 0)
				ELSE (ISNULL(TSD.KONKAI_URIAGE_GAKU,0) + 
					  ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0)+ 
					  ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) + 
					  ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0))
				END) <> 0))
	/*END*/
    /*IF data.PrintOrder == 1*/
    ORDER BY MT.TORIHIKISAKI_FURIGANA
    /*END*/
    /*IF data.PrintOrder == 2*/
    ORDER BY TSD.TORIHIKISAKI_CD
    /*END*/
    ,TSD.SEIKYUU_NUMBER
/*END*/
