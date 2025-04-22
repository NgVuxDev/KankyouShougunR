SELECT *
  FROM T_UR_SH_ENTRY
 WHERE DELETE_FLG = 0
   AND DAINOU_FLG = 0
/*IF data.HidukeFrom != null*/AND DENPYOU_DATE >= /*data.HidukeFrom*/''/*END*/
/*IF data.HidukeTo != null*/AND DENPYOU_DATE <= /*data.HidukeTo*/''/*END*/
/*IF data.kyotenCd.Value != 99*/AND KYOTEN_CD = /*data.kyotenCd.Value*/0/*END*/
/*IF data.kakuteiKbn.Value != 3*/AND KAKUTEI_KBN = /*data.kakuteiKbn.Value*/0/*END*/
/*IF data.torihikisakiCd != null*/AND TORIHIKISAKI_CD = /*data.torihikisakiCd*/''/*END*/
/*IF data.gyoushaCd != null*/AND GYOUSHA_CD = /*data.gyoushaCd*/''/*END*/
/*IF data.genbaCd != null*/AND GENBA_CD = /*data.genbaCd*/''/*END*/
/*IF data.upnGyoushaCd != null*/AND UNPAN_GYOUSHA_CD = /*data.upnGyoushaCd*/''/*END*/
/*IF data.nizumiGyoushaCd != null*/AND NIZUMI_GYOUSHA_CD = /*data.nizumiGyoushaCd*/''/*END*/
/*IF data.nizumiGenbaCd != null*/AND NIZUMI_GENBA_CD = /*data.nizumiGenbaCd*/''/*END*/
/*IF data.nioroshiGyoushaCd != null*/AND NIOROSHI_GYOUSHA_CD = /*data.nioroshiGyoushaCd*/''/*END*/
/*IF data.nioroshiGenbaCd != null*/AND NIOROSHI_GENBA_CD = /*data.nioroshiGenbaCd*/''/*END*/
/*IF data.eigyouTantoushaCd != null*/AND EIGYOU_TANTOUSHA_CD = /*data.eigyouTantoushaCd*/''/*END*/
ORDER BY T_UR_SH_ENTRY.SYSTEM_ID