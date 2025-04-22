-- マニフェスト推移表 電子マニフェスト 排出事業者別  --

SELECT
	HST_GYOUSHA_CD
,	M_GSH.GYOUSHA_NAME_RYAKU
,	HST_GENBA_CD
,	M_GNB.GENBA_NAME_RYAKU
,	HAIKI_SHURUI_CD
,	HAIKI_SHURUI_NAME
,	KOUFU_YM
,	KANSAN_SUU
FROM
(
SELECT
          R18EX.HST_GYOUSHA_CD AS HST_GYOUSHA_CD,R18EX.HST_GENBA_CD AS HST_GENBA_CD,
          R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE AS HAIKI_SHURUI_CD,
          M_HIK_SRI.HAIKI_SHURUI_NAME AS HAIKI_SHURUI_NAME,
          SUBSTRING(R18.HIKIWATASHI_DATE,1,4) + '/' +SUBSTRING(R18.HIKIWATASHI_DATE,5,2) AS KOUFU_YM,
          SUM(R18EX.KANSAN_SUU) AS KANSAN_SUU
FROM      DT_MF_TOC AS TOC
--R18 マニフェスト情報
INNER JOIN
          DT_R18 AS R18
  ON         TOC.KANRI_ID        = R18.KANRI_ID
  AND        TOC.LATEST_SEQ      = R18.SEQ
  AND        TOC.MANIFEST_ID     = R18.MANIFEST_ID

--電子マニフェスト基本拡張
INNER JOIN
          DT_R18_EX AS R18EX
  ON         R18.KANRI_ID = R18EX.KANRI_ID
  AND        R18EX.DELETE_FLG = 0
  AND        R18EX.KANSAN_SUU IS NOT NULL

  /*IF data.HST_GYOUSHA_CD_START != null && data.HST_GYOUSHA_CD_START != ''*/
  AND        R18EX.HST_GYOUSHA_CD >= /*data.HST_GYOUSHA_CD_START*/                   -- 検索条件:排出事業者From（入力がある場合）
  /*END*/

  /*IF data.HST_GYOUSHA_CD_END != null && data.HST_GYOUSHA_CD_END != ''*/
  AND        R18EX.HST_GYOUSHA_CD <= /*data.HST_GYOUSHA_CD_END*/                     -- 検索条件:排出事業者To（入力がある場合）
  /*END*/

  /*IF data.HST_GENBA_CD_START != null && data.HST_GENBA_CD_START != ''*/
  AND        R18EX.HST_GENBA_CD >= /*data.HST_GENBA_CD_START*/                       -- 検索条件:排出事業場From（入力がある場合）
  /*END*/

  /*IF data.HST_GENBA_CD_END != null && data.HST_GENBA_CD_END != ''*/
  AND        R18EX.HST_GENBA_CD <= /*data.HST_GENBA_CD_END*/                         -- 検索条件:排出事業場To（入力がある場合）
  /*END*/

--電子マニフェスト最終処分
LEFT JOIN
          DT_R13_EX AS R13EX
  ON         R18EX.SYSTEM_ID = R13EX.SYSTEM_ID
  AND        R18EX.SEQ = R13EX.SEQ

  /*IF data.LAST_SBN_GENBA_CD_START != null && data.LAST_SBN_GENBA_CD_START != ''*/
  AND        R13EX.LAST_SBN_GENBA_CD >= /*data.LAST_SBN_GENBA_CD_START*/               -- 検索条件:最終処分場所From（入力がある場合）
  /*END*/

  /*IF data.LAST_SBN_GENBA_CD_END != null && data.LAST_SBN_GENBA_CD_END != ''*/
  AND        R13EX.LAST_SBN_GENBA_CD <= /*data.LAST_SBN_GENBA_CD_END*/                 -- 検索条件:最終処分場所To（入力がある場合）
  /*END*/

--電子マニフェスト収集運搬拡張
INNER JOIN
        (
          SELECT SYSTEM_ID, SEQ
          FROM   DT_R19_EX
          WHERE  DELETE_FLG = 0
            /*IF data.UPN_GYOUSHA_CD_START != null && data.UPN_GYOUSHA_CD_START != ''*/
           AND        UPN_GYOUSHA_CD >= /*data.UPN_GYOUSHA_CD_START*/                     -- 検索条件:運搬受託者From（入力がある場合）
           /*END*/

           /*IF data.UPN_GYOUSHA_CD_END != null && data.UPN_GYOUSHA_CD_END != ''*/
           AND        UPN_GYOUSHA_CD <= /*data.UPN_GYOUSHA_CD_END*/                       -- 検索条件:運搬受託者To（入力がある場合）
           /*END*/

           /*IF data.HST_UPN_SAKI_GYOUSHA_CD_START != null && data.HST_UPN_SAKI_GYOUSHA_CD_START != ''*/
           AND        UPNSAKI_GYOUSHA_CD >= /*data.HST_UPN_SAKI_GYOUSHA_CD_START*/             -- 検索条件:処分受託者From（入力がある場合）
           /*END*/

           /*IF data.HST_UPN_SAKI_GYOUSHA_CD_END != null && data.HST_UPN_SAKI_GYOUSHA_CD_END != ''*/
           AND        UPNSAKI_GYOUSHA_CD <= /*data.HST_UPN_SAKI_GYOUSHA_CD_END*/               -- 検索条件:処分受託者To（入力がある場合）
           /*END*/
             GROUP BY SYSTEM_ID, SEQ
           ) AS R19EX2
          ON 1 = 1
--電子マニフェスト収集運搬拡張
INNER JOIN
          DT_R19_EX AS R19EX
  ON         R18EX.SYSTEM_ID = R19EX.SYSTEM_ID
  AND        R18EX.SEQ       = R19EX.SEQ
  AND        R19EX2.SYSTEM_ID = R19EX.SYSTEM_ID
  AND        R19EX2.SEQ       = R19EX.SEQ
  AND        R19EX.UPN_ROUTE_NO = '1'
  AND        R19EX.DELETE_FLG = 0

--電子廃棄物種類マスタ
INNER JOIN
          M_DENSHI_HAIKI_SHURUI AS M_HIK_SRI
  ON       R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE = M_HIK_SRI.HAIKI_SHURUI_CD
  AND       M_HIK_SRI.DELETE_FLG = 0
  -- 電子廃棄物種類CDの入力がある場合
  /*IF data.HAIKIBUTU_DENSHI != null && data.HAIKIBUTU_DENSHI != ''*/
  AND        HAIKI_SHURUI_CD = /*data.HAIKIBUTU_DENSHI*/                    -- 検索条件:電子廃棄物種類CD（入力がある場合）
  /*END*/

  /*IF data.FIRST_MANIFEST_KBN == '1'*/
  AND R18.FIRST_MANIFEST_FLAG IS NULL                   -- 検索条件:マニフェスト区分（1次:NULL、2次:NOT NULL）
  /*END*/

  /*IF data.FIRST_MANIFEST_KBN == '2'*/
  AND R18.FIRST_MANIFEST_FLAG IS NOT NULL                   -- 検索条件:マニフェスト区分（1次:NULL、2次:NOT NULL）
  /*END*/

AND (
     R18.HIKIWATASHI_DATE IS NOT NULL
     AND  R18.HIKIWATASHI_DATE <> ''
     AND  R18.HIKIWATASHI_DATE >=  SUBSTRING(/*data.DATE_START*/,1,4) + SUBSTRING(/*data.DATE_START*/,6,2)+SUBSTRING(/*data.DATE_START*/,9,2)   -- 年月日From 注：YYYYMMDDの文字列で画面から連携
     AND  R18.HIKIWATASHI_DATE <=  SUBSTRING(/*data.DATE_END*/,1,4) + SUBSTRING(/*data.DATE_END*/,6,2)+SUBSTRING(/*data.DATE_END*/,9,2)-- 年月日To 注：YYYYMMDDの文字列で画面から連携
    )

--排出事業者別 WHERE句 >>
AND R18EX.HST_GYOUSHA_CD IS NOT NULL AND R18EX.HST_GYOUSHA_CD <> ''
AND R18EX.HST_GENBA_CD IS NOT NULL AND R18EX.HST_GENBA_CD <> ''
--排出事業者別 WHERE句 <<

GROUP BY  R18EX.HST_GYOUSHA_CD,R18EX.HST_GENBA_CD,
          SUBSTRING(R18.HIKIWATASHI_DATE,1,4) + '/' +SUBSTRING(R18.HIKIWATASHI_DATE,5,2),
          R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE,
          M_HIK_SRI.HAIKI_SHURUI_NAME

) T_SUII

--業者マスタ JOIN
LEFT JOIN
          M_GYOUSHA AS M_GSH  
  ON        T_SUII.HST_GYOUSHA_CD = M_GSH.GYOUSHA_CD
  AND       M_GSH.DELETE_FLG = 0
--現場マスタ JOIN
LEFT JOIN
          M_GENBA AS M_GNB  
  ON        T_SUII.HST_GYOUSHA_CD = M_GNB.GYOUSHA_CD
  AND       T_SUII.HST_GENBA_CD = M_GNB.GENBA_CD
  AND       M_GNB.DELETE_FLG = 0
  
ORDER BY  
	HST_GYOUSHA_CD
,	HST_GENBA_CD
,	HAIKI_SHURUI_CD
,	KOUFU_YM