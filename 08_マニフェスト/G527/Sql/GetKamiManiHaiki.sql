-- マニフェスト推移表 紙マニフェスト 廃棄物種類別  --

SELECT
	MANI_DTL.HAIKI_SHURUI_CD AS HAIKI_SHURUI_CD
,	M_HIK_SRI.HAIKI_SHURUI_NAME AS HAIKI_SHURUI_NAME
,	SUBSTRING(CONVERT(VARCHAR, MANI_ETY.KOUFU_DATE, 111),1,7) AS KOUFU_YM
,	SUM(MANI_DTL.KANSAN_SUU) AS KANSAN_SUU
FROM      T_MANIFEST_ENTRY AS MANI_ETY 
--マニフェスト明細
INNER JOIN
          T_MANIFEST_DETAIL AS MANI_DTL
  ON         MANI_ETY.SYSTEM_ID = MANI_DTL.SYSTEM_ID
  AND        MANI_ETY.SEQ = MANI_DTL.SEQ
  AND        MANI_DTL.KANSAN_SUU IS NOT NULL
  
--マニフェスト収集運搬２ JOIN
INNER JOIN
          (
             SELECT SYSTEM_ID, SEQ
             FROM T_MANIFEST_UPN 
             WHERE   1=1
             /*IF data.HST_UPN_GYOUSHA_CD_START != null && data.HST_UPN_GYOUSHA_CD_START != ''*/ 
             AND        UPN_GYOUSHA_CD >= /*data.HST_UPN_GYOUSHA_CD_START*/           -- 検索条件:運搬受託者From（入力がある場合）
             /*END*/

             /*IF data.HST_UPN_GYOUSHA_CD_END != null && data.HST_UPN_GYOUSHA_CD_END != ''*/ 
             AND        UPN_GYOUSHA_CD <= /*data.HST_UPN_GYOUSHA_CD_END*/             -- 検索条件:運搬受託者To（入力がある場合）
             /*END*/

             /*IF data.HST_UPN_SAKI_GYOUSHA_CD_START != null && data.HST_UPN_SAKI_GYOUSHA_CD_START != ''*/ 
             AND        UPN_SAKI_GYOUSHA_CD >= /*data.HST_UPN_SAKI_GYOUSHA_CD_START*/    -- 検索条件:処分受託者From（入力がある場合）
             /*END*/

             /*IF data.HST_UPN_SAKI_GYOUSHA_CD_END != null && data.HST_UPN_SAKI_GYOUSHA_CD_END != ''*/ 
             AND        UPN_SAKI_GYOUSHA_CD <= /*data.HST_UPN_SAKI_GYOUSHA_CD_END*/      -- 検索条件:処分受託者To（入力がある場合）
             /*END*/
             GROUP BY SYSTEM_ID, SEQ
           )  AS MANI_UPN2
             ON 1 = 1
--マニフェスト収集運搬 JOIN
INNER JOIN
          T_MANIFEST_UPN   AS MANI_UPN
   ON         MANI_UPN2.SYSTEM_ID = MANI_UPN.SYSTEM_ID
   AND        MANI_UPN2.SEQ       = MANI_UPN.SEQ
   AND        MANI_ETY.SYSTEM_ID  = MANI_UPN.SYSTEM_ID
   AND        MANI_ETY.SEQ        = MANI_UPN.SEQ
   AND        MANI_UPN.UPN_ROUTE_NO  = '1'

