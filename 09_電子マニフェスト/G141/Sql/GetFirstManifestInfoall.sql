--中間処理産業廃棄物情報
SELECT
    --管理番号
    '' AS KANRI_ID,
    --マニフェスト／予約番号
    '' AS R18_MANIFEST_ID,
    --マニフェスト.システムID
    TMD.DETAIL_SYSTEM_ID AS SYSTEM_ID,
    --マニフェスト.廃棄物区分CD
    TME.HAIKI_KBN_CD AS HAIKI_KBN_CD,
    --NULL
    '' AS RENRAKU_ID1,
    --NULL
    '' AS RENRAKU_ID2,
    --NULL
    '' AS RENRAKU_ID3,
    --マニフェスト.排出事業者CD
    TME.HST_GYOUSHA_CD AS HST_GYOUSHA_CD,
    --マニフェスト.排出事業者名称
    TME.HST_GYOUSHA_NAME AS HST_GYOUSHA_NAME,
    --マニフェスト.排出事業場CD
    TME.HST_GENBA_CD AS HST_GENBA_CD,
    --マニフェスト.排出事業場名称
    TME.HST_GENBA_NAME AS HST_GENBA_NAME,
    --マニフェスト.交付年月日
    CASE
        WHEN TME.KOUFU_DATE IS NULL THEN ''
        ELSE CONVERT(nvarchar, TME.KOUFU_DATE, 112)
    END AS KOUFU_DATE,
    --マニフェスト明細.処分終了日
    CASE
        WHEN TMD.SBN_END_DATE IS NULL THEN ''
        ELSE CONVERT(nvarchar, TMD.SBN_END_DATE, 112)
    END AS SBN_END_DATE,
    --マニフェスト明細.廃棄物種類CD
    TMD.HAIKI_SHURUI_CD AS HAIKI_SHURUI_CD,
    --廃棄物種類マスタ.廃棄物種類略称名
    MHS.HAIKI_SHURUI_NAME_RYAKU AS HAIKI_SHURUI_NAME_RYAKU,
    --マニフェスト明細.数量
    TMD.HAIKI_SUU AS HAIKI_SUU,
    --マニフェスト明細.単位CD
    TMD.HAIKI_UNIT_CD AS HAIKI_UNIT_CD,
    --単位マスタ.単位略称名
    MU.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU
FROM
    --マニフェスト
    T_MANIFEST_ENTRY TME
    --マニフェスト明細
    INNER JOIN T_MANIFEST_DETAIL TMD ON TME.SYSTEM_ID = TMD.SYSTEM_ID
                                    AND TME.SEQ = TMD.SEQ
    --マニフェスト紐付
    LEFT JOIN T_MANIFEST_RELATION TMR ON TMR.FIRST_SYSTEM_ID = TMD.DETAIL_SYSTEM_ID
                                     AND TMR.DELETE_FLG = 0
    --廃棄物種類
    LEFT JOIN M_HAIKI_SHURUI MHS ON MHS.HAIKI_KBN_CD = TME.HAIKI_KBN_CD
                                AND MHS.HAIKI_SHURUI_CD = TMD.HAIKI_SHURUI_CD
    --単位マスタ
    LEFT JOIN M_UNIT MU ON MU.UNIT_CD = TMD.HAIKI_UNIT_CD
--マニフェスト．交付番号　＝　【中間処理産業廃棄物-マニフェスト番号／交付】
--マニフェスト．1次マニフェスト区分　＝　０
--マニフェスト．削除フラグ　＝　０
--マニフェスト紐付．1次システムID　IS　NULL
WHERE TME.MANIFEST_ID = /*search.MANIFEST_ID*/
  AND TME.FIRST_MANIFEST_KBN = 0
  AND TME.DELETE_FLG = 0

UNION ALL

