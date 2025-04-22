 SELECT *
   FROM (SELECT SYSTEM_ID,
                SEQ,
                NULL AS KANRI_ID,
                NULL AS LATEST_SEQ,
                FIRST_MANIFEST_KBN,
                HAIKI_KBN_CD
           FROM (SELECT * FROM T_MANIFEST_ENTRY WHERE DELETE_FLG = 0) TME
          UNION ALL
         SELECT (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DETAIL_SYSTEM_ID ELSE R18EX.SYSTEM_ID END) AS SYSTEM_ID,
                 (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.SEQ ELSE R18EX.SEQ END) AS SEQ,
                DMT.KANRI_ID,
                DMT.LATEST_SEQ,
                CASE
                WHEN DT_R18.FIRST_MANIFEST_FLAG IS NULL OR DT_R18.FIRST_MANIFEST_FLAG = '' OR ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 0
                THEN '0'
                ELSE '1'
                END                    AS FIRST_MANIFEST_KBN,
                CAST('4' AS SMALLINT ) AS HAIKI_KBN_CD
           FROM DT_MF_TOC DMT
     INNER JOIN DT_R18
             ON DMT.KANRI_ID   = DT_R18.KANRI_ID
            AND DMT.LATEST_SEQ = DT_R18.SEQ
            AND DT_R18.MANIFEST_ID IS NOT NULL
            AND DT_R18.MANIFEST_ID <> ''
     INNER JOIN DT_R18_EX R18EX
             ON R18EX.KANRI_ID   = DT_R18.KANRI_ID
            AND R18EX.DELETE_FLG = 0
     LEFT JOIN (SELECT * FROM DT_R18_MIX WHERE DELETE_FLG = 0) AS MIX
             ON R18EX.SYSTEM_ID = MIX.SYSTEM_ID
     INNER JOIN DT_R19_EX MAXR19EX
             ON R18EX.SYSTEM_ID = MAXR19EX.SYSTEM_ID
            AND R18EX.SEQ       = MAXR19EX.SEQ
            AND MAXR19EX.DELETE_FLG = 0
     INNER JOIN DT_R19 MAXR19
             ON DMT.KANRI_ID     = MAXR19.KANRI_ID
            AND DMT.LATEST_SEQ          = MAXR19.SEQ
            AND MAXR19EX.UPN_ROUTE_NO = MAXR19.UPN_ROUTE_NO
     INNER JOIN DT_R19_EX MINR19EX
             ON R18EX.SYSTEM_ID       = MINR19EX.SYSTEM_ID
            AND R18EX.SEQ             = MINR19EX.SEQ
            AND MINR19EX.UPN_ROUTE_NO = 1
            AND MINR19EX.DELETE_FLG = 0
     LEFT JOIN M_GYOUSHA AS HST_GYOUSHA
             ON R18EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
         ) MANI
     WHERE HAIKI_KBN_CD       = /*data.HAIKI_KBN_CD*/''
       AND FIRST_MANIFEST_KBN = /*data.FIRST_MANIFEST_KBN*/''
       /*IF data.HAIKI_KBN_CD == '4'*/
       AND KANRI_ID           = /*data.KANRI_ID*/''
       /*IF data.LATEST_SEQ != null && data.LATEST_SEQ != ''*/ AND LATEST_SEQ         = /*data.LATEST_SEQ*/''/*END*/
       --ELSE
       AND SYSTEM_ID          = /*data.SYSTEM_ID*/''
       /*IF data.SEQ != null && data.SEQ != ''*/ AND SEQ                = /*data.SEQ*/''/*END*/
       /*END*/