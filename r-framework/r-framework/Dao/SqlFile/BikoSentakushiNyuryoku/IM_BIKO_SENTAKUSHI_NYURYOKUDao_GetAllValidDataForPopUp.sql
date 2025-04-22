SELECT * FROM dbo.M_BIKO_SENTAKUSHI_NYURYOKU
WHERE DELETE_FLG = 0
/*IF !data.BIKO_DEFAULT_KBN.IsNull*/AND BIKO_DEFAULT_KBN = /*data.BIKO_DEFAULT_KBN*//*END*/
/*IF data.BIKO_CD != null*/AND BIKO_CD = /*data.BIKO_CD*//*END*/
/*IF data.BIKO_NOTE != null*/AND BIKO_NOTE = /*data.BIKO_NOTE*//*END*/
--/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
--/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
--/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
--/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
--/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
--/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
