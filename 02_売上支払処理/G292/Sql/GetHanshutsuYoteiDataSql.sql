SELECT UKETSUKE_KBN          HANSHUTSU_UKETSUKE_KBN,
       SHARYOU_NAME          HANSHUTSU_SHARYOU_NAME,
       SHASHU_NAME           HANSHUTSU_SHASHU_NAME,
       UNPAN_GYOUSHA_NAME    HANSHUTSU_UNPAN_GYOUSHA_NAME,
       UNTENSHA_NAME         HANSHUTSU_UNTENSHA_NAME,
       TORIHIKISAKI_NAME     HANSHUTSU_TORIHIKISAKI_NAME,
       GYOUSHA_NAME          HANSHUTSU_GYOUSHA_NAME,
       GENBA_NAME            HANSHUTSU_GENBA_NAME,
       NIOROSHI_GYOUSHA_NAME HANSHUTSU_NIOROSHI_GYOUSHA_NAME,
       NIOROSHI_GENBA_NAME   HANSHUTSU_NIOROSHI_GENBA_NAME,
       UKETSUKE_NUMBER       HANSHUTSU_UKETSUKE_NUMBER,
       UKETSUKE_DATE         HANSHUTSU_UKETSUKE_DATE
  FROM (

        /*IF data.HanshutsuShubetsu == '1' || data.HanshutsuShubetsu == '2'*/
        SELECT '収集' UKETSUKE_KBN,
               SHARYOU_NAME,
               SHASHU_NAME,
               UNPAN_GYOUSHA_NAME,
               UNTENSHA_NAME,
               TORIHIKISAKI_NAME,
               GYOUSHA_NAME,
               GENBA_NAME,
               NIOROSHI_GYOUSHA_NAME,
               NIOROSHI_GENBA_NAME,
               UKETSUKE_NUMBER,
               UKETSUKE_DATE
          FROM T_UKETSUKE_SS_ENTRY
         WHERE DELETE_FLG = 0
           AND HAISHA_JOKYO_CD IN (1,2)
           /*IF data.KyotenCd != null && data.KyotenCd != ''*/
           AND KYOTEN_CD = /*data.KyotenCd*//*END*/
           /*IF data.SharyouCd != null && data.SharyouCd != ''*/
           AND SHARYOU_CD = /*data.SharyouCd*//*END*/
           /*IF data.TorihikisakiCd != null && data.TorihikisakiCd != ''*/
           AND TORIHIKISAKI_CD = /*data.TorihikisakiCd*//*END*/
           /*IF data.GyoushaCd != null && data.GyoushaCd != ''*/
           AND GYOUSHA_CD = /*data.GyoushaCd*//*END*/
           /*IF data.GenbaCd != null && data.GenbaCd != ''*/
           AND GENBA_CD = /*data.GenbaCd*//*END*/
           /*IF data.NioroshiGyoushaCd != null && data.NioroshiGyoushaCd != ''*/
           AND NIOROSHI_GYOUSHA_CD = /*data.NioroshiGyoushaCd*//*END*/
           /*IF data.NioroshiGenbaCd != null && data.NioroshiGenbaCd != ''*/
           AND NIOROSHI_GENBA_CD = /*data.NioroshiGenbaCd*//*END*/
           AND SAGYOU_DATE IS NOT NULL
           /*IF data.SagyouDateFrom != null && data.SagyouDateFrom != ''*/
           AND SAGYOU_DATE >= /*data.SagyouDateFrom*//*END*/
           /*IF data.SagyouDateTo != null && data.SagyouDateTo != ''*/
           AND SAGYOU_DATE <= /*data.SagyouDateTo*//*END*/
        /*END*/
        /*IF data.HanshutsuShubetsu == '1'*/
        UNION ALL
        /*END*/
        /*IF data.HanshutsuShubetsu == '1' || data.HanshutsuShubetsu == '3'*/
        SELECT '出荷' UKETSUKE_KBN,
               SHARYOU_NAME,
               SHASHU_NAME,
               UNPAN_GYOUSHA_NAME,
               UNTENSHA_NAME,
               TORIHIKISAKI_NAME,
               GYOUSHA_NAME,
               GENBA_NAME,
               NIZUMI_GYOUSHA_NAME NIOROSHI_GYOUSHA_NAME,
               NIZUMI_GENBA_NAME NIOROSHI_GENBA_NAME,
               UKETSUKE_NUMBER,
               UKETSUKE_DATE
          FROM T_UKETSUKE_SK_ENTRY
         WHERE DELETE_FLG = 0
           AND HAISHA_JOKYO_CD IN (1,2)
           /*IF data.KyotenCd != null && data.KyotenCd != ''*/
           AND KYOTEN_CD = /*data.KyotenCd*//*END*/
           /*IF data.SharyouCd != null && data.SharyouCd != ''*/
           AND SHARYOU_CD = /*data.SharyouCd*//*END*/
           /*IF data.TorihikisakiCd != null && data.TorihikisakiCd != ''*/
           AND TORIHIKISAKI_CD = /*data.TorihikisakiCd*//*END*/
           /*IF data.GyoushaCd != null && data.GyoushaCd != ''*/
           AND GYOUSHA_CD = /*data.GyoushaCd*//*END*/
           /*IF data.GenbaCd != null && data.GenbaCd != ''*/
           AND GENBA_CD = /*data.GenbaCd*//*END*/
           /*IF data.NioroshiGyoushaCd != null && data.NioroshiGyoushaCd != ''*/
           AND NIZUMI_GYOUSHA_CD = /*data.NioroshiGyoushaCd*//*END*/
           /*IF data.NioroshiGenbaCd != null && data.NioroshiGenbaCd != ''*/
           AND NIZUMI_GENBA_CD = /*data.NioroshiGenbaCd*//*END*/
           AND SAGYOU_DATE IS NOT NULL
           /*IF data.SagyouDateFrom != null && data.SagyouDateFrom != ''*/
           AND SAGYOU_DATE >= /*data.SagyouDateFrom*//*END*/
           /*IF data.SagyouDateTo != null && data.SagyouDateTo != ''*/
           AND SAGYOU_DATE <= /*data.SagyouDateTo*//*END*/
        /*END*/
       ) AS HANSHUTSU
 ORDER BY 
       HANSHUTSU_UKETSUKE_NUMBER,
       HANSHUTSU_UKETSUKE_DATE
