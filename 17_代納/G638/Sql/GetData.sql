WITH UKEIRE AS
(
   SELECT TUSE.SYSTEM_ID,
          TUSE.SEQ,
          TUSD.DETAIL_SYSTEM_ID,
          TUSE.UR_SH_NUMBER,
          TUSE.DENPYOU_DATE,
          TUSE.SHIHARAI_DATE,
          TUSE.UNPAN_GYOUSHA_CD,
          TUSE.UNPAN_GYOUSHA_NAME,
          TUSE.SHARYOU_CD,
          TUSE.SHARYOU_NAME,
          TUSE.TORIHIKISAKI_CD,
          TUSE.TORIHIKISAKI_NAME,
          TUSE.GYOUSHA_CD,
          TUSE.GYOUSHA_NAME,
          TUSE.GENBA_CD,
          TUSE.GENBA_NAME,
          TUSD.ROW_NO,
          TUSD.HINMEI_CD,
          TUSD.HINMEI_NAME,
          TUSD.STACK_JYUURYOU,
          TUSD.CHOUSEI_JYUURYOU,
          SUM(TUSD.NET_JYUURYOU) AS NET_JYUURYOU,
          TUSD.SUURYOU,
          TUSD.UNIT_CD,
          MU.UNIT_NAME_RYAKU AS UNIT_NAME,
          TUSD.TANKA,
          SUM(TUSD.KINGAKU + TUSD.HINMEI_KINGAKU) AS KINGAKU,
          TUSD.MEISAI_BIKOU
     FROM T_UR_SH_ENTRY TUSE
LEFT JOIN T_UR_SH_DETAIL TUSD
       ON TUSE.SYSTEM_ID = TUSD.SYSTEM_ID
      AND TUSE.SEQ = TUSD.SEQ
LEFT JOIN M_UNIT MU
       ON TUSD.UNIT_CD = MU.UNIT_CD
    WHERE TUSE.DAINOU_FLG = 1
      AND TUSE.DELETE_FLG = 0
      AND TUSD.DENPYOU_KBN_CD = 2
      /*IF data.KYOTEN_CD.Value != 99*/
      AND TUSE.KYOTEN_CD = /*data.KYOTEN_CD.Value*/''
      /*END*/
      AND TUSE.DENPYOU_DATE >= /*data.DATE_FROM.Value*/''
      AND TUSE.DENPYOU_DATE <= /*data.DATE_TO.Value*/''
      AND TUSE.TORIHIKISAKI_CD >= /*data.UKEIRE_TORI_CD_FROM*/''
      AND TUSE.TORIHIKISAKI_CD <= /*data.UKEIRE_TORI_CD_TO*/''
      /*IF data.UKEIRE_GYOUSHA_CD_FROM != null*/
      AND TUSE.GYOUSHA_CD >= /*data.UKEIRE_GYOUSHA_CD_FROM*/''
      /*END*/
      /*IF data.UKEIRE_GYOUSHA_CD_TO != null*/
      AND TUSE.GYOUSHA_CD <= /*data.UKEIRE_GYOUSHA_CD_TO*/''
      /*END*/
      /*IF data.UKEIRE_GENBA_CD_FROM != null*/
      AND TUSE.GENBA_CD >= /*data.UKEIRE_GENBA_CD_FROM*/''
      /*END*/
      /*IF data.UKEIRE_GENBA_CD_TO != null*/
      AND TUSE.GENBA_CD <= /*data.UKEIRE_GENBA_CD_TO*/''
      /*END*/
 GROUP BY GROUPING SETS
        ((TUSE.SYSTEM_ID,
          TUSE.SEQ,
          TUSD.DETAIL_SYSTEM_ID,
          TUSE.UR_SH_NUMBER,
          TUSE.DENPYOU_DATE,
          TUSE.SHIHARAI_DATE,
          TUSE.UNPAN_GYOUSHA_CD,
          TUSE.UNPAN_GYOUSHA_NAME,
          TUSE.SHARYOU_CD,
          TUSE.SHARYOU_NAME,
          TUSE.TORIHIKISAKI_CD,
          TUSE.TORIHIKISAKI_NAME,
          TUSE.GYOUSHA_CD,
          TUSE.GYOUSHA_NAME,
          TUSE.GENBA_CD,
          TUSE.GENBA_NAME,
          TUSD.ROW_NO,
          TUSD.HINMEI_CD,
          TUSD.HINMEI_NAME,
          TUSD.STACK_JYUURYOU,
          TUSD.CHOUSEI_JYUURYOU,
          TUSD.SUURYOU,
          TUSD.UNIT_CD,
          MU.UNIT_NAME_RYAKU,
          TUSD.TANKA,
          TUSD.MEISAI_BIKOU),
          (TUSE.SYSTEM_ID))
),
SHUKKA AS
(
   SELECT TUSE.SYSTEM_ID,
          TUSE.SEQ,
          TUSD.DETAIL_SYSTEM_ID,
          TUSE.UR_SH_NUMBER,
          TUSE.DENPYOU_DATE,
          TUSE.URIAGE_DATE,
          TUSE.UNPAN_GYOUSHA_CD,
          TUSE.UNPAN_GYOUSHA_NAME,
          TUSE.SHARYOU_CD,
          TUSE.SHARYOU_NAME,
          TUSE.TORIHIKISAKI_CD,
          TUSE.TORIHIKISAKI_NAME,
          TUSE.GYOUSHA_CD,
          TUSE.GYOUSHA_NAME,
          TUSE.GENBA_CD,
          TUSE.GENBA_NAME,
          TUSD.ROW_NO,
          TUSD.HINMEI_CD,
          TUSD.HINMEI_NAME,
          TUSD.STACK_JYUURYOU,
          TUSD.CHOUSEI_JYUURYOU,
          SUM(TUSD.NET_JYUURYOU) AS NET_JYUURYOU,
          TUSD.SUURYOU,
          TUSD.UNIT_CD,
          MU.UNIT_NAME_RYAKU AS UNIT_NAME,
          TUSD.TANKA,
          SUM(TUSD.KINGAKU + TUSD.HINMEI_KINGAKU) AS KINGAKU,
          TUSD.MEISAI_BIKOU
     FROM T_UR_SH_ENTRY TUSE
LEFT JOIN T_UR_SH_DETAIL TUSD
       ON TUSE.SYSTEM_ID = TUSD.SYSTEM_ID
      AND TUSE.SEQ = TUSD.SEQ
LEFT JOIN M_UNIT MU
       ON TUSD.UNIT_CD = MU.UNIT_CD
    WHERE TUSE.DAINOU_FLG = 1
      AND TUSE.DELETE_FLG = 0
      AND TUSD.DENPYOU_KBN_CD = 1
      /*IF data.KYOTEN_CD.Value != 99*/
      AND TUSE.KYOTEN_CD = /*data.KYOTEN_CD*/''
      /*END*/
      AND TUSE.DENPYOU_DATE >= /*data.DATE_FROM*/''
      AND TUSE.DENPYOU_DATE <= /*data.DATE_TO*/''
      AND TUSE.TORIHIKISAKI_CD >= /*data.SHUKKA_TORI_CD_FROM*/''
      AND TUSE.TORIHIKISAKI_CD <= /*data.SHUKKA_TORI_CD_TO*/''
      /*IF data.SHUKKA_GYOUSHA_CD_FROM != null*/
      AND TUSE.GYOUSHA_CD >= /*data.SHUKKA_GYOUSHA_CD_FROM*/''
      /*END*/
      /*IF data.SHUKKA_GYOUSHA_CD_TO != null*/
      AND TUSE.GYOUSHA_CD <= /*data.SHUKKA_GYOUSHA_CD_TO*/''
      /*END*/
      /*IF data.SHUKKA_GENBA_CD_FROM != null*/
      AND TUSE.GENBA_CD >= /*data.SHUKKA_GENBA_CD_FROM*/''
      /*END*/
      /*IF data.SHUKKA_GENBA_CD_TO != null*/
      AND TUSE.GENBA_CD <= /*data.SHUKKA_GENBA_CD_TO*/''
      /*END*/
 GROUP BY GROUPING SETS
        ((TUSE.SYSTEM_ID,
          TUSE.SEQ,
          TUSD.DETAIL_SYSTEM_ID,
          TUSE.UR_SH_NUMBER,
          TUSE.DENPYOU_DATE,
          TUSE.URIAGE_DATE,
          TUSE.UNPAN_GYOUSHA_CD,
          TUSE.UNPAN_GYOUSHA_NAME,
          TUSE.SHARYOU_CD,
          TUSE.SHARYOU_NAME,
          TUSE.TORIHIKISAKI_CD,
          TUSE.TORIHIKISAKI_NAME,
          TUSE.GYOUSHA_CD,
          TUSE.GYOUSHA_NAME,
          TUSE.GENBA_CD,
          TUSE.GENBA_NAME,
          TUSD.ROW_NO,
          TUSD.HINMEI_CD,
          TUSD.HINMEI_NAME,
          TUSD.STACK_JYUURYOU,
          TUSD.CHOUSEI_JYUURYOU,
          TUSD.SUURYOU,
          TUSD.UNIT_CD,
          MU.UNIT_NAME_RYAKU,
          TUSD.TANKA,
          TUSD.MEISAI_BIKOU),
          (TUSE.SYSTEM_ID))
)
    SELECT UKEIRE.UR_SH_NUMBER,
           CONVERT(varchar, UKEIRE.DENPYOU_DATE, 111) AS DENPYOU_DATE,
           CONVERT(varchar, UKEIRE.SHIHARAI_DATE, 111) AS SHIHARAI_DATE,
           CONVERT(varchar, SHUKKA.URIAGE_DATE, 111) AS URIAGE_DATE,
           UKEIRE.SHIHARAI_DATE,
           SHUKKA.DENPYOU_DATE,
           UKEIRE.UNPAN_GYOUSHA_CD                    AS UPN_GYOUSHA_CD,
           UKEIRE.UNPAN_GYOUSHA_NAME                  AS UPN_GYOUSHA_NAME,
           UKEIRE.SHARYOU_CD,
           UKEIRE.SHARYOU_NAME,
           UKEIRE.ROW_NO,
           UKEIRE.TORIHIKISAKI_CD                     AS U_TORI_CD,
           UKEIRE.TORIHIKISAKI_NAME                   AS U_TORI_NAME,
           UKEIRE.GYOUSHA_CD                          AS U_GYOUSHA_CD,
           UKEIRE.GYOUSHA_NAME                        AS U_GYOUSHA_NAME,
           UKEIRE.GENBA_CD                            AS U_GENBA_CD,
           UKEIRE.GENBA_NAME                          AS U_GENBA_NAME,
           UKEIRE.HINMEI_CD                           AS U_HINMEI_CD,
           UKEIRE.HINMEI_NAME                         AS U_HINMEI_NAME,
           UKEIRE.STACK_JYUURYOU                      AS U_SHOMI,
           UKEIRE.CHOUSEI_JYUURYOU                    AS U_CHOUSEI,
           UKEIRE.NET_JYUURYOU                        AS U_JITUSHOMI,
           UKEIRE.SUURYOU                             AS U_NUMBER,
           UKEIRE.UNIT_CD                             AS U_UNIT_CD,
           UKEIRE.UNIT_NAME                           AS U_UNIT,
           UKEIRE.TANKA                               AS U_TANKA,
           UKEIRE.KINGAKU                             AS U_KINGAKU,
           UKEIRE.MEISAI_BIKOU                        AS U_BIKOU,
           SUM_UKEIRE.NET_JYUURYOU                    AS U_JITUSHOMI_SUM,
           SUM_UKEIRE.KINGAKU                         AS U_KINGAKU_SUM,
           SHUKKA.TORIHIKISAKI_CD                     AS S_TORI_CD,
           SHUKKA.TORIHIKISAKI_NAME                   AS S_TORI_NAME,
           SHUKKA.GYOUSHA_CD                          AS S_GYOUSHA_CD,
           SHUKKA.GYOUSHA_NAME                        AS S_GYOUSHA_NAME,
           SHUKKA.GENBA_CD                            AS S_GENBA_CD,
           SHUKKA.GENBA_NAME                          AS S_GENBA_NAME,
           SHUKKA.HINMEI_CD                           AS S_HINMEI_CD,
           SHUKKA.HINMEI_NAME                         AS S_HINMEI_NAME,
           SHUKKA.STACK_JYUURYOU                      AS S_SHOMI,
           SHUKKA.CHOUSEI_JYUURYOU                    AS S_CHOUSEI,
           SHUKKA.NET_JYUURYOU                        AS S_JITUSHOMI,
           SHUKKA.SUURYOU                             AS S_NUMBER,
           SHUKKA.UNIT_CD                             AS S_UNIT_CD,
           SHUKKA.UNIT_NAME                           AS S_UNIT,
           SHUKKA.TANKA                               AS S_TANKA,
           SHUKKA.KINGAKU                             AS S_KINGAKU,
           SHUKKA.MEISAI_BIKOU                        AS S_BIKOU,
           SUM_SHUKKA.NET_JYUURYOU                    AS S_JITUSHOMI_SUM,
           SUM_SHUKKA.KINGAKU                         AS S_KINGAKU_SUM,
           SUM_SHUKKA.NET_JYUURYOU - SUM_UKEIRE.NET_JYUURYOU AS DIFF_JITUSHOMI,
           SUM_SHUKKA.KINGAKU - SUM_UKEIRE.KINGAKU    AS DIFF_KINGAKU
      FROM UKEIRE
INNER JOIN SHUKKA
        ON UKEIRE.UR_SH_NUMBER = SHUKKA.UR_SH_NUMBER
       AND UKEIRE.ROW_NO = SHUKKA.ROW_NO
       AND UKEIRE.SEQ IS NOT NULL
       AND SHUKKA.SEQ IS NOT NULL
 LEFT JOIN UKEIRE AS SUM_UKEIRE
        ON SUM_UKEIRE.SEQ IS NULL
       AND UKEIRE.SYSTEM_ID = SUM_UKEIRE.SYSTEM_ID
 LEFT JOIN SHUKKA AS SUM_SHUKKA
        ON SUM_SHUKKA.SEQ IS NULL
       AND SHUKKA.SYSTEM_ID = SUM_SHUKKA.SYSTEM_ID