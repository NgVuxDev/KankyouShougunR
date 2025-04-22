SELECT '持込'                HANNYU_UKETSUKE_KBN,
       SHARYOU_NAME          HANNYU_SHARYOU_NAME,
       SHASHU_NAME           HANNYU_SHASHU_NAME,
       UNPAN_GYOUSHA_NAME    HANNYU_UNPAN_GYOUSHA_NAME,
       ''                    HANNYU_UNTENSHA_NAME,
       TORIHIKISAKI_NAME     HANNYU_TORIHIKISAKI_NAME,
       GYOUSHA_NAME          HANNYU_GYOUSHA_NAME,
       GENBA_NAME            HANNYU_GENBA_NAME,
       NIOROSHI_GYOUSHA_NAME HANNYU_NIOROSHI_GYOUSHA_NAME,
       NIOROSHI_GENBA_NAME   HANNYU_NIOROSHI_GENBA_NAME,
       UKETSUKE_NUMBER       HANNYU_UKETSUKE_NUMBER,
       UKETSUKE_DATE         HANNYU_UKETSUKE_DATE
  FROM T_UKETSUKE_MK_ENTRY
 WHERE DELETE_FLG = 0
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
 ORDER BY 
       HANNYU_UKETSUKE_NUMBER,
       HANNYU_UKETSUKE_DATE