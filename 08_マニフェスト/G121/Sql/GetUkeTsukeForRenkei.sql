WITH UKETSUKE AS(
   SELECT SS_E.SAGYOU_DATE             AS KOUFU_DATE,
          SS_E.TORIHIKISAKI_CD         AS TORIHIKISAKI_CD,
          SS_E.TORIHIKISAKI_NAME       AS TORIHIKISAKI_NAME,
          SS_E.GYOUSHA_CD              AS GYOUSHA_CD,
          SS_E.GYOUSHA_NAME            AS GYOUSHA_NAME,
          SS_E.GENBA_CD                AS GENBA_CD,
          SS_E.GENBA_NAME              AS GENBA_NAME,
          SS_E.UNPAN_GYOUSHA_CD        AS UNPAN_GYOUSHA_CD,
          SS_E.UNPAN_GYOUSHA_NAME      AS UNPAN_GYOUSHA_NAME,
          SS_E.NIOROSHI_GYOUSHA_CD     AS NIOROSHI_GYOUSHA_CD,
          SS_E.NIOROSHI_GYOUSHA_NAME   AS NIOROSHI_GYOUSHA_NAME,
          SS_E.NIOROSHI_GENBA_CD       AS UNPANSAKI_JIGYOBA_CD,
          SS_E.NIOROSHI_GENBA_NAME     AS UNPANSAKI_JIGYOBA_NAME,
          SS_D.HINMEI_CD               AS HINMEI_CD,
          SS_E.SHASHU_CD               AS SHASHU_CD,
          SS_E.SHASHU_NAME             AS SHASHU_NAME,
          SS_E.SHARYOU_CD              AS SHARYOU_CD,
          SS_E.SHARYOU_NAME            AS SHARYOU_NAME,
          SS_E.UNTENSHA_CD             AS UNTENSHA_CD,
          SS_E.UNTENSHA_NAME           AS UNTENSHA_NAME,
          SS_E.SAGYOU_DATE             AS JUTAKU_DATE,
          1                            AS TABLEKUBUN,
          SS_E.SYSTEM_ID               AS SYSTEM_ID,
          SS_D.DETAIL_SYSTEM_ID        AS DETAIL_SYSTEM_ID,
          SS_E.MANIFEST_SHURUI_CD      AS MANIFEST_SHURUI_CD
     FROM (SELECT TOP 1
                  SS_E.*
             FROM T_UKETSUKE_SS_ENTRY  SS_E
        LEFT JOIN T_UKETSUKE_SS_DETAIL SS_D
               ON SS_E.SYSTEM_ID = SS_D.SYSTEM_ID
              AND SS_E.SEQ       = SS_D.SEQ
            WHERE SS_E.UKETSUKE_NUMBER = /*data.RENKEI_NUMBER*/0
              AND SS_E.DELETE_FLG      = 0
              /*IF data.RENKEI_ROW_NO != NULL*/
              AND SS_D.ROW_NO          = /*data.RENKEI_ROW_NO*/0
              /*END*/) SS_E
LEFT JOIN T_UKETSUKE_SS_DETAIL SS_D
       ON SS_E.SYSTEM_ID = SS_D.SYSTEM_ID
      AND SS_E.SEQ       = SS_D.SEQ
    WHERE SS_E.UKETSUKE_NUMBER = /*data.RENKEI_NUMBER*/0
      AND SS_E.DELETE_FLG      = 0
      /*IF data.RENKEI_ROW_NO != NULL*/
      AND SS_D.ROW_NO          = /*data.RENKEI_ROW_NO*/0
      /*END*/
      AND /*data.RENKEI_MANI_KBN*/0 = 1
UNION ALL
   SELECT MK_E.SAGYOU_DATE             AS KOUFU_DATE,
          MK_E.TORIHIKISAKI_CD         AS TORIHIKISAKI_CD,
          MK_E.TORIHIKISAKI_NAME       AS TORIHIKISAKI_NAME,
          MK_E.GYOUSHA_CD              AS GYOUSHA_CD,
          MK_E.GYOUSHA_NAME            AS GYOUSHA_NAME,
          MK_E.GENBA_CD                AS GENBA_CD,
          MK_E.GENBA_NAME              AS GENBA_NAME,
          MK_E.UNPAN_GYOUSHA_CD        AS UNPAN_GYOUSHA_CD,
          MK_E.UNPAN_GYOUSHA_NAME      AS UNPAN_GYOUSHA_NAME,
          MK_E.NIOROSHI_GYOUSHA_CD     AS NIOROSHI_GYOUSHA_CD,
          MK_E.NIOROSHI_GYOUSHA_NAME   AS NIOROSHI_GYOUSHA_NAME,
          MK_E.NIOROSHI_GENBA_CD       AS UNPANSAKI_JIGYOBA_CD,
          MK_E.NIOROSHI_GENBA_NAME     AS UNPANSAKI_JIGYOBA_NAME,
          MK_D.HINMEI_CD               AS HINMEI_CD,
          MK_E.SHASHU_CD               AS SHASHU_CD,
          MK_E.SHASHU_NAME             AS SHASHU_NAME,
          MK_E.SHARYOU_CD              AS SHARYOU_CD,
          MK_E.SHARYOU_NAME            AS SHARYOU_NAM,
          NULL                         AS UNTENSHA_CD,
          NULL                         AS UNTENSHA_NAME,
          MK_E.SAGYOU_DATE             AS JUTAKU_DATE,
          2                            AS TABLEKUBUN,
          MK_E.SYSTEM_ID               AS SYSTEM_ID,
          MK_D.DETAIL_SYSTEM_ID        AS DETAIL_SYSTEM_ID,
          MK_E.MANIFEST_SHURUI_CD      AS MANIFEST_SHURUI_CD
     FROM (SELECT TOP 1
                  MK_E.*
             FROM T_UKETSUKE_MK_ENTRY  MK_E
        LEFT JOIN T_UKETSUKE_MK_DETAIL MK_D
               ON MK_E.SYSTEM_ID = MK_D.SYSTEM_ID
              AND MK_E.SEQ       = MK_D.SEQ
            WHERE MK_E.UKETSUKE_NUMBER = /*data.RENKEI_NUMBER*/0
              AND MK_E.DELETE_FLG      = 0
              /*IF data.RENKEI_ROW_NO != NULL*/
              AND MK_D.ROW_NO          = /*data.RENKEI_ROW_NO*/0
              /*END*/) MK_E
LEFT JOIN T_UKETSUKE_MK_DETAIL MK_D
       ON MK_E.SYSTEM_ID = MK_D.SYSTEM_ID
      AND MK_E.SEQ       = MK_D.SEQ
    WHERE MK_E.UKETSUKE_NUMBER = /*data.RENKEI_NUMBER*/0
      AND MK_E.DELETE_FLG      = 0
      /*IF data.RENKEI_ROW_NO != NULL*/
      AND MK_D.ROW_NO          = /*data.RENKEI_ROW_NO*/0
      /*END*/
      AND /*data.RENKEI_MANI_KBN*/0 = 1
UNION ALL
   SELECT SK_E.NIZUMI_DATE             AS KOUFU_DATE,
          SK_E.TORIHIKISAKI_CD         AS TORIHIKISAKI_CD,
          SK_E.TORIHIKISAKI_NAME       AS TORIHIKISAKI_NAME,
          SK_E.NIZUMI_GYOUSHA_CD       AS GYOUSHA_CD,
          SK_E.NIZUMI_GYOUSHA_NAME     AS GYOUSHA_NAME,
          SK_E.NIZUMI_GENBA_CD         AS GENBA_CD,
          SK_E.NIZUMI_GENBA_NAME       AS GENBA_NAME,
          SK_E.UNPAN_GYOUSHA_CD        AS UNPAN_GYOUSHA_CD,
          SK_E.UNPAN_GYOUSHA_NAME      AS UNPAN_GYOUSHA_NAME,
          SK_E.GYOUSHA_CD              AS NIOROSHI_GYOUSHA_CD,
          SK_E.GYOUSHA_NAME            AS NIOROSHI_GYOUSHA_NAME,
          SK_E.GENBA_CD                AS UNPANSAKI_JIGYOBA_CD,
          SK_E.GENBA_NAME              AS UNPANSAKI_JIGYOBA_NAME,
          SK_D.HINMEI_CD               AS HINMEI_CD,
          SK_E.SHASHU_CD               AS SHASHU_CD,
          SK_E.SHASHU_NAME             AS SHASHU_NAME,
          SK_E.SHARYOU_CD              AS SHARYOU_CD,
          SK_E.SHARYOU_NAME            AS SHARYOU_NAM,
          SK_E.UNTENSHA_CD             AS UNTENSHA_CD,
          SK_E.UNTENSHA_NAME           AS UNTENSHA_NAME,
          SK_E.SAGYOU_DATE             AS JUTAKU_DATE,
          3                            AS TABLEKUBUN,
          SK_E.SYSTEM_ID               AS SYSTEM_ID,
          SK_D.DETAIL_SYSTEM_ID        AS DETAIL_SYSTEM_ID,
          SK_E.MANIFEST_SHURUI_CD      AS MANIFEST_SHURUI_CD
     FROM (SELECT TOP 1
                  SK_E.*
             FROM T_UKETSUKE_SK_ENTRY  SK_E
        LEFT JOIN T_UKETSUKE_SK_DETAIL SK_D
               ON SK_E.SYSTEM_ID = SK_D.SYSTEM_ID
              AND SK_E.SEQ       = SK_D.SEQ
            WHERE SK_E.UKETSUKE_NUMBER = /*data.RENKEI_NUMBER*/0
              AND SK_E.DELETE_FLG      = 0
              /*IF data.RENKEI_ROW_NO != NULL*/
              AND SK_D.ROW_NO          = /*data.RENKEI_ROW_NO*/0
              /*END*/) SK_E
LEFT JOIN T_UKETSUKE_SK_DETAIL SK_D
       ON SK_E.SYSTEM_ID = SK_D.SYSTEM_ID
      AND SK_E.SEQ       = SK_D.SEQ
    WHERE SK_E.UKETSUKE_NUMBER = /*data.RENKEI_NUMBER*/0
      AND SK_E.DELETE_FLG      = 0
      /*IF data.RENKEI_ROW_NO != NULL*/
      AND SK_D.ROW_NO          = /*data.RENKEI_ROW_NO*/0
      /*END*/
      AND /*data.RENKEI_MANI_KBN*/0 = 2
 )
   SELECT UKETSUKE.*,
          HAIKI.HAIKI_SHURUI_CD         AS HAIKI_SHURUI_CD,
          HAIKI.HAIKI_SHURUI_NAME_RYAKU AS HAIKI_SHURUI_NAME_RYAKU
     FROM UKETSUKE
LEFT JOIN M_HINMEI
       ON UKETSUKE.HINMEI_CD  = M_HINMEI.HINMEI_CD
LEFT JOIN M_HAIKI_SHURUI HAIKI
       ON HAIKI.HAIKI_KBN_CD    = 2
      AND HAIKI.HAIKI_SHURUI_CD = M_HINMEI.KP_HAIKI_SHURUI_CD
    WHERE TABLEKUBUN = (SELECT MIN(TABLEKUBUN) FROM UKETSUKE)