--廃棄物種類マスタ
INNER JOIN
          M_HAIKI_SHURUI AS M_HIK_SRI
  ON        MANI_ETY.HAIKI_KBN_CD = M_HIK_SRI.HAIKI_KBN_CD
  AND       MANI_DTL.HAIKI_SHURUI_CD = M_HIK_SRI.HAIKI_SHURUI_CD
  AND       M_HIK_SRI.DELETE_FLG = 0
  -- 直行廃棄物種類CDの入力がある場合
  /*IF data.HST_HAIKI_SHURUI_CD1 != null && data.HST_HAIKI_SHURUI_CD1 != ''*/
  AND  MANI_ETY.HAIKI_KBN_CD = 1                                                  -- 検索条件:直行固定
  AND  MANI_DTL.HAIKI_SHURUI_CD = /*data.HST_HAIKI_SHURUI_CD1*/                   -- 検索条件:直行廃棄物種類CD（入力がある場合）
  /*END*/

  -- 積替廃棄物種類CDの入力がある場合
  /*IF data.HST_HAIKI_SHURUI_CD2 != null && data.HST_HAIKI_SHURUI_CD2 != ''*/
  AND  MANI_ETY.HAIKI_KBN_CD = 3                                                  -- 検索条件:積替固定
  AND  MANI_DTL.HAIKI_SHURUI_CD = /*data.HST_HAIKI_SHURUI_CD2*/                   -- 検索条件:積替廃棄物種類CD（入力がある場合）
  /*END*/

  -- 建廃廃棄物種類CDの入力がある場合
  /*IF data.HST_HAIKI_SHURUI_CD3 != null && data.HST_HAIKI_SHURUI_CD3 != ''*/
  AND  MANI_ETY.HAIKI_KBN_CD = 2                                                  -- 検索条件:建廃固定
  AND  MANI_DTL.HAIKI_SHURUI_CD = /*data.HST_HAIKI_SHURUI_CD3*/                   -- 検索条件:建廃廃棄物種類CD（入力がある場合）
  /*END*/

--マニフェスト
AND (
     MANI_ETY.KOUFU_DATE IS NOT NULL
     AND  MANI_ETY.KOUFU_DATE <> ''
     AND  MANI_ETY.KOUFU_DATE >= CONVERT (DATETIME, /*data.DATE_START*/, 120)   -- 年月日From
     AND  MANI_ETY.KOUFU_DATE <= CONVERT (DATETIME, /*data.DATE_END*/, 120)     -- 年月日To
    )
AND MANI_ETY.FIRST_MANIFEST_KBN = CONVERT(bit,(CONVERT(int,/*data.FIRST_MANIFEST_KBN*/)-1))                   -- 検索条件:マニフェスト区分

/*IF data.KYOTEN_CD != null && data.KYOTEN_CD != ''*/
AND MANI_ETY.KYOTEN_CD = /*data.KYOTEN_CD*/                                     -- 検索条件:拠点CD （入力がある場合）
/*END*/

/*IF data.HST_GYOUSHA_CD_START != null && data.HST_GYOUSHA_CD_START != ''*/
AND  MANI_ETY.HST_GYOUSHA_CD >= /*data.HST_GYOUSHA_CD_START*/                   -- 検索条件:排出事業者From（入力がある場合）
/*END*/

/*IF data.HST_GYOUSHA_CD_END != null && data.HST_GYOUSHA_CD_END != ''*/
AND  MANI_ETY.HST_GYOUSHA_CD <= /*data.HST_GYOUSHA_CD_END*/                     -- 検索条件:排出事業者To（入力がある場合）
/*END*/

/*IF data.HST_GENBA_CD_START != null && data.HST_GENBA_CD_START != ''*/
AND  MANI_ETY.HST_GENBA_CD >= /*data.HST_GENBA_CD_START*/                       -- 検索条件:排出事業場From（入力がある場合）
/*END*/

/*IF data.HST_GENBA_CD_END != null && data.HST_GENBA_CD_END != ''*/
AND  MANI_ETY.HST_GENBA_CD <= /*data.HST_GENBA_CD_END*/                         -- 検索条件:排出事業場To（入力がある場合）
/*END*/

/*IF data.HST_LAST_SBN_GENBA_CD_START != null && data.HST_LAST_SBN_GENBA_CD_START != ''*/
AND  MANI_ETY.LAST_SBN_GENBA_CD >= /*data.HST_LAST_SBN_GENBA_CD_START*/         -- 検索条件:最終処分場CDFrom（入力がある場合）
/*END*/

/*IF data.HST_LAST_SBN_GENBA_CD_END != null && data.HST_LAST_SBN_GENBA_CD_END != ''*/
AND  MANI_ETY.LAST_SBN_GENBA_CD <= /*data.HST_LAST_SBN_GENBA_CD_END*/           -- 検索条件:最終処分場CDTo（入力がある場合）
/*END*/

AND MANI_ETY.DELETE_FLG = 0

GROUP BY     SUBSTRING(CONVERT(VARCHAR, MANI_ETY.KOUFU_DATE, 111),1,7),MANI_DTL.HAIKI_SHURUI_CD, M_HIK_SRI.HAIKI_SHURUI_NAME
ORDER BY     MANI_DTL.HAIKI_SHURUI_CD, SUBSTRING(CONVERT(VARCHAR, MANI_ETY.KOUFU_DATE, 111),1,7)


