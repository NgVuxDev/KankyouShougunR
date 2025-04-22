SELECT * FROM dbo.M_BIKO_UCHIWAKE_NYURYOKU
WHERE
--/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 --DELETE_FLG = 0
-- ELSE
 --1 = 1
--/*END*/
/*IF data.BIKO_KBN_CD != null*/BIKO_KBN_CD = /*data.BIKO_KBN_CD*//*END*/
/*IF data.BIKO_CD != null*/AND BIKO_CD = /*data.BIKO_CD*//*END*/
/*IF data.BIKO_NOTE != null*/AND BIKO_NOTE = /*data.BIKO_NOTE*//*END*/
--/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
--/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
--/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
--/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
--/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
--/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