SELECT
    --マニフェスト情報．管理番号
    DT_R18.KANRI_ID AS KANRI_ID,
    --マニフェスト情報．マニフェスト／予約番号
    DT_R18.MANIFEST_ID AS R18_MANIFEST_ID,
    --電子マニフェスト基本拡張.システムID
    DT_R18_EX.SYSTEM_ID AS SYSTEM_ID,
    --'4' AS 廃棄物区分CD
    '4' AS HAIKI_KBN_CD,
    --連絡番号１.連絡番号
    R05_1.RENRAKU_ID AS RENRAKU_ID1,
    --連絡番号２.連絡番号
    R05_2.RENRAKU_ID AS RENRAKU_ID2,
    --連絡番号３.連絡番号
    R05_3.RENRAKU_ID AS RENRAKU_ID3,
    --電子マニフェスト基本拡張.排出事業者CD
    DT_R18_EX.HST_GYOUSHA_CD AS HST_GYOUSHA_CD,
    --R18 マニフェスト情報.排出事業者名称
    DT_R18.HST_SHA_NAME AS HST_SHA_NAME,
    --電子マニフェスト基本拡張.排出事業場CD
    DT_R18_EX.HST_GENBA_CD AS HST_GENBA_CD,
    --R18 マニフェスト情報.排出事業場名称
    DT_R18.HST_JOU_NAME AS HST_JOU_NAME,
    --R18 マニフェスト情報.引渡し日
    DT_R18.HIKIWATASHI_DATE AS HIKIWATASHI_DATE,
    --R18 マニフェスト情報.処分終了日
    DT_R18.SBN_END_DATE AS SBN_END_DATE,
    --R18 マニフェスト情報.大分類CD
    --  R18 マニフェスト情報.中分類CD
    --  R18 マニフェスト情報.小分類CD
    CASE
        WHEN DT_R18.HAIKI_DAI_CODE IS NULL THEN ''
        ELSE DT_R18.HAIKI_DAI_CODE
    END +
    CASE
        WHEN DT_R18.HAIKI_CHU_CODE IS NULL THEN ''
        ELSE DT_R18.HAIKI_CHU_CODE
    END +
    CASE
        WHEN DT_R18.HAIKI_SHO_CODE IS NULL THEN ''
        ELSE DT_R18.HAIKI_SHO_CODE
    END AS HAIKI_SHURUI_CD,
    --R18 マニフェスト情報.廃棄物の種類
    DT_R18.HAIKI_SHURUI AS HAIKI_SHURUI,
    --R18 マニフェスト情報.廃棄物の数量
    DT_R18.HAIKI_SUU AS HAIKI_SUU,
    --R18 マニフェスト情報.廃棄物の数量単位コード
    DT_R18.HAIKI_UNIT_CODE AS HAIKI_UNIT_CODE,
    --単位マスタ.単位略称名
    MU.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU
FROM
    --マニフェスト目次情報
    DT_MF_TOC DMT
    --R18 マニフェスト情報
    INNER JOIN DT_R18 ON DT_R18.KANRI_ID = DMT.KANRI_ID
                     AND DT_R18.SEQ = DMT.LATEST_SEQ
    --電子マニフェスト基本拡張
	LEFT JOIN DT_R18_EX ON DT_R18_EX.KANRI_ID = DT_R18.KANRI_ID
	                   AND DT_R18_EX.DELETE_FLG = 0
    --マニフェスト紐付
    LEFT JOIN T_MANIFEST_RELATION TMR ON TMR.FIRST_SYSTEM_ID = DT_R18_EX.SYSTEM_ID
                                     AND TMR.DELETE_FLG = 0
    --LEFT JOIN R05 連絡番号情報1
    LEFT JOIN DT_R05 R05_1 ON R05_1.KANRI_ID = DT_R18.KANRI_ID
                          AND R05_1.SEQ = DT_R18.SEQ
                          AND R05_1.RENRAKU_ID_NO = 1
    --LEFT JOIN R05 連絡番号情報2
    LEFT JOIN DT_R05 R05_2 ON R05_2.KANRI_ID = DT_R18.KANRI_ID
                          AND R05_2.SEQ = DT_R18.SEQ
                          AND R05_2.RENRAKU_ID_NO = 2
    --LEFT JOIN R05 連絡番号情報3
    LEFT JOIN DT_R05 R05_3 ON R05_3.KANRI_ID = DT_R18.KANRI_ID
                          AND R05_3.SEQ = DT_R18.SEQ
                          AND R05_3.RENRAKU_ID_NO = 3
    --単位マスタ
    LEFT JOIN M_UNIT MU ON MU.UNIT_CD = DT_R18.HAIKI_UNIT_CODE
                       AND MU.DENSHI_USE_KBN = 1
    LEFT JOIN M_GYOUSHA ON DT_R18_EX.HST_GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD
--マニフェスト目次情報．マニフェスト／予約番号　＝　【中間処理産業廃棄物-マニフェスト番号／交付】
--かつ　R18 マニフェスト情報．中間処理産業廃棄物情報管理方法フラグ　IS　NULL
--かつ　電子マニフェスト基本拡張．削除フラグ　＝　０
--かつ　マニフェスト紐付．1次システムID　IS　NULL
WHERE DMT.MANIFEST_ID = /*search.MANIFEST_ID*/
  AND (DT_R18.FIRST_MANIFEST_FLAG IS NULL OR DT_R18.FIRST_MANIFEST_FLAG = '' OR M_GYOUSHA.JISHA_KBN = 0)
