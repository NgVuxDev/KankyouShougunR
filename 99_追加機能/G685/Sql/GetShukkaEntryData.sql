SELECT *
  FROM T_SHUKKA_ENTRY
    LEFT JOIN M_TORIHIKISAKI_SEIKYUU
     ON T_SHUKKA_ENTRY.TORIHIKISAKI_CD = M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD
 WHERE T_SHUKKA_ENTRY.DELETE_FLG = 0
   AND (T_SHUKKA_ENTRY.KENSHU_MUST_KBN != 1 OR T_SHUKKA_ENTRY.KENSHU_DATE IS NULL) 
   AND T_SHUKKA_ENTRY.TAIRYUU_KBN = 0
/*IF data.HidukeFrom != null*/AND T_SHUKKA_ENTRY.DENPYOU_DATE >= /*data.HidukeFrom*/''/*END*/
/*IF data.HidukeTo != null*/AND T_SHUKKA_ENTRY.DENPYOU_DATE <= /*data.HidukeTo*/''/*END*/
/*IF data.kyotenCd.Value != 99*/AND T_SHUKKA_ENTRY.KYOTEN_CD = /*data.kyotenCd.Value*/0/*END*/
/*IF data.kakuteiKbn.Value != 3*/AND T_SHUKKA_ENTRY.KAKUTEI_KBN = /*data.kakuteiKbn.Value*/0/*END*/
/*IF data.torihikisakiCd != null*/AND T_SHUKKA_ENTRY.TORIHIKISAKI_CD = /*data.torihikisakiCd*/''/*END*/
/*IF data.gyoushaCd != null*/AND T_SHUKKA_ENTRY.GYOUSHA_CD = /*data.gyoushaCd*/''/*END*/
/*IF data.genbaCd != null*/AND T_SHUKKA_ENTRY.GENBA_CD = /*data.genbaCd*/''/*END*/
/*IF data.upnGyoushaCd != null*/AND T_SHUKKA_ENTRY.UNPAN_GYOUSHA_CD = /*data.upnGyoushaCd*/''/*END*/
/*IF data.nizumiGyoushaCd != null*/AND T_SHUKKA_ENTRY.NIZUMI_GYOUSHA_CD = /*data.nizumiGyoushaCd*/''/*END*/
/*IF data.nizumiGenbaCd != null*/AND T_SHUKKA_ENTRY.NIZUMI_GENBA_CD = /*data.nizumiGenbaCd*/''/*END*/
/*IF data.eigyouTantoushaCd != null*/AND T_SHUKKA_ENTRY.EIGYOU_TANTOUSHA_CD = /*data.eigyouTantoushaCd*/''/*END*/
AND EXISTS (SELECT * FROM T_SHUKKA_DETAIL
                LEFT JOIN M_HINMEI ON T_SHUKKA_DETAIL.HINMEI_CD = M_HINMEI.HINMEI_CD
                    WHERE T_SHUKKA_DETAIL.SYSTEM_ID = T_SHUKKA_ENTRY.SYSTEM_ID
                      AND T_SHUKKA_DETAIL.SEQ = T_SHUKKA_ENTRY.SEQ
/*IF data.hinmeiCd != null*/AND T_SHUKKA_DETAIL.HINMEI_CD = /*data.hinmeiCd*/''/*END*/
/*IF data.shuruiCd != null*/AND M_HINMEI.SHURUI_CD = /*data.shuruiCd*/''/*END*/
/*IF data.bunruiCd != null*/AND M_HINMEI.BUNRUI_CD = /*data.bunruiCd*/''/*END*/
/*IF !data.unitCd.IsNull*/AND T_SHUKKA_DETAIL.UNIT_CD = /*data.unitCd.Value*/0/*END*/
/*IF !data.denpyouKbnCd.IsNull*/AND T_SHUKKA_DETAIL.DENPYOU_KBN_CD = /*data.denpyouKbnCd.Value*/0/*END*/
/*IF !data.torihikiKbnCd.IsNull*/  AND (   (T_SHUKKA_DETAIL.DENPYOU_KBN_CD = 1 AND T_SHUKKA_ENTRY.URIAGE_TORIHIKI_KBN_CD = /*data.torihikiKbnCd*/2)
					                  OR (T_SHUKKA_DETAIL.DENPYOU_KBN_CD = 2 AND T_SHUKKA_ENTRY.SHIHARAI_TORIHIKI_KBN_CD = /*data.torihikiKbnCd*/2))/*END*/)
ORDER BY T_SHUKKA_ENTRY.SYSTEM_ID