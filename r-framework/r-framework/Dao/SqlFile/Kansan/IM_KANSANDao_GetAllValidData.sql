SELECT * FROM dbo.M_KANSAN
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF !data.DENPYOU_KBN_CD.IsNull*/AND DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD.Value*//*END*/
/*IF data.HINMEI_CD != null*/AND HINMEI_CD = /*data.HINMEI_CD*//*END*/
/*IF !data.UNIT_CD.IsNull*/AND UNIT_CD = /*data.UNIT_CD.Value*//*END*/
/*IF !data.KANSANSHIKI.IsNull*/AND KANSANSHIKI = /*data.KANSANSHIKI.Value*//*END*/
/*IF !data.KANSANCHI.IsNull*/AND KANSANCHI = /*data.KANSANCHI.Value*//*END*/
/*IF data.KANSAN_BIKOU != null*/AND KANSAN_BIKOU = /*data.KANSAN_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
